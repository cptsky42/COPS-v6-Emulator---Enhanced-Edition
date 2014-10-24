// * Created by Jean-Philippe Boivin
// * Copyright © 2010-2011
// * Logik. Project

using System;
using System.Runtime.InteropServices;
using COServer.Entities;
using CO2_CORE_DLL.IO;

namespace COServer.Network
{
    public unsafe class MsgPackage : Msg
    {
        public const Int16 Id = _MSG_PACKAGE;

        public enum Action
        {
            QueryList = 0,
            CheckIn = 1,
            CheckOut = 2,
            QueryList2 = 3,
        };

        public enum Type
        {
            None = 0,
            Storage = 10,
            Trunk = 20,
            Chest = 30,
        };

        public struct MsgInfo
        {
            public MsgHeader Header;
            public Int32 UniqId;
            public Byte Action;
            public Byte Type;
            public Int16 Unknow;
            public Int32 Param;
            //ItemInfo (20 bytes)
        };

        public static Byte[] Create(Int32 UniqId, Action Action, Type Type, Int32 Param)
        {
            try
            {
                Byte[] Out = new Byte[16];
                fixed (Byte* p = Out)
                {
                    *((Int16*)(p + 0)) = (Int16)Out.Length;
                    *((Int16*)(p + 2)) = (Int16)Id;
                    *((Int32*)(p + 4)) = (Int32)UniqId;
                    *((Byte*)(p + 8)) = (Byte)Action;
                    *((Byte*)(p + 9)) = (Byte)Type;
                    *((Int16*)(p + 10)) = (Int16)0x00;
                    *((Int32*)(p + 12)) = (Int32)Param;
                }
                return Out;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return null; }
        }

        public static Byte[] Create(Int32 UniqId, Action Action, Type Type, Item[] Items)
        {
            try
            {
                Byte[] Out = new Byte[16 + (Items.Length * 20)];
                fixed (Byte* p = Out)
                {
                    *((Int16*)(p + 0)) = (Int16)Out.Length;
                    *((Int16*)(p + 2)) = (Int16)Id;
                    *((Int32*)(p + 4)) = (Int32)UniqId;
                    *((Byte*)(p + 8)) = (Byte)Action;
                    *((Byte*)(p + 9)) = (Byte)Type;
                    *((Int16*)(p + 10)) = (Int16)0x00;
                    *((Int32*)(p + 12)) = (Int32)Items.Length;

                    Int32 Pos = 16;
                    foreach (Item Item in Items)
                    {
                        *((Int32*)(p + Pos + 0)) = (Int32)Item.UniqId;
                        *((Int32*)(p + Pos + 4)) = (Int32)Item.Id;
                        *((Byte*)(p + Pos + 8)) = (Byte)0x00; //Ident
                        *((Byte*)(p + Pos + 9)) = (Byte)Item.Gem1;
                        *((Byte*)(p + Pos + 10)) = (Byte)Item.Gem2;
                        *((Byte*)(p + Pos + 11)) = (Byte)Item.Attr;
                        *((Byte*)(p + Pos + 12)) = (Byte)0x00; //Magic2
                        *((Byte*)(p + Pos + 13)) = (Byte)Item.Craft;
                        *((Byte*)(p + Pos + 14)) = (Byte)Item.Bless;
                        *((Byte*)(p + Pos + 15)) = (Byte)0x00; //Unknow
                        *((Int16*)(p + Pos + 16)) = (Int16)Item.Enchant;
                        *((Int16*)(p + Pos + 18)) = (Int16)Item.Restrain;
                        Pos += 20;
                    }
                }
                return Out;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return null; }
        }

