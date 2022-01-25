using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using log4net.Util;
using log4net.Layout;
using log4net.Core;

namespace logtest
{
    public class LogHelper
    {
        private LogHelper()
        {

        }

        public static readonly log4net.ILog loginfo = log4net.LogManager.GetLogger("loginfo");//获取一个日志记录器

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
    //public class CsvTextWriter : TextWriter
    //{
    //    private readonly TextWriter _textWriter;

    //    public CsvTextWriter(TextWriter textWriter)
    //    {
    //        _textWriter = textWriter;
    //    }

    //    public override Encoding Encoding => _textWriter.Encoding;

    //    public override void Write(char value)
    //    {
    //        _textWriter.Write(value);
    //        if (value == '"')
    //            _textWriter.Write(value);
    //    }

    //    public void WriteQuote()
    //    {
    //        _textWriter.Write('"');
    //    }
    //}
    //public class NewFieldConverter : PatternConverter
    //{
    //    protected override void Convert(TextWriter writer, object state)
    //    {
    //        var ctw = writer as CsvTextWriter;
    //        ctw.WriteQuote();

    //        writer.Write(',');

    //        ctw.WriteQuote();
    //    }
    //}
    //public class EndRowConverter : PatternConverter
    //{
    //    protected override void Convert(TextWriter writer, object state)
    //    {
    //        var ctw = writer as CsvTextWriter;

    //        ctw.WriteQuote();

    //        writer.WriteLine();
    //    }
    //}   
    //public class CsvPatternLayout : PatternLayout
    //{
    //    public override void ActivateOptions()
    //    {
    //        AddConverter("newfield", typeof(NewFieldConverter));
    //        AddConverter("endrow", typeof(EndRowConverter));
    //        base.ActivateOptions();
    //    }

    //    public override void Format(TextWriter writer, LoggingEvent loggingEvent)
    //    {
    //        var ctw = new CsvTextWriter(writer);
    //        ctw.WriteQuote();
    //        base.Format(ctw, loggingEvent);
    //    }
    //}
    
}
