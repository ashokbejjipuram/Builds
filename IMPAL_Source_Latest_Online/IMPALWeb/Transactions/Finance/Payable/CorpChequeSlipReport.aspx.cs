using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.ServiceLocation;
using IMPALLibrary.Common;

namespace IMPALWeb.Transactions.Finance.Payable
{
    public partial class CorpChequeSlipReport : System.Web.UI.Page
    {
        string pstrBranchCode = default(string);
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (Session["BranchCode"] != null)
                    pstrBranchCode = Session["BranchCode"].ToString();

                if (!IsPostBack)
                {
                    if (crChequeSlipReport != null)
                    {
                        crChequeSlipReport.Dispose();
                        crChequeSlipReport = null;
                    }

                    txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    ddlBranch.SelectedValue = pstrBranchCode;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(source, ex);
            }
        }
        #endregion

        #region Page Init
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
        #endregion

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crChequeSlipReport != null)
            {
                crChequeSlipReport.Dispose();
                crChequeSlipReport = null;
            }
        }
        protected void crChequeSlipReport_Unload(object sender, EventArgs e)
        {
            if (crChequeSlipReport != null)
            {
                crChequeSlipReport.Dispose();
                crChequeSlipReport = null;
            }
        }

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

        #region Report Button Click
        protected void btnreport_Click(object sender, EventArgs e)
        {
            PanelHeaderDtls.Enabled = false;
            btnReportPDF.Attributes.Add("style", "display:inline");
            btnReportExcel.Attributes.Add("style", "display:inline");
            btnBack.Attributes.Add("style", "display:inline");
            btnreport.Attributes.Add("style", "display:none");

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Report Has been Generated Successfully. Please Click on Below Buttons to Export in Respective Formats');", true);
        }

        protected void GenerateAndExportReport(string fileType)
        {
            string strFromDate = default(string);
            string strToDate = default(string);
            string strCryFromDate = default(string);
            string strCryToDate = default(string);
            string strSelectionFormula = default(string);

            strFromDate = txtFromDate.Text;
            strToDate = txtToDate.Text;

            string strSupplierCode = default(string);
            string strBranchCode = default(string);
            string selectionFormula = default(string);

            string strSupplier = default(string);
            string strBranch = default(string);
            string strChequeSlipDate = default(string);
            strSupplierCode = ddlSupplierCode.SelectedValue;
            strBranchCode = ddlBranch.SelectedValue;

            strCryFromDate = "Date (" + DateTime.ParseExact(strFromDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
            strCryToDate = "Date (" + DateTime.ParseExact(strToDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";

            strSupplier = "{Cheque_Slip_Header.Supplier_Code}";
            strBranch = "{Cheque_Slip_Header.Branch_code}";
            strChequeSlipDate = "{Cheque_Slip_Header.Cheque_Slip_Date}";

            if (strSupplierCode != "ALL" && strBranchCode != "ALL")
                strSelectionFormula = strSupplier + "='" + strSupplierCode + "' and " + strBranch + "='" + strBranchCode + "' and " + strChequeSlipDate + ">=" + strCryFromDate + "and " + strChequeSlipDate + "<=" + strCryToDate;
            else if (strSupplierCode != "ALL" && strBranchCode == "ALL")
                selectionFormula = strSupplier + "='" + strSupplierCode + "' and " + strChequeSlipDate + ">=" + strCryFromDate + " and " + strChequeSlipDate + "<=" + strCryToDate;
            else if (strSupplierCode == "ALL" && strBranchCode != "ALL")
                selectionFormula = strBranch + "='" + strBranchCode + "' and " + strChequeSlipDate + ">=" + strCryFromDate + "and " + strChequeSlipDate + "<=" + strCryToDate;
            else if (strSupplierCode == "ALL" && strBranchCode == "ALL")
                selectionFormula = strSupplier + "<>'' and " + strChequeSlipDate + ">=" + strCryFromDate + "and " + strChequeSlipDate + "<=" + strCryToDate;

            crChequeSlipReport.ReportName = "CorpChequeSlipReport";
            crChequeSlipReport.CrystalFormulaFields.Add("fromdate", "\"" + txtFromDate.Text + "\"");
            crChequeSlipReport.CrystalFormulaFields.Add("todate", "\"" + txtToDate.Text + "\"");
            crChequeSlipReport.RecordSelectionFormula = strSelectionFormula;
            crChequeSlipReport.GenerateReportAndExportA4();
        }
        #endregion

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Server.ClearError();
                Response.Redirect("CorpChequeSlipReport.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}