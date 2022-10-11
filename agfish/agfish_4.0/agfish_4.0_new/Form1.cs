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
using logtest;
using System.Collections.Generic;
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


        string[] s_bh = new string[10];
        List<string> baohao = new List<string>();
        List<int> numlist = new List<int>();
        int test;
        string bhsb;
        int i;


        FloatResultInfo single_bh_info;

        int baohao_flag = 1;





        VmProcedure process1_DW, process1_GH, process1_BH;

        string SolutionPath4 = @"C:\Users\agfish\ProjSetup\agfish_4.0\agfishps.sol";                                //别忘了改路径

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
        int number;


        float xpos = 0;
        float ypos = 0;
        float zpos = 0;
        float rz = 0;

        float dw_Y = 0;
        float dw_Y01;
        float dw_Y02;
        string GB_num = "",TB_num="";
        int vmimgflag = 0,vmstrimgflag=0;
        static string work_result = "0";
        static string pianyistr = "", banautostr = "";
        dbTaskHelper dbhelper = new dbTaskHelper();
        public Form1()
        {
            InitializeComponent();
            try
            {
                buttonOpenVM_Click(null, null);
                plc_init();
                //3D相机
                button1_Click_1(null,null);
                timer_deleterizhi.Enabled = true;
                timer_savedata.Enabled = true;
            }
            catch
            {
                string strMsg = "初始化程序出错！";
                //txt_message.AppendText(strMsg + "\r\n");
                listBoxMsg.Items.Add(strMsg);
                listBoxMsg.TopIndex = listBoxMsg.Items.Count - 1;
            }
            
        }
      
        
        void plc_init()
        {
            try
            {
                plc300 = new Plc(CpuType.S7300, "140.80.0.190", 0, 2);
                //plc300 = new Plc(CpuType.S71200, "127.0.0.1", 0, 1);
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


                IntResultInfo onpoint_model_status = process1_DW.GetIntOutputResult("statu");
                if (onpoint_model_status.pIntValue[0] == 1)
                {
                    FloatResultInfo single_px_info = process1_DW.GetFloatOutputResult("single_px");
                    xpos = single_px_info.pFloatValue[0];
                    xpos = (float)Math.Round(xpos, 3);

                    FloatResultInfo single_py_info = process1_DW.GetFloatOutputResult("single_py");
                    ypos = single_py_info.pFloatValue[0];
                    ypos = (float)Math.Round(ypos, 3);

                    FloatResultInfo single_angel_info = process1_DW.GetFloatOutputResult("single_angel");
                    rz = single_angel_info.pFloatValue[0];
                    rz = (float)Math.Round(rz, 3);

                    txt_sdzs1.Text = xpos.ToString() + "," + ypos.ToString();
                    string logtime = DateTime.Now.ToString() + ":";
                    strMsg = xpos.ToString() + "," + ypos.ToString();

                    plc300.Write("DB8.DBD8", xpos);//x
                    plc300.Write("DB8.DBD12", ypos);//y
                    plc300.Write("DB8.DBD20", rz);//rz
                    switch (location_flag)
                    {
                        case 1:
                            if (-600 < xpos && xpos < 600 && xpos != 0)
                            {
                                location_flag = 0;
                                plc300.Write("DB8.DBB0", (byte)10);//
                                plc300.Write("DB8.DBB1", (byte)1);//rz
                                listBoxMsg.Items.Add(logtime + "第一次成功：" + strMsg);
                                listBoxMsg.TopIndex = listBoxMsg.Items.Count - 1;
                            }
                            else
                            {
                                location_flag = 0;
                                plc300.Write("DB8.DBB0", (byte)10);//
                                plc300.Write("DB8.DBB1", (byte)0);//rz
                                listBoxMsg.Items.Add(logtime + "第一次失败：" + strMsg);
                                listBoxMsg.TopIndex = listBoxMsg.Items.Count - 1;
                            }
                            break;
                        case 2:
                            if (-100 < xpos && xpos < 100 && xpos != 0)
                            {
                                location_flag = 0;
                                plc300.Write("DB8.DBB0", (byte)6);//
                                plc300.Write("DB8.DBB1", (byte)1);//rz
                                listBoxMsg.Items.Add(logtime + "第二次成功：" + strMsg);
                                listBoxMsg.TopIndex = listBoxMsg.Items.Count - 1;
                            }
                            else
                            {
                                location_flag = 0;
                                plc300.Write("DB8.DBB0", (byte)6);//
                                plc300.Write("DB8.DBB1", (byte)0);//rz
                                listBoxMsg.Items.Add(logtime + "第二次失败：" + strMsg);
                                listBoxMsg.TopIndex = listBoxMsg.Items.Count - 1;
                            }
                            break;
                        case 3:
                            if (-100 < xpos && xpos < 100 && xpos != 0)
                            {
                                location_flag = 0;
                                plc300.Write("DB8.DBB0", (byte)7);//
                                plc300.Write("DB8.DBB1", (byte)1);//rz
                                listBoxMsg.Items.Add(logtime + "第三次成功：" + strMsg);
                                listBoxMsg.TopIndex = listBoxMsg.Items.Count - 1;
                            }
                            else
                            {
                                location_flag = 0;
                                plc300.Write("DB8.DBB0", (byte)7);//
                                plc300.Write("DB8.DBB1", (byte)0);//rz
                                listBoxMsg.Items.Add(logtime + "第三次失败：" + strMsg);
                                listBoxMsg.TopIndex = listBoxMsg.Items.Count - 1;
                            }
                            break;
                        case 4:
                            if (-100 < xpos && xpos < 100 && xpos != 0)
                            {
                                location_flag = 0;
                                plc300.Write("DB8.DBB0", (byte)8);//
                                plc300.Write("DB8.DBB1", (byte)1);//rz
                                listBoxMsg.Items.Add(logtime + "第四次成功：" + strMsg);
                                listBoxMsg.TopIndex = listBoxMsg.Items.Count - 1;
                            }
                            else
                            {
                                location_flag = 0;
                                plc300.Write("DB8.DBB0", (byte)8);//
                                plc300.Write("DB8.DBB1", (byte)0);//rz
                                listBoxMsg.Items.Add(logtime + "第四次失败：" + strMsg);
                                listBoxMsg.TopIndex = listBoxMsg.Items.Count - 1;
                            }
                            break;
                        default:
                            location_flag = 0;
                            plc300.Write("DB8.DBB1", (byte)0);//rz
                            listBoxMsg.Items.Add(logtime + "错误标识：" + strMsg);
                            listBoxMsg.TopIndex = listBoxMsg.Items.Count - 1;
                            break;
                    }
                }
                else
                {
                    if(location_flag!=0)
                    {
                        location_flag = 0;
                        plc300.Write("DB8.DBB0", (byte)0);//rz
                        plc300.Write("DB8.DBB1", (byte)0);//rz
                        listBoxMsg.Items.Add(DateTime.Now.ToString() + ":错误标识：" + strMsg);
                        listBoxMsg.TopIndex = listBoxMsg.Items.Count - 1;
                    }
                    else
                    {
                        location_flag = 0;
                        plc300.Write("DB8.DBB0", (byte)0);//rz
                        plc300.Write("DB8.DBB1", (byte)0);//rz
                        listBoxMsg.Items.Add(DateTime.Now.ToString() + ":测试标识：" + strMsg);
                        listBoxMsg.TopIndex = listBoxMsg.Items.Count - 1;
                    }
                    
                }

               
                    


            }
            catch (Exception ex)
            {
                location_flag = 0;
                strMsg = "getresult failed. ";
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
                        plc300.Write("DB9.DBB2", (byte)(int.Parse(x.strValue)));//plc罐号
                        plc300.Write("DB9.DBB1", (byte)1);//plc罐号写入成功
                        strMsg = DateTime.Now.ToString() + ":成功识别罐号:" + x.strValue;
                        listBoxMsg.Items.Add(strMsg);
                        listBoxMsg.TopIndex = listBoxMsg.Items.Count - 1;
                    }
                    else
                    {
                        plc300.Write("DB9.DBB2", (byte)0);//plc罐号
                        plc300.Write("DB9.DBB1", (byte)0);//plc罐号写入成功
                        strMsg = DateTime.Now.ToString() + ":失败识别罐号:" + x.strValue;
                        listBoxMsg.Items.Add(strMsg);
                        listBoxMsg.TopIndex = listBoxMsg.Items.Count - 1;
                    }
                    
                }
                else
                {
                    plc300.Write("DB9.DBB2", (byte)0);//plc罐号
                    plc300.Write("DB9.DBB1", (byte)0);//plc罐号写入成功
                    strMsg = DateTime.Now.ToString() + ":失败识别罐号，模块运行错误";
                    listBoxMsg.Items.Add(strMsg);
                    listBoxMsg.TopIndex = listBoxMsg.Items.Count - 1;
                }
                
            }
            catch(Exception ex)
            {
                strMsg = "罐号识别错误 ";
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

                Array a= baohao.ToArray();
          
                number = int.Parse(baohao[0]);
               
             
                

                textBox1_BH.Text = baohao[0];

                plc300.Write("DB9.DBB5", (byte)number);
                LogHelper.WriteLog(baohao[0]);
                strMsg = "包号识别成功:" + baohao[0];

                baohao.Clear();

                //************************************************************无穷多铁水包适用以上程序***************************************************************//

                //************************************************************有限个铁水包适用以下程序***************************************************************//




                ////if (null == process1_BH) return;
                ////process1_BH.Run();
                ////IntResultInfo status = process1_BH.GetIntOutputResult("statu");
                ////if (status.pIntValue[0] == 1)
                ////{
                ////StringResultInfo pxinfo = process1_BH.GetStringOutputResult("BH_str");                                           //输出string类型信息
                ////StringValueInfo x = pxinfo.astStringValue[0];

                ////if (x.strValue.Length == 1)
                ////{
                ////    bhsb = x.strValue;
                ////    baohao.Add(bhsb);                                                        //将识别出来的包号存入泛型
                ////}

                //int one = 0;
                //int two = 0;
                //int three = 0;
                //int four = 0;
                //int five = 0;
                //int six = 0;
                //int seven = 0;
                //int eight = 0;

                //foreach (var bh in baohao)
                //{
                //    switch (bh)
                //    {
                //        case "1":
                //            one++;
                //            break;
                //        case "2":
                //            two++;
                //            break;
                //        case "3":
                //            three++;
                //            break;
                //        case "4":
                //            four++;
                //            break;
                //        case "5":
                //            five++;
                //            break;
                //        case "6":
                //            six++;
                //            break;
                //        case "7":
                //            seven++;
                //            break;
                //        case "8":
                //            eight++;
                //            break;
                //    }
                //}
                /////将每个数字出现的次数依次存入泛型
                //numlist.Add(one);
                //numlist.Add(two);
                //numlist.Add(three);
                //numlist.Add(four);
                //numlist.Add(five);
                //numlist.Add(six);
                //numlist.Add(seven);
                //numlist.Add(eight);

                //for (int i = 0; i < numlist.Count - 1; i++)                                             //将每个数字出现的次数从大到小排序
                //{
                //    for (int j = 0; j < numlist.Count - 1 - i; j++)
                //    {
                //        if (Convert.ToInt32(numlist[j]) < Convert.ToInt32(numlist[j + 1]))
                //        {
                //            test = Convert.ToInt32(numlist[j + 1]);
                //            numlist[j + 1] = numlist[j];
                //            numlist[j] = test;
                //        }
                //    }
                //}

                //numlist.ToArray();
                //int num = numlist[0];                                                                    //取出出现次数最多的数的次数
                //numlist.Clear();

                //if (one == num)                                                                     //取出出现次数最多的数，并写入文本
                //{
                //    textBox1_BH.Text = "1";
                //    plc300.Write("DB9.DBB5", (byte)1);
                //    LogHelper.WriteLog("1");
                //    strMsg = "包号识别成功: 1 ";
                //}
                //if (two == num)
                //{
                //    textBox1_BH.Text = "2";
                //    plc300.Write("DB9.DBB5", (byte)2);
                //    LogHelper.WriteLog("2");
                //    strMsg = "包号识别成功: 2 ";
                //}
                //if (three == num)
                //{
                //    textBox1_BH.Text = "3";
                //    plc300.Write("DB9.DBB5", (byte)3);
                //    LogHelper.WriteLog("3");
                //    strMsg = "包号识别成功: 3 ";
                //}
                //if (four == num)
                //{
                //    textBox1_BH.Text = "4";
                //    plc300.Write("DB9.DBB5", (byte)4);
                //    LogHelper.WriteLog("4");
                //    strMsg = "包号识别成功: 4 ";
                //}
                //if (five == num)
                //{
                //    textBox1_BH.Text = "5";
                //    plc300.Write("DB9.DBB5", (byte)5);
                //    LogHelper.WriteLog("5");
                //    strMsg = "包号识别成功: 5 ";
                //}
                //if (six == num)
                //{
                //    textBox1_BH.Text = "6";
                //    plc300.Write("DB9.DBB5", (byte)6);
                //    LogHelper.WriteLog("6");
                //    strMsg = "包号识别成功: 6 ";
                //}
                //if (seven == num)
                //{
                //    textBox1_BH.Text = "7";
                //    plc300.Write("DB9.DBB5", (byte)7);
                //    LogHelper.WriteLog("7");
                //    strMsg = "包号识别成功: 7 ";
                //}
                //if (eight == num)
                //{
                //    textBox1_BH.Text = "8";
                //    plc300.Write("DB9.DBB5", (byte)8);
                //    LogHelper.WriteLog("8");
                //    strMsg = "包号识别成功: 8 ";
                //}


                //one = 0;                                                                                           //将出现次数和泛型清零
                //two = 0;
                //three = 0;
                //four = 0;
                //five = 0;
                //six = 0;
                //seven = 0;
                //eight = 0;

                //baohao.Clear();                                                                                     //泛型清零

                //strMsg = "包号识别成功 ";
                //plc300.Write("DB9.DBB5", (byte)(int.Parse(x.strValue)));//plc包号
                //plc300.Write("DB9.DBB4", (byte)1);//plc包号写入成功
                //textBox1_BH.Text = "";
                //txt_message.AppendText(strMsg + "\r\n");
                listBoxMsg.Items.Add(/*+nowDay.ToString("yyyy年MM月dd日 hh时mm分ss秒")*/ DateTime.Now.ToString() + strMsg);
                listBoxMsg.TopIndex = listBoxMsg.Items.Count - 1;
                ////}
            }
            catch (Exception ex)
            {
                strMsg = DateTime.Now.ToString() + "包号识别错误 ";
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
                        plc300.Write("DB8.DBB0", (byte)0);//
                        location_flag = location.VisionCode;
                        LocationExecuteOnce_Click(null, null);
                        
                    }
                    if (GH_num.VisionCodeA ==1)//罐号识别
                    {
                        plc300.Write("DB9.DBB0", (byte)5);//
                        Recognition_Click(null, null);
                        /////////////////////////////////////////////////////////////////////////////////////////////////////////
                        //try
                        //{
                        //    FloatResultInfo triangle_y_info = process1_GH.GetFloatOutputResult("triangle_Y");
                        //    triangle_Y = triangle_y_info.pFloatValue[0];
                        //    triangle_Y = (float)Math.Round(triangle_Y, 3);
                        //}
                        //catch (Exception e1)
                        //{

                        //}

                    }
                    //if (location.VisionCode >= 1 && location.VisionCode <= 4)//此条件需要更改为120度到来信号
                    //{
                    //    FloatResultInfo triangle_y_info_120 = process1_GH.GetFloatOutputResult("triangle_Y");
                    //    triangle_Y_120 = triangle_y_info_120.pFloatValue[0];
                    //    triangle_Y_120 = (float)Math.Round(triangle_Y_120, 3);

                    //    plc300.Write("DB9.DBB3", (float)((triangle_Y_120 - triangle_Y)*1.15));//向plc发送上升距离

                    //    triangle_Y_120 = 0;
                    //    triangle_Y = 0;

                    //}


                    if (GH_num.VisionCodeB== 1)//包号识别
                    {
                        plc300.Write("DB9.DBB3", (byte)0);//置位VisionCode
                        button1_BH_Click(null, null);
                        
                    }
                }
                else
                {
                    string strMsg = DateTime.Now.ToString() + ":连接PLC失败！";
                    listBoxMsg.Items.Add(strMsg);
                    listBoxMsg.TopIndex = listBoxMsg.Items.Count - 1;
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

        private void buttonShowHideVM_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonLoadSolution_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //LogHelper.loginfo.Info("test");
            //LogHelper.WriteLog("hellow");
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Process[] proc = Process.GetProcessesByName("Cognex.Designer.VisionPro.Runtime");
            for(int i=0;i<proc.Length;i++)
            {
                proc[i].Kill();
            }
            buttonCloseVM_Click(null,null);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string strMsg = null;
            //textBox1_BH.Text = "";
            if (null == process1_BH) return;
            process1_BH.Run();
            IntResultInfo status_dzf = process1_BH.GetIntOutputResult("status_dzf");
            IntResultInfo status_dw = process1_BH.GetIntOutputResult("status_dw");


            try
            {
                if (status_dzf.pIntValue[0] == 1 && status_dw.pIntValue[0] == 1)                                                       //判断定位状态和检测状态
                {
                    StringResultInfo pxinfo = process1_BH.GetStringOutputResult("BH_str01");                                           //输出string类型信息
                    StringValueInfo x = pxinfo.astStringValue[0];

                    single_bh_info = process1_BH.GetFloatOutputResult("dw_Y");
                    dw_Y = single_bh_info.pFloatValue[0];

                    //dw_Y = (float)Math.Round(ypos, 3);

                    if (x.strValue.Length == 1 && baohao_flag == 1 /*&& dw_Y <= 512*/)                                                        //判断字符长度和是否下落
                    {

                        dw_Y01 = dw_Y;
                        textBox1_BH.Text = "";
                        txt_bh_paizhao.Text = baohao_flag.ToString();
                        bhsb = x.strValue;
                        baohao.Add(bhsb);                                                                                              //将识别出来的包号存入泛型
                        plc300.Write("DB9.DBB4", (byte)1);
                        //plc300.Write("DB9.DBB5", (byte)(int.Parse(bhsb)));
                        baohao_flag++;
                        //i++;
                    }

                    else if (x.strValue.Length == 1 && baohao_flag == 2)
                    {

                        dw_Y02 = dw_Y;
                        txt_bh_paizhao.Text = baohao_flag.ToString();
                        bhsb = x.strValue;
                        baohao.Add(bhsb);                                                                                    //将识别出来的包号存入泛型
                        plc300.Write("DB9.DBB4", (byte)2);
                        //plc300.Write("DB9.DBB6", (byte)(int.Parse(bhsb)));
                        baohao_flag++;
                        //i++;
                    }
                    else if (x.strValue.Length == 1 && baohao_flag == 3)
                    {
                        if (dw_Y01 < dw_Y02)                                                                                  //说明铁水包在下落
                        {
                            plc300.Write("DB9.DBB4", (byte)3);
                            txt_bh_paizhao.Text = baohao_flag.ToString();
                            bhsb = x.strValue;
                            baohao.Add(bhsb);                                                                                 //将识别出来的包号存入泛型

                            baohao_flag = 1;                                                                                  //1！！！！
                            button1_BH_Click(null, null);
                            //plc300.Write("DB9.DBB7", (byte)(int.Parse(bhsb)));
                            //i++;
                        }
                        else if (dw_Y01 > dw_Y02)                                                                             //说明铁水包在上提
                        {
                            strMsg = DateTime.Now.ToString() + "铁水包上提！ ";
                            //txt_message.AppendText(strMsg + "\r\n");
                            listBoxMsg.Items.Add(strMsg);
                            listBoxMsg.TopIndex = listBoxMsg.Items.Count - 1;
                            LogHelper.loginfo.Info(strMsg);
                            txt_bh_paizhao.Text = "上";
                            plc300.Write("DB9.DBB5", (byte)0);
                            plc300.Write("DB9.DBB4", (byte)10);
                            baohao_flag = 1;
                            numlist.Clear();
                            baohao.Clear();
                        }


                    }
                    else if (baohao_flag == 6)
                    {
                        strMsg = DateTime.Now.ToString() + "跳过";
                        //txt_message.AppendText(strMsg + "\r\n");
                        //plc300.Write("DB9.DBB4", (byte)6);
                        //txt_bh_paizhao.Text = "";
                        listBoxMsg.Items.Add(strMsg);
                        LogHelper.loginfo.Info(strMsg);
                        baohao_flag = 1;
                    }
                    else if (baohao.Count == 1 && baohao_flag == 3 && dw_Y01 < dw_Y02)
                    {
                        button1_BH_Click(null, null);
                        strMsg = "只识别成功一次，直接输出此包号" + DateTime.Now.ToString();
                        LogHelper.loginfo.Info(strMsg);
                        txt_bh_paizhao.Text = "一";
                        plc300.Write("DB9.DBB4", (byte)11);
                        baohao_flag = 1;
                        //txt_message.AppendText(strMsg + "\r\n");
                        //listBoxMsg.Items.Add(strMsg);
                    }

                }

                if (status_dzf.pIntValue[0] != 1 || status_dw.pIntValue[0] != 1 /*&& dw_Y01 < dw_Y02*//* && baohao_flag == 3*/)                     //判断定位状态和检测状态
                {

                    if (dw_Y01 < dw_Y02 && baohao_flag == 3)
                    {
                        button1_BH_Click(null, null);
                        strMsg = DateTime.Now.ToString() + "第三次拍照识别状态不正确但铁水包在下降，直接输出此包号";
                        txt_bh_paizhao.Text = "三";
                        plc300.Write("DB9.DBB4", (byte)3);
                        baohao_flag = 1;
                        LogHelper.loginfo.Info(strMsg);
                        //txt_message.AppendText(strMsg + "\r\n");
                        //listBoxMsg.Items.Add(strMsg);
                    }
                    else if (dw_Y01 > dw_Y02 && baohao_flag == 3)
                    {
                        //baohao_flag = 4;
                        //button1_BH_Click(null, null);
                        strMsg = DateTime.Now.ToString() + "铁水包上提！";
                        txt_bh_paizhao.Text = "上";
                        plc300.Write("DB9.DBB5", (byte)0);
                        plc300.Write("DB9.DBB4", (byte)10);
                        baohao_flag = 1;
                        //txt_message.AppendText(strMsg + "\r\n");
                        listBoxMsg.Items.Add(strMsg);
                        LogHelper.loginfo.Info(strMsg);
                        numlist.Clear();
                        baohao.Clear();
                    }
                    else if (/*dw_Y01 > dw_Y02 &&*/ baohao_flag == 2)
                    {
                        //baohao_flag = 4;
                        //button1_BH_Click(null, null);
                        strMsg = DateTime.Now.ToString() + "第二次拍照不正确，不识别此包号！";
                        txt_bh_paizhao.Text = "two";
                        plc300.Write("DB9.DBB4", (byte)20);
                        plc300.Write("DB9.DBB5", (byte)0);
                        baohao_flag = 6;
                        //txt_message.AppendText(strMsg + "\r\n");
                        listBoxMsg.Items.Add(strMsg);
                        LogHelper.loginfo.Info(strMsg);
                        numlist.Clear();
                        baohao.Clear();
                    }
                    else if (baohao_flag == 6)
                    {
                        strMsg = DateTime.Now.ToString() + "跳过";
                        //txt_message.AppendText(strMsg + "\r\n");
                        //plc300.Write("DB9.DBB4", (byte)6);
                        //txt_bh_paizhao.Text = "";
                        listBoxMsg.Items.Add(strMsg);
                        LogHelper.loginfo.Info(strMsg);
                        baohao_flag = 1;
                    }
                    else if (baohao.Count == 1 && baohao_flag == 3 && dw_Y01 < dw_Y02)
                    {
                        button1_BH_Click(null, null);
                        strMsg = "只识别成功一次，直接输出此包号" + DateTime.Now.ToString();
                        txt_bh_paizhao.Text = "一";
                        plc300.Write("DB9.DBB4", (byte)11);
                        baohao_flag = 1;
                        //txt_message.AppendText(strMsg + "\r\n");
                        //listBoxMsg.Items.Add(strMsg);
                    }


                    //else if (baohao_flag == 1)
                    //{
                    //    baohao_flag = 5;
                    //    //button1_BH_Click(null, null);
                    //    //strMsg = "第一次拍照识别状态不正确，无法判断升降状态，直接跳过此包号！";
                    //    //txt_bh_paizhao.Text = "no";
                    //    plc300.Write("DB9.DBB4", (byte)250);
                    //    //txt_message.AppendText(strMsg + "\r\n");
                    //    //listBoxMsg.Items.Add(strMsg);
                    //}
                    //else if (baohao_flag == 2)
                    //{
                    //    baohao_flag = 6;
                    //    //button1_BH_Click(null, null);
                    //    strMsg = "第二次拍照识别状态不正确，无法判断升降状态，直接跳过此包号！";
                    //    txt_bh_paizhao.Text = "no";
                    //    plc300.Write("DB9.DBB4", (byte)100);
                    //    //txt_message.AppendText(strMsg + "\r\n");
                    //    listBoxMsg.Items.Add(strMsg);
                    //}
                    //else if (baohao_flag == 5)
                    //{
                    //    baohao_flag = 6;
                    //    strMsg = "第一次跳过！";
                    //    //txt_message.AppendText(strMsg + "\r\n");
                    //    txt_bh_paizhao.Text = "跳";
                    //    plc300.Write("DB9.DBB4", (byte)105);
                    //    //listBoxMsg.Items.Add(strMsg);
                    //}


                }



                //else if (status_dzf.pIntValue[0] != 1 || status_dw.pIntValue[0] != 1 && dw_Y01 > dw_Y02 && baohao_flag == 3)
                //{
                //    //baohao_flag = 4;
                //    //button1_BH_Click(null, null);
                //    strMsg = "识别状态不正确且铁水包正在上提，不识别此包号！";
                //    //txt_message.AppendText(strMsg + "\r\n");
                //    listBoxMsg.Items.Add(strMsg);
                //}
                //else if (status_dzf.pIntValue[0] != 1 || status_dw.pIntValue[0] != 1 && baohao_flag == 1)
                //{
                //    baohao_flag = 5;
                //    //button1_BH_Click(null, null);
                //    strMsg = "第一次拍照识别状态不正确，无法判断升降状态，直接跳过此包号！";
                //    //txt_message.AppendText(strMsg + "\r\n");
                //    listBoxMsg.Items.Add(strMsg);
                //}
                //else if (status_dzf.pIntValue[0] != 1 || status_dw.pIntValue[0] != 1 && baohao_flag == 2)
                //{
                //    baohao_flag = 6;
                //    //button1_BH_Click(null, null);
                //    strMsg = "第二次拍照识别状态不正确，无法判断升降状态，直接跳过此包号！";
                //    //txt_message.AppendText(strMsg + "\r\n");
                //    listBoxMsg.Items.Add(strMsg);
                //}
                //if (baohao_flag == 5)
                //{
                //    baohao_flag = 6;
                //    strMsg = "第一次跳过！";
                //    //txt_message.AppendText(strMsg + "\r\n");
                //    listBoxMsg.Items.Add(strMsg);
                //}
                //if (baohao_flag == 6)
                //{
                //    strMsg = "第二次跳过";
                //    //txt_message.AppendText(strMsg + "\r\n");
                //    listBoxMsg.Items.Add(strMsg);
                //    baohao_flag = 1;
                //}
                //i = 0;
            }


            catch (VmException ex)
            {
                strMsg = "包号识别错误 " + Convert.ToString(ex.errorCode, 16);
                //txt_message.AppendText(strMsg + "\r\n");
                listBoxMsg.Items.Add(strMsg);
                listBoxMsg.TopIndex = listBoxMsg.Items.Count - 1;
                LogHelper.WriteLog(strMsg, ex);
            }

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            logtest.LogHelper.loginfo.Info("test");
            LogHelper.WriteLog("hellow");
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
                    plc300.Write("DB8.DBB7", (byte)0);//置位报警信号
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
                VmSolution.Import(SolutionPath4, "18663481379");
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
            catch (Exception ex)
            {
                strMsg = "CloseSolution failed. ";
                //txt_message.AppendText(strMsg + "\r\n");
                listBoxMsg.Items.Add(strMsg);
                listBoxMsg.TopIndex = listBoxMsg.Items.Count - 1;
                return;
            }
        


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Process pr = new Process();//声明一个进程类对象
            pr.StartInfo.FileName = @"C:\Runtime\PANYUANHANBIAO\PANYUANCESHI.exe";
            pr.Start();
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
