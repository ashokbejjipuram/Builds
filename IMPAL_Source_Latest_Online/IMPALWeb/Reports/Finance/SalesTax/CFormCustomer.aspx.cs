#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Common;
using IMPALLibrary.Masters.CustomerDetails;
#endregion

namespace IMPALWeb.Reports.Finance.SalesTax
{    public partial class CFormCustomer : System.Web.UI.Page
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

                    GetCustomerList();
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
                    IMPALLibrary.Masters.Finance oFinance = new IMPALLibrary.Masters.Finance();
                    oFinance.SalesTaxSummary(strBranchCode, txtFromDate.Text, txtToDate.Text);

                    string strSelectionFormula = null;
                    string strDateQuery = "{sales_order_header.Document_date}";
                    string strCustomerQuery = "{Sales_order_header.Customer_code}";
                    string strBranchQuery = "{branch_master.branch_code}";

                    string strFromDate = Convert.ToDateTime(hidFromDate.Value).ToString("yyyy,MM,dd");
                    string strToDate = Convert.ToDateTime(hidToDate.Value).ToString("yyyy,MM,dd");
                    strSelectionFormula = strDateQuery + " >= Date (" + strFromDate + ") and "
                                           + strDateQuery + " <= Date (" + strToDate + ")";
                    if (ddlCustomer.SelectedIndex > 0)
                        strSelectionFormula = strSelectionFormula + " and " + strCustomerQuery + " = '" + ddlCustomer.SelectedValue + "'";
                    if (!strBranchCode.Equals("CRP"))
                        strSelectionFormula = strSelectionFormula + " and " + strBranchQuery + " = '" + strBranchCode + "'";

                    rptCrystal.CrystalFormulaFields.Add("From_Date", "'" + hidFromDate.Value + "'");
                    rptCrystal.CrystalFormulaFields.Add("To_Date", "'" + hidToDate.Value + "'");
                    rptCrystal.RecordSelectionFormula = strSelectionFormula;
                    rptCrystal.GenerateReport();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region GetCustomerList
        private void GetCustomerList()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                List<CustomerDtls> lstCompletion = null;
                CustomerDetails oCustomerDtls = new CustomerDetails();
                CustomerDtls oCustomer = new CustomerDtls();
                lstCompletion = oCustomerDtls.GetCustomers(strBranchCode, null);
                ddlCustomer.DataSource = lstCompletion;
                ddlCustomer.DataTextField = "Name";
                ddlCustomer.DataValueField = "Code";
                ddlCustomer.DataBind();
                ddlCustomer.Items.Insert(0, string.Empty);
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion
    }
}
