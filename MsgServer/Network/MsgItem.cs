// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2010 - 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.Runtime.CompilerServices;
using COServer.Entities;

[assembly: InternalsVisibleTo("COServer.Network.Msg")]

namespace COServer.Network
{
    public class MsgItem : Msg
    {
        /// <summary>
        /// This is a "constant" that the child must override.
        /// It is the type of the message as specified in NetworkDef.cs file.
        /// </summary>
        protected override UInt16 _TYPE { get { return MSG_ITEM; } }

        public enum Action : uint
        {
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
        };

        //--------------- Internal Members ---------------
        private Int32 __UniqId = 0;
        private Int32 __Data = 0;
        private Action __Action = (Action)0;
        private Int32 __Timestamp = 0;
        //------------------------------------------------

        public Int32 UniqId
        {
            get { return __UniqId; }
            set { __UniqId = value; WriteInt32(4, value); }
        }

        public Int32 Data
        {
            get { return __Data; }
            set { __Data = value; WriteInt32(8, value); }
        }

        public UInt16 PosX
        {
            get { return (UInt16)((__Data >> 16) & 0x0000FFFFU); }
            set { __Data = (Int32)((value << 16) | (__Data & 0x0000FFFFU)); WriteUInt16(8, value); }
        }

        public UInt16 PosY
        {
            get { return (UInt16)(__Data & 0x0000FFFFU); }
            set { __Data = (Int32)((__Data & 0xFFFF0000U) | (value)); WriteUInt16(8, value); }
        }

        public Action _Action
        {
            get { return __Action; }
            set { __Action = value; WriteUInt32(12, (UInt32)value); }
        }

        public Int32 Timestamp
        {
            get { return __Timestamp; }
            set { __Timestamp = value; WriteInt32(16, value); }
        }

        /// <summary>
        /// Create a message object from the specified buffer.
        /// </summary>
        /// <param name="aBuf">The buffer containing the message.</param>
        /// <param name="aIndex">The index where the message is starting in the buffer.</param>
        /// <param name="aLength">The length of the message.</param>
        internal MsgItem(Byte[] aBuf, int aIndex, int aLength)
            : base(aBuf, aIndex, aLength)
        {
            __UniqId = BitConverter.ToInt32(mBuf, 4);
            __Data = BitConverter.ToInt32(mBuf, 8);
            __Action = (Action)BitConverter.ToUInt32(mBuf, 12);
            __Timestamp = BitConverter.ToInt32(mBuf, 16);
        }

        public MsgItem(Int32 aUniqId, Int32 aData, Action aAction)
            : base(20)
        {
            UniqId = aUniqId;
            Data = aData;
            _Action = aAction;
            Timestamp = Environment.TickCount;
        }

        public MsgItem(Int32 aUniqId, UInt32 aData, Action aAction)
            : base(20)
        {
            UniqId = aUniqId;
            Data = (Int32)aData;
            _Action = aAction;
            Timestamp = Environment.TickCount;
        }

