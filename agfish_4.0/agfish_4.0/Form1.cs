using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;
using System.Net.Sockets;
//using FrontedUI;
//using logtest;
//using DBTaskHelper;
using System.Diagnostics;
using VM.Core;
using VM.PlatformSDKCS;
using S7.Net;
using Utlis;
namespace AGFish
{
    public struct agfish_plc
    {
        public  Byte VisionCode;//定位标志0
        public  Byte FlatCode;//定位结果
        public Byte PLC_Data;//机器人状态  当前在干嘛
        public bool Data_chge_flag;//动作变动标志3.0
        public bool static1;//占位
        public bool static2;//占位
        public Byte static3;//占位
        public Byte static4;//占位
        public Byte static5;//占位
        public Byte  Alarm;//报警代码7
        public Single Pro_X;//8
        public Single Pro_Y;//12
        public Single Pro_Z;//16
        public Single Pro_RX;//20
        public Single static6;//占位
        public Single static7;//占位
        public bool banauto;//占位
        public bool AUTO;//占位
        public Byte static8;//报警代码
      
    }

    public struct GH_Plc
    {
        //plc罐号
        public Byte VisionCodeA;//定位标志
        public Byte FlatCodeA;//定位结果
        public Byte GH_num;//
        //plc包号
        public Byte VisionCodeB;//定位标志
        public Byte FlatCodeB;//定位结果
        public Byte BH_num;//
    }
    public partial class Form1 : Form
    {
        VmProcedure process1_DW, process1_GH, process1_BH;

        string SolutionPath4 = @"E:\ProjSetup\agfish_4.0\agfish.sol";                                //别忘了改路径

        public byte location_flag=0;
        bool mSolutionIsLoad = false;
        public static agfish_plc location = new agfish_plc();
        public static GH_Plc GH_num = new GH_Plc();
        static string path, path0;
        Int16 count1 = 0;
        Int16 count_old1 = 0;
        static Plc plc300;
        //记录数据
        static int[] PLCflag = { 25, 51, 11, 61, 12, 52, 71, 13, 81, 14, 54, 55, 56, 57, 91, 15, 72, 74, 75, 27 };
        static string[] str = new string[20];
        float xpos = 0;
        float ypos = 0;
        float zpos = 0;
        float rz = 0;
        string GB_num = "",TB_num="";
        int vmimgflag = 0,vmstrimgflag=0;
        static string work_result = "0";
        static string pianyistr = "", banautostr = "";
        dbTaskHelper dbhelper = new dbTaskHelper();
        public Form1()
        {
            InitializeComponent();
           // plc_init();
        }
      
        
        void plc_init()
        {
            try
            {
                //plc300 = new Plc(CpuType.S7300, PLCIP, 0, 2);
                plc300 = new Plc(CpuType.S71200, "127.0.0.1", 0, 1);
                plc300.Open();//创建PLC实例
                string strMsg = "成功打开PLC！";
                listBoxMsg.Items.Add(strMsg);
                listBoxMsg.TopIndex = listBoxMsg.Items.Count - 1;
            }
            catch (Exception e)
            {
                string strMsg = "打开PLC失败！";
                listBoxMsg.Items.Add(strMsg);
                listBoxMsg.TopIndex = listBoxMsg.Items.Count - 1;
                LogHelper.WriteLog("plc初始化出错！", e);
            }
        }
       

