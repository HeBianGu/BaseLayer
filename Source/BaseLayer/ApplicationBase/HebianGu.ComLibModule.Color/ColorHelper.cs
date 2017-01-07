#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/1/6 15:24:05
 * 文件名：ColorHelper
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.ColorHelper
{
    public static class ColorHelper
    {
        
        /// <summary> 拾取图片中颜色 </summary>
        public void ToColorFromXY(this Bitmap bit, int x, int y)
        {
            Graphics g = Graphics.FromImage(bit);
            Color c = bit.GetPixel(x, y);
            Color temp = Color.FromArgb(255 - c.R, 255 - c.G, 255 - c.B);
            g.DrawEllipse(new Pen(temp, 1), new Rectangle(x - 2, y - 2, 4, 4));
            g.Save();
            g.Dispose();
        }
    }
}
