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
    public partial class TrialBalance : System.Web.UI.Page
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
                    if (crTrialBal_Branch != null)
                    {
                        crTrialBal_Branch.Dispose();
                        crTrialBal_Branch = null;
                    }

                    PopulateBSZ();
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
            if (crTrialBal_Branch != null)
            {
                crTrialBal_Branch.Dispose();
                crTrialBal_Branch = null;
            }
        }
        protected void crTrialBal_Branch_Unload(object sender, EventArgs e)
        {
            if (crTrialBal_Branch != null)
            {
                crTrialBal_Branch.Dispose();
                crTrialBal_Branch = null;
            }
        }

        #region PopulateBSZ
        public void PopulateBSZ()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "PopulateBSZ", "Entering PopulateBSZ");
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                List<DropDownListValue> oList = new List<DropDownListValue>();
                oList = oCommon.GetDropDownListValues("B/S/Z");
                ddlBSZ.DataSource = oList;
                ddlBSZ.DataValueField = "DisplayValue";
                ddlBSZ.DataTextField = "DisplayText";
                ddlBSZ.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

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
                    if (ddlBSZ.SelectedValue == "Branch")
                        crTrialBal_Branch.ReportName = "TrialBal_Branch";
                    else if (ddlBSZ.SelectedValue == "State")
                        crTrialBal_Branch.ReportName = "TrialBal_State";
                    else if (ddlBSZ.SelectedValue == "Zone")
                        crTrialBal_Branch.ReportName = "TrialBal_Zone";

                    GenerateSelectionFormula();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Generate Selection Formula
        public void GenerateSelectionFormula()
        {
            string strMonYr = default(string);
            string strBrcode = default(string);
            int intProcStatus = default(int);
            string strSelectionFormula = default(string);

            strMonYr = ddlMonYr.Text;
            strBrcode = "{branch_master.branch_code}";
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "GenerateSelectionFormula", "Entering GenerateSelectionFormula");
            try
            {

                Microsoft.Practices.EnterpriseLibrary.Data.Database ImpalDB = DataAccess.GetDatabase();
                DbCommand dbcmd = ImpalDB.GetStoredProcCommand("usp_gp_trial_balance");
                ImpalDB.AddInParameter(dbcmd, "@Branch_Code", DbType.String, strBranchCode);
                ImpalDB.AddInParameter(dbcmd, "@Month_Year", DbType.String, strMonYr.Trim());
                dbcmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                intProcStatus = ImpalDB.ExecuteNonQuery(dbcmd);

                if (strBranchCode != "CRP")
                    strSelectionFormula = strBrcode + " ='" + strBranchCode + "'";

                crTrialBal_Branch.RecordSelectionFormula = strSelectionFormula;
                crTrialBal_Branch.GenerateReport();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);

            }
        }
        #endregion
    }
}
