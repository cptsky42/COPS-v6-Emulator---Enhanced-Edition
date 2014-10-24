// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.Collections.Generic;
using COServer.Network;
using COServer.Entities;

namespace COServer
{
    public class Nobility
    {
        public const Int32 _RANK_KNIGHT = 1;
        public const Int32 _RANK_BARON = 3;
        public const Int32 _RANK_EARL = 5;
        public const Int32 _RANK_DUKE = 7;
        public const Int32 _RANK_PRINCE = 9;
        public const Int32 _RANK_KING = 12;

        public const Int32 _RANK_MIN = _RANK_KNIGHT;
        public const Int32 _RANK_MAX = _RANK_KING;

        public const Int32 _MAX_KING = 3;
        public const Int32 _MAX_PRINCE = 12;
        public const Int32 _MAX_DUKE = 35;

        public const Int64 _DONATION_EARL = 200000000;
        public const Int64 _DONATION_BARON = 100000000;
        public const Int64 _DONATION_KNIGHT = 30000000;

        public class Info
        {
            public Int32 UniqId;
            public String Name;
            public UInt32 Look;
            public Int64 Donation;
            public Int32 Rank;
            public Int32 Position;


            public Info() { }
            public Info(Player Owner)
            {
                UniqId = Owner.UniqId;
                Name = Owner.Name;
                Look = Owner.Look;
            }
        }

        public class Rank
        {
            public static Boolean AddPlayer(Nobility.Info Info)
            {
                if (World.NobilityRank.ContainsKey(Info.UniqId))
                    return false;

                lock (World.NobilityRank) { World.NobilityRank.Add(Info.UniqId, Info); }
                ResetPosition();
                return true;
            }

            public static Boolean DelPlayer(Nobility.Info Info)
            {
                if (!World.NobilityRank.ContainsKey(Info.UniqId))
                    return false;

                lock (World.NobilityRank) { World.NobilityRank.Remove(Info.UniqId); }
                ResetPosition();
                return true;
            }

            public static void ResetPosition()
            {
                lock (World.NobilityRank)
                {
                    if (World.NobilityRank.Count < 1)
                        return;

                    Nobility.Info[] Infos = new Nobility.Info[World.NobilityRank.Count];
                    World.NobilityRank.Values.CopyTo(Infos, 0);

                    List<Int32> Sorted = new List<Int32>();
                    Int64 LastDonation = Int64.MaxValue;
                    Int32 LastUID = 0;
                    Int64 tmp1 = 0;
                    for (Int32 i = 0; i < Infos.Length; i++)
                    {
                        for (Int32 x = 0; x < Infos.Length; x++)
                        {
                            if (Sorted.Contains(Infos[x].UniqId))
                                continue;

                            if (Infos[x].Donation > tmp1 && Infos[x].Donation <= LastDonation)
                            {
                                Infos[x].Position = i;
                                tmp1 = Infos[x].Donation;
                                LastUID = Infos[x].UniqId;
                            }
                        }
                        LastDonation = tmp1;
                        LastDonation++;
                        tmp1 = 0;
                        Sorted.Add(LastUID);
                        LastUID = 0;
                    }

                    World.NobilityRank.Clear();
                    for (Int32 i = 0; i < Infos.Length; i++)
                    {
                        Infos[i].Rank = 0;
                        if (Infos[i].Position >= _MAX_KING + _MAX_PRINCE + _MAX_DUKE)
                        {
                            if (Infos[i].Donation >= _DONATION_KNIGHT)
                                Infos[i].Rank = _RANK_KNIGHT;

                            if (Infos[i].Donation >= _DONATION_BARON)
                                Infos[i].Rank = _RANK_BARON;

                            if (Infos[i].Donation >= _DONATION_EARL)
                                Infos[i].Rank = _RANK_EARL;
                        }
                        else
                        {
                            if (Infos[i].Position < _MAX_KING + _MAX_PRINCE + _MAX_DUKE)
                                Infos[i].Rank = _RANK_DUKE;

                            if (Infos[i].Position < _MAX_KING + _MAX_PRINCE)
                                Infos[i].Rank = _RANK_PRINCE;

                            if (Infos[i].Position < _MAX_KING)
                                Infos[i].Rank = _RANK_KING;
                        }

                        World.NobilityRank.Add(Infos[i].UniqId, Infos[i]);

                        Player Player = null;
                        if (World.AllPlayers.TryGetValue(Infos[i].UniqId, out Player))
                            Player.Send(MsgNoble.Create(Player));
                    }
                }
            }
        }
    }
}
