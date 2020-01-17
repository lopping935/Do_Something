
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using OPCAutomation;
using System.Data.SqlClient;
using TSCSDK;
using hanbiao;
using PDF417;
using PDF417.pdf417.encoder;
namespace printtest2
{
    public partial class 打印机程序 : Form
    {
        public 打印机程序()
        {
            InitializeComponent();
        }
        public TSCSDK.ethernet ip = new TSCSDK.ethernet();
        
         Bitmap img = new Bitmap(712,500);//712,500
        public OPCServer KepServer = new OPCServer();
        OPCGroups KepGroups;
        OPCGroup KepGroup;
        OPCItems KepItems;
        bool opc_connected = false;
        OPCItem item_printflag;//请求打印
        bool printflag_read;
        OPCItem item_print_autoflag;//手自动标志位
        OPCItem item_chanpin_name;//产品名称
        int chanpin_name_read;
        OPCItem item_paihao;//执行标准/牌号
        string paihao_read;
        OPCItem item_hetonghao;//合同号
        string hetonghao_read;
        OPCItem item_luhao;//炉号
        string luhao_read;
        OPCItem item_guige;//规格
        string guige_read;
        string guige_str = "";
        string guige_str_writeback = "";
        OPCItem item_changdu;//长度
        string changdu_read;
        string changdu_str = "";
        OPCItem item_zhahao;//轧号
        string zhahao_read;
        string zhahao_str = "";
        OPCItem item_kunhao;//捆号
        string kunhao_read;
        string kunhao_str = "";
        OPCItem item_zhishu;//支数
        int zhishu_read;
        OPCItem item_weight;//重量
        int weight_read;
        OPCItem item_produce_date;//生产日期
        string produce_date_read;
        OPCItem item_receiveprintflag;//收到打印信息
        OPCItem item_printer_fault;//打印机错误
        OPCItem item_printer_select;//重量
        int printer_select_read;
        SqlConnection conn;
        bool zd = true;
       
        private void printmessage_sd()//手动打印信息
        {
            txt_chanpin_name.Text = cb_chanpinname_sd.SelectedItem.ToString();
            txt_paihao.Text = execu_stand_sd.SelectedItem.ToString()+"/"+cb_paihao_sd.SelectedItem.ToString();           
            txt_hetonghao.Text = txt_hetonghao_sd.Text;
            txt_luhao.Text = txt_luhao_sd.Text;
            zhahao_str = txt_zhahao_sd.Text;
            kunhao_str = txt_kunhao_sd.Text;
            changdu_str = txt_changdu_sd.Text;
            guige_str = cb_guige_sd.SelectedItem.ToString();
            if (guige_str.IndexOf("φ") >= 0)
            {
                guige_str_writeback = guige_str.Replace("φ", "p");
            }
            else
            {
                guige_str_writeback = guige_str;
            }
            txt_guige.Text = guige_str + "X" + txt_changdu_sd.Text;
            txt_zhishu.Text = txt_zhishu_sd.Text;
            txt_weight.Text = txt_weight_sd.Text;
            txt_date.Text = txt_date_sd.Text;
        }

        private void bt_print_Click(object sender, EventArgs e)
        {
            printmessage_sd();
            printcmd();
        }

        public void printconnect()
        {
            try
            {
                //打印机连接
                ip.openport(txt_ip.Text.ToString(), Convert.ToInt32(port.Text.ToString()));
               
                string state = ip.printerstatus().ToString();

                if (state == "0")
                {
                    WriteItem("0", item_printer_fault);
                    txt_message.AppendText("打印机已连接!" + "\n");
                    ip.setup(textBox11.Text.ToString(), textBox12.Text.ToString(), "4", "12", "0", "0.2", "0.00");
                    ip.sendcommand("DIRECTION 0");
                    //ip.sendcommand("CLS");
                    ip.sendcommand("GAP,0mm,0.2");
                    ip.sendcommand("SET CUTTER 1");
                }
                else if (state == "32")
                {

                }
                else
                {
                    WriteItem("1", item_printer_fault);
                    txt_message.AppendText("打印机错误！" + "\n");
                  

                }
                
                timer_printerstate.Enabled = true;
            }
            catch (Exception ex)
            {
                txt_message.AppendText("打印机未连接！" + ex.ToString() + "\n");
               
                WriteItem("2", item_printer_fault);
            }
        }
        private void Connect_shujuku()//连接数据库
        {
            try
            {
                conn = new SqlConnection("server=192.168.0.201\\SL;database=hanbiao;uid=sa;pwd=123");
                //conn = new SqlConnection("server=192.168.99.66;database=dmes；uid=shibie;pwd=shibie611");
                conn.Open();
                //timer_sql.Enabled = true;
                //dgv_gg();
                //string strMsg = "数据库连接成功！";
                //txt_message.AppendText(strMsg + "\n");
              
            }
            catch (Exception ex)
            {
                txt_message.AppendText("数据库未连接！" + "\n");
            }



        }

