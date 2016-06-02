// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2010 - 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Msg")]

namespace COServer.Network
{
    /// <summary>
    /// Msg sent to answer to a connection request.
    /// </summary>
    public class MsgConnect : Msg
    {
        /// <summary>
        /// This is a "constant" that the child must override.
        /// It is the type of the message as specified in NetworkDef.cs file.
        /// </summary>
        protected override UInt16 _TYPE { get { return MSG_CONNECT; } }

        //--------------- Internal Members ---------------
        private UInt32 __AccountUID = 0;
        private UInt32 __Data = 0;
        private String __Info = "";
        //------------------------------------------------

        /// <summary>
        /// Unique ID of the account.
        /// </summary>
        public UInt32 AccountUID
        {
            get { return __AccountUID; }
            set { __AccountUID = value; WriteUInt32(4, value); }
        }

        /// <summary>
        /// Token / session ID of the connection.
        /// </summary>
        public UInt32 Data
        {
            get { return __Data; }
            set { __Data = value; WriteUInt32(8, value); }
        }

        /// <summary>
        /// Information.
        /// </summary>
        public String Info
        {
            get { return __Info; }
            set { __Info = value; WriteString(12, value, MAX_NAME_SIZE); }
        }

        /// <summary>
        /// Create a message object from the specified buffer.
        /// </summary>
        /// <param name="aBuf">The buffer containing the message.</param>
        /// <param name="aIndex">The index where the message is starting in the buffer.</param>
        /// <param name="aLength">The length of the message.</param>
        internal MsgConnect(Byte[] aBuf, int aIndex, int aLength)
            : base(aBuf, aIndex, aLength)
        {
            __AccountUID = BitConverter.ToUInt32(mBuf, 4);
            __Data = BitConverter.ToUInt32(mBuf, 8);
            __Info = Program.Encoding.GetString(mBuf, 12, MAX_NAME_SIZE).Trim('\0');
        }

        public MsgConnect(UInt32 aAccountUID, UInt32 aData, String aInfo)
            : base(28)
        {
            AccountUID = aAccountUID;
            Data = aData;
            Info = aInfo;
        }

        /// <summary>
        /// Process the message for the specified client.
        /// </summary>
        /// <param name="aClient">The client who sent the message.</param>
        public override void Process(Client aClient)
        {
            // Nothing to do...
        }
    }
}
