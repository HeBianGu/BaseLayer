#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/3/29 16:02:16
 * 文件名：LogBindModel
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HebianGu.Product.WinHelper
{
    public class LogBindModel
    {
        private string _message;
        /// <summary> 说明 </summary>
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        private Action _act;
        /// <summary> 说明 </summary>
        public Action Act
        {
            get { return _act; }
            set { _act = value; }
        }

        public override string ToString()
        {
            return _message;
        }

        public Color FontColor
        {
            get
            {
                return _act == null ? Color.Black : Color.Blue;
            }
        }
    }
}
