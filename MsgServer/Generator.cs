// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using COServer.Entities;

namespace COServer
{
    public class Generator
    {
        public static void Generate()
        {
            World.LastMonsterUID = 400001;
            foreach (Spawn Info in Database.AllSpawns)
            {
                for (Int32 i = 0; i < Info.Max_Per_Gen; i++)
                {
                    MonsterInfo MInfo;
                    if (!Database.AllMonsters.TryGetValue(Info.NpcType, out MInfo))
                        continue;

                    Map Map;
                    if (!World.AllMaps.TryGetValue(Info.MapId, out Map))
                        continue;

                    Int32 Rest_Secs = 10000;
                    if (Info.Rest_Secs > 10)
                        Rest_Secs = Info.Rest_Secs * 1000;

                    Monster Monster = new Monster(World.LastMonsterUID, MInfo, Rest_Secs);
                    World.LastMonsterUID++;

                    UInt16 X = (UInt16)(Info.StartX + MyMath.Generate(0, Info.AddX));
                    UInt16 Y = (UInt16)(Info.StartY + MyMath.Generate(0, Info.AddY));

                    if (!Map.IsValidPoint(X, Y))
                    {
                        Monster = null;
                        continue;
                    }

                    Monster.Map = Info.MapId;
                    Monster.StartX = X;
                    Monster.StartY = Y;
                    Monster.X = X;
                    Monster.Y = Y;

                    World.AllMonsters.Add(Monster.UniqId, Monster);
                    Map.AddEntity(Monster);
                }
            }
        }
    }
}
