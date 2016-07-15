using OPT.PCOCCenter.Service.Interface;
using System.ServiceModel;

namespace OPT.PCOCCenter.Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class SimulationAgentService : ISimulationAgentService
    {
        // 模拟任务管理器
        public static SimulationTaskManager simulationTaskManager = new SimulationTaskManager();
        
        /// <summary>
        /// 请求运行任务
        /// </summary>
        /// <param name="taskOwnerIP">任务IP，检查是否允许</param>
        /// <returns></returns>
        public string RequestTask(string taskOwnerIP)
        {
            // 检查是否允许运行此IP的任务
            // todo

            return "Success";
        }

        /// <summary>
        /// 开始任务（添加到任务管理器列表）
        /// </summary>
        /// <param name="simTaskInfo"></param>
        public void StartTask(SimTaskInfo simTaskInfo)
        {
            simulationTaskManager.AddTask(simTaskInfo);
        }

        /// <summary>
        /// 获取任务
        /// </summary>
        /// <param name="simTaskID"></param>
        public SimTaskInfo GetSimTaskInfo(SimTaskInfo request)
        {
            return simulationTaskManager.GetSimTaskInfo(request);
        }

        /// <summary>
        /// 移除任务
        /// </summary>
        /// <param name="simTaskID"></param>
        public SimTaskInfo RemoveTask(SimTaskInfo request)
        {
            return simulationTaskManager.RemoveTask(request);
        }
    }
}
