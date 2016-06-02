// * Created by Jean-Philippe Boivin
// * Copyright © 2011, 2015
// * COPS v6 Emulator

using System;
using COServer.Entities;
using COServer.Network;

namespace COServer
{
    public partial class Battle
    {
        public static Boolean CanAttack(Player Attacker, Player Target)
        {
            //Pk Disable
            if (Attacker.Map.IsPk_Disable())
                return false;

            if (Attacker.PkMode == PkMode.Safe)
                return false;

            if (Attacker.PkMode == PkMode.Arrestment)
                if (!Target.IsCriminal()) //!BlueName && !BlackName
                    return false;

            if (Attacker.PkMode == PkMode.Team)
            {
                if (Attacker.Team != null && Target.Team != null)
                    if (Attacker.Team.UniqId == Target.Team.UniqId) //Same Team
                        return false;

                if (Attacker.Friends.ContainsKey(Target.UniqId))
                    return false;

                //Same guild / allies
                if (Attacker.Syndicate != null && Target.Syndicate != null)
                    if (Attacker.Syndicate.Id == Target.Syndicate.Id)
                        return false;

                if (Attacker.Syndicate != null && Target.Syndicate != null
                    && Attacker.Syndicate.IsAnAlly(Target.Syndicate.Id))
                    return false;
            }

            return true;
        }

        public static Boolean CanAttack(Player Attacker, Monster Target)
        {
            if (Target.Id == 920)
                return false;

            if ((Byte)(Target.Id / 100) != 9)
                return true;

            if (Attacker.PkMode != PkMode.Free)
                return false;

            return true;
        }

        public static Boolean CanAttack(Player Attacker, TerrainNPC Target)
        {
            if (Target.Type == (Byte)TerrainNPC.NpcType.SynFlag)
            {
                if (!Target.IsAlive())
                    return false;
            }

            return true;
        }
    }
}
