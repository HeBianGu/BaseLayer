using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace OPT.PCOCCenter.Service
{
    /// <summary>
    /// 模拟器主机管理
    /// 
    /// </summary>
    public class SimulationHost
    {
        string LoginID;

        public string RegSimulationHost(string LoginID, string UserName, string hostName, string hostIP, string simulationType, string licensePath, string simulationPath, string hostKey)
        {
            this.LoginID = LoginID;
            
            string simulationHostID = string.Empty;

            // 检查权限

            // 检查是否存在
            string sqlCheck = string.Format("select * from SimulationHostInfo where HostIP='{0}' and SimulationType='{1}'", hostIP, simulationType);
            try
            {
                DataTable dtHost = CenterService.DB.ExecuteDataTable(sqlCheck);

                if (dtHost != null && dtHost.Rows.Count > 0)
                {
                    simulationHostID = dtHost.Rows[0]["ID"].ToString();
                }
            }
            catch (System.Exception ex)
            {
            }

            string sql = string.Empty;
            if (simulationHostID == string.Empty)
            {
                System.Guid HostID = System.Guid.NewGuid();
                simulationHostID = HostID.ToString();

                // 注册模拟器主机
                sql = string.Format("insert into SimulationHostInfo (ID, UserName, RegDate, HostName, HostIP, SimulationType, LicensePath, SimulationPath, HostKey) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                                           simulationHostID, UserName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), hostName, hostIP, simulationType, licensePath, simulationPath, hostKey);
            }
            else
            {
                // 更新模拟器主机
                sql = string.Format("update SimulationHostInfo set UserName='{0}', RegDate='{1}', HostName='{2}', HostIP='{3}', LicensePath='{4}', SimulationPath='{5}', HostKey='{6}' where ID = '{7}'",
                                            UserName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), hostName, hostIP, licensePath, simulationPath, hostKey, simulationHostID);
            }

            int ret = 0;

            try
            {
                ret = CenterService.DB.ExecuteNonQuery(sql);
                if (ret > 0)  return "success";
            }
            catch (System.Exception ex)
            {
                return ex.Message; 	
            }
            finally
            {
            }
            return "failed";
        }

        public string DeleteSimulationHost(string LoginID, string simulationHostIDs)
        {
            this.LoginID = LoginID;
            // 检查权限

            // 删除用户
            string sql = string.Format("delete from SimulationHostInfo where ID in ({0})", simulationHostIDs);

            int ret = CenterService.DB.ExecuteNonQuery(sql);

            return ret.ToString();
        }

        public System.Data.DataTable GetSimulationHostList(string LoginID, string simulatonType="")
        {
            this.LoginID = LoginID;

            // 检查权限

            // 获取配置列表
            string sql = string.Empty;
            if (string.IsNullOrEmpty(simulatonType))
                sql = string.Format("select ID, HostIP, HostName, HostAddress, LicensePath, SimulationPath from SimulationHostInfo");
            else
                sql = string.Format("select ID, HostIP, HostName, HostAddress, LicensePath, SimulationPath from SimulationHostInfo where SimulationType='{0}'", simulatonType);

            return CenterService.DB.ExecuteDataTable(sql);
        }

        public string GetSimulationHostLicensePath(string LoginID, string simulationHostID)
        {
            this.LoginID = LoginID;

            // 检查权限

            // 获取配置信息
            string sql = string.Format("select LicensePath from SimulationHostInfo where ID = '{0}'", simulationHostID);
            DataTable dt = CenterService.DB.ExecuteDataTable(sql);

            if (dt != null && dt.Rows.Count > 0)
                return dt.Rows[0]["LicensePath"].ToString();
            else
            {
                // 
                return "";
            }

        }

        public string GetSimulationHostFilePath(string LoginID, string simulationHostID)
        {
            this.LoginID = LoginID;

            // 检查权限

            // 获取配置信息
            string sql = string.Format("select SimulationPath from SimulationHostInfo where ID = '{0}'", simulationHostID);
            DataTable dt = CenterService.DB.ExecuteDataTable(sql);

            if (dt != null && dt.Rows.Count > 0)
                return dt.Rows[0]["SimulationPath"].ToString();
            else
            {
                // 
                return "";
            }

        }

    }
}