        private void LocationExecuteOnce_Click(object sender, EventArgs e)
        {
            string strMsg = null;//

            try
            {
                if (null == process1_DW) return;
                process1_DW.Run();

                FloatResultInfo single_px_info = process1_DW.GetFloatOutputResult("single_px");                                          
                xpos = single_px_info.pFloatValue[0];
                xpos = (float)Math.Round(xpos, 3);

                FloatResultInfo single_py_info = process1_DW.GetFloatOutputResult("single_py");                                          
                ypos = single_py_info.pFloatValue[0];
                ypos = (float)Math.Round(ypos, 3);

                FloatResultInfo single_angel_info = process1_DW.GetFloatOutputResult("single_angel");                                    
                rz = single_angel_info.pFloatValue[0];
                rz = (float)Math.Round(rz, 3);

                txt_sdzs1.Text=  xpos.ToString()+","+ ypos.ToString();
                strMsg= DateTime.Now.ToString() + ":" + xpos.ToString() + "," + ypos.ToString();
                
                plc300.Write("DB8.DBD8", xpos);//x
                plc300.Write("DB8.DBD12", ypos);//y
                plc300.Write("DB8.DBD20", rz);//rz
                switch(location_flag)
                {
                    case 1:
                        if(-600 < xpos && xpos < 600 && xpos != 0)
                        {
                            location_flag = 0;
                            plc300.Write("DB8.DBB1", 1);//rz
                            listBoxMsg.Items.Add("第一次成功："+strMsg);
                            listBoxMsg.TopIndex = listBoxMsg.Items.Count - 1;
                        }
                        else
                        {
                            location_flag = 0;
                            plc300.Write("DB8.DBB1", 0);//rz
                            listBoxMsg.Items.Add("第一次失败：" + strMsg);
                            listBoxMsg.TopIndex = listBoxMsg.Items.Count - 1;
                        }
                        break;
                    case 2:
                        if (-100 < xpos && xpos < 100 && xpos != 0)
                        {
                            location_flag = 0;
                            plc300.Write("DB8.DBB1", 1);//rz
                            listBoxMsg.Items.Add("第二次成功：" + strMsg);
                            listBoxMsg.TopIndex = listBoxMsg.Items.Count - 1;
                        }
                        else
                        {
                            location_flag = 0;
                            plc300.Write("DB8.DBB1", 0);//rz
                            listBoxMsg.Items.Add("第二次失败：" + strMsg);
                            listBoxMsg.TopIndex = listBoxMsg.Items.Count - 1;
                        }
                        break;
                    case 3:
                        if (-100 < xpos && xpos < 100 && xpos != 0)
                        {
                            location_flag = 0;
                            plc300.Write("DB8.DBB1", 1);//rz
                            listBoxMsg.Items.Add("第三次成功：" + strMsg);
                            listBoxMsg.TopIndex = listBoxMsg.Items.Count - 1;
                        }
                        else
                        {
                            location_flag = 0;
                            plc300.Write("DB8.DBB1", 0);//rz
                            listBoxMsg.Items.Add("第三次失败：" + strMsg);
                            listBoxMsg.TopIndex = listBoxMsg.Items.Count - 1;
                        }
                        break;
                    case 4:
                        if (-100 < xpos && xpos < 100 && xpos != 0)
                        {
                            location_flag = 0;
                            plc300.Write("DB8.DBB1", 1);//rz
                            listBoxMsg.Items.Add("第四次成功：" + strMsg);
                            listBoxMsg.TopIndex = listBoxMsg.Items.Count - 1;
                        }
                        else
                        {
                            location_flag = 0;
                            plc300.Write("DB8.DBB1", 0);//rz
                            listBoxMsg.Items.Add("第四次失败：" + strMsg);
                            listBoxMsg.TopIndex = listBoxMsg.Items.Count - 1;
                        }
                        break;
                    default:
                        location_flag = 0;
                        plc300.Write("DB8.DBB1", 0);//rz
                        listBoxMsg.Items.Add("错误标识：" + strMsg);
                        listBoxMsg.TopIndex = listBoxMsg.Items.Count - 1;
                        break;
                }
                    


            }
            catch (VmException ex)
            {
                strMsg = "getresult failed. Error Code: " + Convert.ToString(ex.errorCode, 16);
                //txt_message.AppendText(strMsg + "\r\n");
                listBoxMsg.Items.Add(strMsg);
                listBoxMsg.TopIndex = listBoxMsg.Items.Count - 1;
                LogHelper.WriteLog(strMsg, ex);
                return;
            }
        }

