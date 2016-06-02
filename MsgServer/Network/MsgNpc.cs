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
using COServer.Script;

[assembly: InternalsVisibleTo("COServer.Network.Msg")]

namespace COServer.Network
{
    public class MsgNpc : Msg
    {
        /// <summary>
        /// This is a "constant" that the child must override.
        /// It is the type of the message as specified in NetworkDef.cs file.
        /// </summary>
        protected override UInt16 _TYPE { get { return MSG_NPC; } }

        public enum Action
        {
            BeActived = 0,	    			// to server		// ´¥·¢
            AddNpc = 1,						// no use
            LeaveMap = 2,					// to client		// É¾³ý
            DelNpc = 3,						// to server
            ChangePos = 4,					// to client/server
            LayNpc = 5,						// to client(id=region,data=lookface), answer MsgNpcInfo(CMsgPlayer for statuary)
        };

        //--------------- Internal Members ---------------
        private Int32 __Id = 0;
        private UInt32 __Data = 0;
        private Action __Action = (Action)0;
        private UInt16 __Type = 0;
        //------------------------------------------------

        public Int32 Id
        {
            get { return __Id; }
            set { __Id = value; WriteInt32(4, value); }
        }

        public UInt32 Data
        {
            get { return __Data; }
            set { __Data = value; WriteUInt32(8, value); }
        }

        public UInt16 PosX
        {
            get { return (UInt16)((__Data >> 16) & 0x0000FFFFU); }
            set { __Data = (UInt32)((value << 16) | (__Data & 0x0000FFFFU)); WriteUInt16(8, value); }
        }

        public UInt16 PosY
        {
            get { return (UInt16)(__Data & 0x0000FFFFU); }
            set { __Data = (UInt32)((__Data & 0xFFFF0000U) | (value)); WriteUInt16(10, value); }
        }

        /// <summary>
        /// Action Id.
        /// </summary>
        public Action _Action
        {
            get { return __Action; }
            set { __Action = value; WriteUInt16(12, (UInt16)value); }
        }

        public new UInt16 Type
        {
            get { return __Type; }
            set { __Type = value; WriteUInt16(14, value); }
        }

        /// <summary>
        /// Create a message object from the specified buffer.
        /// </summary>
        /// <param name="aBuf">The buffer containing the message.</param>
        /// <param name="aIndex">The index where the message is starting in the buffer.</param>
        /// <param name="aLength">The length of the message.</param>
        internal MsgNpc(Byte[] aBuf, int aIndex, int aLength)
            : base(aBuf, aIndex, aLength)
        {
            __Id = BitConverter.ToInt32(mBuf, 4);
            __Data = BitConverter.ToUInt32(mBuf, 8);
            __Action = (Action)BitConverter.ToUInt16(mBuf, 12);
            __Type = BitConverter.ToUInt16(mBuf, 14);
        }

        /// <summary>
        /// Process the message for the specified client.
        /// </summary>
        /// <param name="aClient">The client who sent the message.</param>
        public override void Process(Client aClient)
        {
            Player player = aClient.Player;
            aClient.TaskData = "";

            switch (_Action)
            {
                case Action.BeActived:
                    {
                        if (!player.IsAlive())
                        {
                            player.SendSysMsg(StrRes.STR_DIE);
                            return;
                        }

                        Entity npc = null;
                        if (World.AllNPCs.ContainsKey(Id))
                            npc = World.AllNPCs[Id];

                        if (World.AllTerrainNPCs.ContainsKey(Id))
                            npc = World.AllTerrainNPCs[Id];

                        if (npc == null)
                            return;

                        if (player.Map != npc.Map)
                            return;

                        if (!MyMath.CanSee(player.X, player.Y, npc.X, npc.Y, 17))
                            return;

                        if (Program.Debug)
                            player.SendSysMsg("Msg[{0}], Param0[{1}], Param1[{2}], Action[{3}]",
                                _TYPE, Id, Data, (UInt16)_Action);

                        if (TaskHandler.AllTasks.ContainsKey(Id))
                        {
                            var task = TaskHandler.AllTasks[Id];
                            aClient.CurTask = task;

                            task.Execute(aClient, (Byte)0);
                            return;
                        }

                        //case 380: //GérantDeGuilde
                        //    {
                        //        position += ScriptHandler.SendText("I'm in charge of all wars. Since the last war which has devastated this world, we have decided to control those wars.", aClient, ref data, position);
                        //        position += ScriptHandler.SendText(" Guild Leaders can declare the war against the guild which control a city. During the next hour, all guilds", aClient, ref data, position);
                        //        position += ScriptHandler.SendText(" can fight to take the city. At the end, the guild who has successfully sieged the city can control it.", aClient, ref data, position);
                        //        position += ScriptHandler.SendText(" The guild controlling the region will have royalties. Which region would you like to take ?", aClient, ref data, position);
                        //        position += ScriptHandler.SendOption(1, "The Forest", aClient, ref data, position);
                        //        position += ScriptHandler.SendOption(2, "The Canyon", aClient, ref data, position);
                        //        position += ScriptHandler.SendOption(3, "The Desert", aClient, ref data, position);
                        //        position += ScriptHandler.SendOption(4, "The Islands", aClient, ref data, position);
                        //        position += ScriptHandler.SendFace(123, aClient, ref data, position);
                        //        position += ScriptHandler.SendEnd(aClient, ref data, position);
                        //        ScriptHandler.SendData(aClient, data, position);
                        //        break;
                        //    }

                        break;
                    }
                default:
                    {
                        sLogger.Error("Action {0} is not implemented for MsgNpc.", (UInt16)_Action);
                        break;
                    }
            }
        }
    }
}
