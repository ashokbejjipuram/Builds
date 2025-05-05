#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Web.Security;
using System.Web.Caching;
using AjaxControlToolkit;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using IMPALLibrary;
using IMPALLibrary.Common;
using System.Data;
using IMPALWeb.UserControls;
using IMPLLib = IMPALLibrary.Transactions;
using IMPALLibrary.Masters.Sales;
using IMPALLibrary.Transactions.Finance;
using IMPALLibrary.Masters;
using static IMPALLibrary.ReceivableInvoice;
using System.Globalization;

#endregion Namespace

namespace IMPALWeb.Finance
{
    public partial class CashAndBankReceiptNew : System.Web.UI.Page
    {
        private string strBranchCode;
        private AccountingPeriods Acc = new AccountingPeriods();
        private ReceivableInvoice objReceivableInvoice = new ReceivableInvoice();
        ReceiptTransactions objTrans = new ReceiptTransactions();

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(CashAndBankReceiptNew), exp);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = (string)Session["BranchCode"];

                txtChartOfAccount.Attributes.Add("onkeydown", "checkKey();");                

                ddlBranch.SelectedValue = Session["BranchCode"].ToString();
                ddlBranch.Enabled = false;

                if (!IsPostBack)
                {
                    btnBack.Visible = false;

                    if (crCashAndBank != null)
                    {
                        crCashAndBank.Dispose();
                        crCashAndBank = null;
                    }

                    fnPopulateTransactionNumber(strBranchCode);
                    ddlTransactionNumber.Visible = false;
                    ddlCustomer.Enabled = false;
                    ddlHoRefNo.Enabled = false;
                    txtTransactionAmount.Attributes.Add("onkeypress", "return CurrencyNumberOnly();");
                    LoadAccountintPeriod();
                    txtAccountingPeriod.Attributes.Add("style", "display:none");
                    fnPopulateDropDown("CBRReceipts", ddlPayment);
                    fnPopulateDropDown("CBRReceiptTransType", ddlCBReceiptType);                    
                    ddlModeOfReceipt.Attributes.Add("OnChange", "funModeOfPayment();");
                    fnPopulateDropDown("LocalOutStation", ddlLocalOutstation);
                    grvCashAndBankReceipt.Visible = false;
                    BtnSubmit.Visible = false;
                    btnReport.Enabled = false;

                    txtChequeNumber.Enabled = false;
                    txtChequeDate.Enabled = false;
                    txtBank.Enabled = false;
                    txtBankBranch.Enabled = false;
                    hdnMode.Value = ddlModeOfReceipt.SelectedValue;
                    panelCustDetails.Visible = false;

                    DateItem PrevDateStatus = objReceivableInvoice.GetPreviousDateStatus(Session["UserID"].ToString(), "CBReceipt");

                    if (PrevDateStatus.DateItemCode != "0")
                    {
                        txtTransactionDate.Enabled = true;
                        txtTransactionDate.Attributes.Add("onkeypress", "return false;");
                        txtTransactionDate.Attributes.Add("onkeyup", "return false;");
                        txtTransactionDate.Attributes.Add("onkeydown", "return false;");
                        txtTransactionDate.Attributes.Add("onpaste", "return false;");
                        txtTransactionDate.Attributes.Add("ondragstart", "return false;");
                        calTransactionDate.StartDate = DateTime.ParseExact(PrevDateStatus.DateItemDesc, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        calTransactionDate.EndDate = DateTime.ParseExact(PrevDateStatus.DateItemDesc, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    else
                        txtTransactionDate.Enabled = false;
                }                

                if (ddlModeOfReceipt.SelectedValue.ToString().ToUpper() == "H" || ddlModeOfReceipt.SelectedValue.Trim() == "")
                    BankAccNo.Filter = "036";
                else
                    BankAccNo.Filter = "049";
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crCashAndBank != null)
            {
                crCashAndBank.Dispose();
                crCashAndBank = null;
            }
        }
        protected void crCashAndBank_Unload(object sender, EventArgs e)
        {
            if (crCashAndBank != null)
            {
                crCashAndBank.Dispose();
                crCashAndBank = null;
            }
        }

        private void LoadAccountintPeriod()
        {
            List<string> lstAccountingPeriod = new List<string>();

            int PrevFinYearStatus = objReceivableInvoice.GetPreviousAccountingPeriodStatus(Session["UserID"].ToString(), "CBReceipt");

            if (PrevFinYearStatus > 0)
            {
                lstAccountingPeriod.Add((DateTime.Today.Year - 1).ToString() + "-" + DateTime.Today.Year.ToString());
            }

            string accountingPeriod = GetCurrentFinancialYear();
            lstAccountingPeriod.Add(accountingPeriod);
            txtTransactionDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            List<AccountingPeriod> AccordingPeriod = objReceivableInvoice.GetAccountingPeriod();
            List<AccountingPeriod> FinYear = AccordingPeriod.Where(p => lstAccountingPeriod.Contains(p.Desc)).OrderByDescending(c => c.AccPeriodCode).ToList();
            LoadDropDownLists<AccountingPeriod>(FinYear, ddlAccountingPeriod, "AccPeriodCode", "Desc", false, "");
        }

        private void LoadAccountintPeriodView()
        {
            List<string> lstAccountingPeriod = new List<string>();
            string accountingPeriod = GetCurrentFinancialYear();
            lstAccountingPeriod.Add(accountingPeriod);
            List<AccountingPeriod> AccordingPeriod = objReceivableInvoice.GetAccountingPeriod();
            LoadDropDownLists<AccountingPeriod>(AccordingPeriod, ddlAccountingPeriod, "AccPeriodCode", "Desc", false, "");
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

        protected void ddlAccountingPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            DebitCredit objDebitCredit = new DebitCredit();

            ddlAccountingPeriod.Enabled = false;

            if (ddlAccountingPeriod.SelectedIndex == 0)
                txtTransactionDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            else
                txtTransactionDate.Text = objDebitCredit.GetDocumentDate(ddlAccountingPeriod.SelectedValue);
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

        protected void gvResults_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (grvCashAndBankReceipt.Rows.Count > 0)
            {
                if (grvCashAndBankReceipt.FooterRow != null)
                {
                    TextBox txtTotalAmount = (TextBox)grvCashAndBankReceipt.FooterRow.FindControl("txtTotalAmount");
                    txtTotalAmount.Text = "0";

                    for (int i = 0; i < grvCashAndBankReceipt.Rows.Count; i++)
                    {
                        TextBox txtGrdAmount = (TextBox)grvCashAndBankReceipt.Rows[i].Cells[2].FindControl("txtGrdAmount");

                        txtTotalAmount.Text = TwoDecimalConversion(Convert.ToString(Convert.ToDouble(txtTotalAmount.Text) + Convert.ToDouble(txtGrdAmount.Text == "" ? "0.00" : txtGrdAmount.Text)));
                    }
                }
            }

            btnReport.Enabled = false;
        }

        protected void gvResults_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataTable dt = (DataTable)ViewState["CurrentTable"];
                    dt.Rows.Clear();
                    dt.AcceptChanges();

                    if (grvCashAndBankReceipt.Rows.Count >= 1)
                    {
                        for (int i = 0; i < grvCashAndBankReceipt.Rows.Count; i++)
                        {
                            DataRow dr = dt.NewRow();

                            TextBox txtGrdChartAccount = (TextBox)grvCashAndBankReceipt.Rows[i].Cells[1].FindControl("txtGrdChartOfAccount");
                            TextBox txtGrdAmount = (TextBox)grvCashAndBankReceipt.Rows[i].Cells[2].FindControl("txtGrdAmount");
                            TextBox txtGrdRemarks = (TextBox)grvCashAndBankReceipt.Rows[i].Cells[3].FindControl("txtGrdRemarks");

                            dr["Chart_of_Account_Code"] = txtGrdChartAccount.Text;
                            dr["Amount"] = txtGrdAmount.Text;
                            dr["Remarks"] = txtGrdRemarks.Text;
                            dt.Rows.Add(dr);
                        }

                        dt.Rows.RemoveAt(e.RowIndex);
                        grvCashAndBankReceipt.DataSource = dt;
                        grvCashAndBankReceipt.DataBind();

                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                ChartAccount COA = (ChartAccount)grvCashAndBankReceipt.Rows[i].Cells[1].FindControl("COA");
                                TextBox txtGrdChartAccount = (TextBox)grvCashAndBankReceipt.Rows[i].Cells[1].FindControl("txtGrdChartOfAccount");
                                TextBox txtGrdAmount = (TextBox)grvCashAndBankReceipt.Rows[i].Cells[2].FindControl("txtGrdAmount");
                                TextBox txtGrdRemarks = (TextBox)grvCashAndBankReceipt.Rows[i].Cells[3].FindControl("txtGrdRemarks");

                                txtGrdChartAccount.Text = dt.Rows[i]["Chart_of_Account_Code"].ToString();
                                txtGrdAmount.Text = dt.Rows[i]["Amount"].ToString();
                                txtGrdRemarks.Text = dt.Rows[i]["Remarks"].ToString();

                                if (i < dt.Rows.Count - 1)
                                {
                                    COA.Visible = false;
                                    txtGrdAmount.Enabled = false;
                                }
                            }
                        }
                        else
                        {
                            grvCashAndBankReceipt.Visible = false;
                            BtnSubmit.Visible = false;
                            btnTransactionDetails.Visible = true;
                        }

