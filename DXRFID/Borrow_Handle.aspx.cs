using System;
using System.Linq;
using System.Configuration;

namespace DXRFID
{
    public partial class Borrow_Handle : System.Web.UI.Page
    {
        DXRFID.Class.DB_Handle db_handle = new Class.DB_Handle();
        DBHelper.DB dh = new DBHelper.DB();
        protected void Page_Load(object sender, EventArgs e)
        {
            dh._conn = ConfigurationManager.ConnectionStrings["RFIDConnectionString"].ConnectionString;
            dh._timeout = 5;
            select_error.Style.Add("Color", "red");
            tijiao_tishi.Style.Add("Color", "red");
            tijiao_tishi.InnerText = "";
            select_error.InnerText = "";

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


            if (IsPostBack)
            {
                if (Request.Params.AllKeys.Contains("ctl00$Main$RFID") && Request.Params["ctl00$Main$RFID"].ToString() != "")
                {
                    Show_pic.Attributes.Add("onclick", "javascript: window.open('Class/ConsoleBorrow_ShowPicForCustomer.aspx?RFID=" + RFID.Value + "', '', 'width=' + (screen.width / 4) + ',height=' + (screen.availHeight / 2) + ',left=' + (screen.width / 4) + ',top=' + (screen.availHeight / 4) + ',scrollbars=yes,resizable=no,location=no,toolbar=no,menubar=no,resizable=no,status=no,directories:no')");
                    if (Request.Params.AllKeys.Contains("ctl00$Main$Lender") && Request.Params.AllKeys.Contains("ctl00$Main$Lending_Reasons") && Request.Params.AllKeys.Contains("ctl00$Main$Loan_Period") && (Request.Params["ctl00$Main$Lender"].ToString() != "" && Request.Params["ctl00$Main$Loan_Period"].ToString() != ""))
                    {
                        string Borrow_Person = Lender.Value;
                        string Borrow_Reason = Lending_Reasons.Value;
                        int Borrow_Cycle = int.Parse(Loan_Period.Value);

                        string res = dh.SQLServerDBHandle(db_handle.Insert_BorrowMsg_ByRFID(RFID.Value, Borrow_Person, Borrow_Reason, Borrow_Cycle,Session["SetUserID"].ToString()));
                        if (res == "OK")
                        {
                            select_error.InnerText = "借出成功，请提醒借出人定时归还";
                            select_error.Style.Add("Color", "cornflowerblue");
                            Clear();
                            show_borrow_msg.Visible = false;
                        }
                        else
                        {
                            tijiao_tishi.InnerText = "借出失败，ErrorMassage：" + res;
                            return;
                        }
                    }
                    else
                    {
                        if (dh.SQLServerGetDataTable(db_handle.Select_InstoreEquipmentMsg_ByRFID(RFID.Value)).Rows.Count > 0)
                        {
                            select_error.InnerText = "";
                            tijiao_tishi.InnerText = "";
                            Show_pic.Visible = true;
                            Asset_Number.Value = dh.SQLServerGetDataTable(db_handle.Select_InstoreEquipmentMsg_ByRFID(RFID.Value)).Select().Select(s => s["AssetNumber"].ToString()).FirstOrDefault();
                            EquipmentName.Value = dh.SQLServerGetDataTable(db_handle.Select_InstoreEquipmentMsg_ByRFID(RFID.Value)).Select().Select(s => s["EquipmentName"].ToString()).FirstOrDefault();
                            Quantity.Value = dh.SQLServerGetDataTable(db_handle.Select_InstoreEquipmentMsg_ByRFID(RFID.Value)).Select().Select(s => s["Quantity"].ToString()).FirstOrDefault();
                            Specification.Value = dh.SQLServerGetDataTable(db_handle.Select_InstoreEquipmentMsg_ByRFID(RFID.Value)).Select().Select(s => s["Specification"].ToString()).FirstOrDefault();
                            Brand.Value = dh.SQLServerGetDataTable(db_handle.Select_InstoreEquipmentMsg_ByRFID(RFID.Value)).Select().Select(s => s["Brand"].ToString()).FirstOrDefault();
                            Storage_Place.Value = dh.SQLServerGetDataTable(db_handle.Select_InstoreEquipmentMsg_ByRFID(RFID.Value)).Select().Select(s => s["StoragePlace"].ToString()).FirstOrDefault();
                            Keeper.Value = dh.SQLServerGetDataTable(db_handle.Select_InstoreEquipmentMsg_ByRFID(RFID.Value)).Select().Select(s => s["Keeper"].ToString()).FirstOrDefault();
                            show_borrow_msg.Visible = true;
                            Lender.Focus();
                        }
                        else if (dh.SQLServerGetDataTable(db_handle.Check_RFID_ByStatus(RFID.Value, "借出")).Rows.Count > 0)
                        {
                            show_borrow_msg.Visible = false;
                            select_error.InnerText = RFID.Value + " 对应的设备已借出。";
                            RFID.Focus();
                            Clear();
                            return;
                        }
                        else
                        {
                            show_borrow_msg.Visible = false;
                            select_error.InnerText = RFID.Value + " 对应的设备信息未找到。";
                            RFID.Focus();
                            Clear();
                            return;
                        }
                    }
                }
                else if (Request.Params["ctl00$Main$RFID"].ToString() == "")
                {
                    select_error.InnerText = "有必填项为空，请填写完整。";
                    return;
                }
                else
                {
                    select_error.InnerText = "缺少RFID传送值。";
                    return;
                }
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
            Lender.Value = "";
            Lending_Reasons.Value = "";
            Loan_Period.Value = "";
        }
    }
}