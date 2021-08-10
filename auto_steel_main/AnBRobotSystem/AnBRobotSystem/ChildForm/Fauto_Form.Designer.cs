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
            this.button1 = new System.Windows.Forms.Button();
            this.vmProcedureConfigControl1 = new VMControls.Winform.Release.VmProcedureConfigControl();
            this.SuspendLayout();
            // 
            // Guankou_vmRenderControl2
            // 
            this.Guankou_vmRenderControl2.BackColor = System.Drawing.Color.Black;
            this.Guankou_vmRenderControl2.ImageSource = null;
            this.Guankou_vmRenderControl2.Location = new System.Drawing.Point(418, 12);
            this.Guankou_vmRenderControl2.ModuleSource = null;
            this.Guankou_vmRenderControl2.Name = "Guankou_vmRenderControl2";
            this.Guankou_vmRenderControl2.Size = new System.Drawing.Size(400, 300);
            this.Guankou_vmRenderControl2.TabIndex = 4;
            // 
            // Tiebao_vmRenderControl1
            // 
            this.Tiebao_vmRenderControl1.BackColor = System.Drawing.Color.Black;
            this.Tiebao_vmRenderControl1.ImageSource = null;
            this.Tiebao_vmRenderControl1.Location = new System.Drawing.Point(12, 12);
            this.Tiebao_vmRenderControl1.ModuleSource = null;
            this.Tiebao_vmRenderControl1.Name = "Tiebao_vmRenderControl1";
            this.Tiebao_vmRenderControl1.Size = new System.Drawing.Size(400, 300);
            this.Tiebao_vmRenderControl1.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(188, 526);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // vmProcedureConfigControl1
            // 
            this.vmProcedureConfigControl1.Location = new System.Drawing.Point(834, 12);
            this.vmProcedureConfigControl1.Margin = new System.Windows.Forms.Padding(2);
            this.vmProcedureConfigControl1.Name = "vmProcedureConfigControl1";
            this.vmProcedureConfigControl1.Size = new System.Drawing.Size(385, 300);
            this.vmProcedureConfigControl1.TabIndex = 6;
            // 
            // Fauto_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1298, 844);
            this.Controls.Add(this.vmProcedureConfigControl1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Guankou_vmRenderControl2);
            this.Controls.Add(this.Tiebao_vmRenderControl1);
            this.Name = "Fauto_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Fauto_Form";
            this.ResumeLayout(false);

        }

        #endregion

        private VMControls.Winform.Release.VmRenderControl Guankou_vmRenderControl2;
        private VMControls.Winform.Release.VmRenderControl Tiebao_vmRenderControl1;
        private System.Windows.Forms.Button button1;
        private VMControls.Winform.Release.VmProcedureConfigControl vmProcedureConfigControl1;
    }
}