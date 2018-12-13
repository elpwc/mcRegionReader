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
            List<Tag> tags = new List<Tag>();
            tags.Add(new Tag(TagType.TAG_Int, "int", 12345));
            tags.Add(new Tag(TagType.TAG_Short, "short", (short)1234));
            tags.Add(new Tag(TagType.TAG_String, "string", "hello"));
            tags.Add(new Tag(TagType.TAG_Float, "float", 1.234f));
            tags.Add(new Tag(TagType.TAG_Long, "long", (long)123213));
            tags.Add(new Tag(TagType.TAG_End, "", null));
            a= new Tag(TagType.TAG_Compound, "test", tags.ToArray());
            treeView1.Nodes.Add(AddToTree(a));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Stream output=new FileStream("res",FileMode.Create,FileAccess.Write);
            a.WriteTo(output);
            output.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            a = RegionFile_Chunk.ReadTags(File.ReadAllBytes(textBox1.Text));
            treeView1.Nodes.Add(AddToTree(a));
        }

        //List<RegionFile> overworld = new List<RegionFile>();
        Tag a;
        private Bitmap chunkpic;

        List<RegionFile> region = new List<RegionFile>();

        private void button5_Click(object sender, EventArgs e)
        {
            RF = RegionFile.FromMcaFile(textBox1.Text);
            a =RF.chunksData[0].tag;
            
            treeView1.Nodes.Add(AddToTree(a));
        }

        public TreeNode AddToTree(Tag tag)
        {
            if (tag!=null)
            {
                
                TreeNode tn = new TreeNode(tag.name);

                tn.ImageIndex = (int)tag.type - 1 ;

                switch (tag.type)
                {
                    case TagType.TAG_End:
                        break;
                    case TagType.TAG_Byte_Array:
                        //tn.Text += "<TAG_Byte_Array>";
                        tn.Tag = "";
                        foreach (byte r in (byte[])tag.value)
                        {
                                tn.Nodes.Add(new TreeNode(r.ToString()));
                            }
                        return tn;
                    case TagType.TAG_List:
                        //tn.Text += "<TAG_List><" + Enum.GetName(typeof(Tag.Type),tag.listType)+">";
                        tn.Text += $" <{Enum.GetName(typeof(TagType), tag.listType)}>";
                        tn.Tag = "";
                        foreach (Tag r in (Tag[])tag.value)
                        {
                                tn.Nodes.Add(AddToTree(r));
                        }
                        return tn;
                    case TagType.TAG_Compound:
                        //tn.Text += "<TAG_Compound>";
                        tn.Tag = "";
                        foreach (Tag r in (Tag[])tag.value)
                        {
                            if (r.type!=TagType.TAG_End)
                            {
                                tn.Nodes.Add(AddToTree(r));
                            }
                        }
                        return tn;
                    case TagType.TAG_Int_Array:
                        //tn.Text += "<TAG_Int_Array>";
                        tn.Tag = "";
                        foreach (int r in (int[])tag.value)
                        {
                            tn.Nodes.Add(new TreeNode(r.ToString()));
                        }
                        return tn;
                    case TagType.TAG_Long_Array:
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
            InitializeGlobal();
            Size size = JsonMapper.ToObject<Size>(File.ReadAllText(@"images\typeIcon.json"));
            treeView1.ImageList = new ImageList();
            treeView1.ImageList.ImageSize = new Size(18, 25);
            Image[] images = MyGraphics.SplitImage(Image.FromFile(@"images\typeIcon.bmp"), size);
            foreach (Image img in images)
            {
                treeView1.ImageList.Images.Add(img);
            }

        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //e.Node.ImageIndex = e.Node.ImageIndex;
            //treeView1.SelectedImageIndex = e.Node.ImageIndex;
            if (e.Node.ImageIndex ==6 || e.Node.ImageIndex == 10 || e.Node.ImageIndex == 11)
            {
                textBox3.Text = e.Node.Nodes.Count.ToString();
            }
            else if (e.Node.Tag != null)
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
            RF = RegionFile.FromMcaFile(textBox1.Text);
            //foreach (RegionFile_Chunk chunk in RF.chunks)
            //{
            //    treeView1.Nodes.Add(addToTree(chunk.tag));
            //}

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
            Tag biomes=a.FindTagByName("Level").FindTagByName("Biomes");
            int i = 0;
            int currentBiome=-1;

            Bitmap chunkpic = new Bitmap(16, 16);
            for (int y = 0; y < 16;y++)
            {
                for (int x = 0; x < 16; x++)
                {
                    currentBiome = ((byte[])biomes.GetValue())[i];
                    chunkpic.SetPixel(x,y,Color.FromArgb(currentBiome,currentBiome,currentBiome));
                    i++;
                }
            }
            pictureBox1.BackgroundImage= chunkpic;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = RF.GetBiomeImage();
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
                level =Deflactor.Decompress(level);
                Tag leveltags = mcRegionReader.Tag.ReadFrom(new MemoryStream(level));
                treeView1.Nodes.Add(AddToTree(leveltags));
            }

        }


        public byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }
        

        private void button12_Click(object sender, EventArgs e)
        {
            new BiomesModifyForm().Show();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(textBox1.Text+"\\region");
            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                region.Add(RegionFile.FromMcaFile(file.FullName, 1));
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            if (Directory.Exists( folderBrowserDialog1.SelectedPath))
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            int minX = 0, minZ = 0, maxX = 0, maxZ = 0;
            foreach (RegionFile eachRegionFile in region)
            {
                minX = eachRegionFile.regionX < minX ? eachRegionFile.regionX : minX;
                minZ = eachRegionFile.regionZ < minZ ? eachRegionFile.regionZ : minZ;
                maxX = eachRegionFile.regionX > maxX ? eachRegionFile.regionX : maxX;
                maxZ = eachRegionFile.regionZ > maxZ ? eachRegionFile.regionZ : maxZ;
            }
            chunkpic = new Bitmap((maxX - minX + 1) * 512, (maxZ - minZ + 1) * 512);
            foreach (RegionFile eachRegionFile in region)
            {
                chunkpic = (Bitmap)MyGraphics.CombineBitmap(eachRegionFile.GetBiomeImage(),chunkpic,(eachRegionFile.regionX-minX)*512, (eachRegionFile.regionZ - minZ) * 512);
            }
            pictureBox1.BackgroundImage = chunkpic;

        }

        public byte ReverseBits(byte i)
        {
            byte c = i;
            c = (byte)((c & 0x55) << 1 | (c & 0xAA) >> 1);
            c = (byte)((c & 0x33) << 2 | (c & 0xCC) >> 2);
            c = (byte)((c & 0x0F) << 4 | (c & 0xF0) >> 4);
            return c;
        }

        
        private void button16_Click(object sender, EventArgs e)
        {
            chunk1.GetBlocks(a);

        }
        Chunk chunk1 = new Chunk();


        private void button18_Click(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage= chunk1.GetMap(trackBar1.Value);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = chunk1.GetMap(trackBar1.Value);
        }


        public void InitializeGlobal()
        {
            GlobalVar.biomes = JsonUtil.ReadBiomes();
            GlobalVar.blocks_old = JsonUtil.ReadBlocks_old();
            GlobalVar.blocks_flattening = JsonUtil.ReadBlocks_flattening();
        }

        private void button11_Click(object sender, EventArgs e)
        {

            pictureBox1.BackgroundImage = RF.GetMap(trackBar2.Value);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            RF.GetChunks();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = RF.GetMap(trackBar2.Value);
        }
    }

    
}
