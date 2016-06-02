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
    public class MsgPackage : Msg
    {
        /// <summary>
        /// This is a "constant" that the child must override.
        /// It is the type of the message as specified in NetworkDef.cs file.
        /// </summary>
        protected override UInt16 _TYPE { get { return MSG_PACKAGE; } }

        public enum Action
        {
            QueryList = 0,
            CheckIn = 1,
            CheckOut = 2,
            QueryList2 = 3,
        }

        //--------------- Internal Members ---------------
        private Int32 __Id = 0;
        private Action __Action = (Action)0;
        private PackageType __Type = PackageType.None;
        private Int32 __ItemId = 0; // to client/server: itemid, to client: size
        //------------------------------------------------

        public Int32 Id
        {
            get { return __Id; }
            set { __Id = value; WriteInt32(4, value); }
        }

        public Action _Action
        {
            get { return __Action; }
            set { __Action = value; mBuf[8] = (Byte)value; }
        }

        public PackageType Type
        {
            get { return __Type; }
            set { __Type = value; mBuf[9] = (Byte)value; }
        }

        public Int32 ItemId
        {
            get { return __ItemId; }
            set { __ItemId = value; WriteInt32(12, value); }
        }

        /// <summary>
        /// Create a message object from the specified buffer.
        /// </summary>
        /// <param name="aBuf">The buffer containing the message.</param>
        /// <param name="aIndex">The index where the message is starting in the buffer.</param>
        /// <param name="aLength">The length of the message.</param>
        internal MsgPackage(Byte[] aBuf, int aIndex, int aLength)
            : base(aBuf, aIndex, aLength)
        {
            __Id = BitConverter.ToInt32(mBuf, 4);
            __Action = (Action)mBuf[8];
            __Type = (PackageType)mBuf[9];
            __ItemId = BitConverter.ToInt32(mBuf, 12);
        }

        public MsgPackage(Int32 aId, Action aAction, PackageType aType, Int32 aItemId)
            : base(16)
        {
            Id = aId;
            _Action = aAction;
            Type = aType;
            ItemId = aItemId;
        }

        public MsgPackage(Int32 aId, Action aAction, PackageType aType, Item[] aItems)
            : base((UInt16)(16 + (aItems.Length * 20)))
        {
            Id = aId;
            _Action = aAction;
            Type = aType;

            WriteUInt16(12, (UInt16)aItems.Length);

            int offset = 16;
            foreach (Item item in aItems)
            {
                WriteInt32(offset + 0, item.Id);
                WriteInt32(offset + 4, item.Type);
                mBuf[offset + 8] = 0; // Ident
                mBuf[offset + 9] = item.FirstGem; // Gem1
                mBuf[offset + 10] = item.SecondGem; // Gem2
                mBuf[offset + 11] = item.Attribute; // Magic1
                mBuf[offset + 12] = 0x00; // Magic2
                mBuf[offset + 13] = item.Craft; // Magic3
                WriteUInt16(offset + 14, item.Bless);
                WriteUInt16(offset + 16, item.Enchant);
                WriteUInt16(offset + 18, (UInt16)item.Restrain);
                offset += 20;
            }
        }

        /// <summary>
        /// Process the message for the specified client.
        /// </summary>
        /// <param name="aClient">The client who sent the message.</param>
        public override void Process(Client aClient)
        {
            Player player = aClient.Player;

            switch (Type)
            {
                case PackageType.Storage:
                    {
                        switch (_Action)
                        {
                            case Action.QueryList:
                                {
                                    NPC npc = null;
                                    if (!World.AllNPCs.TryGetValue(Id, out npc))
                                        return;

                                    if (player.Map != npc.Map)
                                        return;

                                    if (!MyMath.CanSee(player.X, player.Y, npc.X, npc.Y, MyMath.NORMAL_RANGE))
                                        return;

                                    if (npc.IsStorageNpc())
                                    {
                                        Item[] items = player.GetWHItems((Int16)Id);
                                        player.Send(new MsgPackage(Id, MsgPackage.Action.QueryList, Type, items));
                                    }
                                    break;
                                }
                            case Action.CheckIn:
                                {
                                    Item item = player.GetItemByUID(ItemId);
                                    if (item == null)
                                        return;

                                    Item.Info info;
                                    if (!Database.AllItems.TryGetValue(item.Type, out info))
                                        return;

                                    if (!info.IsStorageEnable())
                                    {
                                        player.SendSysMsg(StrRes.STR_CANT_STORAGE);
                                        return;
                                    }

                                    NPC npc = null;
                                    if (!World.AllNPCs.TryGetValue(Id, out npc))
                                        return;

                                    if (player.Map != npc.Map)
                                        return;

                                    if (!MyMath.CanSee(player.X, player.Y, npc.X, npc.Y, MyMath.NORMAL_RANGE))
                                        return;

                                    if (!npc.IsStorageNpc())
                                        return;

                                    if (Id != 16)
                                    {
                                        if (player.ItemInWarehouse((Int16)Id) >= 20)
                                            return;
                                    }
                                    else
                                    {
                                        if (player.ItemInWarehouse((Int16)Id) >= 40)
                                            return;
                                    }

                                    item.Position = (UInt16)Id;
                                    player.Send(new MsgItem(item.Id, 0, MsgItem.Action.Drop));

                                    Item[] items = player.GetWHItems((Int16)Id);
                                    player.Send(new MsgPackage(Id, Action.QueryList, PackageType.Storage, items));
                                    break;
                                }
                            case Action.CheckOut:
                                {
                                    Item item = player.GetItemByUID(ItemId);
                                    if (item == null)
                                        return;

                                    NPC npc = null;
                                    if (!World.AllNPCs.TryGetValue(Id, out npc))
                                        return;

                                    if (player.Map != npc.Map)
                                        return;

                                    if (!MyMath.CanSee(player.X, player.Y, npc.X, npc.Y, MyMath.NORMAL_RANGE))
                                        return;

                                    if (!npc.IsStorageNpc())
                                        return;

                                    item.Position = 0;
                                    player.Send(new MsgItemInfo(item, MsgItemInfo.Action.AddItem));

                                    Item[] items = player.GetWHItems((Int16)Id);
                                    player.Send(new MsgPackage(Id, Action.QueryList, PackageType.Storage, items));
                                    break;
                                }
                            default:
                                {
                                    sLogger.Error("Action {1}  for type {0} is not implemented for MsgPackage.", (Byte)Type, (Byte)_Action);
                                    break;
                                }
                        }
                        break;
                    }
                default:
                    {
                        sLogger.Error("Type {0} is not implemented for MsgPackage.", (Byte)Type);
                        break;
                    }
            }
        }
    }
}
