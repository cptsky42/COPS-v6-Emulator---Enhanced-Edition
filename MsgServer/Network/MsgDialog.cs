// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2011 - 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using COServer.Script;
using COServer.Entities;

[assembly: InternalsVisibleTo("COServer.Network.Msg")]

namespace COServer.Network
{
    /// <summary>
    /// Message used for NPC's dialogs.
    /// </summary>
    public class MsgDialog : Msg
    {
        /// <summary>
        /// This is a "constant" that the child must override.
        /// It is the type of the message as specified in NetworkDef.cs file.
        /// </summary>
        protected override UInt16 _TYPE { get { return MSG_DIALOG; } }

        public enum Action : byte
        {
            /// <summary>
            /// Text of the dialog.
            /// </summary>
            Text = 1,
            /// <summary>
            /// Options (with next task ID).
            /// </summary>
            Link = 2,
            /// <summary>
            /// Input (with next task ID).
            /// </summary>
            Edit = 3,
            /// <summary>
            /// Pic of the NPC.
            /// </summary>
            Pic = 4,				// data: npc face
            ListLine = 5,
            Create = 100,			// idxTask: default task
            Answer = 101,			// to server
            TaskId = 102,			// to server, launch task id by interface
        };

        //--------------- Internal Members ---------------
        private Int32 __TaskId = 0;
        private UInt16 __Data = 0;
        private Byte __IdxTask = 0;
        private Action __Action = (Action)0;
        private StringPacker __StrPacker = null;
        //------------------------------------------------

        public Int32 TaskId
        {
            get { return __TaskId; }
            set { __TaskId = value; WriteInt32(4, value); }
        }

        public UInt16 PosX
        {
            get { return (UInt16)((__TaskId >> 16) & 0x0000FFFFU); }
            set { __TaskId = (Int32)((value << 16) | (__TaskId & 0x0000FFFFU)); WriteUInt16(4, value); }
        }

        public UInt16 PosY
        {
            get { return (UInt16)(__TaskId & 0x0000FFFFU); }
            set { __TaskId = (Int32)((__TaskId & 0xFFFF0000U) | (value)); WriteUInt16(6, value); }
        }

        /// <summary>
        /// Data of the action (e.g. Pic) 
        /// </summary>
        public UInt16 Data
        {
            get { return __Data; }
            set { __Data = value; WriteUInt16(8, value); }
        }

        /// <summary>
        /// Task's index (0 is default, 255 is end).
        /// </summary>
        public Byte IdxTask
        {
            get { return __IdxTask; }
            set { __IdxTask = value; mBuf[10] = value; }
        }

        /// <summary>
        /// Action Id.
        /// </summary>
        public Action _Action
        {
            get { return __Action; }
            set { __Action = value; mBuf[11] = (Byte)value; }
        }

        /// <summary>
        /// Text
        /// </summary>
        public String Text
        {
            get { String text = ""; __StrPacker.GetString(out text, 0); return text; }
            set { __StrPacker.AddString(value); }
        }

        /// <summary>
        /// Create a message object from the specified buffer.
        /// </summary>
        /// <param name="aBuf">The buffer containing the message.</param>
        /// <param name="aIndex">The index where the message is starting in the buffer.</param>
        /// <param name="aLength">The length of the message.</param>
        internal MsgDialog(Byte[] aBuf, int aIndex, int aLength)
            : base(aBuf, aIndex, aLength)
        {
            __TaskId = BitConverter.ToInt32(mBuf, 4);
            __Data = BitConverter.ToUInt16(mBuf, 8);
            __IdxTask = mBuf[10];
            __Action = (Action)mBuf[11];
            __StrPacker = new StringPacker(this, 12);
        }

        public MsgDialog(String aText, UInt16 aData, Byte aIdxTask, Action aAction)
            : base((UInt16)(14 + aText.Length))
        {
            TaskId = 0;
            Data = aData;
            IdxTask = aIdxTask;
            _Action = aAction;

            __StrPacker = new StringPacker(this, 12);
            __StrPacker.AddString(aText);
        }

        public MsgDialog(UInt16 aPosX, UInt16 aPosY, UInt16 aPic, Byte aIdxTask, Action aAction)
            : base(14)
        {
            PosX = aPosX;
            PosY = aPosY;
            Data = aPic;
            IdxTask = aIdxTask;
            _Action = aAction;

            __StrPacker = new StringPacker(this, 12);
        }

