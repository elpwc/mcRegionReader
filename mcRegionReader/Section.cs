using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mcRegionReader
{
    public struct Palette
    {
        public struct Property
        {
            public string Name;
            public string Value;
        }
        public string block;
        public Property[] properties;
    }

    public class Section
    {
        public Palette[] paletts = { };
        public short[,,] block = { };
        public byte y = 0;
        public void FromTag(Tag section)
        {

        }
     }
}
