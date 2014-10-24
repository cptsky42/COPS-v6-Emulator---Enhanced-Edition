// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.Collections.Generic;
using COServer.Network;

namespace COServer.Entities
{
    public class Booth : NPC
    {
        const Int32 BOOTH_SIZE = 18;
        const Int32 BOOTH_LOOK = 400;	// look for booth

        private struct BoothItem
        {
            public Int32 UniqId;
            public Int32 Price;
            public Boolean Money;
        }

        private Int32 OwnerUID;
        private Player User;
        private Dictionary<Int32, BoothItem> Items;
        private String CryOut;

        public static Booth Create(Player Player, UInt16 X, UInt16 Y)
        {
            Player.Booth = null;

            Map Map = null;
            if (!World.AllMaps.TryGetValue(Player.Map, out Map))
                return null;

            Int32 UniqId = -1;
            foreach (Object Object in Map.Entities.Values)
            {
                NPC Npc = (Object as NPC);
                if (Npc == null)
                    continue;

                if ((Npc.X + 3 == X) && (Npc.Y == Y))
                    UniqId = 610000 + Npc.UniqId;
            }

            if (UniqId == -1)
                return null;

            Booth Booth = new Booth(UniqId, Player, X, Y);

            Map.AddEntity(Booth);
            World.BroadcastMapMsg(Map.UniqId, MsgNpcInfo.Create(Booth, true));
            Player.Booth = Booth;
            return Booth;
        }

        public Booth(Int32 UniqId, Player Owner, UInt16 X, UInt16 Y)
            : base(UniqId, Owner.Name, (Byte)NpcType.Booth, (Int16)(BOOTH_LOOK + Owner.Direction), Owner.Map, X, Y, 0, 0)
        {
            OwnerUID = Owner.UniqId;
            User = Owner;
            Items = new Dictionary<Int32, BoothItem>(BOOTH_SIZE);
            CryOut = "";
        }

        ~Booth()
        {
            User = null;
            Items.Clear();
            Items = null;
            CryOut = null;
        }

        public void SetCryOut(String Words)
        {
            CryOut = Words;
            World.BroadcastRoomMsg(User, MsgTalk.Create(User.Name, "ALLUSERS", CryOut, MsgTalk.Channel.CryOut, 0xFFFFFF), true);
        }

        public Boolean AddItem(Int32 UniqId, Int32 Value, Boolean Money)
        {
            if (UniqId == 0 || Value == 0)
                return false;

            if (Value > Player._MAX_MONEYLIMIT)
                return false;

            if (Items.Count >= BOOTH_SIZE)
            {
                User.SendSysMsg(STR.Get("STR_BOOTH_FULL"));
                return false;
            }

            if (!User.Items.ContainsKey(UniqId))
                return false;

            if (Items.ContainsKey(UniqId))
                return false;

            Items.Add(UniqId, new BoothItem() { UniqId = UniqId, Price = Value, Money = Money });
            World.BroadcastRoomMsg(User, MsgItem.Create(UniqId, Value, (Money ? MsgItem.Action.BoothAdd : MsgItem.Action.BoothAddCPs)), false);
            return true;
        }

        public Boolean DelItem(Int32 UniqId)
        {
            if (UniqId == 0)
                return false;

            if (!Items.ContainsKey(UniqId))
                return false;

            lock (Items) { Items.Remove(UniqId); }
            World.BroadcastRoomMsg(User, MsgItem.Create(UniqId, UniqIdValue, MsgItem.Action.BoothDel), true);
            return true;
        }

