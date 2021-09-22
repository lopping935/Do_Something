using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Core;
using logtest;
using AnBRobotSystem;
using AnBRobotSystem.Utlis;
using VM.PlatformSDKCS;
using OpenCvSharp;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using SDK;
namespace AnBRobotSystem.Core
{
    

    public class Tiebao
    {
        public updatelistiew writelistview;
        
        public double area = 0;
        public int Inital_light = 0;
        public int Fall_edga_light = 0;
        public double Set_circ_Dia = 0;
        public double Real_circ_Dia = 0;
        public bool Tiebao_ok = false;
        public Tiebao()
        {
          //  updata1 = MdiParent.form.mainlog;
        }
   
        public bool TB_init_result()
        {
            try
            {
                if (null == MdiParent.process_TB)
                {
                    return false;
                }
                else
                {
                    MdiParent.process_TB.Run();

                    IntResultInfo TB_info_result = MdiParent.process_TB.GetIntOutputResult("tb_edg_result");

                    int TB_result = TB_info_result.pIntValue[0];
                    if(TB_result==1)
                    {
                        string strMsg = "铁包边缘检测结果:成功";
                        writelistview("铁包视觉", strMsg, "log");
                        return true;
                    }
                    else
                    {
                        string strMsg = "铁包边缘检测结果:失败" ;
                        writelistview("铁包视觉", strMsg, "log");
                        return false;
                    }
                   
                    
                }
            }
            catch (VmException ex)
            {
                writelistview("包口视觉", ex.errorMessage, "err");
                string strMsg = "SaveSolution failed. Error Code: " + Convert.ToString(ex.errorCode, 16);
                LogHelper.WriteLog(strMsg, ex);
                return false;
            }



        }
        public void countiontest()
        {
           
        }
        public void TL_in_TB()
        {
            while (true)
            {
               
            }

        }
    }
    public class GuanKou
    {
        public updatelistiew writelistview;
        public  double area = 0;
        public  int Inital_light = 0;
        public  Single Fall_edga_light = 0;
        public  string shape = "";
        public bool GK_init_result(string GK_station)
        {
            try
            {
                if(GK_station=="A")
                {
                    if (null == MdiParent.process_GK)
                    {
                        return false;
                    }
                    else
                    {

                        MdiParent.process_GK.Run();
                        IntResultInfo GB_A_GK_result = MdiParent.process_GK.GetIntOutputResult("GB_A_GK");

                        int GB_A_GK = GB_A_GK_result.pIntValue[0];
                        if (GB_A_GK == 1)
                        {
                            string strMsg = "罐口检测结果:成功";
                            writelistview("罐口视觉", strMsg, "log");
                            return true;
                        }
                        else
                        {
                            string strMsg = "罐口检测结果:失败";
                            writelistview("罐口视觉", strMsg, "log");
                            return false;
                        }
                    }
                }
                else
                {
                    if (null == MdiParent.process_GK)
                    {
                        return false;
                    }
                    else
                    {

                        MdiParent.process_GK.Run();
                        IntResultInfo GB_B_GK_result = MdiParent.process_GK.GetIntOutputResult("GB_B_GK");

                        int GB_B_GK = GB_B_GK_result.pIntValue[0];
                        if (GB_B_GK == 1)
                        {
                            string strMsg = "罐口检测结果:成功";
                            writelistview("罐口视觉", strMsg, "log");
                            return true;
                        }
                        else
                        {
                            string strMsg = "罐口检测结果:失败";
                            writelistview("罐口视觉", strMsg, "log");
                            return false;
                        }
                    }
                }  

            }
            catch (VmException ex)
            {
                writelistview("罐口视觉", ex.errorMessage, "err");
                string strMsg = "SaveSolution failed. Error Code: " + Convert.ToString(ex.errorCode, 16);
                LogHelper.WriteLog(strMsg, ex);
                return false;
            }

        }
        public bool set_GK_contiu()
        {
            if (null == MdiParent.process_GK) return false;
            if(MdiParent.process_GK.ContinuousRunEnable)
            {
                return true;
            }
            else
            {
                MdiParent.process_GK.ContinuousRunEnable = true;

                if (MdiParent.process_GK.ContinuousRunEnable)
                    return true;
                else
                    return false;
            }
           
        }
        public bool GK_light_result(string GB_flag)
        {

            try
            {
                if (null == MdiParent.process_GK)
                {
                    return false;
                }
                else
                { 
                    if(GB_flag=="A")
                    {
                        IntResultInfo GB_A_edg_result = MdiParent.process_GK.GetIntOutputResult("GB_A_edg");
                        IntResultInfo GB_A_edg_R_result = MdiParent.process_GK.GetIntOutputResult("GB_A_edg_R");
                        int GB_A_edg_R = GB_A_edg_R_result.pIntValue[0];
                        if (GB_A_edg_R == 1)
                        {
                            Fall_edga_light = GB_A_edg_result.pIntValue[0];
                            string strMsg = "边缘亮度:"+ GB_A_edg_result.pIntValue[0];
                            writelistview("罐口视觉", strMsg, "log");
                            return true;
                        }
                        else
                        {
                            string strMsg = "边缘亮度:失败";
                            writelistview("罐口视觉", strMsg, "log");
                            return false;
                        }
                    }
                    else
                    {
                        IntResultInfo GB_B_edg_result = MdiParent.process_GK.GetIntOutputResult("GB_B_edg");
                        IntResultInfo GB_B_edg_R_result = MdiParent.process_GK.GetIntOutputResult("GB_B_edg_R");
                        int GB_B_edg_R = GB_B_edg_R_result.pIntValue[0];
                        if (GB_B_edg_R == 1)
                        {
                            Fall_edga_light = GB_B_edg_result.pIntValue[0];
                            string strMsg = "边缘亮度:" + GB_B_edg_result.pIntValue[0];
                            writelistview("罐口视觉", strMsg, "log");
                            return true;
                        }
                        else
                        {
                            string strMsg = "边缘亮度:失败";
                            writelistview("罐口视觉", strMsg, "log");
                            return false;
                        }
                    }
                   
                }
            }
            catch (VmException ex)
            {
                string strMsg = "SaveSolution failed. Error Code: " + Convert.ToString(ex.errorCode, 16);
                LogHelper.WriteLog(strMsg, ex);
                return false;
            }
        }
    }
    public class TieLiu
    {
        private GroupSDK.DelegateNewFrame NewFrame = null;
        public MagDevice m1 = new MagDevice(IntPtr.Zero);

