using HebianGu.Product.WinHelper.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.Diagnostics;
using HebianGu.ComLibModule.Serialize.Json;
using HebianGu.ComLibModule.API;
using HebianGu.ObjectBase.ObjectHelper;
using HebianGu.ComLibModule.EnumHelper;
using HebianGu.ComLibModule.FileHelper;
using HebianGu.ComLibModule.WinHelper;
using HebianGu.ComLibModule.CMD;

namespace HebianGu.Product.WinHelper
{
    /// <summary>
    /// StartWindow.xaml 的交互逻辑
    /// </summary>
    public partial class StartWindow : Window
    {
        StartWindowViewModel _viewModel = new StartWindowViewModel();
        public StartWindow()
        {
            InitializeComponent();

            this.DataContext = _viewModel;

            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SysTemConfiger.ConfigerFolder);

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            configerPath = System.IO.Path.Combine(filePath, SysTemConfiger.ConfigerMyFiles);

            this.RegisterNotify();

            this.LoadToList();

            this.RegisterAPI();

            this.RefreshSysSource();

            this.RefreshFavoritesSource();
        }


        /// <summary> 此方法的说明 </summary>
        public void RegisterNotify()
        {

            this.notifyIcon = new NotifyIcon();
            this.notifyIcon.BalloonTipText = "系统监控中... ...";
            this.notifyIcon.ShowBalloonTip(2000);
            this.notifyIcon.Text = "系统监控中... ...";
            this.notifyIcon.Icon = new System.Drawing.Icon(@"../../skin/Feather.ico");
            this.notifyIcon.Visible = true;

            this.notifyIcon.MouseDoubleClick += (object sender, System.Windows.Forms.MouseEventArgs e) =>
            {
                this.ShowWindow();
            };
        }


        private NotifyIcon notifyIcon;

        /// <summary> 此方法的说明 </summary>
        public void RegisterAPI()
        {
            //HookKeyboardEngine.KeyUp += HookKeyboardEngine_KeyDown;
        }

        public void DesRegisterAPI()
        {
            HookKeyboardEngine.KeyUp -= HookKeyboardEngine_KeyDown;
        }

        // Todo ：注册双击Ctrl激活窗体 
        System.Windows.Forms.KeyEventArgs lastArgs = null;

        DateTime lastTime = DateTime.Now;

        private void HookKeyboardEngine_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (lastArgs != null)
            {
                if (e.KeyData == Keys.LShiftKey)
                {
                    if (lastArgs.KeyData == Keys.LControlKey)
                    {
                        // Todo ：上一次和这一次都为Ctrl 
                        if ((DateTime.Now - lastTime).TotalSeconds < 0.5)
                        {
                            if (this.IsActive)
                            {
                                this.HideWindow();
                            }
                            else
                            {

                                this.ShowWindow();
                            }
                        }
                    }
                }
            }

