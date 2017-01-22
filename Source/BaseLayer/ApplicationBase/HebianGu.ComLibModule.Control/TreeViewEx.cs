#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/11/20 11:56:23
 * 文件名：TreeViewEx
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
using System.Windows.Forms;

namespace HebianGu.ComLibModule.ControlHelper
{
    /// <summary> winform treeview的扩展方法 </summary>
    public static class TreeViewEx
    {
        /// <summary> 遍历所有节点 执行操作方法 </summary>
        public static void Foreach(this TreeView treeView, Action<TreeNode> match)
        {
            var tns = treeView.Nodes;

            foreach (TreeNode tn in tns)
            {
                tn.Foreach(match);

            }
        }

        /// <summary> 遍历所有节点 执行操作方法 </summary>
        public static void Foreach(this TreeNode treeNode, Action<TreeNode> match)
        {
            //  执行方法
            match(treeNode);

            if (treeNode.Nodes.Count > 0)
            {
                //  遍历节点
                foreach (TreeNode tn in treeNode.Nodes)
                {
                    tn.Foreach(match);
                }
            }
        }

        /// <summary> 查找所有满足条件的节点 </summary>
        public static List<T> FindAll<T>(this TreeView treeView, Predicate<TreeNode> match) where T : TreeNode
        {
            List<T> find = new List<T>();
            var tns = treeView.Nodes;

            foreach (TreeNode tn in tns)
            {
                tn.FindAll<T>(match, ref find);
            }

            return find;
        }
        /// <summary> 查找所有满足条件的节点 </summary>
        public static void FindAll<T>(this TreeNode treeNode, Predicate<TreeNode> match, ref List<T> find) where T : TreeNode
        {
            //  执行方法
            bool isMatch = match(treeNode);

            if (isMatch && treeNode is T)
            {

                find.Add(treeNode as T);

            }
            if (treeNode.Nodes.Count > 0)
            {
                //  遍历节点
                foreach (TreeNode tn in treeNode.Nodes)
                {
                    tn.FindAll<T>(match, ref find);
                }
            }
        }

        public static List<T> FindAll<T>(this TreeView treeView) where T : TreeNode
        {
            return treeView.FindAll<T>(l => true);
        }

        public static List<T> FindAll<T>(this TreeNode treeNode) where T : TreeNode
        {
            List<T> ts = new List<T>();

            treeNode.FindAll<T>(l => true, ref ts);

            return ts;
        }

        public static List<T> FindAll<T>(this TreeNode treeNode, Predicate<TreeNode> match) where T : TreeNode
        {
            List<T> ts = new List<T>();

            treeNode.FindAll<T>(match, ref ts);

            return ts;
        }

        /// <summary> 在本节点下查找指定节点 </summary>
        public static List<T> FindChild<T>(this TreeNode treeNode, Predicate<TreeNode> match) where T : TreeNode
        {
            List<T> find = new List<T>();

            foreach (TreeNode tn in treeNode.Nodes)
            {
                //  执行方法
                bool isMatch = match(tn);

                if (isMatch && treeNode is T)
                {
                    find.Add(treeNode as T);

                }
            }

            return find;
        }

        /// <summary> 清空指定节点节点 </summary>
        public static void ClearNodes(this TreeNode treeNode, Predicate<TreeNode> match)
        {
            List<TreeNode> temp = new List<TreeNode>();

            foreach (TreeNode tn in treeNode.Nodes)
            {

                if (match(tn))
                {
                    temp.Add(tn);

                }
            }

            foreach (var v in temp)
            {
                treeNode.Nodes.Remove(v);

            }
        }

        /// <summary> 清空指定节点节点 </summary>
        public static void ClearNodes(this TreeView tree, Predicate<TreeNode> match)
        {
            List<TreeNode> temp = new List<TreeNode>();

            foreach (TreeNode tn in tree.Nodes)
            {

                if (match(tn))
                {
                    temp.Add(tn);

                }
            }

            foreach (var v in temp)
            {
                tree.Nodes.Remove(v);

            }
        }

        /// <summary> 清空除匹配以为的节点 </summary>
        public static void ClearWithOutNodes(this TreeView tree, Predicate<TreeNode> match)
        {
            List<TreeNode> temp = new List<TreeNode>();

            foreach (TreeNode tn in tree.Nodes)
            {

                if (!match(tn))
                {
                    temp.Add(tn);

                }
            }

            foreach (var v in temp)
            {
                tree.Nodes.Remove(v);

            }
        }
    }
}
