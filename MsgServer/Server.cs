// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2010 - 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.IO;
using System.Text;
using COServer.Network.Sockets;
using COServer.Script;
using COServer.Threads;
using COServer.Workers;

namespace COServer
{
    public class Server
    {
        /// <summary>
        /// The logger of the class.
        /// </summary>
        private static readonly log4net.ILog sLogger = log4net.LogManager.GetLogger(typeof(Server));

        /// <summary>
        /// The path of the configuration file.
        /// </summary>
        public static readonly String CONFIG_FILE = Program.RootPath + "/MsgServer.ini";

        /// <summary>
        /// The workers for processing networking I/O.
        /// </summary>
        public static NetworkIO NetworkIO;

        /// <summary>
        /// The socket of the server.
        /// </summary>
        private static TcpServer sSocket;

        /// <summary>
        /// The name of the server.
        /// </summary>
        public static String Name;

        /// <summary>
        /// The time of the launch.
        /// </summary>
        public static DateTime LaunchTime;

        /// <summary>
        /// Start the execution of the server.
        /// </summary>
        public static void Run()
        {
            if (!File.Exists(CONFIG_FILE))
            {
                sLogger.Fatal("Couldn't find the configuration file at '{0}'.", CONFIG_FILE);
                Environment.Exit(0);
            }

            UInt16 port = 0;
            Int32 backlog = 0;
            Byte threadAmount = 1;

            String mysql_host, mysql_db, mysql_user, mysql_pwd;
            int mysql_nb_connections;

            String acc_host, acc_db, acc_user, acc_pwd;
            int acc_nb_connections;

            String mongo_host, mongo_db, mongo_user, mongo_pwd;

            using (Ini doc = new Ini(CONFIG_FILE))
            {
                port = doc.ReadUInt16("Socket", "Port", 5816);
                backlog = doc.ReadInt32("Socket", "BackLog", 100);
                threadAmount = doc.ReadUInt8("Socket", "ThreadAmount", 1);

                Name = doc.ReadString("Program", "Name", "NULL");
                Program.Encoding = Encoding.GetEncoding(doc.ReadString("Program", "Encoding", "iso-8859-1"));
                Program.Debug = doc.ReadBoolean("Program", "Debug", false);

                mysql_host = doc.ReadString("MySQL", "Host", "localhost");
                mysql_db = doc.ReadString("MySQL", "Database", "zfserver");
                mysql_user = doc.ReadString("MySQL", "Username", "zfserver");
                mysql_pwd = doc.ReadString("MySQL", "Password", "");
                mysql_nb_connections = doc.ReadInt32("MySQL", "NbConnections", 1);

                acc_host = doc.ReadString("AccMySQL", "Host", "localhost");
                acc_db = doc.ReadString("AccMySQL", "Database", "zfserver");
                acc_user = doc.ReadString("AccMySQL", "Username", "zfserver");
                acc_pwd = doc.ReadString("AccMySQL", "Password", "");
                acc_nb_connections = doc.ReadInt32("AccMySQL", "NbConnections", 1);

                mongo_host = doc.ReadString("MongoDB", "Host", "localhost");
                mongo_db = doc.ReadString("MongoDB", "Database", "zfserver");
                mongo_user = doc.ReadString("MongoDB", "Username", "zfserver");
                mongo_pwd = doc.ReadString("MongoDB", "Password", "");
            }

            StrRes.LoadStrRes();

            if (!Database.SetupAccMySQL(acc_nb_connections, acc_host, acc_db, acc_user, acc_pwd))
            {
                sLogger.Fatal("Failed to setup MySQL...");
                Environment.Exit(0);
            }

            if (!Database.SetupMySQL(mysql_nb_connections, mysql_host, mysql_db, mysql_user, mysql_pwd))
            {
                sLogger.Fatal("Failed to setup MySQL...");
                Environment.Exit(0);
            }

            if (!Database.SetupMongo(mongo_host, mongo_db, mongo_user, mongo_pwd))
            {
                sLogger.Fatal("Failed to setup MongoDB...");
                Environment.Exit(0);
            }


            // load Lua VM
            Task.RegisterFunctions(); // register Lua functions
            TaskHandler.LoadAllTasks();

            if (!MapManager.LoadData())
            {
                sLogger.Fatal("Failed to load DMaps in memory...");
                Environment.Exit(0);
            }

            Database.GetItemsInfo();
            Database.GetItemsBonus();
            Database.GetShopsInfo();
            Database.GetMagicsInfo();
            Database.GetLevelsInfo();
            Database.GetPointAllotInfo();
            Database.LoadWeaponSkillReqExp();
            Database.GetPortalsInfo();
            Database.GetAllMaps();
            Database.GetMonstersInfo();
            Database.GetSpawnsInfo();
            Database.GetAllNPCs();
            Database.GetAllItems();
            Database.LoadAllSyndicates();

            ServiceEventsListener.Create();

            NetworkIO = new NetworkIO(threadAmount);

            sSocket = new TcpServer();
            sSocket.OnConnect += new NetworkClientConnection(ConnectionHandler);
            sSocket.OnReceive += new NetworkClientReceive(ReceiveHandler);
            sSocket.OnDisconnect += new NetworkClientConnection(DisconnectionHandler);
            sSocket.Listen(port, backlog);
            sSocket.Accept();

            LaunchTime = DateTime.Now;
            sLogger.Info("Waiting for new connection...");
        }

