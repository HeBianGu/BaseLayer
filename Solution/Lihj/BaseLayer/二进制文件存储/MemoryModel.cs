using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 二进制文件存储
{
    public struct CubeCoord
    {
        public Point3D APt;
        public Point3D BPt;
        public Point3D CPt;
        public Point3D DPt;

        public Point3D EPt;
        public Point3D FPt;
        public Point3D GPt;
        public Point3D HPt;
    }

    public struct Point3D
    {
        private double _x;
        private double _y;
        private double _z;

        /// <summary>
        /// 横坐标。
        /// </summary>
        public double X
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
        public double Y
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
        public double Z
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
        public Point3D(double x, double y, double z)
        {
            _x = x;
            _y = y;
            _z = z;
        }
        public override bool Equals(Object obj)
        {
            if (obj is Point3D)
            {
                Point3D obj1 = (Point3D)obj;
                return (this == obj1);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (int)(_x * 1000000 + _y * 1000 + _z);
        }

        public static bool operator ==(Point3D left, Point3D right)
        {
            if ((left.X == right.X ||
                Math.Abs(left.X - right.X) < 1E-06) &&
                (left.Y == right.Y ||
                Math.Abs(left.Y - right.Y) < 1E-06) &&
                (left.Z == right.Z ||
                Math.Abs(left.Z - right.Z) < 1E-06))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool operator !=(Point3D left, Point3D right)
        {
            if ((left.X == right.X ||
                Math.Abs(left.X - right.X) < 1E-06) &&
                (left.Y == right.Y ||
                Math.Abs(left.Y - right.Y) < 1E-06) &&
                (left.Z == right.Z ||
                Math.Abs(left.Z - right.Z) < 1E-06))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

}
