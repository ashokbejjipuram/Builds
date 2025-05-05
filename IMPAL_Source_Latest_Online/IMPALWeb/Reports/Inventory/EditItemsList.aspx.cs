#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Common;
#endregion

namespace IMPALWeb.Reports.Inventory
{
    public partial class EditItemsList : System.Web.UI.Page
    {
        #region Declaration
        string strBranchCode = string.Empty;
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
                    if (crStockAdjustment != null)
                    {
                        crStockAdjustment.Dispose();
                        crStockAdjustment = null;
                    }

                    PopulateStationddl();
                    txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
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
            if (crStockAdjustment != null)
            {
                crStockAdjustment.Dispose();
                crStockAdjustment = null;
            }
        }
        protected void crStockAdjustment_Unload(object sender, EventArgs e)
        {
            if (crStockAdjustment != null)
            {
                crStockAdjustment.Dispose();
                crStockAdjustment = null;
            }
        }

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

        #region PopulateStationddl
        public void PopulateStationddl()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                List<DropDownListValue> oList = new List<DropDownListValue>();
                oList = oCommon.GetDropDownListValues("Station");
                ddlStation.DataSource = oList;
                ddlStation.DataValueField = "DisplayValue";
                ddlStation.DataTextField = "DisplayText";
                ddlStation.DataBind();
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
                    if (!string.IsNullOrEmpty(strBranchCode))
                    {
                        string strSelectionFormula = null;
                        string strFromDate = null;
                        string strToDate = null;
                        if (!string.IsNullOrEmpty(txtFromDate.Text) && !string.IsNullOrEmpty(txtToDate.Text))
                        {
                            strFromDate = Convert.ToDateTime(hidFromDate.Value).ToString("yyyy,MM,dd");
                            strToDate = Convert.ToDateTime(hidToDate.Value).ToString("yyyy,MM,dd");
                            string strInwardDate = "{Consignment.Inward_date}";
                            string strDateCompare = strInwardDate + " >= Date (" + strFromDate + ") and "
                                                    + strInwardDate + " <= Date (" + strToDate + ")";

                            if (ddlFromLine.SelectedValue.Equals("0"))
                            {
                                if (ddlStation.SelectedValue.Equals("Both"))
                                {
                                    if (strBranchCode.Equals("CRP"))
                                        strSelectionFormula = strDateCompare;
                                    else
                                        strSelectionFormula = strDateCompare + " and {consignment.branch_code} = '" + strBranchCode + "'";
                                }
                                else
                                {
                                    if (strBranchCode.Equals("CRP"))
                                        strSelectionFormula = strDateCompare + " and {consignment.OS_LS_Indicator} ='" + ddlStation.SelectedValue + "'";
                                    else
                                        strSelectionFormula = strDateCompare + " and {consignment.branch_code} = '" + strBranchCode + "'";
                                }
                            }
                            else
                            {
                                string strLineCompare = "mid({consignment.Item_Code},1,3) >= '" + ddlFromLine.SelectedValue
                                                      + "' and mid({consignment.Item_Code},1,3) <= '" + ddlToLine.SelectedValue + "'";
                                if (ddlStation.SelectedValue.Equals("Both"))
                                {
                                    strSelectionFormula = strDateCompare + " and " + strLineCompare;
                                }
                                else
                                {
                                    if (strBranchCode.Equals("CRP"))
                                        strSelectionFormula = strDateCompare + " and {consignment.OS_LS_Indicator} ='" + ddlStation.SelectedValue
                                                            + "' and " + strLineCompare;
                                    else
                                        strSelectionFormula = strDateCompare + " and {Branch_Master.branch_code} = '" + strBranchCode
                                                            + "' and {consignment.OS_LS_Indicator} ='" + ddlStation.SelectedValue
                                                            + "' and " + strLineCompare;
                                }
                            }
                            crStockAdjustment.RecordSelectionFormula = strSelectionFormula;
                            crStockAdjustment.GenerateReport();
                        }
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