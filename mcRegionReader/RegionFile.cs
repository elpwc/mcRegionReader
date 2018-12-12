using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace mcRegionReader
{
    /// <summary>
    /// 提供对Minecraft Region文件的读写。
    /// </summary>
    public class RegionFile
    {
        /// <summary>
        /// 0-null, 1-overworld, 2-the nether, 3-the end
        /// </summary>
        public byte dimension = 0;
        public int regionX = 0;
        public int regionZ = 0;
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
                byte[] chunkData = br.ReadBytes(location.sectorCount * 4096);
                BinaryReader chunkDataBR = new BinaryReader(new MemoryStream(chunkData));
                tempChunk.length = BinaryJava.ReadInt(chunkDataBR);
                tempChunk.compressionType = chunkDataBR.ReadByte();
                if (tempChunk.compressionType == 2)
                {
                    //tempChunk.Data = Deflactor.DeCompress(chunkDataBR.ReadBytes(tempChunk.length)); // chunkData.Skip(5).Take(tempChunk.length).ToArray());
                    //File.WriteAllBytes( "decomBytesNewnewnew",Deflactor.DeCompress(chunkDataBR.ReadBytes(tempChunk.length)));
                    tempChunk.tag = RegionFile_Chunk.ReadTags( Deflactor.DeCompress(chunkDataBR.ReadBytes(tempChunk.length))); 
                    chunkDataBR.Dispose();
                    return tempChunk;
                }
                else
                {
                    throw new Exception("Unknown compression type");
                }
            }
            
        }
        public void GetChunkByLocation(Stream st)
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
                location.offset = (short)BinaryJava.Read3byteInt(br);
                location.sectorCount = br.ReadByte();
                if (readTimeStamps)
                {
                    br.BaseStream.Position = index * 4 + 4096;
                    if (location.sectorCount != 0)
                    {
                        location.timestamp = BinaryJava.ReadInt(br);
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
                    tempLocation.offset = (short)BinaryJava.Read3byteInt(br);
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
                            locations[i].timestamp = BinaryJava.ReadInt(br);
                        }

                    }
                }
            


        }
        /// <summary>
        /// 从mca文件创建新的RegionFile对象
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <param name="demension">世界维度 0-null, 1-overworld, 2-the nether, 3-the end</param>
        /// <param name="isItAStandardMcaFileFromRegionFolder">文件名是否符合r.x.z.mca的规范</param>
        /// <param name="x">若文件名不符合规范而输入的x</param>
        /// <param name="z">若文件名不符合规范而输入的z</param>
        /// <returns></returns>
        public static RegionFile FromMcaFile(string filename, byte demension = 0, bool isItAStandardMcaFileFromRegionFolder = true, int x = 0, int z = 0)
        {
            if (File.Exists(filename))
            {
                FileStream file = new FileStream(filename, FileMode.Open, FileAccess.Read);
                RegionFile regionfile = new RegionFile();
                regionfile.ReadLocations(file, false);
                regionfile.GetChunkByLocation(file);
                regionfile.dimension = demension;
                if (isItAStandardMcaFileFromRegionFolder)
                {
                    string[] filenamesplit = filename.Split('\\').ToList().Last().Split('.');
                    if (filenamesplit.Length ==4 && filenamesplit[0]=="r"&&filenamesplit[3]=="mca")
                    {
                        regionfile.regionX = Convert.ToInt32(filenamesplit[1]);
                        regionfile.regionZ = Convert.ToInt32(filenamesplit[2]);
                    }
                    else
                    {
                        throw new Exception("Wrong region filename format.");
                    }
                }
                else
                {
                    regionfile.regionX = x;
                    regionfile.regionZ = z;
                }
                return regionfile;
            }
            return null;
        }
        public Image GetBiomeImage()
        {
            object regionBiomes;
            Bitmap pic = new Bitmap(16 * 32, 16 * 32);
            int i = 0;
            int currentBiome = -1;
            int chunkX = 0, chunkZ = 0;
            int[] regionBiomesValueInt = { };
            byte[] regionBiomesValueByte = { };
            foreach (RegionFile_Chunk chunk in chunks)
            {
                regionBiomes = chunk.tag.FindTagByName("Level").FindTagByName("Biomes").GetValue();
                if (regionBiomes is int[])
                {
                    regionBiomesValueInt = (int[])regionBiomes;
                }
                else if (regionBiomes is byte[])
                {
                    regionBiomesValueByte = (byte[])regionBiomes;
                }
                else
                {
                    throw new Exception("Unknown biomes type.");
                }
                i = 0;
                chunkX = (int)chunk.tag.FindTagByName("Level").FindTagByName("xPos").GetValue() - regionX * 32;
                chunkZ = (int)chunk.tag.FindTagByName("Level").FindTagByName("zPos").GetValue() - regionZ * 32;
                Bitmap chunkpic = new Bitmap(16, 16);
                for (int y = 0; y < 16; y++)
                {
                    for (int x = 0; x < 16; x++)
                    {
                        if (regionBiomes is int[])
                        {
                            if (regionBiomesValueInt.Length == 256)
                            {
                                currentBiome = (int)regionBiomesValueInt[i];
                                Color tmpcolor = BiomesUtil.GetBiomeById(currentBiome).GetColor();
                                chunkpic.SetPixel(x, y, BiomesUtil.GetBiomeById(currentBiome).GetColor());
                            }
                        }
                        else if (regionBiomes is byte[])
                        {
                            if (regionBiomesValueByte.Length == 256)
                            {
                                currentBiome = regionBiomesValueByte[i];
                                Color tmpcolor = BiomesUtil.GetBiomeById(currentBiome).GetColor();
                                chunkpic.SetPixel(x, y, BiomesUtil.GetBiomeById(currentBiome).GetColor());
                            }
                        }
                        else
                        {
                            throw new Exception("Unknown biomes type.");
                        }

                        i++;
                    }
                }
                pic = (Bitmap)MyGraphics.CombineBitmap(chunkpic, pic, chunkX * 16, chunkZ * 16);

            }
            return pic;
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
            this.offset = (short)offset;
            this.sectorCount = (byte)sectorCount;
        }
        public short offset = 0;
        public byte sectorCount = 0;
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
            this.compressionType = (byte)compressionType;
        }
        /// <summary>
        /// 区块信息长度。
        /// </summary>
        public int length = 0;
        /// <summary>
        /// 区块压缩方式。
        /// </summary>
        public byte compressionType = 2;
        /// <summary>
        /// 区块信息（未处理的）。
        /// </summary>
        //public byte[] Data;
        /// <summary>
        /// 区块时间戳。
        /// </summary>
        public int timestamp = 0;
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
            return Tag.ReadFrom(new MemoryStream(inputData));
        }
        //public void GetTags()
        //{
        //    tag= Tag.readFrom(new MemoryStream(Data));
        //}

    }



}
