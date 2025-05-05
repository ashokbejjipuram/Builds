#region Namespace Declaration
using System;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using IMPALLibrary;
using System.Data;
using System.Data.Common;
#endregion

namespace IMPALWeb.Reports.Finance.Account_Receivable
{
    public partial class SalesBreakup : System.Web.UI.Page
    {
        string sessionvalue = string.Empty;

        #region Page Init
        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "Page_Init", "Sales Breakup Page Init Method"); 
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
            //Log.WriteLog(Source, "Page_Load", "Sales Breakup Page Load Method"); 
            try
            {
                sessionvalue = (string)Session["BranchCode"];
                if (!IsPostBack)
                {
                    if (crSalesBreakup != null)
                    {
                        crSalesBreakup.Dispose();
                        crSalesBreakup = null;
                    }

                    fnPopulateMonthYear();
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
            if (crSalesBreakup != null)
            {
                crSalesBreakup.Dispose();
                crSalesBreakup = null;
            }
        }
        protected void crSalesBreakup_Unload(object sender, EventArgs e)
        {
            if (crSalesBreakup != null)
            {
                crSalesBreakup.Dispose();
                crSalesBreakup = null;
            }
        }

        #region Populate MonthYear Method
        protected void fnPopulateMonthYear()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "fnPopulateMonthYear()", "Sales Breakup MonthYear Populate Method");
            try
            {
                IMPALLibrary.Masters.Finance objMonthYear = new IMPALLibrary.Masters.Finance();
                ddlMonthYear.DataSource = objMonthYear.GetMonthYear("sales_order_header", sessionvalue);
                ddlMonthYear.DataValueField = "MonthYear";
                ddlMonthYear.DataTextField = "MonthYear";
                ddlMonthYear.DataBind();
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
                    string strselMonthYear = "{gpprofitstatement.month_year}=";
                    string strMonthYear = ddlMonthYear.SelectedValue;
                    Database ImpalDB = DataAccess.GetDatabase();
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_salesbreakup");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, sessionvalue);
                    ImpalDB.AddInParameter(cmd, "@month_year", DbType.String, strMonthYear);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);

                    crSalesBreakup.RecordSelectionFormula = string.Empty;
                    crSalesBreakup.ReportName = "sales_breakup";
                    crSalesBreakup.GenerateReport();
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
