using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace ConquerServer_v1Acc.Network
{
    public class ServerSocket
    {
        const int PREALLOC_OPERATIONS = 2;// read, write (don't alloc buffer space for accepts)

        private int m_numConnections;// the maximum number of connections designed to handle simultaneously 
        private int m_receiveBufferSize;// buffer size to use for each socket I/O operation 
        private int m_numConnectedSockets;// the total number of clients connected to the server 
        private Socket m_sock;
        private Semaphore m_maxNumberAcceptedClients;

        public NetworkClientConnection OnConnect;
        public NetworkClientReceive OnReceive;
        public NetworkClientConnection OnDisconnect;

        /// <summary>
        /// Create an uninitialized server instance.  To start the server listening for connection requests
        /// call the Init method followed by Start method 
        /// </summary>
        /// <param name="numConnections">the maximum number of connections the sample is designed to handle simultaneously</param>
        /// <param name="receiveBufferSize">buffer size to use for each socket I/O operation</param>
        public ServerSocket()
        {

        }

        public void Listen(UInt16 BindPort, Int32 BackLog)
        {
            m_numConnectedSockets = 0;
            m_numConnections = BackLog;
            m_receiveBufferSize = 2048;

            m_maxNumberAcceptedClients = new Semaphore(BackLog, BackLog);

            // create the socket which listens for incoming connections
            m_sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            m_sock.Bind(new IPEndPoint(IPAddress.Any, BindPort));
            // start the server with a listen backlog of 100 connections
            m_sock.Listen(100);
        }

        /// <summary>
        /// Starts the server such that it is listening for incoming connection requests.    
        /// </summary>
        /// <param name="localEndPoint">The endpoint which the server will listening for conenction requests on</param>
        public void Accept()
        {
            // post accepts on the listening socket
            StartAccept(null);
        }

        /// <summary>
        /// Begins an operation to accept a connection request from the client 
        /// </summary>
        /// <param name="acceptEventArg">The context object to use when issuing the accept operation on the 
        /// server's listening socket</param>
        private void StartAccept(SocketAsyncEventArgs acceptEventArg)
        {
            if (acceptEventArg == null)
            {
                acceptEventArg = new SocketAsyncEventArgs();
                acceptEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(AcceptEventArg_Completed);
            }
            else
            {
                // socket must be cleared since the context object is being reused
                acceptEventArg.AcceptSocket = null;
            }

            m_maxNumberAcceptedClients.WaitOne();
            bool willRaiseEvent = m_sock.AcceptAsync(acceptEventArg);
            if (!willRaiseEvent)
            {
                ProcessAccept(acceptEventArg);
            }
        }

        /// <summary>
        /// This method is the callback method associated with Socket.AcceptAsync operations and is invoked
        /// when an accept operation is complete
        /// </summary>
        private void AcceptEventArg_Completed(object sender, SocketAsyncEventArgs e)
        {
            ProcessAccept(e);
        }

        private void ProcessAccept(SocketAsyncEventArgs e)
        {
            Socket newSock = e.AcceptSocket;

            if (newSock != null)
            {
                Interlocked.Increment(ref m_numConnectedSockets);
                Console.WriteLine("[Socket] There are currently {0} clients connected.", m_numConnectedSockets);
                NetworkClient pToken = new NetworkClient(this, newSock);

                if (OnConnect != null)
                    OnConnect(pToken);

                SocketAsyncEventArgs pEvent = new SocketAsyncEventArgs();
                pEvent.Completed += new EventHandler<SocketAsyncEventArgs>(IO_Completed);
                pEvent.SetBuffer(new byte[1024], 0, 1024);
                pEvent.UserToken = pToken;

                // As soon as the client is connected, post a receive to the connection
                bool willRaiseEvent = pToken.Socket.ReceiveAsync(pEvent);
                if (!willRaiseEvent)
                {
                    ProcessReceive(pEvent);
                }
            }

            // Accept the next connection request
            StartAccept(e);
        }

        /// <summary>
        /// This method is called whenever a receive or send opreation is completed on a socket 
        /// </summary> 
        /// <param name="e">SocketAsyncEventArg associated with the completed receive operation</param>
        private void IO_Completed(object sender, SocketAsyncEventArgs e)
        {
            // determine which type of operation just completed and call the associated handler
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Receive:
                    ProcessReceive(e);
                    break;
                default:
                    throw new ArgumentException("The last operation completed on the socket was not receive!");
            }
        }

        /// <summary>
        /// This method is invoked when an asycnhronous receive operation completes. If the 
        /// remote host closed the connection, then the socket is closed.  If data was received then
        /// the data is echoed back to the client.
        /// </summary>
        private void ProcessReceive(SocketAsyncEventArgs e)
        {
            // check if the remote host closed the connection
            NetworkClient pToken = (NetworkClient)e.UserToken;
            if (e.BytesTransferred > 0 && e.SocketError == SocketError.Success)
            {
                byte[] buffer = new byte[e.BytesTransferred];
                Buffer.BlockCopy(e.Buffer, e.Offset, buffer, 0, e.BytesTransferred);
                pToken.ProcessPacket(buffer);

                // read the next block of data send from the client
                bool willRaiseEvent = pToken.Socket.ReceiveAsync(e);
                if (!willRaiseEvent)
                {
                    ProcessReceive(e);
                }
            }
            else
            {
                CloseClientSocket(e);
            }
        }

        private void CloseClientSocket(SocketAsyncEventArgs e)
        {
            // Closes the socket associated with client
            NetworkClient pToken = e.UserToken as NetworkClient;
            if (pToken != null)
                pToken.Disconnect();

            // Release the socketasynceventargs
            e.Dispose();
            // Decrement the counter keeping track of the total number of clients connected to the server
            Interlocked.Decrement(ref m_numConnectedSockets);
            m_maxNumberAcceptedClients.Release();
            Console.WriteLine("[Socket] There are currently {0} clients connected.", m_numConnectedSockets);
        }
    }
}
