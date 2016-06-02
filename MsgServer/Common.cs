// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.Collections.Generic;

namespace COServer
{
    public enum Sex
    {
        /// <summary>
        /// Man.
        /// </summary>
        Man,
        /// <summary>
        /// Woman.
        /// </summary>
        Woman,
    }

    public enum GemType : byte
    {
        /// <summary>
        /// No hole in the item.
        /// </summary>
        None = 0,
        /// <summary>
        /// Normal phoenix gem. Add 5% more magic attack.
        /// </summary>
        MAtkLow = 1,
        /// <summary>
        /// Refined phoenix gem. Add 10% more magic attack.
        /// </summary>
        MAtkMid = 2,
        /// <summary>
        /// Super phoenix gem. Add 15% more magic attack.
        /// </summary>
        MAtkHgt = 3,
        /// <summary>
        /// Normal dragon gem. Add 5% more physical attack.
        /// </summary>
        DmgLow = 11,
        /// <summary>
        /// Refined dragon gem. Add 10% more physical attack.
        /// </summary>
        DmgMid = 12,
        /// <summary>
        /// Super dragon gem. Add 15% more physical attack.
        /// </summary>
        DmgHgt = 13,
        /// <summary>
        /// Normal fury gem. Add 5% more hit accuracy.
        /// </summary>
        HitLow = 21,
        /// <summary>
        /// Refined fury gem. Add 10% more hit accuracy.
        /// </summary>
        HitMid = 22,
        /// <summary>
        /// Super fury gem. Add 15% more hit accuracy.
        /// </summary>
        HitHgt = 23,
        /// <summary>
        /// Normal rainbow gem. Add 10% more experience.
        /// </summary>
        ExpLow = 31,
        /// <summary>
        /// Refined rainbow gem. Add 15% more experience.
        /// </summary>
        ExpMid = 32,
        /// <summary>
        /// Super rainbow gem. Add 25% more experience.
        /// </summary>
        ExpHgt = 33,
        /// <summary>
        /// Normal kylin gem. Add 50% more item durability.
        /// </summary>
        DurLow = 41,
        /// <summary>
        /// Refined kylin gem. Add 100% more item durability.
        /// </summary>
        DurMid = 42,
        /// <summary>
        /// Super kylin gem. Add 200% more item durability.
        /// </summary>
        DurHgt = 43,
        /// <summary>
        /// Normal violet gem. Add 30% more experience for weapon skills.
        /// </summary>
        WpnExpLow = 51,
        /// <summary>
        /// Refined violet gem. Add 50% more experience for weapon skills.
        /// </summary>
        WpnExpMid = 52,
        /// <summary>
        /// Super violet gem. Add 100% more experience for weapon skills.
        /// </summary>
        WpnExpHgt = 53,
        /// <summary>
        /// Normal moon gem. Add 15% more experience for magic skills.
        /// </summary>
        MgcExpLow = 61,
        /// <summary>
        /// Refined moon gem. Add 30% more experience for magic skills.
        /// </summary>
        MgcExpMid = 62,
        /// <summary>
        /// Super moon gem. Add 50% more experience for magic skills.
        /// </summary>
        MgcExpHgt = 63,
        /// <summary>
        /// Normal tortoise gem. Reduce by 2% the taken damage.
        /// </summary>
        DefLow = 71,
        /// <summary>
        /// Refined tortoise gem. Reduce by 4% the taken damage.
        /// </summary>
        DefMid = 72,
        /// <summary>
        /// Super tortoise gem. Reduce by 6% the taken damage.
        /// </summary>
        DefHgt = 73,
        /// <summary>
        /// Hole.
        /// </summary>
        Hole = 255,
    }

    public enum PackageType
    {
        None = 0,
        Storage = 10,
        Trunk = 20,
        Chest = 30,
    }

    public struct ShopInfo
    {
        public Int32 Id;
        public List<Int32> Items;
    }

    public struct ItemAddition
    {
        public Int32 Id;
        public Int16 Life;
        public Int16 MaxAtk;
        public Int16 MinAtk;
        public Int16 Defence;
        public Int16 MAtk;
        public Int16 MDef;
        public Int16 Dexterity;
        public Int16 Dodge;
    }

