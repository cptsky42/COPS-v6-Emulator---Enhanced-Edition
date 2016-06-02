// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2010 - 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.Net;
using System.Net.Sockets;

namespace COServer.Network.Sockets
{
    /// <summary>
    /// TCP/IP client.
    /// </summary>
    public class TcpClient : IDisposable
    {
        /// <summary>
        /// Internal socket.
        /// </summary>
        private Socket mSocket;
        /// <summary>
        /// Internal buffer to receive data from the server.
        /// </summary>
        private Byte[] mBuf;

        /// <summary>
        /// Indicate whether or not the instance has been disposed.
        /// </summary>
        private bool mIsDisposed = false;

        /// <summary>
        /// Create a new TCP/IPv4 socket that will act as a client.
        /// </summary>
        public TcpClient()
        {
            const Int32 MAX_BUFFER_SIZE = 2048;

            mSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            mSocket.ReceiveBufferSize = MAX_BUFFER_SIZE;
            mSocket.SendBufferSize = MAX_BUFFER_SIZE;

            mBuf = new Byte[MAX_BUFFER_SIZE];
        }

        ~TcpClient()
        {
            Dispose(false);
        }

        /// <summary>
        /// Dispose the socket.
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
        /// Connect the client to the specified server.
        /// </summary>
        /// <param name="aIPAddress">The IP address of the server.</param>
        /// <param name="aPort">The port of the server.</param>
        public void Connect(String aIPAddress, UInt16 aPort)
        {
            if (!mSocket.Connected)
                mSocket.Connect(IPAddress.Parse(aIPAddress), aPort);
            else
                throw new SocketException((int)SocketError.IsConnected);
        }

        /// <summary>
        /// Begin the asynchronous operations to send the data to the server.
        /// </summary>
        /// <param name="aData">The data to send to the server.</param>
        public void Send(Byte[] aData)
        {
            if (mSocket.Connected)
            {
                try { mSocket.BeginSend(aData, 0, aData.Length, SocketFlags.None, new AsyncCallback(EndSend), null); }
                catch { Disconnect(); }
            }
            else
                throw new SocketException((int)SocketError.NotConnected);
        }

        /// <summary>
        /// Called by the asynchronous callback when the data is sent.
        /// </summary>
        private void EndSend(IAsyncResult res)
        {
            try { mSocket.EndSend(res); }
            catch { Disconnect(); }
        }

        /// <summary>
        /// Disconnect the client from the server.
        /// </summary>
        public void Disconnect()
        {
            if (mSocket != null && mSocket.Connected)
            {
                try { mSocket.Disconnect(true); }
                catch { }
            }
        }
    }
}