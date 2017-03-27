using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HebianGu.ObjectBase.ObjectHelper;

namespace HebianGu.ComLibModule.PathHelper
{
    /// <summary> 文件操作的扩展方法 </summary>
    public static class PathExtend
    {
        /// <summary> 更改路径字符串的扩展名 </summary>
        public static string ChangeExtension(this string path, string extension)
        {
            return Path.ChangeExtension(path, extension);
        }

        /// <summary> 将字符串数组组合成一个路径 </summary>
        public static string Combine(this string[] paths)
        {
            return Path.Combine(paths);
        }

        /// <summary> 返回指定路径字符串的目录信息 </summary>
        public static string GetDirectoryName(this string path)
        {
            return Path.GetDirectoryName(path);
        }

        /// <summary> 返回指定的路径字符串的扩展名 </summary>
        public static string GetExtension(this string path)
        {
            return Path.GetExtension(path);
        }

        /// <summary>  返回指定路径字符串的文件名和扩展名 </summary>
        public static string GetFileName(this string path)
        {
            return Path.GetFileName(path);
        }

        /// <summary> 返回不具有扩展名的指定路径字符串的文件名 </summary>
        public static string GetFileNameWithoutExtension(this string path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }

        /// <summary> 返回指定路径字符串的绝对路径 </summary>
        public static string GetFullPath(this string path)
        {
            return Path.GetFullPath(path);
        }

        /// <summary> 获取指定路径的根目录信息 </summary>
        public static string GetPathRoot(this string path)
        {
            return Path.GetPathRoot(path);
        }

        /// <summary> 返回随机文件夹名或文件名 </summary>
        public static string GetRandomFileName()
        {
            return Path.GetRandomFileName();
        }

        /// <summary> 确定路径是否包括文件扩展名 </summary>
        public static bool HasExtension(this string path)
        {
            return Path.HasExtension(path);
        }

        /// <summary> 获取指示指定的路径字符串是否包含根的值 </summary>
        public static bool IsPathRooted(this string path)
        {
            return Path.IsPathRooted(path);
        }

        /// <summary> 文件名扩展 3T1_100 </summary>
        /// <param name="fileName">3T1_100</param>
        /// <param name="code">'-'</param>
        /// <param name="index">"MAT"</param>
        /// <returns>3T1_100-MAT</returns>
        public static string ExFileName(this string fileName, string code, string index)
        {
            string start = fileName.Split(new string[] { code }, StringSplitOptions.RemoveEmptyEntries)[0];
            return start + code + index;
        }

        /// <summary> 拷贝文件夹 </summary>
        /// <param name="srcdir"> 老文件夹 </param>
        /// <param name="desdir"> 新文件夹 </param>
        /// <param name="isCreateFolder"> 是否新建文件夹 </param>
        public static void CopyDirectory(this string srcdir, string desdir, bool isCreateFolder)
        {
            string folderName = srcdir.Substring(srcdir.LastIndexOf("\\") + 1);

            string desfolderdir = isCreateFolder ? desdir + "\\" + folderName : desdir;

            if (desdir.LastIndexOf("\\") == (desdir.Length - 1))
            {
                desfolderdir = desdir + folderName;
            }
            string[] filenames = Directory.GetFileSystemEntries(srcdir);

            foreach (string file in filenames)// 遍历所有的文件和目录
            {
                if (Directory.Exists(file))// 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
                {

                    string currentdir = desfolderdir + "\\" + file.Substring(file.LastIndexOf("\\") + 1);
                    if (!Directory.Exists(currentdir))
                    {
                        Directory.CreateDirectory(currentdir);
                    }

                    CopyDirectory(file, desfolderdir, isCreateFolder);
                }

                else // 否则直接copy文件
                {
                    string srcfileName = file.Substring(file.LastIndexOf("\\") + 1);

                    srcfileName = desfolderdir + "\\" + srcfileName;


                    if (!Directory.Exists(desfolderdir))
                    {
                        Directory.CreateDirectory(desfolderdir);
                    }
                    if (File.Exists(srcfileName))
                        File.Delete(srcfileName);
                    FileInfo pFile = new FileInfo(file);
                    FileInfo fileNew = pFile.CopyTo(srcfileName, true);
                    fileNew.IsReadOnly = false;

                    //File.Copy(file, srcfileName);
                }
            }
        }

        /// <summary> 是否是路径 </summary>
        public static bool CheckPath(this string path)
        {
            string pattern = @"^[a-zA-Z]:(((\\(?! )[^/:*?<>\""|\\]+)+\\?)|(\\)?)\s*$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(path);
        }

        /// <summary> 根据文件路径获取文件 C:\WorkArea\1\3T1-Mat.data </summary>
        /// <param name="path"> 参考文件 C:\WorkArea\1\3T1.data  </param>
        /// <returns>新文件全路径 C:\WorkArea\5\3T1-Mat.data</returns>
        public static string GetFileFullPath(this string path, string fileName)
        {
            return Path.GetDirectoryName(path) + "\\" + Path.GetFileName(fileName);
        }

        /// <summary> 文件夹+文件 文件夹结尾带不带\\都可以 文件是不是全路径都行 </summary>
        public static string AppendFile(this string dic, string fileName)
        {
            return dic.EndsWith("\\")
                ? dic + Path.GetFileName(fileName)
                : dic + "\\" + Path.GetFileName(fileName);

        }

        ///// <summary> 文件名扩展 3T1_100 </summary>
        ///// <param name="fileName">3T1_100</param>
        ///// <param name="code">'-'</param>
        ///// <param name="index">"MAT"</param>
        ///// <returns>3T1_100-MAT</returns>
        //public static string ExFileName(this string fileName, string code, string index)
        //{
        //    string start = fileName.Split(new string[] { code }, StringSplitOptions.RemoveEmptyEntries)[0];
        //    return start + code + index;
        //}

    }
}
