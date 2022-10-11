namespace AnBRobotSystem.ChildForm
{
    partial class Fauto_Form
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
            this.Guankou_vmRenderControl2 = new VMControls.Winform.Release.VmRenderControl();
            this.Tiebao_vmRenderControl1 = new VMControls.Winform.Release.VmRenderControl();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TL_vmRenderControl1 = new VMControls.Winform.Release.VmRenderControl();
            this.GuankouB_vmRenderControl1 = new VMControls.Winform.Release.VmRenderControl();
            this.SuspendLayout();
            // 
            // Guankou_vmRenderControl2
            // 
            this.Guankou_vmRenderControl2.BackColor = System.Drawing.Color.Black;
            this.Guankou_vmRenderControl2.ImageSource = null;
            this.Guankou_vmRenderControl2.Location = new System.Drawing.Point(470, 81);
            this.Guankou_vmRenderControl2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Guankou_vmRenderControl2.ModuleSource = null;
            this.Guankou_vmRenderControl2.Name = "Guankou_vmRenderControl2";
            this.Guankou_vmRenderControl2.Size = new System.Drawing.Size(400, 300);
            this.Guankou_vmRenderControl2.TabIndex = 4;
            // 
            // Tiebao_vmRenderControl1
            // 
            this.Tiebao_vmRenderControl1.BackColor = System.Drawing.Color.Black;
            this.Tiebao_vmRenderControl1.ImageSource = null;
            this.Tiebao_vmRenderControl1.Location = new System.Drawing.Point(48, 80);
            this.Tiebao_vmRenderControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Tiebao_vmRenderControl1.ModuleSource = null;
            this.Tiebao_vmRenderControl1.Name = "Tiebao_vmRenderControl1";
            this.Tiebao_vmRenderControl1.Size = new System.Drawing.Size(400, 300);
            this.Tiebao_vmRenderControl1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(103, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(350, 37);
            this.label1.TabIndex = 7;
            this.label1.Text = "铁包口图像处理结果";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(508, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(350, 37);
            this.label2.TabIndex = 8;
            this.label2.Text = "罐口口图像处理结果";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(1008, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(165, 37);
            this.label3.TabIndex = 11;
            this.label3.Text = "铁流检测";
            // 
            // TL_vmRenderControl1
            // 
            this.TL_vmRenderControl1.BackColor = System.Drawing.Color.Black;
            this.TL_vmRenderControl1.ImageSource = null;
            this.TL_vmRenderControl1.Location = new System.Drawing.Point(884, 80);
            this.TL_vmRenderControl1.Margin = new System.Windows.Forms.Padding(5);
            this.TL_vmRenderControl1.ModuleSource = null;
            this.TL_vmRenderControl1.Name = "TL_vmRenderControl1";
            this.TL_vmRenderControl1.Size = new System.Drawing.Size(400, 300);
            this.TL_vmRenderControl1.TabIndex = 12;
            // 
            // GuankouB_vmRenderControl1
            // 
            this.GuankouB_vmRenderControl1.BackColor = System.Drawing.Color.Black;
            this.GuankouB_vmRenderControl1.ImageSource = null;
            this.GuankouB_vmRenderControl1.Location = new System.Drawing.Point(475, 391);
            this.GuankouB_vmRenderControl1.Margin = new System.Windows.Forms.Padding(7);
            this.GuankouB_vmRenderControl1.ModuleSource = null;
            this.GuankouB_vmRenderControl1.Name = "GuankouB_vmRenderControl1";
            this.GuankouB_vmRenderControl1.Size = new System.Drawing.Size(400, 336);
            this.GuankouB_vmRenderControl1.TabIndex = 13;
            // 
            // Fauto_Form
            // 
            this.AllowShowTitle = false;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1298, 736);
            this.ControlBox = false;
            this.Controls.Add(this.GuankouB_vmRenderControl1);
            this.Controls.Add(this.Tiebao_vmRenderControl1);
            this.Controls.Add(this.TL_vmRenderControl1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Guankou_vmRenderControl2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Fauto_Form";
            this.Padding = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.ShowDragStretch = true;
            this.ShowInTaskbar = false;
            this.ShowRadius = false;
            this.ShowTitle = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "视觉图像";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Fauto_Form_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Fauto_Form_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private VMControls.Winform.Release.VmRenderControl Guankou_vmRenderControl2;
        private VMControls.Winform.Release.VmRenderControl Tiebao_vmRenderControl1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private VMControls.Winform.Release.VmRenderControl TL_vmRenderControl1;
        private VMControls.Winform.Release.VmRenderControl GuankouB_vmRenderControl1;
    }
}