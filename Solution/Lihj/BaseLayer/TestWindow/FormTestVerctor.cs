using HebianGu.ComLibModule.MathHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestWindow
{
    public partial class FormTestVerctor : Form
    {
        public FormTestVerctor()
        {
            InitializeComponent();

            Init();
        }


        /// <summary> 此方法的说明 </summary>
        public void Init()
        {
            first = new List<Vector3D>();

            for (int i = 0; i < 100; i++)
            {
                //Vector3D p2 = new Vector3D(i * 80, 1 / (i+1));

                Vector3D p2 = new Vector3D(i*10, i*10);

                first.Add(p2);

                // HTodo  ：说明 

            }

            second = new List<Vector3D>();

            for (int i = 10; i < 600; i++)
            {
                Vector3D p2 = new Vector3D(i * 20, i * i);

                second.Add(p2);

            }

        }

        List<Vector3D> first;

        List<Vector3D> second;

        Graphics g;

        Vector3D myPoint = new Vector3D(200, 100);


        double v1 = 500;
        double v2 = 35;

        private void FormTestVerctor_Load(object sender, EventArgs e)
        {
            this.BuildGraphics();

            this.RefreshGraphics(g);

            this.pictureBox1.Refresh();
        }

        public void BuildGraphics()
        {
            Bitmap b = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);

            g = Graphics.FromImage(b);

            this.pictureBox1.Image = b;
        }

        public void RefreshGraphics(Graphics g)
        {
            g.Clear(Color.White);

            Pen p = new Pen(Color.Black);

            Pen p1 = new Pen(Color.Red);

            Pen pblue = new Pen(Color.Blue);

            Vector3D pcross = first.First();

            double minDistance = double.MaxValue;

            for (int i = 0; i < first.Count; i++)
            {
                if (i == 0) continue;

                Vector3D f1 = first[i - 1];

                Vector3D f2 = first[i];

                g.DrawLine(p, (int)f1.X, (int)f1.Y, (int)f2.X, (int)f2.Y);

                g.FillEllipse(p1.Brush, (int)f2.X, (int)f2.Y, 5, 5);

                Vector3D pcrossPer = this.GetNearLestPoint(f1, f2, myPoint);

                double perDistance = myPoint.Distance(pcrossPer);

                if (perDistance < minDistance)
                {
                    minDistance = perDistance;
                    pcross = pcrossPer;
                }

            }

            Vector3D pcross2 = second.First();
            double minDistance2 = double.MaxValue;

            for (int i = 0; i < second.Count; i++)
            {
                if (i == 0) continue;

                Vector3D f1 = second[i - 1];

                Vector3D f2 = second[i];

                g.DrawLine(p, (int)f1.X, (int)f1.Y, (int)f2.X, (int)f2.Y);

                g.FillEllipse(p1.Brush, (int)f2.X, (int)f2.Y, 5, 5);

                Vector3D pcrossPer = this.GetNearLestPoint(f1, f2, myPoint);

                double perDistance = myPoint.Distance(pcrossPer);

                if (perDistance < minDistance2)
                {
                    minDistance2 = perDistance;
                    pcross2 = pcrossPer;
                }
            }

            g.FillEllipse(pblue.Brush, (int)myPoint.X, (int)myPoint.Y, 5, 5);

            // Todo ：求投影点 
            //Vector3D end = first.First(l => l.X >= myPoint.X);
            //Vector3D fir = first.Last(l => l.X < myPoint.X);
            //Vector3D pcross = this.GetNearLestPoint(fir, end, myPoint);
            Pen p3 = new Pen(Color.Black);
            g.DrawRectangle(p3, (int)pcross.X, (int)pcross.Y, 5, 5);
            g.DrawLine(p3, (int)myPoint.X, (int)myPoint.Y, (int)pcross.X, (int)pcross.Y);
            double l1 = Math.Sqrt((myPoint.X - pcross.X) * (myPoint.X - pcross.X) + (myPoint.Y - pcross.Y) * (myPoint.Y - pcross.Y));

            // Todo ：求投影点 
            //Vector3D end2 = second.First(l => l.X >= myPoint.X);
            //Vector3D fir2 = second.Last(l => l.X < myPoint.X);
            //Vector3D pcross2 = this.GetNearLestPoint(fir2, end2, myPoint);
            Pen p2 = new Pen(Color.Black);
            g.DrawRectangle(p2, (int)pcross2.X, (int)pcross2.Y, 5, 5);
            g.DrawLine(p2, (int)myPoint.X, (int)myPoint.Y, (int)pcross2.X, (int)pcross2.Y);
            double l2 = Math.Sqrt((myPoint.X - pcross2.X) * (myPoint.X - pcross2.X) + (myPoint.Y - pcross2.Y) * (myPoint.Y - pcross2.Y));


            if (v1 > v2)
            {
                // Todo ：计算差值 
                double myValue = (l2 * v1 + l1 * v2) / (l1 + l2);
                g.DrawString(myValue.ToString(), this.Font, p2.Brush, (int)myPoint.X + 10, (int)myPoint.Y);
            }


        }


        /// <summary>
        /// 求直线外一点到该直线的投影点
        /// </summary>
        /// <param name="pLine">线上任一点</param>
        /// <param name="k">直线斜率</param>
        /// <param name="pOut">线外指定点</param>
        /// <param name="pProject">投影点</param>
        protected Vector3D GetProjectivePoint(Vector3D pLine, Vector3D pLine2, Vector3D pOut)
        {
            Vector3D pProject = new Vector3D();

            double k = (pLine2.Y - pLine.Y) / (pLine2.X - pLine.X);

            if (k == 0) //垂线斜率不存在情况
            {
                pProject.X = pOut.X;
                pProject.Y = pLine.Y;
            }
            else
            {
                pProject.X = (float)((k * pLine.X + pOut.X / k + pOut.Y - pLine.Y) / (1 / k + k));
                pProject.Y = (float)(-1 / k * (pProject.X - pOut.X) + pOut.Y);
            }

            return pProject;
        }


        /// <summary> 求点到线段最近的点 </summary>
        public Vector3D GetNearLestPoint(Vector3D startLine, Vector3D endLine, Vector3D point)
        {
            Vector3D first = point - startLine;

            Vector3D second = point - endLine;

            Vector3D line = endLine - startLine;

            //double firstCross = Vector3D.Angle(first, line);

            //double secondCross = Vector3D.Angle(second,line);

            double dot1 = first.Dot(line);

            double dot2 = second.Dot(line);

            // Todo ：在外边 
            if (dot1 * dot2 < 0)
            {
                return this.GetProjectivePoint(startLine, endLine, point);
            }

            if (first.Magnitude() <= second.Magnitude())
            {
                return startLine;
            }

            return endLine;

        }

        /// <summary> 点到线段的距离 </summary>
        public double PointToSegDist(Vector3D point, Vector3D startLine, Vector3D endLine)
        {
            return this.PointToSegDist(point.X, point.Y, startLine.X, startLine.Y, endLine.X, endLine.Y);
        }


        /// <summary> 点到线段的距离 </summary>
        public double PointToSegDist(double x, double y, double x1, double y1, double x2, double y2)
        {
            double cross = (x2 - x1) * (x - x1) + (y2 - y1) * (y - y1);
            if (cross <= 0) return Math.Sqrt((x - x1) * (x - x1) + (y - y1) * (y - y1));

            double d2 = (x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1);
            if (cross >= d2) return Math.Sqrt((x - x2) * (x - x2) + (y - y2) * (y - y2));

            double r = cross / d2;
            double px = x1 + (x2 - x1) * r;
            double py = y1 + (y2 - y1) * r;
            return Math.Sqrt((x - px) * (x - px) + (py - y1) * (py - y1));
        }


        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            //myPoint = new Vector3D(e.X, e.Y);

            //this.RefreshGraphics(g);

            //this.pictureBox1.Refresh();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            myPoint = new Vector3D(e.X, e.Y);

            this.RefreshGraphics(g);

            this.pictureBox1.Refresh();
        }
    }
}
