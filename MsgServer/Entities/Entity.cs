// * Created by Jean-Philippe Boivin
// * Copyright © 2010
// * Logik. Project

using System;

namespace COServer.Entities
{
    public class Entity
    {
        const Int32 NpcId_Min = 1;
        const Int32 DynaNpcId_Min = 100001;
        const Int32 DynaNpcId_Max = 199999;
        const Int32 MonsterId_Min = 400001;
        const Int32 MonsterId_Max = 499999;
        const Int32 PetId_Min = 500001;
        const Int32 PetId_Max = 599999;
        const Int32 NpcId_Max = 700000;
        const Int32 CallPetId_Min = 700001;
        const Int32 CallPetId_Max = 799999;
        const Int32 PlayerId_Min = 1000000;
        const Int32 PlayerId_Max = 10000000;

        protected Int32 UniqIdValue = -1;

        public UInt32 Look;
        public Int16 Map;
        public UInt16 X;
        public UInt16 Y;
        public Byte Direction;

        public Int32 UniqId { get { return this.UniqIdValue; } }

        public Entity(Int32 UniqId)
        {
            this.UniqIdValue = UniqId;
        }

        public Boolean IsPlayer() { return UniqId >= PlayerId_Min && UniqId <= PlayerId_Max; }
        public Boolean IsMonster() { return UniqId >= MonsterId_Min && UniqId <= MonsterId_Max; }
        public Boolean IsPet() { return UniqId >= PetId_Min && UniqId <= PetId_Max; }
        public Boolean IsCallPet() { return UniqId >= CallPetId_Min && UniqId <= CallPetId_Max; }
        public Boolean IsNPC() { return UniqId >= NpcId_Min && UniqId <= NpcId_Max && !IsMonster() && !IsPet(); }
        public Boolean IsTerrainNPC() { return UniqId >= DynaNpcId_Min && UniqId <= DynaNpcId_Max; }

        public static Boolean IsPlayer(Int32 UniqId) { return UniqId >= PlayerId_Min && UniqId <= PlayerId_Max; }
        public static Boolean IsMonster(Int32 UniqId) { return UniqId >= MonsterId_Min && UniqId <= MonsterId_Max; }
        public static Boolean IsPet(Int32 UniqId) { return UniqId >= PetId_Min && UniqId <= PetId_Max; }
        public static Boolean IsCallPet(Int32 UniqId) { return UniqId >= CallPetId_Min && UniqId <= CallPetId_Max; }
        public static Boolean IsNPC(Int32 UniqId) { return UniqId >= NpcId_Min && UniqId <= NpcId_Max && !IsMonster(UniqId) && !IsPet(UniqId); }
        public static Boolean IsTerrainNPC(Int32 UniqId) { return UniqId >= DynaNpcId_Min && UniqId <= DynaNpcId_Max; }
    }
}
