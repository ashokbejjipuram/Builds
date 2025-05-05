#region Namespace Declaration
using System;
using System.Collections.Generic;
using IMPALLibrary.Common;
#endregion

namespace IMPALWeb.Reports.Inventory.Aging
{
    public partial class Part_summary : System.Web.UI.Page
    {
        #region Page Init
        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "Page_Init", "Part Summary Page Init Method"); 
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
            //Log.WriteLog(Source, "Page_Load", "Part Summary Page Load Method"); 
            try
            {
                if (!IsPostBack)
                {
                    if (crpartsummary != null)
                    {
                        crpartsummary.Dispose();
                        crpartsummary = null;
                    }

                    ImpalLibrary oCommon = new ImpalLibrary();
                    ddlAgingSummary.DataSource = oCommon.GetDropDownListValues("Aging Summary");
                    ddlAgingSummary.DataTextField = "DisplayText";
                    ddlAgingSummary.DataValueField = "DisplayValue";
                    ddlAgingSummary.DataBind();
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
            if (crpartsummary != null)
            {
                crpartsummary.Dispose();
                crpartsummary = null;
            }
        }
        protected void crpartsummary_Unload(object sender, EventArgs e)
        {
            if (crpartsummary != null)
            {
                crpartsummary.Dispose();
                crpartsummary = null;
            }
        }

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
                    string sessionvalue = string.Empty;
                    sessionvalue = (string)Session["BranchCode"].ToString();
                    string selectionformula = string.Empty;
                    string aging = ddlAgingSummary.SelectedValue;
                    string selbranch = "{consignment.branch_code}";
                    if (aging == "0")
                    {
                        crpartsummary.ReportName = "Inv_sum_lessthan3";
                    }
                    else if (aging == "1")
                    {
                        crpartsummary.ReportName = "Inv_sum_bet_3to6m";
                    }
                    else if (aging == "2")
                    {
                        crpartsummary.ReportName = "Inv_sum_bet_6to1yr";
                    }
                    else if (aging == "3")
                    {
                        crpartsummary.ReportName = "Inv_sum_bet_1to2yr";
                    }
                    else
                    {
                        crpartsummary.ReportName = "Inv_sum_greaterthan_2yr";
                    }

                    if (sessionvalue != "CRP")
                    {
                        selectionformula = selbranch + " = '" + sessionvalue + "'";
                    }
                    crpartsummary.RecordSelectionFormula = selectionformula;
                    crpartsummary.GenerateReport();
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