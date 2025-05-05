#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using IMPALLibrary.Masters.CustomerDetails;
using IMPALLibrary.Masters.Sales;
using System.Data; 
#endregion

namespace IMPALWeb
{
    public partial class CustomerOutstandingHO : System.Web.UI.Page
    {
        #region Declaration
        string strBranchCode = string.Empty;
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
                    ddlBranch.Enabled = true;
                    cmbCustomerName.Enabled = false;
                    Branches oBranch = new Branches();
                    ddlBranch.DataSource = oBranch.GetAllBranch();
                    ddlBranch.DataBind();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        protected void ddlBranch_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (ddlBranch.SelectedIndex > 0)
                {
                    GetCustomerList();
                    cmbCustomerName.Enabled = true;
                    cmbCustomerName.Focus();
                }
                else
                {
                    cmbCustomerName.Items.Clear();
                    cmbCustomerName.Enabled = false;
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        private string TwoDecimalConversion(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
                return "0.00";
            else
                return string.Format("{0:0.00}", Convert.ToDecimal(strValue));
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
                lstCompletion = oCustomerDtls.GetCustomers(ddlBranch.SelectedValue, null);
                cmbCustomerName.DataSource = lstCompletion;
                cmbCustomerName.DataTextField = "Name";
                cmbCustomerName.DataValueField = "Code";
                cmbCustomerName.DataBind();
                cmbCustomerName.SelectedIndex = 0;
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region GetCustomerDetails
        private void GetCustomerDetails(string BranchCode, string CustomerCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Salesbranches oSales = new Salesbranches();
                CustomerOutstandingDetails CustOSdetails = oSales.GetCustomerOutstandingHO(BranchCode, CustomerCode);
                txtCustomerCode.Text = cmbCustomerName.SelectedValue;
                txtAddress1.Text = CustOSdetails.Address1;
                txtAddress2.Text = CustOSdetails.Address2;
                txtAddress3.Text = CustOSdetails.Address3;
                txtAddress4.Text = CustOSdetails.Address4;
                txtLocation.Text = CustOSdetails.Location;
                txtGSTIN.Text = CustOSdetails.GSTINNo;
                txtPinCode.Text = CustOSdetails.PinCode;
                txtPhone.Text = CustOSdetails.Phone;
                txtCreditLimit.Text = TwoDecimalConversion(CustOSdetails.Credit_Limit);
                txtOutstanding.Text = TwoDecimalConversion(CustOSdetails.Outstanding);
                txtAbove180.Text = TwoDecimalConversion(CustOSdetails.Above180);
                txtAbove90.Text = TwoDecimalConversion(CustOSdetails.Above90);
                txtAbove60.Text = TwoDecimalConversion(CustOSdetails.Above60);
                txtAbove30.Text = TwoDecimalConversion(CustOSdetails.Above30);
                txtCurrentBal.Text = TwoDecimalConversion(CustOSdetails.CurBal);
                txtCrBal.Text = TwoDecimalConversion(CustOSdetails.Cr_Bal);

                if (CustOSdetails.Message.ToString().Substring(0, 1) == "O")
                    lblOsMessage.Text = "<span color='red'>" + CustOSdetails.Message + "</span>";
                else
                    lblOsMessage.Text = CustOSdetails.Message;
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        protected void cmbCustomerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlBranch.Enabled = false;
            cmbCustomerName.Enabled = false;
            GetCustomerDetails(ddlBranch.SelectedValue, cmbCustomerName.SelectedValue);
            divCustomerInfo.Attributes.Add("style", "display:inline");
            divOSdetails.Attributes.Add("style", "display:inline");
        }

        protected void BtnReset_Click(object sender, EventArgs e)
        {
            try
            {
                Server.ClearError();
                Response.Redirect("CustomerOutstandingHO.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesInvoiceEntry), exp);
            }
        }
        #endregion
    }
}
