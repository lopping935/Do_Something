using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Pdf417EncoderLibrary;
using PDF417;
using PDF417.pdf417.encoder;
using Zebra.Sdk.Printer;
using Zebra.Sdk.Comm;
using System.IO;
//using hanbiao;
using Zebra.Sdk.Graphics;
using Zebra.Sdk.Device;
using Zebra.Sdk.Printer.Discovery;
using Zebra.Sdk.Settings;
//using SocketHelper;
using System.Configuration;
using SQLPublicClass;
using System.Data.Common;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Reflection;

namespace AnBRobotSystem.ChildForm
{
    public partial class FormPrint : Form
    {
        Bitmap img = new Bitmap(1160, 600);//712,500
                                           //public SocketClient PlcConnect = null;
        string BAR_CODE = "";
        private static IniSqlConfigInfo inisql = new IniSqlConfigInfo(System.AppDomain.CurrentDomain.BaseDirectory + "Config.ini");
        DbHelper db = new DbHelper(inisql.GetConnectionString("SysSQL"));
        Socket raw;
        public FormPrint()
        {
            InitializeComponent();
            auto_Work();
            manu_Work();
        }
        private void auto_Work()
        {
            string Print1ip = "", Print2ip = "";//400PLC ip
            int Print1portr = 0, Print2portr = 0;//400PLC端口
            string sql = "SELECT ACQUISITIONCONFIG_ID,DATAACQUISITION_IP,DATAACQUISITION_PORTR FROM ACQUISITIONCONFIG where ACQUISITIONCONFIG_ID=10 or ACQUISITIONCONFIG_ID=11";// or ACQUISITIONCONFIG_ID=8";
            DbDataReader dr = db.ExecuteReader(db.GetSqlStringCommond(sql));
            while (dr.Read())
            {if (Convert.ToInt16(dr["ACQUISITIONCONFIG_ID"]) == 10)
             {
                    Print1ip = Convert.ToString(dr["DATAACQUISITION_IP"]);
                    Print1portr = Convert.ToInt16(dr["DATAACQUISITION_PORTR"]);
                }
                if (Convert.ToInt16(dr["ACQUISITIONCONFIG_ID"]) == 11)
                {
                    Print2ip = Convert.ToString(dr["DATAACQUISITION_IP"]);
                    Print2portr = Convert.ToInt16(dr["DATAACQUISITION_PORTR"]);
                }
            }
            dr.Close();
            txt_ip1.Text = Print1ip;
            txt_ip2.Text = Print2ip;
            port.Text = Print1portr.ToString();
            txt_ip.Text = txt_ip1.Text;
        }
        private void test()
        {

        }
        private void manu_Work()
        {
            double MAXRECID = 0;// PLANIDNow = 0;                
            string sql = "select MAX(REC_ID) AS REC_ID from TLabelContent WHERE IMP_FINISH=31 or IMP_FINISH=32 or IMP_FINISH=33";
            DbDataReader dr = null;
            dr = db.ExecuteReader(db.GetSqlStringCommond(sql));
            while (dr.Read())
            {
                if (dr["REC_ID"] != DBNull.Value)
                    MAXRECID = Convert.ToDouble(dr["REC_ID"].ToString());
            }
            dr.Close();
            sql = string.Format("select top 1 ID_LOT_PROD,ID_PART_LOT,NUM_BDL,SEQ_LEN,SEQ_OPR,DES_FIPRO_SECTION,BAR_CODE,NAME_PROD,NAME_STLGD,LA_BDL_ACT,ID_CREW_CK,NUM_BAR,NAME_STND,DIM_LEN,ID_HEAT,TMSTP_WEIGH from TLabelContent WHERE REC_ID>{0} AND IMP_FINISH=0 order by REC_ID ASC", MAXRECID);
            //sql = "select top 2 SlabNO from TSlabNO WHERE PEN_FINISH!=1 order by REC_ID ASC";
            string ID_LOT_PROD = "";
            Int16 ID_PART_LOT = 0;
            Int16 NUM_BDL = 0;
            Int16 SEQ_LEN = 0;
            Int16 SEQ_OPR = 0;
            string DES_FIPRO_SECTION = "";
           
            string NAME_PROD = "", NAME_STLGD = "", ID_CREW_CK = "", NAME_STND = "", ID_HEAT = "", TMSTP_WEIGH = "";
            float LA_BDL_ACT = 0;
            Int16 NUM_BAR = 0, DIM_LEN = 0;
            DataTable dt = db.ExecuteDataTable(db.GetSqlStringCommond(sql));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ID_LOT_PROD = dt.Rows[i]["ID_LOT_PROD"].ToString();
                ID_PART_LOT = Int16.Parse(dt.Rows[i]["ID_PART_LOT"].ToString());
                NUM_BDL = Int16.Parse(dt.Rows[i]["NUM_BDL"].ToString());
                SEQ_LEN = Int16.Parse(dt.Rows[i]["SEQ_LEN"].ToString());
                SEQ_OPR = Int16.Parse(dt.Rows[i]["SEQ_OPR"].ToString());
                DES_FIPRO_SECTION = dt.Rows[i]["DES_FIPRO_SECTION"].ToString();
                BAR_CODE = dt.Rows[i]["BAR_CODE"].ToString();
                NAME_PROD = dt.Rows[i]["NAME_PROD"].ToString();
                NUM_BAR = Int16.Parse(dt.Rows[i]["NUM_BAR"].ToString());
                DIM_LEN = Int16.Parse(dt.Rows[i]["DIM_LEN"].ToString());
                NAME_STLGD = dt.Rows[i]["NAME_STLGD"].ToString();
                ID_CREW_CK = dt.Rows[i]["ID_CREW_CK"].ToString();
                NAME_STND = dt.Rows[i]["NAME_STND"].ToString();
                ID_HEAT = dt.Rows[i]["ID_HEAT"].ToString();
                TMSTP_WEIGH = dt.Rows[i]["TMSTP_WEIGH"].ToString();
                LA_BDL_ACT = float.Parse(dt.Rows[i]["LA_BDL_ACT"].ToString());
            }
            manu_textBox_product.Text= NAME_PROD;
            manu_textBox_grade.Text= NAME_STLGD;
            manu_textBox_weight.Text= LA_BDL_ACT.ToString();
            manu_textBox_group.Text= ID_CREW_CK;
            manu_textBox_count.Text= NUM_BAR.ToString()+"/"+ NUM_BDL.ToString();
            manu_textBox_specifi.Text = NAME_STND;
            manu_textBox_size.Text = DES_FIPRO_SECTION + "/" + DIM_LEN.ToString();
            manu_textBox__pro.Text=ID_HEAT + "-" + ID_LOT_PROD;
            manu_textBox_date.Text = TMSTP_WEIGH;
        }
        private void update_manu()//更新图片数据
        {
            textBox_production.Text = manu_textBox_product.Text;
            textBox_grade.Text = manu_textBox_grade.Text;
            textBox_weight.Text = manu_textBox_weight.Text;
            textBox_group.Text = manu_textBox_group.Text;
            textBox_count.Text = manu_textBox_count.Text;
            textBox_specification.Text = manu_textBox_specifi.Text;
            textBox_size.Text = manu_textBox_size.Text;
            textBox_ProNo.Text = manu_textBox__pro.Text;
            textBox_Date.Text = manu_textBox_date.Text;
        }
        #region pirnt the img
        //创建图片
        private  void creat_img()
        {
            update_manu();
            Graphics g = Graphics.FromImage(img);
            // img.SetResolution(100,100);
            // img.SetPixel(10,10,Color.Black);
            g.Clear(Color.White);

            //Font font3 = new Font("黑体", cb_chanpinname_sd.Font.Size, FontStyle.Regular);
            Font font2 = new Font(FontFamily.GenericSansSerif, 20, FontStyle.Bold);
            Rectangle rect = new Rectangle(10, 10, img.Width - 20, img.Height - 20);
            Rectangle rect_o = new Rectangle(10, 10, 20, 20);
            Pen blackPen = new Pen(Color.Black, 3);
            g.DrawRectangle(blackPen, rect);
            g.DrawRectangle(blackPen, rect_o);
            //grid : width:22mm high:8mm  blank width:98mm high:48.5
            g.DrawString(textBox_production.Text, font2, Brushes.Black, new Point(pix_to_mm(4.4), pix_to_mm(0.4)));

            g.DrawString(textBox_grade.Text, font2, Brushes.Black, new Point(pix_to_mm(2.2), pix_to_mm(1.2)));
            g.DrawString(textBox_weight.Text, font2, Brushes.Black, new Point(pix_to_mm(2.2), pix_to_mm(2.0)));
            g.DrawString(textBox_group.Text, font2, Brushes.Black, new Point(pix_to_mm(2.2), pix_to_mm(2.8)));
            g.DrawString(textBox_count.Text, font2, Brushes.Black, new Point(pix_to_mm(2.2), pix_to_mm(3.6)));
            g.DrawString(textBox_specification.Text, font2, Brushes.Black, new Point(pix_to_mm(7.4), pix_to_mm(1.2)));
            g.DrawString(textBox_size.Text, font2, Brushes.Black, new Point(pix_to_mm(7.4), pix_to_mm(2.0)));
            g.DrawString(textBox_ProNo.Text, font2, Brushes.Black, new Point(pix_to_mm(7.2), pix_to_mm(2.8)));
            //textBox_Date.Text = DateTime.Now.ToShortDateString();
            g.DrawString(textBox_Date.Text, font2, Brushes.Black, new Point(pix_to_mm(7.4), pix_to_mm(3.6)));

            string content = BAR_CODE;// "LG;" + textBox_production.Text + ";" + textBox_grade.Text + ";" + textBox_weight.Text + ";" + textBox_group.Text + ";" + textBox_count.Text + ";" + textBox_specification.Text + ";" + textBox_size.Text + ";" + textBox_ProNo.Text + ";" + textBox_Date.Text + "; Pro";

            Pdf417Encoder ptst = new Pdf417Encoder();
            ptst.ErrorCorrection = ErrorCorrectionLevel.AutoHigh;
            ptst.EncodingControl = EncodingControl.ByteOnly;
            ptst.RowHeight = 9;//设置每行像素大小最小是6 必须是NarrowBarWidth的三倍
            ptst.NarrowBarWidth = 3;
            ptst.DefaultDataColumns = 8;//设定总列数，以此计算 行数
            ptst.Encode(content);
            Bitmap img6 = ptst.CreateBarcodeBitmap();
            g.DrawImage(img6, new Point(pix_to_mm(2.2), pix_to_mm(4.26)));//画条形码                                                                                               //img.Save("d:\\img1.bmp");                                                                                               //pictureBox1.Image = img;
            img.RotateFlip(RotateFlipType.Rotate90FlipNone);//图像旋转                                                                                              
        }
        //像素转mm
        public int pix_to_mm(double x)
        {
            int length;
            length = Convert.ToInt16(x * 118.11);
            return length;
        }
        void button_printview_Click(object sender, EventArgs e)
        {
            creat_img();
            img.RotateFlip(RotateFlipType.Rotate270FlipNone);
            print_view p1 = new print_view(img);
            p1.Show();
        }
        //打印图片
        void printp_image()
        {
            Connection connection = new TcpConnection(txt_ip.Text, TcpConnection.DEFAULT_ZPL_TCP_PORT);
            try
            {
                creat_img();
                connection.Open();
                ZebraPrinter printer = ZebraPrinterFactory.GetInstance(connection);
                PrinterStatus printerStatus = printer.GetCurrentStatus();
                if (printerStatus.isReadyToPrint)
                {
                    txt_message.AppendText("start print！");
                    int x = 20;
                    int y = 50;
                    ZebraImageI zp1 = ZebraImageFactory.GetImage(img);
                    printer.PrintImage(zp1, x, y, zp1.Width, zp1.Height, false);
                    img.RotateFlip(RotateFlipType.Rotate270FlipNone);//图像旋转
                }
                else if (printerStatus.isPaused)
                {
                    txt_message.AppendText("Cannot Print because the printer is paused.");
                    //MessageBox.Show("Cannot Print because the printer is paused.");                    
                }
                else if (printerStatus.isHeadOpen)
                {
                    txt_message.AppendText("Cannot Print because the printer head is open.");
                    //MessageBox.Show("Cannot Print because the printer head is open.");                    
                }
                else if (printerStatus.isPaperOut)
                {
                    txt_message.AppendText("Cannot Print because the Paperis Out.");
                    //MessageBox.Show(printerStatus.isPaperOut.ToString());
                }
                else
                {
                    txt_message.AppendText("Cannot Print.");
                    //MessageBox.Show("Cannot Print.");             
                }


            }
            catch (ConnectionException e1)
            {
                MessageBox.Show(e1.ToString());
            }
            catch (ZebraPrinterLanguageUnknownException e2)
            {
                MessageBox.Show(e2.ToString());
            }
            catch (IOException e3)
            {
                MessageBox.Show(e3.ToString());
            }
            finally
            {
                connection.Close();

            }
        }
        void button_handprinnt_Click(object sender, EventArgs e)
        {
            printp_image();
        }
        #endregion
        #region look for the net printer
        private class NetworkDiscoveryHandler : DiscoveryHandler
        {

