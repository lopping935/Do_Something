using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using SQLPublicClass;
using System.Data;
using System.Threading;
using System.Reflection;
using record_data;

namespace DBTaskHelper
{
    public class dbTaskHelper
    {
        DbHelper db;
        object obj = new object();
        public static string constring = "Data Source=.;Initial Catalog=Agfish;User ID=sa; Password=6923263;Enlist=true;Pooling=true;Max Pool Size=300;Min Pool Size=0;Connection Lifetime=300;packet size=1000";
        public dbTaskHelper()
        {
            db = new DbHelper(constring);
        }
        public int MultithreadExecuteNonQuery(string sql)
        {
            lock (obj)
            {
                return db.ExecuteNonQuery(db.GetSqlStringCommond(sql));
            }
        }
        public DbDataReader MultithreadDataReader(string sql)
        {
            lock (obj)
            {
                return db.ExecuteReader(db.GetSqlStringCommond(sql));
            }
        }
        public DataTable MultithreadDataTable(string sql)
        {
            lock (obj)
            {
                return db.ExecuteDataTable(db.GetSqlStringCommond(sql));
            }
        }

        public DataTable MultithreadDataTable_Prc(string PrcName)
        {
            lock (obj)
            {
                return db.ExecuteDataTable(db.GetStoredProcCommond(PrcName));
            }
        }

        public object MultithreadGetTimeSpace()
        {
            lock (obj)
            {
                return db.ExecuteScalar(db.GetSqlStringCommond("SELECT top(1) [OPCDataAcquisition_UpdataRate]/1000 FROM [OPCAcquisitionConfig]"));
            }
        }


        internal int MultithreadInsertAlarm(int TaskID, string AlarmLevel, DateTime AlarmTime, string DispatchText)
        {
            lock (obj)
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
            lock (obj)
            {
                string sql = "UPDATE [TaskWarning] SET [TaskWaring_del] = 'true' where [TaskWaring_del] = 'false' and  Task_id=" + TaskID;
                return db.ExecuteNonQuery(db.GetSqlStringCommond(sql));
            }
        }

    }
}
