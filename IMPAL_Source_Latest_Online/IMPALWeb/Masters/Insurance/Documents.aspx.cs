using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;

namespace IMPALWeb
{
    public partial class Documents : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["RoleCode"] == "BEDP")
            {
                GV_Documents.ShowFooter = false;
                int EditButtonIndex = GV_Documents.Columns.Count - 1;
                GV_Documents.Columns[EditButtonIndex].Visible = false;
            }
        }



        protected void GV_Documents_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GV_Documents_DataBinding(object sender, EventArgs e)
        {
           // GV_GLGroup.Columns[0].Visible = false;
        }

        protected void GV_Documents_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Insert data if the CommandName == "Insert" 
            // and the validation controls indicate valid data...
            if (e.CommandName == "Insert" && Page.IsValid)
                ODSDocuments.Insert();        
           
        }

        protected void ODSdocuments_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            TextBox Desc = (TextBox)GV_Documents.FooterRow.FindControl("txtNewDescription");
            e.InputParameters["Description"] = Desc.Text;
        }

        protected void ODSdocuments_Updating(object sender, ObjectDataSourceMethodEventArgs e)
        {
        }

        protected void GV_Documents_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }
        #region Report Button Click
        protected void btnReport_Click(object sender, EventArgs e)
        { 
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnReport_Click", "Report Button Clicked");
            try
            {
                Server.Execute("DocumentsReport.aspx");
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }        }
        #endregion
    }
}
