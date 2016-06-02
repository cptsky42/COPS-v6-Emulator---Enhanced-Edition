// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2010 - 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.Collections.Generic;
using COServer.Entities;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.Builders;
using MoonSharp.Interpreter;
using COServer.Script;

namespace COServer
{
    [MoonSharpUserData]
    public class Item
    {
        /// <summary>
        /// The logger of the class.
        /// </summary>
        private static readonly log4net.ILog sLogger = log4net.LogManager.GetLogger(typeof(Database));

        /// <summary>
        /// Pseudo-random number generator derived from CO2's code.
        /// </summary>
        private static readonly SafeRandom sRand = new SafeRandom();

        public const Int32 TYPE_DRAGONBALL = 1088000;
        public const Int32 TYPE_METEOR = 1088001;
        public const Int32 TYPE_DIVORCEITEM = 1088002;

        //Item monopoly bit field
        public const Int32 ITEM_MONOPOLY_MASK = 0x01;
        public const Int32 ITEM_STORAGE_MASK = 0x02;
        public const Int32 ITEM_DROP_HINT_MASK = 0x04;
        public const Int32 ITEM_SELL_HINT_MASK = 0x08;
        public const Int32 ITEM_NEVER_DROP_WHEN_DEAD_MASK = 0x10;
        public const Int32 ITEM_SELL_DISABLE_MASK = 0x20;

        //Item status bit field
        public const Int32 ITEM_STATUS_NONE = 0;
        public const Int32 ITEM_STATUS_NOT_IDENT = 1;
        public const Int32 ITEM_STATUS_CANNOT_REPAIR = 2;
        public const Int32 ITEM_STATUS_NEVER_DAMAGE = 4;
        public const Int32 ITEM_STATUS_MAGIC_ADD = 8;

        public struct Info
        {
            public Int32 ID;
            public String Name;
            public Byte RequiredProfession;
            public Byte RequiredWeaponSkill;
            public Byte RequiredLevel;
            public Byte RequiredSex;
            public UInt16 RequiredForce;
            public UInt16 RequiredSpeed;
            public UInt16 RequiredHealth;
            public UInt16 RequiredSoul;
            public Byte Monopoly;
            public UInt16 Weight;
            public UInt32 Price;
            public ItemTask Task;
            public UInt16 MaxAttack;
            public UInt16 MinAttack;
            public Int16 Defense;
            public Int16 Dexterity;
            public Int16 Dodge;
            public Int16 Life;
            public Int16 Mana;
            public UInt16 Amount;
            public UInt16 AmountLimit; // x / 100
            public Byte Status;
            public Byte Gem1;
            public Byte Gem2;
            public Byte Magic1;
            public Byte Magic2;
            public Byte Magic3;
            public UInt16 MagicAttack;
            public UInt16 MagicDefence;
            public UInt16 Range;
            public UInt16 AttackSpeed;

            public Boolean IsExchangeEnable() { return (Monopoly & ITEM_MONOPOLY_MASK) == 0; }
            public Boolean IsStorageEnable() { return (Monopoly & ITEM_STORAGE_MASK) == 0; }
            public Boolean IsSellEnable() { return (Monopoly & ITEM_SELL_DISABLE_MASK) == 0; }
            public Boolean IsRepairEnable() { return true;/* (RepairMode & 0x02) == 0;*/ }
        };

        private Int32 mId = -1;
        private Int32 mOwnerUID = -1;
        private UInt16 mPosition = 0;
        private Int32 mType = 0;
        private Byte mCraft = 0;
        private Byte mBless = 0;
        private Byte mEnchant = 0;
        private Byte mFirstGem = 0;
        private Byte mSecondGem = 0;
        private Byte mAttribute = 0;
        private Int32 mRestrain = 0;
        private UInt16 mCurDura = 0;
        private UInt16 mMaxDura = 0;

        internal Info mInfo;

        public Int32 Id
        {
            get { return mId; }
            private set { mId = value; }
        }

