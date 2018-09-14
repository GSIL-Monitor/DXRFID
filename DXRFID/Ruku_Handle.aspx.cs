using System;
using System.Linq;
using System.Configuration;
using DevExpress.Web;
using System.IO;
using System.Web.UI.WebControls;

namespace DXRFID
{
    public partial class Ruku_Handle : System.Web.UI.Page
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

            if (dh.SQLServerGetDataTable(db_handle.Check_TakeStockTime()).Rows.Count > 0 && dh.SQLServerGetDataTable(db_handle.Check_TakeStockTime()).Select().Select(s=>s["TakeStockStartTime"].ToString()).FirstOrDefault() != "")
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



            if (Request.Params.AllKeys.Contains("ctl00$Main$Asset_Number") && Request.Params.AllKeys.Contains("ctl00$Main$RFID") && Request.Params.AllKeys.Contains("ctl00$Main$Equipment_Name") && Request.Params.AllKeys.Contains("ctl00$Main$Quantity") && Request.Params.AllKeys.Contains("ctl00$Main$Specification") && Request.Params.AllKeys.Contains("ctl00$Main$Brand") && Request.Params.AllKeys.Contains("ctl00$Main$Storage_Place") && Request.Params.AllKeys.Contains("ctl00$Main$Keeper"))
            {
                if (Request.Params["ctl00$Main$Asset_Number"].ToString() == "" || Request.Params["ctl00$Main$RFID"].ToString() == "" || Request.Params["ctl00$Main$Equipment_Name"].ToString() == "" || Request.Params["ctl00$Main$Quantity"].ToString() == "" || Request.Params["ctl00$Main$Storage_Place"].ToString() == "")
                {
                    tijiao_tishi.InnerText = "有必填项为空，请填写完整。";
                    return;
                }
                else if (Session["FileName"] == null)
                {
                    tijiao_tishi.InnerText = "描述图片未上传。";
                    return;
                }
                else if (Upload_res.Contains("上传失败"))
                {
                    tijiao_tishi.InnerText = "描述图片上传失败,请重新上传。";
                    return;
                }
                else
                {
                    string AssetNumber = Asset_Number.Value;
                    string RFID_str = RFID.Value;
                    string EquipmentName = Equipment_Name.Value;
                    int Quantitys = int.Parse(Quantity.Value);
                    string Specifications = Specification.Value;
                    string Brands = Brand.Value;
                    string StoragePlace = Storage_Place.Value;
                    string Keepers = Keeper.Value;
                    string FileName = Session["FileName"].ToString();
                    string res = dh.SQLServerDBHandle(db_handle.Insert_EquipmentMsg(AssetNumber, RFID_str, EquipmentName, Quantitys, Specifications, Brands, StoragePlace, Keepers, "", FileName, Session["SetUserID"].ToString()));
                    if (res == "OK")
                    {
                        tijiao_tishi.InnerText = "设备入库成功。";
                        tijiao_tishi.Style.Add("Color", "cornflowerblue");
                        Clear();
                    }
                    else
                    {
                        tijiao_tishi.InnerText = "设备入库失败。ErrorMassage：" + res;
                        File.Delete(Path.Combine(Server.MapPath(UploadDirectory), FileName));
                        Session["FileName"] = null;
                        return;
                    }
                }
            }
        }
        string Upload_res = "";
        
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
            try
            {
                uploadedFile.SaveAs(filePath);
            }
            catch
            {
                return Upload_res = "上传失败.";
            }

            if (File.Exists(filePath))
            {
                Session["FileName"] = fileName;
                return fileName;  //返回文件名用于预览
            }
            else
            {
                return Upload_res = "上传失败.";
            }
        }

        protected void Upload_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            e.CallbackData = SavePostedFile(e.UploadedFile);
        }

        void Clear()
        {
            Asset_Number.Value = "";
            RFID.Value = "";
            Equipment_Name.Value = "";
            Quantity.Value = "";
            Specification.Value = "";
            Brand.Value = "";
            Storage_Place.Value = "";
            Keeper.Value = "";
            Session["FileName"] = null;
        }
        
    }
}