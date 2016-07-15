using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace OPT.PCOCCenter.Utils
{
    /// <summary>
    /// 用户数据库操作类
    /// </summary>
    public class DataUserInfo
    {
        /// <summary>
        /// 数据库操作对象
        /// </summary>
        private DBProvider _dbProvider;

        /// <summary>
        /// 构造方法
        /// </summary>
        public DataUserInfo()
        {
//            _dbProvider = DBProvider.Instance();
        }
        /*
        /// <summary>
        /// 根据用户名和密码查找用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<UserInfoEntity> SelectUserInfo(UserInfoEntity user)
        {
            List<UserInfoEntity> returnList = new List<UserInfoEntity>();
            DataSet ds = new DataSet();
            string dataSentence = "select * from UserInfo where Uname = '"+user.UName+"'"+" and Upassword = '"+ user.UPassWord+"'";
            ds = _dbProvider.RunCommand(dataSentence, "UserInfo");
            for (int i = 0; i < ds.Tables["UserInfo"].Rows.Count; i++)
            {
                UserInfoEntity userInfo = new UserInfoEntity();
                userInfo.UName = ds.Tables["UserInfo"].Rows[i][0].ToString().Trim();
                userInfo.UPassWord = ds.Tables["UserInfo"].Rows[i][1].ToString().Trim();
                userInfo.UAllowInsert = ds.Tables["UserInfo"].Rows[i][2].ToString();
                userInfo.UAllowDelete = ds.Tables["UserInfo"].Rows[i][3].ToString();
                userInfo.UAllowUpdate = ds.Tables["UserInfo"].Rows[i][4].ToString();
                userInfo.UAllowSelect = ds.Tables["UserInfo"].Rows[i][5].ToString();
                userInfo.UAllowOutInData = ds.Tables["UserInfo"].Rows[i][6].ToString();
                userInfo.State = "原始";
                returnList.Add(userInfo);
            }
            return returnList;
        }

        /// <summary>
        /// 按用户名查找
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public List<UserInfoEntity> SelectUserInfo(string userName)
        {
            List<UserInfoEntity> returnList = new List<UserInfoEntity>();
            DataSet ds = new DataSet();
            string dataSentence = "select * from UserInfo where Uname = '" + userName + "'";
            ds = _dbProvider.RunCommand(dataSentence, "UserInfo");
            for (int i = 0; i < ds.Tables["UserInfo"].Rows.Count; i++)
            {
                UserInfoEntity userInfo = new UserInfoEntity();
                userInfo.UName = ds.Tables["UserInfo"].Rows[i][0].ToString().Trim();
                userInfo.UPassWord = ds.Tables["UserInfo"].Rows[i][1].ToString().Trim();
                userInfo.UAllowInsert = ds.Tables["UserInfo"].Rows[i][2].ToString();
                userInfo.UAllowDelete = ds.Tables["UserInfo"].Rows[i][3].ToString();
                userInfo.UAllowUpdate = ds.Tables["UserInfo"].Rows[i][4].ToString();
                userInfo.UAllowSelect = ds.Tables["UserInfo"].Rows[i][5].ToString();
                userInfo.UAllowOutInData = ds.Tables["UserInfo"].Rows[i][6].ToString();
                userInfo.State = "原始";
                returnList.Add(userInfo);
            }
            return returnList;
        }

        /// <summary>
        /// 查找所有用户信息
        /// </summary>
        /// <returns></returns>
        public List<UserInfoEntity> SelectUserInfo()
        {
            List<UserInfoEntity> returnList = new List<UserInfoEntity>();
            DataSet ds = new DataSet();
            string dataSentence = "select * from UserInfo";
            ds = _dbProvider.RunCommand(dataSentence, "UserInfo");
            for (int i = 0; i < ds.Tables["UserInfo"].Rows.Count; i++)
            {
                UserInfoEntity userInfo = new UserInfoEntity();
                userInfo.UName = ds.Tables["UserInfo"].Rows[i][0].ToString().Trim();
                userInfo.UPassWord = ds.Tables["UserInfo"].Rows[i][1].ToString().Trim();
                userInfo.UAllowInsert = ds.Tables["UserInfo"].Rows[i][2].ToString();
                userInfo.UAllowDelete = ds.Tables["UserInfo"].Rows[i][3].ToString();
                userInfo.UAllowUpdate = ds.Tables["UserInfo"].Rows[i][4].ToString();
                userInfo.UAllowSelect = ds.Tables["UserInfo"].Rows[i][5].ToString();
                userInfo.UAllowOutInData = ds.Tables["UserInfo"].Rows[i][6].ToString();
                userInfo.State = "原始";
                returnList.Add(userInfo);
            }
            return returnList;
        }

        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool SaveUserInfo(List<UserInfoEntity> list)
        {
            bool isOk = false;
            int effectRow = 0;
            string dataSentence = "";
            try
            {
                _dbProvider.Open();
                _dbProvider.BeginTrans();
                for (int i = 0; i < list.Count; i++)
                {
                    UserInfoEntity user = list[i];
                    switch (user.State)
                    {
                        case "新增":
                            dataSentence = "insert into UserInfo values('" + user.UName + "','" + user.UPassWord + "','" + user.UAllowInsert + "','" + user.UAllowDelete + "','" + user.UAllowUpdate + "','" + user.UAllowSelect + "','" + user.UAllowOutInData + "')";
                            _dbProvider.RunCommand(dataSentence, out effectRow);
                            break;
                        case "删除":
                            dataSentence = "delete from UserInfo where Uname = '" + user.UName + "'";
                            _dbProvider.RunCommand(dataSentence, out effectRow);
                            break;
                        case "修改":
                            dataSentence = "update UserInfo set Upassword = '" + user.UPassWord + "',Uallowinsert = '" + user.UAllowInsert + "',Uallowdelete = '" + user.UAllowDelete + "',Uallowupdate = '" + user.UAllowUpdate + "',Uallowselect = '" + user.UAllowSelect + "',Uallowoutindata = '" + user.UAllowOutInData + "' where Uname = '" + user.UName + "'";
                            _dbProvider.RunCommand(dataSentence, out effectRow);
                            break;
                        case "原始":
                            break;
                    }
                }
                _dbProvider.CommitTrans();
                isOk = true;
            }
            catch
            {
                isOk = false;
                _dbProvider.RollbackTrans();
                throw;
            }
            return isOk;
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <returns></returns>
        public bool DataClearUserInfo()
        {
            bool isOk = true;
            try
            {
                int effectRow = 0;
                string dataSentence = dataSentence = "delete from UserInfo";
                _dbProvider.RunCommand(dataSentence, out effectRow);
            }
            catch
            {
                isOk = false;
                throw;
            }
            return isOk;
        }*/
    }
}
