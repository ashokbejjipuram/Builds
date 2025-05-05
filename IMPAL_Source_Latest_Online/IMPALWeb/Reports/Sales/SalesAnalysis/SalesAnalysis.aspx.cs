#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Common;
using IMPALLibrary;
using IMPALLibrary.Masters.CustomerDetails;
using IMPALLibrary.Masters.Sales;
#endregion

namespace IMPALWeb.Reports.Sales.SalesAnalysis
{
    public partial class SalesAnalysis : System.Web.UI.Page
    {
        #region ReportTypes
        private const string TownSalesMan = "Town-SalesMan";
        private const string SalesManTown = "SalesMan-Town";
        private const string CustomerLine = "Customer-Line";
        private const string LineCustomer = "Line-Customer";
        private const string TownLine = "Town-Line";
        private const string LineTown = "Line-Town";
        private const string LineSalesMan = "Line-SalesMan";
        private const string SalesManLine = "SalesMan-Line";
        private const string SalesManCustomer = "SalesMan-Customer";
        private const string CustomerSalesMan = "Customer-SalesMan";
        private const string TownCustomer = "Town-Customer";
        #endregion

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
                    if (crSalesAnalysis != null)
                    {
                        crSalesAnalysis.Dispose();
                        crSalesAnalysis = null;
                    }

                    PopulateReportType();
                    LoadSupplierDDL();
                    GetTownlist();
                    GetCustomerList();
                    PopulateSalesMenDDL();
                    LoadMonthYearDDL();
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
            if (crSalesAnalysis != null)
            {
                crSalesAnalysis.Dispose();
                crSalesAnalysis = null;
            }
        }
        protected void crSalesAnalysis_Unload(object sender, EventArgs e)
        {
            if (crSalesAnalysis != null)
            {
                crSalesAnalysis.Dispose();
                crSalesAnalysis = null;
            }
        }

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
                lstValues = oLib.GetDropDownListValues("ReportType-SalesAnalysis");
                ddlReportType.DataSource = lstValues;
                ddlReportType.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region LoadSupplierDDL
        private void LoadSupplierDDL()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Suppliers oSuppliers = new Suppliers();
                ddlSupplier.DataSource = oSuppliers.GetAllSuppliers();
                ddlSupplier.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region GetTownlist
        /// <summary>
        /// Gets the list of Towns from DB
        /// </summary>
        public void GetTownlist()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                List<IMPALLibrary.Town> lstTowns = null;
                Towns oTowns = new Towns();
                lstTowns = oTowns.GetBranchBasedTowns(null);
                ddlTown.DataSource = lstTowns;
                ddlTown.DataBind();
                ddlTown.Items.Insert(0, string.Empty);
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region GetCustomerList
        public void GetCustomerList()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                List<CustomerDtls> lstCompletion = null;
                CustomerDetails oCustomerDtls = new CustomerDetails();
                CustomerDtls oCustomer = new CustomerDtls();
                lstCompletion = oCustomerDtls.GetCustomers();
                ddlCustomer.DataSource = lstCompletion;
                ddlCustomer.DataTextField = "Name";
                ddlCustomer.DataValueField = "Code";
                ddlCustomer.DataBind();
                ddlCustomer.Items.Insert(0, string.Empty);
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region PopulateSalesMenDDL
        /// <summary>
        /// Populates the dropdown with Sales Men Names from DB
        /// </summary>
        protected void PopulateSalesMenDDL()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                List<SalesMan> lstSalesMen = null;
                SalesMen oSalesMen = new SalesMen();
                lstSalesMen = oSalesMen.GetSalesMenNames();
                ddlSalesMan.DataSource = lstSalesMen;
                ddlSalesMan.DataBind();
                ddlSalesMan.Items.Insert(0, string.Empty);
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

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
                    crSalesAnalysis.ReportName = ddlReportType.SelectedValue;
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

                    string strYearQuery = "Mid({V_Sales_statement.month_year},3,6)";
                    string strMonthQuery = "Mid({V_Sales_statement.month_year},1,2)";
                    string strTownQuery = "{V_Sales_statement.Town_Code}";
                    string strSupplierQuery = "{V_Sales_statement.Supplier_Code}";
                    string strSalesManQuery = "{V_Sales_statement.Sales_Man_Code}";
                    string strBranchQuery = "{V_Sales_statement.Branch_Code}";
                    #endregion

