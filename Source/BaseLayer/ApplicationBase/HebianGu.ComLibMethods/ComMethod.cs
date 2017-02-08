using HebianGu.ComLibModule.Define;
using HebianGu.ComLibModule.Struct;
using HHebianGu.ComLibModule.Delegate;
using HHebianGu.ComLibModule.DelegateEx;
using ICSharpCode.SharpZipLib.Zip;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibMethods
{
    /// <summary> Json操作类 </summary>
    public class JsonMethod
    {

        #region -Start Json-

        /// <summary> 将对象序列化为Json字符串 </summary>
        public static string
            JsonSerialize(object o)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(o);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary> 将Json字符串反序列化为对象 </summary>
        public static object
            JsonDeSerialize(string s, Type t)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject(s, t);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary> 将Json字符串反序列化为对象 </summary>
        public static T
            JsonDeSerialize<T>(string s)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(s);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary> 获得Json字符串中的指定 </summary>
        public static object
            Json_GetValue(String json, String key)
        {
            JsonReader reader = new JsonTextReader(new StringReader(json));
            bool isFind = false;
            while (reader.Read())
            {
                if (isFind)
                    return reader.Value;

                if (reader.TokenType == JsonToken.PropertyName
                    && reader.Value.ToString() == key)
                    isFind = true;
            }
            return null;
        }

        #endregion - Json End-

    }

    /// <summary> 日志操作类 </summary>
    public class LogMethod
    {
        private static string _assPath;
        private static string _errLogPath = "C:\\ErrorLog";
        private static string _runLogPath = "C:\\RunLog";
        private static readonly string ProcessName;

        static LogMethod()
        {
            try
            {
                ProcessName = Process.GetCurrentProcess().ProcessName;

                string file = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
                //if (System.Environment.CurrentDirectory.TrimEnd('\\').ToUpper() 
                //    == AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\').ToUpper())//Windows应用程序则相等
                if (string.IsNullOrEmpty(file) || !file.EndsWith("web.config", StringComparison.CurrentCultureIgnoreCase))
                {
                    _assPath = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
                }
                else
                {
                    _assPath = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\Bin";
                }

                _errLogPath = _assPath + "\\ErrorLog";
                if (!Directory.Exists(_errLogPath))
                {
                    Directory.CreateDirectory(_errLogPath);
                }
                _runLogPath = _assPath + "\\RunLog";
                if (!Directory.Exists(_runLogPath))
                {
                    Directory.CreateDirectory(_runLogPath);
                }
            }
            catch
            {
            }
        }


        #region - 写日志 -

        public static void WriteErrLog(Exception ex)
        {
            WriteErrLog(DateTime.Now, ex);
        }

        public static void WriteErrLog(DateTime time, Exception ex)
        {
            WriteErrLog(string.Empty, time, ex);
        }

        public static void WriteErrLog(string fileSign, DateTime time, Exception ex)
        {
            WriteErrLog(fileSign, time, ex, string.Empty);
        }

        private static void WriteErrLog(string fileSign, DateTime time, Exception ex, string innerSign)
        {
            try
            {
                DateTime now = time;
                string fileName = _errLogPath + "\\" + (String.IsNullOrEmpty(fileSign) ? "" : fileSign + "_") + now.ToString("yyyyMMdd") + ".log";
                File.AppendAllText(
                    fileName
                    ,
                    (string.IsNullOrEmpty(innerSign) ? now.ToString("[HHmmss]") : innerSign) + "[SP2]"
                    + ex.GetType().FullName + "[SP2]"
                    + ex.Source + "[SP2]"
                    + ex.TargetSite.DeclaringType.FullName + "[SP2]"
                    + ex.TargetSite.Name + "[SP2]"
                    + ex.StackTrace.Replace(Environment.NewLine, ";")
                    + ex.Message.Replace(Environment.NewLine, ";").Replace("\n", "") + "[SP1]"
                    + Environment.NewLine
                    ,
                    Encoding.UTF8);

                if (ex.InnerException != null)
                {
                    WriteErrLog(fileSign, time, ex.InnerException, innerSign + " ->");
                }
            }
            catch { }
        }

        public static void WriteRunLog(string msg)
        {
            WriteRunLog(DateTime.Now, msg);
        }

        public static void WriteRunLog(DateTime time, string msg)
        {
            WriteRunLog(null, time, msg);
        }

        /// <summary> 写入运行日志 </summary>
        /// <param name="fileSign">标识</param>
        /// <param name="time">时间</param>
        /// <param name="msg">日志内容</param>
        public static void WriteRunLog(string fileSign, DateTime time, string msg)
        {
            try
            {
                DateTime now = time;
                string fileName = _runLogPath + "\\" + (string.IsNullOrEmpty(fileSign) ? "" : fileSign + "_") + now.ToString("yyyyMMdd") + ".log";
                File.AppendAllText(
                    fileName
                    ,
                    now.ToString("[HHmmss]") + "[SP2]"
                    + msg
                    + Environment.NewLine
                    ,
                    Encoding.UTF8);
            }
            catch { }
        }

        #endregion
    }

    /// <summary> 压缩包操作类 </summary>
    public class ZipMethod
    {
        private static string _assPath;
        /// <summary> 压缩 byte[]  </summary>
        public static byte[] GZipBt(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (System.IO.Compression.GZipStream gz
                    = new System.IO.Compression.GZipStream(ms, System.IO.Compression.CompressionMode.Compress, true))
                {
                    gz.Write(data, 0, data.Length);
                    gz.Close();
                }
                return ms.ToArray();
            }

        }

        /// <summary> 解压 byte[]  </summary>
        public static byte[] UGZipBt(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (System.IO.Compression.GZipStream gz
                    = new System.IO.Compression.GZipStream(new MemoryStream(data), System.IO.Compression.CompressionMode.Decompress, true))
                {
                    byte[] buffer = new byte[4096];
                    int rsv;
                    while ((rsv = gz.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, rsv);
                    }
                    gz.Close();
                }
                return ms.ToArray();
            }
        }

        #region - Zip 压缩&解压文件 -
        /// <summary>功能：zip格式压缩文件  
        /// </summary>
        /// <param name="dirPath">被压缩的文件夹夹路径</param>   
        /// <param name="zipFilePath">生成压缩文件的路径，为空则默认与被压缩文件夹同一级目录，名称为：文件夹名+.zip</param>
        /// <param name="fileName">传出正在处理的文件名。</param>
        /// <param name="fileProg">文件进度。</param>
        /// <param name="strmProg">文件流进度。</param> 
        public static void ZipFile(string dirPath, string zipFilePath, Method<string> fileName, Method<int, int> fileProg, Method<int, int> strmProg)
        {

            if (string.IsNullOrEmpty(dirPath))
            {
                throw new Exception("压缩路径不能为空！");
            }
            else if (!Directory.Exists(dirPath))
            {
                throw new Exception("未在本地电脑上发现压缩路径！");
            }

            dirPath = dirPath.TrimEnd('\\');

            //压缩文件名为空时使用文件夹名＋.zip
            if (string.IsNullOrEmpty(zipFilePath))
            {
                zipFilePath = dirPath + ".zip";
            }

            try
            {
                string[] filenames = Directory.GetFiles(dirPath, "*.*", SearchOption.AllDirectories);
                if (fileProg != null) fileProg(filenames.Length, 0);
                using (ZipOutputStream s = new ZipOutputStream(File.Create(zipFilePath)))
                {
                    s.SetLevel(9);
                    byte[] buffer = new byte[4096];
                    for (int i = 0; i < filenames.Length; i++)
                    {
                        string file = filenames[i];
                        ZipEntry entry = new ZipEntry(file.Substring(dirPath.Length + 1));
                        if (fileName != null) fileName(entry.Name);
                        entry.DateTime = DateTime.Now;
                        s.PutNextEntry(entry);
                        using (FileStream fs = File.OpenRead(file))
                        {
                            int fsLeng = (int)fs.Length;
                            int ctLeng = 0;
                            int sourceBytes;
                            do
                            {
                                sourceBytes = fs.Read(buffer, 0, buffer.Length);
                                s.Write(buffer, 0, sourceBytes);
                                ctLeng += sourceBytes;
                                if (strmProg != null) strmProg(fsLeng, ctLeng);
                            } while (sourceBytes > 0);
                        }
                        if (fileProg != null) fileProg(filenames.Length, i + 1);
                    }
                    s.Finish();
                    s.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>功能：解压zip格式的文件。   
        /// </summary>   
        /// <param name="zipFilePath">压缩文件路径</param>   
        /// <param name="unZipDir">解压文件存放路径,为空时默认与压缩文件同一级目录下，跟压缩文件同名的文件夹</param>   
        /// <param name="err">出错信息</param>   
        /// <returns>解压是否成功</returns>   
        public static void UnZipFile(string zipFilePath, string unZipDir, Method<string> fileName, Method<int, int> fileProg, Method<int, int> strmProg)
        {
            if (string.IsNullOrEmpty(zipFilePath))
            {
                throw new Exception("压缩文件不能为空！");
            }

            zipFilePath = TransferFileActPath(zipFilePath);

            if (!File.Exists(zipFilePath))
            {
                throw new Exception("压缩文件不存在！");
            }

            //解压文件夹为空时默认与压缩文件同一级目录下，跟压缩文件同名的文件夹   
            if (string.IsNullOrEmpty(unZipDir))
            {
                unZipDir = Path.Combine(Path.GetDirectoryName(zipFilePath), Path.GetFileNameWithoutExtension(zipFilePath));
            }

            unZipDir = TransferFileActPath(unZipDir);

            unZipDir = unZipDir.TrimEnd('\\');

            if (!Directory.Exists(unZipDir))
                Directory.CreateDirectory(unZipDir);

            try
            {
                using (ZipInputStream s = new ZipInputStream(File.OpenRead(zipFilePath)))
                {
                    ZipEntry theEntry;
                    while ((theEntry = s.GetNextEntry()) != null)
                    {
                        if (theEntry.Name != String.Empty)
                        {
                            if (fileName != null) fileName(theEntry.Name);

                            string fullFileName = Path.Combine(unZipDir, theEntry.Name);

                            if (!Directory.Exists(Path.GetDirectoryName(fullFileName)))
                                Directory.CreateDirectory(Path.GetDirectoryName(fullFileName));

                            using (FileStream streamWriter = File.Create(fullFileName))
                            {
                                int size = 2048;
                                byte[] data = new byte[2048];
                                int crtSize = 0;
                                while (true)
                                {
                                    size = s.Read(data, 0, data.Length);
                                    if (size > 0)
                                    {
                                        streamWriter.Write(data, 0, size);
                                        crtSize += size;
                                        if (strmProg != null) strmProg((int)theEntry.Size, crtSize);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                streamWriter.Flush();
                            }
                        }
                    }//while   
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }//解压结束  
        #endregion

        #region - GZip 压缩&解压文件 -

        public static void GZipFile(string dirPath, string zipFilePath, Method<string> fileName, Method<int, int> fileProg, Method<int, int> strmProg)
        {
            try
            {
                GZip.Compress(dirPath, "*.*", SearchOption.AllDirectories, Path.GetDirectoryName(zipFilePath), Path.GetFileName(zipFilePath), true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UnGZipFile(string zipFilePath, string unZipDir, Method<string> fileName, Method<int, int> fileProg, Method<int, int> strmProg)
        {
            try
            {
                GZip.Decompress(Path.GetDirectoryName(zipFilePath), unZipDir, Path.GetFileName(zipFilePath));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        /// <summary> 将相对路径转换为绝对路径,相对与主运行程序 </summary>
        /// <param name="path">需转换的路径</param>
        /// <returns>结果路径</returns>
        public static string
            TransferFileActPath(string path)
        {
            try
            {
                string fnlFile = path;

                if (!Path.IsPathRooted(fnlFile))
                {
                    if (fnlFile.StartsWith(@"..\"))
                    {
                        string appPath = _assPath;
                        while (fnlFile.StartsWith(@"..\"))
                        {
                            fnlFile = fnlFile.Remove(0, 3);
                            appPath = appPath.Substring(0, appPath.LastIndexOf('\\'));
                        }
                        fnlFile = appPath + "\\" + fnlFile;
                    }
                    else
                    {
                        fnlFile = Path.Combine(_assPath, fnlFile.TrimStart('.', '\\'));
                    }
                }
                else if (fnlFile.StartsWith("\\"))
                {
                    fnlFile = Path.Combine(Path.GetPathRoot(_assPath), fnlFile.TrimStart('\\'));
                }

                return fnlFile;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    /// <summary> 网络操作类 </summary>
    public class NetMethod
    {
        /// <summary> 验证网络 </summary>
        public static bool CheckNet(string ip, string port, int timeOut)
        {
            if (CheckNetWay() == CHWay.Ping)
            {
                return Ping(ip, timeOut);
            }
            else
            {
                return Telnet(ip, port, timeOut);
            }
        }

        /// <summary> 验证网络 </summary>
        public static CHWay CheckNetWay()
        {
            string value = "PING";
            if (System.Configuration.ConfigurationManager.AppSettings["CheckNet"] != null)
            {
                value = System.Configuration.ConfigurationManager.AppSettings["CheckNet"].ToUpper();
            }

            if (value == "TELNET")
            {
                return CHWay.Telnet;
            }
            else
            {
                return CHWay.Ping;
            }
        }

        public static bool Telnet(string ip, string port, int timeOut)
        {
            try
            {
                bool value = false;
                using (System.Net.Sockets.TcpClient tc = new System.Net.Sockets.TcpClient())
                {
                    tc.SendTimeout = timeOut;
                    tc.Connect(ip, int.Parse(port));
                    value = tc.Connected;
                    tc.Close();
                }
                return value;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 指定（发送回送消息后）等待 ICMP 回送答复消息的最大毫秒数。
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static bool Ping(string ip, int timeOut)
        {
            try
            {
                using (System.Net.NetworkInformation.Ping p = new System.Net.NetworkInformation.Ping())
                {
                    return p.Send(ip, timeOut).Status == System.Net.NetworkInformation.IPStatus.Success;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public const string _reIp = @"^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$";

        /// <summary> 验证IP地址 </summary>
        public static bool IsIP(string str)
        {
            return new System.Text.RegularExpressions.Regex(_reIp).IsMatch(str);
        }

        public static IPAddress[] V6_IP
        {
            get
            {
                List<IPAddress> ips = new List<IPAddress>();
                IPAddress[] ipAds = System.Net.Dns.GetHostAddresses(Dns.GetHostName());
                if (ipAds != null && ipAds.Length > 0)
                {
                    foreach (var forip in ipAds)
                    {
                        if (forip.AddressFamily == AddressFamily.InterNetworkV6)
                            ips.Add(forip);
                    }
                }
                return ips.ToArray();
            }
        }

        public static IPAddress[] V4_IP
        {
            get
            {
                List<IPAddress> ips = new List<IPAddress>();
                IPAddress[] ipAds = System.Net.Dns.GetHostAddresses(Dns.GetHostName());
                if (ipAds != null && ipAds.Length > 0)
                {
                    foreach (var forip in ipAds)
                    {
                        if (forip.AddressFamily == AddressFamily.InterNetwork)
                            ips.Add(forip);
                    }
                }
                return ips.ToArray();
            }
        }


        public enum CHWay
        {
            Ping,
            Telnet
        }
    }

    /// <summary> 文件夹操作类 </summary>
    public class DirectoryMethod
    {
        #region - 拷贝目录 -

        /// <summary>
        /// 拷贝目录内容
        /// </summary>
        /// <param name="source">源目录</param>
        /// <param name="destination">目的目录</param>
        /// <param name="copySubDirs">是否拷贝子目录</param>
        public static void CopyDirectory(string source, string destination, bool copySubDirs)
        {
            try
            {
                CopyDirectory(new DirectoryInfo(source), new DirectoryInfo(destination), copySubDirs);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 拷贝目录内容
        /// </summary>
        /// <param name="source">源目录</param>
        /// <param name="destination">目的目录</param>
        /// <param name="copySubDirs">是否拷贝子目录</param>
        public static void CopyDirectory(DirectoryInfo source, DirectoryInfo destination, bool copySubDirs)
        {
            try
            {
                if (!destination.Exists)
                {
                    destination.Create(); //目标目录若不存在就创建
                }
                FileInfo[] files = source.GetFiles();
                foreach (FileInfo file in files)
                {
                    file.CopyTo(Path.Combine(destination.FullName, file.Name), true); //复制目录中所有文件
                }
                if (copySubDirs)
                {
                    DirectoryInfo[] dirs = source.GetDirectories();
                    foreach (DirectoryInfo dir in dirs)
                    {
                        string destinationDir = Path.Combine(destination.FullName, dir.Name);
                        CopyDirectory(dir, new DirectoryInfo(destinationDir), copySubDirs); //复制子目录
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }

    /// <summary> 程序集操作类 </summary>
    public class AssemblyMethod
    {
        private static string _assPath;
        private static readonly string ProcessName;

        static AssemblyMethod()
        {
            ProcessName = Process.GetCurrentProcess().ProcessName;

            string file = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            //if (System.Environment.CurrentDirectory.TrimEnd('\\').ToUpper() 
            //    == AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\').ToUpper())//Windows应用程序则相等
            if (string.IsNullOrEmpty(file) || !file.EndsWith("web.config", StringComparison.CurrentCultureIgnoreCase))
            {
                _assPath = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
            }
            else
            {
                _assPath = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\Bin";
            }
        }

        /// <summary> 应用程序所在物理目录 </summary>
        public static string ApplicationDirectory
        {
            get { return _assPath; }
        }

        ///// <summary>获得应用程序集的版本发布时间。 </summary>
        //public static GenericContainer<string, DateTime> GetEntryAssemblyV_T()
        //{
        //    object[] attrS = System.Reflection.Assembly.GetEntryAssembly().GetCustomAttributes(true);

        //    if (attrS != null && attrS.Length > 0)
        //    {
        //        string title = string.Empty;
        //        string version = string.Empty;

        //        foreach (object oFor in attrS)
        //        {
        //            if (oFor is AssemblyTitleAttribute)
        //            {
        //                title = (oFor as AssemblyTitleAttribute).Title;
        //            }
        //            else if (oFor is AssemblyFileVersionAttribute)
        //            {
        //                version = (oFor as AssemblyFileVersionAttribute).Version;
        //            }
        //        }

        //        Dictionary<string, DateTime> vTimes = new Dictionary<string, DateTime>(3);
        //        vTimes.Add("LONGO.LicenceServer V1.0", new DateTime(2011, 11, 11));
        //        vTimes.Add("LONGO.LicenceBrowser V1.0", new DateTime(2011, 11, 11));
        //        vTimes.Add("LONGO.LicenceBuilder V1.0", new DateTime(2011, 11, 11));

        //        if (!string.IsNullOrEmpty(version))
        //        {
        //            double date = double.Parse(version.Substring(version.IndexOf('.', 2) + 1));
        //            string ver = version.Substring(0, version.IndexOf('.', 2));
        //            if (vTimes.ContainsKey(title + " V" + ver))
        //            {
        //                return new GenericContainer<string, DateTime>(version, vTimes[title + " V" + ver].AddDays(date));
        //            }
        //        }
        //    }

        //    return null;
        //}
    }


}
