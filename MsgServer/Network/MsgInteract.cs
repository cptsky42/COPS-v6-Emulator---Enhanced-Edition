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
    public class MsgInteract : Msg
    {
        /// <summary>
        /// This is a "constant" that the child must override.
        /// It is the type of the message as specified in NetworkDef.cs file.
        /// </summary>
        protected override UInt16 _TYPE { get { return MSG_INTERACT; } }

        public enum Action
        {
            Steal = 1, //Works?
            Attack = 2, //Attack Miss -> Param = 0
            Heal = 3, //Works?
            Poison = 4, //Works?
            Assassinate = 5, //Works?
            Freeze = 6, //Works?
            Unfreeze = 7, //Works?
            Court = 8,
            Marry = 9,
            Divorce = 10, //Works?
            PresentMoney = 11, //Works?
            PresentItem = 12, //Works?
            SendFlowers = 13,
            Kill = 14,
            JoinSyn = 15, //Works?
            AcceptSynMember = 16, //Works?
            KickOutSynMember = 17, //Works?
            PresentPower = 18, //Works?
            QueryInfo = 19, //Works?
            RushAtk = 20, //Works?
            MagicAttack = 21,
            AbortMagic = 22,
            ReflectWeapon = 23,
            Bump = 24, //Dash
            Shoot = 25,
            ReflectMagic = 26,			
        };

        //--------------- Internal Members ---------------
        private Int32 __Timestamp = 0;
        private Int32 __Sender = 0;
        private Int32 __Target = 0;
        private UInt16 __PosX = 0;
        private UInt16 __PosY = 0;
        private Action __Action = (Action)0;
        private UInt32 __Data = 0;
        //------------------------------------------------

        public Int32 Timestamp
        {
            get { return __Timestamp; }
            set { __Timestamp = value; WriteInt32(4, value); }
        }

        public Int32 Sender
        {
            get { return __Sender; }
            set { __Sender = value; WriteInt32(8, value); }
        }

        public Int32 Target
        {
            get { return __Target; }
            set { __Target = value; WriteInt32(12, value); }
        }

        public UInt16 PosX
        {
            get { return __PosX; }
            set { __PosX = value; WriteUInt16(16, value); }
        }

        public UInt16 PosY
        {
            get { return __PosY; }
            set { __PosY = value; WriteUInt16(18, value); }
        }

        /// <summary>
        /// Action ID.
        /// </summary>
        public Action _Action
        {
            get { return __Action; }
            set { __Action = value; WriteUInt32(20, (UInt32)value); }
        }

        public UInt32 Data
        {
            get { return __Data; }
            set { __Data = value; WriteUInt32(24, value); }
        }

        /// <summary>
        /// Create a message object from the specified buffer.
        /// </summary>
        /// <param name="aBuf">The buffer containing the message.</param>
        /// <param name="aIndex">The index where the message is starting in the buffer.</param>
        /// <param name="aLength">The length of the message.</param>
        internal MsgInteract(Byte[] aBuf, int aIndex, int aLength)
            : base(aBuf, aIndex, aLength)
        {
            __Timestamp = BitConverter.ToInt32(mBuf, 4);
            __Sender = BitConverter.ToInt32(mBuf, 8);
            __Target = BitConverter.ToInt32(mBuf, 12);
            __PosX = BitConverter.ToUInt16(mBuf, 16);
            __PosY = BitConverter.ToUInt16(mBuf, 18);
            __Action = (Action)BitConverter.ToUInt32(mBuf, 20);
            __Data = BitConverter.ToUInt32(mBuf, 24);
        }

        public MsgInteract(Entity aSender, Entity aTarget, UInt32 aData, Action aAction)
            : base(32)
        {
            Timestamp = Environment.TickCount;
            Sender = aSender.UniqId;
            if (aTarget != null)
            {
                Target = aTarget.UniqId;
                PosX = aTarget.X;
                PosY = aTarget.Y;
            }
            _Action = aAction;
            Data = aData;
        }

        public MsgInteract(Entity aSender, Entity aTarget, Int32 aData, Action aAction)
            : base(32)
        {
            Timestamp = Environment.TickCount;
            Sender = aSender.UniqId;
            if (aTarget != null)
            {
                Target = aTarget.UniqId;
                PosX = aTarget.X;
                PosY = aTarget.Y;
            }
            _Action = aAction;
            Data = BitConverter.ToUInt32(BitConverter.GetBytes(aData), 0);
        }

        /// <summary>
        /// Process the message for the specified client.
        /// </summary>
        /// <param name="aClient">The client who sent the message.</param>
        public override void Process(Client aClient)
        {
            Player player = aClient.Player;

            int interval = Timestamp - player.LastAttackTick;

            player.LastAttackTick = Timestamp;
            player.MagicIntone = false;

            switch (_Action)
            {
                case Action.Attack:
                    {
                        player.Action = Emotion.StandBy;
                        if (player.UniqId != Sender)
                            return;

                        if (!MyMath.CanSee(player.X, player.Y, PosX, PosY, player.AtkRange))
                            return;

                        player.TargetUID = Target;
                        player.AtkType = (Int32)_Action;
                        player.IsInBattle = true;
                        player.MagicType = 0;
                        player.MagicLevel = 0;

                        if (Entity.IsPlayer(Target))
                        {
                            Player target = null;
                            if (!World.AllPlayers.TryGetValue(Target, out target))
                            {
                                player.IsInBattle = false;
                                return;
                            }

                            if (!MyMath.CanSee(player.X, player.Y, target.X, target.Y, player.AtkRange))
                            {
                                player.IsInBattle = false;
                                return;
                            }
                            Battle.PvP(player, target);
                        }
                        else if (Entity.IsMonster(Target))
                        {
                            Monster target = null;
                            if (!World.AllMonsters.TryGetValue(Target, out target))
                            {
                                player.IsInBattle = false;
                                return;
                            }

                            if (!MyMath.CanSee(player.X, player.Y, target.X, target.Y, player.AtkRange))
                            {
                                player.IsInBattle = false;
                                return;
                            }
                            Battle.PvM(player, target);
                        }
                        else if (Entity.IsTerrainNPC(Target))
                        {
                            TerrainNPC target = null;
                            if (!World.AllTerrainNPCs.TryGetValue(Target, out target))
                            {
                                player.IsInBattle = false;
                                return;
                            }

                            if (!MyMath.CanSee(player.X, player.Y, target.X, target.Y, player.AtkRange))
                            {
                                player.IsInBattle = false;
                                return;
                            }
                            Battle.PvE(player, target);
                        }
                        else
                        {
                            player.IsInBattle = false;
                            return;
                        }
                        break;
                    }
                case Action.Court:
                    {
                        if (player.UniqId != Sender)
                            return;

                        Player target = null;
                        if (!World.AllPlayers.TryGetValue(Target, out target))
                            return;

                        if (target.Map != player.Map)
                            return;

                        if (!MyMath.CanSee(player.X, player.Y, target.X, target.Y, 17))
                            return;

                        if (!(player.Look % 10 < 3 && target.Look % 10 > 2) && !(player.Look % 10 > 2 && target.Look % 10 < 3))
                            return;

                        if (target.Mate != "Non" && target.Mate != "None" && target.Mate != "")
                            return;

                        target.Send(this);
                        break;
                    }
                case Action.Marry:
                    {
                        if (player.UniqId != Sender)
                            return;

                        Player target = null;
                        if (!World.AllPlayers.TryGetValue(Target, out target))
                            return;

                        if (target.Map != player.Map)
                            return;

                        if (!MyMath.CanSee(player.X, player.Y, target.X, target.Y, 17))
                            return;

                        if (!(player.Look % 10 < 3 && target.Look % 10 > 2) && !(player.Look % 10 > 2 && target.Look % 10 < 3))
                            return;

                        if (target.Mate != "Non" && target.Mate != "None" && target.Mate != "")
                            return;

                        player.Mate = target.Name;
                        target.Mate = player.Name;

                        World.BroadcastMapMsg(player, new MsgName((player.X | player.Y << 16), "1122", MsgName.NameAct.Fireworks));
                        World.BroadcastMsg(new MsgTalk("SYSTEM", "ALLUSERS", String.Format(StrRes.STR_MARRY, player.Name, target.Name), Channel.GM, Color.Red));
                        break;
                    }
                case Action.MagicAttack:
                    {
                        player.Action = Emotion.StandBy;

                        Magic.Info MagicInfo = new Magic.Info();
                        UInt16 MagicType = (UInt16)(Data);

                        //uint16_t encrypt_skilllevel(uint8_t p_skilllevel, int p_timestamp)
                        //{
                        //    return (uint16_t)((p_skilllevel | ((p_timstamp << 8) & 0xff00)) ^ 0x3721);
                        //}

                        if (player.UniqId != Sender)
                            return;

                        Target = (Int32)((((UInt32)Target & 0xffffe000) >> 13) | (((UInt32)Target & 0x1fff) << 19));
                        Target ^= 0x5F2D2463;
                        Target ^= Sender;
                        Target -= 0x746F4AE6;

                        PosX ^= (UInt16)((UInt32)Sender & 0xffff);
                        PosX ^= 0x2ED6;
                        PosX = (UInt16)((PosX << 1) | ((PosX & 0x8000) >> 15));
                        PosX -= 0x22EE;

                        PosY ^= (UInt16)((UInt32)Sender & 0xffff);
                        PosY ^= 0xB99B;
                        PosY = (UInt16)((PosY << 5) | ((PosY & 0xF800) >> 11));
                        PosY -= 0x8922;

                        MagicType ^= (UInt16)0x915D;
                        MagicType ^= (UInt16)Sender;
                        MagicType = (UInt16)(MagicType << 3 | MagicType >> 13);
                        MagicType -= (UInt16)0xEB42;

                        Magic Magic = player.GetMagicByType(MagicType);
                        if (Magic == null)
                            return;

                        if (!Database.AllMagics.TryGetValue((Magic.Type * 10) + Magic.Level, out MagicInfo))
                            return;

                        player.TargetUID = Target;
                        player.AtkType = (Int32)_Action;
                        player.IsInBattle = true;
                        player.MagicType = MagicType;
                        player.MagicLevel = Magic.Level;

                        if (Target == 0)
                        {
                            Battle.UseMagic(player, null, PosX, PosY);
                            if (player.Map.Id != 1039)
                                player.IsInBattle = false;
                            return;
                        }

                        if (Entity.IsPlayer(Target))
                        {
                            Player target = null;
                            if (!World.AllPlayers.TryGetValue(Target, out target))
                            {
                                player.IsInBattle = false;
                                return;
                            }

                            if (!MyMath.CanSee(player.X, player.Y, target.X, target.Y, (Int32)MagicInfo.Distance))
                            {
                                player.IsInBattle = false;
                                return;
                            }
                            Battle.UseMagic(player, target, PosX, PosY);
                        }
                        else if (Entity.IsMonster(Target))
                        {
                            Monster target = null;
                            if (!World.AllMonsters.TryGetValue(Target, out target))
                            {
                                player.IsInBattle = false;
                                return;
                            }

                            if (!MyMath.CanSee(player.X, player.Y, target.X, target.Y, (Int32)MagicInfo.Distance))
                            {
                                player.IsInBattle = false;
                                return;
                            }
                            Battle.UseMagic(player, target, PosX, PosY);
                        }
                        else if (Entity.IsTerrainNPC(Target))
                        {
                            TerrainNPC target = null;
                            if (!World.AllTerrainNPCs.TryGetValue(Target, out target))
                            {
                                player.IsInBattle = false;
                                return;
                            }

                            if (!MyMath.CanSee(player.X, player.Y, target.X, target.Y, (Int32)MagicInfo.Distance))
                            {
                                player.IsInBattle = false;
                                return;
                            }
                            Battle.UseMagic(player, target, PosX, PosY);
                        }

                        if (player.Map.Id != 1039)
                            player.IsInBattle = false;
                        break;
                    }
                case Action.Shoot:
                    {
                        player.Action = Emotion.StandBy;
                        if (player.UniqId != Sender)
                            return;

                        player.TargetUID = Target;
                        player.AtkType = (Int32)_Action;
                        player.IsInBattle = true;
                        player.MagicType = 0;
                        player.MagicLevel = 0;

                        if (Entity.IsPlayer(Target))
                        {
                            Player target = null;
                            if (!World.AllPlayers.TryGetValue(Target, out target))
                            {
                                player.IsInBattle = false;
                                return;
                            }

                            if (!MyMath.CanSee(player.X, player.Y, target.X, target.Y, player.AtkRange))
                            {
                                player.IsInBattle = false;
                                return;
                            }
                            Battle.PvP(player, target);
                        }
                        else if (Entity.IsMonster(Target))
                        {
                            Monster target = null;
                            if (!World.AllMonsters.TryGetValue(Target, out target))
                            {
                                player.IsInBattle = false;
                                return;
                            }

                            if (!MyMath.CanSee(player.X, player.Y, target.X, target.Y, player.AtkRange))
                            {
                                player.IsInBattle = false;
                                return;
                            }
                            Battle.PvM(player, target);
                        }
                        else if (Entity.IsTerrainNPC(Target))
                        {
                            TerrainNPC target = null;
                            if (!World.AllTerrainNPCs.TryGetValue(Target, out target))
                            {
                                player.IsInBattle = false;
                                return;
                            }

                            if (!MyMath.CanSee(player.X, player.Y, target.X, target.Y, player.AtkRange))
                            {
                                player.IsInBattle = false;
                                return;
                            }
                            Battle.PvE(player, target);
                        }
                        else
                        {
                            player.IsInBattle = false;
                            return;
                        }
                        break;
                    }
                default:
                    {
                        sLogger.Error("Action {0} is not implemented for MsgInteract.", (UInt32)_Action);
                        break;
                    }
            }
        }
    }
}
