using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProperyGridControlDemo
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            List<Student> ss = new List<Student>()
            {
                new Student(){Id="1",Name="lee",Age="22"},
                new Student(){Id="2",Name="lee",Age="22"},
                new Student(){Id="3",Name="lee",Age="22"},
               new Student(){Id="4",Name="lee",Age="22"},
                new Student(){Id="5",Name="lee",Age="22"},
                new Student(){Id="6",Name="lee",Age="22"}
            };
            this.bindingSource1.DataSource = ss;

            InitControl();


        }

        private void InitControl()
        {
            AQUDIMS obj = new AQUDIMS();

            this.propertyGridControl1.SelectedObject = obj;


        }

        private void propertyGridControl1_Click(object sender, EventArgs e)
        {

        }
    }

    public class Student
    {
        string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        string age;

        public string Age
        {
            get { return age; }
            set { age = value; }
        }
    }
}
