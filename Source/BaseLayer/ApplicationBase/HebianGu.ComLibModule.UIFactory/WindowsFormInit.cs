using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HebianGu.ComLibModule.UIFactory
{
    /// <summary> windows窗体初始化器 </summary>
    public static class WindowsFormInit
    {

        /// <summary> 设置窗体初始显示位置为鼠标位置 </summary>
        public static void  UISetShowPosInCursor(this Form form)
        {
            form.StartPosition = FormStartPosition.Manual;
            form.Location = Cursor.Position;
        }

        /// <summary> 设置不显示图标最大最小按钮 </summary>
        public static void UISetDialogFormat(this Form form)
        {
            form.ShowIcon = false;
            form.MaximizeBox = false;
            form.MinimizeBox = false;
        }
    }
}
