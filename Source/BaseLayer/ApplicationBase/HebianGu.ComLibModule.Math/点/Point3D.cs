using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.MathHelper
{
    public class Point3D
    {
        #region - start 成员变量-

        public double[] point3D = new double[3];

        private const double E = 0.0000001f;

        public Point3D(double x, double y, double z = 0)
        {
            point3D = new double[3] { x, y, z };
        }

        public Point3D()
        {

        }

        public Point3D(Point3D vct)
        {
            point3D = new double[3];

            point3D[0] = vct.X;

            point3D[1] = vct.Y;

            point3D[2] = vct.Z;
        }

        /// <summary> X方向 </summary>
        public double X
        {
            get { return point3D[0]; }
            set { point3D[0] = value; }
        }

        /// <summary> Y方向 </summary>
        public double Y
        {
            get { return point3D[1]; }
            set { point3D[1] = value; }
        }

        /// <summary> Z方向 </summary>
        public double Z
        {
            get { return point3D[2]; }
            set { point3D[2] = value; }
        }

        #endregion - 成员变量 end-

        #region - start 成员方法-

        /// <summary> 平移 </summary>
        public void Transform()
        {

        }

        public void Rotate()
        {

        }

        #endregion - 成员方法 end-
    }
}
