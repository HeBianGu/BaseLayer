using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using OPT.PCOCCenter.Service.Interface;
using System.Windows.Forms;
using System.ServiceModel.Channels;

namespace OPT.PCOCCenter.Client
{
    public class SimulationAgent
    {
        public static string RemoteAddress { get; set; }
        public static string RemoteFileTransferAddress { get; set; }
        public static string ErrorInfo { get; set; }

        public static void SetMaxItemsInObjectGraph(ChannelFactory<ISimulationAgentService> channelFactory)
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

        private static void LogText(string text)
        {
            ErrorInfo = text;
        }

        #region RequestTask

        public static string RequestTask(string remoteServer, string taskOwnerIP)
        {
            string requestResult = "failed";

            if (remoteServer != null && remoteServer.Length > 0)
            {
                if (remoteServer.IndexOf(":") > 0)
                {
                    RemoteAddress = string.Format("http://{0}/SimulationAgentService", remoteServer);
                    RemoteFileTransferAddress = string.Format("http://{0}/FileTransferService", remoteServer);
                }
                else
                {
                    RemoteAddress = string.Format("http://{0}:22888/SimulationAgentService", remoteServer);
                    RemoteFileTransferAddress = string.Format("http://{0}:22888/FileTransferService", remoteServer);
                }

                try
                {
                    CustomBinding customBinding = new CustomBinding("GZipHttpBinding");

                    using (ChannelFactory<ISimulationAgentService> channelFactory = new ChannelFactory<ISimulationAgentService>(customBinding, RemoteAddress))
                    {
                        SetMaxItemsInObjectGraph(channelFactory);

                        ISimulationAgentService proxy = channelFactory.CreateChannel();

                        LogText("RequestTask");

                        requestResult = proxy.RequestTask(taskOwnerIP);

                        channelFactory.Close();

                    }
                }
                catch (System.Exception ex)
                {
                    LogText("Exception : " + ex.Message);
                    if (ex.InnerException != null) LogText("Inner Exception : " + ex.InnerException.Message);
                }
            }

            return requestResult;
        }


        #endregion


        #region StartTask
        /// <summary>
        /// 开始任务
        /// </summary>
        /// <param name="RemoteAddress">远程服务地址，需要从client中获取</param>
        /// <param name="filePath">本地保存路径，不带文件名</param>
        /// <param name="fileName">需要下载的文件名（远程）</param>
        public static string StartTask(string taskID, string filePath, string dataName, string funcID, string taskType, string taskOwnerIP, string simulatorPath, string simulatorLicensePath, int simulationDays, string remoteDataPath)
        {
            string startTaskRet = "failed";

            try
            {
                CustomBinding customBinding = new CustomBinding("GZipHttpBinding");

                using (ChannelFactory<ISimulationAgentService> channelFactory = new ChannelFactory<ISimulationAgentService>(customBinding, RemoteAddress))
                {
                    SetMaxItemsInObjectGraph(channelFactory);

                    ISimulationAgentService proxy = channelFactory.CreateChannel();

                    LogText("Start Task");

                    SimTaskInfo simTaskInfo = new SimTaskInfo();
                    simTaskInfo.ID = taskID;
                    simTaskInfo.Flag = 0;
                    simTaskInfo.OPath = filePath;
                    simTaskInfo.Name = dataName;
                    simTaskInfo.FuncID = funcID;
                    simTaskInfo.TaskType = taskType;
                    simTaskInfo.OwnerIP = taskOwnerIP;
                    simTaskInfo.SimulatorPath = simulatorPath;
                    simTaskInfo.SimulatorLicensePath = simulatorLicensePath;
                    simTaskInfo.SimulationDays = simulationDays;
                    simTaskInfo.RemoteDataPath = remoteDataPath;
                    proxy.StartTask(simTaskInfo);

                    startTaskRet = simTaskInfo.MessageInfo;

                    channelFactory.Close();

                }
            }
            catch (System.Exception ex)
            {
                LogText("Exception : " + ex.Message);
                if (ex.InnerException != null) LogText("Inner Exception : " + ex.InnerException.Message);
            }

            return startTaskRet;
        }

        #endregion



        #region GetSimTaskInfo
        /// <summary>
        /// 获取指定任务信息
        /// </summary>
        /// <param name="taskID"></param>
        public static SimTaskInfo GetSimTaskInfo(string taskID)
        {
            SimTaskInfo simTaskInfo = new SimTaskInfo();
            simTaskInfo.ID = taskID;
            try
            {
                CustomBinding customBinding = new CustomBinding("GZipHttpBinding");

                using (ChannelFactory<ISimulationAgentService> channelFactory = new ChannelFactory<ISimulationAgentService>(customBinding, RemoteAddress))
                {
                    SetMaxItemsInObjectGraph(channelFactory);

                    ISimulationAgentService proxy = channelFactory.CreateChannel();

                    LogText("GetSimTaskInfo");

                    simTaskInfo = proxy.GetSimTaskInfo(simTaskInfo);


                    channelFactory.Close();

                }
            }
            catch (System.Exception ex)
            {
                LogText("Exception : " + ex.Message);
                if (ex.InnerException != null) LogText("Inner Exception : " + ex.InnerException.Message);
            }

            return simTaskInfo;
        }

        #endregion


        #region RemoveTask
        /// <summary>
        /// 删除指定任务
        /// </summary>
        /// <param name="taskID"></param>
        public static string RemoveTask(string taskID)
        {
            SimTaskInfo simTaskInfo = new SimTaskInfo();
            simTaskInfo.ID = taskID;
            try
            {
                CustomBinding customBinding = new CustomBinding("GZipHttpBinding");

                using (ChannelFactory<ISimulationAgentService> channelFactory = new ChannelFactory<ISimulationAgentService>(customBinding, RemoteAddress))
                {
                    SetMaxItemsInObjectGraph(channelFactory);

                    ISimulationAgentService proxy = channelFactory.CreateChannel();

                    LogText("GetSimTaskInfo");

                    simTaskInfo = proxy.RemoveTask(simTaskInfo);

                    channelFactory.Close();

                }
            }
            catch (System.Exception ex)
            {
                LogText("Exception : " + ex.Message);
                if (ex.InnerException != null) LogText("Inner Exception : " + ex.InnerException.Message);
            }

            return simTaskInfo.MessageInfo;
        }

        #endregion
    }
}
