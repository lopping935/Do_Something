using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using GH_Utlis;
using S7.Net;
using VM.Core;
using VM.PlatformSDKCS;
using Sunny.UI;

namespace temp_robot_datarecord
{
    /*public struct plcdata example
    {
        public Int16 Heart;//心跳
        public bool TB_on_tempos;//铁包在测温位
        public float TB_weight;//铁水包重量        
        public Single TB_LIQID;//铁包液位
    }*/
    public struct plcdata
    {
        public bool datachgeflag;//
        public Int16 rotbot_flag;//
        public Int16 case_flag;
        public Int16 alarmdata;
        //public Single TB_LIQID;//
        //public Int16 Heart;//
    }
    public partial class Form1 : UIForm
    {
        public dbTaskHelper db_plc_helper = new dbTaskHelper();
        static int[] PLCflag = {10,30,40,49,65};
        static string[] signal_str;//后续自动配置
        static string[] str = new string[20];
        public static Plc robot_plc;
        public string plcip;
        public plcdata ZT_data = new plcdata();
        XMLHelper xxx;

        private string logPath = Application.StartupPath + "/Log/Message";//日志路径
        private string strLogPath = Application.StartupPath + "\\log\\Message\\";//路径
        private string strSolutionPath = Application.StartupPath + "\\ocr.sol";//方案路径
        private VmProcedure procedure = null;//流程
        private bool isSolutionLoad = false;//true表示方案加载成功，false表示方案加载失败
        private bool isContinuRun = false;//true表示连续运行，false表示停止连续运行

        public Form1()
        {
           
            InitializeComponent();
            
            LogHelper.loginfo.Info("开始记录！");
            VmSolution.OnWorkStatusEvent += VmSolution_OnWorkStatusEvent;//注册流程状态回调
            //VmSolution.OnProcessStatusStartEvent += VmSolution_OnProcessStatusStartEvent;   // 开始连续执行状态回调
            //VmSolution.OnProcessStatusStopEvent += VmSolution_OnProcessStatusStopEvent; // 结束连续执行状态回调
            Control.CheckForIllegalCrossThreadCalls = false;
            //string sqlstr=string.Format("insert into workrecord values('{0}','{1}','{2}','','','','')",DateTime.Now.ToString(), DateTime.Now.ToString(), DateTime.Now.ToString());
            //db_plc_helper.MultithreadExecuteNonQuery(sqlstr);
        }

