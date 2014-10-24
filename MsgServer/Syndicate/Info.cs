// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.Collections.Generic;
using COServer.Entities;
using COServer.Network;
using AMS.Profile;

namespace COServer
{
    public partial class Syndicate
    {
        public class Info
        {
            public readonly Int16 UniqId;
            public readonly String Name;
            public String Announce;
            public Member Leader;
            public Int32 Money;
            public Int16 FealtySynUID;
            public Dictionary<Int32, Member> Members;
            public Int32[] Allies;
            public Int32[] Enemies;
            private Xml AMSXml;

            public Info(Int16 UniqId, String Name, String Announce, Int32 LeaderUID, String LeaderName, Int32 Money, Int16 FealtySynUID, Int32[] Enemies, Int32[] Allies, Xml AMSXml)
            {
                this.UniqId = UniqId;
                this.Name = Name;
                this.Announce = Announce;

                this.Money = Money;
                this.FealtySynUID = FealtySynUID;

                this.Leader = new Member(LeaderUID, LeaderName, 0, 100, 0);
                this.Members = new Dictionary<Int32, Member>();

                this.Allies = Allies;
                this.Enemies = Enemies;

                this.AMSXml = AMSXml;
            }

            ~Info()
            {
                Members = null;
                AMSXml = null;
            }

            public Syndicate.Info GetMasterSyn()
            {
                Syndicate.Info MasterSyn = null;
                World.AllSyndicates.TryGetValue(FealtySynUID, out MasterSyn);
                return MasterSyn;
            }

            public Boolean DonateMoney(Player Donator, Int32 Donation)
            {
                if(Donation <= 0)
                   return false;

                if (Donator.Money < Donation)
                {
                    Donator.SendSysMsg(STR.Get("STR_NOT_SO_MUCH_MONEY"));
                    return false;
                }

                if (!IsInSyndicate(Donator.UniqId))
                    return false;

                Member SynMember = GetMemberInfo(Donator.UniqId);

                Donator.Money -= Donation;
                Money += Donation;
                SynMember.Donation += Donation;

                World.SynThread.AddToQueue(this, "Money", Money);

                Donator.Send(MsgUserAttrib.Create(Donator, Donator.Money, MsgUserAttrib.Type.Money));
                World.BroadcastSynMsg(this, MsgTalk.Create("SYSTEM", "ALLUSERS", String.Format(STR.Get("STR_DONATE"), Donator.Name, Donation), MsgTalk.Channel.Syndicate, 0xFFFFFF));
                return true;
            }

            public Boolean IsAnAlly(Int16 SynUID)
            {
                foreach (Int32 AllyUID in Allies)
                {
                    if (AllyUID == SynUID)
                        return true;
                }
                return false;
            }

            public Boolean IsAnEnemy(Int16 SynUID)
            {
                foreach (Int32 EnemyUID in Enemies)
                {
                    if (EnemyUID == SynUID)
                        return true;
                }
                return false;
            }

            public Boolean IsInSyndicate(Int32 UniqId)
            {
                if (Leader.UniqId == UniqId)
                    return true;

                if (Members.ContainsKey(UniqId))
                    return true;

                return false;
            }

            public void AddMember(Player Player)
            {
                Member Member = new Member(Player.UniqId, Player.Name, (Byte)Player.Level, 50, 0);
                lock (Members) { Members.Add(Player.UniqId, Member); }

                Player.Syndicate = this;
                Player.Send(MsgSynAttrInfo.Create(Player.UniqId, Player.Syndicate));
                Player.Send(MsgTalk.Create("SYSTEM", "ALLUSERS", Announce, MsgTalk.Channel.SynAnnounce, 0xFFFFFF));

                foreach (Int32 Enemy in Enemies)
                    Player.Send(MsgSyndicate.Create(Enemy, MsgSyndicate.Action.SetAntagonize));

                foreach (Int32 Ally in Allies)
                    Player.Send(MsgSyndicate.Create(Ally, MsgSyndicate.Action.SetAlly));

                World.BroadcastRoomMsg(Player, MsgPlayer.Create(Player), true);
                World.BroadcastSynMsg(this, MsgTalk.Create("SYSTEM", "ALLUSERS", String.Format(STR.Get("STR_SYN_JOIN"), Player.Name), MsgTalk.Channel.Syndicate, 0xFFFFFF));

                Database.Save(Player);
            }

