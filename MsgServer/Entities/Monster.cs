// * Created by Jean-Philippe Boivin
// * Copyright © 2010-2011
// * COPS v6 Emulator

using System;
using System.Collections.Generic;
using System.Timers;
using COServer.Network;
using MoonSharp.Interpreter;
using COServer.Script;

namespace COServer.Entities
{
    public enum MonsterType
    {
        Pet = 0,
        Normal = 1,
    }

    public enum AI_Type
    {
        Normal = 0,
        //Guard = 1, // Pet
        //WaterBat = 2
        //FireBat = 3
        //WarriorRat = 4
        //Skeleton = 5
        CityGuard = 6,
        CityPatrol = 7,
        Reviver = 8
    }

    public struct MonsterInfo
    {
        public Int32 Id;
        public String Name;
        public Byte Type;
        public Byte AIType;
        public UInt32 Look;
        public Byte Level;
        public UInt16 Life;
        public UInt16 EscapeLife;
        public Byte AtkUser;
        public UInt32 MinAtk;
        public UInt32 MaxAtk;
        public UInt32 Defense;
        public Byte Dexterity;
        public Byte Dodge;
        public UInt32 MagicType;
        public UInt32 MagicDef;
        public UInt32 MagicHitrate;
        public Byte ViewRange;
        public Byte AtkRange;
        public UInt16 AtkSpeed;
        public UInt16 MoveSpeed;
        public UInt16 RunSpeed;
        public Byte DropArmet;
        public Byte DropNecklace;
        public Byte DropArmor;
        public Byte DropRing;
        public Byte DropWeapon;
        public Byte DropShield;
        public Byte DropShoes;
        public UInt32 DropMoney;
        public UInt32 DropHP;
        public UInt32 DropMP;
        public UInt16 ExtraExp;
        public UInt16 ExtraDamage;

        // Events...
        public MonsterTask OnDie;
    }

    [MoonSharpUserData]
    public partial class Monster : AdvancedEntity
    {
        public AI Brain = null;
        private Timer Timer = null;
        private Int32 LastKillerUID = -1;

        public Int16 Id = -1;
        public Byte AIType;

        public UInt16 StartX;
        public UInt16 StartY;

        public Byte ViewRange;
        public Int32 MoveSpeed;

        private Generator mGenerator;
        //private Int32 RespawnSpeed = 20000;
        private Boolean Disappeared = false;

        public Int32 LastDieTime;

        /// <summary>
        /// The armet dropped by the monster.
        /// </summary>
        public readonly Byte DropArmet;
        /// <summary>
        /// The necklace dropped by the monster.
        /// </summary>
        public readonly Byte DropNecklace;
        /// <summary>
        /// The armor dropped by the monster.
        /// </summary>
        public readonly Byte DropArmor;
        /// <summary>
        /// The ring dropped by the monster.
        /// </summary>
        public readonly Byte DropRing;
        /// <summary>
        /// The weapon dropped by the monster.
        /// </summary>
        public readonly Byte DropWeapon;
        /// <summary>
        /// The shield dropped by the monster.
        /// </summary>
        public readonly Byte DropShield;
        /// <summary>
        /// The shoes dropped by the monster.
        /// </summary>
        public readonly Byte DropShoes;
        /// <summary>
        /// The money dropped by the monster.
        /// </summary>
        public readonly Int32 DropMoney;
        /// <summary>
        /// The HP dropped by the monster.
        /// </summary>
        public readonly Int32 DropHP;
        /// <summary>
        /// The MP dropped by the monster.
        /// </summary>
        public readonly Int32 DropMP;

        /// <summary>
        /// Task to execute when the monster die.
        /// </summary>
        private readonly MonsterTask mOnDie;

