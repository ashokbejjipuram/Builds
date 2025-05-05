using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;

namespace IMPALWeb
{
    public partial class ApplicationSegment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["RoleCode"] == "BEDP")
            {
                GV_ApplnSegment.ShowFooter = false;
                int EditButtonIndex = GV_ApplnSegment.Columns.Count - 1;
                GV_ApplnSegment.Columns[EditButtonIndex].Visible = false;

            }
        }

        protected void GV_ApplnSegment_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GV_ApplnSegment_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        protected void GV_ApplnSegment_DataBinding(object sender, EventArgs e)
        {
            // GV_GLGroup.Columns[0].Visible = false;
        }

        protected void GV_ApplnSegment_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Insert data if the CommandName == "Insert" 
            // and the validation controls indicate valid data...
            if (e.CommandName == "Insert" && Page.IsValid)
            {
                Button btn = (Button)e.CommandSource;
                ApplicationSegments appSegment = new ApplicationSegments();
                TextBox txt = (TextBox)btn.FindControl("txtNewApplnCode");
                if (appSegment.FindExists(txt.Text.Trim()))
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Application Code Already Exists');", true);
                }
                else
                {
                    ObjectDataASeg.Insert();
                }



            }

        }

        protected void ODSAppln_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            TextBox ApplnCode = (TextBox)GV_ApplnSegment.FooterRow.FindControl("txtNewApplnCode");
            TextBox ApplnDesc = (TextBox)GV_ApplnSegment.FooterRow.FindControl("txtNewApplnDescription");

            e.InputParameters["ApplicationSegmentCode"] = ApplnCode.Text;
            e.InputParameters["ApplnSegmentDescription"] = ApplnDesc.Text;


        }


        protected void ODSAppln_Updating(object sender, ObjectDataSourceMethodEventArgs e)
        {
        }

        protected void GV_ApplnSegment_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        #region report button click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnReport_Click", "Inside btnReport_Click");
            try
            {
                Server.Execute("ApplicationSegmentReport.aspx");
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion
    }
}