        public Int32 OwnerUID
        {
            get { return mOwnerUID; }
            set
            {
                mOwnerUID = value;

                var collection = Database.GetCollection<Item>("Items");
                var query = Query<Item>.EQ(item => item.Id, Id);
                var update = Update<Item>.Set(item => item.OwnerUID, value);
                collection.Update(query, update);
            }
        }

        public UInt16 Position
        {
            get { return mPosition; }
            set
            {
                mPosition = value;

                var collection = Database.GetCollection<Item>("Items");
                var query = Query<Item>.EQ(item => item.Id, Id);
                var update = Update<Item>.Set(item => item.Position, value);
                collection.Update(query, update);
            }
        }

        public Int32 Type
        {
            get { return mType; }
            set
            {
                mType = value;

                Item.Info info;
                if (!Database.AllItems.TryGetValue(mType, out info))
                {
                    sLogger.Warn("Item type {0} not loaded but required by {1}!",
                        mType, Id);
                }
                mInfo = info;

                var collection = Database.GetCollection<Item>("Items");
                var query = Query<Item>.EQ(item => item.Id, Id);
                var update = Update<Item>.Set(item => item.Type, value);
                collection.Update(query, update);
            }
        }

        [BsonIgnore]
        public String Name { get { return mInfo.Name; } }

        [BsonIgnore]
        public UInt16 Weight { get { return mInfo.Weight; } }

        [BsonIgnore]
        public UInt32 Price { get { return mInfo.Price; } }

        [BsonIgnore]
        public UInt16 MinAtk { get { return mInfo.MinAttack; } }

        [BsonIgnore]
        public UInt16 MaxAtk { get { return mInfo.MaxAttack; } }

        [BsonIgnore]
        public Int16 Defense { get { return mInfo.Defense; } }

        [BsonIgnore]
        public UInt16 MagicAtk { get { return mInfo.MagicAttack; } }

        [BsonIgnore]
        public UInt16 MagicDef { get { return mInfo.MagicDefence; } }

        [BsonIgnore]
        public Int16 Dexterity { get { return mInfo.Dexterity; } }

        [BsonIgnore]
        public Int16 Dodge { get { return mInfo.Dodge; } }

        [BsonIgnore]
        public UInt16 Range { get { return mInfo.Range; } }

        [BsonIgnore]
        public UInt16 AtkSpeed { get { return mInfo.AttackSpeed; } }

        [BsonIgnore]
        public Int16 Life { get { return mInfo.Life; } }

        [BsonIgnore]
        public Int16 Mana { get { return mInfo.Mana; } }

        public Byte Craft
        {
            get { return mCraft; }
            set
            {
                mCraft = value;

                var collection = Database.GetCollection<Item>("Items");
                var query = Query<Item>.EQ(item => item.Id, Id);
                var update = Update<Item>.Set(item => item.Craft, value);
                collection.Update(query, update);
            }
        }

        public Byte Bless
        {
            get { return mBless; }
            set
            {
                mBless = value;

                var collection = Database.GetCollection<Item>("Items");
                var query = Query<Item>.EQ(item => item.Id, Id);
                var update = Update<Item>.Set(item => item.Bless, value);
                collection.Update(query, update);
            }
        }

        public Byte Enchant
        {
            get { return mEnchant; }
            set
            {
                mEnchant = value;

                var collection = Database.GetCollection<Item>("Items");
                var query = Query<Item>.EQ(item => item.Id, Id);
                var update = Update<Item>.Set(item => item.Enchant, value);
                collection.Update(query, update);
            }
        }

        public Byte FirstGem
        {
            get { return mFirstGem; }
            set
            {
                mFirstGem = value;

                var collection = Database.GetCollection<Item>("Items");
                var query = Query<Item>.EQ(item => item.Id, Id);
                var update = Update<Item>.Set(item => item.FirstGem, value);
                collection.Update(query, update);
            }
        }

