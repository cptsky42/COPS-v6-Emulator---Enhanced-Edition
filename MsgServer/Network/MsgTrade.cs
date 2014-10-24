// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.Runtime.InteropServices;
using COServer.Entities;

namespace COServer.Network
{
    public unsafe class MsgTrade : Msg
    {
        public const Int16 Id = _MSG_TRADE;

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
            AllCPs = 12, //TC
            AddCPs = 13, //TS
            SelfAllCPs = 14, //TC
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct MsgInfo
        {
            public MsgHeader Header;
            public Int32 Param;
            public Int32 Action;
        };

        public static Byte[] Create(Int32 Param, Action Action)
        {
            try
            {
                MsgInfo* pMsg = stackalloc MsgInfo[1];
                pMsg->Header.Length = (Int16)sizeof(MsgInfo);
                pMsg->Header.Type = Id;

                pMsg->Param = Param;
                pMsg->Action = (Int32)Action;

                Byte[] Out = new Byte[pMsg->Header.Length];
                Marshal.Copy((IntPtr)pMsg, Out, 0, Out.Length);

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

                Int16 MsgLength = (Int16)((Buffer[0x01] << 8) + Buffer[0x00]);
                Int16 MsgId = (Int16)((Buffer[0x03] << 8) + Buffer[0x02]);
                Int32 Param = (Int32)((Buffer[0x07] << 24) + (Buffer[0x06] << 16) + (Buffer[0x05] << 8) + Buffer[0x04]);
                Action Action = (Action)((Buffer[0x0B] << 24) + (Buffer[0x0A] << 16) + (Buffer[0x09] << 8) + Buffer[0x08]);

                Player Player = Client.User;
                if (Player == null)
                    return;

                switch (Action)
                {
                    case Action.Apply:
                        {
                            if (Param == -1 || Param == 0)
                                return;

                            Player Target = null;
                            if (!World.AllPlayers.TryGetValue(Param, out Target))
                                return;

                            if (Player.Map != Target.Map || !MyMath.CanSee(Player.X, Player.Y, Target.X, Target.Y, 20))
                            {
                                Player.SendSysMsg(Client.GetStr("STR_NO_TRADE_TARGET"));
                                return;
                            }

                            if (Player.Deal != null)
                            {
                                Player.Deal.Release();
                                return;
                            }

                            if (Target.Deal != null)
                            {
                                Player.SendSysMsg(Client.GetStr("STR_TARGET_TRADING"));
                                return;
                            }

                            Player.TradeRequest = Param;
                            if (Target.TradeRequest != Player.UniqId)
                            {
                                Target.Send(MsgTrade.Create(Player.UniqId, Action.Apply));
                                Player.SendSysMsg(Client.GetStr("STR_TRADING_REQEST_SENT"));
                                return;
                            }

                            Deal Deal = new Deal(Player, Target);
                            Player.Deal = Deal;
                            Target.Deal = Deal;

                            Player.Send(MsgTrade.Create(Target.UniqId, Action.Open));
                            Target.Send(MsgTrade.Create(Player.UniqId, Action.Open));
                            break;
                        }
                    case Action.Quit:
                        {
                            if (Player.Deal != null)
                                Player.Deal.Release();
                            break;
                        }
                    case Action.AddItem:
                        {
                            if (Player.Deal == null)
                                return;

                            if (!Player.Deal.AddItem(Player, Param))
                            {
                                Player.Send(MsgTrade.Create(Param, Action.AddItemFail));
                                return;
                            }

                            Player.Deal.GetTarget(Player).Send(MsgItemInfo.Create(Player.GetItemByUID(Param), MsgItemInfo.Action.Trade));
                            Player.Send(MsgTrade.Create(Param, Action.AddItem));
                            break;
                        }
                    case Action.AddMoney:
                        {
                            if (Player.Deal == null)
                                return;

                            Int32 AllMoney = Player.Deal.AddMoney(Player, Param);
                            if (AllMoney > -1)
                            {
                                Player.Deal.GetTarget(Player).Send(MsgTrade.Create(AllMoney, Action.AllMoney));
                                Player.Send(MsgTrade.Create(AllMoney, Action.SelfAllMoney));
                            }
                            break;
                        }
                    case Action.Ok:
                        {
                            if (Player.Deal == null)
                                return;

                            if (Player.Deal.ClickOK(Player))
                                Player.Deal.Release();
                            else
                            {
                                Player Target = Player.Deal.GetTarget(Player);
                                Target.Send(Buffer);
                            }
                            break;
                        }
                    case Action.AddCPs:
                        {
                            if (Player.Deal == null)
                                return;

                            Int32 AllCPs = Player.Deal.AddCPs(Player, Param);
                            if (AllCPs > -1)
                            {
                                Player.Deal.GetTarget(Player).Send(MsgTrade.Create(AllCPs, Action.AllCPs));
                                Player.Send(MsgTrade.Create(AllCPs, Action.SelfAllCPs));
                            }
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Msg[{0}], Action[{1}] not implemented yet!", MsgId, (Int16)Action);
                            break;
                        }
                }
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }
    }
}
