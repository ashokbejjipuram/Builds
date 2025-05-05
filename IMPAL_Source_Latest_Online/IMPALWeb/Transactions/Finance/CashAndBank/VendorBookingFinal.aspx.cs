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

namespace IMPALWeb.Finance
{
    public partial class VendorBookingFinal : System.Web.UI.Page
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
                Log.WriteException(typeof(VendorBookingFinal), exp);
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
                    BtnCancel.Visible = false;
                    LoadAccountingPeriod();
                    Session["OSLSstatus"] = null;
                    ddlBranch.SelectedValue = Session["BranchCode"].ToString();
                    ddlBranch.Enabled = false;
                    txtAccountingPeriod.Visible = false;
                    hdnTDStype.Value = "";                    

                    //ddlTDStype.DataSource = vendorBooking.GetTDSType(Session["BranchCode"].ToString());
                    //ddlTDStype.DataBind();
                    fnPopulateDropDown("VendorBookingTDStypes", ddlTDStype);
                    LoadVendorDocumentsForApproval();
                    FirstGridViewRow();
                    
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

                    BtnSubmit.Attributes.Add("OnClick", "return funVendorSubmitValidation(2);");
                    BtnReject.Attributes.Add("OnClick", "return funRejectVendorEntry();");
                    BtnCancel.Attributes.Add("OnClick", "return funCancelVendorEntry();");
                    txtTDSAmount.Attributes.Add("onchange", "javascript:validateTDStype()");

