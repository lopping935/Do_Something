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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MdiParent));
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("系统通信服务", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
            "一级系统通信服务",
            "AnB_CoreAlgorithm",
            "",
            "",
            "与一级系统、喷枪通信，执行自动打印任务"}, -1);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new string[] {
            "MES系统通信服务",
            "AnB_CoreAlgorithmMES",
            "",
            "",
            "与MES系统通信，接收标签数据，反馈标签结果"}, -1);
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("标签打印配置");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("视觉定位系统");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("历史记录查询");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("喷号贴标信息");
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.系统ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.QuitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.cascadeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileVerticalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileHorizontalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.indexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.listView_Server = new System.Windows.Forms.ListView();
            this.columnHeader_server = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_var = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_descr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.listView2 = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.FreshTimer = new System.Windows.Forms.Timer(this.components);
            this.timerLog = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.printToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.printPreviewToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel9 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel6 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel7 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel8 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.MenuStrip_Server.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.系统ToolStripMenuItem,
            this.toolStripMenuItem1,
            this.viewMenu,
            this.windowsMenu,
            this.helpMenu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1370, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 系统ToolStripMenuItem
            // 
            this.系统ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem4,
            this.QuitToolStripMenuItem});
            this.系统ToolStripMenuItem.Name = "系统ToolStripMenuItem";
            this.系统ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.系统ToolStripMenuItem.Text = "系统";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem4.Image")));
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(138, 22);
            this.toolStripMenuItem4.Text = "重新登录(&L)";
            // 
            // QuitToolStripMenuItem
            // 
            this.QuitToolStripMenuItem.Name = "QuitToolStripMenuItem";
            this.QuitToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.QuitToolStripMenuItem.Text = "退出";
            this.QuitToolStripMenuItem.Click += new System.EventHandler(this.QuitToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(85, 21);
            this.toolStripMenuItem1.Text = "用户管理(&U)";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem2.Image")));
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(140, 22);
            this.toolStripMenuItem2.Text = "用户设置(&I)";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem3.Image")));
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(140, 22);
            this.toolStripMenuItem3.Text = "密码设置(&A)";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // viewMenu
            // 
            this.viewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBarToolStripMenuItem,
            this.statusBarToolStripMenuItem});
            this.viewMenu.Name = "viewMenu";
            this.viewMenu.Size = new System.Drawing.Size(60, 21);
            this.viewMenu.Text = "视图(&V)";
            this.viewMenu.Click += new System.EventHandler(this.ToolBarToolStripMenuItem_Click);
            // 
            // toolBarToolStripMenuItem
            // 
            this.toolBarToolStripMenuItem.Checked = true;
            this.toolBarToolStripMenuItem.CheckOnClick = true;
            this.toolBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolBarToolStripMenuItem.Name = "toolBarToolStripMenuItem";
            this.toolBarToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.toolBarToolStripMenuItem.Text = "工具栏(&T)";
            // 
            // statusBarToolStripMenuItem
            // 
            this.statusBarToolStripMenuItem.Checked = true;
            this.statusBarToolStripMenuItem.CheckOnClick = true;
            this.statusBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.statusBarToolStripMenuItem.Name = "statusBarToolStripMenuItem";
            this.statusBarToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.statusBarToolStripMenuItem.Text = "状态栏(&S)";
            this.statusBarToolStripMenuItem.Click += new System.EventHandler(this.StatusBarToolStripMenuItem_Click);
            // 
            // windowsMenu
            // 
            this.windowsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cascadeToolStripMenuItem,
            this.tileVerticalToolStripMenuItem,
            this.tileHorizontalToolStripMenuItem,
            this.closeAllToolStripMenuItem});
            this.windowsMenu.Name = "windowsMenu";
            this.windowsMenu.Size = new System.Drawing.Size(64, 21);
            this.windowsMenu.Text = "窗口(&W)";
            // 
            // cascadeToolStripMenuItem
            // 
            this.cascadeToolStripMenuItem.Name = "cascadeToolStripMenuItem";
            this.cascadeToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.cascadeToolStripMenuItem.Text = "层叠(&C)";
            this.cascadeToolStripMenuItem.Click += new System.EventHandler(this.CascadeToolStripMenuItem_Click);
            // 
            // tileVerticalToolStripMenuItem
            // 
            this.tileVerticalToolStripMenuItem.Name = "tileVerticalToolStripMenuItem";
            this.tileVerticalToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.tileVerticalToolStripMenuItem.Text = "垂直平铺(&V)";
            this.tileVerticalToolStripMenuItem.Click += new System.EventHandler(this.TileVerticalToolStripMenuItem_Click);
            // 
            // tileHorizontalToolStripMenuItem
            // 
            this.tileHorizontalToolStripMenuItem.Name = "tileHorizontalToolStripMenuItem";
            this.tileHorizontalToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.tileHorizontalToolStripMenuItem.Text = "水平平铺(&H)";
            this.tileHorizontalToolStripMenuItem.Click += new System.EventHandler(this.TileHorizontalToolStripMenuItem_Click);
            // 
            // closeAllToolStripMenuItem
            // 
            this.closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
            this.closeAllToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.closeAllToolStripMenuItem.Text = "全部关闭(&L)";
            this.closeAllToolStripMenuItem.Click += new System.EventHandler(this.CloseAllToolStripMenuItem_Click);
            // 
            // helpMenu
            // 
            this.helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.indexToolStripMenuItem,
            this.toolStripSeparator8,
            this.aboutToolStripMenuItem});
            this.helpMenu.Name = "helpMenu";
            this.helpMenu.Size = new System.Drawing.Size(61, 21);
            this.helpMenu.Text = "帮助(&H)";
            // 
            // indexToolStripMenuItem
            // 
            this.indexToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("indexToolStripMenuItem.Image")));
            this.indexToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.indexToolStripMenuItem.Name = "indexToolStripMenuItem";
            this.indexToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.indexToolStripMenuItem.Text = "总索引(&I)";
            this.indexToolStripMenuItem.Click += new System.EventHandler(this.indexToolStripMenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(139, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.aboutToolStripMenuItem.Text = "关于(&A) ... ...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControl1.Location = new System.Drawing.Point(0, 545);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1370, 183);
            this.tabControl1.TabIndex = 7;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.listView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1362, 157);
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
            this.listView1.Location = new System.Drawing.Point(3, 3);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.ShowItemToolTips = true;
            this.listView1.Size = new System.Drawing.Size(1356, 151);
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
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.listView_Server);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1362, 157);
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
            listViewGroup2.Header = "系统通信服务";
            listViewGroup2.Name = "listViewGroup1";
            this.listView_Server.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup2});
            listViewItem3.Group = listViewGroup2;
            listViewItem3.Tag = "";
            listViewItem4.Group = listViewGroup2;
            this.listView_Server.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem3,
            listViewItem4});
            this.listView_Server.Location = new System.Drawing.Point(3, 3);
            this.listView_Server.MultiSelect = false;
            this.listView_Server.Name = "listView_Server";
            this.listView_Server.ShowItemToolTips = true;
            this.listView_Server.Size = new System.Drawing.Size(1356, 151);
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
            // MenuStrip_Server
            // 
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
            this.MenuStrip_Server.Size = new System.Drawing.Size(125, 132);
            // 
            // MenuStrip_Server_Star
            // 
            this.MenuStrip_Server_Star.Name = "MenuStrip_Server_Star";
            this.MenuStrip_Server_Star.Size = new System.Drawing.Size(124, 22);
            this.MenuStrip_Server_Star.Text = "启动";
            this.MenuStrip_Server_Star.Click += new System.EventHandler(this.MenuStrip_Server_Star_Click);
            // 
            // MenuStrip_Server_Stop
            // 
            this.MenuStrip_Server_Stop.Name = "MenuStrip_Server_Stop";
            this.MenuStrip_Server_Stop.Size = new System.Drawing.Size(124, 22);
            this.MenuStrip_Server_Stop.Text = "停止";
            this.MenuStrip_Server_Stop.Click += new System.EventHandler(this.MenuStrip_Server_Stop_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(121, 6);
            // 
            // MenuStrip_Server_Fresh
            // 
            this.MenuStrip_Server_Fresh.Name = "MenuStrip_Server_Fresh";
            this.MenuStrip_Server_Fresh.Size = new System.Drawing.Size(124, 22);
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
            this.ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.ToolStripMenuItem.Text = "更新服务";
            this.ToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // MenuStrip_Server_Unini
            // 
            this.MenuStrip_Server_Unini.Name = "MenuStrip_Server_Unini";
            this.MenuStrip_Server_Unini.Size = new System.Drawing.Size(100, 22);
            this.MenuStrip_Server_Unini.Text = "卸载";
            this.MenuStrip_Server_Unini.Click += new System.EventHandler(this.MenuStrip_Server_Unini_Click);
            // 
            // MenuStrip_Server_Init
            // 
            this.MenuStrip_Server_Init.Name = "MenuStrip_Server_Init";
            this.MenuStrip_Server_Init.Size = new System.Drawing.Size(100, 22);
            this.MenuStrip_Server_Init.Text = "安装";
            this.MenuStrip_Server_Init.Click += new System.EventHandler(this.MenuStrip_Server_Init_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(97, 6);
            // 
            // MenuStrip_Server_Updata
            // 
            this.MenuStrip_Server_Updata.Name = "MenuStrip_Server_Updata";
            this.MenuStrip_Server_Updata.Size = new System.Drawing.Size(100, 22);
            this.MenuStrip_Server_Updata.Text = "更新";
            this.MenuStrip_Server_Updata.Click += new System.EventHandler(this.MenuStrip_Server_Updata_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(121, 6);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(121, 6);
            // 
            // MenuStrip_Server_ReStar
            // 
            this.MenuStrip_Server_ReStar.Name = "MenuStrip_Server_ReStar";
            this.MenuStrip_Server_ReStar.Size = new System.Drawing.Size(124, 22);
            this.MenuStrip_Server_ReStar.Text = "重新启动";
            this.MenuStrip_Server_ReStar.Click += new System.EventHandler(this.MenuStrip_Server_ReStar_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.listView2);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1362, 157);
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
            this.listView2.Location = new System.Drawing.Point(0, 0);
            this.listView2.MultiSelect = false;
            this.listView2.Name = "listView2";
            this.listView2.ShowItemToolTips = true;
            this.listView2.Size = new System.Drawing.Size(1362, 157);
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
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 542);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1370, 3);
            this.splitter1.TabIndex = 8;
            this.splitter1.TabStop = false;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeView1.Location = new System.Drawing.Point(0, 50);
            this.treeView1.Name = "treeView1";
            treeNode5.Name = "节点0";
            treeNode5.Text = "标签打印配置";
            treeNode6.Name = "节点0";
            treeNode6.Text = "视觉定位系统";
            treeNode7.Name = "节点3";
            treeNode7.Text = "历史记录查询";
            treeNode8.Name = "节点0";
            treeNode8.Text = "喷号贴标信息";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8});
            this.treeView1.Size = new System.Drawing.Size(143, 492);
            this.treeView1.TabIndex = 11;
            this.treeView1.DoubleClick += new System.EventHandler(this.treeView1_DoubleClick);
            // 
            // splitter2
            // 
            this.splitter2.Location = new System.Drawing.Point(143, 50);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 492);
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
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.printToolStripButton,
            this.printPreviewToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1370, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // printToolStripButton
            // 
            this.printToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("printToolStripButton.Image")));
            this.printToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
            this.printToolStripButton.Name = "printToolStripButton";
            this.printToolStripButton.Size = new System.Drawing.Size(52, 22);
            this.printToolStripButton.Text = "全屏";
            this.printToolStripButton.Click += new System.EventHandler(this.printToolStripButton_Click);
            // 
            // printPreviewToolStripButton
            // 
            this.printPreviewToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("printPreviewToolStripButton.Image")));
            this.printPreviewToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
            this.printPreviewToolStripButton.Name = "printPreviewToolStripButton";
            this.printPreviewToolStripButton.Size = new System.Drawing.Size(76, 22);
            this.printPreviewToolStripButton.Text = "退出系统";
            this.printPreviewToolStripButton.Click += new System.EventHandler(this.printPreviewToolStripButton_Click);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.Window;
            this.label4.Location = new System.Drawing.Point(1527, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 23);
            this.label4.TabIndex = 20;
            this.label4.Text = "label4";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1652, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(194, 49);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.Window;
            this.label3.Location = new System.Drawing.Point(1527, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 25);
            this.label3.TabIndex = 19;
            this.label3.Text = "label3";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel4,
            this.toolStripStatusLabel9,
            this.toolStripStatusLabel5,
            this.toolStripStatusLabel6,
            this.toolStripStatusLabel7,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel8});
            this.statusStrip1.Location = new System.Drawing.Point(0, 728);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1370, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(83, 17);
            this.toolStripStatusLabel1.Text = "一级系统通信:";
            this.toolStripStatusLabel1.Visible = false;
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(68, 17);
            this.toolStripStatusLabel4.Text = "状态不确定";
            this.toolStripStatusLabel4.Visible = false;
            // 
            // toolStripStatusLabel9
            // 
            this.toolStripStatusLabel9.Name = "toolStripStatusLabel9";
            this.toolStripStatusLabel9.Size = new System.Drawing.Size(12, 17);
            this.toolStripStatusLabel9.Text = " ";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(94, 17);
            this.toolStripStatusLabel5.Text = "MES系统通信：";
            this.toolStripStatusLabel5.Visible = false;
            // 
            // toolStripStatusLabel6
            // 
            this.toolStripStatusLabel6.Name = "toolStripStatusLabel6";
            this.toolStripStatusLabel6.Size = new System.Drawing.Size(68, 17);
            this.toolStripStatusLabel6.Text = "状态不确定";
            this.toolStripStatusLabel6.Visible = false;
            // 
            // toolStripStatusLabel7
            // 
            this.toolStripStatusLabel7.Name = "toolStripStatusLabel7";
            this.toolStripStatusLabel7.Size = new System.Drawing.Size(12, 17);
            this.toolStripStatusLabel7.Text = " ";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(83, 17);
            this.toolStripStatusLabel2.Text = "喷号贴标状态;";
            this.toolStripStatusLabel2.Visible = false;
            // 
            // toolStripStatusLabel8
            // 
            this.toolStripStatusLabel8.Name = "toolStripStatusLabel8";
            this.toolStripStatusLabel8.Size = new System.Drawing.Size(56, 17);
            this.toolStripStatusLabel8.Text = "申请打印";
            this.toolStripStatusLabel8.Visible = false;
            // 
            // MdiParent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 750);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MdiParent";
            this.Text = "大型线喷号贴标机器人信息系统";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MdiParent_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MdiParent_FormClosed);
            this.Load += new System.EventHandler(this.MdiParent_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.MenuStrip_Server.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 系统ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem QuitToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView listView_Server;
        private System.Windows.Forms.ColumnHeader columnHeader_server;
        private System.Windows.Forms.ColumnHeader columnHeader_status;
        private System.Windows.Forms.ColumnHeader columnHeader_descr;
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
        private System.Windows.Forms.ColumnHeader columnHeader_var;
        private System.Windows.Forms.Timer FreshTimer;
        private System.Windows.Forms.ColumnHeader columnHeader_name;
        private System.Windows.Forms.Timer timerLog;
        private System.Windows.Forms.ToolStripMenuItem viewMenu;
        private System.Windows.Forms.ToolStripMenuItem toolBarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statusBarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowsMenu;
        private System.Windows.Forms.ToolStripMenuItem cascadeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tileVerticalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tileHorizontalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenu;
        private System.Windows.Forms.ToolStripMenuItem indexToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton printToolStripButton;
        private System.Windows.Forms.ToolStripButton printPreviewToolStripButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel9;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel6;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel7;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel8;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
    }
}