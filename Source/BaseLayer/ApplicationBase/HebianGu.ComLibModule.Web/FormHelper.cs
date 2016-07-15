using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;

namespace HebianGu.ComLibModule.Web
{
    /// <summary>
    /// 主要于表单操作
    /// </summary>
   public static class FormHelper
    {
        /// <summary>
        /// 批量设置WEB控件的值
        /// </summary>
        /// <param name="nvc"></param>
        public static void SetControl(NameValueCollection nvc,Page page)
        {

            foreach (Control c in page.Form.Controls)
            {
                string value = nvc[c.ID];
                if (value != null && value != "")
                {
                    TextBox te = c as TextBox;
                    CheckBox ce = c as CheckBox;
                    HiddenField id = c as HiddenField;
                    DropDownList dd = c as DropDownList;
                    RadioButtonList rl = c as RadioButtonList;
                    if (te != null)
                    {
                        if (value != null)
                        {
                            te.Text = value;
                        }
                    }
                    else if (ce != null)
                    {
                        if (value != null)
                        {
                            ce.Checked = Convert.ToBoolean(value);
                        }
                    }
                    else if (id != null)
                    {
                        id.Value = value;
                    }
                    else if (dd != null)
                    {
                        dd.SelectedValue = value;
                        if (dd.SelectedValue == string.Empty)
                        {
                            dd.SelectedValue = value.ToUpper();
                            if (dd.SelectedValue == string.Empty)
                            {
                                dd.SelectedValue = value.ToLower();
                            }
                        }

                    }
                    else if (rl != null)
                    {
                        rl.SelectedValue = value.ToString();
                    }
                }
            }
        }
    }
}
