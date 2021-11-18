using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using S7.Net;
using System.Configuration;
using logtest;
using AnBRobotSystem.Utlis;
using System.Data.Common;
using System.Diagnostics;
using System.Threading;

namespace AnBRobotSystem.Core
{
    public struct plcdata
    {
        public Int16 Heart;//心跳
        public bool TB_on_tempos;//铁包在测温位
        public bool TB_on_DZpos;//铁包在吊装位
        public bool TB_pos;//铁包到位
        public bool GB_posA;//铁水包到位，可以用插电来获取
        public bool GB_posB;
        public bool GB_A_connect;//罐车得电
        public bool GB_B_connect;//罐车得电
        public bool GB_A_0_limt;//罐车0限位
        public bool GB_A_120_limt;//罐车0限位
        public bool GB_B_0_limt;//罐车0限位
        public bool GB_B_120_limt;//罐车120限位
        public bool GB_A_carIn;//火车到来信号  当前考虑是激光测距仪  如果不稳定可以考虑用插电代替
        public bool GB_B_carIn;
        public bool ZT_start_signal;//给plc的自动折铁开始信号 db50.dbx3.5
        public bool back2;
        public bool back3;
        public float TB_hight;//铁水液位
        public float TB_weight;//铁水包重量
        public Int16 TB_num;//铁水包号
        public Int16 GB_A_num;//罐车包号
        public Int16 GB_B_num;//罐车包号
        public Single GB_A_angle;//a实时倾角
        public Single GB_B_angle;//
        public Single GB_A_speed;//A设定速度 db50.dbd26
        public Single GB_B_speed;//B设定速度db50.dbd30
        public Single GB_A_Rspeed ;//A真实速度
        public Single GB_B_Rspeed;//B真实速度




    }
    
    public class PLCdata
    {
        public  LogManager my_log = new LogManager();
        public  updatelistiew writelog;
        public  dbTaskHelper db_plc_helper=new dbTaskHelper();
        public  float TB_weight_speed = 0;
        public  List<Single> weight_sp_list = new List<Single>(4);
    
        public  Int16 speed = 0;
        public  bool connect;
        public  bool LGB_A_carIn,LGB_A_getpow;
        public  bool LGB_B_carIn, LGB_B_getpow;
        public  Single Mes_GB_weight = 0;
        static string PLCIP = ConfigurationManager.ConnectionStrings["PLCIP"].ConnectionString;
        public static Plc plc300;
        //mes数据暂存
        public  DateTime in_time=new DateTime();
        public  string pathway="A",GB_initwit="520",GB_num="34";

