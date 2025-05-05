using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IMPALWeb.Masters.Others
{
    public partial class ItemPosting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ucSupplierPartNumber_SearchImageClicked(object sender, EventArgs e)
        {
            try
            {
                txtItemCode.Text = Session["ItemCode"].ToString();
                Session["SupplierPartNumber"] = "";
                Session["ItemCode"] = "";
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        protected void ddlLineCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            supLineCtl.SupplierLine = ddlLineCode.SelectedValue;
        }
    }
}
