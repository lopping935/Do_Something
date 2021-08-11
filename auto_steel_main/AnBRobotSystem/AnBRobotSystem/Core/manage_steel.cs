using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnBRobotSystem.Utlis;
using logtest;
namespace AnBRobotSystem.Core
{
    class manage_steel
    {
        public float need_ibag_weight=0;
        public string fish_station = "";
        public float fish_weight=0;
        public string F_flag = "";
        public float realtime_weight=0;
        public string ibag_flag="";
        dbTaskHelper dbhlper = new dbTaskHelper();

         public void get_ibag_weight()
        {
            if(realtime_weight==0)
            {
                ibag_flag = "no_bag";
            }
            else if(realtime_weight > 280)
            {
                ibag_flag = "full_bag";
            }
            else if(realtime_weight>100&&realtime_weight<120)
            {
                ibag_flag = "nfull_bag";
                need_ibag_weight = 280 - realtime_weight;
            }
            else
            {
                ibag_flag = "error";
            }
      
        }
        public void get_fish_data()
        {
            try
            {
                DateTime TimeA_Fish, TimeB_Fish;
                float AF_mid_weight, BF_mid_weight, AFinit_weight, BFinit_weight;
                TimeA_Fish = Convert.ToDateTime(dbhlper.read_table_onefield("in_time", "RealTime_Car_Bag", "ID", "A_Fish"));
                TimeB_Fish = Convert.ToDateTime(dbhlper.read_table_onefield("in_time", "RealTime_Car_Bag", "ID", "B_Fish"));
                AF_mid_weight = float.Parse((dbhlper.read_table_onefield("mid_weight", "RealTime_Car_Bag", "ID", "A_Fish")));
                BF_mid_weight = float.Parse(dbhlper.read_table_onefield("mid_weight", "RealTime_Car_Bag", "ID", "B_Fish"));
                AFinit_weight = float.Parse((dbhlper.read_table_onefield("init_weight", "RealTime_Car_Bag", "ID", "A_Fish")));
                BFinit_weight = float.Parse(dbhlper.read_table_onefield("init_weight", "RealTime_Car_Bag", "ID", "B_Fish"));
                if (AF_mid_weight != 0 && BF_mid_weight != 0)
                {
                    if (DateTime.Compare(TimeA_Fish, TimeB_Fish) < 0)
                    {
                        fish_station = "A";
                        if(AF_mid_weight== AFinit_weight)
                        {
                            F_flag = "full";
                            fish_weight = AFinit_weight;
                        }
                        else
                        {
                            F_flag = "half";
                            fish_weight = AF_mid_weight;
                        }
                    }                        
                    else
                    {
                        fish_station = "B";
                        if (BF_mid_weight == BFinit_weight)
                        {
                            F_flag = "full";
                            fish_weight = BFinit_weight;
                        }
                        else
                        {
                            F_flag = "half";
                            fish_weight = BF_mid_weight;
                        }
                    }
                       
                }
                else if (AF_mid_weight != 0 && BF_mid_weight == 0)
                {
                    fish_station = "A";
                    if (AF_mid_weight == AFinit_weight)
                    {
                        F_flag = "full";
                        fish_weight = AFinit_weight;
                    }
                    else
                    {
                        F_flag = "half";
                        fish_weight = AF_mid_weight;
                    }
                }
                else if (BF_mid_weight != 0 && AF_mid_weight == 0)
                {
                    fish_station = "B";
                    if (BF_mid_weight == BFinit_weight)
                    {
                        F_flag = "full";
                        fish_weight = BFinit_weight;
                    }
                    else
                    {
                        F_flag = "half";
                        fish_weight = BF_mid_weight;
                    }
                }
                else
                {
                    fish_station = "err";
                }
            }
            catch(Exception e)
            {
                LogHelper.WriteLog("折铁罐号选择程序出错",e);
            }
            

        }
        public int updata_fish_weight(string F_Station)
        {
            int exe_result =dbhlper.updata_table("Data_change", "DataValue", F_Station, "DataName", "F_Station");
            return exe_result;
        }
    }
}
