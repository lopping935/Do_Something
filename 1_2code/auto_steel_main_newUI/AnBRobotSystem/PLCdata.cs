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
        public bool ZT_mission_run;
        public bool ZT_PLC_hum;
        public float TB_hight;//铁水液位
        public float TB_weight;//铁水包重量
        public Int16 TB_num;//铁水包号
        public Int16 GB_A_num;//罐车包号
        public Int16 GB_B_num;//罐车包号
        public Single GB_A_angle;//a实时倾角
        public Single GB_B_angle;//
        public Single GB_A_speed;//A设定速度 db50.dbd26
        public Single GB_B_speed;//B设定速度db50.dbd30
        public Single GB_A_Real_weight;//A罐真实速度
        public Single GB_B_Real_weight;//B罐真实速度
        public Single TB_LIQID;//铁包液位
        public Int16 ZT_station_flag;//折铁位置
        public bool ZT_flag;//折铁位置
        public Single GB_A_full_weight;//罐车a总重
        public Single GB_B_full_weight;//罐车b总重
        public bool GBA_high_speed_fwd;
        public bool GBA_low_speed_fwd;
        public bool GBA_high_speed_back;
        public bool GBA_low_speed_back;
        public bool GBB_high_speed_fwd;
        public bool GBB_low_speed_fwd;
        public bool GBB_high_speed_back;
        public bool GBB_low_speed_back;

    }
    
    public class PLCdata
    {
        public  LogManager my_log = new LogManager();
        public  updatelistiew writelog;
        public  dbTaskHelper db_plc_helper=new dbTaskHelper();
        public  double TB_weight_speed = 0;
        public  List<double> weight_sp_list = new List<double>(4);
    
        public  Int16 speed = 0;
        public  bool connect;
        public  bool LGB_A_carIn,LGB_A_getpow;
        public  bool LGB_B_carIn, LGB_B_getpow;
        public  Single Mes_GB_weight = 0;
        static string PLCIP = ConfigurationManager.ConnectionStrings["PLCIP"].ConnectionString;
        public static Plc plc300;
        public double GB_A_remind_weight = 0, GB_B_remind_weight = 0, GB_A_init_weight = 0, GB_B_init_weight = 0;
        public float TB_init_weight = 0;
        public bool last_TB_on_pos = true;
        public string last_tl_station = "";
        //mes数据暂存
        public DateTime in_time=new DateTime();
        public  string pathway="A",GB_initwit="520",GB_num="34";

        public string plczt_speedflag="";
        public string plczt_sped_setok = "";
        public int plczt_statflag = 0;
        //折铁plc数据对象
        public  plcdata ZT_data = new plcdata();
        public static Thread plc_updata ;

        //鱼雷罐重量更新
        public static Thread GB_updata;
        public  void initplc()
        {
            try
            {
                my_log.LogFielPrefix = "TB_weight_speed";
                my_log.LogPath = @"..\loginfo\";//@"D:\fgg";
                
                plc300 = new Plc(CpuType.S7300, PLCIP, 0, 2);
               // plc300 = new Plc(CpuType.S71200, PLCIP, 0, 1);
                plc300.Open();//创建PLC实例
                
                plc_updata = new Thread(Read_PLC_data);
                plc_updata.IsBackground = true;
                plc_updata.Start();
                GB_updata = new Thread(GB_weight_calc);
                GB_updata.IsBackground = true;
                GB_updata.Start();
                writelog("plc", "plc初始化完成！", "log");
            }
            catch(Exception e)
            {
                writelog("plc", "plc 初始化错误！", "err");
                LogHelper.WriteLog("plc初始化出错！", e);
            }

        }
        //实时计算鱼雷罐剩余总重
        public void GB_weight_calc()
        {
            while (true)
            {

                Thread.Sleep(1000);
                if (MdiParent.tl1.TL_station == "A" && MdiParent.tl1.Get_iron == true && GB_A_remind_weight > 300)
                {
                    GB_A_remind_weight -= TB_weight_speed;
                    db_plc_helper.updata_table("RealTime_Car_Bag", "mid_weight", Math.Round(GB_A_remind_weight, 2).ToString(), "ID", "A");
                }
                else if (MdiParent.tl1.TL_station == "B" && MdiParent.tl1.Get_iron == true && GB_B_remind_weight > 300)
                {
                    GB_B_remind_weight -= TB_weight_speed;
                    db_plc_helper.updata_table("RealTime_Car_Bag", "mid_weight", Math.Round(GB_B_remind_weight, 2).ToString(), "ID", "B");

                }
                //上升沿记录铁包初始重量
                if (last_TB_on_pos == true && ZT_data.TB_pos == false)
                {
                    TB_init_weight = ZT_data.TB_weight;
                }

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
        public void get_gba_num()
        {
            Task.Run(() =>
            {
                plc300.WriteAsync("DB50.DBX59.0", true);
            });
        }
        public int hum_speed_calc(string tl_station)
        {
            int speed_calc = 0;
            if (tl_station == "A")
            {
                if (ZT_data.GBA_high_speed_fwd)
                {
                    return speed_calc = 20;
                }
                else if (ZT_data.GBA_low_speed_fwd)
                {
                    return speed_calc = 10;
                }
                else if (ZT_data.GBA_high_speed_back)
                {
                    return speed_calc = -20;
                }
                else if (ZT_data.GBA_low_speed_back)
                {
                    return speed_calc = -10;
                }
                else
                {
                    return speed_calc = 0;
                }

            }
            else if (tl_station == "B")
            {
                if (ZT_data.GBB_high_speed_fwd)
                {
                    return speed_calc = 20;
                }
                else if (ZT_data.GBB_low_speed_fwd)
                {
                    return speed_calc = 10;
                }
                else if (ZT_data.GBB_high_speed_back)
                {
                    return speed_calc = -20;
                }
                else if (ZT_data.GBB_low_speed_back)
                {
                    return speed_calc = -10;
                }
                else
                {
                    return speed_calc = 0;
                }
            }
            else
            {
                return speed_calc;
            }

        }
        //读取plc数据并更新mes表格数据
        public  float lastvale=0;
        public void write_startsignal()
        {
            plc300.WriteAsync("DB50.DBX3.5", true);
        }
        //发送任务存活心跳
        public void write_mission_signal(bool mission_statu)
        {
            plc300.WriteAsync("DB50.DBX3.6", mission_statu);
        }
        public void reset_hum_signal()
        {
            plc300.WriteAsync("DB50.DBX3.7", false);
        }
        
        public async void Read_PLC_data()
        {
            while (true)
            {
                try
                {
                    //Stopwatch sw = new Stopwatch();
                    System.Threading.Thread.Sleep(400);
                    connect = (bool)plc300.IsConnected;
                    if (connect)
                    {
                        plc300.WriteAsync("DB50.DBW0.0", (Int16)1);
                        ZT_data = (plcdata)await plc300.ReadStructAsync(typeof(plcdata), 50);
                        calc_weight_speed();
                        updata_RealTime_Car_Bag_plc();
                        if ((MdiParent.tl1.Get_iron||TB_weight_speed > 0.09) )//&& ZT_data.TB_pos == false
                        {
                            int hum_speed = hum_speed_calc(MdiParent.tl1.TL_station);
                            string head = "时间,折铁增重速度,铁包总重,铁包液位,罐车倾角,折铁位置,铁流有无判断,铁包位置,罐剩余重量,人工速度,自动速度";
                            string log = TB_weight_speed.ToString() + "," + ZT_data.TB_weight.ToString() + "," + ZT_data.TB_LIQID.ToString() + ",";
                            if (MdiParent.tl1.TL_station == "A")
                                log += Math.Round((ZT_data.GB_A_angle / 20), 2).ToString() + ",2号" + "," + MdiParent.tl1.Get_iron.ToString() + "," + ZT_data.TB_pos.ToString() + "," + GB_A_remind_weight + "," + hum_speed + "," + ZT_data.GB_A_speed;
                            else if (MdiParent.tl1.TL_station == "B")
                                log += Math.Round((ZT_data.GB_B_angle / 20), 2).ToString() + ",1号" + "," + MdiParent.tl1.Get_iron.ToString() + "," + ZT_data.TB_pos.ToString() + "," + GB_B_remind_weight + "," + hum_speed + "," + ZT_data.GB_B_speed;
                            else
                                log += MdiParent.tl1.Get_iron.ToString() + "," + ZT_data.TB_pos.ToString() + "," + ZT_data.TB_pos.ToString() + "," + GB_B_remind_weight;
                            my_log.WriteLogcsv(LogFile.Trace, log, head);
                        }
                        LGB_A_carIn = ZT_data.GB_A_carIn;
                        LGB_B_carIn = ZT_data.GB_B_carIn;
                        LGB_A_getpow = ZT_data.GB_A_connect;
                        LGB_B_getpow = ZT_data.GB_B_connect;
                    }
                    else
                    {
                        writelog("plc", "plc数据读取失败！", "log");
                        Thread.Sleep(5000);
                        plc300.Open();
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

        ////更新plc中罐车总重  在plc中记录
        public async void updata_RealTime_Car_Bag_plc()
        {
            try
            {
                Int64 ID = 0;
                DateTime in_time = new DateTime();
                string GB_initweight = "", GB_num = "";
                if (LGB_A_getpow == false && ZT_data.GB_A_connect == true || MdiParent.updata_GBA_fulweight == true)//当得电信号上升沿时 写入数据
                {
                    MdiParent.updata_GBA_fulweight = false;
                    LGB_A_getpow = ZT_data.GB_A_connect;
                    string sqltext = string.Format("select * from MES_Data where data_flag='N' and pathway='A'");//and number='{0}'
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
                                //in_time = Convert.ToDateTime((dr["in_time"]));
                                GB_initweight = (dr["init_weight"]).ToString().Trim();

                            }
                            else
                            {
                                break;
                            }
                        }
                        dr.Close();
                        if (number == 1)
                        {
                            GB_A_init_weight = double.Parse(GB_initweight);
                            GB_A_remind_weight = GB_A_init_weight;
                            sqltext = string.Format("UPDATE RealTime_Car_Bag set init_weight='{0}',mid_weight='{1}',in_time='{2}' where ID='A'", GB_A_init_weight.ToString(), GB_A_remind_weight.ToString(),DateTime.Now.ToString());
                            db_plc_helper.MultithreadExecuteNonQuery(sqltext);
                            writelog("RealTime_Car_Bag", "2号罐包总重写入plc！", "log");
                        }
                        else
                        {
                            Program.GB_data_flag = 1005;
                            writelog("RealTime_Car_Bag", "2号罐包数据存在冲突，已自动消除！", "log");
                            sqltext = string.Format("UPDATE MES_Data set data_flag='R' where data_flag='N' and pathway='A'");
                            db_plc_helper.MultithreadExecuteNonQuery(sqltext);
                            return;
                        }
                        sqltext = string.Format("UPDATE MES_Data set data_flag='R' where ID={0}", ID);
                        db_plc_helper.MultithreadExecuteNonQuery(sqltext);

                    }
                    else
                    {
                        sqltext = string.Format("UPDATE RealTime_Car_Bag set  init_weight='{0}',mid_weight='{1}',in_time='{2}' where ID='A'", 0.ToString(), 0.ToString(), DateTime.Now.ToString());
                        db_plc_helper.MultithreadExecuteNonQuery(sqltext);
                        Program.GB_data_flag = 1005;
                        writelog("RealTime_Car_Bag", "2号未查询到总重！", "log");
                    }

                }
                if (LGB_B_getpow == false && ZT_data.GB_B_connect == true || MdiParent.updata_GBB_fulweight == true)
                {
                    MdiParent.updata_GBB_fulweight =false;
                    LGB_B_getpow = ZT_data.GB_B_connect;
                    string sqltext = string.Format("select * from MES_Data where data_flag='N' and pathway='B'");
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
                                GB_initweight = (dr["init_weight"]).ToString().Trim();
                                //await plc300.WriteAsync("DB50.DBD54", Single.Parse(GB_initweight));//写入字符串

                            }
                            else
                            {
                                break;
                            }
                        }
                        dr.Close();
                        if (number == 1)
                        {
                            GB_B_init_weight = double.Parse(GB_initweight);
                            GB_B_remind_weight = GB_B_init_weight;
                            sqltext = string.Format("UPDATE RealTime_Car_Bag set init_weight='{0}',mid_weight='{1}',in_time='{2}' where ID='B'", GB_B_init_weight.ToString(), GB_B_remind_weight.ToString(),DateTime.Now.ToString());
                            db_plc_helper.MultithreadExecuteNonQuery(sqltext);
                            writelog("RealTime_Car_Bag", "获取1号罐包总重！", "log");
                        }
                        else
                        {
                            Program.GB_data_flag = 1005;
                            writelog("RealTime_Car_Bag", "1号罐包数据存在冲突，已自动消除！", "log");
                            sqltext = string.Format("UPDATE MES_Data set data_flag='R' where data_flag='N' and pathway='B'");
                            db_plc_helper.MultithreadExecuteNonQuery(sqltext);
                        }
                        sqltext = string.Format("UPDATE MES_Data set data_flag='R' where ID={0}", ID);
                        db_plc_helper.MultithreadExecuteNonQuery(sqltext);
                    }
                    else
                    {
                        sqltext = string.Format("UPDATE RealTime_Car_Bag set  init_weight='{0}',mid_weight='{1}',in_time='{2}' where ID='B'", 0.ToString(), 0.ToString(), DateTime.Now.ToString());
                        db_plc_helper.MultithreadExecuteNonQuery(sqltext);
                        Program.GB_data_flag = 1005;
                        writelog("RealTime_Car_Bag", "1号未查询到总重！", "log");
                    }

                }
            }
            catch (Exception e)
            {
                writelog("RealTime_Car_Bag", "更新新罐数据错误", "err");
                LogHelper.WriteLog("RealTime_Car_Bag更新新罐", e);
            }

        }
        ////更新罐包管理系统实时表内容  在折铁模型中记录
        public  void updata_RealTime_Car_Bag_data()
        {
            try
            {
                Int64 ID = 0;
                DateTime in_time = new DateTime();
                string GB_initweight = "", GB_num = "";
                if (LGB_A_getpow == false && ZT_data.GB_A_connect == true)//当得电信号上升沿时 写入数据
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
                    in_time = DateTime.Now;
                    string sqltext = string.Format("insert into [zt_controldata] VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", in_time, speed.ToString(),TB_weight_speed.ToString(),ZT_data.TB_weight.ToString(), "A",ZT_data.GB_A_angle.ToString(), Program.program_flag.ToString());
                    db_plc_helper.MultithreadExecuteNonQuery(sqltext);
                    return (Int16)0;
                }
                else
                {
                    await plc300.WriteAsync("DB50.DBD30", speed);
                    in_time = DateTime.Now;
                    string sqltext = string.Format("insert into [zt_controldata] VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", in_time, speed.ToString(), TB_weight_speed.ToString(), ZT_data.TB_weight.ToString(), "B", ZT_data.GB_B_angle.ToString(), Program.program_flag.ToString());
                    db_plc_helper.MultithreadExecuteNonQuery(sqltext);
                    return (Int16)0;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog("PLC写入速度", e);
                return 1000;
            }

        }
        public void set_speed(string flag)
        {
            try
            {
                if (flag == "A")
                {
                    in_time = DateTime.Now;
                    string sqltext = string.Format("insert into [zt_controldata] VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", in_time, "", TB_weight_speed.ToString(), ZT_data.TB_weight.ToString(), "A", ZT_data.GB_A_angle.ToString(), Program.program_flag.ToString());
                    db_plc_helper.MultithreadExecuteNonQuery(sqltext);

                }
                else
                {
                    in_time = DateTime.Now;
                    string sqltext = string.Format("insert into [zt_controldata] VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", in_time, "", TB_weight_speed.ToString(), ZT_data.TB_weight.ToString(), "B", ZT_data.GB_B_angle.ToString(), Program.program_flag.ToString());
                    db_plc_helper.MultithreadExecuteNonQuery(sqltext);
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog("PLC写入速度", e);
            }

        }
        //持续计算铁包重量
        public  double calc_weight_speed()
        {
            try
            {
                if(ZT_data.TB_pos==false)
                {
                    double spd_sum = 0;
                    if (weight_sp_list.Count >= 5)
                    {
                        weight_sp_list.RemoveAt(0);
                        weight_sp_list.Add(ZT_data.TB_weight);
                        for (int i = 0; i < 4; i++)
                        {
                            spd_sum += weight_sp_list[i + 1] - weight_sp_list[i];
                        }
                        
                        TB_weight_speed =Math.Round((spd_sum / (Single)4) * (Single)2.5,4) ;
                        return TB_weight_speed;
                    }
                    else
                    {
                        weight_sp_list.Add(ZT_data.TB_weight);
                        return 0;
                    }
                    
                }
                else
                {
                    weight_sp_list.Clear();
                    return TB_weight_speed=0;
                }
                
            }
            catch (Exception e)
            {
                return 0;
            }
        }
    }
}
  

