using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnBRobotSystem;
using AnBRobotSystem.Utlis;
using logtest;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Data.Common;
using System.Speech.Synthesis;

namespace AnBRobotSystem.Core
{
    public class ZT_Date
    {
        public bool TB_on_pos;//铁包到位
        public float TB_hight;//铁水液位
        public float TB_need_weight;//铁水包重量
        public Int16 TB_num;//铁水包号
        public bool TB_BK_vision;
        public float TB_weight = 0;
        public float TB_Init_weight = 0;

        public bool GB_GK_vision;//罐包到位，可以用插电来获取
        public bool GB_on_pos;//罐包到位，可以用插电来获取
        public string GB_station;//罐包位置
        public bool GB_connect;//罐车得电
        public bool GB_0_limt;//罐车0限位
        public bool GB_120_limt;//罐车120限位
        public Int16 GB_num;//罐车包号
        public Single GB_angle;//罐包角度
        public Single GB_have_wight;//罐内有的铁量
        public Single GB_full_wight;//罐包角度
        public string GB_capacity;//罐车容量
        public DateTime GB_train_in_times;//罐车到来时间
        public bool Has_mission;//当前是否有折铁任务


        public float GB_speed;//罐实时速度
        public bool GB_edge;//罐口边缘
        public bool TL;//铁流检测
        public bool TL_in_TB;//铁流进入铁包
        public bool ZT_back;//折铁返回点确定
        public bool ZT_OK;//折铁完成

        public float ZT_reqweight;
       

    }

    public class Auto_model 
    {
        SpeechSynthesizer synthes = new SpeechSynthesizer();

