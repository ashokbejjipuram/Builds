#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Common;
#endregion

namespace IMPALWeb.Reports.Finance.SalesTax
{
    public partial class LocalStatePurchaseTax : System.Web.UI.Page
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
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = (string)Session["BranchCode"];

                if (!IsPostBack)
                {
                    if (Crpotaxlocalstate != null)
                    {
                        Crpotaxlocalstate.Dispose();
                        Crpotaxlocalstate = null;
                    }

                    LoadReportType();

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
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (Crpotaxlocalstate != null)
            {
                Crpotaxlocalstate.Dispose();
                Crpotaxlocalstate = null;
            }
        }
        protected void Crpotaxlocalstate_Unload(object sender, EventArgs e)
        {
            if (Crpotaxlocalstate != null)
            {
                Crpotaxlocalstate.Dispose();
                Crpotaxlocalstate = null;
            }
        }

        protected void LoadReportType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                int branchcnt = oCommon.GetDelhiBranches(Session["BranchCode"].ToString());
                if (branchcnt <= 0)
                    btnReport.Enabled = false;
                else
                {
                    ddlReportType.DataSource = oCommon.GetDropDownListValues("ReportType-LocalPOTax-Delhi");
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
                    CallCrystalReport();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region CallCrystalReport
        private void CallCrystalReport()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!string.IsNullOrEmpty(strBranchCode))
                {
                    if (ddlReportType.SelectedValue == "PurchaseCst")
                    {
                        Crpotaxlocalstate.ReportName = "PurchaseTaxDvatOld";

                        IMPALLibrary.Masters.Finance oFinance = new IMPALLibrary.Masters.Finance();
                        oFinance.LocalStatePOTax(txtFromDate.Text, txtToDate.Text, strBranchCode, "O");

                    }
                    else if (ddlReportType.SelectedValue == "PurchaseVat")
                    {
                        Crpotaxlocalstate.ReportName = "PurchaseTaxDvatOld";

                        IMPALLibrary.Masters.Finance oFinance = new IMPALLibrary.Masters.Finance();
                        oFinance.LocalStatePOTax(txtFromDate.Text, txtToDate.Text, strBranchCode, "L");
                    }
                    else if (ddlReportType.SelectedValue == "STDNIO")
                    {
                        Crpotaxlocalstate.ReportName = "PurchaseTaxDvatSTDN";

                        IMPALLibrary.Masters.Finance oFinance = new IMPALLibrary.Masters.Finance();
                        oFinance.LocalStateInward(txtFromDate.Text, txtToDate.Text, strBranchCode, "O");
                    }
                    else if (ddlReportType.SelectedValue == "STDNIL")
                    {
                        Crpotaxlocalstate.ReportName = "PurchaseTaxDvatSTDN";

                        IMPALLibrary.Masters.Finance oFinance = new IMPALLibrary.Masters.Finance();
                        oFinance.LocalStateInward(txtFromDate.Text, txtToDate.Text, strBranchCode, "L");
                    }

                    Crpotaxlocalstate.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
                    Crpotaxlocalstate.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
                    Crpotaxlocalstate.RecordSelectionFormula = null;
                    Crpotaxlocalstate.GenerateReport();
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