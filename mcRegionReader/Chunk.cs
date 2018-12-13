using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace mcRegionReader
{
    public class Chunk
    {
        public class Block
        {
            public class Property
            {
                public Property(string name, string value)
                {
                    this.name = name;
                    this.value = value;
                }
                public string name="";
                public string value="";
            }
            public Block(string name)
            {
                this.name = name;
            }
            public Block(string name, Property[] properties)
            {
                this.name = name;
                this.properties = properties;
            }
            public Block(string name, List<Property> properties)
            {
                this.name = name;
                this.properties = properties.ToArray();
            }
            public string name="";
            public Property[] properties= { };
        }

        public int xPos = 0;
        public int zPox = 0;
        public int x = 0;
        public int z = 0;
        public Block[,,] blocks = new Block[256, 16, 16];//Y x X x Z
        public short[,] biomes = new short[16,16];//X x Z
        
        public void GetBiomes(Tag a)
        {
            Tag biomes = a.FindTagByName("Level").FindTagByName("Biomes");
            int i = 0;

            Bitmap chunkpic = new Bitmap(16, 16);
            for (int y = 0; y < 16; y++)
            {
                for (int x = 0; x < 16; x++)
                {
                    if (biomes.GetValue() is int[])
                    {
                        this.biomes[x, y] = (short)((int[])biomes.GetValue())[i];
                    }
                    else if (biomes.GetValue() is byte[])
                    {
                        this.biomes[x, y] = ((byte[])biomes.GetValue())[i];
                    }
                    else
                    {
                        throw new Exception("Unknown biomes type.");
                    }
                    i++;
                }
            }
        }

        public static Image GetBiomesImage(Tag a)
        {
            Tag biomes = a.FindTagByName("Level").FindTagByName("Biomes");
            int i = 0;
            int currentBiome = -1;

            Bitmap chunkpic = new Bitmap(16, 16);
            for (int y = 0; y < 16; y++)
            {
                for (int x = 0; x < 16; x++)
                {
                    currentBiome = ((byte[])biomes.GetValue())[i];
                    chunkpic.SetPixel(x, y, Color.FromArgb(currentBiome, currentBiome, currentBiome));
                    i++;
                }
            }
            return chunkpic;
        }

        public void GetBlocks(Tag a)
        {
            int[] blockIds = new int[16 * 16 * 256];
            Tag[] sectionsTags = (Tag[])a.FindTagByName("Level").FindTagByName("Sections").GetValue();
            foreach (Tag section in sectionsTags)
            {
                byte yPos = (byte)section.FindTagByName("Y").GetValue();
                if (section.FindTagByName("Palette")!=null)
                {
                    Tag[] paletteTags = (Tag[])section.FindTagByName("Palette").GetValue();
                    long[] compressedSectionEncodedBlocksIds = (long[])section.FindTagByName("BlockStates").GetValue();
                    int[] sectionBlockIds = LongArrayDecompress(compressedSectionEncodedBlocksIds, 256 * 16);
                    int x = 0, y = 0, z = 0;
                    for (int i = 0; i < sectionBlockIds.Length; i++)
                    {

                        y = yPos * 16 + i / 256;
                        x = i % 256 / 16;
                        z = i % 256 % 16;
                        string stringBlockID = paletteTags[sectionBlockIds[i]].FindTagByName("Name").GetValue().ToString();
                        blocks[y, x, z] = new Block(stringBlockID);

                    }
                }

            }
        }

        public Image GetMap(int y)
        {
            Bitmap mappic = new Bitmap(16, 16);
            int yi = y ;
            for (int z = 0; z < 16; z++)
            {
                for (int x = 0; x < 16; x++)
                {
                    yi = y;
                    Color color = GetBlockColor(y, x, z);
                    if (color==Color.Transparent)
                    {
                        do
                        {
                            if (yi == 0)
                            {
                                color = Color.Transparent;
                                break;
                            }
                            yi--;
                            color = GetBlockColor(yi, x, z);
                        } while (color == Color.Transparent);
                    }
                    
                    mappic.SetPixel(z, x, color);
                }
            }
            return mappic;
        }

        public Color GetBlockColor(int y,int x,int z)
        {
            if (blocks[y, x, z]==null)
            {
                return Color.Transparent;
            }
            else
            {
                return JsonUtil.GetBlock_flatteningById(blocks[y, x, z].name).GetColor();
            }
        }

        public int[] LongArrayDecompress(long[] input, int numberOfValues)
        {
            int[] result = new int[numberOfValues];
            int bitsPerValue = 64 * input.Length / numberOfValues;
            int inputLongIdx = 0;
            int inputBitIdx = 0;
            for (int outputIntIdx = 0; outputIntIdx < result.Length; outputIntIdx++)
            {
                for (int outputBitIdx = 0; outputBitIdx < bitsPerValue; outputBitIdx++)
                {
                    long value = input[inputLongIdx] & (1L << inputBitIdx);
                    if (value != 0)
                    {
                        value = 1;
                    }
                    result[outputIntIdx] = (int)(((uint)result[outputIntIdx] | (value << outputBitIdx)) & 0xff);
                    inputBitIdx++;
                    if (inputBitIdx > 63)
                    {
                        inputLongIdx++;
                        inputBitIdx = 0;
                    }
                }
            }
            return result;
        }

    }
}
