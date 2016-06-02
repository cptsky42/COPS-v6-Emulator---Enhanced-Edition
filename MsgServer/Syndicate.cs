// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2011 - 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using COServer.Entities;
using COServer.Network;
using MySql.Data.MySqlClient;

[assembly: InternalsVisibleTo("Database")]

namespace COServer
{
    public class Syndicate
    {
        /// <summary>
        /// The logger of the class.
        /// </summary>
        private static readonly log4net.ILog sLogger = log4net.LogManager.GetLogger(typeof(Database));

        /// <summary>
        /// The maximum number of allies a syndicate can have.
        /// </summary>
        public const int MAX_ALLIES_COUNT = 5;
        /// <summary>
        /// The maximum number of enemies a syndicate can have.
        /// </summary>
        public const int MAX_ENEMIES_COUNT = 5;

        public enum Rank : byte
        {
            None = 0,
            Member = 50,

            InternMgr = 60,
            DeputyMgr = 70,
            BranchMgr = 80,

            SubLeader = 90,
            Leader = 100,
        }

        public class Member
        {
            private readonly Int32 mId;
            private readonly String mName;
            private Byte mLevel;

            private Rank mRank;
            private UInt32 mProffer;

            public Int32 Id
            { 
                get { return mId; }
            }

            public String Name
            { 
                get { return mName; }
            }

            public Byte Level
            {
                get { return mLevel; }
                set { mLevel = value; }
            }

            public Rank Rank
            {
                get { return mRank; }
                set
                {
                    mRank = value;
                    Database.UpdateField(this, "rank", (Byte)mRank);
                }
            }

            public UInt32 Proffer
            {
                get { return mProffer; }
                set
                {
                    mProffer = value;
                    Database.UpdateField(this, "proffer", mProffer);
                }
            }

            public Member(Int32 aId, String aName, Byte aLevel, Rank aRank, UInt32 aProffer)
            {
                mId = aId;
                mName = aName;
                mLevel = aLevel;
                mRank = aRank;
                mProffer = aProffer;
            }
        }

        private UInt16 mId = 0;
        private String mName = "";
        private String mAnnounce = "";

        private Int32 mLeaderUID = 0;
        private String mLeaderName = "";
        internal Member mLeader = null;
        internal readonly Dictionary<Int32, Member> mMembers = new Dictionary<Int32, Member>();

        private UInt16 mFealtySyn = 0;

        private UInt32 mMoney = 0;
        private List<UInt16> mAllies = new List<UInt16>(MAX_ALLIES_COUNT);
        private List<UInt16> mEnemies = new List<UInt16>(MAX_ENEMIES_COUNT);

        public UInt16 Id
        {
            get { return mId; }
        }

        public String Name
        {
            get { return mName; }
        }

        public String Announce
        {
            get { return mAnnounce; }
            set
            { 
                mAnnounce = value;
                Database.UpdateField(this, "announce", mAnnounce);
            }
        }

        public Int32 LeaderUID
        {
            get { return mLeaderUID; }
            private set
            {
                mLeaderUID = value;
                Database.UpdateField(this, "leader_id", mLeaderUID);
            }
        }

        public String LeaderName
        {
            get { return mLeaderName; }
            private set
            {
                mLeaderName = value;
                Database.UpdateField(this, "leader_name", mLeaderName);
            }
        }

        public Member Leader
        {
            get { return mLeader; }
            set
            {
                mLeader = value;
                mLeaderUID = value.Id;
                mLeaderName = value.Name;
            }
        }

        public Dictionary<Int32, Member> Members
        {
            get { return mMembers; }
        }

        public UInt32 Money
        {
            get { return mMoney; }
            set
            {
                mMoney = value;
                Database.UpdateField(this, "money", mMoney);
            }
        }

        public UInt16 FealtySynUID
        {
            get { return mFealtySyn; }
            private set
            {
                mFealtySyn = value;
                Database.UpdateField(this, "fealty_syn", mFealtySyn);
            }
        }

