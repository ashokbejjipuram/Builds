using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using IMPALLibrary.Masters;

namespace IMPALWeb.Transactions.Finance.Payable
{
    public partial class OtherPayment : System.Web.UI.Page
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
                    ddlChequeSlipNumber.Visible = false;
                    txtChequeSlipDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    ddlBranch.SelectedValue = strBranchCode;
                    BtnSubmit.Attributes.Add("OnClick", "return ValidationSubmit();");
                    txtHOREFNumber.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(typeof(OtherPayment), ex);
            }
        }

        protected void imgEditToggle_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                LoadChequeSlipNumber();
                ddlChequeSlipNumber.Visible = true;
                txtChequeSlipNumber.Visible = false;
                imgEditToggle.Visible = false;
                BtnSubmit.Visible = false;
                
                txtRemarks.Text = "";
                txtChequeSlipNumber.Text = "";
                txtHOREFNumber.Text = "";
                ddlSupplier.SelectedValue = "0";
                txtAmount.Text = "";
                txtChequeDate.Text = "";
                txtChequeNumber.Text = "";
                txtChequeBank.Text = "";
                txtChequeBranch.Text = "";               
                grvItemDetails.DataSource = null;
                grvItemDetails.DataBind();
                ddlHOREFNumber.Visible = true;
                txtHOREFNumber.Visible = false;                
                txtChartofAccount.Text = "";
                ddlHOREFNumber.SelectedValue = "0";
                upHeader.Update();
                UpdPanelGrid.Update();

            }
            catch (Exception ex)
            {
                Log.WriteException(typeof(OtherPayment), ex);
            }
        }

        private void LoadChequeSlipNumber()
        {

            List<ChequeSlipHeader> lstChequeSlipNumber = new List<ChequeSlipHeader>();
            lstChequeSlipNumber = objPayable.GetChequeSlipNumber();
            ddlChequeSlipNumber.DataSource = lstChequeSlipNumber;
            ddlChequeSlipNumber.DataTextField = "ChequeSlipNumber";
            ddlChequeSlipNumber.DataValueField = "ChequeSlipNumber";
            ddlChequeSlipNumber.DataBind();
        }

        protected void ddlChequeSlipNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlChequeSlipNumber.SelectedValue != "-- Select --")
                {
                    List<ChequeSlipHeader> lstChequeSlipNumber = new List<ChequeSlipHeader>();
                    lstChequeSlipNumber = objPayable.GetChequeSlipHeader(ddlChequeSlipNumber.SelectedValue.ToString());
                    if (lstChequeSlipNumber.Count > 0)
                    {

                        for (int i = 0; i <= lstChequeSlipNumber.Count - 1; i++)
                        {
                            txtChequeNumber.Text = lstChequeSlipNumber[i].ChequeNumber;
                            txtChequeSlipDate.Text = lstChequeSlipNumber[i].ChequeSlipDate;
                            txtChequeBranch.Text = lstChequeSlipNumber[0].ChequeBranch;
                            txtChequeDate.Text = lstChequeSlipNumber[i].ChequeDate;
                            txtChequeBank.Text = lstChequeSlipNumber[0].ChequeBank;
                            ddlBranch.SelectedValue = lstChequeSlipNumber[i].BranchCode;
                            txtRemarks.Text = lstChequeSlipNumber[0].Remarks;
                            txtChartofAccount.Text = lstChequeSlipNumber[0].Chart_of_Account_Code;
                            ddlSupplier.SelectedValue = lstChequeSlipNumber[i].SupplierCode;                            
                            txtHOREFNumber.Text = lstChequeSlipNumber[i].HOREFNumber;
                            txtAmount.Text = lstChequeSlipNumber[i].ChequeAmount;
                        }
                    }

                    List<ChequeSlipDetail> lstChequeSlipDetail = new List<ChequeSlipDetail>();
                    lstChequeSlipDetail = objPayable.GetChequeSlipDetail(ddlChequeSlipNumber.SelectedValue.ToString());

                    if (lstChequeSlipDetail.Count > 0)
                    {
                        grvItemDetails.DataSource = lstChequeSlipDetail;
                        grvItemDetails.DataBind();
                    }

                    ddlHOREFNumber.Visible = false;
                    txtHOREFNumber.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(typeof(OtherPayment), ex);
            }
        }

        protected void ddlHOREFNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string Amount = objPayable.GetOtherPaymentAmount(ddlHOREFNumber.SelectedValue);
                txtAmount.Text = Amount;
            }
            catch (Exception ex)
            {
                Log.WriteException(typeof(OtherPayment), ex);
            }
        }

        protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ChequeSlipHeader lstChequeSlipHeader = new ChequeSlipHeader();

                lstChequeSlipHeader = objPayable.GetBMSPaymentBankDetailsForInsert(txtChartofAccount.Text);
                txtChequeBank.Text = lstChequeSlipHeader.ChequeBank;
                txtChequeBranch.Text = lstChequeSlipHeader.ChequeBranch;
                LoadHORefNumber(ddlSupplier.SelectedValue);
            }
            catch (Exception ex)
            {
                Log.WriteException(typeof(OtherPayment), ex);
            }
        }

        private void LoadHORefNumber(string supplierCode)
        {
            List<HORNumber> lstHORNumber = new List<HORNumber>();
            lstHORNumber = objPayable.LoadHOREFNumber(supplierCode);
            ddlHOREFNumber.DataSource = lstHORNumber;
            ddlHOREFNumber.DataTextField = "HOREFNumber";
            ddlHOREFNumber.DataValueField = "HOREFNumber";
            ddlHOREFNumber.DataBind();
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                ChequeSlipHeader chequeSlipHeader = new ChequeSlipHeader();
                IMPALLibrary.Payable payableEntity = new IMPALLibrary.Payable();
                chequeSlipHeader.ChequeSlipNumber = "";
                chequeSlipHeader.ChequeSlipDate = txtChequeSlipDate.Text;
                chequeSlipHeader.BranchCode = Session["BranchCode"].ToString();
                chequeSlipHeader.Chart_of_Account_Code = txtChartofAccount.Text;
                chequeSlipHeader.SupplierCode = ddlSupplier.SelectedValue;
                chequeSlipHeader.HOREFNumber = ddlHOREFNumber.SelectedValue;
                chequeSlipHeader.ChequeNumber = txtChequeNumber.Text;
                chequeSlipHeader.ChequeDate = txtChequeDate.Text;
                chequeSlipHeader.Remarks = txtRemarks.Text;
                chequeSlipHeader.ChequeBank = txtChequeBank.Text;
                chequeSlipHeader.ChequeBranch = txtChequeBranch.Text;
                chequeSlipHeader.ChequeAmount = txtAmount.Text;

                string documentNumber = objPayable.AddNewChequeSlipForOtherPayment(ref chequeSlipHeader);

                if (documentNumber != "")
                {
                    txtChequeSlipNumber.Text = documentNumber;

                    List<ChequeSlipDetail> lstChequeSlipDetail = new List<ChequeSlipDetail>();
                    lstChequeSlipDetail = objPayable.GetChequeSlipDetailForOtherPaymentSearch(ddlHOREFNumber.SelectedValue);

                    if (lstChequeSlipDetail.Count > 0)
                    {
                        grvItemDetails.DataSource = lstChequeSlipDetail;
                        grvItemDetails.DataBind();
                    }
                }

                BtnSubmit.Visible = false;
                imgEditToggle.Enabled = false;
                BankAccNo.Visible = false;
                ddlSupplier.Enabled = false;
                ddlHOREFNumber.Enabled = false;
                txtAmount.Enabled = false;
                txtRemarks.Enabled = false;
                txtChequeNumber.Enabled = false;
                txtChequeDate.Enabled = false;
                grvItemDetails.Enabled = false;
            }
            catch (Exception ex)
            {
                Log.WriteException(typeof(OtherPayment), ex);
            }

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                Server.ClearError();
                Response.Redirect("OtherPayment.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                Log.WriteException(typeof(OtherPayment), ex);
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
                Log.WriteException(typeof(OtherPayment), ex);
            }
        }
    }
}
