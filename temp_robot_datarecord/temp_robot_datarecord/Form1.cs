using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GH_Utlis;
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
        public Int16 rotbot_flag;//
        public Int16 case_flag;
        public Int16 alarmdata;
        //public Single TB_LIQID;//
        //public Int16 Heart;//
    }
    public partial class Form1 : Form
    {
        public dbTaskHelper db_plc_helper = new dbTaskHelper();
        static int[] PLCflag = {10,30,40,49,65};
        static string[] signal_str;//后续自动配置
        static string[] str = new string[20];
        public static Plc robot_plc;
        public string plcip;
        public plcdata ZT_data = new plcdata();
        XMLHelper xxx;
       
        public Form1()
        {
           
            InitializeComponent();
            string path = AppDomain.CurrentDomain.BaseDirectory + "XMLTest.xml";
           xxx = new XMLHelper(path);
            plcip = xxx.XMLNodeSelectValue("PLC", "PLC1", "ip");
            //后续自动配置解析机器人动作代码
            //string signalstr = xxx.XMLNodeSelectValue("message", "STR1", "stepmsg");
            //signal_str = signalstr.Split(new char[] { ';' });
            LogHelper.loginfo.Info("开始记录！");

            string sqlstr=string.Format("insert into workrecord values('{0}','{1}','{2}','','','','')",DateTime.Now.ToString(), DateTime.Now.ToString(), DateTime.Now.ToString());
            db_plc_helper.MultithreadExecuteNonQuery(sqlstr);
        }
        public void initplc()
        {
            try
            {
                robot_plc = new Plc(CpuType.S71200, plcip, 0, 1);
                robot_plc.Open();//创建PLC实例              
            }
            catch (Exception e)
            {

            }
        }
        public void readplc()
        {
            ZT_data = (plcdata)robot_plc.ReadStruct(typeof(plcdata), 50);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            string priod_done = "", alarmtime = "";
           try
           {
                if ((bool)robot_plc.IsConnected)
                {
                    ZT_data = (plcdata)robot_plc.ReadStruct(typeof(plcdata), 50);
                    if(ZT_data.alarmdata!=(Int16)0)
                    {
                        alarmtime = DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss"));
                    }
                    if (ZT_data.datachgeflag)
                    {
                        robot_plc.Write("db50.dbx5.3", false);
                        priod_done = ZT_data.rotbot_flag.ToString();
                        for (int i = 0; i < PLCflag.Length; i++)
                        {
                            if (priod_done == PLCflag[i].ToString())
                            {
                                str[i] = DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss"));
                                break;
                            }
                        }
                        //读取个别字符数据
                        if (priod_done== "13")
                        {
                            //object readzpos = AGFishOPCClient.ReadItem(Pro_Z);
                            //zpos = (float)Math.Round(float.Parse(readzpos.ToString()), 2);
                        }
                        if (priod_done == "57")
                        {
                           // object pianyidata = AGFishOPCClient.ReadItem(pianyi);
                            //pianyistr = pianyidata.ToString();
                           // object banautodata = AGFishOPCClient.ReadItem(banauto);
                           // banautostr = banautodata.ToString();
                        }
                        if (priod_done == "24")
                        {
                            //work_result = "1";
                        }
                    }
                    if (priod_done == "27" || ZT_data .alarmdata.ToString()!= "0")
                    {
                        priod_done = "";
                        //故障报警清零
                        robot_plc.Write("db50.dbx5",(Int16)0);
                        string power_off = "1";
                        //个别字符和主体数据拼接
                        string sqltext1 = "";//string.Format("insert into workrecord values('{0}',{1},{2},{3},{4},'{5}','{6}','{7}','{8}',", Car_num, xpos, ypos, zpos, rz, work_result, pianyistr, banautostr, power_off);
                        string sqltext2 = "";
                        for (int i = 0; i < str.Length; i++)
                        {
                            sqltext2 += "'" + str[i] + "'";
                            sqltext2 += ',';
                        }
                        string sqltext3 = sqltext1 + sqltext2 + string.Format("'{0}','{1}')", ZT_data.alarmdata.ToString(), alarmtime);
                        db_plc_helper.MultithreadExecuteNonQuery(sqltext3);
                        str = new string[20];
                    }
                }
           }
           catch (Exception ex)
           {
               str = new string[20];
           }
        }
    }
}
