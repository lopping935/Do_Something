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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using SocketHelper;

namespace CoreAlgorithm.TaskManager
{
    
    public class CommMaster
    { 
        TasksManager tm;
        INIClass ini = new INIClass(System.AppDomain.CurrentDomain.BaseDirectory + "Config.ini");
        string localip = "", plcip = "", sprayip = "";//400PLC ip
        int localportr = 0, plcportr = 0, plcports = 0, sprayportr = 0, sprayports = 0;//400PLC端口
         public static SocketServer PLC_Server = null;
        YFHelper YF_Helper = new YFHelper();
        public CommMaster()
        {
            tm = new TasksManager();
        }

        public void do_SendMessage(object objTh)
        {

            while (true)
            {
               // TasksManager tm=new TasksManager();
                if (Program.MessageStop == 1)
                    break;
                Thread.Sleep(1000);
                try
                { 
                    if (Program.MessageFlg == 1)
                    {
                        string sql = string.Format("INSERT INTO RECVLOG(REC_CREATE_TIME,CONTENT) VALUES ('{0}','{1}')", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), "收到PLC请求数据信号1");
                        tm.MultithreadExecuteNonQuery(sql);
                        double MAXRECID = 0;
                        //string sql = "select MAX(rownumberf) AS REC_ID from TLabelContent WHERE IMP_FINISH=31 or IMP_FINISH=32 or IMP_FINISH=33";
                         sql = "select MAX(REC_ID) AS REC_ID from TLabelContent WHERE IMP_FINISH=31 or IMP_FINISH=32 or IMP_FINISH=33 or IMP_FINISH=55";
                        DbDataReader dr = null;
                        dr = tm.MultithreadDataReader(sql);
                        while (dr.Read())
                        {
                            if(dr["REC_ID"]!=DBNull.Value)
                            MAXRECID = Convert.ToDouble(dr["REC_ID"].ToString());
                        }
                        dr.Close();
                        int count = 0;
                        sql = string.Format("select count(*) as count from TLabelContent WHERE REC_ID>{0} AND IMP_FINISH=0", MAXRECID);
                        DataTable dt = tm.MultithreadDataTable(sql);
                        for (int i = 0; i < dt.Rows.Count; i++)
                            count = Convert.ToInt32(dt.Rows[i]["count"].ToString());

                        lock (Program.gllock)
                        {
                            if (count>15)
                            {
                                Program.MessageFlg = 2;
                                Send_SignsMessage();                                
                            }                      
                            else if(count <=15 && count > 0)
                            {
                                Program.MessageFlg = 4;
                                Send_SignsMessage();
                            }
                            else
                            {
                                //数据报警
                                sql = "update S_TFlag set Flag=0 where ID=2";
                                tm.MultithreadExecuteNonQuery(sql);
                                Program.MessageFlg = 3;
                                Send_SignsMessage();
                            }
                        
                        }
                    }
                    if (Program.MessageFlg == 11 )//|| Program.MessageFlg == 14
                    {
                        
                        lock (Program.gllock)
                        {
                            FormPrint PrintNow = new FormPrint();
                            string PrintNO = "";
                            if (Program.PrintNum == 1)
                                PrintNO = "Print1";
                            if (Program.PrintNum == 2)
                                PrintNO = "Print2";
                            PrintNow.button_handprinnt_Click(PrintNO);
                            //if (Program.MessageFlg == 14)
                            //{
                            //    if (Program.PrintNum == 1)
                            //    {
                            //        Program.PrintNum = 2;
                            //        PrintNO = "Print2";
                            //    }
                            //    else
                            //    {
                            //        Program.PrintNum = 1;
                            //        PrintNO = "Print1";
                            //    }
                            //}
                            if (Program.MessageFlg == 13)
                                Send_SignsMessage();
                            if (Program.MessageFlg == 12)
                                Send_SignsMessage();
                        }
                    }
                
                }
                catch (Exception ex)
                {
                    Program.MessageFlg = 23;
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
                TasksManager tm = new TasksManager();
                double MAXRECID = 0;// PLANIDNow = 0;                 
                string REC_ID = "", iface_id = "",FUN_NO = "", LotNo = "", XH = "";
                //string sql = "select MAX(rownumberf) AS REC_ID from TLabelContent WHERE IMP_FINISH=31 or IMP_FINISH=32 or IMP_FINISH=33";
                string sql = "select MAX(REC_ID) AS REC_ID from TLabelContent WHERE IMP_FINISH=31 or IMP_FINISH=32 or IMP_FINISH=33 or IMP_FINISH=55";
                
                byte[] sendArray = Enumerable.Repeat((byte)0x0, 148).ToArray();
                byte[] byteArray1 = BitConverter.GetBytes(Program.MessageFlg);
                Buffer.BlockCopy(byteArray1, 0, sendArray, 0, byteArray1.Length);
                DbDataReader dr = null;
                dr = tm.MultithreadDataReader(sql);
                while (dr.Read())
                {
                    if (dr["REC_ID"] != DBNull.Value)
                        MAXRECID = Convert.ToDouble(dr["REC_ID"].ToString());
                }
                dr.Close();
                sql = string.Format("select top 1 REC_ID,iface_id,FUN_NO,LotNo,XH from TLabelContent WHERE REC_ID>{0} AND IMP_FINISH=0 order by REC_ID ASC", MAXRECID);
               // sql = string.Format("select top 1 REC_ID,merge_sinbar,gk,heat_no,mtrl_no,spec,wegith,num_no,print_date,classes,sn_no from TLabelContent WHERE rownumberf>{0} AND IMP_FINISH=0 order by rownumberf ASC", MAXRECID);
                DataTable dt = tm.MultithreadDataTable(sql);
                for (int i = 0; i<dt.Rows.Count; i++)
                {
                    REC_ID = dt.Rows[i]["REC_ID"].ToString();
                    iface_id = dt.Rows[i]["iface_id"].ToString();
                    FUN_NO = dt.Rows[i]["FUN_NO"].ToString();
                    LotNo = dt.Rows[i]["LotNo"].ToString();
                    XH = dt.Rows[i]["XH"].ToString();
                    
                    byte[] byteArray2 = BitConverter.GetBytes(Program.PrintNum);
                    byte[] byteArray3 = Encoding.ASCII.GetBytes(REC_ID);
                    byte[] byteArray4 = Encoding.ASCII.GetBytes(iface_id);
                    byte[] byteArray5=  Encoding.ASCII.GetBytes(FUN_NO);
                    byte[] byteArray6 = Encoding.ASCII.GetBytes(LotNo);
                    byte[] byteArray7 = Encoding.ASCII.GetBytes(XH);
                    
                    Buffer.BlockCopy(byteArray2, 0, sendArray, 2, byteArray2.Length);
                    Buffer.BlockCopy(byteArray3, 0, sendArray, 4, byteArray3.Length);
                    Buffer.BlockCopy(byteArray4, 0, sendArray, 16, byteArray4.Length);
                    Buffer.BlockCopy(byteArray5, 0, sendArray, 28, byteArray5.Length);
                    Buffer.BlockCopy(byteArray6, 0, sendArray, 48, byteArray6.Length);
                    Buffer.BlockCopy(byteArray7, 0, sendArray, 98, byteArray7.Length);
                }
                if (sendArray.Length > 0)
                {
                    KeyValuePair<string, Socket> kvp = PLC_Server.dict.FirstOrDefault();
                    PLC_Server.SendToSomeone(sendArray, kvp.Key);
                    string logindexstr = "";
                    switch(Program.MessageFlg)
                    {
                        case 2:
                            logindexstr = "请求信息成功";
                            break;
                        case 3:
                            logindexstr = "无信息";
                            break;
                        case 4:
                            logindexstr = "请求信息成功但低于15条";
                            break;
                        case 12:
                            logindexstr = "打印完成";
                            break;
                        case 13:
                            logindexstr = "打印失败";
                            break;
                        default :
                            logindexstr = "错误信号";
                            break;

                    }
                    string str = "发送到PLC信号:" + logindexstr + Program.MessageFlg.ToString() + " " + iface_id;
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
        public void Date_Copy()
        {
            while(true)
            {
                Thread.Sleep(1000);
                DataTable dt = null;
                int count = 0;
                TasksManager tm = new TasksManager();
                int num_change = 0;
                try
                {
                    //增加数据
                    string sqltext = "select count(*) as count from READ_TABLE where flag='N'";
                    dt = tm.MultithreadDataTable(sqltext);
                    for (int i = 0; i < dt.Rows.Count; i++)
                        count = Convert.ToInt32(dt.Rows[i]["count"].ToString());
                    if(count>0)
                    {
                        count = 0;
                        string heatno = "";
                        sqltext = "select top 1* from READ_TABLE where flag='N'";
                        dt = tm.MultithreadDataTable(sqltext);
                        if (dt.Rows.Count != 0)
                            heatno = dt.Rows[0]["iface_id"].ToString();

                        sqltext = string.Format("insert into TLabelContent SELECT [FUN_NO] ,[STEEL_CODE_DESC] ,[SPEC_CP_DESC],[iface_id],[NUM],[LENGTH],[NET_WEIGHT],[LotNo] ,[XH] ,[HT_NO],[SCBZ] ,[MFL_DESC] ,[ProTime] ,[ItemPrint] ,[TaskNo] ,[CREATED_CLASS],0,'{0}','{0}',0 FROM READ_TABLE WHERE flag='N' ORDER BY id asc", DateTime.Now.ToString());
                        //sqltext = sqltext + ";update [YFDBBRobotData].[dbo].[TLabelContent] set rownumberf=row2 from (select ROW_NUMBER() over(order by merge_sinbar asc)row2, REC_ID from[YFDBBRobotData].[dbo].[TLabelContent])DETAIL_B14 where[TLabelContent].REC_ID = DETAIL_B14.REC_ID";
                        num_change = tm.MultithreadExecuteNonQuery(sqltext);

                        sqltext = string.Format("update READ_TABLE set flag='A' where id in(select top {0} id from READ_TABLE where flag='N')", num_change);
                        num_change = tm.MultithreadExecuteNonQuery(sqltext);
                        string str = "收到抛送数据:" +heatno + " " + num_change.ToString() + "条 ";// + PLClable.merge_sinbar;
                        sqltext = string.Format("INSERT INTO SENDLOG(REC_CREATE_TIME,CONTENT) VALUES ('{0}','{1}')", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), str);
                        tm.MultithreadExecuteNonQuery(sqltext);
                        
                        num_change = 0;
                    }
                }
                catch (Exception ex)
                {
                    string sql = "update S_TFlag set Flag=1 where ID=2";
                    tm.MultithreadExecuteNonQuery(sql);
                    string sqltext = string.Format("update READ_TABLE set flag='A' where flag='N'", DateTime.Now.ToString());
                    tm.MultithreadExecuteNonQuery(sqltext);
                    log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString() + "::" + MethodBase.GetCurrentMethod().ToString());
                    Log.addLog(log, LogType.ERROR, ex.Message);
                    Log.addLog(log, LogType.ERROR, ex.StackTrace);
                }
                try
                {   
                    //删除数据
                    string sqltext = "select count(*) as count from READ_TABLE where flag='D'";
                    dt = tm.MultithreadDataTable(sqltext);
                    for (int i = 0; i < dt.Rows.Count; i++)
                        count = Convert.ToInt32(dt.Rows[i]["count"].ToString());
                    if (count > 0)
                    {                        
                        string heatno = "";
                        sqltext = "select top 1* from READ_TABLE where flag='D'";
                        dt = tm.MultithreadDataTable(sqltext);
                        if (dt.Rows.Count != 0)
                            heatno = dt.Rows[0]["iface_id"].ToString();

                        sqltext =string.Format("delete from TLabelContent where iface_id in (select top {0} iface_id from READ_TABLE where flag='D')", count);
                        num_change = tm.MultithreadExecuteNonQuery(sqltext);

                        sqltext = string.Format("update READ_TABLE set flag='R' where id in(select top {0} id from READ_TABLE where flag='D')",count);//where flag='D'
                        int num_changed=tm.MultithreadExecuteNonQuery(sqltext);

                        if(num_change>0)
                        {
                            string str = "三级删除数据从:" + heatno +" "+num_change.ToString() + "条 ";// + PLClable.merge_sinbar;
                            sqltext = string.Format("INSERT INTO SENDLOG(REC_CREATE_TIME,CONTENT) VALUES ('{0}','{1}')", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), str);
                            tm.MultithreadExecuteNonQuery(sqltext);
                        }      
                        num_change = 0;
                        count = 0;
                    }
                    //生成历史记录
                    string sql = "select MAX(REC_ID) AS REC_ID from TLabelContent WHERE IMP_FINISH=31 or IMP_FINISH=32 or IMP_FINISH=33  or IMP_FINISH=55";
                    DbDataReader dr = null;
                    double MAXRECID = 0;// PLANIDNow = 0;
                    dr = tm.MultithreadDataReader(sql);
                    while (dr.Read())
                    {
                        if (dr["REC_ID"] != DBNull.Value)
                            MAXRECID = Convert.ToDouble(dr["REC_ID"].ToString());        
                    }
                    dr.Close();
                    sqltext = string.Format("select count(*) as count from TLabelContent where REC_ID<{0} ", MAXRECID);
                    dt = tm.MultithreadDataTable(sqltext);
                    for (int i = 0; i < dt.Rows.Count; i++)
                        count = Convert.ToInt32(dt.Rows[i]["count"].ToString());
                    if (count > 20)
                    {
                        sqltext =string.Format("insert into HLabelContent select top {0} * from TLabelContent where REC_ID!=0 order by REC_ID asc", count - 20);
                        tm.MultithreadExecuteNonQuery(sqltext);
                        sqltext =string.Format("delete from TLabelContent where REC_ID in(select top {0} REC_ID from TLabelContent where REC_ID!=0 order by REC_ID asc)", count - 20);
                        //sqltext = sqltext + ";update [YFDBBRobotData].[dbo].[TLabelContent] set rownumberf=row2 from (select ROW_NUMBER() over(order by merge_sinbar asc)row2, REC_ID from[YFDBBRobotData].[dbo].[TLabelContent])DETAIL_B14 where[TLabelContent].REC_ID = DETAIL_B14.REC_ID";
                        tm.MultithreadExecuteNonQuery(sqltext);
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

        /// <summary>
        /// </summary>
        /// <param name="serverModlue"></param>
        public void RunSINGenerate()
        {
            try
            {      
                string sql = "SELECT ACQUISITIONCONFIG_ID,DATAACQUISITION_IP,DATAACQUISITION_PORTR,DATAACQUISITION_PORTS FROM ACQUISITIONCONFIG where ACQUISITIONCONFIG_ID=1 or ACQUISITIONCONFIG_ID=4 or ACQUISITIONCONFIG_ID=15";// ";
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
				sql = string.Format("update TLabelContent set rownumberf=row2 from (select ROW_NUMBER() over(order by REC_ID asc)row2, REC_ID from TLabelContent)DETAIL_B14 where TLabelContent.REC_ID = DETAIL_B14.REC_ID");
                tm.MultithreadExecuteNonQuery(sql);
				
                Thread thS = new Thread(new System.Threading.ParameterizedThreadStart(do_SendMessage));
                thS.Start(null);
                Thread Datecoyt = new Thread(Date_Copy);
                Datecoyt.Start();
                PLC_Server = new SocketServer(localip, localportr);
                PLC_Server.StarServer(YF_Helper.Recv);

                //PLCSocketServer PLCServer = new PLCSocketServer();
                //PLCServer.CreateSocket(localip,localportr);                
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


    public class DllInvoke
    {
        #region Win API
        [DllImport("kernel32.dll")]
        private extern static IntPtr LoadLibrary(string path);
        [DllImport("kernel32.dll")]
        private extern static IntPtr GetProcAddress(IntPtr lib, string funcName);
        [DllImport("kernel32.dll")]
        private extern static bool FreeLibrary(IntPtr lib);
        #endregion
        private IntPtr hLib;
        public DllInvoke(String DLLPath)
        {
            hLib = LoadLibrary(DLLPath);
        }

        public bool IDisposable()
        {
            return FreeLibrary(hLib);
        }
        //将要执行的函数转换为委托
        public Delegate Invoke(string APIName, Type t)
        {
            IntPtr api = GetProcAddress(hLib, APIName);
            return (Delegate)Marshal.GetDelegateForFunctionPointer(api, t);
        }
    }

    
}