                    strSelectionFormula = strBranchQuery + " = '" + Session["BranchCode"].ToString() + "' and ";

                    if (Convert.ToInt16(ddlMonthYear.SelectedValue.Substring(0, 2)) < 4)
                        strSelectionFormula = strSelectionFormula + " (" + strMonthQuery + ">='04' and " + strMonthQuery + "<='12' and " + strYearQuery + "<='" + (Convert.ToInt16(ddlMonthYear.SelectedValue.Substring(2, 4)) - 1) + "') or ("
                            + strMonthQuery + "<='" + ddlMonthYear.SelectedValue.Substring(0, 2) + "' and " + strYearQuery + "<='" + ddlMonthYear.SelectedValue.Substring(2, 4) + "')";
                    else
                        strSelectionFormula = strSelectionFormula + strMonthQuery + " >= '04' and " + strMonthQuery + "<= '" + ddlMonthYear.SelectedValue.Substring(0, 2) + "' and "
                            + strYearQuery + "<='" + ddlMonthYear.SelectedValue.Substring(2, 4) + "'";

                    string strReportType = ddlReportType.SelectedValue;

                    if (ddlTown.SelectedIndex > 0 && (strReportType.Equals(TownSalesMan) || strReportType.Equals(SalesManTown)
                        || strReportType.Equals(TownLine) || strReportType.Equals(LineTown) || strReportType.Equals(TownCustomer)))
                        strSelectionFormula = strSelectionFormula + " and " + strTownQuery + " = " + ddlTown.SelectedValue;

                    if (ddlSupplier.SelectedIndex > 0 && (strReportType.Equals(CustomerLine) || strReportType.Equals(LineCustomer)
                        || strReportType.Equals(TownLine) || strReportType.Equals(LineTown)
                        || strReportType.Equals(LineSalesMan) || strReportType.Equals(SalesManLine)))
                        strSelectionFormula = strSelectionFormula + " and " + strSupplierQuery + " = '" + ddlSupplier.SelectedValue + "'";

                    if (ddlCustomer.SelectedIndex > 0 && (strReportType.Equals(CustomerLine) || strReportType.Equals(LineCustomer)
                        || strReportType.Equals(SalesManCustomer) || strReportType.Equals(CustomerSalesMan)
                        || strReportType.Equals(TownCustomer)))
                        //strSelectionFormula = strSelectionFormula + " and " + strCustomerQuery + " = '" + ddlCustomer.SelectedValue + "'";

                        if (ddlSalesMan.SelectedIndex > 0 && (strReportType.Equals(TownSalesMan) || strReportType.Equals(SalesManTown)
                            || strReportType.Equals(LineSalesMan) || strReportType.Equals(SalesManLine)
                            || strReportType.Equals(SalesManCustomer) || strReportType.Equals(CustomerSalesMan)))
                            strSelectionFormula = strSelectionFormula + " and " + strSalesManQuery + " = '" + ddlSalesMan.SelectedValue + "'";

                    crSalesAnalysis.CrystalFormulaFields.Add("AccDesc", "\"" + ddlMonthYear.SelectedValue.ToString() + "\"");

                    crSalesAnalysis.RecordSelectionFormula = strSelectionFormula;
                    crSalesAnalysis.GenerateReport();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region LoadMonthYearDDL
        /// <summary>
        /// Populates the dropdown with Month and Year
        /// </summary>
        protected void LoadMonthYearDDL()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                DateTime dtToday = DateTime.Today;
                List<string> lstDates = new List<string>();

                for (int iYear = (dtToday.Year - 2); iYear <= dtToday.Year; iYear++)
                {
                    int iMonth = 1, iTotalMonth = 12;
                    if (iYear == (dtToday.Year - 2))
                        iMonth = (dtToday.Month + 1);
                    if (iYear == dtToday.Year)
                        iTotalMonth = dtToday.Month;
                    while (iMonth <= iTotalMonth)
                    {
                        string strDate = Convert.ToDateTime(iMonth + ",01," + iYear).ToString("MMyyyy");
                        lstDates.Add(strDate);
                        iMonth++;
                    }
                }
                lstDates.Reverse();
                ddlMonthYear.DataSource = lstDates;
                ddlMonthYear.DataBind();
                ddlMonthYear.Items.Insert(0, string.Empty);
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion
    }
}