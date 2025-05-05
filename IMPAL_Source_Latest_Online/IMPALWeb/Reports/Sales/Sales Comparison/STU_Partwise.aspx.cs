#region Namespace Declaration
using System;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using IMPALLibrary;
using System.Data;
using System.Data.Common;
using IMPALLibrary.Masters.Sales;
#endregion

namespace IMPALWeb.Reports.Sales.Sales_Comparison
{
    public partial class STU_Partwise : System.Web.UI.Page
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
                    if (crstupartwise != null)
                    {
                        crstupartwise.Dispose();
                        crstupartwise = null;
                    }

                    fnPopulateMonthyear();
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
            if (crstupartwise != null)
            {
                crstupartwise.Dispose();
                crstupartwise = null;
            }
        }
        protected void crstupartwise_Unload(object sender, EventArgs e)
        {
            if (crstupartwise != null)
            {
                crstupartwise.Dispose();
                crstupartwise = null;
            }
        }

        #region Populate Month Year Dropdown
        private void fnPopulateMonthyear()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                LineWiseSales linewise = new LineWiseSales();
                ddlMonthYear.DataSource = linewise.GetMonthYear(null);
                ddlMonthYear.DataValueField = "MonthYear";
                ddlMonthYear.DataTextField = "MonthYear";
                ddlMonthYear.DataBind();
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
                    string strparameter = ddlMonthYear.SelectedValue.ToString();
                    Database ImpalDB = DataAccess.GetDatabase();
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_cmpr_itemwise");
                    ImpalDB.AddInParameter(cmd, "@month_year", DbType.String, ddlMonthYear.SelectedValue);
                    ImpalDB.AddInParameter(cmd, "@branch_code", DbType.String, sessionvalue);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);

                    crstupartwise.ReportName = "PartNowisesales_periods";
                    crstupartwise.CrystalFormulaFields.Add("monthyear", "\"" + ddlMonthYear.SelectedValue + "\"");
                    crstupartwise.RecordSelectionFormula = selectionformula;
                    crstupartwise.GenerateReport();
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
