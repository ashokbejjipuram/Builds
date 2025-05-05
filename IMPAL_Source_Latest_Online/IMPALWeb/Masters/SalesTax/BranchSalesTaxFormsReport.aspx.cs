#region namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using IMPALLibrary;
using System.Web.UI.WebControls;
#endregion
namespace IMPALWeb.Masters.SalesTax
{
    public partial class BranchSalesTaxFormsReport : System.Web.UI.Page
    {
        #region Page Init
        /// <summary>
        /// To initialize page.
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
        /// ToLoad report page.Report is generated inside this method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source=System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Load", "Entering Page_Load");
            try
            {

                if (!IsPostBack && PreviousPage!=null)
                {
                    crBranchSalesTaxFormsReport.ReportName = "Impal_branchSTform";
                    string strCode = "{branch_ST_forms.Serial_Number}";
                    Control placeHolder = PreviousPage.Controls[0].FindControl("CPHDetails");
                    DropDownList SourceDDl = (DropDownList)placeHolder.FindControl("drpBSTDesc");
                    string strCodeVal = SourceDDl.SelectedValue;
                    //string strCodeVal = (string)Request.QueryString["Code"];
                    string strSelectionFormula = string.Empty;
                    if (strCodeVal != string.Empty && strCodeVal != "ALL")
                    {
                        strSelectionFormula = strCode + "=" + strCodeVal;
                    }
                    crBranchSalesTaxFormsReport.RecordSelectionFormula = strSelectionFormula;
                    crBranchSalesTaxFormsReport.GenerateReport();
                    if (crBranchSalesTaxFormsReport.Visible == false)
                        crBranchSalesTaxFormsReport.Visible = true;

                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            
            }
        }
        #endregion
        #region Back button click event
        /// <summary>
        /// To redirect to previous page
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
                Response.Redirect("BranchSalesTaxForms.aspx", false);
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
