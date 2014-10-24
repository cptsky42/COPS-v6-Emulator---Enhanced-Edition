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
            //Roar...
            public static AdvancedEntity[] GetTargetsForType11(Player Attacker)
            {
                List<AdvancedEntity> Targets = new List<AdvancedEntity>();
                try
                {
                    if (Attacker.Team != null)
                    {
                        Player TeamLeader = Attacker.Team.Leader;
                        if (!Targets.Contains(TeamLeader))
                            Targets.Add(TeamLeader);

                        Player[] TeamMembers = Attacker.Team.Members;
                        foreach (Player TeamMember in TeamMembers)
                        {
                            if (TeamMember == null)
                                continue;

                            if (!Targets.Contains(TeamMember))
                                Targets.Add(TeamMember);
                        }
                    }

                    return Targets.ToArray();
                }
                catch (Exception Exc) { Program.WriteLine(Exc); return Targets.ToArray(); }
            }
        }
    }
}