        public string plczt_speedflag="";
        public string plczt_sped_setok = "";
        public int plczt_statflag = 0;
        //折铁plc数据对象
        public  plcdata ZT_data = new plcdata();
        public static Thread plc_updata ;
        public  void initplc()
        {
            try
            {
                LogManager.LogFielPrefix = "TB_weight_speed";
                LogManager.LogPath = @"..\loginfo\";//@"D:\fgg";
                
                plc300 = new Plc(CpuType.S7300, PLCIP, 0, 2);
               // plc300 = new Plc(CpuType.S71200, PLCIP, 0, 1);
                plc300.Open();//创建PLC实例
                plc_updata = new Thread(Read_PLC_data);
                plc_updata.Start();
                writelog("plc", "plc初始化完成！", "log");
            }
            catch(Exception e)
            {
                writelog("plc", "plc 初始化错误！", "err");
                LogHelper.WriteLog("plc初始化出错！", e);
            }

        }
        public  void still_read()
        {
            while(true)
            {
                Thread.Sleep(1000);
                Read_PLC_data();
            }
        }
        //读取plc数据并更新mes表格数据
        public  float lastvale=0;
        public  void write_startsignal()
        {
            plc300.Write("DB50.DBX3.5", true);
        }
        public async void Read_PLC_data()
        {
            while (true)
            {
                try
                {
                    //Stopwatch sw = new Stopwatch();
                    System.Threading.Thread.Sleep(200);
                    connect = (bool)plc300.IsConnected;
                    if (true)
                    {
                        // sw.Start();
                        // Task<plcdata> t = new Task({ }); plc300.ReadStructAsync(typeof(plcdata), 50);
                        // ZT_data = (plcdata)await Task<plcdata>.Run(() => { return plc300.ReadStructAsync(typeof(plcdata), 50);});
                        ZT_data = (plcdata)await plc300.ReadStructAsync(typeof(plcdata), 50);
                      
                        //sw.Stop();
                       // writelog("plc", sw.ElapsedMilliseconds.ToString(), "err");
                      
                        calc_weight_speed();
                        if (TB_weight_speed > 0 && ZT_data.TB_pos == false)
                        {
                            string log = TB_weight_speed.ToString();
                            LogManager.WriteLog(LogFile.Trace, log);
                        }

                        //LogHelper.WriteLog(log);
                        //  updata_mes_data();
                        // updata_RealTime_Car_Bag_data();
                        LGB_A_carIn = ZT_data.GB_A_carIn;
                        LGB_B_carIn = ZT_data.GB_B_carIn;
                        LGB_A_getpow = ZT_data.GB_A_connect;
                        LGB_B_getpow = ZT_data.GB_B_connect;
                    }
                    else
                    {
                        writelog("plc", "plc数据读取失败！", "log");
                        //System.Threading.Thread.Sleep(5000);
                       // plc300.Open();
                    }

                }
                catch (Exception e)
                {
                    writelog("plc", "plc数据读取错误！", "err");
                    LogHelper.WriteLog("PLC读取数据", e);
                }
            }
        }
        //更新mes记录表内容
        public  void updata_mes_data()
        {
            try
            {
                if (LGB_A_carIn == false && ZT_data.GB_A_carIn == true)
                {
                    in_time = DateTime.Now;
                    //当火车一到来时，将向mes索要火车数据，并将数据更新到MES_Data表中，当插电机器人插电时，将从MES_Data表中拷贝数据到RealTime_Car_Bag表中
                    string sqltext = string.Format("insert into [MES_Data] VALUES ('{0}','{1}','{2}','{3}','N')", pathway, in_time, GB_initwit, GB_num);
                    db_plc_helper.MultithreadExecuteNonQuery(sqltext);
                    writelog("MES_Data表更新", "A道有新罐车到来", "log");
                }
                if (LGB_B_carIn == false && ZT_data.GB_B_carIn == true)
                {
                    string sqltext = string.Format("insert into [MES_Data] VALUES ('{0}','{1}','{2}','{3}','N')", pathway, in_time, GB_initwit, GB_num);
                    db_plc_helper.MultithreadExecuteNonQuery(sqltext);
                    writelog("MES_Data表更新", "B道有新罐车到来", "log");
                }

            }
            catch (Exception e)
            {
                writelog("MES_Data表更新", "MES_Data表更新出错", "err");
                LogHelper.WriteLog("MES_Data表更新出错", e);
            }
            
        }
        ////更新罐包管理系统实时表内容
        public  void updata_RealTime_Car_Bag_data()
        {
            try
            {
                Int64 ID = 0;
                DateTime in_time = new DateTime();
                string GB_initweight = "", GB_num = "";
                if (LGB_A_getpow == false && ZT_data.GB_A_connect == true)
                {
                    string sqltext = string.Format("select * from MES_Data where data_flag='N' and number='{0}' and pathway='A'", ZT_data.GB_A_num);
                    DbDataReader dr = db_plc_helper.MultithreadDataReader(sqltext);
                    if (dr.HasRows)
                    {
                        int number = 0;
                        while (dr.Read())
                        {
                            number++;
                            if (number == 1)
                            {
                                ID = Convert.ToInt64((dr["ID"]));
                                in_time = Convert.ToDateTime((dr["in_time"]));
                                GB_initweight = (dr["init_weight"]).ToString().Trim();
                                GB_num = (dr["number"]).ToString().Trim();
                            }
                            else
                            {
                                Program.GB_data_flag = 1000;
                                writelog("RealTime_Car_Bag", "A道罐包数据存在冲突，已自动消除！", "log");
                                sqltext = string.Format("UPDATE MES_Data set data_flag='R' where data_flag='N' and pathway='A'");
                                db_plc_helper.MultithreadExecuteNonQuery(sqltext);

                                return;
                            }
                        }

                        //当火车一到来时，将向mes索要火车数据，并将数据更新到MES_Data表中，当插电机器人插电时，将从MES_Data表中拷贝数据到RealTime_Car_Bag表中
                        sqltext = string.Format("UPDATE RealTime_Car_Bag SET in_time='{0}',init_weight= '{1}',mid_weight='{2}',number='{3}' WHERE ID= 'A'", in_time, GB_initweight, GB_initweight, GB_num);
                        db_plc_helper.MultithreadExecuteNonQuery(sqltext);
                        writelog("RealTime_Car_Bag", "A道罐包管理数据更新", "log");
                        sqltext = string.Format("UPDATE MES_Data set data_flag='R' where ID={0}", ID);
                        db_plc_helper.MultithreadExecuteNonQuery(sqltext);
                    }
                    else
                    {
                        Program.GB_data_flag = 1000;
                    }

                }
                if (LGB_B_getpow == false && ZT_data.GB_B_connect == true)
                {
                    string sqltext = string.Format("select * from MES_Data where data_flag='N' and number='{0}' and pathway='B'", ZT_data.GB_B_num);
                    DbDataReader dr = db_plc_helper.MultithreadDataReader(sqltext);
                    if (dr.HasRows)
                    {
                        int number = 0;
                        while (dr.Read())
                        {
                            number++;
                            if (number == 1)
                            {
                                if (Convert.ToString(dr["ID"]).Trim() == "A")
                                {
                                    ID = Convert.ToInt64((dr["ID"]));
                                    in_time = Convert.ToDateTime((dr["in_time"]));
                                    GB_initweight = (dr["init_weight"]).ToString().Trim();
                                    GB_num = (dr["number"]).ToString().Trim();
                                }
                            }
                            else
                            {
                                Program.GB_data_flag = 1000;
                                writelog("RealTime_Car_Bag", "B道罐包数据存在冲突，已自动消除！", "log");
                                sqltext = string.Format("UPDATE MES_Data set data_flag='R' where data_flag='N' and pathway='B'");
                                db_plc_helper.MultithreadExecuteNonQuery(sqltext);
                                break;
                            }
                        }

                        //当火车一到来时，将向mes索要火车数据，并将数据更新到MES_Data表中，当插电机器人插电时，将从MES_Data表中拷贝数据到RealTime_Car_Bag表中
                        sqltext = string.Format("UPDATE RealTime_Car_Bag SET in_time='{0}',init_weight= '{1}',mid_weight='{2}',number='{3}' WHERE ID= 'A'", in_time, GB_initweight, GB_initweight, GB_num);
                        db_plc_helper.MultithreadExecuteNonQuery(sqltext);
                        sqltext = string.Format("UPDATE MES_Data set data_flag='R' where ID={0}",ID);
                        db_plc_helper.MultithreadExecuteNonQuery(sqltext);
                        writelog("RealTime_Car_Bag", "B道罐包管理数据更新", "log");
                    }
                    else
                    {
                        Program.GB_data_flag = 1000;
                    }

                }
            }
            catch(Exception e)
            {
                writelog("RealTime_Car_Bag", "更新新罐数据错误", "err");
                LogHelper.WriteLog("RealTime_Car_Bag更新新罐", e);
            }
            
        }
        public async Task<short> set_speed(string flag, Single speed)
        {
            try
            {
                if (flag == "A")
                {
                    await plc300.WriteAsync("DB50.DBD26", speed);
                    return (Int16)0;
                }
                else
                {
                    await plc300.WriteAsync("DB50.DBD30", speed);
                    return (Int16)0;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog("PLC写入速度", e);
                return 1000;
            }

        }
        //持续计算铁包重量
        public  Single calc_weight_speed()
        {
            try
            {
                Single spd_sum = 0;
                if(weight_sp_list.Count>=5)
                {
                    weight_sp_list.RemoveAt(0);
                    weight_sp_list.Add(ZT_data.TB_weight);
                    for(int i=0;i<4;i++)
                    {
                        spd_sum += weight_sp_list[i + 1] - weight_sp_list[i];
                    }
                    TB_weight_speed = spd_sum / (Single)4;
                }
                else
                {
                    weight_sp_list.Add(ZT_data.TB_weight);
                }
                return TB_weight_speed;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
    }
}
  