                        ViewState["CurrentTable"] = dt;
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ddlCBReceiptType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlCustomer.SelectedIndex = 0;

                if (ddlCBReceiptType.SelectedValue.ToString().ToUpper() == "A")
                {
                    ddlCustomer.Enabled = true;
                    btnTransactionDetails.Visible = false;
                    BtnSubmit.Visible = true;
                    BtnSubmit.Enabled = true;
                    BankAccNo.Visible = false;
                    txtChartOfAccount.Text = "40703602500000001" + strBranchCode;
                    txtReferenceDate.Text = txtTransactionDate.Text;
                }
                else
                {
                    txtChartOfAccount.Text = "";
                    ddlCustomer.Enabled = false;
                    btnTransactionDetails.Visible = true;
                    BtnSubmit.Visible = false;
                    BtnSubmit.Enabled = false;
                    BankAccNo.Visible = true;
                    txtReferenceDate.Text = "";

                    if (ddlCBReceiptType.SelectedValue.ToString().ToUpper() != "")
                        fnPopulateDropDown("CBPCashChequeNew", ddlModeOfReceipt);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesInvoiceEntry), exp);
            }
        }

        protected void ddlModeOfReceipt_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlHoRefNo.Items.Clear();
                txtTransactionAmount.Text = "";
                hdnMode.Value = ddlModeOfReceipt.SelectedValue;

                if (ddlCBReceiptType.SelectedValue == "A")
                {
                    if (ddlModeOfReceipt.SelectedValue.ToString().ToUpper() == "H")
                        txtChartOfAccount.Text = "40703602500000001" + strBranchCode;
                    else
                    {
                        txtChartOfAccount.Text = "4070490021" + strBranchCode + "0910" + strBranchCode;                        

                        if (ddlModeOfReceipt.SelectedValue.ToString().ToUpper() == "Q")
                        {
                            txtTransactionAmount.AutoPostBack = false;
                            ddlHoRefNo.Enabled = false;
                            IMPLLib.ChequeSlip objChequeSlip = new IMPLLib.ChequeSlip();
                            IMPLLib.ChequeSlipBankDetails objBankDtl = new IMPLLib.ChequeSlipBankDetails();
                            objBankDtl = objChequeSlip.GetChequeSlipBankDetails(txtChartOfAccount.Text);
                            //txtChequeDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                            txtChequeDate.Text = txtTransactionDate.Text;
                            txtBank.Text = objBankDtl.BankName;
                            txtBankBranch.Text = objBankDtl.Address;

                            txtChequeNumber.Enabled = true;
                            txtChequeDate.Enabled = true;
                            txtBank.Enabled = true;
                            txtBankBranch.Enabled = true;
                        }
                        else
                        {
                            txtTransactionAmount.AutoPostBack = true;
                            ddlHoRefNo.Enabled = true;
                            txtChequeDate.Text = string.Empty;
                            txtBank.Text = string.Empty;
                            txtBankBranch.Text = string.Empty;
                        }
                    }                    
                }
                else
                {
                    txtChartOfAccount.Text = "";
                    txtTransactionAmount.AutoPostBack = false;
                    ddlHoRefNo.Enabled = false;

                    if (ddlModeOfReceipt.SelectedValue.ToString().ToUpper() == "H" || ddlModeOfReceipt.SelectedValue.ToString().ToUpper() == "D")
                    {
                        txtChequeNumber.Text = "";
                        txtChequeDate.Text = "";
                        txtBank.Text = "";
                        txtBankBranch.Text = "";
                        txtChequeNumber.Enabled = false;
                        txtChequeDate.Enabled = false;
                        txtBank.Enabled = false;
                        txtBankBranch.Enabled = false;
                        txtChequeNumber.AutoPostBack = false;
                    }
                    else
                    {
                        if (ddlCBReceiptType.SelectedValue.ToString().ToUpper() == "A")
                            txtChequeNumber.AutoPostBack = true;
                        else
                            txtChequeNumber.AutoPostBack = false;

                        txtChequeNumber.Enabled = true;
                        txtChequeDate.Enabled = true;
                        txtBank.Enabled = true;
                        txtBankBranch.Enabled = true;
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesInvoiceEntry), exp);
            }
        }

        protected void ddlCustomer_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlCustomer.SelectedIndex == 0)
                {
                    panelCustDetails.Visible = false;
                    txtCode.Text = string.Empty;
                    txtLocation.Text = string.Empty;
                    txtAddress1.Text = string.Empty;
                    txtAddress2.Text = string.Empty;
                    txtAddress3.Text = string.Empty;
                    txtAddress4.Text = string.Empty;

                    return;
                }
                else
                {
                    panelCustDetails.Visible = true;
                    ReceivableInvoice receivableReceipt = new ReceivableInvoice();
                    Customer customer = receivableReceipt.GetCustomerInfoChqDishonorByCustomerCode(ddlCustomer.SelectedValue, Session["BranchCode"].ToString());

                    if (customer.ChqDishonorInd == 0)
                        fnPopulateDropDown("CBPCashCheque", ddlModeOfReceipt);
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('This Customer is having Cheque Dishonor Count of " + customer.ChqDishonorCnt + " and hence Cheque Mode is Disabled.');", true);
                        fnPopulateDropDown("CBPCashChequeDishonor", ddlModeOfReceipt);
                    }

                    txtCode.Text = customer.Customer_Code;
                    txtLocation.Text = customer.Location;
                    txtAddress1.Text = customer.address1;
                    txtAddress2.Text = customer.address2;
                    txtAddress3.Text = customer.address3;
                    txtAddress4.Text = customer.address4;
                }
            }
            catch (Exception exp)
            {

                Log.WriteException(typeof(Receipts), exp);
            }
        }

        protected void txtTransactionAmount_OnTextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtTransactionAmount.Text != null && txtTransactionAmount.Text != "")
                {
                    if (ddlCBReceiptType.SelectedValue.ToString().ToUpper() == "A" && ddlModeOfReceipt.SelectedValue.ToString().ToUpper() == "D")
                    {
                        ddlHoRefNo.DataSource = (object)GetHORefDetails();
                        ddlHoRefNo.DataTextField = "ItemDesc";
                        ddlHoRefNo.DataValueField = "ItemCode";
                        ddlHoRefNo.DataBind();

                        if (ddlHoRefNo.Items.Count <= 1)
                            ddlHoRefNo.Enabled = false;
                        else
                            ddlHoRefNo.Enabled = true;
                    }
                    else
                    {
                        ddlHoRefNo.Items.Clear();
                        ddlHoRefNo.Enabled = false;
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesInvoiceEntry), exp);
            }
        }

        private List<IMPALLibrary.Transactions.Item> GetHORefDetails()
        {
            List<IMPALLibrary.Transactions.Item> obj = objTrans.GetHOReceiptRefDetails(strBranchCode, ddlCustomer.SelectedValue, ddlAccountingPeriod.SelectedValue, txtTransactionAmount.Text);
            return obj;
        }

        protected void txtChequeNumber_OnTextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtChequeNumber.Text.Trim() != null && txtChequeNumber.Text.Trim() != "")
                {
                    int ChqStatus = objTrans.GetExistingChequeEntryStatusCB(ddlBranch.SelectedValue, ddlCustomer.SelectedValue, txtChequeNumber.Text, txtTransactionAmount.Text, ddlAccountingPeriod.SelectedValue);

                    if (ChqStatus == 1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('This Cheque Number is Already Exists for the Customer. Please Check Once.');", true);
                        txtChequeNumber.Text = "";
                        txtChequeNumber.Focus();
                        return;
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesInvoiceEntry), exp);
            }
        }

        protected void imgEditToggle_Click(object sender, ImageClickEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                txtTransactionNumber.Visible = false;
                ddlTransactionNumber.Visible = true;
                ddlAccountingPeriod.Enabled = false;
                BtnSubmit.Enabled = false;
                DisableViewMode();
                grvCashAndBankReceipt.Visible = false;
                btnReport.Visible = true;                
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void DisableViewMode()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddlCBReceiptType.Enabled = false;
                txtTransactionDate.Enabled = false;
                txtRemarks.Enabled = false;
                txtTransactionNumber.Enabled = false;
                txtTransactionAmount.Enabled = false;
                ddlPayment.Enabled = false;
                ddlModeOfReceipt.Enabled = false;
                txtChequeNumber.Enabled = false;
                txtChequeDate.Enabled = false;
                txtBank.Enabled = false;
                txtBankBranch.Enabled = false;
                ddlLocalOutstation.Enabled = false;
                txtReferenceDate.Enabled = false;
                grvCashAndBankReceipt.Enabled = false;
                BtnSubmit.Enabled = false;
                imgEditToggle.Visible = false;
                BankAccNo.Visible = false;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            btnReport.Enabled = true;
            ddlModeOfReceipt.Enabled = false;
            ddlAccountingPeriod.Enabled = false;
            try
            {
                SubmitCashAndBankReceipt();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Server.ClearError();
                Response.Redirect("CashAndBankReceiptNew.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ucChartAccount_SearchImageClicked(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                txtChartOfAccount.Text = Session["ChatAccCode"].ToString();
                ddlAccountingPeriod.Enabled = false;
                Session["ChatAccCode"] = "";
                ImpalLibrary lib = new ImpalLibrary();
                List<DropDownListValue> drop = new List<DropDownListValue>();

                drop = lib.GetDropDownListValues("CBPCashCheque");
                if (Session["ChatDescription"].ToString() == "CASH ON HAND")
                {
                    ddlModeOfReceipt.DataSource = drop.Where(p => p.DisplayValue == "H").ToList(); ;
                    ddlModeOfReceipt.DataValueField = "DisplayValue";
                    ddlModeOfReceipt.DataTextField = "DisplayText";
                    ddlModeOfReceipt.DataBind();

                    txtChequeNumber.Text = "";
                    txtChequeDate.Text = "";
                    txtBank.Text = "";
                    txtBankBranch.Text = "";
                }
                else
                {
                    IMPLLib.ChequeSlip objChequeSlip = new IMPLLib.ChequeSlip();
                    IMPLLib.ChequeSlipBankDetails objBankDtl = new IMPLLib.ChequeSlipBankDetails();
                    objBankDtl = objChequeSlip.GetChequeSlipBankDetails(txtChartOfAccount.Text);
                    //txtChequeDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    txtChequeDate.Text = txtTransactionDate.Text;
                    txtBank.Text = objBankDtl.BankName;
                    txtBankBranch.Text = objBankDtl.Address;

                    ddlModeOfReceipt.DataSource = drop.Where(p => p.DisplayValue != "H").ToList();
                    ddlModeOfReceipt.DataValueField = "DisplayValue";
                    ddlModeOfReceipt.DataTextField = "DisplayText";
                    ddlModeOfReceipt.DataBind();
                    ddlModeOfReceipt.SelectedValue = hdnMode.Value;
                }

                if (ddlTransactionNumber.Visible == false)
                {
                    if (ddlModeOfReceipt.SelectedValue == "Q")
                    {
                        txtChequeNumber.Enabled = true;
                        txtChequeDate.Enabled = true;
                        txtBank.Enabled = true;
                        txtBankBranch.Enabled = true;
                    }
                    else
                    {
                        txtChequeNumber.Enabled = false;
                        txtChequeDate.Enabled = false;
                        txtBank.Enabled = false;
                        txtBankBranch.Enabled = false;
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void COA_SearchImageClicked(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                GridViewRow gvr = (GridViewRow)((IMPALWeb.UserControls.ChartAccount)sender).Parent.Parent;
                ((TextBox)gvr.FindControl("txtGrdChartOfAccount")).Text = Session["ChatAccCode"].ToString();
                Session["ChatAccCode"] = "";
                ((TextBox)gvr.FindControl("txtGrdAmount")).Focus();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ddlTransactionNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            ddlCustomer.Items.Clear();
            ddlCustomer.DataSourceID = null;

            Customers customers = new Customers();
            List<Customer> lstCustomers = new List<Customer>();
            lstCustomers = customers.GetAllCustomersExisting(Session["BranchCode"].ToString());
            ddlCustomer.DataSource = lstCustomers;
            ddlCustomer.DataTextField = "Customer_Name";
            ddlCustomer.DataValueField = "Customer_Code";
            ddlCustomer.DataBind();

            fnGetCashAndBankReceipt(ddlTransactionNumber.SelectedValue);
            grvCashAndBankReceipt.Visible = true;
            grvCashAndBankReceipt.Enabled = false;
            txtChartOfAccount.Enabled = false;
            BankAccNo.Visible = false;
            txtTransactionAmount.Enabled = false;
            txtRemarks.Enabled = false;
            ddlPayment.Enabled = false;
            ddlModeOfReceipt.Enabled = false;
            btnTransactionDetails.Visible = false;
            BtnSubmit.Visible = false;
            btnReport.Enabled = true;
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

        private void SubmitCashAndBankReceipt()
        {
            CashAndBankEntity cashandbankEntity = new CashAndBankEntity();
            cashandbankEntity.Items = new List<CashAndBankItem>();

            cashandbankEntity.TransactionNumber = txtTransactionNumber.Text;
            cashandbankEntity.TransactionDate = txtTransactionDate.Text;
            cashandbankEntity.BranchCode = strBranchCode;
            cashandbankEntity.Remarks = txtRemarks.Text;
            cashandbankEntity.ChartOfAccountCode = txtChartOfAccount.Text;
            cashandbankEntity.ReceiptPaymentIndicator = ddlPayment.SelectedValue;
            cashandbankEntity.CashChequeIndicator = ddlModeOfReceipt.SelectedValue;
            cashandbankEntity.TransactionAmount = txtTransactionAmount.Text == "" ? "0.00" : txtTransactionAmount.Text;
            cashandbankEntity.CustomerCode = ddlCustomer.SelectedValue;
            cashandbankEntity.RefNo = ddlHoRefNo.SelectedValue;

            cashandbankEntity.Cheque_Number = txtChequeNumber.Text;
            cashandbankEntity.Cheque_Date = txtChequeDate.Text;
            cashandbankEntity.Cheque_Bank = txtBank.Text;
            cashandbankEntity.Cheque_Branch = txtBankBranch.Text;
            cashandbankEntity.Local_Outstation = ddlLocalOutstation.SelectedValue;
            cashandbankEntity.Ref_Date = txtReferenceDate.Text;

            CashAndBankItem cashandbankItem = null;
            int intCount = 0;

            if (ddlCBReceiptType.SelectedValue.ToString().ToUpper() == "A")
            {
                cashandbankItem = new CashAndBankItem();
                cashandbankItem.Serial_Number = "1";
                cashandbankItem.Chart_of_Account_Code = "4070330220" + ddlCustomer.SelectedValue + strBranchCode;
                cashandbankItem.Amount = txtTransactionAmount.Text;
                cashandbankItem.Remarks = txtRemarks.Text;

                cashandbankEntity.Items.Add(cashandbankItem);
            }
            else
            {
                foreach (GridViewRow grvRow in grvCashAndBankReceipt.Rows)
                {
                    cashandbankItem = new CashAndBankItem();

                    intCount += 1;
                    cashandbankItem.Serial_Number = intCount.ToString();
                    cashandbankItem.Chart_of_Account_Code = ((TextBox)grvRow.Cells[1].FindControl("txtGrdChartOfAccount")).Text;
                    cashandbankItem.Amount = ((TextBox)grvRow.Cells[2].FindControl("txtGrdAmount")).Text == "" ? "0.00" : ((TextBox)grvRow.Cells[2].FindControl("txtGrdAmount")).Text;
                    cashandbankItem.Remarks = ((TextBox)grvRow.Cells[3].FindControl("txtGrdRemarks")).Text;

                    cashandbankEntity.Items.Add(cashandbankItem);
                }
            }

            CashAndBankTransactions cashandbankTransactions = new CashAndBankTransactions();
            int result = cashandbankTransactions.AddCashBankDetailsNew(ref cashandbankEntity, ddlCBReceiptType.SelectedValue);

            if ((cashandbankEntity.ErrorMsg == string.Empty) && (cashandbankEntity.ErrorCode == "0"))
            {
                txtTransactionNumber.Text = cashandbankEntity.TransactionNumber;

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('Cash & Bank Receipts details are successfully inserted');", true);

                grvCashAndBankReceipt.Enabled = false;
                BtnSubmit.Enabled = false;
                txtRemarks.Enabled = false;
                txtTransactionAmount.Enabled = false;
                txtChartOfAccount.Enabled = false;
                BankAccNo.Visible = false;
                ddlPayment.Enabled = false;
                ddlModeOfReceipt.Enabled = false;
                txtChequeNumber.Enabled = false;
                txtChequeDate.Enabled = false;
                ddlCBReceiptType.Enabled = false;
                ddlCustomer.Enabled = false;
                txtBank.Enabled = false;
                txtBankBranch.Enabled = false;
                ddlLocalOutstation.Enabled = false;
                txtReferenceDate.Enabled = false;
                btnReport.Enabled = true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('" + cashandbankEntity.ErrorMsg + "');", true);
            }
        }

        private void fnGetCashAndBankReceipt(string strTransactionNumber)
        {
            CashAndBankTransactions cashandbankTransactions = new CashAndBankTransactions();
            CashAndBankEntity cashandbankEntity = cashandbankTransactions.GetCashBankDetails(strBranchCode, strTransactionNumber);

            LoadAccountintPeriodView();

            txtTransactionNumber.Text = strTransactionNumber;
            txtTransactionDate.Text = cashandbankEntity.TransactionDate;
            txtRemarks.Text = cashandbankEntity.Remarks;
            txtTransactionAmount.Text = cashandbankEntity.TransactionAmount;
            txtChartOfAccount.Text = cashandbankEntity.ChartOfAccountCode;
            ddlPayment.SelectedValue = cashandbankEntity.ReceiptPaymentIndicator;
            ddlAccountingPeriod.SelectedValue = cashandbankEntity.Accounting_Period;

            fnPopulateDropDown("CBPCashCheque", ddlModeOfReceipt);
            ddlModeOfReceipt.SelectedValue = cashandbankEntity.CashChequeIndicator;

            txtChequeNumber.Text = cashandbankEntity.Cheque_Number;
            txtChequeDate.Text = cashandbankEntity.Cheque_Date;
            txtBank.Text = cashandbankEntity.Cheque_Bank;
            txtBankBranch.Text = cashandbankEntity.Cheque_Branch;
            ddlLocalOutstation.SelectedValue = cashandbankEntity.Local_Outstation == "" ? "L" : cashandbankEntity.Local_Outstation;
            txtReferenceDate.Text = cashandbankEntity.Ref_Date;

            if (cashandbankEntity.Remarks.Contains("AUTO CHEQUESLIP"))
            {
                ddlCustomer.SelectedValue = cashandbankEntity.Items[0].Chart_of_Account_Code.Substring(10, 7);
                grvCashAndBankReceipt.DataSource = null;
                grvCashAndBankReceipt.DataBind();
                grvCashAndBankReceipt.Visible = false;
                divItemHeader.Visible= false;
                divScrollFullPage.Visible = false;
            }
            else
            {
                ddlCustomer.SelectedIndex = 0;
                divItemHeader.Visible = true;
                divScrollFullPage.Visible = true;

                grvCashAndBankReceipt.Visible = true;
                grvCashAndBankReceipt.DataSource = (object)cashandbankEntity.Items;
                grvCashAndBankReceipt.DataBind();

                foreach (GridViewRow gr in grvCashAndBankReceipt.Rows)
                {
                    gr.Enabled = false;
                    ChartAccount coA = (ChartAccount)gr.FindControl("COA");
                    coA.Visible = false;
                }
            }
        }

        private void fnPopulateTransactionNumber(string strBranchCode)
        {
            CashAndBankTransactions cashandbankTransactions = new CashAndBankTransactions();
            
            try
            {
                ddlTransactionNumber.DataSource = cashandbankTransactions.GetTransactionNumber(strBranchCode,"R");
                ddlTransactionNumber.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(CashAndBankReceiptNew), exp);
            }
        }

        private void AddNewRowToGrid()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                int rowIndex = 0;

                if (ViewState["CurrentTable"] != null)
                {
                    DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                    DataRow drCurrentRow = null;
                    int RowNumber = 0;
                    if (dtCurrentTable.Rows.Count > 0)
                    {
                        for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                        {
                            TextBox txtGrdChartOfAccount = (TextBox)grvCashAndBankReceipt.Rows[rowIndex].Cells[1].FindControl("txtGrdChartOfAccount");
                            TextBox txtGrdAmount = (TextBox)grvCashAndBankReceipt.Rows[rowIndex].Cells[2].FindControl("txtGrdAmount");
                            TextBox txtGrdRemarks = (TextBox)grvCashAndBankReceipt.Rows[rowIndex].Cells[3].FindControl("txtGrdRemarks");

                            dtCurrentTable.Rows[i - 1]["Chart_of_Account_Code"] = txtGrdChartOfAccount.Text;
                            dtCurrentTable.Rows[i - 1]["Amount"] = txtGrdAmount.Text;
                            dtCurrentTable.Rows[i - 1]["Remarks"] = txtGrdRemarks.Text;
                            RowNumber = i + 1;
                            rowIndex++;
                        }

                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow = GetInitialRow(RowNumber, drCurrentRow);
                        dtCurrentTable.Rows.Add(drCurrentRow);
                        ViewState["CurrentTable"] = dtCurrentTable;
                        grvCashAndBankReceipt.DataSource = dtCurrentTable;
                        grvCashAndBankReceipt.DataBind();
                    }
                }
                else
                {
                    Response.Write("ViewState is null");
                }

                //Set Previous Data on Postbacks
                SetPreviousData();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void SetInitialRow()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                DataTable dt = new DataTable();
                DataRow dr = null;
                dt.Columns.Add(new DataColumn("Chart_of_Account_Code", typeof(string)));
                dt.Columns.Add(new DataColumn("Amount", typeof(string)));
                dt.Columns.Add(new DataColumn("Remarks", typeof(string)));

                dr = dt.NewRow();
                dr["Chart_of_Account_Code"] = string.Empty;
                dr["Amount"] = string.Empty;
                dr["Remarks"] = string.Empty;
                dt.Rows.Add(dr);
                //dr = dt.NewRow();

                ViewState["CurrentTable"] = dt;
                grvCashAndBankReceipt.DataSource = dt;
                grvCashAndBankReceipt.DataBind();

                grvCashAndBankReceipt.Visible = true;
                grvCashAndBankReceipt.Enabled = true;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                AddNewRowToGrid();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void SetPreviousData()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                int rowIndex = 0;
                if (ViewState["CurrentTable"] != null)
                {
                    DataTable dt1 = (DataTable)ViewState["CurrentTable"];
                    if (dt1.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            ChartAccount COA = (ChartAccount)grvCashAndBankReceipt.Rows[rowIndex].Cells[1].FindControl("COA");
                            TextBox txtGrdChartAccount = (TextBox)grvCashAndBankReceipt.Rows[rowIndex].Cells[1].FindControl("txtGrdChartOfAccount");
                            TextBox txtGrdAmount = (TextBox)grvCashAndBankReceipt.Rows[rowIndex].Cells[2].FindControl("txtGrdAmount");
                            TextBox txtGrdRemarks = (TextBox)grvCashAndBankReceipt.Rows[rowIndex].Cells[3].FindControl("txtGrdRemarks");

                            txtGrdChartAccount.Text = dt1.Rows[i]["Chart_of_Account_Code"].ToString();
                            txtGrdAmount.Text = dt1.Rows[i]["Amount"].ToString();
                            txtGrdRemarks.Text = dt1.Rows[i]["Remarks"].ToString();
                            rowIndex++;

                            if (i < dt1.Rows.Count - 1)
                            {
                                COA.Visible = false;
                                txtGrdAmount.Enabled = false;
                            }
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private DataRow GetInitialRow(int RowNumber, DataRow dr)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                dr["Chart_of_Account_Code"] = string.Empty;
                dr["Amount"] = string.Empty;
                dr["Remarks"] = string.Empty;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return dr;
        }

        protected void btnTransactionDetails_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            DataTable pdtCashAndBankReceiptNew = new DataTable();
            try
            {
                grvCashAndBankReceipt.Visible = true;
                grvCashAndBankReceipt.Enabled = true;
                ddlCBReceiptType.Enabled = false;
                btnTransactionDetails.Visible = false;
                BtnSubmit.Visible = true;
                BtnSubmit.Enabled = true;
                txtRemarks.Enabled = false;
                txtTransactionAmount.Enabled = false;
                txtChartOfAccount.Enabled = false;
                BankAccNo.Visible = false;
                ddlPayment.Enabled = false;
                ddlModeOfReceipt.Enabled = false;

                SetInitialRow();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            UpdpanelTop.Visible = false;
            btnBack.Visible = true;

            string strSelectionFormula = default(string);
            string strCryTransDate = default(string);
            string strTransNumbField = default(string);
            string strTransDateField = default(string);
            string strReportName = default(string);
            string strReceiptSupp = default(string);

            string strTransactionNumber = ddlTransactionNumber.SelectedValue;
            string strTransactionDate = txtTransactionDate.Text;
            string strPayment = ddlPayment.SelectedValue;
            string strChartOfAccount = txtChartOfAccount.Text;

            #region Selection Formula Formation
            if (!string.IsNullOrEmpty(strTransactionDate))
                strCryTransDate = "Date (" + DateTime.ParseExact(strTransactionDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
            if (!string.IsNullOrEmpty(strChartOfAccount))
            {
                strReceiptSupp = strChartOfAccount.Substring(7, 4);
            }
            //if Payment is Receipt && Chart of Account Code is empty and Transaction Number is null
            if (strPayment == "R" && string.IsNullOrEmpty(strChartOfAccount) && string.IsNullOrEmpty(strTransactionNumber))
            {
                if (strBranchCode.ToUpper() == "COR")
                    strReportName = "MainCashCor";
                else
                    strReportName = "MainCash";

                strTransDateField = "{View_MainCash.Transaction_date}";

                strSelectionFormula = strTransDateField + "=" + " " + "" + strCryTransDate + "";
            }
            //if Payment is Receipt && Chart of Account is  selected and Transaction Number is null
            if (strPayment == "R" && !string.IsNullOrEmpty(strChartOfAccount) && string.IsNullOrEmpty(strTransactionNumber))
            {
                if (strReceiptSupp == "0060")
                {
                    strReportName = "MainCashSupplier";
                }
                else
                {
                    if (strBranchCode.ToUpper() == "COR")
                        strReportName = "MainCashCor";
                    else
                        strReportName = "MainCash";
                }
                if (strReportName == "MainCash" || strReportName == "MainCashCor")
                {
                    strTransDateField = "{View_MainCash.Transaction_date}";
                }
                else
                {
                    strTransDateField = "{Main_Cash_Header.Transaction_date}";
                }
                strSelectionFormula = strTransDateField + "=" + " " + "" + strCryTransDate + "";

            }
            //if Payment is Receipt and Chart of Account Code is selected and Transaction Number is not null
            if (strPayment == "R" && !string.IsNullOrEmpty(strChartOfAccount) && !string.IsNullOrEmpty(strTransactionNumber))
            {
                if (strReceiptSupp == "0060")
                {
                    strReportName = "MainCashSupplier";
                }
                else
                {
                    if (strBranchCode.ToUpper() == "COR")
                        strReportName = "MainCashCor";
                    else
                        strReportName = "MainCash";
                }
                if (strReportName == "MainCash" || strReportName == "MainCashCor")
                {
                    strTransNumbField = "{View_MainCash.Transaction_Number}";
                }
                else
                {
                    strTransNumbField = "{Main_Cash_Header.Transaction_Number}";
                }
                strSelectionFormula = strTransNumbField + "=" + " " + "'" + strTransactionNumber + "'";

            }
            #endregion

            strSelectionFormula = strSelectionFormula + " and {View_MainCash.Branch_Code}='" + strBranchCode + "'";

            crCashAndBank.ReportName = strReportName;
            crCashAndBank.RecordSelectionFormula = strSelectionFormula;
            crCashAndBank.GenerateReportAndExportInvoiceA4(strTransactionNumber.Replace("/", "-"), 2);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Server.ClearError();
                Response.Redirect("CashAndBankReceiptNew.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private string TwoDecimalConversion(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
                return "0.00";
            else
                return string.Format("{0:0.00}", Convert.ToDecimal(strValue));
        }
    }
}