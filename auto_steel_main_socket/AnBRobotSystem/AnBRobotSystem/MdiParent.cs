﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AnBRobotSystem.ChildForm;
using System.Collections;
using System.ServiceProcess;
using System.Diagnostics;
using SQLPublicClass;
using System.Reflection;
using logtest;
using System.Runtime.InteropServices;
using System.Threading;
using AnBRobotSystem.Core;
using VM.Core;
using VM.PlatformSDKCS;
using System.Threading.Tasks;
using GlobalVariableModuleCs;

namespace AnBRobotSystem
{
    public delegate void updatelistiew(string a, string b,string c);

    public partial class MdiParent :Sunny.UI.UIForm
    {
        public static MdiParent form;
        public static Tiebao tb1=new Tiebao();
        public static GuanKou gk1 = new GuanKou();
        public static TieLiu tl1 = new TieLiu();
        //  public static Auto_model autoprocess = new Auto_model();
        public static ZT_Date zt_state = new ZT_Date();
        public static Auto_model atuomodel;
        bool mSolutionIsLoad = false;  //true 代表方案加载成功，false 代表方案关闭
        public static VmProcedure process_TB, process_GK, process_TL, process4;
        public static GlobalVariableModuleTool GK_global;
        public static int a = 0;

        string SolutionPath = @"E:\ProjSetup\Auto_steel\newvm\autosteelvm.sol";
        // manage_steel m1 = new manage_steel();
        //Auto_model atuomodel = new Auto_model();

        public MdiParent()
        {
            
            InitializeComponent();
            uiNavMenu1.SetNodeSymbol(uiNavMenu1.Nodes[0], 61459);
            uiNavMenu1.SetNodeSymbol(uiNavMenu1.Nodes[1], 61501);
            uiNavMenu1.SetNodeSymbol(uiNavMenu1.Nodes[2], 61555);
            uiNavMenu1.SetNodeSymbol(uiNavMenu1.Nodes[3], 61712);
            // fucktest.ZT_data.GB_A_0_limt = true;
            //PLCdata.ZT_data.GB_A_0_limt = true;
            form = this;
            LogHelper.WriteLog("star program");
            tb1.writelistview = mainlog;
            gk1.writelistview = mainlog;
            //autoprocess.writelisview = mainlog;
            LoadSolution();           
            //ListBox.CheckForIllegalCrossThreadCalls = false;
          //  Tiebaodata = new Thread(trr1.get_Tiebao_data);
        }
        private void MdiParent_Load(object sender, EventArgs e)
        {
            FreshTimer.Start();
            timerLog.Start();
            this.WindowState = FormWindowState.Normal;
            OpenChildForm(GetFromHandle("数据配置"), "数据配置");
            uiLedDisplay1.Text = DateTime.Now.ToString("HH:mm:ss").Trim();            
        }
        private void LoadSolution() 
        {
            string strMsg = null;
            int nProgress = 0;           
            try
            {
                VmSolution.Import(SolutionPath, "");
                mSolutionIsLoad = true;
                process_TB = (VmProcedure)VmSolution.Instance["流程1"];
                process_GK = (VmProcedure)VmSolution.Instance["流程2"];
                process_TL = (VmProcedure)VmSolution.Instance["流程3"];
                GK_global = (GlobalVariableModuleTool)VmSolution.Instance["全局变量1"];
            }
            catch (VmException ex)

            {
                strMsg = "LoadSolution failed. Error Code: " + Convert.ToString(ex.errorCode, 16);
                LogHelper.WriteLog(strMsg, ex);
                return;
            }
            //mainlog("主窗体", "视觉模型加载成功！");


        }
        public void mainlog(string model,string strmsg,string flag)
        {
            if(flag=="log")
            {
                ListViewItem iteme1 = new ListViewItem(DateTime.Now.ToString());
                iteme1.SubItems.Add(model);
                iteme1.SubItems.Add(strmsg);
                listView1.Invoke(new Action(() => { listView1.Items.Insert(0, iteme1); }));
            }
            else if(flag=="err")
            {
                ListViewItem iteme1 = new ListViewItem(DateTime.Now.ToString());
                iteme1.SubItems.Add(model);
                iteme1.SubItems.Add(strmsg);
                listView4.Invoke(new Action(() => { listView4.Items.Insert(0, iteme1); }));
            }
            
        }
        public void errlog(string model, string strmsg)
        {
            ListViewItem iteme1 = new ListViewItem(DateTime.Now.ToString());
            iteme1.SubItems.Add(model);
            iteme1.SubItems.Add(strmsg);
            listView4.Invoke(new Action(() => { listView1.Items.Insert(0, iteme1); }));
        }
        private void buttonExecuteOnce_Click()
        {
            string strMsg = null;

            try
            {

                if (null == process_TB) return;

                Task.Run(() =>
                {
                    process_TB.Run();
                    IntResultInfo circle_yesno_info = process_TB.GetIntOutputResult("circle_yesno");
                    int circle_yesno = circle_yesno_info.pIntValue[0];
                    FloatResultInfo circle_X_info = process_TB.GetFloatOutputResult("circle_X");
                    float circle_X = circle_X_info.pFloatValue[0];
                    circle_X = (float)Math.Round(circle_X, 1);
                    FloatResultInfo circle_Y_info = process_TB.GetFloatOutputResult("circle_Y");
                    float circle_Y = circle_Y_info.pFloatValue[0];
                    circle_Y = (float)Math.Round(circle_Y, 1);
                    FloatResultInfo circle_R_info = process_TB.GetFloatOutputResult("circle_R");
                    float circle_R = circle_R_info.pFloatValue[0];
                    circle_R = (float)Math.Round(circle_R, 1);
                     strMsg = "圆查找结果: " + circle_yesno.ToString() + "个，圆心：" + circle_X.ToString() + "," + circle_Y.ToString() + ",半径：" + circle_R.ToString();
                    mainlog("罐口结果", strMsg, "log");
                    //process2.Run();
                });
            }
            catch (VmException ex)
            {
                strMsg = "SaveSolution failed. Error Code: " + Convert.ToString(ex.errorCode, 16);
             
                LogHelper.WriteLog(strMsg, ex);
                return;
            }

            strMsg = "ExecuteOnce success";

            LogHelper.WriteLog(strMsg);
        }

        
     
