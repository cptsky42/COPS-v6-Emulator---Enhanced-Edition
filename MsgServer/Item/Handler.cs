// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.Collections.Generic;
using COServer.Network;
using COServer.Entities;
using CO2_CORE_DLL;
using CO2_CORE_DLL.IO;

namespace COServer
{
    public partial class ItemHandler
    {
        public static Int32 RemoveQuality(Int32 Id) { return (Id - (Id % 10)) + 3; }
        public static Int32 ChangeQuality(Int32 Id, Byte Quality) { return (Id - (Id % 10)) + Quality; }

        public static Int32 GenerateId(Byte Level)
        {
            if (Level > 125)
                Level = 125;

            if (MyMath.Success(75))
                Level = (Byte)((Double)Level * ((Double)MyMath.Generate(85, 100) / 100.00));

            Int32 Id = 0;

            Int16 ItemType = 0;
            SByte Color = (SByte)MyMath.Generate(3, 9);
            SByte Lvl = 0;
            SByte Quality = (SByte)MyMath.Generate(3, 5);

            SByte Type = (SByte)MyMath.Generate(0, 8);
            switch (Type)
            {
                case 0: //Head
                    {
                        ItemType = (Byte)MyMath.Generate(111, 118);
                        Lvl = (SByte)((Level / 10) - 1);
                        if (Lvl > 9)
                            Lvl = 9;
                        break;
                    }
                case 1: //Necklace
                    {
                        ItemType = (Byte)MyMath.Generate(120, 121);
                        Lvl = (SByte)(((Double)Level / 10.00) * 2.00);
                        break;
                    }
                case 2: //Armor
                    {
                        ItemType = (Byte)MyMath.Generate(130, 139);
                        Lvl = (SByte)((Level / 10) - 1);
                        if (Lvl > 9)
                            Lvl = 9;
                        break;
                    }
                case 3: //Boots
                    {
                        ItemType = 160;
                        Lvl = (SByte)(((Level / 10) * 2) - 1);
                        break;
                    }
                case 4: //Ring
                    {
                        ItemType = (Byte)MyMath.Generate(150, 152);
                        Lvl = (SByte)(((Level / 10) * 2) - 1);
                        break;
                    }
                case 5: //Shield
                    {
                        ItemType = 900;
                        Lvl = (SByte)((Level - 40) / 10);
                        break;
                    }
                case 6: //Weapon
                    {
                        ItemType = 400;
                        Lvl = (SByte)(Level / 5);
                        break;
                    }
                case 7: //Weapon
                    {
                        ItemType = 400;
                        Lvl = (SByte)(Level / 5);
                        break;
                    }
                case 8: //Weapon
                    {
                        ItemType = 400;
                        Lvl = (SByte)(Level / 5);
                        break;
                    }
            }

            //Weapon
            if (ItemType == 400)
            {
                SByte NewType = (SByte)MyMath.Generate(1, 12);
                if (NewType == 1)
                    ItemType = 410;
                else if (NewType == 2)
                    ItemType = 420;
                else if (NewType == 3)
                    ItemType = 421;
                else if (NewType == 4)
                    ItemType = 450;
                else if (NewType == 5)
                    ItemType = 460;
                else if (NewType == 6)
                    ItemType = 480;
                else if (NewType == 7)
                    ItemType = 490;
                else if (NewType == 8)
                    ItemType = 500;
                else if (NewType == 9)
                    ItemType = 530;
                else if (NewType == 10)
                    ItemType = 560;
                else if (NewType == 11)
                    ItemType = 561;
                else if (NewType == 12)
                    ItemType = 580;
            }

            Id = ((ItemType * 1000) + (Lvl * 10) + Quality);
            if ((ItemType / 10) == 11 || (ItemType / 10) == 13 || (ItemType / 10) == 90)
                Id += (Color * 100);

            //Invalid Level
            if (Lvl < 0)
                return 0;

            //Invalid ItemType...
            if (ItemType == 137)
                return 0;

            if (Database2.AllItems.ContainsKey(Id))
                return Id;

            return 0;
        }

