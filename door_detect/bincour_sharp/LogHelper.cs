using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace logtest
{
    public class LogHelper
    {
        private LogHelper()
        {

        }

        public static readonly log4net.ILog loginfo = log4net.LogManager.GetLogger("loginfo");

        public static readonly log4net.ILog logerror = log4net.LogManager.GetLogger("logerror");

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
            }
        }

        public static void WriteLog(string info, Exception se)
        {
            if (logerror.IsErrorEnabled)
            {
                logerror.Error(info, se);
            }
        }
        public static void Dele_LOGFile()
        {
            DirectoryInfo dir = new DirectoryInfo(@"./log/");
            foreach (FileInfo d in dir.GetFiles("*.LOG"))
            {

                Console.WriteLine(d.Name);
                var name = d.Name;
                if (name.Length != "AGFishlog-2020-06-14.LOG".Length)
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
