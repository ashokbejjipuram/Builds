using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;

namespace IMPALWeb
{
    public partial class SLB : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["RoleCode"] == "BEDP")
            {
                GV_SLB.ShowFooter = false;
                int EditButtonIndex = GV_SLB.Columns.Count - 1;
                GV_SLB.Columns[EditButtonIndex].Visible = false;

            }
        }

        protected void GV_SLB_DataBinding(object sender, EventArgs e)
        {
            // GV_GLGroup.Columns[0].Visible = false;
        }

        protected void GV_SLB_RowCommand(object sender, GridViewCommandEventArgs e)
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
            TextBox Code = (TextBox)GV_SLB.FooterRow.FindControl("txtNewSlbCode");
            TextBox Desc = (TextBox)GV_SLB.FooterRow.FindControl("txtNewSlbDesc");
            TextBox minValue = (TextBox)GV_SLB.FooterRow.FindControl("txtNewMinValue");
            TextBox maxValue = (TextBox)GV_SLB.FooterRow.FindControl("txtNewMaxValue");

            e.InputParameters["SLBCode"] = Code.Text.Trim();
            e.InputParameters["Description"] = Desc.Text.Trim();
            e.InputParameters["MinValue"] = minValue.Text.Trim();
            e.InputParameters["MaxValue"] = maxValue.Text.Trim();


            RadioButton Indicator = (RadioButton)GV_SLB.FooterRow.FindControl("rbQtyIndicator");
            if (Indicator.Checked)
                e.InputParameters["Indicator"] = "Q";
            else

                e.InputParameters["Indicator"] = "V";
        }
        protected void ODSSLB_Updating(object sender, ObjectDataSourceMethodEventArgs e)
        {
            RadioButton Indicator1 = (RadioButton)GV_SLB.FooterRow.FindControl("rbNewQtyIndicator");

            RadioButton Indicator = GV_SLB.Rows[GV_SLB.EditIndex].FindControl("rbNewQtyIndicator") as RadioButton;
            if (Indicator.Checked)
                e.InputParameters["Indicator"] = "Q";
            else

                e.InputParameters["Indicator"] = "V";

        }

        protected void txtMinValue_TextChanged(object sender, EventArgs e)
        {

        }

        #region Report button click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnReport_Click", "Inside btnReport_Click");
            try
            {
                Server.ClearError();
                Response.Redirect("SLBReport.aspx", false);
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
