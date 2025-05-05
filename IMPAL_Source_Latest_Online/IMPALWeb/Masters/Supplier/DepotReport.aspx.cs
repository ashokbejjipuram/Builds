#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
#endregion

namespace IMPALWeb.Masters.Supplier
{
    public partial class DepotReport : System.Web.UI.Page
    {

        #region page init
        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Init", "Inside Page_Init");
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region page load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Load", "Inside Page_Load");
            try
            {
                if (!IsPostBack && PreviousPage != null)
                {
                    Control placeHolder = PreviousPage.Controls[0].FindControl("CPHDetails");
                    crSupplierDepot.ReportName = "SupplierDepot";
                    crSupplierDepot.RecordSelectionFormula = "";
                    crSupplierDepot.GenerateReport();
                    if (crSupplierDepot.Visible == false)
                        crSupplierDepot.Visible = true;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region back button click
        protected void btnDepotBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnDepotBack_Click", "Inside btnDepotBack_Click");
            try
            {
                Server.ClearError();
                Response.Redirect("Depot.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }

            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion
    }
}
