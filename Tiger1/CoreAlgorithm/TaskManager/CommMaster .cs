using System;
using System.Runtime.InteropServices;
using SQLPublicClass;
using System.Data.Common;
using System.Threading;
using System.Reflection;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using SocketHelper;
namespace CoreAlgorithm.TaskManager
{
    public struct LabelData
    {
        public string MACHINE_NO;// 打包机组号
        public string ID_TIME;
        public string ID_LOT_PROD;//生产批号
        public Int16 ID_PART_LOT; //分批号            
        public double DIM_LEN; //米长
        public string IND_FIXED;// 定尺标志
        public double SEQ_SEND;// 下发顺序号
        public Int16 NUM_BAR;// 捆内支数
        public Int16 SEQ_LIST;// 排列序号
        public double WT_BDL_ACT;// 重量           
        public string TMSTP_SEND;// 发送时间
        public string ID_PERSON;//下发人
    };
    public class CommMaster
    { 
        public static TasksManager tm;
        INIClass ini = new INIClass(System.AppDomain.CurrentDomain.BaseDirectory + "Config.ini");
        string localip = "", plcip = "";//400PLC ip
        int localportr = 0, plcportr = 0, plcports = 0;//400PLC端口

        public CommMaster()
        {
            tm = new TasksManager();
            string sql = "SELECT ACQUISITIONCONFIG_ID,DATAACQUISITION_IP,DATAACQUISITION_PORTR,DATAACQUISITION_PORTS FROM ACQUISITIONCONFIG where ACQUISITIONCONFIG_ID=1 or ACQUISITIONCONFIG_ID=4";// ";
            DbDataReader dr = tm.MultithreadDataReader(sql);
            while (dr.Read())
            {
                if (Convert.ToInt16(dr["ACQUISITIONCONFIG_ID"]) == 1)
                {
                    plcip = Convert.ToString(dr["DATAACQUISITION_IP"]);
                    plcportr = Convert.ToInt32(dr["DATAACQUISITION_PORTR"]);
                    plcports = Convert.ToInt32(dr["DATAACQUISITION_PORTS"]);
                }
                else if (Convert.ToInt16(dr["ACQUISITIONCONFIG_ID"]) == 4)
                {
                    localip = Convert.ToString(dr["DATAACQUISITION_IP"]);
                    localportr = Convert.ToInt32(dr["DATAACQUISITION_PORTR"]);
                }

            }
            dr.Close();
        }

        public  Double GetMsgID(int ID)
        {
            string sql = "";
            Double Id = 0;
            try
            {
                sql = string.Format("select PARAMETER_VALUE from SYSPARAMETER where PARAMETER_ID={0}", ID);
                DbDataReader dr = tm.MultithreadDataReader(sql);
                while (dr.Read())
                {
                    if (dr["PARAMETER_VALUE"] != DBNull.Value)
                        Id = Convert.ToInt64(dr["PARAMETER_VALUE"]);
                }
                dr.Close();
                sql = string.Format("UPDATE SYSPARAMETER SET PARAMETER_VALUE={0},PARAMETER_TIME='{1}' where PARAMETER_ID={2}", 0, DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")),ID);
                tm.MultithreadExecuteNonQuery(sql);
                return Id;
            }
            catch (Exception ex)
            {
                log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString() + "::" + MethodBase.GetCurrentMethod().ToString());
                Log.addLog(log, LogType.ERROR, ex.Message);
                return 0;
            }

        }

        public void do_SendMessage(object objTh)
        {
            while (true)
            {
                if (Program.MessageStop == 1)
                    break;
                Thread.Sleep(1000);                
                try
                {
                    if(GetMsgID(7)==1)//MES下发打捆指令
                    {
                        Program.MessageFlg = 1;
                        Send_SignsMessage();
                        
                    }
                }
                catch (Exception ex)
                {
                    log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString() + "::" + MethodBase.GetCurrentMethod().ToString());
                    Log.addLog(log, LogType.ERROR, ex.Message);
                    Log.addLog(log, LogType.ERROR, ex.StackTrace);
                }
            }
        }
           
