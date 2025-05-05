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
namespace IMPALWeb.Reports.Ordering.Listing
{
    public partial class PurchaseFigComparision : System.Web.UI.Page
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

        #region Page Load
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
                    fnGetMonthYear();

                    if (crPurchaseFigComp != null)
                    {
                        crPurchaseFigComp.Dispose();
                        crPurchaseFigComp = null;
                    }
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
            if (crPurchaseFigComp != null)
            {
                crPurchaseFigComp.Dispose();
                crPurchaseFigComp = null;
            }
        }
        protected void crPurchaseFigComp_Unload(object sender, EventArgs e)
        {
            if (crPurchaseFigComp != null)
            {
                crPurchaseFigComp.Dispose();
                crPurchaseFigComp = null;
            }
        }

        #region Populate MonthYear
        /// <summary>
        /// Gets Month_Year 
        /// </summary>
        public void fnGetMonthYear()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "fnGetMonthYear", "Entering fnGetMonthYear");
            try
            {
            IMPALLibrary.Masters.Sales.LineWiseSales sales = new IMPALLibrary.Masters.Sales.LineWiseSales();

            ddlMonthYear.DataSource = sales.GetMonthYear(strBranchCode);
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
        public void GenerateSelectionFormula()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "GenerateSelectionFormula()", "Entering GenerateSelectionFormula()");
            try
            {
                #region Declaration
                string strMonthYear = default(string);
                string strSelectionFormula = default(string);
                string strMonthYearField = default(string);
                string strPurBranchCodeField = default(string);
                string strReportName = default(string);
                int intProcStatus = default(int);
                #endregion

                #region Selection Formula formation
                strMonthYear = ddlMonthYear.Text;
                Microsoft.Practices.EnterpriseLibrary.Data.Database ImpalDB = DataAccess.GetDatabase();
                strMonthYearField = "{line_wise_sales.Monthyear}";
                strPurBranchCodeField = "{rep_cmpr_purchasefig.Branchcode}";
                strReportName = "PurchaseFigComparision";

                DbCommand dbcmd = ImpalDB.GetStoredProcCommand("usp_cmpr_purchasefig");
                ImpalDB.AddInParameter(dbcmd, "@Month_year", DbType.String, strMonthYear.Trim());
                ImpalDB.AddInParameter(dbcmd, "@Branch_Code", DbType.String, strBranchCode);
                dbcmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                intProcStatus = ImpalDB.ExecuteNonQuery(dbcmd);
                if (strReportName == "PurchaseFigComparision")
                {
                    if (strBranchCode != "CRP")
                        strSelectionFormula = strPurBranchCodeField + "='" + strBranchCode + "'";
                }
                else if (strReportName != "PurchaseFigComparision")
                {
                    if (strBranchCode == "CRP")
                        strSelectionFormula = strMonthYearField + "=" + " " + "'" + strMonthYear + "'";
                    else if (strBranchCode != "CRP")
                        strSelectionFormula = strMonthYearField + "=" + " " + "'" + strMonthYear + "'" + " and " +
                                              strPurBranchCodeField + "='" + strBranchCode + "'";
                }

                crPurchaseFigComp.ReportName = strReportName;
                crPurchaseFigComp.RecordSelectionFormula = strSelectionFormula;
                crPurchaseFigComp.GenerateReport();
                #endregion
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
