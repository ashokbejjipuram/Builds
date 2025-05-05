#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using IMPALLibrary;
using System.Web.UI.WebControls;
#endregion
namespace IMPALWeb.Reports.Finance.General_Ledger
{
    public partial class OpeningBalance : System.Web.UI.Page
    {
        #region Declaration
        string sessionvalue = string.Empty;
        #endregion

        #region Page Initialization
        /// <summary>
        /// To initialize page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Init", "Entering Page_Init");
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }
        #endregion

        #region Page Load
        /// <summary>
        /// To load page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Load", "Entering Page_Load");
            try
            {
                if (Session["BranchCode"] != null)
                    sessionvalue = (string)Session["BranchCode"];

                if (!IsPostBack)
                {
                    if (cropeningbalance != null)
                    {
                        cropeningbalance.Dispose();
                        cropeningbalance = null;
                    }

                    loadAccountingYear();
                    loadBranch();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }
        #endregion
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (cropeningbalance != null)
            {
                cropeningbalance.Dispose();
                cropeningbalance = null;
            }
        }
        protected void cropeningbalance_Unload(object sender, EventArgs e)
        {
            if (cropeningbalance != null)
            {
                cropeningbalance.Dispose();
                cropeningbalance = null;
            }
        }

        #region loadBranch
        /// <summary>
        /// To load Branch Code in dropdown ddlBranch
        /// </summary>
        protected void loadBranch()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "loadBranch", "Entering loadBranch");
            try
            {
            IMPALLibrary.Branches Branches = new IMPALLibrary.Branches();
            List<IMPALLibrary.Branch> lstBranch = new List<IMPALLibrary.Branch>();
            lstBranch = Branches.GetBranchFromChartofAccount();
            ddlBranch.DataSource = lstBranch;
            ddlBranch.DataValueField = "BranchCode";
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataBind();
            if (sessionvalue != "CRP")
                ddlBranch.SelectedValue = sessionvalue;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }
        #endregion

        #region loadAccountingYear
        /// <summary>
        /// To load Accounting Year in dropdown ddlAccountingPeriod
        /// </summary>
        protected void loadAccountingYear()
        {
              Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
              //Log.WriteLog(source, "loadAccountingYear", "Entering loadAccountingYear");
            try
            {
            IMPALLibrary.Masters.Sales.AccountingPeriods AcctPeriods = new IMPALLibrary.Masters.Sales.AccountingPeriods();
            List<IMPALLibrary.Masters.Sales.AccountingPeriod> lstAcctPeriod = new List<IMPALLibrary.Masters.Sales.AccountingPeriod>();
            lstAcctPeriod = AcctPeriods.GetAccountingPeriod(0, null, sessionvalue);
            ddlAccountingPeriod.DataSource = lstAcctPeriod;
            ddlAccountingPeriod.DataValueField = "AccPeriodCode";
            ddlAccountingPeriod.DataTextField = "Desc";
            ddlAccountingPeriod.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region btnReport_Click
        /// <summary>
        /// To Generate Report
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Main mainmaster = (Main)Page.Master;
                mainmaster.ShowHideFilters(btnReport, reportFiltersTable, reportViewerHolder);
                if (btnReport.Text == "Back")
                {
                    string strSelectionString = string.Empty;
                    string strPeriodCode = "{General_Ledger_Summary.accounting_period_Code}";
                    string strOpeningBalanceIndicator = "{General_Ledger_Summary.Opening_Balance_Indicator}";
                    string strChartofAccount = "mid({General_Ledger_Summary.Chart_Of_Account_Code},18,3)";
                    string strAcountPeriod = ddlAccountingPeriod.SelectedValue;
                    string strBranch = ddlBranch.SelectedValue;

                    strSelectionString = strChartofAccount + " ='" + strBranch + "' and " + strPeriodCode + "=" + " " + strAcountPeriod + "" + " and " + strOpeningBalanceIndicator + "=" + "'0'";
                    cropeningbalance.RecordSelectionFormula = strSelectionString;
                    cropeningbalance.ReportName = "Opening_Balance";
                    cropeningbalance.GenerateReport();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion
    }
}
