// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2010 - 2015 Jean-Philippe Boivin
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
    /// Message sent to the client by the MsgServer to fill all the player variables.
    /// </summary>
    public class MsgUserInfo : Msg
    {
        /// <summary>
        /// This is a "constant" that the child must override.
        /// It is the type of the message as specified in NetworkDef.cs file.
        /// </summary>
        protected override UInt16 _TYPE { get { return MSG_USERINFO; } }

        //--------------- Internal Members ---------------
        private Int32 __UniqId = 0;
        private UInt32 __Look = 0;
        private UInt16 __Hair = 0;
        private Byte __Length = 0;
        private Byte __Fat = 0;
        private UInt32 __Money = 0;
        private UInt32 __Exp = 0;
        private Byte[] __Unknown = new Byte[12];
        private UInt16 __Force = 0;
        private UInt16 __Dexterity = 0;
        private UInt16 __Health = 0;
        private UInt16 __Soul = 0;
        private UInt16 __AddPoints = 0;
        private UInt16 __CurHP = 0;
        private UInt16 __CurMP = 0;
        private Int16 __PkPoints = 0;
        private Byte __Level = 0;
        private Byte __Profession = 0;
        private Boolean __AutoAllot = true;
        private Byte __Metempsychosis = 0;
        private Boolean __ShowName = false;
        private StringPacker __StrPacker = null;
        //------------------------------------------------

        /// <summary>
        /// Unique Id of the player.
        /// </summary>
        public Int32 UniqId
        {
            get { return __UniqId; }
            set { __UniqId = value; WriteInt32(4, value); }
        }

        /// <summary>
        /// Look of the player.
        /// </summary>
        public UInt32 Look
        {
            get { return __Look; }
            set { __Look = value; WriteUInt32(8, value); }
        }

        /// <summary>
        /// Hair style of the player.
        /// </summary>
        public UInt16 Hair
        {
            get { return __Hair; }
            set { __Hair = value; WriteUInt16(12, value); }
        }

        /// <summary>
        /// Money of the player.
        /// </summary>
        public UInt32 Money
        {
            get { return __Money; }
            set { __Money = value; WriteUInt32(16, value); }
        }

        /// <summary>
        /// Experience of the player.
        /// </summary>
        public UInt32 Exp
        {
            get { return __Exp; }
            set { __Exp = value; WriteUInt32(20, value); }
        }

        /// <summary>
        /// Force of the player.
        /// </summary>
        public UInt16 Force
        {
            get { return __Force; }
            set { __Force = value; WriteUInt16(40, value); }
        }

        /// <summary>
        /// Dexterity of the player.
        /// </summary>
        public UInt16 Dexterity
        {
            get { return __Dexterity; }
            set { __Dexterity = value; WriteUInt16(42, value); }
        }

        /// <summary>
        /// Health of the player.
        /// </summary>
        public UInt16 Health
        {
            get { return __Health; }
            set { __Health = value; WriteUInt16(44, value); }
        }

        /// <summary>
        /// Soul of the player.
        /// </summary>
        public UInt16 Soul
        {
            get { return __Soul; }
            set { __Soul = value; WriteUInt16(46, value); }
        }

        /// <summary>
        /// Additional points of the player.
        /// </summary>
        public UInt16 AddPoints
        {
            get { return __AddPoints; }
            set { __AddPoints = value; WriteUInt16(48, value); }
        }

        /// <summary>
        /// Hit points of the player.
        /// </summary>
        public UInt16 CurHP
        {
            get { return __CurHP; }
            set { __CurHP = value; WriteUInt16(50, value); }
        }

        /// <summary>
        /// Mana points of the player.
        /// </summary>
        public UInt16 CurMP
        {
            get { return __CurMP; }
            set { __CurMP = value; WriteUInt16(52, value); }
        }

        /// <summary>
        /// Pk points of the player.
        /// </summary>
        public Int16 PkPoints
        {
            get { return __PkPoints; }
            set { __PkPoints = value; WriteInt16(54, value); }
        }

        /// <summary>
        /// Level of the player.
        /// </summary>
        public Byte Level
        {
            get { return __Level; }
            set { __Level = value; mBuf[56] = value; }
        }

        /// <summary>
        /// Profession of the player.
        /// </summary>
        public Byte Profession
        {
            get { return __Profession; }
            set { __Profession = value; mBuf[57] = value; }
        }

        /// <summary>
        /// If the server handle the allot points.
        /// </summary>
        public Boolean AutoAllot
        {
            get { return __AutoAllot; }
            set { __AutoAllot = value; mBuf[58] = value ? (byte)1 : (byte)0; }
        }

        /// <summary>
        /// Metempsychosis of the player.
        /// </summary>
        public Byte Metempsychosis
        {
            get { return __Metempsychosis; }
            set { __Metempsychosis = value; mBuf[59] = value; }
        }

        /// <summary>
        /// If the client must show the name of the player.
        /// </summary>
        public Boolean ShowName
        {
            get { return __ShowName; }
            set { __ShowName = value; mBuf[60] = value ? (byte)1 : (byte)0; }
        }

        /// <summary>
        /// Name of the player.
        /// </summary>
        public String Name
        {
            get { String name = ""; __StrPacker.GetString(out name, 0); return name; }
            set { __StrPacker.AddString(value); }
        }

        /// <summary>
        /// Mate of the player.
        /// </summary>
        public String Mate
        {
            get { String mate = ""; __StrPacker.GetString(out mate, 1); return mate; }
            set { __StrPacker.AddString(value); }
        }

        /// <summary>
        /// Create a new message for the specified player.
        /// </summary>
        /// <param name="aPlayer">The player.</param>
        public MsgUserInfo(Player aPlayer)
            : base((UInt16)(64 + aPlayer.Name.Length + aPlayer.Mate.Length))
        {
            UniqId = aPlayer.UniqId;
            Look = aPlayer.Look;
            Hair = aPlayer.Hair;
            // Length -> unused
            // Fat -> unused
            Money = aPlayer.Money;
            Exp = aPlayer.Exp;
            // 12 unknown bytes
            Force = aPlayer.Strength;
            Dexterity = aPlayer.Agility;
            Health = aPlayer.Vitality;
            Soul = aPlayer.Spirit;
            AddPoints = aPlayer.AddPoints;
            CurHP = (UInt16)aPlayer.CurHP;
            CurMP = aPlayer.CurMP;
            PkPoints = aPlayer.PkPoints;
            Level = (Byte)aPlayer.Level;
            Profession = aPlayer.Profession;
            AutoAllot = aPlayer.AutoAllot;
            Metempsychosis = aPlayer.Metempsychosis;
            ShowName = true;

            __StrPacker = new StringPacker(this, 61);
            __StrPacker.AddString(aPlayer.Name);
            __StrPacker.AddString(aPlayer.Mate);
        }
    }
}
