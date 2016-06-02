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
        //Circle skill...
        public static AdvancedEntity[] GetTargetsForType05(AdvancedEntity Attacker, UInt16 X, UInt16 Y, UInt32 Range)
        {
            List<AdvancedEntity> targets = new List<AdvancedEntity>();

            var entities = from entity in Attacker.Map.Entities.Values where entity is AdvancedEntity select (AdvancedEntity)entity;

            foreach (AdvancedEntity entity in entities)
            {
                if (targets.Count == MAX_TARGET_COUNT)
                    break;

                if (entity.IsAlive() && MyMath.GetDistance(Attacker.X, Attacker.Y, entity.X, entity.Y) <= Range)
                    targets.Add(entity);
            }

            return targets.ToArray();
        }
    }
}
