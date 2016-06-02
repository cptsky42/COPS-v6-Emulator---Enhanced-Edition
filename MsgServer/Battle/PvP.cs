// * Created by Jean-Philippe Boivin
// * Copyright © 2011, 2015
// * COPS v6 Emulator

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

                if (Battle.WeaponSkill(Attacker, Target))
                    return;

                if (Attacker.AtkType != 25 && Target.IsFlying())
                {
                    Attacker.IsInBattle = false;
                    return;
                }

                if (!Attacker.Map.IsPkField() && !Attacker.Map.IsSynMap() && !Attacker.Map.IsPrisonMap())
                {
                    if (!Target.IsCriminal())
                        Attacker.AttachStatus(Status.Crime, 25000);
                }

                Attacker.LastAttackTick = Environment.TickCount;
                if (!MyMath.Success(Attacker.Dexterity))
                {
                    World.BroadcastMapMsg(Attacker, new MsgInteract(Attacker, Target, 0, (MsgInteract.Action)Attacker.AtkType), true);
                    return;
                }

                Int32 Damage = 0;
                Damage = MyMath.GetDamagePlayer2Player(Attacker, Target);

                if (Attacker.Map.Id !=  1039)
                {
                    Attacker.RemoveAtkDura();
                    Target.RemoveDefDura();
                }

                if (!Target.Reflect())
                {
                    World.BroadcastMapMsg(Attacker, new MsgInteract(Attacker, Target, Damage, MsgInteract.Action.Attack), true);
                    if (Damage >= Target.CurHP)
                    {
                        Target.Die(Attacker);
                        World.BroadcastMapMsg(Attacker, new MsgInteract(Attacker, Target, 1, MsgInteract.Action.Kill), true);
                    }
                    else
                    {
                        Target.CurHP -= Damage;
                        Target.Send(new MsgUserAttrib(Target, Target.CurHP, MsgUserAttrib.AttributeType.Life));
                        if (Target.Team != null)
                            Target.Team.BroadcastMsg(new MsgUserAttrib(Target, Target.CurHP, MsgUserAttrib.AttributeType.Life));

                        if (Target.Action == Emotion.SitDown)
                        {
                            Target.Energy /= 2;
                            Target.Send(new MsgUserAttrib(Target, Target.Energy, MsgUserAttrib.AttributeType.Energy));

                            Target.Action = Emotion.StandBy;
                            World.BroadcastRoomMsg(Target, new MsgAction(Target, (int)Target.Action, MsgAction.Action.Emotion), true);
                        }
                    }
                }
                else
                {
                    if (Damage > 2000)
                        Damage = 2000;

                    World.BroadcastMapMsg(Attacker, new MsgInteract(Target, Attacker, Damage, MsgInteract.Action.ReflectWeapon), true);
                    if (Damage >= Attacker.CurHP)
                    {
                        Attacker.Die(null);
                        World.BroadcastMapMsg(Target, new MsgInteract(Target, Attacker, 1, MsgInteract.Action.Kill), true);
                    }
                    else
                    {
                        Attacker.CurHP -= Damage;
                        Attacker.Send(new MsgUserAttrib(Attacker, Attacker.CurHP, MsgUserAttrib.AttributeType.Life));
                        if (Attacker.Team != null)
                            Attacker.Team.BroadcastMsg(new MsgUserAttrib(Attacker, Attacker.CurHP, MsgUserAttrib.AttributeType.Life));
                    }
                }
                if (Attacker.Map.Id !=  1039)
                    Battle.WeaponAttribute(Attacker, Target);
            }
            catch (Exception exc) { sLogger.Error(exc); }
        }
    }
}
