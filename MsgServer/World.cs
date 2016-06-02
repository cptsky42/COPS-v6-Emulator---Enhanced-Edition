// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2010 - 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.Collections.Generic;
using System.Linq;
using COServer.Entities;
using COServer.Network;
using COServer.Workers;

namespace COServer
{
    public class World
    {
        public static Dictionary<Int32, NPC> AllNPCs = new Dictionary<Int32, NPC>();
        public static Dictionary<Int32, TerrainNPC> AllTerrainNPCs = new Dictionary<Int32, TerrainNPC>();
        public static Dictionary<Int32, Monster> AllMonsters = new Dictionary<Int32, Monster>();
        public static Dictionary<Int32, Player> AllPlayers = new Dictionary<Int32, Player>();
        public static Dictionary<String, Player> AllPlayerNames = new Dictionary<String, Player>();
        public static Dictionary<Int32, Item> AllItems = new Dictionary<Int32, Item>();
        public static Dictionary<Int32, Team> AllTeams = new Dictionary<Int32, Team>();
        public static Dictionary<UInt16, Syndicate> AllSyndicates = new Dictionary<UInt16, Syndicate>();
        public static Dictionary<Int32, FloorItem> AllFloorItems = new Dictionary<Int32, FloorItem>();
        public static ItemDestroyer FloorThread = ItemDestroyer.Instance;
        private static GeneratorThread GeneratorThread = GeneratorThread.Instance;

        public static MessageBoard TradeBoard = new MessageBoard();
        public static MessageBoard FriendBoard = new MessageBoard();
        public static MessageBoard TeamBoard = new MessageBoard();
        public static MessageBoard SynBoard = new MessageBoard();
        public static MessageBoard OtherBoard = new MessageBoard();
        public static MessageBoard SystemBoard = new MessageBoard();

        public static Int32 LastPlayerUID = Database.GetLastPlayerUID();
        public static Int32 LastMonsterUID = Entity.MONSTERID_FIRST;
        public static Int32 LastItemUID = 1;

        public static Int32 GetNextItemUID()
        {
            Int32 NextUID = LastItemUID++;

            if (NextUID == 2000000 || NextUID < 1)
                NextUID = 1;

            while (World.AllItems.ContainsKey(NextUID))
            {
                NextUID++;

                if (NextUID == 2000000 || NextUID < 1)
                    NextUID = 1;
            }

            LastItemUID = NextUID;
            return NextUID;
        }

        private static Int32 sLastDynaNpcUID = Entity.DYNANPCID_FIRST;

        public static Int32 GetNextDynaNpcUID()
        {
            lock (World.AllNPCs)
            {
                do
                {
                    ++sLastDynaNpcUID;
                    if (sLastDynaNpcUID > Entity.DYNANPCID_LAST)
                        sLastDynaNpcUID = Entity.DYNANPCID_FIRST;
                }
                while (World.AllNPCs.ContainsKey(sLastDynaNpcUID));
            }

            return sLastDynaNpcUID;
        }

        public static void BroadcastMsg(Msg aMsg)
        {
            foreach (Player player in AllPlayers.Values)
            {
                if (player == null)
                    continue;

                player.Send(aMsg);
            }
        }

        public static void BroadcastMsg(Player aPlayer, Msg aMsg, Boolean aIncludeSelf)
        {
            if (aIncludeSelf)
                aPlayer.Send(aMsg);

            foreach (Player player in AllPlayers.Values)
            {
                if (player.UniqId == aPlayer.UniqId)
                    continue;

                player.Send(aMsg);
            }
        }

        public static void BroadcastRoomMsg(Player aPlayer, Msg aMsg, Boolean aIncludeSelf)
        {
            if (aIncludeSelf)
                aPlayer.Send(aMsg);

            if (aPlayer.Screen == null)
                return;

            var players = from entity in aPlayer.Screen.mEntities.Values where entity.IsPlayer() select (Player)entity;

            foreach (Player player in players)
                player.Send(aMsg);
        }

