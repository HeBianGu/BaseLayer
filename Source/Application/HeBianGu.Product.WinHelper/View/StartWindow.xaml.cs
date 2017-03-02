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
using Microsoft.Win32;
using System.Collections.ObjectModel;
using HebianGu.Product.WinHelper.View;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using System.Collections.Specialized;

namespace HebianGu.Product.WinHelper
{
    /// <summary> StartWindow.xaml 的交互逻辑 </summary>
    public partial class StartWindow : Window
    {

        #region - 初始化 -

        #region - 注册全局异常 -

        static StartWindow()
        {
            /// <summary> 注册全局异常 </summary>
            System.Windows.Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;

        }

        /// <summary> 补货全局异常 </summary>
        static void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {

            System.Windows.MessageBox.Show(e.Exception.Message);

            e.Handled = true;
        }


        #endregion

        #region - 开机启动项 -

        /// <summary> 开机启动项 </summary> 
        /// <param name=\"Started\">是否启动</param> 
        /// <param name=\"name\">启动值的名称</param> 
        /// <param name=\"path\">启动程序的路径</param> 
        public void RunWhenStart(bool Started, string name, string path)
        {
            using (RegistryKey hklm = Registry.LocalMachine)
            {
                RegistryKey run = hklm.OpenSubKey(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run\", true);

                if (Started == true)
                {
                    // HTodo  ：已经设置开机启动则返回 
                    if (this.IsAutoRun(name)) return;

                    run.SetValue(name, path);
                }
                else
                {
                    run.DeleteValue(name);
                }
            }
        }

        /// <summary> 检查是否是开机启动 </summary>
        public bool IsAutoRun(string name)
        {
            RegistryKey hklm = Registry.LocalMachine;
            //RegistryKey run = hklm.CreateSubKey(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run\");

            RegistryKey run = hklm.OpenSubKey(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run\");

            object obj = run.GetValue(name);

            if (obj.IsNull()) return false;

            return true;

        }


        #endregion

        #region - 监视剪切板 -

        protected override void OnSourceInitialized(EventArgs e)
        {

            base.OnSourceInitialized(e);

            this.win_SourceInitialized(this, e);

            // HTodo  ：添加剪贴板监视 
            System.IntPtr handle = (new System.Windows.Interop.WindowInteropHelper(this)).Handle;

            AddClipboardFormatListener(handle);

        }

        void win_SourceInitialized(object sender, EventArgs e)
        {

            HwndSource hwndSource = PresentationSource.FromVisual(this) as HwndSource;

            if (hwndSource != null)
            {
                hwndSource.AddHook(new HwndSourceHook(WndProc));
            }

        }

        /// <summary>
        /// 剪贴板内容改变时API函数向windows发送的消息
        /// </summary>
        const int WM_CLIPBOARDUPDATE = 0x031D;

        /// <summary>
        /// windows用于监视剪贴板的API函数
        /// </summary>
        /// <param name="hwnd">要监视剪贴板的窗口的句柄</param>
        /// <returns>成功则返回true</returns>
        [DllImport("user32.dll")]//引用dll,确保API可用
        public static extern bool AddClipboardFormatListener(IntPtr hwnd);

        /// <summary>
        /// 取消对剪贴板的监视
        /// </summary>
        /// <param name="hwnd">监视剪贴板的窗口的句柄</param>
        /// <returns>成功则返回true</returns>
        [DllImport("user32.dll")]//引用dll,确保API可用
        public static extern bool RemoveClipboardFormatListener(IntPtr hwnd);


        /// <summary> 剪贴板内容改变 </summary>
        void OnClipboardChanged()
        {
            // HTodo  ：复制的文件路径 
            string text = System.Windows.Clipboard.GetText();

            if (!string.IsNullOrEmpty(text))
            {
                if (this._viewModel.ClipBoradSource.Count > 0)
                {
                    ClipBoradBindModel last = this._viewModel.ClipBoradSource.First();

                    if (last.Detial != text)
                    {
                        ClipBoradBindModel f = new ClipBoradBindModel(text, ClipBoardType.Text);
                        this._viewModel.ClipBoradSource.Insert(0, f);
                    }
                }
                else
                {
                    ClipBoradBindModel f = new ClipBoradBindModel(text, ClipBoardType.Text);
                    this._viewModel.ClipBoradSource.Insert(0, f);
                }
            }


            // HTodo  ：复制的文件 
            System.Collections.Specialized.StringCollection list = System.Windows.Clipboard.GetFileDropList();

            foreach (var item in list)
            {
                if (Directory.Exists(item) || File.Exists(item))
                {
                    if (this._viewModel.ClipBoradSource.Count > 0)
                    {
                        ClipBoradBindModel last = this._viewModel.ClipBoradSource.First();

                        if (last.Detial != item)
                        {
                            ClipBoradBindModel f = new ClipBoradBindModel(item, ClipBoardType.FileSystem);
                            this._viewModel.ClipBoradSource.Insert(0, f);
                        }
                    }
                    else
                    {
                        ClipBoradBindModel f = new ClipBoradBindModel(item, ClipBoardType.FileSystem);
                        this._viewModel.ClipBoradSource.Insert(0, f);
                    }


                }
            }

            // HTodo  ：复制的图片 
            BitmapSource bit = System.Windows.Clipboard.GetImage();

            if (bit != null)
            {
                if (this._viewModel.ClipBoradSource.Count > 0)
                {
                    ClipBoradBindModel last = this._viewModel.ClipBoradSource.First();

                    if (last.Detial != bit.ToString())
                    {
                        ClipBoradBindModel f = new ClipBoradBindModel(bit.ToString(), ClipBoardType.Image);
                        this._viewModel.ClipBoradSource.Insert(0, f);
                    }
                }
                else
                {
                    ClipBoradBindModel f = new ClipBoradBindModel(bit.ToString(), ClipBoardType.Image);
                    this._viewModel.ClipBoradSource.Insert(0, f);
                }


            }

        }

        Action ClipboardChangedHandle;

        #endregion

        #region - 系统消息 -

        protected virtual IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case WM_CLIPBOARDUPDATE:

                    // HTodo  ：触发剪贴板变化事件 
                    ClipboardChangedHandle.Invoke();
                    break;
            }

            return IntPtr.Zero;
        }

