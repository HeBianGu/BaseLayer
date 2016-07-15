using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace OPT.PCOCCenter.Service
{
    /// <summary>
    /// 服务器扩展管理
    /// 比如：传输服务器默认数据库配置信息给客户端
    /// </summary>
    public class ServerExManager
    {
        string LoginID;

        public string AddExtraConfig(string LoginID, string UserName, string configType, string configName, string configFile)
        {
            this.LoginID = LoginID;

            // 检查权限

            // 获取在线用户列表
            string sql = string.Format("insert into ExtraConfig ( UserName, UploadDate, ConfigType, ConfigName, ConfigFile) values ('{0}','{1}','{2}','{3}','{4}')",
                                        UserName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), configType, configName, configFile);

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

        public string DeleteExtraConfig(string LoginID, string configIDs)
        {
            this.LoginID = LoginID;
            // 检查权限

            // 删除用户
            string sql = string.Format("delete from ExtraConfig where ID in ({0})", configIDs);

            int ret = CenterService.DB.ExecuteNonQuery(sql);

            return ret.ToString();
        }

        public System.Data.DataTable GetExtraConfigNameList(string LoginID, string configType)
        {
            this.LoginID = LoginID;

            // 检查权限

            // 获取配置列表
            string sql = string.Format("select ID, ConfigName from ExtraConfig where ConfigType = '{0}'", configType);
            return CenterService.DB.ExecuteDataTable(sql);
        }

        public string GetExtraConfigFile(string LoginID, string configID)
        {
            this.LoginID = LoginID;

            // 检查权限

            // 获取配置信息
            string sql = string.Format("select ConfigFile from ExtraConfig where ID = '{0}'", configID);
            DataTable dt = CenterService.DB.ExecuteDataTable(sql);

            if (dt != null && dt.Rows.Count > 0)
                return dt.Rows[0]["ConfigFile"].ToString();
            else
            {
                // 
                return "";
            }

        }

    }
}
