// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.Collections.Generic;
using COServer.Network;
using COServer.Entities;

namespace COServer
{
    public partial class Battle
    {
        public static Boolean CanAttack(Player Attacker, Player Target)
        {
            try
            {
                Map Map = null;
                if (!World.AllMaps.TryGetValue(Attacker.Map, out Map))
                    return false;

                //Pk Disable
                if (Map.IsPk_Disable())
                    return false;

                if (Attacker.PkMode == (Byte)MsgAction.PkMode.Safe)
                    return false;

                if (Attacker.PkMode == (Byte)MsgAction.PkMode.Arrestment)
                    if (!Target.ContainsFlag(Player.Flag.Flashing) && Target.PkPoints < 100) //!BlueName && !BlackName
                        return false;

                if (Attacker.PkMode == (Byte)MsgAction.PkMode.Team)
                {
                    if (Attacker.Team != null && Target.Team != null)
                        if (Attacker.Team.UniqId == Target.Team.UniqId) //Same Team
                            return false;

                    if (Attacker.Friends.ContainsKey(Target.UniqId))
                        return false;

                    //Same guild / allies
                    if (Attacker.Syndicate != null && Target.Syndicate != null)
                        if (Attacker.Syndicate.UniqId == Target.Syndicate.UniqId)
                            return false;

                    if (Attacker.Syndicate != null && Target.Syndicate != null
                        && Attacker.Syndicate.IsAnAlly(Target.Syndicate.UniqId))
                        return false;
                }

                return true;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return false; }
        }

        public static Boolean CanAttack(Player Attacker, Monster Target)
        {
            try
            {
                if (Target.Id == 920)
                    return false;

                if ((Byte)(Target.Id / 100) != 9)
                    return true;

                if (Attacker.PkMode != (Byte)MsgAction.PkMode.Free)
                    return false;

                return true;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return false; }
        }

        public static Boolean CanAttack(Player Attacker, TerrainNPC Target)
        {
            try
            {
                if (Target.Type == (Byte)TerrainNPC.NpcType.SynFlag)
                {
                    if (!Target.IsAlive())
                        return false;

                    Int16 SynUID = Server.GetHolder(Target.Map);
                    if (Attacker.Syndicate != null && Attacker.Syndicate.UniqId == SynUID)
                        return false;
                }

                return true;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return false; }
        }
    }
}
