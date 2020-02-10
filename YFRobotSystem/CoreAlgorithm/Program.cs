using System;
using System.ServiceProcess;
using System.Text;
using log4net;
[assembly: log4net.Config.DOMConfigurator(Watch = true) ]
namespace CoreAlgorithm
{
    static class Program
    {
        public static object obj = new object();
        public static object gllock = new object();
        public static Int16 MessageFlg = 0;
        public static Int16 MessageStop = 0;
        public static byte PrintNum = 0;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
             ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
			{ 
				new AnB_CoreAlgorithm() 
			};
            ServiceBase.Run(ServicesToRun);
         /* AnB_CoreAlgorithm objISD= new AnB_CoreAlgorithm();
            objISD.OnStart(); 
            System.Threading.Thread.Sleep(120000000);*/
            //objISD.OnStop();
        }
    }
}
