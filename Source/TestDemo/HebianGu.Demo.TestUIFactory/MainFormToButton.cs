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

namespace HebianGu.Demo.TestUIFactory
{
    /// <summary> 将程序集下的所有窗体转换成功能按钮 </summary>
    public partial class MainFormToButton : Form
    {
        Assembly _ass = null;
        public MainFormToButton(Assembly ass)
        {
            InitializeComponent();

            _ass = ass;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //Assembly ass = Assembly.GetExecutingAssembly();

            Type[] classes = _ass.GetTypes();

            classes = classes.ToList().FindAll(l => typeof(Form).IsAssignableFrom(l)).ToArray();

            if (classes == null || classes.Length == 0)
            {
                return;
            }

            classes.OrderBy(l => l.Name);


            foreach (Type form in classes)
            {
                //  是否包含无参构造函数
                if (form.GetConstructor(Type.EmptyTypes) != null)
                {
                    Button btn = new Button();

                    btn.Text = form.Name;

                    btn.AutoSize = true;

                    this.flowLayoutPanel1.Controls.Add(btn);

                    btn.Click += (object ss, EventArgs ee) =>
                        {
                            Form f = Activator.CreateInstance(form) as Form;

                            f.Show();
                        };

                }
            }
        }
    }
}
