using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IMPALLibrary.Masters.Sales;
using IMPALLibrary;
using IMPALWeb.UserControls;
using IMPALLibrary.Transactions.Finance;
using System.Web.Services;

namespace IMPALWeb.Transactions.Finance.GeneralLedger
{
    public partial class JournalVoucherFinal : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(JournalVoucherFinal), exp);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ddlJVNumber.Visible = false;
                    txtJVDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    BtnSubmit.Attributes.Add("OnClick", "return ValidationSubmitFinal();");
                    BtnReject.Attributes.Add("OnClick", "return funJVReject();");
                    txtBranch.Text = Session["BranchName"].ToString();
                    hdnBranch.Value = Session["BranchCode"].ToString();
                    LoadAccountingPeriod();
                    PanelMessage.Visible = false;
                    ChkStatus.Value = "0";
                    idTrans.Visible = false;
                    idGrid.Visible = false;
                    txtAccountingPeriod.Visible = false;
                    BtnSubmit.Enabled = false;
                    BtnReject.Enabled = false;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(JournalVoucherFinal), exp);
            }
        }

        private void FirstGridViewRow()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("S_No", typeof(int)));
            dt.Columns.Add(new DataColumn("Chart_of_Account_Code", typeof(string)));
            dt.Columns.Add(new DataColumn("Description", typeof(string)));
            dt.Columns.Add(new DataColumn("Remarks", typeof(string)));
            dt.Columns.Add(new DataColumn("Ref_Doc_Type", typeof(string)));
            dt.Columns.Add(new DataColumn("Ref_Doc_Number", typeof(string)));
            dt.Columns.Add(new DataColumn("Ref_Doc_Date", typeof(string)));
            dt.Columns.Add(new DataColumn("Dr_Cr", typeof(string)));
            dt.Columns.Add(new DataColumn("Debit_Amount", typeof(double)));
            dt.Columns.Add(new DataColumn("Credit_Amount", typeof(string)));

            DataRow dr = null;
            dr = dt.NewRow();
            dr["Chart_of_Account_Code"] = string.Empty;
            dr["Description"] = string.Empty;
            dr["Remarks"] = string.Empty;
            dr["Ref_Doc_Type"] = "";
            dr["Ref_Doc_Number"] = string.Empty;
            dr["Ref_Doc_Date"] = string.Empty;
            dr["Dr_Cr"] = string.Empty;
            dr["Debit_Amount"] = 0;
            dr["Credit_Amount"] = 0;
            dt.Rows.Add(dr);

            grvTransactionDetails.DataSource = dt;
            grvTransactionDetails.DataBind();
        }


        private void LoadAccountingPeriod()
        {
            AccountingPeriods Acc = new AccountingPeriods();
            ReceivableInvoice objReceivableInvoice = new ReceivableInvoice();
            List<string> lstAccountingPeriod = new List<string>();

            int PrevFinYearStatus = objReceivableInvoice.GetPreviousAccountingPeriodStatus(Session["UserID"].ToString(), "JVApproval");

            if (PrevFinYearStatus > 0)
            {
                lstAccountingPeriod.Add((DateTime.Today.Year - 1).ToString() + "-" + DateTime.Today.Year.ToString());
            }

            string accountingPeriod = GetCurrentFinancialYear();
            lstAccountingPeriod.Add(accountingPeriod);

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
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
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


        protected void imgEditToggle_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (hdnBranch.Value != "")
                    LoadJVNumberFinal(hdnBranch.Value);

                txtJVNumber.Visible = false;
                ddlJVNumber.Visible = true;
                imgEditToggle.Visible = false;
                PanelMessage.Visible = false;
                FirstGridViewRow();

                DisableOnEditMode();
                
                ddlAccountingPeriod.Enabled = false;
                txtAccountingPeriod.Visible = true;
                ddlAccountingPeriod.Visible = false;
                txtNarration.Text = "";
                txtReferenceDocumentDate.Text = "";
                txtReferenceDocumentNumber.Text = "";
                
                UpdPanelGrid.Update();

            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(JournalVoucherFinal), exp);
            }
        }

        private void LoadJVNumberFinal(string strBranch)
        {
            IMPALLibrary.JournalVoucher journalLedger = new IMPALLibrary.JournalVoucher();

            List<JVHeader> lstJVNumber = new List<JVHeader>();
            lstJVNumber = journalLedger.GetJVNumberFinal(strBranch);
            ddlJVNumber.DataSource = lstJVNumber;
            ddlJVNumber.DataTextField = "JVNumber";
            ddlJVNumber.DataValueField = "JVNumber";
            ddlJVNumber.DataBind();
        }

        protected void ddlJVNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string Status = string.Empty;

                if (ddlJVNumber.SelectedValue != "-- Select --")
                {
                    IMPALLibrary.JournalVoucher journalLedger = new IMPALLibrary.JournalVoucher();
                    List<JVHeader> lstJVNumber = new List<JVHeader>();
                    lstJVNumber = journalLedger.GetJVHeader(ddlJVNumber.SelectedValue.ToString(), Session["BranchCode"].ToString());
                    if (lstJVNumber.Count > 0)
                    {
                        for (int i = 0; i <= lstJVNumber.Count - 1; i++)
                        {
                            ddlJVNumber.SelectedValue = lstJVNumber[i].JVNumber;
                            txtJVDate.Text = lstJVNumber[i].JVDate;
                            txtReferenceDocumentType.Text = lstJVNumber[i].ReferenceDocType;
                            txtReferenceDocumentDate.Text = lstJVNumber[i].ReferenceDocDate;
                            txtReferenceDocumentNumber.Text = lstJVNumber[i].ReferenceDocNumber;
                            txtNarration.Text = lstJVNumber[i].Narration;
                            Status = lstJVNumber[i].ApprovalStatus;
                            txtAccountingPeriod.Text = lstJVNumber[i].AccountingPeriod;
                        }
                    }

                    if (Status == "" || Status == null)
                    {
                        BtnSubmit.Visible = true;
                        BtnReject.Visible = true;
                        BtnSubmit.Enabled = true;
                        BtnReject.Enabled = true;
                    }
                    else
                    {
                        BtnSubmit.Visible = false;
                        BtnReject.Visible = false;
                    }

                    List<JVDetail> lstJVDetail = new List<JVDetail>();
                    lstJVDetail = journalLedger.GetJVDetail(ddlJVNumber.SelectedValue.ToString(), Session["BranchCode"].ToString());

                    if (lstJVNumber.Count > 0)
                    {
                        grvTransactionDetails.DataSource = lstJVDetail;
                        grvTransactionDetails.DataBind();

                        if (hdnBranch.Value == "COR" && grvTransactionDetails.Rows.Count > 250)
                        {
                            PanelMessage.Visible = true;
                            grvTransactionDetails.Visible = false;
                            lblUploadMessage.Text = "<br /><br /><center><b><font style='color:green' size='5'>This JV # has " + grvTransactionDetails.Rows.Count + " No.of Records and hence couldn't load the same on Screen. Please Approve the JV if Found Correct.</font></b></center>";
                            lblUploadMessage.Visible = true;
                        }
                        else
                        {
                            PanelMessage.Visible = false;
                            lblUploadMessage.Text = "";
                            lblUploadMessage.Visible = false;
                            grvTransactionDetails.Visible = true;
                        }

                        //if (grvTransactionDetails.HeaderRow != null)
                        //{
                        //    CheckBox chkSelectAll = (CheckBox)grvTransactionDetails.HeaderRow.FindControl("chkSelectAll");
                        //    chkSelectAll.Checked = true;
                        //}

                        if (grvTransactionDetails.FooterRow != null)
                        {
                            TextBox txtTotalDebitAmount = (TextBox)grvTransactionDetails.FooterRow.FindControl("txtTotalDebitAmount");
                            TextBox txtTotalCreditAmount = (TextBox)grvTransactionDetails.FooterRow.FindControl("txtTotalCreditAmount");

                            txtTotalDebitAmount.Text = "0";
                            txtTotalCreditAmount.Text = "0";

                            if (lstJVDetail.Count() > 0)
                            {
                                for (int i = 0; i < lstJVDetail.Count; i++)
                                {
                                    txtTotalDebitAmount.Text = TwoDecimalConversion(Convert.ToString(Convert.ToDouble(txtTotalDebitAmount.Text) + Convert.ToDouble(lstJVDetail[i].Debit_Amount)));
                                    txtTotalCreditAmount.Text = TwoDecimalConversion(Convert.ToString(Convert.ToDouble(txtTotalCreditAmount.Text) + Convert.ToDouble(lstJVDetail[i].Credit_Amount)));

                                    DropDownList ddlDrCr = (DropDownList)grvTransactionDetails.Rows[i].Cells[0].FindControl("ddlDrCr");
                                    TextBox txtDebitAmount = (TextBox)grvTransactionDetails.Rows[i].Cells[0].FindControl("txtDebitAmount");
                                    TextBox txtCreditAmount = (TextBox)grvTransactionDetails.Rows[i].Cells[0].FindControl("txtCreditAmount");
                                    ddlDrCr.SelectedValue = lstJVDetail[i].Dr_Cr.ToString();

                                    if (ddlDrCr.SelectedValue == "D")
                                    {
                                        txtDebitAmount.Enabled = true;
                                        txtCreditAmount.Enabled = false;
                                    }
                                    else
                                    {
                                        txtDebitAmount.Enabled = false;
                                        txtCreditAmount.Enabled = true;
                                    }
                                }
                            }
                        }

                        ChkStatus.Value = grvTransactionDetails.Rows.Count.ToString();

                        idTrans.Visible = true;
                        idGrid.Visible = true;
                    }
                    else
                    {
                        PanelMessage.Visible = false;
                        lblUploadMessage.Text = "";
                        lblUploadMessage.Visible = false;
                        grvTransactionDetails.Visible = false;
                    }
                }
                else
                {
                    btnReset_Click(this, null);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(JournalVoucherFinal), exp);
            }
        }

        protected void grvTransactionDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (IsPostBack)
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkSelected = (CheckBox)e.Row.FindControl("chkSelected");
                        TextBox txtDebitAmount = (TextBox)e.Row.FindControl("txtDebitAmount");
                        TextBox txtRefDocNumber = (TextBox)e.Row.FindControl("txtRefDocNumber");
                        TextBox txtCreditAmount = (TextBox)e.Row.FindControl("txtCreditAmount");
                        DropDownList ddlDrCr = (DropDownList)e.Row.FindControl("ddlDrCr");
                        TextBox txtChartOfAccount = (TextBox)e.Row.FindControl("txtChartOfAccount");
                        TextBox txtReqDocDate = (TextBox)e.Row.FindControl("txtReqDocDate");
                        GridViewRow grdRow = ((GridViewRow)txtDebitAmount.Parent.Parent);

                        chkSelected.Checked = true;
                        //txtDebitAmount.Attributes.Add("onchange", "javascript:calculateTotal()");
                        //txtCreditAmount.Attributes.Add("onchange", "javascript:calculateTotal()");
                        ddlDrCr.Attributes.Add("onchange", "javascript:ValidateDrCrFinal('" + ddlDrCr.ClientID + "', '" + txtChartOfAccount.ClientID + "', '" + txtDebitAmount.ClientID + "', '" + txtCreditAmount.ClientID + "', '" + txtRefDocNumber.ClientID + "', '" + txtReqDocDate.ClientID + "')");
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(JournalVoucherFinal), exp);
            }
        }

        protected void ucChartAccount_SearchImageClicked(object sender, EventArgs e)
        {
            try
            {
                IMPALLibrary.JournalVoucher journalLedger = new IMPALLibrary.JournalVoucher();
                GridViewRow gvr = (GridViewRow)((ChartAccount)sender).Parent.Parent;
                TextBox txtgrdChartOfAccount = (TextBox)gvr.FindControl("txtChartOfAccount");
                TextBox txtDescription = (TextBox)gvr.FindControl("txtDescription");
                TextBox txtRemarks = (TextBox)gvr.FindControl("txtRemarks");

                txtgrdChartOfAccount.Text = Session["ChatAccCode"].ToString();
                txtDescription.Text = journalLedger.GetDescription(Session["ChatAccCode"].ToString(), Session["BranchCode"].ToString());
                txtRemarks.Focus();

            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(JournalVoucherFinal), exp);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                Server.ClearError();
                Response.Redirect("JournalVoucherFinal.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(JournalVoucherFinal), exp);
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                JVHeader jvHeader = new JVHeader();
                List<JVDetail> objvalue = new List<JVDetail>();

                jvHeader.JVNumber = ddlJVNumber.SelectedValue;
                jvHeader.JVDate = txtJVDate.Text;
                jvHeader.AccountingPeriod = ddlAccountingPeriod.SelectedValue.ToString();
                jvHeader.BranchCode = hdnBranch.Value;
                jvHeader.Narration = txtNarration.Text;
                jvHeader.ReferenceDocDate = txtReferenceDocumentDate.Text;
                jvHeader.ReferenceDocNumber = txtReferenceDocumentNumber.Text;
                jvHeader.ReferenceDocType = txtReferenceDocumentType.Text;
                jvHeader.ApprovalLevel = Session["UserName"].ToString() + "/" + Session["UserID"];

                int SNo = 0;

                if (Page.IsValid)
                {
                    if (grvTransactionDetails.Rows.Count >= 1)
                    {
                        for (int i = 0; i < grvTransactionDetails.Rows.Count; i++)
                        {
                            CheckBox chkSelected = (CheckBox)grvTransactionDetails.Rows[i].FindControl("chkSelected");

                            if (!chkSelected.Checked)
                                continue;

                            JVDetail obj = new JVDetail();
                            var strChartOfAccount = (TextBox)grvTransactionDetails.Rows[i].FindControl("txtChartOfAccount");
                            var strDescription = (TextBox)grvTransactionDetails.Rows[i].FindControl("txtDescription");
                            var strRemarks = (TextBox)grvTransactionDetails.Rows[i].FindControl("txtRemarks");
                            var strRefDocType = (DropDownList)grvTransactionDetails.Rows[i].FindControl("ddlRefDocType");
                            var strRefDocNumber = (TextBox)grvTransactionDetails.Rows[i].FindControl("txtRefDocNumber");
                            var strRefDocDate = (TextBox)grvTransactionDetails.Rows[i].FindControl("txtReqDocDate");
                            var strDrCr = (DropDownList)grvTransactionDetails.Rows[i].FindControl("ddlDrCr");
                            var strDebitAmount = (TextBox)grvTransactionDetails.Rows[i].FindControl("txtDebitAmount");
                            var strCreditAmount = (TextBox)grvTransactionDetails.Rows[i].FindControl("txtCreditAmount");

                            if (strDrCr.Text != "0")
                            {
                                SNo += 1;
                                obj.SerialNumber = SNo.ToString();
                                obj.Chart_of_Account_Code = strChartOfAccount.Text;
                                obj.Description = strDescription.Text;
                                obj.Remarks = strRemarks.Text;
                                obj.Ref_Doc_Type = strRefDocType.SelectedValue;
                                obj.Ref_Doc_Date = strRefDocDate.Text;
                                obj.Ref_Doc_Number = strRefDocNumber.Text;
                                obj.Dr_Cr = strDrCr.Text;
                                obj.Credit_Amount = strCreditAmount.Text;
                                obj.Debit_Amount = strDebitAmount.Text;
                                objvalue.Add(obj);
                            }
                        }
                    }
                }

                jvHeader.Items = objvalue;
                IMPALLibrary.JournalVoucher journalVoucher = new IMPALLibrary.JournalVoucher();
                journalVoucher.AddNewJVEntryFinal(ref jvHeader);

                if ((jvHeader.ErrorMsg == string.Empty) && (jvHeader.ErrorCode == "0"))
                {
                    BtnSubmit.Enabled = false;
                    BtnReject.Enabled = false;
                    ddlJVNumber.Enabled = false;
                    grvTransactionDetails.Enabled = false;
                    imgEditToggle.Enabled = false;
                    upHeader.Update();
                    UpdPanelGrid.Update();

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('JV Final Entry has been done successfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('" + jvHeader.ErrorMsg + "');", true);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(JournalVoucherFinal), exp);
            }
        }

        protected void BtnReject_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                BtnSubmit.Enabled = false;
                BtnReject.Enabled = false;
                ddlJVNumber.Enabled = false;
                IMPALLibrary.JournalVoucher journalVoucher = new IMPALLibrary.JournalVoucher();
                journalVoucher.UpdNewJVEntry(Session["BranchCode"].ToString(), ddlJVNumber.SelectedValue, Session["JVRemarks"].ToString());
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('JV Entry has been Rejected/Cancelled Sucessfully');", true);
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
                objp.Session["JVRemarks"] = objp.Session["UserName"] + "/" + objp.Session["UserID"] + " - " + Remarks;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void DisableOnEditMode()
        {
            txtJVDate.Enabled = false;
            //txtReferenceDocumentType.Enabled = false;
            txtReferenceDocumentDate.Enabled = false;
            //imgRequDocDate.Enabled = false;
            //txtReferenceDocumentNumber.Enabled = false;
            //txtNarration.Enabled = false;
        }

        protected void ddlAccountingPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            DebitCredit objDebitCredit = new DebitCredit();

            if (ddlAccountingPeriod.SelectedIndex == 0)
                txtJVDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            else
                txtJVDate.Text = objDebitCredit.GetDocumentDate(ddlAccountingPeriod.SelectedValue);
        }

        private string TwoDecimalConversion(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
                return "0.00";
            else
                return string.Format("{0:0.00}", Convert.ToDecimal(strValue));
        }
    }
}
