// * Created by Jean-Philippe Boivin
// * Copyright © 2011, 2014
// * COPS v6 Emulator

using System;
using System.Collections.Generic;
using COServer.Network;
using COServer.Entities;

namespace COServer
{
    public class Deal
    {
        private const Int32 _MAX_DEALITEMS = 20;
        private const Int32 _MAX_DEALMONEY = 1000000000;

        private Player mFirstPlayer;
        private Player mSecondPlayer;
        private List<Int32> mFirstPlayerItems;
        private List<Int32> mSecondPlayerItems;
        private UInt32 mFirstPlayerMoney;
        private UInt32 mSecondPlayerMoney;
        private Boolean mFirstPlayerAccepted;
        private Boolean mSecondPlayerAccepted;
        private Boolean mIsSuccess;

        public Deal(Player Player, Player Target)
        {
            mFirstPlayer = Player;
            mSecondPlayer = Target;
            mFirstPlayerItems = new List<Int32>();
            mSecondPlayerItems = new List<Int32>();
            mFirstPlayerMoney = 0;
            mSecondPlayerMoney = 0;
            mFirstPlayerAccepted = false;
            mSecondPlayerAccepted = false;
            mIsSuccess = false;
        }

        public Boolean TradeOK()
        {
            if (mFirstPlayer == null || mSecondPlayer == null)
                return false;

            if (mFirstPlayer.Map != mSecondPlayer.Map)
                return false;

            if (!MyMath.CanSee(mFirstPlayer.X, mFirstPlayer.Y, mSecondPlayer.X, mSecondPlayer.Y, 18))
                return false;

            //Check Money
            if (mFirstPlayer.Money < mFirstPlayerMoney || mSecondPlayer.Money < mSecondPlayerMoney)
                return false;

            if (mFirstPlayer.Money + mSecondPlayerMoney > Player._MAX_MONEYLIMIT || mSecondPlayer.Money + mFirstPlayerMoney > Player._MAX_MONEYLIMIT)
                return false;

            //Check Items (1)
            if (mSecondPlayer.ItemInInventory() + mFirstPlayerItems.Count > 40)
                return false;

            foreach (Int32 UniqId in mFirstPlayerItems)
            {
                Item Item = null;
                if (!mFirstPlayer.Items.TryGetValue(UniqId, out Item))
                    return false;

                if (Item.Position != 0)
                    return false;
            }

            //Check Items (2)
            if (mFirstPlayer.ItemInInventory() + mSecondPlayerItems.Count > 40)
                return false;

            foreach (Int32 UniqId in mSecondPlayerItems)
            {
                Item Item = null;
                if (!mSecondPlayer.Items.TryGetValue(UniqId, out Item))
                    return false;

                if (Item.Position != 0)
                    return false;
            }

            //Execute Trade
            if (mFirstPlayerMoney != 0)
            {
                mSecondPlayer.Money += mFirstPlayerMoney;
                mFirstPlayer.Money -= mFirstPlayerMoney;
            }
            if (mSecondPlayerMoney != 0)
            {
                mFirstPlayer.Money += mSecondPlayerMoney;
                mSecondPlayer.Money -= mSecondPlayerMoney;
            }

            //mFirstPlayer.Send(new MsgUserAttrib(mFirstPlayer, mFirstPlayer.Money, MsgUserAttrib.Type.Money));
            //mSecondPlayer.Send(new MsgUserAttrib(mSecondPlayer, mSecondPlayer.Money, MsgUserAttrib.Type.Money));

            String Items1 = "[";
            foreach (Int32 UniqId in mFirstPlayerItems)
            {
                Item Item = null;
                if (!mFirstPlayer.Items.TryGetValue(UniqId, out Item))
                    return false;

                Items1 += UniqId + ":";
                mFirstPlayer.DelItem(Item, true);
                mSecondPlayer.AddItem(Item, true);
            }

            String Items2 = "[";
            foreach (Int32 UniqId in mSecondPlayerItems)
            {
                Item Item = null;
                if (!mSecondPlayer.Items.TryGetValue(UniqId, out Item))
                    return false;

                Items2 += UniqId + ":";
                mSecondPlayer.DelItem(Item, true);
                mFirstPlayer.AddItem(Item, true);
            }

            mFirstPlayer.SendSysMsg(StrRes.STR_TRADE_SUCCEED);
            mSecondPlayer.SendSysMsg(StrRes.STR_TRADE_SUCCEED);
            var msg = new MsgTrade(0, MsgTrade.Action.Success);
            mFirstPlayer.Send(msg);
            mSecondPlayer.Send(msg);
            mIsSuccess = true;

            Database.Save(mFirstPlayer, true);
            Database.Save(mSecondPlayer, true);

            return true;
        }

