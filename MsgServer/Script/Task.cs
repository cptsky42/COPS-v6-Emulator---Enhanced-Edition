// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.Collections.Generic;
using System.Linq;
using COServer.Entities;
using COServer.Network;
using MoonSharp.Interpreter;

namespace COServer.Script
{
    /// <summary>
    /// A task that will do actions based on a Lua script.
    /// </summary>
    public abstract class Task
    {
        /// <summary>
        /// The logger of the class.
        /// </summary>
        private static readonly log4net.ILog sLogger = log4net.LogManager.GetLogger(typeof(Task));

        /// <summary>
        /// The Moon# environment.
        /// </summary>
        private static volatile MoonSharp.Interpreter.Script sEnvironment = null;
        /// <summary>
        /// The lock for the global Moon# environment.
        /// </summary>
        private static object sLock = new object();

        /// <summary>
        /// Get the Moon# environment to execute a Lua function.
        /// </summary>
        /// <returns>The Moon# environment.</returns>
        protected static MoonSharp.Interpreter.Script GetEnvironment()
        {
            if (sEnvironment == null)
            {
                lock (sLock)
                {
                    if (sEnvironment == null)
                        sEnvironment = new MoonSharp.Interpreter.Script();
                }
            }

            return sEnvironment;
        }

        /// <summary>
        /// Register all C# functions in the Moon# environment.
        /// </summary>
        /// <returns>True on success, false otherwise.</returns>
        public static bool RegisterFunctions()
        {
            var env = GetEnvironment();

            UserData.RegisterAssembly();
            env.Globals["getName"] = (Func<Client, String>)GetName;
            env.Globals["getMate"] = (Func<Client, String>)GetMate;
            env.Globals["getLook"] = (Func<Client, UInt32>)GetLook;
            env.Globals["getHair"] = (Func<Client, UInt16>)GetHair;
            env.Globals["getMoney"] = (Func<Client, UInt32>)GetMoney;
            env.Globals["getExp"] = (Func<Client, UInt32>)GetExp;
            env.Globals["getForce"] = (Func<Client, UInt16>)GetForce;
            env.Globals["getHealth"] = (Func<Client, UInt16>)GetHealth;
            env.Globals["getDexterity"] = (Func<Client, UInt16>)GetDexterity;
            env.Globals["getSoul"] = (Func<Client, UInt16>)GetSoul;
            env.Globals["getAddPoints"] = (Func<Client, UInt16>)GetAddPoints;
            env.Globals["getCurHP"] = (Func<Client, UInt16>)GetCurHP;
            env.Globals["getMaxHP"] = (Func<Client, UInt16>)GetMaxHP;
            env.Globals["getCurMP"] = (Func<Client, UInt16>)GetCurMP;
            env.Globals["getMaxMP"] = (Func<Client, UInt16>)GetMaxMP;
            env.Globals["getPkPoints"] = (Func<Client, Int16>)GetPkPoints;
            env.Globals["getVirtue"] = (Func<Client, Int32>)GetVirtue;
            env.Globals["getLevel"] = (Func<Client, Byte>)GetLevel;
            env.Globals["getProfession"] = (Func<Client, Byte>)GetProfession;
            env.Globals["getMetempsychosis"] = (Func<Client, Byte>)GetMetempsychosis;
            env.Globals["getClanRank"] = (Func<Client, Byte>)GetClanRank;

            env.Globals["setHair"] = (Func<Client, UInt16, bool>)SetHair;
            env.Globals["setProfession"] = (Func<Client, Byte, bool>)SetProfession;

            env.Globals["addExp"] = (Func<Client, UInt32, bool>)AddExp;
            env.Globals["addLife"] = (Func<Client, UInt16, bool>)AddLife;
            env.Globals["addMana"] = (Func<Client, UInt16, bool>)AddMana;

            env.Globals["gainMoney"] = (Func<Client, UInt32, bool>)GainMoney;
            env.Globals["spendMoney"] = (Func<Client, UInt32, bool>)SpendMoney;

            env.Globals["gainVirtue"] = (Func<Client, Int32, bool>)GainVirtue;
            env.Globals["spendVirtue"] = (Func<Client, Int32, bool>)SpendVirtue;

            env.Globals["isMarried"] = (Func<Client, bool>)IsMarried;
            env.Globals["divorce"] = (Func<Client, bool>)Divorce;

            env.Globals["isWanted"] = (Func<Client, bool>)IsWanted;

            env.Globals["checkLockPin"] = (Func<Client, bool>)CheckLockPin;
            env.Globals["setLockPin"] = (Func<Client, bool>)SetLockPin;

            env.Globals["awardItem"] = (Func<Client, String, int, bool>)AwardItem;
            env.Globals["spendItem"] = (Func<Client, Int32, int, bool>)SpendItem;
            env.Globals["spendItems"] = (Func<Client, Int32, Int32, int, bool>)SpendItems;
            env.Globals["spendTaskItem"] = (Func<Client, String, bool>)SpendTaskItem;
            env.Globals["hasItem"] = (Func<Client, Int32, int, bool>)HasItem;
            env.Globals["hasItems"] = (Func<Client, Int32, Int32, int, bool>)HasItems;
            env.Globals["hasTaskItem"] = (Func<Client, String, bool>)HasTaskItem;
            env.Globals["getItemsCount"] = (Func<Client, int>)GetItemsCount;
            env.Globals["deleteItem"] = (Func<Item, bool>)DeleteItem;

            env.Globals["chkEquipHole"] = (Func<Client, byte, bool>)ChkEquipHole;
            env.Globals["makeEquipHole"] = (Func<Client, byte, bool>)MakeEquipHole;
            env.Globals["upEquipLevel"] = (Func<Client, Byte, bool>)UpEquipLevel;
            env.Globals["upEquipQuality"] = (Func<Client, Byte, bool>)UpEquipQuality;
            env.Globals["recoverEquipDura"] = (Func<Client, Byte, bool>)RecoverEquipDura;
            env.Globals["getEquipByPos"] = (Func<Client, Byte, UInt32>)GetEquipByPos;

            env.Globals["hasMagic"] = (Func<Client, UInt16, SByte, bool>)HasMagic;
            env.Globals["hasSkill"] = (Func<Client, UInt16, SByte, bool>)HasSkill;
            env.Globals["awardMagic"] = (Func<Client, UInt16, Byte, bool>)AwardMagic;
            env.Globals["awardSkill"] = (Func<Client, UInt16, Byte, bool>)AwardSkill;
            env.Globals["unlearnMagic"] = (Func<Client, UInt16, Boolean, bool>)UnlearnMagic;
            env.Globals["unlearnSkills"] = (Func<Client, bool>)UnlearnSkills;

            env.Globals["upMagicLevel"] = (Func<Client, UInt16, bool>)UpMagicLevel;

            env.Globals["getMapAttrib"] = (Func<Client, UInt32, String, UInt32>)GetMapAttrib;
            env.Globals["getRecordMap"] = (Func<Client, UInt32>)GetRecordMap;
            env.Globals["getRecordX"] = (Func<Client, UInt16>)GetRecordX;
            env.Globals["getRecordY"] = (Func<Client, UInt16>)GetRecordY;
            env.Globals["setRecordPos"] = (Func<Client, UInt32, UInt16, UInt16, bool>)SetRecordPos;
            env.Globals["move"] = (Func<Client, UInt32, UInt16, UInt16, bool>)Move;
            env.Globals["moveToRebornMap"] = (Func<Client, bool>)MoveToRebornMap;
            env.Globals["playersOnMap"] = (Func<Client, UInt32, int>)PlayersOnMap;
            env.Globals["alivePlayersOnMap"] = (Func<Client, UInt32, int>)AlivePlayersOnMap;

            env.Globals["text"] = (Action<Client, String>)Text;
            env.Globals["link"] = (Action<Client, String, Byte>)Link;
            env.Globals["edit"] = (Action<Client, String, Byte, Byte>)Edit;
            env.Globals["pic"] = (Action<Client, UInt16>)Pic;
            env.Globals["create"] = (Action<Client>)Create;

            env.Globals["getRegister"] = (Func<Client, byte, int>)GetRegister;
            env.Globals["setRegister"] = (Func<Client, byte, int, bool>)SetRegister;

            env.Globals["checkUserTask"] = (Func<Client, Byte, bool>)CheckUserTask;
            env.Globals["setUserTask"] = (Func<Client, Byte, bool>)SetUserTask;
            env.Globals["clearUserTask"] = (Func<Client, Byte, bool>)ClearUserTask;

            env.Globals["getUserStats"] = (Func<Client, int, int, int>)GetUserStats;
            env.Globals["setUserStats"] = (Action<Client, int, int, int, bool>)SetUserStats;

            env.Globals["checkTime"] = (Func<Client, UInt32, String, bool>)CheckTime;
            env.Globals["play"] = (Func<Client, String, Boolean, bool>)Play;
            env.Globals["broadcastSysMsg"] = (Func<Client, String, UInt16, bool>)BroadcastSysMsg;
            env.Globals["broadcastMapMsg"] = (Func<Client, UInt32, String, UInt16, bool>)BroadcastMapMsg;
            env.Globals["broadcastMapEffect"] = (Func<Client, UInt32, UInt16, UInt16, String, bool>)BroadcastMapEffect;
            env.Globals["broadcastMapFireworks"] = (Func<Client, bool>)BroadcastMapFireworks;
            env.Globals["broadcastEffect"] = (Func<Client, String, bool>)BroadcastEffect;
            env.Globals["sendSysMsg"] = (Func<Client, String, UInt16, bool>)SendSysMsg;
            env.Globals["postCmd"] = (Func<Client, UInt32, bool>)PostCmd;
            env.Globals["openDialog"] = (Func<Client, UInt32, bool>)OpenDialog;

            env.Globals["moveNpc"] = (Func<Client, Int32, UInt32, UInt16, UInt16, bool>)MoveNpc;

            env.Globals["rand"] = (Func<Client, int, int>)Rand;
            env.Globals["randomAction"] = (Func<Client, Byte, Byte, Byte>)RandomAction;

            env.Globals["dropItem"] = (Func<Monster, Client, Int32, bool>)DropItem;

            env.Globals["createClan"] = (Func<Client, Byte, UInt32, UInt32, bool>)CreateClan;
            env.Globals["destroyClan"] = (Func<Client, bool>)DestroyClan;
            env.Globals["donateToClan"] = (Func<Client, bool>)DonateToClan;
            env.Globals["addClanEnemy"] = (Func<Client, bool>)AddClanEnemy;
            env.Globals["removeClanEnemy"] = (Func<Client, bool>)RemoveClanEnemy;
            env.Globals["addClanAlly"] = (Func<Client, bool>)AddClanAlly;
            env.Globals["removeClanAlly"] = (Func<Client, bool>)RemoveClanAlly;
            env.Globals["demiseFromClan"] = (Func<Client, bool>)DemiseFromClan;
            env.Globals["setClanAssistant"] = (Func<Client, bool>)SetClanAssistant;
            env.Globals["removeClanAssistant"] = (Func<Client, bool>)RemoveClanAssistant;
            env.Globals["createSubClan"] = (Func<Client, Byte, UInt32, UInt32, bool>)CreateSubClan;
            env.Globals["changeSubClanLeader"] = (Func<Client, Byte, bool>)ChangeSubClanLeader;

            return true;
        }

