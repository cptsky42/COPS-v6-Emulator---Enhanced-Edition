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
    public class MsgSynMemberInfo : Msg
    {
        /// <summary>
        /// This is a "constant" that the child must override.
        /// It is the type of the message as specified in NetworkDef.cs file.
        /// </summary>
        protected override UInt16 _TYPE { get { return MSG_SYNMEMBERINFO; } }

        //--------------- Internal Members ---------------
        private UInt32 __Donation = 0;
        private Byte __Rank = 0;
        private String __Name = "";
        //------------------------------------------------

        public UInt32 Donation
        {
            get { return __Donation; }
            set { __Donation = value; WriteUInt32(4, value); }
        }

        public Byte Rank
        {
            get { return __Rank; }
            set { __Rank = value; mBuf[8] = value; }
        }

        public String Name
        {
            get { return __Name; }
            set { __Name = value; WriteString(9, value, MAX_NAME_SIZE); }
        }

        /// <summary>
        /// Create a message object from the specified buffer.
        /// </summary>
        /// <param name="aBuf">The buffer containing the message.</param>
        /// <param name="aIndex">The index where the message is starting in the buffer.</param>
        /// <param name="aLength">The length of the message.</param>
        internal MsgSynMemberInfo(Byte[] aBuf, int aIndex, int aLength)
            : base(aBuf, aIndex, aLength)
        {
            __Donation = BitConverter.ToUInt32(mBuf, 4);
            __Rank = mBuf[8];
            __Name = Program.Encoding.GetString(mBuf, 9, MAX_NAME_SIZE).TrimEnd('\0');
        }

        public MsgSynMemberInfo(Syndicate.Member aMember)
            : base(28)
        {
            Donation = aMember.Proffer;
            Rank = (Byte)aMember.Rank;
            Name = aMember.Name;
        }

        /// <summary>
        /// Process the message for the specified client.
        /// </summary>
        /// <param name="aClient">The client who sent the message.</param>
        public override void Process(Client aClient)
        {
            Player player = aClient.Player;
            if (player.Syndicate == null)
                return;

            Syndicate.Member member = player.Syndicate.GetMemberInfo(Name);
            if (member == null)
                return;

            player.Send(new MsgSynMemberInfo(member));
        }
    }
}
