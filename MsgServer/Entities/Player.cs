// * Created by Jean-Philippe Boivin
// * Copyright © 2010-2011
// * Logik. Project

using System;
using System.Collections;
using System.Collections.Generic;
using COServer.Games;
using COServer.Network;
using COServer.Threads;
using CO2_CORE_DLL.IO;

namespace COServer.Entities
{
    public class Player : AdvancedEntity
    {
        public const Int32 _MAX_MONEYLIMIT = 1000000000;

        public Client Owner;
        public Screen Screen;
        public PlayerThread Thread;

        public String Spouse = "Non";
        public Byte Profession;
        public UInt64 Exp;
        public UInt16 Strength;
        public UInt16 Agility;
        public UInt16 Vitality;
        public UInt16 Spirit;
        public UInt16 AddPoints;
        public Int32 Money;
        public Int32 CPs;
        public UInt16 CurMP;
        public UInt16 MaxMP;

        public Int16 Hair;
        public Byte Metempsychosis;
        public Int32 VPs;
        public Int16 PkPoints;
        public Byte PkMode;
        public Byte Energy;
        public Byte XP;

        public Int16 Potency;

        public Int32 WHMoney;
        public Int32 WHPIN;

        public Int32 KO;
        public Int32 CurKO;
        public Int32 TimeAdd;

        public Int32 DisKO = 0;
        public Int32 DisToKill = 0;

        public Byte FirstLevel;
        public Byte SecondLevel;
        public Byte FirstProfession;
        public Byte SecondProfession;

        public Double ExpBonus;
        public Double MagicBonus;
        public Double WeaponSkillBonus;

        public Boolean AutoAllot;

        public Byte TrainingPoints;

        public Int32 LastXPAdd;
        public Int32 LastTrainingPointAdd;
        public Int32 LastTrainingTimeAdd = 0;
        public Int32 LastPkPointRemove;
        public Int32 LastCoolShow;
        public Int32 LastSave;
        public Int32 LastJumpTick;
        public Int32 LastPickSwingTick;
        public Int32 LastLeaderPosTick;
        public Int32 LastAttackTick;
        public Int32 LastClientTick;
        public Int32 LastRcvClientTick;
        public Int32 LastReqClientTick;
        public Int32 LastDieTick;

        public Int32 DblExpEndTime;
        public Int32 TransformEndTime;
        public Int32 BlueNameEndTime;
        public Int64 BlessEndTime;
        public Int32 CurseEndTime;

        public Dictionary<Int32, Item> Items;
        public Dictionary<Int32, WeaponSkill> WeaponSkills;
        public Dictionary<Int32, Magic> Magics;
        public Dictionary<Int32, String> Friends;
        public Dictionary<Int32, String> Enemies;
		public Team Team;
        public Syndicate.Info Syndicate;
        public Nobility.Info Nobility;

        public Deal Deal;
        public Booth Booth;
        public CageMatch Game;

        public Int32 TradeRequest;
        public Int32 FriendRequest;
        public Int32 GameRequest;

        public Boolean Mining;

        public Int32 PrevSpeedHackReset = Environment.TickCount;
        public Int32 PrevAtkInterval;
        public Int32 BotCheck = 0;
        public Int32 SpeedHack = 0;

        public DateTime PremiumEndTime;

        //Agreed ToS
        public Boolean ToS = false;
        public Int32 ConnectionTime;

        public Int32 JailC;

        //Cheat
        public Boolean AutoLoot;
        public Boolean AutoMine;
        public Boolean AutoKill;

        public Int64 TrainingTicks = 0;
        public Int32 MaxTrainingTime = 0;

        public const Int32 MAX_LUCKY_TIME = 2 * 60 * 60 * 1000;
        public Int32 LuckyTime = 0;
        public Boolean Praying;
        public Boolean CastingPray;
        public Int16 PrayMap;
        public UInt16 PrayX;
        public UInt16 PrayY;

        public Player(Int32 UniqId, Client Owner)
            : base(UniqId)
        {
            this.AutoAllot = true;

            this.LastXPAdd = Environment.TickCount;
            this.LastTrainingPointAdd = Environment.TickCount;
            this.LastPkPointRemove = Environment.TickCount;
            this.LastCoolShow = Environment.TickCount;
            this.LastSave = Environment.TickCount + 30000;
            this.LastClientTick = 0;
            this.LastRcvClientTick = Environment.TickCount;
            this.LastReqClientTick = Environment.TickCount;
            this.LastJumpTick = Environment.TickCount;
            this.LastPickSwingTick = Environment.TickCount;
            this.LastAttackTick = Environment.TickCount;
            this.LastDieTick = Environment.TickCount;

            this.ConnectionTime = Environment.TickCount;

            this.DblExpEndTime = 0;
            this.TransformEndTime = 0;
            this.BlueNameEndTime = 0;
            this.BlessEndTime = 0;
            this.CurseEndTime = 0;

            this.Owner = Owner;
            this.Screen = new Screen(this);
            this.Thread = new PlayerThread(this);

            this.Items = new Dictionary<Int32, Item>();
            this.WeaponSkills = new Dictionary<Int32, WeaponSkill>();
            this.Magics = new Dictionary<Int32, Magic>();
            this.Friends = new Dictionary<Int32, String>();
            this.Enemies = new Dictionary<Int32, String>();
            this.Nobility = new Nobility.Info();
        }

        ~Player()
        {
            Owner = null;
            Screen = null;
            Thread = null;

            Items = null;
            WeaponSkills = null;
            Magics = null;
            Friends = null;
            Enemies = null;
			Team = null;
            Syndicate = null;

            Deal = null;
            Booth = null;
            Game = null;
        }

        public void SetDefaultFlag()
        {
            Flags = (Int64)Player.Flag.None;
            if (PkPoints >= 30 && PkPoints < 100)
                AddFlag(Player.Flag.RedName);
            else if (PkPoints >= 100)
                AddFlag(Player.Flag.BlackName);

            if (Team != null && Team.Leader == this)
                AddFlag(Player.Flag.TeamLeader);

            switch (Metempsychosis)
            {
                case 3:
                    AddFlag(Player.Flag.ThirdMetempsychosis);
                    break;
                case 4:
                    AddFlag(Player.Flag.FourthMetempsychosis);
                    break;
                case 5:
                    AddFlag(Player.Flag.FifthMetempsychosis);
                    break;
                case 6:
                    AddFlag(Player.Flag.SixthMetempsychosis);
                    break;
                case 7:
                    AddFlag(Player.Flag.SeventhMetempsychosis);
                    break;
                case 8:
                    AddFlag(Player.Flag.EighthMetempsychosis);
                    break;
                case 9:
                    AddFlag(Player.Flag.NinthMetempsychosis);
                    break;
                case 10:
                    AddFlag(Player.Flag.TenthMetempsychosis);
                    break;
            }
        }

        public Boolean IsMan() { return (Look - ((Int32)(Look / 10000) * 10000)) / 1000 == 1; }
        public Boolean IsWoman() { return (Look - ((Int32)(Look / 10000) * 10000)) / 1000 == 2; }

        public void Die()
        {
            if (Game != null)
                Game.PlayerDie(this);

            if (Level > 15)
            {
                Map CMap = World.AllMaps[Map];
                if (!CMap.IsPkField() && !CMap.IsSynMap() && !CMap.IsPrisonMap())
                {
                    DropMoney();
                    DropInventory();

                    if (PkPoints >= 30)
                        DropEquipment();
                }
            }

            CurHP = 0;
            Send(MsgUserAttrib.Create(this, CurHP, MsgUserAttrib.Type.Life));
            if (Team != null)
                World.BroadcastTeamMsg(Team, MsgUserAttrib.Create(this, CurHP, MsgUserAttrib.Type.Life));

            SetDefaultFlag();
            AddFlag(Player.Flag.Die);
            AddFlag(Player.Flag.Frozen);
            World.BroadcastRoomMsg(this, MsgUserAttrib.Create(this, Flags, MsgUserAttrib.Type.Flags), true);

            if (TransformEndTime != 0)
            {
                MyMath.GetHitPoints(this, true);
                MyMath.GetMagicPoints(this, true);
            }

            TransformEndTime = 0;
            BlueNameEndTime = 0;
            Mining = false;

            XP = 0;
            Send(MsgUserAttrib.Create(this, XP, MsgUserAttrib.Type.XP));

            if (Look % 10000 == 2001 || Look % 10000 == 2002)
                AddTransform(99);
            else
                AddTransform(98);
            World.BroadcastRoomMsg(this, MsgUserAttrib.Create(this, Look, MsgUserAttrib.Type.Look), true);

            //Block - Revive Hack
            LastDieTick = Environment.TickCount;
        }

        public void Reborn(Boolean ChangeMap)
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
            Send(MsgUserAttrib.Create(this, CurHP, MsgUserAttrib.Type.Life));
            if (Team != null)
                World.BroadcastTeamMsg(Team, MsgUserAttrib.Create(this, CurHP, MsgUserAttrib.Type.Life));

            XP = 0;
            Send(MsgUserAttrib.Create(this, XP, MsgUserAttrib.Type.XP));
            LastXPAdd = Environment.TickCount;

            Energy = 100;
            Send(MsgUserAttrib.Create(this, Energy, MsgUserAttrib.Type.Energy));

            SetDefaultFlag();
            World.BroadcastRoomMsg(this, MsgUserAttrib.Create(this, Flags, MsgUserAttrib.Type.Flags), true);

            DelTransform();
            World.BroadcastRoomMsg(this, MsgUserAttrib.Create(this, Look, MsgUserAttrib.Type.Look), true);

            MyMath.GetEquipStats(this);
            if (ChangeMap)
            {
                Map OldMap = null;
                Map NewMap = null;

                if (!World.AllMaps.TryGetValue(Map, out OldMap))
                    return;

                if (!World.AllMaps.TryGetValue(OldMap.RebornMap, out NewMap))
                    return;

                Move(NewMap.UniqId, NewMap.PortalX, NewMap.PortalY);
            }
            World.BroadcastRoomMsg(this, MsgAction.Create(this, 0, MsgAction.Action.Reborn), true);
        }

