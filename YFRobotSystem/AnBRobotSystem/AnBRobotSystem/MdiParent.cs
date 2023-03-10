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
using log4net;
using System.Runtime.InteropServices;
[assembly: log4net.Config.DOMConfigurator(Watch = true)]
namespace AnBRobotSystem
{
    public partial class MdiParent : Form
    {
        string PLC_ser_appPath = AppDomain.CurrentDomain.BaseDirectory+"CoreAlgorithm.exe"; 
         Dictionary<string, ServiceControllerStatus> serverState;
        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll", EntryPoint = "ShowWindow")]
        private static extern int ShowWindow(IntPtr hwnd, int nCmdShow);
        [DllImport("user32.dll", EntryPoint = "MoveWindow")]
        public static extern bool MoveWindow(System.IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
        private static IniSqlConfigInfo inisql = new IniSqlConfigInfo(System.AppDomain.CurrentDomain.BaseDirectory + "Config.ini");
        DbHelper db = new DbHelper(inisql.GetConnectionString("SysSQL"));
        public MdiParent()
        {
            InitializeComponent();
            //string sql = "UPDATE SYSPARAMETER SET PARAMETER_VALUE=1 where PARAMETER_ID=11";
            //db.ExecuteNonQuery(db.GetSqlStringCommond(sql));
            serverState = new Dictionary<string, ServiceControllerStatus>();
        }
        private void MdiParent_Load(object sender, EventArgs e)
        {
            InitServerList();
            WinServerManage wsm;
            System.Threading.Thread th;
            Process[] pro = Process.GetProcesses();//获取已开启的所有进程
            bool CoreAlgorithmRun = false, CoreAlgorithmMESRun = false;
            for (int i = 0; i < pro.Length; i++)
            {
                //判断此进程是否是要查找的进程
                if (pro[i].ProcessName.ToString() == "CoreAlgorithm")//pro[i].ProcessName.ToString()== "CoreAlgorithm"|| 
                {
                    CoreAlgorithmRun = true;
                }
                if (pro[i].ProcessName.ToString() == "CoreAlgorithmMES")//pro[i].ProcessName.ToString()== "CoreAlgorithm"|| 
                {
                    CoreAlgorithmMESRun = true;
                }
            }
            //遍历所有查找到的进程

            //启动
            if (CoreAlgorithmRun == false)
            {
                wsm = new WinServerManage(new Hashtable(), @PLC_ser_appPath, "AnB_CoreAlgorithm");
                th = new System.Threading.Thread(new System.Threading.ThreadStart(wsm.StartService));
                th.Start();

            }
            FreshTimer.Start();
            timerLog.Start();
            this.WindowState = FormWindowState.Normal;
            OpenChildForm(GetFromHandle("数据配置"), "数据配置");
            label3.Text = System.DateTime.Now.ToString("yyyy年MM月dd日");// +"\n" + DataClass.MyMeans.dayofweek;
            label4.Text = DateTime.Now.ToString("HH:mm:ss").Trim();
            
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null /*|| treeView1.SelectedNode.Parent==null*/) return;
            Form form = null;
            if (treeView1.SelectedNode.Text != "")
            {
                if (treeView1.SelectedNode.Text == "视觉定位")
                {
                    string fexePath = @"D:\Program Files (x86)\Thunder Network\Thunder\Program\ThunderStart.exe";
                    Process p = new Process();
                    p.StartInfo.FileName = fexePath;
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                    p.Start();

                    while (p.MainWindowHandle.ToInt32() == 0)
                    {
                        System.Threading.Thread.Sleep(100);
                    }
                    SetParent(p.MainWindowHandle, this.Handle);
                    MoveWindow(p.MainWindowHandle, 153, 55, 1126, 699, true);
                }
                else
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
            }
            GC.Collect();
        }

        #region//公共函数
        private Form GetFromHandle(string FromName)
        {
            if (FromName == "历史记录查询")
                return new ModelSet();
            else if (FromName == "标签打印配置")
                return new FormPrint();
            else if (FromName == "标签信息记录")
                return new YCGBDesignResult();
            else
                return new FormPrint();
        }

        private void OpenChildForm(Form frm, string frmName)
        {
            if (frm == null) return;
            Form opcFrm = frm;
            opcFrm.WindowState = FormWindowState.Maximized;
            this.Text = "永锋焊标机器人信息系统 - [" + frmName + "]";
            opcFrm.MdiParent = this;
            opcFrm.Show();
        }