        /// <summary>
        /// Restart the server (a maintenance will be triggered).
        /// </summary>
        public static void Restart()
        {
            MaintenanceSystem.Instance.Trigger();

            if (sSocket != null)
            {
                // disconnect directly the client on connection...
                sSocket.OnConnect += new NetworkClientConnection(DisconnectionHandler);
            }
        }

        /// <summary>
        /// Stop the server (a maintenance will be triggered).
        /// </summary>
        public static void Stop()
        {
            MaintenanceSystem.Instance.Trigger(false);

            if (sSocket != null)
            {
                // disconnect directly the client on connection...
                sSocket.OnConnect += new NetworkClientConnection(DisconnectionHandler);
            }
        }

        /// <summary>
        /// Handle a new connection on the server.
        /// </summary>
        /// <param name="aSocket">The socket of the newly connected client.</param>
        private static void ConnectionHandler(TcpSocket aSocket)
        {
            try
            {
                if (aSocket != null)
                    aSocket.Wrapper = new Client(aSocket);
            }
            catch (Exception exc) { sLogger.Error(exc); }
        }

        /// <summary>
        /// Handle the reception of data from a client.
        /// </summary>
        /// <param name="aSocket">The socket of the client that sent the data.</param>
        /// <param name="aData">The data.</param>
        private static void ReceiveHandler(TcpSocket aSocket, ref Byte[] aData)
        {
            try
            {
                if (aSocket != null && aSocket.Wrapper != null)
                {
                    Client client = aSocket.Wrapper as Client;

                    if (client == null)
                        return;

                    client.Receive(ref aData);
                }
            }
            catch (Exception exc) { sLogger.Error(exc); aSocket.Disconnect(); }
        }

        /// <summary>
        /// Handle the disconnection of a client.
        /// </summary>
        /// <param name="aSocket">The socket fo the client now disconnected.</param>
        private static void DisconnectionHandler(TcpSocket aSocket)
        {
            try
            {
                if (aSocket != null && aSocket.Wrapper != null)
                {
                    Client client = aSocket.Wrapper as Client;

                    if (client.Account != null)
                        sLogger.Info("Disconnection of {0}, with {1}.", aSocket.IPAddress, client.Account);

                    client.Disconnect();
                    client.Dispose();

                    aSocket.Wrapper = null;
                }
                else if (aSocket != null)
                {
                    aSocket.Disconnect();
                }
            }
            catch (Exception exc) { sLogger.Error(exc); }
        }
    }
}
