#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Masters.Sales; 
#endregion

namespace IMPALWeb.Reports.Sales.SalesStatement
{
    public partial class BranchRanking : System.Web.UI.Page
    {
        #region Declaration
        string strBranchCode = string.Empty;
        #endregion

        #region Page_Init
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
                if (Session["BranchCode"] != null)
                    strBranchCode = (string)Session["BranchCode"];
                if (!IsPostBack)
                {
                    if (crSalesBranchRanking != null)
                    {
                        crSalesBranchRanking.Dispose();
                        crSalesBranchRanking = null;
                    }

                    LoadAccPeriodDDL();
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
            if (crSalesBranchRanking != null)
            {
                crSalesBranchRanking.Dispose();
                crSalesBranchRanking = null;
            }
        }
        protected void crSalesBranchRanking_Unload(object sender, EventArgs e)
        {
            if (crSalesBranchRanking != null)
            {
                crSalesBranchRanking.Dispose();
                crSalesBranchRanking = null;
            }
        }

        #region LoadAccPeriodDDL
        private void LoadAccPeriodDDL()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                AccountingPeriods oAcc = new AccountingPeriods();
                ddlAccPeriod.DataSource = oAcc.GetAccountingPeriod(1, "BranchBanking", strBranchCode);
                ddlAccPeriod.DataTextField = "Desc";
                ddlAccPeriod.DataValueField = "AccPeriodCode";
                ddlAccPeriod.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region btnReport_Click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Main mainmaster = (Main)Page.Master;
                mainmaster.ShowHideFilters(btnReport, reportFiltersTable, reportViewerHolder);
                if (btnReport.Text == "Back")
                {
                    CallCrystalReport();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region CallCrystalReport
        private void CallCrystalReport()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!string.IsNullOrEmpty(strBranchCode))
                {

                    #region Declaration
                    string strSelectionFormula = null;

                    string strAccPeriodQuery = "{branch_ranking1.accounting_period_Code}";
                    string strBranchQuery = "{branch_ranking1.branch_code}";
                    #endregion

                    #region Selction Formula Formation
                    Salesbranches oBranch = new Salesbranches();
                    int intSuccess = oBranch.RankBranch(strBranchCode, Convert.ToInt16(ddlAccPeriod.SelectedValue));
                    //if (intSuccess > 0)
                    //{
                    if (strBranchCode.Equals("CRP"))
                        strSelectionFormula = strAccPeriodQuery + " = " + ddlAccPeriod.SelectedValue;
                    else
                        strSelectionFormula = strAccPeriodQuery + " = " + ddlAccPeriod.SelectedValue
                            + " and " + strBranchQuery + " = '" + strBranchCode + "'";
                    //}
                    #endregion

                    crSalesBranchRanking.RecordSelectionFormula = strSelectionFormula;
                    crSalesBranchRanking.GenerateReport();
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
