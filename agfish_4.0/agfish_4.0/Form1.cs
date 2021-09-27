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

namespace AGFish
{
    public partial class Form1 : Form
    {

        //dbTaskHelper dbhelper;
        //public static OPCItem xintiao;
        //public static OPCItem VisionCode;
        //public static OPCItem VisionCodeA;
        //public static OPCItem FlatCode;
        //public static OPCItem FlatCodeA;
        //public static OPCItem Product_Type;
        //public static OPCItem Pro_X;
        //public static OPCItem Pro_Y;
        //public static OPCItem Pro_Z;
        //public static OPCItem Pro_RX;
        //public static OPCItem banauto;
        //public static OPCItem pianyi;
        ////记录plc数据
        //public static OPCItem Data_chge_flag;
        //public static OPCItem PLC_Data;
        //public static OPCItem Alarm;


        VmProcedure process1_DW, process1_GH, process1_BH;

        string SolutionPath4 = @"C:\Users\Administrator\Desktop\fishtest\fishtest\bin\Debug\agfish.sol";                                //别忘了改路径

        bool mSolutionIsLoad = false;

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
        }
      
     


        static string work_result = "0";
        static string pianyistr = "", banautostr = "";
        private void timer_savedata_Tick(object sender, EventArgs e)
        {
            //string priod_done = "",alarmtime="";
         
            //try
            //{
                
            //    object datachgeflag = AGFishOPCClient.ReadItem(Data_chge_flag);
            //    object alarmdata = AGFishOPCClient.ReadItem(Alarm);
            //    if(alarmdata.ToString()!="0")
            //    {
            //        alarmtime= DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss"));
            //    }
            //    if ((bool)datachgeflag )
            //    {
            //        AGFishOPCClient.WriteItem(0.ToString(), Data_chge_flag);
            //        object PLCdataflag = AGFishOPCClient.ReadItem(PLC_Data);
            //        priod_done = PLCdataflag.ToString();
            //        for (int i = 0; i < PLCflag.Length; i++)
            //        {
            //            if (PLCdataflag.ToString() == PLCflag[i].ToString())
            //            {
            //                str[i] = DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss"));
            //                break;
            //            }
            //        }
            //        if(PLCdataflag.ToString()=="13")
            //        {
            //            object readzpos= AGFishOPCClient.ReadItem(Pro_Z);
            //            zpos = (float)Math.Round( float.Parse(readzpos.ToString()),2);
            //        }
            //        if (PLCdataflag.ToString() == "57")
            //        {
            //            object pianyidata = AGFishOPCClient.ReadItem(pianyi);
            //            pianyistr = pianyidata.ToString();
            //            object banautodata = AGFishOPCClient.ReadItem(banauto);
            //            banautostr = banautodata.ToString();
            //        }
            //        if (PLCdataflag.ToString() == "24")
            //        {
            //            work_result = "1";
            //        }

            //    }
            //    if (priod_done == "27"|| alarmdata.ToString()!="0")
            //    {
            //        priod_done = "";
            //        AGFishOPCClient.WriteItem(0.ToString(), Alarm);
            //        string power_off = "1";
            //        string sqltext1 = string.Format("insert into aglog values('{0}',{1},{2},{3},{4},'{5}','{6}','{7}','{8}',", Car_num, xpos, ypos, zpos, rz, work_result, pianyistr,banautostr, power_off);
            //        string sqltext2 = "";
            //        for (int i = 0; i < str.Length; i++)
            //        {
            //            sqltext2 += "'" + str[i] + "'";
            //            sqltext2 += ',';
            //        }
            //        string sqltext3 = sqltext1 + sqltext2 + string.Format("'{0}','{1}')", alarmdata.ToString(), alarmtime);
            //        dbhelper.MultithreadExecuteNonQuery(sqltext3);
            //        str = new string[20];
            //        xpos = 0;
            //        ypos = 0;
            //        zpos = 0;
            //        rz = 0;
            //        Car_num = "";
            //        work_result = "0";
            //        pianyistr = "";
            //        banautostr = "";
            //    }

            //}
            //catch (Exception ex)
            //{
            //    LogHelper.WriteLog("数据记录失败", ex);
            //    str = new string[20];
            //    xpos = 0;
            //    ypos = 0;
            //    zpos = 0;
            //    rz = 0;
            //    Car_num = "";
            //    work_result = "0";
            //    pianyistr = "";
            //    banautostr = "";
            //}
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
                vmRenderControl1.ModuleSource = process1_DW;
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
