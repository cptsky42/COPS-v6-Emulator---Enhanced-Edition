// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2010 - 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.Collections.Concurrent;
using System.Linq;
using COServer.Entities;
using COServer.Network;

namespace COServer
{
    /// <summary>
    /// List of all flags for the map type.
    /// </summary>
    [Flags]
    public enum MapFlags : uint
    {
        /// <summary>
        /// The map is normal.
        /// </summary>
        Normal = 0x0000,
        /// <summary>
        /// The map is a PK field.
        /// Players won't get PK points and their name won't flash.
        /// </summary>
        PkField = 0x0001,
        /// <summary>
        /// The map disable changing map with magic call (team member) ???
        /// </summary>
        ChgMapDisable = 0x0002, // magic call team member
        /// <summary>
        /// The map doesn't save the position. It saves the previous map.
        /// </summary>
        RecordDisable = 0x0004,
        /// <summary>
        /// The map doesn't allow PK.
        /// </summary>
        PkDisable = 0x0008,
        /// <summary>
        /// The map allows booth creation.
        /// </summary>
        BoothEnable = 0x0010,
        /// <summary>
        /// The map doesn't allow teams.
        /// </summary>
        TeamDisable = 0x0020,
        /// <summary>
        /// The map doesn't allow changing map by action. (Scrolls, etc)
        /// </summary>
        TeleportDisable = 0x0040, // chgmap by action
        /// <summary>
        /// The map is a syndicate map.
        /// </summary>
        SynMap = 0x0080,
        /// <summary>
        /// The map is a prison map. 
        /// </summary>
        PrisonMap = 0x0100,
        /// <summary>
        /// The map doesn't allow flying.
        /// </summary>
        WingDisable = 0x0200, // bowman fly disable
        /// <summary>
        /// The map is a family map.
        /// </summary>
        Family = 0x0400,
        /// <summary>
        ///  The map is a mine field.
        /// </summary>
        MineField = 0x0800,
        /// <summary>
        /// 
        /// </summary>
        CallNewbieDisable = 0x1000,
        /// <summary>
        /// Reborn here is enabled.
        /// </summary>
        RebornNowEnable = 0x2000,
        /// <summary>
        /// Newbie protection.
        /// </summary>
        NewbieProtect = 0x4000
    }

    public class GameMap
    {
        /// <summary>
        /// The specific information of a game map.
        /// Mostly loaded from the database.
        /// </summary>
        public struct Info
        {
            /// <summary>
            /// The unique ID of the Map Data.
            /// </summary>
            public UInt16 DocId;
            /// <summary>
            /// The type (bitfield) of the map.
            /// </summary>
            public UInt32 Type;
            /// <summary>
            /// The owner UID of the map.
            /// </summary>
            public UInt32 OwnerUID;
            /// <summary>
            /// The weather of the map.
            /// </summary>
            public WeatherType Weather;
            /// <summary>
            /// The main portal X-coord. It is used for reborn.
            /// </summary>
            public UInt16 PortalX;
            /// <summary>
            /// The main portal Y-coord. It is used for reborn.
            /// </summary>
            public UInt16 PortalY;
            /// <summary>
            /// The reborn map UID.
            /// </summary>
            public UInt32 RebornMap;
            /// <summary>
            /// The reborn portal UID. Zero corresponds to the main portal.
            /// </summary>
            public Int32 RebornPortal;
            /// <summary>
            /// The light in ARGB code.
            /// </summary>
            public UInt32 Light;
        }

        /// <summary>
        /// The entities on the map.
        /// </summary>
        public readonly ConcurrentDictionary<Int32, Entity> Entities = new ConcurrentDictionary<Int32, Entity>();
        public readonly ConcurrentDictionary<Int32, FloorItem> FloorItems = new ConcurrentDictionary<Int32, FloorItem>();

        /// <summary>
        /// The unique ID of the map.
        /// </summary>
        protected readonly UInt32 mId;
        /// <summary>
        /// The information of the map.
        /// </summary>
        private Info mInfo;
        /// <summary>
        /// The data of the map.
        /// </summary>
        private MapData mData;

        /// <summary>
        /// The unique ID of the map.
        /// </summary>
        public UInt32 Id { get { return mId; } }

