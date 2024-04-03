using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using MathLib.utils;

namespace MathLib.linalg.common
{
    public class VecBase
    {
        protected float[] m_v;

        public enum PrintFormat { RowVector, ColumnVector };

        #region CONSTRUCTORS
        //public VecBase()
        //{
        //}

        //protected VecBase(VecBase v)
        //{
        //    this.m_v = new float[v.m_v.Length];
        //    for (int v_idx = 0; v_idx < v.m_v.Length; v_idx++)
        //        this.m_v[v_idx] = v.m_v[v_idx];
        //}
        #endregion

        public void SetZero()
        {
            for (int v_idx = 0; v_idx < m_v.Length; v_idx++)
                m_v[v_idx] = 0.0f;
        }

        public float this[int index]
        {
            get
            {
                if (index < 0 || index >= m_v.Length)
                    throw new IndexOutOfRangeException();
                return m_v[index];
            }
            set
            {
                if (index < 0 || index >= m_v.Length)
                    throw new IndexOutOfRangeException();
                m_v[index] = value;
            }
        }

        #region UTILS
        public void Clamp(float l, float u)
        {
            for (int v_idx = 0; v_idx < m_v.Length; v_idx++)
                UtilsFloat.ClampSet(ref m_v[v_idx], l, u);
        }
        #endregion


        // Silly casting mechanics in C# prohibits me from using these operators.
        #region OPERATORS
        //public static VecBase operator -(VecBase v)
        //{
        //    int size = v.m_v.Length;
        //    VecBase result = new VecBase();
        //    result.m_v = new float[size];
        //    for (int v_idx = 0; v_idx < size; v_idx++)
        //        result.m_v[v_idx] = -v.m_v[v_idx];
        //    return result;
        //}

        //public static VecBase operator +(VecBase a, VecBase b)
        //{
        //    int size = a.m_v.Length;
        //    VecBase result = new VecBase();
        //    result.m_v = new float[size];
        //    for (int v_idx = 0; v_idx < size; v_idx++)
        //        result.m_v[v_idx] = a.m_v[v_idx] + b.m_v[v_idx];
        //    return result;
        //}

        //public static VecBase operator -(VecBase a, VecBase b)
        //{
        //    int size = a.m_v.Length;
        //    VecBase result = new VecBase();
        //    result.m_v = new float[size];
        //    for (int v_idx = 0; v_idx < size; v_idx++)
        //        result.m_v[v_idx] = a.m_v[v_idx] - b.m_v[v_idx];
        //    return result;
        //}

        //public static VecBase operator *(VecBase v, float s)
        //{
        //    int size = v.m_v.Length;
        //    VecBase result = new VecBase();
        //    result.m_v = new float[size];
        //    for (int v_idx = 0; v_idx < size; v_idx++)
        //        result.m_v[v_idx] = v.m_v[v_idx] * s;
        //    return result;
        //}

        //public static VecBase operator *(float s, VecBase v)
        //{
        //    return v * s;
        //}

        //public static VecBase operator /(VecBase v, float s)
        //{
        //    int size = v.m_v.Length;
        //    VecBase result = new VecBase();
        //    result.m_v = new float[size];
        //    for (int v_idx = 0; v_idx < size; v_idx++)
        //        result.m_v[v_idx] = v.m_v[v_idx] / s;
        //    return result;
        //}
        #endregion

        #region LINEAR_ALGEBRA
        protected static float Dot(VecBase a, VecBase b)
        {
            int size = a.m_v.Length;
            float dot_prod = 0.0f;
            for (int v_idx = 0; v_idx < size; v_idx++)
                dot_prod += a.m_v[v_idx] * b.m_v[v_idx];
            return dot_prod;
        }

        protected static float DistanceSquared(VecBase a, VecBase b)
        {
            float dist_sq = 0.0f;
            float diff;
            int size = a.m_v.Length;
            for (int v_idx = 0; v_idx < size; v_idx++)
            {
                diff = a.m_v[v_idx] - b.m_v[v_idx];
                dist_sq += diff * diff;
            }
            return dist_sq;
        }

        protected static float Distance(VecBase a, VecBase b)
        {
            return (float)Math.Sqrt(DistanceSquared(a, b));
        }
        #endregion

        #region PROPERTIES
        public float LengthSquared
        {
            get
            {
                // Dot with self.
                float proj_comp = 0.0f;
                foreach (float v in m_v)
                    proj_comp += v * v;
                return proj_comp;
            }
        }

        public float Length
        {
            get { return (float)Math.Sqrt(this.LengthSquared); }
        }

        public bool IsAnyNaN
        {
            get { return is_any_NaN(m_v); }
        }

        public bool IsAnyInfinity
        {
            get { return is_any_Inf(m_v); }
        }

        public bool IsAnyNegativeInfinity
        {
            get { return is_any_NegInf(m_v); }
        }

        public bool IsAnyPositiveInfinity
        {
            get { return is_any_PosInf(m_v); }
        }
        #endregion

        #region TO_STRING
        public override string ToString()
        {
            return row_string();
        }

        public string ToString(PrintFormat format)
        {
            switch (format)
            {
                case PrintFormat.ColumnVector:
                    return column_string();
                case PrintFormat.RowVector:
                    return row_string();
            }
            return null;
        }

        private string column_string()
        {
            int max_str_len = 0;
            string[] val_strings = new string[m_v.Length];
            for (int v_idx = 0; v_idx < m_v.Length; v_idx++)
            {
                val_strings[v_idx] = m_v[v_idx].ToString(NumberFormatInfo.InvariantInfo);
                max_str_len = Math.Max(max_str_len, val_strings[v_idx].Length);
            }

            string vec_str = "";
            foreach (string v_str in val_strings)
                vec_str += "[" + v_str.PadRight(max_str_len) + "]\n";
            vec_str = vec_str.Remove(vec_str.Length - 1);
            return vec_str;
        }

        private string row_string()
        {
            string vec_str = "[";
            for (int v_idx = 0; v_idx < m_v.Length - 1; v_idx++)
                vec_str += m_v[v_idx].ToString(NumberFormatInfo.InvariantInfo) + ", ";
            vec_str += m_v.Last().ToString(NumberFormatInfo.InvariantInfo) + "]";

            return vec_str;
        }
        #endregion

        #region TESTS
        private static bool is_any_NaN(float[] vec)
        {
            foreach (float v in vec)
                if (float.IsNaN(v))
                    return true;
            return false;
        }

        private static bool is_any_Inf(float[] vec)
        {
            foreach (float v in vec)
                if (float.IsInfinity(v))
                    return true;
            return false;
        }

        private static bool is_any_NegInf(float[] vec)
        {
            foreach (float v in vec)
                if (float.IsNegativeInfinity(v))
                    return true;
            return false;
        }

        private static bool is_any_PosInf(float[] vec)
        {
            foreach (float v in vec)
                if (float.IsPositiveInfinity(v))
                    return true;
            return false;
        }
        #endregion
    }
}
