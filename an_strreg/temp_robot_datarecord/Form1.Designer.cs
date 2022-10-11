namespace temp_robot_datarecord
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.listViewLog = new System.Windows.Forms.ListView();
            this.logTimeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.logMessageHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.vmRenderControl1 = new VMControls.Winform.Release.VmRenderControl();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonRunOnce = new System.Windows.Forms.Button();
            this.buttonSaveSolu = new System.Windows.Forms.Button();
            this.buttonLoadSolu = new System.Windows.Forms.Button();
            this.uiPanel1 = new Sunny.UI.UIPanel();
            this.groupBox1.SuspendLayout();
            this.uiPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // listViewLog
            // 
            this.listViewLog.AutoArrange = false;
            this.listViewLog.BackColor = System.Drawing.Color.Silver;
            this.listViewLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.logTimeHeader,
            this.logMessageHeader});
            this.listViewLog.ForeColor = System.Drawing.Color.White;
            this.listViewLog.HideSelection = false;
            this.listViewLog.Location = new System.Drawing.Point(780, 175);
            this.listViewLog.Name = "listViewLog";
            this.listViewLog.Size = new System.Drawing.Size(374, 502);
            this.listViewLog.TabIndex = 1;
            this.listViewLog.UseCompatibleStateImageBehavior = false;
            this.listViewLog.View = System.Windows.Forms.View.Details;
            // 
            // logTimeHeader
            // 
            this.logTimeHeader.Text = "时间";
            this.logTimeHeader.Width = 152;
            // 
            // logMessageHeader
            // 
            this.logMessageHeader.Text = "消息";
            this.logMessageHeader.Width = 250;
            // 
            // vmRenderControl1
            // 
            this.vmRenderControl1.BackColor = System.Drawing.Color.LightGray;
            this.vmRenderControl1.CoordinateInfoVisible = true;
            this.vmRenderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vmRenderControl1.ImageSource = null;
            this.vmRenderControl1.Location = new System.Drawing.Point(0, 0);
            this.vmRenderControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.vmRenderControl1.ModuleSource = null;
            this.vmRenderControl1.Name = "vmRenderControl1";
            this.vmRenderControl1.Size = new System.Drawing.Size(555, 439);
            this.vmRenderControl1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 30F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(129, 104);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(257, 40);
            this.label1.TabIndex = 12;
            this.label1.Text = "1#线识别图像";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.groupBox1.Controls.Add(this.buttonRunOnce);
            this.groupBox1.Controls.Add(this.buttonSaveSolu);
            this.groupBox1.Controls.Add(this.buttonLoadSolu);
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(780, 79);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(374, 90);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "方案操作";
            // 
            // buttonRunOnce
            // 
            this.buttonRunOnce.BackColor = System.Drawing.SystemColors.GrayText;
            this.buttonRunOnce.FlatAppearance.BorderSize = 0;
            this.buttonRunOnce.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRunOnce.ForeColor = System.Drawing.Color.White;
            this.buttonRunOnce.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonRunOnce.Location = new System.Drawing.Point(274, 32);
            this.buttonRunOnce.Name = "buttonRunOnce";
            this.buttonRunOnce.Size = new System.Drawing.Size(91, 41);
            this.buttonRunOnce.TabIndex = 6;
            this.buttonRunOnce.Text = "单次运行";
            this.buttonRunOnce.UseVisualStyleBackColor = false;
            this.buttonRunOnce.Click += new System.EventHandler(this.buttonRunOnce_Click);
            // 
            // buttonSaveSolu
            // 
            this.buttonSaveSolu.BackColor = System.Drawing.SystemColors.GrayText;
            this.buttonSaveSolu.FlatAppearance.BorderSize = 0;
            this.buttonSaveSolu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSaveSolu.ForeColor = System.Drawing.Color.White;
            this.buttonSaveSolu.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonSaveSolu.Location = new System.Drawing.Point(148, 32);
            this.buttonSaveSolu.Name = "buttonSaveSolu";
            this.buttonSaveSolu.Size = new System.Drawing.Size(91, 41);
            this.buttonSaveSolu.TabIndex = 8;
            this.buttonSaveSolu.Text = "保存方案";
            this.buttonSaveSolu.UseVisualStyleBackColor = false;
            // 
            // buttonLoadSolu
            // 
            this.buttonLoadSolu.BackColor = System.Drawing.SystemColors.GrayText;
            this.buttonLoadSolu.FlatAppearance.BorderSize = 0;
            this.buttonLoadSolu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLoadSolu.ForeColor = System.Drawing.Color.White;
            this.buttonLoadSolu.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonLoadSolu.Location = new System.Drawing.Point(33, 32);
            this.buttonLoadSolu.Name = "buttonLoadSolu";
            this.buttonLoadSolu.Size = new System.Drawing.Size(91, 41);
            this.buttonLoadSolu.TabIndex = 3;
            this.buttonLoadSolu.Text = "加载方案";
            this.buttonLoadSolu.UseVisualStyleBackColor = false;
            this.buttonLoadSolu.Click += new System.EventHandler(this.buttonLoadSolu_Click);
            // 
            // uiPanel1
            // 
            this.uiPanel1.Controls.Add(this.vmRenderControl1);
            this.uiPanel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPanel1.Location = new System.Drawing.Point(35, 162);
            this.uiPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel1.Name = "uiPanel1";
            this.uiPanel1.Size = new System.Drawing.Size(555, 439);
            this.uiPanel1.TabIndex = 14;
            this.uiPanel1.Text = "uiPanel1";
            this.uiPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiPanel1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1219, 690);
            this.Controls.Add(this.uiPanel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listViewLog);
            this.Name = "Form1";
            this.Text = "大型厂探伤修磨线字符识别";
            this.ZoomScaleRect = new System.Drawing.Rectangle(15, 15, 1219, 571);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.uiPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ListView listViewLog;
        private System.Windows.Forms.ColumnHeader logTimeHeader;
        private System.Windows.Forms.ColumnHeader logMessageHeader;
        private VMControls.Winform.Release.VmRenderControl vmRenderControl1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonRunOnce;
        private System.Windows.Forms.Button buttonSaveSolu;
        private System.Windows.Forms.Button buttonLoadSolu;
        private Sunny.UI.UIPanel uiPanel1;
    }
}