        private void Recognition_Click(object sender, EventArgs e)
        {
            string strMsg = null;//
            try
            {
                if (null == process1_GH) return;
                    process1_GH.Run();
                IntResultInfo status = process1_GH.GetIntOutputResult("statu");
                if(status.pIntValue[0]==1)
                {
                    StringResultInfo pxinfo = process1_GH.GetStringOutputResult("GH_str");                                           //输出string类型信息
                    StringValueInfo x = pxinfo.astStringValue[0];
                    
                    if(x.strValue.Length==2)
                    {
                        GB_num = x.strValue;
                        txt_count1.Text = x.strValue;
                        plc300.Write("DB9.DBB2", int.Parse(x.strValue));//plc罐号
                        plc300.Write("DB9.DBB1", 1);//plc罐号写入成功
                        strMsg = DateTime.Now.ToString() + ":成功识别罐号:" + x.strValue;
                        listBoxMsg.Items.Add(strMsg);
                        listBoxMsg.TopIndex = listBoxMsg.Items.Count - 1;
                    }
                    else
                    {
                        plc300.Write("DB9.DBB2", 0);//plc罐号
                        plc300.Write("DB9.DBB1", 0);//plc罐号写入成功
                        strMsg = DateTime.Now.ToString() + ":失败识别罐号:" + x.strValue;
                        listBoxMsg.Items.Add(strMsg);
                        listBoxMsg.TopIndex = listBoxMsg.Items.Count - 1;
                    }
                    
                }
                else
                {
                    plc300.Write("DB9.DBB2", 0);//plc罐号
                    plc300.Write("DB9.DBB1", 0);//plc罐号写入成功
                    strMsg = DateTime.Now.ToString() + ":失败识别罐号，模块运行错误";
                    listBoxMsg.Items.Add(strMsg);
                    listBoxMsg.TopIndex = listBoxMsg.Items.Count - 1;
                }
                
            }
            catch(VmException ex)
            {
                strMsg = "罐号识别错误 " + Convert.ToString(ex.errorCode, 16);
                //txt_message.AppendText(strMsg + "\r\n");
                listBoxMsg.Items.Add(strMsg);
                listBoxMsg.TopIndex = listBoxMsg.Items.Count - 1;
                LogHelper.WriteLog(strMsg, ex);
                return;
            }
          
        }
        private void button1_BH_Click(object sender, EventArgs e)
        {
            string strMsg = null;//
            try
            {
                if (null == process1_GH) return;
                    process1_GH.Run();
                IntResultInfo status = process1_GH.GetIntOutputResult("statu");
                if (status.pIntValue[0] == 1)
                {
                    StringResultInfo pxinfo = process1_GH.GetStringOutputResult("GH_str");                                           //输出string类型信息
                    StringValueInfo x = pxinfo.astStringValue[0];
                    if (x.strValue.Length == 2)
                    {
                        txt_count1.Text = x.strValue;
                        plc300.Write("DB9.DBB2", int.Parse(x.strValue));//plc罐号
                        plc300.Write("DB9.DBB1", 1);//plc罐号写入成功
                        strMsg = DateTime.Now.ToString() + ":成功识别罐号:" + x.strValue;
                        listBoxMsg.Items.Add(strMsg);
                        listBoxMsg.TopIndex = listBoxMsg.Items.Count - 1;
                    }
                    else
                    {
                        plc300.Write("DB9.DBB2", 0);//plc罐号
                        plc300.Write("DB9.DBB1", 0);//plc罐号写入成功
                        strMsg = DateTime.Now.ToString() + ":失败识别罐号:" + x.strValue;
                        listBoxMsg.Items.Add(strMsg);
                        listBoxMsg.TopIndex = listBoxMsg.Items.Count - 1;
                    }

                }
                else
                {
                    plc300.Write("DB9.DBB2", 0);//plc罐号
                    plc300.Write("DB9.DBB1", 0);//plc罐号写入成功
                    strMsg = DateTime.Now.ToString() + ":失败识别罐号，模块运行错误";
                    listBoxMsg.Items.Add(strMsg);
                    listBoxMsg.TopIndex = listBoxMsg.Items.Count - 1;
                }
            }
            catch (VmException ex)
            {
                strMsg = "罐号识别错误 " + Convert.ToString(ex.errorCode, 16);
                //txt_message.AppendText(strMsg + "\r\n");
                listBoxMsg.Items.Add(strMsg);
                listBoxMsg.TopIndex = listBoxMsg.Items.Count - 1;
                LogHelper.WriteLog(strMsg, ex);
            }
        }

