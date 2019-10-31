using System.ServiceProcess;
using System.Text;
using log4net;
[assembly: log4net.Config.DOMConfigurator(Watch = true) ]
namespace CoreAlgorithm
{
    static class Program
    {
        public static object obj = new object();
        public static object pobj = new object();
        public static int ShootA = 0;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
             ServiceBase[] ServicesToRun;
               ServicesToRun = new ServiceBase[] 
                { 
                    new MAnB_CoreAlgorithmMES() 
                };
                ServiceBase.Run(ServicesToRun);
            /* MAnB_CoreAlgorithmMES objISD = new MAnB_CoreAlgorithmMES();
              objISD.OnStart(); 
              System.Threading.Thread.Sleep(120000000);*/
            //objISD.OnStop();
        }
    }
}
