using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IMPALWeb.Security
{
    public partial class Roles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["RoleCode"] == "BEDP")
            {
                gvRoles.ShowFooter = false;
                int EditButtonIndex = gvRoles.Columns.Count - 1;
                gvRoles.Columns[EditButtonIndex].Visible = false;
            }
        }

        protected void gvRoles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Insert data if the CommandName == "Insert" 
            // and the validation controls indicate valid data...
            if (e.CommandName == "Insert" && Page.IsValid)

                odRoles.Insert();



        }

        protected void odRoles_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {

            TextBox RoleName = (TextBox)gvRoles.FooterRow.FindControl("txtNewRoleName");
            TextBox RoleCode = (TextBox)gvRoles.FooterRow.FindControl("txtNewRoleCode");
            e.InputParameters["RoleName"] = RoleName.Text.Trim();
            e.InputParameters["RoleCode"] = RoleCode.Text.Trim(); ;

        }

        protected void gvRoles_DataBinding(object sender, EventArgs e)
        {

        }

        protected void gvRoles_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void gvRoles_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