        public Boolean BuyItem(Player Buyer, Int32 ItemUID)
        {
            if (ItemUID == 0)
                return false;

            if (!World.AllPlayers.ContainsKey(User.UniqId))
                return false;

            if (OwnerUID == Buyer.UniqId)
                return false;

            if (!Items.ContainsKey(ItemUID))
                return false;

            Item Item = User.GetItemByUID(ItemUID);
            if (Item == null)
            {
                DelItem(ItemUID);
                return false;
            }

            BoothItem BoothItem = Items[ItemUID];

            if (BoothItem.Price <= 0)
                return false;

            if (BoothItem.Money)
            {
                if (Buyer.Money < BoothItem.Price)
                {
                    Buyer.SendSysMsg(STR.Get("STR_NOT_SO_MUCH_MONEY"));
                    return false;
                }
            }
            else
            {
                if (Buyer.CPs < BoothItem.Price)
                {
                    Buyer.SendSysMsg(STR.Get("STR_NOT_SO_MUCH_MONEY"));
                    return false;
                }
            }

            if (Buyer.ItemInInventory() > 39)
            {
                Buyer.SendSysMsg(STR.Get("STR_FULL_CANNOT_PICK"));
                return false;
            }

            DelItem(ItemUID);
            if (BoothItem.Money)
            {
                Buyer.Money -= BoothItem.Price;
                User.Money += BoothItem.Price;

                Buyer.Send(MsgUserAttrib.Create(Buyer, Buyer.Money, MsgUserAttrib.Type.Money));
                User.Send(MsgUserAttrib.Create(User, User.Money, MsgUserAttrib.Type.Money));
            }
            else
            {
                Buyer.CPs -= BoothItem.Price;
                User.CPs += BoothItem.Price;

                Buyer.Send(MsgUserAttrib.Create(Buyer, Buyer.CPs, MsgUserAttrib.Type.CPs));
                User.Send(MsgUserAttrib.Create(User, User.CPs, MsgUserAttrib.Type.CPs));
            }

            Database.Save(Buyer);
            Database.Save(User);

            User.DelItem(Item, true);
            Buyer.AddItem(Item, true);

            Program.Log(String.Format("Booth: {0} -> {1}: {2}{3} for {4} ({5}).",
                User.UniqId, Buyer.UniqId, BoothItem.Price, (BoothItem.Money ? "$" : "CPs"), BoothItem.UniqId, 
                String.Format("{0}:{1}-{2}-{3}-{4}-{5}-{6}-{7}-{8}", 
                ItemHandler.GetName(Item.Id), Item.Id, Item.Craft, Item.Bless, Item.Enchant, Item.Gem1, Item.Gem2, Item.Attr, Item.Restrain)));

            User.SendSysMsg(String.Format(STR.Get("STR_BOOTH_BUY"),
                 Buyer.Name, BoothItem.Price, (BoothItem.Money ? "$" : "CPs"), 
                 ItemHandler.GetName(Item.Id)));
            return true;
        }

        public void SendItems(Player Player)
        {
            if (!World.AllPlayers.ContainsKey(User.UniqId))
                return;

            foreach (BoothItem BoothItem in Items.Values)
            {
                Item Item = User.GetItemByUID(BoothItem.UniqId);
                if (Item == null)
                {
                    DelItem(BoothItem.UniqId);
                    continue;
                }
                Player.Send(MsgItemInfoEx.Create(UniqId, Item, BoothItem.Price, 
                    (BoothItem.Money ? MsgItemInfoEx.Action.Booth : MsgItemInfoEx.Action.BoothCPs)));
            }
            Player.Send(MsgAction.Create(Player, UniqId, MsgAction.Action.DestroyBooth));
        }

        public void SendShow(Player Player)
        {
            Player.Send(MsgNpcInfo.Create(this, true));
            Player.Send(MsgTalk.Create(User.Name, Player.Name, CryOut, MsgTalk.Channel.CryOut, 0xFFFFFF));
        }

        public void Destroy()
        {
            User.Booth = null;

            Map CMap = null;
            if (World.AllMaps.TryGetValue(Map, out CMap))
                CMap.DelEntity(this);

            World.BroadcastMapMsg(Map, MsgAction.Create(this, 0, MsgAction.Action.LeaveMap));

            foreach (Int32 ItemUID in Items.Keys)
                User.Send(MsgItem.Create(ItemUID, UniqId, MsgItem.Action.BoothDel));
        }
    }
}
