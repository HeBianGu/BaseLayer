#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/11/9 17:41:05
 * 文件名：InsertionSorter
 * 说明：
 * 插入排序算法。对想提高C#语言编程能力的朋友，我们可以互相探讨一下。
 * 如：下面的程序，并没有实现多态，来，帮它实现一下。
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
    /// <summary> 插入排序法 </summary>
    public static class InsertionSorter
    {
        /// <summary> 插入排序法 </summary>
        public static void InsertionSort(this int[] list)
        {
            for (int i = 1; i < list.Length; i++)
            {
                int t = list[i];
                int j = i;
                while ((j > 0) && (list[j - 1] > t))
                {
                    list[j] = list[j - 1];
                    --j;
                }
                list[j] = t;
            }
        }
    }
}
