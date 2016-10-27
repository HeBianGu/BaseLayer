using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.MMF
{
    /// <summary> 泛型 T 内存映射文件 </summary>
    public partial class MmfEntity<T> where T : struct
    {
        #region - 成员变量 -

        private long _size;
        /// <summary> 文件大小 </summary>
        public long Size
        {
            get { return _size; }
        }

        /// <summary> 实体大小 </summary>
        public int MLeight
        {
            get { return Marshal.SizeOf(typeof(T)); }
        }

        private int _count;
        /// <summary> T 类型的数量 </summary>
        public int Count
        {
            get { return _count; }
            set
            {
                //  设置容器大小
                this._size = MLeight * value;

                _count = value;
            }
        }

        private string _file;
        /// <summary> 文件全路径 </summary>
        public string FileInf
        {
            get { return _file; }
            set { _file = value; }
        }

        private string _name;
        /// <summary> 镜像名称 </summary>
        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(_name))
                {
                    _name = Path.GetFileNameWithoutExtension(_file);
                }
                return _name;
            }
        }

        private MemoryMappedFile _mmf;
        /// <summary> 内存镜像 </summary>
        protected MemoryMappedFile Mmf
        {
            get
            {
                BuildFile();

                return _mmf;
            }
        }





        /// <summary> 创建文件 </summary>
        void BuildFile()
        {
            if (File.Exists(this.FileInf))
            {
                this.Dispose();

                File.Delete(this.FileInf);
            }



            //  如果存在删除
            _mmf = MemoryMappedFile.CreateFromFile(this.FileInf, FileMode.OpenOrCreate, _name, _size, MemoryMappedFileAccess.ReadWriteExecute);


        }

        private MemoryMappedViewAccessor _mapView;
        /// <summary> 随机访问视图  </summary>
        protected MemoryMappedViewAccessor MapView
        {
            get
            {
                if (_mmf == null)
                {
                    this.BuildFile();
                }

                if (_mapView == null)
                {
                    _mapView = _mmf.CreateViewAccessor();
                }

                return _mapView;
            }
        }

        private MemoryMappedViewStream _mapStream;
        /// <summary> 按循序访问的流 </summary>
        protected MemoryMappedViewStream MapStream
        {
            get
            {

                if (_mmf == null)
                {
                    this.BuildFile();
                }

                if (_mapStream == null)
                {
                    _mapStream = _mmf.CreateViewStream();
                }


                return _mapStream;
            }
            private set { _mapStream = value; }
        }

        #endregion

        /// <summary> 将 T 类型的结构从访问器读取到提供的引用中 </summary>
        public T GetPostion(long position)
        {
            T structure;

            MapView.Read<T>(position, out structure);

            return structure;
        }

        /// <summary> 读取指定索引处结构 </summary>
        public T GetIndex(int index)
        {
            long postion = index * this.MLeight;

            return this.GetPostion(postion);
        }

        /// <summary> 将 T 类型的结构从访问器读取到 T 类型的数组中 </summary>
        public T[] GetPostion(int count, long position = 0)
        {
            T[] arr = new T[count];

            MapView.ReadArray<T>(position, arr, 0, count);

            return arr;
        }

        /// <summary> 将 T 类型的结构从访问器读取到 T 类型的数组中 </summary>
        public T[] GetAll(long position = 0)
        {
            T[] arr = new T[this._count];

            MapView.ReadArray<T>(position, arr, 0, this._count);

            return arr;
        }

        /// <summary> 将一个结构写入访问器 </summary>
        public void SetPosition(long position, T structure)
        {
            MapView.Write<T>(position, ref structure);
        }

        /// <summary> 写入指定索引处结构 </summary>
        public void SetIndex(int index, T structure)
        {
            long postion = index * this.MLeight;

            this.SetPosition(postion, structure);

        }

        /// <summary> 将结构从 T 类型的数组写入访问器 </summary>
        public void SetPosition(long position, T[] arr)
        {
            MapView.WriteArray<T>(position, arr, 0, arr.Length);
        }

        /// <summary> 将结构从 T 类型的数组写入访问器 </summary>
        public void SetAll(T[] arr)
        {
            MapView.WriteArray<T>(0, arr, 0, arr.Length);
        }

        /// <summary> 将结构从 T 类型的数组写入访问器 </summary>
        public void SetAll(T t)
        {
            T[] arr = new T[this.Count];

            this.Count.DoCountWhile(l => arr[l] = t);

            MapView.WriteArray<T>(0, arr, 0, this.Count);
        }



        /// <summary> 重置大小 </summary>
        public void ReSetSize(int count)
        {
            this._count = count;

            this._size = count * this.MLeight;

            this.BuildFile();
        }


    }

    partial class MmfEntity<T> : IDisposable
    {
        #region - 构造函数 -

        public MmfEntity(string fileFullPath, int Tcount)
        {
            this._file = fileFullPath;

            this._count = Tcount;

            this._size = Tcount * this.MLeight;

            this._name = Path.GetFileNameWithoutExtension(fileFullPath);

            this.BuildFile();
        }

        #endregion

        #region - 资源释放 -

        private bool _isDisposed = false;

        ~MmfEntity()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    if (this._mapView != null) this._mapView.Dispose();

                    if (this._mmf != null) this._mmf.Dispose();
                }
                this._isDisposed = true;
            }
        }

        #endregion
    }

}


