using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IMPALWeb.UserControls
{
    
    public partial class SupplierPartNumberSearch : System.Web.UI.UserControl
    {
        public event EventHandler SearchImageClicked;
        public event EventHandler SupplierddlChanged;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ImgButtonQuery_Click(object sender, ImageClickEventArgs e)
        {
            if (SearchImageClicked != null)
                SearchImageClicked(this, EventArgs.Empty);
        }

        protected void ddlSupplierPartNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["UserCtrolSupplierPartNumber"] = ddlSupplierPartNumber.SelectedItem.Text;
            Session["UserCtrolSupplierItemCode"] = ddlSupplierPartNumber.SelectedItem.Value;
            if (SupplierddlChanged != null)
                SupplierddlChanged(this, EventArgs.Empty);
        }
 
    }
}