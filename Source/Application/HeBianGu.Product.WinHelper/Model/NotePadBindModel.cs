#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/3/29 16:03:26
 * 文件名：NotePadBindModel
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
    /// <summary> 记事本绑定实体 </summary>
    [Serializable]
    public class NotePadBindModel
    {

        private string _title = string.Empty;
        /// <summary> 说明 </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private string _detial = string.Empty;
        /// <summary> 说明 </summary>
        public string DetialMin
        {
            get
            {
                if (_detial.Length < 20)
                {
                    return _detial;
                }

                return _detial.Substring(0, 20) + " ...";
            }
            set { _detial = value; }
        }

        /// <summary> 说明 </summary>
        public string Detial
        {
            get
            {
                return _detial;
            }
            set { _detial = value; }
        }

        private int _level = 1;
        /// <summary> 说明 </summary>
        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }

        private DateTime _cTime = DateTime.Now;
        /// <summary> 说明 </summary>
        public DateTime CreateTime
        {
            get { return _cTime; }
            set { _cTime = value; }
        }

        public string CTime
        {
            get { return _cTime.ToString("yyyy-MM-dd"); }
        }

        private DateTime _notifyTime = DateTime.Now;
        /// <summary> 说明 </summary>
        public DateTime NotifyTime
        {
            get { return _notifyTime; }
            set { _notifyTime = value; }
        }

        /// <summary> 图片路径 </summary>
        public string ImagePath
        {
            get
            {
                switch (this.Level)
                {
                    case 1:
                        return "/HebianGu.Product.WinHelper;component/image/Viewer/1.ICO";
                    case 2:
                        return "/HebianGu.Product.WinHelper;component/image/Viewer/2.ICO";
                    case 3:
                        return "/HebianGu.Product.WinHelper;component/image/Viewer/3.ICO";
                    case 4:
                        return "/HebianGu.Product.WinHelper;component/image/Viewer/4.ICO";
                    case 5:
                        return "/HebianGu.Product.WinHelper;component/image/Viewer/5.ICO";
                    default:
                        return "/HebianGu.Product.WinHelper;component/image/Viewer/zhcn070.ico";
                }
            }
        }


        private List<int> _levelSource = new List<int>() { 1, 2, 3, 4, 5 };
        /// <summary> 说明 </summary>
        public List<int> LevelSource
        {
            get { return _levelSource; }
        }


    }
}
