#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Common;
using IMPALLibrary.Masters.Sales;
#endregion


namespace IMPALWeb.Reports.Finance.SalesTax
{
    public partial class CFormSupplier : System.Web.UI.Page
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

                    LoadAccPeriodDDL();
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
                    string strSelectionFormula = null;
                    string strDateQuery = "{Inward_header.Invoice_date}";
                    string strSupplierQuery = "{Inward_header.supplier_code}";
                    string strOSLSind = "{Inward_header.OS_LS_Indicator}";
                    string strstatus = "{Inward_header.status}";
                    string strBranchQuery = "{branch_master.branch_code}";
                    string strFromDate = null;
                    string strToDate = null;
                    ImpalLibrary oCommon = new ImpalLibrary();
                    #endregion

                    #region Selction Formula Formation
                    strFromDate = "Date (" + DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
                    strToDate = "Date (" + DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
                    strSelectionFormula = strDateQuery + " >= " + strFromDate + " and "
                                             + strDateQuery + " <= " + strToDate + " and " + strOSLSind + "='O' and " + strstatus + "<>'E'";

                    if (ddlSupplier.SelectedIndex > 0)
                        strSelectionFormula = strSelectionFormula + " and " + strSupplierQuery + " = '" + ddlSupplier.SelectedValue + "'";
                    if (!strBranchCode.Equals("CRP"))
                        strSelectionFormula = strSelectionFormula + " and " + strBranchQuery + " = '" + strBranchCode + "'";
                    #endregion

                    rptCrystal.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
                    rptCrystal.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
                    rptCrystal.RecordSelectionFormula = strSelectionFormula;
                    rptCrystal.GenerateReport();
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
        /*protected void PopulateReportType(DateTime StartDate)
        {

            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                //ImpalLibrary oLib = new ImpalLibrary();
                //List<DropDownListValue> lstValues = new List<DropDownListValue>();
                //lstValues = oLib.GetDropDownListValues("ReportType-CFormSupplier");
                //ddlReportType.DataSource = lstValues;
                //ddlReportType.DataBind();
                int iYear = StartDate.Year;
                int iMonth = StartDate.Month;
                if (iMonth == 1)
                {
                    iYear = StartDate.Year - 1;
                }
                int iPrevYear = StartDate.Year - 1;
                string strPrevYear = iPrevYear.ToString().Substring(2);
                string strYear = iYear.ToString().Substring(2);
                string strEndYear = (iYear + 1).ToString().Substring(2);
                ListItem itemCurrent = new ListItem("Current", "Current");
                ListItem itemAprJun = new ListItem("Apr'" + strYear + " - Jun'" + strYear, "01/04/" + iYear);
                ListItem itemJulSep = new ListItem("Jul'" + strYear + " - Sep'" + strYear, "01/07/" + iYear);
                ListItem itemOctDec = new ListItem("Oct'" + strYear + " - Dec'" + strYear, "01/10/" + iYear);
                ListItem itemJanMar = new ListItem("Jan'" + strYear + " - Mar'" + strYear, "01/01/" + iYear);
                ListItem itemOctDec1 = new ListItem("Oct'" + strPrevYear + " - Dec'" + strPrevYear, "01/10/" + iPrevYear);
                ListItem itemJulSep1 = new ListItem("Jul'" + strPrevYear + " - Sep'" + strPrevYear, "01/07/" + iPrevYear);
                switch (iMonth)
                {
                    case 4:
                        ddlReportType.Items.Add(itemCurrent);
                        ddlReportType.Items.Add(itemJulSep1);
                        ddlReportType.Items.Add(itemOctDec1);
                        ddlReportType.Items.Add(itemJanMar);
                        break;
                    case 7:
                        ddlReportType.Items.Add(itemCurrent);
                        ddlReportType.Items.Add(itemAprJun);
                        ddlReportType.Items.Add(itemOctDec1);
                        ddlReportType.Items.Add(itemJanMar);
                        break;
                    case 10:
                        ddlReportType.Items.Add(itemCurrent);
                        ddlReportType.Items.Add(itemAprJun);
                        ddlReportType.Items.Add(itemJulSep);
                        ddlReportType.Items.Add(itemJanMar);
                        break;
                    case 1:
                        ddlReportType.Items.Add(itemCurrent);
                        ddlReportType.Items.Add(itemAprJun);
                        ddlReportType.Items.Add(itemJulSep);
                        ddlReportType.Items.Add(itemOctDec);
                        break;
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }*/
        #endregion

        #region LoadAccPeriodDDL
        private void LoadAccPeriodDDL()
        {

            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                AccountingPeriods oAcc = new AccountingPeriods();
                ddlAccPeriod.DataSource = oAcc.GetAccountingPeriod(22, null, strBranchCode);
                ddlAccPeriod.DataTextField = "Desc";
                ddlAccPeriod.DataValueField = "AccPeriodCode";
                ddlAccPeriod.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region ddlAccPeriod_IndexChanged
        protected void ddlAccPeriod_IndexChanged(object sender, EventArgs e)
        {

            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = txtFromDate.Text;                                
                string strYearStart = ddlAccPeriod.SelectedItem.Text.Substring(0, 4);
                    
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion        
    }
}