        public UInt64 CalcExpBall(Byte Level, UInt64 CurExp, Double Amount)
        {
            if (Level >= Database.AllLevExp.Length)
                return 0;

            if (Level >= Database.AllLevTime.Length)
                return 0;

            UInt64 Exp = 0;
            Int32 Time = (Int32)(600.00 * Amount);

            if (CurExp > 0)
            {
                Double Pct = 1.00 - (Double)CurExp / (Double)Database.AllLevExp[Level];
                if (Time > (Int32)(Pct * Database.AllLevTime[Level]))
                {
                    Time -= (Int32)(Pct * Database.AllLevTime[Level]);
                    Exp += Database.AllLevExp[Level] - CurExp;
                    Level++;
                }
            }

            while (Time > Database.AllLevTime[Level])
            {
                Time -= Database.AllLevTime[Level];
                Exp += Database.AllLevExp[Level];
                Level++;

                if (Level >= Database.AllLevExp.Length)
                    return Exp;

                if (Level >= Database.AllLevTime.Length)
                    return Exp;
            }
            Exp += (UInt64)(((Double)Time / (Double)Database.AllLevTime[Level]) * (Double)Database.AllLevExp[Level]);

            return Exp;
        }

        public UInt64 AddExp(Double Value, Boolean UseBonus)
        {
            if (Level >= Database.AllLevExp.Length)
                return 0;

            if (UseBonus)
            {
                if (DblExpEndTime > 0)
                    Value *= 2.00;

                if (BlessEndTime > 0)
                    Value *= 1.20;

                if (LuckyTime > 0)
                    Value *= 1.10;

                if (CurseEndTime > 0)
                    Value *= 0.00;

                if (Metempsychosis > 1)
                    Value /= 3.00;

                Value += (Value * ((Double)Potency / 100.00));

                Value *= Database.Rates.Exp;
                Value *= ExpBonus;
            }
            Exp += (UInt64)Value;

            Boolean Leveled = false;
            if (Exp > Database.AllLevExp[Level])
                Leveled = true;

            while (Exp > Database.AllLevExp[Level])
            {
                if (Level < 70 && Team != null)
                {
                    if (Team.Leader.UniqId != UniqId && Team.Leader.Level >= 70)
                    {
                        if (Team.Leader.Map == Map)
                        {
                            Team.Leader.VPs += Database.AllLevTime[Level];
                            World.BroadcastTeamMsg(Team, MsgTalk.Create("SYSTEM", "ALLUSERS", Team.Leader.Name + " a obtenu " + Database.AllLevTime[Level] + " points de vertu.", MsgTalk.Channel.Team, 0xFFFFFF));
                        }
                    }
                }

                Exp -= Database.AllLevExp[Level];
                Level++;

                if (Metempsychosis == 0 && Level <= 120)
                    MyMath.GetLevelStats(this);
                else
                    AddPoints += 3;

                if (Level >= Database.AllLevExp.Length)
                    break;
            }

            if (Leveled)
            {
                MyMath.GetHitPoints(this, true);
                MyMath.GetMagicPoints(this, true);
                MyMath.GetPotency(this, true);

                CurHP = MaxHP;
                if (Team != null)
                {
                    World.BroadcastTeamMsg(Team, MsgUserAttrib.Create(this, CurHP, MsgUserAttrib.Type.Life));
                    World.BroadcastTeamMsg(Team, MsgUserAttrib.Create(this, MaxHP, (MsgUserAttrib.Type.Life + 1)));
                }

                Send(MsgUserAttrib.Create(this, CurHP, MsgUserAttrib.Type.Life));
                Send(MsgUserAttrib.Create(this, Strength, MsgUserAttrib.Type.Strength));
                Send(MsgUserAttrib.Create(this, Agility, MsgUserAttrib.Type.Agility));
                Send(MsgUserAttrib.Create(this, Vitality, MsgUserAttrib.Type.Vitality));
                Send(MsgUserAttrib.Create(this, Spirit, MsgUserAttrib.Type.Spirit));
                Send(MsgUserAttrib.Create(this, AddPoints, MsgUserAttrib.Type.AddPoints));
                World.BroadcastRoomMsg(this, MsgUserAttrib.Create(this, Level, MsgUserAttrib.Type.Level), true);
                World.BroadcastRoomMsg(this, MsgAction.Create(this, 0, MsgAction.Action.UpLev), true);

                if (Syndicate != null)
                {
                    Syndicate.Member Member = Syndicate.GetMemberInfo(UniqId);
                    if (Member != null)
                        Member.Level = (Byte)Level;
                }
            }
            Send(MsgUserAttrib.Create(this, (Int64)Exp, MsgUserAttrib.Type.Exp));
            return (UInt64)Value;
        }

        public void AddMagicExp(Int16 Type, Int32 Exp)
        {
            Magic Magic = GetMagicByType(Type);
            if (Magic == null)
                return;

            if (Magic.Unlearn)
            {
                Magic.Unlearn = false;
                Magic.Level = 0;
                Magic.Exp = 0;
                Send(MsgMagicInfo.Create(Magic));
            }

            MagicType.Entry Info;
            if (!Database2.AllMagics.TryGetValue((Magic.Type * 10) + Magic.Level, out Info))
                return;

            if (!Database2.AllMagics.ContainsKey((Magic.Type * 10) + Magic.Level + 1))
                return;

            if (Level < Info.LevelRequired)
                return;
 
            Exp = (Int32)(Exp * Database.Rates.Exp);
            Exp = (Int32)(Exp * MagicBonus); 

            if ((Magic.Exp + Exp >= Info.ExpRequired) || 
                (Magic.Level >= (Byte)(Magic.OldLevel / 2) && Magic.Level < Magic.OldLevel))
            {
                Magic.Level++;
                Magic.Exp = 0;
                Send(MsgMagicInfo.Create(Magic));
                return;
            }

            Magic.Exp += Exp;
            UpdateMagic(Magic);
        }

        public void AddWeaponSkillExp(Int16 Type, Int32 Exp)
        {
            WeaponSkill WeaponSkill = GetWeaponSkillByType(Type);
            if (WeaponSkill == null)
            {
                WeaponSkill = WeaponSkill.Create(UniqId, Type, 0, 0, 0, false);
                AddWeaponSkill(WeaponSkill, true);
            }

            if (WeaponSkill.Unlearn)
            {
                WeaponSkill.Unlearn = false;
                WeaponSkill.Level = 0;
                WeaponSkill.Exp = 0;
                Send(MsgWeaponSkill.Create(WeaponSkill));
            }

            if (WeaponSkill.Level >= Database.AllWeaponSkillExp.Length)
                return;

            Exp = (Int32)(Exp * Database.Rates.Exp);
            Exp = (Int32)(Exp * WeaponSkillBonus);

            if ((WeaponSkill.Exp + Exp >= Database.AllWeaponSkillExp[WeaponSkill.Level]) ||
                (WeaponSkill.Level >= (Byte)(WeaponSkill.OldLevel / 2) && WeaponSkill.Level < WeaponSkill.OldLevel))
            {
                WeaponSkill.Level++;
                WeaponSkill.Exp = 0;
                Send(MsgWeaponSkill.Create(WeaponSkill));
                return;
            }

            WeaponSkill.Exp += Exp;
            UpdateWeaponSkill(WeaponSkill);
        }

