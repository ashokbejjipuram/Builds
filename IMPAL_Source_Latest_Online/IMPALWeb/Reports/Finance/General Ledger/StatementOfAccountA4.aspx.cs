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
using System.Web.UI;
#endregion

namespace IMPALWeb.Reports.Finance.General_Ledger
{
    public partial class StatementOfAccountA4 : System.Web.UI.Page
    {
        string strBranchCode = string.Empty;
        string selectionformula = string.Empty;
        string strselAccCode = string.Empty;        

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
                    if (crStatementOfAccountA4 != null)
                    {
                        crStatementOfAccountA4.Dispose();
                        crStatementOfAccountA4 = null;
                    }

                    lblCustomer.Text = "Customer";

                    divCustomerInfo.Visible = false;
                    fnPopulateCustomerType();
                    fnPopulateCustomer();
                    fnPopulateReportType();
                    fnPopulateMonthYear();
                    ddlCustomer.SelectedIndex = 0;
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
            if (crStatementOfAccountA4 != null)
            {
                crStatementOfAccountA4.Dispose();
                crStatementOfAccountA4 = null;
            }
        }
        protected void crStatementOfAccountA4_Unload(object sender, EventArgs e)
        {
            if (crStatementOfAccountA4 != null)
            {
                crStatementOfAccountA4.Dispose();
                crStatementOfAccountA4 = null;
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
                ddlCustomerType.DataSource = oCommon.GetDropDownListValues("StatementOfAccountA4");
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

        #region Function to Populate customer
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
                    lstCompletion = oCustomerDtls.GetCustomers(strBranchCode, "StatementOfAccountA4");
                }
                else
                {
                    lstCompletion = oCustomerDtls.GetCustomers(strBranchCode, "Corporate");
                }
                ddlCustomer.DataSource = lstCompletion;
                ddlCustomer.DataTextField = "Name";
                ddlCustomer.DataValueField = "Code";
                ddlCustomer.DataBind();
                ddlCustomer.SelectedIndex = 0;
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Function to Populate customer
        public void fnPopulateSalesMan()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                List<CustomerDtls> lstCompletion = null;
                CustomerDetails oCustomerDtls = new CustomerDetails();
                lstCompletion = oCustomerDtls.GetSalesMan(strBranchCode);
                ddlCustomer.DataSource = lstCompletion;
                ddlCustomer.DataTextField = "Name";
                ddlCustomer.DataValueField = "Code";
                ddlCustomer.DataBind();
                ddlCustomer.SelectedIndex = 0;
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region populate Town
        public void fnPopulateTown()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "fnPopulateTown", "Inside fnPopulateTown");
            try
            {
                List<Town> lstCompletion = null;
                Towns town = new Towns();
                lstCompletion = town.GetAllTowns(strBranchCode);
                ddlCustomer.DataSource = lstCompletion;
                ddlCustomer.DataTextField = "TownName";
                ddlCustomer.DataValueField = "Towncode";
                ddlCustomer.DataBind();
                ddlCustomer.SelectedIndex = 0;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
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

        #region Function to Populate Branch
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
                ddlCustomer.DataSource = objBranch.GetCorpBranch();
                ddlCustomer.DataValueField = "BranchCode";
                ddlCustomer.DataTextField = "BranchName";
                ddlCustomer.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Function to Populate Supplier
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
                ddlCustomer.DataSource = objSupplier.GetAllSuppliers();
                ddlCustomer.DataValueField = "SupplierCode";
                ddlCustomer.DataTextField = "SupplierName";
                ddlCustomer.DataBind();
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
                    if (ddlCustomer.SelectedValue != "0" && ddlCustomer.SelectedValue != "")
                    {
                        divCustomerInfo.Visible = true;
                    }
                    else
                    {
                        divCustomerInfo.Visible = false;
                    }

                    lblCustomer.Text = "Customer";

                    fnPopulateCustomer();
                    ddlReportType.SelectedIndex = 0;
                    ddlReportType.Enabled = false;
                }
                else if (ddlCustomerType.SelectedValue == "SalesMan")
                {
                    divCustomerInfo.Visible = false;
                    lblCustomer.Text = "SalesMan";

                    fnPopulateSalesMan();
                    ddlReportType.SelectedIndex = 1;
                    ddlReportType.Enabled = false;
                }
                else if (ddlCustomerType.SelectedValue == "Town")
                {
                    divCustomerInfo.Visible = false;

                    lblCustomer.Text = "Town";

                    fnPopulateTown();
                    ddlReportType.SelectedIndex = 2;
                    ddlReportType.Enabled = false;
                }
                else
                {
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

        #region Customer and To Customer Dropdown Changed Event
        protected void ddlChangedEvent(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "ddlChangedEvent()", "Statement of Account Customer Dropdown Changed Event");
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
                    GetCustomerDetails(DrpDwn.SelectedValue);
                    divCustomerInfo.Visible = true;
                    ddlReportSubType.SelectedIndex = 0;
                    ddlReportSubType.Enabled = false;
                }
                else
                {
                    divCustomerInfo.Visible = false;
                    ddlReportSubType.Enabled = true;
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region To Populate Customer Details Menu
        protected void GetCustomerDetails(string CustomerCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "GetCustomerDetails()", "Statement Of Account Get Customer Details Method");
            try
            {
                CustomerDetails oDtls = new CustomerDetails();
                CustomerDtls oCustomer = null;
                oCustomer = oDtls.GetDetails(strBranchCode, CustomerCode);
                txtCustomerCode.Text = ddlCustomer.SelectedValue;
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
                if (ddlReportType.SelectedValue == "Report-Customer Wise")
                    crStatementOfAccountA4.ReportName = "Impal-report-State-Acc-custA4";
                else if (ddlReportType.SelectedValue == "Report-Town Wise")
                    crStatementOfAccountA4.ReportName = "Impal-report-State-Acc-townA4";
                else if (ddlReportType.SelectedValue == "Report-Salesman Wise")
                    crStatementOfAccountA4.ReportName = "Impal-report-State-Acc-salesmanA4";

                //Database ImpalDB = DataAccess.GetDatabase();
                //DbCommand cmd = null;
                //int timediff = 0;

                //cmd = ImpalDB.GetSqlStringCommand("select top 1 Datediff(ss, datestamp, GETDATE()) from Rpt_ExecCount_Daily WITH (NOLOCK) where BranchCode = '" + Session["BranchCode"].ToString() + "' and reportname = '" + crStatementOfAccountA4.ReportName + "' order by datestamp desc");
                //cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                //timediff = Convert.ToInt32(ImpalDB.ExecuteScalar(cmd));

                //if (timediff > 0 && timediff <= 600)
                //{
                //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('You Are Again Generating this Report With in no Time. Please Wait for 10 Minutes');", true);
                //    btnReportPDF.Attributes.Add("style", "display:none");
                //    btnReportExcel.Attributes.Add("style", "display:none");
                //    btnBack.Attributes.Add("style", "display:inline");
                //    btnReport.Attributes.Add("style", "display:none");
                //    return;
                //}
                //else
                //{
                    ReportsData reportsDt = new ReportsData();
                    reportsDt.UpdRptExecCount(Session["BranchCode"].ToString(), Session["UserID"].ToString(), crStatementOfAccountA4.ReportName);
                //}

                string strMonYr = string.Empty;
                string strEndDate_DDMM = string.Empty;
                string strEndDate_MMDD = string.Empty;
                string strStartDate_YYMM = string.Empty;
                string strMonthYear = ddlMonthYear.SelectedValue;
                string strCustomerType = ddlCustomerType.SelectedValue;
                string strCustomer = ddlCustomer.SelectedValue;

                if (ddlMonthYear.SelectedValue != "")
                {
                    string strFormattedMonYr = string.Format("{0}/{1}", strMonthYear.Substring(0, 2), strMonthYear.Substring(2, 4));

                    strMonYr = string.Format("{0}/{1}/{2}", DateTime.DaysInMonth(Convert.ToInt32(strMonthYear.Substring(2, 4)), Convert.ToInt32(strMonthYear.Substring(0, 2))), strMonthYear.Substring(0, 2), strMonthYear.Substring(2, 4)); //strMonYr
                    strEndDate_DDMM = string.Format("{0}/{1}/{2}", strMonthYear.Substring(0, 2), DateTime.DaysInMonth(Convert.ToInt32(strMonthYear.Substring(2, 4)), Convert.ToInt32(strMonthYear.Substring(0, 2))), strMonthYear.Substring(2, 4));
                    strEndDate_MMDD = string.Format("{0}/{1}/{2}", strMonthYear.Substring(2, 4), strMonthYear.Substring(0, 2), DateTime.DaysInMonth(Convert.ToInt32(strMonthYear.Substring(2, 4)), Convert.ToInt32(strMonthYear.Substring(0, 2))));
                    strStartDate_YYMM = string.Format("{0}/{1}/{2}", strMonthYear.Substring(2, 4), strMonthYear.Substring(0, 2), "01");
                }

                fnCustomerTown(strMonYr, strCustomerType, strEndDate_DDMM, strCustomer, strBranchCode, strMonthYear, strStartDate_YYMM, strEndDate_MMDD);
                PanelHeaderDtls.Enabled = false;

                btnReportPDF.Attributes.Add("style", "display:inline");
                btnReportExcel.Attributes.Add("style", "display:inline");
                btnBack.Attributes.Add("style", "display:inline");
                btnReport.Attributes.Add("style", "display:none");

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Report Has been Generated Successfully. Please Click on Below Button to Export in Respective Format');", true);
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion
        protected void fnCustomerTown(string strMonYr, string strCustomerType, string strEndDate_DDMM, string strCustomer, string strBranchCode, string strMonthYear, string strStartDate_YYMM, string strEndDate_MMDD)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "fnCustomerTown()", "Statement Of Account Custmer and Townwise Method");
            try
            {
                Database ImpalDB = DataAccess.GetDatabaseBackUp();
                DbCommand cmd = null;

                if (strCustomer == "0")
                { strCustomer = string.Empty; }

                if (strCustomerType == "Customer")
                {
                    cmd = ImpalDB.GetStoredProcCommand("usp_calcustos");
                    ImpalDB.AddInParameter(cmd, "@month_year", DbType.String, strEndDate_DDMM);
                    ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, strCustomer.Trim());
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);
                }
                else if (strCustomerType == "SalesMan")
                {
                    cmd = ImpalDB.GetStoredProcCommand("usp_calcustos_SalesManWise");
                    ImpalDB.AddInParameter(cmd, "@month_year", DbType.String, strEndDate_DDMM);
                    ImpalDB.AddInParameter(cmd, "@Salesman_Code", DbType.String, strCustomer.Trim());
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);
                }
                else if (strCustomerType == "Town")
                {
                    cmd = ImpalDB.GetStoredProcCommand("usp_calcustos_TownWise");
                    ImpalDB.AddInParameter(cmd, "@month_year", DbType.String, strEndDate_DDMM);
                    ImpalDB.AddInParameter(cmd, "@Town_Code", DbType.String, strCustomer.Trim());
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
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

        protected void GenerateAndExportReport(string fileType)
        {
            string strMonYr = string.Empty;
            string strEndDate_DDMM = string.Empty;
            string strEndDate_MMDD = string.Empty;
            string strStartDate_YYMM = string.Empty;
            string strMonthYear = ddlMonthYear.SelectedValue;
            string[] split = null;
            string[] split1 = null;
            string strFormattedStDate = null;
            string strFormattedEndDate = null;
            string strFormattedMonYr = string.Format("{0}/{1}", strMonthYear.Substring(0, 2), strMonthYear.Substring(2, 4));

            strMonYr = string.Format("{0}/{1}/{2}", DateTime.DaysInMonth(Convert.ToInt32(strMonthYear.Substring(2, 4)), Convert.ToInt32(strMonthYear.Substring(0, 2))), strMonthYear.Substring(0, 2), strMonthYear.Substring(2, 4)); //strMonYr
            strEndDate_DDMM = string.Format("{0}/{1}/{2}", strMonthYear.Substring(0, 2), DateTime.DaysInMonth(Convert.ToInt32(strMonthYear.Substring(2, 4)), Convert.ToInt32(strMonthYear.Substring(0, 2))), strMonthYear.Substring(2, 4));
            strEndDate_MMDD = string.Format("{0}/{1}/{2}", strMonthYear.Substring(2, 4), strMonthYear.Substring(0, 2), DateTime.DaysInMonth(Convert.ToInt32(strMonthYear.Substring(2, 4)), Convert.ToInt32(strMonthYear.Substring(0, 2))));
            strStartDate_YYMM = string.Format("{0}/{1}/{2}", strMonthYear.Substring(2, 4), strMonthYear.Substring(0, 2), "01");

            split = strStartDate_YYMM.Split('/'); //strStartDate_YYMM
            strFormattedStDate = string.Format("{0},{1},{2}", split[0], split[1], split[2]);
            split1 = strEndDate_MMDD.Split('/'); //strEndDate_MMDD
            strFormattedEndDate = string.Format("{0},{1},{2}", split1[0], split1[1], split1[2]);

            selectionformula = "{outstanding.Branch_Code}='" + strBranchCode + "'";

            if (ddlReportSubType.SelectedValue == "N")
                selectionformula = selectionformula + " and {outstanding.Amount} = 0";
            else if (ddlReportSubType.SelectedValue == "O")
                selectionformula = selectionformula + " and {outstanding.Amount} > 0";
            else if (ddlReportSubType.SelectedValue == "C")
                selectionformula = selectionformula + " and {outstanding.Amount} < 0";

            if (ddlReportType.SelectedValue == "Report-Customer Wise")
                crStatementOfAccountA4.ReportName = "Impal-report-State-Acc-custA4";
            else if (ddlReportType.SelectedValue == "Report-Town Wise")
                crStatementOfAccountA4.ReportName = "Impal-report-State-Acc-townA4";
            else if (ddlReportType.SelectedValue == "Report-Salesman Wise")
                crStatementOfAccountA4.ReportName = "Impal-report-State-Acc-salesmanA4";

            crStatementOfAccountA4.RecordSelectionFormula = selectionformula;
            crStatementOfAccountA4.GenerateReportAndExportA4HO(fileType);
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

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Server.ClearError();
                Response.Redirect("StatementOfAccountA4.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}