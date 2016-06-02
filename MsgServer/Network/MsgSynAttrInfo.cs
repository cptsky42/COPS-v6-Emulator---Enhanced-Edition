// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2011 - 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("COServer.Network.Msg")]

namespace COServer.Network
{
    public class MsgSynAttrInfo : Msg
    {
        /// <summary>
        /// This is a "constant" that the child must override.
        /// It is the type of the message as specified in NetworkDef.cs file.
        /// </summary>
        protected override UInt16 _TYPE { get { return MSG_SYNATTRINFO; } }

        //--------------- Internal Members ---------------
        private UInt32 __SynId = 0;
        private UInt32 __Proffer = 0;
        private UInt32 __SyndicateFund = 0;
        private UInt32 __SyndicatePopulation = 0;
        private Byte __SynRank = 0;
        private String __Leader = "";
        //------------------------------------------------

        public UInt32 SynId
        {
            get { return __SynId; }
            set { __SynId = value; WriteUInt32(4, value); }
        }

        public UInt32 Proffer
        {
            get { return __Proffer; }
            set { __Proffer = value; WriteUInt32(8, value); }
        }

        public UInt32 SyndicateFund
        {
            get { return __SyndicateFund; }
            set { __SyndicateFund = value; WriteUInt32(12, value); }
        }

        public UInt32 SyndicatePopulation
        {
            get { return __SyndicatePopulation; }
            set { __SyndicatePopulation = value; WriteUInt32(16, value); }
        }

        public Byte SynRank
        {
            get { return __SynRank; }
            set { __SynRank = value; mBuf[20] = value; }
        }

        public String Leader
        {
            get { return __Leader; }
            set { __Leader = value; WriteString(21, value, MAX_NAME_SIZE); }
        }

        public MsgSynAttrInfo(Int32 aMemberId, Syndicate aSyn)
            : base(40)
        {
            Syndicate.Member member = null;
            if (aSyn != null)
            {
                if (aSyn.Leader.Id == aMemberId)
                    member = aSyn.Leader;
                else
                    member = aSyn.Members[aMemberId];
            }

            if (aSyn != null)
            {
                SynId = (UInt32)aSyn.Id;
                Proffer = member.Proffer;
                SyndicateFund = aSyn.Money;
                SyndicatePopulation = (UInt32)(aSyn.Members.Count + 1);
                SynRank = (Byte)member.Rank;
                Leader = aSyn.Leader.Name;
            }
        }
    }
}
