using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXRFID
{
    public partial class UpdateEquipmentInformation_History : System.Web.UI.Page
    {
        DXRFID.Class.DB_Handle db_handle = new Class.DB_Handle();
        DXRFID.Class.DataBound dbound = new Class.DataBound();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (key_values.Value == "")
            {
                tishi.Visible = true;
                ASPxGridView_show.Visible = false;
                download.Attributes.Add("disabled", "disabled");
                tishi.Text = "请先输入要查询的RFID";
                key_values.Focus();
            }
            else
            {
                dbound.ASPxGridView_show = ASPxGridView_show;
                dbound.SqlDataSource_show = SqlDataSource_show;
            }
        }

        protected void Download_ServerClick(object sender, EventArgs e)
        {
            ASPxGridView_show.ExportXlsxToResponse();
        }

        protected void Refrash_ServerClick(object sender, EventArgs e)
        {
            Response.AddHeader("Refresh", "0");
        }

        protected void Key_ServerClick(object sender, EventArgs e)
        {
            if (key_values.Value != "")
            {
                ASPxGridView_show.Visible = true;
                download.Attributes.Remove("disabled");
                dbound.DataBounds(db_handle.Select_UpdateEquipmentInformation_ByRFID(key_values.Value));
                tishi.Visible = false;
            }
        }
    }
}