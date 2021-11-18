using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnBRobotSystem;
using VM.Core;
using VM.PlatformSDKCS;
using AnBRobotSystem.Utlis;
using logtest;
namespace AnBRobotSystem.Core
{
    public class Tiebao
    {
        public  double area = 0;
        public  int Inital_light = 0;
        public  int Fall_edga_light = 0;
        public  double Set_circ_Dia = 0;
        public  double Real_circ_Dia = 0;
        public  bool Tiebao_ok = false;
        public bool getTBK_result()
        {
            try
            {
                if (null == MdiParent.process_IBag)
                {
                    return false;
                } 
                else
                {
                    MdiParent.process_IBag.Run();
                    IntResultInfo circle_yesno_info = MdiParent.process_IBag.GetIntOutputResult("circle_yesno");
                    int circle_yesno = circle_yesno_info.pIntValue[0];
                    FloatResultInfo circle_X_info = MdiParent.process_IBag.GetFloatOutputResult("circle_X");
                    float circle_X = circle_X_info.pFloatValue[0];
                    circle_X = (float)Math.Round(circle_X, 1);
                    FloatResultInfo circle_Y_info = MdiParent.process_IBag.GetFloatOutputResult("circle_Y");
                    float circle_Y = circle_Y_info.pFloatValue[0];
                    circle_Y = (float)Math.Round(circle_Y, 1);
                    FloatResultInfo circle_R_info = MdiParent.process_IBag.GetFloatOutputResult("circle_R");
                    float circle_R = circle_R_info.pFloatValue[0];
                    circle_R = (float)Math.Round(circle_R, 1);
                    string strMsg = "圆查找结果: " + circle_yesno.ToString() + "个，圆心：" + circle_X.ToString() + "," + circle_Y.ToString() + ",半径：" + circle_R.ToString();
                    Auto_steel_helper.mainlog("罐口结果", strMsg);
                    return true;
                }                             
            }
            catch(VmException ex)
            {
                string strMsg = "SaveSolution failed. Error Code: " + Convert.ToString(ex.errorCode, 16);
                LogHelper.WriteLog(strMsg, ex);
                return false;
            }
           

            
        }
        public void countiontest()
        {
            if (null == MdiParent.process_IBag) return;
            MdiParent.process_IBag.ContinousRunEnable = true;
        }
        public void get_Tiebao_data()
        {
            while(true)
            {
                System.Threading.Thread.Sleep(300);
                IntResultInfo circle_yesno_info = MdiParent.process_IBag.GetIntOutputResult("circle_yesno");
                int circle_yesno = circle_yesno_info.pIntValue[0];
                FloatResultInfo circle_X_info = MdiParent.process_IBag.GetFloatOutputResult("circle_X");
                float circle_X = circle_X_info.pFloatValue[0];
                circle_X = (float)Math.Round(circle_X, 1);
                FloatResultInfo circle_Y_info = MdiParent.process_IBag.GetFloatOutputResult("circle_Y");
                float circle_Y = circle_Y_info.pFloatValue[0];
                circle_Y = (float)Math.Round(circle_Y, 1);
                FloatResultInfo circle_R_info = MdiParent.process_IBag.GetFloatOutputResult("circle_R");
                float circle_R = circle_R_info.pFloatValue[0];
                circle_R = (float)Math.Round(circle_R, 1);
                string strMsg = "圆查找结果: " + circle_yesno.ToString() + "个，圆心：" + circle_X.ToString() + "," + circle_Y.ToString() + ",半径：" + circle_R.ToString();
                Auto_steel_helper.mainlog("罐口结果", strMsg);
            }
            
        }
    }
}
