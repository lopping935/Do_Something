using System;
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
using OpenCvSharp;
using SDK;
using ImageSourceModuleCs;
using AnBRobotSystem.Utlis;

namespace AnBRobotSystem
{
    public delegate void updatelistiew(string a, string b,string c);
    public delegate void main_void_fun();
    public partial class MdiParent :Sunny.UI.UIForm
    {
        public dbTaskHelper dbhlper;
        public static int baohao = 0;
        public dbTaskHelper uidbhelper=new dbTaskHelper();
        public static PLCdata PLCdata1=new PLCdata();
        public static MdiParent form;
        public static Tiebao tb1=new Tiebao();
        public static GuanKou gk1 = new GuanKou();
        public static TieLiu tl1 = new TieLiu();
        //  public static Auto_model autoprocess = new Auto_model();
        public static ZT_Date zt_state = new ZT_Date(), Azt_state = new ZT_Date();
        public static Auto_model atuomodel=new Auto_model();
        public static Auto_model atuomodelA = new Auto_model();
        bool mSolutionIsLoad = false;  //true 代表方案加载成功，false 代表方案关闭
        public static VmProcedure process_TB, process_GK, process_GKB, process_TL;
        public static ImageSourceModuleTool TL_img_sdk;
        public static GlobalVariableModuleTool GK_global;
        public static int a = 0;
        public VideoCapture camer_cap;
        string SolutionPath = @"C:\ProjSetup\Auto_steel\newvm\autosteelvm.sol";
        public static float Last_GB_A_angle = 0, Last_GB_B_angle = 0;
        public static bool updata_GBA_fulweight = false, updata_GBB_fulweight = false;
        public static string Aim_weight = "";

        

        // Thread plc_updata = new Thread(PLCdata.Read_PLC_data);
        // manage_steel m1 = new manage_steel();
        //Auto_model atuomodel = new Auto_model();