        public MsgDialog(Byte aIdxTask, Action aAction)
            : base(14)
        {
            TaskId = 0;
            Data = 9;
            IdxTask = aIdxTask;
            _Action = aAction;

            __StrPacker = new StringPacker(this, 12);
        }

        /// <summary>
        /// Process the message for the specified client.
        /// </summary>
        /// <param name="aClient">The client who sent the message.</param>
        public override void Process(Client aClient)
        {
            try
            {
                if (aClient == null)
                    return;

                Player Player = aClient.Player;
                aClient.TaskData = Text;

                switch (_Action)
                {
                    case Action.Answer:
                        {
                            if (!Player.IsAlive())
                            {
                                Player.SendSysMsg(StrRes.STR_DIE);
                                return;
                            }

                            if (aClient.CurTask != null)
                            {
                                if (IdxTask != 255)
                                    aClient.CurTask.Execute(aClient, IdxTask);
                                else
                                    aClient.CurTask = null;
                                return;
                            }

                            //case 380: //GérantDeGuilde
                            //    {
                            //        if (IdxTask == 1)
                            //        {
                            //            if (Player.Syndicate == null)
                            //            {
                            //                Position += ScriptHandler.SendText("Vous devez etre dans une guilde pour déclarer la guerre!", aClient, ref Data, Position);
                            //                Position += ScriptHandler.SendOption(255, "Ok, je vois.", aClient, ref Data, Position);
                            //                Position += ScriptHandler.SendFace(123, aClient, ref Data, Position);
                            //                Position += ScriptHandler.SendEnd(aClient, ref Data, Position);
                            //                ScriptHandler.SendData(aClient, Data, Position);
                            //                return;
                            //            }

                            //            Syndicate.Member Member = Player.Syndicate.GetMemberInfo(Player.UniqId);
                            //            if (Member == null || Member.Rank != 100)
                            //            {
                            //                Position += ScriptHandler.SendText("Vous devez etre chef d'une guilde pour déclarer la guerre!", aClient, ref Data, Position);
                            //                Position += ScriptHandler.SendOption(255, "Ok, je vois.", aClient, ref Data, Position);
                            //                Position += ScriptHandler.SendFace(123, aClient, ref Data, Position);
                            //                Position += ScriptHandler.SendEnd(aClient, ref Data, Position);
                            //                ScriptHandler.SendData(aClient, Data, Position);
                            //                return;
                            //            }

                            //            GameMap Map = null;
                            //            if (!MapManager.TryGetMap(1011, out Map))
                            //                return;

                            //            if (Map == null || Map.InWar)
                            //            {
                            //                Position += ScriptHandler.SendText("Il y a déjà une guerre dans cette région!", aClient, ref Data, Position);
                            //                Position += ScriptHandler.SendOption(255, "Ok, je vois.", aClient, ref Data, Position);
                            //                Position += ScriptHandler.SendFace(123, aClient, ref Data, Position);
                            //                Position += ScriptHandler.SendEnd(aClient, ref Data, Position);
                            //                ScriptHandler.SendData(aClient, Data, Position);
                            //                return;
                            //            }

                            //            new Games.CityWar(Map);
                            //            World.BroadcastMsg(new MsgTalk("SYSTEM", "ALLUSERS", Player.Name + " des " + Player.Syndicate.Name + " a déclaré la guerre pour prendre la Foret!", Channel.GM, Color.White));
                            //        }
                            //        else if (IdxTask == 2)
                            //        {
                            //            if (Player.Syndicate == null)
                            //            {
                            //                Position += ScriptHandler.SendText("Vous devez etre dans une guilde pour déclarer la guerre!", aClient, ref Data, Position);
                            //                Position += ScriptHandler.SendOption(255, "Ok, je vois.", aClient, ref Data, Position);
                            //                Position += ScriptHandler.SendFace(123, aClient, ref Data, Position);
                            //                Position += ScriptHandler.SendEnd(aClient, ref Data, Position);
                            //                ScriptHandler.SendData(aClient, Data, Position);
                            //                return;
                            //            }

                            //            Syndicate.Member Member = Player.Syndicate.GetMemberInfo(Player.UniqId);
                            //            if (Member == null || Member.Rank != 100)
                            //            {
                            //                Position += ScriptHandler.SendText("Vous devez etre chef d'une guilde pour déclarer la guerre!", aClient, ref Data, Position);
                            //                Position += ScriptHandler.SendOption(255, "Ok, je vois.", aClient, ref Data, Position);
                            //                Position += ScriptHandler.SendFace(123, aClient, ref Data, Position);
                            //                Position += ScriptHandler.SendEnd(aClient, ref Data, Position);
                            //                ScriptHandler.SendData(aClient, Data, Position);
                            //                return;
                            //            }

                            //            GameMap Map = null;
                            //            if (!MapManager.TryGetMap(1020, out Map))
                            //                return;

                            //            if (Map == null || Map.InWar)
                            //            {
                            //                Position += ScriptHandler.SendText("Il y a déjà une guerre dans cette région!", aClient, ref Data, Position);
                            //                Position += ScriptHandler.SendOption(255, "Ok, je vois.", aClient, ref Data, Position);
                            //                Position += ScriptHandler.SendFace(123, aClient, ref Data, Position);
                            //                Position += ScriptHandler.SendEnd(aClient, ref Data, Position);
                            //                ScriptHandler.SendData(aClient, Data, Position);
                            //                return;
                            //            }

                            //            new Games.CityWar(Map);
                            //            World.BroadcastMsg(new MsgTalk("SYSTEM", "ALLUSERS", Player.Name + " des " + Player.Syndicate.Name + " a déclaré la guerre pour prendre le Canyon", Channel.GM, Color.White));
                            //        }
                            //        else if (IdxTask == 3)
                            //        {
                            //            if (Player.Syndicate == null)
                            //            {
                            //                Position += ScriptHandler.SendText("Vous devez etre dans une guilde pour déclarer la guerre!", aClient, ref Data, Position);
                            //                Position += ScriptHandler.SendOption(255, "Ok, je vois.", aClient, ref Data, Position);
                            //                Position += ScriptHandler.SendFace(123, aClient, ref Data, Position);
                            //                Position += ScriptHandler.SendEnd(aClient, ref Data, Position);
                            //                ScriptHandler.SendData(aClient, Data, Position);
                            //                return;
                            //            }

                            //            Syndicate.Member Member = Player.Syndicate.GetMemberInfo(Player.UniqId);
                            //            if (Member == null || Member.Rank != 100)
                            //            {
                            //                Position += ScriptHandler.SendText("Vous devez etre chef d'une guilde pour déclarer la guerre!", aClient, ref Data, Position);
                            //                Position += ScriptHandler.SendOption(255, "Ok, je vois.", aClient, ref Data, Position);
                            //                Position += ScriptHandler.SendFace(123, aClient, ref Data, Position);
                            //                Position += ScriptHandler.SendEnd(aClient, ref Data, Position);
                            //                ScriptHandler.SendData(aClient, Data, Position);
                            //                return;
                            //            }

                            //            GameMap Map = null;
                            //            if (!MapManager.TryGetMap(1000, out Map))
                            //                return;

                            //            if (Map == null || Map.InWar)
                            //            {
                            //                Position += ScriptHandler.SendText("Il y a déjà une guerre dans cette région!", aClient, ref Data, Position);
                            //                Position += ScriptHandler.SendOption(255, "Ok, je vois.", aClient, ref Data, Position);
                            //                Position += ScriptHandler.SendFace(123, aClient, ref Data, Position);
                            //                Position += ScriptHandler.SendEnd(aClient, ref Data, Position);
                            //                ScriptHandler.SendData(aClient, Data, Position);
                            //                return;
                            //            }

                            //            new Games.CityWar(Map);
                            //            World.BroadcastMsg(new MsgTalk("SYSTEM", "ALLUSERS", Player.Name + " des " + Player.Syndicate.Name + " a déclaré la guerre pour prendre le Désert!", Channel.GM, Color.White));
                            //        }
                            //        else if (IdxTask == 4)
                            //        {
                            //            if (Player.Syndicate == null)
                            //            {
                            //                Position += ScriptHandler.SendText("Vous devez etre dans une guilde pour déclarer la guerre!", aClient, ref Data, Position);
                            //                Position += ScriptHandler.SendOption(255, "Ok, je vois.", aClient, ref Data, Position);
                            //                Position += ScriptHandler.SendFace(123, aClient, ref Data, Position);
                            //                Position += ScriptHandler.SendEnd(aClient, ref Data, Position);
                            //                ScriptHandler.SendData(aClient, Data, Position);
                            //                return;
                            //            }

                            //            Syndicate.Member Member = Player.Syndicate.GetMemberInfo(Player.UniqId);
                            //            if (Member == null || Member.Rank != 100)
                            //            {
                            //                Position += ScriptHandler.SendText("Vous devez etre chef d'une guilde pour déclarer la guerre!", aClient, ref Data, Position);
                            //                Position += ScriptHandler.SendOption(255, "Ok, je vois.", aClient, ref Data, Position);
                            //                Position += ScriptHandler.SendFace(123, aClient, ref Data, Position);
                            //                Position += ScriptHandler.SendEnd(aClient, ref Data, Position);
                            //                ScriptHandler.SendData(aClient, Data, Position);
                            //                return;
                            //            }

                            //            GameMap Map = null;
                            //            if (!MapManager.TryGetMap(1015, out Map))
                            //                return;

                            //            if (Map == null || Map.InWar)
                            //            {
                            //                Position += ScriptHandler.SendText("Il y a déjà une guerre dans cette région!", aClient, ref Data, Position);
                            //                Position += ScriptHandler.SendOption(255, "Ok, je vois.", aClient, ref Data, Position);
                            //                Position += ScriptHandler.SendFace(123, aClient, ref Data, Position);
                            //                Position += ScriptHandler.SendEnd(aClient, ref Data, Position);
                            //                ScriptHandler.SendData(aClient, Data, Position);
                            //                return;
                            //            }

                            //            new Games.CityWar(Map);
                            //            World.BroadcastMsg(new MsgTalk("SYSTEM", "ALLUSERS", Player.Name + " des " + Player.Syndicate.Name + " a déclaré la guerre pour prendre les Iles!", Channel.GM, Color.White));
                            //        }
                            //        break;
                            //    }

                            //case 10003: //Mandarin
                            //    {
                            //        else if (IdxTask == 203)
                            //        {
                            //            if (Player.Syndicate != null)
                            //            {
                            //                if (Player.UniqId != Player.Syndicate.Leader.UniqId)
                            //                {
                            //                    Position += ScriptHandler.SendText("Seul le chef de guilde peut commettre un chef.", aClient, ref Data, Position);
                            //                    Position += ScriptHandler.SendOption(255, "Ok.", aClient, ref Data, Position);
                            //                    Position += ScriptHandler.SendFace(7, aClient, ref Data, Position);
                            //                    Position += ScriptHandler.SendEnd(aClient, ref Data, Position);
                            //                    ScriptHandler.SendData(aClient, Data, Position);
                            //                    return;
                            //                }

                            //                Syndicate Syn = Player.Syndicate;
                            //                Syndicate.Member Leader = Syn.Leader;
                            //                Syndicate.Member Member = Syn.GetMemberInfo(Text);
                            //                Player Target = null;

                            //                if (Member != null && World.AllPlayers.TryGetValue(Member.UniqId, out Target))
                            //                {
                            //                    lock (Syn.Members)
                            //                    {
                            //                        Syn.Members.Remove(Member.UniqId);

                            //                        Leader.Rank = 50;
                            //                        Member.Rank = 100;

                            //                        Syn.Members.Add(Leader.UniqId, Leader);
                            //                        Syn.Leader = Member;
                            //                    }

                            //                    Player.Send(new MsgSynAttrInfo(Player.UniqId, Syn));
                            //                    Target.Send(new MsgSynAttrInfo(Target.UniqId, Syn));

                            //                    World.BroadcastRoomMsg(Player, new MsgPlayer(Player), true);
                            //                    World.BroadcastRoomMsg(Target, new MsgPlayer(Target), true);

                            //                    Database.Save(Player, true);
                            //                    Database.Save(Target, true);
                            //                }
                            //                else
                            //                {
                            //                    Position += ScriptHandler.SendText("Le joueur doit être en-ligne et dans votre guilde.", aClient, ref Data, Position);
                            //                    Position += ScriptHandler.SendOption(255, "Ok.", aClient, ref Data, Position);
                            //                    Position += ScriptHandler.SendFace(7, aClient, ref Data, Position);
                            //                    Position += ScriptHandler.SendEnd(aClient, ref Data, Position);
                            //                    ScriptHandler.SendData(aClient, Data, Position);
                            //                    return;
                            //                }
                            //            }
                            //        }
                            //        else if (IdxTask == 204)
                            //        {
                            //            if (Player.Syndicate != null)
                            //            {
                            //                if (Player.UniqId != Player.Syndicate.Leader.UniqId)
                            //                {
                            //                    Position += ScriptHandler.SendText("Seul le chef de guilde peut commettre un chef.", aClient, ref Data, Position);
                            //                    Position += ScriptHandler.SendOption(255, "Ok.", aClient, ref Data, Position);
                            //                    Position += ScriptHandler.SendFace(7, aClient, ref Data, Position);
                            //                    Position += ScriptHandler.SendEnd(aClient, ref Data, Position);
                            //                    ScriptHandler.SendData(aClient, Data, Position);
                            //                    return;
                            //                }

                            //                Syndicate Syn = Player.Syndicate;
                            //                Syndicate.Member Member = Syn.GetMemberInfo(Text);
                            //                Player Target = null;

                            //                if (Member != null && World.AllPlayers.TryGetValue(Member.UniqId, out Target))
                            //                {
                            //                    if (Member.Rank == 100)
                            //                        return;

                            //                    Member.Rank = 90;
                            //                    Target.Send(new MsgSynAttrInfo(Target.UniqId, Syn));
                            //                    World.BroadcastRoomMsg(Target, new MsgPlayer(Target), true);

                            //                    Database.Save(Target, true);
                            //                }
                            //                else
                            //                {
                            //                    Position += ScriptHandler.SendText("Le joueur doit être en-ligne et dans votre guilde.", aClient, ref Data, Position);
                            //                    Position += ScriptHandler.SendOption(255, "Ok.", aClient, ref Data, Position);
                            //                    Position += ScriptHandler.SendFace(7, aClient, ref Data, Position);
                            //                    Position += ScriptHandler.SendEnd(aClient, ref Data, Position);
                            //                    ScriptHandler.SendData(aClient, Data, Position);
                            //                    return;
                            //                }
                            //            }
                            //        }
                            //        else if (IdxTask == 205)
                            //        {
                            //            if (Player.Syndicate != null)
                            //            {
                            //                if (Player.UniqId != Player.Syndicate.Leader.UniqId)
                            //                {
                            //                    Position += ScriptHandler.SendText("Seul le chef de guilde peut commettre un chef.", aClient, ref Data, Position);
                            //                    Position += ScriptHandler.SendOption(255, "Ok.", aClient, ref Data, Position);
                            //                    Position += ScriptHandler.SendFace(7, aClient, ref Data, Position);
                            //                    Position += ScriptHandler.SendEnd(aClient, ref Data, Position);
                            //                    ScriptHandler.SendData(aClient, Data, Position);
                            //                    return;
                            //                }

                            //                Syndicate Syn = Player.Syndicate;
                            //                Syndicate.Member Member = Syn.GetMemberInfo(Text);
                            //                Player Target = null;

                            //                if (Member != null && World.AllPlayers.TryGetValue(Member.UniqId, out Target))
                            //                {
                            //                    if (Member.Rank == 50 || Member.Rank == 100)
                            //                        return;

                            //                    Member.Rank = 50;
                            //                    Target.Send(new MsgSynAttrInfo(Target.UniqId, Syn));
                            //                    World.BroadcastRoomMsg(Target, new MsgPlayer(Target), true);

                            //                    Database.Save(Target, true);
                            //                }
                            //                else
                            //                {
                            //                    Position += ScriptHandler.SendText("Le joueur doit être en-ligne et dans votre guilde.", aClient, ref Data, Position);
                            //                    Position += ScriptHandler.SendOption(255, "Ok.", aClient, ref Data, Position);
                            //                    Position += ScriptHandler.SendFace(7, aClient, ref Data, Position);
                            //                    Position += ScriptHandler.SendEnd(aClient, ref Data, Position);
                            //                    ScriptHandler.SendData(aClient, Data, Position);
                            //                    return;
                            //                }
                            //            }
                            //        }
                            //        else if (IdxTask == 206)
                            //        {
                            //            lock (World.AllSyndicates)
                            //            {
                            //                foreach (Syndicate Syn in World.AllSyndicates.Values)
                            //                {
                            //                    if (Syn.Name != Text)
                            //                        continue;

                            //                    Position += ScriptHandler.SendText("Le nom de guilde: " + Syn.Name, aClient, ref Data, Position);
                            //                    Position += ScriptHandler.SendText("\nLe chef de guilde: " + Syn.Leader.Name, aClient, ref Data, Position);
                            //                    Position += ScriptHandler.SendText("\nNombre de membres: " + Syn.Members.Count, aClient, ref Data, Position);
                            //                    Position += ScriptHandler.SendText("\nLe fond de guilde: " + Syn.Money, aClient, ref Data, Position);
                            //                    Position += ScriptHandler.SendOption(255, "Ok.", aClient, ref Data, Position);
                            //                    Position += ScriptHandler.SendFace(7, aClient, ref Data, Position);
                            //                    Position += ScriptHandler.SendEnd(aClient, ref Data, Position);
                            //                    ScriptHandler.SendData(aClient, Data, Position);
                            //                    return;
                            //                }
                            //            }

                            //            Position += ScriptHandler.SendText("Cette guilde n'existe pas!", aClient, ref Data, Position);
                            //            Position += ScriptHandler.SendOption(255, "Ok.", aClient, ref Data, Position);
                            //            Position += ScriptHandler.SendFace(7, aClient, ref Data, Position);
                            //            Position += ScriptHandler.SendEnd(aClient, ref Data, Position);
                            //            ScriptHandler.SendData(aClient, Data, Position);
                            //        }
                            //        else if (IdxTask == 211)
                            //        {
                            //            if (Player.Syndicate == null)
                            //                return;

                            //            if (Player.Syndicate.Leader.UniqId != Player.UniqId)
                            //            {
                            //                Position += ScriptHandler.SendText("Seul le chef de guilde peut exclure un membre.", aClient, ref Data, Position);
                            //                Position += ScriptHandler.SendOption(255, "Ok.", aClient, ref Data, Position);
                            //                Position += ScriptHandler.SendFace(7, aClient, ref Data, Position);
                            //                Position += ScriptHandler.SendEnd(aClient, ref Data, Position);
                            //                ScriptHandler.SendData(aClient, Data, Position);
                            //                return;
                            //            }

                            //            if (Player.Name == Text)
                            //                return;

                            //            Syndicate.Member Member = Player.Syndicate.GetMemberInfo(Text);
                            //            if (Member == null)
                            //            {
                            //                Position += ScriptHandler.SendText("Ce membre n'existe pas!", aClient, ref Data, Position);
                            //                Position += ScriptHandler.SendOption(255, "Ok.", aClient, ref Data, Position);
                            //                Position += ScriptHandler.SendFace(7, aClient, ref Data, Position);
                            //                Position += ScriptHandler.SendEnd(aClient, ref Data, Position);
                            //                ScriptHandler.SendData(aClient, Data, Position);
                            //                return;
                            //            }

                            //            Player.Syndicate.DelMember(Member, true);
                            //        }
                            //        break;
                            //    

                            break;
                        }
                    case Action.TaskId:
                        {
                            if (TaskId == 31100) //Kick Out
                            {
                                if (Player.Syndicate == null)
                                    return;

                                if (Player.Syndicate.Leader.Id != Player.UniqId)
                                    return;

                                if (Player.Name == Text)
                                    return;

                                Syndicate.Member Member = Player.Syndicate.GetMemberInfo(Text);
                                if (Member == null)
                                    return;

                                Player.Syndicate.DelMember(Member, true);
                            }
                            break;
                        }
                    default:
                        {
                            sLogger.Error("Action {0} is not implemented for MsgDialog.", (UInt16)_Action);
                            break;
                        }
                }
            }
            catch (Exception exc) { sLogger.Error(exc); }
        }
    }
}
