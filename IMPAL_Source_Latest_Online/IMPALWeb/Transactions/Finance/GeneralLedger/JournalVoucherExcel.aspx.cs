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
using System.IO;
using System.Data.OleDb;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.SqlClient;
using static IMPALLibrary.ReceivableInvoice;
using System.Globalization;

namespace IMPALWeb.Transactions.Finance.GeneralLedger
{
    public partial class JournalVoucherExcel : System.Web.UI.Page
    {
        private string filePath = System.Configuration.ConfigurationManager.AppSettings["UploadPath"].ToString();
        private AccountingPeriods Acc = new AccountingPeriods();
        private ReceivableInvoice objReceivableInvoice = new ReceivableInvoice();

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(JournalVoucherExcel), exp);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BtnSubmit.Visible = false;
                    //btnReport.Visible = false;
                    ddlJVNumber.Visible = false;
                    ddlJVDate.Items.Clear();
                    ddlJVDate.Attributes.Add("style", "display:none");

                    txtBranch.Text = Session["BranchName"].ToString();
                    hdnBranch.Value = Session["BranchCode"].ToString();
                    LoadAccountingPeriod();
                    idTrans.Visible = false;
                    idGrid.Visible = false;
                    txtAccountingPeriod.Visible = false;

                    DateItem PrevDateStatus = objReceivableInvoice.GetPreviousDateStatus(Session["UserID"].ToString(), "JVExcel");