        public void DoMetempsychosis(Byte NewProfession, Boolean Bless)
        {
            Metempsychosis++;
            if (Metempsychosis == 1)
            {
                FirstLevel = (Byte)Level;
                FirstProfession = Profession;
            }
            else if (Metempsychosis == 2)
            {
                SecondLevel = (Byte)Level;
                SecondProfession = Profession;
            }
            Profession = NewProfession;

            Level = 15;
            Exp = 0;
            AutoAllot = false;

            MyMath.GetLevelStats(this);

            foreach (WeaponSkill Skill in WeaponSkills.Values)
            {
                Skill.OldLevel = Skill.Level;
                if (Skill.OldLevel < 20)
                    if (Skill.Exp >= Database.AllWeaponSkillExp[Skill.Level] / 2)
                        Skill.OldLevel++;

                Skill.Unlearn = true;

                Skill.Level = 0;
                Skill.Exp = 0;

                Send(MsgAction.Create(this, Skill.Type, MsgAction.Action.DropSkill));
            }

            List<Magic> SkillToDel = new List<Magic>();
            if (Metempsychosis == 1)
            {
                //Check skills to keep/delete
                foreach (Magic Skill in Magics.Values)
                {
                    Boolean Keep = false;
                    if (FirstProfession == 15)
                    {
                        if (NewProfession == 11)
                            Keep = true;
                        else if (NewProfession == 21 || NewProfession == 41 || NewProfession == 132 || NewProfession == 142)
                        {
                            if (Skill.Type == 1110 || Skill.Type == 1190 || Skill.Type == 1270)
                                Keep = true;
                        }
                    }
                    else if (FirstProfession == 25)
                    {
                        if (NewProfession == 11)
                        {
                            if (Skill.Type == 1040 || Skill.Type == 1320)
                                Keep = true;
                        }
                        else if (NewProfession == 21)
                            Keep = true;
                        else if (NewProfession == 41 || NewProfession == 142)
                        {
                            if (Skill.Type == 1020 || Skill.Type == 1040)
                                Keep = true;
                        }
                        else if (NewProfession == 132)
                        {
                            if (Skill.Type == 1020 || Skill.Type == 1025 || Skill.Type == 1040)
                                Keep = true;
                        }
                    }
                    else if (FirstProfession == 45)
                    {
                        if (NewProfession == 41)
                            Keep = true;
                    }
                    else if (FirstProfession == 135)
                    {
                        if (NewProfession == 11 || NewProfession == 21)
                        {
                            if (Skill.Type == 1000 || Skill.Type == 1005 || Skill.Type == 1085 || Skill.Type == 1090 || Skill.Type == 1095 || Skill.Type == 1195 || Skill.Type == 1280)
                                Keep = true;
                        }
                        else if (NewProfession == 41)
                        {
                            if (Skill.Type == 1000 || Skill.Type == 1005 || Skill.Type == 1075 || Skill.Type == 1090 || Skill.Type == 1095 || Skill.Type == 1195 || Skill.Type == 1280)
                                Keep = true;
                        }
                        else if (NewProfession == 132)
                            Keep = true;
                        else if (NewProfession == 142)
                        {
                            if (Skill.Type == 1000 || Skill.Type == 1001 || Skill.Type == 1005 || Skill.Type == 1050 || Skill.Type == 1055 || Skill.Type == 1075 || Skill.Type == 1175 || Skill.Type == 1195 || Skill.Type == 1280)
                                Keep = true;
                        }
                    }
                    else if (FirstProfession == 145)
                    {
                        if (NewProfession == 11 || NewProfession == 21 || NewProfession == 41)
                        {
                            if (Skill.Type == 1000 || Skill.Type == 1001 || Skill.Type == 1005 || Skill.Type == 1195)
                                Keep = true;
                        }
                        else if (NewProfession == 132)
                        {
                            if (Skill.Type == 1000 || Skill.Type == 1001 || Skill.Type == 1005 || Skill.Type == 1120 || Skill.Type == 1195)
                                Keep = true;
                        }
                        else if (NewProfession == 142)
                            Keep = true;
                    }

                    if (Skill.Type == 1000 || Skill.Type == 1005 || 
                        Skill.Type == 1045 || Skill.Type == 1046 ||
                        Skill.Type == 1220 || Skill.Type == 1260 || Skill.Type == 1290 || Skill.Type == 5040 || Skill.Type == 5050 || Skill.Type == 5010 || Skill.Type == 5020 || Skill.Type == 5030 || Skill.Type == 7010 || Skill.Type == 7020 ||
                        Skill.Type == 1350 || Skill.Type == 1360 || Skill.Type == 3321 ||
                        Skill.Type == 1380 || Skill.Type == 1385 || Skill.Type == 1390 || Skill.Type == 1395 || Skill.Type == 1400 || Skill.Type == 1405 || Skill.Type == 1410)
                        Keep = true;

                    if (Keep)
                    {
                        MagicType.Entry Info;
                        if (Database2.AllMagics.TryGetValue(Skill.Type * 10 + Skill.Level, out Info))
                        {
                            Skill.OldLevel = Skill.Level;
                            if (Skill.Exp >= Info.ExpRequired / 2)
                                if (Database2.AllMagics.ContainsKey(Skill.Type * 10 + Skill.Level + 1))
                                    Skill.OldLevel++;
                        }

                        Skill.Level = 0;
                        Skill.Exp = 0;

                        Send(MsgMagicInfo.Create(Skill));
                    }
                    else
                        SkillToDel.Add(Skill);
                }

                //Delete some skills...
                Magic[] Skills = SkillToDel.ToArray();
                for (Int32 i = 0; i < Skills.Length; i++)
                    DelMagic(Skills[i], true);
                Skills = null;

                //New Skill(s)
                if (FirstProfession == 15)
                {
                    if (NewProfession == 11)
                        AddMagic(Magic.Create(UniqId, 3050, 0, 0, 0, false), true); //CruelShade
                    else if (NewProfession == 21)
                        AddMagic(Magic.Create(UniqId, 5100, 0, 0, 0, false), true); //IronShirt
                }
                else if (FirstProfession == 45)
                {
                    if (NewProfession == 41)
                        AddMagic(Magic.Create(UniqId, 5000, 0, 0, 0, false), true); //FreezingArrow
                    else
                        AddMagic(Magic.Create(UniqId, 5002, 0, 0, 0, false), true); //Poison
                }
                else if (FirstProfession == 135)
                {
                    if (NewProfession == 132)
                        AddMagic(Magic.Create(UniqId, 3090, 0, 0, 0, false), true); //Pervade
                }
                else if (FirstProfession == 145)
                {
                    if (NewProfession == 142)
                        AddMagic(Magic.Create(UniqId, 3080, 0, 0, 0, false), true); //Dodge
                }
            }
            else if (Metempsychosis == 2)
            {
                foreach (Magic Skill in Magics.Values)
                {
                    Boolean Keep = false;
                    if (FirstProfession == 15)
                    {
                        if (SecondProfession == 15)
                        {
                            if (NewProfession == 11)
                                Keep = true;
                            else if (NewProfession == 21 || NewProfession == 41 || NewProfession == 132 || NewProfession == 142)
                            {
                                if (Skill.Type == 1110 || Skill.Type == 1190 || Skill.Type == 1270 || Skill.Type == 3050)
                                    Keep = true;
                            }
                        }
                        else if (SecondProfession == 25)
                        {
                            if (NewProfession == 11)
                            {
                                if (Skill.Type == 1110 || Skill.Type == 5100 || Skill.Type == 1190 || Skill.Type == 1270 || Skill.Type == 1015 || Skill.Type == 1040 || Skill.Type == 1320)
                                    Keep = true;
                            }
                            else if (NewProfession == 21)
                            {
                                if (Skill.Type == 1110 || Skill.Type == 1270 || Skill.Type == 1190)
                                    Keep = true;
                            }
                            else if (NewProfession == 41 || NewProfession == 142)
                            {
                                if (Skill.Type == 1110 || Skill.Type == 1270 || Skill.Type == 1190 || Skill.Type == 1020 || Skill.Type == 1040 || Skill.Type == 5100)
                                    Keep = true;
                            }
                            else if (NewProfession == 132)
                            {
                                if (Skill.Type == 1110 || Skill.Type == 1270 || Skill.Type == 1190 || Skill.Type == 1020 || Skill.Type == 1040 || Skill.Type == 5100 || Skill.Type == 1025)
                                    Keep = true;
                            }
                        }
                        else if (SecondProfession == 45)
                        {
                            if (NewProfession == 41)
                            {
                                if (Skill.Type == 1110 || Skill.Type == 1270 || Skill.Type == 1190 || Skill.Type == 5000)
                                    Keep = true;
                            }
                            else if (NewProfession == 21 || NewProfession == 132 || NewProfession == 142 || NewProfession == 11)
                            {
                                if (Skill.Type == 1110 || Skill.Type == 1270 || Skill.Type == 1190 || Skill.Type == 5002)
                                    Keep = true;
                            }
                        }
                        else if (SecondProfession == 135)
                        {
                            if (NewProfession == 11)
                            {
                                if (Skill.Type == 1270 || Skill.Type == 1005 || Skill.Type == 1085 || Skill.Type == 1090 || Skill.Type == 1095 || Skill.Type == 1195)
                                    Keep = true;
                            }
                            else if (NewProfession == 21)
                            {
                                if (Skill.Type == 1190 || Skill.Type == 1110 || Skill.Type == 1270 || Skill.Type == 1005 || Skill.Type == 1085 || Skill.Type == 1090 || Skill.Type == 1095 || Skill.Type == 1195)
                                    Keep = true;
                            }
                            else if (NewProfession == 41)
                            {
                                if (Skill.Type == 1190 || Skill.Type == 1110 || Skill.Type == 1270 || Skill.Type == 1005 || Skill.Type == 1075 || Skill.Type == 1090 || Skill.Type == 1095 || Skill.Type == 1195)
                                    Keep = true;
                            }
                            else if (NewProfession == 132)
                            {
                                if (Skill.Type == 1190 || Skill.Type == 1110 || Skill.Type == 1270 || Skill.Type == 3090)
                                    Keep = true;
                            }
                            else if (NewProfession == 142)
                            {
                                if (Skill.Type == 1190 || Skill.Type == 1110 || Skill.Type == 1270 || Skill.Type == 1050 || Skill.Type == 1075 || Skill.Type == 1175 || Skill.Type == 1170)
                                    Keep = true;
                            }
                        }
                        else if (SecondProfession == 145)
                        {
                            if (NewProfession == 11 || NewProfession == 21)
                            {
                                if (Skill.Type == 1270 || Skill.Type == 1000 || Skill.Type == 1001 || Skill.Type == 1005 || Skill.Type == 1195)
                                    Keep = true;
                            }
                            else if (NewProfession == 41)
                            {
                                if (Skill.Type == 1190 || Skill.Type == 1110 || Skill.Type == 1270 || Skill.Type == 1000 || Skill.Type == 1001 || Skill.Type == 1005 || Skill.Type == 1195)
                                    Keep = true;
                            }
                            else if (NewProfession == 132)
                            {
                                if (Skill.Type == 1190 || Skill.Type == 1110 || Skill.Type == 1270 || Skill.Type == 1120)
                                    Keep = true;
                            }
                            else if (NewProfession == 142)
                            {
                                if (Skill.Type == 1190 || Skill.Type == 1110 || Skill.Type == 1270 || Skill.Type == 3080)
                                    Keep = true;
                            }
                        }
                    }
                    else if (FirstProfession == 25)
                    {
                        if (SecondProfession == 15)
                        {
                            if (NewProfession == 11)
                            {
                                if (Skill.Type == 1320 || Skill.Type == 1040 || Skill.Type == 3050)
                                    Keep = true;
                            }
                            else if (NewProfession == 21)
                            {
                                if (Skill.Type == 1270 || Skill.Type == 1190 || Skill.Type == 1110)
                                    Keep = true;
                            }
                            else if (NewProfession == 41 || NewProfession == 132 || NewProfession == 142)
                            {
                                if (Skill.Type == 1270 || Skill.Type == 1190 || Skill.Type == 1110 || Skill.Type == 1040 || Skill.Type == 1320)
                                    Keep = true;
                            }
                            else if (SecondProfession == 25)
                            {
                                if (NewProfession == 21)
                                    Keep = true;
                                else if (NewProfession == 11)
                                {
                                    if (Skill.Type == 1320 || Skill.Type == 1015 || Skill.Type == 1040)
                                        Keep = true;
                                }
                                else if (NewProfession == 41 || NewProfession == 142)
                                {
                                    if (Skill.Type == 1020 || Skill.Type == 1040)
                                        Keep = true;
                                }
                                else if (NewProfession == 132)
                                {
                                    if (Skill.Type == 1020 || Skill.Type == 1040 || Skill.Type == 1025)
                                        Keep = true;
                                }
                            }
                            else if (SecondProfession == 45)
                            {
                                if (NewProfession == 11)
                                {
                                    if (Skill.Type == 1040 || Skill.Type == 5002)
                                        Keep = true;
                                }
                                else if (NewProfession == 21)
                                {
                                    if (Skill.Type == 5002)
                                        Keep = true;
                                }
                                else if (NewProfession == 41)
                                {
                                    if (Skill.Type == 5000)
                                        Keep = true;
                                }
                                else if (NewProfession == 132 || NewProfession == 142)
                                {
                                    if (Skill.Type == 1020 || Skill.Type == 1040 || Skill.Type == 5002)
                                        Keep = true;
                                }
                            }
                            else if (SecondProfession == 135)
                            {
                                if (NewProfession == 11)
                                {
                                    if (Skill.Type == 1005 || Skill.Type == 1085 || Skill.Type == 1090 || Skill.Type == 1095 || Skill.Type == 1195 || Skill.Type == 1280 || Skill.Type == 1350 || Skill.Type == 1040)
                                        Keep = true;
                                }
                                else if (NewProfession == 21)
                                {
                                    if (Skill.Type == 1005 || Skill.Type == 1085 || Skill.Type == 1090 || Skill.Type == 1095 || Skill.Type == 1195 || Skill.Type == 1280 || Skill.Type == 1350 || Skill.Type == 1040)
                                        Keep = true;
                                }
                                else if (NewProfession == 41)
                                {
                                    if (Skill.Type == 1005 || Skill.Type == 1090 || Skill.Type == 1095 || Skill.Type == 1195 || Skill.Type == 1280 || Skill.Type == 1350 || Skill.Type == 1040 || Skill.Type == 1075 || Skill.Type == 1020)
                                        Keep = true;
                                }
                                else if (NewProfession == 132)
                                {
                                    if (Skill.Type == 1020 || Skill.Type == 1025 || Skill.Type == 1040 || Skill.Type == 1280 || Skill.Type == 1350 || Skill.Type == 3090)
                                        Keep = true;
                                }
                                else if (NewProfession == 142)
                                {
                                    if (Skill.Type == 1020 || Skill.Type == 1280 || Skill.Type == 1350 || Skill.Type == 1040 || Skill.Type == 1050 || Skill.Type == 1055 || Skill.Type == 1175 || Skill.Type == 1075)
                                        Keep = true;
                                }
                            }
                            else if (SecondProfession == 145)
                            {
                                if (NewProfession == 11)
                                {
                                    if (Skill.Type == 1005 || Skill.Type == 1000 || Skill.Type == 1001 || Skill.Type == 1195 || Skill.Type == 1040)
                                        Keep = true;
                                }
                                else if (NewProfession == 21)
                                    if (Skill.Type == 1005 || Skill.Type == 1000 || Skill.Type == 1001 || Skill.Type == 1195)
                                        Keep = true;
                            }
                            else if (NewProfession == 41)
                            {
                                if (Skill.Type == 1005 || Skill.Type == 1000 || Skill.Type == 1001 || Skill.Type == 1195 || Skill.Type == 1040 || Skill.Type == 1020)
                                    Keep = true;
                            }
                            else if (NewProfession == 142)
                            {
                                if (Skill.Type == 1020 || Skill.Type == 1040 || Skill.Type == 3080)
                                    Keep = true;
                            }
                            else if (NewProfession == 132)
                            {
                                if (Skill.Type == 1020 || Skill.Type == 1040 || Skill.Type == 3090)
                                    Keep = true;
                            }
                        }
                    }
                    else if (FirstProfession == 45)
                    {
                        if (SecondProfession == 15)
                        {
                            if (NewProfession == 11)
                            {
                                if (Skill.Type == 3050 || Skill.Type == 5002)
                                    Keep = true;
                            }
                            else if (NewProfession == 21 || NewProfession == 41 || NewProfession == 132 || NewProfession == 142)
                            {
                                if (Skill.Type == 1110 || Skill.Type == 5002 || Skill.Type == 1190 || Skill.Type == 1270)
                                    Keep = true;
                            }
                        }
                        else if (SecondProfession == 25)
                        {
                            if (NewProfession == 11)
                            {
                                if (Skill.Type == 5002 || Skill.Type == 1015 || Skill.Type == 1040 || Skill.Type == 1320)
                                    Keep = true;
                            }
                            else if (NewProfession == 21)
                            {
                                if (Skill.Type == 5002)
                                    Keep = true;
                            }
                            else if (NewProfession == 41 || NewProfession == 132 || NewProfession == 142)
                            {
                                if (Skill.Type == 5002 || Skill.Type == 1020 || Skill.Type == 1040)
                                    Keep = true;
                            }
                        }
                        else if (SecondProfession == 45)
                        {
                            if (NewProfession == 41)
                                Keep = true;
                            else if (NewProfession == 11 || NewProfession == 21 || NewProfession == 132 || NewProfession == 142)
                            {
                                if (Skill.Type == 5002 || Skill.Type == 5000)
                                    Keep = true;
                            }
                        }
                        else if (SecondProfession == 135)
                        {
                            if (NewProfession == 11 || NewProfession == 21)
                            {
                                if (Skill.Type == 1085 || Skill.Type == 1005 || Skill.Type == 1090 || Skill.Type == 1095 || Skill.Type == 5002 || Skill.Type == 1195 || Skill.Type == 1280 || Skill.Type == 1350)
                                    Keep = true;
                            }
                            else if (NewProfession == 41)
                            {
                                if (Skill.Type == 1075 || Skill.Type == 1005 || Skill.Type == 1090 || Skill.Type == 1095 || Skill.Type == 5002 || Skill.Type == 1195 || Skill.Type == 1280 || Skill.Type == 1350)
                                    Keep = true;
                            }
                            else if (NewProfession == 132)
                            {
                                if (Skill.Type == 5002 || Skill.Type == 1280 || Skill.Type == 1350 || Skill.Type == 3090)
                                    Keep = true;
                            }
                            else if (NewProfession == 142)
                            {
                                if (Skill.Type == 5002 || Skill.Type == 1075 || Skill.Type == 1350 || Skill.Type == 3090 || Skill.Type == 1050 || Skill.Type == 1175 || Skill.Type == 1055)
                                    Keep = true;
                            }
                        }
                        else if (SecondProfession == 145)
                        {
                            if (NewProfession == 11 || NewProfession == 21 || NewProfession == 41)
                            {
                                if (Skill.Type == 5002 || Skill.Type == 1000 || Skill.Type == 1001 || Skill.Type == 1005 || Skill.Type == 1195)
                                    Keep = true;
                            }
                            else if (NewProfession == 132)
                            {
                                if (Skill.Type == 5002 || Skill.Type == 1120)
                                    Keep = true;
                            }
                            else if (NewProfession == 142)
                            {
                                if (Skill.Type == 5002 || Skill.Type == 3080)
                                    Keep = true;
                            }
                        }
                    }
                    else if (FirstProfession == 135)
                    {
                        if (SecondProfession == 15)
                        {
                            if (NewProfession == 11)
                            {
                                if (Skill.Type == 1005 || Skill.Type == 1085 || Skill.Type == 1090 || Skill.Type == 1095 || Skill.Type == 1195 || Skill.Type == 3050)
                                    Keep = true;
                            }
                            else if (NewProfession == 21 || NewProfession == 41 || NewProfession == 132 || NewProfession == 142)
                            {
                                if (Skill.Type == 1005 || Skill.Type == 1085 || Skill.Type == 1090 || Skill.Type == 1095 || Skill.Type == 1195 || Skill.Type == 1190 || Skill.Type == 1110 || Skill.Type == 1270)
                                    Keep = true;
                            }
                        }
                        else if (SecondProfession == 25)
                        {
                            if (NewProfession == 11)
                            {
                                if (Skill.Type == 1005 || Skill.Type == 1085 || Skill.Type == 1090 || Skill.Type == 1095 || Skill.Type == 1195 || Skill.Type == 1015 || Skill.Type == 1040 || Skill.Type == 1320)
                                    Keep = true;
                            }
                            else if (NewProfession == 21)
                            {
                                if (Skill.Type == 1005 || Skill.Type == 1085 || Skill.Type == 1090 || Skill.Type == 1095 || Skill.Type == 1195)
                                    Keep = true;
                            }
                            else if (NewProfession == 41 || NewProfession == 142)
                            {
                                if (Skill.Type == 1005 || Skill.Type == 1085 || Skill.Type == 1090 || Skill.Type == 1095 || Skill.Type == 1195 || Skill.Type == 1040 || Skill.Type == 1020)
                                    Keep = true;
                            }
                            else if (NewProfession == 132)
                            {
                                if (Skill.Type == 1005 || Skill.Type == 1085 || Skill.Type == 1090 || Skill.Type == 1095 || Skill.Type == 1195 || Skill.Type == 1040 || Skill.Type == 1020 || Skill.Type == 1025)
                                    Keep = true;
                            }
                        }
                        else if (SecondProfession == 45)
                        {
                            if (NewProfession == 11 || NewProfession == 21 || NewProfession == 132 || NewProfession == 142)
                            {
                                if (Skill.Type == 1005 || Skill.Type == 1065 || Skill.Type == 1090 || Skill.Type == 1095 || Skill.Type == 1195 || Skill.Type == 5002)
                                    Keep = true;
                            }
                            else if (NewProfession == 41)
                            {
                                if (Skill.Type == 1005 || Skill.Type == 1065 || Skill.Type == 1090 || Skill.Type == 1095 || Skill.Type == 1195 || Skill.Type == 5002 || Skill.Type == 5000)
                                    Keep = true;
                            }
                            else if (SecondProfession == 135)
                            {
                                if (NewProfession == 132)
                                    Keep = true;
                                else if (NewProfession == 11 || NewProfession == 21 || NewProfession == 41)
                                {
                                    if (Skill.Type == 1005 || Skill.Type == 1085 || Skill.Type == 1090 || Skill.Type == 1095 || Skill.Type == 1195 || Skill.Type == 3090)
                                        Keep = true;
                                }
                                else if (NewProfession == 142)
                                {
                                    if (Skill.Type == 3090 || Skill.Type == 1050 || Skill.Type == 1055 || Skill.Type == 1175 || Skill.Type == 1075)
                                        Keep = true;
                                }
                            }
                            else if (SecondProfession == 145)
                            {
                                if (NewProfession == 11 || NewProfession == 21 || NewProfession == 41)
                                {
                                    if (Skill.Type == 1055 || Skill.Type == 1175 || Skill.Type == 1050 || Skill.Type == 1000 || Skill.Type == 1001 || Skill.Type == 1005 || Skill.Type == 1075 || Skill.Type == 1195)
                                        Keep = true;
                                }
                                else if (NewProfession == 142)
                                {
                                    if (Skill.Type == 1050 || Skill.Type == 1055 || Skill.Type == 1175 || Skill.Type == 1075 || Skill.Type == 3080)
                                        Keep = true;
                                }
                                else if (NewProfession == 132)
                                {
                                    if (Skill.Type == 1050 || Skill.Type == 1055 || Skill.Type == 1175 || Skill.Type == 1075 || Skill.Type == 1120)
                                        Keep = true;
                                }
                            }
                        }
                    }
                    else if (FirstProfession == 145)
                    {
                        if (SecondProfession == 15)
                        {
                            if (NewProfession == 11)
                            {
                                if (Skill.Type == 1000 || Skill.Type == 1001 || Skill.Type == 1005 || Skill.Type == 1195 || Skill.Type == 3050)
                                    Keep = true;
                            }
                            else if (NewProfession == 21 || NewProfession == 51 || NewProfession == 132 || NewProfession == 142)
                            {
                                if (Skill.Type == 1000 || Skill.Type == 1001 || Skill.Type == 1005 || Skill.Type == 1195 || Skill.Type == 1110 || Skill.Type == 1190)
                                    Keep = true;
                            }

                        }
                        else if (SecondProfession == 25)
                        {
                            if (NewProfession == 11)
                            {
                                if (Skill.Type == 1000 || Skill.Type == 1001 || Skill.Type == 1005 || Skill.Type == 1195 || Skill.Type == 1040 || Skill.Type == 1015 || Skill.Type == 1320)
                                    Keep = true;
                            }
                            else if (NewProfession == 21)
                            {
                                if (Skill.Type == 1000 || Skill.Type == 1001 || Skill.Type == 1005 || Skill.Type == 1195)
                                    Keep = true;
                            }
                            else if (NewProfession == 41)
                            {
                                if (Skill.Type == 1000 || Skill.Type == 1001 || Skill.Type == 1005 || Skill.Type == 1195 || Skill.Type == 1040 || Skill.Type == 1020)
                                    Keep = true;
                            }
                            else if (NewProfession == 132)
                            {
                                if (Skill.Type == 1000 || Skill.Type == 1001 || Skill.Type == 1005 || Skill.Type == 1195 || Skill.Type == 1040 || Skill.Type == 1020 || Skill.Type == 1025)
                                    Keep = true;
                            }
                            else if (NewProfession == 142)
                            {
                                if (Skill.Type == 1020 || Skill.Type == 1040)
                                    Keep = true;
                            }
                        }
                        else if (SecondProfession == 45)
                        {
                            if (NewProfession == 11 || NewProfession == 21 || NewProfession == 132 || NewProfession == 142)
                            {
                                if (Skill.Type == 1000 || Skill.Type == 1001 || Skill.Type == 1005 || Skill.Type == 1195 || Skill.Type == 5002)
                                    Keep = true;
                            }
                            else if (NewProfession == 41)
                            {
                                if (Skill.Type == 1000 || Skill.Type == 1001 || Skill.Type == 1005 || Skill.Type == 1195 || Skill.Type == 5002 || Skill.Type == 5000)
                                    Keep = true;
                            }
                        }
                        else if (SecondProfession == 135)
                        {
                            if (NewProfession == 11 || NewProfession == 21)
                            {
                                if (Skill.Type == 1005 || Skill.Type == 1090 || Skill.Type == 1095 || Skill.Type == 1195 || Skill.Type == 1095)
                                    Keep = true;
                            }
                            else if (NewProfession == 41)
                            {
                                if (Skill.Type == 1005 || Skill.Type == 1090 || Skill.Type == 1095 || Skill.Type == 1195 || Skill.Type == 1075)
                                    Keep = true;
                            }
                            else if (NewProfession == 142)
                            {
                                if (Skill.Type == 1055 || Skill.Type == 1050 || Skill.Type == 1175 || Skill.Type == 1075)
                                    Keep = true;
                            }
                            else if (NewProfession == 152)
                            {
                                if (Skill.Type == 3090 || Skill.Type == 1120)
                                    Keep = true;
                            }
                        }
                        else if (SecondProfession == 145)
                        {
                            if (NewProfession == 142)
                                Keep = true;
                            else if (NewProfession == 11 || NewProfession == 21 || NewProfession == 41)
                            {
                                if (Skill.Type == 1000 || Skill.Type == 1001 || Skill.Type == 1005 || Skill.Type == 1195 || Skill.Type == 3080)
                                    Keep = true;
                            }
                            else if (NewProfession == 132)
                            {
                                if (Skill.Type == 3080 || Skill.Type == 1120)
                                    Keep = true;
                            }
                        }
                    }

                    if (Skill.Type == 1000 || Skill.Type == 1005 ||
                        Skill.Type == 1045 || Skill.Type == 1046 ||
                        Skill.Type == 1220 || Skill.Type == 1260 || Skill.Type == 1290 || Skill.Type == 5040 || Skill.Type == 5050 || Skill.Type == 5010 || Skill.Type == 5020 || Skill.Type == 5030 || Skill.Type == 7010 || Skill.Type == 7020 ||
                        Skill.Type == 1350 || Skill.Type == 1360 || Skill.Type == 3321 ||
                        Skill.Type == 1380 || Skill.Type == 1385 || Skill.Type == 1390 || Skill.Type == 1395 || Skill.Type == 1400 || Skill.Type == 1405 || Skill.Type == 1410 ||
                        Skill.Type == 4000 || Skill.Type == 4010 || Skill.Type == 4020 || Skill.Type == 4030 || Skill.Type == 4040 || Skill.Type == 4050 || Skill.Type == 4060 || Skill.Type == 4070)
                        Keep = true;

                    if (Keep)
                    {
                        MagicType.Entry Info;
                        if (Database2.AllMagics.TryGetValue(Skill.Type * 10 + Skill.Level, out Info))
                        {
                            Skill.OldLevel = Skill.Level;
                            if (Skill.Exp >= Info.ExpRequired / 2)
                                if (Database2.AllMagics.ContainsKey(Skill.Type * 10 + Skill.Level + 1))
                                    Skill.OldLevel++;
                        }

                        Skill.Level = 0;
                        Skill.Exp = 0;

                        Send(MsgMagicInfo.Create(Skill));
                    }
                    else
                        SkillToDel.Add(Skill);
                }

                Magic[] Skills = SkillToDel.ToArray();
                for (Int32 i = 0; i < Skills.Length; i++)
                    DelMagic(Skills[i], true);
                Skills = null;

                //New Skill(s)
                if (FirstProfession != 15 && SecondProfession == 15)
                {
                    if (NewProfession == 11)
                        AddMagic(Magic.Create(UniqId, 3050, 0, 0, 0, false), true); //CruelShade
                    else if (NewProfession == 21)
                        AddMagic(Magic.Create(UniqId, 5100, 0, 0, 0, false), true); //IronShirt
                }
                else if (FirstProfession != 45 && SecondProfession == 45)
                {
                    if (NewProfession == 41)
                        AddMagic(Magic.Create(UniqId, 5000, 0, 0, 0, false), true); //FreezingArrow
                    else
                        AddMagic(Magic.Create(UniqId, 5002, 0, 0, 0, false), true); //Poison
                }
                else if (FirstProfession != 135 && SecondProfession == 135)
                {
                    if (NewProfession == 132)
                        AddMagic(Magic.Create(UniqId, 3090, 0, 0, 0, false), true); //Pervade
                }
                else if (FirstProfession != 145 && SecondProfession == 145)
                {
                    if (NewProfession == 142)
                        AddMagic(Magic.Create(UniqId, 3080, 0, 0, 0, false), true); //Dodge
                }
                AddMagic(Magic.Create(UniqId, 9876, 0, 0, 0, false), true); //GodBlessing
            }
            else
            {
                foreach (Magic Skill in Magics.Values)
                {
                    MagicType.Entry Info;
                    if (Database2.AllMagics.TryGetValue(Skill.Type * 10 + Skill.Level, out Info))
                    {
                        Skill.OldLevel = Skill.Level;
                        if (Skill.Exp >= Info.ExpRequired / 2)
                            if (Database2.AllMagics.ContainsKey(Skill.Type * 10 + Skill.Level + 1))
                                Skill.OldLevel++;
                    }

                    Skill.Level = 0;
                    Skill.Exp = 0;

                    Send(MsgMagicInfo.Create(Skill));
                }
            }

            for (Byte Pos = 1; Pos < 10; Pos++)
            {
                if (Pos == 7 || Pos == 9)
                    continue;

                Item Item = GetItemByPos(Pos);
                if (Item == null)
                    continue;

                Int32 NewId = ItemHandler.GetFirstId(Item.Id);
                if (NewId == Item.Id)
                    continue;

                Item.Id = NewId;
                Item.CurDura = (UInt16)((Double)ItemHandler.GetMaxDura(NewId) * (Double)Item.CurDura / (Double)Item.MaxDura);
                Item.MaxDura = ItemHandler.GetMaxDura(NewId);

                UpdateItem(Item);
            }

            if (Bless)
            {
                List<Byte> ValidPos = new List<Byte>();

                for (Byte Pos = 1; Pos < 10; Pos++)
                {
                    Item Item = GetItemByPos(Pos);
                    if (Item == null)
                        continue;

                    if (Item.Bless != 0)
                        continue;

                    ValidPos.Add(Pos);
                }

                if (ValidPos.Count > 0)
                {
                    Byte Pos = ValidPos.ToArray()[MyMath.Generate(0, (ValidPos.Count - 1))];
                    Item Item = GetItemByPos(Pos);

                    if (Item != null && Item.Bless == 0)
                    {
                        Item.Bless = 1;
                        UpdateItem(Item);
                    }
                }
            }

            MyMath.GetHitPoints(this, true);
            MyMath.GetMagicPoints(this, true);
            MyMath.GetPotency(this, true);
            MyMath.GetEquipStats(this);

            CurHP = MaxHP;
            CurMP = MaxMP;

            Database.Save(this);

            #region Update Attrib
            Send(MsgUserAttrib.Create(this, Level, MsgUserAttrib.Type.Level));
            Send(MsgUserAttrib.Create(this, Profession, MsgUserAttrib.Type.Profession));
            Send(MsgUserAttrib.Create(this, (Int64)Exp, MsgUserAttrib.Type.Exp));
            Send(MsgUserAttrib.Create(this, Metempsychosis, MsgUserAttrib.Type.Metempsychosis));
            Send(MsgUserAttrib.Create(this, CurHP, MsgUserAttrib.Type.Life));
            Send(MsgUserAttrib.Create(this, CurMP, MsgUserAttrib.Type.Mana));
            Send(MsgUserAttrib.Create(this, Strength, MsgUserAttrib.Type.Strength));
            Send(MsgUserAttrib.Create(this, Agility, MsgUserAttrib.Type.Agility));
            Send(MsgUserAttrib.Create(this, Vitality, MsgUserAttrib.Type.Vitality));
            Send(MsgUserAttrib.Create(this, Spirit, MsgUserAttrib.Type.Spirit));
            Send(MsgUserAttrib.Create(this, AddPoints, MsgUserAttrib.Type.AddPoints));

            if (Team != null)
            {
                World.BroadcastTeamMsg(Team, MsgUserAttrib.Create(this, CurHP, MsgUserAttrib.Type.Life));
                World.BroadcastTeamMsg(Team, MsgUserAttrib.Create(this, MaxHP, (MsgUserAttrib.Type.Life + 1)));
            }
            #endregion

            #region Flags
            if (Metempsychosis > 2)
            {
                if (Metempsychosis == 3)
                    AddFlag(Flag.ThirdMetempsychosis);
                else
                {
                    if (Metempsychosis == 4)
                    {
                        AddFlag(Flag.FourthMetempsychosis);
                        DelFlag(Flag.ThirdMetempsychosis);
                    }
                    else if (Metempsychosis == 5)
                    {
                        AddFlag(Flag.FifthMetempsychosis);
                        DelFlag(Flag.FourthMetempsychosis);
                    }
                    else if (Metempsychosis == 6)
                    {
                        AddFlag(Flag.SixthMetempsychosis);
                        DelFlag(Flag.FifthMetempsychosis);
                    }
                    else if (Metempsychosis == 7)
                    {
                        AddFlag(Flag.SixthMetempsychosis);
                        DelFlag(Flag.SeventhMetempsychosis);
                    }
                    else if (Metempsychosis == 8)
                    {
                        AddFlag(Flag.EighthMetempsychosis);
                        DelFlag(Flag.SeventhMetempsychosis);
                    }
                    else if (Metempsychosis == 9)
                    {
                        AddFlag(Flag.NinthMetempsychosis);
                        DelFlag(Flag.EighthMetempsychosis);
                    }
                    else if (Metempsychosis == 10)
                    {
                        AddFlag(Flag.TenthMetempsychosis);
                        DelFlag(Flag.NinthMetempsychosis);
                    }
                }
                World.BroadcastRoomMsg(this, MsgUserAttrib.Create(this, Flags, MsgUserAttrib.Type.Flags), true);
            }
            #endregion
        }

