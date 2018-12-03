using System;
using System.Collections.Generic;
using System.IO;

namespace mcRegionReader
{
    /// <summary>
    /// 提供对Minecraft Region文件的读写。
    /// </summary>
    public class RegionFile
    {
        public List<RegionFile_Location> locations=new List<RegionFile_Location>();
        public List<RegionFile_Chunk> chunks=new List<RegionFile_Chunk>();
        public static RegionFile_Chunk getChunkByLocation(RegionFile_Location location, Stream st)
        {

            RegionFile_Chunk tempChunk = new RegionFile_Chunk();

            BinaryReader br = new BinaryReader(st);
            { 

                br.ReadBytes(8192);
                long ichi = br.BaseStream.Position;
                br.BaseStream.Position = location.offset * 4096;
                byte[] chunkData = br.ReadBytes(location.sectorCount * 4096);//file.Skip(location.offset*4096).Take(location.sectorCount*4096).ToArray();
                BinaryReader chunkDataBR = new BinaryReader(new MemoryStream(chunkData));
                tempChunk.length = BinaryJava.readInt(chunkDataBR);// NumberTransfer.HexByteArray2Int(chunkData.Skip(0).Take(4).ToArray()) - 1;
                tempChunk.compressionType = chunkDataBR.ReadByte();// NumberTransfer.HexByteArray2Int(chunkData[4]);
                if (tempChunk.compressionType == 2)
                {
                    //tempChunk.Data = Deflactor.DeCompress(chunkDataBR.ReadBytes(tempChunk.length)); // chunkData.Skip(5).Take(tempChunk.length).ToArray());
                    tempChunk.tag = RegionFile_Chunk.ReadTags( Deflactor.DeCompress(chunkDataBR.ReadBytes(tempChunk.length))); 
                    chunkDataBR.Dispose();
                    //GC.Collect();
                    return tempChunk;
                }
                else
                {
                    throw new Exception("Unknown compression type");
                }
            }
            
        }
        public void getChunkByLocation(Stream st)
        {
            foreach (RegionFile_Location eachLocation in locations)
            {
                chunks.Add(getChunkByLocation(eachLocation, st));
            }

        }
        public static RegionFile_Location ReadLocation(Stream st, short index,bool readTimeStamps=true)
        {
            RegionFile_Location location = new RegionFile_Location();
            using (BinaryReader br = new BinaryReader(st))
            {
                br.BaseStream.Position = index * 4;
                location.offset = BinaryJava.read3byteInt(br);
                location.sectorCount = br.ReadByte();
                if (readTimeStamps)
                {
                    br.BaseStream.Position = index * 4 + 4096;
                    if (location.sectorCount != 0)
                    {
                        location.timestamp = BinaryJava.readInt(br);
                    }
                }
                return location;
            }
        }
        public void ReadLocations(Stream st, bool readTimeStamps=true)
        {
            RegionFile_Location tempLocation=new RegionFile_Location();
            BinaryReader br = new BinaryReader(st);
                for (int i = 0; i < 1024; i++)
                {
                    tempLocation.offset = BinaryJava.read3byteInt(br);
                    tempLocation.sectorCount = br.ReadByte();
                    if (tempLocation.sectorCount != 0)
                    {
                        locations.Add(tempLocation);
                    }

                    tempLocation = new RegionFile_Location();
                }
                if (readTimeStamps)
                {
                    for (int i = 0; i < 1024; i++)
                    {
                        if (tempLocation.sectorCount != 0)
                        {
                            locations[i].timestamp = BinaryJava.readInt(br);
                        }

                    }
                }
            


        }
        
    }

    /// <summary>
    /// 提供对Minecraft Region文件中区块位移及时间戳的储存。
    /// </summary>
    /// 
    public class RegionFile_Location
    {
        public RegionFile_Location()
        {
        }
        public RegionFile_Location(int offset, int sectorCount)
        {
            this.offset = offset;
            this.sectorCount = sectorCount;
        }
        public int offset = 0;
        public int sectorCount = 0;
        public int timestamp = 0;
    }

    /// <summary>
    /// 提供对Minecraft Region文件中区块信息的读写。
    /// </summary>
    public class RegionFile_Chunk
    {
        public RegionFile_Chunk() { }
        public RegionFile_Chunk(int length)
        {
            this.length = length;
        }
        public RegionFile_Chunk(int length, int compressionType)
        {
            this.length = length;
            this.compressionType = compressionType;
        }
        /// <summary>
        /// 区块信息长度。
        /// </summary>
        public int length = 0;
        /// <summary>
        /// 区块压缩方式。
        /// </summary>
        public int compressionType = 2;
        /// <summary>
        /// 区块信息（未处理的）。
        /// </summary>
        //public byte[] Data;
        /// <summary>
        /// 区块时间戳。
        /// </summary>
        public long timestamp = 0;
        /// <summary>
        /// 处理为NBT标签的区块信息。
        /// </summary>
        public Tag tag;
        /// <summary>
        /// 将外部的区块数据转换为NBT标签。
        /// </summary>
        /// <param name="inputData">区块数据</param>
        /// <returns>NBT标签</returns>
        public static Tag ReadTags(byte[] inputData)
        {
            return Tag.readFrom(new MemoryStream(inputData));
        }
        //public void GetTags()
        //{
        //    tag= Tag.readFrom(new MemoryStream(Data));
        //}

    }



}
