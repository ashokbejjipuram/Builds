using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using IMPALLibrary.Masters;
using System.Web.Script.Services;
using System.Web.Services;

namespace IMPALWeb.UserControls
{
    public partial class LiabilityPaymentInvDetails : System.Web.UI.UserControl
    {
        IMPALLibrary.Payable objPayable = new IMPALLibrary.Payable();

        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
        }

        public event EventHandler SearchImageClicked;

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Session["LiabilityPaymentBranch"] = txtBranch.Text; ;
                Session["LiabilityPaymentInvAmt"] = txtAdjTotal.Text;
                Session["LiabilityPaymentCdAmt"] = txtCDTotal.Text;

                if (hdnCnt.Value != null && hdnCnt.Value != "")
                    Session["HOhdnCnt"] = hdnCnt.Value;
                else
                    Session["HOhdnCnt"] = grvLiabilityPaymentInvoiceDetails.Rows.Count;

                objPayable.UpdateCorporatePaymentBranchTotalTempInvoice(Session["hdnSupplier"].ToString(), hdnInvoiceNumbers.Value, Session["hdnBranch"].ToString());
                MPELiabilityPayment.Hide();

                if (SearchImageClicked != null)
                    SearchImageClicked(this, EventArgs.Empty);
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                string branch = Session["hdnBranch"].ToString();                
                string amount = Session["hdnInvAmt"].ToString();
                string cdamt = Session["hdnCdAmt"].ToString();
                string supplier = Session["hdnSupplier"].ToString();
                string fromdate = Session["hdnFromDt"].ToString();
                string todate = Session["hdnToDt"].ToString();
                //string hidrowcount = Session["hidrow"];
                //string hidrowcount1 = Session["hidrow1"];
                txtBranch.Text = branch;
                txtAdjTotal.Text = amount;
                txtCDTotal.Text = cdamt;
                txtBranchTotal.Text = amount;

                List<PaymentProcessTemp> lstDetail = objPayable.LoadCorporateLiabilityPaymentBranchTotalTempInvoice(supplier, fromdate, todate, branch);
                grvLiabilityPaymentInvoiceDetails.DataSource = lstDetail;
                grvLiabilityPaymentInvoiceDetails.DataBind();

                MPELiabilityPayment.Show();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void imgBtnPopupExit_Click(object sender, ImageClickEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Session["HOhdnCnt"] = 0;
                MPELiabilityPayment.Hide();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}