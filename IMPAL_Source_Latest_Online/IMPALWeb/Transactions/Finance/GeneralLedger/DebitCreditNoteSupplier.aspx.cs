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

namespace IMPALWeb.Transactions.Finance.General_Ledger
{
    public partial class DebitCreditNoteSupplier : System.Web.UI.Page
    {
        ReceivableInvoice objReceivableInvoice = new ReceivableInvoice();
        DebitCredit objDebitCredit = new DebitCredit();
        ImpalLibrary objImpalLibrary = new ImpalLibrary();
        Suppliers objSupplier = new Suppliers();
        Branches objBranches = new Branches();
        public List<CreditSalesTaxDetailsGST> CATaxDetailsGST = new List<CreditSalesTaxDetailsGST>();
        DebitCreditNoteGST Note = new DebitCreditNoteGST();
        List<DebitCreditAdviceFieldsGST> DebitCreditFieldNoteGST = new List<DebitCreditAdviceFieldsGST>();
        List<DebitCreditNoteDetailsGST> DebitCreditFieldDetailsGST = new List<DebitCreditNoteDetailsGST>();
        EinvAuthGen einvGen = new EinvAuthGen();

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(DebitCreditNoteSupplier), exp);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!IsPostBack)
                {
                    txtBranchCode.Text = (string)Session["BranchName"];
                    LoadAccountintPeriod();
                    LoadDropDownLists<IMPALLibrary.Common.DropDownListValue>(objImpalLibrary.GetDropDownListValues("SupplierNote"), ddlDebitCreditNote, "DisplayValue", "DisplayText", true, "");

                    hdnScreenMode.Value = PageMode.Insert;
                    SetEnable();
                    hdnpath.Value = Page.ResolveClientUrl("~/HandlerFile");
                    hdnOsLsIndicator.Value = "";

                    DateItem PrevDateStatus = objReceivableInvoice.GetPreviousDateStatus(Session["UserID"].ToString(), "DrCrSupplier");

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
                LoadDropDownLists<IMPALLibrary.Supplier>(objSupplier.GetSuppliercodewithOutDefault(), ddlSupplierName, "SupplierCode", "SupplierName", true, "");                
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void LoadDebitCreditView()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                LoadDropDownLists<IMPALLibrary.Supplier>(objSupplier.GetSuppliercodewithOutDefault(), ddlSupplierName, "SupplierCode", "SupplierName", true, "");
                LoadDropDownLists<TransactionType>(objDebitCredit.GetTransactionTypeView(Session["BranchCode"].ToString(), ddlDebitCreditNote.SelectedValue), ddlTransactionType, "Transaction_Type_code", "Transaction_Type_Description", true, "");
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

            int PrevFinYearStatus = objReceivableInvoice.GetPreviousAccountingPeriodStatus(Session["UserID"].ToString(), "DrCrSupplier");

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
                    btnDetails.Visible = true;
                    UpdPanelGrid.Visible = false;
                    UpdatePanel1.Visible = false;
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
                    btnDetails.Visible = false;
                    UpdPanelGrid.Visible = true;
                    UpdatePanel1.Visible = false;
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
                string ChartOfAccount = Session["ChatAccCode"].ToString();
                txtGrdChartAccount.Text = ChartOfAccount;

                if (Convert.ToDecimal(txtGSTValue.Text) <= 0)
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
                else
                {
                    if (ChartOfAccount.Substring(6, 4) == "0320" || ChartOfAccount.Substring(6, 4) == "0367" || ChartOfAccount.Substring(6, 4) == "0068")
                    {
                        hdnChart.Value = "1";

                        if (Session["BranchCode"].ToString() == "CHG")
                        {
                            if (hdnOsLsIndicator.Value == "L")
                                ddlUTGSTCode.Enabled = true;
                            else
                                ddlUTGSTCode.Enabled = false;

                            txtUTGSTPer.Enabled = false;
                            txtUTGSTAmt.Enabled = false;
                        }
                        else
                        {
                            if (hdnOsLsIndicator.Value == "L")
                                ddlSGSTCode.Enabled = true;
                            else
                                ddlSGSTCode.Enabled = false;

                            txtSGSTPer.Enabled = false;
                            txtSGSTAmt.Enabled = false;
                        }

                        if (hdnOsLsIndicator.Value == "L")
                        {
                            ddlCGSTCode.Enabled = true;
                            ddlIGSTCode.Enabled = false;
                        }
                        else
                        {
                            ddlCGSTCode.Enabled = false;
                            ddlIGSTCode.Enabled = true;
                        }

                        txtCGSTPer.Enabled = false;
                        txtCGSTAmt.Enabled = false;
                        txtIGSTPer.Enabled = false;
                        txtIGSTAmt.Enabled = false;
                    }
                    else
                    {
                        hdnChart.Value = "0";
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


                        if (Session["BranchCode"].ToString() == "CHG")
                        {
                            if (hdnOsLsIndicator.Value == "L")
                                ddlUTGSTCode.Enabled = true;
                            else
                                ddlUTGSTCode.Enabled = false;

                            txtUTGSTPer.Enabled = false;
                            txtUTGSTAmt.Enabled = false;
                        }
                        else
                        {
                            if (hdnOsLsIndicator.Value == "L")
                                ddlSGSTCode.Enabled = true;
                            else
                                ddlSGSTCode.Enabled = false;

                            txtSGSTPer.Enabled = false;
                            txtSGSTAmt.Enabled = false;
                        }

                        if (hdnOsLsIndicator.Value == "L")
                        {
                            ddlCGSTCode.Enabled = true;
                            ddlIGSTCode.Enabled = false;
                        }
                        else
                        {
                            ddlCGSTCode.Enabled = false;
                            ddlIGSTCode.Enabled = true;
                        }

                        txtCGSTPer.Enabled = false;
                        txtCGSTAmt.Enabled = false;
                        txtIGSTPer.Enabled = false;
                        txtIGSTAmt.Enabled = false;
                    }

                    if (hdnOsLsIndicator.Value == "L")
                    {

                    }
                }

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
            try
            {
                BtnSubmit.Visible = false;
                string DocumentNumber = string.Empty;
                txtRefDocumnetDate.Enabled = true;
                if (hdnScreenMode.Value == PageMode.Insert)
                {
                    if (objDebitCredit.CheckDocumentNumber(txtDocumentNumber.Text, ddlAccountPeriod.SelectedValue, Session["BranchCode"].ToString(), hdnDebitCreditNote.Value) == 0)
                    {
                        if (hdnTransType.Value == "1")
                            DocumentNumber = AddDebitCreditNote(grdCA);

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

                DrCrNoteEntity.Document_Number = txtDocumentNumber.Text;
                DrCrNoteEntity.Document_Date = txtDocumentDate.Text;
                DrCrNoteEntity.Transaction_Type_Code = ddlTransactionType.SelectedValue;
                DrCrNoteEntity.Dr_Cr_Indicator = hdnDebitCreditNote.Value;
                DrCrNoteEntity.Branch_Code = Session["BranchCode"].ToString();
                DrCrNoteEntity.Supplier_Code = ddlSupplierName.SelectedValue;
                DrCrNoteEntity.Customer_Code = "";
                DrCrNoteEntity.Reference_Document_Number = txtReferenceDocNumber.Text;
                DrCrNoteEntity.Remarks = txtRemarks.Text;
                DrCrNoteEntity.Value = (Convert.ToDecimal(txtValue.Text) + Convert.ToDecimal(txtGSTValue.Text)).ToString(); //txtValue.Text;
                DrCrNoteEntity.GSTValue = txtGSTValue.Text;
                DrCrNoteEntity.Reference_Document_Date = txtRefDocumnetDate.Text;
                DrCrNoteEntity.tr_Branch_Code = ddlSupplyPlant.SelectedValue;
                DrCrNoteEntity.Indicator = txtSupplierInd.Text;
                DrCrNoteEntity.Roundoff = hdnRoundoff.Value;
                DrCrNoteEntity.DrCrSelectedInd = ddlDebitCreditNote.SelectedItem.Text;

                Decimal gstValue = 0;

                foreach (GridViewRow item in dataGrid.Rows)
                {
                    int Index = item.RowIndex;
                    var txtGrdChartAccount = (TextBox)item.FindControl("txtGrdChartAccount");
                    var txtGrdRemarks = (TextBox)item.FindControl("txtGrdRemarks");
                    var txtGrdAmount = (TextBox)item.FindControl("txtGrdValue");
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

                    DebitCreditNoteDetailsGST DrCrNoteDetails = new DebitCreditNoteDetailsGST();

                    DrCrNoteDetails.Amount = txtGrdAmount.Text;
                    DrCrNoteDetails.Remarks_Detail = txtGrdRemarks.Text;
                    DrCrNoteDetails.Item_Code = "";
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

                DocumentNumber = objDebitCredit.AddNewDebitCreditNoteSupplier(ref DrCrNoteEntity);

                if (Convert.ToDecimal(DrCrNoteEntity.GSTValue) > 0 || gstValue > 0)
                {
                    DataSet Datasetresult = objDebitCredit.GetEinvoicingDetailsDrCrSupplier(Session["BranchCode"].ToString(), DocumentNumber);

                    GenerateJSON objGenJsonData = new GenerateJSON();

                    string JSONData = JsonConvert.SerializeObject(objGenJsonData.GenerateInvoiceJSONData(Datasetresult, Session["BranchCode"].ToString()), Formatting.Indented);

                    einvGen.EinvoiceAuthentication(JSONData.Substring(3, JSONData.Length - 4), Session["BranchCode"].ToString(), DocumentNumber, "1", "GENIRN", Datasetresult.Tables[0].Rows[0]["Seller_GST"].ToString(), Datasetresult.Tables[0].Rows[0]["Doc_Type"].ToString(), Datasetresult.Tables[0].Rows[0]["Document_Date"].ToString().Replace("-", "/"), Datasetresult);

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Debit Credit Note Entry Has been Generated Successfully with QR Code.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Debit Credit Note Entry Has been Generated Successfully Without QR Code.');", true);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return DocumentNumber;
        }

        private string GetTaxValue(string value)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            string ReturnValue = "0";
            try
            {
                ReturnValue = (ddlTransactionType.SelectedValue == "658" || ddlTransactionType.SelectedValue == "758") ? ReturnValue : value;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return ReturnValue;
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
                Server.ClearError();
                Response.Redirect("DebitCreditNoteSupplier.aspx", false);
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
                Response.Redirect("DebitCreditNoteSupplier.aspx", false);
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

        struct TransactionCode
        {
            public const string SUPPLIERCOUPONCN = "761";
            public const string CSTOCTROIREIMBURSEDCN = "762";
            public const string INCENTIVEREBATECN = "763";
            public const string SCHEMEITEMCN = "764";
            public const string SUPPLIERCNTODEALERS = "765";
            public const string WARRANTYCLAIMCN = "766";
            public const string SF15INDUSTRAILITEMSCN = "767";
            public const string SF6TATAITEMSCN = "768";
            public const string SHORTSUPPLYRETOFITEMSERRINBILLCN = "769";
            public const string SUPPLIERCASHDISCOUNTCN = "770";
        }

        struct SupplierCustomerBranchIndicator
        {
            public const string Supplier = "Supplier";
            public const string Customer = "Customer";
            public const string Branch = "Branch";
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

        protected void imgButtonQuery_Click(object sender, ImageClickEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                LoadDropDownLists<DocumentNumber>(objDebitCredit.GetAllSupplierDocumentNumber(Session["BranchCode"].ToString()), ddlDocumentNumber, "Document_Number", "Document_Number", true, "");

                hdnScreenMode.Value = PageMode.Edit;
                SetEnable();
                LoadDebitCreditView();
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
                Note = objDebitCredit.ViewDebitCreditNoteGST(Session["BranchCode"].ToString(), DocumentNumber);
                if (Note != null)
                {
                    DebitCreditFieldNoteGST = Note.ListDebitCreditNoteGST;
                    DebitCreditFieldDetailsGST = Note.ListDebitCreditNoteDetailsGST;
                    if (DebitCreditFieldNoteGST.Count > 0)
                    {
                        txtDocumentNumber.Text = DebitCreditFieldNoteGST[0].Document_Number;
                        //ddlAccountPeriod.SelectedItem.Text = DebitCreditFieldNoteGST[0].Accountingperiod;
                        txtAccountPeriod.Text = DebitCreditFieldNoteGST[0].Accountingperiod;
                        txtDocumentDate.Text = DebitCreditFieldNoteGST[0].Document_Date;
                        txtBranchCode.Text = DebitCreditFieldNoteGST[0].Branch_Code;
                        //ddlDebitCreditNote.SelectedValue = DebitCreditFieldNoteGST[0].Dr_Cr_Indicator;
                        txtSupplierInd.Text = DebitCreditFieldNoteGST[0].Indicator;
                        ddlSupplierName.SelectedValue = DebitCreditFieldNoteGST[0].Indicator_Code;

                        ddlSupplyPlant.DataSource = (object)GetSupplyplants();
                        ddlSupplyPlant.DataTextField = "ItemDesc";
                        ddlSupplyPlant.DataValueField = "ItemCode";
                        ddlSupplyPlant.DataBind();
                        ddlSupplyPlant.Width = 250;

                        if (DebitCreditFieldNoteGST[0].tr_Branch_Code != "")
                            ddlSupplyPlant.SelectedValue = DebitCreditFieldNoteGST[0].tr_Branch_Code;

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

        protected void ddlTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                RemoveDropodownData(ddlAccountPeriod, ddlAccountPeriod.SelectedValue, ddlAccountPeriod.SelectedItem.ToString());
                RemoveDropodownData(ddlDebitCreditNote, ddlDebitCreditNote.SelectedValue, ddlDebitCreditNote.SelectedItem.ToString());
                RemoveDropodownData(ddlTransactionType, ddlTransactionType.SelectedValue, ddlTransactionType.SelectedItem.ToString());
                RemoveDropodownData(ddlSupplierName, ddlSupplierName.SelectedValue, ddlSupplierName.SelectedItem.ToString());
                RemoveDropodownData(ddlSupplyPlant, ddlSupplyPlant.SelectedValue, ddlSupplyPlant.SelectedItem.ToString());

                int GSTvalueStatus = objReceivableInvoice.GetSuppGSTValueStatus(Session["BranchCode"].ToString());

                if (GSTvalueStatus == 1 || (Session["UserID"].ToString() == "hoadmin1" || Session["UserID"].ToString() == "hoadmin2"))
                    txtGSTValue.Enabled = true;
                else
                {
                    txtGSTValue.Text = "0";
                    txtGSTValue.Enabled = false;
                }
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
                if (hdnScreenMode.Value == PageMode.Insert)
                {
                    if (ddlDebitCreditNote.SelectedValue == "PC")
                        hdnDebitCreditNote.Value = "DA";
                    else
                        hdnDebitCreditNote.Value = ddlDebitCreditNote.SelectedValue;

                    if (objDebitCredit.CheckDocumentNumber(txtDocumentNumber.Text, ddlAccountPeriod.SelectedValue, Session["BranchCode"].ToString(), hdnDebitCreditNote.Value) != 0)
                    {
                        Alert("Document Number Already Exists");
                        ResetDebitCreditAdvice();
                    }

                    LoadDebitCredit();

                    ddlAccountPeriod.Enabled = false;
                    ddlDebitCreditNote.Enabled = false;
                    imgButtonQuery.Enabled = false;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ddlSupplierName_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlSupplyPlant.DataSource = (object)GetSupplyplants();
                ddlSupplyPlant.DataTextField = "ItemDesc";
                ddlSupplyPlant.DataValueField = "ItemCode";
                ddlSupplyPlant.DataBind();

                if ((string)Session["RoleCode"] == "CORR")
                {
                    LoadDropDownLists<TransactionType>(objDebitCredit.GetTransactionTypeCOR(Session["BranchCode"].ToString(), hdnDebitCreditNote.Value), ddlTransactionType, "Transaction_Type_code", "Transaction_Type_Description", true, "");
                }
                else
                {
                    if (hdnDebitCreditNote.Value == "CA")
                        LoadDropDownLists<IMPALLibrary.Common.DropDownListValue>(objImpalLibrary.GetDropDownListValues("DebitCreditSupplierCAvsAutoTOD"), ddlTransactionType, "DisplayValue", "DisplayText", true, "");
                    else
                    {
                        if (ddlDebitCreditNote.SelectedIndex == 3)
                            LoadDropDownLists<IMPALLibrary.Common.DropDownListValue>(objImpalLibrary.GetDropDownListValues("DebitCreditBranchDAtransactionType"), ddlTransactionType, "DisplayValue", "DisplayText", true, "");
                        else if (objDebitCredit.CheckAutoTODstatus(Session["BranchCode"].ToString(), ddlSupplierName.SelectedValue) > 0)
                            LoadDropDownLists<IMPALLibrary.Common.DropDownListValue>(objImpalLibrary.GetDropDownListValues("DebitCreditSupplierDAvsAutoTOD"), ddlTransactionType, "DisplayValue", "DisplayText", true, "");
                        else
                            LoadDropDownLists<TransactionType>(objDebitCredit.GetTransactionType(Session["BranchCode"].ToString(), hdnDebitCreditNote.Value), ddlTransactionType, "Transaction_Type_code", "Transaction_Type_Description", true, "");
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(InwardEntry), exp);
            }
        }

        private List<IMPALLibrary.Transactions.Item> GetSupplyplants()
        {
            List<IMPALLibrary.Transactions.Item> obj = objDebitCredit.GetSupplierPlant(ddlSupplierName.SelectedValue.ToString());
            return obj;
        }

        protected void ddlSupplyPlant_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlSupplyPlant.SelectedIndex > 0)
                {
                    string OsLsIndicator = objDebitCredit.SupplyPlantInterStateStatus(ddlSupplierName.SelectedValue, ddlSupplyPlant.SelectedValue, Session["BranchCode"].ToString());
                    hdnOsLsIndicator.Value = OsLsIndicator;
                }
                else
                    hdnOsLsIndicator.Value = "";
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(InwardEntry), exp);
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

        protected void btnDetails_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                BtnSubmit.Enabled = true;
                btnDetails.Enabled = false;
                btnDetails.Visible = false;
                txtValue.Enabled = false;
                txtGSTValue.Enabled = false;
                txtReferenceDocNumber.Enabled = false;
                txtRefDocumnetDate.Enabled = false;

                UpdatePanel1.Visible = true;
                hdnTransType.Value = "1";
                SetInitialRow();
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
                dt.Columns.Add(new DataColumn("ChartOfAccount", typeof(string)));
                dt.Columns.Add(new DataColumn("Value", typeof(string)));
                dt.Columns.Add(new DataColumn("Remarks", typeof(string)));
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

                dr = dt.NewRow();
                dr["ChartOfAccount"] = string.Empty;
                dr["Value"] = string.Empty;
                dr["Remarks"] = string.Empty;
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
                dt.Rows.Add(dr);
                //dr = dt.NewRow();

                ViewState["CurrentTable"] = dt;
                grdCA.DataSource = dt;
                grdCA.DataBind();

                if (grdCA.Rows.Count > 0)
                {
                    if (Session["BranchCode"].ToString() == "CHG")
                    {
                        grdCA.Columns[4].Visible = false;
                        grdCA.Columns[5].Visible = false;
                        grdCA.Columns[6].Visible = false;
                        grdCA.Columns[7].Visible = true;
                        grdCA.Columns[8].Visible = true;
                        grdCA.Columns[9].Visible = true;
                    }
                    else
                    {
                        grdCA.Columns[4].Visible = true;
                        grdCA.Columns[5].Visible = true;
                        grdCA.Columns[6].Visible = true;
                        grdCA.Columns[7].Visible = false;
                        grdCA.Columns[8].Visible = false;
                        grdCA.Columns[9].Visible = false;
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
                            TextBox txtGrdChartAccount = (TextBox)grdCA.Rows[rowIndex].Cells[1].FindControl("txtGrdChartAccount");
                            TextBox txtGrdValue = (TextBox)grdCA.Rows[rowIndex].Cells[2].FindControl("txtGrdValue");
                            TextBox txtGrdRemarks = (TextBox)grdCA.Rows[rowIndex].Cells[3].FindControl("txtGrdRemarks");
                            DropDownList ddlSGSTCode = (DropDownList)grdCA.Rows[rowIndex].Cells[4].FindControl("ddlSGSTCode");
                            TextBox txtSGSTPer = (TextBox)grdCA.Rows[rowIndex].Cells[5].FindControl("txtSGSTPer");
                            TextBox txtSGSTAmt = (TextBox)grdCA.Rows[rowIndex].Cells[6].FindControl("txtSGSTAmt");
                            DropDownList ddlUTGSTCode = (DropDownList)grdCA.Rows[rowIndex].Cells[7].FindControl("ddlUTGSTCode");
                            TextBox txtUTGSTPer = (TextBox)grdCA.Rows[rowIndex].Cells[8].FindControl("txtUTGSTPer");
                            TextBox txtUTGSTAmt = (TextBox)grdCA.Rows[rowIndex].Cells[9].FindControl("txtUTGSTAmt");
                            DropDownList ddlCGSTCode = (DropDownList)grdCA.Rows[rowIndex].Cells[10].FindControl("ddlCGSTCode");
                            TextBox txtCGSTPer = (TextBox)grdCA.Rows[rowIndex].Cells[11].FindControl("txtCGSTPer");
                            TextBox txtCGSTAmt = (TextBox)grdCA.Rows[rowIndex].Cells[12].FindControl("txtCGSTAmt");
                            DropDownList ddlIGSTCode = (DropDownList)grdCA.Rows[rowIndex].Cells[13].FindControl("ddlIGSTCode");
                            TextBox txtIGSTPer = (TextBox)grdCA.Rows[rowIndex].Cells[14].FindControl("txtIGSTPer");
                            TextBox txtIGSTAmt = (TextBox)grdCA.Rows[rowIndex].Cells[15].FindControl("txtIGSTAmt");

                            dtCurrentTable.Rows[i - 1]["ChartOfAccount"] = txtGrdChartAccount.Text;
                            dtCurrentTable.Rows[i - 1]["Value"] = txtGrdValue.Text;
                            dtCurrentTable.Rows[i - 1]["Remarks"] = txtGrdRemarks.Text;
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
                dr["ChartOfAccount"] = string.Empty;
                dr["Value"] = string.Empty;
                dr["Remarks"] = string.Empty;
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
                            DropDownList ddlSGSTCode = (DropDownList)grdCA.Rows[rowIndex].Cells[4].FindControl("ddlSGSTCode");
                            TextBox txtSGSTPer = (TextBox)grdCA.Rows[rowIndex].Cells[5].FindControl("txtSGSTPer");
                            TextBox txtSGSTAmt = (TextBox)grdCA.Rows[rowIndex].Cells[6].FindControl("txtSGSTAmt");
                            DropDownList ddlUTGSTCode = (DropDownList)grdCA.Rows[rowIndex].Cells[7].FindControl("ddlUTGSTCode");
                            TextBox txtUTGSTPer = (TextBox)grdCA.Rows[rowIndex].Cells[8].FindControl("txtUTGSTPer");
                            TextBox txtUTGSTAmt = (TextBox)grdCA.Rows[rowIndex].Cells[9].FindControl("txtUTGSTAmt");
                            DropDownList ddlCGSTCode = (DropDownList)grdCA.Rows[rowIndex].Cells[10].FindControl("ddlCGSTCode");
                            TextBox txtCGSTPer = (TextBox)grdCA.Rows[rowIndex].Cells[11].FindControl("txtCGSTPer");
                            TextBox txtCGSTAmt = (TextBox)grdCA.Rows[rowIndex].Cells[12].FindControl("txtCGSTAmt");
                            DropDownList ddlIGSTCode = (DropDownList)grdCA.Rows[rowIndex].Cells[13].FindControl("ddlIGSTCode");
                            TextBox txtIGSTPer = (TextBox)grdCA.Rows[rowIndex].Cells[14].FindControl("txtIGSTPer");
                            TextBox txtIGSTAmt = (TextBox)grdCA.Rows[rowIndex].Cells[15].FindControl("txtIGSTAmt");

                            txtGrdChartAccount.Text = dt1.Rows[i]["ChartOfAccount"].ToString();
                            txtGrdValue.Text = dt1.Rows[i]["Value"].ToString();
                            txtGrdRemarks.Text = dt1.Rows[i]["Remarks"].ToString();
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
                            rowIndex++;

                            if (i < dt1.Rows.Count - 1)
                            {
                                UserChartAccount.Visible = false;
                                txtGrdValue.Enabled = false;
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

                    if (ddlSGSTCode.SelectedIndex == 1)
                    {
                        ddlCGSTCode.SelectedIndex = 1;
                        ddlCGSTCode.Enabled = false;
                    }
                    else if (ddlSGSTCode.SelectedIndex == 2)
                    {
                        ddlCGSTCode.SelectedIndex = 2;
                        ddlCGSTCode.Enabled = false;
                    }

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

                    if (ddlCGSTCode.SelectedIndex == 1)
                    {
                        ddlSGSTCode.SelectedIndex = 1;
                        ddlSGSTCode.Enabled = false;
                    }
                    else if (ddlCGSTCode.SelectedIndex == 2)
                    {
                        ddlSGSTCode.SelectedIndex = 2;
                        ddlSGSTCode.Enabled = false;
                    }

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

                    if (ddlUTGSTCode.SelectedIndex == 1)
                    {
                        ddlCGSTCode.SelectedIndex = 1;
                        ddlCGSTCode.Enabled = false;
                    }
                    else if (ddlUTGSTCode.SelectedIndex == 2)
                    {
                        ddlCGSTCode.SelectedIndex = 2;
                        ddlCGSTCode.Enabled = false;
                    }

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
                Log.WriteException(typeof(DebitCreditNoteSupplier), exp);
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

        protected void grdCA_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataTable dt = (DataTable)ViewState["CurrentTable"];
                    dt.Rows.Clear();
                    dt.AcceptChanges();
                    
                    if (grdCA.Rows.Count >= 1)
                    {
                        for (int i = 0; i < grdCA.Rows.Count; i++)
                        {
                            DataRow dr = dt.NewRow();

                            TextBox txtGrdChartAccount = (TextBox)grdCA.Rows[i].Cells[1].FindControl("txtGrdChartAccount");
                            TextBox txtGrdValue = (TextBox)grdCA.Rows[i].Cells[2].FindControl("txtGrdValue");
                            TextBox txtGrdRemarks = (TextBox)grdCA.Rows[i].Cells[3].FindControl("txtGrdRemarks");
                            DropDownList ddlSGSTCode = (DropDownList)grdCA.Rows[i].Cells[4].FindControl("ddlSGSTCode");
                            TextBox txtSGSTPer = (TextBox)grdCA.Rows[i].Cells[5].FindControl("txtSGSTPer");
                            TextBox txtSGSTAmt = (TextBox)grdCA.Rows[i].Cells[6].FindControl("txtSGSTAmt");
                            DropDownList ddlUTGSTCode = (DropDownList)grdCA.Rows[i].Cells[7].FindControl("ddlUTGSTCode");
                            TextBox txtUTGSTPer = (TextBox)grdCA.Rows[i].Cells[8].FindControl("txtUTGSTPer");
                            TextBox txtUTGSTAmt = (TextBox)grdCA.Rows[i].Cells[9].FindControl("txtUTGSTAmt");
                            DropDownList ddlCGSTCode = (DropDownList)grdCA.Rows[i].Cells[10].FindControl("ddlCGSTCode");
                            TextBox txtCGSTPer = (TextBox)grdCA.Rows[i].Cells[11].FindControl("txtCGSTPer");
                            TextBox txtCGSTAmt = (TextBox)grdCA.Rows[i].Cells[12].FindControl("txtCGSTAmt");
                            DropDownList ddlIGSTCode = (DropDownList)grdCA.Rows[i].Cells[13].FindControl("ddlIGSTCode");
                            TextBox txtIGSTPer = (TextBox)grdCA.Rows[i].Cells[14].FindControl("txtIGSTPer");
                            TextBox txtIGSTAmt = (TextBox)grdCA.Rows[i].Cells[15].FindControl("txtIGSTAmt");

                            dr["ChartOfAccount"] = txtGrdChartAccount.Text;
                            dr["Value"] = txtGrdValue.Text;
                            dr["Remarks"] = txtGrdRemarks.Text;
                            dr["SGSTCode"] = ddlSGSTCode.SelectedValue;
                            dr["SGSTPer"] = txtSGSTPer.Text;
                            dr["SGSTAmt"] = txtSGSTAmt.Text;
                            dr["CGSTCode"] = ddlCGSTCode.SelectedValue;
                            dr["CGSTPer"] = txtCGSTPer.Text;
                            dr["CGSTAmt"] = txtCGSTAmt.Text;
                            dr["IGSTCode"] = ddlIGSTCode.SelectedValue;
                            dr["IGSTPer"] = txtIGSTPer.Text;
                            dr["IGSTAmt"] = txtIGSTAmt.Text;
                            dr["UTGSTCode"] = ddlUTGSTCode.SelectedValue;
                            dr["UTGSTPer"] = txtUTGSTPer.Text;
                            dr["UTGSTAmt"] = txtUTGSTAmt.Text;
                            dt.Rows.Add(dr);
                        }

                        dt.Rows.RemoveAt(e.RowIndex);
                        grdCA.DataSource = dt;
                        grdCA.DataBind();

                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                ChartAccount UserChartAccount = (ChartAccount)grdCA.Rows[i].Cells[1].FindControl("UserChartAccount");
                                TextBox txtGrdChartAccount = (TextBox)grdCA.Rows[i].Cells[1].FindControl("txtGrdChartAccount");
                                TextBox txtGrdValue = (TextBox)grdCA.Rows[i].Cells[2].FindControl("txtGrdValue");
                                TextBox txtGrdRemarks = (TextBox)grdCA.Rows[i].Cells[3].FindControl("txtGrdRemarks");
                                DropDownList ddlSGSTCode = (DropDownList)grdCA.Rows[i].Cells[4].FindControl("ddlSGSTCode");
                                TextBox txtSGSTPer = (TextBox)grdCA.Rows[i].Cells[5].FindControl("txtSGSTPer");
                                TextBox txtSGSTAmt = (TextBox)grdCA.Rows[i].Cells[6].FindControl("txtSGSTAmt");
                                DropDownList ddlUTGSTCode = (DropDownList)grdCA.Rows[i].Cells[7].FindControl("ddlUTGSTCode");
                                TextBox txtUTGSTPer = (TextBox)grdCA.Rows[i].Cells[8].FindControl("txtUTGSTPer");
                                TextBox txtUTGSTAmt = (TextBox)grdCA.Rows[i].Cells[9].FindControl("txtUTGSTAmt");
                                DropDownList ddlCGSTCode = (DropDownList)grdCA.Rows[i].Cells[10].FindControl("ddlCGSTCode");
                                TextBox txtCGSTPer = (TextBox)grdCA.Rows[i].Cells[11].FindControl("txtCGSTPer");
                                TextBox txtCGSTAmt = (TextBox)grdCA.Rows[i].Cells[12].FindControl("txtCGSTAmt");
                                DropDownList ddlIGSTCode = (DropDownList)grdCA.Rows[i].Cells[13].FindControl("ddlIGSTCode");
                                TextBox txtIGSTPer = (TextBox)grdCA.Rows[i].Cells[14].FindControl("txtIGSTPer");
                                TextBox txtIGSTAmt = (TextBox)grdCA.Rows[i].Cells[15].FindControl("txtIGSTAmt");
                                
                                txtGrdChartAccount.Text = dt.Rows[i]["ChartOfAccount"].ToString();
                                txtGrdValue.Text = dt.Rows[i]["Value"].ToString();
                                txtGrdRemarks.Text = dt.Rows[i]["Remarks"].ToString();
                                ddlSGSTCode.SelectedValue = dt.Rows[i]["SGSTCode"].ToString();
                                txtSGSTPer.Text = dt.Rows[i]["SGSTPer"].ToString();
                                txtSGSTAmt.Text = dt.Rows[i]["SGSTAmt"].ToString();
                                ddlCGSTCode.SelectedValue = dt.Rows[i]["CGSTCode"].ToString();
                                txtCGSTPer.Text = dt.Rows[i]["CGSTPer"].ToString();
                                txtCGSTAmt.Text = dt.Rows[i]["CGSTAmt"].ToString();
                                ddlIGSTCode.SelectedValue = dt.Rows[i]["IGSTCode"].ToString();
                                txtIGSTPer.Text = dt.Rows[i]["IGSTPer"].ToString();
                                txtIGSTAmt.Text = dt.Rows[i]["IGSTAmt"].ToString();
                                ddlUTGSTCode.SelectedValue = dt.Rows[i]["UTGSTCode"].ToString();
                                txtUTGSTPer.Text = dt.Rows[i]["UTGSTPer"].ToString();
                                txtUTGSTAmt.Text = dt.Rows[i]["UTGSTAmt"].ToString();                                

                                if (i < dt.Rows.Count - 1)
                                {
                                    UserChartAccount.Visible = false;
                                    txtGrdValue.Enabled = false;
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
                        else
                        {
                            BtnSubmit.Enabled = false;
                            btnDetails.Enabled = true;
                            btnDetails.Visible = true;
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

        protected void grdview_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Cells[5].Text = FormatString(e.Row.Cells[5].Text);
                    e.Row.Cells[6].Text = FormatString(e.Row.Cells[6].Text);
                    e.Row.Cells[8].Text = FormatString(e.Row.Cells[8].Text);
                    e.Row.Cells[9].Text = FormatString(e.Row.Cells[9].Text);
                    e.Row.Cells[11].Text = FormatString(e.Row.Cells[11].Text);
                    e.Row.Cells[12].Text = FormatString(e.Row.Cells[12].Text);
                    e.Row.Cells[14].Text = FormatString(e.Row.Cells[14].Text);
                    e.Row.Cells[15].Text = FormatString(e.Row.Cells[15].Text);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}