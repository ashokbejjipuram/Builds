#region Namespace Declaration
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Masters.Sales;
using IMPALLibrary;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
#endregion

namespace IMPALWeb.Reports.Ordering.StockComparison
{
    public partial class AnnualManager1 : System.Web.UI.Page
    {
        string sessionbrchcode = string.Empty;

        #region Page Init
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
                    sessionbrchcode = (string)Session["BranchCode"];
                if (!IsPostBack)
                {
                    if (crAnnualManager1 != null)
                    {
                        crAnnualManager1.Dispose();
                        crAnnualManager1 = null;
                    }

                    fnPopulateAccountingPeriod();
                    fnPopulateAreaManager();
                    fnPopulateSupplier();
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
            if (crAnnualManager1 != null)
            {
                crAnnualManager1.Dispose();
                crAnnualManager1 = null;
            }
        }
        protected void crAnnualManager1_Unload(object sender, EventArgs e)
        {
            if (crAnnualManager1 != null)
            {
                crAnnualManager1.Dispose();
                crAnnualManager1 = null;
            }
        }

        #region Populate Accounting Period Dropdown
        private void fnPopulateAccountingPeriod()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                AccountingPeriods accperiod = new AccountingPeriods();
                ddlAccountingPeriod.DataSource = accperiod.GetAccountingPeriod(20, null, sessionbrchcode);
                ddlAccountingPeriod.DataTextField = "Desc";
                ddlAccountingPeriod.DataValueField = "AccPeriodCode";
                ddlAccountingPeriod.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Populate Area Manager Dropdown
        private void fnPopulateAreaManager()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                IMPALLibrary.AreaManagers AreaMan = new IMPALLibrary.AreaManagers();
                ddlAreaManager.DataSource = AreaMan.GetAreaManagers();
                ddlAreaManager.DataTextField = "AreaManagerName";
                ddlAreaManager.DataValueField = "AreaManagerCode";
                ddlAreaManager.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Populate Supplier Dropdown
        private void fnPopulateSupplier()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Suppliers supp = new Suppliers();
                ddlLineCode.DataSource = supp.GetAllLinewiseSuppliers("CRP");
                ddlLineCode.DataValueField = "SupplierCode";
                ddlLineCode.DataTextField = "SupplierName";
                ddlLineCode.DataBind();
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
                    string strsel1 = "{line_wise_sales.month_year}=";
                    string strsel2 = "{Area_Manager.Area_Manager_code}=";
                    string strsel3 = "{line_wise_sales.branch_code}=";
                    string strsel4 = "{line_wise_sales.accounting_period_code}=";
                    string strsel5 = "mid({line_wise_sales.Supplier_Line_Code},1,3)=";
                    string selectionformula = string.Empty;
                    string value1 = ddlAccountingPeriod.SelectedValue;
                    string value2 = Convert.ToString((Convert.ToInt32(ddlAccountingPeriod.SelectedValue) - 1));
                    string value3 = Convert.ToString((Convert.ToInt32(ddlAccountingPeriod.SelectedValue) - 2));

                    Database ImpalDB = DataAccess.GetDatabase();
                    DbCommand cmd = ImpalDB.GetSqlStringCommand("select convert(nvarchar,end_date,101) from accounting_period where accounting_period_code=" + value1);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    string str = (string)ImpalDB.ExecuteScalar(cmd);

                    string[] temp1 = str.Split('/');
                    string date1 = temp1[0] + temp1[2];

                    DbCommand cmd1 = ImpalDB.GetSqlStringCommand("select 1 from line_wise_sales where accounting_period_code=" + value1);
                    cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                    string strdate3 = Convert.ToString(ImpalDB.ExecuteScalar(cmd1));
                    if (strdate3 != "0")
                    {
                        DbCommand cmd2 = ImpalDB.GetSqlStringCommand("select 1 from line_wise_sales where month_year=" + date1);
                        cmd2.CommandTimeout = ConnectionTimeOut.TimeOut;
                        string strdate4 = Convert.ToString(ImpalDB.ExecuteScalar(cmd2));
                        if (strdate4 == "0")
                        {

                            string ssql = "select  substring((select max(substring(month_year,3,4)+substring(month_year,1,2))from line_wise_sales where accounting_period_code =" + value1 + "),5,2)+ substring((select max(substring(month_year,3,4)+substring(month_year,1,2)) from line_wise_sales where accounting_period_code= " + value1 + "),1,4) ";
                            DbCommand cmd3 = ImpalDB.GetSqlStringCommand(ssql);
                            cmd3.CommandTimeout = ConnectionTimeOut.TimeOut;
                            date1 = Convert.ToString(ImpalDB.ExecuteScalar(cmd3));
                        }

                    }
                    else
                    { date1 = "0"; }

