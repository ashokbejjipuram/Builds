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


namespace IMPALWeb.Transactions.Finance.Payable
{
    public partial class CorpSupplierBranchInvoicePaid : System.Web.UI.Page
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
                    txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
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

        protected void btnReport_Click(object sender, EventArgs e)
        {
            PanelHeaderDtls.Enabled = false;

            Database ImpalDB = DataAccess.GetDatabaseBackUp();
            DbCommand cmd = null;

            cmd = ImpalDB.GetStoredProcCommand("Usp_GetSupplierInvoicePaid_Report_Crp");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, ddlBranch.SelectedValue);
            ImpalDB.AddInParameter(cmd, "@Supplier_Code", DbType.String, ddlSupplierCode.SelectedValue);
            ImpalDB.AddInParameter(cmd, "@From_Date", DbType.String, txtFromDate.Text);
            ImpalDB.AddInParameter(cmd, "@To_Date", DbType.String, txtToDate.Text);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);

            btnReportPDF.Attributes.Add("style", "display:inline");
            btnReportExcel.Attributes.Add("style", "display:inline");
            btnBack.Attributes.Add("style", "display:inline");
            btnReport.Attributes.Add("style", "display:none");

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

            string strSupplier = default(string);
            string strBranch = default(string);
            string strDocumentDate = default(string);
            strSupplierCode = ddlSupplierCode.SelectedValue;
            strBranchCode = ddlBranch.SelectedValue;

            strCryFromDate = "Date (" + DateTime.ParseExact(strFromDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
            strCryToDate = "Date (" + DateTime.ParseExact(strToDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";

            strSupplier = "{General_Ledger_Detail_Crp.Supplier_code}";
            strBranch = "{General_Ledger_Detail_Crp.Branch_Code}";
            strDocumentDate = "{General_Ledger_Detail_Crp.Document_Date}";

            if (strSupplierCode != "0" && strBranchCode != "0")
                strSelectionFormula = strSupplier + "='" + strSupplierCode + "' and " + strBranch + "='" + strBranchCode + "' and " + strDocumentDate + ">=" + strCryFromDate + "and " + strDocumentDate + "<=" + strCryToDate;
            else if (strSupplierCode != "0" && strBranchCode == "0")
                strSelectionFormula = strSupplier + "='" + strSupplierCode + "' and " + strDocumentDate + ">=" + strCryFromDate + " and " + strDocumentDate + "<=" + strCryToDate;
            else if (strSupplierCode == "0" && strBranchCode != "0")
                strSelectionFormula = strBranch + "='" + strBranchCode + "' and " + strDocumentDate + ">=" + strCryFromDate + "and " + strDocumentDate + "<=" + strCryToDate;
            else if (strSupplierCode == "0" && strBranchCode == "0")
                strSelectionFormula = strSupplier + "<>'' and " + strDocumentDate + ">=" + strCryFromDate + "and " + strDocumentDate + "<=" + strCryToDate;

            strSelectionFormula = strSelectionFormula + " and Mid({ General_Ledger_Detail_Crp.Chart_of_Account_code}, 7, 4)='0060'";

            crSupplierBranchInvoice.ReportName = "CorpSupplierBranchInvoicePaid";
            crSupplierBranchInvoice.CrystalFormulaFields.Add("fromdate", "\"" + txtFromDate.Text + "\"");
            crSupplierBranchInvoice.CrystalFormulaFields.Add("todate", "\"" + txtToDate.Text + "\"");
            crSupplierBranchInvoice.RecordSelectionFormula = strSelectionFormula;
            crSupplierBranchInvoice.GenerateReportAndExportA4HO(fileType);
        }
        #endregion
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Server.ClearError();
                Response.Redirect("CorpSupplierBranchInvoicePaid.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}
