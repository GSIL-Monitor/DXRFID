using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Data;

namespace DXRFID
{
    public partial class Ruku_Find : System.Web.UI.Page
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
                            dbound.DataBounds(db_handle.Select_InStoreMsg_ByCustromsKeyValue("[AssetNumber]", key_values.Value));
                        }
                        break;
                    case "RFID":
                        {
                            dbound.DataBounds(db_handle.Select_InStoreMsg_ByCustromsKeyValue("[RFID]", key_values.Value));
                        }
                        break;
                    case "名称":
                        {
                            dbound.DataBounds(db_handle.Select_InStoreMsg_ByCustromsKeyValue("[EquipmentName]", key_values.Value));
                        }
                        break;
                    case "规格":
                        {
                            dbound.DataBounds(db_handle.Select_InStoreMsg_ByCustromsKeyValue("[Specification]", key_values.Value));
                        }
                        break;
                    case "品牌":
                        {
                            dbound.DataBounds(db_handle.Select_InStoreMsg_ByCustromsKeyValue("[Brand]", key_values.Value));
                        }
                        break;
                    case "保管人":
                        {
                            dbound.DataBounds(db_handle.Select_InStoreMsg_ByCustromsKeyValue("[Keeper]", key_values.Value));
                        }
                        break;
                    case "存放地点":
                        {
                            dbound.DataBounds(db_handle.Select_InStoreMsg_ByCustromsKeyValue("[StoragePlace]", key_values.Value));
                        }
                        break;
                    case "入库时间":
                        {
                            dbound.DataBounds(db_handle.Select_InStoreMsg_ByCustromsKeyValue("CONVERT(nvarchar,[ruku_datetime],120)", key_values_datetime.Value));
                        }
                        break;
                    case "入库人":
                        {
                            dbound.DataBounds(db_handle.Select_InStoreMsg_ByCustromsKeyValue("[ruku_person]", key_values.Value));
                        }
                        break;
                    case "上次校验时间":
                        {
                            dbound.DataBounds(db_handle.Select_InStoreMsg_ByCustromsKeyValue("CONVERT(nvarchar,[CheckTime],120)", key_values_datetime.Value));
                        }
                        break;
                    case "校验负责人":
                        {
                            dbound.DataBounds(db_handle.Select_InStoreMsg_ByCustromsKeyValue("CheckPerson", key_values.Value));
                        }
                        break;
                    case "设备状态":
                        {
                            dbound.DataBounds(db_handle.Select_InStoreMsg_ByCustromsKeyValue("EquipmentStatus", key_values.Value));
                        }
                        break;
                    default: break;

                }
            }
            else if (select_key.Value == "所有类别" && key_values.Value.ToString() == "")
            {
                dbound.DataBounds(db_handle.Select_AllInStoreMsg());
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
            else
            {
                key_values_datetime.Style.Add("display", "none");
                key_values.Style.Add("display", "block");
            }

            key_values_datetime.Value = "";
            key_values.Value = "";
            key_values_datetime.Style.Add("color", "Gray");
            key_values.Style.Add("color", "Gray");
        }
    }
}