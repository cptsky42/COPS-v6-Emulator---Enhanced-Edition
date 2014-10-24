// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.Runtime.InteropServices;

namespace COServer.Network
{
    public unsafe class MsgItemInfoEx : Msg
    {
        public const Int16 Id = _MSG_ITEMINFOEX;

        public enum Action
        {
            None = 0,
            Booth = 1,
            Equipment = 2,
            BoothCPs = 3,
            OtherPlayer_Equipement = 4,
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct MsgInfo
        {
            public MsgHeader Header;
            public Int32 UniqId;
            public Int32 OwnerUID;
            public Int32 Price;
            public Int32 Id;
            public UInt16 CurDura;
            public UInt16 MaxDura;
            public Byte Action;
            public Byte Ident;
            public Byte Position;
            public Int32 Unknow1;
            public Byte Gem1;
            public Byte Gem2;
            public Byte Attr;
            public Byte Magic2;
            public Byte Craft;
            public Byte Bless;
            public Byte Enchant;
            public Int32 Restrain;
        };

        public static Byte[] Create(Int32 OwnerUID, Item Item, Int32 Price, Action Action)
        {
            try
            {
                MsgInfo* pMsg = stackalloc MsgInfo[1];
                pMsg->Header.Length = (Int16)sizeof(MsgInfo);
                pMsg->Header.Type = Id;

                pMsg->UniqId = Item.UniqId;
                pMsg->OwnerUID = OwnerUID;
                pMsg->Price = Price;
                pMsg->Id = Item.Id;
                pMsg->CurDura = Item.CurDura;
                pMsg->MaxDura = Item.MaxDura;
                pMsg->Action = (Byte)Action;
                pMsg->Ident = 0x00;
                pMsg->Position = (Byte)Item.Position;
                pMsg->Unknow1 = 0x00;
                pMsg->Gem1 = Item.Gem1;
                pMsg->Gem2 = Item.Gem2;
                pMsg->Attr = Item.Attr;
                pMsg->Magic2 = 0x00;
                pMsg->Craft = Item.Craft;
                pMsg->Bless = Item.Bless;
                pMsg->Enchant = Item.Enchant;
                pMsg->Restrain = Item.Restrain;

                Byte[] Out = new Byte[pMsg->Header.Length];
                Marshal.Copy((IntPtr)pMsg, Out, 0, Out.Length);

                return Out;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return null; }
        }
    }
}
