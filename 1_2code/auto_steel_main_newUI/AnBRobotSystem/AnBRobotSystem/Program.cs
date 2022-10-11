using System;
using System.Windows.Forms;
using System.Threading;
//using logtest;
using VM.Core;
using VM.PlatformSDKCS;
using _20200123night;

namespace AnBRobotSystem
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        private static System.Threading.Mutex mutex;
        public static object obj = new object();
        public static int program_flag = 0;//折铁线程中的中间变量
        public static int ZT_err_flag = 0;//折铁线程中的中间变量
        public static int ZT_auto_back = 0;//等于0时将跳出自动回灌进程
        public static int ZT_thread_flag = 1000;//是否有折铁正在进行  有折铁程序就等于1 没有折铁线程就等于1000
        public static int GB_data_flag = 0;//折铁过程中数据  主要是mes方面数据自动更新发生错误时
        public static int GB_chose_flag = 0;
        public static string GB_station = "";
        public static string testcode = "";

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
                //Application.Run(new frmcome()); 
                Application.Run(new MdiParent());
                
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
