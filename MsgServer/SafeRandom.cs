// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;

namespace COServer
{
    /// <summary>
    /// Represents a pseudo-random number generator, which is a device that produces a sequence of numbers
    /// that meet certain statistical requirements for randomness.
    /// 
    /// This class is thread-safe.
    /// </summary>
    public class SafeRandom
    {
        /// <summary>
        /// Lock for thread-safeness.
        /// </summary>
        private object mLock = new object();

        /// <summary>
        /// Seed of the pseudo-random number generator.
        /// </summary>
        private Int64 mSeed;

        /// <summary>
        /// Initializes a new instance using a time-dependant default seed value.
        /// </summary>
        public SafeRandom()
        {
            // in the original source, the seed is initialized to 3721 instead of random...
            mSeed = Environment.TickCount;
        }

        /// <summary>
        /// Initializes a new instance using the specified seed value.
        /// </summary>
        public SafeRandom(Int32 aSeed)
        {
            mSeed = aSeed;
        }

        /// <summary>
        /// Returns a nonnegative random number less than the specified maximum.
        /// </summary>
        public int Next(int aMax)
        { 
            lock (mLock)
            {
                mSeed *= 134775813;
                mSeed += 1;
                mSeed = mSeed % UInt32.MaxValue;

                double i = ((double)mSeed) / (double)UInt32.MaxValue;
                return (int)(aMax * i);
            } 
        }

        /// <summary>
        /// Returns a random number within a specified range.
        /// </summary>
        public int Next(int aMin, int aMax)
        {
            return aMin + (int)(((double)Next(aMax) / (double)aMax) * (double)(aMax - aMin));
        }
    }
}