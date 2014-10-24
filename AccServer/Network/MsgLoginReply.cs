// * Created by Jean-Philippe Boivin
// * Copyright © 2010-2011
// * Logik. Project

using System;
using System.Runtime.InteropServices;

namespace COServer.Network
{
    public unsafe class MsgLoginReply : Msg
    {
        public const Int16 Id = _MSG_LOGINREPLY;

        public const String ERROR_STR = "\xD5\xCA\xBA\xC5\xC3\xFB\xBB\xF2\xBF\xDA\xC1\xEE\xB4\xED";

        public enum ErrorId
        {
            None = 0,
            Invalid_Acc = 1,
            Server_Down = 10,
            Try_Later = 11,
            Banned = 12,
            Server_Full = 20,
            Server_Full2 = 21,
            Acc_Exist = 22,
            //Unknow = 22, //10440=ÄúµÄÕÊºÅÒÑËø¶¨£¬·¢BJµ½05291½â³ýËø¶¨Ö®ºó·½¿ÉµÇÂ½¡£
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct MsgInfo
        {
            public MsgHeader Header;
            public Int32 AccountUID;
            public Int32 Token;
            public fixed Byte IPAddress[MAX_NAME_SIZE];
            public Int32 Port;
        };

        public static Byte[] Create(Int32 AccountUID, Int32 Token, String IPAddress, Int32 Port)
        {
            try
            {
                Byte[] Out = new Byte[32];
                fixed (Byte* p = Out)
                {
                    *((Int16*)(p + 0)) = (Int16)Out.Length;
                    *((Int16*)(p + 2)) = (Int16)Id;
                    *((Int32*)(p + 4)) = (Int32)AccountUID;
                    *((Int32*)(p + 8)) = (Int32)Token;
                    for (Byte i = 0; i < IPAddress.Length; i++)
                        *((Byte*)(p + 12 + i)) = (Byte)IPAddress[i];
                    *((Int32*)(p + 28)) = (Int32)Port;
                }
                return Out;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return null; }
        }

    }
}
