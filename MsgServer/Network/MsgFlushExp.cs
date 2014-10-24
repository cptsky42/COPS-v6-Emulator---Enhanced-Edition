// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.Runtime.InteropServices;

namespace COServer.Network
{
    public unsafe class MsgFlushExp : Msg
    {
        public const Int16 Id = _MSG_FLUSHEXP;

        public enum Action
        {
            WeaponSkill = 0,
            Magic = 1,
            Skill = 2,
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct MsgInfo
        {
            public MsgHeader Header;
            public Int32 Exp;
            public Int16 Type;
            public Int16 Action;
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
                pMsg->Action = (Byte)Action.Magic;

                Byte[] Out = new Byte[pMsg->Header.Length];
                Marshal.Copy((IntPtr)pMsg, Out, 0, Out.Length);

                return Out;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return null; }
        }

        public static Byte[] Create(WeaponSkill WeaponSkill)
        {
            try
            {
                MsgInfo* pMsg = (MsgInfo*)Marshal.AllocHGlobal(sizeof(MsgInfo)).ToPointer();
                pMsg->Header.Length = (Int16)sizeof(MsgInfo);
                pMsg->Header.Type = Id;

                pMsg->Exp = WeaponSkill.Exp;
                pMsg->Type = WeaponSkill.Type;
                pMsg->Action = (Byte)Action.WeaponSkill;

                Byte[] Out = new Byte[pMsg->Header.Length];
                Marshal.Copy((IntPtr)pMsg, Out, 0, Out.Length);
                Marshal.FreeHGlobal((IntPtr)pMsg);

                return Out;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return null; }
        }
    }
}
