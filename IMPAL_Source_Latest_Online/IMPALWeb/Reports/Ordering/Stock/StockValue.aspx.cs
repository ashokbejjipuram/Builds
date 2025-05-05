#region Declaration
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
#endregion

namespace IMPALWeb.Reports.Ordering.Stock
{
    public partial class StockValue : System.Web.UI.Page
    {
        string Branchcode = string.Empty;

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

        #region page_load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!IsPostBack)
                {
                    if (crystockvalue != null)
                    {
                        crystockvalue.Dispose();
                        crystockvalue = null;
                    }

                    if (Session["BranchCode"] != null)
                        Branchcode = (string)Session["BranchCode"];

                    if (Branchcode != "CRP")
                    {
                        ddlbranchcode.SelectedValue = Branchcode.ToString();
                        ddlbranchcode.Enabled = false;
                    }
                    else
                    {
                        ddlbranchcode.Enabled = true;

                    }
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
            if (crystockvalue != null)
            {
                crystockvalue.Dispose();
                crystockvalue = null;
            }
        }
        protected void crystockvalue_Unload(object sender, EventArgs e)
        {
            if (crystockvalue != null)
            {
                crystockvalue.Dispose();
                crystockvalue = null;
            }
        }

        #region Button_Click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Main mainmaster = (Main)Page.Master;
                mainmaster.ShowHideFilters(btnReport, reportFiltersTable, reportViewerHolder);
                if (btnReport.Text == "Back")
                {
                    string selectionformula = string.Empty;
                    string stselectionformula = ddlbranchcode.SelectedValue;

                    if (ddlbranchcode.SelectedValue == "0")
                    {
                        selectionformula = string.Empty;
                    }
                    else
                    {
                        selectionformula = "{branch_master.branch_code}= '" + stselectionformula + "' and";
                    }

                    selectionformula = selectionformula + "{consignment.status}='A'";

                    crystockvalue.RecordSelectionFormula = selectionformula;
                    crystockvalue.ReportName = "stock_value1";
                    crystockvalue.GenerateReport();
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
