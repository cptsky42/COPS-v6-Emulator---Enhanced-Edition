// * Created by Jean-Philippe Boivin
// * Copyright © 2011, 2014-2015
// * COPS v6 Emulator

using System;
using System.Collections.Generic;
using System.Linq;
using COServer.Entities;

namespace COServer
{
    public partial class Battle
    {
         //Sector skill...
        public static AdvancedEntity[] GetTargetsForType04(AdvancedEntity Attacker, UInt16 X, UInt16 Y, UInt32 Distance, UInt32 Range)
        {
            List<AdvancedEntity> targets = new List<AdvancedEntity>();

            MyMath.Sector sector = new MyMath.Sector(Attacker, X, Y);
            sector.Arrange((int)Range, (int)Distance);

            var entities = from entity in Attacker.Map.Entities.Values where entity is AdvancedEntity select (AdvancedEntity)entity;

            foreach (AdvancedEntity entity in entities)
            {
                if (targets.Count == MAX_TARGET_COUNT)
                    break;

                if (entity.IsAlive() && sector.Inside(entity.X, entity.Y))
                    targets.Add(entity);
            }

            return targets.ToArray();
        }
    }
}
