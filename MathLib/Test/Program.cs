using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathLib.geometry._2d;
using MathLib.linalg._2d;
using MathLib.utils;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            AABB aabb = new AABB();
            Vec2 vA = new Vec2(-1.0f, 3.0f);
            Vec2 vB = new Vec2(1.0f, 2.0f);
            Vec2 vR = vA - vB;
            Console.WriteLine("vR = {0}", vR);
            Console.WriteLine("dot = {0}", Vec2.Dot(vA, vB));

            Random rand = new Random();

            float flt_val_0 = 2.0f * (float)rand.NextDouble() - 1.0f;
            string flt_hex = Conversion.Float2Hex(flt_val_0);
            float flt_val_1 = Conversion.Hex2Float(flt_hex);
            Console.WriteLine("flt_0 = {0}, hex = {1}, flt_1 = {2}, diff = {3}", flt_val_0, flt_hex, flt_val_1, flt_val_0 - flt_val_1);

            double dbl_val_0 = rand.NextDouble();
            string dbl_hex = Conversion.Double2Hex(dbl_val_0);
            double dbl_val_1 = Conversion.Hex2Double(dbl_hex);
            Console.WriteLine("dbl_0 = {0}, hex = {1}, dbl_1 = {2}, diff = {3}", dbl_val_0, dbl_hex, dbl_val_1, dbl_val_0 - dbl_val_1);

            Console.ReadKey();
        }
    }
}
