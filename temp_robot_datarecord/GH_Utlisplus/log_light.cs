using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vision_Utlis
{

        public class LogManager
        {
            private  string logPath = string.Empty;
            /// <summary>
            /// 保存日志的文件夹
            /// </summary>
            public  string LogPath
            {
                get
                {
                    if (logPath == string.Empty)
                    {

                        logPath = AppDomain.CurrentDomain.BaseDirectory;

                    }
                    return logPath;
                }
                set { logPath = value; }
            }

            private  string logFielPrefix = string.Empty;
            /// <summary>
            /// 日志文件前缀
            /// </summary>
            public  string LogFielPrefix
            {
                get { return logFielPrefix; }
                set { logFielPrefix = value; }
            }

            /// <summary>
            /// 写日志
            /// </summary>
            public void WriteLog(string logFile, string msg)
            {
                try
                {
                    System.IO.StreamWriter sw = System.IO.File.AppendText(
                        LogPath + LogFielPrefix + logFile + " " +
                        DateTime.Now.ToString("yyyyMMdd") + ".Log"
                        );
                    sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss: ") + msg);
                    sw.Close();
                }
                catch(Exception e)
                {

                }
            }
        public void WriteLogcsv(string logFile, string msg, string head)
        {
            try
            {
                string filename = LogPath + LogFielPrefix + logFile + " " + DateTime.Now.ToString("yyyyMMdd") + ".csv";
                if (!File.Exists(filename))
                {
                    using (StreamWriter sw = new StreamWriter(filename, true, System.Text.Encoding.Default))
                    {
                        sw.WriteLine(head);
                    }
                }
                using (StreamWriter sw = new StreamWriter(filename, true, System.Text.Encoding.Default))
                {
                    sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss: ") + "," + msg);
                    sw.Close();
                }


            }
            catch
            { }
        }

        /// <summary>
        /// 写日志
        /// </summary>
        public void WriteLog(LogFile logFile, string msg)
            {
                WriteLog(logFile.ToString(), msg);
            }
        public void WriteLogcsv(LogFile logFile, string msg, string head)
        {
            WriteLogcsv(logFile.ToString(), msg, head);
        }
    }

        /// <summary>
        /// 日志类型
        /// </summary>
        public enum LogFile
        {
            Trace,
            Warning,
            Error,
            SQL
        }


        //LogManager.LogFielPrefix = "ERP ";
        //LogManager.LogPath = @"D:\fgg";
        //LogManager.WriteLog(LogFile.Trace, "A test Msg.");

    }

