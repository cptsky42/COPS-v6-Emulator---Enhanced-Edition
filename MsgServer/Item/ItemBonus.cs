// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.Runtime.InteropServices;

namespace COServer
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ItemBonus
    {
        public Int32 Id;
        public Int16 Life;
        public Int16 MaxAtk;
        public Int16 MinAtk;
        public Int16 Defence;
        public Int16 MAtk;
        public Int16 MDef;
        public Int16 Dexterity;
        public Int16 Dodge;
    }
}
