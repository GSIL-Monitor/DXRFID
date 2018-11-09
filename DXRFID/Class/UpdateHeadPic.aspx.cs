using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXRFID.Class
{
    public partial class UpdateHeadPic : System.Web.UI.Page
    {
        string Upload_res = "";
        string UploadDirectory = "~/assets/img/";
        DXRFID.Class.DB_Handle db_handle = new Class.DB_Handle();
        DBHelper.DB dh = new DBHelper.DB();

        protected void Page_Load(object sender, EventArgs e)
        {
            dh._conn = ConfigurationManager.ConnectionStrings["RFIDConnectionString"].ConnectionString;
            dh._timeout = 5;

            if (!(Request.Params.AllKeys.Contains("opid")))
            {
                Response.Redirect("~/Account/Login.aspx");
            }
            if (Upload_res.Contains("上传失败"))
            {
                tijiao_tishi.InnerHtml = "头像图片上传失败,请重新上传。";
            }
        }

        protected void Upload_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            e.CallbackData = SavePostedFile(e.UploadedFile);
            Page_Load(sender, e);
        }

        
        string SavePostedFile(UploadedFile uploadedFile)
        {
            dh._conn = ConfigurationManager.ConnectionStrings["RFIDConnectionString"].ConnectionString;
            dh._timeout = 5;
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
                if (dh.SQLServerDBHandle(db_handle.UpdateUserHeadPic(Request.Params["opid"].ToString(), fileName)) == "OK")
                {
                    tijiao_tishi.InnerHtml = "头像图片上传失败,请重新上传。";
                }
                else
                {
                    return Upload_res = "上传失败.";
                }
            }
            catch(Exception me)
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
    }
}