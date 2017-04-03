using HebianGu.ComLibModule.API;
using HebianGu.ComLibModule.WinHelper;
using HebianGu.ComLibModule.WPF.Provider;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HebianGu.ComLibModule.FileHelper;
using HebianGu.ComLibModule.Serialize.Json;
using Microsoft.Win32;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Collections.Specialized;
using HebianGu.ComLibModule.CMD;
using HebianGu.ComLibModule.EnumHelper;
using HebianGu.Product.WinHelper.View;

namespace HebianGu.Product.WinHelper.ViewModel
{
    partial class StartWindowViewModel
    {
        public StartWindowViewModel()
        {
            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SysTemConfiger.ConfigerFolder);

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            configerPath = System.IO.Path.Combine(filePath, SysTemConfiger.ConfigerMyFiles);

            notepadPath = System.IO.Path.Combine(filePath, SysTemConfiger.ConfigerNotePadFile);

            clipboardPath = System.IO.Path.Combine(filePath, SysTemConfiger.ConfigerClipBoardFile);

            tempFilePath = System.IO.Path.Combine(filePath, SysTemConfiger.ConfigerTempFile);

            //// HTodo  ：检查是否开机自启动 
            //this.IsStart = this.this(this.GetType().FullName);

            this.RegisterLog();

            //this.RegisterNotify();

            this.LoadToList();

            this.LoadNotePad();

            this.LoadClipBoard();

            this.LoadPrograms();

            this.LoadExtend();

            //this.RegisterAPI();

            this.RefreshSysSource();

            this.RefreshFavoritesSource();
        }

        #region - 成员变量 -

        string configerPath;

        string notepadPath;

        string clipboardPath;

        string tempFilePath;


        #endregion

        #region - 绑定属性 -

        private bool _checked = true;

        /// <summary> 是否启用快捷键 </summary>
        public bool Checked
        {
            get { return _checked; }
            set
            {
                _checked = value;
                RaisePropertyChanged("Checked");
            }
        }

        private bool _isStart = true;

        /// <summary> 是否开机自启动 </summary>
        public bool IsStart
        {
            get { return _isStart; }
            set
            {
                _isStart = value;
                RaisePropertyChanged("IsStart");
            }
        }

        private bool _isMoniterClip = true;

        /// <summary> 是否开机自启动 </summary>
        public bool IsMoniterClip
        {
            get { return _isMoniterClip; }
            set
            {
                _isMoniterClip = value;
                RaisePropertyChanged("IsMoniterClip");
            }
        }

        private LogBindModel _log;
        /// <summary> 显示的日志 </summary>
        public LogBindModel Log
        {
            get { return _log; }
            set
            {
                _log = value;
                RaisePropertyChanged("Log");
            }
        }

        private ObservableCollection<FileBindModel> _bkSource = new ObservableCollection<FileBindModel>();

        /// <summary> 常用文件 </summary>
        public ObservableCollection<FileBindModel> BkSource
        {
            get { return _bkSource; }
            set
            {
                _bkSource = value;
                RaisePropertyChanged("BkSource");
            }
        }

        private ObservableCollection<FileBindModel> _lastSource = new ObservableCollection<FileBindModel>();

        /// <summary> 最近使用的文件 </summary>
        public ObservableCollection<FileBindModel> LastSource
        {
            get { return _lastSource; }
            set
            {
                _lastSource = value;
                RaisePropertyChanged("LastSource");
            }
        }

        private ObservableCollection<FileBindModel> _favoritesSource = new ObservableCollection<FileBindModel>();

        /// <summary> 收藏夹 </summary>
        public ObservableCollection<FileBindModel> FavoritesSource
        {
            get { return _favoritesSource; }
            set
            {
                _favoritesSource = value;
                RaisePropertyChanged("FavoritesSource");
            }
        }

        private ObservableCollection<FileBindModel> _sysSource = new ObservableCollection<FileBindModel>();

        /// <summary> 系统文件 </summary>
        public ObservableCollection<FileBindModel> SysSource
        {
            get { return _sysSource; }
            set
            {
                _sysSource = value;
                RaisePropertyChanged("SysSource");
            }
        }

