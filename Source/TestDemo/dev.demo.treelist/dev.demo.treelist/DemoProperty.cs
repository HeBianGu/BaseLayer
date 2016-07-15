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

namespace dev.demo.treelist
{
    public partial class DemoProperty : Form
    {
        public DemoProperty()
        {
            InitializeComponent();

            this.bindingSource1.DataSource = DataSourceFactory.CreateListSource();

            this.treeList1.DataSource = this.bindingSource1;

            this.treeList1.KeyFieldName = "Id";
            this.treeList1.ParentFieldName = "Pid";

        }
    }
}
