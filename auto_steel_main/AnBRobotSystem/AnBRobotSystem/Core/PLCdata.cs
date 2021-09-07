using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using S7.Net;
using System.Configuration;
using logtest;
using AnBRobotSystem.Utlis;
namespace AnBRobotSystem.Core
{
    public class plcdata
    {
        public bool TB_pos;//铁包到位
        public bool GB_posA;//铁水包到位，可以用插电来获取
        public bool GB_posB;
        public bool connect;//罐车得电
        public bool GB_A_0_limt;//罐车0限位
        public bool GB_A_120_limt;//罐车0限位
        public bool GB_B_0_limt;//罐车0限位
        public bool GB_B_120_limt;//罐车120限位
        public bool GB_A_carIn;//火车到来信号
        public bool GB_B_carIn;
        public float TB_hight;//铁水液位
        public float TB_weight;//铁水包重量
        public Int16 TB_num;//铁水包号
        public Int16 GB_A_num;//罐车包号
        public Int16 GB_B_num;//罐车包号
        public Single GB_A_angle;//a实时倾角
        public Single GB_B_angle;//
        public Single GB_A_speed = 0;//A设定速度
        public Single GB_B_speed = 0;//B设定速度
        public Single GB_A_Rspeed = 0;//A真实速度
        public Single GB_B_Rspeed = 0;//B真实速度

    }
    public static class PLCdata
    {

        public static dbTaskHelper db_plc_helper=new dbTaskHelper();
        public static float TB_weight_speed = 0;
        public static List<Single> weight_sp_list = new List<Single>(4);
    
        public static Int16 speed = 0;
        public static bool connect;
        public static bool LGB_A_carIn;
        public static bool LGB_B_carIn;
        public static Single Mes_GB_weight = 0;
        static string PLCIP = ConfigurationManager.ConnectionStrings["PLCIP"].ConnectionString;
        public static Plc plc300;
        //mes数据暂存
        public static DateTime in_time;
        public static string GB_initwit,GB_num;
        

        //折铁plc数据对象
        public static plcdata ZT_data = new plcdata();
        public static void initplc()
        {
            plc300 = new Plc(CpuType.S7300, PLCIP, 0, 2);
            plc300.Open();//创建PLC实例
        }
        //读取plc数据并更新mes表格数据
        public static void Read_PLC_data()
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
                        ZT_data.GB_A_0_limt = bytes1[0].SelectBit(4);
                        ZT_data.GB_A_120_limt = bytes1[0].SelectBit(5);
                        ZT_data.GB_B_0_limt = bytes1[0].SelectBit(6);
                        ZT_data.GB_B_120_limt = bytes1[0].SelectBit(7);
                        ZT_data.GB_A_carIn = bytes1[1].SelectBit(0);
                        ZT_data.GB_B_carIn = bytes1[1].SelectBit(1);
                        int a = 1;
                        ZT_data.TB_hight = (float)S7.Net.Types.Double.FromByteArray(bytes1.Skip(a).Take(4).ToArray());
                        ZT_data.TB_weight = (float)S7.Net.Types.Double.FromByteArray(bytes1.Skip(a += 4).Take(4).ToArray());
                        ZT_data.TB_num = (Int16)S7.Net.Types.Int.FromByteArray(bytes1.Skip(a += 4).Take(2).ToArray());
                        ZT_data.GB_A_num = (Int16)S7.Net.Types.Int.FromByteArray(bytes1.Skip(a += 2).Take(2).ToArray());
                        ZT_data.GB_B_num = (Int16)S7.Net.Types.Int.FromByteArray(bytes1.Skip(a += 2).Take(2).ToArray());
                        ZT_data.GB_A_angle = (float)S7.Net.Types.Double.FromByteArray(bytes1.Skip(a += 2).Take(4).ToArray());
                        ZT_data.GB_B_angle = (float)S7.Net.Types.Double.FromByteArray(bytes1.Skip(a += 4).Take(4).ToArray());
                        ZT_data.GB_A_Rspeed = (float)S7.Net.Types.Double.FromByteArray(bytes1.Skip(a += 4).Take(4).ToArray());
                        ZT_data.GB_B_Rspeed = (float)S7.Net.Types.Double.FromByteArray(bytes1.Skip(a += 4).Take(4).ToArray());
                        calc_weight_speed();
                        if(LGB_A_carIn==false&& ZT_data.GB_A_carIn==true)
                        {
                            
                            //db_plc_helper.updata_table("RealTime_Car_Bag", "mid_weight", Mes_GB_weight.ToString(), "ID", "A");
                            string sqltext = string.Format("UPDATE RealTime_Car_Bag SET in_time='{0}',init_weight= '{1}',mid_weight='{2}',number='{3}' WHERE ID= 'A'", in_time, GB_initwit, GB_initwit, GB_num);
                            db_plc_helper.MultithreadExecuteNonQuery(sqltext);
                        }
                        if (LGB_B_carIn == false && ZT_data.GB_B_carIn == true)
                        {
                            string sqltext = string.Format("UPDATE RealTime_Car_Bag SET in_time='{0}',init_weight= '{1}',mid_weight='{2}',number='{3}' WHERE ID= 'B'", in_time, GB_initwit, GB_initwit, GB_num);
                            db_plc_helper.MultithreadExecuteNonQuery(sqltext);
                        }
                        LGB_A_carIn = ZT_data.GB_A_carIn;
                        LGB_B_carIn = ZT_data.GB_B_carIn;
                    }
                }
            }
            catch(Exception e)
            {
                LogHelper.WriteLog("PLC读取数据", e);
            }  
        }
        public static Int16 set_speed(string flag,Int16 speed)
        {
            try
            {
                if(flag == "A")
                {
                    //ErrorCode err = plc300.Write("DB1.DBI20", speed);
                    //return (Int16)err;
                    ZT_data.GB_A_speed = 20;
                    return 0;
                }
                else
                {
                    ZT_data.GB_B_speed = 20;
                    return 0;
                    //ErrorCode err = plc300.Write("DB1.DBI20", speed);
                    //return (Int16)err;
                }
                
            }
            catch (Exception e)
            {
                LogHelper.WriteLog("PLC写入速度",e);
                return 1000;
            }
            
        }
        //持续计算铁包重量
        public static Single calc_weight_speed()
        {
            try
            {
                Single spd_sum = 0;
                if(weight_sp_list.Count>=5)
                {
                    weight_sp_list.RemoveAt(0);
                    weight_sp_list.Add(ZT_data.TB_weight);
                    for(int i=0;i<4;i++)
                    {
                        spd_sum += weight_sp_list[i + 1] - weight_sp_list[i];
                    }
                    TB_weight_speed = spd_sum / (Single)4;
                }
                else
                {
                    weight_sp_list.Add(ZT_data.TB_weight);
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
  

