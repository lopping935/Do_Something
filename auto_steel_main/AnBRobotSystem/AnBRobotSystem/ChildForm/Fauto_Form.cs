using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnBRobotSystem;
using VM.Core;
using VM.PlatformSDKCS;
using AnBRobotSystem;
namespace AnBRobotSystem.ChildForm
{
    public partial class Fauto_Form : Form
    {
       
        public Fauto_Form()
        {
            InitializeComponent();
            vmProcedureConfigControl1.InitControl();
            AnBRobotSystem.MdiParent.process1 = (VmProcedure)VmSolution.Instance["流程1"];
            Tiebao_vmRenderControl1.ModuleSource = AnBRobotSystem.MdiParent.process1;
            AnBRobotSystem.MdiParent.process2 = (VmProcedure)VmSolution.Instance["流程2"];
            Guankou_vmRenderControl2.ModuleSource = AnBRobotSystem.MdiParent.process2;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //// MdiParent. = (VmProcedure)VmSolution.Instance["流程1"];
            // Tiebao_vmRenderControl1.ModuleSource = process1;
            //mainlog("visio code", "视觉测试ok");
            AnBRobotSystem.Utlis.Auto_steel_helper.mainlog("visio code", "视觉测试ok");


        }
      
        public static void mainlog(string model, string strmsg)
        {
            ListViewItem iteme1 = new ListViewItem(DateTime.Now.ToString());
            iteme1.SubItems.Add(model);
            iteme1.SubItems.Add(strmsg);
            AnBRobotSystem.MdiParent.form.listView1.Items.Insert(0, iteme1); 
        }
    }
}
