
using System;
namespace OPT.PCOCCenter.Service
{
    class OnlineUsers
    {
        string LoginID;

        public System.Data.DataTable GetOnlineUsers(string LoginID, bool bAllLogin=false)
        {
            this.LoginID = LoginID;            

            // 检查权限

            // 获取在线用户列表
            string sql = string.Empty;

            if (bAllLogin)
                sql = string.Format("select * from Login order by LoginTime desc");
            else
            {
                DateTime oDateTime = DateTime.Now - new TimeSpan(1,0,0,0);
                string dateTime = oDateTime.ToString("yyyy-MM-dd");
                sql = string.Format("select * from Login where Status = '登入' and LoginTime>'{0}' order by LoginTime desc", dateTime);
            }

            System.Data.DataTable dtOnlineUsers = CenterService.DB.ExecuteDataTable(sql);

            return dtOnlineUsers;
        }
    }
}
