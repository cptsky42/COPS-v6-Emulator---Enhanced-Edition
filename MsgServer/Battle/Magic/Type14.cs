// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.Drawing;
using System.Collections.Generic;
using COServer.Entities;

namespace COServer
{
    public partial class Battle
    {
        public partial class Magic
        {
            //Line skill...
            public static AdvancedEntity[] GetTargetsForType14(AdvancedEntity Attacker, UInt16 X, UInt16 Y, UInt32 Range)
            {
                List<AdvancedEntity> Targets = new List<AdvancedEntity>();
                try
                {
                    List<Point> Coords = new List<Point>();
                    MyMath.DDALine(Attacker.X, Attacker.Y, X, Y, (Int32)Range, ref Coords);

                    Map Map = null;
                    if (!World.AllMaps.TryGetValue(Attacker.Map, out Map))
                        return Targets.ToArray();

                    foreach (Object Object in Map.Entities.Values)
                    {
                        AdvancedEntity Entity = (Object as AdvancedEntity);
                        if (Entity == null)
                            continue;

                        foreach (Point Coord in Coords)
                        {
                            if (Coord.X == Entity.X && Coord.Y == Entity.Y)
                            {
                                if (!Targets.Contains(Entity))
                                    Targets.Add(Entity);
                                break;
                            }
                        }
                    }

                    return Targets.ToArray();
                }
                catch (Exception Exc) { Program.WriteLine(Exc); return Targets.ToArray(); }
            }
        }
    }
}
