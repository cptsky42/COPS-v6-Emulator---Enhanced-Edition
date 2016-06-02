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
using COServer.Entities;

[assembly: InternalsVisibleTo("COServer.Network.Msg")]

namespace COServer.Network
{
    public class MsgUserAttrib : Msg
    {
        /// <summary>
        /// This is a "constant" that the child must override.
        /// It is the type of the message as specified in NetworkDef.cs file.
        /// </summary>
        protected override UInt16 _TYPE { get { return MSG_USERATTRIB; } }

        public enum AttributeType : uint
        {
            Life = 0,
            MaxLife = 1,
            Mana = 2,
            MaxMana = 3,
            Money = 4,
            Exp = 5,
            PkPoints = 6,
            Profession = 7,
            Energy = 9,
            AddPoints = 11,
            Look = 12,
            Level = 13,
            Spirit = 14,
            Vitality = 15,
            Strength = 16,
            Agility = 17,
            DblExpTime = 19,
            // 20 = GuildDonation (when you attack the GW pole for example, this is sent with your new donation)
            TimeAdd = 22, //Cyclone & Superman (0 = 0sec / 1 = 1sec / 2 = 2 sec / 3 = 3sec)
            Metempsychosis = 23,
            Flags = 26,
            Hair = 27,
            XP = 28,
            LuckyTime = 29,
        }

        //--------------- Internal Members ---------------
        private Int32 __UniqId = 0;
        private Dictionary<AttributeType, UInt64> __Attributes = new Dictionary<AttributeType, UInt64>();
        //------------------------------------------------

        /// <summary>
        /// Unique ID of the entity.
        /// </summary>
        public Int32 UniqId
        {
            get { return __UniqId; }
            set { __UniqId = value; WriteInt32(4, value); }
        }

        public Dictionary<AttributeType, UInt64> Attributes
        {
            get { return new Dictionary<AttributeType, UInt64>(__Attributes); }
            set
            {
                __Attributes = value;

                WriteInt32(8, value.Count);

                int pos = 12;
                foreach (var kv in value)
                {
                    WriteUInt32(pos + 0, (UInt32)kv.Key);
                    WriteUInt64(pos + 4, kv.Value);
                    pos += 12;
                }
            }
        }

        public MsgUserAttrib(Entity aEntity, Int64 aData, AttributeType aType)
            : base(24)
        {
            if (aData < 0)
                throw new ArgumentException("Data must be greater than 0.");

            UniqId = aEntity.UniqId;
            Attributes = new Dictionary<AttributeType, UInt64>()
            { 
                { aType, (UInt64)aData }
            };
        }

        public MsgUserAttrib(Entity aEntity, UInt64 aData, AttributeType aType)
            : base(24)
        {
            UniqId = aEntity.UniqId;
            Attributes = new Dictionary<AttributeType, UInt64>()
            { 
                { aType, aData }
            };
        }

        public MsgUserAttrib(Entity aEntity, Dictionary<AttributeType, UInt64> aAttributes)
            : base((UInt16)(12 + (aAttributes.Count * 12)))
        {
            UniqId = aEntity.UniqId;
            Attributes = aAttributes;
        }
    }
}
