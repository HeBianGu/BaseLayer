using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;


namespace CodeImage
{
    public partial class FrmExample : Form
    {
        public FrmExample()
        {
            InitializeComponent();
        }

        //生成二维码
        private void btnCreateD1_Click(object sender, EventArgs e)
        {
            EncodingOptions options = null;
            BarcodeWriter writer = null;

            options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = picD2.Width,
                Height = picD2.Height
            };
            writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            writer.Options = options;

            Bitmap bitmap = writer.Write(txtD2.Text);
            picD2.Image = bitmap;

        }

        //生成一维码
        private void btnCreateD2_Click(object sender, EventArgs e)
        {
            EncodingOptions options = null;
            BarcodeWriter writer = null;

            options = new EncodingOptions
            {
                //DisableECI = true,  
                //CharacterSet = "UTF-8",  
                Width = picD1.Width,
                Height = picD1.Height
            };
            writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.ITF;
            writer.Options = options;

            Bitmap bitmap = writer.Write(txtD1.Text);
            picD1.Image = bitmap;
        }
    }
}