    public enum MagicSort : byte
    {
        AttackSingleHP = 1,
        RecoverSingleHP = 2,
        AttackCrossHP = 3,
        AttackSectorHP = 4,
        AttackRoundHP = 5,
        AttackSingleStatus = 6,
        RecoverSingleStatus = 7,
        Square = 8,
        JumpAttack = 9,
        RandomTrans = 10,
        DispatchXP = 11,
        Collide = 12,
        SerialCut = 13,
        Line = 14,
        AtkRange = 15,
        AtkStatus = 16,
        CallTeamMember = 17,
        RecordTransSpell = 18, // record map position to trans spell
        Transform = 19,
        AddMana = 20, // support self target only
        LayTrap = 21,
        Dance = 22,
        CallPet = 23,
        Vampire = 24, // power is percent award. use for call pet
        Instead = 25, // use for call pet
        DecLife = 26,
        GroundSting = 27,
        Reborn = 28,
        TeamMagic = 29,
        BombLockAll = 30,
        SorbSoul = 31,
        Steal = 32,
        LinePenetrable = 33,
        BlastThunder = 34,
        MultiAttachStatus = 35,
        MultiDetachStatus = 36,
        MultiCure = 37,
        StealMoney = 38,
        KO = 39,
        Escape = 40,
        FlashAttack = 41,
        AttrackMonster = 42,
    }

    [Flags]
    public enum MagicTarget : uint
    {
        Self = 0x01,
        None = 0x02,
        Terrain = 0x04,
        Passive = 0x08,
        Body = 0x10,
    }

    public class PasswayInfo
    {
        public UInt32 MapId;
        public UInt32 Idx;
        public UInt32 PortalMap;
        public UInt32 PortalIdx;
        public UInt16 PortalX;
        public UInt16 PortalY;
    }

    public enum PkMode
    {
        Free = 0,
        Safe = 1,
        Team = 2,
        Arrestment = 3,
    };

    public enum Emotion
    {
        None = 0,
        Dance1 = 1,
        Dance2 = 2,
        Dance3 = 3,
        Dance4 = 4,
        Dance5 = 5,
        Dance6 = 6,
        Dance7 = 7,
        Dance8 = 8,
        StandBy = 100,
        Laugh = 150,
        GufFaw = 151,
        Fury = 160,
        Sad = 170,
        Excitement = 180,
        SayHello = 190,
        Salute = 200,
        Genuflect = 210,
        Kneel = 220,
        Cool = 230,
        Swim = 240,
        SitDown = 250,
        Zazen = 260,
        Faint = 270,
        Lie = 271,
    };

    public enum Dialog
    {
        None = 0,
        Compose = 1,
        Forge = 2,
        Forge2 = 3,
        Warehouse = 4,
    };

    public enum Command
    {
        QuitGame = 1,
        HideClient = 2,
        OnlineShop = 26, //Common (EMoney)
        TradeCursor = 27,
        ChatHistorique = 32,
        ChatColor = 33,
        DropMoney = 44,
        SynCursor = 46,
        QuitSyn = 47,
        FriendCursor = 54,
        BlackList = 80,
        ToS = 101, //StrRes (10367)
        HideCounter = 105,
        Crash0 = 114,
        Capture = 1025,
        XPSkillShow = 1037,
        XPSkillHide = 1038,
        InternetLag1 = 1041,
        DeleteEnemy = 1052,
        ShowRevive = 1053,
        HideRevive = 1054,
        LeaveWords = 1058,
        SynStatuary = 1066,
        EmptyDialog1 = 1070,
        OtherEquipmentDialog = 1074,
        GambleOpen = 1077,
        GambleClose = 1078,
        InternetLag2 = 1083,
        AcceptToS = 1085, //StrRes (Text: 10405, 10406 Link: 10367)
        Compose = 1086,
        ForgeOpen = 1088,
        ForgeOpen2 = 1089,
        WarehouseOpen = 1090,
        EnchantOpen = 1091,
        OfflineTG = 1092,
        ShoppingMallOpen = 1099,
        ShoppingMallBtnShow = 1100,
        ShoppingMallBtnHide = 1101,
        Crash1 = 1106,
        EmptyDialog2 = 1116,
        NoOfflineTG = 1117,
        MsgRadioEffect = 1130,
        MsgRadioOpen = 1133,
        MsgRadioClose = 1135,
        PathFindingOpen = 1142,
        PathFindingClose = 1143,
        Crash2 = 1145,
        CancelPathFinding = 1146,
    };

    public enum Channel : ushort
    {
        Normal = 2000,
        Private = 2001,
        Action = 2002,
        Team = 2003,
        Syndicate = 2004,
        System = 2005,
        Family = 2006,
        Talk = 2007,
        Yelp = 2008,
        Friend = 2009,
        Global = 2010,
        GM = 2011,
        Whisper = 2012,
        Ghost = 2013,
        Serve = 2014,
        Register = 2100,
        Entrance = 2101,
        Shop = 2102,
        PetTalk = 2103,
        CryOut = 2104,
        WebPage = 2105,
        NewMessage = 2106,
        Task = 2107,
        SynWar_First = 2108,
        SynWar_Next = 2109,
        LeaveWord = 2110,
        SynAnnounce = 2111,
        MessageBox = 2112,
        Reject = 2113,
        SynTenet = 2114,
        MsgTrade = 2201,
        MsgFriend = 2202,
        MsgTeam = 2203,
        MsgSyn = 2204,
        MsgOther = 2205,
        MsgSystem = 2206,
        Broadcast = 2500,
    }

