using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXRFID
{
    public partial class Update_EquipmentInformation : System.Web.UI.Page
    {
        DXRFID.Class.DB_Handle db_handle = new Class.DB_Handle();
        DBHelper.DB dh = new DBHelper.DB();
        string UploadDirectory = "~/UploadPicture/";
        protected void Page_Load(object sender, EventArgs e)
        {
            dh._conn = ConfigurationManager.ConnectionStrings["RFIDConnectionString"].ConnectionString;
            dh._timeout = 5;
            tijiao_tishi.Style.Add("Color", "red");
            tijiao_tishi.InnerText = "";
            RFID_tishi.InnerText = "";
            
            //盘点禁止操作
            if (dh.SQLServerGetDataTable(db_handle.Check_TakeStockTime()).Rows.Count > 0 && dh.SQLServerGetDataTable(db_handle.Check_TakeStockTime()).Select().Select(s => s["TakeStockStartTime"].ToString()).FirstOrDefault() != "")
            {
                if (System.DateTime.Now >= Convert.ToDateTime(dh.SQLServerGetDataTable(db_handle.Check_TakeStockTime()).Select().Select(s => s["TakeStockStartTime"].ToString()).FirstOrDefault()))
                {
                    NO_data.Text = "盘点开始不可操作。";
                    no_show.Visible = true;
                    show.Visible = false;
                    return;
                }
                else
                {
                    no_show.Visible = false;
                    show.Visible = true;
                }
            }

            //权限
            try
            {
                if (Session["SetPremission"].ToString() == "normal")
                {
                    no_show.Visible = true;
                    show.Visible = false;
                    NO_data.Text = "您不是管理员，不可进行设备信息管理。";
                    return;
                }
            }
            catch
            {
                Response.Redirect("~/Account/Login.aspx");
            }
        }

        protected void Submit_ServerClick(object sender, EventArgs e)
        {
            string res;
            try
            {
                if (RFID.Value != "" && (Session["old_Keeper"].ToString() != Keeper.Value || Session["old_Storage_Place"].ToString() != Storage_Place.Value || Session["FileName"] != null && Session["FileName"].ToString() != ""))
                {
                    if (Session["FileName"] != null && Session["FileName"].ToString() != "")
                    {
                        res = dh.SQLServerDBHandle(db_handle.Update_EquipmentInformation(Storage_Place.Value, Keeper.Value, RFID.Value, Session["SetUserID"].ToString(), Session["FileName"].ToString()));

                        if (res == "OK")
                        {
                            if (Description.Value != "")
                            {
                                string filePath = Path.Combine(Server.MapPath(UploadDirectory), Description.Value.Split(':')[1]);
                                File.Delete(filePath);
                            }
                        }
                        else
                        {
                            tijiao_tishi.InnerText = "修改失败,ErrorMassage:" + res;
                        }
                    }
                    else
                    {
                        res = dh.SQLServerDBHandle(db_handle.Update_EquipmentInformation(Storage_Place.Value, Keeper.Value, RFID.Value, Session["SetUserID"].ToString(), Description.Value.Split(':')[1]));
                    }

                    if (res == "OK")
                    {
                        tijiao_tishi.InnerText = "修改成功";
                        tijiao_tishi.Style.Add("color", "cornflowerblue");
                        Clear();
                        RFID.Focus();
                    }
                }
                else if (Session["old_Keeper"].ToString() == Keeper.Value && Session["old_Keeper"] != null && Session["old_Storage_Place"].ToString() == Storage_Place.Value && Session["old_Storage_Place"] != null)
                {
                    tijiao_tishi.InnerText = "修改失败,输入的保管人及存放地点与原保管人及存放地点相同，未修改任何信息。";
                    return;
                }
                else
                {
                    tijiao_tishi.InnerText = "RFID不能为空.";
                    Clear();
                    return;
                }
            }
            catch
            {

            }
        }

        void Clear()
        {
            RFID.Value = "";
            Asset_Number.Value = "";
            EquipmentName.Value = "";
            Quantity.Value = "";
            Specification.Value = "";
            Brand.Value = "";
            Storage_Place.Value = "";
            Keeper.Value = "";
            Show_pic.Visible = false;
        }

        protected void RFID_ServerChange(object sender, EventArgs e)
        {
            if (RFID.Value != "")
            {
                Show_pic.Attributes.Add("onclick", "javascript: window.open('Class/ConsoleBorrow_ShowPicForCustomer.aspx?RFID=" + RFID.Value + "', '', 'width=' + (screen.width / 4) + ',height=' + (screen.availHeight / 2) + ',left=' + (screen.width / 4) + ',top=' + (screen.availHeight / 4) + ',scrollbars=yes,resizable=no,location=no,toolbar=no,menubar=no,resizable=no,status=no,directories:no')");
                if (dh.SQLServerGetDataTable(db_handle.Select_InstoreEquipmentMsg_ByRFID(RFID.Value)).Rows.Count > 0)
                {
                    Show_pic.Visible = true;
                    Upload.Visible = false;
                    submit.Attributes.Remove("disabled");
                    Asset_Number.Value = dh.SQLServerGetDataTable(db_handle.Select_InstoreEquipmentMsg_ByRFID(RFID.Value)).Select().Select(s => s["AssetNumber"].ToString()).FirstOrDefault();
                    EquipmentName.Value = dh.SQLServerGetDataTable(db_handle.Select_InstoreEquipmentMsg_ByRFID(RFID.Value)).Select().Select(s => s["EquipmentName"].ToString()).FirstOrDefault();
                    Quantity.Value = dh.SQLServerGetDataTable(db_handle.Select_InstoreEquipmentMsg_ByRFID(RFID.Value)).Select().Select(s => s["Quantity"].ToString()).FirstOrDefault();
                    Specification.Value = dh.SQLServerGetDataTable(db_handle.Select_InstoreEquipmentMsg_ByRFID(RFID.Value)).Select().Select(s => s["Specification"].ToString()).FirstOrDefault();
                    Brand.Value = dh.SQLServerGetDataTable(db_handle.Select_InstoreEquipmentMsg_ByRFID(RFID.Value)).Select().Select(s => s["Brand"].ToString()).FirstOrDefault();
                    Session["old_Storage_Place"] = Storage_Place.Value = dh.SQLServerGetDataTable(db_handle.Select_InstoreEquipmentMsg_ByRFID(RFID.Value)).Select().Select(s => s["StoragePlace"].ToString()).FirstOrDefault();
                    Session["old_Keeper"] = Keeper.Value = dh.SQLServerGetDataTable(db_handle.Select_InstoreEquipmentMsg_ByRFID(RFID.Value)).Select().Select(s => s["Keeper"].ToString()).FirstOrDefault();
                    Description.Value = "点此重新上传图片 原图片名:" + dh.SQLServerGetDataTable(db_handle.Select_InstoreEquipmentMsg_ByRFID(RFID.Value)).Select().Select(s => s["picture"].ToString()).FirstOrDefault();
                    Description.Visible = true;
                }
                else if (dh.SQLServerGetDataTable(db_handle.Check_RFID_ByStatus(RFID.Value, "借出")).Rows.Count > 0)
                {
                    tijiao_tishi.InnerText = RFID.Value + " 对应的设备已借出,不可修改。";
                    submit.Attributes.Add("disabled", "disabled");
                    RFID.Focus();
                    return;
                }
                else
                {
                    tijiao_tishi.InnerText = RFID.Value + " 对应的设备信息未找到。";
                    Clear();
                    RFID.Focus();
                    return;
                }
            }
            else
            {
                RFID_tishi.InnerText = "RFID不能为空.";
                Clear();
                return;
            }
        }

        protected void Description_ServerClick(object sender, EventArgs e)
        {
            Upload.Visible = true;
            Description.Visible = false;
        }

        string SavePostedFile(UploadedFile uploadedFile)
        {
            if (System.IO.Directory.Exists(Server.MapPath(UploadDirectory)) == false)//如果不存在就创建file文件夹
            {
                System.IO.Directory.CreateDirectory(Server.MapPath(UploadDirectory));
            }

            if (!uploadedFile.IsValid)
            {
                return string.Empty;
            }

            string fileName = Guid.NewGuid() + Path.GetExtension(uploadedFile.FileName);  //文件名
            string filePath = Path.Combine(Server.MapPath(UploadDirectory), fileName);    //文件完整物理路径
            uploadedFile.SaveAs(filePath);
            Session["FileName"] = fileName;
            return fileName;  //返回文件名用于预览

        }

        protected void Upload_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            e.CallbackData = SavePostedFile(e.UploadedFile);
        }
    }
}