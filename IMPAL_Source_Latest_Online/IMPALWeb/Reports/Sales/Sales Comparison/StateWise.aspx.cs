#region Namespace Declaration
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using IMPALLibrary.Masters.Sales;
using IMPALLibrary;
#endregion

namespace IMPALWeb.Reports.Sales.Sales_Comparison
{
    public partial class StateWise : System.Web.UI.Page
    {
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
                    if (crstatewise != null)
                    {
                        crstatewise.Dispose();
                        crstatewise = null;
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
            if (crstatewise != null)
            {
                crstatewise.Dispose();
                crstatewise = null;
            }
        }
        protected void crstatewise_Unload(object sender, EventArgs e)
        {
            if (crstatewise != null)
            {
                crstatewise.Dispose();
                crstatewise = null;
            }
        }

        #region Populate Accounting Period Dropdown
        private void fnPopulateAccountingPeriod()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                AccountingPeriods accperiod = new AccountingPeriods();
                ddlAccountingPeriod.DataSource = accperiod.GetAccountingPeriod(1, null, Session["BranchCode"].ToString());
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
                    string sel1 = "{branch_ranking.accounting_period_Code}=";
                    string sel2 = "{Branch_Ranking.branch_code}=";

                    Database ImpalDB = DataAccess.GetDatabase();
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_addbranch_ranking2");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, sessionvalue);
                    ImpalDB.AddInParameter(cmd, "@accounting_period_code", DbType.Int32, ddlAccountingPeriod.SelectedValue);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);

                    if (sessionvalue == "CRP")
                    {
                        selectionformula = sel1 + ddlAccountingPeriod.SelectedValue;
                    }
                    else
                    {
                        selectionformula = sel1 + ddlAccountingPeriod.SelectedValue + " AND " + sel2 + "'" + sessionvalue + "'";
                    }
                    crstatewise.ReportName = "impal_statewisesalescomparision";
                    crstatewise.RecordSelectionFormula = selectionformula;
                    crstatewise.GenerateReport();
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