        public void printcmd()
        {
            try
            {
                ip.sendcommand("CLS");
                 print_test();             
                ip.send_bitmap(0, 88, img);
                ip.printlabel("1", "1");
                string dt = DateTime.Now.ToString();
                txt_message.AppendText(dt + "开始打印!" + "\n");
                img.RotateFlip(RotateFlipType.Rotate270FlipNone);

                //pictureBox1.Image = img;


                // 数据写入数据库



                Connect_shujuku();
                string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                //DateTime currentTime = DateTime.Now;
                //string sql_ocrrp = "insert into OCR_RP(time,num_ocr,string_ocr)values( '" + currentTime + "','" + ocr_num + "','" + ocr_string + "')";
                string sql_hanbiao = "insert into message_hanbiao(time,productname,paihao,hetonghao,luhao,guige,zhishu,weight,productdate)values( '" + currentTime + "','" + cb_chanpinname_sd.SelectedItem.ToString() + "','" + txt_paihao.Text + "','" + txt_hetonghao.Text + "','" + txt_luhao.Text + "','" + txt_guige.Text + "','" + txt_zhishu.Text + "','" +int.Parse(txt_weight.Text) + "','" + txt_guige.Text + "')";
                SqlCommand cmd_ocrrp = new SqlCommand(sql_hanbiao, conn);
                cmd_ocrrp.ExecuteNonQuery();
                conn.Close();

            }

            catch (Exception ex)
            {
                //txt_message.AppendText(ex.ToString() + "\n");
                WriteItem("2", item_printer_fault);

            }
            
        }

        private void bt_connect_Click(object sender, EventArgs e)
        {

                printconnect();

        }

       

       

        private void service_connection_Click(object sender, EventArgs e)
        {
            Connect_Server();
        }
        private void Connect_Server()//连接服务器
        {
            try
            {

                if (!ConnectRemoteServer("127.0.0.1", "kepware.kepserverex.v4"))
                {
                    return;
                }

                if (!CreateGroup())
                {
                    return;
                }
                if (KepServer.ServerState == (int)OPCServerState.OPCRunning)
                {

                    txt_message.AppendText("已连接到:" + KepServer.ServerName + "\n");
                    
                    service_connection.BackColor = Color.LightGreen;
                    service_connection.Enabled = false;
                    opc_connected = true;
                }
                else
                {
                    //这里你可以根据返回的状态来自定义显示信息，请查看自动化接口API文档
                    txt_message.AppendText("状态：" + KepServer.ServerState.ToString() + "\n");
                  
                    service_connection.BackColor = Color.Red;
                    opc_connected = false;
                }
               
                item_printflag = AddItem("print_flag");//请求打印
                item_chanpin_name = AddItem("chanpin_name");//产品名称
                item_paihao = AddItem("paihao");//执行标准牌号
                item_hetonghao = AddItem("hetonghao");//合同号
                item_luhao = AddItem("luhao");//炉号
                item_zhahao = AddItem("zhahao");//轧号
                item_kunhao = AddItem("kunhao");//捆号
                item_print_autoflag = AddItem("print_autoflag");
                item_guige = AddItem("guige");//规格
                item_changdu = AddItem("changdu");//长度
                item_zhishu = AddItem("zhishu");//支数
                item_weight = AddItem("weight");//重量
                item_produce_date = AddItem("produce_date");//生产日期
                item_receiveprintflag = AddItem("receive_flag");//收到打印信息
                item_printer_fault= AddItem("printer_fault");//打印机错误
                item_printer_select = AddItem("printer_select");//打印机选择
                if (opc_connected)
                {
                    timer_readwrite.Enabled = true;
                }
            }
            catch (Exception err)
            {
                txt_message.AppendText("初始化出错：" + err.Message + "\n");
               
            }
        }
        private bool ConnectRemoteServer(string remoteServerIP, string remoteServerName)
        {
            try
            {
                KepServer.Connect(remoteServerName, remoteServerIP);//实例化kepserver对象

            }
            catch (Exception err)
            {
                txt_message.AppendText("连接远程服务器出现错误：" + err.Message + "\n");
                
                return false;
            }
            return true;
        }
        /// <summary>
        /// 创建组
        /// </summary>
        private bool CreateGroup()
        {
            try
            {
                KepGroups = KepServer.OPCGroups;
                KepGroup = KepGroups.Add("group1");
                KepServer.OPCGroups.DefaultGroupIsActive = true;
                KepServer.OPCGroups.DefaultGroupDeadband = 0;
                KepGroup.UpdateRate = 1000;
                KepGroup.IsActive = true;
                KepGroup.IsSubscribed = true;

                KepItems = KepGroup.OPCItems;

            }
            catch (Exception err)
            {
                //MessageBox.Show("创建组出现错误：" + err.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_message.AppendText("创建组出现错误：" + err.Message + "\n");
               
                return false;
            }
            return true;
        }