        /// <summary>
        /// 流程执行回调
        /// </summary>
        /// <param name="workStatusInfo"></param>
        private void VmSolution_OnWorkStatusEvent(VM.PlatformSDKCS.ImvsSdkDefine.IMVS_MODULE_WORK_STAUS workStatusInfo)
        {
            string strMessage = null;
            try
            {
                if (workStatusInfo.nWorkStatus == 0 && workStatusInfo.nProcessID == 10000)//流程空闲且为流程1
                {
                    //获取流程结果
                    string strResult = procedure.ModuResult.GetOutputString("OCR").astStringVal[0].strValue;
                    //float H_ANG = procedure.ModuResult.GetOutputFloat("H_angle").pFloatVal[0];
                    if (strResult != null)
                    {
                        //UpdateResult(strResult);
                        //strMessage = "流程运行耗时：" + procedure.ProcessTime.ToString() + "ms";
                        LogUpdate(strResult);
                        //txt_OCR.Text = strResult;
                    }
                    else
                    {
                        strMessage = "获取结果失败：结果为空!";
                        //LogUpdate(strMessage);
                        //txt_OCR.Text = "";
                    }

                }

            }
            catch (VmException ex)
            {
                strMessage = "获取结果失败，错误码：0x" + Convert.ToString(ex.errorCode, 16);
                LogUpdate(strMessage);
            }
            catch (Exception ex)
            {
                strMessage = "获取结果失败：" + Convert.ToString(ex.Message);
                LogUpdate(strMessage);
            }
        }
        private void buttonRunOnce_Click(object sender, EventArgs e)
        {
            string strMessage = null;
            try
            {
                if (isSolutionLoad == true && null != procedure)
                {
                    procedure.Run();
                    strMessage = "运行一次！!";
                    LogUpdate(strMessage);
                }
                else
                {
                    strMessage = "所选流程不存在!";
                    LogUpdate(strMessage);
                }
            }
            catch (VmException ex)
            {
                strMessage = "单次运行失败，错误码：0x" + Convert.ToString(ex.errorCode, 16);
                LogUpdate(strMessage);
            }
            catch (Exception ex)
            {
                strMessage = "单次运行失败：" + Convert.ToString(ex.Message);
                LogUpdate(strMessage);
            }
        }
        private void buttonLoadSolu_Click(object sender, EventArgs e)
        {
            string strMessage = null;
            //buttonLoadSolu.BackColor = Color.Orange;
            buttonLoadSolu.Enabled = false;

            // 禁用其余按钮，防止误操作

            buttonRunOnce.Enabled = false;
            buttonSaveSolu.Enabled = false;

            try
            {
                if (isSolutionLoad == true)
                {
                    isSolutionLoad = false;
                }
                VmSolution.Load(strSolutionPath, "");//加载
                isSolutionLoad = true;
                strMessage = "方案加载成功.";
                buttonLoadSolu.BackColor = Color.Green;
                LogUpdate(strMessage);
                procedure = VmSolution.Instance["流程1"] as VmProcedure;
                if (procedure == null)
                {
                    strMessage = "流程为空，请检查方案!";
                    LogUpdate(strMessage);
                    return;
                }
                //绑定渲染源
                vmRenderControl1.ModuleSource = procedure;



            }
            catch (VmException ex)
            {
                strMessage = "方案加载失败，错误码：0x" + Convert.ToString(ex.errorCode, 16);
                LogUpdate(strMessage);
            }
            catch (Exception ex)
            {
                strMessage = "方案加载失败：" + Convert.ToString(ex.Message);
                LogUpdate(strMessage);
            }
            finally
            {
                buttonLoadSolu.BackColor = Color.DimGray;
                buttonLoadSolu.Enabled = true;

                // 重新启用其余按钮                
                buttonRunOnce.Enabled = true;
                buttonSaveSolu.Enabled = true;
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (null != strSolutionPath && true == isSolutionLoad)
            {

                try
                {
                    if (procedure != null || isContinuRun == true)
                    {
                        procedure.ContinuousRunEnable = false;
                        VmSolution.Save();
                    }
                    VmSolution.Instance.Dispose();
                }
                catch (Exception ex)
                {
                    string strMsg = "方案保存失败！" + ex.ToString();
                    LogUpdate(strMsg);
                }

            }

            

        }
        private void KillProcess(string strKillName)
        {
            foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcesses())
            {
                if (p.ProcessName.Contains(strKillName))
                {
                    p.Kill();
                    p.WaitForExit();
                }
            }
        }


        /// <summary>
        /// 消息显示
        /// </summary>
        /// <param name="str"></param>
        private void LogUpdate(string str)
        {
            string timeStamp = DateTime.Now.ToString("yy-MM-dd HH:mm:ss-fff");
            //如果记录超过1万条，应当清空再添加记录，以防记录的条目巨大引起界面卡顿和闪烁
            if (listViewLog.Items.Count > 100)
                listViewLog.Items.Clear();

            listViewLog.BeginInvoke(new Action(() =>
            {
                listViewLog.Items.Insert(0, new ListViewItem(new string[] { timeStamp, str }));
            }));

            SaveLog(str);
        }
        /// <summary>
        /// 保存日志
        /// </summary>
        /// <param name="str"></param>
        private void SaveLog(string str)
        {
            Task.Run(() =>
            {
                try
                {
                    if (!Directory.Exists(logPath))//如果日志目录不存在就创建
                    {
                        Directory.CreateDirectory(logPath);
                    }
                    string filename = logPath + "/" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";//用日期对日志文件命名
                    StreamWriter mySw = File.AppendText(filename);
                    mySw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss::ffff\t") + str);
                    mySw.Close();
                }
                catch
                {
                    return;
                }
            });
        }
        public void initplc()
        {
            try
            {
                robot_plc = new Plc(CpuType.S71200, plcip, 0, 1);
                robot_plc.Open();//创建PLC实例              
            }
            catch (Exception e)
            {

            }
        }
        public void readplc()
        {
            ZT_data = (plcdata)robot_plc.ReadStruct(typeof(plcdata), 50);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            
           try
           {

           }
           catch (Exception ex)
           {

           }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            buttonLoadSolu_Click(null, null);
        }
    }
}
