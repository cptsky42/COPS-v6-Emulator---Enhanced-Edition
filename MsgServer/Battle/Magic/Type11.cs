// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * COPS v6 Emulator

using System;
using System.Collections.Generic;
using COServer.Entities;

namespace COServer
{
    public partial class Battle
    {
        //Roar...
        public static AdvancedEntity[] GetTargetsForType11(Player Attacker)
        {
            List<AdvancedEntity> targets = new List<AdvancedEntity>();

            if (Attacker.Team != null)
            {
                Player leader = Attacker.Team.Leader;
                if (!targets.Contains(leader))
                    targets.Add(leader);

                Player[] members = Attacker.Team.Members;
                foreach (Player member in members)
                {
                    if (member == null)
                        continue;

                    if (!targets.Contains(member))
                        targets.Add(member);
                }
            }

            return targets.ToArray();
        }
    }
}
