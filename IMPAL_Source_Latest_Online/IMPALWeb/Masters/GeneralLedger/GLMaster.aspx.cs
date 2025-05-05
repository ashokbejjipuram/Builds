using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;

namespace IMPALWeb
{
    public partial class GLMaster : System.Web.UI.Page
    {
        GLMasters Glmaster = new GLMasters();
        GLGroups GlGroup = new GLGroups();
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
                GV_GLMaster.DataSource = Glmaster.GetAllGLMasters(drpGlGroupDesc.SelectedValue);
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
                drpGlGroupDesc.DataSource = GlGroup.GetAllGroup();
                drpGlGroupDesc.DataTextField = "Description";
                drpGlGroupDesc.DataValueField = "GLCode";
                drpGlGroupDesc.DataBind();
                drpGlGroupDesc.Items.Insert(0, "ALL");
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
                TextBox GLDesc = (TextBox)GV_GLMaster.FooterRow.FindControl("txtNewDescription");
                DropDownList GLGroupCode = (DropDownList)GV_GLMaster.FooterRow.FindControl("ddGLGroup");
                RadioButton GLCreditIndicator = (RadioButton)GV_GLMaster.FooterRow.FindControl("rbCreditIndicator");
                string _GLCreditIndicator = string.Empty;
                if (GLCreditIndicator.Checked)
                    _GLCreditIndicator = "CR";
                else
                    _GLCreditIndicator = "DR";
                Glmaster.AddNewGLMaster(GLGroupCode.SelectedValue, GLDesc.Text, _GLCreditIndicator);

                BindGrid();
            }



        }

        protected void ddGLGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Write("testig");
        }
       
        protected void GV_GLMaster_SelectedIndexChanged(object sender, EventArgs e)
        {

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

                var ddGLGroup = (DropDownList)e.Row.FindControl("ddGLGroup");

                ddGLGroup.DataSource = GlGroup.GetAllGroup();
                ddGLGroup.DataTextField = "Description";
                ddGLGroup.DataValueField = "GLCode";
                ddGLGroup.DataBind();
                
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
            var txtDescription = (TextBox)row.FindControl("txtDescription");
            var lblGLMasterCode = (Label)row.FindControl("lblGLMasterCode");
            Glmaster.UpdateGLMaster(lblGLMasterCode.Text, null, txtDescription.Text, null);
            GV_GLMaster.EditIndex = -1;
            BindGrid();
        }
        protected void drpGlGroupDesc_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnReport_Click", "Report Button Clicked");
            try
            {
                Server.Execute("GLMasterReport.aspx");
                //var GLGroupCode = drpGlGroupDesc.SelectedValue;
                //Response.Redirect("GLMasterReport.aspx?GLGroupCode=" + GLGroupCode);
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

    }
}
