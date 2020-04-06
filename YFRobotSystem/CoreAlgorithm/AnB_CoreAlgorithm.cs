using System;
using System.Data;
using System.ServiceProcess;
using System.Data.Common;
using SQLPublicClass;
using CoreAlgorithm.TaskManager;

namespace CoreAlgorithm
{
    public partial class AnB_CoreAlgorithm : ServiceBase
    {
        #region
        //private System.Threading.Timer  ForecastTimer; 
        public  static string           ConnectionStr ;       
        //TaskForecast tf;
        TasksManager tm;
        IniSqlConfigInfo inisql;
        DbHelper dbTemp;
        #endregion


        public AnB_CoreAlgorithm()
        {
            InitializeComponent();
            //inisql = new IniSqlConfigInfo(System.AppDomain.CurrentDomain.BaseDirectory + "Config.ini");
            //ConnectionStr= inisql.GetConnectionString("SysSQL");
            //tf = new TaskForecast();
            //dbTemp = new DbHelper(inisql.GetConnectionString("SysSQL"));
        }

        protected override void OnStart(string[] args)
        //public void OnStart()     
        {
            tm = new TasksManager();
           //DataTable dt = new DataTable("MyDT");
           int SystemRun = 0;

          
               
               //tm.MultithreadExecuteNonQuery(sql);
                CommMaster cm = new CommMaster();            
                cm.RunSINGenerate();
                return;
           
           
       }

        protected override void OnStop()
        {
            //ForecastTimer.Dispose();
            //UpdataTaskTimer.Dispose();
            //string sql = "UPDATE SYSPARAMETER SET PARAMETER_VALUE=3 where PARAMETER_ID=1";
            //tm.MultithreadExecuteNonQuery(sql);
            Program.MessageStop = 1;
        }
    }
}