            private bool discoveryComplete = false;
            List<DiscoveredPrinter> printers = new List<DiscoveredPrinter>();

            public void DiscoveryError(string message)
            {
                MessageBox.Show($"An error occurred during discovery: {message}.");
                discoveryComplete = true;
            }

            public void DiscoveryFinished()
            {
                foreach (DiscoveredPrinter printer in printers)
                {
                    MessageBox.Show(printer.ToString());
                }
                MessageBox.Show($"Discovered {printers.Count} Link-OS™ printers.");
                discoveryComplete = true;
            }

            public void FoundPrinter(DiscoveredPrinter printer)
            {
                printers.Add(printer);
            }

            public bool DiscoveryComplete
            {
                //get => discoveryComplete
                //}
                get {
                    return discoveryComplete;
                 }

            }

            public void show_Net()
            {
                try
                {
                    MessageBox.Show("Starting printer discovery.");

                    NetworkDiscoveryHandler discoveryHandler = new NetworkDiscoveryHandler();
                    NetworkDiscoverer.FindPrinters(discoveryHandler);
                    while (!discoveryHandler.DiscoveryComplete)
                    {
                        System.Threading.Thread.Sleep(10);
                    }
                }
                catch (DiscoveryException e1)
                {
                    MessageBox.Show(e1.ToString());
                }
                finally
                {

                    MessageBox.Show("find no");
                }
            }
            #endregion
            #region print the test string
            //tcp打印zpl命令
            private void SendZplOverTcp(string theIpAddress)
            {
                // Instantiate connection for ZPL TCP port at given address
                Connection thePrinterConn = new TcpConnection(theIpAddress, TcpConnection.DEFAULT_ZPL_TCP_PORT);

                try
                {
                    // Open the connection - physical connection is established here.
                    thePrinterConn.Open();

                    // This example prints "This is a ZPL test." near the top of the label.
                    string zplData = "^XA^FO20,20^A0N,25,25^FDThis is a ZPL test.^FS^XZ";

                    // Send the data to printer as a byte array.
                    thePrinterConn.Write(Encoding.UTF8.GetBytes(zplData));
                }
                catch (ConnectionException e)
                {
                    // Handle communications error here.
                    Console.WriteLine(e.ToString());
                }
                finally
                {
                    // Close the connection to release resources.
                    thePrinterConn.Close();
                }
            }
            //tcp打印char字符
            private void SendCpclOverTcp(string theIpAddress)
            {
                // Instantiate connection for CPCL TCP port at given address
                Connection thePrinterConn = new TcpConnection(theIpAddress, TcpConnection.DEFAULT_CPCL_TCP_PORT);

                try
                {
                    // Open the connection - physical connection is established here.
                    thePrinterConn.Open();

                    // This example prints "This is a CPCL test." near the top of the label.
                    string cpclData = "! 0 200 200 210 1\r\n"
                    + "TEXT 4 0 30 40 This is a CPCL test.\r\n"
                    + "FORM\r\n"
                    + "PRINT\r\n";

                    // Send the data to printer as a byte array.
                    thePrinterConn.Write(Encoding.UTF8.GetBytes(cpclData));
                }
                catch (ConnectionException e)
                {
                    // Handle communications error here.
                    Console.WriteLine(e.ToString());
                }
                finally
                {
                    // Close the connection to release resources.
                    thePrinterConn.Close();
                }
            }
            #endregion
            //打印预览
             