        private ObservableCollection<ClipBoradBindModel> _clipBoradSource = new ObservableCollection<ClipBoradBindModel>();

        /// <summary> 剪贴板信息 </summary>
        public ObservableCollection<ClipBoradBindModel> ClipBoradSource
        {
            get { return _clipBoradSource; }
            set
            {
                _clipBoradSource = value;
                RaisePropertyChanged("ClipBoradSource");
            }
        }

        private ObservableCollection<NotePadBindModel> _notePadSource = new ObservableCollection<NotePadBindModel>();

        /// <summary> 记事本 </summary>
        public ObservableCollection<NotePadBindModel> NotePadSource
        {
            get { return _notePadSource; }
            set
            {
                _notePadSource = value;
                RaisePropertyChanged("NotePadSource");
            }
        }

        private ObservableCollection<FileBindModel> _programSource = new ObservableCollection<FileBindModel>();

        /// <summary> 系统资源 </summary>
        public ObservableCollection<FileBindModel> ProgramSource
        {
            get { return _programSource; }
            set
            {
                _programSource = value;
                RaisePropertyChanged("ProgramSource");
            }
        }

        private ObservableCollection<FileBindModel> _extendSource = new ObservableCollection<FileBindModel>();

        /// <summary> 扩展工具 </summary>
        public ObservableCollection<FileBindModel> ExtendSource
        {
            get { return _extendSource; }
            set
            {
                _extendSource = value;
                RaisePropertyChanged("ExtendSource");
            }
        }


        private FileBindModel _selectFast;
        /// <summary> 说明 </summary>
        public FileBindModel SelectFast
        {
            get { return _selectFast; }
            set
            {
                _selectFast = value;
                RaisePropertyChanged("SelectFast");
            }
        }

        private FileBindModel _selectSystem;
        /// <summary> 说明 </summary>
        public FileBindModel SelectSystem
        {
            get { return _selectSystem; }
            set
            {
                _selectSystem = value;
                RaisePropertyChanged("SelectSystem");
            }
        }

        private FileBindModel _selectTool;
        /// <summary> 说明 </summary>
        public FileBindModel SelectTool
        {
            get { return _selectTool; }
            set
            {
                _selectTool = value;
                RaisePropertyChanged("SelectTool");
            }
        }

        private FileBindModel _selectLast;
        /// <summary> 说明 </summary>
        public FileBindModel SelectLast
        {
            get { return _selectLast; }
            set
            {
                _selectLast = value;
                RaisePropertyChanged("SelectLast");
            }
        }

        private FileBindModel _selectProgram;
        /// <summary> 说明 </summary>
        public FileBindModel SelectProgram
        {
            get { return _selectProgram; }
            set
            {
                _selectProgram = value;
                RaisePropertyChanged("SelectProgram");
            }
        }


        private ClipBoradBindModel _selectClipBoard;
        /// <summary> 说明 </summary>
        public ClipBoradBindModel SelectClipBoard
        {
            get { return _selectClipBoard; }
            set
            {
                _selectClipBoard = value;
                RaisePropertyChanged("SelectClipBoard");
            }
        }

        private NotePadBindModel _selectNotePad;
        /// <summary> 说明 </summary>
        public NotePadBindModel SelectNotePad
        {
            get { return _selectNotePad; }
            set
            {
                _selectNotePad = value;
                RaisePropertyChanged("SelectNotePad");
            }
        }

        private FileBindModel _selectFavorite;
        /// <summary> 说明 </summary>
        public FileBindModel SelectFavorite
        {
            get { return _selectFavorite; }
            set
            {
                _selectFavorite = value;
                RaisePropertyChanged("SelectFavorite");
            }
        }

        #endregion

        #region - 菜单命令 -

