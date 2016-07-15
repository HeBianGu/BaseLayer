using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Text.RegularExpressions;

namespace OPT.PCOCCenter.Service
{
    class Login
    {
        string LoginID;
        string UserIP;
        string UserName;
        string Password;
        string AppName;
        string ModuleName;
        string ModuleVersion;
        string KeyInfo;
        string ClientHost;

        public string ErrorInfo{ get; set; }

        public string checkLogin(string loginString)
        {
            string loginRet = string.Empty;
            ParseLoginFromString(loginString);

            // 处理校验逻辑
            loginRet = CheckRoles();

            if(loginRet == "Success")
            {
                // 检查许可数
                loginRet = CheckLicenseCount();
            }
            
            // 记录登录信息
            //if (loginRet.IndexOf("_exist") < 0)
            //{
            //    WriteLoginInfo(loginRet);
            //}
            //else
            {
                loginRet = "Success";
            }

            return loginRet;
        }

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 检查许可数
        /// </summary>
        /// <returns></returns>
        string CheckLicenseCount()
        {
            return CenterService.LicenseManager.CheckModuleLicense(LoginID, ClientHost, AppName, ModuleName, ModuleVersion);
        }

        /// <summary>
        /// 根据用户权限，校验Roles
        /// </summary>
        /// <returns></returns>
        string CheckRoles()
        {
            string sql = string.Format("select * from Roles order by Priority");
            DataTable dtRoles = CenterService.DB.ExecuteDataTable(sql);

            sql = string.Format("select * from Users where UserName = '{0}'", UserName);
            DataTable dtUser = CenterService.DB.ExecuteDataTable(sql);

            if (dtUser != null && dtUser.Rows.Count > 0)
            {
                string userRoles = dtUser.Rows[0]["Roles"].ToString();
                string userGroupID = dtUser.Rows[0]["UserGroupID"].ToString();
                string AllowApps = string.Empty;

                if (AppName != "PCOCCenter")
                {
                    sql = string.Format("select AllowApps from UserGroups where ID = '{0}'", userGroupID);
                    DataTable dtUserGroup = CenterService.DB.ExecuteDataTable(sql);
                    if (dtUserGroup != null)
                    {
                        AllowApps = dtUserGroup.Rows[0]["AllowApps"].ToString();
                        if (AllowApps != "" && AllowApps.IndexOf(AppName) < 0)
                            return "未授权用户运行此模块！";
                    }
                    else
                    {
                        return "未找到用户组信息！";
                    }
                }

                if (dtRoles != null)
                {
                    for (int i = 0; i < dtRoles.Rows.Count; i++)
                    {
                        DataRow dtRow = dtRoles.Rows[i];

                        string RoleItem = dtRow["RoleItem"].ToString();
                        string RoleName = dtRow["Memo"].ToString();
                        string RoleUsed = dtRow["RoleUsed"].ToString();

                        bool retRole = true;
                        if(userRoles.IndexOf(RoleName)>=0)
                            retRole = CheckRole(RoleItem, RoleUsed);

                        if (retRole == false)
                            return dtRow["Memo"].ToString();
                    }

                    return "Success";
                }

                return "未找到权限信息！";
            }

            return "未找到用户信息！";
        }

        // 检查用户名
        bool CheckUserName()
        {
            string sql = string.Format("select * from Users where UserName = '{0}'", UserName);
            DataTable dt = CenterService.DB.ExecuteDataTable(sql);

            if (dt != null && dt.Rows.Count>0)
                return true;
            else
                return false;
        }

