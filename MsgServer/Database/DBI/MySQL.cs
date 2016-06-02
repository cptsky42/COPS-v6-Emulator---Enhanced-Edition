// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.Collections.Concurrent;
using System.Data.Common;
using System.Threading;
using MySql.Data.MySqlClient;

namespace COServer
{
    /// <summary>
    /// MySQL Database Interface.
    /// 
    /// Wrapper over the official API to enable connections pooling.
    /// </summary>
    public class MySqlConnectionPool : IDisposable
    {
        /// <summary>
        /// MySQL connection.
        /// 
        /// Wrapper over the official API to enable automatic release
        /// of the connection.
        /// </summary>
        public class Connection : IDisposable
        {
            /// <summary>
            /// The pool owning the connection.
            /// </summary>
            private MySqlConnectionPool mPool;
            /// <summary>
            /// The connection.
            /// </summary>
            private MySqlConnection mConnection; // fucking MySQL dev's have sealed their classes

            /// <summary>
            /// Indicate whether or not the instance has been disposed.
            /// </summary>
            private bool mIsDisposed = false;

            /// <summary>
            /// Create a new wrapper around a MySqlConnection for the specified pool.
            /// </summary>
            /// <param name="aPool">The pool owning the connection.</param>
            /// <param name="aConnection">The internal connection.</param>
            internal Connection(MySqlConnectionPool aPool, MySqlConnection aConnection)
            {
                mPool = aPool;
                mConnection = aConnection;
            }

            ~Connection()
            {
                Dispose(false);
            }

            /// <summary>
            /// Dispose the connection (it will be returned to the pool).
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
                        if (mConnection != null)
                            mPool.ReleaseConnection(mConnection);
                    }

                    mConnection = null;

