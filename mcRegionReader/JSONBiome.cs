﻿using System;
using System.Drawing;

namespace mcRegionReader
{
    /// <summary>
    /// 储存一个从JSON文件中直接读出的全部种类Biome的数据，若储存区块中的Biome，请使用<see cref="short[,] biomes=new short[16,16]; "/>储存
    /// </summary>
    public class JSONBiome
    {
        public JSONBiome() { }
        public JSONBiome(short id, string name, string icon, double temperature, string color)
        {
            this.id = id;
            this.name = name;
            this.icon = icon;
            this.temperature = temperature;
            this.color = color;
        }
        public JSONBiome(short id, string name, string icon, double temperature, string color,string zh_cn, string zh_tw, string en_us, string jp)
        {
            this.id = id;
            this.name = name;
            this.icon = icon;
            this.temperature = temperature;
            this.color = color;
            this.langs.zh_cn = zh_cn;
            this.langs.zh_tw = zh_tw;
            this.langs.en_us = en_us;
            this.langs.jp = jp;
        }
        public JSONBiome(string id, string name, string icon, string temperature, string color, string zh_cn, string zh_tw, string en_us, string jp)
        {
            this.id = (byte)Convert.ToInt32(id);
            this.name = name;
            this.icon = icon;
            if (temperature=="")
            {
                temperature = "0";
            }
            this.temperature = Convert.ToDouble( temperature);
            this.color = color;
            this.langs.zh_cn = zh_cn;
            this.langs.zh_tw = zh_tw;
            this.langs.en_us = en_us;
            this.langs.jp = jp;
        }

        public short id = 0;
        public string name = "";
        public string icon = "";
        public double temperature = 0.0;
        public string color = "0";

        public Color GetColor()
        {
            if (color=="transparent")
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

        public lang langs = new lang();
    }
}
