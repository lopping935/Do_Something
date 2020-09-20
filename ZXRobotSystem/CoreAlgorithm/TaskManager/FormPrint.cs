using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
using System.Reflection;
using ZXing;
using ZXing.QrCode;
using ZXing.QrCode.Internal;
using Microsoft.Win32;
namespace CoreAlgorithm.TaskManager
{
    public class FormPrint
    {
        private DataSet FDataSet;
        public static String mode_name = "新国标标牌 .frx";
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
        private static IniSqlConfigInfo inisql = new IniSqlConfigInfo(System.AppDomain.CurrentDomain.BaseDirectory + "Config.ini");
        static TasksManager tm; //DbHelper db = new DbHelper(inisql.GetConnectionString("SysSQL"));
        public FormPrint()
        {
            tm = new TasksManager();
            auto_Work();
            Init_lable();
            
        }
        string Print1ip = "", Print2ip = "";//400PLC ip
        int Print1portr = 0, Print2portr = 0;//400PLC端口
        private void auto_Work()
        {
            
            string sql = "SELECT ACQUISITIONCONFIG_ID,DATAACQUISITION_IP,DATAACQUISITION_PORTR FROM ACQUISITIONCONFIG where ACQUISITIONCONFIG_ID=10 or ACQUISITIONCONFIG_ID=11";// or ACQUISITIONCONFIG_ID=8";
            DbDataReader dr = tm.MultithreadDataReader(sql);
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
        }
        string textBox_stand , manu_textBox_stand,textBox_heatno , manu_textBox_heatno,textBox_wegit , manu_textBox_weight,textBox_date1 , manu_textBox_date1,textBox_garde , manu_textBox_grade,textBox_size , manu_textBox_size,textBox_hook , manu_textBox__hook ,textBox_group , manu_textBox_group;
        string textBox_productionText,manu_textBox_productText,textBox_gradeText,manu_textBox_gradeText,textBox_weightText,manu_textBox_weightText,textBox_groupText,manu_textBox_groupText,textBox_countText,manu_textBox_countText,textBox_specificationText, manu_textBox_specifiText,textBox_sizeText,manu_textBox_sizeText,textBox_ProNoText,manu_textBox__proText,textBox_DateText,manu_textBox_dateText;
        string ID_LOT_PROD = "";
        Int16 ID_PART_LOT = 0;
        Int16 NUM_BDL = 0;
        Int16 SEQ_LEN = 0;
        Int16 SEQ_OPR = 0;
        string DES_FIPRO_SECTION = "";
        string BAR_CODE = "";
        LabelData PLClable;
        string barcodestring = "";
        private void Init_lable()
        {
            DbDataReader dr = null;
            string sql = "select * from SYSPARAMETER where PARAMETER_ID=1";
            dr = tm.MultithreadDataReader(sql);
            while (dr.Read())
            {
                if (dr["PARAMETER_VALUE"] != DBNull.Value)
                    mode_name = dr["PARAMETER_VALUE"].ToString();
            }
            dr.Close();

            double MAXRECID = 0;// PLANIDNow = 0;   
            //string sql = "select MAX(rownumberf) AS REC_ID from TLabelContent WHERE IMP_FINISH=31 or IMP_FINISH=32 or IMP_FINISH=33";
            sql = "select MAX(REC_ID) AS REC_ID from TLabelContent WHERE IMP_FINISH=31 or IMP_FINISH=32 or IMP_FINISH=33 or IMP_FINISH=55";

            dr = tm.MultithreadDataReader(sql);
            while (dr.Read())
            {
                if (dr["REC_ID"] != DBNull.Value)
                    MAXRECID = Convert.ToDouble(dr["REC_ID"].ToString());
            }
            dr.Close();
             sql = string.Format("select top 1 ItemPrint,STEEL_CODE_DESC,HT_NO,FUN_NO,SPEC_CP_DESC,NUM,NET_WEIGHT,ProTime,LotNo,XH,LENGTH,SCBZ,CREATED_CLASS from TLabelContent WHERE REC_ID>{0} AND IMP_FINISH=0 order by REC_ID ASC", MAXRECID);
            //sql = string.Format("select top 1 merge_sinbar,gk,heat_no,mtrl_no,spec,wegith,num_no,print_date,classes,sn_no from TLabelContent WHERE rownumberf>{0} AND IMP_FINISH=0 order by rownumberf ASC", MAXRECID);

            DataTable dt = tm.MultithreadDataTable(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PLClable.ItemPrint = dt.Rows[i]["ItemPrint"].ToString();//产品名称
                PLClable.ItemPrint = PLClable.ItemPrint.Substring(5, PLClable.ItemPrint.Length);
                PLClable.STEEL_CODE_DESC = dt.Rows[i]["STEEL_CODE_DESC"].ToString();//牌号
                PLClable.HT_NO = dt.Rows[i]["HT_NO"].ToString();//合同号
                PLClable.FUN_NO = dt.Rows[i]["FUN_NO"].ToString();//炉号
                PLClable.SPEC_CP_DESC = dt.Rows[i]["SPEC_CP_DESC"].ToString();//规格
                PLClable.NUM = int.Parse(dt.Rows[i]["NUM"].ToString());//支数
                PLClable.NET_WEIGHT = float.Parse(dt.Rows[i]["NET_WEIGHT"].ToString());//重量
                PLClable.ProTime = Convert.ToDateTime(dt.Rows[i]["ProTime"].ToString()).ToShortDateString();//DateTime.Parse(dt.Rows[i]["print_date"].ToString()).ToShortDateString();//日期
                PLClable.LotNo = dt.Rows[i]["LotNo"].ToString();//支数
                PLClable.XH = dt.Rows[i]["XH"].ToString();//重量
                PLClable.Length = dt.Rows[i]["LENGTH"].ToString();
                PLClable.SCBZ = dt.Rows[i]["SCBZ"].ToString();
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
               
            }
        }
        private void update_manu()//更新图片数据
        {
            textBox_stand = manu_textBox_stand;
            textBox_heatno = manu_textBox_heatno;
            textBox_wegit = manu_textBox_weight + " KG";
            textBox_date1 = manu_textBox_date1;
            textBox_garde = manu_textBox_grade;
            textBox_size = manu_textBox_size;
            textBox_hook = manu_textBox__hook + " 支";
            textBox_group = manu_textBox_group;
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
         //   g.DrawRectangle(blackPen, rect);
            //g.DrawRectangle(blackPen, rect_o);
            //grid : width:22mm high:10mm  blank width:85mm high:42.5

            g.DrawString(textBox_stand, font2, Brushes.Black, new Point(pix_to_mm(1.7), pix_to_mm(0.4)));
            g.DrawString(textBox_heatno, font2, Brushes.Black, new Point(pix_to_mm(1.7), pix_to_mm(1.4)));
            g.DrawString(textBox_wegit, font2, Brushes.Black, new Point(pix_to_mm(1.7), pix_to_mm(2.5)));
            g.DrawString(textBox_date1, font2, Brushes.Black, new Point(pix_to_mm(1.7), pix_to_mm(3.5)));
            g.DrawString(textBox_garde, font2, Brushes.Black, new Point(pix_to_mm(6.3), pix_to_mm(0.4)));
            g.DrawString(textBox_size, font2, Brushes.Black, new Point(pix_to_mm(5.8), pix_to_mm(1.4)));
            g.DrawString(textBox_hook, font2, Brushes.Black, new Point(pix_to_mm(5.3), pix_to_mm(2.5)));
            g.DrawString(textBox_group, font2, Brushes.Black, new Point(pix_to_mm(5.2), pix_to_mm(3.5)));

            QrCodeEncodingOptions options = new QrCodeEncodingOptions();
            options.DisableECI = true;
            options.ErrorCorrection = ErrorCorrectionLevel.M;
            options.Width = 210;
            options.Height = 210;
            options.Margin = 1;
            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            writer.Options = options;
            BAR_CODE = "http://www.yfgt.cn/b/#/barcode/" + textBox_heatno;
            Bitmap bmp = writer.Write(BAR_CODE);//不能识别汉字和英文字符
            g.DrawImage(bmp, new Point(pix_to_mm(6.7), pix_to_mm(2.4)));//画条形码
            img.RotateFlip(RotateFlipType.Rotate90FlipNone);//图像旋转                                                                                                  
        }
        //像素转mm
        public int pix_to_mm(double x)
        {
            int length;
            length = Convert.ToInt16(x * 118.11);
            return length;
        }

