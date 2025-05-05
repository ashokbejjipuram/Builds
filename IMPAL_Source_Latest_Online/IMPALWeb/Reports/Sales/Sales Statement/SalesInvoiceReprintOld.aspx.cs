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
using System.Data.Common;
using System.Drawing.Printing;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using IMPALLibrary.Transactions;
#endregion

namespace IMPALWeb.Reports.Sales.Sales_Statement
{
    public partial class SalesInvoiceReprintOld : System.Web.UI.Page
    {
        #region Page Init
        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!IsPostBack)
                {
                    if (crySalesInvoiceReprint != null)
                    {
                        crySalesInvoiceReprint.Dispose();
                        crySalesInvoiceReprint = null;
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crySalesInvoiceReprint != null)
            {
                crySalesInvoiceReprint.Dispose();
                crySalesInvoiceReprint = null;
            }
        }
        protected void crySalesInvoiceReprint_Unload(object sender, EventArgs e)
        {
            if (crySalesInvoiceReprint != null)
            {
                crySalesInvoiceReprint.Dispose();
                crySalesInvoiceReprint = null;
            }
        }

        #region Generate Selection Formula
        public void GenerateSelectionFormula()
        {
            string strSelectionFormula = default(string);
            string strBrchField = default(string);
            string strInvField = default(string);
            string strInvValue = default(string);
            string strReportName = default(string);

            strBrchField = "{V_Invoice.Branch_Code}";
            strInvField = "{V_Invoice.Document_number}";
            strInvValue = txtInvoiceNum.Text;
            strSelectionFormula = strBrchField + "='" + Session["BranchCode"].ToString() + "' and " + strInvField + "= '" + strInvValue + "'";
            strReportName = "po_pp_invoice_invGST";

            crySalesInvoiceReprint.ReportName = strReportName;
            crySalesInvoiceReprint.RecordSelectionFormula = strSelectionFormula;
            crySalesInvoiceReprint.GenerateReportAndExportInvoiceA4(strInvValue.Replace("/", "-"), 2);
        }
        #endregion

        #region ReportButton Click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmdtemps = ImpalDB.GetStoredProcCommand("usp_GetDocumentRePrint_Old");
            ImpalDB.AddInParameter(cmdtemps, "@Branch_Code", DbType.String, Session["BranchCode"].ToString());
            ImpalDB.AddInParameter(cmdtemps, "@Document_Number", DbType.String, txtInvoiceNum.Text.ToString());
            ImpalDB.AddInParameter(cmdtemps, "@Indicator", DbType.String, "I");
            cmdtemps.CommandTimeout = ConnectionTimeOut.TimeOut;
            var signedQRCode = ImpalDB.ExecuteScalar(cmdtemps).ToString();

            if (signedQRCode == "0")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Invoice Reprint is not available');", true);
                txtInvoiceNum.Text = "";
            }
            else
            {
                GenerateQRcode genQRcode = new GenerateQRcode();
                SalesTransactions salesTransactions = new SalesTransactions();
                byte[] imageData;

                if (signedQRCode.ToString() != "")
                {
                    if (signedQRCode.ToString().Substring(0, 4) == "upi:")
                        imageData = genQRcode.GenerateInvQRCodeB2C(signedQRCode.ToString().Trim());
                    else
                        imageData = genQRcode.GenerateInvQRCode(signedQRCode.ToString().Trim());

                    salesTransactions.updEinvoiceIRNDetails(Session["BranchCode"].ToString(), txtInvoiceNum.Text, imageData);
                }

                GenerateSelectionFormula();
            }
        }
        #endregion
    }
}