        public updatelistiew writelistview;
        public bool Get_iron = false;
        public bool task_run_flag = false;
        public double area = 0;
        public static string shape = "";
        public VideoCapture camer_cap;

        public TieLiu()
        {
            m1.Initialize();
            CreateDevice();
            m1.LinkCamera("192.168.1.3", 2000);
        }

        #region 热成像仪数据处理函数
        /// <summary>
        /// 将源图像灰度化，并转化为8位灰度图像。
        /// </summary>
        /// <param name="original"> 源图像。 </param>
        /// <returns> 8位灰度图像。 </returns>
        public Bitmap RgbToGrayScale(Bitmap original)
        {
            if (original != null)
            {
                // 将源图像内存区域锁定
                Rectangle rect = new Rectangle(0, 0, original.Width, original.Height);
                BitmapData bmpData = original.LockBits(rect, ImageLockMode.ReadOnly,
                     original.PixelFormat);

                // 获取图像参数
                int width = bmpData.Width;
                int height = bmpData.Height;
                int stride = bmpData.Stride;  // 扫描线的宽度
                int offset = stride - width * 3;  // 显示宽度与扫描线宽度的间隙
                IntPtr ptr = bmpData.Scan0;   // 获取bmpData的内存起始位置
                int scanBytes = stride * height;  // 用stride宽度，表示这是内存区域的大小

                // 分别设置两个位置指针，指向源数组和目标数组
                int posScan = 0, posDst = 0;
                byte[] rgbValues = new byte[scanBytes];  // 为目标数组分配内存
                Marshal.Copy(ptr, rgbValues, 0, scanBytes);  // 将图像数据拷贝到rgbValues中
                                                             // 分配灰度数组
                byte[] grayValues = new byte[width * height]; // 不含未用空间。
                                                              // 计算灰度数组
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        double temp = rgbValues[posScan++] * 0.11 +
                            rgbValues[posScan++] * 0.59 +
                            rgbValues[posScan++] * 0.3;
                        grayValues[posDst++] = (byte)temp;
                    }
                    // 跳过图像数据每行未用空间的字节，length = stride - width * bytePerPixel
                    posScan += offset;
                }

