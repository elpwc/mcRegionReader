using LitJson;
using System.Collections.Generic;
using System.IO;

namespace mcRegionReader
{
    /// <summary>
    /// 外部Biomes方法
    /// </summary>
    public static class BiomesUtil
    {
        public static Biome[] ReadBiomes()
        {
            List<Biome> BS = new List<Biome>();

            JsonData test = JsonMapper.ToObject(File.ReadAllText(@"data\biomes.json"));
            foreach (JsonData b in test)
            {
                BS.Add(JsonMapper.ToObject<Biome>(b.ToJson()));
            }
            return BS.ToArray();
        }

        public static Biome GetBiomeById(int id)
        {
            Biome[] biomes = GlobalVar.biomes;
            foreach (Biome eachBiome in biomes)
            {
                if (eachBiome.id == id)
                {
                    return eachBiome;
                }
            }
            return null;
        }
    }
}
