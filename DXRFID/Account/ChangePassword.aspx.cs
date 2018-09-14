using DXRFID.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXRFID.Account
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        DXRFID.Class.DB_Handle db_handle = new Class.DB_Handle();
        DBHelper.DB dh = new DBHelper.DB();
        DXRFID.Class.SendMail sm = new Class.SendMail();
        DXRFID.Class.MD5_Encoding md5_encoding = new MD5_Encoding();
        protected void Page_Load(object sender, EventArgs e)
        {
            dh._conn = System.Configuration.ConfigurationManager.ConnectionStrings["RFIDConnectionString"].ConnectionString;
            dh._timeout = 5;

            if (Request.Params.AllKeys.Contains("session_opid") && Request.Params["session_opid"].ToString() != "")
            {
                opid.Value = Request.Params["session_opid"].ToString();
            }
            else
            {
                forget.Visible = false;
                change.Visible = false;
                opid.Value = "未登录，不可修改密码";
                opid.Attributes.Add("disabled", "disabled");
                return;
            }
        }

        protected void Update_ServerClick(object sender, EventArgs e)
        {
            if (new_pwd.Value != "" && comfirm_pwd.Value != "")
            {
                int level_str = 0;
                if (Regex.IsMatch(new_pwd.Value, @"^.*([\W_])+.*$"))
                {
                    level_str++;
                }
                if (Regex.IsMatch(new_pwd.Value, @"^.*([0-9])+.*$"))
                {
                    level_str++;
                }
                if (Regex.IsMatch(new_pwd.Value, @"^.*([A-Z])+.*$"))
                {
                    level_str++;
                }
                if (Regex.IsMatch(new_pwd.Value, @"^.*([a-z])+.*$"))
                {
                    level_str++;
                }
                if (level_str < 3)
                {
                    error.InnerText = "密碼至少為大寫,小寫,特殊字符,數字里三個組合且長度至少為6位...";
                    return;
                }

                md5_encoding.pwd = new_pwd.Value;
                if (new_pwd.Value == comfirm_pwd.Value && md5_encoding.Encoding() != dh.SQLServerGetDataTable(db_handle.Select_User_ConfidentialInformation_ByOpid(opid.Value)).Select().Select(s => s["pwd"].ToString()).FirstOrDefault())
                {
                    if (dh.SQLServerDBHandle(db_handle.Update_Pwd(opid.Value, new_pwd.Value)) == "OK")
                    {
                        Response.Redirect("Login.aspx");
                    }
                }
                else if (new_pwd.Value != comfirm_pwd.Value)
                {
                    error.InnerText = "两次密码输入不一致，请重新输入。";
                    comfirm_pwd.Value = "";
                    return;
                }
                else if (md5_encoding.Encoding() == dh.SQLServerGetDataTable(db_handle.Select_User_ConfidentialInformation_ByOpid(opid.Value)).Select().Select(s => s["pwd"].ToString()).FirstOrDefault())
                {
                    error.InnerText = "密码与之前密码一致，请重新输入。";
                    new_pwd.Value = "";
                    comfirm_pwd.Value = "";
                    return;
                }
            }
            else
            {
                error.InnerText = "新密码及确认密码不能为空。";
                return;
            }
        }

        protected void Check_ServerClick(object sender, EventArgs e)
        {
            md5_encoding.pwd = old_pwd.Value;
            if (md5_encoding.Encoding() != dh.SQLServerGetDataTable(db_handle.Select_User_ConfidentialInformation_ByOpid(opid.Value)).Select().Select(s => s["pwd"].ToString()).FirstOrDefault())
            {
                check_error.InnerText = "旧密码错误，请重新输入。";
                old_pwd.Value = "";
                return;
            }

            else if (dh.SQLServerGetDataTable(db_handle.Select_User_ConfidentialInformation_ByOpid(opid.Value)).Rows.Count <= 0)
            {
                check_error.InnerText = "未查询到输入的工号信息,请重新输入。";
                opid.Value = "";
                old_pwd.Value = "";
                return;
            }
            else
            {
                forget.Visible = false;
                change.Visible = true;
            }
        }
    }
}