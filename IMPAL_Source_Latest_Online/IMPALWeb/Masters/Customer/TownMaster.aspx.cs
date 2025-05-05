using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;

namespace IMPALWeb
{
    public partial class TownMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["RoleCode"] == "BEDP")
            {
                GV_Town.ShowFooter = false;
                int EditButtonIndex = GV_Town.Columns.Count - 1;
                GV_Town.Columns[EditButtonIndex].Visible = false;
            }

        }

        protected void GV_Town_DataBinding(object sender, EventArgs e)
        {
           // GV_GLGroup.Columns[0].Visible = false;
        }

        protected void GV_Town_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Insert data if the CommandName == "Insert" 
            // and the validation controls indicate valid data...
            if (e.CommandName == "Insert" && Page.IsValid)
                ODSTown.Insert();
           // else if (e.CommandName == "Update" && Page.IsValid)
             //   ObjectDataGLGroup.Update();
    
        }

        protected void ODSTown_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            TextBox TName = (TextBox)GV_Town.FooterRow.FindControl("txtNewName");
            DropDownList BrCode = (DropDownList)GV_Town.FooterRow.FindControl("ddlTBranch");
            //e.InputParameters["Towncode"] = null;
            e.InputParameters["TownName"] = TName.Text;
            e.InputParameters["BrCode"] = BrCode.SelectedValue;

        }
        protected void ODSTown_Updating(object sender, ObjectDataSourceMethodEventArgs e)
        {
        }

        #region Report button click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnReport_Click", "Inside btnReport_Click");
            try
            {
                Server.Execute("TownReport.aspx");
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion
    }
}
