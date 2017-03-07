using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HebianGu.Product.WinHelper.View
{
    /// <summary>
    /// ParseFavoriteWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ParseFavoriteWindow : Window
    {
        public ParseFavoriteWindow(string name,string url)
        {
            InitializeComponent();

            this.textBox.Text = name;

            this.textBox1.Text = url;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        public string FileName
        {
            get
            {

                return this.textBox.Text.Trim();
              
            }
        }



        public string Url
        {
            get
            {

                return this.textBox1.Text.Trim();

            }
        }

    }
}
