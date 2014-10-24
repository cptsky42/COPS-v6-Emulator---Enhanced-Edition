// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.Collections.Generic;
using COServer.Network;
using COServer.Entities;

namespace COServer
{
    public class GameMap : Map
    {
        public GameMap(Int16 UniqId, Map Image)
            : base(UniqId, Image.Accessible, Image.Heights, Image.Width, Image.Height)
        {
            Id = Image.Id;
            Flags = Image.Flags;
            Weather = Image.Weather;
            PortalX = Image.PortalX;
            PortalY = Image.PortalY;
            if (Image.UniqId == Image.RebornMap)
                RebornMap = UniqId;
            else
                RebornMap = Image.RebornMap;
            Color = Image.Color;

            World.AllMaps.TryAdd(UniqId, this);
        }

        ~GameMap()
        {

        }

        public static Boolean Create(Int16 MapUID, out GameMap GMap)
        {
            GMap = null;

            Map Map = null;
            if (!World.AllMaps.TryGetValue(MapUID, out Map))
                return false;

            Int16 UniqId = World.LastGameMapUID;
            while (World.AllMaps.ContainsKey(UniqId))
            {
                World.LastGameMapUID++;
                UniqId = World.LastGameMapUID;

                if (World.LastGameMapUID == 25000)
                    World.LastGameMapUID = 20000;
            }

            GMap = new GameMap(UniqId, Map);
            return true;
        }
    }
}
