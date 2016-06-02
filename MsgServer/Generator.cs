// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2011 - 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.Timers;
using COServer.Entities;
using COServer.Network;

namespace COServer
{
    /// <summary>
    /// A generator is a monster's spawn handler.
    /// It is totally based on the one created by TQ.
    /// </summary>
    public class Generator : IDisposable
    {
        /// <summary>
        /// The logger of the class.
        /// </summary>
        private static readonly log4net.ILog sLogger = log4net.LogManager.GetLogger(typeof(Generator));

        /// <summary>
        /// Pseudo-random number generator derived from CO2's code.
        /// </summary>
        private static readonly SafeRandom sRand = new SafeRandom();

        /// <summary>
        /// The generated monster type.
        /// </summary>
        private MonsterInfo mMonsterType;

        /// <summary>
        /// The map where the monsters are generated.
        /// </summary>
        private GameMap mMap;

        private UInt16 mBoundX;
        private UInt16 mBoundY;
        private UInt16 mBoundCX;
        private UInt16 mBoundCY;

        private Int32 mGrid;
        private UInt16 mRestSecs;
        private UInt16 mMaxPerGen;

        internal UInt32 mAmount;
        internal UInt32 mGenAmount;
        private UInt32 mCurGen;
        private Timer mTimer;

        private Int32 mMaxNPC;
        private Int32 mIdxLastGen;

        private bool mTimerElapsed = false;

        /// <summary>
        /// Indicate whether or not the object is disposed.
        /// </summary>
        private bool mIsDisposed = false;

        internal Generator(GameMap aMap, UInt16 aBoundX, UInt16 aBoundY, UInt16 aBoundCX, UInt16 aBoundCY,
                           UInt16 aMaxNPC, UInt16 aRestSecs, UInt16 aMaxPerGen,
                           MonsterInfo aMonsterType)
        {
            mMonsterType = aMonsterType;

            mMap = aMap;
            mBoundX = aBoundX; mBoundY = aBoundY; mBoundCX = aBoundCX; mBoundCY = aBoundCY;
            mGrid = aMaxNPC; mRestSecs = aRestSecs; mMaxPerGen = aMaxPerGen;
            mAmount = 0; mGenAmount = 0; mCurGen = 0;
            mMaxNPC = 0; mIdxLastGen = 0;

            if (mGrid < 1)
                mMaxNPC = 1;
            else
                mMaxNPC = (mBoundCX / mGrid) * (mBoundCY / mGrid);

            if (mMaxNPC < 1)
                mMaxNPC = 1;

            mIdxLastGen = sRand.Next(mMaxNPC);

            mTimer = new Timer();
            mTimer.AutoReset = false;
            mTimer.Elapsed += new ElapsedEventHandler(OnTimer_Elapsed);
            mTimer.Interval = (mRestSecs + sRand.Next(mRestSecs)) * 1000;
            mTimer.Start();
        }

