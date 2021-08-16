using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using S7.Net;
using System.Configuration;
using logtest;
namespace AnBRobotSystem.Core
{
    public static class PLCdata
    {

        public static float TB_weight = 113;
        public  static Single F_angle=0;
        public static bool TB_pos = true;
        public static bool GB_posA = true;
        public static bool GB_posB = true;
        public static Int16 speed = 0;
        public static bool connect = false;
        static string PLCIP = ConfigurationManager.ConnectionStrings["PLCIP"].ConnectionString;
        public static Plc plc300;
      
        public static void initplc()
        {
            plc300 = new Plc(CpuType.S7300, PLCIP, 0, 2);
            plc300.Open();//创建PLC实例
           

        }
        public static void read_io()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(100);
                connect = (bool)plc300.IsConnected;
                if (connect)
                {
                    TB_weight = (float)plc300.Read("");
                    speed = (Int16)plc300.Read("");


                }
            }
        }
        public static Int16 set_speed(Int16 speed)
        {
            try
            {
                ErrorCode err = plc300.Write("DB1.DBI20", speed);
                return (Int16)err;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog("PLC写入速度",e);
                return 1000;
            }
            
        }
        //持续运行记录角度
        public static Single get_angle()
        {
            try
            {
                ErrorCode err = plc300.Write("DB1.DBI20", speed);
                return F_angle;
            }
            catch (Exception e)
            {
                
                return 0;
            }
        }

    }
}
  