        private OPCItem AddItem(string tagname)
        {

            OPCItem myKepItem = null;
            string itemname;
            try
            {
                itemname = "HB.PLC." + tagname;
                myKepItem = KepItems.AddItem(itemname, 1);

                return myKepItem;
            }
            catch (Exception err)
            {
                //MessageBox.Show("添加Item错误！" + err.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_message.AppendText("添加Item错误！" + err.Message + "\n");
                
            }
            return myKepItem;
        }

        private void service_disconnect_Click(object sender, EventArgs e)
        {
            timer_readwrite.Enabled = false;
            if (!opc_connected)
            {
                return;
            }

            KepServer.Disconnect();

            //myTimer.Elapsed -= new System.Timers.ElapsedEventHandler(timer_read_write);

            opc_connected = false;
            if (!opc_connected)
            {
                txt_message.AppendText("已断开服务器" + "\n");
                //service_disconnect.BackColor = Color.Red;
               

            }
            service_connection.Enabled = true;
        }


        /// <summary>
        /// 读数据
        /// </summary>
        private object ReadItem(OPCItem myKepItem)
        {
            object myValue = 0, myQuality = 0, myTimeStamp = 0;
            try
            {
                myKepItem.Read((short)OPCDataSource.OPCDevice, out myValue, out myQuality, out myTimeStamp);
            }
            catch (Exception err)
            {
                txt_message.AppendText("读取" + myKepItem + "错误！" + err.Message + "\n");
               
                return "错误";
            }
            return myValue;
        }
        /// <summary>
        /// 写数据
        /// </summary>
        private void WriteItem(string myValue, OPCItem myKepItem)
        {

            try
            {
                Array Serverhandles, MyErrors;
                object[] valueTemp = new object[] { "", myValue };
                int[] temp = new int[2];
                temp[0] = 0;
                temp[1] = myKepItem.ServerHandle;

                Serverhandles = (Array)temp;
                KepGroup.SyncWrite(1, Serverhandles, valueTemp, out MyErrors);
            }
            catch (Exception err)
            {
                txt_message.AppendText("写入" + myKepItem + "错误！" + err.Message + "\n");
               
            }

        }
        bool flag_up = false;
        bool select1_up = false;
        bool select2_up = false;
        //int i = 0,j=0;
        private void timer_readwrite_Tick(object sender, EventArgs e)
        {
            printflag_read = (bool)ReadItem(item_printflag);//打印请求
            printer_select_read = (short)ReadItem(item_printer_select);//选择标示
            #region 更新数据
            if (zd)
            {
                chanpin_name_read = (short)ReadItem(item_chanpin_name);
                if (chanpin_name_read == 1)
                {
                    txt_chanpin_name.Text = "";
                }
                else if (chanpin_name_read == 2)
                {
                    txt_chanpin_name.Text = "DEFORMED BAR";
                }
                else if (chanpin_name_read == 3)
                {
                    txt_chanpin_name.Text = "HOT ROLLED ALLOY STEEL DEFORMED BAR";
                }
                else if (chanpin_name_read == 4)
                {
                    txt_chanpin_name.Text = "HOT ROLLED RIBBED STEEL BAR";
                }
                else if (chanpin_name_read == 5)
                {
                    txt_chanpin_name.Text = "钢筋混凝土用钢筋";
                }
                else if (chanpin_name_read == 6)
                {
                    txt_chanpin_name.Text = "钢筋混凝土用热轧带肋钢筋";
                }
                else if (chanpin_name_read == 7)
                {
                    txt_chanpin_name.Text = "预应力混凝土用螺纹钢筋";
                }
                else
                {
                    txt_chanpin_name.Text = "产品名称错误！！！！";
                    cb_chanpinname_sd.BackColor = Color.Red;
                }
                paihao_read = (string)ReadItem(item_paihao);
                txt_paihao.Text = paihao_read;
                hetonghao_read = (string)ReadItem(item_hetonghao);
                txt_hetonghao.Text = hetonghao_read;
                luhao_read = (string)ReadItem(item_luhao);
                txt_luhao.Text = luhao_read;
                kunhao_read = (string)ReadItem(item_kunhao);
                kunhao_str = kunhao_read;
                zhahao_read = (string)ReadItem(item_zhahao);
                zhahao_str = zhahao_read;
                guige_read = (string)ReadItem(item_guige);
                if (guige_read.IndexOf("p")>=0)
                {
                    guige_str = guige_read.Replace("p", "φ");
                }
                else
                {
                    guige_str = guige_read;
                }
                
                changdu_read = (string)ReadItem(item_changdu);
                changdu_str = changdu_read;
                txt_guige.Text = guige_str + "X" + changdu_read;
                zhishu_read = (short)ReadItem(item_zhishu);
                txt_zhishu.Text = zhishu_read.ToString();
                weight_read = (short)ReadItem(item_weight);
                txt_weight.Text = weight_read.ToString();
                produce_date_read = (string)ReadItem(item_produce_date);
                txt_date.Text = produce_date_read;
            }
            else
            {
                printmessage_sd();
            }
            #endregion
            if (printer_select_read == 1& select1_up == false)
            {
                ip.closeport();
                txt_ip.Text = txt_ip1.Text;
                
                lbl_printer1.BackColor = Color.Green;
                lbl_printer2.BackColor = Color.LightGray;
                printconnect();
                select1_up = true;
                select2_up = false;

            }
            if (printer_select_read == 2& select2_up == false)
            {
                ip.closeport();
                txt_ip.Text = txt_ip2.Text;
               
                lbl_printer2.BackColor = Color.Green;
                lbl_printer1.BackColor = Color.LightGray;
                printconnect();
                select1_up = false;
                select2_up = true;
            }
            if (printflag_read & flag_up == false)
            {
               
                WriteItem("0", item_printflag);
                if (!zd)
                {
                    WriteItem((cb_chanpinname_sd.SelectedIndex+1).ToString(), item_chanpin_name);
                    WriteItem(txt_paihao.Text, item_paihao);
                    WriteItem(txt_hetonghao.Text, item_hetonghao);
                    WriteItem(txt_luhao.Text, item_luhao);
                    WriteItem(kunhao_str, item_kunhao);
                    WriteItem(zhahao_str, item_zhahao);
                    WriteItem(guige_str_writeback, item_guige);
                    WriteItem(changdu_str, item_changdu);
                    WriteItem(txt_zhishu.Text, item_zhishu);
                    WriteItem(txt_weight.Text, item_weight);
                    WriteItem(txt_date.Text, item_produce_date);
                    WriteItem("1", item_print_autoflag);


                }
                else
                {
                    WriteItem("0", item_print_autoflag);
                }
                flag_up = true;
                #region 2号打印机数据更新
                //if(zd)
                //{
                //    chanpin_name_read = (short)ReadItem(item_chanpin_name);
                //    if (chanpin_name_read == 1)
                //    {
                //        txt_chanpin_name.Text ="";
                //    }
                //    else if (chanpin_name_read == 2)
                //    {
                //        txt_chanpin_name.Text = "DEFORMED BAR";
                //    }
                //    else if (chanpin_name_read == 3)
                //    {
                //        txt_chanpin_name.Text = "HOT ROLLED ALLOY STEEL DEFORMED BAR";
                //    }
                //    else if (chanpin_name_read == 4)
                //    {
                //        txt_chanpin_name.Text = "HOT ROLLED RIBBED STEEL BAR";
                //    }
                //    else if (chanpin_name_read == 5)
                //    {
                //        txt_chanpin_name.Text = "钢筋混凝土用钢筋";
                //    }
                //    else if (chanpin_name_read == 6)
                //    {
                //        txt_chanpin_name.Text = "钢筋混凝土用热轧带肋钢筋";
                //    }
                //    else if (chanpin_name_read == 7)
                //    {
                //        txt_chanpin_name.Text = "预应力混凝土用螺纹钢筋";
                //    }
                //    else
                //    {
                //        txt_chanpin_name.Text = "产品名称错误！！！！";
                //        cb_chanpinname_sd.BackColor = Color.Red;
                //    }
                //    paihao_read = (string)ReadItem(item_paihao);
                //    txt_paihao.Text = paihao_read;
                //    hetonghao_read = (string)ReadItem(item_hetonghao);
                //    txt_hetonghao.Text = hetonghao_read;
                //    luhao_read = (string)ReadItem(item_luhao);
                //    txt_luhao.Text = luhao_read;
                //    kunhao_read= (string)ReadItem(item_kunhao);
                //    kunhao_str = kunhao_read;
                //    zhahao_read = (string)ReadItem(item_zhahao);
                //    zhahao_str = zhahao_read;
                //    guige_read = (string)ReadItem(item_guige);
                //    guige_str = guige_read;
                //    changdu_read= (string)ReadItem(item_changdu);
                //    changdu_str = changdu_read;
                //    txt_guige.Text = guige_read+"X"+ changdu_read;
                //    zhishu_read = (short)ReadItem(item_zhishu);
                //    txt_zhishu.Text = zhishu_read.ToString();
                //    weight_read = (short)ReadItem(item_weight);
                //    txt_weight.Text = weight_read.ToString();
                //    produce_date_read = (string)ReadItem(item_produce_date);
                //    txt_date.Text = produce_date_read;
                //}
                //else
                //{
                //    printmessage_sd();
                //}

                #endregion
                //打印
                
                printcmd();
            }
            if (!printflag_read)
            {

                flag_up = false;
            }

            
        }

