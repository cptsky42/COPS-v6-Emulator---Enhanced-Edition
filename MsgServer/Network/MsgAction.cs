// * Created by Jean-Philippe Boivin
// * Copyright © 2010-2011
// * Logik. Project

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using COServer.Entities;
using CO2_CORE_DLL;

namespace COServer.Network
{
    public unsafe class MsgAction : Msg
    {
        public const Int16 Id = _MSG_ACTION;

        public enum Action
        {
            None = 0,
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

        public enum PkMode
        {
            Free = 0,
            Safe = 1,
            Team = 2,
            Arrestment = 3,
        };

        public enum Emotion
        {
            None = 0,
            Dance1 = 1,
            Dance2 = 2,
            Dance3 = 3,
            Dance4 = 4,
            Dance5 = 5,
            Dance6 = 6,
            Dance7 = 7,
            Dance8 = 8,
            StandBy = 100,
            Laugh = 150,
            GufFaw = 151,
            Fury = 160,
            Sad = 170,
            Excitement = 180,
            SayHello = 190,
            Salute = 200,
            Genuflect = 210,
            Kneel = 220,
            Cool = 230,
            Swim = 240,
            SitDown = 250,
            Zazen = 260,
            Faint = 270,
            Lie = 271,
        };

        public enum Dialog
        {
            None = 0,
            Compose = 1,
            Forge = 2,
            Forge2 = 3,
            Warehouse = 4,
            OfflineTG = 44,
        };

        public enum Command
        {
            QuitGame = 1,
            HideClient = 2,
            OnlineShop = 26, //Common (EMoney)
            TradeCursor = 27,
            ChatHistorique = 32,
            ChatColor = 33,
            DropMoney = 44,
            SynCursor = 46,
            QuitSyn = 47,
            FriendCursor = 54,
            BlackList = 80,
            ToS = 101, //StrRes (10367)
            HideCounter = 105,
            Crash0 = 114,
            Capture = 1025,
            XPSkillShow = 1037,
            XPSkillHide = 1038,
            InternetLag1 = 1041,
            DeleteEnemy = 1052,
            ShowRevive = 1053,
            HideRevive = 1054,
            LeaveWords = 1058,
            SynStatuary = 1066,
            EmptyDialog1 = 1070,
            OtherEquipmentDialog = 1074,
            GambleOpen = 1077,
            GambleClose = 1078,
            InternetLag2 = 1083,
            AcceptToS = 1085, //StrRes (Text: 10405, 10406 Link: 10367)
            Compose = 1086,
            ForgeOpen = 1088,
            ForgeOpen2 = 1089,
            WarehouseOpen = 1090,
            EnchantOpen = 1091,
            OfflineTG = 1092, 
            ShoppingMallOpen = 1099,
            ShoppingMallBtnShow = 1100,
            ShoppingMallBtnHide = 1101,
            Crash1 = 1106,
            EmptyDialog2 = 1116,
            NoOfflineTG = 1117,
            MsgRadioEffect = 1130,
            MsgRadioOpen = 1133,
            MsgRadioClose = 1135,
            PathFindingOpen = 1142,
            PathFindingClose = 1143,
            Crash2 = 1145,
            CancelPathFinding = 1146,
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct MsgInfo
        {
            public MsgHeader Header;
            public Int32 Timestamp;
            public Int32 UniqId;
            public Int32 Param;
            public UInt16 X;
            public UInt16 Y;
            public UInt16 Direction;
            public Int16 Action;
        };

