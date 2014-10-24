// * Created by Jean-Philippe Boivin
// * Copyright © 2012
// * Logik. Project

using System;
using System.Timers;
using System.Collections.Generic;
using System.Collections.Concurrent;
using COServer.Network;
using COServer.Entities;

namespace COServer.Games
{
    public class Tournament
    {
        private List<CageMatch> Matches;
        private List<Player> Players;

        private Timer Timer;
        private Int32 TournamentStartTick;

        public Boolean Closed = false;
        public Boolean Finished = false;

        public Tournament()
        {
            Matches = new List<CageMatch>();
            Players = new List<Player>();

            TournamentStartTick = Environment.TickCount;

            Timer = new Timer();
            Timer.Interval = 5000;
            Timer.Elapsed += new ElapsedEventHandler(Timer_Elapsed);
            Timer.Start();
        }

        private void Timer_Elapsed(Object sender, ElapsedEventArgs e)
        {
            Timer.Stop();
            if (Closed)
            {
                {
                    CageMatch[] Array = Matches.ToArray();
                    for (Int32 i = 0; i < Array.Length; i++)
                    {
                        if (Array[i].Destroyed)
                        {
                            if (Array[i].Winner == null)
                                Players.Remove(Array[i].Looser2);
                            else
                                Array[i].Winner.CPs += 250;
                            Players.Remove(Array[i].Looser);
                            Matches.Remove(Array[i]);
                        }
                    }
                }

                if (Matches.Count <= 0)
                {
                    if (Players.Count == 1)
                    {
                        Player[] Winner = Players.ToArray();
                        Winner[0].CPs += 1000;
                        World.BroadcastMsg(MsgTalk.Create("SYSTEM", "ALLUSERS", Winner[0].Name + " is the winner of the tournament!", MsgTalk.Channel.GM, 0xFFFFFF));
                        Timer.Stop();
                        Finished = true;
                        return;
                    }
                    else
                    {
                        Int32 Amount = Players.Count / 2;
                        if (Amount <= 0)
                        {
                            Timer.Stop();
                            Finished = true;
                            return;
                        }

                        Player[] Array = Players.ToArray();
                        for (Int32 i = 0; i < Amount; i++)
                            Matches.Add(new CageMatch(Array[0], Array[1]));
                    }
                }
            }
            else
            {
                if (Environment.TickCount - TournamentStartTick > 5 * 60000)
                {
                    Closed = true;

                    Int32 Amount = Players.Count / 2;
                    if (Amount <= 0)
                    {
                        Finished = true;
                        return;
                    }

                    Player[] Array = Players.ToArray();
                    for (Int32 i = 0; i < Amount; i++)
                        Matches.Add(new CageMatch(Array[i], Array[i + 1]));
                }
                World.BroadcastMsg(MsgTalk.Create("SYSTEM", "ALLUSERS", "The tournament will start soon. (Ape Mountain [568, 529]) It's 250 CPs per round, plus 1000 CPs for the winner!", MsgTalk.Channel.GM, 0xFFFFFF));
            }
            Timer.Start();
        }

        public void AddPlayer(Player Player) { lock (Players) { if (!Players.Contains(Player)) Players.Add(Player); } }
    }
}