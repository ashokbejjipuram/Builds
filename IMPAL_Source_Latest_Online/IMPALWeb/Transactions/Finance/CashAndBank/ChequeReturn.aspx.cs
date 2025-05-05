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
    public partial class ChequeReturn : System.Web.UI.Page
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
                Log.WriteException(typeof(ChequeReturn), exp);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = (string)Session["BranchCode"];

                if (!IsPostBack)
                {
                    if (crCashAndBank != null)
                    {
                        crCashAndBank.Dispose();
                        crCashAndBank = null;
                    }

                    hdnIndicator.Value = "";
                    ddlTransactionNumber.Visible = false;
                    fnPopulateDropDown("CBPPaymentsCHQreturn", ddlPayment);
                    txtBranchName.Text = fnGetBranch(strBranchCode);
                    LoadAccountintPeriod();
                    txtAccountingPeriod.Attributes.Add("style", "display:none");
                    grvChequeReturn.Visible = false;
                    BtnSubmit.Visible = false;
                    ddlPayment.Enabled = false;
                    fnPopulateDropDown("LocalOutStation", ddlLocalOutstation);
                    txtBranchCode.Text = Session["BranchCode"].ToString();
                    fnPopulateDropDown("CBPPaymentsCHQreturnRemarks", ddlRemarks);
                    ddlRemarks.Visible = true;
                    txtRemarks.Attributes.Add("style", "display:none");
                    txtRemarks.Text = "Cheque Return";

                    DateItem PrevDateStatus = objReceivableInvoice.GetPreviousDateStatus(Session["UserID"].ToString(), "ChequeReturn");

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

                if (grvChequeReturn.Visible == false)
                {
                    BtnSubmit.Visible = false;
                }
                else
                {
                    BtnSubmit.Visible = true;
                }
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

            int PrevFinYearStatus = objReceivableInvoice.GetPreviousAccountingPeriodStatus(Session["UserID"].ToString(), "CBPayment");

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
            btnReport.Enabled = false;
        }

        protected void gvResults_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            BtnSubmit.Visible = false;
            btnReport.Enabled = true;
            ddlAccountingPeriod.Enabled = false;

            try
            {
                SubmitChequeReturn();
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
                Response.Redirect("ChequeReturn.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                Log.WriteException(Source, ex);
            }
        }

        protected void ddlTransactionNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            if (ddlTransactionNumber.SelectedIndex > 0)
            {
                fnGetChequeReturn(ddlTransactionNumber.SelectedValue);
                grvChequeReturn.Visible = true;
                grvChequeReturn.Enabled = false;
                ddlAccountingPeriod.Enabled = false;
                ddlRemarks.Enabled = false;                
                txtRemarks.Enabled = false;
                ddlPayment.Enabled = false;
                BtnSubmit.Visible = false;
                btnReport.Enabled = true;
            }
            else
            {
                Server.ClearError();
                Response.Redirect("ChequeReturn.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
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

        private string fnGetBranch(string strBranchCode)
        {
            CashAndBankTransactions cashandbankTransactions = new CashAndBankTransactions();
            try
            {
                return cashandbankTransactions.GetBranchName(strBranchCode);
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ChequeReturn), exp);
            }
            return cashandbankTransactions.GetBranchName(strBranchCode);
        }

        protected void txtChequeNumber_OnTextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtChequeNumber.Text.Trim() != null && txtChequeNumber.Text.Trim() != "")
                {
                    ddlReceiptNumber.DataSource = (object)GetReceiptDetails();
                    ddlReceiptNumber.DataTextField = "ItemDesc";
                    ddlReceiptNumber.DataValueField = "ItemCode";
                    ddlReceiptNumber.DataBind();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesInvoiceEntry), exp);
            }
        }

        private List<IMPALLibrary.Transactions.Item> GetReceiptDetails()
        {
            List<IMPALLibrary.Transactions.Item> obj = objTrans.GetExistingChequeReceiptDetails(strBranchCode, ddlCustomer.SelectedValue, txtChequeNumber.Text, ddlAccountingPeriod.SelectedValue);
            return obj;
        }

        protected void ddlReceiptNumber_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string[] RecNumArr = ddlReceiptNumber.SelectedValue.Split('_');

                string receiptNumber = RecNumArr[0].ToString();
                string indicator = RecNumArr[1].ToString();
                hdnIndicator.Value = indicator;

                ReceiptTransactions receiptTransactions = new ReceiptTransactions();
                ReceiptEntity receiptEntity = receiptTransactions.GetReceiptsDetailsByNumberChqReturn(Session["BranchCode"].ToString(), ddlCustomer.SelectedValue, receiptNumber, indicator);

                txtReceiptDate.Text = receiptEntity.ReceiptDate;
                ddlCustomer.SelectedValue = receiptEntity.CustomerCode;
                txtChequeAmount.Text = receiptEntity.Amount;

                txtChequeNumber.Text = receiptEntity.ChequeNumber;
                txtChequeDate.Text = receiptEntity.ChequeDate;
                txtBank.Text = receiptEntity.ChequeBank;
                txtBranch.Text = receiptEntity.ChequeBranch;

                //BindExistingRows(receiptEntity.Items);

                grvChequeReturn.DataSource = (object)receiptEntity.Items;
                grvChequeReturn.DataBind();
                grvChequeReturn.Columns[0].Visible = false;

                foreach (GridViewRow gr in grvChequeReturn.Rows)
                {
                    gr.Enabled = false;
                }

                grvChequeReturn.Visible = true;

                ddlAccountingPeriod.Enabled = false;
                ddlCustomer.Enabled = false;
                txtChequeAmount.Enabled = false;
                BtnSubmit.Enabled = false;
                btnReset.Enabled = true;
                btnReport.Enabled = true;
                
                txtChequeNumber.Enabled = false;
                txtChequeDate.Enabled = false;
                txtBank.Enabled = false;
                txtBranch.Enabled = false;
                BtnSubmit.Visible = true;
                BtnSubmit.Enabled = true;
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(Receipts), exp);
            }
        }

        protected void ddlCustomer_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                imgEditToggle.Visible = false;

                if (ddlCustomer.SelectedValue == "0")
                    return;

                ReceivableInvoice receivableReceipt = new ReceivableInvoice();
                Customer customer = receivableReceipt.GetCustomerInfoByCustomerCode(ddlCustomer.SelectedValue, Session["BranchCode"].ToString());
                
                txtChequeAmount.Text = string.Empty;
                txtChequeNumber.Text = string.Empty;
                txtChequeDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtBank.Text = string.Empty;
                txtBranch.Text = string.Empty;
                txtCode.Text = customer.Customer_Code;
                txtLocation.Text = customer.Location;
                txtAddress1.Text = customer.address1;
                txtAddress2.Text = customer.address2;
                txtAddress3.Text = customer.address3;
                txtAddress4.Text = customer.address4;
            }
            catch (Exception exp)
            {

                Log.WriteException(typeof(Receipts), exp);
            }
        }

        private void SubmitChequeReturn()
        {
            CashAndBankEntity cashandbankEntity = new CashAndBankEntity();
            cashandbankEntity.Items = new List<CashAndBankItem>();

            cashandbankEntity.TransactionNumber = txtTransactionNumber.Text;
            cashandbankEntity.TransactionDate = txtTransactionDate.Text;
            cashandbankEntity.TransactionAmount = txtChequeAmount.Text;
            cashandbankEntity.BankCharges = txtBankCharges.Text;
            cashandbankEntity.ReceiptPaymentIndicator = "P";
            cashandbankEntity.CashChequeIndicator = "Q";
            cashandbankEntity.CustomerCode = txtCode.Text;
            cashandbankEntity.BranchCode = strBranchCode;
            cashandbankEntity.Remarks = ddlPayment.SelectedItem.Text + " - " + ddlRemarks.SelectedValue;
            cashandbankEntity.RefNo = ddlReceiptNumber.SelectedItem.Text;
            cashandbankEntity.Ref_Date = txtReceiptDate.Text;
            cashandbankEntity.Indicator = hdnIndicator.Value;            

            CashAndBankItem cashandbankItem = new CashAndBankItem();
            cashandbankItem.Serial_Number = "1";
            cashandbankItem.Chart_of_Account_Code = "4070330220" + ddlCustomer.SelectedValue + strBranchCode;
            cashandbankItem.Remarks = ddlReceiptNumber.SelectedValue;
            cashandbankItem.Amount = txtChequeAmount.Text;
            cashandbankEntity.Items.Add(cashandbankItem);

            CashAndBankTransactions cashandbankTransactions = new CashAndBankTransactions();
            int result = cashandbankTransactions.AddChequeReturnDetails(ref cashandbankEntity, "");

            if ((cashandbankEntity.ErrorMsg == string.Empty) && (cashandbankEntity.ErrorCode == "0"))
            {
                txtTransactionNumber.Text = cashandbankEntity.TransactionNumber;

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('Cheque Return Details are successfully inserted');", true);

                grvChequeReturn.Enabled = false;
                BtnSubmit.Enabled = false;
                imgEditToggle.Visible = false;
                txtRemarks.Enabled = false;
                ddlPayment.Enabled = false;
                txtChequeNumber.Enabled = false;
                txtChequeDate.Enabled = false;
                txtBank.Enabled = false;
                txtBranch.Enabled = false;
                ddlLocalOutstation.Enabled = false;
                btnReport.Enabled = true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('" + cashandbankEntity.ErrorMsg + "');", true);
            }
        }

        private void fnGetChequeReturn(string strTransactionNumber)
        {
            CashAndBankTransactions cashandbankTransactions = new CashAndBankTransactions();
            CashAndBankEntity cashandbankEntity = cashandbankTransactions.GetCashBankDetailsChqReturn(strBranchCode, strTransactionNumber);
            ddlReceiptNumber.Items.Clear();

            LoadAccountintPeriodView();

            txtTransactionNumber.Text = strTransactionNumber;
            txtTransactionDate.Text = cashandbankEntity.TransactionDate;
            txtRemarks.Text = cashandbankEntity.Remarks;
            //ddlPayment.SelectedValue = cashandbankEntity.ReceiptPaymentIndicator;

            txtChequeNumber.Text = cashandbankEntity.Cheque_Number;
            txtChequeDate.Text = cashandbankEntity.Cheque_Date;
            txtBank.Text = cashandbankEntity.Cheque_Bank;
            txtBranch.Text = cashandbankEntity.Cheque_Branch;
            ddlLocalOutstation.SelectedValue = cashandbankEntity.Local_Outstation == "" ? "L" : cashandbankEntity.Local_Outstation;
            ddlCustomer.SelectedValue = cashandbankEntity.CustomerCode;
            ddlReceiptNumber.Items.Insert(0, cashandbankEntity.RefNo);
            txtReceiptDate.Text = cashandbankEntity.Ref_Date;
            txtChequeAmount.Text = cashandbankEntity.TransactionAmount;
            txtBankCharges.Text = cashandbankEntity.BankCharges;
            txtCode.Text = cashandbankEntity.CustomerCode;
            txtLocation.Text = cashandbankEntity.Location;
            txtAddress1.Text = cashandbankEntity.address1;
            txtAddress2.Text = cashandbankEntity.address2;
            txtAddress3.Text = cashandbankEntity.address3;
            txtAddress4.Text = cashandbankEntity.address4;

            grvChequeReturn.DataSource = (object)cashandbankEntity.Items;
            grvChequeReturn.DataBind();
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
                grvChequeReturn.Visible = false;
                btnReport.Visible = true;
                fnPopulateTransactionNumber(strBranchCode);
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
                txtTransactionDate.Enabled = false;
                ddlRemarks.Visible = false;
                txtRemarks.Attributes.Add("style", "display:inline");
                txtRemarks.Text = "";
                txtRemarks.Enabled = false;
                txtTransactionNumber.Enabled = false;
                ddlPayment.Enabled = false;
                txtChequeNumber.Enabled = false;
                txtChequeDate.Enabled = false;
                txtBank.Enabled = false;
                txtBranch.Enabled = false;
                ddlLocalOutstation.Enabled = false;
                ddlCustomer.Enabled = false;
                ddlReceiptNumber.Enabled = false;
                txtChequeAmount.Enabled = false;
                txtBankCharges.Enabled = false;
                grvChequeReturn.Enabled = false;
                BtnSubmit.Enabled = false;
                imgEditToggle.Visible = false;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void fnPopulateTransactionNumber(string strBranchCode)
        {
            CashAndBankTransactions cashandbankTransactions = new CashAndBankTransactions();

            try
            {
                ddlTransactionNumber.DataSource = cashandbankTransactions.GetTransactionNumberChqReturn(strBranchCode, "P", "Cheque Return - ");
                ddlTransactionNumber.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ChequeReturn), exp);
            }
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            UpdpanelTop.Visible = false;
            GenerateSelectionFormula();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Server.ClearError();
                Response.Redirect("ChequeReturn.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void GenerateSelectionFormula()
        {
            string strSelectionFormula = default(string);
            string strCryTransDate = default(string);
            string strTransNumbField = default(string);
            string strTransDateField = default(string);
            string strReportName = default(string);

            string strTransactionNumber = ddlTransactionNumber.SelectedValue;
            string strTransactionDate = txtTransactionDate.Text;
            string strPayment = ddlPayment.SelectedValue;

            if (strBranchCode.ToUpper() == "COR")
                strReportName = "MainChequeSlipCor";
            else
                strReportName = "MainChequeSlip";

            strSelectionFormula = strSelectionFormula + "{Main_Cash_Header.Branch_Code}='" + strBranchCode + "' and {Main_Cash_Header.Receipt_Payment_Indicator} = 'P' and ";

            if (string.IsNullOrEmpty(strTransactionNumber))
            {
                if (!string.IsNullOrEmpty(strTransactionDate))
                    strCryTransDate = "Date (" + DateTime.ParseExact(strTransactionDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";

                strTransDateField = "{Main_Cash_Header.Transaction_date}";
                strSelectionFormula = strSelectionFormula + strTransDateField + "=" + strCryTransDate + "";
            }
            else
            {
                strTransNumbField = "{Main_Cash_Header.Transaction_Number}";
                strSelectionFormula = strSelectionFormula + strTransNumbField + "='" + strTransactionNumber + "'";
            }

            crCashAndBank.ReportName = strReportName;
            crCashAndBank.RecordSelectionFormula = strSelectionFormula;
            crCashAndBank.GenerateReportAndExportA4();
        }
    }
}