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
        protected const int MAX_NAME_SIZE = 16;
        protected const int MAX_LANGUAGE_SIZE = 10;
        protected const int MAX_WORDS_SIZE = 255;

        protected const UInt16 MSG_NONE = 0;
        protected const UInt16 MSG_GENERAL = 1000;
        protected const UInt16 MSG_REGISTER = MSG_GENERAL + 1;
        protected const UInt16 MSG_TALK = MSG_GENERAL + 4;
        protected const UInt16 MSG_WALK = MSG_GENERAL + 5;
        protected const UInt16 MSG_USERINFO = MSG_GENERAL + 6;
        protected const UInt16 MSG_ATTACK = MSG_GENERAL + 7;
        protected const UInt16 MSG_ITEMINFO = MSG_GENERAL + 8;
        protected const UInt16 MSG_ITEM = MSG_GENERAL + 9;
        protected const UInt16 MSG_ACTION = MSG_GENERAL + 10;
        protected const UInt16 MSG_TICK = MSG_GENERAL + 12;
        protected const UInt16 MSG_PLAYER = MSG_GENERAL + 14;
        protected const UInt16 MSG_NAME = MSG_GENERAL + 15;
        protected const UInt16 MSG_WEATHER = MSG_GENERAL + 16;
        protected const UInt16 MSG_USERATTRIB = MSG_GENERAL + 17;
        protected const UInt16 MSG_FRIEND = MSG_GENERAL + 19;
        protected const UInt16 MSG_INTERACT = MSG_GENERAL + 22;
        protected const UInt16 MSG_TEAM = MSG_GENERAL + 23;
        protected const UInt16 MSG_ALLOT = MSG_GENERAL + 24;
        protected const UInt16 MSG_WEAPONSKILL = MSG_GENERAL + 25;
        protected const UInt16 MSG_TEAMMEMBER = MSG_GENERAL + 26;
        protected const UInt16 MSG_GEMEMBED = MSG_GENERAL + 27;
        protected const UInt16 MSG_FUSE = MSG_GENERAL + 28;
        protected const UInt16 MSG_UNKNOW1029 = MSG_GENERAL + 29;

        protected const UInt16 MSG_CONNECT = MSG_GENERAL + 52;
        protected const UInt16 MSG_TRADE = MSG_GENERAL + 56;

        protected const UInt16 MSG_MAPITEM = MSG_GENERAL + 101;
        protected const UInt16 MSG_PACKAGE = MSG_GENERAL + 102;
        protected const UInt16 MSG_MAGICINFO = MSG_GENERAL + 103;
        protected const UInt16 MSG_FLUSHEXP = MSG_GENERAL + 104;
        protected const UInt16 MSG_MAGICEFFECT = MSG_GENERAL + 105;
        protected const UInt16 MSG_SYNATTRINFO = MSG_GENERAL + 106;
        protected const UInt16 MSG_SYNDICATE = MSG_GENERAL + 107;
        protected const UInt16 MSG_ITEMINFOEX = MSG_GENERAL + 108;
        protected const UInt16 MSG_NPCINFOEX = MSG_GENERAL + 109;
        protected const UInt16 MSG_MAPINFO = MSG_GENERAL + 110;
        protected const UInt16 MSG_MESSAGEBOARD = MSG_GENERAL + 111;
        protected const UInt16 MSG_SYNMEMBERINFO = MSG_GENERAL + 112;
        protected const UInt16 MSG_DICE = MSG_GENERAL + 113;
        protected const UInt16 MSG_SYNINFO = MSG_GENERAL + 114;

        protected const UInt16 MSG_NPCINFO = MSG_GENERAL + 1030;
        protected const UInt16 MSG_NPC = MSG_GENERAL + 1031;
        protected const UInt16 MSG_DIALOG = MSG_GENERAL + 1032;
        protected const UInt16 MSG_FRIENDINFO = MSG_GENERAL + 1033;
        protected const UInt16 MSG_PETINFO = MSG_GENERAL + 1035;
        protected const UInt16 MSG_DATAARRAY = MSG_GENERAL + 1036;
    }
}