        /// <summary>
        /// The width (number of cell) of the map.
        /// </summary>
        public UInt16 Width { get { return mData.Width; } }
        /// <summary>
        /// The height (number of cell) of the map.
        /// </summary>
        public UInt16 Height { get { return mData.Height; } }
        /// <summary>
        /// All the cells of the map.
        /// </summary>
        public UInt16[,] Cells { get { return mData.Cells; } }

        /// <summary>
        /// The unique ID of the Map Data.
        /// </summary>
        public UInt16 DocId { get { return mInfo.DocId; } }
        /// <summary>
        /// The type (bitfield) of the map.
        /// </summary>
        public UInt32 Type { get { return mInfo.Type; } set { mInfo.Type = value; } }
        /// <summary>
        /// The weather of the map.
        /// </summary>
        public WeatherType Weather { get { return mInfo.Weather; } }
        /// <summary>
        /// The main portal X-coord. It is used for reborn.
        /// </summary>
        public UInt16 PortalX { get { return mInfo.PortalX; } }
        /// <summary>
        /// The main portal Y-coord. It is used for reborn.
        /// </summary>
        public UInt16 PortalY { get { return mInfo.PortalY; } }
        /// <summary>
        /// The reborn map ID.
        /// </summary>
        public UInt32 RebornMap { get { return mInfo.RebornMap; } }
        /// <summary>
        /// The reborn portal ID. Zero corresponds to the main portal.
        /// </summary>
        public Int32 RebornPortal { get { return mInfo.RebornPortal; } }
        /// <summary>
        /// The light in ARGB code.
        /// </summary>
        public UInt32 Light { get { return mInfo.Light; } }

        public GameMap(UInt32 aMapId, GameMap.Info aInfo, MapData aData)
        {
            mId = aMapId;
            mInfo = aInfo;
            mData = aData;
        }

        public Boolean ContainsFlag(MapFlags aFlag) { return (Type & (uint)aFlag) == (uint)aFlag; }
        public void AddFlag(UInt32 aFlag) { mInfo.Type |= aFlag; }
        public void DelFlag(UInt32 aFlag) { mInfo.Type ^= aFlag; }

        public Boolean IsPkField() { return ContainsFlag(MapFlags.PkField); }
        public Boolean IsChangeMap_Disable() { return ContainsFlag(MapFlags.ChgMapDisable); }
        public Boolean IsRecord_Disable() { return ContainsFlag(MapFlags.RecordDisable); }
        public Boolean IsPk_Disable() { return ContainsFlag(MapFlags.PkDisable); }
        public Boolean IsBooth_Enable() { return ContainsFlag(MapFlags.BoothEnable); }
        public Boolean IsTeam_Disable() { return ContainsFlag(MapFlags.TeamDisable); }
        public Boolean IsTeleport_Disable() { return ContainsFlag(MapFlags.TeleportDisable); }
        public Boolean IsSynMap() { return ContainsFlag(MapFlags.SynMap); }
        public Boolean IsPrisonMap() { return ContainsFlag(MapFlags.PrisonMap); }
        public Boolean IsWing_Disable() { return ContainsFlag(MapFlags.WingDisable); }
        public Boolean IsFamily() { return ContainsFlag(MapFlags.Family); }
        public Boolean IsMineField() { return ContainsFlag(MapFlags.MineField); }
        public Boolean IsCallNewbie_Disable() { return ContainsFlag(MapFlags.CallNewbieDisable); }
        public Boolean IsRebornNow_Enable() { return ContainsFlag(MapFlags.RebornNowEnable); }
        public Boolean IsNewbieProtect() { return ContainsFlag(MapFlags.NewbieProtect); }

        public Boolean GetFloorAccess(UInt16 aX, UInt16 aY)
        {
            if (aX < mData.Width && aY < mData.Height)
                return (mData.Cells[aX, aY] & 0x0001) == 1;
            return false;
        }

        public UInt16 GetFloorAlt(UInt16 aX, UInt16 aY)
        {
            if (aX < mData.Width && aY < mData.Height)
                return mData.Cells[aX, aY];
            return 0;
        }

