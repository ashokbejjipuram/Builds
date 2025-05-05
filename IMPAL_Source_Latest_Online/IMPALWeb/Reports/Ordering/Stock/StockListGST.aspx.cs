#region Namespace Declaration
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using IMPALLibrary;
using IMPALLibrary.Common;
using System.Web.UI.WebControls;
#endregion

namespace IMPALWeb.Reports.Ordering.Stock
{
    public partial class StockListGST : System.Web.UI.Page
    {
        string strBranchCode = string.Empty;

        #region Page init
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

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = (string)Session["BranchCode"];

                if (!IsPostBack)
                {
                    if (crstocklistGST != null)
                    {
                        crstocklistGST.Dispose();
                        crstocklistGST = null;
                    }

                    fnPopulateList();
                    fnPopulateBranches();
                    fnPopulateReportType();

                    if (strBranchCode != "CRP")
                    {
                        ddlBranchCode.SelectedValue = strBranchCode.ToString();
                        ddlBranchCode.Enabled = false;
                    }
                    else
                    {
                        ddlBranchCode.Enabled = true;
                    }

                    fnPopulateSuppliers(ddlBranchCode.SelectedValue);
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
            if (crstocklistGST != null)
            {
                crstocklistGST.Dispose();
                crstocklistGST = null;
            }
        }
        protected void crstocklistGST_Unload(object sender, EventArgs e)
        {
            if (crstocklistGST != null)
            {
                crstocklistGST.Dispose();
                crstocklistGST = null;
            }
        }

        #region Populate List
        protected void fnPopulateList()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                ddlList.DataSource = oCommon.GetDropDownListValues("ListGST");
                ddlList.DataTextField = "DisplayText";
                ddlList.DataValueField = "DisplayValue";
                ddlList.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Populate Report Type and List Dropdown
        protected void fnPopulateReportType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();