            public void DelMember(Member Member, Boolean Kicked)
            {
                if (Member.UniqId == Leader.UniqId)
                    return;

                Player Player = null;
                if (World.AllPlayers.TryGetValue(Member.UniqId, out Player))
                {
                    lock (Members) { Members.Remove(Member.UniqId); }

                    Player.Syndicate = null;
                    Database.SynKickOut(Member.Name);

                    Player.Send(MsgSynAttrInfo.Create(Player.UniqId, Player.Syndicate));
                    World.BroadcastRoomMsg(Player, MsgPlayer.Create(Player), true);
                }
                else
                {
                    lock (Members) { Members.Remove(Member.UniqId); }
                    Database.SynKickOut(Member.Name);
                }

                if (Kicked)
                    World.BroadcastSynMsg(this, MsgTalk.Create("SYSTEM", "ALLUSERS", String.Format(STR.Get("STR_KICKOUT"), Member.Name), MsgTalk.Channel.Syndicate, 0xFFFFFF));
            }

            public Member GetMemberInfo(Int32 UniqId)
            {
                if (Leader.UniqId == UniqId)
                    return Leader;

                if (Members.ContainsKey(UniqId))
                    return Members[UniqId];

                return null;
            }

            public Member GetMemberInfo(String Name)
            {
                if (Leader.Name == Name)
                    return Leader;

                foreach (Member Member in Members.Values)
                {
                    if (Member.Name == Name)
                        return Member;
                }

                return null;
            }

            public String[] GetMemberList()
            {
                List<String> Online = new List<String>();
                List<String> Offline = new List<String>();

                if (World.AllPlayers.ContainsKey(Leader.UniqId))
                    Online.Add(Leader.Name + " " + Leader.Level + " 1");
                else
                    Offline.Add(Leader.Name + " " + Leader.Level + " 0");

                foreach (Member Member in Members.Values)
                {
                    if (World.AllPlayers.ContainsKey(Member.UniqId))
                        Online.Add(Member.Name + " " + Member.Level + " 1");
                    else
                        Offline.Add(Member.Name + " " + Member.Level + " 0");
                }

                String[] List = new String[Online.Count + Offline.Count];
                Online.CopyTo(List, 0);
                Offline.CopyTo(List, Online.Count);
                return List;
            }

            public void Save()
            {
                try
                {
                    lock (AMSXml)
                    {
                        using (AMSXml.Buffer())
                        {
                            AMSXml.SetValue("Informations", "Announce", Announce);
                            AMSXml.SetValue("Informations", "LeaderUID", Leader.UniqId);
                            AMSXml.SetValue("Informations", "LeaderName", Leader.Name);
                            AMSXml.SetValue("Informations", "Money", Money);
                            AMSXml.SetValue("Informations", "FealtySynUID", FealtySynUID);
                            AMSXml.SetValue("Informations", "Enemy0", Enemies[0]);
                            AMSXml.SetValue("Informations", "Enemy1", Enemies[1]);
                            AMSXml.SetValue("Informations", "Enemy2", Enemies[2]);
                            AMSXml.SetValue("Informations", "Enemy3", Enemies[3]);
                            AMSXml.SetValue("Informations", "Enemy4", Enemies[4]);
                            AMSXml.SetValue("Informations", "Ally0", Allies[0]);
                            AMSXml.SetValue("Informations", "Ally1", Allies[1]);
                            AMSXml.SetValue("Informations", "Ally2", Allies[2]);
                            AMSXml.SetValue("Informations", "Ally3", Allies[3]);
                            AMSXml.SetValue("Informations", "Ally4", Allies[4]);
                        }
                    }
                }
                catch (Exception Exc) { Program.WriteLine(Exc); }
            }

            public void Save(String Entry, Object Value)
            {
                try
                {
                    lock (AMSXml)
                    {
                        using (AMSXml.Buffer())
                        {
                            AMSXml.SetValue("Informations", Entry, Value);
                        }
                    }
                }
                catch (Exception Exc) { Program.WriteLine(Exc); }
            }

