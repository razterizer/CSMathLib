using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathLib.utils
{
    public class Conversion
    {
        public static string Float2Hex(float value)
        {
            string hex_out = "";
            byte[] float_bytes = BitConverter.GetBytes(value);
            foreach (byte b in float_bytes.Reverse())
                hex_out += b.ToString("X").PadLeft(2, '0');
            return hex_out;
        }

        public static float Hex2Float(string hex_str)
        {
            uint num = uint.Parse(hex_str, System.Globalization.NumberStyles.AllowHexSpecifier);
            byte[] float_bytes = BitConverter.GetBytes(num);
            float value_out = BitConverter.ToSingle(float_bytes, 0);
            return value_out;
        }

        public static string Double2Hex(double value)
        {
            string hex_out = "";
            byte[] double_bytes = BitConverter.GetBytes(value);
            foreach (byte b in double_bytes.Reverse())
                hex_out += b.ToString("X").PadLeft(2, '0');
            return hex_out;
        }

        public static double Hex2Double(string hex_str)
        {
            long num = long.Parse(hex_str, System.Globalization.NumberStyles.AllowHexSpecifier);
            byte[] double_bytes = BitConverter.GetBytes(num);
            double value_out = BitConverter.ToDouble(double_bytes, 0);
            return value_out;
        }
    }
}
