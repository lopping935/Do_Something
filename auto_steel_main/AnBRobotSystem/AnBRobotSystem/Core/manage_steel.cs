using System;
using System.Collections.Generic;
using System.Data.Common;
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
        public float fish_init_weight = 0;
        public string F_flag = "";
        public float realtime_weight=0;
        public string ibag_flag="";
        public DateTime train_in_time = new DateTime();
        dbTaskHelper dbhlper = new dbTaskHelper();

       
        public bool get_TB_data()
        {
            try
            {
                if (PLCdata.TB_pos)
                {
                    realtime_weight = PLCdata.TB_weight;

                    if (realtime_weight == 0)
                    {
                        ibag_flag = "ER";
                        return false;
                    }
                    else if (realtime_weight > 280)
                    {
                        ibag_flag = "F";
                        return false;
                    }
                    else if (realtime_weight > 100 && realtime_weight < 280)
                    {
                        ibag_flag = "NF";
                        need_ibag_weight = 280 - realtime_weight;
                        return true;
                    }
                    else
                    {
                        ibag_flag = "ER";
                        return false;
                    }
                }
                else
                {
                    ibag_flag = "ER";
                    return false;
                }
                
            }
            catch (Exception e)
            {
                ibag_flag = "ER";
                LogHelper.WriteLog("罐包管理系统铁水包选择程序出错！", e);
                return false;
            }
           
        }
        public bool get_GB_data()
        {
            try
            {
                DateTime TimeA_Fish = new DateTime(), TimeB_Fish = new DateTime();
                Single AF_mid_weight = 0, BF_mid_weight = 0, AFinit_weight = 0, BFinit_weight = 0;
                string sql = "SELECT ID,in_time,init_weight,mid_weight FROM RealTime_Car_Bag where ID='A_Fish' or ID='B_Fish'";
                DbDataReader dr = null;
                dr = dbhlper.MultithreadDataReader(sql);
                while (dr.Read())
                {
                    if ( Convert.ToString(dr["ID"]).Trim() == "A_Fish")
                    {
                        
                        TimeA_Fish = Convert.ToDateTime(dr["in_time"]);
                        AFinit_weight = Convert.ToSingle(dr["init_weight"]);
                        AF_mid_weight = Convert.ToSingle(dr["mid_weight"]);
                    }
                    if (Convert.ToString(dr["ID"]).Trim() == "B_Fish")
                    {
                        TimeB_Fish = Convert.ToDateTime(dr["in_time"]);
                        BFinit_weight = Convert.ToSingle(dr["init_weight"]);
                        BF_mid_weight = Convert.ToSingle(dr["mid_weight"]);
                    }  
                }
                dr.Close();

                //TimeA_Fish = Convert.ToDateTime(dbhlper.read_table_onefield("in_time", "RealTime_Car_Bag", "ID", "A_Fish"));
                //TimeB_Fish = Convert.ToDateTime(dbhlper.read_table_onefield("in_time", "RealTime_Car_Bag", "ID", "B_Fish"));
                //AF_mid_weight = float.Parse((dbhlper.read_table_onefield("mid_weight", "RealTime_Car_Bag", "ID", "A_Fish")));
                //BF_mid_weight = float.Parse(dbhlper.read_table_onefield("mid_weight", "RealTime_Car_Bag", "ID", "B_Fish"));
                //AFinit_weight = float.Parse((dbhlper.read_table_onefield("init_weight", "RealTime_Car_Bag", "ID", "A_Fish")));
                //BFinit_weight = float.Parse(dbhlper.read_table_onefield("init_weight", "RealTime_Car_Bag", "ID", "B_Fish"));
                if (AF_mid_weight != 0 && BF_mid_weight != 0)
                {
                    if (DateTime.Compare(TimeA_Fish, TimeB_Fish) < 0)
                    {
                        fish_station = "A";
                        train_in_time = TimeA_Fish;
                        if (AF_mid_weight== AFinit_weight)
                        {
                            F_flag = "F";
                            fish_weight = AFinit_weight;
                            fish_init_weight = AFinit_weight;
                        }
                        else
                        {
                            F_flag = "NF";
                            fish_weight = AF_mid_weight;
                            fish_init_weight = AFinit_weight;
                        }
                    }                        
                    else
                    {
                        fish_station = "B";
                        train_in_time = TimeB_Fish;
                        if (BF_mid_weight == BFinit_weight)
                        {
                            F_flag = "F";
                            fish_weight = BFinit_weight;
                            fish_init_weight = BFinit_weight;
                        }
                        else
                        {
                            F_flag = "NF";
                            fish_weight = BF_mid_weight;
                            fish_init_weight = BFinit_weight;
                        }
                    }
                    return true;

                }
                else if (AF_mid_weight != 0 && BF_mid_weight == 0)
                {
                    fish_station = "A";
                    train_in_time = TimeA_Fish;
                    if (AF_mid_weight == AFinit_weight)
                    {
                        F_flag = "F";
                        fish_weight = AFinit_weight;
                        fish_init_weight = AFinit_weight;
                    }
                    else
                    {
                        F_flag = "NF";
                        fish_weight = AF_mid_weight;
                        fish_init_weight = AFinit_weight;
                    }
                    return true;
                }
                else if (BF_mid_weight != 0 && AF_mid_weight == 0)
                {
                    fish_station = "B";
                    train_in_time = TimeB_Fish;
                    if (BF_mid_weight == BFinit_weight)
                    {
                        F_flag = "F";
                        fish_weight = BFinit_weight;
                        fish_init_weight = BFinit_weight;
                    }
                    else
                    {
                        F_flag = "NF";
                        fish_weight = BF_mid_weight;
                        fish_init_weight = BFinit_weight;
                    }
                    return true;
                }
                else
                {
                    fish_station = "ER";
                    F_flag = "ER";
                    return false;
                }
            }
            catch(Exception e)
            {
                fish_station = "ER";
                F_flag = "ER";
                LogHelper.WriteLog("罐包管理系统折铁罐号选择程序出错！", e);
                return false;
            }
            

        }
        public int updata_fish_weight(string F_Station)
        {
            int exe_result =dbhlper.updata_table("Data_change", "DataValue", F_Station, "DataName", "F_Station");
            return exe_result;
        }
    }
}
