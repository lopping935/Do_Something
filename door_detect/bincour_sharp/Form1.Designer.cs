namespace bincour_sharp
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
            this.hWindowControl1 = new HalconDotNet.HWindowControl();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.bt_range1_confirm = new System.Windows.Forms.Button();
            this.txt_distance1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_region1 = new System.Windows.Forms.TextBox();
            this.bt_region1_confirm = new System.Windows.Forms.Button();
            this.result = new System.Windows.Forms.Label();
            this.txt_message = new System.Windows.Forms.TextBox();
            this.角度1 = new System.Windows.Forms.GroupBox();
            this.txt_range1 = new System.Windows.Forms.TextBox();
            this.bt_distance1_confirm = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_range2 = new System.Windows.Forms.TextBox();
            this.txt_distance2 = new System.Windows.Forms.TextBox();
            this.bt_distance2_confirm = new System.Windows.Forms.Button();
            this.bt_range2_confirm = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.bt_region2_confirm = new System.Windows.Forms.Button();
            this.txt_region2 = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txt_range3 = new System.Windows.Forms.TextBox();
            this.txt_distance3 = new System.Windows.Forms.TextBox();
            this.bt_distance3_confirm = new System.Windows.Forms.Button();
            this.bt_range3_confirm = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.bt_region3_confirm = new System.Windows.Forms.Button();
            this.txt_region3 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.角度1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // hWindowControl1
            // 
            this.hWindowControl1.AutoSize = true;
            this.hWindowControl1.BackColor = System.Drawing.Color.Black;
            this.hWindowControl1.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl1.ImagePart = new System.Drawing.Rectangle(0, 0, 1280, 960);
            this.hWindowControl1.Location = new System.Drawing.Point(30, 54);
            this.hWindowControl1.Name = "hWindowControl1";
            this.hWindowControl1.Size = new System.Drawing.Size(723, 564);
            this.hWindowControl1.TabIndex = 0;
            this.hWindowControl1.WindowSize = new System.Drawing.Size(723, 564);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(1579, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(29, 33);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // bt_range1_confirm
            // 
            this.bt_range1_confirm.Location = new System.Drawing.Point(245, 40);
            this.bt_range1_confirm.Name = "bt_range1_confirm";
            this.bt_range1_confirm.Size = new System.Drawing.Size(75, 23);
            this.bt_range1_confirm.TabIndex = 2;
            this.bt_range1_confirm.Text = "确认";
            this.bt_range1_confirm.UseVisualStyleBackColor = true;
            this.bt_range1_confirm.Click += new System.EventHandler(this.bt_range1_confirm_Click);
            // 
            // txt_distance1
            // 
            this.txt_distance1.Location = new System.Drawing.Point(110, 14);
            this.txt_distance1.Name = "txt_distance1";
            this.txt_distance1.Size = new System.Drawing.Size(100, 21);
            this.txt_distance1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "标准差：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "区域差：";
            // 
            // txt_region1
            // 
            this.txt_region1.Location = new System.Drawing.Point(110, 73);
            this.txt_region1.Name = "txt_region1";
            this.txt_region1.Size = new System.Drawing.Size(100, 21);
            this.txt_region1.TabIndex = 6;
            // 
            // bt_region1_confirm
            // 
            this.bt_region1_confirm.Location = new System.Drawing.Point(245, 70);
            this.bt_region1_confirm.Name = "bt_region1_confirm";
            this.bt_region1_confirm.Size = new System.Drawing.Size(75, 23);
            this.bt_region1_confirm.TabIndex = 5;
            this.bt_region1_confirm.Text = "确认";
            this.bt_region1_confirm.UseVisualStyleBackColor = true;
            this.bt_region1_confirm.Click += new System.EventHandler(this.bt_region1_confirm_Click);
            // 
            // result
            // 
            this.result.AutoSize = true;
            this.result.Font = new System.Drawing.Font("宋体", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.result.ForeColor = System.Drawing.Color.Red;
            this.result.Location = new System.Drawing.Point(759, 37);
            this.result.Name = "result";
            this.result.Size = new System.Drawing.Size(53, 56);
            this.result.TabIndex = 8;
            this.result.Text = "?";
            // 
            // txt_message
            // 
            this.txt_message.Location = new System.Drawing.Point(805, 393);
            this.txt_message.Multiline = true;
            this.txt_message.Name = "txt_message";
            this.txt_message.Size = new System.Drawing.Size(470, 226);
            this.txt_message.TabIndex = 9;
            // 
            // 角度1
            // 
            this.角度1.Controls.Add(this.txt_range1);
            this.角度1.Controls.Add(this.txt_distance1);
            this.角度1.Controls.Add(this.bt_distance1_confirm);
            this.角度1.Controls.Add(this.bt_range1_confirm);
            this.角度1.Controls.Add(this.label3);
            this.角度1.Controls.Add(this.label1);
            this.角度1.Controls.Add(this.label2);
            this.角度1.Controls.Add(this.bt_region1_confirm);
            this.角度1.Controls.Add(this.txt_region1);
            this.角度1.Location = new System.Drawing.Point(882, 21);
            this.角度1.Margin = new System.Windows.Forms.Padding(2);
            this.角度1.Name = "角度1";
            this.角度1.Padding = new System.Windows.Forms.Padding(2);
            this.角度1.Size = new System.Drawing.Size(331, 102);
            this.角度1.TabIndex = 10;
            this.角度1.TabStop = false;
            this.角度1.Text = "角度1";
            // 
            // txt_range1
            // 
            this.txt_range1.Location = new System.Drawing.Point(110, 44);
            this.txt_range1.Name = "txt_range1";
            this.txt_range1.Size = new System.Drawing.Size(100, 21);
            this.txt_range1.TabIndex = 3;
            // 
            // bt_distance1_confirm
            // 
            this.bt_distance1_confirm.Location = new System.Drawing.Point(245, 10);
            this.bt_distance1_confirm.Name = "bt_distance1_confirm";
            this.bt_distance1_confirm.Size = new System.Drawing.Size(75, 23);
            this.bt_distance1_confirm.TabIndex = 2;
            this.bt_distance1_confirm.Text = "确认";
            this.bt_distance1_confirm.UseVisualStyleBackColor = true;
            this.bt_distance1_confirm.Click += new System.EventHandler(this.bt_distance1_confirm_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(51, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "距离：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_range2);
            this.groupBox1.Controls.Add(this.txt_distance2);
            this.groupBox1.Controls.Add(this.bt_distance2_confirm);
            this.groupBox1.Controls.Add(this.bt_range2_confirm);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.bt_region2_confirm);
            this.groupBox1.Controls.Add(this.txt_region2);
            this.groupBox1.Location = new System.Drawing.Point(882, 142);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(331, 102);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "角度2";
            // 
            // txt_range2
            // 
            this.txt_range2.Location = new System.Drawing.Point(110, 44);
            this.txt_range2.Name = "txt_range2";
            this.txt_range2.Size = new System.Drawing.Size(100, 21);
            this.txt_range2.TabIndex = 3;
            // 
            // txt_distance2
            // 
            this.txt_distance2.Location = new System.Drawing.Point(110, 14);
            this.txt_distance2.Name = "txt_distance2";
            this.txt_distance2.Size = new System.Drawing.Size(100, 21);
            this.txt_distance2.TabIndex = 3;
            // 
            // bt_distance2_confirm
            // 
            this.bt_distance2_confirm.Location = new System.Drawing.Point(245, 10);
            this.bt_distance2_confirm.Name = "bt_distance2_confirm";
            this.bt_distance2_confirm.Size = new System.Drawing.Size(75, 23);
            this.bt_distance2_confirm.TabIndex = 2;
            this.bt_distance2_confirm.Text = "确认";
            this.bt_distance2_confirm.UseVisualStyleBackColor = true;
            this.bt_distance2_confirm.Click += new System.EventHandler(this.bt_distance2_confirm_Click);
            // 
            // bt_range2_confirm
            // 
            this.bt_range2_confirm.Location = new System.Drawing.Point(245, 40);
            this.bt_range2_confirm.Name = "bt_range2_confirm";
            this.bt_range2_confirm.Size = new System.Drawing.Size(75, 23);
            this.bt_range2_confirm.TabIndex = 2;
            this.bt_range2_confirm.Text = "确认";
            this.bt_range2_confirm.UseVisualStyleBackColor = true;
            this.bt_range2_confirm.Click += new System.EventHandler(this.bt_range2_confirm_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(51, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "距离：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(51, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "标准差：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(51, 78);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "区域差：";
            // 
            // bt_region2_confirm
            // 
            this.bt_region2_confirm.Location = new System.Drawing.Point(245, 70);
            this.bt_region2_confirm.Name = "bt_region2_confirm";
            this.bt_region2_confirm.Size = new System.Drawing.Size(75, 23);
            this.bt_region2_confirm.TabIndex = 5;
            this.bt_region2_confirm.Text = "确认";
            this.bt_region2_confirm.UseVisualStyleBackColor = true;
            this.bt_region2_confirm.Click += new System.EventHandler(this.bt_region2_confirm_Click);
            // 
            // txt_region2
            // 
            this.txt_region2.Location = new System.Drawing.Point(110, 73);
            this.txt_region2.Name = "txt_region2";
            this.txt_region2.Size = new System.Drawing.Size(100, 21);
            this.txt_region2.TabIndex = 6;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txt_range3);
            this.groupBox2.Controls.Add(this.txt_distance3);
            this.groupBox2.Controls.Add(this.bt_distance3_confirm);
            this.groupBox2.Controls.Add(this.bt_range3_confirm);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.bt_region3_confirm);
            this.groupBox2.Controls.Add(this.txt_region3);
            this.groupBox2.Location = new System.Drawing.Point(882, 264);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(331, 102);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "角度3";
            // 
            // txt_range3
            // 
            this.txt_range3.Location = new System.Drawing.Point(110, 44);
            this.txt_range3.Name = "txt_range3";
            this.txt_range3.Size = new System.Drawing.Size(100, 21);
            this.txt_range3.TabIndex = 3;
            // 
            // txt_distance3
            // 
            this.txt_distance3.Location = new System.Drawing.Point(110, 14);
            this.txt_distance3.Name = "txt_distance3";
            this.txt_distance3.Size = new System.Drawing.Size(100, 21);
            this.txt_distance3.TabIndex = 3;
            // 
            // bt_distance3_confirm
            // 
            this.bt_distance3_confirm.Location = new System.Drawing.Point(245, 10);
            this.bt_distance3_confirm.Name = "bt_distance3_confirm";
            this.bt_distance3_confirm.Size = new System.Drawing.Size(75, 23);
            this.bt_distance3_confirm.TabIndex = 2;
            this.bt_distance3_confirm.Text = "确认";
            this.bt_distance3_confirm.UseVisualStyleBackColor = true;
            this.bt_distance3_confirm.Click += new System.EventHandler(this.bt_distance3_confirm_Click);
            // 
            // bt_range3_confirm
            // 
            this.bt_range3_confirm.Location = new System.Drawing.Point(245, 40);
            this.bt_range3_confirm.Name = "bt_range3_confirm";
            this.bt_range3_confirm.Size = new System.Drawing.Size(75, 23);
            this.bt_range3_confirm.TabIndex = 2;
            this.bt_range3_confirm.Text = "确认";
            this.bt_range3_confirm.UseVisualStyleBackColor = true;
            this.bt_range3_confirm.Click += new System.EventHandler(this.bt_range3_confirm_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(51, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 4;
            this.label7.Text = "距离：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(51, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 4;
            this.label8.Text = "标准差：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(51, 78);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 7;
            this.label9.Text = "区域差：";
            // 
            // bt_region3_confirm
            // 
            this.bt_region3_confirm.Location = new System.Drawing.Point(245, 70);
            this.bt_region3_confirm.Name = "bt_region3_confirm";
            this.bt_region3_confirm.Size = new System.Drawing.Size(75, 23);
            this.bt_region3_confirm.TabIndex = 5;
            this.bt_region3_confirm.Text = "确认";
            this.bt_region3_confirm.UseVisualStyleBackColor = true;
            this.bt_region3_confirm.Click += new System.EventHandler(this.bt_region3_confirm_Click);
            // 
            // txt_region3
            // 
            this.txt_region3.Location = new System.Drawing.Point(110, 73);
            this.txt_region3.Name = "txt_region3";
            this.txt_region3.Size = new System.Drawing.Size(100, 21);
            this.txt_region3.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(790, 275);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1321, 680);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.角度1);
            this.Controls.Add(this.txt_message);
            this.Controls.Add(this.result);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.hWindowControl1);
            this.Name = "Form1";
            this.Text = "电炉测温视觉检测系统";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.角度1.ResumeLayout(false);
            this.角度1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HalconDotNet.HWindowControl hWindowControl1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button bt_range1_confirm;
        private System.Windows.Forms.TextBox txt_distance1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_region1;
        private System.Windows.Forms.Button bt_region1_confirm;
        private System.Windows.Forms.Label result;
        private System.Windows.Forms.TextBox txt_message;
        private System.Windows.Forms.GroupBox 角度1;
        private System.Windows.Forms.TextBox txt_range1;
        private System.Windows.Forms.Button bt_distance1_confirm;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_range2;
        private System.Windows.Forms.TextBox txt_distance2;
        private System.Windows.Forms.Button bt_distance2_confirm;
        private System.Windows.Forms.Button bt_range2_confirm;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button bt_region2_confirm;
        private System.Windows.Forms.TextBox txt_region2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txt_range3;
        private System.Windows.Forms.TextBox txt_distance3;
        private System.Windows.Forms.Button bt_distance3_confirm;
        private System.Windows.Forms.Button bt_range3_confirm;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button bt_region3_confirm;
        private System.Windows.Forms.TextBox txt_region3;
        private System.Windows.Forms.Button button1;
    }
}

