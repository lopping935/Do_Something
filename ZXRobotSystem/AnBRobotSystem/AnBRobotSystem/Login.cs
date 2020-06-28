using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Win32;



namespace AnBRobotSystem
{ 
    public partial class Login : Form
    {
        bool ifFistIn = true;
        DataClass.MyMeans MyClass = new AnBRobotSystem.DataClass.MyMeans();
        public Login()
        {
            InitializeComponent();
        }

        private void TB_CODE_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                TB_CODE.Focus();
        }

        private void TB_PASS_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                TB_PASS.Focus();
        }

        private void BTLogin_Click(object sender, EventArgs e)
        {
            if (TB_CODE.Text != "" & TB_PASS.Text != "")
            {
                SqlDataReader temDR = MyClass.getcom("select * from TB_LOGIN where NAME='" + TB_CODE.Text.Trim() + "' and PASS='" + TB_PASS.Text.Trim() + "'");
                bool ifcom = temDR.Read();
                if (ifcom)
                {
                    DataClass.MyMeans.Login_Name = TB_CODE.Text.Trim();
                    DataClass.MyMeans.Login_ID = temDR.GetString(0);
                    DataClass.MyMeans.Login_Pope = temDR.GetString(4);
                    DataClass.MyMeans.My_con.Close();
                    DataClass.MyMeans.My_con.Dispose();
                    DataClass.MyMeans.Login_n = (int)(this.Tag);
                    AnBRobotSystem.DataClass.MyMeans.globalVar = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("用户名或密码错误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TB_CODE.Text = "";
                    TB_PASS.Text = "";
                }
                MyClass.con_close();
            }
            else
                MessageBox.Show("请将登录信息添写完整！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BTCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            RegistryKey location = Registry.LocalMachine;
            RegistryKey soft = location.OpenSubKey("SOFTWARE", true);//可写 
            RegistryKey myPass = soft.CreateSubKey("FTLiang");
            myPass.SetValue("s1", TB_CODE.Text);
            myPass.SetValue("s2", TB_PASS.Text);
            myPass.SetValue("s3", checkBox1.Checked);
        }

        private void checkBox2_CheckStateChanged(object sender, EventArgs e)
        { 
            if (ifFistIn == false)
            {
                RegistryKey location = Registry.LocalMachine;
                RegistryKey soft = location.OpenSubKey("SOFTWARE", true);//可写 
                RegistryKey myPass = soft.CreateSubKey("FTLiang");
                myPass.SetValue("s4", checkBox2.Checked);

                if (checkBox2.Checked)
                {
                    string exeDir = Application.ExecutablePath;//要启动的程序绝对路径
                    RegistryKey rk = Registry.LocalMachine;
                    RegistryKey softWare = rk.OpenSubKey("SOFTWARE");
                    RegistryKey microsoft = softWare.OpenSubKey("Microsoft");
                    RegistryKey windows = microsoft.OpenSubKey("Windows");
                    RegistryKey current = windows.OpenSubKey("CurrentVersion");
                    RegistryKey run = current.OpenSubKey(@"Run", true);//这里必须加true就是得到写入权限 
                    run.SetValue("FTStart", exeDir);
                }
                else
                {
                    string exeDir = Application.ExecutablePath;//要启动的程序绝对路径
                    RegistryKey rk = Registry.LocalMachine;
                    RegistryKey softWare = rk.OpenSubKey("SOFTWARE");
                    RegistryKey microsoft = softWare.OpenSubKey("Microsoft");
                    RegistryKey windows = microsoft.OpenSubKey("Windows");
                    RegistryKey current = windows.OpenSubKey("CurrentVersion");
                    RegistryKey run = current.OpenSubKey(@"Run", true);//这里必须加true就是得到写入权限 
                    run.DeleteValue("FTStart");//这里必须加true就是得到写入权限 
                }
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            //从注册表中读取 是否保存了用户名密码 自动启动配置
            try
            {
                RegistryKey location = Registry.LocalMachine;
                RegistryKey soft = location.OpenSubKey("SOFTWARE", false);//可写 
                RegistryKey myPass = soft.OpenSubKey("FTLiang", false);
                TB_CODE.Text = myPass.GetValue("s1").ToString();
                string s2 = myPass.GetValue("s2").ToString();

                bool ifSave = Convert.ToBoolean(myPass.GetValue("s3"));
                checkBox1.Checked = ifSave;
               // checkBox1.Checked = false;
                bool ifSave2 = Convert.ToBoolean(myPass.GetValue("s4"));

                checkBox2.Checked = ifSave2;
                //checkBox2.Checked = false;
                if (ifSave)
                {
                    TB_PASS.Text = s2;
                }
                else
                {
                    TB_PASS.Text = "";
                }

                ifFistIn = false;       //程序已启动完毕，可以执行注册表相关动作

            }
            catch (Exception ex)
            {
                //todo something
            }
        }
    }
}

