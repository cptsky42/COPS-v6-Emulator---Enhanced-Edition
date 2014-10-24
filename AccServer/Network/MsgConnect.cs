// * Created by Jean-Philippe Boivin
// * Copyright © 2010-2011
// * Logik. Project

using System;
using System.Runtime.InteropServices;

namespace COServer.Network
{
    public unsafe class MsgConnect : Msg
    {
        public const Int16 Id = _MSG_CONNECT;

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct MsgInfo
        {
            public MsgHeader Header;
            public Int32 AccountID;
            public Int32 Unknow;
            public fixed Byte ResFile[MAX_NAME_SIZE];
        };

        public static void Process(Client Client, Byte[] Buffer)
        {
            try
            {
                if (Buffer == null)
                    return;

                Client.Socket.Disconnect();
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }
    }
}
