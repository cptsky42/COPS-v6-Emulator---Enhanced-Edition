// * Created by Jean-Philippe Boivin
// * Copyright © 2010
// * Logik. Project

using System;
using System.Runtime.InteropServices;

namespace COServer.Network
{
    public partial class Msg
    {
        public const Int32 MAX_NAME_SIZE = 0x10;

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct MsgHeader
        {
            public Int16 Length;
            public Int16 Type;
        }


    }
}
