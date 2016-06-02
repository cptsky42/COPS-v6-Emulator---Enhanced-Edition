// * Created by Jean-Philippe Boivin
// * Copyright © 2011-2015
// * COPS v6 Emulator

using System;
using System.Threading;
using System.Collections.Generic;
using COServer.Network;
using COServer.Entities;

namespace COServer
{
    public partial class Battle
    {
        public const Int32 MAX_TARGET_COUNT = 28;

        public static void UseMagic(AdvancedEntity aAttacker, AdvancedEntity aTarget, UInt16 aTargetX, UInt16 aTargetY)
        {
            try
            {
                // a lot of logic depends on the fact that the attacker is a player...
                Player attacker = aAttacker as Player;

                if (!aAttacker.IsAlive())
                    return;

                if (aAttacker.IsPlayer())
                {
                    if (attacker.MagicType == 8000 || attacker.MagicType == 8001)
                    {
                        Item arrow = attacker.GetItemByPos(5);
                        if (arrow.Type / 10000 != 105)
                            return;

                        if (attacker.MagicType == 8000)
                            if (arrow.CurDura < 5)
                                return;

                        if (attacker.MagicType == 8001)
                            if (arrow.CurDura < 3)
                                return;
                    }
                }

                Magic.Info Info = new Magic.Info();
                if (!Database.AllMagics.TryGetValue((aAttacker.MagicType * 10) + aAttacker.MagicLevel, out Info))
                    return;

                GameMap Map = aAttacker.Map;

                if (aAttacker.IsPlayer() && Program.Debug)
                {
                    attacker.SendSysMsg(String.Format("Using magic skill {0} [sort={1}, power={2}, status={3}]",
                        Info.Type, Info.Sort, Info.Power, Info.Status));
                }

                #region Fly | IsWing_Disable
                if (Info.Type == 8002 || Info.Type == 8003)
                    if (Map.IsWing_Disable())
                        return;
                #endregion

                #region Use Mp/PP/Xp
                if (aAttacker.IsPlayer())
                {
                    if (Info.UseMP != 0 && attacker.CurMP < Info.UseMP)
                        return;

                    if (Info.UseEP != 0 && attacker.Energy < Info.UseEP)
                        return;

                    if (Info.UseXP == Magic.TYPE_XPSKILL && attacker.XP < 100)
                        return;

                    if (Info.UseMP != 0 && attacker.Map.Id != 1039)
                    {
                        attacker.CurMP -= (UInt16)Info.UseMP;
                        attacker.Send(new MsgUserAttrib(attacker, attacker.CurMP, MsgUserAttrib.AttributeType.Mana));
                    }

                    if (Info.UseEP != 0 && attacker.Map.Id != 1039)
                    {
                        attacker.Energy -= (Byte)Info.UseEP;
                        attacker.Send(new MsgUserAttrib(attacker, attacker.Energy, MsgUserAttrib.AttributeType.Energy));
                    }

                    if (Info.UseXP == Magic.TYPE_XPSKILL)
                    {
                        attacker.XP = 0;
                        attacker.Send(new MsgUserAttrib(attacker, attacker.XP, MsgUserAttrib.AttributeType.XP));
                        attacker.DetachStatus(Status.XpFull);
                    }
                }
                #endregion

                #region Intone
                if (Info.IntoneDuration > 0)
                {
                    aAttacker.MagicIntone = true;
                    // TODO implement intonation
                    //Thread.Sleep((Int32)Info.IntoneDuration);

                    if (!aAttacker.MagicIntone)
                        return;

                    aAttacker.MagicIntone = false;
                }
                #endregion

                #region Potential targets
                AdvancedEntity[] potential_targets = new AdvancedEntity[0];
                if (aTarget == null || Info.Sort == MagicSort.RecoverSingleHP || Info.Sort == MagicSort.AttackRoundHP || Info.Sort == MagicSort.DispatchXP)
                {
                    switch (Info.Sort)
                    {
                        case MagicSort.RecoverSingleHP:
                            {
                                // team healing...
                                if (Info.Type == 1055 || Info.Type == 1170)
                                {
                                    if ((aTarget.IsPlayer() && (aTarget as Player).Team == null))
                                    {
                                        potential_targets = new AdvancedEntity[] { aTarget };
                                        break;
                                    }

                                    if (aAttacker.IsPlayer()) // only a player can have a team...
                                        potential_targets = Battle.GetTargetsForType11(attacker);
                                }
                                else
                                    potential_targets = new AdvancedEntity[] { aTarget };

                                break;
                            }
                        case MagicSort.AttackSectorHP:
                            potential_targets = Battle.GetTargetsForType04(aAttacker, aTargetX, aTargetY, Info.Distance, Info.Range);
                            break;
                        case MagicSort.AttackRoundHP:
                            {
                                if (aTarget != null)
                                {
                                    aTargetX = aTarget.X;
                                    aTargetY = aTarget.Y;
                                    aTarget = null;
                                }
                                potential_targets = Battle.GetTargetsForType05(aAttacker, aTargetX, aTargetY, Info.Range);
                                break;
                            }
                        case MagicSort.DispatchXP:
                            {
                                if (aAttacker.IsPlayer()) // only a player can have a team...
                                    potential_targets = Battle.GetTargetsForType11(attacker);
                                break;
                            }
                        case MagicSort.Line:
                            {
                                potential_targets = Battle.GetTargetsForType14(aAttacker, aTargetX, aTargetY, Info.Range);
                                break;
                            }
                    }
                }
                else
                    potential_targets = new AdvancedEntity[] { aTarget };
                #endregion

                Dictionary<AdvancedEntity, Int32> targets = new Dictionary<AdvancedEntity, Int32>(potential_targets.Length);
                UInt32 Exp = 0;
                UInt32 MagicExp = 0;

                foreach (AdvancedEntity entity in potential_targets)
                {
                    Boolean Reflected = false;
                    Player Player = entity as Player;
                    Monster Monster = entity as Monster;
                    TerrainNPC Npc = entity as TerrainNPC;

                    // entity == NULL || entity isn't alive and not using pray
                    if (entity == null || (!entity.IsAlive() && Info.Sort != MagicSort.RecoverSingleStatus))
                        continue;

                    if (aAttacker.Map != entity.Map)
                        continue;

                    if (!MyMath.CanSee(aAttacker.X, aAttacker.Y, entity.X, entity.Y, (Int32)Info.Distance))
                        continue;

                    if (Info.Target == 0 && aAttacker.UniqId == entity.UniqId)
                        continue;
                    else if (Info.Target == 2 && aAttacker.UniqId != entity.UniqId)
                        continue;
                    else if (Info.Target == 4 && aAttacker.UniqId == entity.UniqId)
                        continue;
                    else if (Info.Target == 8 && aAttacker.UniqId == entity.UniqId
                        && !((Info.Type - (Info.Type % 1000)) == 10000))
                        continue;

                    if (Info.Ground && entity.IsPlayer()) // Ground skill
                    {
                        if (Player.IsFlying())
                            continue;
                    }

                    #region Blue name (Crime)
                    if (aAttacker.IsPlayer() && Info.Crime) // Is a crime to use the skill
                    {
                        if (entity.IsPlayer())
                        {
                            if (!CanAttack(attacker, Player))
                                continue;

                            if (!Map.IsPkField() && !Map.IsSynMap() && !Map.IsPrisonMap())
                            {
                                if (!Player.IsCriminal())
                                    attacker.AttachStatus(Status.Crime, 25000);
                            }
                        }
                        else if (entity.IsMonster())
                        {
                            if (!CanAttack(attacker, Monster))
                                continue;

                            if ((Byte)(Monster.Id / 100) == 9) //Is a guard
                                attacker.AttachStatus(Status.Crime, 25000);
                        }
                    }
                    #endregion

                    #region Get base damage
                    Int32 damage = 0;
                    if (Info.Sort == MagicSort.RecoverSingleHP || Info.Sort == MagicSort.AttackSingleStatus || Info.Sort == MagicSort.DispatchXP || Info.Sort == MagicSort.Transform || Info.Sort == MagicSort.AddMana)
                        damage = (Int32)Info.Power;
                    else if (Info.Sort == MagicSort.RecoverSingleStatus)
                        damage = entity.MaxHP;
                    else if (Info.Sort == MagicSort.DecLife)
                        damage = (Int32)(entity.CurHP * ((Double)(Info.Power - 30000) / 100.00));
                    else
                    {
                        if (aAttacker.IsPlayer())
                        {
                            if (entity.IsPlayer())
                                damage = MyMath.GetDamagePlayer2Player(attacker, Player, attacker.MagicType, attacker.MagicLevel);
                            else if (entity.IsMonster())
                                damage = MyMath.GetDamagePlayer2Monster(attacker, Monster, attacker.MagicType, attacker.MagicLevel);
                            else if (entity.IsTerrainNPC())
                                damage = MyMath.GetDamagePlayer2Environment(attacker, Npc, attacker.MagicType, attacker.MagicLevel);
                        }
                        else
                        {
                            if (entity.IsPlayer())
                                damage = MyMath.GetDamageMonster2Player((Monster)aAttacker, Player, aAttacker.MagicType, aAttacker.MagicLevel);
                        }
                    }

                    //TargetType -> 8; Passive skill
                    if (Info.Target != 8 && Info.Success != 100 && !MyMath.Success(Info.Success))
                        damage = 0;
                    #endregion

                    #region Process damage / skill
                    switch (Info.Sort)
                    {
                        case MagicSort.RecoverSingleHP:
                            {
                                if (entity.CurHP + damage >= entity.MaxHP)
                                {
                                    MagicExp += (UInt32)(entity.MaxHP - entity.CurHP);
                                    entity.CurHP = entity.MaxHP;
                                }
                                else
                                {
                                    MagicExp += (UInt32)damage;
                                    entity.CurHP += damage;
                                }

                                if (entity.IsPlayer())
                                {
                                    var msg = new MsgUserAttrib(Player, Player.CurHP, MsgUserAttrib.AttributeType.Life);
                                    Player.Send(msg);
                                    if (Player.Team != null)
                                        Player.Team.BroadcastMsg(msg);
                                    msg = null;
                                }
                                break;
                            }
                        case MagicSort.AttackSingleStatus:
                            {
                                int duration = (int)Info.StepSecs * 1000;
                                double power = ((double)(damage - 30000) / 100.00);

                                switch (aAttacker.MagicType)
                                {
                                    case 1015:
                                        {
                                            entity.AttachStatus(Status.Accuracy, duration, power);
                                            
                                            if (entity.IsPlayer())
                                                MyMath.GetEquipStats(Player);

                                            break;
                                        }
                                    case 1020:
                                        {
                                            entity.AttachStatus(Status.MagicDefense, duration, power);

                                            if (entity.IsPlayer())
                                                MyMath.GetEquipStats(Player);

                                            break;
                                        }
                                    case 1025:
                                        {
                                            if (entity.HasStatus(Status.SuperSpeed))
                                                entity.DetachStatus(Status.SuperSpeed);

                                            entity.AttachStatus(Status.SuperAtk, duration, power);

                                            if (entity.IsPlayer())
                                                MyMath.GetEquipStats(Player);

                                            break;
                                        }
                                    case 1075:
                                        {
                                            ++MagicExp;
                                            entity.AttachStatus(Status.Hidden, duration);
                                            break;
                                        }
                                    case 1085:
                                        {
                                            ++MagicExp;
                                            entity.AttachStatus(Status.Accuracy, duration, power);
                                            
                                            if (entity.IsPlayer())
                                                MyMath.GetEquipStats(Player);

                                            break;
                                        }
                                    case 1090:
                                        {
                                            ++MagicExp;
                                            entity.AttachStatus(Status.MagicDefense, duration, power);

                                            if (entity.IsPlayer())
                                                MyMath.GetEquipStats(Player);

                                            break;
                                        }
                                    case 1095:
                                        {
                                            ++MagicExp;
                                            entity.AttachStatus(Status.MagicAttack, duration, power);

                                            if (entity.IsPlayer())
                                                MyMath.GetEquipStats(Player);
                                            
                                            break;
                                        }
                                    case 1110:
                                        {
                                            if (entity.HasStatus(Status.SuperAtk))
                                                entity.DetachStatus(Status.SuperAtk);

                                            entity.AttachStatus(Status.SuperSpeed, duration, power);

                                            if (entity.IsPlayer())
                                                MyMath.GetEquipStats(Player);
                                            
                                            break;
                                        }
                                    case 8002:
                                        {
                                            if (entity.HasStatus(Status.MagicDefense))
                                                break;

                                            entity.AttachStatus(Status.Flying, duration);
                                            break;
                                        }
                                    case 8003:
                                        {
                                            if (entity.HasStatus(Status.MagicDefense))
                                                break;

                                            entity.AttachStatus(Status.Flying, duration);
                                            break;
                                        }
                                    case 9000:
                                        {
                                            ++MagicExp;
                                            entity.AttachStatus((Status)Info.Status, -1, power);
                                            break;
                                        }
                                    case 9876:
                                        {
                                            entity.AttachStatus(Status.CastingPray);

                                            if (entity.IsPlayer())
                                            {
                                                Player.CastingPray = true;
                                                Player.PrayMap = entity.Map.Id;
                                                Player.PrayX = entity.X;
                                                Player.PrayY = entity.Y;
                                            }

                                            break;
                                        }
                                    case 10203:
                                        {
                                            StatusEffect effect;
                                            if (entity.GetStatus(Status.MagicDefense, out effect))
                                            {
                                                effect.Data = power;
                                                effect.ResetTimeout(duration);
                                                break;
                                            }

                                            entity.AttachStatus(Status.MagicDefense, duration, power);

                                            if (entity.IsPlayer())
                                                MyMath.GetEquipStats(Player);

                                            break;
                                        }
                                    default:
                                        sLogger.Warn("Magic {0} is not implemented !", aAttacker.MagicType);
                                        break;
                                }
                                damage = 0;
                                break;
                            }
                        case MagicSort.RecoverSingleStatus:
                            {
                                if (entity.IsPlayer() && !entity.IsAlive())
                                    Player.Reborn(false);
                                break;
                            }
                        case MagicSort.DispatchXP:
                            {
                                if (entity.IsPlayer())
                                {
                                    if (Player.XP + damage >= 100)
                                        Player.XP = 100;
                                    else
                                        Player.XP += (Byte)damage;
                                    Player.Send(new MsgUserAttrib(Player, Player.XP, MsgUserAttrib.AttributeType.XP));
                                }
                                break;
                            }
                        case MagicSort.Transform:
                            {
                                if (entity.IsPlayer())
                                {
                                    MonsterInfo MonsterInfo;
                                    if (Database.AllMonsters.TryGetValue(damage, out MonsterInfo))
                                    {
                                        MagicExp++;
                                        Player.TransformEndTime = Environment.TickCount + ((Int32)Info.StepSecs * 1000);
                                        
                                        // TODO re-enable transformation skills
                                        //Player.AddTransform(MonsterInfo.Look);
                                        Player.MinAtk = (Int32)MonsterInfo.MinAtk;
                                        Player.MaxAtk = (Int32)MonsterInfo.MaxAtk;
                                        Player.Defence = (Int32)MonsterInfo.Defense;
                                        Player.MagicAtk = 0;
                                        Player.MagicDef = (Int32)MonsterInfo.MagicDef;
                                        Player.MagicBlock = 0;
                                        Player.Dodge = MonsterInfo.Dodge;
                                        Player.Dexterity = 1000;
                                        Player.AtkRange = MonsterInfo.AtkRange;
                                        Player.AtkSpeed = MonsterInfo.AtkSpeed;
                                        Player.AtkType = 2;
                                        Player.Bless = 1.00;
                                        Player.GemBonus = 1.00;
                                        if (Player.CurHP >= Player.MaxHP)
                                            Player.CurHP = Player.MaxHP;
                                        Player.CurHP = (Int32)((Double)MonsterInfo.Life * ((Double)Player.CurHP / (Double)Player.MaxHP));
                                        Player.MaxHP = MonsterInfo.Life;

                                        Player.Send(new MsgUserAttrib(Player, Player.CurHP, MsgUserAttrib.AttributeType.Life));
                                        Player.Send(new MsgUserAttrib(Player, Player.MaxHP, (MsgUserAttrib.AttributeType.Life + 1)));
                                        if (Player.Team != null)
                                        {
                                            Player.Team.BroadcastMsg(new MsgUserAttrib(Player, Player.CurHP, MsgUserAttrib.AttributeType.Life));
                                            Player.Team.BroadcastMsg(new MsgUserAttrib(Player, Player.MaxHP, (MsgUserAttrib.AttributeType.Life + 1)));
                                        }
                                        World.BroadcastRoomMsg(Player, new MsgUserAttrib(Player, Player.Look, MsgUserAttrib.AttributeType.Look), true);
                                    }
                                }
                                damage = 0;
                                break;
                            }
                        case MagicSort.AddMana:
                            {
                                if (entity.IsPlayer())
                                {
                                    if (Player.CurMP + damage >= Player.MaxMP)
                                    {
                                        MagicExp += (UInt32)(Player.MaxMP - Player.CurMP);
                                        Player.CurMP = Player.MaxMP;
                                    }
                                    else
                                    {
                                        MagicExp += (UInt32)damage;
                                        Player.CurMP += (UInt16)damage;
                                    }
                                    Player.Send(new MsgUserAttrib(Player, Player.CurMP, MsgUserAttrib.AttributeType.Mana));
                                }
                                break;
                            }
                        case MagicSort.CallPet:
                            {
                                if (!aAttacker.IsPlayer())
                                {
                                    sLogger.Error("MagicSort::CallPet used by a non-player entity.");
                                    return;
                                }

                                damage = 0;
                                MagicExp += 1;

                                Generator.generatePet((Player)aAttacker, (Int32)Info.Power);
                                break;
                            }
                        default:
                            {
                                if (entity.IsPlayer())
                                    Player.RemoveDefDura();

                                #region Not a player || Not reflected
                                if (!entity.IsPlayer() || (entity.IsPlayer() && !Player.Reflect()))
                                {
                                    if (damage >= entity.CurHP)
                                    {
                                        if (entity.IsPlayer())
                                        {
                                            Player.Die(aAttacker);
                                        }
                                        else if (entity.IsMonster())
                                        {
                                            if (aAttacker.IsPlayer())
                                            {
                                                if (attacker.XP < 99)
                                                    ++attacker.XP;

                                                StatusEffect effect;
                                                if (attacker.GetStatus(Status.SuperSpeed, out effect))
                                                {
                                                    effect.IncreaseDuration(820);
                                                    ++attacker.CurKO;
                                                }
                                                if (attacker.GetStatus(Status.SuperAtk, out effect))
                                                {
                                                    effect.IncreaseDuration(820);
                                                    ++attacker.CurKO;
                                                }

                                                Int32 CurHP = entity.CurHP;
                                                Monster.Die(attacker.UniqId);

                                                Exp += AdjustExp(CurHP, attacker, Monster);
                                                MagicExp += AdjustExp(CurHP, attacker, Monster);

                                                Int32 Bonus = (Int32)(Monster.MaxHP * 0.05);
                                                if (attacker.Team != null)
                                                    attacker.Team.AwardMemberExp(attacker, Monster, Bonus);

                                                UInt32 BonusExp = AdjustExp(Bonus, attacker, Monster);
                                                attacker.AddExp(BonusExp, true);
                                                attacker.SendSysMsg(String.Format(StrRes.STR_KILL_EXPERIENCE, BonusExp));
                                            }
                                        }
                                        else if (entity.IsTerrainNPC())
                                        {
                                            if (attacker.IsPlayer())
                                            {
                                                if (attacker.XP < 99)
                                                    attacker.XP++;

                                                Int32 CurHP = entity.CurHP;

                                                if (Npc.Type == 21 || Npc.Type == 22)
                                                {
                                                    Exp += AdjustExp(CurHP, attacker, Npc);
                                                    MagicExp += AdjustExp(CurHP, attacker, Npc);

                                                    Int32 Bonus = (Int32)(entity.MaxHP * 0.05);

                                                    UInt32 BonusExp = AdjustExp(Bonus, attacker, Npc);
                                                    attacker.AddExp(BonusExp, true);
                                                }
                                                else if (Npc.Type == (Byte)TerrainNPC.NpcType.SynFlag)
                                                {
                                                    if (attacker.Syndicate != null)
                                                    {
                                                        // TODO re-enable donation gain on pole
                                                        //if (attacker.Map.InWar)
                                                        //    if (attacker.Syndicate.Id != attacker.Map.Holder)
                                                        //    {
                                                        //        attacker.Money += (UInt32)(CurHP / 10000);
                                                        //        attacker.Send(new MsgUserAttrib(attacker, attacker.Money, MsgUserAttrib.AttributeType.Money));

                                                        //        Syndicate.Member Member = attacker.Syndicate.GetMemberInfo(attacker.UniqId);
                                                        //        if (Member != null)
                                                        //        {
                                                        //            Member.Donation += (UInt32)(CurHP / 10000);
                                                        //            attacker.Syndicate.Money += (UInt32)(CurHP / 10000);
                                                        //        }
                                                        //    }
                                                    }
                                                }
                                                Npc.GetAttacked(attacker, Npc.CurHP);

                                                World.BroadcastMapMsg(attacker, new MsgInteract(attacker, entity, 1, MsgInteract.Action.Kill), true);
                                                Npc.Die();
                                            }
                                        }

                                        if (!entity.IsTerrainNPC())
                                        {
                                            if (aAttacker.IsPlayer())
                                            {
                                                if (!entity.IsMonster() || (!aAttacker.HasStatus(Status.SuperAtk) && !aAttacker.HasStatus(Status.SuperSpeed)))
                                                    World.BroadcastMapMsg(attacker, new MsgInteract(aAttacker, entity, 1, MsgInteract.Action.Kill), true);
                                                else
                                                    World.BroadcastMapMsg(attacker, new MsgInteract(aAttacker, entity, 0xFFFF * (attacker.CurKO + 1), MsgInteract.Action.Kill), true);
                                            }
                                            else
                                                World.BroadcastMapMsg(aAttacker, new MsgInteract(aAttacker, entity, 1, MsgInteract.Action.Kill));
                                        }
                                    }
                                    else
                                    {
                                        entity.CurHP -= damage;
                                        if (entity.IsPlayer())
                                        {
                                            Player.Send(new MsgUserAttrib(entity, entity.CurHP, MsgUserAttrib.AttributeType.Life));
                                            if (Player.Team != null)
                                                Player.Team.BroadcastMsg(new MsgUserAttrib(entity, entity.CurHP, MsgUserAttrib.AttributeType.Life));

                                            if (Player.Action == Emotion.SitDown)
                                            {
                                                Player.Energy /= 2;
                                                Player.Send(new MsgUserAttrib(Player, Player.Energy, MsgUserAttrib.AttributeType.Energy));

                                                Player.Action = Emotion.StandBy;
                                                World.BroadcastRoomMsg(Player, new MsgAction(Player, (int)Player.Action, MsgAction.Action.Emotion), true);
                                            }
                                        }
                                        else if (entity.IsMonster())
                                        {
                                            if (aAttacker.IsPlayer())
                                            {
                                                Exp += AdjustExp(damage, attacker, Monster);
                                                MagicExp += (UInt32)AdjustExp(damage, attacker, Monster);
                                            }
                                        }
                                        else if (entity.IsTerrainNPC())
                                        {
                                            if (aAttacker.IsPlayer())
                                            {
                                                if (Npc.Type == 21 || Npc.Type == 22)
                                                {
                                                    Exp += AdjustExp(damage, attacker, Npc);
                                                    MagicExp += (UInt32)AdjustExp(damage, attacker, Npc);
                                                }
                                                else if (Npc.Type == (Byte)TerrainNPC.NpcType.SynFlag)
                                                {
                                                    if (attacker.Syndicate != null)
                                                    {
                                                        // TODO re-enable donation gain on pole
                                                        //if (attacker.Map.InWar)
                                                        //    if (attacker.Syndicate.Id != attacker.Map.Holder)
                                                        //    {
                                                        //        attacker.Money += (UInt32)(damage / 10000);
                                                        //        attacker.Send(new MsgUserAttrib(attacker, attacker.Money, MsgUserAttrib.AttributeType.Money));

                                                        //        Syndicate.Member Member = attacker.Syndicate.GetMemberInfo(attacker.UniqId);
                                                        //        if (Member != null)
                                                        //        {
                                                        //            Member.Donation += (UInt32)(damage / 10000);
                                                        //            attacker.Syndicate.Money += (UInt32)(damage / 10000);
                                                        //            attacker.Send(new MsgSynAttrInfo(attacker.UniqId, attacker.Syndicate));
                                                        //        }
                                                        //    }
                                                    }
                                                }
                                                Npc.GetAttacked(attacker, damage);
                                            }
                                        }
                                    }
                                }
                                #endregion
                                #region Reflected
                                else
                                {
                                    if (damage > 2000)
                                        damage = 2000;

                                    var msg = new MsgInteract(entity, aAttacker, damage, MsgInteract.Action.ReflectMagic);
                                    if (aAttacker.IsPlayer())
                                        World.BroadcastMapMsg(attacker, msg, true);
                                    else
                                        World.BroadcastMapMsg(aAttacker, msg);

                                    if (damage >= aAttacker.CurHP)
                                    {
                                        if (aAttacker.IsPlayer())
                                            attacker.Die(null);
                                        else
                                            ((Monster)aAttacker).Die(entity.UniqId);
                                        World.BroadcastMapMsg(Player, new MsgInteract(entity, aAttacker, 1, MsgInteract.Action.Kill), true);
                                    }
                                    else
                                    {
                                        aAttacker.CurHP -= damage;

                                        var msg2 = new MsgUserAttrib(aAttacker, aAttacker.CurHP, MsgUserAttrib.AttributeType.Life);
                                        if (aAttacker.IsPlayer())
                                        {
                                            attacker.Send(msg2);
                                            if (attacker.Team != null)
                                                attacker.Team.BroadcastMsg(msg2);
                                        }
                                        else
                                            World.BroadcastMapMsg(aAttacker, msg2);
                                    }
                                    Reflected = true;
                                }
                                #endregion
                                break;
                            }
                    }
                    #endregion

                    if (aTarget != null && Info.Sort != MagicSort.DispatchXP && Info.Sort != MagicSort.RecoverSingleHP && !Reflected)
                    {
                        var msg = new MsgMagicEffect(aAttacker, aTarget, damage, aTargetX, aTargetY);

                        if (aAttacker.IsPlayer())
                            World.BroadcastMapMsg(attacker, msg, true);
                        else
                            World.BroadcastMapMsg(aAttacker, msg);
                    }
                    else if (!Reflected)
                        targets.Add(entity, damage);
                }

                #region Player: Update LastAttackTick
                if (aAttacker.IsPlayer())
                    attacker.LastAttackTick = Environment.TickCount;
                #endregion

                #region Player: Remove Dura
                if (aAttacker.IsPlayer() && aAttacker.Map.Id != 1039)
                {
                    if (attacker.MagicType == 8000)
                        for (SByte i = 0; i < 5; i++)
                            attacker.RemoveAtkDura();
                    else if (attacker.MagicType == 8001)
                        for (SByte i = 0; i < 3; i++)
                            attacker.RemoveAtkDura();
                    else
                        attacker.RemoveAtkDura();
                }
                #endregion

                #region Player: Add Exp
                if (aAttacker.IsPlayer())
                {
                    attacker.AddExp(Exp, true);
                    if (MagicExp > 0 && Info.Sort != MagicSort.AtkStatus)
                        attacker.AddMagicExp(attacker.MagicType, MagicExp);
                    else if (MagicExp > 0)
                        attacker.AddMagicExp(attacker.MagicType, 1);
                }
                #endregion

                if (aTarget == null || Info.Sort == MagicSort.DispatchXP || Info.Sort == MagicSort.RecoverSingleHP)
                {
                    var msg = new MsgMagicEffect(aAttacker, targets, aTargetX, aTargetY);

                    if (aAttacker.IsPlayer())
                        World.BroadcastMapMsg(attacker, msg, true);
                    else
                        World.BroadcastMapMsg(aAttacker, msg);
                }

                #region Player: Activate the weapon attribute (e.g. Poison Blade)
                if (aAttacker.IsPlayer() && aAttacker.Map.Id != 1039)
                    Battle.WeaponAttribute(attacker, null);
                #endregion
            }
            catch (Exception exc) { sLogger.Error(exc); }
        }
    }
}