#region <�� �� ע ��>
/*
 * ========================================================================
 * Copyright(c) �����²���ʯ�ͿƼ����޹�˾, All Rights Reserved.
 * ========================================================================
 *    
 * ���ߣ�[���]   ʱ�䣺2015/11/5 18:26:41  ��������ƣ�DEV-LIHAIJUN
 *
 * �ļ�����LoggerOutput
 *
 * ˵����
 * 
 * 
 * �޸��ߣ�           ʱ�䣺               
 * �޸�˵����
 * ========================================================================
*/
#endregion
using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ObjectBase.Logger
{
    /// <summary> ��־����� </summary>
    class LoggerOutput
    {
        void ReplaceFileTag(string logconfig)
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

        /// <summary> ��־�ļ������� p1 = System.Diagnostics.Process.GetCurrentProcess().ProcessName)</summary>
        public void InitLogger(string name)
        {

            string logconfig = @"log4net.config";
            ReplaceFileTag(logconfig);

            Stopwatch st = new Stopwatch();//ʵ������
            st.Start();//��ʼ��ʱ
            log4net.GlobalContext.Properties["dynamicName"] = name;
            Logger = LogManager.GetLogger(name);
            st.Stop();//��ֹ��ʱ
            if (st.ElapsedMilliseconds > 2000)
            {
                Logger.Info("log4net.config file ERROR!!!");
                System.IO.FileStream fs = new System.IO.FileStream(logconfig, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite);
                StreamReader sr = new StreamReader(fs, System.Text.Encoding.UTF8);
                string str = sr.ReadToEnd();
                str = str.Replace(@"ref=""SQLAppender""", @"ref=""SQLAppenderError""");
                sr.Close();
                fs.Close();
                System.IO.FileStream fs1 = new System.IO.FileStream(logconfig, System.IO.FileMode.Open, System.IO.FileAccess.Write);
                StreamWriter swWriter = new StreamWriter(fs1, System.Text.Encoding.UTF8);
                swWriter.Flush();
                swWriter.Write(str);
                swWriter.Close();
                fs1.Close();
            }
        }
        //public static ILog Logger =
        //    LogManager.Exists(Constants.PORCESS_NAME.IndexOf('.') > -1 ? Constants.PORCESS_NAME.Substring(0, Constants.PORCESS_NAME.IndexOf('.')) : Constants.PORCESS_NAME) != null ?
        //    LogManager.Exists(Constants.PORCESS_NAME.IndexOf('.') > -1 ? Constants.PORCESS_NAME.Substring(0, Constants.PORCESS_NAME.IndexOf('.')) : Constants.PORCESS_NAME) :
        //    LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ILog Logger = null;
        /// <summary> �¼�������Ϣ��ӡ </summary>
        public void EventsMsg(object sender, object e, System.Diagnostics.StackFrame SourceFile)
        {
            string msg = string.Format("[FILE:{0} ],LINE:{1},{2}] sender:{3},e:{4}", SourceFile.GetFileName(), SourceFile.GetFileLineNumber(), SourceFile.GetMethod(), sender.GetType(), e.GetType());
            //Trace.WriteLine(msg);
            if (Logger != null)
            {
                Logger.Info(msg);
            }
        }
        string getFileMsg(System.Diagnostics.StackFrame SourceFile)
        {
            return string.Format("FILE: [{0}] LINE:[{1}] Method:[{2}]", SourceFile.GetFileName(), SourceFile.GetFileLineNumber(), SourceFile.GetMethod());
        }
        void Error(System.Diagnostics.StackFrame SourceFile, Exception ex)
        {
            if (ex == null)
                return;
            if (Logger != null)
            {
                Logger.Info(getFileMsg(SourceFile));
                Logger.Error(ex.Message);
                if (ex.InnerException != null)
                    Logger.Fatal(ex.InnerException);
            }
            //if(IsErrorMsg)
            //    System.Windows.Forms.XtraMessageBox.Show(ex.Message);
        }
        void Info(System.Diagnostics.StackFrame SourceFile, string infomsg)
        {
            if (infomsg == null)
                return;
            if (Logger != null)
            {
                Logger.Info(getFileMsg(SourceFile));
                Logger.Info(infomsg);
            }
            //if(IsErrorMsg)
            //    System.Windows.Forms.XtraMessageBox.Show(ex.Message);
        }
    }


}