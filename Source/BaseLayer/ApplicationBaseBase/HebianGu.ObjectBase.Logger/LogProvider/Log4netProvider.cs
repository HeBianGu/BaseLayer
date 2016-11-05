#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/11/5 18:26:41  计算机名称：DEV-LIHAIJUN
 *
 * 文件名：LoggerOutput
 *
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using log4net;
using log4net.Appender;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ObjectBase.Logger
{
    /// <summary> 统一日志输出的类 </summary>
    public class Log4netProvider
    {

        /// <summary> 重置配置 </summary>
        public static void ReplaceFileTag(string logconfig)
        {
            try
            {
                System.IO.FileStream fs = new System.IO.FileStream(logconfig, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite);
                StreamReader sr = new StreamReader(fs, System.Text.Encoding.UTF8);
                string str = sr.ReadToEnd();
                sr.Close();
                fs.Close();

                if (str.IndexOf("#LOG_PATH#") > -1)
                {
                    str = str.Replace(@"#LOG_PATH#", System.Environment.GetEnvironmentVariable("TEMP") + @"\");
                    System.IO.FileStream fs1 = new System.IO.FileStream(logconfig, System.IO.FileMode.Open, System.IO.FileAccess.Write);
                    StreamWriter swWriter = new StreamWriter(fs1, System.Text.Encoding.UTF8);
                    swWriter.Flush();
                    swWriter.Write(str);
                    swWriter.Close();
                    fs1.Close();
                }
            }
            catch (System.Exception ex)
            {

            }
        }

        /// <summary> 初始化日志 </summary>
        public static void Init(string repository, string name)
        {

            string logconfig = @"log4net.config";

            ReplaceFileTag(logconfig);

            Stopwatch st = new Stopwatch();

            st.Start();

            log4net.GlobalContext.Properties["dynamicName"] = name;
            Logger = LogManager.GetLogger(name);

            st.Stop();

            if (st.ElapsedMilliseconds > 2000)
            {
                Logger.Info("log4net.config file ERROR!!!");

                string str;
                using (FileStream fs = new System.IO.FileStream(logconfig, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite))
                {
                    using (StreamReader sr = new StreamReader(fs, System.Text.Encoding.UTF8))
                    {
                        str = sr.ReadToEnd();
                        str = str.Replace(@"ref=""SQLAppender""", @"ref=""SQLAppenderError""");
                    }
                }

                using (FileStream fs1 = new System.IO.FileStream(logconfig, System.IO.FileMode.Open, System.IO.FileAccess.Write))
                {
                    using (StreamWriter swWriter = new StreamWriter(fs1, System.Text.Encoding.UTF8))
                    {
                        swWriter.Flush();
                        swWriter.Write(str);
                    }
                }
            }

            InitLogPath(repository);
        }

        // Todo ：定义日志文件路径格式 
        static void InitLogPath(string repository)
        {
            RollingFileAppender appender = new RollingFileAppender();

            appender.File = repository + "\\Log\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\";

            appender.AppendToFile = true;

            appender.MaxSizeRollBackups = -1;

            //appender.MaximumFileSize = "1MB";  

            appender.RollingStyle = log4net.Appender.RollingFileAppender.RollingMode.Date;

            appender.DatePattern = "yyyy-MM-dd_HH\".log\"";

            appender.StaticLogFileName = false;

            appender.LockingModel = new log4net.Appender.FileAppender.MinimalLock();

            appender.Layout = new log4net.Layout.PatternLayout("%date [%thread] %-5level - %message%newline");

            appender.ActivateOptions();

            log4net.Config.BasicConfigurator.Configure(appender);

        }

        /// <summary> Log4net日志内核 </summary>
        public static ILog Logger
        {
            get
            {
                // Todo ：没有初始化初始化 
                if (_log == null)
                {
                    Log4netProvider.Init(AppDomain.CurrentDomain.BaseDirectory, Process.GetCurrentProcess().ProcessName);
                }

                return _log;
            }
            set
            {
                _log = value;
            }
        }

        static ILog _log = null;

        /// <summary> 事件传送信息打印 </summary>
        public static void EventsMsg(object sender, object e, System.Diagnostics.StackFrame SourceFile)
        {
            string msg = string.Format("[FILE:{0} ],LINE:{1},{2}] sender:{3},e:{4}", SourceFile.GetFileName(), SourceFile.GetFileLineNumber(), SourceFile.GetMethod(), sender.GetType(), e.GetType());

            if (Logger != null)
            {
                Logger.Info(msg);
            }
        }
        
        // Todo ：信息格式 
        static string GetFileMsg(System.Diagnostics.StackFrame SourceFile)
        {
            return string.Format("FILE: [{0}] LINE:[{1}] Method:[{2}]", SourceFile.GetFileName(), SourceFile.GetFileLineNumber(), SourceFile.GetMethod());
        }

        /// <summary> 记录错误日志 </summary>
        private void Error(System.Diagnostics.StackFrame SourceFile, Exception ex)
        {
            if (ex == null) return;

            if (Logger == null) return;

            Logger.Info(GetFileMsg(SourceFile));

            Logger.Error(ex.Message);

            if (ex.InnerException == null) return;

            Logger.Fatal(ex.InnerException);
        }

        /// <summary> 过程日志 </summary>
        private void Info(System.Diagnostics.StackFrame SourceFile, string infomsg)
        {
            if (infomsg == null) return;

            if (Logger == null) return;

            Logger.Info(GetFileMsg(SourceFile));

            Logger.Info(infomsg);
        }

    }

    ///// <summary> 系统需要使用到的常量 </summary>
    //public class Constants
    //{
    //    public Constants()
    //    {
    //        if (!System.IO.Directory.Exists(TEMP_PATH))
    //            Directory.CreateDirectory(TEMP_PATH);
    //        if (!System.IO.Directory.Exists(LOG_PATH))
    //            Directory.CreateDirectory(LOG_PATH);
    //    }

    //    /// <summary> 拷贝目录内容 p1 =源目录 p2 =目的目录 </summary>
    //    public static void CopyDirectory(DirectoryInfo source, DirectoryInfo destination, bool copySubDirs)
    //    {
    //        if (!destination.Exists)
    //        {
    //            // Todo ：目标目录若不存在就创建 
    //            destination.Create();
    //        }
    //        FileInfo[] files = source.GetFiles();

    //        foreach (FileInfo file in files)
    //        {
    //            // Todo ：复制目录中所有文件 
    //            file.CopyTo(Path.Combine(destination.FullName, file.Name), true);
    //        }
    //        if (copySubDirs)
    //        {
    //            DirectoryInfo[] dirs = source.GetDirectories();
    //            foreach (DirectoryInfo dir in dirs)
    //            {
    //                string destinationDir = Path.Combine(destination.FullName, dir.Name);

    //                // Todo ：复制子目录 
    //                CopyDirectory(dir, new DirectoryInfo(destinationDir), copySubDirs);
    //            }
    //        }
    //    }

    //    public const double Pi = 3.14159;

    //    public const int SpeedOfLight = 300000; 

    //    public const string TEMP_PATH = System.Environment.GetEnvironmentVariable("TEMP");
    //     <summary>

    //     </summary>
    //    public const string XML_FILE_FILTER = Language.GetString("Xml_files");
    //    /// <summary>
    //    /// Excel文件对话框过滤
    //    /// </summary>
    //    public const string EXCLE_FILE_FILTER = Language.GetString("Excel_files");
    //    /// <summary>
    //    /// Excel文件对话框过滤
    //    /// </summary>
    //    public const string FLAT_FILE_FILTER = Language.GetString("Text_File");
    //    /// <summary>
    //    /// Access数据库的文件对话框过滤
    //    /// </summary>
    //    public const string ACCESS_FILE_FILTER = Language.GetString("Access_files");
    //    /// <summary>
    //    /// 目的表和源表的 映射关系表 表名称
    //    /// </summary>
    //    public const string TABLE_MAPS_NAME = "SYS_TAB_MAPS";
    //    /// <summary>
    //    /// 目的列和源列的 映射关系表 表名称
    //    /// </summary>
    //    public const string COLUMN_MAPS_NAME = "SYS_COLUMN_MAPS";
    //    /// <summary>
    //    /// 基本数据表信息，包括数据库的版本，等一些常用固定的信息，
    //    /// 在系统初始化的时候写入
    //    /// </summary>
    //    public const string SYS_DATA_INFO = "SYS_DATA_INFO";
    //    public static string PORCESS_NAME = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    //public static string EXE_PATH = Assembly.GetEntryAssembly().Location;
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public static string BIN_PATH = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public static string ROOT_PATH = BIN_PATH + @"\..\";
    //    /// <summary>
    //    /// 系统临时文件路径
    //    /// </summary>
    //    public static string SYS_TEMP_PATH = System.Environment.GetEnvironmentVariable("TEMP") + @"\";
    //    /// <summary>
    //    /// 系统临时文件路径
    //    /// </summary>
    //    /// 
    //    public static string SYS_TMP_PATH = System.Environment.GetEnvironmentVariable("TMP") + @"\";
    //    /// <summary>
    //    /// 配置文件路径,此路径只读 ，系统按装后的配置文件路径
    //    /// </summary>
    //    public static string SYS_CONFIG_PATH = ROOT_PATH + @"\Config\";
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public static string SYS_GLOBAL_PATH = SYS_CONFIG_PATH + @"\Global\";

    //    /// <summary>
    //    /// 临时文件路径
    //    /// </summary>
    //    public static string TEMP_PATH = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\opt\Temp\";
    //    /// <summary>
    //    /// 配置文件路径,此路径可写
    //    /// </summary>
    //    public static string CONFIG_PATH = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\opt\Config\";
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public static string CONFIG_PATH1 = AppDomain.CurrentDomain.BaseDirectory;
    //    /// <summary>
    //    /// 日志路径
    //    /// </summary>
    //    public static string LOG_PATH = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\opt\Log\";
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public static string CONFIG_SCHEMA = Constants.SYS_CONFIG_PATH + @"\Global\db\ImportConfigSchema.xml";

    //    /// <summary>
    //    /// WELL_LOG表名
    //    /// </summary>
    //    public const string WELL_LOG_TABLE_NAME = "T_WELL_LOG";//目标表
    //}
    //public class Regedit
    //{
    //    public static void Access_Registry(RegistryKey keyR, String strHome, string key, ref List<string> value)
    //    {
    //        string[] subkeyNames;
    //        string[] subvalueNames;
    //        try
    //        {
    //            RegistryKey aimdir = keyR.OpenSubKey(strHome, true);
    //            if (aimdir != null)
    //            {
    //                subvalueNames = aimdir.GetValueNames();
    //                foreach (string valueName in subvalueNames)
    //                {
    //                    if (valueName.Equals("ORACLE_HOME"))
    //                    {
    //                        Object val = aimdir.GetValue(key);
    //                        value.Add(val.ToString());
    //                        break;
    //                    }
    //                }
    //                subkeyNames = aimdir.GetSubKeyNames();
    //                foreach (string keyName in subkeyNames)
    //                {
    //                    Access_Registry(aimdir, keyName, key, ref  value);
    //                }
    //            }
    //        }
    //        catch (System.Exception ex)
    //        {
    //            Log4netProvider.Logger.Error(ex.Message);
    //        }
    //        //Console.ReadLine();
    //    }
    //}


}