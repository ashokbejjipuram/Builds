using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;

namespace IMPALWeb
{
    public partial class GLGroup : System.Web.UI.Page
    {

        GLClassifications GlClass = new GLClassifications();
        GLGroups GlGroup = new GLGroups();
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["RoleCode"] == "BEDP")
            {
                GV_GLGroup.ShowFooter = false;
                int EditButtonIndex = GV_GLGroup.Columns.Count - 1;
                GV_GLGroup.Columns[EditButtonIndex].Visible = false;

            }
            if (!IsPostBack)
            {
                LoadDropdown();
                BindGrid();
            }
        }

        private void BindGrid()
        {
            GV_GLGroup.DataSource = GlGroup.GetAllGroup(drpGLClassification.SelectedValue);
            GV_GLGroup.DataBind();
        }

        private void LoadDropdown()
        {
            drpGLClassification.DataSource = GlClass.GetAllGLClassifications();
            drpGLClassification.DataTextField = "GLCLDescription";
            drpGLClassification.DataValueField = "GLCLCode";
            drpGLClassification.DataBind();
            drpGLClassification.Items.Insert(0, "ALL");

            

        }

        protected void drpGLClassification_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void GV_GLGroup_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GV_GLGroup.EditIndex = -1;
            BindGrid();
        }
        protected void GV_GLGroup_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Insert" && Page.IsValid)
            {
                var txtNewcode = (TextBox)GV_GLGroup.FooterRow.FindControl("txtNewGLCode");
                var txtNewDescription = (TextBox)GV_GLGroup.FooterRow.FindControl("txtNewDescription");
                var ddlGLClassification = (DropDownList)GV_GLGroup.FooterRow.FindControl("ddlGLClassification");
                GlGroup.AddNewGLGroup(txtNewcode.Text , txtNewDescription.Text, ddlGLClassification.SelectedValue);
                BindGrid();
            }


        }
        protected void GV_GLGroup_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {

                var drpFooterClassification = (DropDownList)e.Row.FindControl("ddlGLClassification");

                //drpFooterClassification.DataSource = GlClass.GetAllGLClassifications();
                drpFooterClassification.DataSource = GlClass.GetGlClassification_Master();
                drpFooterClassification.DataTextField = "GlClassificationDesc";
                drpFooterClassification.DataValueField = "GlClassificationCode";
                drpFooterClassification.DataBind();
                //drpFooterClassification.Items.Insert(0, " ");

            }
        }
        protected void GV_GLGroup_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GV_GLGroup.EditIndex = e.NewEditIndex;
            BindGrid();
        }
        protected void GV_GLGroup_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = (GridViewRow)GV_GLGroup.Rows[e.RowIndex];
            var lblGLCode = (Label)row.FindControl("lblGLCode");
            var txtDescription = (TextBox)row.FindControl("txtDescription");
            var lblGLClassification = (Label)row.FindControl("lblGLClassification");
            GlGroup.UpdateGLGroup(lblGLCode.Text, txtDescription.Text, lblGLClassification.Text);
            GV_GLGroup.EditIndex = -1;
            BindGrid();

        }
        protected void GV_GLGroup_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV_GLGroup.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnReport_Click", "Inside btnReport_Click");
            try
            {
                Server.Execute("GLGroupReport.aspx");
                //var GLClassificationCode = drpGLClassification.SelectedValue;
                //Response.Redirect("GLGroupReport.aspx?GLClassificationCode=" + GLClassificationCode);
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

    }
}
