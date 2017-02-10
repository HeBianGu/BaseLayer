using HebianGu.ComLibModule.API;
using System;
using System.Collections.Generic;
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

        private List<FileBindModel> _bkSource = new List<FileBindModel>();

        /// <summary> 所有关键字 </summary>
        public List<FileBindModel> BkSource
        {
            get { return _bkSource; }
            set
            {
                _bkSource = value;
                RaisePropertyChanged("BkSource");
            }
        }

        private List<FileBindModel> _lastSource = new List<FileBindModel>();

        /// <summary> 所有关键字 </summary>
        public List<FileBindModel> LastSource
        {
            get { return _lastSource; }
            set
            {
                _lastSource = value;
                RaisePropertyChanged("LastSource");
            }
        }

        private List<FileBindModel> _favoritesSource = new List<FileBindModel>();

        /// <summary> 所有关键字 </summary>
        public List<FileBindModel> FavoritesSource
        {
            get { return _favoritesSource; }
            set
            {
                _favoritesSource = value;
                RaisePropertyChanged("FavoritesSource");
            }
        }
        

        private List<FileBindModel> _sysSource = new List<FileBindModel>();

        /// <summary> 所有关键字 </summary>
        public List<FileBindModel> SysSource
        {
            get { return _sysSource; }
            set
            {
                _sysSource = value;
                RaisePropertyChanged("SysSource");
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
    }


    /// <summary> 说明 </summary>
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
}
