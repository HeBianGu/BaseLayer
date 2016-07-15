#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/11/9 17:26:22
 * 文件名：SelectionSorter
 * 说明：
 * 本人用了C#开发出选择排序算法。希望能为C#语言的学习者带来一些益处。
 * 不要忘了，学语言要花大力气学数据结构和算法。
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

namespace HebianGu.ComLibModule.Sorter
{
    /// <summary> 选择排序法 </summary>
    public static class SelectionSorter
    {
        /// <summary> 选择排序 </summary>
        public static void SelectionSort(this int[] list)
        {
            int min;
            for (int i = 0; i < list.Length - 1; i++)
            {
                min = i;
                for (int j = i + 1; j < list.Length; j++)
                {
                    if (list[j] < list[min])
                        min = j;
                }
                int t = list[min];
                list[min] = list[i];
                list[i] = t;
            }
        }
    }
}
