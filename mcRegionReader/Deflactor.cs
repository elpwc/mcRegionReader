using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System;
using System.IO;
using System.Linq;

namespace mcRegionReader
{
    /// <summary>
    /// 提供Deflactor压缩格式的方法。
    /// </summary>
    public static class Deflactor
    {
        /// <summary>
        /// 去掉ZLIB格式包装的外壳
        /// 详见：https://blog.csdn.net/querw/article/details/51569274
        /// </summary>
        /// <param name="zlibBytes"></param>
        /// <returns></returns>
        public static byte[] ZLibToDeflactor(byte[] zlibBytes)
        {
            byte[] res = zlibBytes.Take<byte>(zlibBytes.Length - 4).ToArray<byte>();
            res = res.Skip<byte>(6).ToArray<byte>();
            return res;
        }


        /// <summary>
        /// 压缩算法
        /// </summary>
        /// <param name="pBytes"></param>
        /// <returns></returns>
        public static byte[] Compress(byte[] pBytes)
        {
            MemoryStream mMemory = new MemoryStream();
            Deflater mDeflater = new Deflater(Deflater.BEST_COMPRESSION);
            using (DeflaterOutputStream mStream = new DeflaterOutputStream(mMemory, mDeflater, 131072))
            {
                mStream.Write(pBytes, 0, pBytes.Length);
            }

            return mMemory.ToArray();
        }
        /// <summary>
        /// 解压缩算法
        /// </summary>
        /// <param name="pBytes"></param>
        /// <returns></returns>
        public static byte[] DeCompress(byte[] pBytes)
        {
            MemoryStream mMemory = new MemoryStream();
            using (InflaterInputStream mStream = new InflaterInputStream(new MemoryStream(pBytes)))
            {
                Int32 mSize;
                byte[] mWriteData = new byte[4096];
                while (true)
                {
                    mSize = mStream.Read(mWriteData, 0, mWriteData.Length);
                    if (mSize > 0)
                        mMemory.Write(mWriteData, 0, mSize);
                    else
                        break;
                }
            }
            return mMemory.ToArray();
        }
    }
}
