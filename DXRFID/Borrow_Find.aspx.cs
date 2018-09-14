using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXRFID
{
    public partial class Borrow_Find : System.Web.UI.Page
    {
        DXRFID.Class.DB_Handle db_handle = new Class.DB_Handle();
        DXRFID.Class.DataBound dbound = new Class.DataBound();
        protected void Page_Load(object sender, EventArgs e)
        {
            dbound.ASPxGridView_show = ASPxGridView_show;
            dbound.SqlDataSource_show = SqlDataSource_show;
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
            if (key_values.Value.ToString() != "" || key_values_datetime.Value.ToString() != "")
            {
                switch (select_key.Value)
                {
                    case "资产编码":
                        {
                            dbound.DataBounds(db_handle.Select_BorrowMsg_ByCustromsKeyValue("[AssetNumber]", key_values.Value));
                        }
                        break;
                    case "RFID":
                        {
                            dbound.DataBounds(db_handle.Select_BorrowMsg_ByCustromsKeyValue("b.[RFID]", key_values.Value));
                        }
                        break;
                    case "名称":
                        {
                            dbound.DataBounds(db_handle.Select_BorrowMsg_ByCustromsKeyValue("[EquipmentName]", key_values.Value));
                        }
                        break;
                    case "规格":
                        {
                            dbound.DataBounds(db_handle.Select_BorrowMsg_ByCustromsKeyValue("[Specification]", key_values.Value));
                        }
                        break;
                    case "品牌":
                        {
                            dbound.DataBounds(db_handle.Select_BorrowMsg_ByCustromsKeyValue("[Brand]", key_values.Value));
                        }
                        break;
                    case "保管人":
                        {
                            dbound.DataBounds(db_handle.Select_BorrowMsg_ByCustromsKeyValue("[Keeper]", key_values.Value));
                        }
                        break;
                    case "存放地点":
                        {
                            dbound.DataBounds(db_handle.Select_BorrowMsg_ByCustromsKeyValue("[StoragePlace]", key_values.Value));
                        }
                        break;
                    case "借出人":
                        {
                            dbound.DataBounds(db_handle.Select_BorrowMsg_ByCustromsKeyValue("[Borrow_Person]", key_values.Value));
                        }
                        break;
                    case "借出时间":
                        {
                            dbound.DataBounds(db_handle.Select_BorrowMsg_ByCustromsKeyValue("CONVERT(nvarchar,[Borrow_Datetime],120)", key_values_datetime.Value));
                        }
                        break;
                    case "借出周期":
                        {
                            dbound.DataBounds(db_handle.Select_BorrowMsg_ByCustromsKeyValue("[Borrow_Cycle]", key_values.Value));
                        }
                        break;
                    case "借出操作人":
                        {
                            dbound.DataBounds(db_handle.Select_BorrowMsg_ByCustromsKeyValue("[Operator]", key_values_datetime.Value));
                        }
                        break;
                    default: break;

                }
            }
            else if(select_key.Value == "所有类别" && key_values.Value.ToString() == "")
            {
                dbound.DataBounds(@"SELECT [AssetNumber] 资产编码,a.[RFID],[EquipmentName] 名称,[Specification] 规格,[Brand] 品牌,[Quantity] 数量,[Keeper] 保管人,[StoragePlace] 存放地点,b.[Borrow_Person] 借出人,
                                        b.[Borrow_Datetime] 借出时间,b.[Borrow_Cycle] 借出周期,b.[Borrow_Reason] 借出原因,b.[Operator] 借出操作人 FROM [RFID].[dbo].[EquipmentInformation]
                                        a inner join[RFID].[dbo].[BorrowInformation] b on a.[RFID] = b.[RFID] order by RFID");
            }
            else
            {
                key_values.Style.Add("color", "red");
                key_values_datetime.Style.Add("color", "red");
                return;
            }


            if (select_key.Value.ToString().Contains("时间"))
            {
                key_values_datetime.Style.Add("display", "block");
                key_values.Style.Add("display", "none");
            }

            key_values_datetime.Value = "";
            key_values.Value = "";
            key_values_datetime.Style.Add("color", "Gray");
            key_values.Style.Add("color", "Gray");
        }
    }
}