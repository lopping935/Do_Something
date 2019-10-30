namespace AnBRobotSystem.ChildForm
{
    partial class CMessageBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CMessageBox));
            this.CHECK_REPLACE = new System.Windows.Forms.CheckBox();
            this.BT_OK = new System.Windows.Forms.Button();
            this.BT_CANCEL = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // CHECK_REPLACE
            // 
            this.CHECK_REPLACE.AutoSize = true;
            this.CHECK_REPLACE.Location = new System.Drawing.Point(95, 21);
            this.CHECK_REPLACE.Name = "CHECK_REPLACE";
            this.CHECK_REPLACE.Size = new System.Drawing.Size(180, 16);
            this.CHECK_REPLACE.TabIndex = 0;
            this.CHECK_REPLACE.Text = "强制更新，对重复的进行替换";
            this.CHECK_REPLACE.UseVisualStyleBackColor = true;
            // 
            // BT_OK
            // 
            this.BT_OK.Location = new System.Drawing.Point(209, 55);
            this.BT_OK.Name = "BT_OK";
            this.BT_OK.Size = new System.Drawing.Size(75, 23);
            this.BT_OK.TabIndex = 1;
            this.BT_OK.Text = "确认";
            this.BT_OK.UseVisualStyleBackColor = true;
            this.BT_OK.Click += new System.EventHandler(this.BT_OK_Click);
            // 
            // BT_CANCEL
            // 
            this.BT_CANCEL.Location = new System.Drawing.Point(95, 55);
            this.BT_CANCEL.Name = "BT_CANCEL";
            this.BT_CANCEL.Size = new System.Drawing.Size(75, 23);
            this.BT_CANCEL.TabIndex = 1;
            this.BT_CANCEL.Text = "取消";
            this.BT_CANCEL.UseVisualStyleBackColor = true;
            this.BT_CANCEL.Click += new System.EventHandler(this.BT_CANCEL_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(30, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(33, 37);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // CMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 90);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.BT_CANCEL);
            this.Controls.Add(this.BT_OK);
            this.Controls.Add(this.CHECK_REPLACE);
            this.Name = "CMessageBox";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "有重复项";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox CHECK_REPLACE;
        private System.Windows.Forms.Button BT_OK;
        private System.Windows.Forms.Button BT_CANCEL;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}