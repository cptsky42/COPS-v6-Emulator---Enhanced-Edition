// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2014 - 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.Runtime.CompilerServices;
using COServer.Entities;

[assembly: InternalsVisibleTo("COServer.Network.Msg")]

namespace COServer.Network
{
    public class MsgPetInfo : Msg
    {
        /// <summary>
        /// This is a "constant" that the child must override.
        /// It is the type of the message as specified in NetworkDef.cs file.
        /// </summary>
        protected override UInt16 _TYPE { get { return MSG_PETINFO; } }

        //--------------- Internal Members ---------------
        private Int32 __Id = 0;
        private UInt32 __Lookface = 0;
        private Int32 __AIType = 0;
        private UInt16 __PosX = 0;
        private UInt16 __PosY = 0;
        private String __Name = "";
        //------------------------------------------------

        /// <summary>
        /// Unique ID of the pet.
        /// </summary>
        public Int32 Id
        {
            get { return __Id; }
            set { __Id = value; WriteInt32(4, value); }
        }

        public UInt32 Lookface
        {
            get { return __Lookface; }
            set { __Lookface = value; WriteUInt32(8, value); }
        }

        public Int32 AIType
        {
            get { return __AIType; }
            set { __AIType = value; WriteInt32(12, value); }
        }

        public UInt16 PosX
        {
            get { return __PosX; }
            set { __PosX = value; WriteUInt16(16, value); }
        }

        public UInt16 PosY
        {
            get { return __PosY; }
            set { __PosY = value; WriteUInt16(18, value); }
        }

        public String Name
        {
            get { return __Name; }
            set { __Name = value; WriteString(20, value, MAX_NAME_SIZE); }
        }

        public MsgPetInfo(Pet aPet)
            : base(36)
        {
            Id = aPet.Id;
            Lookface = aPet.Look;
            AIType = 0;
            PosX = aPet.X;
            PosY = aPet.Y;
            Name = aPet.Name;
        }
    }
}