// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2011 - 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using COServer.Entities;

[assembly: InternalsVisibleTo("COServer.Network.Msg")]

namespace COServer.Network
{
    public class MsgMagicEffect : Msg
    {
        /// <summary>
        /// This is a "constant" that the child must override.
        /// It is the type of the message as specified in NetworkDef.cs file.
        /// </summary>
        protected override UInt16 _TYPE { get { return MSG_MAGICEFFECT; } }

        //--------------- Internal Members ---------------
        private Int32 __PlayerId = 0;
        private UInt32 __Data = 0; // TargetId || (PosX, PosY)
        private UInt16 __Type = 0;
        private UInt16 __Level = 0;
        //------------------------------------------------

        public Int32 PlayerId
        {
            get { return __PlayerId; }
            set { __PlayerId = value; WriteInt32(4, value); }
        }

        public Int32 TargetId
        {
            get { return (Int32)__Data; }
            set { __Data = (UInt32)value; WriteInt32(8, value); }
        }

        public UInt16 PosX
        {
            get { return (UInt16)((__Data >> 16) & 0x0000FFFFU); }
            set { __Data = (UInt32)((value << 16) | (__Data & 0x0000FFFFU)); WriteUInt16(8, value); }
        }

        public UInt16 PosY
        {
            get { return (UInt16)(__Data & 0x0000FFFFU); }
            set { __Data = (UInt32)((__Data & 0xFFFF0000U) | (value)); WriteUInt16(10, value); }
        }

        public UInt16 Type
        {
            get { return __Type; }
            set { __Type = value; WriteUInt16(12, value); }
        }

        public UInt16 Level
        {
            get { return __Level; }
            set { __Level = value; WriteUInt16(14, value); }
        }

        public MsgMagicEffect(AdvancedEntity aAttacker, Dictionary<AdvancedEntity, Int32> aTargets, UInt16 aPosX, UInt16 aPosY)
            : base((UInt16)(20 + (aTargets.Count * 12)))
        {
            PlayerId = aAttacker.UniqId;
            PosX = aPosX;
            PosY = aPosY;
            Type = aAttacker.MagicType;
            Level = aAttacker.MagicLevel;

            WriteInt32(16, aTargets.Count);

            int offset = 20;
            foreach (KeyValuePair<AdvancedEntity, Int32> kv in aTargets)
            {
                WriteInt32(offset + 0, kv.Key.UniqId); // RoleId
                WriteInt32(offset + 4, kv.Value); // Data
                WriteInt32(offset + 8, 0);
                offset += 12;
            }
        }

        public MsgMagicEffect(AdvancedEntity aAttacker, AdvancedEntity aTarget, Int32 aDamage, UInt16 aPosX, UInt16 aPosY)
            : base(32)
        {
            Magic.Info info = Database.AllMagics[(aAttacker.MagicType * 10) + aAttacker.MagicLevel];

            PlayerId = aAttacker.UniqId;
            if (info.Sort == MagicSort.AttackSingleHP)
                TargetId = aTarget.UniqId;
            else
            {
                PosX = aPosX;
                PosY = aPosY;
            }
            Type = aAttacker.MagicType;
            Level = aAttacker.MagicLevel;

            WriteInt32(16, 1);
            WriteInt32(20, aTarget.UniqId); // RoleId
            WriteInt32(24, aDamage); // Data
            WriteInt32(28, 0);
        }
    }
}