        public static Byte[] Create(Entity Entity, Int32 Param, Action Action)
        {
            try
            {
                MsgInfo* Msg = stackalloc MsgInfo[1];
                Msg->Header.Length = (Int16)sizeof(MsgInfo);
                Msg->Header.Type = Id;

                Msg->Timestamp = Environment.TickCount;
                Msg->UniqId = Entity.UniqId;
                Msg->X = Entity.X;
                Msg->Y = Entity.Y;
                Msg->Direction = Entity.Direction;
                Msg->Param = Param;
                Msg->Action = (Int16)Action;

                Byte[] Out = new Byte[Msg->Header.Length];
                Kernel.memcpy(Out, Msg, Out.Length);
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

                if (Buffer.Length != sizeof(MsgInfo))
                    return;

                fixed (Byte* pBuf = Buffer)
                {
                    MsgInfo* pMsg = (MsgInfo*)pBuf;

                    switch ((Action)pMsg->Action)
                    {
                        case Action.GetPosition:
                            {
                                Player Player = Client.User;
                                if (Player.UniqId != pMsg->UniqId)
                                    return;

                                if (!World.AllMaps.ContainsKey(Player.Map))
                                    return;

                                Player.Send(MsgAction.Create(Player, World.AllMaps[Player.Map].Id, Action.GetPosition));
                                Player.Send(MsgAction.Create(Player, (Int32)World.AllMaps[Player.Map].Color, Action.MapARGB));
                                Player.Send(MsgMapInfo.Create(World.AllMaps[Player.Map]));
                                if (World.AllMaps[Player.Map].Weather != 0)
                                    Player.Send(MsgWeather.Create(World.AllMaps[Player.Map]));
                                break;
                            }
                        case Action.GetItemSet:
                            {
                                Player Player = Client.User;
                                if (Player.UniqId != pMsg->UniqId)
                                    return;

                                foreach (Item Item in World.AllItems.Values)
                                {
                                    if (Item.OwnerUID == Player.UniqId)
                                    {
                                        Player.Items.Add(Item.UniqId, Item);
                                        if (Item.Position < 10)
                                        {
                                            Player.Send(MsgItemInfo.Create(Item, MsgItemInfo.Action.AddItem));
                                            World.BroadcastRoomMsg(Player, MsgItemInfoEx.Create(pMsg->UniqId, Item, 0, MsgItemInfoEx.Action.Equipment), false);
                                        }
                                    }
                                }
                                MyMath.GetHitPoints(Player, true);
                                MyMath.GetMagicPoints(Player, true);
                                MyMath.GetPotency(Player, true);
                                MyMath.GetEquipStats(Player);
                                Player.SendEquipStats();

                                Player.Send(Buffer);
                                break;
                            }
                        case Action.GetGoodFriend:
                            {
                                Player Player = Client.User;
                                if (Player.UniqId != pMsg->UniqId)
                                    return;

                                foreach (KeyValuePair<Int32, String> KV in Player.Friends)
                                {
                                    Player.Send(MsgFriend.Create(KV.Key, KV.Value, World.AllPlayers.ContainsKey(KV.Key), MsgFriend.Action.GetInfo));

                                    Player Friend = null;
                                    if (!World.AllPlayers.TryGetValue(KV.Key, out Friend))
                                        continue;

                                    Friend.Send(MsgFriend.Create(Player.UniqId, Player.Name, true, MsgFriend.Action.FriendOnline));
                                }

                                foreach (KeyValuePair<Int32, String> KV in Player.Enemies)
                                    Player.Send(MsgFriend.Create(KV.Key, KV.Value, World.AllPlayers.ContainsKey(KV.Key), MsgFriend.Action.EnemyAdd));

                                foreach (Player Enemy in World.AllPlayers.Values)
                                {
                                    if (!Enemy.Enemies.ContainsKey(Player.UniqId))
                                        continue;

                                    Enemy.Send(MsgFriend.Create(Player.UniqId, Player.Name, true, MsgFriend.Action.EnemyOnline));
                                }

                                Player.Send(Buffer);
                                break;
                            }
                        case Action.GetWeaponSkillSet:
                            {
                                Player Player = Client.User;
                                if (Player.UniqId != pMsg->UniqId)
                                    return;

                                foreach (WeaponSkill WeaponSkill in World.AllWeaponSkills.Values)
                                {
                                    if (WeaponSkill.OwnerUID == Player.UniqId)
                                    {
                                        Player.WeaponSkills.Add(WeaponSkill.UniqId, WeaponSkill);
                                        if (!WeaponSkill.Unlearn)
                                            Player.Send(MsgWeaponSkill.Create(WeaponSkill));
                                    }
                                }

                                Player.Send(Buffer);
                                break;
                            }
                        case Action.GetMagicSet:
                            {
                                Player Player = Client.User;
                                if (Player.UniqId != pMsg->UniqId)
                                    return;

                                foreach (Magic Magic in World.AllMagics.Values)
                                {
                                    if (Magic.OwnerUID == Player.UniqId)
                                    {
                                        Player.Magics.Add(Magic.UniqId, Magic);
                                        if (!Magic.Unlearn)
                                            Player.Send(MsgMagicInfo.Create(Magic));
                                    }
                                }

                                Player.Send(Buffer);
                                Player.CheckWeaponSkills();
                                break;
                            }
                        case Action.ChgDir:
                            {
                                Player Player = Client.User;
                                if (Player.UniqId != pMsg->UniqId)
                                    return;

                                Player.Direction = (Byte)pMsg->Direction;
                                World.BroadcastRoomMsg(Player, Buffer, false);
                                break;
                            }
                        case Action.Emotion:
                            {
                                Player Player = Client.User;
                                if (Player.UniqId != pMsg->UniqId)
                                    return;

                                Player.IsInBattle = false;
                                Player.MagicIntone = false;
                                Player.Mining = false;

                                Player.Action = (Int16)pMsg->Param;
                                if (Emotion.Cool == (Emotion)pMsg->Param)
                                {
                                    if (Environment.TickCount - Player.LastCoolShow > 3000)
                                    {
                                        if (Player.IsAllNonsuchEquip())
                                            pMsg->Param |= (Player.Profession * 0x00010000 + 0x01000000);
                                        else if ((Player.GetArmorTypeID() % 10) == 9)
                                            pMsg->Param |= Player.Profession * 0x010000;

                                        Player.LastCoolShow = Environment.TickCount;
                                    }
                                }
                                World.BroadcastRoomMsg(Player, Buffer, true);
                                break;
                            }
                        case Action.ChgMap:
                            {
                                UInt16 PortalX = (UInt16)(pMsg->Param);
                                UInt16 PortalY = (UInt16)(pMsg->Param >> 16);

                                Player Player = Client.User;
                                if (Player.UniqId != pMsg->UniqId)
                                    return;

                                //If requested with a big range, it's a hack, else we consider that it's a lag...
                                if (!MyMath.CanSee(Player.X, Player.Y, PortalX, PortalY, 10))
                                {
                                    Database.Jail(Player.Name);

                                    Player.JailC++;
                                    Player.Move(6001, 28, 75);

                                    Program.Log("[CRIME] " + Player.Name + " has been sent to jail for using a portal hack!");
                                    World.BroadcastMsg(MsgTalk.Create("SYSTEM", "ALLUSERS", Player.Name + " has been sent to jail!", MsgTalk.Channel.GM, 0xFFFFFF));
                                    return;
                                }

                                if (!MyMath.CanSee(Player.X, Player.Y, PortalX, PortalY, 2))
                                {
                                    Player.Move(Player.Map, Player.PrevX, Player.PrevY);
                                    return;
                                }

                                foreach (PortalInfo Info in Database.AllPortals)
                                {
                                    if (Info.FromMap != Player.Map)
                                        continue;

                                    if (Info.FromX != PortalX || Info.FromY != PortalY)
                                        continue;

                                    Player.Move(Info.ToMap, Info.ToX, Info.ToY);
                                    return;
                                }
                                Player.Move(Player.Map, Player.PrevX, Player.PrevY);
                                break;
                            }
                        case Action.XpClear:
                            {
                                Player Player = Client.User;
                                if (Player.UniqId != pMsg->UniqId)
                                    return;

                                Player.XP = 0;
                                Player.DelFlag(Player.Flag.XPList);
                                Player.Send(MsgUserAttrib.Create(Player, Player.Flags, MsgUserAttrib.Type.Flags));
                                Player.LastXPAdd = Environment.TickCount;
                                break;
                            }
                        case Action.Reborn:
                            {
                                Player Player = Client.User;
                                if (Player.UniqId != pMsg->UniqId)
                                    return;

                                if (Player.IsAlive())
                                    return;

                                //If requested before 17s, it's a hack, else we consider that it's a lag...
                                //It should be 20s in reality.
                                if (Environment.TickCount - Player.LastDieTick < 17000)
                                {
                                    Database.Jail(Player.Name);

                                    Player.JailC++;
                                    Player.Move(6001, 28, 75);

                                    Program.Log("[CRIME] " + Player.Name + " has been sent to jail for using a revive hack!");
                                    World.BroadcastMsg(MsgTalk.Create("SYSTEM", "ALLUSERS", Player.Name + " has been sent to jail!", MsgTalk.Channel.GM, 0xFFFFFF));
                                    return;
                                }

                                if (pMsg->Param == 1)
                                {
                                    Map Map = null;
                                    if (!World.AllMaps.TryGetValue(Player.Map, out Map))
                                        return;

                                    if (!Map.IsRebornNow_Enable())
                                        return;
                                }

                                Player.Reborn(pMsg->Param != 1);
                                break;
                            }
                        case Action.DelRole:
                            {
                                Player Player = Client.User;
                                if (Player.UniqId != pMsg->UniqId)
                                    return;

                                if (pMsg->Param != Player.WHPIN)
                                    return;

                                Database.Delete(Player);
                                break;
                            }
                        case Action.SetPkMode:
                            {
                                Player Player = Client.User;
                                if (Player.UniqId != pMsg->UniqId)
                                    return;

                                String Msg = "";
                                switch ((PkMode)pMsg->Param)
                                {
                                    case PkMode.Free:
                                        Msg = Client.GetStr("STR_FREE_PK_MODE");
                                        break;

                                    case PkMode.Safe:
                                        Msg = Client.GetStr("STR_SAFE_PK_MODE");
                                        break;

                                    case PkMode.Team:
                                        Msg = Client.GetStr("STR_TEAM_PK_MODE");
                                        break;

                                    case PkMode.Arrestment:
                                        Msg = Client.GetStr("STR_ARRESTMENT_PK_MODE");
                                        break;
                                }

                                Player.PkMode = (Byte)pMsg->Param;
                                Player.IsInBattle = false;

                                Player.Send(Buffer);
                                Player.SendSysMsg(Msg);
                                break;
                            }
                        case Action.GetSynAttr:
                            {
                                Player Player = Client.User;
                                if (Player.UniqId != pMsg->UniqId)
                                    return;

                                Syndicate.Info Syndicate = Player.Syndicate;
                                Player.Send(MsgSynAttrInfo.Create(pMsg->UniqId, Syndicate));

                                if (Syndicate != null)
                                {
                                    foreach (Int32 Enemy in Syndicate.Enemies)
                                        Player.Send(MsgSyndicate.Create(Enemy, MsgSyndicate.Action.SetAntagonize));

                                    foreach (Int32 Ally in Syndicate.Allies)
                                        Player.Send(MsgSyndicate.Create(Ally, MsgSyndicate.Action.SetAlly));


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

                                Player.Send(Buffer);
                                break;
                            }
                        case Action.Mine:
                            {
                                Player Player = Client.User;
                                if (Player.UniqId != pMsg->UniqId)
                                    return;

                                if (!Player.IsAlive())
                                {
                                    Player.SendSysMsg(Client.GetStr("STR_DIE"));
                                    return;
                                }

                                Map Map;
                                if (!World.AllMaps.TryGetValue(Player.Map, out Map))
                                    return;

                                if (!Map.IsMineField())
                                {
                                    Player.SendSysMsg(Client.GetStr("STR_NO_MINE"));
                                    return;
                                }

                                Player.Mine();
                                break;
                            }
                        case Action.BotCheckA:
                            {
                                Player Player = Client.User;
                                if (Player.UniqId != pMsg->UniqId)
                                    return;

                                if (pMsg->Param != pMsg->Timestamp)
                                    Client.Disconnect();
                                break;
                            }
                        case Action.QueryPlayer:
                            {
                                Player Player = Client.User;
                                if (Player.UniqId != pMsg->Param)
                                    return;

                                Player Target = null;
                                if (World.AllPlayers.TryGetValue(pMsg->UniqId, out Target))
                                    Player.Send(MsgPlayer.Create(Target));
                                break;
                            }
                        case Action.TeamMemberPos:
                            {
                                Player Player = Client.User;
                                if (pMsg->UniqId != pMsg->Param)
                                    return;

                                if (Player.Team == null)
                                    return;

                                if (!Player.Team.IsTeamMember(pMsg->UniqId))
                                    return;

                                Player Target = null;
                                if (World.AllPlayers.TryGetValue(pMsg->UniqId, out Target))
                                    Player.Send(MsgAction.Create(Target, Target.Map, Action.TeamMemberPos));
                                break;
                            }
                        case Action.CreateBooth:
                            {
                                Player Player = Client.User;
                                if (Player.UniqId != pMsg->UniqId)
                                    return;

                                Player.Direction = (Byte)pMsg->Direction;

                                Booth Booth = Booth.Create(Player, pMsg->X, pMsg->Y);
                                Booth.SendShow(Player);

                                Player.Send(MsgAction.Create(Player, Booth.UniqId, Action.CreateBooth));
                                break;
                            }
                        case Action.DestroyBooth:
                            {
                                Player Player = Client.User;
                                if (Player.UniqId != pMsg->UniqId)
                                    return;

                                if (Player.Booth != null)
                                    Player.Booth.Destroy();

                                World.AllMaps[Player.Map].AddEntity(Player);
                                Player.Screen.ChangeMap();
                                break;
                            }
                        case Action.TakeOff:
                            {
                                Player Player = Client.User;
                                Player.FlyEndTime = Environment.TickCount - 5000;
                                break;
                            }
                        case Action.QueryEquipment:
                            {
                                Player Player = Client.User;
                                if (Player.UniqId != pMsg->UniqId)
                                    return;

                                Player Target = null;
                                if (!World.AllPlayers.TryGetValue(pMsg->Param, out Target))
                                    return;

                                Item Item = null;

                                for (Byte i = 1; i < 10; i++)
                                {
                                    Item = Target.GetItemByPos(i);
                                    if (Item != null)
                                        Player.Send(MsgItemInfo.Create(Target.UniqId, Item, MsgItemInfo.Action.OtherPlayer_Equipement));
                                }
                                break;
                            }
                        case Action.AbortTransform:
                            {
                                Player Player = Client.User;
                                if (Player.UniqId != pMsg->UniqId)
                                    return;

                                Player.TransformEndTime = Environment.TickCount - 2000;
                                break;
                            }
                        case Action.QueryEnemyInfo:
                            {
                                Player Player = Client.User;
                                if (Player.UniqId != pMsg->UniqId)
                                    return;

                                if (!Player.Enemies.ContainsKey(pMsg->Param))
                                    return;

                                Player Enemy = null;
                                if (!World.AllPlayers.TryGetValue(pMsg->Param, out Enemy))
                                    return;

                                Player.Send(MsgFriendInfo.Create(Enemy));
                                break;
                            }
                        case Action.LoginCompleted:
                            {
                                Player Player = Client.User;
                                if (Player.UniqId != pMsg->UniqId)
                                    return;

                                break;
                            }
                        case Action.Jump:
                            {
                                UInt16 NewX = (UInt16)(pMsg->Param);
                                UInt16 NewY = (UInt16)(pMsg->Param >> 16);

                                Player Player = Client.User;
                                if (Player.UniqId != pMsg->UniqId)
                                    return;

                                if (Player.X != pMsg->X || Player.Y != pMsg->Y)
                                {
                                    Player.KickBack();
                                    return;
                                }

                                if (!Player.IsAlive())
                                {
                                    Player.SendSysMsg(Client.GetStr("STR_DIE"));
                                    Player.KickBack();
                                    return;
                                }

                                if (!MyMath.CanSee(pMsg->X, pMsg->Y, NewX, NewY, 17))
                                {
                                    Player.KickBack();
                                    return;
                                }

                                Map Map = null;
                                if (World.AllMaps.TryGetValue(Player.Map, out Map))
                                {
                                    if (!Map.IsValidPoint(NewX, NewY))
                                    {
                                        Player.SendSysMsg(Client.GetStr("STR_INVALID_COORDINATE"));
                                        Player.KickBack();
                                        return;
                                    }

                                    //The maximum elevation difference (between the character's initial position and the check tile's position) is 210
                                    if ((Int32)Map.GetHeight(NewX, NewY) - (Int32)Map.GetHeight(Player.X, Player.Y) > 210)
                                    {
                                        Database.Jail(Player.Name);

                                        Player.JailC++;
                                        Player.Move(6001, 28, 75);

                                        Program.Log("[CRIME] " + Player.Name + " has been sent to jail for using a wall jump hack!");
                                        World.BroadcastMsg(MsgTalk.Create("SYSTEM", "ALLUSERS", Player.Name + " has been sent to jail!", MsgTalk.Channel.GM, 0xFFFFFF));
                                        return;
                                    }
                                }

                                //Normal: 800, Cyclone: 400, Fly: 520, Fly + Cyclone: 275, DH: 250
                                //So, the tick shouldn't be lower than ~410 if the user doesn't use Cyclone or DH...
                                if (!Player.ContainsFlag(Player.Flag.Cyclone) && Player.TransformEndTime == 0
                                    && Environment.TickCount - Player.LastJumpTick < 410)
                                    Player.SpeedHack++;
                                Player.LastJumpTick = Environment.TickCount;

                                Player.PrevX = Player.X;
                                Player.PrevY = Player.Y;

                                Player.X = NewX;
                                Player.Y = NewY;
                                Player.Direction = (Byte)MyMath.GetDirectionCO(Player.PrevX, Player.PrevY, NewX, NewY);
                                Player.Action = (Int16)MsgAction.Emotion.StandBy;

                                Player.IsInBattle = false;
                                Player.MagicIntone = false;
                                Player.Mining = false;

                                Player.Send(Buffer);
                                Player.Screen.Move(Buffer);
                                break;
                            }
                        case Action.Ghost:
                            {
                                Player Player = Client.User;
                                if (Player.UniqId != pMsg->UniqId)
                                    return;

                                if (!Player.IsAlive())
                                {
                                    if (Player.Look / 10000000 != 98 && Player.Look / 10000000 != 99)
                                    {
                                        if (Player.Look % 10000 == 2001 || Player.Look % 10000 == 2002)
                                            Player.AddTransform(99);
                                        else
                                            Player.AddTransform(98);
                                        World.BroadcastRoomMsg(Player, MsgUserAttrib.Create(Player, Player.Look, MsgUserAttrib.Type.Look), true);
                                    }
                                }
                                break;
                            }
                        case Action.Synchro:
                            {
                                UInt16 ClientX = (UInt16)(pMsg->Param >> 16);
                                UInt16 ClientY = (UInt16)(pMsg->Param);

                                Player Player = Client.User;
                                if (Player.UniqId != pMsg->UniqId)
                                    return;

                                Player.X = ClientX;
                                Player.Y = ClientY;

                                Player.Send(MsgAction.Create(Player, pMsg->Param, Action.Jump));
                                World.BroadcastRoomMsg(Player, Buffer, false);
                                break;
                            }
                        case Action.QueryFriendInfo:
                            {
                                Player Player = Client.User;
                                if (Player.UniqId != pMsg->UniqId)
                                    return;

                                if (!Player.Friends.ContainsKey(pMsg->Param))
                                    return;

                                Player Friend = null;
                                if (!World.AllPlayers.TryGetValue(pMsg->Param, out Friend))
                                    return;

                                Player.Send(MsgFriendInfo.Create(Friend));
                                break;
                            }
                        case Action.ChangeFace:
                            {
                                Player Player = Client.User;
                                if (Player.UniqId != pMsg->UniqId)
                                    return;

                                if (Player.Money < 500)
                                {
                                    Player.SendSysMsg(Client.GetStr("STR_NOT_SO_MUCH_MONEY"));
                                    return;
                                }
                                Player.Money -= 500;
                                Player.Send(MsgUserAttrib.Create(Player, Player.Money, MsgUserAttrib.Type.Money));
                                Player.Look = (UInt32)(Player.Look - ((Int32)(Player.Look / 10000) * 10000) + (pMsg->Param * 10000));

                                World.BroadcastRoomMsg(Player, Buffer, true);

                                if (Player.Team != null)
                                    World.BroadcastTeamMsg(Player.Team, Buffer);
                                break;
                            }
                        case (Action)310:
                            {
                                Player Player = Client.User;
                                if (Player.UniqId != pMsg->UniqId)
                                    return;

                                Player Target = null;
                                if (!World.AllPlayers.TryGetValue(pMsg->Param, out Target))
                                    return;

                                //Player.Send(MsgPlayer.Create(Target));

                                Item Item = null;
                                for (Byte i = 1; i < 10; i++)
                                {
                                    Item = Target.GetItemByPos(i);
                                    if (Item != null)
                                        Player.Send(MsgItemInfoEx.Create(Target.UniqId, Item, 0, MsgItemInfoEx.Action.OtherPlayer_Equipement));
                                }

                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Msg[{0}], Action[{1}] not implemented yet!", pMsg->Header.Type, (Int16)pMsg->Action);
                                break;
                            }
                    }
                }
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }
    }
}