        ~Generator()
        {
            Dispose(false);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing,
        /// or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing,
        /// or resetting unmanaged resources.
        /// </summary>
        protected virtual void Dispose(bool aDisposing)
        {
            if (!mIsDisposed)
            {
                if (aDisposing)
                {
                    if (mTimer != null)
                        mTimer.Dispose();
                }

                mTimer = null;

                mIsDisposed = true;
            }
        }

        private void OnTimer_Elapsed(object aSender, ElapsedEventArgs e)
        {
            mTimerElapsed = true;
        }

        /// <summary>
        /// Find an available position inside the valid range.
        /// </summary>
        private void FindGenPos(ref UInt16 aOutPosX, ref UInt16 aOutPosY)
        {
            aOutPosX = mBoundX;
            aOutPosY = mBoundY;
            if (mMaxNPC <= 1)
            {
                aOutPosX += (UInt16)sRand.Next(mBoundCX);
                aOutPosY += (UInt16)sRand.Next(mBoundCY);
            }
            else
            {
                ++mIdxLastGen;
                if (mIdxLastGen >= mMaxNPC)
                    mIdxLastGen = 0;

                int gridX = mBoundCX / mGrid;
                if (gridX < 0)
                    gridX = 1;

                aOutPosX += (UInt16)(mGrid * (mIdxLastGen % gridX) + sRand.Next(mGrid));
                aOutPosY += (UInt16)(mGrid * (mIdxLastGen / gridX) + sRand.Next(mGrid));
            }
        }

        /// <summary>
        /// Try to generate aAmount monsters.
        /// </summary>
        /// <param name="aAmount">The amount of monsters to generate</param>
        /// <returns>The number of generated monsters</returns>
        public UInt32 Generate(UInt32 aAmount)
        {
            const UInt32 RANDOM_GENERATOR_SECS = 600;

            if (mGenAmount >= mMaxNPC)
                return 0;

            if (mTimerElapsed)
            {
                if (mRestSecs >= RANDOM_GENERATOR_SECS)
                {
                    mTimer.Interval = (mRestSecs + sRand.Next(mRestSecs)) * 1000;
                    mTimer.Start();
                }

                mCurGen = mMaxPerGen;
            }

            if (mCurGen <= 0)
                return 0;

            UInt32 generated = 0;
            for (UInt32 i = 0; i < aAmount; ++i)
            {
                UInt16 posX = 0, posY = 0;
                FindGenPos(ref posX, ref posY);

                if (!mMap.GetFloorAccess(posX, posY))
                {
                    posX = (UInt16)(mBoundX + sRand.Next(mBoundCX));
                    posY = (UInt16)(mBoundY + sRand.Next(mBoundCY));

                    if (!mMap.GetFloorAccess(posX, posY))
                        continue;
                }

                Monster monster = null;
                lock (World.AllMonsters)
                {
                    // TODO protection for infinite loop
                    while (World.AllMonsters.ContainsKey(World.LastMonsterUID))
                    {
                        ++World.LastMonsterUID;
                        if (!Entity.IsMonster(World.LastMonsterUID))
                            World.LastMonsterUID = Entity.MONSTERID_FIRST;
                    }


                    monster = new Monster(World.LastMonsterUID++, mMonsterType, this);
                    ++mAmount;

                    monster.Map = mMap;
                    monster.StartX = posX;
                    monster.StartY = posY;
                    monster.X = posX;
                    monster.Y = posY;

                    World.AllMonsters.Add(monster.UniqId, monster);
                    mMap.AddEntity(monster);
                }

                World.BroadcastRoomMsg(monster, new MsgPlayer(monster));
                World.BroadcastRoomMsg(monster, new MsgAction(monster, 0, MsgAction.Action.Reborn));
                World.BroadcastRoomMsg(monster, new MsgName(monster.UniqId, "MBStandard", MsgName.NameAct.RoleEffect));

                --mCurGen;
                ++mGenAmount;
                ++generated;
                if (mCurGen <= 0)
                    break;
            }

            return generated;
        }

        [Obsolete]
        public static void generatePet(Player aOwner, Int32 aPetType)
        {
            MonsterInfo petInfo;
            if (!Database.AllMonsters.TryGetValue(aPetType, out petInfo))
                return;

            if (aOwner.Pet != null)
            {
                aOwner.Pet.Brain.Sleep();
                aOwner.Pet.Disappear();
                aOwner.Pet = null;
            }

            Pet pet = new Pet(++World.LastMonsterUID, petInfo, aOwner);

            pet.Map = aOwner.Map;
            pet.StartX = aOwner.X;
            pet.StartY = aOwner.Y;
            pet.X = aOwner.X;
            pet.Y = aOwner.Y;

            World.AllMonsters.Add(pet.UniqId, pet);
            pet.Map.AddEntity(pet);

            aOwner.Pet = pet;
            pet.Reborn();

            World.BroadcastRoomMsg(pet, new MsgPetInfo(pet));
        }
    }
}
