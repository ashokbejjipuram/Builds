#region Namespace Declaration
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.EnterpriseLibrary.Data;
using IMPALLibrary;
using System.Data.Common;
using System.Data;
using IMPALLibrary.Masters.Sales;
#endregion

namespace IMPALWeb.Reports.Sales.Sales_Comparison
{
    public partial class LineWise : System.Web.UI.Page
    {
        string sessionvalue = string.Empty;
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
                if (!IsPostBack)
                {
                    if (crlinewise != null)
                    {
                        crlinewise.Dispose();
                        crlinewise = null;
                    }

                    fnPopulateAccountingPeriod();
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
            if (crlinewise != null)
            {
                crlinewise.Dispose();
                crlinewise = null;
            }
        }
        protected void crlinewise_Unload(object sender, EventArgs e)
        {
            if (crlinewise != null)
            {
                crlinewise.Dispose();
                crlinewise = null;
            }
        }

        #region Populate Accounting Period Dropdown
        private void fnPopulateAccountingPeriod()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                AccountingPeriods accperiod = new AccountingPeriods();
                ddlAccountingPeriod.DataSource = accperiod.GetAccountingPeriod(20, null, sessionvalue);
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
                    string sel1 = "{V_SalesReports_comp.month_year}=";
                    string sel2 = "{V_SalesReports_comp.accounting_period_code}=";
                    string sel3 = "{V_SalesReports_comp.branch_code}=";
                    string selectionformula = string.Empty;

                    if (Session["BranchCode"] != null)
                        sessionvalue = (string)Session["BranchCode"];

                    Database ImpalDB = DataAccess.GetDatabase();
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_addbranch_ranking_cumm");
                    ImpalDB.AddInParameter(cmd, "@branch_code", DbType.String, sessionvalue);
                    ImpalDB.AddInParameter(cmd, "@accounting_period_code", DbType.Int32, ddlAccountingPeriod.SelectedValue);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);

                    if (sessionvalue != "CRP")
                    {
                        selectionformula = sel2 + ddlAccountingPeriod.SelectedValue + " and " + sel3 + "'" + sessionvalue + "'";
                    }
                    else
                    {
                        selectionformula = sel2 + ddlAccountingPeriod.SelectedValue;
                    }
                    crlinewise.RecordSelectionFormula = selectionformula;
                    crlinewise.ReportName = "impal-reports-linestockcomp";
                    crlinewise.GenerateReport();
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
