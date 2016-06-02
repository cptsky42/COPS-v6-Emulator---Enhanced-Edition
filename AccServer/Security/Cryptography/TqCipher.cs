// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2011 - 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;

namespace COServer.Security.Cryptography
{
    /// <summary>
    /// TQ Digital's Asymmetric Cipher (used on the servers).
    /// </summary>
    public class TqCipher
    {
        /// <summary>
        /// Integer constant used to generate the initialization vector.
        /// </summary>
        public static UInt32 P = 0x13FA0F9D;
        /// <summary>
        /// Integer constant used to generate the initialization vector.
        /// </summary>
        public static UInt32 G = 0x6D5C7962;

        /// <summary>
        /// The key size in bytes.
        /// </summary>
        private const int KEY_SIZE = 512;

        /// <summary>
        /// The initial key.
        /// </summary>
        private readonly Byte[] mIV = new Byte[KEY_SIZE];
        /// <summary>
        /// The alternate key (to be used for decryption).
        /// </summary>
        private readonly Byte[] mAltKey = new Byte[KEY_SIZE];
        /// <summary>
        /// Whether or not the alternate key is used for decryption.
        /// </summary>
        private bool mUsingAltKey = false;

        /// <summary>
        /// The encryption counter.
        /// </summary>
        private UInt16 mEnCounter = 0;
        /// <summary>
        /// The decryption counter.
        /// </summary>
        private UInt16 mDeCounter = 0;

        /// <summary>
        /// Create a new cipher instance. The key will be generated
        /// using the P and G constants.
        /// </summary>
        public unsafe TqCipher()
        {
            const int K = KEY_SIZE / 2;

            fixed (UInt32* _p = &P, _g = &G)
            {
                Byte* p = (Byte*)_p;
                Byte* g = (Byte*)_g;

                for (int i = 0; i < K; ++i)
                {
                    mIV[i + 0] = p[0];
                    mIV[i + K] = g[0];
                    p[0] = (Byte)((p[1] + (Byte)(p[0] * p[2])) * p[0] + p[3]);
                    g[0] = (Byte)((g[1] - (Byte)(g[0] * g[2])) * g[0] + g[3]);
                }
            }
        }

        /// <summary>
        /// Generates an alternate key to use for the algorithm and reset
        /// the encryption counter.
        /// 
        /// In Conquer Online: A = Token, B = AccountUID
        /// </summary>
        public unsafe void GenerateAltKey(Int32 A, Int32 B)
        {
            const int K = KEY_SIZE / 2;

            UInt32 tmp1 = (UInt32)(((A + B) ^ 0x4321) ^ A);
            UInt32 tmp2 = tmp1 * tmp1;

            Byte* tmpKey1 = (Byte*)&tmp1;
            Byte* tmpKey2 = (Byte*)&tmp2;
            for (int i = 0; i < K; ++i)
            {
                mAltKey[i + 0] = (Byte)(mIV[i + 0] ^ tmpKey1[(i % 4)]);
                mAltKey[i + K] = (Byte)(mIV[i + K] ^ tmpKey2[(i % 4)]);
            }
            mUsingAltKey = true;
            mEnCounter = 0;
        }

        /// <summary>
        /// Encrypts data with the algorithm.
        /// </summary>
        public void Encrypt(ref Byte[] aBuf, int aLength)
        {
            const int K = KEY_SIZE / 2;

            for (int i = 0; i < aLength; ++i)
            {
                aBuf[i] ^= (Byte)0xAB;
                aBuf[i] = (Byte)(aBuf[i] >> 4 | aBuf[i] << 4);
                aBuf[i] ^= (Byte)(mIV[(Byte)(mEnCounter & 0xFF) + 0]);
                aBuf[i] ^= (Byte)(mIV[(Byte)(mEnCounter >> 8) + K]);
                ++mEnCounter;
            }
        }

        /// <summary>
        /// Decrypts data with the algorithm.
        /// </summary>
        public void Decrypt(ref Byte[] aBuf, int aLength)
        {
            const int K = KEY_SIZE / 2;

            Byte[] key = mUsingAltKey ? mAltKey : mIV;
            for (int i = 0; i < aLength; ++i)
            {
                aBuf[i] ^= (Byte)0xAB;
                aBuf[i] = (Byte)(aBuf[i] >> 4 | aBuf[i] << 4);
                aBuf[i] ^= (Byte)(key[(Byte)(mDeCounter & 0xFF) + 0]);
                aBuf[i] ^= (Byte)(key[(Byte)(mDeCounter >> 8) + K]);
                ++mDeCounter;
            }
        }

        /// <summary>
        /// Resets the decrypt and the encrypt counters.
        /// </summary>
        public void ResetCounters() { mDeCounter = 0; mEnCounter = 0; }
    }
}