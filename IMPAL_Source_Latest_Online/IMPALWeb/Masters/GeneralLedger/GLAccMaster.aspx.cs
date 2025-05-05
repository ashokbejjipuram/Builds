using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.ServiceLocation;
using IMPALLibrary;

namespace IMPALWeb
{
    public partial class GLAccMaster : System.Web.UI.Page
    {
        public string MainCodeFlag = " ";
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["RoleCode"] == "BEDP")
            {
                GV_GLSubMaster.ShowFooter = false;
                int EditButtonIndex = GV_GLSubMaster.Columns.Count - 1;
                GV_GLSubMaster.Columns[EditButtonIndex].Visible = false;
            }
            try
            {
                if (!IsPostBack)
                {
                    DropDownList GLMainCode = (DropDownList)GV_GLSubMaster.FooterRow.FindControl("ddGLMainGroup");
                    TextBox txtGlmaincode = (TextBox)GV_GLSubMaster.FooterRow.FindControl("txtGLMainCode");

                    GLAccMasters ss = new GLAccMasters();
                    DropDownList GLSubCode = new DropDownList();
                    DropDownList ddGLSubDescription = (DropDownList)GV_GLSubMaster.FooterRow.FindControl("ddGLSubDescription");
                    TextBox txtGlsubcode = (TextBox)GV_GLSubMaster.FooterRow.FindControl("txtGLSubCode");
                    txtGlmaincode.Text = GLMainCode.SelectedValue.ToString();
                }
                // string glSubDescription_selectedValue = ddGLSubDescription.Items.FindByText(GLMainCode.SelectedItem.ToString()).Value;
                // ddGLSubDescription.SelectedValue = glSubDescription_selectedValue;
                //txtGlsubcode.Text = glSubDescription_selectedValue;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }



        protected void GV_GLSubMaster_DataBinding(object sender, EventArgs e)
        {
            // GV_GLGroup.Columns[0].Visible = false;
        }

        protected void GV_GLSubMaster_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Insert data if the CommandName == "Insert" 
            // and the validation controls indicate valid data...
            if (e.CommandName == "Insert" && Page.IsValid)
            {
                Button btn = (Button)e.CommandSource;
                GLAccMasters ss1 = new GLAccMasters();
                //ApplicationSegments appSegment = new ApplicationSegments();
                DropDownList GLMainCode = (DropDownList)GV_GLSubMaster.FooterRow.FindControl("ddGLMainGroup");
                TextBox GLSubCode = (TextBox)btn.FindControl("txtGLSubCode");
                TextBox GLAccCode = (TextBox)btn.FindControl("txtGLAccCode");
                if (ss1.AccSubMaster_FindExists(GLMainCode.SelectedValue.Trim(), GLSubCode.Text.Trim(), GLAccCode.Text.Trim()))
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Duplicate Record Exists');", true);
                }
                else
                {
                    ObjectDSGLSubMaster.Insert();
                }
            }


            // else if (e.CommandName == "Update" && Page.IsValid)
            //   ObjectDataGLGroup.Update();
        }
        protected void ObjectDataGLMain_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            //DropDownList GLMainCode = (DropDownList)GV_GLSubMaster.FooterRow.FindControl("ddGLMainGroup");
            //TextBox GLSubCode = (TextBox)GV_GLSubMaster.FooterRow.FindControl("txtGLSubCode");
            //TextBox GLSubDesc = (TextBox)GV_GLSubMaster.FooterRow.FindControl("txtNewSubDescription");
            //// DropDownList GLCLCode = (DropDownList)GV_GLSubMaster.FooterRow.FindControl("ddlGLClassification");
            //e.InputParameters["GLMasterCode"] = GLMainCode.SelectedValue;
            //e.InputParameters["GLSubMasterCode"] = GLSubCode.Text;
            //e.InputParameters["GLSubMasterDesc"] = GLSubDesc.Text;

        }

        protected void ObjectDSGLSubMaster_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            DropDownList GLMainCode = (DropDownList)GV_GLSubMaster.FooterRow.FindControl("ddGLMainGroup");
            TextBox GLSubCode = (TextBox)GV_GLSubMaster.FooterRow.FindControl("txtGLSubCode");
            DropDownList GLSubDesc = (DropDownList)GV_GLSubMaster.FooterRow.FindControl("ddGLSubDescription");
            TextBox GLAccCode = (TextBox)GV_GLSubMaster.FooterRow.FindControl("txtGLAccCode");
            TextBox GLAccDesc = (TextBox)GV_GLSubMaster.FooterRow.FindControl("txtNewAccDescription");
            // DropDownList GLCLCode = (DropDownList)GV_GLSubMaster.FooterRow.FindControl("ddlGLClassification");
            e.InputParameters["GLMasterCode"] = GLMainCode.SelectedValue;
            e.InputParameters["GLSubMasterCode"] = GLSubCode.Text;
            e.InputParameters["GLSubMasterDesc"] = GLSubDesc.SelectedValue;
            e.InputParameters["GLAccMasterCode"] = GLAccCode.Text;
            e.InputParameters["GLAccMasterDesc"] = GLAccDesc.Text;

        }

        protected void ddGLMainGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList GLMainCode = (DropDownList)GV_GLSubMaster.FooterRow.FindControl("ddGLMainGroup");
                TextBox txtGlmaincode = (TextBox)GV_GLSubMaster.FooterRow.FindControl("txtGLMainCode");

                GLAccMasters ss = new GLAccMasters();
                DropDownList GLSubCode = new DropDownList();
                DropDownList ddGLSubDescription = (DropDownList)GV_GLSubMaster.FooterRow.FindControl("ddGLSubDescription");
                TextBox txtGlsubcode = (TextBox)GV_GLSubMaster.FooterRow.FindControl("txtGLSubCode");
                System.Web.UI.WebControls.Parameter objParam1 = new System.Web.UI.WebControls.Parameter("glmainCode", DbType.String, GLMainCode.SelectedValue);
                ODSGLSub.SelectParameters["glmainCode"] = objParam1;
                ODSGLSub.DataBind();
                ddGLSubDescription.Items.Insert(0, "");
                txtGlmaincode.Text = GLMainCode.SelectedValue.ToString();
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        protected void ddGLMainGroup_TextChanged(object sender, EventArgs e)
        {
            DropDownList GLMainCode = (DropDownList)GV_GLSubMaster.FooterRow.FindControl("ddGLMainGroup");
        }

        protected void ddGLSubDescription_DataBound(object sender, EventArgs e)
        {

        }

        protected void GV_GLSubMaster_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnReport_Click", "Report btn Clicked");
            try
            {
                Server.Execute("GLAccMasterReport.aspx");
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ddGLSubDescription_SelectedIndexChanged(object sender, EventArgs e)
        {

            DropDownList ddGLSubDescription = (DropDownList)GV_GLSubMaster.FooterRow.FindControl("ddGLSubDescription");
            TextBox txtGlsubcode = (TextBox)GV_GLSubMaster.FooterRow.FindControl("txtGLSubCode");
            txtGlsubcode.Text = ddGLSubDescription.SelectedValue;
        }


    }
}
