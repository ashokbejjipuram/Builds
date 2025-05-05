#region namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
#endregion
namespace IMPALWeb.Masters.SalesTax
{
    public partial class BranchProductAndSalesTaxReport : System.Web.UI.Page
    {
        #region Page Init
        /// <summary>
        /// To initialize page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Init", "Entering Page_Init");
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }
        #endregion
        #region Page Load
        /// <summary>
        /// To Load page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Load", "Entering Page_Load");
            try
            {
                if (!IsPostBack && PreviousPage!=null)
                {
                    crBranchProductSalesTax.ReportName = "Impal_Salestax_Branch_Product";
                    string strSelectionFormula = string.Empty;
                    string strBranchCode = "{Branch_Master.Branch_Code}";
                    string strBranchCodeVal = (string)Session["BranchCode"];
                    if (strBranchCodeVal != string.Empty && strBranchCodeVal != "CRP")
                    {
                        strSelectionFormula = strBranchCode + "=" + " '" + strBranchCodeVal + "'";
                    }
                    crBranchProductSalesTax.RecordSelectionFormula = strSelectionFormula;
                    crBranchProductSalesTax.GenerateReport();
                    if (crBranchProductSalesTax.Visible == false)
                        crBranchProductSalesTax.Visible = true;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }


        }
        #endregion
        #region btnBack_Click
        /// <summary>
        /// To navigate to previous page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnBack_Click", "Entering btnBack_Click");
            try
            {
                Server.ClearError();
                Response.Redirect("BranchProductAndSalesTax.aspx", false);
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
