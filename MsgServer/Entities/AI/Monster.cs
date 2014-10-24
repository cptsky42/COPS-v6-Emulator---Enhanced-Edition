// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.Timers;
using System.Runtime.InteropServices;
using COServer.Network;

namespace COServer.Entities
{
    public partial class Monster
    {
        //Type
        //00 -> No Targetting
        //01 -> Normal
        //03 -> Guard
        //04 -> Patrol
        //05 -> Reviver

        public class AI
        {
            private Monster Entity;

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

            private Player Target;

            public AI(Monster Entity, Int32 ThinkSpeed, Int32 MoveSpeed, Int32 AtkSpeed, Int32 ViewRange, Int32 MoveRange, Int32 AtkRange)
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

            public Boolean IsInVisualField(Player Player)
            {
                if (Player == null)
                    return false;

                if (Entity.Map != Player.Map)
                    return false;

                if (!MyMath.CanSee(Player.X, Player.Y, Entity.X, Entity.Y, ViewRange))
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

            public Boolean IsInAttackField(Player Player)
            {
                if (Player == null)
                    return false;

                if (Entity.Map != Player.Map)
                    return false;

                if (!MyMath.CanSee(Player.X, Player.Y, Entity.X, Entity.Y, AtkRange))
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

            public void Sleep() { MotorSystem.Stop(); Target = null; Entity.TargetUID = -1; Entity.IsInBattle = false; }
            public void Awake() { LastAwakeTick = Environment.TickCount;  MotorSystem.Start(); }

            //Moving...
            private void MotorSystem_TakingDecision(Object Sender, ElapsedEventArgs Args)
            {
                try
                {
                    if (Entity.Type == 0)
                    {
                        Sleep();
                        return;
                    }

                    if (!Entity.IsAlive())
                    {
                        Sleep();
                        return;
                    }

                    if (Environment.TickCount - LastAwakeTick < 1000)
                        return;

                    Map Map = null;
                    if (!World.AllMaps.TryGetValue(Entity.Map, out Map))
                        return;

                    Boolean Stop = true;
                    foreach (Object Object in Map.Entities.Values)
                    {
                        Player Player = (Object as Player);
                        if (Player == null)
                            continue;

                        if (MyMath.CanSee(Player.X, Player.Y, Entity.X, Entity.Y, ViewRange))
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

                    if ((Entity.Type == 3 || Entity.Type == 4) && Map.InWar)
                        return;

                    if (MyMath.Success(25))
                        return;

                    if (!Entity.IsInBattle && Entity.TargetUID <= 0)
                    {
                        Int32 Shortest = ViewRange;
                        Player FTarget = null;

                        if (Entity.Type == 5)
                            Shortest = AtkRange;

                        foreach (Object Object in Map.Entities.Values)
                        {
                            Player Target = (Object as Player);

                            if (Target == null)
                                continue;

                            //Guard
                            if (Entity.Type == 3)
                            {
                                if (!Target.ContainsFlag(Player.Flag.Flashing))
                                    continue;
                            }

                            //Patrol
                            if (Entity.Type == 4)
                            {
                                if (!Target.ContainsFlag(Player.Flag.Flashing)
                                    && !Target.ContainsFlag(Player.Flag.BlackName))
                                    continue;
                            }

                            //Reviver
                            if (Entity.Type == 5)
                            {
                                if (Target.IsAlive())
                                    continue;
                            }

                            if (MyMath.GetDistance(Entity.X, Entity.Y, Target.X, Target.Y) == Shortest)
                            {
                                if (FTarget != null && FTarget.Level > Target.Level)
                                    continue;
                                FTarget = Target;
                                Shortest = MyMath.GetDistance(Entity.X, Entity.Y, Target.X, Target.Y);
                            }
                            else if (MyMath.GetDistance(Entity.X, Entity.Y, Target.X, Target.Y) < Shortest)
                            {
                                FTarget = Target;
                                Shortest = MyMath.GetDistance(Entity.X, Entity.Y, Target.X, Target.Y);
                            }
                            else
                                continue;
                        }
                        if (FTarget == null)
                        {
                            Entity.TargetUID = -1;
                            Target = null;
                            return;
                        }

                        Entity.TargetUID = FTarget.UniqId;
                        this.Target = FTarget;
                    }
                    else if (!Entity.IsInBattle)
                    {
                        if (Target == null)
                        {
                            Entity.TargetUID = -1;
                            return;
                        }

                        if (!IsInVisualField(Target))
                        {
                            Entity.TargetUID = -1;
                            Target = null;
                            return;
                        }

                        if (IsInAttackField(Target))
                        {
                            Entity.IsInBattle = true;
                            return;
                        }

                        if (Environment.TickCount - LastMoveTick < MoveSpeed)
                            return;

                        if (MyMath.Success(85.0) || (Entity.Type == 3 || Entity.Type == 4))
                        {
                            UInt16 NewX = Entity.X;
                            UInt16 NewY = Entity.Y;

                            Byte ToDir = (Byte)MyMath.GetDirectionCO(Entity.X, Entity.Y, Target.X, Target.Y);
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

                            if (!Map.IsValidPoint(NewX, NewY))
                                return;

                            foreach (Object Object in Map.Entities.Values)
                            {
                                Monster Monster = (Object as Monster);
                                if (Monster == null)
                                    continue;

                                if (Monster.UniqId == Entity.UniqId)
                                    continue;

                                if (Monster.X == NewX && Monster.Y == NewY)
                                    return;
                            }

                            Entity.PrevX = Entity.X;
                            Entity.PrevY = Entity.Y;

                            Entity.X = NewX;
                            Entity.Y = NewY;
                            Entity.Direction = ToDir;

                            World.BroadcastMapMsg(Entity.Map, MsgWalk.Create(Entity.UniqId, ToDir, false));
                            LastMoveTick = Environment.TickCount;
                        }
                    }
                    else
                    {
                        if (Environment.TickCount - LastAttackTick < AtkSpeed)
                            return;

                        if (!IsInVisualField(Target))
                        {
                            Entity.TargetUID = -1;
                            Target = null;
                            return;
                        }

                        if (!IsInAttackField(Target))
                        {
                            Entity.TargetUID = -1;
                            Target = null;
                            return;
                        }

                        //All
                        if (Entity.Type != 5)
                            if (!Target.IsAlive())
                            {
                                Entity.TargetUID = -1;
                                Target = null;
                                return;
                            }

                        //Guard
                        if (Entity.Type == 3)
                            if (!Target.ContainsFlag(Player.Flag.Flashing))
                            {
                                Entity.TargetUID = -1;
                                Target = null;
                                return;
                            }

                        //Patrol
                        if (Entity.Type == 4)
                            if (!Target.ContainsFlag(Player.Flag.Flashing)
                                && !Target.ContainsFlag(Player.Flag.BlackName))
                            {
                                Entity.TargetUID = -1;
                                Target = null;
                                return;
                            }

                        //Reviver
                        if (Entity.Type == 5)
                            if (Target.IsAlive())
                            {
                                Entity.TargetUID = -1;
                                Target = null;
                                return;
                            }

                        LastAttackTick = Environment.TickCount;
                        if (MyMath.Success(25))
                            return;

                        if (Entity.AtkType != 21)
                            Battle.MvP(Entity, Target);
                        else
                            Battle.Magic.Use(Entity, Target, Target.X, Target.Y);
                    }
                }
                catch (Exception Exc) { Program.WriteLine(Exc); }
            }
        }
    }
}
