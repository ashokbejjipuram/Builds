#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Web.Security;
using System.Web.Caching;
using AjaxControlToolkit;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using IMPALLibrary;
using IMPALLibrary.Common;  
using System.Data;
using IMPALWeb.UserControls;
using IMPLLib = IMPALLibrary.Transactions;
using IMPALLibrary.Masters.Sales;
using IMPALLibrary.Transactions.Finance;


#endregion Namespace

namespace IMPALWeb.Finance
{
    public partial class CashAndBankCancellation : System.Web.UI.Page
    {
        private AccountingPeriods Acc = new AccountingPeriods();
        private ReceivableInvoice objReceivableInvoice = new ReceivableInvoice();
        CashAndBankTransactions cashandbankTransactions = new CashAndBankTransactions();

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(CashAndBankCancellation), exp);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!IsPostBack)
                {
                    ddlBranch.SelectedIndex = 0;
                    ddlTransactionNumber.Visible = false;
                    txtTransactionDate.Enabled = false;
                    fnPopulateDropDown("CBCancellation", ddlPayment);
                    fnPopulateDropDown("CBPCashCheque", ddlCashCheque);
                    grvCashAndBankCancellation.Visible = false;
                    BtnCancel.Visible = false;
                    ddlPayment.Enabled = false;                    
                    fnPopulateDropDown("LocalOutStation", ddlLocalOutstation);

                    BtnCancel.Attributes.Add("OnClick", "return funCBCancellationSubmit();");
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
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

        protected void gvResults_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (grvCashAndBankCancellation.Rows.Count > 0)
            {
                if (grvCashAndBankCancellation.FooterRow != null)
                {
                    TextBox txtTotalAmount = (TextBox)grvCashAndBankCancellation.FooterRow.FindControl("txtTotalAmount");
                    txtTotalAmount.Text = "0";

                    for (int i = 0; i < grvCashAndBankCancellation.Rows.Count; i++)
                    {
                        TextBox txtGrdAmount = (TextBox)grvCashAndBankCancellation.Rows[i].Cells[2].FindControl("txtGrdAmount");

                        txtTotalAmount.Text = TwoDecimalConversion(Convert.ToString(Convert.ToDouble(txtTotalAmount.Text) + Convert.ToDouble(txtGrdAmount.Text == "" ? "0.00" : txtGrdAmount.Text)));
                    }
                }
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                BtnCancel.Attributes.Add("style", "display:none");
                BtnCancel.Visible = false;
                BtnCancel.Enabled = false;
                SubmitCashAndBankCancellation();
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
                Response.Redirect("CashAndBankCancellation.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                Log.WriteException(Source, ex);
            }
        }

        protected void ddlTransactionNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            if (ddlTransactionNumber.SelectedIndex > 0)
            {
                fnGetCashAndBankCancellation(ddlTransactionNumber.SelectedValue);
                grvCashAndBankCancellation.Visible = true;
                grvCashAndBankCancellation.Enabled = false;
                BtnCancel.Visible = true;
            }
            else
            {
                txtTransactionDate.Text = string.Empty;
                txtAccountingPeriod.Text = string.Empty;
                ddlPayment.SelectedIndex = 0;
                txtRemarks.Text = string.Empty;
                txtTransactionAmount.Text = string.Empty;
                txtChartOfAccount.Text = string.Empty;
                ddlCashCheque.SelectedIndex = 0;
                txtChequeNumber.Text = string.Empty;
                txtChequeDate.Text = string.Empty;
                txtBank.Text = string.Empty;
                txtBankBranch.Text = string.Empty;
                ddlLocalOutstation.SelectedIndex = 0;
                txtReferenceDate.Text = string.Empty;
                grvCashAndBankCancellation.DataSource = null;
                grvCashAndBankCancellation.DataBind();
                BtnCancel.Visible = false;
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

        private void SubmitCashAndBankCancellation()
        {
            ddlBranch.Enabled = false;
            ddlTransactionNumber.Enabled = false;
            txtReason.Enabled = false;

            CashAndBankEntity cashandbankEntity = new CashAndBankEntity();
            cashandbankEntity.BranchCode = ddlBranch.SelectedValue;
            cashandbankEntity.TransactionNumber = txtTransactionNumber.Text;
            cashandbankEntity.Remarks = txtReason.Text;

            int result = cashandbankTransactions.UpdCashBankDetailsCancellation(ref cashandbankEntity);

            if ((cashandbankEntity.ErrorMsg == string.Empty) && (cashandbankEntity.ErrorCode == "0"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('Cash & Bank Transaction Has been Cancelled successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('" + cashandbankEntity.ErrorMsg + "');", true);
            }
        }

        private void fnGetCashAndBankCancellation(string strTransactionNumber)
        {
            CashAndBankEntity cashandbankEntity = cashandbankTransactions.GetCashBankDetailsCancellation(ddlBranch.SelectedValue, strTransactionNumber);
            txtTransactionNumber.Text = strTransactionNumber;
            txtTransactionDate.Text = cashandbankEntity.TransactionDate;
            txtRemarks.Text = cashandbankEntity.Remarks;
            txtTransactionAmount.Text = cashandbankEntity.TransactionAmount;
            txtChartOfAccount.Text = cashandbankEntity.ChartOfAccountCode;
            ddlPayment.SelectedValue = cashandbankEntity.ReceiptPaymentIndicator;
            ddlCashCheque.SelectedValue = cashandbankEntity.CashChequeIndicator;
            txtAccountingPeriod.Text = cashandbankEntity.Accounting_Period;

            txtChequeNumber.Text = cashandbankEntity.Cheque_Number;
            txtChequeDate.Text = cashandbankEntity.Cheque_Date;
            txtBank.Text = cashandbankEntity.Cheque_Bank;
            txtBankBranch.Text = cashandbankEntity.Cheque_Branch;
            ddlLocalOutstation.SelectedValue = cashandbankEntity.Local_Outstation == "" ? "L" : cashandbankEntity.Local_Outstation;
            txtReferenceDate.Text = cashandbankEntity.Ref_Date;

            grvCashAndBankCancellation.DataSource = (object)cashandbankEntity.Items;
            grvCashAndBankCancellation.DataBind();

            foreach (GridViewRow gr in grvCashAndBankCancellation.Rows)
            {
                gr.Enabled = false;
            }
        }

        protected void imgEditToggle_Click(object sender, ImageClickEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddlTransactionNumber.Visible = true;
                txtTransactionNumber.Visible = false;

                if (txtTransactionNumber.Text.Trim() == "")
                {
                    ddlTransactionNumber.DataSource = null;
                    ddlTransactionNumber.DataBind();
                }
                else
                {
                    ddlTransactionNumber.DataSource = cashandbankTransactions.GetTransactionForCancellation(ddlBranch.SelectedValue, txtTransactionNumber.Text);
                    ddlTransactionNumber.DataBind();
                }

                DisableViewMode();
                grvCashAndBankCancellation.Visible = false;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void DisableViewMode()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddlBranch.Enabled = false;
                txtTransactionDate.Enabled = false;
                txtRemarks.Enabled = false;
                txtTransactionNumber.Enabled = false;
                txtTransactionAmount.Enabled = false;
                ddlPayment.Enabled = false;
                ddlCashCheque.Enabled = false;
                txtChequeNumber.Enabled = false;
                txtChequeDate.Enabled = false;
                txtBank.Enabled = false;
                txtBankBranch.Enabled = false;
                ddlLocalOutstation.Enabled = false;
                txtReferenceDate.Enabled = false;
                grvCashAndBankCancellation.Enabled = false;
                imgEditToggle.Visible = false;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
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
                            TextBox txtGrdChartAccount = (TextBox)grvCashAndBankCancellation.Rows[rowIndex].Cells[1].FindControl("txtGrdChartOfAccount");
                            TextBox txtGrdAmount = (TextBox)grvCashAndBankCancellation.Rows[rowIndex].Cells[2].FindControl("txtGrdAmount");
                            TextBox txtGrdRemarks = (TextBox)grvCashAndBankCancellation.Rows[rowIndex].Cells[3].FindControl("txtGrdRemarks");

                            txtGrdChartAccount.Text = dt1.Rows[i]["Chart_of_Account_Code"].ToString();
                            txtGrdAmount.Text = dt1.Rows[i]["Amount"].ToString();
                            txtGrdRemarks.Text = dt1.Rows[i]["Remarks"].ToString();
                            rowIndex++;

                            if (i < dt1.Rows.Count - 1)
                            {
                                txtGrdAmount.Enabled = false;
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

        private string TwoDecimalConversion(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
                return "0.00";
            else
                return string.Format("{0:0.00}", Convert.ToDecimal(strValue));
        }
    }    
}