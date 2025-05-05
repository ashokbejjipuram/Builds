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
using System.Data.Common;
using System.Data;
using IMPALWeb.UserControls;
using IMPLLib = IMPALLibrary.Transactions;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using IMPALLibrary.Masters.Sales;
using IMPALLibrary.Transactions.Finance;
using static IMPALLibrary.ReceivableInvoice;
using System.Globalization;

#endregion Namespace

namespace IMPALWeb.Finance
{
    public partial class CashAndBankReceiptExcel : System.Web.UI.Page
    {
        private string strBranchCode;
        private const string serverDownloadFolder = "Downloads\\NEFT";
        private string serverPathMode = System.Configuration.ConfigurationManager.AppSettings["ServerPathMode"].ToString();
        private string filePath = System.Configuration.ConfigurationManager.AppSettings["UploadPath"].ToString();
        private AccountingPeriods Acc = new AccountingPeriods();
        private ReceivableInvoice objReceivableInvoice = new ReceivableInvoice();
        private bool isDownloadPathAvailable = false;
        private const string branchCodeHO = "COR";
        private string fileName;

        IMPALLibrary.Uitility uitility = new IMPALLibrary.Uitility();
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(CashAndBankReceiptExcel), exp);
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
                    btnBack.Visible = false;

                    if (crCashAndBank != null)
                    {
                        crCashAndBank.Dispose();
                        crCashAndBank = null;
                    }

                    fnPopulateTransactionNumber(strBranchCode);
                    ddlTransactionNumber.Visible = false;
                    txtTransactionAmount.Attributes.Add("onkeypress", "return CurrencyNumberOnly();");
                    btnFileUpload.Attributes.Add("OnClick", "return ValidateTransactionFields(2);");
                    LoadAccountintPeriod();
                    txtAccountingPeriod.Attributes.Add("style", "display:none");
                    fnPopulateDropDown("CBRReceipts", ddlPayment);
                    fnPopulateDropDown("CBPCashCheque", ddlCashCheque);
                    ddlCashCheque.Attributes.Add("OnChange", "funModeOfPayment();");
                    txtBranch.Text = fnGetBranch(strBranchCode);
                    fnPopulateDropDown("LocalOutStation", ddlLocalOutstation);
                    divItemDetails.Attributes.Add("style", "display:none");
                    HdnExcelTotalValue.Value = "";
                    divItemDetailsExcel.Attributes.Add("style", "display:inline");

                    if (Session["BranchCode"].ToString() == "COR")
                    {
                        BankAccNo.Filter = "009,036,047,049";
                    }
                    else
                    {
                        BankAccNo.Filter = "036,047,049";
                    }

                    grvCashAndBankReceiptExcel.Visible = false;
                    BtnSubmit.Visible = false;
                    btnReport.Enabled = false;

                    DateItem PrevDateStatus = objReceivableInvoice.GetPreviousDateStatus(Session["UserID"].ToString(), "CBRecptExcel");

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

