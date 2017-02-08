using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Threading;

namespace HebianGu.ComLibModule.ControlHelper
{
    public partial class MainForm : Form
    {      
        public MainForm()
        {
            InitializeComponent();
            //Config.Init();
            //this.Location = Config.Location;
            //this.ck_TopMost.Checked = this.TopMost = Config.TopMost;
            CurrentColor = Color.Black;
            Txt16.Text = "#000000";
            this.Text = "取色器" + (this.TopMost ? "[置顶]" : "");
        }
        bool IsMouseDown = false;

        ExtractColor etc;

        public Color CurrentColor
        {
            get { return lbColor.BackColor; }
            set
            {
                btnCopy.Text = "复制";
                lbColor.BackColor = value;
                lbColor.ForeColor = value;
                label3.Text = string.Format("R:{0},G:{1},B:{2}", value.R, value.G, value.B);
                Txt16.Text= ColorTranslator.ToHtml(value);
            }
           
        }

        public Point DefaultLocation { get; set; }

        public Point CurrentLocation { get; set; }

        
        private void MainForm_Load(object sender, EventArgs e)
        {
            DefaultLocation = this.Location;
            this.extractPad1.CenterPoint = new Point(this.extractPad1.Width / 2, this.extractPad1.Height / 2);
        }

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }
        private void lbColorMouseDown(object sender, MouseEventArgs e)
        {
            this.Text = "取色器"+(this.TopMost?"[置顶]":"")+"-正在取色。。。";
            etc = new ExtractColor();
            etc.TopMost = true;
            etc.Show();
            this.Activate();


            IsMouseDown = true;
            Cursor = Cursors.Arrow;
            DefaultLocation = this.Location;
            this.Location = new Point(0, 0);
            CurrentLocation = this.Location;
        }

        private void lbColorMouseUp(object sender, MouseEventArgs e)
        {
            if (etc != null)
            {
                etc.Close();
            }
            IsMouseDown = false;
            Cursor = Cursors.Default;
            this.Location = DefaultLocation;
            this.Text = "取色器" + (this.TopMost ? "[置顶]" : "");
        }

        private void lbColorMouseMove(object sender, MouseEventArgs e)
        {

            if (IsMouseDown)
            {
                Point p = Cursor.Position;
                if (this.Location.X < p.X && (this.Location.X + this.Width) > p.X
                    && this.Location.Y < p.X && (this.Location.Y + this.Height) > p.Y)
                {
                    if (this.Location.X == 0)
                        this.Location = new Point(SystemInformation.WorkingArea.Width - this.Width, 0);
                    else
                        this.Location = new Point(0, 0);
                }
                else
                {
                    this.extractPad1.Change();
                }
            }
        }      

                
        private void btnCopyClick(object sender, EventArgs e)
        {
            Txt16.SelectAll();
            Txt16.Copy();
            btnCopy.Text = "已复制";
        }
        
        private void ck_TopMostCheckedChanged(object sender, EventArgs e)
        {
           this.TopMost = ck_TopMost.Checked;
           this.Text = "取色器" + (this.TopMost ? "[置顶]" : "");
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Config.TopMost = this.TopMost;
            //Config.Location = this.Location;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "图片文件|*.jpg;*.bmp;*.png;*.gif";
            if (open.ShowDialog() == DialogResult.OK)
            {
                ImportImgColor imp = new ImportImgColor();
                if(Application.OpenForms["importImg"]!=null)
                {
                    imp = Application.OpenForms["importImg"] as ImportImgColor;
                    imp.CurrentImage = Image.FromFile(open.FileName);
                    return;
                }
                imp.Name = "importImg";
                imp.CurrentImage = Image.FromFile(open.FileName);
                if (this.Location.X > imp.Width)
                {
                    imp.Location = new Point(this.Location.X - imp.Width, this.Location.Y);
                }
                else
                {
                    imp.Location = new Point(this.Width+this.Location.X, this.Location.Y);
                }
                this.LocationChanged += (obj, arg) =>
                {
                    if (this.Location.X > imp.Width)
                    {
                        imp.Location = new Point(this.Location.X - imp.Width, this.Location.Y);
                    }
                    else
                    {
                        imp.Location = new Point(this.Width + this.Location.X, this.Location.Y);
                    }
                };
                imp.TopMost = this.TopMost;
                imp.Show();
            }
        }

        private void extractPad1_SelectedColorChanged(object sender, ColorChangedEventArgs e)
        {
            this.CurrentColor = e.NewColor;
        }

    }
}
