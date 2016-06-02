// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2011 - 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("COServer.Network.Msg")]

namespace COServer.Network
{
    public class MsgFlushExp : Msg
    {
        /// <summary>
        /// This is a "constant" that the child must override.
        /// It is the type of the message as specified in NetworkDef.cs file.
        /// </summary>
        protected override UInt16 _TYPE { get { return MSG_FLUSHEXP; } }

        public enum Action : ushort
        {
            WeaponSkill = 0,
            Magic = 1,
            Skill = 2,
        };

        //--------------- Internal Members ---------------
        private UInt32 __Exp = 0;
        private UInt16 __Type = 0;
        private Action __Action = (Action)0;
        //------------------------------------------------

        public UInt32 Exp
        {
            get { return __Exp; }
            set { __Exp = value; WriteUInt32(4, value); }
        }

        public new UInt16 Type
        {
            get { return __Type; }
            set { __Type = value; WriteUInt16(8, value); }
        }

        public Action _Action
        {
            get { return __Action; }
            set { __Action = value; WriteUInt16(10, (UInt16)value); }
        }

        public MsgFlushExp(Magic aMagic)
            : base(12)
        {
            Exp = aMagic.Exp;
            Type = aMagic.Type;
            _Action = Action.Magic;
        }

        public MsgFlushExp(WeaponSkill aSkill)
            : base(12)
        {
            Exp = aSkill.Exp;
            Type = aSkill.Type;
            _Action = Action.WeaponSkill;
        }
    }
}
