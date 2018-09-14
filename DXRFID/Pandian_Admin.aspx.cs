using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;

namespace DXRFID
{
    public partial class Pandian_Admin : System.Web.UI.Page
    {
        DXRFID.Class.DB_Handle db_handle = new Class.DB_Handle();
        DBHelper.DB dh = new DBHelper.DB();
        protected void Page_Load(object sender, EventArgs e)
        {
            dh._conn = ConfigurationManager.ConnectionStrings["RFIDConnectionString"].ConnectionString;
            dh._timeout = 5;
            tijiao_tishi.Style.Add("Color", "red");
            tijiao_tishi.InnerText = "";
            try
            {
                if (Session["SetPremission"].ToString() == "normal")
                {
                    no_show.Visible = true;
                    show.Visible = false;
                    NO_data.Text = "您不是管理员，不可进行盘点区域管理。";
                    return;
                }


                DataTable dt = dh.SQLServerGetDataTable(db_handle.Select_InStoreMsg_ByCustromsKeyValue("EquipmentStatus", "借出"));
                if (dt.Rows.Count > 0)
                {
                    no_show.Visible = true;
                    show.Visible = false;
                    NO_data.Text = "存在未归还设备，请至<a href=\"Ruku_Find.aspx\">设备信息</a>界面检查";
                    return;
                }
                else
                {
                    no_show.Visible = false;
                    show.Visible = true;
                }
            }
            catch
            {
                Response.Redirect("~/Account/Login.aspx");
            }
        }

        protected void Submit_ServerClick(object sender, EventArgs e)
        {
            string res = dh.SQLServerDBHandle(db_handle.Delete_TakeStockAdmin());
            if (res == "OK")
            {
                SqlDataSource_relieve.DataBind();
                ASPxGridView_show.DataBind();

            }
            else
            {
                tijiao_tishi.InnerText = "绑定关系删除失败。ErrorMassage:" + res;
            }
        }

        protected void ASPxGridView_show_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            GridViewDataColumn columnHobbies = ASPxGridView_show.Columns["区域名称"] as GridViewDataColumn;
            ASPxComboBox cbH1 = (ASPxGridView_show.FindEditRowCellTemplateControl(columnHobbies, "ASPxComboBox_StoragePlace") as ASPxComboBox);
            e.NewValues["区域名称"] = cbH1.Text;
        }
    }
}