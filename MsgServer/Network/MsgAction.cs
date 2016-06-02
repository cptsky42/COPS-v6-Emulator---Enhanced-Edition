// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2010- 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;
using COServer.Entities;

[assembly: InternalsVisibleTo("COServer.Network.Msg")]

namespace COServer.Network
{
    public class MsgAction : Msg
    {
        /// <summary>
        /// This is a "constant" that the child must override.
        /// It is the type of the message as specified in NetworkDef.cs file.
        /// </summary>
        protected override UInt16 _TYPE { get { return MSG_ACTION; } }

        public enum Action : ushort
        {
            GetPosition = 74,           // to client: idUser is Map
            GetItemSet = 75,
            GetGoodFriend = 76,
            GetWeaponSkillSet = 77,
            GetMagicSet = 78,
            ChgDir = 79,
            Emotion = 81,
            ChgMap = 85,
            EnterMap = 86,
            UpLev = 92,
            XpClear = 93,
            Reborn = 94,
            DelRole = 95,
            SetPkMode = 96,
            GetSynAttr = 97,
            Mine = 99,
            BotCheckA = 100,            //Not sure of the name...
            QueryPlayer = 102,
            MapARGB = 104,              // to client only
            TeamMemberPos = 106,
            //KickBack = 108,           // to client	idUser is Player ID, unPosX unPosY is Player pos
            DropMagic = 109,            // to client only, data is magic type
            DropSkill = 110,            // to client only, data is weapon skill type
            CreateBooth = 111,          // ¿ªÊ¼°ÚÌ¯ to server - unPosX,unPosY: playerpos; unDir:dirofbooth; to client - idTarget:idnpc;
            DestroyBooth = 114,
            PostCmd = 116,              // to client only
            QueryEquipment = 117,       // to server //idTargetÎªÐèÒªqueryµÄPlayerID
            AbortTransform = 118,       // to server
            //CombineSubSyn = 119,      // to client, idUser-> idSubSyn, idTarget-> idTargetSyn
            TakeOff = 120,              // to server, wing user take off
            GetMoney = 121,             // to client only // ¼ñµ½500ÒÔ¼°500ÒÔÉÏÇ®£¬Ö»´«¸ø×Ô¼º£¬dwDataÎª¼ñµ½µÄÇ®
            //CancelKeepBow = 122,      // to server, cancel keep_bow status
            QueryEnemyInfo = 123,       // idTarget = enemy id	// to server
            OpenDialog = 126,           // to client only, open a dialog, dwData is id of dialog
            //FlashStatus = 127,        // broadcast to client only, team member only. dwData is dwStatus
            //QuerySynInfo = 128,       // to server, return CMsgSynInfo. dwData is target syn id.
            LoginCompleted = 130,
            LeaveMap = 132,
            Jump = 133,
            Ghost = 137,
            Synchro = 138,              // ×ø±êÍ¬²½¡£send to client self, request client broadcast self coord if no synchro; broadcast to other client, set coord of this player
            QueryFriendInfo = 140,
            //QueryLeaveWord = 141,
            ChangeFace = 142,
        };

        //--------------- Internal Members ---------------
        private Int32 __Timestamp = 0;
        private Int32 __UniqId = 0;
        private Int32 __Data = 0;
        private UInt16 __X = 0;
        private UInt16 __Y = 0;
        private UInt16 __Direction = 0;
        private Action __Action = (Action)0;
        //------------------------------------------------

        /// <summary>
        /// Timestamp of the creation of the message.
        /// </summary>
        public Int32 Timestamp
        {
            get { return __Timestamp; }
            set { __Timestamp = value; WriteInt32(4, value); }
        }

        /// <summary>
        /// Unique ID of the entity.
        /// </summary>
        public Int32 UniqId
        {
            get { return __UniqId; }
            set { __UniqId = value; WriteInt32(8, value); }
        }

        /// <summary>
        /// Data of the action.
        /// </summary>
        public Int32 Data
        {
            get { return __Data; }
            set { __Data = value; WriteInt32(12, value); }
        }

