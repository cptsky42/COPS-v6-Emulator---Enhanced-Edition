// * Created by Jean-Philippe Boivin
// * Copyright © 2010-2011
// * Logik. Project

using System;
using System.IO;
using System.Runtime.InteropServices;
using COServer.Entities;

namespace COServer
{
    public unsafe class Item
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct Header
        {
            public Int32 Identifier;
            public Int32 MaxAmount;
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct Info
        {
            public Int32 UniqId;
            public Int32 OwnerUID;
            public UInt16 Position;
            public Int32 Id;
            public Byte Craft;
            public Byte Bless;
            public Byte Enchant;
            public Byte Gem1;
            public Byte Gem2;
            public Byte Attributes;
            public Int32 Restrain;
            public UInt16 CurDura;
            public UInt16 MaxDura;
            public Int64 Reserved;
        };

        private Info* pInfo = null;

        public Int32 UniqId { get { return this.pInfo->UniqId; } }
        public Int32 OwnerUID { get { return this.pInfo->OwnerUID; } set { this.pInfo->OwnerUID = value; World.ItemThread.AddToQueue(this); } }
        public UInt16 Position { get { return this.pInfo->Position; } set { this.pInfo->Position = value; World.ItemThread.AddToQueue(this); } }
        public Int32 Id { get { return this.pInfo->Id; } set { this.pInfo->Id = value; World.ItemThread.AddToQueue(this); } }
        public Byte Craft { get { return this.pInfo->Craft; } set { this.pInfo->Craft = value; World.ItemThread.AddToQueue(this); } }
        public Byte Bless { get { return this.pInfo->Bless; } set { this.pInfo->Bless = value; World.ItemThread.AddToQueue(this); } }
        public Byte Enchant { get { return this.pInfo->Enchant; } set { this.pInfo->Enchant = value; World.ItemThread.AddToQueue(this); } }
        public Byte Gem1 { get { return this.pInfo->Gem1; } set { this.pInfo->Gem1 = value; World.ItemThread.AddToQueue(this); } }
        public Byte Gem2 { get { return this.pInfo->Gem2; } set { this.pInfo->Gem2 = value; World.ItemThread.AddToQueue(this); } }
        public Byte Attr { get { return this.pInfo->Attributes; } set { this.pInfo->Attributes = value; World.ItemThread.AddToQueue(this); } }
        public Int32 Restrain { get { return this.pInfo->Restrain; } set { this.pInfo->Restrain = value; World.ItemThread.AddToQueue(this); } }
        public UInt16 CurDura { get { return this.pInfo->CurDura; } set { this.pInfo->CurDura = value; } }
        public UInt16 MaxDura { get { return this.pInfo->MaxDura; } set { this.pInfo->MaxDura = value; World.ItemThread.AddToQueue(this); } }
        public Info* Ptr { get { return this.pInfo; } }

        public Item(Int32 UniqId, Int32 OwnerUID, UInt16 Position, Int32 Id, Byte Craft, Byte Bless, Byte Enchant, Byte Gem1, Byte Gem2, Byte Attr, Int32 Restrain, UInt16 CurDura, UInt16 MaxDura)
        {
            this.pInfo = (Info*)Marshal.AllocHGlobal(sizeof(Info)).ToPointer();

            this.pInfo->UniqId = UniqId;
            this.pInfo->OwnerUID = OwnerUID;
            this.pInfo->Position = Position;
            this.pInfo->Id = Id;
            this.pInfo->Craft = Craft;
            this.pInfo->Bless = Bless;
            this.pInfo->Enchant = Enchant;
            this.pInfo->Gem1 = Gem1;
            this.pInfo->Gem2 = Gem2;
            this.pInfo->Attributes = Attr;
            this.pInfo->Restrain = Restrain;
            this.pInfo->CurDura = CurDura;
            this.pInfo->MaxDura = MaxDura;
        }

        public Item(Info* pInfo)
        {
            this.pInfo = (Info*)Marshal.AllocHGlobal(sizeof(Info)).ToPointer();

            this.pInfo->UniqId = pInfo->UniqId;
            this.pInfo->OwnerUID = pInfo->OwnerUID;
            this.pInfo->Position = pInfo->Position;
            this.pInfo->Id = pInfo->Id;
            this.pInfo->Craft = pInfo->Craft;
            this.pInfo->Bless = pInfo->Bless;
            this.pInfo->Enchant = pInfo->Enchant;
            this.pInfo->Gem1 = pInfo->Gem1;
            this.pInfo->Gem2 = pInfo->Gem2;
            this.pInfo->Attributes = pInfo->Attributes;
            this.pInfo->Restrain = pInfo->Restrain;
            this.pInfo->CurDura = pInfo->CurDura;
            this.pInfo->MaxDura = pInfo->MaxDura;
        }

        ~Item()
        {
            if (pInfo != null)
                Marshal.FreeHGlobal((IntPtr)pInfo);
            pInfo = null;
        }

        public void Save(ref FileStream Stream)
        {
            try
            {
                lock (Stream)
                {
                    Byte[] Buffer = new Byte[256];
                    Marshal.Copy((IntPtr)pInfo, Buffer, 0, sizeof(Info));
                    Stream.Seek(sizeof(Header) + (sizeof(Info) * UniqId), SeekOrigin.Begin);
                    Stream.Write(Buffer, 0, sizeof(Info));
                    Buffer = null;
                }
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }

        public static Item Create(Int32 OwnerUID, Byte Position, Int32 Id, Byte Craft, Byte Bless, Byte Enchant, Byte Gem1, Byte Gem2, Byte Attr, Int32 Restrain, UInt16 CurDura, UInt16 MaxDura)
        {
            Int32 UniqId = 1;
            Item Item = null;

            try
            {
                lock (World.AllItems)
                {
                    UniqId = World.GetNextItemUID();
                    if (World.AllItems.ContainsKey(UniqId))
                        UniqId = World.GetNextItemUID();
                    World.AllItems.Add(UniqId, null);

                    Item = new Item(UniqId, OwnerUID, Position, Id, Craft, Bless, Enchant, Gem1, Gem2, Attr, Restrain, CurDura, MaxDura);
                    World.AllItems[UniqId] = Item;
                }
                World.ItemThread.AddToQueue(Item);
                return Item;
            }
            catch (Exception Exc) { Program.WriteLine(UniqId + " " + Exc); return null; }
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
