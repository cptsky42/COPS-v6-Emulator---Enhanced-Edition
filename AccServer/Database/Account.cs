// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2010 - 2015 Jean-Philippe Boivin
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
        /// Authenticate an account/password pair.
        /// </summary>
        /// <param name="aAccount">The account to authenticate</param>
        /// <param name="aPassword">The password used with the account</param>
        /// <returns>True if the account/password exist, false otherwise.</returns>
        public static Boolean Authenticate(String aAccount, String aPassword)
        {
            bool authenticated = false;

            using (var connection = sDefaultPool.GetConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT COUNT(*) FROM `account` WHERE `name` = @name AND `password` = @password";
                    command.Parameters.AddWithValue("@name", aAccount);
                    command.Parameters.AddWithValue("@password", aPassword);
                    command.Prepare();

                    sLogger.Debug("Executing SQL: {0}", GetSqlCommand(command));

                    try
                    {
                        int count = Convert.ToInt32(command.ExecuteScalar());
                        authenticated = count == 1;

                        sLogger.Debug("Found {0} accounts for {1}.", count, aAccount);
                    }
                    catch (MySqlException exc)
                    {
                        sLogger.Error("Failed to execute the following cmd : \"{0}\"\nError {1}: {2}",
                            GetSqlCommand(command), exc.Number, exc.Message);
                    }
                }
            }

            return authenticated;
        }

        /// <summary>
        /// Get the information associed to an account.
        /// </summary>
        /// <param name="Client">The client object corresponding to the account.</param>
        /// <param name="Account">The account name.</param>
        /// <param name="Server">The server on which the character is.</param>
        /// <returns>True on success, false otherwise.</returns>
        public static Boolean GetAccInfo(Client aClient, String aAccount, String aServer)
        {
            bool success = false;

            using (var connection = sDefaultPool.GetConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT `id`, `name`, `banned` FROM `account` WHERE `name` = @name";
                    command.Parameters.AddWithValue("@name", aAccount);
                    command.Prepare();

                    sLogger.Debug("Executing SQL: {0}", GetSqlCommand(command));

                    try
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            int count = 0;

                            while (reader.Read())
                            {
                                ++count;

                                aClient.AccountID = reader.GetUInt32("id");
                                aClient.Account = aAccount;
                                aClient.IsBanned = reader.GetBoolean("banned");

                                sLogger.Debug("Account ID of {0} is {1}.", aAccount, aClient.AccountID);
                            }
                            success = count == 1;

                            if (count != 1)
                                sLogger.Error("The command should return only one result, not {0}.", count);
                        }
                    }
                    catch (MySqlException exc)
                    {
                        sLogger.Error("Failed to execute the following cmd : \"{0}\"\nError {1}: {2}",
                            GetSqlCommand(command), exc.Number, exc.Message);
                    }
                }
            }

            return success;
        }

        /// <summary>
        /// Generate the token and update the `token` field of the account.
        /// </summary>
        /// <param name="aToken">The generated token</param>
        /// <param name="aClient">The client</param>
        /// <returns>True on success, false otherwise.</returns>
        public static bool GenerateToken(out UInt32 aToken, Client aClient)
        {
            bool success = false;

            DateTime datetime = DateTime.UtcNow;
            Random rand = new Random(Environment.TickCount);
            UInt32 token = (UInt32)(
                (rand.Next(0x01, 0x3F) << 26) | // 6 bits
                ((datetime.Year - 2000) << 20) | // 6 bits
                (datetime.Month << 16) | // 4 bits
                (datetime.Day << 11) | // 5 bits
                (datetime.Hour << 6) | // 5 bits
                (datetime.Minute)); // 6 bits

            using (var connection = sDefaultPool.GetConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE `account` SET `token` = @token WHERE `id` = @id";
                    command.Parameters.AddWithValue("@id", aClient.AccountID);
                    command.Parameters.AddWithValue("@token", token);
                    command.Prepare();

                    sLogger.Debug("Executing SQL: {0}", GetSqlCommand(command));

                    try
                    {
                        int count = command.ExecuteNonQuery();
                        success = count == 1;
                    }
                    catch (MySqlException exc)
                    {
                        sLogger.Error("Failed to execute the following cmd : \"{0}\"\nError {1}: {2}",
                            GetSqlCommand(command), exc.Number, exc.Message);
                    }
                }
            }

            aToken = token;
            return success;
        }

        /// <summary>
        /// Update the `last_ip` field of the account.
        /// </summary>
        /// <param name="aClient">The client</param>
        /// <returns>True on success, false otherwise.</returns>
        public static bool UpdateLastIP(Client aClient)
        {
            bool success = false;

            using (var connection = sDefaultPool.GetConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE `account` SET `last_ip` = @last_ip WHERE `id` = @id";
                    command.Parameters.AddWithValue("@id", aClient.AccountID);
                    command.Parameters.AddWithValue("@last_ip", aClient.IPAddress);
                    command.Prepare();

                    sLogger.Debug("Executing SQL: {0}", GetSqlCommand(command));

                    try
                    {
                        int count = command.ExecuteNonQuery();
                        success = count == 1;
                    }
                    catch (MySqlException exc)
                    {
                        sLogger.Error("Failed to execute the following cmd : \"{0}\"\nError {1}: {2}",
                            GetSqlCommand(command), exc.Number, exc.Message);
                    }
                }
            }

            return success;
        }

        /// <summary>
        /// Create a new account using the specified account name and password.
        /// </summary>
        /// <param name="aAccount">The account name.</param>
        /// <param name="aPassword">The password.</param>
        /// <returns>True on success, false otherwise.</returns>
        public static Boolean Create(Client aClient, String aAccount, String aPassword)
        {
            if (String.IsNullOrWhiteSpace(aAccount))
                return false;

            bool created = false;

            using (var connection = sDefaultPool.GetConnection())
            {
                DateTime datetime = DateTime.Now.ToUniversalTime();
                uint creationtime = (uint)((datetime.Year * 10000) + (datetime.Month * 100) + datetime.Day);

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO `account` (`name`, `email`, `password`, `creationtime`, `last_ip`) VALUES (@name, @email, @password, @creationtime, @last_ip)";
                    command.Parameters.AddWithValue("@name", aAccount);
                    command.Parameters.AddWithValue("@email", "");
                    command.Parameters.AddWithValue("@password", aPassword);
                    command.Parameters.AddWithValue("@creationtime", creationtime);
                    command.Parameters.AddWithValue("@last_ip", aClient.IPAddress);
                    command.Prepare();

                    sLogger.Debug("Executing SQL: {0}", GetSqlCommand(command));

                    try
                    {
                        command.ExecuteNonQuery();
                        created = true;

                        sLogger.Debug("Created account {0} in database.", aAccount);
                    }
                    catch (MySqlException exc)
                    {
                        if (exc.Number != 1062) // duplicate key
                        {
                            sLogger.Error("Failed to execute the following cmd : \"{0}\"\nError {1}: {2}",
                                GetSqlCommand(command), exc.Number, exc.Message);
                        }
                    }
                }
            }

            return created;
        }
    }
}