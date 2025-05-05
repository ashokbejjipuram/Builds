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
namespace IMPALWeb.Reports.Sales.SalesListing
{
    public partial class TargetSalesReport : System.Web.UI.Page
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
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = Session["BranchCode"].ToString();
                if (!IsPostBack)
                {
                    if (crTargetSales != null)
                    {
                        crTargetSales.Dispose();
                        crTargetSales = null;
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
            if (crTargetSales != null)
            {
                crTargetSales.Dispose();
                crTargetSales = null;
            }
        }
        protected void crTargetSales_Unload(object sender, EventArgs e)
        {
            if (crTargetSales != null)
            {
                crTargetSales.Dispose();
                crTargetSales = null;
            }
        }

        #region Populate MonthYear
        /// <summary>
        /// To Populate Month_Year DDL
        /// </summary>
        public void fnGetMonthYear()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                IMPALLibrary.Masters.Sales.LineWiseSales sales = new IMPALLibrary.Masters.Sales.LineWiseSales();
                ddlMonthYear.DataSource = sales.GetMonthYear(null);
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
        /// Method to Generate Selection Formula 
        /// </summary>
        public void GenerateSelectionFormula()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                #region Declaration
                string strMonthYear = default(string);
                string strSelectionFormula = default(string);
               
                strMonthYear = ddlMonthYear.Text;
                string strMonthYearField = default(string);
                string strSalesBranchCodeField = default(string);
                string strReportName = default(string);
                strMonthYearField = "{V_SalesBranch.Month_year}";
                strSalesBranchCodeField = "{V_SalesBranch.branch_code}";
                strReportName = "TargetSales";
                #endregion

                #region SelectionFormula Formation
                Microsoft.Practices.EnterpriseLibrary.Data.Database ImpalDB = DataAccess.GetDatabase();

                if (strBranchCode == "CRP")
                {
                    strSelectionFormula = strMonthYearField + "=" + "'" + strMonthYear + "'";
                }
                else
                {
                    strSelectionFormula = strMonthYearField + "=" + "'" + strMonthYear + "'" + "and" +
                                        strSalesBranchCodeField + "=" + " '" + strBranchCode + "'";
                }
                #endregion
                crTargetSales.ReportName = strReportName;
                crTargetSales.RecordSelectionFormula = strSelectionFormula;
                crTargetSales.GenerateReport();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion 

        #region Button Click
        /// <summary>
        /// Report button click 
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
