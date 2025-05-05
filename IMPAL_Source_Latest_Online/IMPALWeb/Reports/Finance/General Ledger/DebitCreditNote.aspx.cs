#region Namespace Declaration
using System;
using Microsoft.Practices.EnterpriseLibrary.Data;
using IMPALLibrary;
using System.Data.Common;
using System.Data;
#endregion

namespace IMPALWeb.Reports.Finance.General_Ledger
{
    public partial class DebitCreditNote : System.Web.UI.Page
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
                    if (crDebitCredit != null)
                    {
                        crDebitCredit.Dispose();
                        crDebitCredit = null;
                    }

                    txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtToDate.Text = txtFromDate.Text;
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
            if (crDebitCredit != null)
            {
                crDebitCredit.Dispose();
                crDebitCredit = null;
            }
        }
        protected void crDebitCredit_Unload(object sender, EventArgs e)
        {
            if (crDebitCredit != null)
            {
                crDebitCredit.Dispose();
                crDebitCredit = null;
            }
        }

        #region Report Button Click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            string sessionBranch = string.Empty;
            string selectionformula = string.Empty;
            string strselDocDate = string.Empty;
            string strselBranchCode = string.Empty;

            if (Session["BranchCode"] != null)
            {
                sessionBranch = (string)Session["BranchCode"];
            }

            string strFromDate = txtFromDate.Text;
            string strToDate = txtToDate.Text;
            if (ddlReportType.SelectedIndex == 0)
            {
                strselDocDate = "{v_DebitCreditNote_New.Document_Date}";
                strselBranchCode = "{v_DebitCreditNote_New.Branch_Code}";
                crDebitCredit.ReportName = "DebitCreditNote_New";
            }
            else
            {
                strselDocDate = "{DebitCreditAdvice.Document_Date}";
                strselBranchCode = "{DebitCreditAdvice.Branch_Code}";

                if (ddlReportType.SelectedIndex == 1)
                    crDebitCredit.ReportName = "DebitCreditAdvice1";
                else if (ddlReportType.SelectedIndex == 2)
                    crDebitCredit.ReportName = "DebitCreditAdvice_Int";
                else
                    crDebitCredit.ReportName = "DebitCreditAdvice_Cust";
            }

            var FromDate = txtFromDate.Text.Split('/');
            var ToDate = txtToDate.Text.Split('/');

            string Dfrom_Param = string.Format("Date({0},{1},{2})", FromDate[2], FromDate[1], FromDate[0]);
            string Dto_Param = string.Format("Date({0},{1},{2})", ToDate[2], ToDate[1], ToDate[0]);

            if (sessionBranch == "CRP")
                selectionformula = strselDocDate + ">=" + Dfrom_Param + " and " + strselDocDate + "<=" + Dto_Param;
            else
                selectionformula = strselDocDate + ">=" + Dfrom_Param + " and " + strselDocDate + "<=" + Dto_Param + " and " + strselBranchCode + "='" + sessionBranch + "'";

            crDebitCredit.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
            crDebitCredit.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
            crDebitCredit.RecordSelectionFormula = selectionformula;
            crDebitCredit.GenerateReportAndExportA4();
        }
        #endregion
    }
}

