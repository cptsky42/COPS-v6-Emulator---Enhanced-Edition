// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2010 - 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("COServer.Network.Msg")]

namespace COServer.Network
{
    /// <summary>
    /// Message describing all caracteristics of an item.
    /// </summary>
    public class MsgItemInfo : Msg
    {
        /// <summary>
        /// This is a "constant" that the child must override.
        /// It is the type of the message as specified in NetworkDef.cs file.
        /// </summary>
        protected override UInt16 _TYPE { get { return MSG_ITEMINFO; } }

        public enum Action
        {
            AddItem = 1,
            Trade = 2,
            Update = 3,
            OtherPlayer_Equipement = 4,
        };

        //--------------- Internal Members ---------------
        private Int32 __Id = 0;
        private Int32 __Type = 0;
        private UInt16 __Amount = 0;
        private UInt16 __AmountLimit = 0;
        private Action __Action = (Action)0;
        private Byte __Ident = 0;
        private Byte __Position = 0;
        private Byte __Gem1 = 0;
        private Byte __Gem2 = 0;
        private Byte __Magic1 = 0; // Attribute
        private Byte __Magic2 = 0;
        private Byte __Magic3 = 0; // Craft
        private Byte __Bless = 0;
        private Byte __Enchant = 0;
        private Int32 __Data = 0; // Restrain
        //------------------------------------------------

        /// <summary>
        /// Unique ID of the item.
        /// </summary>
        public Int32 Id
        {
            get { return __Id; }
            set { __Id = value; WriteInt32(4, value); }
        }

        /// <summary>
        /// Type of the item.
        /// </summary>
        public Int32 Type
        {
            get { return __Type; }
            set { __Type = value; WriteInt32(8, value); }
        }

        /// <summary>
        /// Actual amount of the item.
        /// </summary>
        public UInt16 Amount
        {
            get { return __Amount; }
            set { __Amount = value; WriteInt32(12, value); }
        }

        /// <summary>
        /// Maximum amount of the item.
        /// </summary>
        public UInt16 AmountLimit
        {
            get { return __AmountLimit; }
            set { __AmountLimit = value; WriteInt32(14, value); }
        }

        /// <summary>
        /// Action ID.
        /// </summary>
        public Action _Action
        {
            get { return __Action; }
            set { __Action = value; mBuf[16] = (Byte)value; }
        }

        /// <summary>
        /// Unknown ? What is ident for an item ?
        /// </summary>
        public Byte Ident
        {
            get { return __Ident; }
            set { __Ident = value; mBuf[17] = value; }
        }

        /// <summary>
        /// Position of the item.
        /// </summary>
        public Byte Position
        {
            get { return __Position; }
            set { __Position = value; mBuf[18] = value; }
        }

        /// <summary>
        /// First socket of the item.
        /// </summary>
        public Byte Gem1
        {
            get { return __Gem1; }
            set { __Gem1 = value; mBuf[19] = value; }
        }

        /// <summary>
        /// Second socket of the item.
        /// </summary>
        public Byte Gem2
        {
            get { return __Gem2; }
            set { __Gem2 = value; mBuf[20] = value; }
        }

        /// <summary>
        /// Attribute of the item. (e.g. poison)
        /// </summary>
        public Byte Magic1
        {
            get { return __Magic1; }
            set { __Magic1 = value; mBuf[21] = value; }
        }

        /// <summary>
        /// Unknown attribute.
        /// </summary>
        public Byte Magic2
        {
            get { return __Magic2; }
            set { __Magic2 = value; mBuf[22] = value; }
        }

        /// <summary>
        /// Craft of the item.
        /// </summary>
        public Byte Magic3
        {
            get { return __Magic3; }
            set { __Magic3 = value; mBuf[23] = value; }
        }

        /// <summary>
        /// Bless of the item.
        /// </summary>
        public Byte Bless
        {
            get { return __Bless; }
            set { __Bless = value; mBuf[24] = value; }
        }

        /// <summary>
        /// Enchant of the item.
        /// </summary>
        public Byte Enchant
        {
            get { return __Enchant; }
            set { __Enchant = value; mBuf[25] = value; }
        }

        /// <summary>
        /// Restrain of the item. 
        /// </summary>
        public Int32 Data
        {
            get { return __Data; }
            set { __Data = value; WriteInt32(26, value); }
        }

        public MsgItemInfo(Item aItem, Action aAction)
            : base(30)
        {
            Id = aItem.Id;
            Type = aItem.Type;
            Amount = aItem.CurDura;
            AmountLimit = aItem.MaxDura;
            _Action = aAction;
            Ident = 0x00;
            Position = (Byte)aItem.Position;
            Gem1 = aItem.FirstGem;
            Gem2 = aItem.SecondGem;
            Magic1 = aItem.Attribute;
            Magic2 = 0x00;
            Magic3 = aItem.Craft;
            Bless = aItem.Bless;
            Enchant = aItem.Enchant;
            Data = aItem.Restrain;
        }

        public MsgItemInfo(Int32 aOwnerId, Item aItem, Action aAction)
            : base(30)
        {
            Id = aOwnerId;
            Type = aItem.Type;
            Amount = aItem.CurDura;
            AmountLimit = aItem.MaxDura;
            _Action = aAction;
            Ident = 0x00;
            Position = (Byte)aItem.Position;
            Gem1 = aItem.FirstGem;
            Gem2 = aItem.SecondGem;
            Magic1 = aItem.Attribute;
            Magic2 = 0x00;
            Magic3 = aItem.Craft;
            Bless = aItem.Bless;
            Enchant = aItem.Enchant;
            Data = aItem.Restrain;
        }
    }
}
