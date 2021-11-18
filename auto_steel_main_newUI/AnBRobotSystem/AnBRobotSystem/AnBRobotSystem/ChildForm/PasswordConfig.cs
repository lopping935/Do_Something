using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;

namespace AnBRobotSystem
{
    public partial class PasswordConfig : Form
    {   DataClass.MyMeans MyClass = new AnBRobotSystem.DataClass.MyMeans();
        public PasswordConfig()
        {
            InitializeComponent();   
        }

        private void PasswordConfig_Load(object sender, EventArgs e)
        {
            label2.Text = DataClass.MyMeans.Login_Name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != textBox2.Text)
            {
                MessageBox.Show("两次输入的密码不一致！");
                return;
            }
            else
            {
                MyClass.getsqlcom("update tb_Login set Pass='" + textBox1.Text + "' where Name='" + DataClass.MyMeans.Login_Name + "'");
                MessageBox.Show("设置新密码成功！");
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text="";
            textBox2.Text = "";
        }
    }
}