        /// <summary>
        /// X coord of the entity.
        /// </summary>
        public UInt16 X
        {
            get { return __X; }
            set { __X = value; WriteUInt16(16, value); }
        }

        /// <summary>
        /// Y coord of the entity.
        /// </summary>
        public UInt16 Y
        {
            get { return __Y; }
            set { __Y = value; WriteUInt16(18, value); }
        }

        /// <summary>
        /// Direction of the entity.
        /// </summary>
        public Byte Direction
        {
            get { return (Byte)__Direction; }
            set { __Direction = value; WriteUInt16(20, value); }
        }

        /// <summary>
        /// Action ID.
        /// </summary>
        public Action _Action
        {
            get { return __Action; }
            set { __Action = value; WriteUInt16(22, (UInt16)value); }
        }

        /// <summary>
        /// Create a message object from the specified buffer.
        /// </summary>
        /// <param name="aBuf">The buffer containing the message.</param>
        /// <param name="aIndex">The index where the message is starting in the buffer.</param>
        /// <param name="aLength">The length of the message.</param>
        internal MsgAction(Byte[] aBuf, int aIndex, int aLength)
            : base(aBuf, aIndex, aLength)
        {
            __Timestamp = BitConverter.ToInt32(mBuf, 4);
            __UniqId = BitConverter.ToInt32(mBuf, 8);
            __Data = BitConverter.ToInt32(mBuf, 12);
            __X = BitConverter.ToUInt16(mBuf, 16);
            __Y = BitConverter.ToUInt16(mBuf, 18);
            __Direction = BitConverter.ToUInt16(mBuf, 20);
            __Action = (Action)BitConverter.ToUInt16(mBuf, 22);
        }

        public MsgAction(Entity aEntity, Int32 aData, Action aAction)
            : base(24)
        {
            Timestamp = Environment.TickCount;
            UniqId = aEntity.UniqId;
            Data = aData;
            X = aEntity.X;
            Y = aEntity.Y;
            Direction = aEntity.Direction;
            _Action = aAction;
        }

        public MsgAction(Entity aEntity, UInt32 aData, Action aAction)
            : base(24)
        {
            Timestamp = Environment.TickCount;
            UniqId = aEntity.UniqId;
            Data = (Int32)aData;
            X = aEntity.X;
            Y = aEntity.Y;
            Direction = aEntity.Direction;
            _Action = aAction;
        }

