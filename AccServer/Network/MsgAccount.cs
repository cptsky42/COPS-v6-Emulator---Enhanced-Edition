// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2010 - 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using COServer.Network.Sockets;
using COServer.Security.Cryptography;

[assembly: InternalsVisibleTo("Msg")]

namespace COServer.Network
{
    /// <summary>
    /// First message received by the server. The client a new connection.
    /// </summary>
    public class MsgAccount : Msg
    {
        /// <summary>
        /// This is a "constant" that the child must override.
        /// It is the type of the message as specified in NetworkDef.cs file.
        /// </summary>
        protected override UInt16 _TYPE { get { return MSG_ACCOUNT; } }

        //--------------- Internal Members ---------------
        private String __Account = "";
        private String __Password = "";
        private String __Server = "";
        //------------------------------------------------

        /// <summary>
        /// Account's name.
        /// </summary>
        public String Account
        {
            get { return __Account; }
            set { __Account = value; WriteString(4, value, MAX_NAME_SIZE); }
        }

        /// <summary>
        /// Account's password.
        /// </summary>
        public String Password
        {
            get { return __Password; }
            set
            {
                __Password = value;

                RivestCipher5 cipher = new RivestCipher5(COServer.Server.RC5_SEED);

                Byte[] pwd_data = new Byte[MAX_NAME_SIZE];
                Byte[] password = Program.Encoding.GetBytes(value);

                Buffer.BlockCopy(password, 0, pwd_data, 0, password.Length);
                cipher.Encrypt(ref pwd_data, MAX_NAME_SIZE);

                Buffer.BlockCopy(pwd_data, 0, mBuf, 20, MAX_NAME_SIZE);
            }
        }

        /// <summary>
        /// Game server's name.
        /// </summary>
        public String Server
        {
            get { return __Server; }
            set { __Server = value; WriteString(36, value, MAX_NAME_SIZE); }
        }

        /// <summary>
        /// Create a message object from the specified buffer.
        /// </summary>
        /// <param name="aBuf">The buffer containing the message.</param>
        /// <param name="aIndex">The index where the message is starting in the buffer.</param>
        /// <param name="aLength">The length of the message.</param>
        internal MsgAccount(Byte[] aBuf, int aIndex, int aLength)
            : base(aBuf, aIndex, aLength)
        {
            RivestCipher5 cipher = new RivestCipher5(COServer.Server.RC5_SEED);

            Byte[] pwd_data = new Byte[MAX_NAME_SIZE];
            Buffer.BlockCopy(mBuf, 20, pwd_data, 0, MAX_NAME_SIZE);
            cipher.Decrypt(ref pwd_data, MAX_NAME_SIZE);

            __Account = Program.Encoding.GetString(mBuf, 4, MAX_NAME_SIZE).TrimEnd('\0');
            __Password = Program.Encoding.GetString(pwd_data, 0, MAX_NAME_SIZE).TrimEnd('\0');
            __Server = Program.Encoding.GetString(mBuf, 36, MAX_NAME_SIZE).TrimEnd('\0');
        }

        /// <summary>
        /// Process the message for the specified client.
        /// </summary>
        /// <param name="aClient">The client who sent the message.</param>
        public override void Process(Client aClient)
        {
            bool create_acc = false;

            if (Account.StartsWith("NEW"))
            {
                Account = Account.Substring("NEW".Length, Account.Length - "NEW".Length);
                create_acc = true;
            }

            Byte[] pwd_data = Program.Encoding.GetBytes(Password);
            Byte[] acc_data = Program.Encoding.GetBytes(Account);
            Byte[] salt = null;
            Byte[] hash = null;

            using (var sha256 = SHA256.Create())
                salt = sha256.ComputeHash(acc_data, 0, acc_data.Length);

            using (var PBKDF2 = new Rfc2898DeriveBytes(pwd_data, salt, 100000))
                hash = PBKDF2.GetBytes(32); // 256 bits

            String password = "";
            for (Int32 i = 0, len = hash.Length; i < len; ++i)
                password += Convert.ToString(hash[i], 16).PadLeft(2, '0');

            if (Database.IsBanned(aClient.IPAddress))
            {
                sLogger.Info("{0} tried to log in server {1}, but is using a banned IP address ({2}).",
                    Account, Server, aClient.IPAddress);
                aClient.Send(new MsgConnectEx(MsgConnectEx.ErrorId.Banned));
                return;
            }

            if (create_acc)
            {
                if (!Database.Create(aClient, Account, password))
                {
                    aClient.Send(new MsgConnectEx(MsgConnectEx.ErrorId.AccountExist));
                    return;
                }
                sLogger.Info("Creation of {0}, with {1} ({2}).",
                    Account, aClient.IPAddress, Database.GetCountryCode(aClient.IPAddress));
            }

            if (Database.Authenticate(Account, password))
            {
                if (File.Exists(Program.RootPath + "/Servers/" + Server + ".ini"))
                {
                    if (!Database.GetAccInfo(aClient, Account, Server))
                    {
                        aClient.Send(new MsgConnectEx(MsgConnectEx.ErrorId.TryLater));
                        return;
                    }

                    if (aClient.IsBanned)
                    {
                        aClient.Send(new MsgConnectEx(MsgConnectEx.ErrorId.Banned));
                        return;
                    }

                    SvrInfo svr = new SvrInfo(Program.RootPath + "/Servers/" + Server + ".ini");
                    UInt32 accUID = aClient.AccountID;
                    UInt32 token = 0;

                    if (!Database.GenerateToken(out token, aClient))
                    {
                        aClient.Send(new MsgConnectEx(MsgConnectEx.ErrorId.TryLater));
                        return;
                    }

                    using (TcpClient socket = new TcpClient())
                    {
                        try { socket.Connect(svr.IPAddress, svr.Port); }
                        catch
                        {
                            sLogger.Warn("Server {0} is down !", Server);
                            aClient.Send(new MsgConnectEx(MsgConnectEx.ErrorId.ServerDown));
                            return;
                        }
                    }

                    Database.UpdateLastIP(aClient);

                    sLogger.Info("Connection of {0} ({1}), with account {2}, on {3}.",
                        aClient.IPAddress, Database.GetCountryCode(aClient.IPAddress), Account, Server);
                    aClient.Send(new MsgConnectEx(accUID, token, svr.IPAddress, svr.Port));
                }
                else
                {
                    sLogger.Warn("Server {0} does not exist.", Server);
                    aClient.Send(new MsgConnectEx(MsgConnectEx.ErrorId.TryLater));
                }
            }
            else
            {
                aClient.Send(new MsgConnectEx(MsgConnectEx.ErrorId.InvalidPassword));
            }
        }
    }
}
