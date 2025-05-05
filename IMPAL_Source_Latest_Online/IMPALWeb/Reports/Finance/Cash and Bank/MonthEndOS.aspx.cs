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


namespace IMPALWeb.Reports.Finance.Cash_and_Bank
{
    public partial class MonthEndOS : System.Web.UI.Page
    {
        private string strBranchCode = default(string);

        #region Page Init
        /// <summary>
        /// Page Init event 
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
                if (!IsPostBack)
                {
                    if (crMonthEnd != null)
                    {
                        crMonthEnd.Dispose();
                        crMonthEnd = null;
                    }

                    fnGetBranches();

                    if (Session["BranchCode"] != null)
                        strBranchCode = Session["BranchCode"].ToString();

                    if (strBranchCode != "CRP")
                    {
                        ddlBranchCode.SelectedValue = strBranchCode;
                        ddlBranchCode.Enabled = false;
                    }
                    else
                    {
                        ddlBranchCode.Enabled = true;
                        ddlBranchCode.SelectedValue = strBranchCode;
                    }

                    fnPopulateReportType();
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
            if (crMonthEnd != null)
            {
                crMonthEnd.Dispose();
                crMonthEnd = null;
            }
        }
        protected void crMonthEnd_Unload(object sender, EventArgs e)
        {
            if (crMonthEnd != null)
            {
                crMonthEnd.Dispose();
                crMonthEnd = null;
            }
        }

        #region Get BranchCode
        /// <summary>
        /// Get Branches from DB
        /// </summary>
        public void fnGetBranches()
        {
             Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
             //Log.WriteLog(source, "fnGetBranches", "Entering fnGetBranches");
            try
            {
            IMPALLibrary.Branches branch = new IMPALLibrary.Branches();

            ddlBranchCode.DataSource = branch.GetCorpBranch();
            ddlBranchCode.DataTextField = "BranchName";
            ddlBranchCode.DataValueField = "BranchCode";
            ddlBranchCode.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Populate Report Type
        /// <summary>
        /// To Populate Report Type
        /// </summary>
        protected void fnPopulateReportType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "fnPopulateReportType", "Entering fnPopulateReportType");
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                List<DropDownListValue> oList = new List<DropDownListValue>();
                oList = oCommon.GetDropDownListValues("ReportType-Std");
                ddlReportType.DataSource = oList;
                ddlReportType.DataValueField = "DisplayValue";
                ddlReportType.DataTextField = "DisplayText";
                ddlReportType.DataBind();
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
                string strBranchCodeField = default(string);
                string strSelectionFormula = default(string);
                string strReportName = default(string);
                string strFromDate = default(string);
                string strToDate = default(string);
                int intProcStatus = default(int);

                strFromDate = txtFromDate.Text;
                strToDate = txtFromDate.Text;
                strBranchCodeField = "{cust_aging.branch_code}";

                if (strBranchCode != "CRP")
                    strSelectionFormula = strBranchCodeField + " ='" + strBranchCode + "'";
                else
                    strSelectionFormula = "";

                if (ddlReportType.SelectedValue == "Report")
                {
                    strReportName = "MonthEndOS";
                }
                else
                {
                    strReportName = "MonthEndOSSummary";
                }

                crMonthEnd.ReportName = strReportName;
                crMonthEnd.RecordSelectionFormula = strSelectionFormula;
                crMonthEnd.GenerateReport();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }
        #endregion 

        #region Report Button click
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
