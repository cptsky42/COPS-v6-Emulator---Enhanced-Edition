// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.IO;
using System.Runtime.InteropServices;
using COServer.Entities;
using AMS.Profile;

namespace COServer.Network
{
    public unsafe class MsgFriend : Msg
    {
        public const Int16 Id = _MSG_FRIEND;
        public const Int32 _MAX_USERFRIENDSIZE = 50;

        public enum Action
        {
            None = 0,
            FriendApply = 10,
            FriendAccept = 11,
            FriendOnline = 12,
            FriendOffline = 13,
            FriendBreak = 14,
            GetInfo = 15,
            EnemyOnline = 16,			//To client
            EnemyOffline = 17,			//To client
            EnemyDel = 18,			//To client & to server
            EnemyAdd = 19,			//To client
        };

        public enum Status
        {
            Offline = 0,
            Online = 1
        };

        public struct MsgInfo
        {
            public MsgHeader Header;
            public Int32 UniqId;
            public Byte Action;
            public Byte IsOnline;
            public Int64 Unknow1;
            public Int16 Unknow2;
            public fixed Byte Name[0x10];
        };

        public static Byte[] Create(Int32 UniqId, String Name, Boolean IsOnline, Action Action)
        {
            try
            {
                if (Name == null || Name.Length > _MAX_NAMESIZE)
                    return null;

                Byte[] Out = new Byte[36];
                fixed (Byte* p = Out)
                {
                    *((Int16*)(p + 0)) = (Int16)Out.Length;
                    *((Int16*)(p + 2)) = (Int16)Id;
                    *((Int32*)(p + 4)) = (Int32)UniqId;
                    *((Byte*)(p + 8)) = (Byte)Action;
                    *((Byte*)(p + 9)) = (Byte)(IsOnline ? Status.Online : Status.Offline);
                    *((Int64*)(p + 10)) = (Int64)0x00; //Unknow
                    *((Int16*)(p + 18)) = (Int16)0x00; //Unknow
                    for (Byte i = 0; i < Name.Length; i++)
                        *((Byte*)(p + 20 + i)) = (Byte)Name[i];
                }
                return Out;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return null; }
        }

        public static void Process(Client Client, Byte[] Buffer)
        {
            try
            {
                if (Client == null || Buffer == null || Client.User == null)
                    return;

                Int16 MsgLength = (Int16)((Buffer[0x01] << 8) + Buffer[0x00]);
                Int16 MsgId = (Int16)((Buffer[0x03] << 8) + Buffer[0x02]);
                Int32 UniqId = (Int32)((Buffer[0x07] << 24) + (Buffer[0x06] << 16) + (Buffer[0x05] << 8) + Buffer[0x04]);
                Action Action = (Action)Buffer[0x08];
                Byte IsOnline = (Byte)Buffer[0x09];

                Player Player = Client.User;

                switch (Action)
                {
                    case Action.GetInfo:
                        {
                            Player Target = null;
                            if (!World.AllPlayers.TryGetValue(UniqId, out Target))
                                return;

                            Player.Send(MsgFriend.Create(Target.UniqId, Target.Name, true, Action.FriendOnline));
                            break;
                        }
                    case Action.EnemyAdd:
                        {
                            Player Target = null;
                            if (!World.AllPlayers.TryGetValue(UniqId, out Target))
                                return;

                            Player.Send(MsgFriend.Create(Target.UniqId, Target.Name, true, Action.EnemyOnline));
                            break;
                        }
                    case Action.EnemyDel:
                        {
                            if (Player.Enemies.ContainsKey(UniqId))
                            {
                                Player.Enemies.Remove(UniqId);
                                Player.Send(MsgFriend.Create(UniqId, "", true, Action.EnemyDel));
                            }
                            break;
                        }
                    case Action.FriendBreak:
                        {
                            if (!Player.Friends.ContainsKey(UniqId))
                            {
                                Player.Send(MsgFriend.Create(UniqId, "", true, Action.FriendBreak));
                                return;
                            }
                            String Name = Player.Friends[UniqId];

                            Player.Friends.Remove(UniqId);
                            Player.Send(MsgFriend.Create(UniqId, "", true, Action.FriendBreak));

                            Player Friend = null;
                            if (World.AllPlayers.TryGetValue(UniqId, out Friend))
                            {
                                Friend.Friends.Remove(Player.UniqId);
                                Friend.Send(MsgFriend.Create(Player.UniqId, "", true, Action.FriendBreak));
                            }
                            else
                            {
                                if (!File.Exists(Program.RootPath + "\\Characters\\" + Name + ".chr"))
                                    return;

                                Xml AMSXml = new Xml(Program.RootPath + "\\Characters\\" + Name + ".chr");
                                AMSXml.RootName = "Character";

                                String OldFriends = AMSXml.GetValue("Informations", "Friends", "");
                                String NewFriends = "";
                                String[] Parts = OldFriends.Split(',');
                                foreach (String StrFriend in Parts)
                                {
                                    String[] Info = StrFriend.Split(':');
                                    if (Info.Length < 2)
                                        continue;

                                    Int32 FriendUID = 0;
                                    if (!Int32.TryParse(Info[0], out FriendUID))
                                        continue;

                                    if (FriendUID == Player.UniqId)
                                        continue;

                                    NewFriends += StrFriend + ",";
                                }
                                Parts = null;

                                if (NewFriends != null && NewFriends != "")
                                    NewFriends = NewFriends.Substring(0, NewFriends.Length - 1);

                                using (AMSXml.Buffer()) { AMSXml.SetValue("Informations", "Friends", NewFriends); }
                                AMSXml = null;
                            }
                            break;
                        }
                    case Action.FriendApply:
                        {
                            Player Target = null;
                            if (!World.AllPlayers.TryGetValue(UniqId, out Target))
                                return;

                            if (!Player.IsAlive())
                            {
                                Player.SendSysMsg(Client.GetStr("STR_DIE"));
                                return;
                            }

                            if (Player.Map != Target.Map)
                                return;

                            if (Player.Friends.ContainsKey(Target.UniqId))
                            {
                                Player.SendSysMsg(String.Format(Client.GetStr("STR_FRIEND_ALREADY"), Target.Name));
                                return;
                            }

                            if (Player.Friends.Count >= _MAX_USERFRIENDSIZE)
                            {
                                Player.SendSysMsg(Client.GetStr("STR_FRIEND_FULL"));
                                return;
                            }

                            if (Target.Friends.Count >= _MAX_USERFRIENDSIZE)
                                return;

                            if (Target.FriendRequest != Player.UniqId)
                            {
                                Player.FriendRequest = Target.UniqId;
                                Target.Send(MsgFriend.Create(Player.UniqId, Player.Name, true, Action.FriendApply));
                            }
                            else
                            {
                                if (!Player.Friends.ContainsKey(Target.UniqId))
                                    Player.Friends.Add(Target.UniqId, Target.Name);

                                if (!Target.Friends.ContainsKey(Player.UniqId))
                                    Target.Friends.Add(Player.UniqId, Player.Name);

                                Player.Send(MsgFriend.Create(Target.UniqId, Target.Name, true, Action.FriendAccept));
                                Target.Send(MsgFriend.Create(Player.UniqId, Player.Name, true, Action.FriendAccept));
                            }
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Msg[{0}], Action[{1}] not implemented yet!", MsgId, (Int16)Action);
                            break;
                        }
                }
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }
    }
}