        void closechild()
        {
            if (this.MdiChildren.Count() == 1) { this.MdiChildren[0].Close(); };
        }
        #endregion

        #region//ServerList的事件
        private void InitServerList()
        {
            listView_Server.Items[0].Tag = @PLC_ser_appPath;
            //listView_Server.Items[1].Tag = appPath + "\\Server\\DataAcquisition.exe";
           // listView_Server.Items[1].Tag = "D:\\AnBRobotSetup\\Debug\\CoreAlgorithmMES.exe";

            //listView_Server.Items[3].Tag = appPath + "\\Server\\Ice.exe";
            WinServerManage wsm = new WinServerManage();
            if (wsm.ServiceIsExisted("AnB_CoreAlgorithm")) serverState.Add("AnB_CoreAlgorithm", wsm.getServiceState("AnB_CoreAlgorithm"));
            if (wsm.ServiceIsExisted("AnB_CoreAlgorithmMES")) serverState.Add("AnB_CoreAlgorithmMES", wsm.getServiceState("AnB_CoreAlgorithmMES"));
        }

        private void MenuStrip_Server_Star_Click(object sender, EventArgs e)
        {
            WinServerManage wsm = new WinServerManage(new Hashtable(),
                                                        listView_Server.SelectedItems[0].Tag.ToString(),
                                                        listView_Server.SelectedItems[0].SubItems[1].Text);
            System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ThreadStart(wsm.StartService));
            th.Start();
            ListViewItem lvi = new ListViewItem();
            lvi.Text = DateTime.Now.ToString();
            lvi.SubItems.Add("通知");
            lvi.SubItems.Add("手动启动");
            lvi.SubItems.Add(listView_Server.SelectedItems[0].SubItems[1].Text);
            this.listView1.Items.Add(lvi);
        }

