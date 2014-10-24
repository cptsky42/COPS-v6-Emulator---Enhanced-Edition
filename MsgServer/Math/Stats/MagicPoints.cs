// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using COServer.Entities;
using CO2_CORE_DLL.IO;

namespace COServer
{
    public partial class MyMath
    {
        public static UInt16 GetMagicPoints(Player Player, Boolean Set)
        {
            if (Player.TransformEndTime != 0)
                return Player.MaxMP;

            UInt16 MagicPoints = 0;

            MagicPoints += (UInt16)(Player.Spirit * 5);

            if (Player.Profession > 100 && Player.Profession < 200)
                MagicPoints += (UInt16)(MagicPoints * (Player.Profession % 10));

            lock (Player.Items)
            {
                foreach (Item Item in Player.Items.Values)
                {
                    if (Item.Position > 0 && Item.Position < 10)
                    {
                        ItemType.Entry Info;
                        if (!Database2.AllItems.TryGetValue(Item.Id, out Info))
                            continue;

                        MagicPoints += (UInt16)Info.Mana;
                    }
                }
            }

            if (Set)
                Player.MaxMP = MagicPoints;

            return MagicPoints;
        }

        public static UInt16 GetMagicPoints(UInt16 Spirit, Byte Profession)
        {
            UInt16 MagicPoints = 0;

            MagicPoints += (UInt16)(Spirit * 5);

            if (Profession > 100 && Profession < 200)
                MagicPoints += (UInt16)(MagicPoints * (Profession % 10));

            return MagicPoints;
        }
    }
}
