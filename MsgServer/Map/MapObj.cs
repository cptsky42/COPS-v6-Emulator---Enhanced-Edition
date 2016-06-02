// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;

namespace COServer
{
    /// <summary>
    /// A map's object.
    /// </summary>
    public interface MapObj
    {
        /// <summary>
        /// Unique ID of the object.
        /// </summary>
        Int32 Id { get; }

        /// <summary>
        /// Map on which the object is.
        /// </summary>
        GameMap Map { get; }

        /// <summary>
        /// X coordinate of the object.
        /// </summary>
        UInt16 X { get; }

        /// <summary>
        /// Y coordinate of the object.
        /// </summary>
        UInt16 Y { get; }
    }
}
