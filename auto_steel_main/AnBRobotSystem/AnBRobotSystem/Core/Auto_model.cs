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

        public bool GB_GK_vision;//罐包到位，可以用插电来获取
        public bool GB_on_pos;//罐包到位，可以用插电来获取
        public string GB_station;//罐包位置
        public bool GB_connect;//罐车得电
        public bool GB_0_limt;//罐车0限位
        public bool GB_120_limt;//罐车120限位
        public Int16 GB_num;//罐车包号
        public Single GB_angle;//罐包角度
        public Single GB_have_wight;
        public Single GB_full_wight;//罐包角度
        public string GB_capacity;//罐车容量
        public DateTime GB_train_in_times;//罐车到来时间
        public bool Has_mission;
        public float GB_speed;
    }

    public class Auto_model
    {
        dbTaskHelper dbhlper = new dbTaskHelper();
        manage_steel m1 = new manage_steel();

        public updatelistiew writelisview;
        Thread full_thread, nfull_thread;
        public bool threadflag = false;
        public ZT_Date one_ZT = new ZT_Date();
        Single limt_angle = 0;
        public static List<float> edge_value = new List<float>(5);
        public Auto_model()
        {
            m1.writelisview = MdiParent.form.mainlog;
        }

        public void init_ZT_data()
        {


        }
        public void chose_process()
        {
            try
            {
                one_ZT.Has_mission = true;
                if (Program.GB_chose_flag == 1)
                {

                    Program.model_flag = 1;
                    Program.GB_chose_flag = 0;
                    //one_ZT.Has_mission = true;
                    if (m1.get_TB_data() && m1.get_GB_data_manual(Program.GB_station))
                    {
                        one_ZT.TB_num = PLCdata.ZT_data.TB_num;
                        one_ZT.TB_need_weight = m1.need_ibag_weight;
                        one_ZT.TB_on_pos = PLCdata.ZT_data.TB_pos;
                        one_ZT.TB_hight = PLCdata.ZT_data.TB_hight;

                        if (m1.F_flag == "F" && m1.fish_station != "ER")
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
                                if (null != MdiParent.GK_global)
                                {
                                    MdiParent.GK_global.SetGlobalVar("GK_pos", m1.fish_station);
                                    //string strVal = globalVar.GetGlobalVar("var0"); // 获取全局变量
                                    if (MdiParent.gk1.GK_init_result(m1.fish_station))
                                    {
                                        writelisview("罐包口", "罐包口视觉检测正常！", "log");
                                        one_ZT.GB_GK_vision = true;
                                        one_ZT.GB_have_wight = m1.fish_weight;
                                        threadflag = true;
                                        full_thread = new Thread(process_fmodel_full);
                                        full_thread.Start();
                                        //Thread.Sleep(50000);
                                    }
                                    else
                                    {
                                        one_ZT.Has_mission = false;
                                        writelisview("罐包口", "罐包口视觉检测异常！", "log");
                                        one_ZT.GB_GK_vision = false;
                                        Program.model_flag = 1000;
                                        threadflag = false;
                                    }
                                }
                                else
                                {
                                    one_ZT.Has_mission = false;
                                    writelisview("罐口相机", "罐口相机全局变量选择失败！", "log");
                                    one_ZT.GB_GK_vision = false;
                                    Program.model_flag = 1000;
                                    threadflag = false;
                                }



                            }
                            else
                            {
                                one_ZT.Has_mission = false;
                                writelisview("铁包口", "铁包口视觉检测异常!", "log");
                                one_ZT.TB_BK_vision = false;
                                Program.model_flag = 1000;
                                threadflag = false;
                            }

                        }
                        else if (m1.F_flag == "NF" && m1.fish_station != "error")
                        {
                            one_ZT.GB_capacity = "NF";
                            one_ZT.GB_station = m1.fish_station;
                            if (MdiParent.tb1.TB_init_result())
                            {
                                writelisview("铁包口", "铁包口视觉检测正常!", "log");
                                one_ZT.TB_BK_vision = true;
                                writelisview("罐包口", "二次倾倒，罐口正常！", "log");
                                one_ZT.GB_GK_vision = true;
                                one_ZT.GB_have_wight = m1.fish_weight;
                                threadflag = true;
                                nfull_thread.Start();
                            }
                            else
                            {
                                one_ZT.Has_mission = false;
                                writelisview("铁包口", "铁包口视觉检测失败!", "log");
                                one_ZT.TB_BK_vision = false;
                                Program.model_flag = 1000;
                                threadflag = false;
                            }
                        }
                        else
                        {
                            one_ZT.Has_mission = false;
                            writelisview("折铁模型", "折铁初始化条件错误!", "log");
                            Program.model_flag = 1000;
                            threadflag = false;
                        }
                    }
                    else
                    {
                        one_ZT.Has_mission = false;
                        writelisview("罐包管理", "罐包管理系统出差!", "log");
                        Program.model_flag = 1000;
                        threadflag = false;
                    }
                }
                else if (Program.GB_chose_flag == 2)
                {
                    Program.model_flag = 1;
                    Program.GB_chose_flag = 0;
                    if (m1.get_TB_data() && m1.get_GB_data())
                    {
                        one_ZT.TB_num = PLCdata.ZT_data.TB_num;
                        one_ZT.TB_need_weight = m1.need_ibag_weight;
                        one_ZT.TB_on_pos = PLCdata.ZT_data.TB_pos;


                        if (m1.F_flag == "F" && m1.fish_station != "ER")
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
                                if (null != MdiParent.GK_global)
                                {
                                    MdiParent.GK_global.SetGlobalVar("GK_pos", one_ZT.GB_station);
                                    //string strVal = globalVar.GetGlobalVar("var0"); // 获取全局变量
                                    if (MdiParent.gk1.GK_init_result(one_ZT.GB_station))
                                    {
                                        writelisview("罐包口", "罐包口视觉检测正常！", "log");
                                        one_ZT.GB_GK_vision = true;
                                        one_ZT.GB_have_wight = m1.fish_weight;
                                        threadflag = true;
                                        GC.Collect();
                                        GC.SuppressFinalize(this);
                                        full_thread = new Thread(process_fmodel_full);
                                        full_thread.Start();
                                        //Thread.Sleep(50000);
                                    }
                                    else
                                    {
                                        one_ZT.Has_mission = false;
                                        writelisview("罐包口", "罐包口视觉检测异常！", "log");
                                        one_ZT.GB_GK_vision = false;
                                        Program.model_flag = 1000;
                                        threadflag = false;
                                    }
                                }
                                else
                                {
                                    one_ZT.Has_mission = false;
                                    writelisview("罐口相机", "罐口相机全局变量选择失败！", "log");
                                    one_ZT.GB_GK_vision = false;
                                    Program.model_flag = 1000;
                                    threadflag = false;
                                }
                            }
                            else
                            {
                                one_ZT.Has_mission = false;
                                writelisview("铁包口", "铁包口视觉检测异常!", "log");
                                one_ZT.TB_BK_vision = false;
                                Program.model_flag = 1000;
                                threadflag = false;
                            }
                        }
                        else if (m1.F_flag == "NF" && m1.fish_station != "error")
                        {
                            one_ZT.GB_capacity = "NF";
                            one_ZT.GB_station = m1.fish_station;
                            if (MdiParent.tb1.TB_init_result())
                            {
                                writelisview("铁包口", "铁包口视觉检测正常!", "log");
                                one_ZT.TB_BK_vision = true;
                                writelisview("罐包口", "二次倾倒，罐口正常！", "log");
                                one_ZT.GB_GK_vision = true;
                                one_ZT.GB_have_wight = m1.fish_weight;
                                threadflag = true;
                                nfull_thread.Start();
                            }
                            else
                            {
                                one_ZT.Has_mission = false;
                                writelisview("铁包口", "铁包口视觉检测失败!", "log");
                                one_ZT.TB_BK_vision = false;
                                Program.model_flag = 1000;
                                threadflag = false;
                            }
                        }
                        else
                        {
                            one_ZT.Has_mission = false;
                            writelisview("折铁模型", "折铁初始化条件错误!", "log");
                            Program.model_flag = 1000;
                            threadflag = false;
                        }
                    }
                    else
                    {
                        one_ZT.Has_mission = false;
                        writelisview("罐包管理", "罐包管理系统出差!", "log");
                        Program.model_flag = 1000;
                        threadflag = false;
                    }
                }
                else
                {
                    Program.GB_chose_flag = 3;
                }

            }
            catch (Exception e)
            {
                Thread.Sleep(1000);
                //writelisview("折铁模型", "选择核心模型程序出错!", "err");
                Program.model_flag = 1000;
                LogHelper.WriteLog("选择核心模型程序出错！", e);
            }

        }
        public bool calc_fangle()
        {

            try
            {
                //模型计算获取开流倾角
                limt_angle = 20;
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
        int b = 0;
        public void setvalue(string flag)
        {
            one_ZT.TB_hight = PLCdata.ZT_data.TB_hight;
            one_ZT.TB_weight = PLCdata.ZT_data.TB_weight;
            if (flag == "A")
            {
                one_ZT.GB_angle = PLCdata.ZT_data.GB_A_angle;
                one_ZT.GB_num = PLCdata.ZT_data.GB_A_num;
            }
            else
            {
                one_ZT.GB_angle = PLCdata.ZT_data.GB_B_angle;
                one_ZT.GB_num = PLCdata.ZT_data.GB_B_num;
            }


        }
        public void process_fmodel_full()
        {
            int last_flag=0;
            setvalue(one_ZT.GB_station);
            //240吨前要小于1.6t/s  240-260小于1t/s  260-280小于0.25t/s
            //耗时巨大的代码  
            try
            {///[ID],[fishstation],[carnum],[is_full],[train_in_times],[lot],[icode],[icard],[startime],[stoptime],[Tare_Weight],[f_full_weight],[f_has_weight],[i_need_weight],[tempture],[iron_dirction]
                Single last_agle = 0;
                DateTime beforDT = DateTime.Now;
                string sqltext = string.Format("insert into  [AutoSteel].[dbo].[RealTime] VALUES ('{0}','{1}','{2}','{3}','','','','{4}','','','{5}','{6}','{7}','','')", one_ZT.GB_station, one_ZT.GB_num, one_ZT.GB_capacity, one_ZT.GB_train_in_times, beforDT, one_ZT.GB_full_wight.ToString(), one_ZT.GB_have_wight.ToString(), one_ZT.TB_need_weight.ToString());
                dbhlper.MultithreadExecuteNonQuery(sqltext);

                //  int a= ref MdiParent.form.test;
                writelisview("模型", "启动模型！", "log");

                #region
                while (threadflag)
                {
                    setvalue(one_ZT.GB_station);
                    //////模型计算给定速度  现在是直接给定
                    if (Program.program_flag == 0)
                    {
                        Int16 speed_flag = PLCdata.set_speed(one_ZT.GB_station, 20);
                        if (speed_flag != 0)
                        {
                            writelisview("模型", "速度设置错误，错误代码：" + speed_flag.ToString(), "log");
                            Program.model_flag = 1000;
                            //threadflag = false;
                            //  break;
                        }
                        else
                        {
                            Program.program_flag = 1;
                        }
                    }

                    ///选择并 启动连续边缘检测
                    if (Program.program_flag == 1)
                    {
                        if (null != MdiParent.GK_global)
                        {
                            MdiParent.GK_global.SetGlobalVar("GK_pos", one_ZT.GB_station);

                            if (MdiParent.gk1.set_GK_contiu() == false)
                            {

                                writelisview("模型", "启动罐口连续边缘检测出错", "log");
                                Program.model_flag = 1000;
                                // threadflag = false;
                                //  break;
                            }
                            else
                            {
                                Program.program_flag = 2;//连接检测炉口阶段
                                writelisview("模型", "启动罐口连续边缘检测！", "log");
                            }
                        }
                        else
                        {
                            writelisview("模型", "选择边缘检测相机出错！", "log");
                            Program.model_flag = 1000;
                        }
                    }
                    ///获取罐口边缘检测结果 计算炉口边缘 亮度
                    if (Program.program_flag == 2)
                    {
                        if (one_ZT.GB_angle - last_agle > 100)
                        {
                            last_agle = one_ZT.GB_angle;
                            if (MdiParent.gk1.GK_light_result(one_ZT.GB_station))
                            {
                                edge_value.Add(MdiParent.gk1.Fall_edga_light);
                                dbhlper.inser_log("Model_data", last_agle.ToString());
                            }
                            else
                            {
                                writelisview("模型", "视觉连续检测罐口边缘失败！", "log");
                                MdiParent.process_GK.ContinousRunEnable = false;
                                Program.model_flag = 1000;
                                // threadflag = false;
                                // break;
                            }
                            if (edge_value.Count >= 5)
                            {
                                if (edge_value[2] - edge_value[0] > 0 && edge_value[5] - edge_value[3] > 0)
                                {
                                    MdiParent.process_TL.ContinousRunEnable = true;
                                    MdiParent.process_GK.ContinousRunEnable = false;
                                    Program.program_flag = 3;
                                    
                                }
                            }
                            else
                            {
                                writelisview("模型", "连续边缘检测结果异常！", "log");
                                MdiParent.process_GK.ContinousRunEnable = false;
                                Program.model_flag = 1000;
                            }

                        }
                    }
                    //模型计算开流极限角度  开启铁流检测
                    if (Program.program_flag == 3 && one_ZT.GB_angle < limt_angle)
                    {
                        if (MdiParent.tl1.TL_light_result() == false)
                        {
                            writelisview("模型", "铁流检测程序运行错误！！", "log");
                            Program.model_flag = 1000;
                        }
                        else
                        {
                            if(MdiParent.tl1.Get_iron==true)
                            {
                                Program.program_flag = 4;
                                writelisview("模型", "开流成功！", "log");
                            }                            
                        }
                    }
                    
                    //正向全速倾倒
                    if (Program.program_flag == 4 && MdiParent.tl1.TL_light_result() == true)
                    {
                        if(MdiParent.tl1.Get_iron==true)
                        {
                            if (one_ZT.TB_weight < 240)
                            {
                                if (PLCdata.TB_weight_speed < 1.6)
                                {
                                    if (one_ZT.GB_speed == 20)
                                        Program.program_flag = 4;
                                    else
                                        PLCdata.set_speed(one_ZT.GB_station,20);
                                }
                                else
                                {
                                    if (one_ZT.GB_speed == 0)
                                        Program.program_flag = 4;
                                    else
                                        PLCdata.set_speed(one_ZT.GB_station, 0);
                                }

                            }
                            else if (PLCdata.TB_weight > 240 && PLCdata.TB_weight < 260)
                            {
                                if (PLCdata.TB_weight_speed < 1)
                                {
                                    if (one_ZT.GB_speed == 10)
                                        Program.program_flag = 4;
                                    else
                                        PLCdata.set_speed(one_ZT.GB_station,10);
                                }
                                else
                                {
                                    if (one_ZT.GB_speed == 0)
                                        Program.program_flag = 4;
                                    else
                                        PLCdata.set_speed(one_ZT.GB_station, 0);
                                }
                            }
                            else
                            {
                                //返回点
                                writelisview("模型", "启动返回程序！", "log");
                                Program.program_flag = 5;
                            }
                        }
                       
                    }
                    if (Program.program_flag == 5 && MdiParent.tl1.TL_light_result() == true)
                    {
                        if (PLCdata.TB_weight > 260 && PLCdata.TB_weight < 270)
                        {
                            if (PLCdata.TB_weight_speed < 0.5)
                            {
                                if (one_ZT.GB_speed == -20)
                                    Program.program_flag = 5;
                                else
                                    PLCdata.set_speed(one_ZT.GB_station, -20);
                            }
                            else
                            {
                                if (one_ZT.GB_speed == 0)
                                    Program.program_flag = 5;
                                else
                                    PLCdata.set_speed(one_ZT.GB_station, 0);
                            }
                            
                        }
                        else if (PLCdata.TB_weight > 270 && PLCdata.TB_weight < 280)
                        {
                            if (PLCdata.TB_weight_speed < 0.5)
                            {
                                if (one_ZT.GB_speed == -10)
                                    Program.program_flag = 5;
                                else
                                    PLCdata.set_speed(one_ZT.GB_station, -10);
                            }
                            else
                            {
                                if (one_ZT.GB_speed == 0)
                                    Program.program_flag = 5;
                                else
                                    PLCdata.set_speed(one_ZT.GB_station, 0);
                            }
                        }
                        else
                        {

                            Program.program_flag = 6;
                        }
                    }
                    if (Program.program_flag == 6 && MdiParent.tl1.TL_light_result() == true)
                    {
                        if (PLCdata.speed == -20)
                            Program.program_flag = 6;
                        else
                            PLCdata.set_speed(one_ZT.GB_station, 0);
                    }
                    else if (Program.program_flag == 6 && MdiParent.tl1.TL_light_result() == false)
                    {
                        Program.model_flag = 1000;
                        Program.GB_chose_flag = 0;
                        one_ZT.Has_mission = false;
                        MdiParent.process_TL.ContinousRunEnable = false;
                        writelisview("模型", "折铁完成！", "log");
                        DateTime afterDT = System.DateTime.Now;
                        TimeSpan ts = afterDT.Subtract(beforDT);
                        break;
                    }

                    if (Program.program_flag==last_flag)
                    {
                        DateTime afterDT = System.DateTime.Now;
                        TimeSpan ts = afterDT.Subtract(beforDT);
                    }
                    else
                    {
                        last_flag = Program.program_flag;
                    }
                    if (Program.model_flag == 1000)
                    {
                        Thread.Sleep(3000);
                        writelisview("模型", "折铁失败！！", "log");
                        one_ZT.Has_mission = false;
                        MdiParent.process_GK.ContinousRunEnable = false;
                        MdiParent.process_TL.ContinousRunEnable = false;
                        
                        break;
                    }
                    
                }
                #endregion
            }
            catch (Exception e)
            {
                one_ZT.Has_mission = false;
                MdiParent.process_GK.ContinousRunEnable = false;
                MdiParent.process_TL.ContinousRunEnable = false;
                writelisview("模型", "折铁模型发生程序失败！！", "log");
                Program.model_flag = 1000;
                threadflag = false;
            }

        }
        public void process_model_half()
        {
            while (threadflag)
            {
                Thread.Sleep(5000);
                writelisview("折铁中....", "正在折铁", "log");
            }
        }
        ~Auto_model()
        {
            //this.Dispose();
            if (full_thread != null)
            {

                full_thread.Abort();
                GC.Collect();
                GC.SuppressFinalize(this);
                //this.Close();
                // full_thread.ThreadState;
            }


        }

    }
}
