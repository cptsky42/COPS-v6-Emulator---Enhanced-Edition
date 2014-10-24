// * Created by Jean-Philippe Boivin
// * Copyright © 2010-2011
// * Logik. Project

using System;
using System.Collections.Concurrent;
using COServer.Games;
using COServer.Network;
using COServer.Entities;
using System.Drawing;

namespace COServer
{
    public class Map
    {
        //[Flags]
        //public enum Flags : int
        //{
        //    None = 0x0000,
        //    PKField = 0x0001,               //No PkPoints, Not Flashing...
        //    ChangeMap_Disable = 0x0002,     //Unused...
        //    Record_Disable = 0x0004,        //Do not save this position, save the previous
        //    PK_Disable = 0x0008,            //Can't Pk
        //    Booth_Enable = 0x0010,          //Can create booth
        //    Team_Disable = 0x0020,          //Can't create team
        //    Teleport_Disable = 0x0040,      //Can't use scroll
        //    Syn_Map = 0x0080,               //Syndicate Map
        //    Prison_Map = 0x0100,            //Prison Map
        //    Wing_Disable = 0x0200,          //Can't fly
        //    Family = 0x0400,                //Family Map
        //    MineField = 0x0800,             //Mine Map
        //    PKGame = 0x1000,                //MAPTYPE_CALLNEWBIE_DISABLE
        //    NeverWound = 0x2000,            //MAPTYPE_REBORN_NOW_ENABLE
        //    DeadIsland = 0x4000,            //MAPTYPE_NEWBIE_PROTECT
        //};


        public ConcurrentDictionary<Int32, Object> Entities = new ConcurrentDictionary<Int32, Object>();
        public ConcurrentDictionary<Int32, FloorItem> FloorItems = new ConcurrentDictionary<Int32, FloorItem>();

        private Int16 UniqIdValue = -1;
        private Boolean[,] AccessibleValue;
        private UInt16 WidthValue;
        private UInt16 HeightValue;

        private UInt16[,] HeightsValue;

        public UInt16 Id;
        public Int32 Flags;
        public Byte Weather;
        public UInt16 PortalX;
        public UInt16 PortalY;
        public Int16 RebornMap;
        public UInt32 Color;

        public Int16 UniqId { get { return this.UniqIdValue; } }
        public Boolean[,] Accessible { get { return this.AccessibleValue; } }
        public UInt16[,] Heights { get { return this.HeightsValue; } }
        public UInt16 Width { get { return this.WidthValue; } }
        public UInt16 Height { get { return this.HeightValue; } }

        public Boolean InWar = false;
        public Int16 Holder = 0;
        public Int32 RealFlags = 0;
        public CityWar War = null;

        public Map(Int16 UniqId, Boolean[,] Accessible, UInt16[,] Heights, UInt16 Width, UInt16 Height)
        {
            this.UniqIdValue = UniqId;
            this.AccessibleValue = new Boolean[Width, Height];
            Buffer.BlockCopy(Accessible, 0, this.AccessibleValue, 0, Accessible.Length);
            this.HeightsValue = new UInt16[Width, Height];
            Buffer.BlockCopy(Heights, 0, this.HeightsValue, 0, Heights.Length);
            this.WidthValue = Width;
            this.HeightValue = Height;
        }

        ~Map()
        {
            this.AccessibleValue = null;
            Entities.Clear();
            Entities = null;
            FloorItems.Clear();
            FloorItems = null;
        }

        public Boolean ContainsFlag(Int32 Flag) { return (Flags & Flag) == Flag; }
        public void AddFlag(Int32 Flag) { Flags |= Flag; }
        public void DelFlag(Int32 Flag) { Flags ^= Flag; }

        public Boolean IsPkField() { return ContainsFlag(0x0001); }
        public Boolean IsChangeMap_Disable() { return ContainsFlag(0x0002); }
        public Boolean IsRecord_Disable() { return ContainsFlag(0x0004); }
        public Boolean IsPk_Disable() { return ContainsFlag(0x0008); }
        public Boolean IsBooth_Enable() { return ContainsFlag(0x0010); }
        public Boolean IsTeam_Disable() { return ContainsFlag(0x0020); }
        public Boolean IsTeleport_Disable() { return ContainsFlag(0x0040); }
        public Boolean IsSynMap() { return ContainsFlag(0x0080); }
        public Boolean IsPrisonMap() { return ContainsFlag(0x0100); }
        public Boolean IsWing_Disable() { return ContainsFlag(0x0200); }
        public Boolean IsFamily() { return ContainsFlag(0x0400); }
        public Boolean IsMineField() { return ContainsFlag(0x0800); }
        public Boolean IsCallNewbie_Disable() { return ContainsFlag(0x1000); }
        public Boolean IsRebornNow_Enable() { return ContainsFlag(0x2000); }
        public Boolean IsNewbieProtect() { return ContainsFlag(0x4000); }

        public Boolean IsValidPoint(UInt16 X, UInt16 Y)
        {
            if (X < WidthValue && Y < HeightValue)
                return Accessible[X, Y];
            return false;
        }

        public UInt16 GetHeight(UInt16 X, UInt16 Y)
        {
            if (X < WidthValue && Y < HeightValue)
                return Heights[X, Y];
            return 0;
        }

