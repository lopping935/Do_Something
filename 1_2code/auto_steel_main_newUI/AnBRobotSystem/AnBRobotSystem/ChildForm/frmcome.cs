//using AnBRobotSystem;
using AnBRobotSystem;
using AnBRobotSystem.Utlis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _20200123night
{
    public partial class frmcome : Form
    {
        public frmcome()     //登陆界面
        {
            InitializeComponent();
        }


        public dbTaskHelper dbhlper = new dbTaskHelper();
        public static string right;
        public static MdiParent objtxtSto = null;
        public static MdiParent objtxtSto_jia = null;
        public static MdiParent objtxtSto_yi = null;
        public static MdiParent objtxtSto_bing = null;
        public static MdiParent objtxtSto_ding = null;

        private void buttoncome_Click(object sender, EventArgs e)
        {

            //if (txtuser.Text == "jia" && txtpassword.Text == "angang" && txtcodeinput.Text == txtcode.Text && ub_jia.Checked /*&& txtcodeinput.Text != null*/)
            //{
            //    right = "jia";
            //    string sqltext = string.Format("insert into [AutoSteel].[dbo].[banci] VALUES ('{0}','{1}')", right, DateTime.Now);
            //    dbhlper.MultithreadExecuteNonQuery(sqltext);

            //    txtresult.Text = "恭喜成功登录";
            //    if (objtxtSto_jia == null)
            //    {
            //        objtxtSto_jia = new MdiParent();
            //        objtxtSto_jia.Show();
            //    }
            //    else
            //    {
            //        objtxtSto_jia.Activate();
            //        objtxtSto_jia.WindowState = FormWindowState.Normal;
            //    }
            //}
            //else if (txtuser.Text == "yi" && txtpassword.Text == "angang" && txtcodeinput.Text == txtcode.Text && ub_yi.Checked /*&& txtcodeinput.Text != null*/)
            //{
            //    right = "yi";
            //    string sqltext = string.Format("insert into  [AutoSteel].[dbo].[banci] VALUES ('{0}','{1}')", right, DateTime.Now);
            //    txtresult.Text = "恭喜成功登录";
            //    if (objtxtSto_yi == null)
            //    {
            //        objtxtSto_yi = new MdiParent();
            //        objtxtSto_yi.Show();
            //    }
            //    else
            //    {
            //        objtxtSto_yi.Activate();
            //        objtxtSto_yi.WindowState = FormWindowState.Normal;
            //    }
            //}
            //else if (txtuser.Text == "bing" && txtpassword.Text == "angang" && txtcodeinput.Text == txtcode.Text && ub_bing.Checked /*&& txtcodeinput.Text != null*/)
            //{
            //    right = "bing";
            //    string sqltext = string.Format("insert into  [AutoSteel].[dbo].[banci] VALUES ('{0}','{1}')", right, DateTime.Now);
            //    txtresult.Text = "恭喜成功登录";
            //    if (objtxtSto_bing == null)
            //    {
            //        objtxtSto_bing = new MdiParent();
            //        objtxtSto_bing.Show();
            //    }
            //    else
            //    {
            //        objtxtSto_bing.Activate();
            //        objtxtSto_bing.WindowState = FormWindowState.Normal;
            //    }
            //}
            //else if (txtuser.Text == "ding" && txtpassword.Text == "angang" && txtcodeinput.Text == txtcode.Text && ub_ding.Checked /*&& txtcodeinput.Text != null*/)
            //{
            //    right = "ding";
            //    string sqltext = string.Format("insert into  [AutoSteel].[dbo].[banci] VALUES ('{0}','{1}')", right, DateTime.Now);
            //    txtresult.Text = "恭喜成功登录";
            //    if (objtxtSto_ding == null)
            //    {
            //        objtxtSto_ding = new MdiParent();
            //        objtxtSto_ding.Show();
            //    }
            //    else
            //    {
            //        objtxtSto_ding.Activate();
            //        objtxtSto_ding.WindowState = FormWindowState.Normal;
            //    }
            //}
            //else if (txtuser.Text == "admin" && txtpassword.Text == "angang666" && txtcodeinput.Text == txtcode.Text /*&& txtcodeinput.Text != null*/)
            //{
            //    right = "guanliyuan";
            //    string sqltext = string.Format("insert into  [AutoSteel].[dbo].[banci] VALUES ('{0}','{1}')", right, DateTime.Now);
            //    txtresult.Text = "恭喜成功登录";
            //    if (objtxtSto == null)
            //    {
            //        objtxtSto = new MdiParent();
            //        objtxtSto.Show();
            //    }
            //    else
            //    {
            //        objtxtSto.Activate();
            //        objtxtSto.WindowState = FormWindowState.Normal;
            //    }
            //}
            ////else if (txtuser.Text == "text" && txtpassword.Text == "123456" && txtcodeinput.Text == txtcode.Text /*&& txtcodeinput.Text != null*/)
            ////{

            ////    txtresult.Text = "恭喜成功登录";
            ////    if (objtxtSto == null)
            ////    {
            ////        objtxtSto_text = new MdiParent();
            ////        objtxtSto_text.Show();
            ////    }
            ////    else
            ////    {
            ////        objtxtSto_text.Activate();
            ////        objtxtSto_text.WindowState = FormWindowState.Normal;
            ////    }
            ////}
            //else
            //{
            //    txtresult.Text = "账号或密码或验证码不正确";
            //}

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btncreat_Click(object sender, EventArgs e)
        {
            Random verification = new Random();

            txtcode.Text = verification.Next(1000, 99999).ToString();
        }

        private void frmcome_Load(object sender, EventArgs e)
        {
            Random verification = new Random();                                                      //验证码的生成

            txtcode.Text = verification.Next(1000, 99999).ToString();
        }

        private void txtuser_TextChanged(object sender, EventArgs e)
        {

        }

        private void uiComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void uiBtn_come_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtuser.Text == "jia" && txtpassword.Text == "angang" && txtcodeinput.Text == txtcode.Text && ub_jia.Checked /*&& txtcodeinput.Text != null*/)
                {
                    right = "甲";
                    string sqltext = string.Format("insert into [AutoSteel].[dbo].[banci] VALUES ('{0}','{1}')", right, DateTime.Now);
                    dbhlper.MultithreadExecuteNonQuery(sqltext);

                    txtresult.Text = "登录成功";
                    if (objtxtSto_jia == null)
                    {
                        objtxtSto_jia = new MdiParent();
                        objtxtSto_jia.Show();
                    }
                    else
                    {
                        objtxtSto_jia.Activate();
                        objtxtSto_jia.WindowState = FormWindowState.Normal;
                    }
                }
                else if (txtuser.Text == "yi" && txtpassword.Text == "angang" && txtcodeinput.Text == txtcode.Text && ub_yi.Checked /*&& txtcodeinput.Text != null*/)
                {
                    right = "乙";
                    string sqltext = string.Format("insert into  [AutoSteel].[dbo].[banci] VALUES ('{0}','{1}')", right, DateTime.Now);
                    dbhlper.MultithreadExecuteNonQuery(sqltext);

                    txtresult.Text = "登录成功";
                    if (objtxtSto_yi == null)
                    {
                        objtxtSto_yi = new MdiParent();
                        objtxtSto_yi.Show();
                    }
                    else
                    {
                        objtxtSto_yi.Activate();
                        objtxtSto_yi.WindowState = FormWindowState.Normal;
                    }
                }
                else if (txtuser.Text == "bing" && txtpassword.Text == "angang" && txtcodeinput.Text == txtcode.Text && ub_bing.Checked /*&& txtcodeinput.Text != null*/)
                {
                    right = "丙";
                    string sqltext = string.Format("insert into  [AutoSteel].[dbo].[banci] VALUES ('{0}','{1}')", right, DateTime.Now);
                    dbhlper.MultithreadExecuteNonQuery(sqltext);

                    txtresult.Text = "登录成功";
                    if (objtxtSto_bing == null)
                    {
                        objtxtSto_bing = new MdiParent();
                        objtxtSto_bing.Show();
                    }
                    else
                    {
                        objtxtSto_bing.Activate();
                        objtxtSto_bing.WindowState = FormWindowState.Normal;
                    }
                }
                else if (txtuser.Text == "ding" && txtpassword.Text == "angang" && txtcodeinput.Text == txtcode.Text && ub_ding.Checked /*&& txtcodeinput.Text != null*/)
                {
                    right = "丁";
                    string sqltext = string.Format("insert into  [AutoSteel].[dbo].[banci] VALUES ('{0}','{1}')", right, DateTime.Now);
                    dbhlper.MultithreadExecuteNonQuery(sqltext);

                    txtresult.Text = "登录成功";
                    if (objtxtSto_ding == null)
                    {
                        objtxtSto_ding = new MdiParent();
                        objtxtSto_ding.Show();
                    }
                    else
                    {
                        objtxtSto_ding.Activate();
                        objtxtSto_ding.WindowState = FormWindowState.Normal;
                    }
                }
                else if (txtuser.Text == "admin" && txtpassword.Text == "angang666" && txtcodeinput.Text == txtcode.Text /*&& txtcodeinput.Text != null*/)
                {
                    right = "管理员";
                    string sqltext = string.Format("insert into  [AutoSteel].[dbo].[banci] VALUES ('{0}','{1}')", right, DateTime.Now);
                    dbhlper.MultithreadExecuteNonQuery(sqltext);

                    txtresult.Text = "登录成功";
                    if (objtxtSto == null)
                    {
                        objtxtSto = new MdiParent();
                        objtxtSto.Show();
                    }
                    else
                    {
                        objtxtSto.Activate();
                        objtxtSto.WindowState = FormWindowState.Normal;
                    }
                }
                //else if (txtuser.Text == "text" && txtpassword.Text == "123456" && txtcodeinput.Text == txtcode.Text /*&& txtcodeinput.Text != null*/)
                //{

                //    txtresult.Text = "恭喜成功登录";
                //    if (objtxtSto == null)
                //    {
                //        objtxtSto_text = new MdiParent();
                //        objtxtSto_text.Show();
                //    }
                //    else
                //    {
                //        objtxtSto_text.Activate();
                //        objtxtSto_text.WindowState = FormWindowState.Normal;
                //    }
                //}
                else
                {
                    txtresult.Text = "账号密码或验证码不正确";
                }
            }
            catch
            {
                txtresult.Text = "登录异常catch";
            }
           
        }

        private void uib_close_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("系统退出？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                //执行保存
                //【1】清空
                //File.WriteAllText(fileName, string.Empty);

                //【2】把objList写入
                //StreamWriter sw = new StreamWriter(fileName, true, Encoding.Default);
                //foreach (string item in objListStudent)
                //{
                //    sw.WriteLine(item);
                //}
                //sw.Close();
                //【3】写入完成
                MessageBox.Show("完成！", "系统消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            
        }

        //Console.ReadKey();

    }
}
//}