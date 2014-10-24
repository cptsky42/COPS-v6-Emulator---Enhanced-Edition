// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.Runtime.InteropServices;
using CO2_CORE_DLL;

namespace COServer.Network
{
    public unsafe class MsgActionExt : Msg
    {
        public const Int16 Id = _MSG_ACTIONEXT;
        const Int32 _MAX_ACCOUNT_SIZE = 0x10;
        const Int32 _MAX_SERVER_SIZE = 0x10;
        const Int32 _MAX_CHARACTER_SIZE = 0x10;

        public enum Action
        {
            None = 0,
            SetAccFlags = 1,
            SetAccLvl = 2,
            SetCharacter = 3,
            SetChrFlags = 4,
            SetChrLvl = 5,
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct MsgInfo
        {
            public MsgHeader Header;
            public Int16 Action;
            public Int32 Param;
            public fixed Byte Account[_MAX_ACCOUNT_SIZE];
            public fixed Byte Server[_MAX_SERVER_SIZE];
            public fixed Byte Character[_MAX_CHARACTER_SIZE];
        };

        public static Byte[] Create(Int32 Param, String Character, String Account, String Server, Action Action)
        {
            try
            {
                MsgInfo* pMsg = (MsgInfo*)Kernel.calloc(sizeof(MsgInfo));
                pMsg->Header.Length = (Int16)sizeof(MsgInfo);
                pMsg->Header.Type = Id;

                pMsg->Action = (Int16)Action;
                pMsg->Param = Param;

                Byte[] Buffer = null;

                Byte* ptr;

                ptr = Account.ToPointer();
                if (ptr != null)
                    Kernel.memcpy(pMsg->Account, ptr, Math.Min(Kernel.strlen(ptr), _MAX_ACCOUNT_SIZE));

                ptr = Server.ToPointer();
                if (ptr != null)
                    Kernel.memcpy(pMsg->Server, ptr, Math.Min(Kernel.strlen(ptr), _MAX_SERVER_SIZE));

                ptr = Character.ToPointer();
                if (ptr != null)
                    Kernel.memcpy(pMsg->Character, ptr, Math.Min(Kernel.strlen(ptr), _MAX_CHARACTER_SIZE));

                Byte[] Out = new Byte[pMsg->Header.Length];
                Kernel.memcpy(Out, pMsg, pMsg->Header.Length);
                Kernel.free(pMsg);

                return Out;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return null; }
        }
    }
}
