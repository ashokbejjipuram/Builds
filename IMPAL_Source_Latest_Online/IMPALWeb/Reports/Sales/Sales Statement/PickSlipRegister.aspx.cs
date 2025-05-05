#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Common; 
#endregion

namespace IMPALWeb.Reports.Sales.SalesStatement
{
    public partial class PickSlipRegister : System.Web.UI.Page
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
                    if (crPickSlipRegister != null)
                    {
                        crPickSlipRegister.Dispose();
                        crPickSlipRegister = null;
                    }

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
            if (crPickSlipRegister != null)
            {
                crPickSlipRegister.Dispose();
                crPickSlipRegister = null;
            }
        }
        protected void crPickSlipRegister_Unload(object sender, EventArgs e)
        {
            if (crPickSlipRegister != null)
            {
                crPickSlipRegister.Dispose();
                crPickSlipRegister = null;
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
                    string strSelectionFormula = null;
                    string strDateQuery = "{pick_slip_header.pick_slip_date}";
                    string strBranchQuery = "{branch_master.branch_code}";

                    if (!string.IsNullOrEmpty(txtFromDate.Text) && !string.IsNullOrEmpty(txtToDate.Text))
                    {
                        string strFromDate = Convert.ToDateTime(hidFromDate.Value).ToString("yyyy,MM,dd");
                        string strToDate = Convert.ToDateTime(hidToDate.Value).ToString("yyyy,MM,dd");
                        string strDateCompare = strDateQuery + " >= Date (" + strFromDate + ") and "
                                               + strDateQuery + " <= Date (" + strToDate + ")";
                        if (strBranchCode.Equals("CRP"))
                            strSelectionFormula = strDateCompare;
                        else
                            strSelectionFormula = strDateCompare + " and " + strBranchQuery + " = '" + strBranchCode + "'";
                    }

                    crPickSlipRegister.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
                    crPickSlipRegister.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
                    crPickSlipRegister.RecordSelectionFormula = strSelectionFormula;
                    crPickSlipRegister.GenerateReport();
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
