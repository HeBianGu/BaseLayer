using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlTest1
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            SimpleButton btn = sender as SimpleButton;
            string fullName = this.GetType().Namespace + "." + btn.Text;
            Form f=  Assembly.GetExecutingAssembly().CreateInstance(fullName) as Form;
            f.StartPosition = FormStartPosition.Manual;
            f.Location =Cursor.Position;
            //f.Location = btn.PointToScreen(Control.MousePosition);
            f.Show();

        }
    }
}
