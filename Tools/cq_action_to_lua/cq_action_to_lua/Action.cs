using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MySql.Data.MySqlClient;

namespace CO2Tools
{
    public enum ActionID : uint
    {
        ACTION_SYS_FIRST = 100,
        ACTION_MENUTEXT = 101,
        ACTION_MENULINK = 102,
        ACTION_MENUEDIT = 103,
        ACTION_MENUPIC = 104,
        ACTION_MENUBUTTON = 110,
        ACTION_MENULISTPART = 111,
        ACTION_MENUCREATE = 120,
        ACTION_RAND = 121,
        ACTION_RANDACTION = 122,
        ACTION_CHKTIME = 123,
        ACTION_POSTCMD = 124,
        ACTION_BROCASTMSG = 125,
        ACTION_MESSAGEBOX = 126,
        ACTION_SQL_EXE = 127,
        ACTION_SYS_LIMIT = 199,
        ACTION_NPC_FIRST = 200,
        ACTION_NPC_ATTR = 201,
        ACTION_NPC_ERASE = 205,
        ACTION_NPC_MODIFY = 206,
        ACTION_NPC_RESETSYNOWNER = 207,
        ACTION_NPC_FIND_NEXT_TABLE = 208,
        ACTION_NPC_ADD_TABLE = 209,
        ACTION_NPC_DEL_TABLE = 210,
        ACTION_NPC_DEL_INVALID = 211,
        ACTION_NPC_TABLE_AMOUNT = 212,
        ACTION_NPC_SYS_AUCTION = 213,
        ACTION_NPC_DRESS_SYNCLOTHING = 214,
        ACTION_NPC_TAKEOFF_SYNCLOTHING = 215,
        ACTION_NPC_AUCTIONING = 216,
        ACTION_NPC_LIMIT = 299,
        ACTION_MAP_FIRST = 300,
        ACTION_MAP_MOVENPC = 301,
        ACTION_MAP_MAPUSER = 302,
        ACTION_MAP_BROCASTMSG = 303,
        ACTION_MAP_DROPITEM = 304,
        ACTION_MAP_SETSTATUS = 305,
        ACTION_MAP_ATTRIB = 306,
        ACTION_MAP_REGION_MONSTER = 307,
        ACTION_MAP_CHANGEWEATHER = 310,
        ACTION_MAP_CHANGELIGHT = 311,
        ACTION_MAP_MAPEFFECT = 312,
        ACTION_MAP_CREATEMAP = 313,
        ACTION_MAP_FIREWORKS = 314,
        ACTION_MAP_LIMIT = 399,
        ACTION_ITEMONLY_FIRST = 400,
        ACTION_ITEM_REQUESTLAYNPC = 401,
        ACTION_ITEM_COUNTNPC = 402,
        ACTION_ITEM_LAYNPC = 403,
        ACTION_ITEM_DELTHIS = 498,
        ACTION_ITEMONLY_LIMIT = 499,
        ACTION_ITEM_FIRST = 500,
        ACTION_ITEM_ADD = 501,
        ACTION_ITEM_DEL = 502,
        ACTION_ITEM_CHECK = 503,
        ACTION_ITEM_HOLE = 504,
        ACTION_ITEM_REPAIR = 505,
        ACTION_ITEM_MULTIDEL = 506,
        ACTION_ITEM_MULTICHK = 507,
        ACTION_ITEM_LEAVESPACE = 508,
        ACTION_ITEM_UPEQUIPMENT = 509,
        ACTION_ITEM_EQUIPTEST = 510,
        ACTION_ITEM_EQUIPEXIST = 511,
        ACTION_ITEM_EQUIPCOLOR = 512,
        ACTION_ITEM_FIND = 513,
        ACTION_ENCASH_CHIP = 514,
        ACTION_ITEM_EQP_OPT = 517,
        ACTION_ITEM_LIMIT = 599,
        ACTION_NPCONLY_FIRST = 600,
        ACTION_NPCONLY_CREATENEW_PET = 601,
        ACTION_NPCONLY_DELETE_PET = 602,
        ACTION_NPCONLY_MAGICEFFECT = 603,
        ACTION_NPCONLY_MAGICEFFECT2 = 604,
        ACTION_NPCONLY_LIMIT = 699,
        ACTION_SYN_FIRST = 700,
        ACTION_SYN_CREATE = 701,
        ACTION_SYN_DESTROY = 702,
        ACTION_SYN_DONATE = 703,
        ACTION_SYN_CREATE_SUB = 708,
        ACTION_SYN_COMBINE_SUB = 710,
        ACTION_SYN_ATTR = 717,
        ACTION_SYN_ALLOCATE_SYNFUND = 729,
        ACTION_SYN_RENAME = 731,
        ACTION_SYN_DEMISE = 704,
        ACTION_SYN_SET_ASSISTANT = 705,
        ACTION_SYN_CLEAR_RANK = 706,
        ACTION_SYN_PRESENT_MONEY = 707,
        ACTION_SYN_CHANGE_LEADER = 709,
        ACTION_SYN_ANTAGONIZE = 711,
        ACTION_SYN_CLEAR_ANTAGONIZE = 712,
        ACTION_SYN_ALLY = 713,
        ACTION_SYN_CLEAR_ALLY = 714,
        ACTION_SYN_KICKOUT_MEMBER = 715,
        ACTION_SYN_CREATENEW_PET = 716,
        ACTION_SYN_CHANGESYN = 718,
        ACTION_SYN_CHANGE_SUBNAME = 719,
        ACTION_SYN_FIND_NEXT_SYN = 720,
        ACTION_SYN_FIND_BY_NAME = 721,
        ACTION_SYN_FIND_NEXT_SYNMEMBER = 722,
        ACTION_SYN_SAINT = 724,
        ACTION_SYN_RANK = 726,
        ACTION_SYN_UPMEMBERLEVEL = 728,
        ACTION_SYN_APPLLY_ATTACKSYN = 730,
        ACTION_SYN_LIMIT = 799,
        ACTION_MST_FIRST = 800,
        ACTION_MST_DROPITEM = 801,
        ACTION_MST_MAGIC = 802,
        ACTION_MST_LIMIT = 899,
        ACTION_USER_FIRST = 1000,
        ACTION_USER_ATTR = 1001,
        ACTION_USER_FULL = 1002,
        ACTION_USER_CHGMAP = 1003,
        ACTION_USER_RECORDPOINT = 1004,
        ACTION_USER_HAIR = 1005,
        ACTION_USER_CHGMAPRECORD = 1006,
        ACTION_USER_CHGLINKMAP = 1007,
        ACTION_USER_TALK = 1010,
        ACTION_USER_MAGIC = 1020,
        ACTION_USER_WEAPONSKILL = 1021,
        ACTION_USER_LOG = 1022,
        ACTION_USER_BONUS = 1023,
        ACTION_USER_DIVORCE = 1024,
        ACTION_USER_MARRIAGE = 1025,
        ACTION_USER_SEX = 1026,
        ACTION_USER_EFFECT = 1027,
        ACTION_USER_TASKMASK = 1028,
        ACTION_USER_MEDIAPLAY = 1029,
        ACTION_USER_SUPERMANLIST = 1030,
        ACTION_USER_CHKIN_CARD = 1031,
        ACTION_USER_CHKOUT_CARD = 1032,
        ACTION_USER_CREATEMAP = 1033,
        ACTION_USER_ENTER_HOME = 1034,
        ACTION_USER_ENTER_MATE_HOME = 1035,
        ACTION_USER_CHKIN_CARD2 = 1036,
        ACTION_USER_CHKOUT_CARD2 = 1037,
        ACTION_USER_FLY_NEIGHBOR = 1038,
        ACTION_USER_UNLEARN_MAGIC = 1039,
        ACTION_USER_REBIRTH = 1040,
        ACTION_USER_WEBPAGE = 1041,
        ACTION_USER_BBS = 1042,
        ACTION_USER_UNLEARN_SKILL = 1043,
        ACTION_USER_DROP_MAGIC = 1044,
        ACTION_USER_RESET_MAGIC = 1045,
        ACTION_USER_OPEN_DIALOG = 1046,
        ACTION_USER_CHGMAP_REBORN = 1047,
        ACTION_USER_ADD_WPG_BADGE = 1048,
        ACTION_USER_DEL_WPG_BADGE = 1049,
        ACTION_USER_CHK_WPG_BADGE = 1050,
        ACTION_USER_TAKESTUDENTEXP = 1051,
        ACTION_USER_LOCK_CHK = 1052,
        ACTION_USER_LOCK_SET = 1053,
        ACTION_USER_OPEN_STORAGE = 1054,
        ACTION_USER_DATAVAR_CHK = 1060,
        ACTION_USER_DATAVAR_SET = 1061,
        ACTION_USER_STRVAR_CHK = 1062,
        ACTION_USER_STRVAR_SET = 1063,
        ACTION_USER_DATAVAR_CAL = 1064,
        ACTION_USER_EQPCHK = 1065,
        ACTION_USER_TIMER = 1071,
        ACTION_USER_SCREFFECT = 1072,
        ACTION_USER_STATISTIC_CHK = 1073,
        ACTION_USER_STATISTIC_SET = 1074,
        ACTION_USER_DB_FIELD = 1077,
        ACTION_USER_TASK_MANAGER = 1080,
        ACTION_USER_TASK_OPE = 1081,
        ACTION_USER_TASK_LOCALTIME = 1082,
        ACTION_USER_TASK_FIND = 1083,
        ACTION_USER_CUSTOM_LOG = 1085,
        ACTION_USER_TIME_TO_EXP = 1086,
        ACTION_TEAM_BROADCAST = 1101,
        ACTION_TEAM_ATTR = 1102,
        ACTION_TEAM_LEAVESPACE = 1103,
        ACTION_TEAM_ITEM_ADD = 1104,
        ACTION_TEAM_ITEM_DEL = 1105,
        ACTION_TEAM_ITEM_CHECK = 1106,
        ACTION_TEAM_CHGMAP = 1107,
        ACTION_TEAM_CHK_ISLEADER = 1501,
        ACTION_USER_LIMIT = 1999,
        ACTION_EVENT_FIRST = 2000,
        ACTION_EVENT_SETSTATUS = 2001,
        ACTION_EVENT_DELNPC_GENID = 2002,
        ACTION_EVENT_COMPARE = 2003,
        ACTION_EVENT_COMPARE_UNSIGNED = 2004,
        ACTION_EVENT_CHANGEWEATHER = 2005,
        ACTION_EVENT_CREATEPET = 2006,
        ACTION_EVENT_CREATENEW_NPC = 2007,
        ACTION_EVENT_COUNTMONSTER = 2008,
        ACTION_EVENT_DELETEMONSTER = 2009,
        ACTION_EVENT_BBS = 2010,
        ACTION_EVENT_ERASE = 2011,
        ACTION_EVENT_LIMIT = 2099,
        ACTION_TRAP_FIRST = 2100,
        ACTION_TRAP_CREATE = 2101,
        ACTION_TRAP_ERASE = 2102,
        ACTION_TRAP_COUNT = 2103,
        ACTION_TRAP_ATTR = 2104,
        ACTION_TRAP_LIMIT = 2199,
        ACTION_WANTED_FIRST = 3000,
        ACTION_WANTED_NEXT = 3001,
        ACTION_WANTED_NAME = 3002,
        ACTION_WANTED_BONUTY = 3003,
        ACTION_WANTED_NEW = 3004,
        ACTION_WANTED_ORDER = 3005,
        ACTION_WANTED_CANCEL = 3006,
        ACTION_WANTED_MODIFYID = 3007,
        ACTION_WANTED_SUPERADD = 3008,
        ACTION_POLICEWANTED_NEXT = 3010,
        ACTION_POLICEWANTED_ORDER = 3011,
        ACTION_POLICEWANTED_CHECK = 3012,
        ACTION_WANTED_LIMIT = 3099,
        ACTION_MAGIC_FIRST = 4000,
        ACTION_MAGIC_ATTACHSTATUS = 4001,
        ACTION_MAGIC_ATTACK = 4002,
        ACTION_MAGIC_LIMIT = 4099
    }

    class Action
    {
        public UInt32 Id;
        public UInt32 IdNext;
        public UInt32 IdNext_Fail;
        public UInt32 Type;
        public Int32 Data;
        public String Param;

