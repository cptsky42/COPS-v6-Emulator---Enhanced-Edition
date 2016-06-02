// * Created by Jean-Philippe Boivin
// * Copyright © 2010, 2014-2015
// * COPS v6 Emulator

using System;
using System.Net;
using System.Net.Sockets;

namespace COServer.Network.Sockets
{
    /// <summary>
    /// TCP/IP server.
    /// </summary>
    public class TcpServer : IDisposable
    {
        /// <summary>
        /// Internal socket.
        /// </summary>
        private Socket mSocket;
        /// <summary>
        /// The port on which the server is listening.
        /// </summary>
        private UInt16 mPort;

        /// <summary>
        /// Indicate whether or not the server is listening.
        /// </summary>
        private Boolean mIsListening;

        /// <summary>
        /// Indicate whether or not the instance has been disposed.
        /// </summary>
        private bool mIsDisposed = false;

        /// <summary>
        /// The function to call when a new client is connected.
        /// </summary>
        public NetworkClientConnection OnConnect = null;
        /// <summary>
        /// The function to call when the server receives data from a client.
        /// </summary>
        public NetworkClientReceive OnReceive = null;
        /// <summary>
        /// The function to call when a client is disconnected.
        /// </summary>
        public NetworkClientConnection OnDisconnect = null;

        /// <summary>
        /// The port on which the server is listening.
        /// </summary>
        public UInt16 Port { get { return mPort; } }

        /// <summary>
        /// Create a new TCP/IPv4 socket that will act as a server.
        /// </summary>
        public TcpServer()
        {
            mSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            mPort = 0;
            mIsListening = false;
        }

        ~TcpServer()
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
        /// Associate the socket to a local endpoint and place the socket in a listening state.
        /// </summary>
        public void Listen(UInt16 aPort, Int32 aBacklog)
        {
            if (mIsListening)
                throw new SocketException((int)SocketError.IsConnected);

            try
            {
                mSocket.Bind(new IPEndPoint(IPAddress.Any, aPort));
                mSocket.Listen(aBacklog);

                mPort = aPort;
                mIsListening = true;
            }
            catch (Exception exc) { throw exc; }
        }

        /// <summary>
        /// Begin the asynchronous operations to accept incoming connections.
        /// </summary>
        public void Accept()
        {
            mSocket.BeginAccept(new AsyncCallback(EndAccept), null);
        }

        /// <summary>
        /// Called by the asynchronous callback when there is a new incoming connection.
        /// </summary>
        private void EndAccept(IAsyncResult res)
        {
            const Int32 MAX_BUFFER_SIZE = 2048;

            Socket socket = null;

            try { socket = mSocket.EndAccept(res); }
            catch { Accept(); return; }

            socket.SendBufferSize = MAX_BUFFER_SIZE;
            socket.ReceiveBufferSize = MAX_BUFFER_SIZE;

            TcpSocket client = TcpSocket.Create(this, socket, MAX_BUFFER_SIZE);
            if (client == null)
            {
                try { socket.Close(); }
                catch { Accept(); return; }
            }

            if (OnConnect != null)
                OnConnect(client);

            Accept();
        }

        /// <summary>
        /// Force the disconnection of the specified client.
        /// </summary>
        public void InvokeDisconnect(TcpSocket aClient)
        {
            if (aClient == null || !aClient.IsAlive)
                return;

            aClient.Disconnect();
            if (OnDisconnect != null)
                OnDisconnect(aClient);
        }
    }
}