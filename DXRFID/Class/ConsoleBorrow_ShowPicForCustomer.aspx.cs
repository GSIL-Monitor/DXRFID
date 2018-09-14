using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXRFID.Class
{
    public partial class ConsoleBorrow_ShowPicForCustomer : System.Web.UI.Page
    {
        DXRFID.Class.DB_Handle db_handle = new Class.DB_Handle();
        DBHelper.DB dh = new DBHelper.DB();
        protected void Page_Load(object sender, EventArgs e)
        {
            dh._conn = System.Configuration.ConfigurationManager.ConnectionStrings["RFIDConnectionString"].ConnectionString;
            dh._timeout = 5;
            string UploadDirectory = "~/UploadPicture/";

            try
            {
                if (Request.Params.AllKeys.Contains("RFID") && Request.Params["RFID"].ToString() != "" && Session["SetUserID"].ToString() != null)
                {
                    Image_show.ImageUrl = UploadDirectory + dh.SQLServerGetDataTable(db_handle.Select_EquipmentPic_ByRFID(Request.Params["RFID"].ToString())).Select().Select(s=>s["picture"].ToString()).FirstOrDefault();
                    Label_name.Text = "资产编码：" + dh.SQLServerGetDataTable(db_handle.Select_EquipmentPic_ByRFID(Request.Params["RFID"].ToString())).Select().Select(s => s["AssetNumber"].ToString()).FirstOrDefault() + "<br />RFID：" + Request.Params["RFID"].ToString() + "<br />设备名称："+ dh.SQLServerGetDataTable(db_handle.Select_EquipmentPic_ByRFID(Request.Params["RFID"].ToString())).Select().Select(s => s["EquipmentName"].ToString()).FirstOrDefault();
                }
                else
                {
                    Response.Redirect("~/Account/Login.aspx");
                }
            }
            catch
            {
                Response.Redirect("~/Account/Login.aspx");
            }
        }
    }
}