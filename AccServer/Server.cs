// * Created by Jean-Philippe Boivin
// * Copyright © 2010-2011
// * Logik. Project

using System;
using System.IO;
using System.Text;
using System.Globalization;
using COServer.Threads;
using COServer.Network;
using AMS.Profile;
using CO2_CORE_DLL;
using CO2_CORE_DLL.Security;
using CO2_CORE_DLL.Net.Sockets;

namespace COServer
{
    public unsafe class Server
    {
        //private static UsageChecker Checker;
        public static NetworkIO NetworkIO;
        private static ServerSocket Socket;

        public static Int32 COSAC_PKey = 0x13FA0F9D;
        public static Int32 COSAC_GKey = 0x6D5C7962;
        public static UInt32 CORC5_PWKey = 0xB7E15163;
        public static UInt32 CORC5_QWKey = 0x61C88647;
        public static Byte[] CORC5_BufKey = new Byte[] { 0x3C, 0xDC, 0xFE, 0xE8, 0xC4, 0x54, 0xD6, 0x7E, 0x16, 0xA6, 0xF8, 0x1A, 0xE8, 0xD0, 0x38, 0xBE };

        public static void Run()
        {
            if (!File.Exists(Program.RootPath + "\\AccServer.xml"))
            {
                Environment.Exit(0);
                return;
            }

            UInt16 Port = 0;
            Int32 BackLog = 0;

            Xml AMSXml = new Xml(Program.RootPath + "\\AccServer.xml");
            AMSXml.RootName = "AccServer";

            Port = (UInt16)AMSXml.GetValue("Socket", "Port", 9958);
            BackLog = AMSXml.GetValue("Socket", "BackLog", 100);
            Program.Encoding = Encoding.GetEncoding(AMSXml.GetValue("Program", "Encoding", "iso-8859-1"));
            Program.Debug = AMSXml.GetValue("Program", "Debug", false);
            COSAC_PKey = Int32.Parse(AMSXml.GetValue("Cryptography", "COSAC_PKey", "13FA0F9D"), NumberStyles.HexNumber);
            COSAC_GKey = Int32.Parse(AMSXml.GetValue("Cryptography", "COSAC_GKey", "6D5C7962"), NumberStyles.HexNumber);
            CORC5_PWKey = UInt32.Parse(AMSXml.GetValue("Cryptography", "CORC5_PWKey", "B7E15163"), NumberStyles.HexNumber);
            CORC5_QWKey = UInt32.Parse(AMSXml.GetValue("Cryptography", "CORC5_QWKey", "61C88647"), NumberStyles.HexNumber);
            AMSXml = null;

            Database.GetBannedIPs();

            NetworkIO = new NetworkIO();
            Socket = new ServerSocket();
            Socket.OnConnect += new NetworkClientConnection(ConnectionHandler);
            Socket.OnReceive += new NetworkClientReceive(ReceiveHandler);
            Socket.OnDisconnect += new NetworkClientConnection(DisconnectionHandler);
            Socket.Listen(Port, BackLog);
            Socket.Accept();

            Console.WriteLine("Waiting for new connection...");
        }

        private static void ConnectionHandler(Object Obj)
        {
            NetworkClient Socket = Obj as NetworkClient;
            if (Socket != null)
                Socket.Owner = new Client(Socket);
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
            catch (Exception Exc) { Program.WriteLine(Exc); Socket.Disconnect(); }
        }

        private static void DisconnectionHandler(Object Obj)
        {
            NetworkClient Socket = Obj as NetworkClient;
            if (Socket != null && Socket.Owner != null)
            {
                Socket.Owner = null;
            }
        }
    }
}
