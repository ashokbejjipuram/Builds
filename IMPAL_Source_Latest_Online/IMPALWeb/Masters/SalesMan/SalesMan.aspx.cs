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
    public partial class SaleMan : System.Web.UI.Page
    {
        List<DropDownListValue> oList = new List<DropDownListValue>();
        ImpalLibrary oCommon = new ImpalLibrary();
        SalesManMaster SMan = new SalesManMaster();
        //GLGroups GlGroup = new GLGroups();
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
               // GV_GLMaster.DataSource = SMan.GetAllSalesMan(); //STax.GetAllSalesTexes(drpSTDesc.SelectedValue);
                GV_GLMaster.DataSource = SMan.GetAllSalesMan(drpSTDesc.SelectedValue, Session["BranchCode"].ToString()); 
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
                drpSTDesc.DataSource = SMan.GetAllSalesMan(Session["BranchCode"].ToString());
                drpSTDesc.DataTextField = "SalesManName";
                drpSTDesc.DataValueField = "SalesManCode";
                drpSTDesc.DataBind();
                drpSTDesc.Items.Insert(0, "ALL");
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void drpSTDesc_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void GV_GLMaster_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Insert" && Page.IsValid)
            {
                //DropDownList ddBranch = (DropDownList)GV_GLMaster.FooterRow.FindControl("drpBranch");
                //TextBox Desc = (TextBox)GV_GLMaster.FooterRow.FindControl("txtNewDescription");
                //RadioButton Central = (RadioButton)GV_GLMaster.FooterRow.FindControl("rbCentralIndicator");
                //string _Central = string.Empty;
                //if (Central.Checked)
                //    _Central = "C";
                //else
                //    _Central = "L";
                //TextBox Percentage = (TextBox)GV_GLMaster.FooterRow.FindControl("txtNewPercentage");
                //DropDownList CalInd = (DropDownList)GV_GLMaster.FooterRow.FindControl("ddCalInd");
                //DropDownList ddlST = (DropDownList)GV_GLMaster.FooterRow.FindControl("ddlST");
                ////STax.AddNewSalesTax(Desc.Text, _Central, Percentage.Text,CalInd.SelectedValue, ddlST.SelectedValue);  

                var drpSName = (DropDownList)GV_GLMaster.FooterRow.FindControl("drpSName");
                var txtNewSMCode = (TextBox)GV_GLMaster.FooterRow.FindControl("txtNewSMCode");
                var txtNewDesignation = (TextBox)GV_GLMaster.FooterRow.FindControl("txtNewDesignation");
                var txtBranch = (TextBox)GV_GLMaster.FooterRow.FindControl("txtBranch");
                var txtNewStartDate = (TextBox)GV_GLMaster.FooterRow.FindControl("txtNewStartDate");
                var txtNewEndDate = (TextBox)GV_GLMaster.FooterRow.FindControl("txtNewEndDate");
                var ddlStatus = (DropDownList)GV_GLMaster.FooterRow.FindControl("ddlStatus");
                var lblBrcode = (Label)GV_GLMaster.FooterRow.FindControl("lblBrCode1");
                SMan.AddNewSalesMan(txtNewSMCode.Text, drpSName.SelectedValue, txtNewDesignation.Text, txtBranch.Text, txtNewStartDate.Text, txtNewEndDate.Text, ddlStatus.SelectedValue, lblBrcode.Text);
                BindGrid();
            }
        }
        protected void drpSName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //string glSubDescription_selectedValue = ddGLSubDescription.Items.FindByText(GLMainCode.SelectedItem.ToString()).Value;
                //ddGLSubDescription.SelectedValue = glSubDescription_selectedValue;
                //txtGlsubcode.Text = glSubDescription_selectedValue;
                var drpSName = (DropDownList)GV_GLMaster.FooterRow.FindControl("drpSName");
                List<SalesMans> salesmanlist = SMan.GetAllSalesMan(drpSName.SelectedValue, Session["BranchCode"].ToString());
                var txtNewSMCode = (TextBox)GV_GLMaster.FooterRow.FindControl("txtNewSMCode");
                var txtNewDesignation = (TextBox)GV_GLMaster.FooterRow.FindControl("txtNewDesignation");
                var txtBranch = (TextBox)GV_GLMaster.FooterRow.FindControl("txtBranch");
                var lblBrcode = (Label)GV_GLMaster.FooterRow.FindControl("lblBrCode1");
                if (salesmanlist.Count > 0)
                {
                    txtNewSMCode.Text = salesmanlist[0].SalesManCode;
                    txtNewDesignation.Text = salesmanlist[0].Designation;
                    txtBranch.Text = salesmanlist[0].Branch;
                    lblBrcode.Text = salesmanlist[0].BrCode;  
                    
                }
            }
            catch (Exception)
            {
                throw;
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
                var drpFooterSname = (DropDownList)e.Row.FindControl("drpSname");
                LoadDropDownLists<SalesMans>(SMan.GetAllSalesMan(Session["BranchCode"].ToString()), drpFooterSname, "SalesManCode", "SalesManName", true, "");
                var ddlStatus = (DropDownList)e.Row.FindControl("ddlStatus");
                oList = oCommon.GetDropDownListValues("SalesManStatus");
                ddlStatus.DataSource = oList;
                ddlStatus.DataTextField = "DisplayText";
                ddlStatus.DataValueField = "DisplayValue";
                ddlStatus.DataBind();
            }
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowState == DataControlRowState.Edit)
            {
                var ddlStatus = (DropDownList)e.Row.FindControl("ddlEditStatus");
                oList = oCommon.GetDropDownListValues("SalesManStatus");
                ddlStatus.DataSource = oList;
                ddlStatus.DataTextField = "DisplayText";
                ddlStatus.DataValueField = "DisplayValue";
                ddlStatus.DataBind();

            }


           
        }
        protected void GV_GLMaster_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GV_GLMaster.EditIndex = e.NewEditIndex;
            GridViewRow dr = GV_GLMaster.Rows[GV_GLMaster.EditIndex];
            BindGrid();
        }
        protected void GV_GLMaster_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = (GridViewRow)GV_GLMaster.Rows[e.RowIndex];

            var SCode = (Label)row.FindControl("lblSMCode");
            var Sname = (Label)row.FindControl("txtName");
            var Desig = (Label)row.FindControl("lblDesignation");
            var Branch = (Label)row.FindControl("lblBranch");
            var lblBrcode = (Label)row.FindControl("lblBrCode");
            var SDate = (TextBox)row.FindControl("txtStartDate");
            var EDate = (TextBox)row.FindControl("txtEndDate");
            var ddlStatus = (DropDownList)row.FindControl("ddlEditStatus");
            
            //            STax.UpdateSalesTax(txtDescription.Text, null, null, null, StCode.Text);  
            SMan.UpdateSalesMan(SCode.Text, Sname.Text , Desig.Text, Branch.Text, SDate.Text, EDate.Text, ddlStatus.SelectedValue,lblBrcode.Text  );
            
            GV_GLMaster.EditIndex = -1;
            BindGrid();
        }


        protected void GV_GLMaster_DataBound(object sender, EventArgs e)
        {
            try
            {


                foreach (GridViewRow grd in GV_GLMaster.Rows)
                {
                    var Status = (Label)grd.FindControl("lblStatus");
                    if (Status != null)
                        Status.Text = GetText(Status.Text.ToUpper(), oCommon.GetDropDownListValues("SalesManStatus"));
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
        private void LoadDropDownLists<T>(List<T> ListData, DropDownList DDlDropDown, string value_field, string text_field, bool bselect, string DefaultText)
        {
            try
            {
                DDlDropDown.DataSource = ListData;
                DDlDropDown.DataTextField = text_field;
                DDlDropDown.DataValueField = value_field;
                DDlDropDown.DataBind();
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

        protected void GV_GLMaster_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowState == DataControlRowState.Edit)
            {
                //var ddlStatus = (DropDownList)e.Row.FindControl("ddlEditStatus");
                //oList = oCommon.GetDropDownListValues("SalesManStatus");
                //ddlStatus.DataSource = oList;
                //ddlStatus.DataTextField = "DisplayText";
                //ddlStatus.DataValueField = "DisplayValue";
                //ddlStatus.DataBind();
            }
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            Server.Execute("SalesManReport.aspx");
           //string SalesManCode= drpSTDesc.SelectedValue;
           //Response.Redirect("SalesManReport.aspx?Code=" + SalesManCode);
        }

    }
}
