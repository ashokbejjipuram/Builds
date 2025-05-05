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
    public partial class BeforeCN : System.Web.UI.Page
    {
        string strBranchCode = default(string);

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
                    if (crBeforeCn_Branch != null)
                    {
                        crBeforeCn_Branch.Dispose();
                        crBeforeCn_Branch = null;
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
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crBeforeCn_Branch != null)
            {
                crBeforeCn_Branch.Dispose();
                crBeforeCn_Branch = null;
            }
        }
        protected void crBeforeCn_Branch_Unload(object sender, EventArgs e)
        {
            if (crBeforeCn_Branch != null)
            {
                crBeforeCn_Branch.Dispose();
                crBeforeCn_Branch = null;
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
                        crBeforeCn_Branch.ReportName = "BeforeCn_Branch";
                    else if (ddlBSZ.SelectedValue == "State")
                        crBeforeCn_Branch.ReportName = "BeforeCn_State";
                    else if (ddlBSZ.SelectedValue == "Zone")
                        crBeforeCn_Branch.ReportName = "BeforeCn_Zone";

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
            string strGPBrcode = default(string);
            string strGPMonyr = default(string);
            int intProcStatus = default(int);
            string strSelectionFormula = default(string);

            strMonYr = ddlMonYr.Text;
            strGPBrcode = "{gponsale.brcode}";
            strGPMonyr = "{gponsale.month_year}";
             Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
             //Log.WriteLog(source, "GenerateSelectionFormula", "Entering GenerateSelectionFormula");
            try
            {

            Microsoft.Practices.EnterpriseLibrary.Data.Database ImpalDB = DataAccess.GetDatabase();
            DbCommand dbcmd = ImpalDB.GetStoredProcCommand("usp_gponsale");
            ImpalDB.AddInParameter(dbcmd, "@Branch_Code", DbType.String, strBranchCode);
            ImpalDB.AddInParameter(dbcmd, "@month_year", DbType.String, strMonYr.Trim());
            dbcmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            intProcStatus = ImpalDB.ExecuteNonQuery(dbcmd);

            if (strBranchCode == "CRP")
                strSelectionFormula = strGPMonyr + "=" + "'" + strMonYr + "'";
            else
                strSelectionFormula = strGPMonyr + "=" + "'" + strMonYr + " 'and " + strGPBrcode + "=" + " '" + strBranchCode + "'";

            crBeforeCn_Branch.RecordSelectionFormula = strSelectionFormula;
            crBeforeCn_Branch.GenerateReport();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion
    }
}
