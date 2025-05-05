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
    public partial class CashAndBankPayment : System.Web.UI.Page
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
                Log.WriteException(typeof(CashAndBankPayment), exp);
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

                    fnPopulateTransactionNumber(strBranchCode);
                    ddlTransactionNumber.Visible = false;
                    txtTransactionAmount.Attributes.Add("OnChange", "return ValidAmountLimit('" + Session["UserID"].ToString() + "');");
                    fnPopulateDropDown("CBPPayments", ddlPayment);
                    fnPopulateDropDown("CBPCashCheque", ddlCashCheque);
                    txtBranch.Text = fnGetBranch(strBranchCode);
                    LoadAccountintPeriod();
                    txtAccountingPeriod.Attributes.Add("style", "display:none");
                    grvCashAndBankPayment.Visible = false;
                    BtnSubmit.Visible = false;
                    ddlPayment.Enabled = true;
                    ddlCashCheque.Attributes.Add("OnChange", "funModeOfPayment();");
                    fnPopulateDropDown("LocalOutStation", ddlLocalOutstation);

                    DateItem PrevDateStatus = objReceivableInvoice.GetPreviousDateStatus(Session["UserID"].ToString(), "CBPayment");

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
                    {
                        txtTransactionDate.Enabled = false;
                    }
                }                

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
                }
                else
                {
                    if (Session["BranchCode"].ToString() == "COR")
                    {
                        BankAccNo.Filter = "009,036,047,049";
                    }
                    else
                    {
                        BankAccNo.Filter = "036,047,049";
                    }
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

                if (grvCashAndBankPayment.Visible == false)
                {
                    BtnSubmit.Visible = false;
                    btnTransactionDetails.Visible = true;
                }
                else
                {
                    BtnSubmit.Visible = true;
                    btnTransactionDetails.Visible = false;
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
            if (grvCashAndBankPayment.Rows.Count > 0)
            {
                if (grvCashAndBankPayment.FooterRow != null)
                {
                    TextBox txtTotalAmount = (TextBox)grvCashAndBankPayment.FooterRow.FindControl("txtTotalAmount");
                    txtTotalAmount.Text = "0";

                    for (int i = 0; i < grvCashAndBankPayment.Rows.Count; i++)
                    {
                        TextBox txtGrdAmount = (TextBox)grvCashAndBankPayment.Rows[i].Cells[2].FindControl("txtGrdAmount");

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

                    if (grvCashAndBankPayment.Rows.Count >= 1)
                    {
                        for (int i = 0; i < grvCashAndBankPayment.Rows.Count; i++)
                        {
                            DataRow dr = dt.NewRow();

                            TextBox txtGrdChartAccount = (TextBox)grvCashAndBankPayment.Rows[i].Cells[1].FindControl("txtGrdChartOfAccount");
                            TextBox txtGrdAmount = (TextBox)grvCashAndBankPayment.Rows[i].Cells[2].FindControl("txtGrdAmount");
                            TextBox txtGrdRemarks = (TextBox)grvCashAndBankPayment.Rows[i].Cells[3].FindControl("txtGrdRemarks");

                            dr["Chart_of_Account_Code"] = txtGrdChartAccount.Text;
                            dr["Amount"] = txtGrdAmount.Text;
                            dr["Remarks"] = txtGrdRemarks.Text;
                            dt.Rows.Add(dr);
                        }

                        dt.Rows.RemoveAt(e.RowIndex);
                        grvCashAndBankPayment.DataSource = dt;
                        grvCashAndBankPayment.DataBind();

                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                ChartAccount COA = (ChartAccount)grvCashAndBankPayment.Rows[i].Cells[1].FindControl("COA");
                                TextBox txtGrdChartAccount = (TextBox)grvCashAndBankPayment.Rows[i].Cells[1].FindControl("txtGrdChartOfAccount");
                                TextBox txtGrdAmount = (TextBox)grvCashAndBankPayment.Rows[i].Cells[2].FindControl("txtGrdAmount");
                                TextBox txtGrdRemarks = (TextBox)grvCashAndBankPayment.Rows[i].Cells[3].FindControl("txtGrdRemarks");

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
                            grvCashAndBankPayment.Visible = false;
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

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            BtnSubmit.Visible = false;
            btnReport.Enabled = true;
            ddlAccountingPeriod.Enabled = false;
            ddlCashCheque.Enabled = false;
            try
            {
                SubmitCashAndBankPayment();
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
                Response.Redirect("CashAndBankPayment.aspx", false);
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
                drop = lib.GetDropDownListValues("CBPCashCheque");
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
            fnGetCashAndBankPayment(ddlTransactionNumber.SelectedValue);
            grvCashAndBankPayment.Visible = true;
            grvCashAndBankPayment.Enabled = false;
            ddlAccountingPeriod.Enabled = false;
            BankAccNo.Visible = false;
            txtChartOfAccount.Enabled = false;
            txtTransactionAmount.Enabled = false;
            txtRemarks.Enabled = false;
            ddlPayment.Enabled = false;
            ddlCashCheque.Enabled = false;
            btnTransactionDetails.Visible = false;
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
                        BankAccNo.Filter = "009,036,047,049";
                    }
                    else
                    {
                        BankAccNo.Filter = "036,047,049";
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
                Log.WriteException(typeof(CashAndBankPayment), exp);
            }
            return cashandbankTransactions.GetBranchName(strBranchCode);
        }

        private void SubmitCashAndBankPayment()
        {
            CashAndBankEntity cashandbankEntity = new CashAndBankEntity();
            cashandbankEntity.Items = new List<CashAndBankItem>();

            cashandbankEntity.TransactionNumber = txtTransactionNumber.Text;
            cashandbankEntity.TransactionDate = txtTransactionDate.Text;
            cashandbankEntity.BranchCode = strBranchCode;
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

            CashAndBankItem cashandbankItem = null;
            int intCount = 0;

            int rowIndex = Convert.ToInt16(RowNum.Value);

            foreach (GridViewRow grvRow in grvCashAndBankPayment.Rows)
            {
                cashandbankItem = new CashAndBankItem();

                intCount += 1;
                cashandbankItem.Serial_Number = intCount.ToString();
                cashandbankItem.Chart_of_Account_Code = ((TextBox)grvRow.Cells[1].FindControl("txtGrdChartOfAccount")).Text;
                cashandbankItem.Amount = ((TextBox)grvRow.Cells[2].FindControl("txtGrdAmount")).Text == "" ? "0.00" : ((TextBox)grvRow.Cells[2].FindControl("txtGrdAmount")).Text;
                cashandbankItem.Remarks = ((TextBox)grvRow.Cells[3].FindControl("txtGrdRemarks")).Text;

                cashandbankEntity.Items.Add(cashandbankItem);
            }

            CashAndBankTransactions cashandbankTransactions = new CashAndBankTransactions();
            int result = cashandbankTransactions.AddCashBankDetails(ref cashandbankEntity, "");

            if ((cashandbankEntity.ErrorMsg == string.Empty) && (cashandbankEntity.ErrorCode == "0"))
            {
                txtTransactionNumber.Text = cashandbankEntity.TransactionNumber;

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('Cash & Bank Payment details are successfully inserted');", true);

                grvCashAndBankPayment.Enabled = false;
                BtnSubmit.Enabled = false;
                txtRemarks.Enabled = false;
                txtTransactionAmount.Enabled = false;
                txtChartOfAccount.Enabled = false;
                BankAccNo.Visible = false;
                ddlPayment.Enabled = false;
                ddlCashCheque.Enabled = false;
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

        private void fnGetCashAndBankPayment(string strTransactionNumber)
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
            ddlAccountingPeriod.SelectedValue = cashandbankEntity.Accounting_Period;

            txtChequeNumber.Text = cashandbankEntity.Cheque_Number;
            txtChequeDate.Text = cashandbankEntity.Cheque_Date;
            txtBank.Text = cashandbankEntity.Cheque_Bank;
            txtBankBranch.Text = cashandbankEntity.Cheque_Branch;
            ddlLocalOutstation.SelectedValue = cashandbankEntity.Local_Outstation == "" ? "L" : cashandbankEntity.Local_Outstation;
            txtReferenceDate.Text = cashandbankEntity.Ref_Date;

            grvCashAndBankPayment.DataSource = (object)cashandbankEntity.Items;
            grvCashAndBankPayment.DataBind();

            foreach (GridViewRow gr in grvCashAndBankPayment.Rows)
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
                grvCashAndBankPayment.Visible = false;
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
                txtTransactionDate.Enabled = false;
                txtRemarks.Enabled = false;
                txtTransactionNumber.Enabled = false;
                txtTransactionAmount.Enabled = false;
                ddlPayment.Enabled = false;
                ddlCashCheque.Enabled = false;
                txtChequeNumber.Enabled = false;
                txtChequeDate.Enabled = false;
                txtBank.Enabled = false;
                txtBankBranch.Enabled = false;
                ddlLocalOutstation.Enabled = false;
                txtReferenceDate.Enabled = false;
                grvCashAndBankPayment.Enabled = false;
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
                Log.WriteException(typeof(CashAndBankPayment), exp);
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
                            TextBox txtGrdChartOfAccount = (TextBox)grvCashAndBankPayment.Rows[rowIndex].Cells[1].FindControl("txtGrdChartOfAccount");
                            TextBox txtGrdAmount = (TextBox)grvCashAndBankPayment.Rows[rowIndex].Cells[2].FindControl("txtGrdAmount");
                            TextBox txtGrdRemarks = (TextBox)grvCashAndBankPayment.Rows[rowIndex].Cells[3].FindControl("txtGrdRemarks");

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
                        grvCashAndBankPayment.DataSource = dtCurrentTable;
                        grvCashAndBankPayment.DataBind();
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
                grvCashAndBankPayment.DataSource = dt;
                grvCashAndBankPayment.DataBind();

                grvCashAndBankPayment.Visible = true;
                grvCashAndBankPayment.Enabled = true;
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
                            ChartAccount COA = (ChartAccount)grvCashAndBankPayment.Rows[rowIndex].Cells[1].FindControl("COA");
                            TextBox txtGrdChartAccount = (TextBox)grvCashAndBankPayment.Rows[rowIndex].Cells[1].FindControl("txtGrdChartOfAccount");
                            TextBox txtGrdAmount = (TextBox)grvCashAndBankPayment.Rows[rowIndex].Cells[2].FindControl("txtGrdAmount");
                            TextBox txtGrdRemarks = (TextBox)grvCashAndBankPayment.Rows[rowIndex].Cells[3].FindControl("txtGrdRemarks");

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
            
            try
            {                
                btnTransactionDetails.Visible = false;
                BtnSubmit.Visible = true;
                BtnSubmit.Enabled = true;
                txtRemarks.Enabled = false;
                txtTransactionAmount.Enabled = false;
                txtChartOfAccount.Enabled = false;
                BankAccNo.Visible = false;
                ddlPayment.Enabled = false;
                ddlCashCheque.Enabled = false;

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
            GenerateSelectionFormula();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Server.ClearError();
                Response.Redirect("CashAndBankPayment.aspx", false);
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

            strSelectionFormula = strSelectionFormula + "{Main_Cash_Header.Branch_Code}='" + strBranchCode + "' and {Main_Cash_Header.Receipt_Payment_Indicator} <> 'R' and ";

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

        private string TwoDecimalConversion(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
                return "0.00";
            else
                return string.Format("{0:0.00}", Convert.ToDecimal(strValue));
        }
    }    
}