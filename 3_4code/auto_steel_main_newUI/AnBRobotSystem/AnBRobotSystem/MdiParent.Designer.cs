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
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("折铁流程");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("视觉图像");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("历史记录");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("实时数据");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MdiParent));
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.FreshTimer = new System.Windows.Forms.Timer(this.components);
            this.timerLog = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.uiNavBar1 = new Sunny.UI.UINavBar();
            this.PLC_connect_state = new Sunny.UI.UILedBulb();
            this.uiLabel9 = new Sunny.UI.UILabel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.uiLabel4 = new Sunny.UI.UILabel();
            this.uiLabel3 = new Sunny.UI.UILabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.set_weight = new Sunny.UI.UIButton();
            this.text_B_train_full_weight = new Sunny.UI.UITextBox();
            this.uiLabel6 = new Sunny.UI.UILabel();
            this.text_A_train_full_weight = new Sunny.UI.UITextBox();
            this.uiLabel2 = new Sunny.UI.UILabel();
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
            this.uiTitlePanel4 = new Sunny.UI.UITitlePanel();
            this.uiTitlePanel1 = new Sunny.UI.UITitlePanel();
            this.label1 = new System.Windows.Forms.Label();
            this.uiLabel5 = new Sunny.UI.UILabel();
            this.uiSymbolButton3 = new Sunny.UI.UISymbolButton();
            this.uiLabel13 = new Sunny.UI.UILabel();
            this.uiSymbolButton2 = new Sunny.UI.UISymbolButton();
            this.uiLabel12 = new Sunny.UI.UILabel();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.startA_ZT = new Sunny.UI.UISymbolButton();
            this.startB_ZT = new Sunny.UI.UISymbolButton();
            this.uiTitlePanel3 = new Sunny.UI.UITitlePanel();
            this.uiTitlePanel2 = new Sunny.UI.UITitlePanel();
            this.uiLabel8 = new Sunny.UI.UILabel();
            this.uiLabel11 = new Sunny.UI.UILabel();
            this.uiLabel10 = new Sunny.UI.UILabel();
            this.uiLabel7 = new Sunny.UI.UILabel();
            this.hum_chose_GB = new Sunny.UI.UIButton();
            this.reqweight_uiTextBox1 = new Sunny.UI.UITextBox();
            this.uiButton1 = new Sunny.UI.UIButton();
            this.uiNavBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.uiTabControl1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.uiPanel1.SuspendLayout();
            this.uiTitlePanel4.SuspendLayout();
            this.uiTitlePanel1.SuspendLayout();
            this.uiTitlePanel3.SuspendLayout();
            this.uiTitlePanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 949);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1643, 10);
            this.splitter1.TabIndex = 8;
            this.splitter1.TabStop = false;
            // 
            // splitter2
            // 
            this.splitter2.Location = new System.Drawing.Point(0, 35);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 914);
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
            this.uiNavBar1.Controls.Add(this.PLC_connect_state);
            this.uiNavBar1.Controls.Add(this.uiLabel9);
            this.uiNavBar1.Controls.Add(this.button2);
            this.uiNavBar1.Controls.Add(this.button1);
            this.uiNavBar1.Controls.Add(this.uiLabel4);
            this.uiNavBar1.Controls.Add(this.uiLabel3);
            this.uiNavBar1.Controls.Add(this.pictureBox1);
            this.uiNavBar1.Controls.Add(this.set_weight);
            this.uiNavBar1.Controls.Add(this.text_B_train_full_weight);
            this.uiNavBar1.Controls.Add(this.uiLabel6);
            this.uiNavBar1.Controls.Add(this.text_A_train_full_weight);
            this.uiNavBar1.Controls.Add(this.uiLabel2);
            this.uiNavBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiNavBar1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiNavBar1.ForeColor = System.Drawing.Color.Black;
            this.uiNavBar1.Location = new System.Drawing.Point(3, 35);
            this.uiNavBar1.MenuHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.uiNavBar1.MenuSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.uiNavBar1.MenuStyle = Sunny.UI.UIMenuStyle.Custom;
            this.uiNavBar1.Name = "uiNavBar1";
            this.uiNavBar1.NodeInterval = 50;
            treeNode5.BackColor = System.Drawing.Color.White;
            treeNode5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            treeNode5.Name = "折铁流程";
            treeNode5.NodeFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            treeNode5.Text = "折铁流程";
            treeNode6.Name = "视觉图像";
            treeNode6.Text = "视觉图像";
            treeNode7.Name = "历史记录";
            treeNode7.Text = "历史记录";
            treeNode8.Name = "实时数据";
            treeNode8.Text = "实时数据";
            this.uiNavBar1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8});
            this.uiNavBar1.NodeSize = new System.Drawing.Size(120, 40);
            this.uiNavBar1.Size = new System.Drawing.Size(1640, 87);
            this.uiNavBar1.TabIndex = 28;
            this.uiNavBar1.Text = "uiNavBar1";
            this.uiNavBar1.NodeMouseClick += new Sunny.UI.UINavBar.OnNodeMouseClick(this.uiNavBar1_NodeMouseClick);
            // 
            // PLC_connect_state
            // 
            this.PLC_connect_state.Color = System.Drawing.Color.Red;
            this.PLC_connect_state.Location = new System.Drawing.Point(859, 24);
            this.PLC_connect_state.Name = "PLC_connect_state";
            this.PLC_connect_state.Size = new System.Drawing.Size(61, 38);
            this.PLC_connect_state.TabIndex = 100;
            this.PLC_connect_state.Text = "uiLedBulb3";
            // 
            // uiLabel9
            // 
            this.uiLabel9.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Bold);
            this.uiLabel9.Location = new System.Drawing.Point(720, 24);
            this.uiLabel9.Name = "uiLabel9";
            this.uiLabel9.Size = new System.Drawing.Size(160, 34);
            this.uiLabel9.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel9.TabIndex = 99;
            this.uiLabel9.Text = "一级通讯：";
            this.uiLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(424, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(104, 37);
            this.button2.TabIndex = 82;
            this.button2.Text = "gk";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(424, 46);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 37);
            this.button1.TabIndex = 81;
            this.button1.Text = "tb";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
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
            // set_weight
            // 
            this.set_weight.Cursor = System.Windows.Forms.Cursors.Hand;
            this.set_weight.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.set_weight.Location = new System.Drawing.Point(704, 18);
            this.set_weight.MinimumSize = new System.Drawing.Size(1, 1);
            this.set_weight.Name = "set_weight";
            this.set_weight.Size = new System.Drawing.Size(57, 39);
            this.set_weight.Style = Sunny.UI.UIStyle.Custom;
            this.set_weight.TabIndex = 37;
            this.set_weight.Text = "确认";
            this.set_weight.Visible = false;
            this.set_weight.Click += new System.EventHandler(this.set_weight_Click);
            // 
            // text_B_train_full_weight
            // 
            this.text_B_train_full_weight.ButtonSymbol = 61761;
            this.text_B_train_full_weight.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.text_B_train_full_weight.FillColor = System.Drawing.Color.White;
            this.text_B_train_full_weight.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.text_B_train_full_weight.Location = new System.Drawing.Point(668, 48);
            this.text_B_train_full_weight.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.text_B_train_full_weight.Maximum = 2147483647D;
            this.text_B_train_full_weight.Minimum = -2147483648D;
            this.text_B_train_full_weight.MinimumSize = new System.Drawing.Size(1, 1);
            this.text_B_train_full_weight.Name = "text_B_train_full_weight";
            this.text_B_train_full_weight.Size = new System.Drawing.Size(28, 34);
            this.text_B_train_full_weight.Style = Sunny.UI.UIStyle.Custom;
            this.text_B_train_full_weight.TabIndex = 36;
            this.text_B_train_full_weight.Text = "0";
            this.text_B_train_full_weight.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.text_B_train_full_weight.Visible = false;
            // 
            // uiLabel6
            // 
            this.uiLabel6.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel6.Location = new System.Drawing.Point(553, 48);
            this.uiLabel6.Name = "uiLabel6";
            this.uiLabel6.Size = new System.Drawing.Size(153, 23);
            this.uiLabel6.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel6.TabIndex = 35;
            this.uiLabel6.Text = "3号罐车总量：";
            this.uiLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel6.Visible = false;
            // 
            // text_A_train_full_weight
            // 
            this.text_A_train_full_weight.ButtonSymbol = 61761;
            this.text_A_train_full_weight.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.text_A_train_full_weight.FillColor = System.Drawing.Color.White;
            this.text_A_train_full_weight.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.text_A_train_full_weight.Location = new System.Drawing.Point(668, 9);
            this.text_A_train_full_weight.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.text_A_train_full_weight.Maximum = 2147483647D;
            this.text_A_train_full_weight.Minimum = -2147483648D;
            this.text_A_train_full_weight.MinimumSize = new System.Drawing.Size(1, 1);
            this.text_A_train_full_weight.Name = "text_A_train_full_weight";
            this.text_A_train_full_weight.Size = new System.Drawing.Size(28, 34);
            this.text_A_train_full_weight.Style = Sunny.UI.UIStyle.Custom;
            this.text_A_train_full_weight.TabIndex = 34;
            this.text_A_train_full_weight.Text = "0";
            this.text_A_train_full_weight.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.text_A_train_full_weight.Visible = false;
            // 
            // uiLabel2
            // 
            this.uiLabel2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel2.Location = new System.Drawing.Point(550, 9);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(148, 23);
            this.uiLabel2.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel2.TabIndex = 33;
            this.uiLabel2.Text = "4号罐车总量：";
            this.uiLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel2.Visible = false;
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
            this.uiTabControl1.Size = new System.Drawing.Size(358, 239);
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
            this.tabPage4.Size = new System.Drawing.Size(358, 204);
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
            this.listView1.Size = new System.Drawing.Size(358, 204);
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
            this.tabPage5.Size = new System.Drawing.Size(358, 204);
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
            this.listView4.ForeColor = System.Drawing.Color.DeepPink;
            this.listView4.FullRowSelect = true;
            this.listView4.GridLines = true;
            this.listView4.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView4.HideSelection = false;
            this.listView4.Location = new System.Drawing.Point(0, 35);
            this.listView4.MultiSelect = false;
            this.listView4.Name = "listView4";
            this.listView4.ShowItemToolTips = true;
            this.listView4.Size = new System.Drawing.Size(361, 205);
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
            this.alarm_timer.Interval = 1000;
            this.alarm_timer.Tick += new System.EventHandler(this.alarm_timer_Tick);
            // 
            // uiPanel1
            // 
            this.uiPanel1.Controls.Add(this.uiTitlePanel4);
            this.uiPanel1.Controls.Add(this.uiTitlePanel1);
            this.uiPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.uiPanel1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiPanel1.Location = new System.Drawing.Point(3, 122);
            this.uiPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel1.Name = "uiPanel1";
            this.uiPanel1.Size = new System.Drawing.Size(358, 827);
            this.uiPanel1.TabIndex = 44;
            this.uiPanel1.Text = null;
            this.uiPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiTitlePanel4
            // 
            this.uiTitlePanel4.Controls.Add(this.listView4);
            this.uiTitlePanel4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.uiTitlePanel4.Location = new System.Drawing.Point(-3, 592);
            this.uiTitlePanel4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiTitlePanel4.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiTitlePanel4.Name = "uiTitlePanel4";
            this.uiTitlePanel4.Padding = new System.Windows.Forms.Padding(0, 35, 0, 0);
            this.uiTitlePanel4.Size = new System.Drawing.Size(361, 240);
            this.uiTitlePanel4.TabIndex = 47;
            this.uiTitlePanel4.Text = "折铁警示";
            this.uiTitlePanel4.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.uiTitlePanel4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiTitlePanel1
            // 
            this.uiTitlePanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.uiTitlePanel1.Controls.Add(this.label1);
            this.uiTitlePanel1.Controls.Add(this.uiLabel5);
            this.uiTitlePanel1.Controls.Add(this.uiSymbolButton3);
            this.uiTitlePanel1.Controls.Add(this.uiLabel13);
            this.uiTitlePanel1.Controls.Add(this.uiSymbolButton2);
            this.uiTitlePanel1.Controls.Add(this.uiLabel12);
            this.uiTitlePanel1.Controls.Add(this.uiLabel1);
            this.uiTitlePanel1.Controls.Add(this.startA_ZT);
            this.uiTitlePanel1.Controls.Add(this.startB_ZT);
            this.uiTitlePanel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiTitlePanel1.Location = new System.Drawing.Point(1, 165);
            this.uiTitlePanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiTitlePanel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiTitlePanel1.Name = "uiTitlePanel1";
            this.uiTitlePanel1.Padding = new System.Windows.Forms.Padding(0, 35, 0, 0);
            this.uiTitlePanel1.RectColor = System.Drawing.Color.Transparent;
            this.uiTitlePanel1.Size = new System.Drawing.Size(358, 168);
            this.uiTitlePanel1.Style = Sunny.UI.UIStyle.Custom;
            this.uiTitlePanel1.StyleCustomMode = true;
            this.uiTitlePanel1.TabIndex = 45;
            this.uiTitlePanel1.Text = "折铁控制";
            this.uiTitlePanel1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.uiTitlePanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiTitlePanel1.TitleColor = System.Drawing.Color.Beige;
            this.uiTitlePanel1.TitleForeColor = System.Drawing.Color.Brown;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(165, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 154);
            this.label1.TabIndex = 58;
            this.label1.Text = "|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|";
            // 
            // uiLabel5
            // 
            this.uiLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiLabel5.Location = new System.Drawing.Point(88, 119);
            this.uiLabel5.Name = "uiLabel5";
            this.uiLabel5.Size = new System.Drawing.Size(85, 29);
            this.uiLabel5.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel5.TabIndex = 57;
            this.uiLabel5.Text = "3号退出";
            this.uiLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiSymbolButton3
            // 
            this.uiSymbolButton3.CircleRectWidth = 5;
            this.uiSymbolButton3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiSymbolButton3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiSymbolButton3.ImageInterval = 3;
            this.uiSymbolButton3.Location = new System.Drawing.Point(94, 49);
            this.uiSymbolButton3.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolButton3.Name = "uiSymbolButton3";
            this.uiSymbolButton3.Radius = 40;
            this.uiSymbolButton3.Size = new System.Drawing.Size(54, 41);
            this.uiSymbolButton3.Style = Sunny.UI.UIStyle.Custom;
            this.uiSymbolButton3.Symbol = 61516;
            this.uiSymbolButton3.SymbolSize = 40;
            this.uiSymbolButton3.TabIndex = 56;
            this.uiSymbolButton3.TagString = "";
            this.uiSymbolButton3.Click += new System.EventHandler(this.uiSymbolButton3_Click);
            // 
            // uiLabel13
            // 
            this.uiLabel13.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiLabel13.Location = new System.Drawing.Point(273, 119);
            this.uiLabel13.Name = "uiLabel13";
            this.uiLabel13.Size = new System.Drawing.Size(85, 29);
            this.uiLabel13.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel13.TabIndex = 55;
            this.uiLabel13.Text = "4号退出";
            this.uiLabel13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiSymbolButton2
            // 
            this.uiSymbolButton2.CircleRectWidth = 5;
            this.uiSymbolButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiSymbolButton2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiSymbolButton2.ImageInterval = 3;
            this.uiSymbolButton2.Location = new System.Drawing.Point(284, 49);
            this.uiSymbolButton2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolButton2.Name = "uiSymbolButton2";
            this.uiSymbolButton2.Radius = 40;
            this.uiSymbolButton2.Size = new System.Drawing.Size(54, 41);
            this.uiSymbolButton2.Style = Sunny.UI.UIStyle.Custom;
            this.uiSymbolButton2.Symbol = 61516;
            this.uiSymbolButton2.SymbolSize = 40;
            this.uiSymbolButton2.TabIndex = 54;
            this.uiSymbolButton2.TagString = "";
            this.uiSymbolButton2.Click += new System.EventHandler(this.uiSymbolButton2_Click);
            // 
            // uiLabel12
            // 
            this.uiLabel12.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel12.Location = new System.Drawing.Point(186, 119);
            this.uiLabel12.Name = "uiLabel12";
            this.uiLabel12.Size = new System.Drawing.Size(87, 29);
            this.uiLabel12.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel12.TabIndex = 53;
            this.uiLabel12.Text = "4号折铁";
            this.uiLabel12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel1
            // 
            this.uiLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel1.Location = new System.Drawing.Point(-1, 119);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(86, 29);
            this.uiLabel1.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel1.TabIndex = 52;
            this.uiLabel1.Text = "3号折铁";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // startA_ZT
            // 
            this.startA_ZT.CircleRectWidth = 5;
            this.startA_ZT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.startA_ZT.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.startA_ZT.ImageInterval = 3;
            this.startA_ZT.Location = new System.Drawing.Point(199, 49);
            this.startA_ZT.MinimumSize = new System.Drawing.Size(1, 1);
            this.startA_ZT.Name = "startA_ZT";
            this.startA_ZT.Radius = 40;
            this.startA_ZT.Size = new System.Drawing.Size(61, 41);
            this.startA_ZT.Style = Sunny.UI.UIStyle.Custom;
            this.startA_ZT.Symbol = 61515;
            this.startA_ZT.SymbolSize = 40;
            this.startA_ZT.TabIndex = 51;
            this.startA_ZT.TagString = "";
            this.startA_ZT.Click += new System.EventHandler(this.startA_ZT_Click);
            // 
            // startB_ZT
            // 
            this.startB_ZT.CircleRectWidth = 5;
            this.startB_ZT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.startB_ZT.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.startB_ZT.ImageInterval = 3;
            this.startB_ZT.Location = new System.Drawing.Point(6, 49);
            this.startB_ZT.MinimumSize = new System.Drawing.Size(1, 1);
            this.startB_ZT.Name = "startB_ZT";
            this.startB_ZT.Radius = 40;
            this.startB_ZT.Size = new System.Drawing.Size(53, 41);
            this.startB_ZT.Style = Sunny.UI.UIStyle.Custom;
            this.startB_ZT.Symbol = 61515;
            this.startB_ZT.SymbolSize = 40;
            this.startB_ZT.TabIndex = 49;
            this.startB_ZT.TagString = "";
            this.startB_ZT.Click += new System.EventHandler(this.startB_ZT_Click);
            // 
            // uiTitlePanel3
            // 
            this.uiTitlePanel3.Controls.Add(this.uiTabControl1);
            this.uiTitlePanel3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiTitlePanel3.Location = new System.Drawing.Point(0, 465);
            this.uiTitlePanel3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiTitlePanel3.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiTitlePanel3.Name = "uiTitlePanel3";
            this.uiTitlePanel3.Size = new System.Drawing.Size(358, 239);
            this.uiTitlePanel3.TabIndex = 46;
            this.uiTitlePanel3.Text = null;
            this.uiTitlePanel3.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.uiTitlePanel3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiTitlePanel3.TitleHeight = 0;
            // 
            // uiTitlePanel2
            // 
            this.uiTitlePanel2.Controls.Add(this.uiLabel8);
            this.uiTitlePanel2.Controls.Add(this.uiLabel11);
            this.uiTitlePanel2.Controls.Add(this.uiLabel10);
            this.uiTitlePanel2.Controls.Add(this.uiLabel7);
            this.uiTitlePanel2.Controls.Add(this.hum_chose_GB);
            this.uiTitlePanel2.Controls.Add(this.reqweight_uiTextBox1);
            this.uiTitlePanel2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiTitlePanel2.Location = new System.Drawing.Point(3, 124);
            this.uiTitlePanel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiTitlePanel2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiTitlePanel2.Name = "uiTitlePanel2";
            this.uiTitlePanel2.Padding = new System.Windows.Forms.Padding(0, 35, 0, 0);
            this.uiTitlePanel2.RectColor = System.Drawing.Color.Transparent;
            this.uiTitlePanel2.Size = new System.Drawing.Size(358, 161);
            this.uiTitlePanel2.Style = Sunny.UI.UIStyle.Custom;
            this.uiTitlePanel2.StyleCustomMode = true;
            this.uiTitlePanel2.TabIndex = 45;
            this.uiTitlePanel2.Text = "折铁交互";
            this.uiTitlePanel2.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.uiTitlePanel2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiTitlePanel2.TitleColor = System.Drawing.Color.Beige;
            this.uiTitlePanel2.TitleForeColor = System.Drawing.Color.Brown;
            // 
            // uiLabel8
            // 
            this.uiLabel8.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.uiLabel8.Location = new System.Drawing.Point(254, 45);
            this.uiLabel8.Name = "uiLabel8";
            this.uiLabel8.Size = new System.Drawing.Size(105, 56);
            this.uiLabel8.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel8.TabIndex = 44;
            this.uiLabel8.Text = "3.点击对应 \r\n  折铁按钮";
            this.uiLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel11
            // 
            this.uiLabel11.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.uiLabel11.Location = new System.Drawing.Point(145, 45);
            this.uiLabel11.Name = "uiLabel11";
            this.uiLabel11.Size = new System.Drawing.Size(89, 28);
            this.uiLabel11.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel11.TabIndex = 43;
            this.uiLabel11.Text = "2.点击确认";
            this.uiLabel11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel10
            // 
            this.uiLabel10.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.uiLabel10.Location = new System.Drawing.Point(6, 45);
            this.uiLabel10.Name = "uiLabel10";
            this.uiLabel10.Size = new System.Drawing.Size(128, 28);
            this.uiLabel10.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel10.TabIndex = 42;
            this.uiLabel10.Text = "1.设定目标重量";
            this.uiLabel10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel7
            // 
            this.uiLabel7.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel7.Location = new System.Drawing.Point(13, 77);
            this.uiLabel7.Name = "uiLabel7";
            this.uiLabel7.Size = new System.Drawing.Size(51, 59);
            this.uiLabel7.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel7.TabIndex = 38;
            this.uiLabel7.Text = "目标\r\n重量";
            this.uiLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // hum_chose_GB
            // 
            this.hum_chose_GB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hum_chose_GB.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.hum_chose_GB.Location = new System.Drawing.Point(149, 90);
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
            this.reqweight_uiTextBox1.Location = new System.Drawing.Point(71, 91);
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
            // uiButton1
            // 
            this.uiButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiButton1.Location = new System.Drawing.Point(1611, -5);
            this.uiButton1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton1.Name = "uiButton1";
            this.uiButton1.Size = new System.Drawing.Size(32, 43);
            this.uiButton1.TabIndex = 45;
            this.uiButton1.Text = "×";
            this.uiButton1.Visible = false;
            this.uiButton1.Click += new System.EventHandler(this.uiButton1_Click_1);
            // 
            // MdiParent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1643, 959);
            this.Controls.Add(this.uiButton1);
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
            this.MaximumSize = new System.Drawing.Size(1920, 1100);
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
            this.uiPanel1.ResumeLayout(false);
            this.uiTitlePanel4.ResumeLayout(false);
            this.uiTitlePanel1.ResumeLayout(false);
            this.uiTitlePanel1.PerformLayout();
            this.uiTitlePanel3.ResumeLayout(false);
            this.uiTitlePanel2.ResumeLayout(false);
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
        private System.Windows.Forms.PictureBox pictureBox1;
        private Sunny.UI.UILabel uiLabel4;
        private Sunny.UI.UILabel uiLabel3;
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
        public System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private Sunny.UI.UIButton set_weight;
        private Sunny.UI.UITextBox text_B_train_full_weight;
        private Sunny.UI.UILabel uiLabel6;
        private Sunny.UI.UITextBox text_A_train_full_weight;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UILabel uiLabel11;
        private Sunny.UI.UILabel uiLabel10;
        private Sunny.UI.UILabel uiLabel7;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private Sunny.UI.UILedBulb PLC_connect_state;
        private Sunny.UI.UILabel uiLabel9;
        private Sunny.UI.UITitlePanel uiTitlePanel4;
        private Sunny.UI.UISymbolButton startB_ZT;
        private Sunny.UI.UILabel uiLabel12;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UISymbolButton startA_ZT;
        private Sunny.UI.UILabel uiLabel13;
        private Sunny.UI.UISymbolButton uiSymbolButton2;
        private Sunny.UI.UISymbolButton uiSymbolButton3;
        private Sunny.UI.UILabel uiLabel5;
        private Sunny.UI.UILabel uiLabel8;
        private System.Windows.Forms.Label label1;
        private Sunny.UI.UIButton uiButton1;
    }
}