        public static void BroadcastRoomMsg(Entity aEntity, Msg aMsg)
        {
            var players = from entity in aEntity.Map.Entities.Values where entity.IsPlayer() select (Player)entity;

            foreach (Player player in players)
            {
                if (MyMath.CanSee(player.X, player.Y, aEntity.X, aEntity.Y, MyMath.NORMAL_RANGE))
                    player.Send(aMsg);
            }
        }

        public static void BroadcastRoomMsg(FloorItem aItem, Msg aMsg)
        {
            var players = from entity in aItem.Map.Entities.Values where entity.IsPlayer() select (Player)entity;

            foreach (Player player in players)
            {
                if (MyMath.CanSee(player.X, player.Y, aItem.X, aItem.Y, MyMath.NORMAL_RANGE + 5))
                    player.Send(aMsg);
            }
        }

        public static void BroadcastMapMsg(Entity aEntity, Msg aMsg)
        {
            var players = from entity in aEntity.Map.Entities.Values where entity.IsPlayer() select (Player)entity;

            foreach (Player player in players)
                player.Send(aMsg);
        }

        public static void BroadcastMapMsg(Player aPlayer, Msg aMsg, Boolean aIncludeSelf)
        {
            if (aIncludeSelf)
                aPlayer.Send(aMsg);

            var players = from entity in aPlayer.Map.Entities.Values where entity.IsPlayer() select (Player)entity;

            foreach (Player player in players)
            {
                if (player.UniqId == aPlayer.UniqId)
                    continue;

                player.Send(aMsg);
            }
        }

        public static void BroadcastMapMsg(UInt32 aMapId, Msg aMsg)
        {
            GameMap map = null;
            if (MapManager.TryGetMap(aMapId, out map))
            {
                var players = from entity in map.Entities.Values where entity.IsPlayer() select (Player)entity;

                foreach (Player player in players)
                    player.Send(aMsg);
            }
        }

        public static void BroadcastTeamMsg(Player Sender, Msg aMsg, Boolean IncludeSelf)
        {
            if (Sender.Team == null)
                return;

            if (IncludeSelf)
                Sender.Send(aMsg);

            if (Sender.Team.Leader != null && Sender.Team.Leader != Sender)
                Sender.Team.Leader.Send(aMsg);

            foreach (Player Player in Sender.Team.Members)
            {
                if (Player == null)
                    continue;

                if (Player == Sender)
                    continue;

                Player.Send(aMsg);
            }
        }

        public static void BroadcastSynMsg(Syndicate Syndicate, Msg aMsg)
        {
            Player Member = null;
            if (World.AllPlayers.TryGetValue(Syndicate.Leader.Id, out Member))
                Member.Send(aMsg);

            foreach (Int32 MemberUID in Syndicate.Members.Keys)
            {
                if (World.AllPlayers.TryGetValue(MemberUID, out Member))
                    Member.Send(aMsg);
            }
        }

        public static void BroadcastSynMsg(Player Sender, Msg aMsg, Boolean IncludeSelf)
        {
            if (Sender.Syndicate == null)
                return;

            if (IncludeSelf)
                Sender.Send(aMsg);

            Player Member = null;
            if (Sender.Syndicate.Leader.Id != Sender.UniqId)
            {
                if (World.AllPlayers.TryGetValue(Sender.Syndicate.Leader.Id, out Member))
                    Member.Send(aMsg);
            }

            foreach (Int32 MemberUID in Sender.Syndicate.Members.Keys)
            {
                if (MemberUID == Sender.UniqId)
                    continue;

                if (World.AllPlayers.TryGetValue(MemberUID, out Member))
                    Member.Send(aMsg);
            }
        }

        public static void BroadcastFriendMsg(Player Player, Msg aMsg)
        {
            foreach (Int32 FriendUID in Player.Friends.Keys)
            {
                Player Friend = null;
                if (!World.AllPlayers.TryGetValue(FriendUID, out Friend))
                    continue;

                Friend.Send(aMsg);
            }
        }
    }
}
