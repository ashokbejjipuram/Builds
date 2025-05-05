#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
#endregion

namespace IMPALWeb.Reports.Inventory
{
    public partial class AreaWiseStock : System.Web.UI.Page
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
                    if (crAreaWiseStock != null)
                    {
                        crAreaWiseStock.Dispose();
                        crAreaWiseStock = null;
                    }

                    if (strBranchCode.Equals("CRP"))
                    {
                        Zones oZone = new Zones();
                        ddlZone.DataSource = oZone.GetAllZones();
                        ddlZone.DataBind();
                        ddlZone.Items.Insert(0, string.Empty);
                        ddlZone.Enabled = true;
                    }
                    else
                    {
                        Branches oBranches = new Branches();
                        Branch oBranchDetails = oBranches.GetBranchDetails(strBranchCode);
                        if (oBranchDetails != null)
                        {
                            ddlZone.Items.Add(oBranchDetails.Zone);
                            ddlState.Items.Add(oBranchDetails.State);
                            ddlBranch.Items.Add(oBranchDetails.BranchName);
                        }
                    }
                    Suppliers oSuppliers = new Suppliers();
                    ddlSupplier.DataSource = oSuppliers.GetAllSuppliers();
                    ddlSupplier.DataBind();
                    ddlSupplier.Items.Insert(1, "ALL");
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
            if (crAreaWiseStock != null)
            {
                crAreaWiseStock.Dispose();
                crAreaWiseStock = null;
            }
        }
        protected void crAreaWiseStock_Unload(object sender, EventArgs e)
        {
            if (crAreaWiseStock != null)
            {
                crAreaWiseStock.Dispose();
                crAreaWiseStock = null;
            }
        }

        #region ddlZone_OnSelectedIndexChanged
        protected void ddlZone_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddlState.Items.Clear();
                if (ddlZone.SelectedIndex > 0)
                {
                    ddlState.Enabled = true;
                    StateMasters oStateMaster = new StateMasters();
                    ddlState.DataSource = oStateMaster.GetZoneBasedStates(Convert.ToInt16(ddlZone.SelectedValue));
                    ddlState.DataBind();
                    ddlState.Items.Insert(0, string.Empty);
                }
                else
                    ddlState.Enabled = false;
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region ddlState_OnSelectedIndexChanged
        protected void ddlState_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddlBranch.Items.Clear();
                if (ddlState.SelectedIndex > 0)
                {
                    ddlBranch.Enabled = true;
                    Branches oBranch = new Branches();
                    ddlBranch.DataSource = oBranch.GetStateBasedBranch(ddlState.SelectedValue);
                    ddlBranch.DataBind();
                    ddlBranch.Items.Insert(0, string.Empty);
                }
                else
                    ddlBranch.Enabled = false;
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region ddlSupplier_OnSelectedIndexChanged
        protected void ddlSupplier_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddlItemCode.Items.Clear();
                if (ddlSupplier.SelectedIndex > 0)
                {
                    ddlItemCode.Enabled = true;
                    IMPALLibrary.Masters.Items oItems = new IMPALLibrary.Masters.Items();
                    ddlItemCode.DataSource = oItems.GetItems(ddlSupplier.SelectedValue);
                    ddlItemCode.DataBind();
                    ddlItemCode.Items.Insert(0, string.Empty);
                    ddlItemCode.Items.Insert(1, "ALL");
                }
                else
                    ddlItemCode.Enabled = false;
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

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
                        if (ddlSupplier.SelectedIndex > 1)
                        {
                            string strSupplierQuery = "mid({supplier_line_master.supplier_line_code},1,3) = '";
                            if (ddlItemCode.SelectedIndex <= 1)
                                strSelectionFormula = strSupplierQuery + ddlSupplier.SelectedValue + "'";
                            else
                                strSelectionFormula = strSupplierQuery + ddlSupplier.SelectedValue + "' and {item_master.item_code} = '"
                                                    + ddlItemCode.SelectedValue + "'";
                        }
                        crAreaWiseStock.RecordSelectionFormula = strSelectionFormula;
                        crAreaWiseStock.GenerateReport();
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