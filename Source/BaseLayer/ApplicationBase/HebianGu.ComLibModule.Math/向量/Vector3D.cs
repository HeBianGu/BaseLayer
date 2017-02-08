using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.MathHelper
{
    /// <summary>  三维向量类   </summary>  
    [Serializable]
    public class Vector3D
    {

        #region - start 错误信息-

        private const string NORMALIZE_0 = "Can not normalize a vector when" + "it's magnitude is zero";

        private const string INTERPOLATION_RANGE = "Control parameter must be a" + "value between 0 & 1";

        private const string ARGUMENT_VALUE = "Can not normalize a vector when" + "it's magnitude is zero";

        #endregion - 错误信息 end-

        #region - start 成员变量-

        public double[] vector = new double[3];
        private const double E = 0.0000001f;
        public Vector3D(double x, double y, double z = default(double))
        {
            vector = new double[3] { x, y, z };
        }
        public Vector3D()
        {

        }
        public Vector3D(Vector3D vct)
        {
            vector = new double[3];

            vector[0] = vct.X;

            vector[1] = vct.Y;

            vector[2] = vct.Z;
        }
        /// <summary> X方向 </summary>
        public double X
        {
            get { return vector[0]; }
            set { vector[0] = value; }
        }
        /// <summary> Y方向 </summary>
        public double Y
        {
            get { return vector[1]; }
            set { vector[1] = value; }
        }
        /// <summary> Z方向 </summary>
        public double Z
        {
            get { return vector[2]; }
            set { vector[2] = value; }
        }
        #endregion - 成员 end -

        #region  - start 成员方法 -
        /// <summary> 求向量长度 </summary> 
        public double Magnitude()
        {

            return (double)Math.Sqrt(

                  (this.X * this.X)
                + (this.Y * this.Y)
                + (this.Z * this.Z)

                );

        }

        public bool IsUnitVector()
        {

            return IsUnitVector(this);

        }

        public void Normalize()
        {
            Vector3D v = Normalize(this);

            this.X = v.X;

            this.Y = v.Y;

            this.Z = v.Z;

        }

        public Vector3D Lerp(Vector3D other, double control)
        {

            return Lerp(this, other, control);

        }

        public double Distance(Vector3D other)
        {

            return Distance(this, other);

        }

        public double Dot(Vector3D other)
        {

            return Dot(this, other);

        }

        public double Angle(Vector3D other)
        {

            return Angle(this, other);

        }

        public Vector3D Max(Vector3D other)
        {

            return Max(this, other);

        }

        public Vector3D Min(Vector3D other)
        {

            return Min(this, other);

        }

        public void Pitch(double degree)
        {

            Vector3D v = Pitch(this, degree);

            this.X = v.X;

            this.Y = v.Y;

            this.Z = v.Z;


        }

        public void Yaw(double degree)
        {

            Vector3D v = Yaw(this, degree);

            this.X = v.X;

            this.Y = v.Y;

            this.Z = v.Z;

        }

        public void Roll(double degree)
        {

            Vector3D v = Roll(this, degree);

            this.X = v.X;

            this.Y = v.Y;

            this.Z = v.Z;

        }

        public bool IsBackFace(Vector3D lineOfSight)
        {

            return IsBackFace(this, lineOfSight);

        }

        public bool IsPerpendicular(Vector3D other)
        {

            return IsPerpendicular(this, other);

        }

        public double MixedProduct(Vector3D other_v1, Vector3D other_v2)
        {

            return Dot(Cross(this, other_v1), other_v2);

        }

        public double SumComponents()
        {

            return (this.X + this.Y + this.Z);

        }

        public void PowComponents(double power)
        {

            Vector3D v = PowComponents(this, power);

            this.X = v.X;

            this.Y = v.Y;

            this.Z = v.Z;

        }

        public int CompareTo(Vector3D other)
        {

            if (this < other)
            {

                return -1;

            }

            else if (this > other)
            {

                return 1;

            }



            return 0;

        }

        #endregion - 成员方法 end -

        #region - start 运算符-

        public static Vector3D operator +(Vector3D lhs, Vector3D rhs)//向量加法   
        {
            Vector3D result = new Vector3D(lhs);

            result.X += rhs.X;

            result.Y += rhs.Y;

            result.Z += rhs.Z;

            return result;
        }
        public static Vector3D operator -(Vector3D lhs, Vector3D rhs)//向量减法   
        {
            Vector3D result = new Vector3D(lhs);

            result.X -= rhs.X;

            result.Y -= rhs.Y;

            result.Z -= rhs.Z;

            return result;
        }
        /// <summary> 向量除以数量 </summary>
        public static Vector3D operator /(Vector3D lhs, double rhs)
        {
            if (rhs != 0)
                return new Vector3D(lhs.X / rhs, lhs.Y / rhs, lhs.Z / rhs);
            else
                return new Vector3D(0, 0, 0);
        }
        /// <summary> 左乘数量 </summary>
        public static Vector3D operator *(double lhs, Vector3D rhs)
        {
            return new Vector3D(lhs * rhs.X, lhs * rhs.Y, lhs * rhs.Z);
        }
        /// <summary> 右乘数量  </summary>
        public static Vector3D operator *(Vector3D lhs, double rhs)//  
        {
            return new Vector3D(lhs.X * rhs, lhs.Y * rhs, lhs.Z * rhs);
        }
        /// <summary> 向量数性积 </summary>
        public static double operator *(Vector3D lhs, Vector3D rhs)
        {
            return lhs.X * rhs.X + lhs.Y * rhs.Y + lhs.Z * rhs.Z;
        }
        public static bool operator ==(Vector3D lhs, Vector3D rhs)
        {
            if (Math.Abs(lhs.X - rhs.X) < E &&
                Math.Abs(lhs.Y - rhs.Y) < E &&
                Math.Abs(lhs.Z - rhs.Z) < E)

                return true;

            else

                return false;
        }
        /// <summary> 不等于 </summary>
        public static bool operator !=(Vector3D lhs, Vector3D rhs)
        {
            return !(lhs == rhs);
        }

        public static bool operator <(Vector3D v1, Vector3D v2)
        {
            return v1.Magnitude() < v2.Magnitude();

        }

        public static bool operator <=(Vector3D v1, Vector3D v2)
        {

            return v1.Magnitude() <= v2.Magnitude();

        }

        public static bool operator >(Vector3D v1, Vector3D v2)
        {

            return v1.Magnitude() > v2.Magnitude();

        }

        public static bool operator >=(Vector3D v1, Vector3D v2)
        {

            return v1.Magnitude() > v2.Magnitude();

        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        #endregion - 运算符 end-

        #region - start 重写方法 -
        public override string ToString()
        {
            return "(" + X + "," + Y + "," + Z + ")";
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion - 重写方法 end -

        #region - start 静态方法-

        /// <summary> 叉积:求与两向量垂直的向量 </summary> 
        public static Vector3D Cross(Vector3D v1, Vector3D v2)
        {
            Vector3D r = new Vector3D(0, 0, 0);

            r.X = (v1.Y * v2.Z) - (v1.Z * v2.Y);

            r.Y = (v1.Z * v2.X) - (v1.X * v2.Z);

            r.Z = (v1.X * v2.Y) - (v1.Y * v2.X);

            return r;
        }

        /// <summary> 点积:求夹角 </summary>
        public static double Dot(Vector3D v1, Vector3D v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }

        /// <summary> 求向量长度 </summary> 
        public static double Magnitude(Vector3D v1)
        {

            return (double)Math.Sqrt((v1.X * v1.X) + (v1.Y * v1.Y) + (v1.Z * v1.Z));

        }

        /// <summary> 单位化向量 </summary> 
        public static Vector3D Normalize(Vector3D v1)
        {
            double magnitude = Magnitude(v1);

            v1 = v1 / magnitude;

            return v1;
        }

        /// <summary> 计算向量夹角 </summary>
        public static double Angle(Vector3D v1, Vector3D v2)
        {

            return

            (

               Math.Acos

               (

                  Normalize(v1).Dot(Normalize(v2))

               )

            );

        }

        /// <summary> 是否标准向量 </summary>
        public static bool IsUnitVector(Vector3D v1)
        {

            return v1.Magnitude() == 1;

        }

        /// <summary> 差值 </summary> 
        public static Vector3D Lerp(Vector3D v1, Vector3D v2, double control)
        {

            if (control > 1 || control < 0)
            {

                // Error message includes information about the actual value of the 

                // argument

                throw new ArgumentOutOfRangeException

                (

                    "control",

                    control,

                    INTERPOLATION_RANGE + "\n" + ARGUMENT_VALUE + control

                );

            }

            else
            {

                return

                (

                   new Vector3D

                   (

                       v1.X * (1 - control) + v2.X * control,

                       v1.Y * (1 - control) + v2.Y * control,

                       v1.Z * (1 - control) + v2.Z * control

                    )

                );

            }

        }

        /// <summary> 距离 </summary> 
        public static double Distance(Vector3D v1, Vector3D v2)
        {

            return
            (

               Math.Sqrt

               (

                   (v1.X - v2.X) * (v1.X - v2.X) +

                   (v1.Y - v2.Y) * (v1.Y - v2.Y) +

                   (v1.Z - v2.Z) * (v1.Z - v2.Z)

               )

            );

        }

        /// <summary> 获取最大值 </summary>
        public static Vector3D Max(Vector3D v1, Vector3D v2)
        {

            if (v1 >= v2) { return v1; }

            return v2;

        }

        /// <summary> 获取最小值 </summary>
        public static Vector3D Min(Vector3D v1, Vector3D v2)
        {

            if (v1 <= v2) { return v1; }

            return v2;

        }

        /// <summary> 音高:这个方法旋转一个度（欧拉围绕Z轴旋转）绕X轴的矢量。 </summary> 
        public static Vector3D Pitch(Vector3D v1, double degree)
        {

            double x = v1.X;

            double y = (v1.Y * Math.Cos(degree)) - (v1.Z * Math.Sin(degree));

            double z = (v1.Y * Math.Sin(degree)) + (v1.Z * Math.Cos(degree));

            return new Vector3D(x, y, z);

        }

        /// <summary> 偏航:这种方法绕Y轴的向量旋转一个度（欧拉绕Y旋转) </summary>
        public static Vector3D Yaw(Vector3D v1, double degree)
        {

            double x = (v1.Z * Math.Sin(degree)) + (v1.X * Math.Cos(degree));

            double y = v1.Y;

            double z = (v1.Z * Math.Cos(degree)) - (v1.X * Math.Sin(degree));

            return new Vector3D(x, y, z);

        }

        /// <summary> 滚：这个方法旋转一个度（欧拉围绕Z轴旋转）绕Z轴的矢量。 </summary>
        public static Vector3D Roll(Vector3D v1, double degree)
        {

            double x = (v1.X * Math.Cos(degree)) - (v1.Y * Math.Sin(degree));

            double y = (v1.X * Math.Sin(degree)) + (v1.Y * Math.Cos(degree));

            double z = v1.Z;

            return new Vector3D(x, y, z);

        }

        /// <summary> 
        /// 这个方法直接影响到该方法被称为实例。
        /// 背面这种方法解释为面对正常的载体，并确定是否正常代表一个背对着飞机行的视线向量。
        /// 一个背对着飞机将是无形的，在渲染场景，这样，可以从许多场景计算除外 
        /// </summary>
        public static bool IsBackFace(Vector3D normal, Vector3D lineOfSight)
        {

            return normal.Dot(lineOfSight) < 0;

        }

        /// <summary> 垂直:此方法检查，如果两个向量垂直（即如果一个向量是正常的） </summary>
        public static bool IsPerpendicular(Vector3D v1, Vector3D v2)
        {

            return v1.Dot(v2) == 0;

        }

        /// <summary> 混合产品:此方法的代码提供了米莎＃322; BRY＃322;家。该方法计算的三个向量的标量三重积 </summary>
        public static double MixedProduct(Vector3D v1, Vector3D v2, Vector3D v3)
        {

            return Dot(Cross(v1, v2), v3);

        }
        /// <summary>
        /// 组件功能:我提供了一个目标向量组件的功能。
        /// 这些都是作为一个整体的向量数学上并不有效。
        /// 例如，有没有一个向量提高电源（据我所知），
        /// 但是PowComponents方法可以用来提高每到一个给定的功率的X，Y，Z的概念。
        /// 萨姆组件这种方法简单相加的矢量分量（X，Y，Z）
        /// </summary>
        public static double SumComponents(Vector3D v1)
        {

            return (v1.X + v1.Y + v1.Z);

        }

        /// <summary> 电源:这种方法给定的功率乘以向量的组件。 </summary>
        public static Vector3D PowComponents(Vector3D v1, double power)
        {

            return

            (

               new Vector3D

               (

                  Math.Pow(v1.X, power),

                  Math.Pow(v1.Y, power),

                  Math.Pow(v1.Z, power)

               )

            );

        }

        public static readonly Vector3D origin = new Vector3D(0, 0, 0);

        public static readonly Vector3D xAxis = new Vector3D(1, 0, 0);

        public static readonly Vector3D yAxis = new Vector3D(0, 1, 0);

        public static readonly Vector3D zAxis = new Vector3D(0, 0, 1);

        public static readonly Vector3D MinValue = new Vector3D(Double.MinValue, Double.MinValue, Double.MinValue);

        public static readonly Vector3D MaxValue = new Vector3D(Double.MaxValue, Double.MaxValue, Double.MaxValue);

        public static readonly Vector3D Epsilon = new Vector3D(Double.Epsilon, Double.Epsilon, Double.Epsilon);

        #endregion - 静态方法 end-

    }
}
