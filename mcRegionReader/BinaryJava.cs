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
        public static short ReadShort(BinaryReader br)
        {
            return NumberTransfer.HexByteArray2Short(br.ReadBytes(2));
        }

        public static int ReadInt(BinaryReader br)
        {
            return NumberTransfer.HexByteArray2Int(br.ReadBytes(4));
        }

        public static int Read3byteInt(BinaryReader br)
        {
            byte[] t = br.ReadBytes(3);
            return NumberTransfer.HexByteArray2Int(0, t[0], t[1], t[2]);
        }

        public static long ReadLong(BinaryReader br)
        {
            return NumberTransfer.HexByteArray2Long(br.ReadBytes(8));
        }

        public static float ReadFloat(BinaryReader br)
        {
            return NumberTransfer.HexByteArray2Float(br.ReadBytes(4));
        }

        public static double ReadDouble(BinaryReader br)
        {
            return NumberTransfer.HexByteArray2Double(br.ReadBytes(8));
        }

        public static string ReadUTF(BinaryReader br)
        {
            int strlen = ReadShort(br);
            return Encoding.UTF8.GetString(br.ReadBytes(strlen));
        }




        public static void WriteInt(BinaryWriter bw, int value)
        {
            bw.Write(BitConverter.GetBytes(value).Reverse().ToArray());
        }

        public static void WriteShort(BinaryWriter bw, short value)
        {
            bw.Write(BitConverter.GetBytes(value).Reverse().ToArray());
        }

        public static void WriteLong(BinaryWriter bw, long value)
        {
            bw.Write(BitConverter.GetBytes(value).Reverse().ToArray());
        }

        public static void WriteFloat(BinaryWriter bw, float value)
        {
            bw.Write(BitConverter.GetBytes(value).Reverse().ToArray());
        }

        public static void WriteDouble(BinaryWriter bw, double value)
        {
            bw.Write(BitConverter.GetBytes(value).Reverse().ToArray());
        }

        public static void WriteUTF(BinaryWriter bw, string value)
        {
            WriteShort(bw, (short)value.Length);
            bw.Write(Encoding.UTF8.GetBytes(value));
        }
    }
}
