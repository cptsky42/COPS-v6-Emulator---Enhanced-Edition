// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.Runtime.InteropServices;
using COServer.Entities;

namespace COServer.Network
{
    public unsafe class MsgFriendInfo : Msg
    {
        public const Int16 Id = _MSG_FRIENDINFO;

        public struct MsgInfo
        {
            public MsgHeader Header;
            public Int32 UniqId;
            public UInt32 Look;
            public Byte Level;
            public Byte Profession;
            public Int16 PkPoints;
            public Int16 GuildUID;
            public Byte Unknow;
            public Byte Position;
            public fixed Byte Spouse[0x10];
        };

        public static Byte[] Create(Player Player)
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
                    *((Int32*)(p + 4)) = (Int32)Player.UniqId;
                    *((UInt32*)(p + 8)) = (UInt32)Player.Look;
                    *((Byte*)(p + 12)) = (Byte)Player.Level;
                    *((Byte*)(p + 13)) = (Byte)Player.Profession;
                    *((Int16*)(p + 14)) = (Int16)Player.PkPoints;
                    *((Int16*)(p + 16)) = (Int16)0x00; //Guild UID
                    *((Byte*)(p + 18)) = (Byte)0x00; //Unknow
                    *((Byte*)(p + 19)) = (Byte)0x00; //Guild Position
                    for (Byte i = 0; i < Player.Spouse.Length; i++)
                        *((Byte*)(p + 20 + i)) = (Byte)Player.Spouse[i];
                }
                return Out;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return null; }
        }
    }
}