        public Byte SecondGem
        {
            get { return mSecondGem; }
            set
            {
                mSecondGem = value;

                var collection = Database.GetCollection<Item>("Items");
                var query = Query<Item>.EQ(item => item.Id, Id);
                var update = Update<Item>.Set(item => item.SecondGem, value);
                collection.Update(query, update);
            }
        }

        public Byte Attribute
        {
            get { return mAttribute; }
            set
            {
                mAttribute = value;

                var collection = Database.GetCollection<Item>("Items");
                var query = Query<Item>.EQ(item => item.Id, Id);
                var update = Update<Item>.Set(item => item.Attribute, value);
                collection.Update(query, update);
            }
        }

        public Int32 Restrain
        {
            get { return mRestrain; }
            set
            {
                mRestrain = value;

                var collection = Database.GetCollection<Item>("Items");
                var query = Query<Item>.EQ(item => item.Id, Id);
                var update = Update<Item>.Set(item => item.Restrain, value);
                collection.Update(query, update);
            }
        }

        public UInt16 CurDura
        {
            get { return mCurDura; }
            set
            {
                mCurDura = value;
            }
        }

        public UInt16 MaxDura
        {
            get { return mMaxDura; }
            set
            {
                mMaxDura = value;

                var collection = Database.GetCollection<Item>("Items");
                var query = Query<Item>.EQ(item => item.Id, Id);
                var update = Update<Item>.Set(item => item.MaxDura, value);
                collection.Update(query, update);
            }
        }

        public bool Use()
        {
            if (mInfo.Task != null)
            {
                Player player = null;
                if (World.AllPlayers.TryGetValue(OwnerUID, out player))
                    mInfo.Task.Execute(player.Client, new object[] { this });

                return true;
            }

            return false;
        }

        public Byte GetQuality()
        {
            return (Byte)(Type % 10);
        }

        /// <summary>
        /// Get the base type (lowest level item).
        /// </summary>
        public Int32 GetBaseType()
        {
            Int32 firstType = mType;

            Byte sort = (Byte)(mType / 100000);
            Byte type = (Byte)(mType / 10000);
            Int16 subtype = (Int16)(mType / 1000);

            if (subtype == 137 || subtype == 422 || subtype == 562)
                return mType;

            if (mType == 150000 || mType == 150310 || mType == 150320 || mType == 410301 || mType == 421301 || mType == 500301)
                return mType;

            if (mType >= 120310 && mType <= 120319)
                return mType;

            if (sort == 1) //!Weapon
            {
                if (type == 12) //Necklace
                    firstType = (mType - (mType % 1000)) + (mType % 10);
                else if (type == 15 || type == 16) //Ring || Boots
                    firstType = (mType - (mType % 1000)) + 10 + (mType % 10);
                else if (type == 11) //Head
                {
                    if (subtype != 112 && subtype != 115 && subtype != 116)
                        firstType = (mType - (mType % 100)) + (mType % 10);
                    else
                    {
                        firstType = 110000 + (((mType % 100) - (mType % 10)) * 100) + ((mType % 1000) - (mType % 100)) + (mType % 10);
                    }
                }
                else if (type == 13) //Armor
                {
                    if (subtype != 135 && subtype != 136 && subtype != 138 && subtype != 139)
                        firstType = (mType - (mType % 100)) + (mType % 10);
                    else
                    {
                        firstType = (mType - (mType % 100)) + (mType % 10);
                        firstType -= 5000;
                    }
                }
            }
            else if (sort == 4 || sort == 5) //Weapon
                firstType = (mType - (mType % 1000)) + (mType % 10);
            else if (sort == 9)
                firstType = (mType - (mType % 100)) + (mType % 10);

            return Database.AllItems.ContainsKey(firstType) ? firstType : mType;
        }

