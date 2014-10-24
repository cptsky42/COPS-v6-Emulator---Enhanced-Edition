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
        public static void MvP(Monster Attacker, Player Target)
        {
            try
            {
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

                if (!MyMath.CanSee(Attacker.X, Attacker.Y, Target.X, Target.Y, (Attacker.AtkRange)))
                {
                    Attacker.IsInBattle = false;
                    return;
                }

                if (Target.ContainsFlag(Player.Flag.Flying))
                {
                    Attacker.IsInBattle = false;
                    return;
                }

                if (!MyMath.Success(Attacker.Dexterity))
                {
                    World.BroadcastMapMsg(Attacker.Map, MsgInteract.Create(Attacker, Target, 0, (MsgInteract.Action)Attacker.AtkType));
                    return;
                }

                Int32 Damage = 0;
                Damage = MyMath.GetDamageMonster2Player(Attacker, Target);

                Target.RemoveDefDura();

                if (!Target.Reflect())
                {
                    World.BroadcastMapMsg(Attacker.Map, MsgInteract.Create(Attacker, Target, Damage, MsgInteract.Action.Attack));
                    if (Damage >= Target.CurHP)
                    {
                        Target.Die();
                        World.BroadcastMapMsg(Attacker.Map, MsgInteract.Create(Attacker, Target, 1, MsgInteract.Action.Kill));
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

                    World.BroadcastMapMsg(Attacker.Map, MsgInteract.Create(Target, Attacker, Damage, MsgInteract.Action.ReflectWeapon));
                    if (Damage >= Attacker.CurHP)
                    {
                        Attacker.Die(0);
                        World.BroadcastMapMsg(Target, MsgInteract.Create(Target, Attacker, 1, MsgInteract.Action.Kill), true);
                    }
                    else
                    {
                        Attacker.CurHP -= Damage;
                        World.BroadcastMapMsg(Attacker.Map, MsgUserAttrib.Create(Attacker, Attacker.CurHP, MsgUserAttrib.Type.Life));
                    }
                }
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }
    }
}
