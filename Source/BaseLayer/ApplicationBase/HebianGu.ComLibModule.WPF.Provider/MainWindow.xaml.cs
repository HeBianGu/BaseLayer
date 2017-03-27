using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HebianGu.ComLibModule.WPF.Provider
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModel _viewModel;
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = _viewModel = new ViewModel();

            RunMethod();
        }


        /// <summary> 此方法的说明 </summary>
        public void RunMethod()
        {
            List<Menu> s = new List<Menu>();

            Menu t1 = new Menu();
            t1.ID = 1;
            t1.ParentID = 0;
            t1.Name = "111";

            Menu t2 = new Menu();
            t2.ID = 2;
            t2.ParentID = 1;
            t2.Name = "222";

            Menu t3 = new Menu();
            t3.ID = 3;
            t3.ParentID = 1;
            t3.Name = "333";

            Menu t4 = new Menu();
            t4.ID = 4;
            t4.ParentID = 3;
            t4.Name = "444";

            Menu t5 = new Menu();
            t5.ID = 5;
            t5.ParentID = 3;
            t5.Name = "555";


            s.Add(t1);
            s.Add(t2);
            s.Add(t3);
            s.Add(t4);
            s.Add(t5);


            List<Menu> m = this.List2ChildList(s);

            _viewModel.MyProperty = m;
         
        }

        class ViewModel : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;
            public void RaisePropertyChanged(string name)
            {
                if (PropertyChanged != null)
                    this.PropertyChanged(this, new PropertyChangedEventArgs(name));
            }

            public List<Menu> MyProperty { get; set; }
        }

        List<Menu> List2ChildList(List<Menu> menuList)
        {
            foreach (var item in menuList)
            {
                Action<Menu> SetChildren = null;

                SetChildren = parent =>

                {
                    parent.Children = menuList
                        .Where(childItem => childItem.ParentID == parent.ID)
                        .ToList();

                    //为每个子项递归调用SetChildren方法。
                    parent.Children.ForEach(SetChildren);
                };

                //初始化层次结构列表以root级别的项目
                List<Menu> hierarchicalItems = menuList
                    .Where(rootItem => rootItem.ParentID == 0)
                    .ToList();

                //调用SetChildren方法来设置子项的每一根级别项目。
                hierarchicalItems.ForEach(SetChildren);

                return hierarchicalItems;
            }
            return new List<Menu>();
        }
    }

    public class Menu
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ParentID { get; set; }

        #region 拓展
        public List<Menu> Children { get; set; }
        #endregion
    }
}
