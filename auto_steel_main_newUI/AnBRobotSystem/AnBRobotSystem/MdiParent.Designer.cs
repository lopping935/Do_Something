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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("折铁流程");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("视觉图像");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("历史记录");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("实时数据");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MdiParent));
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.FreshTimer = new System.Windows.Forms.Timer(this.components);
            this.timerLog = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.uiNavBar1 = new Sunny.UI.UINavBar();
            this.uiLabel4 = new Sunny.UI.UILabel();
            this.uiLabel3 = new Sunny.UI.UILabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.uiButton1 = new Sunny.UI.UIButton();
            this.uiSymbolButton2 = new Sunny.UI.UISymbolButton();
            this.uiLabel5 = new Sunny.UI.UILabel();
            this.uiSymbolButton1 = new Sunny.UI.UISymbolButton();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.uiTabControl1 = new Sunny.UI.UITabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.listView4 = new System.Windows.Forms.ListView();
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.alarm_timer = new System.Windows.Forms.Timer(this.components);
            this.uiPanel1 = new Sunny.UI.UIPanel();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.uiTitlePanel1 = new Sunny.UI.UITitlePanel();
            this.uiTitlePanel2 = new Sunny.UI.UITitlePanel();
            this.hum_chose_GB = new Sunny.UI.UIButton();
            this.reqweight_uiTextBox1 = new Sunny.UI.UITextBox();
            this.B_chose = new Sunny.UI.UIRadioButton();
            this.A_chose = new Sunny.UI.UIRadioButton();
            this.uiLabel9 = new Sunny.UI.UILabel();
            this.uiTitlePanel3 = new Sunny.UI.UITitlePanel();
            this.uiNavBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.uiTabControl1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.uiPanel1.SuspendLayout();
            this.uiTitlePanel1.SuspendLayout();
            this.uiTitlePanel2.SuspendLayout();
            this.uiTitlePanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 821);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1612, 3);
            this.splitter1.TabIndex = 8;
            this.splitter1.TabStop = false;
            // 
            // splitter2
            // 
            this.splitter2.Location = new System.Drawing.Point(0, 35);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 786);
            this.splitter2.TabIndex = 12;
            this.splitter2.TabStop = false;
            // 
            // FreshTimer
            // 
            this.FreshTimer.Interval = 1000;
            this.FreshTimer.Tick += new System.EventHandler(this.FreshTimer_Tick);
            // 
            // timerLog
            // 
            this.timerLog.Interval = 1000;
            this.timerLog.Tick += new System.EventHandler(this.timerLog_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Location = new System.Drawing.Point(0, 35);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1612, 28);
            this.menuStrip1.TabIndex = 22;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            // 
            // uiNavBar1
            // 
            this.uiNavBar1.BackColor = System.Drawing.Color.LightCyan;
            this.uiNavBar1.Controls.Add(this.uiLabel4);
            this.uiNavBar1.Controls.Add(this.uiLabel3);
            this.uiNavBar1.Controls.Add(this.pictureBox1);
            this.uiNavBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiNavBar1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiNavBar1.ForeColor = System.Drawing.Color.Black;
            this.uiNavBar1.Location = new System.Drawing.Point(3, 35);
            this.uiNavBar1.MenuHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.uiNavBar1.MenuSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.uiNavBar1.MenuStyle = Sunny.UI.UIMenuStyle.Custom;
            this.uiNavBar1.Name = "uiNavBar1";
            this.uiNavBar1.NodeInterval = 50;
            treeNode1.BackColor = System.Drawing.Color.White;
            treeNode1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            treeNode1.Name = "折铁流程";
            treeNode1.NodeFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            treeNode1.Text = "折铁流程";
            treeNode2.Name = "视觉图像";
            treeNode2.Text = "视觉图像";
            treeNode3.Name = "历史记录";
            treeNode3.Text = "历史记录";
            treeNode4.Name = "实时数据";
            treeNode4.Text = "实时数据";
            this.uiNavBar1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4});
            this.uiNavBar1.NodeSize = new System.Drawing.Size(120, 40);
            this.uiNavBar1.Size = new System.Drawing.Size(1609, 87);
            this.uiNavBar1.TabIndex = 28;
            this.uiNavBar1.Text = "uiNavBar1";
            this.uiNavBar1.NodeMouseClick += new Sunny.UI.UINavBar.OnNodeMouseClick(this.uiNavBar1_NodeMouseClick);
            // 
            // uiLabel4
            // 
            this.uiLabel4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel4.Location = new System.Drawing.Point(117, 49);
            this.uiLabel4.Name = "uiLabel4";
            this.uiLabel4.Size = new System.Drawing.Size(390, 23);
            this.uiLabel4.TabIndex = 46;
            this.uiLabel4.Text = "LAIGANG  GROUP  ELECTRONIC  CO.,LTD";
            this.uiLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel3
            // 
            this.uiLabel3.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel3.Location = new System.Drawing.Point(116, 16);
            this.uiLabel3.Name = "uiLabel3";
            this.uiLabel3.Size = new System.Drawing.Size(382, 37);
            this.uiLabel3.TabIndex = 45;
            this.uiLabel3.Text = "莱 芜 钢 铁 集 团 电 子 有 限 公 司\r\n\r\n";
            this.uiLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.ErrorImage")));
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(7, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(104, 78);
            this.pictureBox1.TabIndex = 44;
            this.pictureBox1.TabStop = false;
            // 
            // uiButton1
            // 
            this.uiButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiButton1.Location = new System.Drawing.Point(122, 131);
            this.uiButton1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton1.Name = "uiButton1";
            this.uiButton1.Size = new System.Drawing.Size(100, 35);
            this.uiButton1.Style = Sunny.UI.UIStyle.Custom;
            this.uiButton1.TabIndex = 49;
            this.uiButton1.Text = "测试";
            this.uiButton1.Click += new System.EventHandler(this.uiButton1_Click);
            // 
            // uiSymbolButton2
            // 
            this.uiSymbolButton2.CircleRectWidth = 5;
            this.uiSymbolButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiSymbolButton2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiSymbolButton2.ImageInterval = 3;
            this.uiSymbolButton2.Location = new System.Drawing.Point(10, 49);
            this.uiSymbolButton2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolButton2.Name = "uiSymbolButton2";
            this.uiSymbolButton2.Radius = 40;
            this.uiSymbolButton2.Size = new System.Drawing.Size(109, 41);
            this.uiSymbolButton2.Style = Sunny.UI.UIStyle.Custom;
            this.uiSymbolButton2.Symbol = 61515;
            this.uiSymbolButton2.SymbolSize = 40;
            this.uiSymbolButton2.TabIndex = 48;
            this.uiSymbolButton2.TagString = "";
            this.uiSymbolButton2.Click += new System.EventHandler(this.uiSymbolButton2_Click);
            // 
            // uiLabel5
            // 
            this.uiLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel5.Location = new System.Drawing.Point(185, 105);
            this.uiLabel5.Name = "uiLabel5";
            this.uiLabel5.Size = new System.Drawing.Size(113, 23);
            this.uiLabel5.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel5.TabIndex = 47;
            this.uiLabel5.Text = "强制退出";
            this.uiLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiSymbolButton1
            // 
            this.uiSymbolButton1.CircleRectWidth = 5;
            this.uiSymbolButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiSymbolButton1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiSymbolButton1.ImageInterval = 3;
            this.uiSymbolButton1.Location = new System.Drawing.Point(189, 49);
            this.uiSymbolButton1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolButton1.Name = "uiSymbolButton1";
            this.uiSymbolButton1.Radius = 40;
            this.uiSymbolButton1.Size = new System.Drawing.Size(118, 41);
            this.uiSymbolButton1.Style = Sunny.UI.UIStyle.Custom;
            this.uiSymbolButton1.Symbol = 61516;
            this.uiSymbolButton1.SymbolSize = 40;
            this.uiSymbolButton1.TabIndex = 43;
            this.uiSymbolButton1.TagString = "";
            this.uiSymbolButton1.Click += new System.EventHandler(this.uiSymbolButton1_Click_1);
            // 
            // uiLabel1
            // 
            this.uiLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel1.Location = new System.Drawing.Point(6, 105);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(113, 23);
            this.uiLabel1.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel1.TabIndex = 28;
            this.uiLabel1.Text = "折铁开始";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiTabControl1
            // 
            this.uiTabControl1.Controls.Add(this.tabPage4);
            this.uiTabControl1.Controls.Add(this.tabPage5);
            this.uiTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiTabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.uiTabControl1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiTabControl1.ItemSize = new System.Drawing.Size(100, 35);
            this.uiTabControl1.Location = new System.Drawing.Point(0, 0);
            this.uiTabControl1.MainPage = "地方";
            this.uiTabControl1.MenuStyle = Sunny.UI.UIMenuStyle.Custom;
            this.uiTabControl1.Multiline = true;
            this.uiTabControl1.Name = "uiTabControl1";
            this.uiTabControl1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.uiTabControl1.SelectedIndex = 0;
            this.uiTabControl1.Size = new System.Drawing.Size(358, 188);
            this.uiTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.uiTabControl1.Style = Sunny.UI.UIStyle.Custom;
            this.uiTabControl1.StyleCustomMode = true;
            this.uiTabControl1.TabBackColor = System.Drawing.Color.Beige;
            this.uiTabControl1.TabIndex = 37;
            this.uiTabControl1.TabSelectedColor = System.Drawing.Color.Beige;
            this.uiTabControl1.TabSelectedForeColor = System.Drawing.Color.Brown;
            this.uiTabControl1.TabUnSelectedForeColor = System.Drawing.Color.Transparent;
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.White;
            this.tabPage4.Controls.Add(this.listView1);
            this.tabPage4.Location = new System.Drawing.Point(0, 35);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(358, 153);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "折铁进程";
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader3,
            this.columnHeader4});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.ShowItemToolTips = true;
            this.listView1.Size = new System.Drawing.Size(358, 153);
            this.listView1.TabIndex = 41;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "日期";
            this.columnHeader1.Width = 240;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "类型";
            this.columnHeader3.Width = 150;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "描述";
            this.columnHeader4.Width = 100;
            // 
            // tabPage5
            // 
            this.tabPage5.BackColor = System.Drawing.Color.White;
            this.tabPage5.Controls.Add(this.listView4);
            this.tabPage5.Location = new System.Drawing.Point(0, 35);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(358, 153);
            this.tabPage5.TabIndex = 1;
            this.tabPage5.Text = "折铁警报";
            // 
            // listView4
            // 
            this.listView4.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11});
            this.listView4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView4.FullRowSelect = true;
            this.listView4.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView4.HideSelection = false;
            this.listView4.Location = new System.Drawing.Point(0, 0);
            this.listView4.MultiSelect = false;
            this.listView4.Name = "listView4";
            this.listView4.ShowItemToolTips = true;
            this.listView4.Size = new System.Drawing.Size(358, 153);
            this.listView4.TabIndex = 6;
            this.listView4.UseCompatibleStateImageBehavior = false;
            this.listView4.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "日期";
            this.columnHeader9.Width = 200;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "类型";
            this.columnHeader10.Width = 120;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "描述";
            this.columnHeader11.Width = 317;
            // 
            // alarm_timer
            // 
            this.alarm_timer.Interval = 1000;
            this.alarm_timer.Tick += new System.EventHandler(this.alarm_timer_Tick);
            // 
            // uiPanel1
            // 
            this.uiPanel1.Controls.Add(this.button3);
            this.uiPanel1.Controls.Add(this.button2);
            this.uiPanel1.Controls.Add(this.button1);
            this.uiPanel1.Controls.Add(this.uiTitlePanel1);
            this.uiPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.uiPanel1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiPanel1.Location = new System.Drawing.Point(3, 122);
            this.uiPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel1.Name = "uiPanel1";
            this.uiPanel1.Size = new System.Drawing.Size(358, 699);
            this.uiPanel1.TabIndex = 44;
            this.uiPanel1.Text = null;
            this.uiPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(43, 647);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(122, 32);
            this.button3.TabIndex = 48;
            this.button3.Text = "plcwritetest";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(206, 589);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(104, 37);
            this.button2.TabIndex = 47;
            this.button2.Text = "gk";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(32, 589);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 37);
            this.button1.TabIndex = 46;
            this.button1.Text = "tb";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // uiTitlePanel1
            // 
            this.uiTitlePanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.uiTitlePanel1.Controls.Add(this.uiButton1);
            this.uiTitlePanel1.Controls.Add(this.uiSymbolButton2);
            this.uiTitlePanel1.Controls.Add(this.uiLabel5);
            this.uiTitlePanel1.Controls.Add(this.uiSymbolButton1);
            this.uiTitlePanel1.Controls.Add(this.uiLabel1);
            this.uiTitlePanel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiTitlePanel1.Location = new System.Drawing.Point(0, 2);
            this.uiTitlePanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiTitlePanel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiTitlePanel1.Name = "uiTitlePanel1";
            this.uiTitlePanel1.Padding = new System.Windows.Forms.Padding(0, 35, 0, 0);
            this.uiTitlePanel1.RectColor = System.Drawing.Color.Transparent;
            this.uiTitlePanel1.Size = new System.Drawing.Size(358, 180);
            this.uiTitlePanel1.Style = Sunny.UI.UIStyle.Custom;
            this.uiTitlePanel1.StyleCustomMode = true;
            this.uiTitlePanel1.TabIndex = 45;
            this.uiTitlePanel1.Text = "折铁控制";
            this.uiTitlePanel1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.uiTitlePanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiTitlePanel1.TitleColor = System.Drawing.Color.Beige;
            this.uiTitlePanel1.TitleForeColor = System.Drawing.Color.Brown;
            // 
            // uiTitlePanel2
            // 
            this.uiTitlePanel2.Controls.Add(this.hum_chose_GB);
            this.uiTitlePanel2.Controls.Add(this.reqweight_uiTextBox1);
            this.uiTitlePanel2.Controls.Add(this.B_chose);
            this.uiTitlePanel2.Controls.Add(this.A_chose);
            this.uiTitlePanel2.Controls.Add(this.uiLabel9);
            this.uiTitlePanel2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiTitlePanel2.Location = new System.Drawing.Point(3, 298);
            this.uiTitlePanel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiTitlePanel2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiTitlePanel2.Name = "uiTitlePanel2";
            this.uiTitlePanel2.Padding = new System.Windows.Forms.Padding(0, 35, 0, 0);
            this.uiTitlePanel2.RectColor = System.Drawing.Color.Transparent;
            this.uiTitlePanel2.Size = new System.Drawing.Size(358, 193);
            this.uiTitlePanel2.Style = Sunny.UI.UIStyle.Custom;
            this.uiTitlePanel2.StyleCustomMode = true;
            this.uiTitlePanel2.TabIndex = 45;
            this.uiTitlePanel2.Text = "折铁交互";
            this.uiTitlePanel2.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.uiTitlePanel2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiTitlePanel2.TitleColor = System.Drawing.Color.Beige;
            this.uiTitlePanel2.TitleForeColor = System.Drawing.Color.Brown;
            // 
            // hum_chose_GB
            // 
            this.hum_chose_GB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hum_chose_GB.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.hum_chose_GB.Location = new System.Drawing.Point(206, 108);
            this.hum_chose_GB.MinimumSize = new System.Drawing.Size(1, 1);
            this.hum_chose_GB.Name = "hum_chose_GB";
            this.hum_chose_GB.Size = new System.Drawing.Size(72, 35);
            this.hum_chose_GB.Style = Sunny.UI.UIStyle.Custom;
            this.hum_chose_GB.TabIndex = 27;
            this.hum_chose_GB.Text = "确认";
            this.hum_chose_GB.Click += new System.EventHandler(this.hum_chose_GB_Click);
            // 
            // reqweight_uiTextBox1
            // 
            this.reqweight_uiTextBox1.ButtonSymbol = 61761;
            this.reqweight_uiTextBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.reqweight_uiTextBox1.DoubleValue = 280D;
            this.reqweight_uiTextBox1.FillColor = System.Drawing.Color.White;
            this.reqweight_uiTextBox1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.reqweight_uiTextBox1.IntValue = 280;
            this.reqweight_uiTextBox1.Location = new System.Drawing.Point(222, 55);
            this.reqweight_uiTextBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.reqweight_uiTextBox1.Maximum = 2147483647D;
            this.reqweight_uiTextBox1.Minimum = -2147483648D;
            this.reqweight_uiTextBox1.MinimumSize = new System.Drawing.Size(1, 1);
            this.reqweight_uiTextBox1.Name = "reqweight_uiTextBox1";
            this.reqweight_uiTextBox1.Size = new System.Drawing.Size(56, 34);
            this.reqweight_uiTextBox1.Style = Sunny.UI.UIStyle.Custom;
            this.reqweight_uiTextBox1.TabIndex = 32;
            this.reqweight_uiTextBox1.Text = "280";
            this.reqweight_uiTextBox1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.reqweight_uiTextBox1.TextChanged += new System.EventHandler(this.reqweight_uiTextBox1_TextChanged);
            // 
            // B_chose
            // 
            this.B_chose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.B_chose.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.B_chose.Location = new System.Drawing.Point(32, 108);
            this.B_chose.MinimumSize = new System.Drawing.Size(1, 1);
            this.B_chose.Name = "B_chose";
            this.B_chose.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.B_chose.Size = new System.Drawing.Size(90, 29);
            this.B_chose.Style = Sunny.UI.UIStyle.Custom;
            this.B_chose.TabIndex = 31;
            this.B_chose.Text = "3号罐位";
            // 
            // A_chose
            // 
            this.A_chose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.A_chose.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.A_chose.Location = new System.Drawing.Point(32, 60);
            this.A_chose.MinimumSize = new System.Drawing.Size(1, 1);
            this.A_chose.Name = "A_chose";
            this.A_chose.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.A_chose.Size = new System.Drawing.Size(90, 29);
            this.A_chose.Style = Sunny.UI.UIStyle.Custom;
            this.A_chose.TabIndex = 28;
            this.A_chose.Text = "4号罐位";
            // 
            // uiLabel9
            // 
            this.uiLabel9.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel9.Location = new System.Drawing.Point(164, 61);
            this.uiLabel9.Name = "uiLabel9";
            this.uiLabel9.Size = new System.Drawing.Size(80, 23);
            this.uiLabel9.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel9.TabIndex = 29;
            this.uiLabel9.Text = "重量：";
            this.uiLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiTitlePanel3
            // 
            this.uiTitlePanel3.Controls.Add(this.uiTabControl1);
            this.uiTitlePanel3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiTitlePanel3.Location = new System.Drawing.Point(3, 488);
            this.uiTitlePanel3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiTitlePanel3.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiTitlePanel3.Name = "uiTitlePanel3";
            this.uiTitlePanel3.Size = new System.Drawing.Size(358, 188);
            this.uiTitlePanel3.TabIndex = 46;
            this.uiTitlePanel3.Text = null;
            this.uiTitlePanel3.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.uiTitlePanel3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiTitlePanel3.TitleHeight = 0;
            // 
            // MdiParent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1612, 824);
            this.Controls.Add(this.uiTitlePanel2);
            this.Controls.Add(this.uiTitlePanel3);
            this.Controls.Add(this.uiPanel1);
            this.Controls.Add(this.uiNavBar1);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.menuStrip1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MdiParent";
            this.ShowRadius = false;
            this.Text = "";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MdiParent_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MdiParent_FormClosed);
            this.Load += new System.EventHandler(this.MdiParent_Load);
            this.uiNavBar1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.uiTabControl1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.uiPanel1.ResumeLayout(false);
            this.uiTitlePanel1.ResumeLayout(false);
            this.uiTitlePanel2.ResumeLayout(false);
            this.uiTitlePanel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Timer FreshTimer;
        private System.Windows.Forms.Timer timerLog;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private Sunny.UI.UINavBar uiNavBar1;
        private Sunny.UI.UITabControl uiTabControl1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private Sunny.UI.UILabel uiLabel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Sunny.UI.UILabel uiLabel4;
        private Sunny.UI.UILabel uiLabel3;
        private Sunny.UI.UILabel uiLabel5;
        private Sunny.UI.UISymbolButton uiSymbolButton1;
        private Sunny.UI.UISymbolButton uiSymbolButton2;
        private Sunny.UI.UIButton uiButton1;
        private System.Windows.Forms.Timer alarm_timer;
        public System.Windows.Forms.ListView listView4;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private Sunny.UI.UIPanel uiPanel1;
        private Sunny.UI.UITitlePanel uiTitlePanel1;
        private Sunny.UI.UITitlePanel uiTitlePanel2;
        private Sunny.UI.UITitlePanel uiTitlePanel3;
        private Sunny.UI.UIButton hum_chose_GB;
        private Sunny.UI.UITextBox reqweight_uiTextBox1;
        private Sunny.UI.UIRadioButton B_chose;
        private Sunny.UI.UIRadioButton A_chose;
        private Sunny.UI.UILabel uiLabel9;
        public System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
    }
}