                    DbCommand cmd4 = ImpalDB.GetSqlStringCommand("select convert(nvarchar,end_date,101) from accounting_period where accounting_period_code=" + value2);
                    cmd4.CommandTimeout = ConnectionTimeOut.TimeOut;
                    string str1 = (string)ImpalDB.ExecuteScalar(cmd4);

                    string[] temp2 = str1.Split('/');
                    string date2 = temp2[0] + temp2[2];

                    DbCommand cmd5 = ImpalDB.GetSqlStringCommand("select 1 from line_wise_sales where accounting_period_code=" + value2);
                    cmd5.CommandTimeout = ConnectionTimeOut.TimeOut;
                    string strdate5 = Convert.ToString(ImpalDB.ExecuteScalar(cmd5));
                    if (strdate5 != "0")
                    {
                        DbCommand cmd6 = ImpalDB.GetSqlStringCommand("select 1 from line_wise_sales where month_year=" + date2);
                        cmd6.CommandTimeout = ConnectionTimeOut.TimeOut;
                        string strdate6 = Convert.ToString(ImpalDB.ExecuteScalar(cmd6));
                        if (strdate6 == "0")
                        {

                            string ssql = "select  substring((select max(substring(month_year,3,4)+substring(month_year,1,2))from line_wise_sales where accounting_period_code =" + value1 + "),5,2)+ substring((select max(substring(month_year,3,4)+substring(month_year,1,2)) from line_wise_sales where accounting_period_code= " + value2 + "),1,4) ";
                            DbCommand cmd7 = ImpalDB.GetSqlStringCommand(ssql);
                            cmd7.CommandTimeout = ConnectionTimeOut.TimeOut;
                            date2 = Convert.ToString(ImpalDB.ExecuteScalar(cmd7));
                        }

                    }
                    else
                    { date2 = "0"; }

                    DbCommand cmd8 = ImpalDB.GetSqlStringCommand("select convert(nvarchar,end_date,101) from accounting_period where accounting_period_code=" + value3);
                    cmd8.CommandTimeout = ConnectionTimeOut.TimeOut;
                    string str2 = (string)ImpalDB.ExecuteScalar(cmd8);

                    string[] temp3 = str2.Split('/');
                    string date3 = temp3[0] + temp3[2];

                    DbCommand cmd9 = ImpalDB.GetSqlStringCommand("select 1 from line_wise_sales where accounting_period_code=" + value3);
                    cmd9.CommandTimeout = ConnectionTimeOut.TimeOut;
                    string strdate7 = Convert.ToString(ImpalDB.ExecuteScalar(cmd9));
                    if (strdate5 != "0")
                    {
                        DbCommand cmd10 = ImpalDB.GetSqlStringCommand("select 1 from line_wise_sales where month_year=" + date3);
                        cmd10.CommandTimeout = ConnectionTimeOut.TimeOut;
                        string strdate8 = Convert.ToString(ImpalDB.ExecuteScalar(cmd10));
                        if (strdate8 == "0")
                        {

                            string ssql = "select  substring((select max(substring(month_year,3,4)+substring(month_year,1,2))from line_wise_sales where accounting_period_code =" + value1 + "),5,2)+ substring((select max(substring(month_year,3,4)+substring(month_year,1,2)) from line_wise_sales where accounting_period_code= " + value3 + "),1,4) ";
                            DbCommand cmd11 = ImpalDB.GetSqlStringCommand(ssql);
                            cmd11.CommandTimeout = ConnectionTimeOut.TimeOut;
                            date3 = Convert.ToString(ImpalDB.ExecuteScalar(cmd11));
                        }

                    }
                    else
                    { date3 = "0"; }

                    #region Report SelectionFormula

