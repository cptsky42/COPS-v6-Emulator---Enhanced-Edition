// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.Runtime.InteropServices;
using COServer.Entities;

namespace COServer.Network
{
    public unsafe class MsgMessageBoard : Msg
    {
        public const Int16 Id = _MSG_MESSAGEBOARD;

        public enum Action
        {
            None = 0,
            Del = 1,			        // to server					// no return
            GetList = 2,		    	// to server: index(first index)
            List = 3,		        	// to client: index(first index), name, words, time...
            GetWords = 4,	    		// to server: index(for get)	// return by MsgTalk
        };

        public enum Channel
        {
            None = 0,
            MsgTrade = 2201,
            MsgFriend = 2202,
            MsgTeam = 2203,
            MsgSyn = 2204,
            MsgOther = 2205,
            MsgSystem = 2206,
        };

        public struct MsgInfo
        {
            public MsgHeader Header;
     		public UInt16 Index;
		    public UInt16 Channel;
		    public Byte Action;
            public String Param;
        };

        public static Byte[] Create(UInt16 Index, Channel Channel, String[] Params, Action Action)
        {
            try
            {
                Int32 StrLength = 0;
                if (Params != null)
                {
                    for (Int32 i = 0; i < Params.Length; i++)
                    {
                        if (Params[i] == null || Params[i].Length > _MAX_WORDSSIZE)
                            return null;

                        StrLength += Params[i].Length + 1;
                    }
                }

                Byte[] Out = new Byte[10 + StrLength];
                fixed (Byte* p = Out)
                {
                    *((Int16*)(p + 0)) = (Int16)Out.Length;
                    *((Int16*)(p + 2)) = (Int16)Id;
                    *((UInt16*)(p + 4)) = (UInt16)Index;
                    *((UInt16*)(p + 6)) = (UInt16)Channel;
                    *((Byte*)(p + 8)) = (Byte)Action;

                    if (Params != null)
                    {
                        *((Byte*)(p + 9)) = (Byte)Params.Length;

                        Int32 Pos = 10;
                        for (Int32 x = 0; x < Params.Length; x++)
                        {
                            *((Byte*)(p + Pos)) = (Byte)Params[x].Length;
                            for (Byte i = 0; i < (Byte)Params[x].Length; i++)
                                *((Byte*)(p + Pos + 1 + i)) = (Byte)Params[x][i];
                            Pos += Params[x].Length + 1;
                        }
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
                UInt16 Index = (UInt16)((Buffer[0x05] << 8) + Buffer[0x04]);
                Channel Channel = (Channel)((Buffer[0x07] << 8) + Buffer[0x06]);
                Action Action = (Action)(Buffer[0x08]);
                String Param = null;
                if (Buffer[0x09] == 0x01)
                Param = Program.Encoding.GetString(Buffer, 0x0B, Buffer[0x0A]);

                Player Player = Client.User;

                switch (Action)
                {
                    case Action.Del:
                        {
                            if (Param != Player.Name || Client.AccLvl > 6) // || GM/PM
                                return;

                            World.MessageBoard.MessageInfo Info =
                                World.MessageBoard.GetMsgInfoByAuthor(Param, (UInt16)Channel);

                            World.MessageBoard.Delete(Info, (UInt16)Channel);
                            break;
                        }
                    case Action.GetList:
                        {
                            String[] List = World.MessageBoard.GetList(Index, (UInt16)Channel);
                            Player.Send(MsgMessageBoard.Create(Index, Channel, List, Action.List));
                            break;
                        }
                    case Action.GetWords:
                        {
                            String Words = World.MessageBoard.GetWords(Param, (UInt16)Channel);
                            Player.Send(MsgTalk.Create(Param, Player.Name, Words, (MsgTalk.Channel)Channel, 0xFFFFFF));
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