                    if (PrevDateStatus.DateItemCode != "0")
                    {
                        txtJVDate.Enabled = true;
                        txtJVDate.Attributes.Add("onkeypress", "return false;");
                        txtJVDate.Attributes.Add("onkeyup", "return false;");
                        txtJVDate.Attributes.Add("onkeydown", "return false;");
                        txtJVDate.Attributes.Add("onpaste", "return false;");
                        txtJVDate.Attributes.Add("ondragstart", "return false;");
                        calTransactionDate.StartDate = DateTime.ParseExact(PrevDateStatus.DateItemDesc, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        calTransactionDate.EndDate = DateTime.ParseExact(PrevDateStatus.DateItemDesc, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    else
                        txtJVDate.Enabled = false;

                    LoadJVNumber(hdnBranch.Value, ddlAccountingPeriod.SelectedValue);
                }                
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(JournalVoucherExcel), exp);
            }
        }

        private void FirstGridViewRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

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
            List<string> lstAccountingPeriod = new List<string>();

            int PrevFinYearStatus = objReceivableInvoice.GetPreviousAccountingPeriodStatus(Session["UserID"].ToString(), "JVExcel");

            if (PrevFinYearStatus > 0)
            {
                lstAccountingPeriod.Add((DateTime.Today.Year - 1).ToString() + "-" + DateTime.Today.Year.ToString());
            }

            string accountingPeriod = GetCurrentFinancialYear();
            lstAccountingPeriod.Add(accountingPeriod);
            txtJVDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

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
                ddlJVNumber.Visible = true;
                txtJVNumber.Visible = false;
                imgEditToggle.Visible = false;
                FirstGridViewRow();

                DisableOnEditMode();
                grvTransactionDetails.Enabled = false;
                BtnSubmit.Enabled = false;
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
                Log.WriteException(typeof(JournalVoucherExcel), exp);
            }
        }

        private void LoadJVNumber(string strBranch, string AccPeriodCode)
        {
            IMPALLibrary.JournalVoucher journalLedger = new IMPALLibrary.JournalVoucher();

            List<JVHeader> lstJVNumber = new List<JVHeader>();
            lstJVNumber = journalLedger.GetJVNumber(strBranch, AccPeriodCode);
            ddlJVNumber.DataSource = lstJVNumber;
            ddlJVNumber.DataTextField = "JVNumber";
            ddlJVNumber.DataValueField = "JVNumber";
            ddlJVNumber.DataBind();
        }

        protected void ddlJVDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtJVDate.Text = ddlJVDate.SelectedValue;
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(JournalVoucherExcel), exp);
            }
        }

        protected void ddlJVNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
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
                            //ddlAccountingPeriod.SelectedItem.Text = lstJVNumber[i].AccountingPeriod;
                            txtAccountingPeriod.Text = lstJVNumber[i].AccountingPeriod;
                        }
                    }

                    List<JVDetail> lstJVDetail = new List<JVDetail>();
                    lstJVDetail = journalLedger.GetJVDetail(ddlJVNumber.SelectedValue.ToString(), Session["BranchCode"].ToString());

                    if (lstJVNumber.Count > 0)
                    {
                        grvTransactionDetails.DataSource = lstJVDetail;
                        grvTransactionDetails.DataBind();

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
                                }
                            }
                        }
                        
                        idTrans.Visible = true;
                        idGrid.Visible = false;
                    }
                }
                else
                {
                    btnReset_Click(this, null);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(JournalVoucherExcel), exp);
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
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(JournalVoucherExcel), exp);
            }
        }

        protected void btnUploadExcel_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            DataTable pdtCashAndBankPaymentExcel = new DataTable();
            try
            {
                UploadFileData(false);
                ShowSuccessMessage(Session["UploadDetails"].ToString());
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void ShowSuccessMessage(string message)
        {
            lblUploadMessage.Visible = true;
            lblUploadMessage.Text = "<br /><br /><center style='font-size:13px;'><b>" + message + "</center>";
        }

        protected void UploadFileData(bool isHOProcess)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                string sStatus = string.Empty;

                if (btnFileUpload.HasFile)
                {
                    filePath = @"D:\Downloads\JournalVoucher";

                    Session["UploadDetails"] = "";

                    string fileName = btnFileUpload.FileName;

                    if (File.Exists(filePath + "\\" + fileName))
                        File.Delete(filePath + "\\" + fileName);

                    btnFileUpload.SaveAs(filePath + "\\" + fileName);

                    if (File.Exists(Path.Combine(filePath, fileName)))
                    {
                        UploadJVDetails(filePath, fileName);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "alert('File doesnt exists');", true);
                    }
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        protected void UploadJVDetails(string filePath, string fileName)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Database ImpalDb = DataAccess.GetDatabase();

                string myexceldataquery = "Select Description, Remarks, Ref_No, Ref_Date, Debit_Amount, Credit_Amount from [sheet1$]";
                string ExcelConnectionString = "";
                string sqlTempQuery = "";
                decimal CrAmt = 0;
                decimal DrAmt = 0;

                sqlTempQuery = "truncate table Temp_Journal_Voucher_excel";
                DbCommand cmd = ImpalDb.GetSqlStringCommand(sqlTempQuery);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                ImpalDb.ExecuteNonQuery(cmd);

                if (fileName.ToString().EndsWith("s"))
                    ExcelConnectionString = @"provider=microsoft.jet.oledb.4.0;data source=" + filePath + "\\" + fileName + ";extended properties=" + "\"excel 8.0;hdr=yes;\"";
                else
                    ExcelConnectionString = @"provider=Microsoft.ace.oledb.12.0;data source=" + filePath + "\\" + fileName + ";extended properties=" + "\"excel 12.0 xml;hdr=yes;\"";

                OleDbConnection OledbConn = new OleDbConnection(ExcelConnectionString);
                OleDbCommand OledbCmd = new OleDbCommand(myexceldataquery, OledbConn);
                OledbConn.Open();
                OleDbDataReader dr = OledbCmd.ExecuteReader();
                while (dr.Read())
                {
                    if (dr[4].ToString().Trim() == "")
                        DrAmt = 0;
                    else
                        DrAmt = Convert.ToDecimal(dr[4].ToString().Trim());

                    if (dr[5].ToString().Trim() == "")
                        CrAmt = 0;
                    else
                        CrAmt = Convert.ToDecimal(dr[5].ToString().Trim());

                    sqlTempQuery = "Exec Usp_AddTemp_Journal_Voucher_excel '" + dr[0] + "','" + dr[1] + "','" + dr[2] + "','" + dr[3] + "'," + DrAmt + "," + CrAmt + ",'" + hdnBranch.Value + "','" + Session["UserID"].ToString() + "', 0";
                    DbCommand cmd1 = ImpalDb.GetSqlStringCommand(sqlTempQuery);
                    cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDb.ExecuteNonQuery(cmd1);
                }

                OledbConn.Close();

                string file = Path.Combine(filePath, fileName);
                IMPALLibrary.JournalVoucher journalVoucher = new IMPALLibrary.JournalVoucher();
                JVHeader jvEntity = journalVoucher.GetJournalVoucherDetailsExcel(hdnBranch.Value, 1);
                HdnExcelTotalValue.Value = jvEntity.Amount;
                divItemDetailsExcel.Attributes.Add("style", "display:none");

                if (jvEntity.ErrorMsg != "")
                {
                    btnUploadExcel.Enabled = false;
                    lblUploadMessage.Visible = false;
                    BtnSubmit.Visible = false;
                    BtnSubmit.Enabled = false;
                    grvTransactionDetails.Visible = false;
                    Session["UploadDetails"] = "<font style='color:red' size='2'>Chart of Account Names " + jvEntity.ErrorMsg + " in the Excel File differs from the Master.<br />Please Check the file" + file + "</font>";
                }
                else
                {
                    if (jvEntity.ErrorCode == "0.00")
                    {
                        grvTransactionDetails.Visible = false;
                        idGrid.Visible = true;
                        BtnSubmit.Visible = true;
                        BtnSubmit.Enabled = true;
                        Session["UploadDetails"] = "<b><font style='color:green' size='3'>Transaction Amount has been tallied. Please Submit the file" + file + "</font></b>";

                        grvTransactionDetails.DataSource = (object)jvEntity.Items;
                        grvTransactionDetails.DataBind();

                        decimal TotalDebitAmount = 0;
                        decimal TotalCreditAmount = 0;

                        foreach (GridViewRow gr in grvTransactionDetails.Rows)
                        {
                            TextBox txtDebitAmount = (TextBox)gr.FindControl("txtDebitAmount");
                            TextBox txtCreditAmount = (TextBox)gr.FindControl("txtCreditAmount");
                            gr.Enabled = false;

                            TotalDebitAmount += Convert.ToDecimal(txtDebitAmount.Text);
                            TotalCreditAmount += Convert.ToDecimal(txtCreditAmount.Text);
                        }

                        if (grvTransactionDetails.FooterRow != null)
                        {
                            TextBox txtTotalDebitAmount = (TextBox)grvTransactionDetails.FooterRow.FindControl("txtTotalDebitAmount");
                            TextBox txtTotalCreditAmount = (TextBox)grvTransactionDetails.FooterRow.FindControl("txtTotalCreditAmount");

                            txtTotalDebitAmount.Text = TotalDebitAmount.ToString();
                            txtTotalCreditAmount.Text = TotalCreditAmount.ToString();
                        }
                    }
                    else
                    {
                        btnUploadExcel.Enabled = false;
                        lblUploadMessage.Visible = false;
                        BtnSubmit.Visible = false;
                        BtnSubmit.Enabled = false;
                        grvTransactionDetails.Visible = false;
                        Session["UploadDetails"] = "<b><font style='color:red' size='3'>Transaction Amount Differs with the Excel Amount by Rs." + jvEntity.ErrorCode + ".<br />Please Check the file" + file + "</font></b>";
                    }
                }
            }
            catch (Exception exp)
            {
                Session["UploadDetails"] = "Error in the Data";
                throw new Exception(exp.Message);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                Server.ClearError();
                Response.Redirect("JournalVoucherExcel.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(JournalVoucherExcel), exp);
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                JVHeader jvHeader = new JVHeader();
                List<JVDetail> objvalue = new List<JVDetail>();

                jvHeader.JVNumber = "";
                jvHeader.JVDate = txtJVDate.Text;
                jvHeader.AccountingPeriod = ddlAccountingPeriod.SelectedValue.ToString();
                jvHeader.BranchCode = hdnBranch.Value;
                jvHeader.Narration = txtNarration.Text + " - Excel Upload";
                jvHeader.ReferenceDocDate = txtReferenceDocumentDate.Text;
                jvHeader.ReferenceDocNumber = txtReferenceDocumentNumber.Text;
                jvHeader.ReferenceDocType = txtReferenceDocumentType.Text;
                int SNo = 0;

                if (Page.IsValid)
                {
                    if (grvTransactionDetails.Rows.Count >= 1)
                    {
                        for (int i = 0; i < grvTransactionDetails.Rows.Count; i++)
                        {
                            JVDetail obj = new JVDetail();
                            var strChartOfAccount = (TextBox)grvTransactionDetails.Rows[i].FindControl("txtChartOfAccount");
                            var strDescription = (TextBox)grvTransactionDetails.Rows[i].FindControl("txtDescription");
                            var strRemarks = (TextBox)grvTransactionDetails.Rows[i].FindControl("txtRemarks");
                            var strRefDocType = (DropDownList)grvTransactionDetails.Rows[i].FindControl("ddlRefDocType");
                            var strRefDocNumber = (TextBox)grvTransactionDetails.Rows[i].FindControl("txtRefDocNumber");
                            var strRefDocDate = (TextBox)grvTransactionDetails.Rows[i].FindControl("txtRefDocDate");
                            var strDrCr = (TextBox)grvTransactionDetails.Rows[i].FindControl("txtDrCr");
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
                string JVNumber = journalVoucher.AddNewJVEntry(ref jvHeader);

                if ((jvHeader.ErrorMsg == string.Empty) && (jvHeader.ErrorCode == "0"))
                {
                    txtJVNumber.Text = JVNumber;
                    BtnSubmit.Enabled = false;
                    grvTransactionDetails.Enabled = false;
                    txtNarration.Enabled = false;
                    txtReferenceDocumentNumber.Enabled = false;
                    txtReferenceDocumentDate.Enabled = false;
                    imgEditToggle.Enabled = false;

                    upHeader.Update();
                    UpdPanelGrid.Update();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('JV details have been inserted successfully. Please Get the Manager/HO Approval for Final GL Posting');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('" + jvHeader.ErrorMsg + "');", true);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(JournalVoucherExcel), exp);
            }
        }

        private void DisableOnEditMode()
        {
            txtJVDate.Enabled = false;
            //txtReferenceDocumentType.Enabled = false;
            txtReferenceDocumentDate.Enabled = false;
            //imgRequDocDate.Enabled = false;
            txtReferenceDocumentNumber.Enabled = false;
            txtNarration.Enabled = false;
        }

        protected void ddlAccountingPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            DebitCredit objDebitCredit = new DebitCredit();

            ddlAccountingPeriod.Enabled = false;

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
