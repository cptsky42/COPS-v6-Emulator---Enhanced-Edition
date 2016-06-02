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
    /// Message sent to the client to update the weather of the map.
    /// </summary>
    public class MsgWeather : Msg
    {
        /// <summary>
        /// This is a "constant" that the child must override.
        /// It is the type of the message as specified in NetworkDef.cs file.
        /// </summary>
        protected override UInt16 _TYPE { get { return MSG_WEATHER; } }

        //--------------- Internal Members ---------------
        private WeatherType __Type = 0;
        private UInt32 __Intensity = 0;
        private UInt32 __Direction = 0;
        private UInt32 __Color = 0;
        //------------------------------------------------

        /// <summary>
        /// Type of weather.
        ///
        /// Common values for Conquer Online are :
        ///   1 - None
        ///   2 - Rain
        ///   3 - Snow
        ///   4 - Rain Wind
        ///   5 - Autumn Leaves
        ///   7 - Cherry Blossom Petals
        ///   8 - Cherry Blossom Petals Wind
        ///   9 - Blowing Cotten
        ///  10 - Atoms
        /// </summary>
        public new WeatherType Type
        {
            get { return __Type; }
            set { __Type = value; WriteUInt32(4, (UInt32)value); }
        }

        /// <summary>
        /// Intensity of the weather.
        /// Value must be between 0 and 999.
        /// </summary>
        public UInt32 Intensity
        {
            get { return __Intensity; }
            set { __Intensity = value; WriteUInt32(8, value); }
        }

        /// <summary>
        /// Direction of the weather.
        /// Value must be between 0 and 359.
        /// </summary>
        public UInt32 Direction
        {
            get { return __Direction; }
            set { __Direction = value; WriteUInt32(12, value); }
        }

        /// <summary>
        /// Color of the weather. 
        /// </summary>
        public UInt32 Color
        {
            get { return __Color; }
            set { __Color = value; WriteUInt32(16, value); }
        }

        /// <summary>
        /// Create a new message for the current map.
        /// </summary>
        /// <param name="aMap">The map.</param>
        public MsgWeather(GameMap aMap)
            : base(20)
        {
            Type = aMap.Weather;
            Intensity = (UInt32)MyMath.Generate(125, 150);
            Direction = (UInt32)MyMath.Generate(45, 85);
            Color = 0;
        }
    }
}
