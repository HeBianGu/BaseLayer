using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HebianGu.ComLibModule.ControlHelper
{
    public partial class ExtractPad : Control
    {
        public ExtractPad()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.DoubleBuffered = true;
        }


        public event EventHandler<ColorChangedEventArgs> SelectedColorChanged;

        private Color _selectedColor;
        public Color SelectedColor
        {
            get
            {
                return _selectedColor;
            }
            set
            {
                if (SelectedColorChanged != null)
                {
                    SelectedColorChanged(this, new ColorChangedEventArgs(value, _selectedColor));
                }
                _selectedColor = value;
            }
        }

        public Bitmap Background
        {
            get;
            set;
        }


        private Point _centerPoint;
        public Point CenterPoint
        {
            get
            {
                return _centerPoint;
            }
            set
            {
                var point = value;
                int x = 0, y = 0;
                x = point.X < 1 ? 1 : (point.X > Width - 1 ? Width - 1 : point.X);
                y = point.Y < 1 ? 1 : (point.Y > Height - 1 ? Height - 1 : point.Y);

                if (this.Width == 0) return;



                if (Background != null)
                {
                    try
                    {
                        //SelectedColor = Background.GetPixel(x / 2, y / 2);

                        int xMap = this.Background.Width * x / this.Width;
                        int yMap = this.Background.Height * y / this.Height;

                        SelectedColor = Background.GetPixel(xMap, yMap);
                    }
                    catch
                    {
                    }
                }
                _centerPoint = new Point(x, y);
            }
        }

        public bool IsMouseDown { get; set; }

        public void Change()
        {
            this.Background = new Bitmap(this.Width / 2, this.Height / 2);
            Point p = Cursor.Position;
            Graphics graphics = Graphics.FromImage(Background);
            graphics.CopyFromScreen(new Point(p.X - Width / 4, p.Y - Height / 4), new Point(0, 0), Background.Size);
            this.CenterPoint = new Point(this.Width / 2, this.Height / 2);
            this.Refresh();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.IsMouseDown = true;
            this.CenterPoint = e.Location;
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.IsMouseDown = false;
            this.Refresh();
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (this.IsMouseDown)
            {
                this.CenterPoint = e.Location;
                this.Refresh();
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            var g1 = pe.Graphics;
            if (Background != null)
            {
                g1.DrawImage(Background, new Rectangle(0, 0, this.Width, this.Height));

            }
            if (CenterPoint == null)
            {
                CenterPoint = new Point(this.Width / 2, this.Height / 2);
            }
            //横线
            g1.DrawLine(Pens.Green, new Point(0, CenterPoint.Y), new Point(this.Width, CenterPoint.Y));
            //竖线
            g1.DrawLine(Pens.Green, new Point(CenterPoint.X, 0), new Point(CenterPoint.X, this.Height));

            g1.DrawRectangle(Pens.Gray, new Rectangle(0, 0, Width - 1, Height - 1));

        }
    }

    public class ColorChangedEventArgs : EventArgs
    {
        public Color NewColor { get; set; }

        public Color OldColor { get; set; }
        public ColorChangedEventArgs(Color newColor, Color oldColor)
        {

            this.NewColor = newColor;
            this.OldColor = oldColor;
        }
    }
}
