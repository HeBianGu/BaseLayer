#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/3/29 16:03:03
 * 文件名：ProgramBindModel
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

namespace HebianGu.Product.WinHelper
{
    /// <summary> 文件绑定实体 </summary>
    class ProgramBindModel
    {
        private string _file;
        /// <summary> 说明 </summary>
        public string File
        {
            get { return _file; }
            set { _file = value; }
        }

        private string _path;
        /// <summary> 说明 </summary>
        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }

    }
}