                // 内存解锁
                Marshal.Copy(rgbValues, 0, ptr, scanBytes);
                original.UnlockBits(bmpData);  // 解锁内存区域

                // 构建8位灰度位图
                Bitmap retBitmap = BuiltGrayBitmap(grayValues, width, height);
                return retBitmap;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 用灰度数组新建一个8位灰度图像。
        /// http://www.cnblogs.com/spadeq/archive/2009/03/17/1414428.html
        /// </summary>
        /// <param name="rawValues"> 灰度数组(length = width * height)。 </param>
        /// <param name="width"> 图像宽度。 </param>
        /// <param name="height"> 图像高度。 </param>
        /// <returns> 新建的8位灰度位图。 </returns>
        public Bitmap BuiltGrayBitmap(byte[] rawValues, int width, int height)
        {
            // 新建一个8位灰度位图，并锁定内存区域操作
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, width, height),
                 ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

            // 计算图像参数
            int offset = bmpData.Stride - bmpData.Width;        // 计算每行未用空间字节数
            IntPtr ptr = bmpData.Scan0;                         // 获取首地址
            int scanBytes = bmpData.Stride * bmpData.Height;    // 图像字节数 = 扫描字节数 * 高度
            byte[] grayValues = new byte[scanBytes];            // 为图像数据分配内存