        public static Boolean GenerateLoto(Player Player)
        {
            if (Player.ItemInInventory() >= 40)
                return false;

            Byte Rank = 8;
            if (MyMath.Success(35.0))
                Rank = 7;
            if (MyMath.Success(25.0))
                Rank = 6;
            if (MyMath.Success(15.0))
                Rank = 5;
            if (MyMath.Success(10.0))
                Rank = 4;
            if (MyMath.Success(5.0))
                Rank = 3;
            if (MyMath.Success(2.5))
                Rank = 2;
            if (MyMath.Success(1.0))
                Rank = 1;

            PrizeInfo[] Prizes = GetPrizes(Rank);
            if (Prizes.Length <= 0)
                return false;

            Int32 Pos = MyMath.Generate(0, Prizes.Length - 1);
            while (!MyMath.Success(Prizes[Pos].Chance))
                Pos = MyMath.Generate(0, Prizes.Length - 1);

            Byte Gem1 = 0;
            Byte Gem2 = 0;

            if (Prizes[Pos].Hole_Num > 0)
                Gem1 = 255;
            if (Prizes[Pos].Hole_Num > 1)
                Gem2 = 255;

            UInt16 Dura = GetMaxDura(Prizes[Pos].Item);

            Player.AddItem(Item.Create(Player.UniqId, 0, Prizes[Pos].Item, Prizes[Pos].Addition_Lev, 0, 0, Gem1, Gem2, 2, 0, Dura, Dura), true);
            Player.Move(1036, World.AllMaps[1036].PortalX, World.AllMaps[1036].PortalY);
            if (Rank <= 6)
                World.BroadcastMsg(MsgTalk.Create("SYSTEM", "ALLUSERS", Player.Name + " a gagné " + Prizes[Pos].Name + " chez DameBonheur au Marché!", MsgTalk.Channel.Normal, 0x000000));
            return true;
        }

        private static PrizeInfo[] GetPrizes(Byte Rank)
        {
            List<PrizeInfo> Prizes = new List<PrizeInfo>();

            foreach (PrizeInfo Prize in Database.AllPrizes)
            {
                if (Prize.Rank == Rank)
                    Prizes.Add(Prize);
            }

            return Prizes.ToArray();
        }

        public static Int32 GetBonusId(Int32 Id, Byte Level)
        {
            Int32 BonusId = 0;

            if (Id > 110000 && Id < 120000) //Head
                BonusId = (Id - (Id % 10) - ((Id % 1000) - (Id % 100)));
            else if (Id > 130000 && Id < 140000) //Armor
                BonusId = (Id - (Id % 10) - ((Id % 1000) - (Id % 100)));
            else if (Id > 400000 && Id < 500000 && !(Id > 421000 && Id < 422000)) //Weapon (1H)
                BonusId = (444000 + ((Id % 1000) - (Id % 10)));
            else if (Id > 500000 && Id < 600000 && !(Id > 500000 && Id < 501000)) //Weapon (2H)
                BonusId = (555000 + ((Id % 1000) - (Id % 10)));
            else if (Id > 900000 && Id < 1000000) //Shield
                BonusId = (Id - (Id % 10) - ((Id % 1000) - (Id % 100)));
            else
                BonusId = (Id - (Id % 10));


            BonusId *= 100;
            BonusId += Level;

            return BonusId;
        }

        public static unsafe Int32 GetItemByName(String Name)
        {
            lock (Database2.AllItems)
            {
                foreach (ItemType.Entry Info in Database2.AllItems.Values)
                {
                    ItemType.Entry Info2 = Info;
                    Byte* pName = Info2.Name;
                    if (Kernel.cstring(pName, ItemType.MAX_NAMESIZE).ToLower() == Name.ToLower())
                        return Info.ID;
                }
            }
            return -1;
        }

        public static unsafe String GetName(Int32 Id)
        {
            ItemType.Entry Info;
            if (!Database2.AllItems.TryGetValue(Id, out Info))
                return "Unknow";

            Byte* pName = Info.Name;
            return Kernel.cstring(pName, ItemType.MAX_NAMESIZE);
        }

        public static UInt16 GetMaxDura(Int32 Id)
        {
            ItemType.Entry Info;
            if (!Database2.AllItems.TryGetValue(Id, out Info))
                return 1;

            return Info.AmountLimit;
        }

