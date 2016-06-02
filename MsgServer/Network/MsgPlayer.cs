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
    public class MsgPlayer : Msg
    {
        /// <summary>
        /// This is a "constant" that the child must override.
        /// It is the type of the message as specified in NetworkDef.cs file.
        /// </summary>
        protected override UInt16 _TYPE { get { return MSG_PLAYER; } }

        //--------------- Internal Members ---------------
        private Int32 __Id = 0;
        private UInt32 __Lookface = 0;
        private UInt32 __Status = 0;
        private UInt32 __SynID_Rank = 0; // OwnerId
        private Int32 __HeadType = 0;
        private Int32 __ArmorType = 0;
        private Int32 __WeaponRType = 0;
        private Int32 __WeaponLType = 0;
        private Int32 __Unknown = 0; // MantleType \ (MaxLife, MonsterLife) ?
        private UInt16 __Life = 0;
        private UInt16 __Level = 0;
        private UInt16 __PosX = 0;
        private UInt16 __PosY = 0;
        private UInt16 __Hair = 0;
        private Byte __Dir = 0;
        private Byte __Pose = 0;
        private Byte __Metempsychosis = 0;
        private StringPacker __StrPacker = null;
        //------------------------------------------------

        /// <summary>
        /// Unique Id of the entity.
        /// </summary>
        public Int32 Id
        {
            get { return __Id; }
            set { __Id = value; WriteInt32(4, value); }
        }

        /// <summary>
        /// Look of the entity.
        /// </summary>
        public UInt32 Lookface
        {
            get { return __Lookface; }
            set { __Lookface = value; WriteUInt32(8, value); }
        }

        /// <summary>
        /// Status of the entity.
        /// </summary>
        public UInt32 Status
        {
            get { return __Status; }
            set { __Status = value; WriteUInt32(12, value); }
        }

        public UInt16 SynId
        {
            get { return (UInt16)(__SynID_Rank & 0x00FFFFFFU); }
            set { __SynID_Rank = (UInt32)((__SynID_Rank & 0xFF000000) | value); WriteUInt16(16, value); }
        }

        public Byte SynRank
        {
            get { return (Byte)(__SynID_Rank >> 24); }
            set { __SynID_Rank = (UInt32)((value << 24) | (__SynID_Rank & 0x00FFFFFFU)); mBuf[19] = value; }
        }

        public UInt32 OwnerId
        {
            get { return __SynID_Rank; }
            set { __SynID_Rank = value; WriteUInt32(16, value); }
        }

        public Int32 HeadType
        {
            get { return __HeadType; }
            set { __HeadType = value; WriteInt32(20, value); }
        }

        public Int32 ArmorType
        {
            get { return __ArmorType; }
            set { __ArmorType = value; WriteInt32(24, value); }
        }

        public Int32 WeaponRType
        {
            get { return __WeaponRType; }
            set { __WeaponRType = value; WriteInt32(28, value); }
        }

        public Int32 WeaponLType
        {
            get { return __WeaponLType; }
            set { __WeaponLType = value; WriteInt32(32, value); }
        }

        public UInt16 Life
        {
            get { return __Life; }
            set { __Life = value; WriteUInt16(40, value); }
        }

        public UInt16 Level
        {
            get { return __Level; }
            set { __Level = value; WriteUInt16(42, value); }
        }

        public UInt16 PosX
        {
            get { return __PosX; }
            set { __PosX = value; WriteUInt16(44, value); }
        }

        public UInt16 PosY
        {
            get { return __PosY; }
            set { __PosY = value; WriteUInt16(46, value); }
        }

        public UInt16 Hair
        {
            get { return __Hair; }
            set { __Hair = value; WriteUInt16(48, value); }
        }

        public Byte Direction
        {
            get { return __Dir; }
            set { __Dir = value; mBuf[50] = value; }
        }

        public Byte Pose
        {
            get { return __Pose; }
            set { __Pose = value; mBuf[51] = value; }
        }

        public Byte Metempsychosis
        {
            get { return __Metempsychosis; }
            set { __Metempsychosis = value; mBuf[52] = value; }
        }

        /// <summary>
        /// Name of the entity.
        /// </summary>
        public String Name
        {
            get { String name = ""; __StrPacker.GetString(out name, 0); return name; }
            set { __StrPacker.AddString(value); }
        }

        /// <summary>
        /// Create a new message for the specified player.
        /// </summary>
        /// <param name="aPlayer">The player.</param>
        public MsgPlayer(Player aPlayer)
            : base((UInt16)(55 + aPlayer.Name.Length))
        {
            Syndicate.Member synMember = null;
            if (aPlayer.Syndicate != null)
                synMember = aPlayer.Syndicate.GetMemberInfo(aPlayer.UniqId);

            Id = aPlayer.UniqId;
            Lookface = aPlayer.Look;
            Status = aPlayer.Statuses;
            if (synMember != null)
            {
                SynId = aPlayer.Syndicate.Id;
                SynRank = (Byte)synMember.Rank;
            }
            HeadType = aPlayer.GetHeadTypeID();
            ArmorType = aPlayer.GetArmorTypeID();
            WeaponRType = aPlayer.GetRightHandTypeID();
            WeaponLType = aPlayer.GetLeftHandTypeID();
            Life = (UInt16)aPlayer.CurHP;
            Level = aPlayer.Level;
            PosX = aPlayer.X;
            PosY = aPlayer.Y;
            Hair = aPlayer.Hair;
            Direction = aPlayer.Direction;
            Pose = (Byte)aPlayer.Action;
            Metempsychosis = aPlayer.Metempsychosis;

            __StrPacker = new StringPacker(this, 53);
            __StrPacker.AddString(aPlayer.Name);
        }

        /// <summary>
        /// Create a new message for the specified monster.
        /// </summary>
        /// <param name="aMonster">The monster.</param>
        public MsgPlayer(Monster aMonster)
            : base((UInt16)(55 + aMonster.Name.Length))
        {
            Id = aMonster.UniqId;
            Lookface = aMonster.Look;
            Status = aMonster.Statuses;
            Life = (UInt16)aMonster.CurHP;
            Level = aMonster.Level;
            PosX = aMonster.X;
            PosY = aMonster.Y;
            Direction = aMonster.Direction;
            Pose = (Byte)aMonster.Action;

            __StrPacker = new StringPacker(this, 53);
            __StrPacker.AddString(aMonster.Name);
        }
    }
}
