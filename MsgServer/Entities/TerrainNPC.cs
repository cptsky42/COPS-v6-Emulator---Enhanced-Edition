// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * COPS v6 Emulator

using System;
using COServer.Network;

namespace COServer.Entities
{
    public class TerrainNPC : AdvancedEntity
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

        public Byte Type;
        public Byte Base;
        public Byte Sort;

        public UInt16 RealX;
        public UInt16 RealY;

        public TerrainNPC(Int32 UniqId, String Name, Byte Type, UInt32 Look, GameMap Map, UInt16 X, UInt16 Y, Byte Base, Byte Sort, Byte Level, UInt32 Life, UInt16 Defence, UInt16 MagicDef)
            : base(UniqId)
        {
            if (Look / 10 == 24 || Look / 10 == 27)
                this.Name = "!!";
            else if (Type == (Byte)NpcType.SynFlag)
                this.Name = "Pole";
            else
                this.Name = null;
            this.Type = Type;
            this.Base = Base;
            this.Sort = Sort;

            mLook = Look;
            this.Map = Map;
            this.X = X;
            this.Y = Y;
            this.Direction = (Byte)(Look % 10);

            this.RealX = X;
            this.RealY = Y;

            this.Level = Level;
            this.CurHP = (Int32)Life;
            this.MaxHP = (Int32)Life;
            this.Defence = Defence;
            this.MagicDef = MagicDef;

            if (UniqId - (UniqId % 10) == 100000 + (Map.Id * 10))
            {
                if (UniqId % 10 == 1 || UniqId % 10 == 2 || (UniqId % 10 >= 5 && UniqId % 10 <= 8))
                {
                    this.X = 2000;
                    this.Y = 2000;
                }
            }
        }

        public void GetAttacked(Player Attacker, Int32 Damage)
        {

        }

        public void Die()
        {
            if (Type == (Byte)NpcType.CityGate)
            {
                CurHP = 1;
                if (Look / 10 == Base)
                {
                    Look += 10;
                }
            }
            else if (Type == (Byte)NpcType.SynFlag)
            {
                CurHP = MaxHP;
            }
            else
                CurHP = MaxHP;
            World.BroadcastRoomMsg(this, new MsgUserAttrib(this, CurHP, MsgUserAttrib.AttributeType.Life));
        }

        public Boolean IsGreen(AdvancedEntity Entity) { return (Entity.Level - Level) >= 3; }
        public Boolean IsWhite(AdvancedEntity Entity) { return (Entity.Level - Level) >= 0 && (Entity.Level - Level) < 3; }
        public Boolean IsRed(AdvancedEntity Entity) { return (Entity.Level - Level) >= -4 && (Entity.Level - Level) < 0; }
        public Boolean IsBlack(AdvancedEntity Entity) { return (Entity.Level - Level) < -4; }
    }
}
