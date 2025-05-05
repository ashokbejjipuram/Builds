#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Common;
using CrystalDecisions.Web;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

#endregion

namespace IMPALWeb.Reports.Inventory
{
    public partial class StockSheetList : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = (string)Session["BranchCode"];

                if (crStockSheetList != null)
                {
                    crStockSheetList.Dispose();
                    crStockSheetList = null;
                }
            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crStockSheetList != null)
            {
                crStockSheetList.Dispose();
                crStockSheetList = null;
            }
        }
        protected void crStockSheetList_Unload(object sender, EventArgs e)
        {
            if (crStockSheetList != null)
            {
                crStockSheetList.Dispose();
                crStockSheetList = null;
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
                    if (!string.IsNullOrEmpty(strBranchCode))
                    {
                        string strSelectionFormula = null;
                        if (ddlFromLine.SelectedIndex > 0)
                        {
                            string strLineItem = "mid({item_master.item_Code},1,3)";
                            strSelectionFormula = strLineItem + " >= '" + ddlFromLine.SelectedValue + "' and "
                                                    + strLineItem + " <= '" + ddlToLine.SelectedValue + "'";
                            if (!strBranchCode.Equals("CRP"))
                                strSelectionFormula = strSelectionFormula + " and mid({consignment.inward_number},12,3) = '" + strBranchCode + "'";
                        }
                        crStockSheetList.RecordSelectionFormula = strSelectionFormula;
                        crStockSheetList.GenerateReport();
                    }
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