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
    public partial class CFormSupplierNew : System.Web.UI.Page
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

                    ddlReportType.Items.Clear();
                    LoadAccPeriodDDL();
                    ddlAccPeriod_IndexChanged(ddlAccPeriod, null);
                    ddlFromDate_IndexChanged(ddlFromDate, null);
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
                    string strSelectionFormula = null;
                    string strDateQuery = "{Inward_header.Invoice_date}";
                    string strSupplierQuery = "{Inward_header.supplier_code}";
                    string strBranchQuery = "{branch_master.branch_code}";
                    string strFromDate = null;
                    string strToDate = null;
                    ImpalLibrary oCommon = new ImpalLibrary();

                    DateTime dtFromDate = DateTime.ParseExact(ddlFromDate.SelectedValue, "dd/MM/yyyy", null);
                    int iEndMonth = dtFromDate.Month + 2;
                    int iEndDay = DateTime.DaysInMonth(dtFromDate.Year, iEndMonth);
                    string strEndDate = iEndMonth + "/" + iEndDay + "/" + dtFromDate.Year;
                    strFromDate = dtFromDate.ToString("yyyy,MM,dd");
                    strToDate = Convert.ToDateTime(strEndDate).ToString("yyyy,MM,dd");
                    strSelectionFormula = strDateQuery + " >= Date (" + strFromDate + ") and "
                                             + strDateQuery + " <= Date (" + strToDate + ")";

                    if (ddlSupplier.SelectedIndex > 0)
                        strSelectionFormula = strSelectionFormula + " and " + strSupplierQuery + " = '" + ddlSupplier.SelectedValue + "'";
                    if (!strBranchCode.Equals("CRP"))
                        strSelectionFormula = strSelectionFormula + " and " + strBranchQuery + " = '" + strBranchCode + "'";
                    
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

        protected void PopulateReportType(int Year, int Month)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddlReportType.Items.Clear();
                int iYear = Year;
                int iMonth = Month;
                if (iMonth == 1)
                {
                    iYear = Year - 1;
                }
                int iPrevYear = Year - 1;
                string strPrevYear = iPrevYear.ToString().Substring(2);
                string strYear = iYear.ToString().Substring(2);
                string strEndYear = (iYear + 1).ToString().Substring(2);
                ListItem itemCurrent = new ListItem("Current", "Current");
                ListItem itemJanMar = new ListItem("Jan'" + strYear + " - Mar'" + strYear, "01/01/" + iYear);                
                ListItem itemAprJun = new ListItem("Apr'" + strYear + " - Jun'" + strYear, "01/04/" + iYear);
                ListItem itemJulSep = new ListItem("Jul'" + strYear + " - Sep'" + strYear, "01/07/" + iYear);
                ListItem itemJulSep1 = new ListItem("Jul'" + strPrevYear + " - Sep'" + strPrevYear, "01/07/" + iPrevYear);                
                ListItem itemOctDec = new ListItem("Oct'" + strYear + " - Dec'" + strYear, "01/10/" + iYear);
                ListItem itemOctDec1 = new ListItem("Oct'" + strPrevYear + " - Dec'" + strPrevYear, "01/10/" + iPrevYear);
                switch (iMonth)
                {
                    case 1:
                        ddlReportType.Items.Add(itemCurrent);
                        ddlReportType.Items.Add(itemAprJun);
                        ddlReportType.Items.Add(itemJulSep);
                        ddlReportType.Items.Add(itemOctDec);
                        break;
                    case 4:
                        ddlReportType.Items.Add(itemCurrent);
                        ddlReportType.Items.Add(itemJulSep1);
                        ddlReportType.Items.Add(itemOctDec1);
                        ddlReportType.Items.Add(itemJanMar);
                        break;
                    case 7:
                        ddlReportType.Items.Add(itemCurrent);
                        ddlReportType.Items.Add(itemAprJun);
                        ddlReportType.Items.Add(itemJanMar);
                        ddlReportType.Items.Add(itemOctDec1);
                        break;
                    case 10:
                        ddlReportType.Items.Add(itemCurrent);
                        ddlReportType.Items.Add(itemAprJun);
                        ddlReportType.Items.Add(itemJulSep);
                        ddlReportType.Items.Add(itemJanMar);
                        break;                    
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        protected void ddlFromDate_IndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                AccountingPeriods oAcc = new AccountingPeriods();
                txtToDate.Text = oAcc.GetQuarterEndDate(ddlFromDate.SelectedValue.ToString());
                PopulateReportType(Convert.ToInt32(ddlFromDate.SelectedValue.ToString().Substring(6, 4)), Convert.ToInt32(ddlFromDate.SelectedValue.ToString().Substring(3, 2)));
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

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

        protected void ddlAccPeriod_IndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                AccountingPeriods oAcc = new AccountingPeriods();
                ddlFromDate.DataSource = oAcc.GetQuarterDates(Convert.ToInt32(ddlAccPeriod.SelectedValue.ToString()));
                ddlFromDate.DataTextField = "Desc";
                ddlFromDate.DataValueField = "AccPeriodCode";
                ddlFromDate.DataBind();                
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
    }
}
