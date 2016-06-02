// * Created by Jean-Philippe Boivin
// * Copyright © 2011, 2014
// * COPS v6 Emulator

using System;
using System.Timers;
using System.Runtime.InteropServices;
using COServer.Network;

namespace COServer.Entities
{
    public class PetAI : AI
    {
        private Int32 LastAttackTick;
        private Int32 LastMoveTick;
        private Int32 LastAwakeTick;

        private AdvancedEntity Target;

        public PetAI(Pet aEntity, Int32 aThinkSpeed, Int32 aMoveSpeed, Int32 aAtkSpeed, Int32 aViewRange, Int32 aMoveRange, Int32 aAtkRange)
            : base(aEntity, aThinkSpeed, aMoveSpeed, aAtkSpeed, aViewRange, aMoveRange, aAtkRange)
        {
            this.Target = null;

            this.LastAttackTick = Environment.TickCount;
            this.LastMoveTick = Environment.TickCount;
            this.LastAwakeTick = Environment.TickCount;

            this.MotorSystem = new Timer();
            this.MotorSystem.Interval += mThinkSpeed;
            this.MotorSystem.Elapsed += new ElapsedEventHandler(MotorSystem_TakingDecision);
        }

        ~PetAI()
        {
            if (MotorSystem != null)
                MotorSystem.Stop();
            MotorSystem = null;
        }

        public override void Reset()
        {
            MotorSystem.Stop();
            MotorSystem = null;

            LastAttackTick = Environment.TickCount;
            LastMoveTick = Environment.TickCount;

            Target = null;

            MotorSystem = new Timer();
            MotorSystem.Interval += mThinkSpeed;
            MotorSystem.Elapsed += new ElapsedEventHandler(MotorSystem_TakingDecision);
            if (mEntity.IsAlive())
                MotorSystem.Start();
        }

        public override void Sleep() { MotorSystem.Stop(); Target = null; mEntity.TargetUID = -1; mEntity.IsInBattle = false; }
        public override void Awake() { LastAwakeTick = Environment.TickCount; MotorSystem.Start(); }

        //Moving...
        private void MotorSystem_TakingDecision(Object Sender, ElapsedEventArgs Args)
        {
            Player owner = (mEntity as Pet).Owner;

            if (!mEntity.IsAlive())
            {
                Sleep();
                return;
            }

            if (Environment.TickCount - LastAwakeTick < 500)
                return;

            if (MyMath.Success(2.5))
                return;

            if (Environment.TickCount - LastMoveTick >= mMoveSpeed)
            {
                if (MyMath.GetDistance(mEntity.X, mEntity.Y, owner.X, owner.Y) > 12)
                {
                    UInt16 newX = (UInt16)(owner.X + MyMath.Generate(-2, 2));
                    UInt16 newY = (UInt16)(owner.Y + MyMath.Generate(-2, 2));

                    if (!mEntity.Map.GetFloorAccess(newX, newY))
                        return;

                    mEntity.X = newX;
                    mEntity.Y = newY;

                    World.BroadcastRoomMsg(mEntity, new MsgAction(mEntity, (Int32)((newY << 16) | (newX)), MsgAction.Action.Jump));
                    LastMoveTick = Environment.TickCount;
                    return;
                }
                else if (MyMath.GetDistance(mEntity.X, mEntity.Y, owner.X, owner.Y) > 4)
                {
                    if (!mEntity.IsInBattle)
                    {
                        UInt16 newX = mEntity.X;
                        UInt16 newY = mEntity.Y;

                        Byte direction = (Byte)MyMath.GetDirectionCO(mEntity.X, mEntity.Y, owner.X, owner.Y);
                        switch (direction)
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

                        if (!mEntity.Map.GetFloorAccess(newX, newY))
                            return;

                        mEntity.X = newX;
                        mEntity.Y = newY;
                        mEntity.Direction = direction;

                        World.BroadcastMapMsg(mEntity, new MsgWalk(mEntity.UniqId, direction, false));
                        LastMoveTick = Environment.TickCount;
                        return;
                    }
                }
            }

            if (mEntity.IsInBattle)
            {
                if (Environment.TickCount - LastAttackTick < mAtkSpeed)
                    return;

                LastAttackTick = Environment.TickCount;
                if (MyMath.Success(2.50))
                    return;

                if (!Target.IsAlive() || !IsInAttackField(Target))
                {
                    mEntity.TargetUID = -1;
                    mEntity.IsInBattle = false;
                    Target = null;
                    return;
                }

                Battle.UseMagic(mEntity, Target, Target.X, Target.Y);
            }
            else
            {
                if (owner.TargetUID > 0)
                {
                    if (owner.TargetUID != owner.UniqId)
                    {
                        if (Entity.IsMonster(owner.TargetUID))
                        {
                            Monster monster = null;
                            if (World.AllMonsters.TryGetValue(owner.TargetUID, out monster))
                                Target = monster;
                        }
                        else if (Entity.IsPlayer(owner.TargetUID))
                        {
                            Player player = null;
                            if (World.AllPlayers.TryGetValue(owner.TargetUID, out player))
                                Target = player;
                        }
                        else if (Entity.IsTerrainNPC(owner.TargetUID))
                        {
                            TerrainNPC npc = null;
                            if (World.AllTerrainNPCs.TryGetValue(owner.TargetUID, out npc))
                                Target = npc;
                        }
                    }

                    if (Target != null)
                    {
                        if (!Target.IsAlive() || !IsInAttackField(Target))
                            Target = null;
                    }

                    if (Target != null)
                    {
                        mEntity.TargetUID = Target.UniqId;
                        mEntity.IsInBattle = true;
                    }
                }
            }
        }
    }
}
