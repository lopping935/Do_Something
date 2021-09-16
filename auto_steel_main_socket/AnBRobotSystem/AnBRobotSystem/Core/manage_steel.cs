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
        public float need_ibag_weight = 0;
        public string fish_station = "";
        public float fish_weight = 0;
        public float fish_init_weight = 0;
        public string F_flag = "";
        public float realtime_weight = 0;
        public string ibag_flag = "";
        public string fish_num = "";
        public DateTime train_in_time = new DateTime();
        public dbTaskHelper dbhlper;
        public updatelistiew writelisview;
        public manage_steel()
        {
        }
        public bool get_TB_data()
        {
            try
            {
                if (PLCdata.ZT_data.TB_pos == true)
                {
                    realtime_weight = PLCdata.ZT_data.TB_weight;

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
                    else if (realtime_weight > 70 && realtime_weight < 280)
                    {
                        ibag_flag = "NF";
                        need_ibag_weight = 280 - realtime_weight;
                        writelisview("罐包管理系统", "计算所需铁水", "log");
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
                writelisview("包数据", e.Message, "err");
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
                string fish_numA = "", fish_numB = "";
                string sql = "SELECT ID,in_time,init_weight,mid_weight,number FROM RealTime_Car_Bag where ID='A' or ID='B'";
                DbDataReader dr = null;
                dr = dbhlper.MultithreadDataReader(sql);
                while (dr.Read())
                {
                    if (Convert.ToString(dr["ID"]).Trim() == "A")
                    {

                        TimeA_Fish = Convert.ToDateTime(dr["in_time"]);
                        AFinit_weight = Convert.ToSingle(dr["init_weight"]);
                        AF_mid_weight = Convert.ToSingle(dr["mid_weight"]);
                        fish_numA = dr["number"].ToString().Trim();
                    }
                    if (Convert.ToString(dr["ID"]).Trim() == "B")
                    {
                        TimeB_Fish = Convert.ToDateTime(dr["in_time"]);
                        BFinit_weight = Convert.ToSingle(dr["init_weight"]);
                        BF_mid_weight = Convert.ToSingle(dr["mid_weight"]);
                        fish_numB = dr["number"].ToString();
                    }
                }
                dr.Close();

                if (AF_mid_weight != 0 && BF_mid_weight != 0)
                {
                    if (DateTime.Compare(TimeA_Fish, TimeB_Fish) < 0)
                    {
                        fish_station = "A";
                        fish_num = fish_numA;
                        train_in_time = TimeA_Fish;
                        if (PLCdata.ZT_data.GB_posA == true && PLCdata.ZT_data.GB_A_connect == true && PLCdata.ZT_data.GB_A_0_limt == true&& PLCdata.ZT_data.GB_A_120_limt == false&& fish_num == (PLCdata.ZT_data.GB_A_num).ToString())
                        {
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
                        }
                        else
                        {
                            return false;
                        }
                        
                    }
                    else
                    {
                        fish_station = "B";
                        fish_num = fish_numB;
                        train_in_time = TimeB_Fish;
                        if (PLCdata.ZT_data.GB_posB == true && PLCdata.ZT_data.GB_B_connect == true && PLCdata.ZT_data.GB_B_0_limt == true && PLCdata.ZT_data.GB_B_120_limt == false && fish_num == (PLCdata.ZT_data.GB_B_num).ToString())
                        {
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
                        else
                        {
                            return false;
                        }
                            
                    }
                    return true;

                }
                else if (AF_mid_weight != 0 && BF_mid_weight == 0)
                {
                    fish_station = "A";
                    train_in_time = TimeA_Fish;
                    fish_num = fish_numA;
                    if (PLCdata.ZT_data.GB_posA == true && PLCdata.ZT_data.GB_A_connect == true && PLCdata.ZT_data.GB_A_0_limt == true && PLCdata.ZT_data.GB_A_120_limt == false && fish_num == (PLCdata.ZT_data.GB_A_num).ToString())
                    {
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
                    }
                    else
                    {
                        return false;
                    }
                        
                    return true;
                }
                else if (BF_mid_weight != 0 && AF_mid_weight == 0)
                {
                    fish_station = "B";
                    train_in_time = TimeB_Fish;
                    fish_num = fish_numB;
                    if (PLCdata.ZT_data.GB_posB == true && PLCdata.ZT_data.GB_B_connect == true && PLCdata.ZT_data.GB_B_0_limt == true && PLCdata.ZT_data.GB_B_120_limt == false && fish_num == (PLCdata.ZT_data.GB_B_num).ToString())
                    {
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
                    else
                    {
                        return false;
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
            catch (Exception e)
            {
                writelisview("自动罐数据", e.Message, "err");
                fish_station = "ER";
                F_flag = "ER";
                LogHelper.WriteLog("罐包管理系统折铁罐号选择程序出错！", e);
                return false;
            }


        }
        public bool get_GB_data_manual(string GB_ID)
        {
            try
            {
                DateTime TimeA_Fish = new DateTime();
                Single AF_mid_weight = 0, AFinit_weight = 0;
                bool condition_flag = false;
                //string sql = "SELECT ID,in_time,init_weight,mid_weight FROM RealTime_Car_Bag where ID='A' or ID='B'";
                string sql = string.Format("SELECT ID,in_time,init_weight,mid_weight,number FROM RealTime_Car_Bag where ID='{0}'", GB_ID);

                DbDataReader dr = null;

                dr = dbhlper.MultithreadDataReader(sql);
                while (dr.Read())
                {
                    train_in_time = Convert.ToDateTime(dr["in_time"]);
                    AFinit_weight = Convert.ToSingle(dr["init_weight"]);
                    AF_mid_weight = Convert.ToSingle(dr["mid_weight"]);
                    fish_num = dr["number"].ToString().Trim();
                }
                dr.Close();

                if (GB_ID == "A")
                {
                    fish_num = (PLCdata.ZT_data.GB_A_num).ToString();
                    fish_station = "A";
                    if (PLCdata.ZT_data.GB_posA == true && PLCdata.ZT_data.GB_A_connect == true && PLCdata.ZT_data.GB_A_0_limt == true && PLCdata.ZT_data.GB_A_120_limt == false && fish_num == (PLCdata.ZT_data.GB_A_num).ToString())
                        condition_flag = true;
                    else
                        condition_flag = false;
                }
                else
                {
                    fish_station = "B";
                    if (PLCdata.ZT_data.GB_posB == true && PLCdata.ZT_data.GB_B_connect == true && PLCdata.ZT_data.GB_B_0_limt == true && PLCdata.ZT_data.GB_B_120_limt == false && fish_num == (PLCdata.ZT_data.GB_B_num).ToString())
                        condition_flag = true;
                    else
                        condition_flag = false;

                }
                if (AF_mid_weight != 0)
                {
                    if (condition_flag==true)
                    {
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
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    fish_station = "ER";
                    F_flag = "ER";
                    return false;
                }
                return true;


            }
            catch (Exception e)
            {
                writelisview("手动罐数据", e.Message, "err");
                fish_station = "ER";
                F_flag = "ER";
                LogHelper.WriteLog("罐包管理系统折铁罐号选择程序出错！", e);
                return false;
            }


        }

        public int updata_fish_weight(string F_Station)
        {
            int exe_result = dbhlper.updata_table("Data_change", "DataValue", F_Station, "DataName", "F_Station");
            return exe_result;
        }
    }
}
