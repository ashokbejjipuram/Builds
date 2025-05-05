#region declaration
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using IMPALLibrary;
using System.Web.UI.WebControls;
#endregion
namespace IMPALWeb.Masters.Supplier
{
    public partial class SupplierLineReport : System.Web.UI.Page
    {
        #region Page Init
        /// <summary>
        /// To initialize Page
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
        /// To Load page.Report is generated during page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Load", "Entering Page_Load");
            try
            {
                if (!IsPostBack & PreviousPage!=null)
                {
                    crSupplierLine.ReportName = "Impal_Supplier_Line";
                    string strLineCode = "{Supplier_Line_Master.Supplier_Line_Code}";
                    Control placeHolder = PreviousPage.Controls[0].FindControl("CPHDetails");
                    DropDownList SourceText = (DropDownList)placeHolder.FindControl("drpSupplierLine");
                    string strLineCodeVal = SourceText.SelectedValue;
                    //string strLineCodeVal = (string)Request.QueryString["LineCode"];
                    string strSelectionFormula = string.Empty;
                    if (strLineCodeVal != string.Empty)
                    {
                        strSelectionFormula = strLineCode + "=" + " '" + strLineCodeVal + "'";
                    }
                    crSupplierLine.RecordSelectionFormula = strSelectionFormula;
                    crSupplierLine.GenerateReport();
                    if (crSupplierLine.Visible == false)
                        crSupplierLine.Visible = true;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }


        }
        #endregion
        #region Back Button Click
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
                Response.Redirect("SupplierLine.aspx", false);
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
