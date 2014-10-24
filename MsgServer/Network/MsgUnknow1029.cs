using System;
using System.Runtime.InteropServices;
using CO2_CORE_DLL;

namespace COServer.Network
{
    public unsafe class MsgUnknow1029 : Msg
    {
        public const Int16 Id = _MSG_UNKNOW1029;

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct MsgInfo
        {
            public MsgHeader Header;
            public Int32 Unknow1;
            public Int32 Param;
            public Int32 Unknow2;
        };

        public static Byte[] Create(Int32 Param)
        {
            try
            {
                MsgInfo* Msg = stackalloc MsgInfo[1];
                Msg->Header.Length = (Int16)sizeof(MsgInfo);
                Msg->Header.Type = Id;

                Msg->Unknow1 = 0x00;
                Msg->Param = Param;
                Msg->Unknow2 = 0x00;

                Byte[] Out = new Byte[Msg->Header.Length];
                Kernel.memcpy(Out, Msg, Out.Length);
                return Out;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return null; }
        }
    }
}
