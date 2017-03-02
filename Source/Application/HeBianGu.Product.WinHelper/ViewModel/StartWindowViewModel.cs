using HebianGu.ComLibModule.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace HebianGu.Product.WinHelper.ViewModel
{
    class StartWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private ObservableCollection<FileBindModel> _bkSource = new ObservableCollection<FileBindModel>();

        /// <summary> 所有关键字 </summary>
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

        /// <summary> 所有关键字 </summary>
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

        /// <summary> 所有关键字 </summary>
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

        /// <summary> 所有关键字 </summary>
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

        /// <summary> 所有关键字 </summary>
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

        /// <summary> 所有关键字 </summary>
        public ObservableCollection<NotePadBindModel> NotePadSource
        {
            get { return _notePadSource; }
            set
            {
                _notePadSource = value;
                RaisePropertyChanged("NotePadSource");
            }
        }


        public StartWindowViewModel()
        {

        }

        public void Add(FileBindModel m)
        {
            _bkSource.Add(m);
        }

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
            get { return _checked; }
            set
            {
                _isStart = value;
                RaisePropertyChanged("IsStart");
            }
        }


    }


    /// <summary> 文件绑定实体 </summary>
    [Serializable]
    class FileBindModel
    {
        public FileBindModel(string path)
        {
            if (Path.HasExtension(path))
            {
                if (SysTemConfiger.ExceptShowFile.Exists(l => l == Path.GetExtension(path)))
                {
                    return;
                }

                this.FileName = Path.GetFileNameWithoutExtension(path);
                this.FilePath = path;
                this.IsFile = true;
            }
            else
            {
                this.FileName = Path.GetFileName(path);
                this.FilePath = path;
                this.IsFile = false;
            }

            this.LastTime = DateTime.Now;
        }

        public FileBindModel(FileSystemInfo sysFile)
        {
            if (SysTemConfiger.ExceptShowFile.Exists(l => l == sysFile.Extension))
            {
                return;
            }

            if (sysFile is FileInfo)
            {
                this.FileName = sysFile.Name;
                this.FilePath = sysFile.FullName;
                this.IsFile = true;
            }
            else
            {
                this.FileName = sysFile.Name;
                this.FilePath = sysFile.FullName;
                this.IsFile = false;
            }

            this.LastTime = DateTime.Now;
        }

        private string _fileName;
        /// <summary> 说明 </summary>
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        private string _filePath;
        /// <summary> 说明 </summary>
        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value; }
        }

        private bool _isFile;
        /// <summary> 说明 </summary>
        public bool IsFile
        {
            get { return _isFile; }
            set { _isFile = value; }
        }

        /// <summary> 图片路径 </summary>
        public Icon ImagePath
        {
            get { return IconHelper.Instance.GetSystemInfoIcon(FilePath); }
        }

        private DateTime _lastTime;
        /// <summary> 说明 </summary>
        public DateTime LastTime
        {
            get { return _lastTime; }
            set { _lastTime = value; }
        }



    }


    /// <summary> 记事本绑定实体 </summary>
    [Serializable]
    public class NotePadBindModel
    {

        private string _title = string.Empty;
        /// <summary> 说明 </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private string _detial = string.Empty;
        /// <summary> 说明 </summary>
        public string DetialMin
        {
            get
            {
                if (_detial.Length < 20)
                {
                    return _detial;
                }

                return _detial.Substring(0, 20) + " ...";
            }
            set { _detial = value; }
        }

        /// <summary> 说明 </summary>
        public string Detial
        {
            get
            {
                return _detial;
            }
            set { _detial = value; }
        }

        private int _level = 1;
        /// <summary> 说明 </summary>
        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }

        private DateTime _cTime = DateTime.Now;
        /// <summary> 说明 </summary>
        public DateTime CreateTime
        {
            get { return _cTime; }
            set { _cTime = value; }
        }

        public string CTime
        {
            get { return _cTime.ToString("yyyy-MM-dd"); }
        }

        private DateTime _notifyTime = DateTime.Now;
        /// <summary> 说明 </summary>
        public DateTime NotifyTime
        {
            get { return _notifyTime; }
            set { _notifyTime = value; }
        }

        /// <summary> 图片路径 </summary>
        public string ImagePath
        {
            get
            {
                switch (this.Level)
                {
                    case 1:
                        return "/HebianGu.Product.WinHelper;component/image/Viewer/1.ICO";
                    case 2:
                        return "/HebianGu.Product.WinHelper;component/image/Viewer/2.ICO";
                    case 3:
                        return "/HebianGu.Product.WinHelper;component/image/Viewer/3.ICO";
                    case 4:
                        return "/HebianGu.Product.WinHelper;component/image/Viewer/4.ICO";
                    case 5:
                        return "/HebianGu.Product.WinHelper;component/image/Viewer/5.ICO";
                    default:
                        return "/HebianGu.Product.WinHelper;component/image/Viewer/zhcn070.ico";
                }
            }
        }


        private List<int> _levelSource = new List<int>() { 1, 2, 3, 4, 5 };
        /// <summary> 说明 </summary>
        public List<int> LevelSource
        {
            get { return _levelSource; }
        }


    }


    /// <summary> 文件绑定实体 </summary>
    [Serializable]
    class ClipBoradBindModel
    {
        ClipBoardType _type;

        string _detial;
        public ClipBoradBindModel(string detial, ClipBoardType type)
        {
            Type = type;
            _detial = detial;
            this._createTime = DateTime.Now;
        }

        /// <summary> 说明 </summary>
        public string Title
        {
            get
            {
                switch (Type)
                {
                    case ClipBoardType.FileSystem:
                        return Path.GetFileName(_detial);
                    case ClipBoardType.Image:
                        return _detial;
                    case ClipBoardType.Text:
                        return _detial;
                    default:
                        return _detial;
                }
            }
        }

        public string Detial
        {
            get
            {
                return _detial;
            }
        }


        /// <summary> 图片路径 </summary>
        public Icon ImagePath
        {
            get
            {
                switch (Type)
                {
                    case ClipBoardType.FileSystem:
                        return IconHelper.Instance.GetSystemInfoIcon(_detial);
                    case ClipBoardType.Image:
                        return Icon.ExtractAssociatedIcon("./image/Viewer/M.ICO");
                    case ClipBoardType.Text:
                        return Icon.ExtractAssociatedIcon("./image/Viewer/T.ICO");
                    default:
                        return Icon.ExtractAssociatedIcon("./image/图标0A/D056.ICO");
                }

            }
        }

        private DateTime _createTime;
        /// <summary> 复制的时间 </summary>
        public string CreateTime
        {
            get { return _createTime.ToString("yyyy-MM-dd hh:mm:ss"); }
        }

        internal ClipBoardType Type
        {
            get
            {
                return _type;
            }

            set
            {
                _type = value;
            }
        }
    }


    /// <summary> 剪贴板内容样式 </summary>
    enum ClipBoardType : int
    {
        FileSystem = 0, Text, Image
    }
}
