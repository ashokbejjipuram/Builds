using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Masters;
using IMPALLibrary;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using System.Web.Services;
using IMPALWeb.UserControls;

namespace IMPALWeb.Transactions.Finance.Payable
{
    public partial class LiabilityPayment : System.Web.UI.Page
    {
        IMPALLibrary.Payable objPayable = new IMPALLibrary.Payable();
        string strBranchCode = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = (string)Session["BranchCode"];

                if (!IsPostBack)
                {
                    //btnProcess.Enabled = false;

                    btnProcess.Attributes.Add("OnClick", "javascript:return ValidateClick(2);");

                    if (Session["Confirmstat"] == null)
                        objPayable.DeleteHoPymtTable(Session["BranchCode"].ToString());
                    else
                    {
                        txtInvoiceFromDate.Text = Session["InvFromDate"].ToString();
                        txtInvoiceToDate.Text = Session["InvToDate"].ToString();
                        ddlSupplier.SelectedValue = Session["ddlSupplier"].ToString();
                        txtTotalAmount.Text = Session["hdnTotVal"].ToString();
                        ddlSupplier.Enabled = false;
                    }
                    
                    txtHoCCWHNumber.Visible = false;
                    ddlHoCCWHNumber.Enabled = false;
                    fnLoadHoCCWHNumber();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crHoPaymentReport != null)
            {
                crHoPaymentReport.Dispose();
                crHoPaymentReport = null;
            }
        }
        protected void crHoPaymentReport_Unload(object sender, EventArgs e)
        {
            if (crHoPaymentReport != null)
            {
                crHoPaymentReport.Dispose();
                crHoPaymentReport = null;
            }
        }

        protected void imgEditToggle_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                imgEditToggle.Visible = false;
                txtHoCCWHNumber.Visible = false;
                ddlHoCCWHNumber.Visible = true;
                DivTop.Attributes.Add("style", "display:none");
                btnProcess.Attributes.Add("style", "display:none");
                btnClick.Attributes.Add("style", "display:none");
                btnReport.Attributes.Add("style", "display:inline");
                btnBack.Attributes.Add("style", "display:inline");
                ddlHoCCWHNumber.Enabled = true;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void fnLoadHoCCWHNumber()
        {
            List<ChequeSlipHeader> lstChequeSlipNumber = new List<ChequeSlipHeader>();
            lstChequeSlipNumber = objPayable.GetHoCCWHNumbers();
            ddlHoCCWHNumber.DataSource = lstChequeSlipNumber;
            ddlHoCCWHNumber.DataTextField = "HOREFNumber";
            ddlHoCCWHNumber.DataValueField = "HOREFNumber";
            ddlHoCCWHNumber.DataBind();
        }

        protected void ddlSupplier_SelectIndexChanged(object sender, EventArgs e)
        {
            DivHONum.Attributes.Add("style", "display:none");
            btnBack.Attributes.Add("style", "display:none");
            btnReport.Attributes.Add("style", "display:none");
        }

        protected void btnClick_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                List<PaymentProcessTemp> lstDetail = objPayable.LoadCorporatePymtBranchTotalTemp(ddlSupplier.SelectedValue, txtInvoiceFromDate.Text, txtInvoiceToDate.Text, ddlZone.SelectedValue, strBranchCode);
                grvLiabilityPaymentDetails.DataSource = lstDetail;
                grvLiabilityPaymentDetails.DataBind();

                btnProcess.Attributes.Remove("OnClick");

                int ZoneCnt = ddlZone.Items.Count;

                if (ZoneCnt == 1 || ZoneCnt == 2)
                {
                    btnProcess.Attributes.Add("OnClick", "javascript:return ValidateClick(3);");
                }
                else
                    btnProcess.Attributes.Add("OnClick", "javascript:return ValidateClick(2);");

                //btnProcess.Enabled = true;
                btnClick.Visible = false;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void btnProcess_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                PaymentProcessTempForBranches pymt = new PaymentProcessTempForBranches();
                pymt.paymentProcessTempForBranch = new List<PaymentProcessTempForBranch>();

                PaymentProcessTempForBranch pymtDetails = null;

                foreach (GridViewRow gr in grvLiabilityPaymentDetails.Rows)
                {
                    pymtDetails = new PaymentProcessTempForBranch();

                    CheckBox IsCheck = (CheckBox)gr.Cells[1].FindControl("chkSelected");

                    if (IsCheck.Checked)
                    {
                        TextBox txtBranch = (TextBox)gr.Cells[2].FindControl("txtBranch");
                        TextBox txtInvoiceAmount = (TextBox)gr.Cells[3].FindControl("txtInvoiceAmount");
                        pymtDetails.Selected = IsCheck.Checked;
                        pymtDetails.Supplier = ddlSupplier.SelectedValue;
                        pymtDetails.Branch = txtBranch.Text;
                        pymtDetails.BranchAmount = Convert.ToDecimal(txtInvoiceAmount.Text);
                        pymtDetails.FromDate = txtInvoiceFromDate.Text;
                        pymtDetails.ToDate = txtInvoiceToDate.Text;
                        pymtDetails.Zone = ddlZone.SelectedValue;
                        pymt.paymentProcessTempForBranch.Add(pymtDetails);
                    }
                }

