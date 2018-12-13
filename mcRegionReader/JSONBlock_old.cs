using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace mcRegionReader
{
    
    public class JSONVariantBlock_old : JSONBlock_old
    {
        public int data;
    }


    public class JSONBlock_old
    {
        public int id = 0;
        public string name = "";
        public double alpha = 0;
        public int mask = 0;
        public string color = "000000";
        public bool transparent = false;
        public bool spawninside = false;
        public bool liquid = false;
        public bool rendercube = false;
        public bool canProvidePower = false;
        public JSONVariantBlock_old[] variants = { };
        
        public Color GetColor()
        {
            if (transparent)
            {
                return Color.Transparent;
            }
            else if (color == "")
            {
                return Color.Black;
            }
            else
            {
                return Color.FromArgb(NumberTransfer.HexString2Int(color.Substring(0, 2)), NumberTransfer.HexString2Int(color.Substring(2, 2)), NumberTransfer.HexString2Int(color.Substring(4, 2)));
            }
        }
    }
}
