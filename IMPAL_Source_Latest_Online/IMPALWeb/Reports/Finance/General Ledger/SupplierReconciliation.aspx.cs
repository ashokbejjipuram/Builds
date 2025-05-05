#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Common;
using IMPALLibrary.Masters.Sales;
using IMPALLibrary.Masters.CustomerDetails;
using System.Threading;
#endregion

namespace IMPALWeb.Reports.Finance.General_Ledger
{
    public partial class SupplierReconciliation : System.Web.UI.Page
    {
        #region Declaration
        string strBranchCode = string.Empty;
        #endregion

        #region Page_Init
        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
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
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = (string)Session["BranchCode"];

                if (!IsPostBack)
                {
                    if (rptCrystal != null)
                    {
                        rptCrystal.Dispose();
                        rptCrystal = null;
                    }

                    PopulateReportType();
                    txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    int iYear = DateTime.Now.Year - 6;
                    txtFromDate.Text = "01/04/" + iYear;
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
            if (rptCrystal != null)
            {
                rptCrystal.Dispose();
                rptCrystal = null;
            }
        }
        protected void rptCrystal_Unload(object sender, EventArgs e)
        {
            if (rptCrystal != null)
            {
                rptCrystal.Dispose();
                rptCrystal = null;
            }
        }

        #region btnReport_Click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Main mainmaster = (Main)Page.Master;
                mainmaster.ShowHideFilters(btnReport, reportFiltersTable, reportViewerHolder);

                if (btnReport.Text == "Back")
                {
                    CallCrystalReport();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region CallCrystalReport
        private void CallCrystalReport()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!string.IsNullOrEmpty(strBranchCode))
                {
                    #region Declaration
                    string strSelectionFormula = null, strDateQuery = null, strSupplierQuery = null, strBranchQuery = null;
                    switch (ddlReportType.SelectedValue)
                    {
                        case "Paid":
                            rptCrystal.ReportName = "SupplierReconciliation";
                            strDateQuery = "{View_Supplier_Reconsilation.Document_Reference_Date}";
                            strSupplierQuery = "{View_Supplier_Reconsilation.Supplier}";
                            strBranchQuery = "mid({View_Supplier_Reconsilation.serial_number},15,3)";
                            break;
                        case "Accounted":
                            rptCrystal.ReportName = "SupplierReconciliationCredit";
                            strDateQuery = "{View_Supplier_Account_Credit.Document_Date}";
                            strSupplierQuery = "{View_Supplier_Account_Credit.Supplier}";
                            strBranchQuery = "mid({View_Supplier_Account_Credit.chart_of_account_code},18,3)";
                            break;
                        case "NotAccounted":
                            rptCrystal.ReportName = "SupplierReconciliationDebit";
                            strDateQuery = "{View_Supplier_Account_Debit.Document_Date}";
                            strSupplierQuery = "{View_Supplier_Account_Debit.Supplier}";
                            strBranchQuery = "mid({View_Supplier_Account_Debit.Chart_of_Account_code},18,3)";
                            break;
                    }
                    #endregion

                    IMPALLibrary.Masters.Finance oFinance = new IMPALLibrary.Masters.Finance();
                    IMPALLibrary.Masters.FinanceProp oProp = new IMPALLibrary.Masters.FinanceProp();
                    oProp.BranchCode = strBranchCode;
                    oProp.FromDate = txtFromDate.Text;
                    oProp.ToDate = txtToDate.Text;
                    oFinance.CalculateSupplierReconciliation(oProp, strBranchCode, ddlSupplier.SelectedItem.Value);

                    string strFromDate = Convert.ToDateTime(hidFromDate.Value).ToString("yyyy,MM,dd");
                    string strToDate = Convert.ToDateTime(hidToDate.Value).ToString("yyyy,MM,dd");
                    strSelectionFormula = strDateQuery + " >= Date (" + strFromDate + ") and "
                                           + strDateQuery + " <= Date (" + strToDate + ")";
                    if (ddlSupplier.SelectedIndex > 0)
                        strSelectionFormula = strSelectionFormula + " and " + strSupplierQuery + " = '" + ddlSupplier.SelectedValue + "'";
                    if (!strBranchCode.Equals("CRP"))
                        strSelectionFormula = strSelectionFormula + " and " + strBranchQuery + " = '" + strBranchCode + "'";

                    rptCrystal.RecordSelectionFormula = strSelectionFormula;
                    rptCrystal.CrystalFormulaFields.Add("From_Date", "\"" + txtFromDate.Text + "\"");
                    rptCrystal.CrystalFormulaFields.Add("To_Date", "\"" + txtToDate.Text + "\"");
                    rptCrystal.GenerateReportHO();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region PopulateReportType
        /// <summary>
        /// Populates the dropdown with Report Types from XML
        /// </summary>
        protected void PopulateReportType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ImpalLibrary oLib = new ImpalLibrary();
                List<DropDownListValue> lstValues = new List<DropDownListValue>();
                lstValues = oLib.GetDropDownListValues("ReportType-SupplierReconciliation");
                ddlReportType.DataSource = lstValues;
                ddlReportType.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion
    }
}
