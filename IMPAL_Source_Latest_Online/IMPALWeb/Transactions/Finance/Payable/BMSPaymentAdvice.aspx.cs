using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IMPALLibrary;
using IMPALLibrary.Masters;

namespace IMPALWeb.Transactions.Finance.Payable
{
    public partial class BMSPaymentAdvice : System.Web.UI.Page
    {
        IMPALLibrary.Payable objPayable = new IMPALLibrary.Payable();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    btnDetails.Attributes.Add("OnClick", "return ValidationSubmit();");
                    BtnSubmit.Attributes.Add("OnClick", "return ValidationProcess();");
                    txtTotalAmount.Text = "0.00";
                    BtnSubmit.Visible = false;
                    idTrans.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(typeof(BMSPaymentAdvice), ex);
            }
        }

        private void FirstGridViewRow()
        {
            List<BMSPaymentAdviceDetail> lstDetail = new List<BMSPaymentAdviceDetail>();
            lstDetail = objPayable.GetBMSPaymentAdvice(ddlSupplier.SelectedValue, txtfromdate.Text, txtToDate.Text);

            grvItemDetails.DataSource = lstDetail;
            grvItemDetails.DataBind();

            decimal bmsAmount = lstDetail.Sum(item => Convert.ToDecimal(item.BMSAmount));
            decimal cdAmount = lstDetail.Sum(item => Convert.ToDecimal(item.CDAmount));
            decimal totalValue = lstDetail.Sum(item => Convert.ToDecimal(item.TotalValue));
            decimal bmsValue = lstDetail.Sum(item => Convert.ToDecimal(item.BMSValue));

            TextBox txtTotalBMSAmount = (TextBox)grvItemDetails.FooterRow.FindControl("txtTotalBMSAmount");
            txtTotalBMSAmount.Text = TwoDecimalConversion(Convert.ToString(bmsAmount));
            txtTotalAmount.Text = TwoDecimalConversion(Convert.ToString(bmsValue));

            TextBox txtTotalCDValue = (TextBox)grvItemDetails.FooterRow.FindControl("txtTotalCDValue");
            txtTotalCDValue.Text = TwoDecimalConversion(Convert.ToString(cdAmount));

            TextBox txtTotalValue = (TextBox)grvItemDetails.FooterRow.FindControl("txtTotalValue");
            txtTotalValue.Text = TwoDecimalConversion(Convert.ToString(bmsValue));

            BtnSubmit.Visible = true;
            btnDetails.Visible = false;
            idHeader.Disabled = false;
            idTrans.Visible = true;
        }

        protected void btnDetails_Click(object sender, EventArgs e)
        {
            try
            {
                FirstGridViewRow();
            }
            catch (Exception ex)
            {
                Log.WriteException(typeof(BMSPaymentAdvice), ex);
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                BMSPaymentAdviceDetails bmsPaymentAdviceDetails = new BMSPaymentAdviceDetails();
                bmsPaymentAdviceDetails.bmsPaymentAdviceDetail = new List<BMSPaymentAdviceDetail>();
                BMSPaymentAdviceDetail bmsPaymentAdviceDetail = null;
                bmsPaymentAdviceDetail = new BMSPaymentAdviceDetail();

                foreach (GridViewRow gr in grvItemDetails.Rows)
                {
                    bmsPaymentAdviceDetail = new BMSPaymentAdviceDetail();
                    CheckBox txtSelected = (CheckBox)gr.Cells[0].FindControl("txtSelected");
                    TextBox txtBMSNumber = (TextBox)gr.Cells[1].FindControl("txtBMSNumber");
                    bmsPaymentAdviceDetail.BMSNumber = txtBMSNumber.Text;
                    bmsPaymentAdviceDetail.Selected = txtSelected.Checked;
                    bmsPaymentAdviceDetails.bmsPaymentAdviceDetail.Add(bmsPaymentAdviceDetail);
                }

                string BMSCCWHNumber = objPayable.UpdateBMSHeaderPymtAdvice(ref bmsPaymentAdviceDetails, Session["BranchCode"].ToString(), ddlSupplier.SelectedValue);

                if (BMSCCWHNumber != "" && BMSCCWHNumber != null)
                {
                    Session["BMSCCWHNumber"] = BMSCCWHNumber;
                    Server.ClearError();
                    Response.Redirect("~/Reports/Finance/Accounts Payable/BMSPaymentAdviceReport.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Error in Data');", true);
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(typeof(BMSPaymentAdvice), ex);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                Server.ClearError();
                Response.Redirect("BMSPaymentAdvice.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                Log.WriteException(typeof(BMSPaymentAdvice), ex);
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
