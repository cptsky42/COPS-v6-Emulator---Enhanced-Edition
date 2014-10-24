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
    public class CityWar
    {
        private Map Map;
        private TerrainNPC Pole;
        private Dictionary<Int16, Int32> Scores;

        private Timer Timer;
        private Int32 Time;

        public CityWar(Map Map)
        {
            if (Map.InWar)
                return;

            Map.InWar = true;
            Map.War = this;

            if (World.AllTerrainNPCs.ContainsKey(100000 + (Map.UniqId * 10)))
            {
                Pole = World.AllTerrainNPCs[100000 + (Map.UniqId * 10)];
                Pole.CurHP = Pole.MaxHP;
                World.BroadcastRoomMsg(Pole, MsgUserAttrib.Create(Pole, Pole.CurHP, MsgUserAttrib.Type.Life));

                Pole.War = this;
            }

            this.Map = Map;

            Map.RealFlags = Map.Flags;
            Map.AddFlag(0x0001); //PkField
            Map.AddFlag(0x0080); //PkGame
            Map.DelFlag(0x4000); //Newbie Protection
            Map.DelFlag(0x2000); //Reborn Here
            Map.ShowGates();

            World.BroadcastMapMsg(Map.UniqId, MsgMapInfo.Create(Map));

            Scores = new Dictionary<Int16, Int32>();

            Time = 3600;

            Timer = new Timer();
            Timer.Interval = 1500;
            Timer.Elapsed += new ElapsedEventHandler(Timer_Elapsed);
            Timer.Start();
        }

        private void Timer_Elapsed(Object sender, ElapsedEventArgs e)
        {
            Time--;
            if (Time % 10 == 0)
                SendInfo();

            if (Time == 0)
                Destroy();
        }

        private void SendInfo()
        {
            String StrTime = "Remaining time: " + ((Time / 60) + 1) + " min.";
            Int16[] Top5 = new Int16[Math.Min(Scores.Count, 5)];

            lock (Scores)
            {
                for (SByte i = 0; i < Top5.Length; i++)
                {
                    Int32 Big = 0;
                    foreach (KeyValuePair<Int16, Int32> KV in Scores)
                    {
                        if (i > 0)
                            if (Top5[i - 1] == KV.Key)
                                continue;

                        if (KV.Value > Big)
                        {
                            Top5[i] = KV.Key;
                            Big = KV.Value;
                        }
                    }
                }
            }

            World.BroadcastMapMsg(Map.UniqId, MsgTalk.Create("SYSTEM", "ALLUSERS", StrTime, MsgTalk.Channel.SynWar_First, 0xFFFFFF));
            for (SByte i = 0; i < Top5.Length; i++)
            {
                String Words = (i + 1).ToString() + ". ";
                if (World.AllSyndicates.ContainsKey(Top5[i]))
                    Words += World.AllSyndicates[Top5[i]].Name.PadRight(17, ' ');
                Words += Scores[Top5[i]].ToString().PadRight(8, ' ');
                World.BroadcastMapMsg(Map.UniqId, MsgTalk.Create("SYSTEM", "ALLUSERS", Words, MsgTalk.Channel.SynWar_Next, 0xFFFFFF));
            }
        }

        public void AddScore(Int16 SynUID, Int32 Amount)
        {
            if (SynUID == Map.Holder)
                return;

            lock (Scores)
            {
                if (!Scores.ContainsKey(SynUID))
                    Scores.Add(SynUID, Amount);
                else
                    Scores[SynUID] += Amount;
            }
        }

        public void Die()
        {
            if (Scores.Count == 0)
                return;

            Int16 Winner = 0;
            Int32 Big = 0;

            lock (Scores)
            {
                foreach (KeyValuePair<Int16, Int32> KV in Scores)
                {
                    if (KV.Value > Big)
                    {
                        Winner = KV.Key;
                        Big = KV.Value;
                    }
                }
                Scores.Clear();
            }

            Syndicate.Info Syn = null;
            if (World.AllSyndicates.TryGetValue(Map.Holder, out Syn))
                Syn.Money -= Pole.MaxHP;

            if (World.AllSyndicates.TryGetValue(Winner, out Syn))
                Syn.Money += Big;

            Map.Holder = Winner;

            String Name = "COPS";
            if (World.AllSyndicates.ContainsKey(Map.Holder))
                Name = World.AllSyndicates[Map.Holder].Name;
            else
                Map.Holder = 0;

            Server.SetHolder(Map.UniqId, Winner);
            Map.RenamePole();
            Map.ShowGates();

            String MapName = "";
            if (Map.UniqId == 1011)
                MapName = "la Foret";
            else if (Map.UniqId == 1020)
                MapName = "le Canyon";
            else if (Map.UniqId == 1000)
                MapName = "le Désert";
            else if (Map.UniqId == 1015)
                MapName = "les Iles";
            else if (Map.UniqId == 1038)
                MapName = "la carte de guildes";

            World.BroadcastMsg(MsgTalk.Create("SYSTEM", "ALLUSERS", Name + " a pris " + MapName + "!", MsgTalk.Channel.GM, 0xFF0000));
        }

        public void Destroy()
        {
            Timer.Close();
            Pole.War = null;

            World.BroadcastMapMsg(Map.UniqId, MsgTalk.Create("SYSTEM", "ALLUSERS", "", MsgTalk.Channel.SynWar_First, 0xFFFFFF));
            World.BroadcastMapMsg(Map.UniqId, MsgTalk.Create("SYSTEM", "ALLUSERS", "", MsgTalk.Channel.SynWar_Next, 0xFFFFFF));

            String Name = "";
            if (World.AllSyndicates.ContainsKey(Map.Holder))
                Name = World.AllSyndicates[Map.Holder].Name;
            else
                Map.Holder = 0;

            String MapName = "";
            if (Map.UniqId == 1011)
                MapName = "la Foret";
            else if (Map.UniqId == 1020)
                MapName = "le Canyon";
            else if (Map.UniqId == 1000)
                MapName = "le Désert";
            else if (Map.UniqId == 1015)
                MapName = "les Iles";
            else if (Map.UniqId == 1038)
                MapName = "la carte de guildes";

            if (Name != "")
                World.BroadcastMsg(MsgTalk.Create("SYSTEM", "ALLUSERS", Name + " a assiégé " + MapName + "!", MsgTalk.Channel.GM, 0xFF0000));

            Map.Flags = Map.RealFlags;
            Map.HideGates();

            World.BroadcastMapMsg(Map.UniqId, MsgMapInfo.Create(Map));
            Map.War = null;
            Map.InWar = false;
        }
    }
}