        public static void Process(Client Client, Byte[] Buffer)
        {
            try
            {
                Int16 MsgLength = (Int16)((Buffer[0x01] << 8) + Buffer[0x00]);
                Int16 MsgId = (Int16)((Buffer[0x03] << 8) + Buffer[0x02]);
                Int32 UniqId = (Int32)((Buffer[0x07] << 24) + (Buffer[0x06] << 16) + (Buffer[0x05] << 8) + Buffer[0x04]);
                Action Action = (Action)Buffer[0x08];
                Type Type = (Type)Buffer[0x09];
                Int16 Unknow = (Int16)((Buffer[0x0B] << 8) + Buffer[0x0A]);
                Int32 Param = (Int32)((Buffer[0x0F] << 24) + (Buffer[0x0E] << 16) + (Buffer[0x0D] << 8) + Buffer[0x0C]);

                Player Player = Client.User;

                switch (Type)
                {
                    case Type.Storage:
                        {
                            switch (Action)
                            {
                                case Action.QueryList:
                                    {
                                        NPC Npc = null;
                                        if (!World.AllNPCs.TryGetValue(UniqId, out Npc))
                                            return;

                                        if (Player.Map != Npc.Map)
                                            return;

                                        if (!MyMath.CanSee(Player.X, Player.Y, Npc.X, Npc.Y, 17))
                                            return;

                                        if (Npc.IsStorageNpc())
                                        {
                                            Item[] Items = Player.GetWHItems((Int16)UniqId);
                                            Player.Send(MsgPackage.Create(UniqId, MsgPackage.Action.QueryList, Type, Items));
                                        }
                                        break;
                                    }
                                case Action.CheckIn:
                                    {
                                        Item Item = Player.GetItemByUID(Param);
                                        if (Item == null)
                                            return;

                                        ItemType.Entry Info;
                                        if (!Database2.AllItems.TryGetValue(Item.Id, out Info))
                                            return;

                                        if (!Info.IsStorageEnable())
                                        {
                                            Player.SendSysMsg(Client.GetStr("STR_CANT_STORAGE"));
                                            return;
                                        }

                                        NPC Npc = null;
                                        if (!World.AllNPCs.TryGetValue(UniqId, out Npc))
                                            return;

                                        if (Player.Map != Npc.Map)
                                            return;

                                        if (!MyMath.CanSee(Player.X, Player.Y, Npc.X, Npc.Y, 17))
                                            return;

                                        if (!Npc.IsStorageNpc())
                                            return;

                                        if (UniqId != 16)
                                        {
                                            if (Player.ItemInWarehouse((Int16)UniqId) >= 20)
                                                return;
                                        }
                                        else
                                        {
                                            if (Player.ItemInWarehouse((Int16)UniqId) >= 40)
                                                return;
                                        }

                                        Item.Position = (UInt16)UniqId;
                                        Player.Send(MsgItem.Create(Item.UniqId, 0, MsgItem.Action.Drop));

                                        Item[] Items = Player.GetWHItems((Int16)UniqId);
                                        Player.Send(MsgPackage.Create(UniqId, Action.QueryList, Type.Storage, Items));
                                        Items = null;
                                        break;
                                    }
                                case Action.CheckOut:
                                    {
                                        Item Item = Player.GetItemByUID(Param);
                                        if (Item == null)
                                            return;

                                        NPC Npc = null;
                                        if (!World.AllNPCs.TryGetValue(UniqId, out Npc))
                                            return;

                                        if (Player.Map != Npc.Map)
                                            return;

                                        if (!MyMath.CanSee(Player.X, Player.Y, Npc.X, Npc.Y, 17))
                                            return;

                                        if (!Npc.IsStorageNpc())
                                            return;

                                        Item.Position = 0;
                                        Player.Send(MsgItemInfo.Create(Item, MsgItemInfo.Action.AddItem));

                                        Item[] Items = Player.GetWHItems((Int16)UniqId);
                                        Player.Send(MsgPackage.Create(UniqId, Action.QueryList, Type.Storage, Items));
                                        Items = null;
                                        break;
                                    }
                                default:
                                    {
                                        Console.WriteLine("Msg[{0}], Type[{2}], Action[{1}] not implemented yet!", MsgId, (Int16)Action, (Int16)Type);
                                        break;
                                    }
                            }
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Msg[{0}], Type[{1}] not implemented yet!", MsgId, (Int16)Type);
                            break;
                        }
                }
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }
    }
}
