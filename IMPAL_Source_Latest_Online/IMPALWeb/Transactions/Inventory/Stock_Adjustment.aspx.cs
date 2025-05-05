using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using IMPALWeb.UserControls;

namespace IMPALWeb.Transactions.Inventory.Query
{
    public partial class Stock_Adjustment : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(Stock_Adjustment), exp);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                HdnBranchCode.Value = Session["BranchCode"].ToString();
                txtEntrydate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                lblError.Text = "";

                if (!IsPostBack)
                {
                    UCPartDeails.Visible = false;
                    ddlTagnumber.Visible = false;
                    txtTagnumber.Visible = true;
                    txtTagnumber.Focus();
                    BtnSubmit.Enabled = false;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(Stock_Adjustment), exp);
            }
        }

        protected void ImgButtonQuery_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImgButtonQuery.Enabled = false;
                StockAdjustment stockAdjustment = new StockAdjustment();
                List<TagNumber> objTagNumbers = new List<TagNumber>();

                objTagNumbers = stockAdjustment.GetTagNumber(Session["BranchCode"].ToString(), txtTagnumber.Text.Trim());
                ddlTagnumber.DataSource = objTagNumbers;
                ddlTagnumber.DataTextField = "Tag_Number";
                ddlTagnumber.DataValueField = "Tag_Number";
                ddlTagnumber.DataBind();

                txtTagnumber.Visible = false;
                ddlTagnumber.Visible = true;
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(Stock_Adjustment), exp);
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            int RowCount = 0;
            try
            {
                using (ObjectDataSource obj = new ObjectDataSource())
                {
                    StockAdjustmentItems objSection = new StockAdjustmentItems();
                    objSection.Item_Code = txtItemcode.Text;
                    objSection.Entry_Date = txtEntrydate.Text;
                    objSection.Physical_Balance = txtPhysicalBalance.Text;
                    objSection.Location = txtLocation.Text;
                    objSection.Phy_Balance_Date = txtPhysicalBalanceDate.Text;
                    objSection.Phy_verify = txtPhysicalVerificationBy.Text;
                    objSection.Supplier_Part_Number = txtSuppPartNo.Text;
                    objSection.Tag_Number = txtTagnumber.Text;
                    objSection.Tag_Date = txtTagDate.Text;
                    objSection.WH_Ref_No_Reason = txtWHRefNo_Reason.Text;
                    objSection.Computer_Balance = txtComputerBalance.Text;
                    objSection.Approvedby = txtApprovedBy.Text;
                    objSection.OS_LS_Indicator = ddlOS_LS_Indicator.SelectedValue;
                    objSection.Remarks = ddlRemarks.SelectedValue;
                    objSection.Invoice_Number = txtInvoiceNumber.Text;
                    objSection.Invoice_Date = txtInvoiceDate.Text;
                    objSection.Accounting_Period_Code = hdnAccountingPeriod.Value;

                    StockAdjustment objSaveStockAdjustment = new StockAdjustment();
                    RowCount = objSaveStockAdjustment.UptStockAdjustNumber(objSection, Session["BranchCode"].ToString());

                    if (RowCount > 0)
                        lblError.Text = "Saved Successfully";

                    btnReset_Click(sender, e);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(Stock_Adjustment), exp);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                Server.ClearError();
                Response.Redirect("Stock_Adjustment.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(Stock_Adjustment), exp);
            }
        }

        protected void ddlTagnumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlTagnumber.Enabled = false;

                using (ObjectDataSource obj = new ObjectDataSource())
                {
                    StockAdjustment stockAdjustment = new StockAdjustment();
                    StockAdjustmentItems objSection = new StockAdjustmentItems();

                    objSection = stockAdjustment.GetStockAdjustmentDetails(Session["BranchCode"].ToString(), ddlTagnumber.SelectedValue);

                    if (!(objSection.Item_Code == "" || objSection.Item_Code == null))
                    {
                        txtItemcode.Text = objSection.Item_Code;
                        UCPartDeails.Visible = true;
                        UCPartDeails.ItemCode = objSection.Item_Code;

                        if (objSection.Location == "")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Item Location Details Does not exists. Please Add the Same');", true);
                            BtnSubmit.Enabled = false;
                        }
                        else
                        {
                            BtnSubmit.Enabled = true;
                        }

                        txtTagnumber.Text = objSection.Tag_Number;
                        txtTagDate.Text = objSection.Tag_Date;
                        txtSuppPartNo.Text = objSection.Supplier_Part_Number;
                        txtComputerBalance.Text = objSection.Computer_Balance;
                        txtLocation.Text = objSection.Location;
                        ddlOS_LS_Indicator.SelectedValue = objSection.OS_LS_Indicator;
                        hdnAccountingPeriod.Value = objSection.Accounting_Period_Code;
                        //txtPhysicalBalance.Text = objSection.Physical_Balance;                        
                        //txtPhysicalBalanceDate.Text = objSection.Phy_Balance_Date;
                        //txtPhysicalVerificationBy.Text = objSection.Phy_verify;                        
                        //txtWHRefNo_Reason.Text = objSection.WH_Ref_No_Reason;                        
                        //txtApprovedBy.Text = objSection.Approvedby;                        
                        //ddlRemarks.SelectedValue = objSection.Remarks;
                    }
                    else
                    {
                        Server.ClearError();
                        Response.Redirect("Stock_Adjustment.aspx", false);
                        Context.ApplicationInstance.CompleteRequest();
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(Stock_Adjustment), exp);
            }
        }
    }
}
