#region namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using IMPALLibrary;
using System.Web.UI.WebControls;
#endregion
namespace IMPALWeb.Masters.SalesMan
{
    public partial class SalesManTargetReport : System.Web.UI.Page
    {
        #region Page_Init
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
        /// To Load Page .Report is generated when loading
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Load", "Entering Page_Load");
            try
            {
                if (!IsPostBack && PreviousPage !=null)
                {
                    crSalesManTarget.ReportName = "Impal_Sales_Man_Target";
                    string strCode = "{Sales_Man_Target.Sales_Man_Code}";
                    Control placeHolder = PreviousPage.Controls[0].FindControl("CPHDetails");
                    DropDownList SourceDDl = (DropDownList)placeHolder.FindControl("drpSalesMan1");
                    string strCodeVal = SourceDDl.SelectedValue;
                    // string strCodeVal = (string)Request.QueryString["Code"];
                    string strSelectionFormula = string.Empty;
                    if (strCodeVal != string.Empty && strCodeVal != "ALL")
                    {
                        strSelectionFormula = strCode + "=" + " '" + strCodeVal + "'";
                    }
                    crSalesManTarget.RecordSelectionFormula = strSelectionFormula;
                    crSalesManTarget.GenerateReport();
                    if (crSalesManTarget.Visible == false)
                        crSalesManTarget.Visible = true;
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
                Response.Redirect("SalesManTarget.aspx", false);
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
