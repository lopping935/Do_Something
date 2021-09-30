using iMVS_6000PlatformSDKCS;
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using FrontedUI;
using System.IO;
using System.Net.Sockets;
using OPCAutomation;
//using FrontedUI;
using logtest;
using DBTaskHelper;
using System.Diagnostics;

namespace AGFish
{
    public partial class Form1 : Form
    {
        opchelper AGFishOPCClient = new opchelper();
        dbTaskHelper dbhelper;
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
        public static OPCItem banauto;
        public static OPCItem pianyi;
        //记录plc数据
        public static OPCItem Data_chge_flag;
        public static OPCItem PLC_Data;
        public static OPCItem Alarm;
        


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
        FeatureMatchData featureMacthData = new FeatureMatchData();//图像
        float angle = 1000;
        OCRString ocrsting1 = new OCRString();
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

        //记录数据
        static int[] PLCflag = { 25, 51, 11, 61, 12, 52, 71, 13, 81, 14, 54, 55, 56, 57, 91, 15, 72, 74, 75, 27 };
        static string[] str = new string[20];
        float xpos = 0;
        float ypos = 0;
        float zpos = 0;
        float rz = 0;
        string Car_num = "";

        int vmimgflag = 0,vmstrimgflag=0;
        public Form1()
        {
            InitializeComponent();
            curPictureBox1 = pictureBoxImg1;
            curPictureBox2 = pictureBox1;
            //Initopc();
            dbhelper = new dbTaskHelper();

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
                pianyi = AGFishOPCClient.AddItem("pianyi");
                banauto = AGFishOPCClient.AddItem("banauto");
                //记录plc动作时间
                Data_chge_flag = AGFishOPCClient.AddItem("Data_chge_flag");
                PLC_Data = AGFishOPCClient.AddItem("PLC_Data");
                Alarm = AGFishOPCClient.AddItem("Alarm");
            }
            catch (Exception err)
            {
                LogHelper.WriteLog("opcerr", err);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
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
                        MessageBox.Show("句柄异常，请重启软件，并关闭右下角vm平台。");
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


                buttonOpenVM_Click(null, null);                
                buttonLoadSolution_Click(null, null);
                Thread.Sleep(2000);
                //button1_Click_1(null, null);

            }
             catch(Exception e1)
            {
                LogHelper.WriteLog("程序启动失败", e1);
            }
        }

        int openstatus = 0;
        private void buttonOpenVM_Click(object sender, EventArgs e)
        {

            uint nWaitTime = 10000;
            string strMsg = null;
            if (IntPtr.Zero == m_handle)
            {

                strMsg = "句柄异常, 请重启软件!";
                MessageBox.Show(strMsg);
                openstatus = 5;
                return;
            }

            string VMPath = "C:\\Program Files\\VisionMaster3.4.0\\Applications\\VisionMaster.exe";
            int iRet = ImvsPlatformSDK_API.IMVS_PF_StartVisionMaster_CS(m_handle, VMPath, nWaitTime);
            if (ImvsSdkPFDefine.IMVS_EC_OK != iRet)
            {
                strMsg = "IMVS_PF_StartVisionMaster_CS Failed. Error Code: " + Convert.ToString(iRet, 16);
                txt_message.AppendText(strMsg + "\r\n");
                LogHelper.WriteLog(strMsg);
                openstatus = 5;
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
            string SolutionPath = AppDomain.CurrentDomain.BaseDirectory+ "dybd.sol";
            iRet = ImvsPlatformSDK_API.IMVS_PF_LoadSolution_CS(m_handle, SolutionPath, "");
            if (ImvsSdkPFDefine.IMVS_EC_OK != iRet)
            {
                strMsg = "IMVS_PF_LoadSolution_CS Failed. Error Code: " + Convert.ToString(iRet, 16);
                txt_message.AppendText(strMsg + "\r\n");
               // LogHelper.WriteLog(strMsg);
                return;
            }
            strMsg = "IMVS_PF_LoadSolution_CS success";
            txt_message.AppendText(strMsg + "\r\n");
            //WriteLog LogHelper.WriteLog(strMsg);
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
               // LogHelper.WriteLog(strMsg);
                return;
            }

            // 清空PictureBox控件中的内容
            pictureBoxImg1.Image = null;
            pictureBoxImg1.Refresh();

            strMsg = "IMVS_PF_CloseSolution_CS Success";
            txt_message.AppendText(strMsg + "\r\n");
            //LogHelper.WriteLog(strMsg);
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
               // LogHelper.WriteLog(strMsg);
                return;
            }

