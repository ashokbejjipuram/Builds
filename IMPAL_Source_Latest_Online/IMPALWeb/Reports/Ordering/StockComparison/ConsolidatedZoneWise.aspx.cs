#region Namespace Declaration

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using IMPALLibrary.Masters.Sales;
#endregion

namespace IMPALWeb.Reports.Ordering.StockComparison
{
    public partial class ConsolidatedZoneWise : System.Web.UI.Page
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

        #region Page load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (Session["BranchCode"] != null)
                    sessionbrchcode = (string)Session["BranchCode"];

                if (!IsPostBack)
                {
                    if (crConsolidated != null)
                    {
                        crConsolidated.Dispose();
                        crConsolidated = null;
                    }

                    fnPopulateAccountingPeriod();
                    fnPopulateZone();
                    ddlBranch.Enabled = false;
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
            if (crConsolidated != null)
            {
                crConsolidated.Dispose();
                crConsolidated = null;
            }
        }
        protected void crConsolidated_Unload(object sender, EventArgs e)
        {
            if (crConsolidated != null)
            {
                crConsolidated.Dispose();
                crConsolidated = null;
            }
        }

        #region Accounting Period Dropdown Populate Method
        private void fnPopulateAccountingPeriod()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                AccountingPeriods Acc = new AccountingPeriods();
                ddlAccountingPeriod.DataSource = Acc.GetAccountingPeriod(20, null, sessionbrchcode);
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

        #region Zone Dropdown Populate Method
        private void fnPopulateZone()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                IMPALLibrary.Zones zn = new IMPALLibrary.Zones();
                ddlZone.DataSource = zn.GetAllZones();
                ddlZone.DataTextField = "ZoneName";
                ddlZone.DataValueField = "ZoneCode";
                ddlZone.DataBind();
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
            string selectionformula = string.Empty;
            try
            {
                Main mainmaster = (Main)Page.Master;
                mainmaster.ShowHideFilters(btnReport, reportFiltersTable, reportViewerHolder);
                if (btnReport.Text == "Back")
                {
                    string strsel1 = "{line_wise_sales.month_year}=";
                    string strsel2 = "{line_wise_sales.branch_code}=";
                    string strsel3 = "{line_wise_sales.Accounting_Period_Code}=";
                    string strsel4 = "{zone_master.zone_code}=";

                    string strcurrentdate = DateTime.Today.ToString("MMyyyy");
                    crConsolidated.ReportName = "Impal-Reports-Annualstock";

                    selectionformula = strsel3 + ddlAccountingPeriod.SelectedValue + " and " + strsel1 + "'" + strcurrentdate + "'";

                    if (ddlZone.SelectedValue != "0" && ddlBranch.SelectedIndex == 0)
                    {
                        selectionformula = strsel3 + ddlAccountingPeriod.SelectedValue + " and " + strsel4 + ddlZone.SelectedValue + " and " + strsel1 + "'" + strcurrentdate + "'";
                    }
                    else if (ddlZone.SelectedValue != "0" && ddlBranch.SelectedValue != "")
                    {
                        selectionformula = strsel3 + ddlAccountingPeriod.SelectedValue + " and " + strsel4 + ddlZone.SelectedValue + " and " + strsel1 + "'" + strcurrentdate + "' and " + strsel2 + "'" + ddlBranch.SelectedValue + "'";
                    }
                    crConsolidated.RecordSelectionFormula = selectionformula;
                    crConsolidated.GenerateReport();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }

        }
        #endregion

        #region Zone Dropdown Selection Changed
        protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddlBranch.Enabled = true;
                Branches br = new Branches();
                ddlBranch.DataSource = br.GetBranchBasedonZoneState(ddlZone.SelectedValue);
                ddlBranch.DataTextField = "BranchName";
                ddlBranch.DataValueField = "BranchCode";
                ddlBranch.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion
    }
}