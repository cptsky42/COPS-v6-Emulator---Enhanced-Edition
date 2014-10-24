// * Created by Jean-Philippe Boivin
// * Copyright © 2010-2011
// * Logik. Project

using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Globalization;
using COServer.Script;
using COServer.Threads;
using COServer.Network;
using AMS.Profile;
using CO2_CORE_DLL;
using CO2_CORE_DLL.Net.Sockets;
using CO2_CORE_DLL.Security.Cryptography;

namespace COServer
{
    public unsafe class Server
    {
        private static MaintenanceSystem Maintenance;
        public static NetworkIO NetworkIO;
        public static ServerSocket Socket;

        public static Int32 COSAC_PKey = 0x13FA0F9D;
        public static Int32 COSAC_GKey = 0x6D5C7962;

        public static String Name;
        public static Int32 Version_FR;
        public static Int32 Version_EN;

        public static TcpClient AuthSocket;
        public static COCAC AuthCrypto;

        public static String AuthIP;
        public static Int16 AuthPort;

        public static DateTime LaunchTime;

        public static Int32 MaxPlayers;

        public static Boolean Backup_FTP;
        public static String Backup_Server;
        public static String Backup_Username;
        public static String Backup_Password;

        public static void Run()
        {
            if (!File.Exists(Program.RootPath + "\\MsgServer.xml"))
            {
                Environment.Exit(0);
                return;
            }

            UInt16 Port = 0;
            Int32 BackLog = 0;
            Int32 ThreadAmount = 1;

            Xml AMSXml = new Xml(Program.RootPath + "\\MsgServer.xml");
            AMSXml.RootName = "MsgServer";

            Port = (UInt16)AMSXml.GetValue("Socket", "Port", 5816);
            BackLog = AMSXml.GetValue("Socket", "BackLog", 100);
            ThreadAmount = AMSXml.GetValue("Socket", "ThreadAmount", 1);

            AuthIP = AMSXml.GetValue("Program", "AuthIP", "127.0.0.1");
            AuthPort = (Int16)AMSXml.GetValue("Program", "AuthPort", 9958);

            Name = AMSXml.GetValue("Program", "Name", "NULL");
            Program.Encoding = Encoding.GetEncoding(AMSXml.GetValue("Program", "Encoding", "iso-8859-1"));
            Program.Debug = AMSXml.GetValue("Program", "Debug", false);
            Version_FR = AMSXml.GetValue("Version", "French", 0);
            Version_EN = AMSXml.GetValue("Version", "English", 0);
            COSAC_PKey = Int32.Parse(AMSXml.GetValue("Cryptography", "COSAC_PKey", "13FA0F9D"), NumberStyles.HexNumber);
            COSAC_GKey = Int32.Parse(AMSXml.GetValue("Cryptography", "COSAC_GKey", "6D5C7962"), NumberStyles.HexNumber);
            Database.Rates = new Database.CRates(AMSXml);
            MaxPlayers = AMSXml.GetValue("Records", "MaxPlayers", 0);

            Backup_FTP = AMSXml.GetValue("Backup", "UseFTP", false);
            Backup_Server = AMSXml.GetValue("Backup", "Server", "ftp://127.0.0.1/");
            Backup_Username = AMSXml.GetValue("Backup", "Username", "anonymous");
            Backup_Password = AMSXml.GetValue("Backup", "Password", "");

            AMSXml = null;

            STR.LoadStrRes();
            Database2.GetItemsInfo();
            Database.GetItemsBonus();
            Database.GetShopsInfo();
            Database.GetPrizesInfo();
            Database2.GetMagicsInfo();
            Database.GetLevelsInfo();
            Database.GetPointAllotInfo();
            Database.GetPortalsInfo();
            Database.GetMonstersInfo();
            Database.GetSpawnsInfo();
            Database.GetAllMaps();
            Database.GetAllNPCs();
            Database.GetAllItems();
            Database.GetAllWeaponSkills();
            Database.GetAllMagics();
            Database.GetAllSyndicates();
            Database.GetNobilityRank();
            ScriptHandler.GetAllScripts();
            Generator.Generate();
            Maintenance = new MaintenanceSystem();

            Int16[] Maps = new Int16[] { 1011, 1020, 1000, 1015, 1038 };
            foreach (Int16 MapUID in Maps)
            {
                Map Map = null;
                if (!World.AllMaps.TryGetValue(MapUID, out Map))
                    continue;

                Map.Holder = GetHolder(MapUID);
                Map.RenamePole();
            }

            while (true)
            {
                try { AuthSocket = new TcpClient(AuthIP, AuthPort); }
                catch (SocketException Exc)
                {
                    Console.WriteLine("Can't connect to AccServer! Error: {0}", Exc.ErrorCode);
                    Thread.Sleep(5000);
                    continue;
                }

                AuthCrypto = new COCAC();
                AuthCrypto.GenerateIV(COSAC_PKey, COSAC_GKey);
                break;
            }
            new GeneralThread();

            NetworkIO = new NetworkIO(ThreadAmount);

            Socket = new ServerSocket();
            Socket.OnConnect += new NetworkClientConnection(ConnectionHandler);
            Socket.OnReceive += new NetworkClientReceive(ReceiveHandler);
            Socket.OnDisconnect += new NetworkClientConnection(DisconnectionHandler);
            Socket.Listen(Port, BackLog);
            Socket.Accept();

            LaunchTime = DateTime.Now;
            Console.WriteLine("Waiting for new connection...");
        }