        private void 打印机程序_Load(object sender, EventArgs e)
        {
            ip.openport(txt_ip1.Text.ToString(), Convert.ToInt32(port.Text.ToString()));
          //  Connect_Server();
          //  Connect_shujuku();
            if (zd)
            {
                bt_sd_zd.Text = "自动填写";
                bt_sd_zd.BackColor = Color.LightGreen;
            }
            else
            {
                bt_sd_zd.Text = "手动填写";
                bt_sd_zd.BackColor = Color.Red;
            }
        }

        private void 打印机程序_FormClosing(object sender, FormClosingEventArgs e)
        {
            ip.closeport();
            
            KepServer.Disconnect();

        }

        private void bt_chaxun_Click(object sender, EventArgs e)
        {
            焊标查询 chaxunfrm = new 焊标查询();
            chaxunfrm.Show();
        }

        private void txt_con_shujuku_Click(object sender, EventArgs e)
        {
            Connect_shujuku();
        }

      

        private void bt_sd_zd_Click(object sender, EventArgs e)
        {
            if(zd)
            {
                zd = false;
                bt_sd_zd.Text = "手动填写";
                bt_sd_zd.BackColor = Color.Red;

            }
            else
            {
                zd = true;
                bt_sd_zd.Text = "自动填写";
                bt_sd_zd.BackColor = Color.LightGreen;
            }
        }

       

