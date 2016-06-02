// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2014 - 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;

namespace COServer.Security.Cryptography
{
    /// <summary>
    /// RC5 is a symmetric-key block cipher notable for its simplicity. Designed by Ronald Rivest in 1994.
    /// 
    /// Unlike many schemes, RC5 has a variable block size (32, 64 or 128 bits), key size (0 to 2040 bits)
    /// and number of rounds (0 to 255). The original suggested choice of parameters were a block size of 64 bits,
    /// a 128-bit key and 12 rounds.
    ///
    /// A key feature of RC5 is the use of data-dependent rotations. RC5 also consists of a number of modular additions
    /// and eXclusive OR (XOR)s. The general structure of the algorithm is a Feistel-like network.
    /// </summary>
    public class RivestCipher5
    {
        public static UInt32 RC5_PW32 = 0xB7E15163;
        public static UInt32 RC5_QW32 = 0x61C88647;

        private const Int32 RC5_32 = 32;
        private const Int32 RC5_12 = 12;
        private const Int32 RC5_SUB = (RC5_12 * 2 + 2);
        private const Int32 RC5_16 = 16;
        private const Int32 RC5_KEY = (RC5_16 / 4);

        private UInt32[] mKey = new UInt32[RC5_KEY];
        private UInt32[] mSub = new UInt32[RC5_SUB];

        /// <summary>
        /// Generates a random key (Key) to use for the algorithm.
        /// CO2: { 0x3C, 0xDC, 0xFE, 0xE8, 0xC4, 0x54, 0xD6, 0x7E, 0x16, 0xA6, 0xF8, 0x1A, 0xE8, 0xD0, 0x38, 0xBE }
        /// </summary>
        public unsafe RivestCipher5(Byte[] aSeed)
        {
            if (aSeed.Length != RC5_16)
                throw new ArgumentException("Length of the seed must be exactly " + RC5_16 + " bytes.", "aSeed");

            fixed (Byte* seed = aSeed)
            {
                for (Int32 z = 0; z < RC5_KEY; ++z)
                    mKey[z] = ((UInt32*)seed)[z];
            }

            mSub[0] = RC5_PW32;
            Int32 i, j;
            for (i = 1; i < RC5_SUB; ++i)
                mSub[i] = mSub[i - 1] - RC5_QW32;

            UInt32 x, y;
            i = j = 0;
            x = y = 0;
            for (Int32 k = 0, count = 3 * Math.Max(RC5_KEY, RC5_SUB); k < count; ++k)
            {
                mSub[i] = rotl((mSub[i] + x + y), 3);
                x = mSub[i];
                i = (i + 1) % RC5_SUB;
                mKey[j] = rotl((mKey[j] + x + y), (x + y));
                y = mKey[j];
                j = (j + 1) % RC5_KEY;
            }
        }

        /// <summary>
        /// Encrypts data with the RC5 algorithm.
        /// </summary>
        public unsafe void Encrypt(ref Byte[] aBuf, Int32 aLength)
        {
            if (aLength % 8 != 0)
                throw new ArgumentException("Length of the buffer must be a multiple of 64 bits.", "aLength");

            fixed (Byte* buf = aBuf)
            {
                UInt32* data = (UInt32*)buf;
                for (Int32 k = 0, len = aLength / 8; k < len; ++k)
                {
                    UInt32 lv = data[2 * k] + mSub[0];
                    UInt32 rv = data[2 * k + 1] + mSub[1];
                    for (Int32 i = 1; i <= RC5_12; ++i)
                    {
                        lv = rotl((lv ^ rv), rv) + mSub[2 * i];
                        rv = rotl((rv ^ lv), lv) + mSub[2 * i + 1];
                    }

                    data[2 * k] = lv;
                    data[2 * k + 1] = rv;
                }
            }
        }

        /// <summary>
        /// Decrypts data with the RC5 algorithm.
        /// </summary>
        public unsafe void Decrypt(ref Byte[] aBuf, Int32 aLength)
        {
            if (aLength % 8 != 0)
                throw new ArgumentException("Length of the buffer must be a multiple of 64 bits.", "aLength");

            fixed (Byte* buf = aBuf)
            {
                UInt32* data = (UInt32*)buf;
                for (Int32 k = 0, len = aLength / 8; k < len; ++k)
                {
                    UInt32 lv = data[2 * k];
                    UInt32 rv = data[2 * k + 1];
                    for (Int32 i = RC5_12; i >= 1; --i)
                    {
                        rv = rotr((rv - mSub[2 * i + 1]), lv) ^ lv;
                        lv = rotr((lv - mSub[2 * i]), rv) ^ rv;
                    }

                    data[2 * k] = lv - mSub[0];
                    data[2 * k + 1] = rv - mSub[1];
                }
            }
        }

        private UInt32 rotl(UInt32 aValue, UInt32 aCount)
        {
            Int32 leftShift = (Int32)(aCount % 32);
            Int32 rightShift = 32 - leftShift;

            return (aValue << leftShift) | (aValue >> rightShift);
        }

        private UInt32 rotr(UInt32 aValue, UInt32 aCount)
        {
            Int32 rightShift = (Int32)(aCount % 32);
            Int32 leftShift = 32 - rightShift;

            return (aValue >> rightShift) | (aValue << leftShift);
        }
    }
}