        public static Int16 GetHolder(Int16 MapUID)
        {
            Int16 Holder = 0;
            try
            {
                Xml AMSXml = new Xml(Program.RootPath + "\\MsgServer.xml");
                AMSXml.RootName = "MsgServer";

                using (AMSXml.Buffer()) { Holder = (Int16)AMSXml.GetValue("Holders", MapUID.ToString(), 0); }

                AMSXml = null;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
            return Holder;
        }

        public static void SetHolder(Int16 MapUID, Int16 Holder)
        {
            try
            {
                Xml AMSXml = new Xml(Program.RootPath + "\\MsgServer.xml");
                AMSXml.RootName = "MsgServer";

                using (AMSXml.Buffer()) { AMSXml.SetValue("Holders", MapUID.ToString(), Holder); }

                AMSXml = null;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }

        public static void Restart()
        { 
            Maintenance.Execute(false); 
        }

        public static void SaveMaxPlayersRecord(Int32 PlayersOnline)
        {
            try
            {
                Xml AMSXml = new Xml(Program.RootPath + "\\MsgServer.xml");
                AMSXml.RootName = "MsgServer";

                using (AMSXml.Buffer())
                {
                    AMSXml.SetValue("Records", "MaxPlayers", PlayersOnline);
                }

                AMSXml = null;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
            MaxPlayers = PlayersOnline;
            Program.Log(String.Format("{0} players online on {1}! It's a new record!", PlayersOnline, Name));
        }

        private static void ConnectionHandler(Object Obj)
        {
            try
            {
                NetworkClient Socket = Obj as NetworkClient;
                if (Socket != null)
                {
                    Socket.Owner = new Client(Socket);
                    NetworkIO.AddClient(Socket.Owner as Client);
                }
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }

        private static void ReceiveHandler(Object Obj, Byte[] Buffer)
        {
            NetworkClient Socket = Obj as NetworkClient;

            try
            {
                if (Socket != null && Socket.Owner != null)
                {
                    if (Buffer.Length < sizeof(Msg.MsgHeader))
                        return;

                    Int32 Length = Buffer.Length;

                    Byte* Received = stackalloc Byte[Length];
                    Kernel.memcpy(Received, Buffer, Length);

                    (Socket.Owner as Client).Cipher.Decrypt(Received, Length);

                    Int16 Size = 0;
                    for (Int32 i = 0; i < Length; i += Size)
                    {
                        Size = ((Msg.MsgHeader*)(Received + i))->Length;
                        if (Size < Length)
                        {
                            Byte[] Packet = new Byte[Size];
                            Kernel.memcpy(Packet, Received + i, Size);
                            Server.NetworkIO.Receive((Socket.Owner as Client), Packet);
                        }
                        else
                        {
                            Byte[] Packet = new Byte[Length];
                            Kernel.memcpy(Packet, Received, Length);
                            Server.NetworkIO.Receive((Socket.Owner as Client), Packet);
                        }
                    }
                }
            }
            catch (Exception Exc) { Program.WriteLine(Exc); (Socket.Owner as Client).Disconnect(); }
        }

        private static void DisconnectionHandler(Object Obj)
        {
            try
            {
                NetworkClient Socket = Obj as NetworkClient;
                if (Socket != null && Socket.Owner != null)
                {
                    if ((Socket.Owner as Client).Account != null)
                        Program.Log("Disconnection of " + Socket.IpAddress + ", with " + (Socket.Owner as Client).Account + ".");
                    (Socket.Owner as Client).Disconnect();
                }
                else if (Socket != null)
                    Socket.Disconnect();
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }

        public static Boolean SendToAuth(Byte[] Buffer)
        {
            try
            {
                if (Buffer == null)
                    return false;

                Byte[] Packet = new Byte[Buffer.Length];
                Buffer.CopyTo(Packet, 0);

                if (AuthCrypto == null || AuthSocket == null)
                    return false;

                lock (AuthCrypto)
                {
                    lock (AuthSocket)
                    {
                        AuthCrypto.Encrypt(ref Packet);
                        try { AuthSocket.Client.Send(Packet); }
                        catch { return false; }
                    }
                }
                return true;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return false; }
        }
    }
}
