// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace COServer
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ShopInfo
    {
        public Int32 Id;
        public String Name;
        public Byte MoneyType;
        public List<Int32> Items;
    }
}
