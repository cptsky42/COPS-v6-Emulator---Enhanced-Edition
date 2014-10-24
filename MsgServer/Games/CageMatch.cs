// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.Timers;
using System.Collections.Generic;
using COServer.Network;
using COServer.Entities;

namespace COServer.Games
{
    public class CageMatch
    {
        private Player Player1;
        private Player Player2;

        private Int32 Score1;
        private Int32 Score2;

        private GameMap Map;

        private Timer Timer;
        private Int32 Time;

        public Boolean Destroyed = false;
        public Player Winner = null;
        public Player Looser = null;
        public Player Looser2 = null;

        public CageMatch(Player First, Player Second)
        {
            Player1 = First;
            Player2 = Second;

            Score1 = 0;
            Score2 = 0;

            if (!GameMap.Create(7002, out Map))
                return;

            Player1.Game = this;
            Player2.Game = this;

            Player1.GameRequest = -1;
            Player2.GameRequest = -1;

            Player1.Move(Map.UniqId, Map.PortalX, Map.PortalY);
            Player2.Move(Map.UniqId, Map.PortalX, Map.PortalY);

            Time = 300;

            Timer = new Timer();
            Timer.Interval = 1000;
            Timer.Elapsed += new ElapsedEventHandler(Timer_Elapsed);
            Timer.Start();
        }

        private void Timer_Elapsed(Object sender, ElapsedEventArgs e)
        {
            if (Time % 10 == 0)
                SendInfo();
            Time--;

            if (Player1.Map != Map.UniqId)
            {
                Destroy();
                return;
            }

            if (Player2.Map != Map.UniqId)
            {
                Destroy();
                return;
            }

            if (Time == 0)
                Destroy();
        }

        public void PlayerDie(Player Player)
        {
            if (Player.UniqId == Player1.UniqId)
                Score2++;
            else if (Player.UniqId == Player2.UniqId)
                Score1++;
            SendInfo();
        }

        ~CageMatch()
        {
            Player1 = null;
            Player2 = null;

            Map = null;
        }

        private void SendInfo()
        {
            String StrTime = "Remaining time: " + ((Time / 60) + 1) + " min.";
            String First = "{0} : {1}";
            String Second = "{0} : {1}";

            if (Score1 >= Score2)
            {
                First = String.Format(First, Player1.Name.PadRight(16, ' '), Score1.ToString().PadLeft(4, '0'));
                Second = String.Format(Second, Player2.Name.PadRight(16, ' '), Score2.ToString().PadLeft(4, '0'));
            }
            else
            {
                First = String.Format(First, Player2.Name.PadRight(16, ' '), Score2.ToString().PadLeft(4, '0'));
                Second = String.Format(Second, Player1.Name.PadRight(16, ' '), Score1.ToString().PadLeft(4, '0'));
            }

            Player1.Send(MsgTalk.Create("SYSTEM", "ALLUSERS", StrTime, MsgTalk.Channel.SynWar_First, 0xFFFFFF));
            Player1.Send(MsgTalk.Create("SYSTEM", "ALLUSERS", First, MsgTalk.Channel.SynWar_Next, 0xFFFFFF));
            Player1.Send(MsgTalk.Create("SYSTEM", "ALLUSERS", Second, MsgTalk.Channel.SynWar_Next, 0xFFFFFF));

            Player2.Send(MsgTalk.Create("SYSTEM", "ALLUSERS", StrTime, MsgTalk.Channel.SynWar_First, 0xFFFFFF));
            Player2.Send(MsgTalk.Create("SYSTEM", "ALLUSERS", First, MsgTalk.Channel.SynWar_Next, 0xFFFFFF));
            Player2.Send(MsgTalk.Create("SYSTEM", "ALLUSERS", Second, MsgTalk.Channel.SynWar_Next, 0xFFFFFF));
        }

        public void Destroy()
        {
            Player1.Send(MsgTalk.Create("SYSTEM", "ALLUSERS", "", MsgTalk.Channel.SynWar_First, 0xFFFFFF));
            Player2.Send(MsgTalk.Create("SYSTEM", "ALLUSERS", "", MsgTalk.Channel.SynWar_First, 0xFFFFFF));

            Player1.Move(1002, 400, 400);
            Player2.Move(1002, 400, 400);

            String Words = "{0} is the winner. He killed {1} {2} times and got killed {3} times";
            if (Score1 > Score2)
            {
                Winner = Player1;
                Looser = Player2;
                Words = String.Format(Words, Player1.Name, Player2.Name, Score1, Score2);
            }
            else if (Score2 > Score1)
            {
                Winner = Player2;
                Looser = Player1;
                Words = String.Format(Words, Player2.Name, Player1.Name, Score2, Score1);
            }
            else
            {
                Looser = Player1;
                Looser2 = Player2;
                Words = "No winner!";
            }

            Player1.Send(MsgTalk.Create("SYSTEM", "ALLUSERS", Words, MsgTalk.Channel.GM, 0xFFFFFF));
            Player2.Send(MsgTalk.Create("SYSTEM", "ALLUSERS", Words, MsgTalk.Channel.GM, 0xFFFFFF));

            if (World.AllMaps.ContainsKey(Map.UniqId))
            {
                COServer.Map Tmp;
                World.AllMaps.TryRemove(Map.UniqId, out Tmp);
            }
            Map = null;

            Player1.Game = null;
            Player2.Game = null;

            Player1 = null;
            Player2 = null;
            Timer = null;

            Destroyed = true;
        }
    }
}