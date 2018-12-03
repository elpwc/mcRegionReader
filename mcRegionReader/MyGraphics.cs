using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace mcRegionReader
{
    /// <summary>
    /// 提供一些图像操作方法。
    /// </summary>
    public static class MyGraphics
    {
        /// <summary>
        /// 将图像组分割为指定大小的图像列表
        /// </summary>
        /// <param name="image">原图像</param>
        /// <param name="size">子图像尺寸</param>
        /// <returns>子图像列表</returns>
        public static Image[] SplitImage(Image image, Size size)
        {
            return SplitImage(image, size.Width, size.Height);
        }

        /// <summary>
        /// 将图像组分割为指定大小的图像列表
        /// </summary>
        /// <param name="image">原图像</param>
        /// <param name="width">子图像宽度</param>
        /// <param name="height">子图像高度</param>
        /// <returns>子图像列表</returns>
        public static Image[] SplitImage(Image image, int width, int height)
        {
            List<Image> images = new List<Image>();

            for (int y = 0; y < (int)Math.Ceiling((double)image.Height / height); y++)
            {
                for (int x = 0; x < (int)Math.Ceiling((double)image.Width / width); x++)
                {
                    images.Add(CaptureImage(image, x * width, y * height, width, height));
                }
            }
            return images.ToArray();

        }

        /// <summary>
        /// 从图像中截取出一部分
        /// </summary>
        /// <param name="formerImage">原图像</param>
        /// <param name="offsetX">截取区域左上角x坐标</param>
        /// <param name="offsetY">截取区域左上角y坐标</param>
        /// <param name="width">截取区域宽度</param>
        /// <param name="height">截取区域高度</param>
        /// <returns>截取后的图像</returns>
        public static Image CaptureImage(Image formerImage, int offsetX, int offsetY, int width, int height)
        {
            using (Bitmap bitmap = new Bitmap(width, height))
            {
                using (Graphics graphic = Graphics.FromImage(bitmap))
                {
                    graphic.DrawImage(formerImage, 0, 0, new Rectangle(offsetX, offsetY, width, height), GraphicsUnit.Pixel);
                }
                return Image.FromHbitmap(bitmap.GetHbitmap());
            }

        }

        /// <summary>
        /// 合并图像
        /// </summary>
        /// <param name="A">覆盖图片</param>
        /// <param name="B">被覆盖图片</param>
        /// <param name="x">覆盖图片左上角x坐标</param>
        /// <param name="y">覆盖图片左上角y坐标</param>
        /// <returns></returns>
        public static Image CombineBitmap(Image A, Image B,/* Color Transparent,*/ int x, int y)
        {
            using (Image res = B)
            {
                using (Graphics g = Graphics.FromImage(res))
                {
                    g.DrawImage(A, x, y);
                    g.Dispose();
                }
                return res;
            }

        }

        /// <summary>
        /// 按照像素调整图像大小
        /// </summary>
        /// <param name="bmp">原图片</param>
        /// <param name="newW">新宽度</param>
        /// <param name="newH">新高度</param>
        /// <returns></returns>
        public static Image KiResizeImage(Image bmp, int newW, int newH)
        {
            try
            {
                using (Image b = new Bitmap(newW, newH))
                {
                    using (Graphics g = Graphics.FromImage(b))
                    {
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                    }
                    return b;
                }

            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// 画一个点~
        /// </summary>
        /// <param name="g">要绘制的Graphics</param>
        /// <param name="c">颜色哦~</param>
        /// <param name="site">点的位置~</param>
        /// <param name="width">宽度..</param>
        /// <param name="type">类型：0 圆  1 方</param>
        public static void DrawDot(Graphics g, Color c, Point site, int width, int type = 0)
        {
            if (type == 0)
            {
                g.FillEllipse(new SolidBrush(c), new RectangleF(site.X - width / 2, site.Y - width / 2, width, width));
            }
            else if (type == 1)
            {
                g.FillRectangle(new SolidBrush(c), new RectangleF(site.X - width / 2, site.Y - width / 2, width, width));
            }
        }
    }
}
