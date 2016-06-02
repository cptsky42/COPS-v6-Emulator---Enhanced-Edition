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
        /// Indicate whether or not the IP2Location module is enabled.
        /// </summary>
        public static bool IP2LOCATION_ENABLED = false;

        /// <summary>
        /// Get the country code of the specified IPv4 address.
        /// </summary>
        /// <param name="aIPAddress">The IPv4 address</param>
        /// <returns>The country code of the address</returns>
        public static String GetCountryCode(String aIPAddress)
        {
            if (!IP2LOCATION_ENABLED)
                return "-";

            String[] parts = aIPAddress.Split('.');

            UInt32 ip = (Byte.Parse(parts[0]) * 0xFF000000U) +
                (Byte.Parse(parts[1]) * 0x00FF0000U) +
                (Byte.Parse(parts[2]) * 0x0000FF00U) +
                (Byte.Parse(parts[3]));

            using (var connection = sIP2LocPool.GetConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT `country_code` FROM `ip2location_db1` WHERE `ip_from` <= @ip AND `ip_to` >= @ip";
                    command.Parameters.AddWithValue("@ip", ip);
                    command.Prepare();

                    sLogger.Debug("Executing SQL: {0}", GetSqlCommand(command));

                    try
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                                return reader.GetString("country_code");
                        }
                    }
                    catch (MySqlException exc)
                    {
                        sLogger.Error("Failed to execute the following cmd : \"{0}\"\nError {1}: {2}",
                            GetSqlCommand(command), exc.Number, exc.Message);
                    }
                }
            }

            return "-";
        }

        /// <summary>
        /// Get the country name of the specified IPv4 address.
        /// </summary>
        /// <param name="aIPAddress">The IPv4 address</param>
        /// <returns>The country name of the address</returns>
        public static String GetCountryName(String aIPAddress)
        {
            if (!IP2LOCATION_ENABLED)
                return "-";

            String[] parts = aIPAddress.Split('.');

            UInt32 ip = (Byte.Parse(parts[0]) * 0xFF000000U) +
                (Byte.Parse(parts[1]) * 0x00FF0000U) +
                (Byte.Parse(parts[2]) * 0x0000FF00U) +
                (Byte.Parse(parts[3]));

            using (var connection = sIP2LocPool.GetConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT `country_name` FROM `ip2location_db1` WHERE `ip_from` <= @ip AND `ip_to` >= @ip";
                    command.Parameters.AddWithValue("@ip", ip);
                    command.Prepare();

                    sLogger.Debug("Executing SQL: {0}", GetSqlCommand(command));

                    try
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                                return reader.GetString("country_name");
                        }
                    }
                    catch (MySqlException exc)
                    {
                        sLogger.Error("Failed to execute the following cmd : \"{0}\"\nError {1}: {2}",
                            GetSqlCommand(command), exc.Number, exc.Message);
                    }
                }
            }

            return "-";
        }
    }
}
