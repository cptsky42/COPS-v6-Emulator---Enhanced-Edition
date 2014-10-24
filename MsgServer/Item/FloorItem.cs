// * Created by Jean-Philippe Boivin
// * Copyright © 2010-2011
// * Logik. Project

using System;
using COServer.Network;

namespace COServer
{
    public class FloorItem
    {
        public Item Item;
        public Int32 Money;
        public Int32 OwnerUID;
        public Int16 Map;
        public UInt16 X;
        public UInt16 Y;
        public Int32 DroppedTime;
        public Boolean Destroyed;

        public Int32 UniqId { get { return Item.UniqId; } }

        public FloorItem(Item Item, Int32 Money, Int32 OwnerUID, Int16 Map, UInt16 X, UInt16 Y)
        {
            Item.OwnerUID = 0;
            Item.Position = 254;

            this.Item = Item;
            this.Money = Money;
            this.OwnerUID = OwnerUID;
            this.Map = Map;
            this.X = X;
            this.Y = Y;
            this.Destroyed = false;

            DroppedTime = Environment.TickCount;

            lock (World.AllFloorItems) { World.AllFloorItems.Add(UniqId, this); }

            Map CMap = null;
            if (World.AllMaps.TryGetValue(Map, out CMap))
                CMap.AddItem(this);

            World.BroadcastRoomMsg(this, MsgMapItem.Create(this, MsgMapItem.Action.Create));
        }

        ~FloorItem()
        {

        }

        public Boolean Destroy(Boolean Delete)
        {
            try
            {
                Destroyed = true;
                World.BroadcastRoomMsg(this, MsgMapItem.Create(this, MsgMapItem.Action.Delete));

                if (World.AllFloorItems.ContainsKey(UniqId))
                    lock (World.AllFloorItems) { World.AllFloorItems.Remove(UniqId); }

                Map CMap = null;
                if (World.AllMaps.TryGetValue(Map, out CMap))
                    CMap.DelItem(this);

                if (Delete)
                    Item.Delete(UniqId);
                return true;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return false; }
        }
    }
}
