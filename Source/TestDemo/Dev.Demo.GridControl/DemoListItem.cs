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
    /// <summary> 测试同一列 不同行显示不同控件 主要在这个方法里面 gridView1_CustomRowCellEdit </summary>
    public partial class DemoListItem : Form
    {
        public DemoListItem()
        {
            InitializeComponent();
        }

        private void DemoBandedView_Load(object sender, EventArgs e)
        {
            //this.gridControl1.DataSource = DataSourceFactory.CreateListSource();

            List<CheckList> cs = new List<CheckList>()
            {
                new CheckList(){Name="1"},new CheckList(){Name="2"},new CheckList(){Name="3"}
            };

            this.gridControl1.DataSource = cs;
        }

        private void gridView1_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "Category")
            {
                CheckList obj = this.gridView1.GetRow(e.RowHandle) as CheckList;
                if (obj != null)
                {
                    switch (obj.Name)
                    {
                        case "1":
                            e.RepositoryItem = repositoryItemCheckedComboBoxEdit1;
                            break;
                        case "2":
                            e.RepositoryItem = repositoryItemCheckedComboBoxEdit1;
                            break;
                        case "3":
                            e.RepositoryItem = repositoryItemCheckEdit1;
                            break;
                        //case "Units in Stock":
                        //    e.RepositoryItem = repositoryItemSpinEdit1;
                        //    break;
                        //case "Discontinued":
                        //    e.RepositoryItem = repositoryItemCheckEdit1;
                        //    break;
                        //case "Last Order":
                        //    e.RepositoryItem = repositoryItemCalcEdit1;
                        //    break;
                        //case "Relevance":
                        //    e.RepositoryItem = repositoryItemProgressBar1;
                        //    break;
                        //case "Phone":
                        //    e.RepositoryItem = repositoryItemTextEdit1;
                        //    break;
                    }
                }
            }
        }
    }

    public class CheckList
    {
        //List<string> ss = new List<string>();

        //public List<string> Ss
        //{
        //    get { return ss; }
        //    set { ss = value; }
        //}

        string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        string category;

        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        List<string> ss1 = new List<string>();

        public List<string> Ss1
        {
            get { return ss1; }
            set { ss1 = value; }
        }

    }
}
