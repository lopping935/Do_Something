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
            this.buttonOpenVM = new System.Windows.Forms.Button();
            this.buttonCloseVM = new System.Windows.Forms.Button();
            this.progressBarSaveAndLoad = new System.Windows.Forms.ProgressBar();
            this.buttonCloseSolution = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.buttonLoadSolution = new System.Windows.Forms.Button();
            this.pictureBoxImg1 = new System.Windows.Forms.PictureBox();
            this.groupBoxShowAndHide = new System.Windows.Forms.GroupBox();
            this.comboBoxShowAndHide = new System.Windows.Forms.ComboBox();
            this.buttonShowHideVM = new System.Windows.Forms.Button();
            this.txt_message = new System.Windows.Forms.TextBox();
            this.Recognition = new System.Windows.Forms.Button();
            this.LocationExecuteOnce = new System.Windows.Forms.Button();
            this.timer_deleterizhi = new System.Windows.Forms.Timer(this.components);
            this.txt_count1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_sdzs1 = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.labelProgress = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImg1)).BeginInit();
            this.groupBoxShowAndHide.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOpenVM
            // 
            this.buttonOpenVM.Font = new System.Drawing.Font("Cambria", 12F);
            this.buttonOpenVM.Location = new System.Drawing.Point(61, 57);
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
            this.buttonCloseVM.Location = new System.Drawing.Point(301, 57);
            this.buttonCloseVM.Name = "buttonCloseVM";
            this.buttonCloseVM.Size = new System.Drawing.Size(156, 44);
            this.buttonCloseVM.TabIndex = 6;
            this.buttonCloseVM.Text = "关闭VisionMaster";
            this.buttonCloseVM.UseVisualStyleBackColor = true;
            this.buttonCloseVM.Click += new System.EventHandler(this.buttonCloseVM_Click);
            // 
            // progressBarSaveAndLoad
            // 
            this.progressBarSaveAndLoad.Location = new System.Drawing.Point(157, 197);
            this.progressBarSaveAndLoad.Name = "progressBarSaveAndLoad";
            this.progressBarSaveAndLoad.Size = new System.Drawing.Size(300, 24);
            this.progressBarSaveAndLoad.TabIndex = 32;
            // 
            // buttonCloseSolution
            // 
            this.buttonCloseSolution.Font = new System.Drawing.Font("宋体", 12F);
            this.buttonCloseSolution.Location = new System.Drawing.Point(301, 128);
            this.buttonCloseSolution.Name = "buttonCloseSolution";
            this.buttonCloseSolution.Size = new System.Drawing.Size(156, 44);
            this.buttonCloseSolution.TabIndex = 31;
            this.buttonCloseSolution.Text = "关闭方案";
            this.buttonCloseSolution.UseVisualStyleBackColor = true;
            this.buttonCloseSolution.Click += new System.EventHandler(this.buttonCloseSolution_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 12F);
            this.label11.Location = new System.Drawing.Point(63, 201);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(56, 16);
            this.label11.TabIndex = 30;
            this.label11.Text = "进度：";
            // 
            // buttonLoadSolution
            // 
            this.buttonLoadSolution.Font = new System.Drawing.Font("宋体", 12F);
            this.buttonLoadSolution.Location = new System.Drawing.Point(61, 128);
            this.buttonLoadSolution.Name = "buttonLoadSolution";
            this.buttonLoadSolution.Size = new System.Drawing.Size(156, 44);
            this.buttonLoadSolution.TabIndex = 34;
            this.buttonLoadSolution.Text = "加载方案";
            this.buttonLoadSolution.UseVisualStyleBackColor = true;
            this.buttonLoadSolution.Click += new System.EventHandler(this.buttonLoadSolution_Click);
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
            // groupBoxShowAndHide
            // 
            this.groupBoxShowAndHide.Controls.Add(this.comboBoxShowAndHide);
            this.groupBoxShowAndHide.Font = new System.Drawing.Font("宋体", 9F);
            this.groupBoxShowAndHide.Location = new System.Drawing.Point(61, 239);
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
            // buttonShowHideVM
            // 
            this.buttonShowHideVM.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonShowHideVM.Location = new System.Drawing.Point(187, 237);
            this.buttonShowHideVM.Name = "buttonShowHideVM";
            this.buttonShowHideVM.Size = new System.Drawing.Size(156, 44);
            this.buttonShowHideVM.TabIndex = 36;
            this.buttonShowHideVM.Text = "显示/隐藏 VisionMaster";
            this.buttonShowHideVM.UseVisualStyleBackColor = true;
            this.buttonShowHideVM.Click += new System.EventHandler(this.buttonShowHideVM_Click);
            // 
            // txt_message
            // 
            this.txt_message.AcceptsReturn = true;
            this.txt_message.Font = new System.Drawing.Font("宋体", 12F);
            this.txt_message.Location = new System.Drawing.Point(24, 346);
            this.txt_message.Multiline = true;
            this.txt_message.Name = "txt_message";
            this.txt_message.Size = new System.Drawing.Size(468, 586);
            this.txt_message.TabIndex = 170;
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
            // timer_deleterizhi
            // 
            this.timer_deleterizhi.Enabled = true;
            this.timer_deleterizhi.Interval = 60000;
            this.timer_deleterizhi.Tick += new System.EventHandler(this.timer_deleterizhi_Tick);
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
            // txt_sdzs1
            // 
            this.txt_sdzs1.Font = new System.Drawing.Font("宋体", 24F);
            this.txt_sdzs1.Location = new System.Drawing.Point(294, 255);
            this.txt_sdzs1.Name = "txt_sdzs1";
            this.txt_sdzs1.Size = new System.Drawing.Size(210, 44);
            this.txt_sdzs1.TabIndex = 178;
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
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
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
            this.splitContainer1.Size = new System.Drawing.Size(1904, 1041);
            this.splitContainer1.SplitterDistance = 530;
            this.splitContainer1.TabIndex = 182;
            // 
            // labelProgress
            // 
            this.labelProgress.AutoSize = true;
            this.labelProgress.Font = new System.Drawing.Font("宋体", 12F);
            this.labelProgress.Location = new System.Drawing.Point(125, 201);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new System.Drawing.Size(16, 16);
            this.labelProgress.TabIndex = 33;
            this.labelProgress.Text = "0";
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
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Font = new System.Drawing.Font("宋体", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(402, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(291, 39);
            this.label5.TabIndex = 182;
            this.label5.Text = "莱  钢  电  子";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.pictureBoxImg1);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "鞍钢鱼雷罐车";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImg1)).EndInit();
            this.groupBoxShowAndHide.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOpenVM;
        private System.Windows.Forms.Button buttonCloseVM;
        private System.Windows.Forms.ProgressBar progressBarSaveAndLoad;
        private System.Windows.Forms.Button buttonCloseSolution;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button buttonLoadSolution;
        private System.Windows.Forms.PictureBox pictureBoxImg1;
        private System.Windows.Forms.GroupBox groupBoxShowAndHide;
        private System.Windows.Forms.ComboBox comboBoxShowAndHide;
        private System.Windows.Forms.Button buttonShowHideVM;
        private System.Windows.Forms.TextBox txt_message;
        private System.Windows.Forms.Button LocationExecuteOnce;
        private System.Windows.Forms.Timer timer_deleterizhi;
        private System.Windows.Forms.TextBox txt_count1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_sdzs1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button Recognition;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label labelProgress;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
    }
}

