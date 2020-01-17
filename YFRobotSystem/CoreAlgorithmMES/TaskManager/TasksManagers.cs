using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using SQLPublicClass;
using System.Data;
using System.Threading;
using System.Reflection;

namespace CoreAlgorithm.TaskManager
{
    public class TasksManager
    {
        DbHelper db;
        IniSqlConfigInfo inisql = new IniSqlConfigInfo(System.AppDomain.CurrentDomain.BaseDirectory + "Config.ini");
        public TasksManager()
        {
            db = new DbHelper(inisql.GetConnectionString("SysSQL"));
        }

        public Dictionary<int, TaskModule> GetTaskClass()
        {
            lock (Program.obj)
            {
                Dictionary<int, TaskModule> ListTc = new Dictionary<int, TaskModule>();
                try
                {
                    string sql = "SELECT [Task_id],[Task_Name]" +
                                ",[Task_CreatTime],[Task_CreatUser],[Task_Stats],[Task_TimeSpace] FROM [Task] where " +
                                "Task_Stats='自动' and Task_Deleted='false'";

                    DataTable dt = db.ExecuteDataTable(db.GetSqlStringCommond(sql));

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        TaskModule tc = new TaskModule();
                        tc.m_Taskid = Convert.ToInt16(dr["Task_id"]);
                        tc.m_NumIndex = 0;
                        tc.m_TimeSpace = Convert.ToInt16(dr["Task_TimeSpace"]);
                        tc.m_TaskName = dr["Task_Name"].ToString();
                       
                        tc.m_TaskCreatTime = DateTime.Parse(dr["Task_CreatTime"].ToString());
                        tc.m_TaskCreatUser = dr["Task_CreatUser"].ToString();
                        tc.m_TaskStats = dr["Task_Stats"].ToString();
                       
                        ListTc.Add(tc.m_Taskid, tc);
                    }
                }
                catch (Exception ex)
                {
                    log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString() + "::" + MethodBase.GetCurrentMethod().ToString());
                    Log.addLog(log, LogType.ERROR, ex.Message);
                }
                return ListTc;
            }
        }

        public int MultithreadExecuteNonQuery(string sql)
        {
            lock (Program.obj)
            {
                return db.ExecuteNonQuery(db.GetSqlStringCommond(sql));
            }
        }
        public DbDataReader MultithreadDataReader(string sql)
        {
            lock (Program.obj)
            {
                return db.ExecuteReader(db.GetSqlStringCommond(sql));
            }
        }
        public DataTable MultithreadDataTable(string sql)
        {
            lock (Program.obj)
            {
                return db.ExecuteDataTable(db.GetSqlStringCommond(sql));
            }
        }

        public DataTable MultithreadDataTable_Prc(string PrcName)
        {
            lock (Program.obj)
            {
                return db.ExecuteDataTable(db.GetStoredProcCommond(PrcName));
            }
        }

        public object MultithreadGetTimeSpace()
        {
            lock (Program.obj)
            {
                return db.ExecuteScalar(db.GetSqlStringCommond("SELECT top(1) [OPCDataAcquisition_UpdataRate]/1000 FROM [OPCAcquisitionConfig]"));
            }
        }


        internal int MultithreadInsertAlarm(int TaskID, string AlarmLevel, DateTime AlarmTime, string DispatchText)
        {
            lock (Program.obj)
            {
                string sql = "UPDATE [TaskWarning] SET [TaskWaring_del] = 'true' where [TaskWaring_del] = 'false' and  Task_id=" + TaskID;
                db.ExecuteNonQuery(db.GetSqlStringCommond(sql));
                sql = "INSERT INTO [TaskWarning] ([Task_id] ,[Forecast_time] ,[Warning_time] ,[Warning_type] ,[Warning_txt] ,[TaskWaring_del]) VALUES({0},'{1}','{2}','{3}','{4}','false')";
                sql = string.Format(sql, TaskID, DateTime.Now, AlarmTime, AlarmLevel, DispatchText);
                return db.ExecuteNonQuery(db.GetSqlStringCommond(sql));
            }
        }

        internal int MultithreadCancleAlarm(int TaskID)
        {
            lock (Program.obj)
            {
                string sql = "UPDATE [TaskWarning] SET [TaskWaring_del] = 'true' where [TaskWaring_del] = 'false' and  Task_id=" + TaskID;
                return db.ExecuteNonQuery(db.GetSqlStringCommond(sql));
            }
        }

        //public sealed class TimeSpanWaitor
        //{
        //    public TimeSpanWaitor(int minwaitmillseconds, int maxwaitmillsecondes)
        //    { 
        //        g_AsyncObject = new IntLock();
        //        g_DefaultWaitTime = new TimeSpan(0, 0, 1);
        //        int min = minwaitmillseconds;
        //        if (min < 0)
        //            min = 10;
        //        int max = maxwaitmillsecondes;
        //        if (max < 0)
        //            max = 100;
        //        if (min > max)
        //        {
        //            int x = min;
        //            min = max;
        //            max = x;
        //        }
        //        if (min == max)
        //        {
        //            min = 10; 
        //            max = 100;
        //        }
        //        g_MaxWaitMillSeconds = max;
        //        g_MinWaitMillSeconds = min;
        //        g_WaitTimeDom = new Random();
        //    } 
            
        //    public TimeSpanWaitor(): this(DefaultMinWaitTimeMillSeconds, DefaultMaxWaitTimeMillSeconds)
        //    {
            
        //    }
        //    #region 公有常数
            
        //    public const int DefaultMaxWaitTimeMillSeconds = 100;
        //    public const int DefaultMinWaitTimeMillSeconds = 10;
        //    #endregion     
        //    #region 私有常量 
        //    private IntLock g_AsyncObject;
        //    private TimeSpan g_DefaultWaitTime;
        //    private Random g_WaitTimeDom = null;
        //    private int g_MaxWaitMillSeconds = 0;
        //    private int g_MinWaitMillSeconds = 0;
        //    #endregion
        //    #region 私有方法
            
        //    /// <summary>
        //    /// 尝试锁定
        //    /// </summary>
        //    /// <param name="onenter">成功锁定时调用该回调:返回True指示退出获取锁定,否则继续下一次获取锁定</param>
        //    /// <returns>尝试结果</returns>
        //    private PerWaitEnum TryEnter(Func<bool> onenter)
        //    {           
        //        bool success = g_AsyncObject.Lock(); 
        //        if (success) 
        //        { 
        //            PerWaitEnum r = PerWaitEnum.SuccessAndContinue; 
        //            Exception err = null; 
        //            try 
        //            { 
        //                if (onenter()) 
        //                    r = PerWaitEnum.SuccessAndExists; 
        //            } 
        //            catch (Exception e) 
        //            { 
        //                err = e; 
        //            } 
        //            finally 
        //            { 
        //                g_AsyncObject.UnLock(); 
        //            } 
        //            if (err != null) 
        //                throw err; 
        //            return r; 
        //        } 
        //        return PerWaitEnum.Fail; 
        //    } 
            
        //    /// <summary> 
        //    /// 等待 
        //    /// </summary> 
        //    /// <param name="waittimeout">等待超时值</param>
        //    /// <param name="dt">上次等待时间</param> 
        //    /// <returns>返回True指示未超时</returns>          
        //    private bool WaitTime(ref TimeSpan waittimeout, ref DateTime dt) 
        //    { 
        //        if (waittimeout == TimeSpan.MaxValue)
        //        {
        //            Thread.Sleep(g_WaitTimeDom.Next(g_MinWaitMillSeconds, g_MaxWaitMillSeconds));
        //            dt = DateTime.Now;
        //            return true;
        //        }
        //        else if (waittimeout == TimeSpan.MinValue)
        //        {
        //            dt = DateTime.Now;
        //            return false;
        //        }
        //        else if (waittimeout == TimeSpan.Zero)
        //        {
        //            dt = DateTime.Now;
        //            return false;
        //        }
        //        else
        //        {
        //            Thread.Sleep(g_WaitTimeDom.Next(g_MinWaitMillSeconds, g_MaxWaitMillSeconds));
        //            waittimeout -= GetNowDateTimeSpan(ref dt);
        //            return (waittimeout.Ticks > 0);
        //        }
        //    }
        //    /// <summary>
        //    /// 计算此时同tp的时间差,同时tp返回此时时间         
        //    /// </summary>        
        //    /// <param name="tp">上次等待时间,返回此时</param>
        //    /// <returns>tp同此时的时间差</returns>
        //    private TimeSpan GetNowDateTimeSpan(ref DateTime tp)
        //    {
        //        DateTime kk = tp;
        //        tp = DateTime.Now;
        //        return tp.Subtract(kk);
        //    }
        //    #endregion
        //    #region 公有方法
            
        //    /// <summary>
        //    /// 等待指定的时间:timeout
        //    /// </summary>
        //    /// <param name="timeout">等待超时时间:该值=TimeSpan.MaxValue边示无期限的等待</param>
        //    /// <param name="onenter">当每次获得等待锁时都调用,返回True表示退出等待,否则再次等待锁,直到超时</param>
        //    /// <returns>True表示成功等待到锁并且onenter函数返回True,False:表示等待超时</returns>
        //    public bool WaitForTime(TimeSpan timeout, Func<bool> onenter)
        //    {
        //        TimeSpan tmout = timeout;
        //        DateTime n = DateTime.Now;
        //        PerWaitEnum r = TryEnter(onenter);
        //        while (r != PerWaitEnum.SuccessAndExists)
        //        {
        //            if (!WaitTime(ref tmout, ref n))
        //            break;
        //            r = TryEnter(onenter);
        //        }
        //        return r == PerWaitEnum.SuccessAndExists;
        //    }
        //    #endregion
        //}
        //internal enum PerWaitEnum
        //{
        //    SuccessAndExists,
        //    SuccessAndContinue,
        //    Fail
        //}


    }
}
