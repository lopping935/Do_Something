using System;
using System.Data;
using System.Data.Common;
using System.Configuration;

namespace SQLPublicClass
{
    public class DbHelper
    {

        //private static string dbProviderName = System.Configuration.ConfigurationManager.AppSettings["DbHelperProvider"];
        //private static string dbConnectionString = System.Configuration.ConfigurationManager.AppSettings["DbHelperConnectionString"];


        private static string dbProviderName = "System.Data.SqlClient";
        private static string dbConnectionString = "Data Source=.;Initial Catalog=Tiger;User ID=sa; Password=6923263;Enlist=true;Pooling=true;Connection Timeout=30;Max Pool Size=300;Min Pool Size=0;Connection Lifetime=300;packet size=1000";
        public DbConnection connection;

        public DbHelper()
        {
            this.connection = CreateConnection(DbHelper.dbConnectionString);
        }
        public DbHelper(string _connectionString)
        {
            dbConnectionString = _connectionString;
            this.connection = CreateConnection(dbConnectionString);
        }
        public static DbConnection CreateConnection()
        {
            DbProviderFactory dbfactory = DbProviderFactories.GetFactory(DbHelper.dbProviderName);
            DbConnection dbconn = dbfactory.CreateConnection();
            dbconn.ConnectionString = DbHelper.dbConnectionString;
            return dbconn;
        }
        public static DbConnection CreateConnection(string connectionString)
        {
            DbProviderFactory dbfactory = DbProviderFactories.GetFactory(DbHelper.dbProviderName);
            DbConnection dbconn = dbfactory.CreateConnection();
            dbconn.ConnectionString = connectionString;
            return dbconn;
        }

        public DbCommand GetStoredProcCommond(string storedProcedure)
        {
            DbCommand dbCommand = connection.CreateCommand();
            dbCommand.CommandText = storedProcedure;
            dbCommand.CommandType = CommandType.StoredProcedure;
            return dbCommand;
        }
        public DbCommand GetSqlStringCommond(string sqlQuery)
        {
            DbCommand dbCommand = connection.CreateCommand();
            dbCommand.CommandText = sqlQuery;
            dbCommand.CommandType = CommandType.Text;
            return dbCommand;
        }

        #region  //增加参数
        public void AddParameterCollection(DbCommand cmd, DbParameterCollection dbParameterCollection)
        {
            foreach (DbParameter dbParameter in dbParameterCollection)
            {
                cmd.Parameters.Add(dbParameter);
            }
        }
        public void AddOutParameter(DbCommand cmd, string parameterName, DbType dbType, int size)
        {
            DbParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = dbType;
            dbParameter.ParameterName = parameterName;
            dbParameter.Size = size;
            dbParameter.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(dbParameter);
        }
        public void AddInParameter(DbCommand cmd, string parameterName, DbType dbType, object value)
        {
            DbParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = dbType;
            dbParameter.ParameterName = parameterName;
            dbParameter.Value = value;
            dbParameter.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(dbParameter);
        }
        public void AddReturnParameter(DbCommand cmd, string parameterName, DbType dbType)
        {
            DbParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = dbType;
            dbParameter.ParameterName = parameterName;
            dbParameter.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(dbParameter);
        }
        public DbParameter GetParameter(DbCommand cmd, string parameterName)
        {
            return cmd.Parameters[parameterName];
        }

        #endregion

        #region  //执行
        public DataSet ExecuteDataSet(DbCommand cmd)
        {
            DbProviderFactory dbfactory = DbProviderFactories.GetFactory(DbHelper.dbProviderName);
            DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();
            dbDataAdapter.SelectCommand = cmd;
            DataSet ds = new DataSet();
            dbDataAdapter.Fill(ds);
            return ds;
        }

