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

namespace IMPALWeb.Reports.Gross_Profit
{
    public partial class AfterCN_Summary_ : System.Web.UI.Page
    {
        string strBranchCode = default(string);

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
                    if (crAfterCnSummary != null)
                    {
                        crAfterCnSummary.Dispose();
                        crAfterCnSummary = null;
                    }

                    PopulateMonthYear();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

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
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crAfterCnSummary != null)
            {
                crAfterCnSummary.Dispose();
                crAfterCnSummary = null;
            }
        }

        protected void crAfterCnSummary_Unload(object sender, EventArgs e)
        {
            if (crAfterCnSummary != null)
            {
                crAfterCnSummary.Dispose();
                crAfterCnSummary = null;
            }
        }

        #region Populate MonthYear
        public void PopulateMonthYear()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "PopulateMonthYear", "Entering PopulateMonthYear");
            try
            {
                IMPALLibrary.Masters.Sales.LineWiseSales monyr = new IMPALLibrary.Masters.Sales.LineWiseSales();
                ddlMonYr.DataSource = monyr.GetMonthYear(null);
                ddlMonYr.DataTextField = "MonthYear";
                ddlMonYr.DataValueField = "MonthYear";
                ddlMonYr.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
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
                    string strMonYr = default(string);
                    string strGPBrcode = default(string);
                    int intProcStatus = default(int);
                    string strSelectionFormula = default(string);

                    strMonYr = ddlMonYr.SelectedValue;
                    strGPBrcode = "{branch_master.branch_code}";

                    Database ImpalDB = DataAccess.GetDatabase();
                    DbCommand dbcmd = ImpalDB.GetStoredProcCommand("usp_Gp_Summary");
                    ImpalDB.AddInParameter(dbcmd, "@Branch_Code", DbType.String, strBranchCode);
                    ImpalDB.AddInParameter(dbcmd, "@MOn_Year", DbType.String, strMonYr.Trim());
                    dbcmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    intProcStatus = ImpalDB.ExecuteNonQuery(dbcmd);

                    if (strBranchCode != "CRP")
                        strSelectionFormula = strGPBrcode + " ='" + strBranchCode + "'";

                    crAfterCnSummary.RecordSelectionFormula = strSelectionFormula;
                    crAfterCnSummary.GenerateReport();
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
