// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2011 - 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using COServer.Entities;
using MySql.Data.MySqlClient;

namespace COServer
{
    /// <summary>
    /// A weapon skill of a player.
    /// </summary>
    public class WeaponSkill
    {
        /// <summary>
        /// Unique ID of the weapon skill.
        /// </summary>
        private UInt32 mId = 0;
        /// <summary>
        /// Owner of the weapon skill.
        /// </summary>
        private Player mPlayer = null;
        /// <summary>
        /// Type of the weapon skill.
        /// </summary>
        private UInt16 mType = 0;
        /// <summary>
        /// Level of the weapon skill.
        /// </summary>
        private Byte mLevel = 0;
        /// <summary>
        /// Experience of the weapon skill.
        /// </summary>
        private UInt32 mExp = 0;
        /// <summary>
        /// Previous level of the weapon skill (before rebirth).
        /// </summary>
        private Byte mOldLevel = 0;
        /// <summary>
        /// Whether or not the weapon skill is unlearned.
        /// </summary>
        private Boolean mIsUnlearn = false;

        /// <summary>
        /// Unique ID of the weapon skill.
        /// </summary>
        public UInt32 Id
        { 
            get { return mId; }
        }

        /// <summary>
        /// Owner of the weapon skill.
        /// </summary>
        public Player Player
        { 
            get { return mPlayer; }
        }

        /// <summary>
        /// Type of the weapon skill.
        /// </summary>
        public UInt16 Type
        { 
            get { return mType; }
        }

        /// <summary>
        /// Level of the weapon skill.
        /// </summary>
        public Byte Level
        {
            get { return mLevel; }
            set
            { 
                mLevel = value;
                Database.UpdateField(this, "level", mLevel);
            }
        }

        /// <summary>
        /// Experience of the weapon skill.
        /// </summary>
        public UInt32 Exp
        { 
            get { return mExp; }
            set
            {
                mExp = value;
                Database.UpdateField(this, "exp", mExp);
            }
        }

        /// <summary>
        /// Previous level of the weapon skill (before rebirth).
        /// </summary>
        public Byte OldLevel
        { 
            get { return mOldLevel; }
            set 
            { 
                mOldLevel = value;
                Database.UpdateField(this, "old_level", mOldLevel);
            }
        }

        /// <summary>
        /// Whether or not the weapon skill is unlearned.
        /// </summary>
        public Boolean Unlearn
        { 
            get { return mIsUnlearn; }
            set
            { 
                mIsUnlearn = value;
                Database.UpdateField(this, "unlearn", mIsUnlearn);
            }
        }

        public WeaponSkill(UInt32 aId, Player aPlayer, UInt16 aType, Byte aLevel, UInt32 aExp, Byte aOldLevel, Boolean aIsUnlearn)
        {
            mId = aId;
            mPlayer = aPlayer;
            mType = aType;
            mLevel = aLevel;
            mExp = aExp;
            mOldLevel = aOldLevel;
            mIsUnlearn = aIsUnlearn;
        }

        /// <summary>
        /// Try to create a new weapon skill for the specified player.
        /// If created successfully, it must be awarded to the player.
        /// </summary>
        /// <param name="aPlayer">The player.</param>
        /// <param name="aType">The type of the weapon skill.</param>
        /// <param name="aLevel">The level of the weapon skill.</param>
        /// <returns>A weapon skill on success, null otherwise.</returns>
        public static WeaponSkill Create(Player aPlayer, UInt16 aType, Byte aLevel)
        {
            WeaponSkill skill = null;
            Database.CreateSkill(aPlayer, aType, aLevel, out skill);
            return skill;
        }
    }

    public partial class Database
    {
        /// <summary>
        /// Try to create a new weapon skill for the specified player.
        /// </summary>
        /// <param name="aPlayer">The player.</param>
        /// <param name="aType">The type of the weapon skill.</param>
        /// <param name="aLevel">The level of the weapon skill.</param>
        /// <param name="aOutSkill">The newly created weapon skill.</param>
        /// <returns>True on success, false otherwise.</returns>
        public static Boolean CreateSkill(Player aPlayer, UInt16 aType, Byte aLevel, out WeaponSkill aOutSkill)
        {
            bool created = false;
            aOutSkill = null;

            using (var connection = sDefaultPool.GetConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = (
                        "INSERT INTO `weaponskill` (`owner_id`, `type`, `level`, `exp`, `old_level`, `unlearn`) " +
                        "VALUES (@owner_id, @type, @level, @exp, @old_level, @unlearn)");
                    command.Parameters.AddWithValue("@owner_id", aPlayer.UniqId);
                    command.Parameters.AddWithValue("@type", aType);
                    command.Parameters.AddWithValue("@level", aLevel);
                    command.Parameters.AddWithValue("@exp", 0);
                    command.Parameters.AddWithValue("@old_level", 0);
                    command.Parameters.AddWithValue("@unlearn", false);
                    command.Prepare();

                    sLogger.Debug("Executing SQL: {0}", GetSqlCommand(command));

                    try
                    {
                        command.ExecuteNonQuery();
                        created = true;

                        UInt32 uid = (UInt32)command.LastInsertedId;
                        aOutSkill = new WeaponSkill(uid, aPlayer, aType, aLevel, 0, 0, false);

                        sLogger.Debug("Created skill {0} in database.", aOutSkill.Id);
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

        /// <summary>
        /// Try to retreive the player's weapon skills.
        /// </summary>
        /// <param name="aPlayer">The player.</param>
        /// <returns>True on success, false otherwise.</returns>
        public static Boolean GetPlayerWeaponSkills(Player aPlayer)
        {
            bool success = false;

            using (var connection = sDefaultPool.GetConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT `id`, `type`, `level`, `exp`, `old_level`, `unlearn` FROM `weaponskill` WHERE `owner_id` = @owner_id";
                    command.Parameters.AddWithValue("@owner_id", aPlayer.UniqId);
                    command.Prepare();

                    sLogger.Debug("Executing SQL: {0}", GetSqlCommand(command));

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            WeaponSkill skill = new WeaponSkill(
                                reader.GetUInt32("id"),
                                aPlayer,
                                reader.GetUInt16("type"),
                                reader.GetByte("level"),
                                reader.GetUInt32("exp"),
                                reader.GetByte("old_level"),
                                reader.GetBoolean("unlearn"));

                            aPlayer.AwardSkill(skill, false);
                        }
                    }
                }
            }

            return success;
        }

        /// <summary>
        /// Update the specified field of the weaponskill table.
        /// </summary>
        /// <param name="aPlayer">The skill to update.</param>
        /// <param name="aField">The field to update.</param>
        /// <param name="aValue">The new value.</param>
        /// <returns>True on success, false otherwise.</returns>
        public static Boolean UpdateField(WeaponSkill aSkill, String aField, object aValue)
        {
            bool success = false;

            using (var connection = sDefaultPool.GetConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = String.Format("UPDATE `weaponskill` SET `{0}` = @field WHERE `id` = @id", aField);
                    command.Parameters.AddWithValue("@id", aSkill.Id);
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
        /// Delete the specified weapon skill.
        /// </summary>
        /// <param name="aSkill">The weapon skill to delete.</param>
        /// <returns>True on success, false otherwise.</returns>
        public static Boolean Delete(WeaponSkill aSkill)
        {
            bool success = false;

            using (var connection = sDefaultPool.GetConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM `weaponskill` WHERE `id` = @id";
                    command.Parameters.AddWithValue("@id", aSkill.Id);
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
    }
}
