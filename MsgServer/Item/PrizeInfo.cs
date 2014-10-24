// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.Runtime.InteropServices;

namespace COServer
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PrizeInfo
    {
        public Int32 Id;
        public Byte Rank;
        public Byte Chance;
        public String Name;
        public Int32 Item;
        public Byte Hole_Num;
        public Byte Addition_Lev;
    }
}