        private void timer_printerstate_Tick(object sender, EventArgs e)
        {
            try
            {
                string state = ip.printerstatus().ToString();

                if (state == "0" || state == "32")
                {
                    WriteItem("0", item_printer_fault);

                }
                else if(state == "1"|| state == "2" || state == "3" || state == "4" || state == "5" || state == "8" || state == "9" || state == "10" || state == "11" || state == "12" || state == "13" || state == "16")
                {
                    WriteItem("1", item_printer_fault);
                    txt_message.AppendText("打印机错误！" + "\n");
                   
                }
               else
                {
                    
                }
            }
            catch(Exception ex)
            {
                //txt_message.AppendText("打印机状态未读到！" + "\n");
                
                WriteItem("2", item_printer_fault);
            }

            
        }

       

        private void print_view_Click(object sender, EventArgs e)
        {
            printmessage_sd();
            print_test();
            

            img.RotateFlip(RotateFlipType.Rotate270FlipNone);
            print_view p1 = new print_view(img);
            p1.Show();
        }
        private void print_test()
        {
            Graphics g = Graphics.FromImage(img);
            g.Clear(Color.White);
           
            Font font2 = new Font("黑体", cb_chanpinname_sd.Font.Size, FontStyle.Regular);
            

            g.DrawString(txt_chanpin_name.Text, font2, Brushes.Black, new Point(txt_chanpin_name.Location.X, txt_chanpin_name.Location.Y));
            g.DrawString(txt_paihao.Text, font2, Brushes.Black, new Point(txt_paihao.Location.X, txt_paihao.Location.Y));
            g.DrawString(txt_hetonghao.Text, font2, Brushes.Black, new Point(txt_hetonghao.Location.X, txt_hetonghao.Location.Y));
            g.DrawString(txt_luhao.Text, font2, Brushes.Black, new Point(txt_luhao.Location.X, txt_luhao.Location.Y));
            g.DrawString(txt_guige.Text, font2, Brushes.Black, new Point(txt_guige.Location.X, txt_guige.Location.Y));
            g.DrawString(txt_zhishu.Text, font2, Brushes.Black, new Point(txt_zhishu.Location.X, txt_zhishu.Location.Y));
            g.DrawString(txt_weight.Text+"kg", font2, Brushes.Black, new Point(txt_weight.Location.X, txt_weight.Location.Y));
            g.DrawString(txt_date.Text, font2, Brushes.Black, new Point(txt_date.Location.X, txt_date.Location.Y));
           

            string content = "LG;" + zhahao_str + ";" + kunhao_str + ";" + guige_str + ";" + changdu_str + ";" + txt_zhishu.Text + ";" + txt_weight.Text + ";" + txt_luhao.Text + ";Pro";
            BarcodeWriter badw = new BarcodeWriter();
            
            badw.Format = BarcodeFormat.PDF_417;
            
            badw.Options.Hints.Add(EncodeHintType.CHARACTER_SET, "utf-8");
            badw.Options.Hints.Add(EncodeHintType.ERROR_CORRECTION, PDF417.pdf417.encoder.PDF417ErrorCorrectionLevel.L4);
            //badw.Options.Hints.Add(PDF417.pdf417.encoder.w);
            
           
            //badw.Options.Hints.Add(EncodeHintType.MAX_SIZE, 2);
            //badw.Options.Hints.Add(EncodeHintType.HEIGHT, 2);
             badw.Options.Height = 2;//8
            badw.o.Width = 50;

            //badw.Options.Margin = 0;
            
           PDF417.common.BitMatrix bm = badw.Encode(content);
            
            Bitmap img5 = new Bitmap(bm.Width, bm.Height);
            img5 = badw.Write(bm);
            
            
            g.DrawImage(img5, new Point(pdF417WinForm1.Location.X, pdF417WinForm1.Location.Y));//画条形码                                                                                               //img.Save("d:\\img1.bmp");                                                                                               //pictureBox1.Image = img;
            img.RotateFlip(RotateFlipType.Rotate90FlipNone);//图像旋转
            //pictureBox1.Image = img;

        }


    }
}