        private void timer_deleterizhi_Tick(object sender, EventArgs e)
        {
            try
            {
                if (plc300.IsConnected)
                {
                    location = (agfish_plc)plc300.ReadStruct(typeof(agfish_plc), 8);
                    GH_num = (GH_Plc)plc300.ReadStruct(typeof(GH_Plc), 9);
                    if(location.VisionCode>=1&& location.VisionCode <= 4)//定位
                    {
                        location_flag = location.VisionCode;
                        LocationExecuteOnce_Click(null, null);
                        plc300.Write("DB8.DBB0",(Int16)0);//
                    }
                    if (GH_num.VisionCodeA ==1)//罐号识别
                    {
                        Recognition_Click(null, null);
                        plc300.Write("DB9.DBB0", (Int16)0);//
                    }
                    if (GH_num.VisionCodeB== 1)//包号识别
                    {
                        button1_BH_Click(null, null);
                        plc300.Write("DB9.DBB3", (Int16)0);//置位VisionCode
                    }
                }
                else
                {

                }
            }
            catch(Exception e1)
            {

            }
            
        }

        private void buttonCloseSolution_Click(object sender, EventArgs e)
        {
            string strMsg = null;

            try
            {
                if (mSolutionIsLoad == true)
                {
                    VmSolution.Instance.CloseSolution();
                    mSolutionIsLoad = false;  // 代表方案已经关闭
                }
                else
                {
                    strMsg = "No solution file.";
                    //txt_message.AppendText(strMsg + "\r\n");
                    listBoxMsg.Items.Add(strMsg);
                    listBoxMsg.TopIndex = listBoxMsg.Items.Count - 1;

                    return;
                }

            }
            catch (VmException ex)
            {
                strMsg = "CloseSolution failed. Error Code: " + Convert.ToString(ex.errorCode, 16);
                //txt_message.AppendText(strMsg + "\r\n");
                listBoxMsg.Items.Add(strMsg);
                listBoxMsg.TopIndex = listBoxMsg.Items.Count - 1;
                return;
            }
            strMsg = "CloseSolution success";
            //txt_message.AppendText(strMsg + "\r\n");
            listBoxMsg.Items.Add(strMsg);
            listBoxMsg.TopIndex = listBoxMsg.Items.Count - 1;
        }

