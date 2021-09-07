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
    public partial class Fauto_Form : Sunny.UI.UIForm
    {
       
        public Fauto_Form()
        {
            InitializeComponent();
            vmProcedureConfigControl1.InitControl();
            Tiebao_vmRenderControl1.ModuleSource = AnBRobotSystem.MdiParent.process_TB;
            Guankou_vmRenderControl2.ModuleSource = AnBRobotSystem.MdiParent.process_GK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //// MdiParent. = (VmProcedure)VmSolution.Instance["流程1"];
            // Tiebao_vmRenderControl1.ModuleSource = process1;
            //mainlog("visio code", "视觉测试ok");
            //AnBRobotSystem.Utlis.Auto_steel_helper.mainlog("visio code", "视觉测试ok");


        }
      
       
    }
}
