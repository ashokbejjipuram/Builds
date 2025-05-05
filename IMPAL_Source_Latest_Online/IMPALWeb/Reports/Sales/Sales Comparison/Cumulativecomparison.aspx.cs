#region NameSpace Declaration
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Common;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Data;
using IMPALLibrary;
using System.Data.SqlClient;
using IMPALLibrary.Masters.Sales;
#endregion

namespace IMPALWeb.Reports.Sales.Sales_Comparison
{
    public partial class Cumulativecomparison : System.Web.UI.Page
    {
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
                    if (crcumulative != null)
                    {
                        crcumulative.Dispose();
                        crcumulative = null;
                    }

                    fnpopulateAccountingperiod();
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
            if (crcumulative != null)
            {
                crcumulative.Dispose();
                crcumulative = null;
            }
        }
        protected void crcumulative_Unload(object sender, EventArgs e)
        {
            if (crcumulative != null)
            {
                crcumulative.Dispose();
                crcumulative = null;
            }
        }

        #region Populate Accounting Period Dropdown
        public void fnpopulateAccountingperiod()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                AccountingPeriods accperiod = new AccountingPeriods();
                ddlAccountingPeriod.DataSource = accperiod.GetAccountingPeriod(20, null, Session["BranchCode"].ToString());
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
                    string sessionvalue = (string)Session["BranchCode"];
                    string selectionformula = string.Empty;
                    string sel1 = "{Branch_ranking.accounting_period_code}=";
                    string sel3 = "{Branch_ranking.branch_code}=";

                    Database ImpalDB = DataAccess.GetDatabase();
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_addbranch_ranking_cumm");
                    ImpalDB.AddInParameter(cmd, "@accounting_period_code", DbType.Int32, ddlAccountingPeriod.SelectedValue);
                    ImpalDB.AddInParameter(cmd, "@branch_code", DbType.String, sessionvalue);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);
                    if (sessionvalue != "CRP")
                    {
                        selectionformula = sel1 + ddlAccountingPeriod.SelectedValue + " and " + sel3 + "'" + sessionvalue + "'";
                    }
                    else
                    {
                        selectionformula = sel1 + ddlAccountingPeriod.SelectedValue;
                    }
                    crcumulative.ReportName = "cumul_salescomp_linewise";
                    crcumulative.RecordSelectionFormula = selectionformula;
                    crcumulative.GenerateReport();
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
