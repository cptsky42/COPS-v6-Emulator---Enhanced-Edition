// * Created by Jean-Philippe Boivin
// * Copyright © 2010, 2014-2015
// * COPS v6 Emulator

using System;
using System.Net;
using System.Net.Sockets;

namespace COServer.Network.Sockets
{
    /// <summary>
    /// TCP/IP client connected to a TcpServer.
    /// </summary>
    public class TcpSocket : IDisposable
    {
        /// <summary>
        /// Internal socket.
        /// </summary>
        private Socket mSocket;
        /// <summary>
        /// Server owning the socket.
        /// </summary>
        private TcpServer mServer;
        /// <summary>
        /// Internal buffer to receive data from the client.
        /// </summary>
        private Byte[] mBuf;

        /// <summary>
        /// Indicate whether or not the client is alive.
        /// </summary>
        private bool mIsAlive;

        /// <summary>
        /// Indicate whether or not the instance has been disposed.
        /// </summary>
        private bool mIsDisposed = false;

        /// <summary>
        /// The object wrapping the client.
        /// </summary>
        public Object Wrapper;

        /// <summary>
        /// Indicate whether or not the client is alive.
        /// </summary>
        public Boolean IsAlive { get { return mIsAlive; } }

        /// <summary>
        /// The IP address of the client.
        /// </summary>
        public String IPAddress
        {
            get
            {
                if (mSocket != null)
                    return (mSocket.RemoteEndPoint as IPEndPoint).Address.ToString();
                else
                    return "";
            }
        }

        /// <summary>
        /// The port of the client.
        /// </summary>
        public UInt16 Port
        {
            get
            {
                if (mSocket != null)
                    return (UInt16)(mSocket.RemoteEndPoint as IPEndPoint).Port;
                else
                    return 0;
            }
        }

        /// <summary>
        /// Create the instance, but a call to Create is needed.
        /// </summary>
        private TcpSocket(TcpServer aServer, Socket aSocket, int aBufSize)
        {
            mSocket = aSocket;
            mServer = aServer;

            mBuf = new Byte[aBufSize];
            mIsAlive = false;
        }

        ~TcpSocket()
        {
            Dispose(false);
        }

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
        /// Create the client of the specified server.
        /// </summary>
        public static TcpSocket Create(TcpServer aServer, Socket aSocket, int aBufSize)
        {
            if (aSocket == null || aServer == null || aBufSize == 0)
                throw new ArgumentException();

            TcpSocket client = new TcpSocket(aServer, aSocket, aBufSize);
            client.BeginReceive();
            client.mIsAlive = true;

            return client;
        }

        /// <summary>
        /// Begin the asynchronous operations to receive data from the server.
        /// </summary>
        private void BeginReceive()
        {
            try { mSocket.BeginReceive(mBuf, 0, mBuf.Length, SocketFlags.None, new AsyncCallback(EndReceive), null); }
            catch { mServer.InvokeDisconnect(this); }
        }

        /// <summary>
        /// Called by the asynchronous callback when the data is received.
        /// </summary>
        private void EndReceive(IAsyncResult res)
        {
            try
            {
                int len = mSocket.EndReceive(res);
                if (mIsAlive)
                {
                    if (len > 0)
                    {
                        Byte[] received = new Byte[len];
                        Buffer.BlockCopy(mBuf, 0, received, 0, len);

                        if (mServer.OnReceive != null)
                            mServer.OnReceive(this, ref received);

                        BeginReceive();
                    }
                    else
                        mServer.InvokeDisconnect(this);
                }
            }
            catch (SocketException) { mServer.InvokeDisconnect(this); }
        }

        /// <summary>
        /// Begin the asynchronous operations to send the data to the server.
        /// </summary>
        public void Send(Byte[] aData)
        {
            if (mIsAlive)
            {
                try { mSocket.BeginSend(aData, 0, aData.Length, SocketFlags.None, new AsyncCallback(EndSend), null); }
                catch (SocketException) { mServer.InvokeDisconnect(this); }
            }
        }

        /// <summary>
        /// Called by the asynchronous callback when the data is sent.
        /// </summary>
        private void EndSend(IAsyncResult res)
        {
            try { mSocket.EndSend(res); }
            catch { mServer.InvokeDisconnect(this); }
        }

        /// <summary>
        /// Begin the asynchronous operations to disconnect the socket from the remote endpoint.
        /// </summary>
        public void Disconnect()
        {
            if (mIsAlive)
            {
                mIsAlive = false;
                mSocket.BeginDisconnect(false, new AsyncCallback(EndDisconnect), null);
            }
        }

        /// <summary>
        /// Called by the asynchronous callback when the connection is closed.
        /// </summary>
        private void EndDisconnect(IAsyncResult res)
        {
            try { mSocket.EndDisconnect(res); mServer.InvokeDisconnect(this); }
            catch { mServer.InvokeDisconnect(this); }
        }
    }
}