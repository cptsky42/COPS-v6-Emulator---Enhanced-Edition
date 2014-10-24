// * Created by Jean-Philippe Boivin
// * Copyright © 2010-2011
// * Logik. Project

using System;
using System.Runtime.InteropServices;
using COServer.Entities;

namespace COServer.Network
{
    public unsafe class MsgMapItem : Msg
    {
        public const Int16 Id = _MSG_MAPITEM;

        public enum Action
        {
            None = 0,
            Create = 1,			// to client
            Delete = 2,			// to client
            Pick = 3,			// to server, client: perform action of pick

            CastTrap = 10,		// to client
            SynchroTrap = 11,		// to client
            DropTrap = 12,		// to client, id=trap_id
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct MsgInfo
        {
            public MsgHeader Header;
            public Int32 UniqId;
            public Int32 Id;
            public UInt16 X;
            public UInt16 Y;
            public Int32 Action;
        };

        public static Byte[] Create(FloorItem Item, Action Action)
        {
            try
            {
                MsgInfo* pMsg = stackalloc MsgInfo[1];
                pMsg->Header.Length = (Int16)sizeof(MsgInfo);
                pMsg->Header.Type = Id;

                pMsg->UniqId = Item.UniqId;
                pMsg->Id = Item.Item.Id;
                pMsg->X = Item.X;
                pMsg->Y = Item.Y;
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
                Int32 UniqId = (Int32)((Buffer[0x07] << 24) + (Buffer[0x06] << 16) + (Buffer[0x05] << 8) + Buffer[0x04]);
                Int32 Id = (Int32)((Buffer[0x0B] << 24) + (Buffer[0x0A] << 16) + (Buffer[0x09] << 8) + Buffer[0x08]);
                Int32 Param = (Int32)((Buffer[0x0F] << 24) + (Buffer[0x0E] << 16) + (Buffer[0x0D] << 8) + Buffer[0x0C]);
                UInt16 X = (UInt16)((Buffer[0x0D] << 8) + Buffer[0x0C]);
                UInt16 Y = (UInt16)((Buffer[0x0F] << 8) + Buffer[0x0E]);
                Action Action = (Action)((Buffer[0x13] << 24) + (Buffer[0x12] << 16) + (Buffer[0x11] << 8) + Buffer[0x10]);

                switch (Action)
                {
                    case Action.Pick:
                        {
                            if (!World.AllFloorItems.ContainsKey(UniqId))
                                return;

                            FloorItem Item = World.AllFloorItems[UniqId];
                            Player Player = Client.User;

                            if (Item.X != X || Item.Y != Y)
                                return;

                            if (MyMath.GetDistance(Player.X, Player.Y, X, Y) > 0)
                            {
                                Player.SendSysMsg(Client.GetStr("STR_FAR_CANNOT_PICK"));
                                return;
                            }

                            if (!Player.IsAlive())
                                return;

                            if (Item.Money == 0 && Player.ItemInInventory() >= 40)
                            {
                                Player.SendSysMsg(Client.GetStr("STR_FULL_CANNOT_PICK"));
                                return;
                            }

                            if (Item.OwnerUID != 0)
                            {
                                if (Item.OwnerUID != Player.UniqId)
                                {
                                    if (Environment.TickCount - Item.DroppedTime < 10000)
                                    {
                                        if (Player.Team != null && Player.Team.IsTeamMember(Item.OwnerUID))
                                        {
                                            if (Item.Money > 0 && Player.Team.MoneyForbidden)
                                            {
                                                Player.SendSysMsg(Client.GetStr("STR_OTHERS_ITEM"));
                                                return;
                                            }
                                            else
                                            {
                                                if (Player.Team.ItemForbidden)
                                                {
                                                    Player.SendSysMsg(Client.GetStr("STR_OTHERS_ITEM"));
                                                    return;
                                                }
                                                else if (Item.Item.Id == 1088000 || Item.Item.Id == 1088001) //DB || Met
                                                {
                                                    Player.SendSysMsg(Client.GetStr("STR_OTHERS_ITEM"));
                                                    return;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Player.SendSysMsg(Client.GetStr("STR_OTHERS_ITEM"));
                                            return;
                                        }
                                    }
                                }
                            }

                            if (Item.Money > 0)
                            {
                                if (Player.Money + Item.Money > Player._MAX_MONEYLIMIT)
                                {
                                    Player.SendSysMsg(Client.GetStr("STR_TOOMUCH_MONEY"));
                                    return;
                                }

                                Player.Money += Item.Money;
                                Player.Send(MsgUserAttrib.Create(Player, Player.Money, MsgUserAttrib.Type.Money));

                                if (Item.Money > 1000)
                                {
                                    Player.Send(MsgUnknow1029.Create(Item.Money));
                                    World.BroadcastRoomMsg(Player, MsgAction.Create(Player, Item.Money, MsgAction.Action.GetMoney), true);
                                }

                                Player.SendSysMsg(Client.GetStr("STR_PICK_MONEY").Replace("%d", Item.Money.ToString()));
                            }
                            else
                            {
                                Player.AddItem(Item.Item, true);
                                Player.SendSysMsg(Client.GetStr("STR_GOT_ITEM").Replace("%s", ItemHandler.GetName(Item.Item.Id)));
                            }

                            World.BroadcastRoomMsg(Player, Buffer, false);
                            Item.Destroy(Item.Money > 0);
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
