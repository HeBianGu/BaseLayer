using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FCNS.Calendar
{
    public partial class RichTextBoxPlus : UserControl
    {
        public RichTextBoxPlus()
        {
            InitializeComponent();

            tb1.SuspendLayout();
            tb1.ImageList = imgList1;
            btnSave.ImageIndex = 0;

            btnFont.ImageIndex = 2;
            btnFontColor.ImageIndex = 4;

            btnBlod.ImageIndex = 5;
            btnItalic.ImageIndex = 6;
            btnUnderline.ImageIndex = 7;
            btnStrikeout.ImageIndex = 8;

            btnLeft.ImageIndex = 9;
            btnCenter.ImageIndex = 10;
            btnRight.ImageIndex = 11;

            btnPre.ImageIndex = 12;
            btnNext.ImageIndex = 13;

            btnCut.ImageIndex = 14;
            btnCopy.ImageIndex = 15;
            btnPaste.ImageIndex = 16;

            btnImg.ImageIndex = 17;

            foreach (string s in Enum.GetNames(typeof(KnownColor)))
            {
                btnFontColor.DropDownItems.Add(s);
            }
            tb1.ResumeLayout();
        }

        string fileName;//路径为程序目录的journal目录,文件名为'yyyyMMddHHmm'+'标题'
        public string JournalName
        {
            set
            {
                fileName = value;
                rtb1.LoadFile(Main.appPath + "journal" + fileName + ".jou", RichTextBoxStreamType.RichText);
            }
        }
        Font font;
        public void ChangeFontStyle(FontStyle style, bool styleChecked)
        {
            if (styleChecked)
            {
                rtb1.SelectionFont = new Font(rtb1.SelectionFont, rtb1.SelectionFont.Style | style);
            }
            else
            {
                rtb1.SelectionFont = new Font(rtb1.SelectionFont, rtb1.SelectionFont.Style & ~style);
            }
        }

        public void UpdateToolbar()
        {
            Font f;
            if (rtb1.SelectionFont != null)
            {
                f = rtb1.SelectionFont;
            }
            else
            {
                f = rtb1.Font;
            }
            btnFontSize.Text = f.Size.ToString();
            btnBlod.Checked = f.Bold;
            btnItalic.Checked = f.Italic;
            btnUnderline.Checked = f.Underline;
            btnStrikeout.Checked =f.Strikeout; 
           btnLeft.Checked= (rtb1.SelectionAlignment == HorizontalAlignment.Left); 
          btnCenter.Checked = (rtb1.SelectionAlignment == HorizontalAlignment.Center); 
          btnRight.Checked= (rtb1.SelectionAlignment == HorizontalAlignment.Right);
          f.Dispose();
        }

        private void rtb1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control)
            {
                ToolStripButton tbb = null;

                switch (e.KeyCode)
                {
                    case Keys.B:
                        tbb = btnBlod;
                        break;
                    case Keys.I:
                        tbb = btnItalic;
                        break;
                    case Keys.U:
                        tbb = btnUnderline;
                        break;
                    case Keys.OemMinus:
                        tbb = btnStrikeout;
                        break;
                }

                if (tbb != null)
                {
                    if (e.KeyCode != Keys.S)
                    {
                        tbb.Checked = !tbb.Checked;
                    }
                    tb1_ItemClicked(null, new ToolStripItemClickedEventArgs(tbb));
                }
            }

            if (e.KeyCode == Keys.Tab)
                rtb1.SelectedText = "\t";
        }

        private void rtb1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }

        private void rtb1_SelectionChanged(object sender, EventArgs e)
        {
                UpdateToolbar();
        }

        private void tb1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "保存":
                    rtb1.SaveFile(Main.appPath + fileName + ".jou", RichTextBoxStreamType.RichText);
                    break;

                case "字体":
                    FontDialog fd = new FontDialog();
                    fd.AllowSimulations = true;
                    fd.Color = rtb1.SelectionColor;
                    fd.FontMustExist = true;
                    fd.ShowColor = true;
                    fd.ShowEffects = true;
                    fd.Font = rtb1.SelectionFont;
                    if (fd.ShowDialog() == DialogResult.OK)
                    {
                        rtb1.SelectionFont = fd.Font;
                        rtb1.SelectionColor = fd.Color;
                    }
                    fd.Dispose();
                    break;

                case "加粗":
                    ChangeFontStyle(FontStyle.Bold,  !((ToolStripButton)e.ClickedItem).Checked);
                    break;
                case "斜体":
                    ChangeFontStyle(FontStyle.Italic,  !((ToolStripButton)e.ClickedItem).Checked);
                    break;
                case "下划线":
                    ChangeFontStyle(FontStyle.Underline,  !((ToolStripButton)e.ClickedItem).Checked);
                    break;
                case "删除线":
                    ChangeFontStyle(FontStyle.Strikeout,  !((ToolStripButton)e.ClickedItem).Checked);
                    break;

                case "左对齐":
                    rtb1.SelectionAlignment = HorizontalAlignment.Left;
                    btnCenter.Checked = btnRight.Checked = false;
                    break;
                case "居中":
                    rtb1.SelectionAlignment = HorizontalAlignment.Center;
                    btnLeft.Checked = btnRight.Checked = false;
                    break;
                case "右对齐":
                    rtb1.SelectionAlignment = HorizontalAlignment.Right;
                    btnLeft.Checked = btnCenter.Checked=false;
                    break;

                case "撤销":
                    rtb1.Undo();
                    break;
                case "重复":
                    rtb1.Redo();
                    break;

                case "剪贴":
                    {
                        if (rtb1.SelectedText.Length > 0)
                        {
                            rtb1.Cut();
                        }
                        break;
                    }
                case "复制":
                    {
                        if (rtb1.SelectedText.Length > 0)
                        {
                            rtb1.Copy();
                        }
                        break;
                    }
                case "粘贴":
                    {
                        try
                        {
                            rtb1.Paste();
                        }
                        catch
                        {
                            MessageBox.Show("粘贴失败");
                        }
                        break;
                    }

                case "图片":
                  
                    break;
            }
        }
      
        private void btnFontColor_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            rtb1.SelectionColor = Color.FromName(e.ClickedItem.Text);
        }

        private void btnFontSize_TextChanged(object sender, EventArgs e)
        {
            float f;
            if (float.TryParse(btnFontSize.Text, out f)&& f>0)
            {
                rtb1.SelectionFont = new Font(rtb1.SelectionFont.FontFamily, f);
            }
        }
    }
}