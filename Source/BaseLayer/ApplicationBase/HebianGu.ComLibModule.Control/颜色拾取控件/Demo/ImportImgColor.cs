using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HebianGu.ComLibModule.ControlHelper
{
    public partial class ImportImgColor : Form
    {
        public ImportImgColor()
        {
            InitializeComponent();
            mainForm = Application.OpenForms["MainForm"] as MainForm;
        }
        private Image img;

        public Image CurrentImage
        {
            get { return img; }
            set
            {
                img = value;
                this.pictureBox1.Image = value;
                pictureBox1.Size = value.Size;
                this.Activate();
            }
        }
        MainForm mainForm;
        private void DrawO(MouseEventArgs e)
        {
            if (CurrentImage == null) return;
            try
            {
                Bitmap bit = (Bitmap)CurrentImage.Clone();
                Graphics g = Graphics.FromImage(bit);
                mainForm.CurrentColor = bit.GetPixel(e.X, e.Y);
                Color temp = Color.FromArgb(255 - mainForm.CurrentColor.R, 255 - mainForm.CurrentColor.G, 255 - mainForm.CurrentColor.B);
                g.DrawEllipse(new Pen(temp, 1), new Rectangle(e.X - 2, e.Y - 2, 4, 4));
                g.Save();
                this.pictureBox1.Image = bit;
                g.Dispose();
                GC.Collect();
            }
            catch
            {
            }
        }
        bool IsMouseDown = false;
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            IsMouseDown = true;
            DrawO(e);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseDown)
            {
                DrawO(e);
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            IsMouseDown = false;

        }

    }
}
