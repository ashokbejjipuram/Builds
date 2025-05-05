using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;

namespace IMPALWeb
{
    public partial class StateMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["RoleCode"] == "BEDP")
            {
                GV_StateMaster.ShowFooter = false;
                int EditButtonIndex = GV_StateMaster.Columns.Count - 1;
                GV_StateMaster.Columns[EditButtonIndex].Visible = false;
            }
        }

        protected void GV_StateMaster_DataBinding(object sender, EventArgs e)
        {
        }

        protected void GV_StateMaster_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Insert data if the CommandName == "Insert" 
            // and the validation controls indicate valid data...
            if (e.CommandName == "Insert" && Page.IsValid)

                ObjectDataStateMaster.Insert();
            //else if (e.CommandName == "Update")
            //    ObjectDataStateMaster.Update();

        }

        protected void ObjectDataStateMaster_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            TextBox StateCode = (TextBox)GV_StateMaster.FooterRow.FindControl("lblStateCode");
            TextBox StateName = (TextBox)GV_StateMaster.FooterRow.FindControl("txtStateNameNew");
            DropDownList ZoneCode = (DropDownList)GV_StateMaster.FooterRow.FindControl("ddlZoneName");

            //            DropDownList ZoneCode1 = (DropDownList)GV_StateMaster.FooterRow.FindControl("ddlZone");
            e.InputParameters["StateCode"] = null;
            e.InputParameters["StateName"] = StateName.Text;
            e.InputParameters["ZoneCode"] = ZoneCode.SelectedValue;
        }

        protected void ObjectDataStateMaster_Updating(object sender, ObjectDataSourceMethodEventArgs e)
        {
        }

        #region Report Button Click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnReport_Click", "Inside btnReport_Click");
            try
            {
                Server.Execute("StateMasterReport.aspx");
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

    }
}
