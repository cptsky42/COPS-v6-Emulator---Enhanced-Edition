// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.Runtime.InteropServices;

namespace COServer.Network
{
    public unsafe class MsgMagicInfo : Msg
    {
        public const Int16 Id = _MSG_MAGICINFO;

        [StructLayout(LayoutKind.Sequential)]
        public struct MsgInfo
        {
            public MsgHeader Header;
            public Int32 Exp;
            public Int16 Type;
            public Int16 Level;
        };

        public static Byte[] Create(Magic Magic)
        {
            try
            {
                MsgInfo* pMsg = stackalloc MsgInfo[1];
                pMsg->Header.Length = (Int16)sizeof(MsgInfo);
                pMsg->Header.Type = Id;

                pMsg->Exp = Magic.Exp;
                pMsg->Type = Magic.Type;
                pMsg->Level = Magic.Level;

                Byte[] Out = new Byte[pMsg->Header.Length];
                Marshal.Copy((IntPtr)pMsg, Out, 0, Out.Length);

                return Out;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return null; }
        }
    }
}
