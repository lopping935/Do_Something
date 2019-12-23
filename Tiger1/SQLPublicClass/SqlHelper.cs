using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using SQLPublicClass;
using System.Data;
using System.Threading;
using System.Reflection;

namespace SQLPublicClass
{
    public class SqlHelper
    {
        DbHelper db;
        IniSqlConfigInfo inisql = new IniSqlConfigInfo(System.AppDomain.CurrentDomain.BaseDirectory + "Config.ini");
        public SqlHelper()
        {
            db = new DbHelper(inisql.GetConnectionString("SysSQL"));
        }
       
        public int MultithreadExecuteNonQuery(string sql)
        {
            
                return db.ExecuteNonQuery(db.GetSqlStringCommond(sql));
           
        }
        public DbDataReader MultithreadDataReader(string sql)
        {
            
                return db.ExecuteReader(db.GetSqlStringCommond(sql));
            
        }
        public DataTable MultithreadDataTable(string sql)
        {
           
                return db.ExecuteDataTable(db.GetSqlStringCommond(sql));
            
        }

        public DataTable MultithreadDataTable_Prc(string PrcName)
        {
                return db.ExecuteDataTable(db.GetStoredProcCommond(PrcName));
        }
      
    }
}
