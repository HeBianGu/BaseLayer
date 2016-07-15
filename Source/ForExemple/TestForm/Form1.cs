using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HebianGu.ComLibModule.ControlEx;

namespace TestForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.comboBox1.BindEnum<TestEnum>();
            this.comboBox1.BindEnumItemShowDesc<TestEnum>();
            //this.comboBox1.BindEnumShowDesc<TestEnum>();
            //this.comboBox1.BindEnumItem<TestEnum>();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TestEnum tt = this.comboBox1.GetEnumItemByDesc<TestEnum>();

            MessageBox.Show(tt.ToString());
        }
    }

    enum TestEnum
    {
        [Description("序号1")]
        zero = 0,
        [Description("序号2")]
        one = 1,
        [Description("序号3")]
        two = 2
    }
}
