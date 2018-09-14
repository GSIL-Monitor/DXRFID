using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace DXRFID
{
    public partial class No_Pandian_List : System.Web.UI.Page
    {
        DXRFID.Class.DB_Handle db_handle = new Class.DB_Handle();
        DXRFID.Class.DataBound dbound = new Class.DataBound();
        DBHelper.DB dh = new DBHelper.DB();
        protected void Page_Load(object sender, EventArgs e)
        {
            tishi.Text = "";
            dh._conn = ConfigurationManager.ConnectionStrings["RFIDConnectionString"].ConnectionString;
            dh._timeout = 5;

            dbound.SqlDataSource_show = SqlDataSource_show;
            dbound.ASPxGridView_show = ASPxGridView_show;
            
            if (dh.SQLServerGetDataTable(db_handle.Check_TakeStockTime()).Rows.Count > 0 && System.DateTime.Now >= Convert.ToDateTime(dh.SQLServerGetDataTable(db_handle.Check_TakeStockTime()).Select().Select(s => s["TakeStockStartTime"].ToString()).FirstOrDefault()))
            {
                no_show.Visible = false;
                show.Visible = true;
                dbound.DataBounds(db_handle.Select_AllTakeStockStatus());

                DataTable dt = dh.SQLServerGetDataTable(db_handle.Select_TakeStockCount());
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    tishi.Text += dt.Rows[i].ItemArray[0].ToString() + " : " + dt.Rows[i].ItemArray[1].ToString() + "   ";
                }
                tishi.Text = tishi.Text.Trim();
            }
            else
            {
                no_show.Visible = true;
                show.Visible = false;
                NO_data.Text = "盘点尚未开始,无法查询盘点状态。";
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
    }
}