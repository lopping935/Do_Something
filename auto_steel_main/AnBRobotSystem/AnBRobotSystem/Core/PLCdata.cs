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
    public struct plcdata
    {
        public bool TB_pos;//铁包到位
        public bool GB_posA;//铁水包到位，可以用插电来获取
        public bool GB_posB;
        public bool connect;//罐车得电
        public bool GB_0_limt;//罐车0限位
        public bool GB_120_limt;//罐车120限位
        public float TB_hight;//铁水液位
        public float TB_weight;//铁水包重量
        public Int16 TB_num;//铁水包号
        public Int16 GB_A_num;//罐车包号
        public Int16 GB_B_num;//罐车包号
        public Single GB_A_angle;
        public Single GB_B_angle;
    }
    public static class PLCdata
    {

        
        public static float TB_weight_speed = 0;
        //public static Queue<Single>  weight_queu = new Queue<float>(5);
        public static List<Single> weight_sp_list = new List<Single>(4);
        
        /// <summary>
        /// plc数据
        /// </summary>
        public static bool TB_pos = true;//铁包到位
        public static bool GB_posA = true;//铁水包到位，可以用插电来获取
        public static bool GB_posB = true;
        public static bool connect = false;//罐车得电
        public static bool GB_0_limt = true;//罐车0限位
        public static bool GB_120_limt = true;//罐车120限位        
        public static float TB_hight = 113;//铁水液位
        public static float TB_weight = 113;//铁水包重量
        public static Int16 TB_num = 8;//铁水包号
        public static Int16 GB_A_num = 8;//罐车包号
        public static Int16 GB_B_num = 8;//罐车包号
        public static Single GB_A_angle = 0;
        public static Single GB_B_angle = 0;

        public static Single F_angle = 0;
        public static Int16 speed = 0;

        static string PLCIP = ConfigurationManager.ConnectionStrings["PLCIP"].ConnectionString;
        public static Plc plc300;
       
        public static plcdata ZT_data = new plcdata();
        public static void initplc()
        {
            plc300 = new Plc(CpuType.S7300, PLCIP, 0, 2);
            plc300.Open();//创建PLC实例
           

        }

        public static void read_io()
        {
            try
            {
                while (true)
                {
                    System.Threading.Thread.Sleep(500);
                    connect = (bool)plc300.IsConnected;
                    if (connect)
                    {

                        byte[] bytes1 = plc300.ReadBytes(DataType.DataBlock, 80, 74, 8);
                        ZT_data.TB_pos = bytes1[0].SelectBit(0);
                        ZT_data.GB_posA = bytes1[0].SelectBit(1);
                        ZT_data.GB_posA = bytes1[0].SelectBit(2);
                        ZT_data.connect = bytes1[0].SelectBit(3);
                        ZT_data.GB_0_limt = bytes1[0].SelectBit(4);
                        ZT_data.GB_120_limt = bytes1[0].SelectBit(5);
                        int a = 1;
                        ZT_data.TB_hight = (float)S7.Net.Types.Double.FromByteArray(bytes1.Skip(a).Take(4).ToArray());
                        ZT_data.TB_weight = (float)S7.Net.Types.Double.FromByteArray(bytes1.Skip(a += 4).Take(4).ToArray());
                        ZT_data.TB_num = (Int16)S7.Net.Types.Int.FromByteArray(bytes1.Skip(a += 4).Take(2).ToArray());
                        ZT_data.GB_A_num = (Int16)S7.Net.Types.Int.FromByteArray(bytes1.Skip(a += 2).Take(2).ToArray());
                        ZT_data.GB_B_num = (Int16)S7.Net.Types.Int.FromByteArray(bytes1.Skip(a += 2).Take(2).ToArray());
                        ZT_data.GB_A_angle = (float)S7.Net.Types.Double.FromByteArray(bytes1.Skip(a += 2).Take(4).ToArray());
                        ZT_data.GB_B_angle = (float)S7.Net.Types.Double.FromByteArray(bytes1.Skip(a += 4).Take(4).ToArray());
                        //fish_full_weight = (float)S7.Net.Types.Double.FromByteArray(bytes1.Skip(4).Take(4).ToArray());

                    }
                }
            }
            catch(Exception e)
            {
                LogHelper.WriteLog("PLC读取数据", e);
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
        

        public static Single calc_weight_speed()
        {
            try
            {
                Single spd_sum = 0;
                if(weight_sp_list.Count>=5)
                {
                    weight_sp_list.RemoveAt(0);
                    weight_sp_list.Add(TB_weight);
                    for(int i=0;i<4;i++)
                    {
                        spd_sum += weight_sp_list[i + 1] - weight_sp_list[i];
                    }
                    TB_weight_speed = spd_sum / (Single)4;
                }
                else
                {
                    weight_sp_list.Add(TB_weight);
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
  

