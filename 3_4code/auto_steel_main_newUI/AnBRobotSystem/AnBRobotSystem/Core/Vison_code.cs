using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Core;
using logtest;
using AnBRobotSystem;
using AnBRobotSystem.Utlis;
using System.Threading;
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
                    writelistview("铁包视觉", "即将运行", "log");
                    MdiParent.process_TB.Run();
                    writelistview("铁包视觉", "运行通过", "log");
                    if (MdiParent.process_TB.GetIntOutputResult("tb_edg_state").pIntValue[0] == 1)
                    {
                        if (MdiParent.process_TB.GetIntOutputResult("tb_edg_result").pIntValue[0] == 1)
                        {
                            string strMsg = "铁包边缘检测结果:成功";
                            writelistview("铁包视觉", strMsg, "log");
                            Tiebao_ok = true;
                            return true;
                        }
                        else
                        {
                            string strMsg = "铁包边缘检测结果:成功";
                            writelistview("铁包视觉", strMsg, "log");
                            Tiebao_ok = false;
                            return false;
                        }
                    }
                    else
                    {
                        string strMsg = "铁包边缘程序运行失败！";
                        writelistview("铁包视觉", strMsg, "log");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                writelistview("包口视觉", "包口检测视觉程序运行出错！", "err");
                string strMsg = "包口检测视觉程序运行出错！";
                LogHelper.WriteLog(strMsg, ex);
                return false;
            }

        }

    }
    public class GuanKou
    {
        public updatelistiew writelistview;
        public double area = 0;
        public int Inital_light = 0;
        public Single Fall_edga_light = 0;
        public string shape = "";
        //罐口线程相关变量
        //罐口运行结果
        public bool GK_state = false, GK_edg_state = false;
        //罐口运行标志
        public bool start_GK_state = false, start_GK_edg_state = false;
        public int GK_flag = 0;
        public string GK_station = "";
        public Thread GK_thread, gk_th_test;
        public static bool GK_test_ok = false;
        public string GK_record_num = "";
        public GuanKou()
        {
            GK_thread = new Thread(GK_program);
            GK_thread.IsBackground = true;
            GK_thread.Start();
        }
        //罐口开机自检程序
        public void GK_runtest()
        {
            MdiParent.gk1.GK_station = "A";
            MdiParent.process_GK.Run();
            writelistview("主窗体", "罐口检测自运行通过！", "log");
            GK_test_ok = true;
        }
        public void GK_program()
        {
            while (true)
            {
                Thread.Sleep(100);
                if (GK_test_ok)
                {

                    try
                    {
                        if (start_GK_state)
                        {
                            start_GK_state = false;
                            GK_init_result();
                        }
                        else if (start_GK_edg_state)
                        {
                            start_GK_edg_state = false;
                            GK_light_result();
                        }
                    }
                    catch (Exception e)
                    {

                    }
                }

            }
        }
        //
        public void GK_init_result()
        {
            try
            {
                GK_state = false;
                Task.Run(() =>
                {
                    if (GK_station == "A")
                    {
                        if (null == MdiParent.process_GK)
                        {
                            return;
                        }
                        else
                        {
                            try
                            {
                                try
                                {
                                    MdiParent.process_GK.Run();
                                    MdiParent.GK_global.SetGlobalVar("GK_pos", GK_record_num);
                                    writelistview("罐口视觉", "a run通过！", "log");
                                }
                                catch (VmException vee)
                                {
                                    LogHelper.WriteLog("process_GK", vee);
                                }
                                if (MdiParent.process_GK.GetIntOutputResult("GB_A_state").pIntValue[0] == 1)
                                {
                                    if (MdiParent.process_GK.GetFloatOutputResult("GB_A_GK").pFloatValue[0] > 1000)
                                    {
                                        string strMsg = "A罐口检测结果:成功";
                                        writelistview("罐口视觉", strMsg, "log");
                                        GK_state = true;
                                        return;
                                    }
                                    else
                                    {
                                        string strMsg = "A罐口检测结果:失败";
                                        writelistview("罐口视觉", strMsg, "log");
                                        return;
                                    }

                                }
                                else
                                {
                                    string strMsg = "A罐口检测结果:失败";
                                    writelistview("罐口视觉", strMsg, "log");
                                    return;
                                }
                            }
                            catch (Exception vme)
                            {
                                string strMsg = "vision program busy!";//vme.errorMessage;
                                writelistview("罐口视觉", strMsg, "err");
                            }

                        }
                    }
                    else if (GK_station == "B")
                    {
                        if (null == MdiParent.process_GKB)
                        {
                            return;
                        }
                        else
                        {
                            try
                            {
                                try
                                {
                                    MdiParent.process_GKB.Run();
                                    MdiParent.GK_global.SetGlobalVar("GK_pos", GK_record_num);
                                    writelistview("罐口视觉", "b run通过！", "log");
                                }
                                catch (VmException vee)
                                {
                                    LogHelper.WriteLog("process_GKB", vee);
                                }
                                if (MdiParent.process_GKB.GetIntOutputResult("GB_B_state").pIntValue[0] == 1)
                                {
                                    if (MdiParent.process_GKB.GetFloatOutputResult("GB_B_GK").pFloatValue[0] > 500)
                                    {
                                        GK_state = true;
                                        string strMsg = "B罐口检测结果:成功";
                                        writelistview("罐口视觉", strMsg, "log");
                                        return;
                                    }
                                    else
                                    {
                                        string strMsg = "B罐口检测结果:失败";
                                        writelistview("罐口视觉", strMsg, "log");
                                        return;
                                    }

                                }
                                else
                                {
                                    string strMsg = "B罐口检测结果:失败";
                                    writelistview("罐口视觉", strMsg, "log");
                                    return;
                                }
                            }
                            catch (Exception vme)
                            {
                                string strMsg = "vision program busy!";//vme.errorMessage;
                                writelistview("罐口视觉", strMsg, "err");
                            }

                        }
                    }
                    else
                    {
                        writelistview("罐口视觉", "相机选择失败！", "log");
                        GK_state = false;
                    }
                });



            }
            catch (Exception ex)
            {
                writelistview("罐口视觉", "罐口程序运行错误！", "err");
                string strMsg = "罐口程序运行错误！ ";
                LogHelper.WriteLog(strMsg, ex);
            }

        }
        public void GK_light_result()
        {
            try
            {
                GK_edg_state = false;
                Task.Run(() =>
                {
                    try
                    {
                        if (GK_station == "A")
                        {
                            if (null == MdiParent.process_GK)
                            {
                                GK_edg_state = false;
                                return;
                            }
                            else
                            {
                                MdiParent.process_GK.Run();
                                if (MdiParent.process_GK.GetIntOutputResult("GB_A_edg_R").pIntValue[0] == 1)
                                {
                                    if (MdiParent.process_GK.GetFloatOutputResult("GB_A_edg").pFloatValue[0] > 10)
                                        Fall_edga_light = MdiParent.process_GK.GetFloatOutputResult("GB_A_edg").pFloatValue[0];
                                    string strMsg = "边缘亮度:" + Fall_edga_light;
                                    writelistview("罐口视觉", strMsg, "log");
                                    GK_edg_state = true;

                                }
                                else
                                {
                                    string strMsg = "边缘亮度:失败";
                                    writelistview("罐口视觉", strMsg, "log");

                                }
                            }

                        }
                        else if (GK_station == "B")
                        {
                            if (null == MdiParent.process_GKB)
                            {
                                GK_edg_state = false;
                                return;
                            }
                            else
                            {
                                MdiParent.process_GKB.Run();
                                if (MdiParent.process_GKB.GetIntOutputResult("GB_B_edg_R").pIntValue[0] == 1)
                                {
                                    if (MdiParent.process_GKB.GetFloatOutputResult("GB_B_edg").pFloatValue[0] > 10)
                                        Fall_edga_light = MdiParent.process_GKB.GetFloatOutputResult("GB_B_edg").pFloatValue[0];
                                    string strMsg = "边缘亮度:" + Fall_edga_light;
                                    writelistview("罐口视觉", strMsg, "log");
                                    GK_edg_state = true;
                                }
                                else
                                {
                                    string strMsg = "边缘亮度:失败";
                                    writelistview("罐口视觉", strMsg, "log");
                                }
                            }
                        }
                        else
                        {
                            GK_edg_state = false;
                            writelistview("罐口视觉", "罐口位置未确定！", "err");
                        }
                    }
                    catch (Exception vme)
                    {
                        string strMsg = "vision program busy!";//vme.errorMessage;
                        writelistview("罐口视觉", strMsg, "err");
                    }

                });
            }
            catch (Exception ex)
            {
                string strMsg = "罐口检测视觉程序运行出错！";
                LogHelper.WriteLog(strMsg, ex);
                return;
            }
        }
    }
    public class TieLiu
    {
        public LogManager my_tl_log = new LogManager();

        private GroupSDK.DelegateNewFrame NewFrame = null;
        public MagDevice m1 = new MagDevice(IntPtr.Zero);

        public updatelistiew writelistview1;
        public string TL_station = "";
        public bool Get_iron = false, TL_angle = false, TL_smok = false;
        public bool TL_run_flag = false;
        public double area = 0;
        public static string shape = "";
        public VideoCapture camer_cap;
        public Thread TieLiu_dect;
        public float last_Atlare = 0;
        public float this_Atlare = 0;
        public float this_Btlare = 0;
        public float last_Btlare = 0;
        public string lastTL_station = "";
        public bool lastGet_iron = false;
        public int maxtemp = 0;
        public bool TL_run_test = false;
        public int TL_somk_count = 0;
        public static bool tl_program_run_flag = true;
        public TieLiu()
        {
            m1.Initialize();
            CreateDevice();
            m1.LinkCamera("192.168.100.50", 2000);
            TieLiu_dect = new Thread(TL_light_result);
            TieLiu_dect.IsBackground = true;
            TieLiu_dect.Start();
            my_tl_log.LogFielPrefix = "TL_are_record";
            my_tl_log.LogPath = @"..\loginfo\";//@"D:\fgg";
            tl_program_run_flag = true;
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
            BitmapData bmpData = bitmap.LockBits(rect, ImageLockMode.ReadOnly, bitmap.PixelFormat);

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
            BitmapData bmpData = bitmap.LockBits(rect, ImageLockMode.ReadOnly, bitmap.PixelFormat);

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
        public bool CreateDevice()
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
            //mDevice.SetSubsectionEnlargePara(300000, 500000, 0, 255);
            GroupSDK.TEMP_STATE everstate = mDevice.GetFrameStatisticalData();
            maxtemp = everstate.intMaxTemp / 1000;
            mDevice.SetDetailEnhancement(5, 1);
            //mDevice.GetFrameStatisticalData()
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
            //Play(m1);
            Bitmap b1 = m1.GetOutputBMPBitmap();
            uint dataLen = (uint)(b1.Width * b1.Height * 3);
            byte[] imagedata = Bitmap2Byte_clor(b1);
            ImageBaseData StImg = new ImageBaseData(imagedata, dataLen, b1.Width, b1.Height, 2);
            MdiParent.TL_img_sdk.SetImageData(StImg);
        }
        public void tl_runtest()
        {
            Play(m1);
            MdiParent.process_TL.Run();
            writelistview1("主程序", "铁流检自检运行通过！", "log");
            TL_run_test = true;
        }
        public void TL_light_result()
        {
            while (true)
            {
                if (TL_run_test)
                {
                    try
                    {

                        Thread.Sleep(1000);
                        Play(m1);
                        if (MdiParent.PLCdata1.ZT_data.TB_pos == false  )
                        {
                            if(maxtemp > 300 || MdiParent.PLCdata1.TB_weight_speed > 0.1)
                            {
                                if (null == MdiParent.process_TL)
                                {
                                    TL_run_flag = false;
                                }
                                else
                                {
                                    get_tl_img();
                                    //热成像仪取图，并通过sdk方法给vm
                                    maxtemp = 0;
                                    //运行vm并获取结果
                                    try
                                    {
                                        Task.Run(() =>
                                        {
                                            MdiParent.process_TL.Run();
                                            int A_blobstate = MdiParent.process_TL.GetIntOutputResult("A_blobstate").pIntValue[0];
                                            int B_blobstate = MdiParent.process_TL.GetIntOutputResult("B_blobstate").pIntValue[0];
                                            float A_tl_are = MdiParent.process_TL.GetFloatOutputResult("A_tl").pFloatValue[0];
                                            float B_tl_are = MdiParent.process_TL.GetFloatOutputResult("B_tl").pFloatValue[0];
                                            string log = A_tl_are + "," + last_Atlare + "," + B_tl_are + "," + last_Btlare + ",";
                                            //烟气检测
                                            if (TL_station == "A")
                                            {
                                                if (A_tl_are < 200)
                                                {
                                                    TL_somk_count += 1;
                                                    if (TL_somk_count > 1)
                                                    {
                                                        TL_smok = true;
                                                    }
                                                    else
                                                    {
                                                        TL_somk_count = 0;
                                                        TL_smok = false;
                                                    }

                                                }
                                                else
                                                {
                                                    TL_somk_count = 0;
                                                    TL_smok = false;
                                                }
                                            }
                                            else if (TL_station == "B")
                                            {
                                                if (B_tl_are < 200)
                                                {
                                                    TL_somk_count += 1;
                                                    if (TL_somk_count > 1)
                                                    {
                                                        TL_smok = true;
                                                    }
                                                    else
                                                    {
                                                        TL_somk_count = 0;
                                                        TL_smok = false;
                                                    }

                                                }
                                                else
                                                {
                                                    TL_somk_count = 0;
                                                    TL_smok = false;
                                                }
                                            }
                                            else
                                            {
                                                TL_somk_count = 0;
                                                TL_smok = false;
                                            }

                                            //if (A_blobstate == 0 && B_blobstate == 0)
                                            //{
                                            //    if (last_Atlare - A_tl_are > 200 || (MdiParent.PLCdata1.TB_weight_speed > 0.09 && MdiParent.PLCdata1.TB_weight_speed < 5) || last_Btlare - B_tl_are > 200)
                                            //    {
                                            //        log += " ";
                                            //        TL_station = lastTL_station;
                                            //        Get_iron = lastGet_iron;
                                            //        my_tl_log.WriteLogcsv(LogFile.Trace, log, "time,本次A,上次A,本次B,上次B,铁流角度");
                                            //    }
                                            //    else
                                            //    {
                                            //        log = A_tl_are + "," + last_Atlare + "," + B_tl_are + "," + last_Btlare + "," + "ALL_0";
                                            //        my_tl_log.WriteLogcsv(LogFile.Trace, log, "time,本次A,上次A,本次B,上次B,铁流角度");
                                            //        TL_station = "";
                                            //        Get_iron = false;
                                            //    }
                                            //}
                                            //else if (A_blobstate > 0 && B_blobstate > 0)
                                            //{
                                            //    if (last_Atlare - A_tl_are > 200 || MdiParent.PLCdata1.TB_weight_speed > 0.09 && MdiParent.PLCdata1.TB_weight_speed < 5 || last_Btlare - B_tl_are > 200)
                                            //    {
                                            //        log += " ";
                                            //        TL_station = lastTL_station;
                                            //        Get_iron = lastGet_iron;
                                            //        my_tl_log.WriteLogcsv(LogFile.Trace, log, "time,本次A,上次A,本次B,上次B,铁流角度");
                                            //    }
                                            //    else
                                            //    {
                                            //        log = A_tl_are + "," + last_Atlare + "," + B_tl_are + "," + last_Btlare + "," + "ALL_>0";
                                            //        my_tl_log.WriteLogcsv(LogFile.Trace, log, "time,本次A,上次A,本次B,上次B,铁流角度");
                                            //        TL_station = "";
                                            //        Get_iron = false;
                                            //    }
                                            //}
                                             if (A_blobstate > 0)
                                            {
                                                int A_angle_result = MdiParent.process_TL.GetIntOutputResult("A_angle_result").pIntValue[0];
                                                if (A_angle_result == 1000)
                                                    TL_angle = true;
                                                else
                                                    TL_angle = false;
                                                log += TL_angle.ToString();
                                                TL_station = "A";
                                                Get_iron = true;
                                                my_tl_log.WriteLogcsv(LogFile.Trace, log, "time,本次A,上次A,本次B,上次B,铁流角度");
                                            }
                                            else if (B_blobstate > 0)
                                            {
                                                int B_angle_result = MdiParent.process_TL.GetIntOutputResult("B_angle_result").pIntValue[0];
                                                if (B_angle_result == 1000)
                                                    TL_angle = true;
                                                else
                                                    TL_angle = false;
                                                log += TL_angle.ToString();
                                                TL_station = "B";
                                                Get_iron = true;
                                                my_tl_log.WriteLogcsv(LogFile.Trace, log, "time,本次A,上次A,本次B,上次B,铁流角度");
                                            }
                                            else
                                            {
                                                log = A_tl_are + "," + last_Atlare + "," + B_tl_are + "," + last_Btlare + "," + "NULL";
                                                my_tl_log.WriteLogcsv(LogFile.Trace, log, "time,本次A,上次A,本次B,上次B,铁流角度");
                                                TL_station = "";
                                                Get_iron = false;
                                            }
                                            last_Atlare = A_tl_are;
                                            last_Btlare = B_tl_are;
                                            lastTL_station = TL_station;
                                            lastGet_iron = Get_iron;
                                            TL_run_flag = true;
                                        });
                                    }
                                    catch (Exception vme)
                                    {
                                        string log = 0 + "," + 0 + "," + 0 + "," + 0 + "," + "catch";
                                        my_tl_log.WriteLogcsv(LogFile.Trace, log, "time,本次A,上次A,本次B,上次B,铁流角度");
                                    }


                                }
                            }
                            else
                            {
                                string log = 0 + "," + 0 + "," + 0 + "," + 0 + "," + "maxtemplow";
                                my_tl_log.WriteLogcsv(LogFile.Trace, log, "time,本次A,上次A,本次B,上次B,铁流角度");
                                Get_iron = false;
                                TL_angle = false;
                                TL_run_flag = false;
                                TL_smok = false;
                                TL_somk_count = 0;
                            }

                        }
                        else
                        {
                            string log = 0 + "," + 0 + "," + 0 + "," + 0 + "," + "maxtemplow";
                            my_tl_log.WriteLogcsv(LogFile.Trace, log, "time,本次A,上次A,本次B,上次B,铁流角度");
                            Get_iron = false;
                            TL_angle = false;
                            TL_run_flag = false;
                            TL_smok = false;
                            TL_somk_count = 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        writelistview1("铁流视觉", "铁流检查程序运行失败！", "log");
                        string strMsg = "铁流检查程序运行失败";
                        LogHelper.WriteLog(strMsg, ex);
                        Get_iron = false;
                        TL_run_flag = false;
                        // string strMsg = "铁流检测结果:失败";
                        tl_program_run_flag = false;
                        // return false;
                    }
                }

            }


        }

    }
}
