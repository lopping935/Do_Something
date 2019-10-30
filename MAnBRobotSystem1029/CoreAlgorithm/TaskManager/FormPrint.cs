using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
using System.Reflection;
namespace CoreAlgorithm.TaskManager
{
    public class FormPrint
    {
        Bitmap img = new Bitmap(1160, 600);//712,500
        //public SocketClient PlcConnect = null;
        private static IniSqlConfigInfo inisql = new IniSqlConfigInfo(System.AppDomain.CurrentDomain.BaseDirectory + "Config.ini");
        static TasksManager tm; //DbHelper db = new DbHelper(inisql.GetConnectionString("SysSQL"));
        public FormPrint()
        {
            tm = new TasksManager();
            auto_Work();
            manu_Work();            
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
        string textBox_productionText,manu_textBox_productText,textBox_gradeText,manu_textBox_gradeText,textBox_weightText,manu_textBox_weightText,textBox_groupText,manu_textBox_groupText,textBox_countText,manu_textBox_countText,textBox_specificationText, manu_textBox_specifiText,textBox_sizeText,manu_textBox_sizeText,textBox_ProNoText,manu_textBox__proText,textBox_DateText,manu_textBox_dateText;
        string ID_LOT_PROD = "";
        Int16 ID_PART_LOT = 0;
        Int16 NUM_BDL = 0;
        Int16 SEQ_LEN = 0;
        Int16 SEQ_OPR = 0;
        string DES_FIPRO_SECTION = "";
        string BAR_CODE = "";
        private void manu_Work()
        {
            double MAXRECID = 0;// PLANIDNow = 0;                
            string sql = "select MAX(REC_ID) AS REC_ID from TLabelContent WHERE IMP_FINISH=31 or IMP_FINISH=32 or IMP_FINISH=33";
            DbDataReader dr = null;
            dr = tm.MultithreadDataReader(sql);
            while (dr.Read())
            {
                if (dr["REC_ID"] != DBNull.Value)
                    MAXRECID = Convert.ToDouble(dr["REC_ID"].ToString());
            }
            dr.Close();
            sql = string.Format("select top 1 ID_LOT_PROD,ID_PART_LOT,NUM_BDL,SEQ_LEN,SEQ_OPR,DES_FIPRO_SECTION,BAR_CODE,NAME_PROD,NAME_STLGD,LA_BDL_ACT,ID_CREW_CK,NUM_BAR,NAME_STND,DIM_LEN,ID_HEAT,TMSTP_WEIGH from TLabelContent WHERE REC_ID>{0} AND IMP_FINISH=0 order by REC_ID ASC", MAXRECID);
            //sql = "select top 2 SlabNO from TSlabNO WHERE PEN_FINISH!=1 order by REC_ID ASC";
            
            string NAME_PROD = "", NAME_STLGD = "", ID_CREW_CK = "", NAME_STND = "", ID_HEAT = "", TMSTP_WEIGH = "";
            float LA_BDL_ACT = 0;
            Int16 NUM_BAR = 0, DIM_LEN = 0;
            DataTable dt = tm.MultithreadDataTable(sql);
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
            manu_textBox_productText= NAME_PROD;
            manu_textBox_gradeText= NAME_STLGD;
            manu_textBox_weightText= LA_BDL_ACT.ToString();
            manu_textBox_groupText= ID_CREW_CK;
            manu_textBox_countText= NUM_BAR.ToString()+"/"+ NUM_BDL.ToString();
            manu_textBox_specifiText = NAME_STND;
            manu_textBox_sizeText = DES_FIPRO_SECTION + "/" + DIM_LEN.ToString();
            manu_textBox__proText=ID_HEAT + "-" + ID_LOT_PROD;
            manu_textBox_dateText = TMSTP_WEIGH;
        }
        private void update_manu()//更新图片数据
        {
            textBox_productionText = manu_textBox_productText;
            textBox_gradeText = manu_textBox_gradeText;
            textBox_weightText = manu_textBox_weightText;
            textBox_groupText = manu_textBox_groupText;
            textBox_countText = manu_textBox_countText;
            textBox_specificationText = manu_textBox_specifiText;
            textBox_sizeText = manu_textBox_sizeText;
            textBox_ProNoText = manu_textBox__proText;
            textBox_DateText = manu_textBox_dateText;
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
            g.DrawString(textBox_productionText, font2, Brushes.Black, new Point(pix_to_mm(4.4), pix_to_mm(0.4)));

            g.DrawString(textBox_gradeText, font2, Brushes.Black, new Point(pix_to_mm(2.2), pix_to_mm(1.2)));
            g.DrawString(textBox_weightText, font2, Brushes.Black, new Point(pix_to_mm(2.2), pix_to_mm(2.0)));
            g.DrawString(textBox_groupText, font2, Brushes.Black, new Point(pix_to_mm(2.2), pix_to_mm(2.8)));
            g.DrawString(textBox_countText, font2, Brushes.Black, new Point(pix_to_mm(2.2), pix_to_mm(3.6)));
            g.DrawString(textBox_specificationText, font2, Brushes.Black, new Point(pix_to_mm(7.4), pix_to_mm(1.2)));
            g.DrawString(textBox_sizeText, font2, Brushes.Black, new Point(pix_to_mm(7.4), pix_to_mm(2.0)));
            g.DrawString(textBox_ProNoText, font2, Brushes.Black, new Point(pix_to_mm(7.2), pix_to_mm(2.8)));
            //textBox_Date.Text = DateTime.Now.ToShortDateString();
            g.DrawString(textBox_DateText, font2, Brushes.Black, new Point(pix_to_mm(7.4), pix_to_mm(3.6)));

            string content = BAR_CODE;// "LG;" + textBox_productionText + ";" + textBox_gradeText + ";" + textBox_weightText + ";" + textBox_groupText + ";" + textBox_countText + ";" + textBox_specificationText + ";" + textBox_sizeText + ";" + textBox_ProNoText + ";" + textBox_DateText + "; Pro";

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
    
        //打印图片
        public void Send_SignsMessage()
        {try
            { byte[] sendArray = Enumerable.Repeat((byte)0x0, 92).ToArray();
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
               
                creat_img();
                connection.Open();
                ZebraPrinter printer = ZebraPrinterFactory.GetInstance(connection);
                PrinterStatus printerStatus = printer.GetCurrentStatus();
                string PrintMessage = "";
                if (printerStatus.isReadyToPrint)
                {
                    PrintMessage="start print！";
                    int x = 20;
                    int y = 50;
                    ZebraImageI zp1 = ZebraImageFactory.GetImage(img);
                    printer.PrintImage(zp1, x, y, zp1.Width, zp1.Height, false);
                    img.RotateFlip(RotateFlipType.Rotate270FlipNone);//图像旋转
                    Program.MessageFlg = 12;
                    Send_SignsMessage();
                }
                else if (printerStatus.isPaused)
                {
                    PrintMessage = "Cannot Print because the printer is paused.";
                    //MessageBox.Show("Cannot Print because the printer is paused.");  
                    if(Program.MessageFlg == 11)
                        Program.MessageFlg = 14;
                    else
                        Program.MessageFlg = 13;
                    //Send_SignsMessage();
                }
                else if (printerStatus.isHeadOpen)
                {
                    PrintMessage = "Cannot Print because the printer head is open.";
                    //MessageBox.Show("Cannot Print because the printer head is open.");
                    if (Program.MessageFlg == 11)
                        Program.MessageFlg = 14;
                    else
                        Program.MessageFlg = 13;
                    //Send_SignsMessage();
                }
                else if (printerStatus.isPaperOut)
                {
                    PrintMessage = "Cannot Print because the Paperis Out.";
                    //MessageBox.Show(printerStatus.isPaperOut.ToString());
                    Program.MessageFlg = 13;
                    //Send_SignsMessage();
                }
                else
                {
                    PrintMessage = "Cannot Print.";
                    //MessageBox.Show("Cannot Print.");  
                    if (Program.MessageFlg == 11)
                        Program.MessageFlg = 14;
                    else
                        Program.MessageFlg = 13;
                    //Send_SignsMessage();
                }
                log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString() + "::" + MethodBase.GetCurrentMethod().ToString());
                Log.addLog(log, LogType.INFO, PrintMessage);
            

            }
            catch (ConnectionException e1)
            {
                if (Program.MessageFlg == 11)
                    Program.MessageFlg = 14;
                else
                    Program.MessageFlg = 13;
                //Send_SignsMessage();
                log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString() + "::" + MethodBase.GetCurrentMethod().ToString());
                Log.addLog(log, LogType.ERROR, e1.ToString());
            }
            catch (ZebraPrinterLanguageUnknownException e2)
            {
                if (Program.MessageFlg == 11)
                    Program.MessageFlg = 14;
                else
                    Program.MessageFlg = 13;
                //Send_SignsMessage();
                log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString() + "::" + MethodBase.GetCurrentMethod().ToString());
                Log.addLog(log, LogType.ERROR, e2.ToString());
            }
            catch (IOException e3)
            {
                if (Program.MessageFlg == 11)
                    Program.MessageFlg = 14;
                else
                    Program.MessageFlg = 13;
                //Send_SignsMessage();
                log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString() + "::" + MethodBase.GetCurrentMethod().ToString());
                Log.addLog(log, LogType.ERROR, e3.ToString());
            }
            finally
            {
                connection.Close();

            }
        }
        
            //printp_image(PrintNO);
        
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
