using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using HalconDotNet;
using System.Windows.Forms;
using Opc;
using OPCAutomation;
using logtest;
using SocketHelper;
using System.Configuration;
using System.Net.Sockets;

namespace bincour_sharp
{
    public partial class Form1 : Form
    {
       
        public static SocketServer PLC_Server = null;
        SocketData DD_Helper = new SocketData();
        public static Int16 open_flag = 0;
        public Form1()
        {
            InitializeComponent();
            //Thread thS = new Thread(do_SendMessage);
            //thS.Start();
            Thread halcon_calc = new Thread(halcondis);
            halcon_calc.Start();
            string localip = ConfigurationManager.ConnectionStrings["COMIP"].ConnectionString;
            string localportr = ConfigurationManager.ConnectionStrings["COMPORT"].ConnectionString;
            PLC_Server = new SocketServer(localip, int.Parse(localportr));
            PLC_Server.StarServer(DD_Helper.Recv);
            //halcondis();
        }       
        public void halcondis()
        {
            while(true)
            {
                Thread.Sleep(500);
                if(Program.MessageFlg==1)
                {
                    Program.MessageFlg = 0;
                    HObject img, ho_Rectangle;
                    HTuple hv_WidthL = new HTuple(), hv_HeightL = new HTuple();
                    HTuple hv_WindowHandle1 = new HTuple();
                    HOperatorSet.GenEmptyObj(out img);
                    HOperatorSet.GenEmptyObj(out ho_Rectangle);
                    HDevelopExport h1 = new HDevelopExport();
                    try
                    {
                        HTuple halcon_result = h1.RunHalcon();
                        if (halcon_result == 1)
                        {
                            result.Text = "OK";
                            open_flag = 1;
                            DD_Helper.do_SendMessage();
                        }
                        else
                        {
                            result.Invoke(new Action(() => { result.Text="NG"; }));
                            open_flag = 0;
                            DD_Helper.do_SendMessage();
                        }
                        img = h1.ho_ImageAffineTrans;
                        HOperatorSet.GetImageSize(img, out hv_WidthL, out hv_HeightL);
                        HOperatorSet.SetPart(this.hWindowControl1.HalconWindow, 0, 0, hv_HeightL - 1, hv_WidthL - 1);
                        HOperatorSet.DispObj(img, hWindowControl1.HalconWindow);
                        HOperatorSet.SetColor(hWindowControl1.HalconWindow, "red");
                        HOperatorSet.DispCross(hWindowControl1.HalconWindow, h1.hv_Row_Measure_01_0, h1.hv_Column_Measure_01_0, 50, 50);
                        HOperatorSet.GenRectangle1(out ho_Rectangle, h1.hv_Row_Measure_01_0 + 20, h1.hv_Column_Measure_01_0 + 20, h1.hv_Row_Measure_01_0 + 200, h1.hv_Column_Measure_01_0 + 350);
                        HOperatorSet.DispObj(ho_Rectangle, hWindowControl1.HalconWindow);
                        img.Dispose();
                        ho_Rectangle.Dispose();
                        hv_HeightL.Dispose();
                        hv_WidthL.Dispose();
                        hv_WindowHandle1.Dispose();
                    }
                    catch (Exception)
                    {
                        img.Dispose();
                        ho_Rectangle.Dispose();
                        hv_HeightL.Dispose();
                        hv_WidthL.Dispose();
                        hv_WindowHandle1.Dispose();
                    }
                }

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        #region opc comiunacation
        //opchelper door_detect = new opchelper();
        //public static OPCItem FlatCode;
        //public static OPCItem FlatCodeA;

        //void Initopc()
        //{
        //    try
        //    {
        //        if (!door_detect.ConnectRemoteServer("127.0.0.1", "kepware.kepserverex.v5"))
        //        {
        //            txt_message.AppendText("状态：连接OPC失败！\n");
        //            return;
        //        }
        //        if (!door_detect.CreateGroup())
        //        {
        //            txt_message.AppendText("状态：创建OPC组失败！\n");
        //            return;
        //        }
        //        if (opchelper.KepServer.ServerState == (int)OPCServerState.OPCRunning)
        //        {
        //            string log = "已连接到:" + opchelper.KepServer.ServerName;
        //            txt_message.AppendText(log + " \r\n");
        //            //service_connection.BackColor = Color.LightGreen;
        //            //service_connection.Enabled = false;
        //            LogHelper.WriteLog(log);
        //            door_detect.opc_connected = true;
        //        }
        //        else
        //        {
        //            //这里你可以根据返回的状态来自定义显示信息，请查看自动化接口API文档
        //            txt_message.AppendText("状态：" + opchelper.KepServer.ServerState.ToString() + "\n");
        //            //service_connection.BackColor = Color.Red;
        //            door_detect.opc_connected = false;
        //        }
        //        FlatCode = door_detect.AddItem("FlatCode");
        //        FlatCodeA = door_detect.AddItem("FlatCodeA");
        //    }
        //    catch (Exception err)
        //    {
        //        LogHelper.WriteLog("opcerr", err);
        //    }
        //}
        #endregion

    }
}
