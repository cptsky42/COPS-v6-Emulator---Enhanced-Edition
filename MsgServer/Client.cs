// * Created by Jean-Philippe Boivin
// * Copyright © 2010-2011
// * Logik. Project

using System;
using COServer.Threads;
using COServer.Network;
using COServer.Entities;
using CO2_CORE_DLL.Net.Sockets;
using CO2_CORE_DLL.Security.Cryptography;

namespace COServer
{
    public class Client
    {
        public Player User;
        public NetworkClient Socket;
        public COSAC Cipher;
        public Language Language;
        public String Account;
        public SByte AccLvl;
        public Int32 Flags;

        public Byte IO_UID = 0;

        public enum Flag
        {
            None = 0x00,
            Banned = 0x01,
        };

        public Int32 NpcUID;
        public Boolean NpcAccept;
        public Int32 NpcParam;

        public Client(NetworkClient Socket)
        {
            this.Socket = Socket;
            this.Cipher = new COSAC();
            this.Cipher.GenerateIV(Server.COSAC_PKey, Server.COSAC_GKey);
            this.Language = Language.Fr;
            this.Account = null;
            this.AccLvl = -1;

            this.NpcUID = -1;
        }

        public String GetStr(String Key) { return STR.Get(Language, Key); }

        ~Client()
        {
            User = null;
            Socket = null;
            Cipher = null;
            Account = null;
        }

        public void Send(Byte[] Buffer)
        {
            if (Buffer == null)
                return; 

            Byte[] Packet = new Byte[Buffer.Length];
            Buffer.CopyTo(Packet, 0);
            Server.NetworkIO.Send(this, Packet);
        }

        public void Disconnect()
        {
            try
            {
                if (User != null)
                {
                    if (User.TransformEndTime != 0)
                    {
                        User.TransformEndTime = 0;
                        User.DelTransform();

                        if (User.CurHP >= User.MaxHP)
                            User.CurHP = User.MaxHP;
                        Double Multiplier = (Double)User.CurHP / (Double)User.MaxHP;
                        MyMath.GetHitPoints(User, true);
                        User.CurHP = (Int32)(User.MaxHP * Multiplier);

                        MyMath.GetMagicPoints(User, true);
                        MyMath.GetEquipStats(User);
                    }
                    User.Thread.Stop();

                    Database.Save(User);

                    foreach (Player Programmer in World.AllMasters.Values)
                        Programmer.SendSysMsg(String.Format("$@ Disconnection of {0} [{1}]!", User.Name, User.UniqId));

                    //Mine
                    if (User.Mining)
                        User.Mining = false;

                    //Booth
                    if (User.Booth != null)
                       User. Booth.Destroy();

                    //Team
                    if (User.Team != null)
                    {
                        if (User.Team.Leader.UniqId == User.UniqId)
                            User.Team.Dismiss(User);
                        else
                            User.Team.DelMember(User, true);
                        User.Team = null;
                    }

                    //Game
                    if (User.Game != null)
                        User.Game.Destroy();

                    //Deal
                    if (User.Deal != null)
                        User.Deal.Release();

                    //Friends
                    foreach (Int32 FriendUID in User.Friends.Keys)
                    {
                        Player Friend = null;
                        if (!World.AllPlayers.TryGetValue(FriendUID, out Friend))
                            continue;

                        Friend.Send(MsgFriend.Create(User.UniqId, User.Name, false, MsgFriend.Action.FriendOffline));
                    }

                    //Enemies
                    foreach (Player Enemy in World.AllPlayers.Values)
                    {
                        if (!Enemy.Enemies.ContainsKey(User.UniqId))
                            continue;

                        Enemy.Send(MsgFriend.Create(User.UniqId, User.Name, false, MsgFriend.Action.EnemyOffline));
                    }

                    //Screen
                    if (User.Screen != null)
                        User.Screen.Clear(true);

                    if (World.AllMaps.ContainsKey(User.Map))
                        World.AllMaps[User.Map].DelEntity(User);

                    if (World.AllPlayers.ContainsKey(User.UniqId))
                        World.AllPlayers.Remove(User.UniqId);

                    if (World.AllMasters.ContainsKey(User.UniqId))
                        World.AllMasters.Remove(User.UniqId);

                    User = null;
                }

                Server.NetworkIO.DelClient(this);

                if (Socket != null && Socket.IsAlive)
                    Socket.Disconnect();
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }
    }
}
