using LitJson;
using System.Collections.Generic;
using System.IO;

namespace mcRegionReader
{
    /// <summary>
    /// Json读入文件方法
    /// </summary>
    public static class JsonUtil
    {
        /// <summary>
        /// 将biomes.json中储存的每种biome信息读取到<see cref="GlobalVar.biomes"/>中
        /// </summary>
        /// <returns></returns>
        public static JSONBiome[] ReadBiomes()
        {
            List<JSONBiome> BS = new List<JSONBiome>();

            JsonData test = JsonMapper.ToObject(File.ReadAllText(@"data\biomes.json"));
            foreach (JsonData b in test)
            {
                BS.Add(JsonMapper.ToObject<JSONBiome>(b.ToJson()));
            }
            return BS.ToArray();
        }

        /// <summary>
        /// 根据biomeid取到biome信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static JSONBiome GetBiomeById(int id)
        {
            foreach (JSONBiome eachBiome in GlobalVar. biomes)
            {
                if (eachBiome.id == id)
                {
                    return eachBiome;
                }
            }
            return null;
        }

        /// <summary>
        /// 将blocks.json中储存的每种block信息读取到<see cref="GlobalVar.blocks_old"/>中
        /// </summary>
        /// <returns></returns>
        public static JSONBlock_old[] ReadBlocks_old()
        {
            List<JSONBlock_old> BS = new List<JSONBlock_old>();

            JsonData test = JsonMapper.ToObject(File.ReadAllText(@"data\blocks_old.json"));
            foreach (JsonData b in test)
            {
                BS.Add(JsonMapper.ToObject<JSONBlock_old>(b.ToJson()));
            }
            return BS.ToArray();
        }

        /// <summary>
        /// 根据blockid取到block信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static JSONBlock_old GetBlock_oldById(string id)
        {
            foreach (JSONBlock_old eachBlock in GlobalVar.blocks_old)
            {
                if (id==null)
                {
                    return GlobalVar.blocks_old[0];
                }
                else
                {
                    if (eachBlock.name == id)
                    {
                        return eachBlock;
                    }
                }

            }
            return GlobalVar.blocks_old[0];
        }

        /// <summary>
        /// 将blocks.json中储存的每种block信息读取到<see cref="GlobalVar.blocks_flattening"/>中
        /// </summary>
        /// <returns></returns>
        public static JSONBlock_flattening[] ReadBlocks_flattening()
        {
            List<JSONBlock_flattening> BS = new List<JSONBlock_flattening>();

            JsonData test = JsonMapper.ToObject(File.ReadAllText(@"data\blocks_flattening.json"));
            foreach (JsonData b in test)
            {
                BS.Add(JsonMapper.ToObject<JSONBlock_flattening>(b.ToJson()));
            }
            return BS.ToArray();
        }

        /// <summary>
        /// 根据blockid取到block信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static JSONBlock_flattening GetBlock_flatteningById(string id)
        {
            foreach (JSONBlock_flattening eachBlock in GlobalVar.blocks_flattening)
            {
                if (id == null)
                {
                    return GlobalVar.blocks_flattening[14];
                }
                else
                {
                    if (eachBlock.id == id)
                    {
                        return eachBlock;
                    }
                }

            }
            return GlobalVar.blocks_flattening[14];
        }
    }
}
