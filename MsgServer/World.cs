// * Created by Jean-Philippe Boivin
// * Copyright © 2010-2011
// * Logik. Project

using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.Concurrent;
using COServer.Threads;
using COServer.Network;
using COServer.Entities;
using COServer.Games;

namespace COServer
{
    public class World
    {
        //Tout le monde, il est temps d'entrer. Demandez à Jacol dans le marché (161, 86) de vous téléporter à l'ermitage de gibbon.
        public static Dictionary<Int32, Nobility.Info> NobilityRank = new Dictionary<Int32, Nobility.Info>();

        public static Dictionary<Int32, Player> AllMasters = new Dictionary<Int32, Player>();
        public static Dictionary<Int32, MsgAccountExt.MsgInfo> AllAccounts = new Dictionary<Int32, MsgAccountExt.MsgInfo>();

        public static ConcurrentDictionary<Int16, Map> AllMaps = new ConcurrentDictionary<Int16, Map>();
        public static Dictionary<Int32, NPC> AllNPCs = new Dictionary<Int32, NPC>();
        public static Dictionary<Int32, TerrainNPC> AllTerrainNPCs = new Dictionary<Int32, TerrainNPC>();
        public static Dictionary<Int32, Monster> AllMonsters = new Dictionary<Int32, Monster>();
        public static Dictionary<Int32, Player> AllPlayers = new Dictionary<Int32, Player>();
        public static Dictionary<Int32, Item> AllItems = new Dictionary<Int32, Item>();
        public static Dictionary<Int32, WeaponSkill> AllWeaponSkills = new Dictionary<Int32, WeaponSkill>();
        public static Dictionary<Int32, Magic> AllMagics = new Dictionary<Int32, Magic>();
        public static Dictionary<Int32, Team> AllTeams = new Dictionary<Int32, Team>();
        public static Dictionary<Int16, Syndicate.Info> AllSyndicates = new Dictionary<Int16, Syndicate.Info>();
        public static Dictionary<Int32, FloorItem> AllFloorItems = new Dictionary<Int32, FloorItem>();
        public static FloorThread FloorThread = new FloorThread();
        public static ItemThread ItemThread = new ItemThread(null);
        public static SynThread SynThread = new SynThread();
        public static Tournament Tournament = null;

        public static Int32 LastPlayerUID = Database.GetLastPlayerUID();
        public static Int32 LastMonsterUID = 400001;
        public static Int32 LastItemUID = 1;
        public static Int32 LastWeaponSkillUID = 1;
        public static Int32 LastMagicUID = 1;
        public static Int16 LastSyndicateUID = 1;
        public static Int16 LastDynMapUID = 10000;
        public static Int16 LastGameMapUID = 20000;

        public static GC GC1;
        public static GC GC2;
        public static GC GC3;
        public static GC GC4;

        public struct GC
        {
            public UInt16 Map;
            public Int16 X;
            public Int16 Y;
        }

        public static Boolean DisCity = true;

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

        public static void BroadcastMsg(Byte[] Buffer)
        {
            foreach (Player Player in AllPlayers.Values)
            {
                if (Player == null)
                    continue;

                Player.Send(Buffer);
            }
        }

        public static void BroadcastMsg(Player Sender, Byte[] Buffer, Boolean IncludeSelf)
        {
            if (IncludeSelf)
                Sender.Send(Buffer);

            foreach (Player Player in AllPlayers.Values)
            {
                if (Player == null)
                    continue;

                if (Player.UniqId == Sender.UniqId)
                    continue;

                Player.Send(Buffer);
            }
        }

        public static void BroadcastRoomMsg(Player Sender, Byte[] Buffer, Boolean IncludeSelf)
        {
            if (IncludeSelf)
                Sender.Send(Buffer);

            if (Sender.Screen == null)
                return;

            foreach (Entity Entity in Sender.Screen.Entities.Values)
            {
                if (Entity.IsPlayer())
                    (Entity as Player).Send(Buffer);
            }
        }

        public static void BroadcastRoomMsg(AdvancedEntity Entity, Byte[] Buffer)
        {
            Map Map = null;
            if (!World.AllMaps.TryGetValue(Entity.Map, out Map))
                return;

            foreach (Object Object in Map.Entities.Values)
            {
                Player Player = (Object as Player);
                if (Player == null)
                    continue;

                if (MyMath.CanSee(Player.X, Player.Y, Entity.X, Entity.Y, MyMath.NORMAL_RANGE))
                    Player.Send(Buffer);
            }
        }

