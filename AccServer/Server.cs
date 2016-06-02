// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2010 - 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.Globalization;
using System.IO;
using System.Text;
using COServer.Network.Sockets;
using COServer.Security.Cryptography;
using COServer.Workers;

namespace COServer
{
    /// <summary>
    /// The server itself.
    /// </summary>
    public class Server
    {
        /// <summary>
        /// The logger of the class.
        /// </summary>
        private static readonly log4net.ILog sLogger = log4net.LogManager.GetLogger(typeof(Server));

        /// <summary>
        /// The seed of the RC5 algorithm used to encrypt passwords.
        /// </summary>
        public static readonly Byte[] RC5_SEED = new Byte[] { 0x3C, 0xDC, 0xFE, 0xE8, 0xC4, 0x54, 0xD6, 0x7E, 0x16, 0xA6, 0xF8, 0x1A, 0xE8, 0xD0, 0x38, 0xBE };

        /// <summary>
        /// The path of the configuration file.
        /// </summary>
        public static readonly String CONFIG_FILE = Program.RootPath + "/AccServer.ini";

        /// <summary>
        /// The socket of the server.
        /// </summary>
        private static TcpServer sSocket = null;

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

            String mysql_host, mysql_db, mysql_user, mysql_pwd;
            int mysql_nb_connections;

            String ip2loc_host, ip2loc_db, ip2loc_user, ip2loc_pwd;
            int ip2loc_nb_connections;

            using (Ini doc = new Ini(CONFIG_FILE))
            {
                port = doc.ReadUInt16("Socket", "Port", 9958);
                backlog = doc.ReadInt32("Socket", "BackLog", 100);
                Program.Encoding = Encoding.GetEncoding(doc.ReadString("Program", "Encoding", "iso-8859-1"));
                Program.Debug = doc.ReadBoolean("Program", "Debug", false);

                mysql_host = doc.ReadString("MySQL", "Host", "localhost");
                mysql_db = doc.ReadString("MySQL", "Database", "zfserver");
                mysql_user = doc.ReadString("MySQL", "Username", "zfserver");
                mysql_pwd = doc.ReadString("MySQL", "Password", "");
                mysql_nb_connections = doc.ReadInt32("MySQL", "NbConnections", 1);

                Database.IP2LOCATION_ENABLED = doc.ReadBoolean("IP2Location", "Enabled", false);
                ip2loc_host = doc.ReadString("IP2Location", "Host", "localhost");
                ip2loc_db = doc.ReadString("IP2Location", "Database", "zfserver");
                ip2loc_user = doc.ReadString("IP2Location", "Username", "zfserver");
                ip2loc_pwd = doc.ReadString("IP2Location", "Password", "");
                ip2loc_nb_connections = doc.ReadInt32("IP2Location", "NbConnections", 1);
            }

            if (!Database.SetupMySQL(mysql_nb_connections, mysql_host, mysql_db, mysql_user, mysql_pwd))
            {
                sLogger.Fatal("Failed to setup MySQL...");
                Console.ReadLine();
                Environment.Exit(0);
            }

            if (Database.IP2LOCATION_ENABLED)
            {
                if (!Database.SetupIP2Location(ip2loc_nb_connections, ip2loc_host, ip2loc_db, ip2loc_user, ip2loc_pwd))
                {
                    sLogger.Fatal("Failed to setup IP2Location...");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
            }

            Database.GetBannedIPs();

            sSocket = new TcpServer();
            sSocket.OnConnect += new NetworkClientConnection(ConnectionHandler);
            sSocket.OnReceive += new NetworkClientReceive(ReceiveHandler);
            sSocket.OnDisconnect += new NetworkClientConnection(DisconnectionHandler);
            sSocket.Listen(port, backlog);
            sSocket.Accept();

            sLogger.Info("Waiting for new connection...");
            ServiceEventsListener.Create();
        }

        /// <summary>
        /// Restart the server.
        /// </summary>
        public static void Restart()
        {
            if (sSocket != null)
            {
                // disconnect directly the client on connection...
                sSocket.OnConnect += new NetworkClientConnection(DisconnectionHandler);
            }

            Program.Exit(true);
        }

        /// <summary>
        /// Stop the server.
        /// </summary>
        public static void Stop()
        {
            if (sSocket != null)
            {
                // disconnect directly the client on connection...
                sSocket.OnConnect += new NetworkClientConnection(DisconnectionHandler);
            }

            Program.Exit(false);
        }

        /// <summary>
        /// Handle a new connection on the server.
        /// </summary>
        /// <param name="aSocket">The socket of the newly connected client.</param>
        private static void ConnectionHandler(TcpSocket aSocket)
        {
            if (aSocket != null)
                aSocket.Wrapper = new Client(aSocket);
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
            catch (Exception exc)
            {
                sLogger.Warn("Something wrong happened while receiving data.\n{0}", exc);
                aSocket.Disconnect();
            }
        }

        /// <summary>
        /// Handle the disconnection of a client.
        /// </summary>
        /// <param name="aSocket">The socket fo the client now disconnected.</param>
        private static void DisconnectionHandler(TcpSocket aSocket)
        {
            if (aSocket != null && aSocket.Wrapper != null)
            {
                Client client = aSocket.Wrapper as Client;
                client.Dispose();

                aSocket.Wrapper = null;
            }
        }
    }
}