using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.FontExtend
{
    /// <summary> Font字体帮助类 </summary>
    public static class FontHelper
    {

        /// <summary> 设置字体格式 </summary>
        public static void SetFontStyle(this Font sender, FontStyle style)
        {
            sender = new Font(sender.FontFamily, sender.Size, style);
        }

        /// <summary> 设置字体类型 </summary>
        public static void SetFamilyName(this Font sender, string familyName)
        {
            sender = new Font(familyName, sender.Size, sender.Style);
        }

        /// <summary> 设置字体大小 </summary>
        public static void SetFamilyName(this Font sender, float emsize)
        {
            sender = new Font(sender.FontFamily, emsize, sender.Style);
        }

    }
}
