// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2010 - 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.Runtime.CompilerServices;
using COServer.Network;
using COServer.Network.Sockets;
using COServer.Security.Cryptography;

[assembly: InternalsVisibleTo("Database")]

namespace COServer
{
    /// <summary>
    /// A client connected to the server.
    /// </summary>
    public class Client : IDisposable
    {
        /// <summary>
        /// The TCP/IP socket of the client.
        /// </summary>
        private TcpSocket mSocket;
        /// <summary>
        /// The cipher for decrypting/encrypting the network trafic.
        /// </summary>
        private TqCipher mCipher;

        /// <summary>
        /// Indicate whether or not the instance has been disposed.
        /// </summary>
        private bool mIsDisposed = false;

        /// <summary>
        /// Account ID.
        /// </summary>
        public UInt32 AccountID { get; internal set; }
        /// <summary>
        /// Account name.
        /// </summary>
        public String Account { get; internal set; }
        /// <summary>
        /// Whether or not the account is banned.
        /// </summary>
        public Boolean IsBanned { get; internal set; }

        /// <summary>
        /// The IP address of the client.
        /// </summary>
        public String IPAddress { get { return mSocket.IPAddress; } }
        /// <summary>
        /// The port of the client.
        /// </summary>
        public UInt16 Port { get { return mSocket.Port; } }

        /// <summary>
        /// Create a new client based on the newly connected socket.
        /// </summary>
        /// <param name="aSocket">The socket of the client.</param>
        public Client(TcpSocket aSocket)
        {
            mSocket = aSocket;
            mCipher = new TqCipher();

            AccountID = 0;
            Account = "@INVALID_ACC@";
            IsBanned = false;
        }

        ~Client()
        {
            Dispose(false);
        }

        /// <summary>
        /// Dispose the client. Its socket will be disposed too.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool aDisposing)
        {
            if (!mIsDisposed)
            {
                if (aDisposing)
                {
                    if (mSocket != null)
                        mSocket.Dispose();
                }

                mSocket = null;

                mIsDisposed = true;
            }
        }

        /// <summary>
        /// Send the message to the client.
        /// </summary>
        /// <param name="aMsg">The message to send to the client.</param>
        public void Send(Msg aMsg)
        {
            Byte[] msg = (Byte[])aMsg;
            Program.NetworkMonitor.Send(msg.Length);

            lock (mCipher)
            {
                mCipher.Encrypt(ref msg, msg.Length);
                mSocket.Send(msg);
            }
        }

        /// <summary>
        /// Handle the received data from the client.
        /// </summary>
        /// <param name="aData">The data sent by the client.</param>
        public void Receive(ref Byte[] aData)
        {
            Program.NetworkMonitor.Receive(aData.Length);

            if (aData.Length < Msg.MIN_SIZE)
                return;

            // decrypt the data
            mCipher.Decrypt(ref aData, aData.Length);

            UInt16 size = 0;
            for (Int32 i = 0; i < aData.Length; i += size)
            {
                size = (UInt16)((aData[i + 0x01] << 8) + aData[i + 0x00]);
                if (size < aData.Length)
                {
                    Msg msg = Msg.Create(aData, i, size);
                    msg.Process(this);
                }
                else
                {
                    Msg msg = Msg.Create(aData, 0, aData.Length);
                    msg.Process(this);
                }
            }
        }

        /// <summary>
        /// Disconnect the client.
        /// </summary>
        public void Disconnect()
        {
            mSocket.Disconnect();
        }
    }
}