            strMsg = "IMVS_PF_ShowVisionMaster_CS Success";
            txt_message.AppendText(strMsg + "\r\n");
            //LogHelper.WriteLog(strMsg);
        }
        string location_flag, recognition_flag;
        
        private void timer_deleterizhi_Tick(object sender, EventArgs e)
        {
            try
            {
                object location = AGFishOPCClient.ReadItem(VisionCode);//定位flag
                object recogizestr = AGFishOPCClient.ReadItem(VisionCodeA);
                if (location.ToString() == "1" || location.ToString() == "2" || location.ToString() == "3" || location.ToString() == "4" || location.ToString() == "5")
                {

                    location_flag = location.ToString();
                    AGFishOPCClient.WriteItem(0.ToString(), VisionCode);
                    LocationExecuteOnce_Click(null, null);
                    txt_message.Invoke(new Action(() => { txt_message.AppendText("请求第" + location_flag + "次拍照" + "\r\n"); }));

                }
                if (recogizestr.ToString() == "1")
                {

                    recognition_flag = recogizestr.ToString();
                    AGFishOPCClient.WriteItem(0.ToString(), VisionCodeA);
                    Recognition_Click(null, null);
                    txt_message.Invoke(new Action(() => { txt_message.AppendText("请求字符识别" + "\r\n"); }));

                }
                AGFishOPCClient.WriteItem(1.ToString(), xintiao);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("时钟执行错误", ex);
            }
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
                return;
            }

            //strMsg = "相机1:IMVS_PF_ExecuteOnce_V30_CS Success";
            //txt_message.AppendText(strMsg + "\r\n");
            //LogHelper.WriteLog(strMsg);
        }

        private void LocationExecuteOnce_Click(object sender, EventArgs e)
        {
            nProcID = 10000;
            if (vmimgflag == 50)
            {
                vmimgflag = 0;
                buttonLoadSolution_Click(null, null);
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
                string SolutionPath = AppDomain.CurrentDomain.BaseDirectory + "dybd.sol";
                iRet = ImvsPlatformSDK_API.IMVS_PF_LoadSolution_CS(m_handle, SolutionPath, "");
                if (ImvsSdkPFDefine.IMVS_EC_OK != iRet)
                {
                    strMsg = "IMVS_PF_LoadSolution_CS Failed. Error Code: " + Convert.ToString(iRet, 16);
                    txt_message.AppendText(strMsg + "\r\n");
                    // LogHelper.WriteLog(strMsg);
                    return;
                }
                strMsg = "IMVS_PF_LoadSolution_CS success";
                txt_message.AppendText(strMsg + "\r\n");
            }
            ExecuteOnce();
        }

        private void Recognition_Click(object sender, EventArgs e)
        {
            nProcID = 10001;
            if (vmstrimgflag == 50)
            {
                vmstrimgflag = 0;
                buttonLoadSolution_Click(null, null);
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
                string SolutionPath = AppDomain.CurrentDomain.BaseDirectory + "dybd.sol";
                iRet = ImvsPlatformSDK_API.IMVS_PF_LoadSolution_CS(m_handle, SolutionPath, "");
                if (ImvsSdkPFDefine.IMVS_EC_OK != iRet)
                {
                    strMsg = "IMVS_PF_LoadSolution_CS Failed. Error Code: " + Convert.ToString(iRet, 16);
                    txt_message.AppendText(strMsg + "\r\n");
                    // LogHelper.WriteLog(strMsg);
                    return;
                }
                strMsg = "IMVS_PF_LoadSolution_CS success";
                txt_message.AppendText(strMsg + "\r\n");
            }
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
                   // LogHelper.WriteLog(strMsg);
                return;
                }
               // strMsg = "相机1:IMVS_PF_ContinousExecute_V30_CS Success";
               // txt_message.AppendText(strMsg + "\r\n");
               // LogHelper.WriteLog(strMsg);

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
           // LogHelper.WriteLog(strMsg);

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
        Bitmap bmp,bmp1;
      //回调结果数据  发送给plc
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


                    case ImvsSdkPFDefine.MODU_NAME_CAMERAMODULE:
                        //相机图像
                        ImvsSdkPFDefine.IMVS_PF_CAMERAMODULE_INFO stCameraImgInfo = (ImvsSdkPFDefine.IMVS_PF_CAMERAMODULE_INFO)Marshal.PtrToStructure(struResultInfo.pData, typeof(ImvsSdkPFDefine.IMVS_PF_CAMERAMODULE_INFO));
                        imageData1.Width = stCameraImgInfo.stImgInfo.iWidth;
                        imageData1.Height = stCameraImgInfo.stImgInfo.iHeight;
                        imagebytes1 = IntPtr2Bytes(stCameraImgInfo.stImgInfo.pImgData, stCameraImgInfo.stImgInfo.iImgDataLen);
                       
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
                            bmp1 = imageData1.ImageDataToBitmap().GetArgb32BitMap();
                            curPictureBox1.Invoke(new Action(() => { curPictureBox1.Image = bmp1; }));
                        }
                        else
                        {
                            vmstrimgflag = 50;
                            Exception e = null;
                            LogHelper.WriteLog("字符相机获取图像失败，尝试重启", e);
                        }
                        break;
                    #region ocr
                    case ImvsSdkPFDefine.MODU_NAME_OCRMODU:
                        ImvsSdkPFDefine.IMVS_PF_OCR_MODU_INFO Rec_OCR = (ImvsSdkPFDefine.IMVS_PF_OCR_MODU_INFO)Marshal.PtrToStructure(struResultInfo.pData, typeof(ImvsSdkPFDefine.IMVS_PF_OCR_MODU_INFO));
                        int strnum = Rec_OCR.iRecogResultNum;
                        ImvsSdkPFDefine.IMVS_PF_OCR_RES_INFO[] Rec_String = Rec_OCR.pstOCRResInfo;
                        
                        if(strnum>0)
                        {
                            ImvsSdkPFDefine.IMVS_PF_REGION Rect = Rec_String[0].stDetectRegion;
                            string s1 = Rec_String[0].strTextInfo;
                            txt_count1.Text = s1;
                            Car_num = s1;
                            AGFishOPCClient.WriteItem(s1, Product_Type);
                            //AGFishOPCClient.WriteItem(5.ToString(), Product_Type);
                            if(Rec_String[0].iCharNum==3)
                                AGFishOPCClient.WriteItem(1.ToString(), FlatCodeA);

                            using (var g = bmp1.CreateGraphic())
                            {
                                g.DrawRect(Color.GreenYellow, 3, new PointF(Rect.stCenterPt.fPtX, Rect.stCenterPt.fPtY), Rect.fWidth, Rect.fHeight, Rect.fAngle);
                            }
                        }
                        curPictureBox1.Invoke(new Action(() => { curPictureBox1.Image = bmp1; }));
                        break;
                    #endregion
                    case ImvsSdkPFDefine.MODU_NAME_OCRDLMODU:
                        ImvsSdkPFDefine.IMVS_PF_OCRDL_MODU_INFO Rec_DLOCR = (ImvsSdkPFDefine.IMVS_PF_OCRDL_MODU_INFO)Marshal.PtrToStructure(struResultInfo.pData, typeof(ImvsSdkPFDefine.IMVS_PF_OCRDL_MODU_INFO));
                        ImvsSdkPFDefine.IMVS_PF_OCRDL_RESULT_INFO[] Rec_DLOCRrult = Rec_DLOCR.pstOcrDLResInfo;
                        int dlstrnum = Rec_DLOCR.iOcrDLResNum;

                        if (dlstrnum >0)
                        {
                            AGFishOPCClient.WriteItem("5", VisionCodeA);
                            string dlstr = Rec_DLOCRrult[0].strFontInfo;
                            txt_count1.Text = dlstr;
                            if(dlstr.Length==2)
                            {
                                Car_num = dlstr;
                                AGFishOPCClient.WriteItem(dlstr, Product_Type);
                                AGFishOPCClient.WriteItem(1.ToString(), FlatCodeA);
                            }
                            else
                            {
                                AGFishOPCClient.WriteItem("", Product_Type);
                                AGFishOPCClient.WriteItem(0.ToString(), FlatCodeA);
                            }                         
                        }
                        else
                        {
                            AGFishOPCClient.WriteItem("4", VisionCodeA);
                        }
                        break;
                    default: break;
                }
            }
            if (struResultInfo.nProcessID == 10002)
            {
                switch (struResultInfo.strModuleName)//struResultInfo.nModuleID
                {


                    case ImvsSdkPFDefine.MODU_NAME_CAMERAMODULE:
                        //相机图像
                        ImvsSdkPFDefine.IMVS_PF_CAMERAMODULE_INFO stCameraImgInfo = (ImvsSdkPFDefine.IMVS_PF_CAMERAMODULE_INFO)Marshal.PtrToStructure(struResultInfo.pData, typeof(ImvsSdkPFDefine.IMVS_PF_CAMERAMODULE_INFO));
                        imageData1.Width = stCameraImgInfo.stImgInfo.iWidth;
                        imageData1.Height = stCameraImgInfo.stImgInfo.iHeight;
                        imagebytes1 = IntPtr2Bytes(stCameraImgInfo.stImgInfo.pImgData, stCameraImgInfo.stImgInfo.iImgDataLen);

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
                            bmp1 = imageData1.ImageDataToBitmap().GetArgb32BitMap();
                            curPictureBox1.Invoke(new Action(() => { curPictureBox1.Image = bmp1; }));
                        }
                        else
                        {
                            vmstrimgflag = 50;
                            Exception e = null;
                            LogHelper.WriteLog("字符相机获取图像失败，尝试重启", e);
                        }
                        break;
                    #region ocr
                    case ImvsSdkPFDefine.MODU_NAME_OCRMODU:
                        ImvsSdkPFDefine.IMVS_PF_OCR_MODU_INFO Rec_OCR = (ImvsSdkPFDefine.IMVS_PF_OCR_MODU_INFO)Marshal.PtrToStructure(struResultInfo.pData, typeof(ImvsSdkPFDefine.IMVS_PF_OCR_MODU_INFO));
                        int strnum = Rec_OCR.iRecogResultNum;
                        ImvsSdkPFDefine.IMVS_PF_OCR_RES_INFO[] Rec_String = Rec_OCR.pstOCRResInfo;

                        if (strnum > 0)
                        {
                            ImvsSdkPFDefine.IMVS_PF_REGION Rect = Rec_String[0].stDetectRegion;
                            string s1 = Rec_String[0].strTextInfo;
                            txt_count1.Text = s1;
                            Car_num = s1;
                            AGFishOPCClient.WriteItem(s1, Product_Type);
                            //AGFishOPCClient.WriteItem(5.ToString(), Product_Type);
                            if (Rec_String[0].iCharNum == 3)
                                AGFishOPCClient.WriteItem(1.ToString(), FlatCodeA);

                            using (var g = bmp1.CreateGraphic())
                            {
                                g.DrawRect(Color.GreenYellow, 3, new PointF(Rect.stCenterPt.fPtX, Rect.stCenterPt.fPtY), Rect.fWidth, Rect.fHeight, Rect.fAngle);
                            }
                        }
                        curPictureBox1.Invoke(new Action(() => { curPictureBox1.Image = bmp1; }));
                        break;
                    #endregion
                    case ImvsSdkPFDefine.MODU_NAME_OCRDLMODU:
                        ImvsSdkPFDefine.IMVS_PF_OCRDL_MODU_INFO Rec_DLOCR = (ImvsSdkPFDefine.IMVS_PF_OCRDL_MODU_INFO)Marshal.PtrToStructure(struResultInfo.pData, typeof(ImvsSdkPFDefine.IMVS_PF_OCRDL_MODU_INFO));
                        ImvsSdkPFDefine.IMVS_PF_OCRDL_RESULT_INFO[] Rec_DLOCRrult = Rec_DLOCR.pstOcrDLResInfo;
                        int dlstrnum = Rec_DLOCR.iOcrDLResNum;

                        if (dlstrnum > 0)
                        {
                            AGFishOPCClient.WriteItem("5", VisionCodeA);
                            string dlstr = Rec_DLOCRrult[0].strFontInfo;
                            txt_count1.Text = dlstr;
                            if (dlstr.Length == 2)
                            {
                                Car_num = dlstr;
                                AGFishOPCClient.WriteItem(dlstr, Product_Type);
                                AGFishOPCClient.WriteItem(1.ToString(), FlatCodeA);
                            }
                            else
                            {
                                AGFishOPCClient.WriteItem("", Product_Type);
                                AGFishOPCClient.WriteItem(0.ToString(), FlatCodeA);
                            }
                        }
                        else
                        {
                            AGFishOPCClient.WriteItem("4", VisionCodeA);
                        }
                        break;
                    default: break;
                }
            }
            if (struResultInfo.nProcessID == 10000)
            {
                try
                {

                
                switch (struResultInfo.strModuleName)//struResultInfo.nModuleID
                {

                        #region 相机图像
                        case ImvsSdkPFDefine.MODU_NAME_CAMERAMODULE://1//相机图像
                            ImvsSdkPFDefine.IMVS_PF_CAMERAMODULE_INFO stCameraImgInfo = (ImvsSdkPFDefine.IMVS_PF_CAMERAMODULE_INFO)Marshal.PtrToStructure(struResultInfo.pData, typeof(ImvsSdkPFDefine.IMVS_PF_CAMERAMODULE_INFO));
                            imageData1.Width = stCameraImgInfo.stImgInfo.iWidth;
                            imageData1.Height = stCameraImgInfo.stImgInfo.iHeight;
                            //本地图像测试 MODU_NAME_LOCALIMAGEVIEW
                            //case ImvsSdkPFDefine.MODU_NAME_LOCALIMAGEVIEW:
                            //     ImvsSdkPFDefine.IMVS_PF_LOCALIMAGEVIEW_MODU_INFO stCameraImgInfo = (ImvsSdkPFDefine.IMVS_PF_LOCALIMAGEVIEW_MODU_INFO)Marshal.PtrToStructure(struResultInfo.pData, typeof(ImvsSdkPFDefine.IMVS_PF_LOCALIMAGEVIEW_MODU_INFO));
                            //     imageData1.Width = stCameraImgInfo.stImgInfo.iWidth;
                            //     imageData1.Height = stCameraImgInfo.stImgInfo.iHeight;




                            imagebytes1 = IntPtr2Bytes(stCameraImgInfo.stImgInfo.pImgData, stCameraImgInfo.stImgInfo.iImgDataLen);                      

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
                            
                        }
                        else
                        {
                            vmimgflag = 50;
                            Exception e = null;
                            LogHelper.WriteLog("相机获取图像失败，尝试重启", e);
                        }
                            break;

                        #endregion
                        case ImvsSdkPFDefine.MODU_NAME_HPFEATUREMATCHMODU:
                            ImvsSdkPFDefine.IMVS_PF_HPFEATUREMATCH_MODU_INFO stHpFeatMatchInfo = (ImvsSdkPFDefine.IMVS_PF_HPFEATUREMATCH_MODU_INFO)Marshal.PtrToStructure(struResultInfo.pData, typeof(ImvsSdkPFDefine.IMVS_PF_HPFEATUREMATCH_MODU_INFO));
                            if (stHpFeatMatchInfo.stMatchConInfo.iPtNum > 0)
                            {
                                featureMacthData.outLinePointInfo = new ImvsSdkPFDefine.IMVS_PATMATCH_POINT_INFO[stHpFeatMatchInfo.stMatchConInfo.iPtNum];
                                for (int iNum = 0; iNum < stHpFeatMatchInfo.stMatchConInfo.iPtNum; iNum++)
                                {
                                    featureMacthData.outLinePointInfo[iNum] = stHpFeatMatchInfo.stMatchConInfo.pstPatMatchPt[iNum];
                                }
                            }
                            
                            using (var g = bmp.CreateGraphic())
                            { 
                                if (featureMacthData.outLinePointInfo != null)
                                {
                                    for (int k = 0; k < featureMacthData.outLinePointInfo.Length; k++)
                                    {
                                        g.DrawPoint(Color.GreenYellow, new PointF(featureMacthData.outLinePointInfo[k].fMatchOutlineX, featureMacthData.outLinePointInfo[k].fMatchOutlineY));
                                    }
                                }
                                curPictureBox2.Invoke(new Action(() =>{curPictureBox2.Image = bmp;}));
                            }

                            //featureMacthData.MatchBoxAngle=new float[stHpFeatMatchInfo.iMatchNum];
                            //featureMacthData.MatchBoxAngle[0] = stHpFeatMatchInfo.pstMatchBaseInfo[0].stMatchBox.fAngle;
                           // angle = stHpFeatMatchInfo.pstMatchBaseInfo[0].stMatchBox.fAngle;
                            break;
                            //IMVS_PF_HPMATCH_PT_INFO
                    #region 单点对位
                        case ImvsSdkPFDefine.MODU_NAME_SINGLEPOINTALIGNMODU://1
                         ImvsSdkPFDefine.IMVS_PF_SINGLEPOINTALIGN_MODU_INFO siglepoint = (ImvsSdkPFDefine.IMVS_PF_SINGLEPOINTALIGN_MODU_INFO)Marshal.PtrToStructure(struResultInfo.pData, typeof(ImvsSdkPFDefine.IMVS_PF_SINGLEPOINTALIGN_MODU_INFO));
                         float x = (float)Math.Round(siglepoint.fDeltaX, 3);
                         float y = (float)Math.Round(siglepoint.fDeltaY, 3);
                         float angle = (float)Math.Round(siglepoint.fDeltaTheta, 3);
                            StringBuilder sb = new StringBuilder();
                            sb.Append("25");
                            sb.Append("\",\"");
                            sb.Append(x.ToString());
                            sb.Append("\",\"");
                            sb.Append(y.ToString());
                            LogHelper.WriteLog(sb.ToString());
                            if (location_flag == "1")
                         {
                                location_flag = "";
                                xpos = x;
                                ypos = y;
                                rz = angle;
                                AGFishOPCClient.WriteItem(x.ToString(), Pro_X);                                                 
                                AGFishOPCClient.WriteItem(y.ToString(), Pro_Y);
                                AGFishOPCClient.WriteItem(angle.ToString(), Pro_RX);
                                AGFishOPCClient.WriteItem(10.ToString(), VisionCode);
                                if (-600 < x && x < 600 && x != 0)
                                {
                                    AGFishOPCClient.WriteItem(1.ToString(), FlatCode);
                                    txt_message.Invoke(new Action(() => { txt_message.AppendText("拍照引导1成功将10写入PLC" + "\r\n"); }));
                                    txt_message.Invoke(new Action(() => { txt_message.AppendText("第一次坐标位置" + x.ToString() + "," + y.ToString() + "," + angle.ToString() + "\r\n"); }));
                                    LogHelper.WriteLog("第一次坐标位置" + x.ToString() + "," + y.ToString());
                                    //StringBuilder sb = new StringBuilder();
                                    //sb.Append("25");
                                    //sb.Append("\",\"");
                                    //sb.Append(x.ToString());
                                    //sb.Append("\",\"");
                                    //sb.Append(y.ToString());
                                    //LogHelper.WriteLog(sb.ToString());

                                }
                                else
                                {
                                    AGFishOPCClient.WriteItem(0.ToString(), FlatCode);
                                    txt_message.Invoke(new Action(() => { txt_message.AppendText("拍照引导1失败将0写入PLC" + "\r\n"); }));
                                }
                         }
                         if (location_flag == "2")
                        {
                            location_flag = "";
                            AGFishOPCClient.WriteItem(x.ToString(), Pro_X);
                            AGFishOPCClient.WriteItem(y.ToString(), Pro_Y); 
                            AGFishOPCClient.WriteItem(angle.ToString(), Pro_RX);
                            AGFishOPCClient.WriteItem(6.ToString(), VisionCode);
                            if (-100 < x && x < 100 && x != 0)
                            {
                                AGFishOPCClient.WriteItem(1.ToString(), FlatCode);
                                txt_message.Invoke(new Action(() => { txt_message.AppendText("拍照引导2成功将6写入PLC" + "\r\n"); }));
                                txt_message.Invoke(new Action(() => { txt_message.AppendText("第二次坐标位置" + x.ToString() + "," + y.ToString() + "," + angle.ToString() + "\r\n"); }));
                                //LogHelper.WriteLog("第二次坐标位置" + x.ToString() + "," + y.ToString());
                             }
                            else
                            {
                                AGFishOPCClient.WriteItem(0.ToString(), FlatCode);
                                txt_message.Invoke(new Action(() => { txt_message.AppendText("拍照引导2失败将0写入PLC" + "\r\n"); }));
                                //Thread th1 = new Thread((){ nProcID = 10001; ExecuteOnce();});
                            }
                        }
                         if (location_flag == "3" )
                        {
                            location_flag = "";
                            AGFishOPCClient.WriteItem(x.ToString(), Pro_X);
                            AGFishOPCClient.WriteItem(y.ToString(), Pro_Y);
                            AGFishOPCClient.WriteItem(angle.ToString(), Pro_RX);
                            AGFishOPCClient.WriteItem(7.ToString(), VisionCode);
                            if (-100 < x && x < 100 && x!= 0)
                            {
                                AGFishOPCClient.WriteItem(1.ToString(), FlatCode);
                                txt_message.Invoke(new Action(() => { txt_message.AppendText("拍照引导3成功将7写入PLC" + "\r\n"); }));
                                txt_message.Invoke(new Action(() => { txt_message.AppendText("第三次坐标位置" + x.ToString() + "," + y.ToString() + "," + angle.ToString() + "\r\n"); }));
                                //LogHelper.WriteLog("第二次坐标位置" + x.ToString() + "," + y.ToString());
                            }
                            else
                            {
                                AGFishOPCClient.WriteItem(0.ToString(), FlatCode);
                                txt_message.Invoke(new Action(() => { txt_message.AppendText("拍照引导3失败将0写入PLC" + "\r\n"); }));
                                //Thread th1 = new Thread((){ nProcID = 10001; ExecuteOnce();});
                            }
                        }
                         if (location_flag == "4")
                        {
                            location_flag = "";
                            AGFishOPCClient.WriteItem(x.ToString(), Pro_X);
                            AGFishOPCClient.WriteItem(y.ToString(), Pro_Y);
                            AGFishOPCClient.WriteItem(angle.ToString(), Pro_RX);
                            AGFishOPCClient.WriteItem(8.ToString(), VisionCode);
                            if (-100 < x && x < 100 && x!= 0)
                            {
                                AGFishOPCClient.WriteItem(1.ToString(), FlatCode);
                                txt_message.Invoke(new Action(() => { txt_message.AppendText("拍照引导4成功将8写入PLC" + "\r\n"); }));
                                txt_message.Invoke(new Action(() => { txt_message.AppendText("第四次坐标位置" + x.ToString() + "," + y.ToString() + "," + angle.ToString() + "\r\n"); }));
                                //LogHelper.WriteLog("第二次坐标位置" + x.ToString() + "," + y.ToString());
                            }
                            else
                            {
                                AGFishOPCClient.WriteItem(0.ToString(), FlatCode);
                                txt_message.Invoke(new Action(() => { txt_message.AppendText("拍照引导4失败将0写入PLC" + "\r\n"); }));
                                //Thread th1 = new Thread((){ nProcID = 10001; ExecuteOnce();});
                            }
                        }
                         if (location_flag == "5")
                        {
                            location_flag = "";
                            AGFishOPCClient.WriteItem(x.ToString(), Pro_X);
                            AGFishOPCClient.WriteItem(y.ToString(), Pro_Y);
                            AGFishOPCClient.WriteItem(angle.ToString(), Pro_RX);
                            AGFishOPCClient.WriteItem(9.ToString(), VisionCode);
                            if (-100 < x && x < 100 && x!=0)
                            {
                                AGFishOPCClient.WriteItem(1.ToString(), FlatCode);
                                txt_message.Invoke(new Action(() => { txt_message.AppendText("拍照引导5成功将9写入PLC" + "\r\n"); }));
                                txt_message.Invoke(new Action(() => { txt_message.AppendText("第五次坐标位置" + x.ToString() + "," + y.ToString() + "," + angle.ToString() + "\r\n"); }));
                                //LogHelper.WriteLog("第二次坐标位置" + x.ToString() + "," + y.ToString());
                            }
                            else
                            {
                                AGFishOPCClient.WriteItem(0.ToString(), FlatCode);
                                txt_message.Invoke(new Action(() => { txt_message.AppendText("拍照引导5失败将0写入PLC" + "\r\n"); }));
                                //Thread th1 = new Thread((){ nProcID = 10001; ExecuteOnce();});
                            }
                        }
                            txt_sdzs1.Text = x.ToString() + "," + y.ToString();
                        break;
                    #endregion
                  


                    default: break;
                }
            }
                catch(Exception e)
                {
                    LogHelper.WriteLog("recallbacke process 1000 error", e);
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

        static string work_result = "0";
        static string pianyistr = "", banautostr = "";

        private void button_BH_Click(object sender, EventArgs e)
        {

        }

        private void timer_savedata_Tick(object sender, EventArgs e)
        {
            string priod_done = "",alarmtime="";
         
            try
            {
                
                object datachgeflag = AGFishOPCClient.ReadItem(Data_chge_flag);
                object alarmdata = AGFishOPCClient.ReadItem(Alarm);
                if(alarmdata.ToString()!="0")
                {
                    alarmtime= DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss"));
                }
                if ((bool)datachgeflag )
                {
                    AGFishOPCClient.WriteItem(0.ToString(), Data_chge_flag);
                    object PLCdataflag = AGFishOPCClient.ReadItem(PLC_Data);
                    priod_done = PLCdataflag.ToString();
                    for (int i = 0; i < PLCflag.Length; i++)
                    {
                        if (PLCdataflag.ToString() == PLCflag[i].ToString())
                        {
                            str[i] = DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss"));
                            break;
                        }
                    }
                    if(PLCdataflag.ToString()=="13")
                    {
                        object readzpos= AGFishOPCClient.ReadItem(Pro_Z);
                        zpos = (float)Math.Round( float.Parse(readzpos.ToString()),2);
                    }
                    if (PLCdataflag.ToString() == "57")
                    {
                        object pianyidata = AGFishOPCClient.ReadItem(pianyi);
                        pianyistr = pianyidata.ToString();
                        object banautodata = AGFishOPCClient.ReadItem(banauto);
                        banautostr = banautodata.ToString();
                    }
                    if (PLCdataflag.ToString() == "24")
                    {
                        work_result = "1";
                    }

                }
                if (priod_done == "27"|| alarmdata.ToString()!="0")
                {
                    priod_done = "";
                    AGFishOPCClient.WriteItem(0.ToString(), Alarm);
                    string power_off = "1";
                    string sqltext1 = string.Format("insert into aglog values('{0}',{1},{2},{3},{4},'{5}','{6}','{7}','{8}',", Car_num, xpos, ypos, zpos, rz, work_result, pianyistr,banautostr, power_off);
                    string sqltext2 = "";
                    for (int i = 0; i < str.Length; i++)
                    {
                        sqltext2 += "'" + str[i] + "'";
                        sqltext2 += ',';
                    }
                    string sqltext3 = sqltext1 + sqltext2 + string.Format("'{0}','{1}')", alarmdata.ToString(), alarmtime);
                    dbhelper.MultithreadExecuteNonQuery(sqltext3);
                    str = new string[20];
                    xpos = 0;
                    ypos = 0;
                    zpos = 0;
                    rz = 0;
                    Car_num = "";
                    work_result = "0";
                    pianyistr = "";
                    banautostr = "";
                }

            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("数据记录失败", ex);
                str = new string[20];
                xpos = 0;
                ypos = 0;
                zpos = 0;
                rz = 0;
                Car_num = "";
                work_result = "0";
                pianyistr = "";
                banautostr = "";
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Process pr = new Process();//声明一个进程类对象
            pr.StartInfo.FileName = @"C:\Runtime\PANYUANHANBIAO\PANYUANCESHI.exe";
            pr.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Process[] proc = Process.GetProcessesByName("Cognex.Designer.VisionPro.Runtime");//创建一个进程数组，把与此进程相关的资源关联。
            for (int i = 0; i < proc.Length; i++)
            {
                proc[i].Kill();  //逐个结束进程.
            }
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
