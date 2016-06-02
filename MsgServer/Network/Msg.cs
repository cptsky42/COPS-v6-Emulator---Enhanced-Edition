// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2010 - 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: InternalsVisibleTo("StringPacker")]

namespace COServer.Network
{
    /// <summary>
    /// This class represent a parsed message. It is the base of all message sent
    /// between the client and the server.
    /// </summary>
    public abstract partial class Msg
    {
        /// <summary>
        /// The logger of the class.
        /// </summary>
        protected static readonly log4net.ILog sLogger = log4net.LogManager.GetLogger(typeof(Msg));

        /// <summary>
        /// The minimum size of a message (to have at least an header).
        /// </summary>
        public const int MIN_SIZE = 4;

        /// <summary>
        /// The length of the message.
        /// </summary>
        public UInt16 Length { get { return (UInt16)((mBuf[0x01] << 8) + mBuf[0x00]); } }
        /// <summary>
        /// The type of the message.
        /// </summary>
        public UInt16 Type { get { return (UInt16)((mBuf[0x03] << 8) + mBuf[0x02]); } }

        /// <summary>
        /// This is a "constant" that the child must override.
        /// It is the type of the message as specified in NetworkDef.cs file.
        /// </summary>
        protected abstract UInt16 _TYPE { get; }

        /// <summary>
        /// The internal buffer containing the data of the message.
        /// </summary>
        internal Byte[] mBuf = null;

        /// <summary>
        /// Create a new message of the specified length.
        /// 
        /// The header of the message will be written using
        /// the specified length and the _TYPE property.
        /// </summary>
        /// <param name="aLength">The length of the message.</param>
        protected Msg(UInt16 aLength)
        {
            mBuf = new Byte[aLength];

            WriteUInt16(0, aLength);
            WriteUInt16(2, _TYPE);
        }

        /// <summary>
        /// Create a new message using the specified part of the buffer.
        /// A new internal buffer will be created with a copy of the content.
        /// </summary>
        /// <param name="aBuf">The buffer containing the message.</param>
        /// <param name="aIndex">The index where the message is starting in the buffer.</param>
        /// <param name="aLength">The length of the message.</param>
        protected Msg(Byte[] aBuf, int aIndex, int aLength)
        {
            mBuf = new Byte[aLength];
            Buffer.BlockCopy(aBuf, aIndex, mBuf, 0, aLength);
        }

        /// <summary>
        /// Write a 16 bits signed integer at the specified offset
        /// in the buffer.
        /// </summary>
        /// <param name="aOffset">The offset where to start writing.</param>
        /// <param name="aValue">The value to write.</param>
        protected void WriteInt16(int aOffset, Int16 aValue)
        {
            mBuf[aOffset] = (Byte)((aValue) & 0xFF);
            mBuf[aOffset + 1] = (Byte)((aValue >> 8) & 0xFF);
        }

        /// <summary>
        /// Write a 16 bits unsigned integer at the specified offset
        /// in the buffer.
        /// </summary>
        /// <param name="aOffset">The offset where to start writing.</param>
        /// <param name="aValue">The value to write.</param>
        protected void WriteUInt16(int aOffset, UInt16 aValue)
        {
            mBuf[aOffset] = (Byte)((aValue) & 0xFF);
            mBuf[aOffset + 1] = (Byte)((aValue >> 8) & 0xFF);
        }

        /// <summary>
        /// Write a 32 bits signed integer at the specified offset
        /// in the buffer.
        /// </summary>
        /// <param name="aOffset">The offset where to start writing.</param>
        /// <param name="aValue">The value to write.</param>
        protected void WriteInt32(int aOffset, Int32 aValue)
        {
            mBuf[aOffset] = (Byte)((aValue) & 0xFF);
            mBuf[aOffset + 1] = (Byte)((aValue >> 8) & 0xFF);
            mBuf[aOffset + 2] = (Byte)((aValue >> 16) & 0xFF);
            mBuf[aOffset + 3] = (Byte)((aValue >> 24) & 0xFF);
        }

        /// <summary>
        /// Write a 32 bits unsigned integer at the specified offset
        /// in the buffer.
        /// </summary>
        /// <param name="aOffset">The offset where to start writing.</param>
        /// <param name="aValue">The value to write.</param>
        protected void WriteUInt32(int aOffset, UInt32 aValue)
        {
            mBuf[aOffset] = (Byte)((aValue) & 0xFF);
            mBuf[aOffset + 1] = (Byte)((aValue >> 8) & 0xFF);
            mBuf[aOffset + 2] = (Byte)((aValue >> 16) & 0xFF);
            mBuf[aOffset + 3] = (Byte)((aValue >> 24) & 0xFF);
        }

        /// <summary>
        /// Write a 64 bits signed integer at the specified offset
        /// in the buffer.
        /// </summary>
        /// <param name="aOffset">The offset where to start writing.</param>
        /// <param name="aValue">The value to write.</param>
        protected void WriteInt64(int aOffset, Int64 aValue)
        {
            mBuf[aOffset] = (Byte)((aValue) & 0xFF);
            mBuf[aOffset + 1] = (Byte)((aValue >> 8) & 0xFF);
            mBuf[aOffset + 2] = (Byte)((aValue >> 16) & 0xFF);
            mBuf[aOffset + 3] = (Byte)((aValue >> 24) & 0xFF);
        }

