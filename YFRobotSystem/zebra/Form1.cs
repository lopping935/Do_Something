using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Pdf417EncoderLibrary;
using Zebra.Sdk.Printer;
using Zebra.Sdk.Comm;
using System.IO;
using hanbiao;
using Zebra.Sdk.Graphics;
using Zebra.Sdk.Device;
using Zebra.Sdk.Printer.Discovery;
using Zebra.Sdk.Settings;
using SocketHelper;
using System.Configuration;
namespace zebra
{
    public partial class Form1 : Form
    {
        Bitmap img = new Bitmap(1160, 600);//712,500
        public SocketClient PlcConnect = null;

        public Form1()
        {
            InitializeComponent();
            auto_Work();
        }
        private void auto_Work()
        {
            txt_ip.Text = txt_ip1.Text;
        }
        private void manu_Work()
        {

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
            g.DrawString(textBox_ProNo.Text, font2, Brushes.Black, new Point(pix_to_mm(7.4), pix_to_mm(2.8)));
            textBox_Date.Text = DateTime.Now.ToShortDateString();
            g.DrawString(textBox_Date.Text, font2, Brushes.Black, new Point(pix_to_mm(7.4), pix_to_mm(3.6)));

            string content = "LG;" + textBox_production.Text + ";" + textBox_grade.Text + ";" + textBox_weight.Text + ";" + textBox_group.Text + ";" + textBox_count.Text + ";" + textBox_specification.Text + ";" + textBox_size.Text + ";" + textBox_ProNo.Text + ";" + textBox_Date.Text + "; Pro";

            Pdf417Encoder ptst = new Pdf417Encoder();
            ptst.ErrorCorrection = ErrorCorrectionLevel.AutoHigh;
            ptst.EncodingControl = EncodingControl.ByteOnly;
            ptst.RowHeight = 6;//设置每行像素大小最小是6 必须是NarrowBarWidth的三倍
            ptst.NarrowBarWidth = 2;
            ptst.DefaultDataColumns = 15;//设定总列数，以此计算 行数
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
    }
}
