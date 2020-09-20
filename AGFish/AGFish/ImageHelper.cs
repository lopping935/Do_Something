using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using iMVS_6000PlatformSDKCS;
using Color = System.Drawing.Color;

namespace FrontedUI
{
    /// <summary>
    /// 图像数据
    /// </summary>
    public class ImageData
    {
        public byte[] ImageBuffer = null; // 图像buffer
        public int Width;                 // 图像宽度
        public int Height;                // 图像高度
        public MPixelFormat PixelFormat = MPixelFormat.Gray8;    //图像像素格式
    }

    /// <summary>
    /// 圆数据
    /// </summary>
    public class CircleData
    {
        //圆
        public float radius = float.NaN;
        public float centerx = float.NaN;
        public float centery = float.NaN;
    }
    public class OCRString
    {
        public int charnum=0;
        public ImvsSdkPFDefine.IMVS_PF_OCR_RES_INFO[] String_info=null;
    }
    /// <summary>
    /// 特征匹配数据
    /// </summary>
    public class FeatureMatchData
    {
        //轮廓点
        public float[] EdgePointX = null;
        public float[] EdgePointY = null;

        //匹配框
        public float[] MatchBoxCenterX = null;
        public float[] MatchBoxCenterY = null;
        public float[] MatchBoxWidth = null;
        public float[] MatchBoxHeight = null;
        public float[] MatchBoxAngle = null;

        //匹配点
        public float[] MatchPointX = null;
        public float[] MatchPointY = null;

        //匹配轮廓信息
        public ImvsSdkPFDefine.IMVS_PATMATCH_POINT_INFO[] outLinePointInfo = null;
    }
    

    /// <summary>
    /// 像素格式
    /// </summary>
    public enum MPixelFormat
    {
        Gray8,
        Rgb24
    }

    /// <summary>
    /// 数据转化为位图
    /// </summary>
    public static class Byte2Bitmap
    {
        /// <summary>
        /// 将位图转化为32位的位图
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static Bitmap GetArgb32BitMap(this Bitmap img)
        {

            Bitmap bmp = new Bitmap(img.Width, img.Height, PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.DrawImage(img, 0, 0);
            }
            img.Dispose();
            return bmp;
        }

        /// <summary>
        /// 将bytes转化Bitmap
        /// </summary>
        /// <param name="imageData"></param>
        /// <returns></returns>
        public static Bitmap ImageDataToBitmap(this ImageData imageData)
        {
            if (imageData == null)
                return null;
            var is24Bit = imageData.PixelFormat==MPixelFormat.Rgb24;
            var width = imageData.Width;
            var height = imageData.Height;
            return is24Bit
                ? ToRGBBitmap(imageData.ImageBuffer, width, height)
                : ToGrayBitmap(imageData.ImageBuffer, width, height);
        }

        /// <summary>
        /// 将bytes转化Bitmap(灰度图)
        /// </summary>
        /// <param name="imageData"></param>
        /// <returns></returns>
        private static Bitmap ToGrayBitmap(byte[] rawValues, int width, int height)
        {
            var bmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
            var bmpData = bmp.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);

            //// 获取图像参数  
            var stride = bmpData.Stride;  // 扫描线的宽度  
            var offset = stride - width;  // 显示宽度与扫描线宽度的间隙  
            var iptr = bmpData.Scan0;  // 获取bmpData的内存起始位置  
            var scanBytes = stride * height;// 用stride宽度，表示这是内存区域的大小  

            //// 下面把原始的显示大小字节数组转换为内存中实际存放的字节数组  
            int posScan = 0, posReal = 0;// 分别设置两个位置指针，指向源数组和目标数组  
            var pixelValues = new byte[scanBytes];  //为目标数组分配内存  

            for (var x = 0; x < height; x++)
            {
                //// 下面的循环节是模拟行扫描  
                for (var y = 0; y < width; y++)
                {
                    pixelValues[posScan++] = rawValues[posReal++];
                }
                posScan += offset;  //行扫描结束，要将目标位置指针移过那段“间隙”  
            }

            //// 用Marshal的Copy方法，将刚才得到的内存字节数组复制到BitmapData中  
            Marshal.Copy(pixelValues, 0, iptr, scanBytes);
            bmp.UnlockBits(bmpData);  // 解锁内存区域  

            //// 下面的代码是为了修改生成位图的索引表，从伪彩修改为灰度  
            ColorPalette tempPalette;
            using (var tempBmp = new Bitmap(1, 1, System.Drawing.Imaging.PixelFormat.Format8bppIndexed))
            {
                tempPalette = tempBmp.Palette;
            }
            for (var i = 0; i < 256; i++)
            {
                tempPalette.Entries[i] = Color.FromArgb(i, i, i);
            }

            bmp.Palette = tempPalette;

            //// 算法到此结束，返回结果  
            return bmp;
        }

        /// <summary>
        /// 将bytes转化Bitmap(彩图)
        /// </summary>
        /// <param name="imageData"></param>
        /// <returns></returns>
        private static Bitmap ToRGBBitmap(byte[] rawValues, int width, int height)
        {
            var bmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            var bmpData = bmp.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            //// 获取图像参数  
            var stride = bmpData.Stride;  // 扫描线的宽度  
            var iptr = bmpData.Scan0;  // 获取bmpData的内存起始位置  
            var scanBytes = stride * height;// 用stride宽度，表示这是内存区域的大小  
            var offset = stride - width * 3;  // 显示宽度与扫描线宽度的间隙   

            //// 下面把原始的显示大小字节数组转换为内存中实际存放的字节数组  
            var pixelValues = new byte[scanBytes];  //为目标数组分配内存  
            var iPoint = 0;
            var iPoint1 = 0;

            for (var x = 0; x < height; x++)
            {
                //// 下面的循环节是模拟行扫描  
                for (var y = 0; y < width; y++)
                {
                    pixelValues[iPoint] = rawValues[iPoint1 + 2];
                    pixelValues[iPoint + 1] = rawValues[iPoint1 + 1];
                    pixelValues[iPoint + 2] = rawValues[iPoint1];
                    iPoint += 3;
                    iPoint1 += 3;
                }
                //行扫描结束，要将目标位置指针移过那段“间隙”
                iPoint += offset;
            }

            //// 用Marshal的Copy方法，将刚才得到的内存字节数组复制到BitmapData中  
            Marshal.Copy(pixelValues, 0, iptr, scanBytes);
            bmp.UnlockBits(bmpData);

            // 解锁内存区域  
            //// 算法到此结束，返回结果  
            return bmp;
        }
    }
}
