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
    public partial class InvoiceCNEntry : System.Web.UI.Page
    {
        IMPALLibrary.Payable payableEntity = new IMPALLibrary.Payable();
        string strBranchCode = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = (string)Session["BranchCode"];

                if (!IsPostBack)
                {
                    hdnScreenMode.Value = "A";
                    lblHeaderMessage.Text = "";
                    ddlPaymentStatus.Enabled = false;
                    BtnSubmit.Attributes.Add("OnClick", "return ValidationSubmit(1);");
                    imgEditToggle.Attributes.Add("OnClick", "return ValidationEdit();");
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(typeof(InvoiceCNEntry), ex);
            }
        }

        protected void imgEditToggle_Click(object sender, ImageClickEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                List<InvoiceHeader> lstDetail = payableEntity.LoadInvoiceCNDetails(ddlBranch.SelectedValue, ddlSupplier.SelectedValue, txtInvoiceNumber.Text);

                if (lstDetail.Count > 0)
                {
                    ddlSupplier.SelectedValue = lstDetail[0].SupplierCode;
                    txtHOREFNumber.Text = lstDetail[0].HOREFNumber;
                    txtInvoiceNumber.Text = lstDetail[0].InvoiceNumber;
                    txtInvoiceDate.Text = lstDetail[0].InvoiceDate;
                    txtInvoiceAmount.Text = lstDetail[0].InvoiceAmount;
                    ddlIndicator.SelectedValue = lstDetail[0].Indicator;
                    ddlPaymentStatus.SelectedValue = lstDetail[0].Pymt_Status;
                    ddlBranch.SelectedValue = lstDetail[0].BranchCode;
                    hdnScreenMode.Value = "I";
                    lblHeaderMessage.Text = "";

                    DisableFields();
                    ddlPaymentStatus.Enabled = true;
                    BtnSubmit.Enabled = true;
                    BtnSubmit.Attributes.Remove("OnClick");
                    BtnSubmit.Attributes.Add("OnClick", "return ValidationSubmit(2);");
                }
                else
                {
                    lblHeaderMessage.Text = "Invoice Doesn't Exist With This Supplier";
                }               
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string HOREFNumber = "";
                InvoiceHeader invoiceHeader = new InvoiceHeader();                
                invoiceHeader.BranchCode = ddlBranch.SelectedValue;
                invoiceHeader.SupplierCode = ddlSupplier.SelectedValue;
                invoiceHeader.InvoiceNumber = txtInvoiceNumber.Text;
                invoiceHeader.InvoiceDate = txtInvoiceDate.Text;
                invoiceHeader.InvoiceAmount = txtInvoiceAmount.Text;
                invoiceHeader.Pymt_Status = ddlPaymentStatus.SelectedValue;
                invoiceHeader.Indicator = ddlIndicator.SelectedValue;
                lblHeaderMessage.Text = "";

                if (hdnScreenMode.Value == "A")
                {
                    HOREFNumber = payableEntity.ADDCorporateSupplierInvoicePayment(ref invoiceHeader);

                    if (HOREFNumber != "" && HOREFNumber != null)
                    {
                        txtHOREFNumber.Text = HOREFNumber;
                        DisableFields();
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Details have been Submitted successfully.');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Error in Entry');", true);
                    }
                }
                else
                {
                    invoiceHeader.HOREFNumber = txtHOREFNumber.Text;
                    payableEntity.UpdateCorporateSupplierInvoicePayment(ref invoiceHeader);
                    DisableFields();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Details Updated successfully.');", true);
                }                
            }
            catch (Exception ex)
            {
                Log.WriteException(typeof(InvoiceCNEntry), ex);
            }
        }

        protected void txtInvoiceNumber_TextChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                List<InvoiceHeader> lstDetail = payableEntity.LoadInvoiceCNDetails(ddlBranch.SelectedValue, ddlSupplier.SelectedValue, txtInvoiceNumber.Text);

                if (lstDetail.Count > 0)
                {
                    ddlSupplier.Enabled = false;
                    ddlBranch.Enabled = false;
                    txtInvoiceNumber.Enabled = false;
                    txtInvoiceDate.Enabled = false;
                    txtInvoiceAmount.Enabled = false;
                    ddlPaymentStatus.Enabled = false;
                    ddlIndicator.Enabled = false;
                    BtnSubmit.Enabled = false;
                    imgEditToggle.Enabled = false;
                    lblHeaderMessage.Text = "Invoice Already Exists With This Supplier";
                    BtnSubmit.Enabled = false;
                }
                else
                {
                    ddlSupplier.Enabled = true;
                    ddlBranch.Enabled = true;
                    txtInvoiceNumber.Enabled = true;
                    txtInvoiceDate.Enabled = true;
                    txtInvoiceAmount.Enabled = true;
                    ddlPaymentStatus.Enabled = false;
                    ddlIndicator.Enabled = true;
                    BtnSubmit.Enabled = true;
                    imgEditToggle.Enabled = true;
                    lblHeaderMessage.Text = "";
                    BtnSubmit.Enabled = true;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void txtInvoiceDate_TextChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                txtInvoiceNumber.AutoPostBack = true;
                txtInvoiceAmount.Focus();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        public void DisableFields()
        {
            ddlSupplier.Enabled = false;
            ddlBranch.Enabled = false;
            txtInvoiceNumber.Enabled = false;
            txtInvoiceDate.Enabled = false;
            txtInvoiceAmount.Enabled = false;
            ddlPaymentStatus.Enabled = false;
            ddlIndicator.Enabled = false;
            BtnSubmit.Enabled = false;
            imgEditToggle.Enabled = false;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                Server.ClearError();
                Response.Redirect("InvoiceCNEntry.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                Log.WriteException(typeof(InvoiceCNEntry), ex);
            }
        }
    }
}
