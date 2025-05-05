using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IMPALLibrary;
using IMPLLib = IMPALLibrary.Transactions;
using IMPALLibrary.Transactions.Finance;
using IMPALLibrary.Masters.Sales;
using IMPALWeb.UserControls;
using System.Web.Security;
using System.Web.Caching;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using IMPALLibrary.Masters;
using IMPALLibrary.Common;
using static IMPALLibrary.ReceivableInvoice;
using System.Globalization;
using IMPALLibrary.Masters.VendorDetails;

namespace IMPALWeb.Finance
{
    public partial class VendorPayment : System.Web.UI.Page
    {
        CashAndBankTransactions vendorPayment = new CashAndBankTransactions();
        private AccountingPeriods Acc = new AccountingPeriods();
        private ReceivableInvoice objReceivableInvoice = new ReceivableInvoice();

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(VendorPayment), exp);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!IsPostBack)
                {
                    btnBack.Visible = false;
                    Session["BranchCodeOthers"] = null;

                    if (crVendorPaymentReport != null)
                    {
                        crVendorPaymentReport.Dispose();
                        crVendorPaymentReport = null;
                    }

                    ddlVendorCodeOthers.Enabled = true;
                    ddlDocumentNumber.Visible = false;
                    LoadAccountintPeriod();
                    ddlBranch.SelectedValue = Session["BranchCode"].ToString();
                    ddlBranch.Enabled = false;
                    txtAccountingPeriod.Visible = false;
                    fnPopulateDropDown("VendorPayment", ddlModeOfPayment);
                    ddlModeOfPayment.Attributes.Add("OnChange", "funModeOfPayment();");
                    BtnSubmit.Attributes.Add("OnClick", "return funVendorSubmitValidation(2);");

                    BankAccNo.Visible = false;

                    LoadVendorForOtherBranches();
                    FirstGridViewRow(string.Empty);
                    txtAmount.Attributes.Add("OnChange", "return GetTotal();");

                    DateItem PrevDateStatus = objReceivableInvoice.GetPreviousDateStatus(Session["UserID"].ToString(), "VendorPayment");

                    if (PrevDateStatus.DateItemCode != "0")
                    {
                        txtDocumentDate.Enabled = true;
                        txtDocumentDate.Attributes.Add("onkeypress", "return false;");
                        txtDocumentDate.Attributes.Add("onkeyup", "return false;");
                        txtDocumentDate.Attributes.Add("onkeydown", "return false;");
                        txtDocumentDate.Attributes.Add("onpaste", "return false;");
                        txtDocumentDate.Attributes.Add("ondragstart", "return false;");
                        calTransactionDate.StartDate = DateTime.ParseExact(PrevDateStatus.DateItemDesc, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        calTransactionDate.EndDate = DateTime.ParseExact(PrevDateStatus.DateItemDesc, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    else
                        txtDocumentDate.Enabled = false;
                }                

                if (ddlDocumentNumber.Visible == false)
                {
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
                }

                if (ddlVendorCodeOthers.SelectedIndex > 0)
                {
                    Session["BranchCodeOthers"] = ddlVendorCodeOthers.SelectedValue.Substring(0, 3);
                    BankAccNo.Filter = "208";
                    ddlModeOfPayment.Items.Remove(ddlModeOfPayment.Items.FindByValue("CA"));
                }
                else if (ddlVendorCode.SelectedIndex > 0)
                {
                    Session["BranchCodeOthers"] = null;
                    BankAccNo.Filter = "036,208";
                }

                txtHdnGridCtrls.Attributes.Add("Style", "display:none");
            }
            catch (Exception ex)
            {
                Log.WriteException(Source, ex);
            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crVendorPaymentReport != null)
            {
                crVendorPaymentReport.Dispose();
                crVendorPaymentReport = null;
            }
        }
        protected void crVendorPaymentReport_Unload(object sender, EventArgs e)
        {
            if (crVendorPaymentReport != null)
            {
                crVendorPaymentReport.Dispose();
                crVendorPaymentReport = null;
            }
        }
        private void LoadVendorPaymentDocuments()
        {
            List<VendorBookingEntity> lstMiscBillNumber = new List<VendorBookingEntity>();
            lstMiscBillNumber = vendorPayment.GetVendorPaymentDocuments(Session["BranchCode"].ToString());
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
            grvVendorPaymentDetails.DataSource = dt;
            grvVendorPaymentDetails.DataBind();

            grvVendorPaymentDetails.Rows[0].Cells.Clear();
            grvVendorPaymentDetails.Rows[0].Cells.Add(new TableCell());
            grvVendorPaymentDetails.Rows[0].Cells[0].ColumnSpan = 10;
            grvVendorPaymentDetails.Rows[0].Cells[0].Text = strNoRowsFoundMsg;
            grvVendorPaymentDetails.Rows[0].Cells[0].CssClass = "EmptyRowStyle";
            ViewState["GridRowCount"] = "0";
            hdnRowCnt.Value = "0";
        }

        protected void imgEditToggle_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                LoadVendorPaymentDocuments();
                ddlDocumentNumber.Visible = true;
                txtDocumentNumber.Visible = false;
                grvVendorPaymentDetails.Enabled = false;
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
                ddlDocumentNumber.Visible = true;
                txtDocumentNumber.Visible = false;

                grvVendorPaymentDetails.Enabled = false;
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
                    txtAmount.Text = "";
                    txtChartOfAccount.Text = "";
                    ddlModeOfPayment.SelectedIndex = 0;
                    txtChequeNumber.Text = "";
                    txtChequeDate.Text = "";
                    txtBank.Text = "";
                    txtBranch.Text = "";

                    grvVendorPaymentDetails.DataSource = null;
                    grvVendorPaymentDetails.DataBind();

                    ddlDocumentNumber.Visible = true;
                    txtDocumentNumber.Visible = false;
                    ddlVendorCode.Enabled = false;
                    imgEditToggle.Visible = false;
                    tblBalanceAmount.Visible = false;
                }
                else
                {
                    VendorBookingEntity lstDocumentNumber = new VendorBookingEntity();
                    lstDocumentNumber = vendorPayment.GetVendorPaymentHeaderandDetails(Session["BranchCode"].ToString(), ddlDocumentNumber.SelectedValue.ToString());
                    
                    LoadAccountintPeriodView();

                    ddlVendorCode.DataSourceID = null;
                    VendorDetails vendor = new VendorDetails();
                    LoadDropDownLists<VendorDtls>(vendor.GetAllVendors(Session["BranchCode"].ToString()), ddlVendorCode, "Code", "Name", true, "");

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

                    grvVendorPaymentDetails.DataSource = (object)lstDocumentNumber.Items;
                    grvVendorPaymentDetails.DataBind();

                    foreach (GridViewRow gvr in grvVendorPaymentDetails.Rows)
                    {
                        if (gvr.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox ChkSelected = (CheckBox)gvr.FindControl("ChkSelected");
                            ChkSelected.Checked = true;
                            grvVendorPaymentDetails.Columns[9].Visible = false;
                        }
                    }

                    ddlVendorCode.Enabled = false;                    
                    grvVendorPaymentDetails.Enabled = false;                    
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(typeof(VendorPayment), ex);
            }
        }

        protected void ddlVendorCode_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlVendorCode.SelectedIndex <= 0)
                {
                    ddlVendorCodeOthers.Enabled = true;
                    BankAccNo.Visible = false;
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
                    Session["BranchCodeOthers"] = null;
                }
                else
                {
                    ddlVendorCodeOthers.Enabled = false;
                    Session["BranchCodeOthers"] = null;

                    Customers customers = new Customers();
                    Customer customer = customers.GetVendorInfoByVendorCode(ddlVendorCode.SelectedValue.Substring(0, 3), ddlVendorCode.SelectedValue);

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

                    List<VendorBookingDetail> lstVendorPaymentDetails = vendorPayment.GetVendorDocumentDetails(Session["BranchCode"].ToString(), ddlVendorCode.SelectedValue.Substring(0, 3), ddlVendorCode.SelectedValue);

                    if (lstVendorPaymentDetails.Count > 0)
                    {
                        tblBalanceAmount.Visible = true;
                        grvVendorPaymentDetails.DataSource = lstVendorPaymentDetails;
                        grvVendorPaymentDetails.DataBind();
                        //ChkHeader.Visible = true;
                    }
                    else
                    {
                        tblBalanceAmount.Visible = false;
                        FirstGridViewRow("No Records Found.");
                        //ChkHeader.Visible = false;
                    }

                    foreach (GridViewRow gvr in grvVendorPaymentDetails.Rows)
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

                    if (lstVendorPaymentDetails.Count > 0)
                        BankAccNo.Visible = true;
                    else
                        BankAccNo.Visible = false;

                    //hdnRowCnt.Value = lstVendorPaymentDetails.Count.ToString();
                }
            }
            catch (Exception exp)
            {

                Log.WriteException(typeof(VendorPayment), exp);
            }
        }

        protected void ddlVendorCodeOthers_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlVendorCodeOthers.SelectedIndex <= 0)
                {
                    ddlVendorCode.Enabled = true;
                    BankAccNo.Visible = false;
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
                    Session["BranchCodeOthers"] = null;
                }
                else
                {
                    Session["BranchCodeOthers"] = ddlVendorCodeOthers.SelectedValue.Substring(0, 3);

                    ddlVendorCode.Enabled = false;

                    Customers customers = new Customers();
                    Customer customer = customers.GetVendorInfoByVendorCode(ddlVendorCodeOthers.SelectedValue.Substring(0, 3), ddlVendorCodeOthers.SelectedValue);

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

                    List<VendorBookingDetail> lstVendorPaymentDetails = vendorPayment.GetVendorDocumentDetails(Session["BranchCode"].ToString(), ddlVendorCodeOthers.SelectedValue.Substring(0, 3), ddlVendorCodeOthers.SelectedValue);

                    if (lstVendorPaymentDetails.Count > 0)
                    {
                        tblBalanceAmount.Visible = true;
                        grvVendorPaymentDetails.DataSource = lstVendorPaymentDetails;
                        grvVendorPaymentDetails.DataBind();
                        //ChkHeader.Visible = true;
                    }
                    else
                    {
                        tblBalanceAmount.Visible = false;
                        FirstGridViewRow("No Records Found.");
                        //ChkHeader.Visible = false;
                    }

                    foreach (GridViewRow gvr in grvVendorPaymentDetails.Rows)
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

                    if (lstVendorPaymentDetails.Count > 0)
                        BankAccNo.Visible = true;
                    else
                        BankAccNo.Visible = false;

                    //hdnRowCnt.Value = lstVendorPaymentDetails.Count.ToString();
                }
            }
            catch (Exception exp)
            {

                Log.WriteException(typeof(VendorPayment), exp);
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

        private void LoadVendorForOtherBranches()
        {
            ddlVendorCodeOthers.DataSource = (object)vendorPayment.GetVendorBookingEntriesOtherBranches(Session["BranchCode"].ToString());
            ddlVendorCodeOthers.DataTextField = "ItemDesc";
            ddlVendorCodeOthers.DataValueField = "ItemCode";
            ddlVendorCodeOthers.DataBind();

            if (ddlVendorCodeOthers.Items.Count > 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Other Branch Vendor Bookings are in pending. Please complete the same.');", true);
                ddlVendorCodeOthers.Focus();
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

                if (txtChartOfAccount.Text.Substring(6, 4) == "0140")
                {
                    ddlModeOfPayment.DataSource = drop.Where(p => p.DisplayValue != "H").ToList();
                    ddlModeOfPayment.DataValueField = "DisplayValue";
                    ddlModeOfPayment.DataTextField = "DisplayText";
                    ddlModeOfPayment.DataBind();
                    ddlModeOfPayment.Items.Insert(0, new ListItem("", ""));

                    txtChequeNumber.Text = "";
                    txtChequeDate.Text = "";
                    txtBank.Text = "";
                    txtBranch.Text = "";
                }
                else
                {
                    if (Session["ChatDescription"].ToString() == "CASH ON HAND")
                    {
                        ddlModeOfPayment.DataSource = drop.Where(p => p.DisplayValue == "H").ToList(); ;
                        ddlModeOfPayment.DataValueField = "DisplayValue";
                        ddlModeOfPayment.DataTextField = "DisplayText";
                        ddlModeOfPayment.DataBind();

                        txtChequeNumber.Text = "";
                        txtChequeDate.Text = "";
                        txtBank.Text = "";
                        txtBranch.Text = "";
                    }
                    else
                    {
                        IMPLLib.ChequeSlip objChequeSlip = new IMPLLib.ChequeSlip();
                        IMPLLib.ChequeSlipBankDetails objBankDtl = new IMPLLib.ChequeSlipBankDetails();
                        objBankDtl = objChequeSlip.GetChequeSlipBankDetails(txtChartOfAccount.Text);
                        txtChequeDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                        txtBank.Text = objBankDtl.BankName;
                        txtBranch.Text = objBankDtl.Address;

                        ddlModeOfPayment.DataSource = drop.Where(p => p.DisplayValue != "H").ToList();
                        ddlModeOfPayment.DataValueField = "DisplayValue";
                        ddlModeOfPayment.DataTextField = "DisplayText";
                        ddlModeOfPayment.DataBind();
                    }
                }

                if (ddlDocumentNumber.Visible == false)
                {
                    if (ddlModeOfPayment.SelectedValue == "Q")
                    {
                        txtChequeNumber.Enabled = true;
                        txtChequeDate.Enabled = true;
                        txtBank.Enabled = false;
                        txtBranch.Enabled = false;
                    }
                    else
                    {
                        txtChequeNumber.Enabled = false;
                        txtChequeDate.Enabled = false;
                        txtBank.Enabled = false;
                        txtBranch.Enabled = false;
                    }
                }

                txtTotalBalanceAmount.Text = txtAmount.Text;

                foreach (GridViewRow gvr in grvVendorPaymentDetails.Rows)
                {
                    if (gvr.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkbox = (CheckBox)gvr.FindControl("ChkSelected");

                        if (chkbox != null)
                            chkbox.Checked = false;
                    }
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

                foreach (GridViewRow gvr in grvVendorPaymentDetails.Rows)
                {
                    if (gvr.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkbox = (CheckBox)gvr.FindControl("ChkSelected");

                        TextBox txtBookingAmount = (TextBox)gvr.FindControl("txtBookingAmount");
                        TextBox txtTDSAmount = (TextBox)gvr.FindControl("txtTDSAmount");
                        TextBox txtPaidAmount = (TextBox)gvr.FindControl("txtPaidAmount");
                        TextBox txtPaymentAmount = (TextBox)gvr.FindControl("txtPaymentAmount");                        

                        if (chkbox.Checked)
                        {
                            if (ChkCurrencyDots(txtPaymentAmount.Text))
                            {
                                if (string.IsNullOrEmpty(txtPaymentAmount.Text))
                                    txtPaymentAmount.Text = "0";
                            }

                            totalSum += Convert.ToDecimal(txtPaymentAmount.Text);                            
                            NoOfRowsChecked += 1;
                        }

                        txtTotalBalanceAmount.Text = (customerCollectionAmount - totalSum).ToString();
                        txtPaymentAmount.Text = (Convert.ToDecimal(txtBookingAmount.Text) - Convert.ToDecimal(txtTDSAmount.Text) - Convert.ToDecimal(txtPaidAmount.Text)).ToString();
                    }
                }

                hdnRowCnt.Value = NoOfRowsChecked.ToString(); 
                tblBalanceAmount.Visible = true;
                //ChkHeader.Visible = true;

                //if (grvVendorPaymentDetails.Rows.Count == NoOfRowsChecked)
                    //ChkHeader.Checked = true;
                //else
                    //ChkHeader.Checked = false;
            }
            catch (Exception exp)
            {

                Log.WriteException(typeof(VendorPayment), exp);
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Session.Remove("BranchCodeOthers");
                Session["BranchCodeOthers"] = null;

                VendorBookingEntity vendorpaymentHeader = new VendorBookingEntity();
                IMPALLibrary.Payable payableEntity = new IMPALLibrary.Payable();
                vendorpaymentHeader.Items = new List<VendorBookingDetail>();
                VendorBookingDetail vendorpaymentDetail = null;

                vendorpaymentHeader.PaymentNumber = txtDocumentNumber.Text;
                vendorpaymentHeader.PaymentDate = txtDocumentDate.Text;
                vendorpaymentHeader.TransactionTypeCode = "851";
                vendorpaymentHeader.BranchCode = txtVendorCode.Text.Substring(0, 3);
                vendorpaymentHeader.VendorCode = txtVendorCode.Text;
                vendorpaymentHeader.VendorName = txtVendorName.Text;
                vendorpaymentHeader.Location = txtVendorLocation.Text;
                vendorpaymentHeader.GSTINNumber = txtGSTINNumber.Text;
                vendorpaymentHeader.InvoiceValue = txtAmount.Text;
                vendorpaymentHeader.PaymentMode = ddlModeOfPayment.SelectedValue;
                vendorpaymentHeader.Chart_of_Account_Code = txtChartOfAccount.Text;
                vendorpaymentHeader.ChequeNumber = txtChequeNumber.Text;
                vendorpaymentHeader.ChequeDate = txtChequeDate.Text;
                vendorpaymentHeader.ChequeBank = txtBank.Text;
                vendorpaymentHeader.ChequeBranch = txtBranch.Text;
                vendorpaymentHeader.Narration = txtNarration.Text;
                vendorpaymentHeader.PaymentBranch = Session["BranchCode"].ToString();

                int SNo = 0;
                foreach (GridViewRow gr in grvVendorPaymentDetails.Rows)
                {
                    CheckBox ChkSelected = (CheckBox)gr.FindControl("ChkSelected");

                    if (!ChkSelected.Checked)
                        continue;

                    vendorpaymentDetail = new VendorBookingDetail();
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

                    vendorpaymentDetail.SerialNumber = SNo.ToString();
                    vendorpaymentDetail.InvoiceNumber = txtBookingNumber.Text;
                    vendorpaymentDetail.InvoiceDate = txtBookingDate.Text;
                    vendorpaymentDetail.InvoiceValue = txtBookingAmount.Text;
                    vendorpaymentDetail.PaymentAmount = txtPaymentAmount.Text;
                    vendorpaymentDetail.TDSAmount = txtTDSAmount.Text;
                    vendorpaymentDetail.Remarks = txtRemarks.Text;

                    vendorpaymentHeader.Items.Add(vendorpaymentDetail);
                }

                int result = vendorPayment.AddVendorPaymentEntry(ref vendorpaymentHeader);
                if (result == 1 && vendorpaymentHeader.ErrorMsg == string.Empty && vendorpaymentHeader.ErrorCode == "0")
                {
                    txtDocumentNumber.Text = vendorpaymentHeader.DocumentNumber;
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
                    grvVendorPaymentDetails.Enabled = false;
                    BtnSubmit.Enabled = false;
                    txtHdnGridCtrls.Attributes.Add("Style", "display:none");

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Vendor Payment Has Been Done Successfully.');", true);
                }
                else
                {
                    txtHdnGridCtrls.Attributes.Add("Style", "display:none");
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('" + vendorpaymentHeader.ErrorMsg + "');", true);                    
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
                Session.Remove("BranchCodeOthers");
                Session["BranchCodeOthers"] = null;

                Server.ClearError();
                Response.Redirect("VendorPayment.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                Log.WriteException(Source, ex);
            }
        }

        private void LoadAccountintPeriod()
        {
            List<string> lstAccountingPeriod = new List<string>();

            int PrevFinYearStatus = objReceivableInvoice.GetPreviousAccountingPeriodStatus(Session["UserID"].ToString(), "VendorPayment");

            if (PrevFinYearStatus > 0)
            {
                lstAccountingPeriod.Add((DateTime.Today.Year - 1).ToString() + "-" + DateTime.Today.Year.ToString());
            }

            string accountingPeriod = GetCurrentFinancialYear();
            lstAccountingPeriod.Add(accountingPeriod);
            txtDocumentDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

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

        protected void ddlAccountingPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            DebitCredit objDebitCredit = new DebitCredit();

            ddlAccountingPeriod.Enabled = false;

            if (ddlAccountingPeriod.SelectedIndex == 0)
                txtDocumentDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            else
                txtDocumentDate.Text = objDebitCredit.GetDocumentDate(ddlAccountingPeriod.SelectedValue);

            FirstGridViewRow(string.Empty);
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

        protected void grvVendorPaymentDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                int RowsChecked = 0;

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

                    if (ChkSelected.Checked)
                    {
                        RowsChecked += 1;
                    }

                    hdnRowCnt.Value = RowsChecked.ToString();
                }                
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(VendorPayment), exp);
            }
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            upHeader.Visible = false;
            btnBack.Visible = true;
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
                Response.Redirect("VendorPayment.aspx", false);
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
            string strCryPymtDate = default(string);
            string strPymtNumbField = default(string);
            string strPymtDateField = default(string);
            string strReportName = default(string);
            string strPaymentNumber = default(string);

            string strddlPaymentNumber = ddlDocumentNumber.SelectedValue;
            string strtxtPaymentNumber = txtDocumentNumber.Text;
            string strPaymentDate = txtDocumentDate.Text;
            string strBranchCode = ddlBranch.SelectedValue;

            if (!string.IsNullOrEmpty(strddlPaymentNumber))
                strPaymentNumber = strddlPaymentNumber;
            else
                strPaymentNumber = strtxtPaymentNumber;

            if (strBranchCode.ToUpper() == "COR")
                strReportName = "VendorPaymentCor";
            else
                strReportName = "VendorPayment";

            if (!string.IsNullOrEmpty(strPaymentDate))
                strCryPymtDate = "Date (" + DateTime.ParseExact(strPaymentDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";

            if (!(strPaymentNumber == "" || strPaymentNumber == "--Select--"))
            {
                strPymtNumbField = "{Vendor_Payment_Detail.Payment_Number}";
                strSelectionFormula = strPymtNumbField + "='" + strPaymentNumber + "'";
            }
            else
            {
                strPymtDateField = "{Vendor_Payment_Detail.Payment_Date}";
                strSelectionFormula = strPymtDateField + "=" + " " + "" + strCryPymtDate + "";
            }

            strSelectionFormula = strSelectionFormula + " and {Vendor_Payment_Detail.Branch_Code} ='" + strBranchCode + "'";

            crVendorPaymentReport.ReportName = strReportName;
            crVendorPaymentReport.RecordSelectionFormula = strSelectionFormula;
            crVendorPaymentReport.GenerateReportAndExportA4();
        }
    }
}
