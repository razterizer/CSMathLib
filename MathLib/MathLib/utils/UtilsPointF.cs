using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MathLib.utils
{
    public class UtilsPointF
    {
        public static float DistanceSquared(PointF pA, PointF pB)
        {
            return LengthSquared(Minus(pA, pB));
        }

        public static float Distance(PointF pA, PointF pB)
        {
            return Length(Minus(pA, pB));
        }

        public static float PointLineSegDistanceSquared(PointF p, PointF pSegA, PointF pSegB)
        {
            PointF v1 = Minus(p, pSegA);
            PointF v2 = Minus(pSegB, pSegA);
            float t = Dot(v1, v2) / LengthSquared(v2);
            if (t <= 0.0f)
                return DistanceSquared(p, pSegA);
            else if (t >= 1.0f)
                return DistanceSquared(p, pSegB);
            else
            {
                PointF p_proj = Lerp(t, pSegA, pSegB);
                return DistanceSquared(p, p_proj);
            }
        }

        public static float PointLineSegDistance(PointF p, PointF pSegA, PointF pSegB)
        {
            return (float)Math.Sqrt(PointLineSegDistanceSquared(p, pSegA, pSegB));
        }

        public static float Dot(PointF vecA, PointF vecB)
        {
            return vecA.X * vecB.X + vecA.Y * vecB.Y;
        }

        public static PointF Plus(PointF pA, PointF pB)
        {
            return new PointF(pA.X + pB.X, pA.Y + pB.Y);
        }

        public static PointF Minus(PointF pA, PointF pB)
        {
            return new PointF(pA.X - pB.X, pA.Y - pB.Y);
        }

        public static PointF Mul(PointF pA, float s)
        {
            return new PointF(pA.X * s, pA.Y * s);
        }

        public static float LengthSquared(PointF v)
        {
            return Dot(v, v);
        }

        public static float Length(PointF v)
        {
            return (float)Math.Sqrt(LengthSquared(v));
        }

        public static PointF Normalize(PointF v)
        {
            float length = Length(v);
            return new PointF(v.X / length, v.Y / length);
        }

        public static PointF Lerp(float t, PointF pA, PointF pB)
        {
            return Plus(Mul(pA, (1.0f - t)), Mul(pB, t));
        }
    }
}