        public static Int32 GetNextId(Int32 Id)
        {
            Int32 NextId = Id;

            Byte Sort = (Byte)(Id / 100000);
            Byte Type = (Byte)(Id / 10000);
            Int16 SubType = (Int16)(Id / 1000);

            if (SubType == 137)
                return Id;

            if (Id == 150000 || Id == 150310 || Id == 150320 || Id == 410301 || Id == 421301 || Id == 500301)
                return Id;

            if (Sort == 1) //!Weapon
            {
                if (Type == 12 || Type == 15 || Type == 16) //Necklace || Ring || Boots
                {
                    Byte Level = (Byte)(((Id % 1000) - (Id % 10)) / 10);
                    if ((Type == 12 && Level < 8) ||
                        ((Type == 15 && SubType != 152) && Level > 0 && Level < 21) ||
                        ((Type == 15 && SubType == 152) && Level >= 4 && Level < 22) ||
                        (Type == 16 && Level > 0 && Level < 21))
                    {
                        //Check if it's still the same type of item...
                        if ((Int16)((NextId + 20) / 1000) == SubType)
                            NextId += 20;
                    }
                    else if ((Type == 12 && Level == 8) ||
                        (Type == 12 && Level >= 21) ||
                        ((Type == 15 && SubType != 152) && Level == 0) ||
                        ((Type == 15 && SubType != 152) && Level >= 21) ||
                        ((Type == 15 && SubType == 152) && Level >= 22) ||
                        (Type == 16 && Level >= 21))
                    {
                        //Check if it's still the same type of item...
                        if ((Int16)((NextId + 10) / 1000) == SubType)
                            NextId += 10;
                    }
                    else if ((Type == 12 && Level >= 9 && Level < 21) ||
                        ((Type == 15 && SubType == 152) && Level == 1))
                    {
                        //Check if it's still the same type of item...
                        if ((Int16)((NextId + 30) / 1000) == SubType)
                            NextId += 30;
                    }
                }
                else if (Type == 11 || Type == 13) //Head || Armor
                {
                    if (SubType != 112 && SubType != 115)
                    {
                        Int16 Color = (Int16)((Id % 1000) - (Id % 100));
                        Byte Level = (Byte)(((Id % 100) - (Id % 10)) / 10);
                        Byte Quality = (Byte)(Id % 10);

                        if (Level == 9) //Max...
                        {
                            if (Type == 11)
                                NextId = 112000 + Color + ((SubType % 10) * 10) + Quality;
                            else if (Type == 13)
                                NextId += 5000;
                        }
                        else
                        {
                            //Check if it's still the same type of item...
                            if ((Int16)((NextId + 10) / 1000) == SubType)
                                NextId += 10;
                        }
                    }
                    else
                    {
                        //if (SubType == 112)
                        //    NextId += 3000;
                        //else
                        //    NextId += 1000;
                    }
                }
            }
            else if (Sort == 4 || Sort == 5) //Weapon
            {
                //Check if it's still the same type of item...
                if ((Int16)((NextId + 10) / 1000) == SubType)
                    NextId += 10;

                //Invalid Backsword ID
                if ((Int32)(NextId / 10) == 42103 ||
                    (Int32)(NextId / 10) == 42105 ||
                    (Int32)(NextId / 10) == 42109 ||
                    (Int32)(NextId / 10) == 42111)
                    NextId += 10;
            }
            else if (Sort == 9)
            {
                Byte Level = (Byte)(((Id % 100) - (Id % 10)) / 10);
                if (Level != 9) //!Max...
                {
                    //Check if it's still the same type of item...
                    if ((Int16)((NextId + 10) / 1000) == SubType)
                        NextId += 10;
                }
            }

            if (Database2.AllItems.ContainsKey(NextId))
                return NextId;
            else
                return Id;
        }

        public static Int32 GetFirstId(Int32 Id)
        {
            Int32 FirstId = Id;

            Byte Sort = (Byte)(Id / 100000);
            Byte Type = (Byte)(Id / 10000);
            Int16 SubType = (Int16)(Id / 1000);

            if (SubType == 137 || SubType == 422 || SubType == 562)
                return Id;

            if (Id == 150000 || Id == 150310 || Id == 150320 || Id == 410301 || Id == 421301 || Id == 500301)
                return Id;

            if (Id >= 120310 && Id <= 120319)
                return Id;

            if (Sort == 1) //!Weapon
            {
                if (Type == 12) //Necklace
                    FirstId = (Id - (Id % 1000)) + (Id % 10);
                else if (Type == 15 || Type == 16) //Ring || Boots
                    FirstId = (Id - (Id % 1000)) + 10 + (Id % 10);
                else if (Type == 11) //Head
                {
                    if (SubType != 112 && SubType != 115 && SubType != 116)
                        FirstId = (Id - (Id % 100)) + (Id % 10);
                    else
                    {
                        FirstId = 110000 + (((Id % 100) - (Id % 10)) * 100) + ((Id % 1000) - (Id % 100)) + (Id % 10);
                    }
                }
                else if (Type == 13) //Armor
                {
                    if (SubType != 135 && SubType != 136 && SubType != 138 && SubType != 139)
                        FirstId = (Id - (Id % 100)) + (Id % 10);
                    else
                    {
                        FirstId = (Id - (Id % 100)) + (Id % 10);
                        FirstId -= 5000;
                    }
                }
            }
            else if (Sort == 4 || Sort == 5) //Weapon
                FirstId = (Id - (Id % 1000)) + (Id % 10);
            else if (Sort == 9)
                FirstId = (Id - (Id % 100)) + (Id % 10);

            if (Database2.AllItems.ContainsKey(FirstId))
                return FirstId;
            else
                return Id;
        }

