#region Namespace Declaration
using System;
using System.Collections.Generic;
using IMPALLibrary.Common;
using System.Globalization;
#endregion

namespace IMPALWeb.Reports.Inventory.Aging
{
    public partial class AllbranchesPartdetail : System.Web.UI.Page
    {
        string strBranchCode = string.Empty;

        #region Page init
        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "Page_Init", "All branches Part Detail Page Init Method");
            try
            {
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region page load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "Page_Load", "All branches Part Detail Page Load Method");
            try
            {
                strBranchCode = (string)Session["BranchCode"].ToString();
                if (!IsPostBack)
                {
                    if (crallbranch != null)
                    {
                        crallbranch.Dispose();
                        crallbranch = null;
                    }

                    fnPopulateReportType();
                    divdate.Visible = false;
                    txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtToDate.Text = txtFromDate.Text;
                }
                if (IsPostBack)
                {
                    if (ddlAging.SelectedIndex == 5)
                    {
                        divdate.Visible = true;
                    }
                    else
                    { divdate.Visible = false; }

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
            if (crallbranch != null)
            {
                crallbranch.Dispose();
                crallbranch = null;
            }
        }
        protected void crallbranch_Unload(object sender, EventArgs e)
        {
            if (crallbranch != null)
            {
                crallbranch.Dispose();
                crallbranch = null;
            }
        }

        #region Report Type and Aging Dropdown Populate
        protected void fnPopulateReportType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "fnPopulateReportType()", "All branches Part Detail Report type Populate Method");
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                ddlReportType.DataSource = oCommon.GetDropDownListValues("RepType-Aging");
                ddlReportType.DataTextField = "DisplayText";
                ddlReportType.DataValueField = "DisplayValue";
                ddlReportType.DataBind();