            // 为图像数据赋值
            int posSrc = 0, posScan = 0;                        // rawValues和grayValues的索引
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    grayValues[posScan++] = rawValues[posSrc++];
                }
                // 跳过图像数据每行未用空间的字节，length = stride - width * bytePerPixel
                posScan += offset;
            }

            // 内存解锁
            Marshal.Copy(grayValues, 0, ptr, scanBytes);

            bitmap.UnlockBits(bmpData);  // 解锁内存区域

            // 修改生成位图的索引表，从伪彩修改为灰度
            ColorPalette palette;
            // 获取一个Format8bppIndexed格式图像的Palette对象
            using (Bitmap bmp = new Bitmap(1, 1, PixelFormat.Format8bppIndexed))
            {
                palette = bmp.Palette;
            }
            for (int i = 0; i < 256; i++)
            {
                palette.Entries[i] = Color.FromArgb(i, i, i);
            }
            // 修改生成位图的索引表
            bitmap.Palette = palette;

            return bitmap;
        }
        //截取bmp图像部分数据，去bmp头部  灰度图处理
        public byte[] Bitmap2Byte(Bitmap bitmap)
        {
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData bmpData = bitmap.LockBits(rect, ImageLockMode.ReadOnly,bitmap.PixelFormat);

            // 获取图像参数
            int width = bmpData.Width;
            int height = bmpData.Height;
            int stride = bmpData.Stride;  // 扫描线的宽度
            int offset = stride - width;  // 显示宽度与扫描线宽度的间隙
            IntPtr ptr = bmpData.Scan0;   // 获取bmpData的内存起始位置
            int scanBytes = stride * height;  // 用stride宽度，表示这是内存区域的大小

            // 分别设置两个位置指针，指向源数组和目标数组
            int posScan = 0, posDst = 0;
            byte[] rgbValues = new byte[scanBytes];  // 为目标数组分配内存
            Marshal.Copy(ptr, rgbValues, 0, scanBytes);
            return rgbValues;
        }
        //截取bmp图像部分数据，去bmp头部  彩色图处理
        public byte[] Bitmap2Byte_clor(Bitmap bitmap)
        {
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData bmpData = bitmap.LockBits(rect, ImageLockMode.ReadOnly,bitmap.PixelFormat);

            // 获取图像参数
            int width = bmpData.Width;
            int height = bmpData.Height;
            int stride = bmpData.Stride;  // 扫描线的宽度
            int offset = stride - width * 3;  // 显示宽度与扫描线宽度的间隙
            IntPtr ptr = bmpData.Scan0;   // 获取bmpData的内存起始位置
            int scanBytes = stride * height;  // 用stride宽度，表示这是内存区域的大小

            // 分别设置两个位置指针，指向源数组和目标数组
            int posScan = 0, posDst = 0;
            byte[] rgbValues = new byte[scanBytes];  // 为目标数组分配内存
            Marshal.Copy(ptr, rgbValues, 0, scanBytes);
            return rgbValues;
        }
        public  bool CreateDevice()
        {
            if (m1 == null)
            {
                m1 = new MagDevice(IntPtr.Zero);
            }

            if (NewFrame == null)
            {
                NewFrame = new GroupSDK.DelegateNewFrame(NewFrameCome);
            }

            return m1.Initialize();
        }
        private void NewFrameCome(uint hDevice, int intCamTemp, int intFFCCounter, int intCamState, int intStreamType, IntPtr pUserData)
        {
           // this.Invalidate(false);
        }
        //从热成像中获取数据
        public bool Play(MagDevice mDevice)
        {
            GroupSDK.CAMERA_INFO cam_info = mDevice.GetCamInfo();

            GroupSDK.OUTPUT_PARAM param = new GroupSDK.OUTPUT_PARAM();
            param.intFPAWidth = (uint)cam_info.intFPAWidth;
            param.intFPAHeight = (uint)cam_info.intFPAHeight;
            param.intBMPWidth = (uint)cam_info.intVideoWidth;
            param.intBMPHeight = (uint)cam_info.intVideoHeight;
            param.intColorbarWidth = 20;
            param.intColorbarHeight = 100;
            mDevice.SetSubsectionEnlargePara(30, 60000, 0, 255);
            if (mDevice.StartProcessImage(param, NewFrame, (uint)GroupSDK.STREAM_TYPE.STREAM_TEMPERATURE, IntPtr.Zero))
            {
                mDevice.SetColorPalette(GroupSDK.COLOR_PALETTE.GRAY1);
                return true;
            }

            return false;
        }
        #endregion
        public void get_tl_img()
        {
            //Thread.Sleep(500);
            Play(m1);
            Bitmap b1 = m1.GetOutputBMPBitmap();
            uint dataLen = (uint)(b1.Width * b1.Height * 3);
            byte[] imagedata = Bitmap2Byte_clor(b1);
            ImageBaseData StImg = new ImageBaseData(imagedata, dataLen, b1.Width, b1.Height, 2);
            MdiParent.TL_img_sdk.SetImageData(StImg);
            MdiParent.process_TL.Run();
        }
        public bool TL_light_result()
        {
            
            try
            {
                
                 if (null == MdiParent.process_TL)
                {
                    return false;
                }
                else
                {
                    
                    Play(m1);
                    Bitmap b1 = m1.GetOutputBMPBitmap();
                    uint dataLen = (uint)(b1.Width * b1.Height * 3);
                    byte[] imagedata = Bitmap2Byte_clor(b1);
                   
                    ImageBaseData StImg = new ImageBaseData(imagedata, dataLen, b1.Width, b1.Height, 2);
                    MdiParent.TL_img_sdk.SetImageData(StImg);
                    MdiParent.process_TL.Run();
                    IntResultInfo blob_num_info = MdiParent.process_TL.GetIntOutputResult("blob_num");
                    int blob_num = blob_num_info.pIntValue[0];
                    if (blob_num == 1)
                    {
                        // string strMsg = "铁流检测结果:成功";
                        // writelistview("铁流视觉", strMsg, "log");
                        Get_iron = true;
                        return true;

                    }
                    else
                    {
                        //string strMsg = "铁流检测结果:失败";
                        // writelistview("铁流视觉", strMsg, "log");
                        Get_iron = false;
                        return true;
                    }

                }
            }
            catch (VmException ex)
            {
                string strMsg = "SaveSolution failed. Error Code: " + Convert.ToString(ex.errorCode, 16);
                LogHelper.WriteLog(strMsg, ex);
                Get_iron = false;
                return false;
            }

        }
       
    }
}