        public Int32 GetNextType()
        {
            Int32 nextType = mType;

            Byte sort = (Byte)(mType / 100000);
            Byte type = (Byte)(mType / 10000);
            Int16 subtype = (Int16)(mType / 1000);

            if (subtype == 137)
                return mType;

            if (mType == 150000 || mType == 150310 || mType == 150320 || mType == 410301 || mType == 421301 || mType == 500301)
                return mType;

            if (sort == 1) //!Weapon
            {
                if (type == 12 || type == 15 || type == 16) //Necklace || Ring || Boots
                {
                    Byte Level = (Byte)(((mType % 1000) - (mType % 10)) / 10);
                    if ((type == 12 && Level < 8) ||
                        ((type == 15 && subtype != 152) && Level > 0 && Level < 21) ||
                        ((type == 15 && subtype == 152) && Level >= 4 && Level < 22) ||
                        (type == 16 && Level > 0 && Level < 21))
                    {
                        //Check if it's still the same type of item...
                        if ((Int16)((nextType + 20) / 1000) == subtype)
                            nextType += 20;
                    }
                    else if ((type == 12 && Level == 8) ||
                        (type == 12 && Level >= 21) ||
                        ((type == 15 && subtype != 152) && Level == 0) ||
                        ((type == 15 && subtype != 152) && Level >= 21) ||
                        ((type == 15 && subtype == 152) && Level >= 22) ||
                        (type == 16 && Level >= 21))
                    {
                        //Check if it's still the same type of item...
                        if ((Int16)((nextType + 10) / 1000) == subtype)
                            nextType += 10;
                    }
                    else if ((type == 12 && Level >= 9 && Level < 21) ||
                        ((type == 15 && subtype == 152) && Level == 1))
                    {
                        //Check if it's still the same type of item...
                        if ((Int16)((nextType + 30) / 1000) == subtype)
                            nextType += 30;
                    }
                }
                else if (type == 11 || type == 13) //Head || Armor
                {
                    if (subtype != 112 && subtype != 115)
                    {
                        Int16 Color = (Int16)((mType % 1000) - (mType % 100));
                        Byte Level = (Byte)(((mType % 100) - (mType % 10)) / 10);
                        Byte Quality = (Byte)(mType % 10);

                        if (Level == 9) //Max...
                        {
                            if (type == 11)
                                nextType = 112000 + Color + ((subtype % 10) * 10) + Quality;
                            else if (type == 13)
                                nextType += 5000;
                        }
                        else
                        {
                            //Check if it's still the same type of item...
                            if ((Int16)((nextType + 10) / 1000) == subtype)
                                nextType += 10;
                        }
                    }
                }
            }
            else if (sort == 4 || sort == 5) //Weapon
            {
                //Check if it's still the same type of item...
                if ((Int16)((nextType + 10) / 1000) == subtype)
                    nextType += 10;

                //Invalid Backsword ID
                if ((Int32)(nextType / 10) == 42103 ||
                    (Int32)(nextType / 10) == 42105 ||
                    (Int32)(nextType / 10) == 42109 ||
                    (Int32)(nextType / 10) == 42111)
                    nextType += 10;
            }
            else if (sort == 9)
            {
                Byte Level = (Byte)(((mType % 100) - (mType % 10)) / 10);
                if (Level != 9) //!Max...
                {
                    //Check if it's still the same type of item...
                    if ((Int16)((nextType + 10) / 1000) == subtype)
                        nextType += 10;
                }
            }

            return Database.AllItems.ContainsKey(nextType) ? nextType : mType;
        }

        public double GetGemDmgEffect()
        {
            double effect = 0.0;

            GemType[] gems = new GemType[2] { (GemType)FirstGem, (GemType)SecondGem };
            foreach (var gem in gems)
            {
                if (gem != GemType.None && gem != GemType.Hole)
                {
                    switch (gem)
                    {
                        case GemType.DmgLow:
                            effect += 0.05;
                            break;
                        case GemType.DmgMid:
                            effect += 0.10;
                            break;
                        case GemType.DmgHgt:
                            effect += 0.15;
                            break;
                    }
                }
            }

            return effect;
        }

