using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DXRFID
{
    public partial class Pandian_History : System.Web.UI.Page
    {
        DXRFID.Class.DB_Handle db_handle = new Class.DB_Handle();
        DXRFID.Class.DataBound dbound = new Class.DataBound();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (key_values_datetime.Value == "")
            {
                ASPxGridView_show.Visible = false;
                download.Attributes.Add("disabled", "disabled");
                tishi.Text = "请先选择要查询的盘点时间";
                key_values_datetime.Focus();
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
            if (key_values_datetime.Value != "")
            {
                ASPxGridView_show.Visible = true;
                download.Attributes.Remove("disabled");
                dbound.DataBounds(db_handle.Select_TakeStockHistory_ByTakeStockTime(key_values_datetime.Value));
                tishi.Text = "共有 " + ASPxGridView_show.VisibleRowCount + " 台设备记录,红色底纹标记为系统存放地点与盘点存放地点不一致记录";
            }
        }

        protected void ASPxGridView_show_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {
            if (e.GetValue("系统存放地点") != e.GetValue("盘点存放地点"))
            {
                e.Row.BackColor = System.Drawing.Color.LemonChiffon;
            }
        }
    }
}