// * Created by Jean-Philippe Boivin
// * Copyright © 2010-2011
// * Logik. Project

using System;
using System.Runtime.InteropServices;
using COServer.Entities;
using CO2_CORE_DLL.IO;

namespace COServer.Network
{
    public unsafe class MsgItem : Msg
    {
        public const Int16 Id = _MSG_ITEM;

        public enum Action
        {
            None = 0,
            Buy = 1,					// to server, id: idNpc, data: idItemType
            Sell = 2,	 				// to server, id: idNpc, data: idItem
            Drop = 3,					// to server, x, y
            Use = 4,					// to server, data: position
            Equip = 5,      		    // to client£¬Í¨Öª×°±¸ÎïÆ·
            Unequip = 6,				// to server, data: position
            SplitItem = 7,				// to server, data: num
            CombineItem = 8,			// to server, data: id
            QueryMoneySaved = 9,		// to server/client, id: idNpc, data: nMoney
            SaveMoney = 10,	            // to server, id: idNpc, data: nMoney
            DrawMoney = 11,				// to server, id: idNpc, data: nMoney
            DropMoney = 12,	 			// to server, x, y
            SpendMoney = 13,			// ÎÞÒâÒå£¬ÔÝ±£Áô
            Repair = 14,				// to server, return MsgItemInfo
            RepairAll = 15,         	// to server, return ITEMACT_REPAIRAll, or not return if no money
            Ident = 16,					// to server, return MsgItemInfo
            Durability = 17,			// to client, update durability
            DropEquipement = 18,		// to client, delete equipment
            Improve = 19,				// to server, improve equipment
            UpLevel = 20,	            // to server, upleve equipment
            BoothQuery = 21,			// to server, open booth, data is npc id
            BoothAdd = 22,				// to server/client(broadcast in table), id is idItem, data is money
            BoothDel = 23,				// to server/client(broadcast), id is idItem, data is npc id
            BoothBuy = 24,				// to server, id is idItem, data is npc id
            SynchroAmount = 25,	        // to client(data is amount)
            Fireworks = 26,
            CompleteTask = 27,			// to server, Íê³ÉÓ¶±øÈÎÎñ, id=ÈÎÎñID, dwData=Ìá½»µÄÎïÆ·£¬Èç¹ûÃ»ÓÐÔòÎªID_NONE
            Enchant = 28,               // To Server, UniqId: ItemUID, Param: GemUID | To Client, UniqId: ItemUID, Param: HP
            BoothAddCPs = 29,           // to server/client(broadcast in table), id is idItem, data is money
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct MsgInfo
        {
            public MsgHeader Header;
            public Int32 UniqId;
            public Int32 Param;
            public Int32 Action;
            public Int32 Timestamp;
            public Int32 Amount; //Buy -> CPs
        };

        public static Byte[] Create(Int32 UniqId, Int32 Param, Action Action)
        {
            try
            {
                MsgInfo* pMsg = stackalloc MsgInfo[1];
                pMsg->Header.Length = (Int16)sizeof(MsgInfo);
                pMsg->Header.Type = Id;

                pMsg->UniqId = UniqId;
                pMsg->Param = Param;
                pMsg->Action = (Int32)Action;
                pMsg->Timestamp = Environment.TickCount;
                pMsg->Amount = 0x00;

                Byte[] Out = new Byte[pMsg->Header.Length];
                Marshal.Copy((IntPtr)pMsg, Out, 0, Out.Length);

                return Out;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return null; }
        }

