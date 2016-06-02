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

                if (Battle.WeaponSkill(Attacker, Target))
                    return;

                Attacker.LastAttackTick = Environment.TickCount;
                if (!MyMath.Success(Attacker.Dexterity))
                {
                    World.BroadcastMapMsg(Attacker, new MsgInteract(Attacker, Target, 0, (MsgInteract.Action)Attacker.AtkType), true);
                    return;
                }

                Int32 Damage = 0;
                Damage = MyMath.GetDamagePlayer2Environment(Attacker, Target);

                if (Attacker.Map.Id !=  1039)
                    Attacker.RemoveAtkDura();

                World.BroadcastMapMsg(Attacker, new MsgInteract(Attacker, Target, Damage, MsgInteract.Action.Attack), true);
                if (Damage >= Target.CurHP)
                {
                    if (Attacker.XP < 99)
                        Attacker.XP++;

                    Target.GetAttacked(Attacker, Target.CurHP);

                    Int32 CurHP = Target.CurHP;
                    World.BroadcastMapMsg(Attacker, new MsgInteract(Attacker, Target, 1, MsgInteract.Action.Kill), true);
                    Target.Die();

                    if (Target.Type == 21 || Target.Type == 22)
                    {
                        UInt32 Exp = AdjustExp(CurHP, Attacker, Target);
                        Attacker.AddExp(Exp, true);

                        Int32 Bonus = (Int32)(Target.MaxHP * 0.05);

                        UInt32 BonusExp = AdjustExp(Bonus, Attacker, Target);
                        Attacker.AddExp(BonusExp, true);

                        Item RightHand = Attacker.GetItemByPos(4);
                        if (RightHand != null && ((RightHand.Type / 100000) == 4 || (RightHand.Type / 100000) == 5))
                            Attacker.AddWeaponSkillExp((UInt16)(RightHand.Type / 1000), Exp);
                        else if (RightHand == null)
                            Attacker.AddWeaponSkillExp(0, Exp);

                        Item LeftHand = Attacker.GetItemByPos(5);
                        if (LeftHand != null && ((LeftHand.Type / 100000) == 4 || (LeftHand.Type / 100000) == 9))
                            Attacker.AddWeaponSkillExp((UInt16)(LeftHand.Type / 1000), Exp);
                    }
                    else if (Target.Type == (Byte)TerrainNPC.NpcType.SynFlag)
                    {
                        if (Attacker.Syndicate != null)
                        {
                            // TODO re-enable donation gain on pole
                            //if (Attacker.Map.InWar)
                            //    if (Attacker.Syndicate.Id != Attacker.Map.Holder)
                            //    {
                            //        Attacker.Money += (UInt32)(Damage / 10000);
                            //        Attacker.Send(new MsgUserAttrib(Attacker, Attacker.Money, MsgUserAttrib.AttributeType.Money));

                            //        Syndicate.Member Member = Attacker.Syndicate.GetMemberInfo(Attacker.UniqId);
                            //        if (Member != null)
                            //        {
                            //            Member.Donation += (UInt32)(Damage / 10000);
                            //            Attacker.Syndicate.Money += (UInt32)(Damage / 10000);
                            //            Attacker.Send(new MsgSynAttrInfo(Attacker.UniqId, Attacker.Syndicate));
                            //        }
                            //    }
                        }
                    }
                }
                else
                {
                    Target.CurHP -= Damage;
                    Target.GetAttacked(Attacker, Damage);

                    if (Target.Type == 21 || Target.Type == 22)
                    {
                        UInt32 Exp = AdjustExp(Damage, Attacker, Target);
                        Attacker.AddExp(Exp, true);

                        Item RightHand = Attacker.GetItemByPos(4);
                        if (RightHand != null && ((RightHand.Type / 100000) == 4 || (RightHand.Type / 100000) == 5))
                            Attacker.AddWeaponSkillExp((UInt16)(RightHand.Type / 1000), Exp);
                        else if (RightHand == null)
                            Attacker.AddWeaponSkillExp(0, Exp);

                        Item LeftHand = Attacker.GetItemByPos(5);
                        if (LeftHand != null && ((LeftHand.Type / 100000) == 4 || (LeftHand.Type / 100000) == 9))
                            Attacker.AddWeaponSkillExp((UInt16)(LeftHand.Type / 1000), Exp);
                    }
                    else if (Target.Type == (Byte)TerrainNPC.NpcType.SynFlag)
                    {
                        if (Attacker.Syndicate != null)
                        {
                            // TODO re-enable donation gain on pole
                            //if (Attacker.Map.InWar)
                            //    if (Attacker.Syndicate.Id != Attacker.Map.Holder)
                            //    {
                            //        Attacker.Money += (UInt32)(Target.CurHP / 10000);
                            //        Attacker.Send(new MsgUserAttrib(Attacker, Attacker.Money, MsgUserAttrib.AttributeType.Money));

                            //        Syndicate.Member Member = Attacker.Syndicate.GetMemberInfo(Attacker.UniqId);
                            //        if (Member != null)
                            //        {
                            //            Member.Donation += (UInt32)(Target.CurHP / 10000);
                            //            Attacker.Syndicate.Money += (UInt32)(Target.CurHP / 10000);
                            //            Attacker.Send(new MsgSynAttrInfo(Attacker.UniqId, Attacker.Syndicate));
                            //        }
                            //    }
                        }
                    }
                }

                if (Attacker.Map.Id !=  1039)
                    Battle.WeaponAttribute(Attacker, Target);
            }
            catch (Exception exc) { sLogger.Error(exc); }
        }

        public static UInt32 AdjustExp(Int32 Damage, Player Attacker, TerrainNPC Target)
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

            return Math.Max(0, (UInt32)Exp);
        }
    }
}
