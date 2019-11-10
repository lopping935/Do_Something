using System;
using System.Windows.Forms;

namespace AnBRobotSystem
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        private static System.Threading.Mutex mutex;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            /*try 
            {
                Login FrmLogin = new Login();
                FrmLogin.Tag = 1;   //将登录窗体的Tag属性设为1，表示调用的是登录窗体
                Application.Run(FrmLogin);
                if (AnBRobotSystem.DataClass.MyMeans.globalVar == true)
                    Application.Run(new MdiParent());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }*/
            mutex = new System.Threading.Mutex(true, "OnlyRun");
            if (mutex.WaitOne(0, false))
            {
                Application.Run(new MdiParent()); //Application.Run(new MainForm());
            }
            else
            {
                MessageBox.Show("程序已经在运行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
        }
    }
}