        public static void Process(Client Client, Byte[] Buffer)
        {
            try
            {
                if (Client == null || Buffer == null || Client.User == null)
                    return;

                if (Buffer.Length != sizeof(MsgInfo))
                    return;

                MsgInfo* pMsg = null;
                fixed (Byte* pBuf = Buffer)
                    pMsg = (MsgInfo*)pBuf;

                Player Player = Client.User;

                switch ((Action)pMsg->Action)
                {
                    case Action.Buy:
                        {
                            if (!Player.IsAlive())
                            {
                                Player.SendSysMsg(Client.GetStr("STR_DIE"));
                                return;
                            }

                            if (pMsg->UniqId != 2888) //VIP Shop
                            {
                                NPC Npc = null;
                                if (!World.AllNPCs.TryGetValue(pMsg->UniqId, out Npc))
                                    return;

                                if (!Npc.IsShopNpc())
                                    return;

                                if (Player.Map != Npc.Map)
                                    return;

                                if (!MyMath.CanSee(Player.X, Player.Y, Npc.X, Npc.Y, 17))
                                    return;
                            }

                            ShopInfo Shop = new ShopInfo();
                            if (!Database.AllShops.TryGetValue(pMsg->UniqId, out Shop))
                                return;

                            if (!Shop.Items.Contains(pMsg->Param))
                                return;

                            if (pMsg->Amount < 1)
                                pMsg->Amount = 1;

                            if (Player.ItemInInventory() + pMsg->Amount > 40)
                            {
                                Player.SendSysMsg(Client.GetStr("STR_FULL_CANNOT_BUY"));
                                return;
                            }

                            ItemType.Entry Info = new ItemType.Entry();
                            if (!Database2.AllItems.TryGetValue(pMsg->Param, out Info))
                                return;

                            Byte Craft = 0;
                            if ((Int32)(pMsg->Param / 10) == 73000)
                                Craft = (Byte)(pMsg->Param % 10);

                            UInt16 Dura = ItemHandler.GetMaxDura(pMsg->Param);

                            Int32 Money = 0;
                            if (Shop.MoneyType == 1)
                            {
                                Money = Math.Min(Player._MAX_MONEYLIMIT, (Int32)Info.ConquerPoints * pMsg->Amount);
                                if (Player.CPs < Money)
                                {
                                    Player.SendSysMsg(Client.GetStr("STR_NOT_SO_MUCH_MONEY"));
                                    return;
                                }

                                Player.CPs -= Money;
                                Player.Send(MsgUserAttrib.Create(Player, Player.CPs, MsgUserAttrib.Type.CPs));

                                Item Item = null;

                                Int32 Amount = pMsg->Amount;
                                for (Int32 i = 0; i < Amount; i++)
                                {
                                    Item = Item.Create(Player.UniqId, 0, pMsg->Param, Craft, 0, 0, 0, 0, 2, 0, Dura, Dura);
                                    if (Item != null)
                                        Player.AddItem(Item, true);
                                    else
                                        i--;
                                }
                                Database.Save(Player);
                            }
                            else
                            {
                                Money = Math.Min(Player._MAX_MONEYLIMIT, (Int32)Info.Price * pMsg->Amount);
                                if (Player.Money < Money)
                                {
                                    Player.SendSysMsg(Client.GetStr("STR_NOT_SO_MUCH_MONEY"));
                                    return;
                                }

                                Player.Money -= Money;
                                Player.Send(MsgUserAttrib.Create(Player, Player.Money, MsgUserAttrib.Type.Money));

                                Item Item = null;
                                for (Int32 i = 0; i < pMsg->Amount; i++)
                                {
                                    Item = Item.Create(Player.UniqId, 0, pMsg->Param, Craft, 0, 0, 0, 0, 2, 0, Dura, Dura);
                                    if (Item != null)
                                        Player.AddItem(Item, true);
                                    else
                                        i--;
                                }
                                Database.Save(Player);
                            }
                            break;
                        }
                    case Action.Sell:
                        {
                            if (!Player.IsAlive())
                            {
                                Player.SendSysMsg(Client.GetStr("STR_DIE"));
                                return;
                            }

                            NPC Npc = null;
                            if (!World.AllNPCs.TryGetValue(pMsg->UniqId, out Npc))
                                return;

                            if (!Npc.IsShopNpc())
                                return;

                            if (Player.Map != Npc.Map)
                                return;

                            if (!MyMath.CanSee(Player.X, Player.Y, Npc.X, Npc.Y, 17))
                                return;

                            Item Item = Player.GetItemByUID(pMsg->Param);
                            if (Item == null)
                            {
                                Player.SendSysMsg(Client.GetStr("STR_ITEM_NOT_FOUND"));
                                return;
                            }

                            ItemType.Entry Info = new ItemType.Entry();
                            Database2.AllItems.TryGetValue(Item.Id, out Info);

                            if (!Info.IsSellEnable())
                            {
                                Player.SendSysMsg(Client.GetStr("STR_NOT_SELL_ENABLE"));
                                return;
                            }

                            if (Item.Id % 10 == 9)
                                Program.Log(Player.Name + " (" + Player.UniqId + ") sell item:[id=" + Item.UniqId + ", type=" + Item.Id + "], dur=" + Item.CurDura + ", max_dur=" + Item.MaxDura);

                            Int32 Money = (Int32)(Info.Price / 3);
                            Money = (Int32)((Double)Money * ((Double)Item.CurDura / (Double)Item.MaxDura));

                            if (Money < 0 || Money > Player._MAX_MONEYLIMIT)
                                Money = 0;

                            if (Player.Money + Money > Player._MAX_MONEYLIMIT)
                            {
                                Player.SendSysMsg(Client.GetStr("STR_TOOMUCH_MONEY"));
                                return;
                            }

                            Player.DelItem(pMsg->Param, true);

                            Player.Money += Money;
                            Player.Send(MsgUserAttrib.Create(Player, Player.Money, MsgUserAttrib.Type.Money));
                            break;
                        }
                    case Action.Drop:
                        {
                            UInt16 X = *((UInt16*)&pMsg->Param + 1); //(UInt16)(Param >> 16);
                            UInt16 Y = *((UInt16*)&pMsg->Param);

                            if (!MyMath.CanSee(Player.X, Player.Y, X, Y, MyMath.USERDROP_RANGE))
                                return;

                            if (!Player.IsAlive())
                                return;

                            if (!World.AllMaps.ContainsKey(Player.Map))
                                return;

                            if (!World.AllMaps[Player.Map].IsValidPoint(X, Y))
                                return;

                            Item Item = Player.GetItemByUID(pMsg->UniqId);

                            if (Item == null)
                                return;

                            Player.DelItem(Item, true);
                            FloorItem FloorItem = new FloorItem(Item, 0, 0, Player.Map, X, Y);
                            World.FloorThread.AddToQueue(FloorItem);
                            break;
                        }
                    case Action.Use:
                        {
                            if (!Player.IsAlive())
                            {
                                Player.SendSysMsg(Client.GetStr("STR_DIE"));
                                return;
                            }

                            Item Item = Player.GetItemByUID(pMsg->UniqId);
                            if (Item == null)
                                return;

                            ItemType.Entry Info;
                            if (!Database2.AllItems.TryGetValue(Item.Id, out Info))
                                return;

                            if ((UInt16)(Item.Id / 10000) == 105)
                                pMsg->Param = 5;

                            if ((UInt16)(Item.Id / 10000) == 105)
                            {
                                Item Bow = Player.GetItemByPos(4);
                                if (Bow == null || Bow.Id / 1000 != 500)
                                    return;
                            }

                            if (pMsg->Param != 0)
                            {
                                Byte Sex = (Byte)((Player.Look / 1000) - ((Player.Look / 1000) - ((Player.Look / 1000) % 10)));
                                Byte Job = Player.Profession;
                                Byte Weapon = (Byte)((Item.Id - (Item.Id % 100000)) / 100000);
                                Int16 WeaponSkill = (Int16)((Item.Id - (Item.Id % 1000)) / 1000);

                                if (Player.Profession > 129 && Player.Profession < 136)
                                    Job = (Byte)(Player.Profession + 60);
                                else if (Player.Profession > 139 && Player.Profession < 146)
                                    Job = (Byte)(Player.Profession + 50);
                                else if (Player.Profession > 99 && Player.Profession < 116)
                                    Job = (Byte)(Player.Profession + 90);

                                //Equip
                                if (Info.RequiredLevel > Player.Level)
                                    return;

                                if (Info.RequiredSex != 0 && Info.RequiredSex != Sex)
                                    return;

                                if (Info.RequiredLevel > 70 || (Player.Metempsychosis == 0 && Player.Level < 70))
                                {
                                    if (Info.RequiredProfession != 0 && Info.RequiredProfession / 10 != Job / 10 && Info.RequiredProfession > Job)
                                        return;

                                    if (Info.RequiredForce > Player.Strength)
                                        return;

                                    if (Info.RequiredSpeed > Player.Agility)
                                        return;

                                    if (Info.RequiredHealth > Player.Vitality)
                                        return;

                                    if (Info.RequiredSoul > Player.Spirit)
                                        return;
                                }

                                if (Info.RequiredWeaponSkill != 0)
                                    if (Weapon == 4 || Weapon == 5)
                                        if (Player.GetWeaponSkillByType(WeaponSkill) == null
                                            || Info.RequiredWeaponSkill > Player.GetWeaponSkillByType(WeaponSkill).Level)
                                            return;

                                if (Weapon == 9 && !(Job > 19 && Job < 26))
                                    return;

                                if (Item.Id == 137310 && Client.AccLvl < 7) //GM Robe
                                    return;

                                if (Player.GetItemByPos(5) != null)
                                    if (((Byte)(Item.Id / 100000) == 5 || (Int16)(Item.Id / 1000) == 421))
                                    {
                                        Item LeftHand = Player.GetItemByPos(5);
                                        if (Item.Id / 1000 == 500 && (UInt16)(LeftHand.Id / 10000) != 105)
                                            Player.GetItemByPos(5).Position = 0;
                                        else if (Item.Id / 1000 != 500)
                                            Player.GetItemByPos(5).Position = 0;
                                    }

                                if (Player.GetItemByPos((Byte)pMsg->Param) != null)
                                    Player.GetItemByPos((Byte)pMsg->Param).Position = 0;
                                Item.Position = (Byte)pMsg->Param;

                                MyMath.GetHitPoints(Player, true);
                                MyMath.GetMagicPoints(Player, true);
                                MyMath.GetPotency(Player, true);
                                MyMath.GetEquipStats(Player);
                                Player.SendEquipStats();

                                Player.Send(MsgItem.Create(pMsg->UniqId, Item.Position, Action.Equip));
                                World.BroadcastRoomMsg(Player, MsgPlayer.Create(Player), false);
                            }
                            else
                            {
                                if (Program.Debug)
                                    Player.SendSysMsg("Msg[" + pMsg->Header.Type + "], Param0[" + pMsg->UniqId + ":" + Item.Id + "], Param1[" + pMsg->Param + "], Action[" + (Int16)pMsg->Action + "]");
                                ItemHandler.Use(Player, Item);
                            }
                            break;
                        }
                    case Action.Unequip:
                        {
                            if (!Player.IsAlive())
                            {
                                Player.SendSysMsg(Client.GetStr("STR_DIE"));
                                return;
                            }

                            Item Item = Player.GetItemByUID(pMsg->UniqId);
                            if (Item == null)
                                return;

                            if (Item.Position != (Byte)pMsg->Param)
                                return;

                            Item.Position = 0;

                            MyMath.GetHitPoints(Player, true);
                            MyMath.GetMagicPoints(Player, true);
                            MyMath.GetPotency(Player, true);
                            MyMath.GetEquipStats(Player);
                            Player.SendEquipStats();

                            Player.Send(Buffer);
                            World.BroadcastRoomMsg(Player, MsgPlayer.Create(Player), false);
                            break;
                        }
                    case Action.QueryMoneySaved:
                        {
                            NPC Npc = null;
                            if (!World.AllNPCs.TryGetValue(pMsg->UniqId, out Npc))
                                return;

                            if (Player.Map != Npc.Map)
                                return;

                            if (!MyMath.CanSee(Player.X, Player.Y, Npc.X, Npc.Y, 17))
                                return;

                            if (!Npc.IsStorageNpc())
                                return;

                            Player.Send(MsgItem.Create(pMsg->UniqId, Player.WHMoney, Action.QueryMoneySaved));
                            break;
                        }
                    case Action.SaveMoney:
                        {
                            NPC Npc = null;
                            if (!World.AllNPCs.TryGetValue(pMsg->UniqId, out Npc))
                                return;

                            if (Player.Map != Npc.Map)
                                return;

                            if (!MyMath.CanSee(Player.X, Player.Y, Npc.X, Npc.Y, 17))
                                return;

                            if (!Npc.IsStorageNpc())
                                return;

                            if (Player.WHMoney + pMsg->Param > Player._MAX_MONEYLIMIT)
                                return;

                            if (Player.Money >= pMsg->Param)
                            {
                                Player.Money -= pMsg->Param;
                                Player.WHMoney += pMsg->Param;
                                Player.Send(MsgUserAttrib.Create(Player, Player.Money, MsgUserAttrib.Type.Money));
                            }
                            break;
                        }
                    case Action.DrawMoney:
                        {
                            NPC Npc = null;
                            if (!World.AllNPCs.TryGetValue(pMsg->UniqId, out Npc))
                                return;

                            if (Player.Map != Npc.Map)
                                return;

                            if (!MyMath.CanSee(Player.X, Player.Y, Npc.X, Npc.Y, 17))
                                return;

                            if (!Npc.IsStorageNpc())
                                return;

                            if (Player.Money + pMsg->Param > Player._MAX_MONEYLIMIT)
                                return;

                            if (Player.WHMoney >= pMsg->Param)
                            {
                                Player.WHMoney -= pMsg->Param;
                                Player.Money += pMsg->Param;
                                Player.Send(MsgUserAttrib.Create(Player, Player.Money, MsgUserAttrib.Type.Money));
                            }
                            break;
                        }
                    case Action.DropMoney:
                        {
                            UInt16 X = *((UInt16*)&pMsg->Param + 1); //(UInt16)(Param >> 16);
                            UInt16 Y = *((UInt16*)&pMsg->Param);

                            if (!MyMath.CanSee(Player.X, Player.Y, X, Y, MyMath.USERDROP_RANGE))
                                return;

                            if (Player.Money < pMsg->UniqId)
                                return;

                            if (pMsg->UniqId < 100)
                                return;

                            if (!Player.IsAlive())
                            {
                                Player.SendSysMsg(Client.GetStr("STR_DIE_DROP_MONEY"));
                                return;
                            }

                            Item Item = null;
                            if (pMsg->UniqId <= 10) //Silver
                                Item = Item.Create(0, 254, 1090000, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                            else if (pMsg->UniqId <= 100) //Sycee
                                Item = Item.Create(0, 254, 1090010, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                            else if (pMsg->UniqId <= 1000) //Gold
                                Item = Item.Create(0, 254, 1090020, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                            else if (pMsg->UniqId <= 2000) //GoldBullion
                                Item = Item.Create(0, 254, 1091000, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                            else if (pMsg->UniqId <= 5000) //GoldBar
                                Item = Item.Create(0, 254, 1091010, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                            else if (pMsg->UniqId > 5000) //GoldBars
                                Item = Item.Create(0, 254, 1091020, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                            else //Error
                                Item = Item.Create(0, 254, 1090000, 0, 0, 0, 0, 0, 0, 0, 0, 0);

                            if (Item != null)
                            {
                                Player.Money -= pMsg->UniqId;
                                Player.Send(MsgUserAttrib.Create(Player, Player.Money, MsgUserAttrib.Type.Money));
                                Database.Save(Player);

                                Player.SendSysMsg(Client.GetStr("STR_DROP_MONEY_SUCC").Replace("%d", pMsg->UniqId.ToString()));
                                FloorItem FloorItem = new FloorItem(Item, pMsg->UniqId, 0, Player.Map, X, Y);
                                World.FloorThread.AddToQueue(FloorItem);
                            }
                            else
                                Player.SendSysMsg(Client.GetStr("STR_MAKE_MONEY_FAILED"));
                            break;
                        }
                    case Action.Repair:
                        {
                            if (!Player.IsAlive())
                            {
                                Player.SendSysMsg(Client.GetStr("STR_DIE"));
                                return;
                            }

                            Item Item = Player.GetItemByUID(pMsg->UniqId);
                            if (Item == null)
                            {
                                Player.SendSysMsg(Client.GetStr("STR_ITEM_NOT_FOUND"));
                                return;
                            }

                            if (Item.Position != 0)
                                return;

                            ItemType.Entry Info = new ItemType.Entry();
                            Database2.AllItems.TryGetValue(Item.Id, out Info);

                            if (!Info.IsRepairEnable() || Item.CurDura == Item.MaxDura)
                            {
                                Player.SendSysMsg(Client.GetStr("STR_REPAIR_FAILED"));
                                return;
                            }

                            if (Item.CurDura > Item.MaxDura)
                            {
                                Item.CurDura = Item.MaxDura;
                                Player.Send(MsgItem.Create(Item.UniqId, Item.CurDura, Action.SynchroAmount));
                                return;
                            }

                            if (Item.CurDura > 0)
                            {
                                Int32 Money = ItemHandler.CalcRepairMoney(Item);
                                if (Player.Money < Money)
                                {
                                    Player.SendSysMsg(Client.GetStr("STR_NOT_SO_MUCH_MONEY"));
                                    return;
                                }

                                Player.Money -= Money;
                                Player.Send(MsgUserAttrib.Create(Player, Player.Money, MsgUserAttrib.Type.Money));

                                Item.CurDura = Item.MaxDura;
                                Player.Send(MsgItem.Create(Item.UniqId, Item.CurDura, Action.SynchroAmount));
                            }
                            else
                            {
                                if (!Player.InventoryContains(1088001, 5))
                                {
                                    Player.SendSysMsg(Client.GetStr("STR_REPAIR_FAILED"));
                                    return;
                                }

                                for (Byte i = 0; i < 5; i++)
                                    Player.DelItem(Player.GetItemById(1088001), true);

                                Item.CurDura = Item.MaxDura;
                                Player.Send(MsgItemInfo.Create(Item, MsgItemInfo.Action.Update));
                            }
                            break;
                        }
                    case Action.Improve:
                        {
                            if (!Player.IsAlive())
                            {
                                Player.SendSysMsg(Client.GetStr("STR_DIE"));
                                return;
                            }

                            Item Item = Player.GetItemByUID(pMsg->UniqId);
                            if (Item == null)
                            {
                                Player.SendSysMsg(Client.GetStr("STR_ITEM_NOT_FOUND"));
                                return;
                            }

                            if (Item.Position != 0 || Item.Id % 10 == 9)
                                return;

                            if (Item.CurDura != Item.MaxDura)
                            {
                                Player.SendSysMsg(Client.GetStr("STR_ITEM_DAMAGED"));
                                return; 
                            }

                            Item Treasure = Player.GetItemByUID(pMsg->Param);
                            if (Treasure == null)
                            {
                                Player.SendSysMsg(Client.GetStr("STR_ITEM_NOT_FOUND"));
                                return;
                            }

                            if (Treasure.Position != 0 || Treasure.Id != 1088000)
                                return;

                            Double Chance;
                            Int32 NextId;
                            if (!ItemHandler.GetUpQualityInfo(Item, out Chance, out NextId))
                                return;

                            Player.DelItem(Treasure, true);
                            if (!MyMath.Success(Chance))
                            {
                                Item.CurDura /= 2;
                                Item.MaxDura -= 15;
                                Player.SendSysMsg(Client.GetStr("STR_IMPROVE_FAILED"));
                            }
                            else
                            {
                                if (Item.Gem1 == 0 && MyMath.Success(2.00 * Database.Rates.Socket))
                                    Item.Gem1 = 255;
                                else if (Item.Gem2 == 0 && MyMath.Success(1.00 * Database.Rates.Socket))
                                    Item.Gem2 = 255;

                                Item.Id = NextId;
                                Item.CurDura = ItemHandler.GetMaxDura(NextId);
                                Item.MaxDura = ItemHandler.GetMaxDura(NextId);

                                Byte DuraEffect = 0;
                                if (Item.Gem1 - (Item.Gem1 % 10) == 40) //Kylin
                                {
                                    DuraEffect += (Byte)(Item.Gem1 % 10);
                                    if (Item.Gem1 % 10 == 3)
                                        DuraEffect++;
                                }

                                if (Item.Gem2 - (Item.Gem2 % 10) == 40) //Kylin
                                {
                                    DuraEffect += (Byte)(Item.Gem2 % 10);
                                    if (Item.Gem2 % 10 == 3)
                                        DuraEffect++;
                                }

                                Double Bonus = 1.0;
                                Bonus += 0.5 * DuraEffect;

                                Item.MaxDura = (UInt16)((Double)Item.MaxDura * Bonus);
                                Item.CurDura = Item.MaxDura;

                                Player.SendSysMsg(Client.GetStr("STR_IMPROVE_SUCCEED"));
                            }

                            Player.Send(MsgItemInfo.Create(Item, MsgItemInfo.Action.Update));
                            break;
                        }
                    case Action.UpLevel:
                        {
                            if (!Player.IsAlive())
                            {
                                Player.SendSysMsg(Client.GetStr("STR_DIE"));
                                return;
                            }

                            Item Item = Player.GetItemByUID(pMsg->UniqId);
                            if (Item == null)
                            {
                                Player.SendSysMsg(Client.GetStr("STR_ITEM_NOT_FOUND"));
                                return;
                            }

                            if (Item.Position != 0)
                                return;

                            if (Item.CurDura != Item.MaxDura)
                            {
                                Player.SendSysMsg(Client.GetStr("STR_ITEM_DAMAGED"));
                                return;
                            }

                            Item Treasure = Player.GetItemByUID(pMsg->Param);
                            if (Treasure == null)
                            {
                                Player.SendSysMsg(Client.GetStr("STR_ITEM_NOT_FOUND"));
                                return;
                            }

                            if (Treasure.Position != 0 || Treasure.Id != 1088001)
                                return;

                            Double Chance;
                            Int32 NextId;
                            if (!ItemHandler.GetUpLevelInfo(Item, out Chance, out NextId))
                                return;

                            Player.DelItem(Treasure, true);
                            if (!MyMath.Success(Chance))
                            {
                                Item.CurDura /= 2;
                                Item.MaxDura -= 15;
                                Player.SendSysMsg(Client.GetStr("STR_IMPROVE_FAILED"));
                            }
                            else
                            {
                                if (Item.Gem1 == 0 && MyMath.Success(1.50 * Database.Rates.Socket))
                                    Item.Gem1 = 255;
                                else if (Item.Gem2 == 0 && MyMath.Success(0.75 * Database.Rates.Socket))
                                    Item.Gem2 = 255;

                                Item.Id = NextId;
                                Item.CurDura = ItemHandler.GetMaxDura(NextId);
                                Item.MaxDura = ItemHandler.GetMaxDura(NextId);

                                Byte DuraEffect = 0;
                                if (Item.Gem1 - (Item.Gem1 % 10) == 40) //Kylin
                                {
                                    DuraEffect += (Byte)(Item.Gem1 % 10);
                                    if (Item.Gem1 % 10 == 3)
                                        DuraEffect++;
                                }

                                if (Item.Gem2 - (Item.Gem2 % 10) == 40) //Kylin
                                {
                                    DuraEffect += (Byte)(Item.Gem2 % 10);
                                    if (Item.Gem2 % 10 == 3)
                                        DuraEffect++;
                                }

                                Double Bonus = 1.0;
                                Bonus += 0.5 * DuraEffect;

                                Item.MaxDura = (UInt16)((Double)Item.MaxDura * Bonus);
                                Item.CurDura = Item.MaxDura;

                                Player.SendSysMsg(Client.GetStr("STR_IMPROVE_SUCCEED"));
                            }

                            Player.Send(MsgItemInfo.Create(Item, MsgItemInfo.Action.Update));
                            break;
                        }
                    case Action.BoothQuery:
                        {
                            Map Map = null;
                            if (!World.AllMaps.TryGetValue(Player.Map, out Map))
                                return;

                            if (!Map.Entities.ContainsKey(pMsg->UniqId))
                                return;

                            Booth Booth = (Map.Entities[pMsg->UniqId] as Booth);
                            if (!MyMath.CanSee(Player.X, Player.Y, Booth.X, Booth.Y, MyMath.BIG_RANGE))
                                return;

                            Booth.SendItems(Player);
                            break;
                        }
                    case Action.BoothAdd:
                        {
                            if (Player.Booth == null)
                                return;

                            if (pMsg->Param > Player._MAX_MONEYLIMIT)
                                return;

                            if (!Player.Items.ContainsKey(pMsg->UniqId))
                                return;

                            if (Player.Booth.AddItem(pMsg->UniqId, pMsg->Param, true))
                                Player.Send(Buffer);
                            break;
                        }
                    case Action.BoothDel:
                        {
                            if (Player.Booth == null)
                                return;

                            if (Player.Booth.UniqId != pMsg->Param)
                                return;

                            Player.Booth.DelItem(pMsg->UniqId);
                            break;
                        }
                    case Action.BoothBuy:
                        {
                            Map Map = null;
                            if (!World.AllMaps.TryGetValue(Player.Map, out Map))
                                return;

                            if (!Map.Entities.ContainsKey(pMsg->Param))
                                return;

                            Booth Booth = (Map.Entities[pMsg->Param] as Booth);
                            if (!MyMath.CanSee(Player.X, Player.Y, Booth.X, Booth.Y, MyMath.BIG_RANGE))
                                return;

                            Booth.BuyItem(Player, pMsg->UniqId);
                            break;
                        }
                    case Action.CompleteTask:
                        {
                            Client.Send(Buffer);
                            break;
                        }
                    case Action.Enchant:
                        {
                            if (!Player.IsAlive())
                            {
                                Player.SendSysMsg(Client.GetStr("STR_DIE"));
                                return;
                            }

                            Item Item = Player.GetItemByUID(pMsg->UniqId);
                            if (Item == null)
                            {
                                Player.SendSysMsg(Client.GetStr("STR_ITEM_NOT_FOUND"));
                                return;
                            }

                            if (Item.Position != 0)
                                return;

                            if (Item.Enchant >= 255)
                                return;

                            Item Gem = Player.GetItemByUID(pMsg->Param);
                            if (Gem == null)
                            {
                                Player.SendSysMsg(Client.GetStr("STR_ITEM_NOT_FOUND"));
                                return;
                            }

                            if (Gem.Position != 0 || (Gem.Id / 100000) != 7)
                                return;

                            Byte GemID = (Byte)(Gem.Id % 100);
                            Player.DelItem(Gem, true);

                            Byte Enchant = 0;
                            if (GemID % 10 == 1) //Normal
                                Enchant = (Byte)MyMath.Generate(1, 59);
                            else if (GemID == 2 || GemID == 52 || GemID == 62) //Reffined (Phoenix/Violet/Moon)
                                Enchant = (Byte)MyMath.Generate(60, 109);
                            else if (GemID == 22 || GemID == 42 || GemID == 72) //Reffined (Fury/Kylin/Tortoise)
                                Enchant = (Byte)MyMath.Generate(40, 89);
                            else if (GemID == 12) //Reffined (Dragon)
                                Enchant = (Byte)MyMath.Generate(100, 159);
                            else if (GemID == 32) //Reffined (Rainbow)
                                Enchant = (Byte)MyMath.Generate(80, 129);
                            else if (GemID == 3 || GemID == 33 || GemID == 73) //Super (Phoenix/Rainbow/Tortoise)
                                Enchant = (Byte)MyMath.Generate(170, 229);
                            else if (GemID == 53 || GemID == 63) //Super (Violet/Moon)
                                Enchant = (Byte)MyMath.Generate(140, 199);
                            else if (GemID == 13) //Reffined (Dragon)
                                Enchant = (Byte)MyMath.Generate(200, 255);
                            else if (GemID == 23) //Reffined (Fury)
                                Enchant = (Byte)MyMath.Generate(90, 149);
                            else if (GemID == 43) //Reffined (Kylin)
                                Enchant = (Byte)MyMath.Generate(70, 119);

                            Player.Send(MsgItem.Create(pMsg->UniqId, Enchant, Action.Enchant));

                            if (Enchant > Item.Enchant)
                            {
                                Item.Enchant = Enchant;
                                Player.Send(MsgItemInfo.Create(Item, MsgItemInfo.Action.Update));
                            }
                            break;
                        }
                    case Action.BoothAddCPs:
                        {
                            if (Player.Booth == null)
                                return;

                            if (pMsg->Param > Player._MAX_MONEYLIMIT)
                                return;

                            if (Player.GetItemByUID(pMsg->UniqId) == null)
                                return;

                            if (Player.Booth.AddItem(pMsg->UniqId, pMsg->Param, false))
                                Player.Send(Buffer);
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Msg[{0}], Action[{1}] not implemented yet!", pMsg->Header.Type, (Int16)pMsg->Action);
                            break;
                        }
                }
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }
    }
}
