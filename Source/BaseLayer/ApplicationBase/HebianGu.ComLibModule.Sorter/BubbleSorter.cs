#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/11/9 17:20:29
 * 文件名：BubbleSorter
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

namespace HebianGu.ComLibModule.Sorter
{
    /// <summary> 冒泡排序算法 </summary>
    public static class BubbleSorter
    {
        /// <summary> 冒泡排序算法 </summary>
        public static void BubbleSort(this int[] list)
        {
            int i, j, temp;
            bool done = false;
            j = 1;
            while ((j < list.Length) && (!done))
            {
                done = true;
                for (i = 0; i < list.Length - j; i++)
                {
                    if (list[i] > list[i + 1])
                    {
                        done = false;
                        temp = list[i];
                        list[i] = list[i + 1];
                        list[i + 1] = temp;
                    }
                }
                j++;
            }
        }

        /// <summary> 泛型冒泡排序算法 </summary>
        public static void BubbleSort<T>(this List<T> list) where T:IComparable
        {
            int i, j;
            T temp;
            bool done = false;
            j = 1;
            while ((j < list.Count) && (!done))
            {
                done = true;
                for (i = 0; i < list.Count - j; i++)
                {
                    
                    if (list[i].CompareTo(list[i + 1])>0)
                    {
                        done = false;
                        temp = list[i];
                        list[i] = list[i + 1];
                        list[i + 1] = temp;
                    }
                }
                j++;
            }
        }
    }

}
