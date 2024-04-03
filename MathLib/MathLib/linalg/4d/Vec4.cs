using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathLib.linalg.common;
using MathLib.linalg._2d;
using MathLib.linalg._3d;

namespace MathLib.linalg._4d
{
    public class Vec4 : VecBase
    {
        public static Vec4 Empty;
        public static Vec4 NaN;
        public static Vec4 NegativeInfinity;
        public static Vec4 PositiveInfinity;

        #region CONSTRUCTORS
        static Vec4()
        {
            Empty = new Vec4();
            NaN = new Vec4(float.NaN);
            NegativeInfinity = new Vec4(float.NegativeInfinity);
            PositiveInfinity = new Vec4(float.PositiveInfinity);
        }

        public Vec4()
        {
            m_v = new float[4];
            SetZero();
        }

        public Vec4(float all)
        {
            m_v = new float[4];
            m_v[0] = all;
            m_v[1] = all;
            m_v[2] = all;
            m_v[3] = all;
        }

        public Vec4(float x, float y, float z, float w)
        {
            m_v = new float[4];
            m_v[0] = x;
            m_v[1] = y;
            m_v[2] = z;
            m_v[3] = w;
        }

        public Vec4(Vec2 v)
        {
            m_v = new float[4];
            // By value I hope.
            m_v[0] = v[0];
            m_v[1] = v[1];
            m_v[2] = 0.0f;
            m_v[3] = 0.0f;
        }

        public Vec4(Vec3 v)
        {
            m_v = new float[4];
            // By value I hope.
            m_v[0] = v[0];
            m_v[1] = v[1];
            m_v[2] = v[2];
            m_v[3] = 0.0f;
        }

        public Vec4(Vec4 v)
        {
            m_v = new float[4];
            // By value I hope.
            m_v[0] = v.m_v[0];
            m_v[1] = v.m_v[1];
            m_v[2] = v.m_v[2];
            m_v[3] = v.m_v[3];
        }
        #endregion

        #region MISC
        public void Set(float all)
        {
            m_v[0] = all;
            m_v[1] = all;
            m_v[1] = all;
            m_v[2] = all;
        }

        public void Set(float x, float y, float z, float w)
        {
            m_v[0] = x;
            m_v[1] = y;
            m_v[2] = z;
            m_v[2] = w;
        }

        public void Set(Vec4 v)
        {
            m_v[0] = v.m_v[0];
            m_v[1] = v.m_v[1];
            m_v[2] = v.m_v[2];
            m_v[3] = v.m_v[3];
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

        public float W
        {
            get { return m_v[3]; }
            set { m_v[3] = value; }
        }
        #endregion

        #region OPERATORS
        public static Vec4 operator +(Vec4 v)
        {
            return v;
        }

        public static Vec4 operator +(Vec4 a, Vec4 b)
        {
            return new Vec4(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);
        }

        public static Vec4 operator -(Vec4 v)
        {
            return new Vec4(-v.X, -v.Y, -v.Z, -v.W);
        }

        public static Vec4 operator -(Vec4 a, Vec4 b)
        {
            return new Vec4(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);
        }

        public static Vec4 operator *(Vec4 v, float s)
        {
            return new Vec4(v.X * s, v.Y * s, v.Z * s, v.W * s);
        }

        public static Vec4 operator *(float s, Vec4 v)
        {
            return new Vec4(s * v.X, s * v.Y, s * v.Z, s * v.W);
        }

        public static Vec4 operator /(Vec4 v, float s)
        {
            return new Vec4(v.X / s, v.Y / s, v.Z / s, v.W / s);
        }
        #endregion

        #region LINEAR_ALGEBRA
        public static float Dot(Vec4 a, Vec4 b)
        {
            return VecBase.Dot(a, b);
        }

        public static float DistanceSquared(Vec4 a, Vec4 b)
        {
            return VecBase.DistanceSquared(a, b);
        }

        public static float Distance(Vec4 a, Vec4 b)
        {
            return VecBase.Distance(a, b);
        }

        public void Normalize()
        {
            float factor = 1.0f / this.Length;
            this.X *= factor;
            this.Y *= factor;
            this.Z *= factor;
            this.W *= factor;
        }

        public Vec4 GetNormalized()
        {
            return this / this.Length;
        }
        #endregion

        #region CONVERSION
        public Vec4 Copy()
        {
            return new Vec4(m_v[0], m_v[1], m_v[2], m_v[3]);
        }
        #endregion
    }
}
