#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Common;
using IMPALLibrary;
using System.Diagnostics;
using System.Drawing.Printing;
using CrystalDecisions.CrystalReports.Engine;
#endregion

namespace IMPALWeb.Reports.Finance.Accounts_Payable
{
    public partial class BMSPaymentReport : System.Web.UI.Page
    {

        /// <summary>
        /// Page Init event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, EventArgs e)
        {
            Type source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
            }
            catch (Exception ex)
            {
                Log.WriteException(source, ex);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Type source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                if (!IsPostBack)
                {
                    if (crBMSPaymentReport != null)
                    {
                        crBMSPaymentReport.Dispose();
                        crBMSPaymentReport = null;
                    }

                    if (Request.QueryString["ChequeSlipNumber"] != null)
                    {
                        fnGenerateReport();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(source, ex);
            }
        }
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crBMSPaymentReport != null)
            {
                crBMSPaymentReport.Dispose();
                crBMSPaymentReport = null;
            }
        }
        protected void crBMSPaymentReport_Unload(object sender, EventArgs e)
        {
            if (crBMSPaymentReport != null)
            {
                crBMSPaymentReport.Dispose();
                crBMSPaymentReport = null;
            }
        }

        protected void fnGenerateReport()
        {
            Type source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            string strSelectionFormula = default(string);
            string strField = default(string);
            string strValue = default(string);
            try
            {
                strField = "{Cheque_Slip_Header.Cheque_Slip_Number}";
                strValue = Request.QueryString["ChequeSlipNumber"].ToString();
                strSelectionFormula = strField + "= " + "'" + strValue + "'";
                crBMSPaymentReport.ReportName = "BMSPaymentReport";
                crBMSPaymentReport.RecordSelectionFormula = strSelectionFormula;
                crBMSPaymentReport.GenerateReport();
            }
            catch (Exception Exp)
            {
                Log.WriteException(source, Exp);
            }
        }

        #region Back Button Click
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnBack_Click", "Back Button Clicked");
            try
            {
                Server.ClearError();
                Response.Redirect("~/Transactions/Finance/Payable/BMSPayment.aspx", false);
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