        public DataTable ExecuteDataTable(DbCommand cmd)
        {

            if (cmd.Connection.State == ConnectionState.Open)
                cmd.Connection.Close();
            //cmd.Connection.ConnectionString = "Data Source=Localhost;Initial Catalog=SinteringProControl;User ID=sa; Password=sws;Enlist=true;Pooling=true;Connection Timeout=150;Max Pool Size=300;Min Pool Size=0;Connection Lifetime=300;packet size=1000";
            cmd.Connection.Open();
            DbProviderFactory dbfactory = DbProviderFactories.GetFactory(DbHelper.dbProviderName);
            DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();
            dbDataAdapter.SelectCommand = cmd;
            DataTable dataTable = new DataTable();
            dbDataAdapter.Fill(dataTable);
            //if (cmd.Connection.State == ConnectionState.Open)
                //cmd.Connection.Close();
            return dataTable;
        }

        public DbDataReader ExecuteReader(DbCommand cmd)
        {
            if (cmd.Connection.State == ConnectionState.Open)
                cmd.Connection.Close();
            
                cmd.Connection.Open();
            DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
         
            return reader;
        }
        public int ExecuteNonQuery(DbCommand cmd)
        {
            if (cmd.Connection.State == ConnectionState.Open)
                cmd.Connection.Close();
            cmd.Connection.Open();
            int ret = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return ret;
            
        }

        public object ExecuteScalar(DbCommand cmd)
        {
            if (cmd.Connection.State == ConnectionState.Open)
                cmd.Connection.Close();
            cmd.Connection.Open();
            object ret = cmd.ExecuteScalar();
            cmd.Connection.Close();
            return ret;
        }
        
        #endregion

        #region  //执行事务
        public DataSet ExecuteDataSet(DbCommand cmd,Trans t)
        {
            cmd.Connection = t.DbConnection;
            cmd.Transaction = t.DbTrans;
            DbProviderFactory dbfactory = DbProviderFactories.GetFactory(DbHelper.dbProviderName);
            DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();
            dbDataAdapter.SelectCommand = cmd;
            DataSet ds = new DataSet();
            dbDataAdapter.Fill(ds);
            return ds;
        }

        public DataTable ExecuteDataTable(DbCommand cmd, Trans t)
        {
            cmd.Connection = t.DbConnection;
            cmd.Transaction = t.DbTrans;
            DbProviderFactory dbfactory = DbProviderFactories.GetFactory(DbHelper.dbProviderName);
            DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();
            dbDataAdapter.SelectCommand = cmd;
            DataTable dataTable = new DataTable();
            dbDataAdapter.Fill(dataTable);
            return dataTable;
        }

        public DbDataReader ExecuteReader(DbCommand cmd, Trans t)
        {
            cmd.Connection.Close();
            cmd.Connection = t.DbConnection;
            cmd.Transaction = t.DbTrans;            
            DbDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();            
            return reader;
        }
        public int ExecuteNonQuery(DbCommand cmd, Trans t)
        {
            cmd.Connection.Close();
            cmd.Connection = t.DbConnection;
            cmd.Transaction = t.DbTrans;  
            int ret = cmd.ExecuteNonQuery();            
            return ret;
        }

        public object ExecuteScalar(DbCommand cmd, Trans t)
        {
            cmd.Connection.Close();
            cmd.Connection = t.DbConnection;
            cmd.Transaction = t.DbTrans;  
            object ret = cmd.ExecuteScalar();            
            return ret;
        }
        #endregion

        #region sqlhelper
        DbHelper db;
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
        #endregion
    }

    public class Trans : IDisposable
    {
        private DbConnection conn;
        private DbTransaction dbTrans;
        public DbConnection DbConnection
        {
            get { return this.conn; }
        }
        public DbTransaction DbTrans
        {
            get { return this.dbTrans; }
        }

        public Trans()
        {
            conn = DbHelper.CreateConnection();
            conn.Open();
            dbTrans = conn.BeginTransaction();
        }
        public Trans(string connectionString)
        {
            conn = DbHelper.CreateConnection(connectionString);
            conn.Open();
            dbTrans = conn.BeginTransaction();
        }
        public void Commit()
        {
            dbTrans.Commit();
            this.Colse();
        }

        public void RollBack()
        {
            dbTrans.Rollback();
            this.Colse();
        }

        public void Dispose()
        {
            this.Colse();
        }

        public void Colse()
        {
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
        }
    }
}
