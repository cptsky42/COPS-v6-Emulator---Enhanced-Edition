using System;
using CO2_CORE_DLL;

namespace CO2_CORE_DLL
{
    public static class IniExt
    {
        public static Boolean KeyExist(this Ini Ini, String Section, String Key)
        {
            try { return !String.IsNullOrEmpty(Ini.ReadValue(Section, Key)); }
            catch { return false; }
        }
    }
}
