using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using HebianGu.Demo.TestSource;
using System;
using System.Collections;
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
    /// <summary> 用于测试repositoryItemComboBox1绑定数据源 界面映射显示 </summary>
    public partial class DemoComboboxReflect : Form
    {
        public DemoComboboxReflect()
        {
            InitializeComponent();
        }

        List<Student> students = null;
        private void DemoComboboxReflect_Load(object sender, EventArgs e)
        {
            //repositoryItemComboBox1
            this.gridControl1.DataSource = students = DataSourceFactory.CreateListSource();


            //string[] test = new string[2] { "男", "女" };

            //CboItemEntity item = new CboItemEntity();
            //item.Text = "男";
            //item.Value = "0";
            //repositoryItemComboBox1.Items.Add(item);



            //CboItemEntity item1 = new CboItemEntity();
            //item1.Text = "女";
            //item1.Value = "1";
            //repositoryItemComboBox1.Items.Add(item1);


            //repositoryItemComboBox1.Bind<Student>(test);

            //repositoryItemComboBox1.SelectedIndexChanged += ComboBoxEdit_SelectedIndexChanged;

            //repositoryItemComboBox1.ParseEditValue += repositoryItemComboBox1_ParseEditValue;
        }
        void repositoryItemComboBox1_ParseEditValue(object sender, ConvertEditValueEventArgs e)
        {
            e.Value = e.Value.ToString();
            e.Handled = true;
        }
        void ComboBoxEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            //CboItemEntity item = new CboItemEntity();
            //try
            //{
            //    //1.获取下拉框选中值
            //    item = (CboItemEntity)(sender as ComboBoxEdit).SelectedItem;
            //    string text = item.Text.ToString();
            //    //2.获取gridview选中的行
            //    GridView myView = (gridControl1.MainView as GridView);
            //    int dataIndex = myView.GetDataSourceRowIndex(myView.FocusedRowHandle);
            //    //3.保存选中值到datatable
            //    students[dataIndex].Sex = int.Parse(item.Value.ToString());
            //    //dt.Rows[dataIndex]["value"] = value;
            //    //dt.Rows[dataIndex]["text"] = text;
            //}
            //catch (Exception ex)
            //{
            //    XtraMessageBox.Show(ex.Message, "提示");
            //}
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            Student student = this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Student;

            this.propertyGridControl2.SelectedObject = student;

        }

    }

    public class CboItemEntity
    {
        private object _text = 0;
        private object _Value = "";
        /// <summary>
        /// 显示值
        /// </summary>
        public object Text
        {
            get { return this._text; }
            set { this._text = value; }
        }
        /// <summary>
        /// 对象值
        /// </summary>
        public object Value
        {
            get { return this._Value; }
            set { this._Value = value; }
        }

        public override string ToString()
        {
            return this.Text.ToString();
        }
    }

    public static class GirdControlEx
    {
        public static void Bind<T>(this RepositoryItemComboBox combox, ICollection source)
        {
            /*说明：
             *所涉及的列叙设定FieldName，否则会出现无法选中的问题；
             *eg:
             *List<PersonInfo> _source = new List<PersonInfo>();
             *_source.Add(new PersonInfo("Sven", "Petersen"));
             *_source.Add(new PersonInfo("Cheryl", "Saylor"));
             *_source.Add(new PersonInfo("Dirk", "Luchte"));
             *repositoryItemComboBox1.Bind<PersonInfo>(_source); 
             */
            if (source != null)
            {
                try
                {
                    combox.BeginUpdate();
                    combox.Items.AddRange(source);
                    combox.ParseEditValue += combox_ParseEditValue;
                }
                finally
                {
                    combox.EndUpdate();
                }
            }
        }
        //解决'对象必须实现iconvertible’问题
        private static void combox_ParseEditValue(object sender, ConvertEditValueEventArgs e)
        {
            e.Value = e.Value.ToString();
            e.Handled = true;
        }
    }
}
