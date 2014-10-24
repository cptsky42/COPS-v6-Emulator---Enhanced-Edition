using System;
using System.Net;
using System.Net.Sockets;

namespace ConquerServer_v1Acc.Network
{
    public class NetworkClient
    {
        protected Socket m_sock;

        private ServerSocket Serv;
        private Boolean Alive;
        public Object Owner;

        /// <summary>
        /// Create a new instance to handle the new client connection
        /// </summary>
        public NetworkClient(ServerSocket Server, Socket sock)
        {
            Alive = true;
            this.Serv = Server;
            m_sock = sock;
        }

        /// <summary>
        /// Gets or sets the NetworkClient socket instance
        /// </summary>
        public Socket Socket { get { return this.m_sock; } }
        public ServerSocket Server { get { return this.Serv; } }
        public Boolean IsAlive { get { return Alive; } }

        public String IpAddress
        {
            get
            {
                if (m_sock != null)
                    return (m_sock.RemoteEndPoint as IPEndPoint).Address.ToString();
                else
                    return null;
            }
        }

        public UInt16 Port
        {
            get
            {
                if (m_sock != null)
                    return (UInt16)(m_sock.RemoteEndPoint as IPEndPoint).Port;
                else
                    return 0;
            }
        }

        /// <summary>
        /// Method to send packets directly to client using the SendAsync
        /// </summary>
        /// <param name="bufMsg"></param>
        public void Send(byte[] bufMsg)
        {
            if (m_sock == null || !m_sock.Connected)
                return;
            if (bufMsg == null || bufMsg.Length == 0)
                return;

            try
            {
                SocketAsyncEventArgs eventArgs = new SocketAsyncEventArgs();
                eventArgs.Completed += new EventHandler<SocketAsyncEventArgs>(IO_Completed);
                eventArgs.SetBuffer(bufMsg, 0, bufMsg.Length);
                eventArgs.UserToken = this;

                m_sock.SendAsync(eventArgs);
            }
            catch (Exception)
            {
                Console.WriteLine("Error at sendpacket!");
            }
        }

        /// <summary>
        /// Method called to eventually handle incomming packets
        /// </summary>
        public void ProcessPacket(byte[] bufMsg)
        {
            if (Serv.OnReceive != null)
                Serv.OnReceive(this, bufMsg);
        }

        /// <summary>
        /// This method is the callback method associated with Socket.AcceptAsync send operation and is invoked
        /// when the operation is complete
        /// </summary>
        protected void IO_Completed(object sender, SocketAsyncEventArgs e)
        {
            if (e.LastOperation == SocketAsyncOperation.Send
                && e.SocketError == SocketError.Success)
            {
                e.Dispose();// dispose the asynceventargs
            }
        }

        /// <summary>
        /// Force the client disconnection
        /// </summary>
        public void Disconnect()
        {
            // client socket is already closed, just return to prevent exceptions.
            if (m_sock == null)
                return;

            Alive = false;

            if (Serv.OnDisconnect != null)
                Serv.OnDisconnect(this);

            // close the socket associated with the client
            try { m_sock.Shutdown(SocketShutdown.Send); }
            // throws if client process has already closed
            catch (Exception) { }
            m_sock.Close();
        }
    }
}
