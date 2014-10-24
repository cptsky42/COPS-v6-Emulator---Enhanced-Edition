// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.Runtime.InteropServices;

namespace COServer.Network
{
    public unsafe class MsgWeaponSkill : Msg
    {
        public const Int16 Id = _MSG_WEAPONSKILL;

        [StructLayout(LayoutKind.Sequential)]
        public struct MsgInfo
        {
            public MsgHeader Header;
            public Int32 Type;
            public Int32 Level;
            public Int32 Exp;
        };

        public static Byte[] Create(WeaponSkill WeaponSkill)
        {
            try
            {
                MsgInfo* pMsg = stackalloc MsgInfo[1];
                pMsg->Header.Length = (Int16)sizeof(MsgInfo);
                pMsg->Header.Type = Id;

                pMsg->Type = WeaponSkill.Type;
                pMsg->Level = WeaponSkill.Level;
                pMsg->Exp = WeaponSkill.Exp;

                Byte[] Out = new Byte[pMsg->Header.Length];
                Marshal.Copy((IntPtr)pMsg, Out, 0, Out.Length);

                return Out;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return null; }
        }
    }
}
