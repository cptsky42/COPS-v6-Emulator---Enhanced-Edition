// * Created by Jean-Philippe Boivin
// * Copyright © 2010
// * Logik. Project

using System;
using COServer.Threads;
using CO2_CORE_DLL.Net.Sockets;
using CO2_CORE_DLL.Security.Cryptography;

namespace COServer
{
    public class Client
    {
        public NetworkClient Socket;
        public COSAC Cipher;
        public CORC5 RC5;

        public String Account;
        public SByte AccLvl;
        public Int32 Flags;
        public String Character;

        public Client(NetworkClient Socket)
        {
            this.Socket = Socket;
            this.Cipher = new COSAC();
            this.Cipher.GenerateIV(Server.COSAC_PKey, Server.COSAC_GKey);
            this.RC5 = new CORC5(Server.CORC5_PWKey, Server.CORC5_QWKey);
            this.RC5.GenerateKey(Server.CORC5_BufKey);

            this.Account = "@INVALID_ACC@";
            this.AccLvl = 0;
            this.Character = "@INVALID_CHAR@";
        }

        ~Client()
        {
            Socket = null;
            Cipher = null;
            RC5 = null;

            Account = null;
            Character = null;
        }

        public void Send(Byte[] Buffer)
        {
            Byte[] Packet = new Byte[Buffer.Length];

            Buffer.CopyTo(Packet, 0);
            Server.NetworkIO.Send(this, Packet);
        }
    }
}