        public Monster(Int32 UniqId, MonsterInfo aInfo, Generator aGenerator)
            : base(UniqId)
        {
            this.Id = (Int16)aInfo.Id;
            this.Name = aInfo.Name;
            this.AIType = aInfo.AIType;
            mLook = aInfo.Look;
            this.Level = aInfo.Level;
            this.CurHP = aInfo.Life;
            this.MaxHP = aInfo.Life;

            this.Dexterity = aInfo.Dexterity;
            this.MinAtk = (Int32)Math.Min(aInfo.MinAtk , aInfo.MaxAtk);
            this.MaxAtk = (Int32)Math.Max(aInfo.MinAtk, aInfo.MaxAtk);
            this.Defence = (UInt16)aInfo.Defense;
            this.Dodge = aInfo.Dodge;
            this.AtkRange = aInfo.AtkRange;
            this.ViewRange = aInfo.ViewRange;
            this.AtkSpeed = aInfo.AtkSpeed;
            this.MoveSpeed = aInfo.MoveSpeed;

            this.MagicDef = (Int32)aInfo.MagicDef;
            this.MagicAtk = (Int32)aInfo.MaxAtk;
            this.MagicType = (UInt16)aInfo.MagicType;
            this.MagicLevel = 0;

            if (aInfo.MagicType != 0)
                this.AtkType = 21;
            else
                this.AtkType = 2;

            this.DropMoney = (Int32)aInfo.DropMoney;
            this.DropHP = (Int32)aInfo.DropHP;
            this.DropMP = (Int32)aInfo.DropMP;
            DropArmet = aInfo.DropArmet;
            DropNecklace = aInfo.DropNecklace;
            DropArmor = aInfo.DropArmor;
            DropRing = aInfo.DropRing;
            DropWeapon = aInfo.DropWeapon;
            DropShield = aInfo.DropShield;
            DropShoes = aInfo.DropShoes;

            mOnDie = aInfo.OnDie;

            mGenerator = aGenerator;
            //this.RespawnSpeed = RespawnSpeed;
            this.Disappeared = false;

            this.LastDieTime = -1;

            this.Timer = new Timer();
            this.Timer.Interval = 500;
            this.Timer.Elapsed += new ElapsedEventHandler(Process);

            this.Brain = new MonsterAI(this, 500, MoveSpeed, AtkSpeed, ViewRange, 5, AtkRange);
        }

        public void ResetAI() { Brain.Reset(); }

        public void Die(Int32 KillerUID)
        {
            Brain.Sleep();

            LastDieTime = Environment.TickCount;
            LastKillerUID = KillerUID;
            Disappeared = false;

            Timer.Start();

            CurHP = 0;

            DetachAllStatuses();
            AttachStatus(Status.Die);
            AttachStatus(Status.Freeze);
        }

        public void Disappear()
        {
            Disappeared = true;
            World.BroadcastRoomMsg(this, new MsgAction(this, 0, MsgAction.Action.LeaveMap));

            X = StartX;
            Y = StartY;

            Timer.Stop();

            Map.DelEntity(this);

            lock (World.AllMonsters)
            {
                if (World.AllMonsters.ContainsKey(UniqId))
                    World.AllMonsters.Remove(UniqId);
            }

            if (mGenerator != null)
            {
                --mGenerator.mAmount;
                --mGenerator.mGenAmount;
            }
        }

        public void Reborn()
        {
            Action = Emotion.StandBy;
            CurHP = MaxHP;
            DetachAllStatuses();

            World.BroadcastRoomMsg(this, new MsgPlayer(this));
            World.BroadcastRoomMsg(this, new MsgAction(this, 0, MsgAction.Action.Reborn));
            World.BroadcastRoomMsg(this, new MsgName(UniqId, "MBStandard", MsgName.NameAct.RoleEffect));
            Timer.Stop();

            Brain.Awake();
        }

        public bool DropItem(Int32 aItemType, Int32 aOwnerId)
        {
            if (aItemType == 0)
                return true;

            UInt16 x = X, y = Y;
            if (Map.FindFloorItemCell(ref x, ref y, 2)) // TODO MONSTERDROPITEM_RANGE
            {
                UInt16 dura = ItemHandler.GetMaxDura(aItemType);
                Item item = Item.Create(0, 254, aItemType, 0, 0, 0, 0, 0, 2, 0, dura, dura);

                FloorItem floorItem = new FloorItem(item, 0, aOwnerId, Map, x, y);
                World.FloorThread.AddToQueue(floorItem);
            }

            return true;
        }

