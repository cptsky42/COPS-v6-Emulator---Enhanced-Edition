// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using COServer.Entities;
using CO2_CORE_DLL.IO;

namespace COServer.Network
{
    public unsafe class MsgMagicEffect : Msg
    {
        public const Int16 Id = _MSG_MAGICEFFECT;

        public struct EffectRole
        {
            public Int32 TargetUID;
            public Int32 Data;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MsgInfo
        {
            public MsgHeader Header;
            public Int32 UniqId;
            public Int32 Param; //TargetUID || (X, Y)
            public Int16 Type;
            public Int16 Level;
            public Int32 TargetCount;
            public EffectRole[] Targets;
        };

        public static Byte[] Create(AdvancedEntity Attacker, Dictionary<AdvancedEntity, Int32> Targets, UInt16 X, UInt16 Y)
        {
            try
            {
                if (!Database2.AllMagics.ContainsKey((Attacker.MagicType * 10) + Attacker.MagicLevel))
                    return null;

                Byte[] Out = new Byte[20 + (Targets.Count * 12)];
                fixed (Byte* p = Out)
                {
                    *((Int16*)(p + 0)) = (Int16)Out.Length;
                    *((Int16*)(p + 2)) = (Int16)Id;
                    *((Int32*)(p + 4)) = (Int32)Attacker.UniqId;
                    *((Int16*)(p + 8)) = (Int16)X;
                    *((Int16*)(p + 10)) = (Int16)Y;
                    *((Int16*)(p + 12)) = (Int16)Attacker.MagicType;
                    *((Int16*)(p + 14)) = (Int16)Attacker.MagicLevel;
                    *((Int32*)(p + 16)) = (Int32)Targets.Count;

                    Int32 Pos = 20;
                    foreach (KeyValuePair<AdvancedEntity, Int32> KV in Targets)
                    {
                        *((Int32*)(p + Pos + 0)) = (Int32)KV.Key.UniqId;
                        *((Int32*)(p + Pos + 4)) = (Int32)KV.Value;
                        *((Int32*)(p + Pos + 8)) = (Int32)0x00000000;
                        Pos += 12;
                    }
                }
                return Out;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return null; }
        }

        public static Byte[] Create(AdvancedEntity Attacker, AdvancedEntity Target, Int32 Damage, UInt16 X, UInt16 Y)
        {
            try
            {
                MagicType.Entry Info = new MagicType.Entry();
                if (!Database2.AllMagics.TryGetValue((Attacker.MagicType * 10) + Attacker.MagicLevel, out Info))
                    return null;

                Byte[] Out = new Byte[32];
                fixed (Byte* p = Out)
                {
                    *((Int16*)(p + 0)) = (Int16)Out.Length;
                    *((Int16*)(p + 2)) = (Int16)Id;
                    *((Int32*)(p + 4)) = (Int32)Attacker.UniqId;
                    if (Info.ActionSort == 1)
                        *((Int32*)(p + 8)) = (Int32)Target.UniqId;
                    else
                    {
                        *((Int16*)(p + 8)) = (Int16)X;
                        *((Int16*)(p + 10)) = (Int16)Y;
                    }
                    *((Int16*)(p + 12)) = (Int16)Attacker.MagicType;
                    *((Int16*)(p + 14)) = (Int16)Attacker.MagicLevel;
                    *((Int32*)(p + 16)) = (Int32)0x01;
                    *((Int32*)(p + 20)) = (Int32)Target.UniqId;
                    *((Int32*)(p + 24)) = (Int32)Damage;
                    *((Int32*)(p + 28)) = (Int32)0x00000000;
                }
                return Out;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return null; }
        }
    }
}