    public enum WordsStyle : byte
    {
        None = 0,
        Scroll = 1,
        Flash = 2,
        Blast = 8,
    }

    public enum WeatherType : byte
    {
        None = 1,
        Rain = 2,
        Snow = 3,
        RainWind = 4,
        AutumnLeaves = 5,
        CherryBlossomPetals = 7,
        CherryBlossomPetalsWind = 8,
        BlowingCotten = 9,
        Atoms = 10,
    }

    /// <summary>
    /// RGB color code.
    /// </summary>
    public enum Color : uint
    {
        Red = 0xFF0000,
        Cyan = 0x00FFFF,
        Blue = 0x0000FF,
        DarkBlue = 0x0000A0,
        LightBlue = 0xADD8E6,
        Purple = 0x800080,
        Yellow = 0xFFFF00,
        Lime = 0x00FF00,
        Magenta = 0xFF00FF,
        White = 0xFFFFFF,
        Silver = 0xC0C0C0,
        Grey = 0x808080,
        Black = 0x000000,
        Orange = 0xFFA500,
        Brown = 0xA52A2A,
        Maroon = 0x800000,
        Green = 0x008000,
        Olive = 0x808000,
    }

    [Flags]
    public enum Status : uint
    {
        None = 0x00000000,
        Crime = 0x00000001, // Blue name
        Poison = 0x00000002,
        //Invisible = 0x04,
        XpFull = 0x00000010,
        Freeze = 0x00000020,
        TeamLeader = 0x00000040,
        Accuracy = 0x00000080, // Accuracy
        MagicDefense = 0x00000100, // MagicShield
        MagicAttack = 0x00000200, // Stigma
        Die = 0x00000400,
        FadeOut = 0x00000800,
        RedName = 0x00004000,
        BlackName = 0x00008000,
        SuperAtk = 0x00040000, // Superman
        MagicDodge = 0x00200000, // Dodge
        Hidden = 0x00400000, // Invisibility
        SuperSpeed = 0x00800000, // Cyclone
        Flying = 0x08000000, // Flying
        Intensify = 0x10000000, // Intensify
        CastingPray = 0x40000000, // Casting Pray
        Praying = 0x80000000, // Praying
    };

    /// <summary>
    /// Effect of a status on the entity.
    /// </summary>
    public struct StatusEffect
    {
        /// <summary>
        /// The type of the status.
        /// </summary>
        public Status Type { get; private set; }
        /// <summary>
        /// The UTC date and time of the timeout of the effect.
        /// </summary>
        public DateTime Timeout { get; private set; }
        /// <summary>
        /// The data linked to this effect.
        /// </summary>
        public Object Data { get; set; }

        /// <summary>
        /// Determine whether or not the effect is elapsed.
        /// </summary>
        /// <returns>True if the effect is elapsed, false otherwise.</returns>
        public bool IsElapsed() { return DateTime.UtcNow > Timeout; }

        /// <summary>
        /// Create a new effect for the specified status.
        /// </summary>
        /// <param name="aType">The type of the effect.</param>
        /// <param name="aDuration">The duration of the effect in ms (-1 is infinity).</param>
        /// <param name="aData">The initial value of the data (optional).</param>
        public StatusEffect(Status aType, int aDuration, Object aData = null)
            : this()
        {
            Type = aType;
            Timeout = aDuration > 0 ? DateTime.UtcNow.AddMilliseconds(aDuration) : DateTime.MaxValue;
            Data = aData;
        }

        /// <summary>
        /// Increase the duration of the effect.
        /// </summary>
        /// <param name="aDuration">The duration to add to the effect in ms.</param>
        public void IncreaseDuration(int aDuration)
        {
            if (Timeout != DateTime.MaxValue && !IsElapsed())
                Timeout = Timeout.AddMilliseconds(aDuration);
        }

        /// <summary>
        /// Reset the timeout of the effect.
        /// </summary>
        /// <param name="aDuration">The duration of the effect in ms (-1 is infinity).</param>
        public void ResetTimeout(int aDuration)
        {
            Timeout = aDuration > 0 ? DateTime.UtcNow.AddMilliseconds(aDuration) : DateTime.MaxValue;
        }
    }
}
