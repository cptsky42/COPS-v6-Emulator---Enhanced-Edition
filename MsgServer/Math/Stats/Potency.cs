// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using COServer.Entities;

namespace COServer
{
    public partial class MyMath
    {
        public static Int16 GetPotency(Player Player, Boolean Set)
        {
            Int16 Potency = 0;

            Potency += Player.Level;
            Potency += (Int16)(Player.Metempsychosis * 5);
            if (Player.Nobility != null)
                Potency += (Int16)Player.Nobility.Rank;

            lock (Player.Items)
            {
                foreach (Item Item in Player.Items.Values)
                {
                    if (Item.Position > 0 && Item.Position < 10)
                    {
                        Byte ItemPotency = 0;
                        ItemPotency += (Byte)Math.Max(0, (Item.Id % 10) - 5);
                        ItemPotency += Item.Craft;

                        if (Item.Gem1 != 0)
                            ItemPotency++;

                        if (Item.Gem1 % 10 == 3)
                            ItemPotency++;

                        if (Item.Gem2 != 0)
                            ItemPotency++;

                        if (Item.Gem2 % 10 == 3)
                            ItemPotency++;

                        if (((Item.Id - (Item.Id % 100000)) / 100000) == 5)
                            ItemPotency *= 2;

                        Potency += ItemPotency;
                    }
                }
            }

            if (Set)
                Player.Potency = Potency;

            return Potency;
        }
    }
}
