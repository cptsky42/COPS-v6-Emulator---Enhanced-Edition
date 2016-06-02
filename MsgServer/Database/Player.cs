// * Created by Jean-Philippe Boivin
// * Copyright © 2010-2011, 2015
// * COPS v6 Emulator

using System;

using COServer.Entities;
using COServer.Network;

using MySql.Data.MySqlClient;

namespace COServer
{
    public partial class Database
    {
        /// <summary>
        /// Ban the specified player.
        /// </summary>
        /// <param name="aName">The name of the player to ban.</param>
        /// <returns>True on success, false otherwise.</returns>
        public static Boolean Ban(String aName)
        {
            bool success = false;

            using (var connection = sDefaultPool.GetConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE `account` SET `banned` = @banned WHERE `id` = (SELECT `account_id` FROM `user` WHERE `name` = @name)";
                    command.Parameters.AddWithValue("@name", aName);
                    command.Parameters.AddWithValue("@banned", true);
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
        /// Send the specified player to jail.
        /// </summary>
        /// <param name="aName">The name of the player to send to jail.</param>
        /// <returns>True on success, false otherwise.</returns>
        public static Boolean SendPlayerToJail(String aName)
        {
            bool success = false;

            GameMap map = null;
            if (!MapManager.TryGetMap(MapManager.PRISON_MAP_UID, out map))
                return false;

            using (var connection = sDefaultPool.GetConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = (
                        "UPDATE `user` SET `record_map` = @record_map, `record_x` = @record_x, `record_y` = @record_y, " + 
                        "`jail` = `jail` + 1 WHERE `name` = @name");
                    command.Parameters.AddWithValue("@name", aName);
                    command.Parameters.AddWithValue("@record_map", map.Id);
                    command.Parameters.AddWithValue("@record_x", map.PortalX);
                    command.Parameters.AddWithValue("@record_y", map.PortalY);
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

            if (success)
            {
                Player player = null;
                if (World.AllPlayerNames.TryGetValue(aName, out player))
                {
                    player.Move(map.Id, map.PortalX, map.PortalY);
                    ++player.JailC;
                }

                World.BroadcastMsg(new MsgTalk("SYSTEM", "ALLUSERS", aName + " has been sent to jail!", Channel.GM, Color.White));
            }

            return success;
        }

        /// <summary>
        /// Kick-out the specified player from its guild.
        /// </summary>
        /// <param name="aName">The name of the player to kick out.</param>
        /// <returns>True on success, false otherwise.</returns>
        public static Boolean LeaveSyn(String aName)
        {
            bool success = false;

            using (var connection = sDefaultPool.GetConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM `synattr` WHERE `id` = (SELECT `id` FROM `user` WHERE `name` = @name)";
                    command.Parameters.AddWithValue("@name", aName);
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
        /// Break a friendship.
        /// </summary>
        /// <param name="aPlayer">The player breaking the friendship.</param>
        /// <param name="aFriendName">The name of the friend.</param>
        /// <returns>True on success, false otherwise.</returns>
        public static Boolean BreakFriendship(Player aPlayer, String aFriendName)
        {
            bool success = false;

            using (var connection = sDefaultPool.GetConnection())
            {
                String friends = "";

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT `friends` FROM `user` WHERE `name` = @name";
                    command.Parameters.AddWithValue("@name", aFriendName);
                    command.Prepare();

                    sLogger.Debug("Executing SQL: {0}", GetSqlCommand(command));

                    using (var reader = command.ExecuteReader())
                    {
                        int count = 0;
                        if (reader.Read())
                        {
                            ++count;
                            friends = reader.GetString("friends");
                        }
                    }
                }

                String[] parts = friends.Split(',');
                friends = "";

                foreach (String friend in parts)
                {
                    String[] info = friend.Split(':');
                    if (info.Length < 2)
                        continue;

                    Int32 friendId = 0;
                    if (!Int32.TryParse(info[0], out friendId))
                        continue;

                    if (friendId == aPlayer.UniqId)
                        continue;

                    friends += friend + ",";
                }

                if (friends.Length > 0)
                    friends = friends.Substring(0, friends.Length - 1);

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE `user` SET `friends` = @friends WHERE `name` = @name";
                    command.Parameters.AddWithValue("@name", aFriendName);
                    command.Parameters.AddWithValue("@friends", friends);
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
        /// Divorce the specified player.
        /// </summary>
        /// <param name="aName">The player to divorce.</param>
        /// <returns>True on success, false otherwise.</returns>
        public static Boolean Divorce(String aName)
        {
            bool success = false;

            using (var connection = sDefaultPool.GetConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE `user` SET `mate` = @mate WHERE `name` = @name";
                    command.Parameters.AddWithValue("@name", aName);
                    command.Parameters.AddWithValue("@mate", "Non"); // TODO constant for "Non"
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
        /// Try to retreive the player information for the specified client.
        /// </summary>
        /// <param name="aClient">The client</param>
        /// <returns>True on success, false otherwise.</returns>
        public static Boolean GetPlayerInfo(ref Client aClient)
        {
            bool success = false;

            using (var connection = sDefaultPool.GetConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM `user` WHERE `account_id` = @account_id";
                    command.Parameters.AddWithValue("@account_id", aClient.AccountID);
                    command.Prepare();

                    sLogger.Debug("Executing SQL: {0}", GetSqlCommand(command));

                    using (var reader = command.ExecuteReader())
                    {
                        int count = 0;
                        while (reader.Read())
                        {
                            ++count;

                            Int32 uid = reader.GetInt32("id");
                            Player player = new Player(uid, aClient);

                            player.Name = reader.GetString("name");
                            player.Mate = reader.GetString("mate");
                            player.Look = reader.GetUInt32("lookface");
                            player.Hair = reader.GetUInt16("hair");

                            player.Money = reader.GetUInt32("money");
                            player.WHMoney = reader.GetUInt32("money_saved");

                            player.Profession = reader.GetByte("profession");
                            player.Level = reader.GetByte("level");
                            player.Exp = reader.GetUInt32("exp");
                            player.Metempsychosis = reader.GetByte("metempsychosis");

                            player.FirstProfession = reader.GetByte("first_profession");
                            player.FirstLevel = reader.GetByte("first_level");
                            player.SecondProfession = reader.GetByte("second_profession");
                            player.SecondLevel = reader.GetByte("second_level");

                            player.Strength = reader.GetUInt16("force");
                            player.Agility = reader.GetUInt16("dexterity");
                            player.Vitality = reader.GetUInt16("health");
                            player.Spirit = reader.GetUInt16("soul");
                            player.AddPoints = reader.GetUInt16("add_points");

                            player.CurHP = reader.GetUInt16("life");
                            player.CurMP = reader.GetUInt16("mana");

                            player.PkPoints = reader.GetInt16("pk");
                            player.VPs = reader.GetInt32("virtue");

                            UInt32 mapId = reader.GetUInt32("record_map");
                            if (MapManager.TryGetMap(mapId, out player.Map))
                            {
                                player.X = reader.GetUInt16("record_x");
                                player.Y = reader.GetUInt16("record_y");
                            }
                            else
                            {
                                // map not found...
                                if (MapManager.TryGetMap(1002, out player.Map))
                                {
                                    player.X = 400;
                                    player.Y = 400;
                                }
                            }

                            player.PrevMap = player.Map;
                            player.PrevX = player.X;
                            player.PrevY = player.Y;

                            player.mLockPin = reader.GetUInt32("depot_pin");

                            // TODO KO in superman table
                            // Player.KO = AMSXml.GetValue("Informations", "KO", 0);

                            player.TimeAdd = reader.GetInt32("time_add");

                            player.DblExpEndTime = Environment.TickCount + (reader.GetInt32("dbl_exp_time") * 1000);
                            player.LuckyTime = reader.GetInt32("lucky_time");

                            player.JailC = reader.GetByte("jail");

                            #region Friends / Enemies
                            String[] parts = null;

                            String friends = reader.GetString("friends");
                            parts = friends.Split(',');
                            foreach (String friend in parts)
                            {
                                String[] info = friend.Split(':');
                                if (info.Length < 2)
                                    continue;

                                Int32 friendId = 0;
                                if (!Int32.TryParse(info[0], out friendId))
                                    continue;

                                if (!player.Friends.ContainsKey(friendId))
                                    player.Friends.Add(friendId, info[1]);
                            }

                            String enemies = reader.GetString("enemies");
                            parts = enemies.Split(',');
                            foreach (String enemy in parts)
                            {
                                String[] info = enemy.Split(':');
                                if (info.Length < 2)
                                    continue;

                                Int32 enemyId = 0;
                                if (!Int32.TryParse(info[0], out enemyId))
                                    continue;

                                if (!player.Enemies.ContainsKey(enemyId))
                                    player.Enemies.Add(enemyId, info[1]);
                            }
                            #endregion

                            //MyMath
                            player.CalcMaxHP();
                            player.CalcMaxMP();

                            Database.GetPlayerSyndicate(player);
                            Database.GetPlayerWeaponSkills(player);
                            Database.GetPlayerMagics(player);

                            aClient.Player = player;
                        }

                        success = count == 1;
                    }
                }
            }

            return success;
        }

        public static Boolean Delete(Player aPlayer)
        {
            bool success = false;

            try
            {
                var skills = aPlayer.WeaponSkills;
                foreach (var skill in skills)
                    aPlayer.DropSkill(skill, true);

                var magics = aPlayer.Magics;
                foreach (var magic in magics)
                    aPlayer.DropMagic(magic, true);

                Item[] items = new Item[aPlayer.Items.Count];
                aPlayer.Items.Values.CopyTo(items, 0);
                for (int i = 0; i < items.Length; i++)
                    aPlayer.DelItem(items[i], true);

                // TODO Friends

                if (aPlayer.Syndicate != null)
                    aPlayer.Syndicate.LeaveSyn(aPlayer, false, false);

                String account = aPlayer.Client.Account;
                String name = aPlayer.Name;
                String ip = aPlayer.Client.IPAddress;

                aPlayer.Disconnect();

                using (var connection = sDefaultPool.GetConnection())
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "DELETE FROM `user` WHERE `id` = @id";
                        command.Parameters.AddWithValue("@id", aPlayer.UniqId);
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

                Program.Log("Deletion of " + name + ", with " + account + " (" + ip + ").");
                return success;
            }
            catch (Exception exc) { sLogger.Error(exc); return false; }
        }

        public static Boolean Save(Player aPlayer, bool aIsOnline)
        {
            bool success = false;

            // TODO superman table
            //AMSXml.SetValue("Informations", "KO", 0);

            String friends = "";
            foreach (var friend in aPlayer.Friends)
                friends += friend.Key.ToString() + ":" + friend.Value + ",";
            if (friends.Length > 0)
                friends = friends.Substring(0, friends.Length - 1);

            String enemies = "";
            foreach (var enemy in aPlayer.Enemies)
                enemies += enemy.Key.ToString() + ":" + enemy.Value + ",";
            if (enemies.Length > 0)
                enemies = enemies.Substring(0, enemies.Length - 1);

            using (var connection = sDefaultPool.GetConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = (
                        "UPDATE `user` SET " +
                        "`money` = @money, `money_saved` = @money_saved,  " +
                        "`profession` = @profession, `level` = @level, `exp` = @exp, " +
                        "`first_profession` = @first_profession, `first_level` = @first_level, `second_profession` = @second_profession, `second_level` = @second_level, " +
                        "`force` = @force, `dexterity` = @dexterity, `health` = @health, `soul` = @soul, `add_points` = @add_points, " +
                        "`life` = @life, `mana` = @mana, `pk` = @pk, `virtue` = @virtue, " +
                        "`time_add` = @time_add, " +
                        "`dbl_exp_time` = @dbl_exp_time, `lucky_time` = @lucky_time, " +
                        "`record_map` = @record_map, `record_x` = @record_x, `record_y` = @record_y, `friends` = @friends, `enemies` = @enemies, `online` = @online WHERE `id` = @id");

                    command.Parameters.AddWithValue("@id", aPlayer.UniqId);

                    command.Parameters.AddWithValue("@money", aPlayer.Money);
                    command.Parameters.AddWithValue("@money_saved", aPlayer.WHMoney);

                    command.Parameters.AddWithValue("@profession", aPlayer.Profession);
                    command.Parameters.AddWithValue("@level", aPlayer.Level);
                    command.Parameters.AddWithValue("@exp", aPlayer.Exp);

                    command.Parameters.AddWithValue("@first_profession", aPlayer.FirstProfession);
                    command.Parameters.AddWithValue("@first_level", aPlayer.FirstLevel);
                    command.Parameters.AddWithValue("@second_profession", aPlayer.SecondProfession);
                    command.Parameters.AddWithValue("@second_level", aPlayer.SecondLevel);

                    command.Parameters.AddWithValue("@force", aPlayer.Strength);
                    command.Parameters.AddWithValue("@dexterity", aPlayer.Agility);
                    command.Parameters.AddWithValue("@health", aPlayer.Vitality);
                    command.Parameters.AddWithValue("@soul", aPlayer.Spirit);
                    command.Parameters.AddWithValue("@add_points", aPlayer.AddPoints);

                    command.Parameters.AddWithValue("@life", aPlayer.CurHP > 0 ? aPlayer.CurHP : 1);
                    command.Parameters.AddWithValue("@mana", aPlayer.CurMP);

                    command.Parameters.AddWithValue("@pk", aPlayer.PkPoints);
                    command.Parameters.AddWithValue("@virtue", aPlayer.VPs);

                    if (aPlayer.CurHP > 0)
                    {
                        if (!aPlayer.Map.IsRecord_Disable())
                        {
                            command.Parameters.AddWithValue("@record_map", aPlayer.Map.Id);
                            command.Parameters.AddWithValue("@record_x", aPlayer.X);
                            command.Parameters.AddWithValue("@record_y", aPlayer.Y);
                        }
                        else
                        {
                            GameMap rebornMap = null;
                            if (MapManager.TryGetMap(aPlayer.Map.RebornMap, out rebornMap))
                            {
                                command.Parameters.AddWithValue("@record_map", rebornMap.Id);
                                command.Parameters.AddWithValue("@record_x", rebornMap.PortalX);
                                command.Parameters.AddWithValue("@record_y", rebornMap.PortalY);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@record_map", 1002);
                                command.Parameters.AddWithValue("@record_x", 400);
                                command.Parameters.AddWithValue("@record_y", 400);
                            }
                        }
                    }
                    else
                    {
                        GameMap rebornMap;
                        if (MapManager.TryGetMap(aPlayer.Map.RebornMap, out rebornMap))
                        {
                            command.Parameters.AddWithValue("@record_map", rebornMap.Id);
                            command.Parameters.AddWithValue("@record_x", rebornMap.PortalX);
                            command.Parameters.AddWithValue("@record_y", rebornMap.PortalY);
                        }
                    }

                    command.Parameters.AddWithValue("@time_add", aPlayer.TimeAdd);

                    if (aPlayer.DblExpEndTime == 0)
                        command.Parameters.AddWithValue("@dbl_exp_time", 0);
                    else
                        command.Parameters.AddWithValue("@dbl_exp_time", (Int32)((aPlayer.DblExpEndTime - Environment.TickCount) / 1000));
                    command.Parameters.AddWithValue("@lucky_time", aPlayer.LuckyTime);

                    command.Parameters.AddWithValue("@friends", friends);
                    command.Parameters.AddWithValue("@enemies", enemies);
                    command.Parameters.AddWithValue("@online", aIsOnline);
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
        /// Update the specified field of the player table.
        /// </summary>
        /// <param name="aPlayer">The player to update.</param>
        /// <param name="aField">The field to update.</param>
        /// <param name="aValue">The new value.</param>
        /// <returns>True on success, false otherwise.</returns>
        public static Boolean UpdateField(Player aPlayer, String aField, object aValue)
        {
            bool success = false;

            using (var connection = sDefaultPool.GetConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = String.Format("UPDATE `user` SET `{0}` = @field WHERE `id` = @id", aField);
                    command.Parameters.AddWithValue("@id", aPlayer.UniqId);
                    command.Parameters.AddWithValue("@field", aValue);
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
        /// Create a new player with the specified name and profession.
        /// </summary>
        /// <param name="aClient">The client.</param>
        /// <param name="aName">The player name.</param>
        /// <param name="aLook">The player look.</param>
        /// <param name="aProfession">The player profession.</param>
        /// <returns>True on success, false otherwise.</returns>
        public static Boolean CreatePlayer(Client aClient, String aName, Int32 aLook, Byte aProfession)
        {
            UInt16[] points = MyMath.GetLevelStats(1, aProfession);
            if (points == null)
                return false;

            bool created = false;

            DateTime datetime = DateTime.Now.ToUniversalTime();
            uint creationtime = (uint)((datetime.Year * 10000) + (datetime.Month * 100) + datetime.Day);

            Int32 uid = World.LastPlayerUID++;
            UInt16 life = (UInt16)((points[0] * 3) + (points[1] * 3) + (points[2] * 24) + (points[3] * 3));
            UInt16 mana = (UInt16)(points[3] * 5);

            using (var connection = sDefaultPool.GetConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = (
                        "INSERT INTO `user` (`account_id`, `id`, `name`, `lookface`, `profession`, " +
                        "`force`, `dexterity`, `health`, `soul`, `life`, `mana`, `creationtime`, `last_login`) " + 
                        "VALUES (@account_id, @id, @name, @lookface, @profession, @force, @dexterity, @health, @soul, " + 
                        "@life, @mana, @creationtime, @last_login)");
                    command.Parameters.AddWithValue("@account_id", aClient.AccountID);
                    command.Parameters.AddWithValue("@id", uid);
                    command.Parameters.AddWithValue("@name", aName);
                    command.Parameters.AddWithValue("@lookface", aLook);
                    command.Parameters.AddWithValue("@profession", aProfession);
                    command.Parameters.AddWithValue("@force", points[0]);
                    command.Parameters.AddWithValue("@dexterity", points[1]);
                    command.Parameters.AddWithValue("@health", points[2]);
                    command.Parameters.AddWithValue("@soul", points[3]);
                    command.Parameters.AddWithValue("@life", life);
                    command.Parameters.AddWithValue("@mana", mana);
                    command.Parameters.AddWithValue("@creationtime", creationtime);
                    command.Parameters.AddWithValue("@last_login", creationtime);
                    command.Prepare();

                    sLogger.Debug("Executing SQL: {0}", GetSqlCommand(command));

                    try
                    {
                        command.ExecuteNonQuery();
                        created = true;

                        sLogger.Debug("Created player {0} in database.", aName);
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

            if (created)
            {
                Item.Create(uid, 0, 726100, 0, 0, 0, 0, 0, 3, 0, 1, 1);
            }

            return created;
        }

        /// <summary>
        /// Get the last available player's ID.
        /// </summary>
        public static Int32 GetLastPlayerUID()
        {
            Int32 lastUID = Entity.PLAYERID_FIRST;

            using (var connection = sDefaultPool.GetConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT `id` FROM `user`";
                    command.Prepare();

                    sLogger.Debug("Executing SQL: {0}", GetSqlCommand(command));

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Int32 uid = reader.GetInt32("id");

                            if (uid > lastUID)
                                lastUID = uid;
                        }
                    }
                }
            }

            return ++lastUID;
        }
    }
}