        /// <summary>
        /// The name of the Lua fonction to call.
        /// </summary>
        protected readonly String mFct;

        /// <summary>
        /// Create a new task using the specified Lua script.
        /// </summary>
        /// <param name="aPath">The path of the Lua script</param>
        /// <param name="aFct">The function to call</param>
        protected Task(String aPath, String aFct)
        {
            var env = GetEnvironment();
            var res = env.DoFile(aPath);

            mFct = aFct;
        }

        /// <summary>
        /// Execute the task using the specified arguments.
        /// </summary>
        /// <param name="aClient">The client executing the task</param>
        /// <param name="aArgs">The arguments of the task</param>
        public abstract void Execute(Client aClient, params object[] aArgs);


        /////////////////////////////////////////////////////////////////////////////////////////
        // C# functions exposed in the Lua environment
        /////////////////////////////////////////////////////////////////////////////////////////

        private static String GetName(Client aClient)
        {
            return aClient.Player.Name;
        }

        private static String GetMate(Client aClient)
        {
            return aClient.Player.Mate;
        }

        private static UInt32 GetLook(Client aClient)
        {
            return aClient.Player.Look;
        }

        private static UInt16 GetHair(Client aClient)
        {
            return aClient.Player.Hair;
        }

        private static UInt32 GetMoney(Client aClient)
        {
            return aClient.Player.Money;
        }

        private static UInt32 GetExp(Client aClient)
        {
            return aClient.Player.Exp;
        }

        private static UInt16 GetForce(Client aClient)
        {
            return aClient.Player.Strength;
        }

        private static UInt16 GetHealth(Client aClient)
        {
            return aClient.Player.Vitality;
        }

        private static UInt16 GetDexterity(Client aClient)
        {
            return aClient.Player.Agility;
        }

        private static UInt16 GetSoul(Client aClient)
        {
            return aClient.Player.Spirit;
        }

        private static UInt16 GetAddPoints(Client aClient)
        {
            return aClient.Player.AddPoints;
        }

        private static UInt16 GetCurHP(Client aClient)
        {
            return (UInt16)aClient.Player.CurHP;
        }

        private static UInt16 GetMaxHP(Client aClient)
        {
            return (UInt16)aClient.Player.MaxHP;
        }

        private static UInt16 GetCurMP(Client aClient)
        {
            return aClient.Player.CurMP;
        }

        private static UInt16 GetMaxMP(Client aClient)
        {
            return aClient.Player.MaxMP;
        }

        private static Int16 GetPkPoints(Client aClient)
        {
            return aClient.Player.PkPoints;
        }

        private static Int32 GetVirtue(Client aClient)
        {
            return aClient.Player.VPs;
        }

        private static Byte GetLevel(Client aClient)
        {
            return (Byte)aClient.Player.Level;
        }

        private static Byte GetProfession(Client aClient)
        {
            return aClient.Player.Profession;
        }

        private static Byte GetMetempsychosis(Client aClient)
        {
            return aClient.Player.Metempsychosis;
        }

        private static Byte GetClanRank(Client aClient)
        {
            Player player = aClient.Player;
            if (player.Syndicate == null)
                return (Byte)Syndicate.Rank.None;

            Syndicate.Member member = player.Syndicate.GetMemberInfo(player.UniqId);
            if (member == null)
                return (Byte)Syndicate.Rank.None;

            return (Byte)member.Rank;
        }

        private static bool SetHair(Client aClient, UInt16 aHair)
        {
            aClient.Player.Hair = aHair;
            return true;
        }

        private static bool SetProfession(Client aClient, Byte aProfession)
        {
            aClient.Player.Profession = aProfession;
            aClient.Send(new MsgUserAttrib(aClient.Player, aClient.Player.Profession, MsgUserAttrib.AttributeType.Profession));
            return true;
        }

        private static bool AddExp(Client aClient, UInt32 aExp)
        {
            aClient.Player.AddExp(aExp, false);
            return true;
        }