                objPayable.UpdateHOPaymentProcess(ref pymt, Session["BranchCode"].ToString(), ddlSupplier.SelectedValue);

                string confirmValue = HdnConfirmvalue.Value;

                if (confirmValue == "No")
                {
                    string HoCCWHNumber = objPayable.SubmitHOPaymentProcess(ref pymt, Session["BranchCode"].ToString(), ddlSupplier.SelectedValue, Session["hdnTotVal"].ToString());

                    if (HoCCWHNumber != "" && HoCCWHNumber != null)
                    {
                        PanelHeaderDtls.Enabled = false;
                        ddlHoCCWHNumber.Visible = false;
                        imgEditToggle.Visible = false;

                        grvLiabilityPaymentDetails.Visible = false;
                        btnClick.Attributes.Add("style", "display:none");
                        btnProcess.Attributes.Add("style", "display:none");
                        btnReset.Attributes.Add("style", "display:none");
                        btnReport.Attributes.Add("style", "display:inline");
                        btnBack.Attributes.Add("style", "display:inline");
                        DivHONum.Attributes.Add("style", "display:inline");
                        imgEditToggle.Visible = false;
                        txtHoCCWHNumber.Visible = true;
                        ddlHoCCWHNumber.Visible = false;
                        txtHoCCWHNumber.Text = HoCCWHNumber;

                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Payment Advice Number " + HoCCWHNumber + " Has Been Submitted Successfully. Please Generate Report.');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Payment Amount Should Not Less then Zero');", true);
                    }
                }
                else
                {
                    Session["Confirmstat"] = "2";
                    Session["ddlSupplier"] = ddlSupplier.SelectedValue;
                    Session["InvFromDate"] = txtInvoiceFromDate.Text;
                    Session["InvToDate"] = txtInvoiceToDate.Text;
                    Server.ClearError();
                    Response.Redirect("LiabilityPayment.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
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
                Session["Confirmstat"] = null;
                Session["InvFromDate"] = "";
                Session["InvToDate"] = "";
                Session["hdnTotVal"] = "0";
                Server.ClearError();
                Response.Redirect("LiabilityPayment.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Session["Confirmstat"] = null;
                Session["InvFromDate"] = "";
                Session["InvToDate"] = "";
                Session["hdnTotVal"] = "0";
                Server.ClearError();
                Response.Redirect("LiabilityPayment.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            PanelHeaderDtls.Enabled = false;

            string strField = default(string);
            string strValue = default(string);

            strField = "{Corporate_Payment_Detail.ccwh_Number}";

            if (ddlHoCCWHNumber.Visible == false)
                strValue = txtHoCCWHNumber.Text;
            else
            {
                imgEditToggle.Visible = false;
                grvLiabilityPaymentDetails.Visible = false;
                btnClick.Attributes.Add("style", "display:none");
                btnProcess.Attributes.Add("style", "display:none");
                btnReset.Attributes.Add("style", "display:none");
                btnReport.Attributes.Add("style", "display:inline");
                btnBack.Attributes.Add("style", "display:inline");

                strValue = ddlHoCCWHNumber.SelectedValue;
            }

            crHoPaymentReport.ReportName = "HoPaymentReport";
            crHoPaymentReport.RecordSelectionFormula = strField + "= " + "'" + strValue + "'";
            crHoPaymentReport.GenerateReportAndExportA4();
        }

        [WebMethod]
        public static void SetSessionvalues(string Branch, string Amount, string CdAmount, string Supplier, string FromDate, string ToDate)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Page objp = new Page();
                objp.Session["hdnBranch"] = Branch;
                objp.Session["hdnInvAmt"] = Amount;
                objp.Session["hdnCdAmt"] = CdAmount;
                objp.Session["hdnSupplier"] = Supplier;
                objp.Session["hdnFromDt"] = FromDate;
                objp.Session["hdnToDt"] = ToDate;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        [WebMethod]
        public static void SetSessionTotvalue(string TotValue)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Page objp = new Page();
                objp.Session["hdnTotVal"] = TotValue;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ucLiabilityPaymentInvDetails_SearchImageClicked(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                GridViewRow gvr = (GridViewRow)((LiabilityPaymentInvDetails)sender).Parent.Parent;
                var txtBranch = (TextBox)gvr.FindControl("txtBranch");
                var txtInvoiceAmount = (TextBox)gvr.FindControl("txtInvoiceAmount");
                var txtCDAmount = (TextBox)gvr.FindControl("txtCDAmount");
                var chkBox = (CheckBox)gvr.FindControl("chkSelected");
                txtBranch.Text = Session["LiabilityPaymentBranch"].ToString();
                txtInvoiceAmount.Text = Session["LiabilityPaymentInvAmt"].ToString();
                txtCDAmount.Text = Session["LiabilityPaymentCdAmt"].ToString();

                if (Convert.ToInt16(Session["HOhdnCnt"].ToString()) > 0)
                    chkBox.Checked = true;
                else
                    chkBox.Checked = false;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:CalculateTotal();", true);
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}
