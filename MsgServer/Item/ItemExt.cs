using System;
using System.Text;
using CO2_CORE_DLL;
using CO2_CORE_DLL.IO;

namespace COServer
{
    public static class ItemExt
    {
        public static Boolean IsExchangeEnable(this ItemType.Entry Info) { return (Info.Monopoly & ItemType.ITEM_MONOPOLY_MASK) == 0; }
        public static Boolean IsStorageEnable(this ItemType.Entry Info) { return (Info.Monopoly & ItemType.ITEM_STORAGE_MASK) == 0; }
        public static Boolean IsSellEnable(this ItemType.Entry Info) { return (Info.Monopoly & ItemType.ITEM_SELL_DISABLE_MASK) == 0; }
        public static Boolean IsRepairEnable(this ItemType.Entry Info) { return true;/* (RepairMode & 0x02) == 0;*/ }
    }
}