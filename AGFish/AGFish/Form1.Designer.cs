namespace AGFish
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.timer_deleterizhi = new System.Windows.Forms.Timer(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txt_message = new System.Windows.Forms.TextBox();
            this.buttonOpenVM = new System.Windows.Forms.Button();
            this.buttonCloseVM = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.buttonCloseSolution = new System.Windows.Forms.Button();
            this.groupBoxShowAndHide = new System.Windows.Forms.GroupBox();
            this.comboBoxShowAndHide = new System.Windows.Forms.ComboBox();
            this.progressBarSaveAndLoad = new System.Windows.Forms.ProgressBar();
            this.labelProgress = new System.Windows.Forms.Label();
            this.buttonLoadSolution = new System.Windows.Forms.Button();
            this.buttonShowHideVM = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Recognition = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_count1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_sdzs1 = new System.Windows.Forms.TextBox();
            this.LocationExecuteOnce = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBoxImg1 = new System.Windows.Forms.PictureBox();
            this.timer_savedata = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBoxShowAndHide.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImg1)).BeginInit();
            this.SuspendLayout();
            // 
            // timer_deleterizhi
            // 
            this.timer_deleterizhi.Enabled = true;
            this.timer_deleterizhi.Interval = 1000;
            this.timer_deleterizhi.Tick += new System.EventHandler(this.timer_deleterizhi_Tick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txt_message);
            this.splitContainer1.Panel1.Controls.Add(this.buttonOpenVM);
            this.splitContainer1.Panel1.Controls.Add(this.buttonCloseVM);
            this.splitContainer1.Panel1.Controls.Add(this.label11);
            this.splitContainer1.Panel1.Controls.Add(this.buttonCloseSolution);
            this.splitContainer1.Panel1.Controls.Add(this.groupBoxShowAndHide);
            this.splitContainer1.Panel1.Controls.Add(this.progressBarSaveAndLoad);
            this.splitContainer1.Panel1.Controls.Add(this.labelProgress);
            this.splitContainer1.Panel1.Controls.Add(this.buttonLoadSolution);
            this.splitContainer1.Panel1.Controls.Add(this.buttonShowHideVM);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label5);
            this.splitContainer1.Panel2.Controls.Add(this.pictureBox1);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.Recognition);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.txt_count1);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.txt_sdzs1);
            this.splitContainer1.Panel2.Controls.Add(this.LocationExecuteOnce);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2MinSize = 0;
            this.splitContainer1.Size = new System.Drawing.Size(1904, 1041);
            this.splitContainer1.SplitterDistance = 523;
            this.splitContainer1.TabIndex = 182;
            // 
            // txt_message
            // 
            this.txt_message.AcceptsReturn = true;
            this.txt_message.Font = new System.Drawing.Font("宋体", 12F);
            this.txt_message.Location = new System.Drawing.Point(24, 341);
            this.txt_message.Multiline = true;
            this.txt_message.Name = "txt_message";
            this.txt_message.Size = new System.Drawing.Size(468, 583);
            this.txt_message.TabIndex = 170;
            // 
            // buttonOpenVM
            // 
            this.buttonOpenVM.Font = new System.Drawing.Font("Cambria", 12F);
            this.buttonOpenVM.Location = new System.Drawing.Point(61, 96);
            this.buttonOpenVM.Name = "buttonOpenVM";
            this.buttonOpenVM.Size = new System.Drawing.Size(156, 44);
            this.buttonOpenVM.TabIndex = 5;
            this.buttonOpenVM.Text = "开启VisionMaster";
            this.buttonOpenVM.UseVisualStyleBackColor = true;
            this.buttonOpenVM.Click += new System.EventHandler(this.buttonOpenVM_Click);
            // 
            // buttonCloseVM
            // 
            this.buttonCloseVM.Font = new System.Drawing.Font("Cambria", 12F);
            this.buttonCloseVM.Location = new System.Drawing.Point(301, 96);
            this.buttonCloseVM.Name = "buttonCloseVM";
            this.buttonCloseVM.Size = new System.Drawing.Size(156, 44);
            this.buttonCloseVM.TabIndex = 6;
            this.buttonCloseVM.Text = "关闭VisionMaster";
            this.buttonCloseVM.UseVisualStyleBackColor = true;
            this.buttonCloseVM.Click += new System.EventHandler(this.buttonCloseVM_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 12F);
            this.label11.Location = new System.Drawing.Point(63, 228);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(56, 16);
            this.label11.TabIndex = 30;
            this.label11.Text = "进度：";
            // 
            // buttonCloseSolution
            // 
            this.buttonCloseSolution.Font = new System.Drawing.Font("宋体", 12F);
            this.buttonCloseSolution.Location = new System.Drawing.Point(301, 155);
            this.buttonCloseSolution.Name = "buttonCloseSolution";
            this.buttonCloseSolution.Size = new System.Drawing.Size(156, 44);
            this.buttonCloseSolution.TabIndex = 31;
            this.buttonCloseSolution.Text = "关闭方案";
            this.buttonCloseSolution.UseVisualStyleBackColor = true;
            this.buttonCloseSolution.Click += new System.EventHandler(this.buttonCloseSolution_Click);
            // 
            // groupBoxShowAndHide
            // 
            this.groupBoxShowAndHide.Controls.Add(this.comboBoxShowAndHide);
            this.groupBoxShowAndHide.Font = new System.Drawing.Font("宋体", 9F);
            this.groupBoxShowAndHide.Location = new System.Drawing.Point(61, 266);
            this.groupBoxShowAndHide.Name = "groupBoxShowAndHide";
            this.groupBoxShowAndHide.Size = new System.Drawing.Size(103, 42);
            this.groupBoxShowAndHide.TabIndex = 37;
            this.groupBoxShowAndHide.TabStop = false;
            this.groupBoxShowAndHide.Text = "显示隐藏状态";
            // 
            // comboBoxShowAndHide
            // 
            this.comboBoxShowAndHide.FormattingEnabled = true;
            this.comboBoxShowAndHide.Location = new System.Drawing.Point(6, 16);
            this.comboBoxShowAndHide.Name = "comboBoxShowAndHide";
            this.comboBoxShowAndHide.Size = new System.Drawing.Size(82, 20);
            this.comboBoxShowAndHide.TabIndex = 5;
            // 
            // progressBarSaveAndLoad
            // 
            this.progressBarSaveAndLoad.Location = new System.Drawing.Point(157, 224);
            this.progressBarSaveAndLoad.Name = "progressBarSaveAndLoad";
            this.progressBarSaveAndLoad.Size = new System.Drawing.Size(300, 24);
            this.progressBarSaveAndLoad.TabIndex = 32;
            // 
            // labelProgress
            // 
            this.labelProgress.AutoSize = true;
            this.labelProgress.Font = new System.Drawing.Font("宋体", 12F);
            this.labelProgress.Location = new System.Drawing.Point(125, 228);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new System.Drawing.Size(16, 16);
            this.labelProgress.TabIndex = 33;
            this.labelProgress.Text = "0";
            // 
            // buttonLoadSolution
            // 
            this.buttonLoadSolution.Font = new System.Drawing.Font("宋体", 12F);
            this.buttonLoadSolution.Location = new System.Drawing.Point(61, 155);
            this.buttonLoadSolution.Name = "buttonLoadSolution";
            this.buttonLoadSolution.Size = new System.Drawing.Size(156, 44);
            this.buttonLoadSolution.TabIndex = 34;
            this.buttonLoadSolution.Text = "加载方案";
            this.buttonLoadSolution.UseVisualStyleBackColor = true;
            this.buttonLoadSolution.Click += new System.EventHandler(this.buttonLoadSolution_Click);
            // 
            // buttonShowHideVM
            // 
            this.buttonShowHideVM.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonShowHideVM.Location = new System.Drawing.Point(187, 264);
            this.buttonShowHideVM.Name = "buttonShowHideVM";
            this.buttonShowHideVM.Size = new System.Drawing.Size(156, 44);
            this.buttonShowHideVM.TabIndex = 36;
            this.buttonShowHideVM.Text = "显示/隐藏 VisionMaster";
            this.buttonShowHideVM.UseVisualStyleBackColor = true;
            this.buttonShowHideVM.Click += new System.EventHandler(this.buttonShowHideVM_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(89, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(657, 48);
            this.label5.TabIndex = 183;
            this.label5.Text = "鞍钢炼钢厂鱼雷罐车视觉系统";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox1.Location = new System.Drawing.Point(85, 339);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(551, 585);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 181;
            this.pictureBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(785, 182);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(239, 33);
            this.label4.TabIndex = 181;
            this.label4.Text = "罐车字符单动：";
            // 
            // Recognition
            // 
            this.Recognition.Image = ((System.Drawing.Image)(resources.GetObject("Recognition.Image")));
            this.Recognition.Location = new System.Drawing.Point(1039, 182);
            this.Recognition.Name = "Recognition";
            this.Recognition.Size = new System.Drawing.Size(36, 36);
            this.Recognition.TabIndex = 20;
            this.Recognition.UseVisualStyleBackColor = true;
            this.Recognition.Click += new System.EventHandler(this.Recognition_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(94, 176);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(239, 33);
            this.label3.TabIndex = 180;
            this.label3.Text = "罐车定位单动：";
            // 
            // txt_count1
            // 
            this.txt_count1.Font = new System.Drawing.Font("宋体", 24F);
            this.txt_count1.Location = new System.Drawing.Point(981, 255);
            this.txt_count1.Name = "txt_count1";
            this.txt_count1.Size = new System.Drawing.Size(193, 44);
            this.txt_count1.TabIndex = 176;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 24F);
            this.label1.Location = new System.Drawing.Point(785, 266);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 33);
            this.label1.TabIndex = 177;
            this.label1.Text = "罐号字符:";
            // 
            // txt_sdzs1
            // 
            this.txt_sdzs1.Font = new System.Drawing.Font("宋体", 24F);
            this.txt_sdzs1.Location = new System.Drawing.Point(294, 255);
            this.txt_sdzs1.Name = "txt_sdzs1";
            this.txt_sdzs1.Size = new System.Drawing.Size(210, 44);
            this.txt_sdzs1.TabIndex = 178;
            // 
            // LocationExecuteOnce
            // 
            this.LocationExecuteOnce.Image = ((System.Drawing.Image)(resources.GetObject("LocationExecuteOnce.Image")));
            this.LocationExecuteOnce.Location = new System.Drawing.Point(364, 171);
            this.LocationExecuteOnce.Name = "LocationExecuteOnce";
            this.LocationExecuteOnce.Size = new System.Drawing.Size(36, 36);
            this.LocationExecuteOnce.TabIndex = 18;
            this.LocationExecuteOnce.UseVisualStyleBackColor = true;
            this.LocationExecuteOnce.Click += new System.EventHandler(this.LocationExecuteOnce_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 24F);
            this.label2.Location = new System.Drawing.Point(94, 266);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(159, 33);
            this.label2.TabIndex = 179;
            this.label2.Text = "位置坐标:";
            // 
            // pictureBoxImg1
            // 
            this.pictureBoxImg1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBoxImg1.Location = new System.Drawing.Point(1304, 341);
            this.pictureBoxImg1.Name = "pictureBoxImg1";
            this.pictureBoxImg1.Size = new System.Drawing.Size(554, 585);
            this.pictureBoxImg1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxImg1.TabIndex = 35;
            this.pictureBoxImg1.TabStop = false;
            // 
            // timer_savedata
            // 
            this.timer_savedata.Tick += new System.EventHandler(this.timer_savedata_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.pictureBoxImg1);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "Form1";
            this.Text = "山钢集团·山信软件·莱芜钢铁集团电子有限公司";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBoxShowAndHide.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImg1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox txt_message;
        private System.Windows.Forms.Button buttonOpenVM;
        private System.Windows.Forms.Button buttonCloseVM;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button buttonCloseSolution;
        private System.Windows.Forms.GroupBox groupBoxShowAndHide;
        private System.Windows.Forms.ComboBox comboBoxShowAndHide;
        private System.Windows.Forms.ProgressBar progressBarSaveAndLoad;
        private System.Windows.Forms.Label labelProgress;
        private System.Windows.Forms.Button buttonLoadSolution;
        private System.Windows.Forms.Button buttonShowHideVM;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button Recognition;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_count1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_sdzs1;
        private System.Windows.Forms.Button LocationExecuteOnce;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBoxImg1;
        private System.Windows.Forms.Timer timer_deleterizhi;
        private System.Windows.Forms.Timer timer_savedata;
    }
}

