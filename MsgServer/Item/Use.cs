// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using COServer.Network;
using COServer.Entities;

namespace COServer
{
    public partial class ItemHandler
    {
        public static void Use(Player Player, Item Item)
        {
            Client Client = Player.Owner;

            Map Map = null;
            if (!World.AllMaps.TryGetValue(Player.Map, out Map))
                return;

            Boolean Used = false;
            switch (Item.Id)
            {
                case 726011: //Ancient Demon Quest
                case 726012:
                case 726013:
                case 726014:
                case 726015:
                    {
                        if (Player.InventoryContains(726011, 1) &&
                            Player.InventoryContains(726012, 1) &&
                            Player.InventoryContains(726013, 1) &&
                            Player.InventoryContains(726014, 1) &&
                            Player.InventoryContains(726015, 1))
                        {
                            if (Player.Map == 1052 && MyMath.CanSee(Player.X, Player.Y, 216, 210, 10))
                            {
                                MonsterInfo MInfo;
                                Database.AllMonsters.TryGetValue(7006, out MInfo);

                                Monster Monster = new Monster(World.LastMonsterUID, MInfo, -1);
                                World.LastMonsterUID++;

                                Monster.Map = 1052;
                                Monster.StartX = 2000;
                                Monster.StartY = 2000;
                                Monster.X = 216;
                                Monster.Y = 210;

                                World.AllMonsters.Add(Monster.UniqId, Monster);
                                Map.AddEntity(Monster);

                                lock (Map.Entities)
                                {
                                    foreach (Entity Entity in Map.Entities.Values)
                                    {
                                        if (!Entity.IsPlayer())
                                            continue;

                                        (Entity as Player).Screen.ChangeMap();
                                    }
                                }

                                Player.DelItem(726011, 1, true);
                                Player.DelItem(726012, 1, true);
                                Player.DelItem(726013, 1, true);
                                Player.DelItem(726014, 1, true);
                                Player.DelItem(726015, 1, true);
                            }
                        }
                        break;
                    }
                #region PackDNovice
                case 726100: //PackDNoviceL1
                    {
                        if (Player.Level < 1)
                            break;

                        if (Player.ItemInInventory() > 35)
                            break;

                        Player.AddItem(Item.Create(0, 0, 132307, 0, 0, 0, 0, 0, 3, 0, 4099, 4099), true);
                        Player.AddItem(Item.Create(0, 0, 150006, 0, 0, 0, 0, 0, 3, 0, 2099, 2099), true);
                        if (Player.Profession < 100)
                            Player.AddItem(Item.Create(0, 0, 120006, 0, 0, 0, 0, 0, 3, 0, 2099, 2099), true);
                        else
                            Player.AddItem(Item.Create(0, 0, 121006, 0, 0, 0, 0, 0, 3, 0, 2099, 2099), true);
                        Player.AddItem(Item.Create(0, 0, 723017, 0, 0, 0, 0, 0, 3, 0, 1, 1), true);
                        Player.AddItem(Item.Create(0, 0, 726101, 0, 0, 0, 0, 0, 3, 0, 1, 1), true);
                        Used = true;
                        break;
                    }
                case 726101: //PackDNoviceL10
                    {
                        if (Player.Level < 10)
                            break;

                        if (Player.ItemInInventory() > 35)
                            break;

                        Player.AddItem(Item.Create(0, 0, 132318, 0, 0, 0, 0, 0, 3, 0, 4099, 4099), true);
                        Player.AddItem(Item.Create(0, 0, 160016, 0, 0, 0, 0, 0, 3, 0, 2099, 2099), true);
                        if (Player.Profession < 100)
                            Player.AddItem(Item.Create(0, 0, 150016, 0, 0, 0, 0, 0, 3, 0, 2099, 2099), true);
                        else
                            Player.AddItem(Item.Create(0, 0, 152016, 0, 0, 0, 0, 0, 3, 0, 2099, 2099), true);
                        Player.AddItem(Item.Create(0, 0, 723017, 0, 0, 0, 0, 0, 3, 0, 1, 1), true);
                        Player.AddItem(Item.Create(0, 0, 726102, 0, 0, 0, 0, 0, 3, 0, 1, 1), true);
                        Used = true;
                        break;
                    }
                case 726102: //PackDNoviceL20
                    {
                        if (Player.Level < 20)
                            break;

                        if (Player.ItemInInventory() > 35)
                            break;

                        if (Player.Profession / 10 == 1)
                        {
                            Player.AddItem(Item.Create(0, 0, 130317, 0, 0, 0, 0, 0, 3, 0, 4099, 4099), true);
                            Player.AddItem(Item.Create(0, 0, 118317, 0, 0, 0, 0, 0, 3, 0, 4099, 4099), true);
                            Player.AddItem(Item.Create(0, 0, 480047, 0, 0, 0, 0, 0, 3, 0, 4099, 4099), true);
                        }
                        else if (Player.Profession / 10 == 2)
                        {
                            Player.AddItem(Item.Create(0, 0, 131317, 0, 0, 0, 0, 0, 3, 0, 4099, 4099), true);
                            Player.AddItem(Item.Create(0, 0, 111317, 0, 0, 0, 0, 0, 3, 0, 4099, 4099), true);
                            Player.AddItem(Item.Create(0, 0, 480047, 0, 0, 0, 0, 0, 3, 0, 4099, 4099), true);
                        }
                        else if (Player.Profession / 10 == 4)
                        {
                            Player.AddItem(Item.Create(0, 0, 133317, 0, 0, 0, 0, 0, 3, 0, 4099, 4099), true);
                            Player.AddItem(Item.Create(0, 0, 113317, 0, 0, 0, 0, 0, 3, 0, 4099, 4099), true);
                            Player.AddItem(Item.Create(0, 0, 500047, 0, 0, 0, 0, 0, 3, 0, 4099, 4099), true);
                        }
                        else
                        {
                            Player.AddItem(Item.Create(0, 0, 134317, 0, 0, 0, 0, 0, 3, 0, 4099, 4099), true);
                            Player.AddItem(Item.Create(0, 0, 114317, 0, 0, 0, 0, 0, 3, 0, 4099, 4099), true);
                            Player.AddItem(Item.Create(0, 0, 421047, 0, 0, 0, 0, 0, 3, 0, 4099, 4099), true);
                        }
                        Player.AddItem(Item.Create(0, 0, 723700, 0, 0, 0, 0, 0, 3, 0, 1, 1), true);
                        Player.AddItem(Item.Create(0, 0, 726103, 0, 0, 0, 0, 0, 3, 0, 1, 1), true);
                        Used = true;
                        break;
                    }
                case 726103: //PackDNoviceL30
                    {
                        if (Player.Level < 30)
                            break;

                        if (Player.ItemInInventory() > 35)
                            break;

                        Player.AddItem(Item.Create(0, 0, 1088001, 0, 0, 0, 0, 0, 3, 0, 1, 1), true);
                        Player.AddItem(Item.Create(0, 0, 160036, 0, 0, 0, 0, 0, 3, 0, 2099, 2099), true);
                        Player.AddItem(Item.Create(0, 0, 723017, 0, 0, 0, 0, 0, 3, 0, 1, 1), true);
                        Player.AddItem(Item.Create(0, 0, 723700, 0, 0, 0, 0, 0, 3, 0, 1, 1), true);
                        Player.AddItem(Item.Create(0, 0, 726104, 0, 0, 0, 0, 0, 3, 0, 1, 1), true);
                        Used = true;
                        break;
                    }
                case 726104: //PackDNoviceL40
                    {
                        if (Player.Level < 40)
                            break;

                        if (Player.ItemInInventory() > 35)
                            break;

                        if (Player.Profession < 100)
                        {
                            Player.AddItem(Item.Create(0, 0, 120087, 0, 0, 0, 0, 0, 3, 0, 2099, 2099), true);
                            Player.AddItem(Item.Create(0, 0, 150077, 0, 0, 0, 0, 0, 3, 0, 2099, 2099), true);
                        }
                        else
                        {
                            Player.AddItem(Item.Create(0, 0, 121087, 0, 0, 0, 0, 0, 3, 0, 2099, 2099), true);
                            Player.AddItem(Item.Create(0, 0, 152087, 0, 0, 0, 0, 0, 3, 0, 2099, 2099), true);
                        }
                        Player.AddItem(Item.Create(0, 0, 160077, 0, 0, 0, 0, 0, 3, 0, 2099, 2099), true);
                        Player.AddItem(Item.Create(0, 0, 723017, 0, 0, 0, 0, 0, 3, 0, 1, 1), true);
                        Player.AddItem(Item.Create(0, 0, 726105, 0, 0, 0, 0, 0, 3, 0, 1, 1), true);
                        Used = true;
                        break;
                    }
                case 726105: //PackDNoviceL50
                    {
                        if (Player.Level < 50)
                            break;

                        if (Player.ItemInInventory() > 35)
                            break;

                        if (Player.Profession < 100)
                            Player.AddItem(Item.Create(0, 0, 120098, 0, 0, 0, 0, 0, 3, 0, 2099, 2099), true);
                        else
                            Player.AddItem(Item.Create(0, 0, 121098, 0, 0, 0, 0, 0, 3, 0, 2099, 2099), true);
                        Player.AddItem(Item.Create(0, 0, 723700, 0, 0, 0, 0, 0, 3, 0, 1, 1), true);
                        Player.AddItem(Item.Create(0, 0, 723017, 0, 0, 0, 0, 0, 3, 0, 1, 1), true);
                        Player.AddItem(Item.Create(0, 0, 723017, 0, 0, 0, 0, 0, 3, 0, 1, 1), true);
                        Player.AddItem(Item.Create(0, 0, 726106, 0, 0, 0, 0, 0, 3, 0, 1, 1), true);
                        Used = true;
                        break;
                    }
                case 726106: //PackDNoviceL60
                    {
                        if (Player.Level < 60)
                            break;

                        if (Player.ItemInInventory() > 35)
                            break;

                        if (Player.Profession < 100)
                            Player.AddItem(Item.Create(0, 0, 150118, 0, 0, 0, 0, 0, 3, 0, 2099, 2099), true);
                        else
                            Player.AddItem(Item.Create(0, 0, 152128, 0, 0, 0, 0, 0, 3, 0, 2099, 2099), true);
                        Player.AddItem(Item.Create(0, 0, 160118, 0, 0, 0, 0, 0, 3, 0, 2099, 2099), true);
                        Player.AddItem(Item.Create(0, 0, 723700, 0, 0, 0, 0, 0, 3, 0, 1, 1), true);
                        Player.AddItem(Item.Create(0, 0, 723017, 0, 0, 0, 0, 0, 3, 0, 1, 1), true);
                        Player.AddItem(Item.Create(0, 0, 726107, 0, 0, 0, 0, 0, 3, 0, 1, 1), true);
                        Used = true;
                        break;
                    }
                case 726107: //PackDNoviceL70
                    {
                        if (Player.Level < 70)
                            break;

                        if (Player.ItemInInventory() > 35)
                            break;

                        if (Player.Profession / 10 == 1)
                        {
                            Player.AddItem(Item.Create(0, 0, 130368, 0, 0, 0, 0, 0, 3, 0, 4099, 4099), true);
                            Player.AddItem(Item.Create(0, 0, 118368, 0, 0, 0, 0, 0, 3, 0, 4099, 4099), true);
                            Player.AddItem(Item.Create(0, 0, 480148, 0, 0, 0, 0, 0, 3, 0, 4099, 4099), true);
                        }
                        else if (Player.Profession / 10 == 2)
                        {
                            Player.AddItem(Item.Create(0, 0, 131368, 0, 0, 0, 0, 0, 3, 0, 4099, 4099), true);
                            Player.AddItem(Item.Create(0, 0, 111368, 0, 0, 0, 0, 0, 3, 0, 4099, 4099), true);
                            Player.AddItem(Item.Create(0, 0, 480148, 0, 0, 0, 0, 0, 3, 0, 4099, 4099), true);
                        }
                        else if (Player.Profession / 10 == 4)
                        {
                            Player.AddItem(Item.Create(0, 0, 133368, 0, 0, 0, 0, 0, 3, 0, 4099, 4099), true);
                            Player.AddItem(Item.Create(0, 0, 113368, 0, 0, 0, 0, 0, 3, 0, 4099, 4099), true);
                            Player.AddItem(Item.Create(0, 0, 500148, 0, 0, 0, 0, 0, 3, 0, 4099, 4099), true);
                        }
                        else
                        {
                            Player.AddItem(Item.Create(0, 0, 134367, 0, 0, 0, 0, 0, 3, 0, 4099, 4099), true);
                            Player.AddItem(Item.Create(0, 0, 114367, 0, 0, 0, 0, 0, 3, 0, 4099, 4099), true);
                            Player.AddItem(Item.Create(0, 0, 421148, 0, 0, 0, 0, 0, 3, 0, 4099, 4099), true);
                        }
                        Player.AddItem(Item.Create(0, 0, 723700, 0, 0, 0, 0, 0, 3, 0, 1, 1), true);
                        Player.AddItem(Item.Create(0, 0, 726108, 0, 0, 0, 0, 0, 3, 0, 1, 1), true);
                        Used = true;
                        break;
                    }
                case 726108: //PackDNoviceL100
                    {
                        if (Player.Level < 100)
                            break;

                        if (Player.ItemInInventory() > 35)
                            break;

                        if (Player.Profession / 10 == 1)
                            Player.AddItem(Item.Create(0, 0, 130387, 0, 0, 0, 255, 0, 3, 0, 4099, 4099), true);
                        else if (Player.Profession / 10 == 2)
                            Player.AddItem(Item.Create(0, 0, 131387, 0, 0, 0, 255, 0, 3, 0, 4099, 4099), true);
                        else if (Player.Profession / 10 == 4)
                            Player.AddItem(Item.Create(0, 0, 133387, 0, 0, 0, 255, 0, 3, 0, 4099, 4099), true);
                        else
                            Player.AddItem(Item.Create(0, 0, 134387, 0, 0, 0, 255, 0, 3, 0, 4099, 4099), true);
                        Player.AddItem(Item.Create(0, 0, 723017, 0, 0, 0, 0, 0, 3, 0, 1, 1), true);
                        Player.AddItem(Item.Create(0, 0, 723017, 0, 0, 0, 0, 0, 3, 0, 1, 1), true);
                        Player.AddItem(Item.Create(0, 0, 723017, 0, 0, 0, 0, 0, 3, 0, 1, 1), true);
                        Player.AddItem(Item.Create(0, 0, 726109, 0, 0, 0, 0, 0, 3, 0, 1, 1), true);
                        Used = true;
                        break;
                    }
                case 726109: //PackDNoviceL120
                    {
                        if (Player.Level < 120)
                            break;

                        if (Player.ItemInInventory() > 36)
                            break;

                        if (Player.Profession / 10 == 1)
                            Player.AddItem(Item.Create(0, 0, 112387, 0, 0, 0, 255, 0, 3, 0, 4099, 4099), true);
                        else if (Player.Profession / 10 == 2)
                            Player.AddItem(Item.Create(0, 0, 112417, 0, 0, 0, 255, 0, 3, 0, 4099, 4099), true);
                        else if (Player.Profession / 10 == 4)
                            Player.AddItem(Item.Create(0, 0, 112437, 0, 0, 0, 255, 0, 3, 0, 4099, 4099), true);
                        else
                            Player.AddItem(Item.Create(0, 0, 112447, 0, 0, 0, 255, 0, 3, 0, 4099, 4099), true);
                        Player.AddItem(Item.Create(0, 0, 723017, 0, 0, 0, 0, 0, 3, 0, 1, 1), true);
                        Player.AddItem(Item.Create(0, 0, 723017, 0, 0, 0, 0, 0, 3, 0, 1, 1), true);
                        Player.AddItem(Item.Create(0, 0, 723017, 0, 0, 0, 0, 0, 3, 0, 1, 1), true);
                        Used = true;
                        break;
                    }
                #endregion
                case 720010: //+500HP Pack
                    {
                        if (Player.ItemInInventory() > 37)
                            break;

                        for (SByte i = 0; i < 3; i++)
                            Player.AddItem(Item.Create(0, 0, 1000030, 0, 0, 0, 0, 0, 2, 0, ItemHandler.GetMaxDura(1000030), ItemHandler.GetMaxDura(1000030)), true);
                        Used = true;
                        break;
                    }
                case 720011: //+800HP Pack
                    {
                        if (Player.ItemInInventory() > 37)
                            break;

                        for (SByte i = 0; i < 3; i++)
                            Player.AddItem(Item.Create(0, 0, 1002000, 0, 0, 0, 0, 0, 2, 0, ItemHandler.GetMaxDura(1002000), ItemHandler.GetMaxDura(1002000)), true);
                        Used = true;
                        break;
                    }
                case 720012: //+1200HP Pack
                    {
                        if (Player.ItemInInventory() > 37)
                            break;

                        for (SByte i = 0; i < 3; i++)
                            Player.AddItem(Item.Create(0, 0, 1002010, 0, 0, 0, 0, 0, 2, 0, ItemHandler.GetMaxDura(1002010), ItemHandler.GetMaxDura(1002010)), true);
                        Used = true;
                        break;
                    }
                case 720013: //+2000HP Pack
                    {
                        if (Player.ItemInInventory() > 37)
                            break;

                        for (SByte i = 0; i < 3; i++)
                            Player.AddItem(Item.Create(0, 0, 1002020, 0, 0, 0, 0, 0, 2, 0, ItemHandler.GetMaxDura(1002020), ItemHandler.GetMaxDura(1002020)), true);
                        Used = true;
                        break;
                    }
                case 720014: //+1000MP Pack
                    {
                        if (Player.ItemInInventory() > 37)
                            break;

                        for (SByte i = 0; i < 3; i++)
                            Player.AddItem(Item.Create(0, 0, 1001030, 0, 0, 0, 0, 0, 2, 0, ItemHandler.GetMaxDura(1001030), ItemHandler.GetMaxDura(1001030)), true);
                        Used = true;
                        break;
                    }
                case 720015: //+2000MP Pack
                    {
                        if (Player.ItemInInventory() > 37)
                            break;

                        for (SByte i = 0; i < 3; i++)
                            Player.AddItem(Item.Create(0, 0, 1001040, 0, 0, 0, 0, 0, 2, 0, ItemHandler.GetMaxDura(1001040), ItemHandler.GetMaxDura(1001040)), true);
                        Used = true;
                        break;
                    }
                case 720016: //+3000MP Pack
                    {
                        if (Player.ItemInInventory() > 37)
                            break;

                        for (SByte i = 0; i < 3; i++)
                            Player.AddItem(Item.Create(0, 0, 1002030, 0, 0, 0, 0, 0, 2, 0, ItemHandler.GetMaxDura(1002030), ItemHandler.GetMaxDura(1002030)), true);
                        Used = true;
                        break;
                    }
                case 720017: //+4500MP Pack
                    {
                        if (Player.ItemInInventory() > 37)
                            break;

                        for (SByte i = 0; i < 3; i++)
                            Player.AddItem(Item.Create(0, 0, 1002040, 0, 0, 0, 0, 0, 2, 0, ItemHandler.GetMaxDura(1002040), ItemHandler.GetMaxDura(1002040)), true);
                        Used = true;
                        break;
                    }
                case 720027: //Meteors Scroll
                    {
                        if (Player.ItemInInventory() > 30)
                            break;

                        for (SByte i = 0; i < 10; i++)
                            Player.AddItem(Item.Create(0, 0, 1088001, 0, 0, 0, 0, 0, 2, 0, ItemHandler.GetMaxDura(1088001), ItemHandler.GetMaxDura(1088001)), true);
                        Used = true;
                        break;
                    }
                case 720028: //DragonBalls Scroll
                    {
                        if (Player.ItemInInventory() > 30)
                            break;

                        for (SByte i = 0; i < 10; i++)
                            Player.AddItem(Item.Create(0, 0, 1088000, 0, 0, 0, 0, 0, 2, 0, ItemHandler.GetMaxDura(1088000), ItemHandler.GetMaxDura(1088000)), true);
                        Used = true;
                        break;
                    }
                case 721020: //MoonBox
                    {
                        if (Player.ItemInInventory() > 35)
                            break;

                        if (MyMath.Success(15))
                        {
                            Int32 Id = 0;
                            do
                                Id = GenerateId((Byte)MyMath.Generate(25, 100));
                            while (Id == 0);

                            Id = Id - (Id % 10) + 8;
                            Player.AddItem(Item.Create(0, 0, Id, 0, 0, 0, 0, 0, 2, 0, ItemHandler.GetMaxDura(Id), ItemHandler.GetMaxDura(Id)), true);
                        }
                        else if (MyMath.Success(7.5))
                        {
                            Int32 Id = 0;
                            do
                                Id = GenerateId((Byte)MyMath.Generate(25, 100));
                            while (Id == 0);

                            Id = Id - (Id % 10) + 9;
                            Player.AddItem(Item.Create(0, 0, Id, 0, 0, 0, 0, 0, 2, 0, ItemHandler.GetMaxDura(Id), ItemHandler.GetMaxDura(Id)), true);
                        }
                        else if (MyMath.Success(1.5))
                        {
                            Int32 Id = 0;
                            do
                                Id = GenerateId((Byte)MyMath.Generate(25, 100));
                            while (Id == 0 || Id / 100000 == 4 || Id / 100000 == 5);

                            Id = Id - (Id % 10) + MyMath.Generate(5, 8);
                            Player.AddItem(Item.Create(0, 0, Id, 0, 0, 0, 255, 0, 2, 0, ItemHandler.GetMaxDura(Id), ItemHandler.GetMaxDura(Id)), true);
                        }
                        else
                        {
                            Int32 Id = 0;
                            do
                                Id = GenerateId((Byte)MyMath.Generate(25, 100));
                            while (Id == 0);

                            Id = Id - (Id % 10) + 7;

                            Player.AddItem(Item.Create(0, 0, Id, 0, 0, 0, 0, 0, 2, 0, ItemHandler.GetMaxDura(Id), ItemHandler.GetMaxDura(Id)), true);
                            for (SByte i = 0; i < 3; i++)
                                Player.AddItem(Item.Create(0, 0, 1088001, 0, 0, 0, 0, 0, 2, 0, ItemHandler.GetMaxDura(1088001), ItemHandler.GetMaxDura(1088001)), true);
                        }
                        Used = true;
                        break;
                    }
                case 721540: //AncestralBox
                    {
                        if (Player.ItemInInventory() > 35)
                            break;

                        if (MyMath.Success(15))
                        {
                            Int32 Id = 0;
                            do
                                Id = GenerateId((Byte)MyMath.Generate(25, 100));
                            while (Id == 0);

                            Id = Id - (Id % 10) + 8;
                            Player.AddItem(Item.Create(0, 0, Id, 0, 0, 0, 0, 0, 2, 0, ItemHandler.GetMaxDura(Id), ItemHandler.GetMaxDura(Id)), true);
                        }
                        else if (MyMath.Success(10))
                        {
                            Player.AddItem(Item.Create(0, 0, 1088000, 0, 0, 0, 0, 0, 2, 0, ItemHandler.GetMaxDura(1088000), ItemHandler.GetMaxDura(1088000)), true);
                            for (SByte i = 0; i < 2; i++)
                                Player.AddItem(Item.Create(0, 0, 1088001, 0, 0, 0, 0, 0, 2, 0, ItemHandler.GetMaxDura(1088001), ItemHandler.GetMaxDura(1088001)), true);
                        }
                        else if (MyMath.Success(7.5))
                        {
                            Int32 Id = 0;
                            do
                                Id = GenerateId((Byte)MyMath.Generate(25, 100));
                            while (Id == 0);

                            Id = Id - (Id % 10) + 9;
                            Player.AddItem(Item.Create(0, 0, Id, 0, 0, 0, 0, 0, 2, 0, ItemHandler.GetMaxDura(Id), ItemHandler.GetMaxDura(Id)), true);
                        }
                        else if (MyMath.Success(1.5))
                        {
                            Int32 Id = 0;
                            do
                                Id = GenerateId((Byte)MyMath.Generate(25, 100));
                            while (Id == 0 || Id / 100000 == 4 || Id / 100000 == 5);

                            Id = Id - (Id % 10) + MyMath.Generate(5, 8);
                            Player.AddItem(Item.Create(0, 0, Id, 0, 0, 0, 255, 0, 2, 0, ItemHandler.GetMaxDura(Id), ItemHandler.GetMaxDura(Id)), true);
                        }
                        else
                        {
                            Int32 Id = 0;
                            do
                                Id = GenerateId((Byte)MyMath.Generate(25, 100));
                            while (Id == 0);

                            Id = Id - (Id % 10) + 7;

                            Player.AddItem(Item.Create(0, 0, Id, 0, 0, 0, 0, 0, 2, 0, ItemHandler.GetMaxDura(Id), ItemHandler.GetMaxDura(Id)), true);
                            for (SByte i = 0; i < 3; i++)
                                Player.AddItem(Item.Create(0, 0, 1088001, 0, 0, 0, 0, 0, 2, 0, ItemHandler.GetMaxDura(1088001), ItemHandler.GetMaxDura(1088001)), true);
                        }
                        Used = true;
                        break;
                    }
                case 722057: //ExpBall (+10%)
                    {
                        UInt64 Exp = 0;
                        if (Player.Level < Database.AllLevExp.Length)
                            Exp = (UInt64)(10.00 * (Double)Database.AllLevExp[Player.Level] / 100.00);
                        Player.AddExp(Exp, false);
                        Used = true;
                        break;
                    }
                case 722059: //Meteors Box
                    {
                        if (Player.ItemInInventory() > 30)
                            break;

                        for (SByte i = 0; i < 10; i++)
                            Player.AddItem(Item.Create(0, 0, 720027, 0, 0, 0, 0, 0, 2, 0, ItemHandler.GetMaxDura(720027), ItemHandler.GetMaxDura(720027)), true);
                        Used = true;
                        break;
                    }
                case 722060: //DragonBalls Box
                    {
                        if (Player.ItemInInventory() > 30)
                            break;

                        for (SByte i = 0; i < 10; i++)
                            Player.AddItem(Item.Create(0, 0, 720028, 0, 0, 0, 0, 0, 2, 0, ItemHandler.GetMaxDura(720028), ItemHandler.GetMaxDura(720028)), true);
                        Used = true;
                        break;
                    }
                case 723017: //2x Exp Potion
                    {
                        Player.DblExpEndTime = Environment.TickCount + 3600000;

                        Player.Send(MsgUserAttrib.Create(Player, (Int32)((Player.DblExpEndTime - Environment.TickCount) / 1000), MsgUserAttrib.Type.DblExpTime));
                        Used = true;
                        break;
                    }
                case 723583: //Ninja Amulette
                    {
                        if (Player.Look % 2 == 1)
                            Player.Look++;
                        else
                            Player.Look--;

                        World.BroadcastRoomMsg(Player, MsgUserAttrib.Create(Player, Player.Look, MsgUserAttrib.Type.Look), true);
                        Used = true;
                        break;
                    }
                case 723584: //Black Tulip
                    {
                        Item Armor = Player.GetItemByPos(3);
                        if (Armor == null)
                            break;

                        Int32 NewId = Armor.Id - ((Armor.Id % 1000) - Armor.Id % 100);
                        NewId += 200;

                        if (!Database2.AllItems.ContainsKey(NewId))
                            break;

                        Armor.Id = NewId;
                        Player.UpdateItem(Armor);
                        Used = true;
                        break;
                    }
                case 723700: //ExpBall
                    {
                        UInt64 Exp = Player.CalcExpBall((Byte)Player.Level, Player.Exp, 1.00);
                        Player.AddExp(Exp, false);
                        Used = true;
                        break;
                    }
                case 723701: //2rb
                    {
                        Client.NpcUID = 1723701;
                        Client.Send(MsgDialog.Create(0, 0, 0xFF, MsgDialog.Action.Text, "Voulez-vous accomplir la seconde renaissance?"));
                        Client.Send(MsgDialog.Create(0, 0, 1, MsgDialog.Action.Link, "Oui, je suis près."));
                        Client.Send(MsgDialog.Create(0, 0, 255, MsgDialog.Action.Link, "Non, merci."));
                        Client.Send(MsgDialog.Create(0, 6, 0xFF, MsgDialog.Action.Pic, null));
                        Client.Send(MsgDialog.Create(0, 0, 0xFF, MsgDialog.Action.Create, null));
                        break;
                    }
                case 723702: //3rb
                    {
                        Client.NpcUID = 1723702;
                        Client.Send(MsgDialog.Create(0, 0, 0xFF, MsgDialog.Action.Text, "Voulez-vous accomplir le niveau 1 de prestige?"));
                        Client.Send(MsgDialog.Create(0, 0, 1, MsgDialog.Action.Link, "Oui, je suis près."));
                        Client.Send(MsgDialog.Create(0, 0, 255, MsgDialog.Action.Link, "Non, merci."));
                        Client.Send(MsgDialog.Create(0, 6, 0xFF, MsgDialog.Action.Pic, null));
                        Client.Send(MsgDialog.Create(0, 0, 0xFF, MsgDialog.Action.Create, null));
                        break;
                    }
                case 723703: //4rb
                    {
                        Client.NpcUID = 1723702;
                        Client.Send(MsgDialog.Create(0, 0, 0xFF, MsgDialog.Action.Text, "Voulez-vous accomplir le niveau 2 de prestige?"));
                        Client.Send(MsgDialog.Create(0, 0, 2, MsgDialog.Action.Link, "Oui, je suis près."));
                        Client.Send(MsgDialog.Create(0, 0, 255, MsgDialog.Action.Link, "Non, merci."));
                        Client.Send(MsgDialog.Create(0, 6, 0xFF, MsgDialog.Action.Pic, null));
                        Client.Send(MsgDialog.Create(0, 0, 0xFF, MsgDialog.Action.Create, null));
                        break;
                    }
                case 723704: //5rb
                    {
                        Client.NpcUID = 1723702;
                        Client.Send(MsgDialog.Create(0, 0, 0xFF, MsgDialog.Action.Text, "Voulez-vous accomplir le niveau 3 de prestige?"));
                        Client.Send(MsgDialog.Create(0, 0, 3, MsgDialog.Action.Link, "Oui, je suis près."));
                        Client.Send(MsgDialog.Create(0, 0, 255, MsgDialog.Action.Link, "Non, merci."));
                        Client.Send(MsgDialog.Create(0, 6, 0xFF, MsgDialog.Action.Pic, null));
                        Client.Send(MsgDialog.Create(0, 0, 0xFF, MsgDialog.Action.Create, null));
                        break;
                    }
                case 723705: //6rb
                    {
                        Client.NpcUID = 1723702;
                        Client.Send(MsgDialog.Create(0, 0, 0xFF, MsgDialog.Action.Text, "Voulez-vous accomplir le niveau 4 de prestige?"));
                        Client.Send(MsgDialog.Create(0, 0, 4, MsgDialog.Action.Link, "Oui, je suis près."));
                        Client.Send(MsgDialog.Create(0, 0, 255, MsgDialog.Action.Link, "Non, merci."));
                        Client.Send(MsgDialog.Create(0, 6, 0xFF, MsgDialog.Action.Pic, null));
                        Client.Send(MsgDialog.Create(0, 0, 0xFF, MsgDialog.Action.Create, null));
                        break;
                    }
                case 723706: //7rb
                    {
                        Client.NpcUID = 1723702;
                        Client.Send(MsgDialog.Create(0, 0, 0xFF, MsgDialog.Action.Text, "Voulez-vous accomplir le niveau 5 de prestige?"));
                        Client.Send(MsgDialog.Create(0, 0, 5, MsgDialog.Action.Link, "Oui, je suis près."));
                        Client.Send(MsgDialog.Create(0, 0, 255, MsgDialog.Action.Link, "Non, merci."));
                        Client.Send(MsgDialog.Create(0, 6, 0xFF, MsgDialog.Action.Pic, null));
                        Client.Send(MsgDialog.Create(0, 0, 0xFF, MsgDialog.Action.Create, null));
                        break;
                    }
                case 723707: //8rb
                    {
                        Client.NpcUID = 1723702;
                        Client.Send(MsgDialog.Create(0, 0, 0xFF, MsgDialog.Action.Text, "Voulez-vous accomplir le niveau 6 de prestige?"));
                        Client.Send(MsgDialog.Create(0, 0, 6, MsgDialog.Action.Link, "Oui, je suis près."));
                        Client.Send(MsgDialog.Create(0, 0, 255, MsgDialog.Action.Link, "Non, merci."));
                        Client.Send(MsgDialog.Create(0, 6, 0xFF, MsgDialog.Action.Pic, null));
                        Client.Send(MsgDialog.Create(0, 0, 0xFF, MsgDialog.Action.Create, null));
                        break;
                    }
                case 723708: //9rb
                    {
                        Client.NpcUID = 1723702;
                        Client.Send(MsgDialog.Create(0, 0, 0xFF, MsgDialog.Action.Text, "Voulez-vous accomplir le niveau 7 de prestige?"));
                        Client.Send(MsgDialog.Create(0, 0, 7, MsgDialog.Action.Link, "Oui, je suis près."));
                        Client.Send(MsgDialog.Create(0, 0, 255, MsgDialog.Action.Link, "Non, merci."));
                        Client.Send(MsgDialog.Create(0, 6, 0xFF, MsgDialog.Action.Pic, null));
                        Client.Send(MsgDialog.Create(0, 0, 0xFF, MsgDialog.Action.Create, null));
                        break;
                    }
                case 723709: //10rb
                    {
                        Client.NpcUID = 1723702;
                        Client.Send(MsgDialog.Create(0, 0, 0xFF, MsgDialog.Action.Text, "Voulez-vous accomplir le niveau 8 de prestige?"));
                        Client.Send(MsgDialog.Create(0, 0, 8, MsgDialog.Action.Link, "Oui, je suis près."));
                        Client.Send(MsgDialog.Create(0, 0, 255, MsgDialog.Action.Link, "Non, merci."));
                        Client.Send(MsgDialog.Create(0, 6, 0xFF, MsgDialog.Action.Pic, null));
                        Client.Send(MsgDialog.Create(0, 0, 0xFF, MsgDialog.Action.Create, null));
                        break;
                    }
                case 723711: //MeteorTears Pack
                    {
                        if (Player.ItemInInventory() > 35)
                            break;

                        for (SByte i = 0; i < 5; i++)
                            Player.AddItem(Item.Create(0, 0, 1088002, 0, 0, 0, 0, 0, 2, 0, ItemHandler.GetMaxDura(1088002), ItemHandler.GetMaxDura(1088002)), true);
                        Used = true;
                        break;
                    }
                case 723712: //+1Stones Pack
                    {
                        if (Player.ItemInInventory() > 35)
                            break;

                        for (SByte i = 0; i < 5; i++)
                            Player.AddItem(Item.Create(0, 0, 730001, 1, 0, 0, 0, 0, 2, 0, ItemHandler.GetMaxDura(730001), ItemHandler.GetMaxDura(730001)), true);
                        Used = true;
                        break;
                    }
                case 723713: //+300000 MoneyBag
                    {
                        if (Player.Money + 300000 > Player._MAX_MONEYLIMIT)
                        {
                            Player.SendSysMsg(Client.GetStr("STR_TOOMUCH_MONEY"));
                            break;
                        }

                        Player.Money += 300000;
                        Player.Send(MsgUserAttrib.Create(Player, Player.Money, MsgUserAttrib.Type.Money));
                        Used = true;
                        break;
                    }
                case 723714: //+800000 MoneyBag
                    {
                        if (Player.Money + 800000 > Player._MAX_MONEYLIMIT)
                        {
                            Player.SendSysMsg(Client.GetStr("STR_TOOMUCH_MONEY"));
                            break;
                        }

                        Player.Money += 800000;
                        Player.Send(MsgUserAttrib.Create(Player, Player.Money, MsgUserAttrib.Type.Money));
                        Used = true;
                        break;
                    }
                case 723715: //+1200000 MoneyBag
                    {
                        if (Player.Money + 1200000 > Player._MAX_MONEYLIMIT)
                        {
                            Player.SendSysMsg(Client.GetStr("STR_TOOMUCH_MONEY"));
                            break;
                        }

                        Player.Money += 1200000;
                        Player.Send(MsgUserAttrib.Create(Player, Player.Money, MsgUserAttrib.Type.Money));
                        Used = true;
                        break;
                    }
                case 723716: //+1800000 MoneyBag
                    {
                        if (Player.Money + 1800000 > Player._MAX_MONEYLIMIT)
                        {
                            Player.SendSysMsg(Client.GetStr("STR_TOOMUCH_MONEY"));
                            break;
                        }

                        Player.Money += 1800000;
                        Player.Send(MsgUserAttrib.Create(Player, Player.Money, MsgUserAttrib.Type.Money));
                        Used = true;
                        break;
                    }
                case 723717: //+5000000 MoneyBag
                    {
                        if (Player.Money + 5000000 > Player._MAX_MONEYLIMIT)
                        {
                            Player.SendSysMsg(Client.GetStr("STR_TOOMUCH_MONEY"));
                            break;
                        }

                        Player.Money += 5000000;
                        Player.Send(MsgUserAttrib.Create(Player, Player.Money, MsgUserAttrib.Type.Money));
                        Used = true;
                        break;
                    }
                case 723718: //+20000000 MoneyBag
                    {
                        if (Player.Money + 20000000 > Player._MAX_MONEYLIMIT)
                        {
                            Player.SendSysMsg(Client.GetStr("STR_TOOMUCH_MONEY"));
                            break;
                        }

                        Player.Money += 20000000;
                        Player.Send(MsgUserAttrib.Create(Player, Player.Money, MsgUserAttrib.Type.Money));
                        Used = true;
                        break;
                    }
                case 723719: //+25000000 MoneyBag
                    {
                        if (Player.Money + 25000000 > Player._MAX_MONEYLIMIT)
                        {
                            Player.SendSysMsg(Client.GetStr("STR_TOOMUCH_MONEY"));
                            break;
                        }

                        Player.Money += 25000000;
                        Player.Send(MsgUserAttrib.Create(Player, Player.Money, MsgUserAttrib.Type.Money));
                        Used = true;
                        break;
                    }
                case 723720: //+80000000 MoneyBag
                    {
                        if (Player.Money + 80000000 > Player._MAX_MONEYLIMIT)
                        {
                            Player.SendSysMsg(Client.GetStr("STR_TOOMUCH_MONEY"));
                            break;
                        }

                        Player.Money += 80000000;
                        Player.Send(MsgUserAttrib.Create(Player, Player.Money, MsgUserAttrib.Type.Money));
                        Used = true;
                        break;
                    }
                case 723721: //+100000000 MoneyBag
                    {
                        if (Player.Money + 100000000 > Player._MAX_MONEYLIMIT)
                        {
                            Player.SendSysMsg(Client.GetStr("STR_TOOMUCH_MONEY"));
                            break;
                        }

                        Player.Money += 100000000;
                        Player.Send(MsgUserAttrib.Create(Player, Player.Money, MsgUserAttrib.Type.Money));
                        Used = true;
                        break;
                    }
                case 723722: //+300000000 MoneyBag
                    {
                        if (Player.Money + 300000000 > Player._MAX_MONEYLIMIT)
                        {
                            Player.SendSysMsg(Client.GetStr("STR_TOOMUCH_MONEY"));
                            break;
                        }

                        Player.Money += 300000000;
                        Player.Send(MsgUserAttrib.Create(Player, Player.Money, MsgUserAttrib.Type.Money));
                        Used = true;
                        break;
                    }
                case 723723: //+500000000 MoneyBag
                    {
                        if (Player.Money + 500000000 > Player._MAX_MONEYLIMIT)
                        {
                            Player.SendSysMsg(Client.GetStr("STR_TOOMUCH_MONEY"));
                            break;
                        }
                        
                        Player.Money += 500000000;
                        Player.Send(MsgUserAttrib.Create(Player, Player.Money, MsgUserAttrib.Type.Money));
                        Used = true;
                        break;
                    }
                case 723727: //PK Amullette
                    {
                        if (Player.PkPoints <= 0)
                            break;

                        if (Player.PkPoints >= 30 && (Player.PkPoints - 30) < 30)
                            Player.DelFlag(Player.Flag.RedName);
                        else if (Player.PkPoints >= 100 && (Player.PkPoints - 30) < 100)
                        {
                            Player.DelFlag(Player.Flag.BlackName);
                            Player.AddFlag(Player.Flag.RedName);
                        }
                        World.BroadcastRoomMsg(Player, MsgUserAttrib.Create(Player, Player.Flags, MsgUserAttrib.Type.Flags), true);

                        if (Player.PkPoints <= 30)
                            Player.PkPoints = 0;
                        else
                            Player.PkPoints -= 30;

                        Player.Send(MsgUserAttrib.Create(Player, Player.PkPoints, MsgUserAttrib.Type.PkPoints));
                        Used = true;
                        break;
                    }
                case 723728: //+2Stones Pack
                    {
                        if (Player.ItemInInventory() > 35)
                            break;

                        for (SByte i = 0; i < 5; i++)
                            Player.AddItem(Item.Create(0, 0, 730002, 2, 0, 0, 0, 0, 2, 0, ItemHandler.GetMaxDura(730002), ItemHandler.GetMaxDura(730002)), true);
                        Used = true;
                        break;
                    }
                case 723729: //+3Stones Pack
                    {
                        if (Player.ItemInInventory() > 35)
                            break;

                        for (SByte i = 0; i < 5; i++)
                            Player.AddItem(Item.Create(0, 0, 730003, 3, 0, 0, 0, 0, 2, 0, ItemHandler.GetMaxDura(730003), ItemHandler.GetMaxDura(730003)), true);
                        Used = true;
                        break;
                    }
                case 723730: //+4Stones Pack
                    {
                        if (Player.ItemInInventory() > 35)
                            break;

                        for (SByte i = 0; i < 5; i++)
                            Player.AddItem(Item.Create(0, 0, 730004, 4, 0, 0, 0, 0, 2, 0, ItemHandler.GetMaxDura(730004), ItemHandler.GetMaxDura(730004)), true);
                        Used = true;
                        break;
                    }
                case 725000: //Thunder
                    {
                        if (Player.Spirit < 20)
                            break;

                        Magic Magic = Player.GetMagicByType(1000);
                        if (Magic == null)
                            Player.AddMagic(Magic.Create(Player.UniqId, 1000, 0, 0, 0, false), true);
                        else
                        {
                            if (Magic.Unlearn)
                            {
                                Magic.Unlearn = false;
                                Player.Send(MsgMagicInfo.Create(Magic));
                            }
                        }
                        Used = true;
                        break;
                    }
                case 725001: //Fire
                    {
                        if (Player.Spirit < 80)
                            break;

                        Magic Magic = Player.GetMagicByType(1001);
                        if (Magic == null)
                            Player.AddMagic(Magic.Create(Player.UniqId, 1001, 0, 0, 0, false), true);
                        else
                        {
                            if (Magic.Unlearn)
                            {
                                Magic.Unlearn = false;
                                Player.Send(MsgMagicInfo.Create(Magic));
                            }
                        }
                        Used = true;
                        break;
                    }
                case 725002: //Tornado
                    {
                        if (Player.Spirit < 160)
                            break;

                        if (!(Player.Profession >= 142 && Player.Profession <= 145))
                            break;

                        Magic Magic = Player.GetMagicByType(1002);
                        if (Magic == null)
                            Player.AddMagic(Magic.Create(Player.UniqId, 1002, 0, 0, 0, false), true);
                        else
                        {
                            if (Magic.Unlearn)
                            {
                                Magic.Unlearn = false;
                                Player.Send(MsgMagicInfo.Create(Magic));
                            }
                        }
                        Used = true;
                        break;
                    }
                case 725003: //Cure
                    {
                        if (Player.Spirit < 30)
                            break;

                        Magic Magic = Player.GetMagicByType(1005);
                        if (Magic == null)
                            Player.AddMagic(Magic.Create(Player.UniqId, 1005, 0, 0, 0, false), true);
                        else
                        {
                            if (Magic.Unlearn)
                            {
                                Magic.Unlearn = false;
                                Player.Send(MsgMagicInfo.Create(Magic));
                            }
                        }
                        Used = true;
                        break;
                    }
                case 725004: //Lightning
                    {
                        if (Player.Spirit < 25)
                            break;

                        Magic Magic = Player.GetMagicByType(1010);
                        if (Magic == null)
                            Player.AddMagic(Magic.Create(Player.UniqId, 1010, 0, 0, 0, false), true);
                        else
                        {
                            if (Magic.Unlearn)
                            {
                                Magic.Unlearn = false;
                                Player.Send(MsgMagicInfo.Create(Magic));
                            }
                        }
                        Used = true;
                        break;
                    }
                case 725005: //FastBlade
                    {
                        WeaponSkill WeaponSkill = Player.GetWeaponSkillByType(410);
                        if (WeaponSkill == null || WeaponSkill.Level < 5)
                            break;

                        Magic Magic = Player.GetMagicByType(1045);
                        if (Magic == null)
                            Player.AddMagic(Magic.Create(Player.UniqId, 1045, 0, 0, 0, false), true);
                        else
                        {
                            if (Magic.Unlearn)
                            {
                                Magic.Unlearn = false;
                                Player.Send(MsgMagicInfo.Create(Magic));
                            }
                        }
                        Used = true;
                        break;
                    }
                case 725010: //ScentSword
                    {
                        WeaponSkill WeaponSkill = Player.GetWeaponSkillByType(420);
                        if (WeaponSkill == null || WeaponSkill.Level < 5)
                            break;

                        Magic Magic = Player.GetMagicByType(1046);
                        if (Magic == null)
                            Player.AddMagic(Magic.Create(Player.UniqId, 1046, 0, 0, 0, false), true);
                        else
                        {
                            if (Magic.Unlearn)
                            {
                                Magic.Unlearn = false;
                                Player.Send(MsgMagicInfo.Create(Magic));
                            }
                        }
                        Used = true;
                        break;
                    }
                case 725012: //SpeedGun
                    {
                        Magic Magic = Player.GetMagicByType(1260);
                        if (Magic == null)
                            Player.AddMagic(Magic.Create(Player.UniqId, 1260, 0, 0, 0, false), true);
                        else
                        {
                            if (Magic.Unlearn)
                            {
                                Magic.Unlearn = false;
                                Player.Send(MsgMagicInfo.Create(Magic));
                            }
                        }
                        Used = true;
                        break;
                    }
                case 725013: //Penetration
                    {
                        Magic Magic = Player.GetMagicByType(1290);
                        if (Magic == null)
                            Player.AddMagic(Magic.Create(Player.UniqId, 1290, 0, 0, 0, false), true);
                        else
                        {
                            if (Magic.Unlearn)
                            {
                                Magic.Unlearn = false;
                                Player.Send(MsgMagicInfo.Create(Magic));
                            }
                        }
                        Used = true;
                        break;
                    }
                case 725015: //DivineHare
                    {
                        if (Player.Level < 54)
                            break;

                        if (!(Player.Profession >= 132 && Player.Profession <= 135))
                            break;

                        Magic Magic = Player.GetMagicByType(1350);
                        if (Magic == null)
                            Player.AddMagic(Magic.Create(Player.UniqId, 1350, 0, 0, 0, false), true);
                        else
                        {
                            if (Magic.Unlearn)
                            {
                                Magic.Unlearn = false;
                                Player.Send(MsgMagicInfo.Create(Magic));
                            }
                        }
                        Used = true;
                        break;
                    }
                case 725016: //NightDevil
                    {
                        if (Player.Level < 70)
                            break;

                        Magic Magic = Player.GetMagicByType(1360);
                        if (Magic == null)
                            Player.AddMagic(Magic.Create(Player.UniqId, 1360, 0, 0, 0, false), true);
                        else
                        {
                            if (Magic.Unlearn)
                            {
                                Magic.Unlearn = false;
                                Player.Send(MsgMagicInfo.Create(Magic));
                            }
                        }
                        Used = true;
                        break;
                    }
                case 725018: //Dance2
                    {
                        Magic Magic = Player.GetMagicByType(1380);
                        if (Magic == null)
                            Player.AddMagic(Magic.Create(Player.UniqId, 1380, 0, 0, 0, false), true);
                        else
                        {
                            if (Magic.Unlearn)
                            {
                                Magic.Unlearn = false;
                                Player.Send(MsgMagicInfo.Create(Magic));
                            }
                        }
                        Used = true;
                        break;
                    }
                case 725019: //Dance3
                    {
                        Magic Magic = Player.GetMagicByType(1385);
                        if (Magic == null)
                            Player.AddMagic(Magic.Create(Player.UniqId, 1385, 0, 0, 0, false), true);
                        else
                        {
                            if (Magic.Unlearn)
                            {
                                Magic.Unlearn = false;
                                Player.Send(MsgMagicInfo.Create(Magic));
                            }
                        }
                        Used = true;
                        break;
                    }
                case 725020: //Dance4
                    {
                        Magic Magic = Player.GetMagicByType(1390);
                        if (Magic == null)
                            Player.AddMagic(Magic.Create(Player.UniqId, 1390, 0, 0, 0, false), true);
                        else
                        {
                            if (Magic.Unlearn)
                            {
                                Magic.Unlearn = false;
                                Player.Send(MsgMagicInfo.Create(Magic));
                            }
                        }
                        Used = true;
                        break;
                    }
                case 725021: //Dance5
                    {
                        Magic Magic = Player.GetMagicByType(1395);
                        if (Magic == null)
                            Player.AddMagic(Magic.Create(Player.UniqId, 1395, 0, 0, 0, false), true);
                        else
                        {
                            if (Magic.Unlearn)
                            {
                                Magic.Unlearn = false;
                                Player.Send(MsgMagicInfo.Create(Magic));
                            }
                        }
                        Used = true;
                        break;
                    }
                case 725022: //Dance6
                    {
                        Magic Magic = Player.GetMagicByType(1400);
                        if (Magic == null)
                            Player.AddMagic(Magic.Create(Player.UniqId, 1400, 0, 0, 0, false), true);
                        else
                        {
                            if (Magic.Unlearn)
                            {
                                Magic.Unlearn = false;
                                Player.Send(MsgMagicInfo.Create(Magic));
                            }
                        }
                        Used = true;
                        break;
                    }
                case 725023: //Dance7
                    {
                        Magic Magic = Player.GetMagicByType(1405);
                        if (Magic == null)
                            Player.AddMagic(Magic.Create(Player.UniqId, 1405, 0, 0, 0, false), true);
                        else
                        {
                            if (Magic.Unlearn)
                            {
                                Magic.Unlearn = false;
                                Player.Send(MsgMagicInfo.Create(Magic));
                            }
                        }
                        Used = true;
                        break;
                    }
                case 725024: //Dance8
                    {
                        Magic Magic = Player.GetMagicByType(1410);
                        if (Magic == null)
                            Player.AddMagic(Magic.Create(Player.UniqId, 1410, 0, 0, 0, false), true);
                        else
                        {
                            if (Magic.Unlearn)
                            {
                                Magic.Unlearn = false;
                                Player.Send(MsgMagicInfo.Create(Magic));
                            }
                        }
                        Used = true;
                        break;
                    }
                case 725025: //FlyingMoon
                    {
                        if (!(Player.Profession >= 21 && Player.Profession <= 25))
                            break;

                        Magic Magic = Player.GetMagicByType(1320);
                        if (Magic == null)
                            Player.AddMagic(Magic.Create(Player.UniqId, 1320, 0, 0, 0, false), true);
                        else
                        {
                            if (Magic.Unlearn)
                            {
                                Magic.Unlearn = false;
                                Player.Send(MsgMagicInfo.Create(Magic));
                            }
                        }
                        Used = true;
                        break;
                    }
                case 725026: //Snow
                    {
                        Magic Magic = Player.GetMagicByType(5010);
                        if (Magic == null)
                            Player.AddMagic(Magic.Create(Player.UniqId, 5010, 0, 0, 0, false), true);
                        else
                        {
                            if (Magic.Unlearn)
                            {
                                Magic.Unlearn = false;
                                Player.Send(MsgMagicInfo.Create(Magic));
                            }
                        }
                        Used = true;
                        break;
                    }
                case 725027: //StrandedMonster
                    {
                        Magic Magic = Player.GetMagicByType(5020);
                        if (Magic == null)
                            Player.AddMagic(Magic.Create(Player.UniqId, 5020, 0, 0, 0, false), true);
                        else
                        {
                            if (Magic.Unlearn)
                            {
                                Magic.Unlearn = false;
                                Player.Send(MsgMagicInfo.Create(Magic));
                            }
                        }
                        Used = true;
                        break;
                    }
                case 725028: //SpeedLightning
                    {
                        if (Player.Level < 70)
                            break;

                        if (!((Player.Profession / 10) == 13 || (Player.Profession / 10) <= 14))
                            break;

                        Magic Magic = Player.GetMagicByType(5001);
                        if (Magic == null)
                            Player.AddMagic(Magic.Create(Player.UniqId, 5001, 0, 0, 0, false), true);
                        else
                        {
                            if (Magic.Unlearn)
                            {
                                Magic.Unlearn = false;
                                Player.Send(MsgMagicInfo.Create(Magic));
                            }
                        }
                        Used = true;
                        break;
                    }
                case 725029: //Phoenix
                    {
                        Magic Magic = Player.GetMagicByType(5030);
                        if (Magic == null)
                            Player.AddMagic(Magic.Create(Player.UniqId, 5030, 0, 0, 0, false), true);
                        else
                        {
                            if (Magic.Unlearn)
                            {
                                Magic.Unlearn = false;
                                Player.Send(MsgMagicInfo.Create(Magic));
                            }
                        }
                        Used = true;
                        break;
                    }
                case 725030: //Boom
                    {
                        Magic Magic = Player.GetMagicByType(5040);
                        if (Magic == null)
                            Player.AddMagic(Magic.Create(Player.UniqId, 5040, 0, 0, 0, false), true);
                        else
                        {
                            if (Magic.Unlearn)
                            {
                                Magic.Unlearn = false;
                                Player.Send(MsgMagicInfo.Create(Magic));
                            }
                        }
                        Used = true;
                        break;
                    }
                case 725031: //Boreas
                    {
                        Magic Magic = Player.GetMagicByType(5050);
                        if (Magic == null)
                            Player.AddMagic(Magic.Create(Player.UniqId, 5050, 0, 0, 0, false), true);
                        else
                        {
                            if (Magic.Unlearn)
                            {
                                Magic.Unlearn = false;
                                Player.Send(MsgMagicInfo.Create(Magic));
                            }
                        }
                        Used = true;
                        break;
                    }
                case 725041: //Earthquake
                    {
                        Magic Magic = Player.GetMagicByType(7010);
                        if (Magic == null)
                            Player.AddMagic(Magic.Create(Player.UniqId, 7010, 0, 0, 0, false), true);
                        else
                        {
                            if (Magic.Unlearn)
                            {
                                Magic.Unlearn = false;
                                Player.Send(MsgMagicInfo.Create(Magic));
                            }
                        }
                        Used = true;
                        break;
                    }
                case 725042: //Rage
                    {
                        Magic Magic = Player.GetMagicByType(7020);
                        if (Magic == null)
                            Player.AddMagic(Magic.Create(Player.UniqId, 7020, 0, 0, 0, false), true);
                        else
                        {
                            if (Magic.Unlearn)
                            {
                                Magic.Unlearn = false;
                                Player.Send(MsgMagicInfo.Create(Magic));
                            }
                        }
                        Used = true;
                        break;
                    }
                case 725045: //StarFall
                    {
                        Magic Magic = Player.GetMagicByType(1220);
                        if (Magic == null)
                            Player.AddMagic(Magic.Create(Player.UniqId, 1220, 0, 0, 0, false), true);
                        else
                        {
                            if (Magic.Unlearn)
                            {
                                Magic.Unlearn = false;
                                Player.Send(MsgMagicInfo.Create(Magic));
                            }
                        }
                        Used = true;
                        break;
                    }
                case 1000000: //+20HP Potion
                    {
                        Player.CurHP += 20;
                        if (Player.CurHP > Player.MaxHP)
                            Player.CurHP = Player.MaxHP;
                        Player.Send(MsgUserAttrib.Create(Player, Player.CurHP, MsgUserAttrib.Type.Life));
                        Used = true;
                        break;
                    }
                case 1000010: //+100HP Potion
                    {
                        Player.CurHP += 100;
                        if (Player.CurHP > Player.MaxHP)
                            Player.CurHP = Player.MaxHP;
                        Player.Send(MsgUserAttrib.Create(Player, Player.CurHP, MsgUserAttrib.Type.Life));
                        Used = true;
                        break;
                    }
                case 1000020: //+250HP Potion
                    {
                        Player.CurHP += 250;
                        if (Player.CurHP > Player.MaxHP)
                            Player.CurHP = Player.MaxHP;
                        Player.Send(MsgUserAttrib.Create(Player, Player.CurHP, MsgUserAttrib.Type.Life));
                        Used = true;
                        break;
                    }
                case 1000030: //+500HP Potion
                    {
                        Player.CurHP += 500;
                        if (Player.CurHP > Player.MaxHP)
                            Player.CurHP = Player.MaxHP;
                        Player.Send(MsgUserAttrib.Create(Player, Player.CurHP, MsgUserAttrib.Type.Life));
                        Used = true;
                        break;
                    }
                case 1001000: //+70MP Potion
                    {
                        Player.CurMP += 70;
                        if (Player.CurMP > Player.MaxMP)
                            Player.CurMP = Player.MaxMP;
                        Player.Send(MsgUserAttrib.Create(Player, Player.CurMP, MsgUserAttrib.Type.Mana));
                        Used = true;
                        break;
                    }
                case 1001010: //+200MP Potion
                    {
                        Player.CurMP += 200;
                        if (Player.CurMP > Player.MaxMP)
                            Player.CurMP = Player.MaxMP;
                        Player.Send(MsgUserAttrib.Create(Player, Player.CurMP, MsgUserAttrib.Type.Mana));
                        Used = true;
                        break;
                    }
                case 1001020: //+450MP Potion
                    {
                        Player.CurMP += 450;
                        if (Player.CurMP > Player.MaxMP)
                            Player.CurMP = Player.MaxMP;
                        Player.Send(MsgUserAttrib.Create(Player, Player.CurMP, MsgUserAttrib.Type.Mana));
                        Used = true;
                        break;
                    }
                case 1001030: //+1000MP Potion
                    {
                        Player.CurMP += 1000;
                        if (Player.CurMP > Player.MaxMP)
                            Player.CurMP = Player.MaxMP;
                        Player.Send(MsgUserAttrib.Create(Player, Player.CurMP, MsgUserAttrib.Type.Mana));
                        Used = true;
                        break;
                    }
                case 1001040: //+2000MP Potion
                    {
                        Player.CurMP += 2000;
                        if (Player.CurMP > Player.MaxMP)
                            Player.CurMP = Player.MaxMP;
                        Player.Send(MsgUserAttrib.Create(Player, Player.CurMP, MsgUserAttrib.Type.Mana));
                        Used = true;
                        break;
                    }
                case 1002000: //+800HP Potion
                    {
                        Player.CurHP += 800;
                        if (Player.CurHP > Player.MaxHP)
                            Player.CurHP = Player.MaxHP;
                        Player.Send(MsgUserAttrib.Create(Player, Player.CurHP, MsgUserAttrib.Type.Life));
                        Used = true;
                        break;
                    }
                case 1002010: //+1200HP Potion
                    {
                        Player.CurHP += 1200;
                        if (Player.CurHP > Player.MaxHP)
                            Player.CurHP = Player.MaxHP;
                        Player.Send(MsgUserAttrib.Create(Player, Player.CurHP, MsgUserAttrib.Type.Life));
                        Used = true;
                        break;
                    }
                case 1002020: //+2000HP Potion
                    {
                        Player.CurHP += 2000;
                        if (Player.CurHP > Player.MaxHP)
                            Player.CurHP = Player.MaxHP;
                        Player.Send(MsgUserAttrib.Create(Player, Player.CurHP, MsgUserAttrib.Type.Life));
                        Used = true;
                        break;
                    }
                case 1002030: //+3000MP Potion
                    {
                        Player.CurMP += 3000;
                        if (Player.CurMP > Player.MaxMP)
                            Player.CurMP = Player.MaxMP;
                        Player.Send(MsgUserAttrib.Create(Player, Player.CurMP, MsgUserAttrib.Type.Mana));
                        Used = true;
                        break;
                    }
                case 1002040: //+4500MP Potion
                    {
                        Player.CurMP += 4500;
                        if (Player.CurMP > Player.MaxMP)
                            Player.CurMP = Player.MaxMP;
                        Player.Send(MsgUserAttrib.Create(Player, Player.CurMP, MsgUserAttrib.Type.Mana));
                        Used = true;
                        break;
                    }
                case 1002050: //+3000HP Potion
                    {
                        Player.CurHP += 3000;
                        if (Player.CurHP > Player.MaxHP)
                            Player.CurHP = Player.MaxHP;
                        Player.Send(MsgUserAttrib.Create(Player, Player.CurHP, MsgUserAttrib.Type.Life));
                        Used = true;
                        break;
                    }
                case 1060020: //TwinCity Scroll
                    {
                        if (!Map.IsTeleport_Disable() && Player.CurseEndTime == 0)
                            Player.Move(1002, 431, 379);
                        Used = true;
                        break;
                    }
                case 1060021: //DesertCity Scroll
                    {
                        if (World.AllMaps[1000].InWar)
                            return;

                        if (!Map.IsTeleport_Disable() && Player.CurseEndTime == 0)
                            Player.Move(1000, 500, 650);
                        Used = true;
                        break;
                    }
                case 1060022: //ApeMountain Scroll
                    {
                        if (World.AllMaps[1020].InWar)
                            return;

                        if (!Map.IsTeleport_Disable() && Player.CurseEndTime == 0)
                            Player.Move(1020, 567, 576);
                        Used = true;
                        break;
                    }
                case 1060023: //PhoenixCastle Scroll
                    {
                        if (World.AllMaps[1011].InWar)
                            return;

                        if (!Map.IsTeleport_Disable() && Player.CurseEndTime == 0)
                            Player.Move(1011, 190, 271);
                        Used = true;
                        break;
                    }
                case 1060024: //BirdIsland Scroll
                    {
                        if (World.AllMaps[1015].InWar)
                            return;

                        if (!Map.IsTeleport_Disable() && Player.CurseEndTime == 0)
                            Player.Move(1015, 723, 573);
                        Used = true;
                        break;
                    }
                case 1060025: //Wind Scroll
                    {
                        if (!Map.IsTeleport_Disable() && Player.CurseEndTime == 0)
                            Player.Move(1002, 413, 706);
                        Used = true;
                        break;
                    }
                case 1060026: //Wind Scroll
                    {
                        if (!Map.IsTeleport_Disable() && Player.CurseEndTime == 0)
                            Player.Move(1002, 98, 325);
                        Used = true;
                        break;
                    }
                case 1060027: //Wind Scroll
                    {
                        if (!Map.IsTeleport_Disable() && Player.CurseEndTime == 0)
                            Player.Move(1002, 797, 467);
                        Used = true;
                        break;
                    }
                case 1060028: //Wind Scroll
                    {
                        if (!Map.IsTeleport_Disable() && Player.CurseEndTime == 0)
                            Player.Move(1011, 540, 774);
                        Used = true;
                        break;
                    }
                case 1060029: //Wind Scroll
                    {
                        if (!Map.IsTeleport_Disable() && Player.CurseEndTime == 0)
                            Player.Move(1011, 736, 454);
                        Used = true;
                        break;
                    }
                case 1060030: //Black Dye
                    {
                        Player.Hair = (Int16)(300 + (Player.Hair % 100));
                        World.BroadcastRoomMsg(Player, MsgUserAttrib.Create(Player, Player.Hair, MsgUserAttrib.Type.Hair), true);
                        Used = true;
                        break;
                    }
                case 1060031: //Wind Scroll
                    {
                        if (!Map.IsTeleport_Disable() && Player.CurseEndTime == 0)
                            Player.Move(1020, 826, 603);
                        Used = true;
                        break;
                    }
                case 1060032: //Wind Scroll
                    {
                        if (!Map.IsTeleport_Disable() && Player.CurseEndTime == 0)
                            Player.Move(1020, 493, 733);
                        Used = true;
                        break;
                    }
                case 1060033: //Wind Scroll
                    {
                        if (!Map.IsTeleport_Disable() && Player.CurseEndTime == 0)
                            Player.Move(1020, 108, 396);
                        Used = true;
                        break;
                    }
                case 1060034: //Wind Scroll
                    {
                        if (!Map.IsTeleport_Disable() && Player.CurseEndTime == 0)
                            Player.Move(1000, 227, 207);
                        Used = true;
                        break;
                    }
                case 1060035: //Wind Scroll
                    {
                        if (!Map.IsTeleport_Disable() && Player.CurseEndTime == 0)
                            Player.Move(1000, 795, 551);
                        Used = true;
                        break;
                    }
                case 1060037: //Wind Scroll
                    {
                        if (!Map.IsTeleport_Disable() && Player.CurseEndTime == 0)
                            Player.Move(1001, 472, 368);
                        Used = true;
                        break;
                    }
                case 1060038: //Wind Scroll
                    {
                        if (!Map.IsTeleport_Disable() && Player.CurseEndTime == 0)
                            Player.Move(1011, 69, 425);
                        Used = true;
                        break;
                    }
                case 1060040: //Purple Dye
                    {
                        Player.Hair = (Int16)(900 + (Player.Hair % 100));
                        World.BroadcastRoomMsg(Player, MsgUserAttrib.Create(Player, Player.Hair, MsgUserAttrib.Type.Hair), true);
                        Used = true;
                        break;
                    }
                case 1060050: //Blue Dye
                    {
                        Player.Hair = (Int16)(800 + (Player.Hair % 100));
                        World.BroadcastRoomMsg(Player, MsgUserAttrib.Create(Player, Player.Hair, MsgUserAttrib.Type.Hair), true);
                        Used = true;
                        break;
                    }
                case 1060060: //Green Dye
                    {
                        Player.Hair = (Int16)(700 + (Player.Hair % 100));
                        World.BroadcastRoomMsg(Player, MsgUserAttrib.Create(Player, Player.Hair, MsgUserAttrib.Type.Hair), true);
                        Used = true;
                        break;
                    }
                case 1060070: //Brown Dye
                    {
                        Player.Hair = (Int16)(600 + (Player.Hair % 100));
                        World.BroadcastRoomMsg(Player, MsgUserAttrib.Create(Player, Player.Hair, MsgUserAttrib.Type.Hair), true);
                        Used = true;
                        break;
                    }
                case 1060080: //Red Dye
                    {
                        Player.Hair = (Int16)(500 + (Player.Hair % 100));
                        World.BroadcastRoomMsg(Player, MsgUserAttrib.Create(Player, Player.Hair, MsgUserAttrib.Type.Hair), true);
                        Used = true;
                        break;
                    }
                case 1060090: //White Dye
                    {
                        Player.Hair = (Int16)(400 + (Player.Hair % 100));
                        World.BroadcastRoomMsg(Player, MsgUserAttrib.Create(Player, Player.Hair, MsgUserAttrib.Type.Hair), true);
                        Used = true;
                        break;
                    }
                case 1060100: //Bomb
                    {
                        if (Player.Level < 80)
                            break;

                        if (!(Player.Profession >= 142 && Player.Profession <= 145))
                            break;

                        Magic Magic = Player.GetMagicByType(1160);
                        if (Magic == null)
                            Player.AddMagic(Magic.Create(Player.UniqId, 1160, 0, 0, 0, false), true);
                        else
                        {
                            if (Magic.Unlearn)
                            {
                                Magic.Unlearn = false;
                                Player.Send(MsgMagicInfo.Create(Magic));
                            }
                        }
                        Used = true;
                        break;
                    }
                case 1060101: //FireOfHell
                    {
                        if (Player.Level < 82)
                            break;

                        if (!(Player.Profession >= 142 && Player.Profession <= 145))
                            break;

                        Magic Magic = Player.GetMagicByType(1165);
                        if (Magic == null)
                            Player.AddMagic(Magic.Create(Player.UniqId, 1165, 0, 0, 0, false), true);
                        else
                        {
                            if (Magic.Unlearn)
                            {
                                Magic.Unlearn = false;
                                Player.Send(MsgMagicInfo.Create(Magic));
                            }
                        }
                        Used = true;
                        break;
                    }
                case 1060102: //City Scroll
                    {
                        if (!Map.IsTeleport_Disable() && Player.CurseEndTime == 0)
                            Player.Move(1213, 448, 272);
                        Used = true;
                        break;
                    }
                case 1088000: //DragonBall
                    {
                        if (Client.AccLvl > 0)
                        {
                            Player.CPs += 215;
                            Player.Send(MsgUserAttrib.Create(Player, Player.CPs, MsgUserAttrib.Type.CPs));
                            Used = true;
                        }
                        break;
                    }
                case 1088001: //Meteor
                    {
                        if (Client.AccLvl > 0)
                        {
                            if (Player.InventoryContains(1088001, 10))
                            {
                                Player.DelItem(1088001, 10, true);
                                Player.AddItem(Item.Create(Player.UniqId, 0, 720027, 0, 0, 0, 0, 0, 2, 0, 1, 1), true);
                            }
                        }
                        break;
                    }
                case 1200000: //PrayingStone(S)
                    {
                        if (Player.BlessEndTime != 0)
                            return;

                        Player.BlessEndTime = DateTime.UtcNow.AddDays(3).ToBinary();

                        Int64 Seconds = 3 * 24 * 60 * 60;
                        Player.Send(MsgUserAttrib.Create(Player, Seconds, MsgUserAttrib.Type.BlessTime));

                        Byte Param = 0;
                        if (Player.CurseEndTime != 0)
                            Param++;
                        Player.Send(MsgUserAttrib.Create(Player, Param, MsgUserAttrib.Type.SizeAdd));
                        Player.Send(MsgUserAttrib.Create(Player, Param + 2, MsgUserAttrib.Type.SizeAdd));

                        Used = true;
                        break;
                    }
                case 1200001: //PrayingStone(M)
                    {
                        if (Player.BlessEndTime != 0)
                            return;

                        Player.BlessEndTime = DateTime.UtcNow.AddDays(3).ToBinary();

                        Int64 Seconds = 7 * 24 * 60 * 60;
                        Player.Send(MsgUserAttrib.Create(Player, Seconds, MsgUserAttrib.Type.BlessTime));

                        Byte Param = 0;
                        if (Player.CurseEndTime != 0)
                            Param++;
                        Player.Send(MsgUserAttrib.Create(Player, Param, MsgUserAttrib.Type.SizeAdd));
                        Player.Send(MsgUserAttrib.Create(Player, Param + 2, MsgUserAttrib.Type.SizeAdd));

                        Used = true;
                        break;
                    }
                case 1200002: //PrayingStone(L)
                    {
                        if (Player.BlessEndTime != 0)
                            return;

                        Player.BlessEndTime = DateTime.UtcNow.AddDays(30).ToBinary();

                        Int64 Seconds = 30 * 24 * 60 * 60;
                        Player.Send(MsgUserAttrib.Create(Player, Seconds, MsgUserAttrib.Type.BlessTime));

                        Byte Param = 0;
                        if (Player.CurseEndTime != 0)
                            Param++;
                        Player.Send(MsgUserAttrib.Create(Player, Param, MsgUserAttrib.Type.SizeAdd));
                        Player.Send(MsgUserAttrib.Create(Player, Param + 2, MsgUserAttrib.Type.SizeAdd));

                        Used = true;
                        break;
                    }
                default:
                    Player.SendSysMsg("Not Implemented Yet!");
                    break;
            }
            if (Used)
                Player.DelItem(Item.UniqId, true);
        }
    }
}
