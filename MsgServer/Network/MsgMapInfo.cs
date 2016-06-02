// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2010 - 2015 Jean-Philippe Boivin
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
    /// Message sent to the client by the MsgServer to fill all the map variables.
    /// </summary>
    public class MsgMapInfo : Msg
    {
        /// <summary>
        /// This is a "constant" that the child must override.
        /// It is the type of the message as specified in NetworkDef.cs file.
        /// </summary>
        protected override UInt16 _TYPE { get { return MSG_MAPINFO; } }

        //--------------- Internal Members ---------------
        private UInt32 __UniqId = 0;
        private UInt32 __DocId = 0;
        private UInt32 __Type = 0;
        //------------------------------------------------

        /// <summary>
        /// Unique ID of the map.
        /// </summary>
        public UInt32 UniqId
        {
            get { return __UniqId; }
            set { __UniqId = value; WriteUInt32(4, value); }
        }

        /// <summary>
        /// Doc ID of the map.
        /// </summary>
        public UInt32 DocId
        {
            get { return __DocId; }
            set { __DocId = value; WriteUInt32(8, value); }
        }

        /// <summary>
        /// Type (flags) of the map.
        /// </summary>
        public UInt32 Type
        {
            get { return __Type; }
            set { __Type = value; WriteUInt32(12, value); }
        }

        /// <summary>
        /// Create a new MsgMapInfo packet for the specified map.
        /// </summary>
        /// <param name="aMap">The map object</param>
        public MsgMapInfo(GameMap aMap)
            : base(16)
        {
            UniqId = aMap.Id;
            DocId = aMap.DocId;
            Type = aMap.Type;
        }
    }
}
