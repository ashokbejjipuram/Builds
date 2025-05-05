using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IMPALLibrary;
using IMPALLibrary.Transactions.Finance;
using IMPALLibrary.Masters.Sales;
using IMPALWeb.UserControls;
using System.Web.UI.HtmlControls;
using IMPALLibrary.Masters;
using IMPALLibrary.Common;

namespace IMPALWeb.Finance
{
    public partial class TDSPayment : System.Web.UI.Page
    {
        CashAndBankTransactions tdsPayment = new CashAndBankTransactions();

        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!IsPostBack)
                {
                    ddlDocumentNumber.Visible = false;
                    LoadAccountintPeriod();
                    LoadTDSPaymentDocuments();
                    ddlBranch.SelectedValue = Session["BranchCode"].ToString();
                    ddlBranch.Enabled = false;
                    txtAccountingPeriod.Visible = false;
                    txtDocumentDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    ddlModeOfPayment.Attributes.Add("OnChange", "funModeOfPayment();");
                    BtnSubmit.Attributes.Add("OnClick", "return funVendorSubmitValidation(2);");

                    if (Session["BranchCode"].ToString() == "COR")
                    {
                        BankAccNo.Filter = "009,036,047,049";
                    }
                    else
                    {
                        BankAccNo.Filter = "036,047,049";
                    }

                    FirstGridViewRow(string.Empty);
                    txtAmount.Attributes.Add("OnChange", "return GetTotal();");
                }

                if (ddlModeOfPayment.SelectedValue == "CH")
                {
                    txtChequeNumber.Enabled = true;
                    txtChequeDate.Enabled = true;
                    txtBank.Enabled = true;
                    txtBranch.Enabled = true;
                }
                else
                {
                    txtChequeNumber.Enabled = false;
                    txtChequeDate.Enabled = false;
                    txtBank.Enabled = false;
                    txtBranch.Enabled = false;
                }

