// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2010 - 2015 Jean-Philippe Boivin
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
    public class MsgMapItem : Msg
    {
        /// <summary>
        /// This is a "constant" that the child must override.
        /// It is the type of the message as specified in NetworkDef.cs file.
        /// </summary>
        protected override UInt16 _TYPE { get { return MSG_MAPITEM; } }

        public enum Action
        {
            Create = 1,			// to client
            Delete = 2,			// to client
            Pick = 3,			// to server, client: perform action of pick

            CastTrap = 10,		// to client
            SynchroTrap = 11,		// to client
            DropTrap = 12,		// to client, id=trap_id
        };

        //--------------- Internal Members ---------------
        private Int32 __Id = 0;
        private Int32 __Type = 0;
        private UInt32 __Look = 0;
        private UInt16 __PosX = 0;
        private UInt16 __PosY = 0;
        private Action __Action = (Action)0;
        //------------------------------------------------

        public Int32 Id
        {
            get { return __Id; }
            set { __Id = value; WriteInt32(4, value); }
        }

        public new Int32 Type
        {
            get { return __Type; }
            set { __Type = value; __Look = (UInt32)value; WriteInt32(8, value); }
        }

        public UInt32 Look
        {
            get { return __Look; }
            set { __Look = value; __Type = (Int32)value; WriteUInt32(8, value); }
        }

        public UInt16 PosX
        {
            get { return __PosX; }
            set { __PosX = value; WriteUInt16(12, value); }
        }

        public UInt16 PosY
        {
            get { return __PosY; }
            set { __PosY = value; WriteUInt16(14, value); }
        }

        /// <summary>
        /// Action ID.
        /// </summary>
        public Action _Action
        {
            get { return __Action; }
            set { __Action = value; WriteUInt32(16, (UInt32)value); }
        }

        /// <summary>
        /// Create a message object from the specified buffer.
        /// </summary>
        /// <param name="aBuf">The buffer containing the message.</param>
        /// <param name="aIndex">The index where the message is starting in the buffer.</param>
        /// <param name="aLength">The length of the message.</param>
        internal MsgMapItem(Byte[] aBuf, int aIndex, int aLength)
            : base(aBuf, aIndex, aLength)
        {
            __Id = BitConverter.ToInt32(mBuf, 4);
            __Type = BitConverter.ToInt32(mBuf, 8);
            __Look = BitConverter.ToUInt32(mBuf, 8);
            __PosX = BitConverter.ToUInt16(mBuf, 12);
            __PosY = BitConverter.ToUInt16(mBuf, 14);
            __Action = (Action)BitConverter.ToUInt32(mBuf, 16);
        }

        public MsgMapItem(FloorItem aItem, Action aAction)
            : base(20)
        {
            Id = aItem.Id;
            Type = aItem.Item.Type;
            PosX = aItem.X;
            PosY = aItem.Y;
            _Action = aAction;
        }

        /// <summary>
        /// Process the message for the specified client.
        /// </summary>
        /// <param name="aClient">The client who sent the message.</param>
        public override void Process(Client aClient)
        {
            if (aClient == null)
                return;

            switch (_Action)
            {
                case Action.Pick:
                    {
                        if (!World.AllFloorItems.ContainsKey(Id))
                            return;

                        FloorItem floorItem = World.AllFloorItems[Id];
                        Player player = aClient.Player;

                        if (floorItem.X != PosX || floorItem.Y != PosY)
                            return;

                        if (MyMath.GetDistance(player.X, player.Y, PosX, PosY) > 0)
                        {
                            player.SendSysMsg(StrRes.STR_FAR_CANNOT_PICK);
                            return;
                        }

                        if (!player.IsAlive())
                            return;

                        if (floorItem.Money == 0 && player.ItemInInventory() >= 40)
                        {
                            player.SendSysMsg(StrRes.STR_FULL_CANNOT_PICK);
                            return;
                        }

                        if (floorItem.OwnerUID != 0)
                        {
                            if (floorItem.OwnerUID != player.UniqId)
                            {
                                if ((DateTime.UtcNow - floorItem.DroppedTime).TotalSeconds < 10)
                                {
                                    if (player.Team != null && player.Team.IsTeamMember(floorItem.OwnerUID))
                                    {
                                        if (floorItem.Money > 0 && player.Team.MoneyForbidden)
                                        {
                                            player.SendSysMsg(StrRes.STR_OTHERS_ITEM);
                                            return;
                                        }
                                        else
                                        {
                                            if (player.Team.ItemForbidden)
                                            {
                                                player.SendSysMsg(StrRes.STR_OTHERS_ITEM);
                                                return;
                                            }
                                            else if (floorItem.Item.Type == 1088000 || floorItem.Item.Type == 1088001) //DB || Met
                                            {
                                                player.SendSysMsg(StrRes.STR_OTHERS_ITEM);
                                                return;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        player.SendSysMsg(StrRes.STR_OTHERS_ITEM);
                                        return;
                                    }
                                }
                            }
                        }

                        if (floorItem.Money > 0)
                        {
                            if (player.Money + floorItem.Money > Player._MAX_MONEYLIMIT)
                            {
                                player.SendSysMsg(StrRes.STR_TOOMUCH_MONEY);
                                return;
                            }

                            player.Money += floorItem.Money;
                            player.Send(new MsgUserAttrib(player, player.Money, MsgUserAttrib.AttributeType.Money));

                            if (floorItem.Money > 1000)
                            {
                                player.Send(new Msg1029(floorItem.Money));
                                World.BroadcastRoomMsg(player, new MsgAction(player, floorItem.Money, MsgAction.Action.GetMoney), true);
                            }

                            player.SendSysMsg(StrRes.STR_PICK_MONEY, floorItem.Money);
                        }
                        else
                        {
                            player.AddItem(floorItem.Item, true);
                            player.SendSysMsg(StrRes.STR_GOT_ITEM, floorItem.Item.Name);

                            floorItem.Item.Save();
                        }

                        World.BroadcastRoomMsg(player, this, false);
                        floorItem.Destroy(floorItem.Money > 0);
                        break;
                    }
                default:
                    {
                        sLogger.Error("Action {0} is not implemented for MsgMapItem.", (UInt32)_Action);
                        break;
                    }
            }
        }
    }
}
