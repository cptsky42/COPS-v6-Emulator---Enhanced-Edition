// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.Runtime.InteropServices;
using COServer.Entities;

namespace COServer.Network
{
    public unsafe class MsgNpcInfoEx : Msg
    {
        public const Int16 Id = _MSG_NPCINFOEX;

        [StructLayout(LayoutKind.Sequential)]
        public struct MsgInfo
        {
            public MsgHeader Header;
            public Int32 UniqId;
            public Int32 MaxHP;
            public Int32 CurHP;
            public UInt16 X;
            public UInt16 Y;
            public Int16 Look;
            public Int16 Type;
            public Int16 Sort;
            public String Name;
        };

        public static Byte[] Create(TerrainNPC Npc, Boolean SendName)
        {
            try
            {
                Byte[] Out = null;

                if (SendName)
                    Out = new Byte[28 + Npc.Name.Length];
                else
                    Out = new Byte[28];

                fixed (Byte* p = Out)
                {
                    *((Int16*)(p + 0)) = (Int16)Out.Length;
                    *((Int16*)(p + 2)) = (Int16)Id;
                    *((Int32*)(p + 4)) = (Int32)Npc.UniqId;
                    *((Int32*)(p + 8)) = (Int32)Npc.MaxHP;
                    *((Int32*)(p + 12)) = (Int32)Npc.CurHP;
                    *((UInt16*)(p + 16)) = (UInt16)Npc.X;
                    *((UInt16*)(p + 18)) = (UInt16)Npc.Y;
                    *((Int16*)(p + 20)) = (Int16)Npc.Look;
                    *((Int16*)(p + 22)) = (Int16)Npc.Type;
                    *((Int16*)(p + 24)) = (Int16)Npc.Sort;
                    if (SendName)
                    {
                        *((Byte*)(p + 26)) = (Byte)0x01; //String Count
                        *((Byte*)(p + 27)) = (Byte)Npc.Name.Length;
                        for (Byte i = 0; i < (Byte)Npc.Name.Length; i++)
                            *((Byte*)(p + 28 + i)) = (Byte)Npc.Name[i];
                    }
                    else
                    {
                        *((Byte*)(p + 26)) = (Byte)0x00; //String Count
                        *((Byte*)(p + 27)) = (Byte)0x00;
                    }
                }
                return Out;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return null; }
        }
    }
}
