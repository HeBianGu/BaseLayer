using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using OPT.PCOCCenter.Service.Interface;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using OPT.PCOCCenter.Service;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Threading;
using System.ServiceModel.Channels;

namespace OPT.PCOCCenter.Client
{
    public class Client
    {
        [DllImport("KeyObtain.dll")]
        public static extern IntPtr GetKeyString([In]char[] szDesKey);

        public static string RemoteAddress { get; set; }
        public static string RemoteFileTransferAddress { get; set; }
        public static System.Guid LoginID = System.Guid.NewGuid();
        public static string ErrorInfo { get; set; }
        public static CustomBinding customBinding = null;
        public static ExtraConfig ExtraConfig = null;
        //public static ProgressForm progressForm = null;
        //public static BackgroundWorker worker = null;
        //public static List<LicenseInfo> licenseInfos = null;

        public static string gServer { get; set; }
        public static string gUserName { get; set; }
        public static string gPassword { get; set; }

        public static string gAppName { get; set; }
        public static string gModuleName { get; set; }
        public static string gModuleVersion { get; set; }
        public static string gKeyInfo { get; set; }
        public static string gClientHost { get; set; }
        public static System.Timers.Timer tVerifyServer;
        public static string gVerifyModuleNames { get; set; }
        public static ClientEvent clientEvent = new ClientEvent();

        public static void SetMaxItemsInObjectGraph(ChannelFactory<ICenterService> channelFactory)
        {
            foreach (System.ServiceModel.Description.OperationDescription op in channelFactory.Endpoint.Contract.Operations)
            {
                System.ServiceModel.Description.DataContractSerializerOperationBehavior dataContractBehavior =
                            op.Behaviors.Find<System.ServiceModel.Description.DataContractSerializerOperationBehavior>()
                            as System.ServiceModel.Description.DataContractSerializerOperationBehavior;
                if (dataContractBehavior != null)
                {
                    dataContractBehavior.MaxItemsInObjectGraph = int.MaxValue;
                }
            }
        }

        public static int Login(string AppName, string ModuleName, string ModuleVersion, string server, string username, string password, string KeyInfo)
        {
            int nLogin = 0;

            if (server != null && server.Length > 0)
            {
                if (server.IndexOf(":") > 0)
                {
                    RemoteAddress = string.Format("http://{0}/CenterService", server);
                    RemoteFileTransferAddress = string.Format("http://{0}/FileTransferService", server);
                }
                else
                {
                    RemoteAddress = string.Format("http://{0}:22888/CenterService", server);
                    RemoteFileTransferAddress = string.Format("http://{0}:22888/FileTransferService", server);
                }

                try
                {
                    customBinding = new CustomBinding("GZipHttpBinding");

                    using (ChannelFactory<ICenterService> channelFactory = new ChannelFactory<ICenterService>(customBinding, RemoteAddress))
                    {
                        SetMaxItemsInObjectGraph(channelFactory);

                        ICenterService proxy = channelFactory.CreateChannel();

                        string strHostIP = GetLocalIP();
                        string strClientHost = strHostIP;
                        string strClientDesKey = "PCOCSimulator";
                        //char[] szDesKey = strClientDesKey.ToCharArray();
                        //IntPtr intPtrClientHost = GetKeyString(szDesKey);
                        //if (intPtrClientHost != null)
                        //{
                        //    strClientHost = Marshal.PtrToStringAnsi(intPtrClientHost);
                        //}
                        gAppName = AppName;
                        gModuleName = ModuleName;                        
                        gModuleVersion = ModuleVersion;
                        gKeyInfo = KeyInfo;
                        gClientHost = strClientHost;
                        gServer = server;
                        gUserName = username;
                        gPassword = password;

                        string loginString = string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};",
                            LoginID, GetLocalIP(), username, password, AppName, ModuleName, ModuleVersion, KeyInfo, strClientHost);

                        string loginRet = proxy.Login(loginString);

                        if (loginRet == "Success")
                        {
                            nLogin = 1;
                            ExtraConfig = new ExtraConfig();

                            if (tVerifyServer == null)
                            {
                                //实例化Timer类，设置间隔时间为3*60 000毫秒(3分钟)；   
                                tVerifyServer = new System.Timers.Timer(180000);
                                //到达时间的时候执行事件；   
                                tVerifyServer.Elapsed += new System.Timers.ElapsedEventHandler(handlerVerifyServer);
                                //设置是执行一次（false）还是一直执行(true)；
                                tVerifyServer.AutoReset = true;
                                //是否执行System.Timers.Timer.Elapsed事件；   
                                tVerifyServer.Enabled = true;
                            }

                            if (AppName != ModuleName)
                            {
                                gVerifyModuleNames += ModuleName;
                                gVerifyModuleNames += ", ";
                            }                            
                        }
                        else
                        {
                            ErrorInfo = string.Format("登陆失败！\n\n错误信息：{0} \n请确认后再重试。", loginRet);
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    ErrorInfo = string.Format("登陆失败！\n\n错误信息：{0}\n请确认后再重试。", ex.Message);
                }
            }
            
            if (clientEvent != null)
            {
                if (nLogin == 1)
                {
                    clientEvent.doEvent(ClientEvent.LicenseSucceed);
                }
                else
                {
                    clientEvent.doEvent(ClientEvent.LicenseFailed);
                }
            }

            return nLogin;
        }

