using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using CrystalDecisions.Web;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using IMPALLibrary;
using IMPALLibrary.Transactions;
using IMPALLibrary.Masters;
using IMPALWeb.UserControls;
using IMPALLibrary.Common;
using IMPALLibrary.Masters.Sales;

namespace IMPALWeb
{
    public partial class PDCRegister : System.Web.UI.Page
    {
        SendSMS sendsms = new SendSMS();
		ReceiptTransactions objTrans = new ReceiptTransactions();
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(PDCRegister), exp);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ddlBranch.SelectedValue = Session["BranchCode"].ToString();
                    ddlBranch.Enabled = false;

                    SetDefaultValues();
                    ddlAccountingPeriod.Visible = true;
                    txtAccountPeriod.Visible = false;
                    FreezeButtons(true);

                    BtnSubmit.Enabled = true;
                    btnReset.Enabled = true;
                    hdnScreenMode.Value = "A";
                    BtnUpdate.Visible = false;

                    fnPopulateDropDown("PDCRegister", ddlModeOfReceipt);
                }
                
                BtnSubmit.Attributes.Add("OnClick", "return fnPDCRegisterSubmit();");
                BtnUpdate.Attributes.Add("OnClick", "return fnPDCRegisterUpdate();");
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(PDCRegister), exp);
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                BtnSubmit.Attributes.Add("style","display:none");
                BtnSubmit.Visible = false;
                BtnSubmit.Enabled = false;
                BtnUpdate.Enabled = false;
                ddlAccountingPeriod.Enabled = false;
                SubmitHeaderAndItems();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(PDCRegister), exp);
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                BtnSubmit.Attributes.Add("style", "display:none");
                BtnSubmit.Visible = false;
                BtnUpdate.Enabled = false;
                ddlAccountingPeriod.Enabled = false;
                UpdatePDCEntry();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(PDCRegister), exp);
            }
        }

        protected void BtnReset_Click(object sender, EventArgs e)
        {
            try
            {
                Server.ClearError();
                Response.Redirect("PDCRegister.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(PDCRegister), exp);
            }
        }

        protected void fnPopulateDropDown(string strType, DropDownList ddlList)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            ImpalLibrary lib = new ImpalLibrary();
            List<DropDownListValue> drop = new List<DropDownListValue>();
            try
            {
                drop = lib.GetDropDownListValues(strType);
                ddlList.DataSource = drop;
                ddlList.DataValueField = "DisplayValue";
                ddlList.DataTextField = "DisplayText";
                ddlList.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ddlCustomer_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlCustomer.SelectedValue == "0")
                    return;

                ReceivableInvoice receivableReceipt = new ReceivableInvoice();
                Customer customer = receivableReceipt.GetCustomerInfoByCustomerCode(ddlCustomer.SelectedValue, Session["BranchCode"].ToString());

                txtChequeNumber.Text = string.Empty;
                txtChequeDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtBank.Text = string.Empty;
                txtBranch.Text = string.Empty;

                txtCode.Text = ddlCustomer.SelectedValue;
                txtLocation.Text = customer.Location;
                txtAddress1.Text = customer.address1;
                txtAddress2.Text = customer.address2;
                txtAddress3.Text = customer.address3;
                txtAddress4.Text = customer.address4;
            }
            catch (Exception exp)
            {

                Log.WriteException(typeof(PDCRegister), exp);
            }
        }

        protected void imgEditToggle_Click(object sender, EventArgs e)
        {
            try
            {
                imgEditToggle.Visible = false;
                BtnSubmit.Visible = false;
                BtnUpdate.Visible = true;
                hdnScreenMode.Value = "E";

                ddlCustomer.Items.Clear();
                ddlCustomer.DataSourceID = null;

                Customers customers = new Customers();
                List<Customer> lstCustomers = new List<Customer>();
                lstCustomers = customers.GetAllCustomersPDCRegister(Session["BranchCode"].ToString(), ddlAccountingPeriod.SelectedValue);
                ddlCustomer.DataSource = lstCustomers;
                ddlCustomer.DataTextField = "Customer_Name";
                ddlCustomer.DataValueField = "Customer_Code";
                ddlCustomer.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(Receipts), exp);
            }
        }

        private void SetDefaultValues()
        {
            LoadAccountintPeriod();
            ddlCustomer.SelectedValue = "0";

            txtChequeNumber.Text = string.Empty;
            txtChequeDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txtBank.Text = string.Empty;
            txtBranch.Text = string.Empty;

            txtCode.Text = string.Empty;
            txtLocation.Text = string.Empty;
            txtAddress1.Text = string.Empty;
            txtAddress2.Text = string.Empty;
            txtAddress3.Text = string.Empty;
            txtAddress4.Text = string.Empty;
        }

        protected void txtChequeNumber_OnTextChanged(object sender, EventArgs e)
        {
            try
            {
                if (hdnScreenMode.Value == "A")
                {
                    if (txtChequeNumber.Text.Trim() != null && txtChequeNumber.Text.Trim() != "")
                    {
                        int ChqStatus = objTrans.GetExistingPDCchequeEntryStatus(ddlBranch.SelectedValue, ddlCustomer.SelectedValue, txtChequeNumber.Text, txtBank.Text, txtChequeAmount.Text, txtBank.Text, ddlAccountingPeriod.SelectedValue);

                        if (ChqStatus == 1)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('This Cheque Number is Already Exists for the Customer. Please Check Once.');", true);
                            txtChequeNumber.Text = "";
                            txtChequeNumber.Focus();
                            return;
                        }
                    }
                }
                else
                {
                    if (txtChequeNumber.Text.Trim() != null && txtChequeNumber.Text.Trim() != "")
                    {
                        DataSet ChqDetails = objTrans.GetExistingPDCchequeEntryDetails(ddlBranch.SelectedValue, ddlCustomer.SelectedValue, txtChequeNumber.Text, txtBank.Text, txtChequeAmount.Text, txtBank.Text, ddlAccountingPeriod.SelectedValue);

                        if (ChqDetails.Tables[0].Rows.Count <= 0)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('No Cheques are Pending for Clearance. Please Check Once.');", true);
                            txtChequeNumber.Text = "";
                            txtChequeNumber.Focus();
                            return;
                        }
                        else
                        {                            
                            txtChequeDate.Text = ChqDetails.Tables[0].Rows[0]["Cheque_Date"].ToString();
                            txtChequeAmount.Text = ChqDetails.Tables[0].Rows[0]["Cheque_Amount"].ToString();
                            txtBank.Text = ChqDetails.Tables[0].Rows[0]["Cheque_Bank"].ToString();
                            txtBranch.Text = ChqDetails.Tables[0].Rows[0]["Cheque_Branch"].ToString();
                            ddlClearedStatus.SelectedValue = ChqDetails.Tables[0].Rows[0]["Cleared_Status"].ToString();
                            ddlLocalOrOutStation.SelectedValue = ChqDetails.Tables[0].Rows[0]["Local_Outstation"].ToString();
                            txtremarks.Text = ChqDetails.Tables[0].Rows[0]["Remarks"].ToString();

                            txtChequeDate.Enabled = false;
                            txtChequeAmount.Enabled = false;
                            txtBank.Enabled = false;
                            txtBranch.Enabled = false;
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesInvoiceEntry), exp);
            }
        }
        private void FreezeButtons(bool Fzflag)
        {
            ddlCustomer.Enabled = Fzflag;
            txtChequeNumber.Enabled = Fzflag;
            txtChequeDate.Enabled = Fzflag;
            txtChequeAmount.Enabled = Fzflag;
            txtBank.Enabled = Fzflag;
            txtBranch.Enabled = Fzflag;
            ddlClearedStatus.Enabled = Fzflag;
            ddlLocalOrOutStation.Enabled = Fzflag;
            txtremarks.Enabled = Fzflag;            
        }

        private string TwoDecimalConversion(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
                return "0.00";
            else
                return string.Format("{0:0.00}", Convert.ToDecimal(strValue));
        }

        private string DecimalToIntConversion(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
                return "0";
            else
                return string.Format("{0:0}", Convert.ToDecimal(strValue));
        }

        private bool ChkCurrencyDots(string StrCurrValue)
        {
            int count = StrCurrValue.Split('.').Length - 1;
            if (count > 1)
                return false;
            else
                return true;
        }

        private void LoadAccountintPeriod()
        {
            AccountingPeriods Acc = new AccountingPeriods();
            ReceivableInvoice objReceivableInvoice = new ReceivableInvoice();
            List<string> lstAccountingPeriod = new List<string>();

            string accountingPeriod = GetCurrentFinancialYear();
            lstAccountingPeriod.Add(accountingPeriod);

            List<AccountingPeriod> AccordingPeriod = objReceivableInvoice.GetAccountingPeriod();
            List<AccountingPeriod> FinYear = AccordingPeriod.Where(p => lstAccountingPeriod.Contains(p.Desc)).OrderByDescending(c => c.AccPeriodCode).ToList();
            LoadDropDownLists<AccountingPeriod>(FinYear, ddlAccountingPeriod, "AccPeriodCode", "Desc", false, "");
        }

        private void LoadDropDownLists<T>(List<T> ListData, DropDownList DDlDropDown, string value_field, string text_field, bool bselect, string DefaultText)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                DDlDropDown.DataSource = ListData;
                DDlDropDown.DataTextField = text_field;
                DDlDropDown.DataValueField = value_field;
                DDlDropDown.DataBind();
                if (bselect.Equals(true))
                {
                    DDlDropDown.Items.Insert(0, DefaultText);
                }
            }
            catch (Exception ex)
            {

                Log.WriteException(Source, ex);
            }
        }

        public string GetCurrentFinancialYear()
        {
            int CurrentYear = DateTime.Today.Year;
            int PreviousYear = DateTime.Today.Year - 1;
            int NextYear = DateTime.Today.Year + 1;
            string PreYear = PreviousYear.ToString();
            string NexYear = NextYear.ToString();
            string CurYear = CurrentYear.ToString();
            string FinYear = null;

            if (DateTime.Today.Month > 3)
                FinYear = CurYear + "-" + NexYear;
            else
                FinYear = PreYear + "-" + CurYear;

            return FinYear.Trim();
        }
        private void SubmitHeaderAndItems()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            string ReceiptNumber = string.Empty;
            try
            {
                ReceiptEntity receiptEntity = new ReceiptEntity();
                receiptEntity.Items = new List<ReceiptItem>();

                receiptEntity.BranchCode = ddlBranch.SelectedValue;
                receiptEntity.AccountingPeriod = ddlAccountingPeriod.SelectedValue;
                receiptEntity.CustomerCode = ddlCustomer.SelectedValue;
                receiptEntity.PaymentType = ddlModeOfReceipt.SelectedValue;
                receiptEntity.Amount = txtChequeAmount.Text;
                receiptEntity.ChequeNumber = txtChequeNumber.Text;
                receiptEntity.ChequeDate = txtChequeDate.Text;
                receiptEntity.ChequeBank = txtBank.Text;
                receiptEntity.ChequeBranch = txtBranch.Text;
                receiptEntity.ClearedStatus = ddlClearedStatus.SelectedValue;
                receiptEntity.LocalOrOutstation = ddlLocalOrOutStation.SelectedValue;
                receiptEntity.Remarks = txtremarks.Text;               

                ReceiptTransactions receiptTransactions = new ReceiptTransactions();
                receiptTransactions.AddNewPDCRegister(ref receiptEntity);

                if (receiptEntity.ErrorMsg == string.Empty && receiptEntity.ErrorCode == "0")
                {
                    FreezeButtons(false);

                    BtnSubmit.Enabled = false;
                    BtnUpdate.Visible = false;
                    btnReset.Enabled = true;

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('PDC Register Entry Done Successfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('" + receiptEntity.ErrorMsg + "');", true);

                    BtnSubmit.Enabled = false;
                    btnReset.Enabled = true;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void UpdatePDCEntry()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            string ReceiptNumber = string.Empty;
            try
            {
                ReceiptEntity receiptEntity = new ReceiptEntity();
                receiptEntity.Items = new List<ReceiptItem>();

                receiptEntity.BranchCode = ddlBranch.SelectedValue;
                receiptEntity.AccountingPeriod = ddlAccountingPeriod.SelectedValue;
                receiptEntity.CustomerCode = ddlCustomer.SelectedValue;
                receiptEntity.PaymentType = ddlModeOfReceipt.SelectedValue;
                receiptEntity.Amount = txtChequeAmount.Text;
                receiptEntity.ChequeNumber = txtChequeNumber.Text;
                receiptEntity.ChequeDate = txtChequeDate.Text;
                receiptEntity.ChequeBank = txtBank.Text;
                receiptEntity.ChequeBranch = txtBranch.Text;
                receiptEntity.ClearedStatus = ddlClearedStatus.SelectedValue;
                receiptEntity.LocalOrOutstation = ddlLocalOrOutStation.SelectedValue;
                receiptEntity.Remarks = txtremarks.Text;

                ReceiptTransactions receiptTransactions = new ReceiptTransactions();
                receiptTransactions.UpdNewPDCRegister(ref receiptEntity);

                if (receiptEntity.ErrorMsg == string.Empty && receiptEntity.ErrorCode == "0")
                {
                    FreezeButtons(false);

                    BtnSubmit.Enabled = false;
                    BtnUpdate.Visible = false;
                    btnReset.Enabled = true;

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('PDC Register Updated Successfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('" + receiptEntity.ErrorMsg + "');", true);

                    BtnSubmit.Enabled = false;
                    btnReset.Enabled = true;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}
