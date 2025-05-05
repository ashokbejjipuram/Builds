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
    public partial class NEFTDetailsCorrection : System.Web.UI.Page
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
                    Branches oBranch = new Branches();
                    ddlBranch.DataSource = oBranch.GetAllBranch();
                    ddlBranch.DataBind();

                    ddlCustomer.DataSource = null;
                    ddlCustomer.DataBind();
                    ddlBranch.SelectedIndex = 0;
                    txtNEFTDate.Text = "";
                    txtAmount.Text = "";
                    txtRemarks.Text = "";
                    txtCustomerCode.Text = "";
                    BtnSubmit.Visible = false;
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        private string TwoDecimalConversion(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
                return "0.00";
            else
                return string.Format("{0:0.00}", Convert.ToDecimal(strValue));
        }

        #region GetCustomerDetails
        private void GetNEFTDetails(string BankRefNo)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                CustomerDetails customers = new CustomerDetails();
                List<CustomerFields> lstCustomers = null;
                lstCustomers = customers.GetDealerNEFTDetails(BankRefNo);

                if (lstCustomers.Count > 0)
                {
                    ddlBranch.SelectedValue = lstCustomers[0].Branch_Code;
                    txtNEFTDate.Text = lstCustomers[0].NEFT_Date;
                    txtAmount.Text = TwoDecimalConversion(lstCustomers[0].NEFT_Amount);
                    txtRemarks.Text = lstCustomers[0].NEFT_Remarks;

                    GetCustomerList(lstCustomers[0].Branch_Code);
                    txtCustomerCode.Text = "";

                    divNEFTDetails.Attributes.Add("style", "display:inline");
                }
                else
                {
                    divNEFTDetails.Attributes.Add("style", "display:none");                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('NEFT Details are Not Available or Receipt has already been Passed');", true);
                }

                BtnSubmit.Visible = false;
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtBankRefNo.Text.Trim() != "")
            {
                GetNEFTDetails(txtBankRefNo.Text.Trim());                
            }
            else
            {
                ddlCustomer.DataSource = null;
                ddlCustomer.DataBind();
                ddlBranch.SelectedIndex = 0;
                txtNEFTDate.Text = "";
                txtAmount.Text = "";
                txtRemarks.Text = "";
                txtCustomerCode.Text = "";
                divNEFTDetails.Attributes.Add("style", "display:none");
            }
        }

        public void GetCustomerList(string strBranch)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                List<CustomerDtls> lstCompletion = null;
                CustomerDetails oCustomerDtls = new CustomerDetails();
                CustomerDtls oCustomer = new CustomerDtls();
                lstCompletion = oCustomerDtls.GetCustomers(strBranch, null);
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

        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCustomer.SelectedIndex > 0)
            {                
                txtCustomerCode.Text = ddlCustomer.SelectedValue;
                BtnSubmit.Visible = true;
            }
            else
            {
                txtCustomerCode.Text = "";
                BtnSubmit.Visible = false;
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                BtnSubmit.Visible = false;
                btnSearch.Visible = false;

                customer.UpdateDealerNEFTDetails(ddlBranch.SelectedValue, txtBankRefNo.Text, txtCustomerCode.Text);

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Dealer Code has been Updated Succesfully');", true);
                
                txtBankRefNo.Enabled = false;
                ddlCustomer.Enabled = false;

                CustomerDetails customers = new CustomerDetails();
                List<CustomerFields> lstCustomers = null;
                lstCustomers = customers.GetDealerNEFTDetails(txtBankRefNo.Text);
                txtRemarks.Text = lstCustomers[0].NEFT_Remarks;
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
                Response.Redirect("NEFTDetailsCorrection.aspx", false);
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