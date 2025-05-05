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
    public partial class Receipts : System.Web.UI.Page
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
                Log.WriteException(typeof(Receipts), exp);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    btnBack.Visible = false;

                    if (crReceipt != null)
                    {
                        crReceipt.Dispose();
                        crReceipt = null;
                    }

                    hdnScreenMode.Value = "A";

                    ddlBranch.SelectedValue = Session["BranchCode"].ToString();
                    ddlBranch.Enabled = false;

                    SetDefaultValues();
                    ddlReceiptNumber.Visible = false;
                    ddlAccountingPeriod.Visible = true;
                    txtAccountPeriod.Visible = false;
                    FirstGridViewRow(string.Empty);
                    FreezeButtons(true);
                    ddlHoRefNo.Enabled = false;
                    txtAdvanceAmount.Text = "0.00";
                    txtAdvanceAmount.Attributes.Add("disabled", "True");
                    txtAdvanceAmount.Attributes.Add("OnChange", "return GetTotal();");

                    BtnSubmit.Enabled = true;
                    btnReset.Enabled = true;
                    btnReport.Enabled = true;
                    btnReportExcel.Enabled = true;

                    tblBalanceAmount.Visible = false;
                    ChkHeader.Visible = false;

                    txtChequeNumber.Enabled = false;
                    txtChequeDate.Enabled = false;
                    //ImgChequeDate.Enabled = false;
                    txtBank.Enabled = false;
                    txtBranch.Enabled = false;
                    ddlLocalOrOutStation.Enabled = false;                    

                    advChqslipDetls1.Visible = false;
                    advChqslipDetls2.Visible = false;

                    lblAbove180.Text = string.Empty;
                    lblAbove90.Text = string.Empty;
                    lblAbove60.Text = string.Empty;
                    lblAbove30.Text = string.Empty;
                    lblCurrentBal.Text = string.Empty;
                }
                
                //ddlModeOfReceipt.Attributes.Add("OnChange", "funModeOfReceipt();");
                BtnGetDocuments.Attributes.Add("OnClick", "return funGetDocuments();");
                BtnSubmit.Attributes.Add("OnClick", "return funFYRcvReceiptSubmit();");
                btnReset.Attributes.Add("OnClick", "return funFYRcvReceiptReset();");
                //btnReport.Attributes.Add("OnClick", "funShowMEP();");
                txtHdnGridCtrls.Attributes.Add("Style", "display:none");
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(Receipts), exp);
            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crReceipt != null)
            {
                crReceipt.Dispose();
                crReceipt = null;
            }
        }

        protected void crReceipt_Unload(object sender, EventArgs e)
        {
            if (crReceipt != null)
            {
                crReceipt.Dispose();
                crReceipt = null;
            }
        }

        protected void grvItemDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox ChkSelected = (CheckBox)e.Row.FindControl("ChkSelected");

                    TextBox txtDocumentValue = (TextBox)e.Row.FindControl("txtDocumentValue");
                    TextBox txtCollectionAmount = (TextBox)e.Row.FindControl("txtCollectionAmount");
                    DropDownList ddlPaymentIndicator = (DropDownList)e.Row.FindControl("ddlPaymentIndicator");

                    sb.Append(ChkSelected.ClientID);
                    sb.Append(",");
                    sb.Append(txtDocumentValue.ClientID);
                    sb.Append(",");
                    sb.Append(txtCollectionAmount.ClientID);
                    sb.Append(",");
                    sb.Append(ddlPaymentIndicator.ClientID);
                    sb.Append("|");

                    txtCollectionAmount.Enabled = false;

                    txtCollectionAmount.Attributes.Add("OnChange", "return funCollectionAmountValidation('" + ChkSelected.ClientID + "','" + txtDocumentValue.ClientID + "','" + txtCollectionAmount.ClientID + "','" + ddlPaymentIndicator.ClientID + "');");

                    txtHdnGridCtrls.Text += sb.ToString();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(Receipts), exp);
            }
        }

        protected void ddlReceiptNumber_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlHoRefNo.Items.Clear();
                ddlCustomer.Items.Clear();
                ddlCustomer.DataSourceID = null;

                Customers customers = new Customers();
                List<Customer> lstCustomers = new List<Customer>();
                lstCustomers = customers.GetAllCustomersExisting(Session["BranchCode"].ToString());
                ddlCustomer.DataSource = lstCustomers;
                ddlCustomer.DataTextField = "Customer_Name";
                ddlCustomer.DataValueField = "Customer_Code";
                ddlCustomer.DataBind();

                hdnScreenMode.Value = "E";                

                ReceiptTransactions receiptTransactions = new ReceiptTransactions();
                ReceiptEntity receiptEntity = receiptTransactions.GetReceiptsDetailsByNumber(Session["BranchCode"].ToString(), ddlReceiptNumber.SelectedValue, "A");

                ddlReceiptNumber.SelectedValue = receiptEntity.ReceiptNumber;
                txtReceiptNumber.Text = receiptEntity.ReceiptNumber;

                txtReceiptDate.Text = receiptEntity.ReceiptDate;
                txtAccountPeriod.Text = receiptEntity.AccountingPeriodDesc;
                //ddlAccountingPeriod.SelectedValue = receiptEntity.AccountingPeriod;
                ddlCustomer.SelectedValue = receiptEntity.CustomerCode;
                ddlModeOfReceipt.SelectedValue = receiptEntity.PaymentType;
                txtAmount.Text = receiptEntity.Amount;
                txtTempRecptNumber.Text = receiptEntity.TempReceiptNumber;
                txtTempRecptDate.Text = receiptEntity.TempReceiptDate;
                txtAdvanceAmount.Text = TwoDecimalConversion(receiptEntity.AdvanceAmount);
                txtAdvanceChequeSlipNo.Text = receiptEntity.AdvanceChequeSlipNumber;

                if (Convert.ToDecimal(receiptEntity.AdvanceAmount) > 0 && txtAdvanceChequeSlipNo.Text.Trim() != "")
                {
                    advChqslipDetls1.Visible = true;
                    advChqslipDetls2.Visible = true;
                }
                else
                {
                    advChqslipDetls1.Visible = false;
                    advChqslipDetls2.Visible = false;
                }

                txtChequeNumber.Text = receiptEntity.ChequeNumber;
                txtChequeDate.Text = receiptEntity.ChequeDate;
                txtBank.Text = receiptEntity.ChequeBank;
                txtBranch.Text = receiptEntity.ChequeBranch;
                ddlLocalOrOutStation.SelectedValue = receiptEntity.LocalOrOutstation;
                ddlHoRefNo.Items.Add(receiptEntity.Remarks);

                txtCode.Text = receiptEntity.CustomerCode;
                txtLocation.Text = receiptEntity.Location;
                txtAddress1.Text = receiptEntity.Address1;
                txtAddress2.Text = receiptEntity.Address2;
                txtAddress3.Text = receiptEntity.Address3;
                txtAddress4.Text = receiptEntity.Address4;

                txtFromDate.Text = receiptEntity.FromDate;
                txtToDate.Text = receiptEntity.ToDate;

                //BindExistingRows(receiptEntity.Items);

                grvItemDetails.DataSource = (object)receiptEntity.Items;
                grvItemDetails.DataBind();
                grvItemDetails.Columns[0].Visible = false;

                foreach (GridViewRow gr in grvItemDetails.Rows)
                {
                    gr.Enabled = false;
                }

                ddlAccountingPeriod.Enabled = false;
                ddlCustomer.Enabled = false;
                ddlBranch.Enabled = false;
                txtAmount.Enabled = false;
                txtAdvanceChequeSlipNo.Enabled = false;
                BtnSubmit.Enabled = false;
                BtnSubmit.Visible = false;
                btnReset.Enabled = true;
                btnReport.Enabled = true;
                btnReportExcel.Enabled = true;
                ddlHoRefNo.Enabled = false;

                txtTempRecptNumber.Enabled = false;
                txtTempRecptDate.Enabled = false;
                txtChequeNumber.Enabled = false;
                txtChequeDate.Enabled = false;
                txtBank.Enabled = false;
                txtBranch.Enabled = false;
                ddlLocalOrOutStation.Enabled = false;
                ddlModeOfReceipt.Enabled = false;

                tblDocDetails.Visible = false;
                tblBalanceAmount.Visible = false;
                ChkHeader.Visible = false;

                UpdpanelTop.Update();
                UpdPanelGrid.Update();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(Receipts), exp);
            }
        }

        protected void imgEditToggle_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnScreenMode.Value == "A")
                {
                    hdnScreenMode.Value = "E";
                    ddlReceiptNumber.SelectedValue = "0";
                    ddlReceiptNumber.Visible = true;
                    txtReceiptNumber.Visible = false;

                    ddlBranch.Enabled = false;
                    btnReport.Enabled = false;
                    btnReportExcel.Enabled = false;
                    BtnSubmit.Enabled = false;
                    tblDocDetails.Visible = false;
                    tblBalanceAmount.Visible = false;
                    ChkHeader.Visible = false;

                    FirstGridViewRow(string.Empty);
                    txtReceiptNumber.Text = string.Empty;
                    txtReceiptDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    ddlCustomer.SelectedValue = "0";
                    txtAmount.Text = "";
                    txtTempRecptNumber.Text = string.Empty;
                    txtTempRecptDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    txtChequeNumber.Text = string.Empty;
                    txtChequeDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    txtBank.Text = string.Empty;
                    txtBranch.Text = string.Empty;
                    txtAccountPeriod.Visible = true;
                    ddlAccountingPeriod.Visible = false;

                    txtCode.Text = string.Empty;
                    txtLocation.Text = string.Empty;
                    txtAddress1.Text = string.Empty;
                    txtAddress2.Text = string.Empty;
                    txtAddress3.Text = string.Empty;
                    txtAddress4.Text = string.Empty;

                    BtnSubmit.Attributes.Add("OnClick", "return funFYRcvReceiptSubmit();");
                    tblBalanceAmount.Visible = false;
                    ChkHeader.Visible = false;

                    imgEditToggle.Visible = false;
                }
                else if (hdnScreenMode.Value == "E")
                {
                    hdnScreenMode.Value = "A";
                    ddlReceiptNumber.Visible = false;
                    txtReceiptNumber.Visible = true;

                    ddlBranch.Enabled = true;
                    ddlAccountingPeriod.Visible = true;
                    ddlAccountingPeriod.Enabled = true;
                    txtAccountPeriod.Visible = false;
                    ddlCustomer.Enabled = true;

                    tblDocDetails.Visible = true;
                    txtAmount.Enabled = true;

                    //txtFromDate.Text = DateTime.Today.Add(TimeSpan.FromDays(-1096)).ToString("dd/MM/yyyy");
                    txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    btnReport.Enabled = false;
                    btnReportExcel.Enabled = false;

                    FirstGridViewRow(string.Empty);
                    txtReceiptNumber.Text = string.Empty;
                    txtReceiptDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    //ddlAccountingPeriod.SelectedValue = "0";
                    ddlCustomer.SelectedValue = "0";
                    txtAmount.Text = "";
                    txtTempRecptNumber.Text = string.Empty;
                    txtTempRecptDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
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

                    BtnSubmit.Attributes.Add("OnClick", "return funFYRcvReceiptSubmit();");
                    tblBalanceAmount.Visible = false;
                    ChkHeader.Visible = false;

                    FreezeButtons(true);
                }

                UpdpanelTop.Update();
                UpdPanelGrid.Update();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(Receipts), exp);
            }
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            UpdpanelTop.Visible = false;
            btnReport.Visible = false;
            btnReportExcel.Visible = false;
            btnReset.Visible = true;
            btnReset.Enabled = true;
            GenerateSelectionFormula(".pdf");
        }

        protected void btnReportExcel_Click(object sender, EventArgs e)
        {
            UpdpanelTop.Visible = false;
            btnReport.Visible = false;
            btnReportExcel.Visible = false;
            btnReset.Visible = true;
            btnReset.Enabled = true;
            GenerateSelectionFormula(".xls");
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                BtnSubmit.Attributes.Add("style","display:none");
                BtnSubmit.Visible = false;
                BtnSubmit.Enabled = false;
                ddlAccountingPeriod.Enabled = false;
                SubmitHeaderAndItems();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(Receipts), exp);
            }
        }
        protected void GenerateSelectionFormula(string fileType)
        {
            string strSelectionFormula = default(string);
            string strReceiptNumberField = default(string);
            string strReceiptDateField = default(string);
            string strReceiptBranchField = default(string);
            string strCryDate = default(string);
            string strReceiptNumber = default(string);

            if (ddlReceiptNumber.SelectedValue != "" && ddlReceiptNumber.SelectedValue != null)
                strReceiptNumber = ddlReceiptNumber.SelectedValue;
            else
                strReceiptNumber = txtReceiptNumber.Text;

            strReceiptBranchField = "{Collection_Header.Branch_Code}";
            strReceiptNumberField = "{Collection_Header.Receipt_Number}";
            strReceiptDateField = "{Collection_Header.Receipt_date}";
            strCryDate = "Date (" + DateTime.ParseExact(txtReceiptDate.Text, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";

            if (!string.IsNullOrEmpty(strReceiptNumber))
            {

                strSelectionFormula = strReceiptBranchField + "='" + Session["BranchCode"].ToString() + "' and " + strReceiptNumberField + "='" + strReceiptNumber + "'";
            }
            else
            {
                strSelectionFormula = strReceiptBranchField + "='" + Session["BranchCode"].ToString() + "' and " + strReceiptDateField + "=" + " " + "" + strCryDate + "";
            }

            crReceipt.RecordSelectionFormula = strSelectionFormula;
            crReceipt.GenerateReportAndExportA4(fileType);
        }

        protected void BtnGetDocuments_Click(object sender, EventArgs e)
        {
            try
            {
                ReceiptTransactions receiptTransactions = new ReceiptTransactions();
                List<ReceiptItem> docDetails = receiptTransactions.GetDocumentDetails(txtFromDate.Text, txtToDate.Text, ddlCustomer.SelectedValue, Session["BranchCode"].ToString());

                ChkHeader.Checked = false;
                grvItemDetails.DataSource = docDetails;
                grvItemDetails.DataBind();
                grvItemDetails.Columns[0].Visible = true;

                ddlAccountingPeriod.Attributes.Add("disabled", "True");
                ddlCustomer.Attributes.Add("disabled", "True");
                ddlModeOfReceipt.Attributes.Add("disabled", "True");
                ddlBranch.Attributes.Add("disabled", "True");
                txtAmount.Attributes.Add("disabled", "True");
                ddlHoRefNo.Attributes.Add("disabled", "True");
                txtAdvanceAmount.Attributes.Add("disabled", "True");
                txtTempRecptNumber.Attributes.Add("disabled", "True");
                txtTempRecptDate.Attributes.Add("disabled", "True");
                txtTotalBalanceAmount.Text = txtAmount.Text;

                if (docDetails.Count > 0)
                {
                    tblBalanceAmount.Visible = true;
                    hdnRowCnt.Value = docDetails.Count.ToString();
                    ChkHeader.Visible = true;
                }
                else
                {
                    tblBalanceAmount.Visible = false;
                    FirstGridViewRow("No Records Found.");
                    ChkHeader.Visible = false;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(Receipts), exp);
            }
        }

        protected void BtnReset_Click(object sender, EventArgs e)
        {
            try
            {
                Server.ClearError();
                Response.Redirect("Receipts.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(Receipts), exp);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                Server.ClearError();
                Response.Redirect("Receipts.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {

                Log.WriteException(typeof(Receipts), exp);
            }
        }

        protected void ddlCustomer_OnDataBound(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl = sender as DropDownList;
                if (ddl != null)
                {
                    foreach (ListItem li in ddl.Items)
                    {
                        li.Attributes["title"] = li.Text;
                    }
                }
            }
            catch (Exception exp)
            {

                Log.WriteException(typeof(Receipts), exp);
            }
        }
        protected void ChkSelected_OnCheckedChanged(object sender, EventArgs e)
        {
            try
            {
                decimal totalSum = 0;
                decimal customerCollectionAmount = Convert.ToDecimal(txtAmount.Text);
                int NoOfRowsChecked = 0;

                foreach (GridViewRow gvr in grvItemDetails.Rows)
                {
                    if (gvr.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkbox = (CheckBox)gvr.FindControl("ChkSelected");

                        TextBox txtCollectionAmount = (TextBox)gvr.FindControl("txtCollectionAmount");
                        TextBox txtDocumentValue = (TextBox)gvr.FindControl("txtDocumentValue");
                        TextBox txtBalanceAmount = (TextBox)gvr.FindControl("txtBalanceAmount");
                        DropDownList ddlPaymentIndicator = (DropDownList)gvr.FindControl("ddlPaymentIndicator");


                        if (chkbox.Checked)
                        {
                            if (ChkCurrencyDots(txtCollectionAmount.Text))
                            {
                                if (string.IsNullOrEmpty(txtCollectionAmount.Text))
                                    txtCollectionAmount.Text = "0";

                                totalSum += Convert.ToDecimal(txtCollectionAmount.Text);
                                txtBalanceAmount.Text = (Convert.ToDecimal(txtDocumentValue.Text) - Convert.ToDecimal(txtCollectionAmount.Text)).ToString();
                            }
                            else
                            {
                                txtCollectionAmount.Text = txtDocumentValue.Text;
                                totalSum += Convert.ToDecimal(txtCollectionAmount.Text);
                                txtBalanceAmount.Text = (Convert.ToDecimal(txtDocumentValue.Text) - Convert.ToDecimal(txtCollectionAmount.Text)).ToString();
                            }
                            txtCollectionAmount.Enabled = true;
                            NoOfRowsChecked += 1;
                        }
                        else
                        {
                            txtCollectionAmount.Text = txtDocumentValue.Text;
                            txtBalanceAmount.Text = "0.00";
                            txtCollectionAmount.Enabled = false;
                            //ddlPaymentIndicator.SelectedValue = "";
                        }
                    }
                }

                txtTotalBalanceAmount.Text = (customerCollectionAmount - totalSum).ToString();
                tblBalanceAmount.Visible = true;
                ChkHeader.Visible = true;

                if (grvItemDetails.Rows.Count == NoOfRowsChecked)
                    ChkHeader.Checked = true;
                else
                    ChkHeader.Checked = false;
            }
            catch (Exception exp)
            {

                Log.WriteException(typeof(Receipts), exp);
            }
        }

        protected void ChkHeader_OnCheckedChanged(object sender, EventArgs e)
        {
            try
            {
                decimal totalSum = 0;
                decimal customerCollectionAmount = Convert.ToDecimal(txtAmount.Text);

                if (ChkHeader.Checked)
                {
                    foreach (GridViewRow gvr in grvItemDetails.Rows)
                    {
                        if (gvr.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox ChkSelected = (CheckBox)gvr.FindControl("ChkSelected");

                            TextBox txtCollectionAmount = (TextBox)gvr.FindControl("txtCollectionAmount");
                            TextBox txtDocumentValue = (TextBox)gvr.FindControl("txtDocumentValue");
                            TextBox txtBalanceAmount = (TextBox)gvr.FindControl("txtBalanceAmount");
                            DropDownList ddlPaymentIndicator = (DropDownList)gvr.FindControl("ddlPaymentIndicator");

                            ChkSelected.Checked = true;

                            if (ChkCurrencyDots(txtCollectionAmount.Text))
                            {
                                if (string.IsNullOrEmpty(txtCollectionAmount.Text))
                                    txtCollectionAmount.Text = "0";

                                totalSum += Convert.ToDecimal(txtCollectionAmount.Text);
                                txtBalanceAmount.Text = (Convert.ToDecimal(txtDocumentValue.Text) - Convert.ToDecimal(txtCollectionAmount.Text)).ToString();
                            }
                            else
                            {
                                txtCollectionAmount.Text = txtDocumentValue.Text;
                                totalSum += Convert.ToDecimal(txtCollectionAmount.Text);
                                txtBalanceAmount.Text = (Convert.ToDecimal(txtDocumentValue.Text) - Convert.ToDecimal(txtCollectionAmount.Text)).ToString();
                            }

                            txtCollectionAmount.Enabled = true;
                        }
                    }
                }
                else//unchecked
                {
                    foreach (GridViewRow gvr in grvItemDetails.Rows)
                    {
                        if (gvr.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox ChkSelected = (CheckBox)gvr.FindControl("ChkSelected");

                            TextBox txtCollectionAmount = (TextBox)gvr.FindControl("txtCollectionAmount");
                            TextBox txtDocumentValue = (TextBox)gvr.FindControl("txtDocumentValue");
                            TextBox txtBalanceAmount = (TextBox)gvr.FindControl("txtBalanceAmount");
                            DropDownList ddlPaymentIndicator = (DropDownList)gvr.FindControl("ddlPaymentIndicator");

                            ChkSelected.Checked = false;
                            txtCollectionAmount.Text = txtDocumentValue.Text;
                            txtBalanceAmount.Text = "0.00";
                            txtCollectionAmount.Enabled = false;
                            //ddlPaymentIndicator.SelectedValue = "";
                        }
                    }
                }

                txtTotalBalanceAmount.Text = (customerCollectionAmount - totalSum).ToString();
                tblBalanceAmount.Visible = true;
                ChkHeader.Visible = true;
            }
            catch (Exception exp)
            {

                Log.WriteException(typeof(Receipts), exp);
            }
        }

        protected void ddlAccountingPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReceivableInvoice receivableReceipt = new ReceivableInvoice();

            ddlAccountingPeriod.Enabled = false;

            if (ddlAccountingPeriod.SelectedIndex == 0)
            {
                txtReceiptDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                fnPopulateDropDown("ReceivableReceipt", ddlModeOfReceipt);
            }
            else
            {
                txtReceiptDate.Text = receivableReceipt.GetDocumentDate(ddlAccountingPeriod.SelectedValue);
                fnPopulateDropDown("ReceivableReceiptNEFT", ddlModeOfReceipt);
                ddlModeOfReceipt_OnSelectedIndexChanged(ddlModeOfReceipt, EventArgs.Empty);
            }            

            FirstGridViewRow(string.Empty);
            FreezeButtons(true);
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

                //ReceivableInvoice receivableReceipt = new ReceivableInvoice();
                //Customer customer = receivableReceipt.GetCustomerInfoByCustomerCode(ddlCustomer.SelectedValue, Session["BranchCode"].ToString());

                ReceivableInvoice oSales = new ReceivableInvoice();
                CustomerOutstandingDetails CustOSdetails = oSales.GetCustomerOutstandingReceipts(Session["BranchCode"].ToString(), ddlCustomer.SelectedValue);

                if (CustOSdetails.ChqDishonorInd == 0)
                    fnPopulateDropDown("ReceivableReceipt", ddlModeOfReceipt);
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('This Customer is having Cheque Dishonor Count of " + CustOSdetails.ChqDishonorCnt + " and hence Cheque Mode is Disabled.');", true);
                    fnPopulateDropDown("ReceivableReceiptChqDishonor", ddlModeOfReceipt);
                }

                //ddlModeOfReceipt.SelectedValue = "CA";
                txtTempRecptNumber.Text = string.Empty;
                txtAmount.Text = string.Empty;
                txtTempRecptDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtChequeNumber.Text = string.Empty;
                txtChequeDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtBank.Text = string.Empty;
                txtBranch.Text = string.Empty;
                tblBalanceAmount.Visible = false;
                ChkHeader.Visible = false;

                txtCode.Text = ddlCustomer.SelectedValue;
                txtLocation.Text = CustOSdetails.Location;
                txtAddress1.Text = CustOSdetails.Address1;
                txtAddress2.Text = CustOSdetails.Address2;
                txtAddress3.Text = CustOSdetails.Address3;
                txtAddress4.Text = CustOSdetails.Address4;
                
                txtCreditLimit.Text = TwoDecimalConversion(CustOSdetails.Credit_Limit);
                txtOutstanding.Text = TwoDecimalConversion(CustOSdetails.Outstanding);
                txtAbove180.Text = TwoDecimalConversion(CustOSdetails.Above180);
                txtAbove90.Text = TwoDecimalConversion(CustOSdetails.Above90);
                txtAbove60.Text = TwoDecimalConversion(CustOSdetails.Above60);
                txtAbove30.Text = TwoDecimalConversion(CustOSdetails.Above30);
                txtCurrentBal.Text = TwoDecimalConversion(CustOSdetails.CurBal);
                txtCrBal.Text = TwoDecimalConversion(CustOSdetails.Cr_Bal);

                lblAbove180.Text = CustOSdetails.Above180MonthName;
                lblAbove90.Text = CustOSdetails.Above90MonthName;
                lblAbove60.Text = CustOSdetails.Above60MonthName;
                lblAbove30.Text = CustOSdetails.Above30MonthName;
                lblCurrentBal.Text = CustOSdetails.CurBalMonthName;

                FirstGridViewRow(string.Empty);

                txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");//DateTime.Today.Add(TimeSpan.FromDays(-1096)).ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            }
            catch (Exception exp)
            {

                Log.WriteException(typeof(Receipts), exp);
            }
        }
        
        private void SetDefaultValues()
        {
            LoadAccountintPeriod();
            txtReceiptNumber.Text = string.Empty;
            ddlReceiptNumber.SelectedValue = "0";
            ddlCustomer.SelectedValue = "0";

            txtTempRecptNumber.Text = string.Empty;
            txtTempRecptDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
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

            txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
        }
		protected void ddlModeOfReceipt_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlHoRefNo.Items.Clear();
                txtAmount.Text = "";

                if (ddlModeOfReceipt.SelectedValue.ToString().ToUpper() == "CA" || ddlModeOfReceipt.SelectedValue.ToString().ToUpper() == "CH")
                {
                    txtAmount.AutoPostBack = false;
                    ddlHoRefNo.Enabled = false;

                    if (ddlModeOfReceipt.SelectedValue.ToString().ToUpper() == "CH")
                    {
                        txtChequeNumber.AutoPostBack = true;
                    }
                }
                else
                {
                    txtAmount.AutoPostBack = true;
                    ddlHoRefNo.Enabled = true;
                }

                txtChequeNumber.Text = "";
                txtChequeDate.Text = "";
                txtBank.Text = "";
                txtBranch.Text = "";
                txtTempRecptNumber.Text = "";
                txtTempRecptDate.Text = "";
                ddlLocalOrOutStation.Text = "L";

                if (ddlModeOfReceipt.SelectedValue.ToString().ToUpper() == "CA" || ddlModeOfReceipt.SelectedValue.ToString().ToUpper() == "DR")
                {
                    txtChequeNumber.Enabled = false;
                    txtChequeDate.Enabled = false;
                    txtBank.Enabled = false;
                    txtBranch.Enabled = false;
                    ddlLocalOrOutStation.Enabled = false;
                }
                else
                {
                    txtChequeNumber.Enabled = true;
                    txtChequeDate.Enabled = true;
                    txtBank.Enabled = true;
                    txtBranch.Enabled = true;
                    ddlLocalOrOutStation.Enabled = true;
                }

                FirstGridViewRow(string.Empty);
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesInvoiceEntry), exp);
            }
        }

        protected void txtAmount_OnTextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtAmount.Text != null && txtAmount.Text != "")
                {
                    if (ddlModeOfReceipt.SelectedValue.ToString().ToUpper() == "DR")
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

                FirstGridViewRow(string.Empty);
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesInvoiceEntry), exp);
            }
        }

        protected void txtChequeNumber_OnTextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtChequeNumber.Text.Trim() != null && txtChequeNumber.Text.Trim() != "")
                {
                    int ChqStatus = objTrans.GetExistingChequeEntryStatus(ddlBranch.SelectedValue, ddlCustomer.SelectedValue, txtChequeNumber.Text, txtAmount.Text, ddlAccountingPeriod.SelectedValue);

                    if (ChqStatus == 1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('This Cheque Number is Already Exists for the Customer. Please Check Once.');", true);
                        txtChequeNumber.Text = "";
                        txtChequeNumber.Focus();
                        return;
                    }
                }

                FirstGridViewRow(string.Empty);
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesInvoiceEntry), exp);
            }
        }

        private List<IMPALLibrary.Transactions.Item> GetHORefDetails()
        {
            List<IMPALLibrary.Transactions.Item> obj = objTrans.GetHOReceiptRefDetails(ddlBranch.SelectedValue, ddlCustomer.SelectedValue, ddlAccountingPeriod.SelectedValue, txtAmount.Text);
            return obj;
        }

        protected void ddlHoRefNo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FirstGridViewRow(string.Empty);
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesInvoiceEntry), exp);
            }
        }
        private void FreezeButtons(bool Fzflag)
        {
            DivOuter.Disabled = !Fzflag;
            imgEditToggle.Enabled = Fzflag;
            BtnGetDocuments.Enabled = Fzflag;
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

            int PrevFinYearStatus = objReceivableInvoice.GetPreviousAccountingPeriodStatus(Session["UserID"].ToString(), "ReceivableReceipt");

            if (PrevFinYearStatus > 0)
            {
                lstAccountingPeriod.Add((DateTime.Today.Year - 1).ToString() + "-" + DateTime.Today.Year.ToString());
            }

            string accountingPeriod = GetCurrentFinancialYear();
            lstAccountingPeriod.Add(accountingPeriod);
            txtReceiptDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            List<AccountingPeriod> AccordingPeriod = objReceivableInvoice.GetAccountingPeriod();
            List<AccountingPeriod> FinYear = AccordingPeriod.Where(p => lstAccountingPeriod.Contains(p.Desc)).OrderByDescending(c => c.AccPeriodCode).ToList();
            LoadDropDownLists<AccountingPeriod>(FinYear, ddlAccountingPeriod, "AccPeriodCode", "Desc", false, "");
        }

        private void LoadAccountintPeriodView()
        {
            AccountingPeriods Acc = new AccountingPeriods();
            ReceivableInvoice objReceivableInvoice = new ReceivableInvoice();
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

        private void FirstGridViewRow(string strNoRowsFoundMsg)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("SNo", typeof(string)));
            dt.Columns.Add(new DataColumn("ReferenceType", typeof(string)));
            dt.Columns.Add(new DataColumn("ReferenceDocumentNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("ReferenceDocumentNumber1", typeof(string)));
            dt.Columns.Add(new DataColumn("DocumentDate", typeof(string)));
            dt.Columns.Add(new DataColumn("DocumentValue", typeof(string)));
            dt.Columns.Add(new DataColumn("CollectionAmount", typeof(string)));
            dt.Columns.Add(new DataColumn("BalanceAmount", typeof(string)));
            dt.Columns.Add(new DataColumn("PaymentIndicator", typeof(string)));
            DataRow dr = null;
            for (int i = 0; i < 1; i++)
            {
                dr = dt.NewRow();
                dr["SNo"] = i + 1;
                dr["ReferenceType"] = string.Empty;
                dr["ReferenceDocumentNumber"] = string.Empty;
                dr["ReferenceDocumentNumber1"] = string.Empty;
                dr["DocumentDate"] = string.Empty;
                dr["DocumentValue"] = string.Empty;
                dr["CollectionAmount"] = string.Empty;
                dr["BalanceAmount"] = string.Empty;
                dr["PaymentIndicator"] = string.Empty;
                dt.Rows.Add(dr);
            }

            ViewState["CurrentTable"] = dt;
            grvItemDetails.DataSource = dt;
            grvItemDetails.DataBind();

            grvItemDetails.Rows[0].Cells.Clear();
            grvItemDetails.Rows[0].Cells.Add(new TableCell()); //add a new blank cell
            grvItemDetails.Rows[0].Cells[0].ColumnSpan = 9;
            //grvItemDetails.Rows[0].Cells[0].Text = "No records found";
            grvItemDetails.Rows[0].Cells[0].Text = strNoRowsFoundMsg;
            grvItemDetails.Rows[0].Cells[0].CssClass = "EmptyRowStyle";
            hdnRowCnt.Value = "0";
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
                receiptEntity.ReceiptNumber = txtReceiptNumber.Text;
                receiptEntity.ReceiptDate = txtReceiptDate.Text;
                receiptEntity.CustomerCode = ddlCustomer.SelectedValue;
                receiptEntity.PaymentType = ddlModeOfReceipt.SelectedValue;
                receiptEntity.Amount = (Convert.ToDecimal(txtAmount.Text) - Convert.ToDecimal(txtAdvanceAmount.Text)).ToString();
                receiptEntity.AdvanceAmount = txtAdvanceAmount.Text;
                receiptEntity.TempReceiptNumber = txtTempRecptNumber.Text;
                receiptEntity.TempReceiptDate = txtTempRecptDate.Text;
                receiptEntity.ChequeNumber = txtChequeNumber.Text;
                receiptEntity.ChequeDate = txtChequeDate.Text;
                receiptEntity.ChequeBank = txtBank.Text;
                receiptEntity.ChequeBranch = txtBranch.Text;
                receiptEntity.LocalOrOutstation = ddlLocalOrOutStation.SelectedValue;

                if (ddlModeOfReceipt.SelectedValue == "DR")
                {
                    receiptEntity.HORefNo = ddlHoRefNo.SelectedValue;
                    receiptEntity.Remarks = ddlHoRefNo.SelectedItem.Text;
                }
                else
                {
                    receiptEntity.HORefNo = "";
                    receiptEntity.Remarks = "";
                }

                ReceiptItem receiptItem = null;
                int SNo = 0;
                decimal dmlTotal = 0;

                foreach (GridViewRow gr in grvItemDetails.Rows)
                {
                    CheckBox ChkSelected = (CheckBox)gr.FindControl("ChkSelected");

                    if (!ChkSelected.Checked)
                        continue;

                    receiptItem = new ReceiptItem();
                    SNo += 1;

                    TextBox txtReferenceType = (TextBox)gr.FindControl("txtReferenceType");
                    TextBox txtReferenceDocNumber = (TextBox)gr.FindControl("txtReferenceDocNumber");
                    TextBox txtReferenceDocNumber1 = (TextBox)gr.FindControl("txtReferenceDocNumber1");
                    TextBox txtDocumentDate = (TextBox)gr.FindControl("txtDocumentDate");
                    TextBox txtDocumentValue = (TextBox)gr.FindControl("txtDocumentValue");
                    TextBox txtCollectionAmount = (TextBox)gr.FindControl("txtCollectionAmount");
                    TextBox txtBalanceAmount = (TextBox)gr.FindControl("txtBalanceAmount");
                    DropDownList ddlPaymentIndicator = (DropDownList)gr.FindControl("ddlPaymentIndicator");

                    dmlTotal += Convert.ToDecimal(txtCollectionAmount.Text);

                    receiptItem.SNO = SNo.ToString();
                    receiptItem.ReferenceType = txtReferenceType.Text.Trim();
                    receiptItem.ReferenceDocumentNumber = txtReferenceDocNumber.Text.Trim();
                    receiptItem.ReferenceDocumentNumber1 = txtReferenceDocNumber1.Text.Trim();
                    receiptItem.DocumentDate = txtDocumentDate.Text.Trim();
                    receiptItem.DocumentValue = txtDocumentValue.Text.Trim();
                    receiptItem.CollectionAmount = txtCollectionAmount.Text.Trim();
                    receiptItem.BalanceAmount = txtBalanceAmount.Text.Trim();
                    receiptItem.PaymentIndicator = ddlPaymentIndicator.SelectedValue;

                    receiptEntity.Items.Add(receiptItem);
                }

                ReceiptTransactions receiptTransactions = new ReceiptTransactions();
                if (hdnScreenMode.Value == "A")
                {
                    DataSet ds = receiptTransactions.AddNewReceiptEntry(ref receiptEntity);

                    if (receiptEntity.ErrorMsg == string.Empty && receiptEntity.ErrorCode == "0")
                    {
                        txtReceiptNumber.Text = receiptEntity.ReceiptNumber;
                        FreezeButtons(false);

                        BtnSubmit.Enabled = false;
                        btnReset.Enabled = true;
                        ChkHeader.Enabled = false;
                        btnReport.Enabled = true;
                        btnReportExcel.Enabled = true;
                        grvItemDetails.Enabled = false;
                        txtFromDate.Enabled = false;
                        txtToDate.Enabled = false;
                        txtTotalBalanceAmount.Text = "0.00";

                        if (ds.Tables[0].Rows[0]["Phone"].ToString().Length >= 10)
                            if (ds.Tables[0].Rows[0]["Phone"].ToString() != "9999999999")
                                sendsms.SendingSMStoCustomers(ddlBranch.SelectedValue, ds.Tables[0].Rows[0]["Phone"].ToString(), ds.Tables[0].Rows[0]["SMS"].ToString(), ds.Tables[0].Rows[0]["Template_Id"].ToString());

                        if (receiptEntity.AdvanceChequeSlipNumber != "" && Convert.ToDecimal(txtAdvanceAmount.Text) > 0)
                        {
                            txtAdvanceChequeSlipNo.Text = receiptEntity.AdvanceChequeSlipNumber;
                            advChqslipDetls1.Visible = true;
                            advChqslipDetls2.Visible = true;
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Receipt Raised Successfully and an Advance ChequeSlip # " + receiptEntity.AdvanceChequeSlipNumber + " is also Raised for Excess Amount.');", true);
                        }
                        else
                        {
                            advChqslipDetls1.Visible = false;
                            advChqslipDetls2.Visible = false;
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Receipt Raised Successfully.');", true);
                        }
                    }
                    else if (receiptEntity.ErrorMsg == "D" && receiptEntity.ErrorCode == "D")
                    {
                        FreezeButtons(false);

                        BtnSubmit.Enabled = false;
                        btnReset.Enabled = true;
                        ChkHeader.Enabled = false;
                        btnReport.Visible = false;
                        grvItemDetails.Enabled = false;
                        btnReportExcel.Visible = false;
                        btnReportExcel.Enabled = false;
                        BtnGetDocuments.Visible = false;
                        txtFromDate.Enabled = false;
                        txtToDate.Enabled = false;
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Duplicate Temp Receipt Number or Cheque Number Exists For this Dealer on this Receipt Date. Please Reset the Page and Change the Number and Re-Submit.');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('" + receiptEntity.ErrorMsg + "');", true);

                        BtnSubmit.Enabled = false;
                        btnReset.Enabled = true;
                        btnReport.Enabled = false;
                        btnReportExcel.Enabled = false;
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}
