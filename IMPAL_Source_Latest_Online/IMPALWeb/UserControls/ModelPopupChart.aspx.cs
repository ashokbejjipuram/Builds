using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IMPALWeb.UserControls
{
    public partial class ModelPopupChart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AshxPath.Value = Page.ResolveClientUrl("~/HandlerFile");
            string Filter = Request.QueryString["Filter"].ToString();
            string DefaultBranch = Request.QueryString["DefaultBranch"].ToString();
            string BranchCode = Request.QueryString["BranchCode"].ToString();
            hddFilter.Value = Filter;
            hddDefaultBranch.Value = DefaultBranch;
            hddBranch.Value = BranchCode;
        }
    }
}
