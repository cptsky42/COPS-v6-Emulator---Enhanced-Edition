// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.Collections.Generic;
using COServer.Entities;

namespace COServer
{
    public partial class Battle
    {
        public partial class Magic
        {
            //Circle skill...
            public static AdvancedEntity[] GetTargetsForType05(AdvancedEntity Attacker, UInt16 X, UInt16 Y, UInt32 Range)
            {
                List<AdvancedEntity> Targets = new List<AdvancedEntity>();
                try
                {
                    Map Map = null;
                    if (!World.AllMaps.TryGetValue(Attacker.Map, out Map))
                        return Targets.ToArray();

                    foreach (Object Object in Map.Entities.Values)
                    {
                        AdvancedEntity Entity = (Object as AdvancedEntity);
                        if (Entity == null)
                            continue;

                        if (MyMath.GetDistance(Attacker.X, Attacker.Y, Entity.X, Entity.Y) > Range)
                            continue;

                        if (!Targets.Contains(Entity))
                            Targets.Add(Entity);
                    }

                    return Targets.ToArray();
                }
                catch (Exception Exc) { Program.WriteLine(Exc); return Targets.ToArray(); }
            }
        }
    }
}