                txtHdnGridCtrls.Attributes.Add("Style", "display:none");
            }
            catch (Exception ex)
            {
                Log.WriteException(Source, ex);
            }
        }

        private void LoadTDSPaymentDocuments()
        {
            List<VendorBookingEntity> lstMiscBillNumber = new List<VendorBookingEntity>();
            lstMiscBillNumber = tdsPayment.GetVendorPaymentDocuments(Session["BranchCode"].ToString());
            ddlDocumentNumber.DataSource = lstMiscBillNumber;
            ddlDocumentNumber.DataTextField = "DocumentNumber";
            ddlDocumentNumber.DataValueField = "DocumentNumber";
            ddlDocumentNumber.DataBind();
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

        private void FirstGridViewRow(string strNoRowsFoundMsg)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("SerialNumber", typeof(int)));
            dt.Columns.Add(new DataColumn("DocumentNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("DocumentDate", typeof(string)));
            dt.Columns.Add(new DataColumn("InvoiceNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("InvoiceDate", typeof(string)));
            dt.Columns.Add(new DataColumn("InvoiceValue", typeof(string)));
            dt.Columns.Add(new DataColumn("TDSAmount", typeof(string)));
            dt.Columns.Add(new DataColumn("PaidAmount", typeof(string)));
            dt.Columns.Add(new DataColumn("PaymentAmount", typeof(string)));
            dt.Columns.Add(new DataColumn("Remarks", typeof(string)));

            DataRow dr = null;
            dr = dt.NewRow();
            dr["DocumentNumber"] = string.Empty;
            dr["DocumentDate"] = string.Empty;
            dr["InvoiceNumber"] = string.Empty;
            dr["InvoiceDate"] = string.Empty;
            dr["InvoiceValue"] = "0";
            dr["TDSAmount"] = "0";
            dr["PaidAmount"] = "0";
            dr["PaymentAmount"] = "0";
            dr["Remarks"] = string.Empty;
            dt.Rows.Add(dr);

            ViewState["CurrentTable"] = dt;
            grvTDSPaymentDetails.DataSource = dt;
            grvTDSPaymentDetails.DataBind();

            grvTDSPaymentDetails.Rows[0].Cells.Clear();
            grvTDSPaymentDetails.Rows[0].Cells.Add(new TableCell());
            grvTDSPaymentDetails.Rows[0].Cells[0].ColumnSpan = 10;
            grvTDSPaymentDetails.Rows[0].Cells[0].Text = strNoRowsFoundMsg;
            grvTDSPaymentDetails.Rows[0].Cells[0].CssClass = "EmptyRowStyle";
            ViewState["GridRowCount"] = "0";
            hdnRowCnt.Value = "0";
        }

        protected void imgEditToggle_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddlDocumentNumber.Visible = true;
                txtDocumentNumber.Visible = false;

                grvTDSPaymentDetails.Enabled = false;
                imgEditToggle.Enabled = false;
                ddlAccountingPeriod.Enabled = false;
                txtVendorName.Enabled = false;
                txtVendorLocation.Enabled = false;
                txtAmount.Enabled = false;
                txtNarration.Enabled = false;
                BankAccNo.Visible = false;
                ddlModeOfPayment.Enabled = false;
                txtChequeNumber.Enabled = false;
                txtChequeDate.Enabled = false;
                txtBank.Enabled = false;
                txtBranch.Enabled = false;
                tblBalanceAmount.Visible = false;
                BtnSubmit.Enabled = false;
                imgEditToggle.Visible = false;

                FirstGridViewRow(string.Empty);
            }
            catch (Exception ex)
            {
                Log.WriteException(Source, ex);
            }
        }

        protected void ddlDocumentNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlDocumentNumber.SelectedValue == "-- Select --")
                {
                    txtDocumentNumber.Text = "";
                    txtDocumentDate.Text = "";
                    ddlAccountingPeriod.SelectedIndex = 0;
                    ddlVendorCode.SelectedIndex = 0;
                    txtVendorCode.Text = "";
                    txtVendorName.Text = "";
                    txtVendorLocation.Text = "";
                    txtNarration.Text = "";
                    txtGSTINNumber.Text = "";

                    grvTDSPaymentDetails.DataSource = null;
                    grvTDSPaymentDetails.DataBind();

                    ddlDocumentNumber.Visible = true;
                    txtDocumentNumber.Visible = false;
                    ddlVendorCode.Enabled = false;
                    imgEditToggle.Visible = false;
                    tblBalanceAmount.Visible = false;
                }
                else
                {
                    VendorBookingEntity lstDocumentNumber = new VendorBookingEntity();
                    lstDocumentNumber = tdsPayment.GetVendorPaymentHeaderandDetails(Session["BranchCode"].ToString(), ddlDocumentNumber.SelectedValue.ToString());

                    txtDocumentNumber.Text = lstDocumentNumber.DocumentNumber;
                    txtDocumentDate.Text = lstDocumentNumber.DocumentDate;
                    ddlAccountingPeriod.SelectedValue = lstDocumentNumber.AccountingPeriodCode;
                    ddlVendorCode.SelectedValue = lstDocumentNumber.VendorCode;
                    txtVendorCode.Text = lstDocumentNumber.VendorCode;
                    txtVendorName.Text = lstDocumentNumber.VendorName;
                    txtVendorLocation.Text = lstDocumentNumber.Location;
                    txtGSTINNumber.Text = lstDocumentNumber.GSTINNumber;
                    txtAmount.Text = lstDocumentNumber.InvoiceValue;
                    txtNarration.Text = lstDocumentNumber.Narration;
                    txtChartOfAccount.Text = lstDocumentNumber.Chart_of_Account_Code;

                    if (lstDocumentNumber.PaymentMode == "H")
                        ddlModeOfPayment.SelectedIndex = 0;
                    else if (lstDocumentNumber.PaymentMode == "Q")
                        ddlModeOfPayment.SelectedIndex = 1;
                    else if (lstDocumentNumber.PaymentMode == "D")
                        ddlModeOfPayment.SelectedIndex = 2;

                    if (lstDocumentNumber.PaymentMode == "Q")
                    {
                        txtChequeNumber.Text = lstDocumentNumber.ChequeNumber;
                        txtChequeDate.Text = lstDocumentNumber.ChequeDate;
                        txtBank.Text = lstDocumentNumber.ChequeBank;
                        txtBranch.Text = lstDocumentNumber.ChequeBranch;
                    }
                    else
                    {
                        txtChequeNumber.Text = "";
                        txtChequeDate.Text = "";
                        txtBank.Text = "";
                        txtBranch.Text = "";
                    }

                    grvTDSPaymentDetails.DataSource = (object)lstDocumentNumber.Items;
                    grvTDSPaymentDetails.DataBind();

                    foreach (GridViewRow gvr in grvTDSPaymentDetails.Rows)
                    {
                        if (gvr.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox ChkSelected = (CheckBox)gvr.FindControl("ChkSelected");
                            ChkSelected.Checked = true;
                            grvTDSPaymentDetails.Columns[9].Visible = false;
                        }
                    }

                    ddlVendorCode.Enabled = false;                    
                    grvTDSPaymentDetails.Enabled = false;                    
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(typeof(TDSPayment), ex);
            }
        }

        protected void ddlVendorCode_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlVendorCode.SelectedValue == "0")
                {
                    txtVendorCode.Text = "";
                    txtVendorName.Text = "";
                    txtVendorLocation.Text = "";
                    txtGSTINNumber.Text = "";
                    ddlModeOfPayment.SelectedIndex = 0;
                    txtAmount.Text = "0.00";
                    txtNarration.Text = "";
                    txtChequeNumber.Text = "";
                    txtChequeDate.Text = "";
                    txtBank.Text = "";
                    txtBranch.Text = "";
                    txtTotalBalanceAmount.Text = "0.00";
                    FirstGridViewRow(string.Empty);
                }
                else
                {
                    Customers customers = new Customers();
                    Customer customer = customers.GetVendorInfoByVendorCode(Session["BranchCode"].ToString(), ddlVendorCode.SelectedValue);

                    txtVendorCode.Text = customer.Customer_Code;
                    txtVendorName.Text = customer.Customer_Name;
                    txtVendorLocation.Text = customer.Location;
                    txtGSTINNumber.Text = customer.GSTIN;

                    ddlModeOfPayment.SelectedIndex = 0;
                    txtAmount.Text = "0.00";
                    txtNarration.Text = "";
                    txtChequeNumber.Text = "";
                    txtChequeDate.Text = "";
                    txtBank.Text = "";
                    txtBranch.Text = "";
                    txtTotalBalanceAmount.Text = txtAmount.Text;

                    List<VendorBookingDetail> lstTDSPaymentDetails = tdsPayment.GetVendorDocumentDetails(Session["BranchCode"].ToString(), ddlVendorCode.SelectedValue.Substring(0, 3), ddlVendorCode.SelectedValue);

                    if (lstTDSPaymentDetails.Count > 0)
                    {
                        tblBalanceAmount.Visible = true;
                        grvTDSPaymentDetails.DataSource = lstTDSPaymentDetails;
                        grvTDSPaymentDetails.DataBind();
                        //ChkHeader.Visible = true;
                    }
                    else
                    {
                        tblBalanceAmount.Visible = false;
                        FirstGridViewRow("No Records Found.");
                        //ChkHeader.Visible = false;
                    }

                    foreach (GridViewRow gvr in grvTDSPaymentDetails.Rows)
                    {
                        if (gvr.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox ChkSelected = (CheckBox)gvr.FindControl("ChkSelected");
                            if (ChkSelected != null)
                            {
                                ChkSelected.Attributes.Add("OnClick", "return funVendorHeaderValidation('" + ChkSelected.ClientID + "', 1);");
                            }
                        }
                    }

                    hdnRowCnt.Value = lstTDSPaymentDetails.Count.ToString();
                }
            }
            catch (Exception exp)
            {

                Log.WriteException(typeof(TDSPayment), exp);
            }
        }

        private bool ChkCurrencyDots(string StrCurrValue)
        {
            int count = StrCurrValue.Split('.').Length - 1;
            if (count > 1)
                return false;
            else
                return true;
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
                    ddlModeOfPayment.DataSource = drop.Where(p => p.DisplayValue == "H").ToList(); ;
                    ddlModeOfPayment.DataValueField = "DisplayValue";
                    ddlModeOfPayment.DataTextField = "DisplayText";
                    ddlModeOfPayment.DataBind();
                }
                else
                {
                    ddlModeOfPayment.DataSource = drop.Where(p => p.DisplayValue != "H").ToList();
                    ddlModeOfPayment.DataValueField = "DisplayValue";
                    ddlModeOfPayment.DataTextField = "DisplayText";
                    ddlModeOfPayment.DataBind();
                }

                if (ddlModeOfPayment.SelectedValue == "Q")
                {
                    txtChequeNumber.Enabled = true;
                    txtChequeDate.Enabled = true;
                    txtBank.Enabled = true;
                    txtBranch.Enabled = true;
                }
                else
                {
                    txtChequeNumber.Enabled = false;
                    txtChequeDate.Enabled = false;
                    txtBank.Enabled = false;
                    txtBranch.Enabled = false;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ChkSelected_OnCheckedChanged(object sender, EventArgs e)
        {
            try
            {
                decimal totalSum = 0;
                decimal customerCollectionAmount = Convert.ToDecimal(txtAmount.Text);
                int NoOfRowsChecked = 0;

                foreach (GridViewRow gvr in grvTDSPaymentDetails.Rows)
                {
                    if (gvr.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkbox = (CheckBox)gvr.FindControl("ChkSelected");

                        //TextBox txtBookingAmount = (TextBox)gvr.FindControl("txtBookingAmount");
                        //TextBox txtPaidAmount = (TextBox)gvr.FindControl("txtPaidAmount");
                        TextBox txtPaymentAmount = (TextBox)gvr.FindControl("txtPaymentAmount");                        

                        if (chkbox.Checked)
                        {
                            if (ChkCurrencyDots(txtPaymentAmount.Text))
                            {
                                if (string.IsNullOrEmpty(txtPaymentAmount.Text))
                                    txtPaymentAmount.Text = "0";

                                totalSum += Convert.ToDecimal(txtPaymentAmount.Text);
                                txtTotalBalanceAmount.Text = (customerCollectionAmount - Convert.ToDecimal(txtPaymentAmount.Text)).ToString();
                            }
                            else
                            {
                                //txtPaymentAmount.Text = txtBookingAmount.Text;
                                totalSum += Convert.ToDecimal(txtPaymentAmount.Text);
                                txtTotalBalanceAmount.Text = (customerCollectionAmount - Convert.ToDecimal(txtPaymentAmount.Text)).ToString();
                            }
                            
                            NoOfRowsChecked += 1;
                        }
                        else
                        {
                            txtPaymentAmount.Text = "0.00";
                            txtTotalBalanceAmount.Text = customerCollectionAmount.ToString();
                        }
                    }
                }

                tblBalanceAmount.Visible = true;
                //ChkHeader.Visible = true;

                //if (grvTDSPaymentDetails.Rows.Count == NoOfRowsChecked)
                    //ChkHeader.Checked = true;
                //else
                    //ChkHeader.Checked = false;
            }
            catch (Exception exp)
            {

                Log.WriteException(typeof(TDSPayment), exp);
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                VendorBookingEntity TDSPaymentHeader = new VendorBookingEntity();
                IMPALLibrary.Payable payableEntity = new IMPALLibrary.Payable();
                TDSPaymentHeader.Items = new List<VendorBookingDetail>();
                VendorBookingDetail TDSPaymentDetail = null;
                TDSPaymentHeader.PaymentNumber = txtDocumentNumber.Text;
                TDSPaymentHeader.PaymentDate = txtDocumentDate.Text;
                TDSPaymentHeader.TransactionTypeCode = "841";
                TDSPaymentHeader.BranchCode = ddlBranch.SelectedValue;
                TDSPaymentHeader.VendorCode = ddlVendorCode.SelectedValue;
                TDSPaymentHeader.VendorName = txtVendorName.Text;
                TDSPaymentHeader.Location = txtVendorLocation.Text;
                TDSPaymentHeader.GSTINNumber = txtGSTINNumber.Text;
                TDSPaymentHeader.InvoiceValue = txtAmount.Text;
                TDSPaymentHeader.PaymentMode = ddlModeOfPayment.SelectedValue;
                TDSPaymentHeader.Chart_of_Account_Code = txtChartOfAccount.Text;
                TDSPaymentHeader.ChequeNumber = txtChequeNumber.Text;
                TDSPaymentHeader.ChequeDate = txtChequeDate.Text;
                TDSPaymentHeader.ChequeBank = txtBank.Text;
                TDSPaymentHeader.ChequeBranch = txtBranch.Text;
                TDSPaymentHeader.Narration = txtNarration.Text;

                int SNo = 0;
                foreach (GridViewRow gr in grvTDSPaymentDetails.Rows)
                {
                    CheckBox ChkSelected = (CheckBox)gr.FindControl("ChkSelected");

                    if (!ChkSelected.Checked)
                        continue;

                    TDSPaymentDetail = new VendorBookingDetail();
                    SNo += 1;

                    var txtBookingNumber = (TextBox)gr.Cells[1].FindControl("txtBookingNumber");
                    var txtBookingDate = (TextBox)gr.Cells[2].FindControl("txtBookingDate");
                    var txtReferenceNumber = (TextBox)gr.Cells[3].FindControl("txtReferenceNumber");
                    var txtReferenceDate = (TextBox)gr.Cells[4].FindControl("txtReferenceDate");
                    var txtBookingAmount = (TextBox)gr.Cells[5].FindControl("txtBookingAmount");
                    var txtTDSAmount = (TextBox)gr.Cells[6].FindControl("txtTDSAmount");
                    var txtPaidAmount = (TextBox)gr.Cells[7].FindControl("txtPaidAmount");
                    var txtPaymentAmount = (TextBox)gr.Cells[8].FindControl("txtPaymentAmount");
                    var txtRemarks = (TextBox)gr.Cells[9].FindControl("txtRemarks");

                    TDSPaymentDetail.SerialNumber = SNo.ToString();
                    TDSPaymentDetail.InvoiceNumber = txtBookingNumber.Text;
                    TDSPaymentDetail.InvoiceDate = txtBookingDate.Text;
                    TDSPaymentDetail.InvoiceValue = txtBookingAmount.Text;
                    TDSPaymentDetail.PaymentAmount = txtPaymentAmount.Text;
                    TDSPaymentDetail.TDSAmount = txtTDSAmount.Text;

                    TDSPaymentHeader.Items.Add(TDSPaymentDetail);
                }

                int result = tdsPayment.AddVendorPaymentEntry(ref TDSPaymentHeader);
                if (result == 1 && TDSPaymentHeader.ErrorMsg == string.Empty && TDSPaymentHeader.ErrorCode == "0")
                {
                    txtDocumentNumber.Text = TDSPaymentHeader.DocumentNumber;
                    imgEditToggle.Enabled = false;
                    ddlAccountingPeriod.Enabled = false;
                    ddlVendorCode.Enabled = false;
                    ddlModeOfPayment.Enabled = false;
                    txtAmount.Enabled = false;
                    BankAccNo.Visible = false;
                    tblBalanceAmount.Visible = false;
                    txtChequeNumber.Enabled = false;
                    txtDocumentDate.Enabled = false;
                    txtBank.Enabled = false;
                    txtBranch.Enabled = false;
                    txtNarration.Enabled = false;                    
                    grvTDSPaymentDetails.Enabled = false;
                    BtnSubmit.Enabled = false;
                    txtHdnGridCtrls.Attributes.Add("Style", "display:none");

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Vendor Payment Has Been Done Successfully.');", true);
                }
                else
                {
                    txtHdnGridCtrls.Attributes.Add("Style", "display:none");
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('" + TDSPaymentHeader.ErrorMsg + "');", true);                    
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(Source, ex);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Server.ClearError();
                Response.Redirect("TDSPayment.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                Log.WriteException(Source, ex);
            }
        }

        private void LoadAccountintPeriod()
        {
            AccountingPeriods Acc = new AccountingPeriods();
            ReceivableInvoice objReceivableInvoice = new ReceivableInvoice();
            List<string> lstAccountingPeriod = new List<string>();

            if (DateTime.Today.Month == 4)
            {
                lstAccountingPeriod.Add((DateTime.Today.Year - 1).ToString() + "-" + DateTime.Today.Year.ToString());
            }

            string accountingPeriod = GetCurrentFinancialYear();
            lstAccountingPeriod.Add(accountingPeriod);
            List<AccountingPeriod> AccordingPeriod = objReceivableInvoice.GetAccountingPeriod();
            List<AccountingPeriod> FinYear = AccordingPeriod.Where(p => lstAccountingPeriod.Contains(p.Desc)).OrderByDescending(c => c.AccPeriodCode).ToList();
            LoadDropDownLists<AccountingPeriod>(FinYear, ddlAccountingPeriod, "AccPeriodCode", "Desc", false, "");
        }

        protected void ddlAccountingPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            DebitCredit objDebitCredit = new DebitCredit();

            if (ddlAccountingPeriod.SelectedIndex == 0)
                txtDocumentDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            else
                txtDocumentDate.Text = objDebitCredit.GetDocumentDate(ddlAccountingPeriod.SelectedValue);
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

        protected void grvTDSPaymentDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox ChkSelected = (CheckBox)e.Row.FindControl("ChkSelected");
                    TextBox txtSNo = (TextBox)e.Row.FindControl("txtSNo");
                    TextBox txtBookingNumber = (TextBox)e.Row.FindControl("txtBookingNumber");
                    TextBox txtBookingDate = (TextBox)e.Row.FindControl("txtBookingDate");
                    TextBox txtReferenceNumber = (TextBox)e.Row.FindControl("txtReferenceNumber");
                    TextBox txtReferenceDate = (TextBox)e.Row.FindControl("txtReferenceDate");
                    TextBox txtBookingAmount = (TextBox)e.Row.FindControl("txtBookingAmount");
                    TextBox txtTDSAmount = (TextBox)e.Row.FindControl("txtTDSAmount");
                    TextBox txtPaidAmount = (TextBox)e.Row.FindControl("txtPaidAmount");
                    TextBox txtPaymentAmount = (TextBox)e.Row.FindControl("txtPaymentAmount");
                    TextBox txtRemarks = (TextBox)e.Row.FindControl("txtRemarks");
                    GridViewRow grdRow = ((GridViewRow)txtSNo.Parent.Parent);

                    txtPaymentAmount.Attributes.Add("OnChange", "return funPaymentAmountValidation('" + ChkSelected.ClientID + "','" + txtBookingAmount.ClientID + "','" + txtTDSAmount.ClientID + "','" + txtPaidAmount.ClientID + "','" + txtPaymentAmount.ClientID + "');");

                    sb.Append(ChkSelected.ClientID);
                    sb.Append(",");
                    sb.Append(txtBookingAmount.ClientID);
                    sb.Append(",");
                    sb.Append(txtTDSAmount.ClientID);
                    sb.Append(",");
                    sb.Append(txtPaidAmount.ClientID);
                    sb.Append(",");
                    sb.Append(txtPaymentAmount.ClientID);
                    sb.Append(",");

                    txtHdnGridCtrls.Text += sb.ToString();
                }                
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(TDSPayment), exp);
            }
        }
    }
}
