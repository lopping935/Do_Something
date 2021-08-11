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

namespace AnBRobotSystem
{
    public partial class MdiParent : Form
    {
        public static MdiParent form;
        Thread getPLC_data = new Thread(PLCdata.start_get);
        Thread getTieLiu_data = new Thread(Tieliu.get_Tieliu_data);
        Thread getGuankou_data = new Thread(Guankou.get_Guankou_data);
        bool mSolutionIsLoad = false;  //true 代表方案加载成功，false 代表方案关闭
        public static VmProcedure process1, process2, process3, process4;
        public static int a = 0;
        string SolutionPath = @"E:\ProjSetup\Auto_steel\vm\autosteelvm.sol";
        manage_steel m1 = new manage_steel();
        public MdiParent()
        {
            InitializeComponent();
            form = this;
            LogHelper.WriteLog("star program");
            getPLC_data.Start();
            LoadSolution();           
            ListBox.CheckForIllegalCrossThreadCalls = false;
        }
        private void MdiParent_Load(object sender, EventArgs e)
        {
            FreshTimer.Start();
            timerLog.Start();
            this.WindowState = FormWindowState.Normal;
            OpenChildForm(GetFromHandle("数据配置"), "数据配置");
            label3.Text = System.DateTime.Now.ToString("yyyy年MM月dd日");// +"\n" + DataClass.MyMeans.dayofweek;
            label4.Text = DateTime.Now.ToString("HH:mm:ss").Trim();            
        }
        private void LoadSolution() 
        {
            string strMsg = null;
            int nProgress = 0;           
            try
            {
                VmSolution.Import(SolutionPath, "");
                mSolutionIsLoad = true;
            }
            catch (VmException ex)

            {
                strMsg = "LoadSolution failed. Error Code: " + Convert.ToString(ex.errorCode, 16);
                LogHelper.WriteLog(strMsg, ex);
                return;
            }

            strMsg = "LoadSolution success";
            ListViewItem iteme1 = new ListViewItem(DateTime.Now.ToString());
           
            iteme1.SubItems.Add("视觉模型");
            iteme1.SubItems.Add(strMsg);
            listView1.Items.Add(iteme1);
      
            
        }
        public void mainlog(string model,string strmsg)
        {
            ListViewItem iteme1 = new ListViewItem(DateTime.Now.ToString());
            iteme1.SubItems.Add(model);
            iteme1.SubItems.Add(strmsg);
            listView1.Items.Add(iteme1);
        }
        private void buttonExecuteOnce_Click()
        {
            string strMsg = null;

            try
            {

                if (null == process1) return;

                Task.Run(() =>
                {
                    process1.Run();
                    process2.Run();
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
        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null)
                return;
            Form form = null;
            if (treeView1.SelectedNode.Text != "")
            {

                form = GetFromHandle(treeView1.SelectedNode.Text);
                if (form != null && form.IsMdiContainer == false)
                {
                    closechild();
                    OpenChildForm(form, treeView1.SelectedNode.Text);
                }
                else
                    form.ShowDialog();

            }
            GC.Collect();
        }
        private Form GetFromHandle(string FromName)
        {
            if (FromName == "历史记录查询")
                return new ModelSet();
            else if (FromName == "标签打印配置")
                return new FormPrint();
            else if (FromName == "标签信息记录")
                return new YCGBDesignResult();
            else if (FromName == "")
                return new Fauto_Form();
            else
                return new Fauto_Form();
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
            if (this.MdiChildren.Count() == 1) { this.MdiChildren[0].Close(); };
        }
        #endregion
        #region//时钟
        private void FreshTimer_Tick(object sender, EventArgs e)
        {
          
        }
      
        //服务消息更改
        private void timerLog_Tick(object sender, EventArgs e)
        {
            while(listView1.Items.Count > 10)
            
            {
                int count = listView1.Items.Count - 1;
                this.listView1.Items.RemoveAt(count);
            }
                    
        }

        private void button1_Click(object sender, EventArgs e)
        {
            buttonExecuteOnce_Click();
            m1.realtime_weight = 110;
            m1.get_ibag_weight();
            m1.get_fish_data();

        }
        #endregion

        #region//下方状态栏切换
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Name == "tabPage3")
            {
                try
                {
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show( ex.Message);
                }
            }
        }
        #endregion
        #region//主窗体开关程序
        private void MdiParent_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void MdiParent_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
            Application.Exit();
        }

        #endregion

       
    }
}
