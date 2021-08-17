﻿using System;
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
    public class Auto_model
    {
        dbTaskHelper dbhlper = new dbTaskHelper();
        manage_steel m1 = new manage_steel();
        public updatelistiew writelisview;
        public bool TB_initresult = false;
        public bool TB_onpos = false;
        public float TB_need_wight = 0;

        public bool GB_initresult = false;
        public bool GB_onpos = false;
        public string GB_capacity = "";
        public string GB_station = "";
        public float GB_have_wight = 0;

        public bool GB_getpow = false;
        public bool GB_zlimt = false;
        public bool GB_flimt = false;
        Thread full_thread, nfull_thread;
        public bool threadflag=false;
        
        Single limt_angle = 0;

        public static List<Single> edge_value = new List<Single>();
       // public static Single last_angle = 0;
        public Auto_model()
        {
            full_thread = new Thread(process_fmodel_full);
            nfull_thread = new Thread(process_model_half);
        }
        public void chose_process()
        {
            try
            {
                

                if (m1.get_TB_data() && m1.get_GB_data())
                {
                    TB_onpos = true;
                    GB_onpos = true;
                    if (m1.F_flag == "F"&& m1.fish_station != "ER")
                    {
                        GB_capacity = "F";
                        GB_station = m1.fish_station;
                        if (MdiParent.tb1.TB_init_result())
                        {
                            writelisview("铁包口", "铁包口视觉检测正常!");
                            TB_initresult = true;
                            TB_need_wight = m1.need_ibag_weight;
                            Program.model_flag = 1;

                            if (MdiParent.gk1.GK_init_result())
                            {
                                writelisview("罐包口", "罐包口视觉检测正常！");
                                GB_initresult = true;
                                GB_have_wight = m1.fish_weight;
                                Program.model_flag = 2;
                                threadflag = true;
                                full_thread.Start();
                            }
                            else
                            {
                                writelisview("罐包口", "罐包口视觉检测异常！");
                                GB_initresult = false;
                                GB_have_wight = m1.fish_weight;
                                Program.model_flag = 1000;
                                threadflag = false;
                            }

                        }
                        else
                        {
                            writelisview("铁包口", "铁包口视觉检测正常!");
                            TB_initresult = false;
                            TB_need_wight = m1.need_ibag_weight;
                            GB_initresult = false;
                            GB_have_wight = m1.fish_weight;
                            Program.model_flag = 1000;
                            threadflag = false;
                        }

                    }
                    else if (m1.F_flag == "NF" && m1.fish_station != "error")
                    {
                        GB_capacity = "NF";
                        GB_station = m1.fish_station;
                        if (MdiParent.tb1.TB_init_result())
                        {
                            writelisview("铁包口", "铁包口视觉检测正常!");
                            TB_initresult = true;
                            TB_need_wight = m1.need_ibag_weight;
                            Program.model_flag = 1;

                            writelisview("罐包口", "罐包口视觉检测正常！");
                            GB_initresult = true;
                            GB_have_wight = m1.fish_weight;
                            Program.model_flag = 2;
                            threadflag = true;
                            nfull_thread.Start();
                        }
                        else
                        {
                            writelisview("铁包口", "铁包口视觉检测正常!");
                            GB_initresult = false;
                            GB_have_wight = m1.fish_weight;
                            Program.model_flag = 1000;
                            threadflag = false;
                        }
                    }
                    else
                    {
                        writelisview("折铁模型", "折铁初始化条件错误!");
                    }
                }
                
            }
            catch(Exception e)
            {
                writelisview("折铁模型", "折铁初始化条件错误!");
                Program.program_flag = 1000;
                LogHelper.WriteLog("选择核心模型程序出错！",e);
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
            catch(Exception e)
            {
                LogHelper.WriteLog("计算极限倾角",e);
                return false;
            }
            
        }
        public void calc_speed()
        {

        }
        public void process_fmodel_full()
        {
            Single last_agle = 0;
            Stopwatch sw = new Stopwatch();
            //240吨前要小于1.6t/s  240-260小于1t/s  260-280小于0.25t/s

            //耗时巨大的代码  
            sw.Stop();
            TimeSpan ts2 = sw.Elapsed;
            Console.WriteLine("Stopwatch总共花费{0}ms.", ts2.TotalMilliseconds);
            try
            {
                writelisview("模型", "模型开始运行！");
                sw.Start();
                while (threadflag)
                {
                    Thread.Sleep(5000);
                    writelisview("模型", "启动模型！");
                    //////模型计算开流极限角度
                    if(PLCdata.F_angle>limt_angle && MdiParent.tl1.Get_iron==false)
                    {
                        writelisview("模型", "超过罐车模型极限角度未开流！！" );
                        Program.model_flag = 1000;
                        threadflag = false;
                        break;
                    }
                    //////模型计算给定速度  现在是直接给定
                    if (Program.model_flag==0)
                    {
                        Int16 speed_flag = PLCdata.set_speed(20);
                        if (speed_flag != 0)
                        {
                            writelisview("模型", "速度设置错误，错误代码：" + speed_flag.ToString());
                            Program.model_flag = 1000;
                            threadflag = false;
                            break;
                        }
                    }
                    ///开启铁流检测
                    if(Program.model_flag<3)
                    {
                        if (MdiParent.gk1.set_GK_contiu() == false)
                        {
                            writelisview("模型", "启动视觉连续检程序失败！");
                            Program.model_flag = 1000;
                            threadflag = false;
                            break;
                        }
                        else
                        {
                            Program.model_flag = 3;//连接检测炉口阶段
                            writelisview("模型", "启动视觉连续检测罐口边缘！");
                        }
                    }
                    if(Program.model_flag == 3)
                    {
                        if(PLCdata.F_angle-last_agle>1)
                        {
                            last_agle = PLCdata.F_angle;
                            if (MdiParent.gk1.GK_light_result())
                            {
                                edge_value.Add(MdiParent.gk1.Fall_edga_light);
                                dbhlper.inser_log("Model_data", last_agle.ToString());
                            }
                            else
                            {
                                writelisview("模型", "视觉连续检测罐口边缘失败！");
                                Program.model_flag = 1000;
                                threadflag = false;
                                break;
                            }
                            if(edge_value.Count>=5)
                            {
                                if(edge_value[2]-edge_value[0]>0 && edge_value[5] - edge_value[3]>0)
                                    Program.model_flag = 4;
                            }
                        }
                    }
                    if (Program.model_flag == 4&& MdiParent.tl1.TL_light_result()==true)
                    {
                        if (PLCdata.TB_weight < 240 )
                        {
                            if(PLCdata.TB_weight_speed < 1.6)
                            {
                                if (PLCdata.speed == 20)
                                    Program.model_flag = 4;
                                else
                                    PLCdata.set_speed(20);
                            }
                            else
                            {
                                if (PLCdata.speed == 0)
                                    Program.model_flag = 4;
                                else
                                    PLCdata.set_speed(0);
                            }
                            
                        }
                        else if(PLCdata.TB_weight > 240 && PLCdata.TB_weight < 260)
                        {
                            if (PLCdata.TB_weight_speed < 1)
                            {
                                if (PLCdata.speed == 10)
                                    Program.model_flag = 4;
                                else
                                    PLCdata.set_speed(10);
                            }
                            else
                            {
                                if (PLCdata.speed == 0)
                                    Program.model_flag = 4;
                                else
                                    PLCdata.set_speed(0);
                            }
                        }
                        else
                        {
                            //返回点
                            Program.model_flag = 5;
                        }
                    }
                    if (Program.model_flag == 5 && MdiParent.tl1.TL_light_result() == true)
                    {
                        if (PLCdata.TB_weight > 260 && PLCdata.TB_weight < 270)
                        {
                            if (PLCdata.TB_weight_speed < 0.5)
                            {
                                if (PLCdata.speed == -20)
                                    Program.model_flag = 5;
                                else
                                    PLCdata.set_speed(-20);
                            }
                            else
                            {
                                if (PLCdata.speed == 0)
                                    Program.model_flag = 5;
                                else
                                    PLCdata.set_speed(-10);
                            }
                        }
                        else if (PLCdata.TB_weight > 270 && PLCdata.TB_weight < 280)
                        {
                            if (PLCdata.TB_weight_speed < 0.5)
                            {
                                if (PLCdata.speed == -20)
                                    Program.model_flag = 5;
                                else
                                    PLCdata.set_speed(-20);
                            }
                            else
                            {
                                if (PLCdata.speed == 0)
                                    Program.model_flag = 5;
                                else
                                    PLCdata.set_speed(-10);
                            }
                        }
                        else
                        {

                            Program.model_flag = 6;
                        }
                    }
                    if (Program.model_flag == 6 && MdiParent.tl1.TL_light_result() == true)
                    {
                        if (PLCdata.speed == -20)
                            Program.model_flag = 6;
                        else
                            PLCdata.set_speed(-20);
                    }
                    else if (MdiParent.tl1.TL_light_result() == false)
                    {
                        writelisview("模型", "折铁顺利完成！");
                        break;
                    }
                    else
                    {
                        writelisview("模型", "折铁失败！！");
                        Program.model_flag = 1000;
                        threadflag = false;
                        break;
                    }
                }
            }
            catch(Exception e)
            {
                writelisview("模型", "折铁模型发生程序失败！！");
                Program.model_flag = 1000;
                threadflag = false;
            }
            
        }
        public void process_model_half()
        {
            while (threadflag)
            {
                Thread.Sleep(5000);
                writelisview("折铁中....", "正在折铁");
            }
        }
    }
}
