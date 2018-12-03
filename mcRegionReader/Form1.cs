using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using LitJson;
using mcRegionReader;
using System.IO.Compression;
using System.Threading;

namespace mcRegionReader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (File.Exists( openFileDialog1.FileName))
            {
                textBox1.Text = openFileDialog1.FileName;
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<Tag> tags = new List<mcRegionReader.Tag>();
            tags.Add(new mcRegionReader.Tag(mcRegionReader.Tag.Type.TAG_Int, "int", 12345));
            tags.Add(new mcRegionReader.Tag(mcRegionReader.Tag.Type.TAG_Short, "short", (short)1234));
            tags.Add(new mcRegionReader.Tag(mcRegionReader.Tag.Type.TAG_String, "string", "hello"));
            tags.Add(new mcRegionReader.Tag(mcRegionReader.Tag.Type.TAG_Float, "float", 1.234f));
            tags.Add(new mcRegionReader.Tag(mcRegionReader.Tag.Type.TAG_End, "", null));
            a= new Tag(mcRegionReader.Tag.Type.TAG_Compound, "test", tags.ToArray());
            treeView1.Nodes.Add(addToTree(a));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Stream output=new FileStream("res",FileMode.Create,FileAccess.Write);
            a.writeTo(output);
            output.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            a = RegionFile_Chunk.ReadTags(File.ReadAllBytes(textBox1.Text));
            treeView1.Nodes.Add(addToTree(a));
        }

        //List<RegionFile> overworld = new List<RegionFile>();
        Tag a;
        private Bitmap chunkpic;

        private void button5_Click(object sender, EventArgs e)
        {
            FileStream file = new FileStream(textBox1.Text, FileMode.Open, FileAccess.Read);
            RegionFile RF = new RegionFile();
            RF.ReadLocations(file);
            RF.getChunkByLocation(file);
            //RF.chunks[0]= mcRegionReader.RegionFile.getChunkByLocation(file);
            a =RF.chunks[0].tag;
            

            treeView1.Nodes.Add(addToTree(a));

        }

        public TreeNode addToTree(Tag tag)
        {
            if (tag!=null)
            {
                
                TreeNode tn = new TreeNode(tag.name);

                tn.ImageIndex = (int)tag.type - 1 ;

                switch (tag.type)
                {
                    case mcRegionReader.Tag.Type.TAG_End:
                        break;
                    case mcRegionReader.Tag.Type.TAG_Byte_Array:
                        //tn.Text += "<TAG_Byte_Array>";
                        tn.Tag = "";
                        foreach (byte r in (byte[])tag.value)
                        {
                                tn.Nodes.Add(new TreeNode(r.ToString()));
                            }
                        return tn;
                    case mcRegionReader.Tag.Type.TAG_List:
                        //tn.Text += "<TAG_List><" + Enum.GetName(typeof(Tag.Type),tag.listType)+">";
                        tn.Text += $" <{Enum.GetName(typeof(Tag.Type), tag.listType)}>";
                        tn.Tag = "";
                        foreach (Tag r in (Tag[])tag.value)
                        {
                                tn.Nodes.Add(addToTree(r));
                        }
                        return tn;
                    case mcRegionReader.Tag.Type.TAG_Compound:
                        //tn.Text += "<TAG_Compound>";
                        tn.Tag = "";
                        foreach (Tag r in (Tag[])tag.value)
                        {
                            if (r.type!=mcRegionReader.Tag.Type.TAG_End)
                            {
                                tn.Nodes.Add(addToTree(r));
                            }
                        }
                        return tn;
                    case mcRegionReader.Tag.Type.TAG_Int_Array:
                        //tn.Text += "<TAG_Int_Array>";
                        tn.Tag = "";
                        foreach (int r in (int[])tag.value)
                        {
                            tn.Nodes.Add(new TreeNode(r.ToString()));
                        }
                        return tn;
                    case mcRegionReader.Tag.Type.TAG_Long_Array:
                        //tn.Text += "<TAG_Long_Array>";
                        tn.Tag = "";
                        foreach (long r in (long[])tag.value)
                        {
                            tn.Nodes.Add(new TreeNode(r.ToString()));
                        }
                        return tn;
                    default:
                        //tn.Text += "<"+Enum.GetName(typeof(Tag.Type),tag.type)+">";
                        tn.Tag = tag.value;
                        return tn;
                }
                return null;
            }
            return null;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            Size size = JsonMapper.ToObject<Size>(File.ReadAllText(@"images\typeIcon.json"));
            treeView1.ImageList = new ImageList();
            treeView1.ImageList.ImageSize = new Size(18,25);
            Image[] images = MyGraphics.SplitImage(Image.FromFile(@"images\typeIcon.bmp"), size);
            foreach (Image img in images)
            {
                treeView1.ImageList.Images.Add(img);
            }
            
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            e.Node.ImageIndex = e.Node.ImageIndex;
            if (e.Node.Tag != null)
            {
                textBox3.Text = e.Node.Tag.ToString();
            }
            else
            {
                textBox3.Text = e.Node.Text;
            }
        }

        RegionFile RF;
        private void button6_Click(object sender, EventArgs e)
        {
            FileStream file = new FileStream(textBox1.Text, FileMode.Open, FileAccess.Read);
            RF = new RegionFile();
            RF.ReadLocations(file);
            RF.getChunkByLocation(file);
            foreach (RegionFile_Chunk chunk in RF.chunks)
            {
                //chunk.GetTags();
                treeView1.Nodes.Add(addToTree(chunk.tag));
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            a = null;
            textBox1.Text = "";
            treeView1.Nodes.Clear();
            textBox3.Text = "";
            GC.Collect();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Tag biomes=a.findTagByName("Level").findTagByName("Biomes");
            int i = 0;
            int currentBiome=-1;

            Bitmap chunkpic = new Bitmap(16, 16);
            for (int y = 0; y < 16;y++)
            {
                for (int x = 0; x < 16; x++)
                {
                    currentBiome = ((int[])biomes.getValue())[i];
                    chunkpic.SetPixel(x,y,Color.FromArgb(currentBiome,currentBiome,currentBiome));
                    i++;
                }
            }
            pictureBox1.BackgroundImage= chunkpic;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Tag biomes;
            Bitmap pic = new Bitmap(16 * 32, 16 * 32);
            int i = 0;
            int currentBiome = -1;
            int chunkX = 0, chunkZ = 0;
            foreach (RegionFile_Chunk chunk in RF.chunks)
            {
                biomes=chunk.tag.findTagByName("Level").findTagByName("Biomes");
                i = 0;
                chunkX = (int)chunk.tag.findTagByName("Level").findTagByName("xPos").getValue();
                chunkZ = (int)chunk.tag.findTagByName("Level").findTagByName("zPos").getValue();
                Bitmap chunkpic = new Bitmap(16, 16);
                for (int y = 0; y < 16; y++)
                {
                    for (int x = 0; x < 16; x++)
                    {
                        if (((int[])biomes.getValue()).Length==256)
                        {
                            currentBiome = ((int[])biomes.getValue())[i];
                            chunkpic.SetPixel(x, y, Color.FromArgb(currentBiome, currentBiome, currentBiome));
                        }
                        i++;
                    }
                }
                pic = (Bitmap)MyGraphics.CombineBitmap(chunkpic, pic, chunkX * 16, chunkZ * 16);

            }
            //Tag biomes = RF.chunks.findTagByName("Level").findTagByName("Biomes");

            pictureBox1.BackgroundImage = pic;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //byte[] level=Deflactor.DeCompress(Deflactor.ZLibToDeflactor( File.ReadAllBytes(textBox1.Text)));
            //Tag leveltags = mcRegionReader.Tag.readFrom(new MemoryStream(level));
            //treeView1.Nodes.Add(addToTree(leveltags));

            FileStream fs = new FileStream(textBox1.Text,FileMode.Open);
            using (GZipStream zipStream = new GZipStream(fs, CompressionMode.Decompress))
            {
                byte[] level = StreamToBytes(fs);
                level = Decompress(level);
                Tag leveltags = mcRegionReader.Tag.readFrom(new MemoryStream(level));
                treeView1.Nodes.Add(addToTree(leveltags));
            }

        }


        public byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        public static byte[] Decompress(byte[] bytes)
        {
            using (var compressStream = new MemoryStream(bytes))
            {
                using (var zipStream = new GZipStream(compressStream, CompressionMode.Decompress))
                {
                    using (var resultStream = new MemoryStream())
                    {
                        zipStream.CopyTo(resultStream);
                        return resultStream.ToArray();
                    }
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            List<Biome> BS = new List<Biome>();
            JsonData test=JsonMapper.ToObject(File.ReadAllText(@"data\biomes.json"));
            JsonData js=test["biomes"];
            foreach (JsonData b in js)
            {
                BS.Add(JsonMapper.ToObject<Biome>(b.ToJson()));
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            new BiomesModifyForm().Show();
        }
    }








}
