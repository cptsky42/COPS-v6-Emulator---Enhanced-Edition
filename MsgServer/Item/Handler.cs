// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2011 - 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;

namespace COServer
{
    public partial class ItemHandler
    {
        public static Int32 GetBonusId(Int32 aItemId, Byte aLevel)
        {
            Int32 bonusId = 0;

            if (aItemId > 110000 && aItemId < 120000) //Head
                bonusId = (aItemId - (aItemId % 10) - ((aItemId % 1000) - (aItemId % 100)));
            else if (aItemId > 130000 && aItemId < 140000) //Armor
                bonusId = (aItemId - (aItemId % 10) - ((aItemId % 1000) - (aItemId % 100)));
            else if (aItemId > 400000 && aItemId < 500000 && !(aItemId > 421000 && aItemId < 422000)) //Weapon (1H)
                bonusId = (444000 + ((aItemId % 1000) - (aItemId % 10)));
            else if (aItemId > 500000 && aItemId < 600000 && !(aItemId > 500000 && aItemId < 501000)) //Weapon (2H)
                bonusId = (555000 + ((aItemId % 1000) - (aItemId % 10)));
            else if (aItemId > 900000 && aItemId < 1000000) //Shield
                bonusId = (aItemId - (aItemId % 10) - ((aItemId % 1000) - (aItemId % 100)));
            else
                bonusId = (aItemId - (aItemId % 10));


            bonusId *= 100;
            bonusId += aLevel;

            return bonusId;
        }

        public static UInt16 GetMaxDura(Int32 Id)
        {
            Item.Info Info;
            if (!Database.AllItems.TryGetValue(Id, out Info))
                return 1;

            return Info.AmountLimit;
        }

        public static Boolean GetUpQualityInfo(Item aItem, out Double aChance, out Int32 aNextId)
        {
            aNextId = 0;
            aChance = 0;

            if (aItem.Type % 10 == 9)
                return false;

            if (aItem.Type / 1000 == 137)
                return false;

            if (aItem.Type == 150000 || aItem.Type == 150310 || aItem.Type == 150320 || aItem.Type == 410301 || aItem.Type == 421301 || aItem.Type == 500301)
                return false;

            aNextId = aItem.Type;
            ++aNextId;

            Item.Info info;
            if (!Database.AllItems.TryGetValue(aNextId, out info))
                return false;

            aChance = 100.00;
            switch (aItem.Type % 10)
            {
                case 6: aChance = 50.00; break;
                case 7: aChance = 33.00; break;
                case 8: aChance = 20.00; break;
                default: aChance = 100.00; break;
            }

            Double factor = info.RequiredLevel;
            if (factor > 70)
                aChance = aChance * (100.00 - (factor - 70.00)) / 100.00;
            return true;
        }

        public static Boolean GetUpLevelInfo(Item aItem, out Double aChance, out Int32 aNextId)
        {
            aNextId = 0;
            aChance = 0;

            if (aItem.GetNextType() == aItem.Type)
                return false;

            aNextId = aItem.GetNextType();

            Item.Info info;
            if (!Database.AllItems.TryGetValue(aNextId, out info))
                return false;

            if (info.RequiredLevel >= 120)
                return false;

            Byte type = (Byte)(aItem.Type / 10000);

            aChance = 100.00;
            if (type == 11 || type == 13 || type == 90) // Head || Armor || Shield
            {
                switch ((aItem.Type % 100) / 10)
                {
                    case 5: aChance = 50.00; break;
                    case 6: aChance = 40.00; break;
                    case 7: aChance = 30.00; break;
                    case 8: aChance = 20.00; break;
                    case 9: aChance = 15.00; break;
                    default: aChance = 500.00; break;
                }

                switch (aItem.Type % 10)
                {
                    case 6: aChance = aChance * 0.90; break;
                    case 7: aChance = aChance * 0.70; break;
                    case 8: aChance = aChance * 0.30; break;
                    case 9: aChance = aChance * 0.10; break;
                }
            }
            else
            {
                switch ((aItem.Type % 1000) / 10)
                {
                    case 11: aChance = 95.00; break;
                    case 12: aChance = 90.00; break;
                    case 13: aChance = 85.00; break;
                    case 14: aChance = 80.00; break;
                    case 15: aChance = 75.00; break;
                    case 16: aChance = 70.00; break;
                    case 17: aChance = 65.00; break;
                    case 18: aChance = 60.00; break;
                    case 19: aChance = 55.00; break;
                    case 20: aChance = 50.00; break;
                    case 21: aChance = 45.00; break;
                    case 22: aChance = 40.00; break;
                    default: aChance = 500.00; break;
                }

                switch (aItem.Type % 10)
                {
                    case 6: aChance = aChance * 0.90; break;
                    case 7: aChance = aChance * 0.70; break;
                    case 8: aChance = aChance * 0.30; break;
                    case 9: aChance = aChance * 0.10; break;
                }
            }
            return true;
        }
    }
}
