using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXRFID
{
    public partial class Header : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckSessionLogin.CheckLogin(this.Page);

            string opid = Session["SetUserID"].ToString();
            DBHelper.DB dh = new DBHelper.DB();
            Class.DB_Handle db_handle = new Class.DB_Handle();
            dh._conn = ConfigurationManager.ConnectionStrings["RFIDConnectionString"].ConnectionString;
            dh._timeout = 5;

            string picid = dh.SQLServerGetDataTable(db_handle.SelectHeadPic(opid)).Select().Select(s => s["picture"].ToString()).FirstOrDefault();
            headpic.Attributes.Add("src","assets/img/"+ picid + "");
            
            UpdatePic.Attributes.Add("onclick", "javascript: window.open('Class/UpdateHeadPic.aspx?opid=" + opid + "', '', 'width=' + (screen.width / 4) + ',height=' + (screen.availHeight / 2) + ',left=' + (screen.width / 4) + ',top=' + (screen.availHeight / 4) + ',scrollbars=yes,resizable=no,location=no,toolbar=no,menubar=no,resizable=no,status=no,directories:no')");
        }

        public void ClearClientPageCache()
        {
            Response.Buffer = true;
            Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
            Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.Cache.SetNoStore();
        }

        protected void ClearLogin_Click(object sender, EventArgs e)
        {
            Session["SetUserID"] = null;
            ClearClientPageCache();
            CheckSessionLogin.CheckLogin(this.Page);
        }

        protected void Changepwd_Click(object sender, EventArgs e)
        {
            CheckSessionLogin.ChangePwd(this.Page);
            ClearLogin_Click(sender, e);
        }

        protected void ChangeMsg_Click(object sender, EventArgs e)
        {
            CheckSessionLogin.ChangeMsg(this.Page);
            ClearLogin_Click(sender, e);
        }
    }
}