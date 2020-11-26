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
        public static Int16 distance1 = 1310;
        public static Int16 distance2 = 1183;
        public static Int16 distance3 = 1040;
        public static Int16 range1 = 18;
        public static Int16 range2 = 18;
        public static Int16 range3 = 18;
        public static Int16 region1 = 50;
        public static Int16 region2 = 50;
        public static Int16 region3 = 50;
        Thread halcon_calc;
        public HTuple hv_AcqHandle11 = new HTuple(), hv_AcqHandle2 = new HTuple(), HomMat2D1 = new HTuple();
        public Form1()
        {
            try
            {
                InitializeComponent();
                string localip = ConfigurationManager.ConnectionStrings["COMIP"].ConnectionString;
                string localportr = ConfigurationManager.ConnectionStrings["COMPORT"].ConnectionString;
                PLC_Server = new SocketServer(localip, int.Parse(localportr));
                PLC_Server.StarServer(DD_Helper.Recv);
                //halcondis();
               // getimg();
                txt_distance1.Text = distance1.ToString();
                txt_distance2.Text = distance2.ToString();
                txt_distance3.Text = distance3.ToString();
                txt_range1.Text = range1.ToString();
                txt_range2.Text = range2.ToString();
                txt_range3.Text = range3.ToString();
                txt_region1.Text = region1.ToString();
                txt_region2.Text = region2.ToString();
                txt_region3.Text = region3.ToString();
                //LogHelper.WriteLog("program inital err", "gfdg");
            }
            catch (Exception e)
            {
                LogHelper.WriteLog("program inital err", e);
            }
        }
        int subvalue = 0;
        int calcdistance(double angle)
        {
            int standlocation = 0;
            //0 2.5 3.8 
            if (0<=angle&& angle < 1.6)
            {
                standlocation =(int) (distance1 + ((0.8 - angle) / 0.2 * range1));
            }
            else if(1.6<=angle && angle < 3)
            {
                standlocation = (int)(distance2 + ((2.4 - angle) / 0.2 * range2));
            }
            else if(3<= angle && angle < 3.8)
            {
                standlocation = (int)(distance3 + ((3.4 - angle) / 0.2 * range3));
            }
            else
            {
                
            }

            return standlocation;
           
        }
        public void halwork()
        {
            
                Thread.Sleep(2000);
                if (Program.MessageFlg == 1)
                {
                    Program.Message_angle = 2.5;
                    //Program.MessageFlg = 0;
                    HObject img, ho_Rectangle;
                    HTuple hv_WidthL = new HTuple(), hv_HeightL = new HTuple();
                    HTuple hv_WindowHandle1 = new HTuple();
                    HOperatorSet.GenEmptyObj(out img);
                    HOperatorSet.GenEmptyObj(out ho_Rectangle);
                    HDevelopExport h1 = new HDevelopExport();
                    int standlocation = calcdistance(Program.Message_angle);
                    h1.InitHalcon(standlocation, region1);
                    HTuple hv_door_region = new HTuple();
                    try
                    {
                        HTuple halcon_result = h1.RunHalcon();
                        if (halcon_result == 1)
                        {
                            result.Invoke(new Action(() => { result.Text="OK"; }));
                            open_flag = 1;
                            txt_message.Invoke(new Action(() => { txt_message.AppendText("测量角度："+Program.Message_angle+",标准值："+ standlocation.ToString()+",容差值：" + region1.ToString()+"，结果：炉门全开" +"\r\n"); }));
                            StringBuilder sb = new StringBuilder();
                            sb.Append(Program.Message_angle);
                            sb.Append("\",\"");
                            sb.Append(standlocation);
                            sb.Append("\",\"");
                            sb.Append(region1);
                            sb.Append("\",\"");
                            sb.Append("成功");
                            LogHelper.WriteLog(sb.ToString());


                        DD_Helper.do_SendMessage();
                        }
                        else
                        {
                            result.Invoke(new Action(() => { result.Text="NG"; }));
                            open_flag = 0;
                            txt_message.Invoke(new Action(() => { txt_message.AppendText("测量角度：" + Program.Message_angle + ",标准值：" + standlocation.ToString() + ",容差值：" + region1.ToString() + "，结果：炉门未全开" + "\r\n"); }));
                        StringBuilder sb = new StringBuilder();
                        sb.Append(Program.Message_angle);
                        sb.Append("\",\"");
                        sb.Append(standlocation);
                        sb.Append("\",\"");
                        sb.Append(region1);
                        sb.Append("\",\"");
                        sb.Append("失败");
                        LogHelper.WriteLog(sb.ToString());
                        DD_Helper.do_SendMessage();
                        }
                        img = h1.ho_ImageAffineTrans;
                        HOperatorSet.GetImageSize(img, out hv_WidthL, out hv_HeightL);
                        HOperatorSet.SetPart(this.hWindowControl1.HalconWindow, 0, 0, hv_HeightL - 1, hv_WidthL - 1);
                        HOperatorSet.DispObj(img, hWindowControl1.HalconWindow);
                        HOperatorSet.SetColor(hWindowControl1.HalconWindow, "red");
                        HOperatorSet.DispCross(hWindowControl1.HalconWindow, h1.hv_Row_Measure_01_0, h1.hv_Column_Measure_01_0, 50, 50);
                        hv_door_region.Dispose();
                //HOperatorSet.GenRectangle1(out ho_Rectangle, h1.hv_Row_Measure_01_0 + 20, h1.hv_Column_Measure_01_0 + 20, h1.hv_Row_Measure_01_0 + 200, h1.hv_Column_Measure_01_0 + hv_door_region);
                //HOperatorSet.DispObj(ho_Rectangle, hWindowControl1.HalconWindow);

                        img.Dispose();
                  
                        ho_Rectangle.Dispose();
                        hv_HeightL.Dispose();
                        hv_WidthL.Dispose();
                        hv_WindowHandle1.Dispose();
                         GC.Collect();
                    }
                    catch (Exception e)
                    {
                        img.Dispose();
                        ho_Rectangle.Dispose();
                        hv_HeightL.Dispose();
                        hv_WidthL.Dispose();
                        hv_WindowHandle1.Dispose();
                    LogHelper.WriteLog("vision program err", e);
                    }
                }

            
        }

        
        public void getimg()
        {
            try
            {
               // HOperatorSet.ReadTuple("./fg.tup", out HomMat2D1);
                hv_AcqHandle11.Dispose();
                HOperatorSet.OpenFramegrabber("GigEVision2", 0, 0, 0, 0, 0, 0, "progressive",
                    -1, "default", -1, "false", "default", "c42f90fb62f3_Hikvision_MVCA02310GM",
                    0, -1, out hv_AcqHandle11);
                hv_AcqHandle2.Dispose();
                //HOperatorSet.CloseFramegrabber(hv_AcqHandle1);

            }
            catch (HalconException HDevExpDefaultException)
            {
                hv_AcqHandle11.Dispose();
                throw HDevExpDefaultException;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            halcon_calc.Abort();

            hv_AcqHandle11.Dispose();
        }
        private void bt_distance1_confirm_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("确定修改吗？","修改",MessageBoxButtons.YesNo,MessageBoxIcon.Question); 
            if(result == DialogResult.Yes)
            {
                distance1 =Int16.Parse(txt_distance1.Text);
             }
           else
            { return; }

        }
        private void bt_range1_confirm_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("确定修改吗？", "修改", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                range1 = Int16.Parse(txt_range1.Text);
            }
            else
            { return; }
        }

        private void bt_region1_confirm_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("确定修改吗？", "修改", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                region1 = Int16.Parse(txt_region1.Text);
            }
            else
            { return; }
        }

        private void bt_distance2_confirm_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("确定修改吗？", "修改", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                distance2 = Int16.Parse(txt_distance2.Text);
            }
            else
            { return; }
        }

        private void bt_range2_confirm_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("确定修改吗？", "修改", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                range2 = Int16.Parse(txt_range2.Text);
            }
            else
            { return; }
        }

        private void bt_region2_confirm_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("确定修改吗？", "修改", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                region2 = Int16.Parse(txt_region2.Text);
            }
            else
            { return; }
        }

        private void bt_distance3_confirm_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("确定修改吗？", "修改", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                distance3 = Int16.Parse(txt_distance3.Text);
            }
            else
            { return; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //LogHelper.WriteLog("sdfsdf");
            halcon_calc = new Thread(halwork);
            halcon_calc.Start();
        }


        private void bt_range3_confirm_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("确定修改吗？", "修改", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                range3 = Int16.Parse(txt_range3.Text);
            }
            else
            { return; }
        }

        private void bt_region3_confirm_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("确定修改吗？", "修改", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                region3 = Int16.Parse(txt_region3.Text);
            }
            else
            { return; }
        }
    }
}