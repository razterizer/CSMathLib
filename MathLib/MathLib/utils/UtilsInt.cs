using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathLib.utils
{
    class UtilsInt
    {
        /// <summary>
        /// Greatest Common Divisor using Euclids algorithm.
        /// </summary>
        /// <param name="a">Natural number a</param>
        /// <param name="b">Natural number b</param>
        /// <returns>The biggest prime factor that divides both a and b.</returns>
        public static int GreatestCommonDivisor(int a, int b)
        {
            if (a == b)
                return a;
            else if (a == 0)
                return b;
            else if (b == 0)
                return a;
            else if (a > b)
                return GreatestCommonDivisor(a - b, b);
            else if (a < b)
                return GreatestCommonDivisor(a, b - a);
            else
                return 0;
        }

        public static int Factorial(int n)
        {
            if (n == 0 || n == 1)
                return 1;
            else
                return n * Factorial(n - 1);
        }

        public static int QFactorial(int n)
        {
            int res = 1;
            for (int i = 1; i <= n; i++)
                res *= i;
            return res;
        }

        public static int Fibonacci(int n)
        {
            if (n == 0 || n == 1)
                return n;
            else
                return Fibonacci(n - 2) + Fibonacci(n - 1);
        }

        public static int QFibonacci(int n)
        {
            if (n == 0 || n == 1)
                return n;

            int Fn_2 = 0; // F(n-2) = F(0) = 0
            int Fn_1 = 1; // F(n-1) = F(1) = 1
            int Fn = 0;
            for (int i = 2; i <= n; i++)
            {
                Fn = Fn_2 + Fn_1;
                Fn_2 = Fn_1;
                Fn_1 = Fn;
            }
            return Fn;
        }

        // Svår!!!
        // Primtalsfaktorisering.
        //public static void Factorize(int n, List<int> prime_factors)
        //{
        //}
    }
}
