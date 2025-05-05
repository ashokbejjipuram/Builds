using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;

namespace IMPALWeb
{
    public partial class ClassificationDocuments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if ((string)Session["BranchCode"] == "CRP")
            {

                DropDownList CLCode = (DropDownList)GV_Documents.FooterRow.FindControl("ddlclassification");
                TextBox txtCLCode = (TextBox)GV_Documents.FooterRow.FindControl("txtCLCode");
                txtCLCode.Text = CLCode.SelectedValue.ToString();
                DropDownList docCode = (DropDownList)GV_Documents.FooterRow.FindControl("ddlDescription");
                TextBox txtDocCode = (TextBox)GV_Documents.FooterRow.FindControl("txtDocCode");
                txtDocCode.Text = docCode.SelectedValue.ToString();
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
                ODSDocuments1.Insert();
        }

        protected void ODSdocuments1_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            TextBox CLCode = (TextBox)GV_Documents.FooterRow.FindControl("txtCLCode");
            TextBox DocCode = (TextBox)GV_Documents.FooterRow.FindControl("txtDocCode");
            DropDownList Status = (DropDownList)GV_Documents.FooterRow.FindControl("ddlStatus");

            e.InputParameters["classificationCode"] = CLCode.Text;
            e.InputParameters["classification"] = "";
            e.InputParameters["documentCode"] = DocCode.Text;
            e.InputParameters["documents"] = "";
            e.InputParameters["status"] = Status.SelectedValue.ToString();

        }

        protected void ODSdocuments1_Updating(object sender, ObjectDataSourceMethodEventArgs e)
        {
        }

        protected void GV_Documents_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
        }

        protected void ddlclassification_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList CLCode = (DropDownList)GV_Documents.FooterRow.FindControl("ddlclassification");
                TextBox txtCLCode = (TextBox)GV_Documents.FooterRow.FindControl("txtCLCode");
                txtCLCode.Text = CLCode.SelectedValue.ToString();
            }
            catch (Exception exe)
            {
                throw exe;
            }
        }

        protected void ddlDescription_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList docCode = (DropDownList)GV_Documents.FooterRow.FindControl("ddlDescription");
                TextBox txtDocCode = (TextBox)GV_Documents.FooterRow.FindControl("txtDocCode");
                txtDocCode.Text = docCode.SelectedValue.ToString();
            }
            catch (Exception exe)
            {
                throw exe;
            }

        }

        #region Report button click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnReport_Click", "Inside btnReport_Click");
            try
            {
                Server.ClearError();
                Response.Redirect("ClDocReport.aspx", false);
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