        public static Action getAction(UInt32 actionId)
        {
            if (actionId == 0)
                return null;

            Action action = new Action();

            using (MySqlConnection connection = new MySqlConnection("Server=" + Program.HOST + ";Database='" + Program.DATABASE + "';Username='" + Program.USERNAME + "';Password='" + Program.PASSWORD + "';"))
            {
                MySqlCommand cmd = null;
                MySqlDataReader reader = null;

                connection.Open();

                cmd = new MySqlCommand("SELECT `id`, `id_next`, `id_nextfail`, `type`, `data`, `param` FROM `cq_action` WHERE `id` = " + actionId, connection);
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    action.Id = Convert.ToUInt32(reader["id"]);
                    action.IdNext = Convert.ToUInt32(reader["id_next"]);
                    action.IdNext_Fail = Convert.ToUInt32(reader["id_nextfail"]);
                    action.Type = Convert.ToUInt32(reader["type"]);
                    action.Data = Convert.ToInt32(reader["data"]);
                    action.Param = Convert.ToString(reader["param"]);
                }
                else
                {
                    Console.WriteLine("Missing action {0}", actionId);
                    return null;
                }
            }

            return action;
        }

        public static void processAction(StreamWriter stream, String ident, IObj obj, UInt32 actionId)
        {
            Action action = getAction(actionId);
            if (action == null)
                return;

            // ACTION_SQL_EXE is only used to insert logs in the cq_log table...
            if (action.Type == (UInt32)ActionID.ACTION_SQL_EXE)
                return;

            // ACTION_USER_DB_FIELD is only used to insert gmlog with emoney_buy
            if (action.Type == (UInt32)ActionID.ACTION_USER_DB_FIELD)
                return;

            // ACTION_USER_CUSTOM_LOG is only used to insert gmlog with emoney_buy
            if (action.Type == (UInt32)ActionID.ACTION_USER_CUSTOM_LOG)
                return;

            string param = "";
            replaceAttrStr(ref param, action.Param);

            if (action.IdNext_Fail != 0)
                stream.Write(ident + "if ");

            UInt32 type = action.Type;
            if (type > (UInt32)ActionID.ACTION_SYS_FIRST && type < (UInt32)ActionID.ACTION_SYS_LIMIT)
                processActionSys(stream, action.IdNext_Fail != 0 ? "" : ident, obj, action, param);
            else if (type > (UInt32)ActionID.ACTION_NPC_FIRST && type < (UInt32)ActionID.ACTION_NPC_LIMIT)
                processActionNpc(stream, action.IdNext_Fail != 0 ? "" : ident, obj, action, param);
            else if (type > (UInt32)ActionID.ACTION_MAP_FIRST && type < (UInt32)ActionID.ACTION_MAP_LIMIT)
                processActionMap(stream, action.IdNext_Fail != 0 ? "" : ident, obj, action, param);
            else if (type > (UInt32)ActionID.ACTION_ITEMONLY_FIRST && type < (UInt32)ActionID.ACTION_ITEMONLY_LIMIT)
                processActionItemOnly(stream, action.IdNext_Fail != 0 ? "" : ident, obj, action, param);
            else if (type > (UInt32)ActionID.ACTION_ITEM_FIRST && type < (UInt32)ActionID.ACTION_ITEM_LIMIT)
                processActionItem(stream, action.IdNext_Fail != 0 ? "" : ident, obj, action, param);
            else if (type > (UInt32)ActionID.ACTION_NPCONLY_FIRST && type < (UInt32)ActionID.ACTION_NPCONLY_LIMIT)
                processActionNpcOnly(stream, action.IdNext_Fail != 0 ? "" : ident, obj, action, param);
            else if (type > (UInt32)ActionID.ACTION_SYN_FIRST && type < (UInt32)ActionID.ACTION_SYN_LIMIT)
                processActionSyn(stream, action.IdNext_Fail != 0 ? "" : ident, obj, action, param);
            else if (type > (UInt32)ActionID.ACTION_USER_FIRST && type < (UInt32)ActionID.ACTION_USER_LIMIT)
                processActionUser(stream, action.IdNext_Fail != 0 ? "" : ident, obj, action, param);
            else if (type > (UInt32)ActionID.ACTION_EVENT_FIRST && type < (UInt32)ActionID.ACTION_EVENT_LIMIT)
                processActionEvent(stream, action.IdNext_Fail != 0 ? "" : ident, obj, action, param);
            else if (type > (UInt32)ActionID.ACTION_TRAP_FIRST && type < (UInt32)ActionID.ACTION_TRAP_LIMIT)
                processActionEvent(stream, action.IdNext_Fail != 0 ? "" : ident, obj, action, param);
            else if (type > (UInt32)ActionID.ACTION_WANTED_FIRST && type < (UInt32)ActionID.ACTION_WANTED_LIMIT)
                processActionWanted(stream, action.IdNext_Fail != 0 ? "" : ident, obj, action, param);
            else if (type > (UInt32)ActionID.ACTION_MST_FIRST && type < (UInt32)ActionID.ACTION_MST_LIMIT)
                processActionMonster(stream, action.IdNext_Fail != 0 ? "" : ident, obj, action, param);
            else if (type > (UInt32)ActionID.ACTION_MAP_FIRST && type < (UInt32)ActionID.ACTION_MAGIC_LIMIT)
                processActionMagic(stream, action.IdNext_Fail != 0 ? "" : ident, obj, action, param);
            else
            {
                Console.WriteLine("Invalid action type {0} !", type);
            }

            if (action.IdNext_Fail != 0)
                stream.Write(" then");
            stream.WriteLine();

            if (action.IdNext_Fail != 0)
            {
                stream.WriteLine();
                processAction(stream, ident + "    ", obj, action.IdNext);
                stream.WriteLine();
                stream.WriteLine(ident + "else");
                stream.WriteLine();
                processAction(stream, ident + "    ", obj, action.IdNext_Fail);
                stream.WriteLine();
                stream.WriteLine(ident + "end");
            }
            else
                processAction(stream, ident, obj, action.IdNext);
        }

