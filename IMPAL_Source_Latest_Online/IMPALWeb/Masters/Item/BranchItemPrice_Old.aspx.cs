using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using IMPALWeb.UserControls;

namespace IMPALWeb
{
    public partial class BranchItemPrice_Old : System.Web.UI.Page
    {
        ItemLocationDetails Item = new ItemLocationDetails();
        //BranchItemPrices Item = new BranchItemPrices();
        BranchItemPrices BIP = new BranchItemPrices();

        public string supplierLine_value = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    if ((string)Session["RoleCode"] == "BEDP")
                    {
                        grvItemDetails.ShowFooter = false;
                        int EditButtonIndex = grvItemDetails.Columns.Count - 1;
                        grvItemDetails.Columns[EditButtonIndex].Visible = false;
                    }

                    List<BIPSupplierLine> SupplierLineList = BIP.GetSuppliers();
                    drpSupplierLine.DataSource = SupplierLineList;
                    drpSupplierLine.DataTextField = "SupplierName";
                    drpSupplierLine.DataValueField = "SupplierCode";
                    drpSupplierLine.DataBind();
                    drpSupplierLine.SelectedIndex = 0;

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void BindItemDetails()
        {
            try
            {
                grvItemDetails.DataSource = BIP.GetBranchItemPriceList(drpSupplierLine.SelectedValue, txtSupplierPartNo.Text, Session["BranchCode"].ToString());
                //Item.GetItemDetails(drpSupplierLine.SelectedValue, txtSupplierPartNo.Text);
                grvItemDetails.DataBind();
            }
            catch (Exception)
            {

                throw;
            }
        }
        protected void btnList_Click(object sender, EventArgs e)
        {
            try
            {
                BindItemDetails();
                if (grvItemDetails.Rows.Count > 0)
                    grvItemDetails.PageIndex = 0;
            }
            catch (Exception)
            {

                throw;
            }
        }
        protected void grvItemDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grvItemDetails.PageIndex = e.NewPageIndex;
                BindItemDetails();
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void ucSupplierPartNumber_SearchImageClicked(object sender, EventArgs e)
        {
            try
            {
                var txtItemCode = (TextBox)grvItemDetails.FooterRow.FindControl("txtNewItemCode");
                var txtNewPartNo = (TextBox)grvItemDetails.FooterRow.FindControl("txtNewPartNo");
                txtItemCode.Text = Session["ItemCode"].ToString();
                txtNewPartNo.Text = Session["SupplierPartNumber"].ToString();
            }
            catch (Exception)
            {

            }
        }

        protected void drpSupplierLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void grvItemDetails_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                var User_SupplierLine = (ItemCodePartNumber)e.Row.FindControl("user");
                var drpFooterBranch = (DropDownList)e.Row.FindControl("drpBranch");

                User_SupplierLine.SupplierLine = drpSupplierLine.SelectedValue;
                User_SupplierLine.SupplierDesc = drpSupplierLine.SelectedItem.Text;
                drpFooterBranch.DataSource = Item.GetGlBranch(Session["BranchCode"].ToString());
                drpFooterBranch.DataTextField = "BankName";
                drpFooterBranch.DataValueField = "BankCode";
                drpFooterBranch.DataBind();
                drpFooterBranch.Enabled = false;
            }
        }

        protected void grvItemDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grvItemDetails.EditIndex = e.NewEditIndex;
            BindItemDetails();
        }

        protected void grvItemDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grvItemDetails.EditIndex = -1;
            BindItemDetails();
        }

        protected void grvItemDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = (GridViewRow)grvItemDetails.Rows[e.RowIndex];
            var ItemCode = (TextBox)row.FindControl("txtEditItemCode");
            var Branch = (TextBox)row.FindControl("txtEditBranch");
            var lPrice = (TextBox)row.FindControl("txtEditlistPrice");
            var cPrice = (TextBox)row.FindControl("txtEditCostPrice");
            var sPrice = (TextBox)row.FindControl("txtEditSellingPrice");

            BIP.UpdateBranchItemPrice(null, ItemCode.Text, null, Branch.Text, lPrice.Text, cPrice.Text, sPrice.Text);
            grvItemDetails.EditIndex = -1;
            BindItemDetails();
        }

        protected void grvItemDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Insert" && Page.IsValid)
            {
                var Itemcode = (TextBox)grvItemDetails.FooterRow.FindControl("txtNewItemCode");
                var branch = (DropDownList)grvItemDetails.FooterRow.FindControl("drpBranch");
                var lPrice = (TextBox)grvItemDetails.FooterRow.FindControl("txtNewlistPrice");
                var cPrice = (TextBox)grvItemDetails.FooterRow.FindControl("txtNewcostPrice");
                var sPrice = (TextBox)grvItemDetails.FooterRow.FindControl("txtNewsellingPrice");
                //var BalanceQnt = (TextBox)grvItemDetails.FooterRow.FindControl("txtBalance_Quantity");
                //var MaxQnt = (TextBox)grvItemDetails.FooterRow.FindControl("txtMaximum_Quantity");
                //Item.AddNewItemLocation(Itemcode.Text, branch.SelectedValue, Aisle.Text, Row.Text, Bin.Text, MaxQnt.Text);
                BIP.AddNewBranchItemPriceOld(null, Itemcode.Text, null, branch.SelectedValue, lPrice.Text, cPrice.Text, sPrice.Text);
                BindItemDetails();
            }
        }
        
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnReport_Click", "Inside btnReport_Click");
            try
            {
                hdnSupplierLine.Value = drpSupplierLine.SelectedValue;
                Server.Execute("BranchItemPriceReport.aspx");
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
                Response.Redirect("BranchItemPrice_Old.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}
