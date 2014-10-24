// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using COServer.Script;
using COServer.Entities;
using CO2_CORE_DLL.IO;
using CO2_CORE_DLL;

namespace COServer.Network
{
    public unsafe class MsgDialog : Msg
    {
        public const Int16 Id = _MSG_DIALOG;

        public enum Action
        {
            Text = 1,
            Link = 2,
            Edit = 3,
            Pic = 4,				// data: npc face
            ListLine = 5,
            Create = 100,			// idxTask: default task
            Answer = 101,			// to server
            TaskId = 102,			// to server, launch task id by interface
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct MsgInfo
        {
            public MsgHeader Header;
            public Int32 TaskId;
            public Int16 Param;
            public Byte IdxTask;
            public Byte Action;
            public String Text;
        };

        public static Byte[] Create(Int32 TaskId, Int16 Param, Byte IdxTask, Action Action, String Text)
        {
            try
            {
                if (Text == null)
                    Text = "";

                if (Text.Length > _MAX_WORDSSIZE)
                    return null;

                Byte[] Out = new Byte[14 + Text.Length];
                fixed (Byte* p = Out)
                {
                    *((Int16*)(p + 0)) = (Int16)Out.Length;
                    *((Int16*)(p + 2)) = (Int16)Id;
                    *((Int32*)(p + 4)) = (Int32)TaskId;
                    *((Int16*)(p + 8)) = (Int16)Param;
                    *((Byte*)(p + 10)) = (Byte)IdxTask;
                    *((Byte*)(p + 11)) = (Byte)Action;
                    if (Text != null || Text != "")
                    {
                        *((Byte*)(p + 12)) = (Byte)0x01;
                        *((Byte*)(p + 13)) = (Byte)Text.Length;
                        for (Byte i = 0; i < (Byte)Text.Length; i++)
                            *((Byte*)(p + 14 + i)) = (Byte)Text[i];
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
                if (Client == null || Buffer == null || Client.User == null)
                    return;

                Int16 MsgLength = (Int16)((Buffer[0x01] << 8) + Buffer[0x00]);
                Int16 MsgId = (Int16)((Buffer[0x03] << 8) + Buffer[0x02]);
                Int32 TaskId = (Int32)((Buffer[0x07] << 24) + (Buffer[0x06] << 16) + (Buffer[0x05] << 8) + Buffer[0x04]);
                Int16 Param = (Int16)((Buffer[0x09] << 8) + Buffer[0x08]);
                Byte IdxTask = (Byte)Buffer[0x0A];
                Action Action = (Action)Buffer[0x0B];
                String Text = Program.Encoding.GetString(Buffer, 0x0E, Buffer[0x0D]);

                Player Player = Client.User;

                switch (Action)
                {
                    case Action.Answer:
                        {
                            if (!Player.IsAlive())
                            {
                                Player.SendSysMsg(Client.GetStr("STR_DIE"));
                                return;
                            }


                            Entity Npc = null;
                            if (Client.NpcUID < 1000000) //Not an item!
                            {
                                if (World.AllNPCs.ContainsKey(Client.NpcUID))
                                    Npc = World.AllNPCs[Client.NpcUID];

                                if (World.AllTerrainNPCs.ContainsKey(Client.NpcUID))
                                    Npc = World.AllTerrainNPCs[Client.NpcUID];

                                if (Npc == null)
                                    return;

                                if (Player.Map != Npc.Map)
                                    return;

                                if (!MyMath.CanSee(Player.X, Player.Y, Npc.X, Npc.Y, 17))
                                    return;
                            }

                            Byte[] Data = new Byte[Kernel.MAX_BUFFER_SIZE];
                            Int32 Position = 0;

                            //Poles
                            if (Client.NpcUID == 110110 ||
                                Client.NpcUID == 110200 ||
                                Client.NpcUID == 110000 ||
                                Client.NpcUID == 110150 ||
                                Client.NpcUID == 110380)
                            {
                                Map Map = World.AllMaps[Npc.Map];
                                if (IdxTask == 1)
                                {
                                    if (Player.Syndicate == null || Player.Syndicate.UniqId != Map.Holder)
                                    {
                                        Position += ScriptHandler.SendText("Juste la guilde possédant le Pole peut le réparer.", Client, ref Data, Position);
                                        Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                        Position += ScriptHandler.SendFace(47, Client, ref Data, Position);
                                        Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                        ScriptHandler.SendData(Client, Data, Position);
                                        return;
                                    }

                                    Syndicate.Member Member = Player.Syndicate.GetMemberInfo(Player.UniqId);
                                    if (Member == null || (Member.Rank != 100))
                                    {
                                        Position += ScriptHandler.SendText("Vous n'etes pas chef de la guilde!", Client, ref Data, Position);
                                        Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                        Position += ScriptHandler.SendFace(47, Client, ref Data, Position);
                                        Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                        ScriptHandler.SendData(Client, Data, Position);
                                        return;
                                    }

                                    TerrainNPC Pole = Map.Entities[Client.NpcUID] as TerrainNPC;
                                    if (Pole != null && Pole.CurHP < Pole.MaxHP)
                                    {
                                        Int32 Money = Pole.MaxHP - Pole.CurHP;
                                        if (Player.Syndicate.Money > Money)
                                        {
                                            Pole.CurHP += Money;
                                            Player.Syndicate.Money -= Money;
                                            World.BroadcastMapMsg(Map.UniqId, MsgNpcInfoEx.Create(Pole, true));
                                        }
                                    }
                                }
                            }
                            //Poles

                            //Gates
                            if (Client.NpcUID == 110111 || Client.NpcUID == 110112 ||
                                Client.NpcUID == 110201 || Client.NpcUID == 110202 ||
                                Client.NpcUID == 110001 || Client.NpcUID == 110002 ||
                                Client.NpcUID == 110151 || Client.NpcUID == 110152 ||
                                Client.NpcUID == 110381 || Client.NpcUID == 110382)
                            {
                                Map Map = World.AllMaps[Npc.Map];
                                if (IdxTask == 1)
                                {
                                    if (Player.Syndicate == null || Player.Syndicate.UniqId != Map.Holder)
                                    {
                                        Position += ScriptHandler.SendText("Juste la guilde possédant le Pole peut contrôler les portes.", Client, ref Data, Position);
                                        Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                        Position += ScriptHandler.SendFace(47, Client, ref Data, Position);
                                        Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                        ScriptHandler.SendData(Client, Data, Position);
                                        return;
                                    }

                                    Syndicate.Member Member = Player.Syndicate.GetMemberInfo(Player.UniqId);
                                    if (Member == null || (Member.Rank != 90 && Member.Rank != 100))
                                    {
                                        Position += ScriptHandler.SendText("Vous n'etes pas sous-chef ou chef de la guilde!", Client, ref Data, Position);
                                        Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                        Position += ScriptHandler.SendFace(47, Client, ref Data, Position);
                                        Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                        ScriptHandler.SendData(Client, Data, Position);
                                        return;
                                    }

                                    TerrainNPC Gate = Map.Entities[Client.NpcUID] as TerrainNPC;
                                    if (Gate != null)
                                    {
                                        if (Gate.CurHP <= 1)
                                        {
                                            if (Player.Syndicate.Money > 300000 && Member.Rank == 100)
                                            {
                                                if (Gate.Look / 10 != Gate.Base)
                                                    Gate.Look = (UInt32)(Gate.Base * 10 + (Gate.Look % 10));
                                                Gate.CurHP = Gate.MaxHP;

                                                Player.Syndicate.Money -= 300000;
                                            }
                                        }
                                        else
                                        {
                                            if (Gate.Look / 10 != Gate.Base)
                                                Gate.Look = (UInt32)(Gate.Base * 10 + (Gate.Look % 10));
                                            else
                                                Gate.Look += 10;
                                        }
                                        World.BroadcastMapMsg(Map.UniqId, MsgNpcInfoEx.Create(Gate, true));
                                    }
                                }
                                else if (IdxTask == 2)
                                {
                                    if (Player.Syndicate == null || Player.Syndicate.UniqId != Map.Holder)
                                    {
                                        Position += ScriptHandler.SendText("Juste la guilde possédant le Pole peut contrôler les portes.", Client, ref Data, Position);
                                        Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                        Position += ScriptHandler.SendFace(47, Client, ref Data, Position);
                                        Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                        ScriptHandler.SendData(Client, Data, Position);
                                        return;
                                    }

                                    if (Client.NpcUID % 10 == 1)
                                        Player.Move(Npc.Map, Npc.X, (UInt16)(Npc.Y - 4));
                                    else if (Client.NpcUID % 10 == 2)
                                        Player.Move(Npc.Map, (UInt16)(Npc.X - 4), Npc.Y);
                                }
                            }
                            //Gates

                            if (ScriptHandler.AllScripts[(Byte)Client.Language].ContainsKey(Client.NpcUID))
                            {
                                if (IdxTask != 255)
                                    ScriptHandler.AllScripts[(Byte)Client.Language][Client.NpcUID].Execute(IdxTask, Client);
                                else
                                    Client.NpcUID = -1;
                                return;
                            }
                            else
                            {
                                if (Client.Language == Language.En)
                                {
                                    switch (Client.NpcUID)
                                    {
                                        case 422: //MaçonVieux
                                            {
                                                if (IdxTask == 1)
                                                {
                                                    if (World.Tournament == null || World.Tournament.Closed)
                                                    {
                                                        Position += ScriptHandler.SendText("Sorry. There is no tournament at the moment, or the tournament is closed!", Client, ref Data, Position);
                                                        Position += ScriptHandler.SendOption(255, "Ok, thanks.", Client, ref Data, Position);
                                                        Position += ScriptHandler.SendFace(54, Client, ref Data, Position);
                                                        Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                        ScriptHandler.SendData(Client, Data, Position);
                                                        return;
                                                    }

                                                    World.Tournament.AddPlayer(Player);
                                                }
                                                return;
                                            }
                                    }
                                }
                                else
                                {
                                    switch (Client.NpcUID)
                                    {
                                        case 422: //MaçonVieux
                                            {
                                                if (IdxTask == 1)
                                                {
                                                    if (World.Tournament == null || World.Tournament.Closed)
                                                    {
                                                        Position += ScriptHandler.SendText("Désolé. Il n'y a pas de tournoi en cours, ou le tournoi est fermé.", Client, ref Data, Position);
                                                        Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                        Position += ScriptHandler.SendFace(54, Client, ref Data, Position);
                                                        Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                        ScriptHandler.SendData(Client, Data, Position);
                                                        return;
                                                    }

                                                    World.Tournament.AddPlayer(Player);
                                                }
                                                return;
                                            }
                                        case 923: //DameBonheur
                                            {
                                                if (IdxTask == 1)
                                                {
                                                    if (Player.CPs < 54 || Player.Level < 70)
                                                    {
                                                        Position += ScriptHandler.SendText("Vous n'avez pas assez de CPs ou audessous du level 70.", Client, ref Data, Position);
                                                        Position += ScriptHandler.SendOption(255, "Désolé.", Client, ref Data, Position);
                                                        Position += ScriptHandler.SendFace(1, Client, ref Data, Position);
                                                        Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                        ScriptHandler.SendData(Client, Data, Position);
                                                        return;
                                                    }

                                                    Player.CPs -= 54;
                                                    Player.Send(MsgUserAttrib.Create(Player, Player.CPs, MsgUserAttrib.Type.CPs));

                                                    Player.Move(700, 50, 50);
                                                }
                                                else if (IdxTask == 2)
                                                {
                                                    Position += ScriptHandler.SendText("Vous sez téléporté au Centre de Loterie où il y a beaucoup de BoîteSurprises après vous me donnez 54CPs. Vous pouvez choisir une boîte pour essayer votre chance si vous voulez. Mais vous avez un chance pour ouvrir une BoîteSurprise à chaque fois. Si vous voulez ouvrir une autre boîte, vous devez quitter la chambre et vous reinscrire dans le marché.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Ok, Je vois.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(1, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                }
                                                return;
                                            }
                                    }
                                }

                                switch (Client.NpcUID)
                                {
                                    case 41: //ArtisantOu
                                        {
                                            if (IdxTask == 1)
                                            {
                                                Item Item = Player.GetItemByPos(4);
                                                if (Item == null)
                                                {
                                                    Position += ScriptHandler.SendText("Vous devez équiper l'objet.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(96, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    break;
                                                }

                                                if (Item.CurDura != Item.MaxDura)
                                                {
                                                    Position += ScriptHandler.SendText("Vous devez réparer l'objet avant de l'améliorer.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(96, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    break;
                                                }

                                                if (Item.Gem2 != 0)
                                                {
                                                    Position += ScriptHandler.SendText("Je ne peux pas créer de trou dans cet objet.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(96, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    break;
                                                }

                                                if (Item.Gem1 == 0)
                                                {
                                                    Position += ScriptHandler.SendText("Voulez-vous que je crée un trou dans votre arme? Il vous en coutera une Perle de Dragon.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(2, "Oui, S.V.P.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Non, merci.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(96, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    break;
                                                }

                                                if (Item.Gem1 != 0 && Item.Gem2 == 0)
                                                {
                                                    Position += ScriptHandler.SendText("Voulez-vous que je crée un trou dans votre arme? Il vous en coutera 5 Perles de Dragon.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(3, "Oui, S.V.P.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Non, merci.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(96, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    break;
                                                }
                                            }
                                            else if (IdxTask >= 2 && IdxTask <= 3)
                                            {
                                                Item Item = Player.GetItemByPos(4);
                                                if (Item == null)
                                                {
                                                    Position += ScriptHandler.SendText("Vous devez équiper l'objet.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(96, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    break;
                                                }

                                                if (Item.CurDura != Item.MaxDura)
                                                {
                                                    Position += ScriptHandler.SendText("Vous devez réparer l'objet avant de l'améliorer.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(96, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    break;
                                                }

                                                if (Item.Gem2 != 0)
                                                {
                                                    Position += ScriptHandler.SendText("Je ne peux pas créer de trou dans cet objet.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(96, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    break;
                                                }

                                                Byte Amount = 1;
                                                if (IdxTask == 3)
                                                    Amount = 5;

                                                if (!Player.InventoryContains(1088000, Amount))
                                                {
                                                    Position += ScriptHandler.SendText("Vous n'avez pas assez de Perle de Dragon.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(96, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    break;
                                                }

                                                Player.DelItem(1088000, Amount, true);

                                                if (IdxTask == 2)
                                                    Item.Gem1 = 255;
                                                else if (IdxTask == 3)
                                                    Item.Gem2 = 255;

                                                Player.UpdateItem(Item);

                                                Position += ScriptHandler.SendText("Votre équipement a été amélioré!", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Merci.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(96, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            break;
                                        }
                                    case 45: //GérantDeMarché
                                        {
                                            if (IdxTask == 1)
                                            {
                                                Int16 PrevMap = Player.PrevMap;
                                                if (PrevMap != 1000 && PrevMap != 1002 && PrevMap != 1011 && PrevMap != 1015 && PrevMap != 1020)
                                                    PrevMap = 1002;

                                                if (!World.AllMaps.ContainsKey(PrevMap))
                                                {
                                                    if (!World.AllMaps.ContainsKey(1002))
                                                        return;
                                                    PrevMap = 1002;
                                                }

                                                Map Map = World.AllMaps[PrevMap];
                                                Player.Move(PrevMap, Map.PortalX, Map.PortalY);
                                            }
                                            break;
                                        }
                                    case 140: //GardienDePrison
                                        {
                                            if (IdxTask == 1)
                                            {
                                                if (Player.JailC > 0)
                                                {
                                                    Int32[] Costs = new Int32[] { 1000, 2500, 5000 };
                                                    if (Player.CPs < Costs[Player.JailC - 1])
                                                        return;

                                                    Player.CPs -= Costs[Player.JailC - 1];
                                                    Player.Send(MsgUserAttrib.Create(Player, Player.CPs, MsgUserAttrib.Type.CPs));
                                                    Player.Move(1002, 400, 400);
                                                }
                                                else
                                                    Player.Move(1002, 400, 400);
                                            }
                                            break;
                                        }
                                    case 380: //GérantDeGuilde
                                        {
                                            if (IdxTask == 1)
                                                Player.Move(1038, (UInt16)(330 + MyMath.Generate(-15, 15)), (UInt16)(325 + MyMath.Generate(-15, 15)));

                                            //if (IdxTask == 1)
                                            //{
                                            //    if (Player.Syndicate == null)
                                            //    {
                                            //        Position += ScriptHandler.SendText("Vous devez etre dans une guilde pour déclarer la guerre!", Client, ref Data, Position);
                                            //        Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                            //        Position += ScriptHandler.SendFace(123, Client, ref Data, Position);
                                            //        Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                            //        ScriptHandler.SendData(Client, Data, Position);
                                            //        return;
                                            //    }

                                            //    Syndicate.Member Member = Player.Syndicate.GetMemberInfo(Player.UniqId);
                                            //    if (Member == null || Member.Rank != 100)
                                            //    {
                                            //        Position += ScriptHandler.SendText("Vous devez etre chef d'une guilde pour déclarer la guerre!", Client, ref Data, Position);
                                            //        Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                            //        Position += ScriptHandler.SendFace(123, Client, ref Data, Position);
                                            //        Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                            //        ScriptHandler.SendData(Client, Data, Position);
                                            //        return;
                                            //    }

                                            //    Map Map = null;
                                            //    if (!World.AllMaps.TryGetValue(1011, out Map))
                                            //        return;

                                            //    if (Map == null || Map.InWar)
                                            //    {
                                            //        Position += ScriptHandler.SendText("Il y a déjà une guerre dans cette région!", Client, ref Data, Position);
                                            //        Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                            //        Position += ScriptHandler.SendFace(123, Client, ref Data, Position);
                                            //        Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                            //        ScriptHandler.SendData(Client, Data, Position);
                                            //        return;
                                            //    }

                                            //    new Games.CityWar(Map);
                                            //    World.BroadcastMsg(MsgTalk.Create("SYSTEM", "ALLUSERS", Player.Name + " des " + Player.Syndicate.Name + " a déclaré la guerre pour prendre la Foret!", MsgTalk.Channel.GM, 0xFFFFFF));
                                            //}
                                            //else if (IdxTask == 2)
                                            //{
                                            //    if (Player.Syndicate == null)
                                            //    {
                                            //        Position += ScriptHandler.SendText("Vous devez etre dans une guilde pour déclarer la guerre!", Client, ref Data, Position);
                                            //        Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                            //        Position += ScriptHandler.SendFace(123, Client, ref Data, Position);
                                            //        Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                            //        ScriptHandler.SendData(Client, Data, Position);
                                            //        return;
                                            //    }

                                            //    Syndicate.Member Member = Player.Syndicate.GetMemberInfo(Player.UniqId);
                                            //    if (Member == null || Member.Rank != 100)
                                            //    {
                                            //        Position += ScriptHandler.SendText("Vous devez etre chef d'une guilde pour déclarer la guerre!", Client, ref Data, Position);
                                            //        Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                            //        Position += ScriptHandler.SendFace(123, Client, ref Data, Position);
                                            //        Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                            //        ScriptHandler.SendData(Client, Data, Position);
                                            //        return;
                                            //    }

                                            //    Map Map = null;
                                            //    if (!World.AllMaps.TryGetValue(1020, out Map))
                                            //        return;

                                            //    if (Map == null || Map.InWar)
                                            //    {
                                            //        Position += ScriptHandler.SendText("Il y a déjà une guerre dans cette région!", Client, ref Data, Position);
                                            //        Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                            //        Position += ScriptHandler.SendFace(123, Client, ref Data, Position);
                                            //        Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                            //        ScriptHandler.SendData(Client, Data, Position);
                                            //        return;
                                            //    }

                                            //    new Games.CityWar(Map);
                                            //    World.BroadcastMsg(MsgTalk.Create("SYSTEM", "ALLUSERS", Player.Name + " des " + Player.Syndicate.Name + " a déclaré la guerre pour prendre le Canyon", MsgTalk.Channel.GM, 0xFFFFFF));
                                            //}
                                            //else if (IdxTask == 3)
                                            //{
                                            //    if (Player.Syndicate == null)
                                            //    {
                                            //        Position += ScriptHandler.SendText("Vous devez etre dans une guilde pour déclarer la guerre!", Client, ref Data, Position);
                                            //        Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                            //        Position += ScriptHandler.SendFace(123, Client, ref Data, Position);
                                            //        Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                            //        ScriptHandler.SendData(Client, Data, Position);
                                            //        return;
                                            //    }

                                            //    Syndicate.Member Member = Player.Syndicate.GetMemberInfo(Player.UniqId);
                                            //    if (Member == null || Member.Rank != 100)
                                            //    {
                                            //        Position += ScriptHandler.SendText("Vous devez etre chef d'une guilde pour déclarer la guerre!", Client, ref Data, Position);
                                            //        Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                            //        Position += ScriptHandler.SendFace(123, Client, ref Data, Position);
                                            //        Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                            //        ScriptHandler.SendData(Client, Data, Position);
                                            //        return;
                                            //    }

                                            //    Map Map = null;
                                            //    if (!World.AllMaps.TryGetValue(1000, out Map))
                                            //        return;

                                            //    if (Map == null || Map.InWar)
                                            //    {
                                            //        Position += ScriptHandler.SendText("Il y a déjà une guerre dans cette région!", Client, ref Data, Position);
                                            //        Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                            //        Position += ScriptHandler.SendFace(123, Client, ref Data, Position);
                                            //        Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                            //        ScriptHandler.SendData(Client, Data, Position);
                                            //        return;
                                            //    }

                                            //    new Games.CityWar(Map);
                                            //    World.BroadcastMsg(MsgTalk.Create("SYSTEM", "ALLUSERS", Player.Name + " des " + Player.Syndicate.Name + " a déclaré la guerre pour prendre le Désert!", MsgTalk.Channel.GM, 0xFFFFFF));
                                            //}
                                            //else if (IdxTask == 4)
                                            //{
                                            //    if (Player.Syndicate == null)
                                            //    {
                                            //        Position += ScriptHandler.SendText("Vous devez etre dans une guilde pour déclarer la guerre!", Client, ref Data, Position);
                                            //        Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                            //        Position += ScriptHandler.SendFace(123, Client, ref Data, Position);
                                            //        Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                            //        ScriptHandler.SendData(Client, Data, Position);
                                            //        return;
                                            //    }

                                            //    Syndicate.Member Member = Player.Syndicate.GetMemberInfo(Player.UniqId);
                                            //    if (Member == null || Member.Rank != 100)
                                            //    {
                                            //        Position += ScriptHandler.SendText("Vous devez etre chef d'une guilde pour déclarer la guerre!", Client, ref Data, Position);
                                            //        Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                            //        Position += ScriptHandler.SendFace(123, Client, ref Data, Position);
                                            //        Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                            //        ScriptHandler.SendData(Client, Data, Position);
                                            //        return;
                                            //    }

                                            //    Map Map = null;
                                            //    if (!World.AllMaps.TryGetValue(1015, out Map))
                                            //        return;

                                            //    if (Map == null || Map.InWar)
                                            //    {
                                            //        Position += ScriptHandler.SendText("Il y a déjà une guerre dans cette région!", Client, ref Data, Position);
                                            //        Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                            //        Position += ScriptHandler.SendFace(123, Client, ref Data, Position);
                                            //        Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                            //        ScriptHandler.SendData(Client, Data, Position);
                                            //        return;
                                            //    }

                                            //    new Games.CityWar(Map);
                                            //    World.BroadcastMsg(MsgTalk.Create("SYSTEM", "ALLUSERS", Player.Name + " des " + Player.Syndicate.Name + " a déclaré la guerre pour prendre les Iles!", MsgTalk.Channel.GM, 0xFFFFFF));
                                            //}
                                            break;
                                        }
                                    case 390: //RocherDeL'Amour
                                        {
                                            if (IdxTask == 1)
                                            {
                                                Position += ScriptHandler.SendText("Le mariage est une promesse éternelle. Voulez-vous l'aimer par coeur durant toute votre vie?", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(2, "Oui, je veux me marier.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Non, j'aime la vie de célibataire.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 2)
                                            {
                                                Position += ScriptHandler.SendText("Etes-vous prêt au mariage?", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(3, "Oui, j'ai tout préparé.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Non, je suis en train de tout préparer.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 3)
                                            {
                                                Position += ScriptHandler.SendText("Cliquez sur votre futur mari/femme pour le/la demander en mariage.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Ok.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                                Client.Send(MsgAction.Create(Player, 1067, MsgAction.Action.PostCmd));
                                            }
                                            else if (IdxTask == 4)
                                            {
                                                Position += ScriptHandler.SendText("Si vous êtes béni, vous pouvez obtenir une heure de points d'expérience doubles, une fois par jour. Le voulez-vous?", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(5, "Oui, S.V.P.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Non, attendez un instant.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 5)
                                            {
                                                if (Player.BlessEndTime == 0)
                                                {
                                                    Position += ScriptHandler.SendText("Désolé, vous n'êtes pas béni.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                }
                                                else
                                                {
                                                    if (true)
                                                    {
                                                        Position += ScriptHandler.SendText("Désolé, vous avez déjà utilisé votre Exp double.", Client, ref Data, Position);
                                                        Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                        Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                        Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                        ScriptHandler.SendData(Client, Data, Position);
                                                    }
                                                    else
                                                    {
                                                        Player.DblExpEndTime = Environment.TickCount + 3600000;
                                                        Player.Send(MsgUserAttrib.Create(Player, (Int32)((Player.DblExpEndTime - Environment.TickCount) / 1000), MsgUserAttrib.Type.DblExpTime));
                                                    }
                                                }
                                            }
                                            break;
                                        }
                                    case 941: //DieuD'aptitude
                                        {
                                            if (IdxTask == 1)
                                            {
                                                Position += ScriptHandler.SendText("Quelle compétence voulez-vous améliorer? Il vous en coutera le niveau actuel en Balle Exp. Par exemple, de passer du niveau 1 à 2, il faudra 1 Balle Exp.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(10, "Sabre", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(11, "Épée", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(12, "Glaive", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(13, "Hache", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(14, "Marteau", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(15, "Baton", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(16, "Dague", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Rien.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(42, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 2)
                                            {
                                                Position += ScriptHandler.SendText("Quelle compétence voulez-vous améliorer? Il vous en coutera le niveau actuel en Balle Exp. Par exemple, de passer du niveau 1 à 2, il faudra 1 Balle Exp.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(17, "Arc", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(18, "HacheD'arme", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(19, "Lance", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(20, "Canne", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(21, "Hallebarde", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Rien.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(42, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 3)
                                            {
                                                Position += ScriptHandler.SendText("Quelle compétence voulez-vous améliorer? Il vous en coutera le niveau actuel en Balle Exp. Par exemple, de passer du niveau 1 à 2, il faudra 1 Balle Exp.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(22, "Boxe", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(23, "Bouclier", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Rien.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(42, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask >= 10 && IdxTask <= 23)
                                            {
                                                Int16[] Types = new Int16[] { 410, 420, 421, 450, 460, 480, 490, 500, 530, 560, 561, 580, 000, 900 };

                                                Int16 Type = Types[IdxTask - 10];

                                                WeaponSkill Skill = Player.GetWeaponSkillByType(Type);
                                                if (Skill == null || Skill.Level < 1)
                                                {
                                                    Position += ScriptHandler.SendText("Votre compétence doit etre au moins au niveau 1!", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(42, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    return;
                                                }

                                                if (Skill.Level >= 12)
                                                {
                                                    Position += ScriptHandler.SendText("Votre compétence ne peut plus etre améliorée!", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(42, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    return;
                                                }

                                                if (!Player.InventoryContains(723700, Skill.Level))
                                                {
                                                    Position += ScriptHandler.SendText("Vous n'avez pas " + Skill.Level + " Balles Exp!", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(42, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    return;
                                                }

                                                Player.DelItem(723700, Skill.Level, true);

                                                Skill.Level++;
                                                Skill.Exp = 0;
                                                Player.Send(MsgWeaponSkill.Create(Skill));

                                                Position += ScriptHandler.SendText("Votre compétence a été améliorée!", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Merci.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(42, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            break;
                                        }
                                    case 925: //BoîteSurprise 
                                    case 926: //BoîteSurprise 
                                    case 927: //BoîteSurprise 
                                    case 928: //BoîteSurprise 
                                    case 929: //BoîteSurprise 
                                    case 930: //BoîteSurprise 
                                    case 931: //BoîteSurprise 
                                    case 932: //BoîteSurprise 
                                    case 933: //BoîteSurprise 
                                    case 934: //BoîteSurprise 
                                    case 935: //BoîteSurprise 
                                    case 936: //BoîteSurprise 
                                    case 937: //BoîteSurprise 
                                    case 938: //BoîteSurprise 
                                    case 939: //BoîteSurprise 
                                    case 940: //BoîteSurprise 
                                    case 942: //BoîteSurprise 
                                    case 943: //BoîteSurprise 
                                    case 944: //BoîteSurprise 
                                    case 945: //BoîteSurprise 
                                        {
                                            if (!ItemHandler.GenerateLoto(Player))
                                            {
                                                Position += ScriptHandler.SendText("Votre inventaire est plein!", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(102, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                                break;
                                            }
                                            break;
                                        }
                                    case 1550: //ForgeronLee
                                        {
                                            if (IdxTask >= 1 && IdxTask <= 7)
                                            {
                                                Item Item = null;
                                                if (IdxTask == 1)
                                                    Item = Player.GetItemByPos(1);
                                                else if (IdxTask == 2)
                                                    Item = Player.GetItemByPos(2);
                                                else if (IdxTask == 3)
                                                    Item = Player.GetItemByPos(3);
                                                else if (IdxTask == 5)
                                                    Item = Player.GetItemByPos(5);
                                                else if (IdxTask == 6)
                                                    Item = Player.GetItemByPos(6);
                                                else if (IdxTask == 7)
                                                    Item = Player.GetItemByPos(8);

                                                if (Item == null)
                                                {
                                                    Position += ScriptHandler.SendText("Vous devez équiper l'objet.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(96, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    break;
                                                }

                                                if (IdxTask == 5 && Item.Id / 1000 != 900)
                                                {
                                                    Position += ScriptHandler.SendText("Vous devez équiper un bouclier.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(96, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    break;
                                                }

                                                if (Item.CurDura != Item.MaxDura)
                                                {
                                                    Position += ScriptHandler.SendText("Vous devez réparer l'objet avant de l'améliorer.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(96, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    break;
                                                }

                                                if (Item.Gem1 != 0 && Item.Gem2 != 0)
                                                {
                                                    Position += ScriptHandler.SendText("Je ne peux pas améliorer cet objet.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(96, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    break;
                                                }

                                                if (Item.Gem1 == 0)
                                                {
                                                    if (!Player.InventoryContains(1088000, 15))
                                                    {
                                                        Position += ScriptHandler.SendText("Vous n'avez pas assez de perle de dragon.", Client, ref Data, Position);
                                                        Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                        Position += ScriptHandler.SendFace(96, Client, ref Data, Position);
                                                        Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                        ScriptHandler.SendData(Client, Data, Position);
                                                        break;
                                                    }
                                                    Player.DelItem(1088000, 15, true);
                                                    Item.Gem1 = 255;
                                                }
                                                else if (Item.Gem2 == 0)
                                                {
                                                    if (!Player.InventoryContains(1200005, 1) && !Player.InventoryContains(1200006, 7))
                                                    {
                                                        Position += ScriptHandler.SendText("Vous n'avez pas de foreuse de diamant ou vous n'avez pas assez de foreuse d'étoile.", Client, ref Data, Position);
                                                        Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                        Position += ScriptHandler.SendFace(96, Client, ref Data, Position);
                                                        Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                        ScriptHandler.SendData(Client, Data, Position);
                                                        break;
                                                    }
                                                    if (Player.InventoryContains(1200006, 7))
                                                    {
                                                        Player.DelItem(1200006, 7, true);
                                                        Item.Gem2 = 255;
                                                    }
                                                    else
                                                    {
                                                        if (MyMath.Success(15.0))
                                                        {
                                                            Player.DelItem(1200005, 1, true);
                                                            Item.Gem2 = 255;
                                                        }
                                                        else
                                                        {
                                                            Item Treasure = Player.GetItemById(1200005);
                                                            Treasure.Id++;

                                                            Player.UpdateItem(Treasure);

                                                            Position += ScriptHandler.SendText("Votre foreuse de diamant a brisé avant de forer le deuxième trou. Je suis désolé", Client, ref Data, Position);
                                                            Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                            Position += ScriptHandler.SendFace(96, Client, ref Data, Position);
                                                            Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                            ScriptHandler.SendData(Client, Data, Position);
                                                            break;
                                                        }
                                                    }
                                                }

                                                Player.UpdateItem(Item);
                                                MyMath.GetEquipStats(Player);

                                                Position += ScriptHandler.SendText("Votre équipement a été amélioré!", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Merci.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(96, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            break;
                                        }
                                    case 3216: //ChevalierLibre
                                        {
                                            if (IdxTask == 1)
                                            {
                                                if (!Player.InventoryContains(723087, 1))
                                                {
                                                    Position += ScriptHandler.SendText("Vous n'avez pas d'Amulette-Chance!", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Désolé...", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(134, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    return;
                                                }

                                                Player.Send(MsgUserAttrib.Create(Player, Player.Hair, MsgUserAttrib.Type.Hair));

                                                Position += ScriptHandler.SendText("Quelle coiffure voulez-vous?", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(21, "Style 01", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(22, "Style 02", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(23, "Style 03", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(24, "Style 04", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(25, "Style 05", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(74, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                                Client.NpcAccept = false;
                                            }
                                            else if (IdxTask >= 10 && IdxTask <= 50)
                                            {
                                                if (!Client.NpcAccept)
                                                {
                                                    Player.Send(MsgUserAttrib.Create(Player, (Player.Hair - (Player.Hair % 100) + IdxTask), MsgUserAttrib.Type.Hair));

                                                    Position += ScriptHandler.SendText("Vous aimez cette coiffure?", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(IdxTask, "Oui.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(1, "Non.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(134, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    Client.NpcAccept = true;
                                                }
                                                else
                                                {
                                                    if (!Player.InventoryContains(723087, 1))
                                                    {
                                                        Position += ScriptHandler.SendText("Vous n'avez pas d'Amulette-Chance!", Client, ref Data, Position);
                                                        Position += ScriptHandler.SendOption(255, "Désolé...", Client, ref Data, Position);
                                                        Position += ScriptHandler.SendFace(134, Client, ref Data, Position);
                                                        Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                        ScriptHandler.SendData(Client, Data, Position);
                                                        return;
                                                    }

                                                    Player.DelItem(723087, 1, true);

                                                    Player.Hair = (Int16)(Player.Hair - (Player.Hair % 100) + IdxTask);
                                                    World.BroadcastRoomMsg(Player, MsgUserAttrib.Create(Player, Player.Hair, MsgUserAttrib.Type.Hair), true);
                                                    Client.NpcAccept = false;
                                                }
                                            }
                                            break;
                                        }
                                    case 7050: //MaitreD'arme
                                        {
                                            if (IdxTask >= 1 && IdxTask <= 7)
                                            {
                                                Item Item = null;
                                                if (IdxTask == 1)
                                                    Item = Player.GetItemByPos(1);
                                                else if (IdxTask == 2)
                                                    Item = Player.GetItemByPos(2);
                                                else if (IdxTask == 3)
                                                    Item = Player.GetItemByPos(3);
                                                else if (IdxTask == 4)
                                                    Item = Player.GetItemByPos(4);
                                                else if (IdxTask == 5)
                                                    Item = Player.GetItemByPos(5);
                                                else if (IdxTask == 6)
                                                    Item = Player.GetItemByPos(6);
                                                else if (IdxTask == 7)
                                                    Item = Player.GetItemByPos(8);

                                                if (Item == null)
                                                {
                                                    Position += ScriptHandler.SendText("Vous devez équiper l'objet.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(96, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    break;
                                                }

                                                if (Item.CurDura != Item.MaxDura)
                                                {
                                                    Position += ScriptHandler.SendText("Vous devez réparer l'objet avant de l'améliorer.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(96, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    break;
                                                }

                                                Int32 NextId = ItemHandler.GetNextId(Item.Id);
                                                if (NextId == Item.Id)
                                                {
                                                    Position += ScriptHandler.SendText("Je ne peux pas améliorer le niveau de cet objet.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(96, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    break;
                                                }

                                                ItemType.Entry Info;
                                                if (!Database2.AllItems.TryGetValue(NextId, out Info))
                                                {
                                                    Position += ScriptHandler.SendText("Je ne peux pas améliorer le niveau de cet objet.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(96, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    break;
                                                }

                                                if (Info.RequiredLevel < 120 || Info.RequiredLevel > 130)
                                                {
                                                    Position += ScriptHandler.SendText("Je ne peux pas améliorer le niveau de cet objet.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(96, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    break;
                                                }

                                                if (Info.RequiredLevel > Player.Level)
                                                {
                                                    Position += ScriptHandler.SendText("Vous n'avez pas le niveau requis pour améliorer le niveau de cet objet.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(96, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    break;
                                                }

                                                if (!Player.InventoryContains(1088000, 1))
                                                {
                                                    Position += ScriptHandler.SendText("Vous n'avez pas de Perle de Dragon.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(96, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    break;
                                                }

                                                Player.DelItem(1088000, 1, true);

                                                Item.Id = NextId;
                                                Item.CurDura = ItemHandler.GetMaxDura(NextId);
                                                Item.MaxDura = ItemHandler.GetMaxDura(NextId);

                                                Byte DuraEffect = 0;
                                                if (Item.Gem1 - (Item.Gem1 % 10) == 40) //Kylin
                                                {
                                                    DuraEffect += (Byte)(Item.Gem1 % 10);
                                                    if (Item.Gem1 % 10 == 3)
                                                        DuraEffect++;
                                                }

                                                if (Item.Gem2 - (Item.Gem2 % 10) == 40) //Kylin
                                                {
                                                    DuraEffect += (Byte)(Item.Gem2 % 10);
                                                    if (Item.Gem2 % 10 == 3)
                                                        DuraEffect++;
                                                }

                                                Double Bonus = 1.0;
                                                Bonus += 0.5 * DuraEffect;

                                                Item.MaxDura = (UInt16)((Double)Item.MaxDura * Bonus);
                                                Item.CurDura = Item.MaxDura;

                                                Player.UpdateItem(Item);
                                                MyMath.GetEquipStats(Player);

                                                Position += ScriptHandler.SendText("Votre équipement a été amélioré!", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Merci.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(96, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            break;
                                        }
                                    case 10002: //Coiffeur
                                        {
                                            if (IdxTask == 1)
                                            {
                                                Player.Send(MsgUserAttrib.Create(Player, Player.Hair, MsgUserAttrib.Type.Hair));

                                                Position += ScriptHandler.SendText("Quelle coiffure voulez-vous?", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(30, "Style 01", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(31, "Style 02", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(32, "Style 03", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(33, "Style 04", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(34, "Style 05", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(35, "Style 06", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(3, "Page suivante.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(134, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                                Client.NpcAccept = false;
                                            }
                                            else if (IdxTask == 2)
                                            {
                                                Player.Send(MsgUserAttrib.Create(Player, Player.Hair, MsgUserAttrib.Type.Hair));

                                                Position += ScriptHandler.SendText("Quelle coiffure voulez-vous?", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(10, "Style 01", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(11, "Style 02", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(12, "Style 03", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(13, "Style 04", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(14, "Style 05", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(15, "Style 06", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(16, "Style 07", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(17, "Style 08", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(134, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                                Client.NpcAccept = false;
                                            }
                                            else if (IdxTask == 3)
                                            {
                                                Player.Send(MsgUserAttrib.Create(Player, Player.Hair, MsgUserAttrib.Type.Hair));

                                                Position += ScriptHandler.SendText("Quelle coiffure voulez-vous?", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(36, "Style 07", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(37, "Style 08", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(38, "Style 09", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(39, "Style 10", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(40, "Style 11", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(41, "Style 12", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(1, "Page précédente.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(134, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                                Client.NpcAccept = false;
                                            }
                                            else if (IdxTask >= 10 && IdxTask <= 50)
                                            {
                                                if (!Client.NpcAccept)
                                                {
                                                    Player.Send(MsgUserAttrib.Create(Player, (Player.Hair - (Player.Hair % 100) + IdxTask), MsgUserAttrib.Type.Hair));

                                                    Position += ScriptHandler.SendText("Vous aimez cette coiffure?", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(IdxTask, "Oui.", Client, ref Data, Position);
                                                    if (IdxTask >= 10 && IdxTask < 20)
                                                        Position += ScriptHandler.SendOption(2, "Non.", Client, ref Data, Position);
                                                    else
                                                        Position += ScriptHandler.SendOption(1, "Non.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(134, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    Client.NpcAccept = true;
                                                }
                                                else
                                                {
                                                    if (Player.Money < 500)
                                                    {
                                                        Position += ScriptHandler.SendText("Vous n'avez pas assez d'argent!", Client, ref Data, Position);
                                                        Position += ScriptHandler.SendOption(255, "Désolé...", Client, ref Data, Position);
                                                        Position += ScriptHandler.SendFace(134, Client, ref Data, Position);
                                                        Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                        ScriptHandler.SendData(Client, Data, Position);
                                                        return;
                                                    }

                                                    Player.Money -= 500;
                                                    Player.Send(MsgUserAttrib.Create(Player, Player.Money, MsgUserAttrib.Type.Money));

                                                    Player.Hair = (Int16)(Player.Hair - (Player.Hair % 100) + IdxTask);
                                                    World.BroadcastRoomMsg(Player, MsgUserAttrib.Create(Player, Player.Hair, MsgUserAttrib.Type.Hair), true);
                                                    Client.NpcAccept = false;
                                                }
                                            }
                                            break;
                                        }
                                    case 10003: //Mandarin
                                        {
                                            if (IdxTask == 1)
                                            {
                                                if (Player.Syndicate != null)
                                                {
                                                    Position += ScriptHandler.SendText("Vous devez quitter votre guilde avant d'en créer une.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "D'accord.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(7, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    return;
                                                }

                                                if (Player.Money < 1000000)
                                                {
                                                    Position += ScriptHandler.SendText("Vous n'avez pas assez d'argent, la création d'une guilde coûte 1 million d'argent.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "D'accord.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(7, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    return;
                                                }

                                                Position += ScriptHandler.SendText("Comment voulez-vous nommer votre guilde?", Client, ref Data, Position);
                                                Position += ScriptHandler.SendInput(200, "Elle s'appelle", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Je veux réfléchir.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(7, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 2)
                                            {
                                                Position += ScriptHandler.SendText("Réfléchissez, si vous dégroupez votre guilde, il sera impossible de la reconstituer.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(201, "Dégrouper.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Je veux réfléchir.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(7, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 3)
                                            {
                                                Position += ScriptHandler.SendText("Une guilde forte a besoin d'un fond suffisant. Combien d'argent voulez-vous donnez?", Client, ref Data, Position);
                                                Position += ScriptHandler.SendInput(202, "Je veux donner", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Je dois réfléchir.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(7, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 4)
                                            {
                                                Position += ScriptHandler.SendText("Le chef est le responsable de la guilde. Vous devez réfléchir et choisir une personne convenable.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendInput(203, "J'abdique le pouvoir à..", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Je dois réfléchir.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(7, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 5)
                                            {
                                                Position += ScriptHandler.SendText("La guilde a besoin non seulement d'un chef mais aussi d'un sous-chef. Qui voulez-vous commettre sous-chef?", Client, ref Data, Position);
                                                Position += ScriptHandler.SendInput(204, "Je commet..", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Je dois réfléchir.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(7, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 6)
                                            {
                                                Position += ScriptHandler.SendText("Pour le développement de la guilde, vous devez donner les fonctions aux talents!", Client, ref Data, Position);
                                                Position += ScriptHandler.SendInput(205, "Démettre la fonction de", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Je dois réfléchir.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(7, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 7)
                                            {
                                                Position += ScriptHandler.SendText("De quelle guilde voulez-vous savoir les renseignements?", Client, ref Data, Position);
                                                Position += ScriptHandler.SendInput(206, "Le nom de guilde", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Je dois réfléchir.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(7, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 8)
                                            {
                                                Position += ScriptHandler.SendText("Je m'occupe des guildes de la région. Lorsque vous avez des questions concernant les guildes, vous pouvez me le demander.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(9, "Noter des ennemis.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(10, "Réconcilier avec une guilde.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(11, "Coalition de guilde.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(12, "Annuler la coalition.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(13, "Liste de guilde.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(14, "Liste des membres en ligne.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(15, "Exclure membre.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(16, "Autres options", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(7, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 9)
                                            {
                                                Position += ScriptHandler.SendText("Quelle guilde est votre ennemi?", Client, ref Data, Position);
                                                Position += ScriptHandler.SendInput(207, "Le nom de guilde", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Je dois réfléchir.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(7, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 10)
                                            {
                                                Position += ScriptHandler.SendText("Vous voulez réconcilier avec quelle guilde?", Client, ref Data, Position);
                                                Position += ScriptHandler.SendInput(208, "Le nom de ligue antagoniste", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Je dois réfléchir.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(7, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 11)
                                            {
                                                Position += ScriptHandler.SendText("La coalition aide votre guilde à développer. Après que les deux chefs de guilde sont dans une équipe, ils peuvent se coaliser.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(209, "Ok.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Je dois réfléchir.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(7, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 12)
                                            {
                                                Position += ScriptHandler.SendText("Vous voulez annuler la coalition avec quelle guilde?", Client, ref Data, Position);
                                                Position += ScriptHandler.SendInput(210, "Le nom de guilde alliée", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Je dois réfléchir.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(7, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 13)
                                            {
                                                Position += ScriptHandler.SendText("Not implemented yet!", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Ok.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(7, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 14)
                                            {
                                                Position += ScriptHandler.SendText("Not implemented yet!", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Ok.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(7, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 15)
                                            {
                                                Position += ScriptHandler.SendText("Quel membre voulez-vous exclure?", Client, ref Data, Position);
                                                Position += ScriptHandler.SendInput(211, "Nom de membre", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Je dois réfléchir.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(7, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 16)
                                            {
                                                Position += ScriptHandler.SendText("Je m'occupe des guildes de la région. Lorsque vous avez des questions concernant les guildes, vous pouvez me le demander.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(17, "Créer une filiade.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(18, "Désigner un directeur de filiade.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(19, "Fusioner les fonds.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(20, "Autres options", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(7, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 17)
                                            {
                                                Position += ScriptHandler.SendText("Not implemented yet!", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Ok.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(7, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 18)
                                            {
                                                Position += ScriptHandler.SendText("Not implemented yet!", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Ok.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(7, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 19)
                                            {
                                                Position += ScriptHandler.SendText("Not implemented yet!", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Ok.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(7, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 20)
                                            {
                                                Position += ScriptHandler.SendText("Je m'occupe des guildes de la région. Lorsque vous avez des questions concernant les guildes, vous pouvez me le demander.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(1, "Créer une guilde.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(2, "Dégrouper la guilde.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(3, "Donation de guilde.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(4, "Chef abdique.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(5, "Commettre à sous-chef.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(6, "Décharger.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(7, "Consulter une guilde.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(8, "Autres options", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(7, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 200)
                                            {
                                                if (Player.Level < 90)
                                                {
                                                    Position += ScriptHandler.SendText("Vous devez etre au moins niveau 90 pour créer une guilde.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Ok.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(7, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    return;
                                                }

                                                if (Player.Syndicate != null)
                                                {
                                                    Position += ScriptHandler.SendText("Vous devez quitter votre guilde avant d'en créer une.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "D'accord.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(7, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    return;
                                                }

                                                if (Player.Money < 1000000)
                                                {
                                                    Position += ScriptHandler.SendText("Vous n'avez pas assez d'argent, la création d'une guilde coûte 1 million d'argent.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "D'accord.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(7, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    return;
                                                }

                                                foreach (Syndicate.Info Syn in World.AllSyndicates.Values)
                                                {
                                                    if (Syn.Name == Text)
                                                    {
                                                        Position += ScriptHandler.SendText("Désoler... Ce nom de guilde existe déjà!", Client, ref Data, Position);
                                                        Position += ScriptHandler.SendOption(255, "D'accord.", Client, ref Data, Position);
                                                        Position += ScriptHandler.SendFace(7, Client, ref Data, Position);
                                                        Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                        ScriptHandler.SendData(Client, Data, Position);
                                                        return;
                                                    }
                                                }

                                                if (Syndicate.Info.Create(Player, Text))
                                                {
                                                    Player.Money -= 1000000;
                                                    Player.Send(MsgUserAttrib.Create(Player, Player.Money, MsgUserAttrib.Type.Money));

                                                    Player.Send(MsgSynAttrInfo.Create(Player.UniqId, Player.Syndicate));
                                                    World.BroadcastRoomMsg(Player, MsgPlayer.Create(Player), true);

                                                    World.BroadcastMsg(MsgTalk.Create("SYSTEM", "ALLUSERS", String.Format(Client.GetStr("STR_SYN_CREATE"), Player.Name, Text), MsgTalk.Channel.GM, 0xFFFFFF));

                                                    Database.Save(Player);
                                                }
                                            }
                                            else if (IdxTask == 201)
                                            {
                                                if (Player.Syndicate != null)
                                                {
                                                    if (Player.UniqId == Player.Syndicate.Leader.UniqId)
                                                    {
                                                        String Name = Player.Syndicate.Name;
                                                        Syndicate.Info.Delete(Player.Syndicate.UniqId);
                                                        World.BroadcastMsg(MsgTalk.Create("SYSTEM", "ALLUSERS", String.Format(Client.GetStr("STR_SYN_DISBAND"), Name), MsgTalk.Channel.GM, 0xFFFFFF));
                                                    }
                                                }
                                            }
                                            else if (IdxTask == 202)
                                            {
                                                if (Player.Syndicate != null)
                                                {
                                                    Int32 Donation = 0;
                                                    Int32.TryParse(Text, out Donation);

                                                    if (Donation <= 0)
                                                        return;

                                                    if (Player.Syndicate.DonateMoney(Player, Donation))
                                                        Player.Send(MsgSynAttrInfo.Create(Player.UniqId, Player.Syndicate));
                                                }
                                            }
                                            else if (IdxTask == 203)
                                            {
                                                if (Player.Syndicate != null)
                                                {
                                                    if (Player.UniqId != Player.Syndicate.Leader.UniqId)
                                                    {
                                                        Position += ScriptHandler.SendText("Seul le chef de guilde peut commettre un chef.", Client, ref Data, Position);
                                                        Position += ScriptHandler.SendOption(255, "D'accord.", Client, ref Data, Position);
                                                        Position += ScriptHandler.SendFace(7, Client, ref Data, Position);
                                                        Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                        ScriptHandler.SendData(Client, Data, Position);
                                                        return;
                                                    }

                                                    Syndicate.Info Syn = Player.Syndicate;
                                                    Syndicate.Member Leader = Syn.Leader;
                                                    Syndicate.Member Member = Syn.GetMemberInfo(Text);
                                                    Player Target = null;

                                                    if (Member != null && World.AllPlayers.TryGetValue(Member.UniqId, out Target))
                                                    {
                                                        lock (Syn.Members)
                                                        {
                                                            Syn.Members.Remove(Member.UniqId);

                                                            Leader.Rank = 50;
                                                            Member.Rank = 100;

                                                            Syn.Members.Add(Leader.UniqId, Leader);
                                                            Syn.Leader = Member;

                                                            World.SynThread.AddToQueue(Syn, "LeaderUID", Syn.Leader.UniqId);
                                                            World.SynThread.AddToQueue(Syn, "LeaderName", Syn.Leader.Name);
                                                        }

                                                        Player.Send(MsgSynAttrInfo.Create(Player.UniqId, Syn));
                                                        Target.Send(MsgSynAttrInfo.Create(Target.UniqId, Syn));

                                                        World.BroadcastRoomMsg(Player, MsgPlayer.Create(Player), true);
                                                        World.BroadcastRoomMsg(Target, MsgPlayer.Create(Target), true);

                                                        Database.Save(Player);
                                                        Database.Save(Target);
                                                    }
                                                    else
                                                    {
                                                        Position += ScriptHandler.SendText("Le joueur doit être en-ligne et dans votre guilde.", Client, ref Data, Position);
                                                        Position += ScriptHandler.SendOption(255, "D'accord.", Client, ref Data, Position);
                                                        Position += ScriptHandler.SendFace(7, Client, ref Data, Position);
                                                        Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                        ScriptHandler.SendData(Client, Data, Position);
                                                        return;
                                                    }
                                                }
                                            }
                                            else if (IdxTask == 204)
                                            {
                                                if (Player.Syndicate != null)
                                                {
                                                    if (Player.UniqId != Player.Syndicate.Leader.UniqId)
                                                    {
                                                        Position += ScriptHandler.SendText("Seul le chef de guilde peut commettre un chef.", Client, ref Data, Position);
                                                        Position += ScriptHandler.SendOption(255, "D'accord.", Client, ref Data, Position);
                                                        Position += ScriptHandler.SendFace(7, Client, ref Data, Position);
                                                        Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                        ScriptHandler.SendData(Client, Data, Position);
                                                        return;
                                                    }

                                                    Syndicate.Info Syn = Player.Syndicate;
                                                    Syndicate.Member Member = Syn.GetMemberInfo(Text);
                                                    Player Target = null;

                                                    if (Member != null && World.AllPlayers.TryGetValue(Member.UniqId, out Target))
                                                    {
                                                        if (Member.Rank == 100)
                                                            return;

                                                        Member.Rank = 90;
                                                        Target.Send(MsgSynAttrInfo.Create(Target.UniqId, Syn));
                                                        World.BroadcastRoomMsg(Target, MsgPlayer.Create(Target), true);

                                                        Database.Save(Target);
                                                    }
                                                    else
                                                    {
                                                        Position += ScriptHandler.SendText("Le joueur doit être en-ligne et dans votre guilde.", Client, ref Data, Position);
                                                        Position += ScriptHandler.SendOption(255, "D'accord.", Client, ref Data, Position);
                                                        Position += ScriptHandler.SendFace(7, Client, ref Data, Position);
                                                        Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                        ScriptHandler.SendData(Client, Data, Position);
                                                        return;
                                                    }
                                                }
                                            }
                                            else if (IdxTask == 205)
                                            {
                                                if (Player.Syndicate != null)
                                                {
                                                    if (Player.UniqId != Player.Syndicate.Leader.UniqId)
                                                    {
                                                        Position += ScriptHandler.SendText("Seul le chef de guilde peut commettre un chef.", Client, ref Data, Position);
                                                        Position += ScriptHandler.SendOption(255, "D'accord.", Client, ref Data, Position);
                                                        Position += ScriptHandler.SendFace(7, Client, ref Data, Position);
                                                        Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                        ScriptHandler.SendData(Client, Data, Position);
                                                        return;
                                                    }

                                                    Syndicate.Info Syn = Player.Syndicate;
                                                    Syndicate.Member Member = Syn.GetMemberInfo(Text);
                                                    Player Target = null;

                                                    if (Member != null && World.AllPlayers.TryGetValue(Member.UniqId, out Target))
                                                    {
                                                        if (Member.Rank == 50 || Member.Rank == 100)
                                                            return;

                                                        Member.Rank = 50;
                                                        Target.Send(MsgSynAttrInfo.Create(Target.UniqId, Syn));
                                                        World.BroadcastRoomMsg(Target, MsgPlayer.Create(Target), true);

                                                        Database.Save(Target);
                                                    }
                                                    else
                                                    {
                                                        Position += ScriptHandler.SendText("Le joueur doit être en-ligne et dans votre guilde.", Client, ref Data, Position);
                                                        Position += ScriptHandler.SendOption(255, "D'accord.", Client, ref Data, Position);
                                                        Position += ScriptHandler.SendFace(7, Client, ref Data, Position);
                                                        Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                        ScriptHandler.SendData(Client, Data, Position);
                                                        return;
                                                    }
                                                }
                                            }
                                            else if (IdxTask == 206)
                                            {
                                                lock (World.AllSyndicates)
                                                {
                                                    foreach (Syndicate.Info Syn in World.AllSyndicates.Values)
                                                    {
                                                        if (Syn.Name != Text)
                                                            continue;

                                                        Position += ScriptHandler.SendText("Le nom de guilde: " + Syn.Name, Client, ref Data, Position);
                                                        Position += ScriptHandler.SendText("\nLe chef de guilde: " + Syn.Leader.Name, Client, ref Data, Position);
                                                        Position += ScriptHandler.SendText("\nNombre de membres: " + Syn.Members.Count, Client, ref Data, Position);
                                                        Position += ScriptHandler.SendText("\nLe fond de guilde: " + Syn.Money, Client, ref Data, Position);
                                                        Position += ScriptHandler.SendOption(255, "D'accord.", Client, ref Data, Position);
                                                        Position += ScriptHandler.SendFace(7, Client, ref Data, Position);
                                                        Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                        ScriptHandler.SendData(Client, Data, Position);
                                                        return;
                                                    }
                                                }

                                                Position += ScriptHandler.SendText("Cette guilde n'existe pas!", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "D'accord.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(7, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 211)
                                            {
                                                if (Player.Syndicate == null)
                                                    return;

                                                if (Player.Syndicate.Leader.UniqId != Player.UniqId)
                                                {
                                                    Position += ScriptHandler.SendText("Seul le chef de guilde peut exclure un membre.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "D'accord.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(7, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    return;
                                                }

                                                if (Player.Name == Text)
                                                    return;

                                                Syndicate.Member Member = Player.Syndicate.GetMemberInfo(Text);
                                                if (Member == null)
                                                {
                                                    Position += ScriptHandler.SendText("Ce membre n'existe pas!", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "D'accord.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(7, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    return;
                                                }

                                                Player.Syndicate.DelMember(Member, true);
                                            }
                                            break;
                                        }
                                    case 10062: //ArtisantMagique
                                        {
                                            if (IdxTask == 1)
                                            {
                                                Position += ScriptHandler.SendText("Vous voulez améliorer la qualité de quel objet?", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(3, "Casque/Boucles", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(4, "Collier/Sac", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(5, "Armure/Robe", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(6, "Arme (Main droite)", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(7, "Arme/Bouclier (Main gauche)", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(8, "Bague/Anneau/Bracelets", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(9, "Bottes", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Rien.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(67, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 2)
                                            {
                                                Position += ScriptHandler.SendText("Vous voulez améliorer le niveau de quel objet?", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(10, "Casque/Boucles", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(11, "Collier/Sac", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(12, "Armure/Robe", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(13, "Arme (Main droite)", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(14, "Arme/Bouclier (Main gauche)", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(15, "Bague/Anneau/Bracelets", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(16, "Bottes", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Rien.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(67, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask >= 3 && IdxTask <= 9)
                                            {
                                                Item Item = null;
                                                if (IdxTask == 3)
                                                    Item = Player.GetItemByPos(1);
                                                else if (IdxTask == 4)
                                                    Item = Player.GetItemByPos(2);
                                                else if (IdxTask == 5)
                                                    Item = Player.GetItemByPos(3);
                                                else if (IdxTask == 6)
                                                    Item = Player.GetItemByPos(4);
                                                else if (IdxTask == 7)
                                                    Item = Player.GetItemByPos(5);
                                                else if (IdxTask == 8)
                                                    Item = Player.GetItemByPos(6);
                                                else if (IdxTask == 9)
                                                    Item = Player.GetItemByPos(8);

                                                if (Item == null)
                                                {
                                                    Position += ScriptHandler.SendText("Vous devez équiper l'objet.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(67, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    break;
                                                }

                                                if (Item.Id % 10 == 0 || Item.Id % 10 == 9)
                                                {
                                                    Position += ScriptHandler.SendText("Je ne peux pas améliorer la qualité de cet objet.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(67, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    break;
                                                }

                                                if (Item.CurDura != Item.MaxDura)
                                                {
                                                    Position += ScriptHandler.SendText("Vous devez réparer l'objet avant de l'améliorer.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(67, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    break;
                                                }

                                                Double Chance;
                                                Int32 NextId;
                                                if (!ItemHandler.GetUpQualityInfo(Item, out Chance, out NextId))
                                                {
                                                    Position += ScriptHandler.SendText("Je ne peux pas améliorer la qualité de cet objet.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(67, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    break;
                                                }

                                                Int32 Amount = (Int32)Math.Ceiling(100.00 / Chance + 1);
                                                if (!Client.NpcAccept)
                                                {
                                                    Position += ScriptHandler.SendText("Je peux améliorer la qualité de cet objet si vous me donnez " + Amount.ToString() + " Perle de Dragon. Voulez-vous?", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(IdxTask, "Oui, S.V.P.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Non, merci.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(67, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    Client.NpcAccept = true;
                                                    break;
                                                }

                                                if (!Player.InventoryContains(1088000, Amount))
                                                {
                                                    Position += ScriptHandler.SendText("Vous n'avez pas assez de Perle de Dragon.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(67, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    break;
                                                }

                                                Player.DelItem(1088000, Amount, true);

                                                if (Item.Gem1 == 0 && MyMath.Success(1.75 * Database.Rates.Socket))
                                                    Item.Gem1 = 255;
                                                else if (Item.Gem2 == 0 && MyMath.Success(0.75 * Database.Rates.Socket))
                                                    Item.Gem2 = 255;

                                                Item.Id = NextId;
                                                Item.CurDura = ItemHandler.GetMaxDura(NextId);
                                                Item.MaxDura = ItemHandler.GetMaxDura(NextId);

                                                Byte DuraEffect = 0;
                                                if (Item.Gem1 - (Item.Gem1 % 10) == 40) //Kylin
                                                {
                                                    DuraEffect += (Byte)(Item.Gem1 % 10);
                                                    if (Item.Gem1 % 10 == 3)
                                                        DuraEffect++;
                                                }

                                                if (Item.Gem2 - (Item.Gem2 % 10) == 40) //Kylin
                                                {
                                                    DuraEffect += (Byte)(Item.Gem2 % 10);
                                                    if (Item.Gem2 % 10 == 3)
                                                        DuraEffect++;
                                                }

                                                Double Bonus = 1.0;
                                                Bonus += 0.5 * DuraEffect;

                                                Item.MaxDura = (UInt16)((Double)Item.MaxDura * Bonus);
                                                Item.CurDura = Item.MaxDura;

                                                Player.UpdateItem(Item);
                                                MyMath.GetEquipStats(Player);
                                            }
                                            else if (IdxTask >= 10 && IdxTask <= 16)
                                            {
                                                Item Item = null;
                                                if (IdxTask == 10)
                                                    Item = Player.GetItemByPos(1);
                                                else if (IdxTask == 11)
                                                    Item = Player.GetItemByPos(2);
                                                else if (IdxTask == 12)
                                                    Item = Player.GetItemByPos(3);
                                                else if (IdxTask == 13)
                                                    Item = Player.GetItemByPos(4);
                                                else if (IdxTask == 14)
                                                    Item = Player.GetItemByPos(5);
                                                else if (IdxTask == 15)
                                                    Item = Player.GetItemByPos(6);
                                                else if (IdxTask == 16)
                                                    Item = Player.GetItemByPos(8);

                                                if (Item == null)
                                                {
                                                    Position += ScriptHandler.SendText("Vous devez équiper l'objet.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(67, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    break;
                                                }

                                                if (Item.CurDura != Item.MaxDura)
                                                {
                                                    Position += ScriptHandler.SendText("Vous devez réparer l'objet avant de l'améliorer.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(67, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    break;
                                                }

                                                Double Chance;
                                                Int32 NextId;
                                                if (!ItemHandler.GetUpLevelInfo(Item, out Chance, out NextId))
                                                {
                                                    Position += ScriptHandler.SendText("Je ne peux pas améliorer le niveau de cet objet.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(67, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    break;
                                                }

                                                ItemType.Entry Info;
                                                if (!Database2.AllItems.TryGetValue(NextId, out Info))
                                                {
                                                    Position += ScriptHandler.SendText("Je ne peux pas améliorer le niveau de cet objet.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(67, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    break;
                                                }

                                                if (Info.RequiredLevel > Player.Level)
                                                {
                                                    Position += ScriptHandler.SendText("Vous n'avez pas le niveau requis pour améliorer le niveau de cet objet.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(67, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    break;
                                                }

                                                Int32 Amount = (Int32)Math.Ceiling((100.00 / Chance + 1) * 1.2);
                                                if (Amount > 40)
                                                    Amount = 40;

                                                if (!Client.NpcAccept)
                                                {
                                                    Position += ScriptHandler.SendText("Je peux améliorer le niveau de cet objet si vous me donnez " + Amount.ToString() + " Météores. Voulez-vous?", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(IdxTask, "Oui, S.V.P.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Non, merci.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(67, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    Client.NpcAccept = true;
                                                    break;
                                                }

                                                if (!Player.InventoryContains(1088001, Amount))
                                                {
                                                    Position += ScriptHandler.SendText("Vous n'avez pas assez de Météores.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(67, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    break;
                                                }

                                                Player.DelItem(1088001, Amount, true);

                                                if (Item.Gem1 == 0 && MyMath.Success(1.25 * Database.Rates.Socket))
                                                    Item.Gem1 = 255;
                                                else if (Item.Gem2 == 0 && MyMath.Success(0.25 * Database.Rates.Socket))
                                                    Item.Gem2 = 255;

                                                Item.Id = NextId;
                                                Item.CurDura = ItemHandler.GetMaxDura(NextId);
                                                Item.MaxDura = ItemHandler.GetMaxDura(NextId);

                                                Byte DuraEffect = 0;
                                                if (Item.Gem1 - (Item.Gem1 % 10) == 40) //Kylin
                                                {
                                                    DuraEffect += (Byte)(Item.Gem1 % 10);
                                                    if (Item.Gem1 % 10 == 3)
                                                        DuraEffect++;
                                                }

                                                if (Item.Gem2 - (Item.Gem2 % 10) == 40) //Kylin
                                                {
                                                    DuraEffect += (Byte)(Item.Gem2 % 10);
                                                    if (Item.Gem2 % 10 == 3)
                                                        DuraEffect++;
                                                }

                                                Double Bonus = 1.0;
                                                Bonus += 0.5 * DuraEffect;

                                                Item.MaxDura = (UInt16)((Double)Item.MaxDura * Bonus);
                                                Item.CurDura = Item.MaxDura;

                                                Player.UpdateItem(Item);
                                                MyMath.GetEquipStats(Player);
                                            }
                                            break;
                                        }
                                    case 10064: //Tinturier
                                        {
                                            if (IdxTask == 1)
                                            {
                                                Position += ScriptHandler.SendText("Quelle couleur voulez-vous?", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(13, "Orange", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(14, "Bleu Ciel", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(15, "Rouge", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(16, "Bleu", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(17, "Jaune", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(18, "Mauve", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(19, "Blanc", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Rien.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(67, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 2)
                                            {
                                                Position += ScriptHandler.SendText("Quelle couleur voulez-vous?", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(23, "Orange", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(24, "Bleu Ciel", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(25, "Rouge", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(26, "Bleu", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(27, "Jaune", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(28, "Mauve", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(29, "Blanc", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Rien.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(67, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 3)
                                            {
                                                Position += ScriptHandler.SendText("Quelle couleur voulez-vous?", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(33, "Orange", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(34, "Bleu Ciel", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(35, "Rouge", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(36, "Bleu", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(37, "Jaune", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(38, "Mauve", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(39, "Blanc", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Rien.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(67, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask >= 10 && IdxTask < 20)
                                            {
                                                Item Item = Player.GetItemByPos(3);
                                                if (Item == null)
                                                    break;

                                                Int32 NewId = Item.Id - ((Item.Id % 1000) - (Item.Id % 100)) + ((IdxTask - 10) * 100);

                                                if (!Database2.AllItems.ContainsKey(NewId))
                                                    return;

                                                Item.Id = NewId;
                                                Player.UpdateItem(Item);
                                            }
                                            else if (IdxTask >= 20 && IdxTask < 30)
                                            {
                                                Item Item = Player.GetItemByPos(1);
                                                if (Item == null)
                                                    break;

                                                Int32 NewId = Item.Id - ((Item.Id % 1000) - (Item.Id % 100)) + ((IdxTask - 20) * 100);

                                                if (!Database2.AllItems.ContainsKey(NewId))
                                                    return;

                                                Item.Id = NewId;
                                                Player.UpdateItem(Item);
                                            }
                                            else if (IdxTask >= 30 && IdxTask < 40)
                                            {
                                                Item Item = Player.GetItemByPos(5);
                                                if (Item == null)
                                                    break;

                                                Int32 NewId = Item.Id - ((Item.Id % 1000) - (Item.Id % 100)) + ((IdxTask - 30) * 100);

                                                if (Item.Id / 1000 != 900)
                                                    return;

                                                if (!Database2.AllItems.ContainsKey(NewId))
                                                    return;

                                                Item.Id = NewId;
                                                Player.UpdateItem(Item);
                                            }
                                            break;
                                        }
                                    case 10065: //ArtisantDuCiel
                                        {
                                            if (IdxTask == 1)
                                            {
                                                Position += ScriptHandler.SendText("Très bien. Je peux réparer votre sac, mais sachez que tout ce qu'il contient sera détruit!", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(2, "Réparez mon sac.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Rien.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(67, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 2)
                                            {
                                                lock (Player.Items)
                                                {
                                                    Item[] Items = new Item[Player.Items.Count];
                                                    Player.Items.Values.CopyTo(Items, 0);

                                                    foreach (Item Item in Items)
                                                    {
                                                        if (Item.Position != 0)
                                                            continue;

                                                        Player.DelItem(Item.UniqId, true);
                                                    }
                                                    Items = null;
                                                }

                                                Position += ScriptHandler.SendText("Votre sac devrait etre comme neuf!", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Merci.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(67, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            break;
                                        }
                                    case 30000: //Célébrant
                                        {
                                            if (IdxTask == 1)
                                            {
                                                Position += ScriptHandler.SendText("Le mariage est une promesse éternelle. Voulez-vous l'aimer par coeur durant toute votre vie?", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(2, "Oui, je veux me marier.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Non, j'aime la vie de célibataire.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 2)
                                            {
                                                Position += ScriptHandler.SendText("Etes-vous prêt au mariage?", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(3, "Oui, j'ai tout préparé.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Non, je suis en train de tout préparer.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 3)
                                            {
                                                Position += ScriptHandler.SendText("Cliquez sur votre futur mari/femme pour le/la demander en mariage.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Ok.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                                Client.Send(MsgAction.Create(Player, 1067, MsgAction.Action.PostCmd));
                                            }
                                            break;
                                        }
                                    case 35015: //Éthéré
                                        {
                                            if (IdxTask == 1)
                                            {
                                                Position += ScriptHandler.SendText("Il vous faut 5 gemmes mythiques pour augmenter la bénédiction à -1%, 1 gemmes mythiques pour augmenter la bénédiction à -3%,", Client, ref Data, Position);
                                                Position += ScriptHandler.SendText(" 3 gemmes mythiques pour augmenter la bénédiction à -5% et 5 gemmes mythiques pour augmenter la bénédiction à -7%. Quelle objet voulez-vous améliorer?", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(9, "Casque/Boucles", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(10, "Collier/Sac", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(11, "Armure/Robe", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(12, "Arme (Main droite)", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(13, "Arme/Bouclier (Main gauche)", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(14, "Bague/Anneau/Bracelets", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(15, "Bottes", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Rien.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(67, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 2)
                                            {
                                                Position += ScriptHandler.SendText("Voici le WuxingFour. Vous pouvez enchanter votre équipement avec une gemme. L'objet enchanté augmentera votre vie maximale. Il peut également vous aider à composer des objets pour améliorer le +n.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(4, "Comment enchanter des objets?", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(7, "Comment améliorer +n des objets?", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(67, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 3)
                                            {
                                                Position += ScriptHandler.SendText("Vous pouvez améliorer le bonus de vos objets dans le WuxingFour.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Oh, je vois.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(67, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 4)
                                            {
                                                Position += ScriptHandler.SendText("L'objet enchanté peut augmenter votre vie jusqu'à 255. Plus la gemme est de qualité, plus elle augmentera votre vie. L'efficacité de l'enchantement varie également avec le type de gemme.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(5, "Un objet peut etre enchanté plusieurs fois?", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(6, "Quelles gemmes sont requises?", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(67, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 5)
                                            {
                                                Position += ScriptHandler.SendText("Vous pouvez refaire l'enchantement de votre objet. Si le nouvel attribut est plus haut, il remplacera l'ancien. Sinon, vous garderez l'ancien attribut.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(67, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 6)
                                            {
                                                Position += ScriptHandler.SendText("Toutes les gemmes peuvent etre utilisé, ainsi que toutes les qualités de gemmes. Néanmoins, l'efficacité de l'enchantement dépend de ces deux facteurs.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(67, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 7)
                                            {
                                                Position += ScriptHandler.SendText("Si vous avez des objets de meme type, vous pouvez les composer sur votre objet principal pour augmenter son bonus.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(8, "Il n'y a pas de limitation?", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(67, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 8)
                                            {
                                                Position += ScriptHandler.SendText("Le bonus ne peut dépasser le niveau 9 (+9).", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(67, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask >= 9 && IdxTask <= 15)
                                            {
                                                Item Item = null;
                                                if (IdxTask == 9)
                                                    Item = Player.GetItemByPos(1);
                                                else if (IdxTask == 10)
                                                    Item = Player.GetItemByPos(2);
                                                else if (IdxTask == 11)
                                                    Item = Player.GetItemByPos(3);
                                                else if (IdxTask == 12)
                                                    Item = Player.GetItemByPos(4);
                                                else if (IdxTask == 13)
                                                    Item = Player.GetItemByPos(5);
                                                else if (IdxTask == 14)
                                                    Item = Player.GetItemByPos(6);
                                                else if (IdxTask == 15)
                                                    Item = Player.GetItemByPos(8);

                                                if (Item == null)
                                                {
                                                    Position += ScriptHandler.SendText("Vous devez équiper l'objet.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(67, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    break;
                                                }

                                                if (Item.Bless >= 7)
                                                {
                                                    Position += ScriptHandler.SendText("Votre équipement est déjà béni à son maximum.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(67, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    break;
                                                }

                                                Int32 Amount = 0;
                                                if (Item.Bless == 0)
                                                    Amount = 5;
                                                else if (Item.Bless > 0 && Item.Bless < 3)
                                                    Amount = 1;
                                                else if (Item.Bless >= 3 && Item.Bless < 5)
                                                    Amount = 3;
                                                else if (Item.Bless >= 5 && Item.Bless < 7)
                                                    Amount = 5;

                                                if (!Player.InventoryContains(700073, Amount))
                                                {
                                                    Position += ScriptHandler.SendText("Vous n'avez pas assez de gemmes.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(67, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    break;
                                                }

                                                Player.DelItem(700073, Amount, true);

                                                if (Item.Bless == 0)
                                                    Item.Bless = 1;
                                                else if (Item.Bless > 0 && Item.Bless < 3)
                                                    Item.Bless = 3;
                                                else if (Item.Bless >= 3 && Item.Bless < 5)
                                                    Item.Bless = 5;
                                                else if (Item.Bless >= 5 && Item.Bless < 7)
                                                    Item.Bless = 7;

                                                Player.UpdateItem(Item);
                                                MyMath.GetEquipStats(Player);
                                            }
                                            break;
                                        }
                                    case 35016: //WuxingFour
                                        {
                                            if (IdxTask == 1)
                                                Client.Send(MsgAction.Create(Player, 1091, MsgAction.Action.PostCmd));
                                            else if (IdxTask == 2)
                                                Client.Send(MsgAction.Create(Player, 1086, MsgAction.Action.PostCmd));
                                            else if (IdxTask == 3)
                                                Client.Send(MsgAction.Create(Player, 1088, MsgAction.Action.PostCmd));
                                            else if (IdxTask == 4)
                                            {
                                                Position += ScriptHandler.SendText("Il vous faut 12 Perles de Dragon pour le +10, 25 pour le +11 et 40 pour le +12. Quel objet voulez-vous améliorer?", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(5, "Casque/Boucles", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(6, "Collier/Sac", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(7, "Armure/Robe", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(8, "Arme (Main droite)", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(9, "Arme/Bouclier (Main gauche)", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(10, "Bague/Anneau/Bracelets", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(11, "Bottes", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Rien.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(67, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask >= 5 && IdxTask <= 11)
                                            {
                                                Item Item = null;
                                                if (IdxTask == 5)
                                                    Item = Player.GetItemByPos(1);
                                                else if (IdxTask == 6)
                                                    Item = Player.GetItemByPos(2);
                                                else if (IdxTask == 7)
                                                    Item = Player.GetItemByPos(3);
                                                else if (IdxTask == 8)
                                                    Item = Player.GetItemByPos(4);
                                                else if (IdxTask == 9)
                                                    Item = Player.GetItemByPos(5);
                                                else if (IdxTask == 10)
                                                    Item = Player.GetItemByPos(6);
                                                else if (IdxTask == 11)
                                                    Item = Player.GetItemByPos(8);

                                                if (Item == null)
                                                {
                                                    Position += ScriptHandler.SendText("Vous devez équiper l'objet.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(67, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    break;
                                                }

                                                if (Item.Craft < 9)
                                                    break;

                                                if (Item.Craft >= 12)
                                                {
                                                    Position += ScriptHandler.SendText("Votre équipement est composé à son maximum.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(67, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    break;
                                                }

                                                Int32 Amount = 0;
                                                if (Item.Craft == 9)
                                                    Amount = 12;
                                                else if (Item.Craft == 10)
                                                    Amount = 25;
                                                else if (Item.Craft == 11)
                                                    Amount = 40;

                                                if (!Player.InventoryContains(1088000, Amount))
                                                {
                                                    Position += ScriptHandler.SendText("Vous n'avez pas assez de Perles de Dragon.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(67, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    break;
                                                }

                                                Player.DelItem(1088000, Amount, true);

                                                Item.Craft++;
                                                Player.UpdateItem(Item);
                                                MyMath.GetEquipStats(Player);
                                            }
                                            break;
                                        }
                                    case 35501: //ArtisantRow
                                        {
                                            if (IdxTask >= 1 && IdxTask <= 7)
                                            {
                                                Item Item = null;
                                                if (IdxTask == 1)
                                                    Item = Player.GetItemByPos(1);
                                                else if (IdxTask == 2)
                                                    Item = Player.GetItemByPos(2);
                                                else if (IdxTask == 3)
                                                    Item = Player.GetItemByPos(3);
                                                else if (IdxTask == 4)
                                                    Item = Player.GetItemByPos(4);
                                                else if (IdxTask == 5)
                                                    Item = Player.GetItemByPos(5);
                                                else if (IdxTask == 6)
                                                    Item = Player.GetItemByPos(6);
                                                else if (IdxTask == 7)
                                                    Item = Player.GetItemByPos(8);

                                                if (Item == null)
                                                {
                                                    Position += ScriptHandler.SendText("Vous devez équiper l'objet.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(96, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    break;
                                                }

                                                if (Item.CurDura != Item.MaxDura)
                                                {
                                                    Position += ScriptHandler.SendText("Vous devez réparer l'objet avant de l'améliorer.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(96, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    break;
                                                }

                                                Int32 NextId = ItemHandler.GetNextId(Item.Id);
                                                if (NextId == Item.Id)
                                                {
                                                    Position += ScriptHandler.SendText("Je ne peux pas améliorer le niveau de cet objet.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(96, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    break;
                                                }

                                                ItemType.Entry Info;
                                                if (!Database2.AllItems.TryGetValue(NextId, out Info))
                                                {
                                                    Position += ScriptHandler.SendText("Je ne peux pas améliorer le niveau de cet objet.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(96, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    break;
                                                }

                                                if (Info.RequiredLevel <= 130)
                                                {
                                                    Position += ScriptHandler.SendText("Je ne peux pas améliorer le niveau de cet objet.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(96, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    break;
                                                }

                                                if (Info.RequiredLevel > Player.Level)
                                                {
                                                    Position += ScriptHandler.SendText("Vous n'avez pas le niveau requis pour améliorer le niveau de cet objet.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(96, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    break;
                                                }

                                                if (Info.RequiredLevel > 130 && Info.RequiredLevel < 135)
                                                {
                                                    if (!Player.InventoryContains(1088003, 1))
                                                    {
                                                        Position += ScriptHandler.SendText("Vous n'avez pas de Citrine.", Client, ref Data, Position);
                                                        Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                        Position += ScriptHandler.SendFace(96, Client, ref Data, Position);
                                                        Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                        ScriptHandler.SendData(Client, Data, Position);
                                                        break;
                                                    }
                                                    Player.DelItem(1088003, 1, true);
                                                }
                                                else if (Info.RequiredLevel > 134 && Info.RequiredLevel < 140)
                                                {
                                                    if (!Player.InventoryContains(1088004, 1))
                                                    {
                                                        Position += ScriptHandler.SendText("Vous n'avez pas d'Oeil de Tigre.", Client, ref Data, Position);
                                                        Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                        Position += ScriptHandler.SendFace(96, Client, ref Data, Position);
                                                        Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                        ScriptHandler.SendData(Client, Data, Position);
                                                        break;
                                                    }
                                                    Player.DelItem(1088004, 1, true);
                                                }
                                                else if (Info.RequiredLevel == 140)
                                                {
                                                    if (!(Player.InventoryContains(1088003, 3) && Player.InventoryContains(1088004, 3)))
                                                    {
                                                        Position += ScriptHandler.SendText("Vous n'avez pas trois Citrine et trois Oeil de Tigre.", Client, ref Data, Position);
                                                        Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                        Position += ScriptHandler.SendFace(96, Client, ref Data, Position);
                                                        Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                        ScriptHandler.SendData(Client, Data, Position);
                                                        break;
                                                    }
                                                    Player.DelItem(1088003, 3, true);
                                                    Player.DelItem(1088004, 3, true);
                                                }

                                                Item.Id = NextId;
                                                Item.CurDura = ItemHandler.GetMaxDura(NextId);
                                                Item.MaxDura = ItemHandler.GetMaxDura(NextId);

                                                Byte DuraEffect = 0;
                                                if (Item.Gem1 - (Item.Gem1 % 10) == 40) //Kylin
                                                {
                                                    DuraEffect += (Byte)(Item.Gem1 % 10);
                                                    if (Item.Gem1 % 10 == 3)
                                                        DuraEffect++;
                                                }

                                                if (Item.Gem2 - (Item.Gem2 % 10) == 40) //Kylin
                                                {
                                                    DuraEffect += (Byte)(Item.Gem2 % 10);
                                                    if (Item.Gem2 % 10 == 3)
                                                        DuraEffect++;
                                                }

                                                Double Bonus = 1.0;
                                                Bonus += 0.5 * DuraEffect;

                                                Item.MaxDura = (UInt16)((Double)Item.MaxDura * Bonus);
                                                Item.CurDura = Item.MaxDura;

                                                Player.UpdateItem(Item);
                                                MyMath.GetEquipStats(Player);

                                                Position += ScriptHandler.SendText("Votre équipement a été amélioré!", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Merci.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(96, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            break;
                                        }
                                    case 300000: //ShanLee
                                        {
                                            if (IdxTask == 1)
                                            {
                                                Position += ScriptHandler.SendText("Vous pouvez échanger 5'000 points de vertu contre une Météore et 270'000 points de virtue contre une Perle de Dragon.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(3, "Je veux une Météore.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(4, "Je veux une Perle de Dragon.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Rien.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(29, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 2)
                                            {
                                                Position += ScriptHandler.SendText("Vous gagnez des points de vertu lorsque vous aidez des personnes, de niveau inférieur à 70, à monter de niveau. Vous pouvez utiliser les points de vertu pour obtenir des objets ou pour certaines quetes.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Ok, merci.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(29, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 3)
                                            {
                                                if (Player.VPs < 5000)
                                                {
                                                    Position += ScriptHandler.SendText("Vous n'avez pas assez de points de vertu!", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Désolé.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(29, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    return;
                                                }

                                                if (Player.ItemInInventory() >= 40)
                                                {
                                                    Position += ScriptHandler.SendText("Votre inventaire est plein!", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Désolé.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(29, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    return;
                                                }

                                                Player.VPs -= 5000;
                                                Player.AddItem(Item.Create(0, 0, 1088001, 0, 0, 0, 0, 0, 2, 0, 1, 1), true);
                                            }
                                            else if (IdxTask == 4)
                                            {
                                                if (Player.VPs < 270000)
                                                {
                                                    Position += ScriptHandler.SendText("Vous n'avez pas assez de points de vertu!", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Désolé.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(29, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    return;
                                                }

                                                if (Player.ItemInInventory() >= 40)
                                                {
                                                    Position += ScriptHandler.SendText("Votre inventaire est plein!", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Désolé.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(29, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    return;
                                                }

                                                Player.VPs -= 270000;
                                                Player.AddItem(Item.Create(0, 0, 1088000, 0, 0, 0, 0, 0, 2, 0, 1, 1), true);
                                            }
                                            break;
                                        }
                                    case 300500: //TaoisteDuDestin
                                        {
                                            if (Player.Metempsychosis > 0)
                                                return;
                                            if (IdxTask == 1)
                                            {
                                                Position += ScriptHandler.SendText("Pour accomplir la première renaissance, vous devez avoir un niveau supérieur à 120, ou supérieur à 110 pour les taoistes d'eau, ainsi que la dernière promotion de votre promotion. ", Client, ref Data, Position);
                                                Position += ScriptHandler.SendText("En m'apportant une Sainte Pierre, je vous guiderai. Après votre renaissance, vous pourrez controler vos points et apprendre certaines compétences.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(2, "Qu'est-ce qu'une Sainte Pierre?", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(3, "Distribuer les points?", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(4, "Compétences plus puissantes?", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(5, "Je veux renaitre.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Laissez-moi réfléchir.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 2)
                                            {
                                                Position += ScriptHandler.SendText("La Sainte Pierre possède une spiritualité. Avec cette pierre, je veux ouvrir la porte de la vie future.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(1, "Ok.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 3)
                                            {
                                                Position += ScriptHandler.SendText("Lorsque vous allez renaitre, vous serez au niveau 15. Selon votre niveau lors de votre renaissance, vous aurez plus ou moins de points, en plus des 30 points de votre renaissance. Par la suite, chaque niveau vous donnera 3 points supplémentaire.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(1, "Ok.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 4)
                                            {
                                                Position += ScriptHandler.SendText("Après votre réincarnation, vous conserverez certains pouvoirs de votre vie passé. Si vous faites une renaissance pure, vous aurez un nouveau pouvoir très puissant. De plus, en renaissant, vous pourrez désormait invoquer des monstres.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(1, "Ok.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 5)
                                            {
                                                Position += ScriptHandler.SendText("Vous avez le choix entre deux renaissances. Le niveau de votre équipement sera descendu pour que vous continuez à le porter.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(6, "Renaissance divine.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(7, "Renaissance normale.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 6)
                                            {
                                                Position += ScriptHandler.SendText("Je suis dévoué à la force de la bénédiction éternelle qui absorbe l'essence de l'univers. Si vous voulez, je peux utiliser cette force et l'ajouter à votre équipement avec la bénédiction de Dieu.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(8, "Je voudrais en savoir plus.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(9, "Je veux renaitre.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Laissez-moi réfléchir.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 7)
                                            {
                                                if (!Player.InventoryContains(721259, 1))
                                                {
                                                    Position += ScriptHandler.SendText("Vous n'avez pas la SaintePierre", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Désolé.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    return;
                                                }

                                                if (Player.Profession % 10 != 5)
                                                {
                                                    Position += ScriptHandler.SendText("Vous devez avoir entré en fonction. Vous n'avez pas accompli toutes les promotions.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Désolé.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    return;
                                                }

                                                if ((Player.Level < 110 && Player.Profession == 135) || (Player.Level < 120 && Player.Profession != 135))
                                                {
                                                    Position += ScriptHandler.SendText("Vous n'avez pas le niveau requis pour renaitre...", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Désolé.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    return;
                                                }

                                                Position += ScriptHandler.SendText("Quelle gemme superbe voulez-vous?", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(17, "Gemme du Phénix", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(18, "Gemme du Dragon", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(19, "Gemme de Fureur", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(20, "Gemme Arc-en-ciel", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(21, "Gemme d'Ivoire", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(22, "Gemme Violette", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(23, "Gemme de Lune", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(24, "Gemme Mythique", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 8)
                                            {
                                                Position += ScriptHandler.SendText("Depuis l'apparition de la gemme mythique qui remonte à l'essence de l'univers, tous les etres vivants sont bénis par son aura. Je peux utiliser cette gemme pour vous offrir la bénédiction de Dieu.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(6, "Ok.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 9)
                                            {
                                                Position += ScriptHandler.SendText("Votre Sainte Pierre disparaitra après la renaissance. Pour ce qui est de la gemme mythique. Elle est très puissante, et je la controle mal. L'équipement amélioré sera donc aléatoire...", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(10, "D'accord.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Je veux réfléchir.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 10)
                                            {
                                                Position += ScriptHandler.SendText("Avez-vous décidé de recevoir la bénédiction de Dieu?", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(11, "Oui, je l'ai décidé!", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 11)
                                            {
                                                if (!Player.InventoryContains(721259, 1))
                                                {
                                                    Position += ScriptHandler.SendText("Vous n'avez pas la SaintePierre", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Désolé.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    return;
                                                }

                                                if (Player.Profession % 10 != 5)
                                                {
                                                    Position += ScriptHandler.SendText("Vous devez avoir entré en fonction. Vous n'avez pas accompli toutes les promotions.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Désolé.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    return;
                                                }

                                                if ((Player.Level < 110 && Player.Profession == 135) || (Player.Level < 120 && Player.Profession != 135))
                                                {
                                                    Position += ScriptHandler.SendText("Vous n'avez pas le niveau requis pour renaitre...", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Désolé.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    return;
                                                }

                                                Position += ScriptHandler.SendText("Que voulez-vous etre dans votre prochaine vie?", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(12, "Un brave.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(13, "Un guerrier.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(14, "Un archer.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(15, "Un taoiste de feu.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(16, "Un taoiste d'eau.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask >= 12 && IdxTask <= 16)
                                            {
                                                if (!Player.InventoryContains(721259, 1))
                                                {
                                                    Position += ScriptHandler.SendText("Vous n'avez pas la SaintePierre", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Désolé.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    return;
                                                }

                                                if (Player.Profession % 10 != 5)
                                                {
                                                    Position += ScriptHandler.SendText("Vous devez avoir entré en fonction. Vous n'avez pas accompli toutes les promotions.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Désolé.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    return;
                                                }

                                                if ((Player.Level < 110 && Player.Profession == 135) || (Player.Level < 120 && Player.Profession != 135))
                                                {
                                                    Position += ScriptHandler.SendText("Vous n'avez pas le niveau requis pour renaitre...", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Désolé.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    return;
                                                }

                                                Byte NewProfession = 0;
                                                if (IdxTask == 12)
                                                    NewProfession = 11;
                                                else if (IdxTask == 13)
                                                    NewProfession = 21;
                                                else if (IdxTask == 14)
                                                    NewProfession = 41;
                                                else if (IdxTask == 15)
                                                    NewProfession = 142;
                                                else if (IdxTask == 16)
                                                    NewProfession = 132;

                                                Player.DelItem(721259, 1, true);
                                                Player.DoMetempsychosis(NewProfession, true);

                                                World.BroadcastRoomMsg(Player, MsgName.Create(Player.UniqId, "born", MsgName.Action.RoleEffect), true);
                                                World.BroadcastMsg(MsgTalk.Create("SYSTEM", "ALLUSERS", Player.Name + " a accompli la première renaissance!", MsgTalk.Channel.GM, 0xFF00FF));
                                            }
                                            else if (IdxTask >= 17 && IdxTask <= 24)
                                            {
                                                if (!Player.InventoryContains(721259, 1))
                                                {
                                                    Position += ScriptHandler.SendText("Vous n'avez pas la SaintePierre", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Désolé.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    return;
                                                }

                                                if (Player.Profession % 10 != 5)
                                                {
                                                    Position += ScriptHandler.SendText("Vous devez avoir entré en fonction. Vous n'avez pas accompli toutes les promotions.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Désolé.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    return;
                                                }

                                                if ((Player.Level < 110 && Player.Profession == 135) || (Player.Level < 120 && Player.Profession != 135))
                                                {
                                                    Position += ScriptHandler.SendText("Vous n'avez pas le niveau requis pour renaitre...", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Désolé.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    return;
                                                }

                                                Client.NpcParam = 700003;
                                                Client.NpcParam += ((IdxTask - 17) * 10);

                                                Position += ScriptHandler.SendText("Que voulez-vous etre dans votre prochaine vie?", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(25, "Un brave.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(26, "Un guerrier.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(27, "Un archer.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(28, "Un taoiste de feu.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(29, "Un taoiste d'eau.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask >= 25 && IdxTask <= 29)
                                            {
                                                if (!Player.InventoryContains(721259, 1))
                                                {
                                                    Position += ScriptHandler.SendText("Vous n'avez pas la SaintePierre", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Désolé.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    return;
                                                }

                                                if (Player.Profession % 10 != 5)
                                                {
                                                    Position += ScriptHandler.SendText("Vous devez avoir entré en fonction. Vous n'avez pas accompli toutes les promotions.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Désolé.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    return;
                                                }

                                                if ((Player.Level < 110 && Player.Profession == 135) || (Player.Level < 120 && Player.Profession != 135))
                                                {
                                                    Position += ScriptHandler.SendText("Vous n'avez pas le niveau requis pour renaitre...", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Désolé.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    return;
                                                }

                                                Byte NewProfession = 0;
                                                if (IdxTask == 25)
                                                    NewProfession = 11;
                                                else if (IdxTask == 26)
                                                    NewProfession = 21;
                                                else if (IdxTask == 27)
                                                    NewProfession = 41;
                                                else if (IdxTask == 28)
                                                    NewProfession = 142;
                                                else if (IdxTask == 29)
                                                    NewProfession = 132;

                                                Player.DelItem(721259, 1, true);
                                                Player.DoMetempsychosis(NewProfession, false);
                                                Player.AddItem(Item.Create(Player.UniqId, 0, Client.NpcParam, 0, 0, 0, 0, 0, 2, 0, 1, 1), true);

                                                World.BroadcastRoomMsg(Player, MsgName.Create(Player.UniqId, "born", MsgName.Action.RoleEffect), true);
                                                World.BroadcastMsg(MsgTalk.Create("SYSTEM", "ALLUSERS", Player.Name + " a accompli la première renaissance!", MsgTalk.Channel.GM, 0xFF00FF));
                                            }
                                            break;
                                        }
                                    case 350050: //Saint-Taoiste
                                        {
                                            if (IdxTask == 1)
                                            {
                                                if (Player.Metempsychosis < 1 || Player.Level < 70)
                                                {
                                                    Position += ScriptHandler.SendText("Vous n'avez pas accompli la première renaissance ou vous n'avez pas encore atteint le niveau 70.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(27, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                }
                                                else
                                                {
                                                    Item Item = Player.GetItemById(1088000);
                                                    if (Item == null)
                                                    {
                                                        Position += ScriptHandler.SendText("Vous n'avez pas de Perle de Dragon.", Client, ref Data, Position);
                                                        Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                        Position += ScriptHandler.SendFace(27, Client, ref Data, Position);
                                                        Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                        ScriptHandler.SendData(Client, Data, Position);
                                                    }
                                                    else
                                                    {
                                                        Player.DelItem(Item, true);

                                                        MyMath.GetLevelStats(Player);
                                                        Player.Send(MsgUserAttrib.Create(Player, Player.Strength, MsgUserAttrib.Type.Strength));
                                                        Player.Send(MsgUserAttrib.Create(Player, Player.Agility, MsgUserAttrib.Type.Agility));
                                                        Player.Send(MsgUserAttrib.Create(Player, Player.Vitality, MsgUserAttrib.Type.Vitality));
                                                        Player.Send(MsgUserAttrib.Create(Player, Player.Spirit, MsgUserAttrib.Type.Spirit));
                                                        Player.Send(MsgUserAttrib.Create(Player, Player.AddPoints, MsgUserAttrib.Type.AddPoints));
                                                    }
                                                }
                                            }
                                            break;
                                        }
                                    case 600055: //Astre
                                        {
                                            if (IdxTask == 1)
                                            {
                                                Position += ScriptHandler.SendText("Pour le moment j'ai beaucoup de LarmeDeMétéore, mais j'en ai de besoin. ", Client, ref Data, Position);
                                                Position += ScriptHandler.SendText("Si vous me donnez une météorite, je peux vous divorcer de votre femme.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(2, "Voici une météorite", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "J'aime ma femme", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(27, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask == 2)
                                            {
                                                if (Player.InventoryContains(1088002, 1))
                                                {
                                                    Position += ScriptHandler.SendText("C'est bien vous avez une larme de météorite. Mais êtes-vous certain(e) de vouloir divorcer?", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(3, "Oui.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Je ne suis pas certain...", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(27, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                }
                                                else
                                                {
                                                    Position += ScriptHandler.SendText("Vous n'avez pas de larme de météorite... Revenez lorsque vous en aurez.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Je vois.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(27, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                }
                                            }
                                            else if (IdxTask == 3)
                                            {
                                                World.BroadcastMsg(MsgTalk.Create("SYSTEM", "ALLUSERS", Player.Name + " et " + Player.Spouse + " ont divorcé... Bonne chance lors de votre prochain mariage!", MsgTalk.Channel.GM, 0xFFFFFF));
                                                Player.DelItem(1088002, 1, true);

                                                Boolean IsOnline = false;
                                                foreach (Player Target in World.AllPlayers.Values)
                                                {
                                                    if (Target.Name == Player.Spouse)
                                                    {
                                                        IsOnline = true;
                                                        Target.Spouse = "Non";
                                                        Database.Save(Target);

                                                        World.BroadcastRoomMsg(Target, MsgName.Create(Target.UniqId, Target.Spouse, MsgName.Action.Spouse), true);
                                                        break;
                                                    }
                                                }

                                                if (!IsOnline)
                                                    Database.Divorce(Player.Spouse);

                                                Player.Spouse = "Non";
                                                Database.Save(Player);

                                                World.BroadcastRoomMsg(Player, MsgName.Create(Player.UniqId, Player.Spouse, MsgName.Action.Spouse), true);
                                            }
                                            break;
                                        }
                                    case 600075: //BoxeurHuang
                                        {
                                            if (IdxTask == 1)
                                            {
                                                Int16 PrevMap = Player.PrevMap;
                                                if (PrevMap != 1000 && PrevMap != 1002 && PrevMap != 1011 && PrevMap != 1015 && PrevMap != 1020)
                                                    PrevMap = 1002;

                                                if (!World.AllMaps.ContainsKey(PrevMap))
                                                {
                                                    if (!World.AllMaps.ContainsKey(1002))
                                                        return;
                                                    PrevMap = 1002;
                                                }

                                                Map Map = World.AllMaps[PrevMap];
                                                Player.Move(PrevMap, Map.PortalX, Map.PortalY);
                                            }
                                            break;
                                        }
                                    case 3215: //TaoïsteDeMer
                                        {
                                            if (IdxTask == 1)
                                            {
                                                UInt64 Exp = Player.CalcExpBall((Byte)Player.Level, Player.Exp, 2.5);
                                                Player.AddExp(Exp, false);

                                                Byte Teleport = (Byte)MyMath.Generate(1, 4);

                                                if (Teleport == 1)
                                                    Player.Move(2021, 298, 414);
                                                if (Teleport == 2)
                                                    Player.Move(2021, 365, 375);
                                                if (Teleport == 3)
                                                    Player.Move(2021, 302, 324);
                                                if (Teleport == 4)
                                                    Player.Move(2021, 223, 329);

                                                Player.Send(MsgTalk.Create("SYSTEM", "ALLUSERS", "Vous devez obtenir 5 Jades d'enfer..", MsgTalk.Channel.Talk, 0));
                                            }
                                            break;
                                        }
                                    case 600401: //TaoïsteDeMer
                                        {
                                            if (IdxTask == 1)
                                            {
                                                if (Player.InventoryContains(723085, 5))
                                                {
                                                    if (Player.Profession > 9 && Player.Profession < 16)
                                                        Player.DisToKill = 800;
                                                    if (Player.Profession > 19 && Player.Profession < 26)
                                                        Player.DisToKill = 900;
                                                    if (Player.Profession > 39 && Player.Profession < 46)
                                                        Player.DisToKill = 1300;
                                                    if (Player.Profession > 129 && Player.Profession < 136)
                                                        Player.DisToKill = 600;
                                                    if (Player.Profession > 139 && Player.Profession < 146)
                                                        Player.DisToKill = 1000;

                                                    Player.Move(2022, 228, 343);
                                                    Player.DisKO = 0;

                                                    Map Map = World.AllMaps[2022];
                                                    List<Player> Players = new List<Player>();

                                                    foreach (Entity Entity in Map.Entities.Values)
                                                    {
                                                        if (!Entity.IsPlayer())
                                                            continue;

                                                        Players.Add(Entity as Player);
                                                    }

                                                    if (Player.Syndicate == null)
                                                        World.BroadcastMsg(MsgTalk.Create("SYSTEM", "ALLUSERS", Players.Count + ": " + Player.Name + " est entré dans la deuxième carte de Dis City!", MsgTalk.Channel.System, 0xFFFFF));
                                                    else
                                                        World.BroadcastMsg(MsgTalk.Create("SYSTEM", "ALLUSERS", Players.Count + ": " + Player.Name + " des " + Player.Syndicate.Name + " est entré dans la deuxième carte de Dis City!", MsgTalk.Channel.System, 0xFFFFF));

                                                    if (Players.Count == 1)
                                                        World.BroadcastMsg(MsgTalk.Create("SYSTEM", "ALLUSERS", Player.Name + " est entré dans la SalleInfernale!", MsgTalk.Channel.GM, 0xFFFFF));

                                                    if (Players.Count > 59)
                                                    {
                                                        foreach (Player Player2 in Players)
                                                        {
                                                            if (Player2.Map == 2021)
                                                            {
                                                                Player2.Move(1002, 430, 380);
                                                                Player2.Send(MsgTalk.Create("SYSTEM", "ALLUSERS", "Pour protéger les joueurs, " + Player.Name + " a téléporté les joueurs hors du dangé!", MsgTalk.Channel.GM, 0xFFFFF));
                                                            }
                                                        }
                                                    }

                                                    while (Player.InventoryContains(723085, 1))
                                                        Player.DelItem(723085, 1, true);

                                                    UInt64 Exp = Player.CalcExpBall((Byte)Player.Level, Player.Exp, 3);
                                                    Player.AddExp(Exp, false);

                                                    Player.Send(MsgTalk.Create("SYSTEM", "ALLUSERS", "Tuez 800 monstres pour les Braves, 900 monstres pour les Guerriers, 1300 monstres pour les Archers, 1000 monstres pour les Taoistes de Feu, 600 monstres pour les Taoistes d’Eau. Je ne peux envoyer que 30 personnes au niveau suivant.", MsgTalk.Channel.Talk, 0));
                                                }
                                                else
                                                {
                                                    Position += ScriptHandler.SendText("Vous n'avez pas 5 jades...", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Je vois", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                }
                                            }
                                            break;
                                        }
                                    case 600402: //TaoïsteDeMer
                                        {
                                            if (IdxTask == 1)
                                            {
                                                if (Player.Profession > 9 && Player.Profession < 16)
                                                    Player.DisToKill = 800;
                                                if (Player.Profession > 19 && Player.Profession < 26)
                                                    Player.DisToKill = 900;
                                                if (Player.Profession > 39 && Player.Profession < 46)
                                                    Player.DisToKill = 1300;
                                                if (Player.Profession > 129 && Player.Profession < 136)
                                                    Player.DisToKill = 600;
                                                if (Player.Profession > 139 && Player.Profession < 146)
                                                    Player.DisToKill = 1000;

                                                if (Player.ItemInInventory() < 40 && Player.DisKO >= Player.DisToKill)
                                                {
                                                    Player.Move(2023, 301, 653);
                                                    Player.AddItem(Item.Create(0, 0, 723087, 0, 0, 0, 0, 0, 2, 0, 1, 1), true);

                                                    Map Map = World.AllMaps[2022];
                                                    Int32 Map2022 = 0;

                                                    lock (Map.Entities)
                                                    {
                                                        foreach (Entity Entity in Map.Entities.Values)
                                                        {
                                                            if (!Entity.IsPlayer())
                                                                continue;

                                                            Map2022++;
                                                        }
                                                    }

                                                    Map = World.AllMaps[2023];
                                                    List<Player> Players = new List<Player>();

                                                    lock (Map.Entities)
                                                    {
                                                        foreach (Entity Entity in Map.Entities.Values)
                                                        {
                                                            if (!Entity.IsPlayer())
                                                                continue;

                                                            Players.Add(Entity as Player);
                                                        }
                                                    }

                                                    if (Player.Syndicate == null)
                                                        World.BroadcastMsg(MsgTalk.Create("SYSTEM", "ALLUSERS", Players.Count + ": " + Player.Name + " est entré dans la troisième carte de Dis City!", MsgTalk.Channel.System, 0xFFFFF));
                                                    else
                                                        World.BroadcastMsg(MsgTalk.Create("SYSTEM", "ALLUSERS", Players.Count + ": " + Player.Name + " des " + Player.Syndicate.Name + " est entré dans la troisième carte de Dis City!", MsgTalk.Channel.System, 0xFFFFF));

                                                    if (Players.Count == 1)
                                                        World.BroadcastMsg(MsgTalk.Create("SYSTEM", "ALLUSERS", Player.Name + " est entré dans la CloîtreInfernal!", MsgTalk.Channel.GM, 0xFFFFF));

                                                    if (Players.Count > 29 || Map2022 == 0)
                                                    {
                                                        foreach (Player Player2 in Players)
                                                        {
                                                            if (Player2.Map == 2023)
                                                                Player2.Move(2024, 150, 283);
                                                        }

                                                        lock (World.AllPlayers)
                                                        {
                                                            foreach (Player Player2 in World.AllPlayers.Values)
                                                            {
                                                                if (Player2.Map == 2022)
                                                                {
                                                                    Player2.Move(1002, 430, 380);
                                                                    Player2.Send(MsgTalk.Create("SYSTEM", "ALLUSERS", "Pour protéger les joueurs, " + Player.Name + " a téléporté les joueurs hors du dangé!", MsgTalk.Channel.GM, 0xFFFFF));
                                                                }
                                                            }
                                                        }
                                                    }

                                                    UInt64 Exp = Player.CalcExpBall((Byte)Player.Level, Player.Exp, 3);
                                                    Player.AddExp(Exp, false);

                                                    Player.Send(MsgTalk.Create("SYSTEM", "ALLUSERS", "Lorsque la 30e personne ou la dernière personne rentrera dans cette carte vous irez à la prochaine!", MsgTalk.Channel.Talk, 0));
                                                }
                                                else
                                                {
                                                    Position += ScriptHandler.SendText("Vous n'avez pas tué assez de monstre ou votre inventaire est plein!", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Je vois", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                }
                                            }
                                            break;
                                        }
                                    case 1723701: //2rb
                                        {
                                            if (Player.Metempsychosis != 1)
                                                return;

                                            if (IdxTask == 1)
                                            {
                                                if (!Player.InventoryContains(723701, 1))
                                                {
                                                    Position += ScriptHandler.SendText("Vous n'avez pas le signe de franchise!", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Désolé.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    return;
                                                }

                                                if (Player.Profession % 10 != 5)
                                                {
                                                    Position += ScriptHandler.SendText("Vous devez avoir entré en fonction. Vous n'avez pas accompli toutes les promotions.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Désolé.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    return;
                                                }

                                                if (Player.Level < 120)
                                                {
                                                    Position += ScriptHandler.SendText("Vous n'avez pas le niveau requis pour renaitre...", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Désolé.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    return;
                                                }

                                                Position += ScriptHandler.SendText("Que voulez-vous etre dans votre prochaine vie?", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(12, "Un brave.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(13, "Un guerrier.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(14, "Un archer.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(15, "Un taoiste de feu.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(16, "Un taoiste d'eau.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (IdxTask >= 12 && IdxTask <= 16)
                                            {
                                                if (!Player.InventoryContains(723701, 1))
                                                {
                                                    Position += ScriptHandler.SendText("Vous n'avez pas la SaintePierre", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Désolé.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    return;
                                                }

                                                if (Player.Profession % 10 != 5)
                                                {
                                                    Position += ScriptHandler.SendText("Vous devez avoir entré en fonction. Vous n'avez pas accompli toutes les promotions.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Désolé.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    return;
                                                }

                                                if (Player.Level < 120)
                                                {
                                                    Position += ScriptHandler.SendText("Vous n'avez pas le niveau requis pour renaitre...", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Désolé.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    return;
                                                }

                                                Byte NewProfession = 0;
                                                if (IdxTask == 12)
                                                    NewProfession = 11;
                                                else if (IdxTask == 13)
                                                    NewProfession = 21;
                                                else if (IdxTask == 14)
                                                    NewProfession = 41;
                                                else if (IdxTask == 15)
                                                    NewProfession = 142;
                                                else if (IdxTask == 16)
                                                    NewProfession = 132;

                                                Player.DelItem(723701, 1, true);
                                                Player.DoMetempsychosis(NewProfession, false);

                                                World.BroadcastRoomMsg(Player, MsgName.Create(Player.UniqId, "born", MsgName.Action.RoleEffect), true);
                                                World.BroadcastMsg(MsgTalk.Create("SYSTEM", "ALLUSERS", Player.Name + " a accompli la seconde renaissance!", MsgTalk.Channel.GM, 0xFF00FF));
                                            }
                                            break;
                                        }
                                    case 1723702:
                                        {
                                            if (IdxTask >= 1 && IdxTask <= 8)
                                            {
                                                Byte ID = 2;
                                                if (IdxTask == 1)
                                                    ID = 2;
                                                else if (IdxTask == 2)
                                                    ID = 3;
                                                else if (IdxTask == 3)
                                                    ID = 4;
                                                else if (IdxTask == 4)
                                                    ID = 5;
                                                else if (IdxTask == 5)
                                                    ID = 6;
                                                else if (IdxTask == 6)
                                                    ID = 7;
                                                else if (IdxTask == 7)
                                                    ID = 8;
                                                else if (IdxTask == 8)
                                                    ID = 9;

                                                if (Player.Metempsychosis != ID)
                                                {
                                                    Position += ScriptHandler.SendText("Vous n'etes pas près à accomplir cette renaissance!", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Désolé.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    return;
                                                }

                                                if (!Player.InventoryContains(723700 + ID, 1))
                                                {
                                                    Position += ScriptHandler.SendText("Vous n'avez pas le signe de prestige!", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Désolé.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    return;
                                                }

                                                if (Player.Profession % 10 != 5)
                                                {
                                                    Position += ScriptHandler.SendText("Vous devez avoir entré en fonction. Vous n'avez pas accompli toutes les promotions.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Désolé.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    return;
                                                }

                                                if (Player.Level < 130)
                                                {
                                                    Position += ScriptHandler.SendText("Vous n'avez pas le niveau requis pour renaitre...", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendOption(255, "Désolé.", Client, ref Data, Position);
                                                    Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                    Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                    ScriptHandler.SendData(Client, Data, Position);
                                                    return;
                                                }

                                                Player.DelItem(723700 + ID, 1, true);
                                                Player.DoMetempsychosis(Player.Profession, false);

                                                World.BroadcastRoomMsg(Player, MsgName.Create(Player.UniqId, "born", MsgName.Action.RoleEffect), true);
                                                World.BroadcastMsg(MsgTalk.Create("SYSTEM", "ALLUSERS", Player.Name + " a accompli le niveau " + IdxTask + " de prestige!", MsgTalk.Channel.GM, 0xFF00FF));
                                            }
                                            break;
                                        }
                                }
                            }
                            break;
                        }
                    case Action.TaskId:
                        {
                            if (TaskId == 31100) //Kick Out
                            {
                                if (Player.Syndicate == null)
                                    return;

                                if (Player.Syndicate.Leader.UniqId != Player.UniqId)
                                    return;

                                if (Player.Name == Text)
                                    return;

                                Syndicate.Member Member = Player.Syndicate.GetMemberInfo(Text);
                                if (Member == null)
                                    return;

                                Player.Syndicate.DelMember(Member, true);
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
