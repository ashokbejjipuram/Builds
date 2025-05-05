using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using IMPALLibrary.Masters.Sales;
using IMPALLibrary.Masters;
using IMPALLibrary.Transactions.Finance;
using IMPALLibrary.Common;
using IMPALLibrary.Masters.CustomerDetails;
using System.Data;
using IMPALWeb.UserControls;
using System.Collections;
using System.Web.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static IMPALLibrary.ReceivableInvoice;
using System.Globalization;
using IMPALLibrary.Masters.VendorDetails;

namespace IMPALWeb.Transactions.Finance.General_Ledger
{
    public partial class DebitCreditNoteCustomerBranch : System.Web.UI.Page
    {
        ReceivableInvoice objReceivableInvoice = new ReceivableInvoice();
        DebitCredit objDebitCredit = new DebitCredit();
        ImpalLibrary objImpalLibrary = new ImpalLibrary();
        Suppliers objSupplier = new Suppliers();
        Branches objBranches = new Branches();
        public List<CreditSalesTaxDetailsGST> CATaxDetailsGST = new List<CreditSalesTaxDetailsGST>();
        DebitCreditNoteGST Note = new DebitCreditNoteGST();
        List<DebitCreditAdviceFieldsGST> DebitCreditFieldNoteGST = new List<DebitCreditAdviceFieldsGST>();
        List<DebitCreditNoteDetailsCustBranchGST> DebitCreditFieldDetailsGST = new List<DebitCreditNoteDetailsCustBranchGST>();
        EinvAuthGen einvGen = new EinvAuthGen();
        VendorDetails vendor = new VendorDetails();
        Customers customers = new Customers();

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(DebitCreditNoteCustomerBranch), exp);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!IsPostBack)
                {
                    LoadDebitCredit();
                    hdninterStateStatus.Value = "2";
                    hdnScreenMode.Value = PageMode.Insert;
                    hdnBranchCode.Value = Session["BranchCode"].ToString();
                    SetEnable();
                    hdnpath.Value = Page.ResolveClientUrl("~/HandlerFile");

                    hddrefdate.Value = "";
                    hddrefStatus.Value = "";
                    Session["AutoTODsuppliers"] = "";

                    DateItem PrevDateStatus = objReceivableInvoice.GetPreviousDateStatus(Session["UserID"].ToString(), "DrCrCustomerBranch");

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
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void LoadDebitCredit()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                //LoadDropDownLists<AccountingPeriod>(objReceivableInvoice.GetAccountingPeriod(), ddlAccountPeriod, "AccPeriodCode", "Desc", false, "");
                LoadAccountintPeriod();
                LoadDropDownLists<IMPALLibrary.Common.DropDownListValue>(objImpalLibrary.GetDropDownListValues("CustomerBranchNote"), ddlDebitCreditNote, "DisplayValue", "DisplayText", true, "");
                LoadDropDownLists<IMPALLibrary.Common.DropDownListValue>(objImpalLibrary.GetDropDownListValues("CustomerSupplierBranch"), ddlSuppCustBranchInd, "DisplayValue", "DisplayText", true, "");
                //ddlSuppCustBranchInd.Items.Remove("Supplier");
                txtBranchCode.Text = (string)Session["BranchName"];
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void LoadAccountintPeriod()
        {
            AccountingPeriods Acc = new AccountingPeriods();
            List<string> lstAccountingPeriod = new List<string>();

            int PrevFinYearStatus = objReceivableInvoice.GetPreviousAccountingPeriodStatus(Session["UserID"].ToString(), "DrCrCustomerBranch");

            if (PrevFinYearStatus > 0)
            {
                lstAccountingPeriod.Add((DateTime.Today.Year - 1).ToString() + "-" + DateTime.Today.Year.ToString());
            }

            string accountingPeriod = GetCurrentFinancialYear();
            lstAccountingPeriod.Add(accountingPeriod);
            txtDocumentDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            List<AccountingPeriod> AccordingPeriod = objReceivableInvoice.GetAccountingPeriod();
            List<AccountingPeriod> FinYear = AccordingPeriod.Where(p => lstAccountingPeriod.Contains(p.Desc)).OrderByDescending(c => c.AccPeriodCode).ToList();
            LoadDropDownLists<AccountingPeriod>(FinYear, ddlAccountPeriod, "AccPeriodCode", "Desc", false, "");
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

        private void SetEnable()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (hdnScreenMode.Value == PageMode.Insert)
                {
                    ddlDocumentNumber.Visible = false;
                    txtAccountPeriod.Visible = false;
                    ddlAccountPeriod.Visible = true;
                    txtDocumentNumber.Visible = true;
                    txtDocumentNumber.Enabled = false;
                    txtDocumentNumber.Style.Add("background-color", "lightgrey");
                    imgButtonQuery.Visible = true;
                    BtnSubmit.Enabled = false;
                    btnItemDetails.Visible = true;
                    pnlView.Visible = false;
                    pnlCA.Visible = false;
                    pnlDA.Visible = false;
                    pnldebitCredit.Enabled = true;
                    pnldocument.Enabled = true;
                }
                else if (hdnScreenMode.Value == PageMode.Edit)
                {
                    ddlDocumentNumber.Visible = true;
                    txtAccountPeriod.Visible = true;
                    ddlAccountPeriod.Visible = false;
                    txtDocumentNumber.Visible = false;
                    imgButtonQuery.Visible = false;
                    BtnSubmit.Enabled = false;
                    btnItemDetails.Visible = false;
                    pnlView.Visible = true;
                    pnlCA.Visible = false;
                    pnlDA.Visible = false;
                    pnldebitCredit.Enabled = false;
                    pnldocument.Enabled = false;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void DebitCreditAdvice_SearchImageClicked(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                GridViewRow gvr = (GridViewRow)((ChartAccount)sender).Parent.Parent;
                var txtGrdChartAccount = (TextBox)gvr.FindControl("txtGrdChartAccount");
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
                var txtPartNumber = (TextBox)gvr.FindControl("txtPartNumber");
                string ChartOfAccount = Session["ChatAccCode"].ToString();
                txtGrdChartAccount.Text = ChartOfAccount;

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

                if (Convert.ToDecimal(txtGSTValue.Text) <= 0)
                {
                }
                else
                {
                    if (ddlSuppCustBranchInd.SelectedValue.Equals(SupplierCustomerBranchIndicator.Branch, StringComparison.OrdinalIgnoreCase))
                    {
                        hdnChart.Value = "0";                        

                        if (hdninterStateStatus.Value == "2")
                        {
                            ddlIGSTCode.Enabled = true;
                            txtIGSTPer.Enabled = false;
                            txtIGSTAmt.Enabled = false;
                        }
                    }
                    else
                    {
                        if (ChartOfAccount.Substring(6, 4) == "0320" || ChartOfAccount.Substring(6, 4) == "0367" || ChartOfAccount.Substring(6, 4) == "0068")
                            hdnChart.Value = "1";
                        else
                            hdnChart.Value = "0";

                        if (Session["BranchCode"].ToString() == "CHG")
                        {
                            ddlUTGSTCode.Enabled = true;
                            txtUTGSTPer.Enabled = false;
                            txtUTGSTAmt.Enabled = false;
                        }
                        else
                        {
                            ddlSGSTCode.Enabled = true;
                            txtSGSTPer.Enabled = false;
                            txtSGSTAmt.Enabled = false;
                        }

                        ddlCGSTCode.Enabled = true;
                        txtCGSTPer.Enabled = false;
                        txtCGSTAmt.Enabled = false;
                        ddlIGSTCode.Enabled = true;
                        txtIGSTPer.Enabled = false;
                        txtIGSTAmt.Enabled = false;
                    }
                }

                if (ChartOfAccount.Substring(6, 4) == "0320")
                    hddItemCode.Value = "1";
                else
                    hddItemCode.Value = "0";

                txtGrdChartAccount.Focus();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            Session["AutoTODsuppliers"] = null;

            try
            {
                BtnSubmit.Visible = false;
                string DocumentNumber = string.Empty;                
                txtRefDocumnetDate.Enabled = true;

                if (hdnScreenMode.Value == PageMode.Insert)
                {
                    if (objDebitCredit.CheckDocumentNumber(txtDocumentNumber.Text, ddlAccountPeriod.SelectedValue, Session["BranchCode"].ToString(), ddlDebitCreditNote.SelectedValue) == 0)
                    {
                        if (hdnTransType.Value == "1")
                            DocumentNumber = AddDebitCreditNote(grdCA);
                        else if (hdnTransType.Value == "2")
                        {
                            DocumentNumber = AddDebitCreditNoteCA(grdDA);
                        }

                        if (DocumentNumber != null)
                        {
                            hdnScreenMode.Value = PageMode.Edit;
                            SetEnable();
                            ViewDebitCredit(DocumentNumber);
                            ddlDocumentNumber.Visible = false;
                            txtDocumentNumber.Visible = true;
                            txtDocumentNumber.Enabled = false;
                            txtDocumentNumber.Style.Add("background-color", "none");
                            txtDocumentNumber.Text = DocumentNumber;
                        }
                    }
                    else
                    {
                        Alert("Document Number Already Exists");
                        ResetDebitCreditAdvice();
                    }
                }

                hddrefdate.Value = "";
                hddrefStatus.Value = "";
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private string AddDebitCreditNote(GridView dataGrid)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            string DocumentNumber = string.Empty;
            try
            {
                DebitCreditNoteEntityGST DrCrNoteEntity = new DebitCreditNoteEntityGST();
                DrCrNoteEntity.Items = new List<DebitCreditNoteDetailsGST>();

                if (txtValue.Text == "") txtValue.Text = "0";
                if (txtGSTValue.Text == "") txtGSTValue.Text = "0";

                txtTotalAmount.Text = (Convert.ToDecimal(txtValue.Text) + Convert.ToDecimal(txtGSTValue.Text)).ToString();

                DrCrNoteEntity.Document_Number = txtDocumentNumber.Text;
                DrCrNoteEntity.Document_Date = txtDocumentDate.Text;
                DrCrNoteEntity.Transaction_Type_Code = ddlTransactionType.SelectedValue;
                DrCrNoteEntity.Dr_Cr_Indicator = ddlDebitCreditNote.SelectedValue;
                DrCrNoteEntity.Indicator = ddlSuppCustBranchInd.SelectedValue;
                DrCrNoteEntity.Branch_Code = Session["BranchCode"].ToString();
                DrCrNoteEntity.Supplier_Code = "";
                DrCrNoteEntity.Customer_Code = ddlSuppCustBranchInd.SelectedValue == SupplierCustomerBranchIndicator.Branch ? "" : ddlSuppCustBranch.SelectedValue;
                DrCrNoteEntity.Reference_Document_Number = txtReferenceDocNumber.Text;
                DrCrNoteEntity.Remarks = txtRemarks.Text;
                DrCrNoteEntity.Value = txtTotalAmount.Text;
                DrCrNoteEntity.GSTValue = txtGSTValue.Text;
                DrCrNoteEntity.Reference_Document_Date = txtRefDocumnetDate.Text;
                DrCrNoteEntity.tr_Branch_Code = ddlSuppCustBranchInd.SelectedValue == SupplierCustomerBranchIndicator.Branch ? ddlSuppCustBranch.SelectedValue : "";
                DrCrNoteEntity.Roundoff = hdntxtRoundoff.Value;

                Decimal gstValue = 0;

                foreach (GridViewRow item in dataGrid.Rows)
                {
                    int Index = item.RowIndex;
                    var txtGrdChartAccount = (TextBox)item.FindControl("txtGrdChartAccount");
                    var txtGrdRemarks = (TextBox)item.FindControl("txtGrdRemarks");
                    var txtGrdAmount = (TextBox)item.FindControl("txtGrdValue");
                    var txtItemCode = (TextBox)item.FindControl("txtItemCode");
                    var txtReturnQuantity = (TextBox)item.FindControl("txtReturnQuantity");
                    var ddlSGSTCode = (DropDownList)item.FindControl("ddlSGSTCode");
                    var txtSGSTPer = (TextBox)item.FindControl("txtSGSTPer");
                    var txtSGSTAmt = (TextBox)item.FindControl("txtSGSTAmt");
                    var ddlCGSTCode = (DropDownList)item.FindControl("ddlCGSTCode");
                    var txtCGSTPer = (TextBox)item.FindControl("txtCGSTPer");
                    var txtCGSTAmt = (TextBox)item.FindControl("txtCGSTAmt");
                    var ddlIGSTCode = (DropDownList)item.FindControl("ddlIGSTCode");
                    var txtIGSTPer = (TextBox)item.FindControl("txtIGSTPer");
                    var txtIGSTAmt = (TextBox)item.FindControl("txtIGSTAmt");
                    var ddlUTGSTCode = (DropDownList)item.FindControl("ddlUTGSTCode");
                    var txtUTGSTPer = (TextBox)item.FindControl("txtUTGSTPer");
                    var txtUTGSTAmt = (TextBox)item.FindControl("txtUTGSTAmt");
                    var txtPartNumber = (TextBox)item.FindControl("txtPartNumber");

                    DebitCreditNoteDetailsGST DrCrNoteDetails = new DebitCreditNoteDetailsGST();

                    if (txtSGSTAmt.Text.Trim() == "" || txtSGSTAmt.Text == null)
                        txtSGSTAmt.Text = "0";
                    if (txtCGSTAmt.Text.Trim() == "" || txtCGSTAmt.Text == null)
                        txtCGSTAmt.Text = "0";
                    if (txtIGSTAmt.Text.Trim() == "" || txtIGSTAmt.Text == null)
                        txtIGSTAmt.Text = "0";
                    if (txtUTGSTAmt.Text.Trim() == "" || txtUTGSTAmt.Text == null)
                        txtUTGSTAmt.Text = "0";

                    DrCrNoteDetails.Item_Code = txtItemCode.Text != null ? txtItemCode.Text : "";
                    DrCrNoteDetails.Return_Document_Quantity = "0";
                    DrCrNoteDetails.Return_Actual_Quantity = "0";
                    DrCrNoteDetails.Amount = txtGrdAmount.Text;
                    DrCrNoteDetails.Remarks_Detail = txtGrdRemarks.Text;
                    DrCrNoteDetails.Chart_of_Account_Code = txtGrdChartAccount.Text;
                    DrCrNoteDetails.SGST_Code = ddlSGSTCode.SelectedValue != "0" ? ddlSGSTCode.SelectedItem.Text.Substring(0, 4) : "";
                    DrCrNoteDetails.SGST_Per = txtSGSTPer.Text;
                    DrCrNoteDetails.SGST_Amt = txtSGSTAmt.Text;
                    DrCrNoteDetails.CGST_Code = ddlCGSTCode.SelectedValue != "0" ? ddlCGSTCode.SelectedItem.Text.Substring(0, 4) : "";
                    DrCrNoteDetails.CGST_Per = txtCGSTPer.Text;
                    DrCrNoteDetails.CGST_Amt = txtCGSTAmt.Text;
                    DrCrNoteDetails.IGST_Code = ddlIGSTCode.SelectedValue != "0" ? ddlIGSTCode.SelectedItem.Text.Substring(0, 4) : "";
                    DrCrNoteDetails.IGST_Per = txtIGSTPer.Text;
                    DrCrNoteDetails.IGST_Amt = txtIGSTAmt.Text;
                    DrCrNoteDetails.UTGST_Code = ddlUTGSTCode.SelectedValue != "0" ? ddlUTGSTCode.SelectedItem.Text.Substring(0, 4) : "";
                    DrCrNoteDetails.UTGST_Per = txtUTGSTPer.Text;
                    DrCrNoteDetails.UTGST_Amt = txtUTGSTAmt.Text;

                    gstValue += Convert.ToDecimal(txtSGSTPer.Text) + Convert.ToDecimal(txtCGSTPer.Text) + Convert.ToDecimal(txtIGSTPer.Text) + Convert.ToDecimal(txtUTGSTPer.Text);

                    DrCrNoteEntity.Items.Add(DrCrNoteDetails);
                }

                DocumentNumber = objDebitCredit.AddNewDebitCreditNoteCustBranch(ref DrCrNoteEntity);

                if (Convert.ToDecimal(DrCrNoteEntity.GSTValue) > 0 || gstValue > 0)
                {
                    DataSet Datasetresult = new DataSet();

                    if (ddlSuppCustBranchInd.SelectedValue == "Customer" || ddlSuppCustBranchInd.SelectedValue == "Vendor")
                        Datasetresult = objDebitCredit.GetEinvoicingDetailsDrCrCust(Session["BranchCode"].ToString(), DocumentNumber);
                    else if (ddlSuppCustBranchInd.SelectedValue == "Branch")
                        Datasetresult = objDebitCredit.GetEinvoicingDetailsDrCrBranch(Session["BranchCode"].ToString(), DocumentNumber);

                    GenerateJSON objGenJsonData = new GenerateJSON();

                    string JSONData = JsonConvert.SerializeObject(objGenJsonData.GenerateInvoiceJSONData(Datasetresult, Session["BranchCode"].ToString()), Formatting.Indented);

                    einvGen.EinvoiceAuthentication(JSONData.Substring(3, JSONData.Length - 4), Session["BranchCode"].ToString(), DocumentNumber, "1", "GENIRN", Datasetresult.Tables[0].Rows[0]["Seller_GST"].ToString(), Datasetresult.Tables[0].Rows[0]["Doc_Type"].ToString(), Datasetresult.Tables[0].Rows[0]["Document_Date"].ToString().Replace("-", "/"), Datasetresult);

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Debit Credit Note Entry Has been Generated Successfully with QR Code.');", true);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return DocumentNumber;
        }

        private string AddDebitCreditNoteCA(GridView dataGrid)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            string DocumentNumber = string.Empty;
            try
            {
                DebitCreditNoteEntityGST DrCrNoteEntity = new DebitCreditNoteEntityGST();
                DrCrNoteEntity.Items = new List<DebitCreditNoteDetailsGST>();

                DrCrNoteEntity.Document_Number = txtDocumentNumber.Text;
                DrCrNoteEntity.Document_Date = txtDocumentDate.Text;
                DrCrNoteEntity.Transaction_Type_Code = ddlTransactionType.SelectedValue;
                DrCrNoteEntity.Dr_Cr_Indicator = ddlDebitCreditNote.SelectedValue;
                DrCrNoteEntity.Branch_Code = Session["BranchCode"].ToString();
                DrCrNoteEntity.Indicator = ddlSuppCustBranchInd.SelectedValue;
                DrCrNoteEntity.Supplier_Code = "";
                DrCrNoteEntity.Customer_Code = ddlSuppCustBranchInd.SelectedValue == SupplierCustomerBranchIndicator.Branch ? "" : ddlSuppCustBranch.SelectedValue;
                DrCrNoteEntity.Reference_Document_Number = txtReferenceDocNumber.Text;
                DrCrNoteEntity.Remarks = txtRemarks.Text;
                DrCrNoteEntity.TotalValue = txtTotalAmount.Text;
                DrCrNoteEntity.Value = txtValue.Text;
                DrCrNoteEntity.GSTValue = txtGSTValue.Text;
                DrCrNoteEntity.Reference_Document_Date = txtRefDocumnetDate.Text;
                DrCrNoteEntity.tr_Branch_Code = ddlSuppCustBranchInd.SelectedValue == SupplierCustomerBranchIndicator.Branch ? ddlSuppCustBranch.SelectedValue : "";
                DrCrNoteEntity.Roundoff = hdntxtRoundoff.Value;
                DrCrNoteEntity.DocExists = hddrefdate.Value;

                List<CreditSalesTaxDetailsGST> SSCATaxDetails = (List<CreditSalesTaxDetailsGST>)Session["CATaxDetailsGST"];
                Decimal gstValue = 0;

                foreach (GridViewRow item in dataGrid.Rows)
                {
                    int Index = item.RowIndex;
                    var chkSelected = (CheckBox)item.FindControl("chkSelected");
                    var txtSGST = (TextBox)item.FindControl("txtSGST");
                    var txtCGST = (TextBox)item.FindControl("txtCGST");
                    var txtIGST = (TextBox)item.FindControl("txtIGST");
                    var txtUTGST = (TextBox)item.FindControl("txtUTGST");
                    var txtGrdValue = (TextBox)item.FindControl("txtGrdValue");
                    var txtReturnquantity = (TextBox)item.FindControl("txtReturnquantity");
                    var txtQuantity = (TextBox)item.FindControl("txtQuantity");
                    var txtPartNumber = (TextBox)item.FindControl("txtPartNumber");
                    var hdnSerialNumber = (HiddenField)item.FindControl("txtSerialNumber");
                    var txtReturnvalue = (TextBox)item.FindControl("txtReturnvalue");
                    string SGST_Code = string.Empty;
                    string SGST_per = string.Empty;
                    string SGST_Amount = string.Empty;
                    string CGST_Code = string.Empty;
                    string CGST_per = string.Empty;
                    string CGST_Amount = string.Empty;
                    string IGST_Code = string.Empty;
                    string IGST_per = string.Empty;
                    string IGST_Amount = string.Empty;
                    string UTGST_Code = string.Empty;
                    string UTGST_per = string.Empty;
                    string UTGST_Amount = string.Empty;

                    DebitCreditNoteDetailsGST DrCrNoteDetails = new DebitCreditNoteDetailsGST();

                    if (item.Enabled && chkSelected.Checked)
                    {
                        DrCrNoteDetails.Serial_Number = hdnSerialNumber.Value;
                        DrCrNoteDetails.Item_Code = SSCATaxDetails[Index].item_code;
                        DrCrNoteDetails.Return_Document_Quantity = txtQuantity.Text;
                        DrCrNoteDetails.Return_Actual_Quantity = txtReturnquantity.Text;
                        DrCrNoteDetails.Amount = txtReturnvalue.Text;
                        DrCrNoteDetails.Remarks_Detail = txtRemarks.Text;
                        DrCrNoteDetails.Chart_of_Account_Code = txtChartOtAccount.Text;

                        if (Convert.ToDecimal(txtSGST.Text) > 0)
                        {
                            SGST_Code = SSCATaxDetails[Index].SGSTSalesCode;
                            SGST_per = SSCATaxDetails[Index].SGSTSalesPer;
                            SGST_Amount = txtSGST.Text;
                        }

                        if (Convert.ToDecimal(txtCGST.Text) > 0)
                        {
                            CGST_Code = SSCATaxDetails[Index].CGSTSalesCode;
                            CGST_per = SSCATaxDetails[Index].CGSTSalesPer;
                            CGST_Amount = txtCGST.Text;
                        }

                        if (Convert.ToDecimal(txtIGST.Text) > 0)
                        {
                            IGST_Code = SSCATaxDetails[Index].IGSTSalesCode;
                            IGST_per = SSCATaxDetails[Index].IGSTSalesPer;
                            IGST_Amount = txtIGST.Text;
                        }

                        if (Convert.ToDecimal(txtUTGST.Text) > 0)
                        {
                            UTGST_Code = SSCATaxDetails[Index].UTGSTSalesCode;
                            UTGST_per = SSCATaxDetails[Index].UTGSTSalesPer;
                            UTGST_Amount = txtUTGST.Text;
                        }
                        
                        DrCrNoteDetails.SGST_Code = SGST_Code;
                        DrCrNoteDetails.SGST_Per = SGST_per;
                        DrCrNoteDetails.SGST_Amt = SGST_Amount;
                        DrCrNoteDetails.CGST_Code = CGST_Code;
                        DrCrNoteDetails.CGST_Per = CGST_per;
                        DrCrNoteDetails.CGST_Amt = CGST_Amount;
                        DrCrNoteDetails.IGST_Code = IGST_Code;
                        DrCrNoteDetails.IGST_Per = IGST_per;
                        DrCrNoteDetails.IGST_Amt = IGST_Amount;
                        DrCrNoteDetails.UTGST_Code = UTGST_Code;
                        DrCrNoteDetails.UTGST_Per = UTGST_per;
                        DrCrNoteDetails.UTGST_Amt = UTGST_Amount;

                        gstValue += Convert.ToDecimal(txtSGST.Text) + Convert.ToDecimal(txtCGST.Text) + Convert.ToDecimal(txtIGST.Text) + Convert.ToDecimal(txtUTGST.Text);

                        DrCrNoteEntity.Items.Add(DrCrNoteDetails);
                    }                    
                }

                DocumentNumber = objDebitCredit.AddNewDebitCreditNoteCustBranchCA(ref DrCrNoteEntity);

                if (Convert.ToDecimal(DrCrNoteEntity.GSTValue) > 0 || gstValue > 0)
                {
                    DataSet Datasetresult = new DataSet();

                    if (ddlSuppCustBranchInd.SelectedValue == "Customer")
                        Datasetresult = objDebitCredit.GetEinvoicingDetailsDrCrCust(Session["BranchCode"].ToString(), DocumentNumber);
                    else if (ddlSuppCustBranchInd.SelectedValue == "Branch")
                        Datasetresult = objDebitCredit.GetEinvoicingDetailsDrCrBranch(Session["BranchCode"].ToString(), DocumentNumber);

                    GenerateJSON objGenJsonData = new GenerateJSON();

                    string JSONData = JsonConvert.SerializeObject(objGenJsonData.GenerateInvoiceJSONData(Datasetresult, Session["BranchCode"].ToString()), Formatting.Indented);

                    einvGen.EinvoiceAuthentication(JSONData.Substring(3, JSONData.Length - 4), Session["BranchCode"].ToString(), DocumentNumber, "1", "GENIRN", Datasetresult.Tables[0].Rows[0]["Seller_GST"].ToString(), Datasetresult.Tables[0].Rows[0]["Doc_Type"].ToString(), Datasetresult.Tables[0].Rows[0]["Document_Date"].ToString().Replace("-", "/"), Datasetresult);

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Debit Credit Note Entry Has been Generated Successfully with QR Code.');", true);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return DocumentNumber;
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

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Session["AutoTODsuppliers"] = null;
                Server.ClearError();
                Response.Redirect("DebitCreditNoteCustomerBranch.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void ResetDebitCreditAdvice()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Server.ClearError();
                Response.Redirect("DebitCreditNoteCustomerBranch.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {

            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        struct PageMode
        {
            public const string Insert = "INSERT";
            public const string Edit = "EDIT";
            public const string ReadOnly = "READONLY";
        }

        struct SupplierCustomerBranchIndicator
        {
            public const string Supplier = "Supplier";
            public const string Customer = "Customer";
            public const string Branch = "Branch";
            public const string Vendor = "Vendor";
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
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void LoadDropDownListsBranch<T>(List<T> ListData, DropDownList DDlDropDown, string value_field, string text_field, bool bselect, string DefaultText)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                DDlDropDown.DataSource = ListData;
                DDlDropDown.DataTextField = text_field;
                DDlDropDown.DataValueField = value_field;
                DDlDropDown.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void LoadDropDownVsIndicator(string Indicator, string DebitCreditNote)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {

                if (Indicator.Equals(SupplierCustomerBranchIndicator.Branch, StringComparison.OrdinalIgnoreCase))
                    LoadDropDownListsBranch<IMPALLibrary.Branch>(objBranches.GetCorpBranch(), ddlSuppCustBranch, "BranchCode", "BranchName", false, "");
                else if (Indicator.Equals(SupplierCustomerBranchIndicator.Customer, StringComparison.OrdinalIgnoreCase))
                    LoadDropDownLists<CustomerDtls>(objDebitCredit.GetCustomer(Session["BranchCode"].ToString()), ddlSuppCustBranch, "Code", "Name", false, "");
                else if (ddlSuppCustBranchInd.SelectedValue.Equals(SupplierCustomerBranchIndicator.Vendor, StringComparison.OrdinalIgnoreCase))
                    LoadDropDownLists<VendorDtls>(vendor.GetVendorswithLocation(Session["BranchCode"].ToString()), ddlSuppCustBranch, "Code", "Name", true, "");

                LoadDropDownLists<TransactionType>(objDebitCredit.GetCustomerBranchTransactionTypeView(), ddlTransactionType, "Transaction_Type_code", "Transaction_Type_Description", false, "");

            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void imgButtonQuery_Click(object sender, ImageClickEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                LoadDropDownLists<DocumentNumber>(objDebitCredit.GetAllCustBranchDocumentNumber(Session["BranchCode"].ToString()), ddlDocumentNumber, "Document_Number", "Document_Number", true, "");
                
                hdnScreenMode.Value = PageMode.Edit;
                SetEnable();
                //ViewDebitCredit("");
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void ViewDebitCredit(string DocumentNumber)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                BindDebitCredit(DocumentNumber == "" ? ddlDocumentNumber.SelectedValue : DocumentNumber);
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void BindDebitCredit(string DocumentNumber)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {

                Note = objDebitCredit.ViewDebitCreditNoteCustBranchGST(Session["BranchCode"].ToString(), DocumentNumber);
                if (Note != null)
                {
                    DebitCreditFieldNoteGST = Note.ListDebitCreditNoteGST;
                    DebitCreditFieldDetailsGST = Note.ListDebitCreditNoteDetailsCustBranchGST;

                    if (DebitCreditFieldNoteGST.Count > 0)
                    {
                        LoadDropDownVsIndicator(DebitCreditFieldNoteGST[0].Indicator, (DebitCreditFieldNoteGST[0].Dr_Cr_Indicator));
                        txtDocumentNumber.Text = DebitCreditFieldNoteGST[0].Document_Number;
                        //ddlAccountPeriod.SelectedItem.Text = DebitCreditFieldNoteGST[0].Accountingperiod;
                        txtAccountPeriod.Text = DebitCreditFieldNoteGST[0].Accountingperiod;
                        txtDocumentDate.Text = DebitCreditFieldNoteGST[0].Document_Date;
                        txtBranchCode.Text = DebitCreditFieldNoteGST[0].Branch_Code;
                        ddlDebitCreditNote.SelectedValue = DebitCreditFieldNoteGST[0].Dr_Cr_Indicator;
                        ddlSuppCustBranchInd.SelectedValue = DebitCreditFieldNoteGST[0].Indicator;
                        ddlSuppCustBranch.SelectedValue = DebitCreditFieldNoteGST[0].Indicator_Code;
                        ddlTransactionType.SelectedValue = DebitCreditFieldNoteGST[0].Transaction_Type_Code;
                        txtReferenceDocNumber.Text = DebitCreditFieldNoteGST[0].Reference_Document_Number;
                        txtRefDocumnetDate.Text = DebitCreditFieldNoteGST[0].Reference_Document_Date;
                        txtValue.Text = FormatString(DebitCreditFieldNoteGST[0].Value);
                        txtGSTValue.Text = FormatString(DebitCreditFieldNoteGST[0].Tax_Value);
                        txtRemarks.Text = DebitCreditFieldNoteGST[0].Remarks;
                    }

                    if (DebitCreditFieldDetailsGST.Count > 0)
                    {
                        grdview.DataSource = DebitCreditFieldDetailsGST;
                        grdview.DataBind();
                    }
                    else
                    {
                        grdview.DataSource = null;
                        grdview.DataBind();
                    }
                }
                else
                {

                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ddlSuppCustBranchInd_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddlAccountPeriod.Enabled = false;

                if (ddlSuppCustBranchInd.SelectedValue.Equals(SupplierCustomerBranchIndicator.Branch, StringComparison.OrdinalIgnoreCase))
                    LoadDropDownListsBranch<IMPALLibrary.Branch>(objBranches.GetCorpBranch(), ddlSuppCustBranch, "BranchCode", "BranchName", true, "");
                else if (ddlSuppCustBranchInd.SelectedValue.Equals(SupplierCustomerBranchIndicator.Customer, StringComparison.OrdinalIgnoreCase))
                    LoadDropDownLists<CustomerDtls>(objDebitCredit.GetCustomer(Session["BranchCode"].ToString()), ddlSuppCustBranch, "Code", "Name", true, "");
                else if (ddlSuppCustBranchInd.SelectedValue.Equals(SupplierCustomerBranchIndicator.Vendor, StringComparison.OrdinalIgnoreCase))
                    LoadDropDownLists<VendorDtls>(vendor.GetVendorswithLocation(Session["BranchCode"].ToString()), ddlSuppCustBranch, "Code", "Name", true, "");

                //LoadDropDownLists<TransactionType>(objDebitCredit.GetCustomerBranchTransactionType(ddlDebitCreditNote.SelectedValue, ddlSuppCustBranchInd.SelectedValue, ddlAccountPeriod.SelectedIndex), ddlTransactionType, "Transaction_Type_code", "Transaction_Type_Description", true, "");

                if (ddlDebitCreditNote.SelectedValue.Equals("CA", StringComparison.OrdinalIgnoreCase) && ddlSuppCustBranchInd.SelectedValue.Equals("Customer", StringComparison.OrdinalIgnoreCase))
                {
                    if (ddlAccountPeriod.SelectedIndex == 0)
                    {
                        LoadDropDownLists<IMPALLibrary.Common.DropDownListValue>(objImpalLibrary.GetDropDownListValues("DebitCreditCustomerCAtransactionTypeCurYear"), ddlTransactionType, "DisplayValue", "DisplayText", true, "");
                    }
                    else
                    {
                        LoadDropDownLists<IMPALLibrary.Common.DropDownListValue>(objImpalLibrary.GetDropDownListValues("DebitCreditCustomerCAtransactionTypePrevYear"), ddlTransactionType, "DisplayValue", "DisplayText", true, "");
                    }
                }
                else if (ddlDebitCreditNote.SelectedValue.Equals("DA", StringComparison.OrdinalIgnoreCase) && ddlSuppCustBranchInd.SelectedValue.Equals("Customer", StringComparison.OrdinalIgnoreCase))
                {
                    LoadDropDownLists<IMPALLibrary.Common.DropDownListValue>(objImpalLibrary.GetDropDownListValues("DebitCreditCustomerDAtransactionType"), ddlTransactionType, "DisplayValue", "DisplayText", true, "");
                }
                else if (ddlDebitCreditNote.SelectedValue.Equals("CA", StringComparison.OrdinalIgnoreCase) && ddlSuppCustBranchInd.SelectedValue.Equals("Branch", StringComparison.OrdinalIgnoreCase))
                {
                    LoadDropDownLists<IMPALLibrary.Common.DropDownListValue>(objImpalLibrary.GetDropDownListValues("DebitCreditBranchCAtransactionType"), ddlTransactionType, "DisplayValue", "DisplayText", true, "");
                }
                else if (ddlDebitCreditNote.SelectedValue.Equals("DA", StringComparison.OrdinalIgnoreCase) && ddlSuppCustBranchInd.SelectedValue.Equals("Branch", StringComparison.OrdinalIgnoreCase))
                {
                    LoadDropDownLists<IMPALLibrary.Common.DropDownListValue>(objImpalLibrary.GetDropDownListValues("DebitCreditBranchDAtransactionType"), ddlTransactionType, "DisplayValue", "DisplayText", true, "");
                }
                else if (ddlDebitCreditNote.SelectedValue.Equals("CA", StringComparison.OrdinalIgnoreCase) && ddlSuppCustBranchInd.SelectedValue.Equals("Vendor", StringComparison.OrdinalIgnoreCase))
                {
                    LoadDropDownLists<IMPALLibrary.Common.DropDownListValue>(objImpalLibrary.GetDropDownListValues("DebitCreditBranchCAtransactionType"), ddlTransactionType, "DisplayValue", "DisplayText", true, "");
                }
                else if (ddlDebitCreditNote.SelectedValue.Equals("DA", StringComparison.OrdinalIgnoreCase) && ddlSuppCustBranchInd.SelectedValue.Equals("Vendor", StringComparison.OrdinalIgnoreCase))
                {
                    LoadDropDownLists<IMPALLibrary.Common.DropDownListValue>(objImpalLibrary.GetDropDownListValues("DebitCreditBranchDAtransactionType"), ddlTransactionType, "DisplayValue", "DisplayText", true, "");
                }

                RemoveDropodownData(ddlSuppCustBranchInd, ddlSuppCustBranchInd.SelectedValue, ddlSuppCustBranchInd.SelectedItem.ToString());
                RemoveDropodownData(ddlAccountPeriod, ddlAccountPeriod.SelectedValue, ddlAccountPeriod.SelectedItem.ToString());
                RemoveDropodownData(ddlDebitCreditNote, ddlDebitCreditNote.SelectedValue, ddlDebitCreditNote.SelectedItem.ToString());

                ddlSuppCustBranchInd.Focus();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ddlAccountPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddlAccountPeriod.Enabled = false;

                if (ddlAccountPeriod.SelectedIndex == 0)
                    txtDocumentDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                else
                    txtDocumentDate.Text = objDebitCredit.GetDocumentDate(ddlAccountPeriod.SelectedValue);

                txtDocumentDate.Focus();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void RemoveDropodownData(DropDownList ddlDropDown, string SelectedValue, string SelectedText)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (ddlDropDown.SelectedIndex > 0)
                {
                    ListItem SelectedData = new ListItem();
                    SelectedData.Text = SelectedText;
                    SelectedData.Value = SelectedValue;
                    ddlDropDown.Items.Clear();
                    ddlDropDown.Items.Add(SelectedData);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ddlSuppCustBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddlAccountPeriod.Enabled = false;

                if (ddlSuppCustBranchInd.SelectedValue.Equals(SupplierCustomerBranchIndicator.Customer, StringComparison.OrdinalIgnoreCase))
                {
                    LoadCustomerData(ddlSuppCustBranch.SelectedValue);
                    txtGSTValue.Enabled = true;
                }
                else if (ddlSuppCustBranchInd.SelectedValue.Equals(SupplierCustomerBranchIndicator.Branch, StringComparison.OrdinalIgnoreCase))
                {
                    int interStateStatus = objReceivableInvoice.GetInterStateStatusGST(Session["BranchCode"].ToString(), ddlSuppCustBranch.SelectedValue);
                    hdninterStateStatus.Value = interStateStatus.ToString();

                    if (interStateStatus == 1)
                    {
                        txtGSTValue.Enabled = false;
                        txtGSTValue.Text = "0";
                    }
                    else
                    {
                        txtGSTValue.Enabled = true;
                    }
                }
                else if (ddlSuppCustBranchInd.SelectedValue.Equals(SupplierCustomerBranchIndicator.Vendor, StringComparison.OrdinalIgnoreCase))
                {
                    LoadVendorData(ddlSuppCustBranch.SelectedValue);
                    txtGSTValue.Enabled = true;
                }

                ddlSuppCustBranch.Focus();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ddlTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddlAccountPeriod.Enabled = false;
                //TextBox txtGSTINNo = (FormDetailCustomer.FindControl("txtGSTINNo") as TextBox);

                RemoveDropodownData(ddlSuppCustBranchInd, ddlSuppCustBranchInd.SelectedValue, ddlSuppCustBranchInd.SelectedItem.ToString());
                RemoveDropodownData(ddlAccountPeriod, ddlAccountPeriod.SelectedValue, ddlAccountPeriod.SelectedItem.ToString());
                RemoveDropodownData(ddlDebitCreditNote, ddlDebitCreditNote.SelectedValue, ddlDebitCreditNote.SelectedItem.ToString());
                RemoveDropodownData(ddlSuppCustBranch, ddlSuppCustBranch.SelectedValue, ddlSuppCustBranch.SelectedItem.ToString());
                RemoveDropodownData(ddlTransactionType, ddlTransactionType.SelectedValue, ddlTransactionType.SelectedItem.ToString());
                ddlTransactionType.Focus();

                if ((ddlDebitCreditNote.SelectedValue.ToUpper() == "CA" && ddlTransactionType.SelectedValue.ToString() == "751")
                    || ddlDebitCreditNote.SelectedValue.ToUpper() == "DA")
                {
                    txtDocumentNumber.Enabled = false;
                    txtDocumentNumber.Style.Add("background-color", "lightgrey");

                }
                else
                {
                    txtDocumentNumber.Enabled = true;
                    txtDocumentNumber.Style.Add("background-color", "none");
                }

                if ((ddlDebitCreditNote.SelectedValue.ToUpper() == "CA" && ddlSuppCustBranchInd.SelectedValue == "Customer" && (ddlTransactionType.SelectedValue.ToString() == "752"
                    || ddlTransactionType.SelectedValue.ToString() == "754" || ddlTransactionType.SelectedValue.ToString() == "755"
                    || ddlTransactionType.SelectedValue.ToString() == "756" || ddlTransactionType.SelectedValue.ToString() == "757"))
                    || (ddlDebitCreditNote.SelectedValue.ToUpper() == "DA" && (ddlSuppCustBranchInd.SelectedValue == "Customer" || ddlSuppCustBranchInd.SelectedValue == "Vendor"))
                    || (ddlSuppCustBranchInd.SelectedValue == "Branch" && hdninterStateStatus.Value == "2"))
                {
                    txtGSTValue.Enabled = true;
                }
                else
                {
                    int GSTvalueStatus = objReceivableInvoice.GetCustGSTValueStatus(Session["BranchCode"].ToString());

                    if (GSTvalueStatus == 1 || (Session["UserID"].ToString() == "hoadmin1" || Session["UserID"].ToString() == "hoadmin2"))
                        txtGSTValue.Enabled = true;
                    else
                    {
                        txtGSTValue.Text = "0";
                        txtGSTValue.Enabled = false;
                    }
                }

                if (ddlSuppCustBranchInd.SelectedValue == "Customer" && (ddlTransactionType.SelectedValue == "658" || ddlTransactionType.SelectedValue == "751" || ddlTransactionType.SelectedValue == "753" || ddlTransactionType.SelectedValue == "758"))
                    Session["AutoTODsuppliers"] = "1";
                else
                    Session["AutoTODsuppliers"] = "";
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ddlDebitCreditNote_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddlAccountPeriod.Enabled = false;
                //if (hdnScreenMode.Value == PageMode.Insert)
                //{
                //    if (objDebitCredit.CheckDocumentNumber(txtDocumentNumber.Text, ddlAccountPeriod.SelectedValue, Session["BranchCode"].ToString(), ddlDebitCreditNote.SelectedValue) != 0)
                //    {
                //        Alert("Document Number Already Exists");
                //        ResetDebitCreditAdvice();
                //    }
                //    ddlAccountPeriod.Enabled = false;
                //    ddlDebitCreditNote.Enabled = false;
                //    imgButtonQuery.Enabled = false;
                //}
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void txtDocumentNumber_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (hdnScreenMode.Value == PageMode.Insert)
                {
                    if (objDebitCredit.CheckDocumentNumber(txtDocumentNumber.Text, ddlAccountPeriod.SelectedValue, Session["BranchCode"].ToString(), ddlDebitCreditNote.SelectedValue) != 0)
                    {
                        Alert("Document Number Already Exists");
                        ResetDebitCreditAdvice();
                    }

                    ddlAccountPeriod.Enabled = false;
                    imgButtonQuery.Enabled = false;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(DebitCreditNoteCustomerBranch), exp);
            }
        }

        protected void txtGrdValue_TextChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow gvr = (GridViewRow)((TextBox)sender).Parent.Parent;
                var txtGrdValue = (TextBox)gvr.FindControl("txtGrdValue");

                if (txtGrdValue.Text != "")
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
                        txtSGSTAmt.Text = TwoDecimalConversion(((Convert.ToDecimal(txtGrdValue.Text) * Convert.ToDecimal(txtSGSTPer.Text)) / 100).ToString());
                    }
                    else
                    {
                        txtSGSTAmt.Text = "0";
                    }

                    if (ddlCGSTCode.SelectedIndex > 0)
                    {
                        txtCGSTAmt.Text = TwoDecimalConversion(((Convert.ToDecimal(txtGrdValue.Text) * Convert.ToDecimal(txtCGSTPer.Text)) / 100).ToString());
                    }
                    else
                    {
                        txtCGSTAmt.Text = "0";
                    }

                    if (ddlIGSTCode.SelectedIndex > 0)
                    {
                        txtIGSTAmt.Text = TwoDecimalConversion(((Convert.ToDecimal(txtGrdValue.Text) * Convert.ToDecimal(txtIGSTPer.Text)) / 100).ToString());
                    }
                    else
                    {
                        txtIGSTAmt.Text = "0";
                    }

                    if (ddlUTGSTCode.SelectedIndex > 0)
                    {
                        txtUTGSTAmt.Text = TwoDecimalConversion(((Convert.ToDecimal(txtGrdValue.Text) * Convert.ToDecimal(txtUTGSTPer.Text)) / 100).ToString());
                    }
                    else
                    {
                        txtUTGSTAmt.Text = "0";
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(DebitCreditNoteCustomerBranch), exp);
            }
        }

        private void LoadCustomerData(string CustomerCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Customer CustomerDts = objReceivableInvoice.GetCustomerInfoByCustomerCode(CustomerCode, Session["BranchCode"].ToString());
                List<Customer> CustomerList = new List<Customer>();
                CustomerList.Add(CustomerDts);
                FormDetailCustomer.DataSource = CustomerList;
                FormDetailCustomer.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void LoadVendorData(string VendorCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Customer VendorDts = customers.GetVendorInfoByVendorCode(Session["BranchCode"].ToString(), VendorCode);
                List<Customer> CustomerList = new List<Customer>();
                CustomerList.Add(VendorDts);
                FormDetailCustomer.DataSource = CustomerList;
                FormDetailCustomer.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ddlDocumentNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (ddlDocumentNumber.SelectedIndex > 0)
                {
                    BindDebitCredit(ddlDocumentNumber.SelectedValue);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void btnItemDetails_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                BtnSubmit.Enabled = true;
                btnItemDetails.Enabled = false;
                btnItemDetails.Visible = false;
                txtValue.Enabled = false;
                txtGSTValue.Enabled = false;
                txtReferenceDocNumber.Enabled = false;
                txtRefDocumnetDate.Enabled = false;

                if (hddrefdate.Value == "1")
                    txtRefDocumnetDate.Enabled = false;

                if ((ddlDebitCreditNote.SelectedValue.Equals("CA", StringComparison.OrdinalIgnoreCase) && (ddlTransactionType.SelectedValue != "651" && ddlTransactionType.SelectedValue != "751")) || (ddlDebitCreditNote.SelectedValue.Equals("CA", StringComparison.OrdinalIgnoreCase) && ddlSuppCustBranchInd.SelectedValue == SupplierCustomerBranchIndicator.Branch && (ddlTransactionType.SelectedValue == "651" || ddlTransactionType.SelectedValue == "751") && hddrefdate.Value == "1"))
                {
                    pnlCA.Visible = false;
                    pnlDA.Visible = true;
                    hdnTransType.Value = "2";
                    txtReferenceDocNumber.ReadOnly = true;
                    txtValue.ReadOnly = true;
                    txtGSTValue.ReadOnly = true;
                    imgButtonQuery.Enabled = false;

                    if (ddlTransactionType.SelectedValue == "656" || ddlTransactionType.SelectedValue == "756" || ddlTransactionType.SelectedValue == "758" || ddlTransactionType.SelectedValue == "658" || ddlTransactionType.SelectedValue == "753" || ddlTransactionType.SelectedValue == "651" || ddlTransactionType.SelectedValue == "751")
                        hddreturnQntCA.Value = "1";

                    if (!GetCADetails())
                    {
                        ResetDebitCreditAdvice();
                    }
                }
                else
                {
                    pnlCA.Visible = true;
                    pnlDA.Visible = false;
                    hdnTransType.Value = "1";
                    SetInitialRow();
                    DisableReturnQnt();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void DisableReturnQnt()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (grdCA.Rows.Count > 0)
                {
                    for (int i = 0; i < grdCA.Rows.Count; i++)
                    {
                        TextBox txtReturnQuantity = (TextBox)grdCA.Rows[i].Cells[5].FindControl("txtReturnQuantity");
                        if ((ddlTransactionType.SelectedValue == "642") || (ddlTransactionType.SelectedValue == "742"))
                        {
                            //txtReturnQty.ReadOnly = true;
                            txtReturnQuantity.Enabled = false;
                        }
                        else
                        {
                            //txtReturnQty.ReadOnly = false;
                            txtReturnQuantity.Enabled = true;
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private bool GetCADetails()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            bool _Flag = false;
            try
            {
                if (hddrefStatus.Value == "0")
                {
                    string ChartOfAccount = (ddlSuppCustBranchInd.SelectedValue == SupplierCustomerBranchIndicator.Customer ? "10000103200000001" : "21212906030000001") + Session["BranchCode"].ToString();
                    txtTotalAmount.Text = (Convert.ToDecimal(txtValue.Text) + Convert.ToDecimal(txtGSTValue.Text)).ToString();
                    txtCollectedAmount.Text = "0";
                    txtChartOtAccount.Text = ChartOfAccount;
                    TextBox txtGSTINNo = (FormDetailCustomer.FindControl("txtGSTINNo") as TextBox);

                    if (txtGSTINNo != null)
                    {
                        CATaxDetailsGST = objDebitCredit.GetCATaxDetailsGST(Session["BranchCode"].ToString(), txtReferenceDocNumber.Text, ddlTransactionType.SelectedValue, (ddlSuppCustBranchInd.SelectedValue == SupplierCustomerBranchIndicator.Customer ? "1" : "2"), txtGSTINNo.Text);
                    }
                    else
                    {
                        CATaxDetailsGST = objDebitCredit.GetCATaxDetailsGST(Session["BranchCode"].ToString(), txtReferenceDocNumber.Text, ddlTransactionType.SelectedValue, (ddlSuppCustBranchInd.SelectedValue == SupplierCustomerBranchIndicator.Customer ? "1" : "2"), "");
                    }

                    grdDA.DataSource = CATaxDetailsGST;
                    grdDA.DataBind();
                    Session["CATaxDetailsGST"] = CATaxDetailsGST;
                    DisableReturnCA();
                    _Flag = true;
                }
                else if (hddrefStatus.Value == "1")
                {
                    Alert("Reference document number not available for this customer");
                }
                else if (hddrefStatus.Value == "2")
                {
                    Alert("Reference document number not available for this  branch");
                }
                else if (hddrefStatus.Value == "3")
                {
                    Alert("Already credit note passed for this reference document number");
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return _Flag;
        }

        private void DisableReturnCA()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (grdDA.Rows.Count > 0)
                {
                    for (int i = 0; i < grdDA.Rows.Count; i++)
                    {
                        CheckBox Chk = (CheckBox)grdDA.Rows[i].Cells[0].FindControl("chkSelected");
                        TextBox txtQuantity = (TextBox)grdDA.Rows[i].Cells[2].FindControl("txtQuantity");
                        TextBox txtReturnquantity = (TextBox)grdDA.Rows[i].Cells[4].FindControl("txtReturnQuantity");
                        TextBox txtReturnvalue = (TextBox)grdDA.Rows[i].Cells[5].FindControl("txtReturnvalue");
                        TextBox txtSGST = (TextBox)grdDA.Rows[i].Cells[6].FindControl("txtSGST");
                        TextBox txtCGST = (TextBox)grdDA.Rows[i].Cells[7].FindControl("txtCGST");
                        TextBox txtIGST = (TextBox)grdDA.Rows[i].Cells[8].FindControl("txtIGST");
                        TextBox txtUTGST = (TextBox)grdDA.Rows[i].Cells[9].FindControl("txtUTGST");

                        if (ddlTransactionType.SelectedValue == "758" || ddlTransactionType.SelectedValue == "658")
                        {
                            txtReturnquantity.Enabled = false;
                            txtReturnvalue.Enabled = true;
                            txtSGST.Enabled = true;
                            txtCGST.Enabled = true;
                            txtIGST.Enabled = true;
                            txtUTGST.Enabled = true;
                        }
                        else
                        {
                            if (Convert.ToInt64(txtReturnquantity.Text) >= Convert.ToInt64(txtQuantity.Text))
                            {
                                grdDA.Rows[i].Enabled = false;
                                Chk.Checked = true;
                                txtReturnquantity.Enabled = false;
                                txtReturnvalue.Enabled = false;
                            }
                            else
                            {
                                grdDA.Rows[i].Enabled = true;
                                Chk.Checked = false;
                                txtReturnquantity.Enabled = true;
                                txtReturnvalue.Enabled = false;
                            }

                            if (ddlTransactionType.SelectedValue == "752" || ddlTransactionType.SelectedValue == "753" || ddlTransactionType.SelectedValue == "754" || ddlTransactionType.SelectedValue == "755" || ddlTransactionType.SelectedValue == "756" || ddlTransactionType.SelectedValue == "757")
                            {
                                if (ddlTransactionType.SelectedValue == "756")
                                    txtReturnvalue.Enabled = true;
                                else
                                    txtReturnvalue.Enabled = false; //true                                
                            }
                            else
                            {
                                txtReturnvalue.Enabled = true;                                
                            }

                            txtSGST.Enabled = false; //true
                            txtCGST.Enabled = false; //true
                            txtIGST.Enabled = false; //true
                            txtUTGST.Enabled = false; //true
                        }
                    }
                }
                else
                {
                    grdDA.DataSource = null;
                    grdDA.DataBind();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        [WebMethod]
        public static List<CreditSalesTaxDetailsGST> GetCollection()
        {
            List<CreditSalesTaxDetailsGST> List = null;
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                List = (List<CreditSalesTaxDetailsGST>)HttpContext.Current.Session["CATaxDetailsGST"];
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return List;
        }

        private void SetInitialRow()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                DataTable dt = new DataTable();
                DataRow dr = null;
                dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                dt.Columns.Add(new DataColumn("ChartOfAccount", typeof(string)));
                dt.Columns.Add(new DataColumn("Value", typeof(string)));
                dt.Columns.Add(new DataColumn("Remarks", typeof(string)));
                dt.Columns.Add(new DataColumn("Itemcode", typeof(string)));
                dt.Columns.Add(new DataColumn("ReturnQuantity", typeof(string)));
                dt.Columns.Add(new DataColumn("SGSTCode", typeof(string)));
                dt.Columns.Add(new DataColumn("SGSTPer", typeof(string)));
                dt.Columns.Add(new DataColumn("SGSTAmt", typeof(string)));
                dt.Columns.Add(new DataColumn("CGSTCode", typeof(string)));
                dt.Columns.Add(new DataColumn("CGSTPer", typeof(string)));
                dt.Columns.Add(new DataColumn("CGSTAmt", typeof(string)));
                dt.Columns.Add(new DataColumn("IGSTCode", typeof(string)));
                dt.Columns.Add(new DataColumn("IGSTPer", typeof(string)));
                dt.Columns.Add(new DataColumn("IGSTAmt", typeof(string)));
                dt.Columns.Add(new DataColumn("UTGSTCode", typeof(string)));
                dt.Columns.Add(new DataColumn("UTGSTPer", typeof(string)));
                dt.Columns.Add(new DataColumn("UTGSTAmt", typeof(string)));
                dt.Columns.Add(new DataColumn("SuppPartNumber", typeof(string)));

                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["ChartOfAccount"] = string.Empty;
                dr["Value"] = string.Empty;
                dr["Remarks"] = string.Empty;
                dr["Itemcode"] = string.Empty;
                dr["ReturnQuantity"] = "0";
                dr["SGSTCode"] = string.Empty;
                dr["SGSTPer"] = "0";
                dr["SGSTAmt"] = "0";
                dr["CGSTCode"] = string.Empty;
                dr["CGSTPer"] = "0";
                dr["CGSTAmt"] = "0";
                dr["IGSTCode"] = string.Empty;
                dr["IGSTPer"] = "0";
                dr["IGSTAmt"] = "0";
                dr["UTGSTCode"] = string.Empty;
                dr["UTGSTPer"] = "0";
                dr["UTGSTAmt"] = "0";
                dr["SuppPartNumber"] = string.Empty;
                dt.Rows.Add(dr);
                //dr = dt.NewRow();

                ViewState["CurrentTable"] = dt;
                grdCA.DataSource = dt;
                grdCA.DataBind();

                if (grdCA.Rows.Count > 0)
                {
                    if (ddlDebitCreditNote.SelectedValue == "DA")
                        grdCA.HeaderRow.Cells[5].Text = "Quantity";
                    else
                        grdCA.HeaderRow.Cells[5].Text = "Return Quantity";

                    if (Session["BranchCode"].ToString() == "CHG")
                    {
                        grdCA.Columns[6].Visible = false;
                        grdCA.Columns[7].Visible = false;
                        grdCA.Columns[8].Visible = false;
                        grdCA.Columns[9].Visible = true;
                        grdCA.Columns[10].Visible = true;
                        grdCA.Columns[11].Visible = true;
                    }
                    else
                    {
                        grdCA.Columns[6].Visible = true;
                        grdCA.Columns[7].Visible = true;
                        grdCA.Columns[8].Visible = true;
                        grdCA.Columns[9].Visible = false;
                        grdCA.Columns[10].Visible = false;
                        grdCA.Columns[11].Visible = false;
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
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
                            //extract the TextBox values
                            TextBox txtGrdChartAccount = (TextBox)grdCA.Rows[rowIndex].Cells[1].FindControl("txtGrdChartAccount");
                            TextBox txtGrdValue = (TextBox)grdCA.Rows[rowIndex].Cells[2].FindControl("txtGrdValue");
                            TextBox txtGrdRemarks = (TextBox)grdCA.Rows[rowIndex].Cells[3].FindControl("txtGrdRemarks");
                            TextBox txtItemCode = (TextBox)grdCA.Rows[rowIndex].Cells[4].FindControl("txtItemCode");
                            TextBox txtReturnQuantity = (TextBox)grdCA.Rows[rowIndex].Cells[5].FindControl("txtReturnQuantity");
                            DropDownList ddlSGSTCode = (DropDownList)grdCA.Rows[rowIndex].Cells[6].FindControl("ddlSGSTCode");
                            TextBox txtSGSTPer = (TextBox)grdCA.Rows[rowIndex].Cells[7].FindControl("txtSGSTPer");
                            TextBox txtSGSTAmt = (TextBox)grdCA.Rows[rowIndex].Cells[8].FindControl("txtSGSTAmt");
                            DropDownList ddlUTGSTCode = (DropDownList)grdCA.Rows[rowIndex].Cells[9].FindControl("ddlUTGSTCode");
                            TextBox txtUTGSTPer = (TextBox)grdCA.Rows[rowIndex].Cells[10].FindControl("txtUTGSTPer");
                            TextBox txtUTGSTAmt = (TextBox)grdCA.Rows[rowIndex].Cells[11].FindControl("txtUTGSTAmt");
                            DropDownList ddlCGSTCode = (DropDownList)grdCA.Rows[rowIndex].Cells[12].FindControl("ddlCGSTCode");
                            TextBox txtCGSTPer = (TextBox)grdCA.Rows[rowIndex].Cells[13].FindControl("txtCGSTPer");
                            TextBox txtCGSTAmt = (TextBox)grdCA.Rows[rowIndex].Cells[14].FindControl("txtCGSTAmt");
                            DropDownList ddlIGSTCode = (DropDownList)grdCA.Rows[rowIndex].Cells[15].FindControl("ddlIGSTCode");
                            TextBox txtIGSTPer = (TextBox)grdCA.Rows[rowIndex].Cells[16].FindControl("txtIGSTPer");
                            TextBox txtIGSTAmt = (TextBox)grdCA.Rows[rowIndex].Cells[17].FindControl("txtIGSTAmt");
                            TextBox txtPartNumber = (TextBox)grdCA.Rows[rowIndex].Cells[18].FindControl("txtPartNumber");

                            dtCurrentTable.Rows[i - 1]["ChartOfAccount"] = txtGrdChartAccount.Text;
                            dtCurrentTable.Rows[i - 1]["Value"] = txtGrdValue.Text;
                            dtCurrentTable.Rows[i - 1]["Remarks"] = txtGrdRemarks.Text;
                            dtCurrentTable.Rows[i - 1]["Itemcode"] = txtItemCode.Text;
                            dtCurrentTable.Rows[i - 1]["ReturnQuantity"] = txtReturnQuantity.Text;
                            dtCurrentTable.Rows[i - 1]["SGSTCode"] = ddlSGSTCode.SelectedValue;
                            dtCurrentTable.Rows[i - 1]["SGSTPer"] = txtSGSTPer.Text;
                            dtCurrentTable.Rows[i - 1]["SGSTAmt"] = txtSGSTAmt.Text;
                            dtCurrentTable.Rows[i - 1]["CGSTCode"] = ddlCGSTCode.SelectedValue;
                            dtCurrentTable.Rows[i - 1]["CGSTPer"] = txtCGSTPer.Text;
                            dtCurrentTable.Rows[i - 1]["CGSTAmt"] = txtCGSTAmt.Text;
                            dtCurrentTable.Rows[i - 1]["IGSTCode"] = ddlIGSTCode.SelectedValue;
                            dtCurrentTable.Rows[i - 1]["IGSTPer"] = txtIGSTPer.Text;
                            dtCurrentTable.Rows[i - 1]["IGSTAmt"] = txtIGSTAmt.Text;
                            dtCurrentTable.Rows[i - 1]["UTGSTCode"] = ddlUTGSTCode.SelectedValue;
                            dtCurrentTable.Rows[i - 1]["UTGSTPer"] = txtUTGSTPer.Text;
                            dtCurrentTable.Rows[i - 1]["UTGSTAmt"] = txtUTGSTAmt.Text;
                            dtCurrentTable.Rows[i - 1]["SuppPartNumber"] = txtPartNumber.Text;
                            RowNumber = i + 1;
                            rowIndex++;
                        }
                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow = GetInitialRow(RowNumber, drCurrentRow);
                        dtCurrentTable.Rows.Add(drCurrentRow);
                        ViewState["CurrentTable"] = dtCurrentTable;
                        grdCA.DataSource = dtCurrentTable;
                        grdCA.DataBind();
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

        private DataRow GetInitialRow(int RowNumber, DataRow dr)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                dr["RowNumber"] = RowNumber;
                dr["ChartOfAccount"] = string.Empty;
                dr["Value"] = string.Empty;
                dr["Remarks"] = string.Empty;
                dr["Itemcode"] = string.Empty;
                dr["ReturnQuantity"] = string.Empty;
                dr["SGSTCode"] = string.Empty;
                dr["SGSTPer"] = "0";
                dr["SGSTAmt"] = "0";
                dr["CGSTCode"] = string.Empty;
                dr["CGSTPer"] = "0";
                dr["CGSTAmt"] = "0";
                dr["IGSTCode"] = string.Empty;
                dr["IGSTPer"] = "0";
                dr["IGSTAmt"] = "0";
                dr["UTGSTCode"] = string.Empty;
                dr["UTGSTPer"] = "0";
                dr["UTGSTAmt"] = "0";
                dr["SuppPartNumber"] = string.Empty;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return dr;
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
                            ChartAccount UserChartAccount = (ChartAccount)grdCA.Rows[rowIndex].Cells[1].FindControl("UserChartAccount");
                            TextBox txtGrdChartAccount = (TextBox)grdCA.Rows[rowIndex].Cells[1].FindControl("txtGrdChartAccount");
                            TextBox txtGrdValue = (TextBox)grdCA.Rows[rowIndex].Cells[2].FindControl("txtGrdValue");
                            TextBox txtGrdRemarks = (TextBox)grdCA.Rows[rowIndex].Cells[3].FindControl("txtGrdRemarks");
                            TextBox txtItemCode = (TextBox)grdCA.Rows[rowIndex].Cells[4].FindControl("txtItemCode");
                            ItemCodePartNumber UserPartNumber = (ItemCodePartNumber)grdCA.Rows[rowIndex].Cells[4].FindControl("UserPartNumber");
                            TextBox txtReturnQuantity = (TextBox)grdCA.Rows[rowIndex].Cells[5].FindControl("txtReturnQuantity");
                            DropDownList ddlSGSTCode = (DropDownList)grdCA.Rows[rowIndex].Cells[6].FindControl("ddlSGSTCode");
                            TextBox txtSGSTPer = (TextBox)grdCA.Rows[rowIndex].Cells[7].FindControl("txtSGSTPer");
                            TextBox txtSGSTAmt = (TextBox)grdCA.Rows[rowIndex].Cells[8].FindControl("txtSGSTAmt");
                            DropDownList ddlUTGSTCode = (DropDownList)grdCA.Rows[rowIndex].Cells[9].FindControl("ddlUTGSTCode");
                            TextBox txtUTGSTPer = (TextBox)grdCA.Rows[rowIndex].Cells[10].FindControl("txtUTGSTPer");
                            TextBox txtUTGSTAmt = (TextBox)grdCA.Rows[rowIndex].Cells[11].FindControl("txtUTGSTAmt");
                            DropDownList ddlCGSTCode = (DropDownList)grdCA.Rows[rowIndex].Cells[12].FindControl("ddlCGSTCode");
                            TextBox txtCGSTPer = (TextBox)grdCA.Rows[rowIndex].Cells[13].FindControl("txtCGSTPer");
                            TextBox txtCGSTAmt = (TextBox)grdCA.Rows[rowIndex].Cells[14].FindControl("txtCGSTAmt");
                            DropDownList ddlIGSTCode = (DropDownList)grdCA.Rows[rowIndex].Cells[15].FindControl("ddlIGSTCode");
                            TextBox txtIGSTPer = (TextBox)grdCA.Rows[rowIndex].Cells[16].FindControl("txtIGSTPer");
                            TextBox txtIGSTAmt = (TextBox)grdCA.Rows[rowIndex].Cells[17].FindControl("txtIGSTAmt");
                            TextBox txtPartNumber = (TextBox)grdCA.Rows[rowIndex].Cells[18].FindControl("txtPartNumber");

                            txtGrdChartAccount.Text = dt1.Rows[i]["ChartOfAccount"].ToString();
                            txtGrdValue.Text = dt1.Rows[i]["Value"].ToString();
                            txtGrdRemarks.Text = dt1.Rows[i]["Remarks"].ToString();
                            txtItemCode.Text = dt1.Rows[i]["Itemcode"].ToString();
                            txtReturnQuantity.Text = dt1.Rows[i]["ReturnQuantity"].ToString();
                            ddlSGSTCode.SelectedValue = dt1.Rows[i]["SGSTCode"].ToString();
                            txtSGSTPer.Text = dt1.Rows[i]["SGSTPer"].ToString();
                            txtSGSTAmt.Text = dt1.Rows[i]["SGSTAmt"].ToString();
                            ddlCGSTCode.SelectedValue = dt1.Rows[i]["CGSTCode"].ToString();
                            txtCGSTPer.Text = dt1.Rows[i]["CGSTPer"].ToString();
                            txtCGSTAmt.Text = dt1.Rows[i]["CGSTAmt"].ToString();
                            ddlIGSTCode.SelectedValue = dt1.Rows[i]["IGSTCode"].ToString();
                            txtIGSTPer.Text = dt1.Rows[i]["IGSTPer"].ToString();
                            txtIGSTAmt.Text = dt1.Rows[i]["IGSTAmt"].ToString();
                            ddlUTGSTCode.SelectedValue = dt1.Rows[i]["UTGSTCode"].ToString();
                            txtUTGSTPer.Text = dt1.Rows[i]["UTGSTPer"].ToString();
                            txtUTGSTAmt.Text = dt1.Rows[i]["UTGSTAmt"].ToString();
                            txtPartNumber.Text = dt1.Rows[i]["SuppPartNumber"].ToString();
                            rowIndex++;

                            if (i < dt1.Rows.Count - 1)
                            {
                                UserChartAccount.Visible = false;
                                txtGrdValue.Enabled = false;
                                txtReturnQuantity.Enabled = false;
                                UserPartNumber.Visible = false;
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

        protected void btnadd_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                AddNewRowToGrid();
                DisableReturnQnt();
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
                var txtGrdValue = (TextBox)gvr.FindControl("txtGrdValue");
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
                    txtSGSTPer.Text = TwoDecimalConversion(objDebitCredit.GetSalesTaxPer(ddlSGSTCode.SelectedItem.Text.Substring(0, 4)));
                    txtSGSTAmt.Text = TwoDecimalConversion(((Convert.ToDecimal(txtGrdValue.Text) * Convert.ToDecimal(txtSGSTPer.Text)) / 100).ToString());

                    //if (ddlSGSTCode.SelectedIndex == 1)
                    //{
                    //    ddlCGSTCode.SelectedIndex = 1;
                    //    ddlCGSTCode.Enabled = false;
                    //}
                    //else if (ddlSGSTCode.SelectedIndex == 2)
                    //{
                    //    ddlCGSTCode.SelectedIndex = 2;
                    //    ddlCGSTCode.Enabled = false;
                    //}

                    ddlCGSTCode.SelectedIndex = ddlSGSTCode.SelectedIndex;
                    ddlCGSTCode.Enabled = false;

                    txtCGSTPer.Text = TwoDecimalConversion(objDebitCredit.GetSalesTaxPer(ddlCGSTCode.SelectedItem.Text.Substring(0, 4)));
                    txtCGSTAmt.Text = TwoDecimalConversion(((Convert.ToDecimal(txtGrdValue.Text) * Convert.ToDecimal(txtCGSTPer.Text)) / 100).ToString());
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
                var txtGrdValue = (TextBox)gvr.FindControl("txtGrdValue");
                var ddlSGSTCode = (DropDownList)gvr.FindControl("ddlSGSTCode");
                var txtSGSTPer = (TextBox)gvr.FindControl("txtSGSTPer");
                var txtSGSTAmt = (TextBox)gvr.FindControl("txtSGSTAmt");
                var ddlCGSTCode = (DropDownList)gvr.FindControl("ddlCGSTCode");
                var txtCGSTPer = (TextBox)gvr.FindControl("txtCGSTPer");
                var txtCGSTAmt = (TextBox)gvr.FindControl("txtCGSTAmt");

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
                    txtCGSTPer.Text = TwoDecimalConversion(objDebitCredit.GetSalesTaxPer(ddlCGSTCode.SelectedItem.Text.Substring(0, 4)));
                    txtCGSTAmt.Text = TwoDecimalConversion(((Convert.ToDecimal(txtGrdValue.Text) * Convert.ToDecimal(txtCGSTPer.Text)) / 100).ToString());

                    //if (ddlCGSTCode.SelectedIndex == 1)
                    //{
                    //    ddlSGSTCode.SelectedIndex = 1;
                    //    ddlSGSTCode.Enabled = false;
                    //}
                    //else if (ddlCGSTCode.SelectedIndex == 2)
                    //{
                    //    ddlSGSTCode.SelectedIndex = 2;
                    //    ddlSGSTCode.Enabled = false;
                    //}

                    ddlSGSTCode.SelectedIndex = ddlCGSTCode.SelectedIndex;
                    ddlSGSTCode.Enabled = false;

                    txtSGSTPer.Text = TwoDecimalConversion(objDebitCredit.GetSalesTaxPer(ddlSGSTCode.SelectedItem.Text.Substring(0, 4)));
                    txtSGSTAmt.Text = TwoDecimalConversion(((Convert.ToDecimal(txtGrdValue.Text) * Convert.ToDecimal(txtSGSTPer.Text)) / 100).ToString());
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
                var txtGrdValue = (TextBox)gvr.FindControl("txtGrdValue");
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
                    txtIGSTPer.Text = TwoDecimalConversion(objDebitCredit.GetSalesTaxPer(ddlIGSTCode.SelectedItem.Text.Substring(0, 4)));
                    txtIGSTAmt.Text = TwoDecimalConversion(((Convert.ToDecimal(txtGrdValue.Text) * Convert.ToDecimal(txtIGSTPer.Text)) / 100).ToString());
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
                var txtGrdValue = (TextBox)gvr.FindControl("txtGrdValue");
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
                    txtUTGSTPer.Text = TwoDecimalConversion(objDebitCredit.GetSalesTaxPer(ddlUTGSTCode.SelectedItem.Text.Substring(0, 4)));
                    txtUTGSTAmt.Text = TwoDecimalConversion(((Convert.ToDecimal(txtGrdValue.Text) * Convert.ToDecimal(txtUTGSTPer.Text)) / 100).ToString());

                    //if (ddlUTGSTCode.SelectedIndex == 1)
                    //{
                    //    ddlCGSTCode.SelectedIndex = 1;
                    //    ddlCGSTCode.Enabled = false;
                    //}
                    //else if (ddlUTGSTCode.SelectedIndex == 2)
                    //{
                    //    ddlCGSTCode.SelectedIndex = 2;
                    //    ddlCGSTCode.Enabled = false;
                    //}

                    ddlCGSTCode.SelectedIndex = ddlUTGSTCode.SelectedIndex;
                    ddlCGSTCode.Enabled = false;

                    txtCGSTPer.Text = TwoDecimalConversion(objDebitCredit.GetSalesTaxPer(ddlCGSTCode.SelectedItem.Text.Substring(0, 4)));
                    txtCGSTAmt.Text = TwoDecimalConversion(((Convert.ToDecimal(txtGrdValue.Text) * Convert.ToDecimal(txtCGSTPer.Text)) / 100).ToString());
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

        protected void grdCA_RowCreated(object sender, GridViewRowEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
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

                    var txtPartNumber = (TextBox)e.Row.FindControl("txtPartNumber");
                    var txtReturnQty = (TextBox)e.Row.FindControl("txtReturnquantity");
                    LoadDropDownLists<SalesTaxCode>(objDebitCredit.GetSalesTaxCodeSGST(), ddlSGSTCode, "Sales_Tax_Percentage", "sales_tax_code", true, "");
                    LoadDropDownLists<SalesTaxCode>(objDebitCredit.GetSalesTaxCodeCGST(), ddlCGSTCode, "Sales_Tax_Percentage", "sales_tax_code", true, "");
                    LoadDropDownLists<SalesTaxCode>(objDebitCredit.GetSalesTaxCodeIGST(), ddlIGSTCode, "Sales_Tax_Percentage", "sales_tax_code", true, "");
                    LoadDropDownLists<SalesTaxCode>(objDebitCredit.GetSalesTaxCodeUTGST(), ddlUTGSTCode, "Sales_Tax_Percentage", "sales_tax_code", true, "");
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
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ucSupplierPartNumber_SearchImageClicked(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                GridViewRow gvr = (GridViewRow)((ItemCodePartNumber)sender).Parent.Parent;
                var txtItemCode = (TextBox)gvr.FindControl("txtItemCode");
                var txtPartNumber = (TextBox)gvr.FindControl("txtPartNumber");
                txtItemCode.Text = Session["ItemCode"].ToString();
                txtPartNumber.Text = Session["SupplierPartNumber"].ToString();
                txtItemCode.Focus();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private string FormatString(string grdValue)
        {
            string result = string.Empty;
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                result = String.Format("{0:0.00}", string.IsNullOrEmpty(grdValue) ? 0.00 : Convert.ToDouble(grdValue));
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return result;
        }

        private string FourDecimalConversion(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
                return "0.0000";
            else
                return string.Format("{0:0.0000}", Convert.ToDecimal(strValue));
        }

        protected void grdDA_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    var txtQuantity = (TextBox)e.Row.FindControl("txtQuantity");
                    var txtGrdValue = (TextBox)e.Row.FindControl("txtGrdValue");
                    var txtReturnquantity = (TextBox)e.Row.FindControl("txtReturnquantity");
                    var txtReturnvalue = (TextBox)e.Row.FindControl("txtReturnvalue");
                    var txtSGST = (TextBox)e.Row.FindControl("txtSGST");
                    var txtCGST = (TextBox)e.Row.FindControl("txtCGST");
                    var txtIGST = (TextBox)e.Row.FindControl("txtIGST");
                    var txtUTGST = (TextBox)e.Row.FindControl("txtUTGST");

                    txtGrdValue.Text = FourDecimalConversion(txtGrdValue.Text);
                    txtReturnvalue.Text = FourDecimalConversion(txtReturnvalue.Text);
                    txtSGST.Text = FourDecimalConversion(txtSGST.Text);
                    txtCGST.Text = FourDecimalConversion(txtCGST.Text);
                    txtIGST.Text = FourDecimalConversion(txtIGST.Text);
                    txtUTGST.Text = FourDecimalConversion(txtUTGST.Text);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void grdview_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Cells[4].Text = FormatString(e.Row.Cells[4].Text);
                    e.Row.Cells[7].Text = FormatString(e.Row.Cells[7].Text);
                    e.Row.Cells[8].Text = FormatString(e.Row.Cells[8].Text);
                    e.Row.Cells[9].Text = FormatString(e.Row.Cells[9].Text);
                    e.Row.Cells[10].Text = FormatString(e.Row.Cells[10].Text);
                    e.Row.Cells[11].Text = FormatString(e.Row.Cells[11].Text);
                    e.Row.Cells[12].Text = FormatString(e.Row.Cells[12].Text);
                    e.Row.Cells[13].Text = FormatString(e.Row.Cells[13].Text);
                    e.Row.Cells[14].Text = FormatString(e.Row.Cells[14].Text);
                    e.Row.Cells[15].Text = FormatString(e.Row.Cells[15].Text);
                    e.Row.Cells[16].Text = FormatString(e.Row.Cells[16].Text);
                    e.Row.Cells[17].Text = FormatString(e.Row.Cells[17].Text);
                    e.Row.Cells[18].Text = FormatString(e.Row.Cells[18].Text);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}

