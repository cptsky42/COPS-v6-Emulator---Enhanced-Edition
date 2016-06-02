// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2011 - 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.Runtime.CompilerServices;
using COServer.Entities;

[assembly: InternalsVisibleTo("COServer.Network.Msg")]

namespace COServer.Network
{
    /// <summary>
    /// Message sent to the client to specify the information of a friend or enemy.
    /// </summary>
    public class MsgFriendInfo : Msg
    {
        /// <summary>
        /// This is a "constant" that the child must override.
        /// It is the type of the message as specified in NetworkDef.cs file.
        /// </summary>
        protected override UInt16 _TYPE { get { return MSG_FRIENDINFO; } }

        /// <summary>
        /// Syndicate ID mask on a 32-bit integer.
        /// </summary>
        private const UInt32 SYN_ID_MASK = 0x00FFFFFFU;
        /// <summary>
        /// Syndicate rank shift on a 32-bit integer.
        /// </summary>
        private const int RANK_SHIFT = 24;

        //--------------- Internal Members ---------------
        private Int32 __UniqId = 0;
        private UInt32 __Look = 0;
        private Byte __Level = 0;
        private Byte __Profession = 0;
        private Int16 __PkPoints = 0;
        private UInt32 __SynId_Rank = 0;
        private String __Mate = "";
        //------------------------------------------------

        /// <summary>
        /// Unique ID of the friend / enemy.
        /// </summary>
        public Int32 UniqId
        {
            get { return __UniqId; }
            set { __UniqId = value; WriteInt32(4, value); }
        }

        /// <summary>
        /// Look of the friend / enemy.
        /// </summary>
        public UInt32 Look
        {
            get { return __Look; }
            set { __Look = value; WriteUInt32(8, value); }
        }

        /// <summary>
        /// Level of the friend / enemy.
        /// </summary>
        public Byte Level
        {
            get { return __Level; }
            set { __Level = value; mBuf[12] = value; }
        }

        /// <summary>
        /// Profession of the friend / enemy.
        /// </summary>
        public Byte Profession
        {
            get { return __Profession; }
            set { __Profession = value; mBuf[13] = value; }
        }

        /// <summary>
        /// PkPoints of the friend / enemy.
        /// </summary>
        public Int16 PkPoints
        {
            get { return __PkPoints; }
            set { __PkPoints = value; WriteInt16(14, value); }
        }

        /// <summary>
        /// Syndicate ID of the friend / enemy.
        /// </summary>
        public UInt16 SynId
        {
            get { return (UInt16)(__SynId_Rank & SYN_ID_MASK); }
            set
            {
                __SynId_Rank = (UInt32)(__SynId_Rank & ~SYN_ID_MASK) | (UInt32)(value & SYN_ID_MASK);
                WriteUInt32(16, __SynId_Rank);
            }
        }

        /// <summary>
        /// Syndicate rank of the friend / enemy.
        /// </summary>
        public Byte SynRank
        {
            get { return (Byte)(__SynId_Rank >> RANK_SHIFT); }
            set
            {
                __SynId_Rank = (UInt32)(value << RANK_SHIFT) | (UInt32)(__SynId_Rank & SYN_ID_MASK);
                WriteUInt32(16, __SynId_Rank);
            }
        }

        /// <summary>
        /// Mate of the friend / enemy.
        /// </summary>
        public String Mate
        {
            get { return __Mate; }
            set { __Mate = value; WriteString(20, value, MAX_NAME_SIZE); }
        }

        /// <summary>
        /// Create a new MsgFriendInfo packet for the friend / enemy.
        /// </summary>
        /// <param name="aPlayer">The friend / enemy</param>
        public MsgFriendInfo(Player aPlayer)
            : base(36)
        {
            UniqId = aPlayer.UniqId;
            Look = aPlayer.Look;
            Level = (Byte)aPlayer.Level;
            Profession = aPlayer.Profession;
            PkPoints = aPlayer.PkPoints;
            if (aPlayer.Syndicate != null)
            {
                Syndicate.Member member = aPlayer.Syndicate.GetMemberInfo(aPlayer.UniqId);

                SynId = aPlayer.Syndicate.Id;
                SynRank = (Byte)member.Rank;
            }
            Mate = aPlayer.Mate;
        }
    }
}
