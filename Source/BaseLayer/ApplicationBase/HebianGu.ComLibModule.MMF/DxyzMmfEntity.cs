using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.MMF
{

    /// <summary> 三维存储结构 </summary>
    public class DxyzMmfEntity<T> : MmfEntity<T> where T : struct
    {
        #region - 构造函数 -
        public DxyzMmfEntity(string fileFullPath, int x, int y, int z)
            : base(fileFullPath, x * y * z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        /// <summary> 初始化方法 </summary>
        public void Init(int x, int y, int z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;

            base.ReSetSize(x * y * z);
        }
        #endregion

        private int _x;
        /// <summary> X维数 </summary>
        public int X
        {
            get { return _x; }
            set { _x = value; }
        }

        private int _y;
        /// <summary> Y维数 </summary>
        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }

        private int _z;
        /// <summary> Z方向维数 </summary>
        public int Z
        {
            get { return _z; }
            set { _z = value; }
        }

        public void Set(int x, int y, int z, T value)
        {
            this.SetIndex(TranFucntion(x, y, z), value);
        }

        /// <summary> 获取指定二维值 </summary>
        public T Get(int x, int y, int z)
        {
            return this.GetIndex(TranFucntion(x, y, z));
        }

        Func<int, int, int> tranFucntion;
        /// <summary> 设置指定二维值 </summary>
        public Func<int, int, int, int> TranFucntion
        {
            get { return (x, y, z) => z * this.X * this.Y + y * this.X + x; }
        }
    }
}
