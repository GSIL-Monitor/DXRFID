using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace DXRFID.Class
{
    public class DataBound
    {
        public SqlDataSource SqlDataSource_show { get; set; }
        public DevExpress.Web.ASPxGridView ASPxGridView_show { get; set; }

        public void DataBounds(string sql)
        {
            SqlDataSource_show.SelectCommand = sql;
            SqlDataSource_show.DataBind();
            ASPxGridView_show.DataBind();
        }
    }
}