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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //// MdiParent. = (VmProcedure)VmSolution.Instance["流程1"];
            // Tiebao_vmRenderControl1.ModuleSource = process1;
           
        }
    }
}
