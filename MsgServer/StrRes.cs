// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2011 - 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.IO;

namespace COServer
{
    /// <summary>
    /// String resources of the server.
    /// </summary>
    public class StrRes
    {
        /// <summary>
        /// The logger of the class.
        /// </summary>
        private static readonly log4net.ILog sLogger = log4net.LogManager.GetLogger(typeof(StrRes));

        public static String STR_WRONG_CLIENT = "You must use the client distribued by the COPS team.";
        public static String STR_WRONG_VERSION = "Please update your client with the patches distribued by the COPS team.";
        public static String STR_LOGIN_INFORMATION = "COPS v6 - The Return Of The Legend!";
        public static String STR_SERVER_UPTIME = "The server is online since {0}.";
        public static String STR_SERVER_INFORMATION = "There is {0} players on the server {1}.";
        public static String STR_FAR_CANNOT_PICK = "The item is too far.";
        public static String STR_FULL_CANNOT_PICK = "Your inventory is full!";
        public static String STR_OTHERS_ITEM = "You can't take this item for the moment.";
        public static String STR_TOOMUCH_MONEY = "You have too much money.";
        public static String STR_NOT_SO_MUCH_MONEY = "You don't have enough money.";
        public static String STR_PICK_MONEY = "You got {0} silvers.";
        public static String STR_GOT_ITEM = "You got {0}.";
        public static String STR_FREE_PK_MODE = "In free mode, you can attack everybody.";
        public static String STR_SAFE_PK_MODE = "In safe mode, you can only attack monsters.";
        public static String STR_TEAM_PK_MODE = "In team mode, you can attack everybody, except your friends, your teammates, and your guildmates.";
        public static String STR_ARRESTMENT_PK_MODE = "In arrestment mode, you can only attack monsters and black name players.";
        public static String STR_DIE_DROP_MONEY = "You're dead. You can't drop money.";
        public static String STR_DROP_MONEY_SUCC = "You have droped {0} silvers.";
        public static String STR_MAKE_MONEY_FAILED = "Error: Can't create the money.";
        public static String STR_NAME_USED = "This name is already used.";
        public static String STR_INVALID_NAME = "This name contains invalid characters.";
        public static String STR_INVALID_COORDINATE = "Invalid coordinates.";
        public static String STR_DIE = "You're dead.";
        public static String STR_ALLOT_CHEAT = "[Cheat] You're trying to distribute more points that you really have.";
        public static String STR_CONNECTION_OFF = "Your session has expired.";
        public static String STR_NOT_TEAM_LEADER = "You aren't the team leader.";
        public static String STR_TEAM_FULL = "The team is already full.";
        public static String STR_HAVE_TEAM = "You're already in a team!";
        public static String STR_INVITATION_SENT = "The invitation has been sent.";
        public static String STR_REQUEST_SENT = "The request has been sent.";
        public static String STR_TEAM_CLOSED = "You can't join this team.";
        public static String STR_TEAM_EXPERIENCE = "You got {0} exp points with your team.";
        public static String STR_TEAM_VIRTUE = "The team leader got {0} virtue points.";
        public static String STR_TRADE_SUCCEED = "The trade is a success.";
        public static String STR_TRADE_FAIL = "The trade has failed.";
        public static String STR_NO_TRADE_TARGET = "You don't have a target to trade with.";
        public static String STR_TARGET_TRADING = "The player is already trading.";
        public static String STR_TRADING_REQEST_SENT = "The trade request has been sent.";
        public static String STR_NOT_FOR_TRADE = "You can't trade this item.";
        public static String STR_ITEM_NOT_FOUND = "The specified item wasn't found.";
        public static String STR_NOT_SELL_ENABLE = "You can't sell this item.";
        public static String STR_ITEM_INEXIST = "The item doesn't exist.";
        public static String STR_REPAIR_FAILED = "The reparation has failed.";
        public static String STR_IMPROVE_FAILED = "The upgrade has failed.";
        public static String STR_IMPROVE_SUCCEED = "The upgrade has succeed.";
        public static String STR_ITEM_DAMAGED = "Please repair your item before.";
        public static String STR_FULL_CANNOT_BUY = "Your inventory is full!";
        public static String STR_COMMAND_NOT_FOUND = "The command doesn't exist.";
        public static String STR_NOT_ONLINE = "The player isn't online.";
        public static String STR_NO_MINE = "You can't mine here.";
        public static String STR_MINE_WITH_PECKER = "You need a pecker to mine.";
        public static String STR_MARRY = "{0} and {1} are now married!";
        public static String STR_FRIEND_ALREADY = "{0} is already your friend!";
        public static String STR_FRIEND_FULL = "Your friend list is full!";
        public static String STR_KILL_EXPERIENCE = "You got {0} exp points by killing a monster.";
        public static String STR_DONATE = "{0} has donated {1} silvers to the guild.";
        public static String STR_KICKOUT = "{0} doesn't respect the rules and he has been kicked out.";
        public static String STR_SYN_NOTENOUGH_DONATION_LEAVE = "You have to donate 20'000 before leaving the guild.";
        public static String STR_SYN_LEAVE = "{0} has leaved the guild.";
        public static String STR_SYN_JOIN = "{0} has joined the guild.";
        public static String STR_SYN_DISBAND = "The guild {0} has been disbanded.";
        public static String STR_SYN_CREATE = "{0} has created {1}!";
        public static String STR_NO_DISBAND = "You can't disband the guild.";
        public static String STR_BOOTH_FULL = "Your warehouse is full!";
        public static String STR_BOOTH_BUY = "{0} has payed you {1}{2} for {3}!";
        public static String STR_GOT_DRAGONBALL = "Congradulation! {0} got a DragonBall!";
        public static String STR_TOS_ACCEPTED = "You have accepted the ToS.";
        public static String STR_SENT_TO_JAIL = "{0} has been sent to jail for his crimes!";
        public static String STR_CANT_STORAGE = "The item cannot be stored.";

        // TODO add to StrRes.ini or whatever
        public static String STR_INVALID_GUILD_NAME = "The guild name is invalid.";
        public static String STR_ALLY_FULL = "You already have the maximum number of allies in your guild.";
        public static String STR_HIS_ALLY_FULL = "He already has the maximum number of allies in his guild.";
        public static String STR_NOT_ENOUGH_LEV = "You must be at least level {0}.";
        public static String STR_NOT_ENOUGH_MONEY = "You don't have {0} silvers.";
        public static String STR_NOT_AUTHORIZED = "You are not authorized to do the requested action.";

        /// <summary>
        /// Load all string resources and override the default values.
        /// </summary>
        public static void LoadStrRes()
        {
            sLogger.Info("Overriding string resources...");

            if (File.Exists(Program.RootPath + "/StrRes.ini"))
            {
                String[] lines = File.ReadAllLines(Program.RootPath + "/StrRes.ini", Program.Encoding);
                foreach (String line in lines)
                {
                    String[] parts = line.Split('=');

                    if (parts.Length != 2)
                    {
                        sLogger.Warn("Found an invalid line in StrRes.ini. Line: '{0}'", line);
                        continue;
                    }

                    var type = typeof(StrRes);
                    var field = type.GetField(parts[0]);
                    if (field == null)
                    {
                        sLogger.Warn("Field {0} is specified in StrRes.ini, but does not exist in StrRes class.", parts[0]);
                        continue;
                    }

                    field.SetValue(null, parts[1]);
                }
            }
            else
                sLogger.Info("StrRes.ini not found. Default values will be used.");
        }
    }
}
