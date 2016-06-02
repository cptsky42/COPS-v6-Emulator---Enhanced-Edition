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
    /// Message sent to the client by the MsgServer or by the client to the MsgServer to
    /// indicate a deplacement in a specific direction by walking or running.
    /// </summary>
    public class MsgWalk : Msg
    {
        /// <summary>
        /// This is a "constant" that the child must override.
        /// It is the type of the message as specified in NetworkDef.cs file.
        /// </summary>
        protected override UInt16 _TYPE { get { return MSG_WALK; } }

        //--------------- Internal Members ---------------
        private Int32 __UniqId = 0;
        private Byte __Direction = 0;
        private Byte __Mode = 0;
        private UInt16 __Unknown = 0;
        //------------------------------------------------

        /// <summary>
        /// Unique Id of the entity which is walking.
        /// </summary>
        public Int32 UniqId
        {
            get { return __UniqId; }
            set { __UniqId = value; WriteInt32(4, value); }
        }

        /// <summary>
        /// Direction of the mouvement.
        /// </summary>
        public Byte Direction
        {
            get { return __Direction; }
            set { __Direction = value; mBuf[8] = (Byte)(((MyMath.Generate(100, 255) % 31) * 8) + value); }
        }

        /// <summary>
        /// Mode of the mouvement (walk/run).
        /// </summary>
        public Byte Mode
        {
            get { return __Mode; }
            set { __Mode = value; mBuf[9] = value; }
        }

        /// <summary>
        /// Create a message object from the specified buffer.
        /// </summary>
        /// <param name="aBuf">The buffer containing the message.</param>
        /// <param name="aIndex">The index where the message is starting in the buffer.</param>
        /// <param name="aLength">The length of the message.</param>
        internal MsgWalk(Byte[] aBuf, int aIndex, int aLength)
            : base(aBuf, aIndex, aLength)
        {
            __UniqId = BitConverter.ToInt32(mBuf, 4);
            __Direction = (Byte)(mBuf[8] % 8);
            __Mode = mBuf[9];
            __Unknown = BitConverter.ToUInt16(mBuf, 10);
        }

        /// <summary>
        /// Create a message for the specified entity to move it
        /// in the specified direction.
        /// </summary>
        /// <param name="aUniqId">The unique Id of the entity.</param>
        /// <param name="aDirection">The direction of the mouvement.</param>
        /// <param name="aIsRunning">The mode of the mouvement.</param>
        public MsgWalk(Int32 aUniqId, Byte aDirection, Boolean aIsRunning)
            : base(12)
        {
            UniqId = aUniqId;
            Direction = aDirection;
            Mode = aIsRunning ? (Byte)0 : (Byte)1;
        }

        /// <summary>
        /// Process the message for the specified client.
        /// </summary>
        /// <param name="aClient">The client who sent the message.</param>
        public override void Process(Client aClient)
        {
            Player player = aClient.Player;
            bool isRunning = Mode != 0;

            if (UniqId != player.UniqId)
            {
                aClient.Disconnect();
                return;
            }

            UInt16 newX = player.X;
            UInt16 newY = player.Y;

            switch (Direction)
            {
                case 0: { newY += 1; break; }
                case 1: { newX -= 1; newY += 1; break; }
                case 2: { newX -= 1; break; }
                case 3: { newX -= 1; newY -= 1; break; }
                case 4: { newY -= 1; break; }
                case 5: { newX += 1; newY -= 1; break; }
                case 6: { newX += 1; break; }
                case 7: { newX += 1; newY += 1; break; }
            }

            if (player != null)
            {
                // TODO isGhost()
                //if (!Player.IsAlive() && !Player.IsGhost())
                //{
                //    Player.SendSysMsg(StrRes.STR_DIE);
                //    return;
                //}

                if (!player.Map.GetFloorAccess(newX, newY))
                {
                    player.SendSysMsg(StrRes.STR_INVALID_COORDINATE);
                    player.KickBack();
                    return;
                }

                // TODO re-enable prevX/Y in walk ?
                //player.PrevX = player.X;
                //player.PrevY = player.Y;

                player.X = newX;
                player.Y = newY;
                player.Direction = Direction;
                player.Action = Emotion.StandBy;

                player.IsInBattle = false;
                player.MagicIntone = false;
                player.Mining = false;

                player.Send(this);
                player.Screen.Move(this);
            }
        }
    }
}