                    mIsDisposed = true;
                }
            }

            public MySqlCommand CreateCommand() { return mConnection.CreateCommand(); }
        }

        /// <summary>
        /// The logger of the class.
        /// </summary>
        private static readonly log4net.ILog sLogger = log4net.LogManager.GetLogger(typeof(MySqlConnectionPool));
        /// <summary>
        /// Last Id used by an instance.
        /// </summary>
        private static ulong sId = 1;

        /// <summary>
        /// The name of the pool.
        /// </summary>
        public String Name
        {
            get { return mName; }
            set
            {
                sLogger.Info("{0} is {1}.", mName, value);
                mName = value;
            }
        }

        /// <summary>
        /// The name of the pool.
        /// </summary>
        private String mName = String.Format("MYSQL_POOL_{0}", sId++);

        /// <summary>
        /// The semaphore for the connections pool.
        /// </summary>
        private Semaphore mPoolSem = null;
        /// <summary>
        /// The connections pool.
        /// </summary>
        private ConcurrentQueue<MySqlConnection> mConnections = null;

        /// <summary>
        /// The MySQL host.
        /// </summary>
        private String mHost = null;
        /// <summary>
        /// The MySQL connection string.
        /// </summary>
        private String mConnectionString = null;

        /// <summary>
        /// Indicate whether or not the instance has been disposed.
        /// </summary>
        private bool mIsDisposed = false;

        /// <summary>
        /// Create a new MySQL connections pool.
        /// </summary>
        /// <param name="aCount">The number of connections in the pool.</param>
        /// <param name="aHost">The hostname of the MySQL server</param>
        /// <param name="aDatabase">The database to connect to</param>
        /// <param name="aUsername">The username to use to connect to the database</param>
        /// <param name="aPassword">The password to use to connect to the database</param>
        /// <returns>A MySQL connections pool on success, null otherwise.</returns>
        public static MySqlConnectionPool CreatePool(int aCount, String aHost, String aDatabase, String aUsername, String aPassword)
        {
            MySqlConnectionPool pool = new MySqlConnectionPool();
            if (pool.Setup(aCount, aHost, aDatabase, aUsername, aPassword))
                return pool;

            pool.Dispose();
            return null;
        }

        private MySqlConnectionPool()
        {

        }

        ~MySqlConnectionPool()
        {
            Dispose(false);
        }

        /// <summary>
        /// Dispose the pool. All connections will be disposed.
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
                    while (mConnections.Count != 0)
                    {
                        MySqlConnection connection;
                        if (mConnections.TryDequeue(out connection))
                            connection.Dispose();
                    }

                    if (mPoolSem != null)
                        mPoolSem.Dispose();
                }

                mPoolSem = null;

                mIsDisposed = true;
            }
        }

        /// <summary>
        /// Setup the MySQL connections pool.
        /// </summary>
        /// <param name="aCount">The number of connections in the pool.</param>
        /// <param name="aHost">The hostname of the MySQL server</param>
        /// <param name="aDatabase">The database to connect to</param>
        /// <param name="aUsername">The username to use to connect to the database</param>
        /// <param name="aPassword">The password to use to connect to the database</param>
        /// <returns>True on success, false otherwise.</returns>
        private bool Setup(int aCount, String aHost, String aDatabase, String aUsername, String aPassword)
        {
            mPoolSem = new Semaphore(aCount, aCount, mName + "_SEM");
            mConnections = new ConcurrentQueue<MySqlConnection>();

            String host = aHost;
            UInt16 port = 3306;

            String[] parts = aHost.Split(':');
            if (parts.Length == 2)
            {
                host = parts[0];
                port = UInt16.Parse(parts[1]);
            }

            // see https://www.connectionstrings.com/mysql/
            mHost = aHost;
            mConnectionString = String.Format("SERVER='{0}';PORT={1};DATABASE='{2}';UID='{3}';PWD='{4}';",
                host, port, aDatabase, aUsername, aPassword);
            sLogger.Debug("[{0}] Connection string : {1}", mName, mConnectionString);

            for (int i = 0; i < aCount; ++i)
            {
                bool success = false;
                int count = 0;

                sLogger.Debug("[{0}] Connection n° {1}... Trying to connect to {2}...", mName, i, mHost);
                while (!success && count < 3)
                {
                    try
                    {
                        MySqlConnection connection = new MySqlConnection(mConnectionString);
                        connection.Open();

                        mConnections.Enqueue(connection);
                        success = true;
                    }
                    catch (MySqlException exc)
                    {
                        sLogger.Warn("[{0}] Connection n° {1}... Error {2}: {3}. Trying again...",
                            mName, i, exc.Number, exc.Message);
                        ++count;
                    }
                }

                if (!success)
                    return false;
            }

            return true;
        }

        private class FailedPingException : DbException
        {
            public FailedPingException() : base() { }
        }

        /// <summary>
        /// Get a connection from the connections pool.
        /// This function will block if no connection is available.
        /// </summary>
        /// <returns>A connection</returns>
        public Connection GetConnection()
        {
            mPoolSem.WaitOne();

            MySqlConnection connection = null;

            if (!mConnections.TryDequeue(out connection))
                return null;

            try
            {
                // the connection might be closed now, try to ping it...
                if (!connection.Ping())
                    throw new FailedPingException();
            }
            catch (DbException) // MySqlException is a DbException too
            {
                bool success = false;
                int count = 0;

                sLogger.Debug("[{0}] Trying to reconnect to {1}...", mName, mHost);
                while (!success && count < 3) // connection is closed, it must be reopened
                {
                    try
                    {
                        connection = new MySqlConnection(mConnectionString);
                        connection.Open();

                        success = true;
                    }
                    catch (MySqlException exc)
                    {
                        sLogger.Warn("[{0}] Error {1}: {2}. Trying again...",
                            mName, exc.Number, exc.Message);
                        ++count;
                    }
                }

                if (!success)
                {
                    sLogger.Fatal("[{0}] Failed to reconnect to {1}...", mName, mHost);
                    connection = null;
                }
            }

            return new Connection(this, connection);
        }

        /// <summary>
        /// Release the connection. It will be put back in the connections pool.
        /// </summary>
        /// <param name="aConnection">The connection to release</param>
        internal void ReleaseConnection(MySqlConnection aConnection)
        {
            mConnections.Enqueue(aConnection);
            mPoolSem.Release();
        }
    }
}
