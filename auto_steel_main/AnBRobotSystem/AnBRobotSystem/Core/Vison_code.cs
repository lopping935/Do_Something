using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Core;
using logtest;
using AnBRobotSystem;
using AnBRobotSystem.Utlis;
using VM.PlatformSDKCS;

namespace AnBRobotSystem.Core
{
    

    public class Tiebao
    {
        public updatelistiew writelistview;
        
        public double area = 0;
        public int Inital_light = 0;
        public int Fall_edga_light = 0;
        public double Set_circ_Dia = 0;
        public double Real_circ_Dia = 0;
        public bool Tiebao_ok = false;
        public Tiebao()
        {
          //  updata1 = MdiParent.form.mainlog;
        }
   
        public bool TB_init_result()
        {
            try
            {
                if (null == MdiParent.process_TB)
                {
                    return false;
                }
                else
                {
                    MdiParent.process_TB.Run();
                    IntResultInfo circle_yesno_info = MdiParent.process_TB.GetIntOutputResult("circle_yesno");
                    int circle_yesno = circle_yesno_info.pIntValue[0];
                    FloatResultInfo circle_X_info = MdiParent.process_TB.GetFloatOutputResult("circle_X");
                    float circle_X = circle_X_info.pFloatValue[0];
                    circle_X = (float)Math.Round(circle_X, 1);
                    FloatResultInfo circle_Y_info = MdiParent.process_TB.GetFloatOutputResult("circle_Y");
                    float circle_Y = circle_Y_info.pFloatValue[0];
                    circle_Y = (float)Math.Round(circle_Y, 1);
                    FloatResultInfo circle_R_info = MdiParent.process_TB.GetFloatOutputResult("circle_R");
                    float circle_R = circle_R_info.pFloatValue[0];
                    circle_R = (float)Math.Round(circle_R, 1);
                    string strMsg = "圆查找结果: " + circle_yesno.ToString() + "个，圆心：" + circle_X.ToString() + "," + circle_Y.ToString() + ",半径：" + circle_R.ToString();
                    writelistview("铁包视觉",strMsg,"log");
                    return true;
                }
            }
            catch (VmException ex)
            {
                writelistview("包口视觉", ex.errorMessage, "err");
                string strMsg = "SaveSolution failed. Error Code: " + Convert.ToString(ex.errorCode, 16);
                LogHelper.WriteLog(strMsg, ex);
                return false;
            }



        }
        public void countiontest()
        {
            if (null == MdiParent.process_GK) return;
            MdiParent.process_GK.ContinousRunEnable = true;
        }
        public void TL_in_TB()
        {
            while (true)
            {
               
            }

        }
    }
    public class GuanKou
    {
        public updatelistiew writelistview;
        public  double area = 0;
        public  int Inital_light = 0;
        public  Single Fall_edga_light = 0;
        public  string shape = "";
        public bool GK_init_result(string GK_station)
        {
            try
            {
                if(GK_station=="A")
                {
                    if (null == MdiParent.process_GK)
                    {
                        return false;
                    }
                    else
                    {

                        MdiParent.process_GK.Run();
                        IntResultInfo circle_yesno_info = MdiParent.process_GK.GetIntOutputResult("circle_yesno");
                        int circle_yesno = circle_yesno_info.pIntValue[0];
                        FloatResultInfo circle_X_info = MdiParent.process_GK.GetFloatOutputResult("circle_X");
                        float circle_X = circle_X_info.pFloatValue[0];
                        circle_X = (float)Math.Round(circle_X, 1);
                        FloatResultInfo circle_Y_info = MdiParent.process_GK.GetFloatOutputResult("circle_Y");
                        float circle_Y = circle_Y_info.pFloatValue[0];
                        circle_Y = (float)Math.Round(circle_Y, 1);
                        FloatResultInfo circle_R_info = MdiParent.process_GK.GetFloatOutputResult("circle_R");
                        float circle_R = circle_R_info.pFloatValue[0];
                        circle_R = (float)Math.Round(circle_R, 1);
                        string strMsg = "圆查找结果: " + circle_yesno.ToString() + "个，圆心：" + circle_X.ToString() + "," + circle_Y.ToString() + ",半径：" + circle_R.ToString();
                        writelistview("罐口结果", strMsg, "log");

                        return true;
                    }
                }
                else
                {
                    if (null == MdiParent.process_GK)
                    {
                        return false;
                    }
                    else
                    {

                        MdiParent.process_GK.Run();
                        IntResultInfo circle_yesno_info = MdiParent.process_GK.GetIntOutputResult("circle_yesno");
                        int circle_yesno = circle_yesno_info.pIntValue[0];
                        FloatResultInfo circle_X_info = MdiParent.process_GK.GetFloatOutputResult("circle_X");
                        float circle_X = circle_X_info.pFloatValue[0];
                        circle_X = (float)Math.Round(circle_X, 1);
                        FloatResultInfo circle_Y_info = MdiParent.process_GK.GetFloatOutputResult("circle_Y");
                        float circle_Y = circle_Y_info.pFloatValue[0];
                        circle_Y = (float)Math.Round(circle_Y, 1);
                        FloatResultInfo circle_R_info = MdiParent.process_GK.GetFloatOutputResult("circle_R");
                        float circle_R = circle_R_info.pFloatValue[0];
                        circle_R = (float)Math.Round(circle_R, 1);
                        string strMsg = "圆查找结果: " + circle_yesno.ToString() + "个，圆心：" + circle_X.ToString() + "," + circle_Y.ToString() + ",半径：" + circle_R.ToString();
                        writelistview("罐口结果", strMsg, "log");

                        return true;
                    }
                }  

            }
            catch (VmException ex)
            {
                writelistview("罐口视觉", ex.errorMessage, "err");
                string strMsg = "SaveSolution failed. Error Code: " + Convert.ToString(ex.errorCode, 16);
                LogHelper.WriteLog(strMsg, ex);
                return false;
            }

        }
        public bool set_GK_contiu()
        {
            if (null == MdiParent.process_GK) return false;
            if(MdiParent.process_GK.ContinousRunEnable)
            {
                return true;
            }
            else
            {
                MdiParent.process_GK.ContinousRunEnable = true;
                if (MdiParent.process_GK.ContinousRunEnable)
                    return true;
                else
                    return false;
            }
           
        }
        public bool GK_light_result(string GB_flag)
        {

            try
            {
                if (null == MdiParent.process_GK)
                {
                    return false;
                }
                else
                { 
                    if(GB_flag=="A")
                    {
                        Fall_edga_light = 25;
                        return true;
                    }
                    else
                    {
                        Fall_edga_light = 25;
                        return true;
                    }
                   
                }
            }
            catch (VmException ex)
            {
                string strMsg = "SaveSolution failed. Error Code: " + Convert.ToString(ex.errorCode, 16);
                LogHelper.WriteLog(strMsg, ex);
                return false;
            }
        }
    }
    public class TieLiu
    {
        public updatelistiew writelistview;
        public  bool Get_iron = false;
        public  double area = 0;
        public static string shape = "";
        public bool TL_light_result()
        {
            try
            {
                if (null == MdiParent.process_TL)
                {
                    return false;
                }
                else
                {
                    //MdiParent.process_TL.Run();
                    IntResultInfo blob_num_info = MdiParent.process_TL.GetIntOutputResult("blob_num");
                    int blob_num = blob_num_info.pIntValue[0];
                    string strMsg = "blob结果: " + blob_num.ToString() + "个";
                    writelistview("铁流结果", strMsg, "log");
                    Get_iron = true;
                    return true;
                }
            }
            catch (VmException ex)
            {
                string strMsg = "SaveSolution failed. Error Code: " + Convert.ToString(ex.errorCode, 16);
                LogHelper.WriteLog(strMsg, ex);
                Get_iron = false;
                return false;
            }

        }
       
    }
}
