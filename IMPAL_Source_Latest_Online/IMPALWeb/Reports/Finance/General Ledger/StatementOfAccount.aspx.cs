#region Namespace Declaration
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using IMPALLibrary.Common;
using IMPALLibrary.Masters.CustomerDetails;
using Microsoft.Practices.EnterpriseLibrary.Data;
using IMPALLibrary;
using System.Data.Common;
using System.Data;
using AjaxControlToolkit;
#endregion

namespace IMPALWeb.Reports.Finance.General_Ledger
{
    public partial class StatementOfAccount : System.Web.UI.Page
    {
        string strBranchCode = string.Empty;
        string selectionformula = string.Empty;
        string strselAccCode = string.Empty;
        string strselDocDate = "{general_ledger_detail.document_date}";
        string strselBranch = "{branch_master.branch_code}";

        #region Page Init
        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "Page_Init", "Statement Of Account Page Init Method");
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
            //Log.WriteLog(Source, "Page_Load", "Statement Of Account Page Load Method");
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = (string)Session["BranchCode"];

                if (!IsPostBack)
                {
                    if (crStatementOfAccount != null)
                    {
                        crStatementOfAccount.Dispose();
                        crStatementOfAccount = null;
                    }

                    divCustomerInfo.Visible = false;
                    fnPopulateCustomerType();
                    fnPopulateCustomer();
                    fnPopulateReportType();
                    fnPopulateMonthYear();
                    txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtToDate.Text = txtFromDate.Text;
                    cboFromCustomer.SelectedIndex = 0;
                    cboToCustomer.SelectedIndex = 0;
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
            if (crStatementOfAccount != null)
            {
                crStatementOfAccount.Dispose();
                crStatementOfAccount = null;
            }
        }
        protected void crStatementOfAccount_Unload(object sender, EventArgs e)
        {
            if (crStatementOfAccount != null)
            {
                crStatementOfAccount.Dispose();
                crStatementOfAccount = null;
            }
        }