        public double GetGemMgcAtkEffect()
        {
            double effect = 0.0;

            GemType[] gems = new GemType[2] { (GemType)FirstGem, (GemType)SecondGem };
            foreach (var gem in gems)
            {
                if (gem != GemType.None && gem != GemType.Hole)
                {
                    switch (gem)
                    {
                        case GemType.MAtkLow:
                            effect += 0.05;
                            break;
                        case GemType.MAtkMid:
                            effect += 0.10;
                            break;
                        case GemType.MAtkHgt:
                            effect += 0.15;
                            break;
                    }
                }
            }

            return effect;
        }

        public double GetGemHitRateEffect()
        {
            double effect = 0.0;

            GemType[] gems = new GemType[2] { (GemType)FirstGem, (GemType)SecondGem };
            foreach (var gem in gems)
            {
                if (gem != GemType.None && gem != GemType.Hole)
                {
                    switch (gem)
                    {
                        case GemType.HitLow:
                            effect += 0.05;
                            break;
                        case GemType.HitMid:
                            effect += 0.10;
                            break;
                        case GemType.HitHgt:
                            effect += 0.15;
                            break;
                    }
                }
            }

            return effect;
        }

        public double GetGemExpEffect()
        {
            double effect = 0.0;

            GemType[] gems = new GemType[2] { (GemType)FirstGem, (GemType)SecondGem };
            foreach (var gem in gems)
            {
                if (gem != GemType.None && gem != GemType.Hole)
                {
                    switch (gem)
                    {
                        case GemType.ExpLow:
                            effect += 0.10;
                            break;
                        case GemType.ExpMid:
                            effect += 0.15;
                            break;
                        case GemType.ExpHgt:
                            effect += 0.25;
                            break;
                    }
                }
            }

            return effect;
        }

        public double GetGemWpnExpEffect()
        {
            double effect = 0.0;

            GemType[] gems = new GemType[2] { (GemType)FirstGem, (GemType)SecondGem };
            foreach (var gem in gems)
            {
                if (gem != GemType.None && gem != GemType.Hole)
                {
                    switch (gem)
                    {
                        case GemType.WpnExpLow:
                            effect += 0.30;
                            break;
                        case GemType.WpnExpMid:
                            effect += 0.50;
                            break;
                        case GemType.WpnExpHgt:
                            effect += 1.00;
                            break;
                    }
                }
            }

            return effect;
        }

        public double GetGemMgcExpEffect()
        {
            double effect = 0.0;

            GemType[] gems = new GemType[2] { (GemType)FirstGem, (GemType)SecondGem };
            foreach (var gem in gems)
            {
                if (gem != GemType.None && gem != GemType.Hole)
                {
                    switch (gem)
                    {
                        case GemType.MgcExpLow:
                            effect += 0.30;
                            break;
                        case GemType.MgcExpMid:
                            effect += 0.50;
                            break;
                        case GemType.MgcExpHgt:
                            effect += 1.00;
                            break;
                    }
                }
            }

            return effect;
        }

        public double GetGemDurEffect()
        {
            double effect = 1.0;

            GemType[] gems = new GemType[2] { (GemType)FirstGem, (GemType)SecondGem };
            foreach (var gem in gems)
            {
                if (gem != GemType.None && gem != GemType.Hole)
                {
                    switch (gem)
                    {
                        case GemType.DurLow:
                            effect += 0.50;
                            break;
                        case GemType.DurMid:
                            effect += 1.00;
                            break;
                        case GemType.DurHgt:
                            effect += 2.00;
                            break;
                    }
                }
            }

            return effect;
        }

