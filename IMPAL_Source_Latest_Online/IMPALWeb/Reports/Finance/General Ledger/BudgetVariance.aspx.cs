using System;
using System.Collections.Generic;

namespace IMPALWeb.Reports.Finance.General_Ledger
{
    public partial class BudgetVariance : System.Web.UI.Page
    {
        #region Page Init
        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "Page_Init", "Budget Variance Page Init Method"); 
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
            //Log.WriteLog(Source, "Page_Load", "Budget Variance Page Load Method");
            try
            {
                if (!IsPostBack)
                {
                    if (crBudgetVariance != null)
                    {
                        crBudgetVariance.Dispose();
                        crBudgetVariance = null;
                    }

                    fnPopulateGLMain();
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
            if (crBudgetVariance != null)
            {
                crBudgetVariance.Dispose();
                crBudgetVariance = null;
            }
        }
        protected void crBudgetVariance_Unload(object sender, EventArgs e)
        {
            if (crBudgetVariance != null)
            {
                crBudgetVariance.Dispose();
                crBudgetVariance = null;
            }
        }

        #region Populare Gl main Dropdown
        protected void fnPopulateGLMain()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "fnPopulateGLMain()", "Budget Variance Populate GLMain Method");
            try
            {
                IMPALLibrary.BudgetMasters objBudget = new IMPALLibrary.BudgetMasters();
                ddlFromGL.DataSource = objBudget.GetChartofAccountCode();
                ddlFromGL.DataValueField = "Chart_of_Account_Code";
                ddlFromGL.DataTextField = "Gl_main_description";
                ddlFromGL.DataBind();
                ddlToGL.DataSource = objBudget.GetChartofAccountCode();
                ddlToGL.DataValueField = "Chart_of_Account_Code";
                ddlToGL.DataTextField = "Gl_main_description";
                ddlToGL.DataBind();
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
                    string sessionBranch = string.Empty;
                    string selectionformula = string.Empty;
                    string strselMainCode = "{GL_master.Gl_main_code}";
                    string strselBranchCode = "{chart_of_Account.Branch_code}=";
                    string strFromGL = ddlFromGL.SelectedValue;
                    string strToGL = ddlToGL.SelectedValue;
                    if (Session["BranchCode"] != null)
                    {
                        sessionBranch = (string)Session["BranchCode"];
                    }

                    if (sessionBranch == "CRP")
                    {

                        if (ddlFromGL.SelectedValue != "0" && ddlToGL.SelectedValue != "0")
                        {
                            selectionformula = strselMainCode + ">='" + strFromGL + "' and " + strselMainCode + "<='" + strToGL + "'";
                        }
                        else if (ddlFromGL.SelectedValue == "0" && ddlToGL.SelectedValue != "0")
                        {
                            selectionformula = strselMainCode + "<='" + strToGL + "'";
                        }

                    }
                    else
                    {
                        if (ddlFromGL.SelectedValue != "0" && ddlToGL.SelectedValue != "0")
                        {
                            selectionformula = strselMainCode + ">='" + strFromGL + "' and " + strselMainCode + "<='" + strToGL + "' and " + strselBranchCode + "'" + sessionBranch + "'";
                        }
                        else if (ddlFromGL.SelectedValue == "0" && ddlToGL.SelectedValue != "0")
                        {
                            selectionformula = strselMainCode + "<='" + strToGL + "' and" + strselBranchCode + "'" + sessionBranch + "'";
                        }
                        else
                        {
                            selectionformula = strselBranchCode + "'" + sessionBranch + "'";
                        }
                    }
                    crBudgetVariance.ReportName = "Budget_variance";
                    crBudgetVariance.RecordSelectionFormula = selectionformula;
                    crBudgetVariance.GenerateReport();
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