        #endregion

        private NotifyIcon notifyIcon;

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

            notepadPath = System.IO.Path.Combine(filePath, SysTemConfiger.ConfigerNotePadFile);

            clipboardPath = System.IO.Path.Combine(filePath, SysTemConfiger.ConfigerClipBoardFile);

            tempFilePath = System.IO.Path.Combine(filePath, SysTemConfiger.ConfigerTempFile);

            // HTodo  ：检查是否开机自启动 
            _viewModel.IsStart = this.IsAutoRun(this.GetType().FullName);

            this.RegisterNotify();

            this.LoadToList();

            this.LoadNotePad();

            this.LoadClipBoard();

            this.RegisterAPI();

            this.RefreshSysSource();

            this.RefreshFavoritesSource();
        }

        /// <summary> 加载记事本 </summary>
        public void LoadNotePad()
        {
            if (!File.Exists(notepadPath)) return;

            string s = File.ReadAllText(notepadPath);

            ObservableCollection<NotePadBindModel> b = s.JsonDeserialize<ObservableCollection<NotePadBindModel>>();

            if (b == null || b.Count == 0) return;

            this._viewModel.NotePadSource = b;

        }

        /// <summary> 加载记事本 </summary>
        public void LoadClipBoard()
        {
            if (!File.Exists(clipboardPath)) return;

            string s = File.ReadAllText(clipboardPath);

            ObservableCollection<ClipBoradBindModel> b = s.JsonDeserialize<ObservableCollection<ClipBoradBindModel>>();

            if (b == null || b.Count == 0) return;

            this._viewModel.ClipBoradSource = b;

        }

        /// <summary> 注册托盘 </summary>
        public void RegisterNotify()
        {

            this.notifyIcon = new NotifyIcon();
            this.notifyIcon.BalloonTipText = "光速启动";
            this.notifyIcon.ShowBalloonTip(2000);
            this.notifyIcon.Text = "光速启动";
            this.notifyIcon.Icon = new System.Drawing.Icon(@"../../skin/Feather.ico");
            this.notifyIcon.Visible = true;

            this.notifyIcon.MouseDoubleClick += (object sender, System.Windows.Forms.MouseEventArgs e) =>
            {
                this.ShowWindow();
            };
        }