            lastArgs = e;
            lastTime = DateTime.Now;
        }

        private void ShowWindow()
        {
            this.Visibility = System.Windows.Visibility.Visible;
            this.ShowInTaskbar = true;
            this.Topmost = true;
            this.WindowState = WindowState.Normal;
            this.Activate();
        }

        private void HideWindow()
        {
            this.ShowInTaskbar = false;
            this.Visibility = System.Windows.Visibility.Hidden;
        }

        public void Click_AddFolder(object sender, RoutedEventArgs e)
        {

            FolderBrowserDialog open = new FolderBrowserDialog();

            if (open.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            string filePath = open.SelectedPath;

            FileBindModel m = new FileBindModel(filePath);
            if (string.IsNullOrEmpty(m.FilePath))return;
            _viewModel.Add(m);

            this.RefreshUI();

            this.SaveToFile();

        }

        public void Click_Delete(object sender, RoutedEventArgs e)
        {
            if (this.listBox.SelectedIndex == -1) return;

            FileBindModel m = this.listBox.SelectedItem as FileBindModel;

            _viewModel.BkSource.Remove(m);

            this.RefreshUI();

            this.SaveToFile();

        }

        public void Click_AddFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            if (open.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            string filePath = open.FileName;

            FileBindModel m = new FileBindModel(filePath);
            if (string.IsNullOrEmpty(m.FilePath)) return;
            _viewModel.Add(m);

            this.RefreshUI();

            this.SaveToFile();


        }

        public void RefreshUI()
        {
            //////var b= this.grid.GetBindingExpression(System.Windows.Controls.DataGrid.ItemsSourceProperty);
            ////// b.UpdateTarget();

            this.listBox.ItemsSource = null;

            this.listBox.ItemsSource = _viewModel.BkSource.OrderByDescending(l => l.LastTime);

       
            this.RefreshLastSource();


        }

        public void RefreshLastSource()
        {
            string recent = Environment.GetFolderPath(Environment.SpecialFolder.Recent);

            var files = Directory.GetFiles(recent);
            _viewModel.LastSource.Clear();
            foreach (var item in files)
            {
                FileBindModel f = new FileBindModel(item);

                if (string.IsNullOrEmpty(f.FilePath)) continue;
                _viewModel.LastSource.Add(f);
            }

            this.last.ItemsSource = null;
            this.last.ItemsSource = _viewModel.LastSource;


        }

        public void RefreshFavoritesSource()
        {
            string recent = Environment.GetFolderPath(Environment.SpecialFolder.Favorites);

            var files = recent.GetAllFile();

            _viewModel.FavoritesSource.Clear();
            foreach (var item in files)
            {
                FileBindModel f = new FileBindModel(item);
                if (string.IsNullOrEmpty(f.FilePath)) continue;
                _viewModel.FavoritesSource.Add(f);
            }
        }

        public void RefreshSysSource()
        {
            var names = Enum.GetNames(typeof(Environment.SpecialFolder));

            _viewModel.SysSource.Clear();

            foreach (var item in names)
            {
                Environment.SpecialFolder e = item.GetEnumByNameOrValue<Environment.SpecialFolder>();

                //string recent = Environment.GetFolderPath(e);

                string recent= WinSysHelper.Instance.GetSystemPath(e);

                FileBindModel f = new FileBindModel(recent);
                f.FileName = item;
                if (string.IsNullOrEmpty(f.FilePath)) continue;
                _viewModel.SysSource.Add(f);
            }
        }


        string configerPath;

        /// <summary> 此方法的说明 </summary>
        public void SaveToFile()
        {
            string s = _viewModel.BkSource.JsonSerialize<List<FileBindModel>>();

            File.WriteAllText(configerPath, s);
        }

        public void LoadToList()
        {
            if (!File.Exists(configerPath)) return;

            string s = File.ReadAllText(configerPath);

            List<FileBindModel> b = s.JsonDeserialize<List<FileBindModel>>();

            if (b == null || b.Count == 0) return;

            this._viewModel.BkSource = b;

            this.RefreshUI();
        }


        private void checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            this.DesRegisterAPI();
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            this.RegisterAPI();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch
            {

            }

        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void maxButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void mniButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void menuButton_Click(object sender, RoutedEventArgs e)
        {
            Menu.IsOpen = true;
        }

        private void listBox_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.listBox.ContextMenu.Visibility = Visibility.Visible;
        }

        private void listBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Controls.ListBox box = sender as System.Windows.Controls.ListBox;

            if (box.SelectedIndex == -1) return;

            FileBindModel m = box.SelectedItem as FileBindModel;

            if (m == null) return;

            if (string.IsNullOrEmpty(m.FilePath)) return;

            m.LastTime = DateTime.Now;

            Process.Start(m.FilePath);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.SaveToFile();
        }

        private void mycompute_Click(object sender, RoutedEventArgs e)
        {
            CmdAPI.RunCmd(CmdStr.CmdMyCompute, null, true);

            //Process.Start(CmdStr.CmdControl,null,true);
        }
    }
}
