// * Created by Jean-Philippe Boivin
// * Copyright © 2010-2011
// * Logik. Project

using System;
using System.Runtime.InteropServices;
using COServer.Entities;

namespace COServer.Network
{
    public unsafe class MsgNoble : Msg
    {
        public const Int16 Id = _MSG_NOBLE;

        public enum Action
        {
            None = 0,
            Donate = 1,
            GetList = 2,
            Information = 3,
            Donation = 4,
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct MsgInfo
        {
            public MsgHeader Header;
            public Int32 Action;
            public Int64 Param;
            public Byte StringCount;
            public String[] Strings;
        };

        public static Byte[] Create(Int64 Param, Action Action)
        {
            try
            {
                Byte[] Out = new Byte[24];
                fixed (Byte* p = Out)
                {
                    *((Int16*)(p + 0)) = (Int16)Out.Length;
                    *((Int16*)(p + 2)) = (Int16)Id;
                    *((Int32*)(p + 4)) = (Int32)Action;
                    *((Int64*)(p + 8)) = (Int64)Param;
                }
                return Out;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return null; }
        }

        public static Byte[] Create(Player Entity, Int64 Param)
        {
            try
            {
                String String = Entity.Nobility.UniqId + 
                    Entity.Nobility.Donation + " " + //Not sure
                    Entity.Nobility.Rank + " " + //Not Sure
                    Param + " " + 
                    Entity.Nobility.Donation + " " +
                    Entity.Nobility.Position;

                Byte[] Out = new Byte[24 + String.Length];
                fixed (Byte* p = Out)
                {
                    *((Int16*)(p + 0)) = (Int16)Out.Length;
                    *((Int16*)(p + 2)) = (Int16)Id;
                    *((Int32*)(p + 4)) = (Int32)Action.Donate;
                    *((Int64*)(p + 8)) = (Int64)Param;
                    *((Byte*)(p + 16)) = (Byte)0x01; //String Count
                    *((Byte*)(p + 17)) = (Byte)String.Length;
                    for (Byte i = 0; i < (Byte)String.Length; i++)
                        *((Byte*)(p + 18 + i)) = (Byte)String[i];
                }
                return Out;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return null; }
        }

        public static Byte[] Create(Player Entity)
        {
            try
            {
                String String = Entity.UniqId + " " + Entity.Nobility.Donation + " " + Entity.Nobility.Rank + " " + Entity.Nobility.Position;

                Byte[] Out = new Byte[24 + String.Length];
                fixed (Byte* p = Out)
                {
                    *((Int16*)(p + 0)) = (Int16)Out.Length;
                    *((Int16*)(p + 2)) = (Int16)Id;
                    *((Int32*)(p + 4)) = (Int32)Action.Information;
                    *((Int64*)(p + 8)) = (Int64)Entity.UniqId; //Param
                    *((Byte*)(p + 16)) = (Byte)0x01; //String Count
                    *((Byte*)(p + 17)) = (Byte)String.Length;
                    for (Byte i = 0; i < (Byte)String.Length; i++)
                        *((Byte*)(p + 18 + i)) = (Byte)String[i];
                }
                return Out;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return null; }
        }

