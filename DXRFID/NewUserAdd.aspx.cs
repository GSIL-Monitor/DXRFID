using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXRFID
{
    public partial class NewUserAdd : System.Web.UI.Page
    {
        DXRFID.Class.DB_Handle db_handle = new Class.DB_Handle();
        DBHelper.DB dh = new DBHelper.DB();
        Class.SendMail sm = new Class.SendMail();
        protected void Page_Load(object sender, EventArgs e)
        {
            dh._conn = ConfigurationManager.ConnectionStrings["RFIDConnectionString"].ConnectionString;
            dh._timeout = 5;
            
            tijiao_tishi.Style.Add("Color", "red");
            tijiao_tishi.InnerText = "";
            try
            {
                if (Session["SetPremission"].ToString() != "superroot")
                {
                    no_show.Visible = true;
                    show.Visible = false;
                    NO_data.Text = "您不是超级管理员，不可进行人员管理。";
                    return;
                }
            }
            catch
            {
                Response.Redirect("~/Account/Login.aspx");
            }
            
        }

        protected void InsertUser_ServerClick(object sender, EventArgs e)
        {
            if (opid.Value != "" && username.Value != "" && function.Value != "" && email.Value != "")
            {
                string res = dh.SQLServerDBHandle(db_handle.Insert_User(opid.Value, username.Value, function.Value, "123456", email.Value));
                if (res == "OK")
                {
                    if (sm.SendEmail(email.Value, "OFT RFID 设备管理系统--用户开通提醒", @"Dear  "+username.Value+"，<br />"+
                                                                                        "&nbsp; &nbsp; &nbsp; 您的OFT RFID 设备管理系统账号已经开通，登录用户名为："+opid.Value+" ，初始密码为123456，请尽快登入系统修改密码。<br /> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; Form：系统管理员"))
                    {
                        tijiao_tishi.InnerText = "人员新增成功。初始密码为123456";
                        tijiao_tishi.Style.Add("color", "cornflowerblue");
                        Clear();
                    }
                }
                else
                {
                    tijiao_tishi.InnerText = "人员新增失败。ErrorMassage:" + res;
                    return;
                }
                
            }
            else
            {
                tijiao_tishi.InnerText = "有必填项为空。";
                return;
            }
        }
        void Clear()
        {
            opid.Value = "";
            username.Value = "";
            function.Value = "";
            email.Value = "";
        }
    }
}