                if (ddlList.SelectedValue.Equals("0"))
                {
                    ddlReportType.DataSource = oCommon.GetDropDownListValues("RptType-ListGSTOthers");
                    ddlReportType.DataTextField = "DisplayText";
                    ddlReportType.DataValueField = "DisplayValue";
                    ddlReportType.DataBind();
                }
                else
                {
                    ddlReportType.DataSource = oCommon.GetDropDownListValues("RptType-ListGST");
                    ddlReportType.DataTextField = "DisplayText";
                    ddlReportType.DataValueField = "DisplayValue";
                    ddlReportType.DataBind();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Populate Suppliers Dropdown
        protected void fnPopulateSuppliers(string branchcode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Suppliers objSup = new Suppliers();

                if (ddlList.SelectedValue.Equals("0"))
                {
                    ddlSupplierCode.DataSource = objSup.GetAllLinewiseSuppliersGST(branchcode, "O");
                    ddlSupplierCode.DataValueField = "SupplierCode";
                    ddlSupplierCode.DataTextField = "SupplierName";
                    ddlSupplierCode.DataBind();
                }
                else
                {
                    ddlSupplierCode.DataSource = objSup.GetAllLinewiseSuppliersGST(branchcode, "L");
                    ddlSupplierCode.DataValueField = "SupplierCode";
                    ddlSupplierCode.DataTextField = "SupplierName";
                    ddlSupplierCode.DataBind();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Populate Branches Dropdown
        protected void fnPopulateBranches()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType; 
            try
            {
                Branches objBranch = new Branches();
                ddlBranchCode.DataSource = objBranch.GetAllBranch();
                ddlBranchCode.DataValueField = "BranchCode";
                ddlBranchCode.DataTextField = "BranchName";
                ddlBranchCode.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region List Dropdown Index changed Method
        protected void ddlList_OnSelectIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                ddlSupplierCode.Enabled = true;
                ddlSupplierCode.SelectedIndex = 0;

                fnPopulateSuppliers(ddlBranchCode.SelectedValue);
                fnPopulateReportType();
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
                    string sel4 = "{STOCK_LIST_GST.Supplier_Code}";
                    string sel5 = "{STOCK_LIST_GST.Original_Receipt_Date}";
                    string selbranch = "{branch_master.branch_code}=";
                    string selectionformula = string.Empty;

                    int intsuppcode = ddlSupplierCode.SelectedIndex;
                    string strsuppcode = ddlSupplierCode.SelectedValue.ToString();
                    int intbranchcode = ddlBranchCode.SelectedIndex;
                    string strBranchCode = ddlBranchCode.SelectedValue.ToString();

                    Database ImpalDB = DataAccess.GetDatabase();
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("stocklistGST");

                    if (!strsuppcode.Equals("0"))
                    {
                        ImpalDB.AddInParameter(cmd, "@branch", DbType.String, strBranchCode);
                        ImpalDB.AddInParameter(cmd, "@Indicator", DbType.String, ddlList.SelectedValue);
                        ImpalDB.AddInParameter(cmd, "@Supplier", DbType.String, strsuppcode);
                    }
                    else
                    {
                        ImpalDB.AddInParameter(cmd, "@branch", DbType.String, strBranchCode);
                        ImpalDB.AddInParameter(cmd, "@Indicator", DbType.String, ddlList.SelectedValue);
                    }

                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);
                    crstocklistGST.ReportName = "StockListnewGST";

                    #region Selection Formula

                    if (strBranchCode.Equals("CRP"))
                    {
                        if (intsuppcode != 0 && intbranchcode != 0)
                        {
                            if (ddlList.SelectedValue.Equals("0"))
                            {
                                selectionformula = selbranch + "'" + strBranchCode + "' and " + sel4 + "='" + strsuppcode + "'";
                            }
                        }
                        else if (intsuppcode == 0 && intbranchcode != 0)
                        {
                            selectionformula = selbranch + "'" + strBranchCode + "'";

                        }
                        else if (intsuppcode != 0 && intbranchcode == 0)
                        {
                            if (ddlList.SelectedValue.Equals("0"))
                            {
                                selectionformula = sel4 + "='" + strsuppcode + "'";
                            }
                        }
                    }
                    else
                    {
                        if (intsuppcode != 0)
                        {
                            selectionformula = selbranch + "'" + strBranchCode + "' and " + sel4 + "='" + strsuppcode + "'";
                        }
                        else
                        {
                            selectionformula = selbranch + "'" + strBranchCode + "'";
                        }
                    }

                    if (ddlList.SelectedIndex == 0)
                    {
                        if (ddlReportType.SelectedIndex == 0)
                        {
                            selectionformula = selectionformula + " and " + sel5 + "< Date (2016,07,01)";
                        }
                        else if (ddlReportType.SelectedIndex == 1)
                        {
                            selectionformula = selectionformula + " and " + sel5 + ">= Date (2016,07,01) and " + sel5 + "<= Date (2017,06,30) ";
                        }
                        else
                        {
                            selectionformula = selectionformula + " and " + sel5 + ">= Date (2017,07,01)";
                        }
                    }
                    else
                    {
                        if (ddlReportType.SelectedIndex == 0)
                        {
                            selectionformula = selectionformula + " and " + sel5 + "< Date (2016,07,01)";
                        }
                        else if (ddlReportType.SelectedIndex == 1)
                        {
                            selectionformula = selectionformula + " and " + sel5 + ">= Date (2016,07,01) and " + sel5 + "<= Date (2017,05,25) ";
                        }
                        else if (ddlReportType.SelectedIndex == 2)
                        {
                            selectionformula = selectionformula + " and " + sel5 + "> Date (2017,05,25) and " + sel5 + "<= Date (2017,06,30) ";
                        }
                        else
                        {
                            selectionformula = selectionformula + " and " + sel5 + ">= Date (2017,07,01)";
                        }
                    }

                    #endregion

                    crstocklistGST.RecordSelectionFormula = selectionformula;
                    crstocklistGST.CrystalFormulaFields.Add("Desc", "\"" + ddlReportType.SelectedItem.Text + "\"");
                    crstocklistGST.GenerateReport();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region BranchCode Index Changed Method
        protected void ddlBranchCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            fnPopulateSuppliers(ddlBranchCode.SelectedValue);
        }
        #endregion
    }
}
