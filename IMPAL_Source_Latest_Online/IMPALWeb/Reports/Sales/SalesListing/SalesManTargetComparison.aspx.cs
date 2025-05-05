using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using IMPALLibrary.Common;

namespace IMPALWeb.Reports.Sales.SalesListing
{
    public partial class SalesManTargetComparison : System.Web.UI.Page
    {
        private string strBranchCode = default(string);
        protected void Page_Init(object sender, EventArgs e)
        {
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["BranchCode"] != null)
                strBranchCode = Session["BranchCode"].ToString();
            if (!IsPostBack)
            {
                if (crSalesManTargetComparison != null)
                {
                    crSalesManTargetComparison.Dispose();
                    crSalesManTargetComparison = null;
                }

                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crSalesManTargetComparison != null)
            {
                crSalesManTargetComparison.Dispose();
                crSalesManTargetComparison = null;
            }
        }
        protected void crSalesManTargetComparison_Unload(object sender, EventArgs e)
        {
            if (crSalesManTargetComparison != null)
            {
                crSalesManTargetComparison.Dispose();
                crSalesManTargetComparison = null;
            }
        }
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Main mainmaster = (Main)Page.Master;
                mainmaster.ShowHideFilters(btnReport, reportFiltersTable, reportViewerHolder);
                if (btnReport.Text == "Back")
                {
                    string strFromDate = default(string);
                    string strToDate = default(string);
                    string strCryFromDate = default(string);
                    string strCryToDate = default(string);
                    //string strBranchCode=default(string);
                    string strSelectionFormula = default(string);
                    int intPrevFromYear = default(int);
                    int intPrevToYear = default(int);
                    bool blnFlag = false;
                    strFromDate = txtFromDate.Text;
                    strToDate = txtToDate.Text;

                    string strBranchCodeField = default(string);
                    string strDateField = default(string);
                    string strSupplierName = default(string);
                    string strReportName = default(string);
                    string strTransCode = default(string);
                    string strTempFromDate = default(string);
                    string strTempToDate = default(string);
                    string strTempToDatePrint = default(string);

                    Microsoft.Practices.EnterpriseLibrary.Data.Database ImpalDB = DataAccess.GetDatabase();

                    strCryFromDate = "Date (" + DateTime.ParseExact(strFromDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
                    strCryToDate = "Date (" + DateTime.ParseExact(strToDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";

                    intPrevFromYear = Convert.ToInt32(strFromDate.Substring(6)) - 1;
                    intPrevToYear = Convert.ToInt32(strToDate.Substring(6)) - 1;

                    if (Convert.ToInt32(strFromDate.Split(new char[] { '/' })[1]) < 4)
                        strTempFromDate = "Date (" + Convert.ToString(Convert.ToInt32(strFromDate.Substring(6)) - 1) + ",04,01)";
                    else
                        strTempFromDate = "Date (" + strFromDate.Substring(6) + ",04,01)";

                    if (Convert.ToInt32(strToDate.Split(new char[] { '/' })[1]) < 4)
                        strTempToDate = "Date (" + Convert.ToString(intPrevToYear - 1) + ",04,01)";
                    else
                        strTempToDate = "Date (" + Convert.ToString(intPrevToYear) + ",04,01)";

                    strTempToDatePrint = "Date (" + Convert.ToString(intPrevToYear) + "," + strToDate.Split(new char[] { '/' })[1] + "," + strToDate.Split(new char[] { '/' })[0] + ")";

                    strBranchCodeField = "{V_salesSalesman.branch_code}";
                    strDateField = "CDate ({V_SalesSalesman.Document_Date})";
                    strSupplierName = "{V_salesSalesman.supplier_name}";
                    strTransCode = "{V_salesSalesman.transaction_type_code}";
                    strReportName = "Sales_Man_Target_Sales_Comparison";

                    if (strBranchCode == "CRP")
                    {
                        if (ddlTransType.SelectedValue == "0" && ddlSuplinecode.SelectedValue == "0")
                            strSelectionFormula = "(((" + strDateField + " >= " + strTempFromDate + " and " + strDateField + " <= " + strCryToDate + ") OR (" + strDateField + " >= " + strTempToDate + " and " + strDateField + " <= " + strTempToDatePrint + ")))";
                        else if (ddlTransType.SelectedValue == "0" && ddlSuplinecode.SelectedValue != "0")
                            strSelectionFormula = "(" + strSupplierName + "='" + ddlSuplinecode.SelectedValue + "' and ((" + strDateField + " >= " + strTempFromDate + " and " + strDateField + " <= " + strCryToDate + ") OR (" + strDateField + " >= " + strTempToDate + " and " + strDateField + " <= " + strTempToDatePrint + ")))";
                        else if (ddlTransType.SelectedValue != "0" && ddlSuplinecode.SelectedValue == "0")
                            strSelectionFormula = "(" + strTransCode + "='" + ddlTransType.SelectedValue + "' and ((" + strDateField + " >= " + strTempFromDate + " and " + strDateField + " <= " + strCryToDate + ") OR (" + strDateField + " >= " + strTempToDate + " and " + strDateField + " <= " + strTempToDatePrint + ")))";
                        else if (ddlTransType.SelectedValue != "0" && ddlSuplinecode.SelectedValue != "0")
                            strSelectionFormula = "(" + strSupplierName + "='" + ddlSuplinecode.SelectedValue + "' and " + strTransCode + "= '" + ddlTransType.SelectedValue + "' and ((" + strDateField + " >= " + strTempFromDate + " and " + strDateField + " <= " + strCryToDate + ") OR (" + strDateField + " >= " + strTempToDate + " and " + strDateField + " <= " + strTempToDatePrint + ")))";
                    }
                    else
                    {
                        if (ddlTransType.SelectedValue == "0" && ddlSuplinecode.SelectedValue == "0")
                            strSelectionFormula = "(" + strBranchCodeField + "='" + strBranchCode + "' and ((" + strDateField + " >= " + strTempFromDate + " and " + strDateField + " <= " + strCryToDate + ") OR (" + strDateField + " >= " + strTempToDate + " and " + strDateField + " <= " + strTempToDatePrint + ")))";
                        else if (ddlTransType.SelectedValue == "0" && ddlSuplinecode.SelectedValue != "0")
                            strSelectionFormula = "(" + strBranchCodeField + "='" + strBranchCode + "' and " + strSupplierName + "='" + ddlSuplinecode.SelectedValue + "' and ((" + strDateField + " >= " + strTempFromDate + " and " + strDateField + " <= " + strCryToDate + ") OR (" + strDateField + " >= " + strTempToDate + " and " + strDateField + " <= " + strTempToDatePrint + ")))";
                        else if (ddlTransType.SelectedValue != "0" && ddlSuplinecode.SelectedValue == "0")
                            strSelectionFormula = "(" + strBranchCodeField + "='" + strBranchCode + "' and " + strTransCode + "='" + ddlTransType.SelectedValue + "' and ((" + strDateField + " >= " + strTempFromDate + " and " + strDateField + " <= " + strCryToDate + ") OR (" + strDateField + " >= " + strTempToDate + " and " + strDateField + " <= " + strTempToDatePrint + ")))";
                        else if (ddlTransType.SelectedValue != "0" && ddlSuplinecode.SelectedValue != "0")
                            strSelectionFormula = "(" + strBranchCodeField + "='" + strBranchCode + "' and " + strSupplierName + "='" + ddlSuplinecode.SelectedValue + "' and " + strTransCode + "= '" + ddlTransType.SelectedValue + "' and ((" + strDateField + " >= " + strTempFromDate + " and " + strDateField + " <= " + strCryToDate + ") OR (" + strDateField + " >= " + strTempToDate + " and " + strDateField + " <= " + strTempToDatePrint + ")))";
                    }

                    crSalesManTargetComparison.ReportName = strReportName;
                    crSalesManTargetComparison.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
                    crSalesManTargetComparison.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
                    crSalesManTargetComparison.RecordSelectionFormula = strSelectionFormula;
                    crSalesManTargetComparison.GenerateReport();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}
