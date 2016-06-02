// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2010 - 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Msg")]

namespace COServer.Network
{
    /// <summary>
    /// Message sent by the server to answer to a connection request.
    /// </summary>
    public class MsgConnectEx : Msg
    {
        /// <summary>
        /// This is a "constant" that the child must override.
        /// It is the type of the message as specified in NetworkDef.cs file.
        /// </summary>
        protected override UInt16 _TYPE { get { return MSG_CONNECTEX; } }

        /// <summary>
        /// List of responses that can be returned by the server.
        /// </summary>
        public enum ErrorId : uint
        {
            /// <summary>
            /// The account or the password is invalid.
            /// </summary>
            InvalidPassword = 1, // 10418
            /// <summary>
            /// The game server is down. It cannot be contacted.
            /// </summary>
            ServerDown = 10, // 10417
            /// <summary>
            /// The login request must be tried again later.
            /// The request cannot be fulfilled at the moment.
            /// </summary>
            TryLater = 11, // 10419
            /// <summary>
            /// The account or the IP address is banned.
            /// </summary>
            Banned = 12, // 10438
            /// <summary>
            /// The game server is busy.
            /// </summary>
            ServerBusy = 20, // 10480
            /// <summary>
            /// The game server is full.
            /// </summary>
            ServerFull = 21, // 10480
            /// <summary>
            /// The account cannot be created because it already exist.
            /// </summary>
            AccountExist = 22, // 10440 (COPS v6 only)
            /// <summary>
            /// The account is locked.
            /// </summary>
            AccountLocked = 22, // 10440
            /// <summary>
            /// ???
            /// </summary>
            UnknownError = 999 // 10439
        }

        /// <summary>
        /// Mapping of the ErrorId used by the newer clients and the Info string used by
        /// the older clients.
        /// </summary>
        private static Dictionary<ErrorId, String> ERROR_ID_TO_STRINGS = new Dictionary<ErrorId, String>()
        {
            { ErrorId.InvalidPassword, "\xD5\xCA\xBA\xC5\xC3\xFB\xBB\xF2\xBF\xDA\xC1\xEE\xB4\xED" },
            { ErrorId.ServerDown, "\xB7\xFE\xCE\xF1\xC6\xF7\xCE\xB4\xC6\xF4\xB6\xAF" },
            { ErrorId.TryLater, "\xC7\xEB\xC9\xD4\xBA\xF3\xD6\xD8\xD0\xC2\xB5\xC7\xC2\xBC" },
            { ErrorId.Banned, "\xB8\xC3\xD5\xCA\xBA\xC5\xB1\xBB\xB7\xE2\xBA\xC5" },
            { ErrorId.ServerBusy, "\xB7\xFE\xCE\xF1\xC6\xF7\xC8\xCB\xCA\xFD\xD2\xD1\xC2\xFA" },
            { ErrorId.ServerFull, "\xB7\xFE\xCE\xF1\xC6\xF7\xC3\xA6\xC7\xEB\xC9\xD4\xBA\xF2" },
            { ErrorId.AccountLocked, "\xB8\xC3\xD5\xCA\xBA\xC5\xCB\xF8\xB6\xA8\xD6\xD0" },
        };

        //--------------- Internal Members ---------------
        private UInt32 __AccountUID = 0;
        private UInt32 __Data = 0;
        private String __Info = "";
        private UInt32 __Port = 0;
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
        /// Token / session ID of the connection  or the ID of the error.
        /// </summary>
        public UInt32 Data
        {
            get { return __Data; }
            set { __Data = value; WriteUInt32(8, value); }
        }

        /// <summary>
        /// The information (IP address) of the MsgServer or the error string.
        /// </summary>
        public String Info
        {
            get { return __Info; }
            set { __Info = value; WriteString(12, value, MAX_NAME_SIZE); }
        }

        /// <summary>
        /// The port of the MsgServer.
        /// </summary>
        public UInt16 Port
        {
            get { return (UInt16)__Port; }
            set { __Data = value; WriteUInt32(28, value); }
        }

        /// <summary>
        /// Create a new message with the session informations.
        /// </summary>
        /// <param name="aAccountUID">The unique ID of the account.</param>
        /// <param name="aData">The session ID.</param>
        /// <param name="aInfo">The game server IP address.</param>
        /// <param name="aPort">The game server port.</param>
        public MsgConnectEx(UInt32 aAccountUID, UInt32 aData, String aInfo, UInt16 aPort)
            : base(32)
        {
            AccountUID = aAccountUID;
            Data = aData;
            Info = aInfo;
            Port = aPort;
        }

        /// <summary>
        /// Create a new message with an error as the message.
        /// </summary>
        /// <param name="aErrorId">The error to signal to the client.</param>
        public MsgConnectEx(ErrorId aErrorId)
            : this(0, (UInt32)aErrorId, ERROR_ID_TO_STRINGS[aErrorId], 0)
        {
            
        }
    }
}
