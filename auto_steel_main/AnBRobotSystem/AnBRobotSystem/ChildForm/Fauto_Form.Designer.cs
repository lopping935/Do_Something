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
            this.vmProcedureConfigControl1 = new VMControls.Winform.Release.VmProcedureConfigControl();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Guankou_vmRenderControl2
            // 
            this.Guankou_vmRenderControl2.BackColor = System.Drawing.Color.Black;
            this.Guankou_vmRenderControl2.ImageSource = null;
            this.Guankou_vmRenderControl2.Location = new System.Drawing.Point(418, 78);
            this.Guankou_vmRenderControl2.ModuleSource = null;
            this.Guankou_vmRenderControl2.Name = "Guankou_vmRenderControl2";
            this.Guankou_vmRenderControl2.Size = new System.Drawing.Size(400, 300);
            this.Guankou_vmRenderControl2.TabIndex = 4;
            // 
            // Tiebao_vmRenderControl1
            // 
            this.Tiebao_vmRenderControl1.BackColor = System.Drawing.Color.Black;
            this.Tiebao_vmRenderControl1.ImageSource = null;
            this.Tiebao_vmRenderControl1.Location = new System.Drawing.Point(12, 78);
            this.Tiebao_vmRenderControl1.ModuleSource = null;
            this.Tiebao_vmRenderControl1.Name = "Tiebao_vmRenderControl1";
            this.Tiebao_vmRenderControl1.Size = new System.Drawing.Size(400, 300);
            this.Tiebao_vmRenderControl1.TabIndex = 3;
            // 
            // vmProcedureConfigControl1
            // 
            this.vmProcedureConfigControl1.Location = new System.Drawing.Point(823, 78);
            this.vmProcedureConfigControl1.Margin = new System.Windows.Forms.Padding(2);
            this.vmProcedureConfigControl1.Name = "vmProcedureConfigControl1";
            this.vmProcedureConfigControl1.Size = new System.Drawing.Size(385, 300);
            this.vmProcedureConfigControl1.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(55, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(274, 29);
            this.label1.TabIndex = 7;
            this.label1.Text = "铁包口图像处理结果";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(475, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(274, 29);
            this.label2.TabIndex = 8;
            this.label2.Text = "罐口口图像处理结果";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(929, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 29);
            this.label3.TabIndex = 9;
            this.label3.Text = "处理流程";
            // 
            // Fauto_Form
            // 
            this.AllowShowTitle = false;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1298, 541);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.vmProcedureConfigControl1);
            this.Controls.Add(this.Guankou_vmRenderControl2);
            this.Controls.Add(this.Tiebao_vmRenderControl1);
            this.Name = "Fauto_Form";
            this.Padding = new System.Windows.Forms.Padding(0);
            this.ShowTitle = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "视觉图像";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private VMControls.Winform.Release.VmRenderControl Guankou_vmRenderControl2;
        private VMControls.Winform.Release.VmRenderControl Tiebao_vmRenderControl1;
        private VMControls.Winform.Release.VmProcedureConfigControl vmProcedureConfigControl1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}