using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXRFID
{
    public partial class UserManagement : System.Web.UI.Page
    {
        DXRFID.Class.DB_Handle db_handle = new Class.DB_Handle();
        DXRFID.Class.DataBound dbound = new Class.DataBound();
        DBHelper.DB dh = new DBHelper.DB();

        protected void Page_Load(object sender, EventArgs e)
        {
            dh._conn = ConfigurationManager.ConnectionStrings["RFIDConnectionString"].ConnectionString;
            dh._timeout = 5;

            dbound.SqlDataSource_show = SqlDataSource_show;
            dbound.ASPxGridView_show = ASPxGridView_show;

            dbound.DataBounds(db_handle.Select_UserAllInformation());

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

        protected void Download_ServerClick(object sender, EventArgs e)
        {
            ASPxGridView_show.ExportXlsxToResponse();
        }

        protected void Refrash_ServerClick(object sender, EventArgs e)
        {
            Response.AddHeader("Refresh", "0");
            select_key.SelectedIndex = 0;
        }

        protected void Key_ServerClick(object sender, EventArgs e)
        {
            if (key_values.Value.ToString() != "")
            {
                switch (select_key.Value)
                {
                    case "工号":
                        {
                            dbound.DataBounds(db_handle.Select_UserInformation("[opid]", key_values.Value));
                        }
                        break;
                    case "姓名":
                        {
                            dbound.DataBounds(db_handle.Select_UserInformation("[name]", key_values.Value));
                        }
                        break;
                    case "部门":
                        {
                            dbound.DataBounds(db_handle.Select_UserInformation("[function]", key_values.Value));
                        }
                        break;
                    default: break;

                }
            }
            else if (select_key.Value == "所有类别")
            {
                dbound.DataBounds(db_handle.Select_UserAllInformation());
            }
        }

        protected void ASPxGridView_show_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            GridViewDataColumn columnHobbies = ASPxGridView_show.Columns["权限"] as GridViewDataColumn;
            ASPxComboBox cbH1 = (ASPxGridView_show.FindEditRowCellTemplateControl(columnHobbies, "ASPxComboBox_premission") as ASPxComboBox);
            e.NewValues["权限"] = cbH1.Value.ToString().ToLower();
        }
    }
}