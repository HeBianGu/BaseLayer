using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using WpfApplication7;

namespace LifeCalendar.View
{
    /// <summary>
    /// NoteView.xaml 的交互逻辑
    /// </summary>
    public partial class NoteView : UserControl
    {
        public NoteView()
        {
            InitializeComponent();

            NoArgDelegate fetcher = new NoArgDelegate(
               this.FetchWeatherFromServer);

            fetcher.BeginInvoke(null, null);

        }
        ObservableCollection<Note> notes = null;
        private void FetchWeatherFromServer()
        {
            notes = new ObservableCollection<Note>();
            try
            {
                string sql = "select * from note order by id limit " + pageSize + " offset 0";
                Note note = null;
                DataTable t = DBHelper.ExecuteDataTable(sql, null);
                foreach (DataRow item in t.Rows)
                {
                    note = new Note();
                    note.ID = Int32.Parse(item["id"].ToString());
                    note.Title = item["title"].ToString();
                    note.Content = item["content"].ToString();
                    note.CreateTime = (DateTime)item["CreateTime"];
                    note.ModifyTime = (DateTime)item["ModifyTime"];
                    notes.Add(note);
                }
                
                lb.Dispatcher.BeginInvoke(
                   System.Windows.Threading.DispatcherPriority.Normal,
                   new NoArgDelegate(UpdateUserInterface));
            }
            catch (Exception ex)
            {
                string a = ex.Message;
            }
        }
        private void UpdateUserInterface()
        {
            lb.ItemsSource = notes;
        }
        // Delegates to be used in placking jobs onto the Dispatcher.
        private delegate void NoArgDelegate();
        //<SnippetThreadingWeatherDelegates>
        private delegate void OneArgDelegate(dynamic arg);

        int pageIndex = 1;
        int pageSize = 50;
        private void PART_VerticalScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ScrollBar scrollBar = sender as ScrollBar;

            if (scrollBar.Maximum - scrollBar.Value < 100)
            {

                try
                {
                    string sql = "select * from note order by id limit " + pageSize + " offset " + pageIndex * pageSize;
                    pageIndex++;
                    Note note = null;
                    DataTable t = DBHelper.ExecuteDataTable(sql, null);
                    foreach (DataRow item in t.Rows)
                    {
                        note = new Note();
                        note.ID = Int32.Parse(item["id"].ToString());
                        note.Title = item["title"].ToString();
                        note.Content = item["content"].ToString();
                        note.CreateTime = (DateTime)item["CreateTime"];
                        note.ModifyTime = (DateTime)item["ModifyTime"];
                        notes.Add(note);
                    }

                  ///  lb.Dispatcher.BeginInvoke(
                   //    System.Windows.Threading.DispatcherPriority.Normal,
                   //    new NoArgDelegate(UpdateUserInterface));
                }
                catch (Exception ex)
                {
                    string a = ex.Message;
                }
                //  Loaded 事件代码
                //for (int i = 0; i < 100; i++)
                //{
                //    notes.Add(new Note(i, "ccccccccc" + i.ToString(), i.ToString(), DateTime.Now));
                //}
            }
        }
    }
}
