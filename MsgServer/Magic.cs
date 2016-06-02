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
    /// A magic skill of a player.
    /// </summary>
    public class Magic
    {
        /// <summary>
        /// The logger of the class.
        /// </summary>
        private static readonly log4net.ILog sLogger = log4net.LogManager.GetLogger(typeof(Database));

        public const Int32 MAX_NAMESIZE = 0x10;
        public const Int32 MAX_DESCSIZE = 0x40;
        public const Int32 MAX_DESCEXSIZE = 0x100;
        public const Int32 MAX_EFFECTSIZE = 0x40;
        public const Int32 MAX_SOUNDSIZE = 0x104;

        // Constants for magic's UseXP field.
        public const Byte TYPE_MAGIC = 0;
        public const Byte TYPE_XPSKILL = 1;
        public const Byte TYPE_KONGFU = 2;

        public class Info // Circular definition, must not be struct !
        {
            /// <summary>
            /// Type of the magic.
            /// </summary>
            public UInt16 Type;
            /// <summary>
            /// Sort of the magic.
            /// </summary>
            public MagicSort Sort;
            /// <summary>
            /// Whether or not using the magic is a crime.
            /// </summary>
            public Boolean Crime;
            /// <summary>
            /// Whether or not it is a ground magic.
            /// </summary>
            public Boolean Ground;
            /// <summary>
            /// Whether or not it can target multiple entities.
            /// </summary>
            public Boolean Multi;
            /// <summary>
            /// Bit mask for the target type of the magic.
            /// </summary>
            public UInt32 Target;
            /// <summary>
            /// Level of the magic.
            /// </summary>
            public UInt16 Level;
            /// <summary>
            /// Mana used by the magic.
            /// </summary>
            public UInt16 UseMP;
            /// <summary>
            /// Power of the magic / Percentage (if more than 30k).
            /// </summary>
            public UInt32 Power;
            /// <summary>
            /// Intone time in milliseconds.
            /// </summary>
            public UInt16 IntoneDuration;
            /// <summary>
            /// Success rate when using the magic.
            /// </summary>
            public Byte Success;
            /// <summary>
            /// Duration of the magic in seconds.
            /// </summary>
            public UInt32 StepSecs;
            /// <summary>
            /// ???
            /// </summary>
            public Byte Range; // x = %100, y= %10000 - x*100
            /// <summary>
            /// ???
            /// </summary>
            public Byte Distance;
            /// <summary>
            /// Status flag of the magic.
            /// </summary>
            public UInt32 Status;
            /// <summary>
            /// Required profession of the magic.
            /// </summary>
            public Byte ReqProf;
            /// <summary>
            /// Required exp for the next level of the magic.
            /// </summary>
            public UInt32 ReqExp;
            /// <summary>
            /// Required player level for the next level of the magic.
            /// </summary>
            public Byte ReqLevel;
            /// <summary>
            /// Type of the magic skill (e.g. XP skill).
            /// </summary>
            public Byte UseXP; // 0 -> Magic, 1 -> XP Skill, 2 -> Kongfu
            /// <summary>
            /// Required weapon subtype to use the magic.
            /// </summary>
            public UInt16 WeaponSubtype;
            /// <summary>
            /// ???
            /// </summary>
            public UInt32 ActiveTimes;
            /// <summary>
            /// ???
            /// </summary>
            public Boolean AutoActive;
            /// <summary>
            /// ???
            /// </summary>
            public UInt32 FloorAttr;
            /// <summary>
            /// Whether or not the magic is automatically learned.
            /// </summary>
            public Boolean AutoLearn;
            /// <summary>
            /// Required player level to automatically learn the magic.
            /// </summary>
            public Byte LearnLevel;
            /// <summary>
            /// Whether or not the right weapon is thrown when using the magic.
            /// </summary>
            public Boolean DropWeapon;
            /// <summary>
            /// Energy used by the magic.
            /// </summary>
            public Byte UseEP;
            /// <summary>
            /// ???
            /// </summary>
            public Boolean WeaponHit; // TQ's comment is: Hit by weapon...
            /// <summary>
            /// Use the equipped item.
            /// </summary>
            public Int32 UseItem;
            /// <summary>
            /// Next magic to use.
            /// </summary>
            public Magic.Info NextMagic; // launch this magic after current magic, use same target and pos
            /// <summary>
            /// Delay (in milliseconds) before the next magic.
            /// </summary>
            public UInt16 NextMagicDelay;
            /// <summary>
            /// Durability used by the magic.
            /// </summary>
            public Byte UseItemNum;
            /// <summary>
            /// Whether or not the magic is usable in the market.
            /// </summary>
            public Boolean UsableInMarket;
        }

        /// <summary>
        /// Unique ID of the magic skill.
        /// </summary>
        private UInt32 mId = 0;
        /// <summary>
        /// Owner of the magic skill.
        /// </summary>
        private Player mPlayer = null;
        /// <summary>
        /// Type of the magic skill.
        /// </summary>
        private UInt16 mType = 0;
        /// <summary>
        /// Level of the magic skill.
        /// </summary>
        private Byte mLevel = 0;
        /// <summary>
        /// Experience of the magic skill.
        /// </summary>
        private UInt32 mExp = 0;
        /// <summary>
        /// Previous level of the magic skill (before rebirth).
        /// </summary>
        private Byte mOldLevel = 0;
        /// <summary>
        /// Whether or not the magic skill is unlearned.
        /// </summary>
        private Boolean mIsUnlearn = false;

        /// <summary>
        /// Generic information of the magic skill.
        /// </summary>
        private Info mInfo;

        /// <summary>
        /// Unique ID of the magic skill.
        /// </summary>
        public UInt32 Id
        {
            get { return mId; }
        }

        /// <summary>
        /// Owner of the magic skill.
        /// </summary>
        public Player Player
        {
            get { return mPlayer; }
        }

        /// <summary>
        /// Type of the magic skill.
        /// </summary>
        public UInt16 Type
        {
            get { return mType; }
        }

        /// <summary>
        /// Sort of the magic.
        /// </summary>
        public MagicSort Sort { get { return mInfo.Sort; } }

        /// <summary>
        /// Whether or not using the magic is a crime.
        /// </summary>
        public Boolean IsCrime { get { return mInfo.Crime; } }

        /// <summary>
        /// Whether or not it is a ground magic.
        /// </summary>
        public Boolean IsGround { get { return mInfo.Ground; } }

        /// <summary>
        /// Bit mask for the target type of the magic.
        /// </summary>
        public MagicTarget TargetType { get { return (MagicTarget)mInfo.Target; } }

        /// <summary>
        /// Required weapon subtype to use the magic.
        /// </summary>
        public UInt16 WeaponSubtype { get { return mInfo.WeaponSubtype; } }

        /// <summary>
        /// Level of the magic skill.
        /// </summary>
        public Byte Level
        {
            get { return mLevel; }
            set
            {
                mLevel = value;

                Magic.Info info;
                if (!Database.AllMagics.TryGetValue((mType * 10) + mLevel, out info))
                {
                    sLogger.Warn("Magic type {0} not loaded but required by {1}!",
                        mType, Id);
                }
                mInfo = info;

                Database.UpdateField(this, "level", mLevel);
            }
        }

        /// <summary>
        /// Experience of the magic skill.
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
        /// Previous level of the magic skill (before rebirth).
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
        /// Whether or not the magic skill is unlearned.
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

        public Magic(UInt32 aId, Player aPlayer, UInt16 aType, Byte aLevel, UInt32 aExp, Byte aOldLevel, Boolean aIsUnlearn)
        {
            mId = aId;
            mPlayer = aPlayer;
            mType = aType;
            mLevel = aLevel;
            mExp = aExp;
            mOldLevel = aOldLevel;
            mIsUnlearn = aIsUnlearn;

            Magic.Info info;
            if (!Database.AllMagics.TryGetValue((mType * 10) + mLevel, out info))
            {
                sLogger.Warn("Magic type {0} not loaded but required by {1}!",
                    mType, Id);
            }
            mInfo = info;
        }

        /// <summary>
        /// Try to create a new magic skill for the specified player.
        /// If created successfully, it must be awarded to the player.
        /// </summary>
        /// <param name="aPlayer">The player.</param>
        /// <param name="aType">The type of the magic skill.</param>
        /// <param name="aLevel">The level of the magic skill.</param>
        /// <returns>A magic skill on success, null otherwise.</returns>
        public static Magic Create(Player aPlayer, UInt16 aType, Byte aLevel)
        {
            Magic magic = null;
            Database.CreateMagic(aPlayer, aType, aLevel, out magic);
            return magic;
        }
    }

    public partial class Database
    {
        /// <summary>
        /// Try to create a new magic skill for the specified player.
        /// </summary>
        /// <param name="aPlayer">The player.</param>
        /// <param name="aType">The type of the magic skill.</param>
        /// <param name="aLevel">The level of the magic skill.</param>
        /// <param name="aOutMagic">The newly created magic skill.</param>
        /// <returns>True on success, false otherwise.</returns>
        public static Boolean CreateMagic(Player aPlayer, UInt16 aType, Byte aLevel, out Magic aOutMagic)
        {
            bool created = false;
            aOutMagic = null;

            using (var connection = sDefaultPool.GetConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = (
                        "INSERT INTO `magic` (`owner_id`, `type`, `level`, `exp`, `old_level`, `unlearn`) " +
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
                        aOutMagic = new Magic(uid, aPlayer, aType, aLevel, 0, 0, false);

                        sLogger.Debug("Created magic {0} in database.", aOutMagic.Id);
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
        /// Try to retreive the player's magic skills.
        /// </summary>
        /// <param name="aPlayer">The player.</param>
        /// <returns>True on success, false otherwise.</returns>
        public static Boolean GetPlayerMagics(Player aPlayer)
        {
            bool success = false;

            using (var connection = sDefaultPool.GetConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT `id`, `type`, `level`, `exp`, `old_level`, `unlearn` FROM `magic` WHERE `owner_id` = @owner_id";
                    command.Parameters.AddWithValue("@owner_id", aPlayer.UniqId);
                    command.Prepare();

                    sLogger.Debug("Executing SQL: {0}", GetSqlCommand(command));

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Magic magic = new Magic(
                                reader.GetUInt32("id"),
                                aPlayer,
                                reader.GetUInt16("type"),
                                reader.GetByte("level"),
                                reader.GetUInt32("exp"),
                                reader.GetByte("old_level"),
                                reader.GetBoolean("unlearn"));

                            aPlayer.AwardMagic(magic, false);
                        }
                    }
                }
            }

            return success;
        }

        /// <summary>
        /// Update the specified field of the magic table.
        /// </summary>
        /// <param name="aMagic">The skill to update.</param>
        /// <param name="aField">The field to update.</param>
        /// <param name="aValue">The new value.</param>
        /// <returns>True on success, false otherwise.</returns>
        public static Boolean UpdateField(Magic aMagic, String aField, object aValue)
        {
            bool success = false;

            using (var connection = sDefaultPool.GetConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = String.Format("UPDATE `magic` SET `{0}` = @field WHERE `id` = @id", aField);
                    command.Parameters.AddWithValue("@id", aMagic.Id);
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
        /// Delete the specified magic skill.
        /// </summary>
        /// <param name="aMagic">The magic skill to delete.</param>
        /// <returns>True on success, false otherwise.</returns>
        public static Boolean Delete(Magic aMagic)
        {
            bool success = false;

            using (var connection = sDefaultPool.GetConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM `magic` WHERE `id` = @id";
                    command.Parameters.AddWithValue("@id", aMagic.Id);
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
