using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;


namespace IMPALWeb
{
    public partial class AreaManagerBranch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["RoleCode"] == "BEDP")
            {
                GV_AMB.ShowFooter = false;
                int EditButtonIndex = GV_AMB.Columns.Count - 1;
                GV_AMB.Columns[EditButtonIndex].Visible = false;
            }
        }
        protected void GV_AMB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GV_AMB_DataBinding(object sender, EventArgs e)
        {
            // GV_GLGroup.Columns[0].Visible = false;
        }

        protected void GV_AMB_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Insert data if the CommandName == "Insert" 
            // and the validation controls indicate valid data...
            if (e.CommandName == "Insert" && Page.IsValid)
                ObjectDataAMB.Insert();

        }

        protected void ODSAMB_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {

            TextBox Code = (TextBox)GV_AMB.FooterRow.FindControl("txtNewAMBCode");
            DropDownList AMName = (DropDownList)GV_AMB.FooterRow.FindControl("ddlAMBName");
            TextBox BMName = (TextBox)GV_AMB.FooterRow.FindControl("txtNewBMName");
            DropDownList OpBranch = (DropDownList)GV_AMB.FooterRow.FindControl("ddlOpBranch");
            TextBox sDate = (TextBox)GV_AMB.FooterRow.FindControl("txtAMBStDate");
            TextBox eDate = (TextBox)GV_AMB.FooterRow.FindControl("txtAMBEDate");

            e.InputParameters["AreaManagerCode"] = Code.Text;
            e.InputParameters["AreaManagerName"] = AMName.Text;
            e.InputParameters["OperatingBranch"] = OpBranch.SelectedValue;
            e.InputParameters["BranchManager"] = BMName.Text;
            e.InputParameters["StartDate"] = sDate.Text;
            e.InputParameters["EndDate"] = eDate.Text;

        }



        protected void ODSAMB_Updating(object sender, ObjectDataSourceMethodEventArgs e)
        {
            DropDownList OpBranch = (DropDownList)GV_AMB.FooterRow.FindControl("ddlAMBBranch");
            e.InputParameters["OperatingBranch"] = OpBranch.SelectedValue;
        }

        protected void GV_AMB_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        #region Report Button Click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnReport_Click", "Inside btnReport_Click");
            try
            {
                Server.Execute("AMBRReport.aspx");
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion
    }
}
