// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2010- 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.Collections.Generic;
using COServer.Network;

namespace COServer.Entities
{
    public abstract class AdvancedEntity : Entity
    {
        public String Name;
        public Int32 CurHP;
        public Int32 MaxHP;
        public UInt16 Level;
        public Emotion Action;

        public Int32 MinAtk;
        public Int32 MaxAtk;
        public Int32 Defence;
        public Int32 MagicAtk;
        public Int32 MagicDef;
        public Int32 MagicBlock;
        public Int32 Dexterity;
        public Int32 Dodge;
        public Int32 AtkRange;
        public Int32 AtkSpeed;
        public Double Bless;
        public Double GemBonus;

        public Boolean IsInBattle;
        public Int32 TargetUID;
        public Int32 AtkType;
        public UInt16 MagicType;
        public Byte MagicLevel;
        public Boolean MagicIntone;
        public Int32 AtkSpeedT;

        /// <summary>
        /// The value of the attached statuses as a bit field.
        /// </summary>
        public UInt32 Statuses { get { return mStatusesValue; } }

        /// <summary>
        /// All the statuses attached to the entity.
        /// </summary>
        protected readonly Dictionary<Status, StatusEffect> mStatuses = new Dictionary<Status, StatusEffect>();
        /// <summary>
        /// The value of the attached statuses as a bit field.
        /// </summary>
        protected UInt32 mStatusesValue = 0;

        /// <summary>
        /// Attach a status to the entity.
        /// </summary>
        /// <param name="aStatus">The status to attach.</param>
        /// <param name="aDuration"></param>
        /// <param name="aData"></param>
        public void AttachStatus(Status aStatus, int aDuration = -1, Object aData = null)
        {
            lock (mStatuses)
            {
                StatusEffect effect;
                if (mStatuses.TryGetValue(aStatus, out effect))
                {
                    effect.Data = aData;
                    effect.ResetTimeout(aDuration);
                }
                else
                {
                    mStatuses.Add(aStatus, new StatusEffect(aStatus, aDuration, aData));
                }

                mStatusesValue |= (UInt32)aStatus;

                // update
                var msg = new MsgUserAttrib(this, mStatusesValue, MsgUserAttrib.AttributeType.Flags);
                World.BroadcastRoomMsg(this, msg);

                if (IsPlayer())
                {
                    Player player = (Player)this;
                    player.Send(msg);
                }
            }
        }

        /// <summary>
        /// Detach a status from the entity.
        /// </summary>
        /// <param name="aStatus">The status to detach.</param>
        public void DetachStatus(Status aStatus)
        {
            lock (mStatuses)
            {
                if (mStatuses.ContainsKey(aStatus))
                    mStatuses.Remove(aStatus);

                mStatusesValue &= ~(UInt32)aStatus;

                // update
                var msg = new MsgUserAttrib(this, mStatusesValue, MsgUserAttrib.AttributeType.Flags);
                World.BroadcastRoomMsg(this, msg);

                if (IsPlayer())
                {
                    Player player = (Player)this;
                    player.Send(msg);
                }
            }
        }

        /// <summary>
        /// Detach all the statuses from the entity.
        /// </summary>
        public void DetachAllStatuses()
        {
            lock (mStatuses)
            {
                mStatuses.Clear();
                mStatusesValue = 0;

                // update
                var msg = new MsgUserAttrib(this, mStatusesValue, MsgUserAttrib.AttributeType.Flags);
                World.BroadcastRoomMsg(this, msg);

                if (IsPlayer())
                {
                    Player player = (Player)this;
                    player.Send(msg);
                }
            }
        }

        /// <summary>
        /// Detach all the statuses from the entity and add back
        /// the default ones.
        /// </summary>
        public void ResetStatuses()
        {
            DetachAllStatuses();

            if (IsPlayer())
            {
                Player player = (Player)this;
                if (player.PkPoints >= 30 && player.PkPoints < 100)
                    AttachStatus(Status.RedName);
                else if (player.PkPoints >= 100)
                    AttachStatus(Status.BlackName);

                if (player.Team != null && player.Team.Leader == player)
                    AttachStatus(Status.TeamLeader);
            }
        }

        public bool HasStatus(Status aStatus)
        {
            lock (mStatuses)
            {
                return (mStatusesValue & (UInt32)aStatus) != 0;
            }
        }

        public bool GetStatus(Status aStatus, out StatusEffect aEffect)
        {
            lock (mStatuses)
            {
                return mStatuses.TryGetValue(aStatus, out aEffect);
            }
        }

        public AdvancedEntity(Int32 UniqId)
            : base(UniqId)
        {
            this.Action = Emotion.StandBy;

            this.IsInBattle = false;
            this.TargetUID = -1;
        }

        public bool IsCriminal() { return HasStatus(Status.Crime) || HasStatus(Status.BlackName); }
        public bool IsBlueName() { return HasStatus(Status.Crime); }
        public bool IsRedName() { return HasStatus(Status.RedName); }
        public bool IsBlackName() { return HasStatus(Status.BlackName); }

        public bool IsPoisoned() { return HasStatus(Status.Poison); }
        public bool IsFrozen() { return HasStatus(Status.Freeze); }
        public bool IsTeamLeader() { return HasStatus(Status.TeamLeader); }

        public bool HasMagicDefense() { return HasStatus(Status.MagicDefense); }
        public bool HasMagicAttack() { return HasStatus(Status.MagicAttack); }
        public bool HasMagicDodge() { return HasStatus(Status.MagicDodge); }

        public bool IsAlive() { return !HasStatus(Status.Die); }

        public bool IsFlying() { return HasStatus(Status.Flying); }
        public bool IsCastingPray() { return HasStatus(Status.CastingPray); }
        public bool IsPraying() { return HasStatus(Status.Praying); }
    }
}