        private static void processActionSys(StreamWriter stream, String ident, IObj obj, Action action, string param)
        {
            switch (action.Type)
            {
                case (UInt32)ActionID.ACTION_MENUTEXT:
                    {
                        stream.Write(ident + "text(client, \"{0}\")", param.Replace('~', ' '));
                        break;
                    }
                case (UInt32)ActionID.ACTION_MENULINK:
                    {
                        string[] parts = param.Replace("  ", " ").Split(' ');
                        string text = parts[0].Replace('~', ' ');
                        UInt32 idTask = Convert.ToUInt32(parts[1]);
                        Int32 align = 0;

                        if (parts.Length == 3)
                            align = Convert.ToInt32(parts[2]);

                        stream.Write(ident + "link(client, \"{0}\", {1})", text, obj.pushTaskId(idTask));
                        break;
                    }
                case (UInt32)ActionID.ACTION_MENUEDIT:
                    {
                        string[] parts = param.Replace("  ", " ").Split(' ');

                        int nAcceptLen = int.Parse(parts[0]);
                        UInt32 idTask = UInt32.Parse(parts[1]);
                        string text = "";
                        
                        if (parts.Length == 3)
                            text = parts[2].Replace('~', ' ');

                        stream.Write(ident + "edit(client, \"{0}\", {1}, {2})", text, obj.pushTaskId(idTask), nAcceptLen);

                        //CMsgDialog msg;
                        //CHECKF(msg.Create(MSGDIALOG_EDIT, szText, pUser->PushTaskID(idTask), nAcceptLen));
                        //pUser->SendMsg(&msg);

                        break;
                    }
                case (UInt32)ActionID.ACTION_MENUPIC:
                    {
                        string[] parts = param.Split(' ');
                        Int32 x = Convert.ToInt32(parts[0]);
                        Int32 y = Convert.ToInt32(parts[1]);
                        UInt32 idPic = Convert.ToUInt32(parts[2]);
                        UInt32 idTask = 0;
                        
                        if (parts.Length == 4)
                            idTask = Convert.ToUInt32(parts[3]);

                        stream.Write(ident + "pic(client, {0})", idPic);
                        break;
                    }
                case (UInt32)ActionID.ACTION_MENUCREATE:
                    {
                        UInt32 idTask = 0;
                        
                        //Convert.ToUInt32(param);

                        stream.Write(ident + "create(client)");
                        break;
                    }
                case (UInt32)ActionID.ACTION_BROCASTMSG:
                    {
                        if (String.IsNullOrWhiteSpace(param))
                            param = action.Param;

                        stream.Write(ident + "broadcastSysMsg(client, \"{0}\", {1})", param.Replace('~', ' '), action.Data != 0 ? action.Data : 2000);
                        break;
                    }
                case (UInt32)ActionID.ACTION_RAND:
                    {
                        string[] parts = param.Split(' ');
                        Int32 data1 = Convert.ToInt32(parts[0]);
                        Int32 data2 = Convert.ToInt32(parts[1]);

                        stream.Write(ident + "(rand(client, {0}) < {1})", data2, data1);
                        break;
                    }
                case (UInt32)ActionID.ACTION_RANDACTION:
                    {
                        string[] parts = param.Split(' ');
                        UInt32 action1 = Convert.ToUInt32(parts[0]);
                        UInt32 action2 = Convert.ToUInt32(parts[1]);
                        UInt32 action3 = Convert.ToUInt32(parts[2]);
                        UInt32 action4 = Convert.ToUInt32(parts[3]);
                        UInt32 action5 = Convert.ToUInt32(parts[4]);
                        UInt32 action6 = Convert.ToUInt32(parts[5]);
                        UInt32 action7 = Convert.ToUInt32(parts[6]);
                        UInt32 action8 = Convert.ToUInt32(parts[7]);

                        byte count = 8;
                        if (action8 == action.Id) // stack overflow otherwise...
                            --count;

                        stream.Write(ident + "action = randomAction(client, 1, {0})", count);

                        stream.WriteLine();
                        stream.WriteLine(ident + "if action == 1 then");
                        Action.processAction(stream, ident + "    ", obj, action1);
                        stream.WriteLine(ident + "elseif action == 2 then");
                        Action.processAction(stream, ident + "    ", obj, action2);
                        stream.WriteLine(ident + "elseif action == 3 then");
                        Action.processAction(stream, ident + "    ", obj, action3);
                        stream.WriteLine(ident + "elseif action == 4 then");
                        Action.processAction(stream, ident + "    ", obj, action4);
                        stream.WriteLine(ident + "elseif action == 5 then");
                        Action.processAction(stream, ident + "    ", obj, action5);
                        stream.WriteLine(ident + "elseif action == 6 then");
                        Action.processAction(stream, ident + "    ", obj, action6);
                        stream.WriteLine(ident + "elseif action == 7 then");
                        Action.processAction(stream, ident + "    ", obj, action7);
                        if (count >= 8)
                        {
                            stream.WriteLine(ident + "elseif action == 8 then");
                            Action.processAction(stream, ident + "    ", obj, action8);
                        }
                        stream.WriteLine(ident + "end");

                        //            ProcessAction(idAction[::RandGet(8)], pUser, pRole, pItem, pszAccept);
                        //            return true;
                        break;
                    }
                case (UInt32)ActionID.ACTION_CHKTIME:
                    {
                        stream.Write(ident + "checkTime(client, {0}, \"{1}\")", action.Data, param);
                        // See MapGroup.cpp:CheckTime()
                        break;
                    }
                case (UInt32)ActionID.ACTION_POSTCMD:
                    {
                        stream.Write(ident + "postCmd(client, {0})", action.Data);
                        break;
                        //CMsgAction msg;
                        //IF_OK (msg.Create(	pUser->GetID(), 
                        //                    pUser->GetPosX(), 
                        //                    pUser->GetPosY(), 
                        //                    pUser->GetDir(), 
                        //                    actionPostCmd,
                        //                    pAction->GetInt(ACTIONDATA_DATA) ))
                        //    pUser->SendMsg(&msg);

                        //return true;
                    }
                default:
                    {
                        Program.MISSING_ACTIONS.TryAdd(action.Type, 0);
                        Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);
                        break;
                    }
            }
        }

        private static void processActionNpc(StreamWriter stream, String ident, Object obj, Action action, string param)
        {
            switch (action.Type)
            {
                case (UInt32)ActionID.ACTION_NPC_ATTR:
                    {
                        string[] parts = param.Split(' ');
                        string attr = parts[0];
                        string opt = parts[1];
                        string data = parts[2];
                        uint npcId = uint.Parse(parts[3]);

                        Program.MISSING_ACTIONS.TryAdd(action.Type, 0);
                        Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);
                        break;
                    }
                default:
                    {
                        Program.MISSING_ACTIONS.TryAdd(action.Type, 0);
                        Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);
                        break;
                    }
            }
        }

        private static void processActionMap(StreamWriter stream, String ident, Object obj, Action action, string param)
        {
            switch (action.Type)
            {
                case (UInt32)ActionID.ACTION_MAP_MOVENPC:
                    {
                        Int32 npcId = action.Data;

                        string[] parts = param.Split(' ');
                        UInt32 mapId = Convert.ToUInt32(parts[0]);
                        Int32 x = Convert.ToInt32(parts[1]);
                        Int32 y = Convert.ToInt32(parts[2]);

                        stream.Write(ident + "moveNpc(client, {0}, {1}, {2}, {3})", npcId, mapId, x, y);
                        break;
                    }
                case (UInt32)ActionID.ACTION_MAP_MAPUSER:
                    {
                        string[] parts = param.Split(' ');

                        string cmd = parts[0], opt = parts[1];
                        int data = Convert.ToInt32(parts[2]);

                        int mapId = action.Data;

                        if (cmd == "map_user")
                        {
                            if (opt == "==")
                                stream.Write(ident + "playersOnMap(client, {0}) == {1}", mapId, data);
                            else if (opt == "<=")
                                stream.Write(ident + "playersOnMap(client, {0}) <= {1}", mapId, data);
                            else if (opt == ">=")
                                stream.Write(ident + "playersOnMap(client, {0}) >= {1}", mapId, data);
                            else
                                Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);
                        }
                        else if (cmd == "alive_user")
                        {
                            if (opt == "==")
                                stream.Write(ident + "alivePlayersOnMap(client, {0}) == {1}", mapId, data);
                            else if (opt == "<=")
                                stream.Write(ident + "alivePlayersOnMap(client, {0}) <= {1}", mapId, data);
                            else if (opt == ">=")
                                stream.Write(ident + "alivePlayersOnMap(client, {0}) >= {1}", mapId, data);
                            else
                                Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);
                        }
                        else
                            Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);

                        break;
                    }
                case (UInt32)ActionID.ACTION_MAP_MAPEFFECT:
                    {
                        string[] parts = param.Split(' ');
                        UInt32 mapId = Convert.ToUInt32(parts[0]);
                        Int32 x = Convert.ToInt32(parts[1]);
                        Int32 y = Convert.ToInt32(parts[2]);
                        string effect = parts[3];

                        stream.Write(ident + "broadcastMapEffect(client, {0}, {1}, {2}, \"{3}\")", mapId, x, y, effect);
                        break;
                        //if (4 == sscanf(szParam, "%u %d %d %s", &idMap, &nPosX, &nPosY, szEffect))
                        //{
                        //    CGameMap* pMap = MapManager()->QueryMap(idMap);
                        //    if (pMap)
                        //    {
                        //        CMsgName msg;
                        //        IF_OK (msg.Create(NAMEACT_MAPEFFECT, szEffect, nPosX, nPosY))
                        //            pMap->BroadcastBlockMsg(nPosX, nPosY, &msg);

                        //        return true;
                        //    }
                        //}
                        //else
                        //    LOGERROR("ACTION %u: ´íÎóµÄ²ÎÊýÊýÁ¿", pAction->GetID());
                    }
                case (UInt32)ActionID.ACTION_MAP_FIREWORKS:
                    {
                        stream.Write(ident + "broadcastMapFireworks(client)");
                        break;

                        //IF_OK (pUser)
                        //{
                        //    CMsgItem msg;
                        //    IF_OK (msg.Create(pUser->GetID(), ITEMACT_FIREWORKS))
                        //        pUser->BroadcastRoomMsg(&msg, true);

                        //    return true;
                        //}
                    }
                case (UInt32)ActionID.ACTION_MAP_BROCASTMSG:
                    {
                        if (String.IsNullOrWhiteSpace(param))
                            param = action.Param;

                        stream.Write(ident + "broadcastMapMsg(client, {2}, \"{0}\", {1})",
                            param.Replace('~', ' '), 2005, action.Data);
                        break;
                    }
                case (UInt32)ActionID.ACTION_MAP_ATTRIB:
                    {
                        string[] parts = param.Split(' ');
                        string field = parts[0];
                        string opt = parts[1];
                        int data = int.Parse(parts[2]);
                        uint mapId = 0;

                        if (parts.Length == 4)
                            mapId = uint.Parse(parts[3]);

                        if (field == "mapdoc")
                        {
                            if (opt == "==")
                                stream.Write(ident + "getMapAttrib(client, {0}, \"{1}\") == {2}", mapId, field, data);
                            else
                                Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);
                        }
                        else
                            Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);

                        break;
                    }
                default:
                    {
                        Program.MISSING_ACTIONS.TryAdd(action.Type, 0);
                        Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);
                        break;
                    }
            }
        }

        private static void processActionItemOnly(StreamWriter stream, String ident, Object obj, Action action, string param)
        {
            switch (action.Type)
            {
                case (UInt32)ActionID.ACTION_ITEM_DELTHIS:
                    {
                        stream.Write(ident + "deleteItem(self)");
                        break;
                    }
                default:
                    {
                        Program.MISSING_ACTIONS.TryAdd(action.Type, 0);
                        Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);
                        break;
                    }
            }
        }

        private static void processActionItem(StreamWriter stream, String ident, Object obj, Action action, string param)
        {
            switch (action.Type)
            {
                case (UInt32)ActionID.ACTION_ITEM_ADD:
                    {
                        string item = action.Data.ToString();

                        string[] parts = param.Split(' ');
                        if (parts.Length == 8)
                        {
                            int amount, amountLimit, _ident, gem1, gem2, magic1, magic2, magic3;

                            amount = int.Parse(parts[0]);
                            amountLimit = int.Parse(parts[1]);
                            _ident = int.Parse(parts[2]);
                            gem1 = int.Parse(parts[3]);
                            gem2 = int.Parse(parts[4]);
                            magic1 = int.Parse(parts[5]);
                            magic2 = int.Parse(parts[6]);
                            magic3 = int.Parse(parts[7]);

                            item = String.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10}",
                                action.Data, amount, amountLimit, _ident, gem1, gem2, magic1, magic2, magic3, 0, 0);
                        }
                        else if (parts.Length != 0 && parts.Length != 1)
                            Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);

                        stream.Write(ident + "awardItem(client, \"{0}\", 1)", item);
                        break;
                    }
                case (UInt32)ActionID.ACTION_ITEM_DEL:
                    {
                        int num = 1;
                        if (action.Data != 0)
                        {
                            Int32 nParam = 0;
                            Int32.TryParse(param, out nParam);

                            num = Math.Max(1, nParam);
                            stream.Write(ident + "spendItem(client, {0}, {1})", action.Data, num);
                        }
                        else
                        {
                            stream.Write(ident + "spendTaskItem(client, \"{0}\")", param);
                        }
                        break;
                    }
                case (UInt32)ActionID.ACTION_ITEM_CHECK:
                    {
                        int num = 1;
                        if (action.Data != 0)
                        {
                            Int32 nParam = 0;
                            Int32.TryParse(param, out nParam);

                            num = Math.Max(1, nParam);
                            stream.Write(ident + "hasItem(client, {0}, {1})", action.Data, num);
                        }
                        else
                            Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);
                        break;
                    }
                case (UInt32)ActionID.ACTION_ITEM_MULTIDEL:
                    {
                        string[] parts = param.Split(' ');
                        UInt32 type1 = Convert.ToUInt32(parts[0]);
                        UInt32 type2 = Convert.ToUInt32(parts[1]);
                        Int32 num = Convert.ToInt32(parts[2]);

                        if (type1 == type2)
                            stream.Write(ident + "spendItem(client, {0}, {1})", type1, num);
                        else
                            stream.Write(ident + "spendItems(client, {0}, {1}, {2})",
                                Math.Min(type1, type2), Math.Max(type1, type2), num);
                        break;
                    }
                case (UInt32)ActionID.ACTION_ITEM_MULTICHK:
                    {
                        string[] parts = param.Split(' ');
                        UInt32 type1 = Convert.ToUInt32(parts[0]);
                        UInt32 type2 = Convert.ToUInt32(parts[1]);
                        Int32 num = Convert.ToInt32(parts[2]);

                        if (type1 == type2)
                            stream.Write(ident + "hasItem(client, {0}, {1})", type1, num);
                        else
                            stream.Write(ident + "hasItems(client, {0}, {1}, {2})",
                                Math.Min(type1, type2), Math.Max(type1, type2), num);
                        break;
                    }
                case (UInt32)ActionID.ACTION_ITEM_HOLE:
                    {
                        string[] parts = param.Split(' ');
                        string cmd = parts[0];
                        int data = int.Parse(parts[1]);

                        if (cmd == "ChkHole")
                            stream.Write(ident + "chkEquipHole(client, {0})", data);
                        else if (cmd == "MakeHole")
                            stream.WriteLine(ident + "makeEquipHole(client, {0})", data);
                        else
                            Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);

                        break;
                    }
                case (UInt32)ActionID.ACTION_ITEM_LEAVESPACE:
                    {
                        if (param != "")
                            Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);

                        int count = 40 - action.Data;
                        stream.Write(ident + "(getItemsCount(client) <= {0})", count);
                        break;
                    }
                case (UInt32)ActionID.ACTION_ITEM_UPEQUIPMENT:
                    {
                        string[] parts = param.Split(' ');
                        string cmd = parts[0];
                        int pos = int.Parse(parts[1]);

                        if (cmd == "up_lev")
                            stream.Write(ident + "upEquipLevel(client, {0})", pos);
                        else if (cmd == "up_quality")
                            stream.Write(ident + "upEquipQuality(client, {0})", pos);
                        else if (cmd == "recover_dur")
                            stream.Write(ident, "recoverEquipDura(client, {0})", pos);
                        else
                            Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);

                        break;
                    }
                case (UInt32)ActionID.ACTION_ITEM_EQUIPTEST:
                    {
                        string[] parts = param.Split(' ');
                        
                        int equipType = int.Parse(parts[0]);
                        string cmd = parts[1];
                        string opt = parts[2];
                        int data = int.Parse(parts[3]);

                        //if (cmd == "level")
                        //{

                        //}
                        if (cmd == "quality")
                        {
                            if (opt == "==")
                                stream.Write("getEquipByPos(client, {0}) % 10 == {1}", equipType, data);
                            else if (opt == "<=")
                                stream.Write("getEquipByPos(client, {0}) % 10 <= {1}", equipType, data);
                            else if (opt == ">=")
                                stream.Write("getEquipByPos(client, {0}) % 10 >= {1}", equipType, data);
                            else
                                Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);
                        }
                        //else if (cmd == "durability")
                        //{

                        //}
                        //else if (cmd == "max_dur")
                        //{

                        //}
                        else
                            Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);
                //CItem* pItem = pUser->GetEquipItemByPos(nEquipType);
                //if (pItem)
                //{
                //    int nTestData = 0;
                //    if (0 == stricmp("level", szCmd))
                //    {	
                //        nTestData = pItem->GetLevel();
                //    }
                //    else if (0 == stricmp("durability", szCmd))
                //    {
                //        if (-1 == nData)
                //            nData = pItem->GetInt(ITEMDATA_AMOUNTLIMIT)/100;

                //        nTestData = pItem->GetInt(ITEMDATA_AMOUNT)/100;
                //    }
                //    else if (0 == stricmp("max_dur", szCmd))
                //    {
                //        if (-1 == nData)
                //            nData = pItem->GetInt(ITEMDATA_AMOUNTLIMIT_ORIGINAL)/100;

                //        nTestData = (pItem->GetInt(ITEMDATA_AMOUNTLIMIT)*100/(100+pItem->GetGemDurEffect()))/100;
                //    }
                        break;
                    }
                case (UInt32)ActionID.ACTION_ITEM_EQUIPEXIST:
                    {
                        stream.Write(ident + "getEquipByPos(client, {0}) ~= 0", action.Data);
                        //CItem* pEquip = pUser->GetEquipItemByPos(pAction->GetInt(ACTIONDATA_DATA));
                        //if (pEquip)
                        //    return true;
                        break;
                    }
                case (UInt32)ActionID.ACTION_ITEM_EQP_OPT:
                    {
                        string[] parts = param.Split(' ');

                        int pos = int.Parse(parts[0]);
                        int data = int.Parse(parts[1]);
                        string opt = parts[2]; // set, +=, ==, >=
                        int value = int.Parse(parts[3]);

                        bool sync = false;
                        if (parts.Length == 5)
                            sync = int.Parse(parts[4]) != 0;

                        if (data == 14) // bless
                        {
                            Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);
                        }
                        else
                            Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);

                        break;
                    }
                default:
                    {
                        Program.MISSING_ACTIONS.TryAdd(action.Type, 0);
                        Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);
                        break;
                    }
            }
        }

        private static void processActionNpcOnly(StreamWriter stream, String ident, Object obj, Action action, string param)
        {
            switch (action.Type)
            {
                default:
                    {
                        Program.MISSING_ACTIONS.TryAdd(action.Type, 0);
                        Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);
                        break;
                    }
            }
        }

        private static void processActionSyn(StreamWriter stream, String ident, Object obj, Action action, string param)
        {
            switch (action.Type)
            {
                case (UInt32)ActionID.ACTION_SYN_CREATE:
                    {
                        string[] parts = param.Split(' ');
                        uint level = uint.Parse(parts[0]);
                        uint money = uint.Parse(parts[1]);
                        uint moneyLeave = uint.Parse(parts[2]);

                        stream.Write(ident + "createClan(client, {0}, {1}, {2})",
                            level, money, moneyLeave);
                        break;
                    }
                case (UInt32)ActionID.ACTION_SYN_DESTROY:
                    {
                        stream.Write(ident + "destroyClan(client)");
                        break;
                    }
                case (UInt32)ActionID.ACTION_SYN_DONATE:
                    {
                        stream.Write(ident + "donateToClan(client)");
                        break;
                    }
                case (UInt32)ActionID.ACTION_SYN_DEMISE:
                    {
                        stream.Write(ident + "demiseFromClan(client)");
                        break;
                    }
                case (UInt32)ActionID.ACTION_SYN_SET_ASSISTANT:
                    {
                        stream.Write(ident + "setClanAssistant(client)");
                        break;
                    }
                case (UInt32)ActionID.ACTION_SYN_CLEAR_RANK:
                    {
                        stream.Write(ident + "removeClanAssistant(client)");
                        break;
                    }
                case (UInt32)ActionID.ACTION_SYN_CREATE_SUB:
                    {
                        string[] parts = param.Split(' ');
                        uint level = uint.Parse(parts[0]);
                        uint money = uint.Parse(parts[1]);
                        uint moneyLeave = uint.Parse(parts[2]);

                        stream.Write(ident + "createSubClan(client, {0}, {1}, {2})", level, money, moneyLeave);
                        break;
                    }
                case (UInt32)ActionID.ACTION_SYN_CHANGE_LEADER:
                    {                        
                        string[] parts = param.Split(' ');
                        byte reqLevel = byte.Parse(parts[0]);

                        stream.Write(ident + "changeSubClanLeader(client, {0})", reqLevel);
                        break;
                    }
                case (UInt32)ActionID.ACTION_SYN_ANTAGONIZE:
                    {
                        stream.Write(ident + "addClanEnemy(client)");
                        break;
                    }
                case (UInt32)ActionID.ACTION_SYN_CLEAR_ANTAGONIZE:
                    {
                        stream.Write(ident + "removeClanEnemy(client)");
                        break;
                    }
                case (UInt32)ActionID.ACTION_SYN_ALLY:
                    {
                        stream.Write(ident + "addClanAlly(client)");
                        break;
                    }
                case (UInt32)ActionID.ACTION_SYN_CLEAR_ALLY:
                    {
                        stream.Write(ident + "removeClanAlly(client)");
                        break;
                    }
                default:
                    {
                        Program.MISSING_ACTIONS.TryAdd(action.Type, 0);
                        Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);
                        break;
                    }
            }
        }

        private static void processActionUser(StreamWriter stream, String ident, IObj obj, Action action, string param)
        {
            switch (action.Type)
            {
                case (UInt32)ActionID.ACTION_USER_ATTR:
                    {
                        if (String.IsNullOrEmpty(param))
                            return;

                        string[] parts = param.Split(' ');
                        string attr = parts[0];
                        string opt = parts[1];
                        string data = parts[2];

                        Int32 nData = 0;
                        Int32.TryParse(data, out nData);

                        switch (attr)
                        {
                            case "life":
                                {
                                    if (opt == "+=")
                                        stream.Write(ident + "addLife(client, {0})", nData);
                                    else if (opt == "==")
                                        stream.Write(ident + "getCurHP(client) == {0}", nData);
                                    else if (opt == "<")
                                        stream.Write(ident + "getCurHP(client) < {0}", nData);
                                    else
                                        Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);
                                    break;
                                }
                            case "mana":
                                {
                                    if (opt == "+=")
                                        stream.Write(ident + "addMana(client, {0})", nData);
                                    else if (opt == "==")
                                        stream.Write(ident + "getCurMP(client) == {0}", nData);
                                    else if (opt == "<")
                                        stream.Write(ident + "getCurMP(client) < {0}", nData);
                                    else
                                        Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);
                                    break;
                                }
                            case "money":
                                {
                                    if (opt == "+=")
                                        if (nData < 0)
                                            stream.Write(ident + "spendMoney(client, {0})", Math.Abs(nData));
                                        else
                                            stream.Write(ident + "gainMoney(client, {0})", nData);
                                    else if (opt == "==")
                                        stream.Write(ident + "getMoney(client) == {0}", nData);
                                    else if (opt == "<")
                                        stream.Write(ident + "getMoney(client) < {0}", nData);
                                    else
                                        Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);
                                    break;
                                }
                            case "exp":
                                {
                                    if (opt == "+=")
                                        stream.Write(ident + "addExp(client, {0})", nData);
                                    else if (opt == "==")
                                        stream.Write(ident + "getExp(client) == {0}", nData);
                                    else if (opt == "<")
                                        stream.Write(ident + "getExp(client) < {0}", nData);
                                    else
                                        Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);
                                    break;
                                }
                            case "pk":
                                {
                                    if (opt == "+=")
                                        stream.Write(ident + "addPkPoints(client, {0})", nData);
                                    else if (opt == "==")
                                        stream.Write(ident + "getPkPoints(client) == {0}", nData);
                                    else if (opt == "<")
                                        stream.Write(ident + "getPkPoints(client) < {0}", nData);
                                    else
                                        Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);
                                    break;
                                }
                            case "profession":
                                {
                                    if (opt == "==")
                                        stream.Write(ident + "getProfession(client) == {0}", nData);
                                    else if (opt == "<=")
                                        stream.Write(ident + "getProfession(client) <= {0}", nData);
                                    else if (opt == ">=")
                                        stream.Write(ident + "getProfession(client) >= {0}", nData);
                                    else if (opt == "set")
                                        // TODO BROADCAST
                                        stream.Write(ident + "setProfession(client, {0})", nData);
                                    else
                                        Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);
                                    break;
                                }
                            case "level":
                                {
                                    if (opt == "==")
                                        stream.Write(ident + "getLevel(client) == {0}", nData);
                                    else if (opt == "<")
                                        stream.Write(ident + "getLevel(client) < {0}", nData);
                                    else
                                        Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);
                                    break;
                                }
                            case "force":
                                {
                                    if (opt == "+=")
                                        stream.Write(ident + "addForce(client, {0})", nData);
                                    else if (opt == "==")
                                        stream.Write(ident + "getForce(client) == {0}", nData);
                                    else if (opt == "<")
                                        stream.Write(ident + "getForce(client) < {0}", nData);
                                    else
                                        Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);
                                    break;
                                }
                            case "speed":
                                {
                                    if (opt == "+=")
                                        stream.Write(ident + "addDexterity(client, {0})", nData);
                                    else if (opt == "==")
                                        stream.Write(ident + "getDexterity(client) == {0}", nData);
                                    else if (opt == "<")
                                        stream.Write(ident + "getDexterity(client) < {0}", nData);
                                    else
                                        Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);
                                    break;
                                }
                            case "health":
                                {
                                    if (opt == "+=")
                                        stream.Write(ident + "addHealth(client, {0})", nData);
                                    else if (opt == "==")
                                        stream.Write(ident + "getHealth(client) == {0}", nData);
                                    else if (opt == "<")
                                        stream.Write(ident + "getHealth(client) < {0}", nData);
                                    else
                                        Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);
                                    break;
                                }
                            case "soul":
                                {
                                    if (opt == "+=")
                                        stream.Write(ident + "addSoul(client, {0})", nData);
                                    else if (opt == "==")
                                        stream.Write(ident + "getSoul(client) == {0}", nData);
                                    else if (opt == "<")
                                        stream.Write(ident + "getSoul(client) < {0}", nData);
                                    else
                                        Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);
                                    break;
                                }
                            case "metempsychosis":
                                {
                                    if (opt == "==")
                                        stream.Write(ident + "getMetempsychosis(client) == {0}", nData);
                                    else if (opt == "<")
                                        stream.Write(ident + "getMetempsychosis(client) < {0}", nData);
                                    else
                                        Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);
                                    break;
                                }
                            case "virtue":
                                {
                                    if (opt == "+=")
                                        if (nData < 0)
                                            stream.Write(ident + "spendVirtue(client, {0})", Math.Abs(nData));
                                        else
                                            stream.Write(ident + "gainVirtue(client, {0})", nData);
                                    else if (opt == "==")
                                        stream.Write(ident + "getVirtue(client) == {0}", nData);
                                    else if (opt == "<")
                                        stream.Write(ident + "getVirtue(client) < {0}", nData);
                                    else if (opt == ">")
                                        stream.Write(ident + "getVirtue(client) > {0}", nData);
                                    else if (opt == "<=")
                                        stream.Write(ident + "getVirtue(client) <= {0}", nData);
                                    else if (opt == ">=")
                                        stream.Write(ident + "getVirtue(client) >= {0}", nData);
                                    else
                                        Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);
                                    break;
                                }
                            case "rankshow":
                                {
                                    if (opt == "==")
                                        stream.Write(ident + "getClanRank(client) == {0}", nData);
                                    else
                                        Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);
                                    break;
                                }
                            default:
                                Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);
                                break;
                        }
                        break;
                    }
                case (UInt32)ActionID.ACTION_USER_CHGMAPRECORD:
                    {
                        stream.Write(ident + "move(client, getRecordMap(client), getRecordX(client), getRecordY(client))");

                        //int ret = pUser->FlyMap(pUser->QueryDataForAction()->GetRecordMap(),
                        //                        pUser->QueryDataForAction()->GetRecordX(),
                        //                        pUser->QueryDataForAction()->GetRecordY());
                        //if (ret == FLYMAP_NORMAL)
                        //    return true;
                        //else if (ret == FLYMAP_MAPGROUP)
                        //{
                        //    m_pUser = NULL;					// Çå³ýÈÎÎñÍæ¼Ò
                        //    return true;
                        //}

                        break;
                    }
                case (UInt32)ActionID.ACTION_USER_CHGMAP:
                    {
                        string[] parts = param.Split(' ');

                        UInt32 mapId = Convert.ToUInt32(parts[0]);
                        Int32 x = Convert.ToInt32(parts[1]);
                        Int32 y = Convert.ToInt32(parts[2]);
                        bool immediacy = false;

                        if (parts.Length == 4)
                            immediacy = Convert.ToByte(parts[3]) != 0;

                        stream.Write(ident + "move(client, {0}, {1}, {2})", mapId, x, y);

                        //if (!pUser->GetMap()->IsTeleportDisable() || bImmediacy)
                        //{
                        //    int ret = pUser->FlyMap(idMap, nMapX, nMapY);
                        //    if (ret == FLYMAP_NORMAL)
                        //        return true;
                        //    else if (ret == FLYMAP_MAPGROUP)
                        //    {
                        //        m_pUser = NULL;					// Çå³ýÈÎÎñÍæ¼Ò
                        //        return true;
                        //    }
                        //    else if (ret == FLYMAP_ERROR)
                        //    {
                        //        pUser->FlyMap(DEFAULT_LOGIN_MAPID, DEFAULT_LOGIN_POSX, DEFAULT_LOGIN_POSY);
                        //        UserManager()->KickOutSocket(pUser->GetSocketID(), "ÈÎÎñÇÐÆÁ×ø±ê´í£¡");
                        //    }
                        //}

                        break;
                    }
                case (UInt32)ActionID.ACTION_USER_RECORDPOINT:
                    {
                        string[] parts = param.Split(' ');

                        int mapId = int.Parse(parts[0]);
                        int mapX = int.Parse(parts[1]);
                        int mapY = int.Parse(parts[2]);

                        stream.Write(ident + "setRecordPos(client, {0}, {1}, {2})", mapId, mapX, mapY);
                        //pUser->SetRecordPos(idMap, nMapX, nMapY, true);
                        break;
                    }
                case (UInt32)ActionID.ACTION_USER_HAIR:
                    {
                        string[] parts = param.Split(' ');

                        string cmd = parts[0];
                        uint data = uint.Parse(parts[1]);

                        if (cmd == "style")
                        {
                            data %= 100;
                            stream.Write(ident + "setHair(client, getHair(client) - (getHair(client) % 100) + {0})", data);

                            //        if (nHair < 100)	// no color
                            //        {
                            //            const DEFAULT_COLOR = 3;
                            //            nHair += DEFAULT_COLOR*100;
                            //        }
                        }
                        else if (cmd == "color")
                        {
                            data %= 10;
                            stream.Write(ident + "setHair(client, getHair(client) - (math.floor((getHair(client) % 1000) / 100) * 100) + ({0} * 100))", data);
                        }
                        else
                            Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);

                        break;
                    }
                case (UInt32)ActionID.ACTION_USER_TALK:
                    {
                        if (String.IsNullOrWhiteSpace(param))
                            param = action.Param;

                        stream.Write(ident + "sendSysMsg(client, \"{0}\", {1})", param.Replace('~', ' '), action.Data != 0 ? action.Data : 2000);
                        break;
                    }
                case (UInt32)ActionID.ACTION_USER_EFFECT:
                    {
                        string[] parts = param.Split(' ');

                        string opt = parts[0];
                        string effect = parts[1];

                        if (opt == "self")
                        {
                            stream.Write(ident + "broadcastEffect(client, \"{0}\")", effect);

                            //CMsgName msg;
                            //IF_OK (msg.Create(NAMEACT_ROLEEFFECT, szEffect, pUser->GetID()))
                            //    pUser->BroadcastRoomMsg(&msg, true);
                        }
                        else
                            Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);

                        break;
                    }
                case (UInt32)ActionID.ACTION_USER_TASKMASK:
                    {
                        string[] parts = param.Split(' ');

                        string opt = parts[0];
                        int idx = int.Parse(parts[1]);

                        if (opt == "chk")
                            stream.Write(ident + "checkUserTask(client, {0})", idx);
                        else if (opt == "add")
                            stream.Write(ident + "setUserTask(client, {0})", idx);
                        else if (opt == "clr")
                            stream.Write(ident + "clearUserTask(client, {0})", idx);
                        else
                            Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);
                        break;
                    }
                case (UInt32)ActionID.ACTION_USER_SEX:
                    {
                        // sex check, return to a male and female returns 0
                        stream.Write(ident + "getSex(client) == 1");
                        break;
                    }
                case (UInt32)ActionID.ACTION_USER_MARRIAGE:
                    {
                        stream.Write(ident + "isMarried(client)");
                        break;
                    }
                case (UInt32)ActionID.ACTION_USER_DIVORCE:
                    {
                        stream.Write(ident + "divorce(client)");
                        break;
                    }
                case (UInt32)ActionID.ACTION_USER_MAGIC:
                    {
                        string[] parts = param.Split(' ');
                        string operation = parts[0];
                        int type = Convert.ToInt32(parts[1]);
                        int data = 0;
                        bool save = true;

                        if (parts.Length > 2)
                            data = Convert.ToInt32(parts[2]);
                        if (parts.Length > 3)
                        {
                            save = Convert.ToBoolean(parts[3]);
                            if (!save)
                                Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);
                        }

                        if (operation == "check")
                        {
                            if (parts.Length == 3)
                                stream.Write(ident + "hasMagic(client, {0}, {1})", type, data);
                            else
                                stream.Write(ident + "hasMagic(client, {0}, {1})", type, -1);
                        }
                        else if (operation == "learn")
                            stream.Write(ident + "awardMagic(client, {0}, {1})", type, data);
                        else if (operation == "uplevel")
                            stream.Write(ident + "upMagicLevel(client, {0})", type);
                        //            return pUser->QueryMagic()->UpLevelByTask(nType);
                        //else if (operation == "addexp")
                        //    //            return pUser->QueryMagic()->AwardExp(nType, 0, nData);
                        else
                            Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);

                        break;
                    }
                case (UInt32)ActionID.ACTION_USER_WEAPONSKILL:
                    {
                        string[] parts = param.Split(' ');
                        string operation = parts[0];
                        int type = Convert.ToInt32(parts[1]);
                        int level = Convert.ToInt32(parts[2]);

                        if (operation == "check")
                            stream.Write(ident + "hasSkill(client, {0}, {1})", type, level);
                        else if (operation == "learn")
                            stream.Write(ident + "awardSkill(client, {0}, {1})", type, level);
                        else
                            Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);

                        break;
                    }
                case (UInt32)ActionID.ACTION_USER_UNLEARN_MAGIC:
                    {
                        string[] parts = param.Split(' ');

                        int[] magics = new int[parts.Length];
                        for (int i = 0; i < parts.Length; ++i)
                            magics[i] = int.Parse(parts[i]);

                        string magics_str = "";
                        foreach (int magic in magics)
                            magics_str += magic.ToString() + ",";
                        if (magics_str.EndsWith(","))
                            magics_str = magics_str.TrimEnd(new char[] { ',' });

                        stream.Write(ident + "unlearnMagic(client, {0}, {1})", magics_str, false);

                            //bool bDrop = false;
                            //for(int i = 0; i < nNum; i++)
                            //{
                            //    int nType = nData[i];
                            //    pUser->QueryMagic()->UnlearnMagic(nType, bDrop);
                            //}

                            //return true;

                        break;
                    }
                case (UInt32)ActionID.ACTION_USER_DROP_MAGIC:
                    {
                        string[] parts = param.Split(' ');

                        int[] magics = new int[parts.Length];
                        for (int i = 0; i < parts.Length; ++i)
                            magics[i] = int.Parse(parts[i]);

                        string magics_str = "";
                        foreach (int magic in magics)
                            magics_str += magic.ToString() + ",";
                        if (magics_str.EndsWith(","))
                            magics_str = magics_str.TrimEnd(new char[] { ',' });

                        stream.Write(ident + "unlearnMagic(client, {0}, {1})", magics_str, true);

                        //bool bDrop =  true;
                        //for(int i = 0; i < nNum; i++)
                        //{
                        //    int nType = nData[i];
                        //    pUser->QueryMagic()->UnlearnMagic(nType, bDrop);
                        //}

                        //return true;

                        break;
                    }
                case (UInt32)ActionID.ACTION_USER_OPEN_DIALOG:
                    {
                        stream.Write(ident + "openDialog(client, {0})", action.Data);

            // CMsgAction	msg;
            //IF_OK(msg.Create(pUser->GetID(), pUser->GetPosX(), pUser->GetPosY(), pUser->GetDir(), actionOpenDialog, pAction->GetInt(ACTIONDATA_DATA)))
            //    pUser->SendMsg(&msg);

                        if (param != "")
                            Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);
                        break;
                    }
                case (UInt32)ActionID.ACTION_USER_CHGMAP_REBORN:
                    {
                        stream.Write(ident + "moveToRebornMap(client)");
                        break;
                    }
                case (UInt32)ActionID.ACTION_USER_UNLEARN_SKILL:
                    {
                        stream.Write(ident + "unlearnSkills(client)");
                        break;
                    }
                //case (UInt32)ActionID.ACTION_USER_REBIRTH:
                //    {

                //        break;
                //    }
                case (UInt32)ActionID.ACTION_USER_LOG:
                    // skip logs
                    break;
                case (UInt32)ActionID.ACTION_USER_MEDIAPLAY:
                    {
                        string[] parts = param.Split(' ');
                        bool broadcast = parts[0] != "play";
                        string media = parts[1];

                        stream.Write(ident + "play(client, \"{0}\", {1})", media, broadcast ? "true" : "false");
                        break;
                        //        CMsgName msg;
                        //        IF_OK(msg.Create(NAMEACT_PLAYER_WAVE, szMedia))
                        //            pUser->SendMsg(&msg);

                        //        CMsgName msg;
                        //        IF_OK(msg.Create(NAMEACT_PLAYER_WAVE, szMedia))
                        //            pUser->BroadcastRoomMsg(&msg, true);
                    }
                case (UInt32)ActionID.ACTION_USER_LOCK_CHK:
                    {
                        stream.Write(ident + "checkLockPin(client)");
                        break;
                    }
                case (UInt32)ActionID.ACTION_USER_LOCK_SET:
                    {
                        stream.Write(ident + "setLockPin(client)");
                        break;
                    }
                case (UInt32)ActionID.ACTION_USER_OPEN_STORAGE:
                    {
                        // ACTION_USER_OPEN_DIALOG already used
                        // What to do ?
                        break;
                    }
                case (UInt32)ActionID.ACTION_USER_DATAVAR_CHK:
                    {
                        string[] parts = param.Split(' ');

                        int regId = int.Parse(parts[0].Replace("var(", "").Replace(")", ""));
                        string opt = parts[1];
                        int data = int.Parse(parts[2]);

                        if (opt == ">")
                            stream.Write(ident + "getRegister(client, {0}) > {1}", regId, data);
                        else if (opt == ">=")
                            stream.Write(ident + "getRegister(client, {0}) >= {1}", regId, data);
                        else if (opt == "==")
                            stream.Write(ident + "getRegister(client, {0}) == {1}", regId, data);
                        else
                            Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);
                        break;
                    }
                case (UInt32)ActionID.ACTION_USER_DATAVAR_SET:
                    {
                        string[] parts = param.Split(' ');

                        if (parts[1] != "set")
                            Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);

                        int regId = int.Parse(parts[0].Replace("var(", "").Replace(")", ""));
                        int data = int.Parse(parts[2]);

                        stream.Write(ident + "setRegister(client, {0}, {1})", regId, data);
                        break;
                    }
                //case (UInt32)ActionID.ACTION_USER_STRVAR_CHK:
                //    {
                //        break;
                //    }
                //case (UInt32)ActionID.ACTION_USER_STRVAR_SET:
                //    {
                //        break;
                //    }
                //case (UInt32)ActionID.ACTION_USER_DATAVAR_CAL:
                //    {
                //        break;
                //    }
                case (UInt32)ActionID.ACTION_USER_TIMER:
                    {
                        //set the timer, countdown timer is triggered after the specified ACTION, parameter "delay_time idAction", delay_time seconds
                        string[] parts = param.Split(' ');

                        int delay_time = int.Parse(parts[0]);
                        UInt32 actionId = UInt32.Parse(parts[1]);

                        stream.WriteLine();
                        processAction(stream, ident, obj, actionId);

                        break;
                    }
                case (UInt32)ActionID.ACTION_USER_SCREFFECT:
                    {
                        //ACTION_USER_SCREFFECT = 1072, / / ​​screen effects, including vibration 1, zoom 2, dim lights 4, from DATA to specify, can be superimposed.
                        // Such as vibration and also need to zoom, data should be set to 3 (1 +2)
                        Console.WriteLine("ACTION_USER_SCREFFECT ignored...");
                        break;
                    }
                case (UInt32)ActionID.ACTION_USER_STATISTIC_CHK:
                    {
                        string[] parts = param.Split(' ');
                        string[] eventParts = parts[0].Replace("stc(", "").Replace(")", "").Split(',');

                        int eventId = int.Parse(eventParts[0]);
                        int type = int.Parse(eventParts[1]);
                        string opt = parts[1]; // >, >=, ==
                        int data = int.Parse(parts[2]);

                        if (opt == "==")
                            stream.Write(ident + "getUserStats(client, {0}, {1}) == {2}", eventId, type, data);
                        else if (opt == ">")
                            stream.Write(ident + "getUserStats(client, {0}, {1}) > {2}", eventId, type, data);
                        else if (opt == ">=")
                            stream.Write(ident + "getUserStats(client, {0}, {1}) >= {2}", eventId, type, data);
                        else
                            Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);

                        break;
                    }
                case (UInt32)ActionID.ACTION_USER_STATISTIC_SET:
                    {
                        string[] parts = param.Split(' ');
                        string[] eventParts = parts[0].Replace("stc(", "").Replace(")", "").Split(',');

                        int eventId = int.Parse(eventParts[0]);
                        int type = int.Parse(eventParts[1]);
                        string opt = parts[1]; // +=, =
                        int data = int.Parse(parts[2]);
                        bool save = int.Parse(parts[3]) != 0;

                        if (opt == "=")
                            stream.Write(ident + "setUserStats(client, {0}, {1}, {2}, {3})", eventId, type, data, save ? "true" : "false");
                        else if (opt == "+=")
                            stream.Write(ident + "setUserStats(client, {0}, {1}, getUserStats({0}, {1}) + {2}, {3})", eventId, type, data, save ? "true" : "false");
                        else
                            Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);

                        break;
                    }
                case (UInt32)1075:
                    {
                        string[] parts = param.Split(' ');
                        string opt = parts[0];
                        uint type = uint.Parse(parts[1]);
                        int data = int.Parse(parts[2]);

                        if (opt == "send")
                            stream.Write(ident + "sendCustomMsg(client, {0}, {1})", type, data);
                        else if (opt == "broadcast")
                            stream.Write(ident + "broadcastCustomMsg(client, {0}, {1})", type, data);
                        else
                            Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);
                        break;
                    }
                default:
                    {
                        Program.MISSING_ACTIONS.TryAdd(action.Type, 0);
                        Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);
                        break;
                    }
            }
        }

        private static void processActionEvent(StreamWriter stream, String ident, Object obj, Action action, string param)
        {
            switch (action.Type)
            {
                case (UInt32)ActionID.ACTION_EVENT_CREATENEW_NPC:
                    {

                        break;
                    }
                case (UInt32)ActionID.ACTION_EVENT_CREATEPET:
                    {
                        string[] parts = param.Split(' ');
                        Int32 ownerType = Convert.ToInt32(parts[0]);
                        UInt32 ownerUID = Convert.ToUInt32(parts[1]);
                        UInt32 mapId = Convert.ToUInt32(parts[2]);
                        Int32 x = Convert.ToInt32(parts[3]);
                        Int32 y = Convert.ToInt32(parts[4]);
                        UInt32 idGen = Convert.ToUInt32(parts[5]);
                        UInt32 type = Convert.ToUInt32(parts[6]);

                        Int32 data = 0;
                        String name = "";

                        if (parts.Length > 7)
                            data = Convert.ToInt32(parts[7]);
                        if (parts.Length > 8)
                            name = parts[8];

                        stream.Write(ident + "spawnMonster(client, {0}, \"{1}\", {2}, {3}, {4}, {5}, {6}, {7}, {8})",
                            type, name, ownerType, ownerUID, mapId, x, y, idGen, data);

                        //            // check syndicate name
                        //            const char* pszName = szName;
                        //            if(pszAccept && strlen(pszAccept) > 0)
                        //            {
                        //                if(strlen(pszAccept) >= _MAX_NAMESIZE || !::NameStrCheck((char*)pszAccept))
                        //                {
                        //                    //pUser->SendSysMsg(STR_INVALID_PET_NAME);
                        //                    return false;
                        //                }
                        //                pszName = pszAccept;
                        //            }

                        //            ST_CREATENEWNPC	info;
                        //            memset(&info, 0, sizeof(ST_CREATENEWNPC));
                        //            info.idMap			= idMap;
                        //            info.idData			= idGen;
                        //            info.idOwner		= idOwner;
                        //            info.usPosX			= nPosX;
                        //            info.usPosY			= nPosY;
                        //            info.usType			= idType;
                        //            info.ucOwnerType	= nOwnerType;
                        //            info.nData			= nData;

                        //            if(strlen(pszName) > 0 && strcmp(pszName, ACTIONSTR_NONE) != 0)
                        //                return NpcManager()->CreateSynPet(&info, pszName);
                        //            else
                        //                return NpcManager()->CreateSynPet(&info);

                        break;
                    }
                case (UInt32)ActionID.ACTION_EVENT_COUNTMONSTER:
                    {
                        string[] parts = param.Split(' ');
                        UInt32 mapId = Convert.ToUInt32(parts[0]);
                        String field = parts[1];
                        String data = parts[2];
                        String opt = parts[3];
                        Int32 num = Convert.ToInt32(parts[4]);

                        string fct = "";
                        if (field == "name")
                            fct = String.Format("countMonsterByName(client, \"{0}\", {1})", data, mapId);
                        else if (field == "gen_id")
                            fct = String.Format("countMonsterByGenID(client, {0}, {1})", data, mapId);
                        else
                        {
                            Program.MISSING_ACTIONS.TryAdd(action.Type, 0);
                            Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);
                            break;
                        }

                        if (opt == "<")
                            stream.Write(ident + fct + " < {0}", num);
                        else if (opt == "==")
                            stream.Write(ident + fct + " == {0}", num);
                        else
                        {
                            Program.MISSING_ACTIONS.TryAdd(action.Type, 0);
                            Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);
                            break;
                        }

                        break;
                    }
                case (UInt32)2013://ActionID.ACTION_EVENT_MAPUSER_EXEACTION:
                    {
                        string[] parts = param.Split(' ');
                        uint mapId = uint.Parse(parts[0]);
                        uint actionId = uint.Parse(parts[1]);
                        int data = int.Parse(parts[2]);

                        Action.processAction(stream, "", null, actionId); 
                        break;
                    }
                default:
                    {
                        Program.MISSING_ACTIONS.TryAdd(action.Type, 0);
                        Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);
                        break;
                    }
            }
        }

        private static void processActionWanted(StreamWriter stream, String ident, Object obj, Action action, string param)
        {
            switch (action.Type)
            {
                case (UInt32)ActionID.ACTION_POLICEWANTED_CHECK:
                    {
                        // Bounty or PkPoints >= 100
                        stream.Write(ident + "isWanted(client)");
                        break;
                    }
                default:
                    {
                        Program.MISSING_ACTIONS.TryAdd(action.Type, 0);
                        Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);
                        break;
                    }
            }
        }

        private static void processActionMonster(StreamWriter stream, String ident, Object obj, Action action, string param)
        {
            switch (action.Type)
            {
                case (UInt32)ActionID.ACTION_MST_DROPITEM:
                    {
                        string[] parts = param.Split(' ');
                        string opt = parts[0];

                        if (opt == "dropitem")
                        {
                            uint itemtype = uint.Parse(parts[1]);
                            stream.Write(ident + "dropItem(self, client, {0})", itemtype);
                        }
                        else
                        {
                            Program.MISSING_ACTIONS.TryAdd(action.Type, 0);
                            Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);
                            break;
                        }

                        break;
                    }
                default:
                    {
                        Program.MISSING_ACTIONS.TryAdd(action.Type, 0);
                        Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);
                        break;
                    }
            }
        }

        private static void processActionMagic(StreamWriter stream, String ident, Object obj, Action action, string param)
        {
            switch (action.Type)
            {
                default:
                    {
                        Program.MISSING_ACTIONS.TryAdd(action.Type, 0);
                        Console.WriteLine("Not implemented ! type=[{0}] param=[{1}] data=[{2}]", action.Type, param, action.Data);
                        break;
                    }
            }
        }

        private static void replaceAttrStr(ref string target, string source)
        {
            if (source.IndexOf('%') < 0)
            {
                target = source;
                return;
            }
            else
            {
                target = source;
                target = target.Replace("%%", "%");
                target = target.Replace("%user_name", "\" .. getName(client) .. \"");

                if (target.IndexOf('%') >= 0)
                    Console.WriteLine("replaceAttrStr not implemented yet ! ({0})", target);
            }
        }

