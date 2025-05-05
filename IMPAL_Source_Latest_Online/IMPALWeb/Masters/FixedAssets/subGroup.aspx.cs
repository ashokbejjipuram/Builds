using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;

namespace IMPALWeb.Masters.FixedAssets
{
    public partial class subGroup : System.Web.UI.Page
    {
        GLFixedAssetGroups objAssetGroup = new GLFixedAssetGroups();
        chartAccCode objChartAcc = new chartAccCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if ((string)Session["RoleCode"] == "BEDP")
                {
                    GV_GLMaster.ShowFooter = false;
                    int EditButtonIndex = GV_GLMaster.Columns.Count - 1;
                    GV_GLMaster.Columns[EditButtonIndex].Visible = false;
                }

                if (!IsPostBack)
                {

                    LoadDropDownLists<GLFixedAssetGroup>(objAssetGroup.GetAllGLFixedGroups(), drpAssertGroup, "GLCLCode", "GLCLDescription", true, "ALL");
                    LoadDropDownLists<GLFixedAssetSubGroup>(objAssetGroup.GetAllGLFixedSubGroups(""), drpAssertSubGroup, "GLCLSubCode", "GLCLSubDescription", true, "ALL");
                    BindGrid();
                }
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

        private void BindGrid()
        {
            try
            {
                string GroupCode = string.Empty;
                string GroupSubCode = string.Empty;
                GroupCode = (drpAssertGroup.SelectedValue != "ALL") ? drpAssertGroup.SelectedValue : "ALL";
                GroupCode = (drpAssertSubGroup.SelectedValue != "ALL") ? drpAssertSubGroup.SelectedValue : "ALL";
                GV_GLMaster.DataSource = objAssetGroup.ViewFixedAssetSubGroup(drpAssertGroup.SelectedValue, drpAssertSubGroup.SelectedValue);
                GV_GLMaster.DataBind();
            }
            catch (Exception)
            {

                throw;
            }
        }


        protected void GV_GLMaster_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            try
            {
                if (e.CommandName == "Insert" && Page.IsValid)
                {
                    SubGroupDetails SubGroupDts = new SubGroupDetails();
                    var drpFtGroupCode = (DropDownList)GV_GLMaster.FooterRow.FindControl("drpFtGroupCode");
                    var drpFtcode = (DropDownList)GV_GLMaster.FooterRow.FindControl("drpFtcode");
                    var txtFtDescription = (TextBox)GV_GLMaster.FooterRow.FindControl("txtFtDescription");
                    var txtFtBookDesc = (TextBox)GV_GLMaster.FooterRow.FindControl("txtFtBookDesc");
                    var txtFtItDesc = (TextBox)GV_GLMaster.FooterRow.FindControl("txtFtItDesc");
                    var drpFtGlMain = (DropDownList)GV_GLMaster.FooterRow.FindControl("drpFtGlMain");
                    var drpFtGlSub = (DropDownList)GV_GLMaster.FooterRow.FindControl("drpFtGlSub");
                    var drpFtGlAccount = (DropDownList)GV_GLMaster.FooterRow.FindControl("drpFtGlAccount");


                    SubGroupDts.FA_Group_Code = drpFtGroupCode.SelectedValue;
                    SubGroupDts.FA_Sub_Group_Code = drpFtcode.SelectedValue;
                    SubGroupDts.FA_Sub_Group_Description = txtFtDescription.Text;
                    SubGroupDts.Depreciation_Percentage = txtFtBookDesc.Text;
                    SubGroupDts.Gl_Main_Code = drpFtGlMain.SelectedValue;
                    SubGroupDts.Gl_Sub_Code = drpFtGlSub.SelectedValue;
                    SubGroupDts.Gl_Account_Code = drpFtGlAccount.SelectedValue;
                    SubGroupDts.IT_Depreciation_Percentage = txtFtItDesc.Text;
                    SubGroupDts.GL_Main_Description = "";
                    SubGroupDts.Gl_Sub_Description = "";
                    SubGroupDts.Description = "";

                    objAssetGroup.AddNewFixedAssetSubGroup(SubGroupDts);

                    BindGrid();
                }
            }
            catch (Exception)
            {

                throw;
            }



        }

