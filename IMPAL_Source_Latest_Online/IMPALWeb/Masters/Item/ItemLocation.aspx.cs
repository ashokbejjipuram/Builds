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
    public partial class ItemLocation : System.Web.UI.Page
    {
        ItemLocationDetails Item = new ItemLocationDetails();
        public string supplierLine_value = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
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

                    List<SupplierCode> SupplierLineList = Item.GetSupplierCode();
                    drpSupplierLine.DataSource = SupplierLineList;
                    drpSupplierLine.DataTextField = "Supplier_Name";
                    drpSupplierLine.DataValueField = "Supplier_Code";
                    drpSupplierLine.DataBind();
                    drpSupplierLine.SelectedIndex = 0;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        public void BindItemDetails()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                grvItemDetails.DataSource = Item.GetItemLocDetails(drpSupplierLine.SelectedValue, txtSupplierPartNo.Text, Session["BranchCode"].ToString());
                grvItemDetails.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }        

        protected void btnList_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                BindItemDetails();
                if (grvItemDetails.Rows.Count > 0)
                    grvItemDetails.PageIndex = 0;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void grvItemDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                grvItemDetails.PageIndex = e.NewPageIndex;
                BindItemDetails();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        #region drpSupplierLine_SelectedIndexChanged
        protected void drpSupplierLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {                
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        protected void user_SearchImageClicked(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                var ddlCustomer = (TextBox)grvItemDetails.FooterRow.FindControl("txtItemCode");
                var txtSupplierPartNo = (TextBox)grvItemDetails.FooterRow.FindControl("txtSuppPartNo");
                ddlCustomer.Text = Session["ItemCode"].ToString();
                txtSupplierPartNo.Text = Session["SupplierPartNumber"].ToString();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void grvItemDetails_RowCreated(object sender, GridViewRowEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
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
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void grvItemDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                grvItemDetails.EditIndex = e.NewEditIndex;
                BindItemDetails();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void grvItemDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = (GridViewRow)grvItemDetails.Rows[e.RowIndex];
            var Itemcode = (TextBox)row.FindControl("txtEditItemCode");
            var branch = (TextBox)row.FindControl("txtEditBranch");
            var Aisle = (TextBox)row.FindControl("txtEditAisle");
            var Row = (TextBox)row.FindControl("txtEditRow");
            var Bin = (TextBox)row.FindControl("txtEditBin");
            var BalanceQnt = (TextBox)row.FindControl("txtEditBalance_Quantity");
            var MaxQnt = (TextBox)row.FindControl("txtEditMaximum_Quantity");

            Item.UpdateItemLocation(Itemcode.Text, branch.Text, Aisle.Text, Row.Text, Bin.Text, MaxQnt.Text);
            grvItemDetails.EditIndex = -1;
            BindItemDetails();
        }

        protected void grvItemDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                grvItemDetails.EditIndex = -1;
                BindItemDetails();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void grvItemDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (e.CommandName == "Insert" && Page.IsValid)
                {
                    var Itemcode = (TextBox)grvItemDetails.FooterRow.FindControl("txtItemCode");
                    var branch = (DropDownList)grvItemDetails.FooterRow.FindControl("drpBranch");
                    var Aisle = (TextBox)grvItemDetails.FooterRow.FindControl("txtAisle");
                    var Row = (TextBox)grvItemDetails.FooterRow.FindControl("txtRow");
                    var Bin = (TextBox)grvItemDetails.FooterRow.FindControl("txtBin");
                    var BalanceQnt = (TextBox)grvItemDetails.FooterRow.FindControl("txtBalance_Quantity");
                    var MaxQnt = (TextBox)grvItemDetails.FooterRow.FindControl("txtMaximum_Quantity");

                    if (Item.CheckItemLocation(Itemcode.Text, Aisle.Text, Row.Text, Bin.Text, branch.SelectedValue) == 0)
                    {
                        Item.AddNewItemLocation(Itemcode.Text, branch.SelectedValue, Aisle.Text, Row.Text, Bin.Text, MaxQnt.Text);
                        BindItemDetails();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record already exists');", true);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Session["SuppLine"] = drpSupplierLine.SelectedValue.ToString();
                Session["SuppPartNo"] = txtSupplierPartNo.Text;
                Response.Redirect("ItemLocationReport.aspx");
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
                Response.Redirect("ItemLocation.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}
