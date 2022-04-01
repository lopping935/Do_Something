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
            this.PLC_connect_state = new Sunny.UI.UILedBulb();
            this.uiLabel7 = new Sunny.UI.UILabel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.uiLabel4 = new Sunny.UI.UILabel();
            this.uiLabel3 = new Sunny.UI.UILabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.uiSymbolButton2 = new Sunny.UI.UISymbolButton();
            this.uiLabel5 = new Sunny.UI.UILabel();
            this.uiSymbolButton1 = new Sunny.UI.UISymbolButton();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.listView4 = new System.Windows.Forms.ListView();
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.alarm_timer = new System.Windows.Forms.Timer(this.components);
            this.uiPanel1 = new Sunny.UI.UIPanel();
            this.uiTitlePanel2 = new Sunny.UI.UITitlePanel();
            this.uiLabel11 = new Sunny.UI.UILabel();
            this.uiLabel10 = new Sunny.UI.UILabel();
            this.uiLabel8 = new Sunny.UI.UILabel();
            this.set_weight = new Sunny.UI.UIButton();
            this.text_B_train_full_weight = new Sunny.UI.UITextBox();
            this.uiLabel6 = new Sunny.UI.UILabel();
            this.text_A_train_full_weight = new Sunny.UI.UITextBox();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.hum_chose_GB = new Sunny.UI.UIButton();
            this.reqweight_uiTextBox1 = new Sunny.UI.UITextBox();
            this.B_chose = new Sunny.UI.UIRadioButton();
            this.A_chose = new Sunny.UI.UIRadioButton();
            this.uiLabel9 = new Sunny.UI.UILabel();
            this.uiTitlePanel1 = new Sunny.UI.UITitlePanel();
            this.uiTitlePanel3 = new Sunny.UI.UITitlePanel();
            this.uiTabControl1 = new Sunny.UI.UITabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.uiTitlePanel4 = new Sunny.UI.UITitlePanel();
            this.uiNavBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.uiPanel1.SuspendLayout();
            this.uiTitlePanel2.SuspendLayout();
            this.uiTitlePanel1.SuspendLayout();
            this.uiTitlePanel3.SuspendLayout();
            this.uiTabControl1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.uiTitlePanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 956);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1643, 3);
            this.splitter1.TabIndex = 8;
            this.splitter1.TabStop = false;
            // 
            // splitter2
            // 
            this.splitter2.Location = new System.Drawing.Point(0, 35);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 921);
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
            this.menuStrip1.Size = new System.Drawing.Size(1700, 28);
            this.menuStrip1.TabIndex = 22;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            // 
            // uiNavBar1
            // 
            this.uiNavBar1.BackColor = System.Drawing.Color.LightCyan;
            this.uiNavBar1.Controls.Add(this.PLC_connect_state);
            this.uiNavBar1.Controls.Add(this.uiLabel7);
            this.uiNavBar1.Controls.Add(this.button1);
            this.uiNavBar1.Controls.Add(this.button2);
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
            this.uiNavBar1.Size = new System.Drawing.Size(1640, 87);
            this.uiNavBar1.TabIndex = 28;
            this.uiNavBar1.Text = "uiNavBar1";
            this.uiNavBar1.NodeMouseClick += new Sunny.UI.UINavBar.OnNodeMouseClick(this.uiNavBar1_NodeMouseClick);
            // 
            // PLC_connect_state
            // 
            this.PLC_connect_state.Color = System.Drawing.Color.Red;
            this.PLC_connect_state.Location = new System.Drawing.Point(883, 29);
            this.PLC_connect_state.Name = "PLC_connect_state";
            this.PLC_connect_state.Size = new System.Drawing.Size(61, 38);
            this.PLC_connect_state.TabIndex = 98;
            this.PLC_connect_state.Text = "uiLedBulb3";
            this.PLC_connect_state.Click += new System.EventHandler(this.PLC_connect_state_Click);
            // 
            // uiLabel7
            // 
            this.uiLabel7.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Bold);
            this.uiLabel7.Location = new System.Drawing.Point(744, 29);
            this.uiLabel7.Name = "uiLabel7";
            this.uiLabel7.Size = new System.Drawing.Size(160, 34);
            this.uiLabel7.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel7.TabIndex = 41;
            this.uiLabel7.Text = "一级通讯：";
            this.uiLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(422, 47);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 37);
            this.button1.TabIndex = 46;
            this.button1.Text = "tb";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(422, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(104, 37);
            this.button2.TabIndex = 47;
            this.button2.Text = "gk";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
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
            this.uiLabel4.Visible = false;
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
            // uiSymbolButton2
            // 
            this.uiSymbolButton2.CircleRectWidth = 5;
            this.uiSymbolButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiSymbolButton2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiSymbolButton2.ImageInterval = 3;
            this.uiSymbolButton2.Location = new System.Drawing.Point(47, 49);
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
            this.uiLabel5.Location = new System.Drawing.Point(215, 105);
            this.uiLabel5.Name = "uiLabel5";
            this.uiLabel5.Size = new System.Drawing.Size(113, 29);
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
            this.uiSymbolButton1.Location = new System.Drawing.Point(210, 49);
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
            this.uiLabel1.Location = new System.Drawing.Point(54, 105);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(117, 29);
            this.uiLabel1.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel1.TabIndex = 28;
            this.uiLabel1.Text = "折铁开始";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // listView4
            // 
            this.listView4.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11});
            this.listView4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView4.ForeColor = System.Drawing.Color.DeepPink;
            this.listView4.FullRowSelect = true;
            this.listView4.GridLines = true;
            this.listView4.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView4.HideSelection = false;
            this.listView4.Location = new System.Drawing.Point(0, 35);
            this.listView4.MultiSelect = false;
            this.listView4.Name = "listView4";
            this.listView4.ShowItemToolTips = true;
            this.listView4.Size = new System.Drawing.Size(361, 163);
            this.listView4.TabIndex = 6;
            this.listView4.UseCompatibleStateImageBehavior = false;
            this.listView4.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "日期";
            this.columnHeader9.Width = 240;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "类型";
            this.columnHeader10.Width = 150;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "描述";
            this.columnHeader11.Width = 100;
            // 
            // alarm_timer
            // 
            this.alarm_timer.Interval = 500;
            this.alarm_timer.Tick += new System.EventHandler(this.alarm_timer_Tick);
            // 
            // uiPanel1
            // 
            this.uiPanel1.Controls.Add(this.uiTitlePanel2);
            this.uiPanel1.Controls.Add(this.uiTitlePanel1);
            this.uiPanel1.Controls.Add(this.uiTitlePanel3);
            this.uiPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.uiPanel1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiPanel1.Location = new System.Drawing.Point(3, 122);
            this.uiPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel1.Name = "uiPanel1";
            this.uiPanel1.Size = new System.Drawing.Size(358, 834);
            this.uiPanel1.TabIndex = 44;
            this.uiPanel1.Text = null;
            this.uiPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiTitlePanel2
            // 
            this.uiTitlePanel2.Controls.Add(this.uiLabel11);
            this.uiTitlePanel2.Controls.Add(this.uiLabel10);
            this.uiTitlePanel2.Controls.Add(this.uiLabel8);
            this.uiTitlePanel2.Controls.Add(this.set_weight);
            this.uiTitlePanel2.Controls.Add(this.text_B_train_full_weight);
            this.uiTitlePanel2.Controls.Add(this.uiLabel6);
            this.uiTitlePanel2.Controls.Add(this.text_A_train_full_weight);
            this.uiTitlePanel2.Controls.Add(this.uiLabel2);
            this.uiTitlePanel2.Controls.Add(this.hum_chose_GB);
            this.uiTitlePanel2.Controls.Add(this.reqweight_uiTextBox1);
            this.uiTitlePanel2.Controls.Add(this.B_chose);
            this.uiTitlePanel2.Controls.Add(this.A_chose);
            this.uiTitlePanel2.Controls.Add(this.uiLabel9);
            this.uiTitlePanel2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiTitlePanel2.Location = new System.Drawing.Point(1, 2);
            this.uiTitlePanel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiTitlePanel2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiTitlePanel2.Name = "uiTitlePanel2";
            this.uiTitlePanel2.Padding = new System.Windows.Forms.Padding(0, 35, 0, 0);
            this.uiTitlePanel2.RectColor = System.Drawing.Color.Transparent;
            this.uiTitlePanel2.Size = new System.Drawing.Size(358, 252);
            this.uiTitlePanel2.Style = Sunny.UI.UIStyle.Custom;
            this.uiTitlePanel2.StyleCustomMode = true;
            this.uiTitlePanel2.TabIndex = 45;
            this.uiTitlePanel2.Text = "折铁交互";
            this.uiTitlePanel2.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.uiTitlePanel2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiTitlePanel2.TitleColor = System.Drawing.Color.Beige;
            this.uiTitlePanel2.TitleForeColor = System.Drawing.Color.Brown;
            // 
            // uiLabel11
            // 
            this.uiLabel11.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.uiLabel11.Location = new System.Drawing.Point(265, 40);
            this.uiLabel11.Name = "uiLabel11";
            this.uiLabel11.Size = new System.Drawing.Size(89, 28);
            this.uiLabel11.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel11.TabIndex = 40;
            this.uiLabel11.Text = "3.点击确认";
            this.uiLabel11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel10
            // 
            this.uiLabel10.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.uiLabel10.Location = new System.Drawing.Point(131, 40);
            this.uiLabel10.Name = "uiLabel10";
            this.uiLabel10.Size = new System.Drawing.Size(128, 28);
            this.uiLabel10.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel10.TabIndex = 39;
            this.uiLabel10.Text = "2.设定目标重量";
            this.uiLabel10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel8
            // 
            this.uiLabel8.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.uiLabel8.Location = new System.Drawing.Point(8, 40);
            this.uiLabel8.Name = "uiLabel8";
            this.uiLabel8.Size = new System.Drawing.Size(89, 28);
            this.uiLabel8.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel8.TabIndex = 38;
            this.uiLabel8.Text = "1.选择位置";
            this.uiLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // set_weight
            // 
            this.set_weight.Cursor = System.Windows.Forms.Cursors.Hand;
            this.set_weight.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.set_weight.Location = new System.Drawing.Point(284, 166);
            this.set_weight.MinimumSize = new System.Drawing.Size(1, 1);
            this.set_weight.Name = "set_weight";
            this.set_weight.Size = new System.Drawing.Size(57, 82);
            this.set_weight.Style = Sunny.UI.UIStyle.Custom;
            this.set_weight.TabIndex = 37;
            this.set_weight.Text = "确认";
            this.set_weight.Click += new System.EventHandler(this.set_weight_Click);
            // 
            // text_B_train_full_weight
            // 
            this.text_B_train_full_weight.ButtonSymbol = 61761;
            this.text_B_train_full_weight.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.text_B_train_full_weight.FillColor = System.Drawing.Color.White;
            this.text_B_train_full_weight.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.text_B_train_full_weight.Location = new System.Drawing.Point(135, 212);
            this.text_B_train_full_weight.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.text_B_train_full_weight.Maximum = 2147483647D;
            this.text_B_train_full_weight.Minimum = -2147483648D;
            this.text_B_train_full_weight.MinimumSize = new System.Drawing.Size(1, 1);
            this.text_B_train_full_weight.Name = "text_B_train_full_weight";
            this.text_B_train_full_weight.Size = new System.Drawing.Size(110, 34);
            this.text_B_train_full_weight.Style = Sunny.UI.UIStyle.Custom;
            this.text_B_train_full_weight.TabIndex = 36;
            this.text_B_train_full_weight.Text = "0";
            this.text_B_train_full_weight.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel6
            // 
            this.uiLabel6.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel6.Location = new System.Drawing.Point(3, 216);
            this.uiLabel6.Name = "uiLabel6";
            this.uiLabel6.Size = new System.Drawing.Size(125, 23);
            this.uiLabel6.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel6.TabIndex = 35;
            this.uiLabel6.Text = "1号罐车总量：";
            this.uiLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // text_A_train_full_weight
            // 
            this.text_A_train_full_weight.ButtonSymbol = 61761;
            this.text_A_train_full_weight.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.text_A_train_full_weight.FillColor = System.Drawing.Color.White;
            this.text_A_train_full_weight.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.text_A_train_full_weight.Location = new System.Drawing.Point(135, 167);
            this.text_A_train_full_weight.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.text_A_train_full_weight.Maximum = 2147483647D;
            this.text_A_train_full_weight.Minimum = -2147483648D;
            this.text_A_train_full_weight.MinimumSize = new System.Drawing.Size(1, 1);
            this.text_A_train_full_weight.Name = "text_A_train_full_weight";
            this.text_A_train_full_weight.Size = new System.Drawing.Size(110, 34);
            this.text_A_train_full_weight.Style = Sunny.UI.UIStyle.Custom;
            this.text_A_train_full_weight.TabIndex = 34;
            this.text_A_train_full_weight.Text = "0";
            this.text_A_train_full_weight.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel2
            // 
            this.uiLabel2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel2.Location = new System.Drawing.Point(8, 167);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(120, 23);
            this.uiLabel2.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel2.TabIndex = 33;
            this.uiLabel2.Text = "2号罐车总量：";
            this.uiLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // hum_chose_GB
            // 
            this.hum_chose_GB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hum_chose_GB.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.hum_chose_GB.Location = new System.Drawing.Point(269, 87);
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
            this.reqweight_uiTextBox1.DoubleValue = 285D;
            this.reqweight_uiTextBox1.FillColor = System.Drawing.Color.White;
            this.reqweight_uiTextBox1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.reqweight_uiTextBox1.IntValue = 285;
            this.reqweight_uiTextBox1.Location = new System.Drawing.Point(189, 87);
            this.reqweight_uiTextBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.reqweight_uiTextBox1.Maximum = 2147483647D;
            this.reqweight_uiTextBox1.Minimum = -2147483648D;
            this.reqweight_uiTextBox1.MinimumSize = new System.Drawing.Size(1, 1);
            this.reqweight_uiTextBox1.Name = "reqweight_uiTextBox1";
            this.reqweight_uiTextBox1.Size = new System.Drawing.Size(56, 34);
            this.reqweight_uiTextBox1.Style = Sunny.UI.UIStyle.Custom;
            this.reqweight_uiTextBox1.TabIndex = 32;
            this.reqweight_uiTextBox1.Text = "285";
            this.reqweight_uiTextBox1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.reqweight_uiTextBox1.TextChanged += new System.EventHandler(this.reqweight_uiTextBox1_TextChanged);
            // 
            // B_chose
            // 
            this.B_chose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.B_chose.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.B_chose.Location = new System.Drawing.Point(12, 111);
            this.B_chose.MinimumSize = new System.Drawing.Size(1, 1);
            this.B_chose.Name = "B_chose";
            this.B_chose.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.B_chose.Size = new System.Drawing.Size(101, 29);
            this.B_chose.Style = Sunny.UI.UIStyle.Custom;
            this.B_chose.TabIndex = 31;
            this.B_chose.Text = "1号位";
            // 
            // A_chose
            // 
            this.A_chose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.A_chose.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.A_chose.Location = new System.Drawing.Point(12, 71);
            this.A_chose.MinimumSize = new System.Drawing.Size(1, 1);
            this.A_chose.Name = "A_chose";
            this.A_chose.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.A_chose.Size = new System.Drawing.Size(104, 29);
            this.A_chose.Style = Sunny.UI.UIStyle.Custom;
            this.A_chose.TabIndex = 28;
            this.A_chose.Text = "2号位";
            // 
            // uiLabel9
            // 
            this.uiLabel9.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel9.Location = new System.Drawing.Point(138, 71);
            this.uiLabel9.Name = "uiLabel9";
            this.uiLabel9.Size = new System.Drawing.Size(60, 59);
            this.uiLabel9.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel9.TabIndex = 29;
            this.uiLabel9.Text = "目标\r\n重量";
            this.uiLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiTitlePanel1
            // 
            this.uiTitlePanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.uiTitlePanel1.Controls.Add(this.uiSymbolButton2);
            this.uiTitlePanel1.Controls.Add(this.uiLabel5);
            this.uiTitlePanel1.Controls.Add(this.uiSymbolButton1);
            this.uiTitlePanel1.Controls.Add(this.uiLabel1);
            this.uiTitlePanel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiTitlePanel1.Location = new System.Drawing.Point(1, 256);
            this.uiTitlePanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiTitlePanel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiTitlePanel1.Name = "uiTitlePanel1";
            this.uiTitlePanel1.Padding = new System.Windows.Forms.Padding(0, 35, 0, 0);
            this.uiTitlePanel1.RectColor = System.Drawing.Color.Transparent;
            this.uiTitlePanel1.Size = new System.Drawing.Size(354, 144);
            this.uiTitlePanel1.Style = Sunny.UI.UIStyle.Custom;
            this.uiTitlePanel1.StyleCustomMode = true;
            this.uiTitlePanel1.TabIndex = 45;
            this.uiTitlePanel1.Text = "折铁控制";
            this.uiTitlePanel1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.uiTitlePanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiTitlePanel1.TitleColor = System.Drawing.Color.Beige;
            this.uiTitlePanel1.TitleForeColor = System.Drawing.Color.Brown;
            // 
            // uiTitlePanel3
            // 
            this.uiTitlePanel3.Controls.Add(this.uiTabControl1);
            this.uiTitlePanel3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiTitlePanel3.Location = new System.Drawing.Point(0, 410);
            this.uiTitlePanel3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiTitlePanel3.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiTitlePanel3.Name = "uiTitlePanel3";
            this.uiTitlePanel3.Size = new System.Drawing.Size(358, 225);
            this.uiTitlePanel3.TabIndex = 46;
            this.uiTitlePanel3.Text = null;
            this.uiTitlePanel3.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.uiTitlePanel3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiTitlePanel3.TitleHeight = 0;
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
            this.uiTabControl1.Size = new System.Drawing.Size(358, 225);
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
            this.tabPage4.Size = new System.Drawing.Size(358, 190);
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
            this.listView1.Size = new System.Drawing.Size(358, 190);
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
            this.tabPage5.Location = new System.Drawing.Point(0, 35);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(358, 190);
            this.tabPage5.TabIndex = 1;
            this.tabPage5.Text = "折铁警报";
            // 
            // uiTitlePanel4
            // 
            this.uiTitlePanel4.Controls.Add(this.listView4);
            this.uiTitlePanel4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.uiTitlePanel4.Location = new System.Drawing.Point(0, 756);
            this.uiTitlePanel4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiTitlePanel4.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiTitlePanel4.Name = "uiTitlePanel4";
            this.uiTitlePanel4.Padding = new System.Windows.Forms.Padding(0, 35, 0, 0);
            this.uiTitlePanel4.Size = new System.Drawing.Size(361, 198);
            this.uiTitlePanel4.TabIndex = 46;
            this.uiTitlePanel4.Text = "折铁警示";
            this.uiTitlePanel4.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.uiTitlePanel4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MdiParent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1643, 959);
            this.Controls.Add(this.uiTitlePanel4);
            this.Controls.Add(this.uiPanel1);
            this.Controls.Add(this.uiNavBar1);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.menuStrip1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(1700, 1100);
            this.Name = "MdiParent";
            this.ShowRadius = false;
            this.Text = "";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MdiParent_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MdiParent_FormClosed);
            this.Load += new System.EventHandler(this.MdiParent_Load);
            this.uiNavBar1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.uiPanel1.ResumeLayout(false);
            this.uiTitlePanel2.ResumeLayout(false);
            this.uiTitlePanel1.ResumeLayout(false);
            this.uiTitlePanel3.ResumeLayout(false);
            this.uiTabControl1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.uiTitlePanel4.ResumeLayout(false);
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
        private Sunny.UI.UILabel uiLabel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Sunny.UI.UILabel uiLabel4;
        private Sunny.UI.UILabel uiLabel3;
        private Sunny.UI.UILabel uiLabel5;
        private Sunny.UI.UISymbolButton uiSymbolButton1;
        private Sunny.UI.UISymbolButton uiSymbolButton2;
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
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private Sunny.UI.UIButton set_weight;
        private Sunny.UI.UITextBox text_B_train_full_weight;
        private Sunny.UI.UILabel uiLabel6;
        private Sunny.UI.UITextBox text_A_train_full_weight;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UILabel uiLabel11;
        private Sunny.UI.UILabel uiLabel10;
        private Sunny.UI.UILabel uiLabel8;
        private Sunny.UI.UILabel uiLabel7;
        private Sunny.UI.UILedBulb PLC_connect_state;
        private Sunny.UI.UITitlePanel uiTitlePanel4;
        private Sunny.UI.UITabControl uiTabControl1;
        private System.Windows.Forms.TabPage tabPage4;
        public System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.TabPage tabPage5;
    }
}