using System;
using System.Linq;

namespace mcRegionReader
{
    /// <summary>
    /// 提供字符数组转数字的方法。
    /// </summary>
    public static class NumberTransfer
    {
        public static int HexByteArray2Int(params byte[] hex)
        {
            return BitConverter.ToInt32(hex.Reverse().ToArray (),0);
            //string res = "";
            //foreach (byte eachByte in hex)
            //{
            //    res += Convert.ToString(eachByte, 16).PadLeft(2, '0');
            //}
            //if (res != "")
            //{
            //    return Convert.ToInt32(res, 16);
            //}
            //return 0;

        }
        public static short HexByteArray2Short(params byte[] hex)
        {
            return BitConverter.ToInt16(hex.Reverse().ToArray(), 0);
            //string res = "";
            //foreach (byte eachByte in hex)
            //{
            //    res += Convert.ToString(eachByte, 16).PadLeft(2, '0');
            //}
            //if (res != "")
            //{
            //    return Convert.ToInt16(res, 16);
            //}
            //return 0;

        }
        public static long HexByteArray2Long(params byte[] hex)
        {
            return BitConverter.ToInt64(hex.Reverse().ToArray(), 0);
            //string res = "";
            //foreach (byte eachByte in hex)
            //{
            //    res += Convert.ToString(eachByte, 16).PadLeft(2, '0');
            //}
            //if (res != "")
            //{
            //    long b= Convert.ToInt64(res, 16);
            //    return Convert.ToInt64(res, 16);
            //}
            //return 0;

        }

        public static float HexByteArray2Float(params byte[] hex)
        {
                return BitConverter.ToSingle(hex.Reverse().ToArray(), 0);
        }

        public static double HexByteArray2Double(params byte[] hex)
        {
                return BitConverter.ToDouble(hex.Reverse().ToArray(), 0);
        }


    }
}
