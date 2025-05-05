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

namespace IMPALWeb.Reports.Finance.Accounts_Payable
{
    public partial class PurchaseBreak : System.Web.UI.Page
    {
        private string strBranchCode = default(string);
        #region Page Init
        /// <summary>
        /// Page init event 
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
        /// Page Load event 
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
                    strBranchCode = Session["BranchCode"].ToString();

                if (!IsPostBack)
                {
                    if (crPurchaseBreak != null)
                    {
                        crPurchaseBreak.Dispose();
                        crPurchaseBreak = null;
                    }

                    fnGetMonthYear();
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
            if (crPurchaseBreak != null)
            {
                crPurchaseBreak.Dispose();
                crPurchaseBreak = null;
            }
        }
        protected void crPurchaseBreak_Unload(object sender, EventArgs e)
        {
            if (crPurchaseBreak != null)
            {
                crPurchaseBreak.Dispose();
                crPurchaseBreak = null;
            }
        }

        #region Get MonthYear
        /// <summary>
        /// Gets MonthYear 
        /// </summary>
        public void fnGetMonthYear()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "fnGetMonthYear", "Entering fnGetMonthYear");
            try
            {
                IMPALLibrary.Masters.Finance finance = new IMPALLibrary.Masters.Finance();
                ddlMonthYear.DataSource = finance.GetMonthYear("V_PURCHASEBREAK", null);
                ddlMonthYear.DataTextField = "MonthYear";
                ddlMonthYear.DataValueField = "MonthYear";
                ddlMonthYear.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Generate Selection Formula
        /// <summary>
        /// Method to generate selection formula 
        /// </summary>
        public void GenerateSelectionFormula()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "GenerateSelectionFormula()", "Entering GenerateSelectionFormula()");
            try
            {
                #region Declaration
                string strMonthYear = default(string);
                string strSelectionFormula = default(string);
                string strDateField = default(string);
                string strAccountCodeField = default(string);
                string strReportName = default(string);
                strDateField = "mid({v_purchasebreak.invoice_date},4,7)";
                strAccountCodeField = "{V_purchasebreak.Branch_Code}";
                strReportName = "PurchaseBreak";
                #endregion

                Microsoft.Practices.EnterpriseLibrary.Data.Database ImpalDB = DataAccess.GetDatabase();
                #region Selection Formula Formation
                strMonthYear = ddlMonthYear.SelectedValue;

                if (strBranchCode == "CRP")
                    strSelectionFormula = strDateField + "=" + "'" + strMonthYear + "'";
                else
                    strSelectionFormula = strDateField + "=" + "'" + strMonthYear + "' and " + strAccountCodeField + "=" + " '" + strBranchCode + "'";
                #endregion

                crPurchaseBreak.ReportName = strReportName;
                crPurchaseBreak.RecordSelectionFormula = strSelectionFormula;
                crPurchaseBreak.GenerateReport();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }
        #endregion 

        #region Button Click
        /// <summary>
        /// Report Button click
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
                    GenerateSelectionFormula();
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
