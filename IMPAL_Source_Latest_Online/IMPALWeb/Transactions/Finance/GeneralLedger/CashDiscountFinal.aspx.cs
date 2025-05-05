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
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Web.Services;

namespace IMPALWeb.Transactions.Finance.GeneralLedger
{
    public partial class CashDiscountFinal : System.Web.UI.Page
    {
        ReceivableInvoice objReceivableInvoice = new ReceivableInvoice();
        DebitCredit objDebitCredit = new DebitCredit();
        CashDiscountDts objCashDiscount = new CashDiscountDts();
        ImpalLibrary objImpalLibrary = new ImpalLibrary();

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(CashDiscount), exp);
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

                    if (cryCashDiscountReprint != null)
                    {
                        cryCashDiscountReprint.Dispose();
                        cryCashDiscountReprint = null;
                    }

                    LoadCashDiscount();
                    hdnpath.Value = Page.ResolveClientUrl("~/HandlerFile");
                    BtnSubmit.Attributes.Add("OnClick", "return fnSubmitcheck('M');");
                    BtnReject.Attributes.Add("OnClick", "return funCdReject();");
                    ddlDocumentNumber.Items.Clear();
                    ddlDocumentNumber.Enabled = false;
                    imgButtonQuery.Visible = true;
                    BtnSubmit.Enabled = false;
                    BtnReject.Enabled = false;
                    btnReport.Enabled = false;
                    btnReport.Visible = false;
                    pnlHeader.Enabled = true;
                }
            }
            catch (Exception exception)
            {
                Log.WriteException(Source, exception);
            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (cryCashDiscountReprint != null)
            {
                cryCashDiscountReprint.Dispose();
                cryCashDiscountReprint = null;
            }
        }
        protected void cryCashDiscountReprint_Unload(object sender, EventArgs e)
        {
            if (cryCashDiscountReprint != null)
            {
                cryCashDiscountReprint.Dispose();
                cryCashDiscountReprint = null;
            }
        }

        private void LoadCashDiscount()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                LoadAccountintPeriod();

                txtDocumentDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtBranchCode.Text = (string)Session["BranchName"];
                LoadDropDownLists<IMPALLibrary.Common.DropDownListValue>(objImpalLibrary.GetDropDownListValues("CustomerIndication"), ddlCustomerInd, "DisplayValue", "DisplayText", true, "");
                LoadDropDownLists<CustomerDtls>(objDebitCredit.GetCustomer(Session["BranchCode"].ToString()), ddlSuppCust, "Code", "Name", true, "");
            }
            catch (Exception exception)
            {
                Log.WriteException(Source, exception);
            }
        }

        private void LoadAccountintPeriod()
        {
            AccountingPeriods Acc = new AccountingPeriods();
            List<string> lstAccountingPeriod = new List<string>();

            int PrevFinYearStatus = objReceivableInvoice.GetPreviousAccountingPeriodStatus(Session["UserID"].ToString(), "CDApproval");

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

            LoadhdnStartEndDates(ddlAccountPeriod.SelectedValue);
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
            catch (Exception exception)
            {
                Log.WriteException(Source, exception);
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
            catch (Exception exception)
            {
                Log.WriteException(Source, exception);
            }
        }

        private void ResetCashDiscount()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Server.ClearError();
                Response.Redirect("CashDiscountFinal.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exception)
            {
                Log.WriteException(Source, exception);
            }
        }

        private void Alert(string Message)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Message + "');", true);
            }
            catch (Exception exception)
            {
                Log.WriteException(Source, exception);
            }
        }

        protected void imgButtonQuery_Click(object sender, ImageClickEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                imgButtonQuery.Visible = false;
                ddlDocumentNumber.Enabled = true;
                IMPALLibrary.Masters.Finance documentNumber = new IMPALLibrary.Masters.Finance();
                ddlDocumentNumber.DataSource = documentNumber.GetCashDiscountNumberForApproval(Session["BranchCode"].ToString());
                ddlDocumentNumber.DataBind();
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
                    CashDiscountEntity cdDiscountEntity = objCashDiscount.GetCDDiscountDetailsCustomer(Session["BranchCode"].ToString(), ddlDocumentNumber.SelectedValue);

                    txtBranchCode.Text = cdDiscountEntity.Branch_Code;
                    ddlSuppCust.SelectedValue = cdDiscountEntity.Customer_Code;
                    ddlAccountPeriod.SelectedValue = cdDiscountEntity.AccPeriodCode.ToString();
                    txtDocumentDate.Text = cdDiscountEntity.DocumentDate;
                    txtValue.Text = cdDiscountEntity.Value;
                    ddlCustomerInd.SelectedValue = cdDiscountEntity.Indicator;

                    ddlSuppCust_SelectedIndexChanged(ddlSuppCust, EventArgs.Empty);

                    if (cdDiscountEntity.Items.Count > 0)
                    {
                        grdCD.DataSource = cdDiscountEntity.Items;
                        grdCD.DataBind();

                        foreach (GridViewRow gr in grdCD.Rows)
                        {
                            CheckBox chkSelected = (CheckBox)gr.FindControl("chkSelected");
                            HtmlSelect drpCdPer = (HtmlSelect)gr.FindControl("drpCdPer");
                            chkSelected.Checked = true;
                            drpCdPer.Attributes.Add("style", "display:none");
                        }
                    }

                    UpdpanelTop.Update();

                    BtnSubmit.Enabled = true;
                    BtnReject.Enabled = true;
                }
                else
                {
                    BtnSubmit.Enabled = false;
                    BtnReject.Enabled = false;
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

            ddlAccountPeriod.Enabled = false;

            try
            {
                LoadhdnStartEndDates(ddlAccountPeriod.SelectedValue);
            }
            catch (Exception exception)
            {
                Log.WriteException(Source, exception);
            }
        }

        private void LoadhdnStartEndDates(string AccPeriod)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                DebitCredit objDebitCredit = new DebitCredit();

                if (ddlAccountPeriod.SelectedIndex == 0)
                    txtDocumentDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                else
                    txtDocumentDate.Text = objDebitCredit.GetDocumentDate(AccPeriod);

                List<CDAccStartEndDate> cdstartenddates = objCashDiscount.AccountingStartEnddate(AccPeriod);
                hdnStartdate.Value = cdstartenddates[0].Start_Date.ToString();
                hdnEnddate.Value = cdstartenddates[0].End_Date.ToString();
            }
            catch (Exception exception)
            {
                Log.WriteException(Source, exception);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ResetCashDiscount();
            }
            catch (Exception exception)
            {
                Log.WriteException(Source, exception);
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                BtnSubmit.Enabled = false;
                BtnReject.Enabled = false;
                SubmitCashDiscount();
            }
            catch (Exception exception)
            {
                Log.WriteException(Source, exception);
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
            catch (Exception exception)
            {
                Log.WriteException(Source, exception);
            }

            return result;
        }

        protected void grdview_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Cells[2].Text = FormatString(e.Row.Cells[2].Text);
                    e.Row.Cells[3].Text = FormatString(e.Row.Cells[3].Text);
                    e.Row.Cells[6].Text = FormatString(e.Row.Cells[6].Text);
                }
            }
            catch (Exception exception)
            {
                Log.WriteException(Source, exception);
            }
        }

        private void SubmitCashDiscount()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            string DocumentNumber = string.Empty;
            string DrCrIndicator = "CA";
            string Transaction_Type = "651";
            string Remarks = "CASH DISCOUNT";
            string ChartAccountCode = "21415808890000001" + Session["BranchCode"].ToString();

            CashDiscountEntity cashdiscountEntity = new CashDiscountEntity();
            cashdiscountEntity.Items = new List<CashDiscountItem>();

            cashdiscountEntity.AccPeriodCode = Convert.ToInt16(ddlAccountPeriod.SelectedValue);
            cashdiscountEntity.AccPeriodYear = Convert.ToInt16(ddlAccountPeriod.SelectedItem.Text.Substring(0, 4).ToString());
            cashdiscountEntity.Document_Number = ddlDocumentNumber.SelectedValue;
            cashdiscountEntity.Transaction_Type_Code = Transaction_Type;
            cashdiscountEntity.Dr_Cr_Indicator = DrCrIndicator;
            cashdiscountEntity.Document_Date = DateTime.ParseExact(txtDocumentDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture); //DateTime.Now.Date.ToString();
            cashdiscountEntity.Branch_Code = Session["BranchCode"].ToString();
            cashdiscountEntity.Customer_Code = ddlSuppCust.SelectedValue;
            cashdiscountEntity.Indicator = ddlCustomerInd.SelectedValue;
            cashdiscountEntity.ApprovalLevel = Session["UserName"].ToString() + "/" + Session["UserID"];

            CashDiscountItem cashdiscountItem = null;
            int SNo = 0;
            decimal AdjValue = 0;

            foreach (GridViewRow gr in grdCD.Rows)
            {
                CheckBox chkSelected = (CheckBox)gr.FindControl("chkSelected");

                if (!chkSelected.Checked)
                    continue;

                cashdiscountItem = new CashDiscountItem();
                
                HtmlInputHidden ChkStatus = (HtmlInputHidden)gr.FindControl("ChkStatus");
                TextBox txtInVoiceNo = (TextBox)gr.FindControl("txtInVoiceNo");
                TextBox txtInvoiceDate = (TextBox)gr.FindControl("txtInvoiceDate");
                TextBox txtOrderValue = (TextBox)gr.FindControl("txtOrderValue");
                TextBox txtAmtCollected = (TextBox)gr.FindControl("txtAmtCollected");
                HtmlSelect drpCdPer = (HtmlSelect)gr.FindControl("drpCdPer");
                TextBox txtCdPer = (TextBox)gr.FindControl("txtCdPer");
                TextBox txtNoOfDays = (TextBox)gr.FindControl("txtNoOfDays");
                TextBox txtCDValue = (TextBox)gr.FindControl("txtCDValue");
                HtmlInputHidden hdnCDValue = (HtmlInputHidden)gr.FindControl("hdnCDValue");
                TextBox txtDelayDays = (TextBox)gr.FindControl("txtDelayDays");

                cashdiscountItem.Reference_Document_Number = txtInVoiceNo.Text;
                cashdiscountItem.Document_Value = txtOrderValue.Text;
                cashdiscountItem.Collection_Amount = txtAmtCollected.Text;
                cashdiscountItem.Days = txtNoOfDays.Text;
                cashdiscountItem.CD_Per = txtCdPer.Text;
                cashdiscountItem.Delay_Days = txtDelayDays.Text;
                cashdiscountItem.Remarks = Remarks;                
                cashdiscountItem.Reference_Document_Date = txtInvoiceDate.Text;

                if (ChkStatus.Value == "1")
                    cashdiscountItem.Value = hdnCDValue.Value;
                else
                    cashdiscountItem.Value = txtCDValue.Text;

                AdjValue += Convert.ToDecimal(cashdiscountItem.Value);
                cashdiscountItem.Adjustment_Value = AdjValue.ToString();
                cashdiscountItem.Chart_Of_Account_Code = ChartAccountCode;
                cashdiscountItem.Cnt = SNo;
                cashdiscountEntity.Items.Add(cashdiscountItem);
                SNo += 1;
            }

            int result = objCashDiscount.AddNewCashDiscountFinal(ref cashdiscountEntity, AdjValue);

            if ((cashdiscountEntity.ErrorMsg == string.Empty) && (cashdiscountEntity.ErrorCode == "0"))
            {
                grdCD.DataSource = null;
                grdCD.DataBind();
                pnlHeader.Enabled = false;
                txtValue.Text = hdnValue.Value;
                
                DataSet CashCdValue = objCashDiscount.GetCashCdCustValueFinal(Session["BranchCode"].ToString(), cashdiscountEntity.Document_Number);
                if (CashCdValue.Tables[0].Rows.Count > 0)
                {
                    grdview.DataSource = CashCdValue.Tables[0];
                    grdview.DataBind();
                }

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Cash Discount details have been Approved and Saved successfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('" + cashdiscountEntity.ErrorMsg + "');", true);
            }
        }

        protected void BtnReject_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                BtnSubmit.Enabled = false;
                BtnReject.Enabled = false;
                ddlDocumentNumber.Enabled = false;

                //int SNo = 0;

                //foreach (GridViewRow gr in grdCD.Rows)
                //{
                //    CheckBox chkSelected = (CheckBox)gr.FindControl("chkSelected");

                //    if (!chkSelected.Checked)
                //        continue;
                //    SNo += 1;
                //}

                //if (SNo <= 0)
                //{
                    objCashDiscount.UpdCdEntry(Session["BranchCode"].ToString(), ddlDocumentNumber.SelectedValue, Session["CDRemarks"].ToString());
                //}

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Cash Discount Entry has been Rejected/Cancelled Sucessfully');", true);
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
                objp.Session["CDRemarks"] = objp.Session["UserName"] + "/" + objp.Session["UserID"] + " - " + Remarks;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ddlSuppCust_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (ddlSuppCust.SelectedIndex > 0)
                {
                    Customer CustomerDts = objReceivableInvoice.GetCustomerInfoByCustomerCode(ddlSuppCust.SelectedValue, Session["BranchCode"].ToString());
                    List<Customer> CustomerList = new List<Customer>();
                    CustomerList.Add(CustomerDts);
                    FormDetailCustomer.DataSource = CustomerList;
                    FormDetailCustomer.DataBind();
                    ddlSuppCust.Focus();
                }
            }
            catch (Exception exception)
            {
                Log.WriteException(Source, exception);
            }
        }

        protected void grdCD_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    var txtOrderValue = (TextBox)e.Row.FindControl("txtOrderValue");
                    var txtAmtCollected = (TextBox)e.Row.FindControl("txtAmtCollected");
                    txtOrderValue.Text = FormatString(txtOrderValue.Text);
                    txtAmtCollected.Text = FormatString(txtAmtCollected.Text);
                }
            }
            catch (Exception exception)
            {
                Log.WriteException(Source, exception);
            }
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            UpdpanelTop.Visible = false;
            btnBack.Visible = true;

            string strSelectionFormula = default(string);
            strSelectionFormula = "";
            string strReportName = "CashDiscount";
            string documentNumber = "";

            documentNumber = ddlDocumentNumber.SelectedValue;

            strSelectionFormula = "{Cash_Discount_Cust.Branch_Code}='" + Session["BranchCode"].ToString() + "'";

            strSelectionFormula = strSelectionFormula + " and {Cash_Discount_Cust.CD_CN_Number}='" + documentNumber + "'";

            cryCashDiscountReprint.ReportName = strReportName;
            cryCashDiscountReprint.RecordSelectionFormula = strSelectionFormula;
            cryCashDiscountReprint.GenerateReportAndExportA4();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Server.ClearError();
                Response.Redirect("CashDiscountFinal.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}