        private void Drop(Int32 KillerUID)
        {
            // TODO Type == 1 and Id != 7006
            if (this.AIType != (Byte)AI_Type.Normal && this.Id != 7006)
                return;

            Player Killer = null;
            World.AllPlayers.TryGetValue(KillerUID, out Killer);

            Double Bonus = 1.0;
            if (Killer != null && Killer.LuckyTime > 0)
                Bonus = 1.2;

            //Money Drop
            if (DropMoney != 0 && MyMath.Success(50))
            {
                UInt32 Money = 0;
                SByte DropTimes = 1;
                if (MyMath.Success(50))
                    DropTimes = (SByte)MyMath.Generate(1, 3);

                for (SByte i = 0; i < DropTimes; i++)
                {
                    Money = (UInt32)MyMath.Generate((int)(DropMoney * Bonus),
                        (int)(DropMoney * 2 * Bonus));

                    if (MyMath.Success(90))
                        Money = (UInt32)MyMath.Generate(DropMoney, DropMoney * 3);
                    if (MyMath.Success(70))
                        Money = (UInt32)MyMath.Generate(DropMoney, DropMoney * 5);
                    if (MyMath.Success(50))
                        Money = (UInt32)MyMath.Generate(DropMoney, DropMoney * 7);
                    if (MyMath.Success(30))
                        Money = (UInt32)MyMath.Generate(DropMoney, DropMoney * 8);
                    if (MyMath.Success(15))
                        Money = (UInt32)MyMath.Generate(DropMoney, DropMoney * 10);

                    Item Item = null;
                    if (Money <= 10) //Silver
                        Item = Item.CreateMoney(1090000);
                    else if (Money <= 100) //Sycee
                        Item = Item.CreateMoney(1090010);
                    else if (Money <= 1000) //Gold
                        Item = Item.CreateMoney(1090020);
                    else if (Money <= 2000) //GoldBullion
                        Item = Item.CreateMoney(1091000);
                    else if (Money <= 5000) //GoldBar
                        Item = Item.CreateMoney(1091010);
                    else if (Money > 5000) //GoldBars
                        Item = Item.CreateMoney(1091020);
                    else //Error
                        Item = Item.CreateMoney(1090000);

                    UInt16 ItemX = (UInt16)(X + MyMath.Generate(-1, 1));
                    UInt16 ItemY = (UInt16)(Y + MyMath.Generate(-1, 1));

                    for (SByte Try = 0; Try < 5; Try++)
                    {
                        if (Map.GetFloorAccess(ItemX, ItemY))
                        {
                            FloorItem FloorItem = new FloorItem(Item, Money, KillerUID, Map, ItemX, ItemY);
                            World.FloorThread.AddToQueue(FloorItem);
                            break;
                        }
                        else
                        {
                            ItemX = (UInt16)(X + MyMath.Generate(-1, 1));
                            ItemY = (UInt16)(Y + MyMath.Generate(-1, 1));
                        }
                    }
                }
            }

            //Potion Drop
            if (DropHP != 0 && MyMath.Success(5))
            {
                Item Item = Item.Create(0, 254, DropHP, 0, 0, 0, 0, 0, 2, 0, ItemHandler.GetMaxDura(DropHP), ItemHandler.GetMaxDura(DropHP));

                UInt16 ItemX = (UInt16)(X + MyMath.Generate(-1, 1));
                UInt16 ItemY = (UInt16)(Y + MyMath.Generate(-1, 1));

                for (SByte Try = 0; Try < 5; Try++)
                {
                    if (Map.GetFloorAccess(ItemX, ItemY))
                    {
                        FloorItem FloorItem = new FloorItem(Item, 0, KillerUID, Map, ItemX, ItemY);
                        World.FloorThread.AddToQueue(FloorItem);
                        break;
                    }
                    else
                    {
                        ItemX = (UInt16)(X + MyMath.Generate(-1, 1));
                        ItemY = (UInt16)(Y + MyMath.Generate(-1, 1));
                    }
                }
            }
            if (DropMP != 0 && MyMath.Success(5))
            {
                Item Item = Item.Create(0, 254, DropMP, 0, 0, 0, 0, 0, 2, 0, ItemHandler.GetMaxDura(DropMP), ItemHandler.GetMaxDura(DropMP));

                UInt16 ItemX = (UInt16)(X + MyMath.Generate(-1, 1));
                UInt16 ItemY = (UInt16)(Y + MyMath.Generate(-1, 1));

                for (SByte Try = 0; Try < 5; Try++)
                {
                    if (Map.GetFloorAccess(ItemX, ItemY))
                    {
                        FloorItem FloorItem = new FloorItem(Item, 0, KillerUID, Map, ItemX, ItemY);
                        World.FloorThread.AddToQueue(FloorItem);
                        break;
                    }
                    else
                    {
                        ItemX = (UInt16)(X + MyMath.Generate(-1, 1));
                        ItemY = (UInt16)(Y + MyMath.Generate(-1, 1));
                    }
                }
            }

            //Item Drop
            //Drop Meteor
            double meteorRate = 0.072 * ((double)(Level + 25) / 17.00);
            if (MyMath.Success(meteorRate * Bonus))
            {
                Item Item = Item.Create(0, 254, 1088001, 0, 0, 0, 0, 0, 2, 0, ItemHandler.GetMaxDura(1088001), ItemHandler.GetMaxDura(1088001));

                UInt16 ItemX = (UInt16)(X + MyMath.Generate(-1, 1));
                UInt16 ItemY = (UInt16)(Y + MyMath.Generate(-1, 1));

                for (SByte Try = 0; Try < 5; Try++)
                {
                    if (Map.GetFloorAccess(ItemX, ItemY))
                    {
                        FloorItem FloorItem = new FloorItem(Item, 0, KillerUID, Map, ItemX, ItemY);
                        World.FloorThread.AddToQueue(FloorItem);
                        break;
                    }
                    else
                    {
                        ItemX = (UInt16)(X + MyMath.Generate(-1, 1));
                        ItemY = (UInt16)(Y + MyMath.Generate(-1, 1));
                    }
                }
            }

            //Drop DragonBall
            double dragonBallRate = 0.00356 * ((double)(Level + 25) / 17.00);
            if (MyMath.Success(dragonBallRate * Bonus))
            {
                Item Item = Item.Create(0, 254, 1088000, 0, 0, 0, 0, 0, 2, 0, ItemHandler.GetMaxDura(1088000), ItemHandler.GetMaxDura(1088000));

                UInt16 ItemX = (UInt16)(X + MyMath.Generate(-1, 1));
                UInt16 ItemY = (UInt16)(Y + MyMath.Generate(-1, 1));

                for (SByte Try = 0; Try < 5; Try++)
                {
                    if (Map.GetFloorAccess(ItemX, ItemY))
                    {
                        FloorItem FloorItem = new FloorItem(Item, 0, KillerUID, Map, ItemX, ItemY);
                        World.FloorThread.AddToQueue(FloorItem);

                        if (Killer != null)
                            World.BroadcastMsg(new MsgTalk("SYSTEM", "ALLUSERS", String.Format(StrRes.STR_GOT_DRAGONBALL, Killer.Name), Channel.System, Color.Red));
                        break;
                    }
                    else
                    {
                        ItemX = (UInt16)(X + MyMath.Generate(-1, 1));
                        ItemY = (UInt16)(Y + MyMath.Generate(-1, 1));
                    }
                }
            }

            //Drop Equip
            if (MyMath.Success(60))
            {
                byte quality = 0;
                uint value = (uint)MyMath.Generate(DropMoney, DropMoney * 2);

                int rate = (int)MyMath.Generate(0, 100);
                if (rate < 1) // 1% got super
                    quality = 9;
                else if (rate < 3) // 3% got elite
                    quality = 8;
                else if (rate < 7) // 7% got unique
                    quality = 7;
                else if (rate < 15) // 15% got refined
                    quality = 6;

                Item Item = null;
                if (Item.Create(out Item, value, this, quality))
                {
                    UInt16 ItemX = (UInt16)(X + MyMath.Generate(-1, 1));
                    UInt16 ItemY = (UInt16)(Y + MyMath.Generate(-1, 1));

                    for (SByte Try = 0; Try < 5; Try++)
                    {
                        if (Map.GetFloorAccess(ItemX, ItemY))
                        {
                            FloorItem FloorItem = new FloorItem(Item, 0, KillerUID, Map, ItemX, ItemY);
                            World.FloorThread.AddToQueue(FloorItem);
                            break;
                        }
                        else
                        {
                            ItemX = (UInt16)(X + MyMath.Generate(-1, 1));
                            ItemY = (UInt16)(Y + MyMath.Generate(-1, 1));
                        }
                    }
                }
            }
        }

        private void Process(Object sender, ElapsedEventArgs e)
        {
            try
            {
                if (LastKillerUID != -1)
                {
                    Drop(LastKillerUID);

                    if (mOnDie != null) // TODO execute OnDie() if no killer...
                    {
                        Player killer = null;
                        if (World.AllPlayers.TryGetValue(LastKillerUID, out killer))
                            mOnDie.Execute(killer.Client, new object[] { this });
                    }

                    LastKillerUID = -1;
                }

                if (LastDieTime != -1 && !Disappeared && Environment.TickCount - LastDieTime >= 3000)
                    Disappear();
            }
            catch (Exception exc) { Console.WriteLine(exc); }
        }

        public Boolean IsGreen(AdvancedEntity Entity) { return (Entity.Level - Level) >= 3; }
        public Boolean IsWhite(AdvancedEntity Entity) { return (Entity.Level - Level) >= 0 && (Entity.Level - Level) < 3; }
        public Boolean IsRed(AdvancedEntity Entity) { return (Entity.Level - Level) >= -4 && (Entity.Level - Level) < 0; }
        public Boolean IsBlack(AdvancedEntity Entity) { return (Entity.Level - Level) < -4; }
    }
}
