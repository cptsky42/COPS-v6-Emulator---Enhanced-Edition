// * Created by Jean-Philippe Boivin
// * Copyright © 2010-2011
// * Logik. Project

using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using CO2_CORE_DLL;
using CO2_CORE_DLL.Net.Sockets;
using CO2_CORE_DLL.Security.Cryptography;

namespace COServer.Network
{
    public unsafe class MsgAccount : Msg
    {
        public const Int16 Id = _MSG_ACCOUNT;

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct MsgInfo
        {
            public MsgHeader Header;
            public fixed Byte Account[MAX_NAME_SIZE];
            public fixed Byte Password[MAX_NAME_SIZE];
            public fixed Byte Server[MAX_NAME_SIZE];
        };

        public static void Process(Client Client, Byte[] Buffer)
        {
            try
            {
                if (Client == null || Buffer == null)
                    return;

                if (Buffer.Length != sizeof(MsgInfo))
                    return;

                fixed (Byte* pBuf = Buffer)
                {
                    MsgInfo* pMsg = (MsgInfo*)pBuf;

                    String Account = Kernel.cstring(pMsg->Account, MAX_NAME_SIZE);
                    String Server = Kernel.cstring(pMsg->Server, MAX_NAME_SIZE);

                    Byte[] Data = new Byte[MAX_NAME_SIZE];
                    Kernel.memcpy(Data, pMsg->Password, MAX_NAME_SIZE);
                    Client.RC5.Decrypt(ref Data);

                    String Password = "";
                    Byte[] Hash;

                    SHA256 Sha256 = SHA256.Create();
                    fixed (Byte* pData = Data)
                        Hash = Sha256.ComputeHash(Data, 0, Kernel.strlen(pData));

                    for (Int32 i = 0; i < Hash.Length; i++)
                        Password += Convert.ToString(Hash[i], 16).PadLeft(2, '0');
                    
                    if (Database.IsIPBanned(Client.Socket.IpAddress))
                    {
                        Client.Send(MsgLoginReply.Create(0, (Int32)MsgLoginReply.ErrorId.Banned, MsgLoginReply.ERROR_STR, 0));
                        return;
                    }

                    if (Account.StartsWith("NEW"))
                    {
                        Account = Account.Substring(3, Account.Length - 3);
                        if (!Database.Create(Account, Password))
                        {
                            Client.Send(MsgLoginReply.Create(0, (Int32)MsgLoginReply.ErrorId.Acc_Exist, MsgLoginReply.ERROR_STR, 0));
                            return;
                        }
                        Program.Log("Creation of " + Account + ", with " + Client.Socket.IpAddress + ".");
                    }

                    if (Database.Authenticate(Account, Password))
                    {
                        if (File.Exists(Program.RootPath + "\\Servers\\" + Server + ".svr"))
                        {
                            SvrInfo Svr = new SvrInfo(Program.RootPath + "\\Servers\\" + Server + ".svr");

                            ClientSocket Socket = new ClientSocket();
                            try { Socket.Connect(Svr.IPAddress, Svr.Port); }
                            catch
                            {
                                Client.Send(MsgLoginReply.Create(0, (Int32)MsgLoginReply.ErrorId.Server_Down, MsgLoginReply.ERROR_STR, 0));
                                return;
                            }

                            if (!Database.GetAccInfo(Client, Account, Server))
                            {
                                Client.Socket.Disconnect();
                                return;
                            }

                            if ((Client.Flags & 0x01) != 0)
                            {
                                Client.Send(MsgLoginReply.Create(0, (Int32)MsgLoginReply.ErrorId.Banned, MsgLoginReply.ERROR_STR, 0));
                                return;
                            }

                            Int32 AccountID = Account.GetHashCode();
                            Int32 Token = Environment.TickCount;

                            //** Communicate with MsgServer
                            COCAC Crypto = new COCAC();
                            Crypto.GenerateIV(COServer.Server.COSAC_PKey, COServer.Server.COSAC_GKey);
                            Byte[] Packet = MsgAccountExt.Create(
                                Client.Account,
                                Client.AccLvl,
                                Client.Flags,
                                Token,
                                AccountID,
                                Client.Character);
                            Crypto.Encrypt(ref Packet);

                            Socket.Send(Packet);
                            Socket.Disconnect();
                            //**

                            Program.Log("Connection of " + Client.Socket.IpAddress + ", with " + Account + ", on " + Server + ".");
                            Client.Send(MsgLoginReply.Create(AccountID, Token, Svr.IPAddress, Svr.Port));
                            return;
                        }
                        else
                        {
                            Client.Send(MsgLoginReply.Create(0, (Int32)MsgLoginReply.ErrorId.Try_Later, MsgLoginReply.ERROR_STR, 0));
                            return;
                        }
                    }
                    else
                    {
                        Client.Send(MsgLoginReply.Create(0, (Int32)MsgLoginReply.ErrorId.Invalid_Acc, MsgLoginReply.ERROR_STR, 0));
                        return;
                    }
                }
            }
            catch (Exception Exc) { Program.WriteLine(Exc); Client.Socket.Disconnect(); }
        }
    }
}
