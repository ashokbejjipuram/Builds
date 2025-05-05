#region Namespace Declaration
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Common;
using IMPALLibrary;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using IMPALLibrary.Common;
using IMPALLibrary.Masters.Sales;
#endregion

namespace IMPALWeb.Reports.Sales.Sales_Comparison
{

    public partial class MonthWise : System.Web.UI.Page
    {
        string sessionvalue = string.Empty;

        #region Page init
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
                if (!IsPostBack)
                {
                    sessionvalue = (string)Session["BranchCode"];

                    if (crmonthwise != null)
                    {
                        crmonthwise.Dispose();
                        crmonthwise = null;
                    }

                    fnPopulateReportList();
                    fnPopulateAccountingPeriod();
                    fnPopulateCustomerCode();                  
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
            if (crmonthwise != null)
            {
                crmonthwise.Dispose();
                crmonthwise = null;
            }
        }
        protected void crmonthwise_Unload(object sender, EventArgs e)
        {
            if (crmonthwise != null)
            {
                crmonthwise.Dispose();
                crmonthwise = null;
            }
        }

        #region Populate Report List Dropdown
        private void fnPopulateReportList()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                ddlReportType.DataSource = oCommon.GetDropDownListValues("MonthWise");
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

        #region Populate Customer Code Dropdown
        private void fnPopulateCustomerCode()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                SalesOrderHeaders soHeader = new SalesOrderHeaders();
                cbocustomer.DataSource = soHeader.GetSalesOrder("CRP"); ;
                cbocustomer.DataValueField = "customer_code";
                cbocustomer.DataTextField = "customer_name";
                cbocustomer.DataBind();
                cbocustomer.SelectedIndex = 0;
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Populate Accounting Period Dropdown
        private void fnPopulateAccountingPeriod()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                AccountingPeriods Acc = new AccountingPeriods();
                ddaccountingperiod.DataSource = Acc.GetAccountingPeriod(20, null, sessionvalue);
                ddaccountingperiod.DataValueField = "AccPeriodCode";
                ddaccountingperiod.DataTextField = "Desc";
                ddaccountingperiod.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Report Button Click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Main mainmaster = (Main)Page.Master;
                mainmaster.ShowHideFilters(btnReport, reportFiltersTable, reportViewerHolder);
                if (btnReport.Text == "Back")
                {
                    string selectionformula = string.Empty;
                    string sel1 = "{month_sales.accounting_period_code}";
                    string sel2 = "{month_sales.branch_code}=";
                    string sel4 = "{branch_master.branch_code}";
                    string sel5 = "{V_SalesReports.document_date}";
                    string sel6 = "{V_SalesReports.customer_code}";
                    string sel7 = "{V_SalesReports.Supplier_Code}";
                    string sel8 = "{V_SalesReports.Branch_Code}";
                    string prevdt = string.Empty;
                    string date1 = string.Empty;
                    string date2 = string.Empty;
                    string date3 = string.Empty;
                    string date4 = string.Empty;
                    string prevtodt = string.Empty;
                    string DFrom_param = string.Empty;
                    string DTo_param = string.Empty;
                    string prevyr1 = string.Empty;
                    string prevyr2 = string.Empty;
                    string a = string.Empty;
                    string currdate = string.Empty;
                    string prevfromdt = string.Empty;

                    if (Session["BranchCode"] != null)
                        sessionvalue = (string)Session["BranchCode"];

                    Database ImpalDB = DataAccess.GetDatabase();

                    if (ddlReportType.SelectedValue == "5")
                    {
                        crmonthwise.ReportName = "impal_reports_monthwisecompchk";
                    }
                    else if (ddlReportType.SelectedValue == "4")
                    {
                        crmonthwise.ReportName = "sales_stmt_Branch_Summary";
                    }
                    else if (ddlReportType.SelectedValue == "3")
                    {
                        crmonthwise.ReportName = "sales_stmt_Town_Summary";
                    }
                    else if (ddlReportType.SelectedValue == "2")
                    {
                        crmonthwise.ReportName = "Sales_Stmt_Cust_line_Summary";
                    }
                    else if (ddlReportType.SelectedValue == "1")
                    {
                        crmonthwise.ReportName = "sales_stmt_Cust_line_Detail";
                    }
                    else if (ddlReportType.SelectedValue == "0")
                    {
                        crmonthwise.ReportName = "Sales_Stmt_line_Summary";
                    }

                    if (ddlReportType.SelectedValue == "5")
                    {
                        DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_monthsales");
                        ImpalDB.AddInParameter(cmd, "@br_code", DbType.String, sessionvalue);
                        ImpalDB.AddInParameter(cmd, "@ap_Code", DbType.String, ddaccountingperiod.SelectedValue);
                        cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDB.ExecuteNonQuery(cmd);

                        string sSqlAccPeriod = "select distinct a.accounting_period_code,description from accounting_period a, month_sales b where a.accounting_period_code = b.accounting_period_code order by a.accounting_period_code";
                        string sSqlAcc = "select distinct a.accounting_period_code,description from accounting_period a, month_sales b where a.accounting_period_code = b.accounting_period_code order by a.accounting_period_code desc";
                        string value_prevdes = string.Empty;
                        string valuedes = string.Empty;
                        DbCommand cmdC = ImpalDB.GetSqlStringCommand(sSqlAccPeriod);
                        cmdC.CommandTimeout = ConnectionTimeOut.TimeOut;
                        using (IDataReader readerAccPeriod = ImpalDB.ExecuteReader(cmdC))
                            if (readerAccPeriod != null)
                            {
                                while (readerAccPeriod.Read())
                                { value_prevdes = readerAccPeriod[1].ToString(); }
                            }
                        DbCommand cmdB = ImpalDB.GetSqlStringCommand(sSqlAcc);
                        cmdB.CommandTimeout = ConnectionTimeOut.TimeOut;
                        using (IDataReader readerAccPeriodprev = ImpalDB.ExecuteReader(cmdB))
                            if (readerAccPeriodprev != null)
                            {
                                while (readerAccPeriodprev.Read())
                                { valuedes = readerAccPeriodprev[1].ToString(); }
                            }
                        crmonthwise.CrystalFormulaFields.Add("AccDesc", "\"" + value_prevdes + "\"");
                        crmonthwise.CrystalFormulaFields.Add("accprevdes", "\"" + valuedes + "\"");

                        if (sessionvalue != "CRP")
                        { selectionformula = sel2 + "'" + sessionvalue + "'"; }

                    }
                    else
                    {
                        string ssql = "select convert(varchar(10),start_date,103),convert(varchar(10),End_date,103),convert(varchar(10),dateadd(yy,-1,start_date),103),convert(varchar(10),dateadd(yy,-1,End_date),103) from accounting_period where accounting_period_code =" + ddaccountingperiod.SelectedValue;
                        DbCommand cmdA = ImpalDB.GetSqlStringCommand(ssql);
                        cmdA.CommandTimeout = ConnectionTimeOut.TimeOut;
                        using (IDataReader reader = ImpalDB.ExecuteReader(cmdA))
                            if (reader != null)
                            {
                                while (reader.Read())
                                {
                                    date1 = reader[0].ToString();
                                    date2 = reader[1].ToString();
                                    date3 = reader[2].ToString();
                                    date4 = reader[3].ToString();
                                }
                            }

                        a = Convert.ToString(Convert.ToInt32(date2.Substring(6)) % 4);
                        currdate = (date2.Split('/').ElementAt(1));

                        if (Convert.ToInt32(a) > 0)
                        {
                            if (currdate == "29")
                            {
                                prevdt = Convert.ToString(Convert.ToInt32(date2.Substring(6)) - 1);
                            }
                            else
                            {
                                prevdt = (date2.Split('/').ElementAt(2));
                            }
                        }

                        prevfromdt = "Date (" + DateTime.ParseExact(date3, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";

                        if (Convert.ToInt32(a) == 0)
                        {
                            prevtodt = "Date (" + DateTime.ParseExact(date4, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";

                        }
                        else
                        {
                            prevtodt = "Date (" + DateTime.ParseExact(date4, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
                        }

                        DFrom_param = "Date (" + DateTime.ParseExact(date1, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
                        DTo_param = "Date (" + DateTime.ParseExact(date2, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";

                        string prevaccperiod = Convert.ToString(Convert.ToInt32(ddaccountingperiod.SelectedValue) - 1);
                        string sSqlAccPeriod = "select distinct a.accounting_period_code,description from accounting_period a  where accounting_period_code ='" + prevaccperiod + "'";
                        string sSqlAcc = "select distinct a.accounting_period_code,description from accounting_period a  where accounting_period_code ='" + ddaccountingperiod.SelectedValue + "'";
                        string value_prevdes = string.Empty;
                        string valuedes = string.Empty;
                        DbCommand cmd = ImpalDB.GetSqlStringCommand(sSqlAccPeriod);
                        cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                        using (IDataReader readerAccPeriod = ImpalDB.ExecuteReader(cmd))
                            if (readerAccPeriod != null)
                            {

                                while (readerAccPeriod.Read())
                                {
                                    value_prevdes = readerAccPeriod[1].ToString();

                                }
                            }
                        DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSqlAcc);
                        cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                        using (IDataReader readerAccPeriodprev = ImpalDB.ExecuteReader(cmd1))
                            if (readerAccPeriodprev != null)
                            {
                                while (readerAccPeriodprev.Read())
                                { valuedes = readerAccPeriodprev[1].ToString(); }
                            }
                        crmonthwise.CrystalFormulaFields.Add("AccDesc", "\"" + valuedes + "\"");
                        crmonthwise.CrystalFormulaFields.Add("accprevdes", "\"" + value_prevdes + "\"");

                        if (sessionvalue != "CRP")
                        {
                            if ((cbocustomer.SelectedIndex == -1 || cbocustomer.SelectedIndex == 0) && ddsupplier.SelectedIndex == 0)
                            {
                                selectionformula = "(" + sel8 + "='" + sessionvalue + "' and ((" + sel5 + " >=" + prevfromdt + " and " + sel5 + " <= " + prevtodt + " ) or (" + sel5 + " >=" + DFrom_param + " and " + sel5 + " <= " + DTo_param + " )))";
                            }
                            else if ((cbocustomer.SelectedIndex == -1 || cbocustomer.SelectedIndex == 0) && ddsupplier.SelectedIndex != 0)
                            {
                                selectionformula = "(" + sel8 + "='" + sessionvalue + "' and " + sel7 + "='" + ddsupplier.SelectedValue + "' and ((" + sel5 + " >=" + prevfromdt + " and " + sel5 + " <= " + prevtodt + " ) or (" + sel5 + " >=" + DFrom_param + " and " + sel5 + " <= " + DTo_param + " )))";
                            }
                            else if ((cbocustomer.SelectedIndex != -1 || cbocustomer.SelectedIndex != 0) && ddsupplier.SelectedIndex == 0)
                            {
                                selectionformula = "(" + sel8 + "='" + sessionvalue + "' and " + sel6 + "='" + cbocustomer.SelectedValue + "' and ((" + sel5 + " >=" + prevfromdt + " and " + sel5 + " <= " + prevtodt + " ) or (" + sel5 + " >=" + DFrom_param + " and " + sel5 + " <= " + DTo_param + " )))";

                            }
                            else if ((cbocustomer.SelectedIndex != -1 || cbocustomer.SelectedIndex != 0) && ddsupplier.SelectedIndex != 0)
                            {
                                selectionformula = "(" + sel8 + "='" + sessionvalue + "' and " + sel7 + "='" + ddsupplier.SelectedValue + "' and " + sel6 + "='" + cbocustomer.SelectedValue + "' and ((" + sel5 + " >=" + prevfromdt + " and " + sel5 + " <= " + prevtodt + " ) or (" + sel5 + " >=" + DFrom_param + " and " + sel5 + " <= " + DTo_param + " )))";

                            }
                        }
                        else
                        {
                            if ((cbocustomer.SelectedIndex == -1 || cbocustomer.SelectedIndex == 0) && ddsupplier.SelectedIndex == 0)
                            {
                                selectionformula = "(" + " ((" + sel5 + " >=" + prevfromdt + " and " + sel5 + " <= " + prevtodt + " ) or (" + sel5 + " >=" + DFrom_param + " and " + sel5 + " <= " + DTo_param + " )))";
                            }
                            else if ((cbocustomer.SelectedIndex == -1 || cbocustomer.SelectedIndex == 0) && ddsupplier.SelectedIndex != 0)
                            {
                                selectionformula = "(" + sel7 + "='" + ddsupplier.SelectedValue + "' and ((" + sel5 + " >=" + prevfromdt + " and " + sel5 + " <= " + prevtodt + " ) or (" + sel5 + " >=" + DFrom_param + " and " + sel5 + " <= " + DTo_param + " )))";
                            }
                            else if ((cbocustomer.SelectedIndex != -1 || cbocustomer.SelectedIndex != 0) && ddsupplier.SelectedIndex == 0)
                            {
                                selectionformula = "(" + " " + sel6 + "='" + cbocustomer.SelectedValue + "' and ((" + sel5 + " >=" + prevfromdt + " and " + sel5 + " <= " + prevtodt + " ) or (" + sel5 + " >=" + DFrom_param + " and " + sel5 + " <= " + DTo_param + " )))";

                            }
                            else if ((cbocustomer.SelectedIndex != -1 || cbocustomer.SelectedIndex != 0) && ddsupplier.SelectedIndex != 0)
                            {
                                selectionformula = "(" + " " + sel7 + "='" + ddsupplier.SelectedValue + "' and " + sel6 + "='" + cbocustomer.SelectedValue + "' and ((" + sel5 + " >=" + prevfromdt + " and " + sel5 + " <= " + prevtodt + " ) or (" + sel5 + " >=" + DFrom_param + " and " + sel5 + " <= " + DTo_param + " )))";

                            }
                        }
                    }

                    //crmonthwise.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
                    //crmonthwise.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
                    crmonthwise.RecordSelectionFormula = selectionformula;
                    crmonthwise.GenerateReport();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion
    }
}
