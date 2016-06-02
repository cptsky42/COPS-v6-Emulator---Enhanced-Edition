// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2011 - 2015 Jean-Philippe Boivin
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
    public class MsgNpcInfoEx : Msg
    {
        /// <summary>
        /// This is a "constant" that the child must override.
        /// It is the type of the message as specified in NetworkDef.cs file.
        /// </summary>
        protected override UInt16 _TYPE { get { return MSG_NPCINFOEX; } }

        //--------------- Internal Members ---------------
        private Int32 __Id = 0; // lookface for statuary
        private UInt32 __MaxLife = 0;
        private UInt32 __Life = 0;
        private UInt16 __CellX = 0;
        private UInt16 __CellY = 0;
        private UInt16 __Look = 0; // dir for statuary
        private UInt16 __Type = 0; // frame for statuary
        private UInt16 __Sort = 0; // pose for statuary
        private StringPacker __StrPacker = null;
        //------------------------------------------------

        public Int32 Id
        {
            get { return __Id; }
            set { __Id = value; WriteInt32(4, value); }
        }

        public UInt32 MaxLife
        {
            get { return __MaxLife; }
            set { __MaxLife = value; WriteUInt32(8, value); }
        }

        public UInt32 Life
        {
            get { return __Life; }
            set { __Life = value; WriteUInt32(12, value); }
        }

        public UInt16 CellX
        {
            get { return __CellX; }
            set { __CellX = value; WriteUInt16(16, value); }
        }

        public UInt16 CellY
        {
            get { return __CellY; }
            set { __CellY = value; WriteUInt16(18, value); }
        }

        public UInt16 Look
        {
            get { return __Look; }
            set { __Look = value; WriteUInt16(20, value); }
        }

        public UInt16 Type
        {
            get { return __Type; }
            set { __Type = value; WriteUInt16(22, value); }
        }

        public UInt16 Sort
        {
            get { return __Sort; }
            set { __Sort = value; WriteUInt16(24, value); }
        }

        public String Name
        {
            get { String name = null; __StrPacker.GetString(out name, 0); return name; }
            set { __StrPacker.AddString(value); }
        }

        public MsgNpcInfoEx(TerrainNPC aNpc, Boolean aSendName)
            : base((UInt16)(28 + (aSendName ? aNpc.Name.Length : 0)))
        {
            Id = aNpc.UniqId;
            MaxLife = (UInt32)aNpc.MaxHP;
            Life = (UInt32)aNpc.CurHP;
            CellX = aNpc.X;
            CellY = aNpc.Y;
            Look = (UInt16)aNpc.Look;
            Type = aNpc.Type;
            Sort = aNpc.Sort;

            __StrPacker = new StringPacker(this, 26);
            if (aSendName)
                __StrPacker.AddString(aNpc.Name);
        }
    }
}
