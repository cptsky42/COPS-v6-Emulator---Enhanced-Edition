// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2010 - 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.Timers;
using COServer.Network;

namespace COServer.Entities
{
    /// <summary>
    /// Base class of all entity on a map.
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// First valid UID for a NPC. 
        /// </summary>
        public const Int32 SCENEID_FIRST = 1;
        /// <summary>
        /// First valid UID for a system NPC.
        /// </summary>
        public const Int32 SYSNPCID_FIRST = 1;
        /// <summary>
        /// Last valid UID for a system NPC.
        /// </summary>
        public const Int32 SYSNPCID_LAST = 99999;
        /// <summary>
        /// First valid UID for a user/dynamic NPC.
        /// </summary>
        public const Int32 DYNANPCID_FIRST = 100001;
        /// <summary>
        /// Last valid UID for a user/dynamic NPC.
        /// </summary>
        public const Int32 DYNANPCID_LAST = 199999;
        /// <summary>
        /// Last valid UID for a NPC.
        /// </summary>
        public const Int32 SCENEID_LAST = 299999;

        /// <summary>
        /// First valid UID for an advanced NPC.
        /// </summary>
        public const Int32 NPCSERVERID_FIRST = 400001;
        /// <summary>
        /// First valid UID for a monster.
        /// </summary>
        public const Int32 MONSTERID_FIRST = 400001;
        /// <summary>
        /// Last valid UID for a monster.
        /// </summary>
        public const Int32 MONSTERID_LAST = 499999;
        /// <summary>
        /// First valid UID for a pet.
        /// </summary>
        public const Int32 PETID_FIRST = 500001;
        /// <summary>
        /// Last valid UID for a pet.
        /// </summary>
        public const Int32 PETID_LAST = 599999;
        /// <summary>
        /// Last valid UID for a pet.
        /// </summary>
        public const Int32 NPCSERVERID_LAST = 699999;

        /// <summary>
        /// First valid UID for a called pet.
        /// </summary>
        public const Int32 CALLPETID_FIRST = 700001;
        /// <summary>
        /// First valid UID for a called pet.
        /// </summary>
        public const Int32 CALLPETID_LAST = 799999;

        // unused for the moment ?
        public const Int32 TRAPID_FIRST = 900001;
        public const Int32 MAGICTRAPID_FIRST = 900001;
        public const Int32 MAGICTRAPID_LAST = 989999;
        public const Int32 SYSTRAPID_FIRST = 990001;
        public const Int32 SYSTRAPID_LAST = 999999;
        public const Int32 TRAPID_LAST = 999999;

        /// <summary>
        /// First valid UID for a player.
        /// </summary>
        public const Int32 PLAYERID_FIRST = 1000000;
        /// <summary>
        /// Last valid UID for a player.
        /// </summary>
        public const Int32 PLAYERID_LAST = 1999999999;

        protected readonly Int32 mUID;

        protected UInt32 mLook = 0;

        public Int32 UniqId { get { return mUID; } }

        /// <summary>
        /// The look of the entity.
        /// </summary>
        public virtual UInt32 Look
        { 
            get { return mLook; }
            set
            {
                mLook = value;

                var msg = new MsgUserAttrib(this, mLook, MsgUserAttrib.AttributeType.Look);
                World.BroadcastRoomMsg(this, msg);
            }
        }

        public GameMap Map;
        public UInt16 X;
        public UInt16 Y;
        public Byte Direction;

        public Entity(Int32 aUID)
        {
            mUID = aUID;

            Map = null;
            X = 0;
            Y = 0;
            Direction = 0;
        }

        /// <summary>
        /// Called when the timer elapse.
        /// </summary>
        public virtual void TimerElapsed(Object aSender, ElapsedEventArgs Args) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aMsg"></param>
        public virtual void Send(Msg aMsg) { }

        public Boolean IsPlayer() { return UniqId >= PLAYERID_FIRST && UniqId <= PLAYERID_LAST; }
        public Boolean IsMonster() { return UniqId >= MONSTERID_FIRST && UniqId <= MONSTERID_LAST; }
        public Boolean IsPet() { return UniqId >= PETID_FIRST && UniqId <= PETID_LAST; }
        public Boolean IsCallPet() { return UniqId >= CALLPETID_FIRST && UniqId <= CALLPETID_LAST; }
        public Boolean IsNPC() { return UniqId >= SCENEID_FIRST && UniqId <= NPCSERVERID_LAST && !IsMonster() && !IsPet(); }
        public Boolean IsTerrainNPC() { return UniqId >= DYNANPCID_FIRST && UniqId <= DYNANPCID_LAST; }

        public static Boolean IsPlayer(Int32 UniqId) { return UniqId >= PLAYERID_FIRST && UniqId <= PLAYERID_LAST; }
        public static Boolean IsMonster(Int32 UniqId) { return UniqId >= MONSTERID_FIRST && UniqId <= MONSTERID_LAST; }
        public static Boolean IsPet(Int32 UniqId) { return UniqId >= PETID_FIRST && UniqId <= PETID_LAST; }
        public static Boolean IsCallPet(Int32 UniqId) { return UniqId >= CALLPETID_FIRST && UniqId <= CALLPETID_LAST; }
        public static Boolean IsNPC(Int32 UniqId) { return UniqId >= SCENEID_FIRST && UniqId <= NPCSERVERID_LAST && !IsMonster(UniqId) && !IsPet(UniqId); }
        public static Boolean IsTerrainNPC(Int32 UniqId) { return UniqId >= DYNANPCID_FIRST && UniqId <= DYNANPCID_LAST; }
    }
}
