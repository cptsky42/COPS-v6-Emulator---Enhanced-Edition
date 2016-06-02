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
    /// <summary>
    /// Message sent to the client to add a new weapon skill.
    /// </summary>
    public class MsgWeaponSkill : Msg
    {
        /// <summary>
        /// This is a "constant" that the child must override.
        /// It is the type of the message as specified in NetworkDef.cs file.
        /// </summary>
        protected override UInt16 _TYPE { get { return MSG_WEAPONSKILL; } }

        //--------------- Internal Members ---------------
        private UInt32 __Type = 0;
        private UInt32 __Level = 0;
        private UInt32 __Exp = 0;
        //------------------------------------------------

        /// <summary>
        /// Id of the weapon skill.
        /// </summary>
        public new UInt16 Type
        {
            get { return (UInt16)__Type; }
            set { __Type = value; WriteUInt32(4, value); }
        }

        /// <summary>
        /// Level of the weapon skill.
        /// </summary>
        public Byte Level
        {
            get { return (Byte)__Level; }
            set { __Level = value; WriteUInt32(8, value); }
        }

        /// <summary>
        /// Experience of the weapon skill.
        /// </summary>
        public UInt32 Exp
        {
            get { return __Exp; }
            set { __Exp = value; WriteUInt32(12, value); }
        }

        /// <summary>
        /// Create a new message for the specified skill.
        /// </summary>
        /// <param name="aSkill">The weapon skill.</param>
        public MsgWeaponSkill(WeaponSkill aSkill)
            : base(16)
        {
            Type = aSkill.Type;
            Level = aSkill.Level;
            Exp = aSkill.Exp;
        }
    }
}
