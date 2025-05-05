#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Common;
using IMPALLibrary.Masters.CustomerDetails;
using IMPALLibrary.Masters.Sales;
#endregion

namespace IMPALWeb.Reports.Sales.SalesStatement
{
    public partial class SegmentTODSales : System.Web.UI.Page
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
                    if (crSegmentTOD != null)
                    {
                        crSegmentTOD.Dispose();
                        crSegmentTOD = null;
                    }

                    txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtToDate.Text = txtFromDate.Text;

                    GetCustomerList();
                    PopulateSegements();
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
            if (crSegmentTOD != null)
            {
                crSegmentTOD.Dispose();
                crSegmentTOD = null;
            }
        }
        protected void crSegmentTOD_Unload(object sender, EventArgs e)
        {
            if (crSegmentTOD != null)
            {
                crSegmentTOD.Dispose();
                crSegmentTOD = null;
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
                lstCompletion = oCustomerDtls.GetCustomers(strBranchCode, "SalesOrder");
                cbCustomerName.DataSource = lstCompletion;
                cbCustomerName.DataTextField = "Name";
                cbCustomerName.DataValueField = "Code";
                cbCustomerName.DataBind();
                cbCustomerName.SelectedIndex = 0;
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
                    CallCrystalReport();
                    btnReport.Visible = true;
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion      

        #region PopulateSegements
        private void PopulateSegements()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ImpalLibrary oLib = new ImpalLibrary();
                List<DropDownListValue> lstValues = new List<DropDownListValue>();
                lstValues = oLib.GetDropDownListValues("ReportType-Segment");
                ddlPlant.DataSource = lstValues;
                ddlPlant.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region GetCustomerDetails
        private void GetCustomerDetails(string CustomerCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                CustomerDetails oDtls = new CustomerDetails();
                CustomerDtls oCustomer = null;
                oCustomer = oDtls.GetDetails(strBranchCode, CustomerCode);
                txtCustomerCode.Text = cbCustomerName.SelectedValue;
                txtAddress1.Text = oCustomer.Address1;
                txtAddress2.Text = oCustomer.Address2;
                txtAddress3.Text = oCustomer.Address3;
                txtAddress4.Text = oCustomer.Address4;
                txtLocation.Text = oCustomer.Location;
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        protected void cbCustomerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCustomerDetails(cbCustomerName.SelectedValue);
            if (cbCustomerName.SelectedIndex > 0)
                divCustomerInfo.Style.Add("display", "block");
            else
                divCustomerInfo.Style.Add("display", "none");

        }
        #endregion

        #region CallCrystalReport
        private void CallCrystalReport()
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
                        string strDateQuery = "{V_tod.document_date}";
                        string strBranchQuery = "{V_tod.Branch_code}";
                        string strSupplierQuery = "mid({V_tod.Item_code},1,3)";
                        string strCustomerQuery = "{V_tod.Customer_Code}";
                        string strPartNumQuery = "mid({Item_master.supplier_part_number},1,8)";
                        string strPartNumPEKit = "mid({Item_master.supplier_part_number},1,2)";
                        string strVehCodeQuery = "{Item_master.vehicle_type_code}";
                        string strFromDate = Convert.ToDateTime(hidFromDate.Value).ToString("yyyy,MM,dd");
                        string strToDate = Convert.ToDateTime(hidToDate.Value).ToString("yyyy,MM,dd");
                        string strDateCompare = strDateQuery + " >= Date (" + strFromDate + ") and "
                                               + strDateQuery + " <= Date (" + strToDate + ")";

                        strSelectionFormula = strDateCompare;                        
                        TextBox txtCustomerName = cbCustomerName.FindControl("cbCustomerName_TextBox") as TextBox;
                        if (txtCustomerName != null && !string.IsNullOrEmpty(txtCustomerName.Text))
                            strSelectionFormula = strSelectionFormula + " and " + strCustomerQuery + " = '" + cbCustomerName.SelectedValue + "'";

                        if (ddlPlant.SelectedValue == "1")
                        {
                            strVehCodeQuery = strSelectionFormula + " and " + strSupplierQuery + "=" + "'160' and " + strVehCodeQuery + "='999990'";
                        }
                        else if (ddlPlant.SelectedValue == "2")
                        {
                            strVehCodeQuery = strSelectionFormula + " and " + strSupplierQuery + "=" + "'160' and " + strVehCodeQuery + "<>'999990'";
                        }
                        else if (ddlPlant.SelectedValue == "3")
                        {
                            strVehCodeQuery = strSelectionFormula + " and " + strSupplierQuery + "=" + "'750' and " + strPartNumQuery + "='29966940'";
                        }
                        else if (ddlPlant.SelectedValue == "4")
                        {
                            strVehCodeQuery = strSelectionFormula + " and " + strSupplierQuery + "=" + "'750' and " + strPartNumQuery + "<>'29966940'";
                        }
                        else if (ddlPlant.SelectedValue == "5")
                        {
                            strVehCodeQuery = strSelectionFormula + " and " + strSupplierQuery + "=" + "'450' and " + strVehCodeQuery + "='450000'";
                        }
                        else if (ddlPlant.SelectedValue == "6")
                        {
                            strVehCodeQuery = strSelectionFormula + " and " + strSupplierQuery + "=" + "'450' and " + strVehCodeQuery + "<>'450000' and " + strPartNumPEKit + "<>'35'";
                        }
                        else if (ddlPlant.SelectedValue == "7")
                        {
                            strVehCodeQuery = strSelectionFormula + " and " + strSupplierQuery + "=" + "'450' and " + strPartNumPEKit + "='35'";
                        }

                        if (!strBranchCode.Equals("CRP"))
                            strSelectionFormula = strVehCodeQuery + " and " + strBranchQuery + " = '" + strBranchCode + "'";

                        crSegmentTOD.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
                        crSegmentTOD.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
                        crSegmentTOD.RecordSelectionFormula = strSelectionFormula;
                        crSegmentTOD.GenerateReport();
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
