using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace 二进制文件存储
{
    // 摘要: 
    //     没有经过测试，用到的自行测试。
    /// <summary>  二进制文件写入与读出  "T" 文件单元泛型  Add by lihaijun  2015.10.26 </summary>
    class MemoryInput<T> where T : struct
    {

        #region - Start 内部成员 -

        /// <summary> 二进制文件的写入与读取 </summary>
        /// <param name="pFilePath"> 二进制文件路径 </param>
        /// <param name="pFileName"> 二进制文件名称 </param>
        public MemoryInput(string pFilePath, string pFileName)
        {
            _filePath = pFilePath;
            _fileName = pFileName;
            InitCompenont();

        }
        //  初始化对象
        void InitCompenont()
        {
            //  获取角点类型长度标准
            int cubeCoordSize = Marshal.SizeOf(typeof(T));

            //  由文件生成二进制对象实例
            _mappedFile = MemoryMappedFile.CreateFromFile
                (
                _filePath,
                FileMode.OpenOrCreate,
                _fileName,
                Nz * Ny * Nx * cubeCoordSize,
                MemoryMappedFileAccess.ReadWriteExecute
                );

            _accessorModel = _mappedFile.CreateViewAccessor
                       (0, 0, MemoryMappedFileAccess.ReadWrite);
        }
  
        string _filePath = AppDomain.CurrentDomain.BaseDirectory;
        /// <summary> 二进制文件路径 </summary>
        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value; }
        }

        string _fileName = string.Empty;

        /// <summary> 二进制文件名 </summary>
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        MemoryMappedFile _mappedFile = null;
        MemoryMappedViewAccessor _accessorModel = null;



        /// <summary> X </summary>
        public int Nx { get; set; }
        /// <summary> Y </summary>
        public int Ny { get; set; }
        /// <summary> Z </summary>
        public int Nz { get; set; }

        #endregion - 内部成员 End -


        /// <summary> 角点存储 ：打开整个文件，然后存储单个数据，因为用户内存有限，所以不适用于100万以上网格数据 </summary>
        public void SetModelCoordEntire(int ix, int iy, int iz, T cubeCoordValue)
        {
            //  角点单位
            int cubeCoordSize = Marshal.SizeOf(typeof(T));

            //  角点偏移
            long offset = (iz * Ny * Nx + iy * Nx + ix) * cubeCoordSize;

            //  写入二进制
            _accessorModel.Write(offset, ref cubeCoordValue);

        }


        /// <summary> 角点读取 ：打开整个文件，然后读取单个数据，因为用户内存有限，所以不适用于100万以上网格数据 </summary>
        public bool GetModelCoordEntire(int ix, int iy, int iz, out T cubeCoordValue)
        {
            int nx = Nx;
            int ny = Ny;
            int nz = Nz;

            if (ix < nx && iy < ny && iz < nz)
            {
                //  角点单位
                int cubeCoordSize = Marshal.SizeOf(typeof(CubeCoord));

                //  角点偏移
                long offset = (iz * ny * nx + iy * nx + ix) * cubeCoordSize;

                //  读取二进制
                _accessorModel.Read(offset, out cubeCoordValue);

                return true;
            }

            cubeCoordValue = new T();

            return false;
        }


    }
}
