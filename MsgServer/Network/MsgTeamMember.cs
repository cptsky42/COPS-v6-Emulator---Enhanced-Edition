// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.Runtime.InteropServices;
using COServer.Entities;

namespace COServer.Network
{
    public unsafe class MsgTeamMember : Msg
    {
        public const Int16 Id = _MSG_TEAMMEMBER;

        public enum Action
        {
            AddMember = 0,
            DelMember = 1,
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct MsgInfo
        {
            public MsgHeader Header;
            public Byte Action;
            public Byte Amount;
            public Int16 Unknow;
            [MarshalAs(UnmanagedType.LPStr, SizeConst = _MAX_NAMESIZE)]
            public String Name;
            public Int32 UniqId;
            public Int32 Look;
            public UInt16 MaxHP;
            public UInt16 CurHP;
        };

        public static Byte[] Create(Player Player, Action Action)
        {
            try
            {
                if (Player.Name == null || Player.Name.Length > _MAX_NAMESIZE)
                    return null;

                Byte[] Out = new Byte[36];
                fixed (Byte* p = Out)
                {
                    *((Int16*)(p + 0)) = (Int16)Out.Length;
                    *((Int16*)(p + 2)) = (Int16)Id;
                    *((Byte*)(p + 4)) = (Byte)Action;
                    *((Byte*)(p + 5)) = (Byte)0x01;
                    *((Int16*)(p + 6)) = (Int16)0x00; //Unknow
                    for (Byte i = 0; i < Player.Name.Length; i++)
                        *((Byte*)(p + 8 + i)) = (Byte)Player.Name[i];
                    *((Int32*)(p + 24)) = (Int32)Player.UniqId;
                    *((Int32*)(p + 28)) = (Int32)Player.Look;
                    *((UInt16*)(p + 32)) = (UInt16)Player.MaxHP;
                    *((UInt16*)(p + 34)) = (UInt16)Player.CurHP;
                }
                return Out;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return null; }
        }
    }
}