        private static bool AddLife(Client aClient, UInt16 aLife)
        {
            if (aClient.Player.CurHP + aLife > aClient.Player.MaxHP)
                aClient.Player.CurHP = aClient.Player.MaxHP;
            else
                aClient.Player.CurHP += aLife;
            return true;
        }

        private static bool AddMana(Client aClient, UInt16 aMana)
        {
            if (aClient.Player.CurMP + aMana > aClient.Player.MaxMP)
                aClient.Player.CurMP = aClient.Player.MaxMP;
            else
                aClient.Player.CurMP += aMana;
            return true;
        }

        private static bool GainMoney(Client aClient, UInt32 aMoney)
        {
            if (aClient.Player.Money + aMoney < UInt32.MaxValue)
            {
                aClient.Player.Money += aMoney;
                aClient.Send(new MsgUserAttrib(aClient.Player, aClient.Player.Money, MsgUserAttrib.AttributeType.Money));
                return true;
            }

            return false;
        }

        private static bool SpendMoney(Client aClient, UInt32 aMoney)
        {
            if (aClient.Player.Money >= aMoney)
            {
                aClient.Player.Money -= aMoney;
                aClient.Send(new MsgUserAttrib(aClient.Player, aClient.Player.Money, MsgUserAttrib.AttributeType.Money));
                return true;
            }

            return false;
        }

        private static bool GainVirtue(Client aClient, Int32 aVirtue)
        {
            if (aClient.Player.VPs + aVirtue < Int32.MaxValue)
            {
                aClient.Player.VPs += aVirtue;
                return true;
            }

            return false;
        }

        private static bool SpendVirtue(Client aClient, Int32 aVirtue)
        {
            if (aClient.Player.VPs >= aVirtue)
            {
                aClient.Player.VPs -= aVirtue;
                return true;
            }

            return false;
        }

        private static bool IsMarried(Client aClient)
        {
            return aClient.Player.Mate != "None";
        }

        private static bool Divorce(Client aClient)
        {
            throw new NotImplementedException();
        }

        private static bool IsWanted(Client aClient)
        {
            // TODO Bounty thing
            return aClient.Player.PkPoints >= 100;
        }

        private static bool CheckLockPin(Client aClient)
        {
            if (aClient.Player.CheckLockPin(0))
                return true;

            UInt32 pin = 0;
            if (!UInt32.TryParse(aClient.TaskData, out pin))
                return false;

            return aClient.Player.CheckLockPin(pin);
        }

        private static bool SetLockPin(Client aClient)
        {
            UInt32 pin = 0;

            if (!String.IsNullOrEmpty(aClient.TaskData))
            {
                if (!UInt32.TryParse(aClient.TaskData, out pin))
                    return false;
            }

            return aClient.Player.SetLockPin(pin);
        }

        private static bool AwardItem(Client aClient, String aItem, int aCount)
        {
            String[] parts = aItem.Split(' ');

            Int32 itemtype = 0;
            UInt16 amount = 0, amountLimit = 0;
            Byte ident = 0, firstGem = 0, secondGem = 0, magic1 = 0, magic2 = 0, magic3 = 0, bless = 0, enchant = 0;
            if (parts.Length == 11)
            {
                if (!Int32.TryParse(parts[0], out itemtype)) return false;
                if (!UInt16.TryParse(parts[1], out amount)) return false;
                if (!UInt16.TryParse(parts[2], out amountLimit)) return false;
                if (!Byte.TryParse(parts[3], out ident)) return false;
                if (!Byte.TryParse(parts[4], out firstGem)) return false;
                if (!Byte.TryParse(parts[5], out secondGem)) return false;
                if (!Byte.TryParse(parts[6], out magic1)) return false;
                if (!Byte.TryParse(parts[7], out magic2)) return false;
                if (!Byte.TryParse(parts[8], out magic3)) return false;
                if (!Byte.TryParse(parts[9], out bless)) return false;
                if (!Byte.TryParse(parts[10], out enchant)) return false;
            }
            else
            {
                if (!Int32.TryParse(aItem, out itemtype))
                    return false;
            }

            Item.Info info;
            if (!Database.AllItems.TryGetValue(itemtype, out info))
                return false;

            if (amount == 0 && amountLimit == 0)
            {
                amount = info.Amount;
                amountLimit = info.AmountLimit;
            }

            for (int i = 0; i < aCount; ++i)
            {
                Item item = Item.Create(0, 0, itemtype, magic3, bless, enchant, firstGem, secondGem, magic1, 0, amount, amountLimit);
                aClient.Player.AddItem(item, true);
            }

            return true;
        }

        private static bool SpendItem(Client aClient, Int32 aItemType, int aCount)
        {
            aClient.Player.DelItem(aItemType, aCount, true);
            return true;
        }

        private static bool SpendItems(Client aClient, Int32 aFirstType, Int32 aLastType, int aCount)
        {
            lock (aClient.Player.Items)
            {
                List<Int32> uids = new List<Int32>(aCount);

                var items = from item in aClient.Player.Items.Values where item.Position == 0 select item;

                int count = 0;
                foreach (var item in items)
                {
                    if (item.Type >= aFirstType && item.Type <= aLastType)
                    {
                        uids.Add(item.Id);
                        if (count >= aCount)
                            break;
                    }
                }

                if (count >= aCount)
                {
                    foreach (Int32 uid in uids)
                        aClient.Player.DelItem(uid, true);

                    return true;
                }
            }

            return false;
        }

