using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;

namespace IMPALWeb
{
    public partial class VehicleType : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["RoleCode"] == "BEDP")
            {
                GV_VehicleType.ShowFooter = false;
                int EditButtonIndex = GV_VehicleType.Columns.Count - 1;
                GV_VehicleType.Columns[EditButtonIndex].Visible = false;
            }
        }

        protected void GV_VehicleType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GV_VehicleType_DataBinding(object sender, EventArgs e)
        {
            // GV_GLGroup.Columns[0].Visible = false;
        }

        protected void GV_VehicleType_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Insert" && Page.IsValid)
            {
                Button btn = (Button)e.CommandSource;
                VehilcleTypes VTypes = new VehilcleTypes();
                TextBox txt = (TextBox)btn.FindControl("txtNewVechileTypeCode");
                if (VTypes.FindExists(txt.Text.Trim()))
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Vehicle Code Already Exists');", true);
                }
                else
                {
                    ObjectDataVT.Insert();
                }
            }
        }

        protected void ODSVT_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            TextBox VTCode = (TextBox)GV_VehicleType.FooterRow.FindControl("txtNewVechileTypeCode");
            TextBox VTDesc = (TextBox)GV_VehicleType.FooterRow.FindControl("txtNewVehicleTypeDescription");

            e.InputParameters["VehicleTypeCode"] = VTCode.Text;
            e.InputParameters["VehicleTypeDescription"] = VTDesc.Text;


        }


        protected void ODSAppln_Updating(object sender, ObjectDataSourceMethodEventArgs e)
        {
        }

        protected void GV_VehicleType_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }
        #region report button click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnReport_Click", "Inside btnReport_Click");
            try
            {
                Server.Execute("VehicleTypeReport.aspx");
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }
        #endregion
    }
}
