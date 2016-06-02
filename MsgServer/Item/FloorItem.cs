// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2010 - 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using COServer.Network;

namespace COServer
{
    /// <summary>
    /// Dropped item.
    /// </summary>
    public class FloorItem : MapObj
    {
        /// <summary>
        /// Unique ID of the item.
        /// </summary>
        public Int32 Id { get { return Item.Id; } }

        /// <summary>
        /// Associated item object.
        /// </summary>
        public Item Item { get; private set; }

        /// <summary>
        /// Associated money.
        /// </summary>
        public UInt32 Money { get; private set; }

        /// <summary>
        /// Unique ID of the owner.
        /// </summary>
        public Int32 OwnerUID { get; private set; }

        /// <summary>
        /// Map on which the item is.
        /// </summary>
        public GameMap Map { get; private set; }

        /// <summary>
        /// X coordinate of the object.
        /// </summary>
        public UInt16 X { get; private set; }

        /// <summary>
        /// Y coordinate of the object.
        /// </summary>
        public UInt16 Y { get; private set; }

        /// <summary>
        /// Time at which the item was dropped.
        /// </summary>
        public DateTime DroppedTime { get; private set; }

        /// <summary>
        /// Whether or not the item has been destroyed/picked.
        /// </summary>
        public Boolean Destroyed { get; private set; }

        /// <summary>
        /// Lock to synchronize the destruction.
        /// </summary>
        private object mLock = new object();

        /// <summary>
        /// Create a new floor item.
        /// </summary>
        /// <param name="aItem">The associated item on the floor.</param>
        /// <param name="aMoney">The associated money.</param>
        /// <param name="aOwnerUID">The unique ID of the owner.</param>
        /// <param name="aMap">The map on which the item is laying.</param>
        /// <param name="aX">The X coordinate of the item.</param>
        /// <param name="aY">The Y coordinate of the item.</param>
        public FloorItem(Item aItem, UInt32 aMoney, Int32 aOwnerUID, GameMap aMap, UInt16 aX, UInt16 aY)
        {
            aItem.OwnerUID = 0;
            aItem.Position = 254;

            Item = aItem;
            Money = aMoney;
            OwnerUID = aOwnerUID;

            DroppedTime = DateTime.UtcNow;
            Destroyed = false;

            lock (World.AllFloorItems) { World.AllFloorItems.Add(Id, this); }

            Map = aMap;
            X = aX;
            Y = aY;

            aMap.AddItem(this);
        }

        /// <summary>
        /// Remove the item from the map. Optionally, the item object will be deleted.
        /// </summary>
        /// <param name="aDelete">Whether or not the item object must be deleted.</param>
        /// <returns>True on success, false otherwise.</returns>
        public Boolean Destroy(Boolean aDelete)
        {
            bool destroy = false;

            lock (mLock)
            {
                if (!Destroyed)
                {
                    destroy = true;
                    Destroyed = true;
                }
            }

            if (destroy)
            {
                lock (World.AllFloorItems)
                {
                    if (World.AllFloorItems.ContainsKey(Id))
                        World.AllFloorItems.Remove(Id);
                }

                if (Map != null)
                    Map.DelItem(this);

                if (aDelete)
                    Item.Delete(Id);
            }

            return destroy;
        }
    }
}
