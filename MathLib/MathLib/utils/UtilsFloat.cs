using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathLib.utils
{
    public class UtilsFloat
    {
        public static float Clamp(float v, float l, float u)
        {
            if (v < l)
                return l;
            if (v > u)
                return u;
            return v;
        }
        public static void ClampSet(ref float v, float l, float u)
        {
            if (v < l)
                v = l;
            else if (v > u)
                v = u;
        }

        // I hope it does an inline on this.
        public static float Sqr(float v)
        {
            return v * v;
        }
    }
}
