// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.Timers;
using System.Runtime.InteropServices;
using COServer.Network;

namespace COServer.Entities
{
    public partial class Pet
    {
        public class AI
        {
            private Pet Entity;

            private Timer MotorSystem;

            private Int32 ThinkSpeed;
            private Int32 MoveSpeed;
            private Int32 AtkSpeed;

            private Int32 ViewRange;
            private Int32 MoveRange;
            private Int32 AtkRange;

            private Int32 LastAttackTick;
            private Int32 LastMoveTick;
            private Int32 LastAwakeTick;

            private AdvancedEntity Target;

            public AI(Pet Entity, Int32 ThinkSpeed, Int32 MoveSpeed, Int32 AtkSpeed, Int32 ViewRange, Int32 MoveRange, Int32 AtkRange)
            {
                this.Entity = Entity;

                this.ThinkSpeed = ThinkSpeed;
                this.MoveSpeed = MoveSpeed;
                this.AtkSpeed = AtkSpeed + 1000;
                this.ViewRange = ViewRange;
                if (this.ViewRange > 15)
                    this.ViewRange = 15;
                this.MoveRange = MoveRange;
                this.AtkRange = AtkRange;

                this.LastAttackTick = Environment.TickCount;
                this.LastMoveTick = Environment.TickCount;
                this.LastAwakeTick = Environment.TickCount;

                this.MotorSystem = new Timer();
                this.MotorSystem.Interval += ThinkSpeed;
                this.MotorSystem.Elapsed += new ElapsedEventHandler(MotorSystem_TakingDecision);
            }

            ~AI()
            {
                Entity = null;
                if (MotorSystem != null)
                    MotorSystem.Stop();
                MotorSystem = null;
            }

            public void Reset()
            {
                MotorSystem.Stop();
                MotorSystem = null;

                LastAttackTick = Environment.TickCount;
                LastMoveTick = Environment.TickCount;

                Target = null;

                MotorSystem = new Timer();
                MotorSystem.Interval += ThinkSpeed;
                MotorSystem.Elapsed += new ElapsedEventHandler(MotorSystem_TakingDecision);
                if (Entity.IsAlive())
                    MotorSystem.Start();
            }

            public Boolean IsInVisualField(AdvancedEntity Target)
            {
                if (Target == null)
                    return false;

                if (Entity.Map != Target.Map)
                    return false;

                if (!MyMath.CanSee(Target.X, Target.Y, Entity.X, Entity.Y, ViewRange))
                    return false;
                return true;
            }

            public Boolean IsInVisualField(Int16 MapId, UInt16 X, UInt16 Y)
            {
                if (Entity.Map != MapId)
                    return false;

                if (!MyMath.CanSee(X, Y, Entity.X, Entity.Y, ViewRange))
                    return false;
                return true;
            }

            public Boolean IsInAttackField(AdvancedEntity Target)
            {
                if (Target == null)
                    return false;

                if (Entity.Map != Target.Map)
                    return false;

                if (!MyMath.CanSee(Target.X, Target.Y, Entity.X, Entity.Y, AtkRange))
                    return false;
                return true;
            }

            public Boolean IsInAttackField(Int16 MapId, UInt16 X, UInt16 Y)
            {
                if (Entity.Map != MapId)
                    return false;

                if (!MyMath.CanSee(X, Y, Entity.X, Entity.Y, AtkRange))
                    return false;
                return true;
            }

            //Moving...
            private void MotorSystem_TakingDecision(Object Sender, ElapsedEventArgs Args)
            {

            }
        }
    }
}
