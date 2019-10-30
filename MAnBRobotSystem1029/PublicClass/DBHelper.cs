using System;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.Configuration;

namespace PublicClass
{
    public class DbHelper
    {

        //private static string dbProviderName = System.Configuration.ConfigurationManager.AppSettings["DbHelperProvider"];
        //private static string dbConnectionString = System.Configuration.ConfigurationManager.AppSettings["DbHelperConnectionString"];


        private static string dbProviderName = "System.Data.OracleClient";
        private static string dbConnectionString = "User Id=IMAGE;Password=6923263;Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.0.115)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=ORCL)))";
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

        public OracleCommand GetStoredProcCommond(string storedProcedure,string [] sp)
        {OracleCommand dbCommand = (OracleCommand)connection.CreateCommand();
            dbCommand.CommandText = storedProcedure;
            if (sp != null)
            //dbCommand.Parameters.Add(sp);
            {
                dbCommand.Parameters.Add("OrderTimeFrom", OracleType.Char).Direction = ParameterDirection.Input;
                dbCommand.Parameters["OrderTimeFrom"].Value =sp[0];
                dbCommand.Parameters.Add("OrderTimeTo", OracleType.Char).Direction = ParameterDirection.Input;
                dbCommand.Parameters["OrderTimeTo"].Value = sp[1];
            }
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
            //cmd.Connection.ConnectionString = "Data Source=Localhost;Initial Catalog=SinteringProControl;User ID=sa; password=6923263;Enlist=true;Pooling=true;Connection Timeout=150;Max Pool Size=300;Min Pool Size=0;Connection Lifetime=300;packet size=1000";
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

        /*public DbDataReader ExecuteReader(DbCommand cmd)
        {
            if (cmd.Connection.State == ConnectionState.Open)
                cmd.Connection.Close();
            //cmd.Connection.ConnectionString = "Data Source=Localhost;Initial Catalog=SinteringProControl;User ID=sa; password=6923263;Enlist=true;Pooling=true;Connection Timeout=30;Max Pool Size=300;Min Pool Size=0;Connection Lifetime=300;packet size=1000";
            //if (cmd.Connection.State == ConnectionState.Closed) 
                cmd.Connection.Open();
            DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            //cmd.Connection.Close();
            return reader;
        }*/
        public DbDataReader ExecuteReader(DbCommand cmd)
        {
            if (cmd.Connection.State == ConnectionState.Open)
                cmd.Connection.Close();
            //cmd.Connection.ConnectionString = "Data Source=Localhost;Initial Catalog=SinteringProControl;User ID=sa; password=6923263;Enlist=true;Pooling=true;Connection Timeout=30;Max Pool Size=300;Min Pool Size=0;Connection Lifetime=300;packet size=1000";
            //if (cmd.Connection.State == ConnectionState.Closed) 
            cmd.Connection.Open();
            DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            //cmd.Connection.Close();
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
            /*if (cmd.Connection.State == ConnectionState.Closed)
                //cmd.Connection.Close();
            cmd.Connection.Open();
            int ret = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return ret;*/
        }
        public int ExecuteNonQuery_Prc(OracleCommand cmd)
        {
            if (cmd.Connection.State == ConnectionState.Open)
                cmd.Connection.Close();
            cmd.Connection.Open();
            int ret = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return ret;
            /*if (cmd.Connection.State == ConnectionState.Closed)
                //cmd.Connection.Close();
            cmd.Connection.Open();
            int ret = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return ret;*/
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
            //cmd.Connection.Close();
            if (cmd.Connection.State == ConnectionState.Open)
                cmd.Connection.Close();
            //cmd.Connection.ConnectionString = "Data Source=Localhost;Initial Catalog=SinteringProControl;User ID=sa; password=6923263;Enlist=true;Pooling=true;Connection Timeout=30;Max Pool Size=300;Min Pool Size=0;Connection Lifetime=300;packet size=1000";
            //if (cmd.Connection.State == ConnectionState.Closed) 
            cmd.Connection.Open();
            cmd.Connection = t.DbConnection;
            cmd.Transaction = t.DbTrans;            
            DbDataReader reader = cmd.ExecuteReader();
            //DataTable dt = new DataTable();
            //t.Commit();
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
