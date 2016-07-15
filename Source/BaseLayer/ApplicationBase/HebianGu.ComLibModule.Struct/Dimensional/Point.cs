using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.Struct.Dimensional
{
    public struct Point3D<T>
    {
        private T _x;
        private T _y;
        private T _z;

        /// <summary>
        /// 横坐标。
        /// </summary>
        public T X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }

        /// <summary>
        /// 纵坐标。
        /// </summary>
        public T Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }

        /// <summary>
        /// Z坐标。
        /// </summary>
        public T Z
        {
            get
            {
                return _z;
            }
            set
            {
                _z = value;
            }
        }

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="x">横坐标。</param>
        /// <param name="y">纵坐标。</param>
        public Point3D(T x, T y, T z)
        {
            _x = x;
            _y = y;
            _z = z;
        }

    }
}