        public MdiParent()
        {
            try
            {

                InitializeComponent();
                my_log.LogFielPrefix = "text_logtest";
                my_log.LogPath = @"..\loginfo\";//@"D:\fgg";
                //侧边栏 sysmbol设计
                //uiNavMenu1.SetNodeSymbol(uiNavMenu1.Nodes[0], 61459);
                //uiNavMenu1.SetNodeSymbol(uiNavMenu1.Nodes[1], 61501);
                //uiNavMenu1.SetNodeSymbol(uiNavMenu1.Nodes[2], 61555);
                //uiNavMenu1.SetNodeSymbol(uiNavMenu1.Nodes[3], 61712);

                uiNavBar1.SetNodeSymbol(uiNavBar1.Nodes[0], 61459);
                uiNavBar1.SetNodeSymbol(uiNavBar1.Nodes[1], 61501);
                uiNavBar1.SetNodeSymbol(uiNavBar1.Nodes[2], 61555);
                uiNavBar1.SetNodeSymbol(uiNavBar1.Nodes[3], 61712);
                form = this;
                LogHelper.loginfo.Info("开始启动程序！");
                tb1.writelistview = mainlog;
                gk1.writelistview = mainlog;
                tl1.writelistview1 = mainlog;
                atuomodel.writelisview = mainlog;
                atuomodel.m1.writelisview = mainlog;
                atuomodelA.writelisview = mainlog;
                atuomodelA.m1.writelisview = mainlog;
                LoadSolution();
                LogHelper.loginfo.Info("vm启动成功！");
                PLCdata1.writelog = mainlog;
                PLCdata1.initplc();
                timerLog.Enabled = true;
                FreshTimer.Enabled = true;
                alarm_timer.Enabled = true;
                LogHelper.loginfo.Info("初始化完成，启动程序！");


            }
            catch(Exception e)
            {
                mainlog("主窗体", "主窗体初始化错误！", "err");
                LogHelper.WriteLog("主窗体",e);
                
            }
        }
        private void MdiParent_Load(object sender, EventArgs e)
        {
            FreshTimer.Start();
            timerLog.Start();
            //plc_updata.Start();
            this.WindowState = FormWindowState.Normal;
            OpenChildForm(GetFromHandle("数据配置"), "数据配置");
          //  uiLedDisplay1.Text = DateTime.Now.ToString("HH:mm:ss").Trim();            
        }
        private void LoadSolution() 
        {
            string strMsg = null;
            int nProgress = 0;           
            try
            {
                VmSolution.Import(SolutionPath,"");
                process_TB = (VmProcedure)VmSolution.Instance["流程1"];
                process_GK = (VmProcedure)VmSolution.Instance["流程2"];
                process_TL = (VmProcedure)VmSolution.Instance["流程3"];
                process_GKB = (VmProcedure)VmSolution.Instance["流程4"];
                TL_img_sdk = (ImageSourceModuleTool)VmSolution.Instance["流程3.图像源1"];
                GK_global = (GlobalVariableModuleTool)VmSolution.Instance["全局变量1"];
                mainlog("main", "视觉平台启动成功！", "log");
                mSolutionIsLoad = true;
                vmprocess_runtest();
            }
            catch (VmException ex)
            {
                strMsg = "LoadSolution failed. Error Code: " + Convert.ToString(ex.errorCode, 16);
                LogHelper.WriteLog(strMsg, ex);
                return;
            }


        }
        public void vmprocess_runtest()
        {
            if (mSolutionIsLoad)
            {
                gk1.GK_runtest();
                tl1.tl_runtest();
            }
        }
        public void mainlog(string model,string strmsg,string flag)
        {
            if(flag=="log")
            {
                ListViewItem iteme1 = new ListViewItem(strmsg);
                iteme1.SubItems.Add(DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"));
                iteme1.SubItems.Add(model);
                listView1.Invoke(new Action(() => { listView1.Items.Insert(0, iteme1); }));
            }
            else if(flag=="err")
            {
                ListViewItem iteme1 = new ListViewItem(strmsg);
                iteme1.SubItems.Add(DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"));
                iteme1.SubItems.Add(model);
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

        #region//窗体切换
       //标题栏  窗体切换代码
        private void uiNavBar1_NodeMouseClick(TreeNode node, int menuIndex, int pageIndex)
        {
            if (node == null)
                return;
            Form form = null;
            if (node.Text != "")
            {
                form = GetFromHandle(node.Text);
                if (form != null && form.Visible == false)
                {
                    OpenChildForm(form, node.Text);
                }
                else
                    form.Activate();
            }
        }
        //侧边栏 窗体切换代码
        /*private void uiNavMenu1_MenuItemClick(TreeNode node, Sunny.UI.NavMenuItem item, int pageIndex)
        {
            if (node == null)
                return;
            Form form = null;
            if (node.Text != "")
            {
                form = GetFromHandle(node.Text);
                if (form!=null && form.Visible==false)
                {
                    OpenChildForm(form, node.Text);
                }
                else
                    form.Activate();
            }
        }*/
        //获取窗体句柄
        private Form GetFromHandle(string FromName)
        {
            if (FromName == "实时数据")
            {
                foreach (Form childfrom in this.MdiChildren)
                {
                    if (childfrom.Name == "Real_data")
                        return childfrom;
                }
                return new Real_data();
            }   
            else if (FromName == "视觉图像")
            {
                foreach (Form childfrom in this.MdiChildren)
                {
                    if (childfrom.Name == "Fauto_Form")
                        return childfrom;
                }
                return new Fauto_Form();
            }
            else if (FromName == "历史记录")
            {
                foreach (Form childfrom in this.MdiChildren)
                {
                    if (childfrom.Name == "ModelSet")
                        return childfrom;
                }
                return new ModelSet();
            }  
            else if (FromName == "折铁流程")
            {
                foreach (Form childfrom in this.MdiChildren)
                {
                    if (childfrom.Name == "Main_process")
                        return childfrom;
                }
                return new Main_process(mainlog, A_back, B_back);
            }
            else
            {
                foreach (Form childfrom in this.MdiChildren)
                {
                    if (childfrom.Name == "Main_process")
                        return childfrom;
                }
                return new Main_process(mainlog, A_back, B_back);
            }        
        }

        private void OpenChildForm(Form frm, string frmName)
        {
            if (frm == null) return;
            Form opcFrm = frm;
            opcFrm.WindowState = FormWindowState.Maximized;
            this.Text = "自动折铁系统 - [" + frmName + "]";
           // opcFrm.TopLevel = false;
            opcFrm.MdiParent = this;
            opcFrm.Show();
        }

        void closechild()
        {
            foreach (Form  childfrom in this.MdiChildren)
            {
                childfrom.Close();
            }
 
        }
        #endregion
        #region//时钟
        int threadclocl = 0;
        private void FreshTimer_Tick(object sender, EventArgs e)
        {
           // PLCdata.Read_PLC_data();
            if (atuomodel.one_ZT != null)
                zt_state = atuomodel.one_ZT;
            else
                zt_state.Has_mission = false;

            if (atuomodelA.one_ZT != null)
                Azt_state = atuomodelA.one_ZT;
            else
                Azt_state.Has_mission = false;

            //plc网络通讯
            if (PLCdata1.connect)
            {
                PLC_connect_state.Color = Color.Green;
            }
            else
            {
                PLC_connect_state.Color = Color.Red;
            }
            //线程清除
            if (atuomodelA.ZT_thread_flag == 1000)
            {
                if (atuomodelA.full_thread != null)
                {
                    if (atuomodelA.full_thread.IsAlive)
                    {
                        if (threadclocl > 10)
                        {
                            threadclocl = 0;
                            atuomodelA.full_thread.Abort();
                            atuomodelA.full_thread.Join();
                        }
                        else
                        {
                            threadclocl++;
                        }

                    }
                    else
                    {
                        atuomodelA.full_thread = null;
                    }


                }
                if (atuomodelA.nfull_thread != null)
                {
                    if (atuomodelA.nfull_thread.IsAlive)
                    {
                        if (threadclocl > 10)
                        {
                            threadclocl = 0;
                            atuomodelA.nfull_thread.Abort();
                            atuomodelA.nfull_thread.Join();
                        }
                        else
                        {
                            threadclocl++;
                        }

                    }
                    else
                    {
                        atuomodelA.nfull_thread = null;
                    }
                }
            }
            if (atuomodel.ZT_thread_flag == 1000)
            {
                if (atuomodel.full_thread != null)
                {
                    if (atuomodel.full_thread.IsAlive)
                    {
                        if (threadclocl > 10)
                        {
                            threadclocl = 0;
                            atuomodel.full_thread.Abort();
                            atuomodel.full_thread.Join();
                        }
                        else
                        {
                            threadclocl++;
                        }

                    }
                    else
                    {
                        atuomodel.full_thread = null;
                    }


                }
                if (atuomodel.nfull_thread != null)
                {
                    if (atuomodel.nfull_thread.IsAlive)
                    {
                        if (threadclocl > 10)
                        {
                            threadclocl = 0;
                            atuomodel.nfull_thread.Abort();
                            atuomodel.nfull_thread.Join();
                        }
                        else
                        {
                            threadclocl++;
                        }

                    }
                    else
                    {
                        atuomodel.nfull_thread = null;
                    }
                }
            }
            //总重写入
            if (Last_GB_A_angle == 0 || Last_GB_B_angle == 0)
            {
                Last_GB_A_angle = PLCdata1.ZT_data.GB_A_angle;
                Last_GB_B_angle = PLCdata1.ZT_data.GB_B_angle;
            }
            if (Last_GB_A_angle - PLCdata1.ZT_data.GB_A_angle > 400)
            {
                Last_GB_A_angle = PLCdata1.ZT_data.GB_A_angle;
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("2号折铁角度发生错误，请检查！", "退出系统", messButton);
            }
            else
            {
                Last_GB_A_angle = PLCdata1.ZT_data.GB_A_angle;
            }
            if (Last_GB_B_angle - PLCdata1.ZT_data.GB_B_angle > 400)
            {
                Last_GB_B_angle = PLCdata1.ZT_data.GB_B_angle;
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("1号折铁角度发生错误，请检查！", "退出系统", messButton);
            }
            else
            {
                Last_GB_B_angle = PLCdata1.ZT_data.GB_B_angle;
            }
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
        #region  主窗体按键函数
        private void MdiParent_FormClosed(object sender, FormClosedEventArgs e)
        {
            //VmSolution.Instance.Dispose();
            //this.Dispose();
            //Application.Exit();

            //if ((atuomodelA.ZT_program_flag > 1 && atuomodelA.ZT_program_flag < 12)|| atuomodel.ZT_thread_flag != 1000)//折铁动作未完成！！！
            //{
            //    MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
            //    DialogResult dr = MessageBox.Show("折铁动作未完成，请勿退出！", "系统警报", messButton);

            //    return;
            //}


            //DialogResult result = MessageBox.Show("系统将退出，是否确认", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (result == DialogResult.Yes)
            //{
            //    VmSolution.Instance.Dispose();
            //    this.Dispose();
            //    Application.Exit();
            //    Close(); //退出程序

            //    MessageBox.Show("退出完成！", "系统消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }
        private void alarm_timer_Tick(object sender, EventArgs e)
        {
           // uiTextBox_weightspeed.Text = PLCdata1.TB_weight_speed.ToString();
            if (Program.GB_data_flag == 1000)
            {
                Program.GB_data_flag = 0;
                ShowErrorDialog("折铁过程发生错误！");
            }
        }

        private void hum_chose_GB_Click(object sender, EventArgs e)
        {
            

                try
                {
         
                if (PLCdata1.ZT_data.TB_weight < 80)//PLCdata1.ZT_data.TB_weight < 80
                {

                    reqweight_uiTextBox1.IntValue = 285;
                    ShowErrorDialog("无空包重时，只能设置折铁为最大值！");

                }
                else
                {
                    if (reqweight_uiTextBox1.IntValue > 300)
                    {
                        reqweight_uiTextBox1.IntValue = 0;
                        ShowErrorDialog("重量错误，重量超过总重限制！");
                       
                    }
                    else
                    {
                        Program.GB_chose_flag = 1;
                        mainlog("罐选择", "确认目标重量！", "log");
                    }

                }

            }
                catch (Exception e1)
                {
                    LogHelper.WriteLog("数据更新界面出差", e1);
                }

             
        }
        //重量设置限制
        private void reqweight_uiTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (PLCdata1.ZT_data.TB_weight < 80)//PLCdata1.ZT_data.TB_weight < 80
            {

                reqweight_uiTextBox1.IntValue = 285;
                ShowErrorDialog("无空包重时，只能设置折铁为最大值！");

            }
            else
            {
                if (reqweight_uiTextBox1.IntValue > 300)
                {
                    reqweight_uiTextBox1.IntValue = 0;
                    ShowErrorDialog("重量错误，重量超过总重限制！");
                }
                else
                {
                    //reqweight_uiTextBox1.IntValue = reqweight_uiTextBox1.IntValue + (int) PLCdata1.ZT_data.TB_weight;
                }

            }
        }
        //罐口隐藏测试按钮
        private void button2_Click(object sender, EventArgs e)
        {
            //if (A_chose.Checked)
            //{
            //    gk1.GK_station = "A";
            //    gk1.start_GK_state = true;
            //}
            //else
            //{
            //    gk1.GK_station = "B";
            //    gk1.start_GK_state = true;
            //}

        }
        //一键回罐程序
        public void A_back()
        {
            if (atuomodelA.ZT_thread_flag != 1000)
            {
                mainlog("折铁按键", "折铁程序正在运行，请勿重复点击！", "log");
                return;
            }
            Program.GB_station = "A";
            Program.GB_chose_flag = 4;
            atuomodelA.init_ZT_data();
            Auto_model.A_live = 1;
            atuomodelA.one_ZT.ZT_reqweight = -50;//设置本次需折铁量
            atuomodelA.chose_process();

        }
        public void B_back()
        {
            if (atuomodel.ZT_thread_flag != 1000)
            {
                mainlog("折铁按键", "折铁程序正在运行，请勿重复点击！", "log");
                return;
            }
            Program.GB_station = "B";
            Program.GB_chose_flag = 5;
            atuomodel.init_ZT_data();
            Auto_model.B_live = 1;
            atuomodel.one_ZT.ZT_reqweight = -50;//设置本次需折铁量
            //Program.program_flag = 0;
            atuomodel.chose_process();
        }

        

        private void set_weight_Click(object sender, EventArgs e)
        {
            if (text_A_train_full_weight.IntValue != 0)
            {
                
                string sqltext = string.Format("insert into [MES_Data] VALUES ('{0}','{1}','{2}','{3}','N')", "A", DateTime.Now.ToString(), text_A_train_full_weight.Text, "");
                uidbhelper.MultithreadExecuteNonQuery(sqltext);
                text_A_train_full_weight.IntValue = 0;
                //手动更新罐车总重标志
                updata_GBA_fulweight = true;

            }
            if (text_B_train_full_weight.IntValue != 0)
            {
               
                string sqltext = string.Format("insert into [MES_Data] VALUES ('{0}','{1}','{2}','{3}','N')", "B", DateTime.Now.ToString(), text_B_train_full_weight.Text, "");
                uidbhelper.MultithreadExecuteNonQuery(sqltext);
                text_B_train_full_weight.IntValue = 0;
                updata_GBB_fulweight = true;
            }

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        

        public LogManager my_log = new LogManager();

        private void uiSymbolButton2_Click(object sender, EventArgs e)
        {
            PLCdata1.reset_start("B");

            atuomodel.ZT_thread_flag = 1000;

            zt_state.Has_mission = false;

            Program.GB_chose_flag = 0;
        }

        private void uiLabel5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //tb1.TB_init_result();
            //my_log.WriteLog(LogFile.Trace, "log test");
            //LogHelper.WriteLog("fuck test");
            //LogHelper.WritecoreLog1("MYLOG", "fuck test");
            //string log = "-30" + "\",\"" + "test" + "\",\"" + "hellow" + "\",\"" + "testagin";
            LogHelper.loginfo.Info("test");
            //LogHelper.WriteLog("test");
        }

        //B位置折铁开始
        private void startB_ZT_Click(object sender, EventArgs e)
        {

            if (atuomodel.ZT_thread_flag != 1000)
            {
                mainlog("折铁按键", "折铁B程序正在运行，请勿重复点击！", "log");
                return;
            }
            else if (Program.GB_chose_flag != 1)//未选择折铁位
            {
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("请确认目标重量！", "系统警告", messButton);
                Program.GB_chose_flag = 0;
                return;
            }
            else if (atuomodelA.ZT_program_flag > 1 && atuomodelA.ZT_program_flag < 6)//另一个罐折铁动作未完成
            {
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("A折铁位未进入返回状态！", "系统警告", messButton);
                Program.GB_chose_flag = 0;
                return;
            }
            else if (TieLiu.tl_program_run_flag==false)
            {
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("视觉平台启动失败，请重启软件！", "退出系统", messButton);
                Program.GB_chose_flag = 0;
                return;
            }
            else
            {
                Program.GB_station = "B";
                atuomodel.init_ZT_data();
                Auto_model.B_live = 1;
                if (reqweight_uiTextBox1.IntValue == 280)
                {
                    atuomodel.one_ZT.ZT_reqweight = 280;//设置本次需折铁量
                    Aim_weight = atuomodel.one_ZT.ZT_reqweight.ToString();
                }
                else
                {
                    atuomodel.one_ZT.ZT_reqweight = reqweight_uiTextBox1.IntValue;
                    Aim_weight = atuomodel.one_ZT.ZT_reqweight.ToString();
                }
                LogHelper.loginfo.Info("启动B位折铁！");
                atuomodel.chose_process();
            }
            
        }
        //A位置折铁开始
        private void startA_ZT_Click(object sender, EventArgs e)
        {
            if (atuomodelA.ZT_thread_flag != 1000)
            {
                mainlog("折铁按键", "折铁A程序正在运行，请勿重复点击！", "log");
                return;
            }
            else if (Program.GB_chose_flag != 1)
            {
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("请确认目标重量！", "退出系统", messButton);
                Program.GB_chose_flag = 0;
                return;
            }
            else if (atuomodel.ZT_program_flag > 1 && atuomodel.ZT_program_flag < 6)
            {
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("B折铁位未进入返回状态！", "退出系统", messButton);
                Program.GB_chose_flag = 0;
                return;
            }
            else if (TieLiu.tl_program_run_flag == false)
            {
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("视觉平台启动失败，请重启软件！", "退出系统", messButton);
                Program.GB_chose_flag = 0;
                return;
            }
            else
            {
                Program.GB_station = "A";
                atuomodelA.init_ZT_data();
                Auto_model.A_live = 1;
                if (reqweight_uiTextBox1.IntValue == 280)
                {
                    atuomodelA.one_ZT.ZT_reqweight = 280;//设置本次需折铁量
                    Aim_weight = atuomodelA.one_ZT.ZT_reqweight.ToString();
                }
                else
                {
                    atuomodelA.one_ZT.ZT_reqweight = reqweight_uiTextBox1.IntValue;
                    Aim_weight = atuomodelA.one_ZT.ZT_reqweight.ToString();
                }
                atuomodelA.chose_process();
            }
           
        }
        //1退出折铁按钮
        
        //2退出折铁按钮
        private void A_stop_Click(object sender, EventArgs e)
        {
            PLCdata1.reset_start("A");

            atuomodelA.ZT_thread_flag = 1000;

            Azt_state.Has_mission = false;

            Program.GB_chose_flag = 0;
        }


        private void MdiParent_FormClosing(object sender, FormClosingEventArgs e)
        {
            VmSolution.Instance.Dispose();
            this.Dispose();
            Application.Exit();

            //if ((atuomodelA.ZT_program_flag > 1 && atuomodelA.ZT_program_flag < 12)|| atuomodel.ZT_thread_flag != 1000)//折铁动作未完成！！！
            //{
            //    MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
            //    DialogResult dr = MessageBox.Show("折铁动作未完成，请勿退出！", "系统警报", messButton);

            //    return;
            //}


            //DialogResult result = MessageBox.Show("系统将退出，是否确认", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (result == DialogResult.Yes)
            //{
            //    VmSolution.Instance.Dispose();
            //    this.Dispose();
            //    Application.Exit();
                

            //    MessageBox.Show("退出完成！", "系统消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    Close(); //退出程序
            //}
        }

        #endregion

    }
}