        // 检查密码
        bool CheckPassword()
        {
            string sql = string.Format("select * from Users where UserName = '{0}' and Password = '{1}'", UserName, Password);
            DataTable dt = CenterService.DB.ExecuteDataTable(sql);

            if (dt != null && dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        // 检查IP是否符合要求
        bool CheckIPRange()
        {
            string sql = string.Format("select UserGroupID from Users where UserName = '{0}'", UserName);
            DataTable dt = CenterService.DB.ExecuteDataTable(sql);

            string userGroupID = string.Empty;
            if (dt != null && dt.Rows.Count > 0)
                userGroupID = dt.Rows[0]["UserGroupID"].ToString();
            else
            {
                // 用户组没有设置，直接返回失败（校验IP范围，必须设置用户组）
                return false;
            }

            sql = string.Format("select IPRanges from UserGroups where ID = '{0}'", userGroupID);
            dt = CenterService.DB.ExecuteDataTable(sql);

            if (dt != null && dt.Rows.Count > 0)
            {
                bool bRet = false;
                // 校验取到的IP范围
                string ruleIPRanges = dt.Rows[0]["IPRanges"].ToString();
                try
                {
                    bRet = IsAllowIP(ruleIPRanges, UserIP);
                }
                catch (System.Exception ex)
                {
                	ErrorInfo = ex.Message;
                }

                return bRet;
            }
            else
                return false;
        }

        // 检查指定规则
        bool CheckRole(string RoleItem, string RoleUsed)
        {
            if (!string.Equals(RoleUsed, "true", StringComparison.OrdinalIgnoreCase)) return true;

            if (RoleItem == "CheckUserName")
            {
                if ( CheckUserName() == false ) return false;
            }
            if (RoleItem == "CheckPassword")
            {
                if (CheckPassword() == false) return false;
            }
            if (RoleItem == "CheckIPRange")
            {
                if (CheckIPRange() == false) return false;
            }
            return true;
        }

        // 写入登陆信息
        void WriteLoginInfo(string loginRet)
        {
            string Status = "登入";
            if (loginRet != "Success") Status = loginRet;

            string sql = string.Format("insert into Login (ID, UserName, UserIP, LoginTime, AppName, ModuleName, ModuleVersion, Status) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}', '{7}')",
                                        LoginID, UserName, UserIP, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), AppName, ModuleName, ModuleVersion, Status);

            try
            {
                CenterService.DB.ExecuteNonQuery(sql);
            }
            catch (System.Exception ex)
            {
            	
            }
            finally
            {
            }
        }

        // 解析登陆字符串
        bool ParseLoginFromString(string loginString)
        {
            string[] sArray = loginString.Split(';');

            if (sArray.Length >= 9)
            {
                LoginID = sArray[0].ToString();
                UserIP = sArray[1].ToString();
                UserName = sArray[2].ToString();
                Password = sArray[3].ToString();
                AppName = sArray[4].ToString();
                ModuleName = sArray[5].ToString();
                ModuleVersion = sArray[6].ToString();
                KeyInfo = sArray[7].ToString();
                ClientHost = sArray[8].ToString();
            }

            return true;
        }
        
        #region 验证在IP范围内是否允许
        /// <summary>
        /// 判断指定的IP是否在指定的 规则下允许的(三个特殊符号 -?*）
        /// rule[192.*.1.236-239:yes;192.*.1.226:no;218.85.*.*:no]最后一个不要加";"分号
        /// 前面的规则优先级高
        /// 注意，规则中的 * - ? 不能同时存在于同一个段内 如: 192.168.*?.123 会出错
        /// *号在同一段内只能有一个, 如 192.16*.1.*,  192.1**.1.1 是错误的，可以用 ?号代替
        /// </summary>
        /// <param name="rule">(192.*.1.236-239:yes;192.*.1.226:no;218.85.*.*:no) 最后一个规则不要再多加";"分号</param>
        /// <param name="ip">192.168.1.237(不正确的IP会出错)</param>
        /// <returns></returns>
        public bool IsAllowIP(string rule, string ip)
        {
            //IP正则表达式
            string ipRegexString = @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$";
            //如果IP地址是错的，禁止
            if (!Regex.IsMatch(ip, ipRegexString))
            {
                throw new Exception("参数ip错误：错误的IP地址" + ip);
            }
            else
            {
                //分离规则
                string[] ruleArray = rule.Split(new char[] { ';' });
                string[] ipdata = ip.Split(new char[] { '.' });
                bool retValue = false;//默认返回值

                string ruleItem = string.Empty;
                //遍历规则并验证
                foreach (string s in ruleArray)
                {
                    ruleItem = s;

                    bool IsFind = false;

                    if (ruleItem.IndexOf(":") < 0) ruleItem += ":yes";
                    string[] data = ruleItem.Split(new char[] { ':' });
                    //如果没有用:分开
                    if (data.Length != 2) { throw new Exception("请用:分开 如:192.168.1.1:yes"); }

                    string ruleIp = data[0];//得到 192.168.20-60.*:yes 中的 [192.168.20-60.*]部分
                    retValue = data[1].ToString().ToLower() == "yes" ? true : false;


                    string[] ruleIpArray = ruleIp.Split(new char[] { '.' });
                    if (ruleIpArray.Length != 4) { throw new Exception("IP部分错误！"); }

                    #region
                    for (int i = 0; i < 4; i++)
                    {
                        bool AA = ruleIpArray[i].Contains("*");
                        bool BB = ruleIpArray[i].Contains("-");
                        bool CC = ruleIpArray[i].Contains("?");
                        if ((AA && BB) || (AA && CC) || (BB && CC) || (AA && BB && CC))
                        {
                            throw new Exception("这样的格式是错误的,192.168.15-20*,*与-不能包含在同一个部分! ");
                        }
                        else if (!AA && !BB && !CC) //没有包含 * 与 - 及 ?
                        {
                            if (!Regex.IsMatch(ruleIpArray[i], @"^2[0-4]\d|25[0-5]|[01]?\d\d?$"))
                            {
                                throw new Exception("IP段错误应该在1~255之间:" + ruleIpArray[i]);
                            }
                            else
                            {
                                #region 这里判断 111111111111
                                if (ruleIpArray[i] == ipdata[i])
                                {
                                    IsFind = true;
                                }
                                else
                                {
                                    IsFind = false;
                                    break;
                                }
                                #endregion
                            }
                        }
                        else if (AA && !BB && !CC) //包含 [*] 的
                        {
                            if (ruleIpArray[i] != "*")
                            {
                                if (ruleIpArray[i].StartsWith("*") || !ruleIpArray[i].EndsWith("*") || ruleIpArray[i].Contains("**"))
                                {
                                    throw new Exception("IP中的*部分：不能以*开头，不能有两个**，只能以*结尾");
                                }
                            }
                            else
                            {
                                #region 这里判断22222222222222
                                if (ipdata[i].StartsWith(ruleIpArray[i].Replace("*", "")))
                                {
                                    IsFind = true;
                                }
                                else
                                {
                                    IsFind = false;
                                    break;
                                }
                                #endregion
                            }
                        }
                        else if (BB && !AA && !CC) //包含 [-] 的
                        {

                            string[] temp = ruleIpArray[i].Split(new char[] { '-' });
                            if (temp.Length != 2)
                            {
                                throw new Exception("IP段错误, 如:23-50,在1~255之间");
                            }
                            else
                            {
                                if (Convert.ToInt32(temp[0]) < 1 || Convert.ToInt32(temp[1]) > 255)
                                {
                                    throw new Exception("IP段错误, 如:23-50,在1~255之间");
                                }
                                else
                                {
                                    #region 这里判断33333333333333333
                                    string[] Num = ruleIpArray[i].Split(new char[] { '-' });
                                    int p = int.Parse(ipdata[i]);
                                    if (p >= int.Parse(Num[0]) && p <= int.Parse(Num[1]))
                                    {
                                        IsFind = true;
                                    }
                                    else
                                    {
                                        IsFind = false;
                                        break;
                                    }
                                    #endregion
                                }
                            }
                        }
                        else if (CC && !AA & !BB) //包含 [?] 的
                        {
                            //去掉问号后 
                            string temp = ruleIpArray[i].Replace("?", "");
                            Regex re = new Regex(@"^\d\d?$");
                            if (!re.IsMatch(temp) || temp.Length > 2)
                            {
                                throw new Exception("IP段错误:" + ruleIpArray[i]);
                            }
                            else
                            {
                                #region 这里判断4444444444444
                                if (ruleIpArray[i].Length != ipdata[i].Length)
                                {
                                    IsFind = false;
                                    break;
                                }
                                else
                                {
                                    string tempRegstring = "^" + ruleIpArray[i].Replace("?", @"\d") + "$";
                                    Regex tempRe = new Regex(tempRegstring);
                                    if (tempRe.IsMatch(ipdata[i]))
                                    {
                                        IsFind = true;
                                    }
                                    else
                                    {
                                        IsFind = false;
                                        break;
                                    }
                                }
                                #endregion
                            }
                        }
                        else
                        {
                            IsFind = false;
                            break;
                        }


                    }
                    #endregion
                    if (IsFind)
                    {
                        return retValue;//IP规则中 :后面的 yes/no 对应的  true false
                    }
                }
                return false;

            }
        }
        #endregion
    }
}
