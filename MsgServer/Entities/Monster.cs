// * Created by Jean-Philippe Boivin
// * Copyright © 2010-2011
// * Logik. Project

using System;
using System.Timers;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using COServer.Network;

namespace COServer.Entities
{
    public enum MonsterType
    {
        Normal = 0,
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct MonsterInfo
    {
        public Int32 Id;
        public String Name;
        public Byte Type;
        public Int16 Look;
        public Int16 Level;
        public Int32 Life;
        public Int32 MinAtk;
        public Int32 MaxAtk;
        public Int32 Defence;
        public Byte Dodge;
        public Byte AtkRange;
        public Byte ViewRange;
        public Int32 AtkSpeed;
        public Int32 MoveSpeed;
        public UInt16 MagicType;
        public UInt16 MagicDef;
        public Int32 DropMoney;
        public Int32 DropHP;
        public Int32 DropMP;
        public Int16 BattleLvl;

        public List<Drop> Drops;
    }

    public struct Rate
    {
        public Double Refined;
        public Double Unique;
        public Double Elite;
        public Double Super;
        public Double Craft;
        public Double Meteor;
        public Double DragonBall;

        public void Default()
        {
            Refined = 0.154;
            Unique = 0.096;
            Elite = 0.0192;
            Super = 0.0048;
            Craft = 0.0337;
            Meteor = 0.072;
            DragonBall = 0.00356;
        }
    }

    public struct Drop
    {
        public Int32 Id;
        public Double Rate;
    }

    public partial class Monster : AdvancedEntity
    {
        public Monster.AI Brain = null;
        private Timer Timer = null;
        private Int32 LastKillerUID = -1;

        public Int16 Id = -1;
        public SByte Type = -1;

        public UInt16 StartX;
        public UInt16 StartY;

        public Byte ViewRange;
        public Int32 MoveSpeed;
        public Int16 BattleLevel;

        public Int32 DropMoney;
        public Int32 DropHP;
        public Int32 DropMP;
        public Rate Rate;
        public List<Drop> Drops;

        private Int32 RespawnSpeed = 20000;
        private Boolean Disappeared = false;

        public Int32 LastDieTime;

        public Monster(Int32 UniqId, MonsterInfo Info, Int32 RespawnSpeed)
            : base(UniqId)
        {
            this.Id = (Int16)Info.Id;
            this.Name = Info.Name;
            this.Type = (SByte)Info.Type;
            this.Look = (UInt32)Info.Look;
            this.Level = Info.Level;
            this.CurHP = Info.Life;
            this.MaxHP = Info.Life;

            this.Dexterity = 85;
            this.MinAtk = Math.Min(Info.MinAtk , Info.MaxAtk);
            this.MaxAtk = Math.Max(Info.MinAtk, Info.MaxAtk);
            this.Defence = (UInt16)Info.Defence;
            this.Dodge = Info.Dodge;
            this.AtkRange = Info.AtkRange;
            this.ViewRange = Info.ViewRange;
            this.AtkSpeed = Info.AtkSpeed;
            this.MoveSpeed = Info.MoveSpeed;
            this.BattleLevel = Info.BattleLvl;

            this.MagicDef = Info.MagicDef;
            this.MagicAtk = Info.MaxAtk;
            this.MagicType = (Int16)Info.MagicType;
            this.MagicLevel = 0;

            if (Info.MagicType != 0)
                this.AtkType = 21;
            else
                this.AtkType = 2;

            this.DropMoney = Info.DropMoney;
            this.DropHP = Info.DropHP;
            this.DropMP = Info.DropMP;
            this.Drops = Info.Drops;

            Rate.Default();
            Rate.Refined = Rate.Refined * ((Double)(Level + 25) / 17.00);
            Rate.Unique = Rate.Unique * ((Double)(Level + 25) / 17.00);
            Rate.Elite = Rate.Elite * ((Double)(Level + 25) / 17.00);
            Rate.Super = Rate.Super * ((Double)(Level + 25) / 17.00);
            Rate.Craft = Rate.Craft * ((Double)(Level + 25) / 17.00);
            Rate.Meteor = Rate.Meteor * ((Double)(Level + 25) / 17.00);
            Rate.DragonBall = Rate.DragonBall * ((Double)(Level + 25) / 17.00);

            this.RespawnSpeed = RespawnSpeed;
            this.Disappeared = false;

            this.LastDieTime = -1;

            this.Timer = new Timer();
            this.Timer.Interval = 500;
            this.Timer.Elapsed += new ElapsedEventHandler(Process);

            this.Brain = new Monster.AI(this, 500, MoveSpeed, AtkSpeed, ViewRange, 5, AtkRange);
        }

