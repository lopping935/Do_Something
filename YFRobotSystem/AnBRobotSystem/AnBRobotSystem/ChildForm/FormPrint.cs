using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
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
using ZXing;
using ZXing.QrCode;
using ZXing.QrCode.Internal;
namespace AnBRobotSystem.ChildForm
{
    public partial class FormPrint : Form
    {
        public struct LabelData
        {
            public string merge_sinbar;
            public string gk;
            public string heat_no;
            public string mtrl_no;
            public string spec;
            public int wegith;
            public int num_no;
            public string print_date;
            public string classes;
        };
        Bitmap img = new Bitmap(1030, 512);//712,500
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
            {
                if (Convert.ToInt16(dr["ACQUISITIONCONFIG_ID"]) == 10)
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
        private void manu_Work()
        {
            LabelData PLClable;
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
            sql = string.Format("select top 1 merge_sinbar,gk,heat_no,mtrl_no,spec,wegith,num_no,print_date,classes from TLabelContent WHERE REC_ID>{0} AND IMP_FINISH=0 order by REC_ID ASC", MAXRECID);

            DataTable dt = db.ExecuteDataTable(db.GetSqlStringCommond(sql));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PLClable.merge_sinbar = dt.Rows[i]["merge_sinbar"].ToString();//捆号
                PLClable.gk = dt.Rows[i]["gk"].ToString();//技术标准
                PLClable.heat_no = dt.Rows[i]["heat_no"].ToString();//炉批号
                PLClable.mtrl_no = dt.Rows[i]["mtrl_no"].ToString();//牌号
                PLClable.spec = dt.Rows[i]["spec"].ToString();//规格
                PLClable.wegith = int.Parse(dt.Rows[i]["wegith"].ToString());//重量
                PLClable.num_no = int.Parse(dt.Rows[i]["num_no"].ToString());//支数
                PLClable.print_date = dt.Rows[i]["print_date"].ToString();//DateTime.Parse(dt.Rows[i]["print_date"].ToString()).ToShortDateString();//日期
                PLClable.classes = dt.Rows[i]["classes"].ToString();//班次

                manu_textBox_stand.Text= PLClable.gk;
                manu_textBox_heatno.Text = PLClable.heat_no;
                manu_textBox_weight.Text = PLClable.wegith.ToString();
                manu_textBox_date1.Text = PLClable.print_date;
                manu_textBox_grade.Text = PLClable.mtrl_no;
                manu_textBox_size.Text = PLClable.spec;
                manu_textBox__hook.Text = PLClable.num_no.ToString();
                manu_textBox_group.Text = PLClable.classes;
                
            }
        }
        private void update_manu()//更新图片数据
        {
            textBox_stand.Text = manu_textBox_stand.Text;
            textBox_heatno.Text = manu_textBox_heatno.Text;
            textBox_wegit.Text = manu_textBox_weight.Text+" KG";
            textBox_date1.Text = manu_textBox_date1.Text;
            textBox_garde.Text = manu_textBox_grade.Text;
            textBox_size.Text = manu_textBox_size.Text;
            textBox_hook.Text = manu_textBox__hook.Text + " 支";
            textBox_group.Text = manu_textBox_group.Text;
            
        }
        #region pirnt the img
        //创建图片
        private  void creat_img()
        {
            update_manu();
            Graphics g = Graphics.FromImage(img);
            g.Clear(Color.White);

            Font font2 = new Font(FontFamily.GenericSansSerif, 25, FontStyle.Bold);
            Rectangle rect = new Rectangle(10, 0, img.Width - 20, img.Height - 20);
            Rectangle rect_o = new Rectangle(0, 0, 20, 20);
            Pen blackPen = new Pen(Color.Black, 3);
            g.DrawRectangle(blackPen, rect);
            g.DrawRectangle(blackPen, rect_o);
            //grid : width:22mm high:10mm  blank width:85mm high:42.5

            g.DrawString(textBox_stand.Text, font2, Brushes.Black, new Point(pix_to_mm(1.7), pix_to_mm(0.4)));
            g.DrawString(textBox_heatno.Text, font2, Brushes.Black, new Point(pix_to_mm(1.7), pix_to_mm(1.4)));
            g.DrawString(textBox_wegit.Text, font2, Brushes.Black, new Point(pix_to_mm(1.7), pix_to_mm(2.5)));
            g.DrawString(textBox_date1.Text, font2, Brushes.Black, new Point(pix_to_mm(1.7), pix_to_mm(3.5)));
            g.DrawString(textBox_garde.Text, font2, Brushes.Black, new Point(pix_to_mm(6.3), pix_to_mm(0.4)));
            g.DrawString(textBox_size.Text, font2, Brushes.Black, new Point(pix_to_mm(5.8), pix_to_mm(1.4)));
            g.DrawString(textBox_hook.Text, font2, Brushes.Black, new Point(pix_to_mm(5.3), pix_to_mm(2.5)));
            g.DrawString(textBox_group.Text, font2, Brushes.Black, new Point(pix_to_mm(5.2), pix_to_mm(3.5)));

            QrCodeEncodingOptions options = new QrCodeEncodingOptions();
            options.DisableECI = true;
            options.ErrorCorrection = ErrorCorrectionLevel.M;
            options.Width = 150;
            options.Height = 150;
            options.Margin = 1;
            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            writer.Options = options;
            BAR_CODE = "http://www.yfgt.cn/b/#/barcode/" + textBox_heatno.Text;
            Bitmap bmp = writer.Write(BAR_CODE);//不能识别汉字和英文字符
            g.DrawImage(bmp, new Point(pix_to_mm(6.5), pix_to_mm(2.2)));//画条形码
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
                    int x = 45;//上下位置 宽
                    int y = 110;//左右位置  长
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
        }

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
                get
                {
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
        #endregion


    }

}
