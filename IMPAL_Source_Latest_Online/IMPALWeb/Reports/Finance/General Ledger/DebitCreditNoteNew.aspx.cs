#region Namespace Declaration
using System;
using Microsoft.Practices.EnterpriseLibrary.Data;
using IMPALLibrary;
using System.Data.Common;
using System.Data;
using IMPALLibrary.Common;
using IMPALLibrary.Transactions;
#endregion

namespace IMPALWeb.Reports.Finance.General_Ledger
{
    public partial class DebitCreditNoteNew : System.Web.UI.Page
    {
        #region Page Init
        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "Page_Init", "Debit Credit Note Page Init Method");
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
            //Log.WriteLog(Source, "Page_Load", "Debit Credit Note Page Load Method");
            try
            {
                if (!IsPostBack)
                {
                    if (crDebitCreditNew != null)
                    {
                        crDebitCreditNew.Dispose();
                        crDebitCreditNew = null;
                    }
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crDebitCreditNew != null)
            {
                crDebitCreditNew.Dispose();
                crDebitCreditNew = null;
            }
        }
        protected void crDebitCreditNew_Unload(object sender, EventArgs e)
        {
            if (crDebitCreditNew != null)
            {
                crDebitCreditNew.Dispose();
                crDebitCreditNew = null;
            }
        }

        #region Report Button Click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            string sessionBranch = string.Empty;
            string selectionformula = string.Empty;            
            string strselBranchCode = string.Empty;
            string strselDocNo = string.Empty;

            if (Session["BranchCode"] != null)
                sessionBranch = (string)Session["BranchCode"];

            if (ddlReportType.SelectedIndex == 0)
            {
                strselBranchCode = "{v_DebitCreditNote_New.Branch_Code}";
                strselDocNo = "{v_DebitCreditNote_New.Document_Number}";
                crDebitCreditNew.ReportName = "DebitCreditNote_New";
            }
            else
            {
                strselBranchCode = "{DebitCreditAdvice.Branch_Code}";
                strselDocNo = "{DebitCreditAdvice.Document_Number}";

                if (ddlReportType.SelectedIndex == 1)
                    crDebitCreditNew.ReportName = "DebitCreditAdvice1";
                else if (ddlReportType.SelectedIndex == 2)
                    crDebitCreditNew.ReportName = "DebitCreditAdvice_Int";
                else
                    crDebitCreditNew.ReportName = "DebitCreditAdvice_Cust";
            }

            Microsoft.Practices.EnterpriseLibrary.Data.Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmdtemps = ImpalDB.GetStoredProcCommand("usp_GetDocumentRePrint_Old");
            ImpalDB.AddInParameter(cmdtemps, "@Branch_Code", DbType.String, Session["BranchCode"].ToString());
            ImpalDB.AddInParameter(cmdtemps, "@Document_Number", DbType.String, txtDocumentNo.Text.ToString());
            ImpalDB.AddInParameter(cmdtemps, "@Indicator", DbType.String, "D");
            cmdtemps.CommandTimeout = ConnectionTimeOut.TimeOut;
            var signedQRCode = ImpalDB.ExecuteScalar(cmdtemps).ToString();

            if (signedQRCode.ToString() != "")
            {
                GenerateQRcode genQRcode = new GenerateQRcode();
                SalesTransactions salesTransactions = new SalesTransactions();
                byte[] imageData = genQRcode.GenerateInvQRCode(signedQRCode.ToString().Trim());

                salesTransactions.updEinvoiceIRNDetails(Session["BranchCode"].ToString(), txtDocumentNo.Text, imageData);
            }

            selectionformula = strselBranchCode + "='" + sessionBranch + "' and " + strselDocNo + "='" + txtDocumentNo.Text + "'";

            crDebitCreditNew.RecordSelectionFormula = selectionformula;
            crDebitCreditNew.GenerateReportAndExportA4();
        }
        #endregion

    }

}

