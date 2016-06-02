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
    public class MsgTrade : Msg
    {
        /// <summary>
        /// This is a "constant" that the child must override.
        /// It is the type of the message as specified in NetworkDef.cs file.
        /// </summary>
        protected override UInt16 _TYPE { get { return MSG_TRADE; } }

        public enum Action
        {
            Apply = 1, //TS & TC
            Quit = 2, //TS
            Open = 3, //TC
            Success = 4, //TC
            False = 5, //TC
            AddItem = 6, //TS & TC
            AddMoney = 7, //TS
            AllMoney = 8, //TC
            SelfAllMoney = 9, //TC
            Ok = 10, //TS & TC
            AddItemFail = 11, //TC
        };

        //--------------- Internal Members ---------------
        private Int32 __Id = 0;
        private UInt32 __Data = 0;
        private Action __Action = (Action)0;
        //------------------------------------------------

        /// <summary>
        /// Unique ID of the player.
        /// </summary>
        public Int32 Id
        {
            get { return __Id; }
            set { __Id = value; __Data = (UInt32)value; WriteInt32(4, value); }
        }

        /// <summary>
        /// Data of the action.
        /// </summary>
        public UInt32 Data
        {
            get { return __Data; }
            set { __Data = value; __Id = (Int32)value; WriteUInt32(4, value); }
        }

        /// <summary>
        /// Action ID.
        /// </summary>
        public Action _Action
        {
            get { return __Action; }
            set { __Action = value; WriteUInt32(8, (UInt32)value); }
        }

        /// <summary>
        /// Create a message object from the specified buffer.
        /// </summary>
        /// <param name="aBuf">The buffer containing the message.</param>
        /// <param name="aIndex">The index where the message is starting in the buffer.</param>
        /// <param name="aLength">The length of the message.</param>
        internal MsgTrade(Byte[] aBuf, int aIndex, int aLength)
            : base(aBuf, aIndex, aLength)
        {
            __Id = BitConverter.ToInt32(mBuf, 4);
            __Data = BitConverter.ToUInt32(mBuf, 4);
            __Action = (Action)BitConverter.ToUInt32(mBuf, 8);
        }

        public MsgTrade(Int32 aId, Action aAction)
            : base(12)
        {
            Id = aId;
            _Action = aAction;
        }

        public MsgTrade(UInt32 aData, Action aAction)
            : base(12)
        {
            Data = aData;
            _Action = aAction;
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
                case Action.Apply:
                    {
                        Player target = null;
                        if (!World.AllPlayers.TryGetValue(Id, out target))
                            return;

                        if (player.Map.Id != target.Map.Id ||
                            !MyMath.CanSee(player.X, player.Y, target.X, target.Y, MyMath.NORMAL_RANGE))
                        {
                            player.SendSysMsg(StrRes.STR_NO_TRADE_TARGET);
                            return;
                        }

                        if (player.Deal != null)
                        {
                            player.Deal.Release();
                            return;
                        }

                        if (target.Deal != null)
                        {
                            player.SendSysMsg(StrRes.STR_TARGET_TRADING);
                            return;
                        }

                        player.TradeRequest = Id;
                        if (target.TradeRequest != player.UniqId)
                        {
                            target.Send(new MsgTrade(player.UniqId, Action.Apply));
                            player.SendSysMsg(StrRes.STR_TRADING_REQEST_SENT);
                            return;
                        }

                        Deal deal = new Deal(player, target);
                        player.Deal = deal;
                        target.Deal = deal;

                        player.Send(new MsgTrade(target.UniqId, Action.Open));
                        target.Send(new MsgTrade(player.UniqId, Action.Open));
                        break;
                    }
                case Action.Quit:
                    {
                        if (player.Deal != null)
                            player.Deal.Release();
                        break;
                    }
                case Action.AddItem:
                    {
                        if (player.Deal == null)
                            return;

                        if (!player.Deal.AddItem(player, Id))
                        {
                            player.Send(new MsgTrade(Id, Action.AddItemFail));
                            return;
                        }

                        player.Deal.GetTarget(player).Send(new MsgItemInfo(player.GetItemByUID(Id), MsgItemInfo.Action.Trade));
                        player.Send(new MsgTrade(Id, Action.AddItem));
                        break;
                    }
                case Action.AddMoney:
                    {
                        if (player.Deal == null)
                            return;

                        Int32 allMoney = player.Deal.AddMoney(player, (int)Data);
                        if (allMoney > -1)
                        {
                            player.Deal.GetTarget(player).Send(new MsgTrade(allMoney, Action.AllMoney));
                            player.Send(new MsgTrade(allMoney, Action.SelfAllMoney));
                        }
                        break;
                    }
                case Action.Ok:
                    {
                        if (player.Deal == null)
                            return;

                        if (player.Deal.ClickOK(player))
                            player.Deal.Release();
                        else
                        {
                            Player target = player.Deal.GetTarget(player);
                            target.Send(this);
                        }
                        break;
                    }
                default:
                    {
                        sLogger.Error("Action {0} is not implemented for MsgTrade.", (UInt32)_Action);
                        break;
                    }
            }
        }
    }
}
