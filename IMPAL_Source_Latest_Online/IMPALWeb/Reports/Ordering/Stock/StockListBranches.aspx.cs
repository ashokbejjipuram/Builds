#region Namespace Declaration
using System;
using System.Collections.Generic;
using System.Data.Common;
using IMPALLibrary;
using IMPALLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
#endregion

namespace IMPALWeb.Reports.Ordering.Stock
{
    public partial class Stock_List_Branches : System.Web.UI.Page
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
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Page load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = Session["BranchCode"].ToString();
                if (!IsPostBack)
                {
                    if (crbranches != null)
                    {
                        crbranches.Dispose();
                        crbranches = null;
                    }

                    fnPopulateAging();
                    cbopartno.Enabled = false;
                    ddbranchcode.SelectedValue = strBranchCode;
                    ddbranchcode.Enabled = false;
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
            if (crbranches != null)
            {
                crbranches.Dispose();
                crbranches = null;
            }
        }
        protected void crbranches_Unload(object sender, EventArgs e)
        {
            if (crbranches != null)
            {
                crbranches.Dispose();
                crbranches = null;
            }
        }

        #region Aging Dropdown Populate Method
        protected void fnPopulateAging()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                ddlAging.DataSource = oCommon.GetDropDownListValues("Time");
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
                    string strStartDate = string.Empty;
                    string strEndDate = string.Empty;
                    string selectionformula = string.Empty;
                    string strsel1 = "{branch_master.branch_code}=";
                    string strsel2 = "Mid({Item_Master.Item_Code},1 ,3 )=";
                    string strsel3 = "{Item_Master.Supplier_Part_Number}=";                    

                    if (ddlAging.SelectedValue == " ")
                    {
                        strStartDate = "1900-01-01";
                        strEndDate = "getdate()";
                    }
                    else if (ddlAging.SelectedValue == "0")
                    {
                        strStartDate = "DATEADD(ww,-4,getdate())";
                        strEndDate = "getdate()";
                    }
                    else if (ddlAging.SelectedValue == "1")
                    {
                        strStartDate = "DATEADD(ww,-6,getdate())";
                        strEndDate = "DATEADD(ww,-4,getdate())";
                    }
                    else if (ddlAging.SelectedValue == "2")
                    {
                        strStartDate = "DATEADD(ww,-8,getdate())";
                        strEndDate = "DATEADD(ww,-6,getdate())";
                    }
                    else if (ddlAging.SelectedValue == "3")
                    {
                        strStartDate = "DATEADD(ww,-12,getdate())";
                        strEndDate = "DATEADD(ww,-8,getdate())";
                    }
                    else if (ddlAging.SelectedValue == "4")
                    {
                        strStartDate = "DATEADD(ww,-24,getdate())";
                        strEndDate = "DATEADD(ww,-12,getdate())";
                    }
                    else if (ddlAging.SelectedValue == "5")
                    {
                        strStartDate = "1900-01-01";
                        strEndDate = "DATEADD(ww,-24,getdate())";
                    }
                    else if (ddlAging.SelectedValue == "6")
                    {
                        strStartDate = "DATEADD(mm,-12,getdate())";
                        strEndDate = "DATEADD(mm,-6,getdate())";
                    }
                    else if (ddlAging.SelectedValue == "7")
                    {
                        strStartDate = "DATEADD(mm,-24,getdate())";
                        strEndDate = "DATEADD(mm,-12,getdate())";
                    }
                    else if (ddlAging.SelectedValue == "8")
                    {
                        strStartDate = "1900-01-01";
                        strEndDate = "DATEADD(mm,-24,getdate())";
                    }

                    Database ImpalDB = DataAccess.GetDatabase();
                    DbCommand cmd = ImpalDB.GetSqlStringCommand("Delete From Temp_Aging where Branch_Code='" + strBranchCode + "'; Insert into Temp_Aging values ('" + strBranchCode + "'," + strStartDate + "," + strEndDate + ")");
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);

                    if (strBranchCode != "CRP")
                    {
                        if (ddlinecode.SelectedValue.ToString() != "0" && (cbopartno.SelectedIndex == -1 || cbopartno.SelectedIndex == 0))
                        {
                            selectionformula = strsel1 + "'" + ddbranchcode.SelectedValue + "' and " + strsel2 + "'" + ddlinecode.SelectedValue + "'";
                        }
                        else if (ddlinecode.SelectedValue.ToString() != "0" && cbopartno.SelectedIndex != -1)
                        {
                            selectionformula = strsel1 + "'" + ddbranchcode.SelectedValue + "' and " + strsel2 + "'" + ddlinecode.SelectedValue + "' and " + strsel3 + "'" + cbopartno.SelectedValue + "'";
                        }
                        else if (ddlinecode.SelectedValue.ToString() == "0" && (cbopartno.SelectedIndex == -1 || cbopartno.SelectedIndex == 0))
                        {
                            selectionformula = strsel1 + "'" + ddbranchcode.SelectedValue + "'";
                        }
                    }
                    else
                    {
                        if (ddlinecode.SelectedValue.ToString() != "0" && (cbopartno.SelectedIndex == -1 || cbopartno.SelectedIndex == 0))
                        {
                            selectionformula = strsel2 + "'" + ddlinecode.SelectedValue + "'";
                        }
                        if (ddlinecode.SelectedValue.ToString() != "0" && cbopartno.SelectedIndex != -1)
                        {
                            selectionformula = strsel2 + "'" + ddlinecode.SelectedValue + "' and " + strsel3 + "'" + cbopartno.SelectedValue + "'";
                        }
                    }
                    crbranches.RecordSelectionFormula = selectionformula;
                    crbranches.ReportName = "StockListnew_aging";
                    crbranches.GenerateReport();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region LineCode Selected Index Changed
        protected void ddlinecode_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (ddlinecode.SelectedValue != "0")
                {
                    cbopartno.Enabled = true;
                    Suppliers supp = new Suppliers();
                    cbopartno.DataSource = supp.ConsignmentBasedSupplier(ddlinecode.SelectedValue, strBranchCode);
                    cbopartno.DataValueField = "SupplierCode";
                    cbopartno.DataTextField = "SupplierCode";
                    cbopartno.DataBind();
                    cbopartno.SelectedIndex = 0;
                }
                else
                {
                    cbopartno.Enabled = false;
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