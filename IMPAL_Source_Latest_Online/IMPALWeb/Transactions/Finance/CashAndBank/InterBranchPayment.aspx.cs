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
using static IMPALLibrary.ReceivableInvoice;
using System.Globalization;


#endregion Namespace

namespace IMPALWeb.Finance
{
    public partial class InterBranchPayment : System.Web.UI.Page
    {
        private string strBranchCode;
        private AccountingPeriods Acc = new AccountingPeriods();
        private ReceivableInvoice objReceivableInvoice = new ReceivableInvoice();

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(InterBranchPayment), exp);
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

                if (!IsPostBack)
                {
                    if (crCashAndBank != null)
                    {
                        crCashAndBank.Dispose();
                        crCashAndBank = null;
                    }

                    Session.Remove("BranchCodeOthers");
                    Session["BranchCodeOthers"] = null;
                    BankAccNo.Visible = false;

                    ddlTransactionNumber.Visible = false;
                    txtNoOfTransactions.Attributes.Add("onkeypress", "return IntegerValueOnly();");
                    txtTransactionAmount.Attributes.Add("OnChange", "return ValidAmountLimit('" + Session["UserID"].ToString() + "');");
                    fnPopulateDropDown("CBPPaymentsOthers", ddlPayment);
                    fnPopulateDropDown("CBPCashChequeOthers", ddlCashCheque);
                    txtBranch.Text = fnGetBranch(strBranchCode);
                    LoadAccountintPeriod();
                    txtAccountingPeriod.Attributes.Add("style", "display:none");
                    grvInterBranchPayment.Visible = false;
                    BtnSubmit.Visible = false;
                    ddlCashCheque.Attributes.Add("OnChange", "funModeOfPayment();");
                    fnPopulateDropDown("LocalOutStation", ddlLocalOutstation);

                    ddlBranchOthers.Enabled = true;
                    Branches oBranch = new Branches();
                    //ddlBranchOthers.DataSource = oBranch.GetAllBranchesCBPayment(Session["BranchCode"].ToString());
                    ddlBranchOthers.DataSource = oBranch.GetChqIssueBranchesInterBranch(Session["BranchCode"].ToString());
                    ddlBranchOthers.DataBind();

                    if (ddlBranchOthers.Items.Count > 1)
                    {
                        ddlBranchOthers.Items.Insert(0, new ListItem("-- Select --", "0"));
                    }
                    
                    ddlBranchOthers.SelectedIndex = 0;

                    DateItem PrevDateStatus = objReceivableInvoice.GetPreviousDateStatus(Session["UserID"].ToString(), "CBPaymentsOthers");

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

                if (Session["BranchCode"].ToString() == "COR")
                {
                    BankAccNo.Filter = "009,047,049";
                }
                else
                {
                    BankAccNo.Filter = "047,049";
                }

                if (ddlTransactionNumber.Visible == false)
                {
                    if (ddlCashCheque.SelectedValue == "Q")
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

                if (grvInterBranchPayment.Visible == false)
                {
                    BtnSubmit.Visible = false;
                    btnReportTransaction.Visible = true;
                }
                else
                {
                    BtnSubmit.Visible = true;
                    btnReportTransaction.Visible = false;
                }

                if (ddlBranchOthers.SelectedIndex > 0)
                    Session["BranchCodeOthers"] = ddlBranchOthers.SelectedValue.Substring(0, 3);
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

        protected void ddlBranchOthers_IndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (ddlBranchOthers.SelectedIndex > 0)
                    BankAccNo.Visible = true;
                else
                    BankAccNo.Visible = false;

                ddlBranchOthers.Enabled = false;
                Session["BranchCodeOthers"] = ddlBranchOthers.SelectedValue;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
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
            ddlCashCheque.Enabled = false;
            try
            {
                Session.Remove("BranchCodeOthers");
                Session["BranchCodeOthers"] = null;
                SubmitInterBranchPayment();
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
                Session.Remove("BranchCodeOthers");
                Session["BranchCodeOthers"] = null;

                Server.ClearError();
                Response.Redirect("InterBranchPayment.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                Log.WriteException(Source, ex);
            }
        }

        protected void ucChartAccount_SearchImageClicked(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                txtChartOfAccount.Text = Session["ChatAccCode"].ToString();
                Session["ChatAccCode"] = "";
                ImpalLibrary lib = new ImpalLibrary();
                List<DropDownListValue> drop = new List<DropDownListValue>();
                drop = lib.GetDropDownListValues("CBPCashChequeOthers");
                if (Session["ChatDescription"].ToString() == "CASH ON HAND")
                {
                    ddlCashCheque.DataSource = drop.Where(p => p.DisplayValue == "H").ToList(); ;
                    ddlCashCheque.DataValueField = "DisplayValue";
                    ddlCashCheque.DataTextField = "DisplayText";
                    ddlCashCheque.DataBind();

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

                    ddlCashCheque.DataSource = drop.Where(p => p.DisplayValue != "H").ToList();
                    ddlCashCheque.DataValueField = "DisplayValue";
                    ddlCashCheque.DataTextField = "DisplayText";
                    ddlCashCheque.DataBind();
                }

                if (ddlTransactionNumber.Visible == false)
                {
                    if (ddlPayment.SelectedValue == "F")
                    {
                        if (Session["ChatDescription"].ToString() == "CASH ON HAND")
                            ddlCashCheque.SelectedValue = "H";
                        else
                            ddlCashCheque.SelectedValue = "D";
                    }
                    else if (ddlPayment.SelectedValue == "P" && Session["ChatDescription"].ToString() == "CASH ON HAND")
                        ddlCashCheque.SelectedValue = "H";
                    else
                        ddlCashCheque.SelectedValue = "Q";

                    if (ddlCashCheque.SelectedValue == "Q")
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
                GridViewRow gvr = (GridViewRow)((IMPALWeb.UserControls.ChartAccountOtherBranches)sender).Parent.Parent;
                ((TextBox)gvr.FindControl("txtChartOfAccount")).Text = Session["ChatAccCode"].ToString();
                Session["ChatAccCode"] = "";
                ((TextBox)gvr.FindControl("txtAmount")).Focus();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ddlTransactionNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            fnGetInterBranchPayment(ddlTransactionNumber.SelectedValue);
            grvInterBranchPayment.Visible = true;
            grvInterBranchPayment.Enabled = false;
            ddlAccountingPeriod.Enabled = false;
            BankAccNo.Visible = false;
            txtChartOfAccount.Enabled = false;
            txtNoOfTransactions.Enabled = false;
            txtTransactionAmount.Enabled = false;
            txtRemarks.Enabled = false;
            ddlPayment.Enabled = false;
            ddlCashCheque.Enabled = false;
            lblNoOfTransactions.Visible = false;
            txtNoOfTransactions.Visible = false;
            idspan.Visible = false;
            btnReportTransaction.Visible = false;
            BtnSubmit.Visible = false;
            btnReport.Enabled = true;
        }

        protected void ddlPayment_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            ddlPayment.Enabled = false;
            ddlAccountingPeriod.Enabled = false;
            try
            {
                if (ddlPayment.SelectedValue == "C")
                {
                    if (Session["BranchCode"].ToString() == "COR")
                    {
                        BankAccNo.Filter = "009,047,049";
                    }
                    else
                    {
                        BankAccNo.Filter = "047,049";
                    }

                    ddlCashCheque.SelectedValue = "Q";
                }
                else
                {
                    if (ddlPayment.SelectedValue == "F")
                        ddlCashCheque.SelectedValue = "D";
                    else
                        ddlCashCheque.SelectedValue = "H";

                    if (Session["BranchCode"].ToString() == "COR")
                    {
                        BankAccNo.Filter = "009,047,049";
                    }
                    else
                    {
                        BankAccNo.Filter = "047,049";
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
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
                Log.WriteException(typeof(InterBranchPayment), exp);
            }
            return cashandbankTransactions.GetBranchName(strBranchCode);
        }

        private void SubmitInterBranchPayment()
        {
            CashAndBankEntity cashandbankEntity = new CashAndBankEntity();
            cashandbankEntity.Items = new List<CashAndBankItem>();

            cashandbankEntity.TransactionNumber = txtTransactionNumber.Text;
            cashandbankEntity.TransactionDate = txtTransactionDate.Text;
            cashandbankEntity.BranchCode = ddlBranchOthers.SelectedValue;
            cashandbankEntity.Remarks = ddlPayment.SelectedItem.Text + " - " + txtRemarks.Text;
            cashandbankEntity.ChartOfAccountCode = txtChartOfAccount.Text;
            cashandbankEntity.ReceiptPaymentIndicator = "P"; //ddlPayment.SelectedValue;
            cashandbankEntity.CashChequeIndicator = ddlCashCheque.SelectedValue;
            cashandbankEntity.TransactionAmount = txtTransactionAmount.Text == "" ? "0.00" : txtTransactionAmount.Text;
            cashandbankEntity.Cheque_Number = txtChequeNumber.Text;
            cashandbankEntity.Cheque_Date = txtChequeDate.Text;
            cashandbankEntity.Cheque_Bank = txtBank.Text;
            cashandbankEntity.Cheque_Branch = txtBankBranch.Text;
            cashandbankEntity.Local_Outstation = ddlLocalOutstation.SelectedValue;
            cashandbankEntity.Ref_Date = txtReferenceDate.Text;
            cashandbankEntity.PaymentBranch = Session["BranchCode"].ToString();

            CashAndBankItem cashandbankItem = null;
            int intCount = 0;
            int intVisible = 0;

            int rowIndex = Convert.ToInt16(RowNum.Value);

            foreach (GridViewRow grvRow in grvInterBranchPayment.Rows)
            {
                cashandbankItem = new CashAndBankItem();

                if (!string.IsNullOrEmpty(((TextBox)grvRow.Cells[1].FindControl("txtChartOfAccount")).Text))
                {
                    intCount += 1;
                    cashandbankItem.Serial_Number = intCount.ToString();
                    cashandbankItem.Chart_of_Account_Code = ((TextBox)grvRow.Cells[1].FindControl("txtChartOfAccount")).Text;
                    cashandbankItem.Amount = ((TextBox)grvRow.Cells[2].FindControl("txtAmount")).Text == "" ? "0.00" : ((TextBox)grvRow.Cells[2].FindControl("txtAmount")).Text;
                    cashandbankItem.Remarks = ((TextBox)grvRow.Cells[3].FindControl("txtRemarks")).Text;

                    cashandbankEntity.Items.Add(cashandbankItem);
                }
            }

            CashAndBankTransactions cashandbankTransactions = new CashAndBankTransactions();
            int result = cashandbankTransactions.AddCashBankDetailsInterBranch(ref cashandbankEntity, "");

            if ((cashandbankEntity.ErrorMsg == string.Empty) && (cashandbankEntity.ErrorCode == "0"))
            {
                txtTransactionNumber.Text = cashandbankEntity.TransactionNumber;

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('Inter Branch Payment details are successfully inserted');", true);

                grvInterBranchPayment.Enabled = false;
                BtnSubmit.Enabled = false;
                txtRemarks.Enabled = false;
                txtTransactionAmount.Enabled = false;
                txtChartOfAccount.Enabled = false;
                BankAccNo.Visible = false;
                ddlPayment.Enabled = false;
                ddlCashCheque.Enabled = false;
                txtNoOfTransactions.Enabled = false;
                txtChequeNumber.Enabled = false;
                txtChequeDate.Enabled = false;
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

        private void fnGetInterBranchPayment(string strTransactionNumber)
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
            ddlCashCheque.SelectedValue = cashandbankEntity.CashChequeIndicator;

            txtChequeNumber.Text = cashandbankEntity.Cheque_Number;
            txtChequeDate.Text = cashandbankEntity.Cheque_Date;
            txtBank.Text = cashandbankEntity.Cheque_Bank;
            txtBankBranch.Text = cashandbankEntity.Cheque_Branch;
            ddlLocalOutstation.SelectedValue = cashandbankEntity.Local_Outstation == "" ? "L" : cashandbankEntity.Local_Outstation;
            txtReferenceDate.Text = cashandbankEntity.Ref_Date;

            grvInterBranchPayment.DataSource = (object)cashandbankEntity.Items;
            grvInterBranchPayment.DataBind();

            foreach (GridViewRow gr in grvInterBranchPayment.Rows)
            {
                gr.Enabled = false;
                ChartAccount coA = (ChartAccount)gr.FindControl("COA");
                coA.Visible = false;
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
                grvInterBranchPayment.Visible = false;
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
                txtRemarks.Enabled = false;
                txtTransactionNumber.Enabled = false;
                txtTransactionAmount.Enabled = false;
                ddlPayment.Enabled = false;
                ddlCashCheque.Enabled = false;
                txtNoOfTransactions.Enabled = false;
                txtChequeNumber.Enabled = false;
                txtChequeDate.Enabled = false;
                txtBank.Enabled = false;
                txtBankBranch.Enabled = false;
                ddlLocalOutstation.Enabled = false;
                txtReferenceDate.Enabled = false;
                grvInterBranchPayment.Enabled = false;
                BtnSubmit.Enabled = false;
                imgEditToggle.Visible = false;
                BankAccNo.Visible = false;
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
                ddlTransactionNumber.DataSource = cashandbankTransactions.GetTransactionNumber(strBranchCode, "P");
                ddlTransactionNumber.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(InterBranchPayment), exp);
            }
        }

        protected void btnReportTransaction_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            DataTable pdtInterBranchPayment = new DataTable();
            try
            {
                grvInterBranchPayment.Visible = true;
                grvInterBranchPayment.Enabled = true;
                btnReportTransaction.Visible = false;
                BtnSubmit.Visible = true;
                BtnSubmit.Enabled = true;
                txtRemarks.Enabled = false;
                txtTransactionAmount.Enabled = false;
                txtChartOfAccount.Enabled = false;
                BankAccNo.Visible = false;
                ddlPayment.Enabled = false;
                ddlCashCheque.Enabled = false;
                lblNoOfTransactions.Visible = false;
                txtNoOfTransactions.Visible = false;
                idspan.Visible = false;
                if (pdtInterBranchPayment != null)
                {
                    pdtInterBranchPayment.Columns.Add("Serial_Number");
                    pdtInterBranchPayment.Columns.Add("Chart_of_Account_Code");
                    pdtInterBranchPayment.Columns.Add("Amount");
                    pdtInterBranchPayment.Columns.Add("Remarks");

                    for (int intCount = 0; intCount < Convert.ToInt32(txtNoOfTransactions.Text);)
                    {
                        DataRow dtRow = pdtInterBranchPayment.NewRow();

                        dtRow["Serial_Number"] = (intCount + 1).ToString();
                        dtRow["Chart_of_Account_Code"] = "";
                        dtRow["Amount"] = "";
                        dtRow["Remarks"] = "";
                        pdtInterBranchPayment.Rows.Add(dtRow);
                        intCount++;
                    }
                }

                grvInterBranchPayment.DataSource = pdtInterBranchPayment;
                grvInterBranchPayment.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
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
                Session.Remove("BranchCodeOthers");
                Session["BranchCodeOthers"] = null;

                Server.ClearError();
                Response.Redirect("InterBranchPayment.aspx", false);
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