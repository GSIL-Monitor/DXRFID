using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXRFID
{
    public partial class Pandian_List : System.Web.UI.Page
    {
        DXRFID.Class.DB_Handle db_handle = new Class.DB_Handle();
        DBHelper.DB dh = new DBHelper.DB();
        Class.DataBound DataBounds = new Class.DataBound();
        protected void Page_Load(object sender, EventArgs e)
        {
            dh._conn = ConfigurationManager.ConnectionStrings["RFIDConnectionString"].ConnectionString;
            dh._timeout = 5;
            DataBounds.ASPxGridView_show = ASPxGridView_show;
            DataBounds.SqlDataSource_show = SqlDataSource_show;

            if (dh.SQLServerGetDataTable(db_handle.Check_TakeStockTime()).Rows.Count > 0 && System.DateTime.Now.AddDays(7) >= Convert.ToDateTime(dh.SQLServerGetDataTable(db_handle.Check_TakeStockTime()).Select().Select(s=>s["TakeStockStartTime"].ToString()).FirstOrDefault()))
            {
                DataBounds.DataBounds(db_handle.Select_WaitTakeStockList());
                no_show.Visible = false;
                show.Visible = true;
            }
            else
            {
                no_show.Visible = true;
                show.Visible = false;
                NO_data.Text = "盘点尚未开始,无法查询盘点列表,将在预计盘点时间的提前一周开放。";
                return;

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

        protected void ASPxGridView_show_DataBound(object sender, EventArgs e)
        {
            for (int i = 0; i < ASPxGridView_show.VisibleRowCount; i++)
            {
                if (ASPxGridView_show.GetRowValues(i, "设备状态").ToString() != "在库")
                {
                    tishi.Text = "存在没有归还的设备，请及时处理，否则无法进行盘点。";
                    DownLoad.Attributes.Add("disabled", "disabled");
                    return;
                }
            }
        }
    }
}