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

namespace AnBRobotSystem.ChildForm
{
    public partial class Main_process : Sunny.UI.UIPage
    {
        Thread update_control;
        bool flag=false;
        public updatelistiew writelistview;
        
        public Main_process(updatelistiew up1)
        {
            InitializeComponent();
            writelistview = up1;
            update_control = new Thread(update);
            flag = true;
            update_control.IsBackground = true;
            update_control.Start();
         
        }
        public void update()
        {
            try
            {
                while (flag)
                {
                    ////静态模型状态更新

                    if (AnBRobotSystem.MdiParent.zt_state.Has_mission)
                    {
                        if(ZT_mision.OnColor==Color.Red)
                            ZT_mision.OnColor= Color.Green;
                        else
                            ZT_mision.OnColor = Color.Red;
                        Thread.Sleep(1000);
                        if (AnBRobotSystem.MdiParent.zt_state.TB_on_pos)
                            TB_uiLight.OnColor = Color.Green;
                        else
                            TB_uiLight.OnColor = Color.Red;
                        if (AnBRobotSystem.MdiParent.zt_state.TB_BK_vision)
                        {
                            TBK_uiLight.OnColor = Color.Green;

                            ZTL_uiLedLabel1.Invoke(new Action(() => { ZTL_uiLedLabel1.Text = AnBRobotSystem.MdiParent.zt_state.TB_need_weight.ToString(); }));
                        }
                        else
                        {
                            TBK_uiLight.OnColor = Color.Red;
                            ZTL_uiLedLabel1.Invoke(new Action(() => { ZTL_uiLedLabel1.Text = "0"; }));

                        }

                        if (AnBRobotSystem.MdiParent.zt_state.GB_on_pos)
                        {
                            GCDW_uiLight.OnColor = Color.Green;

                            GW_uiLedLabel.Invoke(new Action(() => { GW_uiLedLabel.Text = AnBRobotSystem.MdiParent.zt_state.GB_station; }));
                        }
                        else
                        {
                            GCDW_uiLight.OnColor = Color.Red;

                            GW_uiLedLabel.Invoke(new Action(() => { GW_uiLedLabel.Text = "N"; }));
                        }


                        if (AnBRobotSystem.MdiParent.zt_state.GB_GK_vision)
                        {
                            GBSJ_uiLight.OnColor = Color.Green;

                            MG_uiLedLabel.Invoke(new Action(() => { MG_uiLedLabel.Text = AnBRobotSystem.MdiParent.zt_state.GB_capacity; }));
                            GNTL_uiLedLabel.Invoke(new Action(() => { GNTL_uiLedLabel.Text = AnBRobotSystem.MdiParent.zt_state.GB_have_wight.ToString(); }));

                        }
                        else
                        {
                            GBSJ_uiLight.OnColor = Color.Red;

                            MG_uiLedLabel.Invoke(new Action(() => { MG_uiLedLabel.Text = "N"; }));
                            GNTL_uiLedLabel.Invoke(new Action(() => { GNTL_uiLedLabel.Text = "0"; }));

                        }
                        if (AnBRobotSystem.MdiParent.zt_state.GB_0_limt)
                            GB_0_pos.OnColor = Color.Green;
                        else
                            GB_0_pos.OnColor = Color.Red;
                        if (AnBRobotSystem.MdiParent.zt_state.GB_120_limt)
                            GB_limt_pos.OnColor = Color.Red;
                        else
                            GB_limt_pos.OnColor = Color.Green;
                        if (AnBRobotSystem.MdiParent.zt_state.GB_connect)
                            GB_get_pow.OnColor = Color.Green;
                        else
                            GB_get_pow.OnColor = Color.Red;
                        //动态模型状态更新
                        //罐口边缘
                        if (AnBRobotSystem.MdiParent.zt_state.GB_edge)
                            GK_edge.OnColor = Color.Green;
                        else
                            GK_edge.OnColor = Color.Red;
                        //铁流有无
                        if (AnBRobotSystem.MdiParent.zt_state.TL)
                            GB_TL.OnColor = Color.Green;
                        else
                            GB_TL.OnColor = Color.Red;
                        //铁流入包
                        if (AnBRobotSystem.MdiParent.zt_state.TL_in_TB)
                            TB_getiron.OnColor = Color.Green;
                        else
                            TB_getiron.OnColor = Color.Red;
                        //折铁返回
                        if (AnBRobotSystem.MdiParent.zt_state.ZT_back)
                            GB_back.OnColor = Color.Green;
                        else
                            GB_back.OnColor = Color.Red;
                        //折铁完成
                        if (AnBRobotSystem.MdiParent.zt_state.ZT_OK)
                            ZT_finsh.OnColor = Color.Green;
                        else
                            ZT_finsh.OnColor = Color.Red;
                    }
                    else
                    {
                        Thread.Sleep(1000);
                        GB_0_pos.OnColor = Color.Red;
                        GB_limt_pos.OnColor = Color.Red;
                        GB_get_pow.OnColor = Color.Red;
                        ZT_mision.OnColor = Color.Gray;
                        TB_uiLight.OnColor = Color.Red;
                        TBK_uiLight.OnColor = Color.Red;
                        ZTL_uiLedLabel1.Invoke(new Action(() => { ZTL_uiLedLabel1.Text = "0"; }));
                        GCDW_uiLight.OnColor = Color.Red;
                        GW_uiLedLabel.Invoke(new Action(() => { GW_uiLedLabel.Text = "N"; }));
                        GBSJ_uiLight.OnColor = Color.Red;
                        MG_uiLedLabel.Invoke(new Action(() => { MG_uiLedLabel.Text = "N"; }));
                        GNTL_uiLedLabel.Invoke(new Action(() => { GNTL_uiLedLabel.Text = "0"; }));
                        //writelistview("test", "test", "err");
                        //动态模型状态置位
                        GK_edge.OnColor = Color.Red;
                        GB_TL.OnColor = Color.Red;
                        TB_getiron.OnColor = Color.Red;
                        GB_back.OnColor = Color.Red;
                        ZT_finsh.OnColor = Color.Red;

                    }

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
                    ShowSuccessTip("A罐被选中");

                }
                else if (B_chose.Checked)
                {
                    Program.GB_station = "B";
                    Program.GB_chose_flag = 1;
                    writelistview("罐选择", "选择B罐", "log");
                    ShowInfoTip("B罐被选中");
                }
                else
                {
                    ShowErrorDialog("选罐失败，请重新选择！");
                }
                
            }
            catch(Exception e1)
            {
                LogHelper.WriteLog("数据更新界面出差", e1);
            }
            
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {

            PLCdata.ZT_data.GB_posA = true;
            PLCdata.ZT_data.GB_A_0_limt = true;
            PLCdata.ZT_data.GB_A_120_limt = false;
            PLCdata.ZT_data.GB_A_connect = true;
            PLCdata.ZT_data.GB_A_num = 34;
            PLCdata.ZT_data.GB_A_Rspeed = Single.Parse(GB_R_speed.Text);
            PLCdata.ZT_data.GB_A_angle =Single.Parse( GB_R_angle.Text);

            PLCdata.ZT_data.TB_num = 6;
            PLCdata.ZT_data.TB_pos = true;
            PLCdata.ZT_data.TB_weight = Single.Parse(TB_R_weight.Text);


        }

        private void uiButton2_Click(object sender, EventArgs e)
        {
            PLCdata.ZT_data.GB_A_angle += 100;
            GB_R_angle.Text = PLCdata.ZT_data.GB_A_angle.ToString();
        }
    }

}
