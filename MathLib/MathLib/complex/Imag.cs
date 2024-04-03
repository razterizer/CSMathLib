using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathLib.complex
{
    public class Imag
    {
        #region OPERATORS
        #region ADDITION
        public static Imag operator +(Imag i)
        {
            return i;
        }
        
        public static Complex operator +(Imag iA, Imag iB)
        {
            return new Complex(0.0f, 2.0f);
        }

        public static Complex operator +(float r, Imag i)
        {
            return new Complex(r, 1.0f);
        }

        public static Complex operator +(Imag i, float r)
        {
            return new Complex(r, 1.0f);
        }
        #endregion

        #region SUBTRACTION
        public static Complex operator -(Imag i)
        {
            return new Complex(0.0f, -1.0f);
        }

        public static float operator -(Imag iA, Imag iB)
        {
            return 0.0f;
        }

        public static Complex operator -(float r, Imag i)
        {
            return new Complex(r, -1.0f);
        }

        public static Complex operator -(Imag i, float r)
        {
            return new Complex(-r, 1.0f);
        }
        #endregion

        #region MULTIPLICATION
        public static float operator *(Imag iA, Imag iB)
        {
            return -1.0f;
        }

        public static Complex operator *(float r, Imag i)
        {
            return new Complex(0.0f, r);
        }

        public static Complex operator *(Imag i, float r)
        {
            return new Complex(0.0f, r);
        }
        #endregion

        #region DIVISION
        public static float operator /(Imag iA, Imag iB)
        {
            return 1.0f;
        }

        public static Complex operator /(float r, Imag i)
        {
            return new Complex(0.0f, -r);
        }

        public static Complex operator /(Imag i, float r)
        {
            return new Complex(0.0f, 1.0f / r);
        }
        #endregion
        #endregion
    }
}
