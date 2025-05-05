using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Masters;
using IMPALLibrary;

namespace IMPALWeb.Transactions.Finance.Payable
{
    public partial class BMSPayment : System.Web.UI.Page
    {
        IMPALLibrary.Payable objPayable = new IMPALLibrary.Payable();
        string strBranchCode = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = (string)Session["BranchCode"];

                if (!IsPostBack)
                {
                    txtChequeSlipDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    ddlChequeSlipNumber.Visible = false;
                    btnReport.Visible = false;
                    ddlBranch.SelectedValue = strBranchCode;
                    txtHOREFNumber.Visible = false;
                    BtnSubmit.Attributes.Add("OnClick", "return ValidationSubmit();");
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(typeof(OtherPayment), ex);
            }
        }

        protected void imgEditToggle_Click(object sender, EventArgs e)
        {
            try
            {
                LoadChequeSlipNumber();
                ddlChequeSlipNumber.Visible = true;
                txtChequeSlipNumber.Visible = false;
                imgEditToggle.Visible = false;
                BankAccNo.Visible = false;
                ddlHOREFNumber.Visible = false;
                txtHOREFNumber.Visible = true;
                btnReport.Visible = true;
                BtnSubmit.Visible = false;
                txtHOREFNumber.Enabled = false;
                txtRemarks.Enabled = false;
                txtChequeNumber.Enabled = false;
                txtChequeDate.Enabled = false;
            }
            catch (Exception ex)
            {
                Log.WriteException(typeof(OtherPayment), ex);
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                ChequeSlipHeader chequeSlipHeader = new ChequeSlipHeader();
                IMPALLibrary.Payable payableEntity = new IMPALLibrary.Payable();
                chequeSlipHeader.Items = new List<ChequeSlipDetail>();
                ChequeSlipDetail chequeSlipDetail = null;
                chequeSlipHeader.ChequeSlipNumber = "";
                chequeSlipHeader.ChequeSlipDate = txtChequeSlipDate.Text;
                chequeSlipHeader.BranchCode = Session["BranchCode"].ToString();
                chequeSlipHeader.Chart_of_Account_Code = txtChartofAccount.Text;
                chequeSlipHeader.SupplierCode = "410";
                chequeSlipHeader.ChequeNumber = txtChequeNumber.Text;
                chequeSlipHeader.ChequeDate = txtChequeDate.Text;
                chequeSlipHeader.Remarks = txtRemarks.Text;
                chequeSlipHeader.ChequeBank = txtBank.Text;
                chequeSlipHeader.ChequeBranch = txtBranch.Text;
                chequeSlipHeader.ChequeAmount = txtAmount.Text;

                int Sno = 0;

                foreach (GridViewRow gr in grvItemDetails.Rows)
                {
                    Sno += 1;
                    chequeSlipDetail = new ChequeSlipDetail();
                    TextBox txtBMSNumber = (TextBox)gr.Cells[0].FindControl("txtBMSNumber");
                    TextBox txtBMSDate = (TextBox)gr.Cells[1].FindControl("txtBMSDate");
                    TextBox txtBMSDueDate = (TextBox)gr.Cells[2].FindControl("txtBMSDueDate");
                    TextBox txtBMSAmount = (TextBox)gr.Cells[3].FindControl("txtBMSAmount");
                    TextBox txtNoofDays = (TextBox)gr.Cells[4].FindControl("txtNoofDays");
                    TextBox txtCDValue = (TextBox)gr.Cells[5].FindControl("txtCDValue");
                    TextBox txtTotalValue = (TextBox)gr.Cells[6].FindControl("txtTotalValue");
                    HiddenField hdnRateofInterest = (HiddenField)gr.Cells[6].FindControl("hdnRateofInterest");
                    chequeSlipDetail.SerialNo = Sno;
                    chequeSlipDetail.ReferenceDocumentNumber = txtBMSNumber.Text;
                    chequeSlipDetail.ReferenceDocumentDate = txtBMSDate.Text;
                    chequeSlipDetail.Amount = Convert.ToDecimal(Convert.ToString(txtTotalValue.Text));
                    chequeSlipDetail.DocumentValue = txtBMSAmount.Text;
                    chequeSlipDetail.CDValue = Convert.ToDecimal(txtCDValue.Text);
                    chequeSlipDetail.Branch = Session["BranchCode"].ToString();
                    chequeSlipDetail.CDPercentage = Convert.ToDecimal(hdnRateofInterest.Value);
                    chequeSlipHeader.Items.Add(chequeSlipDetail);
                }

                string documentNumber = objPayable.AddNewChequeSlip(ref chequeSlipHeader);

                if (documentNumber != "")
                {
                    txtChequeSlipNumber.Text = documentNumber;
                    btnReport.Visible = true;
                    BtnSubmit.Visible = false;
                    imgEditToggle.Enabled = false;
                    ddlHOREFNumber.Enabled = false;
                    txtRemarks.Enabled = false;
                    BankAccNo.Visible = false;
                    txtChequeNumber.Enabled = false;
                    txtChequeDate.Enabled = false;
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Details Submitted Successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Error in Data');", true);
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(typeof(BMSPayment), ex);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                Server.ClearError();
                Response.Redirect("BMSPayment.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(BMSPayment), exp);
            }
        }

        protected void ucChartAccount_SearchImageClicked(object sender, EventArgs e)
        {
            try
            {
                txtChartofAccount.Text = Session["ChatAccCode"].ToString();

            }
            catch (Exception ex)
            {
                Log.WriteException(typeof(BMSPayment), ex);
            }
        }

        private void LoadChequeSlipNumber()
        {

            List<ChequeSlipHeader> lstChequeSlipNumber = new List<ChequeSlipHeader>();
            lstChequeSlipNumber = objPayable.GetALLChequeSlipNumberBMS();
            ddlChequeSlipNumber.DataSource = lstChequeSlipNumber;
            ddlChequeSlipNumber.DataTextField = "ChequeSlipNumber";
            ddlChequeSlipNumber.DataValueField = "ChequeSlipNumber";
            ddlChequeSlipNumber.DataBind();
        }

        protected void ddlChequeSlipNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List<ChequeSlipHeader> lstChequeSlipHeader = new List<ChequeSlipHeader>();
                lstChequeSlipHeader = objPayable.GetBMSPayment(ddlChequeSlipNumber.SelectedValue);

                if (lstChequeSlipHeader.Count > 0)
                {
                    for (int i = 0; i <= lstChequeSlipHeader.Count - 1; i++)
                    {
                        txtChequeSlipNumber.Text = lstChequeSlipHeader[i].ChequeSlipNumber;
                        txtChequeSlipDate.Text = lstChequeSlipHeader[i].ChequeSlipDate;
                        txtBranch.Text = lstChequeSlipHeader[i].Branch;
                        txtRemarks.Text = lstChequeSlipHeader[i].Remarks;
                        txtChartofAccount.Text = lstChequeSlipHeader[i].Chart_of_Account_Code;
                        txtHOREFNumber.Text = lstChequeSlipHeader[i].HOREFNumber;
                        txtChequeNumber.Text = lstChequeSlipHeader[i].ChequeNumber;
                        txtChequeDate.Text = lstChequeSlipHeader[i].ChequeDate;
                        txtAmount.Text = lstChequeSlipHeader[i].ChequeAmount;
                        txtBank.Text = lstChequeSlipHeader[i].ChequeBank;
                        txtBranch.Text = lstChequeSlipHeader[i].ChequeBranch;
                    }                   

                    List<BMSPaymentAdviceDetail> lstDetail = new List<BMSPaymentAdviceDetail>();
                    lstDetail = objPayable.GetBMSPaymentDetails(txtHOREFNumber.Text);

                    grvItemDetails.DataSource = lstDetail;
                    grvItemDetails.DataBind();                    
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(typeof(BMSPayment), ex);
            }
        }

        protected void ddlHOREFNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ChequeSlipHeader lstChequeSlipHeader = new ChequeSlipHeader();

                lstChequeSlipHeader = objPayable.GetBMSPaymentBankDetailsForInsert(txtChartofAccount.Text);
                txtBank.Text = lstChequeSlipHeader.ChequeBank;
                txtBranch.Text = lstChequeSlipHeader.ChequeBranch;
                BankAccNo.Visible = false;

                if (!(lstChequeSlipHeader.ChequeBank == null && lstChequeSlipHeader.ChequeBranch == null))
                {
                    List<BMSPaymentAdviceDetail> lstDetail = new List<BMSPaymentAdviceDetail>();
                    lstDetail = objPayable.GetBMSPaymentDetailsForInsert(ddlHOREFNumber.SelectedValue);

                    decimal amount = lstDetail.Sum(item => Convert.ToDecimal(item.BMSValue));
                    txtAmount.Text = Convert.ToString(amount);
                    grvItemDetails.DataSource = lstDetail;
                    grvItemDetails.DataBind();
                    UpdPanelGrid.Update();
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(typeof(BMSPayment), ex);
            }
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                string chequeSlipNumber = ddlChequeSlipNumber.SelectedValue == "0" ? txtChequeSlipNumber.Text : ddlChequeSlipNumber.SelectedValue;

                Server.ClearError();
                Response.Redirect("~/Reports/Finance/Accounts Payable/BMSPaymentReport.aspx?ChequeSlipNumber=" + chequeSlipNumber, false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                Log.WriteException(typeof(BMSPayment), ex);
            }
        }
    }
}
