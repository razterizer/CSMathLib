using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathLib.linalg.common;
using MathLib.linalg._2d;
using MathLib.linalg._4d;

namespace MathLib.linalg._3d
{
    public class Vec3 : VecBase
    {
        public static Vec3 Empty;
        public static Vec3 NaN;
        public static Vec3 NegativeInfinity;
        public static Vec3 PositiveInfinity;

        #region CONSTRUCTORS
        static Vec3()
        {
            Empty = new Vec3();
            NaN = new Vec3(float.NaN);
            NegativeInfinity = new Vec3(float.NegativeInfinity);
            PositiveInfinity = new Vec3(float.PositiveInfinity);
        }

        public Vec3()
        {
            m_v = new float[3];
            SetZero();
        }

        public Vec3(float all)
        {
            m_v = new float[3];
            m_v[0] = all;
            m_v[1] = all;
            m_v[2] = all;
        }

        public Vec3(float x, float y, float z)
        {
            m_v = new float[3];
            m_v[0] = x;
            m_v[1] = y;
            m_v[2] = z;
        }

        public Vec3(Vec2 v)
        {
            m_v = new float[3];
            // By value I hope.
            m_v[0] = v[0];
            m_v[1] = v[1];
            m_v[2] = 0.0f;
        }

        public Vec3(Vec3 v)
        {
            m_v = new float[3];
            // By value I hope.
            m_v[0] = v.m_v[0];
            m_v[1] = v.m_v[1];
            m_v[2] = v.m_v[2];
        }

        public Vec3(Vec4 v)
        {
            m_v = new float[3];
            // By value I hope.
            m_v[0] = v[0];
            m_v[1] = v[1];
            m_v[2] = v[2];
        }
        #endregion

        #region MISC
        public void Set(float all)
        {
            m_v[0] = all;
            m_v[1] = all;
            m_v[2] = all;
        }

        public void Set(float x, float y, float z)
        {
            m_v[0] = x;
            m_v[1] = y;
            m_v[2] = z;
        }

        public void Set(Vec3 v)
        {
            m_v[0] = v.m_v[0];
            m_v[1] = v.m_v[1];
            m_v[2] = v.m_v[2];
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

        public float Z
        {
            get { return m_v[2]; }
            set { m_v[2] = value; }
        }
        #endregion

        #region OPERATORS
        public static Vec3 operator +(Vec3 v)
        {
            return v;
        }

        public static Vec3 operator +(Vec3 a, Vec3 b)
        {
            return new Vec3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Vec3 operator -(Vec3 v)
        {
            return new Vec3(-v.X, -v.Y, -v.Z);
        }

        public static Vec3 operator -(Vec3 a, Vec3 b)
        {
            return new Vec3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Vec3 operator *(Vec3 v, float s)
        {
            return new Vec3(v.X * s, v.Y * s, v.Z * s);
        }

        public static Vec3 operator *(float s, Vec3 v)
        {
            return new Vec3(s * v.X, s * v.Y, s * v.Z);
        }

        public static Vec3 operator /(Vec3 v, float s)
        {
            return new Vec3(v.X / s, v.Y / s, v.Z / s);
        }
        #endregion

        #region LINEAR_ALGEBRA
        public static float Dot(Vec3 a, Vec3 b)
        {
            return VecBase.Dot(a, b);
        }

        public static Vec3 Cross(Vec3 a, Vec3 b)
        {
            //| x  y  z|
            //|ax ay az|
            //|bx by bz|
            //=
            //(ay*bz - by*az, az*bx - bz*ax, ax*by - bx*ay)

            return new Vec3(
                a.Y * b.Z - b.Y * a.Z,
                a.Z * b.X - b.Z * a.X,
                a.X * b.Y - b.X * a.Y);
        }

        public static Vec3 Cross(Vec3 a, Vec2 b)
        {
            //| x  y  z|
            //|ax ay az|
            //|bx by bz|
            //=
            //(ay*bz - by*az, az*bx - bz*ax, ax*by - bx*ay)

            return new Vec3(
                -b.Y * a.Z,
                +a.Z * b.X,
                a.X * b.Y - b.X * a.Y);
        }

        public static Vec3 Cross(Vec2 a, Vec3 b)
        {
            //| x  y  z|
            //|ax ay az|
            //|bx by bz|
            //=
            //(ay*bz - by*az, az*bx - bz*ax, ax*by - bx*ay)

            return new Vec3(
                +a.Y * b.Z,
                -b.Z * a.X,
                a.X * b.Y - b.X * a.Y);
        }

        public static float DistanceSquared(Vec3 a, Vec3 b)
        {
            return VecBase.DistanceSquared(a, b);
        }

        public static float Distance(Vec3 a, Vec3 b)
        {
            return VecBase.Distance(a, b);
        }

        public void Normalize()
        {
            float factor = 1.0f / this.Length;
            this.X *= factor;
            this.Y *= factor;
            this.Z *= factor;
        }

        public Vec3 GetNormalized()
        {
            return this / this.Length;
        }
        #endregion

        #region CONVERSION
        public Vec3 Copy()
        {
            return new Vec3(m_v[0], m_v[1], m_v[2]);
        }
        #endregion
    }
}
