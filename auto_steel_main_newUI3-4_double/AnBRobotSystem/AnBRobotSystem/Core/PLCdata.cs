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
using System.Windows.Forms;

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
        public bool ZT_PLC_hum_A;
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

        //public bool GHA_VISION_CODE;
        //public bool GHB_VISION_CODE;

        public bool ZT_PLC_hum_B;
        public bool BACK2;
        public bool BACK3;
        public bool MODEL_RESET;
        public bool GBA_TL;
        public bool GBB_TL;
        public Single TB_WEIGHT_SPEED;
        public Int16 end;
        //public static explicit operator plcdata(Task<object> v)
        //{
        //    throw new NotImplementedException();
        //}
    }
    
    public class PLCdata
    {
        public  LogManager my_log = new LogManager();
        public  updatelistiew writelog;
        public  dbTaskHelper db_plc_helper=new dbTaskHelper();
        public  double TB_weight_speed = 0,TB_accelerate=0;
        public  List<double> weight_sp_list = new List<double>(4);
        public List<double> weight_sp__speed = new List<double>(4);


        public  bool connect;
        public  bool LGB_A_carIn,LGB_A_getpow;
        public  bool LGB_B_carIn, LGB_B_getpow;
        public  Single Mes_GB_weight = 0;
        static string PLCIP = ConfigurationManager.ConnectionStrings["PLCIP"].ConnectionString;
        public static Plc plc300;
        public double GB_A_remind_weight=0, GB_B_remind_weight = 0, GB_A_init_weight=0, GB_B_init_weight=0;
        public float TB_init_weight = 0;
        public bool last_TB_on_pos = true;
        public string last_tl_station = "";
        
        //mes数据暂存
        public  DateTime in_time=new DateTime();
        public  string pathway="A",GB_initwit="520",GB_num="34";
        
        public string plczt_speedflag="";
        public string plczt_sped_setok = "";
        public int plczt_statflag = 0;
        //测试数据


        Stopwatch sw = new Stopwatch();
        public double swmilltime;
        public double runtime = 0;
        Stopwatch sw1 = new Stopwatch();
        public double swmilltime1;
        public double runtime1 = 0,taskw=0;
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
                GB_updata.Start();
                GB_updata.IsBackground = true;
                writelog("plc", "plc初始化完成！", "log");
            }
            catch(Exception e)
            {
                writelog("plc", "plc 初始化错误！", "err");
                LogHelper.WriteLog("plc初始化出错！", e);
            }

        }
        #region 主要工作线程

        //实时将鱼雷罐剩余总重写入sql表中  
        public void GB_weight_calc()
        {
            Int16 heart_count = 0;//心跳计数

            while (true)
            {
                heart_count += 1;
                if (heart_count > 32000)
                    heart_count = 0;
                Thread.Sleep(1000);
               // sw1.Restart();
               // runtime += 1;
                //plc300.WriteAsync("DB50.DBW0.0", (Int16)1);


                //写入数据到plc
                if (MdiParent.tl1.TL_station == "A")
                {
                    plc300.WriteAsync("DB50.DBX59.4", MdiParent.tl1.Get_iron);
                }
                else if (MdiParent.tl1.TL_station == "B")
                {
                    plc300.WriteAsync("DB50.DBX59.5", MdiParent.tl1.Get_iron);
                }
                else
                {
                    plc300.WriteAsync("DB50.DBX59.4", false);
                    plc300.WriteAsync("DB50.DBX59.5", false);
                }

                plc300.WriteAsync("DB50.DBD60", (Single)TB_weight_speed);
                //写入心跳数据
                plc300.WriteAsync("DB50.DBW0", heart_count);

                updata_RealTime_Car_Bag_plc();
                LGB_A_carIn = ZT_data.GB_A_carIn;
                LGB_B_carIn = ZT_data.GB_B_carIn;
                LGB_A_getpow = ZT_data.GB_A_connect;
                LGB_B_getpow = ZT_data.GB_B_connect;
                
                //上升沿记录铁包初始重量
                if (last_TB_on_pos == true && ZT_data.TB_pos == false)
                {
                    TB_init_weight = ZT_data.TB_weight;
                }
              //  sw.Stop();
              //  swmilltime = sw.Elapsed.Milliseconds;
            }
        }
        //plc数据读取主线程
        public async void Read_PLC_data()
        {
            while (true)
            {
                try
                {
                    //System.Threading.Thread.Sleep(400);
                    DateTime bf = DateTime.Now;
                    //sw1.Reset();
                    sw1.Restart();
                    taskw += 1;
                    connect = (bool)plc300.IsConnected;
                    if (connect)
                    {
                        Task.Run(() =>
                        {
                            ZT_data = (plcdata)plc300.ReadStruct(typeof(plcdata), 50);
                            runtime1 += 1;
                        });
                        //ZT_data =(plcdata)await plc300.ReadStructAsync(typeof(plcdata), 50);
                        Delay(800);
                        //System.Threading.Thread.Sleep(800);
                        //折铁实时速度计算和罐车总重计算
                        calc_TB_weight_speed();
                        ztlog();
                    }
                    else
                    {
                        writelog("plc", "plc数据读取失败！", "log");
                        Thread.Sleep(5000);
                        plc300.Open();
                    }
                    sw1.Stop();

                    DateTime af = DateTime.Now;
                    swmilltime1 = af.Subtract(bf).TotalMilliseconds;

                }
                catch (Exception e)
                {
                    writelog("plc", "plc数据读取错误！", "err");
                    LogHelper.WriteLog("PLC读取数据", e);
                }
            }
        }

        //读取plc数据并更新mes表格数据
        #endregion

        #region 发送数据给plc
        //设定速度
        public async void  set_speed(string flag, Single speed)
        {
            try
            {
                if (flag == "A")
                {
                    await plc300.WriteAsync("DB50.DBD26", speed);
                    in_time = DateTime.Now;
                    string sqltext = string.Format("insert into [zt_controldata] VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", in_time, speed.ToString(), TB_weight_speed.ToString(), ZT_data.TB_weight.ToString(), "A", ZT_data.GB_A_angle.ToString(), Program.program_flag.ToString());
                    db_plc_helper.MultithreadExecuteNonQuery(sqltext);
                 
                }
                else
                {
                    await plc300.WriteAsync("DB50.DBD30", speed);
                    in_time = DateTime.Now;
                    string sqltext = string.Format("insert into [zt_controldata] VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", in_time, speed.ToString(), TB_weight_speed.ToString(), ZT_data.TB_weight.ToString(), "B", ZT_data.GB_B_angle.ToString(), Program.program_flag.ToString());
                    db_plc_helper.MultithreadExecuteNonQuery(sqltext);
               
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog("PLC写入速度", e);
            
            }

        }
        //设定速度记录
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
        //罐号读取
        public async void get_gb_num()
        {

            await plc300.WriteAsync("DB50.DBX59.0", true);

        }
        //发送清除开始命令
        public async void reset_start()
        {

            await plc300.WriteAsync("DB50.DBX59.3", true);
            //await plc300.WriteAsync("DB50.DBX59.4", true);
        }
        public async void write_startsignal()
        {
            await plc300.WriteAsync("DB50.DBX3.5", true);
            
        }
        //发送任务存活心跳
        public void write_mission_signal(bool mission_statu)
        {
            plc300.WriteAsync("DB50.DBX3.6", mission_statu);
        }
        public async void reset_hum_signal()
        {
             await plc300.WriteAsync("DB50.DBX3.7", false);
        }
        #endregion

        #region 功能函数
        public void TO_plc_aim_weight(Int16 aim_weight)
        {
            plc300.WriteAsync("DB50.DBW60", aim_weight);
        }
        public static void Delay(int mm)
        {
            DateTime current = DateTime.Now;
            while (current.AddMilliseconds(mm) > DateTime.Now)
            {
                Application.DoEvents();
            }
            return;
        }
        public int hum_speed_calc(string tl_station)
        {
            int speed_calc = 0;
            if(tl_station=="A")
            {
                if(ZT_data.GBA_high_speed_fwd)
                {
                    return speed_calc = 20;
                }
                else if(ZT_data.GBA_low_speed_fwd)
                {
                    return speed_calc = 10;
                }
                else if(ZT_data.GBA_high_speed_back)
                {
                    return speed_calc = -20;
                }
                else if(ZT_data.GBA_low_speed_back)
                {
                    return speed_calc = -10;
                }
                else
                {
                    return speed_calc = 0;
                }
                
            }
            else if(tl_station=="B")
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

        ////更新罐车总重  在plc中记录
        public void updata_RealTime_Car_Bag_plc()
        {
            try
            {
                Int64 ID = 0;
                DateTime in_time = new DateTime();
                string GB_initweight = "", GB_num = "";
                //A罐总重更新
                if (LGB_A_getpow == false && ZT_data.GB_A_connect == true|| MdiParent.updata_GBA_fulweight == true)//当得电信号上升沿时 写入数据
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
                            //增加写入时间--
                            GB_A_init_weight = double.Parse(GB_initweight);
                            GB_A_remind_weight = GB_A_init_weight;
                            
                            sqltext = string.Format("UPDATE RealTime_Car_Bag set  init_weight='{0}',mid_weight='{1}',in_time='{2}' where ID='A'", GB_A_init_weight.ToString(), GB_A_remind_weight.ToString(),DateTime.Now.ToString());
                            db_plc_helper.MultithreadExecuteNonQuery(sqltext);
                            writelog("RealTime_Car_Bag", "4号罐包总重写入plc！", "log");
                        }
                        else
                        {
                            Program.GB_data_flag = 1005;
                            writelog("RealTime_Car_Bag", "A道罐包数据存在冲突，已自动消除！", "log");
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
                        writelog("RealTime_Car_Bag", "4号未查询到总重！", "log");
                    }

                }
                else if(LGB_A_getpow == true && ZT_data.GB_A_connect == false )//清空鱼雷罐车总重
                {
                    GB_A_init_weight = 0;
                    GB_A_remind_weight = 0;
                    string sqltext = string.Format("UPDATE RealTime_Car_Bag set  init_weight='{0}',mid_weight='{1}',in_time='{2}' where ID='A'", 0.ToString(), 0.ToString(), DateTime.Now.ToString());
                    db_plc_helper.MultithreadExecuteNonQuery(sqltext);
                    Program.GB_data_flag = 1005;
                    writelog("RealTime_Car_Bag", "4号未查询到总重！", "log");
                }
                else
                {
                    if (MdiParent.tl1.TL_station == "A" && MdiParent.tl1.Get_iron == true && GB_A_remind_weight > 300)
                    {
                        db_plc_helper.updata_table("RealTime_Car_Bag", "mid_weight", Math.Round(GB_A_remind_weight, 2).ToString(), "ID", "A");
                    }
                }
                //B罐总重更新
                if (LGB_B_getpow == false && ZT_data.GB_B_connect == true|| MdiParent.updata_GBB_fulweight == true)
                {
                    MdiParent.updata_GBB_fulweight = false;
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
                        if(number == 1)
                        {
                            GB_B_init_weight = double.Parse(GB_initweight);
                            GB_B_remind_weight = GB_B_init_weight;
                            sqltext = string.Format("UPDATE RealTime_Car_Bag set init_weight='{0}',mid_weight='{1}',in_time='{2}' where ID='B'", GB_B_init_weight.ToString(), GB_B_remind_weight.ToString(),DateTime.Now.ToString());
                            db_plc_helper.MultithreadExecuteNonQuery(sqltext);
                            writelog("RealTime_Car_Bag", "设置3号罐包总重！", "log");
                        }
                        else
                        {
                            Program.GB_data_flag = 1005;
                            writelog("RealTime_Car_Bag", "B道罐包数据存在冲突，已自动消除！", "log");
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
                        writelog("RealTime_Car_Bag", "3号未查询到总重！", "log");
                    }

                }
                else if(LGB_B_getpow == true && ZT_data.GB_B_connect == false)
                {
                    GB_A_init_weight = 0;
                    GB_A_remind_weight = 0;
                    string sqltext = string.Format("UPDATE RealTime_Car_Bag set  init_weight='{0}',mid_weight='{1}',in_time='{2}' where ID='B'", 0.ToString(), 0.ToString(), DateTime.Now.ToString());
                    db_plc_helper.MultithreadExecuteNonQuery(sqltext);
                    Program.GB_data_flag = 1005;
                    writelog("RealTime_Car_Bag", "3号总重清零！", "log");
                }
                else
                {
                    if (MdiParent.tl1.TL_station == "B" && MdiParent.tl1.Get_iron == true && GB_B_remind_weight > 300)
                    {
                        db_plc_helper.updata_table("RealTime_Car_Bag", "mid_weight", Math.Round(GB_B_remind_weight, 2).ToString(), "ID", "B");
                    }
                }
            }
            catch (Exception e)
            {
                writelog("RealTime_Car_Bag", "更新新罐数据错误", "err");
                LogHelper.WriteLog("RealTime_Car_Bag更新新罐", e);
            }

        }
 
        //持续计算铁包重量
        public double calc_TB_weight_speed()
        {
            try
            {
                //罐内铁水统计
                if (MdiParent.tl1.TL_station == "A" && MdiParent.tl1.Get_iron == true && GB_A_remind_weight > 300)
                {
                    GB_A_remind_weight -= Math.Round(TB_weight_speed / 2.5, 4);
                }
                else if (MdiParent.tl1.TL_station == "B" && MdiParent.tl1.Get_iron == true && GB_B_remind_weight > 300)
                {
                    GB_B_remind_weight -= Math.Round(TB_weight_speed / 2.5, 4);
                }
                //铁水包速度计算
                if (ZT_data.TB_pos==false)
                {
                    double spd_sum = 0;
                    double acc_sum = 0;
                    if (weight_sp_list.Count >= 5)
                    {
                        weight_sp_list.RemoveAt(0);
                        weight_sp_list.Add(ZT_data.TB_weight);
                        for (int i = 0; i < 4; i++)
                        {
                            spd_sum += weight_sp_list[i + 1] - weight_sp_list[i];
                        }
                        TB_weight_speed = 0;
                        TB_weight_speed =Math.Round((spd_sum / (Single)4) * (Single)2.5,4) ;
                        //速度的增速
                        if(weight_sp__speed.Count>=5)
                        {
                            weight_sp__speed.RemoveAt(0);
                            weight_sp__speed.Add(TB_weight_speed);
                            for (int i = 0; i < 4; i++)
                            {
                                acc_sum += weight_sp__speed[i + 1] - weight_sp__speed[i];
                            }
                            TB_accelerate = acc_sum;
                            //TB_accelerate = Math.Round((acc_sum / (Single)4) * (Single)2.5, 4);
                        }
                        else
                        {
                            TB_accelerate = 0;
                            weight_sp__speed.Add(TB_weight_speed);
                        }

                        return TB_weight_speed;
                    }
                    else
                    {
                        weight_sp_list.Add(ZT_data.TB_weight);
                        return TB_weight_speed=0;
                    }
                    
                }
                else
                {
                    weight_sp_list.Clear();
                    weight_sp__speed.Clear();
                    return TB_weight_speed=0;
                }
                
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        //折铁过程记录
        public void ztlog()
        {
            if ((MdiParent.tl1.Get_iron || TB_weight_speed > 0.09))//&& ZT_data.TB_pos == false  
            {
                int hum_speed = hum_speed_calc(MdiParent.tl1.TL_station);
                string head = "时间,折铁增重速度,铁包总重,铁包液位,罐车倾角,折铁位置,铁流有无判断,铁包位置,罐包剩余重量,人工速度,自动速度";
                string log = TB_weight_speed.ToString() + "," + ZT_data.TB_weight.ToString() + "," + ZT_data.TB_LIQID.ToString() + ",";
                if (MdiParent.tl1.TL_station == "A")
                    log += Math.Round((ZT_data.GB_A_angle / 20), 2).ToString() + ",4号" + "," + MdiParent.tl1.Get_iron.ToString() + "," + ZT_data.TB_pos.ToString() + "," + GB_A_remind_weight + "," + hum_speed + "," + ZT_data.GB_A_speed + "," + Program.program_flag.ToString() + "," + Program.testcode;
                else if (MdiParent.tl1.TL_station == "B")
                    log += Math.Round((ZT_data.GB_B_angle / 20), 2).ToString() + ",3号" + "," + MdiParent.tl1.Get_iron.ToString() + "," + ZT_data.TB_pos.ToString() + "," + GB_B_remind_weight + "," + hum_speed + "," + ZT_data.GB_B_speed + "," + Program.program_flag.ToString() + "," + Program.testcode;
                else
                    log += MdiParent.tl1.Get_iron.ToString() + "," + ZT_data.TB_pos.ToString() + "," + ZT_data.TB_pos.ToString() + "," + GB_B_remind_weight;
                my_log.WriteLogcsv(LogFile.Trace, log, head);
            }
        }
        #endregion
    }
}
  

