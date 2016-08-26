using HebianGu.ComLibModule.Wcf.Service.Entity;
using HebianGu.ComLibModule.Wcf.Service.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace HebianGu.ComLibModule.Wcf.Service
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“Service1”。
    public class WorkScreamService : IWorkScreamService
    {

        #region - 成员变量 -

        static string _preHMName = "PREHM.exe";

        static string _EsmDaName = "ESMDA.exe";

        static string _EClIPSEName = "EClIPSE.exe";

        static string _eclipseName = "eclipse.exe";

        static string _casePath = string.Empty;

        static string _message = string.Empty;

        static string _errMessage = string.Empty;

        static string _resultDirectory = "WORK";

        static string _parentDirectory = "历史拟合\\";

        FileTransferService _fileTransferService = null;

        public static CaseConfiger tempCase = null;

        public static CaseManager _manager = new CaseManager();

        List<Process> prehms = new List<Process>();

        List<Process> esmdas = new List<Process>();

        
        #endregion 


        /// <summary> 构造函数 </summary>
        public WorkScreamService()
        {
            _fileTransferService = new FileTransferService();
        }

        /// <summary> 析构函数 </summary>
        ~WorkScreamService()
        {
            //StopAllProcess();
        }

        /// <summary> 执行PreHM.exe </summary>
        public bool ExecutivePreHM(CaseConfiger pCase)
        {

            //  获取缓存
            CaseConfiger caseConfiger = _manager.GetCatchCase(pCase);

            //  记录案例
            tempCase = caseConfiger;

            string binFolder = AppDomain.CurrentDomain.BaseDirectory;
            if (!binFolder.EndsWith("\\")) binFolder += "\\";

            if (caseConfiger.PreHMProcess != null || caseConfiger.EsmdaProcess != null)
            {
                _errMessage = "当前案例正在计算中,重启计算请先停止计算！";
                return false;
            }

            //  检查是否存在ECLIPSE.exe
            string eclipse = caseConfiger.ServerPath.EndsWith("\\")
                ? caseConfiger.ServerPath + _EClIPSEName
                : caseConfiger.ServerPath + "\\" + _EClIPSEName;

            //  解密文件
            string passPreHM = binFolder + "PREHM.exe.dat";
            if (File.Exists(passPreHM))
            {
                if (!File.Exists(binFolder + _preHMName))
                {
                    passPreHM.DecryptFileFromDat("OPTPassWord");
                }
            }


            //  不存在
            if (!File.Exists(eclipse))
            {
                File.Copy(binFolder + _EClIPSEName, eclipse, true);
            }

            try
            {
                //  找到exe程序
                Process preHM = new Process();
                preHM.StartInfo.WorkingDirectory = caseConfiger.ServerPath;
                preHM.StartInfo.FileName = binFolder + _preHMName;
                preHM.EnableRaisingEvents = true;
                preHM.Exited += ExecutiveEsmDa;
                preHM.Start();

                //  绑定进程
                caseConfiger.PreHMProcess = preHM;

                //  记录步骤
                caseConfiger.Step = Step.RunPreHM;

                return true;
            }
            catch (Exception ex)
            {
                _errMessage = ex.Message;
                return false;
            }

        }

        /// <summary> 执行Esmda.exe </summary>
        private void ExecutiveEsmDa(object sender, EventArgs e)
        {
            //  获取缓存
            CaseConfiger caseConfiger = _manager.GetCatchCase(tempCase);

            //  解绑步骤一
            caseConfiger.PreHMProcess = null;

            string binFolder = AppDomain.CurrentDomain.BaseDirectory;
            if (!binFolder.EndsWith("\\")) binFolder += "\\";

            //  解密文件
            string passEsmda = binFolder + "ESMDA.exe.dat";
            if (File.Exists(passEsmda))
            {
                if (!File.Exists(binFolder + _EsmDaName))
                {
                    passEsmda.DecryptFileFromDat("OPTPassWord");
                }

            }

            Process EsmDa = new Process();
            EsmDa.StartInfo.WorkingDirectory = caseConfiger.ServerPath;
            EsmDa.StartInfo.FileName = binFolder + _EsmDaName;
            EsmDa.EnableRaisingEvents = true;
            EsmDa.Exited += EsmDa_Exited;
            EsmDa.Start();

            //  绑定进程
            caseConfiger.EsmdaProcess = EsmDa;
            //  记录步骤
            caseConfiger.Step = Step.RunEsmDA;



            //  删除文件
            string passPreHM = binFolder + "PREHM.exe";
            if (File.Exists(passPreHM))
            {
                try
                {
                    //passPreHM.EncryptFileToDat("OPTPassWord");
                    File.Delete(passPreHM);
                }
                catch
                {

                }

            }

        }

        /// <summary> 执行结束 </summary>
        private void EsmDa_Exited(object sender, EventArgs e)
        {
            CaseConfiger caseConfiger = _manager.GetCatchCase(tempCase);

            //  解绑进程
            caseConfiger.EsmdaProcess = null;
            caseConfiger.EclipseProcess = null;

            string binFolder = AppDomain.CurrentDomain.BaseDirectory;

            if (!binFolder.EndsWith("\\")) binFolder += "\\";

            string passEsmda = binFolder + "ESMDA.exe";
            if (File.Exists(passEsmda))
            {
                try
                {
                    //passEsmda.EncryptFileToDat("OPTPassWord");
                    File.Delete(passEsmda);
                }
                catch
                {

                }
            }

            //  记录步骤
            if (caseConfiger.Step != Step.StopOver)
                caseConfiger.Step = Step.Over;

        }

        /// <summary> 获取服务器上所有案例 </summary>
        public List<CaseConfiger> GetCaseDiretroys(string basePath)
        {
            //  获取当前缓存案例
            List<CaseConfiger> caseconfigers = new List<CaseConfiger>();
            string baseDirectory = _fileTransferService.GetUploadFolder(basePath);
            List<string> caseDirs = Directory.GetDirectories(baseDirectory).ToList();
            foreach (string str in caseDirs)
            {
                CaseConfiger caseConfiger = new CaseConfiger();
                caseConfiger.Name = Path.GetFileName(str);
                caseConfiger = _manager.GetCatchCase(caseConfiger);
                caseConfiger.Id = Guid.NewGuid().ToString();
                caseConfiger.ServerPath = str + "\\";
                caseConfiger.Messager = "开始启动中....";
                caseConfiger = _manager.GetCatchCase(caseConfiger);
                caseconfigers.Add(caseConfiger);
            }

            //  不在文件夹中的案例清理
            //List<CaseConfiger> noFindCase = _manager.FindAll(l => !caseDirs.Exists(r => Path.GetFileName(r) == l.Name));
            //foreach (CaseConfiger caseconfiger in noFindCase)
            //{
            //    DeleteCase(caseconfiger);
            //    _manager.RemoveCase(caseconfiger);
            //}

            return caseconfigers;
        }

        /// <summary> 删除案例 </summary>
        public bool DeleteCase(CaseConfiger pCase)
        {
            CaseConfiger caseConfiger = _manager.GetCatchCase(pCase);

            //  结束进程
            if (!StopProcess(caseConfiger))
            {
                return false;
            }
            //  删除文件

            try
            {
                Directory.Delete(caseConfiger.ServerPath, true);
                //  移除
                _manager.RemoveCase(caseConfiger);
                return true;
            }
            catch (Exception ex)
            {
                _errMessage = ex.Message;
                return false;
            }

        }

        /// <summary> 终止当前案例进程 </summary>
        public bool StopProcess(CaseConfiger pCase)
        {
            pCase = _manager.GetCatchCase(pCase);

            try
            {
                if (pCase.PreHMProcess != null && !pCase.PreHMProcess.HasExited)
                {
                    pCase.PreHMProcess.Kill();
                    pCase.PreHMProcess = null;

                }

                if (pCase.EsmdaProcess != null && !pCase.EsmdaProcess.HasExited)
                {
                    pCase.EsmdaProcess.Kill();
                    pCase.EsmdaProcess = null;
                }
                if (pCase.EclipseProcess != null && !pCase.EclipseProcess.HasExited)
                {
                    pCase.EclipseProcess.Kill();
                    pCase.EclipseProcess = null;
                }

                pCase.Step = Step.StopOver;
                return true;
            }
            catch (Exception ex)
            {
                _errMessage = ex.Message;
                pCase.Step = Step.StopOver;
                return false;
            }
        }

        /// <summary> 停止所有进程 </summary>
        public bool StopAllProcess()
        {

            List<Process> pros = GetAllCaseProcess();

            try
            {
                pros.ForEach(l =>
                    {
                        if (!l.HasExited)
                        {
                            l.Kill();
                            l = null;
                        }

                    });
                return true;
            }
            catch (Exception ex)
            {
                _errMessage = ex.Message;
                return false;
            }


        }

        /// <summary> 获取当前案例所有进程 </summary>
        private List<Process> GetAllCaseProcess()
        {

            List<Process> pros = new List<Process>();
            //  preHM
            var preHMs = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(_preHMName)).ToList();
            if (preHMs != null)
            {
                pros.AddRange(preHMs);
            }

            //  ESMDA
            var esmDas = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(_EsmDaName)).ToList();
            if (esmDas != null)
            {
                pros.AddRange(esmDas);
            }

            //  ECLIPSE
            var ECLIPSEs = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(_EClIPSEName)).ToList();
            if (ECLIPSEs != null)
            {
                pros.AddRange(ECLIPSEs);
            }


            //  eclipse
            //var eclipses = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(_eclipseName)).ToList();
            //if (ECLIPSEs != null)
            //{
            //    pros.AddRange(ECLIPSEs);
            //}

            return pros;

        }

        /// <summary> 获取当前案例执行步骤 </summary>
        public bool GetStepOfCase(CaseConfiger pCase, ref Step step)
        {
            try
            {
                step = _manager.GetCatchCase(pCase).Step;
                return true;
            }
            catch (Exception ex)
            {
                _errMessage = ex.Message;
                return false;
            }
        }

        /// <summary> 获取日志信息 </summary>
        public string GetLogMessage()
        {
            return _errMessage;
        }

        /// <summary> 增加案例(增加到内存) </summary>
        public bool AddCase(CaseConfiger pCase)
        {
            try
            {
                string baseDirectory = _fileTransferService.GetUploadFolder(_parentDirectory);
                //  增加服务器目录
                if (string.IsNullOrEmpty(pCase.ServerPath))
                {
                    pCase.ServerPath = baseDirectory + pCase.Name + "\\";
                }
                _manager.AddCase(pCase);
                return true;
            }
            catch (Exception ex)
            {
                _errMessage = ex.Message;
                return false;
            }

        }

        /// <summary> 通过名称获取案例 </summary>
        public CaseConfiger GetCaseByName(string pName)
        {
            return _manager.Find(l => l.Name == pName.Trim());
        }


    }
}
