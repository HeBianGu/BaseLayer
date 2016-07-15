using HebianGu.Demo.TestSource;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dev.Demo.GridControl
{
    public partial class DemoCardView : Form
    {
        public DemoCardView()
        {
            InitializeComponent();
        }

        private void DemoCardView_Load(object sender, EventArgs e)
        {
            this.gridControl1.DataSource = DataSourceFactory.CreateListSource();
        }
    }
}
