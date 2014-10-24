// * Created by Jean-Philippe Boivin
// * Copyright © 2010
// * Logik. Project

using System;

namespace COServer.Entities
{
    public class NPC : Entity
    {
        //enum NPCTYPE
        //{
        //    _ROLE_PLAYER = 11,			// ÆäËûÍæ¼Ò		(only use for client)
        //    _ROLE_HERO = 12,			// ×Ô¼º			(only use for client)
        //    _ROLE_MONSTER = 13,			// ¹ÖÎï			(only use for client)
        //    _ROLE_MOUSE_NPC = 17,			// Êó±êÉÏµÄNPC	(only use for client)
        //    _ROLE_MAGICITEM = 18,			// ÏÝÚå»ðÇ½		(only use for client)
        //    _ROLE_SHELF_NPC = 20,			// ÎïÆ·¼Ü
        //    _ROLE_CALL_PET = 28,			// ÕÙ»½ÊÞ	(only use for client)
        //    _EUDEMON_TRAINPLACE_NPC = 29,			// »ÃÊÞÑ±ÑøËù
        //    _AUCTION_NPC = 30,			// ÅÄÂòNPC	ÎïÆ·ÁìÈ¡NPC  LW
        //    _ROLE_MINE_NPC = 31,			// ¿óÊ¯NPC		 
        //};

        public enum NpcType
        {
            None = 0,
            ShopKeeper = 1,
            Task = 2,
            Storage = 3,
            Trunck = 4,
            Face = 5,
            Forge = 6,
            Embed = 7,

            Statuary = 9,
            SynFlag = 10,

            Booth = 14,
            SynTrans = 15,
            BoothFlag = 16,

            Dice = 19, //Game

            WeaponGoal = 21,
            MagicGoal = 22,
            BowGoal = 23,
            Target = 24,
            Furniture = 25,
            CityGate = 26,
            NeighbourGate = 27,
        };

        public enum NpcSort
        {
            None = 0,
            Task = 1,
            Recycle = 2,
            Scene = 4,
            LinkMap = 8,
            DieAction = 16,
            DelEnable = 32,
            Event = 64,
            Table = 128,
        };

        public enum NpcBase
        {
            None = 0,
            Lamp = 1,
            LowShelf = 2,
            Cabinet = 3,
            HighShelf = 4,
            BombeChest = 5,
            RosewoodCabinet = 6,
            HighCabinet = 7,
            FoldingScreen = 8,
            Dresser = 9,
            BasinRack = 10,
            Chair = 11,
            EndTable = 12,
            LeftGate = 24,
            RightGate = 27,
        };

        public String Name;

        public Byte Type;
        public Byte Base;
        public Byte Sort;

        public NPC(Int32 UniqId, String Name, Byte Type, Int16 Look, Int16 Map, UInt16 X, UInt16 Y, Byte Base, Byte Sort)
            : base(UniqId)
        {
            this.Name = Name;
            this.Type = Type;
            this.Base = Base;
            this.Sort = Sort;

            this.Look = (UInt32)Look;
            this.Map = Map;
            this.X = X;
            this.Y = Y;
            this.Direction = (Byte)(Look % 10);
        }

        ~NPC()
        {
            Name = null;
        }

        public Boolean IsShopNpc() { return Type == (Byte)NpcType.ShopKeeper; }
        public Boolean IsStorageNpc() { return Type == (Byte)NpcType.Storage; }
        public Boolean IsBoothNpc() { return Type == (Byte)NpcType.Booth; }

        public void Move(Int16 Map, UInt16 X, UInt16 Y)
        {

        }
    }
}