        public List<UInt16> Allies
        {
            get { return mAllies; }
            set
            {
                if (value.Count != MAX_ALLIES_COUNT)
                    throw new ArgumentException(String.Format("A syndicate must have {0} allies.", MAX_ALLIES_COUNT));

                mAllies = value;
                Database.UpdateField(this, "ally0", mAllies[0]);
                Database.UpdateField(this, "ally1", mAllies[1]);
                Database.UpdateField(this, "ally2", mAllies[2]);
                Database.UpdateField(this, "ally3", mAllies[3]);
                Database.UpdateField(this, "ally4", mAllies[4]);
            }
        }

        public List<UInt16> Enemies
        {
            get { return mEnemies; }
            set
            {
                if (value.Count != MAX_ENEMIES_COUNT)
                    throw new ArgumentException(String.Format("A syndicate must have {0} enemies.", MAX_ENEMIES_COUNT));

                mEnemies = value;
                Database.UpdateField(this, "enemy0", mEnemies[0]);
                Database.UpdateField(this, "enemy1", mEnemies[1]);
                Database.UpdateField(this, "enemy2", mEnemies[2]);
                Database.UpdateField(this, "enemy3", mEnemies[3]);
                Database.UpdateField(this, "enemy4", mEnemies[4]);
            }
        }

        public Syndicate(UInt16 aId, String aName, String aAnnounce, Int32 aLeaderUID, String aLeaderName, UInt32 aMoney, UInt16 aFealtySyn, UInt16[] aEnemies, UInt16[] aAllies)
        {
            mId = aId;
            mName = aName;
            mAnnounce = aAnnounce;
            mLeaderUID = aLeaderUID;
            mLeaderName = aLeaderName;
            mMoney = aMoney;
            mFealtySyn = aFealtySyn;
            mEnemies = new List<UInt16>(aEnemies);
            mAllies = new List<UInt16>(aAllies);
        }

        public bool IsMasterSyn()
        {
            return mFealtySyn == 0;
        }

        public Syndicate GetMasterSyn()
        {
            if (mFealtySyn == 0)
                return this;

            Syndicate syn = null;
            if (!World.AllSyndicates.TryGetValue(mFealtySyn, out syn))
                return this;

            return syn;
        }

        public Boolean CreateSubSyn(Player aLeader, String aName, UInt32 aMoney, out Syndicate aOutSyn)
        {
            aOutSyn = null;

            if (mFealtySyn != 0)
                return false;

            if (Create(this, aLeader, aName, aMoney, out aOutSyn))
                return true;

            // synchro relation
            //var msg = new MsgSyndicate(
            //    CMsgSyndicate	msg;
            //    IF_OK(msg.Create(SET_SYN, idNewSyn, idFealty))
            //        pFealty->BroadcastSynMsg(&msg);
            //    return idNewSyn;

            return false;
        }

        /// <summary>
        /// Donate money to the syndicate.
        /// </summary>
        /// <param name="aPlayer">The member donating to the syndicate.</param>
        /// <param name="aMoney">The money to donate.</param>
        /// <returns>True on success, false otherwise.</returns>
        public Boolean Donate(Player aPlayer, UInt32 aMoney)
        {
            if (aMoney <= 0)
                return false;

            if (aPlayer.Money < aMoney)
            {
                aPlayer.SendSysMsg(StrRes.STR_NOT_SO_MUCH_MONEY);
                return false;
            }

            if (!IsMember(aPlayer.UniqId))
                return false;

            Member member = GetMemberInfo(aPlayer.UniqId);

            aPlayer.Money -= aMoney;
            Money += aMoney;
            member.Proffer += aMoney;

            // TODO no longer manually update MsgUserAttrib
            aPlayer.Send(new MsgUserAttrib(aPlayer, aPlayer.Money, MsgUserAttrib.AttributeType.Money));

            Msg msg = new MsgTalk(
                "SYSTEM", "ALLUSERS",
                String.Format(StrRes.STR_DONATE, aPlayer.Name, aMoney),
                Channel.Syndicate, Color.White);
            World.BroadcastSynMsg(this, msg);

            SynchroInfo(aPlayer, false);
            return true;
        }

        public Boolean IsAnAlly(UInt16 SynUID)
        {
            foreach (UInt16 AllyUID in Allies)
            {
                if (AllyUID == SynUID)
                    return true;
            }
            return false;
        }

