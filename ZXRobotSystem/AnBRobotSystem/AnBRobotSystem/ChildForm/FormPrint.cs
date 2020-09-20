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
using FastReport;
using FastReport.Export.Image;
using SQLPublicClass;
using System.Data.Common;
using System.Net;
using System.Net.Sockets;
using ZXing;
using ZXing.QrCode;
using ZXing.QrCode.Internal;
using System.Threading;

namespace AnBRobotSystem.ChildForm
{
    public partial class FormPrint : Form
    {
        private DataSet FDataSet;
        public struct LabelData
        {
            public string ItemPrint;
            public string STEEL_CODE_DESC;
            public string HT_NO;
            public string FUN_NO;
            public string SPEC_CP_DESC;
            public int NUM;
            public float NET_WEIGHT;
            public string LotNo;
            public string XH;
            public string ProTime;
            public string classes;
            public string order_num;
            public string Length;
            public string SCBZ;
        };
        Bitmap img = new Bitmap(1030, 512);//712,500
                                           //public SocketClient PlcConnect = null;
        public static String  mode_name = "";
        string BAR_CODE = "";
        private static IniSqlConfigInfo inisql = new IniSqlConfigInfo(System.AppDomain.CurrentDomain.BaseDirectory + "Config.ini");
        DbHelper db = new DbHelper(inisql.GetConnectionString("SysSQL"));
        Socket raw;
        public FormPrint()
        {
            InitializeComponent();
            auto_Work();
            Init_lable();
            DirectoryInfo dir = new DirectoryInfo(@"./Print_Model/");
            foreach (FileInfo d in dir.GetFiles())
            {
                if(d.Name.IndexOf(".frx")>0)
                comboBox1.Items.Add(d.Name);
            }
            DbDataReader dr = null;
            string sql = "select * from SYSPARAMETER where PARAMETER_ID=1";
            dr=db.ExecuteReader(db.GetSqlStringCommond(sql));
            if(dr.Read())
            {
                if(!dr.IsDBNull(3))
                {
                    mode_name = dr["PARAMETER_VALUE"].ToString();
                    comboBox1.Text = mode_name;
                }
                
            }
            dr.Close();
            if(mode_name=="")
            {
                mode_name = "新国标标牌.frx";
                sql = string.Format("UPDATE SYSPARAMETER SET PARAMETER_VALUE='{0}' where PARAMETER_ID=1", mode_name);
                db.ExecuteNonQuery(db.GetSqlStringCommond(sql));
                //this.FormBorderStyle = FormBorderStyle.None;
                //comboBox1.SelectedText = mode_name;
            }

            string heatno = "";
            string sqltext = "select top 1* from READ_TABLE where flag='N'";
            DataTable dt = db.ExecuteDataTable(db.GetSqlStringCommond(sqltext));
           // for (int i = 0; i < dt.Rows.Count; i++)
           if(dt.Rows.Count!=0)
            heatno = dt.Rows[0]["heat_no"].ToString();
            dt.Dispose();
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
            txt_ip.Text = txt_ip2.Text;
        }
        LabelData PLClable;
        string barcodestring = "";
        private void Init_lable()
        {          
            double MAXRECID = 0;// PLANIDNow = 0;                
            //string sql = "select MAX(rownumberf) AS REC_ID from TLabelContent WHERE IMP_FINISH=31 or IMP_FINISH=32 or IMP_FINISH=33";
            string sql = "select MAX(REC_ID) AS REC_ID from TLabelContent WHERE IMP_FINISH=31 or IMP_FINISH=32 or IMP_FINISH=33 or IMP_FINISH=55";
            DbDataReader dr = null;
            dr = db.ExecuteReader(db.GetSqlStringCommond(sql));
            while (dr.Read())
            {
                if (dr["REC_ID"] != DBNull.Value)
                    MAXRECID = Convert.ToDouble(dr["REC_ID"].ToString());
            }
            dr.Close();
           // sql = string.Format("select top 1 merge_sinbar,gk,heat_no,mtrl_no,spec,wegith,num_no,print_date,classes,sn_no from TLabelContent WHERE rownumberf>{0} AND IMP_FINISH=0 order by rownumberf ASC", MAXRECID);
            sql = string.Format("select top 1 ItemPrint,STEEL_CODE_DESC,HT_NO,FUN_NO,SPEC_CP_DESC,NUM,NET_WEIGHT,ProTime,LotNo,XH,LENGTH,SCBZ,CREATED_CLASS from TLabelContent WHERE REC_ID>{0} AND IMP_FINISH=0 order by REC_ID ASC", MAXRECID);

            DataTable dt = db.ExecuteDataTable(db.GetSqlStringCommond(sql));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PLClable.ItemPrint = dt.Rows[i]["ItemPrint"].ToString();//产品名称
                //PLClable.ItemPrint = PLClable.ItemPrint.Substring(5, PLClable.ItemPrint.Length);
                PLClable.STEEL_CODE_DESC = dt.Rows[i]["STEEL_CODE_DESC"].ToString();//牌号
                PLClable.HT_NO = dt.Rows[i]["HT_NO"].ToString();//合同号
                PLClable.FUN_NO = dt.Rows[i]["FUN_NO"].ToString();//炉号
                PLClable.SPEC_CP_DESC = dt.Rows[i]["SPEC_CP_DESC"].ToString();//规格
                PLClable.NUM = int.Parse(dt.Rows[i]["NUM"].ToString());//支数
                PLClable.NET_WEIGHT = float.Parse(dt.Rows[i]["NET_WEIGHT"].ToString());//重量
                PLClable.ProTime =Convert.ToDateTime( dt.Rows[i]["ProTime"].ToString()).ToShortDateString();//DateTime.Parse(dt.Rows[i]["print_date"].ToString()).ToShortDateString();//日期
                PLClable.LotNo = dt.Rows[i]["LotNo"].ToString();//轧号
                PLClable.XH = dt.Rows[i]["XH"].ToString();//捆号
                PLClable.Length= dt.Rows[i]["LENGTH"].ToString();
                PLClable.SCBZ = dt.Rows[i]["SCBZ"].ToString();
                //PLClable.classes = dt.Rows[i]["CREATED_CLASS"].ToString();
                switch (dt.Rows[i]["CREATED_CLASS"].ToString().Trim())
                {
                    case "甲":
                        PLClable.classes = "1";
                        break;
                    case "乙":
                        PLClable.classes = "2";
                        break;
                    case "丙":
                        PLClable.classes = "3";
                        break;
                    case "丁":
                        PLClable.classes = "4";
                        break;
                    default:
                        break;
                }
                barcodestring = "LG;" + PLClable.LotNo + ";" + PLClable.XH + ";" + PLClable.SPEC_CP_DESC + ";" + PLClable.Length + ";" + PLClable.NUM + ";" + PLClable.NET_WEIGHT + ";" + PLClable.FUN_NO + ";Pro";
                // PLClable.classes = dt.Rows[i]["classes"].ToString();//班次
                //PLClable.order_num = dt.Rows[i]["sn_no"].ToString();

                //manu_textBox_stand.Text= PLClable.ItemPrint;
                //manu_textBox_heatno.Text = PLClable.STEEL_CODE_DESC;
                //manu_textBox_weight.Text = PLClable.wegith.ToString();
                //manu_textBox_date1.Text = PLClable.print_date;
                //manu_textBox_grade.Text = PLClable.mtrl_no;
                //manu_textBox_size.Text = PLClable.spec;
                //manu_textBox__hook.Text = PLClable.num_no.ToString();
                //manu_textBox_group.Text = PLClable.classes + " / " + PLClable.order_num;

            }
        }
        private void update_manu()//更新图片数据
        {
            //textBox_stand.Text = manu_textBox_stand.Text;
            //textBox_heatno.Text = manu_textBox_heatno.Text;
            //textBox_wegit.Text = manu_textBox_weight.Text+" KG";
            //textBox_date1.Text = manu_textBox_date1.Text;
            //textBox_garde.Text = manu_textBox_grade.Text;
            //textBox_size.Text = manu_textBox_size.Text;
            //textBox_hook.Text = manu_textBox__hook.Text + " 支";
            //textBox_group.Text = manu_textBox_group.Text;            
        }
        #region pirnt the img
        //创建图片
        private  void creat_img()
        {
            update_manu();
            Graphics g = Graphics.FromImage(img);
            g.Clear(Color.White);





            Font font2 = new Font("Arial", 30, FontStyle.Regular);
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
            options.Width = 210;
            options.Height = 210;
            options.Margin = 1;
            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            writer.Options = options;
            BAR_CODE = "http://www.yfgt.cn/b/#/barcode/" + textBox_heatno.Text;
            Bitmap bmp = writer.Write(BAR_CODE);//不能识别汉字和英文字符
            g.DrawImage(bmp, new Point(pix_to_mm(6.6), pix_to_mm(2.3)));//画条形码
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
            //creat_img();
            CreateDataSet();
            Size s1 = new Size(300, 500);
            Image imgtest = new Bitmap("myReport.jpg");

            Bitmap img2 = new Bitmap(imgtest, s1);
            imgtest.Dispose();
            img2.RotateFlip(RotateFlipType.Rotate90FlipNone);
            print_view p1 = new print_view(img2);
            p1.Show();
        }
        //打印图片
        void printp_image()
        {
            Connection connection = new TcpConnection(txt_ip.Text, TcpConnection.DEFAULT_ZPL_TCP_PORT);
            try
            {
                CreateDataSet();
                
                Bitmap imgtest = new Bitmap("myReport.jpg");
                //imgtest.RotateFlip(RotateFlipType.Rotate180FlipNone);
                connection.Open();
                ZebraPrinter printer = ZebraPrinterFactory.GetInstance(connection);
                PrinterStatus printerStatus = printer.GetCurrentStatus();
                if (printerStatus.isReadyToPrint)
                {
                    txt_message.AppendText("start print！");
                    int x = 40;//上下位置 宽
                    int y = 110;//左右位置  长
                    ZebraImageI zp1 = ZebraImageFactory.GetImage(imgtest);
                   // imgtest.Dispose();
                    printer.PrintImage(zp1, x, y, zp1.Width, zp1.Height, false);
                    // img.RotateFlip(RotateFlipType.Rotate270FlipNone);//图像旋转
                    imgtest.Dispose();
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

        private void CreateDataSet()
        {
            // create simple dataset with one table

            #region create simple dataset with one table
            //FDataSet = new DataSet();

            //DataTable table = new DataTable();
            //table.TableName = "PrintData";
            //FDataSet.Tables.Add(table);
            //Init_lable();
            ////table.Columns.Add("ID", typeof(string));
            ////table.Columns.Add("NAME", typeof(string));
            ////table.Columns.Add("FUCKASS", typeof(string));
            //table.Columns.Add("T_STANDARD", typeof(string));
            //table.Columns.Add("GRADE_NAME", typeof(string));
            //table.Columns.Add("BATCH_CODE", typeof(string));
            //table.Columns.Add("SPE_NAME", typeof(string));
            //table.Columns.Add("HOOK_NUM", typeof(string));
            //table.Columns.Add("SN", typeof(string));
            //table.Columns.Add("GROUP_NUM", typeof(string));
            //table.Columns.Add("LABEL_DATE", typeof(string));
            //table.Columns.Add("MAT_FWEIGHT", typeof(string));
            //table.Columns.Add("MAT_SINBAR", typeof(string));
            //table.Rows.Add(PLClable.ItemPrint, PLClable.STEEL_CODE_DESC, PLClable.HT_NO, PLClable.FUN_NO, PLClable.SPEC_CP_DESC.ToString(), PLClable.NUM, PLClable.NET_WEIGHT);// , PLClable.print_date, PLClable.wegith.ToString(), PLClable.merge_sinbar
            ////table.Rows.Add(2, "Nancy Davolio");
            ////table.Rows.Add(3, "Margaret Peacock");
            //Report report = new Report();
            //report.Load("./Print_Model/"+ "Print.frx");
            //report.RegisterData(FDataSet);
            //report.Prepare();
            //ImageExport imge = new ImageExport();
            //imge.Resolution = 300;
            //report.Export(imge, "myReport.jpg");//"myReport.jpg"
            //report.Dispose();
            #endregion
            Init_lable();
            Report report = new Report();
            report.Load("./Print_Model/" + mode_name);
            if(mode_name== "Print.frx")
            {
                if(PLClable.SCBZ.IndexOf("国标|")>=0)
                {
                    PLClable.SCBZ = PLClable.SCBZ.Substring(3, PLClable.SCBZ.Length - 3);
                }
               
                report.SetParameterValue("ZXBZ", PLClable.SCBZ + "/" + PLClable.STEEL_CODE_DESC);
                report.SetParameterValue("GH", PLClable.HT_NO);
            }
            else
            {
                report.SetParameterValue("ZXBZ", PLClable.SCBZ);
                report.SetParameterValue("GH", PLClable.STEEL_CODE_DESC);
            }
            report.SetParameterValue("WUZIMC", PLClable.ItemPrint );
            report.SetParameterValue("LH", PLClable.FUN_NO);
            report.SetParameterValue("GG", PLClable.SPEC_CP_DESC + "X" + PLClable.Length + "mm");
            report.SetParameterValue("ZS", PLClable.NUM);
            report.SetParameterValue("WEIGHT", PLClable.NET_WEIGHT*1000+"kg");
            report.SetParameterValue("SCRQ", PLClable.ProTime + "/" + PLClable.classes);
            report.SetParameterValue("KH", PLClable.LotNo+"-"+PLClable.XH);
            report.SetParameterValue("LGBARDCODE", barcodestring);
            report.Prepare();
            ImageExport imge = new ImageExport();
            imge.Resolution = 300;
            report.Export(imge, "myReport.jpg");//"myReport.jpg"
            report.Dispose();

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

        private void button1_Click(object sender, EventArgs e)
        {
            
                Connection connection = new TcpConnection(txt_ip.Text, TcpConnection.DEFAULT_ZPL_TCP_PORT);
            try
            {
                connection.Open();
                ZebraPrinter printer = ZebraPrinterFactory.GetInstance(connection);
                PrinterStatus printerStatus = printer.GetCurrentStatus();
                if (printerStatus.isReadyToPrint)
                {
                    txt_message.AppendText("start print！");
                    int x = 30;//上下位置 宽
                    int y = 110;//左右位置  长
                    Bitmap b = new Bitmap("./myReport.jpg");
                    ZebraImageI zp1 = ZebraImageFactory.GetImage(b);
                    
                    printer.PrintImage(zp1, x, y, zp1.Width, zp1.Height, false);

                     img.RotateFlip(RotateFlipType.Rotate270FlipNone);//图像旋转
                    b.Dispose();
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            mode_name = comboBox1.SelectedItem.ToString();
            string sql = string.Format("UPDATE SYSPARAMETER SET PARAMETER_VALUE='{0}' where PARAMETER_ID=1", mode_name);
            db.ExecuteNonQuery(db.GetSqlStringCommond(sql));
        }
    }

}
