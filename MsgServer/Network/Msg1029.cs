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
    /// <summary>
    /// Message sent to the client when the player pick-up more than 1000.
    /// </summary>
    public class Msg1029 : Msg
    {
        /// <summary>
        /// This is a "constant" that the child must override.
        /// It is the type of the message as specified in NetworkDef.cs file.
        /// </summary>
        protected override UInt16 _TYPE { get { return MSG_UNKNOW1029; } }

        //--------------- Internal Members ---------------
        private UInt32 __Unknown1 = 0;
        private UInt32 __Data = 0;
        private UInt32 __Unknown2 = 0;
        //------------------------------------------------

        private UInt32 Unknown1
        {
            get { return __Unknown1; }
            set { __Unknown1 = value; WriteUInt32(4, value); }
        }

        public UInt32 Data
        {
            get { return __Data; }
            set { __Data = value; WriteUInt32(8, value); }
        }

        private UInt32 Unknown2
        {
            get { return __Unknown2; }
            set { __Unknown2 = value; WriteUInt32(12, value); }
        }

        public Msg1029(UInt32 aData)
            : base(16)
        {
            Unknown1 = 0;
            Data = aData;
            Unknown2 = 0;
        }
    }
}
