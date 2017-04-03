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
using HebianGu.ComLibModule.FileEx;
using HebianGu.ComLibModule.CMD;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using HebianGu.Product.WinHelper.View;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using System.Collections.Specialized;
using HebianGu.ObjectBase.Logger;
using HebianGu.ComLibModule.MatchHelper;

namespace HebianGu.Product.WinHelper
{
    /// <summary> StartWindow.xaml 的交互逻辑 </summary>
    public partial class StartWindow : Window
    {

        #region - 成员变量 -

        private NotifyIcon notifyIcon;

        StartWindowViewModel _viewModel = new StartWindowViewModel();

        #endregion


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

        /// <summary> WPF窗口重写 </summary>
        protected override void OnSourceInitialized(EventArgs e)
        {

            base.OnSourceInitialized(e);

            this.win_SourceInitialized(this, e);

            // HTodo  ：添加剪贴板监视 
            System.IntPtr handle = (new System.Windows.Interop.WindowInteropHelper(this)).Handle;

            AddClipboardFormatListener(handle);

        }

        /// <summary> 添加监视消息 </summary>
        void win_SourceInitialized(object sender, EventArgs e)
        {

            HwndSource hwndSource = PresentationSource.FromVisual(this) as HwndSource;

            if (hwndSource != null)
            {
                hwndSource.AddHook(new HwndSourceHook(WndProc));
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
                    {
                        if (isclipDoubleClick <= 0)
                        {
                            if (ClipboardChangedHandle != null)
                            {
                                // HTodo  ：触发剪贴板变化事件 
                                ClipboardChangedHandle.Invoke();
                            }
                        }

                        isclipDoubleClick--;
                    }
                    break;
            }

            return IntPtr.Zero;
        }

        #endregion

        public StartWindow()
        {
            InitializeComponent();

            this.DataContext = _viewModel;

            this.RegisterNotify();

            this.RegisterAPI();

        }

        /// <summary> 注册托盘 </summary>
        void RegisterNotify()
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

            ContextMenuStrip m = new ContextMenuStrip();
            ToolStripItem t = new ToolStripMenuItem();
            t.Text = "退出";
            t.Click += (object ss, EventArgs ee) =>
            {
                this.Close();
            };
            this.notifyIcon.ContextMenuStrip = m;
            this.notifyIcon.ContextMenuStrip.Items.Add(t);
        }

        /// <summary> 注册监视快捷键 </summary>
        void RegisterAPI()
        {
            HookKeyboardEngine.KeyUp += HookKeyboardEngine_KeyDown;
        }

        /// <summary> 取消注册事件 </summary>
        public void DesRegisterAPI()
        {
            HookKeyboardEngine.KeyUp -= HookKeyboardEngine_KeyDown;
        }

        #endregion

        #region - 左侧功能 -

        // Todo ：注册双击Ctrl激活窗体 
        System.Windows.Forms.KeyEventArgs lastArgs = null;

        DateTime lastTime = DateTime.Now;

        /// <summary> 注册事件 </summary>
        private void HookKeyboardEngine_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (lastArgs != null)
            {
                if (e.KeyData == Keys.CapsLock)
                {
                    if (lastArgs.KeyData == Keys.CapsLock)
                    {
                        // Todo ：上一次和这一次都为Ctrl 
                        if ((DateTime.Now - lastTime).TotalSeconds < 0.5)
                        {
                            this.ShowWindow();
                        }
                    }
                }
            }

            lastArgs = e;
            lastTime = DateTime.Now;
        }

        private void notepad_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.notepad.SelectedIndex == -1) return;

            NotePadBindModel m = this.notepad.SelectedItem as NotePadBindModel;

            NotePadWindow w = new NotePadWindow(m);

            w.ShowDialog();
        }

        int isclipDoubleClick = 0;
        /// <summary> 双击列表项 </summary>
        private void clipboard_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (clipboard.SelectedIndex == -1) return;

            ClipBoradBindModel m = clipboard.SelectedItem as ClipBoradBindModel;

            if (m == null) return;

            isclipDoubleClick = 2;

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
                Process.Start("mspaint", m.Detial);
            }

            LogBindModel log = new LogBindModel();
            log.Message = "提示：复制成功！";
            log.Act += () =>
              {
                  System.Windows.MessageBox.Show("剪贴板日志");
              };

            this._viewModel.Log = log;

        }

        /// <summary> 取消注册快捷键 </summary>
        private void checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            this.DesRegisterAPI();
        }

        /// <summary> 注册快捷键 </summary>
        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            //this.RegisterAPI();
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

            this._viewModel.RefreshUI();
        }

        #endregion

        #region - 窗体事件 -

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

        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.ShowWindow();
        }

        /// <summary> 关闭窗口时事件 </summary>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this._viewModel.SaveToFile();
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
            this.ClipboardChangedHandle += this._viewModel.OnClipboardChanged;
        }

        /// <summary> 取消监视剪贴板 </summary>
        private void cbx_monitorClipboard_Unchecked(object sender, RoutedEventArgs e)
        {
            this.ClipboardChangedHandle -= this._viewModel.OnClipboardChanged;
        }


        #endregion

        #region - 日志信息 -

        private void textBlock1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this._viewModel.Log != null && this._viewModel.Log.Act != null)
            {
                this._viewModel.Log.Act.Invoke();
            }
        }

        #endregion

    }
}
