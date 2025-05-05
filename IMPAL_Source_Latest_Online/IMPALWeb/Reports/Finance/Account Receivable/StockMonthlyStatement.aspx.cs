#region Namespace Declaration
using System;
using System.Collections.Generic;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using IMPALLibrary;
using System.Data;
#endregion

namespace IMPALWeb.Reports.Finance.Account_Receivable
{
    public partial class StockMonthlyStatement : System.Web.UI.Page
    {
        string strBranchCode = string.Empty;

        #region Page Init
        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "Page_Init", "Stock Monthly Statement Page Init Method"); 
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
            //Log.WriteLog(Source, "Page_Load", "Stock Monthly Statement Page Load Method"); 
            try
            {
               

                if (!IsPostBack)
                {
                    strBranchCode = (string)Session["BranchCode"];

                    if (crMonthlyStatement != null)
                    {
                        crMonthlyStatement.Dispose();
                        crMonthlyStatement = null;
                    }

                    fnPopulateMonthYear();
                    fnPopulateBranch();
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
            if (crMonthlyStatement != null)
            {
                crMonthlyStatement.Dispose();
                crMonthlyStatement = null;
            }
        }
        protected void crMonthlyStatement_Unload(object sender, EventArgs e)
        {
            if (crMonthlyStatement != null)
            {
                crMonthlyStatement.Dispose();
                crMonthlyStatement = null;
            }
        }

        #region Populate MonthYear Dropdown
        protected void fnPopulateMonthYear()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "fnPopulateMonthYear()", "Stock Monthly Statement MonthYear Dropdown Populate Method"); 
            try
            {
                IMPALLibrary.Masters.Finance objFinance = new IMPALLibrary.Masters.Finance();
                ddlMonthYear.DataSource = objFinance.GetMonthYear("month_year", null);
                ddlMonthYear.DataTextField = "MonthYear";
                ddlMonthYear.DataValueField = "MonthYear";
                ddlMonthYear.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Populate Branch Method
        protected void fnPopulateBranch()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "fnPopulateBranch()", "Stock Monthly Statement Branch Dropdown Populate Method");
            try
            {
                IMPALLibrary.Branches objBranch = new IMPALLibrary.Branches();
                ddlBranch.DataSource = objBranch.GetAllLinewiseBranches(null, null, strBranchCode);
                ddlBranch.DataTextField = "BranchName";
                ddlBranch.DataValueField = "BranchCode";
                ddlBranch.DataBind();
                ddlBranch.SelectedValue = strBranchCode;
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

                    string strselMonYr = "{cust_stockdebtor.monthyear}=";
                    string strselBranch = "{cust_stockdebtor.branch_code}=";
                    string strBranch = ddlBranch.SelectedValue;
                    string strMonthyear = ddlMonthYear.SelectedValue;

                    Database ImpalDB = DataAccess.GetDatabase();
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_ospercentmonth_new");
                    ImpalDB.AddInParameter(cmd, "@Month_Year", DbType.String, strMonthyear);
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranch);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);
                    crMonthlyStatement.RecordSelectionFormula = strselMonYr + "'" + strMonthyear + "' and " + strselBranch + "'" + strBranch + "'";
                    crMonthlyStatement.ReportName = "impal_stockDeptorsmonthly";
                    crMonthlyStatement.GenerateReport();
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
