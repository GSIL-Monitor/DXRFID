using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Microsoft.Win32;
using DevExpress.Utils.OAuth.Provider;

/// <summary>
///CheckSessionLogin 的摘要说明
/// </summary>
public class CheckSessionLogin
{
    public static void CheckLogin(Page pg)
    {
        string urlpath = System.IO.Path.GetFileName(pg.Request.Path).ToLower().ToString();

        if (pg.Session["SetUserID"] == null || string.IsNullOrEmpty(pg.Session["SetUserID"].ToString()))
        {
            if (urlpath == "default.aspx")
            {
                pg.Response.Redirect("./Account/Login.aspx");
            }
            else
            {
                pg.Response.Redirect("./Account/Login.aspx");
            }
        }
        else
        {
            Label user_lab = (Label)pg.Master.FindControl("LoginName");
            user_lab.Text = pg.Session["SetUserName"].ToString() + "(" + pg.Session["SetUserID"].ToString() + ")";

            Label welcome = (Label)pg.Master.FindControl("welcome");
            if (pg.Session["SetPremission"].ToString() == "normal")
            {
                welcome.Text = "普通用户 " + pg.Session["SetUserName"].ToString() + " 您好,欢迎回来!";
            }
            else if (pg.Session["SetPremission"].ToString() == "root")
            {
                welcome.Text = "管理员用户 " + pg.Session["SetUserName"].ToString() + " 您好,欢迎回来!";
            }
            else if (pg.Session["SetPremission"].ToString() == "superroot")
            {
                welcome.Text = "超级管理员用户 " + pg.Session["SetUserName"].ToString() + " 您好,欢迎回来!";
            }
            
        }
    }

    public static void ChangePwd(Page pg)
    {
        string urlpath = System.IO.Path.GetFileName(pg.Request.Path).ToLower().ToString();
        if (pg.Session["SetUserID"] != null && !string.IsNullOrEmpty(pg.Session["SetUserID"].ToString()))
        {
            if (urlpath == "default.aspx" || urlpath == "about.aspx")
            {
                pg.Response.Redirect("./Account/ChangePassword.aspx?session_opid=" + pg.Session["SetUserID"]);
            }
            else
            {
                pg.Response.Redirect("./Account/ChangePassword.aspx?session_opid=" + pg.Session["SetUserID"]);
            }
        }
    }

    public static void ChangeMsg(Page pg)
    {
        string urlpath = System.IO.Path.GetFileName(pg.Request.Path).ToLower().ToString();
        if (pg.Session["SetUserID"] != null && !string.IsNullOrEmpty(pg.Session["SetUserID"].ToString()))
        {
            if (urlpath == "default.aspx" || urlpath == "about.aspx")
            {
                pg.Response.Redirect("./Account/ChangeMsg.aspx");
            }
            else
            {
                pg.Response.Redirect("./Account/ChangeMsg.aspx");
            }
        }
    }

    public static string SetUserID
    {
        get
        {
            return HttpContext.Current.Session["SetUserID"].ToString();
        }
        set
        {
            HttpContext.Current.Session["SetUserID"] = value;
        }
    }
}