        private void timer_savedata_Tick(object sender, EventArgs e)
        {
            string priod_done = "", alarmtime = "";

            try
            {
                agfish_plc temploc_value = location;
                bool datachgeflag = temploc_value.Data_chge_flag;
                byte alarmdata = temploc_value.Alarm;
                byte PLCdataflag = temploc_value.PLC_Data;
                if (alarmdata.ToString() != "0")
                {
                    alarmtime = DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss"));
                }
                if (datachgeflag)
                {
                    plc300.Write("DB8.DBX3.0", false);//已经读取了 机器人动作状态变量
                   // AGFishOPCClient.WriteItem(0.ToString(), Data_chge_flag);
                    
                    //priod_done = PLCdataflag.ToString();
                    for (int i = 0; i < PLCflag.Length; i++)
                    {
                        if (PLCdataflag.ToString() == PLCflag[i].ToString())
                        {
                            str[i] = DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss"));
                            break;
                        }
                    }
                    if (PLCdataflag.ToString() == "13")
                    {
                        float readzpos = temploc_value.Pro_Z;
                        zpos = (float)Math.Round(float.Parse(readzpos.ToString()), 2);
                    }
                    if (PLCdataflag.ToString() == "57")
                    {
                        //object pianyidata = AGFishOPCClient.ReadItem(pianyi);
                        //pianyistr = pianyidata.ToString();
                        bool banautodata = temploc_value.banauto;
                        banautostr = banautodata.ToString();
                    }
                    if (PLCdataflag.ToString() == "24")
                    {
                        work_result = "1";
                    }

                }
                if (PLCdataflag.ToString() == "27" || alarmdata.ToString() != "0")
                {
                    priod_done = "";
                    //AGFishOPCClient.WriteItem(0.ToString(), Alarm);
                    plc300.Write("DB8.DBB7", 0);//置位报警信号
                    string power_off = "1";
                    string sqltext1 = string.Format("insert into aglog values('{0}',{1},{2},{3},{4},'{5}','{6}','{7}','{8}',", GB_num, xpos, ypos, zpos, rz, work_result, pianyistr, banautostr, power_off);
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
                    GB_num = "";
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
                GB_num = "";
                work_result = "0";
                pianyistr = "";
                banautostr = "";
            }
        }

        //开启vm
        private void buttonOpenVM_Click(object sender, EventArgs e)
        {
            string strMsg = null;
            int nProgress = 0;
            progressBarSaveAndLoad.Value = nProgress;
            labelProgress.Text = nProgress.ToString();
            labelProgress.Refresh();

            try
            {
                VmSolution.Import(SolutionPath4, "");
                mSolutionIsLoad = true;
                process1_DW = (VmProcedure)VmSolution.Instance["流程1"];
                process1_GH = (VmProcedure)VmSolution.Instance["流程2"];
                process1_BH= (VmProcedure)VmSolution.Instance["流程3"];
                vmRenderControl1.ModuleSource = process1_DW;
                vmRenderControl2.ModuleSource = process1_GH;
                vmRenderControl3.ModuleSource = process1_BH;

            }
            catch (VmException ex)
            {
                strMsg = "LoadSolution failed. Error Code: " + Convert.ToString(ex.errorCode, 16);
                listBoxMsg.Items.Add(strMsg);
                listBoxMsg.TopIndex = listBoxMsg.Items.Count - 1;
                //LogHelper.WriteLog(strMsg, ex);
                return;
            }

            strMsg = "LoadSolution success";
            listBoxMsg.Items.Add(strMsg);
            listBoxMsg.TopIndex = listBoxMsg.Items.Count - 1;
            //LogHelper.WriteLog(strMsg);
            nProgress = 100;
            labelProgress.Text = nProgress.ToString();
            labelProgress.Refresh();
            progressBarSaveAndLoad.Value = Convert.ToInt32(nProgress);
        }
        //关闭vm
        private void buttonCloseVM_Click(object sender, EventArgs e)
        {
            string strMsg = null;

            try
            {
                if (mSolutionIsLoad == true)
                {
                    VmSolution.Instance.CloseSolution();
                    mSolutionIsLoad = false;  // 代表方案已经关闭
                }
                else
                {
                    strMsg = "No solution file.";
                    //txt_message.AppendText(strMsg + "\r\n");
                    listBoxMsg.Items.Add(strMsg);
                    listBoxMsg.TopIndex = listBoxMsg.Items.Count - 1;

                    return;
                }

            }
            catch (VmException ex)
            {
                strMsg = "CloseSolution failed. Error Code: " + Convert.ToString(ex.errorCode, 16);
                //txt_message.AppendText(strMsg + "\r\n");
                listBoxMsg.Items.Add(strMsg);
                listBoxMsg.TopIndex = listBoxMsg.Items.Count - 1;
                return;
            }
            strMsg = "CloseSolution success";
            //txt_message.AppendText(strMsg + "\r\n");
            listBoxMsg.Items.Add(strMsg);
            listBoxMsg.TopIndex = listBoxMsg.Items.Count - 1;


            /****************************************************************************
            * @fn           4.0的关闭方案
            * @fn           Close solution
            ****************************************************************************/
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //Process pr = new Process();//声明一个进程类对象
            //pr.StartInfo.FileName = @"C:\Runtime\PANYUANHANBIAO\PANYUANCESHI.exe";
            //pr.Start();
        }

       
        private void button1_Click(object sender, EventArgs e)
        {

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
