using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mcRegionReader
{
    public class Chunk
    {
        public int xPos = 0;
        public int zPox = 0;
        public Section[] sections = { };
        public Biome[] biomes = { };
    }
}
