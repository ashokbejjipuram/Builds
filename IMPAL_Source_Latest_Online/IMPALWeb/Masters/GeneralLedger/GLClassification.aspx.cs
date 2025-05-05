using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;

namespace IMPALWeb
{
    public partial class GLClassification : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["RoleCode"] == "BEDP")
            {
                GV_GLClassification.ShowFooter = false;
                int EditButtonIndex = GV_GLClassification.Columns.Count - 1;
                GV_GLClassification.Columns[EditButtonIndex].Visible = false;

            }
        }



        protected void GV_GLClassification_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GV_GLClassification_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        protected void GV_GLClassification_DataBinding(object sender, EventArgs e)
        {
            // GV_GLGroup.Columns[0].Visible = false;
        }

        protected void GV_GLClassification_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Insert data if the CommandName == "Insert" 
            // and the validation controls indicate valid data...
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetSqlStringCommand("select MAX(Gl_Classification_Code) from GL_Classification");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            var Rows=ImpalDB.ExecuteScalar(cmd);
            if (e.CommandName == "Insert" && Page.IsValid)
            {
                if (Convert.ToInt32(Rows) >= 9)
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Cannot Add Records');", true); 
                }
                if (GV_GLClassification.Rows.Count <= 9 )
                    ObjectDataGLCL.Insert();
                else
                { 
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Cannot Add Records');", true);
                }
            }
        }

        protected void ODSGLClassification_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {

            TextBox GLDesc = (TextBox)GV_GLClassification.FooterRow.FindControl("txtNewGLCLDescription");

            e.InputParameters["GLCLDescription"] = GLDesc.Text;
        }



        protected void ODSGLClassification_Updating(object sender, ObjectDataSourceMethodEventArgs e)
        {
        }

        protected void GV_GLClassification_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        #region Report button click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnReport_Click", "Inside btnReport_Click");
            try
            {
                Server.ClearError();
                Response.Redirect("GLCLReport.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion
    }
}