        public Boolean AddEntity(Object Entity)
        {
            if (Entity == null)
                return false;

            if ((Entity as Entity) == null)
                return false;

            if (Entities.ContainsKey((Entity as Entity).UniqId))
                return false;

            Entities.TryAdd((Entity as Entity).UniqId, Entity);
            return true;
        }

        public Boolean DelEntity(Object Entity)
        {
            if (Entity == null)
                return false;

            if ((Entity as Entity) == null)
                return false;

            if (!Entities.ContainsKey((Entity as Entity).UniqId))
                return false;

            Object Tmp = null;
            Entities.TryRemove((Entity as Entity).UniqId, out Tmp);
            return true;
        }

        public Boolean AddItem(FloorItem Item)
        {
            if (Item == null)
                return false;

            if (FloorItems.ContainsKey(Item.UniqId))
                return false;

            FloorItems.TryAdd(Item.UniqId, Item);
            return true;
        }

        public Boolean DelItem(FloorItem Item)
        {
            if (Item == null)
                return false;

            if (!FloorItems.ContainsKey(Item.UniqId))
                return false;

            FloorItem Tmp = null;
            FloorItems.TryRemove(Item.UniqId, out Tmp);
            return true;
        }

        public void RenamePole()
        {
            Int32 PoleUID = 100000 + (UniqId * 10);
            if (Entities.ContainsKey(PoleUID))
            {
                TerrainNPC Npc = Entities[PoleUID] as TerrainNPC;
                if (Npc != null)
                {
                    String Name = "COPS";
                    if (World.AllSyndicates.ContainsKey(Holder))
                        Name = World.AllSyndicates[Holder].Name;

                    Npc.Name = Name;
                    World.BroadcastMapMsg(UniqId, MsgNpcInfoEx.Create(Npc, true));
                }
            }
        }

        public void ShowGates()
        {
            Int32 GateUID = 100001 + (UniqId * 10);
            if (Entities.ContainsKey(GateUID))
            {
                TerrainNPC Npc = Entities[GateUID] as TerrainNPC;
                if (Npc != null)
                {
                    if (Npc.Look / 10 != Npc.Base)
                        Npc.Look = (UInt32)(Npc.Base * 10 + (Npc.Look % 10));
                    Npc.CurHP = Npc.MaxHP;
                    Npc.X = Npc.RealX;
                    Npc.Y = Npc.RealY;

                    World.BroadcastMapMsg(UniqId, MsgNpcInfoEx.Create(Npc, true));
                }
            }

            GateUID++;
            if (Entities.ContainsKey(GateUID))
            {
                TerrainNPC Npc = Entities[GateUID] as TerrainNPC;
                if (Npc != null)
                {
                    if (Npc.Look / 10 != Npc.Base)
                        Npc.Look = (UInt32)(Npc.Base * 10 + (Npc.Look % 10));
                    Npc.CurHP = Npc.MaxHP;
                    Npc.X = Npc.RealX;
                    Npc.Y = Npc.RealY;

                    World.BroadcastMapMsg(UniqId, MsgNpcInfoEx.Create(Npc, true));
                }
            }

            if (UniqId == 1000)
            {
                for (Int32 i = 0; i < 4; i++)
                {
                    GateUID = 100005 + (UniqId * 10) + i;
                    if (Entities.ContainsKey(GateUID))
                    {
                        TerrainNPC Npc = Entities[GateUID] as TerrainNPC;
                        if (Npc != null)
                        {
                            Npc.X = Npc.RealX;
                            Npc.Y = Npc.RealY;
                            World.BroadcastMapMsg(UniqId, MsgNpcInfoEx.Create(Npc, true));
                        }
                    }
                }
            }
        }

        public void HideGates()
        {
            Int32 GateUID = 100001 + (UniqId * 10);
            if (Entities.ContainsKey(GateUID))
            {
                TerrainNPC Npc = Entities[GateUID] as TerrainNPC;
                if (Npc != null)
                {
                    Npc.X = 2000;
                    Npc.Y = 2000;

                    World.BroadcastMapMsg(UniqId, MsgAction.Create(Npc, 0, MsgAction.Action.LeaveMap));
                }
            }

            GateUID++;
            if (Entities.ContainsKey(GateUID))
            {
                TerrainNPC Npc = Entities[GateUID] as TerrainNPC;
                if (Npc != null)
                {
                    Npc.X = 2000;
                    Npc.Y = 2000;

                    World.BroadcastMapMsg(UniqId, MsgAction.Create(Npc, 0, MsgAction.Action.LeaveMap));
                }
            }

            if (UniqId == 1000)
            {
                for (Int32 i = 0; i < 4; i++)
                {
                    GateUID = 100005 + (UniqId * 10) + i;
                    if (Entities.ContainsKey(GateUID))
                    {
                        TerrainNPC Npc = Entities[GateUID] as TerrainNPC;
                        if (Npc != null)
                        {
                            Npc.X = 2000;
                            Npc.Y = 2000;

                            World.BroadcastMapMsg(UniqId, MsgAction.Create(Npc, 0, MsgAction.Action.LeaveMap));
                        }
                    }
                }
            }
        }
    }
}
