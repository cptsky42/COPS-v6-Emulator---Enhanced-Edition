// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using COServer.Entities;
using CO2_CORE_DLL.IO;

namespace COServer
{
    public partial class MyMath
    {
        public static void GetEquipStats(Player Player)
        {
            if (Player.TransformEndTime != 0)
                return;

            Int32 MinAtk = Player.Strength;
            Int32 MaxAtk = Player.Strength;
            Int32 Defence = 0;
            Int32 MagicAtk = 0;
            Int32 MagicDef = 0;
            Int32 MagicBlock = 0;
            Int32 Dexterity = Player.Agility + 25;
            Int32 Dodge = 0;
            Int32 Weight = (Int32)(Player.Look % 10);
            Int32 AtkRange = 0;
            Int32 AtkSpeed = 0;
            Double Bless = 1.00;
            Double GemBonus = 1.00;

            Double AtkBonus = 1.00;
            Double MAtkBonus = 1.00;
            Double DextBonus = 1.00;
            Double ExpBonus = 1.00;
            Double MagicExpBonus = 1.00;
            Double WeaponSkillExpBonus = 1.00;

            lock (Player.Items)
            {
                foreach (Item Item in Player.Items.Values)
                {
                    if (Item.Position > 0 && Item.Position < 10)
                    {
                        if (Item.MaxDura != 0 && Item.CurDura == 0)
                            continue;

                        Bless -= (Double)Item.Bless / 100;

                        ItemType.Entry Info;
                        if (!Database2.AllItems.TryGetValue(Item.Id, out Info))
                            continue;

                        if (Item.Position != 5)
                        {
                            MinAtk += Info.MinAttack;
                            MaxAtk += Info.MaxAttack;

                            Dodge += Info.Dodge;
                            Weight += Info.Weight;
                        }
                        else
                        {
                            MinAtk += Info.MinAttack / 2;
                            MaxAtk += Info.MaxAttack / 2;

                            Dodge += Info.Dodge / 2;
                            Weight += Info.Weight / 2;
                        }

                        Defence += Info.Defense;
                        MagicAtk += Info.MagicAttack;
                        MagicDef += Info.MagicDefence;
                        Dexterity += Info.Dexterity;
                        AtkRange += Info.Range;
                        AtkSpeed += Info.AttackSpeed;

                        ItemBonus Bonus;
                        if (Database.AllBonus.TryGetValue(ItemHandler.GetBonusId(Item.Id, Item.Craft), out Bonus))
                        {
                            MinAtk += Bonus.MinAtk;
                            MaxAtk += Bonus.MaxAtk;
                            Defence += Bonus.Defence;
                            MagicAtk += Bonus.MAtk;
                            MagicBlock += Bonus.MDef;
                            Dexterity += Bonus.Dexterity;

                            if (Item.Position != 5)
                                Dodge += Bonus.Dodge;
                            else
                                Dodge += Bonus.Dodge / 2;
                        }

                        if (Item.Position == 4 || Item.Position == 5)
                        {
                            Int16 SubType = (Int16)(Item.Id / 1000);
                            if (Player.WeaponSkills.ContainsKey(SubType))
                            {
                                WeaponSkill WeaponSkill = Player.WeaponSkills[SubType];
                                if (WeaponSkill.Level > 12 && WeaponSkill.Level <= 20)
                                    AtkBonus += ((20.00 - (Double)WeaponSkill.Level) / 100.00);
                            }
                        }

                        switch (Item.Gem1)
                        {
                            case 1:
                                MAtkBonus += 0.05;
                                break;
                            case 2:
                                MAtkBonus += 0.10;
                                break;
                            case 3:
                                MAtkBonus += 0.15;
                                break;
                            case 11:
                                AtkBonus += 0.05;
                                break;
                            case 12:
                                AtkBonus += 0.10;
                                break;
                            case 13:
                                AtkBonus += 0.15;
                                break;
                            case 21:
                                DextBonus += 0.05;
                                break;
                            case 22:
                                DextBonus += 0.10;
                                break;
                            case 23:
                                DextBonus += 0.15;
                                break;
                            case 31:
                                ExpBonus += 0.10;
                                break;
                            case 32:
                                ExpBonus += 0.15;
                                break;
                            case 33:
                                ExpBonus += 0.25;
                                break;
                            case 51:
                                WeaponSkillExpBonus += 0.30;
                                break;
                            case 52:
                                WeaponSkillExpBonus += 0.50;
                                break;
                            case 53:
                                WeaponSkillExpBonus += 1.00;
                                break;
                            case 61:
                                MagicExpBonus += 0.30;
                                break;
                            case 62:
                                MagicExpBonus += 0.50;
                                break;
                            case 63:
                                MagicExpBonus += 1.00;
                                break;
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

                        switch (Item.Gem2)
                        {
                            case 1:
                                MAtkBonus += 0.05;
                                break;
                            case 2:
                                MAtkBonus += 0.10;
                                break;
                            case 3:
                                MAtkBonus += 0.15;
                                break;
                            case 11:
                                AtkBonus += 0.05;
                                break;
                            case 12:
                                AtkBonus += 0.10;
                                break;
                            case 13:
                                AtkBonus += 0.15;
                                break;
                            case 21:
                                DextBonus += 0.05;
                                break;
                            case 22:
                                DextBonus += 0.10;
                                break;
                            case 23:
                                DextBonus += 0.15;
                                break;
                            case 31:
                                ExpBonus += 0.10;
                                break;
                            case 32:
                                ExpBonus += 0.15;
                                break;
                            case 33:
                                ExpBonus += 0.25;
                                break;
                            case 51:
                                WeaponSkillExpBonus += 0.30;
                                break;
                            case 52:
                                WeaponSkillExpBonus += 0.50;
                                break;
                            case 53:
                                WeaponSkillExpBonus += 1.00;
                                break;
                            case 61:
                                MagicExpBonus += 0.30;
                                break;
                            case 62:
                                MagicExpBonus += 0.50;
                                break;
                            case 63:
                                MagicExpBonus += 1.00;
                                break;
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

            MinAtk = (Int32)Math.Round(MinAtk * AtkBonus, 0);
            MaxAtk = (Int32)Math.Round(MaxAtk * AtkBonus, 0);
            MagicAtk = (Int32)Math.Round(MagicAtk * MAtkBonus, 0);
            Dexterity = (Int32)Math.Round(Dexterity * DextBonus, 0);

            Player.MinAtk = MinAtk;
            Player.MaxAtk = MaxAtk;
            Player.Defence = Defence + Player.DefenceAddBonus;
            Player.MagicAtk = MagicAtk;
            Player.MagicDef = MagicDef;
            Player.MagicBlock = MagicBlock;
            Player.Dexterity = Dexterity;
            Player.Dodge = Dodge;
            Player.Weight = Weight;
            Player.AtkRange = AtkRange;
            Player.Bless = Bless;
            Player.GemBonus = GemBonus;

            Player.ExpBonus = ExpBonus;
            Player.MagicBonus = MagicExpBonus;
            Player.WeaponSkillBonus = WeaponSkillExpBonus;

            Player.AtkSpeed = AtkSpeed;
            switch (Player.Profession)
            {
                case 41:
                    Player.AtkSpeed = (Int32)(Player.AtkSpeed * 0.85);
                    break;
                case 42:
                    Player.AtkSpeed = (Int32)(Player.AtkSpeed * 0.75);
                    break;
                case 43:
                    Player.AtkSpeed = (Int32)(Player.AtkSpeed * 0.65);
                    break;
                case 44:
                    Player.AtkSpeed = (Int32)(Player.AtkSpeed * 0.55);
                    break;
                case 45:
                    Player.AtkSpeed = (Int32)(Player.AtkSpeed * 0.45);
                    break;
            }
            Player.AtkSpeed -= 375;
            if (Player.AtkSpeed < 500)
                Player.AtkSpeed = 500;

            if (Player.ContainsFlag(Player.Flag.Stigma) || Player.ContainsFlag(Player.Flag.SuperMan))
            {
                Player.MinAtk = (Int32)((Double)Player.MinAtk * Player.AttackBonus);
                Player.MaxAtk = (Int32)((Double)Player.MaxAtk * Player.AttackBonus);
            }

            if (Player.ContainsFlag(Player.Flag.Shield))
                Player.Defence = (Int32)((Double)Player.Defence * Player.DefenceBonus);

            if (Player.ContainsFlag(Player.Flag.Accuracy))
                Player.Dexterity = (Int32)((Double)Player.Dexterity * Player.DexterityBonus);

            if (Player.ContainsFlag(Player.Flag.Cyclone))
                Player.AtkSpeed -= (Int32)((Double)Player.AtkSpeed * Player.SpeedBonus);

            Item Arrow = Player.GetItemByPos(5);
            if (Arrow != null)
            {
                if (Arrow.Id / 10 == 105010) //Bodkin
                {
                    Player.MinAtk = (Int32)((Double)Player.MinAtk * 0.85);
                    Player.MaxAtk = (Int32)((Double)Player.MaxAtk * 0.85);
                }
                else if (Arrow.Id / 10 == 105020) //Blunt
                {
                    Player.AtkSpeed = 500;
                    Player.MinAtk = 0;
                    Player.MaxAtk = 0;
                }
                else if (Arrow.Id / 10 == 105040) //Broadhead
                {
                    Player.MinAtk = (Int32)((Double)Player.MinAtk * 1.15);
                    Player.MaxAtk = (Int32)((Double)Player.MaxAtk * 1.15);
                }
            }
        }
    }
}
