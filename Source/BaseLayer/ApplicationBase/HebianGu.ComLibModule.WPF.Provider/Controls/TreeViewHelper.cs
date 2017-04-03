#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/4/3 19:17:58
 * 文件名：TreeViewHelper
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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HebianGu.ComLibModule.WPF.Provider.Controls
{
   public  partial class TreeViewHelper
    {
        /// <summary> 加载磁盘树结构 </summary>
        public void LoadTreeHandler(TreeView treeView, Action<TreeViewItem> expandAct = null)
        {
            var drivers = Directory.GetLogicalDrives();

            foreach (var item in drivers)
            {
                TreeViewItem t = new TreeViewItem();
                t.Header = item;
                t.Tag = item;
                treeView.Items.Add(t);

                LoadChildNode(t);

                t.Expanded += (object sender, RoutedEventArgs e) =>
                {
                    if (expandAct != null)
                        expandAct(sender as TreeViewItem);
                };
            }
        }

        /// <summary> 加载磁盘树结构 </summary>
        public void LoadTree(TreeView treeView)
        {
            Action<TreeViewItem> act = l =>
              {
                  LoadChildChildNode(l);
              };

            this.LoadTree(treeView);
        }


        /// <summary> 加载当前节点 </summary>
        void LoadChildNode(TreeViewItem treeItem, Action<TreeViewItem> expandAct = null)
        {
            var folders = Directory.GetDirectories(treeItem.Tag.ToString());

            foreach (var item in folders)
            {
                TreeViewItem t = new TreeViewItem();
                t.Header = System.IO.Path.GetFileName(item);
                t.Tag = item;
                treeItem.Items.Add(t);

                LoadChildChildNode(t);

                t.Expanded += (object sender, RoutedEventArgs e) =>
                {
                    LoadChildChildNode(t);

                    if (expandAct != null)
                        expandAct(sender as TreeViewItem);
                };
            }
        }

        /// <summary> 加载当前节点下级节点的下级节点(只加载一个,为了显示有子节点) </summary>
        void LoadChildChildNode(TreeViewItem treeItem, Action<TreeViewItem> expandAct = null)
        {
            foreach (var item in treeItem.Items)
            {
                TreeViewItem tvi = item as TreeViewItem;

                var folders = Directory.GetDirectories(tvi.Tag.ToString());

                if (tvi.Items.Count > 0) continue;

                foreach (var it in folders)
                {
                    TreeViewItem t = new TreeViewItem();
                    t.Header = System.IO.Path.GetFileName(it);
                    t.Tag = it;
                    tvi.Items.Add(t);

                    t.Expanded += (object sender, RoutedEventArgs e) =>
                    {
                        LoadChildChildNode(t);

                        if (expandAct != null)
                            expandAct(sender as TreeViewItem);
                    };
                }
            }
        }
    }

    /// <summary> 此类的说明 </summary>
   partial class TreeViewHelper
    {
        #region - Start 单例模式 -

        /// <summary> 单例模式 </summary>
        private static TreeViewHelper t = null;

        /// <summary> 多线程锁 </summary>
        private static object localLock = new object();

        /// <summary> 创建指定对象的单例实例 </summary>
        public static TreeViewHelper Instance
        {
            get
            {
                if (t == null)
                {
                    lock (localLock)
                    {
                        if (t == null)
                            return t = new TreeViewHelper();
                    }
                }
                return t;
            }
        }
        /// <summary> 禁止外部实例 </summary>
        private TreeViewHelper()
        {

        }
        #endregion - 单例模式 End -

    }
}
