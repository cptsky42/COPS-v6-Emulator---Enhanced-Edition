// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * COPS v6 Emulator

using System;
using System.Linq;
using COServer.Entities;

namespace COServer
{
    public partial class MyMath
    {
        public static void GetEquipStats(Player Player)
        {
            if (Player.TransformEndTime != 0)
                return;

            var skills = Player.WeaponSkills.ToDictionary(skill => skill.Type, skill => skill);

            Int32 MinAtk = Player.Strength;
            Int32 MaxAtk = Player.Strength;
            Int32 Defence = 0;
            Int32 MagicAtk = 0;
            Int32 MagicDef = 0;
            Int32 MagicBlock = 0;
            Int32 Dexterity = Player.Agility + 25;
            Int32 Dodge = 0;
            Int32 AtkRange = 0;
            Int32 AtkSpeed = 0;
            Double Bless = 1.00;
            Double GemBonus = 1.00;

            Double AtkBonus = 1.00;
            Double DextBonus = 1.00;
            Double ExpBonus = 1.00;
            Double MagicExpBonus = 1.00;
            Double WeaponSkillExpBonus = 1.00;

            lock (Player.Items)
            {
                foreach (Item item in Player.Items.Values)
                {
                    if (item.Position > 0 && item.Position < 10)
                    {
                        if (item.MaxDura != 0 && item.CurDura == 0)
                            continue;

                        Bless -= (Double)item.Bless / 100;

                        if (item.Position != 5)
                        {
                            MinAtk += item.MinAtk;
                            MaxAtk += item.MaxAtk;
                        }
                        else
                        {
                            MinAtk += item.MinAtk / 2;
                            MaxAtk += item.MaxAtk / 2;
                        }

                        Defence += item.Defense;
                        MagicAtk += item.MagicAtk;
                        MagicDef += item.MagicDef;
                        Dexterity += item.Dexterity;
                        AtkRange += item.Range;
                        AtkSpeed += item.AtkSpeed;

                        ItemAddition bonus;
                        if (Database.AllBonus.TryGetValue(ItemHandler.GetBonusId(item.Type, item.Craft), out bonus))
                        {
                            MinAtk += bonus.MinAtk;
                            MaxAtk += bonus.MaxAtk;
                            Defence += bonus.Defence;
                            MagicAtk += bonus.MAtk;
                            MagicBlock += bonus.MDef;
                            Dexterity += bonus.Dexterity;

                            if (item.Position != 5)
                                Dodge += bonus.Dodge;
                            else
                                Dodge += bonus.Dodge / 2;
                        }

                        if (item.Position == 4 || item.Position == 5)
                        {
                            UInt16 skill_type = (UInt16)(item.Type / 1000);
                            if (skills.ContainsKey(skill_type))
                            {
                                WeaponSkill skill = skills[skill_type];
                                if (skill.Level > 12 && skill.Level <= 20)
                                    AtkBonus += ((20.00 - (Double)skill.Level) / 100.00);
                            }
                        }

                        DextBonus += item.GetGemHitRateEffect();
                        ExpBonus += item.GetGemExpEffect();
                        WeaponSkillExpBonus += item.GetGemWpnExpEffect();
                        MagicExpBonus += item.GetGemMgcExpEffect();

                        switch (item.FirstGem)
                        {
                            case 71:
                                GemBonus -= 0.02;
                                break;
                            case 72:
                                GemBonus -= 0.04;
                                break;
                            case 73:
                                GemBonus -= 0.06;
                                break;
                        }

                        switch (item.SecondGem)
                        {
                            case 71:
                                GemBonus -= 0.02;
                                break;
                            case 72:
                                GemBonus -= 0.04;
                                break;
                            case 73:
                                GemBonus -= 0.06;
                                break;
                        }
                    }
                }
            }

            MinAtk = (Int32)Math.Round(MinAtk * Player.GetGemAtkEffect(), 0);
            MaxAtk = (Int32)Math.Round(MaxAtk * Player.GetGemAtkEffect(), 0);
            MagicAtk = (Int32)Math.Round(MagicAtk * Player.GetGemMAtkEffect(), 0);
            Dexterity = (Int32)Math.Round(Dexterity * DextBonus, 0);

            Player.MinAtk = MinAtk;
            Player.MaxAtk = MaxAtk;
            Player.Defence = Defence;
            Player.MagicAtk = MagicAtk;
            Player.MagicDef = MagicDef;
            Player.MagicBlock = MagicBlock;
            Player.Dexterity = Dexterity;
            Player.Dodge = Dodge;
            Player.AtkRange = AtkRange;
            Player.Bless = Bless;
            Player.GemBonus = GemBonus;

            Player.ExpBonus = ExpBonus;
            Player.MagicBonus = MagicExpBonus;
            Player.WeaponSkillBonus = WeaponSkillExpBonus;

            Player.AtkSpeed = AtkSpeed;

            StatusEffect effect;
            if (Player.GetStatus(Status.MagicAttack, out effect))
            {
                Player.MinAtk = (Int32)((double)Player.MinAtk * (double)effect.Data);
                Player.MaxAtk = (Int32)((double)Player.MaxAtk * (double)effect.Data);
            }

            if (Player.GetStatus(Status.SuperAtk, out effect))
            {
                Player.MinAtk = (Int32)((double)Player.MinAtk * (double)effect.Data);
                Player.MaxAtk = (Int32)((double)Player.MaxAtk * (double)effect.Data);
            }

            if (Player.GetStatus(Status.MagicDefense, out effect))
                Player.Defence = (Int32)((double)Player.Defence * (double)effect.Data);

            if (Player.GetStatus(Status.Accuracy, out effect))
                Player.Dexterity = (Int32)((double)Player.Dexterity * (double)effect.Data);

            if (Player.GetStatus(Status.SuperSpeed, out effect))
                Player.AtkSpeed += (Int32)((double)Player.AtkSpeed * (double)effect.Data);
        }
    }
}
