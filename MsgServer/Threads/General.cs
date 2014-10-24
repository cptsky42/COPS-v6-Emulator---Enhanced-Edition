// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.Threading;
using COServer.Network;
using COServer.Entities;

namespace COServer.Threads
{
    public class GeneralThread
    {
        private Thread Thread;

        public GeneralThread()
        {
            Thread = new Thread(Checking);
            Thread.IsBackground = true;
            Thread.Start();
        }

        ~GeneralThread()
        {
            Thread = null;
        }

        private void Checking()
        {
            while (true)
            {
                try
                {
                    DateTime Date = DateTime.UtcNow;
                    Map Map = null;

                    #region Dis City
                    if (World.AllMaps.TryGetValue(2024, out Map))
                    {
                        //Dis On/Off
                        if (((Date.DayOfWeek == DayOfWeek.Monday || Date.DayOfWeek == DayOfWeek.Wednesday || Date.DayOfWeek == DayOfWeek.Friday) && Date.Hour == 18 && Date.Minute < 6) ||
                            ((Date.DayOfWeek == DayOfWeek.Thursday || Date.DayOfWeek == DayOfWeek.Tuesday) && Date.Hour == 19 && Date.Minute < 6))
                        {
                            World.DisCity = true;
                            World.BroadcastMsg(MsgTalk.Create("SYSTEM", "ALLUSERS", "Dis City est commencée! Allez voir TaoïsteDeMer à Ville Tigre (532,480).", MsgTalk.Channel.GM, 0xFFFFFF));
                        }
                        else
                            World.DisCity = false;

                        //Dyrade
                        Int32 Amount = 0;
                        foreach (Entity Entity in Map.Entities.Values)
                        {
                            if (!Entity.IsMonster())
                                continue;

                            Monster Monster = Entity as Monster;
                            if (!Monster.IsAlive())
                                continue;

                            if (Monster.Id == 5058)
                                Amount++;

                            if (Monster.Id == 5059)
                                Amount++;
                        }

                        //PlutoFinal
                        if (Amount == 0)
                        {
                            MonsterInfo MInfo;
                            if (Database.AllMonsters.TryGetValue(5059, out MInfo))
                            {
                                Monster Monster = new Monster(World.LastMonsterUID, MInfo, -1);
                                World.LastMonsterUID++;

                                Monster.Map = 2024;
                                Monster.StartX = 2000;
                                Monster.StartY = 2000;
                                Monster.X = 150;
                                Monster.Y = 146;

                                World.AllMonsters.Add(Monster.UniqId, Monster);
                                Map.AddEntity(Monster);

                                lock (Map.Entities)
                                {
                                    foreach (Entity Entity in Map.Entities.Values)
                                    {
                                        if (!Entity.IsPlayer())
                                            continue;

                                        (Entity as Player).Screen.ChangeMap();
                                    }
                                }
                            }
                        }
                    }
                    #endregion

                    #region Tournament
                    if (World.Tournament != null && World.Tournament.Finished)
                        World.Tournament = null;

                    if (Date.Hour % 3 == 0 && Date.Minute < 5 && World.Tournament == null)
                        World.Tournament = new Games.Tournament();
                    #endregion

                    #region Guild War
                    if (World.AllMaps.TryGetValue(1038, out Map))
                    {
                        if (!Map.InWar && Date.Hour % 2 == 0)
                        {
                            new Games.CityWar(Map);
                            World.BroadcastMsg(MsgTalk.Create("SYSTEM", "ALLUSERS", "La guerre de guilde est commencée!", MsgTalk.Channel.GM, 0xFFFFFF));
                        }
                    }
                    #endregion
                }
                catch (Exception Exc) { Program.WriteLine(Exc); }
                Thread.Sleep(10000);
            }
        }
    }
}
