// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.Runtime.InteropServices;
using COServer.Entities;

namespace COServer.Network
{
    public unsafe class MsgName : Msg
    {
        public const Int16 Id = _MSG_NAME;

        public enum Action
        {
            None = 0,
            Fireworks = 1,
            CreateSyn = 2,
            Syndicate = 3,
            ChangeTitle = 4,
            DelRole = 5,
            Spouse = 6,
            QueryNPC = 7,           //To client, To Server
            Wanted = 8,             //To client
            MapEffect = 9,          //To client
            RoleEffect = 10,        //To client
            MemberList = 11,        //To client, To Server, dwData is index
            KickOut_SynMem = 12,
            QueryWanted = 13,
            QueryPoliceWanted = 14,
            PoliceWanted = 15,
            QuerySpouse = 16,
            AddDice_Player = 17,	//BcastClient(INCLUDE_SELF) Ôö¼Ó÷»×ÓÍæ¼Ò// dwDataÎª÷»×ÓÌ¯ID // To Server ¼ÓÈë ÐèÒªÔ­ÏûÏ¢·µ»Ø
            DelDice_Player = 18,    //BcastClient(INCLUDE_SELF) É¾³ý÷»×ÓÍæ¼Ò// dwDataÎª÷»×ÓÌ¯ID // To Server Àë¿ª ÐèÒªÔ­ÏûÏ¢·µ»Ø
            DiceBonus = 19,         //dwDataÎªMoney
            Sound = 20,
            SynEnemie = 21,	
            SynAlly = 22,
            Bavarder = 26,
        };

        public struct MsgInfo
        {
            public MsgHeader Header;
            public Int32 Data;
            public Byte Action;
            public Byte Count;
            public String[] Params;
        };

        public static Byte[] Create(Int32 Data, String Param, Action Action)
        {
            try
            {
                if (Param == null || Param.Length > _MAX_WORDSSIZE)
                    return null;

                Byte[] Out = new Byte[13 + Param.Length];
                fixed (Byte* p = Out)
                {
                    *((Int16*)(p + 0)) = (Int16)Out.Length;
                    *((Int16*)(p + 2)) = (Int16)Id;
                    *((Int32*)(p + 4)) = (Int32)Data;
                    *((Byte*)(p + 8)) = (Byte)Action;
                    *((Byte*)(p + 9)) = (Byte)0x01;
                    *((Byte*)(p + 10)) = (Byte)Param.Length;
                    for (Byte i = 0; i < (Byte)Param.Length; i++)
                        *((Byte*)(p + 11 + i)) = (Byte)Param[i];
                }
                return Out;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return null; }
        }

        public static Byte[] Create(Int32 Data, String[] Params, Action Action)
        {
            try
            {
                if (Params == null || Params.Length < 1)
                    return null;

                Int32 StrLength = 0;
                for (Int32 i = 0; i < Params.Length; i++)
                {
                    if (Params[i] == null || Params[i].Length > _MAX_WORDSSIZE)
                        return null;

                    StrLength += Params[i].Length + 1;
                }

                Byte[] Out = new Byte[12 + StrLength];
                fixed (Byte* p = Out)
                {
                    *((Int16*)(p + 0)) = (Int16)Out.Length;
                    *((Int16*)(p + 2)) = (Int16)Id;
                    *((Int32*)(p + 4)) = (Int32)Data;
                    *((Byte*)(p + 8)) = (Byte)Action;
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
                Int32 Data = (Int32)((Buffer[0x07] << 24) + (Buffer[0x06] << 16) + (Buffer[0x05] << 8) + Buffer[0x04]);
                Action Action = (Action)Buffer[0x08];
                Byte Count = Buffer[0x09];
                String[] Params = new String[Count];

                Int32 Pos = 10;
                for (Int32 i = 0; i < Count; i++)
                {
                    Params[i] = Program.Encoding.GetString(Buffer, Pos + 1, Buffer[Pos]);
                    Pos = Params[i].Length + 1;
                }

                switch (Action)
                {
                    case Action.MemberList:
                        {
                            Player Player = Client.User;
                            if (Player == null)
                                return;

                            if (Player.Syndicate == null)
                                return;

                            Syndicate.Info Syn = Player.Syndicate;
                            String[] List = Syn.GetMemberList();

                            if (Data == -1)
                                Data = 0;

                            Int32 Amount = List.Length - (Data * 10);
                            if (Amount > 10)
                                Amount = 10;

                            String[] Tmp = new String[Amount];
                            Array.Copy(List, Data * 10, Tmp, 0, Amount);

                            Player.Send(MsgName.Create(Data + 1, Tmp, Action.MemberList));
                            break;
                        }
                    case Action.QuerySpouse:
                        {
                            Player Player = Client.User;
                            if (Player == null)
                                return;

                            Player Target = null;
                            if (!World.AllPlayers.TryGetValue(Data, out Target))
                                return;

                            Player.Send(MsgName.Create(Target.UniqId, Target.Spouse, Action.QuerySpouse));
                            break;
                        }
                    case Action.Bavarder:
                        {
                            Player Target = null;
                            foreach (Player Player in World.AllPlayers.Values)
                            {
                                if (Player.Name == Params[0])
                                {
                                    Target = Player;
                                    break;
                                }
                            }

                            if (Target == null)
                                return;

                            String SynName = "";
                            if (Target.Syndicate != null)
                                SynName = Target.Syndicate.Name;

                            String TargetInfo = 
                                Target.UniqId + " " + 
                                Target.Level + " " + 
                                Target.Potency + " #" + 
                                SynName + " #" + 
                                "Family" + " " + 
                                Target.Spouse + " " + 
                                Target.Nobility.Rank + " " + 
                                (Target.IsMan() ? (Byte)1 : (Byte)0);

                            Client.Send(MsgName.Create(0, new String[] { Target.Name, TargetInfo }, Action.Bavarder));
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