        /// <summary>
        /// Get the passage ID of the given coords.
        /// </summary>
        /// <param name="aX">The X coordinate of the passage.</param>
        /// <param name="aX">The Y coordinate of the passage.</param>
        /// <returns>The passage ID if found, -1 otherwise.</returns>
        public int GetPassage(UInt16 aX, UInt16 aY)
        {
            int passageId = -1;

            try { passageId = mData.Passages.First(p => p.PosX == aX && p.PosY == aY).Index; }
            catch (InvalidOperationException) { }

            return passageId;
        }

        public Boolean AddEntity(Entity aEntity)
        {
            if (aEntity == null)
                return false;

            if (Entities.ContainsKey(aEntity.UniqId))
                return false;

            return Entities.TryAdd(aEntity.UniqId, aEntity);
        }

        public Boolean DelEntity(Entity aEntity)
        {
            if (aEntity == null)
                return false;

            if (!Entities.ContainsKey(aEntity.UniqId))
                return false;

            Entity entity;
            Entities.TryRemove(aEntity.UniqId, out entity);
            return true;
        }

        public Boolean AddItem(FloorItem aItem)
        {
            if (aItem == null)
                return false;

            if (FloorItems.ContainsKey(aItem.Id))
                return false;

            FloorItems.TryAdd(aItem.Id, aItem);
            BroadcastBlockMsg(aItem, new MsgMapItem(aItem, MsgMapItem.Action.Create));

            // add item to all player screens
            var players = (from entity in Entities.Values
                           where entity.IsPlayer() && MyMath.CanSee(aItem.X, aItem.Y, entity.X, entity.Y, MyMath.NORMAL_RANGE)
                           select (Player)entity);

            foreach (var player in players)
                player.Screen.Add(aItem, false);

            return true;
        }

        public Boolean DelItem(FloorItem aItem)
        {
            if (aItem == null)
                return false;

            if (!FloorItems.ContainsKey(aItem.Id))
                return false;

            FloorItem Tmp = null;
            FloorItems.TryRemove(aItem.Id, out Tmp);
            BroadcastBlockMsg(aItem, new MsgMapItem(aItem, MsgMapItem.Action.Delete));
            return true;
        }

        public bool FindFloorItemCell(ref UInt16 aPosX, ref UInt16 aPosY, int aRange)
        {
            UInt16 posX = aPosX, posY = aPosY;
            var items = (from item in FloorItems.Values
                         where MyMath.CanSee(item.X, item.Y, posX, posY, aRange)
                         select item);

            posX = (UInt16)(posX + MyMath.Generate(-aRange, aRange));
            posY = (UInt16)(posY + MyMath.Generate(-aRange, aRange));

            if ((from item in items where item.X == posX && item.Y == posY select item).Count() == 0)
            {
                if (GetFloorAccess(posX, posY)) // IsLayItemEnable => FloorMask && IsMoveEnable
                {
                    aPosX = posX;
                    aPosY = posY;
                    return true;
                }
            }

            for (int i = Math.Max(aPosX - aRange, 0); i <= aPosX + aRange && i < Width; ++i)
            {
                for (int j = Math.Max(aPosY - aRange, 0); j <= aPosY + aRange && j < Height; ++j)
                {
                    if ((from item in items where item.X == posX && item.Y == posY select item).Count() > 0)
                        continue;

                    if (GetFloorAccess(posX, posY)) // IsLayItemEnable => FloorMask && IsMoveEnable
                    {
                        aPosX = posX;
                        aPosY = posY;
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Broadcast the message in the block around the specified map object.
        /// </summary>
        /// <param name="aObj">The map object.</param>
        /// <param name="aMsg">The message to broadcast.</param>
        public void BroadcastBlockMsg(MapObj aObj, Msg aMsg)
        {
            var players = (from entity in Entities.Values
                           where entity.IsPlayer() && MyMath.CanSee(aObj.X, aObj.Y, entity.X, entity.Y, MyMath.NORMAL_RANGE)
                           select (Player)entity);

            foreach (var player in players)
                player.Send(aMsg);
        }

        public void BroadcastBlockMsg(UInt16 aPosX, UInt16 aPosY, Msg aMsg)
        {
            var players = (from entity in Entities.Values
                           where entity.IsPlayer() && MyMath.CanSee(aPosX, aPosY, entity.X, entity.Y, MyMath.NORMAL_RANGE)
                           select (Player)entity);

            foreach (var player in players)
                player.Send(aMsg);
        }
    }
}
