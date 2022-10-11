using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using log4net.Util;
using log4net.Layout;
using log4net.Core;
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4App.config", ConfigFileExtension = "config", Watch = true)]
namespace GH_Utlis
{
    
    public class LogHelper
    {
        private LogHelper()
        {

        }

        public static readonly log4net.ILog loginfo = log4net.LogManager.GetLogger("mainlog");//获取一个日志记录器 记录程序运行过程
        public static readonly log4net.ILog logerror = log4net.LogManager.GetLogger("errlog");//获取一个日志记录器 记录程序运行过程中的错误
        public static readonly log4net.ILog csvlog = log4net.LogManager.GetLogger("datalog");//获取一个日志记录器 记录程序运行过程的数据

        public static void SetConfig()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public static void SetConfig(FileInfo configFile)
        {
            log4net.Config.XmlConfigurator.Configure(configFile);
        }

        public static void WriteLog(string info)
        {
            if (loginfo.IsInfoEnabled)
            {
                loginfo.Info(info);
               // loginfo.
            }
        }



        public static void WriteLog(string info, Exception se)
        {
            if (logerror.IsErrorEnabled)
            {
                logerror.Error(info, se);
            }
        }
        public static void WriteCsv(string info)
        {
            if (csvlog.IsErrorEnabled)
            {
                csvlog.Info(info);
            }
        }
        public static void Dele_LOGFile()
        {
            DirectoryInfo dir = new DirectoryInfo(@"./log/");
            foreach (FileInfo d in dir.GetFiles("*.LOG"))
            {

                Console.WriteLine(d.Name);
                var name = d.Name;
                if (name.Length != "Autosteel-2020-06-14.LOG".Length)
                {
                    continue;
                }
                var ss = name.Substring(10, 10).Split('-');
                int year = int.Parse(ss[0]);
                int month = int.Parse(ss[1]);
                int day = int.Parse(ss[2]);
                var date = new DateTime(year, month, day);
                TimeSpan ts = DateTime.Now - date;
                if (ts.Days >= 20)
                {                  
                    File.Delete(d.FullName);
                }
            }
        }
    }
    
}
