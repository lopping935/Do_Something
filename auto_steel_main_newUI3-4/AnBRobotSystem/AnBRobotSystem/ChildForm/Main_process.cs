﻿using System;
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
    public partial class Main_process : Sunny.UI.UIForm
    {
        Thread update_control;
        bool flag = false;
        public updatelistiew writelistview;
        dbTaskHelper uidb = new dbTaskHelper();
        public Main_process(updatelistiew up1)
        {

            InitializeComponent();
            main_tl_vmRenderControl1.ModuleSource = AnBRobotSystem.MdiParent.process_TL;
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

               
                //铁包位置
                if (AnBRobotSystem.MdiParent.PLCdata1.ZT_data.TB_pos == false)
                {
                   // main_tl_vmRenderControl1.Visible = true;
                    zt_Panel1.Enabled = true;
                    zt_ScrollingText1.Active = true;
                    zt_ScrollingText1.ForeColor = Color.Red;
                    tb_limit.Color = Color.LightGreen;
                    if ((int)AnBRobotSystem.MdiParent.PLCdata1.ZT_data.TB_weight > 90)
                    {
                        uiProcessBar1.Enabled = true;
                        uiProcessBar1.Value = (int)AnBRobotSystem.MdiParent.PLCdata1.ZT_data.TB_weight - 90;
                    }
                    else
                        uiProcessBar1.Value = 0;
                    TB_weight.Text = AnBRobotSystem.MdiParent.PLCdata1.ZT_data.TB_weight.ToString();
                    TB_hight.Text = AnBRobotSystem.MdiParent.PLCdata1.ZT_data.TB_LIQID.ToString();
                    ZT_tbnum.Text = AnBRobotSystem.MdiParent.PLCdata1.ZT_data.TB_num.ToString();

                    if (AnBRobotSystem.MdiParent.tl1.TL_station == "A")
                    {
                        //uiImageButton1.Visible = true;
                        //uiImageButton1.Enabled = !uiImageButton1.Enabled;
                    }
                    else
                    {
                       // uiImageButton1.Visible = false;

                    }

                    if (AnBRobotSystem.MdiParent.tl1.TL_station == "B")
                    {
                       // uiImageButton2.Visible = true;
                       // uiImageButton2.Enabled = !uiImageButton2.Enabled;
                    }
                    else
                    {
                       // uiImageButton2.Visible = false;
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
                    //uiImageButton1.Visible = false;
                    //uiImageButton2.Visible = false;
                    //main_tl_vmRenderControl1.Visible = false;

                }

                //罐车A
                if (AnBRobotSystem.MdiParent.PLCdata1.ZT_data.GB_A_carIn == true)
                {
                    train1_coming.Color = Color.LightGreen;
                    GBA_img.Enabled = true;
                    //读取实时数据库更新 罐重
                    //string A_nowweight = uidb.read_table_onefield("mid_weight", "RealTime_Car_Bag", "ID", "A");
                    GBA_now_weight.Text = AnBRobotSystem.MdiParent.PLCdata1.GB_A_remind_weight.ToString();
                    ui_gba_angle.Text = Math.Round((AnBRobotSystem.MdiParent.PLCdata1.ZT_data.GB_A_angle / 20), 2).ToString();
                }
                else
                {
                    train1_coming.Color = Color.Silver;
                    GBA_img.Enabled = false;
                    //读取实时数据库更新 罐重
                    GBA_now_weight.Text = "0";
                }
                GBA_num.Text = AnBRobotSystem.MdiParent.PLCdata1.ZT_data.GB_A_num.ToString();
                if (AnBRobotSystem.MdiParent.PLCdata1.ZT_data.GB_A_connect == true)
                {
                    GBA_getpow.Color = Color.LightGreen;
                }
                else
                    GBA_getpow.Color = Color.Silver;


                //罐车B
                if (AnBRobotSystem.MdiParent.PLCdata1.ZT_data.GB_B_carIn == true)
                {
                    train2_coming.Color = Color.LightGreen;
                    GBB_img.Enabled = true;

                    //读取实时数据库更新 罐重
                   // string B_nowweight = uidb.read_table_onefield("mid_weight", "RealTime_Car_Bag", "ID", "B");
                    GBB_now_weight.Text = AnBRobotSystem.MdiParent.PLCdata1.GB_B_remind_weight.ToString();
                    ui_gbb_angle.Text = Math.Round((AnBRobotSystem.MdiParent.PLCdata1.ZT_data.GB_B_angle/20), 2).ToString();
                }
                else
                {
                    train2_coming.Color = Color.Silver;
                    GBB_img.Enabled = false;
                    //读取实时数据库更新 罐重
                    GBB_now_weight.Text = "0";
                }
                GBB_num.Text = AnBRobotSystem.MdiParent.PLCdata1.ZT_data.GB_B_num.ToString();
                if (AnBRobotSystem.MdiParent.PLCdata1.ZT_data.GB_B_connect == true)
                {
                    GBB_getpow.Color = Color.LightGreen;
                }
                else
                    GBB_getpow.Color = Color.Silver;
                //实时折铁显示
                if (AnBRobotSystem.MdiParent.zt_state.Has_mission)
                {
                    zt_gb.Text = AnBRobotSystem.MdiParent.zt_state.GB_station;
                    if(AnBRobotSystem.MdiParent.zt_state.TB_BK_vision)
                        TBK_vision.Color = Color.LightGreen;
                    else
                        TBK_vision.Color = Color.Silver;

                    if (AnBRobotSystem.MdiParent.zt_state.GB_GK_vision)
                        GBK_vision.Color = Color.LightGreen;
                    else
                        GBK_vision.Color = Color.Silver;

                    if (AnBRobotSystem.MdiParent.tl1.Get_iron)
                        TL.Color = Color.LightGreen;
                    else
                        TL.Color = Color.Silver;
                    if(AnBRobotSystem.MdiParent.zt_state.GB_station=="A")
                    {
                        switch(AnBRobotSystem.MdiParent.zt_state.GB_speed)
                        {
                            //361703  低速倾翻
                            //361699高度倾翻
                            //361702 低速返回
                            //361698 高速返回
                            case 20:
                                A_go.Symbol = 361699;
                                A_go.SymbolColor = Color.Red;
                                break;
                            case 10:
                                A_go.Symbol = 361703;
                                A_go.SymbolColor = Color.Red;
                                break;
                            case -20:
                                A_back.Symbol = 361698;
                                A_back.SymbolColor = Color.Green;
                                break;
                            case -10:
                                A_back.Symbol = 361702;
                                A_back.SymbolColor = Color.Green;
                                break;
                            default:
                                A_go.SymbolColor = Color.Transparent;
                                A_back.SymbolColor = Color.Transparent;
                                break;
                        }
                    }
                    else if (AnBRobotSystem.MdiParent.zt_state.GB_station == "B")
                    {
                        switch (AnBRobotSystem.MdiParent.zt_state.GB_speed)
                        {
                            //361703  低速倾翻
                            //361699高度倾翻
                            //361702 低速返回
                            //361698 高速返回
                            case 20:
                                B_go.Symbol = 361699;
                                B_go.SymbolColor = Color.Red;
                                break;
                            case 10:
                                B_go.Symbol = 361703;
                                B_go.SymbolColor = Color.Red;
                                break;
                            case -20:
                                B_back.Symbol = 361698;
                                B_back.SymbolColor = Color.Green;
                                break;
                            case -10:
                                B_back.Symbol = 361702;
                                B_back.SymbolColor = Color.Green;
                                break;
                            default:
                                B_go.SymbolColor = Color.Transparent;
                                B_back.SymbolColor = Color.Transparent;
                                break;
                        }
                    }
                    else
                    {
                        A_go.SymbolColor = Color.Transparent;
                        A_back.SymbolColor = Color.Transparent;
                        B_go.SymbolColor = Color.Transparent;
                        B_back.SymbolColor = Color.Transparent;
                    }
                }
                else
                {
                    TBK_vision.Color = Color.Silver;
                    GBK_vision.Color = Color.Silver;
                    TL.Color = Color.Silver;
                    A_go.SymbolColor = Color.Transparent;
                    A_back.SymbolColor = Color.Transparent;
                    B_go.SymbolColor = Color.Transparent;
                    B_back.SymbolColor = Color.Transparent;
                }
                #endregion
            #region 测温位更新
            if (AnBRobotSystem.MdiParent.PLCdata1.ZT_data.TB_on_tempos == false)
            {
                cw_Panel2.Enabled = true;
                CW_tbnum.Text = AnBRobotSystem.MdiParent.PLCdata1.ZT_data.TB_num.ToString();

                temp_weight.Text = AnBRobotSystem.MdiParent.PLCdata1.ZT_data.TB_weight.ToString();

                if ((int)AnBRobotSystem.MdiParent.PLCdata1.ZT_data.TB_weight > 90)
                {
                    uiProcessBar2.Enabled = true;
                    uiProcessBar2.Value = (int)AnBRobotSystem.MdiParent.PLCdata1.ZT_data.TB_weight - 90;
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
            if (AnBRobotSystem.MdiParent.PLCdata1.ZT_data.TB_on_DZpos == false)
            {
                dz_Panel3.Enabled = true;
                DZ_tbnum.Text = AnBRobotSystem.MdiParent.PLCdata1.ZT_data.TB_num.ToString();
                dz_weight.Text = AnBRobotSystem.MdiParent.PLCdata1.ZT_data.TB_weight.ToString();

                if ((int)AnBRobotSystem.MdiParent.PLCdata1.ZT_data.TB_weight > 90)
                {
                    uiProcessBar3.Enabled = true;
                    uiProcessBar3.Value = (int)AnBRobotSystem.MdiParent.PLCdata1.ZT_data.TB_weight - 90;
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
        catch (Exception e)
        {
            LogHelper.WriteLog("界面更新错误！", e);
        }
        #endregion   
        }
        public void ui_ztdata()
        {
            if (AnBRobotSystem.MdiParent.zt_state.Has_mission)
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
                    if (AnBRobotSystem.MdiParent.zt_state.GB_station == "A")
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

            while (flag)
            {
                try
                { //更新前端界面
                    Thread.Sleep(500);
                    ui_updata();
                    textBox1_time_sp.Text = AnBRobotSystem.MdiParent.PLCdata1.TB_accelerate.ToString();
                    textBox1_plc.Text = AnBRobotSystem.MdiParent.PLCdata1.swmilltime1.ToString() + ":" + AnBRobotSystem.MdiParent.PLCdata1.runtime1+":"+ AnBRobotSystem.MdiParent.PLCdata1.taskw;
                }
                catch (Exception e)
                {
                    LogHelper.WriteLog("数据更新界面出差", e);
                }

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

        

        private void uiButton1_Click(object sender, EventArgs e)
        {
            AnBRobotSystem.MdiParent.PLCdata1.ZT_data.GB_A_carIn = true;
            AnBRobotSystem.MdiParent.PLCdata1.ZT_data.GB_posA = true;
            AnBRobotSystem.MdiParent.PLCdata1.ZT_data.GB_A_0_limt = true;
            AnBRobotSystem.MdiParent.PLCdata1.ZT_data.GB_A_120_limt = true;
            AnBRobotSystem.MdiParent.PLCdata1.ZT_data.GB_A_connect = true;
            AnBRobotSystem.MdiParent.PLCdata1.ZT_data.GB_A_num = 34;
            // AnBRobotSystem.MdiParent.PLCdata1.ZT_data.GB_A_Rspeed = Single.Parse(GB_R_speed.Text);
            AnBRobotSystem.MdiParent.PLCdata1.ZT_data.GB_A_angle = Single.Parse("501");

            AnBRobotSystem.MdiParent.PLCdata1.ZT_data.TB_num = 6;
            AnBRobotSystem.MdiParent.PLCdata1.ZT_data.TB_pos = false;
            AnBRobotSystem.MdiParent.PLCdata1.ZT_data.TB_weight = Single.Parse(TB_R_weight.Text);

        }

        private void uiButton2_Click(object sender, EventArgs e)
        {
            AnBRobotSystem.MdiParent.PLCdata1.ZT_data.GB_A_angle += 50;
            AnBRobotSystem.MdiParent.PLCdata1.ZT_data.TB_weight += 1;
            GB_R_angle.Text = AnBRobotSystem.MdiParent.PLCdata1.ZT_data.GB_A_angle.ToString();
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

        private void GBA_img_Click(object sender, EventArgs e)
        {
            AnBRobotSystem.MdiParent.PLCdata1.get_gb_num();
        }

        private void GBB_img_Click(object sender, EventArgs e)
        {

        }
    }

}