        public void AddTransform(Int16 TransformId) { Look = (UInt32)((TransformId * 10000000L) + (Look % 10000000L)); }
        public void DelTransform() { Look = Look % 10000000; }

        public void Mine()
        {
            if (Mining && Environment.TickCount - LastPickSwingTick < 3000)
                return;

            Mining = true;
            LastPickSwingTick = Environment.TickCount;

            if (!IsAlive())
            {
                Mining = false;
                SendSysMsg(Owner.GetStr("STR_DIE"));
                return;
            }

            Item Item = GetItemByPos(4);
            if (Item == null)
            {
                Mining = false;
                SendSysMsg(Owner.GetStr("STR_MINE_WITH_PECKER"));
                return;
            }

            if (Item.Id / 1000 != 562)
            {
                Mining = false;
                SendSysMsg(Owner.GetStr("STR_MINE_WITH_PECKER"));
                return;
            }

            World.BroadcastRoomMsg(this, MsgAction.Create(this, 0, MsgAction.Action.Mine), true);

            Double Bonus = 1.00;
            if (WeaponSkills.ContainsKey(562))
                Bonus += (Double)WeaponSkills[562].Level / 100.00;

            Int32 ItemId = 0;
            if (MyMath.Success(30 * Bonus))
            {
                //1023 CrystalMine-1 1023 10242 10 0 0 1000 4294967295
                //1024 CrystalMine-2 1024 10242 10 0 0 1000 4294967295
                //1025 CopperMine 1025 10242 10 0 0 1011 4294967295
                //1026 SilverMine 1026 10242 10 0 0 1020 4294967295
                //1027 GoldMine 1027 10242 10 0 0 1000 4294967295
                //1028 MineCave 1028 10242 10 0 0 1002 4294967295
                //1218 Adventure-8.1 1025 2114 10 1003 1278 1219 4294967295
                //6000 BlackJail 6000 10594 0 29 72 6000 4294967295

                //Iron
                ItemId = 1072010 + MyMath.Generate(0, 3);
                if (MyMath.Success(25 * Bonus))
                {
                    ItemId = 1072010 + MyMath.Generate(4, 7);
                    if (MyMath.Success(25 * Bonus))
                        ItemId++;
                    if (MyMath.Success(15 * Bonus))
                        ItemId++;
                }

                //Copper
                if (MyMath.Success(75.0 * Bonus) && Map != 1028)
                {
                    ItemId = 1072020 + MyMath.Generate(0, 3);
                    if (MyMath.Success(25 * Bonus))
                    {
                        ItemId = 1072020 + MyMath.Generate(4, 7);
                        if (MyMath.Success(25 * Bonus))
                            ItemId++;
                        if (MyMath.Success(15 * Bonus))
                            ItemId++;
                    }
                }

                //Silver
                if (MyMath.Success(35.0 * Bonus) && (Map == 1026 || Map == 1027))
                {
                    ItemId = 1072040 + MyMath.Generate(0, 3);
                    if (MyMath.Success(25 * Bonus))
                    {
                        ItemId = 1072040 + MyMath.Generate(4, 7);
                        if (MyMath.Success(25 * Bonus))
                            ItemId++;
                        if (MyMath.Success(15 * Bonus))
                            ItemId++;
                    }
                }

                //Gold
                if (MyMath.Success(5.0 * Bonus) && Map != 1027 || MyMath.Success(25.0 * Bonus))
                {
                    ItemId = 1072050 + MyMath.Generate(0, 3);
                    if (MyMath.Success(25 * Bonus))
                    {
                        ItemId = 1072050 + MyMath.Generate(4, 7);
                        if (MyMath.Success(25 * Bonus))
                            ItemId++;
                        if (MyMath.Success(15 * Bonus))
                            ItemId++;
                    }
                }

                if (MyMath.Success(7.5 * Bonus) && Map == 1028)
                    ItemId = 1072031; //Euxite

                if (MyMath.Success(1.0 * Bonus) && Map == 1023)
                    ItemId = 1088003; //Citrine

                if (MyMath.Success(1.0 * Bonus) && Map == 1024)
                    ItemId = 1088004; //TigerEye

                if (MyMath.Success(1.5 * Bonus))
                {
                    ItemId = 700001;
                    while (true)
                    {
                        Byte GemType = (Byte)MyMath.Generate(0, 7);
                        if (Map == 1028 && !(GemType == 0 || GemType == 1 || GemType == 2 || GemType == 7))
                            continue;
                        else if (Map == 1025 && !(GemType == 0 || GemType == 1 || GemType == 3 || GemType == 4))
                            continue;
                        else if (Map == 1026 && !(GemType == 5 || GemType == 6))
                            continue;
                        else if (Map == 1027 && !(GemType == 5 || GemType == 6))
                            continue;
                        else if (Map == 1218 && !(GemType == 0 || GemType == 1 || GemType == 3 || GemType == 4))
                            continue;
                        else if (Map == 6000 && !(GemType == 0 || GemType == 1 || GemType == 3 || GemType == 4))
                            continue;
                        ItemId += (GemType * 10);
                        break;
                    }
                    if (MyMath.Success(7.5 * Bonus))
                        ItemId++;
                    if (MyMath.Success(5.0 * Bonus))
                        ItemId++;
                }
            }

            if (ItemId != 0 && ItemInInventory() < 40)
            {
                if (!AutoMine)
                    AddItem(Item.Create(0, 0, ItemId, 0, 0, 0, 0, 0, 2, 0, ItemHandler.GetMaxDura(ItemId), ItemHandler.GetMaxDura(ItemId)), true);
                else
                {
                    if (ItemId / 100000 == 7 || ItemId == 1088003 || ItemId == 1088003)
                        AddItem(Item.Create(0, 0, ItemId, 0, 0, 0, 0, 0, 2, 0, ItemHandler.GetMaxDura(ItemId), ItemHandler.GetMaxDura(ItemId)), true);
                    else
                    {
                        ItemType.Entry Info;
                        if (Database2.AllItems.TryGetValue(ItemId, out Info))
                        {
                            Money += (Int32)Info.Price / 3;
                            Send(MsgUserAttrib.Create(this, Money, MsgUserAttrib.Type.Money));
                        }
                    }
                }
            }
        }

