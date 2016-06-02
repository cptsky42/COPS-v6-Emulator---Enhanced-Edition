// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2011 - 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.Runtime.CompilerServices;
using COServer.Entities;

[assembly: InternalsVisibleTo("COServer.Network.Msg")]

namespace COServer.Network
{
    /// <summary>
    /// Message used to validate the client connection.
    /// </summary>
    public class MsgTick : Msg
    {
        /// <summary>
        /// This is a "constant" that the child must override.
        /// It is the type of the message as specified in NetworkDef.cs file.
        /// </summary>
        protected override UInt16 _TYPE { get { return MSG_TICK; } }

        //--------------- Internal Members ---------------
        private Int32 __UniqId = 0;
        private Int32 __Timestamp = 0;
        private Int32[] __Junk = new Int32[4];
        private UInt32 __CheckData = 0;
        //------------------------------------------------

        /// <summary>
        /// Unique ID of the player.
        /// </summary>
        public Int32 UniqId
        {
            get { return __UniqId; }
            set { __UniqId = value; WriteInt32(4, value); }
        }

        /// <summary>
        /// Timestamp of the creation of the message.
        /// </summary>
        public Int32 Timestamp
        {
            get { return __Timestamp; }
            set { __Timestamp = value; WriteInt32(8, value); }
        }

        /// <summary>
        /// Random values.
        /// </summary>
        public Int32[] Junk
        {
            get { return __Junk; }
            set
            {
                __Junk = value;

                for (int i = 0; i < value.Length; ++i)
                    WriteInt32(12 + (i * 4), value[i]);
            }
        }

        /// <summary>
        /// Hash of the name.
        /// </summary>
        public UInt32 CheckData
        {
            get { return __CheckData; }
            set { __CheckData = value; WriteUInt32(28, value); }
        }

        /// <summary>
        /// Create a message object from the specified buffer.
        /// </summary>
        /// <param name="aBuf">The buffer containing the message.</param>
        /// <param name="aIndex">The index where the message is starting in the buffer.</param>
        /// <param name="aLength">The length of the message.</param>
        internal MsgTick(Byte[] aBuf, int aIndex, int aLength)
            : base(aBuf, aIndex, aLength)
        {
            __UniqId = BitConverter.ToInt32(mBuf, 4);
            __Timestamp = BitConverter.ToInt32(mBuf, 8);
            for (int i = 0; i < __Junk.Length; ++i)
                __Junk[i] = BitConverter.ToInt32(mBuf, 12 + (i * 4));
            __CheckData = BitConverter.ToUInt32(mBuf, 28);
        }

        public MsgTick(Player aPlayer)
            : base(32)
        {
            UniqId = aPlayer.UniqId;
            Timestamp = 0;
            Junk = new Int32[4]
            {
                MyMath.Generate(0x1FFFFFFF, 0x6FFFFFFF),
                MyMath.Generate(0x1FFFFFFF, 0x6FFFFFFF),
                MyMath.Generate(0x1FFFFFFF, 0x6FFFFFFF),
                MyMath.Generate(0x1FFFFFFF, 0x6FFFFFFF)
            };
            CheckData = 0;
        }

        /// <summary>
        /// Process the message for the specified client.
        /// </summary>
        /// <param name="aClient">The client who sent the message.</param>
        public override void Process(Client aClient)
        {
            if (aClient == null)
                return;

            Player player = aClient.Player;
            Int32 timestamp = Timestamp ^ UniqId;

            if (UniqId != player.UniqId)
            {
                player.Disconnect();
                return;
            }

            if (CheckData != HashName(player.Name))
            {
                player.Disconnect();
                return;
            }

            player.ProcessTick(timestamp, 1);
        }

        /// <summary>
        /// Compute the hash of the player's name.
        /// </summary>
        /// <param name="aName">The player's name.</param>
        /// <returns>The 32 bits hash of the player's name.</returns>
        private static UInt32 HashName(String aName)
        {
            if (String.IsNullOrEmpty(aName) || aName.Length < 4)
                return 0x9D4B5703;
            else
            {
                Byte[] name = Program.Encoding.GetBytes(aName);
                return BitConverter.ToUInt16(name, 0) ^ 0x9823U;
            }
        }
    }
}