                    BtnSubmit.Visible = false;
                    BtnReject.Visible = false;
                    BtnCancel.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(Source, ex);
            }
        }

        private void LoadVendorDocumentsForApproval()
        {
            List<VendorBookingEntity> lstMiscBillNumber = new List<VendorBookingEntity>();
            lstMiscBillNumber = vendorBooking.GetVendorDocumentsForApproval(Session["BranchCode"].ToString());
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
            grvVendorBookingFinalDetails.DataSource = dt;
            grvVendorBookingFinalDetails.DataBind();

            grvVendorBookingFinalDetails.Rows[0].Cells.Clear();
            ViewState["GridRowCount"] = "0";
            hdnRowCnt.Value = "0";

            if (Session["BranchCode"].ToString() == "CHG")
            {
                grvVendorBookingFinalDetails.Columns[5].Visible = false;
                grvVendorBookingFinalDetails.Columns[6].Visible = false;
                grvVendorBookingFinalDetails.Columns[7].Visible = false;
                grvVendorBookingFinalDetails.Columns[8].Visible = true;
                grvVendorBookingFinalDetails.Columns[9].Visible = true;
                grvVendorBookingFinalDetails.Columns[10].Visible = true;
            }
            else
            {
                grvVendorBookingFinalDetails.Columns[5].Visible = true;
                grvVendorBookingFinalDetails.Columns[6].Visible = true;
                grvVendorBookingFinalDetails.Columns[7].Visible = true;
                grvVendorBookingFinalDetails.Columns[8].Visible = false;
                grvVendorBookingFinalDetails.Columns[9].Visible = false;
                grvVendorBookingFinalDetails.Columns[10].Visible = false;
            }
        }

        protected void imgEditToggle_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddlDocumentNumber.Visible = true;
                txtDocumentNumber.Visible = false;
                grvVendorBookingFinalDetails.Enabled = false;
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
                imgEditToggle.Visible = false;
                //txtReferenceDocumentNumber.Enabled = false;
                //txtReferenceDocumentDate.Enabled = false;
                //txtNarration.Enabled = false;
                txtInvoiceAmount.Enabled = false;
                txtGSTAmount.Enabled = false;
                txtTDSAmount.Enabled = false;
                ddlTDStype.Enabled = false;
                ddlRCMStatus.Enabled = false;
                //ddlPaymentBranch.Enabled = false;
                //txtPaymentDueDate.Enabled = false;
                BtnSubmit.Enabled = false;
                BtnSubmit.Visible = false;
                BtnReject.Visible = false;
                BtnCancel.Visible = false;
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

                    BtnSubmit.Visible = false;
                    BtnReject.Visible = false;
                    BtnCancel.Visible = false;

                    grvVendorBookingFinalDetails.DataSource = null;
                    grvVendorBookingFinalDetails.DataBind();
                }
                else
                {
                    VendorBookingEntity lstDocumentNumber = new VendorBookingEntity();
                    lstDocumentNumber = vendorBooking.GetVendorBookingHeaderandDetails(Session["BranchCode"].ToString(), ddlDocumentNumber.SelectedValue.ToString());

                    LoadAccountintPeriodView();

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

                    if (lstDocumentNumber.Status == "A" && lstDocumentNumber.PaymentStatus == "0")
                    {
                        if (lstDocumentNumber.AuthorityMatrixStatus == "1")
                        {
                            BtnSubmit.Visible = false;
                            BtnCancel.Visible = true;
                            BtnReject.Visible = false;
                        }
                        else if (lstDocumentNumber.AuthorityMatrixStatus == "0")
                        {
                            BtnSubmit.Enabled = true;
                            BtnSubmit.Visible = true;
                            BtnCancel.Visible = false;
                            BtnReject.Visible = true;
                        }
                        else
                        {
                            BtnSubmit.Visible = false;
                            BtnCancel.Visible = false;
                            BtnReject.Visible = false;
                        }
                    }
                    else
                    {
                        BtnSubmit.Visible = false;
                        BtnReject.Visible = false;
                        BtnCancel.Visible = false;
                    }

                    grvVendorBookingFinalDetails.DataSource = (object)lstDocumentNumber.Items;
                    grvVendorBookingFinalDetails.DataBind();
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(typeof(VendorBookingFinal), ex);
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

                Log.WriteException(typeof(VendorBookingFinal), exp);
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
                VendorBookingEntity vendorbookingHeader = new VendorBookingEntity();
                IMPALLibrary.Payable payableEntity = new IMPALLibrary.Payable();
                vendorbookingHeader.DocumentNumber = ddlDocumentNumber.SelectedValue;
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
                vendorbookingHeader.ApprovalLevel = Session["UserName"].ToString() + "/" + Session["UserID"];

                int result = vendorBooking.AddNewVendorBookingEntryFinal(ref vendorbookingHeader);
                if (result == 1 && vendorbookingHeader.ErrorMsg == string.Empty && vendorbookingHeader.ErrorCode == "0")
                {
                    ddlDocumentNumber.Enabled = false;
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
                    grvVendorBookingFinalDetails.Enabled = false;
                    BtnSubmit.Enabled = false;
                    ddlTDStype.Enabled = false;

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Vendor Booking Has Been Approved Successfully.');", true);
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

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                BtnSubmit.Enabled = false;
                BtnCancel.Enabled = false;
                BtnSubmit.Visible = false;
                BtnCancel.Visible = false;
                ddlDocumentNumber.Enabled = false;
                vendorBooking.CancelVendorBookingEntry(Session["BranchCode"].ToString(), ddlDocumentNumber.SelectedValue, "I", "I", Session["VendorRemarks"].ToString());
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Vendor Booking Entry has been Cancelled Sucessfully');", true);
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void BtnReject_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                BtnSubmit.Enabled = false;
                BtnReject.Enabled = false;
                BtnSubmit.Visible = false;
                BtnReject.Visible = false;
                ddlDocumentNumber.Enabled = false;
                vendorBooking.CancelVendorBookingEntry(Session["BranchCode"].ToString(), ddlDocumentNumber.SelectedValue, "I", "R", Session["VendorRemarks"].ToString());
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Vendor Booking Entry has been Rejected Sucessfully');", true);
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        [WebMethod]
        public static void SetSessionRemarks(string Remarks)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Page objp = new Page();
                objp.Session["VendorRemarks"] = objp.Session["UserName"] + "/" + objp.Session["UserID"] + " - " + Remarks;
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
                Response.Redirect("VendorBookingFinal.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                Log.WriteException(Source, ex);
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

            int PrevFinYearStatus = objReceivableInvoice.GetPreviousAccountingPeriodStatus(Session["UserID"].ToString(), "VBFinal");

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

        protected void grvVendorBookingFinalDetails_RowCreated(object sender, GridViewRowEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    TextBox txtAmount = (TextBox)e.Row.FindControl("txtAmount");
                    TextBox txtChartOfAccount = (TextBox)e.Row.FindControl("txtChartOfAccount");
                    GridViewRow grdRow = ((GridViewRow)txtAmount.Parent.Parent);

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
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
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
    }
}
