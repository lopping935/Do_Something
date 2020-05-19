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
            this.labelProgress = new System.Windows.Forms.Label();
            this.progressBarSaveAndLoad = new System.Windows.Forms.ProgressBar();
            this.buttonCloseSolution = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.buttonLoadSolution = new System.Windows.Forms.Button();
            this.pictureBoxImg1 = new System.Windows.Forms.PictureBox();
            this.groupBoxShowAndHide = new System.Windows.Forms.GroupBox();
            this.comboBoxShowAndHide = new System.Windows.Forms.ComboBox();
            this.buttonShowHideVM = new System.Windows.Forms.Button();
            this.txt_message = new System.Windows.Forms.TextBox();
            this.groupBoxControl = new System.Windows.Forms.GroupBox();
            this.buttonStopExecute = new System.Windows.Forms.Button();
            this.buttonExecuteOnce = new System.Windows.Forms.Button();
            this.buttonContinuExecute = new System.Windows.Forms.Button();
            this.PLC_disconnection = new System.Windows.Forms.Button();
            this.PLC_connection = new System.Windows.Forms.Button();
            this.timer_deleterizhi = new System.Windows.Forms.Timer(this.components);
            this.txt_count1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_sdzs1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImg1)).BeginInit();
            this.groupBoxShowAndHide.SuspendLayout();
            this.groupBoxControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOpenVM
            // 
            this.buttonOpenVM.Font = new System.Drawing.Font("Cambria", 12F);
            this.buttonOpenVM.Location = new System.Drawing.Point(159, 48);
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
            this.buttonCloseVM.Location = new System.Drawing.Point(399, 48);
            this.buttonCloseVM.Name = "buttonCloseVM";
            this.buttonCloseVM.Size = new System.Drawing.Size(156, 44);
            this.buttonCloseVM.TabIndex = 6;
            this.buttonCloseVM.Text = "关闭VisionMaster";
            this.buttonCloseVM.UseVisualStyleBackColor = true;
            this.buttonCloseVM.Click += new System.EventHandler(this.buttonCloseVM_Click);
            // 
            // labelProgress
            // 
            this.labelProgress.AutoSize = true;
            this.labelProgress.Font = new System.Drawing.Font("宋体", 12F);
            this.labelProgress.Location = new System.Drawing.Point(214, 192);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new System.Drawing.Size(16, 16);
            this.labelProgress.TabIndex = 33;
            this.labelProgress.Text = "0";
            // 
            // progressBarSaveAndLoad
            // 
            this.progressBarSaveAndLoad.Location = new System.Drawing.Point(255, 184);
            this.progressBarSaveAndLoad.Name = "progressBarSaveAndLoad";
            this.progressBarSaveAndLoad.Size = new System.Drawing.Size(300, 24);
            this.progressBarSaveAndLoad.TabIndex = 32;
            // 
            // buttonCloseSolution
            // 
            this.buttonCloseSolution.Font = new System.Drawing.Font("宋体", 12F);
            this.buttonCloseSolution.Location = new System.Drawing.Point(399, 119);
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
            this.label11.Location = new System.Drawing.Point(161, 192);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(56, 16);
            this.label11.TabIndex = 30;
            this.label11.Text = "进度：";
            // 
            // buttonLoadSolution
            // 
            this.buttonLoadSolution.Font = new System.Drawing.Font("宋体", 12F);
            this.buttonLoadSolution.Location = new System.Drawing.Point(159, 119);
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
            this.pictureBoxImg1.Location = new System.Drawing.Point(672, 165);
            this.pictureBoxImg1.Name = "pictureBoxImg1";
            this.pictureBoxImg1.Size = new System.Drawing.Size(1147, 788);
            this.pictureBoxImg1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxImg1.TabIndex = 35;
            this.pictureBoxImg1.TabStop = false;
            // 
            // groupBoxShowAndHide
            // 
            this.groupBoxShowAndHide.Controls.Add(this.comboBoxShowAndHide);
            this.groupBoxShowAndHide.Font = new System.Drawing.Font("宋体", 9F);
            this.groupBoxShowAndHide.Location = new System.Drawing.Point(175, 228);
            this.groupBoxShowAndHide.Name = "groupBoxShowAndHide";
            this.groupBoxShowAndHide.Size = new System.Drawing.Size(94, 42);
            this.groupBoxShowAndHide.TabIndex = 37;
            this.groupBoxShowAndHide.TabStop = false;
            this.groupBoxShowAndHide.Text = "显示隐藏状态";
            // 
            // comboBoxShowAndHide
            // 
            this.comboBoxShowAndHide.FormattingEnabled = true;
            this.comboBoxShowAndHide.Location = new System.Drawing.Point(6, 17);
            this.comboBoxShowAndHide.Name = "comboBoxShowAndHide";
            this.comboBoxShowAndHide.Size = new System.Drawing.Size(82, 20);
            this.comboBoxShowAndHide.TabIndex = 5;
            // 
            // buttonShowHideVM
            // 
            this.buttonShowHideVM.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonShowHideVM.Location = new System.Drawing.Point(285, 228);
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
            this.txt_message.Location = new System.Drawing.Point(108, 367);
            this.txt_message.Multiline = true;
            this.txt_message.Name = "txt_message";
            this.txt_message.Size = new System.Drawing.Size(473, 586);
            this.txt_message.TabIndex = 170;
            // 
            // groupBoxControl
            // 
            this.groupBoxControl.Controls.Add(this.buttonStopExecute);
            this.groupBoxControl.Controls.Add(this.buttonExecuteOnce);
            this.groupBoxControl.Controls.Add(this.buttonContinuExecute);
            this.groupBoxControl.Location = new System.Drawing.Point(672, 69);
            this.groupBoxControl.Name = "groupBoxControl";
            this.groupBoxControl.Size = new System.Drawing.Size(178, 80);
            this.groupBoxControl.TabIndex = 171;
            this.groupBoxControl.TabStop = false;
            this.groupBoxControl.Text = "单次执行 连续执行 停止执行";
            // 
            // buttonStopExecute
            // 
            this.buttonStopExecute.Image = ((System.Drawing.Image)(resources.GetObject("buttonStopExecute.Image")));
            this.buttonStopExecute.Location = new System.Drawing.Point(121, 26);
            this.buttonStopExecute.Name = "buttonStopExecute";
            this.buttonStopExecute.Size = new System.Drawing.Size(36, 36);
            this.buttonStopExecute.TabIndex = 19;
            this.buttonStopExecute.UseVisualStyleBackColor = true;
            this.buttonStopExecute.Click += new System.EventHandler(this.buttonStopExecute_Click);
            // 
            // buttonExecuteOnce
            // 
            this.buttonExecuteOnce.Image = ((System.Drawing.Image)(resources.GetObject("buttonExecuteOnce.Image")));
            this.buttonExecuteOnce.Location = new System.Drawing.Point(12, 26);
            this.buttonExecuteOnce.Name = "buttonExecuteOnce";
            this.buttonExecuteOnce.Size = new System.Drawing.Size(36, 36);
            this.buttonExecuteOnce.TabIndex = 18;
            this.buttonExecuteOnce.UseVisualStyleBackColor = true;
            this.buttonExecuteOnce.Click += new System.EventHandler(this.buttonExecuteOnce_Click);
            // 
            // buttonContinuExecute
            // 
            this.buttonContinuExecute.Image = ((System.Drawing.Image)(resources.GetObject("buttonContinuExecute.Image")));
            this.buttonContinuExecute.Location = new System.Drawing.Point(66, 26);
            this.buttonContinuExecute.Name = "buttonContinuExecute";
            this.buttonContinuExecute.Size = new System.Drawing.Size(36, 36);
            this.buttonContinuExecute.TabIndex = 20;
            this.buttonContinuExecute.UseVisualStyleBackColor = true;
            this.buttonContinuExecute.Click += new System.EventHandler(this.buttonContinuExecute_Click);
            // 
            // PLC_disconnection
            // 
            this.PLC_disconnection.Font = new System.Drawing.Font("宋体", 12F);
            this.PLC_disconnection.Location = new System.Drawing.Point(399, 289);
            this.PLC_disconnection.Name = "PLC_disconnection";
            this.PLC_disconnection.Size = new System.Drawing.Size(156, 44);
            this.PLC_disconnection.TabIndex = 174;
            this.PLC_disconnection.Text = "PLC断开";
            this.PLC_disconnection.UseVisualStyleBackColor = true;
            this.PLC_disconnection.Click += new System.EventHandler(this.PLC_disconnection_Click);
            // 
            // PLC_connection
            // 
            this.PLC_connection.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PLC_connection.Location = new System.Drawing.Point(159, 289);
            this.PLC_connection.Name = "PLC_connection";
            this.PLC_connection.Size = new System.Drawing.Size(156, 44);
            this.PLC_connection.TabIndex = 175;
            this.PLC_connection.Text = "PLC连接";
            this.PLC_connection.UseVisualStyleBackColor = true;
            this.PLC_connection.Click += new System.EventHandler(this.PLC_connection_Click);
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
            this.txt_count1.Location = new System.Drawing.Point(1545, 89);
            this.txt_count1.Name = "txt_count1";
            this.txt_count1.Size = new System.Drawing.Size(119, 44);
            this.txt_count1.TabIndex = 176;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 24F);
            this.label1.Location = new System.Drawing.Point(1345, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 33);
            this.label1.TabIndex = 177;
            this.label1.Text = "棒材计数:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 24F);
            this.label2.Location = new System.Drawing.Point(916, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(159, 33);
            this.label2.TabIndex = 179;
            this.label2.Text = "设定支数:";
            // 
            // txt_sdzs1
            // 
            this.txt_sdzs1.Font = new System.Drawing.Font("宋体", 24F);
            this.txt_sdzs1.Location = new System.Drawing.Point(1116, 86);
            this.txt_sdzs1.Name = "txt_sdzs1";
            this.txt_sdzs1.Size = new System.Drawing.Size(119, 44);
            this.txt_sdzs1.TabIndex = 178;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 132);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 180;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_sdzs1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_count1);
            this.Controls.Add(this.PLC_disconnection);
            this.Controls.Add(this.PLC_connection);
            this.Controls.Add(this.groupBoxControl);
            this.Controls.Add(this.txt_message);
            this.Controls.Add(this.groupBoxShowAndHide);
            this.Controls.Add(this.buttonShowHideVM);
            this.Controls.Add(this.pictureBoxImg1);
            this.Controls.Add(this.buttonLoadSolution);
            this.Controls.Add(this.labelProgress);
            this.Controls.Add(this.progressBarSaveAndLoad);
            this.Controls.Add(this.buttonCloseSolution);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.buttonOpenVM);
            this.Controls.Add(this.buttonCloseVM);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "中棒计数";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImg1)).EndInit();
            this.groupBoxShowAndHide.ResumeLayout(false);
            this.groupBoxControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOpenVM;
        private System.Windows.Forms.Button buttonCloseVM;
        private System.Windows.Forms.Label labelProgress;
        private System.Windows.Forms.ProgressBar progressBarSaveAndLoad;
        private System.Windows.Forms.Button buttonCloseSolution;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button buttonLoadSolution;
        private System.Windows.Forms.PictureBox pictureBoxImg1;
        private System.Windows.Forms.GroupBox groupBoxShowAndHide;
        private System.Windows.Forms.ComboBox comboBoxShowAndHide;
        private System.Windows.Forms.Button buttonShowHideVM;
        private System.Windows.Forms.TextBox txt_message;
        private System.Windows.Forms.GroupBox groupBoxControl;
        private System.Windows.Forms.Button buttonStopExecute;
        private System.Windows.Forms.Button buttonExecuteOnce;
        private System.Windows.Forms.Button buttonContinuExecute;
        private System.Windows.Forms.Button PLC_disconnection;
        private System.Windows.Forms.Button PLC_connection;
        private System.Windows.Forms.Timer timer_deleterizhi;
        private System.Windows.Forms.TextBox txt_count1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_sdzs1;
        private System.Windows.Forms.Button button1;
    }
}

