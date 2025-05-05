using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IMPALWeb.Security
{
    public partial class UserRoleMapping : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["RoleCode"] == "BEDP")
            {
                gvUserRole.ShowFooter = false;
                int EditButtonIndex = gvUserRole.Columns.Count - 1;
                gvUserRole.Columns[EditButtonIndex].Visible = false;
            }
        }

        protected void gvUserRole_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void gvUserRole_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddUserRole_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvUserRole_DataBinding(object sender, EventArgs e)
        {

        }

        protected void gvUserRole_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}
