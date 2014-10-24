// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.Threading;
using System.Collections.Generic;
using COServer.Network;
using COServer.Entities;
using CO2_CORE_DLL.IO;

namespace COServer
{
    public partial class Battle
    {
        public partial class Magic
        {
            #region Use(Player, AdvancedEntity, UInt16, UInt16)
            public static void Use(Player Attacker, AdvancedEntity Target, UInt16 X, UInt16 Y)
            {
                try
                {
                    if (!Attacker.IsAlive())
                        return;

                    if (Attacker.MagicType == 8000 || Attacker.MagicType == 8001)
                    {
                        Item Arrow = Attacker.GetItemByPos(5);
                        if (Arrow.Id / 10000 != 105)
                            return;

                        if (Attacker.MagicType == 8000)
                            if (Arrow.CurDura < 5)
                                return;

                        if (Attacker.MagicType == 8001)
                            if (Arrow.CurDura < 3)
                                return;
                    }

                    MagicType.Entry Info = new MagicType.Entry();
                    if (!Database2.AllMagics.TryGetValue((Attacker.MagicType * 10) + Attacker.MagicLevel, out Info))
                        return;

                    Map Map = null;
                    if (!World.AllMaps.TryGetValue(Attacker.Map, out Map))
                        return;

                    #region Fly | IsWing_Disable
                    if (Info.MagicType == 8002 || Info.MagicType == 8003)
                        if (Map.IsWing_Disable())
                            return;
                    #endregion

                    #region Use Mp/PP/Xp
                    if (Info.MpCost != 0 && Attacker.CurMP < Info.MpCost)
                        return;

                    if (Info.UsePP != 0 && Attacker.Energy < Info.UsePP)
                        return;

                    if (Info.Xp == 1 && Attacker.XP < 100)
                        return;

                    if (Info.MpCost != 0 && Attacker.Map != 1039)
                    {
                        Attacker.CurMP -= (UInt16)Info.MpCost;
                        Attacker.Send(MsgUserAttrib.Create(Attacker, Attacker.CurMP, MsgUserAttrib.Type.Mana));
                    }

                    if (Info.UsePP != 0 && Attacker.Map != 1039)
                    {
                        Attacker.Energy -= (Byte)Info.UsePP;
                        Attacker.Send(MsgUserAttrib.Create(Attacker, Attacker.Energy, MsgUserAttrib.Type.Energy));
                    }

                    if (Info.Xp == 1)
                    {
                        Attacker.XP = 0;
                        Attacker.DelFlag(Player.Flag.XPList);
                        Attacker.Send(MsgUserAttrib.Create(Attacker, Attacker.XP, MsgUserAttrib.Type.XP));
                        Attacker.Send(MsgUserAttrib.Create(Attacker, Attacker.Flags, MsgUserAttrib.Type.Flags));
                    }
                    #endregion

                    #region Intone
                    if (Info.IntoneDuration > 0)
                    {
                        Attacker.MagicIntone = true;
                        //Thread.Sleep((Int32)Info.IntoneDuration);

                        if (!Attacker.MagicIntone)
                            return;

                        Attacker.MagicIntone = false;
                    }
                    #endregion

                    #region Potential targets
                    AdvancedEntity[] Entities;
                    if (Target == null || Info.ActionSort == 2 || Info.ActionSort == 5 || Info.ActionSort == 11)
                    {
                        switch (Info.ActionSort)
                        {
                            case 2:
                                {
                                    //Team healing...
                                    if (Info.MagicType != 1055 && Info.MagicType != 1170 || (Target.IsPlayer() && (Target as Player).Team == null))
                                    {
                                        Entities = new AdvancedEntity[] { Target };
                                        break;
                                    }
                                    Entities = Battle.Magic.GetTargetsForType11(Attacker);
                                    break;
                                }
                            case 4:
                                Entities = Battle.Magic.GetTargetsForType04(Attacker, X, Y, Info.Distance, Info.Range);
                                break;
                            case 5:
                                {
                                    if (Target != null)
                                    {
                                        X = Target.X;
                                        Y = Target.Y;
                                        Target = null;
                                    }
                                    Entities = Battle.Magic.GetTargetsForType05(Attacker, X, Y, Info.Range);
                                    break;
                                }
                            case 11:
                                Entities = Battle.Magic.GetTargetsForType11(Attacker);
                                break;
                            case 14:
                                Entities = Battle.Magic.GetTargetsForType14(Attacker, X, Y, Info.Range);
                                break;
                            default:
                                Entities = new AdvancedEntity[0];
                                break;
                        }
                    }
                    else
                        Entities = new AdvancedEntity[] { Target };
                    #endregion

                    Dictionary<AdvancedEntity, Int32> Targets = new Dictionary<AdvancedEntity, Int32>(Entities.Length);
                    Int32 Exp = 0;
                    Int32 MagicExp = 0;

                    foreach (AdvancedEntity Entity in Entities)
                    {
                        Boolean Reflected = false;
                        Player Player = Entity as Player;
                        Monster Monster = Entity as Monster;
                        TerrainNPC Npc = Entity as TerrainNPC;

                        //Entity == NULL || Entity isn't alive and not using pray
                        if (Entity == null || (!Entity.IsAlive() && Info.ActionSort != 7))
                            continue;

                        if (Attacker.Map != Entity.Map)
                            continue;

                        if (!MyMath.CanSee(Attacker.X, Attacker.Y, Entity.X, Entity.Y, (Int32)Info.Distance))
                            continue;

                        if (Info.Target == 0 && Attacker.UniqId == Entity.UniqId)
                            continue;
                        else if (Info.Target == 2 && Attacker.UniqId != Entity.UniqId)
                            continue;
                        else if (Info.Target == 4 && Attacker.UniqId == Entity.UniqId)
                            continue;
                        else if (Info.Target == 8 && Attacker.UniqId == Entity.UniqId
                            && !((Info.MagicType - (Info.MagicType % 1000)) == 10000))
                            continue;

                        if (Info.Ground == 1 && Entity.IsPlayer()) //Ground skill
                        {
                            if (Player.ContainsFlag(Player.Flag.Flying))
                                continue;
                        }

                        #region Blue name (Crime)
                        if (Info.Crime == 1) //Is a crime to use the skill
                        {
                            if (Entity.IsPlayer())
                            {
                                if (!CanAttack(Attacker, Player))
                                    continue;

                                if (!Map.IsPkField() && !Map.IsSynMap() && !Map.IsPrisonMap())
                                {
                                    if (!Player.ContainsFlag(Player.Flag.BlackName) && !Player.ContainsFlag(Player.Flag.Flashing))
                                    {
                                        if (!Attacker.ContainsFlag(Player.Flag.Flashing))
                                        {
                                            Attacker.AddFlag(Player.Flag.Flashing);
                                            World.BroadcastRoomMsg(Attacker, MsgUserAttrib.Create(Attacker, Attacker.Flags, MsgUserAttrib.Type.Flags), true);
                                        }
                                        Attacker.BlueNameEndTime = Environment.TickCount + 25000;
                                    }
                                }
                            }
                            else if (Entity.IsMonster())
                            {
                                if (!CanAttack(Attacker, Monster))
                                    continue;

                                if ((Byte)(Monster.Id / 100) == 9) //Is a guard
                                {
                                    if (!Attacker.ContainsFlag(Player.Flag.Flashing))
                                    {
                                        Attacker.AddFlag(Player.Flag.Flashing);
                                        World.BroadcastRoomMsg(Attacker, MsgUserAttrib.Create(Attacker, Attacker.Flags, MsgUserAttrib.Type.Flags), true);
                                    }
                                    Attacker.BlueNameEndTime = Environment.TickCount + 25000;
                                }
                            }
                        }
                        #endregion

                        #region Get base damage
                        Int32 Damage = 0;
                        if (Info.ActionSort == 2 || Info.ActionSort == 6 || Info.ActionSort == 11 || Info.ActionSort == 19 || Info.ActionSort == 20)
                            Damage = (Int32)Info.Power;
                        else if (Info.ActionSort == 7)
                            Damage = Entity.MaxHP;
                        else if (Info.ActionSort == 26)
                            Damage = (Int32)(Entity.CurHP * ((Double)(Info.Power - 30000) / 100.00));
                        else
                        {
                            if (Entity.IsPlayer())
                                Damage = MyMath.GetDamagePlayer2Player(Attacker, Player, Attacker.MagicType, Attacker.MagicLevel);
                            else if (Entity.IsMonster())
                                Damage = MyMath.GetDamagePlayer2Monster(Attacker, Monster, Attacker.MagicType, Attacker.MagicLevel);
                            else if (Entity.IsTerrainNPC())
                                Damage = MyMath.GetDamagePlayer2Environment(Attacker, Npc, Attacker.MagicType, Attacker.MagicLevel);
                        }

                        //TargetType -> 8; Passive skill
                        if (Info.Target != 8 && Info.Success != 100 && !MyMath.Success(Info.Success))
                            Damage = 0;
                        #endregion

                        #region Process damage / skill
                        switch (Info.ActionSort)
                        {
                            case 2:
                                {
                                    if (Entity.CurHP + Damage >= Entity.MaxHP)
                                    {
                                        MagicExp += Entity.MaxHP - Entity.CurHP;
                                        Entity.CurHP = Entity.MaxHP;
                                    }
                                    else
                                    {
                                        MagicExp += Damage;
                                        Entity.CurHP += Damage;
                                    }

                                    if (Entity.IsPlayer())
                                    {
                                        Byte[] Msg = MsgUserAttrib.Create(Player, Player.CurHP, MsgUserAttrib.Type.Life);
                                        Player.Send(Msg);
                                        if (Player.Team != null)
                                            World.BroadcastTeamMsg(Player.Team, Msg);
                                        Msg = null;
                                    }
                                    break;
                                }
                            case 6:
                                {
                                    switch (Attacker.MagicType)
                                    {
                                        case 1015:
                                            {
                                                if (Entity.AccuracyEndTime != 0)
                                                {
                                                    Entity.AccuracyEndTime = Environment.TickCount + ((Int32)Info.Time * 1000);
                                                    break;
                                                }

                                                Entity.AccuracyEndTime = Environment.TickCount + ((Int32)Info.Time * 1000);
                                                Entity.StarOfAccuracyEndTime = 0;
                                                Entity.DexterityBonus = ((Double)(Damage - 30000) / 100.00);
                                                Entity.AddFlag(Player.Flag.Accuracy);
                                                if (Entity.IsPlayer())
                                                {
                                                    MyMath.GetEquipStats(Player);
                                                    World.BroadcastRoomMsg(Player, MsgUserAttrib.Create(Player, Player.Flags, MsgUserAttrib.Type.Flags), true);
                                                }
                                                else
                                                    World.BroadcastRoomMsg(Entity, MsgUserAttrib.Create(Entity, Entity.Flags, MsgUserAttrib.Type.Flags));
                                                break;
                                            }
                                        case 1020:
                                            {
                                                if (Entity.ShieldEndTime != 0)
                                                {
                                                    Entity.ShieldEndTime = Environment.TickCount + ((Int32)Info.Time * 1000);
                                                    break;
                                                }

                                                Entity.ShieldEndTime = Environment.TickCount + ((Int32)Info.Time * 1000);
                                                Entity.MagicShieldEndTime = 0;
                                                Entity.DefenceBonus = ((Double)(Damage - 30000) / 100.00);
                                                Entity.AddFlag(Player.Flag.Shield);
                                                if (Entity.IsPlayer())
                                                {
                                                    MyMath.GetEquipStats(Player);
                                                    World.BroadcastRoomMsg(Player, MsgUserAttrib.Create(Player, Player.Flags, MsgUserAttrib.Type.Flags), true);
                                                }
                                                else
                                                    World.BroadcastRoomMsg(Entity, MsgUserAttrib.Create(Entity, Entity.Flags, MsgUserAttrib.Type.Flags));
                                                break;
                                            }
                                        case 1025:
                                            {
                                                if (Entity.SupermanEndTime != 0)
                                                {
                                                    Entity.SupermanEndTime = Environment.TickCount + ((Int32)Info.Time * 1000);
                                                    break;
                                                }

                                                if (Entity.CycloneEndTime != 0)
                                                    Entity.CycloneEndTime = Environment.TickCount;

                                                Entity.SupermanEndTime = Environment.TickCount + ((Int32)Info.Time * 1000);
                                                Entity.StigmaEndTime = 0;
                                                Entity.AttackBonus = ((Double)(Damage - 30000) / 100.00);
                                                Entity.AddFlag(Player.Flag.SuperMan);
                                                Entity.DelFlag(Player.Flag.Stigma);
                                                if (Entity.IsPlayer())
                                                {
                                                    MyMath.GetEquipStats(Player);
                                                    World.BroadcastRoomMsg(Player, MsgUserAttrib.Create(Player, Player.Flags, MsgUserAttrib.Type.Flags), true);
                                                }
                                                else
                                                    World.BroadcastRoomMsg(Entity, MsgUserAttrib.Create(Entity, Entity.Flags, MsgUserAttrib.Type.Flags));
                                                break;
                                            }
                                        case 1075:
                                            {
                                                MagicExp++;
                                                if (Entity.InvisibilityEndTime != 0)
                                                {
                                                    Entity.InvisibilityEndTime = Environment.TickCount + ((Int32)Info.Time * 1000);
                                                    break;
                                                }

                                                Entity.InvisibilityEndTime = Environment.TickCount + ((Int32)Info.Time * 1000);
                                                Entity.AddFlag(Player.Flag.Invisibility);

                                                if (Entity.IsPlayer())
                                                    World.BroadcastRoomMsg(Player, MsgUserAttrib.Create(Player, Player.Flags, MsgUserAttrib.Type.Flags), true);
                                                else
                                                    World.BroadcastRoomMsg(Entity, MsgUserAttrib.Create(Entity, Entity.Flags, MsgUserAttrib.Type.Flags));
                                                break;
                                            }
                                        case 1085:
                                            {
                                                MagicExp++;
                                                if (Entity.StarOfAccuracyEndTime != 0)
                                                {
                                                    Entity.StarOfAccuracyEndTime = Environment.TickCount + ((Int32)Info.Time * 1000);
                                                    break;
                                                }

                                                Entity.StarOfAccuracyEndTime = Environment.TickCount + ((Int32)Info.Time * 1000);
                                                Entity.AccuracyEndTime = 0;
                                                Entity.DexterityBonus = ((Double)(Damage - 30000) / 100.00);
                                                Entity.AddFlag(Player.Flag.Accuracy);
                                                if (Entity.IsPlayer())
                                                {
                                                    MyMath.GetEquipStats(Player);
                                                    World.BroadcastRoomMsg(Player, MsgUserAttrib.Create(Player, Player.Flags, MsgUserAttrib.Type.Flags), true);
                                                }
                                                else
                                                    World.BroadcastRoomMsg(Entity, MsgUserAttrib.Create(Entity, Entity.Flags, MsgUserAttrib.Type.Flags));
                                                break;
                                            }
                                        case 1090:
                                            {
                                                MagicExp++;
                                                if (Entity.MagicShieldEndTime != 0)
                                                {
                                                    Entity.MagicShieldEndTime = Environment.TickCount + ((Int32)Info.Time * 1000);
                                                    break;
                                                }

                                                Entity.MagicShieldEndTime = Environment.TickCount + ((Int32)Info.Time * 1000);
                                                Entity.ShieldEndTime = 0;
                                                Entity.DefenceBonus = ((Double)(Damage - 30000) / 100.00);
                                                Entity.AddFlag(Player.Flag.Shield);
                                                if (Entity.IsPlayer())
                                                {
                                                    MyMath.GetEquipStats(Player);
                                                    World.BroadcastRoomMsg(Player, MsgUserAttrib.Create(Player, Player.Flags, MsgUserAttrib.Type.Flags), true);
                                                }
                                                else
                                                    World.BroadcastRoomMsg(Entity, MsgUserAttrib.Create(Entity, Entity.Flags, MsgUserAttrib.Type.Flags));
                                                break;
                                            }
                                        case 1095:
                                            {
                                                MagicExp++;
                                                if (Entity.StigmaEndTime != 0)
                                                {
                                                    Entity.StigmaEndTime = Environment.TickCount + ((Int32)Info.Time * 1000);
                                                    break;
                                                }

                                                Entity.StigmaEndTime = Environment.TickCount + ((Int32)Info.Time * 1000);
                                                Entity.SupermanEndTime = 0;
                                                Entity.AttackBonus = ((Double)(Damage - 30000) / 100.00);
                                                Entity.AddFlag(Player.Flag.Stigma);
                                                Entity.DelFlag(Player.Flag.SuperMan);
                                                if (Entity.IsPlayer())
                                                {
                                                    MyMath.GetEquipStats(Player);
                                                    World.BroadcastRoomMsg(Player, MsgUserAttrib.Create(Player, Player.Flags, MsgUserAttrib.Type.Flags), true);
                                                }
                                                else
                                                    World.BroadcastRoomMsg(Entity, MsgUserAttrib.Create(Entity, Entity.Flags, MsgUserAttrib.Type.Flags));
                                                break;
                                            }
                                        case 1110:
                                            {
                                                if (Entity.CycloneEndTime != 0)
                                                {
                                                    Entity.CycloneEndTime = Environment.TickCount + ((Int32)Info.Time * 1000);
                                                    break;
                                                }

                                                if (Entity.SupermanEndTime != 0)
                                                    Entity.SupermanEndTime = Environment.TickCount;

                                                Entity.CycloneEndTime = Environment.TickCount + ((Int32)Info.Time * 1000);
                                                Entity.SpeedBonus = ((Double)(Damage - 30000) / 100.00);
                                                Entity.AddFlag(Player.Flag.Cyclone);
                                                if (Entity.IsPlayer())
                                                {
                                                    MyMath.GetEquipStats(Player);
                                                    World.BroadcastRoomMsg(Player, MsgUserAttrib.Create(Player, Player.Flags, MsgUserAttrib.Type.Flags), true);
                                                }
                                                else
                                                    World.BroadcastRoomMsg(Entity, MsgUserAttrib.Create(Entity, Entity.Flags, MsgUserAttrib.Type.Flags));
                                                break;
                                            }
                                        case 8002:
                                            {
                                                if (Entity.ShieldEndTime != 0 || Entity.MagicShieldEndTime != 0)
                                                    break;

                                                if (Entity.FlyEndTime != 0)
                                                {
                                                    Entity.FlyEndTime = Environment.TickCount + ((Int32)Info.Time * 1000);
                                                    break;
                                                }

                                                Entity.FlyEndTime = Environment.TickCount + ((Int32)Info.Time * 1000);
                                                Entity.AddFlag(Player.Flag.Flying);

                                                if (Entity.IsPlayer())
                                                    World.BroadcastRoomMsg(Player, MsgUserAttrib.Create(Player, Player.Flags, MsgUserAttrib.Type.Flags), true);
                                                else
                                                    World.BroadcastRoomMsg(Entity, MsgUserAttrib.Create(Entity, Entity.Flags, MsgUserAttrib.Type.Flags));
                                                break;
                                            }
                                        case 3510:
                                            {
                                                MagicExp++;
                                                if (Entity.AzureShieldEndTime != 0)
                                                {
                                                    Entity.AzureShieldEndTime = Environment.TickCount + ((Int32)Info.Time * 1000);
                                                    break;
                                                }

                                                Entity.AzureShieldEndTime = Environment.TickCount + ((Int32)Info.Time * 1000);
                                                Entity.DefenceAddBonus = Damage;
                                                Entity.AddFlag(Player.Flag.AzureShield);
                                                if (Entity.IsPlayer())
                                                {
                                                    MyMath.GetEquipStats(Player);
                                                    World.BroadcastRoomMsg(Player, MsgUserAttrib.Create(Player, Player.Flags, MsgUserAttrib.Type.Flags), true);
                                                }
                                                else
                                                    World.BroadcastRoomMsg(Entity, MsgUserAttrib.Create(Entity, Entity.Flags, MsgUserAttrib.Type.Flags));
                                                break;
                                            }
                                        case 8003:
                                            {
                                                if (Entity.ShieldEndTime != 0 || Entity.MagicShieldEndTime != 0)
                                                    break;

                                                if (Entity.FlyEndTime != 0)
                                                {
                                                    Entity.FlyEndTime = Environment.TickCount + ((Int32)Info.Time * 1000);
                                                    break;
                                                }

                                                Entity.FlyEndTime = Environment.TickCount + ((Int32)Info.Time * 1000);
                                                Entity.AddFlag(Player.Flag.Flying);

                                                if (Entity.IsPlayer())
                                                    World.BroadcastRoomMsg(Player, MsgUserAttrib.Create(Player, Player.Flags, MsgUserAttrib.Type.Flags), true);
                                                else
                                                    World.BroadcastRoomMsg(Entity, MsgUserAttrib.Create(Entity, Entity.Flags, MsgUserAttrib.Type.Flags));
                                                break;
                                            }
                                        case 9876:
                                            {
                                                Entity.AddFlag(Player.Flag.CastingPray);

                                                if (Entity.IsPlayer())
                                                {
                                                    Player.CastingPray = true;
                                                    Player.PrayMap = Entity.Map;
                                                    Player.PrayX = Entity.X;
                                                    Player.PrayY = Entity.Y;
                                                }

                                                if (Entity.IsPlayer())
                                                    World.BroadcastRoomMsg(Player, MsgUserAttrib.Create(Player, Player.Flags, MsgUserAttrib.Type.Flags), true);
                                                else
                                                    World.BroadcastRoomMsg(Entity, MsgUserAttrib.Create(Entity, Entity.Flags, MsgUserAttrib.Type.Flags));
                                                break;
                                            }
                                        case 10203:
                                            {
                                                if (Entity.MagicShieldEndTime != 0)
                                                {
                                                    Entity.MagicShieldEndTime = Environment.TickCount + ((Int32)Info.Time * 1000);
                                                    break;
                                                }

                                                Entity.MagicShieldEndTime = Environment.TickCount + ((Int32)Info.Time * 1000);
                                                Entity.ShieldEndTime = 0;
                                                Entity.DefenceBonus = ((Double)(Damage - 30000) / 100.00);
                                                Entity.AddFlag(Player.Flag.Shield);
                                                if (Entity.IsPlayer())
                                                {
                                                    MyMath.GetEquipStats(Player);
                                                    World.BroadcastRoomMsg(Player, MsgUserAttrib.Create(Player, Player.Flags, MsgUserAttrib.Type.Flags), true);
                                                }
                                                else
                                                    World.BroadcastRoomMsg(Entity, MsgUserAttrib.Create(Entity, Entity.Flags, MsgUserAttrib.Type.Flags));
                                                break;
                                            }
                                        default:
                                            Attacker.SendSysMsg("Magic[" + Attacker.MagicType + "] not implemented yet!");
                                            break;
                                    }
                                    Damage = 0;
                                    break;
                                }
                            case 7:
                                {
                                    if (Entity.IsPlayer() && !Entity.IsAlive())
                                        Player.Reborn(false);
                                    break;
                                }
                            case 11:
                                {
                                    if (Entity.IsPlayer())
                                    {
                                        if (Player.XP + Damage >= 100)
                                            Player.XP = 100;
                                        else
                                            Player.XP += (Byte)Damage;
                                        Player.Send(MsgUserAttrib.Create(Player, Player.XP, MsgUserAttrib.Type.XP));
                                    }
                                    break;
                                }
                            case 19:
                                {
                                    if (Entity.IsPlayer())
                                    {
                                        MonsterInfo MonsterInfo;
                                        if (Database.AllMonsters.TryGetValue(Damage, out MonsterInfo))
                                        {
                                            MagicExp++;
                                            Player.TransformEndTime = Environment.TickCount + ((Int32)Info.Time * 1000);
                                            Player.AddTransform(MonsterInfo.Look);
                                            Player.MinAtk = MonsterInfo.MinAtk;
                                            Player.MaxAtk = MonsterInfo.MaxAtk;
                                            Player.Defence = MonsterInfo.Defence;
                                            Player.MagicAtk = 0;
                                            Player.MagicDef = MonsterInfo.MagicDef;
                                            Player.MagicBlock = 0;
                                            Player.Dodge = MonsterInfo.Dodge;
                                            Player.Weight = 0;
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

                                            Player.Send(MsgUserAttrib.Create(Player, Player.CurHP, MsgUserAttrib.Type.Life));
                                            Player.Send(MsgUserAttrib.Create(Player, Player.MaxHP, (MsgUserAttrib.Type.Life + 1)));
                                            if (Player.Team != null)
                                            {
                                                World.BroadcastTeamMsg(Player.Team, MsgUserAttrib.Create(Player, Player.CurHP, MsgUserAttrib.Type.Life));
                                                World.BroadcastTeamMsg(Player.Team, MsgUserAttrib.Create(Player, Player.MaxHP, (MsgUserAttrib.Type.Life + 1)));
                                            }
                                            World.BroadcastRoomMsg(Player, MsgUserAttrib.Create(Player, Player.Look, MsgUserAttrib.Type.Look), true);
                                        }
                                    }
                                    Damage = 0;
                                    break;
                                }
                            case 20:
                                {
                                    if (Entity.IsPlayer())
                                    {
                                        if (Player.CurMP + Damage >= Player.MaxMP)
                                        {
                                            MagicExp += Player.MaxMP - Player.CurMP;
                                            Player.CurMP = Player.MaxMP;
                                        }
                                        else
                                        {
                                            MagicExp += Damage;
                                            Player.CurMP += (UInt16)Damage;
                                        }
                                        Player.Send(MsgUserAttrib.Create(Player, Player.CurMP, MsgUserAttrib.Type.Mana));
                                    }
                                    break;
                                }
                            default:
                                {
                                    if (Entity.IsPlayer())
                                        Player.RemoveDefDura();

                                    #region Not a player || Not reflected
                                    if (!Entity.IsPlayer() || (Entity.IsPlayer() && !Player.Reflect()))
                                    {
                                        if (Damage >= Entity.CurHP)
                                        {
                                            if (Entity.IsPlayer())
                                            {
                                                Player.Die();
                                                if (Info.Crime == 1)
                                                {
                                                    if (!Map.IsPkField() && !Map.IsSynMap() && !Map.IsPrisonMap())
                                                    {
                                                        if (!Player.ContainsFlag(Player.Flag.RedName) &&
                                                            !Player.ContainsFlag(Player.Flag.BlackName) &&
                                                            !Player.ContainsFlag(Player.Flag.Flashing))
                                                        {
                                                            if (Attacker.Enemies.ContainsKey(Player.UniqId))
                                                                Attacker.PkPoints += 5;
                                                            else if (Attacker.Syndicate != null && Player.Syndicate != null
                                                                && Attacker.Syndicate.IsAnEnemy(Player.Syndicate.UniqId))
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

                                                            if (Player.BlessEndTime != 0 && Attacker.BlessEndTime == 0)
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

                                                        if (!Player.Enemies.ContainsKey(Attacker.UniqId))
                                                        {
                                                            Player.Enemies.Add(Attacker.UniqId, Attacker.Name);
                                                            Player.Send(MsgFriend.Create(Attacker.UniqId, Attacker.Name, true, MsgFriend.Action.EnemyAdd));
                                                        }

                                                        if (Player.BlessEndTime == 0)
                                                        {
                                                            UInt64 Exp2 = (UInt64)(Player.Exp * 0.02);
                                                            if (Target.ContainsFlag(Player.Flag.RedName))
                                                                Exp2 = (UInt64)(Player.Exp * 0.10);
                                                            else if (Target.ContainsFlag(Player.Flag.BlackName))
                                                                Exp2 = (UInt64)(Player.Exp * 0.20);

                                                            //if ((Targets1[i] as Player).Syndicate != null)
                                                            //{
                                                            //    Syndicate.Member Member = (Targets1[i] as Player).Syndicate.GetMemberInfo(Target.UniqId);
                                                            //    if (Member != null)
                                                            //    {
                                                            //        UInt64 Compensation = 0;
                                                            //        if (Member.Donation > 0)
                                                            //        {
                                                            //            if (Member.Rank == 100) //Guild Leader
                                                            //                Compensation = (UInt64)(Exp2 * 0.80);
                                                            //            else if (Member.Rank == 90) //Deputy Leader
                                                            //                Compensation = (UInt64)(Exp2 * 0.60);
                                                            //            else if (Member.Rank == 80)
                                                            //                Compensation = (UInt64)(Exp2 * 0.40);
                                                            //            else if (Member.Rank == 70)
                                                            //                Compensation = (UInt64)(Exp2 * 0.35);
                                                            //            else if (Member.Rank == 60)
                                                            //                Compensation = (UInt64)(Exp2 * 0.30);
                                                            //            else
                                                            //                Compensation = (UInt64)(Exp2 * 0.25);
                                                            //        }
                                                            //        else
                                                            //            Compensation = (UInt64)(Exp2 * 0.20);

                                                            //        if (Compensation > Int32.MaxValue)
                                                            //            Compensation = Int32.MaxValue;

                                                            //        (Targets1[i] as Player).Syndicate.Money -= (Int32)Compensation;
                                                            //        Member.Donation -= (Int32)Compensation;
                                                            //        Exp2 -= Compensation;

                                                            //        World.SynThread.AddToQueue((Targets1[i] as Player).Syndicate, "Money", (Targets1[i] as Player).Syndicate.Money);
                                                            //    }
                                                            //}

                                                            if (Player.Exp < Exp2)
                                                                Player.Exp = 0;
                                                            else
                                                                Player.Exp -= Exp2;
                                                            Player.Send(MsgUserAttrib.Create(Player, (Int64)Player.Exp, MsgUserAttrib.Type.Exp));
                                                            Attacker.AddExp(Exp2 * 0.10, false);
                                                        }

                                                        {
                                                            Int32 CPs = (Int32)(Player.CPs * 0.02);
                                                            if (Player.ContainsFlag(Player.Flag.RedName))
                                                                CPs = (Int32)(Player.CPs * 0.10);
                                                            else if (Player.ContainsFlag(Player.Flag.BlackName))
                                                                CPs = (Int32)(Player.CPs * 0.20);

                                                            Int32 Compensation = 0;
                                                            if (Player.Syndicate != null)
                                                            {
                                                                Syndicate.Member Member = Player.Syndicate.GetMemberInfo(Player.UniqId);
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

                                                            if (Player.CPs < CPs)
                                                                Player.CPs = 0;
                                                            else
                                                                Player.CPs -= CPs + Compensation;
                                                            Player.Send(MsgUserAttrib.Create(Player, Player.CPs, MsgUserAttrib.Type.CPs));

                                                            Attacker.CPs += CPs;
                                                            Attacker.Send(MsgUserAttrib.Create(Attacker, Attacker.CPs, MsgUserAttrib.Type.CPs));
                                                        }

                                                        if (Player.ContainsFlag(Player.Flag.RedName) ||
                                                            Player.ContainsFlag(Player.Flag.BlackName))
                                                            Player.DropEquipment();

                                                        if (Player.ContainsFlag(Player.Flag.BlackName))
                                                        {
                                                            World.BroadcastMsg(MsgTalk.Create("SYSTEM", "ALLUSERS", STR.Get("STR_SENT_TO_JAIL"), MsgTalk.Channel.GM, 0xFF0000));
                                                            Player.Move(6000, 29, 72);
                                                        }
                                                    }
                                                }
                                            }
                                            else if (Entity.IsMonster())
                                            {
                                                if (Attacker.XP < 99)
                                                    Attacker.XP++;
                                                if (Attacker.CycloneEndTime != 0)
                                                {
                                                    Attacker.CycloneEndTime += 820;
                                                    Attacker.CurKO++;
                                                }
                                                if (Attacker.SupermanEndTime != 0)
                                                {
                                                    Attacker.SupermanEndTime += 820;
                                                    Attacker.CurKO++;
                                                }

                                                if (Monster.Id == 5053 || Monster.Id == 5054 || Monster.Id == 5055 || Monster.Id == 5056 || Monster.Id == 5057)
                                                {
                                                    Attacker.DisKO++;
                                                    Attacker.SendSysMsg("Vous avez tué " + Attacker.DisKO + " sur " + Attacker.DisToKill + " monstres!");
                                                }

                                                Int32 CurHP = Entity.CurHP;
                                                Monster.Die(Attacker.UniqId);

                                                Exp += AdjustExp(CurHP, Attacker, Monster);
                                                MagicExp += AdjustExp(CurHP, Attacker, Monster);

                                                Int32 Bonus = (Int32)(Monster.MaxHP * 0.05);
                                                if (Attacker.Team != null)
                                                    Attacker.Team.AwardMemberExp(Attacker, Monster, Bonus);

                                                Int32 BonusExp = AdjustExp(Bonus, Attacker, Monster);
                                                Attacker.AddExp(BonusExp, true);
                                                Attacker.SendSysMsg(String.Format(Attacker.Owner.GetStr("STR_KILL_EXPERIENCE"), BonusExp));
                                            }
                                            else if (Entity.IsTerrainNPC())
                                            {
                                                if (Attacker.XP < 99)
                                                    Attacker.XP++;

                                                Int32 CurHP = Entity.CurHP;

                                                if (Npc.Type == 21 || Npc.Type == 22)
                                                {
                                                    Exp += AdjustExp(CurHP, Attacker, Npc);
                                                    MagicExp += AdjustExp(CurHP, Attacker, Npc);

                                                    Int32 Bonus = (Int32)(Entity.MaxHP * 0.05);

                                                    Int32 BonusExp = AdjustExp(Bonus, Attacker, Npc);
                                                    Attacker.AddExp(BonusExp, true);
                                                }
                                                else if (Npc.Type == (Byte)TerrainNPC.NpcType.SynFlag)
                                                {
                                                    if (Attacker.Syndicate != null)
                                                    {
                                                        if (World.AllMaps[Attacker.Map].InWar)
                                                            if (Attacker.Syndicate.UniqId != World.AllMaps[Attacker.Map].Holder)
                                                            {
                                                                Attacker.Money += (CurHP / 10000);
                                                                Attacker.Send(MsgUserAttrib.Create(Attacker, Attacker.Money, MsgUserAttrib.Type.Money));

                                                                Syndicate.Member Member = Attacker.Syndicate.GetMemberInfo(Attacker.UniqId);
                                                                if (Member != null)
                                                                {
                                                                    Member.Donation += (CurHP / 10000);
                                                                    Attacker.Syndicate.Money += (CurHP / 10000);
                                                                    World.SynThread.AddToQueue(Attacker.Syndicate, "Money", Attacker.Syndicate.Money);
                                                                }
                                                            }
                                                    }
                                                }
                                                Npc.GetAttacked(Attacker, Npc.CurHP);

                                                World.BroadcastMapMsg(Attacker, MsgInteract.Create(Attacker, Entity, 1, MsgInteract.Action.Kill), true);
                                                Npc.Die();
                                            }
                                            if (!Entity.IsTerrainNPC())
                                            {
                                                if (!Entity.IsMonster() || (Attacker.SupermanEndTime == 0 && Attacker.CycloneEndTime == 0))
                                                    World.BroadcastMapMsg(Attacker, MsgInteract.Create(Attacker, Entity, 1, MsgInteract.Action.Kill), true);
                                                else
                                                    World.BroadcastMapMsg(Attacker, MsgInteract.Create(Attacker, Entity, 0xFFFF * Attacker.CurKO, MsgInteract.Action.Kill), true);
                                            }
                                        }
                                        else
                                        {
                                            Entity.CurHP -= Damage;
                                            if (Entity.IsPlayer())
                                            {
                                                Player.Send(MsgUserAttrib.Create(Entity, Entity.CurHP, MsgUserAttrib.Type.Life));
                                                if (Player.Team != null)
                                                    World.BroadcastTeamMsg(Player.Team, MsgUserAttrib.Create(Entity, Entity.CurHP, MsgUserAttrib.Type.Life));

                                                if (Player.Action == (Byte)MsgAction.Emotion.SitDown)
                                                {
                                                    Player.Energy /= 2;
                                                    Player.Send(MsgUserAttrib.Create(Player, Player.Energy, MsgUserAttrib.Type.Energy));

                                                    Player.Action = (Byte)MsgAction.Emotion.StandBy;
                                                    World.BroadcastRoomMsg(Player, MsgAction.Create(Player, (Byte)MsgAction.Emotion.StandBy, MsgAction.Action.Emotion), true);
                                                }
                                            }
                                            else if (Entity.IsMonster())
                                            {
                                                Exp += AdjustExp(Damage, Attacker, Monster);
                                                MagicExp += AdjustExp(Damage, Attacker, Monster);
                                            }
                                            else if (Entity.IsTerrainNPC())
                                            {
                                                if (Npc.Type == 21 || Npc.Type == 22)
                                                {
                                                    Exp += AdjustExp(Damage, Attacker, Npc);
                                                    MagicExp += AdjustExp(Damage, Attacker, Npc);
                                                }
                                                else if (Npc.Type == (Byte)TerrainNPC.NpcType.SynFlag)
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
                                                Npc.GetAttacked(Attacker, Damage);
                                            }
                                        }
                                    }
                                    #endregion
                                    #region Reflected
                                    else
                                    {
                                        if (Damage > 2000)
                                            Damage = 2000;

                                        World.BroadcastMapMsg(Attacker, MsgInteract.Create(Entity, Attacker, Damage, MsgInteract.Action.ReflectMagic), true);
                                        if (Damage >= Attacker.CurHP)
                                        {
                                            Attacker.Die();
                                            World.BroadcastMapMsg(Player, MsgInteract.Create(Entity, Attacker, 1, MsgInteract.Action.Kill), true);
                                        }
                                        else
                                        {
                                            Attacker.CurHP -= Damage;
                                            Attacker.Send(MsgUserAttrib.Create(Attacker, Attacker.CurHP, MsgUserAttrib.Type.Life));
                                            if (Attacker.Team != null)
                                                World.BroadcastTeamMsg(Attacker.Team, MsgUserAttrib.Create(Attacker, Attacker.CurHP, MsgUserAttrib.Type.Life));
                                        }
                                        Reflected = true;
                                    }
                                    #endregion
                                    break;
                                }
                        }
                        #endregion

                        if (Target != null && Info.ActionSort != 11 && Info.ActionSort != 2 && !Reflected)
                            World.BroadcastMapMsg(Attacker, MsgMagicEffect.Create(Attacker, Target, Damage, X, Y), true);
                        else if (!Reflected)
                            Targets.Add(Entity, Damage);
                    }
                    Attacker.LastAttackTick = Environment.TickCount;

                    if (Attacker.Map != 1039)
                    {
                        if (Attacker.MagicType == 8000)
                            for (SByte i = 0; i < 5; i++)
                                Attacker.RemoveAtkDura();
                        else if (Attacker.MagicType == 8001)
                            for (SByte i = 0; i < 3; i++)
                                Attacker.RemoveAtkDura();
                        else
                            Attacker.RemoveAtkDura();
                    }

                    Attacker.AddExp(Exp, true);
                    if (MagicExp > 0 && Info.ActionSort != 16)
                        Attacker.AddMagicExp(Attacker.MagicType, MagicExp);
                    else if (MagicExp > 0)
                        Attacker.AddMagicExp(Attacker.MagicType, 1);

                    if (Target == null || Info.ActionSort == 11 || Info.ActionSort == 2)
                        World.BroadcastMapMsg(Attacker, MsgMagicEffect.Create(Attacker, Targets, X, Y), true);

                    if (Attacker.Map != 1039)
                        Battle.Magic.WeaponAttribute(Attacker, null);
                }
                catch (Exception Exc) { Program.WriteLine(Exc); }
            }
            #endregion

            #region Use(Monster, AdvancedEntity, UInt16, UInt16)
            public static void Use(Monster Attacker, AdvancedEntity Target, UInt16 X, UInt16 Y)
            {
                try
                {
                    if (!Attacker.IsAlive())
                        return;

                    MagicType.Entry Info = new MagicType.Entry();
                    if (!Database2.AllMagics.TryGetValue((Attacker.MagicType * 10) + Attacker.MagicLevel, out Info))
                        return;

                    Map Map = null;
                    if (!World.AllMaps.TryGetValue(Attacker.Map, out Map))
                        return;

                    AdvancedEntity[] Targets1;
                    if (Target == null || Info.ActionSort == 2 || Info.ActionSort == 11)
                    {
                        switch (Info.ActionSort)
                        {
                            case 2:
                                {
                                    Targets1 = new AdvancedEntity[] { Target };
                                    break;
                                }
                            case 4:
                                Targets1 = Battle.Magic.GetTargetsForType04(Attacker, X, Y, Info.Range, Info.Distance);
                                break;
                            case 5:
                                Targets1 = Battle.Magic.GetTargetsForType05(Attacker, X, Y, Info.Range);
                                break;
                            case 14:
                                Targets1 = Battle.Magic.GetTargetsForType14(Attacker, X, Y, Info.Range);
                                break;
                            default:
                                Targets1 = new AdvancedEntity[0];
                                break;
                        }
                    }
                    else
                        Targets1 = new AdvancedEntity[] { Target };

                    if (Info.IntoneDuration > 0)
                    {
                        Attacker.MagicIntone = true;
                        //Thread.Sleep((Int32)Info.IntoneDuration);

                        if (!Attacker.MagicIntone)
                            return;

                        Attacker.MagicIntone = false;
                    }

                    Dictionary<AdvancedEntity, Int32> Targets = new Dictionary<AdvancedEntity, Int32>(Targets1.Length);
                    for (Int32 i = 0; i < Targets1.Length; i++)
                    {
                        Boolean Reflected = false;

                        if (Targets1[i] == null || (!Targets1[i].IsAlive() && Info.ActionSort != 7))
                            continue;

                        if (Attacker.Map != Targets1[i].Map)
                            continue;

                        if (!MyMath.CanSee(Attacker.X, Attacker.Y, Targets1[i].X, Targets1[i].Y, (Int32)Info.Distance))
                            continue;

                        if (Info.Target == 0 && Attacker.UniqId == Targets1[i].UniqId)
                            continue;
                        else if (Info.Target == 2 && Attacker.UniqId != Targets1[i].UniqId)
                            continue;
                        else if (Info.Target == 4 && Attacker.UniqId == Targets1[i].UniqId)
                            continue;
                        else if (Info.Target == 8 && Attacker.UniqId == Targets1[i].UniqId)
                            continue;

                        Int32 Damage = 0;
                        if (Info.ActionSort == 2 || Info.ActionSort == 6 || Info.ActionSort == 11 || Info.ActionSort == 19 || Info.ActionSort == 20)
                            Damage = (Int32)Info.Power;
                        else if (Info.ActionSort == 7)
                            Damage = Targets1[i].MaxHP;
                        else if (Info.ActionSort == 26)
                            Damage = (Int32)(Targets1[i].CurHP * ((Double)(Info.Power - 30000) / 100.00));
                        else
                        {
                            if (Targets1[i].IsPlayer())
                                Damage = MyMath.GetDamageMonster2Player(Attacker, (Targets1[i] as Player), Attacker.MagicType, Attacker.MagicLevel);
                        }

                        //TargetType -> 8; Passive skill
                        if (Info.Target != 8 && Info.Success != 100 && !MyMath.Success(Info.Success))
                            Damage = 0;

                        if (Info.ActionSort == 2)
                        {
                            if (Targets1[i].CurHP + Damage >= Targets1[i].MaxHP)
                                Targets1[i].CurHP = Targets1[i].MaxHP;
                            else
                                Targets1[i].CurHP += Damage;

                            if (Targets1[i].IsPlayer())
                            {
                                (Targets1[i] as Player).Send(MsgUserAttrib.Create(Targets1[i], Targets1[i].CurHP, MsgUserAttrib.Type.Life));
                                if ((Targets1[i] as Player).Team != null)
                                    World.BroadcastTeamMsg((Targets1[i] as Player).Team, MsgUserAttrib.Create((Targets1[i] as Player), (Targets1[i] as Player).CurHP, MsgUserAttrib.Type.Life));
                            }
                        }
                        else if (Info.ActionSort == 6)
                        {
                            switch (Attacker.MagicType)
                            {
                                case 1015:
                                    {
                                        if (Targets1[i].AccuracyEndTime != 0)
                                        {
                                            Targets1[i].AccuracyEndTime = Environment.TickCount + ((Int32)Info.Time * 1000);
                                            break;
                                        }

                                        Targets1[i].AccuracyEndTime = Environment.TickCount + ((Int32)Info.Time * 1000);
                                        Targets1[i].StarOfAccuracyEndTime = 0;
                                        Targets1[i].DexterityBonus = ((Double)(Damage - 30000) / 100.00);
                                        Targets1[i].AddFlag(Player.Flag.Accuracy);
                                        if (Targets1[i].IsPlayer())
                                            MyMath.GetEquipStats((Targets1[i] as Player));

                                        if (Targets1[i].IsPlayer())
                                            World.BroadcastRoomMsg((Targets1[i] as Player), MsgUserAttrib.Create(Targets1[i], Targets1[i].Flags, MsgUserAttrib.Type.Flags), true);
                                        else
                                            World.BroadcastRoomMsg(Targets1[i], MsgUserAttrib.Create(Targets1[i], Targets1[i].Flags, MsgUserAttrib.Type.Flags));
                                        break;
                                    }
                                case 1020:
                                    {
                                        if (Targets1[i].ShieldEndTime != 0)
                                        {
                                            Targets1[i].ShieldEndTime = Environment.TickCount + ((Int32)Info.Time * 1000);
                                            break;
                                        }

                                        Targets1[i].ShieldEndTime = Environment.TickCount + ((Int32)Info.Time * 1000);
                                        Targets1[i].MagicShieldEndTime = 0;
                                        Targets1[i].DefenceBonus = ((Double)(Damage - 30000) / 100.00);
                                        Targets1[i].AddFlag(Player.Flag.Shield);
                                        if (Targets1[i].IsPlayer())
                                            MyMath.GetEquipStats((Targets1[i] as Player));

                                        if (Targets1[i].IsPlayer())
                                            World.BroadcastRoomMsg((Targets1[i] as Player), MsgUserAttrib.Create(Targets1[i], Targets1[i].Flags, MsgUserAttrib.Type.Flags), true);
                                        else
                                            World.BroadcastRoomMsg(Targets1[i], MsgUserAttrib.Create(Targets1[i], Targets1[i].Flags, MsgUserAttrib.Type.Flags));
                                        break;
                                    }
                                case 1025:
                                    {
                                        if (Targets1[i].SupermanEndTime != 0)
                                        {
                                            Targets1[i].SupermanEndTime = Environment.TickCount + ((Int32)Info.Time * 1000);
                                            break;
                                        }

                                        Targets1[i].SupermanEndTime = Environment.TickCount + ((Int32)Info.Time * 1000);
                                        Targets1[i].StigmaEndTime = 0;
                                        Targets1[i].AttackBonus = ((Double)(Damage - 30000) / 100.00);
                                        Targets1[i].AddFlag(Player.Flag.SuperMan);
                                        Targets1[i].DelFlag(Player.Flag.Stigma);
                                        if (Targets1[i].IsPlayer())
                                            MyMath.GetEquipStats((Targets1[i] as Player));

                                        if (Targets1[i].IsPlayer())
                                            World.BroadcastRoomMsg((Targets1[i] as Player), MsgUserAttrib.Create(Targets1[i], Targets1[i].Flags, MsgUserAttrib.Type.Flags), true);
                                        else
                                            World.BroadcastRoomMsg(Targets1[i], MsgUserAttrib.Create(Targets1[i], Targets1[i].Flags, MsgUserAttrib.Type.Flags));
                                        break;
                                    }
                                case 1075:
                                    {
                                        if (Targets1[i].InvisibilityEndTime != 0)
                                        {
                                            Targets1[i].InvisibilityEndTime = Environment.TickCount + ((Int32)Info.Time * 1000);
                                            break;
                                        }

                                        Targets1[i].InvisibilityEndTime = Environment.TickCount + ((Int32)Info.Time * 1000);
                                        Targets1[i].AddFlag(Player.Flag.Invisibility);

                                        if (Targets1[i].IsPlayer())
                                            World.BroadcastRoomMsg((Targets1[i] as Player), MsgUserAttrib.Create(Targets1[i], Targets1[i].Flags, MsgUserAttrib.Type.Flags), true);
                                        else
                                            World.BroadcastRoomMsg(Targets1[i], MsgUserAttrib.Create(Targets1[i], Targets1[i].Flags, MsgUserAttrib.Type.Flags));
                                        break;
                                    }
                                case 1085:
                                    {
                                        if (Targets1[i].StarOfAccuracyEndTime != 0)
                                        {
                                            Targets1[i].StarOfAccuracyEndTime = Environment.TickCount + ((Int32)Info.Time * 1000);
                                            break;
                                        }

                                        Targets1[i].StarOfAccuracyEndTime = Environment.TickCount + ((Int32)Info.Time * 1000);
                                        Targets1[i].AccuracyEndTime = 0;
                                        Targets1[i].DexterityBonus = ((Double)(Damage - 30000) / 100.00);
                                        Targets1[i].AddFlag(Player.Flag.Accuracy);
                                        if (Targets1[i].IsPlayer())
                                            MyMath.GetEquipStats((Targets1[i] as Player));

                                        if (Targets1[i].IsPlayer())
                                            World.BroadcastRoomMsg((Targets1[i] as Player), MsgUserAttrib.Create(Targets1[i], Targets1[i].Flags, MsgUserAttrib.Type.Flags), true);
                                        else
                                            World.BroadcastRoomMsg(Targets1[i], MsgUserAttrib.Create(Targets1[i], Targets1[i].Flags, MsgUserAttrib.Type.Flags));
                                        break;
                                    }
                                case 1090:
                                    {
                                        if (Targets1[i].MagicShieldEndTime != 0)
                                        {
                                            Targets1[i].MagicShieldEndTime = Environment.TickCount + ((Int32)Info.Time * 1000);
                                            break;
                                        }

                                        Targets1[i].MagicShieldEndTime = Environment.TickCount + ((Int32)Info.Time * 1000);
                                        Targets1[i].ShieldEndTime = 0;
                                        Targets1[i].DefenceBonus = ((Double)(Damage - 30000) / 100.00);
                                        Targets1[i].AddFlag(Player.Flag.Shield);
                                        if (Targets1[i].IsPlayer())
                                            MyMath.GetEquipStats((Targets1[i] as Player));

                                        if (Targets1[i].IsPlayer())
                                            World.BroadcastRoomMsg((Targets1[i] as Player), MsgUserAttrib.Create(Targets1[i], Targets1[i].Flags, MsgUserAttrib.Type.Flags), true);
                                        else
                                            World.BroadcastRoomMsg(Targets1[i], MsgUserAttrib.Create(Targets1[i], Targets1[i].Flags, MsgUserAttrib.Type.Flags));
                                        break;
                                    }
                                case 1095:
                                    {
                                        if (Targets1[i].StigmaEndTime != 0)
                                        {
                                            Targets1[i].StigmaEndTime = Environment.TickCount + ((Int32)Info.Time * 1000);
                                            break;
                                        }

                                        Targets1[i].StigmaEndTime = Environment.TickCount + ((Int32)Info.Time * 1000);
                                        Targets1[i].SupermanEndTime = 0;
                                        Targets1[i].AttackBonus = ((Double)(Damage - 30000) / 100.00);
                                        Targets1[i].AddFlag(Player.Flag.Stigma);
                                        Targets1[i].DelFlag(Player.Flag.SuperMan);
                                        if (Targets1[i].IsPlayer())
                                            MyMath.GetEquipStats((Targets1[i] as Player));

                                        if (Targets1[i].IsPlayer())
                                            World.BroadcastRoomMsg((Targets1[i] as Player), MsgUserAttrib.Create(Targets1[i], Targets1[i].Flags, MsgUserAttrib.Type.Flags), true);
                                        else
                                            World.BroadcastRoomMsg(Targets1[i], MsgUserAttrib.Create(Targets1[i], Targets1[i].Flags, MsgUserAttrib.Type.Flags));
                                        break;
                                    }
                                case 1110:
                                    {
                                        if (Targets1[i].CycloneEndTime != 0)
                                        {
                                            Targets1[i].CycloneEndTime = Environment.TickCount + ((Int32)Info.Time * 1000);
                                            break;
                                        }

                                        Targets1[i].CycloneEndTime = Environment.TickCount + ((Int32)Info.Time * 1000);
                                        Targets1[i].SpeedBonus = ((Double)(Damage - 30000) / 100.00);
                                        Targets1[i].AddFlag(Player.Flag.Cyclone);
                                        if (Targets1[i].IsPlayer())
                                            MyMath.GetEquipStats((Targets1[i] as Player));

                                        if (Targets1[i].IsPlayer())
                                            World.BroadcastRoomMsg((Targets1[i] as Player), MsgUserAttrib.Create(Targets1[i], Targets1[i].Flags, MsgUserAttrib.Type.Flags), true);
                                        else
                                            World.BroadcastRoomMsg(Targets1[i], MsgUserAttrib.Create(Targets1[i], Targets1[i].Flags, MsgUserAttrib.Type.Flags));
                                        break;
                                    }
                                case 10203:
                                    {
                                        if (Targets1[i].MagicShieldEndTime != 0)
                                        {
                                            Targets1[i].MagicShieldEndTime = Environment.TickCount + ((Int32)Info.Time * 1000);
                                            break;
                                        }

                                        Targets1[i].MagicShieldEndTime = Environment.TickCount + ((Int32)Info.Time * 1000);
                                        Targets1[i].ShieldEndTime = 0;
                                        Targets1[i].DefenceBonus = ((Double)(Damage - 30000) / 100.00);
                                        Targets1[i].AddFlag(Player.Flag.Shield);
                                        if (Targets1[i].IsPlayer())
                                            MyMath.GetEquipStats((Targets1[i] as Player));

                                        if (Targets1[i].IsPlayer())
                                            World.BroadcastRoomMsg((Targets1[i] as Player), MsgUserAttrib.Create(Targets1[i], Targets1[i].Flags, MsgUserAttrib.Type.Flags), true);
                                        else
                                            World.BroadcastRoomMsg(Targets1[i], MsgUserAttrib.Create(Targets1[i], Targets1[i].Flags, MsgUserAttrib.Type.Flags));
                                        break;
                                    }
                            }
                            Damage = 0;
                        }
                        else if (Info.ActionSort == 7)
                        {
                            if (Targets1[i].IsPlayer())
                            {
                                if (!Targets1[i].IsAlive())
                                    (Targets1[i] as Player).Reborn(false);
                            }
                        }
                        else if (Info.ActionSort == 11)
                        {
                            if (Targets1[i].IsPlayer())
                            {
                                if ((Targets1[i] as Player).XP + Damage >= 100)
                                    (Targets1[i] as Player).XP = 100;
                                else
                                    (Targets1[i] as Player).XP += (Byte)Damage;
                                (Targets1[i] as Player).Send(MsgUserAttrib.Create(Targets1[i], (Targets1[i] as Player).XP, MsgUserAttrib.Type.XP));
                            }
                        }
                        else if (Info.ActionSort == 19)
                        {
                            if (Targets1[i].IsPlayer())
                            {
                                MonsterInfo Monster;
                                if (Database.AllMonsters.TryGetValue(Damage, out Monster))
                                {
                                    (Targets1[i] as Player).TransformEndTime = Environment.TickCount + ((Int32)Info.Time * 1000);
                                    (Targets1[i] as Player).AddTransform(Monster.Look);
                                    Targets1[i].MinAtk = Monster.MinAtk;
                                    Targets1[i].MaxAtk = Monster.MaxAtk;
                                    Targets1[i].Defence = Monster.Defence;
                                    Targets1[i].MagicAtk = 0;
                                    Targets1[i].MagicDef = Monster.MagicDef;
                                    Targets1[i].MagicBlock = 0;
                                    Targets1[i].Dodge = Monster.Dodge;
                                    Targets1[i].Weight = 0;
                                    Targets1[i].Dexterity = 1000;
                                    Targets1[i].AtkRange = Monster.AtkRange;
                                    Targets1[i].AtkSpeed = Monster.AtkSpeed;
                                    Targets1[i].AtkType = 2;
                                    Targets1[i].Bless = 1.00;
                                    Targets1[i].GemBonus = 1.00;
                                    Targets1[i].CurHP = (Int32)((Double)Monster.Life * ((Double)Targets1[i].CurHP / (Double)Targets1[i].MaxHP));
                                    Targets1[i].MaxHP = Monster.Life;

                                    (Targets1[i] as Player).Send(MsgUserAttrib.Create(Targets1[i], Targets1[i].CurHP, MsgUserAttrib.Type.Life));
                                    (Targets1[i] as Player).Send(MsgUserAttrib.Create(Targets1[i], Targets1[i].MaxHP, (MsgUserAttrib.Type.Life + 1)));
                                    if ((Targets1[i] as Player).Team != null)
                                    {
                                        World.BroadcastTeamMsg((Targets1[i] as Player).Team, MsgUserAttrib.Create((Targets1[i] as Player), (Targets1[i] as Player).CurHP, MsgUserAttrib.Type.Life));
                                        World.BroadcastTeamMsg((Targets1[i] as Player).Team, MsgUserAttrib.Create((Targets1[i] as Player), (Targets1[i] as Player).MaxHP, (MsgUserAttrib.Type.Life + 1)));
                                    }
                                    World.BroadcastRoomMsg((Targets1[i] as Player), MsgUserAttrib.Create(Targets1[i], Targets1[i].Look, MsgUserAttrib.Type.Look), true);
                                }
                            }
                            Damage = 0;
                        }
                        else if (Info.ActionSort == 20)
                        {
                            if (Targets1[i].IsPlayer())
                            {
                                if ((Targets1[i] as Player).CurMP + Damage >= (Targets1[i] as Player).MaxMP)
                                    (Targets1[i] as Player).CurMP = (Targets1[i] as Player).MaxMP;
                                else
                                    (Targets1[i] as Player).CurMP += (UInt16)Damage;
                                (Targets1[i] as Player).Send(MsgUserAttrib.Create(Targets1[i], (Targets1[i] as Player).CurMP, MsgUserAttrib.Type.Mana));
                            }
                        }
                        else
                        {
                            if (Targets1[i].IsPlayer())
                                (Targets1[i] as Player).RemoveDefDura();
                            if (!Targets1[i].IsPlayer() || (Targets1[i].IsPlayer() && !(Targets1[i] as Player).Reflect()))
                            {
                                if (Damage >= Targets1[i].CurHP)
                                {
                                    if (Targets1[i].IsPlayer())
                                        (Targets1[i] as Player).Die();
                                    else if (Targets1[i].IsMonster())
                                        (Targets1[i] as Monster).Die(Attacker.UniqId);
                                    else if (Targets1[i].IsTerrainNPC())
                                    {
                                        World.BroadcastMapMsg(Attacker.Map, MsgInteract.Create(Attacker, Targets1[i], 1, MsgInteract.Action.Kill));
                                        (Targets1[i] as TerrainNPC).Die();
                                    }
                                    if (!Targets1[i].IsTerrainNPC())
                                        World.BroadcastMapMsg(Attacker.Map, MsgInteract.Create(Attacker, Targets1[i], 1, MsgInteract.Action.Kill));
                                }
                                else
                                {
                                    Targets1[i].CurHP -= Damage;
                                    if (Targets1[i].IsPlayer())
                                    {
                                        (Targets1[i] as Player).Send(MsgUserAttrib.Create(Targets1[i], Targets1[i].CurHP, MsgUserAttrib.Type.Life));
                                        if ((Targets1[i] as Player).Team != null)
                                            World.BroadcastTeamMsg((Targets1[i] as Player).Team, MsgUserAttrib.Create(Targets1[i], Targets1[i].CurHP, MsgUserAttrib.Type.Life));
                                    }
                                }
                            }
                            else
                            {
                                if (Damage > 2000)
                                    Damage = 2000;

                                World.BroadcastMapMsg(Attacker.Map, MsgInteract.Create(Targets1[i], Attacker, Damage, MsgInteract.Action.ReflectMagic));
                                if (Damage >= Attacker.CurHP)
                                {
                                    Attacker.Die(0);
                                    World.BroadcastMapMsg((Targets1[i] as Player), MsgInteract.Create(Targets1[i], Attacker, 1, MsgInteract.Action.Kill), true);
                                }
                                else
                                {
                                    Attacker.CurHP -= Damage;
                                    World.BroadcastMapMsg(Attacker.Map, MsgUserAttrib.Create(Attacker, Attacker.CurHP, MsgUserAttrib.Type.Life));
                                }
                                Reflected = true;
                            }
                        }

                        if (Target != null && Info.ActionSort != 11 && Info.ActionSort != 2 && !Reflected)
                            World.BroadcastMapMsg(Attacker.Map, MsgMagicEffect.Create(Attacker, Target, Damage, X, Y));
                        else if (!Reflected)
                            Targets.Add(Targets1[i], Damage);
                    }

                    if (Target == null || Info.ActionSort == 11 || Info.ActionSort == 2)
                        World.BroadcastMapMsg(Attacker.Map, MsgMagicEffect.Create(Attacker, Targets, X, Y));
                }
                catch (Exception Exc) { Program.WriteLine(Exc); }
            }
            #endregion
        }
    }
}