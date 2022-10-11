namespace AnBRobotSystem.ChildForm
{
    partial class Real_data
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.复制选中行ToolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.粘贴ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.Datemodel = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.uiDataGridView1 = new Sunny.UI.UIDataGridView();
            this.uiContextMenuStrip1 = new Sunny.UI.UIContextMenuStrip();
            this.hjToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iytgToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kjhToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.klhToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txt_ui_message_real = new Sunny.UI.UITextBox();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiDataGridView1)).BeginInit();
            this.uiContextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.ToolStripMenuItem,
            this.toolStripMenuItem3,
            this.复制选中行ToolStripMenuItem5,
            this.粘贴ToolStripMenuItem,
            this.toolStripMenuItem2,
            this.Datemodel});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(161, 158);
            this.contextMenuStrip1.Text = "停止自动刷新";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(160, 22);
            this.toolStripMenuItem1.Text = "删除选中行";
            // 
            // ToolStripMenuItem
            // 
            this.ToolStripMenuItem.Name = "ToolStripMenuItem";
            this.ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.ToolStripMenuItem.Text = "更新数据";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(160, 22);
            this.toolStripMenuItem3.Text = "插入数据";
            // 
            // 复制选中行ToolStripMenuItem5
            // 
            this.复制选中行ToolStripMenuItem5.Name = "复制选中行ToolStripMenuItem5";
            this.复制选中行ToolStripMenuItem5.Size = new System.Drawing.Size(160, 22);
            this.复制选中行ToolStripMenuItem5.Text = "复制选中行";
            // 
            // 粘贴ToolStripMenuItem
            // 
            this.粘贴ToolStripMenuItem.Name = "粘贴ToolStripMenuItem";
            this.粘贴ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.粘贴ToolStripMenuItem.Text = "粘贴数据";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(160, 22);
            this.toolStripMenuItem2.Text = "停止自动刷新";
            // 
            // Datemodel
            // 
            this.Datemodel.Name = "Datemodel";
            this.Datemodel.Size = new System.Drawing.Size(160, 22);
            this.Datemodel.Text = "从此处开始焊标";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 3000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // uiDataGridView1
            // 
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.uiDataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.uiDataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.uiDataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.uiDataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("微软雅黑", 12F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.uiDataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.uiDataGridView1.ColumnHeadersHeight = 32;
            this.uiDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("微软雅黑", 12F);
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.uiDataGridView1.DefaultCellStyle = dataGridViewCellStyle8;
            this.uiDataGridView1.EnableHeadersVisualStyles = false;
            this.uiDataGridView1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiDataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.uiDataGridView1.Location = new System.Drawing.Point(2, 58);
            this.uiDataGridView1.Name = "uiDataGridView1";
            this.uiDataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("微软雅黑", 12F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.uiDataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.uiDataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader;
            this.uiDataGridView1.RowHeight = 30;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.White;
            this.uiDataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.uiDataGridView1.RowTemplate.Height = 30;
            this.uiDataGridView1.SelectedIndex = -1;
            this.uiDataGridView1.ShowGridLine = true;
            this.uiDataGridView1.Size = new System.Drawing.Size(1250, 595);
            this.uiDataGridView1.TabIndex = 1;
            this.uiDataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.uiDataGridView1_CellContentClick);
            this.uiDataGridView1.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.uiDataGridView1_RowsAdded);
            this.uiDataGridView1.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.uiDataGridView1_RowStateChanged);
            // 
            // uiContextMenuStrip1
            // 
            this.uiContextMenuStrip1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiContextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hjToolStripMenuItem,
            this.iytgToolStripMenuItem,
            this.kjhToolStripMenuItem,
            this.klhToolStripMenuItem});
            this.uiContextMenuStrip1.Name = "uiContextMenuStrip1";
            this.uiContextMenuStrip1.Size = new System.Drawing.Size(109, 108);
            // 
            // hjToolStripMenuItem
            // 
            this.hjToolStripMenuItem.Name = "hjToolStripMenuItem";
            this.hjToolStripMenuItem.Size = new System.Drawing.Size(108, 26);
            this.hjToolStripMenuItem.Text = "hj";
            // 
            // iytgToolStripMenuItem
            // 
            this.iytgToolStripMenuItem.Name = "iytgToolStripMenuItem";
            this.iytgToolStripMenuItem.Size = new System.Drawing.Size(108, 26);
            this.iytgToolStripMenuItem.Text = "iytg";
            // 
            // kjhToolStripMenuItem
            // 
            this.kjhToolStripMenuItem.Name = "kjhToolStripMenuItem";
            this.kjhToolStripMenuItem.Size = new System.Drawing.Size(108, 26);
            this.kjhToolStripMenuItem.Text = "kjh";
            // 
            // klhToolStripMenuItem
            // 
            this.klhToolStripMenuItem.Name = "klhToolStripMenuItem";
            this.klhToolStripMenuItem.Size = new System.Drawing.Size(108, 26);
            this.klhToolStripMenuItem.Text = "klh";
            // 
            // txt_ui_message_real
            // 
            this.txt_ui_message_real.ButtonSymbol = 61761;
            this.txt_ui_message_real.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_ui_message_real.FillColor = System.Drawing.Color.White;
            this.txt_ui_message_real.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.txt_ui_message_real.Location = new System.Drawing.Point(326, 5);
            this.txt_ui_message_real.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_ui_message_real.Maximum = 2147483647D;
            this.txt_ui_message_real.Minimum = -2147483648D;
            this.txt_ui_message_real.MinimumSize = new System.Drawing.Size(1, 1);
            this.txt_ui_message_real.Name = "txt_ui_message_real";
            this.txt_ui_message_real.Size = new System.Drawing.Size(564, 51);
            this.txt_ui_message_real.TabIndex = 101;
            this.txt_ui_message_real.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txt_ui_message_real.Visible = false;
            // 
            // timer2
            // 
            this.timer2.Interval = 200;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // Real_data
            // 
            this.AllowAddControlOnTitle = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1254, 656);
            this.Controls.Add(this.txt_ui_message_real);
            this.Controls.Add(this.uiDataGridView1);
            this.Name = "Real_data";
            this.Padding = new System.Windows.Forms.Padding(5, 0, 5, 5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "折铁实时数据";
            this.Initialize += new System.EventHandler(this.Real_data_Initialize);
            this.Load += new System.EventHandler(this.Real_data_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiDataGridView1)).EndInit();
            this.uiContextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem 复制选中行ToolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem 粘贴ToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem Datemodel;
        private Sunny.UI.UIDataGridView uiDataGridView1;
        private Sunny.UI.UIContextMenuStrip uiContextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem hjToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iytgToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kjhToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem klhToolStripMenuItem;
        private Sunny.UI.UITextBox txt_ui_message_real;
        private System.Windows.Forms.Timer timer2;
    }
}