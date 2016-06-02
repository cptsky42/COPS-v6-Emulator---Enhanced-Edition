// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * COPS v6 Emulator

using System;
using System.Collections.Generic;
using COServer.Network;

namespace COServer.Entities
{
    public class Booth : NPC
    {
        const Int32 BOOTH_SIZE = 18;
        const UInt32 BOOTH_LOOK = 400;	// look for booth

        private struct BoothItem
        {
            public Int32 UniqId;
            public UInt32 Price;
        }

        private Int32 OwnerUID;
        private Player User;
        private Dictionary<Int32, BoothItem> Items;
        private String CryOut;

        public static Booth Create(Player Player, UInt16 X, UInt16 Y)
        {
            Player.Booth = null;

            GameMap Map = Player.Map;

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
            World.BroadcastMapMsg(Map.Id, new MsgNpcInfo(Booth, true));
            Player.Booth = Booth;
            return Booth;
        }

        public Booth(Int32 UniqId, Player Owner, UInt16 X, UInt16 Y)
            : base(UniqId, Owner.Name, (Byte)NpcType.Booth, BOOTH_LOOK + Owner.Direction, Owner.Map, X, Y, 0, 0)
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
            World.BroadcastRoomMsg(User, new MsgTalk(User.Name, "ALLUSERS", CryOut, Channel.CryOut, Color.White), true);
        }

        public Boolean AddItem(Int32 UniqId, UInt32 Value)
        {
            if (UniqId == 0 || Value == 0)
                return false;

            if (Value > Player._MAX_MONEYLIMIT)
                return false;

            if (Items.Count >= BOOTH_SIZE)
            {
                User.SendSysMsg(StrRes.STR_BOOTH_FULL);
                return false;
            }

            if (!User.Items.ContainsKey(UniqId))
                return false;

            if (Items.ContainsKey(UniqId))
                return false;

            Items.Add(UniqId, new BoothItem() { UniqId = UniqId, Price = Value });
            World.BroadcastRoomMsg(User, new MsgItem(UniqId, Value, MsgItem.Action.BoothAdd));
            return true;
        }

        public Boolean DelItem(Int32 UniqId)
        {
            if (UniqId == 0)
                return false;

            if (!Items.ContainsKey(UniqId))
                return false;

            lock (Items) { Items.Remove(UniqId); }
            World.BroadcastRoomMsg(User, new MsgItem(UniqId, mUID, MsgItem.Action.BoothDel), true);
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

            if (Buyer.Money < BoothItem.Price)
            {
                Buyer.SendSysMsg(StrRes.STR_NOT_SO_MUCH_MONEY);
                return false;
            }

            if (Buyer.ItemInInventory() > 39)
            {
                Buyer.SendSysMsg(StrRes.STR_FULL_CANNOT_PICK);
                return false;
            }

            DelItem(ItemUID);

            Buyer.Money -= BoothItem.Price;
            User.Money += BoothItem.Price;

            Buyer.Send(new MsgUserAttrib(Buyer, Buyer.Money, MsgUserAttrib.AttributeType.Money));
            User.Send(new MsgUserAttrib(User, User.Money, MsgUserAttrib.AttributeType.Money));

            Database.Save(Buyer, true);
            Database.Save(User, true);

            User.DelItem(Item, true);
            Buyer.AddItem(Item, true);

            User.SendSysMsg(StrRes.STR_BOOTH_BUY, Buyer.Name, BoothItem.Price, "$", Item.Name);
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
                Player.Send(new MsgItemInfoEx(UniqId, Item, BoothItem.Price, 
                    MsgItemInfoEx.Action.Booth));
            }
            Player.Send(new MsgAction(Player, UniqId, MsgAction.Action.DestroyBooth));
        }

        public void SendShow(Player Player)
        {
            Player.Send(new MsgNpcInfo(this, true));
            Player.Send(new MsgTalk(User.Name, Player.Name, CryOut, Channel.CryOut, Color.White));
        }

        public void Destroy()
        {
            User.Booth = null;

            Map.DelEntity(this);
            World.BroadcastMapMsg(Map.Id, new MsgAction(this, 0, MsgAction.Action.LeaveMap));

            foreach (Int32 ItemUID in Items.Keys)
                User.Send(new MsgItem(ItemUID, UniqId, MsgItem.Action.BoothDel));
        }
    }
}
