// * Created by Jean-Philippe Boivin
// * Copyright © 2010-2011
// * Logik. Project

using System;
using System.IO;
using System.Runtime.InteropServices;
using COServer.Network;
using COServer.Entities;
using AMS.Profile;

namespace COServer
{
    public unsafe class Item
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct Info
        {
            public Int32 UniqId;
            public Int32 OwnerUID;
            public Byte Position;
            public Int32 Id;
            public Byte Craft;
            public Byte Bless;
            public Byte Enchant;
            public Byte Gem1;
            public Byte Gem2;
            public Int32 Restrain;
            public UInt16 CurDura;
            public UInt16 MaxDura;
            public Int64 Reserved;
        }

        private Info* pInfo = null;

        private Int32 UniqIdValue = -1;
        private Int32 OwnerUIDValue = -1;
        private UInt16 PositionValue = 0;
        private Int32 IdValue;
        private Byte CraftValue;
        private Byte BlessValue;
        private Byte EnchantValue;
        private Byte Gem1Value;
        private Byte Gem2Value;
        private Byte AttrValue;
        private Int32 RestrainValue;
        private UInt16 CurDuraValue;
        private UInt16 MaxDuraValue;

        private Xml AMSXml;

        public Int32 UniqId { get { return this.UniqIdValue; } }
        public Int32 OwnerUID { get { return this.OwnerUIDValue; } set { this.OwnerUIDValue = value; World.ItemThread.AddToQueue(this, "OwnerUID", value); } }
        public UInt16 Position { get { return this.PositionValue; } set { this.PositionValue = value; World.ItemThread.AddToQueue(this, "Position", value); } }
        public Int32 Id { get { return this.IdValue; } set { this.IdValue = value; World.ItemThread.AddToQueue(this, "Id", value); } }
        public Byte Craft { get { return this.CraftValue; } set { this.CraftValue = value; World.ItemThread.AddToQueue(this, "Craft", value); } }
        public Byte Bless { get { return this.BlessValue; } set { this.BlessValue = value; World.ItemThread.AddToQueue(this, "Bless", value); } }
        public Byte Enchant { get { return this.EnchantValue; } set { this.EnchantValue = value; World.ItemThread.AddToQueue(this, "Enchant", value); } }
        public Byte Gem1 { get { return this.Gem1Value; } set { this.Gem1Value = value; World.ItemThread.AddToQueue(this, "Gem1", value); } }
        public Byte Gem2 { get { return this.Gem2Value; } set { this.Gem2Value = value; World.ItemThread.AddToQueue(this, "Gem2", value); } }
        public Byte Attr { get { return this.AttrValue; } set { this.AttrValue = value; World.ItemThread.AddToQueue(this, "Attr", value); } }
        public Int32 Restrain { get { return this.RestrainValue; } set { this.RestrainValue = value; World.ItemThread.AddToQueue(this, "Restrain", value); } }
        public UInt16 CurDura { get { return this.CurDuraValue; } set { this.CurDuraValue = value; } }
        public UInt16 MaxDura { get { return this.MaxDuraValue; } set { this.MaxDuraValue = value; World.ItemThread.AddToQueue(this, "MaxDura", value); } }

        public Item(Int32 UniqId, Int32 OwnerUID, UInt16 Position, Int32 Id, Byte Craft, Byte Bless, Byte Enchant, Byte Gem1, Byte Gem2, Byte Attr, Int32 Restrain, UInt16 CurDura, UInt16 MaxDura)
        {
            this.UniqIdValue = UniqId;
            this.OwnerUIDValue = OwnerUID;
            this.PositionValue = Position;
            this.IdValue = Id;
            this.CraftValue = Craft;
            this.BlessValue = Bless;
            this.EnchantValue = Enchant;
            this.Gem1Value = Gem1;
            this.Gem2Value = Gem2;
            this.AttrValue = Attr;
            this.RestrainValue = Restrain;
            this.CurDuraValue = CurDura;
            this.MaxDuraValue = MaxDura;

            this.AMSXml = new Xml(Program.RootPath + "\\Items\\" + UniqId + ".item");
            this.AMSXml.RootName = "Item";
        }

