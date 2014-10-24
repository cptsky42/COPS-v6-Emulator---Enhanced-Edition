// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.Runtime.InteropServices;

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
                MsgInfo* pMsg = stackalloc MsgInfo[1];
                pMsg->Header.Length = (Int16)sizeof(MsgInfo);
                pMsg->Header.Type = Id;

                pMsg->Action = (Int16)Action;
                pMsg->Param = Param;

                Byte[] Buffer = null;

                Buffer = Program.Encoding.GetBytes(Account.PadRight(_MAX_ACCOUNT_SIZE, (Char)0x00));
                Marshal.Copy(Buffer, 0, (IntPtr)pMsg->Account, _MAX_ACCOUNT_SIZE);

                Buffer = Program.Encoding.GetBytes(Server.PadRight(_MAX_SERVER_SIZE, (Char)0x00));
                Marshal.Copy(Buffer, 0, (IntPtr)pMsg->Server, _MAX_SERVER_SIZE);

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
                if (Buffer == null)
                    return;

                if (Buffer.Length != sizeof(MsgInfo))
                    return;

                MsgInfo* pMsg = null;
                fixed (Byte* pBuf = Buffer)
                    pMsg = (MsgInfo*)pBuf;

                Byte[] Tmp = null;
                String Account = "";
                String Server = "";
                String Character = "";

                Tmp = new Byte[_MAX_ACCOUNT_SIZE];
                Marshal.Copy((IntPtr)pMsg->Account, Tmp, 0, _MAX_ACCOUNT_SIZE);
                Account = Program.Encoding.GetString(Tmp).TrimEnd((Char)0x00);

                Tmp = new Byte[_MAX_SERVER_SIZE];
                Marshal.Copy((IntPtr)pMsg->Server, Tmp, 0, _MAX_SERVER_SIZE);
                Server = Program.Encoding.GetString(Tmp).TrimEnd((Char)0x00);

                Tmp = new Byte[_MAX_CHARACTER_SIZE];
                Marshal.Copy((IntPtr)pMsg->Character, Tmp, 0, _MAX_CHARACTER_SIZE);
                Character = Program.Encoding.GetString(Tmp).TrimEnd((Char)0x00);

                switch ((Action)pMsg->Action)
                {
                    case Action.SetAccFlags:
                        {
                            Database.SetAccInfo(Account, Server, "Flags", pMsg->Param.ToString());
                            break;
                        }
                    case Action.SetAccLvl:
                        {
                            Database.SetAccInfo(Account, Server, "AccLvl", pMsg->Param.ToString());
                            break;
                        }
                    case Action.SetCharacter:
                        {
                            Database.SetAccInfo(Account, Server, "Character", Character);
                            break;
                        }
                    case Action.SetChrFlags:
                        {
                            Database.SetAccInfo2(Character, Server, "Flags", pMsg->Param.ToString());
                            break;
                        }
                    case Action.SetChrLvl:
                        {
                            Database.SetAccInfo2(Character, Server, "AccLvl", pMsg->Param.ToString());
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Msg[{0}], Action[{1}] not implemented yet!", pMsg->Header.Type, pMsg->Action);
                            break;
                        }
                }
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }
    }
}
