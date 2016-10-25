using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.MMF
{
    /// <summary> 二维存储结构 </summary>
    public class DxyMmfEntity<T> : MmfEntity<T> where T : struct
    {
        #region - 构造函数 -
        public DxyMmfEntity(string fileFullPath, int Tcount)
            : base(fileFullPath, Tcount)
        {
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

        public void Set(int x, int y, T value)
        {
            this.SetIndex(TranFucntion(x, y), value);
        }

        /// <summary> 获取指定二维值 </summary>
        public T Get(int x, int y)
        {
            return this.GetIndex(TranFucntion(x, y));
        }

        Func<int, int, int> tranFucntion;
        /// <summary> 设置指定二维值 </summary>
        public Func<int, int, int> TranFucntion
        {
            get { return (x, y) => y * this.X + x; }
        }


    }
}
