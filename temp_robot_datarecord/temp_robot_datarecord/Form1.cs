using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Vision_Utlis;
using S7.Net;


namespace temp_robot_datarecord
{
    /*public struct plcdata example
    {
        public Int16 Heart;//心跳
        public bool TB_on_tempos;//铁包在测温位
        public float TB_weight;//铁水包重量        
        public Single TB_LIQID;//铁包液位
    }*/
    public struct plcdata
    {
        public bool datachgeflag;//
        public bool auto_flag;//
        public Int16 case_flag;
        public Int16 alarmdata;
        //public Single TB_LIQID;//
        //public Int16 Heart;//
    }
    public partial class Form1 : Form
    {
        int countnum = 0;
        static int[] PLCflag;//= { 30, 36, 49 ,65};
        static string[] signal_str;//后续自动配置
        static string[] str;//= new string[20];
        public static Plc robot_plc;
        public string plcip,heartmax,DBnum;
        public plcdata ZT_data = new plcdata();
        XMLHelper xxx;
        public string constring = "Data Source=127.0.0.1;Initial Catalog=temp_robot_datarecord;User ID=sa; Password=6923263;Enlist=true;Pooling=true;Max Pool Size=300;Min Pool Size=0;Connection Lifetime=300;packet size=1000";
        string normal_stop="";
        public dbTaskHelper db_plc_helper;
        public Form1()
        {
            
            InitializeComponent();
            db_plc_helper = new dbTaskHelper(constring);
            string path = AppDomain.CurrentDomain.BaseDirectory + "XMLTest.xml";
            xxx = new XMLHelper(path);

            plcip = xxx.XMLNodeSelectValue("PLC", "PLC1", "ip");
            heartmax= xxx.XMLNodeSelectValue("message", "setmessage", "stepmsg");
            string runstep = xxx.XMLNodeSelectValue("message", "setmessage", "runflag");
             DBnum = xxx.XMLNodeSelectValue("message", "setmessage", "DBnum");

            normal_stop = xxx.XMLNodeSelectValue("message", "setmessage", "normal_stop");

            signal_str = runstep.Split(new char[] { ';' });            
            PLCflag = new int[signal_str.Length];
            str = new string[signal_str.Length];
            for(int i=0;i< signal_str.Length;i++)
            {
                if(signal_str[i]!="")
                {
                    PLCflag[i] = int.Parse(signal_str[i]);
                }              
            }

            LogHelper.loginfo.Info("软件启动时间！");
            LogHelper.loginfo.Info("步骤总长：" + signal_str.Length.ToString());
            LogHelper.loginfo.Info("步骤序号："+ runstep);
            LogHelper.loginfo.Info("DB块号：" + DBnum.ToString());
            initplc();
            timer1.Enabled = true;
            //string sqlstr = string.Format("insert into workrecord values('{0}','{1}','{2}','','','','')", DateTime.Now.ToString(), DateTime.Now.ToString(), DateTime.Now.ToString());
            //db_plc_helper.MultithreadExecuteNonQuery(sqlstr);
        }
        public void initplc()
        {
            try
            {
                robot_plc = new Plc(CpuType.S7300, plcip, 0, 2);
                robot_plc.Open();//创建PLC实例
                robot_plc.Write("DB"+DBnum+".DBX0.0", false);
                LogHelper.loginfo.Info("PLC连接成功！");
            }
            catch (Exception e)
            {
                LogHelper.logerror.Error(e.Message);
            }
        }
        public void readplc()
        {
            ZT_data = (plcdata)robot_plc.ReadStruct(typeof(plcdata), int.Parse(DBnum));
        }
        string job_state = "";
        private void timer1_Tick(object sender, EventArgs e)
        {
            string priod_done = "", alarmtime = "";
            try
            {
                if ((bool)robot_plc.IsConnected)
                {
                    countnum++;
                    
                    if(countnum>=int.Parse(heartmax))
                    {
                        con_state.Text = "已连接";
                        //LogHelper.loginfo.Info("plc心跳正常！");
                        countnum = 0;
                    }
                    ZT_data = (plcdata)robot_plc.ReadStruct(typeof(plcdata), int.Parse(DBnum));
                    if (ZT_data.alarmdata != (Int16)0)
                    {
                        alarmtime = DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss"));
                        textBox6.Text = "";
                        textBox7.Text = "0";
                    }
                    if (ZT_data.datachgeflag)
                    {
                        robot_plc.Write("DB"+DBnum + ".DBX0.0", false);
                        
                        jobstate.Text = "任务进行中";
                        LogHelper.loginfo.Info("数据改变一次！");
                        
                        priod_done = ZT_data.case_flag.ToString();
                        now_job.Text = priod_done;
                        for (int i = 0; i < PLCflag.Length; i++)
                        {
                            if (priod_done == PLCflag[i].ToString())
                            {
                                str[i] = DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss"));
                                break;
                            }
                        }
                        textBox1.Text = str[0];
                        textBox2.Text = str[1];
                        textBox3.Text = str[2];
                        textBox4.Text = str[3];
                        textBox5.Text = str[4];
                        

                        //读取个别字符数据
                        //if (priod_done == "13")
                        //{
                        //    //object readzpos = AGFishOPCClient.ReadItem(Pro_Z);
                        //    //zpos = (float)Math.Round(float.Parse(readzpos.ToString()), 2);
                        //}

                    }
                    if (priod_done == normal_stop || ZT_data.alarmdata.ToString() != "0")
                    {
                        jobstate.Text = "当前任务完成";
                        now_job.Text = priod_done;
                        LogHelper.loginfo.Info("记录一次数据！");
                        priod_done = "";
                        //故障报警清零
                        robot_plc.Write("DB"+DBnum + ".DBX0.0", false);
                        string power_off = "1";
                        //个别字符和主体数据拼接
                        string sqltext1 = "insert into workrecord values(";
                        string sqltext2 = "";
                        for (int i = 0; i < str.Length; i++)
                        {
                            sqltext2 += "'" + str[i] + "'";
                            sqltext2 += ',';
                        }
                        string sqltext3 = sqltext1 + sqltext2 + string.Format("'{0}','{1}')", alarmtime, ZT_data.alarmdata.ToString());
                        db_plc_helper.MultithreadExecuteNonQuery(sqltext3);

                        //清除显示
                        str = new string[20];
                        foreach (Control control in this.panel1.Controls)
                        {
                            if (control is TextBox)
                            {
                                TextBox t = (TextBox)control;
                                t.Text = "";
                            }
                        }
                    }
                }
                else
                {
                    con_state.Text = "断开连接";
                    con_state.ForeColor = Color.Red;
                    
                    robot_plc.Close();
                    Thread.Sleep(5000);
                    con_state.Text = "重新连接";
                    LogHelper.loginfo.Info("网络重连一次！");
                    robot_plc.Open();

                }
            }
            catch (Exception ex)
            {
                LogHelper.logerror.Error(ex.Message);
                str = new string[20];
                foreach (Control control in this.panel1.Controls)
                {
                    if (control is TextBox)
                    {
                        TextBox t = (TextBox)control;
                        t.Text = "";
                    }
                }
            }
        }
        History_data data_from = null;
        private void button2_Click(object sender, EventArgs e)
        {
            data_from = new History_data();
            data_from.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LogHelper.loginfo.Info("测试记录一次！");
            foreach (Control control in this.panel1.Controls)
            {
                if(control is TextBox)
                {
                    TextBox t = (TextBox)control;
                    t.Text = "";
                }
            }

            str[0] = DateTime.Now.ToString();
            str[1] = DateTime.Now.ToString();
            str[2] = DateTime.Now.ToString();
            str[3] = DateTime.Now.ToString();
            
            string sqltext1 = "insert into workrecord values(";
            string sqltext2 = "";
            for (int i = 0; i < 5; i++)
            {
                sqltext2 += "'" + str[i] + "'";
                sqltext2 += ',';
                
            }
            textBox1.Text = str[0];
            textBox2.Text = str[1];
            textBox3.Text = str[2];
            textBox4.Text = str[3];
            textBox5.Text = str[4];
            textBox6.Text = "";
            textBox7.Text = "0";

            string sqltext3 = sqltext1 + sqltext2 + string.Format("'{0}','{1}')", "", ZT_data.alarmdata.ToString());
            db_plc_helper.MultithreadExecuteNonQuery(sqltext3);
            str = new string[20];
        }
    }
}
