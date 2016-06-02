// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2011 - 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.Runtime.CompilerServices;
using COServer.Entities;

[assembly: InternalsVisibleTo("COServer.Network.Msg")]

namespace COServer.Network
{
    public class MsgFriend : Msg
    {
        /// <summary>
        /// This is a "constant" that the child must override.
        /// It is the type of the message as specified in NetworkDef.cs file.
        /// </summary>
        protected override UInt16 _TYPE { get { return MSG_FRIEND; } }

        public const Int32 _MAX_USERFRIENDSIZE = 50;

        public enum Action : byte
        {
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

        public enum Status : byte
        {
            Offline = 0,
            Online = 1
        };

        //--------------- Internal Members ---------------
        private Int32 __FriendId = 0;
        private Action __Action = (Action)0;
        private Status __OnlineFlag = Status.Offline;
        private UInt16 __Unknown = 0;
        private String __Name = "";
        //------------------------------------------------

        /// <summary>
        /// Unique ID of the friend / enemy.
        /// </summary>
        public Int32 FriendId
        {
            get { return __FriendId; }
            set { __FriendId = value; WriteInt32(4, value); }
        }

        /// <summary>
        /// Action ID.
        /// </summary>
        public Action _Action
        {
            get { return __Action; }
            set { __Action = value; mBuf[8] = (Byte)value; }
        }

        public Status OnlineFlag
        {
            get { return __OnlineFlag; }
            set { __OnlineFlag = value; mBuf[9] = (Byte)value; }
        }

        public String Name
        {
            get { return __Name; }
            set { __Name = value; WriteString(12, value, MAX_NAME_SIZE); }
        }

        /// <summary>
        /// Create a message object from the specified buffer.
        /// </summary>
        /// <param name="aBuf">The buffer containing the message.</param>
        /// <param name="aIndex">The index where the message is starting in the buffer.</param>
        /// <param name="aLength">The length of the message.</param>
        internal MsgFriend(Byte[] aBuf, int aIndex, int aLength)
            : base(aBuf, aIndex, aLength)
        {
            __FriendId = BitConverter.ToInt32(mBuf, 4);
            __Action = (Action)mBuf[8];
            __OnlineFlag = (Status)mBuf[9];
            __Unknown = BitConverter.ToUInt16(mBuf, 10);
            __Name = Program.Encoding.GetString(mBuf, 12, MAX_NAME_SIZE).Trim('\0');
        }

        public MsgFriend(Int32 aFriendId, String aName, Status aOnlineFlag, Action aAction)
            : base(28)
        {
            FriendId = aFriendId;
            _Action = aAction;
            OnlineFlag = aOnlineFlag;
            Name = aName;
        }

        /// <summary>
        /// Process the message for the specified client.
        /// </summary>
        /// <param name="aClient">The client who sent the message.</param>
        public override void Process(Client aClient)
        {
            Player player = aClient.Player;

            switch (_Action)
            {
                case Action.GetInfo:
                    {
                        Player friend = null;
                        if (!World.AllPlayers.TryGetValue(FriendId, out friend))
                            return;

                        player.Send(new MsgFriend(friend.UniqId, friend.Name, Status.Online, Action.FriendOnline));
                        break;
                    }
                case Action.EnemyAdd:
                    {
                        Player enemy = null;
                        if (!World.AllPlayers.TryGetValue(FriendId, out enemy))
                            return;

                        player.Send(new MsgFriend(enemy.UniqId, enemy.Name, Status.Online, Action.EnemyOnline));
                        break;
                    }
                case Action.EnemyDel:
                    {
                        if (player.Enemies.ContainsKey(FriendId))
                        {
                            player.Enemies.Remove(FriendId);
                            player.Send(new MsgFriend(FriendId, "", Status.Offline, Action.EnemyDel));
                        }
                        break;
                    }
                case Action.FriendBreak:
                    {
                        if (!player.Friends.ContainsKey(FriendId))
                        {
                            player.Send(new MsgFriend(FriendId, "", Status.Online, Action.FriendBreak));
                            return;
                        }

                        String name = player.Friends[FriendId];

                        player.Friends.Remove(FriendId);
                        player.Send(new MsgFriend(FriendId, "", Status.Online, Action.FriendBreak));

                        Player friend = null;
                        if (World.AllPlayers.TryGetValue(FriendId, out friend))
                        {
                            friend.Friends.Remove(player.UniqId);
                            friend.Send(new MsgFriend(player.UniqId, "", Status.Online, Action.FriendBreak));
                        }
                        else
                            Database.BreakFriendship(player, name);

                        break;
                    }
                case Action.FriendApply:
                    {
                        Player friend = null;
                        if (!World.AllPlayers.TryGetValue(FriendId, out friend))
                            return;

                        if (!player.IsAlive())
                        {
                            player.SendSysMsg(StrRes.STR_DIE);
                            return;
                        }

                        if (player.Map != friend.Map)
                            return;

                        if (player.Friends.ContainsKey(friend.UniqId))
                        {
                            player.SendSysMsg(StrRes.STR_FRIEND_ALREADY, friend.Name);
                            return;
                        }

                        if (player.Friends.Count >= _MAX_USERFRIENDSIZE)
                        {
                            player.SendSysMsg(StrRes.STR_FRIEND_FULL);
                            return;
                        }

                        if (friend.Friends.Count >= _MAX_USERFRIENDSIZE)
                            return;

                        if (friend.FriendRequest != player.UniqId)
                        {
                            player.FriendRequest = friend.UniqId;
                            friend.Send(new MsgFriend(player.UniqId, player.Name, Status.Online, Action.FriendApply));
                        }
                        else
                        {
                            if (!player.Friends.ContainsKey(friend.UniqId))
                                player.Friends.Add(friend.UniqId, friend.Name);

                            if (!friend.Friends.ContainsKey(player.UniqId))
                                friend.Friends.Add(player.UniqId, player.Name);

                            player.Send(new MsgFriend(friend.UniqId, friend.Name, Status.Online, Action.FriendAccept));
                            friend.Send(new MsgFriend(player.UniqId, player.Name, Status.Online, Action.FriendAccept));
                        }
                        break;
                    }
                default:
                    {
                        sLogger.Error("Action {0} is not implemented for MsgFriend.", (Byte)_Action);
                        break;
                    }
            }
        }
    }
}
