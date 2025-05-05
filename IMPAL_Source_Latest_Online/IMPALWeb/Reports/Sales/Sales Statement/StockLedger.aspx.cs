#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Masters.Sales;
using IMPALLibrary.Masters;
#endregion

namespace IMPALWeb.Reports.Sales.SalesStatement
{
    public partial class StockLedger : System.Web.UI.Page
    {
        #region Declaration
        string strBranchCode = string.Empty;
        #endregion

        #region Page_Init
        /// <summary>
        /// Page Init event 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        #region LoadMonthYearDDL
        /// <summary>
        /// Method to  Load monthYear ddl
        /// </summary>
        private void LoadMonthYearDDL()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                LineWiseSales oLineWiseSales = new LineWiseSales();
                ddlMonthYear.DataSource = oLineWiseSales.GetMonthYear(null);
                ddlMonthYear.DataTextField = "MonthYear";
                ddlMonthYear.DataValueField = "MonthYear";
                ddlMonthYear.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Page Load
        /// <summary>
        /// Page load event 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = (string)Session["BranchCode"];

                if (!IsPostBack)
                {
                    if (crStockLedger != null)
                    {
                        crStockLedger.Dispose();
                        crStockLedger = null;
                    }

                    LoadMonthYearDDL();
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
            if (crStockLedger != null)
            {
                crStockLedger.Dispose();
                crStockLedger = null;
            }
        }
        protected void crStockLedger_Unload(object sender, EventArgs e)
        {
            if (crStockLedger != null)
            {
                crStockLedger.Dispose();
                crStockLedger = null;
            }
        }

        #region btnReport_Click
        /// <summary>
        /// Report Button Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// Method to call crystal Report 
        /// </summary>
        private void CallCrystalReport()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!string.IsNullOrEmpty(strBranchCode))
                {
                    #region Declaration
                    string strSelectionFormula = null;

                    string strMonthYearQuery = "{%PrevMnth}";
                    string strSupplierQuery = "mid({stk_ledger_tran.Item_Code},1,3)";
                    string strItemQuery = "{stk_ledger_tran.Item_Code}";
                    string strBranchQuery = "{stk_ledger_tran.branch_code}";
                    #endregion

                    Stock oStock = new Stock();
                    string supplierCode = string.Empty;
                    supplierCode = ddlSupplierCode.SelectedValue;
                    if (supplierCode == "0")
                    {
                        supplierCode = "";
                    }
                    oStock.StockLedgerTransaction(ddlMonthYear.SelectedValue, strBranchCode, supplierCode, txtItemCode.Text);

                    #region Selction Formula Formation
                    if (ddlMonthYear.SelectedIndex > 0)
                    {
                        if (ddlSupplierCode.SelectedIndex > 0)
                        {
                            if (txtItemCode.Text.Equals(string.Empty))
                                strSelectionFormula = strMonthYearQuery + " = '" + ddlMonthYear.SelectedValue + "' and "
                                + strBranchQuery + " = '" + strBranchCode + "' and "
                                + strSupplierQuery + " = '" + ddlSupplierCode.SelectedValue + "'";
                            else
                                strSelectionFormula = strMonthYearQuery + " = '" + ddlMonthYear.SelectedValue + "' and "
                                + strBranchQuery + " = '" + strBranchCode + "' and "
                                + strSupplierQuery + " = '" + ddlSupplierCode.SelectedValue + "' and "
                                + strItemQuery + " = '" + txtItemCode.Text + "'";
                        }
                        else
                            strSelectionFormula = strMonthYearQuery + " = '" + ddlMonthYear.SelectedValue + "' and "
                                + strBranchQuery + " = '" + strBranchCode + "'";
                    }
                    #endregion

                    crStockLedger.RecordSelectionFormula = strSelectionFormula;
                    crStockLedger.GenerateReport();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region ItemPopUp_ImageClicked
        /// <summary>
        /// ItemPopup Image clicked event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ItemPopUp_ImageClicked(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                txtSupplierPartNo.Text = Session["SupplierPartNumber"].ToString();
                txtItemCode.Text = Session["ItemCode"].ToString();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        protected void ddlSupplierCode_IndexChanged(object sender, EventArgs e)
        {
            ItemPopUp.SupplierLine = ddlSupplierCode.SelectedValue;
            ItemPopUp.SupplierDesc = ddlSupplierCode.SelectedItem.Text;
            txtSupplierPartNo.Text = "";
            txtItemCode.Text = "";
        }
        #endregion
    }
}