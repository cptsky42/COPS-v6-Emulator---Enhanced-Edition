// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2010 - 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using COServer.Entities;

[assembly: InternalsVisibleTo("COServer.Network.Msg")]

namespace COServer.Network
{
    public class MsgGemEmbed : Msg
    {
        /// <summary>
        /// This is a "constant" that the child must override.
        /// It is the type of the message as specified in NetworkDef.cs file.
        /// </summary>
        protected override UInt16 _TYPE { get { return MSG_GEMEMBED; } }

        public enum Action : ushort
        {
            Embed = 0,
            TakeOff = 1,
        };

        //--------------- Internal Members ---------------
        private Int32 __PlayerId = 0;
        private Int32 __ItemId = 0;
        private Int32 __GemId = 0;
        private UInt16 __Pos = 0;
        private Action __Action = (Action)0;
        //------------------------------------------------

        /// <summary>
        /// Unique ID of the player.
        /// </summary>
        public Int32 PlayerId
        {
            get { return __PlayerId; }
            set { __PlayerId = value; WriteInt32(4, value); }
        }

        /// <summary>
        /// Unique ID of the item.
        /// </summary>
        public Int32 ItemId
        {
            get { return __ItemId; }
            set { __ItemId = value; WriteInt32(8, value); }
        }

        /// <summary>
        /// Unique ID of the gem.
        /// </summary>
        public Int32 GemId
        {
            get { return __GemId; }
            set { __GemId = value; WriteInt32(12, value); }
        }

        /// <summary>
        /// Position of the gem.
        /// </summary>
        public UInt16 Pos
        {
            get { return __Pos; }
            set { __Pos = value; WriteUInt16(16, value); }
        }

        /// <summary>
        /// Action ID.
        /// </summary>
        public Action _Action
        {
            get { return __Action; }
            set { __Action = value; WriteUInt16(18, (UInt16)value); }
        }

        /// <summary>
        /// Create a message object from the specified buffer.
        /// </summary>
        /// <param name="aBuf">The buffer containing the message.</param>
        /// <param name="aIndex">The index where the message is starting in the buffer.</param>
        /// <param name="aLength">The length of the message.</param>
        internal MsgGemEmbed(Byte[] aBuf, int aIndex, int aLength)
            : base(aBuf, aIndex, aLength)
        {
            __PlayerId = BitConverter.ToInt32(mBuf, 4);
            __ItemId = BitConverter.ToInt32(mBuf, 8);
            __GemId = BitConverter.ToInt32(mBuf, 12);
            __Pos = BitConverter.ToUInt16(mBuf, 16);
            __Action = (Action)BitConverter.ToUInt16(mBuf, 18);
        }

        /// <summary>
        /// Process the message for the specified client.
        /// </summary>
        /// <param name="aClient">The client who sent the message.</param>
        public override void Process(Client aClient)
        {
            Player player = aClient.Player;

            if (player.UniqId != PlayerId)
                return;

            if (!player.IsAlive())
            {
                player.SendSysMsg(StrRes.STR_DIE);
                return;
            }

            switch (_Action)
            {
                case Action.Embed:
                    {
                        Item item = player.GetItemByUID(ItemId);
                        if (item == null)
                        {
                            player.SendSysMsg(StrRes.STR_ITEM_NOT_FOUND);
                            return;
                        }

                        if (item.Position != 0)
                            return;

                        Item gem = player.GetItemByUID(GemId);
                        if (gem == null)
                        {
                            player.SendSysMsg(StrRes.STR_ITEM_NOT_FOUND);
                            return;
                        }

                        if (gem.Position != 0 || (gem.Type / 100000) != 7)
                            return;

                        Byte gemType = (Byte)(gem.Type % 100);
                        player.DelItem(gem, true);

                        Byte DuraEffect = 0;
                        if (gemType - (gemType % 10) == 40) //Kylin
                            DuraEffect = (Byte)(gemType % 10);

                        if (Pos == 1)
                            item.FirstGem = gemType;

                        if (Pos == 2)
                            item.SecondGem = gemType;

                        if (DuraEffect > 0)
                        {
                            Double Bonus = 1.0;
                            if (DuraEffect == 1) //Normal (50%)
                                Bonus = 1.5;
                            else if (DuraEffect == 2) //Reffined (100%)
                                Bonus = 2.0;
                            else if (DuraEffect == 3) //Super (200%)
                                Bonus = 3.0;

                            item.MaxDura = (UInt16)((Double)item.MaxDura * Bonus);
                        }

                        player.Send(new MsgItemInfo(item, MsgItemInfo.Action.Update));
                        player.Send(this);
                        break;
                    }
                case Action.TakeOff:
                    {
                        Item item = player.GetItemByUID(ItemId);
                        if (item == null)
                        {
                            player.SendSysMsg(StrRes.STR_ITEM_NOT_FOUND);
                            return;
                        }

                        if (item.Position != 0)
                            return;

                        Byte DuraEffect = 0;
                        if (Pos == 1)
                        {
                            if ((item.FirstGem % 100) - (item.FirstGem % 10) == 40) //Kylin
                                DuraEffect = (Byte)(item.FirstGem % 10);
                            item.FirstGem = 255;
                        }

                        if (Pos == 2)
                        {
                            if ((item.SecondGem % 100) - (item.SecondGem % 10) == 40) //Kylin
                                DuraEffect = (Byte)(item.SecondGem % 10);
                            item.SecondGem = 255;
                        }

                        if (DuraEffect > 0)
                        {
                            Double Bonus = 1.0;
                            if (DuraEffect == 1) //Normal (50%)
                                Bonus = 1.5;
                            else if (DuraEffect == 2) //Reffined (100%)
                                Bonus = 2.0;
                            else if (DuraEffect == 3) //Super (200%)
                                Bonus = 3.0;

                            item.CurDura = (UInt16)((Double)item.CurDura / Bonus);
                            item.MaxDura = (UInt16)((Double)item.MaxDura / Bonus);
                        }

                        player.Send(new MsgItemInfo(item, MsgItemInfo.Action.Update));
                        player.Send(this);
                        break;
                    }
                default:
                    {
                        sLogger.Error("Action {0} is not implemented for MsgGemEmbed.", (UInt16)_Action);
                        break;
                    }
            }
        }
    }
}
