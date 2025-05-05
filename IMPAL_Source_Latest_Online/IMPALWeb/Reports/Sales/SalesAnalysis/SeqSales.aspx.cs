#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Common;
using IMPALLibrary.Common;
using IMPALLibrary;
using IMPALLibrary.Masters.Sales;
using IMPALLibrary.Masters.CustomerDetails;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
#endregion

namespace IMPALWeb.Reports.Sales.SalesAnalysis
{
    public partial class SeqSales : System.Web.UI.Page
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
                    if (crSeqSales != null)
                    {
                        crSeqSales.Dispose();
                        crSeqSales = null;
                    }

                    LoadAccPeriodDDL();
                    GetTownlist();
                    GetCustomerList();
                    PopulateReportType();
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
            if (crSeqSales != null)
            {
                crSeqSales.Dispose();
                crSeqSales = null;
            }
        }
        protected void crSeqSales_Unload(object sender, EventArgs e)
        {
            if (crSeqSales != null)
            {
                crSeqSales.Dispose();
                crSeqSales = null;
            }
        }

        #region LoadAccPeriodDDL
        private void LoadAccPeriodDDL()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                AccountingPeriods oAcc = new AccountingPeriods();
                ddlAccPeriod.DataSource = oAcc.GetAccountingPeriod(18, null, strBranchCode);
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

        #region GetCustomerList
        public void GetCustomerList()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                List<CustomerDtls> lstCompletion = null;
                CustomerDetails oCustomerDtls = new CustomerDetails();
                CustomerDtls oCustomer = new CustomerDtls();
                lstCompletion = oCustomerDtls.GetCustomers(strBranchCode, null);
                cbCustomerName.DataSource = lstCompletion;
                cbCustomerName.DataTextField = "Name";
                cbCustomerName.DataValueField = "Code";
                cbCustomerName.DataBind();
                cbCustomerName.SelectedIndex = 0;
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region GetCustomerListByTown
        public void GetCustomerListByTown()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                List<CustomerDtls> lstCompletion = null;
                CustomerDetails oCustomerDtls = new CustomerDetails();
                CustomerDtls oCustomer = new CustomerDtls();
                int TownCode = 0;
                if (ddlTownCode.SelectedValue.ToString() != "")
                    TownCode = Convert.ToInt32(ddlTownCode.SelectedValue);
                lstCompletion = oCustomerDtls.GetCustomersByTown(strBranchCode, TownCode, null);
                cbCustomerName.DataSource = lstCompletion;
                cbCustomerName.DataTextField = "Name";
                cbCustomerName.DataValueField = "Code";
                cbCustomerName.DataBind();
                cbCustomerName.SelectedIndex = 0;
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region GetCustomerDetails
        private void GetCustomerDetails(string CustomerCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                CustomerDetails oDtls = new CustomerDetails();
                CustomerDtls oCustomer = null;
                oCustomer = oDtls.GetDetails(strBranchCode, CustomerCode);
                txtCustomerCode.Text = cbCustomerName.SelectedValue;
                txtAddress1.Text = oCustomer.Address1;
                txtAddress2.Text = oCustomer.Address2;
                txtAddress3.Text = oCustomer.Address3;
                txtAddress4.Text = oCustomer.Address4;
                txtLocation.Text = oCustomer.Location;
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region cbCustomerNameChange
        protected void cbCustomerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCustomerDetails(cbCustomerName.SelectedValue);
            if (cbCustomerName.SelectedIndex > 0)
                divCustomerInfo.Style.Add("display", "block");
            else
                divCustomerInfo.Style.Add("display", "none");
        }
        #endregion

        #region TownCodeChange
        protected void ddlTownCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            GetCustomerListByTown();
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
                    if (ddlReportType.SelectedIndex.Equals(0))
                        crSeqSales.ReportName = "SeqSalesQty";
                    else if (ddlReportType.SelectedIndex.Equals(1))
                        crSeqSales.ReportName = "SeqSalesVal";
                    else if (ddlReportType.SelectedIndex.Equals(2))
                        crSeqSales.ReportName = "SeqSalesQtySummary";
                    else if (ddlReportType.SelectedIndex.Equals(3))
                        crSeqSales.ReportName = "SeqSalesValSummary";

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

                    string strBranchQuery = "{V_SalesReports.branch_code}";
                    string strCustCode = "{customer_master.customer_code}";
                    string strSupplierQuery = "{V_SalesReports.Supplier_Code}";
                    string strTownQuery = "{Town_Master.town_code}";
                    string strDocumentDate = "{V_SalesReports.Document_Date}";

                    if (cbCustomerName.SelectedValue.ToString() == "0" && ddlLineCode.SelectedValue.ToString() == "0" && ddlTownCode.SelectedValue.ToString() == "")
                        strSelectionFormula = strBranchQuery + "='" + strBranchCode + "'";
                    else if (cbCustomerName.SelectedValue.ToString() != "0" && ddlLineCode.SelectedValue.ToString() == "0" && ddlTownCode.SelectedValue.ToString() == "")
                        strSelectionFormula = strBranchQuery + "='" + strBranchCode + "' and " + strCustCode + "='" + cbCustomerName.SelectedValue.ToString() + "'";
                    else if (cbCustomerName.SelectedValue.ToString() == "0" && ddlLineCode.SelectedValue.ToString() != "0" && ddlTownCode.SelectedValue.ToString() == "")
                        strSelectionFormula = strBranchQuery + "='" + strBranchCode + "' and " + strSupplierQuery + "='" + ddlLineCode.SelectedValue.ToString() + "'";
                    else if (cbCustomerName.SelectedValue.ToString() == "0" && ddlLineCode.SelectedValue.ToString() == "0" && ddlTownCode.SelectedValue.ToString() != "")
                        strSelectionFormula = strBranchQuery + "='" + strBranchCode + "' and " + strTownQuery + "=" + ddlTownCode.SelectedValue;
                    else if (cbCustomerName.SelectedValue.ToString() != "0" && ddlLineCode.SelectedValue.ToString() != "0" && ddlTownCode.SelectedValue.ToString() == "")
                        strSelectionFormula = strBranchQuery + "='" + strBranchCode + "' and " + strCustCode + "='" + cbCustomerName.SelectedValue.ToString() + "' and " + strSupplierQuery + "='" + ddlLineCode.SelectedValue.ToString() + "'";
                    else if (cbCustomerName.SelectedValue.ToString() == "0" && ddlLineCode.SelectedValue.ToString() != "0" && ddlTownCode.SelectedValue.ToString() != "")
                        strSelectionFormula = strBranchQuery + "='" + strBranchCode + "' and " + strSupplierQuery + "='" + ddlLineCode.SelectedValue.ToString() + "' and " + strTownQuery + "=" + ddlTownCode.SelectedValue + "";
                    else if (cbCustomerName.SelectedValue.ToString() != "0" && ddlLineCode.SelectedValue.ToString() == "0" && ddlTownCode.SelectedValue.ToString() != "")
                        strSelectionFormula = strBranchQuery + "='" + strBranchCode + "' and " + strCustCode + "='" + cbCustomerName.SelectedValue.ToString() + "' and " + strTownQuery + "=" + ddlTownCode.SelectedValue + "";
                    else if (cbCustomerName.SelectedValue.ToString() != "0" && ddlLineCode.SelectedValue.ToString() != "0" && ddlTownCode.SelectedValue.ToString() != "")
                        strSelectionFormula = strBranchQuery + "='" + strBranchCode + "' and " + strCustCode + "='" + cbCustomerName.SelectedValue.ToString() + "' and " + strSupplierQuery + "='" + ddlLineCode.SelectedValue.ToString() + "' and " + strTownQuery + "=" + ddlTownCode.SelectedValue + "";

                    Database ImpalDB = DataAccess.GetDatabase();
                    string StartDate = string.Empty;
                    string EndDate = string.Empty;
                    DbCommand cmd = ImpalDB.GetSqlStringCommand("select start_date, end_date from accounting_period WITH (NOLOCK) where accounting_period_code ='" + ddlAccPeriod.SelectedValue + "'");
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {
                            StartDate = reader[0].ToString();
                            EndDate = reader[1].ToString();
                        }
                    }

                    strSelectionFormula = strSelectionFormula + " and " + strDocumentDate + ">=Date (" + Convert.ToDateTime(StartDate).ToString("yyyy,MM,dd") + ")";
                    strSelectionFormula = strSelectionFormula + " and " + strDocumentDate + "<=Date (" + Convert.ToDateTime(EndDate).ToString("yyyy,MM,dd") + ")";

                    crSeqSales.CrystalFormulaFields.Add("AccDesc", "\"" + ddlAccPeriod.SelectedItem.Text + "\"");
                    crSeqSales.RecordSelectionFormula = strSelectionFormula;
                    crSeqSales.GenerateReport();
                }
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
                if (strBranchCode.Equals("CRP"))
                    lstTowns = oTowns.GetBranchBasedTowns(null);
                else
                    lstTowns = oTowns.GetBranchBasedTowns(strBranchCode);
                ddlTownCode.DataSource = lstTowns;
                ddlTownCode.DataBind();
                ddlTownCode.Items.Insert(0, string.Empty);
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
                lstValues = oLib.GetDropDownListValues("ReportType-SeqSales");
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