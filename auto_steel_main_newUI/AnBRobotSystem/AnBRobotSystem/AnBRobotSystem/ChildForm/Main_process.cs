using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
//using hanbiao;
using SQLPublicClass;
using System.Data.Common;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using AnBRobotSystem;
using logtest;
using AnBRobotSystem.Core;
using AnBRobotSystem.Utlis;

namespace AnBRobotSystem.ChildForm
{
    public partial class Main_process :Sunny.UI.UIForm
    {
        Thread update_control;
        bool flag=false;
        public updatelistiew writelistview;
        dbTaskHelper uidb = new dbTaskHelper();
        public Main_process(updatelistiew up1)
        {

            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            writelistview = up1;
            update_control = new Thread(update);
            flag = true;
            update_control.IsBackground = true;
            update_control.Start();
         
        }
        //更新实时更新ui界面
        public void ui_updata()
        {
            #region 折铁位
            //铁包状态
           try
            {

            
            if (PLCdata.ZT_data.TB_pos == false)
            {
                zt_Panel1.Enabled = true;
                
                zt_ScrollingText1.Active = true;
                zt_ScrollingText1.ForeColor = Color.Red;
                tb_limit.Color = Color.LightGreen;
                if ((int)PLCdata.ZT_data.TB_weight > 90)
                {
                    uiProcessBar1.Enabled = true;
                    uiProcessBar1.Value = (int)PLCdata.ZT_data.TB_weight - 90;
                }
                else
                    uiProcessBar1.Value = 0;
                TB_weight.Text = PLCdata.ZT_data.TB_weight.ToString();
               
                ZT_tbnum.Text = PLCdata.ZT_data.TB_num.ToString();

                if (PLCdata.TB_weight_speed != 0 && PLCdata.ZT_data.GB_A_angle > 1200)
                {
                    uiImageButton1.Visible = true;
                    uiImageButton1.Enabled = !uiImageButton1.Enabled;
                }
                else
                {
                    uiImageButton1.Visible = false;

                }

                if (PLCdata.TB_weight_speed != 0 && PLCdata.ZT_data.GB_B_angle > 500)
                {
                    uiImageButton2.Visible = true;
                    uiImageButton2.Enabled = !uiImageButton2.Enabled;
                }
                else
                {
                    uiImageButton2.Visible = false;
                }
            }
            else
            {
               // zt_Panel1.Enabled = false;
                uiProcessBar1.Enabled = false;
                zt_ScrollingText1.Active = false;
                zt_ScrollingText1.ForeColor = Color.Silver;
                tb_limit.Color = Color.Silver;
                TB_weight.Text = "0";
                uiProcessBar1.Value = 0;
                ZT_tbnum.Text = "0";
            }
            
            //罐车A
            if (PLCdata.ZT_data.GB_A_carIn==true)
            {
                train1_coming.Color = Color.LightGreen;
                GBA_img.Enabled = true;
                //读取实时数据库更新 罐重
                string A_nowweight=uidb.read_table_onefield("mid_weight", "RealTime_Car_Bag", "ID", "A");
                GBA_now_weight.Text = A_nowweight;
                
            }
            else
            {
                train1_coming.Color = Color.Silver;
                GBA_img.Enabled = false;
                //读取实时数据库更新 罐重
                GBA_now_weight.Text = "0";
            }
            GBA_num.Text = PLCdata.ZT_data.GB_A_num.ToString();
            if (PLCdata.ZT_data.GB_A_connect == true)
            {
                GBA_getpow.Color = Color.LightGreen;
            }
            else
                GBA_getpow.Color = Color.Silver;


            //罐车B
            if (PLCdata.ZT_data.GB_B_carIn == true)
            {
                train2_coming.Color = Color.LightGreen;
                GBB_img.Enabled = true;
               
                //读取实时数据库更新 罐重
                string B_nowweight = uidb.read_table_onefield("mid_weight", "RealTime_Car_Bag", "ID", "B");
                GBB_now_weight.Text = B_nowweight;
            }
            else
            {
                train2_coming.Color = Color.Silver;
                GBB_img.Enabled = false;
                //读取实时数据库更新 罐重
                GBB_now_weight.Text = "0";
            }
            GBB_num.Text = PLCdata.ZT_data.GB_B_num.ToString();
            if (PLCdata.ZT_data.GB_B_connect == true)
            {
                GBB_getpow.Color = Color.LightGreen;
            }
            else
                GBB_getpow.Color = Color.Silver;
            #endregion
            #region 测温位更新
            if(PLCdata.ZT_data.TB_on_tempos==false)
            {
                cw_Panel2.Enabled = true;
                CW_tbnum.Text = PLCdata.ZT_data.TB_num.ToString();

                temp_weight.Text = PLCdata.ZT_data.TB_weight.ToString();
                
                if ((int)PLCdata.ZT_data.TB_weight > 90)
                {
                    uiProcessBar2.Enabled = true;
                    uiProcessBar2.Value = (int)PLCdata.ZT_data.TB_weight - 90;
                }
                else
                {
                    uiProcessBar2.Value = 0;
                }
                
                uiScrollingText2.Active = true;
                uiScrollingText2.ForeColor = Color.Red;
            }
            else
            {
                cw_Panel2.Enabled = false;
                CW_tbnum.Text = "0";
                uiProcessBar2.Enabled = false;
                uiProcessBar2.Value = 0;
                uiScrollingText2.Active = false;
                uiScrollingText2.ForeColor = Color.Silver;
            }
            #endregion
            #region 吊装位更新
            if (PLCdata.ZT_data.TB_on_DZpos == false)
            {
                dz_Panel3.Enabled = true;
                DZ_tbnum.Text = PLCdata.ZT_data.TB_num.ToString();
                dz_weight.Text = PLCdata.ZT_data.TB_weight.ToString();
                
                if ((int)PLCdata.ZT_data.TB_weight > 90)
                {
                    uiProcessBar3.Enabled = true;
                    uiProcessBar3.Value = (int)PLCdata.ZT_data.TB_weight - 90;
                }
                else
                {
                    uiProcessBar3.Value = 0;
                }
                uiScrollingText3.Active = true;
                uiScrollingText3.ForeColor = Color.Red;
            }
            else
            {
                dz_Panel3.Enabled = false;
                
                DZ_tbnum.Text = "0";
               
                uiProcessBar3.Value = 0;
                uiProcessBar3.Enabled = true;
                
                uiProcessBar3.Enabled = false;
                uiScrollingText3.Active = false;
                uiScrollingText3.ForeColor = Color.Silver;
            }
            }
        catch(Exception e)
            {
                LogHelper.WriteLog("界面更新错误！", e);
            }
            #endregion
        }
        public void ui_ztdata()
        {
            if(AnBRobotSystem.MdiParent.zt_state.Has_mission)
            {
                zt_gb.Text = AnBRobotSystem.MdiParent.zt_state.GB_station;
                if (AnBRobotSystem.MdiParent.zt_state.TB_BK_vision)
                    TBK_vision.Color = Color.LightGreen;
                else
                    TBK_vision.Color = Color.Silver;
                if (AnBRobotSystem.MdiParent.zt_state.GB_GK_vision)
                    GBK_vision.Color = Color.LightGreen;
                else
                    GBK_vision.Color = Color.Silver;
                if (AnBRobotSystem.MdiParent.zt_state.TL)
                {
                    TL.Color = Color.LightGreen;
                    if(AnBRobotSystem.MdiParent.zt_state.GB_station == "A")
                    {
                    }
                    else
                    {
                    }
                    
                }
                else
                {
                    TL.Color = Color.Silver;
                }
                    

            }
            else
            {

            }
        }
        public void update()
        {
            try
            {
                while (flag)
                {
                    //更新前端界面
                    ui_updata();

                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog("数据更新界面出差", e);
            }
            
            
        }

        private void Main_process_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            this.Close();
        }

   

