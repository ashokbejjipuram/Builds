#region namesapce
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
    public partial class SalesManReport : System.Web.UI.Page
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
        /// To Load Page .Report is generated when loading
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Load", "Entering Page_Load");
            string strBranchCode = Session["BranchCode"].ToString();
            try
            {
                if (!IsPostBack && PreviousPage!=null)
                {
                    crSalesMan.ReportName = "Impal_Sales_Man";
                    string strCode = "{Sales_Man_Master.Sales_Man_Code}";
                    string strbrcode = "{Branch_Master.Branch_Code}";
                    Control placeHolder = PreviousPage.Controls[0].FindControl("CPHDetails");
                    DropDownList SourceDDl = (DropDownList)placeHolder.FindControl("drpSTDesc");
                    string strCodeVal = SourceDDl.SelectedValue;
                    //string strCodeVal = (string)Request.QueryString["Code"];
                    string strSelectionFormula = string.Empty;

                    if (strBranchCode.ToUpper() != "CRP")
                    {
                        strSelectionFormula = strbrcode + " = '" + strBranchCode + "'";
                    }                                        

                    if (strCodeVal != string.Empty && strCodeVal != "ALL")
                    {
                        strSelectionFormula = strSelectionFormula + " and " + strCode + "=" + " '" + strCodeVal + "'";
                    }

                    crSalesMan.RecordSelectionFormula = strSelectionFormula;
                    crSalesMan.GenerateReport();
                    if (crSalesMan.Visible == false)
                        crSalesMan.Visible = true;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }
        #endregion
        #region Button Back click
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
                Response.Redirect("SalesMan.aspx", false);
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
