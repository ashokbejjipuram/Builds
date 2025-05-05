using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;

namespace IMPALWeb
{
    public partial class Location : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["RoleCode"] == "BEDP")
            {
                GV_Location.ShowFooter = false;
                int EditButtonIndex = GV_Location.Columns.Count - 1;
                GV_Location.Columns[EditButtonIndex].Visible = false;
            }


        }

        protected void GV_Location_DataBinding(object sender, EventArgs e)
        {
            // GV_GLGroup.Columns[0].Visible = false;
        }

        protected void GV_Location_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Insert data if the CommandName == "Insert" 
            // and the validation controls indicate valid data...
            if (e.CommandName == "Insert" && Page.IsValid)
                ODSLocation.Insert();
            // else if (e.CommandName == "Update" && Page.IsValid)
            //   ObjectDataGLGroup.Update();

        }

        protected void ODSLocation_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            TextBox TName = (TextBox)GV_Location.FooterRow.FindControl("txtNewName");
            DropDownList BrCode = (DropDownList)GV_Location.FooterRow.FindControl("ddlLBranch");
            //e.InputParameters["Towncode"] = null;
            e.InputParameters["FALocationName"] = TName.Text;
            e.InputParameters["BrCode"] = BrCode.SelectedValue;

        }
        protected void ODSLocation_Updating(object sender, ObjectDataSourceMethodEventArgs e)
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
                Response.Redirect("LocationReport.aspx", false);
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