        #endregion

        #region - 左侧功能 -

        /// <summary> 此方法的说明 </summary>
        public void RegisterAPI()
        {
            //HookKeyboardEngine.KeyUp += HookKeyboardEngine_KeyDown;
        }

        /// <summary> 取消注册事件 </summary>
        public void DesRegisterAPI()
        {
            HookKeyboardEngine.KeyUp -= HookKeyboardEngine_KeyDown;
        }

        // Todo ：注册双击Ctrl激活窗体 
        System.Windows.Forms.KeyEventArgs lastArgs = null;

        DateTime lastTime = DateTime.Now;

        /// <summary> 注册事件 </summary>
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

        /// <summary> 显示窗体 </summary>
        private void ShowWindow()
        {
            if (this.Visibility == System.Windows.Visibility.Visible && this.WindowState != WindowState.Minimized)
            {
                this.Visibility = System.Windows.Visibility.Hidden;
                this.WindowState = WindowState.Normal;
                this.HideWindow();
            }
            else
            {

                this.Visibility = System.Windows.Visibility.Visible;
                this.ShowInTaskbar = true;
                this.Topmost = true;
                this.WindowState = WindowState.Normal;
                this.Activate();
            }
        }

        /// <summary> 隐藏窗体 </summary>
        private void HideWindow()
        {
            this.Visibility = System.Windows.Visibility.Hidden;
        }

        /// <summary> 添加文件夹 </summary>
        public void Click_AddFolder(object sender, RoutedEventArgs e)
        {

            FolderBrowserDialog open = new FolderBrowserDialog();

            if (open.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            string filePath = open.SelectedPath;

            FileBindModel m = new FileBindModel(filePath);
            if (string.IsNullOrEmpty(m.FilePath)) return;
            _viewModel.Add(m);

            this.RefreshUI();

            this.SaveToFile();

        }

        /// <summary> 删除文件 </summary>
        public void Click_Delete(object sender, RoutedEventArgs e)
        {
            if (this.listBox.SelectedIndex == -1) return;

            FileBindModel m = this.listBox.SelectedItem as FileBindModel;

            _viewModel.BkSource.Remove(m);

            this.RefreshUI();

            this.SaveToFile();

        }

        /// <summary> 粘贴文件 </summary>
        public void Copy_AddFolder(object sender, RoutedEventArgs e)
        {
            // HTodo  ：复制的文件路径 
            string text = System.Windows.Clipboard.GetText();

            if (!string.IsNullOrEmpty(text))
            {
                if (Directory.Exists(text))
                {
                    FileBindModel f = new FileBindModel(Directory.CreateDirectory(text));
                    this._viewModel.BkSource.Add(f);
                }

                if (File.Exists(text))
                {
                    FileInfo file = new FileInfo(text);
                    FileBindModel f = new FileBindModel(file);

                    this._viewModel.BkSource.Add(f);
                }
            }


            // HTodo  ：复制的文件 
            System.Collections.Specialized.StringCollection list = System.Windows.Clipboard.GetFileDropList();

            foreach (var item in list)
            {

                if (Directory.Exists(item))
                {
                    FileBindModel f = new FileBindModel(Directory.CreateDirectory(item));
                    this._viewModel.BkSource.Add(f);
                }

                if (File.Exists(item))
                {
                    FileInfo file = new FileInfo(item);
                    FileBindModel f = new FileBindModel(file);

                    this._viewModel.BkSource.Add(f);
                }
            }

            this.RefreshUI();

            this.SaveToFile();

        }

        /// <summary> 添加文件 </summary>
        public void Click_AddFile(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog open = new System.Windows.Forms.OpenFileDialog();

            if (open.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            string filePath = open.FileName;

            FileBindModel m = new FileBindModel(filePath);
            if (string.IsNullOrEmpty(m.FilePath)) return;
            _viewModel.Add(m);

            this.RefreshUI();

            this.SaveToFile();


        }

        public void AddNotePad_Click(object sender, RoutedEventArgs e)
        {
            NotePadWindow w = new NotePadWindow();

            bool? r = w.ShowDialog();

            if (r == null) return;

            bool result = r.Value;

            if (result)
            {
                this._viewModel.NotePadSource.Add(w.ViewModel);
            }
        }

        public void DeleteNotePad_Click(object sender, RoutedEventArgs e)
        {
            if (this.notepad.SelectedIndex == -1) return;

            NotePadBindModel m = this.notepad.SelectedItem as NotePadBindModel;

            _viewModel.NotePadSource.Remove(m);
        }


        public void Copyclipboard_Click(object sender, RoutedEventArgs e)
        {
            if (this.clipboard.SelectedIndex == -1) return;

            ClipBoradBindModel m = this.clipboard.SelectedItem as ClipBoradBindModel;

            if (m == null) return;

            if (m.Type == ClipBoardType.FileSystem)
            {
                // HTodo  ：添加到剪贴板中 
                StringCollection c = new StringCollection();
                c.Add(m.Detial);
                System.Windows.Clipboard.SetFileDropList(c);

            }
            else if (m.Type == ClipBoardType.Text)
            {
                System.Windows.Clipboard.SetText(m.Detial);
            }
            else
            {
                throw new Exception("未实现该功能");
                //Process.Start("mspaint", m.Detial);
            }
        }
        public void Openclipboard_Click(object sender, RoutedEventArgs e)
        {
            if (clipboard.SelectedIndex == -1) return;

            ClipBoradBindModel m = clipboard.SelectedItem as ClipBoradBindModel;

            if (m == null) return;

            if (m.Type == ClipBoardType.FileSystem)
            {
                Process.Start(m.Detial);
            }
            else if (m.Type == ClipBoardType.Text)
            {
                File.WriteAllText(this.tempFilePath, m.Detial);
                Process.Start(this.tempFilePath);
            }
            else
            {
                throw new Exception("未实现该功能");
                //Process.Start("mspaint", m.Detial);
            }
        }

        public void Deleteclipboard_Click(object sender, RoutedEventArgs e)
        {
            if (this.clipboard.SelectedIndex == -1) return;

            ClipBoradBindModel m = this.clipboard.SelectedItem as ClipBoradBindModel;

            _viewModel.ClipBoradSource.Remove(m);
        }

        private void notepad_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.notepad.SelectedIndex == -1) return;

            NotePadBindModel m = this.notepad.SelectedItem as NotePadBindModel;

            NotePadWindow w = new NotePadWindow(m);

            w.ShowDialog();
        }

