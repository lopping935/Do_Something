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

namespace AnBRobotSystem.ChildForm
{
    public partial class Main_process : Sunny.UI.UIForm
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
                    }
                    else
                    {
                        Thread.Sleep(1000);
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
                if (A_chose.Checked)
                {
                    Program.GB_station = "A";
                    Program.GB_chose_flag = 1;
                    writelistview("罐选择", "选择A罐", "log");

                }
                else if (B_chose.Checked)
                {
                    Program.GB_station = "B";
                    Program.GB_chose_flag = 1;
                    writelistview("罐选择", "选择B罐", "log");
                }
                else
                {
                    MessageBox.Show("错误", "数据设置有失败！");
                }
                
            }
            catch(Exception e1)
            {
                LogHelper.WriteLog("数据更新界面出差", e1);
            }
            
        }
    }

}
