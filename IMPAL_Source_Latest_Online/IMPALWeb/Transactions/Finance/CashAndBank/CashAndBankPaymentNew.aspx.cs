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
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.IO;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.OleDb;
using static IMPALLibrary.ReceivableInvoice;
using System.Globalization;

#endregion Namespace

namespace IMPALWeb.Finance
{
    public partial class CashAndBankPaymentNew : System.Web.UI.Page
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

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(CashAndBankPaymentNew), exp);
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
                    txtNoOfTransactions.Attributes.Add("onkeypress", "return IntegerValueOnly();");
                    txtTransactionAmount.Attributes.Add("OnChange", "return ValidAmountLimit('" + Session["UserID"].ToString() + "');");
                    btnFileUpload.Attributes.Add("OnClick", "return ValidateTransactionFields(2);");
                    fnPopulateDropDown("CBPPayments", ddlPayment);
                    fnPopulateDropDown("CBPCashCheque", ddlCashCheque);
                    txtBranch.Text = fnGetBranch(strBranchCode);
                    LoadAccountintPeriod();
                    txtAccountingPeriod.Attributes.Add("style", "display:none");                    
                    grvCashAndBankPaymentNew.Visible = false;
                    BtnSubmit.Visible = false;
                    ddlPayment.Enabled = true;
                    ddlCashCheque.Attributes.Add("OnChange", "funModeOfPayment();");
                    fnPopulateDropDown("LocalOutStation", ddlLocalOutstation);
                    divItemDetailsExcel.Attributes.Add("style", "display:none");
                    HdnExcelTotalValue.Value = "";

                    DateItem PrevDateStatus = objReceivableInvoice.GetPreviousDateStatus(Session["UserID"].ToString(), "CBPymtNew");

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

                if (grvCashAndBankPaymentNew.Visible == false)
                {
                    BtnSubmit.Visible = false;
                    btnReportTransaction.Visible = true;
                }
                else
                {
                    BtnSubmit.Visible = true;
                    btnReportTransaction.Visible = false;
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

            int PrevFinYearStatus = objReceivableInvoice.GetPreviousAccountingPeriodStatus(Session["UserID"].ToString(), "CBPymtNew");

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
            ddlCashCheque.Enabled = false;
            try
            {
                SubmitCashAndBankPaymentNew();
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
                Response.Redirect("CashAndBankPaymentNew.aspx", false);
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
            fnGetCashAndBankPaymentNew(ddlTransactionNumber.SelectedValue);
            grvCashAndBankPaymentNew.Visible = true;
            grvCashAndBankPaymentNew.Enabled = false;
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
                Log.WriteException(typeof(CashAndBankPaymentNew), exp);
            }
            return cashandbankTransactions.GetBranchName(strBranchCode);												 
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
                    filePath = @"D:\Downloads\CashAndBankPayments";

                    Session["UploadDetails"] = "";

                    string fileName = btnFileUpload.FileName;

                    if (File.Exists(filePath + "\\" + fileName))
                        File.Delete(filePath + "\\" + fileName);

                    btnFileUpload.SaveAs(filePath + "\\" + fileName);

                    if (File.Exists(Path.Combine(filePath, fileName)))
                    {
                        UploadCBPaymentDetails(filePath, fileName);
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

        protected void UploadCBPaymentDetails(string filePath, string fileName)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Database ImpalDb = DataAccess.GetDatabase();

                string myexceldataquery = "Select Branch, Date, Remarks, Amount from [sheet1$]";
                string ExcelConnectionString = "";
                string sqlTempQuery = "";

                sqlTempQuery = "truncate table Temp_Payment_Main_cash_details_excel";
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
                    sqlTempQuery = "Exec Usp_AddTemp_Payment_CashAndBank_Excel_New '" + dr[0] + "','" + dr[1] + "','" + dr[2] + "'," + dr[3] + ",'" + strBranchCode + "'," + txtTransactionAmount.Text + ",0,'" + filePath + "','" + fileName + "','" + Session["UserID"].ToString() + "', 0";
                    DbCommand cmd1 = ImpalDb.GetSqlStringCommand(sqlTempQuery);
                    cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDb.ExecuteNonQuery(cmd1);
                }

                OledbConn.Close();

                string file = Path.Combine(filePath, fileName);
                CashAndBankTransactions cashandbankTransactions = new CashAndBankTransactions();
                CashAndBankEntity cashandbankEntity = cashandbankTransactions.GetCashBankPaymentDetailsExcelNew(filePath, fileName, strBranchCode, txtTransactionAmount.Text, 1, hdnTotalAmount.Value);
                HdnExcelTotalValue.Value = cashandbankEntity.TransactionAmount;
                divItemDetailsExcel.Attributes.Add("style", "display:none");

                if (cashandbankEntity.ErrorMsg != "")
                {
                    btnUploadExcel.Enabled = false;
                    lblUploadMessage.Visible = false;
                    BtnSubmit.Visible = false;
                    BtnSubmit.Enabled = false;
                    txtTransactionAmount.Enabled = true;
                    grvCashAndBankPaymentNew.Visible = false;
                    Session["UploadDetails"] = "<font style='color:red' size='2'>Branch Names " + cashandbankEntity.ErrorMsg + " in the Excel File differs from the Branch Master.<br />Please Check the file" + file + "</font>";
                }
                else
                {
                    if (cashandbankEntity.ErrorCode == "0.00")
                    {
                        //divItemDetails.Attributes.Add("style", "display:inline");
                        grvCashAndBankPaymentNew.Visible = false;
                        BtnSubmit.Visible = true;
                        BtnSubmit.Enabled = true;
                        txtTransactionAmount.Enabled = false;

                        foreach (GridViewRow gr in grvCashAndBankPaymentNew.Rows)
                        {
                            TextBox txtChartOfAccount = (TextBox)gr.FindControl("txtChartOfAccount");
                            TextBox txtAmount = (TextBox)gr.FindControl("txtAmount");
                            TextBox txtRemarks = (TextBox)gr.FindControl("txtRemarks");

                            sqlTempQuery = "Exec Usp_AddTemp_Payment_CashAndBank_Excel_New '" + txtChartOfAccount.Text + "','" + txtTransactionDate.Text + "','" + txtRemarks.Text + "'," + txtAmount.Text + ",'" + strBranchCode + "'," + txtTransactionAmount.Text + ",0,'" + filePath + "','" + fileName + "','" + Session["UserID"].ToString() + "', 2";
                            DbCommand cmd2 = ImpalDb.GetSqlStringCommand(sqlTempQuery);
                            cmd2.CommandTimeout = ConnectionTimeOut.TimeOut;
                            ImpalDb.ExecuteNonQuery(cmd2);
                        }

                        Session["UploadDetails"] = "<b><font style='color:green' size='3'>Transaction Amount has been tallied with Rows & Excel Amount. Please Submit the file" + file + "</font></b>";
                    }
                    else
                    {
                        btnUploadExcel.Enabled = false;
                        lblUploadMessage.Visible = false;
                        BtnSubmit.Visible = false;
                        BtnSubmit.Enabled = false;
                        txtTransactionAmount.Enabled = true;
                        grvCashAndBankPaymentNew.Visible = false;
                        Session["UploadDetails"] = "<b><font style='color:red' size='3'>Transaction Amount Differs with the Excel Amount by Rs." + cashandbankEntity.ErrorCode + ".<br />Please Check the file" + file + "</font></b>";
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

        private void SubmitCashAndBankPaymentNew()
        {
            CashAndBankEntity cashandbankEntity = new CashAndBankEntity();
            cashandbankEntity.Items = new List<CashAndBankItem>();

            cashandbankEntity.TransactionNumber = txtTransactionNumber.Text;
            cashandbankEntity.TransactionDate = txtTransactionDate.Text;
            cashandbankEntity.BranchCode = strBranchCode;
            cashandbankEntity.Remarks = ddlPayment.SelectedItem.Text + " - " + txtRemarks.Text + "-HOREGUPD";
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

            CashAndBankTransactions cashandbankTransactions = new CashAndBankTransactions();
            int result = cashandbankTransactions.AddCashBankDetailsExcelNew(ref cashandbankEntity, "");

            if ((cashandbankEntity.ErrorMsg == string.Empty) && (cashandbankEntity.ErrorCode == "0"))
            {
                txtTransactionNumber.Text = cashandbankEntity.TransactionNumber;

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('Cash & Bank Payment details are successfully inserted');", true);

                grvCashAndBankPaymentNew.Enabled = false;
                BtnSubmit.Visible = false;
                txtRemarks.Enabled = false;
                txtTransactionAmount.Enabled = false;
                txtChartOfAccount.Enabled = false;
                BankAccNo.Visible = false;
                ddlPayment.Enabled = false;
                ddlCashCheque.Enabled = false;
                txtNoOfTransactions.Visible = false;
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

        private void fnGetCashAndBankPaymentNew(string strTransactionNumber)
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

            grvCashAndBankPaymentNew.DataSource = (object)cashandbankEntity.Items;
            grvCashAndBankPaymentNew.DataBind();

            foreach (GridViewRow gr in grvCashAndBankPaymentNew.Rows)
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
                grvCashAndBankPaymentNew.Visible = false;
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
                txtNoOfTransactions.Enabled = false;
                txtChequeNumber.Enabled = false;
                txtChequeDate.Enabled = false;
                txtBank.Enabled = false;
                txtBankBranch.Enabled = false;
                ddlLocalOutstation.Enabled = false;
                txtReferenceDate.Enabled = false;
                grvCashAndBankPaymentNew.Enabled = false;
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
                ddlTransactionNumber.DataSource = cashandbankTransactions.GetTransactionNumberUploadAndRegular(strBranchCode,"P");
                ddlTransactionNumber.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(CashAndBankPaymentNew), exp);
            }
        }      

        protected void btnReportTransaction_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            DataTable pdtCashAndBankPaymentNew= new DataTable();
            try
            {
                grvCashAndBankPaymentNew.Visible = true;
                grvCashAndBankPaymentNew.Enabled = true;  
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
                if (pdtCashAndBankPaymentNew != null)
                {
                    pdtCashAndBankPaymentNew.Columns.Add("Serial_Number");
                    pdtCashAndBankPaymentNew.Columns.Add("Chart_of_Account_Code");
                    pdtCashAndBankPaymentNew.Columns.Add("Amount");
                    pdtCashAndBankPaymentNew.Columns.Add("Remarks");

                    for (int intCount = 0; intCount < Convert.ToInt32(txtNoOfTransactions.Text); )
                    {
                        DataRow dtRow = pdtCashAndBankPaymentNew.NewRow();

                        dtRow["Serial_Number"] = (intCount + 1).ToString();
                        dtRow["Chart_of_Account_Code"] = "";
                        dtRow["Amount"] = "";
                        dtRow["Remarks"] = "";
                        pdtCashAndBankPaymentNew.Rows.Add(dtRow);
                        intCount++;
                    }

                    divItemDetailsExcel.Attributes.Add("style", "display:inline");
                }
                
                grvCashAndBankPaymentNew.DataSource = pdtCashAndBankPaymentNew;
                grvCashAndBankPaymentNew.DataBind();

                ViewState["CurrentTable"] = pdtCashAndBankPaymentNew;
                ViewState["GridRowCount"] = pdtCashAndBankPaymentNew.Rows.Count;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void btnUploadExcel_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            DataTable pdtCashAndBankPaymentExcel = new DataTable();
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
            GenerateSelectionFormula();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Server.ClearError();
                Response.Redirect("CashAndBankPaymentNew.aspx", false);
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
    }
}