// * Created by Jean-Philippe Boivin
// * Copyright © 2010-2011
// * Logik. Project

using System;
using System.Runtime.InteropServices;
using COServer.Entities;

namespace COServer.Network
{
    public unsafe class MsgUserInfo : Msg
    {
        public const Int16 Id = _MSG_USERINFO;

        public struct MsgInfo
        {
            public MsgHeader Header;
            public Int32 UniqId;
            public Int32 Look;
            public Int16 Hair;
            public Int32 Money;
            public Int32 CPs;
            public UInt64 Exp;
            public UInt64 Unknow1;
            public UInt64 Unknow2;
            public UInt16 Strength;
            public UInt16 Agility;
            public UInt16 Vitality;
            public UInt16 Spirit;
            public UInt16 AddPoints;
            public UInt16 CurHP;
            public UInt16 CurMP;
            public Int16 PkPoints;
            public Byte Level;
            public Byte Profession;
            public Byte AutoAllot;
            public Byte Metempsychosis;
            public Byte ShowName;
            public Byte StringCount;
            public String Name;
            public String Spouse;
        };

        public static Byte[] Create(Player User)
        {
            try
            {
                if (User.Name == null || User.Name.Length > _MAX_NAMESIZE)
                    return null;

                if (User.Spouse == null || User.Spouse.Length > _MAX_NAMESIZE)
                    return null;

                Byte[] Out = new Byte[70 + User.Name.Length + User.Spouse.Length];
                fixed (Byte* p = Out)
                {
                    *((Int16*)(p + 0)) = (Int16)Out.Length;
                    *((Int16*)(p + 2)) = (Int16)Id;
                    *((Int32*)(p + 4)) = (Int32)User.UniqId;
                    *((Int32*)(p + 8)) = (Int32)User.Look;
                    *((Int16*)(p + 12)) = (Int16)User.Hair;
                    *((Int32*)(p + 14)) = (Int32)User.Money;
                    *((Int32*)(p + 18)) = (Int32)User.CPs;
                    *((UInt64*)(p + 22)) = (UInt64)User.Exp;
                    *((UInt64*)(p + 30)) = (UInt64)0x00;
                    *((UInt64*)(p + 38)) = (UInt64)0x00;
                    *((UInt16*)(p + 46)) = (UInt16)User.Strength;
                    *((UInt16*)(p + 48)) = (UInt16)User.Agility;
                    *((UInt16*)(p + 50)) = (UInt16)User.Vitality;
                    *((UInt16*)(p + 52)) = (UInt16)User.Spirit;
                    *((UInt16*)(p + 54)) = (UInt16)User.AddPoints;
                    *((UInt16*)(p + 56)) = (UInt16)User.CurHP;
                    *((UInt16*)(p + 58)) = (UInt16)User.CurMP;
                    *((Int16*)(p + 60)) = (Int16)User.PkPoints;
                    *((Byte*)(p + 62)) = (Byte)User.Level;
                    *((Byte*)(p + 63)) = (Byte)User.Profession;
                    *((Byte*)(p + 64)) = (Byte)(User.AutoAllot ? 1 : 0);
                    *((Byte*)(p + 65)) = (Byte)User.Metempsychosis;
                    *((Byte*)(p + 66)) = (Byte)0x01; //Show Name
                    *((Byte*)(p + 67)) = (Byte)0x02; //String Count
                    *((Byte*)(p + 68)) = (Byte)User.Name.Length;
                    for (Byte i = 0; i < User.Name.Length; i++)
                        *((Byte*)(p + 69 + i)) = (Byte)User.Name[i];
                    *((Byte*)(p + 69 + (Byte)User.Name.Length)) = (Byte)User.Spouse.Length;
                    for (Byte i = 0; i < User.Spouse.Length; i++)
                        *((Byte*)(p + 70 + (Byte)User.Name.Length + i)) = (Byte)User.Spouse[i];
                }
                return Out;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return null; }
        }
    }
}
