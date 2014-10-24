// * Created by Jean-Philippe Boivin
// * Copyright © 2010-2011
// * Logik. Project

using System;
using System.Runtime.InteropServices;
using COServer.Entities;

namespace COServer.Network
{
    public unsafe class MsgNpcInfo : Msg
    {
        public const Int16 Id = _MSG_NPCINFO;

        [StructLayout(LayoutKind.Sequential)]
        public struct MsgInfo
        {
            public MsgHeader Header;
            public Int32 UniqId;
            public UInt16 X;
            public UInt16 Y;
            public Int16 Look;
            public Int16 Type;
            public Int16 Sort;
            public String Name;
        };

        public static Byte[] Create(NPC Npc, Boolean SendName)
        {
            try
            {
                Byte[] Out = null;

                if (SendName)
                    Out = new Byte[20 + Npc.Name.Length];
                else
                    Out = new Byte[20];

                fixed (Byte* p = Out)
                {
                    *((Int16*)(p + 0)) = (Int16)Out.Length;
                    *((Int16*)(p + 2)) = (Int16)Id;
                    *((Int32*)(p + 4)) = (Int32)Npc.UniqId;
                    *((UInt16*)(p + 8)) = (UInt16)Npc.X;
                    *((UInt16*)(p + 10)) = (UInt16)Npc.Y;
                    *((Int16*)(p + 12)) = (Int16)Npc.Look;
                    *((Int16*)(p + 14)) = (Int16)Npc.Type;
                    *((Int16*)(p + 16)) = (Int16)Npc.Sort;
                    if (SendName)
                    {
                        *((Byte*)(p + 18)) = (Byte)0x01; //String Count
                        *((Byte*)(p + 19)) = (Byte)Npc.Name.Length;
                        for (Byte i = 0; i < (Byte)Npc.Name.Length; i++)
                            *((Byte*)(p + 20 + i)) = (Byte)Npc.Name[i];
                    }
                    else
                    {
                        *((Byte*)(p + 18)) = (Byte)0x00; //String Count
                        *((Byte*)(p + 19)) = (Byte)0x00;
                    }
                }
                return Out;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return null; }
        }
    }
}
