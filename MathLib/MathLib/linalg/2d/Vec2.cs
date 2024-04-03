using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathLib.linalg.common;
using MathLib.linalg._3d;
using MathLib.linalg._4d;

namespace MathLib.linalg._2d
{
    public class Vec2 : VecBase
    {
        public static Vec2 Empty;
        public static Vec2 NaN;
        public static Vec2 NegativeInfinity;
        public static Vec2 PositiveInfinity;

        #region CONSTRUCTORS
        static Vec2()
        {
            Empty = new Vec2();
            NaN = new Vec2(float.NaN);
            NegativeInfinity = new Vec2(float.NegativeInfinity);
            PositiveInfinity = new Vec2(float.PositiveInfinity);
        }

        public Vec2()
        {
            m_v = new float[2];
            SetZero();
        }

        public Vec2(float all)
        {
            m_v = new float[2];
            m_v[0] = all;
            m_v[1] = all;
        }

        public Vec2(float x, float y)
        {
            m_v = new float[2];
            m_v[0] = x;
            m_v[1] = y;
        }

        public Vec2(Vec2 v)
        {
            m_v = new float[2];
            // By value I hope.
            m_v[0] = v.m_v[0];
            m_v[1] = v.m_v[1];
        }

        public Vec2(Vec3 v)
        {
            m_v = new float[2];
            // By value I hope.
            m_v[0] = v[0];
            m_v[1] = v[1];
        }

        public Vec2(Vec4 v)
        {
            m_v = new float[2];
            // By value I hope.
            m_v[0] = v[0];
            m_v[1] = v[1];
        }

        public Vec2(complex.Complex z)
        {
            m_v = new float[2];
            m_v[0] = z.Real;
            m_v[1] = z.Imaginary;
        }
        #endregion

        #region MISC
        public void Set(float all)
        {
            m_v[0] = all;
            m_v[1] = all;
        }

        public void Set(float x, float y)
        {
            m_v[0] = x;
            m_v[1] = y;
        }

        public void Set(Vec2 v)
        {
            m_v[0] = v.m_v[0];
            m_v[1] = v.m_v[1];
        }
        #endregion

        #region PROPERTIES
        public float X
        {
            get { return m_v[0]; }
            set { m_v[0] = value; }
        }

        public float Y
        {
            get { return m_v[1]; }
            set { m_v[1] = value; }
        }
        #endregion

        #region OPERATORS
        public static Vec2 operator +(Vec2 v)
        {
            return v;
        }

        public static Vec2 operator +(Vec2 a, Vec2 b)
        {
            return new Vec2(a.X + b.X, a.Y + b.Y);
        }

        public static Vec2 operator -(Vec2 v)
        {
            return new Vec2(-v.X, -v.Y);
        }

        public static Vec2 operator -(Vec2 a, Vec2 b)
        {
            return new Vec2(a.X - b.X, a.Y - b.Y);
        }

        public static Vec2 operator *(Vec2 v, float s)
        {
            return new Vec2(v.X * s, v.Y * s);
        }

        public static Vec2 operator *(float s, Vec2 v)
        {
            return new Vec2(s * v.X, s * v.Y);
        }

        public static Vec2 operator /(Vec2 v, float s)
        {
            return new Vec2(v.X / s, v.Y / s);
        }
        #endregion

        #region LINEAR_ALGEBRA
        public static float Dot(Vec2 a, Vec2 b)
        {
            return VecBase.Dot(a, b);
        }

        public static Vec3 Cross(Vec2 a, Vec2 b)
        {
            //| x  y  z|
            //|ax ay  0|
            //|bx by  0|
            //=
            //(0, 0, ax*by - bx*ay)

            return new Vec3(0.0f, 0.0f, a.X * b.Y - b.X * a.Y);
        }

        public static float RotDir(Vec2 a, Vec2 b)
        {
            //| x  y  z|
            //|ax ay  0|
            //|bx by  0|
            //=
            //(0, 0, ax*by - bx*ay)

            return a.X * b.Y - b.X * a.Y;
        }

        public static float DistanceSquared(Vec2 a, Vec2 b)
        {
            return VecBase.DistanceSquared(a, b);
        }

        public static float Distance(Vec2 a, Vec2 b)
        {
            return VecBase.Distance(a, b);
        }

        public void Normalize()
        {
            float factor = 1.0f / this.Length;
            this.X *= factor;
            this.Y *= factor;
        }

        public Vec2 GetNormalized()
        {
            return this / this.Length;
        }
        #endregion

        #region CONVERSION
        public Vec2 Copy()
        {
            return new Vec2(m_v[0], m_v[1]);
        }

        public System.Drawing.PointF ToPointF(float y_height)
        {
            return new System.Drawing.PointF(m_v[0], y_height - m_v[1]);
        }
        #endregion
    }
}
