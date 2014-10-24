// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.Runtime.InteropServices;
using CO2_CORE_DLL;

namespace COServer.Network
{
    public unsafe class MsgAccountExt : Msg
    {
        public const Int16 Id = _MSG_ACCOUNTEXT;

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct MsgInfo
        {
            public MsgHeader Header;
            public Int32 Timestamp;
            public Int32 Token;
            public Int32 AccountUID;
            public SByte AccLvl;
            public Int32 Flags;
            public fixed Byte AccountId[MAX_NAME_SIZE];
            public fixed Byte Character[MAX_NAME_SIZE];
        };

        public static Byte[] Create(String AccountId, SByte AccLvl, Int32 Flags, Int32 Token, Int32 AccountUID, String Character)
        {
            try
            {
                MsgInfo* pMsg = stackalloc MsgInfo[1];
                pMsg->Header.Length = (Int16)sizeof(MsgInfo);
                pMsg->Header.Type = Id;

                pMsg->Timestamp = Environment.TickCount;
                pMsg->Timestamp ^= Token;
                pMsg->Token = Token;
                pMsg->AccountUID = AccountUID;
                pMsg->AccLvl = AccLvl;
                pMsg->Flags = Flags;

                Byte* Buffer = stackalloc Byte[MAX_NAME_SIZE];
                Kernel.memset(Buffer, 0, MAX_NAME_SIZE);
                Kernel.memcpy(Buffer, AccountId.ToPointer(), AccountId.Length);
                Kernel.memcpy(pMsg->AccountId, Buffer, MAX_NAME_SIZE);

                Kernel.memset(Buffer, 0, MAX_NAME_SIZE);
                Kernel.memcpy(Buffer, Character.ToPointer(), Character.Length);
                Kernel.memcpy(pMsg->Character, Buffer, MAX_NAME_SIZE);

                Byte[] Out = new Byte[pMsg->Header.Length];
                Kernel.memcpy(Out, pMsg, Out.Length);
                return Out;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return null; }
        }
    }
}
