using DevExpress.XtraGrid.Views.Layout;
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
    public partial class DemoLayoutView : Form
    {
        public DemoLayoutView()
        {
            InitializeComponent();
            //  设置角度
            this.layoutView1.OptionsCarouselMode.PitchAngle = 1.13f;

            //  设置显示格式
            this.layoutView1.OptionsView.ViewMode = LayoutViewMode.Carousel;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.gridControl1.DataSource = DataSourceFactory.CreateListSource();
        }
    }
}
