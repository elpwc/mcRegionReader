using System;

namespace mcRegionReader
{
    /// <summary>
    /// 提供字符数组转数字的方法。
    /// </summary>
    public static class NumberTransfer
    {
        public static int HexString2Int(string hex)
        {
            return Convert.ToInt32(hex, 16);
        }

        public static int HexByteArray2Int(params byte[] hex)
        {
            Array.Reverse(hex);
            return BitConverter.ToInt32(hex,0);
        }
        public static short HexByteArray2Short(params byte[] hex)
        {
            Array.Reverse(hex);
            return BitConverter.ToInt16(hex, 0);
        }
        public static long HexByteArray2Long(params byte[] hex)
        {
            Array.Reverse(hex);
            return BitConverter.ToInt64(hex, 0);
        }

        public static float HexByteArray2Float(params byte[] hex)
        {
            Array.Reverse(hex);
            return BitConverter.ToSingle(hex, 0);
        }

        public static double HexByteArray2Double(params byte[] hex)
        {
            Array.Reverse(hex);
            return BitConverter.ToDouble(hex, 0);
        }


    }
}
