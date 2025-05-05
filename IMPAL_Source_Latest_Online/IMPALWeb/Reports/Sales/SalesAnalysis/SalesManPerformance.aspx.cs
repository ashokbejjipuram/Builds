#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Common;
using IMPALLibrary.Masters.Sales;
#endregion

namespace IMPALWeb.Reports.Sales.SalesAnalysis
{
    public partial class SalesManPerformance : System.Web.UI.Page
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
                    if (crPerformance != null)
                    {
                        crPerformance.Dispose();
                        crPerformance = null;
                    }

                    txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtToDate.Text = txtFromDate.Text;

                    PopulateSalesMenDDL();
                    PopulateReportType();
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
            if (crPerformance != null)
            {
                crPerformance.Dispose();
                crPerformance = null;
            }
        }
        protected void crPerformance_Unload(object sender, EventArgs e)
        {
            if (crPerformance != null)
            {
                crPerformance.Dispose();
                crPerformance = null;
            }
        }

        #region PopulateReportType
        /// <summary>
        /// Populates the dropdown with Report Types from XML
        /// </summary>
        protected void PopulateReportType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ImpalLibrary oLib = new ImpalLibrary();
                List<DropDownListValue> lstValues = new List<DropDownListValue>();
                lstValues = oLib.GetDropDownListValues("ReportType-SalesMen");
                ddlReportType.DataSource = lstValues;
                ddlReportType.DataBind();
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
                    if (ddlReportType.SelectedValue.Equals("Report"))
                        crPerformance.ReportName = "SalesManPerformance";
                    else
                        crPerformance.ReportName = "SalesManPerformanceSummary";

                    CallCrystalReport();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region PopulateSalesMenDDL
        /// <summary>
        /// Populates the dropdown with Sales Men Names from DB
        /// </summary>
        protected void PopulateSalesMenDDL()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                List<SalesMan> lstSalesMen = null;
                SalesMen oSalesMen = new SalesMen();
                lstSalesMen = oSalesMen.GetSalesMenNames(strBranchCode);
                ddlSalesMen.DataSource = lstSalesMen;
                ddlSalesMen.DataBind();
                ddlSalesMen.Items.Insert(0, string.Empty);
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
                    string strSelectionFormula = null;
                    string strFromDate = null;
                    string strToDate = null;
                    //string strInwardDate = "{Grn_Discrepency.Inward_Date}";
                    if (!string.IsNullOrEmpty(txtFromDate.Text) && !string.IsNullOrEmpty(txtToDate.Text))
                    {
                        strFromDate = Convert.ToDateTime(hidFromDate.Value).ToString("yyyy,MM,dd");
                        strToDate = Convert.ToDateTime(hidToDate.Value).ToString("yyyy,MM,dd");

                        SalesMen oSalesMen = new SalesMen();
                        if (ddlSalesMen.SelectedIndex > 0)
                            oSalesMen.SendFilterDetailsToDB(hidFromDate.Value, hidToDate.Value, strBranchCode, ddlSalesMen.SelectedValue);
                        else
                            oSalesMen.SendFilterDetailsToDB(hidFromDate.Value, hidToDate.Value, strBranchCode, "");

                        strSelectionFormula = "{Sales_Man_Performance.Branch_Code} = '" + strBranchCode + "'";

                        crPerformance.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
                        crPerformance.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
                        crPerformance.RecordSelectionFormula = strSelectionFormula;
                        crPerformance.GenerateReport();
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