using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.ServiceLocation;
using System.Data;
using System.Data.Common;
using System.Collections;

namespace IMPALWeb.Masters.Others
{
    public partial class ClearingAgent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["RoleCode"] == "BEDP")
            {
                grdClearingAgent.ShowFooter = false;
                int EditButtonIndex = grdClearingAgent.Columns.Count - 1;
                grdClearingAgent.Columns[EditButtonIndex].Visible = false;
            }
        }

        protected void ODSClearingAgent_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            e.InputParameters["BranchCode"] = ((DropDownList)grdClearingAgent.FooterRow.FindControl("ddlBranch")).SelectedValue.ToString();
            e.InputParameters["AgentName"] = ((TextBox)grdClearingAgent.FooterRow.FindControl("txtAgentName")).Text.Trim();
            e.InputParameters["Address"] = ((TextBox)grdClearingAgent.FooterRow.FindControl("txtAddress")).Text.Trim();
            e.InputParameters["Phone"] = ((TextBox)grdClearingAgent.FooterRow.FindControl("txtPhone")).Text.Trim();
            e.InputParameters["Fax"] = ((TextBox)grdClearingAgent.FooterRow.FindControl("txtFax")).Text.Trim();
            e.InputParameters["EMail"] = ((TextBox)grdClearingAgent.FooterRow.FindControl("txtEMail")).Text.Trim();
            e.InputParameters["ContactPerson"] = ((TextBox)grdClearingAgent.FooterRow.FindControl("txtContactPerson")).Text.Trim();
            e.InputParameters["Remarks"] = ((TextBox)grdClearingAgent.FooterRow.FindControl("txtRemarks")).Text.Trim();
        }

        protected void grdClearingAgent_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Insert" && Page.IsValid)
                ODSClearingAgent.Insert();
        }
        #region report button click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnReport_Click", "Inside btnReport_Click");
            try
            {
                Server.Execute("ClearingAgentReport.aspx");
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion
    }
}
