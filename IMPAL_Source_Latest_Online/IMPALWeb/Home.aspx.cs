#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using IMPALLibrary;
using System.Diagnostics;
#endregion Namespace

namespace IMPALWeb
{
    public partial class Home : System.Web.UI.Page
    {
        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            //To hide the Site Map Bread Crumb on Home Page

            HtmlContainerControl siteMapBreadCrumb = (HtmlContainerControl)this.Master.FindControl("SiteMapPathHolder");
            siteMapBreadCrumb.Visible = false;

            if (!String.IsNullOrEmpty(Request.QueryString["ConfirmMsg"]))
            {
                if (Request.QueryString["ConfirmMsg"].ToString().ToUpper() == "INVTRANS")
                    ClientScript.RegisterStartupScript(this.GetType(), "WarningMsg", "<script>alert('Transactions Cannot be done in Corporate Login');</script>");

                if (Request.QueryString["ConfirmMsg"].ToString().ToUpper() == "INVACCESS")
                    ClientScript.RegisterStartupScript(this.GetType(), "WarningMsg", "<script>alert('You Are Not Authorized to Access this Page');</script>");
            }
        }

        #endregion Events
    }
}
