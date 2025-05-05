using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;

namespace IMPALWeb
{
    public partial class ProductGroup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["RoleCode"] == "BEDP")
            {
                GV_ProductGroup.ShowFooter = false;
                int EditButtonIndex = GV_ProductGroup.Columns.Count - 1;
                GV_ProductGroup.Columns[EditButtonIndex].Visible = false;
            }

        }

        protected void GV_ProductGroup_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GV_ProductGroup_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        protected void GV_ProductGroup_DataBinding(object sender, EventArgs e)
        {
            // GV_GLGroup.Columns[0].Visible = false;
        }

        protected void GV_ProductGroup_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Insert data if the CommandName == "Insert" 
            // and the validation controls indicate valid data...
            if (e.CommandName == "Insert" && Page.IsValid)
                ObjectDataPG.Insert();

        }

        protected void ODSGV_ProductGroup_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            TextBox PGDesc = (TextBox)GV_ProductGroup.FooterRow.FindControl("txtNewPGDescription");

            e.InputParameters["ProductGroupDescription"] = PGDesc.Text;
        }

        protected void ODSProductGroup_Updating(object sender, ObjectDataSourceMethodEventArgs e)
        {
        }

        protected void GV_ProductGroup_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        #region report button click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnReport_Click", "Inside btnReport_Click");
            try
            {
                Server.Execute("ProductGroupReport.aspx");
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion
    }
}
