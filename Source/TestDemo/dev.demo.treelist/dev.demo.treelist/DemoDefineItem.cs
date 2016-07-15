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

    public partial class DemoDefineItem : Form
    {
        public DemoDefineItem()
        {
            InitializeComponent();

            InitData();
        }

        private void InitData()
        {

            //treeList1.DataSource = DataSourceFactory.CreateRecordListSource();
            treeList1.DataSource = DataSourceFactory.CreateRecordTestSource();
            treeList1.KeyFieldName = "Id";
            treeList1.ParentFieldName = "Pid";
            treeList1.ExpandAll();
        }

        private void treeList1_GetCustomNodeCellEdit(object sender, DevExpress.XtraTreeList.GetCustomNodeCellEditEventArgs e)
        {
            if (e.Column.FieldName != "Category")
            {
                object obj = e.Node.GetValue(0);
                if (obj != null)
                {
                    switch (obj.ToString())
                    {
                        case "Category":
                            e.RepositoryItem = repositoryItemImageComboBox1;
                            break;
                        case "Supplier":
                            e.RepositoryItem = repositoryItemComboBox1;
                            break;
                        case "Unit Price":
                            e.RepositoryItem = repositoryItemCalcEdit1;
                            break;
                        case "Units in Stock":
                            e.RepositoryItem = repositoryItemSpinEdit1;
                            break;
                        case "Discontinued":
                            e.RepositoryItem = repositoryItemCheckEdit1;
                            break;
                        case "Last Order":
                            e.RepositoryItem = repositoryItemCalcEdit1;
                            break;
                        case "Relevance":
                            e.RepositoryItem = repositoryItemProgressBar1;
                            break;
                        case "Phone":
                            e.RepositoryItem = repositoryItemTextEdit1;
                            break;
                    }
                }
            }
        }
        //..
        /*
         ~Process key strokes within the in-place ProgressBar editor:
         */
        private void repositoryItemProgressBar1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            int i = 0;
            if (treeList1.ActiveEditor == null) return;

            if (e.KeyCode == Keys.Add)
            {
                i = (int)treeList1.ActiveEditor.EditValue;
                if (i < 100)
                    treeList1.ActiveEditor.EditValue = i + 1;
            }
            if (e.KeyCode == Keys.Subtract)
            {
                i = (int)treeList1.ActiveEditor.EditValue;
                if (i > 0)
                    treeList1.ActiveEditor.EditValue = i - 1;
            }
        }

        private void DemoDefineItem_Load(object sender, EventArgs e)
        {

            //this.reportViewer1.RefreshReport();
        }

    }
}
