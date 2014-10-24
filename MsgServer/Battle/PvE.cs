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
        public static void PvE(Player Attacker, TerrainNPC Target)
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

                if (Battle.Magic.WeaponSkill(Attacker, Target))
                    return;

                Attacker.LastAttackTick = Environment.TickCount;
                if (!MyMath.Success(Attacker.Dexterity))
                {
                    World.BroadcastMapMsg(Attacker, MsgInteract.Create(Attacker, Target, 0, (MsgInteract.Action)Attacker.AtkType), true);
                    return;
                }

                Int32 Damage = 0;
                Damage = MyMath.GetDamagePlayer2Environment(Attacker, Target);

                if (Attacker.Map != 1039)
                    Attacker.RemoveAtkDura();

                World.BroadcastMapMsg(Attacker, MsgInteract.Create(Attacker, Target, Damage, MsgInteract.Action.Attack), true);
                if (Damage >= Target.CurHP)
                {
                    if (Attacker.XP < 99)
                        Attacker.XP++;

                    Target.GetAttacked(Attacker, Target.CurHP);

                    Int32 CurHP = Target.CurHP;
                    World.BroadcastMapMsg(Attacker, MsgInteract.Create(Attacker, Target, 1, MsgInteract.Action.Kill), true);
                    Target.Die();

                    if (Target.Type == 21 || Target.Type == 22)
                    {
                        Int32 Exp = AdjustExp(CurHP, Attacker, Target);
                        Attacker.AddExp(Exp, true);

                        Int32 Bonus = (Int32)(Target.MaxHP * 0.05);

                        Int32 BonusExp = AdjustExp(Bonus, Attacker, Target);
                        Attacker.AddExp(BonusExp, true);

                        Item RightHand = Attacker.GetItemByPos(4);
                        if (RightHand != null && ((RightHand.Id / 100000) == 4 || (RightHand.Id / 100000) == 5))
                            Attacker.AddWeaponSkillExp((Int16)(RightHand.Id / 1000), Exp);
                        else if (RightHand == null)
                            Attacker.AddWeaponSkillExp(0, Exp);

                        Item LeftHand = Attacker.GetItemByPos(5);
                        if (LeftHand != null && ((LeftHand.Id / 100000) == 4 || (LeftHand.Id / 100000) == 9))
                            Attacker.AddWeaponSkillExp((Int16)(LeftHand.Id / 1000), Exp);
                    }
                    else if (Target.Type == (Byte)TerrainNPC.NpcType.SynFlag)
                    {
                        if (Attacker.Syndicate != null)
                        {
                            if (World.AllMaps[Attacker.Map].InWar)
                                if (Attacker.Syndicate.UniqId != World.AllMaps[Attacker.Map].Holder)
                                {
                                    Attacker.Money += (Damage / 10000);
                                    Attacker.Send(MsgUserAttrib.Create(Attacker, Attacker.Money, MsgUserAttrib.Type.Money));

                                    Syndicate.Member Member = Attacker.Syndicate.GetMemberInfo(Attacker.UniqId);
                                    if (Member != null)
                                    {
                                        Member.Donation += (Damage / 10000);
                                        Attacker.Syndicate.Money += (Damage / 10000);
                                        Attacker.Send(MsgSynAttrInfo.Create(Attacker.UniqId, Attacker.Syndicate));
                                        World.SynThread.AddToQueue(Attacker.Syndicate, "Money", Attacker.Syndicate.Money);
                                    }
                                }
                        }
                    }
                }
                else
                {
                    Target.CurHP -= Damage;
                    Target.GetAttacked(Attacker, Damage);

                    if (Target.Type == 21 || Target.Type == 22)
                    {
                        Int32 Exp = AdjustExp(Damage, Attacker, Target);
                        Attacker.AddExp(Exp, true);

                        Item RightHand = Attacker.GetItemByPos(4);
                        if (RightHand != null && ((RightHand.Id / 100000) == 4 || (RightHand.Id / 100000) == 5))
                            Attacker.AddWeaponSkillExp((Int16)(RightHand.Id / 1000), Exp);
                        else if (RightHand == null)
                            Attacker.AddWeaponSkillExp(0, Exp);

                        Item LeftHand = Attacker.GetItemByPos(5);
                        if (LeftHand != null && ((LeftHand.Id / 100000) == 4 || (LeftHand.Id / 100000) == 9))
                            Attacker.AddWeaponSkillExp((Int16)(LeftHand.Id / 1000), Exp);
                    }
                    else if (Target.Type == (Byte)TerrainNPC.NpcType.SynFlag)
                    {
                        if (Attacker.Syndicate != null)
                        {
                            if (World.AllMaps[Attacker.Map].InWar)
                                if (Attacker.Syndicate.UniqId != World.AllMaps[Attacker.Map].Holder)
                                {
                                    Attacker.Money += (Target.CurHP / 10000);
                                    Attacker.Send(MsgUserAttrib.Create(Attacker, Attacker.Money, MsgUserAttrib.Type.Money));

                                    Syndicate.Member Member = Attacker.Syndicate.GetMemberInfo(Attacker.UniqId);
                                    if (Member != null)
                                    {
                                        Member.Donation += (Target.CurHP / 10000);
                                        Attacker.Syndicate.Money += (Target.CurHP / 10000);
                                        Attacker.Send(MsgSynAttrInfo.Create(Attacker.UniqId, Attacker.Syndicate));
                                        World.SynThread.AddToQueue(Attacker.Syndicate, "Money", Attacker.Syndicate.Money);
                                    }
                                }
                        }
                    }
                }

                if (Attacker.Map != 1039)
                    Battle.Magic.WeaponAttribute(Attacker, Target);
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }

        public static Int32 AdjustExp(Int32 Damage, Player Attacker, TerrainNPC Target)
        {
            Byte Level = 125;
            if (Attacker.Level < 125)
                Level = (Byte)Attacker.Level;

            Double Exp = Damage;
            Int32 DeltaLvl = Level - Target.Level;

            if (Target.Type == (Byte)TerrainNPC.NpcType.MagicGoal)
            {
                Exp /= 70.0;
                if (DeltaLvl < 0)
                    Exp = 0.00;
                else if (DeltaLvl >= 5)
                    Exp /= 3.00;
            }
            else if (Target.Type == (Byte)TerrainNPC.NpcType.WeaponGoal)
            {
                Exp /= 10.0;
                if (DeltaLvl < 0)
                    Exp = 0.00;
                else if (DeltaLvl >= 5)
                    Exp /= 10.0;
            }
            else
                Exp = 0.00;

            return Math.Max(0, (Int32)Exp);
        }
    }
}
