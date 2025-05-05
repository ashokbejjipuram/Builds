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
    public partial class PettyCashRegister : System.Web.UI.Page
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
                    if (crPettyCash != null)
                    {
                        crPettyCash.Dispose();
                        crPettyCash = null;
                    }

                    fnPopulateReportType();
                    txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
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
            if (crPettyCash != null)
            {
                crPettyCash.Dispose();
                crPettyCash = null;
            }
        }
        protected void crPettyCash_Unload(object sender, EventArgs e)
        {
            if (crPettyCash != null)
            {
                crPettyCash.Dispose();
                crPettyCash = null;
            }
        }

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
                string strFromDate = default(string);
                string strToDate = default(string);
                string strCryFromDate = default(string);
                string strCryToDate = default(string);
                string strReferenceNumField = default(string);
                string strReferenceDateField = default(string);
                string strReportName = default(string);
                string strSelectionFormula = default(string);

                strFromDate = txtFromDate.Text;
                strToDate = txtToDate.Text;
                strCryFromDate = "Date (" + DateTime.ParseExact(strFromDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
                strCryToDate = "Date (" + DateTime.ParseExact(strToDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
                strReferenceNumField = "RIGHT({General_Ledger_Detail.Document_Reference_Number},3)";
                strReferenceDateField = "{General_Ledger_Detail.Document_Reference_Date}";

                if (strBranchCode == "CRP")
                    strSelectionFormula = strReferenceDateField + ">=" + strCryFromDate + " and " + strReferenceDateField + "<=" + strCryToDate;
                else if (strBranchCode != "CRP" && !string.IsNullOrEmpty(strBranchCode))
                    strSelectionFormula = strReferenceDateField + ">=" + strCryFromDate + " and " + strReferenceDateField + "<=" + strCryToDate + " and " + strReferenceNumField + " ='" + strBranchCode + "'";

                if (ddlReportType.SelectedValue == "Report")
                {
                    strReportName = "PettyCashReport";
                }
                else
                {
                    strReportName = "PettyCashSummary";
                }

                crPettyCash.ReportName = strReportName;
                crPettyCash.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
                crPettyCash.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
                crPettyCash.RecordSelectionFormula = strSelectionFormula;
                crPettyCash.GenerateReportHO();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }
        #endregion 

        #region Populate Report Type
        /// <summary>
        /// Populate report Type
        /// </summary>
        protected void fnPopulateReportType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "fnPopulateReportType", "Entered Reports Type Populate method");
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

        #region Button Click
        /// <summary>
        /// Report Button Click
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