                ddlAging.DataSource = oCommon.GetDropDownListValues("Aging Branch");
                ddlAging.DataTextField = "DisplayText";
                ddlAging.DataValueField = "DisplayValue";
                ddlAging.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Report button click
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
                    string selparamsupplier = "{supplier_master.supplier_code}";
                    string selparambranch = "{consignment_AllBranches.branch_code}";
                    if (ddlAging.SelectedIndex != 5 && ddlReportType.SelectedIndex == 0)
                    {
                        string selsupplier = ddlSupplierCode.SelectedValue;
                        if (ddlAging.SelectedIndex == 0)
                        {
                            crallbranch.ReportName = "Inv_det_lessthan3_allbranches";
                            if (ddlSupplierCode.SelectedIndex != 0)
                            {
                                crallbranch.RecordSelectionFormula = selparamsupplier + " = '" + selsupplier + "'";
                            }
                        }
                        else if (ddlAging.SelectedIndex == 1)
                        {
                            crallbranch.ReportName = "Inv_det_bet_3to6m_allbranches";
                            if (ddlSupplierCode.SelectedIndex != 0)
                            {
                                crallbranch.RecordSelectionFormula = selparamsupplier + " = '" + selsupplier + "'";
                            }
                        }
                        else if (ddlAging.SelectedIndex == 2)
                        {
                            crallbranch.ReportName = "Inv_det_bet_6to1yr_allbranches";
                            if (ddlSupplierCode.SelectedIndex != 0)
                            {
                                crallbranch.RecordSelectionFormula = selparamsupplier + " = '" + selsupplier + "'";
                            }
                        }
                        else if (ddlAging.SelectedIndex == 3)
                        {
                            crallbranch.ReportName = "Inv_det_bet_1to2yr_allbranches";
                            if (ddlSupplierCode.SelectedIndex != 0)
                            {
                                crallbranch.RecordSelectionFormula = selparamsupplier + " = '" + selsupplier + "'";
                            }
                        }
                        else if (ddlAging.SelectedIndex == 4)
                        {
                            crallbranch.ReportName = "Inv_det_greaterthan_2yr_allbranches";
                            if (ddlSupplierCode.SelectedIndex != 0)
                            {
                                crallbranch.RecordSelectionFormula = selparamsupplier + " = '" + selsupplier + "'";
                            }
                        }
                    }
                    else if (ddlAging.SelectedIndex == 5 && ddlReportType.SelectedIndex == 0)
                    {
                        string selsupplier = ddlSupplierCode.SelectedValue;
                        crallbranch.ReportName = "Inv_det_givenperiod_allbranches";
                        string strFromDate = null;
                        string strToDate = null;
                        string receiveddate = "{consignment_AllBranches.Original_Receipt_Date}";
                        if (!string.IsNullOrEmpty(txtFromDate.Text) && !string.IsNullOrEmpty(txtToDate.Text))
                        {
                            strFromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy,MM,dd");
                            strToDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy,MM,dd");

                            if (ddlSupplierCode.SelectedIndex != 0 && strBranchCode == "CRP")
                            {
                                crallbranch.RecordSelectionFormula = receiveddate + ">=Date (" + strFromDate + ") and " + receiveddate + " <= Date (" + strToDate + ") and " + selparamsupplier + "='" + selsupplier + "'";
                            }
                            else if (ddlSupplierCode.SelectedIndex == 0 && strBranchCode == "CRP")
                            {
                                crallbranch.RecordSelectionFormula = receiveddate + ">=Date (" + strFromDate + ") and " + receiveddate + " <= Date (" + strToDate + ")";
                            }
                            else if (ddlSupplierCode.SelectedIndex != 0 && strBranchCode != "CRP")
                            {
                                crallbranch.RecordSelectionFormula = receiveddate + ">=Date (" + strFromDate + ") and " + receiveddate + " <= Date (" + strToDate + ") and " + selparamsupplier + "='" + selsupplier + "' and " + selparambranch + "='" + strBranchCode + "'";
                            }
                            else if (ddlSupplierCode.SelectedIndex == 0 && strBranchCode != "CRP")
                            {
                                crallbranch.RecordSelectionFormula = receiveddate + ">=Date (" + strFromDate + ") and " + receiveddate + " <= Date (" + strToDate + ") and " + selparambranch + "='" + strBranchCode + "'";
                            }
                        }
                    }
                    else if (ddlAging.SelectedIndex != 5 && ddlReportType.SelectedIndex == 1)
                    {
                        string selsupplier = ddlSupplierCode.SelectedValue;
                        crallbranch.ReportName = "Aging_AllBranches";
                        if (ddlSupplierCode.SelectedIndex != 0)
                        {
                            crallbranch.RecordSelectionFormula = selparamsupplier + " = '" + selsupplier + "'";
                        }
                    }
                    else if (ddlAging.SelectedIndex == 5 && ddlReportType.SelectedIndex != 0)
                    {
                        string selsupplier = ddlSupplierCode.SelectedValue;
                        crallbranch.ReportName = "Aging_AllBranches";
                        string strFromDate = null;
                        string strToDate = null;
                        string receiveddate = "{consignment_AllBranches.Original_Receipt_Date}";

                        if (!string.IsNullOrEmpty(txtFromDate.Text) && !string.IsNullOrEmpty(txtToDate.Text))
                        {
                            strFromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy,MM,dd");
                            strToDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy,MM,dd");

                            if (ddlSupplierCode.SelectedIndex != 0 && strBranchCode == "CRP")
                            {
                                crallbranch.RecordSelectionFormula = receiveddate + ">=Date (" + strFromDate + ") and " + receiveddate + " <= Date (" + strToDate + ") and " + selparamsupplier + "='" + selsupplier + "'";
                            }
                            else if (ddlSupplierCode.SelectedIndex == 0 && strBranchCode == "CRP")
                            {
                                crallbranch.RecordSelectionFormula = receiveddate + ">=Date (" + strFromDate + ") and " + receiveddate + " <= Date (" + strToDate + ")";
                            }
                            else if (ddlSupplierCode.SelectedIndex != 0 && strBranchCode != "CRP")
                            {
                                crallbranch.RecordSelectionFormula = receiveddate + ">=Date (" + strFromDate + ") and " + receiveddate + " <= Date (" + strToDate + ") and " + selparamsupplier + "='" + selsupplier + "' and " + selparambranch + "='" + strBranchCode + "'";
                            }
                            else if (ddlSupplierCode.SelectedIndex == 0 && strBranchCode != "CRP")
                            {
                                crallbranch.RecordSelectionFormula = receiveddate + ">=Date (" + strFromDate + ") and " + receiveddate + " <= Date (" + strToDate + ") and " + selparambranch + "='" + strBranchCode + "'";
                            }
                        }
                    }

                    crallbranch.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
                    crallbranch.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
                    crallbranch.GenerateReport();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region summary button click
        protected void btnsummary_Click(object sender, EventArgs e)
        {


        }
        #endregion 

        protected void txtToDate_TextChanged(object sender, EventArgs e)
        {

        }
    }
}