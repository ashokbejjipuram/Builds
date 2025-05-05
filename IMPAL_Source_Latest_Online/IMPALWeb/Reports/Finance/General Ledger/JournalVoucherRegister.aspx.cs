#region Namespace Declaration
using System;
using System.Collections.Generic;
using IMPALLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using IMPALLibrary;
using System.Data.Common;
using System.Data;
using System.Web.UI;
#endregion

namespace IMPALWeb.Reports.Finance.General_Ledger
{
    public partial class JournalVoucherRegister : System.Web.UI.Page
    {
        string sessionBranch = string.Empty;
        #region Page Init
        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "Page_Init", "JournalVoucherRegister Page Init Method"); 
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
            //Log.WriteLog(Source, "Page_Load", "JournalVoucherRegister Page Load Method"); 
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
                    fnPopulateReportType();
                }

                if (Session["BranchCode"] != null)
                    sessionBranch = Session["BranchCode"].ToString();
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

        #region Populate Report Type method
        protected void fnPopulateReportType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "fnPopulateReportType()", "JournalVoucherRegister Report Type Populate method"); 
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                ddlreportType.DataSource = oCommon.GetDropDownListValues("ReportType-DtlSumm");
                ddlreportType.DataTextField = "DisplayText";
                ddlreportType.DataValueField = "DisplayValue";
                ddlreportType.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

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
            string selectionformula = string.Empty;
            string strselDocDate = "{jv_header.jv_date}";
            string strselBranchCode = "{jv_header.branch_code}";

            string strFromDate = txtFromDate.Text;
            string strToDate = txtToDate.Text;            

            var FromDate = txtFromDate.Text.Split('/');
            var ToDate = txtToDate.Text.Split('/');

            string Dfrom_Param = string.Format("Date({0},{1},{2})", FromDate[2], FromDate[1], FromDate[0]);
            string Dto_Param = string.Format("Date({0},{1},{2})", ToDate[2], ToDate[1], ToDate[0]);

            selectionformula = strselDocDate + ">=" + Dfrom_Param + " and " + strselDocDate + "<=" + Dto_Param + " and " + strselBranchCode + "='" + sessionBranch + "'";

            crDebitCredit.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
            crDebitCredit.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
            crDebitCredit.RecordSelectionFormula = selectionformula;

            if (ddlreportType.SelectedValue == "Detail")
            {
                crDebitCredit.ReportName = "impal-reports-journalvoucher";
                crDebitCredit.GenerateReportAndExportHO(fileType);
            }
            else if (ddlreportType.SelectedValue == "Summary")
            {
                crDebitCredit.ReportName = "impal-reports-journalvoucher-summ";
                crDebitCredit.GenerateReportAndExportA4HO(fileType);
            }            
        }
        #endregion
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Server.ClearError();
                Response.Redirect("JournalVoucherRegister.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}