#region Namespace Declaration
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using IMPALLibrary;
using IMPALLibrary.Masters.Sales;
#endregion

namespace IMPALWeb.Reports.Sales.Sales_Comparison
{
    public partial class ZoneWise : System.Web.UI.Page
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

        #region Page load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!IsPostBack)
                {
                    if (crzonewise != null)
                    {
                        crzonewise.Dispose();
                        crzonewise = null;
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
            if (crzonewise != null)
            {
                crzonewise.Dispose();
                crzonewise = null;
            }
        }
        protected void crzonewise_Unload(object sender, EventArgs e)
        {
            if (crzonewise != null)
            {
                crzonewise.Dispose();
                crzonewise = null;
            }
        }

        #region Populate Accounting Period Dropdown
        private void fnPopulateAccountingPeriod()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                AccountingPeriods accperiod = new AccountingPeriods();
                ddlAccountingperiod.DataSource = accperiod.GetAccountingPeriod(1, "WithLine", Session["BranchCode"].ToString());
                ddlAccountingperiod.DataTextField = "Desc";
                ddlAccountingperiod.DataValueField = "AccPeriodCode";
                ddlAccountingperiod.DataBind();
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
                    string sel1 = "{branch_ranking2.accounting_period_code}=";

                    Database ImpalDB = DataAccess.GetDatabase();
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_addbranch_ranking2");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, sessionvalue);
                    ImpalDB.AddInParameter(cmd, "@accounting_period_code", DbType.Int32, ddlAccountingperiod.SelectedValue);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);
                    if (ddlAccountingperiod.SelectedIndex != 0)
                    {
                        selectionformula = sel1 + ddlAccountingperiod.SelectedValue;
                    }
                    crzonewise.ReportName = "zonewise_salescomp1_new";
                    crzonewise.RecordSelectionFormula = selectionformula;
                    crzonewise.GenerateReport();
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
