using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using S7.Net;
using logtest;
using System.Diagnostics;

namespace record_data
{
   public class ZT_record
    {
        Plc ZT_plc = new Plc(CpuType.S7300, "140.80.0.80", 0, 2);
        public static bool back_high_speed;
        public static bool back_low_speed;
        public static bool fall_high_speed;
        public static bool fall_low_speed;
        public static bool TB_onpos=true;//铁包到位信号
        public static float weight=0;//铁包时时重量
        public static float lastweight=0;
        public static float realangle = 0;//罐车倾角
        public static float iron_hight = 0;//铁水包液面高度
        public static float fish_full_weight = 0;//鱼雷罐满罐重量


        public void open_plc()
        {
            try
            {
                ZT_plc.Open();
                
            }
            catch
            {
                Console.WriteLine("PLC打开失败！");
            }

            
        }

        public void record_data()
        {
            while (true)
            {
                //Thread.Sleep(500);
                Stopwatch sw = new Stopwatch();


                sw.Start();
                back_high_speed = (bool)ZT_plc.Read("M4.3");
                
                back_low_speed = (bool)ZT_plc.Read("M4.1");
                fall_high_speed = (bool)ZT_plc.Read("M3.7");
                fall_low_speed = (bool)ZT_plc.Read("M3.5"); 
                TB_onpos=(bool)ZT_plc.Read("I11.6");
                lastweight = weight;

                //weight = ((uint)ZT_plc.Read("DB80.DBD4")).ConvertToFloat();
                //realangle = ((uint)ZT_plc.Read("DB80.DBD74")).ConvertToFloat();
                //fish_full_weight = ((uint)ZT_plc.Read("DB80.DBD78")).ConvertToFloat();
               
                var bytes = ZT_plc.ReadBytes(DataType.DataBlock, 80, 4, 4);
               
                weight = (float)S7.Net.Types.Double.FromByteArray(bytes);
                
                var bytes1 = ZT_plc.ReadBytes(DataType.DataBlock, 80, 74, 8);
                
                realangle = (float)S7.Net.Types.Double.FromByteArray(bytes1.Skip(0).Take(4).ToArray());
                fish_full_weight = (float)S7.Net.Types.Double.FromByteArray(bytes1.Skip(4).Take(4).ToArray());
                sw.Stop();
                Console.WriteLine("plc数据耗时:" + sw.ElapsedMilliseconds);

                if ((bool)back_high_speed == true)//speed=-80hz
                {
                    Console.WriteLine("记录一条数据");
                    string log = "-45" + "\",\"" + weight.ToString()+ "\",\"" + realangle.ToString() + "\",\""  + fish_full_weight.ToString();
                    LogHelper.WriteLog(log);
                }
                else if ((bool)back_low_speed == true)//speed=-40hz
                {
                    Console.WriteLine("记录一条数据");
                    string log = "-30" + "\",\"" + weight.ToString() + "\",\"" + realangle.ToString() + "\",\"" + fish_full_weight.ToString();
                    LogHelper.WriteLog(log);
                }
                else if ((bool)fall_high_speed == true)////speed=80hz
                {
                    Console.WriteLine("记录一条数据");
                    string log = "45" + "\",\"" + weight.ToString() + "\",\"" + realangle.ToString() + "\",\"" + fish_full_weight.ToString();
                    LogHelper.WriteLog(log);
                }
                else if ((bool)fall_low_speed == true)////speed=40hz
                {
                    Console.WriteLine("记录一条数据");
                    string log = "30" + "\",\"" + weight.ToString() + "\",\"" + realangle.ToString() + "\",\""  + fish_full_weight.ToString();
                    LogHelper.WriteLog(log);
                }
                else if(TB_onpos==false && weight > 70 && weight-lastweight>0.1&& realangle>0)
                {
                    Console.WriteLine("记录一条数据");
                    string log = "0" + "\",\"" + weight.ToString() + "\",\"" + realangle.ToString() + "\",\""  + fish_full_weight.ToString();
                    LogHelper.WriteLog(log);
                }
                else
                {

                }
            }

        }

    }
}

//var bytes = ZT_plc.ReadBytes(DataType.DataBlock, 5, 4, 4);
//Double realVariable = S7.Net.Types.Double.FromByteArray(bytes);