        public Boolean Reflect()
        {
            if (!(FirstProfession == 25 && Profession > 19 && Profession < 26) && !(SecondProfession == 25))
                return false;

            if (!MyMath.Success(15))
                return false;
            return true;
        }

        public Boolean IsAllNonsuchEquip()
        {
            Boolean TwoHandsWeapon = false;
            for (Byte i = 1; i < 10; i++)
            {
                if (i == 5 && TwoHandsWeapon)
                    continue;

                if (i == 7 || i == 9)
                    continue;

                Item Item = GetItemByPos(i);
                if (Item == null)
                    return false;

                if (Item.Id % 10 != 9)
                    return false;

                if (i == 4 && ((Byte)(Item.Id / 100000) == 5 || (Int16)(Item.Id / 1000) == 421))
                    TwoHandsWeapon = true;
            }
            return true;
        }

        public Int32 GetArmorTypeID()
        {
            Item Item = GetItemByPos(3);
            if (Item == null)
                return 0;
            return Item.Id;
        }

        public Int32 GetHeadTypeID()
        {
            Item Item = GetItemByPos(1);
            if (Item == null)
                return 0;
            return Item.Id;
        }

        public Int32 GetRightHandTypeID()
        {
            Item Item = GetItemByPos(4);
            if (Item == null)
                return 0;
            return Item.Id;
        }