#if SOMETHING
	const char*	ptr = pszSource; 
	char*	ptr2 = pszTarget;
	while(*ptr)
	{
		if(*ptr == '%')
		{
			ptr++;

			// width
			int nWidth	= 0;
			if(isdigit(*ptr))
				nWidth	= nWidth*10 + (*(ptr++))-'0';
			if(isdigit(*ptr))
				nWidth	= nWidth*10 + (*(ptr++))-'0';		// ½öÖ§³Ö2Î»Êý
			const char* pNext = ptr2 + nWidth;

			if(*(ptr) == '%')
			{
				*ptr2	= '%';

				ptr	+= 2;
				ptr2++;
				// continue;
			}
			if (strnicmp(ptr, PARAM_DATE_STAMP, sizeof(PARAM_DATE_STAMP)-1) == 0)
			{
				char szString[256] = "(err)";
				sprintf(szString, "%d", ::DateStamp());

				strcpy(ptr2, szString);

				ptr += sizeof(PARAM_DATE_STAMP)-1;
				ptr2 += strlen(szString);
				// continue;
			}
			else if (strnicmp(ptr, PARAM_TIME, sizeof(PARAM_TIME)-1) == 0)
			{
				char szString[256] = "(err)";
				sprintf(szString, "%d", time(NULL));

				strcpy(ptr2, szString);

				ptr += sizeof(PARAM_TIME)-1;
				ptr2 += strlen(szString);
				// continue;
			}
			else if(strnicmp(ptr, ACCEPT_STR_, sizeof(ACCEPT_STR_)-1) == 0 
						&& *(ptr+sizeof(ACCEPT_STR_)-1) >= '0' && *(ptr+sizeof(ACCEPT_STR_)-1) < '0'+ MAX_ACCEPTSTR)
			{
				char	szNum[256] = "(err)";
				int		nSize = strlen(szNum);
				int		idx = *(ptr+sizeof(ACCEPT_STR_)-1) - '0';
				if(pszAccept && idx >= 0 && idx < MAX_ACCEPTSTR)
				{
					char	szStr[4][256] = {0};
					sscanf(pszAccept, "%s %s %s %s", szStr[0], szStr[1], szStr[2], szStr[3]);
					strcpy(szNum, szStr[idx]);
				}
				strcpy(ptr2, szNum);

				ptr	+= sizeof(ACCEPT_STR_)-1 + 1;		//+1 : index
				ptr2		+= strlen(szNum);
				// continue;
			}
			if(pUser)
			{
				if (strnicmp(ptr, PARA_ITER_GAME_CARD2, sizeof(PARA_ITER_GAME_CARD2)-1) == 0)
				{
					char szString[256] = "--";
					sprintf(szString, "%d", pUser->CountCard2());

					strcpy(ptr2, szString);

					ptr += sizeof(PARA_ITER_GAME_CARD2)-1;
					ptr2 += strlen(szString);
					// continue;
				}
				else if (strnicmp(ptr, PARA_AVAILABLE_FUND, sizeof(PARA_AVAILABLE_FUND)-1) == 0)
				{
					//°ïÅÉ»ù½ð
					char szString[256] = "--";
					sprintf(szString, "%d", pUser->GetAvailableSynFund());
					
					strcpy(ptr2, szString);
					
					ptr += sizeof(PARA_AVAILABLE_FUND)-1;
					ptr2 += strlen(szString);

				}								
				else if (strnicmp(ptr, PARA_ITER_GAME_CARD, sizeof(PARA_ITER_GAME_CARD)-1) == 0)
				{
					char szString[256] = "--";
					sprintf(szString, "%d", pUser->CountCard());

					strcpy(ptr2, szString);

					ptr += sizeof(PARA_ITER_GAME_CARD)-1;
					ptr2 += strlen(szString);
					// continue;
				}
				else if (strnicmp(ptr, PARA_ITER_COST_DURRECOVER, sizeof(PARA_ITER_COST_DURRECOVER)-1) == 0)
				{
					char szString[256] = "--";
					if (pUser->TaskIterator() != 0)
					{
						CItem* pItem = pUser->GetEquipItemByPos(pUser->TaskIterator());
						if (pItem)
							sprintf(szString, "%d", pItem->GetRecoverDurCost());
					}
					strcpy(ptr2, szString);

					ptr += sizeof(PARA_ITER_COST_DURRECOVER)-1;
					ptr2 += strlen(szString);
					// continue;
				}
				else if (strnicmp(ptr, PARA_ITER_GEMSUPQUALITY, sizeof(PARA_ITER_GEMSUPQUALITY)-1) == 0)
				{
					char szString[256] = "--";
					if (pUser->TaskIterator() != 0)
					{
						int nChance = pUser->GetChanceUpEpQuality(pUser->TaskIterator());

						int nGemCost = 0;
						if (nChance >= 0)
							nGemCost = 100/__max(1, nChance) + 1;

						sprintf(szString, "%d", nGemCost);
					}
					strcpy(ptr2, szString);

					ptr += sizeof(PARA_ITER_GEMSUPQUALITY)-1;
					ptr2 += strlen(szString);
					// continue;
				}
				else if (strnicmp(ptr, PARA_ITER_GEMSUPLEVEL, sizeof(PARA_ITER_GEMSUPLEVEL)-1) == 0)
				{
					char szString[256] = "--";
					if (pUser->TaskIterator() != 0)
					{
						int nChance = pUser->GetChanceUpEpLevel(pUser->TaskIterator());

						int nGemCost = 0;
						if (nChance >= 0)
							nGemCost = (100/__max(1, nChance) + 1)*12/10;

						sprintf(szString, "%d", nGemCost);
					}
					strcpy(ptr2, szString);

					ptr += sizeof(PARA_ITER_GEMSUPLEVEL)-1;
					ptr2 += strlen(szString);
					// continue;
				}
				else if (strnicmp(ptr, PARA_ITER_POLICEWANTED, sizeof(PARA_ITER_POLICEWANTED)-1) == 0)
				{
					char szString[256] = "--";
					//if (pUser->TaskIterator() != 0)
					{
						PoliceWantedStruct* pWanted = PoliceWanted().GetWantedByIndex(pUser->TaskIterator());
						if (pWanted)
						{
							sprintf(szString, "%03u %-15s %-15s %03d %d", 
									pUser->TaskIterator(), 
									pWanted->strName.c_str(), 
									pWanted->strSynName.c_str(), 
									pWanted->nLev,
									pWanted->nPk);
						}
					}
					strcpy(ptr2, szString);

					ptr += sizeof(PARA_ITER_POLICEWANTED)-1;
					ptr2 += strlen(szString);
					// continue;
				}
				else if (strnicmp(ptr, PARA_ITER_WANTED, sizeof(PARA_ITER_WANTED)-1) == 0)
				{
					char szString[256] = "--";
					//if (pUser->TaskIterator() != 0)
					{
						CWantedData* pWanted = CWantedList::s_WantedList.GetWantedByIndex(pUser->TaskIterator());
						if (pWanted)
						{
							sprintf(szString, "%06u %-15s %u", pWanted->GetInt(DATA_ID), pWanted->GetStr(DATA_TARGET_NAME), pWanted->GetInt(DATA_BOUNTY));
						}
					}
					strcpy(ptr2, szString);

					ptr += sizeof(PARA_ITER_WANTED)-1;
					ptr2 += strlen(szString);
					// continue;
				}
				else if (strnicmp(ptr, PARA_TUTOREXP_, sizeof(PARA_TUTOREXP_)-1) == 0)
				{
					char	szNum[256] = "0";
					sprintf(szNum, "%d", pUser->GetTutorExp());
					strcpy(ptr2, szNum);

					ptr += sizeof(PARA_TUTOREXP_)-1;
					ptr2		+= strlen(szNum);
					// continue;					
				}
				else if (strnicmp(ptr, PARA_STUDENT_EXP_, sizeof(PARA_STUDENT_EXP_)-1) == 0)
				{
					char	szNum[256]	= "0";
					CTutorexpData*	pExpData;
					pExpData = CTutorexpData::CreateNew();
					if (pExpData)
					{
						if (pExpData->Create(pUser->GetID(), Database()))
						{
							sprintf(szNum, "%u", pExpData->GetInt(TUTOREXPDATA_EXP));
						}
						SAFE_RELEASE (pExpData);
					}
					strcpy(ptr2, szNum);

					ptr		+= sizeof(PARA_STUDENT_EXP_)-1;
					ptr2	+= strlen(szNum);
					// continue;
				}
				else if (strnicmp(ptr, PARA_EXPLOIT_, sizeof(PARA_EXPLOIT_)-1) == 0)
				{
					char	szNum[256]	= "0";
					sprintf(szNum, "%u", pUser->GetExploit());

					strcpy(ptr2, szNum);

					ptr		+= sizeof(PARA_EXPLOIT_)-1;
					ptr2	+= strlen(szNum);
				}
				else if(strnicmp(ptr, PARA_ENEMY_SYN_, sizeof(PARA_ENEMY_SYN_)-1) == 0 
							&& *(ptr+sizeof(PARA_ENEMY_SYN_)-1) >= '0' && *(ptr+sizeof(PARA_ENEMY_SYN_)-1) < '0'+ MAX_SYNENEMYSIZE)
				{
					char	szNum[256] = "--";
					int		idx = *(ptr+sizeof(PARA_ENEMY_SYN_)-1) - '0';
					if(idx >= 0 && idx < MAX_SYNENEMYSIZE)
					{
						CSynPtr pSyn = SynManager()->QuerySyndicate(pUser->GetSynID());
						if(pSyn)
						{
							CSynPtr pTarget = SynManager()->QuerySyndicate(pSyn->GetInt((SYNDATA)(SYNDATA_ENEMY0+idx)));
							if(pTarget)
								sprintf(szNum, "%s", pTarget->GetStr(SYNDATA_NAME));
						}
					}
					strcpy(ptr2, szNum);

					ptr	+= sizeof(PARA_ENEMY_SYN_)-1 + 1;
					ptr2		+= strlen(szNum);
					// continue;
				}
				else if(strnicmp(ptr, PARA_ALLY_SYN_, sizeof(PARA_ALLY_SYN_)-1) == 0 
							&& *(ptr+sizeof(PARA_ALLY_SYN_)-1) >= '0' && *(ptr+sizeof(PARA_ALLY_SYN_)-1) < '0'+ MAX_SYNALLYSIZE)
				{
					char	szNum[256] = "--";
					int		idx = *(ptr+sizeof(PARA_ALLY_SYN_)-1) - '0';
					if(idx >= 0 && idx < MAX_SYNALLYSIZE)
					{
						CSynPtr pSyn = SynManager()->QuerySyndicate(pUser->GetSynID());
						if(pSyn)
						{
							CSynPtr pTarget = SynManager()->QuerySyndicate(pSyn->GetInt((SYNDATA)(SYNDATA_ALLY0+idx)));
							if(pTarget)
								sprintf(szNum, "%s", pTarget->GetStr(SYNDATA_NAME));
						}
					}
					strcpy(ptr2, szNum);

					ptr	+= sizeof(PARA_ALLY_SYN_)-1 + 1;
					ptr2		+= strlen(szNum);
					// continue;
				}
				else if(strnicmp(ptr, PARA_USER_ID, sizeof(PARA_USER_ID)-1) == 0)
				{
					char	szNum[256] = "";
					sprintf(szNum, "%u", pUser->GetID());
					strcpy(ptr2, szNum);

					ptr += sizeof(PARA_USER_ID)-1;
					ptr2		+= strlen(szNum);
					// continue;
				}
				else if(strnicmp(ptr, PARA_USER_MAPID, sizeof(PARA_USER_MAPID)-1) == 0)
				{
					char	szNum[256] = "";
					sprintf(szNum, "%u", pUser->GetMapID());
					strcpy(ptr2, szNum);

					ptr += sizeof(PARA_USER_MAPID)-1;
					ptr2		+= strlen(szNum);
					// continue;
				}
				else if(strnicmp(ptr, PARA_USER_MAPX, sizeof(PARA_USER_MAPX)-1) == 0)
				{
					char	szNum[256] = "";
					sprintf(szNum, "%u", pUser->GetPosX());
					strcpy(ptr2, szNum);

					ptr += sizeof(PARA_USER_MAPX)-1;
					ptr2		+= strlen(szNum);
					// continue;
				}
				else if(strnicmp(ptr, PARA_USER_MAPY, sizeof(PARA_USER_MAPY)-1) == 0)
				{
					char	szNum[256] = "";
					sprintf(szNum, "%u", pUser->GetPosY());
					strcpy(ptr2, szNum);

					ptr += sizeof(PARA_USER_MAPY)-1;
					ptr2		+= strlen(szNum);
					// continue;
				}
				else if(strnicmp(ptr, PARA_USER_HOME, sizeof(PARA_USER_HOME)-1) == 0)
				{
					char	szNum[256] = "";
					sprintf(szNum, "%u", pUser->GetHomeID());
					strcpy(ptr2, szNum);

					ptr += sizeof(PARA_USER_HOME)-1;
					ptr2		+= strlen(szNum);
					// continue;
				}
				else if(strnicmp(ptr, PARA_SYN_ID, sizeof(PARA_SYN_ID)-1) == 0)
				{
					char	szNum[256] = "";
					sprintf(szNum, "%u", pUser->GetMasterSynID());
					strcpy(ptr2, szNum);

					ptr += sizeof(PARA_SYN_ID)-1;
					ptr2		+= strlen(szNum);
					// continue;
				}
				else if(strnicmp(ptr, PARA_SYN_NAME, sizeof(PARA_SYN_NAME)-1) == 0)
				{
					const char*	pName = SYNNAME_NONE;
					if(pUser->GetSynID() != ID_NONE)
					{
						CSynPtr pSyn = SynManager()->QuerySyndicate(pUser->GetSynID());
						if(pSyn)
							pName = pSyn->GetStr(SYNDATA_NAME);
					}
					strcpy(ptr2, pName);

					ptr += sizeof(PARA_SYN_NAME)-1;
					ptr2		+= strlen(pName);
					// continue;
				}
				else if(strnicmp(ptr, PARA_USER_NAME, sizeof(PARA_USER_NAME)-1) == 0)
				{
					strcpy(ptr2, pUser->GetName());

					ptr += sizeof(PARA_USER_NAME)-1;
					ptr2		+= strlen(pUser->GetName());
					// continue;
				}
				else if(strnicmp(ptr, PARA_MATE_NAME, sizeof(PARA_MATE_NAME)-1) == 0)
				{
					strcpy(ptr2, pUser->GetMate());

					ptr += sizeof(PARA_MATE_NAME)-1;
					ptr2		+= strlen(pUser->GetMate());
					// continue;
				}
				else if(strnicmp(ptr, PARA_MAP_OWNER_ID, sizeof(PARA_MAP_OWNER_ID)-1) == 0)
				{
					char	szNum[256] = "0";	// È±Ê¡OWNERIDÎª0
					sprintf(szNum, "%u", pUser->GetMap()->GetOwnerID());
					strcpy(ptr2, szNum);

					ptr += sizeof(PARA_MAP_OWNER_ID)-1;
					ptr2		+= strlen(szNum);
					// continue;
				}
				else if(strnicmp(ptr, PARA_MAP_OWNER_TYPE, sizeof(PARA_MAP_OWNER_TYPE)-1) == 0)
				{
					char	szNum[256] = "0";	// È±Ê¡OWNERIDÎª0
					sprintf(szNum, "%u", pUser->GetMap()->GetOwnerType());
					strcpy(ptr2, szNum);

					ptr += sizeof(PARA_MAP_OWNER_TYPE)-1;
					ptr2		+= strlen(szNum);
					// continue;
				}
				else if(strnicmp(ptr, PARA_ITER_VALUE, sizeof(PARA_ITER_VALUE)-1) == 0)
				{
					char	szNum[256] = "0";	// È±Ê¡OWNERIDÎª0
					sprintf(szNum, "%u", pUser->TaskIterator());
					strcpy(ptr2, szNum);

					ptr += sizeof(PARA_ITER_VALUE)-1;
					ptr2		+= strlen(szNum);
					// continue;
				}
				else if(strnicmp(ptr, PARA_ITER_SYN_NAME, sizeof(PARA_ITER_SYN_NAME)-1) == 0)
				{
					const char*	pName = "--";
					if(pUser->TaskIterator() != ID_NONE)
					{
						CSynPtr pSyn = SynManager()->QuerySyndicate((OBJID)pUser->TaskIterator());
						if(pSyn)
							pName = pSyn->GetStr(SYNDATA_NAME);
					}
					strcpy(ptr2, pName);

					ptr += sizeof(PARA_ITER_SYN_NAME)-1;
					ptr2		+= strlen(pName);
					// continue;
				}
				else if(strnicmp(ptr, PARA_ITER_SYN_LEADER, sizeof(PARA_ITER_SYN_LEADER)-1) == 0)
				{
					const char*	pName = "--";
					if(pUser->TaskIterator() != ID_NONE)
					{
						CSynPtr pSyn = SynManager()->QuerySyndicate((OBJID)pUser->TaskIterator());
						if(pSyn)
							pName = pSyn->GetStr(SYNDATA_LEADERNAME);
					}
					strcpy(ptr2, pName);

					ptr += sizeof(PARA_ITER_SYN_LEADER)-1;
					ptr2		+= strlen(pName);
					// continue;
				}
				else if(strnicmp(ptr, PARA_ITER_SYN_MONEY, sizeof(PARA_ITER_SYN_MONEY)-1) == 0)
				{
					char	szNum[256] = "--";	// È±Ê¡OWNERIDÎª0
					if(pUser->TaskIterator() != ID_NONE)
					{
						CSynPtr pSyn = SynManager()->QuerySyndicate((OBJID)pUser->TaskIterator());
						if(pSyn)
							sprintf(szNum, "%d", pSyn->GetInt(SYNDATA_MONEY));
					}
					strcpy(ptr2, szNum);

					ptr += sizeof(PARA_ITER_SYN_MONEY)-1;
					ptr2		+= strlen(szNum);
					// continue;
				}
				else if(strnicmp(ptr, PARA_ITER_SYN_AMOUNT, sizeof(PARA_ITER_SYN_AMOUNT)-1) == 0)
				{
					char	szNum[256] = "--";	// È±Ê¡OWNERIDÎª0
					if(pUser->TaskIterator() != ID_NONE)
					{
						CSynPtr pSyn = SynManager()->QuerySyndicate((OBJID)pUser->TaskIterator());
						if(pSyn)
							sprintf(szNum, "%u", pSyn->GetSynAmount());
					}
					strcpy(ptr2, szNum);

					ptr += sizeof(PARA_ITER_SYN_AMOUNT)-1;
					ptr2		+= strlen(szNum);
					// continue;
				}
				else if(strnicmp(ptr, PARA_ITER_SYN_FEALTY, sizeof(PARA_ITER_SYN_FEALTY)-1) == 0)
				{
					char	szNum[256] = "--";	// È±Ê¡OWNERIDÎª0
					if(pUser->TaskIterator() != ID_NONE)
					{
						CSynPtr pSyn = SynManager()->QuerySyndicate((OBJID)pUser->TaskIterator());
						if(pSyn)
							sprintf(szNum, "%u", pSyn->GetFealtyName());
					}
					strcpy(ptr2, szNum);

					ptr += sizeof(PARA_ITER_SYN_FEALTY)-1;
					ptr2		+= strlen(szNum);
					// continue;
				}
				else if(strnicmp(ptr, PARA_ITER_MEMBER_RANK, sizeof(PARA_ITER_MEMBER_RANK)-1) == 0)
				{
					const char*	pName = "--";
					if(pUser->TaskIterator() != ID_NONE)
					{
						CUser* pTheUser = UserManager()->GetUser((OBJID)pUser->TaskIterator());
						if(pTheUser && pTheUser->GetSynID())
							pName = pTheUser->QuerySynAttr()->GetRankTitle();
					}
					strcpy(ptr2, pName);

					ptr += sizeof(PARA_ITER_MEMBER_RANK)-1;
					ptr2		+= strlen(pName);
					// continue;
				}
				else if(strnicmp(ptr, PARA_ITER_MEMBER_NAME, sizeof(PARA_ITER_MEMBER_NAME)-1) == 0)
				{
					const char*	pName = "--";
					if(pUser->TaskIterator() != ID_NONE)
					{
						CUser* pTheUser = UserManager()->GetUser((OBJID)pUser->TaskIterator());
						if(pTheUser)
							pName = pTheUser->GetName();
					}
					strcpy(ptr2, pName);

					ptr += sizeof(PARA_ITER_MEMBER_NAME)-1;
					ptr2		+= strlen(pName);
					// continue;
				}
				else if(strnicmp(ptr, PARA_ITER_MEMBER_PROFFER, sizeof(PARA_ITER_MEMBER_PROFFER)-1) == 0)
				{
					char	szNum[256] = "--";	// È±Ê¡OWNERIDÎª0
					if(pUser->TaskIterator() != ID_NONE)
					{
						CUser* pTheUser = UserManager()->GetUser((OBJID)pUser->TaskIterator());
						if(pTheUser && pTheUser->GetSynID() != ID_NONE)
							sprintf(szNum, "%d", pTheUser->QuerySynAttr()->GetInt(SYNATTRDATA_PROFFER));
					}
					strcpy(ptr2, szNum);

					ptr += sizeof(PARA_ITER_MEMBER_PROFFER)-1;
					ptr2		+= strlen(szNum);
					// continue;
				}
				else if (strnicmp(ptr, PARA_ITER_TABLE_DATASTR, sizeof(PARA_ITER_TABLE_DATASTR)-1) == 0)
				{
					CHECK(pNpc && pNpc->QueryTable());
					CTableData* pData = pNpc->QueryTable()->QuerySet()->GetObj(pUser->TaskIterator());
					CHECK(pData);

					strcpy(ptr2, pData->GetStr(TABLEDATA_DATASTR));

					ptr += sizeof(PARA_ITER_TABLE_DATASTR)-1;
					ptr2		+= strlen(pData->GetStr(TABLEDATA_DATASTR));
					// continue;
				}
				else if(strnicmp(ptr, PARA_ITER_TABLE_DATA_, sizeof(PARA_ITER_TABLE_DATA_)-1) == 0 
							&& *(ptr+sizeof(PARA_ITER_TABLE_DATA_)-1) >= '0' && *(ptr+sizeof(PARA_ITER_TABLE_DATA_)-1) < '0'+ TABLEDATA_SIZE)
				{
					CHECK(pNpc && pNpc->QueryTable());
					CTableData* pData = pNpc->QueryTable()->QuerySet()->GetObj(pUser->TaskIterator());
					CHECK(pData);

					char	szNum[256] = "--";
					int		idx = *(ptr+sizeof(PARA_ITER_TABLE_DATA_)-1) - '0';
					if(idx >= 0 && idx < TABLEDATA_SIZE)
						sprintf(szNum, "%d", pData->GetInt(TABLEDATA(TABLEDATA_DATA0 + idx)));
					strcpy(ptr2, szNum);

					ptr	+= sizeof(PARA_ITER_TABLE_DATA_)-1 + 1;		//+1 : index
					ptr2		+= strlen(szNum);
					// continue;
				}
				else if(strnicmp(ptr, PARA_ITER_ITEM_DATA, sizeof(PARA_ITER_ITEM_DATA)-1) == 0)
				{
					char	szNum[256] = "(err)";
					CItem* pItem = pUser->GetItem(pUser->TaskIterator());
					IF_OK(pItem)
						sprintf(szNum, "%u", pItem->GetInt(ITEMDATA_DATA));
					strcpy(ptr2, szNum);

					ptr += sizeof(PARA_ITER_ITEM_DATA)-1;
					ptr2		+= strlen(szNum);
					// continue;
				}
				//---ÈÎÎñÏµÍ³---begin
				else if(strnicmp(ptr, PARA_ITER_TASK_USERNAME, sizeof(PARA_ITER_TASK_USERNAME)-1) == 0)
				{
					//´ËÈÎÎñ¶ÔÓ¦µÄÓÃ»§Ãû
					char szName[128];
					CTaskDetail * pTask = pUser->GetTaskDetail();
					CHECK(pTask);
					CTaskDetailData * pTaskData = pTask->QueryData(pUser->TaskIterator());
					CHECK(pTaskData);
					OBJID idUser = pTaskData->GetInt(TASKDETAILDATA_USERID);
					CUser * pUser = UserManager()->GetUser(idUser);
					CHECK(pUser);
					
					sprintf(szName,"%s",pUser->GetName());
					
					strcpy(ptr2, szName);

					ptr += sizeof(PARA_ITER_TASK_USERNAME)-1;
					ptr2		+= strlen(szName);
					// continue;
				}  
				else if(strnicmp(ptr, PARA_ITER_TASK_COMPLETENUM, sizeof(PARA_ITER_TASK_COMPLETENUM)-1) == 0)
				{
					//ÈÎÎñÍê³É´ÎÊý
					char szBuff[128];

					CTaskDetail * pTask = pUser->GetTaskDetail();
					CHECK(pTask);
					CTaskDetailData * pTaskData = pTask->QueryData(pUser->TaskIterator());
					CHECK(pTaskData);

					sprintf(szBuff,"%d",pTaskData->GetInt(TASKDETAILDATA_TASKCOMPLETENUM));

					strcpy(ptr2, szBuff);

					ptr += sizeof(PARA_ITER_TASK_COMPLETENUM)-1;
					ptr2		+= strlen(szBuff);

				}
				else if(strnicmp(ptr, PARA_ITER_TASK_BEGINTIME, sizeof(PARA_ITER_TASK_BEGINTIME)-1) == 0)
				{
					//ÈÎÎñµÄ¿ªÊ¼Ê±¼ä
					char szBuff[128];

					CTaskDetail * pTask =pUser->GetTaskDetail();
					CHECK(pTask);
					CTaskDetailData * pTaskData = pTask->QueryData(pUser->TaskIterator());
					CHECK(pTaskData);

					time_t ltime = pTaskData->GetInt(TASKDETAILDATA_TASKBEGINTIME);
					struct tm *pTime;
					pTime = localtime( &ltime );
					CHECK(pTime);
					
					int nYear, nMonth, nDay, nHour, nMinute, nSec;
					nYear	=pTime->tm_year+1900;
					nMonth	=pTime->tm_mon+1;
					nDay	=pTime->tm_mday;					
					nHour	=pTime->tm_hour;
					nMinute	=pTime->tm_min;
					nSec    = pTime->tm_sec;

					sprintf(szBuff,"%d-%02d-%02d %02d:%02d:%02d",nYear,nMonth,nDay,nHour,nMinute,nSec);

					strcpy(ptr2, szBuff);

					ptr += sizeof(TASKDETAILDATA_TASKBEGINTIME)-1;
					ptr2		+= strlen(szBuff);

				}
				//---ÈÎÎñÏµÍ³---end

			}
			if(pNpc)
			{
				if (strnicmp(ptr, PARA_DATASTR, sizeof(PARA_DATASTR)-1) == 0)
				{
					strcpy(ptr2, pNpc->GetStr(NPCDATA_DATASTR));

					ptr += sizeof(PARA_DATASTR)-1;
					ptr2		+= strlen(pNpc->GetStr(NPCDATA_DATASTR));
					// continue;
				}
				else if(strnicmp(ptr, PARA_DATA_, sizeof(PARA_DATA_)-1) == 0 
							&& *(ptr+sizeof(PARA_DATA_)-1) >= '0' && *(ptr+sizeof(PARA_DATA_)-1) < '0'+ MAX_NPCDATA)
				{
					char	szNum[256] = "";
					int		idx = *(ptr+sizeof(PARA_DATA_)-1) - '0';
					if(idx >= 0 && idx < MAX_NPCDATA)
						sprintf(szNum, "%d", pNpc->GetData(idx));
					strcpy(ptr2, szNum);

					ptr	+= sizeof(PARA_DATA_)-1 + 1;		//+1 : index
					ptr2		+= strlen(szNum);
					// continue;
				}
				else if(strnicmp(ptr, PARA_NAME, sizeof(PARA_NAME)-1) == 0)
				{
					strcpy(ptr2, pNpc->GetName());

					ptr += sizeof(PARA_NAME)-1;
					ptr2		+= strlen(pNpc->GetStr(NPCDATA_NAME));
					// continue;
				}
				else if(strnicmp(ptr, PARA_NPC_ID, sizeof(PARA_NPC_ID)-1) == 0)
				{
					char	szNum[256] = "0";
					sprintf(szNum, "%u", pNpc->GetRecordID());
					strcpy(ptr2, szNum);

					ptr += sizeof(PARA_NPC_ID)-1;
					ptr2		+= strlen(szNum);
					// continue;
				}
				else if(strnicmp(ptr, PARA_NPC_X, sizeof(PARA_NPC_X)-1) == 0)
				{
					char	szNum[256] = "0";
					sprintf(szNum, "%u", pNpc->GetPosX());
					strcpy(ptr2, szNum);

					ptr += sizeof(PARA_NPC_X)-1;
					ptr2		+= strlen(szNum);
					// continue;
				}
				else if(strnicmp(ptr, PARA_NPC_Y, sizeof(PARA_NPC_Y)-1) == 0)
				{
					char	szNum[256] = "0";
					sprintf(szNum, "%u", pNpc->GetPosY());
					strcpy(ptr2, szNum);

					ptr += sizeof(PARA_NPC_Y)-1;
					ptr2		+= strlen(szNum);
					// continue;
				}
				else if(strnicmp(ptr, PARA_NPC_OWNERID, sizeof(PARA_NPC_OWNERID)-1) == 0)
				{
					char	szNum[256] = "0";
					sprintf(szNum, "%u", pNpc->GetInt(NPCDATA_OWNERID));
					strcpy(ptr2, szNum);

					ptr += sizeof(PARA_NPC_OWNERID)-1;
					ptr2		+= strlen(szNum);
					// continue;
				}
			}
			if(pItem)
			{
				if(strnicmp(ptr, PARA_ITEM_TYPE, sizeof(PARA_ITEM_TYPE)-1) == 0)
				{
					char	szNum[256] = "";
					sprintf(szNum, "%u", pItem->GetInt(ITEMDATA_TYPE));
					strcpy(ptr2, szNum);

					ptr += sizeof(PARA_ITEM_TYPE)-1;
					ptr2		+= strlen(szNum);
					// continue;
				}
				else if(strnicmp(ptr, PARA_ITEM_DATA, sizeof(PARA_ITEM_DATA)-1) == 0)
				{
					char	szNum[256] = "";
					sprintf(szNum, "%u", pItem->GetInt(ITEMDATA_DATA));
					strcpy(ptr2, szNum);

					ptr += sizeof(PARA_ITEM_DATA)-1;
					ptr2		+= strlen(szNum);
					// continue;
				}
			}
			
			// fill space
			if(nWidth)
			{
				while(ptr2 < pNext)
					*(ptr2++)	= ' ';
			}
		} // %
		else
		{
			*ptr2	= *ptr;

			ptr++;
			ptr2++;
		}
	} // while
	*ptr2	= 0;
}
#endif
    }
}