        public static Byte[] Create(Int16 Page)
        {
            try
            {
                Int16 PageC = (Int16)(World.NobilityRank.Count / 10);
                if (World.NobilityRank.Count % 10 != 0)
                    PageC++;

                if (Page > PageC)
                    return null;

                Int32 Count = World.NobilityRank.Count - (Page * 10);
                Count = Math.Min(Count, 10);

                if (Count < 0)
                    Count = 0;

                String[] Strings = new String[Count];
                foreach (Nobility.Info Info in World.NobilityRank.Values)
                {
                    if (Info.Position >= (Page * 10) && Info.Position < ((Page + 1) * 10))
                    {
                        //UniqId HasLook Look Name Donation Rank Position
                        if (Info.Rank == Nobility._RANK_KING)
                            Strings[Info.Position - (Page * 10)] = Info.UniqId + " 1 " + Info.Look + " " + Info.Name + " " + Info.Donation + " " + Info.Rank + " " + Info.Position;
                        else
                            Strings[Info.Position - (Page * 10)] = Info.UniqId + " 0 0 " + Info.Name + " " + Info.Donation + " " + Info.Rank + " " + Info.Position;
                    }
                }

                Int32 Length = 0;
                foreach (String String in Strings)
                    Length += String.Length + 1;

                Count = 0;

                Byte[] Out = new Byte[24 + Length];
                fixed (Byte* p = Out)
                {
                    *((Int16*)(p + 0)) = (Int16)Out.Length;
                    *((Int16*)(p + 2)) = (Int16)Id;
                    *((Int32*)(p + 4)) = (Int32)Action.GetList;
                    *((Int16*)(p + 8)) = (Int16)Page; //Page n° (Param)
                    *((Int16*)(p + 10)) = (Int16)PageC; //Page Count (Param)
                    *((Int32*)(p + 12)) = (Int32)0x00; //(Param)
                    *((Byte*)(p + 16)) = (Byte)Strings.Length; //String Count
                    for (Int32 i = 0; i < Strings.Length; i++)
                    {
                        *((Byte*)(p + 17 + Count)) = (Byte)Strings[i].Length;
                        for (Byte x = 0; x < (Byte)Strings[i].Length; x++)
                            *((Byte*)(p + 18 + Count + x)) = (Byte)Strings[i][x];
                        Count += Strings[i].Length + 1;
                    }
                }
                Strings = null;
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
                Action Action = (Action)((Buffer[0x07] << 24) + (Buffer[0x06] << 16) + (Buffer[0x05] << 8) + Buffer[0x04]);
                Int64 Param = (Int64)((Buffer[0x0B] << 24) + (Buffer[0x0A] << 16) + (Buffer[0x09] << 8) + Buffer[0x08]);
                Param += (Int64)((Buffer[0x0F] << 56) + (Buffer[0x0E] << 48) + (Buffer[0x0D] << 40) + (Buffer[0x0C] << 32));

                switch (Action)
                {
                    case Action.Donate:
                        {
                            if (Param >= 1000000000)
                                return;

                            if (Param < 3000000)
                                return;

                            Player Player = Client.User;
                            if (Player.Money >= Param)
                            {
                                Player.Money -= (Int32)Param;
                                Player.Send(MsgUserAttrib.Create(Player, Player.Money, MsgUserAttrib.Type.Money));

                                Int64 OldDonation = Player.Nobility.Donation;

                                Player.Nobility.Donation += Param;
                                if (OldDonation > 0)
                                    Nobility.Rank.ResetPosition();
                                else
                                    Nobility.Rank.AddPlayer(Player.Nobility);

                                Client.Send(MsgNoble.Create(Client.User, Param));
                            }
                            else if (Player.CPs >= Param / 50000)
                            {
                                Player.CPs -= (Int32)(Param / 50000);
                                Player.Send(MsgUserAttrib.Create(Player, Player.CPs, MsgUserAttrib.Type.CPs));

                                Int64 OldDonation = Player.Nobility.Donation;

                                Player.Nobility.Donation += Param;
                                if (OldDonation > 0)
                                    Nobility.Rank.ResetPosition();
                                else
                                    Nobility.Rank.AddPlayer(Player.Nobility);

                                Client.Send(MsgNoble.Create(Client.User, Param));
                            }
                            break;
                        }
                    case Action.GetList:
                        {
                            Client.Send(MsgNoble.Create((Int16)Param));
                            break;
                        }
                    case Action.Donation:
                        {
                            Player Player = Client.User;
                            Int64 MinDonation = Int64.MaxValue;

                            if (Param > 5)
                            {
                                lock (World.NobilityRank)
                                {
                                    foreach (Nobility.Info Info in World.NobilityRank.Values)
                                    {
                                        if (Info.Rank != Param)
                                            continue;

                                        if (Info.Donation < MinDonation)
                                            MinDonation = Info.Donation;
                                    }
                                }
                            }

                            if (Param == 1)
                                MinDonation = Nobility._DONATION_KNIGHT;
                            else if (Param == 3)
                                MinDonation = Nobility._DONATION_BARON;
                            else if (Param == 5)
                                MinDonation = Nobility._DONATION_EARL;

                            if (MinDonation == Int64.MaxValue)
                                MinDonation = 0;

                            if (Player.Nobility.Donation >= MinDonation)
                                MinDonation = 0;
                            else
                                MinDonation -= Player.Nobility.Donation;

                            Client.Send(MsgNoble.Create(Math.Max(3000000, MinDonation), Action.Donation));
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
