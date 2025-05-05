#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Common;
using IMPALLibrary.Masters;
#endregion

namespace IMPALWeb.Reports.Inventory
{
    public partial class StockTransfer : System.Web.UI.Page
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
                    if (crStockTransfer != null)
                    {
                        crStockTransfer.Dispose();
                        crStockTransfer = null;
                    }

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
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crStockTransfer != null)
            {
                crStockTransfer.Dispose();
                crStockTransfer = null;
            }
        }
        protected void crStockTransfer_Unload(object sender, EventArgs e)
        {
            if (crStockTransfer != null)
            {
                crStockTransfer.Dispose();
                crStockTransfer = null;
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
                    if (!string.IsNullOrEmpty(strBranchCode))
                    {
                        string strSelectionFormula = null;
                        string strFromDate = null;
                        string strToDate = null;
                        string strStockDate = "{STDN_Header.STDN_Date}";
                        if (!string.IsNullOrEmpty(txtFromDate.Text) && !string.IsNullOrEmpty(txtToDate.Text))
                        {
                            ImpalLibrary oCommon = new ImpalLibrary();
                            strFromDate = Convert.ToDateTime(hidFromDate.Value).ToString("yyyy,MM,dd");
                            strToDate = Convert.ToDateTime(hidToDate.Value).ToString("yyyy,MM,dd");
                            string strDateCompare = strStockDate + " >= Date (" + strFromDate + ") and "
                                                  + strStockDate + " <= Date (" + strToDate + ")";

                            if (strBranchCode.Equals("CRP"))
                            {
                                strSelectionFormula = strDateCompare;
                            }
                            else
                            {
                                strSelectionFormula = strDateCompare + " and {STDN_Header.Branch_Code} = '" + strBranchCode + "'";
                            }

                            crStockTransfer.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
                            crStockTransfer.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
                            crStockTransfer.RecordSelectionFormula = strSelectionFormula;
                            crStockTransfer.GenerateReport();
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