        private static bool SpendTaskItem(Client aClient, String aItemName)
        {
            lock (aClient.Player.Items)
            {
                var items = from item in aClient.Player.Items.Values where item.Position == 0 select item;

                foreach (var item in items)
                {
                    if (item.Name == aItemName)
                    {
                        aClient.Player.DelItem(item.Id, true);
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool HasItem(Client aClient, Int32 aItemType, int aCount)
        {
            return aClient.Player.InventoryContains(aItemType, aCount);
        }

        private static bool HasItems(Client aClient, Int32 aFirstType, Int32 aLastType, int aCount)
        {
            lock (aClient.Player.Items)
            {
                var items = from item in aClient.Player.Items.Values where item.Position == 0 select item;

                int count = 0;
                foreach (var item in items)
                {
                    if (item.Type >= aFirstType && item.Type <= aLastType)
                    {
                        if (++count >= aCount)
                            return true;
                    }
                }
            }

            return false;
        }
        
        private static bool HasTaskItem(Client aClient, String aItemName)
        {
            lock (aClient.Player.Items)
            {
                var items = from item in aClient.Player.Items.Values where item.Position == 0 select item;

                foreach (var item in items)
                {
                    if (item.Name == aItemName)
                        return true;
                }
            }

            return false;
        }

        private static int GetItemsCount(Client aClient)
        {
            return aClient.Player.ItemInInventory();
        }

        private static bool DeleteItem(Item aItem)
        {
            Player owner = null;
            if (!World.AllPlayers.TryGetValue(aItem.OwnerUID, out owner))
                return false;

            owner.DelItem(aItem, true);
            return true;
        }

        private static bool ChkEquipHole(Client aClient, byte aData)
        {
            Item item = aClient.Player.GetItemByPos(4);

            if (item != null)
            {
                if (1 == aData && GemType.None != (GemType)item.FirstGem)
                    return true;
                else if (2 == aData && GemType.None != (GemType)item.SecondGem)
                    return true;
            }

            return false;
        }

        private static bool MakeEquipHole(Client aClient, byte aData)
        {
            Item item = aClient.Player.GetItemByPos(4);

            if (item != null)
            {
                if (1 == aData && 
                    GemType.None == (GemType)item.FirstGem)
                {
                    item.FirstGem = (Byte)GemType.Hole;
                    aClient.Send(new MsgItemInfo(item, MsgItemInfo.Action.Update));
                }
                else if (2 == aData && 
                    GemType.None != (GemType)item.FirstGem && 
                    GemType.None == (GemType)item.SecondGem)
                {
                    item.SecondGem = (Byte)GemType.Hole;
                    aClient.Send(new MsgItemInfo(item, MsgItemInfo.Action.Update));
                }
            }

            return false;
        }

        private static bool UpEquipLevel(Client aClient, Byte aPos)
        {
            var equip = aClient.Player.GetItemByPos(aPos);
            if (equip == null)
                return false;

            Int32 nextType = 0;
            double chance = 0;

            if (!ItemHandler.GetUpLevelInfo(equip, out chance, out nextType))
                return false;

            Item.Info info;
            if (!Database.AllItems.TryGetValue(nextType, out info))
                return false;

            if (info.RequiredLevel > aClient.Player.Level)
            {
                aClient.Player.SendSysMsg("StrRes.STR_NEXTEQP_OVERLEV"); // TODO STR_NEXTEQP_OVERLEV
                return false;
            }

            int cost = (int)(Math.Ceiling((100.00 / chance + 1) * 1.2));
            if (cost > 40)
                cost = 40;

            if (!HasItems(aClient, Item.TYPE_METEOR, Item.TYPE_DIVORCEITEM, cost))
                return false;

            if (!SpendItems(aClient, Item.TYPE_METEOR, Item.TYPE_DIVORCEITEM, cost))
                return false;

            equip.Type = nextType;

            // durability, 2 percent chance to increase 100-300 point
            UInt16 amountLimit = equip.MaxDura;
            if (MyMath.Generate(0, 100) < 2)
                amountLimit += (UInt16)(MyMath.Generate(1, 3) * 100);

            // gem effect
            amountLimit = (UInt16)(amountLimit * equip.GetGemDurEffect());

            // update durability
            equip.CurDura = amountLimit;
            equip.MaxDura = amountLimit;

            // gem hole, 1 percent chance to make a hole
            if (MyMath.Generate(0, 300) < 1)
            {
                if (GemType.None == (GemType)equip.FirstGem)
                    equip.FirstGem = (Byte)GemType.Hole;
                else if (GemType.None == (GemType)equip.SecondGem)
                    equip.SecondGem = (Byte)GemType.Hole;
            }

            // inform client
            /* aClient.Send(new MsgItemInfo(equip, MsgItemInfo.Action.Update)); */
            World.BroadcastRoomMsg(aClient.Player, new MsgItemInfoEx(equip.Id, equip, 0, MsgItemInfoEx.Action.Equipment), true);
            MyMath.GetEquipStats(aClient.Player);

	        return true;
        }

        private static bool UpEquipQuality(Client aClient, Byte aPos)
        {
            var equip = aClient.Player.GetItemByPos(aPos);
            if (equip == null)
                return false;

            Int32 nextType = 0;
            double chance = 0;

            if (!ItemHandler.GetUpQualityInfo(equip, out chance, out nextType))
                return false;

            Item.Info info;
            if (!Database.AllItems.TryGetValue(nextType, out info))
                return false;

            int cost = (int)Math.Ceiling(100.00 / chance + 1);
            if (cost > 40)
                cost = 40;

            if (!HasItem(aClient, Item.TYPE_DRAGONBALL, cost))
                return false;

            if (!SpendItem(aClient, Item.TYPE_DRAGONBALL, cost))
                return false;

            equip.Type = nextType;

            // durability, 2 percent chance to increase 100-300 point
            UInt16 amountLimit = equip.MaxDura;
            if (MyMath.Generate(0, 100) < 2)
                amountLimit += (UInt16)(MyMath.Generate(1, 3) * 100);

            // gem effect
            amountLimit = (UInt16)(amountLimit * equip.GetGemDurEffect());

            // update durability
            equip.CurDura = amountLimit;
            equip.MaxDura = amountLimit;

            // gem hole, 1 percent chance to make a hole
            if (MyMath.Generate(0, 300) < 1)
            {
                if (GemType.None == (GemType)equip.FirstGem)
                    equip.FirstGem = (Byte)GemType.Hole;
                else if (GemType.None == (GemType)equip.SecondGem)
                    equip.SecondGem = (Byte)GemType.Hole;
            }

            // inform client
            /* aClient.Send(new MsgItemInfo(equip, MsgItemInfo.Action.Update)); */
            World.BroadcastRoomMsg(aClient.Player, new MsgItemInfoEx(equip.Id, equip, 0, MsgItemInfoEx.Action.Equipment), true);
            MyMath.GetEquipStats(aClient.Player);

            return true;
        }

        private static bool RecoverEquipDura(Client aClient, Byte aPos)
        {
            var equip = aClient.Player.GetItemByPos(aPos);
            if (equip == null)
                return false;

            Item.Info info;
            if (!Database.AllItems.TryGetValue(equip.Type, out info))
                return false;

	        // take cost
            UInt32 recoverCost = equip.GetRecoverDurCost();
            if (!SpendMoney(aClient, recoverCost))
                return false;

	        // recover dur
            equip.MaxDura = (UInt16)(info.AmountLimit * equip.GetGemDurEffect());
		
            // inform client
            aClient.Send(new MsgItemInfo(equip, MsgItemInfo.Action.Update));

            return true;
        }

        private static UInt32 GetEquipByPos(Client aClient, Byte aPos)
        {
            var equip = aClient.Player.GetItemByPos(aPos);
            return equip != null ? (UInt32)equip.Type : 0;
        }

        private static bool HasMagic(Client aClient, UInt16 aType, SByte aLevel)
        {
            var magic = aClient.Player.GetMagicByType(aType);
            if (magic == null)
                return false;

            if (aLevel >= 0 && magic.Level != aLevel)
                return false;

            return true;
        }

        private static bool HasSkill(Client aClient, UInt16 aType, SByte aLevel)
        {
            var skill = aClient.Player.GetWeaponSkillByType(aType);
            if (skill == null)
                return false;

            if (aLevel >= 0 && skill.Level != aLevel)
                return false;

            return true;
        }

        private static bool AwardMagic(Client aClient, UInt16 aType, Byte aLevel)
        {
            Magic magic = Magic.Create(aClient.Player, aType, aLevel);
            aClient.Player.AwardMagic(magic, true);

            return true;
        }

        private static bool AwardSkill(Client aClient, UInt16 aType, Byte aLevel)
        {
            WeaponSkill skill = WeaponSkill.Create(aClient.Player, aType, aLevel);
            aClient.Player.AwardSkill(skill, true);

            return true;
        }

        private static bool UnlearnMagic(Client aClient, UInt16 aType, bool aDrop)
        {
            Magic magic = aClient.Player.GetMagicByType(aType);
            if (magic == null)
                return false;

            if (aDrop)
                aClient.Player.DropMagic(magic, true);
            else
                throw new NotImplementedException("Unlearn magic !");

            return true;
        }

        private static bool UnlearnSkills(Client aClient)
        {
            throw new NotImplementedException();
        }

        private static bool UpMagicLevel(Client aClient, UInt16 aType)
        {
            Magic magic = aClient.Player.GetMagicByType(aType);
            if (magic == null)
                return false;

            if (!Database.AllMagics.ContainsKey((magic.Type * 10) + magic.Level + 1))
                return false;

            ++magic.Level;
            magic.Exp = 0;

            aClient.Send(new MsgMagicInfo(magic));
            return true;
        }

        private static UInt32 GetMapAttrib(Client aClient, UInt32 aMapId, String aField)
        {
            GameMap map = aClient.Player.Map;

            if (aMapId != 0)
            {
                if (!MapManager.TryGetMap(aMapId, out map))
                    return 0;
            }

            switch (aField)
            {
                case "mapdoc":
                    return map.DocId;
                default:
                    throw new NotImplementedException();
            }
        }

        private static UInt32 GetRecordMap(Client aClient)
        {
            return aClient.Player.PrevMap.Id;
        }

        private static UInt16 GetRecordX(Client aClient)
        {
            return aClient.Player.PrevX;
        }

        private static UInt16 GetRecordY(Client aClient)
        {
            return aClient.Player.PrevY;
        }

        private static bool SetRecordPos(Client aClient, UInt32 aMapId, UInt16 aX, UInt16 aY)
        {
            GameMap prevMap;
            if (!MapManager.TryGetMap(aMapId, out prevMap))
                return false;

            aClient.Player.PrevMap = prevMap;
            aClient.Player.PrevX = aX;
            aClient.Player.PrevY = aY;

            return true;
        }

        private static bool Move(Client aClient, UInt32 aMapId, UInt16 aX, UInt16 aY)
        {
            aClient.Player.Move(aMapId, aX, aY);
            return true;
        }

        private static bool MoveToRebornMap(Client aClient)
        {
            GameMap rebornMap = null;

            if (!MapManager.TryGetMap(aClient.Player.Map.RebornMap, out rebornMap))
                return false;

            aClient.Player.Move(rebornMap.Id, rebornMap.PortalX, rebornMap.PortalY);
            return true;
        }

        private static int PlayersOnMap(Client aClient, UInt32 aMapId)
        {
            GameMap map;
            if (!MapManager.TryGetMap(aMapId, out map))
                return 0;

            var players = from entity in map.Entities.Values where entity.IsPlayer() select entity;
            return players.Count();
        }

        private static int AlivePlayersOnMap(Client aClient, UInt32 aMapId)
        {
            GameMap map;
            if (!MapManager.TryGetMap(aMapId, out map))
                return 0;

            var players = from entity in map.Entities.Values where entity.IsPlayer() && ((Player)entity).IsAlive() select entity;
            return players.Count();
        }

        private static int GetRegister(Client aClient, byte aRegId)
        {
            object data = aClient.Player.GetRegData(aRegId);
            return data != null ? (int)data : 0;
        }

        private static bool SetRegister(Client aClient, byte aRegId, int aData)
        {
            return aClient.Player.SetRegData(aRegId, aData);
        }

        private static bool CheckUserTask(Client aClient, Byte aTaskId)
        {
            return aClient.Player.UserTasks.Contains(aTaskId);
        }

        private static bool SetUserTask(Client aClient, Byte aTaskId)
        {
            aClient.Player.UserTasks.Add(aTaskId);
            return true;
        }

        private static bool ClearUserTask(Client aClient, Byte aTaskId)
        {
            return aClient.Player.UserTasks.Remove(aTaskId);
        }

        private static int GetUserStats(Client aClient, int aEventId, int aType)
        {
            int data = 0;
            aClient.Player.UserStats.TryGetValue(new Tuple<int, int>(aEventId, aType), out data);

            return data;
        }

        private static void SetUserStats(Client aClient, int aEventId, int aType, int aData, bool aSave)
        {
            // TODO handle saving of user stats
            aClient.Player.UserStats[new Tuple<int, int>(aEventId, aType)] = aData;
        }

        private static bool CheckTime(Client aClient, UInt32 aType, String aParam)
        {
            if (String.IsNullOrEmpty(aParam))
                return false;

            DateTime ltime = DateTime.Now;

            int year0 = 0, month0 = 0, day0 = 0, hour0 = 0, minute0 = 0;
            int year1 = 0, month1 = 0, day1 = 0, hour1 = 0, minute1 = 0;

            switch (aType)
            {
                case 0:
                    {
                        aParam = aParam.Replace('-', ' ');
                        aParam = aParam.Replace(':', ' ');

                        string[] parts = aParam.Split(' ');
                        if (parts.Length != 10)
                            return false;

                        if (!int.TryParse(parts[0], out year0)) return false;
                        if (!int.TryParse(parts[1], out month0)) return false;
                        if (!int.TryParse(parts[2], out day0)) return false;
                        if (!int.TryParse(parts[3], out hour0)) return false;
                        if (!int.TryParse(parts[4], out minute0)) return false;
                        if (!int.TryParse(parts[5], out year1)) return false;
                        if (!int.TryParse(parts[6], out month1)) return false;
                        if (!int.TryParse(parts[7], out day1)) return false;
                        if (!int.TryParse(parts[8], out hour1)) return false;
                        if (!int.TryParse(parts[9], out minute1)) return false;

                        DateTime time0 = new DateTime(year0, month0, day0, hour0, minute0, ltime.Second);
                        DateTime time1 = new DateTime(year1, month1, day1, hour1, minute1, ltime.Second);

                        if (ltime >= time0 && ltime <= time1)
                            return true;

                        break;
                    }
                case 1:
                    {
                        aParam = aParam.Replace('-', ' ');
                        aParam = aParam.Replace(':', ' ');

                        string[] parts = aParam.Split(' ');
                        if (parts.Length != 8)
                            return false;

                        if (!int.TryParse(parts[0], out month0)) return false;
                        if (!int.TryParse(parts[1], out day0)) return false;
                        if (!int.TryParse(parts[2], out hour0)) return false;
                        if (!int.TryParse(parts[3], out minute0)) return false;
                        if (!int.TryParse(parts[4], out month1)) return false;
                        if (!int.TryParse(parts[5], out day1)) return false;
                        if (!int.TryParse(parts[6], out hour1)) return false;
                        if (!int.TryParse(parts[7], out minute1)) return false;

                        DateTime time0 = new DateTime(ltime.Year, month0, day0, hour0, minute0, ltime.Second);
                        DateTime time1 = new DateTime(ltime.Year, month1, day1, hour1, minute1, ltime.Second);

                        if (ltime >= time0 && ltime <= time1)
                            return true;

                        break;
                    }
                case 2:
                    {
                        aParam = aParam.Replace('-', ' ');
                        aParam = aParam.Replace(':', ' ');

                        string[] parts = aParam.Split(' ');
                        if (parts.Length != 6)
                            return false;

                        if (!int.TryParse(parts[0], out day0)) return false;
                        if (!int.TryParse(parts[1], out hour0)) return false;
                        if (!int.TryParse(parts[2], out minute0)) return false;
                        if (!int.TryParse(parts[3], out day1)) return false;
                        if (!int.TryParse(parts[4], out hour1)) return false;
                        if (!int.TryParse(parts[5], out minute1)) return false;

                        DateTime time0 = new DateTime(ltime.Year, ltime.Month, day0, hour0, minute0, ltime.Second);
                        DateTime time1 = new DateTime(ltime.Year, ltime.Month, day1, hour1, minute1, ltime.Second);

                        if (ltime >= time0 && ltime <= time1)
                            return true;

                        break;
                    }
                case 3:
                    {
                        aParam = aParam.Replace('-', ' ');
                        aParam = aParam.Replace(':', ' ');

                        string[] parts = aParam.Split(' ');
                        if (parts.Length != 6)
                            return false;

                        if (!int.TryParse(parts[0], out day0)) return false;
                        if (!int.TryParse(parts[1], out hour0)) return false;
                        if (!int.TryParse(parts[2], out minute0)) return false;
                        if (!int.TryParse(parts[3], out day1)) return false;
                        if (!int.TryParse(parts[4], out hour1)) return false;
                        if (!int.TryParse(parts[5], out minute1)) return false;

                        int timeNow = (int)ltime.DayOfWeek * 24 * 60 + ltime.Hour * 60 + ltime.Minute;
                        if (timeNow >= day0 * 24 * 60 + hour0 * 60 + minute0 &&
                            timeNow <= day1 * 24 * 60 + hour1 * 60 + minute1)
                            return true;

                        break;
                    }
                case 4:
                    {
                        aParam = aParam.Replace('-', ' ');
                        aParam = aParam.Replace(':', ' ');

                        string[] parts = aParam.Split(' ');
                        if (parts.Length != 4)
                            return false;

                        if (!int.TryParse(parts[0], out hour0)) return false;
                        if (!int.TryParse(parts[1], out minute0)) return false;
                        if (!int.TryParse(parts[2], out hour1)) return false;
                        if (!int.TryParse(parts[3], out minute1)) return false;

                        int timeNow = ltime.Hour * 60 + ltime.Minute;
                        if (timeNow >= hour0 * 60 + minute0 &&
                            timeNow <= hour1 * 60 + minute1)
                            return true;

                        break;
                    }
                case 5:
                    {
                        aParam = aParam.Replace('-', ' ');
                        aParam = aParam.Replace(':', ' ');

                        string[] parts = aParam.Split(' ');
                        if (parts.Length != 2)
                            return false;

                        if (!int.TryParse(parts[0], out minute0)) return false;
                        if (!int.TryParse(parts[1], out minute1)) return false;

                        if (ltime.Minute >= minute0 && ltime.Minute <= minute1)
                            return true;

                        break;
                    }
            }

            return false;
        }

        private static bool Play(Client aClient, String aMedia, Boolean aBroadcast)
        {
            var msg = new MsgName(0, aMedia, MsgName.NameAct.Sound);
            aClient.Send(msg);

            if (aBroadcast)
                World.BroadcastRoomMsg(aClient.Player, msg);

            return true;
        }

        private static bool BroadcastSysMsg(Client aClient, String aMessage, UInt16 aChannel)
        {
            var msg = new MsgTalk("SYSTEM", "ALLUSERS",  aMessage, (Channel)aChannel, Color.White);
            World.BroadcastMsg(msg);

            return true;
        }

        private static bool BroadcastMapMsg(Client aClient, UInt32 aMapId, String aMessage, UInt16 aChannel)
        {
            GameMap map;
            if (!MapManager.TryGetMap(aMapId, out map))
                return false;

            var msg = new MsgTalk("SYSTEM", "ALLUSERS",  aMessage, (Channel)aChannel, Color.White);
            World.BroadcastMapMsg(aMapId, msg);

            return true;
        }

        private static bool BroadcastMapEffect(Client aClient, UInt32 aMapId, UInt16 aX, UInt16 aY, String aEffect)
        {
            GameMap map;
            if (!MapManager.TryGetMap(aMapId, out map))
                return false;

            var msg = new MsgName(aX, aY, aEffect, MsgName.NameAct.MapEffect);
            map.BroadcastBlockMsg(aX, aY, msg);

            return true;
        }

        private static bool BroadcastMapFireworks(Client aClient)
        {
            var msg = new MsgItem(aClient.Player.UniqId, 0, MsgItem.Action.Fireworks);
            World.BroadcastRoomMsg(aClient.Player, msg, true);

            return true;
        }

        private static bool BroadcastEffect(Client aClient, String aEffect)
        {
            var msg = new MsgName(aClient.Player.UniqId, aEffect, MsgName.NameAct.RoleEffect);
            World.BroadcastRoomMsg(aClient.Player, msg, true);

            return true;
        }

        private static bool SendSysMsg(Client aClient, String aMessage, UInt16 aChannel)
        {
            var msg = new MsgTalk("SYSTEM", "ALLUSERS", aMessage, (Channel)aChannel, Color.White);
            aClient.Send(msg);

            return true;
        }

        private static bool PostCmd(Client aClient, UInt32 aCmd)
        {
            var msg = new MsgAction(aClient.Player, aCmd, MsgAction.Action.PostCmd);
            aClient.Send(msg);

            return true;
        }

        private static bool OpenDialog(Client aClient, UInt32 aDialog)
        {
            var msg = new MsgAction(aClient.Player, aDialog, MsgAction.Action.OpenDialog);
            aClient.Send(msg);

            return true;
        }

        private static void Text(Client aClient, String aText)
        {
            for (int i = 0; i < aText.Length; i += 250)
            {
                Byte len = 250;
                if (i + 250 > aText.Length)
                    len = (Byte)(aText.Length - i);

                aClient.Send(new MsgDialog(aText.Substring(i, len), 0, 0, MsgDialog.Action.Text));
            }
        }

        private static void Link(Client aClient, String aText, Byte aIdx)
        {
            aClient.Send(new MsgDialog(aText, 0, aIdx, MsgDialog.Action.Link));
        }

        private static void Edit(Client aClient, String aText, Byte aIdx, Byte aAcceptedLen)
        {
            aClient.Send(new MsgDialog(aText, aAcceptedLen, aIdx, MsgDialog.Action.Edit));
        }

        private static void Pic(Client aClient, UInt16 aFace)
        {
            aClient.Send(new MsgDialog(0, 0, aFace, 0, MsgDialog.Action.Pic));
        }

        private static void Create(Client aClient)
        {
            aClient.Send(new MsgDialog(0xFF, MsgDialog.Action.Create));
        }

        private static bool DropItem(Monster aMonster, Client aClient, Int32 aItemType)
        {
            return aMonster.DropItem(aItemType, aClient != null ? aClient.Player.UniqId : 0);
        }

        private static bool MoveNpc(Client aCleint, Int32 aId, UInt32 aMapId, UInt16 aX, UInt16 aY)
        {
            throw new NotImplementedException();
        }

        private static int Rand(Client aClient, int aValue)
        {
            return MyMath.Generate(0, aValue);
        }

        private static Byte RandomAction(Client aClient, Byte aFirstIdx, Byte aLastIdx)
        {
            return (Byte)MyMath.Generate(Math.Min(aFirstIdx, aLastIdx), Math.Max(aFirstIdx, aLastIdx));
        }

        private static bool CreateClan(Client aClient, Byte aLevel, UInt32 aMoney, UInt32 aMoneyLeave)
        {
            Player player = aClient.Player;
            if (player.Syndicate != null)
                return false;

            // check clan name
            if (String.IsNullOrWhiteSpace(aClient.TaskData))
                return false;

            String name = aClient.TaskData;
            if (name.Length < 2 || name.Length >= 16) // TODO MAX_NAME_SIZE
                return false;

            if (name.IsValidName())
            {
                player.SendSysMsg(StrRes.STR_INVALID_GUILD_NAME);
                return false;
            }

            if (player.Level < aLevel)
            {
                player.SendSysMsg(StrRes.STR_NOT_ENOUGH_LEV, aLevel);
                return false;
            }

            if (player.Money < aMoney)
            {
                player.SendSysMsg(StrRes.STR_NOT_ENOUGH_MONEY, aMoney);
                return false;
            }

            Syndicate syn = null;
            if (!Syndicate.Create(player, name, aMoneyLeave, out syn))
                return false;

            SpendMoney(aClient, aMoney);

            Msg msg = new MsgTalk(
                "SYSTEM", "ALLUSERS",
                String.Format(StrRes.STR_SYN_CREATE, player.Name, name),
                Channel.GM, Color.White);
            World.BroadcastMsg(msg);

            Syndicate.SynchroInfo(player, true);

            return true;
        }

        private static bool DestroyClan(Client aClient)
        {
            Player player = aClient.Player;
            if (player.Syndicate == null)
                return false;

            if (player.UniqId != player.Syndicate.LeaderUID)
            {
                player.SendSysMsg(StrRes.STR_NOT_AUTHORIZED);
                return false;
            }

            return player.Syndicate.Destroy();
        }

        private static bool DonateToClan(Client aClient)
        {
            Player player = aClient.Player;
            if (player.Syndicate == null)
                return false;

            UInt32 money = 0;
            if (!UInt32.TryParse(aClient.TaskData, out money))
                return false;

            return player.Syndicate.Donate(player, money);
        }

        private static bool AddClanEnemy(Client aClient)
        {
            if (String.IsNullOrWhiteSpace(aClient.TaskData))
                return false;

            Player player = aClient.Player;
            if (player.Syndicate == null)
                return false;

            if (player.UniqId != player.Syndicate.LeaderUID)
            {
                player.SendSysMsg(StrRes.STR_NOT_AUTHORIZED);
                return false;
            }

            // TODO QuerySynByName instead of LINQ query
            Syndicate[] syndicates = World.AllSyndicates.Values.Where(s => s.Name == aClient.TaskData).ToArray();
            if (syndicates.Length != 1 || syndicates[0].Id == player.Syndicate.Id)
                return false;

            Syndicate syn = player.Syndicate;
            Syndicate targetSyn = syndicates[0].GetMasterSyn();

            foreach (UInt16 enemyId in syn.Enemies)
            {
                if (enemyId != 0 && enemyId == targetSyn.Id)
                    return false;
            }

            var enemies = syn.Enemies;
            for (int i = 0; i < enemies.Count; ++i)
            {
                UInt16 enemyId = enemies[i];
                if (enemyId == 0 || !World.AllSyndicates.ContainsKey(enemyId))
                {
                    enemies[i] = targetSyn.Id;
                    syn.Enemies = enemies;
                    break;
                }
            }

            var msg = new MsgSyndicate(targetSyn.Id, MsgSyndicate.Action.SetAntagonize);
            World.BroadcastSynMsg(syn, msg);

            return true;
        }

        private static bool RemoveClanEnemy(Client aClient)
        {
            if (String.IsNullOrWhiteSpace(aClient.TaskData))
                return false;

            Player player = aClient.Player;
            if (player.Syndicate == null)
                return false;

            if (player.UniqId != player.Syndicate.LeaderUID)
            {
                player.SendSysMsg(StrRes.STR_NOT_AUTHORIZED);
                return false;
            }

            // TODO QuerySynByName instead of LINQ query
            Syndicate[] syndicates = World.AllSyndicates.Values.Where(s => s.Name == aClient.TaskData).ToArray();
            if (syndicates.Length != 1 || syndicates[0].Id == player.Syndicate.Id)
                return false;

            Syndicate syn = player.Syndicate;
            Syndicate targetSyn = syndicates[0].GetMasterSyn();

            var enemies = syn.Enemies;
            for (int i = 0; i < enemies.Count; ++i)
            {
                UInt16 enemyId = enemies[i];
                if (enemyId == targetSyn.Id)
                {
                    enemies[i] = 0;
                    syn.Enemies = enemies;
                    break;
                }
            }

            var msg = new MsgSyndicate(targetSyn.Id, MsgSyndicate.Action.ClearAntagonize);
            World.BroadcastSynMsg(syn, msg);

            return true;
        }

        private static bool AddClanAlly(Client aClient)
        {
            Player player = aClient.Player;
            if (player.Syndicate == null)
                return false;

            if (player.UniqId != player.Syndicate.LeaderUID)
            {
                player.SendSysMsg(StrRes.STR_NOT_AUTHORIZED);
                return false;
            }

            // target syn
            Team team = player.Team;
            if (team == null)
                return false;

            Player target = null;
            foreach (var member in team.Members)
            {
                if (member != null && member.UniqId != player.UniqId)
                {
                    target = member;
                    break;
                }
            }

            if (target == null)
                return false;

            Syndicate syn = player.Syndicate;
            Syndicate targetSyn = target.Syndicate;
            if (targetSyn == null)
                return false;
            if (target.UniqId != targetSyn.LeaderUID) // TODO GetSynRankShow() != RANK_LEADER everywhere...
                return false;

            targetSyn = targetSyn.GetMasterSyn();

            int idx1 = -1, idx2 = -1;

            var allies = syn.Allies;
            for (int i = 0; i < allies.Count; ++i)
            {
                UInt16 allyId = allies[i];
                if (allyId == 0 || !World.AllSyndicates.ContainsKey(allyId) || allyId == targetSyn.Id)
                {
                    idx1 = i;
                    break;
                }
            }

            if (idx1 == -1)
            {
                player.SendSysMsg(StrRes.STR_ALLY_FULL);
                return false;
            }

            allies = targetSyn.Allies;
            for (int i = 0; i < allies.Count; ++i)
            {
                UInt16 allyId = allies[i];
                if (allyId == 0 || !World.AllSyndicates.ContainsKey(allyId) || allyId == syn.Id)
                {
                    idx2 = i;
                    break;
                }
            }

            if (idx2 == -1)
            {
                player.SendSysMsg(StrRes.STR_HIS_ALLY_FULL);
                return false;
            }

            allies = syn.Allies;
            allies[idx1] = targetSyn.Id;
            syn.Allies = allies;

            allies = targetSyn.Allies;
            allies[idx2] = syn.Id;
            targetSyn.Allies = allies;

            Msg msg = null;

            msg = new MsgSyndicate(targetSyn.Id, MsgSyndicate.Action.SetAlly);
            World.BroadcastSynMsg(syn, msg);
            msg = new MsgSyndicate(syn.Id, MsgSyndicate.Action.SetAlly);
            World.BroadcastSynMsg(targetSyn, msg);

            return true;
        }

        private static bool RemoveClanAlly(Client aClient)
        {
            if (String.IsNullOrWhiteSpace(aClient.TaskData))
                return false;

            Player player = aClient.Player;
            if (player.Syndicate == null)
                return false;

            if (player.UniqId != player.Syndicate.LeaderUID)
            {
                player.SendSysMsg(StrRes.STR_NOT_AUTHORIZED);
                return false;
            }

            // TODO QuerySynByName instead of LINQ query
            Syndicate[] syndicates = World.AllSyndicates.Values.Where(s => s.Name == aClient.TaskData).ToArray();
            if (syndicates.Length != 1 || syndicates[0].Id == player.Syndicate.Id)
                return false;

            Syndicate syn = player.Syndicate;
            Syndicate targetSyn = syndicates[0].GetMasterSyn();

            var allies = syn.Allies;
            for (int i = 0; i < allies.Count; ++i)
            {
                UInt16 allyId = allies[i];
                if (allyId == targetSyn.Id)
                {
                    allies[i] = 0;
                    syn.Allies = allies;
                    break;
                }
            }

            allies = targetSyn.Allies;
            for (int i = 0; i < allies.Count; ++i)
            {
                UInt16 allyId = allies[i];
                if (allyId == syn.Id)
                {
                    allies[i] = 0;
                    targetSyn.Allies = allies;
                    break;
                }
            }

            Msg msg = null;

            msg = new MsgSyndicate(targetSyn.Id, MsgSyndicate.Action.ClearAlly);
            World.BroadcastSynMsg(syn, msg);
            msg = new MsgSyndicate(syn.Id, MsgSyndicate.Action.ClearAlly);
            World.BroadcastSynMsg(targetSyn, msg);

            return true;
        }

        private static bool DemiseFromClan(Client aClient)
        {
            //if (!pszAccept)
            //    return false;

            //// ¼ì²é°ïÅÉ
            //OBJID idSyn = pUser->GetSynID();
            //if (idSyn == ID_NONE)
            //    return false;
            //CSynPtr pSyn = SynManager()->QuerySyndicate(idSyn);
            //if (!pSyn)
            //    return false;

            //// Ö»ÔÊÐí¾üÍÅ³¤ìøÈÃ£¬·ÖÍÅ³¤ºÍ·Ö¶Ó³¤¶¼²»ÔÊÐí
            //if (pSyn->GetInt(SYNDATA_FEALTY) != ID_NONE)
            //    return false;

            //// ¼ì²éÈ¨ÏÞ
            //if (pUser->GetSynRank() != RANK_LEADER)
            //    return false;

            //// target syn
            //CUser* pTarget = UserManager()->GetUser(pszAccept);
            //if (!pTarget)
            //{
            //    pUser->SendSysMsg(STR_NOT_HERE);
            //    return false;
            //}
            //if (pTarget->GetID() == pUser->GetID() || pTarget->GetSynID() != pUser->GetSynID())
            //    return false;

            //int nNeedLevel = atol(szParam);
            //if (nNeedLevel && pTarget->GetLev() < nNeedLevel)
            //{
            //    pUser->SendSysMsg(STR_LEVEL_NOT_ENOUGH);
            //    return false;
            //}
            //OBJID idTargetSyn = pTarget->GetSynID();
            //if (idTargetSyn == ID_NONE || idTargetSyn != idSyn)
            //    return false;

            //pSyn->QueryModify()->Demise(pUser->GetID(), pUser->QuerySynAttr()->GetSynMemberLevel(), pTarget->GetID(), pTarget->GetName(), pTarget->QuerySynAttr()->GetSynMemberLevel(), pSyn->GetID());
            //return true;
            return false; // to remove
        }

        private static bool SetClanAssistant(Client aClient)
        {
            if (String.IsNullOrWhiteSpace(aClient.TaskData))
                return false;

            //// ¼ì²é°ïÅÉ
            //OBJID idSyn = pUser->GetSynID();
            //if (idSyn == ID_NONE)
            //    return false;
            //CSynPtr pSyn = SynManager()->QuerySyndicate(idSyn);
            //if (!pSyn)
            //    return false;
            //// ¼ì²éÈ¨ÏÞ
            //int nRank = pUser->GetSynRank();
            //if (nRank != RANK_LEADER)
            //    return false;

            //// check amount, temporary code.
            //if (pUser->QuerySynAttr()->GetAssistantCount() >= MAX_ASSISTANTSIZE)
            //{
            //    pUser->SendSysMsg(STR_SYN_PLACE_FULL);
            //    return false;
            //}

            //// target syn
            //CUser* pTarget = UserManager()->GetUser(pszAccept);
            //if (!pTarget)
            //{
            //    pUser->SendSysMsg(STR_NOT_HERE);
            //    return false;
            //}
            //if (pUser->GetID() == pTarget->GetID())
            //    return false;
            //OBJID idTargetSyn = pTarget->GetSynID();
            //if (idTargetSyn == ID_NONE || idTargetSyn == idSyn)
            //    return false;

            //pTarget->QuerySynAttr()->SetRank(RANK_SUBLEADER);
            //return true;

            return false; // to remove
        }

        private static bool RemoveClanAssistant(Client aClient)
        {
            if (String.IsNullOrWhiteSpace(aClient.TaskData))
                return false;

            //// ¼ì²é°ïÅÉ
            //OBJID idSyn = pUser->GetSynID();
            //if (idSyn == ID_NONE)
            //    return false;
            //CSynPtr pSyn = SynManager()->QuerySyndicate(idSyn);
            //if (!pSyn)
            //    return false;
            //// ¼ì²éÈ¨ÏÞ
            //int nRank = pUser->GetSynRank();
            //if (nRank != RANK_LEADER)
            //    return false;

            //// target syn
            //CUser* pTarget = UserManager()->GetUser(pszAccept);
            //if (!pTarget)
            //{
            //    pUser->SendSysMsg(STR_NOT_HERE);
            //    return false;
            //}
            //if (pUser->GetID() == pTarget->GetID())
            //    return false;
            //OBJID idTargetSyn = pTarget->GetSynID();
            //if (idTargetSyn == ID_NONE || idTargetSyn != idSyn)
            //    return false;

            //pTarget->QuerySynAttr()->SetRank(RANK_NEWBIE);
            //return true;

            return false; // to remove
        }

        private static bool CreateSubClan(Client aClient, Byte aLevel, UInt32 aMoney, UInt32 aMoneyLeave)
        {
            // check syndicate name
            if (String.IsNullOrWhiteSpace(aClient.TaskData))
                return false;

            Player player = aClient.Player;
            if (player.Syndicate == null)
                return false;

            if (player.UniqId != player.Syndicate.LeaderUID)
            {
                player.SendSysMsg(StrRes.STR_NOT_AUTHORIZED);
                return false;
            }

            Syndicate syn = player.Syndicate;
            if (!syn.IsMasterSyn())
                return false;

            // target syn
            Team team = player.Team;
            if (team == null)
                return false;

            Player target = null;
            foreach (var member in team.Members)
            {
                if (member != null && member.UniqId != player.UniqId)
                {
                    target = member;
                    break;
                }
            }

            if (target == null)
                return false;

            Syndicate targetSyn = target.Syndicate;
            if (targetSyn == null)
                return false;
            if (targetSyn.Id != syn.Id)
                return false;


            String name = aClient.TaskData;
            if (name.Length < 2 || name.Length >= 7) // TODO SUBSYN_NAMESIZE
            {
                player.SendSysMsg(StrRes.STR_INVALID_GUILD_NAME);
                return false;
            }

            if (name.IsValidName())
            {
                player.SendSysMsg(StrRes.STR_INVALID_GUILD_NAME);
                return false;
            }

            if (target.Level < aLevel)
            {
                player.SendSysMsg(StrRes.STR_NOT_ENOUGH_LEV, aLevel);
                return false;
            }

            if (player.Money < aMoney)
            {
                player.SendSysMsg(StrRes.STR_NOT_ENOUGH_MONEY, aMoney);
                return false;
            }

            Syndicate subsyn = null;
            if (!syn.CreateSubSyn(target, name, aMoneyLeave, out subsyn))
                return false;

            SpendMoney(aClient, aMoney);
            Syndicate.SynchroInfo(target, true);

            return true;
        }

        private static bool ChangeSubClanLeader(Client aClient, Byte aReqLevel)
        {
            Player player = aClient.Player;
            if (player.Syndicate == null)
                return false;

            if (player.UniqId != player.Syndicate.LeaderUID)
            {
                player.SendSysMsg(StrRes.STR_NOT_AUTHORIZED);
                return false;
            }

            //// check syndicate name
            //OBJID idNewLeader = ID_NONE;
            //MSGBUF	szSubSynName;
            //if(!pszAccept || sscanf(pszAccept, "%u %s", &idNewLeader, szSubSynName) != 2)
            //{
            //    LOGERROR("ACTION %u: ´íÎóµÄacceptÊýÁ¿", pAction->GetID());
            //    return false;
            //}

            //// target
            //CUser* pTarget = UserManager()->GetUser(idNewLeader);
            //if(!pTarget || pTarget->GetSynID() != pSyn->GetID() || pTarget->GetNobilityRank() < nNobilityRank)
            //    return false;

            //if(pTarget->QuerySynAttr()->ChangeLeader(szSubSynName, nNeedLevel))
            //    return true;
            //else
            //    return false;

            return false;
        }
    }
}