        #region//窗体切换
    
        private Form GetFromHandle(string FromName)
        {
            if (FromName == "实时数据")
                return new Real_data();
            else if (FromName == "视觉图像")
                return new Fauto_Form();
            else if (FromName == "历史记录")
                return new ModelSet();
            else if (FromName == "折铁流程")
            {
                return new Main_process(mainlog);
            }
            else
                return new Main_process(mainlog);
        }

        private void OpenChildForm(Form frm, string frmName)
        {
            if (frm == null) return;
            Form opcFrm = frm;
            opcFrm.WindowState = FormWindowState.Maximized;
            this.Text = "自动折铁系统 - [" + frmName + "]";
            opcFrm.MdiParent = this;
            opcFrm.Show();
        }

        void closechild()
        {
            if (this.MdiChildren.Count() == 1)
            {
                this.MdiChildren[0].Close();
            }
        }
        #endregion
        #region//时钟
      
  
        
        private void FreshTimer_Tick(object sender, EventArgs e)
        {
            //PLCdata.calc_weight_speed();
            if (atuomodel != null)
                zt_state = atuomodel.one_ZT;
            else
                zt_state.Has_mission = false;

            if (Program.model_flag == 1000)
            {
                atuomodel = null;
            }
            uiLedDisplay1.Text = DateTime.Now.ToString("HH:mm:ss").Trim();
        }
      
        //服务消息更改
        private void timerLog_Tick(object sender, EventArgs e)
        {
            int count = 0;
            if (listView1.Items.Count > 100)
            {
                count = listView1.Items.Count;
                while (listView1.Items.Count > 10)

                {
                     count -=1;
                    this.listView1.Items.RemoveAt(count);
                }
            }
        }


        
        #endregion

        #region//下方状态栏切换
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (tabControl1.SelectedTab.Name == "tabPage3")
            //{
            //    try
            //    {
                   
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show( ex.Message);
            //    }
            //}
        }
        #endregion
        #region//主窗体开关程序
        private void MdiParent_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void uiNavMenu1_MenuItemClick(TreeNode node, Sunny.UI.NavMenuItem item, int pageIndex)
        {
            if (node == null)
                return;
            Form form = null;
            if (node.Text != "")
            {
                form = GetFromHandle(node.Text);
                if (form != null && form.IsMdiContainer == false)
                {
                    closechild();
                    OpenChildForm(form, node.Text);
                }
                else
                    form.ShowDialog();

            }
            GC.Collect();
        }

        



        private void uiSymbolButton2_Click(object sender, EventArgs e)//开始折铁按钮
        {
            if (Program.model_flag != 1000)
            {
                mainlog("折铁按键", "折铁程序正在运行，请勿重复点击！", "log");
                return;
            }
            else if(Program.GB_chose_flag !=1)
            {
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定要自动选罐吗?", "退出系统", messButton);
                if (dr == DialogResult.OK)
                {
                    Program.GB_chose_flag = 2;
                    // this.Close();
                }
                else
                {
                    Program.GB_chose_flag = 3;
                    //this.Close();
                    return;
                }

            }
           // PLCdata.ZT_data.TB_pos = true;
          //  PLCdata.ZT_data.TB_weight = 80;
            atuomodel = new Auto_model();
            atuomodel.writelisview = mainlog;
            //zt_state.Has_mission = true;
            Program.program_flag = 0;
            atuomodel.chose_process();
            //zt_state = atuomodel.one_ZT;
        
    }

        private void uiSymbolButton1_Click_1(object sender, EventArgs e)//退出折铁按钮
        {
            Program.model_flag = 1000;
            zt_state.Has_mission = false;
            Program.GB_chose_flag = 0;
        }



        private void MdiParent_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
            Application.Exit();
        }

        #endregion
       
      

    }
}
