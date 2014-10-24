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
            //Sector skill...
            public static AdvancedEntity[] GetTargetsForType04(AdvancedEntity Attacker, UInt16 X, UInt16 Y, UInt32 Distance, UInt32 Range)
            {
                List<AdvancedEntity> Targets = new List<AdvancedEntity>();
                try
                {
                    Map Map = null;
                    if (!World.AllMaps.TryGetValue(Attacker.Map, out Map))
                        return Targets.ToArray();

                    MyMath.Sector Sector = new MyMath.Sector(Attacker.X, Attacker.Y, X, Y);
                    Sector.Arrange((Int32)(Range), (Int32)Distance);

                    foreach (Object Object in Map.Entities.Values)
                    {
                        AdvancedEntity Entity = (Object as AdvancedEntity);
                        if (Entity == null)
                            continue;

                        if (!Sector.Inside(Entity.X, Entity.Y))
                            continue;

                        Targets.Add(Entity);
                    }

                    return Targets.ToArray();
                }
                catch (Exception Exc) { Program.WriteLine(Exc); return Targets.ToArray(); }
            }
        }
    }
}
