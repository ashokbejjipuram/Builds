using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IMPALWeb.Masters.Others
{
    public partial class CustomerSlab : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["RoleCode"] == "BEDP")
            {
                //grdCustomerSlab.ShowFooter = false;
                //int EditButtonIndex = grdCustomerSlab.Columns.Count - 1;
                //grdCustomerSlab.Columns[EditButtonIndex].Visible = false;
            }
        }

        protected void ODSCustomerSlab_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            //e.InputParameters["StateCode"] = ((DropDownList)grdCustomerSlab.FooterRow.FindControl("ddlState")).SelectedValue.ToString();
            //e.InputParameters["PartyTypeCode"] = ((DropDownList)grdCustomerSlab.FooterRow.FindControl("ddlPartyType")).SelectedValue.ToString();
            //e.InputParameters["SlabCode"] = ((DropDownList)grdCustomerSlab.FooterRow.FindControl("ddlSlab")).SelectedValue.ToString();
            //e.InputParameters["SupplierLineCode"] = ((DropDownList)grdCustomerSlab.FooterRow.FindControl("ddlSupplierLine")).SelectedValue.ToString(); 
        }

        protected void grdCustomerSlab_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //if (e.CommandName == "Insert" && Page.IsValid)
                //ODSCustomerSlab.Insert();
        }


    }
}
