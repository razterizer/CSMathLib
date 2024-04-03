using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace MathLib.linalg._2d
{
    public class Mtx2
    {
        Vec2[] m_v; // Column vectors.

        public Mtx2()
        {
            m_v = new Vec2[2];
            SetIdentity();
        }

        public Mtx2(Vec2 col0, Vec2 col1)
        {
            m_v = new Vec2[2];
            // INVESTIGATE
            // Is this really by value?
            m_v[0] = col0;
            m_v[1] = col1;
        }

        public Mtx2(float xx, float xy, float yx, float yy)
        {
            m_v = new Vec2[2];
            m_v[0] = new Vec2(xx, yx);
            m_v[1] = new Vec2(xy, yy);
        }

        public void SetIdentity()
        {
            m_v[0] = new Vec2(1.0f, 0.0f);
            m_v[1] = new Vec2(0.0f, 1.0f);
        }

        public static Mtx2 Rotate(float angle_rad)
        {
            return new Mtx2(
                +(float)Math.Cos(angle_rad), -(float)Math.Sin(angle_rad),
                +(float)Math.Sin(angle_rad), +(float)Math.Cos(angle_rad));
        }

        public float this[int r, int c]
        {
            get
            {
                if (c < 0 || c >= 2)
                    throw new IndexOutOfRangeException();
                return m_v[c][r];
            }
            set
            {
                if (c < 0 || c >= 2)
                    throw new IndexOutOfRangeException();
                m_v[c][r] = value;
            }
        }

        public Vec2 Transform(Vec2 v)
        {
            return new Vec2(
                m_v[0].X * v.X + m_v[1].X * v.Y,
                m_v[0].Y * v.X + m_v[1].Y * v.Y);
        }

        public Mtx2 Transpose()
        {
            return new Mtx2(m_v[0][0], m_v[0][1], m_v[1][0], m_v[1][1]);
        }

        public override string ToString()
        {
            string xx_str = m_v[0][0].ToString(NumberFormatInfo.InvariantInfo);
            string xy_str = m_v[1][0].ToString(NumberFormatInfo.InvariantInfo);
            string yx_str = m_v[0][1].ToString(NumberFormatInfo.InvariantInfo);
            string yy_str = m_v[1][1].ToString(NumberFormatInfo.InvariantInfo);

            int max_str_len_xcol = 0;
            max_str_len_xcol = Math.Max(max_str_len_xcol, xx_str.Length);
            max_str_len_xcol = Math.Max(max_str_len_xcol, yx_str.Length);

            int max_str_len_ycol = 0;
            max_str_len_ycol = Math.Max(max_str_len_ycol, xy_str.Length);
            max_str_len_ycol = Math.Max(max_str_len_ycol, yy_str.Length);

            return
                "[" + xx_str.PadRight(max_str_len_xcol) + " " + xy_str.PadRight(max_str_len_ycol) + "]\n" +
                "[" + yx_str.PadRight(max_str_len_xcol) + " " + yy_str.PadRight(max_str_len_ycol) + "]";
        }
    }
}