        // 定时连接服务器
        public static void handlerVerifyServer(object source, System.Timers.ElapsedEventArgs e)
        {
            Verify();
        }
        
        // 功能模块登陆服务器
        public static int Login(string ModuleName, string ModuleVersion)
        {
            int nLogin = Login("", ModuleName, ModuleVersion, gServer, gUserName, gPassword, gKeyInfo);

            if (nLogin != 1 && ErrorInfo != null)
                MessageBox.Show(OPT.PCOCCenter.Client.Client.ErrorInfo, "提示");

           return nLogin;
        }

        // 登陆服务器
        public static int Login(string AppName, string ModuleName, string ModuleVersion, string KeyInfo)
        {
            int nLogin = 0;
            LoginForm loginForm = new LoginForm();

            if (loginForm.ShowDialog() == DialogResult.OK && loginForm.Server != null && loginForm.Server.Length > 0)
            {
                nLogin = Login(AppName, ModuleName, ModuleVersion, loginForm.Server, loginForm.UserName, loginForm.Password, KeyInfo);
            }

            return nLogin;
        }

        // 退出服务器
        public static int Logout()
        {
            int nLogout = 0;

            try
            {
                using (ChannelFactory<ICenterService> channelFactory = new ChannelFactory<ICenterService>(customBinding, RemoteAddress))
                {
                    SetMaxItemsInObjectGraph(channelFactory);

                    ICenterService proxy = channelFactory.CreateChannel();

                    string logoutString = string.Format("{0};{1};{2};{3};{4};{5};",
                        LoginID, gAppName, gModuleName, gModuleVersion, gKeyInfo, gClientHost);

                    nLogout = proxy.Logout(logoutString);
                }
            }
            catch (System.Exception ex)
            {
            }

            return nLogout;
        }
        
        // 不定时验证服务器
        public static int Verify()
        {
            int nVerify = 0;

            if (gVerifyModuleNames == null || gVerifyModuleNames == "") return nVerify;

            try
            {
                using (ChannelFactory<ICenterService> channelFactory = new ChannelFactory<ICenterService>(customBinding, RemoteAddress))
                {
                    SetMaxItemsInObjectGraph(channelFactory);

                    ICenterService proxy = channelFactory.CreateChannel();

                    string verifyString = string.Format("{0};{1};{2};{3};{4};{5};{6};{7};",
                        LoginID, GetLocalIP(), gUserName, gAppName, gVerifyModuleNames, gModuleVersion, gKeyInfo, gClientHost);

                    string verifyRet = proxy.Verify(verifyString);

                    if (verifyRet == "Success")
                    {
                        nVerify = 1;
                    }
                    else
                    {
                        ErrorInfo = string.Format("服务器连接验证失败！\n\n错误信息：{0} \n请确认后再重试。", verifyRet);
                    }
                }
            }
            catch (System.Exception ex)
            {
                ErrorInfo = string.Format("服务器连接验证失败！\n\n错误信息：{0}\n请确认后再重试。", ex.Message);
            }


            if (clientEvent != null)
            {
                if (nVerify == 1)
                {
                    clientEvent.doEvent(ClientEvent.LicenseSucceed);
                }
                else
                {
                    clientEvent.doEvent(ClientEvent.LicenseFailed);
                }
            }

            return nVerify;
        }

