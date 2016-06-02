// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using MySql.Data.MySqlClient;

namespace COServer
{
    public static partial class Database
    {
        /// <summary>
        /// The logger of the class.
        /// </summary>
        private static readonly log4net.ILog sLogger = log4net.LogManager.GetLogger(typeof(Database));

        /// <summary>
        /// The default MySQL pool.
        /// </summary>
        private static MySqlConnectionPool sDefaultPool = null;

        /// <summary>
        /// The IP2Location MySQL pool.
        /// </summary>
        private static MySqlConnectionPool sIP2LocPool = null;

        /// <summary>
        /// Setup the default MySQL connections pool.
        /// </summary>
        /// <param name="aCount">The number of connections in the pool.</param>
        /// <param name="aHost">The hostname of the MySQL server</param>
        /// <param name="aDatabase">The database to connect to</param>
        /// <param name="aUsername">The username to use to connect to the database</param>
        /// <param name="aPassword">The password to use to connect to the database</param>
        /// <returns>True on success, false otherwise.</returns>
        public static bool SetupMySQL(int aCount, String aHost, String aDatabase, String aUsername, String aPassword)
        {
            sDefaultPool = MySqlConnectionPool.CreatePool(aCount, aHost, aDatabase, aUsername, aPassword);
            if (sDefaultPool != null)
                sDefaultPool.Name = "Default";

            return sDefaultPool != null;
        }

        /// <summary>
        /// Setup the IP2Location connections pool.
        /// </summary>
        /// <param name="aCount">The number of connections in the pool.</param>
        /// <param name="aHost">The hostname of the MySQL server</param>
        /// <param name="aDatabase">The database to connect to</param>
        /// <param name="aUsername">The username to use to connect to the database</param>
        /// <param name="aPassword">The password to use to connect to the database</param>
        /// <returns>True on success, false otherwise.</returns>
        public static bool SetupIP2Location(int aCount, String aHost, String aDatabase, String aUsername, String aPassword)
        {
            sIP2LocPool = MySqlConnectionPool.CreatePool(aCount, aHost, aDatabase, aUsername, aPassword);
            if (sIP2LocPool != null)
                sIP2LocPool.Name = "IP2Location";

            return sIP2LocPool != null;
        }

        /// <summary>
        /// Get the SQL command string for the specified prepared statement.
        /// </summary>
        /// <param name="aCommand">The command to convert to string</param>
        /// <returns>The SQL command string</returns>
        public static String GetSqlCommand(MySqlCommand aCommand)
        {
            String command = aCommand.CommandText;

            foreach (MySqlParameter param in aCommand.Parameters)
                command = command.Replace(param.ParameterName, "'" + param.Value.ToString() + "'");

            return command;
        }
    }
}