        public void Send_SignsMessage()
        {

            try
            {
                double MAXRECID = 0;             
                string sql = "select MAX(REC_ID) AS REC_ID from TLabelContent";
                DbDataReader dr = null;
                dr = tm.MultithreadDataReader(sql);
                while (dr.Read())
                {
                    if (dr["REC_ID"] != DBNull.Value)
                        MAXRECID = Convert.ToDouble(dr["REC_ID"].ToString());
                    else
                        return;
                }
                dr.Close();
                sql = string.Format("select SEQ_SEND,ID_LOT_PROD,ID_PART_LOT,NAME_PROD,WT_AVG_LEN_PROD,DES_FIPRO_SECTION,DIM_LEN,NUM_BAR from TLabelContent WHERE REC_ID={0}", MAXRECID);
                string SEQ_SEND = "",ID_LOT_PROD = "", NAME_PROD="", DES_FIPRO_SECTION="";    
                Int16 ID_PART_LOT = 0, NUM_BAR=0, NAME_PROD_NUM=0;
                float DIM_LEN = 0, WT_AVG_LEN_PROD=0;

                DataTable dt = tm.MultithreadDataTable(sql);
                for (int i = 0; i<dt.Rows.Count; i++)
                {
                    SEQ_SEND = dt.Rows[i]["SEQ_SEND"].ToString();
                    ID_LOT_PROD = dt.Rows[i]["ID_LOT_PROD"].ToString();
                    ID_PART_LOT = Int16.Parse(dt.Rows[i]["ID_PART_LOT"].ToString());
                    NAME_PROD = dt.Rows[i]["NAME_PROD"].ToString();
                    WT_AVG_LEN_PROD= Single.Parse(dt.Rows[i]["WT_AVG_LEN_PROD"].ToString());
                    DES_FIPRO_SECTION = dt.Rows[i]["DES_FIPRO_SECTION"].ToString();
                    DIM_LEN = Single.Parse(dt.Rows[i]["DIM_LEN"].ToString());                   
                    NUM_BAR= Int16.Parse(dt.Rows[i]["NUM_BAR"].ToString());                                         
                }
                string[] steeltype = { "槽", "角", "工", "球" };
                for (short i = 1; i <= 4; i++)
                {
                    if (NAME_PROD.IndexOf(steeltype[i - 1]) >= 0)
                    {
                        NAME_PROD_NUM = i;
                        break;
                    }
                }
                byte[] sendArray = Enumerable.Repeat((byte)0x0, 78).ToArray();
                byte[] byteArray0 = BitConverter.GetBytes(1);//2
                byte[] byteArray1 = Encoding.ASCII.GetBytes(SEQ_SEND);//4
                byte[] byteArray2 = Encoding.ASCII.GetBytes(ID_LOT_PROD);//9
                byte[] byteArray3 = BitConverter.GetBytes(ID_PART_LOT);//2
                byte[] byteArray4= BitConverter.GetBytes(NAME_PROD_NUM);//8
                byte[] byteArray5 = BitConverter.GetBytes(WT_AVG_LEN_PROD);//1
                byte[] byteArray6 = Encoding.ASCII.GetBytes(DES_FIPRO_SECTION);//8
                byte[] byteArray7= BitConverter.GetBytes(DIM_LEN); //2
                byte[] byteArray8 = BitConverter.GetBytes(NUM_BAR);//8


                Buffer.BlockCopy(byteArray0, 0, sendArray, 0, byteArray0.Length);
                Buffer.BlockCopy(byteArray1, 0, sendArray, 2, byteArray1.Length);
                Buffer.BlockCopy(byteArray2, 0, sendArray, 14, byteArray2.Length);
                Buffer.BlockCopy(byteArray3, 0, sendArray, 24, byteArray3.Length);
                Buffer.BlockCopy(byteArray4, 0, sendArray, 26, byteArray4.Length);
                Buffer.BlockCopy(byteArray5, 0, sendArray, 28, byteArray5.Length);
                Buffer.BlockCopy(byteArray6, 0, sendArray, 32, byteArray6.Length);
                Buffer.BlockCopy(byteArray7, 0, sendArray, 72, byteArray7.Length);
                Buffer.BlockCopy(byteArray8, 0, sendArray, 76, byteArray8.Length);

                   
                if (sendArray.Length > 0)
                    {
                        PLCSocketServer.senddata(sendArray);
                        string str = "二级发送消息到虎踞PLC"+ SEQ_SEND + " " + ID_LOT_PROD + " "  + ID_PART_LOT.ToString() + " " + DIM_LEN .ToString()+ " "+ NUM_BAR;
                        sql = string.Format("INSERT INTO SENDLOG(REC_CREATE_TIME,CONTENT) VALUES ('{0}','{1}')", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), str);
                        tm.MultithreadExecuteNonQuery(sql);
                    }
            
            }
            catch (Exception ex)
            {
                log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString() + "::" + MethodBase.GetCurrentMethod().ToString());
                Log.addLog(log, LogType.ERROR, ex.Message);
                Log.addLog(log, LogType.ERROR, ex.StackTrace);
            }
            
        }
                     
        public void RunSINGenerate()
        {
            try
            {      
                Thread thS = new Thread(new System.Threading.ParameterizedThreadStart(do_SendMessage));
                thS.Start(null);
                PLCSocketServer PLCServer = new PLCSocketServer();
                PLCServer.CreateSocket(localip,localportr);                             
            }
            catch (Exception ex)
            {
                log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString() + "::" + MethodBase.GetCurrentMethod().ToString());
                Log.addLog(log, LogType.ERROR, ex.Message);
                Log.addLog(log, LogType.ERROR, ex.StackTrace);
            }
            return;
        }      
   }     
}
