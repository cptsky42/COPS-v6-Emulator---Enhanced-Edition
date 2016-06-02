// * Created by Jean-Philippe Boivin
// * Copyright © 2011, 2014-2015
// * COPS v6 Emulator

using System;
using System.Linq;
using System.Timers;
using System.Runtime.InteropServices;
using COServer.Network;

namespace COServer.Entities
{
    public class MonsterAI : AI
    {
        private Int32 LastAttackTick;
        private Int32 LastMoveTick;
        private Int32 LastAwakeTick;

        private Player Target;

        public MonsterAI(Monster aEntity, Int32 aThinkSpeed, Int32 aMoveSpeed, Int32 aAtkSpeed, Int32 aViewRange, Int32 aMoveRange, Int32 aAtkRange)
            : base(aEntity, aThinkSpeed, aMoveSpeed, aAtkSpeed, aViewRange, aMoveRange, aAtkRange)
        {
            this.LastAttackTick = Environment.TickCount;
            this.LastMoveTick = Environment.TickCount;
            this.LastAwakeTick = Environment.TickCount;

            this.MotorSystem = new Timer();
            this.MotorSystem.Interval += mThinkSpeed;
            this.MotorSystem.Elapsed += new ElapsedEventHandler(MotorSystem_TakingDecision);
        }

