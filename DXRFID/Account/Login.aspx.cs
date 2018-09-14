using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXRFID.Account
{
    public partial class Login : System.Web.UI.Page
    {
        DXRFID.Class.MD5_Encoding md5_encoding = new Class.MD5_Encoding();
        DXRFID.Class.DB_Handle db_handle = new Class.DB_Handle();
        DBHelper.DB dh = new DBHelper.DB();
        protected void Page_Load(object sender, EventArgs e)
        {
            dh._conn = ConfigurationManager.ConnectionStrings["RFIDConnectionString"].ConnectionString;
            dh._timeout = 5;
            if (IsPostBack)
            {
                if (Request.Params["opid"].ToString() != "" && Request.Params["pwd"].ToString() != "")
                {
                    string opid = this.opid.Value;
                    md5_encoding.pwd = pwd.Value;
                    string password = md5_encoding.Encoding();

                    string db_pwd = dh.SQLServerGetDataTable(db_handle.Select_User_ConfidentialInformation_ByOpid(opid)).Select().Select(s => s["pwd"].ToString()).FirstOrDefault();
                    if (password == db_pwd)
                    {
                        Session["SetUserID"] = opid;
                        Session["SetUserName"] = dh.SQLServerGetDataTable(db_handle.Select_User_ConfidentialInformation_ByOpid(opid)).Select().Select(s => s["name"].ToString()).FirstOrDefault();
                        Session["SetPremission"] = dh.SQLServerGetDataTable(db_handle.Select_User_ConfidentialInformation_ByOpid(opid)).Select().Select(s => s["premission"].ToString()).FirstOrDefault().ToLower();
                        if (pwd.Value == "123456")
                        {
                            Response.Redirect("./ChangePassword.aspx?session_opid=" + opid);
                        }
                        else
                        {
                            Response.Redirect("../Default.aspx");
                        }
                    }
                    else
                    {
                        error.InnerText = "密码错误！";
                        pwd.Value = "";
                        pwd.Focus();
                        return;
                    }
                }
                else
                {
                    error.InnerText = "缺少登录信息！";
                }
            }
            else
            {

            }
        }
    }
}