        /// <summary>
        /// Write a 64 bits unsigned integer at the specified offset
        /// in the buffer.
        /// </summary>
        /// <param name="aOffset">The offset where to start writing.</param>
        /// <param name="aValue">The value to write.</param>
        protected void WriteUInt64(int aOffset, UInt64 aValue)
        {
            mBuf[aOffset] = (Byte)((aValue) & 0xFF);
            mBuf[aOffset + 1] = (Byte)((aValue >> 8) & 0xFF);
            mBuf[aOffset + 2] = (Byte)((aValue >> 16) & 0xFF);
            mBuf[aOffset + 3] = (Byte)((aValue >> 24) & 0xFF);
            mBuf[aOffset + 4] = (Byte)((aValue >> 32) & 0xFF);
            mBuf[aOffset + 5] = (Byte)((aValue >> 40) & 0xFF);
            mBuf[aOffset + 6] = (Byte)((aValue >> 48) & 0xFF);
            mBuf[aOffset + 7] = (Byte)((aValue >> 56) & 0xFF);
        }

        /// <summary>
        /// Write a string at the specified offset in the buffer.
        /// </summary>
        /// <param name="aOffset">The offset where to start writing.</param>
        /// <param name="aValue">The value to write.</param>
        /// <param name="aMaxLength">The maximum length of the string.</param>
        protected void WriteString(int aOffset, String aValue, int aMaxLength)
        {
            Byte[] value = Program.Encoding.GetBytes(aValue);
            if (value.Length > aMaxLength)
                throw new ArgumentOutOfRangeException(
                    "aValue", aValue, "Length of the parameter is greater than the imposed limit (" + aMaxLength + ").");

            int i = aOffset;
            for (int j = 0, n = aOffset + value.Length; i < n; ++i, ++j)
                mBuf[i] = value[j];

            for (int n = aOffset + aMaxLength; i < n; ++i)
                mBuf[i] = (Byte)'\0';
        }

        /// <summary>
        /// Process the message for the specified client.
        /// </summary>
        /// <param name="aClient">The client who sent the message.</param>
        public virtual void Process(Client aClient)
        {
            Console.WriteLine("Msg[{0}]::Process() not implemented yet!", Type);
        }

        /// <summary>
        /// Explicit conversion operator for Msg to Byte[].
        /// </summary>
        public static explicit operator Byte[](Msg aMsg)
        {
            Byte[] msg = new Byte[aMsg.mBuf.Length];
            Buffer.BlockCopy(aMsg.mBuf, 0, msg, 0, aMsg.mBuf.Length);

            return msg;
        }

        /// <summary>
        /// Create a Msg object using a part of the specified buffer.
        /// </summary>
        /// <param name="aBuf">The buffer containing the message.</param>
        /// <param name="aIndex">The index where the message is starting in the buffer.</param>
        /// <param name="aLength">The length of the message.</param>
        /// <returns>A Msg object representing the parsed data.</returns>
        public static Msg Create(Byte[] aBuf, int aIndex, int aLength)
        {
            UInt16 type = (UInt16)((aBuf[aIndex + 0x03] << 8) + aBuf[aIndex + 0x02]);

            switch (type)
            {
                case MSG_REGISTER:
                    return new MsgRegister(aBuf, aIndex, aLength);
                case MSG_TALK:
                    return new MsgTalk(aBuf, aIndex, aLength);
                case MSG_WALK:
                    return new MsgWalk(aBuf, aIndex, aLength);
                case MSG_ITEM:
                    return new MsgItem(aBuf, aIndex, aLength);
                case MSG_ACTION:
                    return new MsgAction(aBuf, aIndex, aLength);
                case MSG_TICK:
                    return new MsgTick(aBuf, aIndex, aLength);
                case MSG_NAME:
                    return new MsgName(aBuf, aIndex, aLength);
                case MSG_FRIEND:
                    return new MsgFriend(aBuf, aIndex, aLength);
                case MSG_GEMEMBED:
                    return new MsgGemEmbed(aBuf, aIndex, aLength);
                case MSG_INTERACT:
                    return new MsgInteract(aBuf, aIndex, aLength);
                case MSG_TEAM:
                    return new MsgTeam(aBuf, aIndex, aLength);
                case MSG_ALLOT:
                    return new MsgAllot(aBuf, aIndex, aLength);
                case MSG_CONNECT:
                    return new MsgConnect(aBuf, aIndex, aLength);
                case MSG_TRADE:
                    return new MsgTrade(aBuf, aIndex, aLength);
                case MSG_MAPITEM:
                    return new MsgMapItem(aBuf, aIndex, aLength);
                case MSG_PACKAGE:
                    return new MsgPackage(aBuf, aIndex, aLength);
                case MSG_SYNDICATE:
                    return new MsgSyndicate(aBuf, aIndex, aLength);
                case MSG_MESSAGEBOARD:
                    return new MsgMessageBoard(aBuf, aIndex, aLength);
                case MSG_SYNMEMBERINFO:
                    return new MsgSynMemberInfo(aBuf, aIndex, aLength);
                case MSG_NPC:
                    return new MsgNpc(aBuf, aIndex, aLength);
                case MSG_DIALOG:
                    return new MsgDialog(aBuf, aIndex, aLength);
                case MSG_DATAARRAY:
                    return new MsgDataArray(aBuf, aIndex, aLength);
                default:
                    {
                        sLogger.Debug("Msg[{0}] not implemented yet!", type);
                        break;
                    }
            }

            return null;
        }
    }
}
