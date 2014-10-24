// * Created by Jean-Philippe Boivin
// * Copyright © 2010
// * Logik. Project

using System;
using System.Threading;
//using System.Timers;
using System.Collections.Generic;
using COServer.Network;
using COServer.Entities;

namespace COServer.Threads
{
    public class PlayerThread
    {
        private Thread Thread;
        private Player Owner;

        private Boolean Dispose = false;

        public PlayerThread(Player Owner)
        {
            this.Owner = Owner;

            Thread = new Thread(Process);
            Thread.IsBackground = true;
            Thread.Start();

            //Thread = new Timer();
            //Thread.Elapsed += new ElapsedEventHandler(Process);
            //Thread.Interval = 500;
            //Thread.Start();
        }

        public void Stop() { Dispose = true; }

        ~PlayerThread()
        {
            //if (Thread != null)
            //    Thread.Dispose();
            //Thread = null;
            Dispose = true;
        }

        private void Process()//Object Sender, ElapsedEventArgs Args)
        {
            //Thread.Stop();
            while (!Dispose)
            {
                try
                {
                    if (Owner.BotCheck >= 10)
                    {
                        lock (World.AllMasters)
                        {
                            foreach (Player Master in World.AllMasters.Values)
                                Master.Send(MsgTalk.Create("SYSTEM", "ALLUSERS", String.Format("[CRIME] {0} ({1}) : AUTOCLICK_CHEAT", Owner.Name, Owner.UniqId), MsgTalk.Channel.GM, 0xFF00FF));
                        }
                        Owner.BotCheck = 0;
                    }

                    //Reset speedhack check each 3 mins
                    if (Environment.TickCount - Owner.PrevSpeedHackReset > 180000)
                    {
                        Owner.PrevSpeedHackReset = Environment.TickCount;
                        Owner.SpeedHack = 0;
                    }

                    if (Owner.SpeedHack >= 15)
                    {
                        Owner.SpeedHack = 0;
                        Database.Jail(Owner.Name);

                        Owner.JailC++;
                        Owner.Move(6001, 28, 75);

                        Program.Log("[CRIME] " + Owner.Name + " has been sent to jail for using a speed hack!");
                        World.BroadcastMsg(MsgTalk.Create("SYSTEM", "ALLUSERS", Owner.Name + " has been sent to jail!", MsgTalk.Channel.GM, 0xFFFFFF));
                    }

                    if (Environment.TickCount - Owner.LastSave > 20000)
                    {
                        Database.Save(Owner);
                        Owner.LastSave = Environment.TickCount;
                    }

                    if (Environment.TickCount - Owner.LastRcvClientTick > 15000)
                    {
                        //Should disconnect, but eh... The host is really shitty.
                        //Owner.Disconnect();
                        //break;
                    }

                    if (Environment.TickCount - Owner.LastReqClientTick >= 10000)
                    {
                        Owner.LastReqClientTick = Environment.TickCount;
                        Owner.Send(MsgTick.Create(Owner));
                    }

                    if (!Owner.ToS && Environment.TickCount - Owner.ConnectionTime >= 150000)
                        Owner.Disconnect();

                    if (Owner.PremiumEndTime.CompareTo(DateTime.UtcNow) <= 0)
                    {
                        if (Owner.Owner.AccLvl == 1)
                        {
                            Owner.Owner.AccLvl = 0;
                            Owner.Send(MsgTalk.Create("SYSTEM", "ALLUSERS", "Vous n'etes plus premium!", MsgTalk.Channel.Talk, 0xFFFFFF));
                        }
                    }

                    if (Owner.IsInBattle && Environment.TickCount - Owner.LastAttackTick >= Owner.AtkSpeed)
                    {
                        if (Entity.IsPlayer(Owner.TargetUID))
                        {
                            Player Target = null;
                            if (World.AllPlayers.TryGetValue(Owner.TargetUID, out Target))
                            {
                                if (Owner.MagicType != -1)
                                    Battle.Magic.Use(Owner, Target, Target.X, Target.Y);
                                else
                                    Battle.PvP(Owner, Target);
                            }
                            else
                                Owner.IsInBattle = false;
                        }
                        else if (Entity.IsMonster(Owner.TargetUID))
                        {
                            Monster Target = null;
                            if (World.AllMonsters.TryGetValue(Owner.TargetUID, out Target))
                            {
                                if (Owner.MagicType != -1)
                                    Battle.Magic.Use(Owner, Target, Target.X, Target.Y);
                                else
                                    Battle.PvM(Owner, Target);
                            }
                            else
                                Owner.IsInBattle = false;
                        }
                        else if (Entity.IsTerrainNPC(Owner.TargetUID))
                        {
                            TerrainNPC Target = null;
                            if (World.AllTerrainNPCs.TryGetValue(Owner.TargetUID, out Target))
                            {
                                if (Owner.MagicType != -1)
                                    Battle.Magic.Use(Owner, Target, Target.X, Target.Y);
                                else
                                    Battle.PvE(Owner, Target);
                            }
                            else
                                Owner.IsInBattle = false;
                        }
                        else if (Owner.TargetUID == 0)
                        {
                            if (Owner.MagicType != -1)
                                Battle.Magic.Use(Owner, null, Owner.X, Owner.Y);
                        }
                        else
                            Owner.IsInBattle = false;
                    }

                    if (Owner.PkPoints > 0 && Environment.TickCount - Owner.LastPkPointRemove > 120000)
                    {
                        Owner.LastPkPointRemove = Environment.TickCount;
                        if (Owner.PkPoints == 100)
                        {
                            Owner.DelFlag(Player.Flag.BlackName);
                            Owner.AddFlag(Player.Flag.RedName);
                            World.BroadcastRoomMsg(Owner, MsgUserAttrib.Create(Owner, Owner.Flags, MsgUserAttrib.Type.Flags), true);
                        }
                        else if (Owner.PkPoints == 30)
                        {
                            Owner.DelFlag(Player.Flag.RedName);
                            World.BroadcastRoomMsg(Owner, MsgUserAttrib.Create(Owner, Owner.Flags, MsgUserAttrib.Type.Flags), true);
                        }
                        Owner.PkPoints--;
                        Owner.Send(MsgUserAttrib.Create(Owner, Owner.PkPoints, MsgUserAttrib.Type.PkPoints));
                    }

                    if (Owner.DblExpEndTime != 0)
                    {
                        if (Environment.TickCount >= Owner.DblExpEndTime)
                        {
                            Owner.DblExpEndTime = 0;
                            Owner.Send(MsgUserAttrib.Create(Owner, Owner.DblExpEndTime, MsgUserAttrib.Type.DblExpTime));
                        }
                    }

                    #region LuckyTime
                    if (Owner.LuckyTime != 0 && !(Owner.Praying || Owner.CastingPray))
                    {
                        Owner.LuckyTime -= 500;
                        if (Owner.LuckyTime <= 0)
                            Owner.LuckyTime = 0;
                        Owner.Send(MsgUserAttrib.Create(Owner, Owner.LuckyTime, MsgUserAttrib.Type.LuckyTime));
                    }

                    if (Owner.CastingPray)
                    {
                        if (Owner.X != Owner.PrayX || Owner.Y != Owner.PrayY || Owner.Map != Owner.PrayMap || Owner.IsInBattle)
                        {
                            Owner.CastingPray = false;
                            Owner.PrayX = 0;
                            Owner.PrayY = 0;

                            Owner.DelFlag(Player.Flag.CastingPray);
                            World.BroadcastRoomMsg(Owner, MsgUserAttrib.Create(Owner, Owner.Flags, MsgUserAttrib.Type.Flags), true);

                            Map Map = World.AllMaps[Owner.Map];

                            List<Player> Casters = new List<Player>();
                            List<Player> Prayers = new List<Player>();
                            foreach (Entity Entity in Map.Entities.Values)
                            {
                                if (!Entity.IsPlayer())
                                    continue;

                                Player Player2 = Entity as Player;
                                if (Player2.CastingPray)
                                    Casters.Add(Player2);

                                if (Player2.Praying)
                                    Prayers.Add(Player2);
                            }

                            foreach (Player Prayer in Prayers)
                            {
                                Boolean Stop = true;
                                foreach (Player Caster in Casters)
                                {
                                    if (MyMath.CanSee(Prayer.X, Prayer.Y, Caster.PrayX, Caster.PrayY, 4))
                                    {
                                        Stop = false;
                                        break;
                                    }
                                }

                                if (Stop)
                                {
                                    Prayer.Praying = false;

                                    Prayer.DelFlag(Player.Flag.Praying);
                                    World.BroadcastRoomMsg(Prayer, MsgUserAttrib.Create(Prayer, Prayer.Flags, MsgUserAttrib.Type.Flags), true);
                                }
                            }
                        }
                        else
                        {

                            Owner.LuckyTime += 1500;
                            if (Owner.LuckyTime > Player.MAX_LUCKY_TIME)
                                Owner.LuckyTime = Player.MAX_LUCKY_TIME;
                        }
                    }

                    if (Owner.Praying)
                    {
                        if (Owner.X != Owner.PrayX || Owner.Y != Owner.PrayY || Owner.Map != Owner.PrayMap || Owner.IsInBattle)
                        {
                            Owner.Praying = false;
                            Owner.PrayMap = 0;
                            Owner.PrayX = 0;
                            Owner.PrayY = 0;

                            Owner.DelFlag(Player.Flag.Praying);
                            World.BroadcastRoomMsg(Owner, MsgUserAttrib.Create(Owner, Owner.Flags, MsgUserAttrib.Type.Flags), true);
                        }

                        Owner.LuckyTime += 500;
                        if (Owner.LuckyTime > Player.MAX_LUCKY_TIME)
                            Owner.LuckyTime = Player.MAX_LUCKY_TIME;
                    }
                    else
                    {
                        List<Player> Casters = new List<Player>();
                        foreach (Entity Entity in Owner.Screen.Entities.Values)
                        {
                            if (!Entity.IsPlayer())
                                continue;

                            Player Caster = Entity as Player;
                            if (Caster.CastingPray)
                                Casters.Add(Caster);
                        }

                        foreach (Player Caster in Casters)
                        {
                            if (MyMath.CanSee(Owner.X, Owner.Y, Caster.PrayX, Caster.PrayY, 4))
                            {
                                Owner.Praying = true;
                                Owner.PrayMap = Owner.Map;
                                Owner.PrayX = Owner.X;
                                Owner.PrayY = Owner.Y;

                                Owner.AddFlag(Player.Flag.Praying);
                                World.BroadcastRoomMsg(Owner, MsgUserAttrib.Create(Owner, Owner.Flags, MsgUserAttrib.Type.Flags), true);
                                break;
                            }
                            break;
                        }
                    }
                    #endregion

                    if (Owner.BlessEndTime != 0)
                    {
                        DateTime BlessEnd = DateTime.FromBinary(Owner.BlessEndTime);
                        if (BlessEnd.CompareTo(DateTime.UtcNow) <= 0)
                        {
                            Owner.BlessEndTime = 0;
                            Owner.Send(MsgUserAttrib.Create(Owner, Owner.BlessEndTime, MsgUserAttrib.Type.BlessTime));

                            Byte Param = 0;
                            if (Owner.CurseEndTime != 0)
                                Param = 1;
                            World.BroadcastRoomMsg(Owner, MsgUserAttrib.Create(Owner, Param, MsgUserAttrib.Type.SizeAdd), true);
                        }
                    }

                    if (Owner.CurseEndTime != 0)
                    {
                        if (Environment.TickCount >= Owner.CurseEndTime)
                        {
                            Owner.CurseEndTime = 0;
                            Owner.Send(MsgUserAttrib.Create(Owner, Owner.CurseEndTime, MsgUserAttrib.Type.CurseTime));

                            Byte Param = 0;
                            if (Owner.BlessEndTime != 0)
                                Param = 2;
                            World.BroadcastRoomMsg(Owner, MsgUserAttrib.Create(Owner, Param, MsgUserAttrib.Type.SizeAdd), true);
                        }
                    }

                    if (Owner.Team != null && Owner.Team.Leader.UniqId == Owner.UniqId)
                    {
                        if (Environment.TickCount - Owner.LastLeaderPosTick > 5000)
                        {
                            Owner.Team.LeaderXY();
                            Owner.LastLeaderPosTick = Environment.TickCount;
                        }
                    }

                    if (Owner.IsAlive())
                    {
                        Byte MaxEnergy = 100;
                        if (Owner.BlessEndTime != 0)
                            MaxEnergy = 150;

                        if (Owner.CurHP > UInt16.MaxValue)
                            Owner.Die();

                        if (Owner.Action == (Int16)MsgAction.Emotion.SitDown && !Owner.ContainsFlag(Player.Flag.Flying))
                        {
                            if (Owner.Energy < MaxEnergy)
                            {
                                Owner.Energy += 8;
                                if (Owner.Energy > MaxEnergy)
                                    Owner.Energy = MaxEnergy;
                                Owner.Send(MsgUserAttrib.Create(Owner, Owner.Energy, MsgUserAttrib.Type.Energy));
                            }
                        }
                        if (Owner.Action == (Int16)MsgAction.Emotion.StandBy && !Owner.ContainsFlag(Player.Flag.Flying))
                        {
                            if (Owner.Energy < MaxEnergy)
                            {
                                Owner.Energy++;
                                if (Owner.Energy > MaxEnergy)
                                    Owner.Energy = MaxEnergy;
                                Owner.Send(MsgUserAttrib.Create(Owner, Owner.Energy, MsgUserAttrib.Type.Energy));
                            }
                        }

                        if ((Owner.ContainsFlag(Player.Flag.XPList) && Environment.TickCount - Owner.LastXPAdd > 20000)
                            || (!Owner.ContainsFlag(Player.Flag.XPList) && Environment.TickCount - Owner.LastXPAdd > 3000))
                        {
                            Owner.XP++;
                            if (Owner.XP == 100)
                            {
                                Owner.AddFlag(Player.Flag.XPList);
                                Owner.Send(MsgUserAttrib.Create(Owner, Owner.Flags, MsgUserAttrib.Type.Flags));
                            }
                            if (Owner.XP > 100)
                            {
                                Owner.XP = 0;
                                Owner.DelFlag(Player.Flag.XPList);
                                Owner.Send(MsgUserAttrib.Create(Owner, Owner.Flags, MsgUserAttrib.Type.Flags));
                            }
                            if (Owner.XP % 20 == 0)
                                Owner.Send(MsgUserAttrib.Create(Owner, Owner.XP, MsgUserAttrib.Type.XP));
                            Owner.LastXPAdd = Environment.TickCount;
                        }

                        if (Owner.BlessEndTime != 0)
                        {
                            if (Owner.Map != 601 && Environment.TickCount - Owner.LastTrainingPointAdd > 60000)
                            {
                                Owner.TrainingPoints++;
                                Owner.Send(MsgUserAttrib.Create(Owner, 3, MsgUserAttrib.Type.TrainingPoints));

                                if (Owner.TrainingPoints == 10)
                                {
                                    Owner.Send(MsgUserAttrib.Create(Owner, 4, MsgUserAttrib.Type.TrainingPoints));

                                    UInt64 Exp = Owner.CalcExpBall((Byte)Owner.Level, Owner.Exp, 0.1);
                                    Owner.AddExp(Exp, false);
                                }

                                if (Owner.TrainingPoints > 10)
                                    Owner.TrainingPoints = 1;

                                Owner.LastTrainingPointAdd = Environment.TickCount;
                            }

                            if (Owner.Map != 601 && Environment.TickCount - Owner.LastTrainingTimeAdd > 60000)
                            {
                                Owner.MaxTrainingTime += 10;

                                if (Owner.MaxTrainingTime > 900)
                                    Owner.MaxTrainingTime = 900;

                                Owner.LastTrainingTimeAdd = Environment.TickCount;
                            }
                        }

                        if (Owner.TransformEndTime != 0)
                        {
                            if (Environment.TickCount >= Owner.TransformEndTime)
                            {
                                Owner.TransformEndTime = 0;
                                Owner.DelTransform();

                                if (Owner.CurHP >= Owner.MaxHP)
                                    Owner.CurHP = Owner.MaxHP;
                                Double Multiplier = (Double)Owner.CurHP / (Double)Owner.MaxHP;
                                MyMath.GetHitPoints(Owner, true);
                                Owner.CurHP = (Int32)(Owner.MaxHP * Multiplier);

                                MyMath.GetMagicPoints(Owner, true);
                                MyMath.GetEquipStats(Owner);

                                Owner.Send(MsgUserAttrib.Create(Owner, Owner.CurHP, MsgUserAttrib.Type.Life));
                                Owner.Send(MsgUserAttrib.Create(Owner, Owner.MaxHP, (MsgUserAttrib.Type.Life + 1)));
                                if (Owner.Team != null)
                                {
                                    World.BroadcastTeamMsg(Owner.Team, MsgUserAttrib.Create(Owner, Owner.CurHP, MsgUserAttrib.Type.Life));
                                    World.BroadcastTeamMsg(Owner.Team, MsgUserAttrib.Create(Owner, Owner.MaxHP, (MsgUserAttrib.Type.Life + 1)));
                                }
                                World.BroadcastRoomMsg(Owner, MsgUserAttrib.Create(Owner, Owner.Look, MsgUserAttrib.Type.Look), true);
                            }
                        }

                        if (Owner.FlyEndTime != 0 && Environment.TickCount >= Owner.FlyEndTime)
                        {
                            Owner.FlyEndTime = 0;
                            Owner.DelFlag(Player.Flag.Flying);

                            World.BroadcastRoomMsg(Owner, MsgUserAttrib.Create(Owner, Owner.Flags, MsgUserAttrib.Type.Flags), true);
                        }

                        if (Owner.BlueNameEndTime != 0)
                        {
                            if (Environment.TickCount >= Owner.BlueNameEndTime)
                            {
                                Owner.BlueNameEndTime = 0;

                                Owner.DelFlag(Player.Flag.Flashing);
                                World.BroadcastRoomMsg(Owner, MsgUserAttrib.Create(Owner, Owner.Flags, MsgUserAttrib.Type.Flags), true);
                            }
                        }

                        if (Owner.AccuracyEndTime != 0 && Environment.TickCount >= Owner.AccuracyEndTime)
                        {
                            Owner.AccuracyEndTime = 0;
                            Owner.DexterityBonus = 0;
                            Owner.DelFlag(Player.Flag.Accuracy);
                            MyMath.GetEquipStats(Owner);

                            World.BroadcastRoomMsg(Owner, MsgUserAttrib.Create(Owner, Owner.Flags, MsgUserAttrib.Type.Flags), true);
                        }

                        if (Owner.ShieldEndTime != 0 && Environment.TickCount >= Owner.ShieldEndTime)
                        {
                            Owner.ShieldEndTime = 0;
                            Owner.DefenceBonus = 0;
                            Owner.DelFlag(Player.Flag.Shield);
                            MyMath.GetEquipStats(Owner);

                            World.BroadcastRoomMsg(Owner, MsgUserAttrib.Create(Owner, Owner.Flags, MsgUserAttrib.Type.Flags), true);
                        }

                        if (Owner.SupermanEndTime != 0 && Environment.TickCount >= Owner.SupermanEndTime)
                        {
                            if (Owner.CurKO > Owner.KO)
                            {
                                Owner.KO = Owner.CurKO;
                                if (Owner.CurKO > 15000)
                                    Owner.TimeAdd = 3;
                                else if (Owner.CurKO > 10000)
                                    Owner.TimeAdd = 2;
                                else if (Owner.CurKO > 5000)
                                    Owner.TimeAdd = 1;
                                Owner.Send(MsgUserAttrib.Create(Owner, Owner.TimeAdd, MsgUserAttrib.Type.TimeAdd));
                            }
                            if (Owner.CurKO > 1000)
                                World.BroadcastMsg(MsgTalk.Create("SYSTEM", "ALLUSERS", Owner.Name + " a tué " + Owner.CurKO + " monstres en KO!", MsgTalk.Channel.Talk, 0xFFFFFF));
                            Owner.CurKO = 0;

                            Owner.SupermanEndTime = 0;
                            Owner.AttackBonus = 0;
                            Owner.DelFlag(Player.Flag.SuperMan);
                            MyMath.GetEquipStats(Owner);

                            World.BroadcastRoomMsg(Owner, MsgUserAttrib.Create(Owner, Owner.Flags, MsgUserAttrib.Type.Flags), true);
                        }

                        if (Owner.InvisibilityEndTime != 0 && Environment.TickCount >= Owner.InvisibilityEndTime)
                        {
                            Owner.InvisibilityEndTime = 0;
                            Owner.DelFlag(Player.Flag.Invisibility);

                            World.BroadcastRoomMsg(Owner, MsgUserAttrib.Create(Owner, Owner.Flags, MsgUserAttrib.Type.Flags), true);
                        }

                        if (Owner.StarOfAccuracyEndTime != 0 && Environment.TickCount >= Owner.StarOfAccuracyEndTime)
                        {
                            Owner.StarOfAccuracyEndTime = 0;
                            Owner.DexterityBonus = 0;
                            Owner.DelFlag(Player.Flag.Accuracy);
                            MyMath.GetEquipStats(Owner);

                            World.BroadcastRoomMsg(Owner, MsgUserAttrib.Create(Owner, Owner.Flags, MsgUserAttrib.Type.Flags), true);
                        }

                        if (Owner.MagicShieldEndTime != 0 && Environment.TickCount >= Owner.MagicShieldEndTime)
                        {
                            Owner.MagicShieldEndTime = 0;
                            Owner.DefenceBonus = 0;
                            Owner.DelFlag(Player.Flag.Shield);
                            MyMath.GetEquipStats(Owner);

                            World.BroadcastRoomMsg(Owner, MsgUserAttrib.Create(Owner, Owner.Flags, MsgUserAttrib.Type.Flags), true);
                        }

                        if (Owner.AzureShieldEndTime != 0 && Environment.TickCount >= Owner.AzureShieldEndTime)
                        {
                            Owner.AzureShieldEndTime = 0;
                            Owner.DefenceAddBonus = 0;
                            Owner.DelFlag(Player.Flag.AzureShield);
                            MyMath.GetEquipStats(Owner);

                            World.BroadcastRoomMsg(Owner, MsgUserAttrib.Create(Owner, Owner.Flags, MsgUserAttrib.Type.Flags), true);
                            World.BroadcastRoomMsg(Owner, MsgName.Create(Owner.UniqId, "sscs_htsd_dis", MsgName.Action.RoleEffect), true);
                        }

                        if (Owner.StigmaEndTime != 0 && Environment.TickCount >= Owner.StigmaEndTime)
                        {
                            Owner.StigmaEndTime = 0;
                            Owner.AttackBonus = 0;
                            Owner.DelFlag(Player.Flag.Stigma);
                            MyMath.GetEquipStats(Owner);

                            World.BroadcastRoomMsg(Owner, MsgUserAttrib.Create(Owner, Owner.Flags, MsgUserAttrib.Type.Flags), true);
                        }

                        if (Owner.CycloneEndTime != 0 && Environment.TickCount >= Owner.CycloneEndTime)
                        {
                            if (Owner.CurKO > Owner.KO)
                            {
                                Owner.KO = Owner.CurKO;
                                if (Owner.CurKO > 15000)
                                    Owner.TimeAdd = 3;
                                else if (Owner.CurKO > 10000)
                                    Owner.TimeAdd = 2;
                                else if (Owner.CurKO > 5000)
                                    Owner.TimeAdd = 1;
                                Owner.Send(MsgUserAttrib.Create(Owner, Owner.TimeAdd, MsgUserAttrib.Type.TimeAdd));
                            }
                            if (Owner.CurKO > 1000)
                                World.BroadcastMsg(MsgTalk.Create("SYSTEM", "ALLUSERS", Owner.Name + " a tué " + Owner.CurKO + " monstres en KO!", MsgTalk.Channel.Talk, 0xFFFFFF));
                            Owner.CurKO = 0;

                            Owner.CycloneEndTime = 0;
                            Owner.SpeedBonus = 0;
                            Owner.DelFlag(Player.Flag.Cyclone);
                            MyMath.GetEquipStats(Owner);

                            World.BroadcastRoomMsg(Owner, MsgUserAttrib.Create(Owner, Owner.Flags, MsgUserAttrib.Type.Flags), true);
                        }

                        if (Owner.Mining && Environment.TickCount - Owner.LastPickSwingTick > 3000)
                            Owner.Mine();

                        if (Owner.AutoKill)
                        {
                            Owner.AtkType = 2;
                            Owner.AtkRange = 25;
                            Owner.DblExpEndTime = Environment.TickCount + 3600000;
                            Owner.LuckyTime = Player.MAX_LUCKY_TIME;

                            Map Map = World.AllMaps[Owner.Map];
                            Object[] Entities = new Object[Map.Entities.Count];
                            Map.Entities.Values.CopyTo(Entities, 0);

                            foreach (Object Obj in Entities)
                            {
                                Monster Monster = Obj as Monster;
                                if (Monster == null)
                                    continue;

                                if (!MyMath.CanSee(Owner.X, Owner.Y, Monster.X, Monster.Y, Owner.AtkRange))
                                    continue;

                                Battle.PvM(Owner, Monster);
                            }
                        }
                    }
                }
                catch (Exception Exc) { Program.WriteLine(Exc); }
                Thread.Sleep(500);
            }
                //Thread.Start();
        }
    }
}
