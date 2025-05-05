#region namespace declaration
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using IMPALLibrary.Common;
using System.Globalization;
#endregion


namespace IMPALWeb.Reports.Inventory.Aging
{
    public partial class Part_Details : System.Web.UI.Page
    {
        string strBranchCode = string.Empty;
        ImpalLibrary oCommon = new ImpalLibrary();

        #region Page Init
        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "Page_Init", "Part Details Page Init Method");
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
            //Log.WriteLog(Source, "Page_Load", "Part Details Page Load Method");
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = Session["BranchCode"].ToString();

                if (!IsPostBack)
                {
                    if (crpartdetails != null)
                    {
                        crpartdetails.Dispose();
                        crpartdetails = null;
                    }

                    ddaging.DataSource = oCommon.GetDropDownListValues("Aging");
                    ddaging.DataTextField = "DisplayText";
                    ddaging.DataValueField = "DisplayValue";
                    ddaging.DataBind();

                    ddlreporttype.DataSource = oCommon.GetDropDownListValues("RptType-List2");
                    ddlreporttype.DataTextField = "DisplayText";
                    ddlreporttype.DataValueField = "DisplayValue";
                    ddlreporttype.DataBind();

                    divDate.Visible = false;
                    divList.Visible = true;
                    div1.Visible = true;
                    txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtToDate.Text = txtFromDate.Text;

                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion
        protected void ddlreporttype_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crpartdetails != null)
            {
                crpartdetails.Dispose();
                crpartdetails = null;
            }
        }
        protected void crpartdetails_Unload(object sender, EventArgs e)
        {
            if (crpartdetails != null)
            {
                crpartdetails.Dispose();
                crpartdetails = null;
            }
        }

        #region nextitem button click
        protected void btnnextitem_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "btnnextitem_Click", "Part Details Next Item Button Click Method");
            try
            {
                ListItem li = lstsuppliers.Items.FindByText(ddsuppliercode.SelectedValue.ToString());
                if (li == null)
                {
                    lstsuppliers.Items.Add(new ListItem(ddsuppliercode.SelectedValue.ToString()));
                }
                ddsuppliercode.SelectedIndex = 0;
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }

        }
        #endregion

        #region remove button click
        protected void btnremove_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "btnremove_Click()", "Part Details Remove Button Click Method");
            try
            {
                lstsuppliers.Visible = true;
                List<ListItem> itemsToRemove = new List<ListItem>();
                foreach (ListItem listItem in lstsuppliers.Items)
                {
                    if (listItem.Selected == true)
                        itemsToRemove.Add(listItem);
                }
                foreach (ListItem listItem in itemsToRemove)
                {
                    lstsuppliers.Items.Remove(listItem);
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Generate Button Click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Main mainmaster = (Main)Page.Master;
                mainmaster.ShowHideFilters(btnReport, reportFiltersTable, reportViewerHolder);
                if (btnReport.Text == "Back")
                {
                    if (ddlreporttype.SelectedValue == "1")
                    {
                        crpartdetails.ReportName = "Inv_det_partno_All";

                        string selectionformula = string.Empty;
                        string selcon1 = "' and Right({Consignment.Inward_Number},2) <> 'si'";

                        string strbrch = "{consignment.branch_code}";
                        string strSupplierCode = " and {supplier_master.supplier_code}='" + ddsuppliercode.SelectedValue + "'";

                        if (ddsuppliercode.SelectedIndex != 0)
                        {
                            crpartdetails.RecordSelectionFormula = strbrch + "='" + strBranchCode + selcon1 + strSupplierCode;
                        }
                        else
                        {
                            crpartdetails.RecordSelectionFormula = strbrch + "='" + strBranchCode + selcon1;
                        }

                        crpartdetails.GenerateReport();
                    }
                    else if (ddlreporttype.SelectedValue == "2")
                    {
                        crpartdetails.ReportName = "Inv_det_partno_All";
                        string selectionformula = string.Empty;
                        string selcon2 = "' and Right({Consignment.Inward_Number},2) = 'si'";
                        string strbrch = "{consignment.branch_code}";
                        string strSupplierCode = "and {supplier_master.supplier_code}='" + ddsuppliercode.SelectedValue + "'";

                        if (ddsuppliercode.SelectedIndex != 0)
                        {
                            crpartdetails.RecordSelectionFormula = strbrch + "='" + strBranchCode + selcon2 + strSupplierCode;
                        }
                        else
                        {
                            crpartdetails.RecordSelectionFormula = strbrch + "='" + strBranchCode + selcon2;
                        }
                        crpartdetails.GenerateReport();
                    }
                    else if (ddlreporttype.SelectedValue == "3")
                    {
                        crpartdetails.ReportName = "Inv_det_partno_All";
                        string selectionformula = string.Empty;
                        string strbrch = "{consignment.branch_code}";
                        string strSupplierCode = "and {supplier_master.supplier_code}='" + ddsuppliercode.SelectedValue + "'";

                        if (ddsuppliercode.SelectedIndex != 0)
                        {
                            crpartdetails.RecordSelectionFormula = strbrch + "='" + strBranchCode + "'" + strSupplierCode;
                        }
                        else
                        {
                            crpartdetails.RecordSelectionFormula = strbrch + "='" + strBranchCode + "'";
                        }
                        crpartdetails.GenerateReport();
                    }

                    else if (ddlreporttype.SelectedValue == "0")
                    {
                        string selectionformula = string.Empty;
                        string strFromDate = string.Empty;
                        string strToDate = string.Empty;
                        string receiveddate = "{consignment.Original_Receipt_Date}";
                        string selparamsupplier = "{supplier_master.supplier_code}";
                        string selsupplier = ddsuppliercode.SelectedValue;
                        string selparambranch = "{consignment.branch_code}=";
                        string _suppcode = string.Empty;
                        if (lstsuppliers.Items.Count > 0)
                        {
                            foreach (var item in lstsuppliers.Items)
                            {
                                if (_suppcode != string.Empty)
                                    _suppcode = _suppcode + ",";
                                _suppcode = _suppcode + "'" + item + "'";
                            }
                        }
                        else
                            _suppcode = "'" + selsupplier + "'";

                        if (ddaging.SelectedIndex == 0)
                        {
                            if (ddsuppliercode.SelectedIndex == 0)
                            {
                                crpartdetails.RecordSelectionFormula = selparambranch + "'" + strBranchCode + "'";
                            }
                            else
                            {
                                crpartdetails.RecordSelectionFormula = selparambranch + "'" + strBranchCode + "'" + " and " + selparamsupplier + " in[" + _suppcode + "]";
                            }
                            crpartdetails.ReportName = "Inv_det_lessthan3";

                        }
                        else if (ddaging.SelectedIndex == 1)
                        {
                            if (ddsuppliercode.SelectedIndex == 0)
                            {
                                crpartdetails.RecordSelectionFormula = selparambranch + "'" + strBranchCode + "'";
                            }
                            else
                            {
                                crpartdetails.RecordSelectionFormula = selparambranch + "'" + strBranchCode + "'" + " and " + selparamsupplier + " in [" + _suppcode + "]";
                            }
                            crpartdetails.ReportName = "Inv_det_bet_3to6m";

                        }
                        else if (ddaging.SelectedIndex == 2)
                        {
                            if (ddsuppliercode.SelectedIndex == 0)
                            {
                                crpartdetails.RecordSelectionFormula = selparambranch + "'" + strBranchCode + "'";
                            }
                            else
                            {
                                crpartdetails.RecordSelectionFormula = selparambranch + "'" + strBranchCode + "'" + " and " + selparamsupplier + " in [" + _suppcode + "]";
                            }
                            crpartdetails.ReportName = "Inv_det_bet_6to1yr";

                        }
                        else if (ddaging.SelectedIndex == 3)
                        {
                            if (ddsuppliercode.SelectedIndex == 0)
                            {
                                crpartdetails.RecordSelectionFormula = selparambranch + "'" + strBranchCode + "'";
                            }
                            else
                            {
                                crpartdetails.RecordSelectionFormula = selparambranch + "'" + strBranchCode + "'" + " and " + selparamsupplier + " in [" + _suppcode + "]";
                            }
                            crpartdetails.ReportName = "Inv_det_bet_1to2yr";

                        }
                        else if (ddaging.SelectedIndex == 4)
                        {
                            if (ddsuppliercode.SelectedIndex == 0)
                            {
                                crpartdetails.RecordSelectionFormula = selparambranch + "'" + strBranchCode + "'";
                            }
                            else
                            {
                                crpartdetails.RecordSelectionFormula = selparambranch + "'" + strBranchCode + "'" + " and " + selparamsupplier + " in [" + _suppcode + "]";
                            }
                            crpartdetails.ReportName = "Inv_det_greaterthan_2yr";
                        }
                        else if (ddaging.SelectedIndex == 6)
                        {
                            if (!string.IsNullOrEmpty(txtFromDate.Text) && !string.IsNullOrEmpty(txtToDate.Text))
                            {
                                strFromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy,MM,dd");
                                strToDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy,MM,dd");
                                crpartdetails.ReportName = "Inv_det_givenperiod";

                                if (ddsuppliercode.SelectedIndex != 0 && strBranchCode == "CRP")
                                {
                                    crpartdetails.RecordSelectionFormula = receiveddate + ">=Date (" + strFromDate + ") and " + receiveddate + " <= Date (" + strToDate + ")and" + selparamsupplier + "='" + selsupplier + "'";

                                }
                                else if (ddsuppliercode.SelectedIndex == 0 && strBranchCode == "CRP")
                                {
                                    crpartdetails.RecordSelectionFormula = receiveddate + ">=Date (" + strFromDate + ") and " + receiveddate + " <= Date (" + strToDate + ")";

                                }
                                else if (ddsuppliercode.SelectedIndex != 0 && strBranchCode != "CRP")
                                {
                                    crpartdetails.RecordSelectionFormula = receiveddate + ">=Date (" + strFromDate + ") and " + receiveddate + " <= Date (" + strToDate + ") and " + selparamsupplier + "='" + selsupplier + "' and " + selparambranch + "'" + strBranchCode + "'";

                                }
                                else if (ddsuppliercode.SelectedIndex == 0 && strBranchCode != "CRP")
                                {
                                    crpartdetails.RecordSelectionFormula = receiveddate + ">=Date (" + strFromDate + ") and " + receiveddate + " <= Date (" + strToDate + ") and " + selparambranch + "'" + strBranchCode + "'";
                                }
                            }
                        }
                    }
                    crpartdetails.GenerateReport();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Supplier Code Selected index changed
        protected void ddsuppliercode_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "ddsuppliercode_SelectedIndexChanged()", "Part Details Supplier code Index Changed Method");
            try
            {
                if (ddsuppliercode.SelectedIndex != 0 && ddaging.SelectedIndex != 6 && ddaging.SelectedIndex != 5)
                {

                    divDate.Visible = false;
                }
                else if (ddaging.SelectedIndex == 5 || ddaging.SelectedIndex == 6)
                {
                    if (ddaging.SelectedIndex == 6)
                    {
                        divDate.Visible = true;
                    }
                    else
                    {
                        divDate.Visible = false;
                    }


                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Aging Selected Index Changed Method
        protected void ddaging_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "ddaging_SelectedIndexChanged()", "Part Details Aging Dropdown Changed event");
            try
            {
                if (ddaging.SelectedIndex == 5)
                {
                    divDate.Visible = false;
                    divList.Visible = false;
                    div1.Visible = false;
                    ddlreporttype.DataSource = oCommon.GetDropDownListValues("RptType-List3");
                    ddlreporttype.DataTextField = "DisplayText";
                    ddlreporttype.DataValueField = "DisplayValue";
                    ddlreporttype.DataBind();
                }
                else if (ddaging.SelectedIndex == 6)
                {
                    divDate.Visible = true;
                    divList.Visible = false;
                    div1.Visible = false;
                    ddlreporttype.DataSource = oCommon.GetDropDownListValues("RptType-List2");
                    ddlreporttype.DataTextField = "DisplayText";
                    ddlreporttype.DataValueField = "DisplayValue";
                    ddlreporttype.DataBind();
                }
                else
                {
                    divDate.Visible = false;
                    divList.Visible = true;
                    div1.Visible = true;
                    lstsuppliers.Items.Clear();
                    ddlreporttype.DataSource = oCommon.GetDropDownListValues("RptType-List2");
                    ddlreporttype.DataTextField = "DisplayText";
                    ddlreporttype.DataValueField = "DisplayValue";
                    ddlreporttype.DataBind();
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