        public Item(Xml File, Int32 UniqId, Int32 OwnerUID, UInt16 Position, Int32 Id, Byte Craft, Byte Bless, Byte Enchant, Byte Gem1, Byte Gem2, Byte Attr, Int32 Restrain, UInt16 CurDura, UInt16 MaxDura)
        {
            this.UniqIdValue = UniqId;
            this.OwnerUIDValue = OwnerUID;
            this.PositionValue = Position;
            this.IdValue = Id;
            this.CraftValue = Craft;
            this.BlessValue = Bless;
            this.EnchantValue = Enchant;
            this.Gem1Value = Gem1;
            this.Gem2Value = Gem2;
            this.AttrValue = Attr;
            this.RestrainValue = Restrain;
            this.CurDuraValue = CurDura;
            this.MaxDuraValue = MaxDura;

            this.AMSXml = File;
        }

        ~Item()
        {
            AMSXml = null;
        }

        public void Save()
        {
            try
            {
                lock (AMSXml)
                {
                    using (AMSXml.Buffer())
                    {
                        AMSXml.SetValue("Informations", "OwnerUID", OwnerUIDValue);
                        AMSXml.SetValue("Informations", "Position", PositionValue);
                        AMSXml.SetValue("Informations", "Id", IdValue);
                        AMSXml.SetValue("Informations", "Craft", CraftValue);
                        AMSXml.SetValue("Informations", "Bless", BlessValue);
                        AMSXml.SetValue("Informations", "Enchant", EnchantValue);
                        AMSXml.SetValue("Informations", "Gem1", Gem1Value);
                        AMSXml.SetValue("Informations", "Gem2", Gem2Value);
                        AMSXml.SetValue("Informations", "Attr", AttrValue);
                        AMSXml.SetValue("Informations", "Restrain", RestrainValue);
                        AMSXml.SetValue("Informations", "CurDura", CurDuraValue);
                        AMSXml.SetValue("Informations", "MaxDura", MaxDuraValue);
                    }
                }
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }

        public void Save(String Entry, Int32 Value)
        {
            try
            {
                lock (AMSXml)
                {
                    using (AMSXml.Buffer())
                    {
                        AMSXml.SetValue("Informations", Entry, Value);
                    }
                }
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }

        public static Item Create(Int32 OwnerUID, Byte Position, Int32 Id, Byte Craft, Byte Bless, Byte Enchant, Byte Gem1, Byte Gem2, Byte Attr, Int32 Restrain, UInt16 CurDura, UInt16 MaxDura)
        {
            Int32 UniqId = 1;
            Xml AMSXml = null;
            Item Item = null;

            try
            {
                lock (World.AllItems)
                {
                    UniqId = World.GetNextItemUID();
                    if (World.AllItems.ContainsKey(UniqId))
                        UniqId = World.GetNextItemUID();
                    World.AllItems.Add(UniqId, null);

                    AMSXml = new Xml(Program.RootPath + "\\Items\\" + UniqId + ".item");
                    AMSXml.RootName = "Item";

                    Item = new Item(AMSXml, UniqId, OwnerUID, Position, Id, Craft, Bless, Enchant, Gem1, Gem2, Attr, Restrain, CurDura, MaxDura);
                    World.AllItems[UniqId] = Item;
                }

                using (AMSXml.Buffer())
                {
                    AMSXml.SetValue("Informations", "UniqId", UniqId);
                    AMSXml.SetValue("Informations", "OwnerUID", OwnerUID);
                    AMSXml.SetValue("Informations", "Position", Position);
                    AMSXml.SetValue("Informations", "Id", Id);
                    AMSXml.SetValue("Informations", "Craft", Craft);
                    AMSXml.SetValue("Informations", "Bless", Bless);
                    AMSXml.SetValue("Informations", "Enchant", Enchant);
                    AMSXml.SetValue("Informations", "Gem1", Gem1);
                    AMSXml.SetValue("Informations", "Gem2", Gem2);
                    AMSXml.SetValue("Informations", "Attr", Attr);
                    AMSXml.SetValue("Informations", "Restrain", Restrain);
                    AMSXml.SetValue("Informations", "CurDura", CurDura);
                    AMSXml.SetValue("Informations", "MaxDura", MaxDura);
                }
                AMSXml = null;
                return Item;
            }
            catch (Exception Exc) { Program.WriteLine(UniqId + " " + Exc); AMSXml = null; return null; }
        }

        public static void Delete(Int32 UniqId)
        {
            if (World.AllItems.ContainsKey(UniqId))
            {
                Player Owner = null;
                if (World.AllPlayers.TryGetValue(World.AllItems[UniqId].OwnerUID, out Owner))
                    Owner.DelItem(UniqId, true);

                World.AllItems[UniqId].OwnerUID = 0;
                lock (World.AllItems) { World.AllItems.Remove(UniqId); }
            }
        }
    }
}