        public UInt32 CalcRepairMoney()
        {
            Int32 recoverDurability = Math.Max(0, MaxDura - CurDura);
            if (recoverDurability == 0)
                return 0;

            Double repairCost = 0;
            if (Price > 0)
                repairCost = (Double)(Price * recoverDurability / MaxDura);

            switch (Type % 10)
            {
                case 9: repairCost *= 1.125; break;
                case 8: repairCost *= 0.975; break;
                case 7: repairCost *= 0.900; break;
                case 6: repairCost *= 0.825; break;
                default: repairCost *= 0.750; break;
            }

            return Math.Max(1, (UInt32)Math.Floor(repairCost));
        }

        public UInt32 GetRecoverDurCost()
        {
            UInt16 realMaxDur = (UInt16)(MaxDura / GetGemDurEffect());
            if (realMaxDur >= mInfo.AmountLimit)
                return 0;

            UInt16 repairDur = (UInt16)(mInfo.AmountLimit - realMaxDur);
            UInt32 cost = 0;

            switch (GetQuality())
            {
                case 9:
                    {
                        cost = (5000000U * repairDur) / mInfo.AmountLimit;
                        cost = Math.Max(cost, 500000);
                        break;
                    }
                case 8:
                    {
                        cost = (3500000U * repairDur) / mInfo.AmountLimit;
                        cost = Math.Max(cost, 350000);
                        break;
                    }
                default:
                    {
                        cost = (1500000U * repairDur) / mInfo.AmountLimit;
                        cost = Math.Max(cost, 150000);
                        break;
                    }
            }

            return Math.Max(1, cost);
        }

        public void Save()
        {
            try
            {
                var collection = Database.GetCollection<Item>("Items");
                collection.Save(this);
            }
            catch (Exception exc) { sLogger.Error(exc); }
        }

        private static double randomRate(double aRange)
        {
            const double PI = 3.1415926;

            int rand = sRand.Next(999) + 1;
            double a = Math.Sin(rand * PI / 1000.0);

            return (rand >= 90) ?
                        (1.0 + aRange) - Math.Sqrt(Math.Sqrt(a)) * aRange :
                        (1.0 - aRange) + Math.Sqrt(Math.Sqrt(a)) * aRange;
        }

