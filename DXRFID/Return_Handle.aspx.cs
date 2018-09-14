using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXRFID
{
    public partial class Return_Handle : System.Web.UI.Page
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
                    if (Request.Params.AllKeys.Contains("ctl00$Main$Return_People") && Request.Params["ctl00$Main$Return_People"].ToString() != "")
                    {
                        string ReturnPeople = Return_People.Value;
                        string res = dh.SQLServerDBHandle(db_handle.Insert_ReturnMsg_ByRFID(RFID.Value, ReturnPeople, Session["SetUserID"].ToString()));
                        if (res == "OK")
                        {
                            select_error.InnerText = "归还成功。";
                            select_error.Style.Add("Color", "cornflowerblue");
                            Clear();
                            show_return_msg.Visible = false;
                        }
                        else
                        {
                            tijiao_tishi.InnerText = "归还失败，ErrorMassage：" + res;
                            return;
                        }
                    }
                    else
                    {
                        if (dh.SQLServerGetDataTable(db_handle.Select_BorrowMsg_ByRFID(RFID.Value)).Rows.Count > 0)
                        {
                            Show_pic.Visible = true;
                            select_error.InnerText = "";
                            tijiao_tishi.InnerText = "";
                            Asset_Number.Value = dh.SQLServerGetDataTable(db_handle.Select_BorrowMsg_ByRFID(RFID.Value)).Select().Select(s => s["AssetNumber"].ToString()).FirstOrDefault();
                            EquipmentName.Value = dh.SQLServerGetDataTable(db_handle.Select_BorrowMsg_ByRFID(RFID.Value)).Select().Select(s => s["EquipmentName"].ToString()).FirstOrDefault();
                            Quantity.Value = dh.SQLServerGetDataTable(db_handle.Select_BorrowMsg_ByRFID(RFID.Value)).Select().Select(s => s["Quantity"].ToString()).FirstOrDefault();
                            Specification.Value = dh.SQLServerGetDataTable(db_handle.Select_BorrowMsg_ByRFID(RFID.Value)).Select().Select(s => s["Specification"].ToString()).FirstOrDefault();
                            Brand.Value = dh.SQLServerGetDataTable(db_handle.Select_BorrowMsg_ByRFID(RFID.Value)).Select().Select(s => s["Brand"].ToString()).FirstOrDefault();
                            Lender.Value = dh.SQLServerGetDataTable(db_handle.Select_BorrowMsg_ByRFID(RFID.Value)).Select().Select(s => s["Borrow_Person"].ToString()).FirstOrDefault();
                            Lending_Time.Value = dh.SQLServerGetDataTable(db_handle.Select_BorrowMsg_ByRFID(RFID.Value)).Select().Select(s => s["Borrow_Datetime"].ToString()).FirstOrDefault();
                            Loan_Period.Value = dh.SQLServerGetDataTable(db_handle.Select_BorrowMsg_ByRFID(RFID.Value)).Select().Select(s => s["Borrow_Cycle"].ToString()).FirstOrDefault();

                            DateTime Borrow_Time = Convert.ToDateTime(Lending_Time.Value);
                            TimeSpan diff = new TimeSpan();
                            if (System.DateTime.Now > Borrow_Time.AddDays(double.Parse(Loan_Period.Value)))
                            {
                                diff = System.DateTime.Now - Borrow_Time.AddDays(double.Parse(Loan_Period.Value));
                                select_error.Style.Add("color", "blue");
                                select_error.InnerText = "超过借出周期 " + diff.TotalDays.ToString("f3") + " 天";
                            }
                            show_return_msg.Visible = true;
                            Return_People.Focus();
                        }
                        else if (dh.SQLServerGetDataTable(db_handle.Check_RFID_ByStatus(RFID.Value, "借出")).Rows.Count > 0)
                        {
                            show_return_msg.Visible = false;
                            select_error.InnerText = RFID.Value + " 对应的设备在库中，未借出。";
                            RFID.Focus();
                            Clear();
                            return;
                        }
                        else
                        {
                            show_return_msg.Visible = false;
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
            Lender.Value = "";
            Loan_Period.Value = "";
            Lending_Time.Value = "";
        }
    }
}