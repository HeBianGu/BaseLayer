using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.String.字节扩展
{
    public static class ByteHelper
    {
        /// <summary> 转换为十六进制字符串 </summary>
        public static string ToHex(this byte b)
        {
            return b.ToString("X2");
        }

        /// <summary> 转换为十六进制字符串 </summary>
        public static string ToHex(this IEnumerable<byte> bytes)
        {
            var sb = new StringBuilder();
            foreach (byte b in bytes)
                sb.Append(b.ToString("X2"));
            return sb.ToString();
        }

        /// <summary>   转换为Base64字符串 </summary>
        public static string ToBase64String(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        /// <summary> 转换为基础数据类型 </summary>
        public static int ToInt(this byte[] value, int startIndex)
        {
            return BitConverter.ToInt32(value, startIndex);
        }

        /// <summary> 转换为基础数据类型 </summary>
        public static long ToInt64(this byte[] value, int startIndex)
        {
            return BitConverter.ToInt64(value, startIndex);
        }

        /// <summary> 转换为指定编码的字符串  </summary>
        public static string Decode(this byte[] data, Encoding encoding)
        {
            return encoding.GetString(data);
        }

        /// <summary> 使用指定算法Hash </summary>
        public static byte[] Hash(this byte[] data, string hashName)
        {
            HashAlgorithm algorithm;
            if (string.IsNullOrEmpty(hashName)) algorithm = HashAlgorithm.Create();
            else algorithm = HashAlgorithm.Create(hashName);
            return algorithm.ComputeHash(data);
        }

        /// <summary> 使用指定算法Hash </summary>
        public static byte[] Hash(this byte[] data)
        {
            return Hash(data, null);
        }

        /// <summary> index从0开始 ex是否为1 </summary>
        public static bool GetBit(this byte b, int index)
        {
            return (b & (1 << index)) > 0;
        }

        /// <summary> 将第index位设为1 </summary>
        public static byte SetBit(this byte b, int index)
        {
            b |= (byte)(1 << index);
            return b;
        }

        /// <summary> 将第index位设为0 </summary>
        public static byte ClearBit(this byte b, int index)
        {
            b &= (byte)((1 << 8) - 1 - (1 << index));
            return b;
        }

        /// <summary> 将第index位取反 </summary>
        public static byte ReverseBit(this byte b, int index)
        {
            b ^= (byte)(1 << index);
            return b;
        }

        /// <summary> 保存为文件 </summary> 
        public static void Save(this byte[] data, string path)
        {
            File.WriteAllBytes(path, data);
        }

        /// <summary> 保存为内存流 </summary>
        public static MemoryStream ToMemoryStream(this byte[] data)
        {
            return new MemoryStream(data);
        }
    }
}