        public Int32 GetLeftHandTypeID()
        {
            Item Item = GetItemByPos(5);
            if (Item == null)
                return 0;
            return Item.Id;
        }

        public Int32 GetGarmentTypeID()
        {
            Item Item = GetItemByPos(9);
            if (Item == null)
                return 0;
            return Item.Id;
        }

        public Item GetItemByPos(Byte Position)
        {
            lock (Items)
            {
                foreach (Item Item in Items.Values)
                {
                    if (Item.Position == Position)
                        return Item;
                }
            }
            return null;
        }

        public Item GetItemById(Int32 Id)
        {
            lock (Items)
            {
                foreach (Item Item in Items.Values)
                {
                    if (Item.Position == 0 && Item.Id == Id)
                        return Item;
                }
            }
            return null;
        }

        public Item GetItemByUID(Int32 UniqId)
        {
            if (Items.ContainsKey(UniqId))
                return Items[UniqId];
            return null;
        }

        public Boolean InventoryContains(Int32 Id, Int32 Count)
        {
            Int32 ICount = 0;
            lock (Items)
            {
                foreach (Item Item in Items.Values)
                {
                    if (Item.Id == Id && Item.Position == 0)
                        ICount++;
                }
            }
            return Count <= ICount;
        }

        public Int32 ItemInInventory()
        {
            Int32 Count = 0;
            lock (Items)
            {
                foreach (Item Item in Items.Values)
                {
                    if (Item.Position == 0)
                        Count++;
                }
            }
            return Count;
        }