        public static Int32 GenerateId(Byte aQuality, Byte aDropArmet, Byte aDropNecklace, Byte aDropArmor, Byte aDropRing, Byte aDropWeapon, Byte aDropShield, Byte aDropShoes)
        {
            int rand = 0;
            Byte quality = 0;

            // item quality
            if (aQuality == 0)
            {
                // auto create this param
                rand = sRand.Next(100);
                if (rand >= 0 && rand < 30)
                    quality = 5;
                else if (rand >= 30 && rand < 70)
                    quality = 4;
                else
                    quality = 3;
            }
            else
                quality = aQuality;

            // item sort & item color
            UInt16 sort = 0;
            Byte color = 0;
            Byte lev = 0;

            rand = sRand.Next(1200);
            if (rand >= 0 && rand < 20)
            {
                // shoes
                sort = 160;
                lev = aDropShoes;
            }
            else if (rand >= 20 && rand < 50)
            {
                UInt16[] sorts = { 120, 121 }; // taoist bag & necklace

                // necklace
                sort = sorts[sRand.Next(sorts.Length)];
                lev = aDropNecklace;
            }
            else if (rand >= 50 && rand < 100)
            {
                UInt16[] sorts = { 150, 151, 152 };

                // ring
                sort = sorts[sRand.Next(sorts.Length)];
                lev = aDropRing;
            }
            else if (rand >= 100 && rand < 400)
            {
                UInt16[] sorts = { 111, 112, 113, 114, 117, 118 };

                // armet
                sort = sorts[sRand.Next(sorts.Length)];
                lev = aDropArmet;

                if (sort <= 118) // have color
                    color = (Byte)(sRand.Next(7) + 3); // 3 - 9
            }
            else if (rand >= 400 && rand < 700)
            {
                UInt16[] sorts = { 130, 131, 132, 133, 134 };

                // armor
                sort = sorts[sRand.Next(sorts.Length)];
                lev = aDropArmor;
                color = (Byte)(sRand.Next(7) + 3); // 3 - 9
            }
            else
            {	// weapon & shield
                int rate = sRand.Next(100);
                if (rate >= 0 && rate < 20) // 20% for sword of Taoist
                {
                    sort = 421;
                }
                else if (rate >= 20 && rate < 40) // 20% for archer
                {
                    sort = 500;
                }
                else if (rate >= 40 && rate < 80) // 40% for single hand weapon
                {
                    UInt16[] sorts = { 410, 420, 421, 430, 440, 450, 460, 480, 481, 490 };

                    sort = sorts[sRand.Next(sorts.Length)];
                }
                else // 20% for two hand weapon
                {
                    UInt16[] sorts = { 510, 530, 560, 561, 580, 900 };

                    sort = sorts[sRand.Next(sorts.Length)];
                }

                if (sort == 900) // shield
                {
                    color = (Byte)(sRand.Next(7) + 3); // 3 - 9
                    lev = aDropShield;
                }
                else // weapon
                {
                    lev = aDropWeapon;
                }
            }


            // item lev
            if (lev == 99)
                return -1;

            {
                int rate = sRand.Next(100);
                if (rate < 50) // 50% down one lev
                {
                    lev = (Byte)(sRand.Next(lev / 2) + (lev / 3));

                    if (lev >= 1)
                        --lev;
                }
                else if (rate >= 80) // 20% up one lev
                {
                    if ((sort >= 110 && sort <= 114) ||
                        (sort >= 130 && sort <= 134) ||
                        (sort >= 900 && sort <= 999))
                    {
                        // item with color
                        lev = (Byte)Math.Min(lev + 1, 9);
                    }
                    else
                    {
                        lev = (Byte)Math.Min(lev + 1, 23);
                    }
                }

                // 50% do nothing
            }


            // item type
            return (sort * 1000) + (color * 100) + (lev * 10) + quality;
        }

        public static bool Create(out Item aOutItem, UInt32 aValue, Monster aMonster, Byte aQuality)
        {
            aOutItem = null;

            Int32 itemtype = GenerateId(
                aQuality,
                aMonster.DropArmet, aMonster.DropNecklace, aMonster.DropArmor, aMonster.DropRing,
                aMonster.DropWeapon, aMonster.DropShield, aMonster.DropShoes);

            if (itemtype == -1)
                return false;

            Item.Info info;
            if (!Database.AllItems.TryGetValue(itemtype, out info))
                return false;

            Byte quality = (Byte)(itemtype % 10);

            UInt16 amount = info.Amount, amountLimit = info.AmountLimit;
            Byte firstGem = 0, secondGem = 0;
            Byte addition = 0, bless = 0;

            amountLimit = (UInt16)(amountLimit * randomRate(0.3));
            if (amountLimit < 1)
                amountLimit = 1;

            if (quality > 5)
                amount = (UInt16)(amountLimit * ((double)(50 + sRand.Next(50)) / 100.00));
            else
            {
                double price = info.Price;
                if (price <= 0)
                    price = 1;

                amount = (UInt16)(3 * amountLimit * ((double)aValue / price));
                if (amount >= amountLimit)
                    amount = amountLimit;

                if (amount < 1)
                    amount = 1;
            }

            // gem hole
            if (itemtype >= 400000 && itemtype < 599999)
            {
                // is weapon
                int rate = sRand.Next(100);
                if (rate < 5) // 5% got 2 holes
                {
                    firstGem = 0xFF;
                    secondGem = 0xFF;
                }
                else if (rate < 20) // 15% got 1 hole
                {
                    firstGem = 0xFF;
                }

                // 80% do nothing
            }

            #if !_TQ_ONLY_
            // addition
            {
                int rate = sRand.Next(100);
                if (rate < 3) // 3% got +1
                    addition = 1;
            }

            // bless
            {
                int rate = sRand.Next(100);
                if (rate < 1) // 1% got -5%
                    bless = 5;
                else if (rate < 3) // 3% got -3%
                    bless = 3;
                else if (rate < 5) // 5% got -1%
                    bless = 1;
            }
            #endif

            Item item = Item.Create(0, 254, itemtype, addition, bless, 0, firstGem, secondGem, 2, 0, amount, amountLimit);
            aOutItem = item;

            return true;
        }

