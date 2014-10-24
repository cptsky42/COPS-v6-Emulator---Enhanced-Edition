// * Created by Jean-Philippe Boivin
// * Copyright © 2010
// * Logik. Project

using System;
using System.Drawing;
using System.Collections.Generic;
using CO2_CORE_DLL;

namespace COServer
{
    public partial class MyMath
    {
        private static SafeRandom Rand = new SafeRandom(Environment.TickCount);

        public const Int32 NORMAL_RANGE = 17;
        public const Int32 BIG_RANGE = 34;
        public const Int32 USERDROP_RANGE = 9;

        /// <summary>
        /// Return true if the distance between two coordinates is less than Range.
        /// </summary>
        public static Boolean CanSee(Double x1, Double y1, Double x2, Double y2, Int32 Range)
        {
            if (Math.Abs(x2 - x1) > Range || Math.Abs(y2 - y1) > Range)
                return false;
            return true;
        }

        /// <summary>
        /// Return the direction for going to the second position if you are from the first one.
        /// </summary>
        public static Int32 GetDirection(Double x1, Double y1, Double x2, Double y2)
        {
            Int32 direction = 0;

            Double DeltaX = x2 - x1;
            Double DeltaY = y2 - x1;
            Double R = (Double)Math.Atan2(DeltaY, DeltaX);

            if (R < 0)
                R += (Double)Math.PI * 2;

            direction = (Int32)(360 - (R * 180 / Math.PI));
            return direction;
        }

        /// <summary>
        /// Return the CO2 direction for going to the second position if you are from the first one.
        /// </summary>
        public static Int32 GetDirectionCO(Int32 x0, Int32 y0, Int32 x1, Int32 y1)
        {
            Int32 Dir = 0;

            Int32[] Tan = new Int32[] { -241, -41, 41, 241 };
            Int32 DeltaX = x1 - x0;
            Int32 DeltaY = y1 - y0;

            if (DeltaX == 0)
            {
                if (DeltaY > 0)
                    Dir = 0;
                else
                    Dir = 4;
            }
            else if (DeltaY == 0)
            {
                if (DeltaX > 0)
                    Dir = 6;
                else
                    Dir = 2;
            }
            else
            {
                Int32 Flag = Math.Abs(DeltaX) / DeltaX;

                DeltaY *= (100 * Flag);
                Int32 i;
                for (i = 0; i < 4; i++)
                    Tan[i] *= Math.Abs(DeltaX);

                for (i = 0; i < 3; i++)
                    if (DeltaY >= Tan[i] && DeltaY < Tan[i + 1])
                        break;

                //** note :
                //   i=0    ------- -241 -- -41
                //   i=1    ------- -41  --  41
                //   i=2    -------  41  -- 241
                //   i=3    -------  241 -- -241

                DeltaX = x1 - x0;
                DeltaY = y1 - y0;

                if (DeltaX > 0)
                {
                    if (i == 0) Dir = 5;
                    else if (i == 1) Dir = 6;
                    else if (i == 2) Dir = 7;
                    else if (i == 3)
                    {
                        if (DeltaY > 0) Dir = 0;
                        else Dir = 4;
                    }
                }
                else
                {
                    if (i == 0) Dir = 1;
                    else if (i == 1) Dir = 2;
                    else if (i == 2) Dir = 3;
                    else if (i == 3)
                    {
                        if (DeltaY > 0) Dir = 0;
                        else Dir = 4;
                    }
                }
            }

            Dir = (Dir + 8) % 8;
            return Dir;
        }

        /// <summary>
        /// Return all points on that line. (From TQ)
        /// </summary>
        public static void DDALine(int x0, int y0, int x1, int y1, int nRange, ref List<Point> vctPoint)
        {
            if (x0 == x1 && y0 == y1)
                return;

            float scale = (float)(1.0f * nRange / Math.Sqrt((x1 - x0) * (x1 - x0) + (y1 - y0) * (y1 - y0)));
            x1 = (int)(0.5f + scale * (x1 - x0) + x0);
            y1 = (int)(0.5f + scale * (y1 - y0) + y0);
            DDALineEx(x0, y0, x1, y1, ref vctPoint);
        }

        /// <summary>
        /// Return all points on that line. (From TQ)
        /// </summary>
        public static void DDALineEx(int x0, int y0, int x1, int y1, ref List<Point> vctPoint)
        {
            if (x0 == x1 && y0 == y1)
                return;
            if (vctPoint == null)
                vctPoint = new List<Point>();
            int dx = x1 - x0;
            int dy = y1 - y0;
            int abs_dx = Math.Abs(dx);
            int abs_dy = Math.Abs(dy);
            Point point = new Point();
            if (abs_dx > abs_dy)
            {
                int _0_5 = abs_dx * (dy > 0 ? 1 : -1);
                int numerator = dy * 2;
                int denominator = abs_dx * 2;
                if (dx > 0)
                {
                    // x0 ++
                    for (int i = 1; i <= abs_dx; i++)
                    {
                        point = new Point();
                        point.X = x0 + i;
                        point.Y = y0 + ((numerator * i + _0_5) / denominator);
                        vctPoint.Add(point);
                    }
                }
                else if (dx < 0)
                {
                    // x0 --
                    for (int i = 1; i <= abs_dx; i++)
                    {
                        point = new Point();
                        point.X = x0 - i;
                        point.Y = y0 + ((numerator * i + _0_5) / denominator);
                        vctPoint.Add(point);
                    }
                }
            }
            else
            {
                int _0_5 = abs_dy * (dx > 0 ? 1 : -1);
                int numerator = dx * 2;
                int denominator = abs_dy * 2;
                if (dy > 0)
                {
                    // y0 ++
                    for (int i = 1; i <= abs_dy; i++)
                    {
                        point = new Point();
                        point.Y = y0 + i;
                        point.X = x0 + ((numerator * i + _0_5) / denominator);
                        vctPoint.Add(point);
                    }
                }
                else if (dy < 0)
                {
                    // y0 -- 
                    for (int i = 1; i <= abs_dy; i++)
                    {
                        point = new Point();
                        point.Y = y0 - i;
                        point.X = x0 + ((numerator * i + _0_5) / denominator);
                        vctPoint.Add(point);
                    }
                }
            }
        }

        /// <summary>
        /// Return the distance between two coordinates. [√(∆x^2 + ∆y^2)]
        /// </summary>
        public static Int32 GetDistance(Double x1, Double y1, Double x2, Double y2)
        {
            Double Value = (Double)(((x1 - x2) * (x1 - x2)) + ((y1 - y2) * (y1 - y2)));
            return (Int32)Math.Sqrt(Value);
        }

        /// <summary>
        /// Generate a number in a specified range. (Number ∈ [Min, Max])
        /// </summary>
        public static Int32 Generate(Int32 Min, Int32 Max)
        {
            if (Max != Int32.MaxValue)
                Max++;

            Int32 Value = 0;
            /*lock (Rand) { */Value = Rand.Next(Min, Max); /*}*/
            return Value;
        }

        public static Boolean Success(Double Chance) { return ((Double)Generate(1, 1000000)) / 10000 >= 100 - Chance; }
    }
}
