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
    public partial class ChartOfAccountMaster : System.Web.UI.Page
    {
        GLMasters Glmaster = new GLMasters();
        GLSubMasters Glsub = new GLSubMasters();
        GLGroups GlGroup = new GLGroups();
        ChartOfAccounts caGroup = new ChartOfAccounts();
        GLAccMasters glacc = new GLAccMasters();
        DropGlBranchClassification glAccBranch = new DropGlBranchClassification();

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
                GV_GLMaster.DataSource = caGroup.GetAllChartOfAccount(drpClassificationGrp.SelectedValue); //Glmaster.GetAllGLMasters(drpGlGroupDesc.SelectedValue);
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
                drpClassificationGrp.DataSource =  caGroup.GetAllDrpChartOfAccount();//  .GetAllChartOfAccount();
                drpClassificationGrp.DataTextField = "GL_Classification_Description";
                drpClassificationGrp.DataValueField = "GL_Classification_Code";
                drpClassificationGrp.DataBind();
                drpClassificationGrp.Items.Insert(0, "");
            }
            catch (Exception)
            {

                throw;
            }
        }
        protected void drpClassificationGrp_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void drpGLClassification_SelectedIndexChanged(object sender, EventArgs e)
        {
            var drpGLC = (DropDownList)GV_GLMaster.FooterRow.FindControl("drpGLClassification");
            var drpGLGroup = (DropDownList)GV_GLMaster.FooterRow.FindControl("drpGLGroup");
            var drpGLMain = (DropDownList)GV_GLMaster.FooterRow.FindControl("drpGLMain");
            var drpGLSub = (DropDownList)GV_GLMaster.FooterRow.FindControl("drpGLSub");
            var drpGLAccount = (DropDownList)GV_GLMaster.FooterRow.FindControl("drpGLAccount");


            drpGLGroup.DataSource = GlGroup.GetAllGroupEmpty(drpGLC.SelectedValue);
            drpGLGroup.DataTextField = "Description";
            drpGLGroup.DataValueField = "GLCode";
            drpGLGroup.DataBind();

            drpGLMain.Items.Clear();
            drpGLSub.Items.Clear();
            drpGLAccount.Items.Clear();
            

        }
        protected void drpGLGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            var drpGLGroup = (DropDownList)GV_GLMaster.FooterRow.FindControl("drpGLGroup");
            var drpGLMain = (DropDownList)GV_GLMaster.FooterRow.FindControl("drpGLMain");
            var drpGLSub = (DropDownList)GV_GLMaster.FooterRow.FindControl("drpGLSub");
            var drpGLAccount = (DropDownList)GV_GLMaster.FooterRow.FindControl("drpGLAccount");


            drpGLMain.DataSource = Glmaster.GetAllGLMasters(drpGLGroup.SelectedValue);
            drpGLMain.DataTextField = "Description";
            drpGLMain.DataValueField = "GLMasterCode";
            drpGLMain.DataBind();
            drpGLMain.Items.Insert(0, " ");

            drpGLSub.Items.Clear();
            drpGLAccount.Items.Clear();
        }
        protected void drpGLMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            var drpGLMain = (DropDownList)GV_GLMaster.FooterRow.FindControl("drpGLMain");
            var drpGLSub = (DropDownList)GV_GLMaster.FooterRow.FindControl("drpGLSub");
            var drpGLAccount = (DropDownList)GV_GLMaster.FooterRow.FindControl("drpGLAccount");

            drpGLSub.DataSource = Glsub.GetAllGlSubGroup(drpGLMain.SelectedValue); //  Glmaster.GetAllGLMasters(drpGLMain.SelectedValue);
            drpGLSub.DataTextField = "GLSubMasterDesc";
            drpGLSub.DataValueField = "GLSubMasterCode";
            drpGLSub.DataBind();
            drpGLSub.Items.Insert(0, " ");

            drpGLAccount.Items.Clear();

        }
        protected void drpGLSub_SelectedIndexChanged(object sender, EventArgs e)
        {
            var drpGLClassification = (DropDownList)GV_GLMaster.FooterRow.FindControl("drpGLClassification");
            var drpGLGroup = (DropDownList)GV_GLMaster.FooterRow.FindControl("drpGLGroup");
            var drpGLMain = (DropDownList)GV_GLMaster.FooterRow.FindControl("drpGLMain");
            var drpGLSub = (DropDownList)GV_GLMaster.FooterRow.FindControl("drpGLSub");
            var drpGLAccount = (DropDownList)GV_GLMaster.FooterRow.FindControl("drpGLAccount");



            drpGLAccount.DataSource = glacc.GetAllGlAccSubGrp(drpGLClassification.SelectedValue, drpGLGroup.SelectedValue, drpGLMain.SelectedValue ,drpGLSub.SelectedValue);// Glmaster.GetAllGLMasters(drpGLSub.SelectedValue);
            drpGLAccount.DataTextField = "GLAccDesc";
            drpGLAccount.DataValueField = "GLAccCode";
            drpGLAccount.DataBind();
            drpGLAccount.Items.Insert(0, "");
        }


        protected void drpGLAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            var drpGLC = (DropDownList)GV_GLMaster.FooterRow.FindControl("drpGLClassification");
            var drpGLGroup = (DropDownList)GV_GLMaster.FooterRow.FindControl("drpGLGroup");
            var drpGLMain = (DropDownList)GV_GLMaster.FooterRow.FindControl("drpGLMain");
            var drpGLSub = (DropDownList)GV_GLMaster.FooterRow.FindControl("drpGLSub");
            var drpGLAccount = (DropDownList)GV_GLMaster.FooterRow.FindControl("drpGLAccount");
            var drpGLBranch = (DropDownList)GV_GLMaster.FooterRow.FindControl("drpGLBranch");
            drpGLBranch.DataSource = caGroup.GetAllDrpChartOfAccountBranch(drpGLC.SelectedValue, drpGLGroup.SelectedValue, drpGLMain.SelectedValue, drpGLSub.SelectedValue);// Glmaster.GetAllGLMasters(drpGLSub.SelectedValue);
            drpGLBranch.DataTextField = "BranchName";
            drpGLBranch.DataValueField = "BranchCode";
            drpGLBranch.DataBind();
            drpGLBranch.Items.Insert(0, "");
        }

        protected void GV_GLMaster_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Insert" && Page.IsValid)
            {
                //DropDownList GLGroupCode = (DropDownList)GV_GLMaster.FooterRow.FindControl("ddGLGroup");
                var drpGLC = (DropDownList)GV_GLMaster.FooterRow.FindControl("drpGLClassification");
                var drpGLGroup = (DropDownList)GV_GLMaster.FooterRow.FindControl("drpGLGroup");
                var drpGLMain = (DropDownList)GV_GLMaster.FooterRow.FindControl("drpGLMain");
                var drpGLSub = (DropDownList)GV_GLMaster.FooterRow.FindControl("drpGLSub");
                var drpGLAccount = (DropDownList)GV_GLMaster.FooterRow.FindControl("drpGLAccount");

                caGroup.AddNewChartOfAccount(drpGLC.SelectedValue , drpGLSub.SelectedValue , drpGLMain.SelectedValue ,
                       drpGLGroup.SelectedValue ,drpGLAccount.SelectedValue );
                //TextBox GLDesc = (TextBox)GV_GLMaster.FooterRow.FindControl("txtNewDescription");
                //DropDownList GLGroupCode = (DropDownList)GV_GLMaster.FooterRow.FindControl("ddGLGroup");
                //RadioButton GLCreditIndicator = (RadioButton)GV_GLMaster.FooterRow.FindControl("rbCreditIndicator");
                //string _GLCreditIndicator = string.Empty;
                //if (GLCreditIndicator.Checked)
                //    _GLCreditIndicator = "CR";
                //else
                //    _GLCreditIndicator = "DR";
                //Glmaster.AddNewGLMaster(GLGroupCode.SelectedValue, GLDesc.Text, _GLCreditIndicator);

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

                var drpGLClassification = (DropDownList)e.Row.FindControl("drpGLClassification");
                var drpGLGroup = (DropDownList)e.Row.FindControl("drpGLGroup");
                var drpGLMain = (DropDownList)e.Row.FindControl("drpGLMain");
                var drpGLSub = (DropDownList)e.Row.FindControl("drpGLSub");
                var drpGLAccount = (DropDownList)e.Row.FindControl("drpGLAccount");
                
                drpGLClassification.DataSource = caGroup.GetAllDrpChartOfAccount();
                drpGLClassification.DataTextField = "GL_Classification_Description";
                drpGLClassification.DataValueField = "GL_Classification_Code";
                drpGLClassification.DataBind();

                drpGLGroup.DataSource = GlGroup.GetAllGroup();
                drpGLGroup.DataTextField = "Description";
                drpGLGroup.DataValueField = "GLCode";
                drpGLGroup.DataBind();

                drpGLMain.DataSource = Glmaster.GetAllGLMasters();
                drpGLMain.DataTextField = "Description";
                drpGLMain.DataValueField = "GLMasterCode";
                drpGLMain.DataBind();

                drpGLSub.DataSource = Glsub.GetAllGlSubGroup();
                drpGLSub.DataTextField = "GLSubMasterDesc";
                drpGLSub.DataValueField = "GLSubMasterCode";
                drpGLSub.DataBind();

                drpGLAccount.DataSource = glacc.GetAllGlAccSubGrp("","","","");//glacc.GetAllGlAccGroup();
                drpGLAccount.DataTextField = "GLAccDesc";
                drpGLAccount.DataValueField = "GLAccCode";
                drpGLAccount.DataBind();

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
            //var txtDescription = (TextBox)row.FindControl("txtDescription");
            //var lblGLMasterCode = (Label)row.FindControl("lblGLMasterCode");
            //Glmaster.UpdateGLMaster(lblGLMasterCode.Text, null, txtDescription.Text, null);
            GV_GLMaster.EditIndex = -1;
            BindGrid();
        }

        #region Report Button Click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnReport_Click", "Inside btnReport_Click");
            try
            {
                Server.Execute("ChartOfAccReport.aspx");
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion
    }
}
