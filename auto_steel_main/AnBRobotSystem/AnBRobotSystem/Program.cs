using System;
using System.Windows.Forms;
using System.Threading;
//using logtest;
using VM.Core;
using VM.PlatformSDKCS;
namespace AnBRobotSystem
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        private static System.Threading.Mutex mutex;
        public static object obj = new object();
        public static int program_flag = 0;//1000 报错
        public static int model_flag = 0;//1000 报错

        public static int GB_chose_flag = 0;
        public static string GB_station = "";

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Thread th = new Thread(test);
            
            mutex = new System.Threading.Mutex(true, "OnlyRun");
            if (mutex.WaitOne(0, false))
            {
              //  th.Start();
                Application.Run(new MdiParent()); //Application.Run(new MainForm());
                
            }
            else
            {
                MessageBox.Show("程序已经在运行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
            
        }
        static void test()
        {
            while(true)
            {
                Thread.Sleep(1000);
                //LogHelper.WriteLog("fuck test");
            }
        }
    }
}
