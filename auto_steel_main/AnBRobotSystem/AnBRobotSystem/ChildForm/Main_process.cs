using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
//using hanbiao;
using SQLPublicClass;
using System.Data.Common;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using AnBRobotSystem;

namespace AnBRobotSystem.ChildForm
{
    public partial class Main_process : Sunny.UI.UIForm
    {
        Thread update_control;
        bool flag=false;
        public updatelistiew writelistview;
        
        public Main_process(updatelistiew up1)
        {
            InitializeComponent();
            writelistview = up1;
            update_control = new Thread(update);
            flag = true;
            update_control.IsBackground = true;
            update_control.Start();
         
        }
        public void update()
        {
            while(flag)
            {
                Thread.Sleep(1000);
                if (AnBRobotSystem.MdiParent.autoprocess.TB_onpos)
                    TB_uiLight.OnColor = Color.Green;
                else
                    TB_uiLight.OnColor = Color.Red;
                if (AnBRobotSystem.MdiParent.autoprocess.TB_initresult)
                {
                    TBK_uiLight.OnColor = Color.Green;
                    
                    ZTL_uiLedLabel1.Invoke(new Action(() => { ZTL_uiLedLabel1.Text = AnBRobotSystem.MdiParent.autoprocess.TB_need_wight.ToString(); }));
                }
                else
                {
                    TBK_uiLight.OnColor = Color.Red;
                    ZTL_uiLedLabel1.Invoke(new Action(() => { ZTL_uiLedLabel1.Text = "0"; }));
                    
                }

                if (AnBRobotSystem.MdiParent.autoprocess.GB_onpos)
                {
                    GCDW_uiLight.OnColor = Color.Green;
                    
                    GW_uiLedLabel.Invoke(new Action(() => { GW_uiLedLabel.Text = AnBRobotSystem.MdiParent.autoprocess.GB_station; }));
                }
                else
                {
                    GCDW_uiLight.OnColor = Color.Red;
                    
                    GW_uiLedLabel.Invoke(new Action(() => { GW_uiLedLabel.Text = "N"; }));
                }
                    

                if (AnBRobotSystem.MdiParent.autoprocess.GB_initresult)
                {
                    GBSJ_uiLight.OnColor = Color.Green;
                   
                    MG_uiLedLabel.Invoke(new Action(() => { MG_uiLedLabel.Text = AnBRobotSystem.MdiParent.autoprocess.GB_capacity; }));
                    GNTL_uiLedLabel.Invoke(new Action(() => { GNTL_uiLedLabel.Text = AnBRobotSystem.MdiParent.autoprocess.GB_have_wight.ToString(); }));

                }
                else
                {
                    GBSJ_uiLight.OnColor = Color.Red;
                    MG_uiLedLabel.Text = "N";
                    MG_uiLedLabel.Text = "N";
                }
            }
        }

        private void Main_process_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            this.Close();
        }

   

        private void Main_process_FormClosing(object sender, FormClosingEventArgs e)
        {
            update_control.Abort();
            flag = false;
            this.Dispose();
            this.Close();
            
        }
    }

}