        /// <summary>
        /// Process the message for the specified client.
        /// </summary>
        /// <param name="aClient">The client who sent the message.</param>
        public override void Process(Client aClient)
        {
            try
            {
                if (aClient == null || aClient.Player == null)
                    return;

                Player player = aClient.Player;

                switch (_Action)
                {
                    case Action.Buy:
                        {
                            if (!player.IsAlive())
                            {
                                player.SendSysMsg(StrRes.STR_DIE);
                                return;
                            }

                            if (UniqId != 2888) //VIP Shop
                            {
                                NPC Npc = null;
                                if (!World.AllNPCs.TryGetValue(UniqId, out Npc))
                                    return;

                                if (!Npc.IsShopNpc())
                                    return;

                                if (player.Map != Npc.Map)
                                    return;

                                if (!MyMath.CanSee(player.X, player.Y, Npc.X, Npc.Y, 17))
                                    return;
                            }

                            ShopInfo Shop = new ShopInfo();
                            if (!Database.AllShops.TryGetValue(UniqId, out Shop))
                                return;

                            if (!Shop.Items.Contains(Data))
                                return;

                            if (player.ItemInInventory() + 1 > 40)
                            {
                                player.SendSysMsg(StrRes.STR_FULL_CANNOT_BUY);
                                return;
                            }

                            Item.Info Info = new Item.Info();
                            if (!Database.AllItems.TryGetValue(Data, out Info))
                                return;

                            Byte Craft = 0;
                            if ((Int32)(Data / 10) == 73000)
                                Craft = (Byte)(Data % 10);

                            UInt16 Dura = ItemHandler.GetMaxDura(Data);

                            UInt32 Money = Math.Min(Player._MAX_MONEYLIMIT, Info.Price);
                            if (player.Money < Money)
                            {
                                player.SendSysMsg(StrRes.STR_NOT_SO_MUCH_MONEY);
                                return;
                            }

                            player.Money -= Money;
                            player.Send(new MsgUserAttrib(player, player.Money, MsgUserAttrib.AttributeType.Money));

                            Item Item = Item.Create(player.UniqId, 0, Data, Craft, 0, 0, 0, 0, 2, 0, Dura, Dura);
                            if (Item != null)
                                player.AddItem(Item, true);

                            Database.Save(player, true);
                            break;
                        }
                    case Action.Sell:
                        {
                            if (!player.IsAlive())
                            {
                                player.SendSysMsg(StrRes.STR_DIE);
                                return;
                            }

                            NPC Npc = null;
                            if (!World.AllNPCs.TryGetValue(UniqId, out Npc))
                                return;

                            if (!Npc.IsShopNpc())
                                return;

                            if (player.Map != Npc.Map)
                                return;

                            if (!MyMath.CanSee(player.X, player.Y, Npc.X, Npc.Y, 17))
                                return;

                            Item Item = player.GetItemByUID(Data);
                            if (Item == null)
                            {
                                player.SendSysMsg(StrRes.STR_ITEM_NOT_FOUND);
                                return;
                            }

                            Item.Info Info = new Item.Info();
                            Database.AllItems.TryGetValue(Item.Type, out Info);

                            if (!Info.IsSellEnable())
                            {
                                player.SendSysMsg(StrRes.STR_NOT_SELL_ENABLE);
                                return;
                            }

                            UInt32 Money = (UInt32)(Info.Price / 3);
                            Money = (UInt32)((Double)Money * ((Double)Item.CurDura / (Double)Item.MaxDura));

                            if (Money < 0 || Money > Player._MAX_MONEYLIMIT)
                                Money = 0;

                            if (player.Money + Money > Player._MAX_MONEYLIMIT)
                            {
                                player.SendSysMsg(StrRes.STR_TOOMUCH_MONEY);
                                return;
                            }

                            player.DelItem(Data, true);

                            player.Money += Money;
                            player.Send(new MsgUserAttrib(player, player.Money, MsgUserAttrib.AttributeType.Money));
                            break;
                        }
                    case Action.Drop:
                        {
                            if (!MyMath.CanSee(player.X, player.Y, PosX, PosY, MyMath.USERDROP_RANGE))
                                return;

                            if (!player.IsAlive())
                                return;

                            if (!player.Map.GetFloorAccess(PosX, PosY))
                                return;

                            Item Item = player.GetItemByUID(UniqId);

                            if (Item == null)
                                return;

                            player.DelItem(Item, true);
                            FloorItem FloorItem = new FloorItem(Item, 0, 0, player.Map, PosX, PosY);
                            World.FloorThread.AddToQueue(FloorItem);
                            break;
                        }
                    case Action.Use:
                        {
                            if (!player.IsAlive())
                            {
                                player.SendSysMsg(StrRes.STR_DIE);
                                return;
                            }

                            Item Item = player.GetItemByUID(UniqId);
                            if (Item == null)
                                return;

                            Item.Info Info;
                            if (!Database.AllItems.TryGetValue(Item.Type, out Info))
                                return;

                            if ((UInt16)(Item.Type / 10000) == 105)
                                Data = 5;

                            if ((UInt16)(Item.Type / 10000) == 105)
                            {
                                Item Bow = player.GetItemByPos(4);
                                if (Bow == null || Bow.Type / 1000 != 500)
                                    return;
                            }

                            if (Data != 0)
                            {
                                Byte Sex = (Byte)((player.Look / 1000) - ((player.Look / 1000) - ((player.Look / 1000) % 10)));
                                Byte Job = player.Profession;
                                Byte Weapon = (Byte)((Item.Type - (Item.Type % 100000)) / 100000);
                                UInt16 WeaponSkill = (UInt16)((Item.Type - (Item.Type % 1000)) / 1000);

                                if (player.Profession > 129 && player.Profession < 136)
                                    Job = (Byte)(player.Profession + 60);
                                else if (player.Profession > 139 && player.Profession < 146)
                                    Job = (Byte)(player.Profession + 50);
                                else if (player.Profession > 99 && player.Profession < 116)
                                    Job = (Byte)(player.Profession + 90);

                                //Equip
                                if (Info.RequiredLevel > player.Level)
                                    return;

                                if (Info.RequiredSex != 0 && Info.RequiredSex != Sex)
                                    return;

                                if (Info.RequiredLevel > 70 || (player.Metempsychosis == 0 && player.Level < 70))
                                {
                                    if (Info.RequiredProfession != 0 && Info.RequiredProfession / 10 != Job / 10 && Info.RequiredProfession > Job)
                                        return;

                                    if (Info.RequiredForce > player.Strength)
                                        return;

                                    if (Info.RequiredSpeed > player.Agility)
                                        return;

                                    if (Info.RequiredHealth > player.Vitality)
                                        return;

                                    if (Info.RequiredSoul > player.Spirit)
                                        return;
                                }

                                if (Info.RequiredWeaponSkill != 0)
                                    if (Weapon == 4 || Weapon == 5)
                                        if (player.GetWeaponSkillByType(WeaponSkill) == null
                                            || Info.RequiredWeaponSkill > player.GetWeaponSkillByType(WeaponSkill).Level)
                                            return;

                                if (Weapon == 9 && !(Job > 19 && Job < 26))
                                    return;

                                if (Item.Type == 137310 && !(player.IsGM || player.IsPM)) //GM Robe
                                    return;

                                if (player.GetItemByPos(5) != null)
                                    if (((Byte)(Item.Type / 100000) == 5 || (Int16)(Item.Type / 1000) == 421))
                                    {
                                        Item LeftHand = player.GetItemByPos(5);
                                        if (Item.Type / 1000 == 500 && (UInt16)(LeftHand.Type / 10000) != 105)
                                            player.GetItemByPos(5).Position = 0;
                                        else if (Item.Type / 1000 != 500)
                                            player.GetItemByPos(5).Position = 0;
                                    }

                                if (player.GetItemByPos((Byte)Data) != null)
                                    player.GetItemByPos((Byte)Data).Position = 0;
                                Item.Position = (Byte)Data;

                                player.CalcMaxHP();
                                player.CalcMaxMP();
                                MyMath.GetEquipStats(player);

                                player.Send(new MsgItem(UniqId, Item.Position, Action.Equip));
                                World.BroadcastRoomMsg(player, new MsgPlayer(player), false);
                            }
                            else
                            {
                                if (!Item.Use())
                                    ItemHandler.Use(player, Item);
                            }
                            break;
                        }
                    case Action.Unequip:
                        {
                            if (!player.IsAlive())
                            {
                                player.SendSysMsg(StrRes.STR_DIE);
                                return;
                            }

                            Item Item = player.GetItemByUID(UniqId);
                            if (Item == null)
                                return;

                            if (Item.Position != (Byte)Data)
                                return;

                            Item.Position = 0;

                            player.CalcMaxHP();
                            player.CalcMaxMP();
                            MyMath.GetEquipStats(player);

                            player.Send(this);
                            World.BroadcastRoomMsg(player, new MsgPlayer(player), false);
                            break;
                        }
                    case Action.QueryMoneySaved:
                        {
                            NPC Npc = null;
                            if (!World.AllNPCs.TryGetValue(UniqId, out Npc))
                                return;

                            if (player.Map != Npc.Map)
                                return;

                            if (!MyMath.CanSee(player.X, player.Y, Npc.X, Npc.Y, 17))
                                return;

                            if (!Npc.IsStorageNpc())
                                return;

                            player.Send(new MsgItem(UniqId, player.WHMoney, Action.QueryMoneySaved));
                            break;
                        }
                    case Action.SaveMoney:
                        {
                            NPC Npc = null;
                            if (!World.AllNPCs.TryGetValue(UniqId, out Npc))
                                return;

                            if (player.Map != Npc.Map)
                                return;

                            if (!MyMath.CanSee(player.X, player.Y, Npc.X, Npc.Y, 17))
                                return;

                            if (!Npc.IsStorageNpc())
                                return;

                            if (player.WHMoney + Data > Player._MAX_MONEYLIMIT)
                                return;

                            if (player.Money >= Data)
                            {
                                player.Money -= (UInt32)Data;
                                player.WHMoney += (UInt32)Data;
                                player.Send(new MsgUserAttrib(player, player.Money, MsgUserAttrib.AttributeType.Money));
                            }
                            break;
                        }
                    case Action.DrawMoney:
                        {
                            NPC Npc = null;
                            if (!World.AllNPCs.TryGetValue(UniqId, out Npc))
                                return;

                            if (player.Map != Npc.Map)
                                return;

                            if (!MyMath.CanSee(player.X, player.Y, Npc.X, Npc.Y, 17))
                                return;

                            if (!Npc.IsStorageNpc())
                                return;

                            if (player.Money + Data > Player._MAX_MONEYLIMIT)
                                return;

                            if (player.WHMoney >= Data)
                            {
                                player.WHMoney -= (UInt32)Data;
                                player.Money += (UInt32)Data;
                                player.Send(new MsgUserAttrib(player, player.Money, MsgUserAttrib.AttributeType.Money));
                            }
                            break;
                        }
                    case Action.DropMoney:
                        {
                            if (!MyMath.CanSee(player.X, player.Y, PosX, PosY, MyMath.USERDROP_RANGE))
                                return;

                            if (player.Money < UniqId)
                                return;

                            if (UniqId < 100)
                                return;

                            if (!player.IsAlive())
                            {
                                player.SendSysMsg(StrRes.STR_DIE_DROP_MONEY);
                                return;
                            }

                            Item Item = null;
                            if (UniqId <= 10) //Silver
                                Item = Item.CreateMoney(1090000);
                            else if (UniqId <= 100) //Sycee
                                Item = Item.CreateMoney(1090010);
                            else if (UniqId <= 1000) //Gold
                                Item = Item.CreateMoney(1090020);
                            else if (UniqId <= 2000) //GoldBullion
                                Item = Item.CreateMoney(1091000);
                            else if (UniqId <= 5000) //GoldBar
                                Item = Item.CreateMoney(1091010);
                            else if (UniqId > 5000) //GoldBars
                                Item = Item.CreateMoney(1091020);
                            else //Error
                                Item = Item.CreateMoney(1090000);

                            if (Item != null)
                            {
                                player.Money -= (UInt32)UniqId;
                                player.Send(new MsgUserAttrib(player, player.Money, MsgUserAttrib.AttributeType.Money));
                                Database.Save(player, true);

                                player.SendSysMsg(StrRes.STR_DROP_MONEY_SUCC, UniqId.ToString());
                                FloorItem FloorItem = new FloorItem(Item, (UInt32)UniqId, 0, player.Map, PosX, PosY);
                                World.FloorThread.AddToQueue(FloorItem);
                            }
                            else
                                player.SendSysMsg(StrRes.STR_MAKE_MONEY_FAILED);
                            break;
                        }
                    case Action.Repair:
                        {
                            if (!player.IsAlive())
                            {
                                player.SendSysMsg(StrRes.STR_DIE);
                                return;
                            }

                            Item Item = player.GetItemByUID(UniqId);
                            if (Item == null)
                            {
                                player.SendSysMsg(StrRes.STR_ITEM_NOT_FOUND);
                                return;
                            }

                            if (Item.Position != 0)
                                return;

                            Item.Info Info = new Item.Info();
                            Database.AllItems.TryGetValue(Item.Type, out Info);

                            if (!Info.IsRepairEnable() || Item.CurDura == Item.MaxDura)
                            {
                                player.SendSysMsg(StrRes.STR_REPAIR_FAILED);
                                return;
                            }

                            if (Item.CurDura > Item.MaxDura)
                            {
                                Item.CurDura = Item.MaxDura;
                                player.Send(new MsgItem(Item.Id, Item.CurDura, Action.SynchroAmount));
                                return;
                            }

                            if (Item.CurDura > 0)
                            {
                                UInt32 Money = Item.CalcRepairMoney();
                                if (player.Money < Money)
                                {
                                    player.SendSysMsg(StrRes.STR_NOT_SO_MUCH_MONEY);
                                    return;
                                }

                                player.Money -= Money;
                                player.Send(new MsgUserAttrib(player, player.Money, MsgUserAttrib.AttributeType.Money));

                                Item.CurDura = Item.MaxDura;
                                player.Send(new MsgItem(Item.Id, Item.CurDura, Action.SynchroAmount));
                            }
                            else
                            {
                                if (!player.InventoryContains(1088001, 5))
                                {
                                    player.SendSysMsg(StrRes.STR_REPAIR_FAILED);
                                    return;
                                }

                                for (Byte i = 0; i < 5; i++)
                                    player.DelItem(player.GetItemById(1088001), true);

                                Item.CurDura = Item.MaxDura;
                                player.Send(new MsgItemInfo(Item, MsgItemInfo.Action.Update));
                            }
                            break;
                        }
                    case Action.Improve:
                        {
                            if (!player.IsAlive())
                            {
                                player.SendSysMsg(StrRes.STR_DIE);
                                return;
                            }

                            Item Item = player.GetItemByUID(UniqId);
                            if (Item == null)
                            {
                                player.SendSysMsg(StrRes.STR_ITEM_NOT_FOUND);
                                return;
                            }

                            if (Item.Position != 0 || Item.Type % 10 == 9)
                                return;

                            if (Item.CurDura != Item.MaxDura)
                            {
                                player.SendSysMsg(StrRes.STR_ITEM_DAMAGED);
                                return; 
                            }

                            Item Treasure = player.GetItemByUID(Data);
                            if (Treasure == null)
                            {
                                player.SendSysMsg(StrRes.STR_ITEM_NOT_FOUND);
                                return;
                            }

                            if (Treasure.Position != 0 || Treasure.Type != 1088000)
                                return;

                            Double Chance;
                            Int32 NextId;
                            if (!ItemHandler.GetUpQualityInfo(Item, out Chance, out NextId))
                                return;

                            player.DelItem(Treasure, true);
                            if (!MyMath.Success(Chance))
                            {
                                Item.CurDura /= 2;
                                Item.MaxDura -= 15;
                                player.SendSysMsg(StrRes.STR_IMPROVE_FAILED);
                            }
                            else
                            {
                                if (Item.FirstGem == 0 && MyMath.Success(2.00))
                                    Item.FirstGem = 255;
                                else if (Item.SecondGem == 0 && MyMath.Success(1.00))
                                    Item.SecondGem = 255;

                                Item.Type = NextId;
                                Item.CurDura = ItemHandler.GetMaxDura(NextId);
                                Item.MaxDura = ItemHandler.GetMaxDura(NextId);

                                Byte DuraEffect = 0;
                                if (Item.FirstGem - (Item.FirstGem % 10) == 40) //Kylin
                                {
                                    DuraEffect += (Byte)(Item.FirstGem % 10);
                                    if (Item.FirstGem % 10 == 3)
                                        DuraEffect++;
                                }

                                if (Item.SecondGem - (Item.SecondGem % 10) == 40) //Kylin
                                {
                                    DuraEffect += (Byte)(Item.SecondGem % 10);
                                    if (Item.SecondGem % 10 == 3)
                                        DuraEffect++;
                                }

                                Double Bonus = 1.0;
                                Bonus += 0.5 * DuraEffect;

                                Item.MaxDura = (UInt16)((Double)Item.MaxDura * Bonus);
                                Item.CurDura = Item.MaxDura;

                                player.SendSysMsg(StrRes.STR_IMPROVE_SUCCEED);
                            }

                            player.Send(new MsgItemInfo(Item, MsgItemInfo.Action.Update));
                            break;
                        }
                    case Action.UpLevel:
                        {
                            if (!player.IsAlive())
                            {
                                player.SendSysMsg(StrRes.STR_DIE);
                                return;
                            }

                            Item Item = player.GetItemByUID(UniqId);
                            if (Item == null)
                            {
                                player.SendSysMsg(StrRes.STR_ITEM_NOT_FOUND);
                                return;
                            }

                            if (Item.Position != 0)
                                return;

                            if (Item.CurDura != Item.MaxDura)
                            {
                                player.SendSysMsg(StrRes.STR_ITEM_DAMAGED);
                                return;
                            }

                            Item Treasure = player.GetItemByUID(Data);
                            if (Treasure == null)
                            {
                                player.SendSysMsg(StrRes.STR_ITEM_NOT_FOUND);
                                return;
                            }

                            if (Treasure.Position != 0 || Treasure.Type != 1088001)
                                return;

                            Double Chance;
                            Int32 NextId;
                            if (!ItemHandler.GetUpLevelInfo(Item, out Chance, out NextId))
                                return;

                            player.DelItem(Treasure, true);
                            if (!MyMath.Success(Chance))
                            {
                                Item.CurDura /= 2;
                                Item.MaxDura -= 15;
                                player.SendSysMsg(StrRes.STR_IMPROVE_FAILED);
                            }
                            else
                            {
                                if (Item.FirstGem == 0 && MyMath.Success(1.50))
                                    Item.FirstGem = 255;
                                else if (Item.SecondGem == 0 && MyMath.Success(0.75))
                                    Item.SecondGem = 255;

                                Item.Type = NextId;
                                Item.CurDura = ItemHandler.GetMaxDura(NextId);
                                Item.MaxDura = ItemHandler.GetMaxDura(NextId);

                                Byte DuraEffect = 0;
                                if (Item.FirstGem - (Item.FirstGem % 10) == 40) //Kylin
                                {
                                    DuraEffect += (Byte)(Item.FirstGem % 10);
                                    if (Item.FirstGem % 10 == 3)
                                        DuraEffect++;
                                }

                                if (Item.SecondGem - (Item.SecondGem % 10) == 40) //Kylin
                                {
                                    DuraEffect += (Byte)(Item.SecondGem % 10);
                                    if (Item.SecondGem % 10 == 3)
                                        DuraEffect++;
                                }

                                Double Bonus = 1.0;
                                Bonus += 0.5 * DuraEffect;

                                Item.MaxDura = (UInt16)((Double)Item.MaxDura * Bonus);
                                Item.CurDura = Item.MaxDura;

                                player.SendSysMsg(StrRes.STR_IMPROVE_SUCCEED);
                            }

                            player.Send(new MsgItemInfo(Item, MsgItemInfo.Action.Update));
                            break;
                        }
                    case Action.BoothQuery:
                        {
                            if (!player.Map.Entities.ContainsKey(UniqId))
                                return;

                            Booth Booth = (player.Map.Entities[UniqId] as Booth);
                            if (!MyMath.CanSee(player.X, player.Y, Booth.X, Booth.Y, MyMath.NORMAL_RANGE))
                                return;

                            Booth.SendItems(player);
                            break;
                        }
                    case Action.BoothAdd:
                        {
                            if (player.Booth == null)
                                return;

                            if (Data > Player._MAX_MONEYLIMIT)
                                return;

                            if (!player.Items.ContainsKey(UniqId))
                                return;

                            if (player.Booth.AddItem(UniqId, (UInt32)Data))
                                player.Send(this);
                            break;
                        }
                    case Action.BoothDel:
                        {
                            if (player.Booth == null)
                                return;

                            if (player.Booth.UniqId != Data)
                                return;

                            player.Booth.DelItem(UniqId);
                            break;
                        }
                    case Action.BoothBuy:
                        {
                            if (!player.Map.Entities.ContainsKey(Data))
                                return;

                            Booth Booth = (player.Map.Entities[Data] as Booth);
                            if (!MyMath.CanSee(player.X, player.Y, Booth.X, Booth.Y, MyMath.NORMAL_RANGE))
                                return;

                            Booth.BuyItem(player, UniqId);
                            break;
                        }
                    case Action.CompleteTask:
                        {
                            aClient.Send(this);
                            break;
                        }
                    case Action.Enchant:
                        {
                            if (!player.IsAlive())
                            {
                                player.SendSysMsg(StrRes.STR_DIE);
                                return;
                            }

                            Item Item = player.GetItemByUID(UniqId);
                            if (Item == null)
                            {
                                player.SendSysMsg(StrRes.STR_ITEM_NOT_FOUND);
                                return;
                            }

                            if (Item.Position != 0)
                                return;

                            if (Item.Enchant >= 255)
                                return;

                            Item Gem = player.GetItemByUID(Data);
                            if (Gem == null)
                            {
                                player.SendSysMsg(StrRes.STR_ITEM_NOT_FOUND);
                                return;
                            }

                            if (Gem.Position != 0 || (Gem.Type / 100000) != 7)
                                return;

                            Byte GemID = (Byte)(Gem.Type % 100);
                            player.DelItem(Gem, true);

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

                            player.Send(new MsgItem(UniqId, Enchant, Action.Enchant));

                            if (Enchant > Item.Enchant)
                            {
                                Item.Enchant = Enchant;
                                player.Send(new MsgItemInfo(Item, MsgItemInfo.Action.Update));
                            }
                            break;
                        }
                    default:
                        {
                            sLogger.Error("Action {0} is not implemented for MsgItem.", (UInt32)_Action);
                            break;
                        }
                }
            }
            catch (Exception exc) { sLogger.Error(exc); }
        }
    }
}