        public static string banci = "";
        public static DateTime load = new DateTime();
        dbTaskHelper dbhelper = new dbTaskHelper();
        DbDataReader dr = null;
        double second = 0;
        int minute = 0;
        public dbTaskHelper dbhlper ;
        public manage_steel m1 =new manage_steel();
        public static float full_speed = 2;
        public updatelistiew writelisview;
        public Thread full_thread, nfull_thread;
        //线程信号量
        public static int A_live=0, B_live = 0;
        //自动折铁进程数据 
        public int ZT_program_flag = 0;
        public int ZT_thread_flag = 1000;
        public bool threadflag = false;
        public ZT_Date one_ZT;// = new ZT_Date();
        Single limt_angle = 1200;
        public int tl_good_count = 0, tl_good_count_limt = 60, tl_bad_count = 0, tl_bad_count_limt = 5, tl_count = 0;
        public bool tl_bad_flag = false;
        public List<float> edge_value; //= new List<float>(5);
        public Auto_model()
        {
            dbhlper = new dbTaskHelper();
            //m1.writelisview = MdiParent.form.mainlog;
            m1.dbhlper = this.dbhlper;
            string sqltext = string.Format("select * from para_adjust");
            DbDataReader dr = dbhlper.MultithreadDataReader(sqltext);
            if (dr.HasRows)
            {
                int number = 0;
                while (dr.Read())
                {
                    if ((dr["op_name"]).ToString().Trim() == "full_speed")
                    {
                        full_speed = float.Parse(dr["value"].ToString().Trim());
                    }
                    if ((dr["op_name"]).ToString().Trim() == "tl_good_count_limt")
                    {
                        tl_good_count_limt = int.Parse(dr["value"].ToString().Trim());
                    }
                    if ((dr["op_name"]).ToString().Trim() == "tl_bad_count_limt")
                    {
                        tl_bad_count_limt = int.Parse(dr["value"].ToString().Trim());
                    }
                }
                dr.Close();
            }
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
        public void init_ZT_data()
        {
            ZT_program_flag = 0;
            one_ZT = new ZT_Date();
            edge_value = new List<float>(5);
            full_thread = null;
            nfull_thread = null;
        }
        public void chose_process()
        {
            try
            {
                //给plc写入自动本次折铁开始信号
                one_ZT.GB_station = Program.GB_station;
                //写入目标数据
                MdiParent.PLCdata1.TO_plc_aim_weight((Int16)one_ZT.ZT_reqweight);
                MdiParent.PLCdata1.set_speed("A", 0);
                MdiParent.PLCdata1.set_speed("B", 0);
                MdiParent.PLCdata1.plczt_statflag = 1;
                one_ZT.Has_mission = true;
                if (Program.GB_chose_flag == 1)//人工选罐
                {
                    ZT_thread_flag = 1;
                    Program.GB_chose_flag = 0;
                    //one_ZT.Has_mission = true;
                    if (m1.get_TB_data(one_ZT.ZT_reqweight) && m1.get_GB_data_manual(one_ZT.GB_station))//m1.get_TB_data()&& m1.get_GB_data_manual(one_ZT.GB_station)
                    {
                        dbhlper.inser_log("ZT_log", "人工选" + one_ZT.GB_station);
                        setvalue(one_ZT.GB_station);
                        one_ZT.TB_num = MdiParent.PLCdata1.ZT_data.TB_num;
                        one_ZT.TB_need_weight = m1.need_ibag_weight;
                        one_ZT.TB_on_pos = MdiParent.PLCdata1.ZT_data.TB_pos;
                        one_ZT.TB_hight = MdiParent.PLCdata1.ZT_data.TB_hight;
                        one_ZT.TB_Init_weight = MdiParent.PLCdata1.ZT_data.TB_weight;
                        if (m1.F_flag == "F" && m1.fish_station != "ER")
                        {

                            one_ZT.GB_capacity = m1.F_flag;
                            one_ZT.GB_station = m1.fish_station;
                            one_ZT.GB_have_wight = m1.fish_weight;
                            one_ZT.GB_full_wight = m1.fish_init_weight;
                            one_ZT.GB_train_in_times = m1.train_in_time;
                            MdiParent.gk1.GK_station = m1.fish_station;
                            if (true)//MdiParent.tb1.TB_init_result()
                            {
                                writelisview("铁包口", "铁包口视觉检测正常!", "log");
                                LogHelper.loginfo.Info("铁包口视觉检测正常！");
                                //将罐口号和罐口对应
                                MdiParent.gk1.GK_record_num = one_ZT.GB_num.ToString();
                                //启动罐口检测
                                one_ZT.TB_BK_vision = true;
                                MdiParent.gk1.start_GK_state = true;
                                Delay(1000);
                                if (MdiParent.gk1.GK_state == true)
                                {
                                    MdiParent.gk1.GK_state = false;
                                    writelisview("罐包口", "罐包口视觉检测正常！", "log");
                                    dbhlper.inser_log("ZT_log", "罐包口视觉检测正常");
                                    one_ZT.GB_GK_vision = true;
                                    one_ZT.GB_have_wight = m1.fish_weight;
                                    threadflag = true;
                                    LogHelper.loginfo.Info("将启动满罐折铁线程！");
                                    full_thread = new Thread(process_model_full);
                                    full_thread.IsBackground = true;
                                    full_thread.Start();
                                    
                                    
                                }
                                else
                                {
                                    if (one_ZT.GB_station == "A")
                                        A_live = 0;
                                    if (one_ZT.GB_station == "B")
                                        B_live = 0;
                                    one_ZT.Has_mission = false;
                                    Program.ZT_err_flag = 1;
                                    LogHelper.loginfo.Info("罐口检测未通过！");
                                    writelisview("罐包口", "罐口状态不理想！", "err");
                                    one_ZT.GB_GK_vision = false;
                                    ZT_thread_flag = 1000;
                                    threadflag = false;
                                }

                            }

                        }
                        else if (m1.F_flag == "NF" && m1.fish_station != "error")
                        {
                            one_ZT.GB_capacity = m1.F_flag;
                            one_ZT.GB_station = m1.fish_station;
                            one_ZT.GB_have_wight = m1.fish_weight;
                            one_ZT.GB_full_wight = m1.fish_init_weight;
                            one_ZT.GB_train_in_times = m1.train_in_time;
                            if (MdiParent.PLCdata1.ZT_data.TB_pos == false)//MdiParent.tb1.TB_init_result()
                            {
                                one_ZT.TB_BK_vision = true;
                                writelisview("罐包口", "二次倾倒，罐口正常！", "log");
                                one_ZT.GB_GK_vision = true;
                                one_ZT.GB_have_wight = m1.fish_weight;
                                threadflag = true;
                                LogHelper.loginfo.Info("将启动半罐折铁线程！");
                                nfull_thread = new Thread(process_model_half);
                                nfull_thread.IsBackground = true;
                                nfull_thread.Start();
                            }
                            else
                            {
                                if (one_ZT.GB_station == "A")
                                    A_live = 0;
                                if (one_ZT.GB_station == "B")
                                    B_live = 0;
                                one_ZT.Has_mission = false;
                                writelisview("铁包口", "铁包口视觉检测失败!", "log");
                                one_ZT.TB_BK_vision = false;
                                ZT_thread_flag = 1000;
                                threadflag = false;
                            }
                        }
                        else
                        {
                            if (one_ZT.GB_station == "A")
                                A_live = 0;
                            if (one_ZT.GB_station == "B")
                                B_live = 0;
                            one_ZT.Has_mission = false;
                            writelisview("折铁模型", "折铁初始化条件错误!", "log");
                            ZT_thread_flag = 1000;
                            threadflag = false;
                        }
                    }
                    else
                    {
                        if (one_ZT.GB_station == "A")
                            A_live = 0;
                        if (one_ZT.GB_station == "B")
                            B_live = 0;
                        one_ZT.Has_mission = false;
                        LogHelper.loginfo.Info("铁水包不在位！");
                        writelisview("罐包管理", "铁水包可能不在位!", "err");
                        ZT_thread_flag = 1000;
                        threadflag = false;
                    }

                }
                else if (Program.GB_chose_flag == 2)//自动选罐
                    {
                       /* ZT_thread_flag = 1;
                        Program.GB_chose_flag = 0;
                        if (m1.get_TB_data() && m1.get_GB_data())
                        {
                            m1.dbhlper.close_conn();
                            one_ZT.TB_num = MdiParent.PLCdata1.ZT_data.TB_num;
                            one_ZT.TB_need_weight = m1.need_ibag_weight;
                            one_ZT.TB_on_pos = MdiParent.PLCdata1.ZT_data.TB_pos;
                            one_ZT.TB_hight = MdiParent.PLCdata1.ZT_data.TB_hight;
                            one_ZT.TB_Init_weight = MdiParent.PLCdata1.ZT_data.TB_weight;


                            if (m1.F_flag == "F" && m1.fish_station != "ER")
                            {
                                one_ZT.GB_capacity = m1.F_flag;
                                one_ZT.GB_station = m1.fish_station;
                                one_ZT.GB_have_wight = m1.fish_weight;
                                one_ZT.GB_full_wight = m1.fish_init_weight;
                                one_ZT.GB_train_in_times = m1.train_in_time;
                                MdiParent.gk1.GK_station = one_ZT.GB_station;
                                if (true)//MdiParent.tb1.TB_init_result()
                                {
                                    writelisview("铁包口", "铁包口视觉检测正常!", "log");
                                    one_ZT.TB_BK_vision = true;
                                    if (null != MdiParent.GK_global)
                                    {
                                        //MdiParent.GK_global.SetGlobalVar("GK_pos", one_ZT.GB_station);
                                        //string strVal = globalVar.GetGlobalVar("var0"); // 获取全局变量

                                        MdiParent.gk1.start_GK_state = true;
                                        Thread.Sleep(500);
                                        if (MdiParent.gk1.GK_state == true)
                                        {
                                            MdiParent.gk1.GK_state = false;
                                            writelisview("罐包口", "罐包口视觉检测正常！", "log");
                                            one_ZT.GB_GK_vision = true;
                                            one_ZT.GB_have_wight = m1.fish_weight;
                                            threadflag = true;
                                            full_thread = new Thread(process_model_full);
                                            full_thread.IsBackground = true;
                                            full_thread.Start();

                                        }
                                        else
                                        {
                                            one_ZT.Has_mission = false;
                                            writelisview("罐包口", "罐包口视觉检测异常！", "log");
                                            one_ZT.GB_GK_vision = false;
                                            ZT_thread_flag = 1000;
                                            threadflag = false;
                                        }
                                    }
                                    else
                                    {
                                        one_ZT.Has_mission = false;
                                        writelisview("罐口相机", "罐口相机全局变量选择失败！", "log");
                                        one_ZT.GB_GK_vision = false;
                                        ZT_thread_flag = 1000;
                                        threadflag = false;
                                    }
                                }
                                else
                                {
                                    one_ZT.Has_mission = false;
                                    writelisview("铁包口", "铁包口视觉检测异常!", "log");
                                    one_ZT.TB_BK_vision = false;
                                    ZT_thread_flag = 1000;
                                    threadflag = false;
                                }
                            }
                            else if (m1.F_flag == "NF" && m1.fish_station != "error")
                            {
                                one_ZT.GB_capacity = m1.F_flag;
                                one_ZT.GB_station = m1.fish_station;
                                one_ZT.GB_have_wight = m1.fish_weight;
                                one_ZT.GB_full_wight = m1.fish_init_weight;
                                one_ZT.GB_train_in_times = m1.train_in_time;
                                if (MdiParent.tb1.TB_init_result())
                                {
                                    writelisview("铁包口", "铁包口视觉检测正常!", "log");
                                    one_ZT.TB_BK_vision = true;
                                    writelisview("罐包口", "二次倾倒，罐口正常！", "log");
                                    one_ZT.GB_GK_vision = true;
                                    one_ZT.GB_have_wight = m1.fish_weight;
                                    threadflag = true;
                                    nfull_thread = new Thread(process_model_half);
                                    nfull_thread.IsBackground = true;
                                    nfull_thread.Start();
                                }
                                else
                                {
                                    one_ZT.Has_mission = false;
                                    writelisview("铁包口", "铁包口视觉检测失败!", "log");
                                    one_ZT.TB_BK_vision = false;
                                    ZT_thread_flag = 1000;
                                    threadflag = false;
                                }
                            }
                            else
                            {
                                one_ZT.Has_mission = false;
                                writelisview("折铁模型", "折铁初始化条件错误!", "log");
                                ZT_thread_flag = 1000;
                                threadflag = false;
                            }
                        }
                        else
                        {
                            one_ZT.Has_mission = false;
                            writelisview("罐包管理", "罐包管理系统出错!", "log");
                            ZT_thread_flag = 1000;
                            threadflag = false;
                        }*/
                    }
                else if(Program.GB_chose_flag==4)
                {
                    ZT_thread_flag = 1;
                    Program.GB_chose_flag = 0;
                    
                    //one_ZT.Has_mission = true;
                    if (MdiParent.PLCdata1.ZT_data.GB_A_0_limt == true && MdiParent.PLCdata1.ZT_data.GB_A_angle > -300)
                    {
                        writelisview("鱼雷罐", "A鱼雷自动回罐!", "log");
                        LogHelper.loginfo.Info("A鱼雷自动回罐！");
                        one_ZT.GB_capacity= "NF";
                        one_ZT.TB_need_weight = -50;
                        setvalue(one_ZT.GB_station);

                        one_ZT.TB_num = MdiParent.PLCdata1.ZT_data.TB_num;
                        one_ZT.TB_on_pos = MdiParent.PLCdata1.ZT_data.TB_pos;
                        one_ZT.TB_hight = MdiParent.PLCdata1.ZT_data.TB_hight;
                        one_ZT.TB_Init_weight = MdiParent.PLCdata1.ZT_data.TB_weight;
                        one_ZT.GB_train_in_times = DateTime.Now;
                        threadflag = true;
                        nfull_thread = new Thread(process_model_half);
                        nfull_thread.IsBackground = true;
                        nfull_thread.Start();
  
                      
                    }
                    else
                    {
                        if (one_ZT.GB_station == "A")
                            A_live = 0;
                        if (one_ZT.GB_station == "B")
                            B_live = 0;
                        one_ZT.Has_mission = false;
                        LogHelper.loginfo.Info("A鱼雷罐已回零！");
                        writelisview("罐包管理", "A鱼雷罐已回零!", "err");
                        ZT_thread_flag = 1000;
                        threadflag = false;
                    }
                }
                else if (Program.GB_chose_flag == 5)
                {
                    ZT_thread_flag = 1;
                    Program.GB_chose_flag = 0;
                   // Program.GB_station = "B";
                    //one_ZT.Has_mission = true;
                    if (MdiParent.PLCdata1.ZT_data.GB_B_0_limt == true && MdiParent.PLCdata1.ZT_data.GB_B_angle > -300)
                    {
                        writelisview("鱼雷罐", "鱼雷自动回罐!", "log");

                        one_ZT.GB_capacity = "NF";
                        one_ZT.TB_need_weight = -50;
                        setvalue(one_ZT.GB_station);

                        one_ZT.TB_num = MdiParent.PLCdata1.ZT_data.TB_num;
                        one_ZT.TB_on_pos = MdiParent.PLCdata1.ZT_data.TB_pos;
                        one_ZT.TB_hight = MdiParent.PLCdata1.ZT_data.TB_hight;
                        one_ZT.TB_Init_weight = MdiParent.PLCdata1.ZT_data.TB_weight;
                        one_ZT.GB_train_in_times = DateTime.Now;

                        threadflag = true;
                        nfull_thread = new Thread(process_model_half);
                        nfull_thread.IsBackground = true;
                        nfull_thread.Start();
                    }
                    else
                    {
                        if (one_ZT.GB_station == "A")
                            A_live = 0;
                        if (one_ZT.GB_station == "B")
                            B_live = 0;
                        one_ZT.Has_mission = false;
                        LogHelper.loginfo.Info("B鱼雷罐已回零！");
                        writelisview("罐包管理", "B鱼雷罐已回零!", "err");
                        ZT_thread_flag = 1000;
                        threadflag = false;
                    }
                }
                else
                {
                    if (one_ZT.GB_station == "A")
                        A_live = 0;
                    if (one_ZT.GB_station == "B")
                        B_live = 0;
                    Program.GB_chose_flag = 3;
                    ZT_thread_flag = 1000;
                    Program.GB_chose_flag = 0;
                }    

                
            }
            catch (Exception e)
            {
                if (one_ZT.GB_station == "A")
                    A_live = 0;
                if (one_ZT.GB_station == "B")
                    B_live = 0;
                Thread.Sleep(1000);
                ZT_thread_flag = 1000;
                LogHelper.loginfo.Info("线程选择错误！");
            }
            finally
            {
                dbhlper.close_conn();
            }



        }
        //开流角度模型  用于计算开流角度
        public bool calc_fangle()
        {

            try
            {
                //模型计算获取开流倾角
                limt_angle = 2000;
                return true;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog("计算极限倾角", e);
                return false;
            }

        }
        public void calc_speed()
        {

        }

        public void setvalue(string flag)
        {
            one_ZT.TB_hight = MdiParent.PLCdata1.ZT_data.TB_LIQID;
            one_ZT.TB_weight = MdiParent.PLCdata1.ZT_data.TB_weight;
            if (flag == "A")
            {
                one_ZT.GB_angle = MdiParent.PLCdata1.ZT_data.GB_A_angle;
                one_ZT.GB_num = MdiParent.PLCdata1.ZT_data.GB_A_num;
                one_ZT.GB_0_limt = MdiParent.PLCdata1.ZT_data.GB_A_0_limt;
                one_ZT.GB_120_limt = MdiParent.PLCdata1.ZT_data.GB_A_120_limt;
                one_ZT.GB_on_pos = MdiParent.PLCdata1.ZT_data.GB_posA;
                one_ZT.GB_speed = MdiParent.PLCdata1.ZT_data.GB_A_speed;//不用真实  真实值变动太大
                one_ZT.GB_connect = MdiParent.PLCdata1.ZT_data.GB_A_connect;

            }
            else
            {
                one_ZT.GB_angle = MdiParent.PLCdata1.ZT_data.GB_B_angle;
                one_ZT.GB_num = MdiParent.PLCdata1.ZT_data.GB_B_num;
                one_ZT.GB_0_limt = MdiParent.PLCdata1.ZT_data.GB_B_0_limt;
                one_ZT.GB_120_limt = MdiParent.PLCdata1.ZT_data.GB_B_120_limt;
                one_ZT.GB_on_pos = MdiParent.PLCdata1.ZT_data.GB_posB;
                one_ZT.GB_speed = MdiParent.PLCdata1.ZT_data.GB_B_speed;
                one_ZT.GB_connect = MdiParent.PLCdata1.ZT_data.GB_B_connect;
            }


        }
       

        public  void process_model_full()
        {
            //线程开始前发送开始
            int edg_count = 0 , last_speed_count = 0;
            float no_iron_angle = 0;
            int limt_has_set = 0;
            setvalue(one_ZT.GB_station);
            string ztstate = "";
            Program.testcode = "0";
            //240吨前要小于1.6t/s  240-260小于1t/s  260-280小于0.25t/s
            //耗时巨大的代码  
            try
            {
                //[ID],[fishstation],[carnum],[is_full],[train_in_times],[lot],[icode],[icard],[startime],[stoptime],[Tare_Weight],[f_full_weight],[f_has_weight],[i_need_weight],[tempture],[iron_dirction]
                //plc开始信号  测试阶段需关闭
                MdiParent.PLCdata1.write_startsignal(one_ZT.GB_station);
                Thread.Sleep(1000);
                Single last_agle = 0;
                DateTime beforDT = DateTime.Now;

                //dr = dbhelper.MultithreadDataReader("SELECT * FROM [AutoSteel].[dbo].[banci] where time_load!='' and name_class!='0' order by time_load DESC");
                //if (dr.Read())
                //{
                //    banci = Convert.ToString(dr["name_class"]);
                //    load = (DateTime)dr["time_load"];

                //    //string sqltext_load = string.Format("insert into  [AutoSteel].[dbo].[RealTime] (name_class , time_load ,startime)VALUES ('"+banci+"','"+load+"','"+DateTime.Now+"')");
                //    //dbhlper.MultithreadExecuteNonQuery(sqltext_load);
                //}

                string sqltext = string.Format("insert into  [AutoSteel].[dbo].[RealTime] VALUES ('{0}','{1}','{2}','{3}','','','','{4}','','','{5}','{6}','{7}','{8}','{9}','','','','')", one_ZT.GB_station, one_ZT.GB_num, one_ZT.GB_capacity, one_ZT.GB_train_in_times, beforDT, one_ZT.GB_full_wight.ToString(), one_ZT.GB_have_wight.ToString(), (one_ZT.ZT_reqweight).ToString(), one_ZT.TB_weight.ToString(), one_ZT.TB_num.ToString());
                dbhlper.MultithreadExecuteNonQuery(sqltext);
                writelisview("模型", "启动模型！", "log");
                tl_good_count = 0;
                tl_bad_count = 0;
                tl_count = 0;
                int tl_smok_count = 0;
                bool tl_err_flag = false;
                int gb_hight_speed_count = 0,gb_low_speed_count=0;
                MdiParent.PLCdata1.write_resetstartsignal(one_ZT.GB_station);
                LogHelper.loginfo.Info("满罐折铁线程启动！");
                #region
                while (threadflag)
                {
                    Thread.Sleep(500);
                    setvalue(one_ZT.GB_station);
                    MdiParent.PLCdata1.write_mission_signal(one_ZT.Has_mission);
                    //极限角度回罐
                    //////模型计算给定速度  现在是直接给定
                    if (ZT_program_flag == 0)
                    {                        
                        if(one_ZT.GB_angle<600)
                        {
                            if (one_ZT.GB_speed == 20)
                            {
                                ZT_program_flag = 0;
                            }
                            else
                            {
                                MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 20);
                                writelisview("模型", "设置初始速度！", "log");
                            }
                        }
                        else
                        {
                            //writelisview("模型", "高速运行完成！", "log");
                            writelisview("模型", "启动罐口边缘连续检测算法！", "log");
                            MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 20);
                            ZT_program_flag = 1;
                            LogHelper.loginfo.Info("满罐角度大于30，进入1！");
                        }                           
                    }
                    //获取罐口边缘检测结果 计算炉口边缘 亮度
                    if (ZT_program_flag == 1)
                    {
                        if(MdiParent.tl1.Get_iron==false)
                        {
                            //不断增加角度
                            if (one_ZT.GB_angle - last_agle > 60)
                            {
                                last_agle = one_ZT.GB_angle;
                                MdiParent.GK_global.SetGlobalVar("GK_pos", one_ZT.GB_station);
                                MdiParent.gk1.start_GK_edg_state=true;
                                Delay(500);
                                if (MdiParent.gk1.GK_edg_state == true)
                                {
                                    MdiParent.gk1.GK_edg_state = false;
                                    edge_value.Add(MdiParent.gk1.Fall_edga_light);
                                }
                                else
                                {
                                    writelisview("模型", "边缘检测一次！", "err");
                                    edge_value.Add(0);
                                }
                                if (edge_value.Count >= 5)
                                {
                                    //实际应该大于0  测试改为==0
                                    foreach(float edge_val in edge_value)
                                    {
                                        if(edge_val>=0)
                                        {
                                            edg_count++;
                                        }
                                    }
                                    if (edg_count > 1)
                                    {
                                        MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 20);
                                        ZT_program_flag = 2;
                                        one_ZT.GB_edge = true;
                                        writelisview("模型", "正常进入慢速开流！", "log");
                                        LogHelper.loginfo.Info("正常进入慢速开流，进入2！");


                                    }
                                    else
                                    {
                                        LogHelper.loginfo.Info("连续边缘检测结果异常，进入11！");
                                        writelisview("模型", "连续边缘检测结果异常！", "err");
                                        ZT_program_flag = 11;
                                        no_iron_angle = one_ZT.GB_angle;
                                    }
                                }
                                else
                                {
                                    ZT_program_flag = 1;
                                }

                            }
                            else
                            {
                                if (one_ZT.GB_speed == 20)
                                {
                                    ZT_program_flag = 1;
                                }
                                else
                                {
                                    MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 20);
                                    ZT_program_flag = 1;
                                }
                            }
                        }
                        else
                        {
                            ZT_program_flag = 2;
                            writelisview("模型", "提前慢速开流！", "log");
                            MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 10);
                        }
                        
                    }
                    //  慢速开流  模型计算开流极限角度  开启铁流检测
                    if (ZT_program_flag == 2)
                    {
                        if (one_ZT.GB_angle < limt_angle)
                        {
                            if (MdiParent.tl1.Get_iron == true)//
                            {
                                ZT_program_flag = 3;
                                one_ZT.TL = true;
                                one_ZT.TL_in_TB = true;
                                writelisview("模型", "检测到铁流！", "log");
                                LogHelper.loginfo.Info("正常角度见铁流，进入3！");
                                MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 10);
                            }
                            else
                            {
                                if (one_ZT.GB_speed == 10)
                                {
                                    ZT_program_flag = 2;
                                }
                                else
                                {
                                    MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 10);
                                }
                            }
                        }
                        else
                        {
                            writelisview("模型", "角度超限未检测到铁流！", "log");
                            writelisview("模型", "角度超限未检测到铁流！", "err");
                            LogHelper.loginfo.Info("角度过大未见铁流，进入3！");
                            ZT_program_flag = 11;
                            no_iron_angle = one_ZT.GB_angle;
                        }

                    }
                    //开流稳定阶段
                    if (ZT_program_flag == 3)
                    {
                        tl_count += 1;
                        if (MdiParent.tl1.Get_iron == true || MdiParent.PLCdata1.TB_weight_speed > 0.09)
                        {
                            if(MdiParent.tl1.TL_angle==true)
                            {
                                tl_good_count += 1;
                                tl_bad_count = 0;

                            }
                            else
                            {
                                tl_bad_count += 1;
                            }
                           
                            //开流阶段限速
                            if (MdiParent.PLCdata1.TB_weight_speed < full_speed * 0.4)
                            {
                                if (one_ZT.GB_speed == 10)
                                    ZT_program_flag = 3;
                                else
                                    MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 10);
                            }
                            else
                            {
                                if (one_ZT.GB_speed == 0)
                                    ZT_program_flag = 3;
                                else
                                    MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 0);
                            }

                        }
                        else
                        {
                            if (one_ZT.GB_speed == 10)
                                ZT_program_flag = 3;
                            else
                            {
                                //开流阶段限速
                                if (MdiParent.PLCdata1.TB_weight_speed < full_speed * 0.4)
                                {
                                    if (one_ZT.GB_speed == 10)
                                        ZT_program_flag = 3;
                                    else
                                        MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 10);
                                }
                                else
                                {
                                    if (one_ZT.GB_speed == 0)
                                        ZT_program_flag = 3;
                                    else
                                        MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 0);
                                }
                            }
                        }
                        if(tl_count<150)
                        {
                            if(tl_good_count > tl_good_count_limt)
                            {
                                ZT_program_flag = 4;
                                MdiParent.PLCdata1.set_speed(one_ZT.GB_station);
                                writelisview("模型", "开流成功！", "log");
                                LogHelper.loginfo.Info("开流稳定结束，进入4！");
                            }
                            else if(MdiParent.tl1.TL_angle==false)
                            {
                                if(tl_bad_count< tl_good_count_limt)
                                {
                                    if (one_ZT.GB_speed == 0)
                                        ZT_program_flag = 3;
                                    else
                                        MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 0);
                                    Thread.Sleep(1000);
                                }
                                else
                                {
                                    ZT_program_flag = 10;
                                    LogHelper.loginfo.Info("开流稳定有问题，进入10！");
                                    writelisview("模型", "罐口有问题！", "err");
                                }
                                
                                
                            }
                            else
                            {
                                ZT_program_flag = 3;
                            }
                        }
                        else
                        {
                            ZT_program_flag = 10;
                            writelisview("模型", "开流尝试次数过多！", "log");
                        }
                    }
                    //正常折铁阶段
                    if (ZT_program_flag == 4)
                    {
                        if (one_ZT.TB_weight <= one_ZT.ZT_reqweight * 0.75)
                        {
                            #region
                            if (MdiParent.tl1.TL_station != one_ZT.GB_station)
                            {
                                tl_err_flag = true;
                            }
                            else
                            {
                                tl_err_flag = false;
                            }
                            if (MdiParent.tl1.TL_smok == true || tl_err_flag == true)
                            {
                                tl_smok_count += 1;
                            }
                            else
                            {
                                tl_smok_count = 0;
                            }

                           
                            if (one_ZT.TB_weight - one_ZT.TB_Init_weight < 30)
                            {
                                
                                if (tl_smok_count > 10)
                                {
                                    ZT_program_flag = 10;
                                    writelisview("模型", "罐口问题转人工！", "log");
                                    writelisview("模型", "烟气过大超过十次！", "err");
                                }
                                else if (tl_smok_count == 1)
                                {
                                    writelisview("模型", "有烟气降速倾倒！", "log");
                                    writelisview("模型", "有烟气降速倾倒！", "err");
                                    MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 0);
                                }
                                else if (tl_smok_count > 2 || tl_err_flag == true)
                                {
                                    writelisview("模型", "烟气过大回折！", "log");
                                    writelisview("模型", "烟气过大回折！", "err");
                                    MdiParent.PLCdata1.set_speed(one_ZT.GB_station, -20);
                                }//前半段低速度
                                else if (MdiParent.PLCdata1.TB_weight_speed < full_speed * 0.4)
                                {
                                   
                                    if (one_ZT.GB_speed == 20)
                                        ZT_program_flag = 4;
                                    else
                                    {
                                        MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 20);
                                    }

                                }
                                else
                                {
                                    if (one_ZT.GB_speed == 0)
                                        ZT_program_flag = 4;
                                    else
                                        MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 0);
                                }
                            }
                            else
                            {
                                if (tl_smok_count > 5)
                                {
                                    ZT_program_flag = 10;
                                }
                                else if (tl_smok_count == 1)
                                {
                                    writelisview("模型", "有烟气降速倾倒！", "log");
                                    writelisview("模型", "有烟气降速倾倒！", "err");
                                    MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 0);
                                }
                                else if (tl_smok_count > 2 || tl_err_flag == true)
                                {
                                    writelisview("模型", "烟气过大回折！", "log");
                                    writelisview("模型", "烟气过大回折！", "err");
                                    MdiParent.PLCdata1.set_speed(one_ZT.GB_station, -20);
                                }
                                #endregion
                                //高速控制
                                else if (MdiParent.PLCdata1.TB_weight_speed < full_speed)
                                {
                                    if (one_ZT.GB_speed == 20)
                                        ZT_program_flag = 4;
                                    else
                                    {
                                        MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 20);
                                    }

                                }
                                else
                                {
                                    if (one_ZT.GB_speed == 0)
                                        ZT_program_flag = 4;
                                    else
                                        MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 0);
                                }
                            }


                        }
                        //后半段低速度
                        else if (one_ZT.TB_weight > one_ZT.ZT_reqweight * 0.75 && one_ZT.TB_weight <= one_ZT.ZT_reqweight * 0.85)
                        {
                            if (MdiParent.PLCdata1.TB_weight_speed < full_speed * 0.6)
                            {
                                if (one_ZT.GB_speed == 10)
                                    ZT_program_flag = 4;
                                else
                                {
                                    //if (MdiParent.PLCdata1.TB_weight_speed > full_speed * 0.5)
                                    //    ZT_program_flag = 4;
                                    //else
                                    MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 10);
                                }

                            }
                            else
                            {
                                if (one_ZT.GB_speed == 0)
                                    ZT_program_flag = 4;
                                else
                                    MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 0);
                            }
                        }
                        else if (one_ZT.TB_weight > one_ZT.ZT_reqweight * 0.85 && one_ZT.TB_weight <= one_ZT.ZT_reqweight * 0.95)
                        {
                            if (MdiParent.PLCdata1.TB_weight_speed < full_speed * 0.5)
                            {
                                if (one_ZT.GB_speed == 10)
                                    ZT_program_flag = 4;
                                else
                                {
                                    //if (MdiParent.PLCdata1.TB_weight_speed > full_speed * 0.5)
                                    //    ZT_program_flag = 4;
                                    //else
                                    MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 10);
                                }

                            }
                            else
                            {
                                if (one_ZT.GB_speed == 0)
                                    ZT_program_flag = 4;
                                else
                                    MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 0);
                            }
                        }
                        else
                        {
                            //返回点
                            LogHelper.loginfo.Info("正向倾倒结束，进入5！");
                            ZT_program_flag = 5;
                            one_ZT.ZT_back = true;
                            writelisview("模型", "启动返回程序！", "log");
                        }


                    }
                    //折铁返回罐内有足够铁量时的返回程序
                    if (ZT_program_flag == 5)//&& MdiParent.tl1.TL_light_result() == true
                    {
                        if (one_ZT.TB_weight > one_ZT.ZT_reqweight * 0.95 && one_ZT.TB_weight < one_ZT.ZT_reqweight * 0.98)
                        {
                            if (MdiParent.tl1.Get_iron == true || MdiParent.PLCdata1.TB_weight_speed > 0.05)
                            {
                                if (MdiParent.PLCdata1.TB_weight_speed > full_speed * 0.8)
                                {
                                    Program.testcode = "0.95_1.4";
                                    if (one_ZT.GB_speed == -20)
                                        ZT_program_flag = 5;
                                    else
                                        MdiParent.PLCdata1.set_speed(one_ZT.GB_station, -20);
                                }
                                else if (MdiParent.PLCdata1.TB_weight_speed > full_speed * 0.6)//////////hr
                                {
                                    Program.testcode = "0.95_1";
                                    if (one_ZT.GB_speed == -10)
                                        ZT_program_flag = 5;
                                    else
                                        MdiParent.PLCdata1.set_speed(one_ZT.GB_station, -10);
                                }
                                else if (MdiParent.PLCdata1.TB_weight_speed < full_speed * 0.05)
                                {
                                    Program.testcode = "0.98_0.2";
                                    if (one_ZT.GB_speed == 10)
                                        ZT_program_flag = 5;
                                    else
                                    {
                                        MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 10);
                                    }
                                }
                                else
                                {
                                    Program.testcode = "0.95_0";
                                    if (one_ZT.GB_speed == 0)
                                        ZT_program_flag = 5;
                                    else
                                        MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 0);
                                }
                            }

                        }
                        else if (one_ZT.TB_weight >= one_ZT.ZT_reqweight * 0.98 && one_ZT.TB_weight < one_ZT.ZT_reqweight - 0.8)
                        {
                            if ((MdiParent.tl1.Get_iron == true || MdiParent.PLCdata1.TB_weight_speed > 0.05))
                            {
                                if (MdiParent.PLCdata1.TB_weight_speed > full_speed * 0.6)
                                {
                                    Program.testcode = "0.98_0.5";

                                    if (one_ZT.GB_speed == -20)
                                        ZT_program_flag = 5;
                                    else
                                        MdiParent.PLCdata1.set_speed(one_ZT.GB_station, -20);
                                }
                                else if (MdiParent.PLCdata1.TB_weight_speed > full_speed * 0.25 && MdiParent.PLCdata1.TB_weight_speed <= full_speed * 0.6)
                                {
                                    Program.testcode = "0.98_0.5";

                                    if (one_ZT.GB_speed == -10)
                                        ZT_program_flag = 5;
                                    else
                                        MdiParent.PLCdata1.set_speed(one_ZT.GB_station, -10);
                                }
                                //else if (MdiParent.PLCdata1.TB_weight_speed < full_speed * 0.055)
                                //{
                                //    Program.testcode = "0.98_0.2";
                                //    if (one_ZT.GB_speed == 10)
                                //        ZT_program_flag = 5;
                                //    else
                                //    {
                                //        //if (MdiParent.PLCdata1.TB_weight_speed >= 0.179)
                                //        //    MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 0);
                                //        //else
                                //            MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 10);
                                //    }
                                //}
                                else if (MdiParent.PLCdata1.TB_weight_speed < full_speed * 0.09)/////////hr
                                {
                                    last_speed_count++;
                                    Program.testcode = "0.98_0.2";
                                    if (last_speed_count < 3)
                                    {
                                        if (one_ZT.GB_speed == 10)
                                            ZT_program_flag = 5;
                                        else
                                        {
                                            MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 10);
                                        }
                                    }
                                    else
                                    {
                                        if (one_ZT.GB_speed == 0)
                                            ZT_program_flag = 5;
                                        else
                                        {
                                            MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 0);
                                        }
                                    }
                                    if (last_speed_count == 4)
                                        last_speed_count = 0;

                                }
                                else
                                {
                                    Program.testcode = "0.98_0";
                                    if (one_ZT.GB_speed == 0)
                                        ZT_program_flag = 5;
                                    else
                                        MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 0);

                                }
                            }
                            else
                            {
                                LogHelper.loginfo.Info("折铁末端控流结束1，进入6！");
                                ZT_program_flag = 6;
                            }

                        }
                        else
                        {
                            LogHelper.loginfo.Info("折铁末端控流结束2，进入6！");
                            ZT_program_flag = 6;
                        }
                    }
                    //重量达标后到铁流消失
                    if (ZT_program_flag == 6)
                    {
                        if (one_ZT.GB_speed == -20)
                            ZT_program_flag = 6;
                        else
                            MdiParent.PLCdata1.set_speed(one_ZT.GB_station, -20);

                        if (MdiParent.tl1.Get_iron == false)
                        {
                            LogHelper.loginfo.Info("铁流消失，进入7！");
                            ZT_program_flag = 7;
                            writelisview("模型", "折铁返回中铁流消失！", "log");
                            //本次折铁铁流消失
                            DateTime afterDT = System.DateTime.Now;
                            TimeSpan tl_ts_stop = afterDT.Subtract(beforDT);
                            minute = (int)Math.Round(tl_ts_stop.TotalMinutes, 2);
                            second = ((double)Math.Round(tl_ts_stop.TotalMinutes, 2) - minute) * 0.6;
                            string state = "7-折铁返回中铁流消失";
                            sqltext = string.Format("UPDATE RealTime SET fall_time='{0}',fall_weight='{1}',fall_state='{2}',tb_hight='{3}' WHERE startime= '{4}'", Math.Round(minute + second, 2).ToString(), one_ZT.TB_weight.ToString(), state, (5404 - one_ZT.TB_hight).ToString(), beforDT);
                            dbhlper.MultithreadExecuteNonQuery(sqltext);
                            no_iron_angle = one_ZT.GB_angle;
                        }
                    }
                    //罐内有余铁时回罐
                    if (ZT_program_flag == 7)
                    {
                        if (no_iron_angle - one_ZT.GB_angle > 200)
                        {
                            LogHelper.loginfo.Info("满罐回罐结束，进入8！");
                            ZT_program_flag = 8;
                            MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 0);
                        }
                        else
                        {
                            if (one_ZT.GB_speed == -20)
                                ZT_program_flag = 7;
                            else
                                MdiParent.PLCdata1.set_speed(one_ZT.GB_station, -20);
                        }
                    }
                    //首次折铁 回罐完成 本次折铁所有动作正常完成
                    if (ZT_program_flag == 8)
                    {
                        Program.GB_chose_flag = 0;
                        if (one_ZT.GB_station == "A")
                            A_live = 0;
                        if (one_ZT.GB_station == "B")
                            B_live = 0;
                        if (A_live==0&&B_live==0)
                            MdiParent.PLCdata1.reset_start(one_ZT.GB_station);
                        one_ZT.Has_mission = false;
                        MdiParent.process_TL.ContinuousRunEnable = false;
                        writelisview("模型", "折铁完成！", "log");
                        LogHelper.loginfo.Info("折铁正常结束！");
                        DateTime afterDT = System.DateTime.Now;
                        TimeSpan ts = afterDT.Subtract(beforDT);
                       // ztstate = "8-正常完成";
                        sqltext = string.Format("UPDATE RealTime SET stoptime= '{0}' WHERE startime= '{1}'", afterDT, beforDT);
                        dbhlper.MultithreadExecuteNonQuery(sqltext);
                        ZT_thread_flag = 1000;
                        ZT_program_flag = 0;
                        tl_count = 0;
                        MdiParent.PLCdata1.write_mission_signal(one_ZT.Has_mission);
                        //one_ZT = null;
                        break;
                    }
                    
                    //极限角度回罐
                    if (ZT_program_flag == 9)
                    {
                        if (one_ZT.GB_speed == -20)
                        {
                            ZT_program_flag = 9;
                            //罐内无铁时的数据记录
                            if (MdiParent.tl1.Get_iron == false)
                            {
                                LogHelper.loginfo.Info("折铁触发极限条件，进入12！"+ztstate);
                                DateTime afterDT = System.DateTime.Now;
                                TimeSpan tl_ts_stop = afterDT.Subtract(beforDT);
                                minute = (int)Math.Round(tl_ts_stop.TotalMinutes, 2);
                                second = ((double)Math.Round(tl_ts_stop.TotalMinutes, 2) - minute) * 0.6;
                                sqltext = string.Format("UPDATE RealTime SET fall_time='{0}',fall_weight='{1}',fall_state='{2}',tb_hight='{3}',stoptime= '{4}' WHERE startime= '{5}'", Math.Round(minute + second, 2).ToString(), one_ZT.TB_weight.ToString(), ztstate, (5404 - one_ZT.TB_hight).ToString(), afterDT, beforDT);
                                dbhlper.MultithreadExecuteNonQuery(sqltext);
                                ZT_program_flag = 12;
                            }
                        }
                        else
                        {
                            ZT_program_flag = 9;
                            MdiParent.PLCdata1.set_speed(one_ZT.GB_station, -20);
                        }

                    }
                    //开流失败 处理至铁流消失 转至11
                    if (ZT_program_flag == 10)
                    {
                        if (one_ZT.GB_speed == -20)
                            ZT_program_flag = 10;
                        else
                            MdiParent.PLCdata1.set_speed(one_ZT.GB_station, -20);

                        if (MdiParent.tl1.Get_iron == false)
                        {
                            ZT_program_flag = 11;
                            writelisview("模型", "开流失败，折铁返回中铁流消失！", "log");
                            //本次折铁铁流消失
                            DateTime afterDT = System.DateTime.Now;
                            TimeSpan tl_ts_stop = afterDT.Subtract(beforDT);

                            minute = (int)Math.Round(tl_ts_stop.TotalMinutes, 2);
                            second = ((double)Math.Round(tl_ts_stop.TotalMinutes, 2) - minute) * 0.6;
                            ztstate = "10-烟气过大停止折铁！";
                            sqltext = string.Format("UPDATE RealTime SET fall_time='{0}',fall_weight='{1}',tb_hight='{2}',fall_state='{3}' WHERE startime= '{4}'", Math.Round(minute + second, 2).ToString(), one_ZT.TB_weight.ToString(), (5404 - one_ZT.TB_hight).ToString(), ztstate, beforDT);
                            dbhlper.MultithreadExecuteNonQuery(sqltext);
                            no_iron_angle = one_ZT.GB_angle;
                        }
                    }
                    //开流失败 铁流消失后回转20度
                    if (ZT_program_flag == 11)
                    {
                        if (no_iron_angle - one_ZT.GB_angle > 200)
                        {
                            MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 0);
                            ZT_thread_flag = 1000;
                            //折铁失败后处理
                            
                        }
                        else
                        {
                            if (one_ZT.GB_speed == -20)
                                ZT_program_flag = 11;
                            else
                                MdiParent.PLCdata1.set_speed(one_ZT.GB_station, -20);
                        }
                    }
                    //达到出铁极限 一直回转到0位 并记录数据
                    if (ZT_program_flag == 12)
                    {
                        if (one_ZT.GB_0_limt == true && one_ZT.GB_angle > -300)
                        {
                            if (one_ZT.GB_speed == -20)
                                ZT_program_flag = 12;
                            else
                                MdiParent.PLCdata1.set_speed(one_ZT.GB_station, -20);
                        }
                        else
                        {
                            MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 0);
                            if (one_ZT.GB_station == "A")
                                A_live = 0;
                            if (one_ZT.GB_station == "B")
                                B_live = 0;
                            if (A_live == 0 && B_live == 0)
                                MdiParent.PLCdata1.reset_start(one_ZT.GB_station);
                            ZT_thread_flag = 1000;
                            Program.GB_chose_flag = 0;
                            one_ZT.Has_mission = false;
                            MdiParent.process_TL.ContinuousRunEnable = false;
                            DateTime afterDT = System.DateTime.Now;
                            TimeSpan ts = afterDT.Subtract(beforDT);

                            minute = (int)Math.Round(ts.TotalMinutes, 2);
                            second = ((double)Math.Round(ts.TotalMinutes, 2) - minute) * 0.6;
                            //sqltext = string.Format("UPDATE RealTime SET stoptime= '{0}',fall_time='{1}',fall_weight='{2}' WHERE startime= '{3}'", afterDT, ts.TotalMinutes.ToString(), (one_ZT.TB_weight - one_ZT.TB_Init_weight).ToString(), beforDT);
                            
                            sqltext = string.Format("UPDATE RealTime SET stoptime= '{0}' WHERE startime= '{1}'", afterDT,beforDT);
                            dbhlper.MultithreadExecuteNonQuery(sqltext);
                            writelisview("模型", "罐包到达0位！", "log");
                            LogHelper.loginfo.Info("折铁回零，结束！");
                            ZT_program_flag = 0;
                            tl_count = 0;
                            MdiParent.PLCdata1.write_mission_signal(one_ZT.Has_mission);
                            break;
                        }
                    }
                    //折铁失败人工处理
                    //if (MdiParent.PLCdata1.ZT_data.ZT_PLC_hum == true)
                    //{
                    //    MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 0);
                    //    MdiParent.PLCdata1.reset_hum_signal();
                    //    writelisview("模型", "人工操作，折铁自动结束！！", "log");
                    //    DateTime afterDT = System.DateTime.Now;
                    //    TimeSpan ts = afterDT.Subtract(beforDT);
                    //    sqltext = string.Format("UPDATE RealTime SET stoptime= '{0}',fall_state='{1}' WHERE startime= '{2}'", afterDT, ZT_program_flag.ToString(), beforDT);
                    //    dbhlper.MultithreadExecuteNonQuery(sqltext);
                    //    one_ZT.Has_mission = false;
                    //    MdiParent.process_GK.ContinuousRunEnable = false;
                    //    MdiParent.process_TL.ContinuousRunEnable = false;
                    //    ZT_thread_flag = 1000;
                    //    threadflag = false;
                    //    tl_count = 0;
                    //    MdiParent.PLCdata1.write_mission_signal(one_ZT.Has_mission);
                    //    break;
                    //}
                    //检测罐角度，当超过设定或极限角度时应返回
                    if (limt_has_set == 0)
                    {
                        if ((5404 - one_ZT.TB_hight) < 580 && limt_has_set == 0)//||MdiParent.PLCdata1.GB_A_remind_weight<300|| one_ZT.TB_weight >= one_ZT.ZT_reqweight + 15
                        {
                            if (ZT_program_flag != 6)
                            {
                                ztstate = "6-净空到达极限";
                                limt_has_set = 1;
                                ZT_program_flag = 6;
                                writelisview("模型", "净空到达极限", "err");
                            }
                        }
                        else if (one_ZT.GB_angle > 2300 && limt_has_set == 0)//||MdiParent.PLCdata1.GB_A_remind_weight<300|| one_ZT.TB_weight >= one_ZT.ZT_reqweight + 15
                        {
                            if (ZT_program_flag != 6)
                            {
                                ztstate = "6-到达极限角度";
                                limt_has_set = 1;
                                ZT_program_flag = 6;
                                writelisview("模型", "到达极限角度", "err");
                            }
                        }
                        else if (one_ZT.GB_120_limt == false && limt_has_set == 0)//||MdiParent.PLCdata1.GB_A_remind_weight<300|| one_ZT.TB_weight >= one_ZT.ZT_reqweight + 15
                        {
                            if (ZT_program_flag != 6)
                            {
                                ztstate = "6-120度限位检测异常";
                                limt_has_set = 1;
                                ZT_program_flag =6;
                                writelisview("模型", "120度限位检测异常", "err");
                            }
                        }
                        else if (MdiParent.PLCdata1.ZT_data.TB_pos == true && limt_has_set == 0)//||MdiParent.PLCdata1.GB_A_remind_weight<300|| one_ZT.TB_weight >= one_ZT.ZT_reqweight + 15
                        {
                            if (ZT_program_flag != 6)
                            {
                                ztstate = "6-铁包不在位";
                                limt_has_set = 1;
                                ZT_program_flag = 6;
                                writelisview("模型", "铁包不在位", "err");
                            }
                        }
                        else if ((one_ZT.TB_weight >= one_ZT.ZT_reqweight - 0.8) && limt_has_set == 0)//||MdiParent.PLCdata1.GB_A_remind_weight<300|| one_ZT.TB_weight >= one_ZT.ZT_reqweight + 15
                        {
                            if (ZT_program_flag != 6)
                            {
                                ztstate = "6-到达设定重量极限";
                                limt_has_set = 1;
                                ZT_program_flag = 6;
                                writelisview("模型", "到达设定重量极限", "err");
                            }
                        }
                        //else if (((one_ZT.GB_station == "A" && MdiParent.PLCdata1.ZT_data.ZT_PLC_hum_A == false) || (one_ZT.GB_station == "B" && MdiParent.PLCdata1.ZT_data.ZT_PLC_hum_B == false)) && limt_has_set == 0)//||MdiParent.PLCdata1.GB_A_remind_weight<300|| one_ZT.TB_weight >= one_ZT.ZT_reqweight + 15
                        //{
                        //    if (ZT_program_flag != 6)
                        //    {
                        //        ztstate = "6-人工强制退出";
                        //        limt_has_set = 1;
                        //        ZT_program_flag = 6;
                        //        writelisview("模型", "人工强制退出", "err");
                        //    }
                        //}
                    }
                    //if (one_ZT.GB_station == "A")
                    //{
                    //    if (MdiParent.PLCdata1.ZT_data.ZT_PLC_hum_A == false)
                    //    {
                    //        ZT_thread_flag = 1000;
                    //    }
                    //}
                    //if (one_ZT.GB_station == "B")
                    //{
                    //    if (MdiParent.PLCdata1.ZT_data.ZT_PLC_hum_B == false)
                    //    {
                    //        ZT_thread_flag = 1000;
                    //    }
                    //}
                    if (ZT_thread_flag==1000)
                    {
                        MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 0);
                        writelisview("模型", "人工强制退出！！", "log");
                        DateTime afterDT = System.DateTime.Now;
                        TimeSpan ts = afterDT.Subtract(beforDT);
                        if(ztstate=="")
                            ztstate = "1000-人工强制终止";

                        sqltext = string.Format("UPDATE RealTime SET stoptime= '{0}',fall_state='{1}'  WHERE startime= '{2}'", afterDT,ztstate, beforDT);
                        dbhlper.MultithreadExecuteNonQuery(sqltext);
                        one_ZT.Has_mission = false;
                        MdiParent.process_GK.ContinuousRunEnable = false;
                        MdiParent.process_TL.ContinuousRunEnable = false;
                        ZT_thread_flag = 1000;
                        ZT_program_flag = 0;
                        threadflag = false;
                        tl_count = 0;
                        MdiParent.PLCdata1.write_mission_signal(one_ZT.Has_mission);
                        break;
                    }
                }
                if (one_ZT.GB_station == "A")
                {
                    A_live = 0;
                }
                if (one_ZT.GB_station == "B")
                {
                    B_live = 0;
                }
                #endregion
            }
            catch (Exception e)
            {
                MdiParent.PLCdata1.write_mission_signal(one_ZT.Has_mission);
                one_ZT.Has_mission = false;
                MdiParent.process_GK.ContinuousRunEnable = false;
                MdiParent.process_TL.ContinuousRunEnable = false;
                writelisview("模型", "折铁模型发生程序失败！！", "log");
                ZT_thread_flag = 1000;
                ZT_program_flag = 0;
                threadflag = false;
                tl_count = 0;
                LogHelper.WriteLog("折铁线程", e);
            }
        }
        public void process_model_half()
        {
            //线程开始前发送开始
            tl_count = 0;
            int limt_has_set = 0, last_speed_count = 0;
            float no_iron_angle=0;
            string ztstate = "";
            setvalue(one_ZT.GB_station);
            Program.testcode = "0";

            float init_weight = 0;
            float now_weight = 0;
            
            //240吨前要小于1.6t/s  240-260小于1t/s  260-280小于0.25t/s
            //耗时巨大的代码  
            try
            {
                MdiParent.PLCdata1.write_startsignal(one_ZT.GB_station);
                Thread.Sleep(1000);
                int tl_smok_count = 0,last_speed_cound=0;
                bool tl_err_flag = false;
                DateTime beforDT = DateTime.Now;
                DateTime recordDT = System.DateTime.Now;
                string sqltext = string.Format("insert into  [AutoSteel].[dbo].[RealTime] VALUES ('{0}','{1}','{2}','{3}','','','','{4}','','','{5}','{6}','{7}','{8}','{9}','','','','')", one_ZT.GB_station, one_ZT.GB_num, one_ZT.GB_capacity, one_ZT.GB_train_in_times, beforDT, one_ZT.GB_full_wight.ToString(), one_ZT.GB_have_wight.ToString(), (one_ZT.ZT_reqweight).ToString(), one_ZT.TB_weight.ToString(), one_ZT.TB_num.ToString());
                dbhlper.MultithreadExecuteNonQuery(sqltext);
                writelisview("模型", "启动模型！", "log");
                MdiParent.PLCdata1.write_resetstartsignal(one_ZT.GB_station);

                //plc开始信号  测试阶段需关闭

                #region
                while (threadflag)
                {
                    //synthes.Rate = -2; //语速,[-10,10]
                    //synthes.Volume = 100; //音量,[0,100]

                    //init_weight = MdiParent.PLCdata1.ZT_data.TB_weight;

                    Thread.Sleep(500);

                    //now_weight = MdiParent.PLCdata1.ZT_data.TB_weight;

                    //if (now_weight - init_weight > 6)
                    //{
                    //    string textToSpeak = "增速异常，请注意";

                    //    synthes.Speak(textToSpeak);//同步
                    //    //synthes.SpeakAsync(textToSpeak);//异步

                    //}

                    MdiParent.PLCdata1.write_mission_signal(one_ZT.Has_mission);
                    setvalue(one_ZT.GB_station);
                    //dbhlper.updata_table("RealTime_Car_Bag", "mid_weight", MdiParent.PLCdata1.GB_A_remind_weight.ToString(), "ID", one_ZT.GB_station);
                    //////模型计算给定速度  现在是直接给定
                    if (ZT_program_flag == 0)
                    {
                         MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 20);
                         ZT_program_flag = 2;
                         MdiParent.PLCdata1.set_speed(one_ZT.GB_station);
                         writelisview("模型", "设置初始速度完成！", "log");
                        LogHelper.loginfo.Info("设置初速度完成，进入2！");
                    }
                    if(ZT_program_flag == 2)
                    {
                        if (one_ZT.GB_angle < 2100)
                        {
                            if(MdiParent.tl1.Get_iron == true)
                            {
                                ZT_program_flag = 3;
                                one_ZT.TL = true;
                                one_ZT.TL_in_TB = true;
                                MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 10);
                                writelisview("模型", "检测到铁流！", "log");
                                LogHelper.loginfo.Info("检测到铁流，进入3！");
                            }
                            else
                            {
                                if (one_ZT.GB_speed == 20)
                                    ZT_program_flag = 2;
                                else
                                    MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 20);
                            }
                            
                        }
                        else
                        {
                            ZT_program_flag = 10;
                            MdiParent.PLCdata1.set_speed(one_ZT.GB_station);
                            LogHelper.loginfo.Info("出铁角度过大，进入10！");
                            writelisview("模型", "出铁失败角度过大！", "err");
                        }
                        
                    }
                    //开流阶段
                    if (ZT_program_flag == 3)
                    {
                        tl_count += 1;
                        if (MdiParent.tl1.Get_iron == true || MdiParent.PLCdata1.TB_weight_speed > 0.09)
                        {
                            if(tl_count>20)
                            {
                                ZT_program_flag = 4;
                                MdiParent.PLCdata1.set_speed(one_ZT.GB_station);
                                LogHelper.loginfo.Info("开流完成，进入4！");
                                writelisview("模型", "进入正常折铁控制程序！", "log");
                            }
                            else
                            {
                                
                                    if (MdiParent.PLCdata1.TB_weight_speed < full_speed*0.4)
                                    {
                                        if (one_ZT.GB_speed == 10)
                                            ZT_program_flag = 3;
                                        else
                                            MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 10);
                                    }
                                    else
                                    {
                                        if (one_ZT.GB_speed == 0)
                                            ZT_program_flag = 3;
                                        else
                                            MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 0);
                                    }
                                
                            }
                            
                        }
                        else
                        {
                            if (one_ZT.GB_speed == 10)
                                ZT_program_flag = 3;
                            else
                                MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 10);
                        }

                    }
                    if (ZT_program_flag == 4)
                    {
                        if (one_ZT.TB_weight <= one_ZT.ZT_reqweight * 0.75)
                        {
                            #region
                            if (MdiParent.tl1.TL_station != one_ZT.GB_station)
                            {
                                tl_err_flag = true;
                            }
                            else
                            {
                                tl_err_flag = false;
                            }
                            if (MdiParent.tl1.TL_smok == true || tl_err_flag == true)
                            {
                                tl_smok_count += 1;
                            }
                            else
                            {
                                tl_smok_count = 0;
                            }


                            if (one_ZT.TB_weight - one_ZT.TB_Init_weight < 30)
                            {

                                if (tl_smok_count > 10)
                                {
                                    ZT_program_flag = 10;
                                    writelisview("模型", "罐口问题转人工！", "log");
                                    writelisview("模型", "连续十次烟气过大！", "err");
                                }
                                else if (tl_smok_count == 1)
                                {
                                    writelisview("模型", "有烟气降速倾倒！", "log");
                                    writelisview("模型", "有烟气降速倾倒！", "err");
                                    MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 0);
                                }
                                else if (tl_smok_count > 2 || tl_err_flag == true)
                                {
                                    writelisview("模型", "烟气过大回折！", "log");
                                    writelisview("模型", "烟气过大回折！", "err");
                                    MdiParent.PLCdata1.set_speed(one_ZT.GB_station, -20);
                                }//前半段低速度
                                else if (MdiParent.PLCdata1.TB_weight_speed < full_speed * 0.4)
                                {

                                    if (one_ZT.GB_speed == 20)
                                        ZT_program_flag = 4;
                                    else
                                    {
                                        MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 20);
                                    }

                                }
                                else
                                {
                                    if (one_ZT.GB_speed == 0)
                                        ZT_program_flag = 4;
                                    else
                                        MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 0);
                                }
                            }
                            else
                            {
                                if (tl_smok_count > 5)
                                {
                                    ZT_program_flag = 10;
                                }
                                else if (tl_smok_count == 1)
                                {
                                    writelisview("模型", "有烟气降速倾倒！", "log");
                                    writelisview("模型", "有烟气降速倾倒！", "err");
                                    MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 0);
                                }
                                else if (tl_smok_count > 2 || tl_err_flag == true)
                                {
                                    writelisview("模型", "烟气过大回折！", "log");
                                    writelisview("模型", "烟气过大回折！", "err");
                                    MdiParent.PLCdata1.set_speed(one_ZT.GB_station, -20);
                                }
                                #endregion
                                //高速控制
                                else if (MdiParent.PLCdata1.TB_weight_speed < full_speed)
                                {
                                    if (one_ZT.GB_speed == 20)
                                        ZT_program_flag = 4;
                                    else
                                    {
                                        MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 20);
                                    }
                                }
                                else
                                {
                                    if (one_ZT.GB_speed == 0)
                                        ZT_program_flag = 4;
                                    else
                                        MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 0);
                                }
                            }

                        }
                        //后半段低速度
                        else if (one_ZT.TB_weight > one_ZT.ZT_reqweight * 0.75 && one_ZT.TB_weight <= one_ZT.ZT_reqweight * 0.85)
                        {
                            if (MdiParent.PLCdata1.TB_weight_speed < full_speed * 0.6)
                            {
                                if (one_ZT.GB_speed == 10)
                                    ZT_program_flag = 4;
                                else
                                {
                                    MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 10);
                                }

                            }
                            else
                            {
                                if (one_ZT.GB_speed == 0)
                                    ZT_program_flag = 4;
                                else
                                    MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 0);
                            }
                        }
                        else if (one_ZT.TB_weight > one_ZT.ZT_reqweight * 0.85 && one_ZT.TB_weight <= one_ZT.ZT_reqweight * 0.95)
                        {
                            if (MdiParent.PLCdata1.TB_weight_speed < full_speed * 0.5)
                            {
                                if (one_ZT.GB_speed == 10)
                                    ZT_program_flag = 4;
                                else
                                {
                                    MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 10);
                                }

                            }
                            else
                            {
                                if (one_ZT.GB_speed == 0)
                                    ZT_program_flag = 4;
                                else
                                    MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 0);
                            }
                        }
                        else
                        {
                            //返回点
                            ZT_program_flag = 5;
                            one_ZT.ZT_back = true;
                            writelisview("模型", "启动返回程序！", "log");
                            LogHelper.loginfo.Info("正向折铁结束，进入返回5！");
                        }
                    }
                    if (ZT_program_flag == 5)//&& MdiParent.tl1.TL_light_result() == true
                    {
                        if (one_ZT.TB_weight > one_ZT.ZT_reqweight * 0.95 && one_ZT.TB_weight < one_ZT.ZT_reqweight * 0.98)
                        {
                            if (MdiParent.tl1.Get_iron == true || MdiParent.PLCdata1.TB_weight_speed > 0.05)
                            {
                                if (MdiParent.PLCdata1.TB_weight_speed > full_speed * 0.8)
                                {
                                    Program.testcode = "0.95_1.4";
                                    if (one_ZT.GB_speed == -20)
                                        ZT_program_flag = 5;
                                    else
                                        MdiParent.PLCdata1.set_speed(one_ZT.GB_station, -20);
                                }
                                else if (MdiParent.PLCdata1.TB_weight_speed > full_speed * 0.6)////////hr
                                {
                                    Program.testcode = "0.95_1";
                                    if (one_ZT.GB_speed == -10)
                                        ZT_program_flag = 5;
                                    else
                                        MdiParent.PLCdata1.set_speed(one_ZT.GB_station, -10);
                                }
                                else if (MdiParent.PLCdata1.TB_weight_speed < full_speed * 0.05)
                                {
                                    Program.testcode = "0.98_0.2";
                                    if (one_ZT.GB_speed == 10)
                                        ZT_program_flag = 5;
                                    else
                                    {
                                        MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 10);
                                    }

                                }
                                else 
                                {
                                    Program.testcode = "0.95_0";
                                    if (one_ZT.GB_speed == 0)
                                        ZT_program_flag = 5;
                                    else
                                        MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 0);
                                }
                            }

                        }
                        else if (one_ZT.TB_weight >= one_ZT.ZT_reqweight * 0.98 && one_ZT.TB_weight < one_ZT.ZT_reqweight - 0.7)
                        {
                            if ((MdiParent.tl1.Get_iron == true || MdiParent.PLCdata1.TB_weight_speed > 0.05))
                            {
                                if (MdiParent.PLCdata1.TB_weight_speed > full_speed * 0.6)
                                {
                                    Program.testcode = "0.98_0.5";

                                    if (one_ZT.GB_speed == -20)
                                        ZT_program_flag = 5;
                                    else
                                        MdiParent.PLCdata1.set_speed(one_ZT.GB_station, -20);
                                }
                                else if (MdiParent.PLCdata1.TB_weight_speed > full_speed * 0.25&& MdiParent.PLCdata1.TB_weight_speed <= full_speed * 0.6)
                                {
                                    Program.testcode = "0.98_0.5";

                                    if (one_ZT.GB_speed == -10)
                                        ZT_program_flag = 5;
                                    else
                                        MdiParent.PLCdata1.set_speed(one_ZT.GB_station, -10);
                                }
                                //else if (MdiParent.PLCdata1.TB_weight_speed < full_speed * 0.055)
                                //{
                                //    Program.testcode = "0.98_0.2";
                                //    if (one_ZT.GB_speed == 10)
                                //        ZT_program_flag = 5;
                                //    else
                                //    {
                                //            MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 10);
                                //    }

                                //}
                                else if (MdiParent.PLCdata1.TB_weight_speed < full_speed * 0.09)/////////hr
                                {
                                    last_speed_count++;
                                    Program.testcode = "0.98_0.2";
                                    if (last_speed_count < 3)
                                    {
                                        if (one_ZT.GB_speed == 10)
                                            ZT_program_flag = 5;
                                        else
                                        {
                                            MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 10);
                                        }
                                    }
                                    else
                                    {
                                        if (one_ZT.GB_speed == 0)
                                            ZT_program_flag = 5;
                                        else
                                        {
                                            MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 0);
                                        }
                                    }
                                    if (last_speed_count == 4)
                                        last_speed_count = 0;

                                }
                                else
                                {
                                    Program.testcode = "0.98_0";
                                    if (one_ZT.GB_speed == 0)
                                        ZT_program_flag = 5;
                                    else
                                        MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 0);

                                }
                            }
                            else
                            {
                                ztstate = "正常结束";
                                LogHelper.loginfo.Info("控流结束，进入9！");
                                ZT_program_flag = 9;
                            }

                        }
                        else
                        {
                            ztstate = "正常结束";
                            LogHelper.loginfo.Info("控流结束，进入9！");
                            ZT_program_flag = 9;
                        }
                    }
                    if (ZT_program_flag == 6)//&& MdiParent.tl1.TL_light_result() == true
                    {
                        if (one_ZT.GB_speed == -20)
                            ZT_program_flag = 6;
                        else
                            MdiParent.PLCdata1.set_speed(one_ZT.GB_station, -20);

                        if (MdiParent.tl1.Get_iron==false)
                        {
                            ZT_program_flag =7;
                            writelisview("模型", "折铁返回中铁流消失！", "log");
                            LogHelper.loginfo.Info("返回中铁流消失，进入7！");
                            //本次折铁铁流消失
                            DateTime afterDT = System.DateTime.Now;
                            TimeSpan tl_ts_stop = afterDT.Subtract(beforDT);

                            minute = (int)Math.Round(tl_ts_stop.TotalMinutes, 2);
                            second = ((double)Math.Round(tl_ts_stop.TotalMinutes, 2) - minute) * 0.6;
                            string state = "7-折铁返回中铁流消失";
                            sqltext = string.Format("UPDATE RealTime SET fall_time='{0}',fall_weight='{1}',fall_state='{2}',tb_hight='{3}' WHERE startime= '{4}'", Math.Round(minute + second, 2).ToString(), one_ZT.TB_weight.ToString(), state, (5404 - one_ZT.TB_hight).ToString(),beforDT);
                            dbhlper.MultithreadExecuteNonQuery(sqltext);
                            no_iron_angle = one_ZT.GB_angle;
                        }
                    }
                    //罐内有余铁时回罐
                    if(ZT_program_flag == 7)
                    {
                        if(no_iron_angle-one_ZT.GB_angle>200)
                        {
                            ZT_program_flag = 8;
                            MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 0);
                        }
                        else
                        {
                            if (one_ZT.GB_speed == -20)
                                ZT_program_flag = 7;
                            else
                                MdiParent.PLCdata1.set_speed(one_ZT.GB_station, -20);
                        }
                    }
                    if (ZT_program_flag == 8)//&& MdiParent.tl1.TL_light_result() == false
                    {
                        Program.GB_chose_flag = 0;
                        one_ZT.Has_mission = false;
                        MdiParent.process_TL.ContinuousRunEnable = false;
                        writelisview("模型", "折铁完成！", "log");
                        DateTime afterDT = System.DateTime.Now;
                        TimeSpan ts = afterDT.Subtract(beforDT);
                        string state = "8-折铁完成";
                        sqltext = string.Format("UPDATE RealTime SET stoptime= '{0}',fall_state='{1}' ,state_end = '{2}' WHERE startime= '{3}'", afterDT, ZT_program_flag.ToString(),state ,beforDT);
                        dbhlper.MultithreadExecuteNonQuery(sqltext);
                        ZT_thread_flag = 1000;
                        ZT_program_flag = 0;
                        MdiParent.PLCdata1.write_mission_signal(one_ZT.Has_mission);
                        break;
                    }
                    //极限角度回罐
                    if (ZT_program_flag == 9)
                    {                        
                        if (one_ZT.GB_speed == -20)
                        {
                            ZT_program_flag = 9;
                            //罐内无铁时的数据记录
                            if(MdiParent.tl1.Get_iron==false)
                            {
                                writelisview("模型", "铁流消失！", "log");
                                LogHelper.loginfo.Info("9铁流消失，进入12！");
                                DateTime afterDT = System.DateTime.Now;
                                TimeSpan tl_ts_stop = afterDT.Subtract(beforDT);
                                minute = (int)Math.Round(tl_ts_stop.TotalMinutes, 2);
                                second = ((double)Math.Round(tl_ts_stop.TotalMinutes, 2) - minute) * 0.6;
                                sqltext = string.Format("UPDATE RealTime SET fall_time='{0}',fall_weight='{1}',fall_state='{2}',tb_hight='{3}',stoptime= '{4}' WHERE startime= '{5}'", Math.Round(minute + second, 2).ToString(), one_ZT.TB_weight.ToString(), ztstate, (5404 - one_ZT.TB_hight).ToString(), afterDT, beforDT);
                                dbhlper.MultithreadExecuteNonQuery(sqltext);
                                ZT_program_flag = 12;
                            }
                        }
                        else
                        {
                            ZT_program_flag = 9;
                            MdiParent.PLCdata1.set_speed(one_ZT.GB_station, -20);
                        }
                    }
                    if (ZT_program_flag == 10)
                    {
                        if (one_ZT.GB_speed == -20)
                            ZT_program_flag = 10;
                        else
                            MdiParent.PLCdata1.set_speed(one_ZT.GB_station, -20);

                        if (MdiParent.tl1.Get_iron == false)
                        {
                            ZT_program_flag = 11;
                            writelisview("模型", "折铁返回中铁流消失！", "log");
                            //本次折铁铁流消失
                            DateTime afterDT = System.DateTime.Now;
                            TimeSpan tl_ts_stop = afterDT.Subtract(beforDT);
                            minute = (int)Math.Round(tl_ts_stop.TotalMinutes, 2);
                            second = ((double)Math.Round(tl_ts_stop.TotalMinutes, 2) - minute) * 0.6;
                            ztstate = "10-烟气过大停止折铁！";
                            //string state = "11-折铁返回中铁流消失";
                            sqltext = string.Format("UPDATE RealTime SET fall_time='{0}',fall_weight='{1}',tb_hight='{2}',fall_state='{3}' WHERE startime= '{4}'", Math.Round(minute + second, 2).ToString(), one_ZT.TB_weight.ToString(),(5404 - one_ZT.TB_hight).ToString(),ztstate, beforDT);
                            dbhlper.MultithreadExecuteNonQuery(sqltext);
                            no_iron_angle = one_ZT.GB_angle;
                        }
                    }
                    if (ZT_program_flag == 11)
                    {
                        if (no_iron_angle - one_ZT.GB_angle > 200)
                        {
                            MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 0);
                            ZT_thread_flag = 1000;
                        }
                        else
                        {
                            if (one_ZT.GB_speed == -20)
                                ZT_program_flag = 11;
                            else
                                MdiParent.PLCdata1.set_speed(one_ZT.GB_station, -20);
                        }
                    }
                    //极限角度下 回罐时铁流消失之后 
                    if(ZT_program_flag==12)
                    {
                        if (one_ZT.GB_0_limt == true && one_ZT.GB_angle > -300)
                        {
                            if (one_ZT.GB_speed == -20)
                                ZT_program_flag = 12;
                            else
                                MdiParent.PLCdata1.set_speed(one_ZT.GB_station, -20);
                        }
                        else
                        {
                            MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 0);

                            if (one_ZT.GB_station == "A")
                                A_live = 0;
                            if (one_ZT.GB_station == "B")
                                B_live = 0;

                            if (A_live == 0 && B_live == 0)
                                MdiParent.PLCdata1.reset_start(one_ZT.GB_station);
                            ZT_thread_flag = 1000;
                            Program.GB_chose_flag = 0;
                            one_ZT.Has_mission = false;
                            MdiParent.process_TL.ContinuousRunEnable = false;
                            DateTime afterDT = System.DateTime.Now;
                            TimeSpan ts = afterDT.Subtract(beforDT);
                            minute = (int)Math.Round(ts.TotalMinutes, 2);
                            second = ((double)Math.Round(ts.TotalMinutes, 2) - minute) * 0.6;
                            sqltext = string.Format("UPDATE RealTime SET stoptime= '{0}' WHERE startime= '{1}'", afterDT, beforDT);
                            dbhlper.MultithreadExecuteNonQuery(sqltext);
                            writelisview("模型", "折铁完成！", "log");
                            writelisview("模型", "折铁完成！", "err");
                            LogHelper.loginfo.Info("折铁完成，结束！");
                            MdiParent.PLCdata1.write_mission_signal(one_ZT.Has_mission);
                            ZT_program_flag = 0;
                            break;
                        }
                    }
                   

                    //检测罐角度，当超过设定或极限角度时应返回
                    if (limt_has_set == 0)
                    {
                        if ((5404 - one_ZT.TB_hight) < 580 && limt_has_set == 0)
                        {
                            if (ZT_program_flag != 9)
                            {
                                ztstate = "9-净空到达极限";
                                limt_has_set = 1;
                                ZT_program_flag = 9;
                                writelisview("模型", "净空到达极限", "err");
                            }
                        }
                        else if (one_ZT.GB_angle > 2300 && limt_has_set == 0)
                        {
                            if (ZT_program_flag != 9)
                            {
                                ztstate = "9-到达极限角度";
                                limt_has_set = 1;
                                ZT_program_flag = 9;
                                writelisview("模型", "到达极限角度", "err");
                            }
                        }
                        else if (one_ZT.GB_120_limt == false && limt_has_set == 0)
                        {
                            if (ZT_program_flag != 9)
                            {
                                ztstate = "9-120度限位检测异常";
                                limt_has_set = 1;
                                ZT_program_flag = 9;
                                writelisview("模型", "120度限位检测异常", "err");
                            }
                        }
                        else if (MdiParent.PLCdata1.ZT_data.TB_pos == true && limt_has_set == 0)
                        {
                            if (ZT_program_flag != 9)
                            {
                                ztstate = "9-铁包不在位";
                                limt_has_set = 1;
                                ZT_program_flag = 9;
                                writelisview("模型", "铁包不在位", "err");
                            }
                        }
                        else if ((one_ZT.TB_weight >= one_ZT.ZT_reqweight - 0.7) && limt_has_set == 0)
                        {
                            if (ZT_program_flag != 9)
                            {
                                ztstate = "9-到达设定重量极限";
                                limt_has_set = 1;
                                ZT_program_flag = 9;
                                writelisview("模型", "到达设定重量极限", "err");
                            }
                        }
                        //else if (((one_ZT.GB_station == "A" && MdiParent.PLCdata1.ZT_data.ZT_PLC_hum_A == false) || (one_ZT.GB_station == "B" && MdiParent.PLCdata1.ZT_data.ZT_PLC_hum_B == false)) && limt_has_set == 0)//||MdiParent.PLCdata1.GB_A_remind_weight<300|| one_ZT.TB_weight >= one_ZT.ZT_reqweight + 15
                        //{
                        //    if (ZT_program_flag != 6)
                        //    {
                        //        ztstate = "6-人工强制退出";
                        //        limt_has_set = 1;
                        //        ZT_program_flag = 6;
                        //        writelisview("模型", "人工强制退出", "err");
                        //    }
                        //}
                    }                  
                    //烟气过大时限速
                    if (MdiParent.tl1.TL_smok == true && ZT_program_flag == 4)
                    {
                        writelisview("模型", "烟气过大降速倾倒！", "log");
                        MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 0);
                        Thread.Sleep(500);
                    }

                    ////折铁失败人工处理
                    //if (one_ZT.GB_station == "A")
                    //{
                    //    if (MdiParent.PLCdata1.ZT_data.ZT_PLC_hum_A == false)
                    //    {
                    //        ZT_thread_flag = 1000;
                    //    }
                    //}
                    //if (one_ZT.GB_station == "B")
                    //{
                    //    if (MdiParent.PLCdata1.ZT_data.ZT_PLC_hum_B == false)
                    //    {
                    //        ZT_thread_flag = 1000;
                    //    }
                    //}
                    if (ZT_thread_flag == 1000)
                    {
                        MdiParent.PLCdata1.set_speed(one_ZT.GB_station, 0);
                        writelisview("模型", "人工强制退出！！", "log");
                        DateTime afterDT = System.DateTime.Now;
                        TimeSpan ts = afterDT.Subtract(beforDT);
                        if (ztstate == "")
                            ztstate = "1000-人工强制终止";
                        sqltext = string.Format("UPDATE RealTime SET stoptime= '{0}',fall_state='{1}'  WHERE startime= '{2}'", afterDT, ztstate, beforDT);
                        dbhlper.MultithreadExecuteNonQuery(sqltext);
                        one_ZT.Has_mission = false;
                        MdiParent.process_GK.ContinuousRunEnable = false;
                        MdiParent.process_TL.ContinuousRunEnable = false;
                        ZT_thread_flag = 1000;
                        threadflag = false;
                        MdiParent.PLCdata1.write_mission_signal(one_ZT.Has_mission);
                        ZT_program_flag = 0;
                        break;
                    }
                }
                if (one_ZT.GB_station == "A")
                {
                    A_live = 0;
                }
                if (one_ZT.GB_station == "B")
                {
                    B_live = 0;
                }
                #endregion
            }
            catch (Exception e)
            {
                MdiParent.PLCdata1.reset_hum_signal();
                one_ZT.Has_mission = false;
                MdiParent.PLCdata1.write_mission_signal(one_ZT.Has_mission);
                MdiParent.process_GK.ContinuousRunEnable = false;
                MdiParent.process_TL.ContinuousRunEnable = false;
                writelisview("模型", "折铁模型发生程序失败！！", "log");
                ZT_thread_flag = 1000;
                ZT_program_flag = 0;
                threadflag = false;
                LogHelper.WriteLog("折铁线程", e);
            }
        }
    }

    }

