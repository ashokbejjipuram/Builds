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
    public partial class SaleTax : System.Web.UI.Page
    {
        List<DropDownListValue> oList = new List<DropDownListValue>();
        ImpalLibrary oCommon = new ImpalLibrary();
        SalesTaxes STax = new SalesTaxes();
        //        GLGroups GlGroup = new GLGroups();
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
                GV_GLMaster.DataSource = STax.GetAllSalesTexes(drpSTDesc.SelectedValue);
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
                drpSTDesc.DataSource = STax.GetAllSalesTexes();   //.GetAllGroup();
                drpSTDesc.DataTextField = "SalesTaxDescription";
                drpSTDesc.DataValueField = "SalesTaxCode";
                drpSTDesc.DataBind();
                drpSTDesc.Items.Insert(0, "ALL");
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
                TextBox Desc = (TextBox)GV_GLMaster.FooterRow.FindControl("txtNewDescription");
                RadioButton Central = (RadioButton)GV_GLMaster.FooterRow.FindControl("rbCentralIndicator");
                string _Central = string.Empty;
                if (Central.Checked)
                    _Central = "C";
                else
                    _Central = "L";
                TextBox Percentage = (TextBox)GV_GLMaster.FooterRow.FindControl("txtNewPercentage");
                DropDownList CalInd = (DropDownList)GV_GLMaster.FooterRow.FindControl("ddCalInd");
                DropDownList ddlST = (DropDownList)GV_GLMaster.FooterRow.FindControl("ddlST");
                STax.AddNewSalesTax(Desc.Text, _Central, Percentage.Text, CalInd.SelectedValue, ddlST.SelectedValue);

                //STax.AddNewGLMaster(GLGroupCode.SelectedValue, GLDesc.Text, _GLCreditIndicator);

                BindGrid();
            }



        }

        //protected void ddGLGroup_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Response.Write("testig");
        //}
        //protected void btnReport_Click(object sender, EventArgs e)
        //{
        //    //Response.Redirect("GLMasterReport.aspx");
        //}
        //protected void GV_GLMaster_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}
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


                var ddlCalInd = (DropDownList)e.Row.FindControl("ddCalInd");
                var ddlst = (DropDownList)e.Row.FindControl("ddlST");

                oList = oCommon.GetDropDownListValues("CalculationIndicator");
                ddlCalInd.DataSource = oList;
                ddlCalInd.DataTextField = "DisplayText";
                ddlCalInd.DataValueField = "DisplayValue";
                ddlCalInd.DataBind();

                oList = oCommon.GetDropDownListValues("SalesTaxType");
                ddlst.DataSource = oList;
                ddlst.DataTextField = "DisplayText";
                ddlst.DataValueField = "DisplayValue";
                ddlst.DataBind();

                //var ddGLGroup = (DropDownList)e.Row.FindControl("ddGLGroup");

                //ddGLGroup.DataSource = GlGroup.GetAllGroup();
                //ddGLGroup.DataTextField = "Description";
                //ddGLGroup.DataValueField = "GLCode";
                //ddGLGroup.DataBind();

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
            STax.UpdateSalesTax(txtDescription.Text, null, null, null, StCode.Text);

            //Glmaster.UpdateGLMaster(lblGLMasterCode.Text, null, txtDescription.Text, null);
            GV_GLMaster.EditIndex = -1;
            BindGrid();
        }
        protected void drpSTDesc_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
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

        #region Report button click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnReport_Click", "Inside btnReport_Click");
            try
            {
                Server.Execute("SaleTaxReport.aspx");
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion


    }
}
