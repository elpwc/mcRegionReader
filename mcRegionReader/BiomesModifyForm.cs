using LitJson;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace mcRegionReader
{
    public partial class BiomesModifyForm : Form
    {
        public BiomesModifyForm()
        {
            InitializeComponent();
        }

        private void BiomesModifyForm_Load(object sender, EventArgs e)
        {

        }
        List<JSONBiome> BS = new List<JSONBiome>();
        private void button11_Click(object sender, EventArgs e)
        {
            JsonData test = JsonMapper.ToObject(File.ReadAllText(@"data\biomes.json"));
            foreach (JsonData b in test)
            {
                BS.Add(JsonMapper.ToObject<JSONBiome>(b.ToJson()));
            }
            foreach (JSONBiome B in BS)
            {
                ListViewItem lvt = new ListViewItem(B.id.ToString());
                lvt.SubItems.Add(new ListViewItem.ListViewSubItem(lvt,B.name));
                listView1.Items.Add(lvt);
            }
        }


        private void listView1_Click(object sender, EventArgs e)
        {
            int selected = listView1.SelectedItems[0].Index;
            textBox1.Text = BS[selected].id.ToString();
            textBox2.Text = BS[selected].name;
            textBox3.Text = BS[selected].icon;
            textBox4.Text = BS[selected].temperature.ToString();
            textBox5.Text = BS[selected].color;
            textBox6.Text = BS[selected].langs.zh_cn;
            textBox7.Text = BS[selected].langs.zh_tw;
            textBox8.Text = BS[selected].langs.en_us;
            textBox9.Text = BS[selected].langs.jp;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            JSONBiome remove = new JSONBiome();
            foreach (JSONBiome B in BS)
            {
                if (B.id.ToString()==textBox1.Text)
                {
                    remove = B;
                    break;
                }
            }
            BS.Remove(remove);
            BS.Add(new JSONBiome(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text, textBox8.Text, textBox9.Text));
            BS = BS.OrderBy(s => s.id).ToList();

            listView1.Items.Clear();
            foreach (JSONBiome B in BS)
            {
                ListViewItem lvt = new ListViewItem(B.id.ToString());
                lvt.SubItems.Add(new ListViewItem.ListViewSubItem(lvt, B.name));
                listView1.Items.Add(lvt);
            }

            int next = Convert.ToInt16(textBox1.Text) + 1;
            if (indexExist(next))
            {
                try
                {
                    textBox1.Text = BS[next].id.ToString();
                    textBox2.Text = BS[next].name;
                    textBox3.Text = BS[next].icon;
                    textBox4.Text = BS[next].temperature.ToString();
                    textBox5.Text = BS[next].color;
                    textBox6.Text = BS[next].langs.zh_cn;
                    textBox7.Text = BS[next].langs.zh_tw;
                    textBox8.Text = BS[next].langs.en_us;
                    textBox9.Text = BS[next].langs.jp;
                }
                catch (Exception)
                {
                }

            }
            else
            {
                textBox1.Text = next.ToString();
                textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = textBox7.Text = textBox8.Text = textBox9.Text = "";
            }

        }

        public bool indexExist(int index)
        {
            foreach (JSONBiome B in BS)
            {
                if (B.id==index)
                {
                    return true;
                }
            }
            return false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            File.WriteAllText(@"data\biomes.json" ,JsonMapper.ToJson(BS));
        }
    }
}
