using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IMPALLibrary;
using IMPALLibrary.Transactions.Finance;
using IMPALLibrary.Masters.Sales;
using IMPALWeb.UserControls;
using System.Web.UI.HtmlControls;
using IMPALLibrary.Masters;

namespace IMPALWeb.HOAdmin.Finance
{
    public partial class VendorBooking : System.Web.UI.Page
    {
        #region Declaration
        string strBranchCode = string.Empty;
        #endregion

        #region Page Init
        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {              
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!IsPostBack)
                {
                    ddlBranch.Enabled = true;
                    Branches oBranch = new Branches();
                    ddlBranch.DataSource = oBranch.GetAllBranchesAdmin();
                    ddlBranch.DataBind();
                    ddlBranch.SelectedIndex = 0;
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region ddlBranch_IndexChanged
        protected void ddlBranch_IndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (ddlBranch.SelectedIndex > 0)
                {
                    Session["BranchCode"] = ddlBranch.SelectedValue;
                    Session["BranchName"] = ddlBranch.SelectedItem.Text;
                    Server.ClearError();
                    Response.Redirect("~/Transactions/Finance/CashAndBank/VendorBooking.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion
    }
}
