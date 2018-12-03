using System;
using System.IO;
using System.Linq;
using System.Text;

namespace mcRegionReader
{
    /// <summary>
    /// 提供与Java一致的流读写操作。
    /// </summary>
    public static class BinaryJava
    {
        public static short readShort(BinaryReader br)
        {
            return NumberTransfer.HexByteArray2Short(br.ReadBytes(2));
        }

        public static int readInt(BinaryReader br)
        {
            return NumberTransfer.HexByteArray2Int(br.ReadBytes(4));
        }

        public static int read3byteInt(BinaryReader br)
        {
            byte[] t = br.ReadBytes(3);
            return NumberTransfer.HexByteArray2Int(0, t[0], t[1], t[2]);
        }

        public static long readLong(BinaryReader br)
        {
            return NumberTransfer.HexByteArray2Long(br.ReadBytes(8));
        }

        public static float readFloat(BinaryReader br)
        {
            return NumberTransfer.HexByteArray2Float(br.ReadBytes(4));
        }

        public static double readDouble(BinaryReader br)
        {
            return NumberTransfer.HexByteArray2Double(br.ReadBytes(8));
        }

        public static string readUTF(BinaryReader br)
        {
            int strlen = readShort(br);
            return Encoding.UTF8.GetString(br.ReadBytes(strlen));
        }




        public static void writeInt(BinaryWriter bw, int value)
        {
            bw.Write(BitConverter.GetBytes(value).Reverse().ToArray());
        }

        public static void writeShort(BinaryWriter bw, short value)
        {
            bw.Write(BitConverter.GetBytes(value).Reverse().ToArray());
        }

        public static void writeLong(BinaryWriter bw, long value)
        {
            bw.Write(BitConverter.GetBytes(value).Reverse().ToArray());
        }

        public static void writeFloat(BinaryWriter bw, float value)
        {
            bw.Write(BitConverter.GetBytes(value).Reverse().ToArray());
        }

        public static void writeDouble(BinaryWriter bw, double value)
        {
            bw.Write(BitConverter.GetBytes(value).Reverse().ToArray());
        }

        public static void writeUTF(BinaryWriter bw, string value)
        {
            writeShort(bw, (short)value.Length);
            bw.Write(Encoding.UTF8.GetBytes(value));
        }
    }
}
