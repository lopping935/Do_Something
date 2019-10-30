using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using log4net;


namespace PublicClass
{
    public enum LogType { INFO, ERROR, WARNING, FATAL };
    public class Log
    {
        public static LogType LogTypes;
        public static void addLog(ILog lg, LogType ly, string str)
        {
            //log4net.ILog log = log4net.LogManager.GetLogger(ClassInfo);
            switch (ly)
            {
                case LogType.WARNING:
                    lg.Warn(str);
                    break;
                case LogType.FATAL:
                    lg.Fatal(str);
                    break;
                case LogType.ERROR:
                    lg.Error(str);
                    break;
                default:
                    lg.Info(str);
                    break;

            }

        }

        //private static FileInfo logFile;            //创建、复制、删除、移动和打开文件的实例
        //private static StreamWriter logWriter;      //写入流
        //private static DirectoryInfo logDirectory;  //创建、移动和枚举目录的实例
        //public Log(string _praName)
        //{
        //    //在可执行的文件路径对应的目录下创建log文件夹作为日志存放的位置
        //    string logDirectoryName = System.AppDomain.CurrentDomain.BaseDirectory + "../log/" + _praName;
        //    logDirectory = new DirectoryInfo(logDirectoryName);
        //    if (!logDirectory.Exists)//如果目录不存在，则创建目录
        //    {
        //        logDirectory.Create();
        //    }
        //    string logFileName = logDirectoryName + "/" + System.DateTime.Now.Year + System.DateTime.Now.Month + System.DateTime.Now.Day + ".Log";

        //    logFile = new FileInfo(logFileName);
        //    if (!logFile.Exists)//如果文件不存在则创建文件。
        //    {
        //        logFile.Create().Close();
        //    }
        //    logWriter = logFile.AppendText();//创建一个System.IO.StreamWriter，它向logFile对应的文件追加文本
        //}

        ////添加log记录
        //public void addLog(string log)
        //{
        //    try
        //    {
        //        logWriter.WriteLine(System.DateTime.Now + ":  " + log);//写入行，记录时间和对应的事件描述
        //        logWriter.Flush();//清理编写器的所有缓冲区，并将所有缓冲数据写入基础流
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message.ToString());
        //    }
        //}

        ////释放资源，并关闭当前对象和基础流
        //public void disP()
        //{
        //    logWriter.Dispose();
        //    logWriter.Close();
        //}
    }
}
