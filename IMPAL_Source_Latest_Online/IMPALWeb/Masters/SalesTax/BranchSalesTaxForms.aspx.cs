using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using IMPALLibrary.Common;

namespace IMPALWeb
{
    public partial class BranchSalesTaxForms : System.Web.UI.Page
    {
        List<DropDownListValue> oList = new List<DropDownListValue>();
        ImpalLibrary oCommon = new ImpalLibrary();
        IMPALLibrary.BranchSalesTaxForms BSTax = new IMPALLibrary.BranchSalesTaxForms();
        Branches Branch = new Branches();

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["RoleCode"] == "BEDP")
            {
                GV_GLMaster.ShowFooter = false;
                int EditButtonIndex = GV_GLMaster.Columns.Count - 1;
                GV_GLMaster.Columns[EditButtonIndex].Visible = false;
            }

            if (!IsPostBack)
            {
                LoadDropDown();
                BindGrid();
            }
        }

        private void BindGrid()
        {
            try
            {
                GV_GLMaster.DataSource = BSTax.GetAllBranchSalesTexes(drpBSTDesc.SelectedValue);
                GV_GLMaster.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void LoadDropDown()
        {
            try
            {
                drpBSTDesc.DataSource = BSTax.GetAllBranchSalesTexes();    //.GetAllGroup();
                drpBSTDesc.DataTextField = "Serialnumber";
                drpBSTDesc.DataValueField = "Serialnumber";
                drpBSTDesc.DataBind();
                drpBSTDesc.Items.Insert(0, "ALL");
            }
            catch (Exception)
            {
                throw;
            }
        }


        protected void GV_GLMaster_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Insert" && Page.IsValid)
            {
                RadioButton Receive = (RadioButton)GV_GLMaster.FooterRow.FindControl("rbReceiveIndicator");
                string _Receive  = string.Empty;
                if (Receive.Checked)
                    _Receive = "R";
                else
                    _Receive = "I";

                RadioButton Supplier = (RadioButton)GV_GLMaster.FooterRow.FindControl("rbSupplierIndicator");
                string _Supplier = string.Empty;
                if (Supplier.Checked)
                    _Supplier = "S";
                else
                    _Supplier = "D";

                DropDownList ddBranch = (DropDownList)GV_GLMaster.FooterRow.FindControl("drpBranch");
                TextBox stForm = (TextBox)GV_GLMaster.FooterRow.FindControl("txtNewSTForm");
                /*DropDownList ddlST = (DropDownList)GV_GLMaster.FooterRow.FindControl("ddlST");*/
                BSTax.AddNewBranchSalesTax(ddBranch.SelectedValue, null, _Supplier, _Receive, stForm.Text);  
                BindGrid();
            }
        }


        protected void GV_GLMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV_GLMaster.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        protected void GV_GLMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GV_GLMaster.EditIndex = -1;
            BindGrid();
        }
        protected void GV_GLMaster_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                var drpFooterBranch = (DropDownList)e.Row.FindControl("drpBranch");
                LoadDropDownLists<Branch>(Branch.GetAllBranch(), drpFooterBranch, "BranchCode", "BranchName", true, "");

                //var ddlCalInd = (DropDownList)e.Row.FindControl("ddCalInd");
                //var ddlst = (DropDownList)e.Row.FindControl("ddlST");

                //oList = oCommon.GetDropDownListValues("CalculationIndicator");
                //ddlCalInd.DataSource = oList;
                //ddlCalInd.DataTextField = "DisplayText";
                //ddlCalInd.DataValueField = "DisplayValue";
                //ddlCalInd.DataBind();

                //oList = oCommon.GetDropDownListValues("SalesTaxType");
                //ddlst.DataSource = oList;
                //ddlst.DataTextField = "DisplayText";
                //ddlst.DataValueField = "DisplayValue";
                //ddlst.DataBind();
                

            }
        }
        protected void GV_GLMaster_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GV_GLMaster.EditIndex = e.NewEditIndex;
            BindGrid();
        }
        protected void GV_GLMaster_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = (GridViewRow)GV_GLMaster.Rows[e.RowIndex];
            
            var StCode = (Label)row.FindControl("lblSTCode");
            var txtDescription = (TextBox)row.FindControl("txtDescription");
            var lblBranch = (Label)row.FindControl("lblBranch");
            var lblSrno = (Label)row.FindControl("lblSrNo");
            var txtstForm = (TextBox)row.FindControl("txtSTForm");

            BSTax.UpdateBranchSalesTax(lblBranch.Text, lblSrno.Text, null, null, txtstForm.Text);


            //Glmaster.UpdateGLMaster(lblGLMasterCode.Text, null, txtDescription.Text, null);
            GV_GLMaster.EditIndex = -1;
            BindGrid();
        }
        protected void drpBSTDesc_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void drpBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindGrid();
            }
            catch (Exception)
            {

                throw;
            }
        }

       
        private void LoadDropDownLists<T>(List<T> ListData, DropDownList DDlDropDown, string value_field, string text_field, bool bselect, string DefaultText)
        {
            try
            {
                DDlDropDown.DataSource = ListData;
                DDlDropDown.DataTextField = text_field;
                DDlDropDown.DataValueField = value_field;
                DDlDropDown.DataBind();

                ListItem li = new ListItem();
                li.Text = "";
                li.Value = "";

                if (bselect.Equals(true))
                {
                    DDlDropDown.Items.Insert(0, DefaultText);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void GV_GLMaster_DataBound(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow grd in GV_GLMaster.Rows)
                {

                    var lblCalInd = (Label)grd.FindControl("lblCalInd");
                    if (lblCalInd != null)
                        lblCalInd.Text = GetText(lblCalInd.Text, oCommon.GetDropDownListValues("CalculationIndicator"));
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private string GetText(string textvalue, List<DropDownListValue> list)
        {
            string Test = string.Empty;

            try
            {
                DropDownListValue od = list.Find(p => p.DisplayValue == textvalue);
                Test = od.DisplayText;
            }
            catch (Exception)
            {

                throw;
            }

            return Test;
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            //string code = drpBSTDesc.SelectedValue;
            //Response.Redirect("BranchSalesTaxFormsReport.aspx?Code=" + code);
            Server.Execute("BranchSalesTaxFormsReport.aspx");

        }

    }
}