        ~Monster()
        {
            this.Timer = null;
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

            Flags = (Int64)Monster.Flag.None;
            AddFlag(Monster.Flag.Die);
            AddFlag(Monster.Flag.Frozen);
            World.BroadcastRoomMsg(this, MsgUserAttrib.Create(this, Flags, MsgUserAttrib.Type.Flags));
        }

        public void Disappear()
        {
            Disappeared = true;
            World.BroadcastRoomMsg(this, MsgAction.Create(this, 0, MsgAction.Action.LeaveMap));

            X = StartX;
            Y = StartY;

            if (RespawnSpeed == -1)
            {
                Timer.Stop();

                Map CMap = World.AllMaps[Map];
                CMap.DelEntity(this);

                lock (World.AllMonsters)
                {
                    if (World.AllMonsters.ContainsKey(UniqId))
                        World.AllMonsters.Remove(UniqId);
                }
            }
        }

        public void Reborn()
        {
            AccuracyEndTime = 0;
            StarOfAccuracyEndTime = 0;
            ShieldEndTime = 0;
            MagicShieldEndTime = 0;
            StigmaEndTime = 0;
            InvisibilityEndTime = 0;
            SupermanEndTime = 0;
            CycloneEndTime = 0;
            FlyEndTime = 0;
            DodgeEndTime = 0;

            DexterityBonus = 1;
            DefenceBonus = 1;
            AttackBonus = 1;
            DodgeBonus = 1;
            SpeedBonus = 1;

            Action = (Int16)MsgAction.Emotion.StandBy;
            CurHP = MaxHP;
            Flags = (Int64)Monster.Flag.None;
            
            World.BroadcastRoomMsg(this, MsgPlayer.Create(this));
            //World.BroadcastRoomMsg(this, MsgAction.Create(this, 0, MsgAction.Action.Reborn));
            World.BroadcastRoomMsg(this, MsgName.Create(UniqId, "MBStandard", MsgName.Action.RoleEffect));
            Timer.Stop();

            Brain.Awake();
        }

