using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HebianGu.Product.WinHelper.ViewModel;

namespace HebianGu.Product.WinHelper.WeatherControl
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new DateTime2Service();
            ////创建一个数据库文件
            //string datasource = "database.db";
            //if (!File.Exists(datasource))
            //{
            //    System.Data.SQLite.SQLiteConnection.CreateFile(datasource);
            //}
            ////连接数据库

            //System.Data.SQLite.SQLiteConnection conn =

            //    new System.Data.SQLite.SQLiteConnection();

            //System.Data.SQLite.SQLiteConnectionStringBuilder connstr =

            //    new System.Data.SQLite.SQLiteConnectionStringBuilder();

            //connstr.DataSource = datasource;

            //connstr.Password = "admin";//设置密码，SQLite ADO.NET实现了数据库密码保护

            //conn.ConnectionString = connstr.ToString();

            //conn.Open();
            //try
            //{
            //    System.Data.SQLite.SQLiteCommand cmd = new System.Data.SQLite.SQLiteCommand();
            //    cmd.Connection = conn;

            //    string sql = "INSERT INTO Note([Title],[Content],[UserID]) VALUES('愚人节趣事_600字','今天，我刚一起床，老妈便喊：“昊昊，都几点了？你怎么才起床，马上要上早读了！”，我一听，哎呀呀呀呀呀，遭了今天可是“老班”（对一班之长的简称）负责早读，那个“魔鬼女侠”可厉害了，要是迟到，他肯定要骂死我也，我不管三七二十一，迅速起床叠被，洗漱完毕，就到饭桌面前了，老妈哈哈大笑，这时，我才想起今天是愚人节，我昨天还说要谨慎一点，不要被人骗呢！哎！真不愧是“姜还是老的辣”。',1)";

            //    cmd.CommandText = sql;

            //    cmd.ExecuteNonQuery();
            //}
            //catch (Exception ex)
            //{
            //    string a = ex.Message;
            //}

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void btn_close_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void btn_mini_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("最小化");
        }


        private bool inMouseSelectionMode = false;

        private List<ListBoxItem> selectedItems = new List<ListBoxItem>();

        private void lbItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            // MouseDown时清空已选Item

            // 同时开始"inMouseSelectionMode"

            foreach (var item in selectedItems)
            {

                item.ClearValue(ListBoxItem.BackgroundProperty);

                item.ClearValue(TextElement.ForegroundProperty);

            }

            selectedItems.Clear();

            inMouseSelectionMode = true;

        }

        private void lbItem_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {

            // MouseUp时停止"inMouseSelectionMode"

            ListBoxItem mouseUpItem = sender as ListBoxItem;

            inMouseSelectionMode = false;

        }

        private void lbItem_PreviewMouseMove(object sender, MouseEventArgs e)
        {

            ListBoxItem mouseOverItem = sender as ListBoxItem;

            if (mouseOverItem != null && inMouseSelectionMode && e.LeftButton == MouseButtonState.Pressed)
            {

                // Mouse所在的Item设置高亮

                mouseOverItem.Background = SystemColors.HighlightBrush;

                mouseOverItem.SetValue(TextElement.ForegroundProperty, SystemColors.HighlightTextBrush);

                if (!selectedItems.Contains(mouseOverItem)) { selectedItems.Add(mouseOverItem); }

            }

        }
    }
}
