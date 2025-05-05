using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;

namespace IMPALWeb
{
    public partial class Zone : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["RoleCode"] == "BEDP")
            {
                GV_Zone.ShowFooter = false;
                int EditButtonIndex = GV_Zone.Columns.Count - 1;
                GV_Zone.Columns[EditButtonIndex].Visible = false;
            }
        }

        protected void GV_Zone_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GV_Zone_DataBinding(object sender, EventArgs e)
        {
            // GV_GLGroup.Columns[0].Visible = false;
        }

        protected void GV_Zone_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Insert data if the CommandName == "Insert" 
            // and the validation controls indicate valid data...
            if (e.CommandName == "Insert" && Page.IsValid)
                ObjectDataZone.Insert();
        }

        protected void ODSZone_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {

            TextBox zoneName = (TextBox)GV_Zone.FooterRow.FindControl("txtNewZoneName");

            e.InputParameters["ZoneName"] = zoneName.Text;
        }

        protected void ODSZone_Updating(object sender, ObjectDataSourceMethodEventArgs e)
        {
        }

        protected void GV_Zone_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        #region Report Button Click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnReport_Click", "Inside btnReport_Click");
            try
            {
                Server.Execute("ZoneReport.aspx");
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion
    }
}
