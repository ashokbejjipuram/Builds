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
    public partial class BMSEntry : System.Web.UI.Page
    {
        private string strBranchCode;
        IMPALLibrary.Payable objPayable = new IMPALLibrary.Payable();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = (string)Session["BranchCode"];

                if (!IsPostBack)
                {
                    BtnSubmit.Attributes.Add("OnClick", "return ValidationSubmit();");
                    txtBMSDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    ddlHOBMSNumber.Visible = false;
                    imgEditToggle.Visible = true;
                    lblIsExists.Text = "";
                    lblHeaderMessage.Text = "";
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(typeof(BMSEntry), ex);
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                BMS bms = new BMS();
                IMPALLibrary.Payable payableEntity = new IMPALLibrary.Payable();

                if (ddlHOBMSNumber.Visible == true)
                {
                    bms.HOBMSNumber = ddlHOBMSNumber.SelectedValue;
                    bms.BMSNumber = txtBMSNumber.Text;
                    bms.SupplierCode = ddlSupplier.SelectedValue;
                    bms.BMSDate = txtBMSDate.Text;
                    bms.BMSDueDate = txtBMSDueDate.Text;
                    bms.BMSAmount = Convert.ToDecimal(txtBMSAmount.Text);
                    bms.BMSBankName = txtBankName.Text;
                    bms.BranchCode = strBranchCode;

                    int result = payableEntity.UpdateBMSEntry(ref bms);

                    if (result == 1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Details Updated Successfully');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Error in Data');", true);
                    }
                }
                else
                {
                    bool IsExists = payableEntity.CheckExists(txtBMSNumber.Text);
                    if (IsExists)
                    {
                        lblIsExists.Text = "Record already exists";
                    }
                    else
                    {
                        bms.BMSNumber = txtBMSNumber.Text;
                        bms.SupplierCode = ddlSupplier.SelectedValue;
                        bms.BMSDate = txtBMSDate.Text;
                        bms.BMSDueDate = txtBMSDueDate.Text;
                        bms.BMSAmount = Convert.ToDecimal(txtBMSAmount.Text);
                        bms.BMSBankName = txtBankName.Text;
                        bms.BranchCode = strBranchCode;

                        string HOBMSNumber = payableEntity.AddNewBMSEntry(ref bms);

                        if (HOBMSNumber != "")
                        {
                            txtHOBMSNumber.Text = HOBMSNumber;
                            txtHOBMSNumber.Enabled = false;
                            BtnSubmit.Enabled = false;
                            imgEditToggle.Visible = false;
                            lblIsExists.Text = "";
                            ddlSupplier.Enabled = false;
                            txtBMSNumber.Enabled = false;
                            txtBMSDate.Enabled = false;
                            txtBMSDueDate.Enabled = false;
                            txtBMSAmount.Enabled = false;
                            txtBankName.Enabled = false;
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Details Submitted Successfully');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Error in Data');", true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(typeof(BMSEntry), ex);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                Server.ClearError();
                Response.Redirect("BMSEntry.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(BMSEntry), exp);
            }
        }

        protected void imgEditToggle_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                LoadHOBMSNumber();
                ddlHOBMSNumber_SelectedIndexChanged(ddlHOBMSNumber, EventArgs.Empty);                
            }
            catch (Exception ex)
            {
                Log.WriteException(typeof(BMSEntry), ex);
            }
        }

        private void LoadHOBMSNumber()
        {
            List<BMS> lstBMSNumber = new List<BMS>();
            lstBMSNumber = objPayable.GetHOBMSNumber();
            ddlHOBMSNumber.DataSource = lstBMSNumber;
            ddlHOBMSNumber.DataTextField = "BMSNumber";
            ddlHOBMSNumber.DataValueField = "BMSNumber";
            ddlHOBMSNumber.DataBind();
        }

        protected void ddlHOBMSNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblHeaderMessage.Text = "";

                List<BMS> lstBMSNumber = new List<BMS>();
                lstBMSNumber = objPayable.GetBMSHeader(ddlHOBMSNumber.SelectedValue.ToString());
                if (lstBMSNumber.Count > 0)
                {
                    txtBMSNumber.Text = lstBMSNumber[0].BMSNumber;
                    txtBMSDate.Text = lstBMSNumber[0].BMSDate;
                    ddlSupplier.SelectedValue = lstBMSNumber[0].SupplierCode;
                    txtBMSDueDate.Text = lstBMSNumber[0].BMSDueDate;
                    txtBMSAmount.Text = Convert.ToString(lstBMSNumber[0].BMSAmount);
                    txtBankName.Text = lstBMSNumber[0].BMSBankName;


                    if (lstBMSNumber[0].BMSStatus == "P")
                    {
                        BtnSubmit.Visible = false;
                        ddlSupplier.Enabled = false;
                        txtHOBMSNumber.Enabled = false;
                        txtBMSDate.Enabled = false;
                        txtBMSNumber.Enabled = false;
                        txtBMSDueDate.Enabled = false;
                        txtBankName.Enabled = false;
                        txtBMSAmount.Enabled = false;
                        lblHeaderMessage.Text = "Payment already made vide Cheque Slip No:" + lstBMSNumber[0].TransactionNumber + ", Dated: " + lstBMSNumber[0].TransactionDate;
                    }
                    else
                    {
                        BtnSubmit.Visible = true;
                        ddlSupplier.Enabled = true;
                        txtHOBMSNumber.Enabled = true;
                        txtBMSNumber.Enabled = true;
                        txtBMSDate.Enabled = true;
                        txtBMSDueDate.Enabled = true;
                        txtBankName.Enabled = true;
                        txtBMSAmount.Enabled = true;
                        ddlSupplier.Items.RemoveAt(0);
                    }

                    ddlHOBMSNumber.Visible = true;
                    txtHOBMSNumber.Visible = false;
                    imgEditToggle.Visible = false;
                }
                else
                {
                    txtBMSDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    ddlSupplier.SelectedValue = "0";
                    txtBMSDueDate.Text = "";
                    txtBMSAmount.Text = "";
                    txtBankName.Text = "";
                    lblHeaderMessage.Text = "";
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(typeof(BMSEntry), ex);
            }
        }
    }
}
