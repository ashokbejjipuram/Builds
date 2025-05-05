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
using IMPALLibrary.Masters;
#endregion

namespace IMPALWeb
{
    public partial class TCSExemption : System.Web.UI.Page
    {
        CustomerDetails customer = new CustomerDetails();

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
                    if (strBranchCode.Equals("CRP"))
                    {
                        ddlBranch.Enabled = true;
                        Branches oBranch = new Branches();
                        ddlBranch.DataSource = oBranch.GetAllBranch();
                        ddlBranch.DataBind();
                    }
                    else
                    {
                        ddlBranch.Enabled = false;
                        Branches oBranches = new Branches();
                        Branch oBranchDetails = oBranches.GetBranchDetails(strBranchCode);
                        if (oBranchDetails != null)
                            ddlBranch.Items.Add(oBranchDetails.BranchName);
                    }

                    BtnSubmit.Visible = false;
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
                    GetCustomerList(ddlBranch.SelectedValue);
                }
                else
                {
                    ddlCustomer.Items.Clear();
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
        public void GetCustomerList(string strBranch)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                List<CustomerDtls> lstCompletion = null;
                lstCompletion = customer.GetCustomers(strBranch, null);
                ddlCustomer.DataSource = lstCompletion;
                ddlCustomer.DataTextField = "Name";
                ddlCustomer.DataValueField = "Code";
                ddlCustomer.DataBind();
                ddlCustomer.SelectedIndex = 0;
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
                Customers customers = new Customers();
                List<Customer> lstCustomers = new List<Customer>();
                lstCustomers = customers.GetCustomerDetails(ddlBranch.SelectedValue, ddlCustomer.SelectedValue);
                txtCustomerCode.Text = ddlCustomer.SelectedValue;
                txtAddress1.Text = lstCustomers[0].address1;
                txtAddress2.Text = lstCustomers[0].address2;
                txtAddress3.Text = lstCustomers[0].address3;
                txtAddress4.Text = lstCustomers[0].address4;
                txtLocation.Text = lstCustomers[0].Location;
                txtGSTIN.Text = lstCustomers[0].GSTIN;
                txtPinCode.Text = lstCustomers[0].Pincode;
                txtPhone.Text = lstCustomers[0].Phone;
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCustomer.SelectedIndex > 0)
            {
                ddlCustomer.Enabled = false;
                GetCustomerDetails(ddlCustomer.SelectedValue);
                divCustomerInfo.Attributes.Add("style", "display:inline");
                divOSdetails.Attributes.Add("style", "display:inline");
                BtnSubmit.Visible = true;
                ddlExemptionStatus.SelectedValue = customer.GetCustomerTCSExemptionStatus(ddlBranch.SelectedValue, ddlCustomer.SelectedValue);
            }
            else
            {
                BtnSubmit.Visible = false;
                divCustomerInfo.Attributes.Add("style", "display:none");
                divOSdetails.Attributes.Add("style", "display:none");
                ddlExemptionStatus.SelectedIndex = 0;
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddlBranch.Enabled = false;
                BtnSubmit.Visible = false;
                
                customer.UpdateCustomerTCSExemptionStatus(ddlBranch.SelectedValue, ddlCustomer.SelectedValue, ddlCustomer.SelectedItem.Text, txtLocation.Text, ddlExemptionStatus.SelectedValue);

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('TCS Exemption Status has been Updated Succesfully');", true);
                ddlCustomer.Enabled = false;
                ddlExemptionStatus.Enabled = false;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void BtnReset_Click(object sender, EventArgs e)
        {
            try
            {
                Server.ClearError();
                Response.Redirect("TCSExemption.aspx", false);
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