// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2010 - 2015 Jean-Philippe Boivin
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
        private UInt16 __Constant = 0;
        private String __Language = "";
        private UInt32 __Version = 0;
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

        public UInt16 Constant
        {
            get { return __Constant; }
            set { __Constant = value; WriteUInt16(12, value); }
        }

        public String Language
        {
            get { return __Language; }
            set { __Language = value; WriteString(14, value, MAX_LANGUAGE_SIZE); }
        }

        public UInt32 Version
        {
            get { return __Version; }
            set { __Version = value; WriteUInt32(24, value); }
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

            // it is actually the Info field, but several data is in it...
            __Constant = BitConverter.ToUInt16(mBuf, 12);
            __Language = Program.Encoding.GetString(mBuf, 14, MAX_LANGUAGE_SIZE).Trim('\0');
            __Version = BitConverter.ToUInt32(mBuf, 24);
        }

        /// <summary>
        /// Process the message for the specified client.
        /// </summary>
        /// <param name="aClient">The client who sent the message.</param>
        public override void Process(Client aClient)
        {
            try
            {
                aClient.handleExchangeResponse(AccountUID, Data);

                if (!Database.Authenticate(aClient, AccountUID, Data))
                {
                    aClient.Send(new MsgTalk("SYSTEM", "ALLUSERS", "Token not found !", Channel.Entrance, Color.White));
                    return;
                }

                sLogger.Info("Connection of {0}, with {1}.", aClient.IPAddress, aClient.Account);

                if (!Database.GetPlayerInfo(ref aClient))
                    aClient.Send(new MsgTalk("SYSTEM", "ALLUSERS", "NEW_ROLE", Channel.Entrance, 0x000000));
                else
                {
                    if (World.AllPlayers.ContainsKey(aClient.Player.UniqId))
                        World.AllPlayers[aClient.Player.UniqId].Disconnect();

                    lock (World.AllPlayers) { World.AllPlayers.Add(aClient.Player.UniqId, aClient.Player); }
                    lock (World.AllPlayerNames) { World.AllPlayerNames.Add(aClient.Player.Name, aClient.Player); }

                    aClient.Send(new MsgTalk("SYSTEM", "ALLUSERS", "ANSWER_OK", Channel.Entrance, 0x000000));
                    aClient.Send(new MsgUserInfo(aClient.Player));

                    aClient.Player.ResetStatuses();
                    aClient.Send(new MsgUserAttrib(aClient.Player, aClient.Player.TimeAdd, MsgUserAttrib.AttributeType.TimeAdd));

                    aClient.Send(new MsgTalk("SYSTEM", aClient.Player.Name, StrRes.STR_LOGIN_INFORMATION, Channel.Normal, 0x000000));
                    #if _X64
                    aClient.Send(new MsgTalk("SYSTEM", aClient.Player.Name, "COPS v6 Emulator (64-bit) : v" + Program.Version, Channel.Normal, 0x000000));
                    #else
                    aClient.Send(new MsgTalk("SYSTEM", aClient.Player.Name, "COPS v6 Emulator : v" + Program.Version, Channel.Normal, 0x000000));
                    #endif
                    aClient.Send(new MsgTalk("SYSTEM", aClient.Player.Name, Environment.OSVersion.VersionString, Channel.Normal, 0x000000));
                    aClient.Send(new MsgTalk("SYSTEM", aClient.Player.Name, String.Format(StrRes.STR_SERVER_UPTIME, String.Format("{0:G}", (DateTime.Now - Server.LaunchTime))), Channel.Normal, 0x000000));

                    aClient.Player.SendSysMsg(StrRes.STR_SERVER_INFORMATION, World.AllPlayers.Count, Server.Name);
                }
            }
            catch (Exception exc) { sLogger.Error(exc); }
        }
    }
}
