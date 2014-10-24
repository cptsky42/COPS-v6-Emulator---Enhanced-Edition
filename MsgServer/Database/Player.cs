// * Created by Jean-Philippe Boivin
// * Copyright © 2010-2011
// * Logik. Project

using System;
using System.IO;
using System.Collections.Generic;
using COServer.Network;
using COServer.Entities;
using AMS.Profile;

namespace COServer
{
    public partial class Database
    {
        public static Boolean Ban(String Name)
        {
            try
            {
                if (!Server.SendToAuth(MsgActionExt.Create(
                    (Int32)Client.Flag.Banned,
                    Name,
                    "@UNKNOW_ACC@",
                    Server.Name,
                    MsgActionExt.Action.SetChrFlags)))
                    return false;
                return true;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return false; }
        }

        public static Boolean Jail(String Name)
        {
            try
            {
                if (!File.Exists(Program.RootPath + "\\Characters\\" + Name + ".chr"))
                    return false;

                Xml AMSXml = new Xml(Program.RootPath + "\\Characters\\" + Name + ".chr");
                AMSXml.RootName = "Character";
                using (AMSXml.Buffer())
                {
                    AMSXml.SetValue("Informations", "Jail", AMSXml.GetValue("Informations", "Jail", 0) + 1);
                    if (World.AllMaps.ContainsKey(6001))
                    {
                        AMSXml.SetValue("Informations", "Map", 6001);
                        AMSXml.SetValue("Informations", "X", 28);
                        AMSXml.SetValue("Informations", "Y", 75);
                    }
                }
                AMSXml = null;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
            return true;
        }

        public static Boolean SynKickOut(String Name)
        {
            try
            {
                if (!File.Exists(Program.RootPath + "\\Characters\\" + Name + ".chr"))
                    return false;

                Xml AMSXml = new Xml(Program.RootPath + "\\Characters\\" + Name + ".chr");
                AMSXml.RootName = "Character";
                using (AMSXml.Buffer())
                {
                    AMSXml.SetValue("Informations", "SynUID", 0);
                    AMSXml.SetValue("Informations", "SynRank", 0);
                    AMSXml.SetValue("Informations", "SynDonation", 0);
                }
                AMSXml = null;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
            return true;
        }

        public static Boolean Divorce(String Name)
        {
            try
            {
                if (!File.Exists(Program.RootPath + "\\Characters\\" + Name + ".chr"))
                    return false;

                Xml AMSXml = new Xml(Program.RootPath + "\\Characters\\" + Name + ".chr");
                AMSXml.RootName = "Character";
                using (AMSXml.Buffer()) { AMSXml.SetValue("Informations", "Spouse", "Non"); }
                AMSXml = null;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
            return true;
        }

        public static Boolean GetPlayerInfo(ref Client Client, String Name)
        {
            try
            {
                if (!File.Exists(Program.RootPath + "\\Characters\\" + Name + ".chr"))
                    return false;

                Xml AMSXml = new Xml(Program.RootPath + "\\Characters\\" + Name + ".chr");
                AMSXml.RootName = "Character";

                try
                {
                    Client.User = new Player(AMSXml.GetValue("Informations", "UniqId", -1), Client);
                    Player Player = Client.User;
                    String[] Parts = null;

                    Player.Name = AMSXml.GetValue("Informations", "Name", "NULL");
                    if (Player.Name == "NULL")
                    {
                        AMSXml = null;
                        return false;
                    }

                    Player.Spouse = AMSXml.GetValue("Informations", "Spouse", "Non");
                    Player.Profession = (Byte)AMSXml.GetValue("Informations", "Profession", 0);
                    Player.Level = (Byte)AMSXml.GetValue("Informations", "Level", 0);
                    Player.FirstProfession = (Byte)AMSXml.GetValue("Informations", "FirstProfession", 0);
                    Player.FirstLevel = (Byte)AMSXml.GetValue("Informations", "FirstLevel", 0);
                    Player.SecondProfession = (Byte)AMSXml.GetValue("Informations", "SecondProfession", 0);
                    Player.SecondLevel = (Byte)AMSXml.GetValue("Informations", "SecondLevel", 0);
                    Player.Exp = UInt64.Parse(AMSXml.GetValue("Informations", "Exp", "0"));
                    //AMSXml.GetValue("Informations", "LastExp", 0);
                    Player.Strength = (UInt16)AMSXml.GetValue("Informations", "Strength", 0);
                    Player.Agility = (UInt16)AMSXml.GetValue("Informations", "Agility", 0);
                    Player.Vitality = (UInt16)AMSXml.GetValue("Informations", "Vitality", 0);
                    Player.Spirit = (UInt16)AMSXml.GetValue("Informations", "Spirit", 0);
                    Player.AddPoints = (UInt16)AMSXml.GetValue("Informations", "AddPoints", 0);
                    Player.Look = UInt32.Parse(AMSXml.GetValue("Informations", "Look", "0"));
                    Player.Hair = (Int16)AMSXml.GetValue("Informations", "Hair", 0);
                    Player.Money = AMSXml.GetValue("Informations", "Money", 0);
                    Player.CPs = AMSXml.GetValue("Informations", "CPs", 0);
                    Player.VPs = AMSXml.GetValue("Informations", "VPs", 0);
                    Player.PkPoints = (Int16)AMSXml.GetValue("Informations", "PkPoints", 0);
                    Player.CurHP = (UInt16)AMSXml.GetValue("Informations", "CurHP", 0);
                    Player.CurMP = (UInt16)AMSXml.GetValue("Informations", "CurMP", 0);
                    Player.Metempsychosis = (Byte)AMSXml.GetValue("Informations", "Metempsychosis", 0);
                    Player.Map = (Int16)AMSXml.GetValue("Informations", "Map", 1002);
                    Player.X = (UInt16)AMSXml.GetValue("Informations", "X", 400);
                    Player.Y = (UInt16)AMSXml.GetValue("Informations", "Y", 400);
                    Player.WHMoney = AMSXml.GetValue("Informations", "WHMoney", 0);
                    Player.WHPIN = AMSXml.GetValue("Informations", "WHPIN", 0);
                    Player.KO = AMSXml.GetValue("Informations", "KO", 0);
                    Player.TimeAdd = AMSXml.GetValue("Informations", "TimeAdd", 0);

                    Player.DblExpEndTime = Environment.TickCount + (AMSXml.GetValue("Informations", "DblExpTime", 0) * 1000);
                    Player.LuckyTime = AMSXml.GetValue("Informations", "LuckyTime", 0);
                    Player.CurseEndTime = Environment.TickCount + (AMSXml.GetValue("Informations", "CurseTime", 0) * 1000);
                    Player.BlessEndTime = Int64.Parse(AMSXml.GetValue("Informations", "BlessTime", "0"));
                    Player.TrainingTicks = Int64.Parse(AMSXml.GetValue("Informations", "TrainingTime", "0"));
                    Player.MaxTrainingTime = AMSXml.GetValue("Informations", "MaxTrainingTime", 0);

                    Player.PremiumEndTime = DateTime.FromBinary(Int64.Parse(AMSXml.GetValue("Informations", "Premium", "0")));
                    if (Player.PremiumEndTime.CompareTo(DateTime.UtcNow) > 0)
                    {
                        if (Client.AccLvl < 1)
                            Client.AccLvl = 1;
                    }

                    Player.ToS = AMSXml.GetValue("Informations", "ToS", false);
                    Player.JailC = AMSXml.GetValue("Informations", "Jail", 0);

                    Int16 SynUID = (Int16)AMSXml.GetValue("Informations", "SynUID", 0);
                    World.AllSyndicates.TryGetValue(SynUID, out Player.Syndicate);

                    if (!World.AllMaps.ContainsKey(Player.Map) || Player.Map == 700)
                    {
                        Player.Map = 1002;
                        Player.X = 400;
                        Player.Y = 400;
                    }

                    String Friends = AMSXml.GetValue("Informations", "Friends", "");
                    Parts = Friends.Split(',');
                    foreach (String Friend in Parts)
                    {
                        String[] Info = Friend.Split(':');
                        if (Info.Length < 2)
                            continue;

                        Int32 UniqId = 0;
                        if (!Int32.TryParse(Info[0], out UniqId))
                            continue;

                        if (!Player.Friends.ContainsKey(UniqId))
                            Player.Friends.Add(UniqId, Info[1]);
                    }
                    Parts = null;

                    String Enemies = AMSXml.GetValue("Informations", "Enemies", "");
                    Parts = Enemies.Split(',');
                    foreach (String Enemy in Parts)
                    {
                        String[] Info = Enemy.Split(':');
                        if (Info.Length < 2)
                            continue;

                        Int32 UniqId = 0;
                        if (!Int32.TryParse(Info[0], out UniqId))
                            continue;

                        if (!Player.Enemies.ContainsKey(UniqId))
                            Player.Enemies.Add(UniqId, Info[1]);
                    }
                    Parts = null;

                    if (Player.Metempsychosis > 0)
                        Player.AutoAllot = false;

                    if (Player.Look > 9999999)
                        Player.DelTransform();

                    if (!World.NobilityRank.TryGetValue(Player.UniqId, out Player.Nobility))
                        Player.Nobility = new Nobility.Info(Player);

                    //MyMath
                    MyMath.GetHitPoints(Player, true);
                    MyMath.GetMagicPoints(Player, true);
                    MyMath.GetPotency(Player, true);

                    AMSXml = null;
                    return true;
                }
                catch (Exception Exc) { Program.WriteLine(Exc); AMSXml = null; return false; }
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return false; }
        }

        public static Boolean Delete(Player Player)
        {
            try
            {
                if (!Server.SendToAuth(MsgActionExt.Create(0, "@INVALID_CHAR@", Player.Owner.Account, Server.Name, MsgActionExt.Action.SetCharacter)))
                    return false;

                Server.SendToAuth(MsgActionExt.Create(0, "", Player.Owner.Account, Server.Name, MsgActionExt.Action.SetAccLvl));

                WeaponSkill[] WeaponSkills = new WeaponSkill[Player.WeaponSkills.Count];
                Player.WeaponSkills.Values.CopyTo(WeaponSkills, 0);
                for (Int32 i = 0; i < WeaponSkills.Length; i++)
                    Player.DelWeaponSkill(WeaponSkills[i], true);

                Magic[] Magics = new Magic[Player.Magics.Count];
                Player.Magics.Values.CopyTo(Magics, 0);
                for (Int32 i = 0; i < Magics.Length; i++)
                    Player.DelMagic(Magics[i], true);

                Item[] Items = new Item[Player.Items.Count];
                Player.Items.Values.CopyTo(Items, 0);
                for (Int32 i = 0; i < Items.Length; i++)
                    Player.DelItem(Items[i], true);

                if (World.NobilityRank.ContainsKey(Player.UniqId))
                {
                    World.NobilityRank.Remove(Player.UniqId);
                    Nobility.Rank.ResetPosition();
                }

                //Friends


                if (Player.Syndicate != null)
                    Player.Syndicate.DelMember(Player.Syndicate.GetMemberInfo(Player.UniqId), false);

                String Account = Player.Owner.Account;
                String Name = Player.Name;
                String IPAddress = Player.Owner.Socket.IpAddress;

                Player.Disconnect();
                File.Delete(Program.RootPath + "\\Characters\\" + Name + ".chr");

                Program.Log("Deletion of " + Name + ", with " + Account + " (" + IPAddress + ").");
                return true;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return false; }
        }

        public static void Save(Player Player)
        {
            Xml AMSXml = null;
            try
            {
                if (!File.Exists(Program.RootPath + "\\Characters\\" + Player.Name + ".chr"))
                    return;

                AMSXml = new Xml(Program.RootPath + "\\Characters\\" + Player.Name + ".chr");
                AMSXml.RootName = "Character";
                using (AMSXml.Buffer())
                {
                    AMSXml.SetValue("Informations", "Spouse", Player.Spouse);
                    AMSXml.SetValue("Informations", "Profession", Player.Profession);
                    AMSXml.SetValue("Informations", "Level", Player.Level);
                    AMSXml.SetValue("Informations", "FirstProfession", Player.FirstProfession);
                    AMSXml.SetValue("Informations", "FirstLevel", Player.FirstLevel);
                    AMSXml.SetValue("Informations", "SecondProfession", Player.SecondProfession);
                    AMSXml.SetValue("Informations", "SecondLevel", Player.SecondLevel);
                    AMSXml.SetValue("Informations", "Exp", Player.Exp);
                    AMSXml.SetValue("Informations", "Strength", Player.Strength);
                    AMSXml.SetValue("Informations", "Agility", Player.Agility);
                    AMSXml.SetValue("Informations", "Vitality", Player.Vitality);
                    AMSXml.SetValue("Informations", "Spirit", Player.Spirit);
                    AMSXml.SetValue("Informations", "AddPoints", Player.AddPoints);
                    AMSXml.SetValue("Informations", "Look", Player.Look);
                    AMSXml.SetValue("Informations", "Hair", Player.Hair);
                    AMSXml.SetValue("Informations", "Money", Player.Money);
                    AMSXml.SetValue("Informations", "CPs", Player.CPs);
                    AMSXml.SetValue("Informations", "VPs", Player.VPs);
                    AMSXml.SetValue("Informations", "PkPoints", Player.PkPoints);
                    AMSXml.SetValue("Informations", "CurHP", Player.CurHP);
                    AMSXml.SetValue("Informations", "CurMP", Player.CurMP);
                    AMSXml.SetValue("Informations", "Metempsychosis", Player.Metempsychosis);

                    Map Map = null;
                    if (World.AllMaps.TryGetValue(Player.Map, out Map))
                    {
                        if (!Map.IsRecord_Disable())
                        {
                            AMSXml.SetValue("Informations", "Map", Player.Map);
                            AMSXml.SetValue("Informations", "X", Player.X);
                            AMSXml.SetValue("Informations", "Y", Player.Y);
                        }
                        else
                        {
                            if (World.AllMaps.ContainsKey(Player.PrevMap))
                            {
                                Map = World.AllMaps[Player.PrevMap];
                                AMSXml.SetValue("Informations", "Map", Player.PrevMap);
                                AMSXml.SetValue("Informations", "X", Map.PortalX);
                                AMSXml.SetValue("Informations", "Y", Map.PortalY);
                            }
                            else
                            {
                                AMSXml.SetValue("Informations", "Map", 1002);
                                AMSXml.SetValue("Informations", "X", 400);
                                AMSXml.SetValue("Informations", "Y", 400);
                            }
                        }

                        if (Player.CurHP == 0)
                        {
                            Map = World.AllMaps[Player.Map];
                            AMSXml.SetValue("Informations", "CurHP", 1);
                            AMSXml.SetValue("Informations", "Map", Map.RebornMap);

                            Map = World.AllMaps[Map.RebornMap];
                            AMSXml.SetValue("Informations", "X", Map.PortalX);
                            AMSXml.SetValue("Informations", "Y", Map.PortalY);
                        }
                    }
                    else
                    {
                        AMSXml.SetValue("Informations", "Map", 1002);
                        AMSXml.SetValue("Informations", "X", 400);
                        AMSXml.SetValue("Informations", "Y", 400);

                        if (Player.CurHP == 0)
                            AMSXml.SetValue("Informations", "CurHP", 1);
                    }

                    AMSXml.SetValue("Informations", "WHMoney", Player.WHMoney);
                    AMSXml.SetValue("Informations", "WHPIN", Player.WHPIN);
                    AMSXml.SetValue("Informations", "KO", Player.KO);
                    AMSXml.SetValue("Informations", "TimeAdd", Player.TimeAdd);
                    if (Player.Nobility != null)
                        AMSXml.SetValue("Informations", "NobilityDonation", Player.Nobility.Donation);
                    else
                        AMSXml.SetValue("Informations", "NobilityDonation", 0);
                    AMSXml.SetValue("Informations", "KO", 0);
                    if (Player.DblExpEndTime == 0)
                        AMSXml.SetValue("Informations", "DblExpTime", 0);
                    else
                        AMSXml.SetValue("Informations", "DblExpTime", (Int32)((Player.DblExpEndTime - Environment.TickCount) / 1000));
                    AMSXml.SetValue("Informations", "LuckyTime", Player.LuckyTime);
                    if (Player.CurseEndTime == 0)
                        AMSXml.SetValue("Informations", "CurseTime", 0);
                    else
                        AMSXml.SetValue("Informations", "CurseTime", (Int32)((Player.CurseEndTime - Environment.TickCount) / 1000));
                    AMSXml.SetValue("Informations", "BlessTime", Player.BlessEndTime);

                    AMSXml.SetValue("Informations", "TrainingTime", Player.TrainingTicks);
                    AMSXml.SetValue("Informations", "MaxTrainingTime", Player.MaxTrainingTime);

                    String Friends = "";
                    foreach (KeyValuePair<Int32, String> KV in Player.Friends)
                        Friends += KV.Key.ToString() + ":" + KV.Value + ",";
                    if (Friends != null && Friends != "")
                        Friends = Friends.Substring(0, Friends.Length - 1);
                    AMSXml.SetValue("Informations", "Friends", Friends);

                    String Enemies = "";
                    foreach (KeyValuePair<Int32, String> KV in Player.Enemies)
                        Enemies += KV.Key.ToString() + ":" + KV.Value + ",";
                    if (Enemies != null && Enemies != "")
                        Enemies = Enemies.Substring(0, Enemies.Length - 1);
                    AMSXml.SetValue("Informations", "Enemies", Enemies);

                    Syndicate.Member SynMember = null;
                    if (Player.Syndicate != null)
                    {
                        SynMember = Player.Syndicate.GetMemberInfo(Player.UniqId);
                        if (SynMember != null)
                        {
                            AMSXml.SetValue("Informations", "SynUID", Player.Syndicate.UniqId);
                            AMSXml.SetValue("Informations", "SynRank", SynMember.Rank);
                            AMSXml.SetValue("Informations", "SynDonation", SynMember.Donation);
                        }
                        else
                        {
                            AMSXml.SetValue("Informations", "SynUID", 0);
                            AMSXml.SetValue("Informations", "SynRank", 0);
                            AMSXml.SetValue("Informations", "SynDonation", 0);
                        }
                    }

                    AMSXml.SetValue("Informations", "ToS", Player.ToS);
                }
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
            AMSXml = null;
        }

        public static Boolean Register(String Account, String Name, Int32 Look, Byte Profession)
        {
            try
            {
                if (File.Exists(Program.RootPath + "\\Characters\\" + Name + ".chr"))
                    return false;

                Int16[] Points = MyMath.GetLevelStats(1, Profession);
                if (Points == null)
                    return false;

                if (!Server.AuthSocket.Connected)
                    return false;

                if (!Server.SendToAuth(MsgActionExt.Create(0, Name, Account, Server.Name, MsgActionExt.Action.SetCharacter)))
                    return false;

                Server.SendToAuth(MsgActionExt.Create(0, "", Account, Server.Name, MsgActionExt.Action.SetAccLvl));
                Server.SendToAuth(MsgActionExt.Create(0, "", Account, Server.Name, MsgActionExt.Action.SetAccFlags));

                Xml AMSXml = new Xml(Program.RootPath + "\\Characters\\" + Name + ".chr");
                AMSXml.RootName = "Character";
                using (AMSXml.Buffer())
                {
                    Int32 UniqId = World.LastPlayerUID;
                    World.LastPlayerUID++;

                    AMSXml.SetValue("Informations", "UniqId", UniqId);
                    AMSXml.SetValue("Informations", "Name", Name);
                    AMSXml.SetValue("Informations", "Spouse", "Non");
                    AMSXml.SetValue("Informations", "Profession", Profession);
                    AMSXml.SetValue("Informations", "Level", 1);
                    AMSXml.SetValue("Informations", "FirstProfession", 0);
                    AMSXml.SetValue("Informations", "FirstLevel", 0);
                    AMSXml.SetValue("Informations", "SecondProfession", 0);
                    AMSXml.SetValue("Informations", "SecondLevel", 0);
                    AMSXml.SetValue("Informations", "Exp", 0);
                    AMSXml.SetValue("Informations", "Strength", Points[0]);
                    AMSXml.SetValue("Informations", "Agility", Points[1]);
                    AMSXml.SetValue("Informations", "Vitality", Points[2]);
                    AMSXml.SetValue("Informations", "Spirit", Points[3]);
                    AMSXml.SetValue("Informations", "AddPoints", 0);
                    AMSXml.SetValue("Informations", "Look", Look);
                    AMSXml.SetValue("Informations", "Hair", 310);
                    AMSXml.SetValue("Informations", "Money", 1000);
                    AMSXml.SetValue("Informations", "CPs", 0);
                    AMSXml.SetValue("Informations", "VPs", 0);
                    AMSXml.SetValue("Informations", "PkPoints", 0);
                    AMSXml.SetValue("Informations", "CurHP", MyMath.GetHitPoints((UInt16)Points[0], (UInt16)Points[1], (UInt16)Points[2], (UInt16)Points[3], Profession));
                    AMSXml.SetValue("Informations", "CurMP", MyMath.GetMagicPoints((UInt16)Points[3], Profession));
                    AMSXml.SetValue("Informations", "Metempsychosis", 0);
                    AMSXml.SetValue("Informations", "Map", 1010);
                    AMSXml.SetValue("Informations", "X", 31);
                    AMSXml.SetValue("Informations", "Y", 83);
                    AMSXml.SetValue("Informations", "WHMoney", 0);
                    AMSXml.SetValue("Informations", "WHPIN", 0);
                    AMSXml.SetValue("Informations", "KO", 0);
                    AMSXml.SetValue("Informations", "TimeAdd", 0);
                    AMSXml.SetValue("Informations", "NobilityDonation", 0);
                    AMSXml.SetValue("Informations", "DblExpTime", 0);
                    AMSXml.SetValue("Informations", "LuckyTime", 0);
                    AMSXml.SetValue("Informations", "CurseTime", 0);
                    AMSXml.SetValue("Informations", "BlessTime", DateTime.UtcNow.AddDays(30).ToBinary());
                    AMSXml.SetValue("Informations", "Friends", "");
                    AMSXml.SetValue("Informations", "Enemies", "");
                    AMSXml.SetValue("Informations", "ToS", "false");
                    AMSXml.SetValue("Informations", "Jail", "0");

                    Item.Create(UniqId, 0, 726100, 0, 0, 0, 0, 0, 3, 0, 1, 1);
                }
                AMSXml = null;
                return true;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return false; }
        }

        public static Int32 GetLastPlayerUID()
        {
            Int32 LastUID = 1000000;

            String[] Characters = Directory.GetFiles(Program.RootPath + "\\Characters", "*.chr");
            foreach (String Character in Characters)
            {
                Xml AMSXml = new Xml(Character);
                AMSXml.RootName = "Character";

                Int32 UID = AMSXml.GetValue("Informations", "UniqId", 1000000);
                AMSXml = null;

                if (UID > LastUID)
                    LastUID = UID;
            }
            LastUID++;
            return LastUID;
        }
    }
}
