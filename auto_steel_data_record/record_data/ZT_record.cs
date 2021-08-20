using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using S7.Net;
using logtest;
namespace record_data
{
   public class ZT_record
    {
        Plc ZT_plc = new Plc(CpuType.S7300, "140.80.0.80", 0, 2);
        public static bool back_high_speed;
        public static bool back_low_speed;
        public static bool fall_high_speed;
        public static bool fall_low_speed;
        public static bool TB_onpos=true;
        public static float weight=0;
        public static float lastweight=0;

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
                Thread.Sleep(1000);
                back_high_speed = (bool)ZT_plc.Read("M4.3");
                back_low_speed = (bool)ZT_plc.Read("M4.1");
                fall_high_speed = (bool)ZT_plc.Read("M3.7");
                fall_low_speed = (bool)ZT_plc.Read("M3.5");
               // TB_onpos=(bool)ZT_plc.Read("M3.6");
                lastweight = weight;
                weight = (float)ZT_plc.Read(" DB80.DBD4");
                if ((bool)back_high_speed == false)//speed=-80hz
                {

                    string log = "-80" + "\",\"" + weight.ToString();
                    LogHelper.WriteLog(log);
                }
                else if ((bool)back_low_speed == true)//speed=-40hz
                {
                    string log = "-40" + "\",\"" + weight.ToString();
                    LogHelper.WriteLog(log);
                }
                else if ((bool)fall_high_speed == true)////speed=80hz
                {
                    string log = "80" + "\",\"" + weight.ToString();
                    LogHelper.WriteLog(log);
                }
                else if ((bool)fall_low_speed == true)////speed=40hz
                {
                    string log = "40" + "\",\"" + weight.ToString();
                    LogHelper.WriteLog(log);
                }
                else if(TB_onpos==true && weight > 70 && lastweight-weight>0)
                {
                    string log = "0" + "\",\"" + weight.ToString();
                    LogHelper.WriteLog(log);
                }
                else
                {

                }
            }

        }

    }
}
