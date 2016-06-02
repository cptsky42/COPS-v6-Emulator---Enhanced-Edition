// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * COPS v6 Emulator

using System;
using System.Collections.Generic;
using COServer.Network;
using COServer.Entities;

namespace COServer
{
    public partial class Battle
    {
        public static void PvM(Player Attacker, Monster Target)
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

                Attacker.LastAttackTick = Environment.TickCount;
                if (!MyMath.Success(Attacker.Dexterity))
                {
                    World.BroadcastMapMsg(Attacker, new MsgInteract(Attacker, Target, 0, (MsgInteract.Action)Attacker.AtkType), true);
                    return;
                }

                Int32 Damage = 0;
                Damage = MyMath.GetDamagePlayer2Monster(Attacker, Target);

                if (Attacker.Map.Id !=  1039)
                    Attacker.RemoveAtkDura();

                World.BroadcastMapMsg(Attacker, new MsgInteract(Attacker, Target, Damage, MsgInteract.Action.Attack), true);

                if ((Byte)(Target.Id / 100) == 9)
                    Attacker.AttachStatus(Status.Crime, 25000);
                
                if (Damage >= Target.CurHP)
                {
                    if (Attacker.XP < 99)
                        Attacker.XP++;

                    StatusEffect effect;
                    if (Attacker.GetStatus(Status.SuperSpeed, out effect))
                    {
                        effect.IncreaseDuration(820);
                        ++Attacker.CurKO;
                    }
                    if (Attacker.GetStatus(Status.SuperAtk, out effect))
                    {
                        effect.IncreaseDuration(820);
                        ++Attacker.CurKO;
                    }

                    Int32 CurHP = Target.CurHP;
                    Target.Die(Attacker.UniqId);

                    if (Attacker.HasStatus(Status.SuperAtk) || Attacker.HasStatus(Status.SuperSpeed))
                        World.BroadcastMapMsg(Attacker, new MsgInteract(Attacker, Target, 0xFFFF * (Attacker.CurKO + 1), MsgInteract.Action.Kill), true);
                    else
                        World.BroadcastMapMsg(Attacker, new MsgInteract(Attacker, Target, 1, MsgInteract.Action.Kill), true);

                    UInt32 Exp = AdjustExp(CurHP, Attacker, Target);
                    Attacker.AddExp(Exp, true);

                    Int32 Bonus = (Int32)(Target.MaxHP * 0.05);
                    if (Attacker.Team != null)
                        Attacker.Team.AwardMemberExp(Attacker, Target, Bonus);

                    UInt32 BonusExp = AdjustExp(Bonus, Attacker, Target);
                    Attacker.AddExp(BonusExp, true);
                    Attacker.SendSysMsg(StrRes.STR_KILL_EXPERIENCE, BonusExp);

                    Item RightHand = Attacker.GetItemByPos(4);
                    if (RightHand != null && ((RightHand.Type / 100000) == 4 || (RightHand.Type / 100000) == 5))
                        Attacker.AddWeaponSkillExp((UInt16)(RightHand.Type / 1000), Exp);
                    else if (RightHand == null)
                        Attacker.AddWeaponSkillExp(0, Exp);

                    Item LeftHand = Attacker.GetItemByPos(5);
                    if (LeftHand != null && ((LeftHand.Type / 100000) == 4 || (LeftHand.Type / 100000) == 9))
                        Attacker.AddWeaponSkillExp((UInt16)(LeftHand.Type / 1000), Exp);
                }
                else
                {
                    Target.CurHP -= Damage;

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
                if (Attacker.Map.Id !=  1039)
                    Battle.WeaponAttribute(Attacker, Target);
            }
            catch (Exception exc) { sLogger.Error(exc); }
        }

        public static UInt32 AdjustExp(Int32 Damage, Player Attacker, Monster Target)
        {
            Byte Level = 120;
            if (Attacker.Level < 120)
                Level = (Byte)Attacker.Level;

            Double Exp = Damage;
            Int32 DeltaLvl = Level - Target.Level;

            if (Target.IsGreen(Attacker))
            {
                if (DeltaLvl >= 3 && DeltaLvl <= 5)
                    Exp *= 0.70;
                else if (DeltaLvl > 5 && DeltaLvl <= 10)
                    Exp *= 0.20;
                else if (DeltaLvl > 10 && DeltaLvl <= 20)
                    Exp *= 0.10;
                else if (DeltaLvl > 20)
                    Exp *= 0.05;
            }
            else if (Target.IsRed(Attacker))
                Exp *= 1.30;
            else if (Target.IsBlack(Attacker))
            {
                if (DeltaLvl >= -10 && DeltaLvl < -5)
                    Exp *= 1.5;
                else if (DeltaLvl >= -20 && DeltaLvl < -10)
                    Exp *= 1.8;
                else if (DeltaLvl < -20)
                    Exp *= 2.3;
            }
            return Math.Max(0, (UInt32)Exp);
        }
    }
}