                if (grvCashAndBankReceiptExcel.Visible == false)
                {
                    BtnSubmit.Visible = false;
                    btnUploadExcel.Visible = true;
                }
                else
                {
                    BtnSubmit.Visible = true;
                    btnUploadExcel.Visible = false;
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

            int PrevFinYearStatus = objReceivableInvoice.GetPreviousAccountingPeriodStatus(Session["UserID"].ToString(), "CBRecptExcel");

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

        protected void imgEditToggle_Click(object sender, ImageClickEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                txtTransactionNumber.Visible = false;
                ddlTransactionNumber.Visible = true;
                BtnSubmit.Enabled = false;
                DisableViewMode();
                grvCashAndBankReceiptExcel.Visible = false;
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
                grvCashAndBankReceiptExcel.Enabled = false;
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
            ddlCashCheque.Enabled = false;
            try
            {
                SubmitCashAndBankReceiptExcel();
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
                Response.Redirect("CashAndBankReceiptExcel.aspx", false);
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
                    txtChequeDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    txtBank.Text = objBankDtl.BankName;
                    txtBankBranch.Text = objBankDtl.Address;

                    ddlCashCheque.DataSource = drop.Where(p => p.DisplayValue != "H").ToList();
                    ddlCashCheque.DataValueField = "DisplayValue";
                    ddlCashCheque.DataTextField = "DisplayText";
                    ddlCashCheque.DataBind();
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
            fnGetCashAndBankReceiptExcel(ddlTransactionNumber.SelectedValue);
            grvCashAndBankReceiptExcel.Visible = true;
            grvCashAndBankReceiptExcel.Enabled = false;
            divItemDetails.Attributes.Add("style", "display:inline");
            divItemDetailsExcel.Attributes.Add("style", "display:none");
            txtChartOfAccount.Enabled = false;
            BankAccNo.Visible = false;
            txtTransactionAmount.Enabled = false;
            txtRemarks.Enabled = false;
            ddlPayment.Enabled = false;
            ddlCashCheque.Enabled = false;
            btnUploadExcel.Visible = false;
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

        private string fnGetBranch(string strBranchCode)
        {
            CashAndBankTransactions cashandbankTransactions = new CashAndBankTransactions();
            try
            {
                return (string)Session["BranchName"];
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(CashAndBankReceiptExcel), exp);
            }

            return (string)Session["BranchName"];
        }

        private void ShowSuccessMessage(string message)
        {
            lblUploadMessage.Visible = true;
            lblUploadMessage.Text = "<br /><br /><center style='font-size:13px;'><b>" + message + "</center>";
        }

        protected void UploadFileData(bool isHOProcess)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                string sStatus = string.Empty;

                if (btnFileUpload.HasFile)
                {
                    filePath = @"D:\Downloads\CashAndBankReceipts";

                    Session["UploadDetails"] = "";

                    string fileName = btnFileUpload.FileName;

                    if (File.Exists(filePath + "\\" + fileName))
                        File.Delete(filePath + "\\" + fileName);

                    btnFileUpload.SaveAs(filePath + "\\" + fileName);

                    if (File.Exists(Path.Combine(filePath, fileName)))
                    {
                        UploadCBReceiptDetails(filePath, fileName);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "alert('File doesnt exists');", true);
                    }
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        protected void UploadCBReceiptDetails(string filePath, string fileName)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Database ImpalDb = DataAccess.GetDatabase();

                string myexceldataquery = "Select Branch, Date, Remarks, Amount from [sheet1$]";
                string ExcelConnectionString = "";
                string sqlTempQuery = "";

                sqlTempQuery = "truncate table Temp_Receipt_Main_cash_details_excel";
                DbCommand cmd = ImpalDb.GetSqlStringCommand(sqlTempQuery);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                ImpalDb.ExecuteNonQuery(cmd);

                if (fileName.ToString().EndsWith("s"))
                    ExcelConnectionString = @"provider=microsoft.jet.oledb.4.0;data source=" + filePath + "\\" + fileName + ";extended properties=" + "\"excel 8.0;hdr=yes;\"";
                else
                    ExcelConnectionString = @"provider=Microsoft.ace.oledb.12.0;data source=" + filePath + "\\" + fileName + ";extended properties=" + "\"excel 12.0 xml;hdr=yes;\"";

                OleDbConnection OledbConn = new OleDbConnection(ExcelConnectionString);
                OleDbCommand OledbCmd = new OleDbCommand(myexceldataquery, OledbConn);
                OledbConn.Open();
                OleDbDataReader dr = OledbCmd.ExecuteReader();
                while (dr.Read())
                {
                    sqlTempQuery = "Exec Usp_AddTemp_Receipt_CashAndBank_Excel '" + dr[0] + "','" + dr[1] + "','" + dr[2] + "'," + dr[3] + ",'" + strBranchCode + "'," + txtTransactionAmount.Text + ",'" + filePath + "','" + fileName + "','" + Session["UserID"].ToString() + "', 0";
                    DbCommand cmd1 = ImpalDb.GetSqlStringCommand(sqlTempQuery);
                    cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDb.ExecuteNonQuery(cmd1);
                }

                OledbConn.Close();

                string file = Path.Combine(filePath, fileName);
                CashAndBankTransactions cashandbankTransactions = new CashAndBankTransactions();
                CashAndBankEntity cashandbankEntity = cashandbankTransactions.GetCashBankReceiptDetailsExcel(filePath, fileName, strBranchCode, txtTransactionAmount.Text, 1);
                HdnExcelTotalValue.Value = cashandbankEntity.TransactionAmount;
                divItemDetailsExcel.Attributes.Add("style", "display:none");

                if (cashandbankEntity.ErrorMsg != "")
                {
                    btnUploadExcel.Enabled = false;
                    lblUploadMessage.Visible = false;
                    BtnSubmit.Visible = false;
                    BtnSubmit.Enabled = false;
                    txtTransactionAmount.Enabled = true;
                    grvCashAndBankReceiptExcel.Visible = false;
                    Session["UploadDetails"] = "<font style='color:red' size='2'>Branch Names " + cashandbankEntity.ErrorMsg + " in the Excel File differs from the Branch Master.<br />Please Check the file" + file + "</font>";
                }
                else
                {
                    if (cashandbankEntity.ErrorCode == "0.00")
                    {
                        divItemDetails.Attributes.Add("style", "display:inline");
                        grvCashAndBankReceiptExcel.Visible = true;
                        BtnSubmit.Visible = true;
                        BtnSubmit.Enabled = true;
                        txtTransactionAmount.Enabled = false;

                        Session["UploadDetails"] = "<b><font style='color:green' size='3'>Transaction Amount has been tallied. Please Submit the file" + file + "</font></b>";

                        grvCashAndBankReceiptExcel.DataSource = (object)cashandbankEntity.Items;
                        grvCashAndBankReceiptExcel.DataBind();
                        foreach (GridViewRow gr in grvCashAndBankReceiptExcel.Rows)
                        {
                            TextBox txtAmount = (TextBox)gr.FindControl("txtAmount");
                            ChartAccount coA = (ChartAccount)gr.FindControl("COA");
                            TextBox txtRemarks = (TextBox)gr.FindControl("txtRemarks");
                            coA.Visible = false;
                            txtAmount.Enabled = false;
                        }
                    }
                    else
                    {
                        btnUploadExcel.Enabled = false;
                        lblUploadMessage.Visible = false;
                        BtnSubmit.Visible = false;
                        BtnSubmit.Enabled = false;
                        txtTransactionAmount.Enabled = true;
                        grvCashAndBankReceiptExcel.Visible = false;
                        Session["UploadDetails"] = "<font style='color:red' size='2'>Transaction Amount Differs with the Excel Amount by Rs." + cashandbankEntity.ErrorCode + ".<br />Please Check the file" + file + "</font>";
                    }
                }
            }
            catch (Exception exp)
            {
                Session["UploadDetails"] = "Error in the Data";
                throw new Exception(exp.Message);
            }
        }