        public Item[] GetWHItems(Int16 WarehouseUID)
        {
            List<Item> WHItems = new List<Item>();
            lock (Items)
            {
                foreach (Item Item in Items.Values)
                {
                    if (Item.Position == WarehouseUID)
                        WHItems.Add(Item);
                }
            }
            return WHItems.ToArray();
        }

        public Int32 ItemInWarehouse(Int16 WarehouseUID)
        {
            Int32 Count = 0;
            lock (Items)
            {
                foreach (Item Item in Items.Values)
                {
                    if (Item.Position == WarehouseUID)
                        Count++;
                }
            }
            return Count;
        }

        public void AddItem(Item Item, Boolean Send)
        {
            if (Items.ContainsKey(Item.UniqId))
                return;

            Item.OwnerUID = UniqId;
            Item.Position = 0;

            lock (Items) { Items.Add(Item.UniqId, Item); }
            if (Send)
                this.Send(MsgItemInfo.Create(Item, MsgItemInfo.Action.AddItem));
        }

        public void UpdateItem(Item Item)
        {
            if (!Items.ContainsKey(Item.UniqId))
                return;

            Send(MsgItemInfo.Create(Item, MsgItemInfo.Action.Update));
            World.BroadcastRoomMsg(this, MsgItemInfoEx.Create(UniqId, Item, 0, MsgItemInfoEx.Action.Equipment), false);
        }

        public void UpdateItem(Int32 UniqId)
        {
            if (!Items.ContainsKey(UniqId))
                return;

            Send(MsgItemInfo.Create(Items[UniqId], MsgItemInfo.Action.Update));
            World.BroadcastRoomMsg(this, MsgItemInfoEx.Create(UniqId, Items[UniqId], 0, MsgItemInfoEx.Action.Equipment), false);
        }

        public void DelItem(Item Item, Boolean Send)
        {
            if (!Items.ContainsKey(Item.UniqId))
                return;

            Item.OwnerUID = 0;

            if (Send)
                this.Send(MsgItem.Create(Item.UniqId, Item.Position, MsgItem.Action.Drop)); //?

            lock (Items) { Items.Remove(Item.UniqId); }
        }

        public void DelItem(Int32 UniqId, Boolean Send)
        {
            if (!Items.ContainsKey(UniqId))
                return;

            Item Item = Items[UniqId];
            Item.OwnerUID = 0;

            if (Send)
                this.Send(MsgItem.Create(UniqId, Item.Position, MsgItem.Action.Drop)); //?

            lock (Items) { Items.Remove(UniqId); }
        }

        public void DelItem(Int32 Id, Int32 Count, Boolean Send)
        {
            lock (Items)
            {
                Item[] Array = new Item[Items.Count];
                Items.Values.CopyTo(Array, 0);

                Int32 ICount = 0;
                for (Int32 i = 0; i < Array.Length; i++)
                {
                    if (Count == ICount)
                        break;

                    if (Array[i].Id == Id && Array[i].Position == 0)
                    {
                        Array[i].OwnerUID = 0;

                        if (Send)
                            this.Send(MsgItem.Create(Array[i].UniqId, 0, MsgItem.Action.Drop));
                        Items.Remove(Array[i].UniqId);
                        ICount++;
                    }
                }

                Array = null;
            }
        }

        public WeaponSkill GetWeaponSkillByType(Int16 Type)
        {
            lock (WeaponSkills)
            {
                foreach (WeaponSkill WeaponSkill in WeaponSkills.Values)
                {
                    if (WeaponSkill.Type == Type)
                        return WeaponSkill;
                }
            }
            return null;
        }

        public void AddWeaponSkill(WeaponSkill WeaponSkill, Boolean Send)
        {
            if (WeaponSkills.ContainsKey(WeaponSkill.UniqId))
                return;

            WeaponSkill.OwnerUID = UniqId;

            lock (WeaponSkills) { WeaponSkills.Add(WeaponSkill.UniqId, WeaponSkill); }
            if (Send)
                this.Send(MsgWeaponSkill.Create(WeaponSkill));
        }

        public void UpdateWeaponSkill(WeaponSkill WeaponSkill)
        {
            if (!WeaponSkills.ContainsKey(WeaponSkill.UniqId))
                return;

            Send(MsgFlushExp.Create(WeaponSkill));
        }

        public void UpdateWeaponSkill(Int32 UniqId)
        {
            if (!WeaponSkills.ContainsKey(UniqId))
                return;

            Send(MsgFlushExp.Create(WeaponSkills[UniqId]));
        }

        public void DelWeaponSkill(WeaponSkill WeaponSkill, Boolean Send)
        {
            if (!WeaponSkills.ContainsKey(WeaponSkill.UniqId))
                return;

            WeaponSkill.OwnerUID = 0;

            if (Send)
                this.Send(MsgAction.Create(this, WeaponSkill.Type, MsgAction.Action.DropSkill));

            lock (WeaponSkills) { WeaponSkills.Remove(WeaponSkill.UniqId); }
        }

        public void DelWeaponSkill(Int32 UniqId, Boolean Send)
        {
            if (!WeaponSkills.ContainsKey(UniqId))
                return;

            WeaponSkill WeaponSkill = WeaponSkills[UniqId];
            WeaponSkill.OwnerUID = 0;

            if (Send)
                this.Send(MsgAction.Create(this, WeaponSkill.Type, MsgAction.Action.DropSkill));

            lock (WeaponSkills) { WeaponSkills.Remove(UniqId); }
        }

        public void CheckWeaponSkills()
        {
            Dictionary<Int16, Int32> List = new Dictionary<Int16, Int32>();
            List<Int32> ToDelete = new List<Int32>();

            lock (WeaponSkills)
            {
                foreach (WeaponSkill WeaponSkill in WeaponSkills.Values)
                {
                    if (List.ContainsKey(WeaponSkill.Type))
                    {
                        WeaponSkill WeaponSkill2 = WeaponSkills[List[WeaponSkill.Type]];
                        if (WeaponSkill2.Level == WeaponSkill.Level)
                        {
                            if (WeaponSkill.Exp >= WeaponSkill2.Exp)
                                ToDelete.Add(WeaponSkill2.UniqId);
                            else
                                ToDelete.Add(WeaponSkill.UniqId);
                        }
                        else
                        {
                            if (WeaponSkill.Level > WeaponSkill2.Level)
                                ToDelete.Add(WeaponSkill2.UniqId);
                            else
                                ToDelete.Add(WeaponSkill.UniqId);
                        }
                    }
                    else
                        List.Add(WeaponSkill.Type, WeaponSkill.UniqId);
                }
            }

            foreach (Int32 UniqId in ToDelete)
                DelWeaponSkill(UniqId, true);

            if (ToDelete.Count > 0)
                Disconnect();
        }

        public Magic GetMagicByType(Int16 Type)
        {
            lock (Magics)
            {
                foreach (Magic Magic in Magics.Values)
                {
                    if (Magic.Type == Type)
                        return Magic;
                }
            }
            return null;
        }

        public void AddMagic(Magic Magic, Boolean Send)
        {
            if (Magics.ContainsKey(Magic.UniqId))
                return;

            Magic.OwnerUID = UniqId;

            lock (Magics) { Magics.Add(Magic.UniqId, Magic); }
            if (Send)
                this.Send(MsgMagicInfo.Create(Magic));
        }

        public void UpdateMagic(Magic Magic)
        {
            if (!Magics.ContainsKey(Magic.UniqId))
                return;

            Send(MsgFlushExp.Create(Magic));
        }

        public void UpdateMagic(Int32 UniqId)
        {
            if (!Magics.ContainsKey(UniqId))
                return;

            Send(MsgFlushExp.Create(Magics[UniqId]));
        }

        public void DelMagic(Magic Magic, Boolean Send)
        {
            if (!Magics.ContainsKey(Magic.UniqId))
                return;

            Magic.OwnerUID = 0;

            if (Send)
                this.Send(MsgAction.Create(this, Magic.Type, MsgAction.Action.DropMagic));

            lock (Magics) { Magics.Remove(Magic.UniqId); }
        }

        public void DelMagic(Int32 UniqId, Boolean Send)
        {
            if (!Magics.ContainsKey(UniqId))
                return;

            Magic Magic = Magics[UniqId];
            Magic.OwnerUID = 0;

            if (Send)
                this.Send(MsgAction.Create(this, Magic.Type, MsgAction.Action.DropMagic));

            lock (Magics) { Magics.Remove(UniqId); }
        }

        public void Disconnect() { Owner.Disconnect(); }
        public void Send(Byte[] Buffer) { Owner.Send(Buffer); }
        public void SendSysMsg(String Message) { Send(MsgTalk.Create("SYSTEM", "ALLUSERS", Message, MsgTalk.Channel.System, 0xFF0000)); }
        public void KickBack() { Send(MsgAction.Create(this, World.AllMaps[Map].Id, MsgAction.Action.EnterMap)); }

        public void SendEquipStats()
        { 
            Send(MsgTalk.Create("SYSTEM", "ALLUSERS",
                "MinAtk: " + MinAtk +
                " MaxAtk:" + MaxAtk +
                " Def:" + Defence +
                " MAtk:" + MagicAtk +
                " MDef:" + (MagicDef + MagicBlock) +
                " Dext:" + Dexterity +
                " Dodge:" + Dodge +
                " Weight:" + Weight +
                " Range:" + AtkRange +
                " Speed:" + AtkSpeed +
                " Potency:" + Potency
                , MsgTalk.Channel.GM, 0x00FF00));
        }

