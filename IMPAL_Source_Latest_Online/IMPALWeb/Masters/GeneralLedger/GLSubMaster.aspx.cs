using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

namespace IMPALWeb.Masters
{
    public partial class GLSubMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["RoleCode"] == "BEDP")
            {
                GV_GLSubMaster.ShowFooter = false;
                int EditButtonIndex = GV_GLSubMaster.Columns.Count - 1;
                GV_GLSubMaster.Columns[EditButtonIndex].Visible = false;
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

                ObjectDSGLSubMaster.Insert();
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
            TextBox GLSubDesc = (TextBox)GV_GLSubMaster.FooterRow.FindControl("txtNewSubDescription");
            // DropDownList GLCLCode = (DropDownList)GV_GLSubMaster.FooterRow.FindControl("ddlGLClassification");
            e.InputParameters["GLMasterCode"] = GLMainCode.SelectedValue;
            e.InputParameters["GLSubMasterCode"] = GLSubCode.Text;
            e.InputParameters["GLSubMasterDesc"] = GLSubDesc.Text;

        }
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnReport_Click", "Report Button Clicked");
            try
            {
                Server.Execute("GLSubMasterReport.aspx");
                //Response.Redirect("GLSubMasterReport.aspx");
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }


       
        
    }
}
