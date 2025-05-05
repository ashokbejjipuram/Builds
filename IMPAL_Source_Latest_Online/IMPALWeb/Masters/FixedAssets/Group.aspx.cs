using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;

namespace IMPALWeb
{
    public partial class Group : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["RoleCode"] == "BEDP")
            {
                GV_Group.ShowFooter = false;
                int EditButtonIndex = GV_Group.Columns.Count - 1;
                GV_Group.Columns[EditButtonIndex].Visible = false;
            }

        }
        protected void GV_Group_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GV_Group_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        protected void GV_Group_DataBinding(object sender, EventArgs e)
        {
            // GV_GLGroup.Columns[0].Visible = false;
        }

        protected void GV_Group_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Insert data if the CommandName == "Insert" 
            // and the validation controls indicate valid data...
            if (e.CommandName == "Insert" && Page.IsValid)
                ObjectDataGroup.Insert();

        }

        protected void ODSGroup_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            TextBox Desc = (TextBox)GV_Group.FooterRow.FindControl("txtNewDescription");
            e.InputParameters["GLCLDescription"] = Desc.Text;
        }



        protected void ODSGroup_Updating(object sender, ObjectDataSourceMethodEventArgs e)
        {
        }

        protected void GV_Group_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        #region Report button click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnReport_Click", "Inside btnReport_Click");
            try
            {
                Server.Execute("GroupReport.aspx");
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion
    }
}
