// * Created by Jean-Philippe Boivin
// * Copyright © 2012
// * COPS v6 Emulator

using System;
using COServer.Entities;

namespace COServer
{
    public partial class MyMath
    {
        public struct Sector
        {
            private UInt16 mAttackerX, mAttackerY;
            private UInt16 mAttackX, mAttackY;

            private bool mAddExtra;

            private int mDegree, mLeftSide, mRightSide;
            private int mDistance;

            public Sector(Entity aAttacker, UInt16 aAttackX, UInt16 aAttackY)
            {
                mAttackerX = aAttacker.X;
                mAttackerY = aAttacker.Y;
                mAttackX = aAttackX;
                mAttackY = aAttackY;

                mDegree = MyMath.GetDirection(mAttackerX, mAttackX, mAttackerY, mAttackY);
                mLeftSide = 0; mRightSide = 0;
                mDistance = 0;

                mAddExtra = false;
            }

            public void Arrange(int aSectorSize, int aDistance)
            {
                mDistance = Math.Min(aDistance, 14);

                mLeftSide = mDegree - (aSectorSize / 2);
                if (mLeftSide < 0)
                    mLeftSide += 360;

                mRightSide = mDegree + (aSectorSize / 2);
                if (mLeftSide < mRightSide || mRightSide - mLeftSide != aSectorSize)
                {
                    mRightSide += 360;
                    mAddExtra = true;
                }
            }


            public bool Inside(UInt16 aPosX, UInt16 aPosY)
            {
                if (MyMath.GetDistance(aPosX, aPosY, mAttackerX, mAttackerY) <= mDistance)
                {
                    int degree = MyMath.GetDirection(mAttackerX, aPosX, mAttackerY, aPosY);
                    
                    if (mAddExtra)
                        degree += 360;
                    
                    if (degree >= mLeftSide && degree <= mRightSide)
                        return true;
                }
                return false;
            }
        }
    }
}
