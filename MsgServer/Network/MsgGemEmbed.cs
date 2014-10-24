// * Created by Jean-Philippe Boivin
// * Copyright © 2010-2011
// * Logik. Project

using System;
using System.Runtime.InteropServices;
using COServer.Entities;

namespace COServer.Network
{
    public unsafe class MsgGemEmbed : Msg
    {
        public const Int16 Id = _MSG_GEMEMBED;

        public enum Action
        {
            Embed = 0,
            TakeOff = 1,
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct MsgInfo
        {
            public MsgHeader Header;
            public Int32 UniqId;
            public Int32 ItemUID;
            public Int32 GemUID;
            public Int16 Position;
            public Int16 Action;
        };

        public static void Process(Client Client, Byte[] Buffer)
        {
            try
            {
                if (Client == null || Buffer == null || Client.User == null)
                    return;

                Int16 MsgLength = (Int16)((Buffer[0x01] << 8) + Buffer[0x00]);
                Int16 MsgId = (Int16)((Buffer[0x03] << 8) + Buffer[0x02]);
                Int32 UniqId = (Int32)((Buffer[0x07] << 24) + (Buffer[0x06] << 16) + (Buffer[0x05] << 8) + Buffer[0x04]);
                Int32 ItemUID = (Int32)((Buffer[0x0B] << 24) + (Buffer[0x0A] << 16) + (Buffer[0x09] << 8) + Buffer[0x08]);
                Int32 GemUID = (Int32)((Buffer[0x0F] << 24) + (Buffer[0x0E] << 16) + (Buffer[0x0D] << 8) + Buffer[0x0C]);
                Int16 Position = (Int16)((Buffer[0x11] << 8) + Buffer[0x10]);
                Action Action = (Action)((Buffer[0x13] << 8) + Buffer[0x12]);

                Player Player = Client.User;

                if (Player.UniqId != UniqId)
                    return;

                if (!Player.IsAlive())
                {
                    Player.SendSysMsg(Client.GetStr("STR_DIE"));
                    return;
                }

                switch (Action)
                {
                    case Action.Embed:
                        {
                            Item Item = Player.GetItemByUID(ItemUID);
                            if (Item == null)
                            {
                                Player.SendSysMsg(Client.GetStr("STR_ITEM_NOT_FOUND"));
                                return;
                            }

                            if (Item.Position != 0)
                                return;

                            Item Gem = Player.GetItemByUID(GemUID);
                            if (Gem == null)
                            {
                                Player.SendSysMsg(Client.GetStr("STR_ITEM_NOT_FOUND"));
                                return;
                            }

                            if (Gem.Position != 0 || (Gem.Id / 100000) != 7)
                                return;

                            Byte GemID = (Byte)(Gem.Id % 100);
                            Player.DelItem(Gem, true);

                            Byte DuraEffect = 0;
                            if (GemID - (GemID % 10) == 40) //Kylin
                                DuraEffect = (Byte)(GemID % 10);

                            if (Position == 1)
                                Item.Gem1 = GemID;

                            if (Position == 2)
                                Item.Gem2 = GemID;

                            if (DuraEffect > 0)
                            {
                                Double Bonus = 1.0;
                                if (DuraEffect == 1) //Normal (50%)
                                    Bonus = 1.5;
                                else if (DuraEffect == 2) //Reffined (100%)
                                    Bonus = 2.0;
                                else if (DuraEffect == 3) //Super (200%)
                                    Bonus = 3.0;

                                Item.MaxDura = (UInt16)((Double)Item.MaxDura * Bonus);
                            }

                            Player.Send(MsgItemInfo.Create(Item, MsgItemInfo.Action.Update));
                            Player.Send(Buffer);
                            break;
                        }
                    case Action.TakeOff:
                        {
                            Item Item = Player.GetItemByUID(ItemUID);
                            if (Item == null)
                            {
                                Player.SendSysMsg(Client.GetStr("STR_ITEM_NOT_FOUND"));
                                return;
                            }

                            if (Item.Position != 0)
                                return;

                            Byte DuraEffect = 0;
                            if (Position == 1)
                            {
                                if ((Item.Gem1 % 100) - (Item.Gem1 % 10) == 40) //Kylin
                                    DuraEffect = (Byte)(Item.Gem1 % 10);
                                Item.Gem1 = 255;
                            }

                            if (Position == 2)
                            {
                                if ((Item.Gem2 % 100) - (Item.Gem2 % 10) == 40) //Kylin
                                    DuraEffect = (Byte)(Item.Gem2 % 10);
                                Item.Gem2 = 255;
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

                                Item.CurDura = (UInt16)((Double)Item.CurDura / Bonus);
                                Item.MaxDura = (UInt16)((Double)Item.MaxDura / Bonus);
                            }

                            Player.Send(MsgItemInfo.Create(Item, MsgItemInfo.Action.Update));
                            Player.Send(Buffer);
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