        /// <summary> 双击列表项 </summary>
        private void clipboard_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (clipboard.SelectedIndex == -1) return;

            ClipBoradBindModel m = clipboard.SelectedItem as ClipBoradBindModel;

            if (m == null) return;

            if (m.Type == ClipBoardType.FileSystem)
            {
                Process.Start(m.Detial);

                // HTodo  ：添加到剪贴板中 
                StringCollection c = new StringCollection();

                c.Add(m.Detial);

                System.Windows.Clipboard.SetFileDropList(c);
            }
            else if (m.Type == ClipBoardType.Text)
            {
                File.WriteAllText(this.tempFilePath, m.Detial);

                Process.Start(this.tempFilePath);

                System.Windows.Clipboard.SetText(m.Detial);
            }
            else
            {
                Process.Start("mspaint", m.Detial);
            }


        }

        public void ClearNotePad_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary> 刷新界面数据 </summary>
        public void RefreshUI()
        {
            var collection = _viewModel.BkSource.OrderByDescending(l => l.LastTime);

            ObservableCollection<FileBindModel> temp = new ObservableCollection<FileBindModel>();


            foreach (var item in collection)
            {
                temp.Add(item);
            };

            _viewModel.BkSource = temp;

            this.RefreshLastSource();


        }

        /// <summary> 最近使用的文件 </summary>
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
        }

        /// <summary> 收藏文件夹 </summary>
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

        /// <summary> 系统文件夹 </summary>
        public void RefreshSysSource()
        {
            var names = Enum.GetNames(typeof(Environment.SpecialFolder));

            _viewModel.SysSource.Clear();

            foreach (var item in names)
            {
                Environment.SpecialFolder e = item.GetEnumByNameOrValue<Environment.SpecialFolder>();

                //string recent = Environment.GetFolderPath(e);

                string recent = WinSysHelper.Instance.GetSystemPath(e);

                FileBindModel f = new FileBindModel(recent);
                f.FileName = item;
                if (string.IsNullOrEmpty(f.FilePath)) continue;
                _viewModel.SysSource.Add(f);
            }
        }

        string configerPath;

        string notepadPath;

        string clipboardPath;

        string tempFilePath;

        /// <summary> 保存配置文件 </summary>
        public void SaveToFile()
        {
            string s = _viewModel.BkSource.JsonSerialize<ObservableCollection<FileBindModel>>();

            File.WriteAllText(configerPath, s);

            string n = _viewModel.NotePadSource.JsonSerialize<ObservableCollection<NotePadBindModel>>();

            File.WriteAllText(notepadPath, n);

            string c = _viewModel.ClipBoradSource.JsonSerialize<ObservableCollection<ClipBoradBindModel>>();

            File.WriteAllText(clipboardPath, c);
        }

        /// <summary> 加载配置文件 </summary>
        public void LoadToList()
        {
            if (!File.Exists(configerPath)) return;

            string s = File.ReadAllText(configerPath);

            ObservableCollection<FileBindModel> b = s.JsonDeserialize<ObservableCollection<FileBindModel>>();

            if (b == null || b.Count == 0) return;

            this._viewModel.BkSource = b;

            this.RefreshUI();
        }

        /// <summary> 取消注册快捷键 </summary>
        private void checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            this.DesRegisterAPI();
        }

        /// <summary> 注册快捷键 </summary>
        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            this.RegisterAPI();
        }

        /// <summary> 双击列表项 </summary>
        private void listBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Controls.ListBox box = sender as System.Windows.Controls.ListBox;

            if (box.SelectedIndex == -1) return;

            FileBindModel m = box.SelectedItem as FileBindModel;

            if (m == null) return;

            if (string.IsNullOrEmpty(m.FilePath)) return;

            m.LastTime = DateTime.Now;

            Process.Start(m.FilePath);

            this.RefreshUI();
        }
        #endregion

        #region - 窗体事件 -

        /// <summary> 关闭窗口时事件 </summary>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.SaveToFile();
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

        /// <summary> 关闭窗口 </summary>
        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary> 最大化窗口 </summary>
        private void maxButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        /// <summary> 最小化窗口 </summary>
        private void mniButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void menuButton_Click(object sender, RoutedEventArgs e)
        {
            Menu.IsOpen = true;
        }

        /// <summary> 右键弹出菜单项 </summary>
        private void listBox_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.listBox.ContextMenu.Visibility = Visibility.Visible;
        }

        #endregion

        #region - 快捷按钮 -

        /// <summary> 我的电脑 </summary>
        private void mycompute_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("::{20D04FE0-3AEA-1069-A2D8-08002B30309D}");
        }

        /// <summary> 控制面板 </summary>
        private void mycontrol_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(CmdStr.CmdControl);
        }

        /// <summary> 回收站 </summary>
        private void mydushbin_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("::{645FF040-5081-101B-9F08-00AA002F954E}");
        }

        /// <summary> 注册表 </summary>
        private void myregedit_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("regedit");
        }

        /// <summary> 远程连接 </summary>
        private void mymstsc_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("mstsc");
        }

        /// <summary> 网络测速 </summary>
        private void mynetspeed_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("ping", "www.baidu.com -t");
        }

        /// <summary> 锁定电脑 </summary>
        private void mylock_Click(object sender, RoutedEventArgs e)
        {
            WinAPIServer.Instance.Lock();
        }

        /// <summary> 记事本 </summary>
        private void mynotepad_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("notepad");
        }

        /// <summary> 启动浏览器 </summary>
        private void myexplorer_Click(object sender, RoutedEventArgs e)
        {
            //Process.Start("explore.exe", "https://www.hao123.com/");

            //从注册表中读取默认浏览器可执行文件路径  
            RegistryKey key = Registry.ClassesRoot.OpenSubKey(@"http\shell\open\command\");
            string s = key.GetValue("").ToString();

            //s就是你的默认浏览器，不过后面带了参数，把它截去，不过需要注意的是：不同的浏览器后面的参数不一样！  
            //"D:\Program Files (x86)\Google\Chrome\Application\chrome.exe" -- "%1"  
            System.Diagnostics.Process.Start(s.Substring(0, s.Length - 8), "https://www.hao123.com/");


        }

        /// <summary> 截图工具 </summary>
        private void snippingtool_Click(object sender, RoutedEventArgs e)
        {
            string cutPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SysTemConfiger.ConfigerExtend, SysTemConfiger.ConfigerCut);

            Process.Start(cutPath);


        }





        #endregion

        #region - 配置信息 -

        /// <summary> 勾选开机启动 </summary>
        private void cbx_isstart_Checked(object sender, RoutedEventArgs e)
        {
            this.RunWhenStart(true, this.GetType().FullName, System.Windows.Forms.Application.ExecutablePath);
        }

        /// <summary> 取消开机启动 </summary>
        private void cbx_isstart_Unchecked(object sender, RoutedEventArgs e)
        {
            this.RunWhenStart(false, this.GetType().FullName, System.Windows.Forms.Application.ExecutablePath);
        }


        /// <summary> 监视剪贴板 </summary>
        private void cbx_monitorClipboard_Checked(object sender, RoutedEventArgs e)
        {
            this.ClipboardChangedHandle += this.OnClipboardChanged;
        }

        /// <summary> 取消监视剪贴板 </summary>
        private void cbx_monitorClipboard_Unchecked(object sender, RoutedEventArgs e)
        {
            this.ClipboardChangedHandle -= this.OnClipboardChanged;
        }


        #endregion

        private void listBox_DragEnter(object sender, System.Windows.DragEventArgs e)
        {

            string sss = string.Empty;
            //if (e.Data.GetDataPresent(DataFormats.FileDrop))
            //{
            //    e.Effect = DragDropEffects.Copy;
            //}
            //else
            //{
            //    e.Effect = DragDropEffects.None;
            //}
        }

        private void listBox_Drop(object sender, System.Windows.DragEventArgs e)
        {
            try
            {
                String[] files = e.Data.GetData(System.Windows.DataFormats.FileDrop, false) as String[];

                // Copy file from external application   
                foreach (string srcfile in files)
                {
                    //string destFile = labelCurFolder.Text + " \\ " + System.IO.Path.GetFileName(srcfile);
                    if (System.IO.File.Exists(srcfile))
                    {
                        //if (MessageBox.Show(string.Format(
                        //" This folder already contains a file named {0}," +
                        //"would you like to replace the existing file ",
                        //System.IO.Path.GetFileName(srcfile)),
                        //" Confirm File Replace ",
                        //MessageBoxButtons.YesNo,
                        //MessageBoxIcon.None) != DialogResult.Yes)
                        //{
                        //    continue;
                        //}
                    }

                    //System.IO.File.Copy(srcfile, destFile, true);
                }

                // List current folder   
                //ListFolder();
            }
            catch (Exception e1)
            {
                //MessageBox.Show(e1.Message, " Error ",
                //MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listBox_DragOver(object sender, System.Windows.DragEventArgs e)
        {

        }

        private void listBox_PreviewMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ListBoxItem listBoxItem = listBox.SelectedItem as ListBoxItem;
        }

        private void listBox_QueryContinueDrag(object sender, System.Windows.QueryContinueDragEventArgs e)
        {
            ListBoxItem listBoxItem = listBox.SelectedItem as ListBoxItem;// Find your actual visual you want to drag

            // DragDropAdorner adorner = new DragDropAdorner(listBoxItem);
            //    mAdornerLayer = AdornerLayer.GetAdornerLayer(mTopLevelGrid);
            //          mAdornerLayer.Add(adorner);

            // DataItem dataItem = listBoxItem.Content as DataItem;
            //           DataObject dataObject = new DataObject(dataItem.Clone());
            //         // Here, we should notice that dragsource param will specify on which 
            //System.Windows.DragDrop.DoDragDrop(listBox, dataObject, System.Windows.DragDropEffects.Copy);
        }

    }
}
