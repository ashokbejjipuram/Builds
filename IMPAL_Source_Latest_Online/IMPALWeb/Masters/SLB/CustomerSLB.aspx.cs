using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.ServiceLocation;
using IMPALLibrary;

namespace IMPALWeb
{
    public partial class CustomerSLB : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.Parameter brCode = new System.Web.UI.WebControls.Parameter("strBranchCode", DbType.String, Session["BranchCode"].ToString());
            ODSSLB.SelectParameters["strBranchCode"] = brCode;

            ODSCustomer.SelectParameters["strBranchCode"] = brCode;

            if ((string)Session["RoleCode"] == "BEDP")
            {
                GV_SLBGroup.ShowFooter = false;
                int EditButtonIndex = GV_SLBGroup.Columns.Count - 1;
                GV_SLBGroup.Columns[EditButtonIndex].Visible = false;

            }
        }

        protected void GV_SLBGroup_DataBinding(object sender, EventArgs e)
        {
            // GV_GLGroup.Columns[0].Visible = false;
        }

        protected void GV_SLBGroup_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Insert data if the CommandName == "Insert" 
            // and the validation controls indicate valid data...
            if (e.CommandName == "Insert" && Page.IsValid)

                ODSSLB.Insert();
            // else if (e.CommandName == "Update" && Page.IsValid)
            //   ObjectDataGLGroup.Update();



        }

        protected void ODSSLB_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            //TextBox GLDesc = (TextBox)GV_SLBGroup.FooterRow.FindControl("txtNewDescription");
            DropDownList CustCode = (DropDownList)GV_SLBGroup.FooterRow.FindControl("ddlCustomerName");
            DropDownList SuppCode = (DropDownList)GV_SLBGroup.FooterRow.FindControl("ddlSupplier");
            DropDownList SLBCode = (DropDownList)GV_SLBGroup.FooterRow.FindControl("ddlSLB");

            e.InputParameters["CustomerCode"] = CustCode.SelectedValue;
            e.InputParameters["SupplierLineCode"] = SuppCode.SelectedValue;
            e.InputParameters["SLBCode"] = SLBCode.SelectedValue;
        }

        protected void GV_SLBGroup_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //TextBox CustCode = (TextBox)GV_SLBGroup.FooterRow.FindControl("txtCustCode");
            //TextBox SuppCode = (TextBox)GV_SLBGroup.FooterRow.FindControl("txtSuppCode");
            //TextBox SLBCode = (TextBox)GV_SLBGroup.FooterRow.FindControl("txtSLBCode");

            //e.InputParameters["CustomerCode"] = CustCode.Text;
            //e.InputParameters["SupplierLineCode"] = SuppCode.Text;
            //e.InputParameters["SLBCode"] = SLBCode.Text; 

        }

        protected void GV_SLBGroup_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ODSSLB_Updating(object sender, ObjectDataSourceMethodEventArgs e)
        {
        }

        protected void ddlCustomerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList CustName = (DropDownList)GV_SLBGroup.FooterRow.FindControl("ddlCustomerName");
                TextBox txtCustCode = (TextBox)GV_SLBGroup.FooterRow.FindControl("txtNewCustCode");
                txtCustCode.Text = CustName.SelectedValue.ToString();
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        protected void ddlSLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddlSLB = (DropDownList)GV_SLBGroup.FooterRow.FindControl("ddlSLB");
                TextBox txtNewSLBCode = (TextBox)GV_SLBGroup.FooterRow.FindControl("txtNewSLBCode");
                txtNewSLBCode.Text = ddlSLB.SelectedValue.ToString();
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddlSupplier = (DropDownList)GV_SLBGroup.FooterRow.FindControl("ddlSupplier");
                TextBox txtNewSuppCode = (TextBox)GV_SLBGroup.FooterRow.FindControl("txtNewSuppCode");
                txtNewSuppCode.Text = ddlSupplier.SelectedValue.ToString();
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }        

        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnReport_Click", "Inside btnReport_Click");
            try
            {
                Server.ClearError();
                Response.Redirect("CustomerSLBReport.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Server.ClearError();
                Response.Redirect("CustomerSLB.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}
