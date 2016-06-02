// * Created by Jean-Philippe Boivin
// * Copyright © 2014
// * COPS v6 Emulator

using System;
using System.Timers;

namespace COServer.Entities
{
    public abstract class AI
    {
        protected Monster mEntity;

        protected Timer MotorSystem;

        protected Int32 mThinkSpeed;
        protected Int32 mMoveSpeed;
        protected Int32 mAtkSpeed;

        protected Int32 mViewRange;
        protected Int32 mMoveRange;
        protected Int32 mAtkRange;

        public AI(Monster aEntity, Int32 aThinkSpeed, Int32 aMoveSpeed, Int32 aAtkSpeed, Int32 aViewRange, Int32 aMoveRange, Int32 aAtkRange)
        {
            mEntity = aEntity;

            mThinkSpeed = aThinkSpeed;
            mMoveSpeed = aMoveSpeed;
            mAtkSpeed = aAtkSpeed + 1000;

            mViewRange = aViewRange;
            if (mViewRange > 15)
                mViewRange = 15;
            mMoveRange = aMoveRange;
            mAtkRange = aAtkRange;
        }

        public abstract void Reset();
        public abstract void Sleep();
        public abstract void Awake();

        public Boolean IsInVisualField(AdvancedEntity aTarget)
        {
            if (aTarget == null)
                return false;

            if (mEntity.Map != aTarget.Map)
                return false;

            if (!MyMath.CanSee(aTarget.X, aTarget.Y, mEntity.X, mEntity.Y, mViewRange))
                return false;

            return true;
        }

        public Boolean IsInVisualField(UInt32 MapId, UInt16 X, UInt16 Y)
        {
            if (mEntity.Map.Id != MapId)
                return false;

            if (!MyMath.CanSee(X, Y, mEntity.X, mEntity.Y, mViewRange))
                return false;
            return true;
        }

        public Boolean IsInAttackField(AdvancedEntity aTarget)
        {
            if (aTarget == null)
                return false;

            if (mEntity.Map != aTarget.Map)
                return false;

            if (!MyMath.CanSee(aTarget.X, aTarget.Y, mEntity.X, mEntity.Y, mAtkRange))
                return false;
            return true;
        }

        public Boolean IsInAttackField(UInt32 aMapId, UInt16 aX, UInt16 aY)
        {
            if (mEntity.Map.Id !=  aMapId)
                return false;

            if (!MyMath.CanSee(aX, aY, mEntity.X, mEntity.Y, mAtkRange))
                return false;
            return true;
        }
    }
}
