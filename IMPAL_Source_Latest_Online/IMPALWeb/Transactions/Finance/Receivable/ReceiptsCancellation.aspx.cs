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
using System.Web.Services;

namespace IMPALWeb
{
    public partial class ReceiptsCancellation : System.Web.UI.Page
    {
        ReceiptEntity receiptEntity = new ReceiptEntity();
        ReceiptTransactions receiptTransactions = new ReceiptTransactions();

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ReceiptsCancellation), exp);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ddlBranch.SelectedIndex = 0;

                    SetDefaultValues();
                    ddlReceiptNumber.Visible = false;
                    ddlAccountingPeriod.Visible = true;
                    txtAccountPeriod.Visible = false;
                    FirstGridViewRow(string.Empty);
                    FreezeButtons(true);

                    BtnCancel.Enabled = true;

                    tblBalanceAmount.Visible = false;
                    ChkHeader.Visible = false;

                    txtChequeNumber.Enabled = false;
                    txtChequeDate.Enabled = false;
                    //ImgChequeDate.Enabled = false;
                    txtBank.Enabled = false;
                    txtBranch.Enabled = false;
                    ddlLocalOrOutStation.Enabled = false;
                    fnPopulateDropDown("ReceivableReceipt", ddlModeOfReceipt);
                }

                ddlAccountingPeriod.Enabled = false;
                ddlCustomer.Enabled = false;
                //ddlBranch.Enabled = false;
                txtAmount.Enabled = false;
                btnReset.Enabled = true;

                txtTempRecptNumber.Enabled = false;
                txtTempRecptDate.Enabled = false;
                txtChequeNumber.Enabled = false;
                txtChequeDate.Enabled = false;
                txtBank.Enabled = false;
                txtBranch.Enabled = false;
                ddlLocalOrOutStation.Enabled = false;
                ddlModeOfReceipt.Enabled = false;
                tblNEFTremarks.Attributes.Add("Style", "display:none");

                BtnCancel.Attributes.Add("OnClick", "return funFYRcvReceiptCancellationSubmit();");
                btnReset.Attributes.Add("OnClick", "return funFYRcvReceiptReset();");
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ReceiptsCancellation), exp);
            }
        }

        protected void ddlReceiptNumber_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlReceiptNumber.SelectedValue != "0")
                {
                    receiptEntity = receiptTransactions.GetReceiptsDetailsByNumber(ddlBranch.SelectedValue, ddlReceiptNumber.SelectedValue, "I");
                    grvItemDetails.DataSource = (object)receiptEntity.Items;
                }
                else
                {
                    grvItemDetails.DataSource = null;
                }

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

                txtChequeNumber.Text = receiptEntity.ChequeNumber;
                txtChequeDate.Text = receiptEntity.ChequeDate;
                txtBank.Text = receiptEntity.ChequeBank;
                txtBranch.Text = receiptEntity.ChequeBranch;
                ddlLocalOrOutStation.SelectedValue = receiptEntity.LocalOrOutstation;
                hdnAdvanceAmount.Value = TwoDecimalConversion(receiptEntity.AdvanceAmount);
                hdnAdvanceChequSlipNo.Value = receiptEntity.AdvanceChequeSlipNumber;

                txtCode.Text = receiptEntity.CustomerCode;
                txtLocation.Text = receiptEntity.Location;
                txtAddress1.Text = receiptEntity.Address1;
                txtAddress2.Text = receiptEntity.Address2;
                txtAddress3.Text = receiptEntity.Address3;
                txtAddress4.Text = receiptEntity.Address4;

                if (receiptEntity.PaymentType == "DR")
                {
                    tblNEFTremarks.Attributes.Add("Style", "display:inline");
                    txtNEFTremarks.Text = receiptEntity.Remarks;
                }
                else
                {
                    tblNEFTremarks.Attributes.Add("Style", "display:none");
                    txtNEFTremarks.Text = "";
                }
                
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
                btnReset.Enabled = true;

                txtTempRecptNumber.Enabled = false;
                txtTempRecptDate.Enabled = false;
                txtChequeNumber.Enabled = false;
                txtChequeDate.Enabled = false;
                txtBank.Enabled = false;
                txtBranch.Enabled = false;
                ddlLocalOrOutStation.Enabled = false;
                ddlModeOfReceipt.Enabled = false;
                ddlReason.Enabled = true;
                
                tblBalanceAmount.Visible = false;
                ChkHeader.Visible = false;

                UpdpanelTop.Update();
                UpdPanelGrid.Update();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ReceiptsCancellation), exp);
            }
        }

        protected void imgEditToggle_Click(object sender, EventArgs e)
        {
            try
            {
                ddlReceiptNumber.Visible = true;
                txtReceiptNumber.Visible = false;

                if (txtReceiptNumber.Text.Trim() == "")
                {
                    ddlReceiptNumber.DataSource = null;
                    ddlReceiptNumber.DataBind();
                }
                else
                {
                    ddlReceiptNumber.DataSource = receiptTransactions.GetReceiptsListForCancellation(ddlBranch.SelectedValue, txtReceiptNumber.Text);
                    ddlReceiptNumber.DataBind();
                }

                ddlBranch.Enabled = false;
                tblBalanceAmount.Visible = false;
                ChkHeader.Visible = false;
                txtAmount.Enabled = false;
                txtTempRecptNumber.Enabled = false;
                txtTempRecptDate.Enabled = false;
                txtChequeNumber.Enabled = false;
                txtChequeDate.Enabled = false;
                txtBank.Enabled = false;
                txtBranch.Enabled = false;
                ddlLocalOrOutStation.Enabled = false;

                FirstGridViewRow(string.Empty);
                txtReceiptNumber.Text = string.Empty;
                txtReceiptDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                ddlCustomer.Enabled = false;
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
                ddlModeOfReceipt.Enabled = false;

                txtCode.Text = string.Empty;
                txtLocation.Text = string.Empty;
                txtAddress1.Text = string.Empty;
                txtAddress2.Text = string.Empty;
                txtAddress3.Text = string.Empty;
                txtAddress4.Text = string.Empty;

                BtnCancel.Attributes.Add("OnClick", "return funFYRcvReceiptCancellationSubmit();");
                tblBalanceAmount.Visible = false;
                ChkHeader.Visible = false;

                imgEditToggle.Visible = false;

                UpdpanelTop.Update();
                UpdPanelGrid.Update();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ReceiptsCancellation), exp);
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                BtnCancel.Attributes.Add("style","display:none");
                BtnCancel.Visible = false;
                BtnCancel.Enabled = false;
                ddlAccountingPeriod.Enabled = false;
                SubmitHeaderAndItems();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ReceiptsCancellation), exp);
            }
        }

        protected void BtnReset_Click(object sender, EventArgs e)
        {
            try
            {
                Server.ClearError();
                Response.Redirect("ReceiptsCancellation.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ReceiptsCancellation), exp);
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
        
        private void SetDefaultValues()
        {
            LoadAccountintPeriod();
            txtReceiptNumber.Text = string.Empty;
            ddlReceiptNumber.SelectedValue = "0";
            ddlCustomer.SelectedValue = "0";

            txtTempRecptNumber.Text = string.Empty;
            txtTempRecptDate.Text = string.Empty;
            txtChequeNumber.Text = string.Empty;
            txtChequeDate.Text = string.Empty;
            txtBank.Text = string.Empty;
            txtBranch.Text = string.Empty;

            txtCode.Text = string.Empty;
            txtLocation.Text = string.Empty;
            txtAddress1.Text = string.Empty;
            txtAddress2.Text = string.Empty;
            txtAddress3.Text = string.Empty;
            txtAddress4.Text = string.Empty;
        }

        private void FreezeButtons(bool Fzflag)
        {
            DivOuter.Disabled = !Fzflag;
            imgEditToggle.Enabled = Fzflag;
            ddlReason.Enabled = !Fzflag;
        }

        private string TwoDecimalConversion(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
                return "0.00";
            else
                return string.Format("{0:0.00}", Convert.ToDecimal(strValue));
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
            grvItemDetails.Rows[0].Cells[0].Text = strNoRowsFoundMsg;
            grvItemDetails.Rows[0].Cells[0].CssClass = "EmptyRowStyle";
        }

        [WebMethod]
        public static void SetSessionRemarks(string Remarks)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Page objp = new Page();
                objp.Session["ReceiptsRemarks"] = Remarks;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void SubmitHeaderAndItems()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            string ReceiptNumber = string.Empty;
            try
            {                
                receiptEntity.Items = new List<ReceiptItem>();
                receiptEntity.BranchCode = ddlBranch.SelectedValue;
                receiptEntity.CustomerCode = ddlCustomer.SelectedValue;
                receiptEntity.Amount = txtAmount.Text;
                receiptEntity.ReceiptNumber = ddlReceiptNumber.SelectedValue;
                receiptEntity.PaymentType = ddlModeOfReceipt.SelectedValue;
                receiptEntity.AdvanceAmount = hdnAdvanceAmount.Value;
                receiptEntity.AdvanceChequeSlipNumber = hdnAdvanceChequSlipNo.Value;

                if (ddlReason.SelectedValue == "O")
                    receiptEntity.Remarks = Session["ReceiptsRemarks"].ToString();
                else
                    receiptEntity.Remarks = ddlReason.SelectedItem.Text;

                receiptEntity.HORefNo = txtNEFTremarks.Text;

                ReceiptItem receiptItem = null;
                int SNo = 0;

                foreach (GridViewRow gr in grvItemDetails.Rows)
                {
                    receiptItem = new ReceiptItem();
                    SNo += 1;

                    TextBox txtReferenceType = (TextBox)gr.FindControl("txtReferenceType");
                    TextBox txtReferenceDocNumber = (TextBox)gr.FindControl("txtReferenceDocNumber");
                    TextBox txtReferenceDocNumber1 = (TextBox)gr.FindControl("txtReferenceDocNumber1");
                    TextBox txtCollectionAmount = (TextBox)gr.FindControl("txtCollectionAmount");
                    DropDownList ddlPaymentIndicator = (DropDownList)gr.FindControl("ddlPaymentIndicator");

                    receiptItem.SNO = SNo.ToString();
                    receiptItem.ReferenceType = txtReferenceType.Text.Trim();
                    receiptItem.ReferenceDocumentNumber = txtReferenceDocNumber.Text.Trim();
                    receiptItem.ReferenceDocumentNumber1 = txtReferenceDocNumber1.Text.Trim();
                    receiptItem.CollectionAmount = txtCollectionAmount.Text.Trim();
                    receiptItem.PaymentIndicator = ddlPaymentIndicator.SelectedValue;

                    receiptEntity.Items.Add(receiptItem);
                }

                ReceiptTransactions receiptTransactions = new ReceiptTransactions();

                DataSet ds = receiptTransactions.CancelReceiptEntry(ref receiptEntity);

                if (receiptEntity.ErrorMsg == string.Empty && receiptEntity.ErrorCode == "0")
                {
                    FreezeButtons(false);

                    ddlBranch.Enabled = false;
                    ddlReceiptNumber.Enabled = false;
                    BtnCancel.Enabled = false;
                    btnReset.Enabled = true;
                    ddlReason.Enabled = false;
                    ChkHeader.Enabled = false;
                    grvItemDetails.Enabled = false;

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Receipt Has been Cancelled successfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('" + receiptEntity.ErrorMsg + "');", true);

                    BtnCancel.Enabled = false;
                    btnReset.Enabled = true;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}
