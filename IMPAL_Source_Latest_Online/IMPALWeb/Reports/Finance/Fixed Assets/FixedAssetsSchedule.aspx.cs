#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using IMPALLibrary.Common;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.ServiceLocation;
#endregion
namespace IMPALWeb.Reports.Finance.Fixed_Assets
{
    public partial class FixedAssetsSchedule : System.Web.UI.Page
    {
        private string strBranchCode = default(string);

        #region Page Init
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

        #region Page load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Load", "Entering Page_Load");
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = Session["BranchCode"].ToString();

                if (!IsPostBack)
                {
                    if (crFixedAssetsSchedule != null)
                    {
                        crFixedAssetsSchedule.Dispose();
                        crFixedAssetsSchedule = null;
                    }

                    fnGetAccountingPeriod();                    
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
            if (crFixedAssetsSchedule != null)
            {
                crFixedAssetsSchedule.Dispose();
                crFixedAssetsSchedule = null;
            }
        }
        protected void crFixedAssetsSchedule_Unload(object sender, EventArgs e)
        {
            if (crFixedAssetsSchedule != null)
            {
                crFixedAssetsSchedule.Dispose();
                crFixedAssetsSchedule = null;
            }
        }

        #region Gets Accounting Period
        /// <summary>
        /// Get values for Accounting Period
        /// </summary>
        public void fnGetAccountingPeriod()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "fnGetAccountingPeriod", "Entering fnGetAccountingPeriod");
            try
            {
                IMPALLibrary.Masters.Sales.AccountingPeriods accperiod = new IMPALLibrary.Masters.Sales.AccountingPeriods();

                ddlAccPeriod.DataSource = accperiod.GetAccountingPeriod(1, null, strBranchCode);
                ddlAccPeriod.DataTextField = "Desc";
                ddlAccPeriod.DataValueField = "AccPeriodCode";
                ddlAccPeriod.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Button Click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Main mainmaster = (Main)Page.Master;
                mainmaster.ShowHideFilters(btnReport, reportFiltersTable, reportViewerHolder);
                if (btnReport.Text == "Back")
                {
                    #region Declaration
                    string strAccPeriod = default(string);
                    string strReportName = default(string);
                    int intProcStatus = default(int);
                    strReportName = "FixedAssetsSchedule";
                    #endregion

                    strAccPeriod = ddlAccPeriod.SelectedValue;

                    Microsoft.Practices.EnterpriseLibrary.Data.Database ImpalDB = DataAccess.GetDatabase();
                    DbCommand dbcmd = ImpalDB.GetStoredProcCommand("usp_addfixed_assets_schedule");
                    ImpalDB.AddInParameter(dbcmd, "@accounting_period_code", DbType.String, strAccPeriod.Trim());
                    dbcmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    intProcStatus = ImpalDB.ExecuteNonQuery(dbcmd);

                    crFixedAssetsSchedule.ReportName = strReportName;
                    crFixedAssetsSchedule.RecordSelectionFormula = "";
                    crFixedAssetsSchedule.GenerateReport();
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
