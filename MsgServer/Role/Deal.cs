// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.Collections.Generic;
using COServer.Network;
using COServer.Entities;
using CO2_CORE_DLL.IO;

namespace COServer
{
    public class Deal
    {
        private const Int32 _MAX_DEALITEMS = 20;
        private const Int32 _MAX_DEALMONEY = 1000000000;
        private const Int32 _MAX_DEALCPS = 100000000;

        private Player m_Player1;
        private Player m_Player2;
        private List<Int32> m_Items1;
        private List<Int32> m_Items2;
        private Int32 m_Money1;
        private Int32 m_Money2;
        private Int32 m_CPs1;
        private Int32 m_CPs2;
        private Boolean m_ClickOK1;
        private Boolean m_ClickOK2;
        private Boolean m_TradeSuccess;

        private Boolean disposed = false;

        public Deal(Player Player, Player Target)
        {
            m_Player1 = Player;
            m_Player2 = Target;
            m_Items1 = new List<Int32>();
            m_Items2 = new List<Int32>();
            m_Money1 = 0;
            m_Money2 = 0;
            m_CPs1 = 0;
            m_CPs2 = 0;
            m_ClickOK1 = false;
            m_ClickOK2 = false;
            m_TradeSuccess = false;
        }

        ~Deal()
        {
            m_Player1 = null;
            m_Player2 = null;
            m_Items1 = null;
            m_Items2 = null;
        }

        public Boolean TradeOK()
        {
            if (m_Player1 == null || m_Player2 == null)
                return false;

            if (m_Player1.Map != m_Player2.Map)
                return false;

            if (!MyMath.CanSee(m_Player1.X, m_Player1.Y, m_Player2.X, m_Player2.Y, 18))
                return false;

            //Check Money
            if (m_Player1.Money < m_Money1 || m_Player2.Money < m_Money2)
                return false;

            if (m_Player1.Money + m_Money2 > Player._MAX_MONEYLIMIT || m_Player2.Money + m_Money1 > Player._MAX_MONEYLIMIT)
                return false;

            //Check CPs
            if (m_Player1.CPs < m_CPs1 || m_Player2.CPs < m_CPs2)
                return false;

            if (m_Player1.CPs + m_CPs2 > Player._MAX_MONEYLIMIT || m_Player2.CPs + m_CPs1 > Player._MAX_MONEYLIMIT)
                return false;

            //Check Items (1)
            if (m_Player2.ItemInInventory() + m_Items1.Count > 40)
                return false;

            foreach (Int32 UniqId in m_Items1)
            {
                Item Item = null;
                if (!m_Player1.Items.TryGetValue(UniqId, out Item))
                    return false;

                if (Item.Position != 0)
                    return false;
            }

            //Check Items (2)
            if (m_Player1.ItemInInventory() + m_Items2.Count > 40)
                return false;

            foreach (Int32 UniqId in m_Items2)
            {
                Item Item = null;
                if (!m_Player2.Items.TryGetValue(UniqId, out Item))
                    return false;

                if (Item.Position != 0)
                    return false;
            }

            //Execute Trade
            if (m_Money1 != 0)
            {
                m_Player2.Money += m_Money1;
                m_Player1.Money -= m_Money1;
            }
            if (m_Money2 != 0)
            {
                m_Player1.Money += m_Money2;
                m_Player2.Money -= m_Money2;
            }

            if (m_CPs1 != 0)
            {
                m_Player2.CPs += m_CPs1;
                m_Player1.CPs -= m_CPs1;
            }
            if (m_CPs2 != 0)
            {
                m_Player1.CPs += m_CPs2;
                m_Player2.CPs -= m_CPs2;
            }

            m_Player1.Send(MsgUserAttrib.Create(m_Player1, m_Player1.CPs, MsgUserAttrib.Type.CPs));
            m_Player2.Send(MsgUserAttrib.Create(m_Player2, m_Player2.CPs, MsgUserAttrib.Type.CPs));
            //m_Player1.Send(MsgUserAttrib.Create(m_Player1, m_Player1.Money, MsgUserAttrib.Type.Money));
            //m_Player2.Send(MsgUserAttrib.Create(m_Player2, m_Player2.Money, MsgUserAttrib.Type.Money));

            String Items1 = "[";
            foreach (Int32 UniqId in m_Items1)
            {
                Item Item = null;
                if (!m_Player1.Items.TryGetValue(UniqId, out Item))
                    return false;

                Items1 += UniqId + ":";
                m_Player1.DelItem(Item, true);
                m_Player2.AddItem(Item, true);
            }

            String Items2 = "[";
            foreach (Int32 UniqId in m_Items2)
            {
                Item Item = null;
                if (!m_Player2.Items.TryGetValue(UniqId, out Item))
                    return false;

                Items2 += UniqId + ":";
                m_Player2.DelItem(Item, true);
                m_Player1.AddItem(Item, true);
            }

            m_Player1.SendSysMsg(STR.Get("STR_TRADE_SUCCEED"));
            m_Player2.SendSysMsg(STR.Get("STR_TRADE_SUCCEED"));
            Byte[] Buffer = MsgTrade.Create(0, MsgTrade.Action.Success);
            m_Player1.Send(Buffer);
            m_Player2.Send(Buffer);
            m_TradeSuccess = true;

            Database.Save(m_Player1);
            Database.Save(m_Player2);

            Program.Log(String.Format("Deal: {0} -> {1}: {2} Money, {3} CPs, {4} | {1} -> {0}: {5} Money, {6} CPs, {7}.",
                m_Player1.UniqId, m_Player2.UniqId, m_Money1, m_CPs1, Items1 + "]", m_Money2, m_CPs2, Items2 + "]"));
            return true;
        }