            public static Boolean Create(Player Leader, String Name)
            {
                Int16 UniqId = 1;
                Xml AMSXml = null;
                Syndicate.Info Syn = null;

                try
                {
                    lock (World.AllSyndicates)
                    {
                        UniqId = World.LastSyndicateUID;
                        if (World.AllSyndicates.ContainsKey(UniqId))
                            UniqId++;
                        World.AllSyndicates.Add(UniqId, null);

                        World.LastSyndicateUID = (Int16)(UniqId + 1);

                        AMSXml = new Xml(Program.RootPath + "\\Syndicates\\" + UniqId + ".syn");
                        AMSXml.RootName = "Syndicate";

                        Syn = new Info(UniqId, Name, "Nouvelle guilde!", Leader.UniqId, Leader.Name, 1000000, 0, new Int32[5], new Int32[5], AMSXml);
                        World.AllSyndicates[UniqId] = Syn;

                        Syn.Leader.Donation = 500000;
                        Syn.Leader.Level = (Byte)Leader.Level;

                        Leader.Syndicate = Syn;
                    }

                    using (AMSXml.Buffer())
                    {
                        AMSXml.SetValue("Informations", "UniqId", UniqId);
                        AMSXml.SetValue("Informations", "Name", Name);
                        AMSXml.SetValue("Informations", "Announce", Syn.Announce);
                        AMSXml.SetValue("Informations", "LeaderUID", Syn.Leader.UniqId);
                        AMSXml.SetValue("Informations", "LeaderName", Syn.Leader.Name);
                        AMSXml.SetValue("Informations", "Money", Syn.Money);
                        AMSXml.SetValue("Informations", "FealtySynUID", Syn.FealtySynUID);
                        AMSXml.SetValue("Informations", "Enemy0", Syn.Enemies[0]);
                        AMSXml.SetValue("Informations", "Enemy1", Syn.Enemies[1]);
                        AMSXml.SetValue("Informations", "Enemy2", Syn.Enemies[2]);
                        AMSXml.SetValue("Informations", "Enemy3", Syn.Enemies[3]);
                        AMSXml.SetValue("Informations", "Enemy4", Syn.Enemies[4]);
                        AMSXml.SetValue("Informations", "Ally0", Syn.Allies[0]);
                        AMSXml.SetValue("Informations", "Ally1", Syn.Allies[1]);
                        AMSXml.SetValue("Informations", "Ally2", Syn.Allies[2]);
                        AMSXml.SetValue("Informations", "Ally3", Syn.Allies[3]);
                        AMSXml.SetValue("Informations", "Ally4", Syn.Allies[4]);
                    }
                    AMSXml = null;

                    Database.Save(Leader);
                    return true;
                }
                catch (Exception Exc) { Program.WriteLine(UniqId + " " + Exc); AMSXml = null; return false; }
            }

            public static void Delete(Int16 UniqId)
            {
                if (World.AllSyndicates.ContainsKey(UniqId))
                {
                    Syndicate.Info Syn = World.AllSyndicates[UniqId];

                    Player Player = null;
                    if (World.AllPlayers.TryGetValue(Syn.Leader.UniqId, out Player))
                    {
                        Player.Syndicate = null;
                        Database.SynKickOut(Syn.Leader.Name);

                        Player.Send(MsgSynAttrInfo.Create(Player.UniqId, Player.Syndicate));
                        World.BroadcastRoomMsg(Player, MsgPlayer.Create(Player), true);
                    }
                    else
                        Database.SynKickOut(Syn.Leader.Name);

                    foreach (Syndicate.Member Member in Syn.Members.Values)
                    {
                        if (World.AllPlayers.TryGetValue(Member.UniqId, out Player))
                        {
                            Player.Syndicate = null;
                            Database.SynKickOut(Member.Name);

                            Player.Send(MsgSynAttrInfo.Create(Player.UniqId, Player.Syndicate));
                            World.BroadcastRoomMsg(Player, MsgPlayer.Create(Player), true);
                        }
                        else
                            Database.SynKickOut(Member.Name);
                    }

                    foreach (Int32 AllyUID in Syn.Allies)
                    {
                        Syndicate.Info Ally = null;
                        if (World.AllSyndicates.TryGetValue((Int16)AllyUID, out Ally))
                        {
                            for (Byte i = 0; i < Ally.Allies.Length; i++)
                            {
                                if (Ally.Allies[i] == Syn.UniqId)
                                {
                                    Ally.Allies[i] = 0;
                                    World.SynThread.AddToQueue(Ally, "Ally" + i, 0);
                                    break;
                                }
                            }
                        }
                    }

                    World.SynThread.AddToQueue(Syn, "DelFlag", true);
                    World.BroadcastMsg(MsgSyndicate.Create(Syn.UniqId, MsgSyndicate.Action.DestroySyn));
                    lock (World.AllSyndicates) { World.AllSyndicates.Remove(UniqId); }
                }
            }
        }
    }
}
