using HebianGu.ObjectBase.Logger;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.FileEx
{
    public static class FileHelper
    {
        /// <summary> 打开文件</summary>
        public static void OpenFileInfo(this string FilePath)
        {
            if (File.Exists(FilePath))
            {
                System.Diagnostics.Process.Start("explorer.exe", FilePath);
            }
            else
            {
                ComLogProvider.Log.RunLog("No Find File " + FilePath);
            }
        }

        /// <summary>  打开文件夹 </summary>
        public static void OpenDirectoryInfo(this string dirctroyPath)
        {

            if (Directory.Exists(dirctroyPath))
            {
                System.Diagnostics.Process.Start("explorer.exe", dirctroyPath);
            }
            else
            {
                ComLogProvider.Log.RunLog("No Find File " + dirctroyPath);
            }

        }

        /// <summary> 删除文件 </summary>
        public static bool DeleteFile(this string fileFullPath)
        {
            if (File.Exists(fileFullPath))
            {
                if (File.GetAttributes(fileFullPath) == FileAttributes.Normal)
                {
                    File.Delete(fileFullPath);
                }
                else
                {
                    File.SetAttributes(fileFullPath, FileAttributes.Normal);
                    File.Delete(fileFullPath);
                }
                return true;
            }
            else
            {
                ComLogProvider.Log.RunLog("No Find File " + fileFullPath);
            }
            return false;
        }

        /// <summary> 根据传来的文件全路径，获取文件名称部分默认包括扩展名 </summary>
        public static string GetFileName(this string fileFullPath)
        {
            if (File.Exists(fileFullPath))
            {
                var f = new FileInfo(fileFullPath);
                return f.Name;
            }
            return null;
        }

        /// <summary> 根据传来的文件全路径，获取文件名称部分 </summary>
        public static string GetFileName(this string fileFullPath, bool includeExtension)
        {
            if (File.Exists(fileFullPath))
            {
                var f = new FileInfo(fileFullPath);
                if (includeExtension)
                {
                    return f.Name;
                }
                return f.Name.Replace(f.Extension, "");
            }
            return null;
        }

        /// <summary> 根据传来的文件全路径，获取新的文件名称全路径,一般用作临时保存用 </summary>
        public static string GetNewFileFullName(this string fileFullPath)
        {
            if (File.Exists(fileFullPath))
            {
                var f = new FileInfo(fileFullPath);
                string tempFileName = fileFullPath.Replace(f.Extension, "");
                for (int i = 0; i < 1000; i++)
                {
                    fileFullPath = tempFileName + i.ToString() + f.Extension;
                    if (File.Exists(fileFullPath) == false)
                    {
                        break;
                    }
                }
            }
            return fileFullPath;
        }

        /// <summary> 根据传来的文件全路径，获取文件扩展名不包括“.”，如“doc” </summary>
        public static string GetFileExtension(this string fileFullPath)
        {
            if (File.Exists(fileFullPath))
            {
                var f = new FileInfo(fileFullPath);
                return f.Extension;
            }
            return null;
        }

        /// <summary> 根据传来的文件全路径，外部打开文件，默认用系统注册类型关联软件打开 </summary>
        public static bool OpenFile(this string fileFullPath)
        {
            if (File.Exists(fileFullPath))
            {
                Process.Start(fileFullPath);
                return true;
            }
            return false;
        }

        /// <summary> 根据传来的文件全路径，得到文件大小，规范文件大小称呼，如1ＧＢ以上，单位用ＧＢ，１ＭＢ以上，单位用ＭＢ，１ＭＢ以下，单位用ＫＢ </summary>
        public static string GetFileSize(this string fileFullPath)
        {
            if (File.Exists(fileFullPath))
            {
                var f = new FileInfo(fileFullPath);
                long fl = f.Length;
                if (fl > 1024 * 1024 * 1024)
                {
                    return Convert.ToString(Math.Round((fl + 0.00) / (1024 * 1024 * 1024), 2)) + " GB";
                }
                if (fl > 1024 * 1024)
                {
                    return Convert.ToString(Math.Round((fl + 0.00) / (1024 * 1024), 2)) + " MB";
                }
                return Convert.ToString(Math.Round((fl + 0.00) / 1024, 2)) + " KB";
            }
            return null;
        }

        /// <summary> 文件转换成二进制，返回二进制数组Byte[] </summary>
        public static byte[] FileToStreamByte(this string fileFullPath)
        {
            byte[] fileData = null;
            if (File.Exists(fileFullPath))
            {
                var fs = new FileStream(fileFullPath, FileMode.Open);
                fileData = new byte[fs.Length];
                fs.Read(fileData, 0, fileData.Length);
                fs.Close();
            }
            return fileData;
        }

        /// <summary> 二进制数组Byte[]生成文件 </summary>
        public static bool ByteStreamToFile(this string createFileFullPath, byte[] streamByte)
        {
            if (File.Exists(createFileFullPath) == false)
            {
                FileStream fs = File.Create(createFileFullPath);
                fs.Write(streamByte, 0, streamByte.Length);
                fs.Close();
                return true;
            }
            return false;
        }

        /// <summary> 二进制数组Byte[]生成文件，并验证文件是否存在，存在则先删除 </summary>
        public static bool ByteStreamToFile(this string createFileFullPath, byte[] streamByte, bool fileExistsDelete)
        {
            if (File.Exists(createFileFullPath))
            {
                if (fileExistsDelete && DeleteFile(createFileFullPath) == false)
                {
                    return false;
                }
            }
            FileStream fs = File.Create(createFileFullPath);
            fs.Write(streamByte, 0, streamByte.Length);
            fs.Close();
            return true;
        }

        /// <summary> 读写文件，并进行匹配文字替换 替换字典 </summary>
        public static void ReadAndWriteFile(this string pathRead, string pathWrite, Dictionary<string, string> replaceStrings)
        {
            var objReader = new StreamReader(pathRead);
            if (File.Exists(pathWrite))
            {
                File.Delete(pathWrite);
            }
            var streamw = new StreamWriter(pathWrite, false, Encoding.GetEncoding("utf-8"));
            var readLine = objReader.ReadToEnd();
            if (replaceStrings != null && replaceStrings.Count > 0)
            {
                foreach (var dicPair in replaceStrings)
                {
                    readLine = readLine.Replace(dicPair.Key, dicPair.Value);
                }
            }
            streamw.WriteLine(readLine);
            objReader.Close();
            streamw.Close();
        }

        /// <summary> 读取文件 </summary>
        public static string ReadFile(this string filePath)
        {
            var objReader = new StreamReader(filePath);
            string readLine = null;
            if (File.Exists(filePath))
            {
                readLine = objReader.ReadToEnd();
            }
            objReader.Close();
            return readLine;
        }

        /// <summary> 写入文件 </summary>
        public static void WriteFile(this string pathWrite, string content)
        {
            if (File.Exists(pathWrite))
            {
                File.Delete(pathWrite);
            }
            var streamw = new StreamWriter(pathWrite, false, Encoding.GetEncoding("utf-8"));
            streamw.WriteLine(content);
            streamw.Close();
        }

        /// <summary> 读取并附加文本 </summary>
        public static void ReadAndAppendFile(this string filePath, string content)
        {
            File.AppendAllText(filePath, content, Encoding.GetEncoding("utf-8"));
        }

        /// <summary> 复制文件 </summary>
        public static void CopyFile(this string sources, string dest)
        {
            var dinfo = new DirectoryInfo(sources);
            foreach (FileSystemInfo f in dinfo.GetFileSystemInfos())
            {
                var destName = Path.Combine(dest, f.Name);
                if (f is FileInfo)
                {
                    File.Copy(f.FullName, destName, true);
                }
                else
                {
                    Directory.CreateDirectory(destName);
                    CopyFile(f.FullName, destName);
                }
            }
        }

        /// <summary> 复制文件 </summary>
        public static void MoveFile(this string sources, string dest)
        {
            var dinfo = new DirectoryInfo(sources);
            foreach (FileSystemInfo f in dinfo.GetFileSystemInfos())
            {
                var destName = Path.Combine(dest, f.Name);
                if (f is FileInfo)
                {
                    File.Move(f.FullName, destName);
                }
                else
                {
                    Directory.CreateDirectory(destName);
                    MoveFile(f.FullName, destName);
                }
            }
        }

        /// <summary> 检测指定文件是否存在,如果存在则返回true。 </summary>
        public static bool IsExistFile(this string filePath)
        {
            return File.Exists(filePath);
        }

        /// <summary> 文件是否被占用 </summary>
        public static bool IsFileOpen(this string filePath)
        {
            bool result = false;

            System.IO.FileStream fs = null;
            try
            {
                fs = File.OpenWrite(filePath);
                fs.Close();
            }
            catch (Exception ex)
            {
                result = true;
            }
            return result;//true 打开 false 没有打开
        }


        /// <summary> 设置文件特性 </summary>
        public static void SetAttribute(this string filePath, FileAttributes attr)
        {
            filePath.FileSetAction(l => l.Attributes = attr);
        }

        /// <summary> 去掉指定特性 </summary>
        public static void RemoveAttribute(this string filePath, FileAttributes attr)
        {
            filePath.FileSetAction(l => l.Attributes &= ~attr);
        }

        /// <summary> 增加指定特性 </summary>
        public static void AddAttribute(this string filePath, FileAttributes attr)
        {
            filePath.FileSetAction(l => l.Attributes |= attr);
        }

        /// <summary> 对文件执行Func操作</summary>
        public static void FileSetAction(this string filePath, Action<FileInfo> action)
        {
            if (File.Exists(filePath))
            {
                FileInfo file = new FileInfo(filePath);

                action(file);
            }
            else
            {
                ComLogProvider.Log.RunLog("No Find File " + filePath);
            }



        }
    }
}
