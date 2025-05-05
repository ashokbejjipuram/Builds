using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using IMPALLibrary.Common;
using IMPALLibrary.Masters;

namespace IMPALWeb.Reports.SLB
{
    public partial class ipl_depotbilling : System.Web.UI.Page
    {
        string strBranchCode = string.Empty;

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!IsPostBack)
                {
                    if (cripldepotbilling != null)
                    {
                        cripldepotbilling.Dispose();
                        cripldepotbilling = null;
                    }

                    Populatedepotddl();
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
            if (cripldepotbilling != null)
            {
                cripldepotbilling.Dispose();
                cripldepotbilling = null;
            }
        }
        protected void cripldepotbilling_Unload(object sender, EventArgs e)
        {
            if (cripldepotbilling != null)
            {
                cripldepotbilling.Dispose();
                cripldepotbilling = null;
            }
        }

        #region Populatedepotddl
        public void Populatedepotddl()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                List<DropDownListValue> oList = new List<DropDownListValue>();
                oList = oCommon.GetDropDownListValues("depot");
                ddldepot.DataSource = oList;
                ddldepot.DataValueField = "DisplayValue";
                ddldepot.DataTextField = "DisplayText";
                ddldepot.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region fnPopulateLineCode

        public void fnPopulateItemCode()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ItemMasters itemcode = new ItemMasters();
                ddlcode.DataSource = itemcode.GetItemCode(strBranchCode);
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
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Main mainmaster = (Main)Page.Master;
                mainmaster.ShowHideFilters(btnReport, reportFiltersTable, reportViewerHolder);
                if (btnReport.Text == "Back")
                {
                    strBranchCode = Session["BranchCode"].ToString();
                    if (!string.IsNullOrEmpty(strBranchCode))
                    {
                        string strSelectionFormula = null;
                        string strslbbranchcode = "{SLB_Item_calculation.branch_code}";
                        string SLBItemcode = "{SLB_Item_calculation.item_code}";
                        if ((strBranchCode != "CRP") || (ddlcode.SelectedValue != "CRP" && strBranchCode == "CRP"))
                        {
                            strSelectionFormula = strslbbranchcode + "='" + ddlcode.SelectedValue + "'";

                        }

                        if (ddlcode.SelectedValue == "" && strBranchCode != "CRP")
                        {
                            strSelectionFormula = SLBItemcode + " ='" + ddlcode.SelectedValue + "'";
                        }
                        cripldepotbilling.RecordSelectionFormula = strSelectionFormula;

                        switch (ddldepot.SelectedValue)
                        {
                            case "IPL Depot":
                                cripldepotbilling.ReportName = "Impal-report-IPL-billing";
                                break;
                            case "IPL Non Depot":
                                cripldepotbilling.ReportName = "Impal-report-Nondepot";
                                break;
                        }
                        cripldepotbilling.GenerateReport();
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