        public void Release()
        {
            Byte[] Buffer = MsgTrade.Create(0, MsgTrade.Action.False);
            if (m_Player1 != null)
            {
                m_Player1.TradeRequest = 0;
                m_Player1.Send(Buffer);
                if (!m_TradeSuccess)
                    m_Player1.SendSysMsg(STR.Get("STR_TRADE_FAIL"));
                m_Player1.Deal = null;
            }

            if (m_Player2 != null)
            {
                m_Player2.TradeRequest = 0;
                m_Player2.Send(Buffer);
                if (!m_TradeSuccess)
                    m_Player2.SendSysMsg(STR.Get("STR_TRADE_FAIL"));
                m_Player2.Deal = null;
            }
        }

        public Boolean ClickOK(Player Player)
        {
            if (m_Player1.UniqId == Player.UniqId)
            {
                m_ClickOK1 = true;
                if (m_ClickOK2)
                {
                    TradeOK();
                    return true;
                }
                return false;
            }
            else if (m_Player2.UniqId == Player.UniqId)
            {
                m_ClickOK2 = true;
                if (m_ClickOK1)
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
            ItemType.Entry Info;

            if (Target == null || Item == null)
                return false;

            if (!Database2.AllItems.TryGetValue(Item.Id, out Info))
                return false;

            if (Target.ItemInInventory() >= 40)
                return false;

            if (!Info.IsExchangeEnable())
            {
                Player.SendSysMsg(STR.Get("STR_NOT_FOR_TRADE"));
                return false;
            }

            if (m_Player1.UniqId == Player.UniqId)
            {
                if (m_Items1.Count >= _MAX_DEALITEMS)
                    return false;

                m_Items1.Add(UniqId);
            }
            else if (m_Player2.UniqId == Player.UniqId)
            {
                if (m_Items2.Count >= _MAX_DEALITEMS)
                    return false;

                m_Items2.Add(UniqId);
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

            if (m_Player1.UniqId == Player.UniqId)
                m_Money1 = Value;
            else if (m_Player2.UniqId == Player.UniqId)
                m_Money2 = Value;
            else
                return -1;

            return Value;
        }

        public Int32 AddCPs(Player Player, Int32 Value)
        {
            if (Value < 0)
                return -1;

            if (Player.CPs < Value)
                return -1;

            if (Value > _MAX_DEALCPS)
                Value = _MAX_DEALCPS;

            if (m_Player1.UniqId == Player.UniqId)
                m_CPs1 = Value;
            else if (m_Player2.UniqId == Player.UniqId)
                m_CPs2 = Value;
            else
                return -1;

            return Value;
        }

        public Player GetTarget(Player Player)
        {
            if (m_Player1.UniqId == Player.UniqId)
                return m_Player2;
            else if (m_Player2.UniqId == Player.UniqId)
                return m_Player1;
            else
                return null;
        }
    }
}