// * Created by Jean-Philippe Boivin
// * Copyright © 2011, 2014-2015
// * COPS v6 Emulator

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using COServer.Entities;

namespace COServer
{
    public partial class Battle
    {
        //Line skill...
        public static AdvancedEntity[] GetTargetsForType14(AdvancedEntity Attacker, UInt16 X, UInt16 Y, UInt32 Range)
        {
            List<AdvancedEntity> targets = new List<AdvancedEntity>();

            List<Point> Coords = new List<Point>();
            MyMath.DDALine(Attacker.X, Attacker.Y, X, Y, (Int32)Range, ref Coords);

            var entities = from entity in Attacker.Map.Entities.Values where entity is AdvancedEntity select (AdvancedEntity)entity;

            foreach (AdvancedEntity entity in entities)
            {
                if (targets.Count == MAX_TARGET_COUNT)
                    break;

                if (!entity.IsAlive())
                    continue;

                foreach (Point Coord in Coords)
                {
                    if (Coord.X == entity.X && Coord.Y == entity.Y)
                    {
                        targets.Add(entity);
                        break;
                    }
                }
            }

            return targets.ToArray();
        }
    }
}