        private void Main_process_FormClosing(object sender, FormClosingEventArgs e)
        {
            update_control.Abort();
            flag = false;
            this.Dispose();
            this.Close();
            
        }

        private void hum_chose_GB_Click(object sender, EventArgs e)
        {
            try
            {
                if (A_chose.Checked)//A_chose.Checked
                {
                    Program.GB_station = "A";
                    Program.GB_chose_flag = 1;
                    writelistview("罐选择", "选择A罐", "log");
                    //ShowSuccessTip("A罐被选中");

                }
                else if (B_chose.Checked)
                {
                    Program.GB_station = "B";
                    Program.GB_chose_flag = 1;
                    writelistview("罐选择", "选择B罐", "log");
                    //ShowInfoTip("B罐被选中");
                }
                else
                {
                   // ShowErrorDialog("选罐失败，请重新选择！");
                }
                
            }
            catch(Exception e1)
            {
                LogHelper.WriteLog("数据更新界面出差", e1);
            }
            
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            //PLCdata.ZT_data.GB_A_carIn = true;
            //PLCdata.ZT_data.GB_posA = true;
            //PLCdata.ZT_data.GB_A_0_limt = true;
            //PLCdata.ZT_data.GB_A_120_limt = false;
            //PLCdata.ZT_data.GB_A_connect = true;
            //PLCdata.ZT_data.GB_A_num = 34;
            //PLCdata.ZT_data.GB_A_Rspeed = Single.Parse(GB_R_speed.Text);
            //PLCdata.ZT_data.GB_A_angle =Single.Parse( GB_R_angle.Text);

            //PLCdata.ZT_data.TB_num = 6;
            //PLCdata.ZT_data.TB_pos = true;
            //PLCdata.ZT_data.TB_weight = Single.Parse(TB_R_weight.Text);

        }

        private void uiButton2_Click(object sender, EventArgs e)
        {
            PLCdata.ZT_data.GB_A_angle += 10;
            PLCdata.ZT_data.TB_weight += 50;
            GB_R_angle.Text = PLCdata.ZT_data.GB_A_angle.ToString();
        }

        private void uiGroupBox1_Click(object sender, EventArgs e)
        {

        }

        private void uiButton3_Click(object sender, EventArgs e)
        {
            //PLCdata.ZT_data.GB_A_carIn = false;
            //PLCdata.ZT_data.GB_posA = false;
            //PLCdata.ZT_data.GB_A_0_limt = false;
            //PLCdata.ZT_data.GB_A_120_limt = false;
            //PLCdata.ZT_data.GB_A_connect = false;
            //PLCdata.ZT_data.GB_A_num = 34;
            //uiImageButton1.Visible = true;
            //PLCdata.ZT_data.TB_num = 6;
            //PLCdata.ZT_data.TB_pos = false;


        }
    }

}