        public void Release()
        {
            var msg = new MsgTrade(0, MsgTrade.Action.False);
            if (mFirstPlayer != null)
            {
                mFirstPlayer.TradeRequest = 0;
                mFirstPlayer.Send(msg);
                if (!mIsSuccess)
                    mFirstPlayer.SendSysMsg(StrRes.STR_TRADE_FAIL);
                mFirstPlayer.Deal = null;
            }

            if (mSecondPlayer != null)
            {
                mSecondPlayer.TradeRequest = 0;
                mSecondPlayer.Send(msg);
                if (!mIsSuccess)
                    mSecondPlayer.SendSysMsg(StrRes.STR_TRADE_FAIL);
                mSecondPlayer.Deal = null;
            }
        }

        public Boolean ClickOK(Player Player)
        {
            if (mFirstPlayer.UniqId == Player.UniqId)
            {
                mFirstPlayerAccepted = true;
                if (mSecondPlayerAccepted)
                {
                    TradeOK();
                    return true;
                }
                return false;
            }
            else if (mSecondPlayer.UniqId == Player.UniqId)
            {
                mSecondPlayerAccepted = true;
                if (mFirstPlayerAccepted)
                {
                    TradeOK();
                    return true;
                }
                return false;
            }
            else
            {
                Release();
                return false;
            }
        }

        public Boolean AddItem(Player Player, Int32 UniqId)
        {
            if (!Player.Items.ContainsKey(UniqId))
                return false;

            Player Target = GetTarget(Player);
            Item Item = Player.GetItemByUID(UniqId);
            Item.Info Info;

            if (Target == null || Item == null)
                return false;

            if (!Database.AllItems.TryGetValue(Item.Type, out Info))
                return false;

            if (Target.ItemInInventory() >= 40)
                return false;

            if (!Info.IsExchangeEnable())
            {
                Player.SendSysMsg(StrRes.STR_NOT_FOR_TRADE);
                return false;
            }

            if (mFirstPlayer.UniqId == Player.UniqId)
            {
                if (mFirstPlayerItems.Count >= _MAX_DEALITEMS)
                    return false;

                mFirstPlayerItems.Add(UniqId);
            }
            else if (mSecondPlayer.UniqId == Player.UniqId)
            {
                if (mSecondPlayerItems.Count >= _MAX_DEALITEMS)
                    return false;

                mSecondPlayerItems.Add(UniqId);
            }
            else
                return false;

            return true;
        }

        public Int32 AddMoney(Player Player, Int32 Value)
        {
            if (Value < 0)
                return -1;

            if (Player.Money < Value)
                return -1;

            if (Value > _MAX_DEALMONEY)
                Value = _MAX_DEALMONEY;

            if (mFirstPlayer.UniqId == Player.UniqId)
                mFirstPlayerMoney = (UInt32)Value;
            else if (mSecondPlayer.UniqId == Player.UniqId)
                mSecondPlayerMoney = (UInt32)Value;
            else
                return -1;

            return Value;
        }

        public Player GetTarget(Player Player)
        {
            if (mFirstPlayer.UniqId == Player.UniqId)
                return mSecondPlayer;
            else if (mSecondPlayer.UniqId == Player.UniqId)
                return mFirstPlayer;
            else
                return null;
        }
    }
}