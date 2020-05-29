using iMVS_6000PlatformSDKCS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Text.RegularExpressions;
using FrontedUI;
using System.IO;
using System.Net.Sockets;
using System.Net;
using OPCAutomation;
//using FrontedUI;
using logtest;
namespace AGFish
{
    public partial class Form1 : Form
    {
        opchelper AGFishOPCClient = new opchelper();
        public static OPCItem xintiao;
        public static OPCItem VisionCode;
        public static OPCItem VisionCodeA;
        public static OPCItem FlatCode;
        public static OPCItem FlatCodeA;
        public static OPCItem Product_Type;
        public static OPCItem Pro_X;
        public static OPCItem Pro_Y;
        public static OPCItem Pro_Z;
        public static OPCItem Pro_RX;
        public static OPCItem Pro_RY;
        public static OPCItem Pro_RZ;

        // 全局变量定义
        public IntPtr m_handle = IntPtr.Zero;                                   // SDK4Server句柄
        private delegateOutputCallBack PlatformInfoCallBack;                               // 回调函数委托  
        public uint m_nContinStatus = 9999;                                          // 连续运行状态值
        public uint m_nStopStatus = 9999;                                          // 停止运行状态值
        public uint m_nWorkStatus = 9999;                                          // 流程工作状态值
        public uint m_nModuHbID = 9999;                                          // 模块心跳异常状态值
        public uint m_nServerHbStatus = 9999;                                          // 服务心跳异常状态值
        public uint m_nClientHbStatus = 9999;                                          // 界面心跳异常状态值
        public uint m_nDongleStatus = 9999;                                          // 加密狗异常状态值
        public uint m_nShowCallbackFlag = 0;                                             // 显示回调内容标志位
        public int m_nShowProcessID = 0;                                             // 显示用流程ID
        public uint m_nProgressFlag = 0;                                             // 显示加载或保存进度标志位
                                                                                     //图像  
                                                                                     //图像
        CircleData circleData = new CircleData();
        ImageData imageData1 = new ImageData();
        byte[] imagebytes1;
        public PictureBox curPictureBox1 { get; set; }
        public PictureBox curPictureBox2 { get; set; }
        public static Socket socketClient_PLC;
        static FileStream fs;
        static StreamWriter sw;
        static string path, path0;
        Int16 count1 = 0;
        Int16 count_old1 = 0;
        public Form1()
        {
            InitializeComponent();
            curPictureBox1 = pictureBoxImg1;
            curPictureBox2 = pictureBox1;
            Initopc();

        }
        void Initopc()
        {
            try
            {
                if (!AGFishOPCClient.ConnectRemoteServer("127.0.0.1", "kepware.kepserverex.v5"))
                {
                    txt_message.AppendText("状态：连接OPC失败！\n");
                    return;
                }
                if (!AGFishOPCClient.CreateGroup())
                {
                    txt_message.AppendText("状态：创建OPC组失败！\n");
                    return;
                }
                if (opchelper.KepServer.ServerState == (int)OPCServerState.OPCRunning)
                {
                    string log = "已连接到:" + opchelper.KepServer.ServerName;
                    txt_message.AppendText(log + " \r\n");
                    //service_connection.BackColor = Color.LightGreen;
                    //service_connection.Enabled = false;
                    LogHelper.WriteLog(log);
                    AGFishOPCClient.opc_connected = true;
                }
                else
                {
                    //这里你可以根据返回的状态来自定义显示信息，请查看自动化接口API文档
                    txt_message.AppendText("状态：" + opchelper.KepServer.ServerState.ToString() + "\n");
                    //service_connection.BackColor = Color.Red;
                    AGFishOPCClient.opc_connected = false;
                }
                xintiao = AGFishOPCClient.AddItem("xintiao");
                VisionCode = AGFishOPCClient.AddItem("VisionCode");
                VisionCodeA = AGFishOPCClient.AddItem("VisionCodeA");
                FlatCode = AGFishOPCClient.AddItem("FlatCode");
                FlatCodeA = AGFishOPCClient.AddItem("FlatCodeA");
                Product_Type = AGFishOPCClient.AddItem("Product_Type");
                Pro_X = AGFishOPCClient.AddItem("Pro_X");
                Pro_Y = AGFishOPCClient.AddItem("Pro_Y");
                Pro_Z = AGFishOPCClient.AddItem("Pro_Z");
                Pro_RX = AGFishOPCClient.AddItem("Pro_RX");
                Pro_RY = AGFishOPCClient.AddItem("Pro_RY");
                Pro_RZ = AGFishOPCClient.AddItem("Pro_RZ");
            }
            catch (Exception err)
            {
                LogHelper.WriteLog("opcerr", err);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 进度条设置
            progressBarSaveAndLoad.Value = 0;
            progressBarSaveAndLoad.Maximum = 100;

            // 显示隐藏
            comboBoxShowAndHide.Items.Add("隐藏");
            comboBoxShowAndHide.Items.Add("显示");
            comboBoxShowAndHide.SelectedIndex = 1;

            // 消除ListBox在回调中使用产生警告
            ListBox.CheckForIllegalCrossThreadCalls = false;
            // 创建句柄
            string strMsg = null;
            if (IntPtr.Zero != m_handle)
            {
                ImvsPlatformSDK_API.IMVS_PF_DestroyHandle_CS(m_handle);
                m_handle = IntPtr.Zero;
            }
            if (IntPtr.Zero == m_handle)
            {
                try { m_handle = ImvsPlatformSDK_API.IMVS_PF_CreateHandle_CS(); }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                
                if (m_handle == IntPtr.Zero)
                {
                    strMsg = "IMVS_PF_CreateHandle_CS Failed.";
                    txt_message.AppendText(strMsg + "\r\n");
                    LogHelper.WriteLog(strMsg);
                    return;
                }
                
            }

            // 注册回调
            IntPtr pUser = new IntPtr();
            pUser = this.Handle;
            PlatformInfoCallBack = new delegateOutputCallBack(delegateOutputCallBackFunc);
            int iRet = ImvsPlatformSDK_API.IMVS_PF_RegisterResultCallBack_V30_CS(m_handle, PlatformInfoCallBack, pUser);
            if (ImvsSdkPFDefine.IMVS_EC_OK != iRet)
            {
                strMsg = "IMVS_PF_RegisterResultCallBack_V30_CS Failed";
                txt_message.AppendText(strMsg + "\r\n");
                LogHelper.WriteLog(strMsg);
                return;
            }

           // buttonOpenVM_Click(null, null);
            //buttonLoadSolution_Click(null, null);
           // CreateConnect_PLC();
        }


        private void buttonOpenVM_Click(object sender, EventArgs e)
        {

            uint nWaitTime = 10000;
            string strMsg = null;
            if (IntPtr.Zero == m_handle)
            {

                strMsg = "句柄异常, 请重启软件!";
                txt_message.AppendText(strMsg + "\r\n");
                return;
            }

            string VMPath = "D:\\Program Files\\VisionMaster3.2.0\\Applications\\VisionMaster.exe";
            int iRet = ImvsPlatformSDK_API.IMVS_PF_StartVisionMaster_CS(m_handle, VMPath, nWaitTime);
            if (ImvsSdkPFDefine.IMVS_EC_OK != iRet)
            {
                strMsg = "IMVS_PF_StartVisionMaster_CS Failed. Error Code: " + Convert.ToString(iRet, 16);
                txt_message.AppendText(strMsg + "\r\n");
                LogHelper.WriteLog(strMsg);
                return;
            }
            strMsg = "IMVS_PF_StartVisionMaster_CS Success.";
            txt_message.AppendText(strMsg + "\r\n");
            LogHelper.WriteLog(strMsg);
        }

        private void buttonCloseVM_Click(object sender, EventArgs e)
        {
            new Thread(new ThreadStart(delegate              // 开辟线程关闭, 防止主线程连续运行时阻塞
            {
                string strMsg = null;
                if (IntPtr.Zero == m_handle)
                {
                    MessageBoxButtons msgType = MessageBoxButtons.OK;
                    DialogResult diagMsg = MessageBox.Show("句柄异常, 请重启软件!", "提示", msgType);
                    if (diagMsg == DialogResult.OK)
                    {
                        return;
                    }
                }

                int iRet = ImvsSdkPFDefine.IMVS_EC_UNKNOWN;
                iRet = ImvsPlatformSDK_API.IMVS_PF_CloseVisionMaster_CS(m_handle);
                if (ImvsSdkPFDefine.IMVS_EC_OK != iRet)
                {
                    strMsg = "IMVS_PF_CloseVisionMaster_CS Failed. Error Code: " + Convert.ToString(iRet, 16);
                    txt_message.AppendText(strMsg + "\r\n");
                    LogHelper.WriteLog(strMsg);
                    return;
                }
                strMsg = "IMVS_PF_CloseVisionMaster_CS Success.";
                txt_message.AppendText(strMsg + "\r\n");
                //LogHelper.WriteLog(strMsg);

                // 清空图像
                curPictureBox1.Image = null;
                curPictureBox1.Refresh();
                curPictureBox2.Image = null;
                curPictureBox2.Refresh();
            }))
            { IsBackground = true }.Start();
        }

        private void buttonLoadSolution_Click(object sender, EventArgs e)
        {

            string strMsg = null;
            progressBarSaveAndLoad.Value = 0;
            uint nProcess = 0;
            labelProgress.Text = nProcess.ToString();
            labelProgress.Refresh();

            int iRet = ImvsSdkPFDefine.IMVS_EC_UNKNOWN;
            if (IntPtr.Zero == m_handle)
            {
                MessageBoxButtons msgType = MessageBoxButtons.OK;
                DialogResult diagMsg = MessageBox.Show("句柄异常, 请重启软件!", "提示", msgType);
                if (diagMsg == DialogResult.OK)
                {
                    return;
                }
            }
            string SolutionPath =@"E:\ProjSetup\AGFish\Debug\分流程触发.sol";
            iRet = ImvsPlatformSDK_API.IMVS_PF_LoadSolution_CS(m_handle, SolutionPath, "");
            if (ImvsSdkPFDefine.IMVS_EC_OK != iRet)
            {
                strMsg = "IMVS_PF_LoadSolution_CS Failed. Error Code: " + Convert.ToString(iRet, 16);
                txt_message.AppendText(strMsg + "\r\n");
                LogHelper.WriteLog(strMsg);
                return;
            }
            strMsg = "IMVS_PF_LoadSolution_CS success";
            txt_message.AppendText(strMsg + "\r\n");
            LogHelper.WriteLog(strMsg);
            DateTime dtStart = DateTime.Now;
            uint nProgress = 0;
            m_nProgressFlag = 1;    // 显示加载方案进度标志位置位
            for (; nProgress < 100;)
            {
                iRet = ImvsPlatformSDK_API.IMVS_PF_GetLoadProgress_CS(m_handle, ref nProgress);

                labelProgress.Text = nProgress.ToString();
                labelProgress.Refresh();
                progressBarSaveAndLoad.Value = Convert.ToInt32(nProgress);

                if (ImvsSdkPFDefine.IMVS_EC_OK != iRet)
                {
                    strMsg = "IMVS_PF_GetLoadProgress_CS Failed. Error Code: " + Convert.ToString(iRet, 16);
                    txt_message.AppendText(strMsg + "\r\n");
                    //LogHelper.WriteLog(strMsg);
                    return;
                }

                Thread.Sleep(300);

                TimeSpan spanNow = new TimeSpan();
                spanNow = DateTime.Now - dtStart;
                if (spanNow.Seconds > 50)    // 50s后退出循环, 防止死循环
                {
                    break;
                }
            }
            m_nProgressFlag = 0;    // 显示加载方案进度标志位复位

        }

        private void buttonCloseSolution_Click(object sender, EventArgs e)
        {
            string strMsg = null;
            int iRet = ImvsSdkPFDefine.IMVS_EC_UNKNOWN;
            if (IntPtr.Zero == m_handle)
            {
                MessageBoxButtons msgType = MessageBoxButtons.OK;
                DialogResult diagMsg = MessageBox.Show("句柄异常, 请重启软件!", "提示", msgType);
                if (diagMsg == DialogResult.OK)
                {
                    return;
                }
            }

            iRet = ImvsPlatformSDK_API.IMVS_PF_CloseSolution_CS(m_handle);
            if (ImvsSdkPFDefine.IMVS_EC_OK != iRet)
            {
                strMsg = "IMVS_PF_CloseSolution_CS Failed. Error Code: " + Convert.ToString(iRet, 16);
                txt_message.AppendText(strMsg + "\r\n");
                LogHelper.WriteLog(strMsg);
                return;
            }

            // 清空PictureBox控件中的内容
            pictureBoxImg1.Image = null;
            pictureBoxImg1.Refresh();

            strMsg = "IMVS_PF_CloseSolution_CS Success";
            txt_message.AppendText(strMsg + "\r\n");
            LogHelper.WriteLog(strMsg);
        }

        

        private void buttonShowHideVM_Click(object sender, EventArgs e)
        {
            string strMsg = null;
            int iRet = ImvsSdkPFDefine.IMVS_EC_UNKNOWN;
            if (IntPtr.Zero == m_handle)
            {
                MessageBoxButtons msgType = MessageBoxButtons.OK;
                DialogResult diagMsg = MessageBox.Show("句柄异常, 请重启软件!", "提示", msgType);
                if (diagMsg == DialogResult.OK)
                {
                    return;
                }
            }
            uint nCurSel = uint.Parse(comboBoxShowAndHide.SelectedIndex.ToString());
            iRet = ImvsPlatformSDK_API.IMVS_PF_ShowVisionMaster_CS(m_handle, nCurSel);
            if (ImvsSdkPFDefine.IMVS_EC_OK != iRet)
            {
                strMsg = "IMVS_PF_ShowVisionMaster_CS Failed. Error Code: " + Convert.ToString(iRet, 16);
                txt_message.AppendText(strMsg + "\r\n");
                LogHelper.WriteLog(strMsg);
                return;
            }

            strMsg = "IMVS_PF_ShowVisionMaster_CS Success";
            txt_message.AppendText(strMsg + "\r\n");
            LogHelper.WriteLog(strMsg);
        }
        string location_flag, recognition_flag;

        private void timer_deleterizhi_Tick(object sender, EventArgs e)
        {
            //try { 
            //    object location = AGFishOPCClient.ReadItem(VisionCode);
            //    txt_sdzs1.Text = location.ToString();
            //    if(location.ToString()=="0")
            //    {
            //        LocationExecuteOnce_Click(null, null);
            //        AGFishOPCClient.WriteItem(10.ToString(), VisionCode);
            //    }
            //    object recognition = AGFishOPCClient.ReadItem(VisionCodeA);
            //    txt_sdzs1.Text = location.ToString();
            //    if (recognition.ToString() == "0")
            //    {
            //        Recognition_Click(null, null);
            //        AGFishOPCClient.WriteItem(10.ToString(), VisionCodeA);
            //    }
            //}
            //catch(Exception ex)
            //{
            //    LogHelper.WriteLog("时钟执行错误",ex);
            //}
        }

        uint nProcID = 0;

        private void ExecuteOnce()
        {
            string strMsg = null;
            int iRet = ImvsSdkPFDefine.IMVS_EC_UNKNOWN;
            if (IntPtr.Zero == m_handle)
            {
                MessageBoxButtons msgType = MessageBoxButtons.OK;
                DialogResult diagMsg = MessageBox.Show("句柄异常, 请重启软件!", "提示", msgType);
                if (diagMsg == DialogResult.OK)
                {
                    return;
                }
            }

            iRet = ImvsPlatformSDK_API.IMVS_PF_ExecuteOnce_V30_CS(m_handle, nProcID, null);
            if (ImvsSdkPFDefine.IMVS_EC_OK != iRet)
            {
                strMsg = "相机1:IMVS_PF_ExecuteOnce_V30_CS Failed. Error Code: " + Convert.ToString(iRet, 16);
                txt_message.AppendText(strMsg + "\r\n");
                LogHelper.WriteLog(strMsg);
                return;
            }

            strMsg = "相机1:IMVS_PF_ExecuteOnce_V30_CS Success";
            txt_message.AppendText(strMsg + "\r\n");
            LogHelper.WriteLog(strMsg);
        }

        private void LocationExecuteOnce_Click(object sender, EventArgs e)
        {
            nProcID = 10000;
            ExecuteOnce();
        }
        private void Recognition_Click(object sender, EventArgs e)
        {
            nProcID = 10001;
            ExecuteOnce();
        }

        private void buttonContinuExecute_Click(object sender, EventArgs e)
        {
            string strMsg = null;
            int iRet = ImvsSdkPFDefine.IMVS_EC_UNKNOWN;
            if (IntPtr.Zero == m_handle)
            {
                MessageBoxButtons msgType = MessageBoxButtons.OK;
                DialogResult diagMsg = MessageBox.Show("句柄异常, 请重启软件!", "提示", msgType);
                if (diagMsg == DialogResult.OK)
                {
                    return;
                }
            }
               
                uint nProcID = 10000;
                iRet = ImvsPlatformSDK_API.IMVS_PF_ContinousExecute_V30_CS(m_handle, 10000);
            iRet = ImvsPlatformSDK_API.IMVS_PF_ContinousExecute_V30_CS(m_handle, 10001);
            if (ImvsSdkPFDefine.IMVS_EC_OK != iRet)
                {
                    strMsg = "相机1:IMVS_PF_ContinousExecute_V30_CS Failed. Error Code: " + Convert.ToString(iRet, 16);
                    txt_message.AppendText(strMsg + "\r\n");
                    LogHelper.WriteLog(strMsg);
                return;
                }
                strMsg = "相机1:IMVS_PF_ContinousExecute_V30_CS Success";
                txt_message.AppendText(strMsg + "\r\n");
                LogHelper.WriteLog(strMsg);

        }

        private void buttonStopExecute_Click(object sender, EventArgs e)
        {
            string strMsg = null;
            int iRet = ImvsSdkPFDefine.IMVS_EC_UNKNOWN;
            if (IntPtr.Zero == m_handle)
            {
                MessageBoxButtons msgType = MessageBoxButtons.OK;
                DialogResult diagMsg = MessageBox.Show("句柄异常, 请重启软件!", "提示", msgType);
                if (diagMsg == DialogResult.OK)
                {
                    return;
                }
            }

            uint nWaitTime = 5000; 
            uint nProcID = 10000;
            iRet = ImvsPlatformSDK_API.IMVS_PF_StopExecute_V30_CS(m_handle, nProcID, nWaitTime);
            if (ImvsSdkPFDefine.IMVS_EC_OK != iRet)
            {
                strMsg = "相机1:IMVS_PF_StopExecute_V30_CS Failed. Error Code: " + Convert.ToString(iRet, 16);
                txt_message.AppendText(strMsg + "\r\n");
                LogHelper.WriteLog(strMsg);
                return;
            }

            strMsg = "相机1:IMVS_PF_StopExecute_V30_CS Success";
            txt_message.AppendText(strMsg + "\r\n");
            LogHelper.WriteLog(strMsg);

        }
        /****************************************************************************
       * @fn           回调结果数据
       ****************************************************************************/
        public void delegateOutputCallBackFunc(IntPtr pInputStruct, IntPtr pUser)
        {
            //回调信息转换
            ImvsSdkPFDefine.IMVS_PF_OUTPUT_PLATFORM_INFO struInfo = (ImvsSdkPFDefine.IMVS_PF_OUTPUT_PLATFORM_INFO)Marshal.PtrToStructure(pInputStruct, typeof(ImvsSdkPFDefine.IMVS_PF_OUTPUT_PLATFORM_INFO));
            switch (struInfo.nInfoType)
            {

                // 回调模块结果信息
                case (uint)ImvsSdkPFDefine.IMVS_CTRLC_OUTPUT_PlATFORM_INFO_TYPE.IMVS_ENUM_CTRLC_OUTPUT_PLATFORM_INFO_MODULE_RESULT:
                    {

                        ImvsSdkPFDefine.IMVS_PF_MODU_RES_INFO resModuInfo = (ImvsSdkPFDefine.IMVS_PF_MODU_RES_INFO)Marshal.PtrToStructure(struInfo.pData, typeof(ImvsSdkPFDefine.IMVS_PF_MODU_RES_INFO));


                        if ((0 == m_nProgressFlag))
                        {
                            UpdateDataModuResutOutput(resModuInfo);
                        }


                        break;
                    }


                default:
                    {
                        break;
                    }
            }

        }
        /****************************************************************************
       * @fn           接收回调结果数据（模块结果）
       ****************************************************************************/
        internal void UpdateDataModuResutOutput(ImvsSdkPFDefine.IMVS_PF_MODU_RES_INFO struResultInfo)
        {
            if (null == struResultInfo.pData)
            {
                return;
            }
            if(struResultInfo.nProcessID==10001)
            {
                switch (struResultInfo.strModuleName)//struResultInfo.nModuleID
                {


                    case ImvsSdkPFDefine.MODU_NAME_LOCALIMAGEVIEW://1
                        //相机图像
                        //ImvsSdkPFDefine.IMVS_PF_CAMERAMODULE_INFO stCameraImgInfo = (ImvsSdkPFDefine.IMVS_PF_CAMERAMODULE_INFO)Marshal.PtrToStructure(struResultInfo.pData, typeof(ImvsSdkPFDefine.IMVS_PF_CAMERAMODULE_INFO));
                        //imageData1.Width = stCameraImgInfo.stImgInfo.iWidth;
                        //imageData1.Height = stCameraImgInfo.stImgInfo.iHeight;
                        //imagebytes1 = IntPtr2Bytes(stCameraImgInfo.stImgInfo.pImgData, stCameraImgInfo.stImgInfo.iImgDataLen);
                        //本地图像测试
                        ImvsSdkPFDefine.IMVS_PF_LOCALIMAGEVIEW_MODU_INFO stLocalImgInfo = (ImvsSdkPFDefine.IMVS_PF_LOCALIMAGEVIEW_MODU_INFO)Marshal.PtrToStructure(struResultInfo.pData, typeof(ImvsSdkPFDefine.IMVS_PF_LOCALIMAGEVIEW_MODU_INFO));
                        imageData1.Width = stLocalImgInfo.stImgInfo.iWidth;
                        imageData1.Height = stLocalImgInfo.stImgInfo.iHeight;
                        imagebytes1 = IntPtr2Bytes(stLocalImgInfo.stImgInfo.pImgData, stLocalImgInfo.stImgInfo.iImgDataLen);
                        Bitmap bmp;
                        if (imageData1.Width != 0 && imageData1.Height != 0 && imagebytes1 != null)
                        {
                            uint ImageLenth = (uint)(imageData1.Width * imageData1.Height);
                            if (ImageLenth != imagebytes1.Length)
                            {
                                break;
                            }
                        }
                        imageData1.ImageBuffer = imagebytes1;
                        if (imageData1.ImageBuffer != null)
                        {
                            bmp = imageData1.ImageDataToBitmap().GetArgb32BitMap();
                            //using (var g = bmp.CreateGraphic())
                            //{
                            //    ////画匹配框
                            //    if (x != null && y != null && width != null && height != null && angle != null &&
                            //        x.Length == y.Length && x.Length == width.Length && x.Length == height.Length && x.Length == angle.Length)
                            //    {
                            //        for (int i = 0; i < x.Length; i++)
                            //        {
                            //            g.DrawRect(Color.GreenYellow, 5, new PointF(x[i], y[i]), width[i], height[i], angle[i]);

                            //        }
                            //    }
                            //}
                            curPictureBox1.Invoke(new Action(() =>
                            {
                                curPictureBox1.Image = bmp;
                            }));
                        }
                        break;

                    case ImvsSdkPFDefine.MODU_NAME_CIRCLEFINDMODU:
                        ImvsSdkPFDefine.IMVS_PF_CIRCLEFIND_MODU_INFO stCirFindInfo = (ImvsSdkPFDefine.IMVS_PF_CIRCLEFIND_MODU_INFO)Marshal.PtrToStructure(struResultInfo.pData, typeof(ImvsSdkPFDefine.IMVS_PF_CIRCLEFIND_MODU_INFO));
                        circleData.radius = stCirFindInfo.fRadius;
                        circleData.centerx = stCirFindInfo.stCirPt.fPtX;
                        circleData.centery = stCirFindInfo.stCirPt.fPtY;

                        string strMsg = "circle radius is:" + struResultInfo.strDisplayName + " " + circleData.radius;
                        txt_message.AppendText(strMsg + "\r\n");
                        break;
                    case "sdf"://10000

                        ImvsSdkPFDefine.IMVS_PF_CNNDETECT_MODU_INFO stCNNDETECTInfo = (ImvsSdkPFDefine.IMVS_PF_CNNDETECT_MODU_INFO)Marshal.PtrToStructure(struResultInfo.pData, typeof(ImvsSdkPFDefine.IMVS_PF_CNNDETECT_MODU_INFO));
                        count1 = (short)stCNNDETECTInfo.iTargetNum;
                        if (count1 != count_old1)
                        {
                            byte[] sendbuffer = Enumerable.Repeat((byte)0x00, 2).ToArray();
                            byte[] count_byte = BitConverter.GetBytes(count1);
                            Buffer.BlockCopy(count_byte, 0, sendbuffer, 0, count_byte.Length);
                            socketClient_PLC.Send(sendbuffer);
                        }
                        //if (count1 > 0)
                        //{

                        ImvsSdkPFDefine.IMVS_PF_TARGET_INFO[] pstTargetInfo = new ImvsSdkPFDefine.IMVS_PF_TARGET_INFO[count1];
                        ImvsSdkPFDefine.IMVS_PF_RECT_INFO_F[] rectinfo = new ImvsSdkPFDefine.IMVS_PF_RECT_INFO_F[count1];
                        ImvsSdkPFDefine.IMVS_PF_2DPOINT_F[] TargetCenter = new ImvsSdkPFDefine.IMVS_PF_2DPOINT_F[count1];
                        float[] width = new float[count1];
                        float[] height = new float[count1];
                        float[] angle = new float[count1];
                        float[] x = new float[count1];
                        float[] y = new float[count1];
                        for (int i = 0; i < count1; i++)
                        {
                            pstTargetInfo[i] = stCNNDETECTInfo.pstTargetInfo[i];
                            rectinfo[i] = pstTargetInfo[i].stTargetRect;
                            width[i] = rectinfo[i].fWidth;
                            height[i] = rectinfo[i].fHeight;
                            angle[i] = rectinfo[i].fAngle;
                            TargetCenter[i] = rectinfo[i].stCentPt;
                            x[i] = TargetCenter[i].fPtX;
                            y[i] = TargetCenter[i].fPtY;
                        }

                        //图像
                        if (imageData1.Width != 0 && imageData1.Height != 0 && imagebytes1 != null)
                        {
                            uint ImageLenth = (uint)(imageData1.Width * imageData1.Height);
                            if (ImageLenth != imagebytes1.Length)
                            {
                                break;
                            }
                            imageData1.ImageBuffer = imagebytes1;

                            //获取图像数据
                            if (imageData1.ImageBuffer != null)
                            {
                                bmp = imageData1.ImageDataToBitmap().GetArgb32BitMap();
                                using (var g = bmp.CreateGraphic())
                                {
                                    ////画匹配框
                                    if (x != null && y != null && width != null && height != null && angle != null &&
                                        x.Length == y.Length && x.Length == width.Length && x.Length == height.Length && x.Length == angle.Length)
                                    {
                                        for (int i = 0; i < x.Length; i++)
                                        {
                                            g.DrawRect(Color.GreenYellow, 5, new PointF(x[i], y[i]), width[i], height[i], angle[i]);

                                        }
                                    }
                                }
                                curPictureBox1.Invoke(new Action(() =>
                                {
                                    curPictureBox1.Image = bmp;
                                }));
                            }

                        }
                        txt_count1.Text = count1.ToString();
                        count_old1 = count1;
                        //}
                        break;



                    default: break;
                }
            }
            if (struResultInfo.nProcessID == 10000)
            {
                switch (struResultInfo.strModuleName)//struResultInfo.nModuleID
                {


                    case ImvsSdkPFDefine.MODU_NAME_LOCALIMAGEVIEW://1
                        //相机图像
                        //ImvsSdkPFDefine.IMVS_PF_CAMERAMODULE_INFO stCameraImgInfo = (ImvsSdkPFDefine.IMVS_PF_CAMERAMODULE_INFO)Marshal.PtrToStructure(struResultInfo.pData, typeof(ImvsSdkPFDefine.IMVS_PF_CAMERAMODULE_INFO));
                        //imageData1.Width = stCameraImgInfo.stImgInfo.iWidth;
                        //imageData1.Height = stCameraImgInfo.stImgInfo.iHeight;
                        //imagebytes1 = IntPtr2Bytes(stCameraImgInfo.stImgInfo.pImgData, stCameraImgInfo.stImgInfo.iImgDataLen);
                        //本地图像测试
                        ImvsSdkPFDefine.IMVS_PF_LOCALIMAGEVIEW_MODU_INFO stLocalImgInfo = (ImvsSdkPFDefine.IMVS_PF_LOCALIMAGEVIEW_MODU_INFO)Marshal.PtrToStructure(struResultInfo.pData, typeof(ImvsSdkPFDefine.IMVS_PF_LOCALIMAGEVIEW_MODU_INFO));
                        imageData1.Width = stLocalImgInfo.stImgInfo.iWidth;
                        imageData1.Height = stLocalImgInfo.stImgInfo.iHeight;
                        imagebytes1 = IntPtr2Bytes(stLocalImgInfo.stImgInfo.pImgData, stLocalImgInfo.stImgInfo.iImgDataLen);
                        Bitmap bmp;
                        if (imageData1.Width != 0 && imageData1.Height != 0 && imagebytes1 != null)
                        {
                            uint ImageLenth = (uint)(imageData1.Width * imageData1.Height);
                            if (ImageLenth != imagebytes1.Length)
                            {
                                break;
                            }
                        }
                        imageData1.ImageBuffer = imagebytes1;
                        if (imageData1.ImageBuffer != null)
                        {
                            bmp = imageData1.ImageDataToBitmap().GetArgb32BitMap();
                            //using (var g = bmp.CreateGraphic())
                            //{
                            //    ////画匹配框
                            //    if (x != null && y != null && width != null && height != null && angle != null &&
                            //        x.Length == y.Length && x.Length == width.Length && x.Length == height.Length && x.Length == angle.Length)
                            //    {
                            //        for (int i = 0; i < x.Length; i++)
                            //        {
                            //            g.DrawRect(Color.GreenYellow, 5, new PointF(x[i], y[i]), width[i], height[i], angle[i]);

                            //        }
                            //    }
                            //}
                            curPictureBox2.Invoke(new Action(() =>
                            {
                                curPictureBox2.Image = bmp;
                            }));
                        }
                        break;

                    case ImvsSdkPFDefine.MODU_NAME_CIRCLEFINDMODU:
                        ImvsSdkPFDefine.IMVS_PF_CIRCLEFIND_MODU_INFO stCirFindInfo = (ImvsSdkPFDefine.IMVS_PF_CIRCLEFIND_MODU_INFO)Marshal.PtrToStructure(struResultInfo.pData, typeof(ImvsSdkPFDefine.IMVS_PF_CIRCLEFIND_MODU_INFO));
                        circleData.radius = stCirFindInfo.fRadius;
                        circleData.centerx = stCirFindInfo.stCirPt.fPtX;
                        circleData.centery = stCirFindInfo.stCirPt.fPtY;

                        string strMsg = "circle radius is:" + struResultInfo.strDisplayName + " " + circleData.radius;
                        txt_message.AppendText(strMsg + "\r\n");
                        break;
                    case "sdf"://10000

                        ImvsSdkPFDefine.IMVS_PF_CNNDETECT_MODU_INFO stCNNDETECTInfo = (ImvsSdkPFDefine.IMVS_PF_CNNDETECT_MODU_INFO)Marshal.PtrToStructure(struResultInfo.pData, typeof(ImvsSdkPFDefine.IMVS_PF_CNNDETECT_MODU_INFO));
                        count1 = (short)stCNNDETECTInfo.iTargetNum;
                        if (count1 != count_old1)
                        {
                            byte[] sendbuffer = Enumerable.Repeat((byte)0x00, 2).ToArray();
                            byte[] count_byte = BitConverter.GetBytes(count1);
                            Buffer.BlockCopy(count_byte, 0, sendbuffer, 0, count_byte.Length);
                            socketClient_PLC.Send(sendbuffer);
                        }
                        //if (count1 > 0)
                        //{

                        ImvsSdkPFDefine.IMVS_PF_TARGET_INFO[] pstTargetInfo = new ImvsSdkPFDefine.IMVS_PF_TARGET_INFO[count1];
                        ImvsSdkPFDefine.IMVS_PF_RECT_INFO_F[] rectinfo = new ImvsSdkPFDefine.IMVS_PF_RECT_INFO_F[count1];
                        ImvsSdkPFDefine.IMVS_PF_2DPOINT_F[] TargetCenter = new ImvsSdkPFDefine.IMVS_PF_2DPOINT_F[count1];
                        float[] width = new float[count1];
                        float[] height = new float[count1];
                        float[] angle = new float[count1];
                        float[] x = new float[count1];
                        float[] y = new float[count1];
                        for (int i = 0; i < count1; i++)
                        {
                            pstTargetInfo[i] = stCNNDETECTInfo.pstTargetInfo[i];
                            rectinfo[i] = pstTargetInfo[i].stTargetRect;
                            width[i] = rectinfo[i].fWidth;
                            height[i] = rectinfo[i].fHeight;
                            angle[i] = rectinfo[i].fAngle;
                            TargetCenter[i] = rectinfo[i].stCentPt;
                            x[i] = TargetCenter[i].fPtX;
                            y[i] = TargetCenter[i].fPtY;
                        }

                        //图像
                        if (imageData1.Width != 0 && imageData1.Height != 0 && imagebytes1 != null)
                        {
                            uint ImageLenth = (uint)(imageData1.Width * imageData1.Height);
                            if (ImageLenth != imagebytes1.Length)
                            {
                                break;
                            }
                            imageData1.ImageBuffer = imagebytes1;

                            //获取图像数据
                            if (imageData1.ImageBuffer != null)
                            {
                                bmp = imageData1.ImageDataToBitmap().GetArgb32BitMap();
                                using (var g = bmp.CreateGraphic())
                                {
                                    ////画匹配框
                                    if (x != null && y != null && width != null && height != null && angle != null &&
                                        x.Length == y.Length && x.Length == width.Length && x.Length == height.Length && x.Length == angle.Length)
                                    {
                                        for (int i = 0; i < x.Length; i++)
                                        {
                                            g.DrawRect(Color.GreenYellow, 5, new PointF(x[i], y[i]), width[i], height[i], angle[i]);

                                        }
                                    }
                                }
                                curPictureBox1.Invoke(new Action(() =>
                                {
                                    curPictureBox2.Image = bmp;
                                }));
                            }

                        }
                        txt_count1.Text = count1.ToString();
                        count_old1 = count1;
                        //}
                        break;



                    default: break;
                }
            }

        }

        private static void write(string message)
        {
            fs = new FileStream(path, FileMode.Append);
            sw = new StreamWriter(fs);
            string dt = DateTime.Now.ToString();
            sw.Write(dt + message + "\r\n");
            sw.Close();
            fs.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            int nRet = ImvsSdkPFDefine.IMVS_EC_UNKNOWN;
            new Thread(new ThreadStart(delegate              // 开辟线程关闭, 防止主线程连续运行时阻塞
            {
                if (IntPtr.Zero != m_handle)
                {
                    nRet = ImvsPlatformSDK_API.IMVS_PF_CloseVisionMaster_CS(m_handle);
                    Thread.Sleep(100);
                }
                if (IntPtr.Zero != m_handle)
                {
                    nRet = ImvsPlatformSDK_API.IMVS_PF_DestroyHandle_CS(m_handle);
                    m_handle = IntPtr.Zero;
                }
               
                
                sw.Dispose();
                fs.Dispose();
                e.Cancel = false;
                Environment.Exit(0);
            }))
            { IsBackground = true }.Start();

            e.Cancel = true;
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            //AGFishOPCClient.WriteItem(txt_sdzs1.Text, Pro_X);
            //object a= AGFishOPCClient.ReadItem(VisionCode);
            //txt_sdzs1.Text = a.ToString();
            nProcID = uint.Parse(txt_sdzs1.Text);
        }


        /****************************************************************************
        * @fn           IntPtr转Bytes
        ****************************************************************************/
        public static byte[] IntPtr2Bytes(IntPtr ptr, int size)
        {
            byte[] bytes = null;
            if ((size > 0) && (null != ptr))
            {
                bytes = new byte[size];
                Marshal.Copy(ptr, bytes, 0, size);
            }
            return bytes;
        }

    }
}
