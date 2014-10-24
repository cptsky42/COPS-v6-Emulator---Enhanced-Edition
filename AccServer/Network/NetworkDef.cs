// * Created by Jean-Philippe Boivin
// * Copyright © 2010
// * Logik. Project

using System;

namespace COServer.Network
{
    public partial class Msg
    {
        protected const Int16 _MSG_NONE = 0;
        protected const Int16 _MSG_GENERAL = 1000;

        protected const Int16 _MSG_ACCOUNT = _MSG_GENERAL + 51;
        protected const Int16 _MSG_CONNECT = _MSG_GENERAL + 52;
        protected const Int16 _MSG_LOGINREPLY = _MSG_GENERAL + 55;

        protected const Int16 _MSG_EXTENSION = 20000;
        protected const Int16 _MSG_ACCOUNTEXT = _MSG_EXTENSION + 1;
        protected const Int16 _MSG_ACTIONEXT = _MSG_EXTENSION + 1;
    }
}
