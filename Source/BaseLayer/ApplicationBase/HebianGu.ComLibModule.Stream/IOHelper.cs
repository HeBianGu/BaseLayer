using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace HebianGu.ComLibModule.StreamEx
{
    public class IOHelper
    {
        /// <summary>
        /// 写入内容到文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="content">内容</param>
        /// <param name="isAppend">是否附加写入</param>
        /// <returns></returns>
        public static bool WriteFile(string path, string content, bool isAppend)
        {
            if (!CreateDirectory(path))
                return false;
            try
            {
                using (StreamWriter sw = new StreamWriter(path, isAppend, Encoding.Default))
                {
                    sw.WriteLine(content);
                    sw.Flush();
                    sw.Close();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 写入内容到文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="content">内容</param>
        /// <param name="isAppend">是否附加写入</param>
        /// <returns></returns>
        public static bool WriteFile(string path, string content, bool isAppend, Encoding encoding)
        {
            if (!CreateDirectory(path))
                return false;
            using (StreamWriter sw = new StreamWriter(path, isAppend, encoding))
            {
                sw.WriteLine(content);
                sw.Flush();
                sw.Close();
            }
            return true;
        }
        /// <summary>
        /// 从指定位置的文件中读取字符串。
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public static string ReadFile(string path)
        {
            if (!File.Exists(path))
                return string.Empty;
            string rs = string.Empty;
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                rs = sr.ReadToEnd();
                sr.Close();
            }
            return rs;
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns></returns>
        public static bool DeleteFile(string path)
        {
            if (!File.Exists(path))
                return true;
            try
            {
                File.Delete(path);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 复制文件到指定位置
        /// </summary>
        /// <param name="src">源路径</param>
        /// <param name="des">目标路径</param>
        /// <returns></returns>
        public static bool CopyFile(string src, string des)
        {
            if (!File.Exists(src))
                return false;
            try
            {
                File.Copy(src, des, true);
                return true;
            }
            catch { }
            return false;
        }
        /// <summary>
        /// 创建目录，已存在目录不操作。
        /// </summary>
        /// <param name="strDirectoryName">路径</param>
        /// <returns></returns>
        public static bool CreateDirectory(string strDirectoryName)
        {
            if (strDirectoryName == null || strDirectoryName == string.Empty)
                return false;
            try
            {
                strDirectoryName = Path.GetDirectoryName(strDirectoryName);
                if (!Directory.Exists(strDirectoryName))
                {
                    Directory.CreateDirectory(strDirectoryName);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="strDirectoryName">路径</param>
        /// <returns></returns>
        public static bool DeleteDirectory(string strDirectoryName)
        {
            if (strDirectoryName == null || strDirectoryName == string.Empty)
                return false;
            try
            {
                if (Directory.Exists(strDirectoryName))
                    Directory.Delete(strDirectoryName, true);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 路径合并
        /// </summary>
        /// <param name="paths">多个路径</param>
        /// <returns></returns>
        public static string CombinePath(params string[] paths)
        {
            if (paths.Length == 0)
            {
                throw new ArgumentException("please input path");
            }
            else
            {
                StringBuilder builder = new StringBuilder();
                string spliter = "\\";
                string firstPath = paths[0];
                if (firstPath.StartsWith("HTTP", StringComparison.OrdinalIgnoreCase))
                {
                    spliter = "/";
                }
                if (!firstPath.EndsWith(spliter))
                {
                    firstPath = firstPath + spliter;
                }
                builder.Append(firstPath);
                for (int i = 1; i < paths.Length; i++)
                {
                    string nextPath = paths[i];
                    if (nextPath.StartsWith("/") || nextPath.StartsWith("\\"))
                    {
                        nextPath = nextPath.Substring(1);
                    }
                    if (i != paths.Length - 1)//not the last one
                    {
                        if (nextPath.EndsWith("/") || nextPath.EndsWith("\\"))
                        {
                            nextPath = nextPath.Substring(0, nextPath.Length - 1) + spliter;
                        }
                        else
                        {
                            nextPath = nextPath + spliter;
                        }
                    }
                    builder.Append(nextPath);
                }
                return builder.ToString();
            }
        }
        /// <summary>
        /// 查找文件
        /// </summary>
        /// <param name="FoldPath"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static List<string> FindFile(string FoldPath, string filter)//要查找的文件夹和文件类型
        {
            List<string> fileNames = new List<string>();
            DirectoryInfo thefolder = new DirectoryInfo(FoldPath);
            foreach (DirectoryInfo nextfolder in thefolder.GetDirectories())
            {
                FindFile(nextfolder.FullName, filter);
            }
            foreach (FileInfo nextfile in thefolder.GetFiles(filter))
            {
                fileNames.Add(nextfile.FullName);
            }
            return fileNames;
        }
        /// <summary>
        /// 重命名文件
        /// </summary>
        /// <param name="oldPath">旧文件名</param>
        /// <param name="newPath">新文件名</param>
        public static void Rename(string oldPath, string newPath)
        {

            try
            {
                if (!File.Exists(oldPath))
                {
                    // This statement ensures that the file is created,            
                    // but the handle is not kept.                
                    //using (FileStream fs = File.Create(oldPath)) { }
                    return;
                }
                // Ensure that the target does not exist.         
                if (File.Exists(newPath))
                    File.Delete(newPath);
                // Move the file.    
                File.Move(oldPath, newPath);
            }
            catch (Exception ex)
            {

            }

        }

        /// <summary>
        /// 拷贝目录下所有的文件
        /// </summary>
        /// <param name="srcDir">源目录</param>
        /// <param name="tgtDir">目标目录</param>
        public static void CopyDirectory(string srcDir, string tgtDir)
        {
            DirectoryInfo source = new DirectoryInfo(srcDir);
            DirectoryInfo target = new DirectoryInfo(tgtDir);

            //if (target.FullName.StartsWith(source.FullName, StringComparison.CurrentCultureIgnoreCase))
            //{
            //    throw new Exception("父目录不能拷贝到子目录！");
            //}

            if (!source.Exists)
            {
                return;
            }

            if (!target.Exists)
            {
                target.Create();
            }

            FileInfo[] files = source.GetFiles();

            for (int i = 0; i < files.Length; i++)
            {
                File.Copy(files[i].FullName, target.FullName + @"\" + files[i].Name, true);
            }

            DirectoryInfo[] dirs = source.GetDirectories();

            for (int j = 0; j < dirs.Length; j++)
            {
                CopyDirectory(dirs[j].FullName, target.FullName + @"\" + dirs[j].Name);
            }
        } 
    }
}