        public static void BroadcastRoomMsg(FloorItem Item, Byte[] Buffer)
        {
            Map Map = null;
            if (!World.AllMaps.TryGetValue(Item.Map, out Map))
                return;

            foreach (Object Object in Map.Entities.Values)
            {
                Player Player = (Object as Player);
                if (Player == null)
                    continue;

                if (MyMath.CanSee(Player.X, Player.Y, Item.X, Item.Y, MyMath.NORMAL_RANGE + 5))
                    Player.Send(Buffer);
            }
        }

        public static void BroadcastMapMsg(Player Sender, Byte[] Buffer, Boolean IncludeSelf)
        {
            if (IncludeSelf)
                Sender.Send(Buffer);

            Map Map = null;
            if (!World.AllMaps.TryGetValue(Sender.Map, out Map))
                return;

            foreach (Entity Entity in Map.Entities.Values)
            {
                if (Entity.UniqId == Sender.UniqId)
                    continue;

                if (Entity.IsPlayer())
                    (Entity as Player).Send(Buffer);
            }
        }

        public static void BroadcastMapMsg(Int16 Map, Byte[] Buffer)
        {
            if (!AllMaps.ContainsKey(Map))
                return;

            foreach (Entity Entity in AllMaps[Map].Entities.Values)
            {
                Player Player = (Entity as Player);
                if (Player == null)
                    continue;

                Player.Send(Buffer);
            }
        }

        public static void BroadcastTeamMsg(Team Team, Byte[] Buffer)
        {
            if (Team.Leader != null)
                Team.Leader.Send(Buffer);

            foreach (Player Player in Team.Members)
            {
                if (Player == null)
                    continue;

                Player.Send(Buffer);
            }
        }

        public static void BroadcastTeamMsg(Player Sender, Byte[] Buffer, Boolean IncludeSelf)
        {
            if (Sender.Team == null)
                return;

            if (IncludeSelf)
                Sender.Send(Buffer);

            if (Sender.Team.Leader != null && Sender.Team.Leader != Sender)
                Sender.Team.Leader.Send(Buffer);

            foreach (Player Player in Sender.Team.Members)
            {
                if (Player == null)
                    continue;

                if (Player == Sender)
                    continue;

                Player.Send(Buffer);
            }
        }

        public static void BroadcastSynMsg(Syndicate.Info Syndicate, Byte[] Buffer)
        {
            Player Member = null;
            if (World.AllPlayers.TryGetValue(Syndicate.Leader.UniqId, out Member))
                Member.Send(Buffer);

            foreach (Int32 MemberUID in Syndicate.Members.Keys)
            {
                if (World.AllPlayers.TryGetValue(MemberUID, out Member))
                    Member.Send(Buffer);
            }
        }

        public static void BroadcastSynMsg(Player Sender, Byte[] Buffer, Boolean IncludeSelf)
        {
            if (Sender.Syndicate == null)
                return;

            if (IncludeSelf)
                Sender.Send(Buffer);

            Player Member = null;
            if (Sender.Syndicate.Leader.UniqId != Sender.UniqId)
            {
                if (World.AllPlayers.TryGetValue(Sender.Syndicate.Leader.UniqId, out Member))
                    Member.Send(Buffer);
            }

            foreach (Int32 MemberUID in Sender.Syndicate.Members.Keys)
            {
                if (MemberUID == Sender.UniqId)
                    continue;

                if (World.AllPlayers.TryGetValue(MemberUID, out Member))
                    Member.Send(Buffer);
            }
        }

        public static void BroadcastFriendMsg(Player Player, Byte[] Buffer)
        {
            foreach (Int32 FriendUID in Player.Friends.Keys)
            {
                Player Friend = null;
                if (!World.AllPlayers.TryGetValue(FriendUID, out Friend))
                    continue;

                Friend.Send(Buffer);
            }
        }

        public class MessageBoard
        {
            private const Int32 TITLE_SIZE = 44;
            private const Int32 LIST_SIZE = 10;

            private static List<MessageInfo> TradeBoard = new List<MessageInfo>();
            private static List<MessageInfo> FriendBoard = new List<MessageInfo>();
            private static List<MessageInfo> TeamBoard = new List<MessageInfo>();
            private static List<MessageInfo> SynBoard = new List<MessageInfo>();
            private static List<MessageInfo> OtherBoard = new List<MessageInfo>();
            private static List<MessageInfo> SystemBoard = new List<MessageInfo>();

            public struct MessageInfo
            {
                public String Author;
                public String Words;
                public String Date;
            };

