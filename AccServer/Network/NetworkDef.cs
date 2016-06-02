// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2010 - 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;

namespace COServer.Network
{
    public partial class Msg
    {
        /// <summary>
        /// The maximum length of a name.
        /// </summary>
        public const int MAX_NAME_SIZE = 16;

        protected const UInt16 MSG_GENERAL = 1000;

        protected const UInt16 MSG_ACCOUNT = MSG_GENERAL + 51;
        protected const UInt16 MSG_CONNECT = MSG_GENERAL + 52;
        protected const UInt16 MSG_CONNECTEX = MSG_GENERAL + 55;
    }
}