        public Boolean IsHostile(UInt16 SynUID)
        {
            foreach (UInt16 EnemyUID in Enemies)
            {
                if (EnemyUID == SynUID)
                    return true;
            }
            return false;
        }

        public Boolean IsMember(Int32 UniqId)
        {
            if (Leader.Id == UniqId)
                return true;

            if (Members.ContainsKey(UniqId))
                return true;

            return false;
        }

        public void AddMember(Player aPlayer)
        {
            Member member = null;
            Database.CreateSynMember(aPlayer, this, (Byte)Rank.Member, 0, out member);

            lock (mMembers) { mMembers.Add(member.Id, member); }

            aPlayer.Syndicate = this;
            aPlayer.Send(new MsgSynAttrInfo(aPlayer.UniqId, this));
            aPlayer.Send(new MsgTalk("SYSTEM", "ALLUSERS", Announce, Channel.SynAnnounce, Color.White));

            foreach (Int16 enemyId in Enemies)
                aPlayer.Send(new MsgSyndicate((UInt32)enemyId, MsgSyndicate.Action.SetAntagonize));

            foreach (Int16 allyId in Allies)
                aPlayer.Send(new MsgSyndicate((UInt32)allyId, MsgSyndicate.Action.SetAlly));

            World.BroadcastRoomMsg(aPlayer, new MsgPlayer(aPlayer), true);
            World.BroadcastSynMsg(this, new MsgTalk("SYSTEM", "ALLUSERS", String.Format(StrRes.STR_SYN_JOIN, aPlayer.Name), Channel.Syndicate, Color.White));
        }

        [Obsolete]
        public void DelMember(Member Member, Boolean Kicked)
        {
            if (Member.Id == Leader.Id)
                return;

            Player Player = null;
            if (World.AllPlayers.TryGetValue(Member.Id, out Player))
            {
                lock (Members) { Members.Remove(Member.Id); }

                Player.Syndicate = null;
                Database.LeaveSyn(Member.Name);

                Player.Send(new MsgSynAttrInfo(Player.UniqId, Player.Syndicate));
                World.BroadcastRoomMsg(Player, new MsgPlayer(Player), true);
            }
            else
            {
                lock (Members) { Members.Remove(Member.Id); }
                Database.LeaveSyn(Member.Name);
            }

            if (Kicked)
                World.BroadcastSynMsg(this, new MsgTalk("SYSTEM", "ALLUSERS", String.Format(StrRes.STR_KICKOUT, Member.Name), Channel.Syndicate, Color.White));
        }

        public bool LeaveSyn(Player aPlayer, bool aKickout, bool aSynchro)
        {
            Syndicate syn = aPlayer.Syndicate;

            // TODO destroy syn if player is leader

            aPlayer.Syndicate = null;
            Database.LeaveSyn(aPlayer.Name);

            lock (mMembers) { mMembers.Remove(aPlayer.UniqId); }

            if (aSynchro)
                SynchroInfo(aPlayer, true);

            if (aKickout)
            {
                Msg msg = new MsgTalk(
                    "SYSTEM", "ALLUSERS",
                    String.Format(StrRes.STR_KICKOUT, aPlayer.Name),
                    Channel.Syndicate, Color.White);
                World.BroadcastSynMsg(this, msg);
            }

            return true;
        }

        public Member GetMemberInfo(Int32 aUID)
        {
            if (Leader.Id == aUID)
                return Leader;

            if (Members.ContainsKey(aUID))
                return Members[aUID];

            return null;
        }

        public Member GetMemberInfo(String aName)
        {
            if (Leader.Name == aName)
                return Leader;

            foreach (Member Member in Members.Values)
            {
                if (Member.Name == aName)
                    return Member;
            }

            return null;
        }

        public String[] GetMemberList()
        {
            List<String> Online = new List<String>();
            List<String> Offline = new List<String>();

            if (World.AllPlayers.ContainsKey(Leader.Id))
                Online.Add(Leader.Name + " " + Leader.Level + " 1");
            else
                Offline.Add(Leader.Name + " " + Leader.Level + " 0");

            foreach (Member Member in Members.Values)
            {
                if (World.AllPlayers.ContainsKey(Member.Id))
                    Online.Add(Member.Name + " " + Member.Level + " 1");
                else
                    Offline.Add(Member.Name + " " + Member.Level + " 0");
            }

            String[] List = new String[Online.Count + Offline.Count];
            Online.CopyTo(List, 0);
            Offline.CopyTo(List, Online.Count);
            return List;
        }

