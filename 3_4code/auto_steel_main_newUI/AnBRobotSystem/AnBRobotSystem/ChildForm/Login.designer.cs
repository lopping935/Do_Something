namespace AnBRobotSystem
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.TB_PASS = new System.Windows.Forms.TextBox();
            this.TB_CODE = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BTLogin = new System.Windows.Forms.Button();
            this.BTCancle = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(378, 118);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.TB_PASS);
            this.groupBox1.Controls.Add(this.TB_CODE);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(0, 104);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(378, 126);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(217, 104);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(72, 16);
            this.checkBox2.TabIndex = 5;
            this.checkBox2.Text = "自动登录";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckStateChanged += new System.EventHandler(this.checkBox2_CheckStateChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(132, 104);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 16);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "记住密码";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckStateChanged += new System.EventHandler(this.checkBox1_CheckStateChanged);
            // 
            // TB_PASS
            // 
            this.TB_PASS.Location = new System.Drawing.Point(132, 65);
            this.TB_PASS.Name = "TB_PASS";
            this.TB_PASS.PasswordChar = '*';
            this.TB_PASS.Size = new System.Drawing.Size(157, 21);
            this.TB_PASS.TabIndex = 3;
            this.TB_PASS.UseSystemPasswordChar = true;
            this.TB_PASS.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_PASS_KeyPress);
            // 
            // TB_CODE
            // 
            this.TB_CODE.Location = new System.Drawing.Point(132, 20);
            this.TB_CODE.Name = "TB_CODE";
            this.TB_CODE.Size = new System.Drawing.Size(157, 21);
            this.TB_CODE.TabIndex = 2;
            this.TB_CODE.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_CODE_KeyPress);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(51, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "密码:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(51, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "账号:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BTLogin
            // 
            this.BTLogin.Location = new System.Drawing.Point(99, 236);
            this.BTLogin.Name = "BTLogin";
            this.BTLogin.Size = new System.Drawing.Size(75, 23);
            this.BTLogin.TabIndex = 2;
            this.BTLogin.Text = "登录";
            this.BTLogin.UseVisualStyleBackColor = true;
            this.BTLogin.Click += new System.EventHandler(this.BTLogin_Click);
            // 
            // BTCancle
            // 
            this.BTCancle.Location = new System.Drawing.Point(217, 236);
            this.BTCancle.Name = "BTCancle";
            this.BTCancle.Size = new System.Drawing.Size(75, 23);
            this.BTCancle.TabIndex = 3;
            this.BTCancle.Text = "取消";
            this.BTCancle.UseVisualStyleBackColor = true;
            this.BTCancle.Click += new System.EventHandler(this.BTCancle_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 268);
            this.Controls.Add(this.BTCancle);
            this.Controls.Add(this.BTLogin);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Login";
            this.Tag = "";
            this.Text = "欢迎登录：大型线喷号贴标机器人信息系统";
            this.Load += new System.EventHandler(this.Login_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox TB_PASS;
        private System.Windows.Forms.TextBox TB_CODE;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BTLogin;
        private System.Windows.Forms.Button BTCancle;
    }
}