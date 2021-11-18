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
    public partial class UserInfor : Form
    {
        DataClass.MyMeans MyClass = new AnBRobotSystem.DataClass.MyMeans();

        public UserInfor()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (listBox1.Text == "")
                return;
            //删除系统用户,当前用户不能删除
            if (listBox1.SelectedItem.ToString() == DataClass.MyMeans.Login_Name)
            {
                MessageBox.Show(" 当前使用的用户不能删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //删除系统操作员后,重新定向到本页
                MyClass.getsqlcom("DELETE FROM tb_Login WHERE(Name = '" + listBox1.SelectedItem.ToString() + "')");
                MessageBox.Show("删除用户成功！");
                this.listBox1.Items.Remove(listBox1.SelectedItem.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //判断该系统用户名是否存在,如果存在将不允许创建,否则设置系统用户
            DataSet ds = MyClass.getDataSet("Select * from tb_Login where Name='" + textBox3.Text + "'", "tb_Login");
            if (textBox1.Text != textBox2.Text)
            {
                MessageBox.Show("两次输入的密码不一致！");
                return;
            }
            else
            {
                //添加系统用户

                MyClass.getsqlcom("INSERT INTO tb_Login (ID,Name, Pass, loginTime, system,sign) values('0005','" + textBox3.Text + "','" + textBox1.Text + "','','0','false')");
                MessageBox.Show("添加用户成功！");
                this.listBox1.Items.Add(textBox3.Text);

            }
        }

        private void UserInfor_Load(object sender, EventArgs e)
        {
            DbDataReader temDR = MyClass.getcom("Select Name from tb_Login");
            while (temDR.Read())
            {
                this.listBox1.Items.Add(temDR["Name"].ToString());
                //this.comboBox1.Items.Add(temDR["Name"].ToString());
            }
        }
    }
}