                    if (sessionbrchcode != "CRP")
                    {
                        if (date1 != "0" && date2 != "0" && date3 != "0")
                        {
                            if (ddlLineCode.SelectedIndex == 0 && ddlAreaManager.SelectedIndex == 0)
                            {
                                selectionformula = strsel1 + "'" + date1 + "' and " + strsel3 + "'" + sessionbrchcode + "'";
                            }
                            else if (ddlAreaManager.SelectedIndex == 0 && ddlLineCode.SelectedIndex != 0)
                            {
                                selectionformula = "(" + strsel1 + "'" + date1 + "' or " + strsel1 + "'" + date2 + "' or " + strsel1 + "'" + date3 + "') and " + strsel3 + "'" + sessionbrchcode + "'";
                            }
                            else if (ddlAreaManager.SelectedIndex != 0 && ddlLineCode.SelectedIndex != 0)
                            {
                                selectionformula = "(" + strsel1 + "'" + date1 + "' or " + strsel1 + "'" + date2 + "' or " + strsel1 + "'" + date3 + "') and " + strsel3 + "'" + sessionbrchcode + "' and " + strsel5 + "'" + ddlLineCode.SelectedValue + "'";
                            }
                            else if (ddlAreaManager.SelectedIndex != 0 && ddlLineCode.SelectedIndex == 0)
                            {
                                selectionformula = "(" + strsel1 + "'" + date1 + "' or " + strsel1 + "'" + date2 + "' or " + strsel1 + "'" + date3 + "') and " + strsel3 + "'" + sessionbrchcode + "' and " + strsel2 + "'" + ddlAreaManager.SelectedValue + "'";
                            }

                        }
                        else if (date1 != "0" && date2 != "0" && date3 == "0")
                        {
                            if (ddlLineCode.SelectedIndex == 0 && ddlAreaManager.SelectedIndex == 0)
                            {
                                selectionformula = strsel1 + "'" + date1 + "' and " + strsel3 + "'" + sessionbrchcode + "'";
                            }
                            else if (ddlAreaManager.SelectedIndex == 0 && ddlLineCode.SelectedIndex != 0)
                            {
                                selectionformula = "(" + strsel1 + "'" + date1 + "' or " + strsel1 + "'" + date2 + "' ) and " + strsel3 + "'" + sessionbrchcode + "'";
                            }
                            else if (ddlAreaManager.SelectedIndex != 0 && ddlLineCode.SelectedIndex != 0)
                            {
                                selectionformula = "(" + strsel1 + "'" + date1 + "' or " + strsel1 + "'" + date2 + "') and " + strsel3 + "'" + sessionbrchcode + "' and " + strsel5 + "'" + ddlLineCode.SelectedValue + "'";
                            }
                            else if (ddlAreaManager.SelectedIndex != 0 && ddlLineCode.SelectedIndex == 0)
                            {
                                selectionformula = "(" + strsel1 + "'" + date1 + "' or " + strsel1 + "'" + date2 + "') and " + strsel3 + "'" + sessionbrchcode + "' and " + strsel2 + "'" + ddlAreaManager.SelectedValue + "'";
                            }

                        }
                        else if (date1 != "0" && date2 == "0" && date3 == "0")
                        {
                            if (ddlLineCode.SelectedIndex == 0 && ddlAreaManager.SelectedIndex == 0)
                            {
                                selectionformula = strsel1 + "'" + date1 + "' and " + strsel3 + "'" + sessionbrchcode + "'";
                            }
                            else if (ddlAreaManager.SelectedIndex == 0 && ddlLineCode.SelectedIndex != 0)
                            {
                                selectionformula = strsel1 + "'" + date1 + "' and " + strsel3 + "'" + sessionbrchcode + "'";
                            }
                            else if (ddlAreaManager.SelectedIndex != 0 && ddlLineCode.SelectedIndex != 0)
                            {
                                selectionformula = strsel1 + "'" + date1 + "' and " + strsel3 + "'" + sessionbrchcode + "' and " + strsel5 + "'" + ddlLineCode.SelectedValue + "'";
                            }
                            else if (ddlAreaManager.SelectedIndex != 0 && ddlLineCode.SelectedIndex == 0)
                            {
                                selectionformula = strsel1 + "'" + date1 + "' and " + strsel3 + "'" + sessionbrchcode + "' and " + strsel2 + "'" + ddlAreaManager.SelectedValue + "'";
                            }

                        }

                    }
                    else
                    {
                        if (date1 != "0" && date2 != "0" && date3 != "0")
                        {
                            if (ddlLineCode.SelectedIndex == 0 && ddlAreaManager.SelectedIndex == 0)
                            {
                                selectionformula = strsel1 + "'" + date1 + "' ";
                            }
                            else if (ddlAreaManager.SelectedIndex == 0 && ddlLineCode.SelectedIndex != 0)
                            {
                                selectionformula = "(" + strsel1 + "'" + date1 + "' or " + strsel1 + "'" + date2 + "' or " + strsel1 + "'" + date3 + "') and " + strsel5 + "'" + ddlLineCode.SelectedValue + "'";
                            }
                            else if (ddlAreaManager.SelectedIndex != 0 && ddlLineCode.SelectedIndex != 0)
                            {
                                selectionformula = "(" + strsel1 + "'" + date1 + "' or " + strsel1 + "'" + date2 + "' or " + strsel1 + "'" + date3 + "') and " + strsel5 + "'" + ddlLineCode.SelectedValue + "' and " + strsel2 + "'" + ddlAreaManager.SelectedValue + "'";
                            }
                            else if (ddlAreaManager.SelectedIndex != 0 && ddlLineCode.SelectedIndex == 0)
                            {
                                selectionformula = "(" + strsel1 + "'" + date1 + "' or " + strsel1 + "'" + date2 + "' or " + strsel1 + "'" + date3 + "') and " + strsel2 + "'" + ddlAreaManager.SelectedValue + "'";
                            }

                        }
                        else if (date1 != "0" && date2 != "0" && date3 == "0")
                        {
                            if (ddlLineCode.SelectedIndex == 0 && ddlAreaManager.SelectedIndex == 0)
                            {
                                selectionformula = strsel1 + "'" + date1 + "' ";
                            }
                            else if (ddlAreaManager.SelectedIndex == 0 && ddlLineCode.SelectedIndex != 0)
                            {
                                selectionformula = "(" + strsel1 + "'" + date1 + "' or " + strsel1 + "'" + date2 + "' ) and " + strsel5 + "'" + ddlLineCode.SelectedValue + "'";
                            }
                            else if (ddlAreaManager.SelectedIndex != 0 && ddlLineCode.SelectedIndex != 0)
                            {
                                selectionformula = "(" + strsel1 + "'" + date1 + "' or " + strsel1 + "'" + date2 + "') and " + strsel5 + "'" + ddlLineCode.SelectedValue + "' and " + strsel2 + "'" + ddlAreaManager.SelectedValue + "'";
                            }
                            else if (ddlAreaManager.SelectedIndex != 0 && ddlLineCode.SelectedIndex == 0)
                            {
                                selectionformula = "(" + strsel1 + "'" + date1 + "' or " + strsel1 + "'" + date2 + "') and " + strsel2 + "'" + ddlAreaManager.SelectedValue + "'";
                            }

                        }
                        else if (date1 != "0" && date2 == "0" && date3 == "0")
                        {
                            if (ddlLineCode.SelectedIndex == 0 && ddlAreaManager.SelectedIndex == 0)
                            {
                                selectionformula = strsel1 + "'" + date1 + "'";
                            }
                            else if (ddlAreaManager.SelectedIndex == 0 && ddlLineCode.SelectedIndex != 0)
                            {
                                selectionformula = strsel1 + "'" + date1 + "' and " + strsel5 + "'" + ddlLineCode.SelectedValue + "'";
                            }
                            else if (ddlAreaManager.SelectedIndex != 0 && ddlLineCode.SelectedIndex != 0)
                            {
                                selectionformula = strsel1 + "'" + date1 + "' and " + strsel5 + "'" + ddlLineCode.SelectedValue + "' and " + strsel2 + "'" + ddlAreaManager.SelectedValue + "'";
                            }
                            else if (ddlAreaManager.SelectedIndex != 0 && ddlLineCode.SelectedIndex == 0)
                            {
                                selectionformula = strsel1 + "'" + date1 + "' and " + strsel2 + "'" + ddlAreaManager.SelectedValue + "'";
                            }

                        }
                    }
                    #endregion

                    crAnnualManager1.RecordSelectionFormula = selectionformula;
                    crAnnualManager1.ReportName = "impal-Reports-Areamanagerstock1";
                    crAnnualManager1.GenerateReport();
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