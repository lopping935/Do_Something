namespace AnBRobotSystem
{
    partial class MdiParent
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("标签打印配置");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("历史记录查询");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("标签信息记录");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MdiParent));
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("系统通信服务", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "一级系统通信服务",
            "AnB_CoreAlgorithm",
            "",
            "",
            "与一级系统、喷枪通信，执行自动打印任务"}, -1);
            this.MenuStrip_Server = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuStrip_Server_Star = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip_Server_Stop = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuStrip_Server_Fresh = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip_Server_Unini = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip_Server_Init = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuStrip_Server_Updata = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuStrip_Server_ReStar = new System.Windows.Forms.ToolStripMenuItem();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.FreshTimer = new System.Windows.Forms.Timer(this.components);
            this.timerLog = new System.Windows.Forms.Timer(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.listView2 = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.listView_Server = new System.Windows.Forms.ListView();
            this.columnHeader_server = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_var = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_descr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MenuStrip_Server.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuStrip_Server
            // 
            this.MenuStrip_Server.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MenuStrip_Server.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuStrip_Server_Star,
            this.MenuStrip_Server_Stop,
            this.toolStripSeparator4,
            this.MenuStrip_Server_Fresh,
            this.ToolStripMenuItem,
            this.toolStripSeparator5,
            this.toolStripSeparator3,
            this.MenuStrip_Server_ReStar});
            this.MenuStrip_Server.Name = "MenuStrip_Server";
            this.MenuStrip_Server.Size = new System.Drawing.Size(139, 142);
            // 
            // MenuStrip_Server_Star
            // 
            this.MenuStrip_Server_Star.Name = "MenuStrip_Server_Star";
            this.MenuStrip_Server_Star.Size = new System.Drawing.Size(138, 24);
            this.MenuStrip_Server_Star.Text = "启动";
            this.MenuStrip_Server_Star.Click += new System.EventHandler(this.MenuStrip_Server_Star_Click);
            // 
            // MenuStrip_Server_Stop
            // 
            this.MenuStrip_Server_Stop.Name = "MenuStrip_Server_Stop";
            this.MenuStrip_Server_Stop.Size = new System.Drawing.Size(138, 24);
            this.MenuStrip_Server_Stop.Text = "停止";
            this.MenuStrip_Server_Stop.Click += new System.EventHandler(this.MenuStrip_Server_Stop_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(135, 6);
            // 
            // MenuStrip_Server_Fresh
            // 
            this.MenuStrip_Server_Fresh.Name = "MenuStrip_Server_Fresh";
            this.MenuStrip_Server_Fresh.Size = new System.Drawing.Size(138, 24);
            this.MenuStrip_Server_Fresh.Text = "刷新服务";
            this.MenuStrip_Server_Fresh.Click += new System.EventHandler(this.MenuStrip_Server_Fresh_Click);
            // 
            // ToolStripMenuItem
            // 
            this.ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuStrip_Server_Unini,
            this.MenuStrip_Server_Init,
            this.toolStripSeparator6,
            this.MenuStrip_Server_Updata});
            this.ToolStripMenuItem.Name = "ToolStripMenuItem";
            this.ToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.ToolStripMenuItem.Text = "更新服务";
            this.ToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // MenuStrip_Server_Unini
            // 
            this.MenuStrip_Server_Unini.Name = "MenuStrip_Server_Unini";
            this.MenuStrip_Server_Unini.Size = new System.Drawing.Size(114, 26);
            this.MenuStrip_Server_Unini.Text = "卸载";
            this.MenuStrip_Server_Unini.Click += new System.EventHandler(this.MenuStrip_Server_Unini_Click);
            // 
            // MenuStrip_Server_Init
            // 
            this.MenuStrip_Server_Init.Name = "MenuStrip_Server_Init";
            this.MenuStrip_Server_Init.Size = new System.Drawing.Size(114, 26);
            this.MenuStrip_Server_Init.Text = "安装";
            this.MenuStrip_Server_Init.Click += new System.EventHandler(this.MenuStrip_Server_Init_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(111, 6);
            // 
            // MenuStrip_Server_Updata
            // 
            this.MenuStrip_Server_Updata.Name = "MenuStrip_Server_Updata";
            this.MenuStrip_Server_Updata.Size = new System.Drawing.Size(114, 26);
            this.MenuStrip_Server_Updata.Text = "更新";
            this.MenuStrip_Server_Updata.Click += new System.EventHandler(this.MenuStrip_Server_Updata_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(135, 6);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(135, 6);
            // 
            // MenuStrip_Server_ReStar
            // 
            this.MenuStrip_Server_ReStar.Name = "MenuStrip_Server_ReStar";
            this.MenuStrip_Server_ReStar.Size = new System.Drawing.Size(138, 24);
            this.MenuStrip_Server_ReStar.Text = "重新启动";
            this.MenuStrip_Server_ReStar.Click += new System.EventHandler(this.MenuStrip_Server_ReStar_Click);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 868);
            this.splitter1.Margin = new System.Windows.Forms.Padding(4);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1872, 4);
            this.splitter1.TabIndex = 8;
            this.splitter1.TabStop = false;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeView1.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeView1.ItemHeight = 50;
            this.treeView1.Location = new System.Drawing.Point(0, 71);
            this.treeView1.Margin = new System.Windows.Forms.Padding(4);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "节点0";
            treeNode1.Text = "标签打印配置";
            treeNode2.Name = "节点3";
            treeNode2.Text = "历史记录查询";
            treeNode3.Name = "节点0";
            treeNode3.Text = "标签信息记录";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            this.treeView1.Size = new System.Drawing.Size(189, 797);
            this.treeView1.TabIndex = 11;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.DoubleClick += new System.EventHandler(this.treeView1_DoubleClick);
            // 
            // splitter2
            // 
            this.splitter2.Location = new System.Drawing.Point(189, 71);
            this.splitter2.Margin = new System.Windows.Forms.Padding(4);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(4, 797);
            this.splitter2.TabIndex = 12;
            this.splitter2.TabStop = false;
            // 
            // FreshTimer
            // 
            this.FreshTimer.Enabled = true;
            this.FreshTimer.Interval = 1000;
            this.FreshTimer.Tick += new System.EventHandler(this.FreshTimer_Tick);
            // 
            // timerLog
            // 
            this.timerLog.Enabled = true;
            this.timerLog.Interval = 1000;
            this.timerLog.Tick += new System.EventHandler(this.timerLog_Tick);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.Window;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(766, 37);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(161, 31);
            this.label4.TabIndex = 20;
            this.label4.Text = "label4";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(546, 71);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.Window;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(766, 6);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(161, 31);
            this.label3.TabIndex = 19;
            this.label3.Text = "label3";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.listView2);
            this.tabPage3.Location = new System.Drawing.Point(4, 28);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1864, 147);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "一级通信内容";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // listView2
            // 
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6});
            this.listView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView2.FullRowSelect = true;
            this.listView2.HideSelection = false;
            this.listView2.Location = new System.Drawing.Point(0, 0);
            this.listView2.Margin = new System.Windows.Forms.Padding(4);
            this.listView2.MultiSelect = false;
            this.listView2.Name = "listView2";
            this.listView2.ShowItemToolTips = true;
            this.listView2.Size = new System.Drawing.Size(1864, 147);
            this.listView2.TabIndex = 2;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "日期时间";
            this.columnHeader5.Width = 200;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "通信内容";
            this.columnHeader6.Width = 1200;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.listView_Server);
            this.tabPage2.Location = new System.Drawing.Point(4, 28);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage2.Size = new System.Drawing.Size(1864, 147);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "服务状态";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // listView_Server
            // 
            this.listView_Server.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_server,
            this.columnHeader_name,
            this.columnHeader_status,
            this.columnHeader_var,
            this.columnHeader_descr});
            this.listView_Server.ContextMenuStrip = this.MenuStrip_Server;
            this.listView_Server.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_Server.FullRowSelect = true;
            listViewGroup1.Header = "系统通信服务";
            listViewGroup1.Name = "listViewGroup1";
            this.listView_Server.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1});
            this.listView_Server.HideSelection = false;
            listViewItem1.Group = listViewGroup1;
            listViewItem1.Tag = "";
            this.listView_Server.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.listView_Server.Location = new System.Drawing.Point(4, 4);
            this.listView_Server.Margin = new System.Windows.Forms.Padding(4);
            this.listView_Server.MultiSelect = false;
            this.listView_Server.Name = "listView_Server";
            this.listView_Server.ShowItemToolTips = true;
            this.listView_Server.Size = new System.Drawing.Size(1856, 139);
            this.listView_Server.TabIndex = 2;
            this.listView_Server.UseCompatibleStateImageBehavior = false;
            this.listView_Server.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader_server
            // 
            this.columnHeader_server.Text = "服务";
            this.columnHeader_server.Width = 104;
            // 
            // columnHeader_name
            // 
            this.columnHeader_name.Text = "服务名称";
            this.columnHeader_name.Width = 126;
            // 
            // columnHeader_status
            // 
            this.columnHeader_status.Text = "状态";
            this.columnHeader_status.Width = 117;
            // 
            // columnHeader_var
            // 
            this.columnHeader_var.Text = "版本";
            this.columnHeader_var.Width = 156;
            // 
            // columnHeader_descr
            // 
            this.columnHeader_descr.Text = "描述";
            this.columnHeader_descr.Width = 434;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.listView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 28);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(1864, 147);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "日志信息";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(4, 4);
            this.listView1.Margin = new System.Windows.Forms.Padding(4);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.ShowItemToolTips = true;
            this.listView1.Size = new System.Drawing.Size(1856, 139);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "日期";
            this.columnHeader1.Width = 200;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "等级";
            this.columnHeader2.Width = 120;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "类型";
            this.columnHeader3.Width = 120;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "描述";
            this.columnHeader4.Width = 317;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControl1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(0, 872);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1872, 179);
            this.tabControl1.TabIndex = 7;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(29, 68);
            this.toolStripLabel3.Text = "     ";
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1872, 71);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1872, 28);
            this.menuStrip1.TabIndex = 22;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            // 
            // MdiParent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1872, 1051);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MdiParent";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MdiParent_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MdiParent_FormClosed);
            this.Load += new System.EventHandler(this.MdiParent_Load);
            this.MenuStrip_Server.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ContextMenuStrip MenuStrip_Server;
        private System.Windows.Forms.ToolStripMenuItem MenuStrip_Server_Star;
        private System.Windows.Forms.ToolStripMenuItem MenuStrip_Server_Stop;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem MenuStrip_Server_Fresh;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuStrip_Server_Unini;
        private System.Windows.Forms.ToolStripMenuItem MenuStrip_Server_Init;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem MenuStrip_Server_Updata;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem MenuStrip_Server_ReStar;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Timer FreshTimer;
        private System.Windows.Forms.Timer timerLog;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView listView_Server;
        private System.Windows.Forms.ColumnHeader columnHeader_server;
        private System.Windows.Forms.ColumnHeader columnHeader_name;
        private System.Windows.Forms.ColumnHeader columnHeader_status;
        private System.Windows.Forms.ColumnHeader columnHeader_var;
        private System.Windows.Forms.ColumnHeader columnHeader_descr;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
    }
}