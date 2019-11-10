namespace printtest2
{
    partial class 打印机程序
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.bt_print = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txt_chanpin_name = new System.Windows.Forms.TextBox();
            this.txt_weight = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.pdF417WinForm1 = new BarcodeLib.Barcode.WinForms.PDF417WinForm();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_date = new System.Windows.Forms.TextBox();
            this.txt_zhishu = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_guige = new System.Windows.Forms.TextBox();
            this.txt_luhao = new System.Windows.Forms.TextBox();
            this.txt_hetonghao = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_paihao = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.txt_ip1 = new System.Windows.Forms.TextBox();
            this.port = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_printer2 = new System.Windows.Forms.Label();
            this.lbl_printer1 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.txt_ip2 = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.txt_ip = new System.Windows.Forms.TextBox();
            this.bt_connect = new System.Windows.Forms.Button();
            this.service_disconnect = new System.Windows.Forms.Button();
            this.service_connection = new System.Windows.Forms.Button();
            this.timer_readwrite = new System.Windows.Forms.Timer(this.components);
            this.timer_printerstate = new System.Windows.Forms.Timer(this.components);
            this.txt_message = new System.Windows.Forms.TextBox();
            this.bt_chaxun = new System.Windows.Forms.Button();
            this.bt_sd_zd = new System.Windows.Forms.Button();
            this.txt_weight_sd = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txt_date_sd = new System.Windows.Forms.TextBox();
            this.txt_zhishu_sd = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.txt_zhahao_sd = new System.Windows.Forms.TextBox();
            this.txt_luhao_sd = new System.Windows.Forms.TextBox();
            this.txt_hetonghao_sd = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.txt_kunhao_sd = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txt_changdu_sd = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.cb_chanpinname_sd = new System.Windows.Forms.ComboBox();
            this.cb_paihao_sd = new System.Windows.Forms.ComboBox();
            this.cb_guige_sd = new System.Windows.Forms.ComboBox();
            this.print_view = new System.Windows.Forms.Button();
            this.execu_stand_sd = new System.Windows.Forms.ComboBox();
            this.label36 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bt_print
            // 
            this.bt_print.Font = new System.Drawing.Font("宋体", 12F);
            this.bt_print.Location = new System.Drawing.Point(525, 69);
            this.bt_print.Name = "bt_print";
            this.bt_print.Size = new System.Drawing.Size(124, 35);
            this.bt_print.TabIndex = 0;
            this.bt_print.Text = "打印测试";
            this.bt_print.UseVisualStyleBackColor = true;
            this.bt_print.Click += new System.EventHandler(this.bt_print_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.txt_chanpin_name);
            this.panel1.Controls.Add(this.txt_weight);
            this.panel1.Controls.Add(this.label28);
            this.panel1.Controls.Add(this.pdF417WinForm1);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.txt_date);
            this.panel1.Controls.Add(this.txt_zhishu);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.txt_guige);
            this.panel1.Controls.Add(this.txt_luhao);
            this.panel1.Controls.Add(this.txt_hetonghao);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txt_paihao);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(28, 419);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(719, 500);
            this.panel1.TabIndex = 2;
            // 
            // txt_chanpin_name
            // 
            this.txt_chanpin_name.Font = new System.Drawing.Font("黑体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_chanpin_name.Location = new System.Drawing.Point(329, 114);
            this.txt_chanpin_name.Name = "txt_chanpin_name";
            this.txt_chanpin_name.Size = new System.Drawing.Size(270, 29);
            this.txt_chanpin_name.TabIndex = 23;
            this.txt_chanpin_name.Text = "钢筋混凝土用热轧带肋钢筋";
            // 
            // txt_weight
            // 
            this.txt_weight.Font = new System.Drawing.Font("黑体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_weight.Location = new System.Drawing.Point(599, 335);
            this.txt_weight.Name = "txt_weight";
            this.txt_weight.Size = new System.Drawing.Size(74, 29);
            this.txt_weight.TabIndex = 22;
            this.txt_weight.Text = "10";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label28.Location = new System.Drawing.Point(475, 339);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(87, 21);
            this.label28.TabIndex = 21;
            this.label28.Text = "WT.重量";
            // 
            // pdF417WinForm1
            // 
            this.pdF417WinForm1.AutoSize = true;
            this.pdF417WinForm1.BackgroundColor = System.Drawing.Color.White;
            this.pdF417WinForm1.BarRatio = 0.3333333F;
            this.pdF417WinForm1.BarWidth = 2F;
            this.pdF417WinForm1.BottomMargin = 0F;
            this.pdF417WinForm1.Columns = 5;
            this.pdF417WinForm1.Compact = false;
            this.pdF417WinForm1.Data = "PDF417";
            this.pdF417WinForm1.ECL = BarcodeLib.Barcode.PDF417ErrorCorrectionLevel.Level_2;
            this.pdF417WinForm1.Encoding = BarcodeLib.Barcode.PDF417Encoding.Text;
            this.pdF417WinForm1.ImageFormat = System.Drawing.Imaging.ImageFormat.Png;
            this.pdF417WinForm1.ImageHeight = 0F;
            this.pdF417WinForm1.ImageWidth = 0F;
            this.pdF417WinForm1.LeftMargin = 0F;
            this.pdF417WinForm1.Location = new System.Drawing.Point(254, 6);
            this.pdF417WinForm1.ModuleColor = System.Drawing.Color.Black;
            this.pdF417WinForm1.Name = "pdF417WinForm1";
            this.pdF417WinForm1.ProcessTilde = true;
            this.pdF417WinForm1.ResizeImage = false;
            this.pdF417WinForm1.Resolution = 96;
            this.pdF417WinForm1.RightMargin = 0F;
            this.pdF417WinForm1.Rotate = BarcodeLib.Barcode.RotateOrientation.BottomFacingDown;
            this.pdF417WinForm1.Rows = 4;
            this.pdF417WinForm1.Size = new System.Drawing.Size(308, 24);
            this.pdF417WinForm1.TabIndex = 20;
            this.pdF417WinForm1.TopMargin = 0F;
            this.pdF417WinForm1.UOM = BarcodeLib.Barcode.UnitOfMeasure.PIXEL;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label17.Location = new System.Drawing.Point(26, 415);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(76, 21);
            this.label17.TabIndex = 18;
            this.label17.Text = "Orihin";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.Location = new System.Drawing.Point(30, 372);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(65, 21);
            this.label16.TabIndex = 18;
            this.label16.Text = "Date.";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.Location = new System.Drawing.Point(30, 329);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(43, 21);
            this.label15.TabIndex = 18;
            this.label15.Text = "NR.";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(30, 286);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 21);
            this.label14.TabIndex = 18;
            this.label14.Text = "Size.";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(30, 243);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(98, 21);
            this.label13.TabIndex = 19;
            this.label13.Text = "Heat No.";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(30, 200);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(142, 21);
            this.label12.TabIndex = 18;
            this.label12.Text = "Contract No.";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(30, 157);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(131, 21);
            this.label11.TabIndex = 18;
            this.label11.Text = "SPEC./Grade";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(30, 119);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(168, 16);
            this.label10.TabIndex = 17;
            this.label10.Text = "Description of Goods";
            // 
            // txt_date
            // 
            this.txt_date.Font = new System.Drawing.Font("黑体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_date.Location = new System.Drawing.Point(330, 379);
            this.txt_date.Name = "txt_date";
            this.txt_date.Size = new System.Drawing.Size(270, 29);
            this.txt_date.TabIndex = 15;
            this.txt_date.Text = "2019-05-01/8";
            // 
            // txt_zhishu
            // 
            this.txt_zhishu.Font = new System.Drawing.Font("黑体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_zhishu.Location = new System.Drawing.Point(329, 338);
            this.txt_zhishu.Name = "txt_zhishu";
            this.txt_zhishu.Size = new System.Drawing.Size(74, 29);
            this.txt_zhishu.TabIndex = 14;
            this.txt_zhishu.Text = "85";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(199, 414);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(109, 21);
            this.label9.TabIndex = 13;
            this.label9.Text = "产    地:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(203, 376);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(109, 21);
            this.label8.TabIndex = 12;
            this.label8.Text = "生产日期:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(203, 334);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(109, 21);
            this.label7.TabIndex = 11;
            this.label7.Text = "支    数:";
            // 
            // txt_guige
            // 
            this.txt_guige.Font = new System.Drawing.Font("黑体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_guige.Location = new System.Drawing.Point(329, 295);
            this.txt_guige.Name = "txt_guige";
            this.txt_guige.Size = new System.Drawing.Size(270, 29);
            this.txt_guige.TabIndex = 10;
            this.txt_guige.Text = "φ25X9000mm";
            // 
            // txt_luhao
            // 
            this.txt_luhao.Font = new System.Drawing.Font("黑体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_luhao.Location = new System.Drawing.Point(329, 252);
            this.txt_luhao.Name = "txt_luhao";
            this.txt_luhao.Size = new System.Drawing.Size(270, 29);
            this.txt_luhao.TabIndex = 9;
            this.txt_luhao.Text = "Y186-11753";
            // 
            // txt_hetonghao
            // 
            this.txt_hetonghao.Font = new System.Drawing.Font("黑体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_hetonghao.Location = new System.Drawing.Point(329, 209);
            this.txt_hetonghao.Name = "txt_hetonghao";
            this.txt_hetonghao.Size = new System.Drawing.Size(270, 29);
            this.txt_hetonghao.TabIndex = 8;
            this.txt_hetonghao.Text = "LG12345678";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(203, 288);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(120, 21);
            this.label6.TabIndex = 7;
            this.label6.Text = "规    格：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(203, 246);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 21);
            this.label5.TabIndex = 6;
            this.label5.Text = "炉    号：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(203, 204);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 21);
            this.label4.TabIndex = 5;
            this.label4.Text = "合同批号：";
            // 
            // txt_paihao
            // 
            this.txt_paihao.Font = new System.Drawing.Font("黑体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_paihao.Location = new System.Drawing.Point(329, 166);
            this.txt_paihao.Name = "txt_paihao";
            this.txt_paihao.Size = new System.Drawing.Size(270, 29);
            this.txt_paihao.TabIndex = 4;
            this.txt_paihao.Text = "GB/T1499.2-2018/HRB400E";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("黑体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(164, 164);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(159, 19);
            this.label3.TabIndex = 3;
            this.label3.Text = "执行标准/牌号：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(203, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "产品名称：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("黑体", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(53, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 48);
            this.label1.TabIndex = 0;
            this.label1.Text = "莱钢";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(26, 36);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(71, 12);
            this.label18.TabIndex = 5;
            this.label18.Text = "IP1 地 址：";
            // 
            // txt_ip1
            // 
            this.txt_ip1.Location = new System.Drawing.Point(119, 32);
            this.txt_ip1.Name = "txt_ip1";
            this.txt_ip1.Size = new System.Drawing.Size(137, 21);
            this.txt_ip1.TabIndex = 6;
            this.txt_ip1.Text = "192.168.0.120";
            // 
            // port
            // 
            this.port.Location = new System.Drawing.Point(119, 76);
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(137, 21);
            this.port.TabIndex = 8;
            this.port.Text = "9100";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(26, 80);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(83, 12);
            this.label19.TabIndex = 7;
            this.label19.Text = "端 口 地 址：";
            // 
            // textBox11
            // 
            this.textBox11.Location = new System.Drawing.Point(219, 232);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(48, 21);
            this.textBox11.TabIndex = 10;
            this.textBox11.Text = "70";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(141, 238);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(77, 12);
            this.label20.TabIndex = 9;
            this.label20.Text = "标签纸宽度：";
            // 
            // textBox12
            // 
            this.textBox12.Location = new System.Drawing.Point(89, 232);
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(46, 21);
            this.textBox12.TabIndex = 12;
            this.textBox12.Text = "110";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(8, 240);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(77, 12);
            this.label21.TabIndex = 11;
            this.label21.Text = "标签纸高度：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbl_printer2);
            this.groupBox1.Controls.Add(this.lbl_printer1);
            this.groupBox1.Controls.Add(this.label26);
            this.groupBox1.Controls.Add(this.txt_ip2);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.label27);
            this.groupBox1.Controls.Add(this.txt_ip);
            this.groupBox1.Controls.Add(this.txt_ip1);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.port);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.textBox11);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.textBox12);
            this.groupBox1.Location = new System.Drawing.Point(28, 45);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(312, 268);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "打印机机参数设置";
            // 
            // lbl_printer2
            // 
            this.lbl_printer2.AutoSize = true;
            this.lbl_printer2.Font = new System.Drawing.Font("宋体", 9F);
            this.lbl_printer2.Location = new System.Drawing.Point(124, 204);
            this.lbl_printer2.Name = "lbl_printer2";
            this.lbl_printer2.Size = new System.Drawing.Size(47, 12);
            this.lbl_printer2.TabIndex = 26;
            this.lbl_printer2.Text = "打印机2";
            // 
            // lbl_printer1
            // 
            this.lbl_printer1.AutoSize = true;
            this.lbl_printer1.Font = new System.Drawing.Font("宋体", 9F);
            this.lbl_printer1.Location = new System.Drawing.Point(25, 204);
            this.lbl_printer1.Name = "lbl_printer1";
            this.lbl_printer1.Size = new System.Drawing.Size(47, 12);
            this.lbl_printer1.TabIndex = 25;
            this.lbl_printer1.Text = "打印机1";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(26, 124);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(71, 12);
            this.label26.TabIndex = 21;
            this.label26.Text = "IP2 地 址：";
            // 
            // txt_ip2
            // 
            this.txt_ip2.Location = new System.Drawing.Point(119, 120);
            this.txt_ip2.Name = "txt_ip2";
            this.txt_ip2.Size = new System.Drawing.Size(137, 21);
            this.txt_ip2.TabIndex = 22;
            this.txt_ip2.Text = "192.168.0.121";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(26, 168);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(59, 12);
            this.label27.TabIndex = 23;
            this.label27.Text = "IP地 址：";
            // 
            // txt_ip
            // 
            this.txt_ip.Location = new System.Drawing.Point(119, 164);
            this.txt_ip.Name = "txt_ip";
            this.txt_ip.Size = new System.Drawing.Size(137, 21);
            this.txt_ip.TabIndex = 24;
            // 
            // bt_connect
            // 
            this.bt_connect.Font = new System.Drawing.Font("宋体", 12F);
            this.bt_connect.Location = new System.Drawing.Point(369, 70);
            this.bt_connect.Name = "bt_connect";
            this.bt_connect.Size = new System.Drawing.Size(124, 35);
            this.bt_connect.TabIndex = 23;
            this.bt_connect.Text = "连接";
            this.bt_connect.UseVisualStyleBackColor = true;
//            this.bt_connect.Click += new System.EventHandler(this.bt_connect_Click);
            // 
            // service_disconnect
            // 
            this.service_disconnect.Font = new System.Drawing.Font("宋体", 12F);
            this.service_disconnect.Location = new System.Drawing.Point(369, 250);
            this.service_disconnect.Name = "service_disconnect";
            this.service_disconnect.Size = new System.Drawing.Size(124, 35);
            this.service_disconnect.TabIndex = 62;
            this.service_disconnect.Text = "服务器断开";
            this.service_disconnect.UseVisualStyleBackColor = true;
            this.service_disconnect.Click += new System.EventHandler(this.service_disconnect_Click);
            // 
            // service_connection
            // 
            this.service_connection.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.service_connection.Location = new System.Drawing.Point(369, 190);
            this.service_connection.Name = "service_connection";
            this.service_connection.Size = new System.Drawing.Size(124, 35);
            this.service_connection.TabIndex = 63;
            this.service_connection.Text = "服务器连接";
            this.service_connection.UseVisualStyleBackColor = true;
            this.service_connection.Click += new System.EventHandler(this.service_connection_Click);
            // 
            // timer_readwrite
            // 
            this.timer_readwrite.Interval = 500;
            this.timer_readwrite.Tick += new System.EventHandler(this.timer_readwrite_Tick);
            // 
            // timer_printerstate
            // 
            this.timer_printerstate.Interval = 10000;
            this.timer_printerstate.Tick += new System.EventHandler(this.timer_printerstate_Tick);
            // 
            // txt_message
            // 
            this.txt_message.AcceptsReturn = true;
            this.txt_message.Font = new System.Drawing.Font("宋体", 12F);
            this.txt_message.Location = new System.Drawing.Point(764, 419);
            this.txt_message.Multiline = true;
            this.txt_message.Name = "txt_message";
            this.txt_message.Size = new System.Drawing.Size(448, 500);
            this.txt_message.TabIndex = 168;
            // 
            // bt_chaxun
            // 
            this.bt_chaxun.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_chaxun.Location = new System.Drawing.Point(526, 130);
            this.bt_chaxun.Name = "bt_chaxun";
            this.bt_chaxun.Size = new System.Drawing.Size(124, 35);
            this.bt_chaxun.TabIndex = 169;
            this.bt_chaxun.Text = "查询打印记录";
            this.bt_chaxun.UseVisualStyleBackColor = true;
            this.bt_chaxun.Click += new System.EventHandler(this.bt_chaxun_Click);
            // 
            // bt_sd_zd
            // 
            this.bt_sd_zd.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_sd_zd.Location = new System.Drawing.Point(369, 130);
            this.bt_sd_zd.Name = "bt_sd_zd";
            this.bt_sd_zd.Size = new System.Drawing.Size(124, 35);
            this.bt_sd_zd.TabIndex = 172;
            this.bt_sd_zd.Text = "手动/自动";
            this.bt_sd_zd.UseVisualStyleBackColor = true;
            this.bt_sd_zd.Click += new System.EventHandler(this.bt_sd_zd_Click);
            // 
            // txt_weight_sd
            // 
            this.txt_weight_sd.Font = new System.Drawing.Font("宋体", 12F);
            this.txt_weight_sd.Location = new System.Drawing.Point(1041, 236);
            this.txt_weight_sd.Name = "txt_weight_sd";
            this.txt_weight_sd.Size = new System.Drawing.Size(74, 26);
            this.txt_weight_sd.TabIndex = 188;
            this.txt_weight_sd.Text = "3210";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("宋体", 12F);
            this.label22.Location = new System.Drawing.Point(955, 245);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(80, 16);
            this.label22.TabIndex = 187;
            this.label22.Text = "WT.重量：";
            // 
            // txt_date_sd
            // 
            this.txt_date_sd.Font = new System.Drawing.Font("宋体", 12F);
            this.txt_date_sd.Location = new System.Drawing.Point(853, 268);
            this.txt_date_sd.Name = "txt_date_sd";
            this.txt_date_sd.Size = new System.Drawing.Size(262, 26);
            this.txt_date_sd.TabIndex = 186;
            this.txt_date_sd.Text = "2019-05-01/8";
            // 
            // txt_zhishu_sd
            // 
            this.txt_zhishu_sd.Font = new System.Drawing.Font("宋体", 12F);
            this.txt_zhishu_sd.Location = new System.Drawing.Point(853, 236);
            this.txt_zhishu_sd.Name = "txt_zhishu_sd";
            this.txt_zhishu_sd.Size = new System.Drawing.Size(74, 26);
            this.txt_zhishu_sd.TabIndex = 185;
            this.txt_zhishu_sd.Text = "85";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("宋体", 12F);
            this.label24.Location = new System.Drawing.Point(767, 274);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(80, 16);
            this.label24.TabIndex = 183;
            this.label24.Text = "生产日期:";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("宋体", 12F);
            this.label25.Location = new System.Drawing.Point(767, 242);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(80, 16);
            this.label25.TabIndex = 182;
            this.label25.Text = "支    数:";
            // 
            // txt_zhahao_sd
            // 
            this.txt_zhahao_sd.Font = new System.Drawing.Font("宋体", 12F);
            this.txt_zhahao_sd.Location = new System.Drawing.Point(853, 366);
            this.txt_zhahao_sd.Name = "txt_zhahao_sd";
            this.txt_zhahao_sd.Size = new System.Drawing.Size(262, 26);
            this.txt_zhahao_sd.TabIndex = 181;
            // 
            // txt_luhao_sd
            // 
            this.txt_luhao_sd.Font = new System.Drawing.Font("宋体", 12F);
            this.txt_luhao_sd.Location = new System.Drawing.Point(853, 162);
            this.txt_luhao_sd.Name = "txt_luhao_sd";
            this.txt_luhao_sd.Size = new System.Drawing.Size(262, 26);
            this.txt_luhao_sd.TabIndex = 180;
            this.txt_luhao_sd.Text = "Y186-11753";
            // 
            // txt_hetonghao_sd
            // 
            this.txt_hetonghao_sd.Font = new System.Drawing.Font("宋体", 12F);
            this.txt_hetonghao_sd.Location = new System.Drawing.Point(853, 126);
            this.txt_hetonghao_sd.Name = "txt_hetonghao_sd";
            this.txt_hetonghao_sd.Size = new System.Drawing.Size(262, 26);
            this.txt_hetonghao_sd.TabIndex = 179;
            this.txt_hetonghao_sd.Text = "LG12345678";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("宋体", 12F);
            this.label29.Location = new System.Drawing.Point(767, 369);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(88, 16);
            this.label29.TabIndex = 178;
            this.label29.Text = "轧    号：";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("宋体", 12F);
            this.label30.Location = new System.Drawing.Point(767, 166);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(88, 16);
            this.label30.TabIndex = 177;
            this.label30.Text = "炉    号：";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("宋体", 12F);
            this.label31.Location = new System.Drawing.Point(767, 129);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(88, 16);
            this.label31.TabIndex = 176;
            this.label31.Text = "合同批号：";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("宋体", 12F);
            this.label32.Location = new System.Drawing.Point(767, 64);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(88, 16);
            this.label32.TabIndex = 174;
            this.label32.Text = "牌    号：";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label33.Location = new System.Drawing.Point(767, 34);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(88, 16);
            this.label33.TabIndex = 173;
            this.label33.Text = "产品名称：";
            // 
            // txt_kunhao_sd
            // 
            this.txt_kunhao_sd.Font = new System.Drawing.Font("宋体", 12F);
            this.txt_kunhao_sd.Location = new System.Drawing.Point(853, 300);
            this.txt_kunhao_sd.Name = "txt_kunhao_sd";
            this.txt_kunhao_sd.Size = new System.Drawing.Size(262, 26);
            this.txt_kunhao_sd.TabIndex = 191;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("宋体", 12F);
            this.label23.Location = new System.Drawing.Point(767, 303);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(88, 16);
            this.label23.TabIndex = 190;
            this.label23.Text = "捆    号：";
            // 
            // txt_changdu_sd
            // 
            this.txt_changdu_sd.Font = new System.Drawing.Font("宋体", 12F);
            this.txt_changdu_sd.Location = new System.Drawing.Point(853, 334);
            this.txt_changdu_sd.Name = "txt_changdu_sd";
            this.txt_changdu_sd.Size = new System.Drawing.Size(262, 26);
            this.txt_changdu_sd.TabIndex = 195;
            this.txt_changdu_sd.Text = "9000mm";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("宋体", 12F);
            this.label34.Location = new System.Drawing.Point(767, 337);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(88, 16);
            this.label34.TabIndex = 194;
            this.label34.Text = "长    度：";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("宋体", 12F);
            this.label35.Location = new System.Drawing.Point(767, 209);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(88, 16);
            this.label35.TabIndex = 192;
            this.label35.Text = "规    格：";
            // 
            // cb_chanpinname_sd
            // 
            this.cb_chanpinname_sd.Font = new System.Drawing.Font("宋体", 12F);
            this.cb_chanpinname_sd.FormattingEnabled = true;
            this.cb_chanpinname_sd.Items.AddRange(new object[] {
            "",
            "DEFORMED BAR",
            "HOT ROLLED ALLOY STEEL DEFORMED BAR",
            "HOT ROLLED RIBBED STEEL BAR",
            "钢筋混凝土用钢筋",
            "钢筋混凝土用热轧带肋钢筋",
            "预应力混凝土用螺纹钢筋"});
            this.cb_chanpinname_sd.Location = new System.Drawing.Point(853, 34);
            this.cb_chanpinname_sd.Name = "cb_chanpinname_sd";
            this.cb_chanpinname_sd.Size = new System.Drawing.Size(262, 24);
            this.cb_chanpinname_sd.TabIndex = 196;
            this.cb_chanpinname_sd.Text = "HOT ROLLED ALLOY STEEL DEFORMED BAR";
            // 
            // cb_paihao_sd
            // 
            this.cb_paihao_sd.Font = new System.Drawing.Font("宋体", 12F);
            this.cb_paihao_sd.FormattingEnabled = true;
            this.cb_paihao_sd.Items.AddRange(new object[] {
            "500B",
            "500N",
            "B500B",
            "Grade460",
            "Grade60",
            "HRB400",
            "HRB400E",
            "HRB500",
            "HRB500E",
            "HRB600",
            "HRB600E",
            "PSB830",
            "SD390",
            "SD400",
            "SD500"});
            this.cb_paihao_sd.Location = new System.Drawing.Point(853, 64);
            this.cb_paihao_sd.Name = "cb_paihao_sd";
            this.cb_paihao_sd.Size = new System.Drawing.Size(262, 24);
            this.cb_paihao_sd.TabIndex = 197;
            this.cb_paihao_sd.Text = "HRB500E";
            // 
            // cb_guige_sd
            // 
            this.cb_guige_sd.Font = new System.Drawing.Font("宋体", 12F);
            this.cb_guige_sd.FormattingEnabled = true;
            this.cb_guige_sd.Items.AddRange(new object[] {
            "1\"",
            "1-1/4\"",
            "1-1/4″",
            "1-1/8\"",
            "1-1/8″",
            "1-3/8\"",
            "3/4\"",
            "50",
            "7/8\"",
            "D19",
            "D20",
            "D22",
            "D24",
            "D25",
            "D28",
            "D32",
            "D41",
            "D47",
            "φ18",
            "φ19",
            "φ20",
            "φ22",
            "φ25",
            "φ28",
            "φ32",
            "φ36",
            "φ40",
            "φ50"});
            this.cb_guige_sd.Location = new System.Drawing.Point(853, 201);
            this.cb_guige_sd.Name = "cb_guige_sd";
            this.cb_guige_sd.Size = new System.Drawing.Size(262, 24);
            this.cb_guige_sd.TabIndex = 198;
            this.cb_guige_sd.Text = "φ25";
            // 
            // print_view
            // 
            this.print_view.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.print_view.Location = new System.Drawing.Point(525, 190);
            this.print_view.Name = "print_view";
            this.print_view.Size = new System.Drawing.Size(124, 35);
            this.print_view.TabIndex = 199;
            this.print_view.Text = "打印预览";
            this.print_view.UseVisualStyleBackColor = true;
            this.print_view.Click += new System.EventHandler(this.print_view_Click);
            // 
            // execu_stand_sd
            // 
            this.execu_stand_sd.Font = new System.Drawing.Font("宋体", 12F);
            this.execu_stand_sd.FormattingEnabled = true;
            this.execu_stand_sd.Items.AddRange(new object[] {
            "AS/NZS 4671:2001",
            "ASTM A615/A615M-09b",
            "ASTM A706/A706M-14",
            "BS4449:1988/CS2:1995",
            "BS4449:2005 +A2:2009",
            "BS4449:2005+A3:2016",
            "CS2: 2012 Grade",
            "CS2:1995",
            "CS2:2012",
            "DIN 488-1:2009-08",
            "DIN 488-2:2009",
            "GB/T 1499.2-2018",
            "GB/T 20065-2006",
            "GB/T1499.2-2007",
            "GB/T1499.2-2018",
            "GB/T20065-2016",
            "GB1499.2-2007",
            "JIS G 3112:2010",
            "KS D 3504:2011",
            "KS D3504:2016",
            "LGJX183-2016"});
            this.execu_stand_sd.Location = new System.Drawing.Point(853, 94);
            this.execu_stand_sd.Name = "execu_stand_sd";
            this.execu_stand_sd.Size = new System.Drawing.Size(262, 24);
            this.execu_stand_sd.TabIndex = 201;
            this.execu_stand_sd.Text = "GB/T1499.2-2018";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("宋体", 12F);
            this.label36.Location = new System.Drawing.Point(767, 94);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(88, 16);
            this.label36.TabIndex = 200;
            this.label36.Text = "执行标准：";
            // 
            // 打印机程序
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1224, 961);
            this.ControlBox = false;
            this.Controls.Add(this.execu_stand_sd);
            this.Controls.Add(this.label36);
            this.Controls.Add(this.print_view);
            this.Controls.Add(this.cb_guige_sd);
            this.Controls.Add(this.cb_paihao_sd);
            this.Controls.Add(this.cb_chanpinname_sd);
            this.Controls.Add(this.txt_changdu_sd);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.txt_kunhao_sd);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.txt_weight_sd);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.txt_date_sd);
            this.Controls.Add(this.txt_zhishu_sd);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.txt_zhahao_sd);
            this.Controls.Add(this.txt_luhao_sd);
            this.Controls.Add(this.txt_hetonghao_sd);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.label32);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.bt_sd_zd);
            this.Controls.Add(this.bt_chaxun);
            this.Controls.Add(this.txt_message);
            this.Controls.Add(this.service_disconnect);
            this.Controls.Add(this.service_connection);
            this.Controls.Add(this.bt_connect);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.bt_print);
            this.Name = "打印机程序";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "打印机程序";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.打印机程序_FormClosing);
            this.Load += new System.EventHandler(this.打印机程序_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bt_print;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_paihao;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_date;
        private System.Windows.Forms.TextBox txt_zhishu;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_guige;
        private System.Windows.Forms.TextBox txt_luhao;
        private System.Windows.Forms.TextBox txt_hetonghao;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private BarcodeLib.Barcode.WinForms.PDF417WinForm pdF417WinForm1;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txt_ip1;
        private System.Windows.Forms.TextBox port;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bt_connect;
        private System.Windows.Forms.Button service_disconnect;
        private System.Windows.Forms.Button service_connection;
        private System.Windows.Forms.Timer timer_readwrite;
        private System.Windows.Forms.Label lbl_printer1;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox txt_ip2;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txt_ip;
        private System.Windows.Forms.Label lbl_printer2;
        private System.Windows.Forms.Timer timer_printerstate;
        private System.Windows.Forms.TextBox txt_message;
        private System.Windows.Forms.TextBox txt_weight;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Button bt_chaxun;
        private System.Windows.Forms.Button bt_sd_zd;
        private System.Windows.Forms.TextBox txt_weight_sd;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txt_date_sd;
        private System.Windows.Forms.TextBox txt_zhishu_sd;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox txt_zhahao_sd;
        private System.Windows.Forms.TextBox txt_luhao_sd;
        private System.Windows.Forms.TextBox txt_hetonghao_sd;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.TextBox txt_kunhao_sd;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txt_changdu_sd;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.ComboBox cb_chanpinname_sd;
        private System.Windows.Forms.TextBox txt_chanpin_name;
        private System.Windows.Forms.ComboBox cb_paihao_sd;
        private System.Windows.Forms.ComboBox cb_guige_sd;
        private System.Windows.Forms.Button print_view;
        private System.Windows.Forms.ComboBox execu_stand_sd;
        private System.Windows.Forms.Label label36;
    }
}

