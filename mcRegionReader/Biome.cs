using System;
using System.Collections.Generic;
using System.Drawing;

namespace mcRegionReader
{
    public class Biome
    {
        public Biome() { }
        public Biome(short id, string name, string icon, double temperature, string color)
        {
            this.id = id;
            this.name = name;
            this.icon = icon;
            this.temperature = temperature;
            this.color = color;
        }
        public Biome(short id, string name, string icon, double temperature, string color,string zh_cn, string zh_tw, string en_us, string jp)
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
        public Biome(string id, string name, string icon, string temperature, string color, string zh_cn, string zh_tw, string en_us, string jp)
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

        public lang langs = new lang();
    }
}