        private void Drop(Int32 KillerUID)
        {
            if (this.Type != 1 && this.Id != 7006)
                return;

            Map CMap = null;
            if (!World.AllMaps.TryGetValue(Map, out CMap))
                return;

            Player Killer = null;
            World.AllPlayers.TryGetValue(KillerUID, out Killer);

            Double Bonus = 1.0;
            if (Killer != null && Killer.LuckyTime > 0)
                Bonus = 1.2;

            //Money Drop
            if (DropMoney != 0 && MyMath.Success(50))
            {
                Int32 Money = 0;
                SByte DropTimes = 1;
                if (MyMath.Success(50))
                    DropTimes = (SByte)MyMath.Generate(1, 3);

                for (SByte i = 0; i < DropTimes; i++)
                {
                    Money = MyMath.Generate((Int32)(DropMoney * Bonus * Database.Rates.Money), 
                        (Int32)(DropMoney * 2 * Bonus * Database.Rates.Money));

                    if (MyMath.Success(90))
                        Money = MyMath.Generate(DropMoney, DropMoney * 3);
                    if (MyMath.Success(70))
                        Money  = MyMath.Generate(DropMoney, DropMoney * 5);
                    if (MyMath.Success(50))
                        Money = MyMath.Generate(DropMoney, DropMoney * 7);
                    if (MyMath.Success(30))
                        Money = MyMath.Generate(DropMoney, DropMoney * 8);
                    if (MyMath.Success(15))
                        Money = MyMath.Generate(DropMoney, DropMoney * 10);

                    Item Item = null;
                    if (Money <= 10) //Silver
                        Item = Item.Create(0, 254, 1090000, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                    else if (Money <= 100) //Sycee
                        Item = Item.Create(0, 254, 1090010, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                    else if (Money <= 1000) //Gold
                        Item = Item.Create(0, 254, 1090020, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                    else if (Money <= 2000) //GoldBullion
                        Item = Item.Create(0, 254, 1091000, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                    else if (Money <= 5000) //GoldBar
                        Item = Item.Create(0, 254, 1091010, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                    else if (Money > 5000) //GoldBars
                        Item = Item.Create(0, 254, 1091020, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                    else //Error
                        Item = Item.Create(0, 254, 1090000, 0, 0, 0, 0, 0, 0, 0, 0, 0);

                    if (Killer == null || !Killer.AutoLoot)
                    {
                        UInt16 ItemX = (UInt16)(X + MyMath.Generate(-1, 1));
                        UInt16 ItemY = (UInt16)(Y + MyMath.Generate(-1, 1));

                        for (SByte Try = 0; Try < 5; Try++)
                        {
                            if (CMap.IsValidPoint(ItemX, ItemY))
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
                    else
                    {
                        Killer.Money += Money;
                        Killer.Send(MsgUserAttrib.Create(Killer, Killer.Money, MsgUserAttrib.Type.Money));
                    }
                }
            }

            //CPs Drop
            if (Killer != null && Database.Rates.CPs > 0)
            {
                if (MyMath.Success(7.5 * Bonus))
                    Killer.CPs += (Int32)(3.0 * Database.Rates.CPs);
                if (MyMath.Success(5.0 * Bonus))
                    Killer.CPs += (Int32)(5.0 * Database.Rates.CPs);
                if (MyMath.Success(2.5 * Bonus))
                    Killer.CPs += (Int32)(10.0 * Database.Rates.CPs);
                Killer.Send(MsgUserAttrib.Create(Killer, Killer.CPs, MsgUserAttrib.Type.CPs));
            }

            //Potion Drop
            if (DropHP != 0 && MyMath.Success(5))
            {
                Item Item = Item.Create(0, 254, DropHP, 0, 0, 0, 0, 0, 2, 0, ItemHandler.GetMaxDura(DropHP), ItemHandler.GetMaxDura(DropHP));

                UInt16 ItemX = (UInt16)(X + MyMath.Generate(-1, 1));
                UInt16 ItemY = (UInt16)(Y + MyMath.Generate(-1, 1));

                for (SByte Try = 0; Try < 5; Try++)
                {
                    if (CMap.IsValidPoint(ItemX, ItemY))
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
                    if (CMap.IsValidPoint(ItemX, ItemY))
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
            if (MyMath.Success(Rate.Meteor * Bonus * Database.Rates.Meteor) && (Killer == null || !Killer.AutoLoot))
            {
                Item Item = Item.Create(0, 254, 1088001, 0, 0, 0, 0, 0, 2, 0, ItemHandler.GetMaxDura(1088001), ItemHandler.GetMaxDura(1088001));

                UInt16 ItemX = (UInt16)(X + MyMath.Generate(-1, 1));
                UInt16 ItemY = (UInt16)(Y + MyMath.Generate(-1, 1));

                for (SByte Try = 0; Try < 5; Try++)
                {
                    if (CMap.IsValidPoint(ItemX, ItemY))
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
            if (MyMath.Success(Rate.DragonBall * Bonus * Database.Rates.DragonBall))
            {
                Item Item = Item.Create(0, 254, 1088000, 0, 0, 0, 0, 0, 2, 0, ItemHandler.GetMaxDura(1088000), ItemHandler.GetMaxDura(1088000));

                UInt16 ItemX = (UInt16)(X + MyMath.Generate(-1, 1));
                UInt16 ItemY = (UInt16)(Y + MyMath.Generate(-1, 1));

                for (SByte Try = 0; Try < 5; Try++)
                {
                    if (CMap.IsValidPoint(ItemX, ItemY))
                    {
                        FloorItem FloorItem = new FloorItem(Item, 0, KillerUID, Map, ItemX, ItemY);
                        World.FloorThread.AddToQueue(FloorItem);

                        if (Killer != null)
                            World.BroadcastMsg(MsgTalk.Create("SYSTEM", "ALLUSERS", String.Format(STR.Get("STR_GOT_DRAGONBALL"), Killer.Name), MsgTalk.Channel.System, 0xFF0000));
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
            if (MyMath.Success(60) && (Killer == null || !Killer.AutoLoot))
            {
                Int32 Id = ItemHandler.GenerateId((Byte)Level);
                if (Id != 0)
                {
                    Byte Craft = 0;
                    Byte Bless = 0;
                    Byte Gem1 = 0;
                    Byte Gem2 = 0;

                    if (MyMath.Success(Rate.Refined * Bonus * Database.Rates.Refined))
                        Id = ((Id - (Id % 10)) + 6);

                    if (MyMath.Success(Rate.Unique * Bonus * Database.Rates.Unique))
                        Id = ((Id - (Id % 10)) + 7);

                    if (MyMath.Success(Rate.Elite * Bonus * Database.Rates.Elite))
                        Id = ((Id - (Id % 10)) + 8);

                    if (MyMath.Success(Rate.Super * Bonus * Database.Rates.Super))
                        Id = ((Id - (Id % 10)) + 9);

                    if (MyMath.Success(Rate.Craft * Bonus * Database.Rates.Craft))
                        Craft = 1;

                    if ((Byte)(Id / 100000) == 4 || (Byte)(Id / 100000) == 5)
                    {
                        if (MyMath.Success(50.00 / (Double)(Id % 10)))
                        {
                            Gem1 = 255;
                            if (MyMath.Success(25.00 / (Double)(Id % 10)))
                                Gem2 = 255;
                        }
                    }

                    if (MyMath.Success(0.5))
                        Bless = (Byte)MyMath.Generate(1, 5);

                    if (Bless == 2 || Bless == 4)
                        Bless = 3;

                    UInt16 MaxDura = ItemHandler.GetMaxDura(Id);
                    UInt16 CurDura = (UInt16)((Double)MaxDura * ((Double)MyMath.Generate(1, 100) / 100.00));

                    Item Item = Item.Create(0, 254, Id, Craft, Bless, 0, Gem1, Gem2, 2, 0, CurDura, MaxDura);

                    UInt16 ItemX = (UInt16)(X + MyMath.Generate(-1, 1));
                    UInt16 ItemY = (UInt16)(Y + MyMath.Generate(-1, 1));

                    for (SByte Try = 0; Try < 5; Try++)
                    {
                        if (CMap.IsValidPoint(ItemX, ItemY))
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

            //Drop Special Item
            if (Drops.Count > 0)
            {
                foreach (Drop Drop in Drops)
                {
                    if (!MyMath.Success(Drop.Rate * Bonus))
                        continue;

                    Int32 ItemId = Drop.Id;

                    UInt16 MaxDura = ItemHandler.GetMaxDura(ItemId);

                    Item Item = Item.Create(0, 254, ItemId, 0, 0, 0, 0, 0, 2, 0, MaxDura, MaxDura);

                    if (Killer == null || !Killer.AutoLoot)
                    {
                        UInt16 ItemX = (UInt16)(X + MyMath.Generate(-1, 1));
                        UInt16 ItemY = (UInt16)(Y + MyMath.Generate(-1, 1));

                        for (SByte Try = 0; Try < 5; Try++)
                        {
                            if (CMap.IsValidPoint(ItemX, ItemY))
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
                    else
                    {
                        if (Killer.ItemInInventory() < 40)
                            Killer.AddItem(Item, true);
                    }
                }
            }

            //LuckyDrop
            if (Killer != null && Killer.LuckyTime > 0)
                LuckyDrop(Killer);
        }

        private void LuckyDrop(Player Killer)
        {
            if (Killer.ItemInInventory() < 40)
            {
                if (MyMath.Success(0.005))
                {
                    World.BroadcastRoomMsg(Killer, MsgName.Create(Killer.UniqId, "LuckyGuy", MsgName.Action.RoleEffect), true);
                    World.BroadcastMsg(MsgTalk.Create("SYSTEM", "ALLUSERS", "Parfait! " + Killer.Name + " a obtenu une pierre +4!", MsgTalk.Channel.GM, 0xFFFFFF));
                    Killer.AddItem(Item.Create(Killer.UniqId, 0, 730004, 4, 0, 0, 0, 0, 2, 0, 1, 1), true);
                    return;
                }
                if (MyMath.Success(0.005))
                {
                    World.BroadcastRoomMsg(Killer, MsgName.Create(Killer.UniqId, "LuckyGuy", MsgName.Action.RoleEffect), true);
                    World.BroadcastMsg(MsgTalk.Create("SYSTEM", "ALLUSERS", "Parfait! " + Killer.Name + " a obtenu une pierre +5!", MsgTalk.Channel.GM, 0xFFFFFF));
                    Killer.AddItem(Item.Create(Killer.UniqId, 0, 730005, 5, 0, 0, 0, 0, 2, 0, 1, 1), true);
                    return;
                }
                if (MyMath.Success(0.0025))
                {
                    World.BroadcastRoomMsg(Killer, MsgName.Create(Killer.UniqId, "LuckyGuy", MsgName.Action.RoleEffect), true);
                    World.BroadcastMsg(MsgTalk.Create("SYSTEM", "ALLUSERS", "Parfait! " + Killer.Name + " a obtenu une pierre +6!", MsgTalk.Channel.GM, 0xFFFFFF));
                    Killer.AddItem(Item.Create(Killer.UniqId, 0, 730006, 6, 0, 0, 0, 0, 2, 0, 1, 1), true);
                    return;
                }
                if (MyMath.Success(0.00075))
                {
                    World.BroadcastRoomMsg(Killer, MsgName.Create(Killer.UniqId, "LuckyGuy", MsgName.Action.RoleEffect), true);
                    World.BroadcastMsg(MsgTalk.Create("SYSTEM", "ALLUSERS", "Parfait! " + Killer.Name + " a obtenu une pierre +7!", MsgTalk.Channel.GM, 0xFFFFFF));
                    Killer.AddItem(Item.Create(Killer.UniqId, 0, 730007, 7, 0, 0, 0, 0, 2, 0, 1, 1), true);
                    return;
                }
                if (MyMath.Success(0.0005))
                {
                    World.BroadcastRoomMsg(Killer, MsgName.Create(Killer.UniqId, "LuckyGuy", MsgName.Action.RoleEffect), true);
                    World.BroadcastMsg(MsgTalk.Create("SYSTEM", "ALLUSERS", "Parfait! " + Killer.Name + " a obtenu une pierre +8!", MsgTalk.Channel.GM, 0xFFFFFF));
                    Killer.AddItem(Item.Create(Killer.UniqId, 0, 730008, 8, 0, 0, 0, 0, 2, 0, 1, 1), true);
                    return;
                }
                if (MyMath.Success(0.0005))
                {
                    World.BroadcastRoomMsg(Killer, MsgName.Create(Killer.UniqId, "LuckyGuy", MsgName.Action.RoleEffect), true);
                    World.BroadcastMsg(MsgTalk.Create("SYSTEM", "ALLUSERS", "Parfait! " + Killer.Name + " a obtenu une foreuse de diamant!", MsgTalk.Channel.GM, 0xFFFFFF));
                    Killer.AddItem(Item.Create(Killer.UniqId, 0, 1200005, 0, 0, 0, 0, 0, 2, 0, 1, 1), true);
                    return;
                }
                if (MyMath.Success(0.0005))
                {
                    World.BroadcastRoomMsg(Killer, MsgName.Create(Killer.UniqId, "LuckyGuy", MsgName.Action.RoleEffect), true);
                    World.BroadcastMsg(MsgTalk.Create("SYSTEM", "ALLUSERS", "Parfait! " + Killer.Name + " a obtenu une balle exp de force!", MsgTalk.Channel.GM, 0xFFFFFF));
                    Killer.AddItem(Item.Create(Killer.UniqId, 0, 722057, 0, 0, 0, 0, 0, 2, 0, 1, 1), true);
                    return;
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
                    LastKillerUID = -1;
                }

                if (LastDieTime != -1 && !Disappeared && Environment.TickCount - LastDieTime >= 3000)
                    Disappear();

                if (LastDieTime != -1 && Disappeared && Environment.TickCount - LastDieTime >= RespawnSpeed)
                    Reborn();
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }

        public Boolean IsGreen(AdvancedEntity Entity) { return (Entity.Level - Level) >= 3; }
        public Boolean IsWhite(AdvancedEntity Entity) { return (Entity.Level - Level) >= 0 && (Entity.Level - Level) < 3; }
        public Boolean IsRed(AdvancedEntity Entity) { return (Entity.Level - Level) >= -4 && (Entity.Level - Level) < 0; }
        public Boolean IsBlack(AdvancedEntity Entity) { return (Entity.Level - Level) < -4; }
    }
}
