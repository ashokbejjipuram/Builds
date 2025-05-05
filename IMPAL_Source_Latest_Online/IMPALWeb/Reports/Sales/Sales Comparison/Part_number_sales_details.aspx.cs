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
    public partial class Part_number_sales_details : System.Web.UI.Page
    {
        #region PageInit
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

        #region Pageload
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!IsPostBack)
                {
                    if (crpartnumber != null)
                    {
                        crpartnumber.Dispose();
                        crpartnumber = null;
                    }

                    fnPopulateMonthYear();
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
            if (crpartnumber != null)
            {
                crpartnumber.Dispose();
                crpartnumber = null;
            }
        }
        protected void crpartnumber_Unload(object sender, EventArgs e)
        {
            if (crpartnumber != null)
            {
                crpartnumber.Dispose();
                crpartnumber = null;
            }
        }

        #region Populate Monthyear Dropdown
        private void fnPopulateMonthYear()
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
                    string sel1 = "{branch_master.branch_code}=";
                    string sel3 = "{supplier_master.Supplier_code}=";
                    int intsupplier = ddsupplier.SelectedIndex;
                    string strsupplier = ddsupplier.SelectedValue;
                    string strmonthyear = ddlMonthYear.SelectedValue;
                    Database ImpalDB = DataAccess.GetDatabase();
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_cmpr_itemwisesum");
                    ImpalDB.AddInParameter(cmd, "@branch", DbType.String, sessionvalue);
                    ImpalDB.AddInParameter(cmd, "@month_year", DbType.String, ddlMonthYear.SelectedValue);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);

                    if (sessionvalue == "CRP")
                    {
                        if (intsupplier != 0)
                        {
                            selectionformula = sel3 + "'" + strsupplier + "'";
                        }
                        else
                        {
                            selectionformula = null;
                        }
                    }
                    else
                    {
                        if (intsupplier == 0)
                        {
                            selectionformula = sel1 + "'" + sessionvalue + "'";
                        }
                        else
                        {
                            selectionformula = sel1 + "'" + sessionvalue + "' and " + sel3 + "'" + strsupplier + "'";
                        }
                    }
                    crpartnumber.ReportName = "partnosalesdetails";
                    crpartnumber.RecordSelectionFormula = selectionformula;
                    crpartnumber.GenerateReport();
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
