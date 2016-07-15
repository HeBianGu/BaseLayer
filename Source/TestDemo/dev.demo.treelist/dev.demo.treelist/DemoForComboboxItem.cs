using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
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
    public partial class DemoForComboboxItem : Form
    {
        public DemoForComboboxItem()
        {
            InitializeComponent();
        }

        private void DemoForComboboxItem_Load(object sender, EventArgs e)
        {
            InitControl();
        }
        RepositoryItemComboBox cmbFilterWells;
        /// <summary> 初始化控件 </summary>
        void InitControl()
        {
            List<string> wells = new List<string>() { "AAAAA", "BBBBBBB", "CCCCCCCCCC", "DDDDDDDDDDDD" };

            //  添加过滤列
            TreeListColumn filterWellColumn = this.treeList1.Columns.AddField("FilterWell");
            filterWellColumn.Width = 200;
            filterWellColumn.Visible = true;

            cmbFilterWells = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            cmbFilterWells.BestFitWidth = 100;
            cmbFilterWells.AutoHeight = false;
            cmbFilterWells.EditValueChangedDelay = -1;
            cmbFilterWells.EditValueChangedFiringMode = EditValueChangedFiringMode.Buffered;
            //  不可编辑
            cmbFilterWells.TextEditStyle = TextEditStyles.DisableTextEditor;

     

            cmbFilterWells.Popup += (object sender, EventArgs e) =>
            {
                RefreshControl();

            };

            cmbFilterWells.SelectedIndexChanged += (object sender, EventArgs e) =>
            {
                this.treeList1.RefreshNode(this.treeList1.FocusedNode);
                this.treeList1.ActiveEditor.Properties.BeginUpdate();
                this.treeList1.ActiveEditor.Properties.EndUpdate();
                this.treeList1.ActiveEditor.Properties.CancelUpdate();


                this.treeList1.RefreshCell(this.treeList1.FocusedNode, filterWellColumn);

                this.treeList1.Refresh();

                this.treeList1.Update();


                string ewr= this.treeList1.FocusedNode.GetValue("FilterWell")==null?"空":this.treeList1.FocusedNode.GetValue("FilterWell").ToString();

                string ss= this.treeList1.FocusedNode.GetDisplayText("FilterWell");
               
                XtraMessageBox.Show(ss, "提示");

                XtraMessageBox.Show(ewr, "提示");

            }; 

            //  添加绑定井
            cmbFilterWells.Items.AddRange(wells);
            cmbFilterWells.Name = "cmbFilterWells";
            filterWellColumn.ColumnEdit = cmbFilterWells;


            this.treeList1.Enabled = true;
            this.treeList1.OptionsBehavior.ReadOnly = false;
            this.treeList1.OptionsBehavior.Editable = true;

        }

        /// <summary> 刷新控件 </summary>
        void RefreshControl()
        {
            this.treeList1.ActiveEditor.DoValidate();

            List<string> wells = new List<string>() { "", "AAAAA", "BBBBBBB", "CCCCCCCCCC", "DDDDDDDDDDDD" };

            //  过滤选择过的井
            foreach (TreeListNode node in this.treeList1.Nodes)
            {
                string selectWell = node.GetDisplayText("FilterWell");

                wells.Remove(selectWell);
            }


            cmbFilterWells.Items.Clear();
            cmbFilterWells.Items.AddRange(wells);


        }

        private void treeList1_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            string sss;
        }
    }
}
