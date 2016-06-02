// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;

namespace COServer.Network
{
    /// <summary>
    /// String packer for an internal buffer. All strings are prefixed with
    /// their length in an unsigned 8-bit integer.
    /// 
    /// When extracting string, the first string is at the index 0.
    /// </summary>
    public class StringPacker
    {
        /// <summary>
        /// The Msg object owning the packer.
        /// </summary>
        private Msg mMsg;
        /// <summary>
        /// The position where the strings start.
        /// </summary>
        private UInt16 mPos;
        /// <summary>
        /// The number of strings in the buffer.
        /// </summary>
        private Byte mStrCount;

        /// <summary>
        /// Create a new packer around the specified buffer.
        /// </summary>
        /// <param name="aMsg">The Msg object owning the packer.</param>
        /// <param name="aPosition">The position where the strings start</param>
        public StringPacker(Msg aMsg, UInt16 aPosition)
        {
            mMsg = aMsg;
            mPos = aPosition;
            mStrCount = aMsg.mBuf[mPos];
        }

        /// <summary>
        /// Append a string at the end of the buffer.
        /// </summary>
        /// <param name="aStr">The string to append at the end of the buffer</param>
        public void AddString(String aStr)
        {
            Byte[] str = Program.Encoding.GetBytes(aStr);
            if (str.Length > Byte.MaxValue)
                throw new ArgumentOutOfRangeException(
                    "aStr", aStr, "Length of the parameter is greater than the imposed limit (" + Byte.MaxValue + ").");

            int pos = mPos + 1;
            for (int i = 0; i < mStrCount; ++i)
            {
                Byte len = mMsg.mBuf[pos++];
                pos += len;
            }

            mMsg.mBuf[pos++] = (Byte)str.Length;
            Buffer.BlockCopy(str, 0, mMsg.mBuf, pos, str.Length);

            mMsg.mBuf[mPos] = ++mStrCount;
        }

        /// <summary>
        /// Extract a string from the buffer.
        /// </summary>
        /// <param name="aOutStr">A reference to the string object that will receive the string</param>
        /// <param name="aIndex">The index of the string to retreive (0 is the first string)</param>
        /// <returns>True on success, false otherwise.</returns>
        public bool GetString(out String aOutStr, Byte aIndex)
        {
            bool success = false;
            aOutStr = null;

            if (aIndex < mStrCount)
            {
                int pos = mPos + 1;
                for (int i = 0; i < aIndex; ++i)
                {
                    Byte len = mMsg.mBuf[pos++];
                    pos += len;
                }

                int strlen = mMsg.mBuf[pos++];
                aOutStr = Program.Encoding.GetString(mMsg.mBuf, pos, strlen);
                success = true;
            }

            return success;
        }

        /// <summary>
        /// Explicit conversion operator for StringPacker to String[].
        /// </summary>
        public static explicit operator String[](StringPacker aStrPacker)
        {
            String[] strings = new String[aStrPacker.mStrCount];
            for (byte i = 0; i < aStrPacker.mStrCount; ++i)
                aStrPacker.GetString(out strings[i], i);

            return strings;
        }
    }
}
