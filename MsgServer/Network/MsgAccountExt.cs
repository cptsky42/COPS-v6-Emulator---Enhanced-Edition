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
        const Int32 _MAX_ACCOUNT_SIZE = 16;
        const Int32 _MAX_CHARACTER_SIZE = 16;

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct MsgInfo
        {
            public MsgHeader Header;
            public Int32 Timestamp;
            public Int32 Token;
            public Int32 AccountUID;
            public SByte AccLvl;
            public Int32 Flags;
            public fixed Byte AccountId[_MAX_ACCOUNT_SIZE];
            public fixed Byte Character[_MAX_ACCOUNT_SIZE];
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

                Byte[] Buffer = null;

                Buffer = Program.Encoding.GetBytes(AccountId.PadRight(_MAX_ACCOUNT_SIZE, (Char)0x00));
                Marshal.Copy(Buffer, 0, (IntPtr)pMsg->AccountId, _MAX_ACCOUNT_SIZE);

                Buffer = Program.Encoding.GetBytes(Character.PadRight(_MAX_CHARACTER_SIZE, (Char)0x00));
                Marshal.Copy(Buffer, 0, (IntPtr)pMsg->Character, _MAX_CHARACTER_SIZE);

                Byte[] Out = new Byte[pMsg->Header.Length];
                Marshal.Copy((IntPtr)pMsg, Out, 0, Out.Length);

                return Out;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return null; }
        }

        public static void Process(Client Client, Byte[] Buffer)
        {
            try
            {
                if (Client == null || Buffer == null)
                    return;

                if (Buffer.Length != sizeof(MsgInfo))
                    return;

                MsgInfo* Msg = stackalloc MsgInfo[1];
                fixed (Byte* pBuf = Buffer)
                    Kernel.memcpy(Msg, pBuf, sizeof(MsgInfo));

                lock (World.AllAccounts)
                {
                    if (!World.AllAccounts.ContainsKey(Msg->Token))
                        World.AllAccounts.Add(Msg->Token, *Msg);
                }
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }
    }
}