            //手动打印
            

        }
        public void MsgToTextBox(string msg)
        {
            if (textBox2.InvokeRequired)
            {
                Action<string> actionDelegate = (x) => { this.textBox1.Text += x.ToString(); };
                // 或者
                // Action<string> actionDelegate = delegate(string txt) { this.label2.Text = txt; };
                this.textBox1.Invoke(actionDelegate, msg);
            }
            else
            {
                textBox1.Text += msg;
            }
        }
        private void ListenRecall()
        {
            try
            {
                while (0 == 0)
                {
                    byte[] buffer = new byte[1024];
                    int byteCount = raw.Receive(buffer);
                    int SystemRun = 0;
                    string sql = "SELECT PARAMETER_VALUE FROM SYSPARAMETER where PARAMETER_ID=11";
                    DbDataReader dr = null;
                    dr = db.ExecuteReader(db.GetSqlStringCommond(sql));
                    while (dr.Read())
                    {
                        SystemRun = Convert.ToInt16(dr["PARAMETER_VALUE"]);
                    }
                    dr.Close();
                    if (SystemRun == 3)
                        break;
                        if (byteCount == 0)
                    {
                        break;
                    }
                    else
                    {
                        string msg = Encoding.Default.GetString(buffer);
                        MsgToTextBox(msg);
                    }
                }
            }
            catch (Exception ex)
            {
                MsgToTextBox(ex.Message);
            }
        }
        public void CreateRawSocket(string sprayip, int sprayports)
        {
            //IPAddress ip = IPAddress.Parse("127.0.0.1");
            //int port = 15786;
            IPAddress ip = IPAddress.Parse(sprayip);
            try
            {
                raw = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                raw.Connect(new IPEndPoint(ip, sprayports));
                label31.Text = "已连接";
            }
            catch (Exception ex)
            {
                textBox1.Text += ex.Message;
            }
        }

