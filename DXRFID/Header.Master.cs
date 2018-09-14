using System;
using System.Collections.Generic;
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