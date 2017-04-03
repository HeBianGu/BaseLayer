#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/3/29 16:03:52
 * 文件名：ClipBoradBindModel
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using HebianGu.ComLibModule.API;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using HebianGu.ComLibModule.WinHelper;

namespace HebianGu.Product.WinHelper
{
    /// <summary> 文件绑定实体 </summary>
    [Serializable]
    class ClipBoradBindModel
    {
        ClipBoardType _type;

        string _detial;
        public ClipBoradBindModel(string detial, ClipBoardType type)
        {
            Type = type;
            _detial = detial;
            this._createTime = DateTime.Now;
        }

        /// <summary> 说明 </summary>
        public string Title
        {
            get
            {
                switch (Type)
                {
                    case ClipBoardType.FileSystem:
                        return Path.GetFileName(_detial);
                    case ClipBoardType.Image:
                        return _detial;
                    case ClipBoardType.Text:
                        return _detial;
                    default:
                        return _detial;
                }
            }
        }

        public string Detial
        {
            get
            {
                return _detial;
            }
        }

        /// <summary> 图片路径 </summary>
        public Icon ImagePath
        {
            get
            {
                switch (Type)
                {
                    case ClipBoardType.FileSystem:
                        return IconHelper.Instance.GetSystemInfoIcon(_detial);
                    case ClipBoardType.Image:
                        return Icon.ExtractAssociatedIcon("./image/Viewer/M.ICO");
                    case ClipBoardType.Text:
                        return Icon.ExtractAssociatedIcon("./image/Viewer/T.ICO");
                    default:
                        return Icon.ExtractAssociatedIcon("./image/图标0A/D056.ICO");
                }

            }
        }

        private DateTime _createTime;
        /// <summary> 复制的时间 </summary>
        public string CreateTime
        {
            get { return _createTime.ToString("yyyy-MM-dd hh:mm:ss"); }
        }

        internal ClipBoardType Type
        {
            get
            {
                return _type;
            }

            set
            {
                _type = value;
            }
        }
    }
}