        /// <summary>
        /// Try to create a new syndicate for the specified player.
        /// If created successfully, it will be assigned to the player.
        /// </summary>
        /// <param name="aLeader">The player.</param>
        /// <param name="aName">The name of the syndicate.</param>
        /// <param name="aMoney">The money of the syndicate.</param>
        /// <param name="aOutSyn">The newly created syndicate.</param>
        /// <returns>True on success, false otherwise.</returns>
        public static Boolean Create(Player aLeader, String aName, UInt32 aMoney, out Syndicate aOutSyn)
        {
            if (Database.CreateSyndicate(aLeader, aName, aMoney, out aOutSyn))
            {
                aLeader.Syndicate = aOutSyn;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Try to create a new sub-syn for the specified syndicate with the specified
        /// player as the branch manager.
        /// </summary>
        /// <param name="aFealtySyn">The fealty syndicate.</param>
        /// <param name="aLeader">The branch manager.</param>
        /// <param name="aName">The name of the syndicate.</param>
        /// <param name="aMoney">The money of the syndicate.</param>
        /// <param name="aOutSyn">The newly created syndicate.</param>
        /// <returns>True on success, false otherwise.</returns>
        private static Boolean Create(Syndicate aFealtySyn, Player aLeader, String aName, UInt32 aMoney, out Syndicate aOutSyn)
        {
            if (Database.CreateSyndicate(aFealtySyn, aLeader, aName, aMoney, out aOutSyn))
            {
                aLeader.Syndicate = aOutSyn;
                return true;
            }

            return false;
        }

        public static void SynchroInfo(Player aPlayer, bool aAnnounce)
        {
            Msg msg = null;

            msg = new MsgSynAttrInfo(aPlayer.UniqId, aPlayer.Syndicate);
            aPlayer.Send(msg);

            msg = new MsgPlayer(aPlayer);
            World.BroadcastRoomMsg(aPlayer, msg, true);

            if (aAnnounce)
            {
                String words = "";
                if (aPlayer.Syndicate != null)
                {
                    var master = aPlayer.Syndicate.GetMasterSyn();
                    words = master.Announce;
                }

                msg = new MsgTalk("SYSTEM", "ALLUSERS", words, Channel.SynAnnounce, Color.White);
                aPlayer.Send(msg);
            }
        }

        public bool Destroy()
        {
            lock (World.AllSyndicates)
            {
                if (World.AllSyndicates.ContainsKey(Id))
                {
                    Msg msg = null;

                    msg = new MsgTalk(
                        "SYSTEM", "ALLUSERS",
                        String.Format(StrRes.STR_SYN_DISBAND, Name),
                        Channel.GM, Color.White);
                    World.BroadcastMsg(msg);

                    msg = new MsgSyndicate((UInt16)Id, MsgSyndicate.Action.DestroySyn);
                    World.BroadcastMsg(msg);

                    Syndicate master = GetMasterSyn();
                    Player player = null;

                    lock (mMembers)
                    {
                        List<Syndicate.Member> members = new List<Syndicate.Member>(mMembers.Values);
                        members.Add(mLeader);

                        foreach (Syndicate.Member member in members)
                        {
                            if (master == this)
                            {
                                if (World.AllPlayers.TryGetValue(member.Id, out player))
                                    LeaveSyn(player, false, true);
                                else
                                    Database.LeaveSyn(member.Name);
                            }
                            else
                                master.AddMember(player);
                        }
                    }

                    foreach (UInt16 allyId in Allies)
                    {
                        Syndicate ally = null;
                        if (World.AllSyndicates.TryGetValue(allyId, out ally))
                        {
                            if (ally.Allies.Contains(Id))
                            {
                                var allies = ally.Allies;
                                allies[allies.IndexOf(Id)] = 0;
                                ally.Allies = allies;

                                msg = new MsgSyndicate(Id, MsgSyndicate.Action.ClearAlly);
                                World.BroadcastSynMsg(ally, msg);
                            }
                        }
                    }

                    World.AllSyndicates.Remove(Id);
                    return Database.Delete(this);
                }
            }

            return false;
        }
    }

    public static partial class Database
    {
        /// <summary>
        /// Try to create a new syndicate for the specified player.
        /// </summary>
        /// <param name="aLeader">The player.</param>
        /// <param name="aName">The name of the syndicate.</param>
        /// <param name="aMoney">The money of the syndicate.</param>
        /// <param name="aOutSyn">The newly created syndicate.</param>
        /// <returns>True on success, false otherwise.</returns>
        public static Boolean CreateSyndicate(Player aLeader, String aName, UInt32 aMoney, out Syndicate aOutSyn)
        {
            bool created = false;
            aOutSyn = null;

            using (var connection = sDefaultPool.GetConnection())
            {
                // TODO use transactions...

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = (
                        "INSERT INTO `syndicate` (`name`, `announce`, `leader_id`, `leader_name`, `money`, `fealty_syn`) " +
                        "VALUES (@name, @announce, @leader_id, @leader_name, @money, @fealty_syn)");
                    command.Parameters.AddWithValue("@name", aName);
                    command.Parameters.AddWithValue("@announce", "");
                    command.Parameters.AddWithValue("@leader_id", aLeader.UniqId);
                    command.Parameters.AddWithValue("@leader_name", aLeader.Name);
                    command.Parameters.AddWithValue("@money", aMoney);
                    command.Parameters.AddWithValue("@fealty_syn", 0);
                    command.Prepare();

                    sLogger.Debug("Executing SQL: {0}", GetSqlCommand(command));

                    try
                    {
                        command.ExecuteNonQuery();
                        created = true;

                        UInt16 uid = (UInt16)command.LastInsertedId;
                        aOutSyn = new Syndicate(uid, aName, "", aLeader.UniqId, aLeader.Name, aMoney, 0,
                            new UInt16[Syndicate.MAX_ENEMIES_COUNT] { 0, 0, 0, 0, 0 },
                            new UInt16[Syndicate.MAX_ALLIES_COUNT] { 0, 0, 0, 0, 0 });

                        sLogger.Debug("Created syndicate {0} in database.", aOutSyn.Id);

                        created = Database.CreateSynMember(aLeader, aOutSyn, (Byte)Syndicate.Rank.Leader, aMoney, out aOutSyn.mLeader);

                        if (created)
                        {
                            lock (World.AllSyndicates)
                                World.AllSyndicates.Add(aOutSyn.Id, aOutSyn);
                        }
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
        /// Try to create a new sub-syn for the specified syndicate with the specified
        /// player as the branch manager.
        /// </summary>
        /// <param name="aFealtySyn">The fealty syndicate.</param>
        /// <param name="aLeader">The branch manager.</param>
        /// <param name="aName">The name of the syndicate.</param>
        /// <param name="aMoney">The money of the syndicate.</param>
        /// <param name="aOutSyn">The newly created syndicate.</param>
        /// <returns>True on success, false otherwise.</returns>
        public static Boolean CreateSyndicate(Syndicate aFealtySyn, Player aLeader, String aName, UInt32 aMoney, out Syndicate aOutSyn)
        {
            bool created = false;
            aOutSyn = null;

            using (var connection = sDefaultPool.GetConnection())
            {
                // TODO use transactions...

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = (
                        "INSERT INTO `syndicate` (`name`, `announce`, `leader_id`, `leader_name`, `money`, `fealty_syn`) " +
                        "VALUES (@name, @announce, @leader_id, @leader_name, @money, @fealty_syn)");
                    command.Parameters.AddWithValue("@name", aName);
                    command.Parameters.AddWithValue("@announce", "");
                    command.Parameters.AddWithValue("@leader_id", aLeader.UniqId);
                    command.Parameters.AddWithValue("@leader_name", aLeader.Name);
                    command.Parameters.AddWithValue("@money", aMoney);
                    command.Parameters.AddWithValue("@fealty_syn", aFealtySyn.Id);
                    command.Prepare();

                    sLogger.Debug("Executing SQL: {0}", GetSqlCommand(command));

                    try
                    {
                        command.ExecuteNonQuery();
                        created = true;

                        UInt16 uid = (UInt16)command.LastInsertedId;
                        aOutSyn = new Syndicate(uid, aName, "", aLeader.UniqId, aLeader.Name, aMoney, aFealtySyn.Id,
                            new UInt16[Syndicate.MAX_ENEMIES_COUNT] { 0, 0, 0, 0, 0 },
                            new UInt16[Syndicate.MAX_ALLIES_COUNT] { 0, 0, 0, 0, 0 });

                        sLogger.Debug("Created syndicate {0} in database.", aOutSyn.Id);

                        bool success = aFealtySyn.LeaveSyn(aLeader, false, false);
                        if (success)
                            created = Database.CreateSynMember(aLeader, aOutSyn, (Byte)Syndicate.Rank.BranchMgr, aMoney, out aOutSyn.mLeader);

                        if (created)
                        {
                            lock (World.AllSyndicates)
                                World.AllSyndicates.Add(aOutSyn.Id, aOutSyn);
                        }
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
        /// Try to create a new syndicate member for the specified player.
        /// </summary>
        /// <param name="aPlayer">The player.</param>
        /// <param name="aSyn">The syndicate of the player.</param>
        /// <param name="aRank">The rank of the player.</param>
        /// <param name="aProffer">The proffer of the player.</param>
        /// <param name="aOutSynMember">The newly created syndicate member.</param>
        /// <returns>True on success, false otherwise.</returns>
        public static Boolean CreateSynMember(Player aPlayer, Syndicate aSyn, Byte aRank, UInt32 aProffer, out Syndicate.Member aOutSynMember)
        {
            bool created = false;
            aOutSynMember = null;

            using (var connection = sDefaultPool.GetConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = (
                        "INSERT INTO `synattr` (`id`, `syn_id`, `rank`, `proffer`) " +
                        "VALUES (@id, @syn_id, @rank, @proffer)");
                    command.Parameters.AddWithValue("@id", aPlayer.UniqId);
                    command.Parameters.AddWithValue("@syn_id", aSyn.Id);
                    command.Parameters.AddWithValue("@rank", aRank);
                    command.Parameters.AddWithValue("@proffer", aProffer);
                    command.Prepare();

                    sLogger.Debug("Executing SQL: {0}", GetSqlCommand(command));

                    try
                    {
                        command.ExecuteNonQuery();
                        created = true;

                        Int32 uid = aPlayer.UniqId;
                        aOutSynMember = new Syndicate.Member(uid, aPlayer.Name, (Byte)aPlayer.Level, (Syndicate.Rank)aRank, aProffer);

                        sLogger.Debug("Created syndicate member {0} in database.", aOutSynMember.Id);
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
        /// Try to retreive the player's syndicate.
        /// </summary>
        /// <param name="aPlayer">The player.</param>
        /// <returns>True on success, false otherwise.</returns>
        public static Boolean GetPlayerSyndicate(Player aPlayer)
        {
            bool success = false;

            using (var connection = sDefaultPool.GetConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT `syn_id` FROM `synattr` WHERE `id` = @id";
                    command.Parameters.AddWithValue("@id", aPlayer.UniqId);
                    command.Prepare();

                    sLogger.Debug("Executing SQL: {0}", GetSqlCommand(command));

                    using (var reader = command.ExecuteReader())
                    {
                        int count = 0;
                        while (reader.Read())
                        {
                            ++count;

                            UInt16 synId = reader.GetUInt16("syn_id");
                            success = World.AllSyndicates.TryGetValue(synId, out aPlayer.Syndicate);
                        }

                        success = success && count == 1;
                    }
                }
            }

            return success;
        }

        /// <summary>
        /// Update the specified field of the syndicate table.
        /// </summary>
        /// <param name="aSyndicate">The syndicate to update.</param>
        /// <param name="aField">The field to update.</param>
        /// <param name="aValue">The new value.</param>
        /// <returns>True on success, false otherwise.</returns>
        public static Boolean UpdateField(Syndicate aSyn, String aField, object aValue)
        {
            bool success = false;

            using (var connection = sDefaultPool.GetConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = String.Format("UPDATE `syndicate` SET `{0}` = @field WHERE `id` = @id", aField);
                    command.Parameters.AddWithValue("@id", aSyn.Id);
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
        /// Update the specified field of the synattr table.
        /// </summary>
        /// <param name="aMember">The syndicate member to update.</param>
        /// <param name="aField">The field to update.</param>
        /// <param name="aValue">The new value.</param>
        /// <returns>True on success, false otherwise.</returns>
        public static Boolean UpdateField(Syndicate.Member aMember, String aField, object aValue)
        {
            bool success = false;

            using (var connection = sDefaultPool.GetConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = String.Format("UPDATE `synattr` SET `{0}` = @field WHERE `id` = @id", aField);
                    command.Parameters.AddWithValue("@id", aMember.Id);
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
        /// Delete the specified syndicate.
        /// </summary>
        /// <param name="aSyn">The syndicate to delete.</param>
        /// <returns>True on success, false otherwise.</returns>
        public static Boolean Delete(Syndicate aSyn)
        {
            bool success = false;

            using (var connection = sDefaultPool.GetConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM `syndicate` WHERE `id` = @id";
                    command.Parameters.AddWithValue("@id", aSyn.Id);
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
        /// Load all syndicates in memory.
        /// </summary>
        /// <returns>True on success, false otherwise.</returns>
        public static Boolean LoadAllSyndicates()
        {
            bool success = false;

            sLogger.Info("Loading all syndicates in memory...");

            using (var connection = sDefaultPool.GetConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = (
                        "SELECT `id`, `name`, `announce`, `leader_id`, `leader_name`, `money`, `fealty_syn`, " +
                        "`enemy0`, `enemy1`, `enemy2`, `enemy3`, `enemy4`, " +
                        "`ally0`, `ally1`, `ally2`, `ally3`, `ally4` FROM `syndicate`");
                    command.Prepare();

                    sLogger.Debug("Executing SQL: {0}", GetSqlCommand(command));

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Syndicate syn = new Syndicate(
                                reader.GetUInt16("id"),
                                reader.GetString("name"),
                                reader.GetString("announce"),
                                reader.GetInt32("leader_id"),
                                reader.GetString("leader_name"),
                                reader.GetUInt32("money"),
                                reader.GetUInt16("fealty_syn"),
                                new UInt16[5] { reader.GetUInt16("enemy0"), reader.GetUInt16("enemy1"), reader.GetUInt16("enemy2"), reader.GetUInt16("enemy3"), reader.GetUInt16("enemy4") },
                                new UInt16[5] { reader.GetUInt16("ally0"), reader.GetUInt16("ally1"), reader.GetUInt16("ally2"), reader.GetUInt16("ally3"), reader.GetUInt16("ally4") });

                            if (!World.AllSyndicates.ContainsKey(syn.Id))
                                World.AllSyndicates.Add(syn.Id, syn);
                        }
                    }
                }

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT u.`id`, u.`name`, u.`level`, s.`syn_id`, s.`rank`, s.`proffer` FROM `synattr` s, `user` u WHERE u.`id` = s.`id`";
                    command.Prepare();

                    sLogger.Debug("Executing SQL: {0}", GetSqlCommand(command));

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Int32 uid = reader.GetInt32("id");
                            String name = reader.GetString("name");
                            Byte level = reader.GetByte("level");

                            UInt16 synId = reader.GetUInt16("syn_id");

                            Syndicate syn = null;
                            if (!World.AllSyndicates.TryGetValue(synId, out syn))
                            {
                                Database.LeaveSyn(name);
                                continue;
                            }

                            Syndicate.Rank rank = (Syndicate.Rank)reader.GetByte("rank");
                            UInt32 proffer = reader.GetUInt32("proffer");

                            Syndicate.Member member = new Syndicate.Member(uid, name, level, rank, proffer);
                            if (syn.LeaderUID == uid)
                                syn.mLeader = member;
                            else
                            {
                                if (!syn.mMembers.ContainsKey(uid))
                                    syn.mMembers.Add(uid, member);
                            }
                        }
                    }
                }
            }

            return success;
        }
    }
}