        private void MenuStrip_Server_Stop_Click(object sender, EventArgs e)
        {
            WinServerManage wsm = new WinServerManage(new Hashtable(),
                                                        listView_Server.SelectedItems[0].Tag.ToString(),
                                                        listView_Server.SelectedItems[0].SubItems[1].Text);
            System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ThreadStart(wsm.StopService));
            th.Start();
            ListViewItem lvi = new ListViewItem();
            lvi.Text = DateTime.Now.ToString();
            lvi.SubItems.Add("通知");
            lvi.SubItems.Add("手动暂停");
            lvi.SubItems.Add(listView_Server.SelectedItems[0].SubItems[1].Text);
            this.listView1.Items.Add(lvi);
            if (listView_Server.SelectedItems[0].SubItems[1].Text == "CoreAlgorithm")
            {
                serverState["CoreAlgorithm"] = ServiceControllerStatus.StopPending;
            }
            if (listView_Server.SelectedItems[0].SubItems[1].Text == "DataAcquisition")
            {
                serverState["DataAcquisition"] = ServiceControllerStatus.StopPending;
            }
            if (listView_Server.SelectedItems[0].SubItems[1].Text == "FuzzyControl")
            {
                serverState["FuzzyControl"] = ServiceControllerStatus.StopPending;
            }
        }

        private void MenuStrip_Server_Fresh_Click(object sender, EventArgs e)
        {
            FreshTimer_Tick(null, null);
        }

        private void MenuStrip_Server_Init_Click(object sender, EventArgs e)
        {
            WinServerManage wsm = new WinServerManage(new Hashtable(),
                                                        listView_Server.SelectedItems[0].Tag.ToString(),
                                                        listView_Server.SelectedItems[0].SubItems[1].Text);
            System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ThreadStart(wsm.InstallService));
            th.Start();
            ListViewItem lvi = new ListViewItem();
            lvi.Text = DateTime.Now.ToString();
            lvi.SubItems.Add("通知");
            lvi.SubItems.Add("手动安装");
            lvi.SubItems.Add(listView_Server.SelectedItems[0].SubItems[1].Text);
            this.listView1.Items.Add(lvi);
            if (listView_Server.SelectedItems[0].SubItems[1].Text == "AnB_CoreAlgorithm")
            {
                serverState["AnB_CoreAlgorithm"] = ServiceControllerStatus.StartPending;
            }
            if (listView_Server.SelectedItems[0].SubItems[1].Text == "AnB_CoreAlgorithmMES")
            {
                serverState["AnB_CoreAlgorithmMES"] = ServiceControllerStatus.StartPending;
            }
            if (listView_Server.SelectedItems[0].SubItems[1].Text == "SPC_FuzzyControl")
            {
                serverState["SPC_FuzzyControl"] = ServiceControllerStatus.StartPending;
            }
        }

        private void MenuStrip_Server_Unini_Click(object sender, EventArgs e)
        {

            WinServerManage wsm = new WinServerManage(new Hashtable(),
                                                        listView_Server.SelectedItems[0].Tag.ToString(),
                                                        listView_Server.SelectedItems[0].SubItems[1].Text);
            System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ThreadStart(wsm.UnInstallService));
            th.Start();
            ListViewItem lvi = new ListViewItem();
            lvi.Text = DateTime.Now.ToString();
            lvi.SubItems.Add("通知");
            lvi.SubItems.Add("手动卸载");
            lvi.SubItems.Add(listView_Server.SelectedItems[0].SubItems[1].Text);
            this.listView1.Items.Add(lvi);
        }

        private void MenuStrip_Server_Updata_Click(object sender, EventArgs e)
        {
            FreshTimer_Tick(null, null);
        }

        private void MenuStrip_Server_ReStar_Click(object sender, EventArgs e)
        {
            WinServerManage wsm = new WinServerManage(new Hashtable(),
                                                        listView_Server.SelectedItems[0].Tag.ToString(),
                                                        listView_Server.SelectedItems[0].SubItems[1].Text);
            System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ThreadStart(wsm.ReStartService));
            th.Start();
            ListViewItem lvi = new ListViewItem();
            lvi.Text = DateTime.Now.ToString();
            lvi.SubItems.Add("通知");
            lvi.SubItems.Add("手动重启");
            lvi.SubItems.Add(listView_Server.SelectedItems[0].SubItems[1].Text);
            this.listView1.Items.Add(lvi);
        }
        #endregion

        #region//ServerList的刷新
        private void FreshTimer_Tick(object sender, EventArgs e)
        {
            WinServerManage wsm = new WinServerManage();
            for (int i = 0; i < 1; i++)
            {
                if (wsm.ServiceIsExisted(listView_Server.Items[i].SubItems[1].Text))
                {
                    listView_Server.Items[i].SubItems[2].Text =
                        wsm.getServiceState(listView_Server.Items[i].SubItems[1].Text).ToString();
                    listView_Server.Items[i].SubItems[3].Text = FileClass.GetFileVersion(listView_Server.Items[i].Tag.ToString());
                }
                else
                {
                    listView_Server.Items[i].SubItems[2].Text = "未安装";
                }
            }
            label3.Text = System.DateTime.Now.ToString("yyyy年MM月dd日");
            label4.Text = DateTime.Now.ToString("HH:mm:ss").Trim();
            //toolStripButton1.ForeColor = Color.Green;
            DbDataReader dr = null;
            double MES_Status = 0;
            string sql = "";
            #region 模式

            //sql = string.Format("select * from SYSPARAMETER  where PARAMETER_ID=14");
            //dr = db.ExecuteReader(db.GetSqlStringCommond(sql));
            //while (dr.Read())
            //{
            //    if (dr["PARAMETER_VALUE"] != DBNull.Value)
            //        MES_Status = Convert.ToDouble(dr["PARAMETER_VALUE"].ToString());
            //}
            //dr.Close();
            //if (MES_Status == 1)
            //{
            //    toolStripButton1.ForeColor = Color.Green;
            //    toolStripButton1.Text = "正常连接";
            //}                
            //else
            //{
            //    toolStripButton1.ForeColor = Color.Red;
            //    toolStripButton1.Text = "断开连接";
            //}

            //double Model_Status = 0;
            //sql = string.Format("select * from SYSPARAMETER  where PARAMETER_ID=15");
            //dr = db.ExecuteReader(db.GetSqlStringCommond(sql));
            //while (dr.Read())
            //{
            //    if (dr["PARAMETER_VALUE"] != DBNull.Value)
            //        Model_Status = Convert.ToDouble(dr["PARAMETER_VALUE"].ToString());
            //}
            //dr.Close();
            //if (Model_Status == 1)
            //{
            //    toolStripButton2.ForeColor = Color.Green;
            //    toolStripButton2.Text = "自动模式";
            //}
            //else
            //{
            //    toolStripButton2.ForeColor = Color.Red;
            //    toolStripButton2.Text = "手动模式";
            //}
            #endregion

            int Date_alarm = 0;
            sql = string.Format("select * from S_TFlag where ID=2");
            dr = db.ExecuteReader(db.GetSqlStringCommond(sql));
            while (dr.Read())
            {
                if (dr["Flag"] != DBNull.Value)
                    Date_alarm = Convert.ToInt32(dr["Flag"].ToString());
            }
            dr.Close();
            if (Date_alarm == 0)
            {
                sql = "update S_TFlag set Flag=-1 where ID=2";
                db.ExecuteNonQuery(db.GetSqlStringCommond(sql));
                MessageBox.Show("发生数据报警，请及时处理！","警报             ");                  
            }
            if (Date_alarm == 1)
            {
                sql = "update S_TFlag set Flag=-1 where ID=2";
                db.ExecuteNonQuery(db.GetSqlStringCommond(sql));
                MessageBox.Show("下抛数据有误，请及时处理！", "错误             ");
            }

        }
        #endregion
        //服务消息更改
        private void timerLog_Tick(object sender, EventArgs e)
        {
            WinServerManage wsm = new WinServerManage();
            if (wsm.ServiceIsExisted("AnB_CoreAlgorithm"))
            {
                if (wsm.getServiceState("AnB_CoreAlgorithm") != serverState["AnB_CoreAlgorithm"])
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = DateTime.Now.ToString();
                    lvi.SubItems.Add("通知");
                    lvi.SubItems.Add(wsm.getServiceState("AnB_CoreAlgorithm").ToString());
                    lvi.SubItems.Add("AnB_CoreAlgorithm服务更改成功！");
                    this.listView1.Items.Add(lvi);
                    serverState["AnB_CoreAlgorithm"] = wsm.getServiceState("AnB_CoreAlgorithm");
                    log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString() + "::" + MethodBase.GetCurrentMethod().ToString());
                    Log.addLog(log, LogType.INFO, "AnB_CoreAlgorithm服务更改为：" + serverState["AnB_CoreAlgorithm"].ToString());
                }
            }       
        }

        private void QuitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

        

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.timerScreen.Enabled = false;
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void indexToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
        }

        private void printPreviewToolStripButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            //UserInfor childForm = new UserInfor();
            //childForm.ShowDialog();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            //closechild();
            //PasswordConfig childForm = new PasswordConfig();
            //childForm.ShowDialog();
        }

        public void ThreadMethod()
        {
            System.Diagnostics.Process.Start(Application.ExecutablePath);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadMethod));
            t.Start();
            Dispose(true);
            this.Close();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Name == "tabPage3")
            {
                try
                {
                    string sql = "select [REC_CREATE_TIME] ,[CONTENT] from (select top 5000 [REC_CREATE_TIME],[CONTENT] from (select [REC_CREATE_TIME],[RECV_CONTENT] as [CONTENT]  from [MESRECVLOG]  union all select [REC_CREATE_TIME],[SEND_CONTENT] as [CONTENT]  from [MESSENDLOG]  union all select[REC_CREATE_TIME],[CONTENT] from [RECVLOG]  union all select [REC_CREATE_TIME],[CONTENT] from [SENDLOG])aa order by [REC_CREATE_TIME] desc )bb order by [REC_CREATE_TIME] asc";
                    DataTable dt = db.ExecuteDataTable(db.GetSqlStringCommond(sql));
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ListViewItem lvi = new ListViewItem();
                        lvi.Text =dt.Rows[i]["REC_CREATE_TIME"].ToString();
                        //lvi.SubItems.Add();
                        string ComContent = dt.Rows[i]["CONTENT"].ToString();
                        string ComContentHead = "";
                        if (ComContent.Length == 1)
                            ComContentHead = ComContent.Substring(0, 1);
                        else
                            ComContentHead = ComContent.Substring(0, 2);
                        switch (ComContentHead)
                            { 
                            case "Se":
                                ComContent = "发送喷码机信息 " + ComContent;
                                break;
                            default:
                                ComContent = "信息内容： " + ComContent;
                                break;
                        }
                        lvi.SubItems.Add(ComContent);
                        this.listView2.Items.Add(lvi);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show( ex.Message);
                }
            }
        }

        private void MdiParent_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void MdiParent_FormClosing(object sender, FormClosingEventArgs e)
        {
            //string sql = "UPDATE SYSPARAMETER SET PARAMETER_VALUE=3 where PARAMETER_ID=11";
            //db.ExecuteNonQuery(db.GetSqlStringCommond(sql));
            this.Dispose();
            Application.Exit();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
    }
}
