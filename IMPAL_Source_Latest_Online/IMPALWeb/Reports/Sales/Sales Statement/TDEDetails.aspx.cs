#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Masters.CustomerDetails;
using IMPALLibrary.Common;
#endregion

namespace IMPALWeb.Reports.Sales.SalesStatement
{
    public partial class TDEDetails : System.Web.UI.Page
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
                    if (crSalesTDEDetails != null)
                    {
                        crSalesTDEDetails.Dispose();
                        crSalesTDEDetails = null;
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
            if (crSalesTDEDetails != null)
            {
                crSalesTDEDetails.Dispose();
                crSalesTDEDetails = null;
            }
        }
        protected void crSalesTDEDetails_Unload(object sender, EventArgs e)
        {
            if (crSalesTDEDetails != null)
            {
                crSalesTDEDetails.Dispose();
                crSalesTDEDetails = null;
            }
        }

        #region GetCustomerList
        public void GetCustomerList()
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

        #region btnReport_Click
        protected void btnReportPDF_Click(object sender, EventArgs e)
        {
            PanelHeaderDtls.Enabled = false;
            GenerateAndExportReport(".pdf");
        }
        protected void btnReportExcel_Click(object sender, EventArgs e)
        {
            PanelHeaderDtls.Enabled = false;
            GenerateAndExportReport(".xls");
        }

        protected void btnReportRTF_Click(object sender, EventArgs e)
        {
            PanelHeaderDtls.Enabled = false;
            GenerateAndExportReport(".doc");
        }
        protected void btnReport_Click(object sender, EventArgs e)
        {
            PanelHeaderDtls.Enabled = false;

            btnReportPDF.Attributes.Add("style", "display:inline");
            btnReportExcel.Attributes.Add("style", "display:inline");
            btnReportRTF.Attributes.Add("style", "display:inline");
            btnBack.Attributes.Add("style", "display:inline");
            btnReport.Attributes.Add("style", "display:none");

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Report Has been Generated Successfully. Please Click on Below Buttons to Export in Respective Formats');", true);
        }
        #endregion

        #region CallCrystalReport
        private void GenerateAndExportReport(string fileType)
        {
            string strSelectionFormula = null;
            string strDocDateQuery = "{V_Tod.Document_Date}";
            string strBranchQuery = "{V_Tod.Branch_code}";
            string strSupplierQuery = "mid({V_Tod.Item_code},1,3)";
            string strCustomerQuery = "{V_TOD.Customer_code}";

            if (!string.IsNullOrEmpty(txtFromDate.Text) && !string.IsNullOrEmpty(txtToDate.Text))
            {
                string strFromDate = Convert.ToDateTime(hidFromDate.Value).ToString("yyyy,MM,dd");
                string strToDate = Convert.ToDateTime(hidToDate.Value).ToString("yyyy,MM,dd");
                strSelectionFormula = strDocDateQuery + " >= Date (" + strFromDate + ") and "
                                       + strDocDateQuery + " <= Date (" + strToDate + ")";
                if (ddlCustomer.SelectedIndex > 0)
                    strSelectionFormula = strSelectionFormula + " and " + strCustomerQuery + " = '" + ddlCustomer.SelectedValue + "'";
                if (ddlSupplier.SelectedIndex > 0)
                    strSelectionFormula = strSelectionFormula + " and " + strSupplierQuery + " = '" + ddlSupplier.SelectedValue + "'";
                if (!strBranchCode.Equals("CRP"))
                    strSelectionFormula = strSelectionFormula + " and " + strBranchQuery + " = '" + strBranchCode + "'";
            }

            crSalesTDEDetails.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
            crSalesTDEDetails.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
            crSalesTDEDetails.RecordSelectionFormula = strSelectionFormula;
            crSalesTDEDetails.GenerateReportAndExportHO(fileType);
        }
        #endregion

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Server.ClearError();
                Response.Redirect("TDEDetails.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
    }
}