        // 获取授权服务器配置信息
        public static string GetServerInfo()
        {
            try
            {
                using (ChannelFactory<ICenterService> channelFactory = new ChannelFactory<ICenterService>(customBinding, RemoteAddress))
                {
                    SetMaxItemsInObjectGraph(channelFactory);

                    ICenterService proxy = channelFactory.CreateChannel();
                    return proxy.GetServerInfo(LoginID.ToString());
                }
            }
            catch (System.Exception ex)
            {
            }

            return "";
        }

        public static System.Data.DataTable GetOnlineUsers(bool bAllLogin = false)
        {
            try
            {
                using (ChannelFactory<ICenterService> channelFactory = new ChannelFactory<ICenterService>(customBinding, RemoteAddress))
                {
                    SetMaxItemsInObjectGraph(channelFactory);

                    ICenterService proxy = channelFactory.CreateChannel();
                    System.Data.DataTable onlineUsers = proxy.GetOnlineUsers(LoginID.ToString(), bAllLogin);
                    return onlineUsers;
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public static void AddLicenseToConfig(string licenseFile)
        {
            try
            {
                using (ChannelFactory<ICenterService> channelFactory = new ChannelFactory<ICenterService>(customBinding, RemoteAddress))
                {
                    SetMaxItemsInObjectGraph(channelFactory);

                    ICenterService proxy = channelFactory.CreateChannel();
                    proxy.AddLicenseToConfig(LoginID.ToString(), licenseFile);
                }
            }
            catch (System.Exception ex)
            {
            }
        }

        public static List<LicenseInfo> ReloadLicenseFiles(Form parentForm)
        {
            OPT.PCOCCenter.Client.Client.ErrorInfo = "加载许可信息错误";
            try
            {
                using (ChannelFactory<ICenterService> channelFactory = new ChannelFactory<ICenterService>(customBinding, RemoteAddress))
                {
                    SetMaxItemsInObjectGraph(channelFactory);

                    ICenterService proxy = channelFactory.CreateChannel();
                    //worker.ReportProgress(20);
                    //Thread.Sleep(0);
                    List<LicenseInfo> licenseInfos = proxy.ReloadLicenseFiles(LoginID.ToString());
                    //worker.ReportProgress(100);
                    //Thread.Sleep(0);
                    return licenseInfos;
                }

                /*
                if (progressForm == null || progressForm.IsDisposed)
                {
                    progressForm = new ProgressForm();
                }
                progressForm.progressBar.Value = 0;
                progressForm.Owner = parentForm;
                progressForm.Show();
                licenseInfos = null;

                // Prepare the background worker   
                //for asynchronous prime number calculation  
                //准备进度条的记数  
                worker = new BackgroundWorker();
                // Specify that the background   
                //worker provides progress notifications    
                //指定提供进度通知  
                worker.WorkerReportsProgress = true;
                // Specify that the background worker supports cancellation  
                //提供中断功能  
                worker.WorkerSupportsCancellation = true;
                // The DoWork event handler is the main   
                //work function of the background thread  
                //线程的主要功能是处理事件  
                //开启线程执行工作  ，C#进度条实现之异步实例
                worker.DoWork += new DoWorkEventHandler(worker_DoReloadLicenseFiles);
                // Specify the function to use to handle progress  
                //指定使用的功能来处理进度  
                worker.ProgressChanged += new ProgressChangedEventHandler(progressForm.OnProgressChanged);
                // Specify the function to run when the   
                //background worker finishes  
                // There are three conditions possible   
                //that should be handled in this function:  
                // 1. The work completed successfully  
                // 2. The work aborted with errors  
                // 3. The user cancelled the process  
                //进度条结束完成工作  
                //1.工作完成  
                //2.工作错误异常  
                //3.取消工作  
//                worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(parentForm.ImportLicense_OnCompleted);
                worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(progressForm.OnProcessCompleted);

                //If your background operation requires a parameter,   
                //call System.ComponentModel.BackgroundWorker.RunWorkerAsync   
                //with your parameter. Inside   
                //the System.ComponentModel.BackgroundWorker.DoWork   
                //event handler, you can extract the parameter from the   
                //System.ComponentModel.DoWorkEventArgs.Argument property.  
                //如果进度条需要参数  
                //调用System.ComponentModel.BackgroundWorker.RunWorkerAsync  
                //传入你的参数至System.ComponentModel.BackgroundWorker.DoWork   
                //提取参数  
                //System.ComponentModel.DoWorkEventArgs.Argument   
                worker.RunWorkerAsync(null);

                while (licenseInfos == null)
                {
                    Thread.Sleep(1000);
                }
                */
            }
            catch (System.Exception ex)
            {
            }

            return null;
        }

        //单线程执行工作  
        private static void worker_DoReloadLicenseFiles(object sender, DoWorkEventArgs e)
        {
            //using (ChannelFactory<ICenterService> channelFactory = new ChannelFactory<ICenterService>(wsHttpBinding, RemoteAddress))
            //{
            //    SetMaxItemsInObjectGraph(channelFactory);

            //    ICenterService proxy = channelFactory.CreateChannel();
            //    worker.ReportProgress(20);
            //    Thread.Sleep(0);
            //    licenseInfos = proxy.ReloadLicenseFiles(LoginID.ToString());
            //    worker.ReportProgress(100);
            //    Thread.Sleep(0);
            //}
        }

        public static List<LicenseInfo> GetLicenseInfos()
        {
            try
            {
                using (ChannelFactory<ICenterService> channelFactory = new ChannelFactory<ICenterService>(customBinding, RemoteAddress))
                {
                    SetMaxItemsInObjectGraph(channelFactory);

                    ICenterService proxy = channelFactory.CreateChannel();
                    List<LicenseInfo> licenseInfos = proxy.GetLicenseInfos(LoginID.ToString());
                    return licenseInfos;
                }
            }
            catch (System.Exception ex)
            {
            }

            return null;
        }

        public static List<string> GetLicenseApps()
        {
            try
            {
                using (ChannelFactory<ICenterService> channelFactory = new ChannelFactory<ICenterService>(customBinding, RemoteAddress))
                {
                    SetMaxItemsInObjectGraph(channelFactory);

                    ICenterService proxy = channelFactory.CreateChannel();
                    List<string> licenseApps = proxy.GetLicenseApps(LoginID.ToString());
                    return licenseApps;
                }
            }
            catch (System.Exception ex)
            {
            }

            return null;
        }

        public static System.Data.DataTable GetUsers()
        {
            try
            {
                using (ChannelFactory<ICenterService> channelFactory = new ChannelFactory<ICenterService>(customBinding, RemoteAddress))
                {
                    SetMaxItemsInObjectGraph(channelFactory);

                    ICenterService proxy = channelFactory.CreateChannel();
                    System.Data.DataTable Users = proxy.GetUsers(LoginID.ToString());
                    return Users;
                }
            }
            catch (System.Exception ex)
            {
            }

            return null;
        }

        public static System.Data.DataTable GetUserGroups()
        {
            try
            {
                using (ChannelFactory<ICenterService> channelFactory = new ChannelFactory<ICenterService>(customBinding, RemoteAddress))
                {
                    SetMaxItemsInObjectGraph(channelFactory);

                    ICenterService proxy = channelFactory.CreateChannel();
                    System.Data.DataTable UserGroups = proxy.GetUserGroups(LoginID.ToString());
                    return UserGroups;
                }
            }
            catch (System.Exception ex)
            {
            }

            return null;
        }

        public static void DeleteUserGroup(string userGroupID)
        {
            try
            {
                using (ChannelFactory<ICenterService> channelFactory = new ChannelFactory<ICenterService>(customBinding, RemoteAddress))
                {
                    SetMaxItemsInObjectGraph(channelFactory);

                    ICenterService proxy = channelFactory.CreateChannel();
                    proxy.DeleteUserGroup(userGroupID);
                }
            }
            catch (System.Exception ex)
            {
            }
        }

        public static void AddUserGroup(string groupName, string ipRanges, string allowApps, string roles)
        {
            try
            {
                using (ChannelFactory<ICenterService> channelFactory = new ChannelFactory<ICenterService>(customBinding, RemoteAddress))
                {
                    SetMaxItemsInObjectGraph(channelFactory);

                    ICenterService proxy = channelFactory.CreateChannel();
                    proxy.AddUserGroup(groupName, ipRanges, allowApps, roles);
                }
            }
            catch (System.Exception ex)
            {
            }
        }

        public static void UpdateUserGroup(string userGroupID, string groupName, string ipRanges, string allowApps, string roles)
        {
            try
            {
                using (ChannelFactory<ICenterService> channelFactory = new ChannelFactory<ICenterService>(customBinding, RemoteAddress))
                {
                    SetMaxItemsInObjectGraph(channelFactory);

                    ICenterService proxy = channelFactory.CreateChannel();
                    proxy.UpdateUserGroup(userGroupID, groupName, ipRanges, allowApps, roles);
                }
            }
            catch (System.Exception ex)
            {
            }
        }

        public static System.Data.DataTable GetRoles()
        {
            try
            {
                using (ChannelFactory<ICenterService> channelFactory = new ChannelFactory<ICenterService>(customBinding, RemoteAddress))
                {
                    SetMaxItemsInObjectGraph(channelFactory);

                    ICenterService proxy = channelFactory.CreateChannel();
                    System.Data.DataTable UserRoles = proxy.GetRoles(LoginID.ToString());
                    return UserRoles;
                }
            }
            catch (System.Exception ex)
            {
            }

            return null;
        }

        public static int AddUser(string userInfo)
        {
            int nRet = 0;

            try
            {
                using (ChannelFactory<ICenterService> channelFactory = new ChannelFactory<ICenterService>(customBinding, RemoteAddress))
                {
                    SetMaxItemsInObjectGraph(channelFactory);

                    ICenterService proxy = channelFactory.CreateChannel();

                    string addUserRet = proxy.AddUser(LoginID.ToString(), userInfo);

                    if (addUserRet != "0")
                        nRet = 1;
                }
            }
            catch (System.Exception ex)
            {
            }

            return nRet;
        }

        public static int EditUser(string userID, string userInfo)
        {
            int nRet = 0;

            try
            {
                using (ChannelFactory<ICenterService> channelFactory = new ChannelFactory<ICenterService>(customBinding, RemoteAddress))
                {
                    SetMaxItemsInObjectGraph(channelFactory);

                    ICenterService proxy = channelFactory.CreateChannel();

                    string editUserRet = proxy.EditUser(LoginID.ToString(), userID, userInfo);

                    if (editUserRet != "0")
                        nRet = 1;
                }
            }
            catch (System.Exception ex)
            {
            }

            return nRet;
        }

        public static int DeleteUser(string userIDs)
        {
            int nRet = 0;

            try
            {
                using (ChannelFactory<ICenterService> channelFactory = new ChannelFactory<ICenterService>(customBinding, RemoteAddress))
                {
                    SetMaxItemsInObjectGraph(channelFactory);

                    ICenterService proxy = channelFactory.CreateChannel();

                    string deleteUserRet = proxy.DeleteUser(LoginID.ToString(), userIDs);

                    if (deleteUserRet != "0")
                        nRet = 1;
                }
            }
            catch (System.Exception ex)
            {
            }

            return nRet;
        }

        //获取本机IP
        public static string GetLocalIP()
        {
            IPAddress[] arrIPAddresses = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress ip in arrIPAddresses)
            {
                if (ip.AddressFamily.Equals(AddressFamily.InterNetwork))
                {
                    return ip.ToString();
                }
            }
            return "127.0.0.1";
        }

        //////////////////////////////////////////////////////////////////////////
        // ExtraConfig
        //////////////////////////////////////////////////////////////////////////
        // 上传配置文件
        public static int AddExtraConfig(string configType, string configName, string configFile)
        {
            return ExtraConfig.AddExtraConfig(configType, configName, configFile);
        }

        // 删除指定配置文件
        public static int DeleteExtraConfig(string configIDs)
        {
            return ExtraConfig.DeleteExtraConfig(configIDs);

        }

        // 获取配置名称列表
        public static System.Data.DataTable GetExtraConfigNameList(string configType)
        {
            return ExtraConfig.GetExtraConfigNameList(configType);
        }

        // 获取配置文件
        public static string GetExtraConfigFile(string configID)
        {
            return ExtraConfig.GetExtraConfigFile(configID);
        }

        //////////////////////////////////////////////////////////////////////////
        // 模拟器主机管理
        //////////////////////////////////////////////////////////////////////////

        // 注册Simulation主机
        public static int RegSimulationHost(string hostName, string hostIP, string simulationType, string licensePath, string simulationPath, string hostKey)
        {
            return SimulationHost.RegSimulationHost(hostName, hostIP, simulationType, licensePath, simulationPath, hostKey);
        }

        // 获取Simulation主机列表
        public static System.Data.DataTable GetSimulationHostList(string simulationType="")
        {
            return SimulationHost.GetSimulationHostList(simulationType);
        }

        public static string GetSimulationHostFilePath(string simulationHostID)
        {
            return SimulationHost.GetSimulationHostFilePath(simulationHostID);
        }

        public static string GetSimulationHostLicensePath(string simulationHostID)
        {
            return SimulationHost.GetSimulationHostLicensePath(simulationHostID);
        }        
    }
}