        protected void GV_GLMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GV_GLMaster.PageIndex = e.NewPageIndex;
                BindGrid();
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void GV_GLMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                GV_GLMaster.EditIndex = -1;
                BindGrid();
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void GV_GLMaster_RowCreated(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Footer)
                {

                    var drpFtGroupCode = (DropDownList)e.Row.FindControl("drpFtGroupCode");
                    var drpFtcode = (DropDownList)e.Row.FindControl("drpFtcode");
                    var drpFtGlMain = (DropDownList)e.Row.FindControl("drpFtGlMain");
                    var drpFtGlSub = (DropDownList)e.Row.FindControl("drpFtGlSub");
                    var drpFtGlAccount = (DropDownList)e.Row.FindControl("drpFtGlAccount");
                    LoadDropDownLists<GLFixedAssetGroup>(objAssetGroup.GetAllGLFixedGroups(), drpFtGroupCode, "GLCLCode", "GLCLDescription", true, "");
                    LoadDropDownLists<GLFixedAssetSubGroup>(objAssetGroup.GetAllGLFixedSubGroups(drpFtGroupCode.SelectedValue), drpFtcode, "GLCLSubCode", "GLCLSubDescription", true, "");
                    LoadDropDownLists<Glmain>(objChartAcc.GetGlmain(""), drpFtGlMain, "GlMainCode", "GlMainName", true, "");

                }
                if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowState == DataControlRowState.Edit)
                {
                    var lblEditGroupCode = (Label)e.Row.FindControl("lblEditGroupCode");
                    var lblEditcode = (Label)e.Row.FindControl("lblEditcode");
                    var drpGlMain = (DropDownList)e.Row.FindControl("drpGlMain");
                    var drpGlSub = (DropDownList)e.Row.FindControl("drpGlSub");
                    var drpGlAccount = (DropDownList)e.Row.FindControl("drpGlAccount");
                    LoadDropDownLists<Glmain>(objChartAcc.GetGlmain(""), drpGlMain, "GlMainCode", "GlMainName", true, "");

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void GV_GLMaster_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                GV_GLMaster.EditIndex = e.NewEditIndex;
                BindGrid();
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void GV_GLMaster_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)GV_GLMaster.Rows[e.RowIndex];
                SubGroupDetails SubGroupDts = new SubGroupDetails();
                var lblEditGroupCode = (Label)row.FindControl("lblEditGroupCode");
                var lblEditcode = (Label)row.FindControl("lblEditcode");
                var txtDescription = (TextBox)row.FindControl("txtDescription");
                var txtBookDesc = (TextBox)row.FindControl("txtBookDesc");
                var txtItDesc = (TextBox)row.FindControl("txtItDesc");
                var drpGlMain = (DropDownList)row.FindControl("drpGlMain");
                var drpGlSub = (DropDownList)row.FindControl("drpGlSub");
                var drpGlAccount = (DropDownList)row.FindControl("drpGlAccount");


                SubGroupDts.FA_Group_Code = lblEditGroupCode.Text;
                SubGroupDts.FA_Sub_Group_Code = lblEditcode.Text;
                SubGroupDts.FA_Sub_Group_Description = txtDescription.Text;
                SubGroupDts.Depreciation_Percentage = txtBookDesc.Text;
                SubGroupDts.Gl_Main_Code = drpGlMain.SelectedValue;
                SubGroupDts.Gl_Sub_Code = drpGlSub.SelectedValue;
                SubGroupDts.Gl_Account_Code = drpGlAccount.SelectedValue;
                SubGroupDts.IT_Depreciation_Percentage = txtItDesc.Text;
                SubGroupDts.GL_Main_Description = "";
                SubGroupDts.Gl_Sub_Description = "";
                SubGroupDts.Description = "";

                objAssetGroup.UpdateFixedAssetSubGroup(SubGroupDts);

                GV_GLMaster.EditIndex = -1;
                BindGrid();
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void drpAssertGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string strGroupCode = drpAssertGroup.SelectedValue;
                if (strGroupCode == "ALL")
                    strGroupCode = "";
                LoadDropDownLists<GLFixedAssetSubGroup>(objAssetGroup.GetAllGLFixedSubGroups(strGroupCode), drpAssertSubGroup, "GLCLSubCode", "GLCLSubDescription", true, "ALL");
            }
            catch (Exception)
            {

                throw;
            }
        }


        protected void BtnSearct_Click(object sender, ImageClickEventArgs e)
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

        //protected void drpGroupCode_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        GridViewRow gvr = (GridViewRow)((DropDownList)sender).Parent.Parent;
        //        var drpGroupCode = (DropDownList)gvr.FindControl("drpGroupCode");
        //        var drpcode = (DropDownList)gvr.FindControl("drpcode");
        //        LoadDropDownLists<GLFixedAssetSubGroup>(objAssetGroup.GetAllGLFixedSubGroups(drpGroupCode.SelectedValue), drpcode, "GLCLSubCode", "GLCLSubDescription", true, "");
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        protected void drpFtGroupCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var drpFtGroupCode = (DropDownList)GV_GLMaster.FooterRow.FindControl("drpFtGroupCode");
                var drpFtcode = (DropDownList)GV_GLMaster.FooterRow.FindControl("drpFtcode");
                LoadDropDownLists<GLFixedAssetSubGroup>(objAssetGroup.GetAllGLFixedSubGroups(drpFtGroupCode.SelectedValue), drpFtcode, "GLCLSubCode", "GLCLSubDescription", true, "");
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void drpGlMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow gvr = (GridViewRow)((DropDownList)sender).Parent.Parent;
                var drpGlMain = (DropDownList)gvr.FindControl("drpGlMain");
                var drpGlSub = (DropDownList)gvr.FindControl("drpGlSub");
                LoadDropDownLists<GlSub>(objChartAcc.GetGlSub(drpGlMain.SelectedValue), drpGlSub, "GlSubCode", "GlSubName", true, "");
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void drpFtGlMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var drpFtGlMain = (DropDownList)GV_GLMaster.FooterRow.FindControl("drpFtGlMain");
                var drpFtGlSub = (DropDownList)GV_GLMaster.FooterRow.FindControl("drpFtGlSub");
                LoadDropDownLists<GlSub>(objChartAcc.GetGlSub(drpFtGlMain.SelectedValue), drpFtGlSub, "GlSubCode", "GlSubName", true, "");
            }
            catch (Exception)
            {

                throw;
            }

        }

        protected void drpGlSub_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow gvr = (GridViewRow)((DropDownList)sender).Parent.Parent;
                var drpGlMain = (DropDownList)gvr.FindControl("drpGlMain");
                var drpGlSub = (DropDownList)gvr.FindControl("drpGlSub");
                var drpGlAccount = (DropDownList)gvr.FindControl("drpGlAccount");
                LoadDropDownLists<GlAccount>(objChartAcc.GetGlAccount(drpGlMain.SelectedValue, drpGlSub.SelectedValue), drpGlAccount, "GlAccountCode", "GlAccountName", true, "");
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void drpFtGlSub_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                var drpFtGlMain = (DropDownList)GV_GLMaster.FooterRow.FindControl("drpFtGlMain");
                var drpFtGlSub = (DropDownList)GV_GLMaster.FooterRow.FindControl("drpFtGlSub");
                var drpFtGlAccount = (DropDownList)GV_GLMaster.FooterRow.FindControl("drpFtGlAccount");
                LoadDropDownLists<GlAccount>(objChartAcc.GetGlAccount(drpFtGlMain.SelectedValue, drpFtGlSub.SelectedValue), drpFtGlAccount, "GlAccountCode", "GlAccountName", true, "");

            }
            catch (Exception)
            {

                throw;
            }
        }

        #region Report button click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnReport_Click", "Inside btnReport_Click");
            try
            {
                //string strGroupCode = drpAssertGroup.SelectedValue;
                Server.Execute("SubGroupReport.aspx");
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

    }
}
