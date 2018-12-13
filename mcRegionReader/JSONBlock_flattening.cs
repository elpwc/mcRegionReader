using System.Drawing;

namespace mcRegionReader
{
    public struct JSONBlock_flattening_Material
    {
        public bool powerSource;
        public int lightValue;
        public double hardness;
        public double resistance;
        public bool ticksRandomly;
        public bool fullCube;
        public double slipperiness;
        public bool liquid;
        public bool solid;
        public bool movementBlocker;
        public bool burnable;
        public bool opaque;
        public bool replacedDuringPlacement;
        public bool toolRequired;
        public bool fragileWhenPushed;
        public bool unpushable;
        public string mapColor;
        public bool isTranslucent;
        public bool hasContainer;
    }
    public class JSONBlock_flattening
    {
        public string id = "";
        public string localizedName = "";
        public JSONBlock_flattening_Material material = new JSONBlock_flattening_Material();

        public Color GetColor()
        {
            if (localizedName=="Air")
            {
                return Color.Transparent;
            }
            else if (material.mapColor == "")
            {
                return Color.Black;
            }
            else
            {
                return Color.FromArgb(NumberTransfer.HexString2Int(material.mapColor.Substring(0, 2)), NumberTransfer.HexString2Int(material.mapColor.Substring(2, 2)), NumberTransfer.HexString2Int(material.mapColor.Substring(4, 2)));
            }
        }
    }
}

