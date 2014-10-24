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
        public static UInt16 GetHitPoints(Player Player, Boolean Set)
        {
            if (Player.TransformEndTime != 0)
                return (UInt16)Player.MaxHP;

            UInt16 HitPoints = 0;

            HitPoints += (UInt16)(Player.Strength * 3);
            HitPoints += (UInt16)(Player.Agility * 3);
            HitPoints += (UInt16)(Player.Vitality * 24);
            HitPoints += (UInt16)(Player.Spirit * 3);

            switch (Player.Profession)
            {
                case 11:
                    HitPoints = (UInt16)(HitPoints * 1.05);
                    break;
                case 12:
                    HitPoints = (UInt16)(HitPoints * 1.08);
                    break;
                case 13:
                    HitPoints = (UInt16)(HitPoints * 1.10);
                    break;
                case 14:
                    HitPoints = (UInt16)(HitPoints * 1.12);
                    break;
                case 15:
                    HitPoints = (UInt16)(HitPoints * 1.15);
                    break;
            }

            lock (Player.Items)
            {
                foreach (Item Item in Player.Items.Values)
                {
                    if (Item.Position > 0 && Item.Position < 10)
                    {
                        HitPoints += Item.Enchant;

                        ItemType.Entry Info;
                        if (!Database2.AllItems.TryGetValue(Item.Id, out Info))
                            continue;

                        HitPoints += (UInt16)Info.Life;

                        ItemBonus Bonus;
                        if (Database.AllBonus.TryGetValue(ItemHandler.GetBonusId(Item.Id, Item.Craft), out Bonus))
                            HitPoints += (UInt16)Bonus.Life;
                    }
                }
            }

            if (Set)
                Player.MaxHP = HitPoints;

            return HitPoints;
        }

        public static UInt16 GetHitPoints(UInt16 Strength, UInt16 Agility, UInt16 Vitality, UInt16 Spirit, Byte Profession)
        {
            UInt16 HitPoints = 0;

            HitPoints += (UInt16)(Strength * 3);
            HitPoints += (UInt16)(Agility * 3);
            HitPoints += (UInt16)(Vitality * 24);
            HitPoints += (UInt16)(Spirit * 3);

            switch (Profession)
            {
                case 11:
                    HitPoints = (UInt16)(HitPoints * 1.05);
                    break;
                case 12:
                    HitPoints = (UInt16)(HitPoints * 1.08);
                    break;
                case 13:
                    HitPoints = (UInt16)(HitPoints * 1.10);
                    break;
                case 14:
                    HitPoints = (UInt16)(HitPoints * 1.12);
                    break;
                case 15:
                    HitPoints = (UInt16)(HitPoints * 1.15);
                    break;
            }

            return HitPoints;
        }
    }
}
