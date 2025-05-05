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
using System.Web.Services;
using static IMPALLibrary.ReceivableInvoice;
using System.Globalization;
using IMPALLibrary.Masters.VendorDetails;

namespace IMPALWeb.Finance
{
    public partial class VendorBooking : System.Web.UI.Page
    {
        CashAndBankTransactions vendorBooking = new CashAndBankTransactions();
        private AccountingPeriods Acc = new AccountingPeriods();
        private ReceivableInvoice objReceivableInvoice = new ReceivableInvoice();
        DebitCredit objDebitCredit = new DebitCredit();

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(VendorBooking), exp);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!IsPostBack)
                {
                    ddlDocumentNumber.Visible = false;
                    LoadAccountingPeriod();
                    Session["OSLSstatus"] = null;
                    ddlBranch.SelectedValue = Session["BranchCode"].ToString();
                    ddlBranch.Enabled = false;
                    txtAccountingPeriod.Visible = false;
                    hdnTDStype.Value = "";
                    BtnSubmit.Attributes.Add("OnClick", "return funVendorSubmitValidation(2);");
                    txtTDSAmount.Attributes.Add("onchange", "javascript:validateTDStype()");

                    //ddlTDStype.DataSource = vendorBooking.GetTDSType(Session["BranchCode"].ToString());
                    //ddlTDStype.DataBind();
                    fnPopulateDropDown("VendorBookingTDStypes", ddlTDStype);

                    FirstGridViewRow();

                    if (grvVendorBookingDetails.FooterRow != null)
                    {
                        Button btnAddRow = (Button)grvVendorBookingDetails.FooterRow.FindControl("btnAdd");
                        if (btnAddRow != null)
                            btnAddRow.Attributes.Add("OnClick", "return funVendorSubmitValidation(1);");
                    }

                    DateItem PrevDateStatus = objReceivableInvoice.GetPreviousDateStatus(Session["UserID"].ToString(), "VendorBooking");

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

                    txtTDSAmount.Text = "0.00";

                    Branches oBranch = new Branches();
                    ddlPaymentBranch.DataSource = oBranch.GetChqIssueBranchesVendorBooking(Session["BranchCode"].ToString());
                    ddlPaymentBranch.DataBind();

                    if (ddlPaymentBranch.Items.Count > 1)
                    {
                        ddlPaymentBranch.Items.Insert(0, new ListItem("-- Select --", "0"));
                    }

                    ddlPaymentBranch.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(Source, ex);
            }
        }

        private void LoadVendorDocuments()
        {
            List<VendorBookingEntity> lstMiscBillNumber = new List<VendorBookingEntity>();
            lstMiscBillNumber = vendorBooking.GetVendorDocuments(Session["BranchCode"].ToString());
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

        private void FirstGridViewRow()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("S_No", typeof(int)));
            dt.Columns.Add(new DataColumn("Chart_of_Account_Code", typeof(string)));
            dt.Columns.Add(new DataColumn("Description", typeof(string)));
            dt.Columns.Add(new DataColumn("Remarks", typeof(string)));
            dt.Columns.Add(new DataColumn("Amount", typeof(string)));
            dt.Columns.Add(new DataColumn("SGST_Code", typeof(string)));
            dt.Columns.Add(new DataColumn("SGST_Per", typeof(string)));
            dt.Columns.Add(new DataColumn("SGST_Amt", typeof(string)));
            dt.Columns.Add(new DataColumn("CGST_Code", typeof(string)));
            dt.Columns.Add(new DataColumn("CGST_Per", typeof(string)));
            dt.Columns.Add(new DataColumn("CGST_Amt", typeof(string)));
            dt.Columns.Add(new DataColumn("IGST_Code", typeof(string)));
            dt.Columns.Add(new DataColumn("IGST_Per", typeof(string)));
            dt.Columns.Add(new DataColumn("IGST_Amt", typeof(string)));
            dt.Columns.Add(new DataColumn("UTGST_Code", typeof(string)));
            dt.Columns.Add(new DataColumn("UTGST_Per", typeof(string)));
            dt.Columns.Add(new DataColumn("UTGST_Amt", typeof(string)));

            DataRow dr = null;
            dr = dt.NewRow();
            dr["Chart_of_Account_Code"] = string.Empty;
            dr["Description"] = string.Empty;
            dr["Remarks"] = string.Empty;
            dr["Amount"] = "0";
            dr["SGST_Code"] = string.Empty;
            dr["SGST_Per"] = "0";
            dr["SGST_Amt"] = "0";
            dr["CGST_Code"] = string.Empty;
            dr["CGST_Per"] = "0";
            dr["CGST_Amt"] = "0";
            dr["IGST_Code"] = string.Empty;
            dr["IGST_Per"] = "0";
            dr["IGST_Amt"] = "0";
            dr["UTGST_Code"] = string.Empty;
            dr["UTGST_Per"] = "0";
            dr["UTGST_Amt"] = "0";
            dt.Rows.Add(dr);

            ViewState["CurrentTable"] = dt;
            ViewState["GridRowCount"] = "1";
            grvVendorBookingDetails.DataSource = dt;
            grvVendorBookingDetails.DataBind();

            grvVendorBookingDetails.Rows[0].Cells.Clear();
            ViewState["GridRowCount"] = "0";
            hdnRowCnt.Value = "0";

            if (Session["BranchCode"].ToString() == "CHG")
            {
                grvVendorBookingDetails.Columns[5].Visible = false;
                grvVendorBookingDetails.Columns[6].Visible = false;
                grvVendorBookingDetails.Columns[7].Visible = false;
                grvVendorBookingDetails.Columns[8].Visible = true;
                grvVendorBookingDetails.Columns[9].Visible = true;
                grvVendorBookingDetails.Columns[10].Visible = true;
            }
            else
            {
                grvVendorBookingDetails.Columns[5].Visible = true;
                grvVendorBookingDetails.Columns[6].Visible = true;
                grvVendorBookingDetails.Columns[7].Visible = true;
                grvVendorBookingDetails.Columns[8].Visible = false;
                grvVendorBookingDetails.Columns[9].Visible = false;
                grvVendorBookingDetails.Columns[10].Visible = false;
            }
        }

        protected void imgEditToggle_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                LoadVendorDocuments();

                ddlDocumentNumber.Visible = true;
                txtDocumentNumber.Visible = false;
                grvVendorBookingDetails.Enabled = false;
                imgEditToggle.Enabled = false;
                ddlAccountingPeriod.Enabled = false;
                ddlVendorCode.SelectedIndex = 0;

                txtVendorCode.Text = "";
                txtVendorName.Text = "";                
                txtAddress1.Text = "";
                txtAddress2.Text = "";
                txtAddress3.Text = "";
                txtAddress4.Text = "";
                txtVendorLocation.Text = "";
                txtGSTINNumber.Text = "";
                txtNarration.Text = "";
                txtReferenceDocumentNumber.Text = "";
                txtReferenceDocumentDate.Text = "";
                txtInvoiceAmount.Text = "";
                txtGSTAmount.Text = "";
                txtTDSAmount.Text = "";
                ddlTDStype.SelectedIndex = 0;
                ddlRCMStatus.SelectedIndex = 0;
				ddlPaymentBranch.SelectedIndex = 0;
                txtPaymentDueDate.Text = "";

                FirstGridViewRow();

                ddlVendorCode.Enabled = false;
                txtVendorName.Enabled = false;
                txtVendorLocation.Enabled = false;
                txtReferenceDocumentNumber.Enabled = false;
                txtReferenceDocumentDate.Enabled = false;
                txtNarration.Enabled = false;
                imgEditToggle.Visible = false;
                ddlVendorCode.Enabled = false;
                txtInvoiceAmount.Enabled = false;
                txtGSTAmount.Enabled = false;
                txtTDSAmount.Enabled = false;
                ddlTDStype.Enabled = false;
                ddlRCMStatus.Enabled = false;
				ddlPaymentBranch.Enabled = false;
                txtPaymentDueDate.Enabled = false;
                imgEditToggle.Visible = false;
                BtnSubmit.Enabled = false;
                BtnSubmit.Visible = false;

                if (grvVendorBookingDetails.FooterRow != null)
                {
                    Button btnAddRow = (Button)grvVendorBookingDetails.FooterRow.FindControl("btnAdd");
                    if (btnAddRow != null)
                        btnAddRow.Visible = false;
                }
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
                    txtAddress1.Text = "";
                    txtAddress2.Text = "";
                    txtAddress3.Text = "";
                    txtAddress4.Text = "";
                    txtVendorLocation.Text = "";
                    txtReferenceDocumentNumber.Text = "";
                    txtReferenceDocumentDate.Text = "";
                    txtNarration.Text = "";
                    txtGSTINNumber.Text = "";
                    txtInvoiceAmount.Text = "";
                    txtGSTAmount.Text = "";
                    txtTDSAmount.Text = "";
                    ddlTDStype.SelectedIndex = 0;
                    ddlPaymentBranch.SelectedIndex = 0;
                    txtPaymentDueDate.Text = "";

                    grvVendorBookingDetails.DataSource = null;
                    grvVendorBookingDetails.DataBind();

                    if (grvVendorBookingDetails.FooterRow != null)
                    {
                        Button btnAddRow = (Button)grvVendorBookingDetails.FooterRow.FindControl("btnAdd");
                        if (btnAddRow != null)
                            btnAddRow.Visible = false;
                    }
                }
                else
                {
                    VendorBookingEntity lstDocumentNumber = new VendorBookingEntity();
                    lstDocumentNumber = vendorBooking.GetVendorBookingHeaderandDetails(Session["BranchCode"].ToString(), ddlDocumentNumber.SelectedValue.ToString());

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
                    txtAddress1.Text = lstDocumentNumber.Address1;
                    txtAddress2.Text = lstDocumentNumber.Address2;
                    txtAddress3.Text = lstDocumentNumber.Address3;
                    txtAddress4.Text = lstDocumentNumber.Address4;
                    txtVendorLocation.Text = lstDocumentNumber.Location;
                    txtReferenceDocumentNumber.Text = lstDocumentNumber.InvoiceNumber;
                    txtReferenceDocumentDate.Text = lstDocumentNumber.InvoiceDate;
                    txtInvoiceAmount.Text = lstDocumentNumber.InvoiceValue;
                    txtGSTAmount.Text = lstDocumentNumber.GSTValue;
                    txtTDSAmount.Text = lstDocumentNumber.TDSvalue;
                    txtNarration.Text = lstDocumentNumber.Narration;
                    txtGSTINNumber.Text = lstDocumentNumber.GSTINNumber;
                    ddlTDStype.Items.Insert(0, new ListItem(lstDocumentNumber.TDStype, lstDocumentNumber.TDStype));
                    ddlTDStype.SelectedIndex = 0;
                    ddlRCMStatus.SelectedValue = lstDocumentNumber.RCMstatus;
                    ddlPaymentBranch.SelectedValue = lstDocumentNumber.PaymentBranch;
                    txtPaymentDueDate.Text = lstDocumentNumber.PaymentDueDate;

                    grvVendorBookingDetails.DataSource = (object)lstDocumentNumber.Items;
                    grvVendorBookingDetails.DataBind();

                    if (grvVendorBookingDetails.FooterRow != null)
                    {
                        Button btnAddRow = (Button)grvVendorBookingDetails.FooterRow.FindControl("btnAdd");
                        if (btnAddRow != null)
                            btnAddRow.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(typeof(VendorBooking), ex);
            }
        }

        protected void ddlVendorCode_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlVendorCode.SelectedValue == "0")
                    return;

                Customers customers = new Customers();
                Customer customer = customers.GetVendorInfoByVendorCode(Session["BranchCode"].ToString(), ddlVendorCode.SelectedValue);

                divVendorInfo.Attributes.Add("style", "display:inline");

                txtVendorCode.Text = customer.Customer_Code;
                txtVendorName.Text = customer.Customer_Name;
                txtAddress1.Text = customer.address1;
                txtAddress2.Text = customer.address2;
                txtAddress3.Text = customer.address3;
                txtAddress4.Text = customer.address4;
                txtVendorLocation.Text = customer.Location;
                txtGSTINNumber.Text = customer.GSTIN;
                txtPhone.Text = customer.Phone;
                Session["OSLSstatus"] = customer.CustOSLSStatus;

                txtReferenceDocumentNumber.Text = "";
                txtReferenceDocumentDate.Text = "";
                txtInvoiceAmount.Text = "";
                txtGSTAmount.Text = "0.00";
                txtTDSAmount.Text = "0.00";
                ddlTDStype.SelectedIndex = 0;
                txtNarration.Text = "";
                ddlRCMStatus.SelectedIndex = 0;
				ddlPaymentBranch.SelectedIndex = 0;
                txtPaymentDueDate.Text = "";

                FirstGridViewRow();
            }
            catch (Exception exp)
            {

                Log.WriteException(typeof(VendorBooking), exp);
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            BtnSubmit.Enabled = false;
            Session.Remove("OSLSstatus");
            Session["OSLSstatus"] = null;
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                decimal differenceTaxAmount = Convert.ToDecimal(txtGSTAmount.Text) - Convert.ToDecimal(hdnTotalTaxAmount.Value);

                if ((Convert.ToDecimal(differenceTaxAmount) < 0))
                {
                    differenceTaxAmount = Convert.ToDecimal(differenceTaxAmount) * (-1);
                }

                if (Convert.ToDecimal(differenceTaxAmount) >= 1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Invoice GST Value and Details GST Amount Rounding Off does not tally');", true);
                    return;
                }

                decimal differenceAmount = Convert.ToDecimal(txtInvoiceAmount.Text) - (Convert.ToDecimal(hdnTotalAmount.Value) + Convert.ToDecimal(hdnTotalTaxAmount.Value));

                if ((Convert.ToDecimal(differenceAmount) < 0))
                {
                    differenceAmount = Convert.ToDecimal(differenceAmount) * (-1);
                }

                if (Convert.ToDecimal(differenceAmount) >= 1)
                {
					ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Invoice Value and Sum of Details Amount and GST Amount Rounding Off does not tally');", true);
					return;
                }

                VendorBookingEntity vendorbookingHeader = new VendorBookingEntity();
                IMPALLibrary.Payable payableEntity = new IMPALLibrary.Payable();
                vendorbookingHeader.Items = new List<VendorBookingDetail>();
                VendorBookingDetail vendorbookingDetail = null;
                vendorbookingHeader.DocumentNumber = txtDocumentNumber.Text;
                vendorbookingHeader.DocumentDate = txtDocumentDate.Text;
                vendorbookingHeader.TransactionTypeCode = "841";
                vendorbookingHeader.BranchCode = ddlBranch.SelectedValue;
                vendorbookingHeader.VendorCode = ddlVendorCode.SelectedValue;
                vendorbookingHeader.VendorName = txtVendorName.Text;
                vendorbookingHeader.Location = txtVendorLocation.Text;
                vendorbookingHeader.InvoiceNumber = txtReferenceDocumentNumber.Text;
                vendorbookingHeader.InvoiceDate = txtReferenceDocumentDate.Text;
                vendorbookingHeader.InvoiceValue = txtInvoiceAmount.Text;
                vendorbookingHeader.GSTValue = txtGSTAmount.Text;
                vendorbookingHeader.RoundingOffValue = "0";
                vendorbookingHeader.TDSvalue = txtTDSAmount.Text;
                vendorbookingHeader.GSTINNumber = txtGSTINNumber.Text;
                vendorbookingHeader.Narration = txtNarration.Text;
                vendorbookingHeader.RCMstatus = ddlRCMStatus.SelectedValue;
                vendorbookingHeader.TDStype = hdnTDStype.Value;
				vendorbookingHeader.PaymentBranch = ddlPaymentBranch.SelectedValue;
                vendorbookingHeader.PaymentDueDate = txtPaymentDueDate.Text;

                foreach (GridViewRow gr in grvVendorBookingDetails.Rows)
                {
                    vendorbookingDetail = new VendorBookingDetail();
                    var strSNo = (TextBox)gr.Cells[0].FindControl("txtSNo");
                    var strChartOfAccount = (TextBox)gr.Cells[1].FindControl("txtChartOfAccount");
                    var strDescription = (TextBox)gr.Cells[2].FindControl("txtDescription");
                    var strRemarks = (TextBox)gr.Cells[3].FindControl("txtRemarks");
                    var strAmount = (TextBox)gr.Cells[4].FindControl("txtAmount");

                    vendorbookingDetail.SerialNumber = strSNo.Text;
                    vendorbookingDetail.Chart_of_Account_Code = strChartOfAccount.Text;
                    vendorbookingDetail.Description = strDescription.Text;
                    vendorbookingDetail.Remarks = strRemarks.Text;
                    vendorbookingDetail.Amount = strAmount.Text;

                    if (Convert.ToDecimal(txtGSTAmount.Text) <= 0)
                    {
                        if (Session["BranchCode"].ToString() == "CHG")
                        {
                            vendorbookingDetail.SGST_Code = "0";
                            vendorbookingDetail.SGST_Per = "0";
                            vendorbookingDetail.SGST_Amt = "0";
                        }
                        else
                        {
                            vendorbookingDetail.SGST_Code = "0";
                            vendorbookingDetail.SGST_Per = "0";
                            vendorbookingDetail.SGST_Amt = "0";
                        }

                        vendorbookingDetail.CGST_Code = "0";
                        vendorbookingDetail.CGST_Per = "0";
                        vendorbookingDetail.CGST_Amt = "0";
                        vendorbookingDetail.IGST_Code = "0";
                        vendorbookingDetail.IGST_Per = "0";
                        vendorbookingDetail.IGST_Amt = "0";
                    }
                    else
                    {
                        var ddlSGSTCode = (DropDownList)gr.Cells[5].FindControl("ddlSGSTCode");
                        var txtSGSTPer = (TextBox)gr.Cells[6].FindControl("txtSGSTPer");
                        var txtSGSTAmt = (TextBox)gr.Cells[7].FindControl("txtSGSTAmt");
                        var ddlUTGSTCode = (DropDownList)gr.Cells[8].FindControl("ddlUTGSTCode");
                        var txtUTGSTPer = (TextBox)gr.Cells[9].FindControl("txtUTGSTPer");
                        var txtUTGSTAmt = (TextBox)gr.Cells[10].FindControl("txtUTGSTAmt");
                        var ddlCGSTCode = (DropDownList)gr.Cells[11].FindControl("ddlCGSTCode");
                        var txtCGSTPer = (TextBox)gr.Cells[12].FindControl("txtCGSTPer");
                        var txtCGSTAmt = (TextBox)gr.Cells[13].FindControl("txtCGSTAmt");
                        var ddlIGSTCode = (DropDownList)gr.Cells[14].FindControl("ddlIGSTCode");
                        var txtIGSTPer = (TextBox)gr.Cells[15].FindControl("txtIGSTPer");
                        var txtIGSTAmt = (TextBox)gr.Cells[16].FindControl("txtIGSTAmt");                        

                        if (Session["BranchCode"].ToString() == "CHG")
                        {
                            vendorbookingDetail.SGST_Code = ddlUTGSTCode.SelectedValue != "0" ? ddlUTGSTCode.SelectedItem.Text.Substring(0, 4) : "";
                            vendorbookingDetail.SGST_Per = txtUTGSTPer.Text;
                            vendorbookingDetail.SGST_Amt = txtUTGSTAmt.Text;
                        }
                        else
                        {
                            vendorbookingDetail.SGST_Code = ddlSGSTCode.SelectedValue != "0" ? ddlSGSTCode.SelectedItem.Text.Substring(0, 4) : "";
                            vendorbookingDetail.SGST_Per = txtSGSTPer.Text;
                            vendorbookingDetail.SGST_Amt = txtSGSTAmt.Text;
                        }

                        vendorbookingDetail.CGST_Code = ddlCGSTCode.SelectedValue != "0" ? ddlCGSTCode.SelectedItem.Text.Substring(0, 4) : "";
                        vendorbookingDetail.CGST_Per = txtCGSTPer.Text;
                        vendorbookingDetail.CGST_Amt = txtCGSTAmt.Text;
                        vendorbookingDetail.IGST_Code = ddlIGSTCode.SelectedValue != "0" ? ddlIGSTCode.SelectedItem.Text.Substring(0, 4) : "";
                        vendorbookingDetail.IGST_Per = txtIGSTPer.Text;
                        vendorbookingDetail.IGST_Amt = txtIGSTAmt.Text;
                    }

                    vendorbookingHeader.Items.Add(vendorbookingDetail);
                }

                int result = vendorBooking.AddNewVendorBookingEntry(ref vendorbookingHeader);
                if (result == 1 && vendorbookingHeader.ErrorMsg == string.Empty && vendorbookingHeader.ErrorCode == "0")
                {
                    txtDocumentNumber.Text = vendorbookingHeader.DocumentNumber;
                    imgEditToggle.Enabled = false;
                    ddlAccountingPeriod.Enabled = false;
                    ddlVendorCode.Enabled = false;
                    txtReferenceDocumentNumber.Enabled = false;
                    txtReferenceDocumentDate.Enabled = false;
                    txtInvoiceAmount.Enabled = false;
                    txtGSTAmount.Enabled = false;
                    txtTDSAmount.Enabled = false;
                    ddlRCMStatus.Enabled = false;
                    txtNarration.Enabled = false;
					ddlPaymentBranch.Enabled = false;
                    txtPaymentDueDate.Enabled = false;
                    grvVendorBookingDetails.Enabled = false;
                    BtnSubmit.Enabled = false;
                    ddlTDStype.Enabled = false;

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Vendor Booking Has Been Saved Successfully. Please Get the Manager Approval for the Same.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('" + vendorbookingHeader.ErrorMsg + "');", true);
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
                Response.Redirect("VendorBooking.aspx", false);
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
                GridViewRow gvr = (GridViewRow)((ChartAccount)sender).Parent.Parent;
                TextBox txtgrdChartOfAccount = (TextBox)gvr.FindControl("txtChartOfAccount");
                TextBox txtDescription = (TextBox)gvr.FindControl("txtDescription");
                TextBox txtRemarks = (TextBox)gvr.FindControl("txtRemarks");

                if (Session["ChatAccCode"].ToString().Substring(0, 6) == "409096" || Session["ChatAccCode"].ToString().Substring(0, 6) == "409227")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('VAT or GST Account Cannot be Entered.');", true);
                    txtgrdChartOfAccount.Text = "";
                    return;
                }

                txtgrdChartOfAccount.Text = Session["ChatAccCode"].ToString();
                txtDescription.Text = vendorBooking.GetDescription(Session["ChatAccCode"].ToString(), Session["BranchCode"].ToString());
                txtRemarks.Focus();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void txtAmount_TextChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (Session["OSLSstatus"] != null)
                    hdnOSLSstatus.Value = Session["OSLSstatus"].ToString();

                GridViewRow gvr = (GridViewRow)((TextBox)sender).Parent.Parent;
                var txtAmount = (TextBox)gvr.FindControl("txtAmount");

                if (txtAmount.Text != "")
                {
                    var ddlSGSTCode = (DropDownList)gvr.FindControl("ddlSGSTCode");
                    var txtSGSTPer = (TextBox)gvr.FindControl("txtSGSTPer");
                    var txtSGSTAmt = (TextBox)gvr.FindControl("txtSGSTAmt");
                    var ddlCGSTCode = (DropDownList)gvr.FindControl("ddlCGSTCode");
                    var txtCGSTPer = (TextBox)gvr.FindControl("txtCGSTPer");
                    var txtCGSTAmt = (TextBox)gvr.FindControl("txtCGSTAmt");
                    var ddlIGSTCode = (DropDownList)gvr.FindControl("ddlIGSTCode");
                    var txtIGSTPer = (TextBox)gvr.FindControl("txtIGSTPer");
                    var txtIGSTAmt = (TextBox)gvr.FindControl("txtIGSTAmt");
                    var ddlUTGSTCode = (DropDownList)gvr.FindControl("ddlUTGSTCode");
                    var txtUTGSTPer = (TextBox)gvr.FindControl("txtUTGSTPer");
                    var txtUTGSTAmt = (TextBox)gvr.FindControl("txtUTGSTAmt");

                    if (ddlSGSTCode.SelectedIndex > 0)
                    {
                        txtSGSTAmt.Text = TwoDecimalConversion(((Convert.ToDecimal(txtAmount.Text) * Convert.ToDecimal(txtSGSTPer.Text)) / 100).ToString());
                    }
                    else
                    {
                        txtSGSTAmt.Text = "0";
                    }

                    if (ddlCGSTCode.SelectedIndex > 0)
                    {
                        txtCGSTAmt.Text = TwoDecimalConversion(((Convert.ToDecimal(txtAmount.Text) * Convert.ToDecimal(txtCGSTPer.Text)) / 100).ToString());
                    }
                    else
                    {
                        txtCGSTAmt.Text = "0";
                    }

                    if (ddlIGSTCode.SelectedIndex > 0)
                    {
                        txtIGSTAmt.Text = TwoDecimalConversion(((Convert.ToDecimal(txtAmount.Text) * Convert.ToDecimal(txtIGSTPer.Text)) / 100).ToString());
                    }
                    else
                    {
                        txtIGSTAmt.Text = "0";
                    }

                    if (ddlUTGSTCode.SelectedIndex > 0)
                    {
                        txtUTGSTAmt.Text = TwoDecimalConversion(((Convert.ToDecimal(txtAmount.Text) * Convert.ToDecimal(txtUTGSTPer.Text)) / 100).ToString());
                    }
                    else
                    {
                        txtUTGSTAmt.Text = "0";
                    }

                    if (txtAmount.Text.Trim() == "")
                        txtAmount.Text = "0";

                    if (!(hdnOSLSstatus.Value == "L" || hdnOSLSstatus.Value == "O"))
                    {
                        ddlUTGSTCode.Enabled = false;
                        ddlCGSTCode.Enabled = false;
                        ddlSGSTCode.Enabled = false;
                        ddlIGSTCode.Enabled = false;
                    }
                    else
                    {
                        if (Convert.ToDecimal(txtAmount.Text) > 0 && Convert.ToDecimal(txtGSTAmount.Text) > 0)
                        {
                            if (Session["BranchCode"].ToString() == "CHG")
                            {
                                if (hdnOSLSstatus.Value == "L")
                                    ddlUTGSTCode.Enabled = true;
                                else
                                    ddlUTGSTCode.Enabled = false;

                                txtUTGSTPer.Enabled = false;
                                txtUTGSTAmt.Enabled = false;
                            }
                            else
                            {
                                if (hdnOSLSstatus.Value == "L")
                                    ddlSGSTCode.Enabled = true;
                                else
                                    ddlSGSTCode.Enabled = false;

                                txtSGSTPer.Enabled = false;
                                txtSGSTAmt.Enabled = false;
                            }

                            if (hdnOSLSstatus.Value == "L")
                                ddlCGSTCode.Enabled = true;
                            else
                                ddlCGSTCode.Enabled = false;

                            txtCGSTPer.Enabled = false;
                            txtCGSTAmt.Enabled = false;

                            if (hdnOSLSstatus.Value == "O")
                                ddlIGSTCode.Enabled = true;
                            else
                                ddlIGSTCode.Enabled = false;

                            txtIGSTPer.Enabled = false;
                            txtIGSTAmt.Enabled = false;
                        }
                        else
                        {
                            ddlSGSTCode.Enabled = false;
                            txtSGSTPer.Enabled = false;
                            txtSGSTAmt.Enabled = false;
                            ddlCGSTCode.Enabled = false;
                            txtCGSTPer.Enabled = false;
                            txtCGSTAmt.Enabled = false;
                            ddlIGSTCode.Enabled = false;
                            txtIGSTPer.Enabled = false;
                            txtIGSTAmt.Enabled = false;
                            ddlUTGSTCode.Enabled = false;
                            txtUTGSTPer.Enabled = false;
                            txtUTGSTAmt.Enabled = false;
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ddlSGSTCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                GridViewRow gvr = (GridViewRow)((DropDownList)sender).Parent.Parent;
                var txtAmount = (TextBox)gvr.FindControl("txtAmount");
                var ddlSGSTCode = (DropDownList)gvr.FindControl("ddlSGSTCode");
                var txtSGSTPer = (TextBox)gvr.FindControl("txtSGSTPer");
                var txtSGSTAmt = (TextBox)gvr.FindControl("txtSGSTAmt");
                var ddlCGSTCode = (DropDownList)gvr.FindControl("ddlCGSTCode");
                var txtCGSTPer = (TextBox)gvr.FindControl("txtCGSTPer");
                var txtCGSTAmt = (TextBox)gvr.FindControl("txtCGSTAmt");

                if (ddlSGSTCode.SelectedIndex == 0)
                {
                    txtSGSTPer.Text = "0";
                    txtSGSTAmt.Text = "0";
                    ddlCGSTCode.SelectedIndex = 0;
                    txtCGSTPer.Text = "0";
                    txtCGSTAmt.Text = "0";
                }
                else
                {
                    txtSGSTPer.Text = TwoDecimalConversion(vendorBooking.GetSalesTaxPer(ddlSGSTCode.SelectedItem.Text.Substring(0, 4)));
                    txtSGSTAmt.Text = TwoDecimalConversion(((Convert.ToDecimal(txtAmount.Text) * Convert.ToDecimal(txtSGSTPer.Text)) / 100).ToString());

                    ddlCGSTCode.SelectedIndex = ddlSGSTCode.SelectedIndex;
                    ddlCGSTCode.Enabled = false;

                    txtCGSTPer.Text = TwoDecimalConversion(vendorBooking.GetSalesTaxPer(ddlCGSTCode.SelectedItem.Text.Substring(0, 4)));
                    txtCGSTAmt.Text = TwoDecimalConversion(((Convert.ToDecimal(txtAmount.Text) * Convert.ToDecimal(txtCGSTPer.Text)) / 100).ToString());
                }

                txtSGSTPer.Enabled = false;
                txtSGSTAmt.Enabled = false;

                var ddlIGSTCode = (DropDownList)gvr.FindControl("ddlIGSTCode");
                var txtIGSTPer = (TextBox)gvr.FindControl("txtIGSTPer");
                var txtIGSTAmt = (TextBox)gvr.FindControl("txtIGSTAmt");
                ddlIGSTCode.SelectedIndex = 0;
                ddlIGSTCode.Enabled = false;
                txtIGSTPer.Enabled = false;
                txtIGSTAmt.Enabled = false;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ddlCGSTCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                GridViewRow gvr = (GridViewRow)((DropDownList)sender).Parent.Parent;
                var txtAmount = (TextBox)gvr.FindControl("txtAmount");
                var ddlSGSTCode = (DropDownList)gvr.FindControl("ddlSGSTCode");
                var txtSGSTPer = (TextBox)gvr.FindControl("txtSGSTPer");
                var txtSGSTAmt = (TextBox)gvr.FindControl("txtSGSTAmt");
                var ddlCGSTCode = (DropDownList)gvr.FindControl("ddlCGSTCode");
                var txtCGSTPer = (TextBox)gvr.FindControl("txtCGSTPer");
                var txtCGSTAmt = (TextBox)gvr.FindControl("txtCGSTAmt");
                var ddlUTGSTCode = (DropDownList)gvr.FindControl("ddlUTGSTCode");
                var txtUTGSTPer = (TextBox)gvr.FindControl("txtUTGSTPer");
                var txtUTGSTAmt = (TextBox)gvr.FindControl("txtUTGSTAmt");

                if (ddlCGSTCode.SelectedIndex == 0)
                {
                    txtCGSTPer.Text = "0";
                    txtCGSTAmt.Text = "0";
                    ddlSGSTCode.SelectedIndex = 0;
                    txtSGSTPer.Text = "0";
                    txtSGSTAmt.Text = "0";
                }
                else
                {
                    txtCGSTPer.Text = TwoDecimalConversion(vendorBooking.GetSalesTaxPer(ddlCGSTCode.SelectedItem.Text.Substring(0, 4)));
                    txtCGSTAmt.Text = TwoDecimalConversion(((Convert.ToDecimal(txtAmount.Text) * Convert.ToDecimal(txtCGSTPer.Text)) / 100).ToString());

                    if (Session["BranchCode"].ToString() == "CHG")
                    {
                        ddlUTGSTCode.SelectedIndex = ddlCGSTCode.SelectedIndex;
                        ddlUTGSTCode.Enabled = false;
                        txtUTGSTPer.Text = TwoDecimalConversion(vendorBooking.GetSalesTaxPer(ddlSGSTCode.SelectedItem.Text.Substring(0, 4)));
                        txtUTGSTAmt.Text = TwoDecimalConversion(((Convert.ToDecimal(txtAmount.Text) * Convert.ToDecimal(txtSGSTPer.Text)) / 100).ToString());
                    }
                    else
                    {
                        ddlSGSTCode.SelectedIndex = ddlCGSTCode.SelectedIndex;
                        ddlSGSTCode.Enabled = false;
                        txtSGSTPer.Text = TwoDecimalConversion(vendorBooking.GetSalesTaxPer(ddlSGSTCode.SelectedItem.Text.Substring(0, 4)));
                        txtSGSTAmt.Text = TwoDecimalConversion(((Convert.ToDecimal(txtAmount.Text) * Convert.ToDecimal(txtSGSTPer.Text)) / 100).ToString());
                    }                    
                }

                txtCGSTPer.Enabled = false;
                txtCGSTAmt.Enabled = false;

                var ddlIGSTCode = (DropDownList)gvr.FindControl("ddlIGSTCode");
                var txtIGSTPer = (TextBox)gvr.FindControl("txtIGSTPer");
                var txtIGSTAmt = (TextBox)gvr.FindControl("txtIGSTAmt");
                ddlIGSTCode.SelectedIndex = 0;
                ddlIGSTCode.Enabled = false;
                txtIGSTPer.Enabled = false;
                txtIGSTAmt.Enabled = false;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ddlIGSTCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                GridViewRow gvr = (GridViewRow)((DropDownList)sender).Parent.Parent;
                var txtAmount = (TextBox)gvr.FindControl("txtAmount");
                var ddlIGSTCode = (DropDownList)gvr.FindControl("ddlIGSTCode");
                var txtIGSTPer = (TextBox)gvr.FindControl("txtIGSTPer");
                var txtIGSTAmt = (TextBox)gvr.FindControl("txtIGSTAmt");

                if (ddlIGSTCode.SelectedIndex == 0)
                {
                    txtIGSTPer.Text = "0";
                    txtIGSTAmt.Text = "0";
                }
                else
                {
                    txtIGSTPer.Text = TwoDecimalConversion(vendorBooking.GetSalesTaxPer(ddlIGSTCode.SelectedItem.Text.Substring(0, 4)));
                    txtIGSTAmt.Text = TwoDecimalConversion(((Convert.ToDecimal(txtAmount.Text) * Convert.ToDecimal(txtIGSTPer.Text)) / 100).ToString());
                }

                txtIGSTPer.Enabled = false;
                txtIGSTAmt.Enabled = false;

                var ddlSGSTCode = (DropDownList)gvr.FindControl("ddlSGSTCode");
                var txtSGSTPer = (TextBox)gvr.FindControl("txtSGSTPer");
                var txtSGSTAmt = (TextBox)gvr.FindControl("txtSGSTAmt");
                var ddlCGSTCode = (DropDownList)gvr.FindControl("ddlCGSTCode");
                var txtCGSTPer = (TextBox)gvr.FindControl("txtCGSTPer");
                var txtCGSTAmt = (TextBox)gvr.FindControl("txtCGSTAmt");
                var ddlUTGSTCode = (DropDownList)gvr.FindControl("ddlUTGSTCode");
                var txtUTGSTPer = (TextBox)gvr.FindControl("txtUTGSTPer");
                var txtUTGSTAmt = (TextBox)gvr.FindControl("txtUTGSTAmt");
                ddlSGSTCode.SelectedIndex = 0;
                ddlSGSTCode.Enabled = false;
                txtSGSTPer.Enabled = false;
                txtSGSTAmt.Enabled = false;
                ddlCGSTCode.SelectedIndex = 0;
                ddlCGSTCode.Enabled = false;
                txtCGSTPer.Enabled = false;
                txtCGSTAmt.Enabled = false;
                ddlUTGSTCode.SelectedIndex = 0;
                ddlUTGSTCode.Enabled = false;
                txtUTGSTPer.Enabled = false;
                txtUTGSTAmt.Enabled = false;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ddlUTGSTCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                GridViewRow gvr = (GridViewRow)((DropDownList)sender).Parent.Parent;
                var txtAmount = (TextBox)gvr.FindControl("txtAmount");
                var ddlUTGSTCode = (DropDownList)gvr.FindControl("ddlUTGSTCode");
                var txtUTGSTPer = (TextBox)gvr.FindControl("txtUTGSTPer");
                var txtUTGSTAmt = (TextBox)gvr.FindControl("txtUTGSTAmt");
                var ddlCGSTCode = (DropDownList)gvr.FindControl("ddlCGSTCode");
                var txtCGSTPer = (TextBox)gvr.FindControl("txtCGSTPer");
                var txtCGSTAmt = (TextBox)gvr.FindControl("txtCGSTAmt");

                if (ddlUTGSTCode.SelectedIndex == 0)
                {
                    txtUTGSTPer.Text = "0";
                    txtUTGSTAmt.Text = "0";
                    ddlCGSTCode.SelectedIndex = 0;
                    txtCGSTPer.Text = "0";
                    txtCGSTAmt.Text = "0";
                }
                else
                {
                    txtUTGSTPer.Text = TwoDecimalConversion(vendorBooking.GetSalesTaxPer(ddlUTGSTCode.SelectedItem.Text.Substring(0, 4)));
                    txtUTGSTAmt.Text = TwoDecimalConversion(((Convert.ToDecimal(txtAmount.Text) * Convert.ToDecimal(txtUTGSTPer.Text)) / 100).ToString());

                    ddlCGSTCode.SelectedIndex = ddlUTGSTCode.SelectedIndex;
                    ddlCGSTCode.Enabled = false;

                    txtCGSTPer.Text = TwoDecimalConversion(vendorBooking.GetSalesTaxPer(ddlCGSTCode.SelectedItem.Text.Substring(0, 4)));
                    txtCGSTAmt.Text = TwoDecimalConversion(((Convert.ToDecimal(txtAmount.Text) * Convert.ToDecimal(txtCGSTPer.Text)) / 100).ToString());
                }

                txtUTGSTPer.Enabled = false;
                txtUTGSTAmt.Enabled = false;

                var ddlIGSTCode = (DropDownList)gvr.FindControl("ddlIGSTCode");
                var txtIGSTPer = (TextBox)gvr.FindControl("txtIGSTPer");
                var txtIGSTAmt = (TextBox)gvr.FindControl("txtIGSTAmt");
                ddlIGSTCode.Enabled = false;
                txtIGSTPer.Enabled = false;
                txtIGSTAmt.Enabled = false;
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

        private void LoadAccountingPeriod()
        {
            List<string> lstAccountingPeriod = new List<string>();

            int PrevFinYearStatus = objReceivableInvoice.GetPreviousAccountingPeriodStatus(Session["UserID"].ToString(), "VendorBooking");

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
            ddlAccountingPeriod.Enabled = false;

            if (ddlAccountingPeriod.SelectedIndex == 0)
                txtDocumentDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            else
                txtDocumentDate.Text = objDebitCredit.GetDocumentDate(ddlAccountingPeriod.SelectedValue);

            FirstGridViewRow();
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

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                AddNewRow();
                ddlVendorCode.Enabled = false;
                txtVendorName.Enabled = false;
                txtVendorLocation.Enabled = false;
                txtReferenceDocumentNumber.Enabled = false;
                txtReferenceDocumentDate.Enabled = false;
                txtNarration.Enabled = false;
                imgEditToggle.Visible = false;
                txtInvoiceAmount.Enabled = false;
                txtGSTAmount.Enabled = false;
                txtTDSAmount.Enabled = false;
                ddlTDStype.Enabled = false;
                ddlRCMStatus.Enabled = false;
				ddlPaymentBranch.Enabled = false;
                txtPaymentDueDate.Enabled = false;
            }
            catch (Exception ex)
            {
                Log.WriteException(Source, ex);
            }
        }

        protected void grvVendorBookingDetails_RowCreated(object sender, GridViewRowEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    TextBox txtAmount = (TextBox)e.Row.FindControl("txtAmount");
                    TextBox txtChartOfAccount = (TextBox)e.Row.FindControl("txtChartOfAccount");
                    ChartAccount ChartOfAccount1 = (ChartAccount)e.Row.FindControl("ChartAccount1");
                    GridViewRow grdRow = ((GridViewRow)txtAmount.Parent.Parent);

                    if (ddlDocumentNumber.Visible)
                        ChartOfAccount1.Visible = false;
                    else
                        ChartOfAccount1.Visible = true;

                    var ddlSGSTCode = (DropDownList)e.Row.FindControl("ddlSGSTCode");
                    var txtSGSTPer = (TextBox)e.Row.FindControl("txtSGSTPer");
                    var txtSGSTAmt = (TextBox)e.Row.FindControl("txtSGSTAmt");
                    var ddlCGSTCode = (DropDownList)e.Row.FindControl("ddlCGSTCode");
                    var txtCGSTPer = (TextBox)e.Row.FindControl("txtCGSTPer");
                    var txtCGSTAmt = (TextBox)e.Row.FindControl("txtCGSTAmt");
                    var ddlIGSTCode = (DropDownList)e.Row.FindControl("ddlIGSTCode");
                    var txtIGSTPer = (TextBox)e.Row.FindControl("txtIGSTPer");
                    var txtIGSTAmt = (TextBox)e.Row.FindControl("txtIGSTAmt");
                    var ddlUTGSTCode = (DropDownList)e.Row.FindControl("ddlUTGSTCode");
                    var txtUTGSTPer = (TextBox)e.Row.FindControl("txtUTGSTPer");
                    var txtUTGSTAmt = (TextBox)e.Row.FindControl("txtUTGSTAmt");

                    if (txtGSTAmount.Text == "")
                        txtGSTAmount.Text = "0.00";

                    txtAmount.Enabled = true;
                    ddlSGSTCode.Enabled = false;
                    txtSGSTPer.Enabled = false;
                    txtSGSTAmt.Enabled = false;
                    ddlCGSTCode.Enabled = false;
                    txtCGSTPer.Enabled = false;
                    txtCGSTAmt.Enabled = false;
                    ddlIGSTCode.Enabled = false;
                    txtIGSTPer.Enabled = false;
                    txtIGSTAmt.Enabled = false;
                    ddlUTGSTCode.Enabled = false;
                    txtUTGSTPer.Enabled = false;
                    txtUTGSTAmt.Enabled = false;

                    if (Convert.ToDecimal(txtGSTAmount.Text) > 0)
                    {
                        LoadDropDownLists<SalesTaxCode>(vendorBooking.GetSalesTaxCodeSGST(), ddlSGSTCode, "Sales_Tax_Percentage", "sales_tax_code", true, "");
                        LoadDropDownLists<SalesTaxCode>(vendorBooking.GetSalesTaxCodeCGST(), ddlCGSTCode, "Sales_Tax_Percentage", "sales_tax_code", true, "");
                        LoadDropDownLists<SalesTaxCode>(vendorBooking.GetSalesTaxCodeIGST(), ddlIGSTCode, "Sales_Tax_Percentage", "sales_tax_code", true, "");
                        LoadDropDownLists<SalesTaxCode>(vendorBooking.GetSalesTaxCodeUTGST(), ddlUTGSTCode, "Sales_Tax_Percentage", "sales_tax_code", true, "");
                    }
                }

                if (grvVendorBookingDetails.FooterRow != null)
                {
                    Button btnAddRow = (Button)grvVendorBookingDetails.FooterRow.FindControl("btnAdd");
                    if (btnAddRow != null)
                        btnAddRow.Attributes.Add("OnClick", "return funVendorSubmitValidation(1);");
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void grvVendorBookingDetails_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.RowIndex);
                DataTable dt = ViewState["CurrentTable"] as DataTable;
                FillLastRowInfoInGrid(ref dt);
                dt.Rows[index].Delete();

                if (dt.Rows.Count > 0)
                {
                    int NewSerialNo = 1;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dr[0] = NewSerialNo.ToString();
                        NewSerialNo += 1;
                    }

                    int LastRow = dt.Rows.Count - 1;

                    if (
                        (string.IsNullOrEmpty(dt.Rows[LastRow][1].ToString())) &&
                        (string.IsNullOrEmpty(dt.Rows[LastRow][2].ToString())) &&
                        (string.IsNullOrEmpty(dt.Rows[LastRow][3].ToString())) &&
                        (string.IsNullOrEmpty(dt.Rows[LastRow][4].ToString())))
                    {
                        dt.Rows[LastRow].Delete();
                    }

                    if (dt.Rows.Count > 0)
                    {
                        ViewState["CurrentTable"] = dt;
                        ViewState["GridRowCount"] = dt.Rows.Count;
                        hdnRowCnt.Value = dt.Rows.Count.ToString();

                        grvVendorBookingDetails.DataSource = dt;
                        grvVendorBookingDetails.DataBind();

                        if (grvVendorBookingDetails.FooterRow != null)
                        {
                            Button btnAddRow = (Button)grvVendorBookingDetails.FooterRow.FindControl("btnAdd");
                            if (btnAddRow != null)
                                btnAddRow.Attributes.Add("OnClick", "return funVendorSubmitValidation(1);");
                        }

                        SetPreviousData();
                    }
                    else
                    {
                        FirstGridViewRow();
                    }
                }
                else
                {
                    FirstGridViewRow();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(InwardEntry), exp);
            }
        }

        private void FillLastRowInfoInGrid(ref DataTable dt)
        {
            int rowIndex = grvVendorBookingDetails.Rows.Count - 1;

            TextBox txtSNo = (TextBox)grvVendorBookingDetails.Rows[rowIndex].FindControl("txtSNo");
            TextBox txtChartOfAccount = (TextBox)grvVendorBookingDetails.Rows[rowIndex].FindControl("txtChartOfAccount");
            TextBox txtDescription = (TextBox)grvVendorBookingDetails.Rows[rowIndex].FindControl("txtDescription");
            TextBox txtRemarks = (TextBox)grvVendorBookingDetails.Rows[rowIndex].FindControl("txtRemarks");
            TextBox txtAmount = (TextBox)grvVendorBookingDetails.Rows[rowIndex].FindControl("txtAmount");
            DropDownList ddlSGSTCode = (DropDownList)grvVendorBookingDetails.Rows[rowIndex].FindControl("ddlSGSTCode");
            TextBox txtSGSTPer = (TextBox)grvVendorBookingDetails.Rows[rowIndex].FindControl("txtSGSTPer");
            TextBox txtSGSTAmt = (TextBox)grvVendorBookingDetails.Rows[rowIndex].FindControl("txtSGSTAmt");
            DropDownList ddlUTGSTCode = (DropDownList)grvVendorBookingDetails.Rows[rowIndex].FindControl("ddlUTGSTCode");
            TextBox txtUTGSTPer = (TextBox)grvVendorBookingDetails.Rows[rowIndex].FindControl("txtUTGSTPer");
            TextBox txtUTGSTAmt = (TextBox)grvVendorBookingDetails.Rows[rowIndex].FindControl("txtUTGSTAmt");
            DropDownList ddlCGSTCode = (DropDownList)grvVendorBookingDetails.Rows[rowIndex].FindControl("ddlCGSTCode");
            TextBox txtCGSTPer = (TextBox)grvVendorBookingDetails.Rows[rowIndex].FindControl("txtCGSTPer");
            TextBox txtCGSTAmt = (TextBox)grvVendorBookingDetails.Rows[rowIndex].FindControl("txtCGSTAmt");
            DropDownList ddlIGSTCode = (DropDownList)grvVendorBookingDetails.Rows[rowIndex].FindControl("ddlIGSTCode");
            TextBox txtIGSTPer = (TextBox)grvVendorBookingDetails.Rows[rowIndex].FindControl("txtIGSTPer");
            TextBox txtIGSTAmt = (TextBox)grvVendorBookingDetails.Rows[rowIndex].FindControl("txtIGSTAmt");

            dt.Rows[rowIndex]["Chart_of_Account_Code"] = txtChartOfAccount.Text;
            dt.Rows[rowIndex]["Description"] = txtDescription.Text;
            dt.Rows[rowIndex]["Remarks"] = txtRemarks.Text;
            dt.Rows[rowIndex]["Amount"] = txtAmount.Text;
            dt.Rows[rowIndex]["SGST_Code"] = ddlSGSTCode.SelectedValue;
            dt.Rows[rowIndex]["SGST_Per"] = txtSGSTPer.Text;
            dt.Rows[rowIndex]["SGST_Amt"] = txtSGSTAmt.Text;
            dt.Rows[rowIndex]["CGST_Code"] = ddlCGSTCode.SelectedValue;
            dt.Rows[rowIndex]["CGST_Per"] = txtCGSTPer.Text;
            dt.Rows[rowIndex]["CGST_Amt"] = txtCGSTAmt.Text;
            dt.Rows[rowIndex]["IGST_Code"] = ddlIGSTCode.SelectedValue;
            dt.Rows[rowIndex]["IGST_Per"] = txtIGSTPer.Text;
            dt.Rows[rowIndex]["IGST_Amt"] = txtIGSTAmt.Text;
            dt.Rows[rowIndex]["UTGST_Code"] = ddlUTGSTCode.SelectedValue;
            dt.Rows[rowIndex]["UTGST_Per"] = txtUTGSTPer.Text;
            dt.Rows[rowIndex]["UTGST_Amt"] = txtUTGSTAmt.Text;
        }

        private void Alert(string Message)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Message + "');", true);
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void txtReferenceDocumentNumber_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (vendorBooking.CheckDocumentNumber(ddlVendorCode.SelectedValue, txtReferenceDocumentNumber.Text, ddlAccountingPeriod.SelectedValue, Session["BranchCode"].ToString()) != 0)
                {
                    Alert("Reference Document Number Already Exists");
                    ResetVendorBooking();
                }

                ddlAccountingPeriod.Enabled = false;
                imgEditToggle.Enabled = false;
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesInvoiceEntry), exp);
            }
        }

        private void AddNewRow()
        {
            int rowIndex = 0;
            int iNoofRows = 0;

            if (ViewState["GridRowCount"] != null)
            {
                iNoofRows = Convert.ToInt32(ViewState["GridRowCount"]);
            }

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;

                if (dtCurrentTable.Rows.Count > 0)
                {
                    if (Convert.ToInt64(ViewState["GridRowCount"]) == 0)
                    {
                        dtCurrentTable.Rows.Clear();
                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["S_No"] = 1;
                    }
                    else
                    {
                        for (int i = iNoofRows; i <= dtCurrentTable.Rows.Count; i++)
                        {
                            TextBox txtSNo = (TextBox)grvVendorBookingDetails.Rows[i - 1].Cells[0].FindControl("txtSNo");
                            TextBox txtChartOfAccount = (TextBox)grvVendorBookingDetails.Rows[i - 1].Cells[1].FindControl("txtChartOfAccount");
                            TextBox txtDescription = (TextBox)grvVendorBookingDetails.Rows[i - 1].Cells[2].FindControl("txtDescription");
                            TextBox txtRemarks = (TextBox)grvVendorBookingDetails.Rows[i - 1].Cells[3].FindControl("txtRemarks");
                            TextBox txtAmount = (TextBox)grvVendorBookingDetails.Rows[i - 1].Cells[4].FindControl("txtAmount");
                            DropDownList ddlSGSTCode = (DropDownList)grvVendorBookingDetails.Rows[i - 1].Cells[5].FindControl("ddlSGSTCode");
                            TextBox txtSGSTPer = (TextBox)grvVendorBookingDetails.Rows[i - 1].Cells[6].FindControl("txtSGSTPer");
                            TextBox txtSGSTAmt = (TextBox)grvVendorBookingDetails.Rows[i - 1].Cells[7].FindControl("txtSGSTAmt");
                            DropDownList ddlUTGSTCode = (DropDownList)grvVendorBookingDetails.Rows[i - 1].Cells[8].FindControl("ddlUTGSTCode");
                            TextBox txtUTGSTPer = (TextBox)grvVendorBookingDetails.Rows[i - 1].Cells[9].FindControl("txtUTGSTPer");
                            TextBox txtUTGSTAmt = (TextBox)grvVendorBookingDetails.Rows[i - 1].Cells[10].FindControl("txtUTGSTAmt");
                            DropDownList ddlCGSTCode = (DropDownList)grvVendorBookingDetails.Rows[i - 1].Cells[11].FindControl("ddlCGSTCode");
                            TextBox txtCGSTPer = (TextBox)grvVendorBookingDetails.Rows[i - 1].Cells[12].FindControl("txtCGSTPer");
                            TextBox txtCGSTAmt = (TextBox)grvVendorBookingDetails.Rows[i - 1].Cells[13].FindControl("txtCGSTAmt");
                            DropDownList ddlIGSTCode = (DropDownList)grvVendorBookingDetails.Rows[i - 1].Cells[14].FindControl("ddlIGSTCode");
                            TextBox txtIGSTPer = (TextBox)grvVendorBookingDetails.Rows[i - 1].Cells[15].FindControl("txtIGSTPer");
                            TextBox txtIGSTAmt = (TextBox)grvVendorBookingDetails.Rows[i - 1].Cells[16].FindControl("txtIGSTAmt");

                            dtCurrentTable.Rows[i - 1]["S_No"] = txtSNo.Text;
                            dtCurrentTable.Rows[i - 1]["Chart_of_Account_Code"] = txtChartOfAccount.Text;
                            dtCurrentTable.Rows[i - 1]["Description"] = txtDescription.Text;
                            dtCurrentTable.Rows[i - 1]["Remarks"] = txtRemarks.Text;
                            dtCurrentTable.Rows[i - 1]["Amount"] = txtAmount.Text;
                            dtCurrentTable.Rows[i - 1]["SGST_Code"] = ddlSGSTCode.SelectedValue;
                            dtCurrentTable.Rows[i - 1]["SGST_Per"] = txtSGSTPer.Text;
                            dtCurrentTable.Rows[i - 1]["SGST_Amt"] = txtSGSTAmt.Text;
                            dtCurrentTable.Rows[i - 1]["CGST_Code"] = ddlCGSTCode.SelectedValue;
                            dtCurrentTable.Rows[i - 1]["CGST_Per"] = txtCGSTPer.Text;
                            dtCurrentTable.Rows[i - 1]["CGST_Amt"] = txtCGSTAmt.Text;
                            dtCurrentTable.Rows[i - 1]["IGST_Code"] = ddlIGSTCode.SelectedValue;
                            dtCurrentTable.Rows[i - 1]["IGST_Per"] = txtIGSTPer.Text;
                            dtCurrentTable.Rows[i - 1]["IGST_Amt"] = txtIGSTAmt.Text;
                            dtCurrentTable.Rows[i - 1]["UTGST_Code"] = ddlUTGSTCode.SelectedValue;
                            dtCurrentTable.Rows[i - 1]["UTGST_Per"] = txtUTGSTPer.Text;
                            dtCurrentTable.Rows[i - 1]["UTGST_Amt"] = txtUTGSTAmt.Text;

                            drCurrentRow = dtCurrentTable.NewRow();
                            drCurrentRow["S_No"] = i + 1;
                            rowIndex++;
                        }
                    }

                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    ViewState["GridRowCount"] = dtCurrentTable.Rows.Count.ToString();
                    hdnRowCnt.Value = dtCurrentTable.Rows.Count.ToString();

                    grvVendorBookingDetails.DataSource = dtCurrentTable;
                    grvVendorBookingDetails.DataBind();

                    if (grvVendorBookingDetails.FooterRow != null)
                    {
                        Button btnAddRow = (Button)grvVendorBookingDetails.FooterRow.FindControl("btnAdd");
                        if (btnAddRow != null)
                            btnAddRow.Attributes.Add("OnClick", "return funVendorSubmitValidation(1);");
                    }
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            SetPreviousData();
        }

        private void ResetVendorBooking()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddlAccountingPeriod.SelectedIndex = 0;
                ddlVendorCode.SelectedIndex = 0;
                txtVendorCode.Text = "";
                txtVendorName.Text = "";
                txtAddress1.Text = "";
                txtAddress2.Text = "";
                txtAddress3.Text = "";
                txtAddress4.Text = "";
                txtVendorLocation.Text = "";
                txtGSTINNumber.Text = "";
                txtReferenceDocumentNumber.Text = "";
                txtReferenceDocumentDate.Text = "";
                txtInvoiceAmount.Text = "";
                txtGSTAmount.Text = "";
                txtTDSAmount.Text = "";
                ddlTDStype.SelectedIndex = 0;
                txtNarration.Text = "";

                FirstGridViewRow();                
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void SetPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox txtSNo = (TextBox)grvVendorBookingDetails.Rows[rowIndex].Cells[0].FindControl("txtSNo");
                        TextBox txtChartOfAccount = (TextBox)grvVendorBookingDetails.Rows[rowIndex].Cells[1].FindControl("txtChartOfAccount");
                        ChartAccount imgChartofAccount = (ChartAccount)grvVendorBookingDetails.Rows[rowIndex].Cells[1].FindControl("ChartAccount1");
                        TextBox txtDescription = (TextBox)grvVendorBookingDetails.Rows[rowIndex].Cells[2].FindControl("txtDescription");
                        TextBox txtRemarks = (TextBox)grvVendorBookingDetails.Rows[rowIndex].Cells[3].FindControl("txtRemarks");
                        TextBox txtAmount = (TextBox)grvVendorBookingDetails.Rows[rowIndex].Cells[4].FindControl("txtAmount");
                        DropDownList ddlSGSTCode = (DropDownList)grvVendorBookingDetails.Rows[rowIndex].Cells[5].FindControl("ddlSGSTCode");
                        TextBox txtSGSTPer = (TextBox)grvVendorBookingDetails.Rows[rowIndex].Cells[6].FindControl("txtSGSTPer");
                        TextBox txtSGSTAmt = (TextBox)grvVendorBookingDetails.Rows[rowIndex].Cells[7].FindControl("txtSGSTAmt");
                        DropDownList ddlUTGSTCode = (DropDownList)grvVendorBookingDetails.Rows[rowIndex].Cells[8].FindControl("ddlUTGSTCode");
                        TextBox txtUTGSTPer = (TextBox)grvVendorBookingDetails.Rows[rowIndex].Cells[9].FindControl("txtUTGSTPer");
                        TextBox txtUTGSTAmt = (TextBox)grvVendorBookingDetails.Rows[rowIndex].Cells[10].FindControl("txtUTGSTAmt");
                        DropDownList ddlCGSTCode = (DropDownList)grvVendorBookingDetails.Rows[rowIndex].Cells[11].FindControl("ddlCGSTCode");
                        TextBox txtCGSTPer = (TextBox)grvVendorBookingDetails.Rows[rowIndex].Cells[12].FindControl("txtCGSTPer");
                        TextBox txtCGSTAmt = (TextBox)grvVendorBookingDetails.Rows[rowIndex].Cells[13].FindControl("txtCGSTAmt");
                        DropDownList ddlIGSTCode = (DropDownList)grvVendorBookingDetails.Rows[rowIndex].Cells[14].FindControl("ddlIGSTCode");
                        TextBox txtIGSTPer = (TextBox)grvVendorBookingDetails.Rows[rowIndex].Cells[15].FindControl("txtIGSTPer");
                        TextBox txtIGSTAmt = (TextBox)grvVendorBookingDetails.Rows[rowIndex].Cells[16].FindControl("txtIGSTAmt");

                        txtSNo.Text = dt.Rows[i]["S_No"].ToString();
                        txtChartOfAccount.Text = dt.Rows[i]["Chart_of_Account_Code"].ToString();
                        txtDescription.Text = dt.Rows[i]["Description"].ToString();
                        txtRemarks.Text = dt.Rows[i]["Remarks"].ToString();
                        txtAmount.Text = dt.Rows[i]["Amount"].ToString();
                        ddlSGSTCode.SelectedValue = dt.Rows[i]["SGST_Code"].ToString();
                        txtSGSTPer.Text = dt.Rows[i]["SGST_Per"].ToString();
                        txtSGSTAmt.Text = dt.Rows[i]["SGST_Amt"].ToString();
                        ddlCGSTCode.SelectedValue = dt.Rows[i]["CGST_Code"].ToString();
                        txtCGSTPer.Text = dt.Rows[i]["CGST_Per"].ToString();
                        txtCGSTAmt.Text = dt.Rows[i]["CGST_Amt"].ToString();
                        ddlIGSTCode.SelectedValue = dt.Rows[i]["IGST_Code"].ToString();
                        txtIGSTPer.Text = dt.Rows[i]["IGST_Per"].ToString();
                        txtIGSTAmt.Text = dt.Rows[i]["IGST_Amt"].ToString();
                        ddlUTGSTCode.SelectedValue = dt.Rows[i]["UTGST_Code"].ToString();
                        txtUTGSTPer.Text = dt.Rows[i]["UTGST_Per"].ToString();
                        txtUTGSTAmt.Text = dt.Rows[i]["UTGST_Amt"].ToString();
                        rowIndex++;

                        if (i < dt.Rows.Count - 1)
                        {
                            txtSNo.Enabled = false;
                            txtChartOfAccount.Enabled = false;
                            txtDescription.Enabled = false;
                            txtRemarks.Enabled = false;
                            txtAmount.Enabled = true;
                            imgChartofAccount.Visible = false;
                        }                        
                    }
                }
            }
        }
    }
}
