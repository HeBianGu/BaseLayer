using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

namespace HebianGu.ComLibModule.Web
{
    public static class JsHelper
    {
        public static void SetJS(string js, Page page, string jsName)
        {
            string str = "<script type=\"text/javascript\" language=\"javascript\">{0}</script>";
            js = string.Format(str, js);
            if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), jsName))
            {
                page.ClientScript.RegisterStartupScript(page.GetType(), jsName, js);
            }
        }
        ///<summary>
        ///弹出JavaScript小窗口
        ///</summary>
        ///<paramname="js">窗口信息</param>
        public static void Alert(string message, Page page)
        {
            string js = @"alert('" + message + "');";
            SetJS(js, page, "alert");
        }
        ///<summary>
        ///弹出消息框并且转向到新的URL
        ///</summary>
        ///<paramname="message">消息内容</param>
        ///<paramname="toURL">连接地址</param>
        public static void AlertAndRedirect(string message, string toURL, Page page)
        {
            #region
            string js = string.Format("alert('{0}');window.location.replace('{1}')", message, toURL);
            SetJS(js, page, "AlertAndRedirect");
            #endregion
        }
        ///<summary>
        ///回到历史页面
        ///</summary>
        ///<paramname="value">-1/1</param>
        public static void GoHistory(int value, Page page)
        {
            string js = string.Format("history.go({0});", value);
            SetJS(js, page, "GoHistory");
        }
        ///<summary>
        ///刷新父窗口
        ///</summary>
        public static void RefreshParent(string url, Page page)
        {
            #region
            string js = @"window.opener.location.href='" + url + "';window.close();";
            SetJS(js, page, "RefreshParent");
            #endregion
        }
        /// <summary>
        /// 刷新打开窗口
        /// </summary>
        /// <param name="page"></param>
        public static void RefreshOpener(Page page)
        {
            #region
            string js = @"opener.location.reload();";
            SetJS(js, page, "RefreshOpener");
            #endregion
        }///<summary>
        ///打开指定大小位置的模式对话框
        ///</summary>
        ///<paramname="webFormUrl">连接地址</param>
        ///<paramname="width">宽</param>
        ///<paramname="height">高</param>
        ///<paramname="top">距离上位置</param>
        ///<paramname="left">距离左位置</param>
        public static void ShowModalDialogWindow(string webFormUrl, int width, int height, int top, int left, Page page)
        {
            #region
            string features = "dialogWidth:" + width.ToString() + "px"
              + ";dialogHeight:" + height.ToString() + "px"
              + ";dialogLeft:" + left.ToString() + "px"
              + ";dialogTop:" + top.ToString() + "px"
              + ";center:yes;help=no;resizable:no;status:no;scroll=yes";
            ShowModalDialogWindow(webFormUrl, features, page);
            #endregion
        }
        public static void ShowModalDialogWindow(string webFormUrl, string features, Page page)
        {
            string js = @"showModalDialog('" + webFormUrl + "','','" + features + "');";
            SetJS(js, page, "ShowModalDialogWindow");
        }
    }
}