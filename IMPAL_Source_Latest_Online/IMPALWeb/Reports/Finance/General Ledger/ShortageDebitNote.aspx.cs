#region Namespace Declaration
using System;
using Microsoft.Practices.EnterpriseLibrary.Data;
using IMPALLibrary;
using System.Data.Common;
using System.Data;
using System.Web.UI;
using IMPALLibrary.Common;
using IMPALLibrary.Transactions;
#endregion

namespace IMPALWeb.Reports.Finance.General_Ledger
{
    public partial class ShortageDebitNote : System.Web.UI.Page
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
                    if (crShortageDebitNote != null)
                    {
                        crShortageDebitNote.Dispose();
                        crShortageDebitNote = null;
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
            if (crShortageDebitNote != null)
            {
                crShortageDebitNote.Dispose();
                crShortageDebitNote = null;
            }
        }
        protected void crShortageDebitNote_Unload(object sender, EventArgs e)
        {
            if (crShortageDebitNote != null)
            {
                crShortageDebitNote.Dispose();
                crShortageDebitNote = null;
            }
        }

        #region Report Button Click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            string sessionBranch = string.Empty;
            string selectionformula = string.Empty;
            string strselDocNumber = string.Empty;
            string strselBranchCode = string.Empty;

            strselBranchCode = "{v_DebitNoteShortage.Branch_Code}";
            strselDocNumber = "{v_DebitNoteShortage.Debit_Advice_Number}";
            sessionBranch = (string)Session["BranchCode"];

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

            selectionformula = strselBranchCode + "='" + sessionBranch + "' and " + strselDocNumber + "='" + txtDocumentNo.Text + "'";

            crShortageDebitNote.ReportName = "DebitNote_Shortage";
            crShortageDebitNote.RecordSelectionFormula = selectionformula;
            crShortageDebitNote.GenerateReportAndExportA4();
        }
        #endregion

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Server.ClearError();
                Response.Redirect("ShortageDebitNote.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}