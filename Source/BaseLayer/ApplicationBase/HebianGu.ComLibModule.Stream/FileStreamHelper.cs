#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2016/12/6 10:28:38
 * 文件名：FileStreamHelper
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

namespace HebianGu.ComLibModule.StreamEx
{
    
    /// <summary> 二进制文件读取器 </summary>
    public static class FileStreamHelper
    {
        
        /// <summary> 读取指定长度的String类型 </summary>
        public static string ReadString(this FileStream stream, int position, int size)
        {
            Func<byte[], String> trans = l =>
            {
                List<byte> newList = l.ToList();

                int index = newList.FindIndex(k => k == '\0');

                newList.RemoveRange(index, newList.Count - index);

                return System.Text.Encoding.ASCII.GetString(newList.ToArray());
            };
            return ReadStruct<String>(stream, position, size, trans);
        }

        
        /// <summary> 读取Int类型 </summary>
        public static int ReadInt(this FileStream stream, int position)
        {
            return ReadStruct<int>(stream, position, sizeof(int), l => BitConverter.ToInt32(l, 0));
        }

        
        /// <summary> 读取Double类型 </summary>
        public static double ReadDouble(this FileStream stream, int position)
        {
            return ReadStruct<double>(stream, position, sizeof(double), l => BitConverter.ToDouble(l, 0));
        }

        /// <summary> 读取二进制转换成指定泛型 </summary>
        public static T ReadStruct<T>(this FileStream stream, int position, int size, Func<byte[], T> trans)
        {
            stream.Seek(position, SeekOrigin.Begin);

            byte[] bytes = new byte[size];

            stream.Read(bytes, 0, size);

            return trans(bytes);
        }

        /// <summary> 读取项 </summary>
        public static List<string> ReadItems(this FileStream stream, int position, int c, int itemlenght)
        {
            // 说明 ：    
            List<string> plists = new List<string>();

            for (int i = 0; i < c; i++)
            {
                stream.Seek(position, SeekOrigin.Begin);

                plists.Add(stream.ReadString(position, itemlenght));

                position += itemlenght;
            }

            return plists;

        }
    }
}
