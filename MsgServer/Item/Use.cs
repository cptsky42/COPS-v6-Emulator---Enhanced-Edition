// * Created by Jean-Philippe Boivin
// * Copyright © 2011, 2015
// * COPS v6 Emulator

using System;
using COServer.Network;
using COServer.Entities;

namespace COServer
{
    public partial class ItemHandler
    {
        public static void Use(Player Player, Item Item)
        {
            Client Client = Player.Client;

            Boolean Used = false;
            switch (Item.Type)
            {
                case 1000000: //+20HP Potion
                    {
                        Player.CurHP += 20;
                        if (Player.CurHP > Player.MaxHP)
                            Player.CurHP = Player.MaxHP;
                        Player.Send(new MsgUserAttrib(Player, Player.CurHP, MsgUserAttrib.AttributeType.Life));
                        Used = true;
                        break;
                    }
                case 1000010: //+100HP Potion
                    {
                        Player.CurHP += 100;
                        if (Player.CurHP > Player.MaxHP)
                            Player.CurHP = Player.MaxHP;
                        Player.Send(new MsgUserAttrib(Player, Player.CurHP, MsgUserAttrib.AttributeType.Life));
                        Used = true;
                        break;
                    }
                case 1000020: //+250HP Potion
                    {
                        Player.CurHP += 250;
                        if (Player.CurHP > Player.MaxHP)
                            Player.CurHP = Player.MaxHP;
                        Player.Send(new MsgUserAttrib(Player, Player.CurHP, MsgUserAttrib.AttributeType.Life));
                        Used = true;
                        break;
                    }
                case 1000030: //+500HP Potion
                    {
                        Player.CurHP += 500;
                        if (Player.CurHP > Player.MaxHP)
                            Player.CurHP = Player.MaxHP;
                        Player.Send(new MsgUserAttrib(Player, Player.CurHP, MsgUserAttrib.AttributeType.Life));
                        Used = true;
                        break;
                    }
                case 1001000: //+70MP Potion
                    {
                        Player.CurMP += 70;
                        if (Player.CurMP > Player.MaxMP)
                            Player.CurMP = Player.MaxMP;
                        Player.Send(new MsgUserAttrib(Player, Player.CurMP, MsgUserAttrib.AttributeType.Mana));
                        Used = true;
                        break;
                    }
                case 1001010: //+200MP Potion
                    {
                        Player.CurMP += 200;
                        if (Player.CurMP > Player.MaxMP)
                            Player.CurMP = Player.MaxMP;
                        Player.Send(new MsgUserAttrib(Player, Player.CurMP, MsgUserAttrib.AttributeType.Mana));
                        Used = true;
                        break;
                    }
                case 1001020: //+450MP Potion
                    {
                        Player.CurMP += 450;
                        if (Player.CurMP > Player.MaxMP)
                            Player.CurMP = Player.MaxMP;
                        Player.Send(new MsgUserAttrib(Player, Player.CurMP, MsgUserAttrib.AttributeType.Mana));
                        Used = true;
                        break;
                    }
                case 1001030: //+1000MP Potion
                    {
                        Player.CurMP += 1000;
                        if (Player.CurMP > Player.MaxMP)
                            Player.CurMP = Player.MaxMP;
                        Player.Send(new MsgUserAttrib(Player, Player.CurMP, MsgUserAttrib.AttributeType.Mana));
                        Used = true;
                        break;
                    }
                case 1001040: //+2000MP Potion
                    {
                        Player.CurMP += 2000;
                        if (Player.CurMP > Player.MaxMP)
                            Player.CurMP = Player.MaxMP;
                        Player.Send(new MsgUserAttrib(Player, Player.CurMP, MsgUserAttrib.AttributeType.Mana));
                        Used = true;
                        break;
                    }
                case 1002000: //+800HP Potion
                    {
                        Player.CurHP += 800;
                        if (Player.CurHP > Player.MaxHP)
                            Player.CurHP = Player.MaxHP;
                        Player.Send(new MsgUserAttrib(Player, Player.CurHP, MsgUserAttrib.AttributeType.Life));
                        Used = true;
                        break;
                    }
                case 1002010: //+1200HP Potion
                    {
                        Player.CurHP += 1200;
                        if (Player.CurHP > Player.MaxHP)
                            Player.CurHP = Player.MaxHP;
                        Player.Send(new MsgUserAttrib(Player, Player.CurHP, MsgUserAttrib.AttributeType.Life));
                        Used = true;
                        break;
                    }
                case 1002020: //+2000HP Potion
                    {
                        Player.CurHP += 2000;
                        if (Player.CurHP > Player.MaxHP)
                            Player.CurHP = Player.MaxHP;
                        Player.Send(new MsgUserAttrib(Player, Player.CurHP, MsgUserAttrib.AttributeType.Life));
                        Used = true;
                        break;
                    }
                case 1002030: //+3000MP Potion
                    {
                        Player.CurMP += 3000;
                        if (Player.CurMP > Player.MaxMP)
                            Player.CurMP = Player.MaxMP;
                        Player.Send(new MsgUserAttrib(Player, Player.CurMP, MsgUserAttrib.AttributeType.Mana));
                        Used = true;
                        break;
                    }
                case 1002040: //+4500MP Potion
                    {
                        Player.CurMP += 4500;
                        if (Player.CurMP > Player.MaxMP)
                            Player.CurMP = Player.MaxMP;
                        Player.Send(new MsgUserAttrib(Player, Player.CurMP, MsgUserAttrib.AttributeType.Mana));
                        Used = true;
                        break;
                    }
                case 1002050: //+3000HP Potion
                    {
                        Player.CurHP += 3000;
                        if (Player.CurHP > Player.MaxHP)
                            Player.CurHP = Player.MaxHP;
                        Player.Send(new MsgUserAttrib(Player, Player.CurHP, MsgUserAttrib.AttributeType.Life));
                        Used = true;
                        break;
                    }
                default:
                    Player.SendSysMsg("Not Implemented Yet!");
                    break;
            }
            if (Used)
                Player.DelItem(Item.Id, true);
        }
    }
}
