using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using S7.Net;
namespace AnBRobotSystem.Core
{
    public static class PLCdata
    {

        public static double Iron_bag_weight = 0;
        public static double speed = 0;
        public static bool connect = false;
        public static Plc plc300 = new Plc(CpuType.S7300, "172.16.47.244", 0, 2);
        public static void start_get()
        {
            plc300.Open();//创建PLC实例
            while(true)
            {
                System.Threading.Thread.Sleep(100);
                connect = (bool)plc300.IsConnected;
                if(connect)
                {
                    Iron_bag_weight = (double)plc300.Read("");
                    speed = (double)plc300.Read("");
                }
            }

        }

    }
}
  

