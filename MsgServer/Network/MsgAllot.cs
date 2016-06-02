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
    /// <summary>
    /// Message sent by the client to allocate points.
    /// </summary>
    public class MsgAllot : Msg
    {
        /// <summary>
        /// This is a "constant" that the child must override.
        /// It is the type of the message as specified in NetworkDef.cs file.
        /// </summary>
        protected override UInt16 _TYPE { get { return MSG_ALLOT; } }

        //--------------- Internal Members ---------------
        private Byte __Force = 0;
        private Byte __Dexterity = 0;
        private Byte __Health = 0;
        private Byte __Soul = 0;
        //------------------------------------------------

        /// <summary>
        /// Additionnal force to add.
        /// </summary>
        public Byte Force
        {
            get { return __Force; }
            set { __Force = value; mBuf[4] = value; }
        }

        /// <summary>
        /// Additionnal dexterity to add.
        /// </summary>
        public Byte Dexterity
        {
            get { return __Dexterity; }
            set { __Dexterity = value; mBuf[5] = value; }
        }

        /// <summary>
        /// Additionnal health to add.
        /// </summary>
        public Byte Health
        {
            get { return __Health; }
            set { __Health = value; mBuf[6] = value; }
        }

        /// <summary>
        /// Additionnal soul to add.
        /// </summary>
        public Byte Soul
        {
            get { return __Soul; }
            set { __Soul = value; mBuf[7] = value; }
        }

        /// <summary>
        /// Create a message object from the specified buffer.
        /// </summary>
        /// <param name="aBuf">The buffer containing the message.</param>
        /// <param name="aIndex">The index where the message is starting in the buffer.</param>
        /// <param name="aLength">The length of the message.</param>
        internal MsgAllot(Byte[] aBuf, int aIndex, int aLength)
            : base(aBuf, aIndex, aLength)
        {
            __Force = mBuf[4];
            __Dexterity = mBuf[5];
            __Health = mBuf[6];
            __Soul = mBuf[7];
        }

        /// <summary>
        /// Process the message for the specified client.
        /// </summary>
        /// <param name="aClient">The client who sent the message.</param>
        public override void Process(Client aClient)
        {
            Player player = aClient.Player;

            UInt16 points = (UInt16)(Force + Dexterity + Health + Soul);

            if (player.AddPoints < points)
            {
                Program.Log(String.Format("[CRIME] {0} ({1}) : ALLOT_CHEAT", player.Name, player.UniqId));
                player.SendSysMsg(StrRes.STR_ALLOT_CHEAT);
                return;
            }

            player.Strength += Force;
            player.Agility += Dexterity;
            player.Vitality += Health;
            player.Spirit += Soul;
            player.AddPoints -= points;

            player.CalcMaxHP();
            player.CalcMaxMP();
        }
    }
}
