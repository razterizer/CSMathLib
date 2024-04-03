using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace MathLib.complex
{
    public class Complex
    {
        public enum PrintFormat { Normal, PolarRad, PolarDeg };

        public static Complex Empty;
        public static Complex NaN;
        public static Complex NegativeInfinity;
        public static Complex PositiveInfinity;

        #region CONSTRUCTORS
        static Complex()
        {
            Empty = new Complex();
            NaN = new Complex(float.NaN, float.NaN);
            NegativeInfinity = new Complex(float.NegativeInfinity, float.NegativeInfinity);
            PositiveInfinity = new Complex(float.PositiveInfinity, float.PositiveInfinity);
        }

        public Complex()
        {
            this.SetZero();
        }

        public Complex(float real)
        {
            this.Real = real;
            this.Imaginary = 0.0f;
        }

        public Complex(float real, float imag)
        {
            this.Real = real;
            this.Imaginary = imag;
        }

        public Complex(linalg._2d.Vec2 v)
        {
            this.Real      = v.X;
            this.Imaginary = v.Y;
        }
        #endregion

        public void SetZero()
        {
            this.Real = 0.0f;
            this.Imaginary = 0.0f;
        }

        #region DATA
        public float Real
        {
            get;
            set;
        }

        public float Imaginary
        {
            get;
            set;
        }
        #endregion

        #region OPERATORS
        #region ADDITION
        public static Complex operator +(Complex z)
        {
            return z;
        }

        public static Complex operator +(float r, Complex z)
        {
            return new Complex(r + z.Real, z.Imaginary);
        }

        public static Complex operator +(Complex z, float r)
        {
            return new Complex(z.Real + r, z.Imaginary);
        }

        public static Complex operator +(Imag i, Complex z)
        {
            return new Complex(z.Real, 1.0f + z.Imaginary);
        }

        public static Complex operator +(Complex z, Imag i)
        {
            return new Complex(z.Real, z.Imaginary + 1.0f);
        }

        public static Complex operator +(Complex zA, Complex zB)
        {
            return new Complex(zA.Real + zB.Real, zA.Imaginary + zB.Imaginary);
        }
        #endregion

        #region SUBTRACTION
        public static Complex operator -(Complex z)
        {
            return new Complex(-z.Real, -z.Imaginary);
        }

        public static Complex operator -(float r, Complex z)
        {
            return new Complex(r - z.Real, z.Imaginary);
        }

        public static Complex operator -(Complex z, float r)
        {
            return new Complex(z.Real - r, z.Imaginary);
        }

        public static Complex operator -(Imag i, Complex z)
        {
            return new Complex(z.Real, 1.0f - z.Imaginary);
        }

        public static Complex operator -(Complex z, Imag i)
        {
            return new Complex(z.Real, z.Imaginary - 1.0f);
        }

        public static Complex operator -(Complex zA, Complex zB)
        {
            return new Complex(zA.Real - zB.Real, zA.Imaginary - zB.Imaginary);
        }
        #endregion

        #region MULTIPLICATION
        public static Complex operator *(float s, Complex z)
        {
            return new Complex(s * z.Real, s * z.Imaginary);
        }

        public static Complex operator *(Complex z, float s)
        {
            return new Complex(z.Real * s, z.Imaginary * s);
        }

        public static Complex operator *(Imag i, Complex z)
        {
            return new Complex(-z.Imaginary, z.Real);
        }

        public static Complex operator *(Complex z, Imag i)
        {
            return new Complex(-z.Imaginary, z.Real);
        }

        public static Complex operator *(Complex zA, Complex zB)
        {
            // (rA + iA)(rB + iB) = rA*rB + rA*iB + iA*rB + iA*iB
            return new Complex(
                zA.Real * zB.Real - zA.Imaginary * zB.Imaginary, 
                zA.Imaginary * zB.Real + zA.Real * zB.Imaginary);
        }
        #endregion

        #region DIVISION
        public static Complex operator /(float r, Complex z)
        {
            Complex conj = z.Conj();
            return r * conj / z.Norm();
        }

        public static Complex operator /(Complex z, float r)
        {
            return new Complex(z.Real / r, z.Imaginary / r);
        }

        public static Complex operator /(Imag i, Complex z)
        {
            return i * (1.0f / z);
        }

        public static Complex operator /(Complex z, Imag i)
        {
            return new Complex(z.Imaginary, -z.Real);
        }

        public static Complex operator /(Complex zA, Complex zB)
        {
            return zA * zB.Conj() / zB.Norm();
        }
        #endregion

        public static implicit operator Complex(float r)
        {
            return new Complex(r);
        }

        public static implicit operator Complex(Imag i)
        {
            return new Complex(0.0f, 1.0f);
        }
        #endregion

        #region METHODS
        public Complex Conj()
        {
            return new Complex(this.Real, -this.Imaginary);
        }

        public float Norm()
        {
            // = z * z.Conj()
            return this.Real * this.Real + this.Imaginary * this.Imaginary;
        }

        public float Abs()
        {
            return (float)Math.Sqrt(this.Norm());
        }

        public float Arg()
        {
            return (float)Math.Atan2(this.Imaginary, this.Real);
        }

        public static Complex Exp(Complex z)
        {
            if (z.IsAnyNaN)
                return NaN;
            if (z.Real == float.NegativeInfinity)
                return 0.0f;
            //if (z.Real == float.PositiveInfinity) // Need to check the imaginary angle for signs.
            //    return new Complex(float.PositiveInfinity, float.PositiveInfinity);
            if (float.IsInfinity(z.Imaginary))
                return NaN;
            if (z.Imaginary == 0.0f)
                return (float)Math.Exp(z.Real);
            if (z.Real == 0.0f)
                return new Complex((float)Math.Cos(z.Imaginary), (float)Math.Sin(z.Imaginary));

            return (float)Math.Exp(z.Real) * 
                new Complex((float)Math.Cos(z.Imaginary), (float)Math.Sin(z.Imaginary)); 
        }

        public static Complex Log(Complex z)
        {
            if (z.IsAnyNaN)
                return NaN;
            //...

            return new Complex((float)Math.Log(z.Abs()), z.Arg());
        }

        public static Complex Cos(Complex z)
        {
            if (z.IsAnyNaN)
                return NaN;
            //...

            return new Complex(
                 (float)(Math.Cos(z.Real) * Math.Cosh(z.Imaginary)),
                -(float)(Math.Sin(z.Real) * Math.Sinh(z.Imaginary)));
        }

        public static Complex Sin(Complex z)
        {
            if (z.IsAnyNaN)
                return NaN;
            //...

            return new Complex(
                (float)(Math.Sin(z.Real) * Math.Cosh(z.Imaginary)),
                (float)(Math.Cos(z.Real) * Math.Sinh(z.Imaginary)));
        }

        public static float Re(Complex z)
        {
            return z.Real;
        }

        public static float Im(Complex z)
        {
            return z.Imaginary;
        }

        public void Normalize()
        {
            float factor = 1.0f / this.Abs();
            this.Real *= factor;
            this.Imaginary *= factor;
        }

        public Complex GetNormalized()
        {
            return this / this.Abs();
        }

        public void FromPolar(float radius, float angle)
        {
            this.Real      = radius * (float)Math.Cos(angle);
            this.Imaginary = radius * (float)Math.Sin(angle);
        }

        public void ToPolar(out float radius, out float angle)
        {
            radius = this.Abs();
            angle = this.Arg();
        }
        #endregion

        #region TO_STRING
        public override string ToString()
        {
            return complex_string();
        }

        public string ToString(PrintFormat format)
        {
            switch (format)
            {
                case PrintFormat.Normal:
                    return complex_string();
                case PrintFormat.PolarRad:
                    return polar_rad_string();
                case PrintFormat.PolarDeg:
                    return polar_deg_string();
            }
            return null;
        }

        private string complex_string()
        {
            string str = this.Real.ToString(NumberFormatInfo.InvariantInfo);
            str += Math.Sign(this.Imaginary) < 0 ? " - " : " + ";
            str += Math.Abs(this.Imaginary).ToString(NumberFormatInfo.InvariantInfo) + "i";
            return str;
        }

        private string polar_rad_string()
        {
            string str = this.Abs().ToString(NumberFormatInfo.InvariantInfo);
            str += " | ";
            str += this.Arg().ToString(NumberFormatInfo.InvariantInfo);
            return str;
        }

        private string polar_deg_string()
        {
            string str = this.Abs().ToString(NumberFormatInfo.InvariantInfo);
            str += " | ";
            str += (this.Arg() * 180.0 / Math.PI).ToString(NumberFormatInfo.InvariantInfo);
            str += " o";
            return str;
        }
        #endregion

        #region TESTS
        public bool IsAnyNaN
        {
            get { return float.IsNaN(this.Real) || float.IsNaN(this.Imaginary); }
        }

        public bool IsAnyInfinty
        {
            get { return float.IsInfinity(this.Real) || float.IsInfinity(this.Imaginary); }
        }

        public bool IsAnyNegativeInfinity
        {
            get { return float.IsNegativeInfinity(this.Real) || float.IsNegativeInfinity(this.Imaginary); }
        }

        public bool IsAnyPositiveInfinity
        {
            get { return float.IsPositiveInfinity(this.Real) || float.IsPositiveInfinity(this.Imaginary); }
        }
        #endregion
    }
}