            public static void Add(String Author, String Words, UInt16 Channel)
            {
                MessageInfo Info = new MessageInfo();
                Info.Author = Author;
                Info.Words = Words;
                Info.Date = DateTime.Now.ToString("yyyyMMddHHmmss");

                switch (Channel)
                {
                    case 2201:
                        TradeBoard.Add(Info);
                        break;
                    case 2202:
                        FriendBoard.Add(Info);
                        break;
                    case 2203:
                        TeamBoard.Add(Info);
                        break;
                    case 2204:
                        SynBoard.Add(Info);
                        break;
                    case 2205:
                        OtherBoard.Add(Info);
                        break;
                    case 2206:
                        SystemBoard.Add(Info);
                        break;
                }
            }

            public static void Delete(MessageInfo Message, UInt16 Channel)
            {
                switch (Channel)
                {
                    case 2201:
                        if (TradeBoard.Contains(Message))
                            TradeBoard.Remove(Message);
                        break;
                    case 2202:
                        if (FriendBoard.Contains(Message))
                            FriendBoard.Remove(Message);
                        break;
                    case 2203:
                        if (TeamBoard.Contains(Message))
                            TeamBoard.Remove(Message);
                        break;
                    case 2204:
                        if (SynBoard.Contains(Message))
                            SynBoard.Remove(Message);
                        break;
                    case 2205:
                        if (OtherBoard.Contains(Message))
                            OtherBoard.Remove(Message);
                        break;
                    case 2206:
                        if (SystemBoard.Contains(Message))
                            SystemBoard.Remove(Message);
                        break;
                }
            }

            public static String[] GetList(UInt16 Index, UInt16 Channel)
            {
                MessageInfo[] Board = null;
                switch (Channel)
                {
                    case 2201:
                        Board = TradeBoard.ToArray();
                        break;
                    case 2202:
                        Board = FriendBoard.ToArray();
                        break;
                    case 2203:
                        Board = TeamBoard.ToArray();
                        break;
                    case 2204:
                        Board = SynBoard.ToArray();
                        break;
                    case 2205:
                        Board = OtherBoard.ToArray();
                        break;
                    case 2206:
                        Board = SystemBoard.ToArray();
                        break;
                    default:
                        return null;
                }

                if (Board.Length == 0)
                    return null;

                if ((Index / 8 * LIST_SIZE) > Board.Length)
                    return null;

                String[] List = null;

                Int32 Start = (Board.Length - ((Index / 8 * LIST_SIZE) + 1));

                if (Start < LIST_SIZE)
                    List = new String[(Start + 1) * 3];
                else
                    List = new String[LIST_SIZE * 3];

                Int32 End = (Start - (List.Length / 3));

                Int32 x = 0;
                for (Int32 i = Start; i > End; i--)
                {
                    List[x + 0] = Board[i].Author;
                    if (Board[i].Words.Length > TITLE_SIZE)
                        List[x + 1] = Board[i].Words.Remove(TITLE_SIZE, Board[i].Words.Length - TITLE_SIZE);
                    else
                        List[x + 1] = Board[i].Words;
                    List[x + 2] = Board[i].Date;
                    x += 3;
                }
                return List;
            }

            public static String GetWords(String Author, UInt16 Channel)
            {
                MessageInfo[] Board = null;
                switch (Channel)
                {
                    case 2201:
                        Board = TradeBoard.ToArray();
                        break;
                    case 2202:
                        Board = FriendBoard.ToArray();
                        break;
                    case 2203:
                        Board = TeamBoard.ToArray();
                        break;
                    case 2204:
                        Board = SynBoard.ToArray();
                        break;
                    case 2205:
                        Board = OtherBoard.ToArray();
                        break;
                    case 2206:
                        Board = SystemBoard.ToArray();
                        break;
                    default:
                        return "";
                }

                foreach (MessageInfo Info in Board)
                {
                    if (Info.Author == Author)
                        return Info.Words;
                }
                return "";
            }

            public static MessageInfo GetMsgInfoByAuthor(String Author, UInt16 Channel)
            {
                MessageInfo[] Board = null;
                switch (Channel)
                {
                    case 2201:
                        Board = TradeBoard.ToArray();
                        break;
                    case 2202:
                        Board = FriendBoard.ToArray();
                        break;
                    case 2203:
                        Board = TeamBoard.ToArray();
                        break;
                    case 2204:
                        Board = SynBoard.ToArray();
                        break;
                    case 2205:
                        Board = OtherBoard.ToArray();
                        break;
                    case 2206:
                        Board = SystemBoard.ToArray();
                        break;
                    default:
                        return new MessageInfo();
                }

                foreach (MessageInfo Info in Board)
                {
                    if (Info.Author == Author)
                        return Info;
                }
                return new MessageInfo();
            }
        }
    }
}