        public static Item Create(Int32 aOwnerUID, Byte aPosition, Int32 aType,
            Byte aCraft, Byte aBless, Byte aEnchant, Byte aFirstGem, Byte aSecondGem,
            Byte aAttribute, Int32 aRestrain, UInt16 aCurDura, UInt16 aMaxDura)
        {
            try
            {
                lock (World.AllItems)
                {
                    var collection = Database.GetCollection<Item>("Items");

                    while (World.AllItems.ContainsKey(World.LastItemUID))
                        ++World.LastItemUID;

                    Item item = new Item
                    {
                        mId = World.LastItemUID++,
                        mOwnerUID = aOwnerUID,
                        mPosition = aPosition,
                        mType = aType,
                        mCraft = aCraft,
                        mBless = aBless,
                        mEnchant = aEnchant,
                        mFirstGem = aFirstGem,
                        mSecondGem = aSecondGem,
                        mAttribute = aAttribute,
                        mRestrain = aRestrain,
                        mCurDura = aCurDura,
                        mMaxDura = aMaxDura,
                        mInfo = Database.AllItems[aType]
                    };
                    World.AllItems.Add(item.Id, item);

                    if (aPosition != 254)
                        collection.Insert(item);

                    return item;
                }
            }
            catch (Exception exc) { sLogger.Error(exc); return null; }
        }

        public static Item CreateMoney(Int32 aType)
        {
            try
            {
                lock (World.AllItems)
                {
                    while (World.AllItems.ContainsKey(World.LastItemUID))
                        ++World.LastItemUID;

                    Item item = new Item
                    {
                        mId = World.LastItemUID++,
                        mOwnerUID = 0,
                        mPosition = 254,
                        mType = aType,
                        mInfo = Database.AllItems[aType]
                    };
                    World.AllItems.Add(item.Id, item);

                    return item;
                }
            }
            catch (Exception exc) { sLogger.Error(exc); return null; }
        }

        public static void Delete(Int32 aId)
        {
            try
            {
                lock (World.AllItems)
                {
                    var collection = Database.GetCollection<Item>("Items");
                    var query = Query<Item>.EQ(item => item.Id, aId);
                    collection.Remove(query);

                    if (World.AllItems.ContainsKey(aId))
                    {
                        Player player = null;
                        if (World.AllPlayers.TryGetValue(World.AllItems[aId].OwnerUID, out player))
                            player.DelItem(aId, true);

                        World.AllItems[aId].OwnerUID = 0;
                        World.AllItems.Remove(aId);
                    }
                }
            }
            catch (Exception exc) { sLogger.Error(exc); }
        }
    }

    public partial class Database
    {
        public static void GetAllItems()
        {
            try
            {
                sLogger.Info("Loading all items in memory...");

                var collection = Database.GetCollection<Item>("Items");
                var items = collection.FindAll();

                World.AllItems = new Dictionary<Int32, Item>(200000);
                foreach (Item item in items)
                {
                    if (item.OwnerUID == 0)
                    {
                        Item.Delete(item.Id);
                        continue;
                    }

                    if (!World.AllItems.ContainsKey(item.Id))
                        World.AllItems.Add(item.Id, item);
                }

                while (collection.FindOne(Query<Item>.EQ(item => item.Id, World.LastItemUID)) != null ||
                       World.AllItems.ContainsKey(World.LastItemUID))
                {
                    ++World.LastItemUID;
                }
            }
            catch (Exception exc) { sLogger.Error(exc); }
        }
    }
}