        ~MonsterAI()
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
            try
            {
                if (!mEntity.IsAlive())
                {
                    Sleep();
                    return;
                }

                if (Environment.TickCount - LastAwakeTick < 500)
                    return;

                var players = from entity in mEntity.Map.Entities.Values where entity.IsPlayer() select (Player)entity;

                Boolean Stop = true;
                foreach (Player player in players)
                {
                    if (MyMath.CanSee(player.X, player.Y, mEntity.X, mEntity.Y, mViewRange))
                    {
                        Stop = false;
                        break;
                    }
                }

                if (Stop)
                {
                    Sleep();
                    return;
                }

                if (MyMath.Success(2.5))
                    return;

                if (!mEntity.IsInBattle && mEntity.TargetUID <= 0)
                {
                    Int32 Shortest = mViewRange;
                    Player FTarget = null;

                    var targets = from entity in mEntity.Map.Entities.Values where entity.IsPlayer() select (Player)entity;

                    foreach (Player target in targets)
                    {
                        //Guard
                        if (mEntity.AIType == (Byte)AI_Type.CityGuard)
                        {
                            if (!target.HasStatus(Status.Crime))
                                continue;
                        }

                        //Patrol
                        if (mEntity.AIType == (Byte)AI_Type.CityPatrol)
                        {
                            if (!target.HasStatus(Status.Crime)
                                && !target.HasStatus(Status.BlackName))
                                continue;
                        }

                        //Reviver
                        if (mEntity.AIType == (Byte)AI_Type.Reviver)
                        {
                            if (target.IsAlive())
                                continue;
                        }
                        else
                        {
                            if (!target.IsAlive())
                                continue;
                        }

                        // non-magical monsters cannot attack a flying player...
                        if (target.IsFlying())
                        {
                            if (mEntity.AIType == (Byte)AI_Type.Normal)
                                if (mEntity.AtkType != 21)
                                    continue;
                        }

                        if (MyMath.GetDistance(mEntity.X, mEntity.Y, target.X, target.Y) == Shortest)
                        {
                            if (FTarget != null && FTarget.Level > target.Level)
                                continue;
                            FTarget = target;
                            Shortest = MyMath.GetDistance(mEntity.X, mEntity.Y, target.X, target.Y);
                        }
                        else if (MyMath.GetDistance(mEntity.X, mEntity.Y, target.X, target.Y) < Shortest)
                        {
                            FTarget = target;
                            Shortest = MyMath.GetDistance(mEntity.X, mEntity.Y, target.X, target.Y);
                        }
                        else
                            continue;
                    }
                    if (FTarget == null)
                    {
                        mEntity.TargetUID = -1;
                        Target = null;
                        return;
                    }

                    mEntity.TargetUID = FTarget.UniqId;
                    this.Target = FTarget;
                }
                else if (!mEntity.IsInBattle)
                {
                    if (Target == null)
                    {
                        mEntity.TargetUID = -1;
                        return;
                    }

                    if (!IsInVisualField(Target))
                    {
                        mEntity.TargetUID = -1;
                        Target = null;
                        return;
                    }

                    if (IsInAttackField(Target))
                    {
                        mEntity.IsInBattle = true;
                        return;
                    }

                    if (Environment.TickCount - LastMoveTick < mMoveSpeed)
                        return;

                    if (MyMath.Success(95.0) || (mEntity.AIType == (Byte)AI_Type.CityGuard || mEntity.AIType == (Byte)AI_Type.CityPatrol))
                    {
                        UInt16 NewX = mEntity.X;
                        UInt16 NewY = mEntity.Y;

                        Byte ToDir = (Byte)MyMath.GetDirectionCO(mEntity.X, mEntity.Y, Target.X, Target.Y);
                        switch (ToDir)
                        {
                            case 0: { NewY += 1; break; }
                            case 1: { NewX -= 1; NewY += 1; break; }
                            case 2: { NewX -= 1; break; }
                            case 3: { NewX -= 1; NewY -= 1; break; }
                            case 4: { NewY -= 1; break; }
                            case 5: { NewX += 1; NewY -= 1; break; }
                            case 6: { NewX += 1; break; }
                            case 7: { NewX += 1; NewY += 1; break; }
                        }

                        if (!mEntity.Map.GetFloorAccess(NewX, NewY))
                            return;

                        var monsters = from entity in mEntity.Map.Entities.Values where entity.IsMonster() select (Monster)entity;

                        foreach (Monster monster in monsters)
                        {
                            if (monster.UniqId == mEntity.UniqId)
                                continue;

                            if (monster.X == NewX && monster.Y == NewY)
                                return;
                        }

                        mEntity.X = NewX;
                        mEntity.Y = NewY;
                        mEntity.Direction = ToDir;

                        World.BroadcastMapMsg(mEntity, new MsgWalk(mEntity.UniqId, ToDir, false));
                        LastMoveTick = Environment.TickCount;
                    }
                }
                else
                {
                    if (Environment.TickCount - LastAttackTick < mAtkSpeed)
                        return;

                    if (!IsInVisualField(Target))
                    {
                        mEntity.IsInBattle = false;
                        mEntity.TargetUID = -1;
                        Target = null;
                        return;
                    }

                    if (!IsInAttackField(Target))
                    {
                        // out of range, must move
                        mEntity.IsInBattle = false;
                        return;
                    }

                    //All
                    if (mEntity.AIType != (Byte)AI_Type.Reviver)
                        if (!Target.IsAlive())
                        {
                            mEntity.IsInBattle = false;
                            mEntity.TargetUID = -1;
                            Target = null;
                            return;
                        }

                    //Guard
                    if (mEntity.AIType == (Byte)AI_Type.CityGuard)
                        if (!Target.HasStatus(Status.Crime))
                        {
                            mEntity.IsInBattle = false;
                            mEntity.TargetUID = -1;
                            Target = null;
                            return;
                        }

                    //Patrol
                    if (mEntity.AIType == (Byte)AI_Type.CityPatrol)
                        if (!Target.HasStatus(Status.Crime)
                            && !Target.HasStatus(Status.BlackName))
                        {
                            mEntity.IsInBattle = false;
                            mEntity.TargetUID = -1;
                            Target = null;
                            return;
                        }

                    //Reviver
                    if (mEntity.AIType == (Byte)AI_Type.Reviver)
                        if (Target.IsAlive())
                        {
                            mEntity.IsInBattle = false;
                            mEntity.TargetUID = -1;
                            Target = null;
                            return;
                        }

                    LastAttackTick = Environment.TickCount;
                    if (MyMath.Success(2.50))
                        return;

                    if (mEntity.AtkType != 21)
                        Battle.MvP(mEntity, Target);
                    else
                        Battle.UseMagic(mEntity, Target, Target.X, Target.Y);
                }
            }
            catch (Exception exc) { Console.WriteLine(exc); }
        }
    }
}