        /// <summary>
        /// Process the message for the specified client.
        /// </summary>
        /// <param name="aClient">The client who sent the message.</param>
        public override void Process(Client aClient)
        {
            try
            {
                Player player = aClient.Player;

                switch (_Action)
                {
                    case Action.GetPosition:
                        {
                            if (player.UniqId != UniqId)
                                return;

                            player.Send(new MsgAction(player, player.Map.DocId, Action.GetPosition));
                            player.Send(new MsgAction(player, (Int32)player.Map.Light, Action.MapARGB));
                            player.Send(new MsgMapInfo(player.Map));
                            if (player.Map.Weather != 0)
                                player.Send(new MsgWeather(player.Map));
                            break;
                        }
                    case Action.GetItemSet:
                        {
                            if (player.UniqId != UniqId)
                                return;

                            foreach (Item Item in World.AllItems.Values)
                            {
                                if (Item.OwnerUID == player.UniqId)
                                {
                                    player.Items.Add(Item.Id, Item);
                                    if (Item.Position < 10)
                                    {
                                        player.Send(new MsgItemInfo(Item, MsgItemInfo.Action.AddItem));
                                        World.BroadcastRoomMsg(player, new MsgItemInfoEx(UniqId, Item, 0, MsgItemInfoEx.Action.Equipment), false);
                                    }
                                }
                            }
                            player.CalcMaxHP();
                            player.CalcMaxMP();
                            MyMath.GetEquipStats(player);

                            player.Send(this);
                            break;
                        }
                    case Action.GetGoodFriend:
                        {
                            if (player.UniqId != UniqId)
                                return;

                            foreach (KeyValuePair<Int32, String> KV in player.Friends)
                            {
                                var onlineFlag = World.AllPlayers.ContainsKey(KV.Key) ? MsgFriend.Status.Online : MsgFriend.Status.Offline;
                                player.Send(new MsgFriend(KV.Key, KV.Value, onlineFlag, MsgFriend.Action.GetInfo));

                                Player friend = null;
                                if (!World.AllPlayers.TryGetValue(KV.Key, out friend))
                                    continue;

                                friend.Send(new MsgFriend(player.UniqId, player.Name, MsgFriend.Status.Online, MsgFriend.Action.FriendOnline));
                            }

                            foreach (KeyValuePair<Int32, String> KV in player.Enemies)
                            {
                                var onlineFlag = World.AllPlayers.ContainsKey(KV.Key) ? MsgFriend.Status.Online : MsgFriend.Status.Offline;
                                player.Send(new MsgFriend(KV.Key, KV.Value, onlineFlag, MsgFriend.Action.EnemyAdd));
                            }

                            foreach (Player enemy in World.AllPlayers.Values)
                            {
                                if (!enemy.Enemies.ContainsKey(player.UniqId))
                                    continue;

                                enemy.Send(new MsgFriend(player.UniqId, player.Name, MsgFriend.Status.Online, MsgFriend.Action.EnemyOnline));
                            }

                            player.Send(this);
                            break;
                        }
                    case Action.GetWeaponSkillSet:
                        {
                            if (player.UniqId != UniqId)
                                return;

                            player.SendWeaponSkillSet();
                            player.Send(this);
                            break;
                        }
                    case Action.GetMagicSet:
                        {
                            if (player.UniqId != UniqId)
                                return;

                            player.SendMagicSkillSet();
                            player.Send(this);
                            break;
                        }
                    case Action.ChgDir:
                        {
                            if (player.UniqId != UniqId)
                                return;

                            player.Direction = Direction;
                            World.BroadcastRoomMsg(player, this, false);
                            break;
                        }
                    case Action.Emotion:
                        {
                            if (player.UniqId != UniqId)
                                return;

                            player.IsInBattle = false;
                            player.MagicIntone = false;
                            player.Mining = false;

                            player.Action = (Emotion)Data;
                            if (Emotion.Cool == player.Action)
                            {
                                if (Environment.TickCount - player.LastCoolShow > 3000)
                                {
                                    if (player.IsAllNonsuchEquip())
                                        Data |= (player.Profession * 0x00010000 + 0x01000000);
                                    else if ((player.GetArmorTypeID() % 10) == 9)
                                        Data |= player.Profession * 0x010000;

                                    player.LastCoolShow = Environment.TickCount;
                                }
                            }
                            World.BroadcastRoomMsg(player, this, true);
                            break;
                        }
                    case Action.ChgMap:
                        {
                            UInt16 passageX = (UInt16)(Data);
                            UInt16 passageY = (UInt16)(Data >> 16);

                            if (player.UniqId != UniqId)
                                return;

                            // if requested with a big range, it's a hack, else we consider that it's a lag...
                            if (!MyMath.CanSee(player.X, player.Y, passageX, passageY, 10))
                            {
                                if (Database.SendPlayerToJail(player.Name))
                                    Program.Log("[CRIME] {0} has been sent to jail for using a portal hack !", player.Name);
                                return;
                            }

                            int passageId = player.Map.GetPassage(passageX, passageY);

                            try
                            {
                                PasswayInfo passway = Database.AllPassways.Find(p => p.MapId == player.Map.Id && p.Idx == passageId);
                                player.Move(passway.PortalMap, passway.PortalX, passway.PortalY);
                            }
                            catch (InvalidOperationException)
                            {
                                sLogger.Warn("Failed to find the passway {0} of the map {1}.", passageId, player.Map.Id);
                                player.Move(player.Map.Id, player.PrevX, player.PrevY);
                            }

                            break;
                        }
                    case Action.XpClear:
                        {
                            if (player.UniqId != UniqId)
                                return;

                            player.XP = 0;
                            player.DetachStatus(Status.XpFull);
                            player.LastXPAdd = Environment.TickCount;
                            break;
                        }
                    case Action.Reborn:
                        {
                            if (player.UniqId != UniqId)
                                return;

                            if (player.IsAlive())
                                return;

                            //If requested before 17s, it's a hack, else we consider that it's a lag...
                            //It should be 20s in reality.
                            if (Environment.TickCount - player.LastDieTick < 17000)
                            {
                                if (Database.SendPlayerToJail(player.Name))
                                    Program.Log("[CRIME] {0} has been sent to jail for using a revive hack!", player.Name);
                                return;
                            }

                            player.Reborn(true);
                            break;
                        }
                    case Action.DelRole:
                        {
                            if (player.UniqId != UniqId)
                                return;

                            if (Data != player.mLockPin)
                                return;

                            Database.Delete(player);
                            break;
                        }
                    case Action.SetPkMode:
                        {
                            if (player.UniqId != UniqId)
                                return;

                            String Msg = "";
                            switch ((PkMode)Data)
                            {
                                case PkMode.Free:
                                    Msg = StrRes.STR_FREE_PK_MODE;
                                    break;

                                case PkMode.Safe:
                                    Msg = StrRes.STR_SAFE_PK_MODE;
                                    break;

                                case PkMode.Team:
                                    Msg = StrRes.STR_TEAM_PK_MODE;
                                    break;

                                case PkMode.Arrestment:
                                    Msg = StrRes.STR_ARRESTMENT_PK_MODE;
                                    break;
                            }

                            player.PkMode = (PkMode)Data;
                            player.IsInBattle = false;

                            player.Send(this);
                            player.SendSysMsg(Msg);
                            break;
                        }
                    case Action.GetSynAttr:
                        {
                            if (player.UniqId != UniqId)
                                return;

                            Syndicate Syndicate = player.Syndicate;
                            player.Send(new MsgSynAttrInfo(UniqId, Syndicate));

                            if (Syndicate != null)
                            {
                                foreach (Int32 enemyId in Syndicate.Enemies)
                                    player.Send(new MsgSyndicate((UInt32)enemyId, MsgSyndicate.Action.SetAntagonize));

                                foreach (Int32 allyId in Syndicate.Allies)
                                    player.Send(new MsgSyndicate((UInt32)allyId, MsgSyndicate.Action.SetAlly));


                                //OBJID idSyn = this->GetID();
                                //CMsgSyndicate	msg;
                                //IF_OK(msg.Create(SET_SYN, idSyn, this->GetInt(SYNDATA_FEALTY)))
                                //    pUser->SendMsg(&msg);

                                //IF_OK(msg.Create(SYN_SET_PUBLISHTIME, idSyn, this->GetInt(SYNDATA_PUBLISHTIME)))
                                //    pUser->SendMsg(&msg);

                                //// ×Ó°ïÅÉ
                                //for( i = 0; i < SynManager()->QuerySynSet()->GetAmount(); i++)  
                                //{
                                //    CSyndicate* pSyn = SynManager()->QuerySynSet()->GetObjByIndex(i);
                                //    if(pSyn && pSyn->GetInt(SYNDATA_FEALTY) == idSyn)
                                //    {
                                //        pSyn->SendInfoToClient(pUser);
                                //    }
                                //}
                            }

                            player.Send(this);
                            break;
                        }
                    case Action.Mine:
                        {
                            if (player.UniqId != UniqId)
                                return;

                            if (!player.IsAlive())
                            {
                                player.SendSysMsg(StrRes.STR_DIE);
                                return;
                            }

                            if (!player.Map.IsMineField())
                            {
                                player.SendSysMsg(StrRes.STR_NO_MINE);
                                return;
                            }

                            player.Mine();
                            break;
                        }
                    case Action.BotCheckA:
                        {
                            if (player.UniqId != UniqId)
                                return;

                            if (Data != Timestamp)
                                aClient.Disconnect();
                            break;
                        }
                    case Action.QueryPlayer:
                        {
                            if (player.UniqId != Data)
                                return;

                            Player Target = null;
                            if (World.AllPlayers.TryGetValue(UniqId, out Target))
                                player.Send(new MsgPlayer(Target));
                            break;
                        }
                    case Action.TeamMemberPos:
                        {
                            if (UniqId != Data)
                                return;

                            if (player.Team == null)
                                return;

                            if (!player.Team.IsTeamMember(UniqId))
                                return;

                            Player Target = null;
                            if (World.AllPlayers.TryGetValue(UniqId, out Target))
                                player.Send(new MsgAction(Target, (Int32)Target.Map.Id, Action.TeamMemberPos));
                            break;
                        }
                    case Action.CreateBooth:
                        {
                            if (player.UniqId != UniqId)
                                return;

                            player.Direction = Direction;

                            Booth Booth = Booth.Create(player, X, Y);
                            Booth.SendShow(player);

                            player.Send(new MsgAction(player, Booth.UniqId, Action.CreateBooth));
                            break;
                        }
                    case Action.DestroyBooth:
                        {
                            if (player.UniqId != UniqId)
                                return;

                            if (player.Booth != null)
                                player.Booth.Destroy();

                            player.Map.AddEntity(player);
                            player.Screen.ChangeMap();
                            break;
                        }
                    case Action.TakeOff:
                        {
                            player.DetachStatus(Status.Flying);
                            break;
                        }
                    case Action.QueryEquipment:
                        {
                            if (player.UniqId != UniqId)
                                return;

                            Player Target = null;
                            if (!World.AllPlayers.TryGetValue(Data, out Target))
                                return;

                            Target.SendSysMsg("TODO STR " + player.Name + " is looking at your gear...");

                            Item Item = null;
                            for (Byte i = 1; i < 10; i++)
                            {
                                Item = Target.GetItemByPos(i);
                                if (Item != null)
                                    player.Send(new MsgItemInfo(Target.UniqId, Item, MsgItemInfo.Action.OtherPlayer_Equipement));
                            }
                            break;
                        }
                    case Action.AbortTransform:
                        {
                            if (player.UniqId != UniqId)
                                return;

                            player.TransformEndTime = Environment.TickCount - 2000;
                            break;
                        }
                    case Action.QueryEnemyInfo:
                        {
                            if (player.UniqId != UniqId)
                                return;

                            if (!player.Enemies.ContainsKey(Data))
                                return;

                            Player enemy = null;
                            if (!World.AllPlayers.TryGetValue(Data, out enemy))
                                return;

                            player.Send(new MsgFriendInfo(enemy));
                            break;
                        }
                    case Action.LoginCompleted:
                        {
                            if (player.UniqId != UniqId)
                                return;

                            break;
                        }
                    case Action.Jump:
                        {
                            UInt16 NewX = (UInt16)(Data);
                            UInt16 NewY = (UInt16)(Data >> 16);

                            if (player.UniqId != UniqId)
                                return;

                            if (player.X != X || player.Y != Y)
                            {
                                player.KickBack();
                                return;
                            }

                            if (!player.IsAlive())
                            {
                                player.SendSysMsg(StrRes.STR_DIE);
                                player.KickBack();
                                return;
                            }

                            if (!MyMath.CanSee(X, Y, NewX, NewY, 17))
                            {
                                player.KickBack();
                                return;
                            }

                            if (!player.Map.GetFloorAccess(NewX, NewY))
                            {
                                player.SendSysMsg(StrRes.STR_INVALID_COORDINATE);
                                player.KickBack();
                                return;
                            }

                            //The maximum elevation difference (between the character's initial position and the check tile's position) is 210
                            int newAlt = player.Map.GetFloorAlt(NewX, NewY), prevAlt = player.Map.GetFloorAlt(player.X, player.Y);
                            if (newAlt - prevAlt > 210 && newAlt - prevAlt < 1000)  // otherwise, a bug in the DMap
                            {
                                if (Database.SendPlayerToJail(player.Name))
                                    Program.Log("[CRIME] {0} has been sent to jail for using a wall jump hack!", player.Name);
                                return;
                            }

                            //Normal: 800, Cyclone: 400, Fly: 520, Fly + Cyclone: 275, DH: 250
                            //So, the tick shouldn't be lower than ~410 if the user doesn't use Cyclone or DH...
                            if (!player.HasStatus(Status.SuperSpeed) && player.TransformEndTime == 0
                                && Environment.TickCount - player.LastJumpTick < 410)
                                player.SpeedHack++;
                            player.LastJumpTick = Environment.TickCount;

                            // TODO re-enable PrevX/Y in jump ?
                            //player.PrevX = player.X;
                            //player.PrevY = player.Y;

                            Byte direction = (Byte)MyMath.GetDirectionCO(player.X, player.Y, NewX, NewY);

                            player.X = NewX;
                            player.Y = NewY;
                            player.Direction = direction;
                            player.Action = Emotion.StandBy;

                            player.IsInBattle = false;
                            player.MagicIntone = false;
                            player.Mining = false;

                            player.Send(this);
                            player.Screen.Move(this);
                            break;
                        }
                    case Action.Ghost:
                        {
                            if (player.UniqId != UniqId)
                                return;

                            if (!player.IsAlive())
                                player.TransformGhost();
                            break;
                        }
                    case Action.Synchro:
                        {
                            UInt16 ClientX = (UInt16)(Data >> 16);
                            UInt16 ClientY = (UInt16)(Data);

                            if (player.UniqId != UniqId)
                                return;

                            player.X = ClientX;
                            player.Y = ClientY;

                            player.Send(new MsgAction(player, Data, Action.Jump));
                            World.BroadcastRoomMsg(player, this, false);
                            break;
                        }
                    case Action.QueryFriendInfo:
                        {
                            if (player.UniqId != UniqId)
                                return;

                            if (!player.Friends.ContainsKey(Data))
                                return;

                            Player friend = null;
                            if (!World.AllPlayers.TryGetValue(Data, out friend))
                                return;

                            player.Send(new MsgFriendInfo(friend));
                            break;
                        }
                    case Action.ChangeFace:
                        {
                            if (player.UniqId != UniqId)
                                return;

                            if (player.Money < 500)
                            {
                                player.SendSysMsg(StrRes.STR_NOT_SO_MUCH_MONEY);
                                return;
                            }
                            player.Money -= 500;
                            player.Send(new MsgUserAttrib(player, player.Money, MsgUserAttrib.AttributeType.Money));
                            player.Look = (UInt32)(player.Look - ((Int32)(player.Look / 10000) * 10000) + (Data * 10000));

                            if (player.Team != null)
                                player.Team.BroadcastMsg(this);
                            break;
                        }
                    case (Action)310:
                        {
                            if (player.UniqId != UniqId)
                                return;

                            Player Target = null;
                            if (!World.AllPlayers.TryGetValue(Data, out Target))
                                return;

                            Target.SendSysMsg("TODO STR " + player.Name + " is looking at your gear...");

                            Item Item = null;
                            for (Byte i = 1; i < 10; i++)
                            {
                                Item = Target.GetItemByPos(i);
                                if (Item != null)
                                    player.Send(new MsgItemInfo(Target.UniqId, Item, MsgItemInfo.Action.OtherPlayer_Equipement));
                            }

                            break;
                        }
                    default:
                        {
                            sLogger.Error("Action {0} is not implemented for MsgAction.", (UInt16)_Action);
                            break;
                        }
                }
            }
            catch (Exception exc) { sLogger.Error(exc); }
        }
    }
}