        public void SendMessage(string msg)
        {
            byte[] msgBytes = Encoding.ASCII.GetBytes(msg);
            raw.Send(msgBytes);
            textBox2.Text = "";
            foreach (byte b in msgBytes)
            {
                textBox2.Text += Convert.ToInt16(b).ToString("x").PadLeft(2, '0') + " ";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //string sprayip = "";//400PLC ip
            int sprayportr = 0, sprayports = 0;//400PLC端口
            string sql = "SELECT DATAACQUISITION_PORTR,DATAACQUISITION_PORTS FROM ACQUISITIONCONFIG where ACQUISITIONCONFIG_ID=15";// ";
            DbDataReader dr = db.ExecuteReader(db.GetSqlStringCommond(sql));
            while (dr.Read())
            {
                
                    sprayportr = Convert.ToInt32(dr["DATAACQUISITION_PORTR"]);
                    sprayports = Convert.ToInt32(dr["DATAACQUISITION_PORTS"]);
            }
            dr.Close();
            CreateRawSocket(textBox5.Text, sprayports);
            //创建监听进程
            Thread receiveThread = new Thread(ListenRecall);
            receiveThread.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            short rownum = Convert.ToInt16(textBox6.Text);
            double MAXRECID = 0;// PLANIDNow = 0;                
            string sql = "select MAX(REC_ID) AS REC_ID from TLabelContent WHERE IMP_FINISH=31 or IMP_FINISH=32 or IMP_FINISH=33";
            DbDataReader dr = null;
            dr = db.ExecuteReader(db.GetSqlStringCommond(sql));
            while (dr.Read())
            {
                if (dr["REC_ID"] != DBNull.Value)
                    MAXRECID = Convert.ToDouble(dr["REC_ID"].ToString());
            }
            dr.Close();
            for (int i = 1; i <= rownum; i++)
            {
                Thread.Sleep(500);
                if (i == 1)
                {
                    sql = "select RTDATA_VALUE from REALTIMETASKDATA where TASK_ID=1";
                    dr = db.ExecuteReader(db.GetSqlStringCommond(sql));
                    string ProductIDA = "";
                    while (dr.Read())
                    {
                        if (dr["RTDATA_VALUE"] != DBNull.Value)
                            ProductIDA = dr["RTDATA_VALUE"].ToString();
                        ProductIDA = ProductIDA.Substring(0, (ProductIDA.Length - 1));
                    }

                    sql = string.Format("select top 1 " + ProductIDA + "  from TLabelContent WHERE REC_ID>{0} AND IMP_FINISH=0 order by REC_ID ASC", MAXRECID);
                    dr = db.ExecuteReader(db.GetSqlStringCommond(sql));
                    string Product = "";
                    while (dr.Read())
                    {
                        if (ProductIDA.Contains("ID_HEAT") && dr["ID_HEAT"] != DBNull.Value)
                            Product = Product + dr["ID_HEAT"].ToString() + "   ";
                        if (ProductIDA.Contains("ID_LOT_PROD") && dr["ID_LOT_PROD"] != DBNull.Value)
                            Product = Product + dr["ID_LOT_PROD"].ToString() + "   ";
                        if (ProductIDA.Contains("NAME_STLGD") && dr["NAME_STLGD"] != DBNull.Value)
                            Product = Product + dr["NAME_STLGD"].ToString() + "   ";
                        if (ProductIDA.Contains("DIM_LEN") && dr["DIM_LEN"] != DBNull.Value)
                            Product = Product + dr["DIM_LEN"].ToString() + "   ";
                        if (ProductIDA.Contains("DES_FIPRO_SECTION") && dr["DES_FIPRO_SECTION"] != DBNull.Value)
                            Product = Product + dr["DES_FIPRO_SECTION"].ToString() + "   ";
                    }
                    Product = Product.Substring(0, (Product.Length - 3));
                    textBox3.Text = textBox3.Text+Product + "\r\n";
                    string msg = "external_field string " + i.ToString()+ " \"" + Product + "\"\r\n";// 
                    try
                    {
                        SendMessage(msg);
                    }
                    catch (Exception ex)
                    {
                        textBox1.Text += ex.Message;
                    }
                }
                if (i == 2)
                {
                    sql = "select RTDATA_VALUE from REALTIMETASKDATA where TASK_ID=2";
                    dr = db.ExecuteReader(db.GetSqlStringCommond(sql));
                    string ProductIDA = "";
                    while (dr.Read())
                    {
                        if (dr["RTDATA_VALUE"] != DBNull.Value)
                            ProductIDA = dr["RTDATA_VALUE"].ToString();
                        ProductIDA = ProductIDA.Substring(0, (ProductIDA.Length - 1));
                    }

                    sql = string.Format("select top 1 " + ProductIDA + "  from TLabelContent WHERE REC_ID>{0} AND IMP_FINISH=0 order by REC_ID ASC", MAXRECID);
                    dr = db.ExecuteReader(db.GetSqlStringCommond(sql));
                    string Product = "";
                    while (dr.Read())
                    {
                        if (ProductIDA.Contains("ID_HEAT") && dr["ID_HEAT"] != DBNull.Value)
                            Product = Product + dr["ID_HEAT"].ToString() + "   ";
                        if (ProductIDA.Contains("ID_LOT_PROD") && dr["ID_LOT_PROD"] != DBNull.Value)
                            Product = Product + dr["ID_LOT_PROD"].ToString() + "   ";
                        if (ProductIDA.Contains("NAME_STLGD") && dr["NAME_STLGD"] != DBNull.Value)
                            Product = Product + dr["NAME_STLGD"].ToString() + "   ";
                        if (ProductIDA.Contains("DIM_LEN") && dr["DIM_LEN"] != DBNull.Value)
                            Product = Product + dr["DIM_LEN"].ToString() + "   ";
                        if (ProductIDA.Contains("DES_FIPRO_SECTION") && dr["DES_FIPRO_SECTION"] != DBNull.Value)
                            Product = Product + dr["DES_FIPRO_SECTION"].ToString() + "   ";

                    }
                    Product = Product.Substring(0, (Product.Length - 3));
                    textBox3.Text = textBox3.Text + Product + "\r\n";
                    string msg = "external_field string " + i.ToString()+ " \"" + Product + "\"\r\n";// 
                    try
                    {
                        SendMessage(msg);
                    }
                    catch (Exception ex)
                    {
                        textBox1.Text += ex.Message;
                    }
                }
                if (i == 3)
                {
                    sql = "select RTDATA_VALUE from REALTIMETASKDATA where TASK_ID=3";
                    dr = db.ExecuteReader(db.GetSqlStringCommond(sql));
                    string ProductIDA = "";
                    while (dr.Read())
                    {
                        if (dr["RTDATA_VALUE"] != DBNull.Value)
                            ProductIDA = dr["RTDATA_VALUE"].ToString();
                        ProductIDA = ProductIDA.Substring(0, (ProductIDA.Length - 1));
                    }

                    sql = string.Format("select top 1 " + ProductIDA + "  from TLabelContent WHERE REC_ID>{0} AND IMP_FINISH=0 order by REC_ID ASC", MAXRECID);
                    dr = db.ExecuteReader(db.GetSqlStringCommond(sql));
                    string Product = "";
                    while (dr.Read())
                    {
                        if (ProductIDA.Contains("ID_HEAT") && dr["ID_HEAT"] != DBNull.Value)
                            Product = Product + dr["ID_HEAT"].ToString() + "   ";
                        if (ProductIDA.Contains("ID_LOT_PROD") && dr["ID_LOT_PROD"] != DBNull.Value)
                            Product = Product + dr["ID_LOT_PROD"].ToString() + "   ";
                        if (ProductIDA.Contains("NAME_STLGD") && dr["NAME_STLGD"] != DBNull.Value)
                            Product = Product + dr["NAME_STLGD"].ToString() + "   ";
                        if (ProductIDA.Contains("DIM_LEN") && dr["DIM_LEN"] != DBNull.Value)
                            Product = Product + dr["DIM_LEN"].ToString() + "   ";
                        if (ProductIDA.Contains("DES_FIPRO_SECTION") && dr["DES_FIPRO_SECTION"] != DBNull.Value)
                            Product = Product + dr["DES_FIPRO_SECTION"].ToString() + "   ";

                    }
                    Product = Product.Substring(0, (Product.Length - 3));
                    textBox3.Text = textBox3.Text + Product + "\r\n";
                    string msg = "external_field string "+ i.ToString()  + " \"" + Product + "\"\r\n";//
                    try
                    {
                        SendMessage(msg);
                    }
                    catch (Exception ex)
                    {
                        textBox1.Text += ex.Message;
                    }
                }
            }  
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sendmessage = "";
            short sendnum = 0;
            if (textBox6.Text=="1"|| textBox6.Text == "2")
            {
                sendnum = Convert.ToInt16(textBox6.Text);
            }
            else
            {
                MessageBox.Show("请输入数字1或2！");
                return;
            }
            
            
            string sql = string.Format("UPDATE SYSPARAMETER SET PARAMETER_VALUE= {0} WHERE PARAMETER_ID =9", sendnum);
            try
            {
                db.ExecuteNonQuery(db.GetSqlStringCommond(sql));
            }
            catch (System.Exception ex)
            {
                log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString() + "::" + MethodBase.GetCurrentMethod().ToString());
                Log.addLog(log, LogType.ERROR, ex.Message);
            }
            for (int i = 1; i <= sendnum; i++)
                {
                if (i == 1)
                    {
                    sendmessage = "";
                    if (radioButton1.Checked == true)
                            sendmessage = "ID_HEAT" +",";
                        if (radioButton6.Checked == true)
                            sendmessage = sendmessage+"ID_LOT_PROD" + ",";
                    if (radioButton9.Checked == true)
                        sendmessage = sendmessage + "NAME_STLGD" + ",";
                    if (radioButton12.Checked == true)
                        sendmessage = sendmessage + "DIM_LEN" + ",";
                    if (radioButton15.Checked == true)
                        sendmessage = sendmessage + "DES_FIPRO_SECTION"+"," ;//
                    if(sendmessage=="")
                    {
                        MessageBox.Show("请在“第一行”进行选择数据！");
                        return;
                    }
                    sql = "UPDATE REALTIMETASKDATA SET RTDATA_VALUE='"+ sendmessage+ "',RTDATA_TIME='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE TASK_ID=1";
                    try
                    {
                        db.ExecuteNonQuery(db.GetSqlStringCommond(sql));
                    }
                    catch (System.Exception ex)
                    {
                        log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString() + "::" + MethodBase.GetCurrentMethod().ToString());
                        Log.addLog(log, LogType.ERROR, ex.Message);
                    }
                }
                if (i == 2)
                {
                    sendmessage = "";
                    if (radioButton2.Checked == true)
                        sendmessage = "ID_HEAT" + ",";
                    if (radioButton5.Checked == true)
                        sendmessage = sendmessage + "ID_LOT_PROD" + ",";
                    if (radioButton8.Checked == true)
                        sendmessage = sendmessage + "NAME_STLGD" + ",";
                    if (radioButton11.Checked == true)
                        sendmessage = sendmessage + "DIM_LEN" + ",";
                    if (radioButton14.Checked == true)
                        sendmessage = sendmessage + "DES_FIPRO_SECTION" + ",";
                    sql = "UPDATE REALTIMETASKDATA SET RTDATA_VALUE='" + sendmessage + "',RTDATA_TIME='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE TASK_ID=2";
                    try
                    {
                        db.ExecuteNonQuery(db.GetSqlStringCommond(sql));
                    }
                    catch (System.Exception ex)
                    {
                        log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString() + "::" + MethodBase.GetCurrentMethod().ToString());
                        Log.addLog(log, LogType.ERROR, ex.Message);
                    }
                }
                
            }
        }

        private void FormPrint_Load(object sender, EventArgs e)
        {
            string sprayip = "";//400PLC ip
            string sql = "SELECT DATAACQUISITION_IP FROM ACQUISITIONCONFIG where ACQUISITIONCONFIG_ID=15";// ";
            DbDataReader dr = db.ExecuteReader(db.GetSqlStringCommond(sql));
            while (dr.Read())
            {
                sprayip = Convert.ToString(dr["DATAACQUISITION_IP"]);
                
            }
            dr.Close();
            textBox5.Text = sprayip;
        }
    } 
}
