// * Created by Jean-Philippe Boivin
// * Copyright © 2010
// * Logik. Project

using System;

namespace COServer.Network
{
    public partial class Msg
    {
        protected const Int32 _MAX_NAMESIZE = 15;
        protected const Int32 _MAX_WORDSSIZE = 255;

        protected const Int16 _MSG_NONE = 0;
        protected const Int16 _MSG_GENERAL = 1000;
        protected const Int16 _MSG_REGISTER = _MSG_GENERAL + 1;
        protected const Int16 _MSG_TALK = _MSG_GENERAL + 4;
        protected const Int16 _MSG_WALK = _MSG_GENERAL + 5;
        protected const Int16 _MSG_USERINFO = _MSG_GENERAL + 6;
        protected const Int16 _MSG_ATTACK = _MSG_GENERAL + 7;
        protected const Int16 _MSG_ITEMINFO = _MSG_GENERAL + 8;
        protected const Int16 _MSG_ITEM = _MSG_GENERAL + 9;
        protected const Int16 _MSG_ACTION = _MSG_GENERAL + 10;
        protected const Int16 _MSG_TICK = _MSG_GENERAL + 12;
        protected const Int16 _MSG_PLAYER = _MSG_GENERAL + 14;
        protected const Int16 _MSG_NAME = _MSG_GENERAL + 15;
        protected const Int16 _MSG_WEATHER = _MSG_GENERAL + 16;
        protected const Int16 _MSG_USERATTRIB = _MSG_GENERAL + 17;
        protected const Int16 _MSG_FRIEND = _MSG_GENERAL + 19;
        protected const Int16 _MSG_INTERACT = _MSG_GENERAL + 22;
        protected const Int16 _MSG_TEAM = _MSG_GENERAL + 23;
        protected const Int16 _MSG_ALLOT = _MSG_GENERAL + 24;
        protected const Int16 _MSG_WEAPONSKILL = _MSG_GENERAL + 25;
        protected const Int16 _MSG_TEAMMEMBER = _MSG_GENERAL + 26;
        protected const Int16 _MSG_GEMEMBED = _MSG_GENERAL + 27;
        protected const Int16 _MSG_FORGE = _MSG_GENERAL + 28;
        protected const Int16 _MSG_UNKNOW1029 = _MSG_GENERAL + 29;

        protected const Int16 _MSG_CONNECT = _MSG_GENERAL + 52;
        protected const Int16 _MSG_TRADE = _MSG_GENERAL + 56;

        protected const Int16 _MSG_MAPITEM = _MSG_GENERAL + 101;
        protected const Int16 _MSG_PACKAGE = _MSG_GENERAL + 102;
        protected const Int16 _MSG_MAGICINFO = _MSG_GENERAL + 103;
        protected const Int16 _MSG_FLUSHEXP = _MSG_GENERAL + 104;
        protected const Int16 _MSG_MAGICEFFECT = _MSG_GENERAL + 105;
        protected const Int16 _MSG_SYNATTRINFO = _MSG_GENERAL + 106;
        protected const Int16 _MSG_SYNDICATE = _MSG_GENERAL + 107;
        protected const Int16 _MSG_ITEMINFOEX = _MSG_GENERAL + 108;
        protected const Int16 _MSG_NPCINFOEX = _MSG_GENERAL + 109;
        protected const Int16 _MSG_MAPINFO = _MSG_GENERAL + 110;
        protected const Int16 _MSG_MESSAGEBOARD = _MSG_GENERAL + 111;
        protected const Int16 _MSG_SYNMEMBERINFO = _MSG_GENERAL + 112;
        protected const Int16 _MSG_DICE = _MSG_GENERAL + 113;
        protected const Int16 _MSG_SYNINFO = _MSG_GENERAL + 114;

        protected const Int16 _MSG_NPCINFO = _MSG_GENERAL + 1030;
        protected const Int16 _MSG_NPC = _MSG_GENERAL + 1031;
        protected const Int16 _MSG_DIALOG = _MSG_GENERAL + 1032;
        protected const Int16 _MSG_FRIENDINFO = _MSG_GENERAL + 1033;
        protected const Int16 _MSG_PETINFO = _MSG_GENERAL + 1035;
        protected const Int16 _MSG_DATAARRAY = _MSG_GENERAL + 1036;
        protected const Int16 _MSG_BLESSINFO = _MSG_GENERAL + 1043;
        protected const Int16 _MSG_BLESS = _MSG_GENERAL + 1044;
        protected const Int16 _MSG_BROADCAST = _MSG_GENERAL + 1050;
        protected const Int16 _MSG_NOBLE = _MSG_GENERAL + 1064;

        protected const Int16 _MSG_EXTENSION = 20000;
        protected const Int16 _MSG_ACCOUNTEXT= _MSG_EXTENSION + 1;
        protected const Int16 _MSG_ACTIONEXT = _MSG_EXTENSION + 1;
    }
}
