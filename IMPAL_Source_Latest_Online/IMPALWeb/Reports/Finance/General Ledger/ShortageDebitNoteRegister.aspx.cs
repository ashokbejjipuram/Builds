#region Namespace Declaration
using System;
using Microsoft.Practices.EnterpriseLibrary.Data;
using IMPALLibrary;
using System.Data.Common;
using System.Data;
using System.Web.UI;
#endregion

namespace IMPALWeb.Reports.Finance.General_Ledger
{
    public partial class ShortageDebitNoteRegister : System.Web.UI.Page
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
                    if (crShortageDebitNoteRegister != null)
                    {
                        crShortageDebitNoteRegister.Dispose();
                        crShortageDebitNoteRegister = null;
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
            if (crShortageDebitNoteRegister != null)
            {
                crShortageDebitNoteRegister.Dispose();
                crShortageDebitNoteRegister = null;
            }
        }
        protected void crShortageDebitNoteRegister_Unload(object sender, EventArgs e)
        {
            if (crShortageDebitNoteRegister != null)
            {
                crShortageDebitNoteRegister.Dispose();
                crShortageDebitNoteRegister = null;
            }
        }

        #region Report Button Click
        protected void btnReportPDF_Click(object sender, EventArgs e)
        {
            PanelHeaderDtls.Enabled = false;
            GenerateAndExportReport(".pdf");
        }
        protected void btnReportExcel_Click(object sender, EventArgs e)
        {
            PanelHeaderDtls.Enabled = false;
            GenerateAndExportReport(".xls");
        }

        protected void btnReportRTF_Click(object sender, EventArgs e)
        {
            PanelHeaderDtls.Enabled = false;
            GenerateAndExportReport(".doc");
        }
        protected void btnReport_Click(object sender, EventArgs e)
        {
            PanelHeaderDtls.Enabled = false;
            btnReportPDF.Attributes.Add("style", "display:inline");
            btnReportExcel.Attributes.Add("style", "display:inline");
            btnReportRTF.Attributes.Add("style", "display:inline");
            btnBack.Attributes.Add("style", "display:inline");
            btnReport.Attributes.Add("style", "display:none");

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Report Has been Generated Successfully. Please Click on Below Buttons to Export in Respective Formats');", true);
        }
        protected void GenerateAndExportReport(string fileType)
        {
            string sessionBranch = string.Empty;
            string selectionformula = string.Empty;
            string strselDocDate = string.Empty;
            string strselBranchCode = string.Empty;

            strselDocDate = "{Grn_Discrepency.Inward_Date}";
            strselBranchCode = "{Grn_Discrepency.Branch_Code}=";

            sessionBranch = (string)Session["BranchCode"];

            string strFromDate = txtFromDate.Text;
            string strToDate = txtToDate.Text;

            var FromDate = txtFromDate.Text.Split('/');
            var ToDate = txtToDate.Text.Split('/');

            string Dfrom_Param = string.Format("Date({0},{1},{2})", FromDate[2], FromDate[1], FromDate[0]);
            string Dto_Param = string.Format("Date({0},{1},{2})", ToDate[2], ToDate[1], ToDate[0]);

            selectionformula = strselDocDate + ">=" + Dfrom_Param + " and " + strselDocDate + "<=" + Dto_Param + " and " + strselBranchCode + "'" + sessionBranch + "'";

            crShortageDebitNoteRegister.ReportName = "SupplyShortageRegister";
            crShortageDebitNoteRegister.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
            crShortageDebitNoteRegister.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
            crShortageDebitNoteRegister.RecordSelectionFormula = selectionformula;
            crShortageDebitNoteRegister.GenerateReportAndExport(fileType);
        }
        #endregion

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Server.ClearError();
                Response.Redirect("ShortageDebitNoteRegister.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}