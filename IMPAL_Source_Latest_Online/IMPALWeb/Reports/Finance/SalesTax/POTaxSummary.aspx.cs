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
    public partial class POTaxSummary : System.Web.UI.Page
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
                    if (rptCrystal != null)
                    {
                        rptCrystal.Dispose();
                        rptCrystal = null;
                    }
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
            if (rptCrystal != null)
            {
                rptCrystal.Dispose();
                rptCrystal = null;
            }
        }
        protected void rptCrystal_Unload(object sender, EventArgs e)
        {
            if (rptCrystal != null)
            {
                rptCrystal.Dispose();
                rptCrystal = null;
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
                    #region Declaration
                    string strSelectionFormula = null;
                    string strBranchQuery = "mid({purchase_order_header.po_number},15,3)";
                    #endregion

                    #region Selction Formula Formation
                    if (!strBranchCode.Equals("CRP"))
                        strSelectionFormula = strBranchQuery + " = '" + strBranchCode + "'";
                    #endregion

                    rptCrystal.RecordSelectionFormula = strSelectionFormula;
                    rptCrystal.GenerateReportHO();
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