        private string TwoDecimalConversion(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
                return "0.00";
            else
                return string.Format("{0:0.00}", Convert.ToDecimal(strValue));
        }

        private void SubmitCashAndBankReceiptExcel()
        {
            CashAndBankEntity cashandbankEntity = new CashAndBankEntity();
            cashandbankEntity.Items = new List<CashAndBankItem>();

            cashandbankEntity.TransactionNumber = txtTransactionNumber.Text;
            cashandbankEntity.TransactionDate = txtTransactionDate.Text;
            cashandbankEntity.BranchCode = strBranchCode;
            cashandbankEntity.Remarks = txtRemarks.Text + "-HOUPD";
            cashandbankEntity.ChartOfAccountCode = txtChartOfAccount.Text;
            cashandbankEntity.ReceiptPaymentIndicator = ddlPayment.SelectedValue;
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
            int intVisible = 0;
            foreach (GridViewRow grvRow in grvCashAndBankReceiptExcel.Rows)
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
                else
                    grvCashAndBankReceiptExcel.Rows[intVisible].Visible = false;

                intVisible += 1;
            }

            CashAndBankTransactions cashandbankTransactions = new CashAndBankTransactions();
            int result = cashandbankTransactions.AddCashBankDetailsExcel(ref cashandbankEntity, "");

            if ((cashandbankEntity.ErrorMsg == string.Empty) && (cashandbankEntity.ErrorCode == "0"))
            {
                txtTransactionNumber.Text = cashandbankEntity.TransactionNumber;

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('Cash & Bank Receipts details are successfully inserted');", true);

                grvCashAndBankReceiptExcel.Enabled = false;
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

        private void fnGetCashAndBankReceiptExcel(string strTransactionNumber)
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

            grvCashAndBankReceiptExcel.DataSource = (object)cashandbankEntity.Items;            
            grvCashAndBankReceiptExcel.DataBind();

            foreach (GridViewRow gr in grvCashAndBankReceiptExcel.Rows)
            {
                gr.Enabled = false;
                ChartAccount coA = (ChartAccount)gr.FindControl("COA");
                coA.Visible = false;
            }
        }

        private void fnPopulateTransactionNumber(string strBranchCode)
        {
            CashAndBankTransactions cashandbankTransactions = new CashAndBankTransactions();
            
            try
            {
                ddlTransactionNumber.DataSource = cashandbankTransactions.GetTransactionNumberUpload(strBranchCode,"R");
                ddlTransactionNumber.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(CashAndBankReceiptExcel), exp);
            }
        }

        protected void btnUploadExcel_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            DataTable pdtCashAndBankReceiptExcel = new DataTable();
            try
            {
                UploadFileData(false);
                ShowSuccessMessage(Session["UploadDetails"].ToString());                
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
            crCashAndBank.GenerateReportAndExportA4();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Server.ClearError();
                Response.Redirect("CashAndBankReceiptExcel.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}