        public void Move(Int16 NewMap, UInt16 NewX, UInt16 NewY)
        {
            if (!World.AllMaps.ContainsKey(NewMap))
                return;

            Action = (Int16)MsgAction.Emotion.StandBy;

            IsInBattle = false;
            MagicIntone = false;
            Mining = false;

            World.AllMaps[Map].DelEntity(this);

            PrevMap = Map;
            PrevX = X;
            PrevY = Y;

            Map = NewMap;
            X = NewX;
            Y = NewY;

            World.AllMaps[Map].AddEntity(this);

            Send(MsgAction.Create(this, World.AllMaps[NewMap].Id, MsgAction.Action.EnterMap));
            Send(MsgAction.Create(this, (Int32)World.AllMaps[NewMap].Color, MsgAction.Action.MapARGB));
            Send(MsgMapInfo.Create(World.AllMaps[NewMap]));
            if (World.AllMaps[NewMap].Weather != 0)
                Send(MsgWeather.Create(World.AllMaps[NewMap]));

            if (PrevMap == 601 && BlessEndTime != 0)
            {
                Send(MsgUserAttrib.Create(this, 2, MsgUserAttrib.Type.TrainingPoints));
                LastTrainingPointAdd = Environment.TickCount;
            }

            if (Map == 601 && BlessEndTime != 0)
            {
                Send(MsgUserAttrib.Create(this, 1, MsgUserAttrib.Type.TrainingPoints));
                LastTrainingPointAdd = Environment.TickCount;
            }
        }

        public void DropMoney()
        {
            Int32 Silvers = Money / 10;
            if (Silvers < 0)
                Silvers = 0;

            Silvers = MyMath.Generate(0, Silvers);

            Item Item = null;
            if (Silvers <= 10) //Silver
                Item = Item.Create(0, 254, 1090000, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            else if (Silvers <= 100) //Sycee
                Item = Item.Create(0, 254, 1090010, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            else if (Silvers <= 1000) //Gold
                Item = Item.Create(0, 254, 1090020, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            else if (Silvers <= 2000) //GoldBullion
                Item = Item.Create(0, 254, 1091000, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            else if (Silvers <= 5000) //GoldBar
                Item = Item.Create(0, 254, 1091010, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            else if (Silvers > 5000) //GoldBars
                Item = Item.Create(0, 254, 1091020, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            else //Error
                Item = Item.Create(0, 254, 1090000, 0, 0, 0, 0, 0, 0, 0, 0, 0);

            UInt16 ItemX = (UInt16)(X + MyMath.Generate(-1, 1));
            UInt16 ItemY = (UInt16)(Y + MyMath.Generate(-1, 1));

            if (!World.AllMaps[Map].IsValidPoint(ItemX, ItemY))
                return;

            FloorItem FloorItem = new FloorItem(Item, Silvers, 0, Map, ItemX, ItemY);
            World.FloorThread.AddToQueue(FloorItem);

            Money -= Silvers;
            Send(MsgUserAttrib.Create(this, Money, MsgUserAttrib.Type.Money));
        }

        public void DropInventory()
        {
            Int32 Count = (Int32)((Double)ItemInInventory() * 0.20);
            if (Count > ItemInInventory())
                Count = ItemInInventory();

            for (SByte i = 0; i < Count; i++)
            {
                Item[] TmpArray = new Item[Items.Count];
                Items.Values.CopyTo(TmpArray, 0);

                do
                {
                    Int32 Pos = MyMath.Generate(0, TmpArray.Length - 1);
                    Item Item = TmpArray[Pos];

                    if (Item.Position != 0)
                        continue;

                    UInt16 ItemX = (UInt16)(X + MyMath.Generate(-1, 1));
                    UInt16 ItemY = (UInt16)(Y + MyMath.Generate(-1, 1));

                    if (!World.AllMaps[Map].IsValidPoint(ItemX, ItemY))
                        break;

                    DelItem(Item, true);
                    FloorItem FloorItem = new FloorItem(Item, 0, 0, Map, ItemX, ItemY);
                    World.FloorThread.AddToQueue(FloorItem);
                    break;
                }
                while (true);
                TmpArray = null;
            }
        }

        public void DropEquipment()
        {
            Int32 Count = MyMath.Generate(0, 3);
            for (SByte i = 0; i < Count; i++)
            {
                if (MyMath.Success(50))
                {
                    Item Item = GetItemByPos((Byte)MyMath.Generate(0, 8));
                    if (Item == null)
                        return;

                    UInt16 ItemX = (UInt16)(X + MyMath.Generate(-1, 1));
                    UInt16 ItemY = (UInt16)(Y + MyMath.Generate(-1, 1));

                    if (!World.AllMaps[Map].IsValidPoint(ItemX, ItemY))
                        return;

                    Send(MsgItem.Create(Item.UniqId, Item.Position, MsgItem.Action.Unequip));
                    DelItem(Item, true);

                    FloorItem FloorItem = new FloorItem(Item, 0, 0, Map, ItemX, ItemY);
                    World.FloorThread.AddToQueue(FloorItem);
                }
            }
        }

        public void RemoveAtkDura()
        {
            GemEffect();

            if (AutoKill)
                return;

            for (Byte Pos = 4; Pos < 7; Pos++)
            {
                Item Item = GetItemByPos(Pos);
                if (Item == null)
                    continue;

                if (Item.CurDura == 0)
                    continue;

                if (!MyMath.Success(25) && Item.Id / 10000 != 105) //!Arrow
                    continue;

                Item.CurDura--;
                if (Item.CurDura == 0)
                    MyMath.GetEquipStats(this);
                Send(MsgItem.Create(Item.UniqId, Item.CurDura, MsgItem.Action.SynchroAmount));

                if (Item.CurDura == 0)
                    Send(MsgItemInfo.Create(Item, MsgItemInfo.Action.Update));

                if (Item.Id / 10000 == 105 && Item.CurDura == 0)
                {
                    IsInBattle = false;

                    Item Bow = GetItemByPos(4);
                    Send(MsgItem.Create(Item.UniqId, Item.Position, MsgItem.Action.Unequip));
                    Send(MsgItem.Create(Bow.UniqId, Bow.Position, MsgItem.Action.Unequip));
                    Send(MsgItem.Create(Bow.UniqId, Bow.Position, MsgItem.Action.Equip));
                    DelItem(Item, true);

                    if (Owner.AccLvl > 0) //VIP 1
                    {
                        lock (Items)
                        {
                            foreach (Item Arrow in Items.Values)
                            {
                                if (Arrow.Id / 10000 == 105)
                                {
                                    Int32 Lvl = Arrow.Id % 10;
                                    if (Lvl == 1 && Level < 15)
                                        continue;
                                    else if (Lvl == 2 && Level < 40)
                                        continue;
                                    else if (Lvl == 3 && Level < 70)
                                        continue;
                                    else if (Lvl == 4 && Level < 100)
                                        continue;
                                    else if (Lvl == 5 && Level < 110)
                                        continue;
                                    else if (Lvl == 6 && Level < 120)
                                        continue;
                                    else if (Lvl == 7 && Level < 130)
                                        continue;
                                    else if (Lvl == 8 && Level < 140)
                                        continue;

                                    Arrow.Position = 5;
                                    Send(MsgItem.Create(Arrow.UniqId, Arrow.Position, MsgItem.Action.Equip));

                                    MyMath.GetEquipStats(this);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        public void RemoveDefDura()
        {
            BlessEffect();
            for (Byte Pos = 1; Pos < 9; Pos++)
            {
                if (Pos >= 4 && Pos <= 7)
                    continue;

                Item Item = GetItemByPos(Pos);
                if (Item == null)
                    continue;

                if (Item.CurDura == 0)
                    continue;

                if (!MyMath.Success(25))
                    continue;

                Item.CurDura--;
                if (Item.CurDura == 0)
                    MyMath.GetEquipStats(this);
                Send(MsgItem.Create(Item.UniqId, Item.CurDura, MsgItem.Action.SynchroAmount));
            }
        }

        private void GemEffect()
        {
            if (MyMath.Success(15))
            {
                String Effect = "";
                for (Byte Pos = 0; Pos < 9; Pos++)
                {
                    Item Item = GetItemByPos(Pos);
                    if (Item == null)
                        continue;

                    if (Item.Gem1 % 10 != 3 && Item.Gem2 % 10 != 3)
                        continue;

                    if (Item.Gem1 == 3 || Item.Gem2 == 3)
                        if (MyMath.Success(10))
                        {
                            Effect = "phoenix";
                            break;
                        }
                    if (Item.Gem1 == 13 || Item.Gem2 == 13)
                        if (MyMath.Success(10))
                        {
                            Effect = "goldendragon";
                            break;
                        }
                    if (Item.Gem1 == 23 || Item.Gem2 == 23)
                        if (MyMath.Success(10))
                        {
                            Effect = "fastflash";
                            break;
                        }
                    if (Item.Gem1 == 33 || Item.Gem2 == 33)
                        if (MyMath.Success(10))
                        {
                            Effect = "rainbow";
                            break;
                        }
                    if (Item.Gem1 == 43 || Item.Gem2 == 43)
                        if (MyMath.Success(10))
                        {
                            Effect = "goldenkylin";
                            break;
                        }
                    if (Item.Gem1 == 53 || Item.Gem2 == 53)
                        if (MyMath.Success(10))
                        {
                            Effect = "purpleray";
                            break;
                        }
                    if (Item.Gem1 == 63 || Item.Gem2 == 73)
                        if (MyMath.Success(10))
                        {
                            Effect = "moon";
                            break;
                        }
                    if (Item.Gem1 == 73 || Item.Gem2 == 73)
                        if (MyMath.Success(10))
                        {
                            Effect = "recovery";
                            break;
                        }
                }
                if (Effect != "")
                    World.BroadcastRoomMsg(this, MsgName.Create(UniqId, Effect, MsgName.Action.RoleEffect), true);
            }
        }

        private void BlessEffect()
        {
            if (MyMath.Success(15))
            {
                String Effect = "";
                for (Byte Pos = 0; Pos < 9; Pos++)
                {
                    Item Item = GetItemByPos(Pos);
                    if (Item == null)
                        continue;

                    if (Item.Bless == 0)
                        continue;

                    if (Item.Bless == 1)
                        if (MyMath.Success(15))
                        {
                            Effect = "Aegis1";
                            break;
                        }
                    if (Item.Bless > 1 && Item.Bless <= 3)
                        if (MyMath.Success(15))
                        {
                            Effect = "Aegis2";
                            break;
                        }
                    if (Item.Bless > 3 && Item.Bless <= 5)
                        if (MyMath.Success(15))
                        {
                            Effect = "Aegis3";
                            break;
                        }
                    if (Item.Bless > 5)
                        if (MyMath.Success(15))
                        {
                            Effect = "Aegis4";
                            break;
                        }
                }
                if (Effect != "")
                    World.BroadcastRoomMsg(this, MsgName.Create(UniqId, Effect, MsgName.Action.RoleEffect), true);
            }
        }
    }
}
