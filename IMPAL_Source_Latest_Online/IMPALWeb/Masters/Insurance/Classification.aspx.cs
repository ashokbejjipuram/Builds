using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;

namespace IMPALWeb
{
    public partial class Classification : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["RoleCode"] == "BEDP")
            {
                GV_Classification.ShowFooter = false;
                int EditButtonIndex = GV_Classification.Columns.Count - 1;
                GV_Classification.Columns[EditButtonIndex].Visible = false;
            }

        }

        protected void GV_Classification_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GV_Classification_DataBinding(object sender, EventArgs e)
        {
           // GV_GLGroup.Columns[0].Visible = false;
        }

        protected void GV_Classification_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Insert data if the CommandName == "Insert" 
            // and the validation controls indicate valid data...
            if (e.CommandName == "Insert" && Page.IsValid)
                ODSClassification.Insert();        
           
        }

        protected void ODSClassification_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            TextBox Desc = (TextBox)GV_Classification.FooterRow.FindControl("txtNewDescription");
            TextBox MinVal = (TextBox)GV_Classification.FooterRow.FindControl("txtNewMinValue");
            TextBox MaxVal = (TextBox)GV_Classification.FooterRow.FindControl("txtNewMaxValue");
            
            e.InputParameters["Description"] = Desc.Text;
            e.InputParameters["MinimumValue"] = MinVal.Text;
            e.InputParameters["MaximumValue"] = MaxVal.Text;
        }

        protected void ODSClassification_Updating(object sender, ObjectDataSourceMethodEventArgs e)
        {
        }

        protected void GV_Classification_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }
        #region ReportButton Click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnReport_Click", "Report Button Clicked");
            try
            {
                Server.Execute("ClassificationReport.aspx");               
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion
    }
}
