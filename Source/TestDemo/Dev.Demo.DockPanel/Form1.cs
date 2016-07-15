using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraEditors;

namespace Dev.Demo.DockPanelDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void All_Click(object sender, EventArgs e)
        {
            //DevExpress.XtraBars.Docking.DockPanel
            if(sender is DevExpress.XtraBars.Docking.DockPanel)
            {
                DevExpress.XtraBars.Docking.DockPanel d = sender as DevExpress.XtraBars.Docking.DockPanel;
                this.propertyGridControl1.SelectedObject = d;
            }

            if (sender is SplitContainerControl)
            {
                SplitContainerControl s = sender as SplitContainerControl;
                this.propertyGridControl1.SelectedObject = s;
            }

            if(sender is PictureEdit)
            {
                PictureEdit p = sender as PictureEdit;

                this.propertyGridControl1.SelectedObject = p;
            }
        }
    }
}