        public static Int32 CalcRepairMoney(Item Item)
        {
            ItemType.Entry Info = new ItemType.Entry();
            Database2.AllItems.TryGetValue(Item.Id, out Info);

            Int32 RecoverDurability = Math.Max(0, Item.MaxDura - Item.CurDura);
            if (RecoverDurability == 0)
                return 0;

            Double RepairCost = 0;
            if (Info.Price > 0)
                RepairCost = (Double)(Info.Price * RecoverDurability / Item.MaxDura);

            switch (Item.Id % 10)
            {
                case 9: RepairCost *= 1.125; break;
                case 8: RepairCost *= 0.975; break;
                case 7: RepairCost *= 0.900; break;
                case 6: RepairCost *= 0.825; break;
                default: RepairCost *= 0.750; break;
            }

            return Math.Max(1, (Int32)Math.Floor(RepairCost));
        }

        public static Boolean GetUpQualityInfo(Item Item, out Double Chance, out Int32 NextId)
        {
            ItemType.Entry Info = new ItemType.Entry();
            Database2.AllItems.TryGetValue(Item.Id, out Info);

            NextId = 0;
            Chance = 0;

            if (Item.Id % 10 == 9)
                return false;

            if (Item.Id / 1000 == 137)
                return false;

            if (Item.Id == 150000 || Item.Id == 150310 || Item.Id == 150320 || Item.Id == 410301 || Item.Id == 421301 || Item.Id == 500301)
                return false;

            NextId = Item.Id;
            NextId++;

            Chance = 100.00;
            switch (Item.Id % 10)
            {
                case 6: Chance = 50.00; break;
                case 7: Chance = 33.00; break;
                case 8: Chance = 20.00; break;
                default: Chance = 100.00; break;
            }

            Double Factor = Info.RequiredLevel;
            if (Factor > 70)
                Chance = Chance * (100.00 - (Factor - 70.00)) / 100.00;
            return true;
        }

        public static Boolean GetUpLevelInfo(Item Item, out Double Chance, out Int32 NextId)
        {
            Byte Type = (Byte)(Item.Id / 10000);

            NextId = 0;
            Chance = 0;

            if (ItemHandler.GetNextId(Item.Id) == Item.Id)
                return false;

            NextId = ItemHandler.GetNextId(Item.Id);

            ItemType.Entry Info = new ItemType.Entry();
            Database2.AllItems.TryGetValue(NextId, out Info);

            if (Info.RequiredLevel >= 120)
                return false;

            Chance = 100.00;
            if (Type == 11 || Type == 13 || Type == 90) //Head || Armor || Shield
            {
                switch ((Int32)((Item.Id % 100) / 10))
                {
                    case 5: Chance = 50.00; break;
                    case 6: Chance = 40.00; break;
                    case 7: Chance = 30.00; break;
                    case 8: Chance = 20.00; break;
                    case 9: Chance = 15.00; break;
                    default: Chance = 500.00; break;
                }

                switch (Item.Id % 10)
                {
                    case 6: Chance = Chance * 0.90; break;
                    case 7: Chance = Chance * 0.70; break;
                    case 8: Chance = Chance * 0.30; break;
                    case 9: Chance = Chance * 0.10; break;
                }
            }
            else
            {
                switch ((Int32)((Item.Id % 1000) / 10))
                {
                    case 11: Chance = 95.00; break;
                    case 12: Chance = 90.00; break;
                    case 13: Chance = 85.00; break;
                    case 14: Chance = 80.00; break;
                    case 15: Chance = 75.00; break;
                    case 16: Chance = 70.00; break;
                    case 17: Chance = 65.00; break;
                    case 18: Chance = 60.00; break;
                    case 19: Chance = 55.00; break;
                    case 20: Chance = 50.00; break;
                    case 21: Chance = 45.00; break;
                    case 22: Chance = 40.00; break;
                    default: Chance = 500.00; break;
                }

                switch (Item.Id % 10)
                {
                    case 6: Chance = Chance * 0.90; break;
                    case 7: Chance = Chance * 0.70; break;
                    case 8: Chance = Chance * 0.30; break;
                    case 9: Chance = Chance * 0.10; break;
                }
            }
            return true;
        }
    }
}
