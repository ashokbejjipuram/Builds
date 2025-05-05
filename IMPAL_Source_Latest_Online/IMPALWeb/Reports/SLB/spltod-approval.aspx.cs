using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using IMPALLibrary.Masters;

namespace IMPALWeb.Reports.SLB
{
    public partial class spltod_approval : System.Web.UI.Page
    {
        string strBranchCode = string.Empty;

        #region Page Init
        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
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
            try
            {

                if (!IsPostBack)
                {
                    if (crspltodapproval != null)
                    {
                        crspltodapproval.Dispose();
                        crspltodapproval = null;
                    }

                    fnPopulateItemCode();
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
            if (crspltodapproval != null)
            {
                crspltodapproval.Dispose();
                crspltodapproval = null;
            }
        }
        protected void crspltodapproval_Unload(object sender, EventArgs e)
        {
            if (crspltodapproval != null)
            {
                crspltodapproval.Dispose();
                crspltodapproval = null;
            }
        }

        #region fnPopulateLineCode

        public void fnPopulateItemCode()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {

            ItemMasters itemcode = new ItemMasters();
            ddlcode.DataSource = itemcode.GetItemCode(Session["BranchCode"].ToString());
            ddlcode.DataTextField = "Supplierpartno";
            ddlcode.DataValueField = "itemcode";
            ddlcode.DataBind();
            if (itemcode != null)
                itemcode = null;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region report generation
        protected void btnReport_Click(object sender, EventArgs e)
        {
            strBranchCode = Session["BranchCode"].ToString();
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
                        string SLBItemcode = "{SLB_Item_calculation.item_code}";
                        string SLBbranchcode = "{SLB_Item_calculation.branch_code}";

                        if (strBranchCode != "CRP" || (ddlcode.SelectedValue != "CRP" && strBranchCode == "CRP"))
                        {
                            strSelectionFormula = SLBbranchcode + " ='" + ddlcode.SelectedValue + "'";
                        }

                        if (ddlcode.SelectedValue == string.Empty && strBranchCode != "CRP")
                        {
                            strSelectionFormula = SLBItemcode + " ='" + ddlcode.SelectedValue + "'";
                        }

                        crspltodapproval.ReportName = "Impal-report-spltod-approval";
                        crspltodapproval.RecordSelectionFormula = strSelectionFormula;
                        crspltodapproval.GenerateReport();
                    }
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
