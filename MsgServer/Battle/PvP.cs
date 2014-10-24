// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.Collections.Generic;
using COServer.Network;
using COServer.Entities;

namespace COServer
{
    public partial class Battle
    {
        public static void PvP(Player Attacker, Player Target)
        {
            try
            {
                Client Client = Attacker.Owner;

                Map Map = null;
                if (!World.AllMaps.TryGetValue(Attacker.Map, out Map))
                    return;

                if (!Attacker.IsAlive())
                {
                    Attacker.IsInBattle = false;
                    return;
                }

                if (Target == null || !Target.IsAlive())
                {
                    Attacker.IsInBattle = false;
                    return;
                }

                if (Attacker.Map != Target.Map)
                {
                    Attacker.IsInBattle = false;
                    return;
                }

                if (!MyMath.CanSee(Attacker.X, Attacker.Y, Target.X, Target.Y, (Attacker.AtkRange + 1)))
                {
                    Attacker.IsInBattle = false;
                    return;
                }

                if (!CanAttack(Attacker, Target))
                {
                    Attacker.IsInBattle = false;
                    return;
                }

                if (Battle.Magic.WeaponSkill(Attacker, Target))
                    return;

                if (Attacker.AtkType != 25 && Target.ContainsFlag(Player.Flag.Flying))
                {
                    Attacker.IsInBattle = false;
                    return;
                }

                if (!Map.IsPkField() && !Map.IsSynMap() && !Map.IsPrisonMap())
                {
                    if (!Target.ContainsFlag(Player.Flag.BlackName) && !Target.ContainsFlag(Player.Flag.Flashing))
                    {
                        if (!Attacker.ContainsFlag(Player.Flag.Flashing))
                        {
                            Attacker.AddFlag(Player.Flag.Flashing);
                            World.BroadcastRoomMsg(Attacker, MsgUserAttrib.Create(Attacker, Attacker.Flags, MsgUserAttrib.Type.Flags), true);
                        }
                        Attacker.BlueNameEndTime = Environment.TickCount + 25000;
                    }
                }

                Attacker.LastAttackTick = Environment.TickCount;
                if (!MyMath.Success(Attacker.Dexterity))
                {
                    World.BroadcastMapMsg(Attacker, MsgInteract.Create(Attacker, Target, 0, (MsgInteract.Action)Attacker.AtkType), true);
                    return;
                }

                Int32 Damage = 0;
                Damage = MyMath.GetDamagePlayer2Player(Attacker, Target);

                if (Attacker.Map != 1039)
                {
                    Attacker.RemoveAtkDura();
                    Target.RemoveDefDura();
                }

                if (!Target.Reflect())
                {
                    World.BroadcastMapMsg(Attacker, MsgInteract.Create(Attacker, Target, Damage, MsgInteract.Action.Attack), true);
                    if (Damage >= Target.CurHP)
                    {
                        Target.Die();
                        World.BroadcastMapMsg(Attacker, MsgInteract.Create(Attacker, Target, 1, MsgInteract.Action.Kill), true);
                        if (!Map.IsPkField() && !Map.IsSynMap() && !Map.IsPrisonMap())
                        {
                            if (!Target.ContainsFlag(Player.Flag.RedName) &&
                                !Target.ContainsFlag(Player.Flag.BlackName) &&
                                !Target.ContainsFlag(Player.Flag.Flashing))
                            {
                                if (Attacker.Enemies.ContainsKey(Target.UniqId))
                                    Attacker.PkPoints += 5;
                                else if (Attacker.Syndicate != null && Target.Syndicate != null
                                    && Attacker.Syndicate.IsAnEnemy(Target.Syndicate.UniqId))
                                    Attacker.PkPoints += 3;
                                else
                                    Attacker.PkPoints += 10;

                                if (Attacker.PkPoints > 30000)
                                    Attacker.PkPoints = 30000;

                                Attacker.Send(MsgUserAttrib.Create(Attacker, Attacker.PkPoints, MsgUserAttrib.Type.PkPoints));

                                if (Attacker.PkPoints >= 30 && Attacker.PkPoints < 100)
                                {
                                    if (!Attacker.ContainsFlag(Player.Flag.RedName))
                                    {
                                        Attacker.AddFlag(Player.Flag.RedName);
                                        World.BroadcastRoomMsg(Attacker, MsgUserAttrib.Create(Attacker, Attacker.Flags, MsgUserAttrib.Type.Flags), true);
                                    }
                                }
                                else if (Attacker.PkPoints >= 100)
                                {
                                    Attacker.DelFlag(Player.Flag.RedName);
                                    if (!Attacker.ContainsFlag(Player.Flag.BlackName))
                                    {
                                        Attacker.AddFlag(Player.Flag.BlackName);
                                        World.BroadcastRoomMsg(Attacker, MsgUserAttrib.Create(Attacker, Attacker.Flags, MsgUserAttrib.Type.Flags), true);
                                    }
                                }

                                if (Target.BlessEndTime != 0 && Attacker.BlessEndTime == 0)
                                {
                                    if (Attacker.CurseEndTime == 0)
                                    {
                                        Byte Param = 1;
                                        if (Attacker.BlessEndTime != 0)
                                            Param += 2;
                                        World.BroadcastRoomMsg(Attacker, MsgUserAttrib.Create(Attacker, Param, MsgUserAttrib.Type.SizeAdd), true);
                                    }
                                    Attacker.CurseEndTime = Environment.TickCount + (1200 * 1000);
                                    Attacker.Send(MsgUserAttrib.Create(Attacker, 1200, MsgUserAttrib.Type.CurseTime));
                                }
                            }

                            if (!Target.Enemies.ContainsKey(Attacker.UniqId))
                            {
                                Target.Enemies.Add(Attacker.UniqId, Attacker.Name);
                                Target.Send(MsgFriend.Create(Attacker.UniqId, Attacker.Name, true, MsgFriend.Action.EnemyAdd));
                            }

                            if (Target.BlessEndTime == 0)
                            {
                                UInt64 Exp = (UInt64)(Target.Exp * 0.02);
                                if (Target.ContainsFlag(Player.Flag.RedName))
                                    Exp = (UInt64)(Target.Exp * 0.10);
                                else if (Target.ContainsFlag(Player.Flag.BlackName))
                                    Exp = (UInt64)(Target.Exp * 0.20);

                                //if (Target.Syndicate != null)
                                //{
                                //    Syndicate.Member Member = Target.Syndicate.GetMemberInfo(Target.UniqId);
                                //    if (Member != null)
                                //    {
                                //        UInt64 Compensation = 0;
                                //        if (Member.Donation > 0)
                                //        {
                                //            if (Member.Rank == 100) //Guild Leader
                                //                Compensation = (UInt64)(Exp * 0.80);
                                //            else if (Member.Rank == 90) //Deputy Leader
                                //                Compensation = (UInt64)(Exp * 0.60);
                                //            else if (Member.Rank == 80)
                                //                Compensation = (UInt64)(Exp * 0.40);
                                //            else if (Member.Rank == 70)
                                //                Compensation = (UInt64)(Exp * 0.35);
                                //            else if (Member.Rank == 60)
                                //                Compensation = (UInt64)(Exp * 0.30);
                                //            else
                                //                Compensation = (UInt64)(Exp * 0.25);
                                //        }
                                //        else
                                //            Compensation = (UInt64)(Exp * 0.20);

                                //        if (Compensation > Int32.MaxValue)
                                //            Compensation = Int32.MaxValue;

                                //        Target.Syndicate.Money -= (Int32)Compensation;
                                //        Member.Donation -= (Int32)Compensation;
                                //        Exp -= Compensation;

                                //        World.SynThread.AddToQueue(Target.Syndicate, "Money", Target.Syndicate.Money);
                                //    }
                                //}

                                if (Target.Exp < Exp)
                                    Target.Exp = 0;
                                else
                                    Target.Exp -= Exp;
                                Target.Send(MsgUserAttrib.Create(Target, (Int64)Target.Exp, MsgUserAttrib.Type.Exp));
                                Attacker.AddExp(Exp * 0.10, false);
                            }

                            {
                                Int32 CPs = (Int32)(Target.CPs * 0.02);
                                if (Target.ContainsFlag(Player.Flag.RedName))
                                    CPs = (Int32)(Target.CPs * 0.10);
                                else if (Target.ContainsFlag(Player.Flag.BlackName))
                                    CPs = (Int32)(Target.CPs * 0.20);

                                Int32 Compensation = 0;
                                if (Target.Syndicate != null)
                                {
                                    Syndicate.Member Member = Target.Syndicate.GetMemberInfo(Target.UniqId);
                                    if (Member != null)
                                    {
                                        if (Member.Rank == 100) //Guild Leader
                                            Compensation = (Int32)(CPs * 0.80);
                                        else if (Member.Rank == 90) //Deputy Leader
                                            Compensation = (Int32)(CPs * 0.60);
                                        else if (Member.Rank == 80)
                                            Compensation = (Int32)(CPs * 0.40);
                                        else if (Member.Rank == 70)
                                            Compensation = (Int32)(CPs * 0.35);
                                        else if (Member.Rank == 60)
                                            Compensation = (Int32)(CPs * 0.30);
                                        else
                                            Compensation = (Int32)(CPs * 0.25);
                                    }
                                }

                                if (Target.CPs < CPs)
                                    Target.CPs = 0;
                                else
                                    Target.CPs -= CPs + Compensation;
                                Target.Send(MsgUserAttrib.Create(Target, Target.CPs, MsgUserAttrib.Type.CPs));

                                Attacker.CPs += CPs;
                                Attacker.Send(MsgUserAttrib.Create(Attacker, Attacker.CPs, MsgUserAttrib.Type.CPs));
                            }

                            if (Target.ContainsFlag(Player.Flag.RedName) ||
                                Target.ContainsFlag(Player.Flag.BlackName))
                                Target.DropEquipment();

                            if (Target.ContainsFlag(Player.Flag.BlackName))
                            {
                                World.BroadcastMsg(MsgTalk.Create("SYSTEM", "ALLUSERS", Client.GetStr("STR_SENT_TO_JAIL"), MsgTalk.Channel.GM, 0xFF0000));
                                Target.Move(6000, 29, 72);
                            }
                        }
                    }
                    else
                    {
                        Target.CurHP -= Damage;
                        Target.Send(MsgUserAttrib.Create(Target, Target.CurHP, MsgUserAttrib.Type.Life));
                        if (Target.Team != null)
                            World.BroadcastTeamMsg(Target.Team, MsgUserAttrib.Create(Target, Target.CurHP, MsgUserAttrib.Type.Life));

                        if (Target.Action == (Byte)MsgAction.Emotion.SitDown)
                        {
                            Target.Energy /= 2;
                            Target.Send(MsgUserAttrib.Create(Target, Target.Energy, MsgUserAttrib.Type.Energy));
                            
                            Target.Action = (Byte)MsgAction.Emotion.StandBy;
                            World.BroadcastRoomMsg(Target, MsgAction.Create(Target, (Byte)MsgAction.Emotion.StandBy, MsgAction.Action.Emotion), true);
                        }
                    }
                }
                else
                {
                    if (Damage > 2000)
                        Damage = 2000;

                    World.BroadcastMapMsg(Attacker, MsgInteract.Create(Target, Attacker, Damage, MsgInteract.Action.ReflectWeapon), true);
                    if (Damage >= Attacker.CurHP)
                    {
                        Attacker.Die();
                        World.BroadcastMapMsg(Target, MsgInteract.Create(Target, Attacker, 1, MsgInteract.Action.Kill), true);
                    }
                    else
                    {
                        Attacker.CurHP -= Damage;
                        Attacker.Send(MsgUserAttrib.Create(Attacker, Attacker.CurHP, MsgUserAttrib.Type.Life));
                        if (Attacker.Team != null)
                            World.BroadcastTeamMsg(Attacker.Team, MsgUserAttrib.Create(Attacker, Attacker.CurHP, MsgUserAttrib.Type.Life));
                    }
                }
                if (Attacker.Map != 1039)
                    Battle.Magic.WeaponAttribute(Attacker, Target);
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }
    }
}