        private DelegateCommand _addFolderCommand = new DelegateCommand();
        /// <summary> 添加常用文件文件夹命令 </summary>
        public DelegateCommand AddFolderCommand
        {
            get
            {
                if (_addFolderCommand.Excute == null)
                {
                    _addFolderCommand.Excute = () =>
                      {
                          FolderBrowserDialog open = new FolderBrowserDialog();

                          if (open.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

                          string filePath = open.SelectedPath;

                          FileBindModel m = new FileBindModel(filePath);
                          if (string.IsNullOrEmpty(m.FilePath)) return;
                          this._bkSource.Add(m);

                          this.RefreshUI();

                          this.SaveToFile();
                      };

                }

                return _addFolderCommand;
            }
        }

        private DelegateCommand _openFast = new DelegateCommand();
        /// <summary> 打开常用文件 </summary>
        public DelegateCommand OpenFast
        {
            get
            {
                if (_openclipboard.Excute == null)
                {
                    _openclipboard.Excute = () =>
                    {
                        if (_selectFast == null) return;

                        Process.Start(_selectFast.FilePath);

                    };
                }
                return _openclipboard;
            }
        }

        private DelegateCommand _deleteFast = new DelegateCommand();
        /// <summary> 删除常用文件 </summary>
        public DelegateCommand DeleteFast
        {
            get
            {
                if (_deleteFast.Excute == null)
                {
                    _deleteFast.Excute = () =>
                    {
                        if (_selectFast == null) return;

                        this.BkSource.Remove(_selectFast);

                        this.RefreshUI();

                        this.SaveToFile();
                    };
                }
                return _deleteFast;
            }
        }

        private DelegateCommand _copyFase = new DelegateCommand();
        /// <summary> 粘贴添加常用文件 </summary>
        public DelegateCommand CopyFase
        {
            get
            {
                if (_copyFase.Excute == null)
                {
                    _copyFase.Excute = () =>
                    {
                        this.Copy_AddFolder();
                    };
                }
                return _copyFase;
            }
        }
        void Copy_AddFolder()
        {
            // HTodo  ：复制的文件路径 
            string text = System.Windows.Clipboard.GetText();

            if (!string.IsNullOrEmpty(text))
            {
                if (Directory.Exists(text))
                {
                    FileBindModel f = new FileBindModel(Directory.CreateDirectory(text));
                    this.BkSource.Add(f);
                }

                if (File.Exists(text))
                {
                    FileInfo file = new FileInfo(text);
                    FileBindModel f = new FileBindModel(file);

                    this.BkSource.Add(f);
                }
            }


            // HTodo  ：复制的文件 
            System.Collections.Specialized.StringCollection list = System.Windows.Clipboard.GetFileDropList();

            foreach (var item in list)
            {

                if (Directory.Exists(item))
                {
                    FileBindModel f = new FileBindModel(Directory.CreateDirectory(item));
                    this.BkSource.Add(f);
                }

                if (File.Exists(item))
                {
                    FileInfo file = new FileInfo(item);
                    FileBindModel f = new FileBindModel(file);

                    this.BkSource.Add(f);
                }
            }

            this.RefreshUI();

            this.SaveToFile();

        }

        private DelegateCommand _addFastFile = new DelegateCommand();
        /// <summary> 添加常用文件 </summary>
        public DelegateCommand AddFastFile
        {
            get
            {
                if (_addFastFile.Excute == null)
                {
                    _addFastFile.Excute = () =>
                    {
                        System.Windows.Forms.OpenFileDialog open = new System.Windows.Forms.OpenFileDialog();

                        if (open.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

                        string filePath = open.FileName;

                        FileBindModel m = new FileBindModel(filePath);
                        if (string.IsNullOrEmpty(m.FilePath)) return;
                        this._bkSource.Add(m);

                        this.RefreshUI();

                        this.SaveToFile();
                    };
                }
                return _addFastFile;
            }
        }
        void Click_AddFile(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog open = new System.Windows.Forms.OpenFileDialog();

            if (open.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            string filePath = open.FileName;

            FileBindModel m = new FileBindModel(filePath);
            if (string.IsNullOrEmpty(m.FilePath)) return;
            this._bkSource.Add(m);

            this.RefreshUI();

            this.SaveToFile();


        }

        private DelegateCommand _AddNotePad = new DelegateCommand();
        /// <summary> 增加记事本 </summary>
        public DelegateCommand AddNotePad
        {
            get
            {
                if (_AddNotePad.Excute == null)
                {
                    _AddNotePad.Excute = () =>
                    {
                        NotePadWindow w = new NotePadWindow();

                        bool? r = w.ShowDialog();

                        if (r == null) return;

                        bool result = r.Value;

                        if (result)
                        {
                            this.NotePadSource.Add(w.ViewModel);
                        }
                    };
                }
                return _AddNotePad;
            }
        }

        private DelegateCommand _deleteNotePad = new DelegateCommand();
        /// <summary> 删除记事本 </summary>
        public DelegateCommand DeleteNotePad
        {
            get
            {
                if (_deleteNotePad.Excute == null)
                {
                    _deleteNotePad.Excute = () =>
                    {
                        if (_selectNotePad == null) return;

                        this.NotePadSource.Remove(_selectNotePad);
                    };
                }
                return _deleteNotePad;
            }
        }

        private DelegateCommand _copyClipBoard = new DelegateCommand();
        /// <summary> 添加到剪贴板 </summary>
        public DelegateCommand CopyClipBoard
        {
            get
            {
                if (_copyClipBoard.Excute == null)
                {
                    _copyClipBoard.Excute = () =>
                    {
                        if (_selectClipBoard == null) return;

                        if (_selectClipBoard.Type == ClipBoardType.FileSystem)
                        {
                            // HTodo  ：添加到剪贴板中 
                            StringCollection c = new StringCollection();
                            c.Add(_selectClipBoard.Detial);
                            System.Windows.Clipboard.SetFileDropList(c);

                        }
                        else if (_selectClipBoard.Type == ClipBoardType.Text)
                        {
                            System.Windows.Clipboard.SetText(_selectClipBoard.Detial);
                        }
                        else
                        {
                            throw new Exception("未实现该功能");
                            //Process.Start("mspaint", m.Detial);
                        }
                    };
                }
                return _copyClipBoard;
            }
        }

        private DelegateCommand _openclipboard = new DelegateCommand();
        /// <summary> 打开剪贴板文件 </summary>
        public DelegateCommand Openclipboard
        {
            get
            {
                if (_openclipboard.Excute == null)
                {
                    _openclipboard.Excute = () =>
                    {
                        if (_selectClipBoard == null) return;


                        if (_selectClipBoard == null) return;

                        if (_selectClipBoard.Type == ClipBoardType.FileSystem)
                        {
                            Process.Start(_selectClipBoard.Detial);
                        }
                        else if (_selectClipBoard.Type == ClipBoardType.Text)
                        {
                            File.WriteAllText(this.tempFilePath, _selectClipBoard.Detial);
                            Process.Start(this.tempFilePath);
                        }
                        else
                        {
                            throw new Exception("未实现该功能");
                            //Process.Start("mspaint", m.Detial);
                        }
                    };
                }
                return _openclipboard;
            }
        }

        private DelegateCommand _deleteclipboard = new DelegateCommand();
        /// <summary> 删除剪贴板文件 </summary>
        public DelegateCommand Deleteclipboard
        {
            get
            {
                if (_deleteclipboard.Excute == null)
                {
                    _deleteclipboard.Excute = () =>
                    {
                        if (_deleteclipboard.IsNull()) return;

                        this.ClipBoradSource.Remove(_selectClipBoard);
                    };
                }
                return _deleteclipboard;
            }
        }

        private DelegateCommand _parsefavorite = new DelegateCommand();
        /// <summary> 粘贴添加收藏夹 </summary>
        public DelegateCommand Parsefavorite
        {
            get
            {
                if (_parsefavorite.Excute == null)
                {
                    _parsefavorite.Excute = () =>
                    {
                        this.Parsefavorite_Click();
                    };
                }
                return _parsefavorite;
            }
        }

        void Parsefavorite_Click()
        {
            // HTodo  ：复制的文件路径 
            string text = System.Windows.Clipboard.GetText();

            if (!string.IsNullOrEmpty(text))
            {
                //if (text.IsURL()||text.IsHTML
                if (Uri.IsWellFormedUriString(text, UriKind.RelativeOrAbsolute))
                {
                    string favorite = Environment.GetFolderPath(Environment.SpecialFolder.Favorites);

                    ParseFavoriteWindow w = new ParseFavoriteWindow(text, text);

                    if (w.ShowDialog() != null && w.DialogResult.Value)
                    {
                        string path = System.IO.Path.Combine(favorite, w.FileName + ".url");

                        if (!File.Exists(path))
                        {
                            string urlstr = string.Format(@"[InternetShortcut]
URL = {0}", w.Url);
                            File.WriteAllText(path, urlstr);
                        }
                    }
                }

                // HTodo  ：复制的文件 
                System.Collections.Specialized.StringCollection list = System.Windows.Clipboard.GetFileDropList();

                foreach (var item in list)
                {

                    if (System.IO.Path.GetExtension(item).ToUpper() == ".URL")
                    {
                        string favorite = Environment.GetFolderPath(Environment.SpecialFolder.Favorites);

                        string path = System.IO.Path.Combine(favorite, System.IO.Path.GetFileName(item));

                        if (!File.Exists(path))
                        {
                            File.Copy(item, path);
                        }
                    }
                }

                this.RefreshFavoritesSource();
            }

        }

        private DelegateCommand _openfavorite = new DelegateCommand();
        /// <summary> 打开收藏夹文件 </summary>
        public DelegateCommand Openfavorite
        {
            get
            {
                if (_openfavorite.Excute == null)
                {
                    _openfavorite.Excute = () =>
                    {
                        if (this._selectFavorite == null) return;

                        Process.Start(this._selectFavorite.FilePath);
                    };
                }
                return _openfavorite;
            }
        }

        private DelegateCommand _deletefavorite = new DelegateCommand();
        /// <summary> 删除收藏夹文件 </summary>
        public DelegateCommand Deletefavorite
        {
            get
            {
                if (_deletefavorite.Excute == null)
                {
                    _deletefavorite.Excute = () =>
                    {
                        if (_selectFavorite == null) return;

                        File.Delete(_selectFavorite.FilePath);

                        this.RefreshFavoritesSource();
                    };
                }
                return _deletefavorite;
            }
        }

        #endregion

        #region - 快捷按钮 -

        DelegateCommand _myCompute = new DelegateCommand();
        /// <summary> 我的电脑 </summary>
        public DelegateCommand MyCompute
        {
            get
            {
                if (_myCompute.Excute == null)
                {
                    _myCompute.Excute = () =>
                    {
                        Process.Start("::{20D04FE0-3AEA-1069-A2D8-08002B30309D}");
                    };
                }
                return _myCompute;
            }
        }

        DelegateCommand _myControl = new DelegateCommand();
        /// <summary> 控制面板 </summary>
        public DelegateCommand MyControl
        {
            get
            {
                if (_myControl.Excute == null)
                {
                    _myControl.Excute = () =>
                    {
                        Process.Start(CmdStr.CmdControl);
                    };
                }
                return _myControl;
            }
        }

        private DelegateCommand _mydushbin = new DelegateCommand();
        /// <summary> 回收站 </summary>
        public DelegateCommand Mydushbin
        {
            get
            {
                if (_mydushbin.Excute == null)
                {
                    _mydushbin.Excute = () =>
                    {
                        Process.Start("::{645FF040-5081-101B-9F08-00AA002F954E}");
                    };
                }
                return _mydushbin;
            }
        }

        private DelegateCommand _myregedit = new DelegateCommand();
        /// <summary> 注册表 </summary>
        public DelegateCommand Myregedit
        {
            get
            {
                if (_myregedit.Excute == null)
                {
                    _myregedit.Excute = () =>
                    {
                        Process.Start("regedit");
                    };
                }
                return _myregedit;
            }
        }

        private DelegateCommand _mymstsc = new DelegateCommand();
        /// <summary> 远程连接 </summary>
        public DelegateCommand Mymstsc
        {
            get
            {
                if (_mymstsc.Excute == null)
                {
                    _mymstsc.Excute = () =>
                    {
                        Process.Start("mstsc");

                    };
                }
                return _mymstsc;
            }
        }

        private DelegateCommand _mynetspeed = new DelegateCommand();
        /// <summary> 网络测速 </summary>
        public DelegateCommand Mynetspeed
        {
            get
            {
                if (_mynetspeed.Excute == null)
                {
                    _mynetspeed.Excute = () =>
                    {
                        Process.Start("ping", "www.baidu.com -t");
                    };
                }
                return _mynetspeed;
            }
        }

        private DelegateCommand _mylock = new DelegateCommand();
        /// <summary> 锁定电脑 </summary>
        public DelegateCommand Mylock
        {
            get
            {
                if (_mylock.Excute == null)
                {
                    _mylock.Excute = () =>
                    {
                        WinAPIServer.Instance.Lock();
                    };
                }
                return _mylock;
            }
        }

        private DelegateCommand _mynotepad = new DelegateCommand();
        /// <summary> 记事本 </summary>
        public DelegateCommand Mynotepad
        {
            get
            {
                if (_mynotepad.Excute == null)
                {
                    _mynotepad.Excute = () =>
                    {
                        Process.Start("notepad");
                    };
                }
                return _mynotepad;
            }
        }

        private DelegateCommand _myexplorer = new DelegateCommand();
        /// <summary> 启动浏览器 </summary>
        public DelegateCommand Myexplorer
        {
            get
            {
                if (_myexplorer.Excute == null)
                {
                    _myexplorer.Excute = () =>
                    {
                        //从注册表中读取默认浏览器可执行文件路径  
                        RegistryKey key = Registry.ClassesRoot.OpenSubKey(@"http\shell\open\command\");

                        string s = key.GetValue("").ToString();

                        System.Diagnostics.Process.Start(s.Substring(0, s.Length - 8), "https://www.hao123.com/");
                    };
                }
                return _myexplorer;
            }
        }

        private DelegateCommand _snippingtool = new DelegateCommand();
        /// <summary> 截图工具 </summary>
        public DelegateCommand Snippingtool
        {
            get
            {
                if (_snippingtool.Excute == null)
                {
                    _snippingtool.Excute = () =>
                    {
                        string cutPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SysTemConfiger.ConfigerExtend, SysTemConfiger.ConfigerCut);

                        Process.Start(cutPath);
                    };
                }
                return _snippingtool;
            }
        }

        #endregion

        #region - 更新方法 -

        /// <summary> 剪贴板内容改变 </summary>
        internal void OnClipboardChanged()
        {
            try
            {
                // HTodo  ：复制的文件路径 
                string text = System.Windows.Clipboard.GetText();

                if (!string.IsNullOrEmpty(text))
                {
                    if (this.ClipBoradSource.Count > 0)
                    {
                        ClipBoradBindModel last = this.ClipBoradSource.First();

                        if (last.Detial != text)
                        {
                            ClipBoradBindModel f = new ClipBoradBindModel(text, ClipBoardType.Text);
                            this.ClipBoradSource.Insert(0, f);
                        }
                    }
                    else
                    {
                        ClipBoradBindModel f = new ClipBoradBindModel(text, ClipBoardType.Text);
                        this.ClipBoradSource.Insert(0, f);
                    }
                }


                // HTodo  ：复制的文件 
                System.Collections.Specialized.StringCollection list = System.Windows.Clipboard.GetFileDropList();

                foreach (var item in list)
                {
                    if (Directory.Exists(item) || File.Exists(item))
                    {
                        if (this.ClipBoradSource.Count > 0)
                        {
                            ClipBoradBindModel last = this.ClipBoradSource.First();

                            if (last.Detial != item)
                            {
                                ClipBoradBindModel f = new ClipBoradBindModel(item, ClipBoardType.FileSystem);
                                this.ClipBoradSource.Insert(0, f);
                            }
                        }
                        else
                        {
                            ClipBoradBindModel f = new ClipBoradBindModel(item, ClipBoardType.FileSystem);
                            this.ClipBoradSource.Insert(0, f);
                        }


                    }
                }

                //// HTodo  ：复制的图片 
                //BitmapSource bit = System.Windows.Clipboard.GetImage();

                //if (bit != null)
                //{
                //    if (this._viewModel.ClipBoradSource.Count > 0)
                //    {
                //        ClipBoradBindModel last = this._viewModel.ClipBoradSource.First();

                //        if (last.Detial != bit.ToString())
                //        {
                //            ClipBoradBindModel f = new ClipBoradBindModel(bit.ToString(), ClipBoardType.Image);
                //            this._viewModel.ClipBoradSource.Insert(0, f);
                //        }
                //    }
                //    else
                //    {
                //        ClipBoradBindModel f = new ClipBoradBindModel(bit.ToString(), ClipBoardType.Image);
                //        this._viewModel.ClipBoradSource.Insert(0, f);
                //    }


                //}
            }
            catch (Exception ex)
            {
                LogBindModel log = new LogBindModel();
                log.Message = ex.Message;
                this.Log = log;
            }

        }

        /// <summary> 加载扩展工具 </summary>
        public void LoadExtend()
        {

            string extendPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SysTemConfiger.ConfigerExtend);

            if (!Directory.Exists(extendPath)) return;

            DirectoryInfo folder = Directory.CreateDirectory(extendPath);

            var extends = folder.GetDirectories();

            foreach (var item in extends)
            {
                var file = item.Find<FileInfo>(l => l.Extension.EndsWith("exe"));

                if (file == null) continue;

                FileBindModel fileBind = new FileBindModel(file);
                fileBind.FileName = item.Name;

                this.ExtendSource.Add(fileBind);
            }
        }

        /// <summary> 加载记事本 </summary>
        public void LoadClipBoard()
        {
            if (!File.Exists(clipboardPath)) return;

            string s = File.ReadAllText(clipboardPath);

            ObservableCollection<ClipBoradBindModel> b = s.JsonDeserialize<ObservableCollection<ClipBoradBindModel>>();

            if (b == null || b.Count == 0) return;

            this.ClipBoradSource = b;

        }

        /// <summary> 加载所有程序 </summary>
        public void LoadPrograms()
        {
            //HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall 此键的子健为本机所有注册过的软件的卸载程序,通过此思路进行遍历安装的软件
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
            string[] key1 = key.GetSubKeyNames();//返回此键所有的子键名称
            List<string> key2 = key1.ToList<string>();//因为有的项木有"DisplayName"或"DisplayName"的键值的时候要把键值所在数组中的的元素进行删除
            RegistryKey subkey = null;

            for (int i = 0; i < key2.Count; i++)
            {

                //通过list泛型数组进行遍历,某款软件项下的子键
                subkey = key.OpenSubKey(key2[i]);

                if (subkey.GetValue("DisplayName") == null) continue;
                if (subkey.GetValue("DisplayIcon") == null) continue;


                string path = subkey.GetValue("DisplayIcon").ToString();
                //截取子键值的最后一位进行判断
                string SubPath = path.Substring(path.Length - 1, 1);

                //如果为o 就是ico 或 找不到exe的 表示为图标文件或只有个标识而没有地址的
                if (SubPath == "o" || path.IndexOf("exe") == -1)
                {
                    //首先删除数组中此索引的元素
                    key2.RemoveAt(i);
                    //把循环条件i的值进行从新复制,否则下面给listview的项的tag属性进行赋值的时候会报错
                    i -= 1;
                    continue;
                }

                //如果为e 就代表着是exe可执行文件,
                if (SubPath == "e")
                {
                    //则表示可以直接把地址赋给tag属性
                    FileBindModel p = new FileBindModel(path);
                    p.FileName = subkey.GetValue("DisplayName").ToString();
                    this.ProgramSource.Add(p);
                    continue;
                }
                //因为根据观察 取的是DisplayIcon的值 表示为图片所在路径 如果为0或1,则是为可执行文件的图标  
                if (SubPath == "0" || SubPath == "1")
                {
                    //进行字符串截取,
                    path = path.Substring(0, path.LastIndexOf("e") + 1);
                    //则表示可以直接把地址赋给tag属性
                    FileBindModel p = new FileBindModel(path);
                    p.FileName = subkey.GetValue("DisplayName").ToString();
                    this.ProgramSource.Add(p);
                    continue;
                }
            }
        }

        /// <summary> 加载记事本 </summary>
        public void LoadNotePad()
        {
            if (!File.Exists(notepadPath)) return;

            string s = File.ReadAllText(notepadPath);

            ObservableCollection<NotePadBindModel> b = s.JsonDeserialize<ObservableCollection<NotePadBindModel>>();

            if (b == null || b.Count == 0) return;

            this.NotePadSource = b;

        }

        /// <summary> 注册日志 </summary>
        public void RegisterLog()
        {

        }

        /// <summary> 保存配置文件 </summary>
        public void SaveToFile()
        {
            string s = this.BkSource.JsonSerialize<ObservableCollection<FileBindModel>>();

            File.WriteAllText(configerPath, s);



            string n = this.NotePadSource.JsonSerialize<ObservableCollection<NotePadBindModel>>();

            File.WriteAllText(notepadPath, n);
            this.ClipBoradSource.RemoveAtAfter(ControlConfiger.Instance.NotePadSaveCount);

            string c = this.ClipBoradSource.JsonSerialize<ObservableCollection<ClipBoradBindModel>>();

            File.WriteAllText(clipboardPath, c);
        }

        /// <summary> 加载配置文件 </summary>
        public void LoadToList()
        {
            if (!File.Exists(configerPath)) return;

            string s = File.ReadAllText(configerPath);

            ObservableCollection<FileBindModel> b = s.JsonDeserialize<ObservableCollection<FileBindModel>>();

            if (b == null || b.Count == 0) return;

            this.BkSource = b;

            this.RefreshUI();
        }

        /// <summary> 刷新界面数据 </summary>
        public void RefreshUI()
        {
            var collection = this.BkSource.OrderByDescending(l => l.LastTime);

            ObservableCollection<FileBindModel> temp = new ObservableCollection<FileBindModel>();


            foreach (var item in collection)
            {
                temp.Add(item);
            };

            this.BkSource = temp;

            this.RefreshLastSource();


        }

        /// <summary> 最近使用的文件 </summary>
        public void RefreshLastSource()
        {
            string recent = Environment.GetFolderPath(Environment.SpecialFolder.Recent);

            var files = Directory.GetFiles(recent);

            this.LastSource.Clear();

            foreach (var item in files)
            {
                FileBindModel f = new FileBindModel(item);

                if (string.IsNullOrEmpty(f.FilePath)) continue;
                this.LastSource.Add(f);
            }
        }

        /// <summary> 收藏文件夹 </summary>
        public void RefreshFavoritesSource()
        {
            string recent = Environment.GetFolderPath(Environment.SpecialFolder.Favorites);

            var files = recent.GetAllFile();

            this.FavoritesSource.Clear();

            foreach (var item in files)
            {
                FileBindModel f = new FileBindModel(item);

                if (string.IsNullOrEmpty(f.FilePath)) continue;

                this.FavoritesSource.Add(f);

            }
        }

        /// <summary> 系统文件夹 </summary>
        public void RefreshSysSource()
        {
            var names = Enum.GetNames(typeof(Environment.SpecialFolder));

            this.SysSource.Clear();

            foreach (var item in names)
            {
                Environment.SpecialFolder e = item.GetEnumByNameOrValue<Environment.SpecialFolder>();

                //string recent = Environment.GetFolderPath(e);

                string recent = WinSysHelper.Instance.GetSystemPath(e);

                FileBindModel f = new FileBindModel(recent);
                f.FileName = item;
                if (string.IsNullOrEmpty(f.FilePath)) continue;
                this.SysSource.Add(f);
            }
        }


        #endregion
    }

    partial class StartWindowViewModel : INotifyPropertyChanged
    {
        #region - MVVM -

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }

}
