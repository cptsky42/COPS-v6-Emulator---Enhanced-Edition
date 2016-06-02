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
    public class MsgTeamMember : Msg
    {
        /// <summary>
        /// This is a "constant" that the child must override.
        /// It is the type of the message as specified in NetworkDef.cs file.
        /// </summary>
        protected override UInt16 _TYPE { get { return MSG_TEAMMEMBER; } }

        public enum Action
        {
            AddMember = 0,
            DelMember = 1,
        }

        //--------------- Internal Members ---------------
        private Action __Action = (Action)0;
        private Byte __Amount = 1;
        // TeamMemberInfo
        private String __Name = "";
        private Int32 __Id = 0;
        private UInt32 __Lookface = 0;
        private UInt16 __MaxLife = 0;
        private UInt16 __Life = 0;
        //------------------------------------------------

        public Action _Action
        {
            get { return __Action; }
            set { __Action = value; mBuf[4] = (Byte)value; }
        }

        public Byte Amount
        {
            get { return __Amount; }
            set { __Amount = value; mBuf[5] = value; }
        }

        public String Name
        {
            get { return __Name; }
            set { __Name = value; WriteString(8, value, MAX_NAME_SIZE); }
        }

        /// <summary>
        /// Unique ID of the team member.
        /// </summary>
        public Int32 Id
        {
            get { return __Id; }
            set { __Id = value; WriteInt32(24, value); }
        }

        /// <summary>
        /// Look of the team member.
        /// </summary>
        public UInt32 Lookface
        {
            get { return __Lookface; }
            set { __Lookface = value; WriteUInt32(28, value); }
        }

        public UInt16 MaxLife
        {
            get { return __MaxLife; }
            set { __MaxLife = value; WriteUInt16(32, value); }
        }

        public UInt16 Life
        {
            get { return __Life; }
            set { __Life = value; WriteUInt16(34, value); }
        }

        public MsgTeamMember(Player aMember, Action aAction)
            : base(36)
        {
            _Action = aAction;
            Amount = 1;

            Name = aMember.Name;
            Id = aMember.UniqId;
            Lookface = aMember.Look;
            MaxLife = (UInt16)aMember.MaxHP;
            Life = (UInt16)aMember.CurHP;
        }
    }
}
