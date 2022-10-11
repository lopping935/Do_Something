namespace _20200123night
{
    partial class frmcome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmcome));
            this.buttoncome = new System.Windows.Forms.Button();
            this.txtuser = new System.Windows.Forms.TextBox();
            this.txtpassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnclose = new System.Windows.Forms.Button();
            this.txtresult_old = new System.Windows.Forms.TextBox();
            this.txtcode = new System.Windows.Forms.TextBox();
            this.txtcodeinput = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ub_jia = new Sunny.UI.UIRadioButton();
            this.ub_yi = new Sunny.UI.UIRadioButton();
            this.ub_bing = new Sunny.UI.UIRadioButton();
            this.ub_ding = new Sunny.UI.UIRadioButton();
            this.uiBtn_come = new Sunny.UI.UIButton();
            this.txtresult = new Sunny.UI.UITextBox();
            this.uib_close = new Sunny.UI.UIButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttoncome
            // 
            this.buttoncome.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttoncome.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttoncome.Location = new System.Drawing.Point(255, 475);
            this.buttoncome.Margin = new System.Windows.Forms.Padding(2);
            this.buttoncome.Name = "buttoncome";
            this.buttoncome.Size = new System.Drawing.Size(101, 32);
            this.buttoncome.TabIndex = 0;
            this.buttoncome.Text = "登录测试";
            this.buttoncome.UseVisualStyleBackColor = true;
            this.buttoncome.Visible = false;
            this.buttoncome.Click += new System.EventHandler(this.buttoncome_Click);
            // 
            // txtuser
            // 
            this.txtuser.Location = new System.Drawing.Point(255, 84);
            this.txtuser.Margin = new System.Windows.Forms.Padding(2);
            this.txtuser.Multiline = true;
            this.txtuser.Name = "txtuser";
            this.txtuser.Size = new System.Drawing.Size(101, 30);
            this.txtuser.TabIndex = 1;
            this.txtuser.TextChanged += new System.EventHandler(this.txtuser_TextChanged);
            // 
            // txtpassword
            // 
            this.txtpassword.Location = new System.Drawing.Point(255, 160);
            this.txtpassword.Margin = new System.Windows.Forms.Padding(2);
            this.txtpassword.Multiline = true;
            this.txtpassword.Name = "txtpassword";
            this.txtpassword.PasswordChar = '*';
            this.txtpassword.Size = new System.Drawing.Size(101, 30);
            this.txtpassword.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(50, 84);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 30);
            this.label1.TabIndex = 2;
            this.label1.Text = "请输入用户名：";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(50, 160);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 30);
            this.label2.TabIndex = 2;
            this.label2.Text = "请输入密码：";
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnclose.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnclose.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnclose.Location = new System.Drawing.Point(1000, 136);
            this.btnclose.Margin = new System.Windows.Forms.Padding(2);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(39, 38);
            this.btnclose.TabIndex = 3;
            this.btnclose.Text = "×";
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Visible = false;
            this.btnclose.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtresult_old
            // 
            this.txtresult_old.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtresult_old.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.txtresult_old.ForeColor = System.Drawing.Color.Red;
            this.txtresult_old.Location = new System.Drawing.Point(55, 512);
            this.txtresult_old.Margin = new System.Windows.Forms.Padding(2);
            this.txtresult_old.Multiline = true;
            this.txtresult_old.Name = "txtresult_old";
            this.txtresult_old.Size = new System.Drawing.Size(301, 32);
            this.txtresult_old.TabIndex = 1;
            this.txtresult_old.Visible = false;
            // 
            // txtcode
            // 
            this.txtcode.Location = new System.Drawing.Point(255, 255);
            this.txtcode.Margin = new System.Windows.Forms.Padding(2);
            this.txtcode.Multiline = true;
            this.txtcode.Name = "txtcode";
            this.txtcode.Size = new System.Drawing.Size(101, 30);
            this.txtcode.TabIndex = 1;
            this.txtcode.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // txtcodeinput
            // 
            this.txtcodeinput.Location = new System.Drawing.Point(255, 367);
            this.txtcodeinput.Margin = new System.Windows.Forms.Padding(2);
            this.txtcodeinput.Multiline = true;
            this.txtcodeinput.Name = "txtcodeinput";
            this.txtcodeinput.Size = new System.Drawing.Size(101, 30);
            this.txtcodeinput.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(50, 367);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(167, 30);
            this.label3.TabIndex = 2;
            this.label3.Text = "请输入验证码：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(50, 255);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 30);
            this.label4.TabIndex = 4;
            this.label4.Text = "验证码：";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(438, 20);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(521, 404);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // ub_jia
            // 
            this.ub_jia.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ub_jia.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.ub_jia.Location = new System.Drawing.Point(55, 20);
            this.ub_jia.MinimumSize = new System.Drawing.Size(1, 1);
            this.ub_jia.Name = "ub_jia";
            this.ub_jia.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.ub_jia.Size = new System.Drawing.Size(56, 29);
            this.ub_jia.TabIndex = 7;
            this.ub_jia.Text = "甲";
            // 
            // ub_yi
            // 
            this.ub_yi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ub_yi.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.ub_yi.Location = new System.Drawing.Point(132, 20);
            this.ub_yi.MinimumSize = new System.Drawing.Size(1, 1);
            this.ub_yi.Name = "ub_yi";
            this.ub_yi.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.ub_yi.Size = new System.Drawing.Size(54, 29);
            this.ub_yi.TabIndex = 7;
            this.ub_yi.Text = "乙";
            // 
            // ub_bing
            // 
            this.ub_bing.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ub_bing.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.ub_bing.Location = new System.Drawing.Point(218, 20);
            this.ub_bing.MinimumSize = new System.Drawing.Size(1, 1);
            this.ub_bing.Name = "ub_bing";
            this.ub_bing.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.ub_bing.Size = new System.Drawing.Size(52, 29);
            this.ub_bing.TabIndex = 7;
            this.ub_bing.Text = "丙";
            // 
            // ub_ding
            // 
            this.ub_ding.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ub_ding.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.ub_ding.Location = new System.Drawing.Point(304, 20);
            this.ub_ding.MinimumSize = new System.Drawing.Size(1, 1);
            this.ub_ding.Name = "ub_ding";
            this.ub_ding.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.ub_ding.Size = new System.Drawing.Size(52, 29);
            this.ub_ding.TabIndex = 7;
            this.ub_ding.Text = "丁";
            // 
            // uiBtn_come
            // 
            this.uiBtn_come.BackColor = System.Drawing.Color.SkyBlue;
            this.uiBtn_come.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiBtn_come.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold);
            this.uiBtn_come.Location = new System.Drawing.Point(55, 448);
            this.uiBtn_come.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiBtn_come.Name = "uiBtn_come";
            this.uiBtn_come.Size = new System.Drawing.Size(147, 59);
            this.uiBtn_come.TabIndex = 8;
            this.uiBtn_come.Text = "登录";
            this.uiBtn_come.Click += new System.EventHandler(this.uiBtn_come_Click);
            // 
            // txtresult
            // 
            this.txtresult.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.txtresult.ButtonSymbol = 61761;
            this.txtresult.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtresult.FillColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtresult.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.txtresult.Location = new System.Drawing.Point(438, 448);
            this.txtresult.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtresult.Maximum = 2147483647D;
            this.txtresult.Minimum = -2147483648D;
            this.txtresult.MinimumSize = new System.Drawing.Size(1, 1);
            this.txtresult.Name = "txtresult";
            this.txtresult.Size = new System.Drawing.Size(521, 51);
            this.txtresult.Style = Sunny.UI.UIStyle.Custom;
            this.txtresult.TabIndex = 9;
            this.txtresult.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uib_close
            // 
            this.uib_close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uib_close.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uib_close.Location = new System.Drawing.Point(1009, 3);
            this.uib_close.MinimumSize = new System.Drawing.Size(1, 1);
            this.uib_close.Name = "uib_close";
            this.uib_close.Size = new System.Drawing.Size(38, 37);
            this.uib_close.TabIndex = 10;
            this.uib_close.Text = "×";
            this.uib_close.Click += new System.EventHandler(this.uib_close_Click);
            // 
            // frmcome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.LightSkyBlue;
            this.ClientSize = new System.Drawing.Size(1050, 547);
            this.Controls.Add(this.uib_close);
            this.Controls.Add(this.txtresult);
            this.Controls.Add(this.uiBtn_come);
            this.Controls.Add(this.ub_ding);
            this.Controls.Add(this.ub_bing);
            this.Controls.Add(this.ub_yi);
            this.Controls.Add(this.ub_jia);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtresult_old);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtcodeinput);
            this.Controls.Add(this.txtcode);
            this.Controls.Add(this.txtpassword);
            this.Controls.Add(this.txtuser);
            this.Controls.Add(this.buttoncome);
            this.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmcome";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmcome_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttoncome;
        private System.Windows.Forms.TextBox txtuser;
        private System.Windows.Forms.TextBox txtpassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.TextBox txtresult_old;
        private System.Windows.Forms.TextBox txtcode;
        private System.Windows.Forms.TextBox txtcodeinput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Sunny.UI.UIRadioButton ub_jia;
        private Sunny.UI.UIRadioButton ub_yi;
        private Sunny.UI.UIRadioButton ub_bing;
        private Sunny.UI.UIRadioButton ub_ding;
        private Sunny.UI.UIButton uiBtn_come;
        private Sunny.UI.UITextBox txtresult;
        private Sunny.UI.UIButton uib_close;
    }
}