        //打印图片
        public static string GetWindowsServiceInstallPath(string ServiceName)
        {
            string key = @"SYSTEM\CurrentControlSet\Services\" + ServiceName;
            string path = Registry.LocalMachine.OpenSubKey(key).GetValue("ImagePath").ToString();
            //替换掉双引号
            path = path.Replace("\"", string.Empty);
            FileInfo fi = new FileInfo(path);
            return fi.Directory.ToString();
        }

        private void CreateDataSet()
        {
            #region create simple dataset with one table
            //Init_lable();
            //FDataSet = new DataSet();

            //DataTable table = new DataTable();
            //table.TableName = "PrintData";
            //FDataSet.Tables.Add(table);

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
            //table.Rows.Add(PLClable.gk, PLClable.mtrl_no, PLClable.heat_no, PLClable.spec, PLClable.num_no.ToString(), PLClable.order_num, PLClable.classes, PLClable.print_date, PLClable.wegith.ToString(), PLClable.merge_sinbar);//, 
            ////table.Rows.Add(2, "Nancy Davolio");
            ////table.Rows.Add(3, "Margaret Peacock");
            //Report report = new Report();
            //report.Load(GetWindowsServiceInstallPath("AnB_CoreAlgorithm")+ "/Print_Model/" + mode_name);
            //report.RegisterData(FDataSet);
            //report.Prepare();
            //ImageExport imge = new ImageExport();
            //imge.Resolution = 300;
            //report.Export(imge, "myReport1.jpg");//"myReport.jpg"
            //report.Dispose();
            #endregion
            Init_lable();
            Report report = new Report();
            report.Load("./Print_Model/" + mode_name);
            if (mode_name == "Print.frx")
            {
                if (PLClable.SCBZ.IndexOf("国标|") >= 0)
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
            report.SetParameterValue("WUZIMC", PLClable.ItemPrint);
            report.SetParameterValue("LH", PLClable.FUN_NO);
            report.SetParameterValue("GG", PLClable.SPEC_CP_DESC+ "X"+ PLClable.Length+"mm");
            report.SetParameterValue("ZS", PLClable.NUM);
            report.SetParameterValue("WEIGHT", PLClable.NET_WEIGHT*1000+"kg");
            
                
            report.SetParameterValue("SCRQ", PLClable.ProTime+"/"+PLClable.classes);
            report.SetParameterValue("KH", PLClable.LotNo + "-" + PLClable.XH);
            report.SetParameterValue("LGBARDCODE", barcodestring);
            report.Prepare();
            ImageExport imge = new ImageExport();
            imge.Resolution = 300;
            report.Export(imge, "myReport1.jpg");//"myReport.jpg"
            report.Dispose();
        }
        public void Send_SignsMessage()
        {
            try
            {
                byte[] sendArray = Enumerable.Repeat((byte)0x0, 94).ToArray();
                byte[] byteArray1 = BitConverter.GetBytes(Program.MessageFlg);
                byte[] byteArray2 = BitConverter.GetBytes(Program.PrintNum);
                byte[] byteArray3 = System.Text.Encoding.ASCII.GetBytes(ID_LOT_PROD);
                byte[] byteArray4= BitConverter.GetBytes(ID_PART_LOT);
                byte[] byteArray5 = BitConverter.GetBytes(NUM_BDL);
                byte[] byteArray6 = BitConverter.GetBytes(SEQ_LEN);
                byte[] byteArray7 = BitConverter.GetBytes(SEQ_OPR);
                byte[] byteArray8 = System.Text.Encoding.ASCII.GetBytes(DES_FIPRO_SECTION);
                byte[] byteArray9 = System.Text.Encoding.ASCII.GetBytes(BAR_CODE);
                Buffer.BlockCopy(byteArray1, 0, sendArray, 0, byteArray1.Length);
                Buffer.BlockCopy(byteArray2, 0, sendArray, 2, byteArray2.Length);
                Buffer.BlockCopy(byteArray3, 0, sendArray, 4, byteArray3.Length);
                Buffer.BlockCopy(byteArray4, 0, sendArray, 14, byteArray4.Length);
                Buffer.BlockCopy(byteArray5, 0, sendArray, 16, byteArray5.Length);
                Buffer.BlockCopy(byteArray6, 0, sendArray, 18, byteArray6.Length);
                Buffer.BlockCopy(byteArray7, 0, sendArray, 20, byteArray7.Length);
                Buffer.BlockCopy(byteArray8, 0, sendArray, 22, byteArray8.Length);
                Buffer.BlockCopy(byteArray9, 0, sendArray, 62, byteArray9.Length);
                if (sendArray.Length > 0)
                {
                    PLCSocketServer.senddata(sendArray);
                    string str = Program.MessageFlg.ToString() + " "+ Program.PrintNum.ToString() + " " + ID_LOT_PROD + " " + ID_PART_LOT.ToString() + " " + NUM_BDL.ToString() + " " + SEQ_LEN.ToString() + " " + SEQ_OPR.ToString() + " " + DES_FIPRO_SECTION + " " + BAR_CODE;
                    string sql = string.Format("INSERT INTO SENDLOG(REC_CREATE_TIME,CONTENT) VALUES ('{0}','{1}')", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), str);
                    tm.MultithreadExecuteNonQuery(sql);
                }

            }
            catch (Exception ex)
            {
                log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString() + "::" + MethodBase.GetCurrentMethod().ToString());
                Log.addLog(log, LogType.ERROR, ex.Message);
                Log.addLog(log, LogType.ERROR, ex.StackTrace);
            }//sendArray[38] = 50;(byte)'1';

        }
        public void button_handprinnt_Click(string PrintNO)
        {
            string Printip = "";
            if (PrintNO == "Print1")
                Printip = Print1ip;
            if (PrintNO == "Print2")
                Printip = Print2ip;
            Connection connection = new TcpConnection(Printip, TcpConnection.DEFAULT_ZPL_TCP_PORT);
            try
            {
                //creat_img();
                CreateDataSet();                
                connection.Open();
                ZebraPrinter printer = ZebraPrinterFactory.GetInstance(connection);
                PrinterStatus printerStatus = printer.GetCurrentStatus();
                string PrintMessage = "";
                if (printerStatus.isReadyToPrint)
                {
                    Bitmap imgtest = new Bitmap("myReport1.jpg");
                    PrintMessage ="start print！";
                    int x = 45;
                    int y = 110;
                    ZebraImageI zp1 = ZebraImageFactory.GetImage(imgtest);
                    printer.PrintImage(zp1, x, y, zp1.Width, zp1.Height, false);
                    //img.RotateFlip(RotateFlipType.Rotate270FlipNone);//图像旋转
                    Program.MessageFlg = 12;
                    imgtest.Dispose();
                }
                else if (printerStatus.isPaused)
                {
                    PrintMessage = "Cannot Print because the printer is paused.";
                    //if(Program.MessageFlg == 11)
                    //    Program.MessageFlg = 14;
                    //else
                        Program.MessageFlg = 13;
                }
                else if (printerStatus.isHeadOpen)
                {
                    PrintMessage = "Cannot Print because the printer head is open.";
                    //if (Program.MessageFlg == 11)
                    //    Program.MessageFlg = 14;
                    //else
                        Program.MessageFlg = 13;
                }
                else if (printerStatus.isPaperOut)
                {
                    PrintMessage = "Cannot Print because the Paperis Out.";
                    Program.MessageFlg = 13;

                }
                else
                {
                    PrintMessage = "Cannot Print.";

                    //if (Program.MessageFlg == 11)
                    //    Program.MessageFlg = 14;
                    //else
                        Program.MessageFlg = 13;

                }
                
            

            }
            catch (ConnectionException e1)
            {
                //if (Program.MessageFlg == 11)
                //    Program.MessageFlg = 14;
                //else
                    Program.MessageFlg = 13;
                log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString() + "::" + MethodBase.GetCurrentMethod().ToString());
                Log.addLog(log, LogType.ERROR, e1.ToString());
            }
            catch (ZebraPrinterLanguageUnknownException e2)
            {
                //if (Program.MessageFlg == 11)
                //    Program.MessageFlg = 14;
                //else
                    Program.MessageFlg = 13;

                log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString() + "::" + MethodBase.GetCurrentMethod().ToString());
                Log.addLog(log, LogType.ERROR, e2.ToString());
            }
            catch (IOException e3)
            {
                //if (Program.MessageFlg == 11)
                //    Program.MessageFlg = 14;
                //else
                    Program.MessageFlg = 13;

                log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString() + "::" + MethodBase.GetCurrentMethod().ToString());
                Log.addLog(log, LogType.ERROR, e3.ToString());
            }
            finally
            {
                connection.Close();

            }
        }
        #endregion
        #region look for the net printer
        private class NetworkDiscoveryHandler : DiscoveryHandler
        {

            private bool discoveryComplete = false;
            List<DiscoveredPrinter> printers = new List<DiscoveredPrinter>();

            public void DiscoveryError(string message)
            {
                log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString() + "::" + MethodBase.GetCurrentMethod().ToString());
                Log.addLog(log, LogType.INFO, ($"An error occurred during discovery: {message}."));
                discoveryComplete = true;
            }

            public void DiscoveryFinished()
            {log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString() + "::" + MethodBase.GetCurrentMethod().ToString());
                foreach (DiscoveredPrinter printer in printers)
                {
                    
                    Log.addLog(log, LogType.INFO, printer.ToString());
                }
                
                Log.addLog(log, LogType.INFO, ($"Discovered {printers.Count} Link-OS™ printers."));
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
                    log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString() + "::" + MethodBase.GetCurrentMethod().ToString());
                    Log.addLog(log, LogType.INFO, ("Starting printer discovery."));

                    NetworkDiscoveryHandler discoveryHandler = new NetworkDiscoveryHandler();
                    NetworkDiscoverer.FindPrinters(discoveryHandler);
                    while (!discoveryHandler.DiscoveryComplete)
                    {
                        System.Threading.Thread.Sleep(10);
                    }
                }
                catch (DiscoveryException e1)
                {
                    log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString() + "::" + MethodBase.GetCurrentMethod().ToString());
                    Log.addLog(log, LogType.ERROR, e1.ToString());
                }
                finally
                {

                    log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString() + "::" + MethodBase.GetCurrentMethod().ToString());
                    Log.addLog(log, LogType.INFO, "find no");
                }
            }
          
       
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
       
            //打印预览
             
            //手动打印
            

        }
        #endregion
    }
}
