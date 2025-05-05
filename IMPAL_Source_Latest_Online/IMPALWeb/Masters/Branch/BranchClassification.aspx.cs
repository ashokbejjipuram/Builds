using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;

namespace IMPALWeb
{
    public partial class BranchClassification : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["RoleCode"] == "BEDP")
            {
                GV_BRClassification.ShowFooter = false;
                int EditButtonIndex = GV_BRClassification.Columns.Count - 1;
                GV_BRClassification.Columns[EditButtonIndex].Visible = false;
            }
        }



        protected void GV_BRClassification_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GV_BRClassification_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        protected void GV_BRClassification_DataBinding(object sender, EventArgs e)
        {
            // GV_GLGroup.Columns[0].Visible = false;
        }

        protected void GV_BRClassification_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Insert data if the CommandName == "Insert" 
            // and the validation controls indicate valid data...
            if (e.CommandName == "Insert" && Page.IsValid)
                ObjectDataBRCL.Insert();


        }

        protected void ODSBRClassification_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            TextBox BRCLCode = (TextBox)GV_BRClassification.FooterRow.FindControl("txtNewBRCLCode");
            TextBox BRCLDesc = (TextBox)GV_BRClassification.FooterRow.FindControl("txtNewBRCLDescription");
            TextBox BRCLOSLimit = (TextBox)GV_BRClassification.FooterRow.FindControl("txtNewBROSLimit");
            TextBox BRCLOSDays = (TextBox)GV_BRClassification.FooterRow.FindControl("txtNewBROSDays");

            e.InputParameters["ClassificationCode"] = BRCLCode.Text;
            e.InputParameters["ClassificationDescription"] = BRCLDesc.Text;
            e.InputParameters["OutstandingLimit"] = BRCLOSLimit.Text;
            e.InputParameters["OutstandingDays"] = BRCLOSDays.Text;






        }



        protected void ODSBRClassification_Updating(object sender, ObjectDataSourceMethodEventArgs e)
        {
        }

        protected void GV_BRClassification_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        #region Report Button Click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnReport_Click", "Inside btnReport_Click");
            try
            {
                Server.Execute("BRCLReport.aspx");
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion
    }
}
