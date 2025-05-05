#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Common;
using CrystalDecisions.Web;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Globalization;
#endregion

namespace IMPALWeb.Reports.Inventory
{
    public partial class SupplyShortageRegister : System.Web.UI.Page
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
                    if (crSupplyShortageRegister != null)
                    {
                        crSupplyShortageRegister.Dispose();
                        crSupplyShortageRegister = null;
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
            if (crSupplyShortageRegister != null)
            {
                crSupplyShortageRegister.Dispose();
                crSupplyShortageRegister = null;
            }
        }
        protected void crSupplyShortageRegister_Unload(object sender, EventArgs e)
        {
            if (crSupplyShortageRegister != null)
            {
                crSupplyShortageRegister.Dispose();
                crSupplyShortageRegister = null;
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
                        string strInwardDate = "{Grn_Discrepency.Inward_Date}";
                        if (!string.IsNullOrEmpty(txtFromDate.Text) && !string.IsNullOrEmpty(txtToDate.Text))
                        {
                            strFromDate = Convert.ToDateTime(hidFromDate.Value).ToString("yyyy,MM,dd");
                            strToDate = Convert.ToDateTime(hidToDate.Value).ToString("yyyy,MM,dd");
                            
                            if (strBranchCode.Equals("CRP"))
                            {
                                strSelectionFormula = strInwardDate + " >= Date (" + strFromDate + ") and "
                                                    + strInwardDate + " <= Date (" + strToDate + ")";
                            }
                            else
                            {
                                strSelectionFormula = strInwardDate + " >= Date (" + strFromDate + ") and "
                                                    + strInwardDate + " <= Date (" + strToDate + ") and "
                                                    + "right({Grn_Discrepency.inward_number},3) = '" + strBranchCode + "'";
                            }

                            crSupplyShortageRegister.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
                            crSupplyShortageRegister.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
                            crSupplyShortageRegister.RecordSelectionFormula = strSelectionFormula;
                            crSupplyShortageRegister.GenerateReport();
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