        #region To Populate Customer type Dropdown
        protected void fnPopulateCustomerType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "fnPopulateCustomerType()", "Statement Of Account Populate Customer Type Method");
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                ddlCustomerType.DataSource = oCommon.GetDropDownListValues("StatementOfAccount");
                ddlCustomerType.DataTextField = "DisplayText";
                ddlCustomerType.DataValueField = "DisplayValue";
                ddlCustomerType.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Function to Populate from and to customer
        public void fnPopulateCustomer()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                List<CustomerDtls> lstCompletion = null;
                CustomerDetails oCustomerDtls = new CustomerDetails();
                CustomerDtls oCustomer = new CustomerDtls();
                if (strBranchCode != "CRP")
                {
                    lstCompletion = oCustomerDtls.GetCustomers(strBranchCode, "StatementOfAccount");
                }
                else
                {
                    lstCompletion = oCustomerDtls.GetCustomers(strBranchCode, "Corporate");
                }
                cboFromCustomer.DataSource = lstCompletion;
                cboFromCustomer.DataTextField = "Name";
                cboFromCustomer.DataValueField = "Code";
                cboFromCustomer.DataBind();
                cboToCustomer.DataSource = lstCompletion;
                cboToCustomer.DataTextField = "Name";
                cboToCustomer.DataValueField = "Code";
                cboToCustomer.DataBind();
                cboFromCustomer.SelectedIndex = 0;
                cboToCustomer.SelectedIndex = 0;
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Function to Populate from and to customer
        public void fnPopulateSalesMan()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                List<CustomerDtls> lstCompletion = null;
                CustomerDetails oCustomerDtls = new CustomerDetails();
                CustomerDtls oCustomer = new CustomerDtls();
                lstCompletion = oCustomerDtls.GetSalesMan(strBranchCode);
                cboFromCustomer.DataSource = lstCompletion;
                cboFromCustomer.DataTextField = "Name";
                cboFromCustomer.DataValueField = "Code";
                cboFromCustomer.DataBind();
                cboToCustomer.DataSource = lstCompletion;
                cboToCustomer.DataTextField = "Name";
                cboToCustomer.DataValueField = "Code";
                cboToCustomer.DataBind();
                cboFromCustomer.SelectedIndex = 0;
                cboToCustomer.SelectedIndex = 0;
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region To Populate Report type Dropdown
        protected void fnPopulateReportType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "fnPopulateReportType()", "Statement Of Account Populate Populate Report type Method");
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                ddlReportType.DataSource = oCommon.GetDropDownListValues("StatementOfAccount-RptType");
                ddlReportType.DataTextField = "DisplayText";
                ddlReportType.DataValueField = "DisplayValue";
                ddlReportType.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }

        }
        #endregion

        #region Function to Populate From and To Branch
        public void fnPopulateBranch()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "fnPopulateBranch()", "Statement Of Account Populate Branch Method");
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                ddlReportType.DataSource = oCommon.GetDropDownListValues("StatementOfAccount-RptType-Customer");
                ddlReportType.DataTextField = "DisplayText";
                ddlReportType.DataValueField = "DisplayValue";
                ddlReportType.DataBind();

                IMPALLibrary.Branches objBranch = new IMPALLibrary.Branches();
                cboFromCustomer.DataSource = objBranch.GetCorpBranch();
                cboFromCustomer.DataValueField = "BranchCode";
                cboFromCustomer.DataTextField = "BranchName";
                cboFromCustomer.DataBind();
                cboToCustomer.DataSource = objBranch.GetCorpBranch();
                cboToCustomer.DataValueField = "BranchCode";
                cboToCustomer.DataTextField = "BranchName";
                cboToCustomer.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Function to Populate From and To Supplier
        public void fnPopulateSupplier()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "fnPopulateSupplier()", "Statement Of Account Populate Supplier Method");
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                ddlReportType.DataSource = oCommon.GetDropDownListValues("StatementOfAccount-RptType-Customer");
                ddlReportType.DataTextField = "DisplayText";
                ddlReportType.DataValueField = "DisplayValue";
                ddlReportType.DataBind();

                IMPALLibrary.Suppliers objSupplier = new IMPALLibrary.Suppliers();
                cboFromCustomer.DataSource = objSupplier.GetAllSuppliers();
                cboFromCustomer.DataValueField = "SupplierCode";
                cboFromCustomer.DataTextField = "SupplierName";
                cboFromCustomer.DataBind();
                cboToCustomer.DataSource = objSupplier.GetAllSuppliers();
                cboToCustomer.DataValueField = "SupplierCode";
                cboToCustomer.DataTextField = "SupplierName";
                cboToCustomer.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Function to Populate Month and year
        public void fnPopulateMonthYear()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "fnPopulateMonthYear()", "Statement Of Account Populate Monthyear Method");
            try
            {
                IMPALLibrary.Masters.Item_wise_sales objItem = new IMPALLibrary.Masters.Item_wise_sales();
                ddlMonthYear.DataSource = objItem.GetMonthYear(strBranchCode);
                ddlMonthYear.DataValueField = "month_year";
                ddlMonthYear.DataTextField = "month_year";
                ddlMonthYear.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Customer type Dropdown Changed
        protected void ddlCustomerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "ddlCustomerType_SelectedIndexChanged()", "Statement Of Account Customer type Dropdown index Changed Method");
            try
            {
                if (ddlCustomerType.SelectedValue == "Customer")
                {
                    if (cboFromCustomer.SelectedValue != "0" && cboToCustomer.SelectedValue != "0" && cboFromCustomer.SelectedValue != "" && cboToCustomer.SelectedValue != "")
                    {
                        divCustomerInfo.Visible = true;
                    }
                    else
                    {
                        divCustomerInfo.Visible = false;
                    }
                    divDate.Visible = true;
                    fnPopulateCustomer();
                }
                else if (ddlCustomerType.SelectedValue == "SalesMan")
                {
                    if (cboFromCustomer.SelectedValue != "0" && cboToCustomer.SelectedValue != "0" && cboFromCustomer.SelectedValue != "" && cboToCustomer.SelectedValue != "")
                    {
                        divCustomerInfo.Visible = true;
                    }
                    else
                    {
                        divCustomerInfo.Visible = false;
                    }
                    fnPopulateReportType();
                    ddlReportType.SelectedIndex = 2;
                    ddlReportType.Enabled = false;
                    divDate.Visible = true;
                    fnPopulateSalesMan();
                }
                else if (ddlCustomerType.SelectedValue == "Branch")
                {

                    divDate.Visible = false;
                    divCustomerInfo.Visible = false;
                    fnPopulateBranch();
                }
                else
                {

                    divDate.Visible = false;
                    divCustomerInfo.Visible = false;
                    fnPopulateSupplier();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion
        
        #region From Customer and To Customer Dropdown Changed Event
        protected void ddlChangedEvent(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "ddlChangedEvent()", "Statement of Account From and To Customer Dropdown Changed Event");
            try
            {
                DropDownList DrpDwn = (DropDownList)sender;
                string DrpDwnID = DrpDwn.ID;

                if (DrpDwn.SelectedValue == string.Empty || DrpDwn.SelectedValue == null || DrpDwn.SelectedValue == "")
                {
                    DrpDwn.SelectedValue = "0";
                }

                if (ddlCustomerType.SelectedValue == "Customer" && DrpDwn.SelectedValue != "0")
                {
                    if (DrpDwn.ID == "cboFromCustomer")
                    {
                        GetCustomerDetails(DrpDwn.SelectedValue, "From");
                    }
                    else
                    {
                        GetCustomerDetails(DrpDwn.SelectedValue, "To");
                    }
                    divCustomerInfo.Visible = true;
                }
                else if (ddlCustomerType.SelectedValue == "Customer" && DrpDwn.SelectedValue == "0")
                {
                    if (DrpDwn.ID == "cboFromCustomer")
                    {
                        if (cboToCustomer.SelectedValue == "0")
                        {
                            divCustomerInfo.Visible = false;
                        }
                        else
                        {
                            GetCustomerDetails(cboToCustomer.SelectedValue, "To");
                            divCustomerInfo.Visible = true;
                        }

                    }
                    else
                    {
                        if (cboFromCustomer.SelectedValue == "0")
                        {
                            divCustomerInfo.Visible = false;
                        }
                        else
                        {
                            GetCustomerDetails(cboFromCustomer.SelectedValue, "From");
                            divCustomerInfo.Visible = true;
                        }
                    }
                }
                else
                {
                    divCustomerInfo.Visible = false;
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region To Populate Customer Details Menu
        protected void GetCustomerDetails(string CustomerCode, string type)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "GetCustomerDetails()", "Statement Of Account Get Customer Details Method");
            try
            {
                CustomerDetails oDtls = new CustomerDetails();
                CustomerDtls oCustomer = null;
                oCustomer = oDtls.GetDetails(strBranchCode, CustomerCode);
                if (type == "From")
                {
                    txtCustomerCode.Text = cboFromCustomer.SelectedValue;
                }
                else
                {
                    txtCustomerCode.Text = cboToCustomer.SelectedValue;
                }
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

        #region Generate Button Click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Main mainmaster = (Main)Page.Master;
                mainmaster.ShowHideFilters(btnReport, reportFiltersTable, reportViewerHolder);
                if (btnReport.Text == "Back")
                {
                    string strMonYr = string.Empty;
                    string strEndDate_DDMM = string.Empty;
                    string strEndDate_MMDD = string.Empty;
                    string strStartDate_YYMM = string.Empty;
                    string strMonthYear = ddlMonthYear.SelectedValue;
                    string strCustomerType = ddlCustomerType.SelectedValue;
                    string strFromCustomer = cboFromCustomer.SelectedValue;
                    string strToCustomer = cboToCustomer.SelectedValue;

                    if (ddlMonthYear.SelectedValue != "")
                    {
                        string strFormattedMonYr = string.Format("{0}/{1}", strMonthYear.Substring(0, 2), strMonthYear.Substring(2, 4));

                        strMonYr = string.Format("{0}/{1}/{2}", DateTime.DaysInMonth(Convert.ToInt32(strMonthYear.Substring(2, 4)), Convert.ToInt32(strMonthYear.Substring(0, 2))), strMonthYear.Substring(0, 2), strMonthYear.Substring(2, 4)); //strMonYr
                        strEndDate_DDMM = string.Format("{0}/{1}/{2}", strMonthYear.Substring(0, 2), DateTime.DaysInMonth(Convert.ToInt32(strMonthYear.Substring(2, 4)), Convert.ToInt32(strMonthYear.Substring(0, 2))), strMonthYear.Substring(2, 4));
                        strEndDate_MMDD = string.Format("{0}/{1}/{2}", strMonthYear.Substring(2, 4), strMonthYear.Substring(0, 2), DateTime.DaysInMonth(Convert.ToInt32(strMonthYear.Substring(2, 4)), Convert.ToInt32(strMonthYear.Substring(0, 2))));
                        strStartDate_YYMM = string.Format("{0}/{1}/{2}", strMonthYear.Substring(2, 4), strMonthYear.Substring(0, 2), "01");
                    }

                    if (ddlReportType.SelectedValue == "Report-Customer Wise" || ddlReportType.SelectedValue == "Report-Town Wise")
                    {
                        if (ddlReportType.SelectedValue == "Report-Town Wise")
                        {
                            crStatementOfAccount.ReportName = "Impal-report-State-Acc-town";
                        }
                        fnCustomerTown(strMonYr, strCustomerType, strEndDate_DDMM, strFromCustomer, strToCustomer, strBranchCode, strMonthYear, strStartDate_YYMM, strEndDate_MMDD);
                    }
                    else if (ddlReportType.SelectedValue == "Report-Salesman Wise")
                    {
                        crStatementOfAccount.ReportName = "Impal-report-State-Acc-salesman";
                        fnSalesman(strMonYr, strCustomerType, strEndDate_DDMM, strFromCustomer, strToCustomer, strBranchCode, strMonthYear, strStartDate_YYMM, strEndDate_MMDD);
                    }
                    else
                    {
                        crStatementOfAccount.ReportName = "Impal-tran-DRS-StatementOfAccounts";
                        fnNewReport(strMonYr, strCustomerType, strEndDate_DDMM, strFromCustomer, strToCustomer, strBranchCode, strMonthYear, strStartDate_YYMM, strEndDate_MMDD);
                    }

                    crStatementOfAccount.GenerateReportHO();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Populate New report
        protected void fnNewReport(string strMonYr, string strCustomerType, string strEndDate_DDMM, string strFromCustomer, string strToCustomer, string strBranchCode, string strMonthYear, string strStartDate_YYMM, string strEndDate_MMDD)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "fnNewReport()", "Statement Of Account New Report Logic Method");
            try
            {
                Database ImpalDB = DataAccess.GetDatabaseBackUp();
                DbCommand cmd = null;
                string strselCusCode = "{Statement_DRS.Customer_code}";
                string strselDocDate = "{Statement_DRS.document_date}";

                if (strFromCustomer == "0")
                { strFromCustomer = string.Empty; }
                if (strToCustomer == "0")
                { strToCustomer = string.Empty; }

                if (!string.IsNullOrEmpty(strMonYr) && strCustomerType == "Branch")
                {
                    cmd = ImpalDB.GetStoredProcCommand("usp_calbros");
                    ImpalDB.AddInParameter(cmd, "@month_year", DbType.String, strEndDate_DDMM);
                    ImpalDB.AddInParameter(cmd, "@from_br", DbType.String, strFromCustomer.Trim());
                    ImpalDB.AddInParameter(cmd, "@To_br", DbType.String, strToCustomer.Trim());
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);
                }
                else if (strCustomerType == "Customer" && (!string.IsNullOrEmpty(txtFromDate.Text) && !string.IsNullOrEmpty(txtToDate.Text)))
                {
                    cmd = ImpalDB.GetStoredProcCommand("[usp_calcustos_NewStmt_Cust]");
                    ImpalDB.AddInParameter(cmd, "@From_Date", DbType.String, txtFromDate.Text);
                    ImpalDB.AddInParameter(cmd, "@To_Date", DbType.String, txtToDate.Text);
                    ImpalDB.AddInParameter(cmd, "@from_Cus", DbType.String, strFromCustomer.Trim());
                    ImpalDB.AddInParameter(cmd, "@To_Cus", DbType.String, strToCustomer.Trim());
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);
                }
                else if (!string.IsNullOrEmpty(strMonYr) && strCustomerType == "Supplier")
                {
                    cmd = ImpalDB.GetStoredProcCommand("usp_calsupos");
                    ImpalDB.AddInParameter(cmd, "@month_year", DbType.String, strEndDate_DDMM);
                    ImpalDB.AddInParameter(cmd, "@from_sup", DbType.String, strFromCustomer.Trim());
                    ImpalDB.AddInParameter(cmd, "@To_sup", DbType.String, strToCustomer.Trim());
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);
                }

                string strFromYrMonDay = string.Format("{0},{1},{2}", txtFromDate.Text.Substring(6, 4), txtFromDate.Text.Substring(3, 2), txtFromDate.Text.Substring(0, 2));
                string strToYrMonDay = string.Format("{0},{1},{2}", txtToDate.Text.Substring(6, 4), txtToDate.Text.Substring(3, 2), txtToDate.Text.Substring(0, 2));
                string[] split = null;
                string strFormattedStDate = null;
                string[] split1 = null;
                string strFormattedEndDate = null;

                if (!string.IsNullOrEmpty(strMonYr))
                {
                    split = strStartDate_YYMM.Split('/'); //strStartDate_YYMM
                    strFormattedStDate = string.Format("{0},{1},{2}", split[0], split[1], split[2]);
                    split1 = strEndDate_MMDD.Split('/'); //strEndDate_MMDD
                    strFormattedEndDate = string.Format("{0},{1},{2}", split1[0], split1[1], split1[2]);
                }

                if (!string.IsNullOrEmpty(strFromCustomer) && string.IsNullOrEmpty(strToCustomer) && string.IsNullOrEmpty(strMonthYear))
                {
                    crStatementOfAccount.RecordSelectionFormula = strselCusCode + "='" + strFromCustomer + "'";
                }
                else if (string.IsNullOrEmpty(strFromCustomer) && string.IsNullOrEmpty(strToCustomer) && !string.IsNullOrEmpty(strMonthYear))
                {
                    crStatementOfAccount.RecordSelectionFormula = strselDocDate + ">=" + string.Format(" Date({0})", strFormattedStDate) + " and " + strselDocDate + "<=" + string.Format(" Date({0})", strFormattedEndDate);
                }
                else if (!string.IsNullOrEmpty(strFromCustomer) && !string.IsNullOrEmpty(strToCustomer) && string.IsNullOrEmpty(strMonthYear))
                {
                    crStatementOfAccount.RecordSelectionFormula = strselCusCode + " in '" + strFromCustomer + "' to '" + strToCustomer + "'";
                }
                else if (string.IsNullOrEmpty(strFromCustomer) && string.IsNullOrEmpty(strToCustomer) && !string.IsNullOrEmpty(strFromYrMonDay) && !string.IsNullOrEmpty(strToYrMonDay))
                {
                    crStatementOfAccount.RecordSelectionFormula = strselDocDate + ">=" + string.Format(" Date({0})", strFromYrMonDay) + " and " + strselDocDate + "<=" + string.Format(" Date({0})", strToYrMonDay);
                }
                else if (!string.IsNullOrEmpty(strFromCustomer) && !string.IsNullOrEmpty(strToCustomer) && !string.IsNullOrEmpty(strMonthYear) && string.IsNullOrEmpty(strFromYrMonDay))
                {
                    crStatementOfAccount.RecordSelectionFormula = strselCusCode + " in '" + strFromCustomer + "' to '" + strToCustomer + "' and " + strselDocDate + ">=" + string.Format(" Date({0})", strFromYrMonDay) + " and " + strselDocDate + "<=" + string.Format(" Date({0})", strToYrMonDay);
                }
                else if (!string.IsNullOrEmpty(strFromCustomer) && string.IsNullOrEmpty(strToCustomer) && !string.IsNullOrEmpty(strMonthYear))
                {
                    crStatementOfAccount.RecordSelectionFormula = strselCusCode + " = '" + strFromCustomer + "'  and " + strselDocDate + ">=" + string.Format(" Date({0})", strFromYrMonDay) + " and " + strselDocDate + "<=" + string.Format(" Date({0})", strToYrMonDay);
                }

            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Populate Customer & Townwise Report
        protected void fnCustomerTown(string strMonYr, string strCustomerType, string strEndDate_DDMM, string strFromCustomer, string strToCustomer, string strBranchCode, string strMonthYear, string strStartDate_YYMM, string strEndDate_MMDD)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "fnCustomerTown()", "Statement Of Account Custmer and Townwise Method");
            try
            {
                Database ImpalDB = DataAccess.GetDatabaseBackUp();
                DbCommand cmd = null;

                if (strFromCustomer == "0")
                { strFromCustomer = string.Empty; }
                if (strToCustomer == "0")
                { strToCustomer = string.Empty; }

                if (ddlCustomerType.SelectedValue == "Branch")
                {
                    if (ddlReportType.SelectedValue == "Report-Customer Wise")
                    {
                        crStatementOfAccount.ReportName = "Impal-report-State-Acc-br";
                    }

                    strselAccCode = "{outstanding_br.Branch_Code}";
                    selectionformula = "{outstanding_br.Branch_Code}" + "='" + strBranchCode + "' and ";
                }
                else if (ddlCustomerType.SelectedValue == "Customer")
                {
                    if (ddlReportType.SelectedValue == "Report-Customer Wise")
                    {
                        crStatementOfAccount.ReportName = "Impal-report-State-Acc-cust";
                    }

                    strselAccCode = "{outstanding.Customer_Code}";
                    selectionformula = "{outstanding.Branch_Code}" + "='" + strBranchCode + "' and ";
                }
                else
                {
                    if (ddlReportType.SelectedValue == "Report-Customer Wise")
                    {
                        crStatementOfAccount.ReportName = "Impal-report-State-Acc-supp";
                    }

                    strselAccCode = "{outstanding_Supp.Supplier_Code}";
                    selectionformula = "{outstanding_Supp.Branch_Code}" + "='" + strBranchCode + "' and ";
                }

                if (!string.IsNullOrEmpty(strMonYr) && strCustomerType == "Branch")
                {
                    cmd = ImpalDB.GetStoredProcCommand("usp_calbros");
                    ImpalDB.AddInParameter(cmd, "@month_year", DbType.String, strEndDate_DDMM);
                    ImpalDB.AddInParameter(cmd, "@from_br", DbType.String, strFromCustomer.Trim());
                    ImpalDB.AddInParameter(cmd, "@To_br", DbType.String, strToCustomer.Trim());
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);
                }
                else if (!string.IsNullOrEmpty(strMonYr) && strCustomerType == "Customer")
                {
                    cmd = ImpalDB.GetStoredProcCommand("usp_calcustos");
                    ImpalDB.AddInParameter(cmd, "@month_year", DbType.String, strEndDate_DDMM);
                    ImpalDB.AddInParameter(cmd, "@from_Cus", DbType.String, strFromCustomer.Trim());
                    ImpalDB.AddInParameter(cmd, "@To_Cus", DbType.String, strToCustomer.Trim());
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);
                }
                else if (!string.IsNullOrEmpty(strMonYr) && strCustomerType == "Supplier")
                {
                    cmd = ImpalDB.GetStoredProcCommand("usp_calsupos");
                    ImpalDB.AddInParameter(cmd, "@month_year", DbType.String, strEndDate_DDMM);
                    ImpalDB.AddInParameter(cmd, "@from_sup", DbType.String, strFromCustomer.Trim());
                    ImpalDB.AddInParameter(cmd, "@To_sup", DbType.String, strToCustomer.Trim());
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);
                }

                string[] split = null;
                string[] split1 = null;
                string strFormattedStDate = null;
                string strFormattedEndDate = null;
                

                if (!string.IsNullOrEmpty(strMonthYear))
                {
                    split = strStartDate_YYMM.Split('/'); //strStartDate_YYMM
                    strFormattedStDate = string.Format("{0},{1},{2}", split[0], split[1], split[2]);
                    split1 = strEndDate_MMDD.Split('/'); //strEndDate_MMDD
                    strFormattedEndDate = string.Format("{0},{1},{2}", split1[0], split1[1], split1[2]);
                }

                if ((!string.IsNullOrEmpty(strFromCustomer) && strFromCustomer != "0") && string.IsNullOrEmpty(strToCustomer) && string.IsNullOrEmpty(strMonthYear))
                {
                    selectionformula = selectionformula + strselAccCode + "='" + strFromCustomer + "'";
                }
                else if ((string.IsNullOrEmpty(strFromCustomer) || strFromCustomer == "0") && string.IsNullOrEmpty(strToCustomer) && !string.IsNullOrEmpty(strMonthYear))
                {
                    selectionformula = selectionformula + strselDocDate + ">= Date(" + strFormattedStDate + ") and " + strselDocDate + "<= Date(" + strFormattedEndDate + ")";
                }
                else if ((!string.IsNullOrEmpty(strFromCustomer) && strFromCustomer != "0") && !string.IsNullOrEmpty(strToCustomer) && string.IsNullOrEmpty(strMonthYear))
                {
                    selectionformula = selectionformula + strselAccCode + " in '" + strFromCustomer + "' to '" + strToCustomer + "'";
                }
                else if ((!string.IsNullOrEmpty(strFromCustomer) && strFromCustomer != "0") && !string.IsNullOrEmpty(strToCustomer) && !string.IsNullOrEmpty(strMonthYear))
                {
                    selectionformula = selectionformula + strselAccCode + " in '" + strFromCustomer + "' to '" + strToCustomer + "' and" + strselDocDate + ">= Date(" + strFormattedStDate + ") and " + strselDocDate + "<= Date(" + strFormattedEndDate + ")";
                }
                else if ((!string.IsNullOrEmpty(strFromCustomer) && strFromCustomer != "0") && string.IsNullOrEmpty(strToCustomer) && !string.IsNullOrEmpty(strMonthYear))
                {
                    selectionformula = selectionformula + strselAccCode + " ='" + strFromCustomer + "' and " + strselDocDate + ">= Date(" + strFormattedStDate + ") and " + strselDocDate + "<= Date(" + strFormattedEndDate + ")";
                }

                crStatementOfAccount.RecordSelectionFormula = selectionformula;
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        protected void fnSalesman(string strMonYr, string strCustomerType, string strEndDate_DDMM, string strFromCustomer, string strToCustomer, string strBranchCode, string strMonthYear, string strStartDate_YYMM, string strEndDate_MMDD)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "fnCustomerTown()", "Statement Of Account Custmer and Townwise Method");
            try
            {
                Database ImpalDB = DataAccess.GetDatabaseBackUp();
                DbCommand cmd = null;

                if (strFromCustomer == "0")
                { strFromCustomer = string.Empty; }
                if (strToCustomer == "0")
                { strToCustomer = string.Empty; }

                strselAccCode = "{sales_order_header.sales_man_code}";

                cmd = ImpalDB.GetStoredProcCommand("usp_calcustsm");
                ImpalDB.AddInParameter(cmd, "@month_year", DbType.String, strEndDate_DDMM);
                ImpalDB.AddInParameter(cmd, "@from_Salesman", DbType.String, strFromCustomer.Trim());
                ImpalDB.AddInParameter(cmd, "@to_Salesman", DbType.String, strToCustomer.Trim());
                ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                ImpalDB.ExecuteNonQuery(cmd);                

                string[] split = null;
                string[] split1 = null;
                string strFormattedStDate = null;
                string strFormattedEndDate = null;
                if (!string.IsNullOrEmpty(strMonthYear))
                {
                    split = strStartDate_YYMM.Split('/'); //strStartDate_YYMM
                    strFormattedStDate = string.Format("{0},{1},{2}", split[0], split[1], split[2]);
                    split1 = strEndDate_MMDD.Split('/'); //strEndDate_MMDD
                    strFormattedEndDate = string.Format("{0},{1},{2}", split1[0], split1[1], split1[2]);
                }

                if ((!string.IsNullOrEmpty(strFromCustomer) && strFromCustomer != "0") && string.IsNullOrEmpty(strToCustomer) && string.IsNullOrEmpty(strMonthYear))
                {
                    crStatementOfAccount.RecordSelectionFormula = strselAccCode + "='" + strFromCustomer + "'";
                }
                else if ((string.IsNullOrEmpty(strFromCustomer) || strFromCustomer == "0") && string.IsNullOrEmpty(strToCustomer) && !string.IsNullOrEmpty(strMonthYear))
                {
                    crStatementOfAccount.RecordSelectionFormula = strselDocDate + ">= Date(" + strFormattedStDate + ") and " + strselDocDate + "<= Date(" + strFormattedEndDate + ")";

                }
                else if ((!string.IsNullOrEmpty(strFromCustomer) && strFromCustomer != "0") && !string.IsNullOrEmpty(strToCustomer) && string.IsNullOrEmpty(strMonthYear))
                {
                    crStatementOfAccount.RecordSelectionFormula = strselAccCode + " in '" + strFromCustomer + "' to '" + strToCustomer + "'";
                }
                else if ((!string.IsNullOrEmpty(strFromCustomer) && strFromCustomer != "0") && !string.IsNullOrEmpty(strToCustomer) && !string.IsNullOrEmpty(strMonthYear))
                {
                    crStatementOfAccount.RecordSelectionFormula = strselAccCode + " in '" + strFromCustomer + "' to '" + strToCustomer + "' and" + strselDocDate + ">= Date(" + strFormattedStDate + ") and " + strselDocDate + "<= Date(" + strFormattedEndDate + ")";
                }
                else if ((!string.IsNullOrEmpty(strFromCustomer) && strFromCustomer != "0") && string.IsNullOrEmpty(strToCustomer) && !string.IsNullOrEmpty(strMonthYear))
                {
                    crStatementOfAccount.RecordSelectionFormula = strselAccCode + " ='" + strFromCustomer + "' and " + strselDocDate + ">= Date(" + strFormattedStDate + ") and " + strselDocDate + "<= Date(" + strFormattedEndDate + ")";
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {

        }

        #region MonthYear Dropdown Changed Event
        protected void ddlMonthYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            //    Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //    //Log.WriteLog(Source, "ddlMonthYear_SelectedIndexChanged()", "Statement Of Account Month Year Dropdown Changed Method"); 
            //    try
            //    {
            //        if (ddlMonthYear.SelectedIndex != 0 && divCustomerInfo.Visible)
            //        {
            //            divCustomerInfo.Visible = false;
            //        }
            //    }
            //    catch (Exception exp)
            //    {
            //        IMPALLibrary.Log.WriteException(Source, exp);
            //    }
        }
        #endregion

    }
}
