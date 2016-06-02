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
    public class MsgMagicInfo : Msg
    {
        /// <summary>
        /// This is a "constant" that the child must override.
        /// It is the type of the message as specified in NetworkDef.cs file.
        /// </summary>
        protected override UInt16 _TYPE { get { return MSG_MAGICINFO; } }

        //--------------- Internal Members ---------------
        private UInt32 __Exp = 0;
        private UInt16 __Type = 0;
        private UInt16 __Level = 0;
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

        public Byte Level
        {
            get { return (Byte)__Level; }
            set { __Level = value; WriteUInt16(10, value); }
        }

        public MsgMagicInfo(Magic aMagic)
            : base(12)
        {
            Exp = aMagic.Exp;
            Type = aMagic.Type;
            Level = aMagic.Level;
        }
    }
}
