using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace DXRFID
{
    public partial class ForgetPwd : System.Web.UI.Page
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

        protected void ChangePWD_ServerClick(object sender, EventArgs e)
        {
            if (opid.Value != "" && opid.Value != "180731559")
            {
                string email = dh.SQLServerGetDataTable(db_handle.Select_User_ConfidentialInformation_ByOpid(opid.Value)).Select().Select(s => s["e_mail"].ToString()).FirstOrDefault();

                if (dh.SQLServerGetDataTable(db_handle.Select_User_ConfidentialInformation_ByOpid(opid.Value)).Rows.Count <= 0)
                {
                    tijiao_tishi.InnerText = "输入的工号不存在，请重新输入。";
                    opid.Value = "";
                    return;
                }
                else if (email != "")
                {
                    string res = dh.SQLServerDBHandle(db_handle.Update_Pwd(opid.Value, "123456"));
                    if (res == "OK")
                    {
                        if (sm.SendEmail(email, "OFT RFID 设备管理系统--用户密码重置提醒", @"Dear，<br />
                                                                                        &nbsp; &nbsp; &nbsp; 您的OFT RFID 设备管理系统账号密码已经重置，登录用户名为：" + opid.Value + " ，初始密码为123456，请尽快登入系统修改密码。<br /> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; Form：系统管理员"))
                        {
                            tijiao_tishi.InnerText = "密码重置成功。初始密码为123456";
                            tijiao_tishi.Style.Add("color", "cornflowerblue");
                        }
                    }
                    else
                    {
                        tijiao_tishi.InnerText = "密码重置失败。ErrorMassage：" + res;
                        return;
                    }
                }
                else
                {
                    tijiao_tishi.InnerText = "此工号对应的邮箱为空。请至用户信息维护界面维护";
                    return;
                }
            }
            else if (opid.Value == "180731559")
            {
                tijiao_tishi.InnerText = "没有修改此工号密码的权限。";
                return;
            }
            else
            {
                tijiao_tishi.InnerText = "工号不能为空。";
                return;
            }
        }
    }
}